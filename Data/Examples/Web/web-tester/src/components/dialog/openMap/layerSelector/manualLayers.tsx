import React, { useRef, useState, forwardRef, useImperativeHandle, useEffect, ChangeEvent } from 'react';
import { Button } from 'primereact/button';
import { Menu } from 'primereact/menu';
import { MenuItem } from 'primereact/menuitem';
import { enums, IndexingData, LayerNameEnum } from 'mapcore-lib';
import { LayerDetails, LayerSourceEnum, LayerParams } from 'mapcore-lib';
import { InputText } from 'primereact/inputtext';
import { runCodeSafely } from '../../../../common/services/error-handling/errorHandler';
import { ContextMenu } from 'primereact/contextmenu';
import LayersFromServerComp from './layersFromServerComp';
import LayersParams, { ParametersMode } from './layersParams';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrash } from '@fortawesome/free-solid-svg-icons'
import { useDispatch, useSelector } from 'react-redux';
import { AppState } from '../../../../redux/combineReducer';
import { setActiveLayerDetails, setActiveLayerDetailsLayerParams } from '../../../../redux/LayerParams/layerParamsAction'
import UploadFilesCtrl, { UploadTypeEnum } from '../../shared/uploadFilesCtrl';
import { Dialog } from 'primereact/dialog';
import DataSourceSubLayers from './dataSourceSubLayers';
import generalService from '../../../../services/general.service';
import objectWorldTreeService from '../../../../services/objectWorldTree.service';

const ManualLayers = forwardRef((props: {
    returnLayers: (SL: LayerDetails[]) => void, requestParams: MapCore.SMcKeyStringValue[],
    initArrLayers?: LayerDetails[]
    body: MapCore.SMcKeyStringValue, onlyDtm: boolean,
    parametersMode: ParametersMode
}, ref) => {


    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    const [activeLayer, setActiveLayer] = useState<LayerDetails>(null);
    const [openDialog, setOpenDialog] = useState(false);
    const [arrLayers, setArrLayers] = useState<any[]>([])
    const menu = useRef<Menu>(null);
    const layerParams: LayerParams = useSelector((state: AppState) => state.layerParamsReducer.activeLayerDetails?.layerParams);
    const dispatch = useDispatch();

    useEffect(() => {
        let arr: any[] = [];
        props.initArrLayers?.forEach((item: LayerDetails) => {
            if (item.layerSource == enums.LayerSourceEnum.Native || item.layerSource == enums.LayerSourceEnum.Raw) {
                arr.push(item)
            }
            else {
                let MapCoreItem = arr.find(i => i.layerSource == item.layerSource)
                if (MapCoreItem) {
                    MapCoreItem.LayersFromServer.push(item)
                }
                else {
                    let r = {
                        LayersFromServer: [item]
                        , layerIReadCallback: item.layerIReadCallback
                        , layerParams: item.layerParams
                        , layerSource: item.layerSource
                        , name: item.layerSource
                        , path: ""
                    }
                    arr.push(r)
                }
            }

        })
        setArrLayers(arr)
    }, [])

    const addInput = (layername: any, layerSource: enums.LayerSourceEnum) => {
        let add: LayerDetails = new LayerDetails("",
            layerSource,
            new LayerParams(),
            generalService.layerPropertiesBase.layerIReadCallback,
            layername);
        setArrLayers([...arrLayers, add]);
        setActiveLayer(add)
    };

    useEffect(() => {
        let AL = [...arrLayers];
        for (let index = 0; index < arrLayers.length; index++) {
            if (arrLayers[index] == activeLayer) {
                AL[index].layerParams = layerParams;
                setArrLayers(AL)
            }
        }
    }, [layerParams])

    useEffect(() => {
        dispatch(setActiveLayerDetailsLayerParams(activeLayer?.layerParams))
        dispatch(setActiveLayerDetails(activeLayer))
    }, [activeLayer, arrLayers])

    useImperativeHandle(ref, () => ({
        getLayersArray: () => {
            let LayerMapCore: LayerDetails[] = [];
            arrLayers.forEach((item) => {
                if (LayerSourceEnum[item.name]) {
                    item.LayersFromServer?.forEach((layer) => { LayerMapCore.push(layer) })
                }
                else {
                    if ((item.path) || (item.layerParams.rawParams?.aComponents[0]?.strName)) {
                        LayerMapCore.push(item)
                    }
                }
            })
            return LayerMapCore;
        }
    }));

    const onLocalLayerNameChanged = (value: string, layerIndex: number, filePathesObjs?: { filePath: any; label: any; }[]) => {
        // setActiveLayer({ ...activeLayer, filePathesObjs: filePathesObjs })
        runCodeSafely(() => {
            const newArr = [...arrLayers];
            newArr[layerIndex].path = value;
            setArrLayers(newArr);
        }, "ManualLayers.showFile")
    }

    const showFile = async (e: any) => {
        runCodeSafely(() => {
            const reader = new FileReader();
            reader.onload = e => {
                const results: any = e.target?.result;
                let arr = results.split('\r\n');
                arr.pop();
                let array = [];
                arr.forEach((item: any) => {
                    let add = new LayerDetails(item,
                        enums.LayerSourceEnum.Native,
                        null,
                        generalService.layerPropertiesBase.layerIReadCallback,
                        enums.LayerNameEnum.NativeVector);
                    array.push(add);
                });
                setArrLayers([...arrLayers, ...array])
            };
            reader.readAsText(e.target.files[0]);
        }, "ManualLayers.showFile")
    }

    const items: MenuItem[] = [
        {
            label: 'Native Layers',
            items: [
                {
                    label: enums.LayerNameEnum.NativeRaster,
                    command: () => {
                        addInput(enums.LayerNameEnum.NativeRaster, enums.LayerSourceEnum.Native)
                    }
                },
                {
                    label: enums.LayerNameEnum.NativeDtm,
                    command: () => {
                        addInput(enums.LayerNameEnum.NativeDtm, enums.LayerSourceEnum.Native)
                    }
                },
                {
                    label: enums.LayerNameEnum.NativeVector,
                    command: () => {
                        addInput(enums.LayerNameEnum.NativeVector, enums.LayerSourceEnum.Native)
                    }
                },
                {
                    label: enums.LayerNameEnum.NativeVectorListFile,
                    command: () => {
                        addInput(enums.LayerNameEnum.NativeVectorListFile, enums.LayerSourceEnum.Native)
                    }
                },
                {
                    label: enums.LayerNameEnum.NativeVector3DExtrrusion,
                    command: () => {
                        addInput(enums.LayerNameEnum.NativeVector3DExtrrusion, enums.LayerSourceEnum.Native)
                    }
                },
                {
                    label: enums.LayerNameEnum.Native3DModel,
                    command: () => {
                        addInput(enums.LayerNameEnum.Native3DModel, enums.LayerSourceEnum.Native)
                    }
                },
                {
                    label: enums.LayerNameEnum.NativeMaterial,
                    command: () => {
                        addInput(enums.LayerNameEnum.NativeMaterial, enums.LayerSourceEnum.Native)
                    }
                },
                {
                    label: enums.LayerNameEnum.NativeTraversability,
                    command: () => {
                        addInput(enums.LayerNameEnum.NativeTraversability, enums.LayerSourceEnum.Native)
                    }
                },
                {
                    label: enums.LayerNameEnum.NativeHeat,
                    command: () => {
                        addInput(enums.LayerNameEnum.NativeHeat, enums.LayerSourceEnum.Native)
                    }
                }
            ]
        },
        {
            label: 'Native Server Layers',
            items: [
                {
                    label: enums.LayerNameEnum.NativeServerRaster,
                    command: () => {
                        addInput(enums.LayerNameEnum.NativeServerRaster, enums.LayerSourceEnum.MAPCORE)
                    }
                },
                {
                    label: enums.LayerNameEnum.NativeServerDtm,
                    command: () => {
                        addInput(enums.LayerNameEnum.NativeServerDtm, enums.LayerSourceEnum.MAPCORE)
                    }
                },
                {
                    label: enums.LayerNameEnum.NativeServerVector,
                    command: () => {
                        addInput(enums.LayerNameEnum.NativeServerVector, enums.LayerSourceEnum.MAPCORE)
                    }
                },
                {
                    label: enums.LayerNameEnum.NativeServerVector3DExtrusion,
                    command: () => {
                        addInput(enums.LayerNameEnum.NativeServerVector3DExtrusion, enums.LayerSourceEnum.MAPCORE)
                    }
                },
                {
                    label: enums.LayerNameEnum.NativeServer3DModel,
                    command: () => {
                        addInput(enums.LayerNameEnum.NativeServer3DModel, enums.LayerSourceEnum.MAPCORE)
                    }
                },
                {
                    label: enums.LayerNameEnum.NativeServerTraversability,
                    command: () => {
                        addInput(enums.LayerNameEnum.NativeServerTraversability, enums.LayerSourceEnum.MAPCORE)
                    }
                }
            ]
        },
        {
            label: 'Raw Layers',
            items: [
                {
                    label: enums.LayerNameEnum.RawRaster,
                    command: () => {
                        addInput(enums.LayerNameEnum.RawRaster, enums.LayerSourceEnum.Raw)
                    }
                },
                {
                    label: enums.LayerNameEnum.RawDtm,
                    command: () => {
                        addInput(enums.LayerNameEnum.RawDtm, enums.LayerSourceEnum.Raw)
                    }
                },
                {
                    label: enums.LayerNameEnum.RawVector,
                    command: () => {
                        addInput(enums.LayerNameEnum.RawVector, enums.LayerSourceEnum.Raw)
                    }
                },
                {
                    label: enums.LayerNameEnum.RawVector3DExtrusion,
                    command: () => {
                        addInput(enums.LayerNameEnum.RawVector3DExtrusion, enums.LayerSourceEnum.Raw)
                    }
                },
                {
                    label: enums.LayerNameEnum.Raw3DModel,
                    command: () => {
                        addInput(enums.LayerNameEnum.Raw3DModel, enums.LayerSourceEnum.Raw)
                    }
                },
                {
                    label: enums.LayerNameEnum.RawTraversability,
                    command: () => {
                        addInput(enums.LayerNameEnum.RawTraversability, enums.LayerSourceEnum.Raw)
                    }
                },
                {
                    label: enums.LayerNameEnum.RawMaterial,
                    command: () => {
                        addInput(enums.LayerNameEnum.RawMaterial, enums.LayerSourceEnum.Raw)
                    }
                }]
        }, {
            label: 'Server Layers',
            items: [
                {
                    label: enums.LayerSourceEnum.MAPCORE.toString(),
                    command: () => {
                        addInput(enums.LayerSourceEnum.MAPCORE, enums.LayerSourceEnum.MAPCORE)
                    }
                },
                {
                    label: enums.LayerSourceEnum.WMTS.toString(),
                    command: () => {
                        addInput(enums.LayerSourceEnum.WMTS, enums.LayerSourceEnum.WMTS)
                    }
                },
                {
                    label: enums.LayerSourceEnum.WCS.toString(),
                    command: () => {
                        addInput(enums.LayerSourceEnum.WCS, enums.LayerSourceEnum.WCS)
                    }
                },
                {
                    label: enums.LayerSourceEnum.WMS.toString(),
                    command: () => {
                        addInput(enums.LayerSourceEnum.WMS, enums.LayerSourceEnum.WMS)
                    }
                },
                {
                    label: enums.LayerSourceEnum.CSW.toString(),
                    command: () => {
                        addInput(enums.LayerSourceEnum.CSW, enums.LayerSourceEnum.CSW)
                    }
                },

            ]
        }, {
            label: 'Web Service',
            items: [
                {
                    label: enums.LayerNameEnum.WebServiceRaster,
                    command: () => {
                        addInput(enums.LayerNameEnum.WebServiceRaster, enums.LayerSourceEnum.WMTS)
                    }
                },
                {
                    label: enums.LayerNameEnum.WebServiceDTM,
                    command: () => {
                        addInput(enums.LayerNameEnum.WebServiceDTM, enums.LayerSourceEnum.WMTS)
                    }
                }
            ]
        },
    ];
    const itemsOnlyDtms: MenuItem[] = [
        {
            label: enums.LayerNameEnum.NativeDtm,
            command: () => {
                addInput(enums.LayerNameEnum.NativeDtm, enums.LayerSourceEnum.Native)
            }
        },
        {
            label: enums.LayerNameEnum.NativeServerDtm,
            command: () => {
                addInput(enums.LayerNameEnum.NativeServerDtm, enums.LayerSourceEnum.MAPCORE)
            }
        },
        {
            label: enums.LayerNameEnum.RawDtm,
            command: () => {
                addInput(enums.LayerNameEnum.RawDtm, enums.LayerSourceEnum.Raw)
            }
        },
        {
            label: 'Server Layers',
            items: [
                {
                    label: enums.LayerSourceEnum.MAPCORE.toString(),
                    command: () => {
                        addInput(enums.LayerSourceEnum.MAPCORE, enums.LayerSourceEnum.MAPCORE)
                    }
                },
                {
                    label: enums.LayerSourceEnum.WMTS.toString(),
                    command: () => {
                        addInput(enums.LayerSourceEnum.WMTS, enums.LayerSourceEnum.WMTS)
                    }
                },
                {
                    label: enums.LayerSourceEnum.WCS.toString(),
                    command: () => {
                        addInput(enums.LayerSourceEnum.WCS, enums.LayerSourceEnum.WCS)
                    }
                },
                {
                    label: enums.LayerSourceEnum.WMS.toString(),
                    command: () => {
                        addInput(enums.LayerSourceEnum.WMS, enums.LayerSourceEnum.WMS)
                    }
                },
                {
                    label: enums.LayerSourceEnum.CSW.toString(),
                    command: () => {
                        addInput(enums.LayerSourceEnum.CSW, enums.LayerSourceEnum.CSW)
                    }
                },

            ]
        },
    ];
    const getParamsborderSize = () => {
        if (props.parametersMode == ParametersMode.AllParameters)
            return 46
        else return 20
    }
    return (
        <div style={{ display: 'flex' }}>
            <div>
                <ContextMenu model={props.onlyDtm ? itemsOnlyDtms : items} ref={menu} />
                <Button style={{ marginBottom: '1% !important' }} className='layout__button-add-layer' label="Add A Manual Layer" onClick={(e) => menu.current.show(e)} />
                <div className='layout__display-selected-layers' >
                    {arrLayers && arrLayers.map((item, i) => {
                        return <div key={i} className={`manual-layers__place-input ${item == activeLayer && "activeLayer"}`} onClick={() => { setActiveLayer(item) }}>
                            {(LayerSourceEnum[item.name]) ? <LayersFromServerComp key={i} onlyDtm={props.onlyDtm} returnLayers={(SL: LayerDetails[]) => { item.LayersFromServer = SL }} typeServer={item.name} requestParams={props.requestParams} body={props.body}></LayersFromServerComp>
                                :
                                (item.name.includes("Raw")) ?
                                    <div className='form__row-container'>
                                        <label>{item.name}</label>
                                        <UploadFilesCtrl isDirectoryUpload={item.name == "Raw Vector 3D Extrusion" ? false : true} accept={item.name == "Raw Vector 3D Extrusion" ? "" : "directory"}
                                            uploadOptions={item.name == "Raw 3D Model" ? [UploadTypeEnum.upload, UploadTypeEnum.url] : [UploadTypeEnum.upload]}
                                            getVirtualFSPath={(virtualFSPath, selectedOption) => {
                                                let filePaths = objectWorldTreeService.getFilePathesFromVirtualDirectory(virtualFSPath);
                                                let filePathesObjs = filePaths.map((path) => { return { filePath: path, label: path?.split('/').slice(1).join('/') } })

                                                onLocalLayerNameChanged(virtualFSPath, i, filePathesObjs);
                                                if (item.name == "Raw Vector 3D Extrusion")
                                                    setOpenDialog(true)
                                            }}  ></UploadFilesCtrl>
                                    </div>
                                    : <div className='form__row-container'>
                                        <label>{item.name}</label>
                                        {item.name == enums.LayerNameEnum.NativeVectorListFile ? <InputText type='file' onChange={showFile} />
                                            : <UploadFilesCtrl
                                                isDirectoryUpload={true}
                                                accept='directory'
                                                uploadOptions={item.name.includes("Server") || item.name.includes("Service") ? [UploadTypeEnum.url] : [UploadTypeEnum.upload, UploadTypeEnum.url]}
                                                existInputValue={!(item.name.includes("Server") || item.name.includes("Service")) && arrLayers[i].path}
                                                getVirtualFSPath={(virtualFSPath, selectedOption) => {
                                                    onLocalLayerNameChanged(virtualFSPath, i)
                                                }} />
                                        }
                                    </div>}
                            <FontAwesomeIcon size='xs' style={{ color: '#6366F1' }} icon={faTrash} onClick={(e) => {
                                setArrLayers(arrLayers.filter(d => d != item)); setActiveLayer(null); e.stopPropagation()
                            }} />
                        </div>
                    })}
                </div>
            </div>
            <div>
                <h1>Params:</h1>
                <div className='layout__display-selected-layers' style={{ height: `${globalSizeFactor * getParamsborderSize()}vh`, width: `${globalSizeFactor * 50}vh`, overflow: 'auto' }} >
                    {activeLayer && <LayersParams rawType={activeLayer ? activeLayer.name : null} layerDetails={activeLayer} parametersMode={props.parametersMode} />}
                </div>
            </div>
            <Dialog visible={openDialog} onHide={() => setOpenDialog(false)}>
                <DataSourceSubLayers></DataSourceSubLayers>
            </Dialog>
        </div >
    )
})
export default ManualLayers;