import { Button } from "primereact/button";
import { Fieldset } from "primereact/fieldset";
import { ListBox } from "primereact/listbox";
import { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { Checkbox } from "primereact/checkbox";
import { InputNumber } from "primereact/inputnumber";

import { getEnumDetailsList } from 'mapcore-lib';
import { OverlayFormTabInfo } from "./overlayForm";
import { Properties } from "../../../dialog";
import ColorPickerCtrl from "../../../../shared/colorPicker";
import TreeNodeModel from "../../../../shared/models/tree-node.model"
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";
import { AppState } from "../../../../../redux/combineReducer";
import objectWorldTreeService from "../../../../../services/objectWorldTree.service";
import generalService from "../../../../../services/general.service";
import dialogStateService from "../../../../../services/dialogStateService";

export class ColorOverridingPropertiesState implements Properties {
    colorComponentFlagArr: any[];

    static getDefault(props: any): ColorOverridingPropertiesState {
        return {
            colorComponentFlagArr: handleSelectedViewportChange(props.props.tabInfo.currentOverlay),
        }
    }
}

export class ColorOverridingProperties extends ColorOverridingPropertiesState {
    viewportsOfOMArr: any[];
    viewportToSave: any;
    //color overriding Fields

    static getDefault(props: any): ColorOverridingProperties {

        let stateDefaults = super.getDefault(props);
        let defaults: ColorOverridingProperties = {
            ...stateDefaults,
            viewportsOfOMArr: Array.from(objectWorldTreeService.getOMMCViewportsByOverlay(props.treeRedux, props.props.tabInfo.currentOverlay)).map(vp => { return { viewport: vp.viewport, label: generalService.getObjectName(vp, "Viewport") } }),
            viewportToSave: null,
        }

        return defaults;
    }
};

const handleSelectedViewportChange = (currentOverlay: TreeNodeModel, vp: MapCore.IMcMapViewport = null) => {
    let EColorComponentFlag = getEnumDetailsList(MapCore.IMcOverlay.EColorComponentFlags);
    let EColorPropertyType = getEnumDetailsList(MapCore.IMcOverlay.EColorPropertyType);

    let propertiesOverriding: MapCore.IMcOverlay.SColorPropertyOverriding[] = currentOverlay.nodeMcContent?.GetColorOverriding(vp);
    let finalColorComponentFlagArr: any[] = [];
    propertiesOverriding && propertiesOverriding.forEach((enumProp: MapCore.IMcOverlay.SColorPropertyOverriding, ind: number) => {
        let colorRGBObj = enumProp.Color
        let compFlag = enumProp.uColorComponentsBitField;
        let enumValues = {};
        EColorComponentFlag.forEach(en => {
            if (en.theEnum != MapCore.IMcOverlay.EColorComponentFlags.ECCF_NONE &&
                en.theEnum != MapCore.IMcOverlay.EColorComponentFlags.ECCF_RGB_FLAGS &&
                en.theEnum != MapCore.IMcOverlay.EColorComponentFlags.ECCF_ALPHA_FLAGS) {
                enumValues[en.name] = ((compFlag & en.code) == en.code);
            }
        });

        let colorComponentFlag: any = {
            index: ind,
            ECPTTypes: EColorPropertyType[ind],
            enable: enumProp.bEnabled,
            colorRGB: { r: colorRGBObj.r, g: colorRGBObj.g, b: colorRGBObj.b },
            colorAlpha: colorRGBObj.a,
            ...enumValues,
        };
        finalColorComponentFlagArr = [...finalColorComponentFlagArr, colorComponentFlag]
    });
    return finalColorComponentFlagArr;
}

export default function ColorOverriding(props: { tabInfo: OverlayFormTabInfo }) {
    let treeRedux = useSelector((state: AppState) => state.objectWorldTreeReducer.objectWorldTree);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    let currentOverlay = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);

    const [enumDetails] = useState({
        EColorComponentFlag: getEnumDetailsList(MapCore.IMcOverlay.EColorComponentFlags),
        EColorPropertyType: getEnumDetailsList(MapCore.IMcOverlay.EColorPropertyType),
    });

    const [isMountedUseEffect, setIsMountedUseEffect] = useState({
        currentOverlay: false,
        treeRedux: false,
    })

    useEffect(() => {
        runCodeSafely(() => {
            // initialize first time the tab is loaded only
            if (!props.tabInfo.properties.colorOverridingProperties) {
                props.tabInfo.setInitialStatePropertiesCallback("colorOverridingProperties", null, ColorOverridingPropertiesState.getDefault({ props, treeRedux }));
                props.tabInfo.setPropertiesCallback("colorOverridingProperties", null, ColorOverridingProperties.getDefault({ props, treeRedux }));
            }
        }, 'OverlayForm/ColorOverriding.useEffect');
    }, [])
    // useEffect(() => {
    //     runCodeSafely(() => {
    //         props.tabInfo.setApplyCallBack("ColorOverriding", applyAll);
    //     }, 'OverlayForm/ColorOverriding.useEffect => properties');
    // }, [props.tabInfo.properties])
    const getFinalEnumHeader = (enumName: string) => {
        let noPrefixName = enumName.substring(enumName.indexOf('_') + 1);
        let nameArr = noPrefixName.split('_')
        let x = nameArr[0].toLowerCase()
        let finalNameArr: string[] = nameArr.map(n => {
            let lowerName = n.toLowerCase();
            return n.substring(0, 1) + lowerName.substring(1);
        })
        let finalName = finalNameArr.join(' ');
        return finalName;
    }
    const getDynamicColumns = () => {
        const colorOverridingColumns = enumDetails.EColorComponentFlag?.map(en => {
            if (en.theEnum != MapCore.IMcOverlay.EColorComponentFlags.ECCF_NONE &&
                en.theEnum != MapCore.IMcOverlay.EColorComponentFlags.ECCF_RGB_FLAGS &&
                en.theEnum != MapCore.IMcOverlay.EColorComponentFlags.ECCF_ALPHA_FLAGS) {
                return { field: en.name, header: getFinalEnumHeader(en.name) }
            }
        }).flat();
        let finalColumns = [
            { field: 'ECPTTypes', header: 'ECPT Types' },
            { field: 'enable', header: 'Enable' },
            { field: 'colorRGB', header: 'Color RGB' },
            { field: 'colorAlpha', header: 'Color Alpha' },
            ...colorOverridingColumns.filter(col => col !== undefined),
        ]
        return finalColumns;
    }

    useEffect(() => {
        runCodeSafely(() => {
            if (isMountedUseEffect.treeRedux) {
                props.tabInfo.setPropertiesCallback("viewportsOfOMArr", null, Array.from(objectWorldTreeService.getOMMCViewportsByOverlay(treeRedux, currentOverlay)).map(vp => { return { viewport: vp.viewport, label: generalService.getObjectName(vp, "Viewport") } }));
            }
            else {
                setIsMountedUseEffect({ ...isMountedUseEffect, treeRedux: true })
            }
        }, 'overlayForms/ColorOverriding.useEffect => treeRedux');
    }, [treeRedux])
    useEffect(() => {
        runCodeSafely(() => {
            if (isMountedUseEffect.currentOverlay) {
                props.tabInfo.setInitialStatePropertiesCallback("colorOverridingProperties", null, ColorOverridingPropertiesState.getDefault({ props, treeRedux }));
                props.tabInfo.setPropertiesCallback("colorOverridingProperties", null, ColorOverridingProperties.getDefault({ props, treeRedux }));
            }
            else {
                setIsMountedUseEffect({ ...isMountedUseEffect, currentOverlay: true })
            }
        }, 'overlayForms/ColorOverriding.useEffect => currentOverlay');
    }, [currentOverlay])

    const saveData = (event: any) => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback("colorOverridingProperties", event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
            if (event.target.name in ColorOverridingPropertiesState.getDefault({ props, treeRedux })) {
                props.tabInfo.setCurrStatePropertiesCallback("colorOverridingProperties", event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value)
            }
        }, "OverlayForms/objectChanging.saveData => onChange")
    }

    const handleClearSelectedVps = () => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback("viewportToSave", null, null);
        }, 'overlayForms/General.useEffect => treeRedux, currentOverlay');
    }
    const handleSetColorOverridingOK = () => {
        let colorPropOverridingArr: MapCore.IMcOverlay.SColorPropertyOverriding[] = [];
        props.tabInfo.properties.colorOverridingProperties?.colorComponentFlagArr.forEach((colorCompFlag) => {
            let colorPropOverriding = new MapCore.IMcOverlay.SColorPropertyOverriding();
            colorPropOverriding.Color.a = colorCompFlag.colorAlpha;
            colorPropOverriding.Color.r = colorCompFlag.colorRGB.r;
            colorPropOverriding.Color.g = colorCompFlag.colorRGB.g;
            colorPropOverriding.Color.b = colorCompFlag.colorRGB.b;

            colorPropOverriding.bEnabled = colorCompFlag.enable;

            let colorCompBitField = 0;
            enumDetails.EColorComponentFlag.forEach(col => {
                if (colorCompFlag[col.name] === true) {
                    colorCompBitField = colorCompBitField | col.code;
                }
            })
            console.log(colorCompBitField);


            colorPropOverriding.uColorComponentsBitField = colorCompBitField;
            colorPropOverridingArr = [...colorPropOverridingArr, colorPropOverriding]
        })
        let currentMCOverlay = currentOverlay.nodeMcContent as MapCore.IMcOverlay;
        if (props.tabInfo.properties.colorOverridingProperties?.viewportToSave) {
            currentMCOverlay.SetColorOverriding(colorPropOverridingArr, props.tabInfo.properties.colorOverridingProperties?.viewportToSave?.viewport);
        }
        else {
            currentMCOverlay.SetColorOverriding(colorPropOverridingArr);
        }
        console.log('ok')
        dialogStateService.applyDialogState(["colorOverridingProperties.colorComponentFlagArr"]);
    }
    const onCellChange = (eValue: any, rowData: any, column: any) => {
        let copiedArr = [...props.tabInfo.properties.colorOverridingProperties?.colorComponentFlagArr];
        let newCurrObj = { ...copiedArr[rowData.index], [column.field]: eValue }
        copiedArr[rowData.index] = newCurrObj;
        props.tabInfo.setPropertiesCallback("colorOverridingProperties", "colorComponentFlagArr", copiedArr);
        props.tabInfo.setCurrStatePropertiesCallback("colorOverridingProperties", "colorComponentFlagArr", copiedArr);
    }
    const getColumnTemplate = (rowData: any, column: any) => {
        switch (column.field) {
            case 'ECPTTypes':
                return <span style={{ minWidth: `${globalSizeFactor * 40.5}vh` }} >{rowData[column.field]?.name}</span>
            case 'colorRGB':
                return <ColorPickerCtrl name='colorRGB' value={rowData[column.field]} onChange={(e) => { onCellChange(e.value, rowData, column) }} />
            case 'colorAlpha':
                return <InputNumber name='colorAlpha' className="form__narrow-input" value={rowData[column.field]} onChange={(e) => { onCellChange(e.value, rowData, column) }} />
            default:
                return <Checkbox checked={rowData[column.field]} onChange={(e) => { onCellChange(e.checked, rowData, column) }} />;
        }
    }

    return (
        <Fieldset className="form__space-between form__row-fieldset" legend="Color Overriding">
            <div style={{ display: 'flex', flexDirection: 'column', width: `${globalSizeFactor * 1.5 * 25}vh` }}>
                <DataTable showGridlines scrollable scrollHeight={`${globalSizeFactor * 55}vh`} size='small' value={props.tabInfo.properties.colorOverridingProperties?.colorComponentFlagArr} editMode="cell">
                    {getDynamicColumns()?.map(({ field, header }) => {
                        return <Column style={{ minWidth: `${field == 'ECPTTypes' ? `${globalSizeFactor * 40.5}vh` : `${header.length * 1.5}vh`}` }} key={header} field={field} header={header} body={getColumnTemplate}></Column>
                    })}
                </DataTable>
                <div style={{ display: 'flex', justifyContent: 'flex-end', padding: `${globalSizeFactor * 1.5 * 0.5}vh` }}>
                    <Button style={{ margin: `${globalSizeFactor * 0.3}vh` }} label="Get" onClick={(e) => {
                        let selectedVpColorComponentFlagArr = handleSelectedViewportChange(currentOverlay, props.tabInfo.properties.colorOverridingProperties?.viewportToSave?.viewport);
                        props.tabInfo.setPropertiesCallback("colorOverridingProperties", "colorComponentFlagArr", selectedVpColorComponentFlagArr);
                        props.tabInfo.setInitialStatePropertiesCallback("colorOverridingProperties", "colorComponentFlagArr", selectedVpColorComponentFlagArr);
                        props.tabInfo.setCurrStatePropertiesCallback("colorOverridingProperties", "colorComponentFlagArr", selectedVpColorComponentFlagArr);
                    }} />
                    <Button style={{ margin: `${globalSizeFactor * 0.3}vh` }} label="OK" onClick={handleSetColorOverridingOK} />
                </div>
            </div>
            <div style={{ width: `${globalSizeFactor * 1.5 * 10}vh` }}>
                <ListBox listStyle={{ minHeight: `${globalSizeFactor * 55}vh`, maxHeight: `${globalSizeFactor * 55}vh` }} name='viewportToSave' value={props.tabInfo.properties.colorOverridingProperties?.viewportToSave} onChange={saveData} optionLabel='label' options={props.tabInfo.properties.colorOverridingProperties?.viewportsOfOMArr} />
                <div style={{ display: 'flex', justifyContent: 'flex-end' }}>
                    <Button label='Clear' onClick={handleClearSelectedVps} />
                </div>
            </div>
        </Fieldset>
    )
}
