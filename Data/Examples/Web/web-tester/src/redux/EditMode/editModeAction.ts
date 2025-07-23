import SecondDialogModel from "../../components/shared/models/second-dialog.model";
import * as actionTypes from "../actionTypes";

// Edit Mode
const setTypeEditModeDialogSecond = (typeDialog: SecondDialogModel) => {
    return {
        type: actionTypes.SET_TYPE_EDIT_MODE_DIALOG_SECOND,
        payload: typeDialog
    };
};

export {
    setTypeEditModeDialogSecond,
};
