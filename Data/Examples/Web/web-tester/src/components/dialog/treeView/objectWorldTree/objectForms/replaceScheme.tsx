import { Button } from "primereact/button";
import { ListBox } from "primereact/listbox";
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { AppState } from "../../../../../redux/combineReducer";
import objectWorldTreeService from "../../../../../services/objectWorldTree.service";
import { objectWorldNodeType } from "../../../../shared/models/tree-node.model";
import { Checkbox } from "primereact/checkbox";
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";
import { setObjectWorldTree, setTypeObjectWorldDialogSecond } from "../../../../../redux/ObjectWorldTree/objectWorldTreeActions";
import TreeNodeModel from "../../../../shared/models/tree-node.model"
import './styles/replaceScheme.css';

export default function ReplaceScheme() {
    const dispatch = useDispatch();
    let selectedNodeInTree = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);
    let treeRedux = useSelector((state: AppState) => state.objectWorldTreeReducer.objectWorldTree);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    const getSchemes = () => {
        let overlayManagerKey = selectedNodeInTree.key.substring(0, selectedNodeInTree.key.length - 4);
        let overlayManagerChildren = (objectWorldTreeService.getNodeByKey(treeRedux, overlayManagerKey) as TreeNodeModel).children;
        let schemesArr = overlayManagerChildren.filter(child => child.nodeType == objectWorldNodeType.OBJECT_SCHEME);
        //need also to filter the schemes by function like GetSchemesWithoutTempSchemes in orit's tester - sm
        return schemesArr;

    }

    let [isKeepRelevantProperties, setIsKeepRelevantProperties] = useState(false)
    let [schemesArr] = useState(getSchemes())
    let [selectedScheme, setSelectedScheme] = useState(null)

    useEffect(() => {
        const root = document.documentElement;
        let pixelWidth = window.innerHeight * 0.3 * globalSizeFactor;
        root.style.setProperty('--replace-scheme-dialog-width', `${pixelWidth}px`);
    }, [])
    const handleOKClick = () => {
        runCodeSafely(() => {
            let mcCurrentObject = selectedNodeInTree.nodeMcContent as MapCore.IMcObject;
            mcCurrentObject.SetScheme(selectedScheme.nodeMcContent, isKeepRelevantProperties);
            let tree = objectWorldTreeService.buildTree();
            dispatch(setObjectWorldTree(tree));
            dispatch(setTypeObjectWorldDialogSecond(undefined));
        }, 'replaceSheme.handleOKClick')
    }

    return <div>
        <ListBox optionLabel='label' value={selectedScheme} onChange={e => setSelectedScheme(e.value)} options={schemesArr} />
        <div style={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between', paddingTop: `${globalSizeFactor * 2}vh` }} >
            <div style={{ padding: `${globalSizeFactor * 1}vh` }}>
                <Checkbox style={{ padding: `${globalSizeFactor * 0.15}vh` }} inputId="isKeepRelevantProperties" onChange={e => setIsKeepRelevantProperties(e.checked)} checked={isKeepRelevantProperties} />
                <label htmlFor="isKeepRelevantProperties">Keep Relevant Properties</label>
            </div>
            <Button label="OK" onClick={handleOKClick} />
        </div>
    </div>
}