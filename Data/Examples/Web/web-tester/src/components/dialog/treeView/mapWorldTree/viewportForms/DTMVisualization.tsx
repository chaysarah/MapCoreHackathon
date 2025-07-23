// External libraries
import { useState, useEffect, ReactElement } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import _ from "lodash";

// UI/component libraries
import { Fieldset } from "primereact/fieldset";
import { Checkbox } from "primereact/checkbox";
import { InputNumber } from "primereact/inputnumber";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { Button } from "primereact/button";

// Project-specific imports
import "./styles/viewport.css";
import { MapViewportFormTabInfo } from "./mapViewportForm";
import { Properties } from "../../../dialog";
import ColorPickerCtrl from "../../../../shared/colorPicker";
import { AppState } from "../../../../../redux/combineReducer";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";
import dialogStateService from "../../../../../services/dialogStateService";

class VisualHeightColor {
    Color: MapCore.SMcBColor;
    Alpha: number;
    HeightInSteps: number;
}

export class DTMVisualizationPropertiesState implements Properties {
    isEnable: boolean;
    dtmVisualizationParams: MapCore.IMcMapViewport.SDtmVisualizationParams
    isDtmTransparencyWithoutRaster: boolean;


    static getDefault(p: any): DTMVisualizationPropertiesState {
        let { selectedNodeInTree } = p;
        let viewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;

        let pParams: any = {}
        let tmpIsEnabled = viewport.GetDtmVisualization(pParams)
        let tmpDtmVisualizationParams: MapCore.IMcMapViewport.SDtmVisualizationParams = pParams.Value;

        return {
            isEnable: tmpIsEnabled,
            dtmVisualizationParams: tmpDtmVisualizationParams,

            isDtmTransparencyWithoutRaster: false,
        }
    }
}
export class DTMVisualizationProperties extends DTMVisualizationPropertiesState {
    // Set Default Height Colors
    minHeight: number;
    maxHeight: number;
    heightColorsVisualTable: VisualHeightColor[];
    static getDefault(p: any): DTMVisualizationProperties {
        let stateDefaults = super.getDefault(p);

        let tmpMinHeight = -500, tmpMaxHeight = 9500;
        if (stateDefaults.dtmVisualizationParams.aHeightColors != null && stateDefaults.dtmVisualizationParams.aHeightColors.length != 0) {
            tmpMinHeight = stateDefaults.dtmVisualizationParams.fHeightColorsHeightOrigin +
                stateDefaults.dtmVisualizationParams.aHeightColors[0].nHeightInSteps *
                stateDefaults.dtmVisualizationParams.fHeightColorsHeightStep;
            tmpMaxHeight = stateDefaults.dtmVisualizationParams.fHeightColorsHeightOrigin +
                stateDefaults.dtmVisualizationParams.aHeightColors[stateDefaults.dtmVisualizationParams.aHeightColors.length - 1].nHeightInSteps *
                stateDefaults.dtmVisualizationParams.fHeightColorsHeightStep;
        }

        let defaults: DTMVisualizationProperties = {
            ...stateDefaults,
            heightColorsVisualTable: stateDefaults.dtmVisualizationParams.aHeightColors.map(
                HeightColor => {
                    return { Color: HeightColor.Color, Alpha: HeightColor.Color.a, HeightInSteps: HeightColor.nHeightInSteps };
                })
                .concat(
                    [{ Color: new MapCore.SMcBColor(), Alpha: 255, HeightInSteps: 0 }]
                ),
            minHeight: tmpMinHeight,
            maxHeight: tmpMaxHeight,
        }
        return defaults;
    }
};

export default function DTMVisualization(props: { tabInfo: MapViewportFormTabInfo }) {
    const dispatch = useDispatch();
    let treeRedux = useSelector((state: AppState) => state.mapWorldTreeReducer.mapWorldTree);
    let selectedNodeInTree = useSelector((state: AppState) => state.mapWorldTreeReducer.selectedNodeInTree);
    let viewport: MapCore.IMcMapViewport = props.tabInfo.currentViewport.nodeMcContent;
    let defaultDTMVisualizationPropertiesState: DTMVisualizationPropertiesState = DTMVisualizationPropertiesState.getDefault({ props, treeRedux, selectedNodeInTree })
    const [isMountedUseEffect, setIsMountedUseEffect] = useState({
        DTMVisualizationProperties: false,
        currentViewport: false,
        heightColorsVisualTable: false,
        aHeightColors: false,
    })
    //UseEffects
    useEffect(() => {
        runCodeSafely(() => {
            // initialize first time the tab is loaded only
            if (!props.tabInfo.properties.DTMVisualizationProperties) {
                props.tabInfo.setInitialStatePropertiesCallback("DTMVisualizationProperties", null, DTMVisualizationPropertiesState.getDefault({ props, treeRedux, selectedNodeInTree }));
                props.tabInfo.setPropertiesCallback("DTMVisualizationProperties", null, DTMVisualizationProperties.getDefault({ props, treeRedux, selectedNodeInTree }));
            }
        }, 'MapViewportForm/DTMVisualization.useEffect');
    }, [])

    useEffect(() => {
        runCodeSafely(() => {
            props.tabInfo.setApplyCallBack("DTMVisualization", applyAll);
        }, 'MapViewportForm/DTMVisualization.useEffect => DTMVisualizationProperties');
    }, [props.tabInfo.properties.DTMVisualizationProperties])

    useEffect(() => {
        runCodeSafely(() => {
            if (isMountedUseEffect.currentViewport) {
                // set to default values
                props.tabInfo.setInitialStatePropertiesCallback("DTMVisualizationProperties", null, DTMVisualizationPropertiesState.getDefault({ props, treeRedux, selectedNodeInTree }));
                props.tabInfo.setPropertiesCallback("DTMVisualizationProperties", null, DTMVisualizationProperties.getDefault({ props, treeRedux, selectedNodeInTree }));
            }
            else {
                setIsMountedUseEffect({ ...isMountedUseEffect, currentViewport: true })
            }
        }, 'MapViewportForm/DTMVisualization.useEffect => currentViewport');
    }, [props.tabInfo.currentViewport])

    useEffect(() => {
        runCodeSafely(() => {
            if (isMountedUseEffect.heightColorsVisualTable) {
                let updatedDTMVisualization = { ...props.tabInfo.properties.DTMVisualizationProperties?.dtmVisualizationParams }
                // updatedDTMVisualization.aHeightColors = _.cloneDeep(props.tabInfo.properties.DTMVisualizationProperties.heightColorsVisualTable)
                updatedDTMVisualization.aHeightColors = props.tabInfo.properties.DTMVisualizationProperties.heightColorsVisualTable
                    .map(
                        visualHeightColor => {
                            let color = visualHeightColor.Color;
                            color.a = visualHeightColor.Alpha;
                            return { Color: color, nHeightInSteps: visualHeightColor.HeightInSteps }
                        }
                    ).slice(0, -1);
                props.tabInfo.setPropertiesCallback("DTMVisualizationProperties", "dtmVisualizationParams", updatedDTMVisualization);
                props.tabInfo.setCurrStatePropertiesCallback("DTMVisualizationProperties", "dtmVisualizationParams", updatedDTMVisualization);
            }
            else {
                setIsMountedUseEffect({ ...isMountedUseEffect, heightColorsVisualTable: true })
            }
        }, 'MapViewportForm/DTMVisualization.useEffect => heightColorsVisualTable');
    }, [props.tabInfo.properties.DTMVisualizationProperties?.heightColorsVisualTable])

    useEffect(() => {
        runCodeSafely(() => {
            if (isMountedUseEffect.aHeightColors) {
                if (!_.isEqual(props.tabInfo.properties.DTMVisualizationProperties.dtmVisualizationParams.aHeightColors,
                    props.tabInfo.properties.DTMVisualizationProperties?.heightColorsVisualTable.slice(0, -1))) {
                    let updatedHeightColorsVisualTable: VisualHeightColor[] = props.tabInfo.properties.DTMVisualizationProperties.dtmVisualizationParams.aHeightColors.map(
                        HeightColor => {
                            return { Color: HeightColor.Color, Alpha: HeightColor.Color.a, HeightInSteps: HeightColor.nHeightInSteps };
                        })
                        .concat(
                            [{ Color: new MapCore.SMcBColor(), Alpha: 255, HeightInSteps: 0 }]
                        )
                    props.tabInfo.setPropertiesCallback("DTMVisualizationProperties", "heightColorsVisualTable", updatedHeightColorsVisualTable);
                }
            }
            else {
                setIsMountedUseEffect({ ...isMountedUseEffect, aHeightColors: true })
            }
        }, 'MapViewportForm/DTMVisualization.useEffect => aHeightColors');
    }, [props.tabInfo.properties.DTMVisualizationProperties?.dtmVisualizationParams?.aHeightColors])

    const saveData = (event: any) => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback("DTMVisualizationProperties", event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
            if (event.target.name in defaultDTMVisualizationPropertiesState) {
                props.tabInfo.setCurrStatePropertiesCallback("DTMVisualizationProperties", event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
            }
        }, "MapViewportForm/DTMVisualization.saveData => onChange")
    }

    const saveDTMVisualization = (event: any) => {
        runCodeSafely(() => {
            let updatedDTMVisualization = { ...props.tabInfo.properties.DTMVisualizationProperties.dtmVisualizationParams }
            updatedDTMVisualization[event.target.name] = event.target.type === "checkbox" ? event.target.checked : event.target.value;
            props.tabInfo.setPropertiesCallback("DTMVisualizationProperties", "dtmVisualizationParams", updatedDTMVisualization);
            props.tabInfo.setCurrStatePropertiesCallback("DTMVisualizationProperties", "dtmVisualizationParams", updatedDTMVisualization);
        }, "MapViewportForm/DTMVisualization.saveDTMVisualization => onChange")
    }

    const onHeightColorsVisualTableChange = (eValue: any, rowIndex: number, field: string): void => {
        let updatedHeightColorsVisualTable = [...props.tabInfo.properties.DTMVisualizationProperties.heightColorsVisualTable]
        let newValue = { ...updatedHeightColorsVisualTable[rowIndex], [field]: eValue }
        if (rowIndex === updatedHeightColorsVisualTable.length - 1) {
            updatedHeightColorsVisualTable.push({ Color: new MapCore.SMcBColor(), Alpha: 255, HeightInSteps: 0 })
        }
        updatedHeightColorsVisualTable[rowIndex] = newValue;
        props.tabInfo.setPropertiesCallback("DTMVisualizationProperties", "heightColorsVisualTable", updatedHeightColorsVisualTable);
    }
    const handleSetDefaultHeightColors = () => {
        let defaultParams: MapCore.IMcMapViewport.SDtmVisualizationParams = { ...props.tabInfo.properties.DTMVisualizationProperties.dtmVisualizationParams }
        runMapCoreSafely(() => {
            MapCore.IMcMapViewport.SDtmVisualizationParams.SetDefaultHeightColors(defaultParams,
                props.tabInfo.properties.DTMVisualizationProperties.minHeight,
                props.tabInfo.properties.DTMVisualizationProperties.maxHeight)
        }, "MapViewportForm/DTMVisualization.handleSetDefaultHeightColors => SetDefaultHeightColors", true)
        props.tabInfo.setPropertiesCallback("DTMVisualizationProperties", "dtmVisualizationParams", defaultParams);
        props.tabInfo.setCurrStatePropertiesCallback("DTMVisualizationProperties", "dtmVisualizationParams", defaultParams);
    }
    const handleDtmTransparencyWithoutRasterOK = () => {
        runMapCoreSafely(() => {
            viewport.SetDtmTransparencyWithoutRaster(props.tabInfo.properties.DTMVisualizationProperties.isDtmTransparencyWithoutRaster)
        }, "MapViewportForm/DTMVisualization.handleDtmTransparencyWithoutRasterOK => SetDtmTransparencyWithoutRaster", true)
        dialogStateService.applyDialogState([
            "DTMVisualizationProperties.isDtmTransparencyWithoutRaster",
        ])
    }
    const handleSetDTMVisualization = () => {
        runMapCoreSafely(() => {
            viewport.SetDtmVisualization(props.tabInfo.properties.DTMVisualizationProperties.isEnable,
                props.tabInfo.properties.DTMVisualizationProperties.dtmVisualizationParams)
        }, "MapViewportForm/DTMVisualization.handleSetDTMVisualization => SetDtmVisualization", true)
        dialogStateService.applyDialogState([
            "DTMVisualizationProperties.isEnable",
            "DTMVisualizationProperties.dtmVisualizationParams",
        ])
    }
    const handleResetForm = () => {
        runCodeSafely(() => {
            props.tabInfo.setInitialStatePropertiesCallback("DTMVisualizationProperties", null, DTMVisualizationPropertiesState.getDefault({ props, treeRedux, selectedNodeInTree }));
            props.tabInfo.setPropertiesCallback("DTMVisualizationProperties", null, DTMVisualizationProperties.getDefault({ props, treeRedux, selectedNodeInTree }));
        }, "MapViewportForm/DTMVisualization.handleResetForm => onClick")
    }
    const applyAll = () => {
        runCodeSafely(() => {

        }, 'MapViewportForm/DTMVisualization.applyAll');
    }
    //DOM Functions

    return (
        <div className="form__column-container">
            <div className="form__center-aligned-row">
                <Checkbox name="isEnable" inputId="isEnable" checked={props.tabInfo.properties.DTMVisualizationProperties?.isEnable} onChange={saveData} />
                <label htmlFor="isEnable">Is Enable</label>
            </div>
            <Fieldset className={`form__column-fieldset ${props.tabInfo.properties.DTMVisualizationProperties?.isEnable ? "" : "form__disabled"}`}
                legend="DTM Visualization">
                <div className="form__row-container">
                    <div className="form__column-container">
                        {/* <div className="form__center-aligned-row">
                        <Checkbox name="isEnable" inputId="isEnable" checked={props.tabInfo.properties.DTMVisualizationProperties?.isEnable} onChange={saveData} />
                        <label htmlFor="isEnable">Is Enable</label>
                    </div> */}
                        <div className="form__center-aligned-row">
                            <Checkbox name="bHeightColorsInterpolation" inputId="isEnable" checked={props.tabInfo.properties.DTMVisualizationProperties?.dtmVisualizationParams?.bHeightColorsInterpolation} onChange={saveDTMVisualization} />
                            <label htmlFor="bHeightColorsInterpolation">Height Colors Interpolation</label>
                        </div>
                        <div className="form__center-aligned-row">
                            <Checkbox name="bDtmVisualizationAboveRaster" inputId="isEnable" checked={props.tabInfo.properties.DTMVisualizationProperties?.dtmVisualizationParams?.bDtmVisualizationAboveRaster} onChange={saveDTMVisualization} />
                            <label htmlFor="bDtmVisualizationAboveRaster">Dtm Visualization Above Raster</label>
                        </div>
                        <div className="form__center-aligned-row">
                            <label htmlFor='fHeightColorsHeightOrigin'>Height Colors Height Origin:</label>
                            <InputNumber name='fHeightColorsHeightOrigin' id='fHeightColorsHeightOrigin' className="form__medium-width-input" value={props.tabInfo.properties.DTMVisualizationProperties?.dtmVisualizationParams?.fHeightColorsHeightOrigin ?? null} onValueChange={saveDTMVisualization} />
                        </div>
                        <div className="form__center-aligned-row">
                            <label htmlFor='fHeightColorsHeightStep'>Height Colors Height Step:</label>
                            <InputNumber name='fHeightColorsHeightStep' id='fHeightColorsHeightOrigin' className="form__medium-width-input" value={props.tabInfo.properties.DTMVisualizationProperties?.dtmVisualizationParams?.fHeightColorsHeightStep ?? null} onValueChange={saveDTMVisualization} />
                        </div>
                        <div className="form__center-aligned-row">
                            <label htmlFor='uHeightColorsTransparency'>Height Colors Transparency:</label>
                            <InputNumber name='uHeightColorsTransparency' id='uHeightColorsTransparency' className="form__medium-width-input" value={props.tabInfo.properties.DTMVisualizationProperties?.dtmVisualizationParams?.uHeightColorsTransparency ?? null} onValueChange={saveDTMVisualization} />
                        </div>
                        <div>
                            <label htmlFor='uHeightColorsTransparency'>Height Colors:</label>
                            <DataTable value={props.tabInfo.properties.DTMVisualizationProperties?.heightColorsVisualTable} rowClassName={() => "viewport__color-table-row-height"}>
                                <Column field="Color" header="Color"
                                    body={(rowData: VisualHeightColor, { rowIndex }) =>
                                        <ColorPickerCtrl id={`${rowIndex}-color`} name='colorRGB' value={rowData.Color} onChange={(e) => { onHeightColorsVisualTableChange(e.value, rowIndex, "Color") }} />}
                                />
                                <Column field="Alpha" header="Alpha"
                                    body={(rowData: VisualHeightColor, { rowIndex }) =>
                                        <InputNumber className="form__medium-width-input" id={`${rowIndex}-alpha`} name='Alpha' value={rowData.Alpha} onChange={e => onHeightColorsVisualTableChange(e.value, rowIndex, "Alpha")} />}
                                />
                                <Column field="HeightInSteps" header="Height"
                                    body={(rowData: VisualHeightColor, { rowIndex }) =>
                                        <InputNumber className="form__medium-width-input" id={`${rowIndex}-height`} name='HeightInSteps' value={rowData.HeightInSteps} onChange={e => onHeightColorsVisualTableChange(e.value, rowIndex, "HeightInSteps")} />}
                                />
                            </DataTable>
                        </div>
                        <Fieldset className="form__column-fieldset" legend="Set Default Height Colors">
                            <div className="form__center-aligned-row">
                                <label htmlFor='minHeight'>Min Height:</label>
                                <InputNumber name='minHeight' id='minHeight' className="form__narrow-input" value={props.tabInfo.properties.DTMVisualizationProperties?.minHeight ?? null} onValueChange={saveData} />
                            </div>
                            <div className="form__center-aligned-row">
                                <label htmlFor='maxHeight'>Max Height:</label>
                                <InputNumber name='maxHeight' id='maxHeight' className="form__narrow-input" value={props.tabInfo.properties.DTMVisualizationProperties?.maxHeight ?? null} onValueChange={saveData} />
                            </div>
                            <div className="form__apply-buttons-container">
                                <Button onClick={handleSetDefaultHeightColors}>OK</Button>
                            </div>
                        </Fieldset>
                    </div>
                    <div className="form__column-container">
                        <div className="form__center-aligned-row">
                            <label htmlFor='fShadingHeightFactor'>Shading Height Factor:</label>
                            <InputNumber name='fShadingHeightFactor' id='fShadingHeightFactor' className="form__medium-width-input" value={props.tabInfo.properties.DTMVisualizationProperties?.dtmVisualizationParams?.fShadingHeightFactor ?? null} onValueChange={saveDTMVisualization} />
                        </div>
                        <div className="form__center-aligned-row">
                            <label htmlFor='fShadingLightSourceYaw'>Shading Light Source Yaw:</label>
                            <InputNumber name='fShadingLightSourceYaw' id='fShadingLightSourceYaw' className="form__medium-width-input" value={props.tabInfo.properties.DTMVisualizationProperties?.dtmVisualizationParams?.fShadingLightSourceYaw ?? null} onValueChange={saveDTMVisualization} />
                        </div>
                        <div className="form__center-aligned-row">
                            <label htmlFor='fShadingLightSourcePitch'>Shading Light Source Pitch:</label>
                            <InputNumber name='fShadingLightSourcePitch' id='fShadingLightSourceYaw' className="form__medium-width-input" value={props.tabInfo.properties.DTMVisualizationProperties?.dtmVisualizationParams?.fShadingLightSourcePitch ?? null} onValueChange={saveDTMVisualization} />
                        </div>
                        <div className="form__center-aligned-row">
                            <label htmlFor='uShadingTransparency'>Shading Transparency:</label>
                            <InputNumber name='uShadingTransparency' id='uShadingTransparency' className="form__medium-width-input" value={props.tabInfo.properties.DTMVisualizationProperties?.dtmVisualizationParams?.uShadingTransparency ?? null} onValueChange={saveDTMVisualization} />
                        </div>

                    </div>

                </div>
                <div>
                    <div className="form__apply-buttons-container">
                        <Button onClick={handleSetDTMVisualization}>Set DTM Visualization</Button>
                        <Button onClick={handleResetForm}>Reset Form</Button>
                    </div>
                </div>
            </Fieldset>
            <div>
                <div className="form__center-aligned-row">
                    <Checkbox name="isDtmTransparencyWithoutRaster" inputId="isDtmTransparencyWithoutRaster" checked={props.tabInfo.properties.DTMVisualizationProperties?.isDtmTransparencyWithoutRaster} onChange={saveData} />
                    <label htmlFor="isDtmTransparencyWithoutRaster">Is Dtm Transparency Without Raster</label>
                    <div className="form__apply-buttons-container">
                        <Button onClick={handleDtmTransparencyWithoutRasterOK}>OK</Button>
                    </div>
                </div>
            </div>
        </div>
    )
}
