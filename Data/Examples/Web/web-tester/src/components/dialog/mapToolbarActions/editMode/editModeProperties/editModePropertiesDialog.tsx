import { ReactElement, useEffect, useState } from 'react';
import { TabView, TabPanel } from 'primereact/tabview';
import { useDispatch, useSelector } from 'react-redux';
import { Dialog } from 'primereact/dialog';
import { Button } from 'primereact/button';
import { Checkbox } from 'primereact/checkbox';

import { getEnumDetailsList, getEnumValueDetails, MapCoreData, MapCoreService, ViewportData } from 'mapcore-lib';
import './styles/editModePropertiesDialog.css';
import General from './subEditModeProperties/general';
import MapManipulationsOperations from './subEditModeProperties/mapManipulationsOperations';
import Permissions from './subEditModeProperties/permissions';
import UtilityItems from './subEditModeProperties/utilityItems';
import '../../../openMap/objectProperties/styles/objectPropertiesDialog.css';
import SecondDialogModel from '../../../../shared/models/second-dialog.model';
import { AppState } from '../../../../../redux/combineReducer';
import { setTypeEditModeDialogSecond } from '../../../../../redux/EditMode/editModeAction';
import { closeDialog, setDialogType } from '../../../../../redux/MapCore/mapCoreAction';
import EditModePropertiesBase from '../../../../../propertiesBase/editModePropertiesBase';
import generalService from '../../../../../services/general.service';
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler"
import { DialogTypesEnum } from '../../../../../tools/enum/enums';

export interface KeyStepType {
    stepType: MapCore.IMcEditMode.EKeyStepType;
    step: number;
}

type AzimuthType = "GEOGRAPHIC" | "GRID COORDINATE SYSTEM" | "MAGNETIC"
type MagneticAzimuthTimeType = "CURRENT" | "CUSTOM"

export interface HiddenIconsPerPermission {
    permissionType: MapCore.IMcEditMode.EPermission;
    hiddenIconsList: Uint32Array;
}
export interface UtilityPictureItem {
    utilityPictureType: MapCore.IMcEditMode.EUtilityPictureType;
    pictureItem: MapCore.IMcPictureItem;
}

export interface Utility3DEditItem {
    utility3DEditItemType: MapCore.IMcEditMode.EUtility3DEditItemType;
    editItem: MapCore.IMcObjectSchemeItem;
}
// EditModePropertiesBase + MapCore properties 
export class EditModeProperties extends EditModePropertiesBase {
    AutoChangeObjectOperationsParams: boolean = true;
    /*** General ***/
    AutoScroll: boolean = true;
    AutoScrollMargineSize: number = 50;
    RotatePictureOffset: number = 0;
    PointAndLineClickTolerance: number = 2;
    RectangleResizeRelativeToCenter: boolean = false;
    LastExitStatus: number = 1;
    AutoSuppressSightPresentationMapTilesWebRequests: boolean = true;
    /* Max Radius */
    MaxRadiusScreen: number = 0;
    MaxRadiusImage: number = 0;
    MaxRadiusWorld: number = 0;
    /* Multi Point Item */
    MaxNumOfPoints: number = 0;
    ForceMaxPoints: boolean = false;
    MouseMoveUsage: any; // = getEnumValueDetails(MapCore.IMcEditMode.EMouseMoveUsage.EMMU_REGULAR);
    IntersectionTargets: any = [];
    CameraMinPitchRange: number = -180;
    CameraMaxPitchRange: number = 180;
    KeyStepTypes: KeyStepType[] = [];

    /*** Map Manipulations Operations ***/
    // DistanceDirectionMeasureParams: MapCore.IMcEditMode.SDistanceDirectionMeasureParams;
    DistanceDirectionMeasureLine: MapCore.IMcLineItem;
    DistanceDirectionMeasureText: MapCore.IMcTextItem;
    /* Distance Text Params */
    EnableDistanceTextParams: boolean = true;
    DistanceFactor: number = 0;
    DistanceUnitsName: string = "";
    DistanceIsUnicode: boolean = false;
    DistanceDigits: number = 2;
    /* Angle Text Params */
    EnableAngleTextParams: boolean = true;
    AngleFactor: number = 0;
    AngleUnitsName: string = "";
    AngleIsUnicode: boolean = false;
    AngleDigits: number = 2;
    /* Height Text Params */
    EnableHeightTextParams: boolean = false;
    HeightFactor: number = 0;
    HeightUnitsName: string = "";
    HeightIsUnicode: boolean = false;
    HeightDigits: number = 2;
    /* Azimuth Type */
    Azimuth: AzimuthType = "GEOGRAPHIC";
    /* - Geographic Azimuth */
    /* - Grid Coordinate System Azimuth */
    SelectedGridCoordinateSystem: MapCore.IMcGridCoordinateSystem;
    GridCoordinateSystemOptions: { coordinateSystem: MapCore.IMcGridCoordinateSystem, name: string }[] = [];
    /* - Magnetic Azimuth */
    SelectCurrentTime: boolean = true;
    MagneticAzimuthTime: MagneticAzimuthTimeType = "CURRENT";
    CustomTime: Date;

    /**** Permissions ****/
    Permissions: { name: string; code: number; theEnum: any; }[] = [];
    HiddenIconsPerPermissions: MapCore.IMcEditMode.SPermissionHiddenIcons[] = [];
    isShowIconsMap: Map<MapCore.IMcEditMode.EPermission, boolean> = new Map([
        [MapCore.IMcEditMode.EPermission.EEMP_MOVE_VERTEX, false],
        [MapCore.IMcEditMode.EPermission.EEMP_BREAK_EDGE, false],
        [MapCore.IMcEditMode.EPermission.EEMP_RESIZE, false],
        [MapCore.IMcEditMode.EPermission.EEMP_ROTATE, false],
        [MapCore.IMcEditMode.EPermission.EEMP_DRAG, false],
        [MapCore.IMcEditMode.EPermission.EEMP_FINISH_TEXT_STRING_BY_KEY, false],
    ])

    /**** Utility Items ****/
    RectangleUtilityItem: MapCore.IMcRectangleItem;
    LineUtilityItem: MapCore.IMcLineItem;
    TextUtilityItem: MapCore.IMcTextItem;
    UtilityPictureItems: UtilityPictureItem[] = [];
    Utility3DEditItems: Utility3DEditItem[] = [];

    constructor(editModePropertiesBase: EditModePropertiesBase) {
        super();
        Object.assign(this, editModePropertiesBase);
    }
}

export default function EditModePropertiesDialog(props: { FooterHook: (footer: () => ReactElement) => void }) {
    const dispatch = useDispatch();
    const typeEditModeDialogSecond: SecondDialogModel = useSelector((state: AppState) => state.editModeReducer.typeEditModeDialogSecond);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [tabs, setTabs] = useState<string[]>(['General', 'Map Manipulations Operations', 'Permissions', 'Utility Items'])
    const [localProperties, setLocalProperties] = useState(new EditModeProperties(generalService.EditModePropertiesBase))


    const [enumDetails] = useState({
        mouseMoveUsage: getEnumDetailsList(MapCore.IMcEditMode.EMouseMoveUsage),
        intersectionTargetType: getEnumDetailsList(MapCore.IMcSpatialQueries.EIntersectionTargetType).slice(1),
        keyStepType: getEnumDetailsList(MapCore.IMcEditMode.EKeyStepType),
        visibleArea3DOperation: getEnumDetailsList(MapCore.IMcMapCamera.ESetVisibleArea3DOperation),
        permissions: getEnumDetailsList(MapCore.IMcEditMode.EPermission),
        utilityPictureType: getEnumDetailsList(MapCore.IMcEditMode.EUtilityPictureType),
        utility3DEditItemType: getEnumDetailsList(MapCore.IMcEditMode.EUtility3DEditItemType),
    });
    const activeViewport: ViewportData = useSelector((state: AppState) => MapCoreData.findViewport(state.mapWindowReducer.activeCard));

    let Comps: any = {
        'General': General,
        'Map Manipulations Operations': MapManipulationsOperations,
        'Permissions': Permissions,
        'Utility Items': UtilityItems
    }

    const getEditModePropertiesFooter = () => {
        return (
            <div className='em-props__column-footer form__footer-padding'>
                <div className='em-props__align-left'>
                    <Checkbox inputId="ingredient1" name="AutoChangeObjectOperationsParams"
                        onChange={(event: any) => {
                            runCodeSafely(() => {
                                setLocalProperties({ ...localProperties, "AutoChangeObjectOperationsParams": event.target.checked })
                            }, "EditModePropertiesDialog/AutoChangeObjectOperationsParams => onChange")
                        }}
                        checked={localProperties.AutoChangeObjectOperationsParams} />
                    <label htmlFor="autoChangeObjectOperationsParams" className="ml-2 props__input-label">Change automatically object operations' parameters according to the definition in each object's scheme</label>
                </div>
                <div>
                    <Button label="Apply" onClick={saveDialogProperties} />
                    <Button label="OK" onClick={() => {
                        saveDialogProperties();
                        dispatch(closeDialog(DialogTypesEnum.editModeProperties));
                    }} />
                </div>
            </div>
        );
    }

    useEffect(() => {
        runCodeSafely(() => {
            const root = document.documentElement;
            let pixelWidth = window.innerHeight * 1 * globalSizeFactor;
            root.style.setProperty('--em-props-dialog-width', `${pixelWidth}px`);
        }, 'EditModePropertiesDialog.useEffect');
    }, [])
    useEffect(() => {
        props.FooterHook(getEditModePropertiesFooter);
    }, [localProperties])

    /* ************************************ initialization start************************************ */
    const getAutoChangeObjectOperationsParams = () => {
        let AutoChangeObjectOperationsParams: boolean = true;
        runMapCoreSafely(() => {
            AutoChangeObjectOperationsParams = activeViewport.editMode.GetAutoChangeObjectOperationsParams()
        }, 'EditModePropertiesDialog/useEffect => getAutoChangeObjectOperationsParams', true);
        return AutoChangeObjectOperationsParams;
    }

    const getAutoScrollMode = () => {
        let AutoScrollMode: boolean = true;
        runMapCoreSafely(() => {
            AutoScrollMode = activeViewport.editMode.GetAutoScrollMode()
        }, 'EditModePropertiesDialog/useEffect => getAutoScrollMode', true);
        return AutoScrollMode;
    }

    const getMarginSize = () => {
        let MarginSize: number = 50;
        runMapCoreSafely(() => {
            MarginSize = activeViewport.editMode.GetMarginSize()
        }, 'EditModePropertiesDialog/useEffect => getMarginSize', true);
        return MarginSize;
    }

    const getRotatePictureOffset = () => {
        let RotatePictureOffset: number = 0;
        runMapCoreSafely(() => {
            RotatePictureOffset = activeViewport.editMode.GetRotatePictureOffset()
        }, 'EditModePropertiesDialog/useEffect => getRotatePictureOffset()', true);
        return RotatePictureOffset;
    }

    const getPointAndLineClickTolerance = () => {
        let PointAndLineClickTolerance: number = 2;
        runMapCoreSafely(() => {
            PointAndLineClickTolerance = activeViewport.editMode.GetPointAndLineClickTolerance()
        }, 'EditModePropertiesDialog/useEffect => getPointAndLineClickTolerance()', true);
        return PointAndLineClickTolerance;
    }

    const getRectangleResizeRelativeToCenter = () => {
        let RectangleResizeRelativeToCenter: boolean = false;
        runMapCoreSafely(() => {
            RectangleResizeRelativeToCenter = activeViewport.editMode.GetRectangleResizeRelativeToCenter()
        }, 'EditModePropertiesDialog/useEffect => getRectangleResizeRelativeToCenter()', true);
        return RectangleResizeRelativeToCenter;
    }

    const getLastExitStatus = () => {
        let LastExitStatus: number = 0;
        runMapCoreSafely(() => {
            LastExitStatus = activeViewport.editMode.GetLastExitStatus()
        }, 'EditModePropertiesDialog/useEffect => getLastExitStatus()', true);
        return LastExitStatus;
    }

    const getAutoSuppressQueryPresentationMapTilesWebRequests = () => {
        let AutoSuppressQueryPresentationMapTilesWebRequests: boolean = false;
        runMapCoreSafely(() => {
            AutoSuppressQueryPresentationMapTilesWebRequests = activeViewport.editMode.GetAutoSuppressQueryPresentationMapTilesWebRequests()
        }, 'EditModePropertiesDialog/useEffect => getAutoSuppressQueryPresentationMapTilesWebRequests()', true);
        return AutoSuppressQueryPresentationMapTilesWebRequests;
    }

    const getMaxNumberOfPoints = () => {
        let puMaxNumberOfPoints: any = {};
        let pbForceFinishOnMaxPoints: any = {};
        runMapCoreSafely(() => {
            activeViewport.editMode.GetMaxNumberOfPoints(puMaxNumberOfPoints, pbForceFinishOnMaxPoints)
        }, 'EditModePropertiesDialog/useEffect => getMaxNumberOfPoints()', true);
        return {
            MaxNumberOfPoints: puMaxNumberOfPoints.Value,
            ForceFinishOnMaxPoints: pbForceFinishOnMaxPoints.Value
        };
    }

    const getMouseMoveUsageForMultiPointItem = () => {
        let MouseMoveUsage: MapCore.IMcEditMode.EMouseMoveUsage = MapCore.IMcEditMode.EMouseMoveUsage.EMMU_REGULAR
        runMapCoreSafely(() => {
            MouseMoveUsage = activeViewport.editMode.GetMouseMoveUsageForMultiPointItem()
        }, 'EditModePropertiesDialog/useEffect => getMouseMoveUsageForMultiPointItem()', true);
        return MouseMoveUsage;
    }

    const getMaxRadius = (eCoordSystem: MapCore.EMcPointCoordSystem) => {
        let MaxRadius: number = 0;
        runMapCoreSafely(() => {
            MaxRadius = activeViewport.editMode.GetMaxRadius(eCoordSystem);
        }, 'EditModePropertiesDialog/useEffect => getMaxRadius(${eCoordSystem})', true);
        return MaxRadius;
    }

    const getIntersectionTargetTypes = () => {
        let intersectionTargets: any[] = [];
        runMapCoreSafely(() => {
            let intersectionTargetsBitMask = activeViewport.editMode.GetIntersectionTargets();
            for (let i = 0; i < enumDetails.intersectionTargetType.length; ++i) {
                if ((enumDetails.intersectionTargetType[i].code & intersectionTargetsBitMask) === enumDetails.intersectionTargetType[i].code) {
                    intersectionTargets.push(enumDetails.intersectionTargetType[i]);
                }
            }
        }, 'EditModePropertiesDialog/useEffect => getIntersectionTargetTypes', true);

        return intersectionTargets;
    }

    const getCameraPitchRange = () => {
        let puMinPitch: any = {};
        let puMaxPitch: any = {};
        runMapCoreSafely(() => {
            activeViewport.editMode.GetCameraPitchRange(puMinPitch, puMaxPitch)
        }, 'EditModePropertiesDialog/useEffect => getCameraPitchRange()', true);
        return {
            MinPitch: puMinPitch.Value,
            MaxPitch: puMaxPitch.Value
        };
    }

    const getKeyStepTypes = () => {
        let keyStepTypes: KeyStepType[] = [];
        runMapCoreSafely(() => {
            for (let i = 0; i < enumDetails.keyStepType.length; ++i) {
                let currStep = activeViewport.editMode.GetKeyStep(enumDetails.keyStepType[i].theEnum);
                keyStepTypes.push({ stepType: enumDetails.keyStepType[i].code, step: currStep });
            }

        }, 'EditModePropertiesDialog/useEffect => getKeyStepTypes', true);

        return keyStepTypes;
    }

    const getDistanceDirectionMeasureParams = () => {
        let DistanceDirectionMeasureParams: MapCore.IMcEditMode.SDistanceDirectionMeasureParams;
        runMapCoreSafely(() => {
            DistanceDirectionMeasureParams = activeViewport.editMode.GetDistanceDirectionMeasureParams();
        }, 'EditModePropertiesDialog/useEffect => getDistanceDirectionMeasureParams()', true);
        return DistanceDirectionMeasureParams;
    }

    const getPermissions = () => {
        let permissions: any[] = [];
        runMapCoreSafely(() => {
            let permissionsBitMask = activeViewport.editMode.GetPermissions();
            for (let i = 0; i < enumDetails.permissions.length; ++i) {
                if ((enumDetails.permissions[i].code & permissionsBitMask) === enumDetails.permissions[i].code) {
                    permissions.push(enumDetails.permissions[i]);
                }
            }
        }, 'EditModePropertiesDialog/useEffect => getPermissions', true);

        return permissions;
    }

    const getHiddenIconsPerPermission = () => {
        let HiddenIconsPerPermissionList: MapCore.IMcEditMode.SPermissionHiddenIcons[] = [];
        runMapCoreSafely(() => {
            for (let i = 0; i < enumDetails.permissions.length; ++i) {
                if (enumDetails.permissions[i].theEnum !== MapCore.IMcEditMode.EPermission.EEMP_NONE
                    && enumDetails.permissions[i].theEnum !== MapCore.IMcEditMode.EPermission.EEMP_FINISH_TEXT_STRING_BY_KEY) {
                    HiddenIconsPerPermissionList.push({
                        ePermission: enumDetails.permissions[i].theEnum,
                        auIconIndices: activeViewport.editMode.GetHiddenIconsPerPermission(enumDetails.permissions[i].theEnum)
                    });
                }
            }
        }, 'EditModePropertiesDialog/useEffect => getHiddenIconsPerPermission', true);
        return HiddenIconsPerPermissionList;
    }
    const getUtilityItems = () => {
        let pRectangleUtilityItem: any = {}, pLineUtilityItem: any = {}, pTextUtilityItem: any = {};
        runMapCoreSafely(() => {
            activeViewport.editMode.GetUtilityItems(pRectangleUtilityItem, pLineUtilityItem, pTextUtilityItem);
        }, 'EditModePropertiesDialog/useEffect => getUtilityItems', true);

        return [pRectangleUtilityItem ? pRectangleUtilityItem.Value as MapCore.IMcRectangleItem : null,
        pLineUtilityItem ? pLineUtilityItem.Value as MapCore.IMcLineItem : null,
        pTextUtilityItem ? pTextUtilityItem.Value as MapCore.IMcTextItem : null]
    }
    const getUtilityPictureItems = () => {
        let UtilityPictureItems: UtilityPictureItem[] = []
        for (let i = 0; i < enumDetails.utilityPictureType.length - 1; ++i) {
            let utilityPicture: MapCore.IMcPictureItem = null;
            runMapCoreSafely(() => {
                utilityPicture = activeViewport.editMode.GetUtilityPicture(enumDetails.utilityPictureType[i].code)
            }, 'EditModePropertiesDialog/useEffect.getUtilityPictureItems => IMcEditMode.GetUtilityPicture', true);
            UtilityPictureItems.push({
                utilityPictureType: enumDetails.utilityPictureType[i].theEnum,
                pictureItem: utilityPicture
            })
        }
        return UtilityPictureItems;
    }
    const getUtility3DEditItems = () => {
        let Utility3DEditItems: Utility3DEditItem[] = []
        runCodeSafely(() => {
            for (let i = 0; i < enumDetails.utility3DEditItemType.length - 1; ++i) {
                Utility3DEditItems.push({ utility3DEditItemType: enumDetails.utility3DEditItemType[i].theEnum, editItem: activeViewport.editMode.GetUtility3DEditItem(enumDetails.utility3DEditItemType[i].code) })
            }
        }, 'EditModePropertiesDialog/useEffect.getUtility3DEditItems');
        return Utility3DEditItems;
    }

    useEffect(() => {
        runCodeSafely(() => {
            let MaxNumberOfPointsInfo: {
                MaxNumberOfPoints: number,
                ForceFinishOnMaxPoints: boolean
            } = getMaxNumberOfPoints();

            let CameraPitchRange: {
                MinPitch: number,
                MaxPitch: number
            } = getCameraPitchRange();

            let DistanceDirectionMeasureParams = getDistanceDirectionMeasureParams();

            const [RectangleUtilityItem, LineUtilityItem, TextUtilityItem] = getUtilityItems();

            setLocalProperties({
                ...localProperties,
                AutoChangeObjectOperationsParams: getAutoChangeObjectOperationsParams(),
                /*** General ***/
                AutoScroll: getAutoScrollMode(),
                // DiscardChanges: ,
                AutoScrollMargineSize: getMarginSize(),
                RotatePictureOffset: getRotatePictureOffset(),
                PointAndLineClickTolerance: getPointAndLineClickTolerance(),
                RectangleResizeRelativeToCenter: getRectangleResizeRelativeToCenter(),
                LastExitStatus: getLastExitStatus(),
                AutoSuppressSightPresentationMapTilesWebRequests: getAutoSuppressQueryPresentationMapTilesWebRequests(),
                /* Max Radius */
                MaxRadiusScreen: getMaxRadius(MapCore.EMcPointCoordSystem.EPCS_SCREEN),
                MaxRadiusImage: getMaxRadius(MapCore.EMcPointCoordSystem.EPCS_IMAGE),
                MaxRadiusWorld: getMaxRadius(MapCore.EMcPointCoordSystem.EPCS_WORLD),
                /* Multi Point Item */
                MaxNumOfPoints: MaxNumberOfPointsInfo.MaxNumberOfPoints,
                ForceMaxPoints: MaxNumberOfPointsInfo.ForceFinishOnMaxPoints,
                MouseMoveUsage: getEnumValueDetails(getMouseMoveUsageForMultiPointItem(), enumDetails.mouseMoveUsage),
                /** 3D Map Only **/
                IntersectionTargets: [...getIntersectionTargetTypes()],
                CameraMinPitchRange: CameraPitchRange.MinPitch,
                CameraMaxPitchRange: CameraPitchRange.MaxPitch,
                KeyStepTypes: [...getKeyStepTypes()],

                /*** Map Manipulations Operations ***/
                DynamicZoomOperation: generalService.EditModePropertiesBase.DynamicZoomOperation ?? MapCore.IMcMapCamera.ESetVisibleArea3DOperation.ESVAO_ROTATE_AND_MOVE, // EditModePropertiesBase initialization
                DistanceDirectionMeasureLine: DistanceDirectionMeasureParams.pLine,
                DistanceDirectionMeasureText: DistanceDirectionMeasureParams.pText,
                /* Distance Text Params */
                EnableDistanceTextParams: DistanceDirectionMeasureParams.pDistanceTextParams !== null,
                DistanceFactor: DistanceDirectionMeasureParams.pDistanceTextParams?.dUnitsFactor,
                DistanceUnitsName: DistanceDirectionMeasureParams.pDistanceTextParams?.UnitsName?.astrStrings?.length > 0 ? DistanceDirectionMeasureParams.pDistanceTextParams?.UnitsName.astrStrings[0] : "",
                DistanceIsUnicode: DistanceDirectionMeasureParams.pDistanceTextParams?.UnitsName?.bIsUnicode,
                DistanceDigits: DistanceDirectionMeasureParams.pDistanceTextParams?.uNumDigitsAfterDecimalPoint,
                /* Angle Text Params */
                EnableAngleTextParams: DistanceDirectionMeasureParams.pAngleTextParams !== null,
                AngleFactor: DistanceDirectionMeasureParams.pAngleTextParams?.dUnitsFactor,
                AngleUnitsName: DistanceDirectionMeasureParams.pAngleTextParams?.UnitsName?.astrStrings?.length > 0 ? DistanceDirectionMeasureParams.pAngleTextParams?.UnitsName.astrStrings[0] : "",
                AngleIsUnicode: DistanceDirectionMeasureParams.pAngleTextParams?.UnitsName?.bIsUnicode, //(DistanceDirectionMeasureParams.pAngleTextParams?.UnitsName as any)?.bIsUnicode,
                AngleDigits: DistanceDirectionMeasureParams.pAngleTextParams?.uNumDigitsAfterDecimalPoint,
                /* Height Text Params */
                EnableHeightTextParams: DistanceDirectionMeasureParams.pHeightTextParams !== null,
                HeightFactor: DistanceDirectionMeasureParams.pHeightTextParams?.dUnitsFactor ?? null,
                HeightUnitsName: DistanceDirectionMeasureParams.pHeightTextParams?.UnitsName?.astrStrings?.length > 0 ? DistanceDirectionMeasureParams.pHeightTextParams?.UnitsName.astrStrings[0] : "",
                HeightIsUnicode: DistanceDirectionMeasureParams.pHeightTextParams?.UnitsName?.bIsUnicode,
                HeightDigits: DistanceDirectionMeasureParams.pHeightTextParams?.uNumDigitsAfterDecimalPoint,
                /* Azimuth Type */
                Azimuth: DistanceDirectionMeasureParams.bUseMagneticAzimuth ? "MAGNETIC" : DistanceDirectionMeasureParams.pDirectionCoordSys !== null ? "GRID COORDINATE SYSTEM" : "GEOGRAPHIC",
                /* - Geographic Azimuth */
                /* - Grid Coordinate System Azimuth */
                SelectedGridCoordinateSystem: DistanceDirectionMeasureParams.pDirectionCoordSys,
                GridCoordinateSystemOptions: MapCoreService.getGridCoordinateSystems(),
                /* - Magnetic Azimuth */
                MagneticAzimuthTime: DistanceDirectionMeasureParams.pDate !== null ? "CUSTOM" : "CURRENT",
                CustomTime: DistanceDirectionMeasureParams.pDate,

                /**** Permissions ****/
                Permissions: getPermissions(),
                HiddenIconsPerPermissions: getHiddenIconsPerPermission(),

                /**** Utility Items ****/
                RectangleUtilityItem: RectangleUtilityItem as MapCore.IMcRectangleItem,
                LineUtilityItem: LineUtilityItem as MapCore.IMcLineItem,
                TextUtilityItem: TextUtilityItem as MapCore.IMcTextItem,
                UtilityPictureItems: getUtilityPictureItems(),
                Utility3DEditItems: getUtility3DEditItems(),

                // Get3DEditParams() : IMcEditMode.S3DEditParams;
                // GetObjectOperationsParams() : IMcEditMode.SObjectOperationsParams;
                // GetAutoChangeObjectOperationsParams() : boolean;
            });
        }, 'EditModePropertiesDialog.useEffect');
    }, [])

    /* ************************************ initialization end************************************ */

    const saveDialogProperties = () => {
        runCodeSafely(() => {
            // Save Local Properties:
            Object.assign(generalService.EditModePropertiesBase, localProperties)
            // Save MapCore Properties:
            runMapCoreSafely(() => {
                activeViewport.editMode.SetAutoChangeObjectOperationsParams(localProperties.AutoChangeObjectOperationsParams)
            }, 'EditModePropertiesDialog/saveDialogProperties => SetAutoChangeObjectOperationsParams', true);
            /*** General ***/
            runMapCoreSafely(() => {
                activeViewport.editMode.AutoScroll(localProperties.AutoScroll, localProperties.AutoScrollMargineSize)
            }, 'EditModePropertiesDialog/saveDialogProperties => AutoScroll', true);
            runMapCoreSafely(() => {
                activeViewport.editMode.SetRotatePictureOffset(localProperties.RotatePictureOffset)
            }, 'EditModePropertiesDialog/saveDialogProperties => SetRotatePictureOffset', true);
            runMapCoreSafely(() => {
                activeViewport.editMode.SetPointAndLineClickTolerance(localProperties.PointAndLineClickTolerance)
            }, 'EditModePropertiesDialog/saveDialogProperties => SetPointAndLineClickTolerance', true);
            runMapCoreSafely(() => {
                activeViewport.editMode.SetRectangleResizeRelativeToCenter(localProperties.RectangleResizeRelativeToCenter)
            }, 'EditModePropertiesDialog/saveDialogProperties => SetRectangleResizeRelativeToCenter', true);
            runMapCoreSafely(() => {
                activeViewport.editMode.SetAutoSuppressQueryPresentationMapTilesWebRequests(localProperties.AutoSuppressSightPresentationMapTilesWebRequests)
            }, 'EditModePropertiesDialog/saveDialogProperties => SetAutoSuppressQueryPresentationMapTilesWebRequests', true);
            /* Max Radius */
            runMapCoreSafely(() => {
                activeViewport.editMode.SetMaxRadius(localProperties.MaxRadiusScreen, MapCore.EMcPointCoordSystem.EPCS_SCREEN)
            }, 'EditModePropertiesDialog/saveDialogProperties => SetMaxRadius', true);
            runMapCoreSafely(() => {
                activeViewport.editMode.SetMaxRadius(localProperties.MaxRadiusImage, MapCore.EMcPointCoordSystem.EPCS_IMAGE)
            }, 'EditModePropertiesDialog/saveDialogProperties => SetMaxRadius', true);
            runMapCoreSafely(() => {
                activeViewport.editMode.SetMaxRadius(localProperties.MaxRadiusWorld, MapCore.EMcPointCoordSystem.EPCS_WORLD)
            }, 'EditModePropertiesDialog/saveDialogProperties => SetMaxRadius', true);
            /* Multi Point Item */
            runMapCoreSafely(() => {
                activeViewport.editMode.SetMaxNumberOfPoints(localProperties.MaxNumOfPoints, localProperties.ForceMaxPoints)
            }, 'EditModePropertiesDialog/saveDialogProperties => SetMaxNumberOfPoints', true);
            runMapCoreSafely(() => {
                activeViewport.editMode.SetMouseMoveUsageForMultiPointItem(localProperties.MouseMoveUsage.theEnum)
            }, 'EditModePropertiesDialog/saveDialogProperties => SetMouseMoveUsageForMultiPointItem', true);
            runMapCoreSafely(() => {
                let IntersectionTargetsBitMask = MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_NONE;
                for (let i = 0; i < localProperties.IntersectionTargets.length; ++i) {
                    IntersectionTargetsBitMask |= localProperties.IntersectionTargets[i].code;
                }
                activeViewport.editMode.SetIntersectionTargets(IntersectionTargetsBitMask)
            }, 'EditModePropertiesDialog/saveDialogProperties => SetIntersectionTargets', true);
            runMapCoreSafely(() => {
                activeViewport.editMode.SetCameraPitchRange(localProperties.CameraMinPitchRange, localProperties.CameraMaxPitchRange)
            }, 'EditModePropertiesDialog/saveDialogProperties => SetCameraPitchRange', true);
            runMapCoreSafely(() => {
                for (let i = 0; i < localProperties.KeyStepTypes.length; ++i) {
                    activeViewport.editMode.SetKeyStep(localProperties.KeyStepTypes[i].stepType, localProperties.KeyStepTypes[i].step)
                }
            }, 'EditModePropertiesDialog/saveDialogProperties => SetKeyStep', true);

            /*** Map Manipulations Operations ***/
            runMapCoreSafely(() => {
                let DistanceDirectionMeasureParams: MapCore.IMcEditMode.SDistanceDirectionMeasureParams = new MapCore.IMcEditMode.SDistanceDirectionMeasureParams();
                DistanceDirectionMeasureParams.pText = localProperties.DistanceDirectionMeasureText;
                DistanceDirectionMeasureParams.pLine = localProperties.DistanceDirectionMeasureLine;

                if (localProperties.EnableDistanceTextParams) {
                    DistanceDirectionMeasureParams.pDistanceTextParams = new MapCore.IMcEditMode.SMeasureTextParams();
                    DistanceDirectionMeasureParams.pDistanceTextParams.UnitsName = new MapCore.SMcVariantString(localProperties.DistanceUnitsName, localProperties.DistanceIsUnicode);
                    DistanceDirectionMeasureParams.pDistanceTextParams.dUnitsFactor = localProperties.DistanceFactor;
                    DistanceDirectionMeasureParams.pDistanceTextParams.uNumDigitsAfterDecimalPoint = localProperties.DistanceDigits;
                }
                if (localProperties.EnableAngleTextParams) {
                    DistanceDirectionMeasureParams.pAngleTextParams = new MapCore.IMcEditMode.SMeasureTextParams();
                    DistanceDirectionMeasureParams.pAngleTextParams.UnitsName = new MapCore.SMcVariantString(localProperties.AngleUnitsName, localProperties.AngleIsUnicode);
                    DistanceDirectionMeasureParams.pAngleTextParams.dUnitsFactor = localProperties.AngleFactor;
                    DistanceDirectionMeasureParams.pAngleTextParams.uNumDigitsAfterDecimalPoint = localProperties.AngleDigits;
                }
                if (localProperties.EnableHeightTextParams) {
                    DistanceDirectionMeasureParams.pHeightTextParams = new MapCore.IMcEditMode.SMeasureTextParams();
                    DistanceDirectionMeasureParams.pHeightTextParams.UnitsName = new MapCore.SMcVariantString(localProperties.HeightUnitsName, localProperties.HeightIsUnicode);
                    DistanceDirectionMeasureParams.pHeightTextParams.dUnitsFactor = localProperties.HeightFactor;
                    DistanceDirectionMeasureParams.pHeightTextParams.uNumDigitsAfterDecimalPoint = localProperties.HeightDigits;
                }

                DistanceDirectionMeasureParams.pDirectionCoordSys = localProperties.SelectedGridCoordinateSystem;
                DistanceDirectionMeasureParams.bUseMagneticAzimuth = localProperties.Azimuth == 'MAGNETIC';
                DistanceDirectionMeasureParams.pDate = localProperties.CustomTime;
                activeViewport.editMode.SetDistanceDirectionMeasureParams(DistanceDirectionMeasureParams);
            }, 'EditModePropertiesDialog/saveDialogProperties => SetDistanceDirectionMeasureParams', true);

            /**** Permissions ****/
            runMapCoreSafely(() => {
                let PermissionsTargetsBitMask = 0;
                for (let i = 0; i < localProperties.Permissions.length; ++i) {
                    PermissionsTargetsBitMask |= localProperties.Permissions[i].code;
                }
                activeViewport.editMode.SetPermissions(PermissionsTargetsBitMask);
            }, 'EditModePropertiesDialog/saveDialogProperties => SetPermissions', true);

            runMapCoreSafely(() => {
                for (let i = 0; i < localProperties.HiddenIconsPerPermissions.length; ++i) {
                    // convert from Uint32Array to Uint8Array
                    let hiddenIcons8Array = new Uint8Array(localProperties.HiddenIconsPerPermissions[i].auIconIndices.length);
                    for (let j = 0; j < localProperties.HiddenIconsPerPermissions[i].auIconIndices.length; ++j) {
                        hiddenIcons8Array[j] = localProperties.HiddenIconsPerPermissions[i].auIconIndices[j];
                    }

                    activeViewport.editMode.SetHiddenIconsPerPermission(localProperties.HiddenIconsPerPermissions[i].ePermission, hiddenIcons8Array);
                }
            }, 'EditModePropertiesDialog/saveDialogProperties => SetHiddenIconsPerPermission', true);

            /**** Utility Items ****/
            runMapCoreSafely(() => {
                activeViewport.editMode.SetUtilityItems(localProperties.RectangleUtilityItem, localProperties.LineUtilityItem, localProperties.TextUtilityItem);
            }, 'EditModePropertiesDialog/saveDialogProperties => SetUtilityItems', true);

            runMapCoreSafely(() => {
                for (let i = 0; i < localProperties.UtilityPictureItems.length; ++i) {
                    activeViewport.editMode.SetUtilityPicture(localProperties.UtilityPictureItems[i].pictureItem, localProperties.UtilityPictureItems[i].utilityPictureType);
                }
            }, 'EditModePropertiesDialog/saveDialogProperties => SetUtilityPicture', true);

            runMapCoreSafely(() => {
                for (let i = 0; i < localProperties.Utility3DEditItems.length; ++i) {
                    activeViewport.editMode.SetUtility3DEditItem(localProperties.Utility3DEditItems[i].editItem, localProperties.Utility3DEditItems[i].utility3DEditItemType);
                }
            }, 'EditModePropertiesDialog/saveDialogProperties => SetUtility3DEditItem', true);


            // Set3DEditParams(Params : IMcEditMode.S3DEditParams) : void;
            // SetEventsCallback(pEventsCallback : IMcEditMode.ICallback) : void;
            // ChangeObjectOperationsParams(Params : IMcEditMode.SObjectOperationsParams, bForOneOperationOnly? : boolean) : void;
            // SetAutoChangeObjectOperationsParams(bChange : boolean) : void;

        }, 'EditModePropertiesDialog.saveDialogProperties');
    }

    const setLocalPropertiesCallback = (key: string, value: any) => {
        runCodeSafely(() => {
            setLocalProperties(localProperties => ({ ...localProperties, [key]: value }))
        }, 'EditModePropertiesDialog.setLocalPropertiesCallback');
    }
    const getDialogClassName = (dialogHeader: string) => {
        switch (dialogHeader) {
            case 'Select Existing Item':
                return 'scroll-dialog-select-existing-item'
            default:
                return '';
        }
    }

    return (
        <div style={{ height: `${globalSizeFactor * 70}vh`, width: `${globalSizeFactor * 100}vh` }}>
            <TabView style={{ height: '100%', borderBottom: `${globalSizeFactor * 1}vh solid #dee2e6`, marginBottom: '1%' }}>
                {tabs.map((tab: string, key: number) => {
                    const Item = Comps[tab];
                    return <TabPanel header={tab} key={key} style={{ height: '100%' }}>
                        <Item dialogProperties={localProperties} setDialogPropertiesCallback={setLocalPropertiesCallback}></Item>
                    </TabPanel>
                })}
            </TabView>
            {typeEditModeDialogSecond && <Dialog
                className={getDialogClassName(typeEditModeDialogSecond.secondDialogHeader)}
                header={typeEditModeDialogSecond.secondDialogHeader}
                onHide={() => dispatch(setTypeEditModeDialogSecond(undefined))} visible>
                {typeEditModeDialogSecond.secondDialogComponent}
            </Dialog>}
        </div>
    );
}