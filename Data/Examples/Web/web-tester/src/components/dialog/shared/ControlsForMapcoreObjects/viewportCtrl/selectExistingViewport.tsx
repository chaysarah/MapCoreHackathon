import { Button } from "primereact/button";
import { ListBox } from "primereact/listbox";
import { useSelector } from "react-redux";
import { useEffect, useState } from "react";

import './styles/selectExistingViewport.css';
import { MapCoreData, ViewportData } from "mapcore-lib";
import { AppState } from "../../../../../redux/combineReducer";
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";
import generalService from "../../../../../services/general.service";

interface BaseSelectExistingViewportProps {
    selectedViewport?: MapCore.IMcMapViewport;
    mcViewports?: MapCore.IMcMapViewport[];
}
interface WithActionProps extends BaseSelectExistingViewportProps {
    finalActionButton: true;
    handleOKClick: (selectedViewport: MapCore.IMcMapViewport) => void;
    getSelectedViewport?: undefined,
};
interface WithoutActionProps extends BaseSelectExistingViewportProps {
    finalActionButton?: undefined;
    handleOKClick?: undefined;
    getSelectedViewport: (selectedViewport: MapCore.IMcMapViewport) => void,
};
type SelectExistingViewportProps = WithActionProps | WithoutActionProps;

export default function SelectExistingViewport(props: SelectExistingViewportProps) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [selectedViewport, setSelectedViewport] = useState<{ mcViewport: MapCore.IMcMapViewport, label: string }>(null);
    const [existingViewportsArr, setExistingViewportsArr] = useState<{ mcViewport: MapCore.IMcMapViewport, label: string }[]>([]);

    //#region useEffects
    useEffect(() => {
        runCodeSafely(() => {
            setDialogWidth();

            let viewportsList = MapCoreData.viewportsData;
            if (props.mcViewports) {
                viewportsList = [];
                props.mcViewports.forEach(mcViewport => {
                    const currentVpData = MapCoreData.viewportsData.find(vpData => vpData.viewport == mcViewport)
                    currentVpData && viewportsList.push(currentVpData)
                })
            }
            const viewportsObjectsArr = viewportsList.map((vpData: ViewportData) => ({
                mcViewport: vpData.viewport,
                label: generalService.getObjectName(vpData, "Viewport")
            }))
            setExistingViewportsArr(viewportsObjectsArr)
        }, 'selectExistingItem.useEffect[mounting]')
    }, [])
    useEffect(() => {
        runCodeSafely(() => {
            const foundedViewport = existingViewportsArr.find(vpObj => vpObj.mcViewport == props.selectedViewport);
            if (foundedViewport) {
                setSelectedViewport(foundedViewport);
            }
        }, 'selectExistingItem.useEffect[props.selectedItem]')
    }, [props.selectedViewport])
    //#endregion

    const setDialogWidth = () => {
        runCodeSafely(() => {
            if (props.finalActionButton) {
                const root = document.documentElement;
                let pixelWidth = window.innerHeight * 0.35 * globalSizeFactor;
                root.style.setProperty('--select-existing-viewport-dialog-width', `${pixelWidth}px`);
            }
        }, 'selectExistingViewport.setDialogWidth')
    }
    const handleSelectedViewportChange = (e) => {
        runCodeSafely(() => {
            setSelectedViewport(e.target.value);
            props.getSelectedViewport && props.getSelectedViewport(e.target.value ? e.target.value.mcViewport : null);
        }, 'selectExistingViewport.handleSelectedViewportChange')
    }

    return <div className='form__column-container'>
        <ListBox listStyle={{ minHeight: `${globalSizeFactor * 13}vh`, maxHeight: `${globalSizeFactor * 13}vh` }} options={existingViewportsArr} optionLabel="label" value={selectedViewport} onChange={handleSelectedViewportChange} />
        {props.finalActionButton && <Button className="form__align-self-end" label='OK' onClick={e => props.handleOKClick(selectedViewport ? selectedViewport.mcViewport : null)} />}
    </div>
}