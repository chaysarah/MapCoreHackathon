import { ListBox } from 'primereact/listbox';
import { useDispatch, useSelector } from 'react-redux';
import { useEffect, useState } from 'react';
import { Button } from 'primereact/button';

import './styles/selectExistingItem.css';
import { objectWorldNodeType } from '../../../../shared/models/tree-node.model';
import { AppState } from '../../../../../redux/combineReducer';
import { runCodeSafely } from '../../../../../common/services/error-handling/errorHandler';
import objectWorldTreeService from '../../../../../services/objectWorldTree.service';
import { TreeNodeModel } from '../../../../../services/tree.service';
import { setObjectWorldTree } from '../../../../../redux/ObjectWorldTree/objectWorldTreeActions';

interface BaseSelectExistingItemProps {
    itemType?: number;
    selectedItem?: MapCore.IMcObjectSchemeItem;
}
interface WithActionProps extends BaseSelectExistingItemProps {
    finalActionButton: true;
    handleOKClick: (selectedItem: MapCore.IMcObjectSchemeItem) => void;
    getSelectedItem?: undefined,
};
interface WithoutActionProps extends BaseSelectExistingItemProps {
    finalActionButton?: undefined;
    handleOKClick?: undefined;
    getSelectedItem: (selectedItem: MapCore.IMcObjectSchemeItem) => void,
};
type SelectExistingItemProps = WithActionProps | WithoutActionProps;

export default function SelectExistingItem(props: SelectExistingItemProps) {
    const dispatch = useDispatch();
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const objectWorldTree = useSelector((state: AppState) => state.objectWorldTreeReducer.objectWorldTree)
    const standAloneItems = useSelector((state: AppState) => state.objectWorldTreeReducer.standAloneItems)
    const [selectedItem, setSelectedItem] = useState<TreeNodeModel>();
    const [existingItemsArr, setExistingItemsArr] = useState<TreeNodeModel[]>([]);

    //#region useEffects
    useEffect(() => {
        runCodeSafely(() => {
            if (props.finalActionButton) {
                const root = document.documentElement;
                let pixelWidth = window.innerHeight * 0.35 * globalSizeFactor;
                root.style.setProperty('--select-existing-item-dialog-width', `${pixelWidth}px`);
            }
        }, 'selectExistingItem.useEffect[mounting]')
    }, [])
    useEffect(() => {
        runCodeSafely(() => {
            const itemNodes: TreeNodeModel[] = objectWorldTreeService.findNodesByType(objectWorldTree, objectWorldNodeType.ITEM);
            const filteredItemNodes = props.itemType ? itemNodes.filter((itemNode: TreeNodeModel) => itemNode.nodeMcContent.GetNodeType() === props.itemType) : itemNodes;
            setExistingItemsArr(filteredItemNodes);
        }, 'selectExistingItem.useEffect[objectWorldTree]')
    }, [objectWorldTree])
    useEffect(() => {
        runCodeSafely(() => {
            let buildedTree: TreeNodeModel = objectWorldTreeService.buildTree();
            dispatch(setObjectWorldTree(buildedTree));
        }, 'selectExistingItem.useEffect[standAloneItems]')
    }, [standAloneItems])
    useEffect(() => {
        runCodeSafely(() => {
            const itemNode = objectWorldTreeService.convertMapcorObjectToTreeNodeModel(objectWorldTree, props.selectedItem)
            if (itemNode) {
                setSelectedItem(itemNode);
            }
        }, 'selectExistingItem.useEffect[props.selectedItem]')
    }, [props.selectedItem])
    //#endregion

    const handleSelectedItemChange = (e) => {
        runCodeSafely(() => {
            setSelectedItem(e.target.value);
            props.getSelectedItem && props.getSelectedItem(e.target.value ? e.target.value.nodeMcContent : null);
        }, 'selectExistingItem.handleSelectedItemChange')
    }

    return <div className='form__column-container'>
        <ListBox listStyle={{ minHeight: `${globalSizeFactor * 13}vh`, maxHeight: `${globalSizeFactor * 13}vh` }} options={existingItemsArr} optionLabel="label" value={selectedItem} onChange={handleSelectedItemChange} />
        {props.finalActionButton && <Button className="form__align-self-end" label='OK' onClick={e => props.handleOKClick(selectedItem ? selectedItem.nodeMcContent : null)} />}
    </div>
}