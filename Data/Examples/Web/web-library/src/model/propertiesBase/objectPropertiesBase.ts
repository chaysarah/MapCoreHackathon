 interface IObjectPropertiesBase {
    //General Properties 
    DrawPriorityGroup: MapCore.IMcSymbolicItem.EDrawPriorityGroup;
    VerifyPrivatePropertiesId: boolean;
    LocationRelativeToDtm: boolean;
    ItemSubTypeFlags: number;
    LocationCoordSys: MapCore.EMcPointCoordSystem;
    LineTextureHeightRange: MapCore.SMcFVector2D
    ImageCalc?: MapCore.IMcImageCalc;

    //Line Base Properties
    LineWidth: number;
    LineStyle: MapCore.IMcLineBasedItem.ELineStyle;
    LineColor: MapCore.SMcBColor;
    LineTexture: MapCore.IMcTexture;
    LineBasedSmoothingLevels: number;
    LineBasedGreatCirclePrecision: number;
    LineOutlineWidth: number;
    LineOutlineColor: MapCore.SMcBColor;
    PointOrderReverseMode: MapCore.IMcLineBasedItem.EPointOrderReverseMode;

    //Closed Shape Properties
    ShapeType: MapCore.IMcLineBasedItem.EShapeType;
    FillColor: MapCore.SMcBColor;
    FillStyle: MapCore.IMcLineBasedItem.EFillStyle;
    FillTexture: MapCore.IMcTexture;
    SidesFillColor: MapCore.SMcBColor;
    SidesFillStyle: MapCore.IMcLineBasedItem.EFillStyle;
    SidesFillTexture: MapCore.IMcTexture;
    VerticalHeight: number;

    //Arrow Properties
    ArrowCoordSys: MapCore.EMcPointCoordSystem;
    ArrowHeadAngle: number
    ArrowHeadSize: number;
    ArrowGapSize: number;

    //Arc Properties
    ArcCoordSys: MapCore.EMcPointCoordSystem;
    ArcEllipseType: MapCore.IMcObjectSchemeItem.EGeometryType;
    ArcStartAngle: number;
    ArcEndAngle: number;
    ArcEllipseDefinition: MapCore.IMcObjectSchemeItem.EEllipseDefinition;

    //Ellipse Properties
    EllipseCoordSys: MapCore.EMcPointCoordSystem;
    EllipseType: MapCore.IMcObjectSchemeItem.EGeometryType;
    EllipseStartAngle: number;
    EllipseEndAngle: number;
    EllipseInnerRadiusFactor: number;
    EllipseDefinition: MapCore.IMcObjectSchemeItem.EEllipseDefinition

    //Text Properties
    TextCoordSys: MapCore.EMcPointCoordSystem;
    TextFont?: MapCore.IMcFont
    NeverUpsideDown: MapCore.IMcTextItem.ENeverUpsideDownMode;
    TextScale: MapCore.SMcFVector2D;
    TextMargin: number;
    TextColor: MapCore.SMcBColor;
    TextBackgroundColor: MapCore.SMcBColor;
    TextIsUnicode: boolean;
    TextOutlineColor: MapCore.SMcBColor;
    TextMarginY: number;
    BackgroundShape: MapCore.IMcTextItem.EBackgroundShape;

    //Line Expansion Properties
    LineExpansionCoordinateSystem: MapCore.EMcPointCoordSystem;
    LineExpansionType: MapCore.IMcObjectSchemeItem.EGeometryType
    LineExpansionRadius: number;

    //Rectangle Properties
    RectangleCoordSys: MapCore.EMcPointCoordSystem;
    RectangleType: MapCore.IMcObjectSchemeItem.EGeometryType;
    RectangleDefinition: MapCore.IMcRectangleItem.ERectangleDefinition;

    //Picture Properties
    PictureCoordSys: MapCore.EMcPointCoordSystem;
    PictureIsSizeFactor: boolean;
    PictureIsUseTextureGeoReferencing: boolean;
    PicWidth: number;
    PicHeight: number;
    PicRectAlignmentBitField: MapCore.IMcSymbolicItem.EBoundingBoxPointFlags;
    PictureNeverUpsideDown: boolean;
}


export default IObjectPropertiesBase 
