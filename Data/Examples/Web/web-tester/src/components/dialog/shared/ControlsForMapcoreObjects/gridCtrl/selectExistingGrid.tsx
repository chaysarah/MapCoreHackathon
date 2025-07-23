import { useSelector } from 'react-redux';
import { useEffect, useMemo, useState } from 'react';
import { ListBox } from 'primereact/listbox';
import { Button } from 'primereact/button';

import { MapCoreData } from 'mapcore-lib';
import './styles/selectExistingGrid.css';
import { mapWorldNodeType } from '../../../../shared/models/map-tree-node.model';
import { AppState } from '../../../../../redux/combineReducer';
import mapWorldTreeService from '../../../../../services/mapWorldTreeService';
import { runCodeSafely } from '../../../../../common/services/error-handling/errorHandler';

export default function SelectExistingGrid(props: {
    handleOKClick: (selectedGrid: MapCore.IMcMapGrid) => void
}) {

    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    let existingGridArr = useMemo(() => MapCoreData.gridArr.map((grid: MapCore.IMcMapGrid, i: number) => ({
        index: i + 1,
        grid: grid,
        name: `${i + 1}} ${mapWorldTreeService.getTreeNodeLabel(grid, mapWorldNodeType.MAP_GRID)}`
    })), [])
    const [selectedGrid, setSelectedGrid] = useState<{ index: number; grid: MapCore.IMcMapGrid; name: string; }>(null);

    useEffect(() => {
        runCodeSafely(() => {
            const root = document.documentElement;
            let pixelWidth = window.innerHeight * 0.35 * globalSizeFactor;
            root.style.setProperty('--select-existing-grid-dialog-width', `${pixelWidth}px`);
        }, 'selectExistingGrid.useEffect')
    }, [])

    return <div className='form__column-container'>
        <ListBox options={existingGridArr} optionLabel="name" value={selectedGrid} onChange={(event) => setSelectedGrid(event.target.value)} />
        <Button className="form__align-self-end" label='OK' onClick={e => props.handleOKClick(selectedGrid?.grid)} />
    </div>
}
