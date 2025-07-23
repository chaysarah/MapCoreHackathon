export const validationMessages = {
    required: 'Required Field',
    arrayNotEmpty: 'Please select at least one group'
}

export const validationFunctions = {
    required: (value) => value && value.length > 0,
    requiredNumber: (value) => value && value >= 0,
    arrayNotEmpty: (array) => array && array.length > 0
}

const validationTypes = {
    required: 'required',
    requiredNumber: 'requiredNumber',
    arrayNotEmpty: 'arrayNotEmpty'
}

export const fieldsValidation = {
    groupName: [validationTypes.required],
    layerId: [validationTypes.required],
    title: [validationTypes.required],
    drawPriority: [],
    epsg: [validationTypes.requiredNumber],
    
    /*Raw Vector Fields*/
    dataSource: [validationTypes.required],
    sourceEpsg: [validationTypes.requiredNumber],
    minScale: [],
    maxScale: [],
    textureFile: [],
    selectedGroups: [validationTypes.arrayNotEmpty],
    selectedDataSource: [validationTypes.required],
    dtmLayerId: [validationTypes.required],
    heightColumn: [], 
    sideTexture: [],
    roofTexture: [],
}