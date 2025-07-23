import { ListBox, ListBoxChangeEvent } from "primereact/listbox"
import { useEffect, useState, ReactElement } from "react"
import { Button } from "primereact/button";
import { useDispatch, useSelector } from "react-redux";
import { ConfirmDialog } from "primereact/confirmdialog";
import _ from 'lodash';

import { ObjectWorldService, MapCoreData, ViewportData, getEnumValueDetails, getEnumDetailsList } from 'mapcore-lib';
import './styles/objectItemsSelectList.css';
import TreeNodeModel, { hideFormReasons } from '../../../../shared/models/tree-node.model';
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";
import { AppState } from "../../../../../redux/combineReducer";
import objectWorldTreeService from "../../../../../services/objectWorldTree.service";
import { setObjectWorldTree, setShowDialog } from "../../../../../redux/ObjectWorldTree/objectWorldTreeActions";
import { closeDialog } from "../../../../../redux/MapCore/mapCoreAction";
import { DialogTypesEnum } from "../../../../../tools/enum/enums";

export default function ObjectItemsSelectList(props: { footerHook: (footer: () => ReactElement) => void, handleOKFunc: (object: MapCore.IMcObject, item: MapCore.IMcObjectSchemeItem) => void }) {
    const dispatch = useDispatch();
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const activeViewport: ViewportData = useSelector((state: AppState) => MapCoreData.findViewport(state.mapWindowReducer.activeCard));
    const dialogTypesArr: DialogTypesEnum[] = useSelector((state: AppState) => state.mapCoreReducer.dialogTypesArr);
    let treeRedux = useSelector((state: AppState) => state.objectWorldTreeReducer.objectWorldTree);
    const screenPos: any = useSelector((state: AppState) => state.mapWindowReducer.screenPos);
    let [objectItemsSelectListData, setObjectItemsSelectListData] = useState({
        isConfirmDialog: false,
        isChoosePointMode: false,
        activeViewportObjects: [],
        selectedObject: null,
        selectedItem: null,
        currentObjectItems: [],
    });

    //#region UseEffects
    useEffect(() => {
        setObjectItemSelectListDialogWidth()
        updateTreeRedux()
    }, [])
    useEffect(() => {
        if (treeRedux.id != '') {
            let activeVpObjects = getActiveViewportObjects();
            setObjectItemsSelectListData({ ...objectItemsSelectListData, activeViewportObjects: activeVpObjects })
        }
    }, [treeRedux])
    useEffect(() => {
        props.footerHook(getFooter);
    }, [objectItemsSelectListData, dialogTypesArr])
    useEffect(() => {
        runCodeSafely(() => {
            if (objectItemsSelectListData.isChoosePointMode) {
                let foundedMcNode = ObjectWorldService.findNodeByChosenScreenPoint(screenPos, activeViewport.viewport, 'ObjectWithItem') as { object: MapCore.IMcObject, item: MapCore.IMcObjectSchemeItem };
                if (foundedMcNode) {
                    let foundedTreeNodeObject = objectWorldTreeService.convertMapcorObjectToTreeNodeModel(treeRedux, foundedMcNode.object);
                    let foundedTreeNodeItem = objectWorldTreeService.convertMapcorObjectToTreeNodeModel(treeRedux, foundedMcNode.item);
                    let itemsAndDefaultSelectedItem = getObjectItems(foundedMcNode.object);
                    let selectListObjectsKeys = objectItemsSelectListData.activeViewportObjects.map(obj => obj.key).flat();
                    if (selectListObjectsKeys.includes(foundedTreeNodeObject.key)) {
                        setObjectItemsSelectListData({
                            ...objectItemsSelectListData,
                            selectedObject: foundedTreeNodeObject,
                            selectedItem: foundedTreeNodeItem,
                            currentObjectItems: itemsAndDefaultSelectedItem.items,
                            isChoosePointMode: false
                        })
                        dispatch(setShowDialog({ hideFormReason: null, dialogType: null }));
                    }
                }
                // else {
                //     let lastDialogType = dialogTypesArr[dialogTypesArr.length - 1];
                //     dispatch(setShowDialog({ hideFormReason: hideFormReasons.CHOOSE_POINT, dialogType: lastDialogType }));
                // }
            }
        }, 'objectItemSelectList.useEffect => screenPos')
    }, [screenPos])
    //#endregion
    //#region Functions
    const saveData = (event: any) => {
        runCodeSafely(() => {
            setObjectItemsSelectListData({ ...objectItemsSelectListData, [event.target.name]: event.target.type === "checkbox" ? event.target.checked : event.target.value })
        }, "objectItemsSelectList.saveData => onChange")
    }
    const updateTreeRedux = () => {
        runCodeSafely(() => {
            let buildedTree: TreeNodeModel = objectWorldTreeService.buildTree();
            dispatch(setObjectWorldTree(buildedTree));
        }, "objectWorldTreeDialog.useEffect");
    }
    function setObjectItemSelectListDialogWidth() {
        const root = document.documentElement;
        let pixelWidth = window.innerHeight * 0.4 * globalSizeFactor;
        root.style.setProperty('--obj-item-select-list-dialog-width', `${pixelWidth}px`);
    }

    const getActiveViewportObjects = () => {
        let viewportOM: MapCore.IMcOverlayManager = activeViewport.viewport.GetOverlayManager();
        let overlays = viewportOM.GetOverlays();
        let vpObjectsArr: TreeNodeModel[] = [];
        overlays.forEach((overlay: MapCore.IMcOverlay) => {
            let mcObjects = overlay.GetObjects();
            mcObjects.forEach((mcObject: MapCore.IMcObject) => {
                let treeNodeModel = objectWorldTreeService.convertMapcorObjectToTreeNodeModel(treeRedux, mcObject) as TreeNodeModel
                vpObjectsArr = [...vpObjectsArr, treeNodeModel];
            })
        })
        return vpObjectsArr;
    }
    const getObjectItems = (mcObject: MapCore.IMcObject): { items: TreeNodeModel[], defaultSelectedItem: TreeNodeModel } => {
        let mcScheme = mcObject.GetScheme();
        let defaultSelectedMcItem: MapCore.IMcObjectSchemeItem = mcScheme.GetEditModeDefaultItem();
        let defaultItem: TreeNodeModel = objectWorldTreeService.convertMapcorObjectToTreeNodeModel(treeRedux, defaultSelectedMcItem) as TreeNodeModel;
        let nodeKindArr = getEnumDetailsList(MapCore.IMcObjectSchemeNode.ENodeKindFlags);
        let anyItemEnum = getEnumValueDetails(MapCore.IMcObjectSchemeNode.ENodeKindFlags.ENKF_ANY_ITEM, nodeKindArr);
        let mcItems: MapCore.IMcObjectSchemeNode[] = mcScheme.GetNodes(anyItemEnum.code);
        let treeNodeModelItemsArr: TreeNodeModel[] = [];
        mcItems.forEach((mcItem: MapCore.IMcObjectSchemeNode) => {
            let treeNodeModel = objectWorldTreeService.convertMapcorObjectToTreeNodeModel(treeRedux, mcItem) as TreeNodeModel
            treeNodeModelItemsArr = [...treeNodeModelItemsArr, treeNodeModel];
        });

        let defaultSelectedItem: TreeNodeModel = null;
        if (defaultItem) {
            let nodeMcContent = defaultItem.nodeMcContent;
            let copiedDefaultItem = _.cloneDeep(defaultItem);
            copiedDefaultItem.label = `Default: ${defaultItem.label}`;
            copiedDefaultItem.nodeMcContent = nodeMcContent;
            defaultItem = copiedDefaultItem;
            defaultSelectedItem = copiedDefaultItem;
        }
        else {
            defaultItem = { key: '', id: '', label: 'Default: None', nodeType: null, nodeMcContent: null }
            defaultSelectedItem = treeNodeModelItemsArr.length == 1 ? treeNodeModelItemsArr[0] : defaultItem;
        }
        treeNodeModelItemsArr = treeNodeModelItemsArr.length > 0 ? [defaultItem, ...treeNodeModelItemsArr] : [];
        return { items: treeNodeModelItemsArr, defaultSelectedItem: defaultSelectedItem };
    }
    const getFooter = () => {
        return <div className="form__footer-padding obj-item-list__buttons-container">
            <Button className="obj-item-list__margin-button" label="OK" onClick={handleOKClick} />
            <Button className="obj-item-list__margin-button" label="..." onClick={handleChoosePointClick} />
        </div>
    }
    //Handle Functions
    const handleChoosePointClick = () => {
        setObjectItemsSelectListData({ ...objectItemsSelectListData, isChoosePointMode: true })
        let lastDialogType = dialogTypesArr[dialogTypesArr.length - 1];
        dispatch(setShowDialog({ hideFormReason: hideFormReasons.CHOOSE_POINT, dialogType: lastDialogType }));
    }
    const handleObjectSelected = (e: ListBoxChangeEvent) => {
        let itemsAndDefaultSelectedItem = getObjectItems(e.value.nodeMcContent);
        setObjectItemsSelectListData({
            ...objectItemsSelectListData,
            currentObjectItems: itemsAndDefaultSelectedItem.items,
            selectedItem: itemsAndDefaultSelectedItem.defaultSelectedItem,
            selectedObject: e.value
        })
    }
    const handleOKClick = () => {
        if (objectItemsSelectListData.selectedObject?.nodeMcContent && objectItemsSelectListData.selectedItem?.nodeMcContent) {
            props.handleOKFunc(objectItemsSelectListData.selectedObject.nodeMcContent, objectItemsSelectListData.selectedItem.nodeMcContent);
            let lastDialogType = dialogTypesArr[dialogTypesArr.length - 1];
            dispatch(closeDialog(lastDialogType))
        } else {
            setObjectItemsSelectListData({ ...objectItemsSelectListData, isConfirmDialog: true })
        }
    }
    //#endregion
    return <div>
        <div className="obj-item-list__lists-container">
            <ListBox emptyMessage={() => { return <div></div> }} listStyle={{ minHeight: `${globalSizeFactor * 20}vh`, maxHeight: `${globalSizeFactor * 20}vh`, width: '10rem' }} name='selectedObject' value={objectItemsSelectListData.selectedObject} onChange={handleObjectSelected} optionLabel="label" options={objectItemsSelectListData.activeViewportObjects} />
            <ListBox emptyMessage={() => { return <div></div> }} listStyle={{ minHeight: `${globalSizeFactor * 20}vh`, maxHeight: `${globalSizeFactor * 20}vh`, width: '10rem' }} name='selectedItem' value={objectItemsSelectListData.selectedItem} onChange={saveData} optionLabel="label" options={objectItemsSelectListData.currentObjectItems} />
        </div>

        <ConfirmDialog
            contentClassName='form__confirm-dialog-content'
            message='You must choose both, Object and an item.'
            header='Invalid Object'
            footer={<div></div>}
            visible={objectItemsSelectListData.isConfirmDialog}
            onHide={e => { setObjectItemsSelectListData({ ...objectItemsSelectListData, isConfirmDialog: false }) }}
        />
    </div>
}