import { Button } from "primereact/button";
import { Checkbox } from "primereact/checkbox";
import { Dropdown } from "primereact/dropdown";
import { Fieldset } from "primereact/fieldset";
import { InputNumber } from "primereact/inputnumber";
import { useState } from "react";
import { runCodeSafely } from "../../../../common/services/error-handling/errorHandler";
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import { useSelector } from "react-redux";
import { AppState } from "../../../../redux/combineReducer";

export default function TilingSchemeParams(props: { getTilingScheme: (tilingScheme: MapCore.IMcMapLayer.STilingScheme) => void }) {
    let [tilingScheme, setTilingScheme] = useState(MapCore.IMcMapLayer.GetStandardTilingScheme(MapCore.IMcMapLayer.ETilingSchemeType.ETST_MAPCORE))
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    const [enumDetails] = useState({
        TilingSchemeType: getEnumDetailsList(MapCore.IMcMapLayer.ETilingSchemeType),
    });
    let [selectedSchemeType, setSelectedSchemeType] = useState(getEnumValueDetails(MapCore.IMcMapLayer.ETilingSchemeType.ETST_MAPCORE, enumDetails.TilingSchemeType))

    const saveData = (event: any) => {
        setSelectedSchemeType(event.target.value)
        setTilingScheme(MapCore.IMcMapLayer.GetStandardTilingScheme(event.target.value.theEnum))
    }

    const saveTilingSchemeData = (event: any) => {
        setSelectedSchemeType(null)
        runCodeSafely(() => {
            let value = event.target.type === "checkbox" ? event.target.checked : event.target.value;
            let class_name: string = event.originalEvent?.currentTarget?.className;
            if (class_name?.includes("p-dropdown-item")) {
                value = event.target.value.theEnum;
            }
            setTilingScheme({ ...tilingScheme, [event.target.name]: value })
        }, "EditModePropertiesDialog/General.saveData => onChange")
    }
    return (<>
        <div >
            <label>Tiling Scheme Type:</label>
            <Dropdown style={{ width: "30vw" }} name="MouseMoveUsage" value={selectedSchemeType} onChange={saveData} options={enumDetails.TilingSchemeType} optionLabel="name" />
        </div>
        <Fieldset className="form__space-between " legend="Tiling Scheme">
            <div>
                <div>
                    <label>Tiling Origin:</label>
                    <label>X:</label>
                    <InputNumber name="TilingOrigin.x" value={tilingScheme.TilingOrigin.x} onValueChange={(e) => {
                        setTilingScheme({ ...tilingScheme, TilingOrigin: { ...tilingScheme.TilingOrigin, x: e.target.value } })
                        ; setSelectedSchemeType(null)
                    }} mode="decimal" />
                    <label>Y:</label>
                    <InputNumber name="TilingOrigin[y]" value={tilingScheme.TilingOrigin.y} onValueChange={(e) => { setTilingScheme({ ...tilingScheme, TilingOrigin: { ...tilingScheme.TilingOrigin, y: e.target.value } }); setSelectedSchemeType(null) }} mode="decimal" />
                </div>
                <div>
                    <label>Largest Tile Size In Map Units:</label>
                    <InputNumber name="dLargestTileSizeInMapUnits" value={tilingScheme.dLargestTileSizeInMapUnits} onValueChange={saveTilingSchemeData} mode="decimal" />
                </div>
                <div>
                    <label>Tile Size In Pixels:</label>
                    <InputNumber name="uTileSizeInPixels" value={tilingScheme.uTileSizeInPixels} onValueChange={saveTilingSchemeData} mode="decimal" />
                </div>
            </div>
            <div>
                <div>
                    <label>Num Largest Tiles X:</label>
                    <InputNumber name="uNumLargestTilesX" value={tilingScheme.uNumLargestTilesX} onValueChange={saveTilingSchemeData} mode="decimal" />
                </div>
                <div>
                    <label>Num Largest Tiles Y:</label>
                    <InputNumber name="uNumLargestTilesY" value={tilingScheme.uNumLargestTilesY} onValueChange={saveTilingSchemeData} mode="decimal" />
                </div>
                <div>
                    <label>Raster Tile Margin In Pixels:</label>
                    <InputNumber name="uRasterTileMarginInPixels" value={tilingScheme.uRasterTileMarginInPixels} onValueChange={saveTilingSchemeData} mode="decimal" />
                </div>
            </div>
        </Fieldset>
        <div style={{direction:'rtl',marginTop: `${globalSizeFactor * 2}vh`}}>     <Button onClick={() => props.getTilingScheme(tilingScheme)}>OK</Button></div>
   
    </>
    );
}
