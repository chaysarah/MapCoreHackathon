import { useState } from "react";
import { useSelector } from "react-redux";
import { Button } from "primereact/button";
import { Fieldset } from "primereact/fieldset";
import { ListBox } from "primereact/listbox";
import { InputNumber } from "primereact/inputnumber";

import { MapCoreData, MapCoreService, OverlayManager } from 'mapcore-lib';
import SelectCoordinateSystem from "../coordinateSystemCtrl/selectCoordinateSystem";
import { AppState } from "../../../../../redux/combineReducer";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";

export default function CreateOverlayManager(props: { toCloseDialog: () => void, getOverlayManager: (OM: MapCore.IMcOverlayManager) => void }) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [selectedCorSys, setSelectedCorSys] = useState<MapCore.IMcGridCoordinateSystem>();
    const [overlayManager, setOverlayManager] = useState<MapCore.IMcOverlayManager>(null);
    const [overlayList, setOverlayList] = useState<string[]>([]);
    const [selectOverlay, setSelectOverlay] = useState<string>();
    const [scaleFactor, setScaleFactor] = useState<number>(1);
    const [count, setCount] = useState<number>(1);

    const createOverlayManager = () => {
        let overlayManager: MapCore.IMcOverlayManager;
        runCodeSafely(() => {
            overlayManager = MapCoreService.createOverlayManager(selectedCorSys)
            setOverlayManager(overlayManager)

            let overlay;
            overlayList.forEach((o, i) => {
                if (o == selectOverlay || i == 0)
                    overlay = MapCoreService.createOverlay(overlayManager)
                else
                    MapCoreService.createOverlay(overlayManager)
            })
            MapCoreData.overlayManagerArr.find((OM: OverlayManager) => OM.overlayManager == overlayManager).overlayActive = overlay;
            runMapCoreSafely(() => {
                overlayManager.SetScaleFactor(scaleFactor);
            }, "CreateOverlayManager => SetScaleFactor", true)
            props.getOverlayManager(overlayManager)
            props.toCloseDialog()
        }, "CreateOverlayManager=>createOverlayManager")
    }

    return (<div>
        <SelectCoordinateSystem header="Grid Coordinate System" getSelectedCorSys={(selectedCorSys: MapCore.IMcGridCoordinateSystem) => {
            setSelectedCorSys(selectedCorSys)
        }}></SelectCoordinateSystem>

        <Fieldset legend="Add Overlay">
            <div>
                <Button onClick={() => { setOverlayList([...overlayList, count + ") overlay"]); setCount(count + 1) }}>Create</Button>
                <Button onClick={() => { setOverlayList(overlayList.filter(o => o != selectOverlay)); }}>Remove</Button>
            </div>
            <ListBox style={{ height: `${globalSizeFactor * 7}vh`, width: `${globalSizeFactor * 14}vh`, overflowY: 'auto' }} options={overlayList} value={selectOverlay} onChange={(event) => setSelectOverlay(event.target.value)}></ListBox>
        </Fieldset>
        <div>
            <label style={{ padding: `${globalSizeFactor * 0.15}vh` }} htmlFor='Max'>Scale Factor:</label>
            <InputNumber className='form__medium-width-input' id='Max' value={scaleFactor} name='fTerrainObjectsBestResolution' onValueChange={(event) => setScaleFactor(event.target.value)} />
        </div>
        <div style={{ direction: 'rtl', marginTop: `${globalSizeFactor * 2}vh` }}> <Button onClick={createOverlayManager} >Create Overlay Manager</Button></div>
    </div >
    )
}