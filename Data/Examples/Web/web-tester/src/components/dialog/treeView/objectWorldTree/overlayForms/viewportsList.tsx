

import { useDispatch, useSelector } from "react-redux";
import { AppState } from "../../../../../redux/combineReducer";
import objectWorldTreeService from "../../../../../services/objectWorldTree.service";
import { useEffect, useState } from "react";
import { ListBox } from "primereact/listbox";
import { Button } from "primereact/button";
import { objectWorldNodeType } from "../../../../shared/models/tree-node.model";
import { ViewportData } from 'mapcore-lib';
import { setObjectWorldTree, setTypeObjectWorldDialogSecond } from "../../../../../redux/ObjectWorldTree/objectWorldTreeActions";
import generalService from "../../../../../services/general.service";
import './styles/viewportsList.css';

export default function ViewportsList(props: { getSelectedViewports: (selectedViewport: ViewportData) => void }) {
    const dispatch = useDispatch();
    let selectedNodeInTree = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);
    let globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor);
    let treeRedux = useSelector((state: AppState) => state.objectWorldTreeReducer.objectWorldTree);

    const getViewports = () => {
        let currentOverlay = selectedNodeInTree;
        let mcViewports: ViewportData[] = objectWorldTreeService.getOMMCViewportsByOverlay(treeRedux, currentOverlay);
        let vpList = mcViewports.map(vp => { return { viewport: vp, label: generalService.getObjectName(vp, "Viewport") } });
        return vpList;
    }

    let [viewportsArr] = useState(getViewports());
    let [selectedViewport, setSelectedViewport] = useState(null);

    useEffect(() => {
        const root = document.documentElement;
        let pixelWidth = window.innerHeight * 0.3 * globalSizeFactor;
        root.style.setProperty('--viewports-list-dialog-width', `${pixelWidth}px`);
    }, [])

    const handleOKClick = () => {
        props.getSelectedViewports(selectedViewport.viewport);
        dispatch(setTypeObjectWorldDialogSecond(undefined));
    }
    const handleClearClick = () => {
        setSelectedViewport(null);
    }

    return (
        <div style={{ display: 'flex', flexDirection: 'column' }}>
            <ListBox optionLabel='label' value={selectedViewport} onChange={e => setSelectedViewport(e.value)} options={viewportsArr} />
            <div style={{ display: 'flex', justifyContent: 'flex-end' }} >
                <Button label="Clear" onClick={handleClearClick} />
                <Button label="OK" onClick={handleOKClick} />
            </div>
        </div>
    )
}
