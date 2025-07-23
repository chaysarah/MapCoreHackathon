import { Checkbox } from "primereact/checkbox";
import { Column } from "primereact/column"
import { DataTable, DataTableRowClickEvent } from "primereact/datatable"
import { Fieldset } from "primereact/fieldset"
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import _ from 'lodash';

import { AppState } from "../../../../../../redux/combineReducer";
import { resetQueryResultsTableRow } from "../../../../../../redux/MapWorldTree/mapWorldTreeActions";
import { runCodeSafely } from "../../../../../../common/services/error-handling/errorHandler";

export enum SpatialQueryName {
    GetTerrainHeight = "GetTerrainHeight",
    GetTerrainHeightsAlongLine = "GetTerrainHeightsAlongLine",
    GetExtremeHeightPointsInPolygon = "GetExtremeHeightPointsInPolygon",
    GetTerrainAngles = "GetTerrainAngles",
    GetLineOfSight = "GetLineOfSight",
    GetPointVisibility = "GetPointVisibility",
    GetRayIntersection = "GetRayIntersection",
    GetRayIntersectionTargets = "GetRayIntersectionTargets",
    LocationFromTwoDistancesAndAzimuth = "LocationFromTwoDistancesAndAzimuth",
    DrawAreaOfSightObject = 'DrawAreaOfSightObject',
    GetAreaOfSight = "GetAreaOfSight",
    GetTerrainHeightMatrix = "GetTerrainHeightMatrix",
    GetAreaOfSightForMultipleScouters = "GetAreaOfSightForMultipleScouters",
    GetBestScoutersLocationsInEllipse = "GetBestScoutersLocationsInEllipse",
    None = "None",
    InitRandomGPSPointInputs = "InitRandomGPSPointInputs",
    InitRandomGPSPoint = "InitRandomGPSPoint",
    GetTraversabilityAlongLine = "GetTraversabilityAlongLine",
    GetRasterLayerColorByPoint = "GetRasterLayerColorByPoint",
}

export class QueryResult {
    index: number;
    spatialQueryName: SpatialQueryName;
    // queryParams: any;
    updateQueryParamsFuncCB: any;
    queryParams: any[];
    isAsync: boolean;
    errorMessage: string;
    areaOfSightName?: string;

    constructor(spatialQueryName: SpatialQueryName, updateQueryParamsFuncCB: any, queryParams: any[], isAsync: boolean, errorMessage: string, areaOfSightName?: string) {
        this.spatialQueryName = spatialQueryName;
        this.updateQueryParamsFuncCB = updateQueryParamsFuncCB;
        this.queryParams = queryParams;
        this.isAsync = isAsync;
        this.errorMessage = errorMessage;
        this.areaOfSightName = areaOfSightName;
    }
};

export default function SpatialQueriesFooter() {
    const dispatch = useDispatch();
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const queryResults: QueryResult[] = useSelector((state: AppState) => state.mapWorldTreeReducer.queryResultsTable)
    const [selectedQueryRow, setSelectedQueryRow] = useState(null);

    useEffect(() => {
        return () => {
            dispatch(resetQueryResultsTableRow())
        }
    }, [])

    let queryResultsTableColumns = [
        { field: 'index', header: '.No' },
        { field: 'spatialQueryName', header: 'Function Name' },
        { field: 'isAsync', header: 'Is Async' },
        { field: 'errorMessage', header: 'Error Message' },
        { field: 'areaOfSightName', header: 'Area Of Sight Name' },
    ]
    const getColumnTemplate = (rowData: QueryResult, column: any, i: number) => {
        switch (column.field) {
            case 'isAsync':
                return <Checkbox checked={rowData.isAsync} />
            default:
                return rowData[column.field];
        }
    }
    const handleRowClick = (e: DataTableRowClickEvent) => {
        runCodeSafely(() => {
            let selectedRow = e.data;
            if (_.isEqual(selectedQueryRow, selectedRow)) {
                setSelectedQueryRow(null)
            }
            else {
                setSelectedQueryRow(selectedRow);
                selectedRow.updateQueryParamsFuncCB && selectedRow.updateQueryParamsFuncCB(...selectedRow.queryParams);
            }
        }, 'SpatialQueriesFooter.handleRowClick')
    }

    return <Fieldset legend="Query results">
        <div style={{ width: `${globalSizeFactor * 100}vh` }}>
            <DataTable scrollable scrollHeight={`${globalSizeFactor * 20}vh`} style={{ width: '99%', paddingTop: `${globalSizeFactor * 1}vh` }} value={queryResults} selectionMode="single"
                selection={selectedQueryRow} dataKey="index" onRowClick={handleRowClick}>
                {queryResultsTableColumns.map((col, i) => {
                    return <Column key={col.field} field={col.field} header={col.header} body={(...args) => getColumnTemplate(...args, i)} />;
                })}
            </DataTable>
        </div>
    </Fieldset >
}