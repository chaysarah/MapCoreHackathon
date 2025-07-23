import { Fieldset } from "primereact/fieldset";
import { Properties } from "../../../../dialog";
import { Button } from "primereact/button";
import { Checkbox } from "primereact/checkbox";
import { TabInfo } from "../../../../shared/tabCtrls/tabModels";
import { InputText } from "primereact/inputtext";
import { runCodeSafely, runMapCoreSafely } from "../../../../../../common/services/error-handling/errorHandler";
import { useDispatch, useSelector } from "react-redux";
import { AppState } from "../../../../../../redux/combineReducer";
import { InputNumber } from "primereact/inputnumber";
import { setShowDialog } from "../../../../../../redux/ObjectWorldTree/objectWorldTreeActions";
import { hideFormReasons } from "../../../../../shared/models/tree-node.model";
import { DialogTypesEnum } from "../../../../../../tools/enum/enums";
import { useEffect } from "react";


export class DirectPropertiesState implements Properties {
    isUse: boolean;
    userColorValuesStr: string;
    brightness: number;
    contrast: number;
    isUseNegative: boolean;
    red: number;
    green: number;
    blue: number;
    gamma: number;

    static getDefault(p: any): DirectPropertiesState {
        let { currentViewport } = p;

        return {
            isUse: false,
            userColorValuesStr: '',
            brightness: 0,
            contrast: 0,
            isUseNegative: false,
            red: 0,
            green: 0,
            blue: 0,
            gamma: 0,
        }
    }
}
export class DirectProperties extends DirectPropertiesState {
    isSelectClick: false;
    currentColorValuesStr: string;

    static getDefault(p: any): DirectProperties {
        let stateDefaults = super.getDefault(p);

        let defaults: DirectProperties = {
            ...stateDefaults,
            isSelectClick: false,
            currentColorValuesStr: '',
        }
        return defaults;
    }
};

export default function Direct(props: { tabInfo: TabInfo }) {
    const dispatch = useDispatch();
    const selectedNodeInTree = useSelector((state: AppState) => state.mapWorldTreeReducer.selectedNodeInTree)
    const cursorRgb = useSelector((state: AppState) => state.mapWindowReducer.cursorRgb);

    const stringsToUint8Array = (colorValuesArr: string[]): Uint8Array => {
        const uint8Array = new Uint8Array(256);
        for (let i = 0; i < colorValuesArr.length; i++) {
            const number = parseInt(colorValuesArr[i]);
            if (!isNaN(number) && number >= 0 && number <= 255) {
                uint8Array[i] = number;
            }
        }
        return uint8Array;
    }

    useEffect(() => {
        if (props.tabInfo.tabProperties.isSelectClick) {
            props.tabInfo.setCurrStatePropertiesCallback('red', cursorRgb.R)
            props.tabInfo.setCurrStatePropertiesCallback('green', cursorRgb.G)
            props.tabInfo.setCurrStatePropertiesCallback('blue', cursorRgb.B)

            props.tabInfo.setPropertiesCallback('red', cursorRgb.R)
            props.tabInfo.setPropertiesCallback('green', cursorRgb.G)
            props.tabInfo.setPropertiesCallback('blue', cursorRgb.B)
            props.tabInfo.setPropertiesCallback('isSelectClick', false)
        }
    }, [cursorRgb])
    //#region Handle Functions
    const handleSetUserColorValuesClick = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['isUse', 'userColorValuesStr']);
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let colorValuesArr = stringsToUint8Array(props.tabInfo.tabProperties.userColorValuesStr.split(','));
            let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
            runMapCoreSafely(() => {
                mcCurrentViewport.SetUserColorValues(finalLayer,
                    props.tabInfo.tabProperties.selectedColorChannel?.theEnum,
                    colorValuesArr, props.tabInfo.tabProperties.isUse);
            }, 'ImageProcessing/ColorTable/Direct.handleSetUserColorValuesClick => IMcImageProcessing.SetUserColorValues', true);
        }, 'ImageProcessing/ColorTable/Direct.handleSetUserColorValuesClick')
    }
    const handleGetUserColorValuesClick = () => {
        runCodeSafely(() => {
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let colorValuesArr: { Value?: MapCore.IMcImageProcessing.MC_COLORTABLE } = { Value: new Uint8Array(256) };
            let mcIsUse = false;
            let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
            runMapCoreSafely(() => {
                mcIsUse = mcCurrentViewport.GetUserColorValues(finalLayer,
                    props.tabInfo.tabProperties.selectedColorChannel?.theEnum, colorValuesArr);
            }, 'ImageProcessing/ColorTable/Direct.handleGetUserColorValuesClick => IMcImageProcessing.GetUserColorValues', true);
            props.tabInfo.setPropertiesCallback('isUse', mcIsUse)
            props.tabInfo.setPropertiesCallback('userColorValuesStr', colorValuesArr.Value.join(','))
        }, 'ImageProcessing/ColorTable/Direct.handleGetUserColorValuesClick')
    }
    const handleSetDefaultColorsClick = () => {
        runCodeSafely(() => {
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
            runMapCoreSafely(() => {
                mcCurrentViewport.SetColorValuesToDefault(finalLayer,
                    props.tabInfo.tabProperties.selectedColorChannel?.theEnum);
            }, 'ImageProcessing/ColorTable/Direct.handleSetDefaultColorsClick => IMcImageProcessing.SetColorValuesToDefault', true);
        }, 'ImageProcessing/ColorTable/Direct.handleSetDefaultColorsClick')
    }
    const handleGetCurrentUserColorsClick = () => {
        runCodeSafely(() => {
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let currentColors: MapCore.IMcImageProcessing.MC_COLORTABLE = null;
            let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
            runMapCoreSafely(() => {
                currentColors = mcCurrentViewport.GetCurrentColorValues(finalLayer,
                    props.tabInfo.tabProperties.selectedColorChannel?.theEnum);
            }, 'ImageProcessing/ColorTable/Direct.handleSetDefaultColorsClick => IMcImageProcessing.SetColorValuesToDefault', true);
            props.tabInfo.setPropertiesCallback('currentColorValuesStr', currentColors.join(','))
        }, 'ImageProcessing/ColorTable/Direct.handleGetCurrentUserColorsClick')
    }
    const handleSetBrightnessClick = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['brightness']);
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
            runMapCoreSafely(() => {
                mcCurrentViewport.SetColorTableBrightness(finalLayer,
                    props.tabInfo.tabProperties.selectedColorChannel?.theEnum, props.tabInfo.tabProperties.brightness);
            }, 'ImageProcessing/ColorTable/Direct.handleSetBrightnessClick => IMcImageProcessing.SetColorTableBrightness', true);
        }, 'ImageProcessing/ColorTable/Direct.handleSetBrightnessClick')
    }
    const handleGetBrightnessClick = () => {
        runCodeSafely(() => {
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let brightness = null;
            let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
            runMapCoreSafely(() => {
                brightness = mcCurrentViewport.GetColorTableBrightness(finalLayer,
                    props.tabInfo.tabProperties.selectedColorChannel?.theEnum);
            }, 'ImageProcessing/ColorTable/Direct.handleGetBrightnessClick => IMcImageProcessing.GetColorTableBrightness', true);
            props.tabInfo.setPropertiesCallback('brightness', brightness);
        }, 'ImageProcessing/ColorTable/Direct.handleGetBrightnessClick')
    }
    const handleSetContrastClick = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['contrast']);
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
            runMapCoreSafely(() => {
                mcCurrentViewport.SetContrast(finalLayer,
                    props.tabInfo.tabProperties.selectedColorChannel?.theEnum, props.tabInfo.tabProperties.contrast);
            }, 'ImageProcessing/ColorTable/Direct.handleSetContrastClick => IMcImageProcessing.SetContrast', true);
        }, 'ImageProcessing/ColorTable/Direct.handleSetContrastClick')
    }
    const handleGetContrastClick = () => {
        runCodeSafely(() => {
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let contrast = null;
            let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
            runMapCoreSafely(() => {
                contrast = mcCurrentViewport.GetContrast(finalLayer,
                    props.tabInfo.tabProperties.selectedColorChannel?.theEnum);
            }, 'ImageProcessing/ColorTable/Direct.handleGetContrastClick => IMcImageProcessing.GetColorTableBrightness', true);
            props.tabInfo.setPropertiesCallback('contrast', contrast);
        }, 'ImageProcessing/ColorTable/Direct.handleGetContrastClick')
    }
    const handleSetUseNegativeClick = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['isUseNegative']);
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
            runMapCoreSafely(() => {
                mcCurrentViewport.SetNegative(finalLayer,
                    props.tabInfo.tabProperties.selectedColorChannel?.theEnum, props.tabInfo.tabProperties.isUseNegative);
            }, 'ImageProcessing/ColorTable/Direct.handleSetUseNegativeClick => IMcImageProcessing.SetNegative', true);
        }, 'ImageProcessing/ColorTable/Direct.handleSetUseNegativeClick')
    }
    const handleGetUseNegativeClick = () => {
        runCodeSafely(() => {
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let isUseNegative = null;
            let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
            runMapCoreSafely(() => {
                isUseNegative = mcCurrentViewport.GetNegative(finalLayer,
                    props.tabInfo.tabProperties.selectedColorChannel?.theEnum);
            }, 'ImageProcessing/ColorTable/Direct.handleGetUseNegativeClick => IMcImageProcessing.GetColorTableBrightness', true);
            props.tabInfo.setPropertiesCallback('isUseNegative', isUseNegative);
        }, 'ImageProcessing/ColorTable/Direct.handleGetUseNegativeClick')
    }
    const handleSetGammaClick = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['gamma']);
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
            runMapCoreSafely(() => {
                mcCurrentViewport.SetGamma(finalLayer,
                    props.tabInfo.tabProperties.selectedColorChannel?.theEnum, props.tabInfo.tabProperties.gamma);
            }, 'ImageProcessing/ColorTable/Direct.handleSetGammaClick => IMcImageProcessing.SetGamma', true);
        }, 'ImageProcessing/ColorTable/Direct.handleSetGammaClick')
    }
    const handleGetGammaClick = () => {
        runCodeSafely(() => {
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let gamma = null;
            let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
            runMapCoreSafely(() => {
                gamma = mcCurrentViewport.GetGamma(finalLayer,
                    props.tabInfo.tabProperties.selectedColorChannel?.theEnum);
            }, 'ImageProcessing/ColorTable/Direct.handleGetGammaClick => IMcImageProcessing.GetGamma', true);
            props.tabInfo.setPropertiesCallback('gamma', gamma);
        }, 'ImageProcessing/ColorTable/Direct.handleGetGammaClick')
    }
    const handleSelectWhitePointClick = () => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback('isSelectClick', true)
            dispatch(setShowDialog({ hideFormReason: hideFormReasons.CHOOSE_POINT, dialogType: DialogTypesEnum.mapWorldTree }));
        }, 'ImageProcessing/ColorTable/Direct.handleSelectWhitePointClick')
    }
    const handleSetWhiteBalanceClick = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['red', 'blue', 'green']);
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
            runMapCoreSafely(() => {
                mcCurrentViewport.SetWhiteBalanceBrightness(finalLayer,
                    props.tabInfo.tabProperties.red,
                    props.tabInfo.tabProperties.green,
                    props.tabInfo.tabProperties.blue,
                );
            }, 'ImageProcessing/ColorTable/Direct.handleSetWhiteBalanceClick => IMcImageProcessing.SetWhiteBalanceBrightness', true);
        }, 'ImageProcessing/ColorTable/Direct.handleSetWhiteBalanceClick')
    }
    const handleGetWhiteBalanceClick = () => {
        runCodeSafely(() => {
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let pRed: { Value?: number } = {};
            let pGreen: { Value?: number } = {};
            let pBlue: { Value?: number } = {};
            let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
            runMapCoreSafely(() => {
                mcCurrentViewport.GetWhiteBalanceBrightness(finalLayer, pRed, pGreen, pBlue);
            }, 'ImageProcessing/ColorTable/Direct.handleGetWhiteBalanceClick => IMcImageProcessing.GetWhiteBalanceBrightness', true);
            props.tabInfo.setPropertiesCallback('red', pRed.Value);
            props.tabInfo.setPropertiesCallback('green', pGreen.Value);
            props.tabInfo.setPropertiesCallback('blue', pBlue.Value);
        }, 'ImageProcessing/ColorTable/Direct.handleGetWhiteBalanceClick')
    }
    //#endregion
    //#region DOM Functions
    const getRightFieldsets = () => {
        return <div style={{ width: '50%' }}>
            <Fieldset className="form__column-fieldset" legend='Color Table Brightness'>
                <div className="form__flex-and-row-between form__items-center">
                    <label htmlFor="brightness">Brightness:</label>
                    <InputNumber maxFractionDigits={5} id='brightness' value={props.tabInfo.tabProperties.brightness} name='brightness' onValueChange={props.tabInfo.saveData} />
                </div>
                <div className="form__flex-and-row-between">
                    <div className="form__justify-end">(Between -1 to 1)</div>
                    <div className="form__row-container">
                        <Button label="Set" onClick={handleSetBrightnessClick} />
                        <Button label="Get" onClick={handleGetBrightnessClick} />
                    </div>
                </div>
            </Fieldset>
            <Fieldset className="form__column-fieldset" legend='Color Table Contrast'>
                <div className="form__flex-and-row-between form__items-center">
                    <label htmlFor="contrast">Contrast:</label>
                    <InputNumber id='contrast' maxFractionDigits={5} value={props.tabInfo.tabProperties.contrast} name='contrast' onValueChange={props.tabInfo.saveData} />
                </div>
                <div className="form__flex-and-row-between">
                    <div className="form__justify-end">(Between -1 to 1)</div>
                    <div className="form__row-container">
                        <Button label="Set" onClick={handleSetContrastClick} />
                        <Button label="Get" onClick={handleGetContrastClick} />
                    </div>
                </div>
            </Fieldset>
            <Fieldset className="form__row-fieldset form__space-between" legend='Negative'>
                <div className="form__flex-and-row form__items-center">
                    <Checkbox name='isUseNegative' inputId="isUseNegative" onChange={props.tabInfo.saveData} checked={props.tabInfo.tabProperties.isUseNegative} />
                    <label htmlFor="isUseNegative">Use</label>
                </div>
                <div className="form__row-container form__justify-end">
                    <Button label="Set" onClick={handleSetUseNegativeClick} />
                    <Button label="Get" onClick={handleGetUseNegativeClick} />
                </div>
            </Fieldset>
            <Fieldset className="form__column-fieldset" legend='Gamma'>
                <div className="form__flex-and-row-between form__items-center">
                    <label htmlFor="gamma">Gamma:</label>
                    <InputNumber id='gamma' maxFractionDigits={5} value={props.tabInfo.tabProperties.gamma} name='gamma' onValueChange={props.tabInfo.saveData} />
                </div>
                <div className="form__flex-and-row-between">
                    <div className="form__justify-end">(Between 0.01 to 100)</div>
                    <div className="form__row-container">
                        <Button label="Set" onClick={handleSetGammaClick} />
                        <Button label="Get" onClick={handleGetGammaClick} />
                    </div>
                </div>
            </Fieldset>
        </div>
    }
    const getLeftFieldsets = () => {
        return <div style={{ width: '50%' }}>
            <Fieldset className="form__column-fieldset" legend='User Color Values'>
                <div className="form__flex-and-row form__items-center">
                    <Checkbox name='isUse' inputId="isUse" onChange={props.tabInfo.saveData} checked={props.tabInfo.tabProperties.isUse} />
                    <label htmlFor="isUse">Use</label>
                </div>
                <div className="form__flex-and-row-between form__items-center">
                    <label htmlFor="userColorValuesStr">Color Values</label>
                    <InputText id='userColorValuesStr' value={props.tabInfo.tabProperties.userColorValuesStr} name='userColorValuesStr' onChange={props.tabInfo.saveData} />
                </div>
                <div className="form__row-container form__justify-end">
                    <Button label="Set" onClick={handleSetUserColorValuesClick} />
                    <Button label="Get" onClick={handleGetUserColorValuesClick} />
                </div>
            </Fieldset>
            <Fieldset legend='Color Values To Default'>
                <div className="form__row-container form__justify-end" style={{ width: '100%' }}>
                    <Button label="Set" onClick={handleSetDefaultColorsClick} />
                </div>
            </Fieldset>
            <Fieldset className="form__column-fieldset" legend='Current Color Values'>
                <div className="form__flex-and-row-between form__items-center">
                    <label htmlFor="currentColorValuesStr">Color Values</label>
                    <InputText id='currentColorValuesStr' value={props.tabInfo.tabProperties.currentColorValuesStr} name='currentColorValuesStr' onChange={props.tabInfo.saveData} />
                </div>
                <div className="form__row-container form__justify-end">
                    <Button label="Get" onClick={handleGetCurrentUserColorsClick} />
                </div>
            </Fieldset>
        </div>
    }
    const getWhiteBalanceBrightness = () => {
        return <Fieldset className="form__row-fieldset form__space-between" legend='White Balance Brightness'>
            <div className="form__column-container" style={{ width: '50%' }}>
                <div style={{ fontWeight: 'bold' }}>(Values Between 0 to 255)</div>
                <div className="form__flex-and-row-between form__items-center">
                    <label htmlFor="red">Red:</label>
                    <InputNumber id='red' value={props.tabInfo.tabProperties.red} name='red' onValueChange={props.tabInfo.saveData} />
                </div>
                <div className="form__flex-and-row-between form__items-center">
                    <label htmlFor="green">Green:</label>
                    <InputNumber id='green' value={props.tabInfo.tabProperties.green} name='green' onValueChange={props.tabInfo.saveData} />
                </div>
                <div className="form__flex-and-row-between form__items-center">
                    <label htmlFor="blue">Blue:</label>
                    <InputNumber id='blue' value={props.tabInfo.tabProperties.blue} name='blue' onValueChange={props.tabInfo.saveData} />
                </div>
            </div>
            <div className="form__row-container form__items-end">
                <Button label="Select White Point" onClick={handleSelectWhitePointClick} />
                <Button label="Set" onClick={handleSetWhiteBalanceClick} />
                <Button label="Get" onClick={handleGetWhiteBalanceClick} />
            </div>
        </Fieldset>
    }
    //#endregion

    return <div className="form__column-container">
        <div className="form__flex-and-row-between">
            {getLeftFieldsets()}
            {getRightFieldsets()}
        </div>
        {getWhiteBalanceBrightness()}
    </div>
}