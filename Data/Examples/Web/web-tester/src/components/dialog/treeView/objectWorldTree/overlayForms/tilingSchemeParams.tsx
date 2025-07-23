import { Dialog } from "primereact/dialog";
import { Dropdown } from "primereact/dropdown";
import { Fieldset } from "primereact/fieldset";
import { InputNumber } from "primereact/inputnumber";
import { useEffect, useState } from "react";
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";
import { getEnumDetailsList, getEnumValueDetails } from "mapcore-lib";
import Footer from "../../../footerDialog";
import generalService from "../../../../../services/general.service";
import { useSelector } from "react-redux";
import { AppState } from "../../../../../redux/combineReducer";
import './styles/tilingSchemeParams.css';

export default function TilingSchemeParams(props: { onHide: () => void }) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    const [enumDetails] = useState({
        ETilingSchemeType: getEnumDetailsList(MapCore.IMcMapLayer.ETilingSchemeType),
    });
    const [tilingSchemeParamsFormData, setTilingSchemeParamsFormData] = useState({
        tilingOriginX: generalService.ObjectWorldTreeProperties.tilingOriginX,
        tilingOriginY: generalService.ObjectWorldTreeProperties.tilingOriginY,
        tilingSchemeTypeValue: getEnumValueDetails(generalService.ObjectWorldTreeProperties.tilingSchemeTypeValue, enumDetails.ETilingSchemeType),
        largestTileSize: generalService.ObjectWorldTreeProperties.largestTileSize,
        tileSizeInPx: generalService.ObjectWorldTreeProperties.tileSizeInPx,
        numLargestTilesX: generalService.ObjectWorldTreeProperties.numLargestTilesX,
        numLargestTilesY: generalService.ObjectWorldTreeProperties.numLargestTilesY,
        rasterTileMarginInPx: generalService.ObjectWorldTreeProperties.rasterTileMarginInPx,
    })

    const saveTilingSchemeParams = () => {
        runCodeSafely(() => {
            generalService.ObjectWorldTreeProperties.tilingOriginX = tilingSchemeParamsFormData.tilingOriginX;
            generalService.ObjectWorldTreeProperties.tilingOriginY = tilingSchemeParamsFormData.tilingOriginY;
            generalService.ObjectWorldTreeProperties.tilingSchemeTypeValue = tilingSchemeParamsFormData.tilingSchemeTypeValue!.theEnum;
            generalService.ObjectWorldTreeProperties.largestTileSize = tilingSchemeParamsFormData.largestTileSize;
            generalService.ObjectWorldTreeProperties.tileSizeInPx = tilingSchemeParamsFormData.tileSizeInPx;
            generalService.ObjectWorldTreeProperties.numLargestTilesX = tilingSchemeParamsFormData.numLargestTilesX;
            generalService.ObjectWorldTreeProperties.numLargestTilesY = tilingSchemeParamsFormData.numLargestTilesY;
            generalService.ObjectWorldTreeProperties.rasterTileMarginInPx = tilingSchemeParamsFormData.rasterTileMarginInPx;
            props.onHide();
        }, "TilingSchemeParams.saveObjectProperties => onOk");
    }
    const saveData = (event: { target: { name: any; value: any; }; }) => {
        runCodeSafely(() => {
            setTilingSchemeParamsFormData({ ...tilingSchemeParamsFormData, [event.target.name]: event.target.value });
        }, "TilingSchemeParams.saveData => onChange")
    }
    useEffect(() => {
        const root = document.documentElement;
        let pixelWidth = window.innerHeight * 0.3 * globalSizeFactor;
        root.style.setProperty('--tiling-scheme-params-dialog-width', `${pixelWidth}px`);
    }, [])

    return (
        <Dialog className='scroll-dialog-tiling-scheme-params' style={{ width: `${globalSizeFactor * 1.5 * 50}vh`, height: `${globalSizeFactor * 40}vh` }} header='Tiling Scheme Params' visible={true} onHide={() => { props.onHide(); }}>
            <label htmlFor="tilingSchemeTypeDropDown">Tiling Scheme Type</label>
            <Dropdown id="tilingSchemeTypeDropDown" name='tilingSchemeTypeValue' value={tilingSchemeParamsFormData.tilingSchemeTypeValue} onChange={saveData} options={enumDetails.ETilingSchemeType} optionLabel="name" />
            <Fieldset className="form__space-around form__row-fieldset" legend='Tiling Scheme'>
                <div>
                    <div style={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between', padding: `${globalSizeFactor * 0.3}vh` }}>
                        Tiling Origin:
                        <div >
                            <label htmlFor='tilingOriginX' className="font-bold block mb-2"> X </label>
                            <InputNumber id='tilingOriginX' value={tilingSchemeParamsFormData.tilingOriginX} name='tilingOriginX' onValueChange={saveData} />
                        </div>
                        <div >
                            <label htmlFor='tilingOriginY'> Y </label>
                            <InputNumber id='tilingOriginY' value={tilingSchemeParamsFormData.tilingOriginY} name='tilingOriginY' onValueChange={saveData} />
                        </div>
                    </div>
                    <div style={{ display: 'flex', justifyContent: 'space-between', padding: `${globalSizeFactor * 0.3}vh` }}>
                        <label htmlFor="largestTileSize">Largest Tile Size In Map Units </label>
                        <InputNumber id='largestTileSize' name='largestTileSize' value={tilingSchemeParamsFormData.largestTileSize} style={{ height: `${globalSizeFactor * 4.5}vh` }} onValueChange={saveData} />
                    </div>
                    <div style={{ display: 'flex', justifyContent: 'space-between', padding: `${globalSizeFactor * 0.3}vh` }}>
                        <label htmlFor="tileSizeInPx">Tile Size In Pixels </label>
                        <InputNumber id='tileSizeInPx' name='tileSizeInPx' value={tilingSchemeParamsFormData.tileSizeInPx} style={{ height: `${globalSizeFactor * 4.5}vh` }} onValueChange={saveData} />
                    </div>
                </div>
                <div>
                    <div style={{ display: 'flex', justifyContent: 'space-between', padding: `${globalSizeFactor * 0.3}vh` }}>
                        <label htmlFor="numLargestTilesX">Num Largest Tiles X </label>
                        <InputNumber id='numLargestTilesX' name='numLargestTilesX' value={tilingSchemeParamsFormData.numLargestTilesX} style={{ height: `${globalSizeFactor * 4.5}vh` }} onValueChange={saveData} />
                    </div>
                    <div style={{ display: 'flex', justifyContent: 'space-between', padding: `${globalSizeFactor * 0.3}vh` }}>
                        <label htmlFor="numLargestTilesY">Num Largest Tiles Y </label>
                        <InputNumber id='numLargestTilesY' name='numLargestTilesY' value={tilingSchemeParamsFormData.numLargestTilesY} style={{ height: `${globalSizeFactor * 4.5}vh` }} onValueChange={saveData} />
                    </div>
                    <div style={{ display: 'flex', justifyContent: 'space-between', padding: `${globalSizeFactor * 0.3}vh` }}>
                        <label htmlFor="rasterTileMarginInPx">Raster Tile Margin In Pixels </label>
                        <InputNumber id='rasterTileMarginInPx' name='rasterTileMarginInPx' value={tilingSchemeParamsFormData.rasterTileMarginInPx} style={{ height: `${globalSizeFactor * 4.5}vh` }} onValueChange={saveData} />
                    </div>
                </div>
            </Fieldset>
            <Footer onOk={saveTilingSchemeParams} label="Ok"></Footer>
        </Dialog>
    )
}

