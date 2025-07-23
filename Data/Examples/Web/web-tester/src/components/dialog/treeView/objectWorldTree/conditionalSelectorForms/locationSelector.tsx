import { Button } from "primereact/button";
import { useEffect, useState } from "react";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";
import { useDispatch, useSelector } from "react-redux";
import { AppState } from "../../../../../redux/combineReducer";
import _ from 'lodash';
import { hideFormReasons, objectWorldNodeType } from "../../../../shared/models/tree-node.model";
import { MapCoreData, ObjectWorldService, ViewportData } from "mapcore-lib";
import generalService from "../../../../../services/general.service";
import { setLocationSelectorObject, setObjectWorldTree, setShowDialog } from "../../../../../redux/ObjectWorldTree/objectWorldTreeActions";
import objectWorldTreeService from "../../../../../services/objectWorldTree.service";
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import Vector3DGrid from "../shared/vector3DGrid";
import { DialogTypesEnum } from "../../../../../tools/enum/enums";
import TreeNodeModel from "../../../../shared/models/tree-node.model";

export default function LocationSelector(props: { setFieldsValues: (func: () => void) => void }) {
    const dispatch = useDispatch();
    let currentTreeNode = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);
    let locationSelectorObjects = useSelector((state: AppState) => state.objectWorldTreeReducer.locationSelectorObjects);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    let treeRedux = useSelector((state: AppState) => state.objectWorldTreeReducer.objectWorldTree);

    const [lastSelector, setLastSelector] = useState(null);
    const [enumDetails] = useState({
        ENodeKindFlags: getEnumDetailsList(MapCore.IMcObjectSchemeNode.ENodeKindFlags),
        EItemSubTypeFlags: getEnumDetailsList(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags),
    });

    let [locationSelectorData, setLocationSelectorData] = useState({
        tmpObject: currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER ? null : locationSelectorObjects.find(selectorObj => selectorObj.selector === currentTreeNode.nodeMcContent)?.object,
        selectorObject: currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER ? null : locationSelectorObjects.find(selectorObj => selectorObj.selector === currentTreeNode.nodeMcContent),
        locationPointsTable: currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER ? [] : [...currentTreeNode.nodeMcContent.GetPolygonPoints()],
    })

    useEffect(() => {
        return () => {
            if (currentTreeNode.nodeType !== objectWorldNodeType.OVERLAY_MANAGER && locationSelectorData.tmpObject) {//this scope writen for update the object on the map when user didnt click OK befor changing treeNode to another type of treeNode
                let points = currentTreeNode.nodeMcContent.GetPolygonPoints();
                locationSelectorData.tmpObject.SetLocationPoints(points);
            }
        }
    }, [])
    useEffect(() => {
        runCodeSafely(() => {
            setLocationSelectorData({
                ...locationSelectorData,
                tmpObject: currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER ? null : locationSelectorObjects.find(selectorObj => selectorObj.selector === currentTreeNode.nodeMcContent)?.object,
                selectorObject: currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER ? null : locationSelectorObjects.find(selectorObj => selectorObj.selector === currentTreeNode.nodeMcContent),
            })
        }, 'locationSelectorData.useEffect => locationSelectorObjects');
    }, [locationSelectorObjects])
    useEffect(() => {
        runCodeSafely(() => {
            if (currentTreeNode.nodeType !== objectWorldNodeType.OVERLAY_MANAGER) {//this scope writen for update the object on the map when user didnt click OK befor changing treeNode to another selector
                if (lastSelector && lastSelector != currentTreeNode && locationSelectorData.tmpObject) {
                    let points = lastSelector.nodeMcContent.GetPolygonPoints();
                    locationSelectorData.tmpObject.SetLocationPoints(points);
                }
                setLastSelector(currentTreeNode);
            }
            setLocationSelectorData({
                ...locationSelectorData,
                selectorObject: currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER ? null : locationSelectorObjects.find(selectorObj => selectorObj.selector === currentTreeNode.nodeMcContent),
                tmpObject: currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER ? null : locationSelectorObjects.find(selectorObj => selectorObj.selector === currentTreeNode.nodeMcContent)?.object,
                locationPointsTable: currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER ? [] : [...currentTreeNode.nodeMcContent.GetPolygonPoints()],
            })
        }, 'locationSelectorData.useEffect');
    }, [currentTreeNode])
    useEffect(() => {
        props.setFieldsValues(handleOKClick);
    }, [locationSelectorData])

    const handleOKClick = (): string | MapCore.IMcLocationConditionalSelector => {
        let returnedValue: MapCore.IMcLocationConditionalSelector | string = '';
        runCodeSafely(() => {
            let arrLen = locationSelectorData.locationPointsTable.length;
            if (currentTreeNode.nodeType === objectWorldNodeType.OVERLAY_MANAGER) {
                let mcCurrentOM = currentTreeNode.nodeMcContent as MapCore.IMcOverlayManager;//always needed
                returnedValue = MapCore.IMcLocationConditionalSelector.Create(mcCurrentOM, locationSelectorData.locationPointsTable)//always needed
                if (!locationSelectorData.tmpObject && arrLen) {//there are location point only by writing points without draw polygon
                    let objAndScheme = createNewPolygon();
                    dispatch(setLocationSelectorObject({ selector: returnedValue, object: objAndScheme.obj }))
                }
                else if (arrLen) {
                    locationSelectorData.tmpObject.SetLocationPoints(locationSelectorData.locationPointsTable);
                    dispatch(setLocationSelectorObject({ selector: returnedValue, object: locationSelectorData.tmpObject }))
                }
                else {
                    //just to show that there is a case that there is no points in locationPointsTable
                }
            }
            else {
                currentTreeNode.nodeMcContent.SetPolygonPoints(locationSelectorData.locationPointsTable);//always needed
                if (!locationSelectorData.tmpObject && arrLen) {//there wasnt a selectorObj (generated with no points) and now there are location points only by writing points without draw polygon
                    let objAndScheme = createNewPolygon();
                    dispatch(setLocationSelectorObject({ selector: currentTreeNode.nodeMcContent as MapCore.IMcLocationConditionalSelector, object: objAndScheme.obj }))
                }
                else if (arrLen) {
                    locationSelectorData.tmpObject.SetLocationPoints(locationSelectorData.locationPointsTable);
                    dispatch(setLocationSelectorObject({ selector: currentTreeNode.nodeMcContent as MapCore.IMcLocationConditionalSelector, object: locationSelectorData.tmpObject }))
                }
            }
        }, 'locationSelectorData.handleOKClick')
        return returnedValue;
    }
    const createNewPolygon = (): { obj: MapCore.IMcObject, objSchemeItem: MapCore.IMcObjectSchemeItem } => {
        let OM = currentTreeNode.nodeType == objectWorldNodeType.OVERLAY_MANAGER ? currentTreeNode : objectWorldTreeService.getParentByChildKey(treeRedux, currentTreeNode.key) as TreeNodeModel;
        let overlay = OM.children[0];
        let viewportData = objectWorldTreeService.getOMMCViewportsByOverlay(treeRedux, overlay)[0];
        let activeOverlay = viewportData ? ObjectWorldService.findActiveOverlayByMcViewport(viewportData.viewport) : null;
        let objSchemeItem: MapCore.IMcPolygonItem = null;
        let subItemsFlag = 0 | getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_ATTACHED_TO_TERRAIN, enumDetails.EItemSubTypeFlags).code |
            getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN, enumDetails.EItemSubTypeFlags).code;

        runMapCoreSafely(() => {
            objSchemeItem = MapCore.IMcPolygonItem.Create(subItemsFlag,
                generalService.ObjectProperties.LineStyle,
                generalService.ObjectProperties.LineColor,
                generalService.ObjectProperties.LineWidth,
                generalService.ObjectProperties.LineTexture,
                generalService.ObjectProperties.LineTextureHeightRange,
                2,
                generalService.ObjectProperties.FillStyle,
                generalService.ObjectProperties.FillColor,
                generalService.ObjectProperties.FillTexture,
                new MapCore.SMcFVector2D(1, 1)
            );
        }, "locationSelectorData.getPointsByDrawPolygon => IMcPolygonItem.Create", true);

        let obj: MapCore.IMcObject = null;
        let locationPoints: MapCore.SMcVector3D[] = locationSelectorData.locationPointsTable;

        runMapCoreSafely(() => {
            obj = MapCore.IMcObject.Create(activeOverlay,
                objSchemeItem,
                generalService.ObjectProperties.LocationCoordSys,
                locationPoints,
                generalService.ObjectProperties.LocationRelativeToDtm);
        }, "locationSelectorData.getPointsByDrawPolygon => IMcObject.Create", true);

        let buildedTree = objectWorldTreeService.buildTree();
        dispatch(setObjectWorldTree(buildedTree));

        return { obj: obj, objSchemeItem: objSchemeItem };
    }
    const startDrawOrEditPolygon = (viewportData: ViewportData, objAndSchemeObject: { obj: MapCore.IMcObject, objSchemeItem: MapCore.IMcObjectSchemeItem }) => {
        runCodeSafely(() => {
            runMapCoreSafely(() => { viewportData.editMode.StartInitObject(objAndSchemeObject.obj, objAndSchemeObject.objSchemeItem); }, "locationSelectorData.getPointsByDrawPolygon => IMcEditMode.StartInitObject", true);

            let iMcEditModeCallback: any = new MapCoreData.iMcEditModeCallbacksClass({
                InitItemResults: (pObject: MapCore.IMcObject, pItem: MapCore.IMcObjectSchemeItem, nExitCode: number) => {
                    dispatch(setShowDialog({ hideFormReason: null, dialogType: null }))
                    let pointsList = pObject.GetLocationPoints()
                    setLocationSelectorData({
                        ...locationSelectorData,
                        locationPointsTable: [...pointsList],
                        tmpObject: pObject,
                    })
                }
            });
            runMapCoreSafely(() => { viewportData.editMode.SetEventsCallback(iMcEditModeCallback); }, "locationSelectorData.getPointsByDrawPolygon => IMcEditMode.SetEventsCallback", true)
        }, 'LocationConditionalSelector.startDrawOrEditPolygon')
    }
    const getPointsByDrawPolygon = () => {
        runCodeSafely(() => {
            dispatch(setShowDialog({ hideFormReason: hideFormReasons.CHOOSE_POLYGON, dialogType: DialogTypesEnum.objectWorldTree }))
            let OM = currentTreeNode.nodeType == objectWorldNodeType.OVERLAY_MANAGER ? currentTreeNode : objectWorldTreeService.getParentByChildKey(treeRedux, currentTreeNode.key) as TreeNodeModel;
            let overlay = OM.children[0];
            let viewportData = objectWorldTreeService.getOMMCViewportsByOverlay(treeRedux, overlay)[0];
            let activeOverlay = viewportData ? ObjectWorldService.findActiveOverlayByMcViewport(viewportData.viewport) : null;
            if (activeOverlay) {
                let returnedObjAndScheme: { obj: MapCore.IMcObject, objSchemeItem: MapCore.IMcObjectSchemeItem } = { obj: null, objSchemeItem: null };
                if (!locationSelectorData.tmpObject) {//will happen only in the first draw polygon click in OM OR in selector that generated without any locationPoints
                    returnedObjAndScheme = createNewPolygon();
                }
                else {// will happen in all the other times
                    let finalObj = locationSelectorData.tmpObject;
                    let scheme = finalObj.GetScheme();
                    let objSchemeItem: MapCore.IMcObjectSchemeItem = null;
                    if (scheme) {
                        let nodeKind = getEnumValueDetails(MapCore.IMcObjectSchemeNode.ENodeKindFlags.ENKF_ANY_ITEM, enumDetails.ENodeKindFlags)
                        let nodes = scheme.GetNodes(nodeKind.code);
                        objSchemeItem = nodes != null && nodes.length > 0 ? objSchemeItem = nodes[0] as MapCore.IMcObjectSchemeItem : null;
                    }
                    returnedObjAndScheme = { obj: finalObj, objSchemeItem: objSchemeItem }
                }
                if (returnedObjAndScheme.obj && returnedObjAndScheme.objSchemeItem) {
                    startDrawOrEditPolygon(viewportData, returnedObjAndScheme);
                }
            }
        }, "locationSelectorData.getPointsByDrawPolygon");
    }
    const saveLocationPointsTable = (locationPointsList: any, validPoints: any) => {
        if (!_.isEqual(locationSelectorData.locationPointsTable, locationPointsList)) {
            setLocationSelectorData({
                ...locationSelectorData,
                locationPointsTable: locationPointsList,
            })
        }
    }

    return <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', width: '100%', padding: `${globalSizeFactor * 2}vh` }}>
        <Button label={'Draw Polygon'} style={{ width: '40%' }} onClick={getPointsByDrawPolygon} />
        <br />
        <Vector3DGrid pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} sendPointList={saveLocationPointsTable} initLocationPointsList={locationSelectorData.locationPointsTable} />
    </div>
}
