export const validationMessages = {
    required: 'Required Field'
}

export const validationFunctions = {
    required: (value) => value
}

const validationTypes = {
    required: 'required'
}

export const fieldsValidation = {
    SuffixUrl: [validationTypes.required],
    MemoryCacheSize: [validationTypes.required],
    DiskCacheSize: [validationTypes.required],
    DiskCacheFolder: [validationTypes.required],
    TilingSchemePolicy: [],
    IntermediateFilesFolder: [validationTypes.required],
    GpuUsageInSpatialQueries: [validationTypes.required],
    SSLUsage: [],
    MapScaleFactor: [],
    DefaultTextIndexFilePath:[],
    SecurityRecordFileName:[],
    ImportScansInterval:[],
    IsClientVerPre_7_11_4:[]
}
