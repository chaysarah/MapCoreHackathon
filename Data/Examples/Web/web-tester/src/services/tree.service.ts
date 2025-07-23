import { TreeExpandedKeysType } from "primereact/tree";

import { TypeToStringService } from 'mapcore-lib'
import { runCodeSafely, runMapCoreSafely } from "../common/services/error-handling/errorHandler";
import MapWorldTreeNodeModel, { mapWorldNodeType } from "../components/shared/models/map-tree-node.model";
import ObjectWorldTreeNodeModel, { objectWorldNodeType } from "../components/shared/models/tree-node.model";

export type nodeType = objectWorldNodeType | mapWorldNodeType;
export type TreeNodeModel = MapWorldTreeNodeModel | ObjectWorldTreeNodeModel;

abstract class TreeService {
    abstract buildTree(): TreeNodeModel;
    abstract createTreeNodeRecursive: (node: any, nodeType_: nodeType, fatherKey: string, ind: number) => TreeNodeModel

    getNodeByKey = (tree: TreeNodeModel, key: string): TreeNodeModel | null => {
        if (tree?.key === key) {
            return tree;
        }

        if (tree?.children) {
            for (const child of tree.children) {
                const foundNode = this.getNodeByKey(child, key);
                if (foundNode) {
                    return foundNode;
                }
            }
        }

        return null;
    }
    getParentByChildKey = (tree: TreeNodeModel, key: string): TreeNodeModel | null => {
        const parts = key.split('_');
        if (parts.length <= 1) {
            return null;
        }
        parts.pop();
        const fatherKey = parts.join('_');
        const fatherNode = this.getNodeByKey(tree, fatherKey);
        return fatherNode;
    }
    getNearestParentByType = (tree: TreeNodeModel, treeNode: TreeNodeModel, nodeType: nodeType): TreeNodeModel | null => {
        const typedNodeArr = this.findNodesByType(tree, nodeType);
        const filteredTypeKeysArr = typedNodeArr.filter(node => treeNode.key.startsWith(node.key)).map(node => node.key)
        const nearestParentKey = filteredTypeKeysArr.reduce((a, b) => a.length >= b.length ? a : b, '');
        const nearestParentNode = this.getNodeByKey(tree, nearestParentKey);
        return nearestParentNode;
    }
    removeChildLocally = (treeNode: TreeNodeModel[], keyToRemove: string): TreeNodeModel[] => {
        return treeNode.reduce((accumulatorArr, node) => {
            if (node.key === keyToRemove) {
                // Remove the node
                return accumulatorArr;
            }

            if (node.children && node.children.length > 0) {
                // Recursively remove child from children
                const updatedChildren = this.removeChildLocally(node.children, keyToRemove);
                accumulatorArr.push({ ...node, children: updatedChildren });
            } else {
                accumulatorArr.push(node);
            }

            return accumulatorArr;
        }, []);
    }
    getObjectHash = (obj: any) => {
        const str = JSON.stringify(obj);
        let hash = 0;

        if (str.length === 0) return hash;

        for (let i = 0; i < str.length; i++) {
            const char = str.charCodeAt(i);
            hash = (hash << 5) - hash + char;
            hash = hash & hash; // Convert to 32-bit integer
        }
        const finalHash = hash > 0 ? hash : -1 * hash;
        return finalHash;
    }
    //#region Virtual File Systm Functions
    getDirectoriesFromFilesPaths(paths: string[]): string[] {
        const directories = paths.reduce((acc, path) => {
            const dirParts = path.split('/').slice(0, -1);
            for (let i = dirParts.length - 1; i >= 0; i--) {
                const currentDir = [...dirParts.slice(0, i + 1)].join('/');
                if (!acc.map(d => d.dir).flat().includes(currentDir) && currentDir != '') {
                    acc.push({ dir: currentDir, slashCount: i });
                }
            }
            return acc;
        }, []);

        // Filtering duplicate results and deleting unnecessary information
        return directories.sort((a, b) => a.slashCount - b.slashCount).map((d) => d.dir);
    }
    getFilePathesFromVirtualDirectory(rootDirectoryName: string) {
        const allFilePaths = [];

        function traverse(path) {
            const currentFolderOrDir = path.split('/').at(-1);
            const isValidPath = currentFolderOrDir && currentFolderOrDir != '.' && currentFolderOrDir != '..';
            if (isValidPath && currentFolderOrDir.includes('.')) {//file type
                allFilePaths.push(path);
            }
            else if (isValidPath) {//directory
                let content = [];
                runMapCoreSafely(() => {
                    content = MapCore.IMcMapDevice.GetFileSystemDirectoryContents(path);
                }, 'treeService.getFilePathesFromVirtualRootDirectory => IMcMapDevice.GetFileSystemDirectoryContents', true)
                content = content.map(con => path + '/' + con);
                content.forEach((con) => traverse(con));
            }
        }

        traverse(rootDirectoryName);
        return allFilePaths;
    }
    getFileExtension(fileName) {
        const lastDotIndex = fileName.lastIndexOf(".");
        if (lastDotIndex === -1 || lastDotIndex == fileName.length - 1) return ""; // No dot, or dot at the end
        return fileName.slice(lastDotIndex + 1);
    }
    deleteSystemDirectories = (directories: string[]) => {
        runCodeSafely(() => {
            const mainDirs = directories.filter(dir => !dir.includes('/'));
            mainDirs.forEach((dir) => {
                const subDirs = directories.filter(d => d.startsWith(dir) && d.includes('/'));
                this.deleteSystemDirectoryByDirName(dir, subDirs);
            })
        }, 'objectWorldTreeService.deleteSystemDirectories')
    }
    private deleteSystemDirectoryByDirName = (directoryName: string, subDirectories: string[]) => {
        runMapCoreSafely(() => {
            const dirContent = MapCore.IMcMapDevice.GetFileSystemDirectoryContents(directoryName);
            dirContent.forEach(dirOrFile => {
                if (dirOrFile != '.' && dirOrFile != '..') {
                    if (subDirectories.includes(dirOrFile)) {//dir
                        const currentSubDirs = subDirectories.filter(dir => dir.startsWith(dirOrFile) && dir.includes('/'));
                        this.deleteSystemDirectoryByDirName(`${directoryName}/${dirOrFile}`, currentSubDirs);
                    }
                    else {//file
                        MapCore.IMcMapDevice.DeleteFileSystemFile(`${directoryName}/${dirOrFile}`);
                    }
                }
            });
            MapCore.IMcMapDevice.DeleteFileSystemEmptyDirectory(directoryName)
        }, 'objectWorldTreeService.deleteSystemDirectoryByDirName => IMcMapDevice.DeleteFileSystemFile', true);
    }
    //#endregion
    //#region treeNode actions
    expandNode = (tree: TreeNodeModel, selectedKey: string): TreeExpandedKeysType => {
        const selectedNode = this.getNodeByKey(tree, selectedKey);
        let _expandedKeys: TreeExpandedKeysType = {};
        _expandedKeys[selectedNode.key] = true;
        for (let node of selectedNode.children) {
            this.expandNodeRecursive(node, _expandedKeys);
        }
        return _expandedKeys;
    }
    expandNodeRecursive = (node: TreeNodeModel, _expandedKeys: TreeExpandedKeysType): void => {
        if (node.children && node.children.length) {
            _expandedKeys[node.key] = true;
            for (let child of node.children) {
                this.expandNodeRecursive(child, _expandedKeys);
            }
        }
    };
    collapseNode = (tree: TreeNodeModel, selectedKey: string, expandedKeys: TreeExpandedKeysType): TreeExpandedKeysType => {
        const selectedNode = this.getNodeByKey(tree, selectedKey);
        let _expandedKeys: TreeExpandedKeysType = {};
        Object.keys(expandedKeys).forEach(key => {
            if (!key.startsWith(selectedNode.key)) {
                _expandedKeys[key] = true;
            }
        })
        return _expandedKeys;
    }

    convertMapcorObjectToTreeNodeModel(tree: TreeNodeModel, MCObj: MapCore.IMcBase): TreeNodeModel | null {
        if (tree?.nodeMcContent == MCObj) {
            return tree;
        }

        if (tree?.children) {
            for (const child of tree.children) {
                const foundNode = this.convertMapcorObjectToTreeNodeModel(child, MCObj);
                if (foundNode) {
                    return foundNode;
                }
            }
        }

        return null;
    }
    abstract renameNode(treeNode: TreeNodeModel, keyToChange: string, newName: string): TreeNodeModel;
    //#endregion

    //help function for the tree visible (not mapcore logic functions)
    getObjIDHeader(id: number): string {
        const finalID = id == MapCore.MC_EMPTY_ID ? '' : `${id}`;
        return finalID;
    }
    getObjIDValue(id: string): number {
        const finalValue = Number.isNaN(parseInt(id)) ? MapCore.MC_EMPTY_ID : parseInt(id);
        return finalValue;
    }
    getTreeNodeLabel(node: any, _nodeType: nodeType) {
        switch (_nodeType) {
            // object world
            case objectWorldNodeType.CONDITIONAL_SELECTOR:
                const selectorType: number = node.GetConditionalSelectorType();
                return TypeToStringService.getSelectorTypeNameByTypeNumber(selectorType);
            case objectWorldNodeType.ITEM:
                const itemType: number = node.GetNodeType();
                return TypeToStringService.getItemNodeTypeByTypeNumber(itemType);

            // map world
            case mapWorldNodeType.MAP_LAYER:
                const layerType: number = node.GetLayerType();
                return TypeToStringService.getLayerTypeByTypeNumber(layerType);
            case mapWorldNodeType.MAP_GRID:
                const mcGrid = node as MapCore.IMcMapGrid;
                const gridRegions = mcGrid.GetGridRegions();
                let gridFinalName = `${_nodeType}`;
                if (gridRegions.length == 1 && gridRegions[0].pCoordinateSystem) {
                    gridFinalName = `${gridFinalName} [${TypeToStringService.convertNumberToGridString(gridRegions[0].pCoordinateSystem.GetGridCoorSysType(), true)}]`;
                }
                return gridFinalName;
            default:
                return _nodeType
        }
    }

    findNodesByType(tree: TreeNodeModel, targetType: mapWorldNodeType | objectWorldNodeType) {
        const foundNodes = [];

        function traverse(node: TreeNodeModel) {
            if (!node) return;

            if (node.nodeType === targetType) {
                foundNodes.push(node);
            }

            if (node.children) {
                node.children.forEach((node: TreeNodeModel) => { traverse(node) });
            }
        }

        traverse(tree);
        return foundNodes;
    }

}

export default TreeService;
