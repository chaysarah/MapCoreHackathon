// External libraries
import { useState, useEffect } from 'react';
import { useSelector } from 'react-redux';

// UI/component libraries
import { Button } from "primereact/button";
import { Dropdown } from "primereact/dropdown";
import { ListBox } from "primereact/listbox";
import { Fieldset } from "primereact/fieldset";
import { InputNumber } from "primereact/inputnumber";
import { Checkbox } from "primereact/checkbox";

// Project-specific imports
import { enums, getEnumDetailsList, getEnumValueDetails, LayerDetails } from 'mapcore-lib';
import { ParametersMode } from "../layersParams";
import { AppState } from "../../../../../redux/combineReducer";
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";
import UploadFilesCtrl, { UploadTypeEnum } from "../../../shared/uploadFilesCtrl";
import Vector2DFromMap from "../../../treeView/objectWorldTree/shared/Vector2DFromMap";
import Vector3DFromMap from "../../../treeView/objectWorldTree/shared/Vector3DFromMap";
import SelectCoordinateSystem from "../../../shared/ControlsForMapcoreObjects/coordinateSystemCtrl/selectCoordinateSystem";

export default function RawParams(props: {
    rawParams: MapCore.IMcMapLayer.SRawParams,
    imageCoordinateSystem: boolean,
    getRawParams: (rawParams: MapCore.IMcMapLayer.SRawParams, imageCoordinateSystem: boolean) => void,
    rawType: enums.LayerNameEnum, parametersMode: ParametersMode
}) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const layerDetails: LayerDetails = useSelector((state: AppState) => state.layerParamsReducer.activeLayerDetails);
    const [rawParams, setRawParams] = useState<MapCore.IMcMapLayer.SRawParams>(props.rawParams);
    const [imageCoordinateSystem, setImageCoordinateSystem] = useState<boolean>(props.imageCoordinateSystem);
    const [dataSource, setDataSource] = useState();
    const [enumDetails] = useState({ EComponentType: getEnumDetailsList(MapCore.IMcMapLayer.EComponentType), });
    const [componentType, setComponentType] = useState<MapCore.IMcMapLayer.EComponentType>(MapCore.IMcMapLayer.EComponentType.ECT_FILE);
    const [componentStrPath, setComponentStrPath] = useState<string>("");
    const [componentWorldLimit, setComponentWorldLimit] = useState<MapCore.SMcBox>(new MapCore.SMcBox());
    const [componentList, setComponentList] = useState<MapCore.IMcMapLayer.SComponentParams[]>([]);

    //#region UseEffects
    useEffect(() => {
        if (rawParams) {
            if (props.parametersMode == ParametersMode.MinimumParameters) {
                let CompparamsList: MapCore.IMcMapLayer.SComponentParams[] = []
                let componentparams = new MapCore.IMcMapLayer.SComponentParams();
                componentparams.eType = MapCore.IMcMapLayer.EComponentType.ECT_DIRECTORY;
                componentparams.strName = "";
                componentparams.WorldLimit.MinVertex = rawParams?.MaxWorldLimit?.MinVertex;
                componentparams.WorldLimit.MaxVertex = rawParams?.MaxWorldLimit?.MaxVertex;
                CompparamsList.push(componentparams);
                rawParams.aComponents = CompparamsList
            }
            layerDetails.layerParams.rawParams.pReadCallback = layerDetails.layerIReadCallback;
        }
        props.getRawParams(rawParams, imageCoordinateSystem);
    }, [rawParams, imageCoordinateSystem])
    useEffect(() => {
        setRawParams(props.rawParams);
    }, [props.rawParams])
    useEffect(() => {
        setImageCoordinateSystem(props.imageCoordinateSystem);
    }, [props.imageCoordinateSystem])
    useEffect(() => {
        setRawParams({ ...rawParams, aComponents: componentList })
    }, [componentList])
    //#endregion

    const saveData = (event: any) => {
        runCodeSafely(() => {
            setRawParams({ ...rawParams, [event.target.name]: event.target.type === "checkbox" ? event.target.checked : event.target.value })
        }, "objectItemsSelectList.saveData => onChange")
    }

    function cutString(inputString) {
        const lastSlashIndex = inputString.lastIndexOf("/");
        if (lastSlashIndex === -1) {
            return "";
        } else {
            return inputString.substring(lastSlashIndex + 1);
        }
    }

    return (<div >
        {props.parametersMode == ParametersMode.AllParameters && <SelectCoordinateSystem getSelectedCorSys={(coor: MapCore.IMcGridCoordinateSystem) =>
        // { dispatch(setActiveLayerDetailsLayerParams({ ...layerParams, targetCoordSys: coor })) }}
        { setRawParams({ ...rawParams, pCoordinateSystem: coor }) }}
            header="Target Coordinate System" initSelectedCorSys={rawParams?.pCoordinateSystem}></SelectCoordinateSystem>}
        {props.parametersMode == ParametersMode.AllParameters && <Fieldset className="form__space-around form__column-fieldset" legend="Add Component">
            <div>
                <label >Component Type: </label>
                <Dropdown style={{ width: `${globalSizeFactor * 1.5 * 9.5}vh` }} name='versionCompatibility'
                    value={getEnumValueDetails(componentType, enumDetails.EComponentType)} optionLabel='name'
                    onChange={(e) => { setComponentType(e.target.value.theEnum) }} options={enumDetails.EComponentType} />
            </div>
            <div>
                <UploadFilesCtrl isDirectoryUpload={componentType == MapCore.IMcMapLayer.EComponentType.ECT_DIRECTORY ? true : false} uploadOptions={[UploadTypeEnum.upload]}
                    getVirtualFSPath={(virtualFSPath, selectedOption) => { setComponentStrPath(virtualFSPath) }}
                    existLocationPath={layerDetails?.path.substring(0, layerDetails?.path.indexOf('/'))}></UploadFilesCtrl>
                {/* <span style={{ width: '60%' }} className="form__flex-and-row-between form__items-center">
                    <label htmlFor='dataSource'>File Name </label>
                    <Dropdown style={{ width: '58%' }} id="dataSource" name='dataSource' value={dataSource} onChange={(event) => { setDataSource(event.target.value); setComponentStrPath(event.target.value.filePath); }} options={layerDetails?.filePathesObjs} optionLabel="label" />
                </span> */}
            </div>
            <Fieldset legend={"World Limit:"}>
                <Fieldset legend="Max World Limit:" className='form__column-fieldset '>
                    <Vector2DFromMap pointCoordSystem={null} flagNull={{ x: false, y: false }} name="Min Point:" disabledPointFromMap={false}
                        initValue={componentWorldLimit.MinVertex} saveTheValue={(point) => { setComponentWorldLimit({ ...componentWorldLimit, MinVertex: new MapCore.SMcVector3D(point.x, point.y, 0) }) }}></Vector2DFromMap>
                    <Vector2DFromMap pointCoordSystem={null} flagNull={{ x: false, y: false }} name="Max Point:" disabledPointFromMap={false}
                        initValue={componentWorldLimit.MaxVertex} saveTheValue={(point) => { setComponentWorldLimit({ ...componentWorldLimit, MaxVertex: new MapCore.SMcVector3D(point.x, point.y, 0) }) }}></Vector2DFromMap>
                </Fieldset>
            </Fieldset>
            <div>
                <ListBox options={rawParams?.aComponents} optionLabel={"strName"} itemTemplate={(item) => { return <>String Path:{item.strName}</> }}></ListBox>
                <Button onClick={() => {
                    let newComponent = new MapCore.IMcMapLayer.SComponentParams();
                    newComponent.eType = componentType;
                    newComponent.strName = componentStrPath;
                    if (layerDetails.path != "")
                        newComponent.strName = cutString(componentStrPath);
                    newComponent.WorldLimit = componentWorldLimit;
                    setComponentList([...componentList, newComponent]);
                }}>Add To List</Button>
                <Button onClick={() => { setComponentList([]) }}>Clear</Button>
            </div>
        </Fieldset>}
        <div className='object-props__flex-and-row-between'>
            <label>First Pyramid Resolution:</label>
            <InputNumber value={rawParams?.fFirstPyramidResolution ?? null} name="fFirstPyramidResolution" onValueChange={saveData} mode="decimal" />
        </div>
        <div className='object-props__flex-and-row-between'>
            <label>Max Num Open Files:</label>
            <InputNumber value={rawParams?.uMaxNumOpenFiles ?? null} name="uMaxNumOpenFiles" onValueChange={saveData} mode="decimal" />
        </div>
        {/* ?????? */}
        {/* <div >
            <label>Pyramid Resolution (separated by whitespace):</label>
            <InputNumber value={rawParams?.auPyramidResolutions} name="auPyramidResolutions" onValueChange={saveData} mode="decimal" />
        </div> */}
        <div className='object-props__flex-and-row-between'>
            <label>Highest Resolution:</label>
            <InputNumber value={rawParams?.fHighestResolution ?? null} name="fHighestResolution" onValueChange={saveData} mode="decimal" />
        </div>
        <div >
            <Checkbox onChange={saveData} name="bIgnoreRasterPalette" checked={rawParams?.bIgnoreRasterPalette}></Checkbox>
            <label className="ml-2">Ignore Raster Palette</label>
        </div>
        {props.rawType == enums.LayerNameEnum.RawRaster && <div >
            <Checkbox onChange={e => setImageCoordinateSystem(e.checked)} checked={imageCoordinateSystem}></Checkbox>
            <label className="ml-2">Image Coordinate System</label>
        </div>}
        <Fieldset legend="Max World Limit:" className='form__column-fieldset '>
            <Vector3DFromMap pointCoordSystem={null} flagNull={{ x: false, y: false, z: false }} name="Min Point:" disabledPointFromMap={false}
                initValue={rawParams?.MaxWorldLimit?.MinVertex} saveTheValue={(point) => { setRawParams({ ...rawParams, MaxWorldLimit: { ...rawParams.MaxWorldLimit, MinVertex: point } }) }}></Vector3DFromMap>
            <Vector3DFromMap pointCoordSystem={null} flagNull={{ x: false, y: false, z: false }} name="Max Point:" disabledPointFromMap={false}
                initValue={rawParams?.MaxWorldLimit?.MaxVertex} saveTheValue={(point) => { setRawParams({ ...rawParams, MaxWorldLimit: { ...rawParams.MaxWorldLimit, MaxVertex: point } }) }}></Vector3DFromMap>
        </Fieldset>
    </div>
    )
}