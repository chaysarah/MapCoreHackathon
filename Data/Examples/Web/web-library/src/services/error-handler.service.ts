import mapcoreData from "../mapcore-data";

export const runCodeLibrarySafely = (func: () => void, funcName: string) => {
  if (mapcoreData.isCatchErrors) {
    try {
      func();
    } catch (error) {
      treatError(error, funcName);
    }
  }
  else {
    func();
  }
}
export const runMapCoreSafely = (func: () => void, funcName: string, throwError: boolean, callbackOnCatch?: (error:any) => void) => {
  if (mapcoreData.isCatchErrors) {
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

const treatError = (error: any, funcName: string) => {
  if (mapcoreData.onErrorCallback)
    mapcoreData.onErrorCallback(error, funcName)
  else
    console.error(`Failed at method ${funcName}`, error);
}
