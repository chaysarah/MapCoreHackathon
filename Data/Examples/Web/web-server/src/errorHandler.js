
import { useState } from 'react';
import { useDispatch } from 'react-redux';
import ApplicationContext from './context/applicationContext';
import { SetErrorInPreview } from './redux/MapContainer/MapContainerAction';
import store from './redux/store';

export const runAsyncCodeSafely = async (func, funcName) => {
    try {
        await func();
    } catch (error) {
        treatError(error, funcName);
    }
}

export const runCodeSafely = (func, funcName) => {
    try {
        func();
    } catch (error) {
        treatError(error, funcName);
    }
}
export const runMapCoreSafely = (func, funcName, throwError) => {
    try {
        func();
    } catch (error) {

        if (error instanceof window.MapCore.CMcError) {
            console.error(`MapCore failed at ${funcName}`, error);
            if (throwError) {
                throw new Error(` ${error.message} ,Error code: ${(error.name).value}`);

                // let er = new MapCoreBugError()
                // er.message=error.message
                // er.error = error;
                // er.cause=error;
                // er.funcName = funcName
                // throw er
            }
        }
        else {
            console.error(`Failed at ${funcName}`, error);
            throw new Error(error);
        }
    }
}
//V
export const treatError = (error, funcName) => {
    if (!Number.isNaN(parseInt(error.message))) {
        console.error(`Failed at method ${funcName} the stack in the warning`, error);
    }
    else {
        console.error(`Failed at method ${funcName}`, error);
    }

    let message;
    if (error instanceof window.MapCore.CMcError) {
        message = `${error.message} \nError code:${(error.name).value}.\n Method Name:${funcName}.`;
        // alert(message)
    }
    else if (error instanceof Error) {
        message = `${error.message} \nError code:${error.name}.\n Method Name:${funcName}.`;
    }
}
