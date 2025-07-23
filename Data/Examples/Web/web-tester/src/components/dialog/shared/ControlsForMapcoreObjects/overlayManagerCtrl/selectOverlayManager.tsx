import { useEffect, useState } from "react";
import { Fieldset } from "primereact/fieldset";
import { MapCoreData, OverlayManager } from 'mapcore-lib';
import { ListBox } from "primereact/listbox";
import { useSelector } from "react-redux";
import _ from 'lodash'
import { Button } from "primereact/button";
import { Dialog } from "primereact/dialog";
import CreateOverlayManager from "./createOverlayManager";
import { AppState } from "../../../../../redux/combineReducer";

export default function SelectOverlayManager(props: { getSelectedOM: (selectedOMs: MapCore.IMcOverlayManager) => void, initSelectedOMs: MapCore.IMcOverlayManager }) {
    const [selectedOM, setSelectedOM] = useState<{ index: number, overlayManager: MapCore.IMcOverlayManager, name: string }>(null);
    const [openDialog, setOpenDialog] = useState<boolean>(false);
    const [newOM, setNewOM] = useState(null);
    const [isNewOM, setIsNewOM] = useState(false);
    
    const existingOverlayManagerArr = MapCoreData.overlayManagerArr.map((OM: OverlayManager, i) => {
        return {
            index: i + 1,
            overlayManager: OM.overlayManager,
            name: i + 1 + ") " + "Overlay Manager"
        }
    })

    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    useEffect(() => {
        if (selectedOM)
            props.getSelectedOM(selectedOM?.overlayManager)
        else
            props.getSelectedOM(null)

    }, [selectedOM])

    useEffect(() => {
        if (isNewOM) {
            setIsNewOM(false);
            let overlayM = existingOverlayManagerArr.find(OM => OM.overlayManager == newOM)
            setSelectedOM(overlayM)
        }
    }, [existingOverlayManagerArr])

    return (<div >
        <Fieldset className="form__space-between form__column-fieldset " style={{ marginLeft: '2%' }} legend="Overlay Manager">
            <ListBox style={{ height: `${globalSizeFactor * 5}vh`, overflowY: 'auto' }} options={existingOverlayManagerArr} optionLabel="name"
                value={selectedOM} onChange={(event) => setSelectedOM(event.target.value)}></ListBox>
            <Button onClick={() => { setOpenDialog(true) }}>Create New Overlay Manager</Button>
        </Fieldset>
        <Dialog onHide={() => { setOpenDialog(false) }} visible={openDialog} header="Create New Overlay Manager">
            <CreateOverlayManager toCloseDialog={() => { setOpenDialog(false) }} getOverlayManager={(OM_: MapCore.IMcOverlayManager) => {
                setNewOM(OM_)
                setIsNewOM(true)
            }}></CreateOverlayManager>
        </Dialog>
    </div>)
}