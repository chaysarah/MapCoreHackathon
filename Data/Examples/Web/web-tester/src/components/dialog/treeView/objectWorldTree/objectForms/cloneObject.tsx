import { Button } from "primereact/button";
import { Checkbox } from "primereact/checkbox";
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { setObjectWorldTree, setTypeObjectWorldDialogSecond } from "../../../../../redux/ObjectWorldTree/objectWorldTreeActions";
import { AppState } from "../../../../../redux/combineReducer";
import objectWorldTreeService from "../../../../../services/objectWorldTree.service";
import './styles/cloneObject.css';

export default function CloneObject() {
    const dispatch = useDispatch()
    let selectedNodeInTree = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);
    let treeRedux = useSelector((state: AppState) => state.objectWorldTreeReducer.objectWorldTree);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    let [isCloneLocationPoints, setIsCloneLocationPoints] = useState(true);
    let [isCloneObjectScheme, setIsCloneObjectScheme] = useState(false);

    useEffect(() => {
        const root = document.documentElement;
        let pixelWidth = window.innerHeight * 0.3 * globalSizeFactor;
        root.style.setProperty('--clone-object-dialog-width', `${pixelWidth}px`);
    }, [])
    const handleCancelClick = () => {
        dispatch(setTypeObjectWorldDialogSecond(undefined))
    }
    const handleOKClick = () => {
        let mcCurrentObject = selectedNodeInTree.nodeMcContent as MapCore.IMcObject;
        let mcCurrentOverlay = objectWorldTreeService.getParentByChildKey(treeRedux, selectedNodeInTree.key).nodeMcContent;
        mcCurrentObject.Clone(mcCurrentOverlay, isCloneObjectScheme, isCloneLocationPoints);
        let tree = objectWorldTreeService.buildTree()
        dispatch(setObjectWorldTree(tree))
        dispatch(setTypeObjectWorldDialogSecond(undefined))
    }

    return <div style={{ display: 'flex', flexDirection: 'column' }}>
        <div style={{ padding: `${globalSizeFactor * 1}vh` }}>
            <Checkbox style={{ padding: `${globalSizeFactor * 0.15}vh` }} inputId="isCloneObjectScheme" onChange={e => setIsCloneObjectScheme(e.checked)} checked={isCloneObjectScheme} />
            <label htmlFor="isCloneObjectScheme">Clone Object Scheme</label>
        </div>
        <div style={{ padding: `${globalSizeFactor * 1}vh` }}>
            <Checkbox style={{ padding: `${globalSizeFactor * 0.15}vh` }} inputId="isCloneLocationPoints" onChange={e => setIsCloneLocationPoints(e.checked)} checked={isCloneLocationPoints} />
            <label htmlFor="isCloneLocationPoints">Clone Location Points</label>
        </div>
        <div style={{ display: 'flex', flexDirection: 'row', justifyContent: 'flex-end', paddingTop: `${globalSizeFactor * 3}vh` }}>
            <Button onClick={handleOKClick} label="OK" />
            <Button onClick={handleCancelClick} label="Cancel" />
        </div>
    </div>
}