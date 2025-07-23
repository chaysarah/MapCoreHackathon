import { useEffect, useRef, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Checkbox } from "primereact/checkbox";
import { InputNumber } from "primereact/inputnumber";
import { Button } from "primereact/button";

import { ViewportData, MapCoreData, ObjectWorldService, EditModeService } from 'mapcore-lib'
import Vector3DFromMap from "../treeView/objectWorldTree/shared/Vector3DFromMap";
import ObjectTreeNode, { hideFormReasons } from "../../shared/models/tree-node.model";
import { DialogTypesEnum } from "../../../tools/enum/enums";
import { setObjectWorldTree, setShowDialog } from "../../../redux/ObjectWorldTree/objectWorldTreeActions";
import { runCodeSafely, runMapCoreSafely } from "../../../common/services/error-handling/errorHandler";
import objectWorldTreeService from "../../../services/objectWorldTree.service";
import { AppState } from "../../../redux/combineReducer";

// IMPORTANT NOTE: if a component rendered with RectFromMapCtrl is fetching objects from overlays,
// make sure the object's name is not WORLD_RECT_CTRL_OBJECT_NAME
export const WORLD_RECT_CTRL_OBJECT_NAME = "RectFromMapCtrlObject"

export class RectFromMapCtrlInfo {
    rectangleBox: MapCore.SMcBox;
    showRectangle: boolean;
    setProperty: (name: string, value: any) => void;
    readOnly: boolean;
    currViewport: MapCore.IMcMapViewport;
    rectCoordSystem: MapCore.EMcPointCoordSystem;
    // ctrlHeight: number;
    // dialogPath: string;
};

export default function RectFromMapCtrl(props: { info: RectFromMapCtrlInfo }) {
    let viewportData: ViewportData = MapCoreData.viewportsData[0];
    let activeOverlay = null;
    const dispatch = useDispatch();
    const dialogTypesArr: DialogTypesEnum[] = useSelector((state: AppState) => state.mapCoreReducer.dialogTypesArr);
    let selectedNodeInTree = useSelector((state: AppState) => state.mapWorldTreeReducer.selectedNodeInTree);
    let [rectangleBox, setRectangleBox] = useState(props.info.rectangleBox);
    let [showRectangle, setShowRectangle] = useState(props.info.showRectangle);
    let [rectangleObj, setRectangleObj] = useState<MapCore.IMcObject>(null);
    const [isMountedUseEffect, setIsMountedUseEffect] = useState({ selectedNodeInTree: false })
    let rectangleObjRef = useRef(rectangleObj);

    const getRectangleBoxValidation = () => {
        if (props.info?.rectangleBox) {
            return !(props.info.rectangleBox.MaxVertex.x < props.info.rectangleBox.MinVertex.x || props.info.rectangleBox.MaxVertex.y < props.info.rectangleBox.MinVertex.y);
        }
        return false;
    }
    const saveWorldRectangle = (name: string, value: MapCore.SMcVector3D) => {
        runCodeSafely(() => {
            let updatedWorldRectangle = { ...props.info.rectangleBox };
            updatedWorldRectangle[name] = value;
            props.info.setProperty("worldRectangleBox", updatedWorldRectangle);
        }, "RectFromMapCtrl.saveWorldRectangle")
    }
    const createRectangleObject = () => {
        let rectObj: MapCore.IMcObject = null;
        runCodeSafely(() => {
            let ObjSchemeItem: MapCore.IMcRectangleItem = null;
            runMapCoreSafely(() => {
                ObjSchemeItem = MapCore.IMcRectangleItem.Create((MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN as any).value,
                    MapCore.EMcPointCoordSystem.EPCS_SCREEN, MapCore.IMcObjectSchemeItem.EGeometryType.EGT_GEOMETRIC_IN_VIEWPORT,
                    MapCore.IMcRectangleItem.ERectangleDefinition.ERD_RECTANGLE_DIAGONAL_POINTS, 0, 0,
                    MapCore.IMcLineBasedItem.ELineStyle.ELS_SOLID, MapCore.bcBlackOpaque, 2,
                    null, new MapCore.SMcFVector2D(0, -1), 1, MapCore.IMcLineBasedItem.EFillStyle.EFS_SOLID,
                    new MapCore.SMcBColor(0, 255, 100, 100));
            }, "RectFromMapCtrl.UseEffect => IMcRectangleItem.Create", true)

            let locationPoints: MapCore.SMcVector3D[] = [new MapCore.SMcVector3D(0, 0, 0), new MapCore.SMcVector3D(0, 0, 0)];
            if (props.info?.rectangleBox) {
                locationPoints = [props.info.rectangleBox.MinVertex, props.info.rectangleBox.MaxVertex]
            }
            runMapCoreSafely(() => {
                rectObj = MapCore.IMcObject.Create(activeOverlay, ObjSchemeItem, props.info.rectCoordSystem, locationPoints)
            }, "RectFromMapCtrl.UseEffect => IMcObject.Create", true)

            runMapCoreSafely(() => {
                rectObj.SetNameAndDescription(WORLD_RECT_CTRL_OBJECT_NAME, "")
            }, "RectFromMapCtrl.UseEffect => SetNameAndDescription", true)

            props.info?.showRectangle ?
                rectObj.SetVisibilityOption(MapCore.IMcConditionalSelector.EActionOptions.EAO_FORCE_TRUE) :
                rectObj.SetVisibilityOption(MapCore.IMcConditionalSelector.EActionOptions.EAO_FORCE_FALSE)

        }, "RectFromMapCtrl.createRectangleObject")
        return rectObj;
    }

    useEffect(() => {
        runCodeSafely(() => {
            viewportData = MapCoreData.viewportsData.filter(v => v.viewport == props.info.currViewport)[0]
            activeOverlay = viewportData ? ObjectWorldService.findActiveOverlayByMcViewport(viewportData.viewport) : null;
            if (activeOverlay) {
                let rectObj = createRectangleObject();
                setRectangleObj(rectObj);
                rectangleObjRef.current = rectObj;
            }
        }, "RectFromMapCtrl.UseEffect[props.info.currViewport]")

        return () => {
            runCodeSafely(() => {
                runMapCoreSafely(() => {
                    rectangleObjRef.current?.Remove();
                }, "RectFromMapCtrl.UseEffect => IMcObject.Remove", true)
                setRectangleObj(null);
                rectangleObjRef.current = null;

                let buildedTree: ObjectTreeNode = objectWorldTreeService.buildTree();
                dispatch(setObjectWorldTree(buildedTree));
            }, "RectFromMapCtrl.UseEffect[props.info.currViewport]")
        }
    }, [props.info.currViewport])

    useEffect(() => {
        runCodeSafely(() => {
            rectangleObjRef.current = rectangleObj;
        }, "RectFromMapCtrl.UseEffect[rectangleObj]")
    }, [rectangleObj])

    useEffect(() => {
        runCodeSafely(() => {
            if (isMountedUseEffect.selectedNodeInTree) {
                let locationPoints: MapCore.SMcVector3D[] = [new MapCore.SMcVector3D(0, 0, 0), new MapCore.SMcVector3D(0, 0, 0)];
                if (props.info?.rectangleBox) {
                    let isValidBox = getRectangleBoxValidation();
                    locationPoints = isValidBox ? [props.info.rectangleBox.MinVertex, props.info.rectangleBox.MaxVertex] : locationPoints;
                }
                runMapCoreSafely(() => {
                    rectangleObjRef.current?.SetLocationPoints(locationPoints)
                }, "RectFromMapCtrl.UseEffect[selectedNodeInTree] => SetLocationPoints", true)
            }
            else {
                setIsMountedUseEffect({ ...isMountedUseEffect, selectedNodeInTree: true })
            }
        }, "RectFromMapCtrl.UseEffect")
    }, [selectedNodeInTree])

    useEffect(() => {
        runCodeSafely(() => {
            setRectangleBox(props.info?.rectangleBox)
            if (props.info?.rectangleBox) {
                let isValidBox = getRectangleBoxValidation();
                if (isValidBox) {
                    runMapCoreSafely(() => {
                        rectangleObjRef.current?.SetLocationPoints([props.info.rectangleBox.MinVertex, props.info.rectangleBox.MaxVertex])
                    }, "RectFromMapCtrl.UseEffect[props.info?.rectangleBox] => SetLocationPoints", true)
                }
            }
        }, "RectFromMapCtrl.UseEffect[props.info?.rectangleBox]")
    }, [props.info?.rectangleBox])

    useEffect(() => {
        runCodeSafely(() => {
            setShowRectangle(props.info?.showRectangle)
            props.info?.showRectangle ?
                rectangleObjRef.current?.SetVisibilityOption(MapCore.IMcConditionalSelector.EActionOptions.EAO_FORCE_TRUE) :
                rectangleObjRef.current?.SetVisibilityOption(MapCore.IMcConditionalSelector.EActionOptions.EAO_FORCE_FALSE)
        }, "RectFromMapCtrl.UseEffect[props.info?.showRectangle]")
    }, [props.info?.showRectangle])

    const selectRectangleOnMap = () => {
        runCodeSafely(() => {
            let lastDialogType = dialogTypesArr[dialogTypesArr.length - 1];
            dispatch(setShowDialog({ hideFormReason: hideFormReasons.PAINT_RECTANGLE, dialogType: lastDialogType }));
            EditModeService.doPaintRectangle(viewportData, props.info.rectCoordSystem, onRectResults)
        }, "RectFromMapCtrl.paintRectangle => onClick")
    }

    const onRectResults = (Coords: MapCore.SMcVector3D[]) => {
        dispatch(setShowDialog({ hideFormReason: null, dialogType: null }))
        if (Coords.length == 2) {
            let updatedWorldRectangle = { ...props.info.rectangleBox }
            updatedWorldRectangle.MinVertex = Coords[0];
            updatedWorldRectangle.MaxVertex = Coords[1];
            props.info.setProperty("worldRectangleBox", updatedWorldRectangle);
        }
    }
    //#region DOM Functions
    const getReadOnlyWorldRect = () => {
        return (
            <div className="form__column-container">
                <div className="form__center-aligned-row">
                    <label>Min Point:</label>
                    <label htmlFor='MinVertex.x'>X:</label>
                    <InputNumber className="form__large-input" id='MinVertex.x' value={rectangleBox?.MinVertex.x} name='MinVertex.x' disabled />

                    <label htmlFor='MinVertex.y'>Y:</label>
                    <InputNumber className="form__large-input" id='MinVertex.y' value={props.info?.rectangleBox?.MinVertex.y} name='MinVertex.y' disabled />

                    <label htmlFor='MinVertex.z'>Z:</label>
                    <InputNumber className="form__large-input" id='MinVertex.z' value={props.info?.rectangleBox?.MinVertex.z} name='MinVertex.z' disabled />
                </div>
                <div className="form__center-aligned-row">
                    <label>Max Point:</label>
                    <label htmlFor='MaxVertex.x'>X:</label>
                    <InputNumber className="form__large-input" id='MaxVertex.x' value={props.info?.rectangleBox?.MaxVertex.x} name='MaxVertex.x' disabled />

                    <label htmlFor='MaxVertex.y'>Y:</label>
                    <InputNumber className="form__large-input" id='MaxVertex.y' value={props.info?.rectangleBox?.MaxVertex.y} name='MaxVertex.y' disabled />

                    <label htmlFor='MaxVertex.z'>Z:</label>
                    <InputNumber className="form__large-input" id='MaxVertex.z' value={props.info?.rectangleBox?.MaxVertex.z} name='MaxVertex.z' disabled />
                </div>
                {getShowRectangleCheckBox()}
            </div>
        )
    }
    const getEditableWorldRect = () => {
        return (
            <div className="form__column-container">
                <Vector3DFromMap pointCoordSystem={props.info.rectCoordSystem} initValue={rectangleBox?.MinVertex} name="Min: " flagNull={{ x: false, y: false, z: false }} saveTheValue={(value: MapCore.SMcVector3D) => { saveWorldRectangle("MinVertex", value) }}></Vector3DFromMap>
                <Vector3DFromMap pointCoordSystem={props.info.rectCoordSystem} initValue={rectangleBox?.MaxVertex} name="Max: " flagNull={{ x: false, y: false, z: false }} saveTheValue={(value: MapCore.SMcVector3D) => { saveWorldRectangle("MaxVertex", value) }}></Vector3DFromMap>
                <div className="form__flex-and-row-between">
                    {getShowRectangleCheckBox()}
                    <Button onClick={selectRectangleOnMap}>Select Rectangle On Map</Button>
                </div>
            </div>
        )
    }
    const getShowRectangleCheckBox = () => {
        return <div className="form__center-aligned-row">
            <Checkbox tooltip={!getRectangleBoxValidation() && 'Invalid Points'} name="showRectangle" inputId="showRectangle" checked={showRectangle}
                onChange={(e) => {
                    props.info.setProperty("showRectangle", e.target.checked);
                }} />
            <label htmlFor="showRectangle">Show Rectangle On Map</label>
        </div>
    }
    //#endregion

    return (
        <div>
            {props.info.readOnly ?
                getReadOnlyWorldRect() :
                getEditableWorldRect()
            }
        </div>
    )

}
