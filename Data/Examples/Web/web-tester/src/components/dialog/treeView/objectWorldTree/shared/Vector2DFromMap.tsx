// External libraries
import { useState, useEffect, ReactElement } from 'react';
import { useDispatch, useSelector } from 'react-redux';

// UI/component libraries
import { InputNumber } from "primereact/inputnumber";
import { Button } from "primereact/button";

// Project-specific imports
import { AppState } from "../../../../../redux/combineReducer";
import { DialogTypesEnum } from "../../../../../tools/enum/enums";
import { hideFormReasons } from "../../../../shared/models/tree-node.model";
import { setShowDialog } from "../../../../../redux/ObjectWorldTree/objectWorldTreeActions";

export default function Vector2DFromMap(props: {
    initValue: MapCore.SMcFVector2D,
    saveTheValue: (point: MapCore.SMcFVector2D, flagNull: boolean) => void,
    applyVector2D?: () => void, name?: string,
    lastPoint?: boolean,
    flagNull?: { x: boolean, y: boolean },
    pointCoordSystem: MapCore.EMcPointCoordSystem,
    disabledPointFromMap?: boolean
}) {

    const dispatch = useDispatch();
    const cursorPos: any = useSelector((state: AppState) => state.mapWindowReducer.cursorPos);
    const screenPos: MapCore.SMcVector3D = useSelector((state: AppState) => state.mapWindowReducer.screenPos);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const dialogTypesArr: DialogTypesEnum[] = useSelector((state: AppState) => state.mapCoreReducer.dialogTypesArr);
    let [vector2D, setVector2D] = useState({ ...props.initValue })
    const [buttonClick, setbuttonClick] = useState(false);
    const [flagNull, setFlagNull] = useState(props.flagNull ? props.flagNull : { x: true, y: true });
    const [ifInValid, setifValid] = useState<boolean>((flagNull.x || flagNull.y) && !(props.lastPoint && !(!flagNull.x || !flagNull.y)));

    useEffect(() => {
        setifValid((flagNull.x || flagNull.y) && !(props.lastPoint && !(!flagNull.x || !flagNull.y)))
    }, [flagNull])

    useEffect(() => {
        setVector2D(props.initValue)
    }, [props.initValue?.x, props.initValue?.y])
    useEffect(() => {
        setFlagNull(props.flagNull ? props.flagNull : { x: true, y: true });
    }, [props.flagNull])
    useEffect(() => {
        setifValid((flagNull.x || flagNull.y) && !(props.lastPoint && !(!flagNull.x || !flagNull.y)));
    }, [props.lastPoint])

    useEffect(() => {
        if (buttonClick == true) {
            setbuttonClick(false)
            setFlagNull({ x: false, y: false });
            if (props.pointCoordSystem === MapCore.EMcPointCoordSystem.EPCS_WORLD) {
                let pointNew = new MapCore.SMcFVector2D(cursorPos.Value.x, cursorPos.Value.y)
                setVector2D(pointNew)
                props.saveTheValue(pointNew, true)
            }

            if (props.pointCoordSystem === MapCore.EMcPointCoordSystem.EPCS_SCREEN) {
                let pointNew = new MapCore.SMcFVector2D(screenPos.x, screenPos.y)
                setVector2D(pointNew)
                props.saveTheValue(pointNew, true)
            }


        }
    }, [cursorPos])

    const saveVector2D = (event: any) => {
        let val = event.target.value
        let v = { ...vector2D, [event.target.name]: val };
        let f = { ...flagNull, [event.target.name]: false }
        if (val == null)
            f = { ...flagNull, [event.target.name]: true };
        let isFlagValid: boolean = f.x == false && f.y == false;
        setVector2D(v);
        setFlagNull(f);
        props.saveTheValue(v, isFlagValid)
    }

    const choosePoint = () => {
        let lastDialogType = dialogTypesArr[dialogTypesArr.length - 1];
        dispatch(setShowDialog({ hideFormReason: hideFormReasons.CHOOSE_POINT, dialogType: lastDialogType }));
        setbuttonClick(true)
    }
    return (<>
        <div style={{ display: 'flex', flexDirection: 'column', margin: '0.5%' }}>
            <div style={{ display: 'flex', alignItems: 'center' }} >
                {props.name && <label style={{ padding: `${globalSizeFactor * 0.75}vh` }}>{props.name}</label>}
                <label style={{ padding: `${globalSizeFactor * 0.75}vh` }}>X:</label>
                <InputNumber className={ifInValid ? 'p-invalid form__narrow-input' : 'form__narrow-input'} name="x" value={flagNull.x == false ? (vector2D?.x || null) : null} onValueChange={saveVector2D} useGrouping={false} maxFractionDigits={5}></InputNumber>
                <label style={{ padding: `${globalSizeFactor * 0.75}vh` }}>Y:</label>
                <InputNumber className={ifInValid ? 'p-invalid form__narrow-input' : 'form__narrow-input'} name="y" value={flagNull.y == false ? (vector2D?.y || null) : null} onValueChange={saveVector2D} useGrouping={false} maxFractionDigits={5}></InputNumber>
                {props.disabledPointFromMap == false ? "" : <div style={{ paddingLeft: `${globalSizeFactor * 0.4}vh` }}>
                    <Button style={{ marginLeft: '2%' }} label="..." onClick={() => { choosePoint() }} /></div>}
                {/* {ifInValid && <label style={{ color: 'red', whiteSpace: 'nowrap' }}>Point Invalid</label>} */}
                {props.applyVector2D && <Button label="Set" style={{ marginLeft: '2%' }} onClick={props.applyVector2D}></Button>}
            </div>
        </div>
    </>
    );

    // useGrouping={false} maxFractionDigits={5}
}