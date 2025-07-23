import { Column } from "primereact/column";
import { DataTable } from "primereact/datatable";
import { useEffect, useState } from "react";
import 'xlsx';
import { Button } from "primereact/button";
import { InputText } from "primereact/inputtext";
import { useDispatch, useSelector } from "react-redux";
import { ConfirmDialog } from "primereact/confirmdialog";

import { MapCoreData, ScanService, ViewportData, ViewportService, TypeToStringService } from 'mapcore-lib';
import './styles/smartRealityBuildingHistory.css';
import { runCodeSafely, runMapCoreSafely } from "../../../../../../common/services/error-handling/errorHandler";
import { AppState } from "../../../../../../redux/combineReducer";
import { setTypeMapWorldDialogSecond } from "../../../../../../redux/MapWorldTree/mapWorldTreeActions";

export default function ScanItem3DModelSmartReality(props: { target?: MapCore.IMcSpatialQueries.STargetFound }) {
    const dispatch = useDispatch();
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const activeCard = useSelector((state: AppState) => state.mapWindowReducer.activeCard);
    const [targetID, setTargetID] = useState(props.target ? ScanService.GetTargetIdByBitCount(props.target) as string : null);
    const [url, setUrl] = useState("");
    const [buildingHistory, setBuildingHistory] = useState<MapCore.IMcMapLayer.SSmartRealityBuildingHistory[]>([]);
    const [confirmDialogVisible, setConfirmDialogVisible] = useState(false);
    const [confirmDialogMessage, setConfirmDialogMessage] = useState('');

    useEffect(() => {
        const root = document.documentElement;
        let pixelWidth = window.innerHeight * (props.target ? 0.6 : 0.3) * globalSizeFactor;
        root.style.setProperty('--building-history-dialog-width', `${pixelWidth}px`);
    }, [])
    //#region Help Functions
    const jumpToBuilding = (buildingHistory: MapCore.IMcMapLayer.SSmartRealityBuildingHistory) => {
        runCodeSafely(() => {
            let centerPoint = MapCore.v3Zero;
            runMapCoreSafely(() => {
                centerPoint = MapCore.SMcBox.CenterPoint(buildingHistory.BoundingBox);
            }, 'ScanItem3DModelSmartReality.handleJumpToBuildingClick => SMcBox.CenterPoint', true)
            if (buildingHistory.pCoordinateSystem) {
                let activeMcViewport: MapCore.IMcMapViewport = MapCoreData.findViewport(activeCard)?.viewport;
                let activeViewportCoordSystem = activeMcViewport ? activeMcViewport.GetCoordinateSystem() : null
                if (activeViewportCoordSystem && buildingHistory.pCoordinateSystem != activeViewportCoordSystem) {
                    let gridConverter: MapCore.IMcGridConverter = null;
                    runMapCoreSafely(() => {
                        gridConverter = MapCore.IMcGridConverter.Create(buildingHistory.pCoordinateSystem, activeViewportCoordSystem, false);
                    }, 'ScanItem3DModelSmartReality.handleJumpToBuildingClick => IMcGridConverter.Create', true)
                    let zoneResult: { Value?: number } = {};
                    runMapCoreSafely(() => { centerPoint = gridConverter.ConvertAtoB(centerPoint, zoneResult); }, 'ScanItem3DModelSmartReality.handleJumpToBuildingClick => IMcGridConverter.ConvertAtoB', true);
                }
                activeMcViewport && ViewportService.setCameraPosition(activeMcViewport, centerPoint)
            }
        }, "ScanItem3DModelSmartReality.jumpToBuilding")
    }
    const parseUUIDEndian = (uuid: string) => {
        // Split UUID into its parts
        const parts = uuid.split('-');

        if (parts.length !== 5) {
            throw new Error('Invalid UUID format');
        }

        parts[0] = reverseHexString(parts[0]);
        parts[1] = reverseHexString(parts[1]);
        parts[2] = reverseHexString(parts[2]);

        const stringToParse = parts.join('');

        const byteArray = new Uint8Array(stringToParse.length / 2);

        for (let i = 0; i < byteArray.length; i++) {
            const byteValue = parseInt(stringToParse.slice(i * 2, i * 2 + 2), 16);
            byteArray[i] = byteValue;
        }

        return byteArray;
    };
    // וזו פונקציית עזר שמחליפה סדר של ערכים הקסהדצימליים:
    const reverseHexString = (hex: string) => {
        return hex.match(/../g)?.reverse().join('') || hex;
    };
    //#endregion
    //#region Handle Functions
    const handleGetBuildingHistoryByUrlClick = () => {
        runCodeSafely(() => {
            let asyncOperationCallback = new MapCoreData.asyncOperationCallBacksClass((sStatus: MapCore.IMcErrors.ECode, strServerURL: string, uObjectID: MapCore.SMcVariantID,
                buildingHistory: MapCore.IMcMapLayer.SSmartRealityBuildingHistory[]) => {
                runCodeSafely(() => {
                    if (sStatus == MapCore.IMcErrors.ECode.SUCCESS) {
                        setBuildingHistory(buildingHistory)
                    }
                    else {
                        let errorMessage = '';
                        runMapCoreSafely(() => { errorMessage = MapCore.IMcErrors.ErrorCodeToString(sStatus); }, 'ScanItem3DModelSmartReality.handleGetBuildingHistoryByUrlClick.asyncOperationCallBacksClass => IMcErrors.ErrorCodeToString', true)
                        setConfirmDialogMessage(errorMessage);
                        setConfirmDialogVisible(true);
                    }
                }, "ScanItem3DModelSmartReality.handleGetBuildingHistoryByUrlClick.asyncOperationCallBacksClass")
            })

            runMapCoreSafely(() => {
                MapCore.IMcMapDevice.Get3DModelSmartRealityData(url, MapCore.IMcMapDevice.ESmartRealityQuery.ESRQ_BUILDING_HISTORY, props.target.uTargetID, asyncOperationCallback);
            }, "ScanItem3DModelSmartReality.handleGetBuildingHistoryByUrlClick => IMcMapDevice.Get3DModelSmartRealityData", true)
        }, "ScanItem3DModelSmartReality.handleGetBuildingHistoryByUrlClick")
    }
    const handleJumpToBuildingClick = () => {
        runCodeSafely(() => {
            let asyncOperationCallback = new MapCoreData.asyncOperationCallBacksClass((sStatus: MapCore.IMcErrors.ECode, strServerURL: string, uObjectID: MapCore.SMcVariantID,
                buildingHistory: MapCore.IMcMapLayer.SSmartRealityBuildingHistory[]) => {
                runCodeSafely(() => {
                    if (sStatus == MapCore.IMcErrors.ECode.SUCCESS && buildingHistory && buildingHistory.length) {
                        let index = 0;
                        for (index = 0; index < buildingHistory.length; index++) {
                            let currentBuildingHistory = buildingHistory[index];
                            let centerPoint;
                            runMapCoreSafely(() => {
                                centerPoint = MapCore.SMcBox.CenterPoint(currentBuildingHistory.BoundingBox);
                            }, 'ScanItem3DModelSmartReality.handleJumpToBuildingClick => SMcBox.CenterPoint', true)
                            if (currentBuildingHistory.pCoordinateSystem && centerPoint == MapCore.v3Zero) {
                                jumpToBuilding(currentBuildingHistory);
                                dispatch(setTypeMapWorldDialogSecond(undefined))
                                return;
                            }
                        }
                        if (index == buildingHistory.length) {
                            setConfirmDialogVisible(true);
                            setConfirmDialogMessage("For this 'Object ID' don't exist building location")
                        }
                    }
                    else {
                        let errorMessage = '';
                        runMapCoreSafely(() => { errorMessage = MapCore.IMcErrors.ErrorCodeToString(sStatus); }, 'ScanItem3DModelSmartReality.handleJumpToBuildingClick.asyncOperationCallBacksClass => IMcErrors.ErrorCodeToString', true)
                        setConfirmDialogMessage(errorMessage);
                        setConfirmDialogVisible(true);
                    }
                }, "ScanItem3DModelSmartReality.handleJumpToBuildingClick.asyncOperationCallBacksClass")
            })

            let parsedUUID = parseUUIDEndian(targetID);//temporary function!
            if (parsedUUID.length == 0) {
                setConfirmDialogMessage('Invalid Object ID, please fix and try again');
                setConfirmDialogVisible(true);
            }
            else {

                let mcVariantID = new MapCore.SMcVariantID(parsedUUID)
                runMapCoreSafely(() => { MapCore.SMcVariantID.Set128Bit(mcVariantID, parsedUUID); }, 'ScanItem3DModelSmartReality.handleJumpToBuildingClick => SMcVariantID.Set128Bit', true)
                runMapCoreSafely(() => {
                    MapCore.IMcMapDevice.Get3DModelSmartRealityData(url, MapCore.IMcMapDevice.ESmartRealityQuery.ESRQ_BUILDING_HISTORY, mcVariantID, asyncOperationCallback);
                }, "ScanItem3DModelSmartReality.handleJumpToBuildingClick => IMcMapDevice.Get3DModelSmartRealityData", true)
            }
        }, "ScanItem3DModelSmartReality.handleJumpToBuildingClick")
    }
    //#endregion
    const toStringBoundingBox = (boundingBox: MapCore.SMcBox) => {
        let str = "MinVertex: (" + boundingBox.MinVertex.x + "," + boundingBox.MinVertex.y + "," + boundingBox.MinVertex.z + ")"
            + "MaxVertex: (" + boundingBox.MaxVertex.x + "," + boundingBox.MaxVertex.y + "," + boundingBox.MaxVertex.z + ")";
        return str;
    }

    return (<div className="form__column-container">
        <div style={{ width: `${globalSizeFactor * 30}vh` }} className=" form__column-container form__aligm-self-center">
            <div className="form__flex-and-row-between form__items-center">
                <label>Target id:</label>
                <InputText style={{ width: '70%' }} disabled={props.target ? true : false} value={targetID} onChange={e => setTargetID(e.target.value)} />
            </div>
            <div style={{ paddingBottom: '2%' }} className='form__flex-and-row-between form__items-center'>
                <label> URL </label>
                <InputText style={{ width: '70%' }} name="URL" value={url} onChange={(e) => setUrl(e.target.value)} />
            </div>
            {props.target ?
                <Button className="form__aligm-self-center" label={'Get'} onClick={handleGetBuildingHistoryByUrlClick} /> :
                <Button className="form__aligm-self-center" label={'Jump To Building'} onClick={handleJumpToBuildingClick} />
            }
        </div>
        <br />
        {props.target && <DataTable emptyMessage={() => { return <div></div> }} value={buildingHistory} tableStyle={{ minWidth: `${globalSizeFactor * 30}vh`, minHeight: `${globalSizeFactor * 10}vh` }} size='small' >
            <Column header="NO." body={(rowData, columnMeta) => columnMeta.rowIndex + 1} />
            <Column field="FlightDate" header="Data Time" body={(rowData) => { return <> {rowData.FlightDate.toString()}</> }}></Column>
            <Column field="dHeight" header="Height"></Column>
            <Column field="BoundingBox" header="Bounding Box" body={(rowData) => { return <> {toStringBoundingBox(rowData.BoundingBox)}</> }}></Column>
            <Column field="pCoordinateSystem" header="Coordinate System" body={(rowData) => { return <> {TypeToStringService.convertNumberToGridString(rowData.pCoordinateSystem)}</> }}></Column>
            <Column header="Jump To Building" body={(rowData) => {
                return <Button onClick={() => {
                    let pos: MapCore.SMcVector3D = new MapCore.SMcVector3D((rowData.BoundingBox.MinVertex.x + rowData.BoundingBox.MaxVertex.x) / 2,
                        (rowData.BoundingBox.MinVertex.y + rowData.BoundingBox.MaxVertex.y) / 2, (rowData.BoundingBox.MinVertex.z + rowData.BoundingBox.MaxVertex.z));
                    let viewportData: ViewportData = MapCoreData.findViewport(activeCard);
                    ViewportService.setCameraPosition(viewportData.viewport, pos)
                }}>??</Button>
            }}></Column>
        </DataTable>}

        <ConfirmDialog
            contentClassName='form__confirm-dialog-content'
            message={confirmDialogMessage}
            header=''
            footer={<div></div>}
            visible={confirmDialogVisible}
            onHide={e => { setConfirmDialogVisible(false) }}
        />
    </div>)
}
