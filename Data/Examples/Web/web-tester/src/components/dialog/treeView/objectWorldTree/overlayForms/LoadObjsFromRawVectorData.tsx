import { InputText } from "primereact/inputtext";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";
import { ChangeEvent, useEffect, useRef, useState } from "react";
import { Checkbox } from "primereact/checkbox";
import { Button } from "primereact/button";
import { Dropdown } from "primereact/dropdown";
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import { useDispatch, useSelector } from "react-redux";
import { AppState } from "../../../../../redux/combineReducer";
import React from "react";
import objectWorldTreeService from "../../../../../services/objectWorldTree.service";
import { setObjectWorldTree, setTypeObjectWorldDialogSecond } from "../../../../../redux/ObjectWorldTree/objectWorldTreeActions";
import TreeNodeModel from '../../../../shared/models/tree-node.model';
import RawVectorParamsCtrl from "../../../shared/rawVectorParamsCtrl/rawVectorParamsCtrl";
import './styles/loadObjFromRawVectorData.css';
import UploadFilesCtrl, { UploadTypeEnum } from "../../../shared/uploadFilesCtrl";


export default function LoadObjsFromRawVectorData() {
    const dispatch = useDispatch();
    let currentOverlay = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [enumDetails] = useState({
        EAutoStylingType: getEnumDetailsList(MapCore.IMcRawVectorMapLayer.EAutoStylingType),
        EVersionCompatibility: getEnumDetailsList(MapCore.IMcOverlayManager.ESavingVersionCompatibility),
    });

    let [loadObjsFromRawVectorFormData, setLoadObjsFromRawVectorFormData] = useState({
        allFileSources: [],
        systemDirectories: [],
        //form fields
        dataSource: { filePath: '', label: '' },
        isClearObjSchemeCacheChecked: false,
        rawVectorParams: new MapCore.IMcRawVectorMapLayer.SParams('', null),
    });

    useEffect(() => {
        const root = document.documentElement;
        let pixelWidth = window.innerHeight * 0.8 * globalSizeFactor;
        root.style.setProperty('--load-obj-from-raw-vec-data-dialog-width', `${pixelWidth}px`);
        runCodeSafely(() => { setMapCoreDefaultFields(); }, 'LoadObjsFromRawVectorForm.useEffect')
    }, [])

    const setMapCoreDefaultFields = () => {
        let sRawParams = new MapCore.IMcRawVectorMapLayer.SParams('', null);
        setLoadObjsFromRawVectorFormData({
            ...loadObjsFromRawVectorFormData,
            dataSource: { filePath: sRawParams.strDataSource, label: sRawParams.strDataSource },
        })
    }
    const saveData = (event: any) => {
        runCodeSafely(() => {
            setLoadObjsFromRawVectorFormData({ ...loadObjsFromRawVectorFormData, [event.target.name]: event.target.type === "checkbox" ? event.target.checked : event.target.value })
        }, "OverlayForms/objects.saveData => onChange")
    }

    const handleLoadObjectsFromRawVectorDataClick = () => {
        runCodeSafely(() => {
            let overlay = currentOverlay.nodeMcContent as MapCore.IMcOverlay;
            let sRawParams = loadObjsFromRawVectorFormData.rawVectorParams;
            sRawParams.strDataSource = loadObjsFromRawVectorFormData.dataSource?.filePath;
            runMapCoreSafely(() => {
                let loadedObjects = overlay.LoadObjectsFromRawVectorData(sRawParams, loadObjsFromRawVectorFormData.isClearObjSchemeCacheChecked);
                objectWorldTreeService.deleteSystemDirectories(loadObjsFromRawVectorFormData.systemDirectories);
                // let sysytemFilesInMainDir = loadObjsFromRawVectorFormData.allFileSources.filter(s => !s.includes('/'));
                // sysytemFilesInMainDir.forEach(file => { MapCore.IMcMapDevice.DeleteFileSystemFile(file); console.log(file) });
            }, 'LoadObjsFromRawVectorFormData.handleLoadObjectsFromRawVectorDataClick => MapCore.IMcOverlay.LoadObjectsFromRawVectorData', true);
            let tree: TreeNodeModel = objectWorldTreeService.buildTree()
            dispatch(setObjectWorldTree(tree))
            dispatch(setTypeObjectWorldDialogSecond(undefined))
        }, 'LoadObjsFromRawVectorFormData.handleLoadObjectsFromRawVectorDataClick => onClick')
    }
    const handleDirectorySelected = (virtualFSPath: string, selectedOption: UploadTypeEnum) => {
        runCodeSafely(() => {
            let filePaths = objectWorldTreeService.getFilePathesFromVirtualDirectory(virtualFSPath);// MapCore.IMcMapDevice.GetFileSystemDirectoryContents(virtualFSPath);
            let filePathesObjs = filePaths.map((path) => { return { filePath: path, label: path?.split('/').slice(1).join('/') } })
            setLoadObjsFromRawVectorFormData({ ...loadObjsFromRawVectorFormData, allFileSources: filePathesObjs, systemDirectories: [virtualFSPath.split('/')[0]] })
        }, "overlayForm.handleFileUpload => onChange")
    }
    const getFileNameFields = () => {
        return <div className="form__column-container">
            <div style={{ width: '60%' }} className="form__flex-and-row-between">
                <div>Directory Name </div>
                <UploadFilesCtrl isDirectoryUpload={true} uploadOptions={[UploadTypeEnum.upload]} getVirtualFSPath={handleDirectorySelected} />
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <span style={{ width: '60%' }} className="form__flex-and-row-between form__items-center">
                    <label htmlFor='dataSource'>File Name </label>
                    <Dropdown style={{ width: '58%' }} id="dataSource" name='dataSource' value={loadObjsFromRawVectorFormData.dataSource} onChange={saveData} options={loadObjsFromRawVectorFormData.allFileSources} optionLabel="label" />
                </span>
                <div style={{ width: '30%' }} className="form__flex-and-row form__items-center">
                    <Checkbox name='isClearObjSchemeCacheChecked' inputId="clearObjSchemeCache" onChange={saveData} checked={loadObjsFromRawVectorFormData.isClearObjSchemeCacheChecked} />
                    <label htmlFor="clearObjSchemeCache">Clear Object Schemes Cache</label>
                </div>
            </div>
        </div>
    }
    const getRawVectorParams = (rawVectorParams: MapCore.IMcRawVectorMapLayer.SParams) => {
        runCodeSafely(() => {
            setLoadObjsFromRawVectorFormData({ ...loadObjsFromRawVectorFormData, rawVectorParams: rawVectorParams })
        }, "LoadObjectsFromRawVectorData.getRawVectorParams")
    }

    return (
        <div>
            {getFileNameFields()}
            <RawVectorParamsCtrl getRawVectorParams={getRawVectorParams} />

            <span style={{ display: 'flex', justifyContent: 'space-around', paddingTop: `${globalSizeFactor * 2}vh` }}>
                <Button style={{ margin: `${globalSizeFactor * 0.3}vh` }} label="Load Objects From Raw Vector Data" onClick={handleLoadObjectsFromRawVectorDataClick} />
            </span>
        </div>
    )
}
