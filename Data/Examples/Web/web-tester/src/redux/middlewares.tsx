import { Middleware } from 'redux';
import  { AppAction, CLOSE_DIALOG, SET_SELECTED_NODE_IN_MAP_WORLD_TREE, SET_SELECTED_NODE_IN_TREE } from './actionTypes'
import { useSelector } from 'react-redux';
import { AppState } from './combineReducer';
import generalService from '../services/general.service';
import dialogStateService from '../services/dialogStateService';
 

const confirmMiddleware: Middleware = ({getState}) => (next) => (action: AppAction) => {
    if((action.type === SET_SELECTED_NODE_IN_TREE ||action.type === SET_SELECTED_NODE_IN_MAP_WORLD_TREE || action.type === CLOSE_DIALOG) 
        && dialogStateService.hasDialogChanged())  {
        let message = action.type === SET_SELECTED_NODE_IN_TREE|| action.type === SET_SELECTED_NODE_IN_MAP_WORLD_TREE? 
            'Are you sure you want to move to another node in tree without applying changes?':
            'Are you sure you want to close this dialog without applying changes?';
        const confirmed = window.confirm(message);
        if(confirmed) {
            dialogStateService.applyDialogState();
            return next(action);
        }
        else {
            return;
        }
    }
    return next(action)
}

export default confirmMiddleware;