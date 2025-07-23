import generalService from "./general.service";
import { runCodeSafely } from "../common/services/error-handling/errorHandler";
import store from "../redux/store";
import { MapCoreData } from 'mapcore-lib';
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';

export default class EditModeNavigation {
    isEditModeNavigationMode: boolean = false;

    handleEditModeNavigationClick = () => {
        runCodeSafely(() => {
            let activeCard = store.getState().mapWindowReducer.activeCard;
            if (!this.isEditModeNavigationMode) {
                this.isEditModeNavigationMode = true;
                if (generalService.EditModePropertiesBase.NavigateMapWaitForMouseClick) {
                    let activeViewport = MapCoreData.findViewport(activeCard);
                    activeViewport.editMode.StartNavigateMap(generalService.EditModePropertiesBase.DrawLine,
                        generalService.EditModePropertiesBase.OneOperationOnly);
                }
            }
            else {
                this.isEditModeNavigationMode = false;
                let activeViewport = MapCoreData.findViewport(activeCard);
                activeViewport.editMode.IsEditingActive() && activeViewport.editMode.ExitCurrentAction(false);
            }
        }, 'mapWindow.handleEditModeNavigationClick')
    }
    editModeNavigationMouseDownHandler = () => {
        let activeCard = store.getState().mapWindowReducer.activeCard;
        let screenPos = store.getState().mapWindowReducer.screenPos;
        if (this.isEditModeNavigationMode && !generalService.EditModePropertiesBase.NavigateMapWaitForMouseClick) {
            let EItemSubTypeFlagsList = getEnumDetailsList(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags);
            let screenEnumCode = getEnumValueDetails(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN, EItemSubTypeFlagsList).code;
            let navigationDefaultLine = MapCore.IMcLineItem.Create(screenEnumCode,
                MapCore.IMcLineBasedItem.ELineStyle.ELS_SOLID, MapCore.bcBlackOpaque, 5);
            let activeViewport = MapCoreData.findViewport(activeCard);
            activeViewport.editMode.StartNavigateMap(generalService.EditModePropertiesBase.DrawLine,
                generalService.EditModePropertiesBase.OneOperationOnly,
                generalService.EditModePropertiesBase.NavigateMapWaitForMouseClick,
                screenPos, navigationDefaultLine);
        }
    }
    editModeNavigationMouseUpHandler = () => {
        let activeCard = store.getState().mapWindowReducer.activeCard;
        let activeViewport = MapCoreData.findViewport(activeCard);
        if (this.isEditModeNavigationMode) {
            this.isEditModeNavigationMode = false;
            activeViewport.editMode.IsEditingActive() && activeViewport.editMode.ExitCurrentAction(false);
        }
    }
}
