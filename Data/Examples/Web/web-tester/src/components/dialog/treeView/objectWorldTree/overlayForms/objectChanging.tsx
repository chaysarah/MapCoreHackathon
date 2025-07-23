import { Column } from "primereact/column";
import { DataTable } from "primereact/datatable";
import { Fieldset } from "primereact/fieldset";
import { ChangeEvent, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { AppState } from "../../../../../redux/combineReducer";
import { InputText } from "primereact/inputtext";
import { Button } from "primereact/button";
import { Dropdown } from "primereact/dropdown";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";
import { setShowDialog } from "../../../../../redux/ObjectWorldTree/objectWorldTreeActions";
import { hideFormReasons } from "../../../../shared/models/tree-node.model";
import { DialogTypesEnum } from "../../../../../tools/enum/enums";
import { OverlayFormTabInfo } from "./overlayForm";
import { Properties } from "../../../dialog";
import TreeNodeModel from "../../../../shared/models/tree-node.model"
import generalService from "../../../../../services/general.service";
import dialogStateService from "../../../../../services/dialogStateService";

export class ObjectChangingPropertiesState implements Properties {
    //object changing fields
    objectsDropDownValue: number | null;
    objectsLocations: any[];

    static getDefault(props: any): ObjectChangingPropertiesState {
        return {
            objectsDropDownValue: null,
            objectsLocations: props.props.tabInfo.currentOverlay.children.map((obj: TreeNodeModel, ind: number): any => { return { index: ind, object: obj.label, point: null, x: '', y: '', z: '' } }).flat(),
        }
    }
}
export class ObjectChangingProperties extends ObjectChangingPropertiesState {
    buttonClick: boolean;
    allObjectsOfOverlay: TreeNodeModel[];
    selectedObjIndex: number | null;

    static getDefault(props: any): ObjectChangingProperties {
        let stateDefaults = super.getDefault(props);
        let defaults: ObjectChangingProperties = { 
            ...stateDefaults,
            buttonClick: false,
            allObjectsOfOverlay: props.props.tabInfo.currentOverlay.children,
            selectedObjIndex: null, 
        }

        return defaults;
    }
}

export default function ObjectChanging(props: {tabInfo: OverlayFormTabInfo}) {
    const dispatch = useDispatch();
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const cursorPos = useSelector((state: AppState) => state.mapWindowReducer.cursorPos);
    const [isMountedUseEffect, setIsMountedUseEffect] = useState({
        cursorPos: false,
    })

    useEffect(() => 
    {
        runCodeSafely(() => {
            // initialize first time the tab is loaded only
            if(!props.tabInfo.properties.objectChangingProperties) {
                let defaultProperties = 
                props.tabInfo.setInitialStatePropertiesCallback("objectChangingProperties", null, ObjectChangingPropertiesState.getDefault({props}));
                props.tabInfo.setPropertiesCallback("objectChangingProperties", null, ObjectChangingProperties.getDefault({props})); 
            }
        }, 'OverlayForm/ObjectChanging.useEffect');
    }, [])
    
    const locationColumns = [
        { field: 'object', header: 'Object' },
        { field: 'point', header: 'Point' },
        { field: 'x', header: 'X' },
        { field: 'y', header: 'Y' },
        { field: 'z', header: 'Z' },
    ];
    // const privatePropertiesColumn = [
    //     { field: 'object', header: 'Object' },
    //     { field: 'id', header: 'ID' },
    //     { field: 'type', header: 'Type' },
    //     { field: 'value', header: 'Value' },
    // ]

    useEffect(() => {
        runCodeSafely(() => {
            if(isMountedUseEffect.cursorPos) {
                if (props.tabInfo.properties.objectChangingProperties?.buttonClick == true) {
                    let objLocationTable = [...props.tabInfo.properties.objectChangingProperties?.objectsLocations]
                    let newSelectedRow = { ...objLocationTable[props.tabInfo.properties.objectChangingProperties?.selectedObjIndex] };
                    newSelectedRow.x = cursorPos.Value.x;
                    newSelectedRow.y = cursorPos.Value.y;
                    newSelectedRow.z = cursorPos.Value.z;
                    objLocationTable[props.tabInfo.properties.objectChangingProperties?.selectedObjIndex] = newSelectedRow;
                    props.tabInfo.setPropertiesCallback("objectChangingProperties", "buttonClick", false);
                    props.tabInfo.setPropertiesCallback("objectChangingProperties", "objectsLocations", objLocationTable);
                    props.tabInfo.setCurrStatePropertiesCallback("objectChangingProperties", "objectsLocations", objLocationTable);
                }           
            }
            else {
                setIsMountedUseEffect({...isMountedUseEffect, cursorPos: true})
            }
        }, 'objectChanging.useEffect => cursorPos')
    }, [cursorPos])

    // useEffect(() => {
    //     runCodeSafely(() => {
    //         props.tabInfo.setApplyCallBack("ObjectChanging", applyAll);
    //     }, 'OverlayForm/ObjectChanging.useEffect => properties');
    // }, [props.tabInfo.properties])

    const choosePoint = (rowData: { index: number, object: string, x: number, y: number, z: number }) => {
        runCodeSafely(() => {
            dispatch(setShowDialog({ hideFormReason: hideFormReasons.CHOOSE_POINT, dialogType: DialogTypesEnum.objectWorldTree }))
            props.tabInfo.setPropertiesCallback("objectChangingProperties", "buttonClick", true);
            props.tabInfo.setPropertiesCallback("objectChangingProperties", "selectedObjIndex", rowData.index);
        }, 'objectChanging.choosePoint => onClick')
    }
    const onPointCellChange = (e: ChangeEvent<HTMLInputElement>, rowData: { index: number, object: string, x: number, y: number, z: number }, column: any) => {
        runCodeSafely(() => {
            let objLocationTable = [...props.tabInfo.properties.objectChangingProperties?.objectsLocations]
            let newSelectedRow = { ...objLocationTable[props.tabInfo.properties.objectChangingProperties?.selectedObjIndex] };
            newSelectedRow[column.field] = parseInt(e.target.value) ? e.target.value : newSelectedRow[column.field];
            objLocationTable[rowData.index] = newSelectedRow;
            props.tabInfo.setPropertiesCallback("objectChangingProperties", "objectsLocations", objLocationTable);
            props.tabInfo.setCurrStatePropertiesCallback("objectChangingProperties", "objectsLocations", objLocationTable);
        }, 'objectChanging.onPointCellChange => onClick')
    }
    const getColumnTemplate = (rowData: { index: number, object: string, x: number, y: number, z: number }, column: any) => {
        switch (column.field) {
            case 'object':
                return <span>{rowData[column.field]}</span>
            case 'point':
                return <Button label='Select point' onClick={(e) => { choosePoint(rowData) }} />
            default:
                return <InputText style={{ width: `${globalSizeFactor *1.5 * 8}vh` }} type="text" onChange={(e) => { onPointCellChange(e, rowData, column) }} value={rowData[column.field]?? ""} />;
        }
    }
    const handleSetEachObjectLocationPointOK = () => {
        runCodeSafely(() => {
            let objectsArray: MapCore.IMcObject[] = props.tabInfo.properties.objectChangingProperties?.allObjectsOfOverlay.map(obj => obj.nodeMcContent);
            let vertexsArr: MapCore.SMcVector3D[] = [];
            props.tabInfo.properties.objectChangingProperties?.objectsLocations.forEach((objLocation) => {
                let vertex = new MapCore.SMcVector3D(parseInt(objLocation.x), parseInt(objLocation.y), parseInt(objLocation.z));
                vertexsArr = [...vertexsArr, vertex];
            })

            runMapCoreSafely(() => {
                MapCore.IMcObject.SetEachObjectLocationPoint(objectsArray, vertexsArr);
            }, 'objectChanging.handleSetEachObjectLocationPointOK => ', true)
            dialogStateService.applyDialogState(["objectChangingProperties.objectsLocations"]);
        }, 'objectChanging.handleSetEachObjectLocationPointOK => onClick')
    }

    return (
        <div>
            <Fieldset className="form__space-around form__column-fieldset" legend="Set Each Object Location Point">
                <DataTable size='small' style={{ padding: `${globalSizeFactor * 1}vh` }} value={props.tabInfo.properties.objectChangingProperties?.objectsLocations} editMode="cell" tableStyle={{ minWidth: `${globalSizeFactor *1.5 * 30}vh`, maxWidth: `${globalSizeFactor *1.5 * 30}vh` }}>
                    {locationColumns.map(({ field, header }) => {
                        return <Column key={field} field={field} header={header} style={{ width: '50%', minHeight: `${globalSizeFactor * 4.5}vh` }} body={getColumnTemplate} />;
                    })}
                </DataTable>
                <span style={{ display: 'flex', justifyContent: 'right' }}>
                    <Button style={{ width: `${globalSizeFactor *1.5 * 5}vh`, right: `${globalSizeFactor * 1.5}vh` }} label='OK' onClick={handleSetEachObjectLocationPointOK} />
                </span>
            </Fieldset>
            {/* <Fieldset className="form__space-around form__column-fieldset" legend="Set Each Object Private Property">
                <div style={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between' }}>
                    <Dropdown name='objectsDropDownValue' value={props.tabInfo.properties.objectChangingProperties?.objectsDropDownValue} onChange={saveData} options={props.tabInfo.properties.objectChangingProperties?.allObjectsOfOverlay} optionLabel="label" />
                    <Button label='Open Private Properties Form' />
                </div>
                <DataTable size='small' style={{ padding: `${globalSizeFactor * 1}vh` }} value={props.tabInfo.properties.objectChangingProperties?.objectsLocations} editMode="cell" tableStyle={{ minWidth: `${globalSizeFactor *1.5 * 30}vh`, maxWidth: `${globalSizeFactor *1.5 * 30}vh` }}>
                    {privatePropertiesColumn.map(({ field, header }) => {
                        return <Column key={field} field={field} header={header} style={{ width: '50%', minHeight: `${globalSizeFactor * 4.5}vh` }} body={getColumnTemplate} />;
                    })}
                </DataTable>
                <span style={{ display: 'flex', justifyContent: 'right' }}>
                    <Button style={{ width: `${globalSizeFactor *1.5 * 5}vh`, right: `${globalSizeFactor * 1.5}vh` }} label='OK' />
                </span>
            </Fieldset> */}

        </div>
    )
}