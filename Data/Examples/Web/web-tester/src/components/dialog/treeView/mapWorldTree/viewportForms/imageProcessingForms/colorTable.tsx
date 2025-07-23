import { Fieldset } from "primereact/fieldset";
import { Properties } from "../../../../dialog";
import { TabInfo, TabType } from "../../../../shared/tabCtrls/tabModels";
import { Checkbox } from "primereact/checkbox";
import { Button } from "primereact/button";
import { runCodeSafely, runMapCoreSafely } from "../../../../../../common/services/error-handling/errorHandler";
import { Dropdown } from "primereact/dropdown";
import { useSelector } from "react-redux";
import { AppState } from "../../../../../../redux/combineReducer";
import NestedTabsCtrl from "../../../../shared/tabCtrls/nestedTabsCtrl";
import { getEnumValueDetails, getEnumDetailsList } from 'mapcore-lib';
import ByHistogram, { ByHistogramPropertiesState, ByHistogramProperties } from "./byHistogram";
import Direct, { DirectProperties, DirectPropertiesState } from "./direct";
import { useEffect } from "react";

export class ColorTablePropertiesState implements Properties {
    colorTableProcessing: boolean;

    static getDefault(p: any): ColorTablePropertiesState {
        let { currentViewport } = p;
        return {
            colorTableProcessing: false,
        }
    }
}
export class ColorTableProperties extends ColorTablePropertiesState {
    colorChannels: any[];
    selectedColorChannel: any;
    DirectProperties: any;
    ByHistogramProperties: any;

    static getDefault(p: any): ColorTableProperties {
        let { viewportLayers } = p;
        let stateDefaults = super.getDefault(p);
        let colorChannelList = getEnumDetailsList(MapCore.IMcImageProcessing.EColorChannel);
        let defaultColorChannel = getEnumValueDetails(MapCore.IMcImageProcessing.EColorChannel.ECC_MULTI_CHANNEL, colorChannelList);

        let defaults: ColorTableProperties = {
            ...stateDefaults,
            colorChannels: colorChannelList,
            selectedColorChannel: defaultColorChannel,
            DirectProperties: {
                ...DirectProperties.getDefault(p),
                selectedLayer: null,
                selectedColorChannel: defaultColorChannel,
            },
            ByHistogramProperties: {
                ...ByHistogramProperties.getDefault(p),
                selectedLayer: null,
                selectedColorChannel: defaultColorChannel,
                viewportLayers: viewportLayers,
            },
        }
        return defaults;
    }
};

export default function ColorTable(props: { tabInfo: TabInfo }) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const selectedNodeInTree = useSelector((state: AppState) => state.mapWorldTreeReducer.selectedNodeInTree)

    const tabTypes: TabType[] = [
        { index: 0, header: 'Direct', statePropertiesClass: DirectPropertiesState, propertiesClass: DirectProperties, component: Direct },
        { index: 1, header: 'ByHistogram', statePropertiesClass: ByHistogramPropertiesState, propertiesClass: ByHistogramProperties, component: ByHistogram },
    ]

    //#region Handle Functions
    const handleSetColorTableProcessingClick = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['colorTableProcessing']);
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
            runMapCoreSafely(() => { mcCurrentViewport.SetEnableColorTableImageProcessing(finalLayer, props.tabInfo.tabProperties.colorTableProcessing) }, 'ImageProcessing/ColorTable.handleFilterImageProcessSetClick => IMcImageProcessing.SetEnableColorTableImageProcessing', true);
        }, 'ImageProcessing/ColorTable.handleSetColorTableProcessingClick')
    }
    const handleGetColorTableProcessingClick = () => {
        runCodeSafely(() => {
            let mcCurrentViewport: MapCore.IMcMapViewport = selectedNodeInTree.nodeMcContent;
            let mcColorTableProcessing = false;
            let finalLayer = props.tabInfo.tabProperties.selectedLayer?.layer || null;
            runMapCoreSafely(() => { mcColorTableProcessing = mcCurrentViewport.GetEnableColorTableImageProcessing(finalLayer) }, 'ImageProcessing/ColorTable.handleFilterImageProcessGetClick => IMcImageProcessing.GetEnableColorTableImageProcessing', true);
            props.tabInfo.setPropertiesCallback('colorTableProcessing', mcColorTableProcessing);
        }, 'ImageProcessing/ColorTable.handleGetColorTableProcessingClick')
    }
    const handleColorChannelChange = (e) => {
        runCodeSafely(() => {
            props.tabInfo.saveData(e);
            let directProps = { ...props.tabInfo.tabProperties.DirectProperties, selectedColorChannel: e.value }
            let byHistProps = { ...props.tabInfo.tabProperties.ByHistogramProperties, selectedColorChannel: e.value }
            props.tabInfo.setPropertiesCallback('DirectProperties', directProps);
            props.tabInfo.setPropertiesCallback('ByHistogramProperties', byHistProps);
        }, 'ImageProcessing/ColorTable.handleColorChannelChange')
    }
    //#endregion
    //#region DOM Functions
    const getEnableImageProcessingFieldset = () => {
        return <Fieldset className="form__row-fieldset form__space-between" legend='Enable Image Processing'>
            <div className="form__flex-and-row form__items-center">
                <Checkbox name='colorTableProcessing' inputId="colorTableProcessing" onChange={props.tabInfo.saveData} checked={props.tabInfo.tabProperties.colorTableProcessing} />
                <label htmlFor="colorTableProcessing">Color Table Processing</label>
            </div>
            <div className="form__row-container form__justify-end">
                <Button label="Set" onClick={handleSetColorTableProcessingClick} />
                <Button label="Get" onClick={handleGetColorTableProcessingClick} />
            </div>
        </Fieldset>
    }
    return <div>
        {getEnableImageProcessingFieldset()}
        <div style={{ padding: `${globalSizeFactor * 1}vh` }} className='form__flex-and-row form__justify-center form__items-center'>
            <label>Color Channel:</label>
            <Dropdown name="selectedColorChannel" value={props.tabInfo.tabProperties.selectedColorChannel} onChange={handleColorChannelChange} options={props.tabInfo.tabProperties.colorChannels} optionLabel="name" />
        </div>
        <div>* Color values size are 256 and separeted by comma</div>
        <NestedTabsCtrl
            tabTypes={tabTypes}
            nestedTabName='colorTable'
            lastTabInfo={props.tabInfo}
        />
    </div>
}