import { InputText } from "primereact/inputtext";
import { useEffect, useState } from "react";

export default function InputMaxNumber(props: { value: number, maxValue: number, getUpdatedMaxInput?: (value: number) => void, style?: any, className?: string, name?: string, disabled?: boolean, id?: string }) {

    let [value, setValue] = useState(props.value == props.maxValue ? 'MAX' : `${props.value}`);

    useEffect(() => {
        setValue(props.value == props.maxValue ? 'MAX' : `${props.value}`);
    }, [props.value])

    const handleInputChange = (e) => {
        if (!isNaN(parseInt(e.target.value))) {//numeric number value
            setValue(e.target.value);
        }
        else if (['M', 'MA', 'MAX', ''].includes(e.target.value)) {
            setValue(e.target.value);
        }
    }
    const handleInputBlur = (e) => {
        let finalVal;
        if (!isNaN(parseInt(value))) {//numeric number value
            finalVal = parseInt(value);
        }
        else if (value == 'MAX') {// 'MAX' string value
            finalVal = props.maxValue;
        }
        else {//'M', 'MA', '' value
            //send the old value
            finalVal = props.value;
        }

        setValue(finalVal == props.maxValue ? 'MAX' : `${finalVal}`);
        props.getUpdatedMaxInput(finalVal);
    }

    return <InputText
        style={props.style}
        className={props.className}
        id={props.id}
        value={value}
        name={props.name}
        onBlur={handleInputBlur}
        onChange={handleInputChange}
        disabled={props.disabled}
    />
}