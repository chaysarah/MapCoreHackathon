import { useSelector } from "react-redux";
import TabsParentCtrl from "../../../shared/tabCtrls/tabsParentCtrl";
import General, { GeneralProperties, GeneralPropertiesState } from "./general";
import Layers, { LayersProperties, LayersPropertiesState } from "./layers";
import { AppState } from "../../../../../redux/combineReducer";
import { TabType } from "../../../shared/tabCtrls/tabModels";

const tabTypes: TabType[] = [
    { index: 0, header: 'General', statePropertiesClass: GeneralPropertiesState, propertiesClass: GeneralProperties, component: General },
    { index: 1, header: 'Layers', statePropertiesClass: LayersPropertiesState, propertiesClass: LayersProperties, component: Layers },
]

export default function MapTerrainForm() {
    const cursorPos = useSelector((state: AppState) => state.mapWindowReducer.cursorPos);
    let currentTerrain = useSelector((state: AppState) => state.mapWorldTreeReducer.selectedNodeInTree);
    let mapWorldTree = useSelector((state: AppState) => state.mapWorldTreeReducer.mapWorldTree);

    return <TabsParentCtrl
        parentName='MapTerrainForm'
        tabTypes={tabTypes}
        getDefaultFuncProps={{ currentTerrain, cursorPos, mapWorldTree }}
        selectedNode={currentTerrain}
    />
}

