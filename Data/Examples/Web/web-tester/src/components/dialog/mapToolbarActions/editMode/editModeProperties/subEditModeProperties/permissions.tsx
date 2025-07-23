import { useState, useEffect, useMemo, useCallback, useRef } from 'react';
import { Checkbox, CheckboxChangeEvent } from 'primereact/checkbox';
import { InputText } from 'primereact/inputtext';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Fieldset } from 'primereact/fieldset';
import { useSelector } from 'react-redux';
import _ from 'lodash'

import { getEnumDetailsList, MapCoreData, ViewportData } from 'mapcore-lib';
import '../styles/editModePropertiesDialog.css';
import { EditModeProperties } from '../editModePropertiesDialog';
import { runAsyncCodeSafely, runCodeSafely, runMapCoreSafely } from '../../../../../../common/services/error-handling/errorHandler';
import { AppState } from '../../../../../../redux/combineReducer';
import screenPositionService from '../../../../../../services/screenPosition.service';

export class PermissionDetails {
    permission: { name: string; code: number; theEnum: any };
    enabled: boolean;
    hiddenIconsList: Uint32Array;
    isShowIcon: boolean;

    constructor(
        permission: { name: string; code: number; theEnum: any },
        enabled: boolean,
        hiddenIconsList: Uint32Array,
        isShowIcon: boolean
    ) {
        this.permission = permission;
        this.enabled = enabled;
        this.hiddenIconsList = hiddenIconsList;
        this.isShowIcon = isShowIcon;
    }
}

export default function Permissions(props: { dialogProperties: EditModeProperties, setDialogPropertiesCallback: (key: string, value: any) => void }) {
    const activeViewport: ViewportData = useSelector((state: AppState) => MapCoreData.findViewport(state.mapWindowReducer.activeCard));
    const [enumDetails] = useState({
        permissions: getEnumDetailsList(MapCore.IMcEditMode.EPermission),
    });
    const [permissionsDetailsList, setPermissionsDetailsList] = useState<PermissionDetails[]>([]);
    const internalUseOverlayRef = useRef<MapCore.IMcOverlay>(null);
    const permissionsColorsMap = useMemo(() => (new Map([
        [MapCore.IMcEditMode.EPermission.EEMP_MOVE_VERTEX, new MapCore.SMcBColor(0, 255, 0, 255)],
        [MapCore.IMcEditMode.EPermission.EEMP_BREAK_EDGE, new MapCore.SMcBColor(255, 0, 0, 255)],
        [MapCore.IMcEditMode.EPermission.EEMP_RESIZE, new MapCore.SMcBColor(0, 0, 255, 255)],
        [MapCore.IMcEditMode.EPermission.EEMP_ROTATE, new MapCore.SMcBColor(255, 255, 0, 255)],
        [MapCore.IMcEditMode.EPermission.EEMP_DRAG, new MapCore.SMcBColor(255, 0, 255, 255)],
        [MapCore.IMcEditMode.EPermission.EEMP_FINISH_TEXT_STRING_BY_KEY, new MapCore.SMcBColor(0, 255, 255, 255)],
    ])), [])
    //#region useEffect
    useEffect(() => {
        return () => {
            runCodeSafely(() => {
                if (internalUseOverlayRef.current) {
                    runMapCoreSafely(() => { internalUseOverlayRef.current.Remove(); }, 'EditModePropertiesDialog/Permissions.useEffect[mounting] => IMcOverlay.Remove', true);
                }
            }, "EditModePropertiesDialog/Permissions.useEffect[mounting]")
        }
    }, [])
    useEffect(() => {
        runCodeSafely(() => {
            const permissionsList = enumDetails.permissions.filter(p => { return p.theEnum != MapCore.IMcEditMode.EPermission.EEMP_NONE }).map(permission => {
                let hiddenIconsList: Uint32Array;
                for (let i = 0; i < props.dialogProperties.HiddenIconsPerPermissions.length; ++i) {
                    if (permission.theEnum === props.dialogProperties.HiddenIconsPerPermissions[i].ePermission) {
                        hiddenIconsList = props.dialogProperties.HiddenIconsPerPermissions[i].auIconIndices;
                        break;
                    }
                }
                const isAuthorized = props.dialogProperties.Permissions.some(p => _.isEqual(p, permission));
                const currentShowIconValue = props.dialogProperties.isShowIconsMap.get(permission.theEnum);
                let permissionDetails = { permission: permission, enabled: isAuthorized, hiddenIconsList: hiddenIconsList, isShowIcon: currentShowIconValue };
                return permissionDetails;
            })
            setPermissionsDetailsList(permissionsList)
        }, "EditModePropertiesDialog/Permissions.useEffect")
    }, [props.dialogProperties.Permissions, props.dialogProperties.HiddenIconsPerPermissions, props.dialogProperties.isShowIconsMap])
    useEffect(() => {
        let cancelled = false;
        runAsyncCodeSafely(async () => {
            if (activeViewport) {
                //remove old objects
                if (internalUseOverlayRef.current) {
                    const overlayObjects = internalUseOverlayRef.current.GetObjects();
                    overlayObjects.map(object => {
                        runMapCoreSafely(() => { object.Remove(); }, 'EditModePropertiesDialog/Permissions.handleIsShowIconsCheckboxChange => IMcObject.Remove', true);
                    })
                }
                else {
                    runMapCoreSafely(() => {
                        internalUseOverlayRef.current = MapCore.IMcOverlay.Create(activeViewport.viewport.GetOverlayManager(), true)
                    }, 'EditModePropertiesDialog/Permissions.handleIsShowIconsCheckboxChange => IMcOverlay.Create', true);
                }

                //generate updated objects
                let iconPositions: MapCore.IMcEditMode.SIconPosition[] = [];
                runMapCoreSafely(() => {
                    iconPositions = activeViewport.editMode.GetIconsScreenPositions();
                }, 'EditModePropertiesDialog/Permissions.handleIsShowIconsCheckboxChange => IMcEditMode.GetIconsScreenPositions', true);
                const checkedMcEnumPermissions = permissionsDetailsList.filter((p: PermissionDetails) => p.isShowIcon).map(p => p.permission.theEnum);
                const filteredIconsPositions = iconPositions.filter(icon => checkedMcEnumPermissions.includes(icon.ePermission));
                const screenPointsObjectScheme: MapCore.IMcObjectScheme = filteredIconsPositions.length ? await screenPositionService.getObjectSchemeScreenPoints(activeViewport.viewport, internalUseOverlayRef.current) : null;
                if (cancelled) return

                filteredIconsPositions.forEach((iconPosition: MapCore.IMcEditMode.SIconPosition, i) => {
                    let finalIconTextColor: MapCore.SMcBColor = permissionsColorsMap.get(iconPosition.ePermission);
                    if (checkedMcEnumPermissions.length == 1) {//single permission checked
                        finalIconTextColor = iconPosition.bIsActive ? new MapCore.SMcBColor(0, 255, 0, 255) : new MapCore.SMcBColor(255, 0, 0, 255);
                    }
                    screenPositionService.createScreenPointObject(internalUseOverlayRef, screenPointsObjectScheme, iconPosition.ScreenPosition, finalIconTextColor, iconPosition.uIndex.toString())
                })
            }
        }, "EditModePropertiesDialog/Permissions.useEffect")

        return () => {
            cancelled = true;
        }
    }, [props.dialogProperties.isShowIconsMap, permissionsDetailsList])
    //#endregion
    //#region Handle Functions
    const handleEnablePermissionCheckedChange = (e: CheckboxChangeEvent) => {
        runCodeSafely(() => {
            if (e.target.checked) {
                for (let i = 0; i < enumDetails.permissions.length; ++i) {
                    if (e.target.name == enumDetails.permissions[i].name) {
                        props.setDialogPropertiesCallback("Permissions", [...props.dialogProperties.Permissions, enumDetails.permissions[i]])
                        break;
                    }
                }
            }
            else {
                let newPermissions = [...props.dialogProperties.Permissions];
                for (let i = 0; i < newPermissions.length; ++i) {
                    if (e.target.name == newPermissions[i].name) {
                        newPermissions.splice(i, 1);
                        props.setDialogPropertiesCallback("Permissions", newPermissions)
                        break;
                    }
                }
            }
        }, "EditModePropertiesDialog/Permissions.handleEnablePermissionCheckedChange")
    }
    const handleRootShowIconsCheckboxChange = (e: CheckboxChangeEvent) => {
        runCodeSafely(() => {
            let newShowIconsMap = new Map(props.dialogProperties.isShowIconsMap);
            Array.from(newShowIconsMap.keys()).forEach(key => {
                newShowIconsMap.set(key, e.checked);
            });
            props.setDialogPropertiesCallback("isShowIconsMap", newShowIconsMap)
        }, "EditModePropertiesDialog/Permissions.handleRootShowIconsCheckboxChange")
    }
    const handleIsShowIconCheckedChange = (e: CheckboxChangeEvent) => {
        runCodeSafely(() => {
            const permissionEnum = enumDetails.permissions.find(permission => permission.name == e.target.name)?.theEnum;
            const newShowIconsMap = new Map(props.dialogProperties.isShowIconsMap).set(permissionEnum, e.checked);
            props.setDialogPropertiesCallback("isShowIconsMap", newShowIconsMap)
        }, "EditModePropertiesDialog/Permissions.handleIsShowIconCheckedChange")
    }
    const handleHiddenIconsListValueChange = (e: any) => {
        runCodeSafely(() => {
            const { rowData, newValue } = e;
            const updated = [...props.dialogProperties.HiddenIconsPerPermissions];

            const iconIndices = Array.isArray(newValue) ? newValue :
                newValue.trim().split(/\s+/).map(str => {
                    const num = parseInt(str, 10);
                    if (isNaN(num)) throw new Error(`Invalid number: ${str}`);
                    return num;
                });

            const target = updated.find(p => _.isEqual(p.ePermission, rowData.permission.theEnum));
            if (target) {
                target.auIconIndices = new Uint32Array(iconIndices);
            }
            props.setDialogPropertiesCallback("HiddenIconsPerPermissions", updated);
        }, "EditModePropertiesDialog/Permissions.handleHiddenIconsListValueChange");
    };

    //#endregion
    //#region DOM Functions
    const getHiddenIconsListEditor = (props: any) => {
        return (
            <InputText type="text" className='em-props__input-field' name={props.rowData.permission.name} placeholder='e.g. 0 1 3'
                disabled={props.rowData.permission.theEnum == MapCore.IMcEditMode.EPermission.EEMP_FINISH_TEXT_STRING_BY_KEY}
                value={props.value}
                onChange={(e) => props.editorCallback(e.target.value)}
            />
        );
    };
    const getHiddenIconsListString = (rowData: any) => {
        let hiddenIconsStr = '';
        if (rowData.hiddenIconsList) {
            let numArray: number[] = Array.from(rowData.hiddenIconsList);
            hiddenIconsStr = numArray.join(" ");
        }
        return <InputText className='em-props__input-field' disabled={rowData.permission.theEnum == MapCore.IMcEditMode.EPermission.EEMP_FINISH_TEXT_STRING_BY_KEY} value={hiddenIconsStr} />
    };
    const getShowIconsHeader = useCallback(() => {
        const isAllShowIconsTrue = Array.from(props.dialogProperties.isShowIconsMap.values()).every(Boolean);

        return (
            <div className="form__flex-and-row-between form__items-center">
                <Checkbox checked={isAllShowIconsTrue} onChange={handleRootShowIconsCheckboxChange} />
                <div>Show Icons Screen Positions</div>
            </div>
        );
    }, [props.dialogProperties.isShowIconsMap]);

    //#endregion
    return (<div className='form__column-container'>
        <Fieldset className="form__space-between form__column-fieldset" legend="Permissions">
            <DataTable size='small' value={permissionsDetailsList} >
                <Column field="enabled" header="Enable" body={(rowData: any) => <Checkbox name={rowData.permission.name} onChange={handleEnablePermissionCheckedChange} checked={rowData.enabled} />} />
                <Column field="permission" header="Permission" body={(rowData: any) => { return rowData.permission.name }} />
                <Column field="hiddenIconsList" header="Hidden Icons" body={(rowData: any) => { return getHiddenIconsListString(rowData) }} onCellEditComplete={handleHiddenIconsListValueChange} editor={getHiddenIconsListEditor} />
                <Column field="isShowIcons" header={getShowIconsHeader()} body={(rowData: any) => <Checkbox name={rowData.permission.name} onChange={handleIsShowIconCheckedChange} checked={rowData.isShowIcon} />} />
            </DataTable>
        </Fieldset>
    </div >)
};