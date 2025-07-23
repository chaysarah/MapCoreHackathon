const config = {
  importOptionsArr: [
    { code: 'RASTER', value: 'Raster Map Layer' },
    { code: 'VECTOR', value: 'Vector Map Layer' },
    { code: 'DTM', value: 'DTM Map Layer' },
    { code: 'MCPACKAGE', value: 'MC Package' }
  ],
  FILE_UPLOAD_MAX_SIZE_IN_BYTES: 104857600, // 0.5GB => 512 * 1024 * 1024  = 536870912 bytes
  LAYERS_MAX_SIZE_FOR_ONE_EXPORT: 112197618,
  urls: {
    backlogOps: 'port/map/opr?backlog-ops',
    uploadFiles: 'upload-files',
    editLayerInfo: 'edit-layer-info',
    changeLayerGroup: 'map/change-layer-group',
    removeLayer: 'remove-layer',
    renameGroup: 'rename-group',
    layersInfo: 'layers-info',
    getCapabilities: 'map/opr?service=wmts&request=GetCapabilities',
    dictionary: 'dictionary.json',
    export: 'map/opr?request=export&layerId=',
    epsgCodes: 'map/opr?service=mcwls&request=proj-info',
    getDTMs: 'map/opr?service=mcwls&request=dtm-layers-info',
    compatibeVersions: 'map/opr?service=mcwls&request=compatible-versions',
    indexOps: 'map/opr?index-ops',
    wsBase: 'ws',
    updateConfig: 'map/opr?update-config',
    getNewDTMs: 'map/opr?service=mcwls&request=dtm-layers-info&epsg='
  },
  nodesLevel: {
    none: '',
    repository: 'repository',
    group: 'group',
    layer: 'layer'
  },
  actions: {
    previewMap: 'previewMap'
  },
  selectedLayerDelimiter: '###',
  BACKLOG_PREFIX: 'BACKLOG:',
  LAYERS_BACKLOGS_TITLE: 'Layer Backlog',
  indexOps: {
    api: {
      getStatus: 'get-status',
      getInfo: 'get-info',
      addLayer: 'add-layer',
      getFields: 'get-fields',
      removeLayer: 'remove-layer'
    },
    statusTypes: {
      notIndexed: 'not-indexed',
      indexed: 'indexed',
      noSuchLayer: 'no-such-layer',
      inProgress: 'in-progress',
    }


  }
};

export const mapActions = {
  MAP: 'Map',
  DATA: 'Data',
  THREE_D: 'threeD',
  // DESCRIPTION: 'Description',
  SHOW_DTM_MAP: 'SHOW_DTM_MAP'
};

export const layerTypesStrings = {
  //RAW_VECTOR_3D_EXTRUSION
  RAW_VECTOR: 'Vector',
  RAW_RASTER: 'Raster',
  RAW_DTM: 'DTM',
  RAW_MCPACKAGE: 'McPackage',
  NATIVE_VECTOR: 'Vector',
  NATIVE_RASTER: 'Raster',
  NATIVE_TRAVERSABILITY: 'Traversability',
  NATIVE_MATERIAL: 'Material',
  NATIVE_DTM: 'DTM',
  NATIVE_MCPACKAGE: 'McPackage',
  NATIVE_3D_MODEL: '3D-model',
  NATIVE_3DMODEL: '3D-Model',
  NATIVE_VECTOR_3D_EXTRUSION: 'Vector-3D-Extrusion',
  NATIVE_VECTOR3DEXTRUSION: '3D-extrusion',
  RAW_VECTOR_3D_EXTRUSION: 'Vector-3D-Extrusion',
  RAW_3D_MODEL: '3D-model',
  'S-57': 'S-57',
  McPackage: "McPackage",
};

export const popupTypes = {
  ABOUT: 'about',
  SHOW_DROPZONE: 'Show Dropzone',
  ADD: 'Add',
  PREVIEW_CONFIG: 'Preview Config',
  SERVER_CONFIG: 'Server Config',
  RENAME: 'Rename',
  EXPORT: 'Export',
  REMOVE: 'Remove',
  EDIT: 'Edit',
  MOVE_TO_GROUP: 'Select Groups',
  VECTOR_INDEXING: 'Vector indexing',
  ADVANCED: 'Advanced',
  SSL_INFO: 'SSL Info',
  TILING_SCHEME: 'Tiling Scheme',
  TILING_SCHEME_TABLE: 'Tiling Scheme Table',
  SHOW_WEBSOCKET_ERRORS: 'Show WebSocket Errors',
  CLEAR_LOG: 'Clear Log',
  NATIVE_LAYER_UPLOAD_INSTEAD_RAW: 'Native Layer Upload instead Raw Layer',
};

export const popupSize = {
  small: 'small',
  medium: 'medium'
};

export default config;