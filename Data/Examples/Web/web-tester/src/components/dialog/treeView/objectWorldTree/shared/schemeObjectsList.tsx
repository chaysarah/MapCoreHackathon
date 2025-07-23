

import { useDispatch, useSelector } from "react-redux";
// import objectWorldTreeService from "../../../../services/objectWorldTree.service";
import { useEffect, useState } from "react";
import { ListBox } from "primereact/listbox";
import { Button } from "primereact/button";
// import TreeNodeModel, { objectWorldNodeType } from "../../../shared/models/tree-node.model";
import { ViewportData } from 'mapcore-lib';
import { TreeNodeModel } from "../../../../../services/tree.service";
import { AppState } from "../../../../../redux/combineReducer";
import objectWorldTreeService from "../../../../../services/objectWorldTree.service";
import { setTypeObjectWorldDialogSecond } from "../../../../../redux/ObjectWorldTree/objectWorldTreeActions";
// import { setObjectWorldTree, setTypeObjectWorldDialogSecond } from "../../../../redux/ObjectWorldTree/objectWorldTreeActions";
import './styles/schemeObjectsList.css';

export default function SchemeObjectsList(props: { mcObjects: MapCore.IMcObject[], getSelectedObject: (selectedObject: MapCore.IMcObject) => void }) {
    const dispatch = useDispatch();
    let selectedNodeInTree = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);
    let treeRedux = useSelector((state: AppState) => state.objectWorldTreeReducer.objectWorldTree);
    let globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor);

    const getObjects = () => {
        let treeNodeObjectsArr: TreeNodeModel[] = [];
        props.mcObjects.forEach(mcObject => {
            let treeNodeObject = objectWorldTreeService.convertMapcorObjectToTreeNodeModel(treeRedux, mcObject) as TreeNodeModel;
            treeNodeObjectsArr = [...treeNodeObjectsArr, treeNodeObject];
        });
        return treeNodeObjectsArr;
    }

    let [objectsArr] = useState(getObjects());
    let [selectedObject, setSelectedObject] = useState(null);

    useEffect(() => {
        const root = document.documentElement;
        let pixelWidth = window.innerHeight * 0.3 * globalSizeFactor;
        root.style.setProperty('--scheme-obj-list-dialog-width', `${pixelWidth}px`);
    }, [])
    const handleOKClick = () => {
        props.getSelectedObject(selectedObject.viewport);
        dispatch(setTypeObjectWorldDialogSecond(undefined));
    }
    const handleClearClick = () => {
        setSelectedObject(null);
    }

    return (
        <div style={{ display: 'flex', flexDirection: 'column' }}>
            <ListBox optionLabel='label' value={selectedObject} onChange={e => setSelectedObject(e.value)} options={objectsArr} />
            <div style={{ display: 'flex', justifyContent: 'flex-end' }} >
                <Button label="Clear" onClick={handleClearClick} />
                <Button label="OK" onClick={handleOKClick} />
            </div>
        </div>
    )
}
