import { useState } from "react";
import { InputNumber } from "primereact/inputnumber";
import { useSelector } from "react-redux";
import { Dropdown } from "primereact/dropdown";
import { Checkbox } from "primereact/checkbox";
import { Button } from "primereact/button";

import { getEnumDetailsList, getEnumValueDetails, MapCoreData, MapCoreService } from 'mapcore-lib';
import './styles/openMapManually.css';
import UploadFilesCtrl, { UploadTypeEnum } from "../../../shared/uploadFilesCtrl";
import { AppState } from "../../../../../redux/combineReducer";
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";
import generalService from "../../../../../services/general.service";

export default function CreateDevice() {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [deviceInitParams, setDeviceInitParams] = useState(generalService.mapCorePropertiesBase.deviceInitParams);
    const [existsDevice, setExistsDevice] = useState(MapCoreData.device ? true : false);

    const saveDeviceData = (event: any) => {
        runCodeSafely(() => {
            let value = event.target.type === "checkbox" ? event.target.checked : event.target.value;
            let class_name: string = event.originalEvent?.currentTarget?.className;
            if (class_name?.includes("p-dropdown-item")) {
                value = event.target.value.theEnum;
            }
            setDeviceInitParams({ ...deviceInitParams, [event.target.name]: value })
            generalService.mapCorePropertiesBase.deviceInitParams = { ...deviceInitParams, [event.target.name]: value }
        }, "EditModePropertiesDialog/General.saveData => onChange")
    }
    const [enumDetails] = useState({
        ERenderingSystem: getEnumDetailsList(MapCore.IMcMapDevice.ERenderingSystem),
        ETerrainObjectsQuality: getEnumDetailsList(MapCore.IMcMapDevice.ETerrainObjectsQuality),
        ELoggingLevel: getEnumDetailsList(MapCore.IMcMapDevice.ELoggingLevel),
        AntiAliasingLevel: getEnumDetailsList(MapCore.IMcMapDevice.EAntiAliasingLevel),
    });

    return (
        <div className={`open-map-manually__device-container ${existsDevice && "form__disabled"}`}>
            <div className='object-props__flex-and-row-between'>
                <label>Logging Level: </label>
                <Dropdown className="object-props__dropdown" name="eLoggingLevel" value={getEnumValueDetails(deviceInitParams.eLoggingLevel, enumDetails.ELoggingLevel)} onChange={saveDeviceData} options={enumDetails.ELoggingLevel} optionLabel="name" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label>Terrain Objects Quality: </label>
                <Dropdown className="object-props__dropdown" name="eTerrainObjectsQuality" value={getEnumValueDetails(deviceInitParams.eTerrainObjectsQuality, enumDetails.ETerrainObjectsQuality)} onChange={saveDeviceData} options={enumDetails.ETerrainObjectsQuality} optionLabel="name" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label>Rendering System: </label>
                <Dropdown className="object-props__dropdown" name="eRenderingSystem" value={getEnumValueDetails(deviceInitParams.eRenderingSystem, enumDetails.ERenderingSystem)} onChange={saveDeviceData} options={enumDetails.ERenderingSystem} optionLabel="name" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label>Log Dir:</label>
                <UploadFilesCtrl isDirectoryUpload={true} accept={"directory"} uploadOptions={[UploadTypeEnum.upload]}
                    getVirtualFSPath={(virtualFSPath, selectedOption) => {
                        generalService.mapCorePropertiesBase.deviceInitParams = { ...deviceInitParams, strPrefixForPathsInResourceFile: virtualFSPath }
                    }}  ></UploadFilesCtrl>
            </div>
            <div className='object-props__flex-and-row-between'>
                <label>config Dir:</label>
                <UploadFilesCtrl isDirectoryUpload={true} accept={"directory"} uploadOptions={[UploadTypeEnum.upload]}
                    getVirtualFSPath={(virtualFSPath, selectedOption) => {
                        generalService.mapCorePropertiesBase.deviceInitParams = { ...deviceInitParams, strConfigFilesDirectory: virtualFSPath }
                    }}  ></UploadFilesCtrl>
            </div>
            <div className='object-props__flex-and-row-between'>
                <label className="ml-2">Viewport Anti Aliasing Level:</label>
                <Dropdown className="object-props__dropdown" name='eViewportAntiAliasingLevel' value={getEnumValueDetails(deviceInitParams.eViewportAntiAliasingLevel, enumDetails.AntiAliasingLevel)} onChange={saveDeviceData} options={enumDetails.AntiAliasingLevel} optionLabel="name" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label className="ml-2">Terrain Objects Anti Aliasing Level: </label>
                <Dropdown className="object-props__dropdown" name='eTerrainObjectsAntiAliasingLevel' value={getEnumValueDetails(deviceInitParams.eTerrainObjectsAntiAliasingLevel, enumDetails.AntiAliasingLevel)} onChange={saveDeviceData} options={enumDetails.AntiAliasingLevel} optionLabel="name" />
            </div>
            <div>
                <Checkbox onChange={saveDeviceData} name="bObjectsTexturesAtlas16bit" checked={deviceInitParams.bObjectsTexturesAtlas16bit}></Checkbox>
                <label className="ml-2">Objects Textures Atlas 16 bit</label>
            </div>
            <div>
                <Checkbox onChange={saveDeviceData} name="bDisableDepthBuffer" checked={deviceInitParams.bDisableDepthBuffer}></Checkbox>
                <label className="ml-2">Disable Depth Buffer</label>
            </div>
            <div>
                <Checkbox onChange={saveDeviceData} name="bIgnoreRasterLayerMipmaps" checked={deviceInitParams.bIgnoreRasterLayerMipmaps}></Checkbox>
                <label className="ml-2">Ignore Raster Layer Mipmaps</label>
            </div>
            <div>
                <Checkbox onChange={saveDeviceData} name="bEnableObjectsBatchEnlarging" checked={deviceInitParams.bEnableObjectsBatchEnlarging}></Checkbox>
                <label className="ml-2">Enable Objects Batch Enlarging</label>
            </div>
            <div>
                <Checkbox onChange={saveDeviceData} name="bAlignScreenSizeObjects" checked={deviceInitParams.bAlignScreenSizeObjects}></Checkbox>
                <label className="ml-2">Align Screen Size Objects</label>
            </div>
            <div >
                <Checkbox onChange={saveDeviceData} name="bPreferUseTerrainTileRenderTargets" checked={deviceInitParams.bPreferUseTerrainTileRenderTargets}></Checkbox>
                <label className="ml-2">Prefer Use Terrain Tile Render Targets</label>
            </div>
            <div className='object-props__flex-and-row-between'>
                <label style={{ padding: `${globalSizeFactor * 0.15}vh` }} htmlFor='Max'>Background Threads:</label>
                <InputNumber className="form__narrow-input" id='Max' value={deviceInitParams.uNumBackgroundThreads} name='uNumBackgroundThreads' onValueChange={saveDeviceData} />
            </div>
            <div className='object-props__flex-and-row-between' >
                <label style={{ padding: `${globalSizeFactor * 0.15}vh` }} htmlFor='Max'>BNum Terrain Tile Render Targets:</label>
                <InputNumber className="form__narrow-input" id='Max' value={deviceInitParams.uNumTerrainTileRenderTargets} name='uNumTerrainTileRenderTargets' onValueChange={saveDeviceData} />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label style={{ padding: `${globalSizeFactor * 0.15}vh` }} htmlFor='Max'>Objects Batch Growth Ratio:</label>
                <InputNumber className="form__narrow-input" id='Max' value={deviceInitParams.fObjectsBatchGrowthRatio} name='fObjectsBatchGrowthRatio' onValueChange={saveDeviceData} />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label style={{ padding: `${globalSizeFactor * 0.15}vh` }} htmlFor='Max'>Objects Textures Atlas Size:</label>
                <InputNumber className="form__narrow-input" id='Max' value={deviceInitParams.uObjectsTexturesAtlasSize} name='uObjectsTexturesAtlasSize' onValueChange={saveDeviceData} />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label style={{ padding: `${globalSizeFactor * 0.15}vh` }} htmlFor='Max'>Objects Batch Initial Num Vertices:</label>
                <InputNumber className="form__narrow-input" id='Max' value={deviceInitParams.uObjectsBatchInitialNumVertices} name='uObjectsBatchInitialNumVertices' onValueChange={saveDeviceData} />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label style={{ padding: `${globalSizeFactor * 0.15}vh` }} htmlFor='Max'>Objects Batch Initial Num Indices:</label>
                <InputNumber className="form__narrow-input" id='Max' value={deviceInitParams.uObjectsBatchInitialNumIndices} name='uObjectsBatchInitialNumIndices' onValueChange={saveDeviceData} />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label style={{ padding: `${globalSizeFactor * 0.15}vh` }} htmlFor='Max'>Dtm Visualization Precision(0, 1 or 2):</label>
                <InputNumber className="form__narrow-input" id='Max' value={deviceInitParams.uDtmVisualizationPrecision} name='uDtmVisualizationPrecision' onValueChange={saveDeviceData} />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label style={{ padding: `${globalSizeFactor * 0.15}vh` }} htmlFor='Max'>Web Request Retry Count:</label>
                <InputNumber className="form__narrow-input" id='Max' value={deviceInitParams.uWebRequestRetryCount} name='uWebRequestRetryCount' onValueChange={saveDeviceData} />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label style={{ padding: `${globalSizeFactor * 0.15}vh` }} htmlFor='Max'>Async Query Tiles Max Active Web Requests:</label>
                <InputNumber className="form__narrow-input" id='Max' value={deviceInitParams.uAsyncQueryTilesMaxActiveWebRequests} name='uAsyncQueryTilesMaxActiveWebRequests' onValueChange={saveDeviceData} />
            </div>
            <div className="open-map-manually__sticky-footer form__row-container form__justify-end">
                <Button onClick={() => {
                    runCodeSafely(() => {
                        if (MapCoreData.device == null) {
                            MapCoreService.initDevice(generalService.mapCorePropertiesBase.deviceInitParams);
                            setExistsDevice(MapCoreData.device ? true : false)
                        }
                    }, "SeveralLayersDialog.useEffect => []")
                }} label="Create" />
            </div>
        </div >
    )
}