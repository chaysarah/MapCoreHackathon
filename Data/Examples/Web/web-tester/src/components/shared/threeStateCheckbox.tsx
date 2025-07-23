import { useEffect, useState } from "react";
import { MultiStateCheckbox } from 'primereact/multistatecheckbox';

import { runCodeSafely } from "../../common/services/error-handling/errorHandler";

export default function ThreeStateCheckbox(props: {
    value: boolean,
    onChange?: (e) => void,
    name?: string,
    id?: string,
    disabled?: boolean,
}) {
    const [value, setValue] = useState('indeterminate');
    const options = [
        { value: 'checked', icon: 'pi pi-check' },
        { value: 'indeterminate', icon: 'pi pi-minus' }
    ];
    let valuesMap = new Map([
        ['indeterminate', null],
        ['checked', true],
        [null, false]
    ])

    useEffect(() => {
        runCodeSafely(() => {
            const foundEntry = Array.from(valuesMap.entries()).find(([key, value]) => value === props.value);
            setValue(foundEntry ? foundEntry[0] : undefined);
        }, 'ThreeStateCheckbox.useEffect');
    }, [props.value]);

    const handleCheckboxChange = (e) => {
        runCodeSafely(() => {
            setValue(e.value);
            e.target.value = valuesMap.get(e.value);
            props.onChange && props.onChange(e);
        }, 'ThreeStateCheckbox.handleCheckboxChange');
    }

    return <MultiStateCheckbox
        value={value}
        onChange={handleCheckboxChange}
        options={options}
        optionValue="value"
        name={props.name}
        id={props.id}
        disabled={props.disabled}
    />
}

