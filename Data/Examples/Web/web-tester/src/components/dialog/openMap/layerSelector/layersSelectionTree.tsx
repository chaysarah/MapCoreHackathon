import { useState, useEffect, useRef } from 'react';
import { TreeSelectionEvent } from 'primereact/tree';
import { TreeTable, TreeTableSelectionKeysType } from 'primereact/treetable';
import { useDispatch, useSelector } from 'react-redux';
import { Column } from 'primereact/column';

import { LayerDetails, LayerSourceEnum, LayerNameEnum, MapCoreData, MapCoreService } from 'mapcore-lib';
import './styles/manualLayers.css';
import Footer from '../../footerDialog';
import { AppState } from '../../../../redux/combineReducer';
import { runCodeSafely } from '../../../../common/services/error-handling/errorHandler';
import generalService from '../../../../services/general.service';
import { TreeNodeModel } from '../../../../services/tree.service';
import objectWorldTreeService from '../../../../services/objectWorldTree.service';
import { setObjectWorldTree } from '../../../../redux/ObjectWorldTree/objectWorldTreeActions';

interface objData {
    key: string,
    label: string,
    data: any,
    children: objChildren[],
}

interface objChildren {
    key: string,
    label: string,
    data: any
}

const TreeSelection = (props: { onChooseLayers: (layerId: LayerDetails[]) => void, setDisplayTreeDialog: (bool: boolean) => void, serverLayers: MapCore.IMcMapLayer.SServerLayerInfo[], urlServer: string }) => {
    const selectGroups = (mapServer: MapCore.IMcMapLayer.SServerLayerInfo[]) => {
        let result: string[] = [];
        mapServer.forEach((layer: MapCore.IMcMapLayer.SServerLayerInfo) => {
            layer.astrGroups.forEach((group: string) => {
                if (!result.includes(group)) {
                    result = [...result, group];
                }
            });
        });

        return result;
    };

    const serverGroups = selectGroups(props.serverLayers);
    let jsonData: objData[] = [];
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor);
    const [nodes, setNodes] = useState<objData[]>([]);
    const [selectedKeys, setSelectedKeys] = useState(null);
    const [selectedLayers, setSelectedLayers] = useState(null);
    const toast = useRef(null);

    useEffect(() => {
        const root = document.documentElement;
        let pixelWidth = window.innerHeight * 0.6 * globalSizeFactor;
        root.style.setProperty('--layers-selection-tree-dialog-width', `${pixelWidth}px`);
        generateJson()
    }, []);

    useEffect(() => {
        runCodeSafely(() => {
            updateTreeRedux();
            if (MapCoreData.device == null)
                MapCoreService.initDevice(generalService.mapCorePropertiesBase.deviceInitParams);
        }, "SeveralLayersDialog.useEffect => []")
    }, []);
    const dispatch = useDispatch();

    const updateTreeRedux = () => {
        runCodeSafely(() => {
            let buildedTree: TreeNodeModel = objectWorldTreeService.buildTree();
            dispatch(setObjectWorldTree(buildedTree));
        }, "objectWorldTreeDialog.useEffect");
    }

    const generateJson = () => {
        serverGroups.forEach((group: string, index: number) => {
            jsonData[index] = {
                key: '',
                label: '',
                data: null,
                children: []
            }
            jsonData[index].key = JSON.stringify(index);
            jsonData[index].label = group;
            jsonData[index].data = group;
            jsonData[index].children = [];
            let _index: number = 0;
            props.serverLayers.forEach((_layer) => {
                if (_layer.astrGroups[0] == group) {
                    jsonData[index].children![_index] = {
                        key: '',
                        label: '',
                        data: null
                    }
                    jsonData[index].children[_index].key = `${index}_${_index}`;
                    jsonData[index].children[_index].label = _layer.strLayerId;
                    jsonData[index].children[_index].data = _layer;
                    _index++;
                }
            })
        })
        jsonData.filter((item: objData) => {
            return item.key.includes('_');
        })
        setNodes(jsonData);
    }

    const onSelectedLayers = (ev: TreeSelectionEvent) => {
        let selectedKeys: string[] = Object.keys(ev);
        let selectedData: string[] = [];
        selectedKeys = selectedKeys.filter((item: string) => {
            return item.includes('_');
        })
        for (let index: number = 0; index < selectedKeys.length;) {
            nodes.forEach((node: objData) => {
                node.children.forEach((layer: objChildren) => {
                    if (layer.key == selectedKeys[index]) {
                        selectedData.push(layer.data)
                        index++;
                    }
                })
            });
        }
        setSelectedKeys(ev);
        setSelectedLayers(selectedData);
    }
    const onSelectedLayers2 = (ev: TreeTableSelectionKeysType) => {
        let selectedKeys: string[] = Object.keys(ev);
        let selectedData: string[] = [];
        selectedKeys = selectedKeys.filter((item: string) => {
            return item.includes('_');
        })
        for (let index: number = 0; index < selectedKeys.length;) {
            nodes.forEach((node: objData) => {
                node.children.forEach((layer: objChildren) => {
                    if (layer.key == selectedKeys[index]) {
                        selectedData.push(layer.data)
                        index++;
                    }
                })
            });
        }
        setSelectedKeys(ev);
        setSelectedLayers(selectedData);
    }
    const onOk = () => {
        let l = selectedLayers.map((l: any) => {
            let layerDetails = new LayerDetails(l.strLayerId,
                LayerSourceEnum.MAPCORE,
                { urlServer: props.urlServer },
                generalService.layerPropertiesBase.layerIReadCallback,
                convertStringToEnum(l.strLayerType))
            return layerDetails;
        })
        props.onChooseLayers(l);
        props.setDisplayTreeDialog(false)
    }
    const convertStringToEnum = (str: string) => {
        switch (str) {
            case "MapCoreServerRaster":
                return LayerNameEnum.NativeServerRaster
            case "MapCoreServerDTM":
                return LayerNameEnum.NativeServerDtm
            case "MapCoreServer3DModel":
                return LayerNameEnum.NativeServer3DModel
            case "MapCoreServerVector":
                return LayerNameEnum.NativeServerVector
            case "MapCoreServerVector3DExtrusion":
                return LayerNameEnum.NativeServerVector3DExtrusion
            case "MapCoreServerTraversability":
                return LayerNameEnum.NativeServerTraversability
            case "MapCoreServerMaterial":
                return LayerNameEnum.NativeServerMaterial
        }
    }
    const GetBBStr = (boundingBox: MapCore.SMcBox) => {
        let strBoundingBox = "";
        if (boundingBox != null) {
            strBoundingBox = "((" + boundingBox.MinVertex.x.toFixed(2) + "," + boundingBox.MinVertex.y.toFixed(2)
                + "),(" + boundingBox.MaxVertex.x.toFixed(2) + "," + boundingBox.MaxVertex.y.toFixed(2) + "))";
        }
        return strBoundingBox;
    }
    return (
        <div>
            <div className="card">
                {/* <Tree value={nodes} selectionMode="checkbox" selectionKeys={selectedKeys} onSelectionChange={e => onSelectedLayers(e.value)} /> */}


                <TreeTable value={nodes} selectionMode="checkbox" selectionKeys={selectedKeys} onSelectionChange={e => onSelectedLayers2(e.value)} tableStyle={{ minWidth: '50rem' }}>
                    <Column header="name" body={(RD) => { return <>{typeof RD.data == "string" ? RD.data : RD.data.strLayerId}</> }} selectionMode="multiple" expander></Column>
                    <Column header="Identifier" field="strLayerId"></Column>
                    <Column header="Title" field="strTitle" ></Column>
                    <Column header="Type" field="strLayerType" ></Column>
                    <Column header="Coordinate System" body={(rowData) => {
                        let strCoordinateSystem = "";
                        if (rowData.data?.aTileMatrixSets && rowData.data?.aTileMatrixSets[0]?.strName)
                            strCoordinateSystem = rowData.data.aTileMatrixSets[0]?.strName
                        if (strCoordinateSystem == "" && rowData.data.pCoordinateSystem) {
                            let OgcCrsCode_: { Value?: any } = {};
                            rowData.data.pCoordinateSystem.GetOgcCrsCode(OgcCrsCode_)
                            strCoordinateSystem = OgcCrsCode_.Value.replace("urn:ogc:def:crs:", "").replace("urn:ogc:def:nil:OGC:", "")
                            console.log(strCoordinateSystem);
                        }
                        return <div>{strCoordinateSystem}
                            {/* {rowData.data.aTileMatrixSets ? rowData.data.aTileMatrixSets[0]?.strName : ""} */}
                        </div>
                    }}></Column>
                    <Column header="Draw Priority" field="nDrawPriority"></Column>
                    <Column header="Bounding Box" field="BoundingBox"
                        body={(rowData) => { return <div><label>{rowData.data.BoundingBox ? GetBBStr(rowData.data.BoundingBox) : ""}  </label></div> }}></Column>
                    <Column header="Metadata" field="aMetadataValues" body={(rowData) => {
                        if (typeof rowData.aMetadataValues == "string")
                            return <> {rowData.aMetadataValues}</>
                        else {
                            let str = ""
                            for (let index = 0; index < rowData.aMetadataValues?.length; index++) {
                                str += "(" + rowData.aMetadataValues[index].strKey + "," + rowData.aMetadataValues[index].strValue + "),"
                            }
                            str = str.slice(0, -1);
                            return <div style={{ overflow: 'auto', maxWidth: `${globalSizeFactor * 30}vh`, maxHeight: `${globalSizeFactor * 10}vh` }}>{str}</div>
                        }
                    }}></Column>
                </TreeTable>
                <Footer label='Ok' onOk={onOk} onCancel={() => props.setDisplayTreeDialog(false)}></Footer>
            </div>
        </div>
    )
}

export default TreeSelection;