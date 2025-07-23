import { IObjectPropertiesBase } from 'mapcore-lib';
import TexturePropertiesBase from './texturePropertiesBase';
// implements IObjectPropertiesBase
class ObjectPropertiesBase {

    //General Properties 
    SubItemsData: MapCore.IMcProperty.SArrayPropertySubItemData = new MapCore.IMcProperty.SArrayPropertySubItemData();
    MoveIfBlockedMaxChange = 0;
    MoveIfBlockedHeightAboveObstacle = 0;
    BlockedTransparency = 0;
    TransformOption = MapCore.IMcConditionalSelector.EActionOptions.EAO_USE_SELECTOR;
    VisibilityOption = MapCore.IMcConditionalSelector.EActionOptions.EAO_USE_SELECTOR;
    DrawPriorityGroup: MapCore.IMcSymbolicItem.EDrawPriorityGroup = MapCore.IMcSymbolicItem.EDrawPriorityGroup.EDPG_REGULAR;
    VerifyPrivatePropertiesId: boolean = false;
    LocationRelativeToDtm: boolean = false;
    ItemSubTypeFlags: number = (MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN as any).value;
    LocationCoordSys: MapCore.EMcPointCoordSystem = MapCore.EMcPointCoordSystem.EPCS_WORLD;
    LineTextureHeightRange: MapCore.SMcFVector2D = new MapCore.SMcFVector2D(0, -1)
    ImageCalc?: MapCore.IMcImageCalc;
    LocationMaxPoints: number = 0;
    Conplanar3dPriority: number = 0;
    DrawPriority: number = 0
    Transparency: number = 255;
    MoveIfBlockedMax: number = 255;
    MoveIfBlockedHeight: number = 255;

    // Symbolic Item - Attach point
    AttachPointType = MapCore.IMcSymbolicItem.EAttachPointType.EAPT_ALL_POINTS;
    AttachPointIndex = 0;
    AttachPointNumPoints = 1;
    BoundingBoxType = (MapCore.IMcSymbolicItem.EBoundingBoxPointFlags.EBBPF_CENTER as any);
    AttachPointPositionValue = 1;

    // Symbolic Item - transform param
    VectorTransformSegment = 0;

    // Symbolic Item - offset
    Orientation = MapCore.IMcSymbolicItem.EOffsetOrientation.EOO_RELATIVE_TO_PARENT_ROTATION;
    VectorOffsetCalc = MapCore.IMcSymbolicItem.EVectorOffsetCalc.EVOC_PARALLEL_DISTANCE;
    VectorOffsetValue = 0;
    Offset = MapCore.v3Zero;
    PointsIndicesAndDuplications: MapCore.IMcProperty.SArrayPropertyInt = new MapCore.IMcProperty.SArrayPropertyInt();
    PointsOffset: MapCore.IMcProperty.SArrayPropertyFVector3D = new MapCore.IMcProperty.SArrayPropertyFVector3D();

    // Symbolic Item - Rotation
    RotationYaw = 0;
    RotationPitch = 0;
    RotationRoll = 0;
    VectorRotation = false;

    //Line Base Properties
    LineWidth: number = 2;
    LineStyle: MapCore.IMcLineBasedItem.ELineStyle = MapCore.IMcLineBasedItem.ELineStyle.ELS_SOLID;
    LineColor: MapCore.SMcBColor = new MapCore.SMcBColor(255, 255, 0, 255);
    LineTexture: MapCore.IMcTexture = null;
    LineTextureProperties: TexturePropertiesBase;
    SidesFillTextureProperties: TexturePropertiesBase;
    LineBasedSmoothingLevels: number = 0;
    LineBasedGreatCirclePrecision: number = 0;
    LineOutlineWidth: number = 0;
    LineOutlineColor: MapCore.SMcBColor = new MapCore.SMcBColor(0, 0, 0, 0);
    PointOrderReverseMode: MapCore.IMcLineBasedItem.EPointOrderReverseMode = MapCore.IMcLineBasedItem.EPointOrderReverseMode.EPORM_NONE;
    LineTextureScale = 1;

    //Closed Shape Properties
    ShapeType: MapCore.IMcLineBasedItem.EShapeType = MapCore.IMcLineBasedItem.EShapeType.EST_2D;
    FillColor: MapCore.SMcBColor = new MapCore.SMcBColor(0, 128, 255, 255);
    FillStyle: MapCore.IMcLineBasedItem.EFillStyle = MapCore.IMcLineBasedItem.EFillStyle.EFS_SOLID;
    FillTexture: MapCore.IMcTexture = null;
    ShapeTextureProperties: TexturePropertiesBase;
    SidesFillColor: MapCore.SMcBColor = MapCore.bcBlackOpaque;
    SidesFillStyle: MapCore.IMcLineBasedItem.EFillStyle = MapCore.IMcLineBasedItem.EFillStyle.EFS_SOLID;
    SidesFillTexture: MapCore.IMcTexture = null
    VerticalHeight: number = 1;
    SidesFillTextureScale = new MapCore.SMcFVector2D(1, 1);
    FillTextureScale = new MapCore.SMcFVector2D(1, 1);

    //Arrow Properties
    ArrowCoordSys: MapCore.EMcPointCoordSystem = MapCore.EMcPointCoordSystem.EPCS_WORLD;
    ArrowHeadAngle: number = 45
    ArrowHeadSize: number = 10;
    ArrowGapSize: number = 0;

    //Arc Properties
    ArcCoordSys: MapCore.EMcPointCoordSystem = MapCore.EMcPointCoordSystem.EPCS_WORLD;
    ArcEllipseType: MapCore.IMcObjectSchemeItem.EGeometryType = MapCore.IMcObjectSchemeItem.EGeometryType.EGT_GEOMETRIC_IN_OVERLAY_MANAGER;
    ArcStartAngle: number = 0;
    ArcEndAngle: number = 180;
    ArcEllipseDefinition: MapCore.IMcObjectSchemeItem.EEllipseDefinition = MapCore.IMcObjectSchemeItem.EEllipseDefinition.EED_ELLIPSE_CENTER_RADIUSES_ANGLES
    ArcRadiusX = 0;
    ArcRadiusY = 0;

    //Ellipse Properties
    EllipseCoordSys: MapCore.EMcPointCoordSystem = MapCore.EMcPointCoordSystem.EPCS_WORLD;
    EllipseType: MapCore.IMcObjectSchemeItem.EGeometryType = MapCore.IMcObjectSchemeItem.EGeometryType.EGT_GEOMETRIC_IN_OVERLAY_MANAGER;
    EllipseStartAngle: number = 0;
    EllipseEndAngle: number = 360;
    EllipseInnerRadiusFactor: number = 0;
    EllipseDefinition: MapCore.IMcObjectSchemeItem.EEllipseDefinition = MapCore.IMcObjectSchemeItem.EEllipseDefinition.EED_ELLIPSE_CENTER_RADIUSES_ANGLES
    EllipseRadiusY = 0;
    EllipseRadiusX = 0;

    //Text Properties
    TextCoordSys: MapCore.EMcPointCoordSystem = MapCore.EMcPointCoordSystem.EPCS_SCREEN;
    TextFont?: MapCore.IMcFont;
    isFontSetAsDefault: boolean;
    NeverUpsideDown: MapCore.IMcTextItem.ENeverUpsideDownMode = MapCore.IMcTextItem.ENeverUpsideDownMode.ENUDM_NONE;
    TextScale: MapCore.SMcFVector2D = new MapCore.SMcFVector2D(1, 1);
    TextMargin: number = 0;
    TextColor: MapCore.SMcBColor = new MapCore.SMcBColor(255, 255, 0, 255);
    TextBackgroundColor: MapCore.SMcBColor = new MapCore.SMcBColor(0, 128, 255, 255);
    TextIsUnicode: boolean = false;
    TextOutlineColor: MapCore.SMcBColor = new MapCore.SMcBColor(0, 0, 0, 0);
    TextMarginY: number = MapCore.UINT_MAX;
    BackgroundShape: MapCore.IMcTextItem.EBackgroundShape = MapCore.IMcTextItem.EBackgroundShape.EBS_RECTANGLE;
    TextRtlReadingOrder = false;
    TextAlignment = MapCore.EAxisXAlignment.EXA_CENTER;
    TextString = "";
    TextCurMappingPos = -1;
    IsWaitForCreate = false;
    TextFontsMap: MapCore.IMcLogFont.SLogFontToTtfFile[] = []
    TextCurMapping: MapCore.IMcLogFont.SLogFontToTtfFile = null


    // NeverUpsideDown: MapCore.IMcTextItem.ENeverUpsideDownMode = MapCore.IMcTextItem.ENeverUpsideDownMode.ENUDM_NONE;
    // BackgroundShape: MapCore.IMcTextItem.EBackgroundShape = MapCore.IMcTextItem.EBackgroundShape.EBS_RECTANGLE;
    // TextMarginY: number = Constants.UINT_MAX;
    // PictureNeverUpsideDown: boolean = false;

    // to do in form
    TextRectAlignment: MapCore.IMcSymbolicItem.EBoundingRectanglePoint = MapCore.IMcSymbolicItem.EBoundingRectanglePoint.EBRP_CENTER;



    //Line Expansion Properties
    LineExpansionCoordinateSystem: MapCore.EMcPointCoordSystem = MapCore.EMcPointCoordSystem.EPCS_WORLD;
    LineExpansionType: MapCore.IMcObjectSchemeItem.EGeometryType = MapCore.IMcObjectSchemeItem.EGeometryType.EGT_GEOGRAPHIC
    LineExpansionRadius: number = 1;

    //Rectangle Properties
    RectangleCoordSys: MapCore.EMcPointCoordSystem = MapCore.EMcPointCoordSystem.EPCS_WORLD;
    RectangleType: MapCore.IMcObjectSchemeItem.EGeometryType = MapCore.IMcObjectSchemeItem.EGeometryType.EGT_GEOMETRIC_IN_OVERLAY_MANAGER;
    RectangleDefinition: MapCore.IMcRectangleItem.ERectangleDefinition = MapCore.IMcRectangleItem.ERectangleDefinition.ERD_RECTANGLE_DIAGONAL_POINTS;
    RectRadiusX = 0;
    RectRadiusY = 0;

    //Picture Properties
    PictureCoordSys: MapCore.EMcPointCoordSystem = MapCore.EMcPointCoordSystem.EPCS_SCREEN;
    PictureIsSizeFactor: boolean = true;
    PictureIsUseTextureGeoReferencing: boolean = false;
    PicWidth: number = 1;
    PicHeight: number = 1;
    PictureNeverUpsideDown: boolean = false;
    PicTexture: MapCore.IMcTexture = null;
    PicTextureProperties: TexturePropertiesBase;
    PicIsDefaultTexture: boolean = false;
    PicTextureColor = new MapCore.SMcBColor(255, 255, 255, 255);

    PicRectAlignment = MapCore.IMcSymbolicItem.EBoundingRectanglePoint.EBRP_CENTER;

    SightPresentation = new SightPresentation();
    constructor() { }

}
class SightPresentation {
    colorsByVisibility: Map<MapCore.IMcSpatialQueries.EPointVisibility, MapCore.SMcBColor> = new Map([
        [MapCore.IMcSpatialQueries.EPointVisibility.EPV_SEEN, new MapCore.SMcBColor(0, 255, 0, 255)],
        [MapCore.IMcSpatialQueries.EPointVisibility.EPV_UNSEEN, new MapCore.SMcBColor(255, 0, 0, 255)],
        [MapCore.IMcSpatialQueries.EPointVisibility.EPV_UNKNOWN, new MapCore.SMcBColor(128, 128, 128, 0)],
        [MapCore.IMcSpatialQueries.EPointVisibility.EPV_OUT_OF_QUERY_AREA, new MapCore.SMcBColor(128, 128, 128, 0)],
        [MapCore.IMcSpatialQueries.EPointVisibility.EPV_SEEN_STATIC_OBJECT, new MapCore.SMcBColor(0, 0, 0, 0)],
        [MapCore.IMcSpatialQueries.EPointVisibility.EPV_ASYNC_CALCULATING, new MapCore.SMcBColor(0, 0, 0, 0)],
    ])
    type = MapCore.IMcSightPresentationItemParams.ESightPresentationType.ESPT_NONE;
    isObserverHeightAbsolute = false;
    isObservedHeightAbsolute = false;
    precision = MapCore.IMcSpatialQueries.EQueryPrecision.EQP_HIGHEST;
    numEllipseRays = 64;
    observedHeight = 1.7;
    observerHeight = 1.7;
    minPitch = -90;
    maxPitch = 90;
    sightTextureResolution = 20;

    constructor() { }
}
export default ObjectPropertiesBase 
