import { Fieldset } from "primereact/fieldset";
import { Dialog } from "primereact/dialog";
import { useState } from "react";
import { Button } from "primereact/button";
import { useDispatch, useSelector } from "react-redux";

import { TypeToStringService } from 'mapcore-lib'
import './styles/coordinateSystem.css';
import GridCoordinateSystemMoreDetails from "./gridCoordinateSystemMoreDetails";
import { DialogTypesEnum } from "../../../../../tools/enum/enums";
import { AppState } from "../../../../../redux/combineReducer";
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";
import { setTypeObjectWorldDialogSecond } from "../../../../../redux/ObjectWorldTree/objectWorldTreeActions";
import { setTypeMapWorldDialogSecond } from "../../../../../redux/MapWorldTree/mapWorldTreeActions";

export default function GridCoordinateSystemDataset(props: { gridCoordinateSystem: MapCore.IMcGridCoordinateSystem, useLocalDialog?: boolean }) {
    const dispatch = useDispatch();
    const dialogTypesArr: DialogTypesEnum[] = useSelector((state: AppState) => state.mapCoreReducer.dialogTypesArr);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    let [dialogVisible, setDialogVisible] = useState(false);

    const showMoreDetails = () => {
        runCodeSafely(() => {
            if (props.useLocalDialog) {
                setDialogVisible(true);
            }
            else if (dialogTypesArr[dialogTypesArr.length - 1] === DialogTypesEnum.objectWorldTree) {
                dispatch(setTypeObjectWorldDialogSecond({
                    secondDialogHeader: "More Details",
                    secondDialogComponent: <GridCoordinateSystemMoreDetails gridCoordinateSystems={props.gridCoordinateSystem} />
                })
                )
            }
            else if (dialogTypesArr[dialogTypesArr.length - 1] === DialogTypesEnum.mapWorldTree) {
                dispatch(setTypeMapWorldDialogSecond({
                    secondDialogHeader: "More Details",
                    secondDialogComponent: <GridCoordinateSystemMoreDetails gridCoordinateSystems={props.gridCoordinateSystem} />
                })
                )
            }
        }, "overlayManagerGeneralForm.showMoreDetails")
    }

    const splitEnumStr = (str: string) => {
        let lastUnderscoreIndex = str.lastIndexOf("_");
        if (lastUnderscoreIndex !== -1) {
            let secondLastUnderscoreIndex = str.lastIndexOf("_", lastUnderscoreIndex - 1);
            if (secondLastUnderscoreIndex !== -1) {
                let subString: string = str.substring(secondLastUnderscoreIndex + 1);
                return subString;
            }
        }
    }

    return (<div>
        <Fieldset style={{ height: '100%' }} className="form__column-fieldset" legend="Grid Coordinate Systems">
            <div style={{ height: `${globalSizeFactor * 10}vh` }}>
                <div style={{ height: '70%' }}>
                    <div className="coordinate-system__coord-sys-params">
                        <label >Type: </label>
                        <div style={{ whiteSpace: "nowrap" }}>{TypeToStringService.convertNumberToGridString(props.gridCoordinateSystem.GetGridCoorSysType())} </div>
                    </div>
                    <div className="coordinate-system__coord-sys-params">
                        <label >datum:  </label>
                        <div>{splitEnumStr(props.gridCoordinateSystem.GetDatum().constructor.name)}</div>
                    </div>
                </div>
                <div style={{ display: 'flex', justifyContent: 'center', height: '30%' }}>
                    <Button onClick={showMoreDetails} >More Details</Button>
                </div>
            </div>
        </Fieldset>

        <Dialog header='More Details' visible={dialogVisible} onHide={() => setDialogVisible(false)}>
            <GridCoordinateSystemMoreDetails gridCoordinateSystems={props.gridCoordinateSystem} />
        </Dialog>
    </div>
    )
}
