import { MutableRefObject } from "react";

import { getEnumDetailsList, getEnumValueDetails } from "mapcore-lib";
import generalService from "./general.service";
import { runAsyncCodeSafely, runCodeSafely, runMapCoreSafely } from "../common/services/error-handling/errorHandler";

class ScreenPositionService {
    private TEXT_PROPERTY_ID = 1;
    private COLOR_PROPERTY_ID = 2;

    public getObjectSchemeScreenPoints = async (mcViewport: MapCore.IMcMapViewport, internalUseOverlay: MapCore.IMcOverlay) => {
        let mcObjectScheme: MapCore.IMcObjectScheme = null;
        await runAsyncCodeSafely(async () => {
            const charachterRanges: MapCore.IMcFont.SCharactersRange[] = [{ nFrom: '0'.charCodeAt(0), nTo: '9'.charCodeAt(0) }];
            const fileSource = await generalService.getFileSourceByUrl('http:arial.ttf');
            let defaultFont: MapCore.IMcFileFont = null;
            runMapCoreSafely(() => {
                defaultFont = MapCore.IMcFileFont.Create(fileSource, 15, false, charachterRanges);
            }, 'ScreenPositionService.getObjectSchemeScreenPoints => IMcFileFont.Create', true)
            const EItemSubTypeFlags = getEnumDetailsList(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags);
            const subItemsFlag = getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN, EItemSubTypeFlags).code;
            let textItem: MapCore.IMcTextItem = null;
            runMapCoreSafely(() => {
                textItem = MapCore.IMcTextItem.Create(subItemsFlag, MapCore.EMcPointCoordSystem.EPCS_SCREEN, defaultFont);
            }, 'ScreenPositionService.getObjectSchemeScreenPoints => IMcTextItem.Create', true)
            const overlayManager: MapCore.IMcOverlayManager = mcViewport.GetOverlayManager();
            runMapCoreSafely(() => {
                mcObjectScheme = MapCore.IMcObjectScheme.Create(overlayManager, textItem, MapCore.EMcPointCoordSystem.EPCS_SCREEN);
            }, 'ScreenPositionService.getObjectSchemeScreenPoints => IMcObjectScheme.Create', true)

            runMapCoreSafely(() => { textItem.SetText(new MapCore.SMcVariantString('', true), this.TEXT_PROPERTY_ID) }, 'ScreenPositionService.getObjectSchemeScreenPoints => IMcTextItem.SetText', true)
            runMapCoreSafely(() => { textItem.SetTextColor(new MapCore.SMcBColor(), this.COLOR_PROPERTY_ID) }, 'ScreenPositionService.getObjectSchemeScreenPoints => IMcTextItem.SetTextColor', true)
            runMapCoreSafely(() => { textItem.SetDrawPriorityGroup(MapCore.IMcSymbolicItem.EDrawPriorityGroup.EDPG_TOP_MOST) }, 'ScreenPositionService.getObjectSchemeScreenPoints => IMcSymbolicItem.SetDrawPriorityGroup', true)
            runMapCoreSafely(() => { textItem.SetRectAlignment(MapCore.IMcSymbolicItem.EBoundingRectanglePoint.EBRP_BOTTOM_MIDDLE) }, 'ScreenPositionService.getObjectSchemeScreenPoints => IMcTextItem.SetRectAlignment', true)

            runMapCoreSafely(() => { internalUseOverlay.SetVisibilityOption(MapCore.IMcConditionalSelector.EActionOptions.EAO_FORCE_FALSE); }, 'ScreenPositionService.getObjectSchemeScreenPoints => IMcOverlay.SetVisibilityOption', true)
            runMapCoreSafely(() => { internalUseOverlay.SetVisibilityOption(MapCore.IMcConditionalSelector.EActionOptions.EAO_FORCE_TRUE, mcViewport); }, 'ScreenPositionService.getObjectSchemeScreenPoints => IMcOverlay.SetVisibilityOption', true)
        }, 'ScreenPositionService.getObjectSchemeScreenPoints')
        return mcObjectScheme;
    }
    public createScreenPointObject = (internalUseOverlayRef: MutableRefObject<MapCore.IMcOverlay>, scheme: MapCore.IMcObjectScheme, point: MapCore.SMcVector2D, color: MapCore.SMcBColor, text: string) => {
        runCodeSafely(() => {
            let iconMcObject: MapCore.IMcObject = null;
            const locationPoints = [new MapCore.SMcVector3D(point)]
            runMapCoreSafely(() => { iconMcObject = MapCore.IMcObject.Create(internalUseOverlayRef.current, scheme, locationPoints); }, 'ScreenPositionService.createScreenPointObject => IMcObject.Create', true);
            runMapCoreSafely(() => { iconMcObject.SetStringProperty(this.TEXT_PROPERTY_ID, new MapCore.SMcVariantString(`${text}`, true)); }, 'ScreenPositionService.createScreenPointObject => IMcObject.SetStringProperty', true);
            runMapCoreSafely(() => { iconMcObject.SetBColorProperty(this.COLOR_PROPERTY_ID, color); }, 'ScreenPositionService.createScreenPointObject => IMcObject.SetBColorProperty', true);
        }, 'ScreenPositionService.createScreenPointObject')
    }
}
export default new ScreenPositionService;