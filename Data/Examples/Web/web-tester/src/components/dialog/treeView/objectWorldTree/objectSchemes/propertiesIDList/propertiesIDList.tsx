import { useSelector } from "react-redux";
import { useEffect, useState } from "react";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { Button } from "primereact/button";
import { Checkbox, CheckboxChangeEvent } from "primereact/checkbox";
import { Dialog } from "primereact/dialog";

import { getEnumDetailsList } from 'mapcore-lib';
import './propertiesIDList.css';
import EnumType from "../propertyTypeForms/enumType";
import ColorArrayType from "../propertyTypeForms/colorArrayType";
import Vector2DArrayType from "../propertyTypeForms/vector2DArrayType";
import EditTextureType from "../propertyTypeForms/editTextureType";
import StringType from "../propertyTypeForms/stringType";
import IntArray from "../propertyTypeForms/intArrayType";
import SubItemArrayType from "../propertyTypeForms/subItemArrayType";
import Vector3DArrayType from "../propertyTypeForms/vector3DArrayType";
import FontType from "../propertyTypeForms/fontType";
import BoolType from "../propertyTypeForms/boolType";
import NumberType from "../propertyTypeForms/numberType";
import ColorType from "../propertyTypeForms/colorType";
import Vector3DType from "../propertyTypeForms/vector3DType";
import Vector2DType from "../propertyTypeForms/vector2DType";
import TreeNodeModel, { hideFormReasons } from '../../../../../shared/models/tree-node.model'
import { AppState } from "../../../../../../redux/combineReducer";
import { runCodeSafely } from "../../../../../../common/services/error-handling/errorHandler";
import { runMapCoreSafely } from "../../../../../../common/services/error-handling/errorHandler";
import { DialogTypesEnum } from "../../../../../../tools/enum/enums";

export default function PropertiesIDList(props: { propObject?: boolean }) {
    let selectedNodeInTree: TreeNodeModel = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [objectOrScheme, setObjectSchemes] = useState(selectedNodeInTree.nodeMcContent)
    const [objectSchemesAllFunc, setobjectSchemesAllFunc] = useState<MapCore.IMcObjectScheme>(Object.getPrototypeOf(objectOrScheme))
    const showDialog: { hideFormReason: hideFormReasons, dialogType: DialogTypesEnum } = useSelector((state: AppState) => state.objectWorldTreeReducer.showDialog);


    let [FormData, setFormData] = useState({
        arrPropertyID: [],
        byName: false
    })
    useEffect(() => {
        const root = document.documentElement;
        let pixelWidth = window.innerHeight * 0.3 * globalSizeFactor;
        root.style.setProperty('--props-id-list-dialog-width', `${pixelWidth}px`);
        buildTableProps()
    }, [])
    const buildTableProps = () => {
        runCodeSafely(() => {
            let arrPropertyID: MapCore.IMcProperty.SPropertyNameIDType[];
            runMapCoreSafely(() => {
                arrPropertyID = objectOrScheme.GetProperties()
            }, "PropertiesIDList.buildTableProps => getPropertyValue", true)
            let arr = arrPropertyID.map((prop: MapCore.IMcProperty.SPropertyNameIDType, i) => {
                let nameType = prop.eType.constructor.name.substring(prop.eType.constructor.name.lastIndexOf("EPT") + 4)
                let val = getPropertyValue(prop, nameType);
                let IsPropertyDefault;
                if (props.propObject) {
                    runMapCoreSafely(() => {
                        IsPropertyDefault = (objectOrScheme as any as MapCore.IMcObject).IsPropertyDefault(prop.uID);
                    }, "PropertiesIDList.buildTableProps => IsPropertyDefault", true)
                }
                return {
                    ...prop,
                    index: i,
                    propertyValue: val,
                    type: nameType,
                    isChanged: false,
                    toReset: false,
                    IsPropertyDefault: IsPropertyDefault
                }
            })
            setFormData({ ...FormData, arrPropertyID: arr })
        }, "PropertiesIDList.buildTableProps")
    }

    const getPropertyValue = (prop: any, nameType: string) => {
        let NameOrID;
        if (FormData.byName)
            NameOrID = "\"" + prop.strName + "\"";
        else
            NameOrID = prop.uID;

        let typeString = nameType.toLowerCase()
        if (nameType.includes("ARRAY")) {
            typeString = "array" + nameType.substring(0, nameType.indexOf("_")).toLowerCase();
            if (prop.eType == MapCore.IMcProperty.EPropertyType.EPT_UINT_ARRAY || prop.eType == MapCore.IMcProperty.EPropertyType.EPT_INT_ARRAY) {
                typeString = "array" + nameType.substring(0, nameType.indexOf("_")).toLowerCase()
            }
            if (prop.eType == MapCore.IMcProperty.EPropertyType.EPT_SUBITEM_ARRAY) {
                typeString = "array" + nameType.substring(0, nameType.indexOf("_")).toLowerCase() + "data"
            }
        }
        for (const key in objectSchemesAllFunc) {
            let method = "";
            if (props.propObject)
                method = "get" + typeString + "property";
            else
                method = "get" + typeString + "propertydefault";
            if (key.toLowerCase().includes(method)) {
                let func: string;
                if (nameType.includes("ARRAY")) {
                    func = "objectOrScheme." + key + "(" + NameOrID + ",MapCore.IMcProperty.EPropertyType." + prop.eType.constructor.name.substring(prop.eType.constructor.name.lastIndexOf("EPT")) + ")"
                }
                else
                    func = "objectOrScheme." + key + "(" + NameOrID + ")";
                let PropVal
                runMapCoreSafely(() => {
                    PropVal = eval(func);
                }, "PropertiesIDList.getPropertyValue = eval(MapCoreFunc)", true)
                return PropVal;
            }
        }
        return;
    }

    let [VDLockscheme, setVDLockscheme] = useState(false);
    let [propTypeForm, setPropTypeForm] = useState(null);

    const onOk = (newValue: any, rowData: any) => {
        runCodeSafely(() => {
            setVDLockscheme(false);
            let newArr = [...FormData.arrPropertyID];
            newArr[rowData.index].propertyValue = newValue;
            newArr[rowData.index].isChanged = true;
            newArr[rowData.index].IsPropertyDefault = false;
            setFormData({ ...FormData, arrPropertyID: newArr })
        }, "PropertiesIDList.onOk")
    }
    let [propertyIdToDialog, setpropertyIdToDialog] = useState(0);

    const openTypeForm = (rowData: any) => {

        runCodeSafely(() => {
            setpropertyIdToDialog(rowData.uID)
            switch (rowData.eType) {
                case MapCore.IMcProperty.EPropertyType.EPT_BOOL:
                    setPropTypeForm(<BoolType value={rowData.propertyValue} onOk={(newValue) => { onOk(newValue, rowData) }}></BoolType>)
                    break;
                case MapCore.IMcProperty.EPropertyType.EPT_INT:
                case MapCore.IMcProperty.EPropertyType.EPT_DOUBLE:
                case MapCore.IMcProperty.EPropertyType.EPT_UINT:
                case MapCore.IMcProperty.EPropertyType.EPT_BYTE:
                case MapCore.IMcProperty.EPropertyType.EPT_FLOAT:
                case MapCore.IMcProperty.EPropertyType.EPT_SBYTE:
                case MapCore.IMcProperty.EPropertyType.EPT_NUM:
                    setPropTypeForm(<NumberType value={rowData.propertyValue} onOk={(newValue) => { onOk(newValue, rowData) }}></NumberType>)
                    break;
                case MapCore.IMcProperty.EPropertyType.EPT_BCOLOR:
                case MapCore.IMcProperty.EPropertyType.EPT_FCOLOR:
                    setPropTypeForm(<ColorType value={rowData.propertyValue} onOk={(newValue) => { onOk(newValue, rowData) }}></ColorType>)
                    break;
                case MapCore.IMcProperty.EPropertyType.EPT_FVECTOR3D:
                case MapCore.IMcProperty.EPropertyType.EPT_VECTOR3D:
                    setPropTypeForm(<Vector3DType value={rowData.propertyValue} onOk={(newValue) => { onOk(newValue, rowData) }}></Vector3DType>)
                    break;
                case MapCore.IMcProperty.EPropertyType.EPT_FVECTOR2D:
                case MapCore.IMcProperty.EPropertyType.EPT_VECTOR2D:
                    setPropTypeForm(<Vector2DType value={rowData.propertyValue} onOk={(newValue) => { onOk(newValue, rowData) }}></Vector2DType>)
                    break;
                case MapCore.IMcProperty.EPropertyType.EPT_TEXTURE:
                    setPropTypeForm(<EditTextureType value={rowData.propertyValue} onOk={(newValue) => { onOk(newValue, rowData) }}></EditTextureType>)
                    break;
                case MapCore.IMcProperty.EPropertyType.EPT_STRING:
                    setPropTypeForm(<StringType value={rowData.propertyValue} onOk={(newValue) => { onOk(newValue, rowData) }}></StringType>)
                    break;
                case MapCore.IMcProperty.EPropertyType.EPT_INT_ARRAY:
                case MapCore.IMcProperty.EPropertyType.EPT_UINT_ARRAY:
                    setPropTypeForm(<IntArray value={rowData.propertyValue} onOk={(newValue) => { onOk(newValue, rowData) }}></IntArray>)
                    break;
                case MapCore.IMcProperty.EPropertyType.EPT_SUBITEM_ARRAY:
                    setPropTypeForm(<SubItemArrayType value={rowData.propertyValue} onOk={(newValue) => { onOk(newValue, rowData) }}></SubItemArrayType>)
                    break;
                case MapCore.IMcProperty.EPropertyType.EPT_FVECTOR3D_ARRAY:
                case MapCore.IMcProperty.EPropertyType.EPT_VECTOR3D_ARRAY:
                    setPropTypeForm(<Vector3DArrayType value={rowData.propertyValue} onOk={(newValue) => { onOk(newValue, rowData) }}></Vector3DArrayType>)
                    break;
                case MapCore.IMcProperty.EPropertyType.EPT_FONT:
                    setPropTypeForm(<FontType value={rowData.propertyValue} onOk={(newValue) => { onOk(newValue, rowData) }} ></FontType>)
                    break;
                case MapCore.IMcProperty.EPropertyType.EPT_ENUM:
                    setPropTypeForm(<EnumType id={rowData.uID} value={rowData.propertyValue} onOk={(newValue) => { onOk(newValue, rowData) }} ></EnumType>)
                    break;
                case MapCore.IMcProperty.EPropertyType.EPT_BCOLOR_ARRAY:
                    setPropTypeForm(<ColorArrayType value={rowData.propertyValue} onOk={(newValue) => { onOk(newValue, rowData) }} ></ColorArrayType>)
                    break;
                case MapCore.IMcProperty.EPropertyType.EPT_FVECTOR2D_ARRAY:
                case MapCore.IMcProperty.EPropertyType.EPT_VECTOR2D_ARRAY:
                    setPropTypeForm(<Vector2DArrayType value={rowData.propertyValue} onOk={(newValue) => { onOk(newValue, rowData) }}></Vector2DArrayType>)
                    break;
                default:
                    break;
            }
        }, "PropertiesIDList.openTypeForm")
    }
    const bodyToPropertyValue = (rowData: any): React.ReactNode => {
        if (rowData.propertyValue == null)
            return "null"
        switch (rowData.eType) {
            case MapCore.IMcProperty.EPropertyType.EPT_BCOLOR:
            case MapCore.IMcProperty.EPropertyType.EPT_FCOLOR:
                return <label>(r:{rowData.propertyValue.r}, g:{rowData.propertyValue.g}, b:{rowData.propertyValue.b}, a:{rowData.propertyValue.a})</label>
            case MapCore.IMcProperty.EPropertyType.EPT_BOOL:
                return rowData.propertyValue ? <label>True</label> : <label>False</label>
            case MapCore.IMcProperty.EPropertyType.EPT_FVECTOR3D:
            case MapCore.IMcProperty.EPropertyType.EPT_VECTOR3D:
                return <label>(X:{rowData.propertyValue.x}, Y:{rowData.propertyValue.y}, Z:{rowData.propertyValue.z})</label>
            case MapCore.IMcProperty.EPropertyType.EPT_FVECTOR2D:
            case MapCore.IMcProperty.EPropertyType.EPT_VECTOR2D:
                return <label>(X:{rowData.propertyValue.x}, Y:{rowData.propertyValue.y})</label>
            case MapCore.IMcProperty.EPropertyType.EPT_ENUM:
                let label
                runCodeSafely(() => {
                    let enumDetails = rowData.propertyValue
                    let enumFullName = "";
                    runMapCoreSafely(() => {
                        enumFullName = "MapCore." + objectOrScheme.GetEnumPropertyActualType(rowData.uID);
                    }, "PropertiesIDList.bodyToPropertyValue=> GetEnumPropertyActualType", true)
                    if (!enumFullName.toLowerCase().includes("flag")) {
                        let TheEnum = eval(enumFullName)
                        let enumDetailsList = getEnumDetailsList(TheEnum);
                        enumDetails = enumDetailsList.find(f => f.code == rowData.propertyValue);

                        label = <label>{enumDetails.name}</label>
                    }
                    else label = <label>{rowData.propertyValue}</label>
                }, "PropertiesIDList.bodyToPropertyValue")
                return label;
            case MapCore.IMcProperty.EPropertyType.EPT_TEXTURE:
                return <label>{rowData.propertyValue.constructor.name}</label>
            case MapCore.IMcProperty.EPropertyType.EPT_STRING:
                return <label>{rowData.propertyValue.astrStrings[0]}</label>
            case MapCore.IMcProperty.EPropertyType.EPT_INT_ARRAY:
            case MapCore.IMcProperty.EPropertyType.EPT_UINT_ARRAY:
                return <label>{rowData.propertyValue.aElements.toString()}</label>
            case MapCore.IMcProperty.EPropertyType.EPT_SUBITEM_ARRAY:
                return <span><label>ID: </label>
                    {rowData.propertyValue.aElements.map((e: MapCore.SMcSubItemData) => { return <label> {e.uSubItemID} </label> })}
                    <label> ,Point Start Index:  {rowData.propertyValue.aElements.map((e: MapCore.SMcSubItemData) => { return <label> {e.nPointsStartIndex} </label> })}</label>
                </span>
            case MapCore.IMcProperty.EPropertyType.EPT_FVECTOR3D_ARRAY:
                let str = "";
                {
                    rowData.propertyValue.aElements.map((item: any, index: any) => {
                        str += "(X:" + item.x + ", Y:" + item.y + ", Z:" + item.z + ")"
                    })
                }
                return <label>{str}</label>
            case MapCore.IMcProperty.EPropertyType.EPT_FONT:
                return <label>{rowData.propertyValue.constructor.name}</label>
            case MapCore.IMcProperty.EPropertyType.EPT_BCOLOR_ARRAY:
                return <div>
                    {rowData.propertyValue.aElements.map((item: any, index: any) => {
                        return <label>(r:{item.r}, g:{item.g}, b:{item.b}, a:{item.a})</label>
                    })}
                </div>
            case MapCore.IMcProperty.EPropertyType.EPT_FVECTOR2D_ARRAY:
                return <div>
                    {rowData.propertyValue.aElements.map((item: any, index: any) => {
                        return <label>(X:{item.x}, Y:{item.y})</label>
                    })}
                </div>
            default:
                return <label>{rowData.propertyValue}</label>
        }
    }
    const setChange = () => {
        runCodeSafely(() => {
            let changedPropertiesList: MapCore.IMcProperty.SVariantProperty[] = [];
            FormData.arrPropertyID.forEach(p => {
                if (p.isChanged == true) {
                    let changeItem: MapCore.IMcProperty.SVariantProperty;
                    changeItem = new MapCore.IMcProperty.SVariantProperty();
                    changeItem.uID = p.uID
                    changeItem.strName = p.strName
                    changeItem.eType = p.eType
                    changeItem.Value = p.propertyValue
                    changedPropertiesList.push(changeItem)
                }
            })
            if (props.propObject)
                runMapCoreSafely(() => { objectOrScheme.SetProperties(changedPropertiesList); }, "PropertiesIDList.setChange=>SetProperties", true)
            else
                runMapCoreSafely(() => { objectOrScheme.SetPropertyDefaults(changedPropertiesList); }, "PropertiesIDList.setChange=>SetPropertyDefaults", true)
        }, "PropertiesIDList.setChange")
    }

    const resetAll = () => {
        runCodeSafely(() => {
            runMapCoreSafely(() => {
                objectOrScheme.ResetAllProperties();
            }, "PropertiesIDList.resetAll=>ResetAllProperties", true)
            buildTableProps();
        }, "PropertiesIDList.resetAll")
    }
    const ResetSelected = () => {
        runCodeSafely(() => {
            let isAnyRowChecked = false
            let arr = FormData.arrPropertyID.map((prop, i) => {
                if (prop.toReset) {
                    runMapCoreSafely(() => { objectOrScheme.ResetProperty(prop.uID); }, "PropertiesIDList.ResetSelected=>ResetProperty", true)
                    isAnyRowChecked = true;
                    let nameType = prop.eType.constructor.name.substring(prop.eType.constructor.name.lastIndexOf("EPT") + 4)
                    let val = getPropertyValue(prop, nameType);
                    return {
                        ...prop,
                        propertyValue: val,
                        isChanged: false,
                        toReset: false,
                        IsPropertyDefault: true
                    }
                }
                else return prop
            })
            if (isAnyRowChecked) setFormData({ ...FormData, arrPropertyID: arr })
            else alert("No Row Selected")
        }, "PropertiesIDList.ResetSelected")
    }
    const toResetCheck = (e: CheckboxChangeEvent, index: number) => {
        runCodeSafely(() => {
            let newArr = [...FormData.arrPropertyID];
            newArr[index].toReset = e.target.checked
            setFormData({ ...FormData, arrPropertyID: newArr })
        }, "PropertiesIDList.toResetCheck")
    }
    return (
        <div>
            <div style={{ height: `${globalSizeFactor * 70}vh`, overflowY: "auto" }}>
                <DataTable size='small' value={FormData.arrPropertyID} >
                    <Column field="index" body={(rowData) => { return <label>{rowData.index}.</label> }}></Column>
                    <Column field="uID" header="ID"></Column>
                    <Column field="type" header="Type"></Column>
                    <Column field="strName" header="Name"></Column>
                    <Column header="Value" body={(rowData) => { return <Button label="value" onClick={() => { setVDLockscheme(true); openTypeForm(rowData) }}></Button> }} >  </Column>
                    <Column header="Property Value" body={(rowData) => { return bodyToPropertyValue(rowData) }}></Column>
                    <Column header="Is Changed" body={(rowData) => { return <Checkbox checked={rowData.isChanged}></Checkbox> }}></Column>
                    {props.propObject && <Column header="Use Default" body={(rowData) => { return <Checkbox checked={rowData.IsPropertyDefault}></Checkbox> }}></Column>}
                    {props.propObject && <Column header="To Reset" body={(rowData) => { return <Checkbox checked={rowData.toReset} onChange={(e) => { toResetCheck(e, rowData.index) }} ></Checkbox> }}></Column>}
                </DataTable>
            </div>
            <br></br>
            <div style={{ display: 'flex' }}>
                <div style={{ marginRight: '4%' }}>
                    <Checkbox onChange={(e) => { setFormData({ ...FormData, byName: e.target.checked }) }} checked={FormData.byName} ></Checkbox><label>By Name If Exists</label><br></br>
                    <Button label="Set Changed Properties" onClick={setChange}></Button>
                </div>
                {props.propObject && <div><Button onClick={ResetSelected}>Reset Each Selected Property</Button>
                    <br></br>
                    <Button onClick={resetAll}>Reset All Properties</Button></div>}
            </div>
            <Dialog onHide={() => { setVDLockscheme(false) }} visible={VDLockscheme} header={propTypeForm?.type.name}
                modal={showDialog.hideFormReason !== null ? false : true}
                className={showDialog.hideFormReason !== null ? "object-props__hidden-dialog scroll-dialog-props-id-list" : 'scroll-dialog-props-id-list'}  >
                <h4 >Property Id:  {propertyIdToDialog}</h4>
                {propTypeForm}
            </Dialog>
        </div>
    )
}