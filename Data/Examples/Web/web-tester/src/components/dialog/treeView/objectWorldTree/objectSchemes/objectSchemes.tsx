import { useDispatch, useSelector } from "react-redux";
import { AppState } from "../../../../../redux/combineReducer";
import TreeNodeModel, { objectWorldNodeType } from '../../../../shared/models/tree-node.model'
import { ChangeEvent, useEffect, useState } from "react";
import { InputText } from "primereact/inputtext";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";
import { Button } from "primereact/button";
import { InputNumber } from "primereact/inputnumber";
import { setDialogType } from "../../../../../redux/MapCore/mapCoreAction";
import { DialogTypesEnum } from "../../../../../tools/enum/enums";
import { setTypeObjectWorldDialogSecond } from "../../../../../redux/ObjectWorldTree/objectWorldTreeActions";
import PropertiesIDList from "./propertiesIDList/propertiesIDList";
import objectWorldTreeService from "../../../../../services/objectWorldTree.service";

export default function ObjectSchemes() {
    let selectedNodeInTree: TreeNodeModel = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);
    const [objectSchemes, setObjectSchemes] = useState<MapCore.IMcObjectScheme>(selectedNodeInTree.nodeMcContent)
    const dispatch = useDispatch();
    let [FormData,  setObjectsSchemesFormData] = useState({
        schemeID: objectWorldTreeService.getObjIDHeader(objectSchemes.GetID()),
        schemeName: objectSchemes.GetName()
    })

    const saveData = (event: any) => {
        runCodeSafely(() => {
            setObjectsSchemesFormData({ ...FormData, [event.target.name]: event.target.value });
        }, "ObjectSchemes.saveData")
    }

    const onClickApplay = () => {
        runCodeSafely(() => {
            runMapCoreSafely(() => {
                objectSchemes.SetID(objectWorldTreeService.getObjIDValue(FormData.schemeID))
                objectSchemes.SetName(FormData.schemeName)
            }, "ObjectSchemes.onClickApplay => SetID/SetName", true)
        }, "ObjectSchemes.onClickApplay")
    }
    useEffect(() => {
        runCodeSafely(() => {
            setObjectsSchemesFormData({
                ...FormData,
                schemeID: selectedNodeInTree.nodeMcContent.GetID(),
                schemeName: selectedNodeInTree.nodeMcContent.GetName()
            })
        }, "ObjectSchemes.useEffect => selectedNodeInTree")
    }, [selectedNodeInTree])

    return (
        <div>
            <h4> ObjectSchemes</h4>
            <label>Scheme ID: </label>
            <InputText name="schemeID" value={FormData.schemeID} onChange={saveData}/>
            <Button label="Apply" onClick={onClickApplay}></Button>
            <label>Scheme Name: </label>
            <InputText name="schemeName" value={FormData.schemeName} onChange={saveData}></InputText>
            <Button label="Apply" onClick={onClickApplay}></Button>
            <br></br>
            <Button label="-->" onClick={() => {
                dispatch(setTypeObjectWorldDialogSecond({
                    secondDialogHeader: "Private Properties Of ObjectScheme",
                    secondDialogComponent: <PropertiesIDList propObject={false} />
                }))
            }
            } ></Button>
        </div>
    )
}