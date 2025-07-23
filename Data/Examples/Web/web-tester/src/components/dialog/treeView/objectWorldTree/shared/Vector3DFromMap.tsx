// External libraries
import { useState, useEffect, ReactElement } from 'react';
import { useDispatch, useSelector } from 'react-redux';

// UI/component libraries
import { InputNumber } from "primereact/inputnumber";
import { Button } from "primereact/button";

// Project-specific imports
import { hideFormReasons } from "../../../../shared/models/tree-node.model";
import { AppState } from "../../../../../redux/combineReducer";
import { DialogTypesEnum } from "../../../../../tools/enum/enums";
import { setShowDialog } from "../../../../../redux/ObjectWorldTree/objectWorldTreeActions";

export default function Vector3DFromMap(props: {
    initValue: MapCore.SMcVector3D,
    saveTheValue: (point: MapCore.SMcVector3D, flagNull: boolean) => void,
    applyVector3D?: () => void, name?: string,
    lastPoint?: boolean,
    flagNull?: { x: boolean, y: boolean, z: boolean },
    pointCoordSystem: MapCore.EMcPointCoordSystem,
    disabledPointFromMap?: boolean
}) {

    const dispatch = useDispatch();
    const cursorPos: any = useSelector((state: AppState) => state.mapWindowReducer.cursorPos);
    const dialogTypesArr: DialogTypesEnum[] = useSelector((state: AppState) => state.mapCoreReducer.dialogTypesArr);
    const screenPos: MapCore.SMcVector3D = useSelector((state: AppState) => state.mapWindowReducer.screenPos);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    let [vector3D, setVector3D] = useState({ ...props.initValue })
    const [buttonClick, setbuttonClick] = useState(false);
    const [flagNull, setFlagNull] = useState(props.flagNull ? props.flagNull : { x: true, y: true, z: true });
    const [ifInValid, setifValid] = useState<boolean>((flagNull.x || flagNull.y) && !(props.lastPoint && !(!flagNull.x || !flagNull.y || !flagNull.z)));

    useEffect(() => {
        let xAndYExist = flagNull.x || flagNull.y;
        setifValid(xAndYExist && !(props.lastPoint && !(!flagNull.x || !flagNull.y || !flagNull.z)))
    }, [flagNull])

    useEffect(() => {
        setVector3D(props.initValue)
        if (!props.flagNull && (props.initValue?.x || props.initValue?.y || props.initValue?.z)) {
            setFlagNull({ x: false, y: false, z: false });
        }
    }, [props.initValue?.x, props.initValue?.y, props.initValue?.z])
    useEffect(() => {
        setFlagNull(props.flagNull ? props.flagNull : { x: true, y: true, z: true });
    }, [props.flagNull])
    useEffect(() => {
        setifValid((flagNull.x || flagNull.y) && !(props.lastPoint && !(!flagNull.x || !flagNull.y || !flagNull.z)));
    }, [props.lastPoint])

    useEffect(() => {
        if (buttonClick == true) {
            setbuttonClick(false)
            if (props.pointCoordSystem === MapCore.EMcPointCoordSystem.EPCS_WORLD) {
                setVector3D(cursorPos.Value)
                props.saveTheValue(cursorPos.Value, true)
            }

            if (props.pointCoordSystem === MapCore.EMcPointCoordSystem.EPCS_SCREEN) {
                setVector3D(screenPos)
                props.saveTheValue(screenPos, true)
            }
            setFlagNull({ x: false, y: false, z: false });
        }
    }, [cursorPos, screenPos])

    const saveVector3D = (event: any) => {
        let val = event.target.value
        let v = { ...vector3D, [event.target.name]: val };
        let f = { ...flagNull, [event.target.name]: false }
        if (val == null)
            f = { ...flagNull, [event.target.name]: true };
        let isFlagValid: boolean = f.x == false && f.y == false;
        setVector3D(v);
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
                {props.name && <label style={{ padding: `${globalSizeFactor * 0.75}vh`, whiteSpace: 'nowrap' }}>{props.name}</label>}
                <label style={{ padding: `${globalSizeFactor * 0.75}vh`, }}>X:</label>
                <InputNumber className={ifInValid ? 'p-invalid form__narrow-input' : 'form__narrow-input'} name="x" value={flagNull.x == false ? (vector3D?.x || null) : null} onValueChange={saveVector3D} useGrouping={false} maxFractionDigits={5}></InputNumber>
                <label style={{ padding: `${globalSizeFactor * 0.75}vh` }}>Y:</label>
                <InputNumber className={ifInValid ? 'p-invalid form__narrow-input' : 'form__narrow-input'} name="y" value={flagNull.y == false ? (vector3D?.y || null) : null} onValueChange={saveVector3D} useGrouping={false} maxFractionDigits={5}></InputNumber>
                <label style={{ padding: `${globalSizeFactor * 0.75}vh` }}>Z:</label>
                <InputNumber className={ifInValid ? 'p-invalid form__narrow-input' : 'form__narrow-input'} name="z" value={flagNull.z == false ? (vector3D?.z || null) : null} onValueChange={saveVector3D} useGrouping={false} maxFractionDigits={5}></InputNumber>
                {props.disabledPointFromMap == false ? "" : <div style={{ paddingLeft: `${globalSizeFactor * 0.4}vh` }}>
                    <Button style={{ marginLeft: '2%' }} label="..." onClick={() => { choosePoint() }} />
                </div>}
                {/* {ifInValid && <label style={{ color: 'red', whiteSpace: 'nowrap' }}>Point Invalid</label>} */}
                {props.applyVector3D && <Button label="Set" style={{ marginLeft: '2%' }} onClick={props.applyVector3D}></Button>}
            </div>
        </div>
    </>
    );
}