import { JsonViewportWindow, ViewportWindow } from 'mapcore-lib';
import { ADD_VIEWPORT_JSON, ADD_VIEWPORT_STANDARD, ADD_SECTION_MAP_VIEWPORT_DETAILED, CLOSE_VIEWPORT, ADD_VIEWPORT_DETAILED, RESIZE_VIEWPORT, SET_CURSOR_POS, SET_CURRENT_VIEWPORT, SET_SCREEN_POS, SET_VIEWPORTS, SET_VIEWPORT_POSITION, RESET_VIEWPORTS, SET_MAP_SCALE_BOX, SET_SCALE_BOX, SET_ACTIVE_CARD, SET_NEW_GENERATED_OBJECT, SET_MAP_WINDOW_DIALOG_TYPE, SET_CREATE_TEXT, SET_CURSOR_RGB, ADD_OPPOSITE_DIMENSION_VIEWPORT } from "../actionTypes";
import { AppState } from "../combineReducer";

export interface MapWindowState {
  size: { width: number, height: number }
  viewportWindows: ViewportWindow[],
  cursorPos: any,
  mapScaleBox?: number,
  scaleBox?: number,
  currentViewport: number;
  screenPos: MapCore.SMcVector3D;
  cursorRgb: { R: number, G: number, B: number },
  activeCard: number,
  newGeneratedObject: MapCore.IMcObject,
  mapWindowDialogType: string,
  createText: boolean,
};

const initialState: MapWindowState = {
  viewportWindows: [],
  size: { width: 400, height: 300 },
  cursorPos: null,
  currentViewport: 0,
  mapScaleBox: undefined,
  scaleBox: undefined,
  cursorRgb: { R: 0, G: 0, B: 0 },
  screenPos: null,
  activeCard: 0,
  newGeneratedObject: null,
  mapWindowDialogType: null,
  createText: false,
};

const selectMaxViewportId = (state: AppState) => {
  let max: number = 0;
  state.mapWindowReducer.viewportWindows.forEach((item: ViewportWindow) => {
    max = Math.max(item.id, max);
  });
  return max
}

const selectViewportPosition = (state: AppState, viewportId: number) => {
  return state.mapWindowReducer.viewportWindows.find(vp => vp.id == viewportId)?.position;
}

const serverReducer = (state = initialState, action: { type: string; payload: any; }) => {
  switch (action.type) {

    case ADD_VIEWPORT_STANDARD:
      return {
        ...state,
        viewportWindows: [...state.viewportWindows, action.payload.standardViewportWindow],
      };
    case ADD_VIEWPORT_JSON:
      return {
        ...state,
        viewportWindows: [...state.viewportWindows, new JsonViewportWindow(action.payload.id, { x: 1, y: 1 }, action.payload.jsonFilePath, action.payload.jsonDataBuffer, action.payload.printViewportPath)],
      };
    case ADD_VIEWPORT_DETAILED:
      return {
        ...state,
        viewportWindows: [...state.viewportWindows, action.payload.detailedViewportWindow],
      };
    case ADD_SECTION_MAP_VIEWPORT_DETAILED:
      return {
        ...state,
        viewportWindows: [...state.viewportWindows, action.payload.detailedSectionMapViewportWindow],
      };
    case ADD_OPPOSITE_DIMENSION_VIEWPORT:
      return {
        ...state,
        viewportWindows: [...state.viewportWindows, action.payload.oppositeDimensionViewportWindow],
      };
    case SET_VIEWPORTS:
      return {
        ...state,
        viewportWindows: [...state.viewportWindows, ...action.payload],
      };
    case CLOSE_VIEWPORT:
      return {
        ...state,
        viewportWindows: state.viewportWindows.filter(x => x.id != action.payload),
        activeCard: state.activeCard == action.payload ? 0 : state.activeCard
      };
    case RESIZE_VIEWPORT:
      return {
        ...state,
        size: action.payload
      };
    case SET_VIEWPORT_POSITION:
      return {
        ...state,
        viewportWindows: [...state.viewportWindows.filter(vp => vp.id !== action.payload.viewportId),
        { ...state.viewportWindows.find(vp => vp.id === action.payload.viewportId), position: action.payload.position }]
      };
    case RESET_VIEWPORTS:
      return {
        ...state,
        viewportWindows: [],
      };
    case SET_CURSOR_RGB:
      return {
        ...state,
        cursorRgb: action.payload,
      };
    case SET_CURSOR_POS:
      return {
        ...state,
        cursorPos: action.payload,
      };
    case SET_MAP_SCALE_BOX:
      return {
        ...state,
        mapScaleBox: action.payload,
      };
    case SET_SCALE_BOX:
      return {
        ...state,
        scaleBox: action.payload,
      };
    case SET_SCREEN_POS:
      return {
        ...state,
        screenPos: action.payload,
      };

    case SET_CURRENT_VIEWPORT:
      return {
        ...state,
        currentViewport: action.payload,
      };
    case SET_ACTIVE_CARD:
      return {
        ...state,
        activeCard: action.payload,
      };
    case SET_NEW_GENERATED_OBJECT:
      return {
        ...state,
        newGeneratedObject: action.payload,
      };
    case SET_MAP_WINDOW_DIALOG_TYPE:
      return {
        ...state,
        mapWindowDialogType: action.payload,
      };
    case SET_CREATE_TEXT:
      return {
        ...state,
        createText: action.payload,
      };
  }
  return state;
};

export default serverReducer;
export { selectMaxViewportId, selectViewportPosition };


