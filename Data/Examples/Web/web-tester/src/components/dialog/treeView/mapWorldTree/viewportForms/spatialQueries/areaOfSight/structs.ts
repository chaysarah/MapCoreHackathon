import { getEnumValueDetails, getEnumDetailsList, ViewportData } from 'mapcore-lib';
import { AOSOperationProperties } from "./aOSOperation";
import { PointVisibilityColorProperties } from "./pointVisibilityColor";
import { CoverageQualityProperties } from "./coverageQuality";
import { ColorsSurroundingProperties } from "./colorsSurrounding";
import { MatrixOperationProperties } from "./matrixOperation";
import { Properties } from "../../../../../dialog";
import { ObjectTypeEnum } from "../../../../../shared/drawObjectCtrl";

export class AreaOfSightResult {
    ppAreaOfSight: MapCore.IMcSpatialQueries.IAreaOfSight;
    paLinesOfSight: MapCore.IMcSpatialQueries.SLineOfSightPoint[][];
    pSeenPolygons: MapCore.IMcSpatialQueries.SPolygonsOfSight;
    pUnseenPolygons: MapCore.IMcSpatialQueries.SPolygonsOfSight;
    paSeenStaticObjects: MapCore.IMcSpatialQueries.SStaticObjectsIDs[];

    constructor(ppAreaOfSight?: MapCore.IMcSpatialQueries.IAreaOfSight, paLinesOfSight?: MapCore.IMcSpatialQueries.SLineOfSightPoint[][], pSeenPolygons?: MapCore.IMcSpatialQueries.SPolygonsOfSight, pUnseenPolygons?: MapCore.IMcSpatialQueries.SPolygonsOfSight, paSeenStaticObjects?: MapCore.IMcSpatialQueries.SStaticObjectsIDs[]) {
        this.ppAreaOfSight = ppAreaOfSight;
        this.paLinesOfSight = paLinesOfSight;
        this.pSeenPolygons = pSeenPolygons;
        this.pUnseenPolygons = pUnseenPolygons;
        this.paSeenStaticObjects = paSeenStaticObjects;
    }
}
export class AreaOfSightOptionResultObjects {
    optionName: AreaOfSightOptionsEnum;
    resultObject: any;
    setVisibilityFunction: (objects: any, isChecked: boolean) => void;
    setRemoveFunction: (objects: any) => void;

    constructor(optionName: AreaOfSightOptionsEnum, resultObject: any, setVisibilityFunction: (objects: any, isChecked: boolean) => void, setRemoveFunction: (objects: any) => void) {
        this.optionName = optionName;
        this.resultObject = resultObject;
        this.setVisibilityFunction = setVisibilityFunction;
        this.setRemoveFunction = setRemoveFunction;
    }
}
export enum AreaOfSightOptionsEnum {
    isDrawObject = 'Draw Object',
    isLineOfSight = 'Line Of Sight',
    isAreaOfSight = 'Area Of Sight',
    isSeenPolygons = 'Seen Polygons',
    isUnseenPolygons = 'Unseen Polygons',
    isStaticObjects = 'Static Objects',
}
export class AreaOfSightSingleScouterGeneralParams {
    scouter: MapCore.SMcVector3D;
    bIsScouterHeightAbsolute: boolean;
    dTargetHeight: number;
    bTargetsHeightAbsolute: boolean;
    fTargetResolutionInMeters: number;
    dRotationAngle: number;
    uNumRaysPer360Degrees: number;
    aVisibilityColors: Map<MapCore.IMcSpatialQueries.EPointVisibility, MapCore.SMcBColor>;
    dMaxPitchAngle: number;
    dMinPitchAngle: number;
    bGPUBase: boolean;

    nameCalc: string;
    calculationOptions: AreaOfSightOptionsEnum[];
    isCreateAutomatic: boolean;
    bPointVisibility: boolean;

    constructor(scouter: MapCore.SMcVector3D, bIsScouterHeightAbsolute: boolean, dTargetHeight: number, bTargetsHeightAbsolute: boolean, fTargetResolutionInMeters: number,
        dRotationAngle: number, uNumRaysPer360Degrees: number, aVisibilityColors: Map<MapCore.IMcSpatialQueries.EPointVisibility, MapCore.SMcBColor>, dMaxPitchAngle: number, dMinPitchAngle: number,
        bGPUBase: boolean, nameCalc: string, calculationOptions: AreaOfSightOptionsEnum[], isCreateAutomatic: boolean, bPointVisibility: boolean
    ) {
        this.scouter = scouter;
        this.bIsScouterHeightAbsolute = bIsScouterHeightAbsolute;
        this.dTargetHeight = dTargetHeight;
        this.bTargetsHeightAbsolute = bTargetsHeightAbsolute;
        this.fTargetResolutionInMeters = fTargetResolutionInMeters;
        this.dRotationAngle = dRotationAngle;
        this.uNumRaysPer360Degrees = uNumRaysPer360Degrees;
        this.aVisibilityColors = aVisibilityColors;
        this.dMaxPitchAngle = dMaxPitchAngle;
        this.dMinPitchAngle = dMinPitchAngle;
        this.bGPUBase = bGPUBase;
        this.nameCalc = nameCalc;
        this.calculationOptions = calculationOptions;
        this.isCreateAutomatic = isCreateAutomatic;
        this.bPointVisibility = bPointVisibility;
    }
}

export class AreaOfSightProperties implements Properties {
    mcCurrentSpatialQueries: MapCore.IMcSpatialQueries;
    activeOverlay: MapCore.IMcOverlay;
    currentViewportData: ViewportData;
    selectedObjectType: ObjectTypeEnum;
    sightObjectPoints: MapCore.SMcVector3D[];
    confirmDialogMessage: string;
    confirmDialogVisible: boolean;
    objSerialNum: number;
    //Elipse Properties
    radiusX: number;
    radiusY: number;
    startAngle: number;
    endAngle: number;
    //Rectangle Properties
    width: number;
    height: number;
    //General Params
    scouter: MapCore.SMcVector3D;
    minPitchAngle: number;
    maxPitchAngle: number;
    rotationAzimuth: number;
    numberOfRays: number;
    targetHeight: number;
    targetResolution: number;
    numCallingFunction: number;
    nameCalc: string;
    isTargetHeightAbsolute: boolean;
    isGPUBased: boolean;
    isScouterHeightAbsolute: boolean;
    isAsync: boolean;
    scoutersAmount: string;
    //Multiple Scouters
    scoutersRadiusX: number;
    scoutersRadiusY: number;
    maxNumOfScouters: number;
    isBestScoutersAsync: boolean;
    scoutersSumTypeList: { name: string, code: number, theEnum: MapCore.IMcSpatialQueries.EScoutersSumType }[];
    selectedScoutersSumType: { name: string, code: number, theEnum: MapCore.IMcSpatialQueries.EScoutersSumType };
    scoutersPoints: MapCore.SMcVector3D[];
    //Calculation Options
    calculationOptions: AreaOfSightOptionsEnum[];
    //Define Colors
    colorsVisibilityList: { name: string, code: number, theEnum: MapCore.IMcSpatialQueries.EPointVisibility }[];;
    selectedColorVisibility: { name: string, code: number, theEnum: MapCore.IMcSpatialQueries.EPointVisibility };
    color: MapCore.SMcBColor;
    visibilityColors: Map<MapCore.IMcSpatialQueries.EPointVisibility, MapCore.SMcBColor>;
    //Results
    mapcoreResults: AreaOfSightResult;
    existOptionsSelection: { option: AreaOfSightOptionsEnum, isSelected: boolean }[];
    areaOfSightsArr: MapCore.IMcSpatialQueries.IAreaOfSight[];
    //Nested Tabs
    AOSOperationProperties: AOSOperationProperties;
    PointVisibilityColorProperties: PointVisibilityColorProperties;
    CoverageQualityProperties: CoverageQualityProperties;
    ColorsSurroundingProperties: ColorsSurroundingProperties;
    MatrixOperationProperties: MatrixOperationProperties;

    static getDefault(p: any): AreaOfSightProperties {
        let { mcCurrentSpatialQueries } = p;

        let scoutersSumTypeEnumList = getEnumDetailsList(MapCore.IMcSpatialQueries.EScoutersSumType);
        let selectedScoutersSumType = getEnumValueDetails(MapCore.IMcSpatialQueries.EScoutersSumType.ESST_ALL, scoutersSumTypeEnumList);
        let colorsVisibilityList = getEnumDetailsList(MapCore.IMcSpatialQueries.EPointVisibility);
        colorsVisibilityList = colorsVisibilityList.filter(colorVis => colorVis.theEnum != MapCore.IMcSpatialQueries.EPointVisibility.EPV_ASYNC_CALCULATING && colorVis.theEnum != MapCore.IMcSpatialQueries.EPointVisibility.EPV_NUM)
        let selectedColorVisibility = getEnumValueDetails(MapCore.IMcSpatialQueries.EPointVisibility.EPV_SEEN, colorsVisibilityList);

        let defaults: AreaOfSightProperties = {
            mcCurrentSpatialQueries: mcCurrentSpatialQueries,
            activeOverlay: null,
            currentViewportData: null,
            selectedObjectType: ObjectTypeEnum.ellipse,
            sightObjectPoints: [],
            confirmDialogMessage: '',
            confirmDialogVisible: false,
            objSerialNum: 0,
            //Elipse Properties
            radiusX: 1,
            radiusY: 1,
            startAngle: 0,
            endAngle: 360,
            //Rectangle Properties
            width: 1,
            height: 1,
            //General Params
            scouter: MapCore.v3Zero,
            minPitchAngle: -90,
            maxPitchAngle: 90,
            rotationAzimuth: 0,
            numberOfRays: 64,
            targetHeight: 1.7,
            targetResolution: 10,
            numCallingFunction: 1,
            nameCalc: 'Ellipse 0',
            isTargetHeightAbsolute: false,
            isGPUBased: false,
            isScouterHeightAbsolute: false,
            //Multiple Scouters
            isAsync: true,
            scoutersAmount: 'single',
            scoutersRadiusX: 1,
            scoutersRadiusY: 1,
            maxNumOfScouters: 32,
            isBestScoutersAsync: true,
            scoutersSumTypeList: scoutersSumTypeEnumList,
            selectedScoutersSumType: selectedScoutersSumType,
            scoutersPoints: [],
            //Calculation Options
            calculationOptions: [AreaOfSightOptionsEnum.isAreaOfSight, AreaOfSightOptionsEnum.isLineOfSight, AreaOfSightOptionsEnum.isSeenPolygons, AreaOfSightOptionsEnum.isUnseenPolygons, AreaOfSightOptionsEnum.isStaticObjects],
            //Define Colors
            colorsVisibilityList: colorsVisibilityList,
            selectedColorVisibility: selectedColorVisibility,
            color: new MapCore.SMcBColor(0, 255, 0, 192),
            visibilityColors: new Map([
                [MapCore.IMcSpatialQueries.EPointVisibility.EPV_SEEN, new MapCore.SMcBColor(0, 255, 0, 192)],
                [MapCore.IMcSpatialQueries.EPointVisibility.EPV_UNSEEN, new MapCore.SMcBColor(255, 0, 0, 255)],
                [MapCore.IMcSpatialQueries.EPointVisibility.EPV_UNKNOWN, new MapCore.SMcBColor(255, 0, 255, 192)],
                [MapCore.IMcSpatialQueries.EPointVisibility.EPV_OUT_OF_QUERY_AREA, new MapCore.SMcBColor(128, 128, 128, 0)],
                [MapCore.IMcSpatialQueries.EPointVisibility.EPV_SEEN_STATIC_OBJECT, new MapCore.SMcBColor(255, 255, 0, 192)],
            ]),
            //Results
            mapcoreResults: new AreaOfSightResult(),
            existOptionsSelection: [],
            areaOfSightsArr: [],
            //Nested Tabs
            AOSOperationProperties: AOSOperationProperties.getDefault(p),
            PointVisibilityColorProperties: PointVisibilityColorProperties.getDefault(p),
            CoverageQualityProperties: CoverageQualityProperties.getDefault(p),
            ColorsSurroundingProperties: ColorsSurroundingProperties.getDefault(p),
            MatrixOperationProperties: MatrixOperationProperties.getDefault(p),

        }
        return defaults;
    }
};