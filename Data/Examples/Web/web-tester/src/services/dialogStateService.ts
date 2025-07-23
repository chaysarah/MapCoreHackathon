import _ from "lodash";
import { DialogTypesEnum } from "../tools/enum/enums";
import { getState } from './general.service';


class DialogStateService {
    DialogStates: Map<string, { initState: any; currState: any; }>;

    constructor() {
        this.DialogStates = new Map<string, { initState: any; currState: any; }>();
    }

    initDialogState(path: string, value: any) {
        let dialogKey = this.createDialogKey();
        let mapDialogState = this.DialogStates.get(dialogKey);
        if (!mapDialogState) {
            mapDialogState = { initState: {}, currState: {} };
        }
        this.assignValue(mapDialogState.initState, path, value);
        this.assignValue(mapDialogState.currState, path, value);
        this.DialogStates.set(dialogKey, mapDialogState);
    }

    setDialogState(path: string, value: any) {
        let dialogKey = this.createDialogKey();
        let mapDialogState = this.DialogStates.get(dialogKey);
        if (!mapDialogState) {
        mapDialogState = { initState: {}, currState: {} };
        // throw Error(`${dialogKey} does not exist in states map. call initDialogState(dialogState) first`)
        }
        this.assignValue(mapDialogState.currState, path, value);
        this.DialogStates.set(dialogKey, mapDialogState);
    }

    applyDialogState(pathesToApply?: string[]) {
        let dialogKey = this.createDialogKey();
        let mapDialogState = this.DialogStates.get(dialogKey);
        if (mapDialogState) {
            if (pathesToApply) {
                for (let i = 0; i < pathesToApply.length; ++i) {
                this.assignPath(mapDialogState.initState, mapDialogState.currState, pathesToApply[i]);
                }
            }
            else {//apply all
                // mapDialogState.initState = cloneDeep(mapDialogState.currState)
                this.synchronizeState(mapDialogState.initState, mapDialogState.currState);
            }
        }
        else {
            // throw Error(`${dialogKey} does not exist in states map. call initDialogState(dialogState) first`);
        }
    }


    hasDialogChanged() {
        let dialogKey = this.createDialogKey();
        let mapDialogState = this.DialogStates.get(dialogKey);
        if (mapDialogState) {
            const differences = this.deepCompare(mapDialogState.initState, mapDialogState.currState);
            // console.log(differences);
            if (differences.length !== 0) {
                return true;
            }
        }
        return false;
    }

    /*** private methods ***/
    private createDialogKey() {
        const state = getState();
        let dialogTypesArr = state.mapCoreReducer.dialogTypesArr;
        let selectedNodeInTree = dialogTypesArr[dialogTypesArr.length - 1] === DialogTypesEnum.objectWorldTree ?
        state.objectWorldTreeReducer.selectedNodeInTree : null;
        return dialogTypesArr[dialogTypesArr.length - 1]?.toString() + selectedNodeInTree?.key;
    }

    private assignValue(obj: any, path: string, value: any): void {
        if (!path) {
            throw new Error("Path cannot be empty");
        }

        const keys = path.split('.');
        let current = obj;

        for (let i = 0; i < keys.length; i++) {
            const key = keys[i];

            if (i === keys.length - 1) {
                current[key] = _.cloneDeep(value);
            }
            else {
                if (!current[key] || typeof current[key] !== 'object') {
                    current[key] = {};
                }
                current = current[key];
            }
        }
    }
    private assignPath(destObject: any, srcObject: any, pathToAssign: string): void {
        if (!pathToAssign) {
            throw new Error("Path cannot be empty");
        }

        const keys = pathToAssign.split('.');
        let current1 = destObject;
        let current2 = srcObject;

        for (let i = 0; i < keys.length; i++) {
            const key = keys[i];

            if (i === keys.length - 1 && current2[key] != undefined) {
                current1[key] = _.cloneDeep(current2[key]);
            }
            else {
                if (!current2[key]) {
                    console.log(`Path ${pathToAssign} does not exist in current state`);
                    console.log(destObject);
                    return;
                }
                if (!current1[key] || typeof current1[key] !== 'object') {
                    current1[key] = {};
                }
                current1 = current1[key];
                current2 = current2[key];
            }
        }
    }

    private synchronizeState(destState: any, srcState: any): void {
        for (const key in srcState) {
            if (typeof srcState[key] === 'object' && srcState[key] !== null) {
                if (typeof destState[key] !== 'object' || destState[key] === null) {
                    destState[key] = Array.isArray(srcState[key]) ? [] : {};
                }
                this.synchronizeState(destState[key], srcState[key]);
            }
            else {
                destState[key] = srcState[key];
            }
        }
    }


    private deepCompare(obj1: any, obj2: any): string[] {
        const differences: string[] = [];
        this.compare(obj1, obj2, differences, '');
        return differences;
    }
        
    private compare(obj1: any, obj2: any, differences: string[], path: string): void {
        if (obj1 === obj2) {
            return;
        }

        if (typeof obj1 !== typeof obj2) {
            differences.push(`${path}: Type mismatch (${typeof obj1} vs ${typeof obj2})`);
         return;
        }

        if (typeof obj1 !== 'object' || obj1 === null || obj2 === null) {
            if (obj1 !== obj2) {
                differences.push(`${path}: Value mismatch (${obj1} vs ${obj2})`);
            }
            return;
        }

        const keys2 = Reflect.ownKeys(obj2);
        for (const key of keys2) {
            if (obj2.getInternalUseKeys/* && obj2.getInternalUseKeys().includes(key)*/) {
                continue;
            }
            this.compare(obj1[key], obj2[key], differences, path ? `${path}.${String(key)}` : String(key));
        }
    }
}

export default new DialogStateService()