import store from "../redux/store";
import { runCodeSafely } from "../common/services/error-handling/errorHandler";
import { selectMaxViewportId } from "../redux/mapWindow/mapWindowReducer";
import { addOppositeDimensionViewport, addViewportStandard } from "../redux/mapWindow/mapWindowAction";
import { ViewportData, MapCoreData, Layerslist, ViewportParams, StandardViewportWindow, OppositeDimensionViewportWindow } from "mapcore-lib";

class OpenOppositeMapService {
    private blockOpenMapCases = () => {
        let isBlockMessageObj = { isBlock: false, message: null };
        runCodeSafely(() => {
            const activeCard = store.getState().mapWindowReducer.activeCard;
            const activeViewport: ViewportData = MapCoreData.findViewport(activeCard);
            if (!activeViewport) {
                isBlockMessageObj.isBlock = true;
                isBlockMessageObj.message = "No active overlay";
                return;
            }
            const is2DMapType = activeViewport.viewport.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_2D;
            const isImageCalc = activeViewport.viewport.GetImageCalc();
            const isSectionMap = MapCore.IMcSectionMapViewport.INTERFACE_TYPE == activeViewport.viewport.GetInterfaceType();
            if (is2DMapType && isImageCalc) {
                isBlockMessageObj.isBlock = true;
                isBlockMessageObj.message = "Image Calc Viewport Can't Opened As 3D Map.";
            }
            if (is2DMapType && isSectionMap) {
                isBlockMessageObj.isBlock = true;
                isBlockMessageObj.message = "Section Map Viewport Can't Opened As 3D Map.";
            }
        }, 'OpenOppositeMapService.blockOpenMapCases')
        return isBlockMessageObj;
    }
    public openOppositeDimensionMap = () => {
        const isBlockMessage = this.blockOpenMapCases();
        runCodeSafely(() => {
            if (!isBlockMessage.isBlock) {
                const activeCard = store.getState().mapWindowReducer.activeCard;
                const activeViewport: ViewportData = MapCoreData.findViewport(activeCard);
                const maxLayerId: number = selectMaxViewportId(store.getState());
                const oppositeDimensionViewport = new OppositeDimensionViewportWindow(maxLayerId + 1, { x: 1, y: 1 }, activeViewport);
                store.dispatch(addOppositeDimensionViewport(oppositeDimensionViewport))
            }
        }, 'OpenOppositeMapService.openOppositeDimensionMap')
            return isBlockMessage;
    }
}
export default new OpenOppositeMapService;