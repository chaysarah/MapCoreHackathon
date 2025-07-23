import { Column } from "primereact/column";
import { DataTable } from "primereact/datatable";
import { ScanService } from 'mapcore-lib';
import { useSelector } from "react-redux";
import { AppState } from "../../../../../../redux/combineReducer";
import { useEffect } from "react";
import React, { useState } from 'react';
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import 'xlsx';
import { utils, write } from 'xlsx';
import { Button } from "primereact/button";
import generalService from "../../../../../../services/general.service";
import { runCodeSafely } from "../../../../../../common/services/error-handling/errorHandler";

export default function ScanItemDetailsLayer(props: { target: MapCore.IMcSpatialQueries.STargetFound }) {
    const currentViewport: number = useSelector((state: AppState) => state.mapWindowReducer.currentViewport);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor);
    const [UnifiedVectorItemsPoints, setUnifiedVectorItemsPoints] = useState(null);
    const [vectorItem, setvectorItem] = useState([]);

    useEffect(() => {
        runCodeSafely(() => {
            const root = document.documentElement;
            let pixelWidth = window.innerHeight * 0.6 * globalSizeFactor;
            root.style.setProperty('--scan-item-details-dialog-width', `${pixelWidth}px`);
            getPoint()
        }, "ScanItemDetailsLayer.useEffect")
    }, [])

    const getPoint = () => {
        let res: any = ScanService.getPoint(props.target, currentViewport, (arr, aUnifiedVectorItemsPoints) => {
            setUnifiedVectorItemsPoints(aUnifiedVectorItemsPoints);
            setvectorItem(arr)
        });
    }
    function exportPoint(): void {
        runCodeSafely(() => {
            const workbook = utils.book_new();
            let data = [];
            data.push(["X", "Y", "Z"])
            for (let index = 0; index < UnifiedVectorItemsPoints.length; index++) {
                const element: MapCore.SMcVector3D = UnifiedVectorItemsPoints[index];
                let row = [element.x, element.y, element.z]
                data.push(row)
            }
            const worksheet = utils.aoa_to_sheet(data);
            utils.book_append_sheet(workbook, worksheet, 'Sheet1');
            const excelFile = write(workbook, { bookType: 'xlsx', type: 'array' });
            generalService.createAnddownloadFile("exportPoint", excelFile, '.csv')
        }, "ScanItemDetailsLayer.exportPoint")
    }

    return (<>
        {props.target ? <div>
            <label>Target id:  {ScanService.GetTargetIdByBitCount(props.target)}</label>
            <h4>Unified Vector Items Points</h4>
            <DataTable rows={5} value={vectorItem} tableStyle={{ minWidth: '50rem' }} size='small'   >
                <Column field="index" ></Column>
                <Column field="VectorItemID" header="VectorItemID"></Column>
                <Column field="VectorItemFirstPointIndex" header="Vector Item First Point Index"></Column>
                <Column field="VectorItemLastPointIndex" header="Vector Item Last Point Index"></Column>
            </DataTable>
            <h4>Object Location</h4>
            <DataTable paginator rows={5} value={UnifiedVectorItemsPoints} tableStyle={{ minWidth: '50rem' }} size='small'>
                <Column field="index" ></Column>
                <Column field="x" header="x"></Column>
                <Column field="y" header="y"></Column>
                <Column field="z" header="z"></Column>
            </DataTable>
            {UnifiedVectorItemsPoints && <label>Namber of points:  {UnifiedVectorItemsPoints.length}</label>}
            <Button onClick={exportPoint}>Export Point</Button>
        </div> : <></>}
    </>)
}