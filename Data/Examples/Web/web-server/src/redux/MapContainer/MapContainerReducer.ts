// import { AppState } from "../combineReducer";
import { mapActions } from "../../config";
import * as actionTypes from "../actionTypes"

const initialState: any = {
  mapToPreview: {},
  activeMapPreview: "",
  activeDtmMapPreview: false,
  prevCameraData: { prevScale: null, prevPosition: null, Yaw: 0, Pitch: 0, Roll: 0 },
  errorInPreview: null,
  openMapService: null
};
const mapContainerReducer = (state = initialState, action: { type: string; payload: any; }) => {
  switch (action.type) {
    case actionTypes.SET_MAPCORE_SERVICE:
      return {
        ...state,
        openMapService: action.payload,
      };
    case actionTypes.SAVE_MAP_TO_PREVIEW:
      return {
        ...state,
        mapToPreview: action.payload,
      };
    case actionTypes.SET_ACTIVE_DTM_MAP_PREVIEW:
      return {
        ...state,
        activeDtmMapPreview: action.payload,
      };
    case actionTypes.SET_ACTIVE_MAP_PREVIEW:
      return {
        ...state,
        activeMapPreview: action.payload,
      };
    case actionTypes.SET_CAMERA_DATA:
      return {
        ...state,
        prevCameraData: action.payload,
      };
    case actionTypes.SET_ERROR_IN_PREVIEW:
      return {
        ...state,
        errorInPreview: action.payload,
      };
  }
  return state;
};

export default mapContainerReducer;



