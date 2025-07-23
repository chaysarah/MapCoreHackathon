import { runCodeSafely, runMapCoreSafely } from '../common/services/error-handling/errorHandler';
import ObjectWorldTreeNodeModel, { objectWorldNodeType, treeNodeActions } from '../components/shared/models/tree-node.model';
import { MapCoreData, OverlayManager, ViewportData } from 'mapcore-lib';
import { ObjectWorldService } from 'mapcore-lib';
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import TreeService, { nodeType } from './tree.service';
import printMapService from './printMap.service';
import store from '../redux/store';


class objectWorldTreeService extends TreeService {
    //Members
    public dialogClassNames: Map<string, string> = new Map([
        ["More Details", 'overlay-manager__scroll-dialog-more-details'],
        ["Private Properties Of Object", 'scroll-dialog-props-id-list'],
        ["Private Properties Of ObjectScheme", 'scroll-dialog-props-id-list'],
        ['Save to File', 'scroll-dialog-save-to-file'],
        ['Viewports List', 'scroll-dialog-viewports-list'],
        ['Load Objects from Raw Vector Data', 'scroll-dialog-load-obj-from-raw-vec-data'],
        ['Conditional Selector Parameters', 'scroll-dialog-base-conditional-selector'],
        ["Add Object", 'scroll-dialog-add-obj-from-scheme'],
        ["Clone Object", 'scroll-dialog-clone-object'],
        ["Move To Other Overlay", 'scroll-dialog-move-to-other-overlay'],
        ["Replace Scheme", 'scroll-dialog-replace-scheme'],
        ['Scheme Objects List', 'scroll-dialog-scheme-obj-list'],
    ]);

    buildTree = (): ObjectWorldTreeNodeModel => {
        let finalTree: ObjectWorldTreeNodeModel = this.createTreeNodeRecursive(objectWorldNodeType.ROOT, objectWorldNodeType.ROOT);
        return finalTree;
    }
    createTreeNodeRecursive = (node: any, nodeType_: nodeType, fatherKey = '-1', ind = -1): ObjectWorldTreeNodeModel => {
        let currentNode: ObjectWorldTreeNodeModel | any = {};
        currentNode.key = nodeType_ === objectWorldNodeType.ROOT ? '0' : `${fatherKey}_${ind !== -1 ? ind : '0'}`;
        currentNode.nodeMcContent = node;
        currentNode.nodeType = nodeType_;
        currentNode.id = this.getObjectHash(currentNode);
        currentNode.label = nodeType_ != 'Root' ? `(${currentNode.id}) ${this.getTreeNodeLabel(node, nodeType_)}` : this.getTreeNodeLabel(node, nodeType_);//actually this property has to be taken from mapCore, it has to be the name that the user called to this node||currentNode.key.
        currentNode.children = [];

        switch (nodeType_) {
            //degree 1
            case objectWorldNodeType.ROOT:
                let arrOverlayManager: OverlayManager[] = MapCoreData.overlayManagerArr;
                arrOverlayManager.forEach((overlayManager: OverlayManager, ind: number) => {
                    let tmpNode = this.createTreeNodeRecursive(overlayManager.overlayManager, objectWorldNodeType.OVERLAY_MANAGER, currentNode.key, ind);
                    currentNode.children.push(tmpNode)
                })
                const standAloneItems = store.getState().objectWorldTreeReducer.standAloneItems;
                standAloneItems.forEach((standAloneItem: { item: MapCore.IMcObjectSchemeNode; itemType: number }, ind: number) => {
                    let tmpNode = this.createTreeNodeRecursive(standAloneItem.item, objectWorldNodeType.ITEM, currentNode.key, arrOverlayManager.length + ind);
                    currentNode.children.push(tmpNode)
                })
                break;
            //degree 2
            case objectWorldNodeType.OVERLAY_MANAGER:
                let overlays: MapCore.IMcOverlay[] = node.GetOverlays();
                overlays.forEach((overlay: MapCore.IMcOverlay, ind: number) => {
                    let tmpNode = this.createTreeNodeRecursive(overlay, objectWorldNodeType.OVERLAY, currentNode.key, ind);
                    currentNode.children.push(tmpNode)
                });
                let objSchemes: MapCore.IMcObjectScheme[] = node?.GetObjectSchemes();
                objSchemes.forEach((objectScheme: MapCore.IMcObjectScheme, ind: number) => {
                    let tmpNode = this.createTreeNodeRecursive(objectScheme, objectWorldNodeType.OBJECT_SCHEME, currentNode.key, overlays.length + ind);
                    currentNode.children.push(tmpNode)
                });
                let conditionalSelectors: MapCore.IMcConditionalSelector[] = node?.GetConditionalSelectors();
                conditionalSelectors.forEach((conditionalS: MapCore.IMcConditionalSelector, ind: number) => {
                    let tmpNode = this.createTreeNodeRecursive(conditionalS, objectWorldNodeType.CONDITIONAL_SELECTOR, currentNode.key, overlays.length + objSchemes.length + ind);
                    currentNode.children.push(tmpNode)
                })
                //getCollections
                break;
            //degree 3
            case objectWorldNodeType.OVERLAY:
                let objs: MapCore.IMcObject[] = node.GetObjects()
                objs?.forEach((object: MapCore.IMcObject, ind: number) => {
                    let tmpNode = this.createTreeNodeRecursive(object, objectWorldNodeType.OBJECT, currentNode.key, ind);
                    currentNode.children.push(tmpNode)
                });
                break;
            case objectWorldNodeType.OBJECT_SCHEME:
                let numOfObjLocations = node.GetNumObjectLocations();
                for (let index = 0; index < numOfObjLocations; index++) {
                    let objLocation = node.GetObjectLocation(index);
                    let tmpNode = this.createTreeNodeRecursive(objLocation, objectWorldNodeType.OBJECT_LOCATION, currentNode.key, index);
                    currentNode.children.push(tmpNode)
                }
                break;
            case objectWorldNodeType.CONDITIONAL_SELECTOR:

                break;
            //degree 4
            case objectWorldNodeType.OBJECT:

                break;
            case objectWorldNodeType.OBJECT_LOCATION:
                let objLocationSchemeNode = node as MapCore.IMcObjectSchemeNode;
                let items = objLocationSchemeNode.GetChildren();
                items?.forEach((item: MapCore.IMcObjectSchemeNode, ind: number) => {
                    let tmpNode = this.createTreeNodeRecursive(item, objectWorldNodeType.ITEM, currentNode.key, ind);
                    currentNode.children.push(tmpNode)
                });
                break;
            case objectWorldNodeType.ITEM:
                let objSchemeNode = node as MapCore.IMcObjectSchemeNode;
                let schemeItems = objSchemeNode.GetChildren();
                schemeItems?.forEach((item: MapCore.IMcObjectSchemeNode, ind: number) => {
                    let tmpNode = this.createTreeNodeRecursive(item, objectWorldNodeType.ITEM, currentNode.key, ind);
                    currentNode.children.push(tmpNode)
                });
                break;
            default:
                break;
        }
        return currentNode;
    }
    getOMMCViewportsByOverlay = (treeNode: ObjectWorldTreeNodeModel, overlay: ObjectWorldTreeNodeModel): ViewportData[] => {
        let currentOM = this.getParentByChildKey(treeNode, overlay.key);
        let OMMapCoreContent = currentOM.nodeMcContent as MapCore.IMcOverlayManager;
        let vpList: ViewportData[] = [];
        runCodeSafely(() => {
            MapCoreData.viewportsData.forEach((v: ViewportData) => {
                let vpOverlayManager = v.viewport.GetOverlayManager();
                if (vpOverlayManager === OMMapCoreContent)
                    vpList.push(v);
            })
        }, 'objectWorldTreeService.getOMMCViewportsByOverlay');
        return vpList;
    }
    removeObjectLocation = (tree: ObjectWorldTreeNodeModel, selectedKey: string): void => {
        runCodeSafely(() => {
            let objectScheme = this.getParentByChildKey(tree, selectedKey).nodeMcContent;
            let locationIndex = parseInt(selectedKey.substring(selectedKey.length - 1));
            runMapCoreSafely(() => {
                objectScheme.RemoveObjectLocation(locationIndex);
            }, "treeNodeActions.removeObjectLocation => IMcObjectScheme.RemoveObjectLocation", true)
        }, 'objectWorldTreeService.removeObjectLocation');
    }
    removeObject = (tree: ObjectWorldTreeNodeModel, selectedNode: ObjectWorldTreeNodeModel): void => {
        runCodeSafely(() => {
            printMapService.deleteObjectFromLists(selectedNode.nodeMcContent);
            runMapCoreSafely(() => { selectedNode.nodeMcContent.Remove() }, "treeNodeActions.removeObject => IMcObject.Remove", true)
        }, 'objectWorldTreeService.removeObject')
    }
    removeOverlay(tree: ObjectWorldTreeNodeModel, overlay: ObjectWorldTreeNodeModel): void {
        let mcOverlayManager = this.getParentByChildKey(tree, overlay.key).nodeMcContent;
        //In case that the removed overlay is the active overlay, the active overlay change to Null
        if (overlay.nodeMcContent == ObjectWorldService.findActiveOverlayByOM(mcOverlayManager)) {
            ObjectWorldService.setActiveOverlayInOverlayManager(mcOverlayManager, null)
        }
        //TO DO -RemoveTempAnchorPoints when there will be anchorPoints  

        runMapCoreSafely(() => {
            overlay.nodeMcContent.Remove();
        }, "treeNodeActions.removeObjectLocation => IMcOverlay.Remove", true)
    }
    // renameNode(treeNode: ObjectWorldTreeNodeModel, keyToChange: string, newName: string): ObjectWorldTreeNodeModel {
    //     if (!treeNode) {
    //         return null;
    //     }
    //     if (treeNode.key === keyToChange) {
    //         let userNodeName = treeNode.label.substring(treeNode.label.lastIndexOf(')') + 1)
    //         return { ...treeNode, label: `(${newName}) ${userNodeName}` };
    //     }
    //     return {
    //         ...treeNode,
    //         children: treeNode.children.map((child) => this.renameNode(child, keyToChange, newName)),
    //     };
    // }
    moveToLocation(selectedObject: ObjectWorldTreeNodeModel, locationIndex: number, viewport: MapCore.IMcMapViewport): void | string {
        let mcObject = selectedObject.nodeMcContent as MapCore.IMcObject;
        let objectLocations: MapCore.SMcVector3D[] = mcObject.GetLocationPoints(locationIndex);
        let largestPoint = objectLocations[0];
        let smallestPoint = objectLocations[0];
        objectLocations.forEach(point => {
            if (point.x >= largestPoint.x && point.y >= largestPoint.y) {
                largestPoint = point;
            }
            if (point.x <= smallestPoint.x && point.y <= smallestPoint.y) {
                smallestPoint = point;
            }
        });

        let averagePoint = MapCore.SMcVector3D.Div(MapCore.SMcVector3D.Plus(largestPoint, smallestPoint), 2);
        if (viewport) {
            let mcObjectLocation = mcObject.GetScheme().GetObjectLocation(locationIndex);
            let objLocationCoordSys: MapCore.EMcPointCoordSystem = mcObjectLocation.GetCoordSystem();
            this.ConvertObjectLocationToCameraLocation(viewport, averagePoint, objLocationCoordSys, mcObject, mcObjectLocation);
        }
        else {
            return 'Missing Viewport';
        }
    }
    showNodePoints(treeNode: ObjectWorldTreeNodeModel, showNodePointsType: string, activeViewport: ViewportData, mcCurrentOverlayManager: MapCore.IMcOverlayManager, mcObject: MapCore.IMcObject) {
        let coordinateSystemAndPoints: { coordinateSystem: MapCore.EMcPointCoordSystem, points: MapCore.SMcVector3D[] }[];
        coordinateSystemAndPoints = this.getObjectOrSchemeNodeCoordAndPoints(treeNode, showNodePointsType, mcObject, activeViewport);
        let overlayAndObject: { overlay: MapCore.IMcOverlay, object: MapCore.IMcObject } = ObjectWorldService.showNodePoints(mcCurrentOverlayManager, coordinateSystemAndPoints)
        return overlayAndObject;
    }
    unShowNodePoints(overlay: MapCore.IMcOverlay, object: MapCore.IMcObject) {
        runCodeSafely(() => {
            runMapCoreSafely(() => { object.Remove() }, "objectWorldTreeService.unShowNodePoints => IMcObject.Remove", true)
            runMapCoreSafely(() => { overlay.Remove(); }, "objectWorldTreeService.unShowNodePoints => IMcOverlay.Remove", true)
        }, 'objectWorldTreeService.unShowNodePoints')
    }

    //#endregion
    ConvertObjectLocationToCameraLocation(viewport: MapCore.IMcMapViewport, averagePoint: MapCore.SMcVector3D, objLocationCoordSys: MapCore.EMcPointCoordSystem, mcObject: MapCore.IMcObject, mcObjectLocation: MapCore.IMcObjectLocation) {
        runCodeSafely(() => {
            let mcViewportImageCalc = viewport.GetImageCalc();
            let isConvert = true;
            let isUseCallback = false;
            let cameraPoint: MapCore.SMcVector3D;
            switch (objLocationCoordSys) {
                case MapCore.EMcPointCoordSystem.EPCS_WORLD:
                    if (!mcViewportImageCalc) {
                        cameraPoint = viewport.OverlayManagerToViewportWorld(averagePoint);
                    }
                    else if (viewport.GetOverlayManager() && averagePoint) {
                        cameraPoint = viewport.GetOverlayManager().ConvertWorldToImage(averagePoint, mcViewportImageCalc);
                    }
                    else {
                        isConvert = false;
                    }
                    break;
                case MapCore.EMcPointCoordSystem.EPCS_SCREEN:
                    let isIntersect: boolean;
                    let pCameraPoint: { Value?: MapCore.SMcVector3D } = {};
                    isIntersect = viewport.ScreenToWorldOnTerrain(averagePoint, pCameraPoint);
                    cameraPoint = pCameraPoint.Value;
                    if (!isIntersect) {
                        isIntersect = viewport.ScreenToWorldOnPlane(averagePoint, pCameraPoint);
                        cameraPoint = pCameraPoint.Value;
                    }
                    if (!isIntersect) {
                        cameraPoint.x = cameraPoint.y = cameraPoint.z = 0;
                    }
                    break;
                case MapCore.EMcPointCoordSystem.EPCS_IMAGE:
                    // let objImageCalc = mcObject.GetImageCalc();
                    // if (!objImageCalc) {
                    //     isConvert = false;
                    // }
                    // else if (mcViewportImageCalc || objImageCalc !== mcViewportImageCalc) {
                    //     isUseCallback = true;
                    //     //let clickAsyncQueryCallback= new cAsyncCallback //need to implement it - sm
                    //     // cameraPoint = objImageCalc.ImagePixelToCoordWorld();
                    // }
                    break;
                default:
                    break;
            }

            if (!isUseCallback && isConvert) {
                this.ConvertObjectLocationToCameraLocationResult(viewport, cameraPoint, mcObjectLocation);
            }
        }, 'objectWorldTreeService.ConvertObjectLocationToCameraLocation');
    }
    ConvertObjectLocationToCameraLocationResult(viewport: MapCore.IMcMapViewport, cameraPoint: MapCore.SMcVector3D, mcObjectLocation: MapCore.IMcObjectLocation) {
        if (viewport.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_2D) {
            viewport.SetCameraPosition(cameraPoint, false);
        }
        else if (viewport.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_3D) {
            let isRelative = mcObjectLocation.GetRelativeToDTM();
            if (isRelative) {
                let pHeight: { Value?: number } = {};
                let isHeightFound = viewport.GetTerrainHeight(cameraPoint, pHeight);
                if (isHeightFound)
                    cameraPoint.z = pHeight.Value;
                else
                    cameraPoint.z = 0;

            }
            cameraPoint.z = cameraPoint.z + 500;
            viewport.SetCameraPosition(cameraPoint, false);
            viewport.SetCameraOrientation(0, -90, 0, false);
        }
    }
    getSchemeAndObjectOfSchemeNodeTreeNode(treeNode: ObjectWorldTreeNodeModel): { scheme: MapCore.IMcObjectScheme, schemeObjects: MapCore.IMcObject[] } {
        //find the scheme
        let scheme: MapCore.IMcObjectScheme = null;
        let schemeObjects: MapCore.IMcObject[] = [];
        if (treeNode.nodeType == objectWorldNodeType.OBJECT_SCHEME) {
            scheme = treeNode.nodeMcContent;
            schemeObjects = scheme.GetObjects();
        }
        else if (treeNode.nodeType == objectWorldNodeType.OBJECT) {
            scheme = treeNode.nodeMcContent.GetScheme();
            schemeObjects = [treeNode.nodeMcContent];
        }
        else if ([objectWorldNodeType.OBJECT_LOCATION, objectWorldNodeType.ITEM].includes(treeNode.nodeType)) {
            scheme = treeNode.nodeMcContent.GetScheme();
            schemeObjects = scheme.GetObjects();
        }
        //find the object by the scheme
        return { scheme: scheme, schemeObjects: schemeObjects };
    }
    getObjectOrSchemeNodeCoordAndPoints(treeNode: ObjectWorldTreeNodeModel, showNodePointsType: string, object: MapCore.IMcObject, viewport: ViewportData) {
        // let coordinateSystem: MapCore.EMcPointCoordSystem = MapCore.EMcPointCoordSystem.EPCS_WORLD;
        let coordAndPointsObjArr: { coordinateSystem: MapCore.EMcPointCoordSystem, points: MapCore.SMcVector3D[] }[] = [];
        let isSymbolicItem = treeNode.nodeType == objectWorldNodeType.ITEM && treeNode.nodeMcContent.GetNodeKind() == MapCore.IMcObjectSchemeNode.ENodeKindFlags.ENKF_SYMBOLIC_ITEM// nodeKind == symbolicItemEnum.code;
        if (isSymbolicItem && [treeNodeActions.CALCULATED_AND_ADDED_POINTS, treeNodeActions.CALCULATED_POINTS].includes(showNodePointsType)) {//Symbolic item with calculated 
            let symbolicItem = treeNode.nodeMcContent as MapCore.IMcSymbolicItem;
            let pCoordinateSystem: { Value?: MapCore.EMcPointCoordSystem } = {};
            let pauOriginalPointsIndices: { Value?: Uint32Array } = {};
            let points: MapCore.SMcVector3D[] = [];
            // TODO ToCheck
            symbolicItem.GetAllCalculatedPoints(viewport.viewport, object, points, pCoordinateSystem, pauOriginalPointsIndices);
            if (showNodePointsType == treeNodeActions.CALCULATED_POINTS && pauOriginalPointsIndices.Value.length > 0) {//calculated points only when there is indices
                let indicesArr = Array.from(pauOriginalPointsIndices.Value);
                points = points.filter((point, index) => indicesArr.includes(index));
            }
            coordAndPointsObjArr = [...coordAndPointsObjArr, { coordinateSystem: pCoordinateSystem.Value, points: points }];
        }
        else if ([objectWorldNodeType.OBJECT_LOCATION, objectWorldNodeType.ITEM].includes(treeNode.nodeType)) {//item when it is not symbolic or based points chosen , or objectLocation 
            let coordAndPointsObj = this.getSchemeNodeCoordAndPoints(viewport, treeNode.nodeMcContent, object);
            coordAndPointsObjArr = [...coordAndPointsObjArr, coordAndPointsObj];
        }
        else if ([objectWorldNodeType.OBJECT, objectWorldNodeType.OBJECT_SCHEME].includes(treeNode.nodeType)) {
            let scheme: MapCore.IMcObjectScheme = treeNode.nodeType == objectWorldNodeType.OBJECT ? treeNode.nodeMcContent.GetScheme() : treeNode.nodeMcContent;
            let nodeKindArr = getEnumDetailsList(MapCore.IMcObjectSchemeNode.ENodeKindFlags);
            let objectLocationEnum = getEnumValueDetails(MapCore.IMcObjectSchemeNode.ENodeKindFlags.ENKF_OBJECT_LOCATION, nodeKindArr);
            let nodes = scheme.GetNodes(objectLocationEnum.code);
            nodes.forEach(node => {
                let coordAndPointsObj = this.getSchemeNodeCoordAndPoints(viewport, node, object);
                coordAndPointsObjArr = [...coordAndPointsObjArr, coordAndPointsObj];
            });
        }
        return coordAndPointsObjArr;
    }
    getSchemeNodeCoordAndPoints(viewport: ViewportData, mcSchemeNode: MapCore.IMcObjectSchemeNode, object: MapCore.IMcObject) {
        let coordinateSystem = mcSchemeNode.GetGeometryCoordinateSystem(object);
        let points = mcSchemeNode.GetCoordinates(viewport.viewport, coordinateSystem, object);
        return { coordinateSystem: coordinateSystem, points: points };
    }
    renameNode(treeNode: ObjectWorldTreeNodeModel, keyToChange: string, newName: string): ObjectWorldTreeNodeModel {
        if (!treeNode) {
            return null;
        }
        if (treeNode.key === keyToChange) {
            let userNodeName = treeNode.label.substring(treeNode.label.lastIndexOf(')') + 1)
            return { ...treeNode, label: `(${newName}) ${userNodeName}` };
        }
        return {
            ...treeNode,
            children: treeNode.children.map((child) => this.renameNode(child, keyToChange, newName)),
        };
    }
}

export default new objectWorldTreeService();

export class fileExplorerSelectionInListBoxService {
    lastClick: ObjectWorldTreeNodeModel;

    handleClick = (itemsList: ObjectWorldTreeNodeModel[], e: any) => {
        let selectedItems: ObjectWorldTreeNodeModel[] = [];

        let currentObj = itemsList.find(obj => obj.label == e.originalEvent.target?.textContent);

        if (e.originalEvent.shiftKey) {
            let lastClickIndex = this.lastClick ? parseInt(this.lastClick.key.substring(this.lastClick.key.length - 1)) : 0;
            let currentClickIndex = parseInt(currentObj.key.substring(currentObj.key.length - 1));
            let startI = lastClickIndex > currentClickIndex ? currentClickIndex : lastClickIndex;
            let endI = lastClickIndex > currentClickIndex ? lastClickIndex : currentClickIndex;
            for (let index = startI; index <= endI; index++) {
                selectedItems = [...selectedItems, itemsList[index]];
            }
        }
        else if (e.originalEvent.ctrlKey) {
            selectedItems = e.value;
        }
        else {
            selectedItems = [currentObj];
        }
        this.lastClick = currentObj;
        return selectedItems;
    }
}

