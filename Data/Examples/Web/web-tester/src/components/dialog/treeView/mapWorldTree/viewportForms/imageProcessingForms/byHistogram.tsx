import { Fieldset } from "primereact/fieldset";
import { Properties } from "../../../../dialog";
import { Checkbox } from "primereact/checkbox";
import { Button } from "primereact/button";
import { InputNumber } from "primereact/inputnumber";
import { InputText } from "primereact/inputtext";
import { TabInfo } from "../../../../shared/tabCtrls/tabModels";
import { runCodeSafely, runMapCoreSafely } from "../../../../../../common/services/error-handling/errorHandler";
import { useSelector } from "react-redux";
import { AppState } from "../../../../../../redux/combineReducer";
import { getEnumValueDetails, getEnumDetailsList } from 'mapcore-lib';

export class ByHistogramPropertiesState implements Properties {
    isOriginalHistogramSet: boolean;
    customHistogram: string;
    isVisibleHistogramArea: boolean;
    isUseEqualization: boolean;
    isHistogramFit: boolean;
    refHistogram: string;
    isUseNormalization: boolean;
    mean: number;
    stdev: number;

    static getDefault(p: any): ByHistogramPropertiesState {
        let { currentViewport } = p;

        return {
            isOriginalHistogramSet: false,
            customHistogram: '',
            isVisibleHistogramArea: false,
            isUseEqualization: false,
            isHistogramFit: false,
            refHistogram: '',
            isUseNormalization: false,
            mean: null,
            stdev: null,
        }
    }
}
export class ByHistogramProperties extends ByHistogramPropertiesState {
    currentHistogram: string;

    static getDefault(p: any): ByHistogramProperties {
        let stateDefaults = super.getDefault(p);

        let defaults: ByHistogramProperties = {
            ...stateDefaults,
            currentHistogram: '',
        }
        return defaults;
    }
};

export default function ByHistogram(props: { tabInfo: TabInfo }) {
    const selectedNodeInTree = useSelector((state: AppState) => state.mapWorldTreeReducer.selectedNodeInTree)

    //#region Help Functions
    const calcHistogram = (layer: MapCore.IMcRasterMapLayer) => {
        let histogram: MapCore.MC_HISTOGRAM[] = [];
        runMapCoreSafely(() => {
            let rasterLayer: MapCore.IMcRasterMapLayer = layer;
            histogram = rasterLayer.CalcHistogram();
        }, 'ImageProcessing/ColorTable/ByHistogram.float64Array => IMcRasterMapLayer.CalcHistogram', true);
        return histogram;
    }
    const ConvertHistogramStrToArr = (inputString: string) => {
        const numberStrings = inputString.split(',');
        const numbers = numberStrings.map(Number).map(num => isNaN(num) ? 0 : num);;
        const newFloatArray = new Float64Array(256);
        for (let i = 0; i < Math.min(numbers.length, 256); i++) {
            newFloatArray[i] = numbers[i];
        }
        return newFloatArray;
    };
    //#endregion
    //#region Handle functions
    const handleGetIsOriginalClick = () => {
        runCodeSafely(() => {
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let mcIsOriginal = false;
            runMapCoreSafely(() => {
                let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
                mcIsOriginal = mcCurrentViewport.IsOriginalHistogramSet(finalLayer,
                    props.tabInfo.tabProperties.selectedColorChannel?.theEnum);
            }, 'ImageProcessing/ColorTable/ByHistogram.handleGetIsOriginalClick => IMcImageProcessing.IsOriginalHistogramSet', true);
            props.tabInfo.setPropertiesCallback('isOriginalHistogramSet', mcIsOriginal)
        }, 'ImageProcessing/ColorTable/ByHistogram.handleGetIsOriginalClick')
    }
    const handleSetFromLayersClick = () => {
        runCodeSafely(() => {
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let histograms: Float64Array[] = [];
            if (props.tabInfo.tabProperties.selectedLayer?.layer) {
                histograms = calcHistogram(props.tabInfo.tabProperties.selectedLayer?.layer);
            }
            else {
                props.tabInfo.tabProperties.viewportLayers?.forEach(layer => {
                    let layerHistogram: Float64Array[] = calcHistogram(layer);
                    if (layerHistogram) {
                        for (let i = 0; i < 256; i++) {
                            histograms[0][i] += layerHistogram[0][i];
                            histograms[1][i] += layerHistogram[1][i];
                            histograms[2][i] += layerHistogram[2][i];
                        }
                    }
                });
            }
            if (histograms) {
                let chosenChannelEnum = props.tabInfo.tabProperties.selectedColorChannel?.theEnum;
                let channelsEnumArr = getEnumDetailsList(MapCore.IMcImageProcessing.EColorChannel);
                channelsEnumArr = channelsEnumArr.filter(channel => channel.theEnum != MapCore.IMcImageProcessing.EColorChannel.ECC_MULTI_CHANNEL)
                channelsEnumArr.forEach(channelEnum => {
                    if (chosenChannelEnum.value == channelEnum.code || chosenChannelEnum == MapCore.IMcImageProcessing.EColorChannel.ECC_MULTI_CHANNEL) {
                        let currentHistogram = histograms[channelEnum.code];
                        let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
                        runMapCoreSafely(() => {
                            mcCurrentViewport.SetOriginalHistogram(finalLayer,
                                props.tabInfo.tabProperties.selectedColorChannel?.theEnum, currentHistogram)
                        }, 'ImageProcessing/ColorTable/ByHistogram.handleSetFromLayersClick => IMcImageProcessing.SetOriginalHistogram', true);
                    }
                })
            }
        }, 'ImageProcessing/ColorTable/ByHistogram.handleSetFromLayersClick')
    }
    const handleSetCustomHistogramClick = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['customHistogram']);
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let splitedValue: MapCore.MC_HISTOGRAM = ConvertHistogramStrToArr(props.tabInfo.tabProperties.customHistogram)
            runMapCoreSafely(() => {
                let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
                mcCurrentViewport.SetOriginalHistogram(finalLayer,
                    props.tabInfo.tabProperties.selectedColorChannel?.theEnum, splitedValue);
            }, 'ImageProcessing/ColorTable/ByHistogram.handleSetCustomHistogramClick => IMcImageProcessing.SetOriginalHistogram', true);
        }, 'ImageProcessing/ColorTable/ByHistogram.handleSetCustomHistogramClick')
    }
    const handleGetCustomHistogramClick = () => {
        runCodeSafely(() => {
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let customHistogram: MapCore.MC_HISTOGRAM = null;
            runMapCoreSafely(() => {
                let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
                customHistogram = mcCurrentViewport.GetOriginalHistogram(finalLayer,
                    props.tabInfo.tabProperties.selectedColorChannel?.theEnum);
            }, 'ImageProcessing/ColorTable/ByHistogram.handleGetCustomHistogramClick => IMcImageProcessing.GetOriginalHistogram', true);
            let strCustonHist = customHistogram?.join(',');
            props.tabInfo.setPropertiesCallback('customHistogram', strCustonHist)
        }, 'ImageProcessing/ColorTable/ByHistogram.handleGetCustomHistogramClick')
    }
    const handleGetCurrentHistogramClick = () => {
        runCodeSafely(() => {
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let currentHistogram: MapCore.MC_HISTOGRAM = null;
            runMapCoreSafely(() => {
                let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
                currentHistogram = mcCurrentViewport.GetCurrentHistogram(finalLayer,
                    props.tabInfo.tabProperties.selectedColorChannel?.theEnum);
            }, 'ImageProcessing/ColorTable/ByHistogram.handleGetCurrentHistogramClick => IMcImageProcessing.GetCurrentHistogram', true);
            let strCurrentHistogram = currentHistogram?.join(',');
            props.tabInfo.setPropertiesCallback('currentHistogram', strCurrentHistogram)
        }, 'ImageProcessing/ColorTable/ByHistogram.handleGetCurrentHistogramClick')
    }
    const handleSetIsVisibleHistogramAreaClick = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['isVisibleHistogramArea']);
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            runMapCoreSafely(() => {
                let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
                mcCurrentViewport.SetVisibleAreaOriginalHistogram(finalLayer,
                    props.tabInfo.tabProperties.selectedColorChannel?.theEnum, props.tabInfo.tabProperties.isVisibleHistogramArea);
            }, 'ImageProcessing/ColorTable/ByHistogram.handleSetIsVisibleHistogramAreaClick => IMcImageProcessing.SetVisibleAreaOriginalHistogram', true);
        }, 'ImageProcessing/ColorTable/ByHistogram.handleSetIsVisibleHistogramAreaClick')
    }
    const handleGetIsVisibleHistogramAreaClick = () => {
        runCodeSafely(() => {
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let isVisibleHistogramArea: boolean = null;
            let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
            runMapCoreSafely(() => {
                isVisibleHistogramArea = mcCurrentViewport.GetVisibleAreaOriginalHistogram(finalLayer,
                    props.tabInfo.tabProperties.selectedColorChannel?.theEnum);
            }, 'ImageProcessing/ColorTable/ByHistogram.handleGetIsVisibleHistogramAreaClick => IMcImageProcessing.GetVisibleAreaOriginalHistogram', true);
            props.tabInfo.setPropertiesCallback('isVisibleHistogramArea', isVisibleHistogramArea)
        }, 'ImageProcessing/ColorTable/ByHistogram.handleGetIsVisibleHistogramAreaClick')
    }
    const handleSetIsUseEqualizationClick = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['isUseEqualization']);
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
            runMapCoreSafely(() => {
                mcCurrentViewport.SetHistogramEqualization(finalLayer,
                    props.tabInfo.tabProperties.selectedColorChannel?.theEnum, props.tabInfo.tabProperties.isUseEqualization);
            }, 'ImageProcessing/ColorTable/ByHistogram.handleSetIsUseEqualizationClick => IMcImageProcessing.SetHistogramEqualization', true);
        }, 'ImageProcessing/ColorTable/ByHistogram.handleSetIsUseEqualizationClick')
    }
    const handleGetIsUseEqualizationClick = () => {
        runCodeSafely(() => {
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let isUseEqualization: boolean = null;
            let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
            runMapCoreSafely(() => {
                isUseEqualization = mcCurrentViewport.GetHistogramEqualization(finalLayer,
                    props.tabInfo.tabProperties.selectedColorChannel?.theEnum);
            }, 'ImageProcessing/ColorTable/ByHistogram.handleGetIsUseEqualizationClick => IMcImageProcessing.GetHistogramEqualization', true);
            props.tabInfo.setPropertiesCallback('isUseEqualization', isUseEqualization)
        }, 'ImageProcessing/ColorTable/ByHistogram.handleGetIsUseEqualizationClick')
    }
    const handleSetIsHistogramFitClick = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['isHistogramFit', 'refHistogram']);
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let refHistArr = ConvertHistogramStrToArr(props.tabInfo.tabProperties.refHistogram);
            let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
            runMapCoreSafely(() => {
                mcCurrentViewport.SetHistogramFit(finalLayer,
                    props.tabInfo.tabProperties.selectedColorChannel?.theEnum, props.tabInfo.tabProperties.isHistogramFit, refHistArr);
            }, 'ImageProcessing/ColorTable/ByHistogram.handleSetIsHistogramFitClick => IMcImageProcessing.SetHistogramFit', true);
        }, 'ImageProcessing/ColorTable/ByHistogram.handleSetIsHistogramFitClick')
    }
    const handleGetIsHistogramFitClick = () => {
        runCodeSafely(() => {
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let isHistogramFit: boolean = null;
            let refHistogram: { Value?: MapCore.MC_HISTOGRAM } = {};
            let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
            runMapCoreSafely(() => {
                isHistogramFit = mcCurrentViewport.GetHistogramFit(finalLayer,
                    props.tabInfo.tabProperties.selectedColorChannel?.theEnum, refHistogram);
            }, 'ImageProcessing/ColorTable/ByHistogram.handleGetIsHistogramFitClick => IMcImageProcessing.GetHistogramFit', true);
            let refHistogramStr = refHistogram.Value?.join(',');
            props.tabInfo.setPropertiesCallback('isHistogramFit', isHistogramFit)
            props.tabInfo.setPropertiesCallback('refHistogram', refHistogramStr)
        }, 'ImageProcessing/ColorTable/ByHistogram.handleGetIsHistogramFitClick')
    }
    const handleSetHistogramNormalizationClick = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['isUseNormalization', 'stdev', 'mean']);
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
            runMapCoreSafely(() => {
                mcCurrentViewport.SetHistogramNormalization(finalLayer,
                    props.tabInfo.tabProperties.selectedColorChannel?.theEnum,
                    props.tabInfo.tabProperties.isUseNormalization,
                    props.tabInfo.tabProperties.mean,
                    props.tabInfo.tabProperties.stdev,
                );
            }, 'ImageProcessing/ColorTable/ByHistogram.handleSetHistogramNormalizationClick => IMcImageProcessing.SetHistogramFit', true);
        }, 'ImageProcessing/ColorTable/ByHistogram.handleSetHistogramNormalizationClick')
    }
    const handleGetHistogramNormalizationClick = () => {
        runCodeSafely(() => {
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let isUseNormalization: boolean = null;
            let pMean: { Value?: number } = {};
            let pStdev: { Value?: number } = {};
            let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
            runMapCoreSafely(() => {
                isUseNormalization = mcCurrentViewport.GetHistogramNormalization(finalLayer,
                    props.tabInfo.tabProperties.selectedColorChannel?.theEnum, pMean, pStdev);
            }, 'ImageProcessing/ColorTable/ByHistogram.handleGetHistogramNormalizationClick => IMcImageProcessing.GetHistogramNormalization', true);
            props.tabInfo.setPropertiesCallback('isUseNormalization', isUseNormalization)
            props.tabInfo.setPropertiesCallback('mean', pMean.Value)
            props.tabInfo.setPropertiesCallback('stdev', pStdev.Value)
        }, 'ImageProcessing/ColorTable/ByHistogram.handleGetHistogramNormalizationClick')
    }

    //#endregion
    //#endregion DOM Functions
    const getRightFieldsets = () => {
        return <div style={{ width: '50%' }}>
            <Fieldset className="form__row-fieldset form__space-between" legend='Histogram Equalization'>
                <div className="form__flex-and-row form__items-center">
                    <Checkbox name='isUseEqualization' inputId="isUseEqualization" onChange={props.tabInfo.saveData} checked={props.tabInfo.tabProperties.isUseEqualization} />
                    <label htmlFor="isUseEqualization">Use</label>
                </div>
                <div className="form__row-container">
                    <Button label="Set" onClick={handleSetIsUseEqualizationClick} />
                    <Button label="Get" onClick={handleGetIsUseEqualizationClick} />
                </div>
            </Fieldset>
            <Fieldset className="form__column-fieldset" legend='Histogram Fit'>
                <div className="form__flex-and-row-between form__items-center">
                    <label htmlFor="refHistogram">Reference Histogram</label>
                    <InputText id='refHistogram' value={props.tabInfo.tabProperties.refHistogram} name='refHistogram' onChange={props.tabInfo.saveData} />
                </div>
                <div className="form__flex-and-row-between form__items-center">
                    <div className="form__flex-and-row form__items-center">
                        <Checkbox name='isHistogramFit' inputId="isHistogramFit" onChange={props.tabInfo.saveData} checked={props.tabInfo.tabProperties.isHistogramFit} />
                        <label htmlFor="isHistogramFit">Use</label>
                    </div>
                    <div className="form__row-container">
                        <Button label="Set" onClick={handleSetIsHistogramFitClick} />
                        <Button label="Get" onClick={handleGetIsHistogramFitClick} />
                    </div>
                </div>
            </Fieldset>
            <Fieldset className="form__column-fieldset" legend='Histogram Normalization'>
                <div className="form__flex-and-row form__items-center">
                    <Checkbox name='isUseNormalization' inputId="isUseNormalization" onChange={props.tabInfo.saveData} checked={props.tabInfo.tabProperties.isUseNormalization} />
                    <label htmlFor="isUseNormalization">Use</label>
                </div>
                <div className="form__flex-and-row-between form__items-center">
                    <label htmlFor="mean">Mean:</label>
                    <InputNumber maxFractionDigits={5} id='mean' value={props.tabInfo.tabProperties.mean} name='mean' onValueChange={props.tabInfo.saveData} />
                </div>
                <div className="form__flex-and-row-between form__items-center">
                    <label htmlFor="stdev">Stdev:</label>
                    <InputNumber maxFractionDigits={5} id='stdev' value={props.tabInfo.tabProperties.stdev} name='stdev' onValueChange={props.tabInfo.saveData} />
                </div>
                <div className="form__row-container form__justify-end">
                    <Button label="Set" onClick={handleSetHistogramNormalizationClick} />
                    <Button label="Get" onClick={handleGetHistogramNormalizationClick} />
                </div>
            </Fieldset>
        </div>
    }
    const getLeftFieldsets = () => {
        return <div style={{ width: '50%' }}>
            <Fieldset className="form__column-fieldset" legend='Original Histogram'>
                <label htmlFor="customHistogram">Custom Histogram:</label>
                <InputText style={{ width: '100%' }} id='customHistogram' value={props.tabInfo.tabProperties.customHistogram} name='customHistogram' onChange={props.tabInfo.saveData} />
                <div className="form__flex-and-row-between">
                    <Button label="Set From Layer(s)" onClick={handleSetFromLayersClick} />
                    <div className="form__row-container">
                        <Button label="Set" onClick={handleSetCustomHistogramClick} />
                        <Button label="Get" onClick={handleGetCustomHistogramClick} />
                    </div>
                </div>
                <div className="form__flex-and-row-between">
                    <div className="form__flex-and-row form__items-center">
                        <Checkbox name='isVisibleHistogramArea' inputId="isVisibleHistogramArea" onChange={props.tabInfo.saveData} checked={props.tabInfo.tabProperties.isVisibleHistogramArea} />
                        <label htmlFor="isVisibleHistogramArea">Visible Histogram Area</label>
                    </div>
                    <div className="form__row-container">
                        <Button label="Set" onClick={handleSetIsVisibleHistogramAreaClick} />
                        <Button label="Get" onClick={handleGetIsVisibleHistogramAreaClick} />
                    </div>
                </div>
            </Fieldset>
            <Fieldset className="form__column-fieldset" legend='Current Histogram'>
                <div className="form__flex-and-row-between form__items-center">
                    <label htmlFor="currentHistogram">Histogram</label>
                    <InputText id='currentHistogram' value={props.tabInfo.tabProperties.currentHistogram} name='currentHistogram' onChange={props.tabInfo.saveData} />
                </div>
                <div className="form__row-container form__justify-end">
                    <Button label="Get" onClick={handleGetCurrentHistogramClick} />
                </div>
            </Fieldset>
        </div>
    }
    const getOriginalHistFieldset = () => {
        return <Fieldset className="form__row-fieldset form__space-between" legend='Is Original Histogram Set'>
            <div className="form__flex-and-row form__items-center form__disabled">
                <Checkbox name='isOriginalHistogramSet' inputId="isOriginalHistogramSet" onChange={props.tabInfo.saveData} checked={props.tabInfo.tabProperties.isOriginalHistogramSet} />
                <label htmlFor="isOriginalHistogramSet">Use</label>
            </div>
            <div className="form__row-container form__items-end">
                <Button label="Get" onClick={handleGetIsOriginalClick} />
            </div>
        </Fieldset>
    }
    //#endregion

    return <div className="form__column-container">
        {getOriginalHistFieldset()}
        <div className="form__flex-and-row-between">
            {getLeftFieldsets()}
            {getRightFieldsets()}
        </div>
    </div>
}