import { InputText } from "primereact/inputtext";
import { useEffect, useState } from "react";
import { Fieldset } from "primereact/fieldset";
import { Button } from "primereact/button";
import { InputNumber } from "primereact/inputnumber";
import { Dropdown } from "primereact/dropdown";
import { Dialog } from "primereact/dialog";
import { Checkbox } from "primereact/checkbox";
import { useSelector } from "react-redux";
import _ from 'lodash';

import { MapCoreData, TypeToStringService, getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import './styles/rawVectorParamsCtrl.css';
import SelectCoordinateSystem from "../ControlsForMapcoreObjects/coordinateSystemCtrl/selectCoordinateSystem";
import Font, { FontDialogActionMode } from "../../mapToolbarActions/symbolicItemsDialogs/font/font";
import InputMaxNumber from "../../../shared/inputMaxNumber";
import { runCodeSafely, runMapCoreSafely } from "../../../../common/services/error-handling/errorHandler";
import { AppState } from "../../../../redux/combineReducer";

export function cloneRawVectorParams(rawVectorParams: MapCore.IMcRawVectorMapLayer.SParams) {
    let updatedObj = _.cloneDeep(rawVectorParams);
    updatedObj.eAutoStylingType = rawVectorParams.eAutoStylingType;
    updatedObj.pSourceCoordinateSystem = rawVectorParams.pSourceCoordinateSystem;
    updatedObj.StylingParams = rawVectorParams.StylingParams;
    return updatedObj;
}

export default function RawVectorParamsCtrl(props: { defaultRawVectorParams?: MapCore.IMcRawVectorMapLayer.SParams, getRawVectorParams: (rawVectorParams: MapCore.IMcRawVectorMapLayer.SParams) => void }) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [isMountingFlag, setIsMountingFlag] = useState(true);
    const [enumDetails] = useState({
        EAutoStylingType: getEnumDetailsList(MapCore.IMcRawVectorMapLayer.EAutoStylingType),
        EVersionCompatibility: getEnumDetailsList(MapCore.IMcOverlayManager.ESavingVersionCompatibility),
    });

    const getGridCS = () => {
        let sourceGridCSOptions: { MCgridCS: MapCore.IMcGridCoordinateSystem, name: string }[] = [];
        runCodeSafely(() => {
            MapCoreData.viewportsData.forEach((vp) => {
                if (vp.viewport) {
                    let cs = null;
                    runMapCoreSafely(() => { cs = vp.viewport.GetCoordinateSystem() }, 'LoadObjsFromRawVectorForm.getGridCS => IMcSpatialQueries.GetCoordinateSystem', true);
                    let csType = TypeToStringService.convertNumberToGridString(cs.GetGridCoorSysType());
                    sourceGridCSOptions.push({ MCgridCS: cs, name: `${csType}` });
                }
            });
        }, 'LoadObjsFromRawVectorForm.getGridCS');
        return sourceGridCSOptions;
    }
    let [loadObjsFromRawVectorFormData, setLoadObjsFromRawVectorFormData] = useState({
        isInternalStyle: false,
        isCustomStyle: false,
        sourceGridCSOptions: getGridCS(),
        isFontDialogVisible: false,
        isWorldLimit: false,
        emptyInput: '',
        //form fields
        rawVectorParams: {
            rawVecParamSourceGridCS: null,//
            localeStr: '',//
            maxScale: '',//
            minScale: 0,//
            pointTextureFile: '',//
            simplificationTolerance: 0,//
            minPixelSizeForObjVisibility: 0,//
            maxNumVerticesPerTile: 0,//
            optimizationMinScale: 0,//
            maxNumVisiblePoint: 0,//
            autoStylingType: getEnumValueDetails(MapCore.IMcRawVectorMapLayer.EAutoStylingType.EAST_INTERNAL, enumDetails.EAutoStylingType),//
            customStylingFolder: '',//
            //World Limit
            maxPointX: 0,//
            maxPointY: 0,//
            minPointX: 0,//
            minPointY: 0,//
            //pStylingParams
            outputFolder: '',
            outputXMLFileName: '',
            stylingFile: '',
            maxScaleFactor: 1,
            versionCompatibility: getEnumValueDetails(MapCore.IMcOverlayManager.ESavingVersionCompatibility.ESVC_LATEST, enumDetails.EVersionCompatibility),
            textMaxScale: 0,
            pDefaultFont: null,
        }
    });

    //#region UseEffects
    useEffect(() => {
        runCodeSafely(() => {
            setIsMountingFlag(false);
        }, 'LoadObjsFromRawVectorForm.useEffect')
    }, [])
    //so that this useEffect will happen, need to set also new adress to the object!
    useEffect(() => {
        runCodeSafely(() => {
            let fieldsByProps = getMapCoreDefaultFields(props.defaultRawVectorParams)
            if (!_.isEqual(fieldsByProps, loadObjsFromRawVectorFormData.rawVectorParams)) {
                let x = {
                    ...loadObjsFromRawVectorFormData,
                    rawVectorParams: fieldsByProps,
                    isCustomStyle: fieldsByProps.autoStylingType.theEnum === MapCore.IMcRawVectorMapLayer.EAutoStylingType.EAST_CUSTOM,
                    isInternalStyle: fieldsByProps.autoStylingType.theEnum === MapCore.IMcRawVectorMapLayer.EAutoStylingType.EAST_INTERNAL,
                    isWorldLimit: props.defaultRawVectorParams?.pClipRect ? true : false,
                }
                setLoadObjsFromRawVectorFormData(x);
            }
        }, 'LoadObjsFromRawVectorForm.useEffect => props.defaultRawVectorParams')
    }, [props.defaultRawVectorParams])
    useEffect(() => {
        runCodeSafely(() => {
            if (!isMountingFlag) {
                setLoadObjsFromRawVectorFormData({
                    ...loadObjsFromRawVectorFormData,
                    isCustomStyle: loadObjsFromRawVectorFormData.rawVectorParams.autoStylingType!.theEnum === MapCore.IMcRawVectorMapLayer.EAutoStylingType.EAST_CUSTOM,
                    isInternalStyle: loadObjsFromRawVectorFormData.rawVectorParams.autoStylingType!.theEnum === MapCore.IMcRawVectorMapLayer.EAutoStylingType.EAST_INTERNAL,
                })
            }
        }, 'LoadObjsFromRawVectorForm.useEffect => loadObjsFromRawVectorFormData.rawVectorParams.autoStylingType')
    }, [loadObjsFromRawVectorFormData.rawVectorParams.autoStylingType])
    useEffect(() => {
        runCodeSafely(() => {
            if (!isMountingFlag) {
                let finalParams = convertLocalParamsToMcParams(loadObjsFromRawVectorFormData.rawVectorParams);
                if (!_.isEqual(finalParams, props.defaultRawVectorParams)) {
                    props.getRawVectorParams(finalParams);
                }
            }
        }, 'LoadObjsFromRawVectorForm.useEffect => loadObjsFromRawVectorFormData')
    }, [loadObjsFromRawVectorFormData])
    useEffect(() => {//Required for prime react bug bypass (zero not written in input in empty input case onBlur)
        runCodeSafely(() => {
            if (loadObjsFromRawVectorFormData.emptyInput != '') {
                let rawVecData = { ...loadObjsFromRawVectorFormData.rawVectorParams, [loadObjsFromRawVectorFormData.emptyInput]: 0 }
                setLoadObjsFromRawVectorFormData({ ...loadObjsFromRawVectorFormData, rawVectorParams: rawVecData, emptyInput: '' })
            }
        }, 'LoadObjsFromRawVectorForm.useEffect => loadObjsFromRawVectorFormData.emptyInput')
    }, [loadObjsFromRawVectorFormData.emptyInput])
    //#endregion
    //#region Help Functions
    const getMapCoreDefaultFields = (defaultRawVectorParams: MapCore.IMcRawVectorMapLayer.SParams) => {
        let localRawVectorParams;
        let sRawParams = defaultRawVectorParams || new MapCore.IMcRawVectorMapLayer.SParams('', null);
        runCodeSafely(() => {
            localRawVectorParams = {
                minScale: sRawParams.fMinScale,
                maxScale: sRawParams.fMaxScale as unknown as string,
                pointTextureFile: sRawParams.strPointTextureFile,
                localeStr: sRawParams.strLocaleStr,
                rawVecParamSourceGridCS: sRawParams.pSourceCoordinateSystem,
                simplificationTolerance: sRawParams.dSimplificationTolerance,
                autoStylingType: getEnumValueDetails(sRawParams.eAutoStylingType, enumDetails.EAutoStylingType),
                customStylingFolder: sRawParams.strCustomStylingFolder,
                maxNumVerticesPerTile: sRawParams.uMaxNumVerticesPerTile,
                maxNumVisiblePoint: sRawParams.uMaxNumVisiblePointObjectsPerTile,
                minPixelSizeForObjVisibility: sRawParams.uMinPixelSizeForObjectVisibility,
                optimizationMinScale: sRawParams.fOptimizationMinScale,
                //World Limit
                maxPointX: sRawParams.pClipRect?.MaxVertex?.x,
                maxPointY: sRawParams.pClipRect?.MaxVertex?.y,
                minPointX: sRawParams.pClipRect?.MinVertex?.x,
                minPointY: sRawParams.pClipRect?.MinVertex?.y,
                //pStylingParams
                outputFolder: sRawParams.StylingParams.strOutputFolder,
                outputXMLFileName: sRawParams.StylingParams.strOutputXMLFileName,
                stylingFile: sRawParams.StylingParams.strStylingFile,
                maxScaleFactor: sRawParams.StylingParams.fMaxScaleFactor,
                versionCompatibility: sRawParams.StylingParams.eVersion,
                textMaxScale: sRawParams.StylingParams.fTextMaxScale,
                pDefaultFont: sRawParams.StylingParams.pDefaultFont,//as unknown as string,
            }
        }, 'rawVectorParamsCtrl.getMapCoreDefaultFields')
        return localRawVectorParams;
    }
    const convertLocalParamsToMcParams = (rawVectorParams: any): MapCore.IMcRawVectorMapLayer.SParams => {
        let sRawParams = new MapCore.IMcRawVectorMapLayer.SParams('', null);
        // sRawParams.strDataSource = `${loadObjsFromRawVectorFormData.rawVectorParams.dataSource}`;
        runCodeSafely(() => {
            sRawParams.fMinScale = rawVectorParams.minScale;
            sRawParams.fMaxScale = !Number.isNaN(parseInt(loadObjsFromRawVectorFormData.rawVectorParams.maxScale)) ? parseInt(loadObjsFromRawVectorFormData.rawVectorParams.maxScale) : 0;
            sRawParams.strPointTextureFile = rawVectorParams.pointTextureFile;
            sRawParams.strLocaleStr = rawVectorParams.localeStr;
            sRawParams.pSourceCoordinateSystem = rawVectorParams.rawVecParamSourceGridCS;
            sRawParams.dSimplificationTolerance = rawVectorParams.simplificationTolerance;
            sRawParams.eAutoStylingType = rawVectorParams.autoStylingType!.theEnum;
            sRawParams.strCustomStylingFolder = rawVectorParams.customStylingFolder;
            sRawParams.uMaxNumVerticesPerTile = rawVectorParams.maxNumVerticesPerTile;
            sRawParams.uMaxNumVisiblePointObjectsPerTile = rawVectorParams.maxNumVisiblePoint;
            sRawParams.uMinPixelSizeForObjectVisibility = rawVectorParams.minPixelSizeForObjVisibility;
            sRawParams.fOptimizationMinScale = rawVectorParams.optimizationMinScale;
            //World Limit
            let sMcBoxForPClipRect: MapCore.SMcBox = null;
            if (loadObjsFromRawVectorFormData.isWorldLimit) {
                let maxSMcVector3DVertex = new MapCore.SMcVector3D(rawVectorParams.maxPointX, rawVectorParams.maxPointY, 0);
                let minSMcVector3DVertex = new MapCore.SMcVector3D(rawVectorParams.minPointX, rawVectorParams.minPointY, 0);
                sMcBoxForPClipRect = new MapCore.SMcBox(minSMcVector3DVertex, maxSMcVector3DVertex);
            }
            sRawParams.pClipRect = sMcBoxForPClipRect;
            // //pStylingParams
            let StylingParamsClass = new MapCore.IMcRawVectorMapLayer.SInternalStylingParams();
            StylingParamsClass.strOutputFolder = rawVectorParams.outputFolder;
            StylingParamsClass.strOutputXMLFileName = rawVectorParams.outputXMLFileName;
            StylingParamsClass.strStylingFile = rawVectorParams.stylingFile;
            StylingParamsClass.fMaxScaleFactor = rawVectorParams.maxScaleFactor;
            StylingParamsClass.eVersion = rawVectorParams.versionCompatibility;
            StylingParamsClass.fTextMaxScale = rawVectorParams.textMaxScale;
            StylingParamsClass.pDefaultFont = rawVectorParams.pDefaultFont;
            sRawParams.StylingParams = StylingParamsClass;
        }, 'rawVectorParamsCtrl.convertLocalParamsToMcParams')
        return sRawParams;
    }
    const getSelectedCoordinateSystem = (selectedCS: MapCore.IMcGridCoordinateSystem) => {
        runCodeSafely(() => {
            let rawVecData = { ...loadObjsFromRawVectorFormData.rawVectorParams, rawVecParamSourceGridCS: selectedCS }
            setLoadObjsFromRawVectorFormData({ ...loadObjsFromRawVectorFormData, rawVectorParams: rawVecData })
        }, "rawVectorParamsCtrl.getSelectedCoordinateSystem")
    }
    const saveRawVectorData = (event: any) => {
        runCodeSafely(() => {
            let val = ['maxScale'].includes(event.target.name) ? parseInt(event.target.value) : event.target.type === "checkbox" ? event.target.checked : event.target.value;
            let rawVecData = { ...loadObjsFromRawVectorFormData.rawVectorParams, [event.target.name]: val }
            setLoadObjsFromRawVectorFormData({ ...loadObjsFromRawVectorFormData, rawVectorParams: rawVecData })
        }, "rawVectorParamsCtrl.saveRawVectorData => onChange")
    }
    const saveMaxScale = (value: number) => {
        runCodeSafely(() => {
            let rawVecData = { ...loadObjsFromRawVectorFormData.rawVectorParams, textMaxScale: value }
            setLoadObjsFromRawVectorFormData({ ...loadObjsFromRawVectorFormData, rawVectorParams: rawVecData })
        }, 'rawVectorParamsCtrl.saveMaxScale => onChange')
    }
    const showFontDialog = (e: any) => {
        runCodeSafely(() => {
            setLoadObjsFromRawVectorFormData({ ...loadObjsFromRawVectorFormData, isFontDialogVisible: true })
        }, 'rawVectorParamsCtrl.showFontDialog => onClick')
    }
    const validateWorldLimitInput = (e: any, inputType: string) => {
        runCodeSafely(() => {
            let isEmptyInput = isNaN(parseInt(e.target.value));
            let rawVecData = { ...loadObjsFromRawVectorFormData.rawVectorParams, [inputType]: isEmptyInput ? 1 : parseInt(e.target.value) }//1 is temporary value till useEffect will put zero (prime react bug bypass)
            setLoadObjsFromRawVectorFormData({ ...loadObjsFromRawVectorFormData, rawVectorParams: rawVecData, emptyInput: isEmptyInput ? inputType : '' })
        }, 'rawVectorParamsCtrl.validateWorldLimitInput => onBlur')
    }
    const handleWorldLimitCheckboxChange = (e) => {
        runCodeSafely(() => {
            setLoadObjsFromRawVectorFormData({
                ...loadObjsFromRawVectorFormData, isWorldLimit: e.checked,
                rawVectorParams: {
                    ...loadObjsFromRawVectorFormData.rawVectorParams,
                    //fill initial inputs value - 0
                    maxPointX: loadObjsFromRawVectorFormData.rawVectorParams.maxPointX || 0,
                    maxPointY: loadObjsFromRawVectorFormData.rawVectorParams.maxPointY || 0,
                    minPointX: loadObjsFromRawVectorFormData.rawVectorParams.minPointX || 0,
                    minPointY: loadObjsFromRawVectorFormData.rawVectorParams.minPointY || 0
                }
            })
        }, 'rawVectorParamsCtrl.convertLocalParamsToMcParams => onChange')
    }
    //#endregion
    //#region DOM Functions
    const getWorldLimitLegend = () => {
        return <span className="form__flex-and-row-between form__items-center" style={{ pointerEvents: 'auto' }} >
            <Checkbox onChange={handleWorldLimitCheckboxChange} checked={loadObjsFromRawVectorFormData.isWorldLimit} />
            <label>World Limit (Source Coordinate System)</label>
        </span>
    }
    const getCoordSystemFieldSet = () => {
        return <SelectCoordinateSystem initSelectedCorSys={loadObjsFromRawVectorFormData.rawVectorParams.rawVecParamSourceGridCS} getSelectedCorSys={getSelectedCoordinateSystem} header="Source Grid Coordinate System" />
    }
    const getWorldLimitFieldSet = () => {
        return <Fieldset className={`form__space-around form__column-fieldset ${loadObjsFromRawVectorFormData.isWorldLimit ? '' : 'form__disabled'}`} legend={getWorldLimitLegend()}>
            <div style={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-around', padding: `${globalSizeFactor * 0}vh` }}>
                Min Point :
                <div >
                    <label style={{ padding: `${globalSizeFactor * 0.15}vh` }} htmlFor='Min'>X :</label>
                    <InputNumber className="form__narrow-input" id='Min' value={loadObjsFromRawVectorFormData.rawVectorParams.minPointX} name='minPointX' onBlur={e => { validateWorldLimitInput(e, 'minPointX') }} />
                </div>
                <div >
                    <label style={{ padding: `${globalSizeFactor * 0.15}vh` }} htmlFor='Max'>Y :</label>
                    <InputNumber className="form__narrow-input" id='Max' value={loadObjsFromRawVectorFormData.rawVectorParams.minPointY} name='minPointY' onBlur={e => { validateWorldLimitInput(e, 'minPointY') }} />
                </div>
            </div>
            <div style={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-around', padding: `${globalSizeFactor * 0}vh` }}>
                Max Point :
                <div>
                    <label style={{ padding: `${globalSizeFactor * 0.15}vh` }} htmlFor='Min' className="font-bold block mb-2">X :</label>
                    <InputNumber className="form__narrow-input" id='Min' value={loadObjsFromRawVectorFormData.rawVectorParams.maxPointX} name='maxPointX' onBlur={e => { validateWorldLimitInput(e, 'maxPointX') }} />
                </div>
                <div>
                    <label style={{ padding: `${globalSizeFactor * 0.15}vh` }} htmlFor='Max'>Y :</label>
                    <InputNumber className="form__narrow-input" id='Max' value={loadObjsFromRawVectorFormData.rawVectorParams.maxPointY} name='maxPointY' onBlur={e => { validateWorldLimitInput(e, 'maxPointY') }} />
                </div>
            </div>
        </Fieldset>
    }
    const getStylingParamsFieldSet = () => {
        return <Fieldset className="form__column-fieldset" legend={<legend style={{ color: loadObjsFromRawVectorFormData.isInternalStyle ? 'black' : '#808080ba' }}>Styling Params</legend>}>
            <div style={{ display: 'flex', flexDirection: 'column', justifyContent: 'space-between' }}>
                <div className="raw-vector-params-ctrl__styling-params-input-divs" style={{ padding: `${globalSizeFactor * 0.15}vh` }}>
                    <label htmlFor='outputFolder' style={{ color: `${loadObjsFromRawVectorFormData.isInternalStyle ? 'black' : '#808080ba'}` }}>Output Folder: </label>
                    <InputText style={{ width: '73.5%' }} disabled={loadObjsFromRawVectorFormData.isInternalStyle ? false : true} id='outputFolder' value={loadObjsFromRawVectorFormData.rawVectorParams.outputFolder} name='outputFolder' onChange={saveRawVectorData} />
                </div>
                <div className="raw-vector-params-ctrl__styling-params-input-divs">
                    <div className="raw-vector-params-ctrl__styling-params-inputs">
                        <label htmlFor='outputXMLFileName' style={{ color: `${loadObjsFromRawVectorFormData.isInternalStyle ? 'black' : '#808080ba'}` }}>Output XML File Name: </label>
                        <InputText disabled={loadObjsFromRawVectorFormData.isInternalStyle ? false : true} id='outputXMLFileName' value={loadObjsFromRawVectorFormData.rawVectorParams.outputXMLFileName} name='outputXMLFileName' onChange={saveRawVectorData} />
                    </div>
                    <div className="raw-vector-params-ctrl__styling-params-inputs">
                        <label htmlFor='stylingFile' style={{ color: `${loadObjsFromRawVectorFormData.isInternalStyle ? 'black' : '#808080ba'}` }}>Styling File(*.sld .lyrx): </label>
                        <InputText disabled={loadObjsFromRawVectorFormData.isInternalStyle ? false : true} id='stylingFile' value={loadObjsFromRawVectorFormData.rawVectorParams.stylingFile} name='stylingFile' onChange={saveRawVectorData} />
                    </div>
                </div>
                <div className="raw-vector-params-ctrl__styling-params-input-divs">
                    <div className="raw-vector-params-ctrl__styling-params-inputs">
                        <label htmlFor='maxScaleFactor' style={{ color: `${loadObjsFromRawVectorFormData.isInternalStyle ? 'black' : '#808080ba'}` }}>Max Scale Factor: </label>
                        <InputNumber id='maxScaleFactor' disabled={loadObjsFromRawVectorFormData.isInternalStyle ? false : true} value={loadObjsFromRawVectorFormData.rawVectorParams.maxScaleFactor} name='maxScaleFactor' onValueChange={saveRawVectorData} />
                    </div>
                    <div className="raw-vector-params-ctrl__styling-params-inputs">
                        <label style={{ color: `${loadObjsFromRawVectorFormData.isInternalStyle ? 'black' : '#808080ba'}` }} htmlFor='versionCompatibility'>Version Compatibility: </label>
                        <Dropdown style={{ width: `${globalSizeFactor * 1.5 * 9.5}vh` }} disabled={loadObjsFromRawVectorFormData.isInternalStyle ? false : true} id="versionCompatibility" name='versionCompatibility' value={loadObjsFromRawVectorFormData.rawVectorParams.versionCompatibility} optionLabel='name' onChange={saveRawVectorData} options={enumDetails.EVersionCompatibility} />
                    </div>
                </div>
                <div className="raw-vector-params-ctrl__styling-params-input-divs">
                    <div className="raw-vector-params-ctrl__styling-params-inputs">
                        <label style={{ paddingRight: `${globalSizeFactor * 1.5 * 3.7}vh`, color: `${loadObjsFromRawVectorFormData.isInternalStyle ? 'black' : '#808080ba'}` }} htmlFor='textMaxScale'>Text Max Scale: </label>
                        <InputMaxNumber disabled={loadObjsFromRawVectorFormData.isInternalStyle ? false : true} value={loadObjsFromRawVectorFormData.rawVectorParams.textMaxScale} maxValue={MapCore.FLT_MAX} getUpdatedMaxInput={saveMaxScale} id='textMaxScale' name='textMaxScale' />
                    </div>
                    <div className="raw-vector-params-ctrl__styling-params-inputs">
                        <label style={{ paddingRight: `${globalSizeFactor * 1.5 * 5}vh`, color: `${loadObjsFromRawVectorFormData.isInternalStyle ? 'black' : '#808080ba'}` }} htmlFor='defaultFont' className="font-bold block mb-2">Default Font: </label>
                        {!loadObjsFromRawVectorFormData.rawVectorParams.pDefaultFont ?
                            <span style={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between', width: `${globalSizeFactor * 1.5 * 9.5}vh` }}>
                                <Button style={{ margin: `${globalSizeFactor * 0.3}vh` }} disabled={loadObjsFromRawVectorFormData.isInternalStyle ? false : true} onClick={showFontDialog} label="Selected" />
                                <Button disabled style={{ margin: `${globalSizeFactor * 0.3}vh` }} label="Delete Font" />
                            </span>
                            :
                            <span style={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between', width: `${globalSizeFactor * 1.5 * 9.5}vh` }}>
                                <Button name='updateText' disabled={loadObjsFromRawVectorFormData.isInternalStyle ? false : true} style={{ margin: `${globalSizeFactor * 0.3}vh` }} label="Update Text" onClick={showFontDialog} />
                                <Button disabled={loadObjsFromRawVectorFormData.isInternalStyle ? false : true} style={{ margin: `${globalSizeFactor * 0.3}vh` }} label="Delete Font" onClick={e => {
                                    let rawParams = { ...loadObjsFromRawVectorFormData.rawVectorParams, pDefaultFont: null };
                                    setLoadObjsFromRawVectorFormData({ ...loadObjsFromRawVectorFormData, rawVectorParams: rawParams })
                                }} />
                            </span>
                        }
                    </div>
                </div>
            </div>
        </Fieldset>
    }
    const getRawVectorFields = () => {
        return <div style={{ display: 'flex', flexDirection: 'column', width: '55%' }}>
            <div className="raw-vector-params-ctrl__load-params-style">
                <label htmlFor='minScale'>Min Scale: </label>
                <InputNumber id='minScale' value={loadObjsFromRawVectorFormData.rawVectorParams.minScale} name='minScale' onValueChange={saveRawVectorData} />
            </div>
            <div className="raw-vector-params-ctrl__load-params-style">
                <label htmlFor='maxScale'>Max Scale: </label>
                <InputText id='maxScale' value={loadObjsFromRawVectorFormData.rawVectorParams.maxScale === (MapCore.FLT_MAX as unknown as string) ? 'MAX' : loadObjsFromRawVectorFormData.rawVectorParams.maxScale} name='maxScale' onChange={saveRawVectorData} />
            </div>
            <div className="raw-vector-params-ctrl__load-params-style">
                <label htmlFor='localeStr'>Locale Str: </label>
                <InputText id='localeStr' value={loadObjsFromRawVectorFormData.rawVectorParams.localeStr} name='localeStr' onChange={saveRawVectorData} />
            </div>
            <div className="raw-vector-params-ctrl__load-params-style">
                <label htmlFor='pointTextureFile'>point Texture File: </label>
                <InputText id='pointTextureFile' value={loadObjsFromRawVectorFormData.rawVectorParams.pointTextureFile} name='pointTextureFile' onChange={saveRawVectorData} />
            </div>
            <div className="raw-vector-params-ctrl__load-params-style">
                <label style={{ paddingRight: `${globalSizeFactor * 1.5 * 0.9}vh` }} htmlFor='SimplificationTolerance'>Simplification Tolerance: </label>
                <InputNumber id='SimplificationTolerance' value={loadObjsFromRawVectorFormData.rawVectorParams.simplificationTolerance} name='SimplificationTolerance' onValueChange={saveRawVectorData} />
            </div>
            <div className="raw-vector-params-ctrl__load-params-style">
                <label htmlFor='minPixelSizeForObjVisibility'>Min Pixel Size For Object Visibility: </label>
                <InputNumber id='minPixelSizeForObjVisibility' value={loadObjsFromRawVectorFormData.rawVectorParams.minPixelSizeForObjVisibility} name='minPixelSizeForObjVisibility' onValueChange={saveRawVectorData} />
            </div>
            <div className="raw-vector-params-ctrl__load-params-style">
                <label htmlFor='maxNumVerticesPerTile'>Max Num Vertices Per Tile: </label>
                <InputNumber id='maxNumVerticesPerTile' value={loadObjsFromRawVectorFormData.rawVectorParams.maxNumVerticesPerTile} name='maxNumVerticesPerTile' onValueChange={saveRawVectorData} />
            </div>
            <div className="raw-vector-params-ctrl__load-params-style">
                <label htmlFor='optimizationMinScale'>Optimization Min Scale: </label>
                <InputNumber id='optimizationMinScale' value={loadObjsFromRawVectorFormData.rawVectorParams.optimizationMinScale} name='optimizationMinScale' onValueChange={saveRawVectorData} />
            </div>
            <div className="raw-vector-params-ctrl__load-params-style">
                <label htmlFor='maxNumVisiblePoint'>Max Num Visible Point Objects Per Tile: </label>
                <InputNumber id='maxNumVisiblePoint' value={loadObjsFromRawVectorFormData.rawVectorParams.maxNumVisiblePoint} name='maxNumVisiblePoint' onValueChange={saveRawVectorData} />
            </div>
            <div className="raw-vector-params-ctrl__load-params-style">
                <label htmlFor='autoStylingType'>Auto Styling Type: </label>
                <Dropdown style={{ width: `${globalSizeFactor * 1.5 * 9.5}vh` }} id="autoStylingType" name='autoStylingType' value={loadObjsFromRawVectorFormData.rawVectorParams.autoStylingType} onChange={saveRawVectorData} options={enumDetails.EAutoStylingType} optionLabel="name" />
            </div>
            <div className="raw-vector-params-ctrl__load-params-style">
                <label htmlFor='customStylingFolder' style={{ color: `${loadObjsFromRawVectorFormData.isCustomStyle ? 'black' : '#808080ba'}` }}>Custom Styling Folder: </label>
                <InputText id='customStylingFolder' disabled={loadObjsFromRawVectorFormData.isCustomStyle ? false : true} value={loadObjsFromRawVectorFormData.isCustomStyle ? loadObjsFromRawVectorFormData.rawVectorParams.customStylingFolder : ''} name='customStylingFolder' onChange={saveRawVectorData} />
            </div>
        </div>
    }
    //#endregion

    return (
        <div>
            <Fieldset className="form__column-fieldset" legend={<legend>Raw Vector Params</legend>}>
                <div style={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between' }}>
                    {getRawVectorFields()}
                    <div className="form__flex-and-column" style={{ justifyContent: 'space-between' }}>
                        {getCoordSystemFieldSet()}
                        {getWorldLimitFieldSet()}
                    </div>
                </div>
                {getStylingParamsFieldSet()}
            </Fieldset>

            <Dialog className="scroll-dialog-10" header={'Font Dialog'} onHide={() => {
                setLoadObjsFromRawVectorFormData({ ...loadObjsFromRawVectorFormData, isFontDialogVisible: false })
            }} visible={loadObjsFromRawVectorFormData.isFontDialogVisible}>
                <Font getFont={(font: MapCore.IMcFont, isSetAsDefault: boolean) => {
                    let rawParams = { ...loadObjsFromRawVectorFormData.rawVectorParams, pDefaultFont: font };
                    setLoadObjsFromRawVectorFormData({ ...loadObjsFromRawVectorFormData, rawVectorParams: rawParams, isFontDialogVisible: false })
                }} defaultFont={loadObjsFromRawVectorFormData.rawVectorParams.pDefaultFont} isSetAsDefaultCheckbox={false} actionMode={loadObjsFromRawVectorFormData.rawVectorParams.pDefaultFont ? FontDialogActionMode.update : FontDialogActionMode.create} />
            </Dialog>
        </div>
    )
}
