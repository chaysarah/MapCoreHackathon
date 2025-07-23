export const validationMessages = {
    required: 'Required Field',
    floatOrIntValue: `Only integer or float numbers are allowed`,
    intValue: 'Only integer numbers are allowed',
}

export const validationFunctions = {
    required: (value) => value && value != 0,
    floatOrIntValue: (value) => {
        const floatRegex = /^-?\d*(\.\d+)?$/;
        const integerRegex = /^\d+$/;
        return (floatRegex.test(value) || integerRegex.test(value)) && value;
    },
    intValue: (value) => {
        const integerRegex = /^\d+$/;
        return integerRegex.test(value);
    },
}

const validationTypes = {
    required: 'required',
    intValue: 'intValue',
    floatOrIntValue: 'floatOrIntValue',
}

export const fieldsValidation = {
    optimizationMinScale: [validationTypes.floatOrIntValue],
    minSizeForObjVisibility: [validationTypes.intValue, validationTypes.required],
    maxVisiblePointsPerTile: [validationTypes.intValue, validationTypes.required],
}

export const fieldsInfo = {
    //raster advanced fields
    enhanceBorderOverlap: 'Set to True to prevent border display where layers overlap (affects performance).',
    resolveOverlapConflicts: 'Select True to ignore transparent pixels (affects performance). Select False to enforce transparent pixels (pixels from other source files may not be displayed).',
    transparentColor: 'Check to select color to be treated as transparent. To select a range of colors, specify the degree of precision: for each R/G/B channel, pixels with that value plus and minus the precision will be treated as transparent.',
    ignoreRasterPalette: 'Select True to ignore the raster palette and show as grayscale.',
    highestResolution: 'Specify highest (finest) resolution in map units per pixel to be used instead of that specified in source data.',
    fillEmptyTilesByLowerResolutionTiles: 'Use only when the highest resolution varies within a single levels-of-detail-based layer. Select True to fill empty tiles with data from lower-resolution tiles.',
    maxScale: `Layer's typical maximal scale for source data without built-in pyramid of levels-of-detail. Best performance is achieved in camera scales equal to or lower than specified value.`,
    imageCoordinateSystem: 'Used to define whether image is not georeferenced. Set True for non-orthographic images (in which case the coordinate system parameter is not relevant and therefore not mandatory).',
    //vector advanced fields
    OptimizationOfHidingObjects: 'Check to specify optimization parameters.',
    maxVerticesPerTile: 'Maximal number of vector object vertices assigned to tile. Higher numbers improve performance and memory consumption in zoom-in; smaller numbers improve performance in zoom-out.',
    optimizationMinScale: 'Minimal camera scale for optimization (0 means perform at all scales).',
    maxVisiblePointsPerTile: 'Maximal number of visible vector point objects per tile; objects exceeding this threshold are not displayed (actual number of visible objects depends on camera scale so that the threshold impact lessens when zooming in). Use to avoid graphic system overload when zooming out in layers with many point objects.',
    minSizeForObjVisibility: 'Minimal threshold (in pixels) for displaying vector lines and polygons; objects smaller than the threshold are not displayed (actual number of visible objects depends on camera scale so that the threshold impact lessens when zooming in). Use to avoid graphic system overload when zooming out in layers with many lines and polygons.',

}