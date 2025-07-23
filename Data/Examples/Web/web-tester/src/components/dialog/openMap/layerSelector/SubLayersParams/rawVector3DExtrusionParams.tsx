import { ChangeEvent, useEffect, useState } from "react";
import SelectCoordinateSystem from "../../../shared/ControlsForMapcoreObjects/coordinateSystemCtrl/selectCoordinateSystem";
import { Fieldset } from "primereact/fieldset";
import Vector3DFromMap from "../../../treeView/objectWorldTree/shared/Vector3DFromMap";
import { Button } from "primereact/button";
import { Dialog } from "primereact/dialog";
import { getEnumDetailsList, getEnumValueDetails, IndexingData, LayerDetails, LayerParams } from 'mapcore-lib';
import { InputText } from "primereact/inputtext";
import { Checkbox } from "primereact/checkbox";
import ExtrusionTexture from "./extrusionTexture";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { useDispatch, useSelector } from "react-redux";
import { AppState } from "../../../../../redux/combineReducer";
import { setActiveLayerDetailsLayerParams, setActiveLayerDetailsLayerParamsThreeDExtrusion } from "../../../../../redux/LayerParams/layerParamsAction";
import UploadFilesCtrl, { UploadTypeEnum } from "../../../shared/uploadFilesCtrl";
import { RadioButton } from "primereact/radiobutton";
import { setTypeMapWorldDialogSecond } from "../../../../../redux/MapWorldTree/mapWorldTreeActions";
import { hideFormReasons } from "../../../../shared/models/tree-node.model";
import { DialogTypesEnum } from "../../../../../tools/enum/enums";

export default function RawVector3DExtrusionParams() {
    let params: MapCore.IMcRawVector3DExtrusionMapLayer.SParams = useSelector((state: AppState) => state.layerParamsReducer.activeLayerDetails?.layerParams?.threeDExtrusionParams);
    let layerDetails: LayerDetails = useSelector((state: AppState) => state.layerParamsReducer.activeLayerDetails);
    let layerParams: LayerParams = useSelector((state: AppState) => state.layerParamsReducer.activeLayerDetails?.layerParams);
    const [nonDefaultIndex, setNonDefaultIndex] = useState<boolean>(false);
    const [withIndexing, setWithIndexing] = useState<boolean>(layerParams?.indexingData ? true : false);

    const [openDialog, setOpenDialog] = useState("")
    const [editExtrusion, setEditExtrusion] = useState(false)
    const [disabledClipRect, setDisabledClipRect] = useState(false)
    const [extrusionTexture, setExtrusionTexture] = useState(null)
    const [initExtrusion, setInitExtrusion] = useState(new MapCore.IMcRawVector3DExtrusionMapLayer.SExtrusionTexture)
    const dispatch = useDispatch();
    const typeMapWorldDialogSecond: { secondDialogHeader: string, secondDialogComponent: any } = useSelector((state: AppState) => state.mapWorldTreeReducer.typeMapWorldDialogSecond);
    const showDialog: { hideFormReason: hideFormReasons, dialogType: DialogTypesEnum } = useSelector((state: AppState) => state.objectWorldTreeReducer.showDialog);

    return (<div >
        <Fieldset className="form__column-fieldset" legend="Raw Vector 3D Extrusion Map Layer Params">
            <Fieldset className="form__row-fieldset" legend={<> <Checkbox onChange={(e) => {
                setWithIndexing(e.target.checked);
                if (e.target.checked == false)
                    dispatch(setActiveLayerDetailsLayerParams({ ...layerParams, indexingData: null }))
                else
                    dispatch(setActiveLayerDetailsLayerParams({ ...layerParams, indexingData: new IndexingData("...", true) }))
            }} checked={withIndexing}></Checkbox>
                <label className="ml-2">With Indexing </label></>}>
                <div className={`${!withIndexing && "form__disabled"}`}>
                    <div >
                        <RadioButton onChange={e => {
                            setNonDefaultIndex(false);
                            if (e.target.checked == false)
                                dispatch(setActiveLayerDetailsLayerParams({ ...layerParams, indexingData: new IndexingData("...", true) }))
                        }} checked={!nonDefaultIndex}></RadioButton>
                        <label className="ml-2"> Default Index Directory: </label>
                        {!nonDefaultIndex && <UploadFilesCtrl isDirectoryUpload={true} accept='directory' uploadOptions={[UploadTypeEnum.upload]}
                            existLocationPath={layerDetails?.path.substring(0, layerDetails?.path.indexOf('/'))} getVirtualFSPath={(virtualFSPath, selectedOption) => {
                                let lp = { ...layerParams, indexingData: new IndexingData("", true) }
                                dispatch(setActiveLayerDetailsLayerParams(lp))
                            }}></UploadFilesCtrl>}
                    </div>
                    <div >
                        <RadioButton onChange={e => {
                            setNonDefaultIndex(true);
                            if (e.target.checked == false)
                                dispatch(setActiveLayerDetailsLayerParams({ ...layerParams, indexingData: new IndexingData("...", false) }))
                        }} checked={nonDefaultIndex}></RadioButton>
                        <label className="ml-2">Non Default Index Directory: </label>
                        {nonDefaultIndex && <UploadFilesCtrl isDirectoryUpload={true} accept='directory' uploadOptions={[UploadTypeEnum.upload]}
                            getVirtualFSPath={(virtualFSPath, selectedOption) => { dispatch(setActiveLayerDetailsLayerParams({ ...layerParams, indexingData: new IndexingData(virtualFSPath, false) })) }}></UploadFilesCtrl>}
                    </div>
                </div>
            </Fieldset>
            <Fieldset className={`${withIndexing && "form__disabled"}`} legend="Other Params"  >
                <div style={{ display: 'flex' }}>
                    <SelectCoordinateSystem getSelectedCorSys={(coor: MapCore.IMcGridCoordinateSystem) => { dispatch(setActiveLayerDetailsLayerParamsThreeDExtrusion({ ...params, pSourceCoordinateSystem: coor })) }} header="Source Grid Coordinate System" initSelectedCorSys={params?.pSourceCoordinateSystem} ></SelectCoordinateSystem>
                    <SelectCoordinateSystem getSelectedCorSys={(coor: MapCore.IMcGridCoordinateSystem) => { dispatch(setActiveLayerDetailsLayerParamsThreeDExtrusion({ ...params, pTargetCoordinateSystem: coor })) }} header="Target Grid Coordinate System" initSelectedCorSys={params?.pTargetCoordinateSystem} ></SelectCoordinateSystem>
                </div>
                <Fieldset
                    legend={<><Checkbox onChange={(e) => { setDisabledClipRect(e.checked) }} checked={disabledClipRect}></Checkbox><label>Clip Rect (In Target Grig Coordinate System)</label></>}>
                    <div className={`${disabledClipRect && "form__disabled"}`} >
                        <Vector3DFromMap pointCoordSystem={null} flagNull={{ x: false, y: false, z: false }} name="MIN:"
                            initValue={params?.pClipRect?.MinVertex} saveTheValue={(point) => { dispatch(setActiveLayerDetailsLayerParamsThreeDExtrusion({ ...params, pClipRect: { ...params.pClipRect, MinVertex: point } })) }}></Vector3DFromMap>
                        <Vector3DFromMap pointCoordSystem={null} flagNull={{ x: false, y: false, z: false }} name="MAX:"
                            initValue={params?.pClipRect?.MaxVertex} saveTheValue={(point) => { dispatch(setActiveLayerDetailsLayerParamsThreeDExtrusion({ ...params, pClipRect: { ...params.pClipRect, MaxVertex: point } })) }}></Vector3DFromMap>
                    </div>
                </Fieldset>
            </Fieldset>
            <Fieldset legend="Graphical Params" >
                <span style={{ width: "60%" }}>
                    <Button onClick={() => {
                        setOpenDialog("Roof Default Texture");
                        setInitExtrusion(params.RoofDefaultTexture);
                        dispatch(setTypeMapWorldDialogSecond({
                            secondDialogHeader: "Roof Default Texture",
                            secondDialogComponent: <ExtrusionTexture getExtrusionTexture={(ex) => { setExtrusionTexture(ex) }} initExtrusion={initExtrusion}></ExtrusionTexture>
                        }))
                    }}>Roof Default Texture</Button>
                    <Button onClick={() => {
                         setOpenDialog("Side Default Texture"); 
                        setInitExtrusion(params.SideDefaultTexture);
                        dispatch(setTypeMapWorldDialogSecond({
                            secondDialogHeader: "Side Default Texture",
                            secondDialogComponent: <ExtrusionTexture getExtrusionTexture={(ex) => { setExtrusionTexture(ex) }} initExtrusion={initExtrusion}></ExtrusionTexture>
                        }))
                    }}>Side Default Texture</Button>
                </span>
                <div>
                    <DataTable value={params?.aSpecificTextures}>
                        <Column header="Specific Texture" body={(rowData, f) => {
                            return <div onClick={() => {
                                 setOpenDialog("Specific Texture"); 
                                setInitExtrusion(rowData); setEditExtrusion(true)
                                dispatch(setTypeMapWorldDialogSecond({
                                    secondDialogHeader: "Specific Texture",
                                    secondDialogComponent: <ExtrusionTexture getExtrusionTexture={(ex) => { setExtrusionTexture(ex) }} initExtrusion={initExtrusion}></ExtrusionTexture>
                                }))
                            }}>value</div>
                        }}></Column>
                        <Column body={(rowData, f) => {
                            return <div><Button label="Delete"
                                onClick={() => {
                                    let arr = params.aSpecificTextures.filter(u => u != rowData)
                                    dispatch(setActiveLayerDetailsLayerParamsThreeDExtrusion({ ...params, aSpecificTextures: arr }))
                                }}></Button></div>
                        }}></Column>
                    </DataTable>
                    <Button onClick={() => {
                         setOpenDialog("Specific Texture");
                        setInitExtrusion(new MapCore.IMcRawVector3DExtrusionMapLayer.SExtrusionTexture()); setEditExtrusion(false)
                        dispatch(setTypeMapWorldDialogSecond({
                            secondDialogHeader: "Specific Texture",
                            secondDialogComponent: <ExtrusionTexture getExtrusionTexture={(ex) => { setExtrusionTexture(ex) }} initExtrusion={initExtrusion}></ExtrusionTexture>
                        }))
                    }}>Add Row</Button>
                </div>
            </Fieldset>
            <div>
                <div className='form__lower-margin'>
                    <label className="props__input-label">Height Column:</label>
                    {/* <Dropdown name="MouseMoveUsage" value={} onChange={(e)=> setParams({ ...params, strHeightColumn: e.value })} options={} optionLabel="name" /> */}
                    <InputText id='fileNameFileFont' value={params?.strHeightColumn} name='fileNameFileFont' onChange={(e) => {
                        dispatch(setActiveLayerDetailsLayerParamsThreeDExtrusion({ ...params, strHeightColumn: e.target.value }))
                    }} />
                </div>
                <div className='form__lower-margin'>
                    <label className="props__input-label">Object ID Column:</label>
                    {/* <Dropdown name="MouseMoveUsage" value={} onChange={(e)=> setParams({ ...params, strObjectIDColumn: e.value })}  options={} optionLabel="name" /> */}
                </div>
                <div className='form__lower-margin'>
                    <label className="props__input-label">Roof Texture Index Column:</label>
                    {/* <Dropdown name="MouseMoveUsage" value={} onChange={(e)=> setParams({ ...params, strRoofTextureIndexColumn: e.value })}  options={} optionLabel="name" /> */}
                </div>
                <div className='form__lower-margin'>
                    <label className="props__input-label">Side Texture Index Column:</label>
                    {/* <Dropdown name="MouseMoveUsage" value={} onChange={(e)=> setParams({ ...params, strSideTextureIndexColumn: e.value })}  options={} optionLabel="name" /> */}
                </div>
            </div>
        </Fieldset>
        {typeMapWorldDialogSecond && <Dialog
            className={showDialog.hideFormReason !== null ? `object-props__hidden-dialog ` : ""}
            header={typeMapWorldDialogSecond.secondDialogHeader}
            onHide={() => dispatch(setTypeMapWorldDialogSecond(undefined))}
            modal={showDialog.hideFormReason !== null ? false : true}
            footer={<Button style={{ margin: '5px' }}
            onClick={() => {
                switch (openDialog) {
                    case "Roof Default Texture":
                        dispatch(setActiveLayerDetailsLayerParamsThreeDExtrusion({ ...params, RoofDefaultTexture: extrusionTexture }))
                        break;
                    case "Side Default Texture":
                        dispatch(setActiveLayerDetailsLayerParamsThreeDExtrusion({ ...params, SideDefaultTexture: extrusionTexture }))
                        break;
                    case "Specific Texture":
                        if (editExtrusion == true) {
                            let index = params.aSpecificTextures.findIndex(obj => obj == initExtrusion);
                            let ST = params.aSpecificTextures;
                            ST[index] = extrusionTexture;
                            dispatch(setActiveLayerDetailsLayerParamsThreeDExtrusion({ ...params, aSpecificTextures: ST }))
                        }
                        else
                            dispatch(setActiveLayerDetailsLayerParamsThreeDExtrusion({ ...params, aSpecificTextures: [...params.aSpecificTextures, extrusionTexture] }))
                        break;
                }
                setOpenDialog("");dispatch(setTypeMapWorldDialogSecond(undefined))
            }}>  Ok</Button>}
            visible>   {typeMapWorldDialogSecond?.secondDialogComponent}</Dialog>}

    </div >
    )
}