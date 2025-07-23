import { DetailedSectionMapViewportWindow, DetailedViewportWindow, OppositeDimensionViewportWindow, StandardViewportWindow, ViewportWindow } from 'mapcore-lib';
import * as actionTypes from "../actionTypes";

const addViewportStandard = (standardViewportWindow: StandardViewportWindow) => {
    return {
        type: actionTypes.ADD_VIEWPORT_STANDARD,
        payload: { standardViewportWindow },
    };
};
const addViewportJson = (id: number, jsonFilePath: string, jsonDataBuffer: { fileName: string, fileBuffer: Uint8Array }[], printViewportPath: string) => {
    return {
        type: actionTypes.ADD_VIEWPORT_JSON,
        payload: { id, jsonFilePath, jsonDataBuffer, printViewportPath },
    };
};
const addViewportDetailed = (detailedViewportWindow: DetailedViewportWindow) => {
    return {
        type: actionTypes.ADD_VIEWPORT_DETAILED,
        payload: { detailedViewportWindow },
    };
};
const addOppositeDimensionViewport = (oppositeDimensionViewportWindow: OppositeDimensionViewportWindow) => {
    return {
        type: actionTypes.ADD_OPPOSITE_DIMENSION_VIEWPORT,
        payload: { oppositeDimensionViewportWindow },
    };
};
const addSectionMapViewportDetailed = (detailedSectionMapViewportWindow: DetailedSectionMapViewportWindow) => {
    return {
        type: actionTypes.ADD_SECTION_MAP_VIEWPORT_DETAILED,
        payload: { detailedSectionMapViewportWindow },
    };
};

const resetViewports = () => {
    return {
        type: actionTypes.RESET_VIEWPORTS,
        payload: 0
    };
};
const closeViewport = (idViewport: number) => {
    return {
        type: actionTypes.CLOSE_VIEWPORT,
        payload: idViewport
    };
};
const resizeViewport = (size: { width: number, height: number }) => {
    return {
        type: actionTypes.RESIZE_VIEWPORT,
        payload: size
    };
};
const setViewportPosition = (viewportId: number, position: { x: number, y: number }) => {
    return {
        type: actionTypes.SET_VIEWPORT_POSITION,
        payload: { viewportId, position }
    };
};
const setCursorRgb = (cursorRgb: any) => {
    return {
        type: actionTypes.SET_CURSOR_RGB,
        payload: cursorRgb
    };
};
const setCursorPos = (pos: any) => {
    return {
        type: actionTypes.SET_CURSOR_POS,
        payload: pos
    };
};
const setMapScaleBox = (mapScaleBox: number) => {
    return {
        type: actionTypes.SET_MAP_SCALE_BOX,
        payload: mapScaleBox
    };
};
const setScaleBox = (scaleBox: number) => {
    return {
        type: actionTypes.SET_SCALE_BOX,
        payload: scaleBox
    };
};
const setScreenPos = (screenPos: MapCore.SMcVector3D) => {
    return {
        type: actionTypes.SET_SCREEN_POS,
        payload: screenPos
    };
};
const setCurrentViewport = (viewportId: number) => {
    return {
        type: actionTypes.SET_CURRENT_VIEWPORT,
        payload: viewportId
    };
};
const setViewports = (viewports: ViewportWindow[]) => {
    return {
        type: actionTypes.SET_VIEWPORTS,
        payload: viewports
    };
}
const setActiveCard = (viewportId: number) => {
    return {
        type: actionTypes.SET_ACTIVE_CARD,
        payload: viewportId
    };
}
const setNewGeneratedObject = (newObject: MapCore.IMcObject) => {
    return {
        type: actionTypes.SET_NEW_GENERATED_OBJECT,
        payload: newObject
    };
}
const setMapWindowDialogType = (dialogType: string) => {
    return {
        type: actionTypes.SET_MAP_WINDOW_DIALOG_TYPE,
        payload: dialogType
    };
}
const setCreateText = (createText: boolean) => {
    return {
        type: actionTypes.SET_CREATE_TEXT,
        payload: createText
    };
}
export {
    addViewportStandard, resizeViewport, resetViewports, closeViewport, setCursorPos, setCursorRgb, setCurrentViewport
    , setViewports, setViewportPosition, setMapScaleBox, setScaleBox, setScreenPos, setActiveCard, addViewportJson
    , setNewGeneratedObject, setMapWindowDialogType, setCreateText, addViewportDetailed, addSectionMapViewportDetailed, addOppositeDimensionViewport
};
