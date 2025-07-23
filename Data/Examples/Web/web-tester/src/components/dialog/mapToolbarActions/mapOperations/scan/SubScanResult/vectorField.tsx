import { useEffect, useState } from "react";
import { MapCoreData, ScanService } from 'mapcore-lib';
import { Dropdown } from "primereact/dropdown";
import { useDispatch } from "react-redux";
import { addDisplayTarget, setDisplayTarget } from "../../../../../../redux/MapCore/mapCoreAction";
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import { Button } from "primereact/button";
import { runCodeSafely } from "../../../../../../common/services/error-handling/errorHandler";
import { Fieldset } from "primereact/fieldset";

export default function VectorField(props: any) {
    const [targetFound, setTargetFound] = useState<MapCore.IMcSpatialQueries.STargetFound[]>(MapCoreData.TargetFound);
    const [optionsToDropdown, setOptionToDropdown] = useState([]);
    const [selectItem, setSelectItem] = useState({ name: "None", code: -1 });
    const [openMoreDetails, setopenMoreDetails] = useState(false);
    const dispatch = useDispatch();

    const [enumDetails] = useState({
        PointCoordSystem: getEnumDetailsList(MapCore.EMcPointCoordSystem),
        TargetType: getEnumDetailsList(MapCore.IMcSpatialQueries.EIntersectionTargetType),
        ItemPart: getEnumDetailsList(MapCore.IMcSpatialQueries.EItemPart)
    });

    function containsObject(array: any[], obj: any) {
        return array.some(item => JSON.stringify(item) === JSON.stringify(obj));
    }

    useEffect(() => {
        runCodeSafely(() => {
            let numFields: number
            let arr: any[] = []
            arr.push({ name: "None", code: -1 })
            targetFound.forEach((itemFound) => {
                if (itemFound.eTargetType == MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_VISIBLE_VECTOR_LAYER) {
                    let vectorLayer: MapCore.IMcVectorMapLayer = itemFound.pTerrainLayer as MapCore.IMcVectorMapLayer;
                    try {
                        numFields = vectorLayer.GetNumFields();
                    } catch {

                    }

                    for (let index = 0; index < numFields; index++) {
                        let pstrName = {}, peFieldType = {};
                        let data = vectorLayer.GetFieldData(index, pstrName, peFieldType);
                        let item = { name: (pstrName as any).Value, code: index }
                        if (!containsObject(arr, item))
                            arr.push({ name: (pstrName as any).Value, code: index })
                    }
                }
            }); setOptionToDropdown(arr)
        }, "VectorField.useEffect")
    }, [])

    const VectorItemField = async (value: any) => {
        runCodeSafely(() => {

            let columnName = value.name;
            props.setName(columnName)
            dispatch(setDisplayTarget([]))
            let obj: any
            setSelectItem(value)
            targetFound.forEach((itemFound, index) => {
                let b: any = null;
                if ((itemFound.eTargetType == MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_VISIBLE_VECTOR_LAYER) ||
                    (itemFound.eTargetType == MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_OVERLAY_MANAGER_OBJECT)) {
                    b = <Button onClick={() => { setopenMoreDetails(true) }}>details</Button>
                }
                let bitCount = 32;
                if (itemFound.eTargetType == MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_STATIC_OBJECTS_LAYER &&
                    ScanService.ifVector3DExtrusion(itemFound.pTerrainLayer)) {
                    bitCount = ScanService.ifVector3DExtrusion(itemFound.pTerrainLayer).GetObjectIDBitCount();
                }
                else if (ScanService.ifVectorLayer(itemFound)) {
                    bitCount = 64;;
                }
                let ItemColor: any = null;
                let color = ScanService.ifVector3DExtrusion(itemFound.pTerrainLayer)?.GetObjectColor(itemFound.uTargetID);
                if (color)
                    ItemColor = "(" + color.r + "," + color.g + "," + color.b + "," + color.a + ")"

                obj = {
                    index: index + 1,
                    target: itemFound,
                    coordSystem: getEnumValueDetails(itemFound.eIntersectionCoordSystem, enumDetails.PointCoordSystem),
                    Target_Type: getEnumValueDetails(itemFound.eTargetType, enumDetails.TargetType),
                    TerrainID: itemFound.pTerrain?.GetID() == MapCore.MC_EMPTY_ID ? "EMPTY" : itemFound.pTerrain?.GetID(),
                    LayerID: itemFound.pTerrainLayer?.GetID() == MapCore.MC_EMPTY_ID ? "EMPTY" : itemFound.pTerrainLayer?.GetID(),
                    TargetID: ScanService.GetTargetIdByBitCount(itemFound) == 0 ? "" : ScanService.GetTargetIdByBitCount(itemFound),
                    ObjectID: itemFound.ObjectItemData.pObject?.GetID(),
                    ItemID: itemFound.ObjectItemData.pItem?.GetID(),
                    SubItemID: itemFound.ObjectItemData.uSubItemID == MapCore.MC_EMPTY_ID ? "EMPTY" : itemFound.ObjectItemData.uSubItemID,
                    ItemType: itemFound.ObjectItemData.pItem?.GetNodeType(),
                    ItemHeight: ScanService.ifVector3DExtrusion(itemFound.pTerrainLayer)?.GetObjectExtrusionHeight(itemFound.uTargetID) === MapCore.FLT_MAX ? "Original" : ScanService.ifVector3DExtrusion(itemFound.pTerrainLayer)?.GetObjectExtrusionHeight(itemFound.uTargetID),
                    PartFound: getEnumValueDetails(itemFound.ObjectItemData.ePartFound, enumDetails.ItemPart),
                    PartIndex: itemFound.ObjectItemData.uPartIndex,
                    ItemColor: ItemColor,
                    MoreDetails: b
                }
                if (itemFound.eTargetType == MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_VISIBLE_VECTOR_LAYER) {
                    let vectorLayer: MapCore.IMcVectorMapLayer = itemFound.pTerrainLayer as MapCore.IMcVectorMapLayer;
                    let targetId: number = ScanService.GetTargetIdByBitCount(itemFound) as any
                    if (value.code != -1) {
                        vectorLayer.GetVectorItemFieldValueAsWString(itemFound.uTargetID.u64BitHigh, value.code, new MapCoreData.asyncOperationCallBacksClass(
                            (pLayer: MapCore.IMcMapLayer, eStatus: MapCore.IMcErrors.ECode, eReturnedType: MapCore.IMcMapLayer.EVectorFieldReturnedType, pValue: any) => {
                                obj = {
                                    index: index + 1,
                                    target: itemFound,
                                    coordSystem: getEnumValueDetails(itemFound.eIntersectionCoordSystem, enumDetails.PointCoordSystem),
                                    Target_Type: getEnumValueDetails(itemFound.eTargetType, enumDetails.TargetType),
                                    TerrainID: itemFound.pTerrain?.GetID() == MapCore.MC_EMPTY_ID ? "EMPTY" : itemFound.pTerrain?.GetID(),
                                    LayerID: itemFound.pTerrainLayer?.GetID() == MapCore.MC_EMPTY_ID ? "EMPTY" : itemFound.pTerrainLayer?.GetID(),
                                    TargetID: ScanService.GetTargetIdByBitCount(itemFound) == 0 ? "" : ScanService.GetTargetIdByBitCount(itemFound),
                                    ObjectID: itemFound.ObjectItemData.pObject?.GetID(),
                                    ItemID: itemFound.ObjectItemData.pItem?.GetID(),
                                    SubItemID: itemFound.ObjectItemData.uSubItemID == MapCore.MC_EMPTY_ID ? "EMPTY" : itemFound.ObjectItemData.uSubItemID,
                                    ItemType: itemFound.ObjectItemData.pItem?.GetNodeType(),
                                    ItemHeight: ScanService.ifVector3DExtrusion(itemFound.pTerrainLayer)?.GetObjectExtrusionHeight(itemFound.uTargetID) === MapCore.FLT_MAX ? "Original" : ScanService.ifVector3DExtrusion(itemFound.pTerrainLayer)?.GetObjectExtrusionHeight(itemFound.uTargetID),
                                    PartFound: getEnumValueDetails(itemFound.ObjectItemData.ePartFound, enumDetails.ItemPart),
                                    PartIndex: itemFound.ObjectItemData.uPartIndex,
                                    ItemColor: ItemColor,
                                    columnName: pValue,
                                    MoreDetails: b
                                }
                                dispatch(addDisplayTarget(obj))
                            }))
                    }
                }
                if ((itemFound.eTargetType != MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_VISIBLE_VECTOR_LAYER) || (value.code == -1))
                    dispatch(addDisplayTarget(obj))
            })
        }, "VectorField.VectorItemField")
    }

    return (<div>
        <Fieldset legend='Vector Field To Add' style={{ height: '100%' }}>
            <Dropdown name="vectorField" value={selectItem} onChange={(event) => { VectorItemField(event.target.value) }} options={optionsToDropdown} optionLabel="name" className="w-full md:w-14rem" />
        </Fieldset>
    </div>)
}