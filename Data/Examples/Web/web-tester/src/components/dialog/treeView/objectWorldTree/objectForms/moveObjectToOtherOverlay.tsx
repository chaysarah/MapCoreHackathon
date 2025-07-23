import { useDispatch, useSelector } from "react-redux";
import { AppState } from "../../../../../redux/combineReducer";
import objectWorldTreeService from "../../../../../services/objectWorldTree.service";
import { useEffect, useState } from "react";
import { ListBox } from "primereact/listbox";
import { Button } from "primereact/button";
import { objectWorldNodeType } from "../../../../shared/models/tree-node.model";
import { setObjectWorldTree, setTypeObjectWorldDialogSecond } from "../../../../../redux/ObjectWorldTree/objectWorldTreeActions";
import TreeNodeModel from "../../../../shared/models/tree-node.model"
import './styles/moveToOtherOverlay.css';

export default function MoveObjectToOtherOverlay() {
    const dispatch = useDispatch();
    let selectedNodeInTree = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);
    let globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor);
    let treeRedux = useSelector((state: AppState) => state.objectWorldTreeReducer.objectWorldTree);

    useEffect(() => {
        const root = document.documentElement;
        let pixelWidth = window.innerHeight * 0.3 * globalSizeFactor;
        root.style.setProperty('--move-to-other-overlay-dialog-width', `${pixelWidth}px`);
    }, [])
    const getOverlays = () => {
        let overlayManagerKey = selectedNodeInTree.key.substring(0, selectedNodeInTree.key.length - 4);
        let overlayManagerChildren = (objectWorldTreeService.getNodeByKey(treeRedux, overlayManagerKey) as TreeNodeModel).children;
        let overlaysArr = overlayManagerChildren.filter(child => child.nodeType == objectWorldNodeType.OVERLAY);
        return overlaysArr;
    }

    let [overlaysArr] = useState(getOverlays());
    let [selectedOverlay, setSelectedOverlay] = useState(null);

    const handleOKClick = () => {
        let mcCurrentObject = selectedNodeInTree.nodeMcContent as MapCore.IMcObject;
        mcCurrentObject.SetOverlay(selectedOverlay.nodeMcContent);
        let tree = objectWorldTreeService.buildTree()
        dispatch(setObjectWorldTree(tree))
        dispatch(setTypeObjectWorldDialogSecond(undefined))
    }

    return (
        <div style={{ display: 'flex', flexDirection: 'column' }}>
            <ListBox optionLabel='label' value={selectedOverlay} onChange={e => setSelectedOverlay(e.value)} options={overlaysArr} />
            <div style={{ display: 'flex', justifyContent: 'flex-end' }} >
                <Button label="OK" onClick={handleOKClick} />
            </div>
        </div>
    )
}