import * as actionTypes from "../actionTypes"

const SaveMapToPreview = (mapToPreview: any) => {
    return {
        type: actionTypes.SAVE_MAP_TO_PREVIEW,
        payload: mapToPreview,
    };

};
const SetActionMapPreview = (ActionMapPreview: any) => {
    return {
        type: actionTypes.SET_ACTIVE_MAP_PREVIEW,
        payload: ActionMapPreview,
    };
}
const SetActionDtmMapPreview = (activeDtmMapPreview: any) => {
    return {
        type: actionTypes.SET_ACTIVE_DTM_MAP_PREVIEW,
        payload: activeDtmMapPreview,
    };
}
const SetCameraData = (prevCameraData: any) => {
    return {
        type: actionTypes.SET_CAMERA_DATA,
        payload: prevCameraData,
    };
}
const SetErrorInPreview = (error: any) => {
    return {
        type: actionTypes.SET_ERROR_IN_PREVIEW,
        payload: error,
    };
}
const SetOpenMapService= (openMapService: any) => {
    return {
        type: actionTypes.SET_MAPCORE_SERVICE,
        payload: openMapService,
    };
}
export { SaveMapToPreview, SetActionMapPreview, SetActionDtmMapPreview,
     SetCameraData, SetErrorInPreview ,SetOpenMapService};