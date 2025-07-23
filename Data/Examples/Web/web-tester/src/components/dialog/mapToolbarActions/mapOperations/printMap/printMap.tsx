import { ReactElement, useEffect, useState } from "react";
import TabsParentCtrl from "../../../shared/tabCtrls/tabsParentCtrl";
import { TabType } from "../../../shared/tabCtrls/tabModels";
import PrintToRawRaster, { PrintToRawRasterProperties } from "./printToRawRaster";
import { useSelector } from "react-redux";
import { AppState } from "../../../../../redux/combineReducer";
import './styles/printMap.css';
import { Button } from "primereact/button";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";
import { MapCoreData, ViewportData } from "mapcore-lib";
import printMapService, { PrintMapData } from "../../../../../services/printMap.service";
import store from "../../../../../redux/store";

export default function PrintMap(props: { FooterHook: (footer: () => ReactElement) => void }) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const activeViewport: ViewportData = MapCoreData.findViewport(store.getState().mapWindowReducer.activeCard);
    let printMapData: PrintMapData = printMapService.createInstance(activeViewport);

    useEffect(() => {
        const root = document.documentElement;
        let pixelWidth = window.innerHeight * 0.5 * globalSizeFactor;
        root.style.setProperty('--print-map-dialog-width', `${pixelWidth}px`);

        return () => {
            printMapService.deletePrintRects(printMapData);
        }
    }, [])

    const tabTypes: TabType[] = [
        { index: 1, header: 'To Raw Raster', propertiesClass: PrintToRawRasterProperties, component: PrintToRawRaster },
    ]
    const handleCancelAsyncPrintClick = () => {
        runCodeSafely(() => {
            printMapService.cancelAsyncPrint(printMapData);
        }, 'PrintMap/PrintToRawRaster.handleCancelAsyncPrintClick')
    }
    const handleDeletePrintRectsClick = () => {
        runCodeSafely(() => {
            printMapService.deletePrintRects(printMapData);
        }, 'PrintMap/PrintToRawRaster.handleDeletePrintRectsClick')
    }

    return <div className="form__column-container">
        <TabsParentCtrl
            parentName='PrintMapForm'
            tabTypes={tabTypes}
            getDefaultFuncProps={{ currentViewport: activeViewport, printMapData }}
        />
        <div className="form__flex-and-row-between form__footer-padding">
            <Button style={{ width: '45%' }} label="Cancel Async Print" onClick={handleCancelAsyncPrintClick} />
            <Button style={{ width: '45%' }} label="Delete Print Rects" onClick={handleDeletePrintRectsClick} />
        </div>
    </div>
}