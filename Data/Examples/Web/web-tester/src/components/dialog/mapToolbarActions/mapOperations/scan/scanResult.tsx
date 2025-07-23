// External libraries
import { useState, useEffect, ReactElement } from 'react';
import { useDispatch, useSelector } from 'react-redux';

// UI/component libraries
import { Dialog } from 'primereact/dialog';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Button } from 'primereact/button';

// Project-specific imports
import { MapCoreData, ScanService, getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import ScanItemDetailsLayer from './SubScanResult/scanItemDetailsLayer';
import ScanItemDetailsObject from './SubScanResult/scanItemDetailsObject';
import EditColorProperties from './SubScanResult/editColorProperties';
import EditObjectHeightProperties from './SubScanResult/editObjectHeightProperties';
import VectorField from './SubScanResult/vectorField';
import ScanItem3DModelSmartReality from './SubScanResult/ScanItem3DModelSmartReality';
import { DialogTypesEnum } from '../../../../../tools/enum/enums';
import { AppState } from '../../../../../redux/combineReducer';
import { closeDialog, setDisplayTarget } from '../../../../../redux/MapCore/mapCoreAction';
import { runCodeSafely } from '../../../../../common/services/error-handling/errorHandler';

export default function ScanResult(props: { FooterHook: (footer: () => ReactElement) => void }) {
    const [targetFound, setTargetFound] = useState<MapCore.IMcSpatialQueries.STargetFound[]>(MapCoreData.TargetFound);
    // const [displayTarget, setDisplayTarget] = useState<any[]>(MapCoreData.TargetFound);
    const [removeColor, setRemoveColor] = useState({ func: () => { } });
    const [removeHieght, setRemoveHieght] = useState({ func: () => { } });
    const currentViewport: number = useSelector((state: AppState) => state.mapWindowReducer.currentViewport);
    const displayTarget = useSelector((state: AppState) => state.mapCoreReducer.displayTarget);
    const [columnName, setColumnName] = useState("");
    const [openMoreDetails, setopenMoreDetails] = useState(false);
    const [SelectTarget, setSelectTarget] = useState<MapCore.IMcSpatialQueries.STargetFound>(null);
    const [listItem, setListItem] = useState([]);

    const dispatch = useDispatch();
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const activeCard = useSelector((state: AppState) => state.mapWindowReducer.activeCard);
    const [enumDetails] = useState({
        PointCoordSystem: getEnumDetailsList(MapCore.EMcPointCoordSystem),
        TargetType: getEnumDetailsList(MapCore.IMcSpatialQueries.EIntersectionTargetType),
        ItemPart: getEnumDetailsList(MapCore.IMcSpatialQueries.EItemPart)
    });
    const func4 = (arrItem: []) => {
        arrItem.forEach((item: any) => {
            if (item.interfaceName) {
                let fullname = "MapCore." + item.interfaceName + ".NODE_TYPE";
                let code = eval(fullname)
                let li = listItem
                li.push({ code: code, displayName: item.displayName })
                setListItem(li)
            }
            else
                func4(item.elements)
        });
    }
    useEffect(() => {
        const root = document.documentElement;
        let pixelWidth = window.innerHeight * 1 * globalSizeFactor;
        root.style.setProperty('--scan-result-dialog-width', `${pixelWidth}px`);

        let res: any = MapCore.IMcObjectScheme.SaveSchemeComponentInterface(MapCore.IMcObjectScheme.ESchemeComponentKind.ESCK_OBJECT_SCHEME_NODE, 0)
        res = JSON.parse(String.fromCharCode.apply(null, res))
        console.log(res.objectSchemeNodes[1].elements);
        let arrItem = res.objectSchemeNodes[1].elements;
        func4(arrItem)
    }, [])

    useEffect(() => {
        runCodeSafely(() => {
            editProperties()
            overlay = ScanService.SetVectorColor(activeCard, targetFound)
        }, "useEffect.ScanResult")
        return () => {
            runCodeSafely(() => { removeVectorColor() }, "useEffect.ScanResult")
            removeHieght.func();
            removeColor.func();
        }
    }, [])

    useEffect(() => {
        props.FooterHook(getScanResultFooter);
    }, [removeHieght, removeColor])

    const getScanResultFooter = () => {
        return (
            <div className='form__footer-padding'>
                <Button label="OK" onClick={() => {
                    dispatch(closeDialog(DialogTypesEnum.scanResult))
                }} />
            </div>
        );
    }

    const editProperties = () => {
        let editTargetFound: any = targetFound.map((target: MapCore.IMcSpatialQueries.STargetFound, index) => {
            let ItemColor = null;
            let color = ScanService.ifVector3DExtrusion(target.pTerrainLayer)?.GetObjectColor(target.uTargetID);
            if (color)
                ItemColor = "(" + color.r + "," + color.g + "," + color.b + "," + color.a + ")"
            let bitCount = 32;
            if (target.eTargetType == MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_STATIC_OBJECTS_LAYER &&
                ScanService.ifVector3DExtrusion(target.pTerrainLayer)) {
                bitCount = ScanService.ifVector3DExtrusion(target.pTerrainLayer).GetObjectIDBitCount();
            }
            else if (ScanService.ifVectorLayer(target)) {
                bitCount = 64;;
            }
            return {
                index: index + 1,
                target,
                coordSystem: getEnumValueDetails(target.eIntersectionCoordSystem, enumDetails.PointCoordSystem),
                Target_Type: getEnumValueDetails(target.eTargetType, enumDetails.TargetType),
                TerrainID: target.pTerrain?.GetID() == MapCore.MC_EMPTY_ID ? "EMPTY" : target.pTerrain?.GetID(),
                LayerID: target.pTerrainLayer?.GetID() == MapCore.MC_EMPTY_ID ? "EMPTY" : target.pTerrainLayer?.GetID(),
                TargetID: ScanService.GetTargetIdByBitCount(target) == 0 ? "" : ScanService.GetTargetIdByBitCount(target),
                ObjectID: target.ObjectItemData.pObject?.GetID(),
                ItemID: target.ObjectItemData.pItem?.GetID(),
                SubItemID: target.ObjectItemData.uSubItemID == MapCore.MC_EMPTY_ID ? "EMPTY" : target.ObjectItemData.uSubItemID,
                ItemType: listItem.find(r => r.code == target.ObjectItemData.pItem?.GetNodeType())?.displayName,
                ItemHeight: ScanService.ifVector3DExtrusion(target.pTerrainLayer)?.GetObjectExtrusionHeight(target.uTargetID) === MapCore.FLT_MAX ? "Original" : ScanService.ifVector3DExtrusion(target.pTerrainLayer)?.GetObjectExtrusionHeight(target.uTargetID),
                PartFound: getEnumValueDetails(target.ObjectItemData.ePartFound, enumDetails.ItemPart),
                PartIndex: target.ObjectItemData.uPartIndex,
                ItemColor: ItemColor,
                MoreDetails: null
            }
        })
        dispatch(setDisplayTarget(editTargetFound))
    }
    let overlay: MapCore.IMcOverlay;

    const removeVectorColor = () => {
        if (overlay != null)
            ScanService.RemoveColorVerctor(overlay);
    }
    const setName = (name: string) => {
        setColumnName(name)
    }
    const getDialogClassName = (header: string) => {
        switch (header) {
            case 'Smart Reality Building History':
                return 'scroll-dialog-building-history';
            default:
                return '';
        }
    }
    const getMoreDetailsDialogContent = () => {
        switch (SelectTarget.eTargetType) {
            case MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_OVERLAY_MANAGER_OBJECT:
                return <ScanItemDetailsObject target={SelectTarget} />
            case MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_VISIBLE_VECTOR_LAYER:
                return <ScanItemDetailsLayer target={SelectTarget} />
            case MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_STATIC_OBJECTS_LAYER:
                return <ScanItem3DModelSmartReality target={SelectTarget} />
            default:
                break;
        }
    }
    const getMoreDetailsDialogHeader = () => {
        switch (SelectTarget.eTargetType) {
            case MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_OVERLAY_MANAGER_OBJECT:
            case MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_VISIBLE_VECTOR_LAYER:
                return 'Scan Item Details'
            case MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_STATIC_OBJECTS_LAYER:
                return 'Smart Reality Building History'
            default:
                break;
        }
    }
    return (
        <div className='scan__scan-result-form'>
            <div style={{ display: 'flex' }}>
                <EditColorProperties setParent={(func: any) => { removeColor.func = func }}  ></EditColorProperties>
                <EditObjectHeightProperties setParent={(func: any) => { removeHieght.func = func }}></EditObjectHeightProperties>
                <VectorField setName={(name: string) => setName(name)}></VectorField>
            </div>
            <div className="card" style={{ overflowY: 'auto', maxHeight: `${globalSizeFactor * 40}vh` }} >
                <DataTable
                    showGridlines
                    value={displayTarget}
                    tableStyle={{ minWidth: '50rem' }} size='small'
                    selectionMode='single'>
                    <Column field="index"></Column>
                    <Column field="TargetID" header="Target ID"></Column>
                    <Column field="Target_Type.name" header="Target Type"></Column>
                    <Column field="target.IntersectionPoint.x" header="IntersectionPoint x"></Column>
                    <Column field="target.IntersectionPoint.y" header="IntersectionPoint y"></Column>
                    <Column field="target.IntersectionPoint.z" header="IntersectionPoint z"></Column>
                    <Column field="coordSystem.name" header="coordSystem"></Column>
                    <Column field="TerrainID" header="Terrain ID"></Column>
                    <Column field="LayerID" header="Layer ID"></Column>
                    <Column field="ObjectID" header="Object ID"></Column>
                    <Column field="ItemID" header="Item ID"></Column>
                    <Column field="SubItemID" header="SubItem ID"></Column>
                    <Column field="PartFound.name" header="Part Found"></Column>
                    <Column field="PartIndex" header="Part Index"></Column>
                    <Column field="ItemType" header="Item Type"></Column>
                    <Column field="ItemHeight" header="Item Height"></Column>
                    {columnName != "None" && <Column field="columnName" header={columnName}></Column>}
                    <Column field="MoreDetails" header="More Details" body={(rowData) => {
                        return <><Button onClick={() => {
                            let target = rowData.target;
                            setSelectTarget(target)
                            if ((target.eTargetType == MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_OVERLAY_MANAGER_OBJECT) ||
                                (target.eTargetType == MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_STATIC_OBJECTS_LAYER) ||
                                (target.eTargetType == MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_VISIBLE_VECTOR_LAYER)) {
                                setopenMoreDetails(true)
                            }
                        }
                        }>details</Button></>
                    }}></Column>
                </DataTable>
                {SelectTarget != null && <Dialog className={`${getDialogClassName(getMoreDetailsDialogHeader())}`} header={getMoreDetailsDialogHeader()} visible={openMoreDetails} onHide={() => { setopenMoreDetails(false) }}>
                    {getMoreDetailsDialogContent()}
                </Dialog>}
            </div>
        </div>
    );
}




