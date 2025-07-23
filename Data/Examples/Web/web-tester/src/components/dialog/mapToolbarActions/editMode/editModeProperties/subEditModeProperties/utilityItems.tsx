import { useState, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { DataTable } from 'primereact/datatable';
import { Button } from 'primereact/button';
import { Column } from 'primereact/column';
import _ from 'lodash'

import { getEnumDetailsList } from 'mapcore-lib';
import '../styles/editModePropertiesDialog.css';
import { EditModeProperties, Utility3DEditItem, UtilityPictureItem } from '../editModePropertiesDialog';
import SelectExistingItem from '../../../../shared/ControlsForMapcoreObjects/itemCtrl/selectExistingItem';
import { runCodeSafely } from '../../../../../../common/services/error-handling/errorHandler';
import { setTypeEditModeDialogSecond } from '../../../../../../redux/EditMode/editModeAction';
import { AppState } from '../../../../../../redux/combineReducer';

// export default 
export default function UtilityItems(props: { dialogProperties: EditModeProperties, setDialogPropertiesCallback: (key: string, value: any) => void }) {
    const dispatch = useDispatch();
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [enumDetails] = useState({
        utilityPictureType: getEnumDetailsList(MapCore.IMcEditMode.EUtilityPictureType),
        utility3DEditItemType: getEnumDetailsList(MapCore.IMcEditMode.EUtility3DEditItemType),
    });

    const [utilityItems, setUtilityItems] = useState([
        {
            itemTypeStr: "Rectangle",
            itemType: MapCore.IMcRectangleItem.NODE_TYPE,
            item: props.dialogProperties.RectangleUtilityItem
        },
        {
            itemTypeStr: "Line",
            itemType: MapCore.IMcLineItem.NODE_TYPE,
            item: props.dialogProperties.LineUtilityItem
        },
        {
            itemTypeStr: "Text",
            itemType: MapCore.IMcTextItem.NODE_TYPE,
            item: props.dialogProperties.TextUtilityItem
        },
    ])

    useEffect(() => {
        props.setDialogPropertiesCallback("RectangleUtilityItem", utilityItems.find(utilityItem => utilityItem.itemTypeStr === "Rectangle")?.item as MapCore.IMcRectangleItem);
        props.setDialogPropertiesCallback("LineUtilityItem", utilityItems.find(utilityItem => utilityItem.itemTypeStr === "Line")?.item as MapCore.IMcLineItem);
        props.setDialogPropertiesCallback("TextUtilityItem", utilityItems.find(utilityItem => utilityItem.itemTypeStr === "Text")?.item as MapCore.IMcTextItem);
    }, [utilityItems])

    const showSelectExistingItemDialog = (setSelectedItem: (item: MapCore.IMcObjectSchemeNode | null) => void, itemType?: number) => {
        runCodeSafely(() => {
            dispatch(setTypeEditModeDialogSecond({
                secondDialogHeader: 'Select Existing Items',
                secondDialogComponent: <SelectExistingItem finalActionButton itemType={itemType} handleOKClick={(selectedItem: MapCore.IMcObjectSchemeItem) => {
                    dispatch(setTypeEditModeDialogSecond(undefined));
                    setSelectedItem(selectedItem);
                }} />
            }));
        }, "EditModePropertiesDialog/MapManipulationsOperations => selectStandAloneRectangle");
    }

    const setSelectedUtilityItem = (itemTypeStr: string) => {
        return function (item: MapCore.IMcObjectSchemeNode | null) {
            let updatedUtilityItems = [...utilityItems];
            let currUtilityItem = utilityItems.find(utilityItem => utilityItem.itemTypeStr === itemTypeStr)
            if (currUtilityItem) {
                currUtilityItem.item = item as MapCore.IMcRectangleItem | MapCore.IMcLineItem | MapCore.IMcTextItem
            }
            setUtilityItems(updatedUtilityItems);
        }
    }

    const setSelectedPictureItem = (rowIndex: number) => {
        return function (item: MapCore.IMcObjectSchemeNode | null) {
            let updatedUtilityPictureItems = [...props.dialogProperties.UtilityPictureItems];
            updatedUtilityPictureItems[rowIndex].pictureItem = item as MapCore.IMcPictureItem;
            props.setDialogPropertiesCallback("UtilityPictureItems", updatedUtilityPictureItems);
        }
    }

    const setSelected3DEditItem = (rowIndex: number) => {
        return function (item: MapCore.IMcObjectSchemeNode | null) {
            let updatedUtilityPictureItems = [...props.dialogProperties.Utility3DEditItems];
            updatedUtilityPictureItems[rowIndex].editItem = item as MapCore.IMcObjectSchemeItem;
            props.setDialogPropertiesCallback("Utility3DEditItems", updatedUtilityPictureItems);
        }
    }
    return (
        <div style={{ height: '100%' }}>
            <div>
                <h3>Utility Items</h3>
                <DataTable size='small' value={utilityItems}>
                    <Column style={{ width: '50%' }} field="itemTypeStr" header="Type" body={(rowData: any) => { return rowData.itemTypeStr }} ></Column>
                    <Column field="item" header="Symbolic Item"
                        body={(rowData: any) => {
                            return <div><Button label={`Select ${rowData.itemTypeStr}`} style={{ width: '30%', margin: `${globalSizeFactor * 0.3}vh` }}
                                onClick={() => showSelectExistingItemDialog(setSelectedUtilityItem(rowData.itemTypeStr), rowData.itemType)} />
                                <span className='em-props__offset'>{rowData.pictureItem ? rowData.pictureItem.GetName() : "Null"}</span></div>
                        }}
                    />
                </DataTable>
            </div>
            <div>
                <h3>Utility Picture</h3>
                <DataTable size='small' value={props.dialogProperties.UtilityPictureItems}>
                    <Column style={{ width: '50%' }} field="utilityPictureType" header="Type" body={(rowData: UtilityPictureItem) => { return enumDetails.utilityPictureType.find(enumDetails => enumDetails.theEnum === rowData.utilityPictureType)?.name }} ></Column>
                    <Column field="pictureItem" header="Picture Item"
                        body={(rowData: UtilityPictureItem) => {
                            return <div><Button label='Select Picture' style={{ width: '30%', margin: `${globalSizeFactor * 0.3}vh` }}
                                onClick={() => showSelectExistingItemDialog(setSelectedPictureItem(props.dialogProperties.UtilityPictureItems.findIndex(item => rowData.utilityPictureType === item.utilityPictureType)), MapCore.IMcPictureItem.NODE_TYPE)} />
                                <span className='em-props__offset'>{rowData.pictureItem ? rowData.pictureItem.GetName() : "Null"}</span></div>
                        }}
                    />
                </DataTable>
            </div>
            <div>
                <h3>Utility 3D Edit Item</h3>
                <DataTable size='small' value={props.dialogProperties.Utility3DEditItems}>
                    <Column style={{ width: '50%' }} field="utility3DEditItemType" header="Type" body={(rowData: Utility3DEditItem) => { return enumDetails.utility3DEditItemType.find(enumDetails => enumDetails.theEnum === rowData.utility3DEditItemType)?.name }} ></Column>
                    <Column field="editItem" header="Edit Item"
                        body={(rowData: Utility3DEditItem) => {
                            return <div><Button label='Select Item' style={{ width: '30%', margin: `${globalSizeFactor * 0.3}vh` }}
                                onClick={() => showSelectExistingItemDialog(setSelected3DEditItem(props.dialogProperties.Utility3DEditItems.findIndex(item => rowData.utility3DEditItemType === item.utility3DEditItemType)))} />
                                <span className='em-props__offset'>{rowData.editItem ? rowData.editItem.GetName() : "Null"}</span></div>
                        }}
                    />
                </DataTable>
            </div>
        </div >
    )
};