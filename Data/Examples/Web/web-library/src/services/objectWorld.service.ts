import { ViewportData } from "../model/viewportData";
import MapCoreData from '../mapcore-data';
// import texturePropertiesBase from "../model/propertiesBase/texturePropertiesBase";
import { runMapCoreSafely } from "./error-handler.service";
import IObjectPropertiesBase from "../model/propertiesBase/objectPropertiesBase"
import { getBitFieldByEnumArr, getEnumDetailsList, getEnumValueDetails } from "./tools.service";
import mapcoreData from "../mapcore-data";
// import textureService from "./texture.service";

class ObjectWorldService {
    pixelFormatSize: number = 0;
    imageData: ImageData;
    stride: number;

    //#region doObject
    public doLine(viewportId: number, objectProperties?: IObjectPropertiesBase) {
        const viewportData: ViewportData = MapCoreData.findViewport(viewportId)
        if (viewportData) {
            let activeOverlay = this.findActiveOverlayByMcViewport(viewportData.viewport)

            if (activeOverlay != null) {
                let locationPoints: MapCore.SMcVector3D[] = [];

                let ObjSchemeItem: MapCore.IMcLineItem = null;
                runMapCoreSafely(() => {
                    ObjSchemeItem = MapCore.IMcLineItem.Create(objectProperties.ItemSubTypeFlags, objectProperties.LineStyle
                        , objectProperties.LineColor, objectProperties.LineWidth
                        , objectProperties.LineTexture,
                        objectProperties.LineTextureHeightRange
                    )
                }, "objectWorldService.doLine => IMcLineItem.Create", true)

                let obj: MapCore.IMcObject = null;
                runMapCoreSafely(() => {
                    obj = MapCore.IMcObject.Create(activeOverlay, ObjSchemeItem, objectProperties.LocationCoordSys,
                        locationPoints, objectProperties.LocationRelativeToDtm);
                }, "objectWorldService.doLine => IMcObject.Create", true)


                runMapCoreSafely(() => { ObjSchemeItem.SetNumSmoothingLevels(objectProperties.LineBasedSmoothingLevels); }, "objectWorldService.doLine => IMcLineBasedItem.SetNumSmoothingLevels", true)
                runMapCoreSafely(() => { ObjSchemeItem.SetGreatCirclePrecision(objectProperties.LineBasedGreatCirclePrecision); }, "objectWorldService.doLine => IMcLineBasedItem.SetGreatCirclePrecision", true)
                runMapCoreSafely(() => { ObjSchemeItem.SetDrawPriorityGroup(objectProperties.DrawPriorityGroup); }, "objectWorldService.doLine => IMcSymbolicItem.SetDrawPriorityGroup", true)
                runMapCoreSafely(() => { ObjSchemeItem.SetOutlineColor(objectProperties.LineOutlineColor); }, "objectWorldService.doLine => IMcLineBasedItem.SetOutlineColor", true)
                runMapCoreSafely(() => { ObjSchemeItem.SetOutlineWidth(objectProperties.LineOutlineWidth); }, "objectWorldService.doLine => IMcLineBasedItem.SetOutlineWidth", true)
                runMapCoreSafely(() => { ObjSchemeItem.SetShapeType(objectProperties.ShapeType); }, "objectWorldService.doLine => IMcLineBasedItem.SetShapeType", true)
                runMapCoreSafely(() => { ObjSchemeItem.SetVerticalHeight(objectProperties.VerticalHeight); }, "objectWorldService.doLine => IMcLineBasedItem.SetVerticalHeight", true)
                runMapCoreSafely(() => { ObjSchemeItem.SetSidesFillStyle(objectProperties.SidesFillStyle); }, "objectWorldService.doLine => IMcLineBasedItem.SetSidesFillStyle", true)
                runMapCoreSafely(() => { ObjSchemeItem.SetSidesFillColor(objectProperties.SidesFillColor); }, "objectWorldService.doLine => IMcLineBasedItem.SetSidesFillColor", true)
                runMapCoreSafely(() => { ObjSchemeItem.SetSidesFillTexture(objectProperties.SidesFillTexture); }, "objectWorldService.doLine => IMcLineBasedItem.SetSidesFillTexture", true)
                runMapCoreSafely(() => { ObjSchemeItem.SetPointOrderReverseMode(objectProperties.PointOrderReverseMode); }, "objectWorldService.doLine => IMcLineBasedItem.SetPointOrderReverseMode", true)
                if (objectProperties.ImageCalc != null)
                    runMapCoreSafely(() => { obj.SetImageCalc(objectProperties.ImageCalc); }, "objectWorldService.doLine => IMcObject.SetImageCalc", true);
                runMapCoreSafely(() => { viewportData.editMode.StartInitObject(obj, ObjSchemeItem) }, "objectWorldService.doLine => IMcEditMode.StartInitObject", true);
            }
            else
                alert("There is no active overlay");
        }
    }
    public doLineExpansion(viewportId: number, objectProperties: IObjectPropertiesBase) {
        const viewportData: ViewportData = MapCoreData.findViewport(viewportId)
        if (viewportData) {
            let activeOverlay = this.findActiveOverlayByMcViewport(viewportData.viewport)
            if (activeOverlay != null) {
                let locationPoints: MapCore.SMcVector3D[] = [];
                let ObjSchemeItem: MapCore.IMcLineExpansionItem = null;
                runMapCoreSafely(() => {
                    ObjSchemeItem = MapCore.IMcLineExpansionItem.Create(objectProperties.ItemSubTypeFlags,
                        objectProperties.LineExpansionCoordinateSystem,
                        objectProperties.LineExpansionType,
                        objectProperties.LineExpansionRadius,
                        objectProperties.LineStyle,
                        objectProperties.LineColor,
                        objectProperties.LineWidth,
                        objectProperties.LineTexture,
                        objectProperties.LineTextureHeightRange,
                        2,
                        objectProperties.FillStyle,
                        objectProperties.FillColor,
                        objectProperties.FillTexture,
                        new MapCore.SMcFVector2D(1, 1)
                    );
                }, "objectWorldService.doLineExpansion => IMcLineExpansionItem.Create", true);

                let obj: MapCore.IMcObject = null;
                runMapCoreSafely(() => {
                    obj = MapCore.IMcObject.Create(activeOverlay,
                        ObjSchemeItem,
                        objectProperties.LocationCoordSys,
                        locationPoints,
                        objectProperties.LocationRelativeToDtm);
                }, "objectWorldService.doLineExpansion => IMcObject.Create", true);

                runMapCoreSafely(() => { ObjSchemeItem.SetNumSmoothingLevels(objectProperties.LineBasedSmoothingLevels); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetNumSmoothingLevels", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetGreatCirclePrecision(objectProperties.LineBasedGreatCirclePrecision); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetGreatCirclePrecision", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetShapeType(objectProperties.ShapeType); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetShapeType", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetVerticalHeight(objectProperties.VerticalHeight); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetVerticalHeight", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetSidesFillStyle(objectProperties.SidesFillStyle); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetSidesFillStyle", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetSidesFillColor(objectProperties.SidesFillColor); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetSidesFillColor", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetSidesFillTexture(objectProperties.SidesFillTexture); }, "objectWorldService.doLineExpansion => McLineBasedItem.SetSidesFillTexture", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetDrawPriorityGroup(objectProperties.DrawPriorityGroup); }, "objectWorldService.doLineExpansion => IMcSymbolicItem.SetDrawPriorityGroup", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetOutlineColor(objectProperties.LineOutlineColor); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetOutlineColor", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetOutlineWidth(objectProperties.LineOutlineWidth); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetOutlineWidth", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetPointOrderReverseMode(objectProperties.PointOrderReverseMode); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetPointOrderReverseMode", true);

                if (objectProperties.ImageCalc != null)
                    runMapCoreSafely(() => { obj.SetImageCalc(objectProperties.ImageCalc); }, "objectWorldService.doLineExpansion => IMcObject.SetImageCalc", true);
                runMapCoreSafely(() => { viewportData.editMode.StartInitObject(obj, ObjSchemeItem) }, "objectWorldService.doLineExpansion => IMcEditMode.StartInitObject", true);
            }
            else
                alert("There is no active overlay");
        }
    }
    public doEllipseCPU(viewportId: number, objectProperties: IObjectPropertiesBase) {
        const viewportData: ViewportData = MapCoreData.findViewport(viewportId)
        if (viewportData) {
            let activeOverlay = this.findActiveOverlayByMcViewport(viewportData.viewport)
            if (activeOverlay != null) {
                let locationPoints: MapCore.SMcVector3D[] = [];
                let ObjSchemeItem: MapCore.IMcArcItem = null;
                runMapCoreSafely(() => {
                    ObjSchemeItem = MapCore.IMcArcItem.Create(objectProperties.ItemSubTypeFlags,
                        objectProperties.ArcCoordSys,
                        objectProperties.ArcEllipseType,
                        1,
                        1,
                        objectProperties.ArcStartAngle,
                        objectProperties.ArcEndAngle,
                        objectProperties.LineStyle,
                        objectProperties.LineColor,
                        objectProperties.LineWidth,
                        objectProperties.LineTexture,
                        objectProperties.LineTextureHeightRange,
                    );
                }, "objectWorldService.doLineExpansion => IMcArcItem.Create", true);
                let obj: MapCore.IMcObject = null;
                runMapCoreSafely(() => {
                    obj = MapCore.IMcObject.Create(activeOverlay,
                        ObjSchemeItem,
                        objectProperties.LocationCoordSys,
                        locationPoints,
                        objectProperties.LocationRelativeToDtm);
                }, "objectWorldService.doLineExpansion => IMcObject.Create", true);

                runMapCoreSafely(() => { ObjSchemeItem.SetEllipseDefinition(objectProperties.ArcEllipseDefinition); }, "objectWorldService.doLineExpansion => IMcArcItem.SetEllipseDefinition", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetNumSmoothingLevels(objectProperties.LineBasedSmoothingLevels); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetNumSmoothingLevels", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetGreatCirclePrecision(objectProperties.LineBasedGreatCirclePrecision); }, "objectWorldService.doLineExpansion =>IMcLineBasedItem.SetGreatCirclePrecision", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetDrawPriorityGroup(objectProperties.DrawPriorityGroup); }, "objectWorldService.doLineExpansion => IMcSymbolicItem.SetDrawPriorityGroup", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetOutlineColor(objectProperties.LineOutlineColor); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetOutlineColor", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetOutlineWidth(objectProperties.LineOutlineWidth); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetOutlineWidth", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetShapeType(objectProperties.ShapeType); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetShapeType", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetVerticalHeight(objectProperties.VerticalHeight); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetVerticalHeight", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetSidesFillStyle(objectProperties.SidesFillStyle); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetSidesFillStyle", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetSidesFillColor(objectProperties.SidesFillColor); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetSidesFillColor", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetSidesFillTexture(objectProperties.SidesFillTexture); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetSidesFillTexture", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetPointOrderReverseMode(objectProperties.PointOrderReverseMode); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetPointOrderReverseMode", true);

                if (objectProperties.ImageCalc != null)
                    runMapCoreSafely(() => { obj.SetImageCalc(objectProperties.ImageCalc); }, "objectWorldService.doLineExpansion => IMcObject.SetImageCalc", true);

                runMapCoreSafely(() => { viewportData.editMode.StartInitObject(obj, ObjSchemeItem) }, "objectWorldService.doLineExpansion => IMcEditMode.StartInitObject", true);
            }
            else
                alert("There is no active overlay");
        }
    }
    public doEllipseGPU(viewportId: number, objectProperties: IObjectPropertiesBase) {
        const viewportData: ViewportData = MapCoreData.findViewport(viewportId)
        if (viewportData) {
            let activeOverlay = this.findActiveOverlayByMcViewport(viewportData.viewport)
            if (activeOverlay != null) {
                let locationPoints: MapCore.SMcVector3D[] = [];

                let ObjSchemeItem: MapCore.IMcEllipseItem = null;
                runMapCoreSafely(() => {
                    ObjSchemeItem = MapCore.IMcEllipseItem.Create(objectProperties.ItemSubTypeFlags,
                        objectProperties.EllipseCoordSys,
                        objectProperties.EllipseType,
                        1,
                        1,
                        objectProperties.EllipseStartAngle,
                        objectProperties.EllipseEndAngle,
                        objectProperties.EllipseInnerRadiusFactor,
                        objectProperties.LineStyle,
                        objectProperties.LineColor,
                        objectProperties.LineWidth,
                        objectProperties.LineTexture,
                        objectProperties.LineTextureHeightRange,
                        4,
                        objectProperties.FillStyle,
                        objectProperties.FillColor,
                        objectProperties.FillTexture,
                        new MapCore.SMcFVector2D(1, 1)
                    );
                }, "objectWorldService.doLineExpansion => IMcEllipseItem.Create", true);

                let obj: MapCore.IMcObject = null;
                runMapCoreSafely(() => {
                    obj = MapCore.IMcObject.Create(activeOverlay,
                        ObjSchemeItem,
                        objectProperties.LocationCoordSys,
                        locationPoints,
                        objectProperties.LocationRelativeToDtm);
                }, "objectWorldService.doLineExpansion => IMcObject.Create", true);

                runMapCoreSafely(() => { ObjSchemeItem.SetEllipseDefinition(objectProperties.EllipseDefinition); }, "objectWorldService.doLineExpansion => IMcEllipseItem.SetEllipseDefinition", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetNumSmoothingLevels(objectProperties.LineBasedSmoothingLevels); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetNumSmoothingLevels", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetGreatCirclePrecision(objectProperties.LineBasedGreatCirclePrecision); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetGreatCirclePrecision", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetShapeType(objectProperties.ShapeType); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetShapeType", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetVerticalHeight(objectProperties.VerticalHeight); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetVerticalHeight", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetSidesFillStyle(objectProperties.SidesFillStyle); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetSidesFillStyle", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetSidesFillColor(objectProperties.SidesFillColor); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetSidesFillColor", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetSidesFillTexture(objectProperties.SidesFillTexture); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetSidesFillTexture", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetDrawPriorityGroup(objectProperties.DrawPriorityGroup); }, "objectWorldService.doLineExpansion => IMcSymbolicItem.SetDrawPriorityGroup", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetOutlineColor(objectProperties.LineOutlineColor); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetOutlineColor", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetOutlineWidth(objectProperties.LineOutlineWidth); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetOutlineWidth", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetPointOrderReverseMode(objectProperties.PointOrderReverseMode); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetPointOrderReverseMode", true);

                if (objectProperties.ImageCalc != null)
                    runMapCoreSafely(() => { obj.SetImageCalc(objectProperties.ImageCalc); }, "objectWorldService.doLineExpansion => IMcObject.SetImageCalc", true);
                runMapCoreSafely(() => { viewportData.editMode.StartInitObject(obj, ObjSchemeItem) }, "objectWorldService.doLineExpansion => IMcEditMode.StartInitObject", true);
            }
            else
                alert("There is no active overlay");
        }
    }
    public doPolygon(viewportId: number, objectProperties: IObjectPropertiesBase) {
        const viewportData: ViewportData = MapCoreData.findViewport(viewportId)
        if (viewportData) {
            let activeOverlay = this.findActiveOverlayByMcViewport(viewportData.viewport)
            if (activeOverlay != null) {
                let locationPoints: MapCore.SMcVector3D[] = [];
                let ObjSchemeItem: MapCore.IMcPolygonItem = null;
                runMapCoreSafely(() => {
                    ObjSchemeItem = MapCore.IMcPolygonItem.Create(objectProperties.ItemSubTypeFlags,
                        objectProperties.LineStyle,
                        objectProperties.LineColor,
                        objectProperties.LineWidth,
                        objectProperties.LineTexture,
                        objectProperties.LineTextureHeightRange,
                        2,
                        objectProperties.FillStyle,
                        objectProperties.FillColor,
                        objectProperties.FillTexture,
                        new MapCore.SMcFVector2D(1, 1)
                    );
                }, "objectWorldService.doLineExpansion => IMcPolygonItem.Create", true);

                let obj: MapCore.IMcObject = null;
                runMapCoreSafely(() => {
                    obj = MapCore.IMcObject.Create(activeOverlay,
                        ObjSchemeItem,
                        objectProperties.LocationCoordSys,
                        locationPoints,
                        objectProperties.LocationRelativeToDtm);
                }, "objectWorldService.doLineExpansion => IMcObject.Create", true);

                runMapCoreSafely(() => { ObjSchemeItem.SetNumSmoothingLevels(objectProperties.LineBasedSmoothingLevels); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetNumSmoothingLevels", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetGreatCirclePrecision(objectProperties.LineBasedGreatCirclePrecision); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetGreatCirclePrecision", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetShapeType(objectProperties.ShapeType); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetShapeType", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetVerticalHeight(objectProperties.VerticalHeight); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetVerticalHeight", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetSidesFillStyle(objectProperties.SidesFillStyle); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetSidesFillStyle", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetSidesFillColor(objectProperties.SidesFillColor); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetSidesFillColor", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetSidesFillTexture(objectProperties.SidesFillTexture); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetSidesFillTexture", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetDrawPriorityGroup(objectProperties.DrawPriorityGroup); }, "objectWorldService.doLineExpansion => IMcSymbolicItem.SetDrawPriorityGroup", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetOutlineColor(objectProperties.LineOutlineColor); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetOutlineColor", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetOutlineWidth(objectProperties.LineOutlineWidth); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetOutlineWidth", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetPointOrderReverseMode(objectProperties.PointOrderReverseMode); }, "objectWorldService.doLineExpansion => IMcLineBasedItem.SetPointOrderReverseMode", true);

                if (objectProperties.ImageCalc != null)
                    runMapCoreSafely(() => { obj.SetImageCalc(objectProperties.ImageCalc); }, "objectWorldService.doLineExpansion => IMcObject.SetImageCalc", true);

                runMapCoreSafely(() => { viewportData.editMode.StartInitObject(obj, ObjSchemeItem); }, "objectWorldService.doLineExpansion => IMcEditMode.StartInitObject", true);
            }
            else
                alert("There is no active overlay");
        }
    }
    public doArrow(viewportId: number, objectProperties: IObjectPropertiesBase) {

        const viewportData: ViewportData = MapCoreData.findViewport(viewportId)
        if (viewportData) {

            let activeOverlay = this.findActiveOverlayByMcViewport(viewportData.viewport)
            let locationPoints: MapCore.SMcVector3D[] = [];

            if (activeOverlay != null) {
                let ObjSchemeItem: MapCore.IMcArrowItem = null;
                runMapCoreSafely(() => {
                    ObjSchemeItem = MapCore.IMcArrowItem.Create(objectProperties.ItemSubTypeFlags,
                        objectProperties.ArrowCoordSys,
                        objectProperties.ArrowHeadSize,
                        objectProperties.ArrowHeadAngle,
                        objectProperties.ArrowGapSize,
                        objectProperties.LineStyle,
                        objectProperties.LineColor,
                        objectProperties.LineWidth,
                        objectProperties.LineTexture,
                        objectProperties.LineTextureHeightRange
                    );
                }, "objectWorldService.doArrow => IMcArrowItem.Create", true);

                let obj: MapCore.IMcObject = null;
                runMapCoreSafely(() => {
                    obj = MapCore.IMcObject.Create(activeOverlay,
                        ObjSchemeItem,
                        objectProperties.LocationCoordSys,
                        locationPoints,
                        objectProperties.LocationRelativeToDtm);
                }, "objectWorldService.doArrow => IMcObject.Create", true);

                runMapCoreSafely(() => { ObjSchemeItem.SetNumSmoothingLevels(objectProperties.LineBasedSmoothingLevels); }, "objectWorldService.doArrow => IMcLineBasedItem.SetNumSmoothingLevels", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetGreatCirclePrecision(objectProperties.LineBasedGreatCirclePrecision); }, "objectWorldService.doArrow => IMcLineBasedItem.SetGreatCirclePrecision", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetShapeType(objectProperties.ShapeType); }, "objectWorldService.doArrow => IMcLineBasedItem.SetShapeType", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetVerticalHeight(objectProperties.VerticalHeight); }, "objectWorldService.doArrow => IMcLineBasedItem.SetVerticalHeight", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetSidesFillStyle(objectProperties.SidesFillStyle); }, "objectWorldService.doArrow => IMcLineBasedItem.SetSidesFillStyle", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetSidesFillColor(objectProperties.SidesFillColor); }, "objectWorldService.doArrow => IMcLineBasedItem.SetSidesFillColor", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetSidesFillTexture(objectProperties.SidesFillTexture); }, "objectWorldService.doArrow => IMcLineBasedItem.SetSidesFillTexture", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetDrawPriorityGroup(objectProperties.DrawPriorityGroup); }, "objectWorldService.doArrow => IMcSymbolicItem.SetDrawPriorityGroup", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetOutlineColor(objectProperties.LineOutlineColor); }, "objectWorldService.doArrow => IMcLineBasedItem.SetOutlineColor", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetOutlineWidth(objectProperties.LineOutlineWidth); }, "objectWorldService.doArrow => IMcLineBasedItem.SetOutlineWidth", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetPointOrderReverseMode(objectProperties.PointOrderReverseMode); }, "objectWorldService.doArrow => IMcLineBasedItem.SetPointOrderReverseMode", true);
                if (objectProperties.ImageCalc != null)
                    runMapCoreSafely(() => { obj.SetImageCalc(objectProperties.ImageCalc); }, "objectWorldService.doArrow => IMcObject.SetImageCalc", true);
                runMapCoreSafely(() => { viewportData.editMode.StartInitObject(obj, ObjSchemeItem); }, "objectWorldService.doArrow => IMcEditMode.StartInitObject", true);
            }
            else
                alert("There is no active overlay");
        }
    }
    public doText(viewportId: number, objectProperties: IObjectPropertiesBase) {
        const viewportData: ViewportData = MapCoreData.findViewport(viewportId)
        if (viewportData) {
            let activeOverlay = this.findActiveOverlayByMcViewport(viewportData.viewport)
            if (activeOverlay != null) {
                let locationPoints: MapCore.SMcVector3D[] = [];
                // frmFontDialog FontDialogForm = new frmFontDialog((IDNMcFont)objectProperties.TextFont, true);
                // if (FontDialogForm.ShowDialog() == DialogResult.OK)
                // {// let m_Font: MapCore.IMcFont = FontDialogForm.CurrFont;

                // let logFont: MapCore.SMcVariantLogFont = new MapCore.SMcVariantLogFont();
                // logFont.lfFaceName = "Arial";
                // logFont.lfWeight = 400;
                // logFont.lfHeight = 12;

                // logFont.LogFont.lfItalic = (byte) (mItalicCB.isChecked() ? 1 : 0);  
                //   logFont.LogFont.lfUnderline = (byte) (mUnderLineCB.isChecked() ? 1 : 0); 
                //  logFont.LogFont.lfWeight = (mBoldCB.isChecked() ? 700 : 400);   
                //   logFont.bIsUnicode = mTxtIsUnicodeCb.isChecked();    return logFont;}

                // let m_Font: MapCore.IMcFont = null;
                // runMapCoreSafely(() => { m_Font = MapCore.IMcLogFont.Create(logFont, false) }, "objectWorldService.doText => IMcLogFont.Create", true);
                // objectProperties.TextFont = m_Font;
                let ObjSchemeItem: MapCore.IMcTextItem = null;
                runMapCoreSafely(() => {
                    ObjSchemeItem = MapCore.IMcTextItem.Create(objectProperties.ItemSubTypeFlags,
                        objectProperties.TextCoordSys,
                        objectProperties.TextFont,
                        objectProperties.TextScale,
                        objectProperties.NeverUpsideDown,
                        MapCore.EAxisXAlignment.EXA_CENTER,
                        MapCore.IMcSymbolicItem.EBoundingRectanglePoint.EBRP_CENTER,
                        false,
                        objectProperties.TextMargin,
                        objectProperties.TextColor,
                        objectProperties.TextBackgroundColor);
                }, "objectWorldService.doText => IMcTextItem.Create", true);

                let obj: MapCore.IMcObject = null;
                runMapCoreSafely(() => {
                    obj = MapCore.IMcObject.Create(activeOverlay,
                        ObjSchemeItem,
                        objectProperties.LocationCoordSys,
                        locationPoints,
                        objectProperties.LocationRelativeToDtm);
                }, "objectWorldService.doText => IMcObject.Create", true);

                runMapCoreSafely(() => { ObjSchemeItem.SetDrawPriorityGroup(objectProperties.DrawPriorityGroup); }, "objectWorldService.doText => IMcSymbolicItem.SetDrawPriorityGroup", true);
                let varString: MapCore.SMcVariantString = new MapCore.SMcVariantString("text", objectProperties.TextIsUnicode);
                runMapCoreSafely(() => { ObjSchemeItem.SetText(varString); }, "objectWorldService.doText => IMcTextItem.SetText", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetOutlineColor(objectProperties.TextOutlineColor); }, "objectWorldService.doText => IMcTextItem.SetOutlineColor", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetMarginY(objectProperties.TextMarginY); }, "objectWorldService.doText => IMcTextItem.SetMarginY", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetBackgroundShape(objectProperties.BackgroundShape); }, "objectWorldService.doText => IMcTextItem.SetBackgroundShape", true);

                if (objectProperties.ImageCalc != null)
                    runMapCoreSafely(() => { obj.SetImageCalc(objectProperties.ImageCalc); }, "objectWorldService.doText => IMcObject.SetImageCalc", true);
                runMapCoreSafely(() => { viewportData.editMode.StartInitObject(obj, ObjSchemeItem); }, "objectWorldService.doText => IMcEditMode.StartInitObject", true);
            }
            else
                alert("There is no active overlay");
            // }
        }
    }
    public doRectangle(viewportId: number, objectProperties: IObjectPropertiesBase) {
        const viewportData: ViewportData = MapCoreData.findViewport(viewportId)
        if (viewportData) {
            let activeOverlay = this.findActiveOverlayByMcViewport(viewportData.viewport)
            if (activeOverlay != null) {
                let locationPoints: MapCore.SMcVector3D[] = [];
                let ObjSchemeItem: MapCore.IMcRectangleItem = null;
                runMapCoreSafely(() => {
                    ObjSchemeItem = MapCore.IMcRectangleItem.Create(objectProperties.ItemSubTypeFlags,
                        objectProperties.RectangleCoordSys,
                        objectProperties.RectangleType,
                        objectProperties.RectangleDefinition,
                        1,
                        1,
                        objectProperties.LineStyle,
                        objectProperties.LineColor,
                        objectProperties.LineWidth,
                        objectProperties.LineTexture,
                        objectProperties.LineTextureHeightRange,
                        2,
                        objectProperties.FillStyle,
                        objectProperties.FillColor,
                        objectProperties.FillTexture,
                        new MapCore.SMcFVector2D(1, 1)
                    );
                }, "objectWorldService.doRectangle => IMcRectangleItem.Create", true);
                let obj: MapCore.IMcObject = null;
                runMapCoreSafely(() => {
                    obj = MapCore.IMcObject.Create(activeOverlay,
                        ObjSchemeItem,
                        objectProperties.LocationCoordSys,
                        locationPoints,
                        objectProperties.LocationRelativeToDtm);
                }, "objectWorldService.doRectangle => IMcObject.Create", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetNumSmoothingLevels(objectProperties.LineBasedSmoothingLevels); }, "objectWorldService.doRectangle => IMcLineBasedItem.SetNumSmoothingLevels", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetGreatCirclePrecision(objectProperties.LineBasedGreatCirclePrecision); }, "objectWorldService.doRectangle => IMcLineBasedItem.SetGreatCirclePrecision", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetShapeType(objectProperties.ShapeType); }, "objectWorldService.doRectangle => IMcLineBasedItem.SetShapeType", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetVerticalHeight(objectProperties.VerticalHeight); }, "objectWorldService.doRectangle => IMcLineBasedItem.SetVerticalHeight", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetSidesFillStyle(objectProperties.SidesFillStyle); }, "objectWorldService.doRectangle => IMcLineBasedItem.SetSidesFillStyle", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetSidesFillColor(objectProperties.SidesFillColor); }, "objectWorldService.doRectangle => IMcLineBasedItem.SetSidesFillColor", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetSidesFillTexture(objectProperties.SidesFillTexture); }, "objectWorldService.doRectangle => IMcLineBasedItem.SetSidesFillTexture", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetDrawPriorityGroup(objectProperties.DrawPriorityGroup); }, "objectWorldService.doRectangle => IMcSymbolicItem.SetDrawPriorityGroup", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetOutlineColor(objectProperties.LineOutlineColor); }, "objectWorldService.doRectangle => IMcLineBasedItem.SetOutlineColor", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetOutlineWidth(objectProperties.LineOutlineWidth); }, "objectWorldService.doRectangle => IMcLineBasedItem.SetOutlineWidth", true);
                runMapCoreSafely(() => { ObjSchemeItem.SetPointOrderReverseMode(objectProperties.PointOrderReverseMode); }, "objectWorldService.doRectangle => IMcLineBasedItem.SetPointOrderReverseMode", true);

                if (objectProperties.ImageCalc != null)
                    runMapCoreSafely(() => { obj.SetImageCalc(objectProperties.ImageCalc); }, "objectWorldService.doRectangle => IMcObject.SetImageCalc", true);
                runMapCoreSafely(() => { viewportData.editMode.StartInitObject(obj, ObjSchemeItem); }, "objectWorldService.doRectangle => IMcEditMode.StartInitObject", true);
            }
            else
                alert("There is no active overlay");
        }
    }
    // public async doPicture(viewportId: number, objectProperties: IObjectPropertiesBase, textureProperties: texturePropertiesBase) {
    //     const viewportData: ViewportData = MapCoreData.findViewport(viewportId)
    //     if (viewportData) {
    //         let activeOverlay = this.findActiveOverlay(viewportData)
    //         if (activeOverlay != null) {
    //             const locationPoints: MapCore.SMcVector3D[] = [];
    //             let texture = await textureService.createTexture(textureProperties)
    //             let ObjSchemeItem: MapCore.IMcPictureItem = null;
    //             runMapCoreSafely(() => {
    //                 ObjSchemeItem = MapCore.IMcPictureItem.Create(objectProperties.ItemSubTypeFlags,
    //                     objectProperties.PictureCoordSys
    //                     , texture,
    //                     objectProperties.PicWidth,
    //                     objectProperties.PicHeight,
    //                     objectProperties.PictureIsSizeFactor,
    //                     new MapCore.SMcBColor(255, 255, 255, 255),
    //                     MapCore.IMcSymbolicItem.EBoundingRectanglePoint.EBRP_CENTER,
    //                     objectProperties.PictureIsUseTextureGeoReferencing
    //                 );
    //             }, "objectWorldService.doPicture => IMcPictureItem.Create", true);

    //             let obj: MapCore.IMcObject = null;
    //             runMapCoreSafely(() => {
    //                 obj = MapCore.IMcObject.Create(activeOverlay,
    //                     ObjSchemeItem,
    //                     objectProperties.LocationCoordSys,
    //                     locationPoints,
    //                     objectProperties.LocationRelativeToDtm
    //                 );
    //             }, "objectWorldService.doPicture => IMcObject.Create", true);

    //             runMapCoreSafely(() => { ObjSchemeItem.SetDrawPriorityGroup(objectProperties.DrawPriorityGroup); }, "objectWorldService.doPicture => IMcSymbolicItem.SetDrawPriorityGroup", true);
    //             runMapCoreSafely(() => { ObjSchemeItem.SetNeverUpsideDown(objectProperties.PictureNeverUpsideDown); }, "objectWorldService.doPicture => IMcPictureItem.SetNeverUpsideDown", true);

    //             if (objectProperties.ImageCalc != null)
    //                 runMapCoreSafely(() => { obj.SetImageCalc(objectProperties.ImageCalc); }, "objectWorldService.doPicture => IMcObject.SetImageCalc", true);

    //             if (objectProperties.PictureIsUseTextureGeoReferencing == false)
    //                 runMapCoreSafely(() => { viewportData.editMode.StartInitObject(obj, ObjSchemeItem); }, "objectWorldService.doPicture => IMcEditMode.StartInitObject", true);
    //         }
    //         else
    //             alert("There is no active overlay");
    //     }
    // }
    //#endregion

    public doEdit(viewportData: ViewportData, mcObject: MapCore.IMcObject, mcItem: MapCore.IMcObjectSchemeItem) {
        if (viewportData) {
            runMapCoreSafely(() => {
                viewportData.editMode.StartEditObject(mcObject, mcItem);
            }, "objectWorldService.doEdit => IMcEditMode.StartEditObject", true);
        }
    }
    public doInit(viewportData: ViewportData, mcObject: MapCore.IMcObject, mcItem: MapCore.IMcObjectSchemeItem) {
        if (viewportData) {
            runMapCoreSafely(() => {
                viewportData.editMode.StartInitObject(mcObject, mcItem);
            }, "objectWorldService.doInit => IMcEditMode.StartInitObject", true);
        }
    }

    public findActiveOverlayByOM(overlayManager: MapCore.IMcOverlayManager) {
        let activeOverlay = null;
        for (let index = 0; index < MapCoreData.overlayManagerArr.length; index++) {
            if (MapCoreData.overlayManagerArr[index].overlayManager == overlayManager)
                activeOverlay = MapCoreData.overlayManagerArr[index].overlayActive;
        }
        return activeOverlay;
    }
    public findActiveOverlayByMcViewport(mcViewport: MapCore.IMcMapViewport) {
        if (mcViewport) {
            let overlayManager = mcViewport.GetOverlayManager();
            let activeOverlay = this.findActiveOverlayByOM(overlayManager);
            return activeOverlay;
        }
        return null;
    }
    public setActiveOverlayInOverlayManager(overlaManager: MapCore.IMcOverlayManager, overlay: MapCore.IMcOverlay) {
        let currentOmIndex = mapcoreData.overlayManagerArr.findIndex(omObj => omObj.overlayManager == overlaManager);
        if (currentOmIndex != -1) {
            mapcoreData.overlayManagerArr[currentOmIndex].overlayActive = overlay;
        }
    }

    //#region objectWorldTreeService
    public findNodeByChosenScreenPoint = (point: MapCore.SMcVector3D, activeViewport: MapCore.IMcMapViewport, desiredNodeType: string) => {
        if (desiredNodeType == 'Overlay Manager') {
            let overlayManager = activeViewport.GetOverlayManager();
            return overlayManager;
        }
        else {
            let scanGeometry = new MapCore.SMcScanPointGeometry(MapCore.EMcPointCoordSystem.EPCS_SCREEN, point, 0);
            let queryParams = new MapCore.IMcSpatialQueries.SQueryParams();
            queryParams.eTerrainPrecision = MapCore.IMcSpatialQueries.EQueryPrecision.EQP_DEFAULT;
            queryParams.uItemTypeFlagsBitField = 0;

            let eIntersectionTargetTypeList = getEnumDetailsList(MapCore.IMcSpatialQueries.EIntersectionTargetType);
            let enumsArr = [getEnumValueDetails(MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_OVERLAY_MANAGER_OBJECT, eIntersectionTargetTypeList),
            getEnumValueDetails(MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_VISIBLE_VECTOR_LAYER, eIntersectionTargetTypeList),
            getEnumValueDetails(MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_STATIC_OBJECTS_LAYER, eIntersectionTargetTypeList),
            ];
            queryParams.uTargetsBitMask = getBitFieldByEnumArr(enumsArr);

            let targetFound = activeViewport.ScanInGeometry(scanGeometry, false, queryParams);
            if (targetFound.length > 0) {
                let foundedObject = targetFound[0].ObjectItemData?.pObject;
                let foundedItem = targetFound[0].ObjectItemData?.pItem;
                if (desiredNodeType == 'Item' && foundedItem) {
                    return foundedItem;
                }
                else if (desiredNodeType == 'Object' && foundedObject) {
                    return foundedObject;
                }
                else if (desiredNodeType == 'ObjectWithItem') {
                    return { object: foundedObject, item: foundedItem };
                }
            }
        }
    }
    public showNodePoints = (mcCurrentOverlayManager: MapCore.IMcOverlayManager, pointsAndCoordSystemArr: { coordinateSystem: MapCore.EMcPointCoordSystem, points: MapCore.SMcVector3D[] }[]) => {
        //create texture
        let texture: MapCore.IMcTexture, overlay: MapCore.IMcOverlay;
        let filesource = new MapCore.SMcFileSource("http:TesterVertex.png", false);
        runMapCoreSafely(() => { texture = MapCore.IMcImageFileTexture.Create(filesource, false, false, new MapCore.SMcBColor(0, 0, 0, 255)); }, "objectWorldService.showNodePoints => IMcImageFileTexture.Create", true);
        // preparings for create pictureItem
        let EItemSubTypeFlagsArr = getEnumDetailsList(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags);
        let screenEnum = getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN, EItemSubTypeFlagsArr);
        let EPredefinedPropertyIDsArr = getEnumDetailsList(MapCore.IMcProperty.EPredefinedPropertyIDs);
        let sharedPropertyEnum = getEnumValueDetails(MapCore.IMcProperty.EPredefinedPropertyIDs.EPPI_SHARED_PROPERTY_ID, EPredefinedPropertyIDsArr);
        //create overlay
        runMapCoreSafely(() => { overlay = MapCore.IMcOverlay.Create(mcCurrentOverlayManager); }, "objectWorldService.showNodePoints => IIMcOverlay.Create", true);
        //declares
        let objectScheme: MapCore.IMcObjectScheme = null;
        let object: MapCore.IMcObject = null;
        let insertAtIndex = 1;
        for (let i = 0; i < pointsAndCoordSystemArr.length; i++) {
            let pObjectLocation: { Value?: MapCore.IMcObjectLocation } = {};
            let pLocationIndex: { Value?: number } = {};
            let pictureItem: MapCore.IMcPictureItem
            runMapCoreSafely(() => { pictureItem = MapCore.IMcPictureItem.Create(screenEnum.code, pointsAndCoordSystemArr[0].coordinateSystem, texture); }, "objectWorldService.showNodePoints => IMcPictureItem.Create", true);
            runMapCoreSafely(() => { pictureItem.SetDrawPriorityGroup(MapCore.IMcSymbolicItem.EDrawPriorityGroup.EDPG_TOP_MOST, sharedPropertyEnum.code, 0); }, "objectWorldService.showNodePoints => IMcSymbolicItem.SetDrawPriorityGroup", true);
            if (objectScheme == null) {//first iteration
                runMapCoreSafely(() => { objectScheme = MapCore.IMcObjectScheme.Create(pObjectLocation, mcCurrentOverlayManager, pointsAndCoordSystemArr[i].coordinateSystem); }, "objectWorldService.showNodePoints => IMcObjectScheme.Create", true);
                runMapCoreSafely(() => { object = MapCore.IMcObject.Create(overlay, objectScheme, pointsAndCoordSystemArr[i].points); }, "objectWorldService.showNodePoints => IMcObject.Create", true);
                runMapCoreSafely(() => { pictureItem.Connect(pObjectLocation.Value); }, "objectWorldService.showNodePoints => IMcSymbolicItem.Connect", true);
            }
            else {//other iterations
                let objectLocation: MapCore.IMcObjectLocation
                runMapCoreSafely(() => { objectLocation = objectScheme.AddObjectLocation(pointsAndCoordSystemArr[i].coordinateSystem, false, pLocationIndex, insertAtIndex); }, "objectWorldService.showNodePoints => IMcObjectScheme.AddObjectLocation", true);
                runMapCoreSafely(() => { object.SetLocationPoints(pointsAndCoordSystemArr[i].points, pLocationIndex.Value); }, "objectWorldService.showNodePoints => IMcObject.SetLocationPoints", true);
                runMapCoreSafely(() => { pictureItem.Connect(objectLocation); }, "objectWorldService.showNodePoints => IMcSymbolicItem.Connect", true);
            }
        }
        runMapCoreSafely(() => { object.SetDetectibility(false); }, "objectWorldService.showNodePoints => IMcObject.SetDetectibility", true);
        return { overlay: overlay, object: object }
        // object.SetImageCalc(mcObject.GetImageCalc());
        //add the scheme of the points object in the tester (ask Orit if to do so)- sm
    }
    //#endregion

}
export default new ObjectWorldService();




