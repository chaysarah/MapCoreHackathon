import { useSelector } from "react-redux";
import { AppState } from "../../../../../redux/combineReducer";
import TreeNodeModel, { objectWorldNodeType } from '../../../../shared/models/tree-node.model'
import { ChangeEvent, useEffect, useState } from "react";
import { InputText } from "primereact/inputtext";
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";
import { Button } from "primereact/button";
import { InputNumber } from "primereact/inputnumber";
import objectWorldTreeService from "../../../../../services/objectWorldTree.service";

export default function ObjectLocationForm() {
    let selectedNodeInTree: TreeNodeModel = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);
    const [objectLocation, setObjectLocation] = useState<MapCore.IMcObjectLocation>(selectedNodeInTree.nodeMcContent)

    let [FormData, setObjectsFormData] = useState({
        locationID: objectWorldTreeService.getObjIDHeader(objectLocation.GetID()),
        locationName: objectLocation.GetName()
    })

    const saveData = (event: any) => {
        runCodeSafely(() => {
            setObjectsFormData({ ...FormData, [event.target.name]: event.target.value });
        }, "arc.saveData => onChange")
    }

    const onClickApply = () => {
        objectLocation.SetID(objectWorldTreeService.getObjIDValue(FormData.locationID))
        objectLocation.SetName(FormData.locationName)

    }

    return (
        <div>
            <h4> Object Location</h4>
            <label>Object Location ID: </label>
            <InputText name="locationID" value={FormData.locationID} onChange={saveData} />
            <Button label="Apply" onClick={onClickApply}></Button>
            <label>Object Location Name: </label>
            <InputText name="locationName" value={FormData.locationName} onChange={saveData}></InputText>
            <Button label="Apply" onClick={onClickApply}></Button>
        </div>
    )
}