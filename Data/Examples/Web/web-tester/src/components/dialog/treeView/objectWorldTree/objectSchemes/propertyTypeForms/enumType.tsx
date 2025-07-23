import { Button } from "primereact/button";
import { Dropdown } from "primereact/dropdown";
import { ChangeEvent, useEffect, useState } from "react";
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import { AppState } from "../../../../../../redux/combineReducer";
import { useSelector } from "react-redux";
import TreeNodeModel, { objectWorldNodeType } from '../../../../../shared/models/tree-node.model'
import { MultiSelect } from "primereact/multiselect";
import { runCodeSafely } from "../../../../../../common/services/error-handling/errorHandler";

export default function EnumType(props: { id: number, value: any, onOk: (newValue: number) => void }) {

    let selectedNodeInTree: TreeNodeModel = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [objectSchemes, setObjectSchemes] = useState<MapCore.IMcObjectScheme>(selectedNodeInTree.nodeMcContent)
    let [value, setValue] = useState(props.value);
    const [enumDetails, setEnumDetails] = useState(null);
    let [isEnumFlag, setisEnumFalg] = useState(false);
    let [MaxLength, setMaxLength] = useState(`${globalSizeFactor *1.5 * 5}vh`);

    useEffect(() => {
        runCodeSafely(() => {
            let TheEnumName = "MapCore." + objectSchemes.GetEnumPropertyActualType(props.id)
            console.log(TheEnumName);
            let TheEnum = eval("MapCore." + objectSchemes.GetEnumPropertyActualType(props.id))
            let enumDetailsList = getEnumDetailsList(TheEnum);
            setEnumDetails(enumDetailsList)
            if (TheEnumName.toLowerCase().includes("flag")) {
                setisEnumFalg(true)
                let r = enumDetailsList.filter(f => (props.value & f.code) == f.code)
                setValue(r)
            }
            else
                setValue(enumDetailsList.find(f => f.code == props.value))
        }, "NumberType.useEffect ")

    }, [])

    useEffect(() => {
        let max = 0;
        enumDetails?.forEach((element: { name: string | any[]; }) => {
            if (element.name.length > max) {
                max = element.name.length
            }
            setMaxLength((4.5 + max * 1.2) + "vh")
        });
    }, [enumDetails])
    const onOk = () => {
        runCodeSafely(() => {
            let val = value;
            val = value.code
            if (isEnumFlag) {
                val = 0
                for (let index: number = 0; index < value.length; index++) {
                    val |= value[index]!.code;
                }
            }
            props.onOk(val)
        }, "NumberType.onOk")
    }
    //  style={{width:'auto!important'}} 
    return (<>
        <div style={{ display: "flex", flexDirection: "column" }}>
            <div><label>Value: </label>
                {!isEnumFlag && <Dropdown maxLength={400} value={value} onChange={(event) => { setValue(event.target.value) }}
                    options={enumDetails} optionLabel="name" style={{ width: `${MaxLength}` }}/>}
                {isEnumFlag && <MultiSelect value={value} onChange={(event) => { setValue(event.target.value) }}
                    options={enumDetails} optionLabel="name"/>}
            </div>
            <br></br>
            <Button label="OK" onClick={onOk}></Button>
        </div>
    </>
    );
}