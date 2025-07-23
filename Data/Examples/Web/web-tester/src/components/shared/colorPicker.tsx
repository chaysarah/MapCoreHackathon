import { InputNumber } from 'primereact/inputnumber';
import React, { useEffect, useState } from 'react';
import { useSelector } from 'react-redux';
import { AppState } from '../../redux/combineReducer';
import { runCodeSafely } from '../../common/services/error-handling/errorHandler';
import _ from 'lodash';

const ColorPickerCtrl = (props: { value: { r: number, g: number, b: number, a?: number } | any, onChange: (event: any) => void, alpha?: boolean, name?: string, id?: string, style?: { [key: string]: any }, className?: string }) => {
    const rgbToHex = (rgb) => {
        return rgb ? `#${((1 << 24) + (rgb.r << 16) + (rgb.g << 8) + rgb.b).toString(16).slice(1)}` : '#000000';
    };

    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [color, setColor] = useState(rgbToHex(props.value));
    const [alpha, setAlpha] = useState(props.alpha ? props.value?.a : null);

    useEffect(() => {
        runCodeSafely(() => {
            let hexRGB = rgbToHex(props.value);
            let localAlpha = props.alpha ? props.value?.a : null;
            if (!_.isEqual(hexRGB, color) || !_.isEqual(alpha, localAlpha)) {
                setColor(hexRGB)
                setAlpha(localAlpha)
            }
        }, 'colorPickerCtrl => useEffect')
    }, [props.value])

    const handleRGBChange = (event) => {
        setColor(event.target.value);
        let rgbVal = hexToRgb(event.target.value);
        if (!_.isEqual(props.value, rgbVal)) {
            let customEvent = { value: rgbVal, target: { value: rgbVal, name: props.name } }
            props.onChange && props.onChange(customEvent);
        }
    };
    const handleAlphaChange = (event) => {
        setAlpha(event.value)
        let customEvent = { value: hexToRgb(color, event.value), target: { value: hexToRgb(color, event.value), name: props.name } }
        props.onChange && props.onChange(customEvent);
    };
    const hexToRgb = (hex, a?) => {
        hex = hex.replace('#', '');
        const r = parseInt(hex.substring(0, 2), 16);
        const g = parseInt(hex.substring(2, 4), 16);
        const b = parseInt(hex.substring(4, 6), 16);
        let finalObj = props.alpha ? { r: r, g: g, b: b, a: a || alpha } : { r: r, g: g, b: b };
        return finalObj;
    }
    let inputStyle = {
        border: 'none',
        inlineSize: `${globalSizeFactor * 3.5}vh`,
        blockSize: `${globalSizeFactor * 2.8}vh`,
        padding: 0,
    }

    return (
        props.alpha ?
            <div style={props.style} className={`form__flex-and-row-between form__items-center ${props.className}`}>
                <input
                    style={inputStyle}
                    id={props.id}
                    type="color"
                    name={props.name}
                    value={color}
                    onChange={handleRGBChange}
                />
                <div>Alpha:</div>
                <InputNumber className="form__medium-width-input" value={alpha} onValueChange={handleAlphaChange} mode="decimal" />
            </div> :
            <input
                style={inputStyle}
                id={props.id}
                type="color"
                name={props.name}
                value={color}
                onChange={handleRGBChange}
            />
    );
};

export default ColorPickerCtrl;