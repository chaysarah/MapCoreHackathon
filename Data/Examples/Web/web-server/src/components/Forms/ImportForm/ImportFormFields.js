export const validationMessages = {
    layerIdValidation: `Only [a-zA-Z0-9_-] characters are allowed`,
    rangeValidation: `Only range of numbers [-128 - +127] are allowed`,
    required: 'Required Field',
    layerIdNotExistValidation: 'Layer Id already exist',
    positiveNumber: 'Only positive numbers are allowed',
}

export const validationFunctions = {
    required: (value) => value && value != 0,
    rangeValidation: (value) => value < 127 && value > -128,
    layerIdValidation: (value) => {
        const regexp = /^[a-zA-Z0-9-_]+$/;
        return regexp.test(value);
    },
    layerIdNotExistValidation: (value, groupsTree) => {
        let groupsTreeFlat = groupsTree.map(group => group.childNodes).flat();
        let flag = true;
        groupsTreeFlat.forEach((layer, ind) => {
            if (layer.LayerId == value) {
                flag = false;
            }
        });
        return flag;
    },
    positiveNumber: (value) => value && value >= 0,
}

const validationTypes = {
    required: 'required',
    rangeValidation: 'rangeValidation',
    layerIdValidation: 'layerIdValidation',
    layerIdNotExistValidation: 'layerIdNotExistValidation',
    positiveNumber:'positiveNumber',
}

export const fieldsValidation = {
    groupName: [],
    layerId: [validationTypes.layerIdValidation, validationTypes.required, validationTypes.layerIdNotExistValidation],
    title: [validationTypes.required],
    drawPriority: [validationTypes.rangeValidation],
    epsg: [validationTypes.required],

    /*Raw Vector Fields*/
    dataSource: [validationTypes.required],
    versionCompatibility: [],
    sourceEpsg: [],
    minScale: [],
    maxScale: [],
    textureFile: [],

    /* S-57 fields */
    locale: [],

    /* 3D model fields */
    orthometricHeights: [],
    targetHighestResolution: [validationTypes.positiveNumber],

    /* 3D vector extrusion fields */
    // selectedDataSource: [validationTypes.required],
    dtmLayerId: [validationTypes.required],
    heightColumn: [validationTypes.required],
    objectIdColumn: [validationTypes.required],
    sideTexture: [],
    roofTexture: [],
    sldFile: [],
}

export const fieldsInfo = {
    layerId: 'Unique, case-sensitive string containing letters a-Z, numbers 0-9, dash, and underscore.',
    title: 'The display name of the layer. May be used by the client application as the layer’s name.',
    groupName: 'Name of group to which this layer is added (read-only).',
    drawPriority: 'Set priority order of layers. Higher number means a layer with higher priority is rendered above a layer with a lower one. The value range is between -128 and 127.',
    epsgTarget: 'Select target coordinate system, otherwise source coordinate system is used.',
    epsgTargetS57: 'Select target coordinate system, otherwise source coordinate system (found in the source data) is used.',
    epsgSource: 'Select source coordinate system, otherwise it is taken from the data. Without source coordinate system, layer cannot be added.',
    epsg: 'Select coordinate system, otherwise it is taken from the data. Without coordinate system, layer cannot be added (unless image coordinate system is True).',
    epsgDtm: 'Select coordinate system, otherwise it is taken from the data. Without coordinate system, layer cannot be added.',
    epsg3DModel: 'Select target coordinate system, otherwise source coordinate system (found in the source data) is used. Target coordinate system is mandatory for Cesium 3DTiles format but otherwise optional.',
    epsg3DTiles: 'When Raw 3DModel is of "Cesium 3DTiles" format Target Coordinate System is mandatory.',
    minScale: 'Enter value to limit minimum scale for layer display.',
    maxScale: 'Enter value to limit maximum scale for layer display.',
    maxDtmScale: `Layer's typical maximal scale for source data without built-in pyramid of levels-of-detail. Best performance is achieved in camera scales equal to or lower than specified value.`,
    versionCompatibility: "Specify when client applications use MapCore SDK version that is older than that of the server.",
    orthometricHeights: "Select True if source heights are orthometric (above geoid / sea level) or False if source heights are ellipsoid (above source coordinate system's ellipsoid).",
    useSpatiallyIndexing: "Select True to use spatial indexing data (and create it, if necessary, in same folder as raw data). Recommended for use with data source without built-in spatial indexing.",
    targetHighestResolution: "Specify highest (finest) resolution in map units per pixel to be used instead of that specified in source data.",
    locale: "Select the language used for texts in this layer.",
    dtmLayerId: 'Specify the name of the DTM layer in which terrain heights are provided.',
    dtmLayerIdOfAdd: 'Specify the name of the DTM layer in which terrain heights are provided (select target coordinate system first).',
    heightColumn: "Specify name of database column in which building height-above-ground is provided. Uncheck parameter if building height-above-sea is provided in building points.",
    objectIdColumn: "Name of database column to use object IDs instead of vector item IDs.",
    dataSourceVector: "Data source for vector information. If MapCore styling XML is used, specify the relevant xml file.",
    dataSourceVector3DExtr: "Data source for vector polygon.",
    sideTexture: 'Select file containing image used as texture for rendering building walls.',
    roofTexture: "Select file containing image used as texture for rendering building tops.",
    SldFile: "Vector information sld file.",
    textureFile: "If MapCore XML styling is not provided, specify customized image file with which to replace the default point icon.",
    highestResolution: 'Specify highest (finest) resolution in map units per DTM point to be used instead of that specified in source data.',
}