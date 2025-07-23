import { Checkbox } from "primereact/checkbox";
import { useEffect, useState } from "react";
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";
import _ from "lodash";
import { useSelector } from "react-redux";
import { AppState } from "../../../../../redux/combineReducer";

export default function CancelScaleMode(props: {
    cancelScaleModeNum: number,
    label: string,
    sendCheckboxesArr: (bitFlag: number) => void,
}) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    const getScaleModeArr = () => {
        let arr: { MASK: number, checked: boolean }[] = [{ MASK: 1, checked: false }];

        for (let i = 1; i < 10; i++) {
            arr = [...arr, { MASK: (arr[i - 1].MASK * 2), checked: false }]
        }
        return arr;
    }
    const [checkboxesArr, setCheckboxesArr] = useState(getScaleModeArr())

    useEffect(() => {
        runCodeSafely(() => {
            let updatedCheckboxesArr = checkboxesArr.map(f => {
                if ((props.cancelScaleModeNum & f.MASK) == f.MASK) {
                    return { MASK: f.MASK, checked: true }
                }
                else {
                    return { MASK: f.MASK, checked: false }
                }
            });
            if (!_.isEqual(updatedCheckboxesArr, checkboxesArr)) {
                setCheckboxesArr(updatedCheckboxesArr);
            }
        }, 'cancelScaleMode.useEffect => props.cancelScaleModeNum')
    }, [props.cancelScaleModeNum])
    useEffect(() => {
        runCodeSafely(() => {
            let bitFlag = 0;
            checkboxesArr.forEach(checkbox => {
                if (checkbox.checked === true) {
                    bitFlag = bitFlag | checkbox.MASK;
                }
            })
            console.log(bitFlag);
            props.sendCheckboxesArr(bitFlag)
        }, 'cancelScaleMode.useEffect => checkboxArr')
    }, [checkboxesArr])


    return <div style={{ padding: `${globalSizeFactor * 1}vh` }}>
        <label htmlFor="showGeo" className="ml-2" style={{ marginLeft: '0.5%' }}>{props.label} :</label>
        <div style={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-around', width: '100%' }}>
            {checkboxesArr.map((MASK, i) => {
                return <div key={i} style={{ display: 'flex', flexDirection: 'column' }}>
                    <label>{i}</label>
                    <Checkbox name={i.toString()} onChange={(e) => {
                        let x = [...checkboxesArr];
                        x[i].checked = e.target.checked;
                        setCheckboxesArr(x);
                    }
                    } checked={checkboxesArr[i].checked}></Checkbox>
                </div>
            })}
        </div>
    </div>
}