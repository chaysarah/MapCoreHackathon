import { Fieldset } from "primereact/fieldset";
import { Properties } from "../../../../dialog";
import { TabInfo } from "../../../../shared/tabCtrls/tabModels";
import { Dropdown } from "primereact/dropdown";
import { useSelector } from "react-redux";
import { AppState } from "../../../../../../redux/combineReducer";

export class RawRasterPropertiesState implements Properties {
    // componentType:;

    static getDefault(p: any): RawRasterPropertiesState {
        let { currentLayer, mapWorldTree } = p;
       
        return {

        }
    }
}
export class RawRasterProperties extends RawRasterPropertiesState {
    mcRawRasterLayer: MapCore.IMcRawRasterMapLayer;

    static getDefault(p: any): RawRasterProperties {
        // let stateDefaults = super.getDefault(p);
        let { currentLayer, mapWorldTree } = p;
        let mcCurrentLayer = currentLayer.nodeMcContent as MapCore.IMcRawRasterMapLayer;

        return {
            mcRawRasterLayer: mcCurrentLayer,

        }
    }
}

export default function RawRaster(props: { tabInfo: TabInfo }) {
 let { tabProperties, setPropertiesCallback, applyCurrStatePropertiesCallback, setCurrStatePropertiesCallback, setApplyCallBack, saveData } = props.tabInfo;
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    const getAddComponentsFieldset = () => {
        return <Fieldset legend='Add Component' className="form__column-fieldset">
            <div style={{ width: '49%' }} className="form__flex-and-row-between form__items-center">
                <span>Component Type:</span>
                <Dropdown style={{ width: '55%' }} name="componentType" value={tabProperties.componentType} onChange={saveData} options={tabProperties.componentType} optionLabel="name" />
            </div>
        </Fieldset>
    }

    return <div className="form__column-container">
        {getAddComponentsFieldset()}
    </div>
}