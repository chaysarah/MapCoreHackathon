
import { combineReducers } from 'redux'
import mapWindowReducer, { MapWindowState } from './mapWindow/mapWindowReducer'
import mapCoreReducer, { MapCoreState } from './MapCore/mapCoreReducer'
import objectWorldTreeReducer, { ObjectWorldTreeState } from './ObjectWorldTree/objectWorldTreeReducer';
import mapWorldTreeReducer, { MapWorldTreeState } from './MapWorldTree/mapWorldTreeReducer';
import editModeReducer, { EditModeState } from './EditMode/editModeReducer';
import layerParamsReducer, { LayerParamsState } from './LayerParams/layerParamsReducer';


export interface AppState {
    mapCoreReducer: MapCoreState,
    mapWindowReducer: MapWindowState,
    objectWorldTreeReducer: ObjectWorldTreeState,
    mapWorldTreeReducer: MapWorldTreeState,
    editModeReducer: EditModeState,
    layerParamsReducer:LayerParamsState
}

const rootReducer = combineReducers({
    mapCoreReducer,
    mapWindowReducer,
    objectWorldTreeReducer,
    mapWorldTreeReducer,
    editModeReducer,
    layerParamsReducer
})

export default rootReducer;