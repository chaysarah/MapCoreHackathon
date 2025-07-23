import { useEffect, useState } from "react";
import { Fieldset } from "primereact/fieldset";
import { Button } from "primereact/button";
import { ListBox } from "primereact/listbox";
import { Dialog } from "primereact/dialog";
import { useSelector } from "react-redux";
import _ from "lodash";

import { MapCoreData, TypeToStringService } from 'mapcore-lib';
import GridCoordinateSystemMoreDetails from "./gridCoordinateSystemMoreDetails";
import CreateCoordinateSystem from "./createCoordinateSystem";
import { AppState } from "../../../../../redux/combineReducer";

export default function SelectCoordinateSystem(props: { getSelectedCorSys: (selectedCorSys: MapCore.IMcGridCoordinateSystem) => void, header: string, initSelectedCorSys?: MapCore.IMcGridCoordinateSystem }) {
    const [openDialog, setOpenDialog] = useState(null);
    const [selectedCorSys, setSelectedCorSys] = useState(null);
    const [footer, setFooter] = useState(null);
    const [newCoorSys, setNewCoorSys] = useState(null);
    const [isNewCoorSys, setIsNewCoorSys] = useState(false);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    const coordinateSystemArr = MapCoreData.coordinateSystemArr.map((cs, i) => {//don't cover with useMemo!
        return {
            index: i + 1,
            coordinateSystem: cs.pCoordinateSystem,
            name: `${i + 1}) ${TypeToStringService.convertNumberToGridString(cs.pCoordinateSystem.GetGridCoorSysType())} (${cs.numInstancePointers} pointers)`
        }
    })

    useEffect(() => {
        let foundedCoordSys = null;
        if (props.initSelectedCorSys) {
            foundedCoordSys = coordinateSystemArr.find(coord => coord.coordinateSystem == props.initSelectedCorSys)
        }
        setSelectedCorSys(foundedCoordSys)
    }, [props.initSelectedCorSys])

    useEffect(() => {
        if (selectedCorSys)
            props.getSelectedCorSys(selectedCorSys?.coordinateSystem)
        else
            props.getSelectedCorSys(null)
    }, [selectedCorSys])

    useEffect(() => {
        if (isNewCoorSys) {
            setIsNewCoorSys(false)
            let c = coordinateSystemArr.find(coord => coord.coordinateSystem == newCoorSys);
            if (!_.isEqual(selectedCorSys, c))
                setSelectedCorSys(c)
        }
    }, [coordinateSystemArr])

    return (<div >
        <Fieldset className="form__space-between form__column-fieldset " style={{ marginLeft: '2%' }} legend={props.header}>
            <ListBox style={{ height: `${globalSizeFactor * 8} vh`, overflowY: 'auto' }} options={coordinateSystemArr} optionLabel="name" itemTemplate={(option) => { return <div style={{ whiteSpace: 'nowrap' }} onDoubleClick={() => { setOpenDialog("GridCoordinateSystemMoreDetails"); setSelectedCorSys(option) }}>{option.name}</div> }}
                value={selectedCorSys} onChange={(event) => setSelectedCorSys(event.target.value)}></ListBox>
            <Button onClick={() => setOpenDialog("CreateCoordinateSystem")}>Create New Coordinate System</Button>
        </Fieldset>
        <Dialog className="scroll-dialog-coordinate-system" onHide={() => setOpenDialog(null)} visible={openDialog != null ? true : false} header="Create Coordinate System" footer={footer}>
            {openDialog == "CreateCoordinateSystem" && <CreateCoordinateSystem getNewCoorSys={(NewCoorSys: MapCore.IMcGridCoordinateSystem) => {
                setNewCoorSys(NewCoorSys);
                setIsNewCoorSys(true);
            }} getFooter={(thisFooter: any) => { setFooter(thisFooter) }} ToClose={() => { setOpenDialog(null) }}></CreateCoordinateSystem>}
            {openDialog == "GridCoordinateSystemMoreDetails" && selectedCorSys && <GridCoordinateSystemMoreDetails gridCoordinateSystems={selectedCorSys?.coordinateSystem}></GridCoordinateSystemMoreDetails>}
        </Dialog>
    </div>)
}