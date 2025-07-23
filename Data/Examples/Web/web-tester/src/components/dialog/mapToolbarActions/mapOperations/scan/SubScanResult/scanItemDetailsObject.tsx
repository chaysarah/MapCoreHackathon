import { Column } from "primereact/column";
import { DataTable } from "primereact/datatable";
import { useEffect } from "react";
import React, { useState } from 'react';
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import { Button } from "primereact/button";
import generalService from "../../../../../../services/general.service";
import 'xlsx';
import { utils, write } from 'xlsx';
import { Dropdown } from "primereact/dropdown";
import { Checkbox } from "primereact/checkbox";
import { runCodeSafely } from "../../../../../../common/services/error-handling/errorHandler";
import { ScanService } from 'mapcore-lib';

export default function ScanItemDetailsObject(props: { target: MapCore.IMcSpatialQueries.STargetFound }) {

    const [locationPoints, setLocationPoints] = useState([]);
    const [locationArr, setLocationArr] = useState([]);
    const [locationsIndex, setLocationsIndex] = useState(null);
    const [RelativeToDTM, setRelativeToDTM] = useState(false);

    useEffect(() => {
        runCodeSafely(() => {
            let numLocations = props.target.ObjectItemData.pObject.GetNumLocations()
            for (let index = 0; index < numLocations; index++) {
                let o = { name: "- " + index, code: index }
                if (index == 0)
                    setLocationsIndex(o)
                locationArr.push(o)
            }
            getLocationPoints(0);
        }, "ScanItemDetailsObject.useEffect")
    }, [])

    const getLocationPoints = (location: number) => {
        runCodeSafely(() => {
            let object: MapCore.IMcObject = props.target.ObjectItemData.pObject;
            let locationPointsArr: MapCore.SMcVector3D[] = object.GetLocationPoints(location);
            locationPointsArr = locationPointsArr.map((p, index) => { return { ...p, index: index + 1 } })
            setLocationPoints(locationPointsArr);
            setRelativeToDTM(ScanService.GetObjectLocationData(location, props.target));
        }, "ScanItemDetailsObject.getLocationPoints")
    }

    function exportPoint(): void {
        runCodeSafely(() => {
            const workbook = utils.book_new();
            let data = [];
            data.push(["X", "Y", "Z"])
            for (let index = 0; index < locationPoints.length; index++) {
                const element: MapCore.SMcVector3D = locationPoints[index];
                let row = [element.x, element.y, element.z]
                data.push(row)
            }
            const worksheet = utils.aoa_to_sheet(data);
            utils.book_append_sheet(workbook, worksheet, 'Sheet1');
            const excelFile = write(workbook, { bookType: 'xlsx', type: 'array' });
            generalService.createAnddownloadFile("exportPoint", excelFile, '.csv')
        }, "ScanItemDetailsObject.exportPoint")
    }

    return (<>
        {props.target && <div style={{ display: 'flex', flexDirection: 'column' }}>
            <label>Object id :  {props.target.ObjectItemData.pObject?.GetID()}</label>
            <label>Item id :  {props.target.ObjectItemData.pItem?.GetID()}</label>
            <label>coordinate System :  {getEnumValueDetails(props.target.eIntersectionCoordSystem, getEnumDetailsList(MapCore.EMcPointCoordSystem)).name}</label>
            <div >
                <label className='form__lower-margin' >Locations Index:</label>
                <Dropdown value={locationsIndex} onChange={(e) => { setLocationsIndex(e.target.value); getLocationPoints(e.target.value.code) }} options={locationArr} optionLabel="name" className="form__lower-margin" />
                <div className='form__lower-margin disabledDiv'>
                    <Checkbox inputId="Checkbox3" name="AddStaticObjectContours" onChange={(event) => { setRelativeToDTM(event.target.checked) }} checked={RelativeToDTM} />
                    <label htmlFor="Checkbox3">Is Relative To DTM</label>
                </div></div>
            <h4>Object Location</h4>
            <DataTable paginator rows={5} value={locationPoints} tableStyle={{ minWidth: '50rem' }} size='small'>
                <Column field="index"></Column>
                <Column field="x" header="x"></Column>
                <Column field="y" header="y"></Column>
                <Column field="z" header="z"></Column>
            </DataTable>
            {locationPoints && <label>Namber of points:  {locationPoints.length}</label>}

        </div>}
        <Button onClick={exportPoint}>Export Point</Button>
    </>)
}