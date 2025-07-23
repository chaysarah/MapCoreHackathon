import generalService from "./general.service";
import { runAsyncCodeSafely } from "../common/services/error-handling/errorHandler";
import { getEnumDetailsList, getEnumValueDetails, ObjectWorldService, MapCoreData, ViewportData } from 'mapcore-lib';
import { Id } from '../tools/enum/enumsOfScheme';
import store from "../redux/store";
import { setCreateText, setMapWindowDialogType, setNewGeneratedObject } from "../redux/mapWindow/mapWindowAction";
import { setDialogType } from "../redux/MapCore/mapCoreAction";
import { DialogTypesEnum } from "../tools/enum/enums";
const getState = () => store.getState()

class symbolicItems {
    currentViewport: ViewportData = null;
    SCHEMES_FOLDERS_MAIN = "http:ObjectWorld/schemes/";
    SCHEMES_FOLDERS_SCREEN = "Screen/";
    SCHEMES_FOLDERS_WORLD = "World/";
    SCHEMES_FOLDERS_SCREEN_ATTACH_TO_WORLD = "ScreenAttachToWorld/";
    SCHEMES_FOLDERS_WORLD_ATTACH_TO_WORLD = "WorldAttachToWorld/";
    SCHEMES_FOLDERS_WORLD_WORLD = "WorldWorld/";

    //general functions
    getFilePathByProperties = (fileName: string) => {
        let fullFilePath: string = this.SCHEMES_FOLDERS_MAIN;
        let itemsSubTypeList = getEnumDetailsList(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags)
        let itemSubTypeFlags = getEnumValueDetails(generalService.ObjectProperties.ItemSubTypeFlags, itemsSubTypeList);
        let locationCoordSys = generalService.ObjectProperties.LocationCoordSys;

        let isSubTypeScreen: boolean = false;
        let isSubTypeWorld: boolean = false;
        let isSubTypeAttachToTerrain: boolean = false;

        itemSubTypeFlags.forEach((element: any) => {
            if (element.theEnum == MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN) isSubTypeScreen = true;
            if (element.theEnum == MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_WORLD) isSubTypeWorld = true;
            if (element.theEnum == MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_ATTACHED_TO_TERRAIN) isSubTypeAttachToTerrain = true;
        });

        if (isSubTypeAttachToTerrain && isSubTypeScreen)
            fullFilePath = fullFilePath.concat(this.SCHEMES_FOLDERS_SCREEN_ATTACH_TO_WORLD);
        else if (isSubTypeAttachToTerrain && isSubTypeWorld)
            fullFilePath = fullFilePath.concat(this.SCHEMES_FOLDERS_WORLD_ATTACH_TO_WORLD);
        else if (isSubTypeScreen) {
            fullFilePath = fullFilePath.concat(this.SCHEMES_FOLDERS_SCREEN);
            if (locationCoordSys == MapCore.EMcPointCoordSystem.EPCS_SCREEN)
                fullFilePath = fullFilePath.concat(this.SCHEMES_FOLDERS_SCREEN);
            else if (locationCoordSys == MapCore.EMcPointCoordSystem.EPCS_WORLD)
                fullFilePath = fullFilePath.concat(this.SCHEMES_FOLDERS_WORLD);
        } else if (isSubTypeWorld)
            fullFilePath = fullFilePath.concat(this.SCHEMES_FOLDERS_WORLD_WORLD);

        fullFilePath = fullFilePath.concat(fileName);
        return fullFilePath;
    }
    FetchFileToByteArray = async (uri: string) => {
        try {
            let fetchResponse = await fetch(uri);
            let fileBuffer = (fetchResponse.ok ? await fetchResponse.arrayBuffer() : null);
            if (fileBuffer != null) {
                return new Uint8Array(fileBuffer);
            }
            else {
                alert("Cannot fetch " + uri);
                return null;
            }
        }
        catch (error) {
            alert("Network error in fetching " + uri);
            return null;
        }
    }
    FetchObjectScheme = async (uri: string,) => {
        const state = getState();
        let t = state.mapWindowReducer.activeCard;
        this.currentViewport = MapCoreData.findViewport(t)
        let bytes = await this.FetchFileToByteArray(uri);
        if (bytes != null) {
            let scheme = this.currentViewport.viewport.GetOverlayManager().LoadObjectSchemes(bytes)[0];
            // scheme.AddRef();
            return scheme;
        }
        else {
            return null;
        }
    }
    setPrivateProperties = (obj: MapCore.IMcObject, objectSchemeItem: MapCore.IMcObjectSchemeItem) => {
        // General
        obj.SetBoolProperty(Id.ObjectLocation_ObjectLocation_RelativeToDTM, generalService.ObjectProperties.LocationRelativeToDtm);
        obj.SetUIntProperty(Id.ObjectLocation_ObjectLocation_MaxNumOfPoints, generalService.ObjectProperties.LocationMaxPoints);
        obj.SetEnumProperty(Id.Item_ObjectSchemeNode_VisibilityOption, (generalService.ObjectProperties.VisibilityOption as any).value);
        obj.SetEnumProperty(Id.Item_ObjectSchemeNode_TransformOption, (generalService.ObjectProperties.TransformOption as any).value);
        obj.SetByteProperty(Id.Item_ObjectSchemeItem_BlockedTransparency, generalService.ObjectProperties.BlockedTransparency);

        if (eval("objectSchemeItem  instanceof MapCore.IMcSymbolicItem")) {
            obj.SetEnumProperty(Id.Item_SymbolicItem_General_DrawPriorityGroup, (generalService.ObjectProperties.DrawPriorityGroup as any).value);
            obj.SetSByteProperty(Id.Item_SymbolicItem_General_DrawPriority, generalService.ObjectProperties.DrawPriority);
            obj.SetSByteProperty(Id.Item_SymbolicItem_General_Conplanar3dPriority, generalService.ObjectProperties.Conplanar3dPriority);
            obj.SetEnumProperty(Id.Item_SymbolicItem_AttachPoint_PointType, (generalService.ObjectProperties.AttachPointType as any).value);
            obj.SetEnumProperty(Id.Item_SymbolicItem_AttachPoint_BoundingBoxType, (generalService.ObjectProperties.BoundingBoxType as any).value);
            obj.SetIntProperty(Id.Item_SymbolicItem_AttachPoint_NumPoints, generalService.ObjectProperties.AttachPointNumPoints);
            obj.SetIntProperty(Id.Item_SymbolicItem_AttachPoint_PointIndex, generalService.ObjectProperties.AttachPointIndex);
            obj.SetFloatProperty(Id.Item_SymbolicItem_AttachPoint_PositionValue, generalService.ObjectProperties.AttachPointPositionValue);
            obj.SetByteProperty(Id.Item_SymbolicItem_General_Transparency, generalService.ObjectProperties.Transparency);
            obj.SetFloatProperty(Id.Item_SymbolicItem_General_MoveIfBlockedMax, generalService.ObjectProperties.MoveIfBlockedMax);
            obj.SetFloatProperty(Id.Item_SymbolicItem_General_MoveIfBlockedHeight, generalService.ObjectProperties.MoveIfBlockedHeight);
            obj.SetUIntProperty(Id.Item_SymbolicItem_Transform_VectorTransformSegment, generalService.ObjectProperties.VectorTransformSegment);
            obj.SetEnumProperty(Id.Item_SymbolicItem_Offset_VectorOffsetCalc, (generalService.ObjectProperties.VectorOffsetCalc as any).value);
            obj.SetEnumProperty(Id.Item_SymbolicItem_Offset_OffsetOrientation, (generalService.ObjectProperties.Orientation as any).value);
            obj.SetFloatProperty(Id.Item_SymbolicItem_Offset_VectorOffsetValue, generalService.ObjectProperties.VectorOffsetValue);
            obj.SetFVector3DProperty(Id.Item_SymbolicItem_Offset_Offset, generalService.ObjectProperties.Offset);
            obj.SetFloatProperty(Id.Item_SymbolicItem_Rotation_Yaw, generalService.ObjectProperties.RotationYaw);
            obj.SetFloatProperty(Id.Item_SymbolicItem_Rotation_Pitch, generalService.ObjectProperties.RotationPitch);
            obj.SetFloatProperty(Id.Item_SymbolicItem_Rotation_Roll, generalService.ObjectProperties.RotationRoll);
            obj.SetBoolProperty(Id.Item_SymbolicItem_Rotation_VectorRotation, generalService.ObjectProperties.VectorRotation);
            obj.SetArrayIntProperty(Id.Item_SymbolicItem_Offset_PointsIndicesAndDuplications, MapCore.IMcProperty.EPropertyType.EPT_INT_ARRAY, generalService.ObjectProperties.PointsIndicesAndDuplications);
            // obj.SetArraySMcFVector3DProperty(Id.Item_SymbolicItem_Offset_PointsOffset, MapCore.IMcProperty.EPropertyType.EPT_FVECTOR3D_ARRAY, generalService.ObjectProperties.PointsOffset);
        }
        // // Line Based
        if (eval("objectSchemeItem  instanceof MapCore.IMcLineBasedItem")) {

            obj.SetFloatProperty(Id.Item_LineBased_LineWidth, generalService.ObjectProperties.LineWidth);
            obj.SetBColorProperty(Id.Item_LineBased_LineColor, generalService.ObjectProperties.LineColor);
            obj.SetEnumProperty(Id.Item_LineBased_LineStyle, (generalService.ObjectProperties.LineStyle as any).value);
            obj.SetFVector2DProperty(Id.Item_LineBased_LineTextureHeightRange, generalService.ObjectProperties.LineTextureHeightRange);
            obj.SetTextureProperty(Id.Item_LineBased_LineTexture, generalService.ObjectProperties.LineTexture);
            obj.SetByteProperty(Id.Item_LineBased_NumSmoothingLevels, generalService.ObjectProperties.LineBasedSmoothingLevels);
            obj.SetFloatProperty(Id.Item_LineBased_OutlineWidth, generalService.ObjectProperties.LineOutlineWidth);
            obj.SetBColorProperty(Id.Item_LineBased_OutlineColor, generalService.ObjectProperties.LineOutlineColor);
            (objectSchemeItem as unknown as (MapCore.IMcLineBasedItem)).SetGreatCirclePrecision(generalService.ObjectProperties.LineBasedGreatCirclePrecision);
            obj.SetFloatProperty(Id.Item_LineBased_TextureScale, generalService.ObjectProperties.LineTextureScale);
            obj.SetEnumProperty(Id.Item_LineBased_PointOrderReverseMode, (generalService.ObjectProperties.PointOrderReverseMode as any).value);
            obj.SetEnumProperty(Id.Item_SightPresentation_SightPresentationType, (generalService.ObjectProperties.SightPresentation.type as any).value);
            if (generalService.ObjectProperties.SightPresentation.colorsByVisibility.has(MapCore.IMcSpatialQueries.EPointVisibility.EPV_SEEN))
                obj.SetBColorProperty(Id.Item_SightPresentation_SightColor_Seen, generalService.ObjectProperties.SightPresentation.colorsByVisibility.get(MapCore.IMcSpatialQueries.EPointVisibility.EPV_SEEN));
            if (generalService.ObjectProperties.SightPresentation.colorsByVisibility.has(MapCore.IMcSpatialQueries.EPointVisibility.EPV_UNSEEN))
                obj.SetBColorProperty(Id.Item_SightPresentation_SightColor_Unseen, generalService.ObjectProperties.SightPresentation.colorsByVisibility.get(MapCore.IMcSpatialQueries.EPointVisibility.EPV_UNSEEN));
            if (generalService.ObjectProperties.SightPresentation.colorsByVisibility.has(MapCore.IMcSpatialQueries.EPointVisibility.EPV_UNKNOWN))
                obj.SetBColorProperty(Id.Item_SightPresentation_SightColor_Unknown, generalService.ObjectProperties.SightPresentation.colorsByVisibility.get(MapCore.IMcSpatialQueries.EPointVisibility.EPV_UNKNOWN));
            if (generalService.ObjectProperties.SightPresentation.colorsByVisibility.has(MapCore.IMcSpatialQueries.EPointVisibility.EPV_OUT_OF_QUERY_AREA))
                obj.SetBColorProperty(Id.Item_SightPresentation_SightColor_OutOfQueryArea, generalService.ObjectProperties.SightPresentation.colorsByVisibility.get(MapCore.IMcSpatialQueries.EPointVisibility.EPV_OUT_OF_QUERY_AREA));
            if (generalService.ObjectProperties.SightPresentation.colorsByVisibility.has(MapCore.IMcSpatialQueries.EPointVisibility.EPV_SEEN_STATIC_OBJECT))
                obj.SetBColorProperty(Id.Item_SightPresentation_SightColor_StaticObject, generalService.ObjectProperties.SightPresentation.colorsByVisibility.get(MapCore.IMcSpatialQueries.EPointVisibility.EPV_SEEN_STATIC_OBJECT));
            if (generalService.ObjectProperties.SightPresentation.colorsByVisibility.has(MapCore.IMcSpatialQueries.EPointVisibility.EPV_ASYNC_CALCULATING))
                obj.SetBColorProperty(Id.Item_SightPresentation_SightColor_AsyncCalculation, generalService.ObjectProperties.SightPresentation.colorsByVisibility.get(MapCore.IMcSpatialQueries.EPointVisibility.EPV_ASYNC_CALCULATING));

            obj.SetFloatProperty(Id.Item_SightPresentation_SightObserverHeight, generalService.ObjectProperties.SightPresentation.observerHeight);
            obj.SetFloatProperty(Id.Item_SightPresentation_SightObservedHeight, generalService.ObjectProperties.SightPresentation.observedHeight);
            obj.SetFloatProperty(Id.Item_SightPresentation_SightObserverMaxPitch, generalService.ObjectProperties.SightPresentation.maxPitch);
            obj.SetFloatProperty(Id.Item_SightPresentation_SightObserverMinPitch, generalService.ObjectProperties.SightPresentation.minPitch);
            obj.SetBoolProperty(Id.Item_SightPresentation_IsSightObserverHeightAbsolute, generalService.ObjectProperties.SightPresentation.isObserverHeightAbsolute);
            obj.SetBoolProperty(Id.Item_SightPresentation_IsSightObservedHeightAbsolute, generalService.ObjectProperties.SightPresentation.isObservedHeightAbsolute);
            obj.SetUIntProperty(Id.Item_SightPresentation_SightNumEllipseRays, generalService.ObjectProperties.SightPresentation.numEllipseRays);
            obj.SetEnumProperty(Id.Item_SightPresentation_SightQueryPrecision, (generalService.ObjectProperties.SightPresentation.precision as any).value);
            obj.SetFloatProperty(Id.Item_SightPresentation_TextureResolution, generalService.ObjectProperties.SightPresentation.sightTextureResolution);

            obj.SetFloatProperty(Id.Item_LineBased_VerticalHeight, generalService.ObjectProperties.VerticalHeight);
            obj.SetBColorProperty(Id.Item_LineBased_SidesFillColor, generalService.ObjectProperties.SidesFillColor);
            obj.SetEnumProperty(Id.Item_LineBased_SidesFillStyle, (generalService.ObjectProperties.SidesFillStyle as any).value);
            obj.SetTextureProperty(Id.Item_LineBased_SidesFillTexture, generalService.ObjectProperties.SidesFillTexture);
            obj.SetFVector2DProperty(Id.Item_LineBased_SidesFillTextureScale, generalService.ObjectProperties.SidesFillTextureScale);
            (objectSchemeItem as MapCore.IMcLineBasedItem).SetShapeType(generalService.ObjectProperties.ShapeType);
        }

        // Closed Shaped
        if (eval("objectSchemeItem  instanceof MapCore.IMcClosedShapeItem")) {
            obj.SetBColorProperty(Id.Item_ClosedShape_FillColor, generalService.ObjectProperties.FillColor);
            obj.SetEnumProperty(Id.Item_ClosedShape_FillStyle, (generalService.ObjectProperties.FillStyle as any).value);
            obj.SetTextureProperty(Id.Item_ClosedShape_FillTexture, generalService.ObjectProperties.FillTexture);
            obj.SetFVector2DProperty(Id.Item_ClosedShape_FillTextureScale, generalService.ObjectProperties.FillTextureScale);
        }

        let type = objectSchemeItem.GetNodeType();
        switch (type) {
            case MapCore.IMcArcItem.NODE_TYPE:
                (objectSchemeItem as MapCore.IMcArcItem).SetEllipseDefinition(generalService.ObjectProperties.ArcEllipseDefinition);
                obj.SetFloatProperty(Id.Item_Arc_StartAngle, generalService.ObjectProperties.ArcStartAngle);
                obj.SetFloatProperty(Id.Item_Arc_EndAngle, generalService.ObjectProperties.ArcEndAngle);
                obj.SetFloatProperty(Id.Item_Arc_RadiusX, generalService.ObjectProperties.ArcRadiusX);
                obj.SetFloatProperty(Id.Item_Arc_RadiusY, generalService.ObjectProperties.ArcRadiusY);
                break;
            case MapCore.IMcArrowItem.NODE_TYPE:
                obj.SetFloatProperty(Id.Item_Arrow_HeadAngle, generalService.ObjectProperties.ArrowHeadAngle);
                obj.SetFloatProperty(Id.Item_Arrow_HeadSize, generalService.ObjectProperties.ArrowHeadSize);
                obj.SetFloatProperty(Id.Item_Arrow_GapSize, generalService.ObjectProperties.ArrowGapSize);
                break;
            case MapCore.IMcLineExpansionItem.NODE_TYPE:
                obj.SetFloatProperty(Id.Item_LineExpansionItem_Radius, generalService.ObjectProperties.LineExpansionRadius);
                break;
            case MapCore.IMcPictureItem.NODE_TYPE:
                obj.SetFloatProperty(Id.Item_Picture_Width, generalService.ObjectProperties.PicWidth);
                obj.SetFloatProperty(Id.Item_Picture_Height, generalService.ObjectProperties.PicHeight);
                obj.SetTextureProperty(Id.Item_Picture_Texture, generalService.ObjectProperties.PicTexture);
                obj.SetBColorProperty(Id.Item_Picture_TextureColor, generalService.ObjectProperties.PicTextureColor);
                obj.SetEnumProperty(Id.Item_Picture_RectAlignment, (generalService.ObjectProperties.PicRectAlignment as any).value);
                obj.SetBoolProperty(Id.Item_Picture_IsSizeFactor, generalService.ObjectProperties.PictureIsSizeFactor);
                obj.SetBoolProperty(Id.Item_Picture_NeverUpsideDown, generalService.ObjectProperties.PictureNeverUpsideDown);
                break;
            case MapCore.IMcPolygonItem.NODE_TYPE:
                break;
            case MapCore.IMcTextItem.NODE_TYPE:
                let strValue: MapCore.SMcVariantString = new MapCore.SMcVariantString(generalService.ObjectProperties.TextString, generalService.ObjectProperties.TextIsUnicode);
                obj.SetStringProperty(Id.Item_Text_Text, strValue);
                obj.SetFVector2DProperty(Id.Item_TextBoundingRect_Scale, generalService.ObjectProperties.TextScale);
                obj.SetUIntProperty(Id.Item_TextBoundingRect_Margin, generalService.ObjectProperties.TextMargin);
                obj.SetBColorProperty(Id.Item_Text_Color, generalService.ObjectProperties.TextColor);
                obj.SetBColorProperty(Id.Item_TextBoundingRect_BackgroungColor, generalService.ObjectProperties.TextBackgroundColor);
                obj.SetEnumProperty(Id.Item_Text_Alignment, (generalService.ObjectProperties.TextAlignment as any).value);
                obj.SetBoolProperty(Id.Item_Text_RightToLeftReadingOrder, generalService.ObjectProperties.TextRtlReadingOrder);
                obj.SetBColorProperty(Id.Item_Text_OutlineColor, generalService.ObjectProperties.TextOutlineColor);
                obj.SetFontProperty(Id.Item_Text_Font, generalService.ObjectProperties.TextFont);
                obj.SetEnumProperty(Id.Item_TextBoundingRect_BackgroundShape, (generalService.ObjectProperties.BackgroundShape as any).value);
                obj.SetEnumProperty(Id.Item_TextBoundingRect_RectAlignment, (generalService.ObjectProperties.TextRectAlignment as any).value);
                obj.SetUIntProperty(Id.Item_TextBoundingRect_MarginY, generalService.ObjectProperties.TextMarginY);
                obj.SetEnumProperty(Id.Item_Text_NeverUpsideDown, (generalService.ObjectProperties.NeverUpsideDown as any).value);
                break;
            case MapCore.IMcEllipseItem.NODE_TYPE:
                (objectSchemeItem as MapCore.IMcEllipseItem).SetEllipseDefinition(generalService.ObjectProperties.EllipseDefinition);
                obj.SetFloatProperty(Id.Item_Ellipse_InnerRadiusFactor, generalService.ObjectProperties.EllipseInnerRadiusFactor);
                obj.SetFloatProperty(Id.Item_Ellipse_StartAngle, generalService.ObjectProperties.EllipseStartAngle);
                obj.SetFloatProperty(Id.Item_Ellipse_EndAngle, generalService.ObjectProperties.EllipseEndAngle);
                obj.SetFloatProperty(Id.Item_Ellipse_RadiusX, generalService.ObjectProperties.EllipseRadiusX);
                obj.SetFloatProperty(Id.Item_Ellipse_RadiusY, generalService.ObjectProperties.EllipseRadiusY);
                break;
            case MapCore.IMcRectangleItem.NODE_TYPE:
                (objectSchemeItem as MapCore.IMcRectangleItem).SetRectangleDefinition(generalService.ObjectProperties.RectangleDefinition);
                obj.SetFloatProperty(Id.Item_Rectangle_RadiusX, generalService.ObjectProperties.RectRadiusX);
                obj.SetFloatProperty(Id.Item_Rectangle_RadiusY, generalService.ObjectProperties.RectRadiusY);
                break;
        }
    }
    DoStartInitObject = (pScheme: MapCore.IMcObjectScheme) => {
        if (this.currentViewport) {
            let activeOverlay = ObjectWorldService.findActiveOverlayByMcViewport(this.currentViewport.viewport)

            if (pScheme != null && activeOverlay) {
                let pObject = MapCore.IMcObject.Create(activeOverlay, pScheme);
                store.dispatch(setNewGeneratedObject(pObject))
                let node: MapCore.IMcObjectSchemeItem = pScheme.GetNodeByID(Id.Item_ObjectSchemeNode_NodeId) as any;
                if (node != null) {
                    this.setPrivateProperties(pObject, node)
                }
                this.currentViewport.editMode.StartInitObject(pObject, node);
            }
            else if (!activeOverlay) {
                store.dispatch(setMapWindowDialogType('Confirm'))
            }
        }
    }

    //Actions Functions
    doLine = (): void => {
        // ObjectWorldService.doLine(props.viewport.id, generalService.ObjectProperties)
        runAsyncCodeSafely(async () => {
            if (this.currentViewport) {
                let fileName = this.getFilePathByProperties("default_line999_pp.mcsch");
                let lineScheme = await this.FetchObjectScheme(fileName);
                this.DoStartInitObject(lineScheme);
            }
        }, "mapWindow.doLine");
    }
    doArrow = () => {
        runAsyncCodeSafely(async () => {
            if (this.currentViewport) {
                // ObjectWorldService.doArrow(props.viewport.id, generalService.ObjectProperties)
                let fileName = this.getFilePathByProperties("default_arrow999_pp.mcsch");
                let lineScheme = await this.FetchObjectScheme(fileName);
                this.DoStartInitObject(lineScheme);
            }
        }, "mapWindow.doArrow");
    }
    doEllipseCPU = () => {
        runAsyncCodeSafely(async () => {
            if (this.currentViewport) {
                // ObjectWorldService.doEllipseCPU(props.viewport.id, generalService.ObjectProperties)
                let fileName = this.getFilePathByProperties("default_arc999_pp.mcsch");
                let lineScheme = await this.FetchObjectScheme(fileName);
                this.DoStartInitObject(lineScheme);
            }
        }, "mapWindow.doEllipseCPU");
    }

    doEllipse = () => {
        runAsyncCodeSafely(async () => {
            // ObjectWorldService.doEllipseGPU(props.viewport.id, generalService.ObjectProperties)
            let fileName = this.getFilePathByProperties("default_ellipse999_pp.mcsch");
            let lineScheme = await this.FetchObjectScheme(fileName);
            this.DoStartInitObject(lineScheme);

        }, "mapWindow.doEllipseGPU");
    }
    doPolygon = () => {
        runAsyncCodeSafely(async () => {
            // ObjectWorldService.doPolygon(props.viewport.id, generalService.ObjectProperties)
            let fileName = this.getFilePathByProperties("default_polygon999_pp.mcsch");
            let lineScheme = await this.FetchObjectScheme(fileName);
            this.DoStartInitObject(lineScheme);
        }, "mapWindow.doPolygon");
    }
    doText = () => {
        runAsyncCodeSafely(async () => {
            // ObjectWorldService.doText(props.viewport.id, generalService.ObjectProperties)
            if (!generalService.ObjectProperties.isFontSetAsDefault) {
                store.dispatch(setDialogType(DialogTypesEnum.font));
            }
            else {
                let fileName = this.getFilePathByProperties("default_text999_pp.mcsch");
                let lineScheme = await this.FetchObjectScheme(fileName);
                store.dispatch(setCreateText(true));
                this.DoStartInitObject(lineScheme);
            }
        }, "mapWindow.doText");
    }
    doLineExpansion = () => {
        runAsyncCodeSafely(async () => {
            // ObjectWorldService.doLineExpansion(props.viewport.id, generalService.ObjectProperties)
            let fileName = this.getFilePathByProperties("default_lineExp999_pp.mcsch");
            let lineScheme = await this.FetchObjectScheme(fileName);
            this.DoStartInitObject(lineScheme);

        }, "mapWindow.doLineExpansion");
    }
    doRectangle = () => {
        runAsyncCodeSafely(async () => {
            let fileName = this.getFilePathByProperties("default_rect999_pp.mcsch");
            let lineScheme = await this.FetchObjectScheme(fileName);
            this.DoStartInitObject(lineScheme);
            // ObjectWorldService.doRectangle(props.viewport.id, generalService.ObjectProperties)

        }, "mapWindow.doRectangle");
    }
    doPicture = () => {
        runAsyncCodeSafely(async () => {
            if (!generalService.ObjectProperties.PicIsDefaultTexture)
                store.dispatch(setDialogType(DialogTypesEnum.createTexture));
            else {
                let fileName = this.getFilePathByProperties("default_pic999_pp.mcsch");
                let lineScheme = await this.FetchObjectScheme(fileName);
                this.DoStartInitObject(lineScheme);
            }

        }, "mapWindow.doPicture");
    }
    getSymbolicItemsToolBar = (currentViewport: ViewportData): { header: string, menuItems: any[] } => {
        this.currentViewport = currentViewport;
        return {
            header: 'Symbolic Items',
            menuItems: [
                { label: "Line item", icon: "http:mctester icons/tsbLineItem.Image.png", action: () => { this.doLine() } },
                { label: "arrow item", icon: "http:mctester icons/tsbArrowItem.Image.png", action: () => { this.doArrow() } },
                { label: "line expansion item", icon: "http:mctester icons/tsbLineExpansionItem.Image.png", action: () => { this.doLineExpansion() } },
                { label: "polygon item", icon: "http:mctester icons/tsbPolygonItem.Image.png", action: () => { this.doPolygon() } },
                { label: "ellipse item", icon: "http:mctester icons/tsbEllipseItem.Image.png", action: () => { this.doEllipse() } },
                { label: "arc item", icon: "http:mctester icons/tsbArcItem.Image.png", action: () => { this.doEllipseCPU() } },
                { label: "rectangle item", icon: "http:mctester icons/tsbRectangleItem.Image.png", action: () => { this.doRectangle() } },
                { label: "text item", icon: "http:mctester icons/tsbTextItem.Image.png", action: () => { this.doText() } },
                { label: "picture item", icon: "http:mctester icons/tsbPictureItem.Image.png", action: () => { this.doPicture() } },
            ],
        };
    }
}
export default new symbolicItems();