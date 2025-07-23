import { MapCoreData, ObjectWorldService, getEnumValueDetails, getEnumDetailsList, ViewportData } from 'mapcore-lib';
import { Button } from 'primereact/button';
import { ConfirmDialog } from 'primereact/confirmdialog';
import { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { runCodeSafely, runMapCoreSafely } from '../../../common/services/error-handling/errorHandler';
import { setShowDialog } from '../../../redux/ObjectWorldTree/objectWorldTreeActions';
import { hideFormReasons } from '../../shared/models/tree-node.model';
import { DialogTypesEnum } from '../../../tools/enum/enums';
import Vector3DGrid from '../treeView/objectWorldTree/shared/vector3DGrid';
import { AppState } from '../../../redux/combineReducer';
import _ from 'lodash';
import generalService from '../../../services/general.service';

export enum ObjectTypeEnum {
    ellipse = 'Ellipse',
    rectangle = 'Rectangle',
    polygon = 'Polygon'
}
export const createSchemeItemByType = (itemType: ObjectTypeEnum, attributes?: { [key: string]: any }) => {
    let objSchemeItem: MapCore.IMcObjectSchemeItem = null;
    runCodeSafely(() => {
        let EItemSubTypeFlags = getEnumDetailsList(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags);
        let subItemsFlag = getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_ATTACHED_TO_TERRAIN, EItemSubTypeFlags).code |
            getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN, EItemSubTypeFlags).code;
        switch (itemType) {
            case ObjectTypeEnum.polygon:
                runMapCoreSafely(() => {
                    objSchemeItem = MapCore.IMcPolygonItem.Create(subItemsFlag, MapCore.IMcLineBasedItem.ELineStyle.ELS_SOLID,
                        MapCore.bcBlackOpaque, 3, null, new MapCore.SMcFVector2D(0, -1), 1, MapCore.IMcLineBasedItem.EFillStyle.EFS_NONE,
                        MapCore.bcBlackOpaque, null, new MapCore.SMcFVector2D(1, 1));
                }, 'DrawObjectCtrl.createSchemeItemByType => IMcPolygonItem.Create', true)
                break;
            case ObjectTypeEnum.ellipse:
                runMapCoreSafely(() => {
                    objSchemeItem = MapCore.IMcEllipseItem.Create(subItemsFlag, MapCore.EMcPointCoordSystem.EPCS_WORLD,
                        MapCore.IMcObjectSchemeItem.EGeometryType.EGT_GEOGRAPHIC,
                        attributes?.RadiusX || generalService.ObjectProperties.EllipseRadiusX,
                        attributes?.RadiusY || generalService.ObjectProperties.EllipseRadiusY,
                        attributes?.StartAngle || generalService.ObjectProperties.EllipseStartAngle,
                        attributes?.EndAngle || generalService.ObjectProperties.EllipseEndAngle,
                        0, MapCore.IMcLineBasedItem.ELineStyle.ELS_SOLID, new MapCore.SMcBColor(0, 0, 0, 255),
                        3, null, new MapCore.SMcFVector2D(1, 1), 1, MapCore.IMcLineBasedItem.EFillStyle.EFS_NONE);
                }, 'DrawObjectCtrl.createSchemeItemByType => IMcEllipseItem.Create', true)
                break;
            case ObjectTypeEnum.rectangle:
                runMapCoreSafely(() => {
                    objSchemeItem = MapCore.IMcRectangleItem.Create(subItemsFlag, MapCore.EMcPointCoordSystem.EPCS_WORLD, MapCore.IMcObjectSchemeItem.EGeometryType.EGT_GEOGRAPHIC,
                        MapCore.IMcRectangleItem.ERectangleDefinition.ERD_RECTANGLE_CENTER_DIMENSIONS,
                        (attributes?.RadiusX || generalService.ObjectProperties.RectRadiusX) / 2,
                        (attributes?.RadiusY || generalService.ObjectProperties.RectRadiusY) / 2,
                        MapCore.IMcLineBasedItem.ELineStyle.ELS_SOLID, new MapCore.SMcBColor(0, 0, 0, 255), 2, null, new MapCore.SMcFVector2D(), 0, MapCore.IMcLineBasedItem.EFillStyle.EFS_NONE);
                }, 'DrawObjectCtrl.createSchemeItemByType => IMcRectangleItem.Create', true)
                break;
            default:
                break;
        }

    }, 'DrawObjectCtrl.createSchemeItemByType')
    return objSchemeItem;
}
export const createItemAndObjByType = (activeOverlay: MapCore.IMcOverlay, objectPoints: MapCore.SMcVector3D[], itemType: ObjectTypeEnum, schemeItemAttributes?: { [key: string]: any }) => {
    let objSchemeItem: MapCore.IMcObjectSchemeItem = null;
    let obj: MapCore.IMcObject = null;
    runCodeSafely(() => {
        objSchemeItem = createSchemeItemByType(itemType, schemeItemAttributes);
        runMapCoreSafely(() => {
            obj = MapCore.IMcObject.Create(activeOverlay,
                objSchemeItem,
                MapCore.EMcPointCoordSystem.EPCS_WORLD,
                objectPoints || [],
                false);
        }, 'DrawObjectCtrl.createItemAndObjByType => IMcObject.Create', true)
    }, 'DrawObjectCtrl.createItemAndObjByType')
    return { obj: obj, objSchemeItem: objSchemeItem };
}
export const getItemTypeByObject = (object: MapCore.IMcObject) => {
    let itemDetails: { itemType: number, mcItem: MapCore.IMcObjectSchemeNode } = { itemType: null, mcItem: null };
    runCodeSafely(() => {
        let nodeKindArr = getEnumDetailsList(MapCore.IMcObjectSchemeNode.ENodeKindFlags);
        let symbolicEnum = getEnumValueDetails(MapCore.IMcObjectSchemeNode.ENodeKindFlags.ENKF_SYMBOLIC_ITEM, nodeKindArr);
        let objectScheme = object.GetScheme();
        let items = objectScheme.GetNodes(symbolicEnum.code);
        itemDetails.itemType = items[0].GetNodeType();
        itemDetails.mcItem = items[0];
    }, 'DrawObjectCtrl.createItemAndObjByType.getItemTypeByObject');
    return itemDetails;
}
export default function DrawObjectCtrl(props: {
    activeViewport: ViewportData, getObject: (polygonObject: MapCore.IMcObject) => void, objectType: ObjectTypeEnum,
    //Optional
    schemeItemAttributes?: { [key: string]: any },
    //Explanation: if pointsTableVisible = true, then: existObjectPoints, existObject and getObjectPoints are required
    pointsTableVisible?: boolean,
    existObjectPoints?: MapCore.SMcVector3D[], existObject?: MapCore.IMcObject, getObjectPoints?: (objectPoints: MapCore.SMcVector3D[]) => void

    disabled?: boolean, ctrlHeight?: number, className?: string, style?: any, handleDrawObjectButtonClick?: () => void
}) {
    const dispatch = useDispatch();
    const [confirmDialogVisible, setConfirmDialogVisible] = useState(false);
    const [confirmDialogMessage, setConfirmDialogMessage] = useState('');
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const dialogTypesArr: DialogTypesEnum[] = useSelector((state: AppState) => state.mapCoreReducer.dialogTypesArr);

    useEffect(() => {
        runCodeSafely(() => {
            if (props.schemeItemAttributes) {
                //This next 3 line cheks: props.existItem.getAttributes!= props.attributes (affects only in rectangle case)
                let itemDetails = props.existObject ? getItemTypeByObject(props.existObject) : null;
                let rectItem = itemDetails?.itemType == MapCore.IMcRectangleItem.NODE_TYPE ? itemDetails?.mcItem as MapCore.IMcRectangleItem : null;
                let isAttributesEqual = rectItem?.GetRadiusX() == props.schemeItemAttributes.RadiusX && rectItem?.GetRadiusY() == props.schemeItemAttributes.RadiusY;
                if (props.existObject && !isAttributesEqual) {
                    let updatedScheme: MapCore.IMcObjectScheme = null;
                    let schemeItem = createSchemeItemByType(props.objectType, props.schemeItemAttributes)
                    let overlayManager = getActiveOverlay()?.GetOverlayManager();
                    runMapCoreSafely(() => {
                        updatedScheme = MapCore.IMcObjectScheme.Create(overlayManager, schemeItem, MapCore.EMcPointCoordSystem.EPCS_WORLD)
                    }, 'DrawObjectCtrl.useEffect => IMcObjectScheme.Create', true);
                    runMapCoreSafely(() => { props.existObject.SetScheme(updatedScheme, true); }, 'DrawObjectCtrl.useEffect => IMcObject.SetScheme', true);
                }
            }
        }, 'DrawObjectCtrl.useEffect')
    }, [JSON.stringify(props.schemeItemAttributes)])

    const getActiveOverlay = () => {
        let activeOverlay: MapCore.IMcOverlay = null;
        runCodeSafely(() => {
            let typedMcCurrentSpatialQueries = props.activeViewport?.viewport as MapCore.IMcSpatialQueries;
            if (typedMcCurrentSpatialQueries && typedMcCurrentSpatialQueries.GetInterfaceType() == MapCore.IMcMapViewport.INTERFACE_TYPE) {
                let mcCurrentViewport = typedMcCurrentSpatialQueries as MapCore.IMcMapViewport;
                activeOverlay = ObjectWorldService.findActiveOverlayByMcViewport(mcCurrentViewport);
            }
        }, 'DrawObjectCtrl.getActiveOverlay');
        return activeOverlay;
    }
    const getPointsList = (...args) => {
        runCodeSafely(() => {
            const [locationPointsList, valid, selectedPoint, pointsType] = args;
            if (!_.isEqual(props.existObjectPoints, locationPointsList)) {
                props.getObjectPoints && props.getObjectPoints(locationPointsList)
                if ((props.objectType == ObjectTypeEnum.ellipse || props.objectType == ObjectTypeEnum.rectangle) && locationPointsList.length > 1) {
                    setConfirmDialogVisible(true);
                    setConfirmDialogMessage(`Num points of ${props.objectType.charAt(0).toUpperCase() + props.objectType.slice(1)} object must be one`);
                }
                else {
                    props.existObject && props.existObject.SetLocationPoints(locationPointsList)
                }
            }
        }, 'DrawObjectCtrl.savePointsTable');
    }
    const startDrawOrEditPolygon = (viewportData: ViewportData, objAndSchemeObject: { obj: MapCore.IMcObject, objSchemeItem: MapCore.IMcObjectSchemeItem }) => {
        runCodeSafely(() => {
            runMapCoreSafely(() => { viewportData.editMode.StartInitObject(objAndSchemeObject.obj, objAndSchemeObject.objSchemeItem); }, "DrawObjectCtrl.startDrawOrEditPolygon => IMcEditMode.StartInitObject", true);

            let iMcEditModeCallback: any = new MapCoreData.iMcEditModeCallbacksClass({
                InitItemResults: (pObject: MapCore.IMcObject, pItem: MapCore.IMcObjectSchemeItem, nExitCode: number) => {
                    runCodeSafely(() => {
                        props.getObject(pObject)
                        dispatch(setShowDialog({ hideFormReason: null, dialogType: null }));
                    }, 'DrawObjectCtrl.startDrawOrEditPolygon => InitItemResults')
                }
            });
            runMapCoreSafely(() => { viewportData.editMode.SetEventsCallback(iMcEditModeCallback); }, "DrawObjectCtrl.startDrawOrEditPolygon => IMcEditMode.SetEventsCallback", true)
        }, 'DrawObjectCtrl.startDrawOrEditPolygon')
    }
    const handleDrawObjectButtonClick = () => {
        runCodeSafely(() => {
            let activeOverlay = getActiveOverlay();
            if (activeOverlay) {
                props.handleDrawObjectButtonClick && props.handleDrawObjectButtonClick();
                let lastDialogType = dialogTypesArr[dialogTypesArr.length - 1];
                dispatch(setShowDialog({ hideFormReason: hideFormReasons.PAINT_LINE, dialogType: lastDialogType }))
                let objAndSchemeObject: { obj: MapCore.IMcObject, objSchemeItem: MapCore.IMcObjectSchemeItem };
                if (!props.existObject) {
                    objAndSchemeObject = createItemAndObjByType(activeOverlay, props.existObjectPoints || [], props.objectType, props.schemeItemAttributes);
                }
                else {
                    let scheme = props.existObject.GetScheme();
                    let objSchemeItem: MapCore.IMcObjectSchemeItem = null;
                    if (scheme) {
                        let ENodeKindFlags = getEnumDetailsList(MapCore.IMcObjectSchemeNode.ENodeKindFlags);
                        let nodeKind = getEnumValueDetails(MapCore.IMcObjectSchemeNode.ENodeKindFlags.ENKF_ANY_ITEM, ENodeKindFlags)
                        let nodes = scheme.GetNodes(nodeKind.code);
                        objSchemeItem = nodes != null && nodes.length > 0 ? objSchemeItem = nodes[0] as MapCore.IMcObjectSchemeItem : null;
                    }

                    objAndSchemeObject = { obj: props.existObject, objSchemeItem: objSchemeItem }
                }
                startDrawOrEditPolygon(props.activeViewport, objAndSchemeObject);
            }
            else {
                setConfirmDialogVisible(true);
                setConfirmDialogMessage('No active overlay');
            }
        }, 'DrawObjectCtrl.handleDrawObjectButtonClick');
    }

    return <div className={props.className} style={props.style}>
        {props.pointsTableVisible && <div className="form__column-container">
            <span style={{ textDecoration: 'underline', padding: `${globalSizeFactor * 0.7}vh` }}>{props.objectType} Points: </span>
            <Vector3DGrid disabledPointFromMap={props.disabled} ctrlHeight={props.ctrlHeight && (props.ctrlHeight - 2)} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initLocationPointsList={props.existObjectPoints} sendPointList={getPointsList} />
        </div>}

        <Button className={!props.disabled && props.activeViewport ? '' : 'form__disabled'} label={`Draw ${props.objectType}`} onClick={handleDrawObjectButtonClick} />

        <ConfirmDialog
            contentClassName='form__confirm-dialog-content'
            message={confirmDialogMessage}
            header=''
            footer={<div></div>}
            visible={confirmDialogVisible}
            onHide={e => { setConfirmDialogVisible(false) }}
        />
    </div>

}