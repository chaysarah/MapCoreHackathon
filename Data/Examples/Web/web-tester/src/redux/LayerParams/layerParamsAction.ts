import * as actionTypes from "../actionTypes";
import { enums, LayerDetails, LayerParams } from 'mapcore-lib';

const setActiveLayerDetails= (layerParams: LayerDetails ) => {
    return {
        type: actionTypes.SET_ACTIVE_LAYER_DETAILS,
        payload: layerParams
    };
};
// setActiveLayerDetailsLayerParams
const setActiveLayerDetailsLayerParams= (layerParams: LayerParams ) => {
    return {
        type: actionTypes.SET_ACTIVE_LAYER_DETAILS_LAYER_PARAMS,
        payload: layerParams
    };
};

// setActiveLayerDetailsLayerParamsThreeDExtrusion
const setActiveLayerDetailsLayerParamsThreeDExtrusion= (layerParams:  MapCore.IMcRawVector3DExtrusionMapLayer.SParams ) => {
    return {
        type: actionTypes.SET_ACTIVE_LAYER_DETAILS_LAYER_PARAMS_THREE_D_EXTRUSION,
        payload: layerParams
    };
};
export {
    setActiveLayerDetails,setActiveLayerDetailsLayerParams ,setActiveLayerDetailsLayerParamsThreeDExtrusion
};

