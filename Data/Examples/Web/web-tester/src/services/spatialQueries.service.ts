import _ from 'lodash';

import { getEnumDetailsList, getEnumValueDetails, MapCoreData, ObjectWorldService, ViewportData } from 'mapcore-lib';
import generalService, { getState } from "./general.service";
import { runAsyncCodeSafely, runCodeSafely, runMapCoreSafely } from "../common/services/error-handling/errorHandler";
import { SpatialQueryName } from "../components/dialog/treeView/mapWorldTree/viewportForms/spatialQueries/spatialQueriesFooter";
import { setSpatialQueriesResultsObjects } from "../redux/MapWorldTree/mapWorldTreeActions";
import store from "../redux/store";
import { Id } from "../tools/enum/enumsOfScheme";
import { createItemAndObjByType, ObjectTypeEnum } from "../components/dialog/shared/drawObjectCtrl";
import { AreaOfSightOptionResultObjects, AreaOfSightOptionsEnum, AreaOfSightResult } from "../components/dialog/treeView/mapWorldTree/viewportForms/spatialQueries/areaOfSight/structs";

class SpatialQueriesService {

    //#region Help Functions
    private getActiveOverlay(mcCurrentSpatialQueries: MapCore.IMcSpatialQueries) {
        let activeOverlay = null;
        let activeCard = store.getState().mapWindowReducer.activeCard;
        let activeViewport = MapCoreData.findViewport(activeCard);
        runCodeSafely(() => {
            if (mcCurrentSpatialQueries.GetInterfaceType() == MapCore.IMcMapViewport.INTERFACE_TYPE) {
                let mcCurrentViewport = mcCurrentSpatialQueries as MapCore.IMcMapViewport;
                activeOverlay = ObjectWorldService.findActiveOverlayByMcViewport(mcCurrentViewport);
            }
            else if (activeViewport) {
                activeOverlay = ObjectWorldService.findActiveOverlayByMcViewport(activeViewport.viewport);
            }
        }, 'SpatialQueriesForm/General.getActiveOverlay');
        return activeOverlay;
    }
    private getMcCurrentViewport(mcCurrentSpatialQueries: MapCore.IMcSpatialQueries) {
        let mcCurrentViewport: MapCore.IMcMapViewport = null;
        let activeCard = store.getState().mapWindowReducer.activeCard;
        runCodeSafely(() => {
            if (mcCurrentSpatialQueries.GetInterfaceType() == MapCore.IMcMapViewport.INTERFACE_TYPE) {
                mcCurrentViewport = mcCurrentSpatialQueries as MapCore.IMcMapViewport;
            }
            else if (activeCard) {
                mcCurrentViewport = MapCoreData.findViewport(activeCard)?.viewport;
            }
        }, 'SpatialQueriesForm/General.getMcCurrentViewport');
        return mcCurrentViewport;
    }
    private createGridConverterOMToVP(mcCurrentSpatialQueries: MapCore.IMcSpatialQueries) {
        let gridConverterOMToVP = null;
        runCodeSafely(() => {
            let activeOverlay = this.getActiveOverlay(mcCurrentSpatialQueries);
            if (activeOverlay) {
                let overlayManagerCoordSys = activeOverlay.GetOverlayManager().GetCoordinateSystemDefinition();
                let spatialQueriesCoordSys = mcCurrentSpatialQueries.GetCoordinateSystem();
                runMapCoreSafely(() => {
                    gridConverterOMToVP = MapCore.IMcGridConverter.Create(overlayManagerCoordSys, spatialQueriesCoordSys, false);
                }, 'SpatialQueriesService.createGridConverterOMToVP => IMcGridConverter.Create', true)
            }
        }, 'SpatialQueriesService.createGridConverterOMToVP')
        return gridConverterOMToVP;
    }
    private getDefaultFont = async () => {
        let defaultFont: MapCore.IMcFont = null;
        await runAsyncCodeSafely(async () => {
            let fileSource = await generalService.getFileSourceByUrl('http:arial.ttf');
            if (generalService.ObjectProperties.TextFont) {
                defaultFont = generalService.ObjectProperties.TextFont
            }
            else {
                runMapCoreSafely(() => {
                    defaultFont = MapCore.IMcFileFont.Create(fileSource, 15);
                }, 'SpatialQueriesService.getDefaultFont => IMcFileFont.Create', true)
            }
        }, 'SpatialQueriesForm/Heights.getDefaultFont')
        return defaultFont;
    }
    private createTextItem = async () => {
        let textItem: MapCore.IMcTextItem = null;
        await runAsyncCodeSafely(async () => {
            let EItemSubTypeFlags = getEnumDetailsList(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags);
            let subItemsFlag = getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN, EItemSubTypeFlags).code;
            let defaultFont = await this.getDefaultFont();
            runMapCoreSafely(() => {
                textItem = MapCore.IMcTextItem.Create(subItemsFlag, MapCore.EMcPointCoordSystem.EPCS_SCREEN, defaultFont);
            }, 'SpatialQueriesService.createTextItem => IMcTextItem.Create', true)
        }, 'SpatialQueriesService.createTextItem')
        return textItem;
    }
    private createHeightsTextItemScheme = async (activeOverlay: MapCore.IMcOverlay) => {
        let scheme: MapCore.IMcObjectScheme = null;
        await runAsyncCodeSafely(async () => {
            let textItem = await this.createTextItem();
            runMapCoreSafely(() => {
                scheme = MapCore.IMcObjectScheme.Create(activeOverlay.GetOverlayManager(), textItem, MapCore.EMcPointCoordSystem.EPCS_WORLD, false);
            }, 'SpatialQueriesService.createHeightsTextItemScheme => IMcObjectScheme.Create', true)
            runMapCoreSafely(() => { textItem.SetText(new MapCore.SMcVariantString("text", true), Id.Item_Text_Text) }, 'SpatialQueriesService.createHeightsTextItemScheme => IMcSymbolicItem.SetText', true)
            runMapCoreSafely(() => { textItem.SetDrawPriorityGroup(MapCore.IMcSymbolicItem.EDrawPriorityGroup.EDPG_TOP_MOST) }, 'SpatialQueriesService.createHeightsTextItemScheme => IMcSymbolicItem.SetDrawPriorityGroup', true)

        }, 'SpatialQueriesService.createHeightsTextItemScheme')
        return scheme;
    }
    private createSightTextObject = async (activeOverlay: MapCore.IMcOverlay, visibilityLocation: { locationPoint: any, text: string, isRelative: boolean; }) => {
        let object: MapCore.IMcObject = null;
        await runAsyncCodeSafely(async () => {
            let textItem = await this.createTextItem();
            let scheme: MapCore.IMcObjectScheme = null;
            runMapCoreSafely(() => {
                scheme = MapCore.IMcObjectScheme.Create(activeOverlay.GetOverlayManager(), textItem, MapCore.EMcPointCoordSystem.EPCS_WORLD, visibilityLocation.isRelative);
            }, 'SpatialQueriesService.createSightTextObject => IMcObjectScheme.Create', true)
            runMapCoreSafely(() => { textItem.SetText(new MapCore.SMcVariantString(visibilityLocation.text, true)) }, 'SpatialQueriesService.createSightTextObject => IMcTextItem.SetText', true)//, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
            runMapCoreSafely(() => { textItem.SetOutlineColor(MapCore.bcWhiteOpaque) }, 'SpatialQueriesService.createSightTextObject => IMcTextItem.SetOutlineColor', true)//, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
            runMapCoreSafely(() => { textItem.SetDrawPriorityGroup(MapCore.IMcSymbolicItem.EDrawPriorityGroup.EDPG_TOP_MOST) }, 'SpatialQueriesService.createSightTextObject => IMcTextItem.SetDrawPriorityGroup', true)//, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
            runMapCoreSafely(() => {
                object = MapCore.IMcObject.Create(activeOverlay, scheme, [visibilityLocation.locationPoint]);
            }, 'SpatialQueriesService.createSightTextObject => IMcObject.Create', true)
        }, 'SpatialQueriesService.createSightTextObject')
        return object;
    }
    private drawText = (activeOverlay: MapCore.IMcOverlay, scheme: MapCore.IMcObjectScheme, point: MapCore.SMcVector3D, textStr: string) => {
        let textObj: MapCore.IMcObject = null;
        runCodeSafely(() => {
            runMapCoreSafely(() => {
                textObj = MapCore.IMcObject.Create(activeOverlay, scheme, [point]);
            }, 'SpatialQueriesService.drawText => IMcObject.Create', true)
            let variantStr = new MapCore.SMcVariantString(textStr, true)
            runMapCoreSafely(() => {
                textObj.SetStringProperty(Id.Item_Text_Text, variantStr);
            }, 'SpatialQueriesService.drawText => IMcObject.SetStringProperty', true)
        }, 'SpatialQueriesService.drawText')
        return textObj;
    }
    private createTesterVertexObject = (activeOverlay: MapCore.IMcOverlay, pointToDraw: MapCore.SMcVector3D) => {
        let testerVertexObj: MapCore.IMcObject = null;
        runCodeSafely(() => {
            let texture: MapCore.IMcImageFileTexture;
            let pictureItem: MapCore.IMcPictureItem;
            let fileSource = new MapCore.SMcFileSource('http:TesterVertex.png', false);
            runMapCoreSafely(() => {
                texture = MapCore.IMcImageFileTexture.Create(fileSource, false, false, new MapCore.SMcBColor(0, 0, 0, 255));
            }, "SpatialQueriesService.createTesterVertexObject => IMcImageFileTexture.Create", true);
            let EItemSubTypeFlagsArr = getEnumDetailsList(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags);
            let screenEnum = getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN, EItemSubTypeFlagsArr);
            runMapCoreSafely(() => { pictureItem = MapCore.IMcPictureItem.Create(screenEnum.code, MapCore.EMcPointCoordSystem.EPCS_SCREEN, texture); }, "SpatialQueriesService.createTesterVertexObject => IMcPictureItem.Create", true);
            runMapCoreSafely(() => { pictureItem.SetDrawPriorityGroup(MapCore.IMcSymbolicItem.EDrawPriorityGroup.EDPG_TOP_MOST); }, "SpatialQueriesService.createTesterVertexObject => IMcSymbolicItem.SetDrawPriorityGroup", true);
            runMapCoreSafely(() => {
                testerVertexObj = MapCore.IMcObject.Create(activeOverlay, pictureItem, MapCore.EMcPointCoordSystem.EPCS_WORLD, [pointToDraw], false)
            }, "SpatialQueriesService.createTesterVertexObject => IMcObject.Create", true);
        }, 'SpatialQueriesService.createTesterVertexObject')
        return testerVertexObj;
    }
    private numberOfSetBits = (i) => {
        i = i - ((i >> 1) & 0x55555555);
        i = (i & 0x33333333) + ((i >> 2) & 0x33333333);
        i = (i + (i >> 4)) & 0x0F0F0F0F;
        return (i * 0x01010101) >>> 24;
    }
    private getIAreaOfSightTextureBuffer = (areaOfSight: MapCore.IMcSpatialQueries.IAreaOfSight, bFillPointsVisibility: boolean, sumType?: MapCore.IMcSpatialQueries.EScoutersSumType, maxNumOfScouters?: number) => {
        let textureBuffer: Uint8Array;
        runCodeSafely(() => {
            let aOSMatrix = areaOfSight.GetAreaOfSightMatrix(bFillPointsVisibility);
            let pointVisibilityColors = aOSMatrix.aPointsVisibilityColors;
            textureBuffer = new Uint8Array(pointVisibilityColors.length * 4);
            let index = 0;

            pointVisibilityColors.forEach(rgba => {
                let localRgba = _.cloneDeep(rgba);
                let dwRGBA = localRgba.r | (localRgba.g << 8) | (localRgba.b << 16) | (localRgba.a << 24);
                switch (sumType) {
                    case MapCore.IMcSpatialQueries.EScoutersSumType.ESST_ADD:
                        localRgba.r = 0;
                        localRgba.g = Math.min(255, Math.floor((dwRGBA * 255) / maxNumOfScouters));
                        localRgba.b = 0;
                        localRgba.a = 192;
                        break;
                    case MapCore.IMcSpatialQueries.EScoutersSumType.ESST_ALL:
                        let numScoutersAll = this.numberOfSetBits(dwRGBA);
                        let newColor = Math.min(255, Math.floor((numScoutersAll * 255) / maxNumOfScouters));
                        localRgba.r = 0;
                        localRgba.g = newColor;
                        localRgba.b = newColor;
                        localRgba.a = 192;
                        break;
                    case MapCore.IMcSpatialQueries.EScoutersSumType.ESST_OR:
                        localRgba.r = 0;
                        localRgba.g = Math.min(255, dwRGBA & 0xff) * 255;
                        localRgba.b = 0;
                        localRgba.a = 192;
                        break;

                    default:
                        break;
                }
                textureBuffer[index++] = localRgba.r;
                textureBuffer[index++] = localRgba.g;
                textureBuffer[index++] = localRgba.b;
                textureBuffer[index++] = localRgba.a;
            });
        }, 'SpatialQueriesService.getIAreaOfSightTextureBuffer')
        return textureBuffer;
    }
    private drawLinesByVisible = (activeOverlay: MapCore.IMcOverlay, mcCurrentSpatialQueries: MapCore.IMcMapViewport, mcLineOfSightPoints: MapCore.IMcSpatialQueries.SLineOfSightPoint[], colors: { visibleLine: MapCore.SMcBColor, nonVisibleLine: MapCore.SMcBColor }) => {
        let objectsList: MapCore.IMcObject[] = [];
        runCodeSafely(() => {
            let lines = { visibleLine: null, nonVisibleLine: null };
            let EItemSubTypeFlags = getEnumDetailsList(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags);
            let subItemsFlag = getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN, EItemSubTypeFlags).code;
            Object.keys(lines).forEach((lineKey) => {
                let lineItem: MapCore.IMcLineItem = null;
                runMapCoreSafely(() => {
                    lineItem = MapCore.IMcLineItem.Create(subItemsFlag, MapCore.IMcLineBasedItem.ELineStyle.ELS_SOLID, colors[lineKey], 3);
                }, 'SpatialQueriesService.drawLinesByVisible => IMcLineItem.Create', true)

                runMapCoreSafely(() => {
                    lines[lineKey] = MapCore.IMcObjectScheme.Create(activeOverlay.GetOverlayManager(), lineItem, MapCore.EMcPointCoordSystem.EPCS_WORLD, false);
                }, 'SpatialQueriesService.drawLinesByVisible => IMcObjectScheme.Create', true)

                runMapCoreSafely(() => { lineItem.SetDrawPriorityGroup(MapCore.IMcSymbolicItem.EDrawPriorityGroup.EDPG_TOP_MOST); }, 'SpatialQueriesService.drawLinesByVisible => IMcLineItem.SetDrawPriorityGroup', true)
            })
            //create line objects between every two points
            for (let i = 1; i < mcLineOfSightPoints.length; i++) {
                let pointA = this.convertPointFromVPtoOM(mcLineOfSightPoints[i - 1].Point, mcCurrentSpatialQueries);
                let pointB = this.convertPointFromVPtoOM(mcLineOfSightPoints[i].Point, mcCurrentSpatialQueries);
                let locationPoints = [pointA, pointB];
                let scheme = mcLineOfSightPoints[i - 1].bVisible ? lines['visibleLine'] : lines['nonVisibleLine'];
                let obj: MapCore.IMcObject = null;
                runMapCoreSafely(() => { obj = MapCore.IMcObject.Create(activeOverlay, scheme, locationPoints); }, 'SpatialQueriesService.drawLinesByVisible => IMcObject.Create', true)
                objectsList = [...objectsList, obj];
            }
        }, 'SpatialQueriesService.drawLinesByVisible')
        return objectsList;
    }
    private drawCrossedEllipse = (activeOverlay: MapCore.IMcOverlay, pointToDraw: MapCore.SMcVector3D, radius: number, color: MapCore.SMcBColor) => {
        let crossedEllipseObj: MapCore.IMcObject = null;
        runCodeSafely(() => {
            let EItemSubTypeFlags = getEnumDetailsList(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags);
            let subItemsFlag = getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN, EItemSubTypeFlags).code |
                getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_ATTACHED_TO_TERRAIN, EItemSubTypeFlags).code;
            let objSchemeItem: MapCore.IMcEllipseItem = null;
            runMapCoreSafely(() => {
                objSchemeItem = MapCore.IMcEllipseItem.Create(subItemsFlag, MapCore.EMcPointCoordSystem.EPCS_WORLD,
                    MapCore.IMcObjectSchemeItem.EGeometryType.EGT_GEOGRAPHIC, radius, radius, 0, 360, 0, MapCore.IMcLineBasedItem.ELineStyle.ELS_SOLID,
                    color, 2, null, new MapCore.SMcFVector2D(1, 1), 2, MapCore.IMcLineBasedItem.EFillStyle.EFS_CROSS, color);
            }, "SpatialQueriesService.drawCrossedEllipse => IMcEllipseItem.Create", true);

            runMapCoreSafely(() => { objSchemeItem.SetDrawPriorityGroup(MapCore.IMcSymbolicItem.EDrawPriorityGroup.EDPG_TOP_MOST); }, "SpatialQueriesService.drawCrossedEllipse => IMcSymbolicItem.SetDrawPriorityGroup", true);

            runMapCoreSafely(() => {
                crossedEllipseObj = MapCore.IMcObject.Create(activeOverlay, objSchemeItem, MapCore.EMcPointCoordSystem.EPCS_WORLD, [pointToDraw], false)
            }, "SpatialQueriesService.drawCrossedEllipse => IMcObject.Create", true);
        }, 'SpatialQueriesService.drawCrossedEllipse');
        return crossedEllipseObj;
    }
    //#endregion
    //#region creatre Objects Private Funcs
    private createTerrainHeightObjs(...args) {
        runCodeSafely(() => {
            let [pointToDraw, mcCurrentSpatialQueries] = args;
            this.removeExistObjects();
            let activeOverlay = this.getActiveOverlay(mcCurrentSpatialQueries);
            if (activeOverlay) {
                let objectsList: MapCore.IMcObject[] = [];
                let obj = this.createTesterVertexObject(activeOverlay, pointToDraw)
                objectsList = [...objectsList, obj];

                store.dispatch(setSpatialQueriesResultsObjects({ queryName: SpatialQueryName.GetTerrainHeight, objects: objectsList, removeObjectsCB: (objects: MapCore.IMcObject[]) => { this.removeObjectsCB(objects) } }));
            }
        }, 'SpatialQueriesService.createTerrainHeightObjs')
    }
    private createHeightAlongLineObjs(...args) {
        runAsyncCodeSafely(async () => {
            let [aPointsWithHeights, afSlopes, mcCurrentSpatialQueries] = args;
            this.removeExistObjects();
            let activeOverlay = this.getActiveOverlay(mcCurrentSpatialQueries);
            if (activeOverlay) {
                let objectsList: MapCore.IMcObject[] = [];
                let EItemSubTypeFlags = getEnumDetailsList(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags);
                let subItemsFlag = getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_ACCURATE_3D_SCREEN_WIDTH, EItemSubTypeFlags).code |
                    getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN, EItemSubTypeFlags).code;
                //create line item
                let lineResultItem: MapCore.IMcLineItem = null;
                runMapCoreSafely(() => {
                    lineResultItem = MapCore.IMcLineItem.Create(subItemsFlag,
                        MapCore.IMcLineBasedItem.ELineStyle.ELS_SOLID, MapCore.bcBlackOpaque,
                        3, null, new MapCore.SMcFVector2D(0, -1), 1);
                }, 'SpatialQueriesService.createHeightAlongLineObjs => IMcLineItem.Create', true)
                let convertedPointsWithHeights = aPointsWithHeights.map(point => this.convertPointFromVPtoOM(point, mcCurrentSpatialQueries));
                //create line object
                let lineResultObj: MapCore.IMcObject = null;
                runMapCoreSafely(() => {
                    lineResultObj = MapCore.IMcObject.Create(activeOverlay,
                        lineResultItem,
                        MapCore.EMcPointCoordSystem.EPCS_WORLD,
                        convertedPointsWithHeights,
                        false);
                }, 'SpatialQueriesService.createHeightAlongLineObjs => IMcObject.Create', true)
                runMapCoreSafely(() => { lineResultItem.SetBlockedTransparency(128) }, 'SpatialQueriesService.createHeightAlongLineObjs => IMcLineBasedItem.SetBlockedTransparency', true)
                runMapCoreSafely(() => { lineResultItem.SetLineColor(new MapCore.SMcBColor(255, 0, 0, 255)) }, 'SpatialQueriesService.createHeightAlongLineObjs => IMcLineBasedItem.SetLineColor', true)
                objectsList = [...objectsList, lineResultObj];
                //create text points objects
                let scheme: MapCore.IMcObjectScheme = await this.createHeightsTextItemScheme(activeOverlay);
                for (let i = 0; i < convertedPointsWithHeights.length; i++) {
                    let slopObj: MapCore.IMcObject = this.drawText(activeOverlay, scheme, convertedPointsWithHeights[i], `${afSlopes[i]}`);
                    objectsList = [...objectsList, slopObj];
                }

                store.dispatch(setSpatialQueriesResultsObjects({ queryName: SpatialQueryName.GetTerrainHeightsAlongLine, objects: objectsList, removeObjectsCB: (objects: MapCore.IMcObject[]) => { this.removeObjectsCB(objects) } }));
            }
        }, 'SpatialQueriesService.createHeightAlongLineObjs')
    }
    private createTerrainHeightMatrixObjs(...args) {
        runAsyncCodeSafely(async () => {
            let [lowerLeftPoint, horizontalResolution, verticalResolution, numHorizontalPoints,
                numVerticalPoints, adHeightMatrix, mcCurrentSpatialQueries] = args;
            this.removeExistObjects();
            let activeOverlay = this.getActiveOverlay(mcCurrentSpatialQueries);
            if (activeOverlay) {
                let objectsList: MapCore.IMcObject[] = [];
                let scheme: MapCore.IMcObjectScheme = await this.createHeightsTextItemScheme(activeOverlay);
                for (let i = 0; i < numVerticalPoints; i++) {
                    let currPoint: MapCore.SMcVector3D = new MapCore.SMcVector3D(MapCore.v3Zero);
                    currPoint.y = lowerLeftPoint.y + (i * verticalResolution);
                    for (let j = 0; j < numHorizontalPoints; j++) {
                        currPoint.z = adHeightMatrix[(i * numHorizontalPoints) + j];
                        currPoint.x = lowerLeftPoint.x + (j * horizontalResolution);
                        let convertedPoint = this.convertPointFromVPtoOM(currPoint, mcCurrentSpatialQueries)
                        let roundedValue = convertedPoint.z?.toFixed(1);
                        let matrixObj: MapCore.IMcObject = this.drawText(activeOverlay, scheme, currPoint, `${roundedValue}`);
                        objectsList = [...objectsList, matrixObj];
                    }
                }
                store.dispatch(setSpatialQueriesResultsObjects({ queryName: SpatialQueryName.GetTerrainHeightMatrix, objects: objectsList, removeObjectsCB: (objects: MapCore.IMcObject[]) => { this.removeObjectsCB(objects) } }));
            }
        }, 'SpatialQueriesService.createTerrainHeightMatrixObjs')
    }
    private createExtremeObjs(...args) {
        runAsyncCodeSafely(async () => {
            let [polygonPoints, isPointsFound, highestPoint, lowestPoint, mcCurrentSpatialQueries] = args;
            this.removeExistObjects();
            let activeOverlay = this.getActiveOverlay(mcCurrentSpatialQueries);
            if (activeOverlay) {
                let objectsList: MapCore.IMcObject[] = [];
                let objAndSchemeObject: { obj: MapCore.IMcObject, objSchemeItem: MapCore.IMcObjectSchemeItem } = createItemAndObjByType(activeOverlay, polygonPoints, ObjectTypeEnum.polygon);
                objectsList = [...objectsList, objAndSchemeObject.obj];
                if (isPointsFound) {
                    let highest = this.getMcCurrentViewport(mcCurrentSpatialQueries).ViewportToOverlayManagerWorld(highestPoint);
                    let lowest = this.getMcCurrentViewport(mcCurrentSpatialQueries).ViewportToOverlayManagerWorld(lowestPoint);
                    let extremePointsArr = [highest, lowest];
                    //Placing flag in the founded points
                    let defaultFont = await this.getDefaultFont();
                    let EItemSubTypeFlags = getEnumDetailsList(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags);
                    let subItemsFlag = getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN, EItemSubTypeFlags).code;
                    for (let i = 0; i < extremePointsArr.length; i++) {
                        let extremeLocationPoint: MapCore.SMcVector3D[] = [new MapCore.SMcVector3D(MapCore.v3Zero), new MapCore.SMcVector3D(MapCore.v3Zero)];
                        extremeLocationPoint[0] = extremePointsArr[i];
                        extremeLocationPoint[1].x = extremePointsArr[i].x;
                        extremeLocationPoint[1].y = extremePointsArr[i].y + 1;
                        extremeLocationPoint[1].z = extremePointsArr[i].z + 30;

                        let lineItem: MapCore.IMcObjectSchemeItem = MapCore.IMcLineItem.Create(subItemsFlag,
                            MapCore.IMcLineBasedItem.ELineStyle.ELS_SOLID, new MapCore.SMcBColor(255, 0, 0, 255),
                            3, null, new MapCore.SMcFVector2D(0, -1), 1);

                        let objLineItem = MapCore.IMcObject.Create(activeOverlay, lineItem,
                            MapCore.EMcPointCoordSystem.EPCS_WORLD, extremeLocationPoint, false);

                        objectsList = [...objectsList, objLineItem];

                        let ObjTextItem = MapCore.IMcTextItem.Create(subItemsFlag, MapCore.EMcPointCoordSystem.EPCS_SCREEN, defaultFont,
                            new MapCore.SMcFVector2D(1, 1), MapCore.IMcTextItem.ENeverUpsideDownMode.ENUDM_NONE, MapCore.EAxisXAlignment.EXA_CENTER,
                            MapCore.IMcSymbolicItem.EBoundingRectanglePoint.EBRP_CENTER, true, 0, MapCore.bcBlackOpaque, new MapCore.SMcBColor(0, 128, 0, 255));

                        ObjTextItem.SetText(new MapCore.SMcVariantString("Highest Pt", true))//, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ObjTextItem.Connect(lineItem);
                        ObjTextItem.SetAttachPointType(0, MapCore.IMcSymbolicItem.EAttachPointType.EAPT_INDEX_POINTS)//,MapCore.IMcProperty.EPredefinedPropertyIDs.EPPI_SHARED_PROPERTY_ID);
                        ObjTextItem.SetAttachPointIndex(0, 1)//, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    }
                }
                store.dispatch(setSpatialQueriesResultsObjects({ queryName: SpatialQueryName.GetExtremeHeightPointsInPolygon, objects: objectsList, removeObjectsCB: (objects: MapCore.IMcObject[]) => { this.removeObjectsCB(objects) } }));
            }
        }, 'SpatialQueriesService.createExtremeObjs')
    }
    private createRayIntersectionObjs(...args) {
        runCodeSafely(() => {
            let [rayOrigin, rayDirection, maxDistance, mcCurrentSpatialQueries] = args;
            this.removeExistObjects();
            let activeOverlay = this.getActiveOverlay(mcCurrentSpatialQueries);
            if (activeOverlay) {
                let objectsList: MapCore.IMcObject[] = [];
                let locationB = MapCore.SMcVector3D.Plus(MapCore.SMcVector3D.Mul(rayDirection, maxDistance), rayOrigin);
                let locationPoints = [rayOrigin, locationB];
                let EItemSubTypeFlags = getEnumDetailsList(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags);
                let subItemsFlag = getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN, EItemSubTypeFlags).code;
                //create line item
                let lineResultItem: MapCore.IMcLineItem = null;
                runMapCoreSafely(() => {
                    lineResultItem = MapCore.IMcLineItem.Create(subItemsFlag,
                        MapCore.IMcLineBasedItem.ELineStyle.ELS_SOLID, new MapCore.SMcBColor(255, 0, 0, 255), 3);
                }, 'SpatialQueriesService.createRayIntersectionObjs => IMcLineItem.Create', true)
                //create line object
                let lineResultObj: MapCore.IMcObject = null;
                runMapCoreSafely(() => {
                    lineResultObj = MapCore.IMcObject.Create(activeOverlay,
                        lineResultItem,
                        MapCore.EMcPointCoordSystem.EPCS_WORLD,
                        locationPoints,
                        false);
                }, 'SpatialQueriesService.createRayIntersectionObjs => IMcObject.Create', true)
                runMapCoreSafely(() => { lineResultItem.SetDrawPriorityGroup(generalService.ObjectProperties.DrawPriorityGroup) }, 'SpatialQueriesService.createHeightAlongLineObjs => IMcLineBasedItem.SetDrawPriorityGroup', true)
                objectsList = [...objectsList, lineResultObj];

                store.dispatch(setSpatialQueriesResultsObjects({ queryName: SpatialQueryName.GetRayIntersection, objects: objectsList, removeObjectsCB: (objects: MapCore.IMcObject[]) => { this.removeObjectsCB(objects) } }));
            }
        }, 'SpatialQueriesService.createRayIntersectionObjs')
    }
    private createPointVisibilityObjs(...args) {
        runAsyncCodeSafely(async () => {
            let [scouter, isScouter, target, isTarget, mcCurrentSpatialQueries] = args;
            this.removeExistObjects();
            let activeOverlay = this.getActiveOverlay(mcCurrentSpatialQueries);
            if (activeOverlay) {
                let objectsList: MapCore.IMcObject[] = [];
                let visibilityLocationArr = [
                    { locationPoint: scouter, text: 'S', isRelative: !isScouter },
                    { locationPoint: target, text: 'T', isRelative: !isTarget }
                ];
                for (let visibilityLocation of visibilityLocationArr) {
                    let object = await this.createSightTextObject(activeOverlay, visibilityLocation);
                    objectsList = [...objectsList, object];
                }
                store.dispatch(setSpatialQueriesResultsObjects({ queryName: SpatialQueryName.GetPointVisibility, objects: objectsList, removeObjectsCB: (objects: MapCore.IMcObject[]) => { this.removeObjectsCB(objects) } }));
            }
        }, 'SpatialQueriesService.createPointVisibilityObjs')
    }
    private createLineOfSightObjs(...args) {
        runAsyncCodeSafely(async () => {
            let [scouter, isScouter, target, isTarget, lineOfSightPoints, mcCurrentSpatialQueries] = args;
            this.removeExistObjects();
            let activeOverlay = this.getActiveOverlay(mcCurrentSpatialQueries);
            if (activeOverlay) {
                let objectsList: MapCore.IMcObject[] = [];

                let visibilityLocationArr = [
                    { locationPoint: scouter, text: 'S', isRelative: !isScouter },
                    { locationPoint: target, text: 'T', isRelative: !isTarget }
                ];
                for (let visibilityLocation of visibilityLocationArr) {
                    let object = await this.createSightTextObject(activeOverlay, visibilityLocation);
                    objectsList = [...objectsList, object];
                }
                let colors = { visibleLine: new MapCore.SMcBColor(0, 255, 0, 255), nonVisibleLine: new MapCore.SMcBColor(255, 0, 0, 255) };
                let linesObjectsList = this.drawLinesByVisible(activeOverlay, mcCurrentSpatialQueries, lineOfSightPoints, colors);
                objectsList = [...objectsList, ...linesObjectsList];
                store.dispatch(setSpatialQueriesResultsObjects({ queryName: SpatialQueryName.GetLineOfSight, objects: objectsList, removeObjectsCB: (objects: MapCore.IMcObject[]) => { this.removeObjectsCB(objects) } }));
            }
        }, 'SpatialQueriesService.createLineOfSightObjs')
    }
    private createBestScoutersLocationsObjs(...args) {
        runCodeSafely(() => {
            let [scoutersPoints, scoutersCenterPoint, attributes, mcCurrentSpatialQueries] = args;
            this.removeExistObjects();
            let activeOverlay = this.getActiveOverlay(mcCurrentSpatialQueries);
            if (activeOverlay) {
                let objectsList: MapCore.IMcObject[] = [];
                scoutersPoints.forEach(point => {
                    let obj = this.createTesterVertexObject(activeOverlay, point)
                    objectsList = [...objectsList, obj];
                });
                let objAndSchemeObject: { obj: MapCore.IMcObject, objSchemeItem: MapCore.IMcObjectSchemeItem } = createItemAndObjByType(activeOverlay, scoutersCenterPoint, ObjectTypeEnum.ellipse, attributes);
                objectsList = [...objectsList, objAndSchemeObject.obj];

                store.dispatch(setSpatialQueriesResultsObjects({ queryName: SpatialQueryName.GetBestScoutersLocationsInEllipse, objects: objectsList, removeObjectsCB: (objects: MapCore.IMcObject[]) => { this.removeObjectsCB(objects) } }));
            }
        }, 'SpatialQueriesService.createBestScoutersLocationsObjs')
    }
    private createAreaOfSightForMultipleScoutersObjs(...args) {
        runCodeSafely(() => {
            let [areaOfSight, selectedScoutersSumType, maxNumOfScouters, ellipseArgs, isCreateAutomatic, bFillPointVisibility, mcCurrentSpatialQueries] = args;
            let [sightObjectPoints, attributes] = ellipseArgs;
            this.removeExistObjects();
            let activeOverlay = this.getActiveOverlay(mcCurrentSpatialQueries);
            if (activeOverlay) {
                let objectsList: AreaOfSightOptionResultObjects[] = [];
                let mcAreaOfSight = areaOfSight as MapCore.IMcSpatialQueries.IAreaOfSight;
                let matrixObj = mcAreaOfSight && isCreateAutomatic ? this.createMatrix(mcCurrentSpatialQueries, mcAreaOfSight, bFillPointVisibility, selectedScoutersSumType, maxNumOfScouters) : null;

                let objAndSchemeEllipse: { obj: MapCore.IMcObject, objSchemeItem: MapCore.IMcObjectSchemeItem } = createItemAndObjByType(activeOverlay, sightObjectPoints, ObjectTypeEnum.ellipse, attributes);

                let drawObjOption = new AreaOfSightOptionResultObjects(AreaOfSightOptionsEnum.isDrawObject, [objAndSchemeEllipse.obj], this.setAreaOfSightObjectVisibility, this.removeObjectsCB);
                let areaOfSightOption = new AreaOfSightOptionResultObjects(AreaOfSightOptionsEnum.isAreaOfSight, [matrixObj], this.setAreaOfSightObjectVisibility, this.removeObjectsCB);
                objectsList = [...objectsList, drawObjOption, areaOfSightOption];

                store.dispatch(setSpatialQueriesResultsObjects({ queryName: SpatialQueryName.GetAreaOfSightForMultipleScouters, objects: objectsList, removeObjectsCB: this.removeAreaOfSightOptionResultObjects }));
            }
        }, 'SpatialQueriesService.createAreaOfSightForMultipleScoutersObjs')
    }
    private createSingleAreaOfSightObjs = (...args) => {
        runCodeSafely(() => {
            let [areaOfSightResults, sightObjectsParams, visibilityColors, isCreateAutomatic, bFillPointVisibility, mcCurrentSpatialQueries] = args;
            let localAreaOfSightResults = areaOfSightResults as AreaOfSightResult;
            this.removeExistObjects();
            let activeOverlay = this.getActiveOverlay(mcCurrentSpatialQueries);
            if (activeOverlay) {
                let objectsList: AreaOfSightOptionResultObjects[] = [];
                //AreaOfSight Option
                let matrixObj = localAreaOfSightResults.ppAreaOfSight && isCreateAutomatic ? this.createMatrix(mcCurrentSpatialQueries, localAreaOfSightResults.ppAreaOfSight, bFillPointVisibility) : null;
                let areaOfSightOption = new AreaOfSightOptionResultObjects(AreaOfSightOptionsEnum.isAreaOfSight, [matrixObj], this.setAreaOfSightObjectVisibility, this.removeObjectsCB);
                objectsList = [...objectsList, areaOfSightOption];
                //Seen And Unseen Polygons
                let polygonsObjects = { seenPolygons: [], unseenPolygons: [] };
                let EItemSubTypeFlags = getEnumDetailsList(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags);
                let subItemsFlag = getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN, EItemSubTypeFlags).code;
                Object.keys(polygonsObjects).forEach((polygonType) => {
                    let polygonColor = polygonType == 'seenPolygons' ? visibilityColors.get(MapCore.IMcSpatialQueries.EPointVisibility.EPV_SEEN) : visibilityColors.get(MapCore.IMcSpatialQueries.EPointVisibility.EPV_UNSEEN);
                    let polygonItem: MapCore.IMcPolygonItem = null;
                    let polygonScheme: MapCore.IMcObjectScheme = null;
                    runMapCoreSafely(() => {
                        polygonItem = MapCore.IMcPolygonItem.Create(subItemsFlag, MapCore.IMcLineBasedItem.ELineStyle.ELS_SOLID, polygonColor, 3, null,
                            new MapCore.SMcFVector2D(0, -1), 1, MapCore.IMcLineBasedItem.EFillStyle.EFS_SOLID, polygonColor);
                    }, 'SpatialQueriesService.createSingleAreaOfSightObjs => IMcPolygonItem.Create', true)

                    runMapCoreSafely(() => {
                        polygonScheme = MapCore.IMcObjectScheme.Create(activeOverlay.GetOverlayManager(), polygonItem, MapCore.EMcPointCoordSystem.EPCS_WORLD, false);
                    }, 'SpatialQueriesService.createSingleAreaOfSightObjs => IMcObjectScheme.Create', true)

                    runMapCoreSafely(() => { polygonItem.SetDrawPriorityGroup(MapCore.IMcSymbolicItem.EDrawPriorityGroup.EDPG_TOP_MOST); }, 'SpatialQueriesService.createSingleAreaOfSightObjs => IMcSymbolicItem.SetDrawPriorityGroup', true)
                    //create objects
                    let contoursPoints = polygonType == 'seenPolygons' ? localAreaOfSightResults.pSeenPolygons?.aaContoursPoints : localAreaOfSightResults.pUnseenPolygons?.aaContoursPoints;
                    contoursPoints?.map(polygon => {
                        let object: MapCore.IMcObject = null;
                        runMapCoreSafely(() => { object = MapCore.IMcObject.Create(activeOverlay, polygonScheme, polygon) }, 'SpatialQueriesService.createSingleAreaOfSightObjs => IMcObject.Create', true)
                        polygonsObjects[polygonType] = [...polygonsObjects[polygonType], object];
                    })

                    let polygonOpt = new AreaOfSightOptionResultObjects(polygonType == 'seenPolygons' ? AreaOfSightOptionsEnum.isSeenPolygons : AreaOfSightOptionsEnum.isUnseenPolygons,
                        polygonsObjects[polygonType], this.setAreaOfSightObjectVisibility, this.removeObjectsCB);
                    objectsList = [...objectsList, polygonOpt];
                })
                //Line Of Sight
                let colors = {
                    visibleLine: visibilityColors.get(MapCore.IMcSpatialQueries.EPointVisibility.EPV_SEEN),
                    nonVisibleLine: visibilityColors.get(MapCore.IMcSpatialQueries.EPointVisibility.EPV_UNSEEN)
                };
                let linesObjectsList: MapCore.IMcObject[] = [];
                localAreaOfSightResults.paLinesOfSight.forEach(pointsArr => {
                    let currentLinesObjectsList = this.drawLinesByVisible(activeOverlay, mcCurrentSpatialQueries, pointsArr, colors);
                    linesObjectsList = [...linesObjectsList, ...currentLinesObjectsList];
                });
                let lineOption = new AreaOfSightOptionResultObjects(AreaOfSightOptionsEnum.isLineOfSight, linesObjectsList, this.setAreaOfSightObjectVisibility, this.removeObjectsCB);
                objectsList = [...objectsList, lineOption];
                //Static Objects
                let staticObjectsColor = visibilityColors.get(MapCore.IMcSpatialQueries.EPointVisibility.EPV_SEEN_STATIC_OBJECT);
                let contoursPolyObjectsColors: { objects: MapCore.IMcObject[], originColor: MapCore.SMcBColor, staticObjectsColor: MapCore.SMcBColor, staticObjectsIDs: MapCore.IMcSpatialQueries.SStaticObjectsIDs }[] = [];
                localAreaOfSightResults.paSeenStaticObjects?.forEach((staticObjectsArr) => {
                    let extrusionLayer = staticObjectsArr.pMapLayer as MapCore.IMcVector3DExtrusionMapLayer;
                    let originColor = extrusionLayer.GetObjectColor(staticObjectsArr.auIDs[0]);
                    runMapCoreSafely(() => { extrusionLayer.SetObjectsColor(staticObjectsColor, staticObjectsArr.auIDs); }, 'SpatialQueriesService.createSingleAreaOfSightObjs => IMcVector3DExtrusionMapLayer.SetObjectsColor', true)
                    let contoursPolyObjects: MapCore.IMcObject[] = [];
                    staticObjectsArr.aaStaticObjectsContours?.forEach((staticObjectContourArr) => {
                        staticObjectContourArr?.forEach((staticObjectContour) => {
                            let subTypeFlags: number = subItemsFlag | getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_ACCURATE_3D_SCREEN_WIDTH, getEnumDetailsList(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags)).code;
                            let polyItem = null;
                            runMapCoreSafely(() => {
                                polyItem = MapCore.IMcPolygonItem.Create(subTypeFlags, MapCore.IMcLineBasedItem.ELineStyle.ELS_SOLID,
                                    MapCore.bcBlackOpaque, 3, null, new MapCore.SMcFVector2D(0, -1), 1, MapCore.IMcLineBasedItem.EFillStyle.EFS_NONE);
                            }, 'SpatialQueriesService.createSingleAreaOfSightObjs => IMcPolygonItem.Create', true)
                            let contourObj: MapCore.IMcObject = null;
                            runMapCoreSafely(() => {
                                contourObj = MapCore.IMcObject.Create(activeOverlay, polyItem, MapCore.EMcPointCoordSystem.EPCS_WORLD,
                                    staticObjectContour.aPoints, false);
                            }, 'SpatialQueriesService.createSingleAreaOfSightObjs => IMcObject.Create', true)
                            contoursPolyObjects = [...contoursPolyObjects, contourObj];
                        })
                    });
                    contoursPolyObjectsColors = [...contoursPolyObjectsColors, { objects: contoursPolyObjects, originColor: originColor, staticObjectsColor: staticObjectsColor, staticObjectsIDs: staticObjectsArr }];
                })

                let staticObjsOption = new AreaOfSightOptionResultObjects(AreaOfSightOptionsEnum.isStaticObjects, contoursPolyObjectsColors, (objects: any, isChecked: boolean) => {
                    let allMcObjects = [];
                    objects.forEach((objectColor: { objects: MapCore.IMcObject[], originColor: MapCore.SMcBColor, staticObjectsColor: MapCore.SMcBColor, staticObjectsIDs: MapCore.IMcSpatialQueries.SStaticObjectsIDs }) => {
                        let currentExstrusionLayer = objectColor.staticObjectsIDs.pMapLayer as MapCore.IMcVector3DExtrusionMapLayer
                        let currentColor = currentExstrusionLayer.GetObjectColor(objectColor.staticObjectsIDs.auIDs[0])
                        if (_.isEqual(currentColor, objectColor.originColor)) {
                            runMapCoreSafely(() => { currentExstrusionLayer.SetObjectsColor(objectColor.staticObjectsColor, objectColor.staticObjectsIDs.auIDs) }, 'SpatialQueriesService.createSingleAreaOfSightObjs => IMcVector3DExtrusionMapLayer.SetObjectsColor', true)
                        }
                        else {
                            runMapCoreSafely(() => { currentExstrusionLayer.SetObjectsColor(objectColor.originColor, objectColor.staticObjectsIDs.auIDs) }, 'SpatialQueriesService.createSingleAreaOfSightObjs => IMcVector3DExtrusionMapLayer.SetObjectsColor', true)
                        }
                        allMcObjects = [...allMcObjects, ...objectColor.objects]
                    });
                    this.setAreaOfSightObjectVisibility(allMcObjects, isChecked)
                }, (objects: any) => {
                    let allMcObjects = [];
                    objects.forEach((objectColor: { objects: MapCore.IMcObject[], originColor: MapCore.SMcBColor, staticObjectsColor: MapCore.SMcBColor, staticObjectsIDs: MapCore.IMcSpatialQueries.SStaticObjectsIDs }) => {
                        let currentExstrusionLayer = objectColor.staticObjectsIDs.pMapLayer as MapCore.IMcVector3DExtrusionMapLayer
                        runMapCoreSafely(() => { currentExstrusionLayer.SetObjectsColor(objectColor.originColor, objectColor.staticObjectsIDs.auIDs) }, 'SpatialQueriesService.createSingleAreaOfSightObjs => IMcVector3DExtrusionMapLayer.SetObjectsColor', true)
                        allMcObjects = [...allMcObjects, ...objectColor.objects]
                    });
                    this.removeObjectsCB(allMcObjects);
                });
                objectsList = [...objectsList, staticObjsOption];
                //Draw Object Option
                let objectType = sightObjectsParams.EndAngle ? ObjectTypeEnum.ellipse : sightObjectsParams.RadiusX ? ObjectTypeEnum.rectangle : ObjectTypeEnum.polygon;
                let objAndSchemeSightObject: { obj: MapCore.IMcObject, objSchemeItem: MapCore.IMcObjectSchemeItem } = createItemAndObjByType(activeOverlay, sightObjectsParams.locationPoints, objectType, sightObjectsParams);
                let drawObjOption = new AreaOfSightOptionResultObjects(AreaOfSightOptionsEnum.isDrawObject, [objAndSchemeSightObject.obj], this.setAreaOfSightObjectVisibility, this.removeObjectsCB);
                objectsList = [...objectsList, drawObjOption];

                store.dispatch(setSpatialQueriesResultsObjects({ queryName: SpatialQueryName.GetAreaOfSight, objects: objectsList, removeObjectsCB: this.removeAreaOfSightOptionResultObjects }));
            }
        }, 'SpatialQueriesService.createSingleAreaOfSightObjs')
    }
    private createTraversabilityAlongLineObjs = (...args) => {
        runCodeSafely(() => {
            let [traversabilityPointsResult, linePoints, mcCurrentSpatialQueries] = args;
            this.removeExistObjects();
            let activeOverlay = this.getActiveOverlay(mcCurrentSpatialQueries);
            if (activeOverlay) {
                let objectsList: MapCore.IMcObject[] = [];
                let linesColorMap = new Map([[MapCore.IMcSpatialQueries.EPointTraversability.EPT_TRAVERSABLE, new MapCore.SMcBColor(0, 255, 0, 255)],
                [MapCore.IMcSpatialQueries.EPointTraversability.EPT_UNTRAVERSABLE, new MapCore.SMcBColor(255, 0, 0, 255)],
                [MapCore.IMcSpatialQueries.EPointTraversability.EPT_UNKNOWN, MapCore.bcWhiteOpaque]])
                let EItemSubTypeFlags = getEnumDetailsList(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags);
                let linesSchemes: Map<MapCore.IMcSpatialQueries.EPointTraversability, MapCore.IMcObjectScheme> = new Map();

                Array.from(linesColorMap.keys()).forEach((visType: MapCore.IMcSpatialQueries.EPointTraversability) => {
                    let subItemsFlag = visType == MapCore.IMcSpatialQueries.EPointTraversability.EPT_UNKNOWN ? getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN, EItemSubTypeFlags).code | getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_ATTACHED_TO_TERRAIN, EItemSubTypeFlags).code :
                        getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN, EItemSubTypeFlags).code;
                    let lineItem: MapCore.IMcLineItem = null;
                    runMapCoreSafely(() => {
                        lineItem = MapCore.IMcLineItem.Create(subItemsFlag, MapCore.IMcLineBasedItem.ELineStyle.ELS_SOLID, linesColorMap.get(visType), 10)
                    }, 'SpatialQueriesService.createTraversabilityAlongLineObjs => IMcLineItem.Create', true)
                    let lineScheme: MapCore.IMcObjectScheme = null;
                    runMapCoreSafely(() => {
                        lineScheme = MapCore.IMcObjectScheme.Create(activeOverlay.GetOverlayManager(), lineItem, MapCore.EMcPointCoordSystem.EPCS_WORLD, false);
                    }, 'SpatialQueriesService.createTraversabilityAlongLineObjs => IMcObjectScheme.Create', true)

                    runMapCoreSafely(() => { lineItem.SetDrawPriorityGroup(MapCore.IMcSymbolicItem.EDrawPriorityGroup.EDPG_TOP_MOST); }, 'SpatialQueriesService.createTraversabilityAlongLineObjs => IMcSymbolicItem.SetDrawPriorityGroup', true)
                    runMapCoreSafely(() => { lineItem.SetOutlineWidth(2); }, 'SpatialQueriesService.createTraversabilityAlongLineObjs => IMcSymbolicItem.SetOutlineWidth', true)
                    runMapCoreSafely(() => { lineItem.SetOutlineColor(MapCore.bcBlackOpaque); }, 'SpatialQueriesService.createTraversabilityAlongLineObjs => IMcSymbolicItem.SetOutlineColor', true)

                    linesSchemes.set(visType, lineScheme);
                });
                for (let i = 1; i < traversabilityPointsResult.length; i++) {
                    let pointA = this.convertPointFromVPtoOM(traversabilityPointsResult[i - 1].Point, mcCurrentSpatialQueries);
                    let pointB = this.convertPointFromVPtoOM(traversabilityPointsResult[i].Point, mcCurrentSpatialQueries);
                    let traversType = traversabilityPointsResult[i - 1].eTraversability;
                    let traversabilityObj: MapCore.IMcObject = null;
                    let currentScheme = linesSchemes.get(traversType)
                    runMapCoreSafely(() => {
                        traversabilityObj = MapCore.IMcObject.Create(activeOverlay, currentScheme, [pointA, pointB]);
                    }, 'SpatialQueriesService.createTraversabilityAlongLineObjs => IMcObject.Create', true)
                    objectsList = [...objectsList, traversabilityObj];
                }

                let objSchemeItem: MapCore.IMcObjectSchemeItem = null;
                let subItemsFlag = getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_ATTACHED_TO_TERRAIN, EItemSubTypeFlags).code |
                    getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN, EItemSubTypeFlags).code;
                runMapCoreSafely(() => {
                    objSchemeItem = MapCore.IMcLineItem.Create(subItemsFlag, MapCore.IMcLineBasedItem.ELineStyle.ELS_SOLID, MapCore.bcBlackOpaque, 3);
                }, 'SpatialQueriesService.createTraversabilityAlongLineObjs => IMcLineItem.Create', true)
                let inputLineObj: MapCore.IMcObject = null;
                runMapCoreSafely(() => {
                    inputLineObj = MapCore.IMcObject.Create(activeOverlay, objSchemeItem, MapCore.EMcPointCoordSystem.EPCS_WORLD, linePoints, false);
                }, 'SpatialQueriesService.createTraversabilityAlongLineObjs => IMcObject.Create', true)
                objectsList = [...objectsList, inputLineObj];

                store.dispatch(setSpatialQueriesResultsObjects({ queryName: SpatialQueryName.GetTraversabilityAlongLine, objects: objectsList, removeObjectsCB: (objects: MapCore.IMcObject[]) => { this.removeObjectsCB(objects) } }));
            }
        }, 'SpatialQueriesService.createTraversabilityAlongLineObjs')
    }
    private createLocationFromTwoDistancesAndAzimuthObjs = (...args) => {
        runCodeSafely(() => {
            let [firstOrigin, firstDistance, secondOrigin, secondDistance, locationResult, mcCurrentSpatialQueries] = args;
            this.removeExistObjects();
            let activeOverlay = this.getActiveOverlay(mcCurrentSpatialQueries);
            if (activeOverlay) {
                let objectsList: MapCore.IMcObject[] = [];
                let locationResultObj = this.createTesterVertexObject(activeOverlay, locationResult)
                let firstOriginObj = this.createTesterVertexObject(activeOverlay, firstOrigin)
                let secondOriginObj = this.createTesterVertexObject(activeOverlay, secondOrigin)
                let firstOriginCross = this.drawCrossedEllipse(activeOverlay, firstOrigin, firstDistance, new MapCore.SMcBColor(0, 0, 255, 150))
                let secondOriginCross = this.drawCrossedEllipse(activeOverlay, secondOrigin, secondDistance, new MapCore.SMcBColor(255, 255, 255, 150))

                objectsList = [...objectsList, locationResultObj, firstOriginObj, secondOriginObj, firstOriginCross, secondOriginCross];
                store.dispatch(setSpatialQueriesResultsObjects({ queryName: SpatialQueryName.LocationFromTwoDistancesAndAzimuth, objects: objectsList, removeObjectsCB: (objects: MapCore.IMcObject[]) => { this.removeObjectsCB(objects) } }));
            }
        }, 'SpatialQueriesService.createLocationFromTwoDistancesAndAzimuthObjs')
    }
    //#endregion

    //#region Public Function
    public manageQueryResultObjects(mcCurrentSpatialQueries: MapCore.IMcSpatialQueries, spatialQueryName: SpatialQueryName, ...args) {
        switch (spatialQueryName) {
            case SpatialQueryName.GetTerrainHeightsAlongLine:
                this.createHeightAlongLineObjs(...args, mcCurrentSpatialQueries);
                break;
            case SpatialQueryName.GetTerrainHeight:
                this.createTerrainHeightObjs(...args, mcCurrentSpatialQueries);
                break;
            case SpatialQueryName.GetTerrainHeightMatrix:
                this.createTerrainHeightMatrixObjs(...args, mcCurrentSpatialQueries);
                break;
            case SpatialQueryName.GetExtremeHeightPointsInPolygon:
                this.createExtremeObjs(...args, mcCurrentSpatialQueries);
                break;
            case SpatialQueryName.GetRayIntersection:
            case SpatialQueryName.GetRayIntersectionTargets:
                this.createRayIntersectionObjs(...args, mcCurrentSpatialQueries);
                break;
            case SpatialQueryName.GetPointVisibility:
                this.createPointVisibilityObjs(...args, mcCurrentSpatialQueries);
                break;
            case SpatialQueryName.GetLineOfSight:
                this.createLineOfSightObjs(...args, mcCurrentSpatialQueries);
                break;
            case SpatialQueryName.GetBestScoutersLocationsInEllipse:
                this.createBestScoutersLocationsObjs(...args, mcCurrentSpatialQueries);
                break;
            case SpatialQueryName.GetAreaOfSightForMultipleScouters:
                this.createAreaOfSightForMultipleScoutersObjs(...args, mcCurrentSpatialQueries);
                break;
            case SpatialQueryName.GetAreaOfSight:
                this.createSingleAreaOfSightObjs(...args, mcCurrentSpatialQueries);
                break;
            case SpatialQueryName.GetTraversabilityAlongLine:
                this.createTraversabilityAlongLineObjs(...args, mcCurrentSpatialQueries);
                break;
            case SpatialQueryName.LocationFromTwoDistancesAndAzimuth:
                this.createLocationFromTwoDistancesAndAzimuthObjs(...args, mcCurrentSpatialQueries);
                break;
            default:
                break;
        }
    }
    public createMatrix = (mcCurrentSpatialQueries: MapCore.IMcSpatialQueries, areaOfSight: MapCore.IMcSpatialQueries.IAreaOfSight, bFillPointsVisibility: boolean, sumType?: MapCore.IMcSpatialQueries.EScoutersSumType, maxNumOfScouters?: number) => {
        let matrixTexture: MapCore.IMcObject = null;
        runCodeSafely(() => {
            let aOSMatrix = areaOfSight.GetAreaOfSightMatrix(bFillPointsVisibility);
            let textureBuffer: Uint8Array = this.getIAreaOfSightTextureBuffer(areaOfSight, bFillPointsVisibility, sumType, maxNumOfScouters);
            let memoryBufferTexture: MapCore.IMcMemoryBufferTexture = null;
            runMapCoreSafely(() => {
                memoryBufferTexture = MapCore.IMcMemoryBufferTexture.Create(aOSMatrix.uWidth, aOSMatrix.uHeight, MapCore.IMcTexture.EPixelFormat.EPF_A8B8G8R8,
                    MapCore.IMcTexture.EUsage.EU_STATIC, true, textureBuffer);
            }, "SpatialQueriesService.createMatrix => IMcMemoryBufferTexture.Create", true);

            let EItemSubTypeFlagsArr = getEnumDetailsList(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags);
            let subTypeEnum = getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_WORLD, EItemSubTypeFlagsArr).code | getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_ATTACHED_TO_TERRAIN, EItemSubTypeFlagsArr).code;

            let picItem: MapCore.IMcPictureItem = null;
            runMapCoreSafely(() => {
                picItem = MapCore.IMcPictureItem.Create(subTypeEnum, MapCore.EMcPointCoordSystem.EPCS_WORLD,
                    memoryBufferTexture, aOSMatrix.uWidth * aOSMatrix.fTargetResolutionInMapUnitsX,
                    aOSMatrix.uHeight * aOSMatrix.fTargetResolutionInMapUnitsY,
                    false,
                    new MapCore.SMcBColor(255, 255, 255, 255),
                    MapCore.IMcSymbolicItem.EBoundingRectanglePoint.EBRP_TOP_LEFT);
            }, "SpatialQueriesService.createMatrix => IMcPictureItem.Create", true);

            runMapCoreSafely(() => { picItem.SetRotationYaw(aOSMatrix.fAngle) }, "SpatialQueriesService.createMatrix => IMcSymbolicItem.SetRotationYaw", true);//, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

            let locationPoint: MapCore.SMcVector3D = null;
            runMapCoreSafely(() => { locationPoint = this.getMcCurrentViewport(mcCurrentSpatialQueries).ViewportToOverlayManagerWorld(aOSMatrix.LeftTopPoint); }, "SpatialQueriesService.createMatrix => IMcMapViewport.ViewportToOverlayManagerWorld", true);
            let activeOverlay = this.getActiveOverlay(mcCurrentSpatialQueries);

            runMapCoreSafely(() => {
                matrixTexture = MapCore.IMcObject.Create(activeOverlay, picItem, MapCore.EMcPointCoordSystem.EPCS_WORLD, [locationPoint], false);
            }, "SpatialQueriesService.createMatrix => IMcObject.Create", true);
        }, 'SpatialQueriesService.createMatrix')
        return matrixTexture;
        // obj.GetScheme().SetName("Tester AOS Texture Scheme " + Manager_MCViewports.IndexSchemeAOS.ToString());
        // Manager_MCViewports.IndexSchemeAOS++;
        // if (m_currCalcParams.TextureGPU != null) {
        //     m_currCalcParams.TextureGPU.Dispose();
        //     m_currCalcParams.TextureGPU.Remove();
        // }
        // m_currCalcParams.TextureGPU = obj;
    }
    public removeExistObjects() {
        runCodeSafely(() => {
            let spatialQueriesResultsObjects = store.getState().mapWorldTreeReducer.spatialQueriesResultsObjects
            spatialQueriesResultsObjects.removeObjectsCB(spatialQueriesResultsObjects.objects);
            store.dispatch(setSpatialQueriesResultsObjects({ queryName: null, objects: [], removeObjectsCB: (objects: MapCore.IMcObject[]) => { } }))
        }, 'SpatialQueriesService.removeExistObjects')
    }
    public removeObjectsCB(objects: MapCore.IMcObject[]) {
        runCodeSafely(() => {
            objects?.forEach(mcObj => {
                runMapCoreSafely(() => { mcObj?.Remove() }, 'SpatialQueriesService.removeObjectsCB => IMcObject.Remove', true)
            });
        }, 'SpatialQueriesService.removeObjectsCB')
    }
    public setAreaOfSightObjectVisibility = (objects: any, isChecked: boolean) => {
        runCodeSafely(() => {
            let visibilityOption = isChecked ? MapCore.IMcConditionalSelector.EActionOptions.EAO_FORCE_TRUE : MapCore.IMcConditionalSelector.EActionOptions.EAO_FORCE_FALSE;
            if (objects) {
                let objectsArr = objects.length >= 0 ? objects : [objects];
                objectsArr.map(object => {
                    runMapCoreSafely(() => { object?.SetVisibilityOption(visibilityOption); }, "SpatialQueriesService.setAreaOfSightObjectVisibility => IMcObject.SetVisibilityOption", true);
                })
            }
        }, 'SpatialQueriesService.setAreaOfSightObjectVisibility')
    }
    public removeAreaOfSightOptionResultObjects = (objects: AreaOfSightOptionResultObjects[]) => {
        runCodeSafely(() => {
            objects.forEach((areaOfSightOptionResultObjects: AreaOfSightOptionResultObjects) => {
                areaOfSightOptionResultObjects.setRemoveFunction(areaOfSightOptionResultObjects.resultObject)
            })
        }, 'SpatialQueriesService.removeObjectsCB')
    }
    public convertPointFromVPtoOM(vpPoint: MapCore.SMcVector3D, mcCurrentSpatialQueries: MapCore.IMcSpatialQueries) {
        let finalPoint = vpPoint;
        runCodeSafely(() => {
            let omPoint: MapCore.SMcVector3D = null;
            let zone: { Value?: number } = {};
            let gridConverterOMToVP = this.createGridConverterOMToVP(mcCurrentSpatialQueries);
            if (gridConverterOMToVP) {
                runMapCoreSafely(() => { omPoint = gridConverterOMToVP.ConvertBtoA(vpPoint, zone) }, 'SpatialQueriesService.convertPointFromVPtoOM => IMcGridConverter.ConvertBtoA', true);
                finalPoint = omPoint;
            }
            else
                finalPoint = vpPoint;
        }, 'SpatialQueriesService.convertPointFromVPtoOM')
        return finalPoint;
    }
    public convertPointFromOMtoVP(omPoint: MapCore.SMcVector3D, mcCurrentSpatialQueries: MapCore.IMcSpatialQueries) {
        let finalPoint = omPoint;
        runCodeSafely(() => {
            let vpPoint: MapCore.SMcVector3D = null;
            let zone: { Value?: number } = {};
            let gridConverterOMToVP = this.createGridConverterOMToVP(mcCurrentSpatialQueries);
            if (gridConverterOMToVP) {
                runMapCoreSafely(() => { vpPoint = gridConverterOMToVP.ConvertAtoB(omPoint, zone) }, 'SpatialQueriesService.ConvertPointFromOMtoVP => IMcGridConverter.ConvertAtoB', true);
                finalPoint = vpPoint;
            }
            else
                finalPoint = omPoint;
        }, 'SpatialQueriesService.convertPointFromOMtoVP')
        return finalPoint;
    }
    //#endregion

}

export default new SpatialQueriesService();
