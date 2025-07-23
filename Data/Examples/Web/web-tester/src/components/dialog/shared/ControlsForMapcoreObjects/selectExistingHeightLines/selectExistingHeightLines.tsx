import { useSelector } from 'react-redux';
import { useEffect, useState } from 'react';
import { ListBox } from 'primereact/listbox';
import { Button } from 'primereact/button';

import { MapCoreData } from 'mapcore-lib';
import './styles/selectExistingHeightLines.css';
import { mapWorldNodeType } from '../../../../shared/models/map-tree-node.model';
import { AppState } from '../../../../../redux/combineReducer';
import mapWorldTreeService from '../../../../../services/mapWorldTreeService';
import { runCodeSafely } from '../../../../../common/services/error-handling/errorHandler';

export default function SelectExistingHeightLines(props: {
    handleOKClick: (selectedHeightLines: MapCore.IMcMapHeightLines) => void
}) {

    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [selectedHeightLines, setSelectedHeightLines] = useState<{ index: number; heightLines: MapCore.IMcMapHeightLines; name: string; }>(null);

    let existingHeightLinesArr = MapCoreData.heightLinesArr.map((heightLines: MapCore.IMcMapHeightLines, i: number) => ({
        index: i + 1,
        heightLines: heightLines,
        name: `${i + 1}) ${mapWorldTreeService.getTreeNodeLabel(heightLines, mapWorldNodeType.MAP_HEIGHT_LINES)}`
    }))

    useEffect(() => {
        runCodeSafely(() => {
            const root = document.documentElement;
            let pixelWidth = window.innerHeight * 0.35 * globalSizeFactor;
            root.style.setProperty('--select-existing-height-lines-dialog-width', `${pixelWidth}px`);
        }, 'selectExistingHeightLines.useEffect')
    }, [])

    return <div className='form__column-container'>
        <ListBox options={existingHeightLinesArr} optionLabel="name" value={selectedHeightLines} onChange={(event) => setSelectedHeightLines(event.target.value)} />
        <Button className="form__align-self-end" label='OK' onClick={e => props.handleOKClick(selectedHeightLines?.heightLines)} />
    </div>
}
