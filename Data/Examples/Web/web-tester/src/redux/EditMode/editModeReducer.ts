
import _ from "lodash";

import { SET_TYPE_EDIT_MODE_DIALOG_SECOND } from "../actionTypes";
import SecondDialogModel from "../../components/shared/models/second-dialog.model";

export interface EditModeState {
    typeEditModeDialogSecond: SecondDialogModel,
}
const initialState: EditModeState = {
    typeEditModeDialogSecond: undefined,
}

const editModeReducer = (state = initialState, action: { type: string; payload: any; }) => {
    switch (action.type) {
        case SET_TYPE_EDIT_MODE_DIALOG_SECOND:
            return {
                ...state,
                typeEditModeDialogSecond: action.payload,
            };
    }
    return state;
};


export default editModeReducer;



