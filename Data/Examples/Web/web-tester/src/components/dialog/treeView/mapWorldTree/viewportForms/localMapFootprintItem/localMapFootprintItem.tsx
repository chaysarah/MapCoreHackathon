import { useCallback, useEffect, useMemo, useState } from 'react';
import { useSelector } from 'react-redux';
import { ListBox } from 'primereact/listbox';
import { Button } from 'primereact/button';

import './localMapFootprintItem.css';
import '../../styles/mapWorldTreeDialog.css';
import { AppState } from '../../../../../../redux/combineReducer';
import { runCodeSafely, runMapCoreSafely } from '../../../../../../common/services/error-handling/errorHandler';
import SelectExistingItem from '../../../../shared/ControlsForMapcoreObjects/itemCtrl/selectExistingItem';
import SelectExistingViewport from '../../../../shared/ControlsForMapcoreObjects/viewportCtrl/selectExistingViewport';

export default function LocalMapFootprintItem(props: {
    viewport: MapCore.IMcMapViewport,
    closeDialog: () => void,
}) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [selectedMcViewport, setSelectedMcViewport] = useState<MapCore.IMcMapViewport>(null);
    const [selectedActiveLine, setSelectedActiveLine] = useState(null);
    const [selectedInactiveLine, setSelectedInactiveLine] = useState(null);

    const getRegisteredViewports = useCallback(() => {
        let mcRegisteredViewports: MapCore.IMcMapViewport[] = [];
        runMapCoreSafely(() => { mcRegisteredViewports = props.viewport.GetRegisteredLocalMaps(); }, 'localMapFootprintItem.getRegisteredViewports => IMcGlobalMap.GetRegisteredLocalMaps', true)
        return mcRegisteredViewports;
    }, [])

    useEffect(() => {
        runCodeSafely(() => {
            const root = document.documentElement;
            let pixelWidth = window.innerHeight * 0.62 * globalSizeFactor;
            root.style.setProperty('--local-map-footprint-dialog-width', `${pixelWidth}px`);

            let puLocalMapInactiveLine: any = {}, puLocalMapActiveLine: any = {};
            runMapCoreSafely(() => {
                props.viewport.GetLocalMapFootprintItem(puLocalMapInactiveLine, puLocalMapActiveLine);
            }, "localMapFootprintItem.useEffect => IMcGlobalMap.GetLocalMapFootprintItem", true)
            setSelectedActiveLine(puLocalMapActiveLine.Value)
            setSelectedInactiveLine(puLocalMapInactiveLine.Value)
        }, 'localMapFootprintItem.useEffect');
    }, [])
    const handleOKClick = () => {
        runCodeSafely(() => {
            props.closeDialog();
            //after shavuot to check if SetLocalMapFootprintItem works - sm
            runMapCoreSafely(() => {
                props.viewport.SetLocalMapFootprintItem(selectedInactiveLine, selectedActiveLine, selectedMcViewport);
            }, 'localMapFootprintItem.handleOKClick => IMcGlobalMap.SetLocalMapFootprintItem', true)
        }, 'localMapFootprintItem.handleOKClick');
    }

    return (
        <div className='form__column-container'>
            <div className='form__flex-and-row-between'>
                <div className='form__column-container'>
                    <label style={{ textDecoration: 'underline' }}> Viewports (Clear slection to set for all viewports)</label>
                    <SelectExistingViewport mcViewports={getRegisteredViewports()} getSelectedViewport={(selectedViewport: MapCore.IMcMapViewport) => setSelectedMcViewport(selectedViewport)} />
                </div>
                <div className='form__column-container'>
                    <label style={{ textDecoration: 'underline' }}> Active Line:</label>
                    <SelectExistingItem selectedItem={selectedActiveLine} itemType={MapCore.IMcLineItem.NODE_TYPE} getSelectedItem={(selectedItem: MapCore.IMcObjectSchemeItem) => setSelectedActiveLine(selectedItem)} />
                </div>
                <div className='form__column-container'>
                    <label style={{ textDecoration: 'underline' }}> Inactive Line:</label>
                    <SelectExistingItem selectedItem={selectedInactiveLine} itemType={MapCore.IMcLineItem.NODE_TYPE} getSelectedItem={(selectedItem: MapCore.IMcObjectSchemeItem) => setSelectedInactiveLine(selectedItem)} />
                </div>
            </div>
            <Button className='form__align-self-end' label='OK' onClick={handleOKClick} />
        </div>
    );
}
