import generalService from '../../../services/general.service';
import store from '../../../redux/store';

export const runAsyncCodeSafely = async (func: () => void, funcName: string, callbackOnCatch?: (error:any) => void) => {
  if (store.getState().mapCoreReducer.isCatchErrors) {
    try {
      await func();
    } catch (error) {
      treatError(error, funcName);
      callbackOnCatch && callbackOnCatch(error)
    }
  }
  else {
    await func();
  }
}

export const runCodeSafely = (func: () => void, funcName: string, callbackOnCatch?: (error:any) => void) => {
  if (store.getState().mapCoreReducer.isCatchErrors) {
    try {
      func();
    } catch (error) {
      treatError(error, funcName);
      callbackOnCatch && callbackOnCatch(error)
    }
  }
  else {
    func();
  }
}

export const runMapCoreSafely = (func: () => void, funcName: string, throwError: boolean, callbackOnCatch?: (error:any) => void) => {
  if (store.getState().mapCoreReducer.isCatchErrors) {
    try {
      func();
    } catch (error: any) {
      callbackOnCatch && callbackOnCatch(error)
      if (error instanceof MapCore.CMcError) {
        console.error(`MapCore Error at ${funcName}`, error);
        throw new Error(`MapCore Error: ${error.message}, \n Error code: ${(error.name as any).value}, \n funcName: ${funcName}`);
      }
      else {
        console.error(`Failed at ${funcName}`, error);
        throw new Error(`Failed at ${funcName} \n ${error} `);
      }
    }
  }
  else {
    func();
  }
}
export const treatError = (error: any, funcName: string,) => {
  // error to console
    console.error(error);
  
  // message to the alert
  let message = `${error.message} \n Method Name: ${funcName}.`;
  if (generalService.ErrorDisplayAlert) {
    alert(message)
  }
  // downlaod fill in testing
  const urlParams = new URLSearchParams(window.location.search);
  let printViewportPath = urlParams.get('printViewportPath');

  if (printViewportPath) {
    generalService.createAnddownloadFile(`autorender_failed_${printViewportPath}`, message);
  }
}

