import { TabPanel, TabView } from "primereact/tabview"
import { useState } from "react";
import { useSelector } from "react-redux";

import { MapCoreData } from "mapcore-lib";
import CreateDevice from "../openMap/objectProperties/openMapManually/createDevice"
import CreateDataSQ from "../openMap/objectProperties/openMapManually/createDataSQ"
import DeviceOperations from "../openMap/objectProperties/openMapManually/deviceOperations"
import { AppState } from "../../../redux/combineReducer";

export default function DeviceAndSpatialQueriesTabs(props: { initialSCreateData: MapCore.IMcMapViewport.SCreateData, setSCreateData: (sCreateData: MapCore.IMcMapViewport.SCreateData) => void }) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [stringPath, setStringPath] = useState<{ index: number, stringPath: string }[]>([]);
    let [activeTab, setActiveTab] = useState(MapCoreData.device ? 1 : 0)


    return <div style={{ width: `${globalSizeFactor * 43}vh` }}>
        <TabView activeIndex={activeTab} onTabChange={e => setActiveTab(e.index)}>
            <TabPanel header={"Create Device"}>
                <CreateDevice />
            </TabPanel>
            <TabPanel header={"Create Data SQ"}>
                <CreateDataSQ initialSCreateData={props.initialSCreateData} setSCreateData={props.setSCreateData} />
            </TabPanel>
            <TabPanel header={"Device Operations"}>
                <DeviceOperations getStringPath={(SP) => { setStringPath(SP) }} initStringPath={stringPath} />
            </TabPanel>
        </TabView>
    </div>
}