import { Fieldset } from "primereact/fieldset";
import { Properties } from "../../../../dialog";
import { TabInfo } from "../../../../shared/tabCtrls/tabModels";
import { InputText } from "primereact/inputtext";
import { Button } from "primereact/button";
import { runCodeSafely, runMapCoreSafely } from "../../../../../../common/services/error-handling/errorHandler";

export class RasterPropertiesState implements Properties {

    static getDefault(p: any): RasterPropertiesState {
        let { currentLayer, mapWorldTree } = p;
        return {}
    }
}
export class RasterProperties extends RasterPropertiesState {
    mcRasterLayer: MapCore.IMcRasterMapLayer;
    histogram: string;
    g: string;
    b: string;

    static getDefault(p: any): RasterProperties {
        // let stateDefaults = super.getDefault(p);
        let { currentLayer, mapWorldTree } = p;
        let mcCurrentLayer = currentLayer.nodeMcContent as MapCore.IMcRasterMapLayer;

        return {
            mcRasterLayer: mcCurrentLayer,
            histogram: '',
            g: '',
            b: '',
        }
    }
}

export default function Raster(props: { tabInfo: TabInfo }) {
    let { tabProperties, setPropertiesCallback, applyCurrStatePropertiesCallback, setCurrStatePropertiesCallback, setApplyCallBack, saveData } = props.tabInfo;

    const handleCalculateClick = () => {
        runCodeSafely(() => {
            let mcRasterLayer = tabProperties.mcRasterLayer as MapCore.IMcRasterMapLayer;
            let histogram: MapCore.MC_HISTOGRAM[] = [];
            runMapCoreSafely(() => {
                histogram = mcRasterLayer.CalcHistogram();
            }, 'MapLayerForm/Raster.handleCalculateClick => ', true)
            let histogramStrings = histogram.map((float64Array) => Array.from(float64Array).join(','));
            setPropertiesCallback({ histogram: histogramStrings[0], g: histogramStrings[1], b: histogramStrings[2] });
        }, 'MapLayerForm/Raster.handleCalculateClick')
    }

    return <Fieldset legend='Layer Histogram' className="form__column-fieldset">
        <div className="form__flex-and-row-between">
            <span></span>
            <InputText style={{ width: '80%' }} value={tabProperties.histogram} />
        </div>
        <div className="form__flex-and-row-between">
            <span>G</span>
            <InputText style={{ width: '80%' }} value={tabProperties.g} />
        </div>
        <div className="form__flex-and-row-between">
            <span>B</span>
            <InputText style={{ width: '80%' }} value={tabProperties.b} />
        </div>
        <Button label="Calculate" onClick={handleCalculateClick} />
    </Fieldset>
}
