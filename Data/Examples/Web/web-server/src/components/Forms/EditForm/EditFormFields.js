export const validationMessages = {
    required: 'Required Field',
    rangeValidation: `Only range of numbers [-128 - +127] are allowed`,
    arrayNotEmpty: 'Please select at least one group',
    layerNameEmpty: "Without sub-layer name, title can't be empty",
    positiveNumber: 'Only positive numbers are allowed',
}

export const validationFunctions = {
    required: (value) => value && value.length > 0,
    rangeValidation: (value) => value < 127 && value > -128,
    positiveNumber: (value) => value && value >= 0,
    arrayNotEmpty: (array) => array && array.length > 0,
    layerNameEmpty: (value) => value && value.length > 0,
}

const validationTypes = {
    required: 'required',
    rangeValidation: 'rangeValidation',
    positiveNumber: 'positiveNumber',
    arrayNotEmpty: 'arrayNotEmpty',
    layerNameEmpty: 'layerNameEmpty',
}

export const fieldsValidation = {
    groupName: [validationTypes.required],
    layerId: [validationTypes.required],
    title: [validationTypes.layerNameEmpty],
    drawPriority: [validationTypes.rangeValidation],
    epsg: [],//appear just in raw layers so dont need the required validation

    /*Raw Vector Fields*/
    dataSource: [validationTypes.required],
    sourceEpsg: [],
    minScale: [],
    maxScale: [],
    textureFile: [],
    selectedGroups: [validationTypes.arrayNotEmpty],

    /* 3D vector extrusion fields */
    selectedDataSource: [validationTypes.required],
    dtmLayerId: [validationTypes.required],
    heightColumn: [validationTypes.required],
    objectIdColumn: [validationTypes.required],
    sideTexture: [],
    roofTexture: [],

    // 3D Model
    targetHighestResolution: [validationTypes.positiveNumber],


}

// export const fieldsInfo = {
//     drawPriority: {
//         title: 'Draw Priority',
//         text: 'Set priority order of layers. Higher number means a higher draw priority. The value range is between -128 and 127.'
//     },
//     epsg: {
//         text: 'Enter EPSG code or coordinate system name.',
//         title: 'Coordinate System'
//     },
//     sourceEpsg: {
//         text: 'Enter EPSG code or coordinate system name.',
//         title: 'Source Coordinate System'
//     },
//     targetEpsg: {
//         text: 'Enter EPSG code or coordinate system name.',
//         title: 'Target Coordinate System'
//     },
//     minScale: {
//         text: 'Enter minimum scale for layer display based on screen resolution units (m/pixels)',
//         title: 'Min Scale (m/pixel)'
//     },
//     maxScale: {
//         text: 'Enter maximum scale for layer display based on screen resolution units (m/pixels).',
//         title: 'Max Scale (m/pixel)'
//     },
//     dtmLayerId: {
//         text: 'Choose a DTM layer that references the terrain used by the 3D Vector Extrusion layer',
//         title: 'DTM Layer ID'
//     },
//     useSpatiallyIndexing: {
//         text: "If true then spatial indexing data will be used (and created if not yet exist alongside raw data)",
//         title: 'Use spatial indexing'
//     },
//     heightColumn: {
//         text: "Whether contour heights are in database column or in contour points",
//         title: 'Height Column'
//     },
//     dataSource: {
//         text: "Vector information data source",
//         title: 'Data Source'
//     },
//     sideTexture: {
//         text: 'A texture used to render building walls',
//         title: 'Side Texture'
//     },
//     roofTexture: {
//         text: "A texture used to render building tops",
//         title: 'Roof Texture'
//     }

// }