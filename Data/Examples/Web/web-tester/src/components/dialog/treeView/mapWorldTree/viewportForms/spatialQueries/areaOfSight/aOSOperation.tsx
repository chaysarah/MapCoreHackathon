import { useEffect, useState } from "react";
import { Properties } from "../../../../../dialog";
import { TabInfo } from "../../../../../shared/tabCtrls/tabModels";
import { Fieldset } from "primereact/fieldset";
import { Button } from "primereact/button";
import { Checkbox } from "primereact/checkbox";
import { runCodeSafely, runMapCoreSafely } from "../../../../../../../common/services/error-handling/errorHandler";
import { useDispatch, useSelector } from "react-redux";
import { AppState } from "../../../../../../../redux/combineReducer";
import spatialQueriesService from "../../../../../../../services/spatialQueries.service";
import { AreaOfSightOptionResultObjects, AreaOfSightOptionsEnum, AreaOfSightProperties, AreaOfSightResult } from "./structs";
import { SpatialQueryName } from "../spatialQueriesFooter";
import { getEnumDetailsList, getEnumValueDetails, MapCoreData, ObjectWorldService, ViewportData } from 'mapcore-lib';
import { setSpatialQueriesResultsObjects } from "../../../../../../../redux/MapWorldTree/mapWorldTreeActions";
import SaveToFile from "../../../../objectWorldTree/overlayForms/saveToFile";
import { Dialog } from "primereact/dialog";
import { setVirtualFSSerialNumber } from "../../../../../../../redux/MapCore/mapCoreAction";
import UploadFilesCtrl, { UploadTypeEnum } from "../../../../../shared/uploadFilesCtrl";
import _ from 'lodash';


export class AOSOperationProperties implements Properties {
    mcCurrentSpatialQueries: MapCore.IMcSpatialQueries;
    activeOverlay: MapCore.IMcOverlay;
    areSameMatrices: boolean;
    bPointVisibility: boolean;
    isCreateAutomatic: boolean;
    mapcoreResults: AreaOfSightResult;
    selectedScoutersSumType: { name: string, code: number, theEnum: MapCore.IMcSpatialQueries.EScoutersSumType };
    maxNumOfScouters: number;
    saveToFileVisible: boolean;
    savedFileTypeOptions: { name: string, extension: string }[];
    handleSaveToFileOkFunc: (fileName: string, fileType: string) => void;


    static getDefault(p: any): AOSOperationProperties {
        let { mcCurrentSpatialQueries } = p;
        let scoutersSumTypeEnumList = getEnumDetailsList(MapCore.IMcSpatialQueries.EScoutersSumType);
        let selectedScoutersSumType = getEnumValueDetails(MapCore.IMcSpatialQueries.EScoutersSumType.ESST_ALL, scoutersSumTypeEnumList);

        let defaults: AOSOperationProperties = {
            mcCurrentSpatialQueries: mcCurrentSpatialQueries,
            activeOverlay: null,
            areSameMatrices: false,
            bPointVisibility: true,
            isCreateAutomatic: false,
            mapcoreResults: new AreaOfSightResult(),
            selectedScoutersSumType: selectedScoutersSumType,
            maxNumOfScouters: 32,
            saveToFileVisible: false,
            savedFileTypeOptions: [],
            handleSaveToFileOkFunc: (fileName: string, fileType: string) => { },
        }
        return defaults;
    }
};

export default function AOSOperation(props: { tabInfo: TabInfo }) {
    let { saveData, setApplyCallBack, setPropertiesCallback, setSiblingProperty, getTabPropertiesByTabPropertiesClass, tabProperties } = props.tabInfo;
    const dispatch = useDispatch();
    const spatialQueriesResultsObjects = useSelector((state: AppState) => state.mapWorldTreeReducer.spatialQueriesResultsObjects)
    let virtualFSSerialNumber = useSelector((state: AppState) => state.mapCoreReducer.virtualFSSerialNumber)
    const activeCard: number = useSelector((state: AppState) => state.mapWindowReducer.activeCard);

    useEffect(() => {
        runCodeSafely(() => {
            let activeOverlay = getActiveOverlay();
            setPropertiesCallback('activeOverlay', activeOverlay);
        }, 'SpatialQueriesForm/AreaOfSight/aOSOperation.useEffect');
    }, [activeCard])

    const getActiveOverlay = () => {
        let activeOverlay = null;
        let activeViewport = MapCoreData.findViewport(activeCard);
        runCodeSafely(() => {
            let typedMcCurrentSpatialQueries = tabProperties.mcCurrentSpatialQueries as MapCore.IMcSpatialQueries;
            if (typedMcCurrentSpatialQueries.GetInterfaceType() == MapCore.IMcMapViewport.INTERFACE_TYPE) {
                let mcCurrentViewport = tabProperties.mcCurrentSpatialQueries as MapCore.IMcMapViewport;
                activeOverlay = ObjectWorldService.findActiveOverlayByMcViewport(mcCurrentViewport);
            }
            else if (activeViewport) {
                activeOverlay = ObjectWorldService.findActiveOverlayByMcViewport(activeViewport.viewport);
            }
        }, 'SpatialQueriesForm/AreaOfSight/aOSOperation.getActiveOverlay');
        return activeOverlay;
    }
    //#region Handle Functions
    const handleCreateMatrixClick = () => {
        runCodeSafely(() => {
            let areaOfSightRes = tabProperties.mapcoreResults as AreaOfSightResult;
            let matrix: MapCore.IMcObject = null;
            let areaOfsightOptionRes: AreaOfSightOptionResultObjects | null = spatialQueriesResultsObjects.objects.find((obj: AreaOfSightOptionResultObjects) => obj.optionName == AreaOfSightOptionsEnum.isAreaOfSight)
            let isMatrixExist = areaOfsightOptionRes?.resultObject[0] ? true : false;
            if (!isMatrixExist) {
                matrix = spatialQueriesResultsObjects.queryName == SpatialQueryName.GetAreaOfSightForMultipleScouters ?
                    spatialQueriesService.createMatrix(tabProperties.mcCurrentSpatialQueries, areaOfSightRes.ppAreaOfSight, tabProperties.bPointVisibility, tabProperties.selectedScoutersSumType.theEnum, tabProperties.maxNumOfScouters) :
                    spatialQueriesService.createMatrix(tabProperties.mcCurrentSpatialQueries, areaOfSightRes.ppAreaOfSight, tabProperties.bPointVisibility)
                let areaOfSightOption = new AreaOfSightOptionResultObjects(AreaOfSightOptionsEnum.isAreaOfSight, [matrix], spatialQueriesService.setAreaOfSightObjectVisibility, spatialQueriesService.removeObjectsCB);
                let filteredOptionResultObjectsList = spatialQueriesResultsObjects.objects.filter((obj: AreaOfSightOptionResultObjects) => obj.optionName !== AreaOfSightOptionsEnum.isAreaOfSight)
                let objectsList = [...filteredOptionResultObjectsList, areaOfSightOption];
                dispatch(setSpatialQueriesResultsObjects({ queryName: spatialQueriesResultsObjects.queryName, objects: objectsList, removeObjectsCB: spatialQueriesService.removeAreaOfSightOptionResultObjects }));
            }
            else {
                let isVisible = areaOfsightOptionRes.resultObject[0].GetVisibilityOption() == MapCore.IMcConditionalSelector.EActionOptions.EAO_FORCE_TRUE;
                !isVisible && areaOfsightOptionRes.setVisibilityFunction(areaOfsightOptionRes.resultObject[0], true);
            }
            let areaOfSightExistOptionsSelection = getTabPropertiesByTabPropertiesClass(AreaOfSightProperties).existOptionsSelection;
            let updatedExistOptionsSelection = [...areaOfSightExistOptionsSelection.filter(option => option.option !== AreaOfSightOptionsEnum.isAreaOfSight), { option: AreaOfSightOptionsEnum.isAreaOfSight, isSelected: true }]
            setSiblingProperty(AreaOfSightProperties, 'existOptionsSelection', updatedExistOptionsSelection)
        }, 'SpatialQueriesForm/AreaOfSight/aOSOperation.handleCreateMatrixClick');
    }
    const handleSaveMatrixToFileClick = () => {
        runCodeSafely(() => {
            setPropertiesCallback('saveToFileVisible', true)
            setPropertiesCallback('savedFileTypeOptions', [{ name: 'CSV File (*.csv)', extension: '.csv' }])
            setPropertiesCallback('handleSaveToFileOkFunc', handleSaveMatrixToFileOK)
        }, 'SpatialQueriesForm/AreaOfSight/aOSOperation.handleSaveMatrixToFileClick');
    }
    const handleSaveClick = () => {
        runCodeSafely(() => {
            setPropertiesCallback('saveToFileVisible', true)
            setPropertiesCallback('savedFileTypeOptions', [{ name: ' ', extension: '' }])
            setPropertiesCallback('handleSaveToFileOkFunc', handleSaveOK)
        }, 'SpatialQueriesForm/AreaOfSight/aOSOperation.handleSaveClick');
    }
    const handleLoadClick = (virtualFSPath: string, selectedOption: UploadTypeEnum) => {
        runCodeSafely(() => {
            let iAreaOfSight: MapCore.IMcSpatialQueries.IAreaOfSight;
            runMapCoreSafely(() => {
                iAreaOfSight = MapCore.IMcSpatialQueries.IAreaOfSight.Load(virtualFSPath);
            }, "SpatialQueriesForm/AreaOfSight/aOSOperation.handleLoadClick => IMcSpatialQueries.IAreaOfSight.Load", true);
            let areaOfSightRes: AreaOfSightResult;
            let existOptionsSelection = []
            if (iAreaOfSight) {
                areaOfSightRes = new AreaOfSightResult(iAreaOfSight);
                existOptionsSelection = [{ option: AreaOfSightOptionsEnum.isAreaOfSight, isSelected: false }]
            }
            else {
                areaOfSightRes = new AreaOfSightResult();
            }
            setPropertiesCallback('mapcoreResults', areaOfSightRes)
            setSiblingProperty(AreaOfSightProperties, 'mapcoreResults', areaOfSightRes);
            setSiblingProperty(AreaOfSightProperties, 'existOptionsSelection', existOptionsSelection);
            //Clean Exist Objects
            spatialQueriesService.removeExistObjects();
            dispatch(setSpatialQueriesResultsObjects({ queryName: SpatialQueryName.GetAreaOfSight, objects: [], removeObjectsCB: (objects: MapCore.IMcObject[]) => { } }))
        }, 'SpatialQueriesForm/AreaOfSight/aOSOperation.handleLoadClick');
    }
    const handleSaveMatrixToFileOK = (fileName: string, fileType: string) => {
        runCodeSafely(() => {
            const csvContent = [];
            let areaOfSightRes = tabProperties.mapcoreResults as AreaOfSightResult;
            let aOSMatrix = areaOfSightRes.ppAreaOfSight.GetAreaOfSightMatrix(tabProperties.bPointVisibility);
            // Header row
            csvContent.push(
                `uWidth: ,${aOSMatrix.uWidth},uHeight:,${aOSMatrix.uHeight},fAngle:,${aOSMatrix.fAngle},fTargetResolutionInMeters:,${aOSMatrix.fTargetResolutionInMeters},` +
                `fTargetResolutionInMapUnitsX:,${aOSMatrix.fTargetResolutionInMapUnitsX},fTargetResolutionInMapUnitsY:,${aOSMatrix.fTargetResolutionInMapUnitsY},LeftTopPoint:,${aOSMatrix.LeftTopPoint.x},` +
                `${aOSMatrix.LeftTopPoint.y},${aOSMatrix.LeftTopPoint.z},RightTopPoint:,${aOSMatrix.RightTopPoint.x},${aOSMatrix.RightTopPoint.y},${aOSMatrix.RightTopPoint.z},` +
                `LeftBottomPoint:,${aOSMatrix.LeftBottomPoint.x},${aOSMatrix.LeftBottomPoint.y},${aOSMatrix.LeftBottomPoint.z},RightBottomPoint:,${aOSMatrix.RightBottomPoint.x},` +
                `${aOSMatrix.RightBottomPoint.y},${aOSMatrix.RightBottomPoint.z}`
            );
            // Data rows
            for (let i = 0; i < aOSMatrix.uHeight; i++) {
                let line = "";
                for (let j = 0; j < aOSMatrix.uWidth; j++) {
                    const index = i * aOSMatrix.uHeight + j;
                    if (index < aOSMatrix.aPointsVisibilityColors.length) {
                        // Convert dwRGBA to hex string (similar to C# "X" format)
                        let dwRGBA = (aOSMatrix.aPointsVisibilityColors[index].a << 24) | (aOSMatrix.aPointsVisibilityColors[index].r << 16) | (aOSMatrix.aPointsVisibilityColors[index].g << 8) | aOSMatrix.aPointsVisibilityColors[index].b;
                        const hexColorStr = dwRGBA.toString(16).toUpperCase();
                        line += hexColorStr + ",";
                    }
                }
                csvContent.push(line);
            }
            const csvString = csvContent.join('\r\n');
            const encoder = new TextEncoder();
            const uint8Array = encoder.encode(csvString);
            let finalFileName = `${fileName}${fileType}`;
            MapCore.IMcMapDevice.DownloadBufferAsFile(uint8Array, finalFileName);
            setPropertiesCallback('saveToFileVisible', false)
        }, 'SpatialQueriesForm/AreaOfSight/aOSOperation.handleSaveMatrixToFileOK');
    }
    const handleSaveOK = (fileName: string, fileType: string) => {
        runCodeSafely(() => {
            runMapCoreSafely(() => { MapCore.IMcMapDevice.CreateFileSystemDirectory(`${virtualFSSerialNumber}`) }, "SpatialQueriesForm/AreaOfSight/aOSOperation.handleSaveOK => IMcMapDevice.CreateFileSystemDirectory", true);
            runMapCoreSafely(() => { MapCore.IMcMapDevice.CreateFileSystemFile(`${virtualFSSerialNumber}/${fileName}`, '') }, "SpatialQueriesForm/AreaOfSight/aOSOperation.handleSaveOK => IMcMapDevice.DeleteFileSystemFile", true);

            let areaOfSightRes = tabProperties.mapcoreResults as AreaOfSightResult;
            if (areaOfSightRes.ppAreaOfSight) {
                areaOfSightRes.ppAreaOfSight.Save(`${virtualFSSerialNumber}/${fileName}`)
            }
            let fileBufferOrStr = MapCore.IMcMapDevice.GetFileSystemFileContents(`${virtualFSSerialNumber}/${fileName}`);
            const encoder = new TextEncoder();
            const uint8Array = typeof fileBufferOrStr === 'string' ? encoder.encode(fileBufferOrStr) : fileBufferOrStr;
            MapCore.IMcMapDevice.DownloadBufferAsFile(uint8Array, fileName);

            runMapCoreSafely(() => { MapCore.IMcMapDevice.DeleteFileSystemFile(`${virtualFSSerialNumber}/${fileName}`) }, "SpatialQueriesForm/AreaOfSight/aOSOperation.handleSaveOK => IMcMapDevice.CreateFileSystemFile", true);
            dispatch(setVirtualFSSerialNumber(virtualFSSerialNumber + 1))
            setPropertiesCallback('saveToFileVisible', false)
        }, 'SpatialQueriesForm/AreaOfSight/aOSOperation.handleLoadClick');
    }
    const handleTestSaveAndLoadClick = () => {
        runCodeSafely(() => {
            runMapCoreSafely(() => { MapCore.IMcMapDevice.CreateFileSystemDirectory(`${virtualFSSerialNumber}`) }, "SpatialQueriesForm/AreaOfSight/aOSOperation.handleTestSaveAndLoadClick => IMcMapDevice.CreateFileSystemDirectory", true);
            runMapCoreSafely(() => { MapCore.IMcMapDevice.CreateFileSystemFile(`${virtualFSSerialNumber}/testSaveAndLoadFile`, '') }, "SpatialQueriesForm/AreaOfSight/aOSOperation.handleTestSaveAndLoadClick => IMcMapDevice.DeleteFileSystemFile", true);

            let areaOfSightRes = tabProperties.mapcoreResults as AreaOfSightResult;
            runMapCoreSafely(() => {
                areaOfSightRes.ppAreaOfSight.Save(`${virtualFSSerialNumber}/testSaveAndLoadFile`)
            }, "SpatialQueriesForm/AreaOfSight/aOSOperation.handleTestSaveAndLoadClick => IMcSpatialQueries.IAreaOfSight.Save", true);

            let loadedIAreaOfSight: MapCore.IMcSpatialQueries.IAreaOfSight;
            runMapCoreSafely(() => {
                loadedIAreaOfSight = MapCore.IMcSpatialQueries.IAreaOfSight.Load(`${virtualFSSerialNumber}/testSaveAndLoadFile`);
            }, "SpatialQueriesForm/AreaOfSight/aOSOperation.handleTestSaveAndLoadClick => IMcSpatialQueries.IAreaOfSight.Load", true);

            let initialMatrix = areaOfSightRes.ppAreaOfSight.GetAreaOfSightMatrix(tabProperties.bPointVisibility);
            let loadedMatrix = loadedIAreaOfSight.GetAreaOfSightMatrix(tabProperties.bPointVisibility);

            let areSame = null;
            runMapCoreSafely(() => {
                areSame = MapCore.IMcSpatialQueries.AreSameRectAreaOfSightMatrices(initialMatrix, loadedMatrix);
            }, 'SpatialQueriesForm/AreaOfSight/MatrixOperation.handleTestSaveAndLoadClick => IMcSpatialQueries.AreSameRectAreaOfSightMatrices', true);

            let isSamePointVisibilityLen = initialMatrix.aPointsVisibilityColors.length == loadedMatrix.aPointsVisibilityColors.length;
            if (areSame && isSamePointVisibilityLen) {
                initialMatrix.aPointsVisibilityColors.forEach((color, i) => {
                    if (!_.isEqual(color, loadedMatrix.aPointsVisibilityColors[i])) {
                        areSame = false;
                    }
                })
            }
            setPropertiesCallback('areSameMatrices', areSame)
            runMapCoreSafely(() => { MapCore.IMcMapDevice.DeleteFileSystemFile(`${virtualFSSerialNumber}/testSaveAndLoadFile`) }, "SpatialQueriesForm/AreaOfSight/aOSOperation.handleTestSaveAndLoadClick => IMcMapDevice.CreateFileSystemFile", true);
            dispatch(setVirtualFSSerialNumber(virtualFSSerialNumber + 1))
        }, 'SpatialQueriesForm/AreaOfSight/aOSOperation.handleTestSaveAndLoadClick');
    }
    //#endregion
    const getCreateMatrixButtonClassName = () => {
        let isSuitQueries = spatialQueriesResultsObjects.queryName == SpatialQueryName.GetAreaOfSight || spatialQueriesResultsObjects.queryName == SpatialQueryName.GetAreaOfSightForMultipleScouters;
        let isAreaOfSightExist = getTabPropertiesByTabPropertiesClass(AreaOfSightProperties).mapcoreResults.ppAreaOfSight
        return isSuitQueries && isAreaOfSightExist && tabProperties.activeOverlay ? '' : 'form__disabled';
    }

    return (
        <div className="form__flex-and-row-between">
            <Fieldset style={{ width: '50%' }} legend='Draw Matrix' className="form__column-fieldset">
                <div className="form__flex-and-row form__items-center" style={{ pointerEvents: 'auto' }}>
                    <Checkbox id="isCreateAutomatic" name="isCreateAutomatic" checked={tabProperties.isCreateAutomatic} onChange={saveData} />
                    <label htmlFor="isCreateAutomatic">Is Create And Draw Matrix Automatic</label>
                </div>
                <div className={`form__flex-and-row form__items-center ${getTabPropertiesByTabPropertiesClass(AreaOfSightProperties).mapcoreResults.ppAreaOfSight ? '' : 'form__disabled'}`}>
                    <Checkbox id="bPointVisibility" name="bPointVisibility" checked={tabProperties.bPointVisibility} onChange={saveData} />
                    <label htmlFor="bPointVisibility">Fill Points Visibility</label>
                </div>
                <Button className={getCreateMatrixButtonClassName()} style={{ width: '50%' }} label="Create Matrix" onClick={handleCreateMatrixClick} />
                <Button className={getTabPropertiesByTabPropertiesClass(AreaOfSightProperties).mapcoreResults.ppAreaOfSight ? '' : 'form__disabled'} style={{ width: '50%' }} label="Save Matrix To File" onClick={handleSaveMatrixToFileClick} />
            </Fieldset>
            <Fieldset style={{ width: '50%' }} legend='Save/Load Area Of Sight' className={`form__column-fieldset`}>
                <Button className={getTabPropertiesByTabPropertiesClass(AreaOfSightProperties).mapcoreResults.ppAreaOfSight ? '' : 'form__disabled'} style={{ width: '50%' }} label="Save" onClick={handleSaveClick} />
                <div style={{ width: '50%' }}>  <UploadFilesCtrl isDirectoryUpload={false} uploadOptions={[UploadTypeEnum.upload]} getVirtualFSPath={handleLoadClick} buttonOnly={true} label='Load' /></div>
                <div className={`form__flex-and-row-between form__items-center ${getTabPropertiesByTabPropertiesClass(AreaOfSightProperties).mapcoreResults.ppAreaOfSight ? '' : 'form__disabled'}`}>
                    <Button style={{ width: '50%' }} label="Test Save And Load" onClick={handleTestSaveAndLoadClick} />
                    <div style={{ width: '45%' }} className="form__flex-and-row form__items-center form__disabled">
                        <Checkbox id="areSameMatrices" name="areSameMatrices" checked={tabProperties.areSameMatrices} onChange={saveData} />
                        <label htmlFor="areSameMatrices">Are Same Matrices</label>
                    </div>
                </div>
            </Fieldset>
            <Dialog visible={tabProperties.saveToFileVisible} onHide={() => setPropertiesCallback('saveToFileVisible', false)}>
                <SaveToFile savedFileTypeOptions={tabProperties.savedFileTypeOptions} handleSaveToFileOk={tabProperties.handleSaveToFileOkFunc} />
            </Dialog>
        </div>
    )
}



