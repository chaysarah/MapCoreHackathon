import { enums, LayerParams,LayerDetails } from "mapcore-lib";
import { SET_ACTIVE_LAYER_DETAILS,SET_ACTIVE_LAYER_DETAILS_LAYER_PARAMS,SET_ACTIVE_LAYER_DETAILS_LAYER_PARAMS_THREE_D_EXTRUSION} from "../actionTypes";

export interface LayerParamsState {
    activeLayerDetails: LayerDetails
    activeLayerDetailsDtmLayers: LayerDetails
}
const initialState: LayerParamsState = {
    activeLayerDetails: null,
    activeLayerDetailsDtmLayers:null
}

const layerParamsReducer = (state = initialState, action: { type: string; payload: any; }) => {
    switch (action.type) {
        case SET_ACTIVE_LAYER_DETAILS:
            return {
                ...state,
                activeLayerDetails: action.payload,
            };
            case SET_ACTIVE_LAYER_DETAILS_LAYER_PARAMS:
                return {
                    ...state,
                    activeLayerDetails:
                    {...state.activeLayerDetails,layerParams:action.payload}
                };

            case SET_ACTIVE_LAYER_DETAILS_LAYER_PARAMS_THREE_D_EXTRUSION:
                return {
                    ...state,
                    activeLayerDetails:{...state.activeLayerDetails,layerParams:
                        {...state.activeLayerDetails?.layerParams,threeDExtrusionParams:action.payload}
                    }
                };
    }
    return state;
};
export default layerParamsReducer;



