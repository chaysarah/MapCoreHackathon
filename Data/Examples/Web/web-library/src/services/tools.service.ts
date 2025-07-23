//#region Enums

const getEnumDetailsList = (enumType: any): { name: string, code: number, theEnum: any }[] => {
    let arr: any[] = []
    let enumCode: number[] = Object.values(enumType);
    let enumsName: string[] = Object.keys(enumType);

    for (let index: number = 1; index < enumCode.length; index++) {
        if (enumsName[index] != "argCount") { // for emsdk 4.0.9 mapcore compilation
            arr.push({ name: enumsName[index], code: (enumCode[index] as any).value, theEnum: enumCode[index] });
        }
    }
    arr = arr.filter(e => e.name.slice(-4) != "_NUM")
    return arr;
}
const getEnumValueDetails = (enumValue: any, enumDetailsList: { name: string, code: number, theEnum: any }[]): any => {
    let code: number;
    if (Number.isFinite(enumValue))
        code = enumValue;
    else
        code = enumValue.value;
    if (Number.isFinite(enumValue)) {
        return enumDetailsList.filter(f => (code & f.code) == f.code);
    }
    else {
        return enumDetailsList.find(f => f.code == code)
    }
}
const getBitFieldByEnumArr = (enumArr: { name: string, code: number, theEnum: any }[]): number => {
    let bitField = 0;
    enumArr.forEach(enumObj => {
        bitField = bitField | enumObj.code;
    })
    return bitField;
}

//#endregion

export { getEnumValueDetails, getEnumDetailsList, getBitFieldByEnumArr };
