import { useSelector } from "react-redux";
import { AppState } from "../../../../../redux/combineReducer";
import { Fieldset } from "primereact/fieldset";
import { InputTextarea } from "primereact/inputtextarea";
import { Properties } from "../../../dialog";

export class UserDataCtrlPropertiesState implements Properties {
    userData: string;

    static getDefault(p: any): UserDataCtrlPropertiesState {
        let decoder = new TextDecoder();
        return {
            userData: p.selectedNodeInTree.nodeMcContent.GetUserData() ? decoder.decode(p.selectedNodeInTree.nodeMcContent.GetUserData()?.getUserData()) : '',
        }
    }
}

export class UserDataCtrlProperties extends UserDataCtrlPropertiesState {

    static getDefault(p: any): UserDataCtrlProperties {
        let stateDefaults = super.getDefault(p);
        let defaults: UserDataCtrlProperties = {
            ...stateDefaults,
        }
        return defaults;
    }
}

export class UserDataCtrlInfo {
    properties: UserDataCtrlProperties;
    setPropertiesCallback: (key: string, value: any) => void; // null key for update the object itself
    setCurrStatePropertiesCallback: (key: string, value: any) => void;
    setApplyCallBack: (Callback: () => void) => void;
    applyCurrStatePropertiesCallback: (pathesToApply?: string[]) => void;
    ctrlHeight: number;
};

export default function UserDataCtrl(props: { userData: Uint8Array, ctrlHeight: number, getUserDataBuffer: (userDataBuffer: Uint8Array) => void }) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    const handleUserDataChange = (e) => {
        let encoder = new TextEncoder();
        let userDataBuffer = encoder.encode(e.target.value);
        props.getUserDataBuffer(userDataBuffer);
    }

    const getUserData = () => {
        let decoder = new TextDecoder();

        return <Fieldset style={{ height: `${globalSizeFactor * props.ctrlHeight}vh` }} className="form__space-around" legend="User Data">
            <InputTextarea style={{ width: '100%', height: `${globalSizeFactor * props.ctrlHeight * 0.75}vh` }} name='userData' value={props.userData ? decoder.decode(props.userData) : ''} onChange={handleUserDataChange} />
        </Fieldset>
    }

    return (<div>
        {getUserData()}
    </div>)
}