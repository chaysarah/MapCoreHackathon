import React, { PureComponent } from "react";
import axios from "axios";

import cn from './ImportForm.module.css';
import { fieldsInfo, fieldsValidation, validationFunctions, validationMessages } from './ImportFormFields';
import Select from '../../Select/Select';
import Input from '../../Input/Input';
import { Dropdown, DropdownMultiple as MultipleSelect } from '../../MultipleSelect';
import Loader from '../../Loader/Loader';
import config, { layerTypesStrings } from '../../../config';
import ApplicationContext from '../../../context/applicationContext';

const localeCodes = require("locale-codes");

export default class ImportForm extends PureComponent {

  static contextType = ApplicationContext;

  constructor() {
    super();
    this.inputRefTitle = React.createRef()
    this.inputRefLayerId = React.createRef()
    this.state = {
      firstTimeTitleFocus: true,
      isShowGroupName: false,
      selectedSldFile: ["defaultValue"],
      type: '',
      errors: {},
      mcPackageData: null,
      firstTime: true,
      isFunc: true,
      DTMs: [],
      is3DTiles: false,
    };
    this.handleGroupChanged = this.handleGroupChanged.bind(this);
    this.handleVersionCompatibilityChanged = this.handleVersionCompatibilityChanged.bind(this);
    this.handleBoolValueChanged = this.handleBoolValueChanged.bind(this);
    this.handleLocaleChanged = this.handleLocaleChanged.bind(this);
  }

  generateDefaultLayerId = (type) => {
    const currentGroupLayers = this.props.groupsTree.map((node) => node.childNodes).flat();
    const pattern = `${layerTypesStrings[type]}-`;
    let serialNum = 0;
    if (currentGroupLayers && currentGroupLayers.length > 0) {
      currentGroupLayers.forEach(layer => {
        if (layer.LayerId.startsWith(pattern)) {
          let num = parseInt(layer.LayerId.substring(layer.LayerId.lastIndexOf('-') + 1, layer.LayerId.length));
          if (isNaN(num) && type.endsWith('VECTOR')) {
            const firstIndex = layer.LayerId.lastIndexOf('Vector-') + 7;
            num = parseInt(layer.LayerId.substring(firstIndex, layer.LayerId.lastIndexOf('-', layer.LayerId.indexOf('-', firstIndex + 1))));
          }
          if (isNaN(num) && type.endsWith("S-57")) {
            const firstIndex = layer.LayerId.lastIndexOf('S-57-') + 5;
            num = parseInt(layer.LayerId.substring(firstIndex, layer.LayerId.lastIndexOf('-', layer.LayerId.indexOf('-', firstIndex + 1))));
          }
          if (!isNaN(num) && num > serialNum) {
            serialNum = num;
          }
        }
      });
    }
    serialNum++;

    if (this.props.layerUploading && this.props.layerUploading.layerId && this.props.layerUploading.layerId.startsWith(pattern)) {
      let tmpSerialNum = parseInt(this.props.layerUploading.layerId.substring(this.props.layerUploading.layerId.lastIndexOf('-') + 1, this.props.layerUploading.layerId.length));
      if (tmpSerialNum >= serialNum) {
        serialNum = tmpSerialNum + 1;
      }
    }

    return `${pattern}${serialNum}`;
  }

  setDefaultFieldsStateByType(type) {
    let data = this.context.getData();
    // set the dropDowns data with the value they are getting on render

    const fields = {
      groupName: this.props.selectedGroup,
      title: '',
      drawPriority: data ? data.data.drawPriority : 0,
      layerType: this.state.type,
    }
    if (type == 'NATIVE_MCPACKAGE') {
      fields.layerId = this.props.fileList[0].name.substring(0, this.props.fileList[0].name.length - 6);
      fields.McPackage = this.props.fileList[0].name;
    } else {
      fields.layerId = data ? data.data.layerId : this.generateDefaultLayerId(type);
    }
    if (type.startsWith('RAW_')) {
      fields.epsg = data ? data.data.epsg : '';
    }
    if (['RAW_VECTOR', 'S-57'].includes(type)) {
      if (type === 'S-57') {
        fields.layerType = 'RAW_VECTOR';
        // fields.dataSource = data ? data.data.dataSource : 'Here just to pass the required field validation';
        fields.dataSource = 'Here just to pass the required field validation';
        fields.locale = data ? data.data.locale : this.context.selectedLang;
      } else {
        fields.dataSource = data ? data.data.dataSource : this.state.dataSource || '';
        fields.dataSource = data ? data.data.SldFile : this.state.SldFile || '';
        fields.locale = data ? data.data.locale : '';
      }
      fields.sourceEpsg = data ? data.data.sourceEpsg : '';
      fields.minScale = data ? data.data.minScale : '';
      fields.textureFile = data ? data.data.textureFile : this.state.textureFile || '';
      fields.versionCompatibility = data ? data.data.versionCompatibility : '';
    }
    if (['RAW_VECTOR', 'RAW_DTM', 'S-57'].includes(type)) {
      fields.maxScale = data ? data.data.maxScale : this.state.maxScale || '';
    }
    if ('RAW_DTM' == type) {
      fields.highestResolution = data ? data.data.highestResolution : this.state.highestResolution || '';
    }
    if (type == 'RAW_3D_MODEL') {
      fields.orthometricHeights = 'true';
      fields.targetHighestResolution = 0.05;
    }
    if (type == 'RAW_VECTOR_3D_EXTRUSION') {
      fields.dtmLayerId = data ? data.data.dtmLayerId : '';
      fields.heightColumn = data ? data.data.heightColumn : '';
      fields.isHeightColumn = data ? data.data.isHeightColumn : true;
      fields.objectIdColumn = data ? data.data?.objectIdColumn : '';
      fields.isObjectIdColumn = data ? data.data.isObjectIdColumn : true;
      fields.sideTexture = data ? data.data.sideTexture : '';
      fields.roofTexture = data ? data.data.roofTexture : '';
      fields.dataSource = data ? data.data.dataSource : '';
      fields.useSpatiallyIndexing = data && data.data.useSpatiallyIndexing ? data.data.useSpatiallyIndexing : '';
    }
    this.setState({ ...fields });
  }

  setDefaultFieldsStateToMCPackage() {
    const { MapLayerConfig: layer } = this.state.mcPackageData;
    const fields = {
      groupName: this.props.selectedGroup,
      layerId: layer.LayerId,
      title: layer.Title,
      drawPriority: layer.DrawPriority || layer.DrawPriority === 0 ? layer.DrawPriority : '',
    }
    if (layer.LayerType.startsWith('RAW_')) {
      fields.epsg = layer.RawLayerInfo.CoordinateSystem.Code;
      const epsgItem = this.context.epsgCodes.find(item => item.code == fields.epsg);
      if (epsgItem) {
        fields.epsgTitle = `${epsgItem.code}: ${epsgItem.desc}`
      }
    }
    if (layer.LayerType === 'RAW_VECTOR') {
      fields.dataSource = layer.RawLayerInfo.Vector.DataSource;
      fields.sldFile = layer.RawLayerInfo.Vector.sldFile;
      fields.versionCompatibility = layer.RawLayerInfo.Vector.VersionCompatibility;
      fields.minScale = layer.RawLayerInfo.Vector.MinScale;
      fields.textureFile = layer.RawLayerInfo.Vector.PointTextureFile;
      fields.sourceEpsg = layer.RawLayerInfo.Vector.SourceCoordinateSystem.Code;
      if (fields.sourceEpsg) {
        const epsgItem = this.context.epsgCodes.find(item => item.code == fields.sourceEpsg);
        if (epsgItem) {
          fields.sourceEpsgTitle = `${epsgItem.code}: ${epsgItem.desc}`
        }
      }
      fields.versionCompatibility = layer.RawLayerInfo.Vector.VersionCompatibility;
      fields.locale = layer.RawLayerInfo.Vector.locale;
    }
    if (['RAW_VECTOR', 'RAW_DTM'].includes(layer.LayerType)) {
      fields.maxScale = layer.RawLayerInfo.Vector.MaxScale;
    }
    if ('RAW_DTM' == layer?.LayerType) {
      fields.highestResolution = layer.RawLayerInfo.Vector?.highestResolution;
    }

    if (layer.LayerType === 'S-57') {
      fields.locale = layer.RawLayerInfo.Vector.Locale;
    }

    if (layer.LayerType == "RAW_3D_MODEL") {
      fields.orthometricHeights = layer.RawLayerInfo.Model3D.OrthometricHeights;
      fields.targetHighestResolution = layer.RawLayerInfo.Model3D.targetHighestResolution;
    }
    if (layer.LayerType == 'RAW_VECTOR_3D_EXTRUSION') {
      fields.dtmLayerId = layer.RawLayerInfo.Vector3DExtrusion.dtmLayerId;
      fields.heightColumn = layer.RawLayerInfo.Vector3DExtrusion.heightColumn;
      fields.objectIdColumn = layer.RawLayerInfo.Vector3DExtrusion.objectIdColumn;
      fields.sideTexture = layer.RawLayerInfo.Vector3DExtrusion.sideTexture;
      fields.roofTexture = layer.RawLayerInfo.Vector3DExtrusion.roofTexture;
      fields.useSpatiallyIndexing = layer.RawLayerInfo?.Vector3DExtrusion?.useSpatiallyIndexing;
    }
    this.setState({ ...fields });
  }

  componentDidUpdate(prevProps, prevState) {

    if (!prevState.type && this.state.type) {
      this.setDefaultFieldsStateByType(this.state.type);
    } else if (!prevState.mcPackageData && this.state.mcPackageData) {
      this.setDefaultFieldsStateToMCPackage();
    }
  }

  setDefaultAdvancedData = () => {
    let rawParams = new window.MapCore.IMcMapLayer.SRawParams();
    let rawAdvancedParams = new window.MapCore.IMcRawVectorMapLayer.SParams("", null)
    const checkAdvancedData = this.context.getAdvancedData();
    const fields = {};
    // if (this.state.type === 'RAW_VECTOR' || this.state.type === "S-57") {
    if (this.state.type === 'RAW_VECTOR' || this.state.type === 'S-57') {
      fields.maxVerticesPerTile = rawAdvancedParams.uMaxNumVerticesPerTile || '100000';
      fields.maxVisiblePointsPerTile = rawAdvancedParams.uMaxNumVisiblePointObjectsPerTile || '5000';
      fields.minSizeForObjVisibility = rawAdvancedParams.uMinPixelSizeForObjectVisibility || '8';
      fields.optimizationMinScale = rawAdvancedParams.fOptimizationMinScale || '0';
    } else if (this.state.type == 'RAW_RASTER') {
      fields.resolveOverlapConflicts = rawParams.bResolveOverlapConflicts;
      fields.enhanceBorderOverlap = rawParams.bEnhanceBorderOverlap;
      fields.fillEmptyTilesByLowerResolutionTiles = rawParams.bFillEmptyTilesByLowerResolutionTiles;
      fields.transparentColorA = rawParams.TransparentColor.a;
      fields.transparentColorR = rawParams.TransparentColor.r;
      fields.transparentColorG = rawParams.TransparentColor.g;
      fields.transparentColorB = rawParams.TransparentColor.b;
      fields.byTransparentColorPrecision = rawParams.byTransparentColorPrecision;
      fields.ignoreRasterPalette = rawParams.bIgnoreRasterPalette;
      fields.highestResolution = rawParams.fHighestResolution;
      fields.maxScale = '';
    }
    !checkAdvancedData && this.context.setAdvancedData(fields)
    !checkAdvancedData && this.context.setAdvancedDataType(this.state.type);
    this.context.setAdvancedPopupType('add');
  }

  componentDidMount() {
    this.props.setParentHook(this.getImportFormData);
    const type = this.getLayerTypeValue();
    if (type) {
      this.setState({ type }, () => {
        if (['S-57', 'RAW_VECTOR', 'RAW_RASTER'].includes(this.state.type)) {
          this.setDefaultAdvancedData()
        }
      });
    }
    this.setState({ emptyDTMsProps: false },)
    if (this.props.selectedImportLayer == "Vector 3D Extrusion") {
      this.getDTMs("");
    }
  }

  getEpsgTooltipName(epsgType) {
    let espgTypeTooltip = epsgType;
    if (this.state.is3DTiles) {
      espgTypeTooltip = "epsg3DTiles";
    }
    if (this.state.type == 'RAW_DTM') {
      espgTypeTooltip = 'epsgDtm';
    }
    if (this.state.type == 'S-57') {
      espgTypeTooltip = 'epsgTargetS57';
    }
    else {
      if (this.state.type.includes("VECTOR")) {
        espgTypeTooltip = epsgType == "epsg" ? "epsgTarget" : "epsgSource";
      }
      if (this.state.type.includes("3D_MODEL")) {
        espgTypeTooltip = "epsg3DModel";
      }
    }
    return espgTypeTooltip;
  }

  isDataSourceSelected = () => {
    return this.state.dataSource;
  }

  isEpsgMandatory(epsgType) {
    if (this.state.is3DTiles) {
      return true;
    }
    else if (epsgType === 'epsg' && (this.state.type === 'RAW_VECTOR_3D_EXTRUSION' || this.isMcPackageRawVector3DExtrusion())) {
      return true;
    }
    return epsgType === 'epsg' && this.state.type != 'S-57' && !this.state.type.startsWith('RAW');
  }

  turnOffTheFlag() {
    this.setState({ isFunc: true });
  }

  getAllLocales() {
    let result = [];
    let lastCode = '';

    for (const element of localeCodes.all) {
      if (element['iso639-1'] && element['iso639-1'] !== lastCode) {
        lastCode = element['iso639-1'];
        result.push(element);
      }
    }

    return result;
  }

  getCurrLocaleTitle(options) {
    if (this.state.locale && this.state.locale !== '') {
      for (const item of options) {
        if (item.id === this.state.locale) {
          return item.title;
        }
      }
    }
    return 'select...';
  }

  outOfRuleValidations(field) {
    if ((this.state.type == 'S-57' || (this.state.type == 'RAW_VECTOR' && !this.state.dataSource.includes('.shp'))) && field == "title") {
      return true;
    }
    else if (this.state.type.startsWith('RAW') && field == "epsg" && !this.state.is3DTiles && this.state.type !== 'RAW_VECTOR_3D_EXTRUSION') {
      return true;
    }
    return false;
  }

  validateForm() {
    this.setState({ firstTime: false })
    const errors = {};
    Object.keys(fieldsValidation).forEach(field => {
      if (this.state.type == 'RAW_VECTOR' && !this.state.dataSource.includes('.shp') && field == "title") return;
      if (this.state.type == 'RAW_VECTOR_3D_EXTRUSION' && !this.state.isHeightColumn && field == "heightColumn") return;
      if (this.state.type == 'RAW_VECTOR_3D_EXTRUSION' && !this.state.isObjectIdColumn && field == "objectIdColumn") return;
      let mapLayersTree = this.props.groupsTree;
      // Check only for fields that are inside the state (we put those fiels in) setDefaultFieldsStateByType
      if (this.state.hasOwnProperty(field)) {
        const valueToValidate = this.state[field];
        const validationsToRun = fieldsValidation[field];
        validationsToRun.forEach(validation => {
          const isValid = this.outOfRuleValidations(field) ? true : validationFunctions[validation](valueToValidate, mapLayersTree);
          if (!isValid) {
            errors[field] = validationMessages[validation];
          }
        })
      }
    });
    this.setState({ errors });
    return Object.keys(errors).length > 0 ? false : true;
  }

  prepareGroupNameBeforeSending() {
    if (this.state.groupName === config.LAYERS_BACKLOGS_TITLE) {
      return config.BACKLOG_PREFIX;
    }
    return this.state.groupName;
  }

  keepData = () => {
    let data = {
      layerId: this.state.layerId,
      title: this.state.title,
      group: this.prepareGroupNameBeforeSending(),
      type: this.state.type || this.state.mcPackageData.MapLayerConfig.LayerType,
      drawPriority: this.state.drawPriority,
      epsg: this.state.epsg
    };
    if (['S-57', 'RAW_VECTOR'].includes(this.state.type)) {
      data.dataSource = this.state.dataSource;
      data.sourceEpsg = this.state.sourceEpsg;
      data.minScale = this.state.minScale;
      data.maxScale = this.state.maxScale;
      data.textureFile = this.state.textureFile;
      data.versionCompatibility = this.state.versionCompatibility;
      data.locale = this.state.locale;
      if (this.state.type === 'S-57') {
        data.layerType = 'RAW_VECTOR';
        data.stylingFormat = 'S52';
        if (this.state.epsg) {
          data.epsg = this.state.epsg;
        }
      }
      this.setState({ dataSource: data.dataSource })
    }
    this.context.setData({ data })
    this.setState({ ...data });

  }

  getImportFormData = () => {
    if (this.isMcPackageRaw())
      this.state.mcPackageData.MapLayerConfig.type = this.state.mcPackageData.LayerType;
    ['S-57', 'RAW_VECTOR', 'RAW_RASTER'].includes(this.state.type) && this.keepData();
    let data = false;
    if (this.state.type == "NATIVE_MCPACKAGE") { data = { McPackage: this.state.McPackage } }
    const advancedData = this.context.getAdvancedData();
    let isValid = this.validateForm();
    if (isValid) {

      data = {
        ...data,
        layerId: this.state.layerId,
        title: this.state.title,
        group: this.prepareGroupNameBeforeSending(),
        type: this.state.type || this.state.mcPackageData.MapLayerConfig.LayerType,
        drawPriority: this.state.drawPriority,
        layerType: this.state.type,
      };

      if (this.state.type.startsWith('RAW_') || this.isMcPackageRaw()) {
        if (this.state.epsg) { data.epsg = this.state.epsg }
        else if (this.isMcPackageRaw()) { data.epsg = this.state.mcPackageData.MapLayerConfig.RawLayerInfo.CoordinateSystem.Code; }
        else { data.epsg = ""; }
      }

      if (this.state.type == 'RAW_3D_MODEL') {
        data.orthometricHeights = this.state.orthometricHeights === "false" ? "false" : "true";
        data.targetHighestResolution = this.state.targetHighestResolution;
      }

      if (['RAW_VECTOR', 'S-57'].includes(this.state.type)) {
        data.dataSource = this.state.dataSource;
        data.sourceEpsg = this.state.sourceEpsg;
        data.minScale = this.state.minScale;
        data.textureFile = this.state.textureFile;
        data.versionCompatibility = this.state.versionCompatibility;
        data.locale = this.state.locale;
        if (this.state.type === 'S-57') {
          data.layerType = 'RAW_VECTOR'
          data.dataSource = '';
          data.stylingFormat = 'S52';
          if (this.state.epsg) {
            data.epsg = this.state.epsg;
          }
        }
        else {
          data.sldFile = this.state.sldFile;
        }
      } else if (this.isMcPackageRawVector()) {
        data.dataSource = this.state.mcPackageData.MapLayerConfig.RawLayerInfo.Vector.DataSource;
        data.sourceEpsg = this.state.mcPackageData.MapLayerConfig.RawLayerInfo.Vector.SourceCoordinateSystem.Code;
        data.minScale = this.state.mcPackageData.MapLayerConfig.RawLayerInfo.Vector.MinScale;
        data.maxScale = this.state.mcPackageData.MapLayerConfig.RawLayerInfo.Vector.MaxScale;
        data.textureFile = this.state.mcPackageData.MapLayerConfig.RawLayerInfo.Vector.PointTextureFile;
        data.versionCompatibility = this.state.versionCompatibility;
        data.locale = this.state.locale;
      } else if (this.isMcPackageRawModel()) {
        data.orthometricHeights = this.state.orthometricHeights === "false" ? "false" : "true";
        data.targetHighestResolution = this.state.targetHighestResolution;
      }

      if (['RAW_VECTOR', 'RAW_DTM', 'S-57'].includes(this.state.type)) {
        data.maxScale = this.state.maxScale;

      }
      if ('RAW_DTM' == this.state.type) {
        data.highestResolution = this.state.highestResolution;
      }
      if (this.state.type == 'RAW_VECTOR_3D_EXTRUSION') {
        data.sourceEpsg = this.state.sourceEpsg;
        data.dataSource = this.state.dataSource;
        data.dtmLayerId = this.state.dtmLayerId;
        data.heightColumn = this.state.heightColumn || '';
        data.objectIdColumn = this.state.objectIdColumn || '';
        data.sideTexture = this.state.sideTexture;
        data.roofTexture = this.state.roofTexture;
        data.useSpatiallyIndexing = this.state.useSpatiallyIndexing === "false" ? "false" : "true";
      }

      if (this.state.mcPackageData) {
        this.state.mcPackageData.MapLayerConfig.Title = data.title;
        this.state.mcPackageData.MapLayerConfig.DrawPriority = data.drawPriority;
        this.state.mcPackageData.MapLayerConfig.Group = data.group;
        this.state.mcPackageData.MapLayerConfig.McPackage = data.McPackage;

        data.packageData = JSON.stringify(this.state.mcPackageData);
      }
    }

    data = data && { ...data, ...advancedData }
    return data;
  }

  isTitleMandatory = () => {
    if (['RAW_VECTOR', 'S-57'].includes(this.state.type)) {
      if (this.state.type == 'RAW_VECTOR' && this.state.dataSource.includes('.shp')) {
        return true;
      }
      return false;
    }
    else {
      return true;
    }
  }

  getDTMs = async (code) => {
    this.setState({ emptyDTMsProps: true, dtmLayerId: false, DTMs: [], backlogDTMs: [] })
    try {
      let DTMLayersRespone = code == "" ? await axios.get(config.urls.getDTMs) : await axios.get(`${config.urls.getNewDTMs}${code}`);
      if (DTMLayersRespone && DTMLayersRespone.data && DTMLayersRespone.data.MapLayerConfig) {
        let DTMLayers = DTMLayersRespone.data.MapLayerConfig.filter(item => item.Group !== "BACKLOG:");
        let backlogDTMs = DTMLayersRespone.data.MapLayerConfig?.filter(item => item.Group === "BACKLOG:");
        this.setState({ DTMs: DTMLayers, backlogDTMs, emptyDTMsProps: false });
      }
    } catch (e) {
      console.log(e);
    }
  }

  cancelWarning = () => {
    if (!this.state.firstTime)
      this.props.setParentHook(this.getImportFormData);
  }

  getDropDownTitleValue(epsgType) {
    switch (epsgType) {
      case 'epsg':
        return this.state.epsgTitle || "Select coordinate system"
      case 'sourceEpsg':
        return this.state.sourceEpsgTitle || "Select coordinate system"
      default:
        break;
    }
  }

  emptyDTMsProps = () => {
    this.setState({ emptyDTMsProps: false })
  }

  isMcPackageRawVector() {
    return this.state.mcPackageData
      && this.state.mcPackageData.MapLayerConfig
      && this.state.mcPackageData.MapLayerConfig.LayerType === 'RAW_VECTOR';
  }

  isMcPackageRawS57() {
    return this.state.mcPackageData
      && this.state.mcPackageData.MapLayerConfig
      && this.state.mcPackageData.MapLayerConfig.LayerType === 'S57';
  }

  isMcPackageRaw() {
    return this.state.mcPackageData
      && this.state.mcPackageData.MapLayerConfig
      && this.state.mcPackageData.MapLayerConfig.LayerType.startsWith('RAW');
  }

  isMcPackageRawModel() {
    return this.state.mcPackageData
      && this.state.mcPackageData.MapLayerConfig
      && this.state.mcPackageData.MapLayerConfig.LayerType === 'RAW_3D_MODEL';
  }

  isMcPackageRawVector3DExtrusion() {
    return this.state.mcPackageData
      && this.state.mcPackageData.MapLayerConfig
      && this.state.mcPackageData.MapLayerConfig.LayerType === 'RAW_VECTOR_3D_EXTRUSION';
  }
  //#region Layer Types Functions
  getModelTypeValue(type) {
    const files = this.props.fileList;

    for (let i = 0; i < files.length; i++) {
      const upperCaseName = files[i].name.toUpperCase();
      if (upperCaseName.endsWith('.B3DM')) {
        this.setState({ is3DTiles: true });
        break;
      }
      if (upperCaseName.endsWith('.BIN')) {

        if (upperCaseName.endsWith('3DMODEL.BIN') && type == "MODEL" || (upperCaseName.endsWith('STATICOBJECTS.BIN') && type == "MODEL")) {
          type = 'NATIVE_3D_MODEL';
          break;
        } else if (upperCaseName.endsWith('VECTOR3DEXTRUSION.BIN') && type == "VECTOR_3D_EXTRUSION" || (upperCaseName.endsWith('STATICOBJECTS.BIN') && type == "VECTOR_3D_EXTRUSION")) {
          type = 'NATIVE_VECTOR_3D_EXTRUSION';
          break;
        }
      }
    }

    if (type === "MODEL") {
      type = 'RAW_3D_MODEL';
    } else if (type === "VECTOR_3D_EXTRUSION") {
      type = 'RAW_VECTOR_3D_EXTRUSION';
    }

    return type;
  }

  getTraversabilityLayerType() {
    let type = '';
    const files = this.props.fileList;

    for (let i = 0; i < files.length; i++) {
      const upperCaseName = files[i].name.toUpperCase();
      if (upperCaseName.endsWith('TRAVERSABILITY.BIN')) {
        type = 'NATIVE_TRAVERSABILITY';
        break;
      }
    }

    if (!type) {
      this.props.onError('Invalid layer type. Traversability.bin file was not found');
    }

    return type;
  }

  getMaterialLayerType() {
    let type = '';
    const files = this.props.fileList;

    for (let i = 0; i < files.length; i++) {
      const upperCaseName = files[i].name.toUpperCase();
      if (upperCaseName.endsWith('MATERIAL.BIN')) {
        type = 'NATIVE_MATERIAL';
        break;
      }
    }

    if (!type) {
      this.props.onError('Invalid layer type. Material.bin file was not found');
    }

    return type;
  }

  getLayerTypeNameUpperCase() {
    let name = this.props.selectedImportLayer.toUpperCase();
    if (name === 'DTM (ELEVATION)') {
      name = 'DTM';
    } else if (name === '3D MODEL') {
      name = 'MODEL';
    } else if (name === 'VECTOR 3D EXTRUSION') {
      name = 'VECTOR_3D_EXTRUSION';
    }
    return name;
  }

  getRegularTypeValue(selectedLayerTypeUpper) {
    let type = "";
    const files = this.props.fileList;
    if (selectedLayerTypeUpper == "NATIVE LAYER" && this.props.isBin3DModelOr3DExtrusion == "") {
      let isStaticObj = false;
      for (let i = 0; i < files.length; i++) {
        let isMainDirectory = files[i].webkitRelativePath.split('/').length;
        if (isMainDirectory <= 2) {
          const upperCaseName = files[i].name.toUpperCase();
          if (upperCaseName.endsWith('.BIN') && ['MATERIAL', 'TRAVERSABILITY', '3DMODEL', 'DTM', 'RASTER', 'VECTOR', 'VECTOR3DEXTRUSION', 'STATICOBJECTS'].includes(upperCaseName.split('.BIN')[0])) {
            if (['STATICOBJECTS'].includes(upperCaseName.split('.BIN')[0])) {
              this.props.setIsStaticObjectsBin(true);
              this.props.setPopupToOpenForStaticObj(this.props.popupToOpenForStaticObj);
              isStaticObj = true;
            }
            else if (['3DMODEL'].includes(upperCaseName.split('.BIN')[0])) {
              type = "NATIVE_3D_MODEL";
            }
            else if (['VECTOR3DEXTRUSION'].includes(upperCaseName.split('.BIN')[0])) {
              type = "NATIVE_VECTOR_3D_EXTRUSION";
            }
            else { type = 'NATIVE_' + upperCaseName.split('.BIN')[0]; }
            let nameToHeader = upperCaseName.toUpperCase() == 'DTM.BIN' ? 'DTM (Elevation)' : upperCaseName.split('.BIN')[0].substring(0, 1) + upperCaseName.split('.BIN')[0].toLowerCase().substring(1);
            (!['STATICOBJECTS'].includes(upperCaseName.split('.BIN')[0])) && this.props.setLayerTypeHook("Add " + nameToHeader);
            break;
          }
        }
      }
      if (type == "" && !isStaticObj) {
        this.props.onError('This folder is not MapCore Native Layer folder');
      }
    }
    else if (selectedLayerTypeUpper == "NATIVE LAYER") {
      type = this.props.isBin3DModelOr3DExtrusion;
      let nameToHeader = type == "NATIVE_VECTOR_3D_EXTRUSION" ? "Vector 3D Extrusion" : "3D Model";
      this.props.setLayerTypeHook("Add " + nameToHeader);
    }
    else {
      // raster, bin, dtm
      type = 'RAW_';
      // for (let i = 0; i < files.length; i++) {
      //   const upperCaseName = files[i].name.toUpperCase();
      //   if (upperCaseName.endsWith('.BIN') && ['MATERIAL', 'TRAVERSABILITY', '3DMODEL', 'DTM', 'RASTER', 'VECTOR', 'VECTOR3DEXTRUSION'].includes(upperCaseName.split('.BIN')[0])) {
      //     this.props.onError('Invalid layer type. layer type is not compatible with layer content');
      //   }
      // }
      type = type + selectedLayerTypeUpper;
    }
    return type;
  }

  getLayerTypeValue() {
    const selectedLayerTypeUpper = this.getLayerTypeNameUpperCase();
    if (selectedLayerTypeUpper === 'MC PACKAGE') {
      return 'NATIVE_MCPACKAGE';
    } else if (selectedLayerTypeUpper === 'MODEL') {
      return this.getModelTypeValue(selectedLayerTypeUpper);
    } else if (selectedLayerTypeUpper === 'VECTOR_3D_EXTRUSION') {
      return this.getModelTypeValue(selectedLayerTypeUpper);
    } else if (selectedLayerTypeUpper === 'S-57') {
      return 'S-57';
    } else if (selectedLayerTypeUpper === 'TRAVERSABILITY') {//will not happen after the change- sm
      return this.getTraversabilityLayerType();
    }
    else if (selectedLayerTypeUpper === 'MATERIAL') {//will not happen after the change - sm
      return this.getMaterialLayerType();
    } else {
      return this.getRegularTypeValue(selectedLayerTypeUpper);
    }
  }
  //#endregion
  //#region Handle Functions
  onTitleFocus = () => {
    if (this.state.firstTimeTitleFocus) {
      let data = this.context.getData();
      this.setState({
        firstTimeTitleFocus: false,
        title: this.state.type == "S-57" ? '' : this.state.layerId
      });
    }
  }
  handleEpsgChange = (code, title) => {
    this.setState({ epsg: code, epsgTitle: title },
      this.cancelWarning,
      (this.props.selectedImportLayer == "Vector 3D Extrusion") && this.getDTMs(code)
    );
  }

  handleEpsgSourceChange = (code, title) => {
    this.setState({ sourceEpsg: code, sourceEpsgTitle: title },
      this.cancelWarning
    );
  }

  handleDTMChange = (title) => {
    this.setState({ dtmLayerId: title },
      this.cancelWarning
    )
  }
  handleEpsgChange = (code, title) => {
    this.setState({ epsg: code, epsgTitle: title },
      this.cancelWarning,
      (this.props.selectedImportLayer == "Vector 3D Extrusion") && this.getDTMs(code)
    );
  }

  handleVersionCompatibilityChanged = (selectedVersion) => {
    if (selectedVersion === 'selectVersion') {
      this.setState({ versionCompatibility: '' });
    }
    else {
      this.setState({ versionCompatibility: selectedVersion },
        this.cancelWarning
      );
    }
  }

  handleTextureFileChanged = (selectedCode) => {
    if (selectedCode === 'selectFile') {
      this.setState({ textureFile: '' });
    } else {
      const file = [...this.props.fileList].find(file => file.name === selectedCode);
      this.setState({ textureFile: file.name },
        this.cancelWarning
      );
    }
  }

  handleBoolValueChanged = (value) => {
    if (this.state.type === 'RAW_3D_MODEL') {
      this.setState({ orthometricHeights: value });
    }
    else {
      this.setState({ useSpatiallyIndexing: value });
    }
  };

  handleInputChange = (e) => {
    const reg = /^[a-zA-Z0-9_-]*$/;
    const regTitle = /^[a-zA-Z0-9_ -]*$/;
    let ref = e.target.name == 'title' ? this.inputRefTitle : this.inputRefLayerId;

    let prevVal = this.state[e.target.name];
    let indexChange = ref.current.value.length;
    let shortLength = prevVal.length > e.target.value.length ? e.target.value.length : prevVal.length;
    let flag = false;
    for (let i = 0; i < shortLength; i++) {
      if (prevVal[i] != e.target.value[i]) {
        if (prevVal.substring(i + 1) == e.target.value.substring(i)) {
          indexChange = i;
          flag = true;
          break;
        }
        else {
          indexChange = i + 1;
          flag = true;
          break;
        }
      }
    }

    if ((e.target.name == 'layerId' && !reg.test(e.target.value)) || (e.target.name == "drawPriority" && (e.target.value < (-128) || e.target.value > 127))) {
      ref.current.selectionStart = ref.current.selectionEnd = (indexChange - 1);
      return;
    }
    else {
      this.setState({ [e.target.name]: e.target.value }, () => {
        if (!flag) {
          indexChange = ref.current.value.length;
        }
        ref.current.selectionStart = ref.current.selectionEnd = indexChange;
        this.cancelWarning();
      });
    }
  }

  handleGroupChanged = (selectedCode) => {
    if (selectedCode === 'newGroup') {
      this.setState({ isShowGroupName: true, groupName: '' },
        this.cancelWarning);
    } else {
      const groupName = this.props.groupsTree.find(group => group.title === selectedCode);
      this.setState({ isShowGroupName: false, groupName: groupName.title },
        this.cancelWarning);
    }
  }

  handleCheckboxChange = (name) => {
    let nameIs = name == 'heightColumn' ? 'isHeightColumn' : 'isObjectIdColumn';
    this.setState({ [nameIs]: !this.state[nameIs] }, () => {
      if (!this.state[nameIs]) {
        this.setState({ [name]: '' })
      }
    })
  }

  handleDataSourceChanged = (selectedCode) => {
    if (selectedCode === 'selectFile') {
      this.setState({ dataSource: '' },
        this.cancelWarning);
    } else {
      this.setState({ dataSource: selectedCode })
    }
  }

  handleSideTextureChanged = (selectedFile) => {
    if (selectedFile === 'selectFile') {
      this.setState({ sideTexture: '' });
    } else {
      this.setState({ sideTexture: selectedFile },
        this.cancelWarning);
    }
  }

  handleRoofTextureChanged = (selectedFile) => {
    if (selectedFile === 'selectFile') {
      this.setState({ roofTexture: '' });
    } else {
      this.setState({ roofTexture: selectedFile },
        this.cancelWarning);
    }
  }

  handleLocaleChanged = (selectedLocale) => {
    if (selectedLocale === 'select...') {
      this.setState({ locale: '' });
    }
    else {
      this.setState({ locale: selectedLocale },
        this.cancelWarning
      );
    }
  }
  //#endregion
  //#region DOM Functions
  getInput(name, label, options, isRef = false) {
    let tooltipName = name == 'maxScale' && this.state.type == 'RAW_DTM' ? 'maxDtmScale' : name;
    let ref = name == 'title' ? this.inputRefTitle : this.inputRefLayerId;
    return (
      <div className={cn.Row}>
        <Input
          parentRef={isRef ? ref : null}
          error={this.state.errors[name]}
          mandatory={options.mandatory}
          label={label}
          name={name}
          maxLength={options.maxLength || null}
          value={this.state[name]}
          type={options.type || "text"}
          onFocus={options.onFocus || null}
          onChange={this.handleInputChange}
          info={fieldsInfo[tooltipName]}
          readOnly={options.readOnly}
          isPositive={options.isPositive}
          checkboxLabel={options.checkboxLabel}
          handleCheckboxChange={() => { this.handleCheckboxChange(name) }}
          checked={name == 'heightColumn' ? this.state.isHeightColumn : this.state.isObjectIdColumn}
        />
      </div>
    );
  }

  getGroupDropDown() {
    const groupsArr = this.props.groupsTree.map(group => ({ code: group.title, value: group.title }));
    const dropDownData = {
      options: [...groupsArr, { code: 'newGroup', value: 'Create New' }],
      label: 'Group Name',
      fieldNames: { code: 'code', value: 'value' },
      onChange: this.handleGroupChanged,
      isMandatoy: true,
      error: this.state.errors['groupName']
    };

    return (
      <div className={cn.Row}>
        <Select {...dropDownData} />
      </div>
    )
  }

  getTexture(textureType) {
    const label = (textureType === 'side' ? this.context.dict.sideTexture : this.context.dict.roofTexture);
    const name = (textureType === 'side' ? "sideTexture" : "roofTexture");

    const imgExtension = /(jpe?g|bmp|gif|png|tif?f)$/;

    if (this.isMcPackageRawVector3DExtrusion()) {
      return this.getInput('sideTexture', label, { readOnly: true })
    }

    const filesArr = this.props.fileList.filter(file =>
      imgExtension.exec(file.name.split('.').pop())
    )
      .map(file =>
        ({ code: file.name, value: file.name })
      );

    return (

      <div className={cn.Row}>
        <Select
          options={[{ code: 'selectFile', value: 'Select file...' }, ...filesArr]}
          onChange={textureType === 'side' ? this.handleSideTextureChanged : this.handleRoofTextureChanged}
          label={label}
          fieldNames={{ code: 'code', value: 'value' }}
          isMandatoy={false}
          error={this.state.errors[name]}
          info={fieldsInfo[name]}
          type={name}
        />
      </div>
    )
  }

  getDataSource() {
    if (this.isMcPackageRawVector()) {
      return this.getInput('dataSource', this.context.dict.dataSource, { readOnly: true })
    }

    const filesArr = this.props.fileList.map(file =>
    ({
      id: file.name,
      key: file.name,
      title: file.name,
      selected: this.isDataSourceSelected(),
      code: file.name,
      value: file.name
    }))

    let tooltipName = this.state.type === 'RAW_VECTOR_3D_EXTRUSION' ? 'dataSourceVector3DExtr' : 'dataSourceVector';

    const dropDownDataSelect = {
      label: this.context.dict.dataSource,
      options: [{ code: 'selectFile', value: 'Select file...' }, ...filesArr],
      onChange: this.handleDataSourceChanged,
      fieldNames: { code: 'code', value: 'value' },
      isMandatoy: true,
      error: this.state.errors['dataSource'],
      info: fieldsInfo[tooltipName],
      type: 'dataSource',
      editMode: true,
    };

    return (
      <div className={cn.Row}>
        <Select {...dropDownDataSelect} />
      </div>
    )
  }

  getEPSGDropDown(epsgType) {
    let label = this.context.dict.coordinateSystem;
    if (this.state.type != "RAW_RASTER" && this.state.type != "RAW_DTM") {
      label = (epsgType === 'sourceEpsg' ? `${this.context.dict.source} ` : `${this.context.dict.target} `) + this.context.dict.coordinateSystem;
    }

    if (this.isMcPackageRaw()) {
      return this.getInput(epsgType + 'Title', label, { readOnly: true });
    }

    if (this.context.epsgCodes.length < 1) return null;

    const options = this.context.epsgCodes.map(item => {
      return {
        id: item.code,
        title: `${item.code}: ${item.desc}`,
        selected: item.code === this.state[epsgType],
        key: epsgType
      }
    });

    let espgTypeTooltip = this.getEpsgTooltipName(epsgType);

    return (
      <div className={cn.Row}>
        <div className={cn.DropDownWrapper}>
          <Dropdown
            searchable={["Search coordinate system...", "No matching values"]}
            title={this.getDropDownTitleValue(epsgType)}
            list={options}
            onChange={epsgType === 'epsg' ? this.handleEpsgChange : this.handleEpsgSourceChange}
            label={label}
            error={this.state.errors[epsgType]}
            info={fieldsInfo[espgTypeTooltip]}
            isMandatoy={this.isEpsgMandatory(epsgType) ? true : false}
            type={epsgType}
          />
        </div>
      </div>
    )
  }

  getPointTextureFile() {
    if (this.isMcPackageRawVector()) {
      return this.getInput('textureFile', this.context.dict.dataSource, { readOnly: true })
    }

    const filesArr = [...this.props.fileList].map(file => ({ code: file.name, value: file.name }));
    const dropDownData = {
      options: [{ code: 'selectFile', value: 'Select point image...' }, ...filesArr],
      onChange: this.handleTextureFileChanged,
      label: this.context.dict.defaultPointImage,
      fieldNames: { code: 'code', value: 'value' },
      isMandatoy: false,
      error: this.state.errors['textureFile'],
      type: 'textureFile',
      info: fieldsInfo['textureFile']
    };

    return (
      <div className={cn.Row}>
        <Select {...dropDownData} />
      </div>
    )
  }

  getVersionCompatibility() {
    if (this.isMcPackageRawVector() || this.isMcPackageRawS57()) {
      return this.getInput('versionCompatibility', this.context.dict, { readOnly: true });
    }

    const options = this.context.compatibeVersions.data.map(item => {
      return {
        id: item,
        title: item,
        selected: item === this.state.versionCompatibility,
        key: 'versionCompatibility'
      }
    })

    const dropDownData = {
      onChange: this.handleVersionCompatibilityChanged,
      label: this.context.dict.versionCompatibility,
      title: this.state.versionCompatibility || 'Select version...',
      list: options,
      isMandatory: false,
      info: fieldsInfo['versionCompatibility'],
      error: this.state.errors['versionCompatibility']
    };

    return (
      <div className={cn.Row}>
        <div className={cn.DropDownWrapper}>
          <Dropdown {...dropDownData} />
        </div>
      </div>
    )
  }
  getLocale() {
    if (this.isMcPackageRawS57()) {
      return this.getInput('locale', this.context.dict.locale, { readOnly: true });
    }

    const options = [...this.getAllLocales()].map(item => {
      return {

        id: item['iso639-1'],
        title: item.name,
        selected: item['iso639-1'] === this.state.locale,
        key: 'locale',
      }
    })

    const dropDownData = {
      onChange: this.handleLocaleChanged,
      label: this.context.dict.locale,
      title: this.getCurrLocaleTitle(options),
      list: options,
      isMandatory: false,
      info: fieldsInfo['locale'],
      error: this.state.errors['locale'],
      type: 'locale',
      searchable: ["Search locale...", "No matching values"]
    }

    return (
      <div className={cn.Row}>
        <div className={cn.DropDownWrapper}>
          <Dropdown {...dropDownData} />
        </div>
      </div>
    )
  };

  getDTMDropDown(DTMType) {
    const label = this.context.dict.dtmLayerId;
    if (this.isMcPackageRaw()) {
      return this.getInput('Title', label, { readOnly: true });
    }
    let options = [];

    // if (this.state.DTMs && this.state.DTMs.length < 1 && this.props.onErrorCancel) {
    //   return this.props.onErrorCancel()
    // }

    options = this.state.DTMs?.map(item => {
      return {
        id: item.LayerId,
        title: item.Title,
        selected: item.Group === this.props.selectedGroup,
        key: item.LayerId
      }
    });

    return (
      <div className={cn.Row}>
        <div className={cn.DropDownWrapper}>
          <Dropdown
            emptyDTMsProps={this.state.emptyDTMsProps}
            emptyDTMsPropsFunc={this.emptyDTMsProps}
            searchable={["Search DTM...", "No matching values"]}
            title={this.state.dtmLayerId || "Select DTM"}
            list={options}
            onChange={this.handleDTMChange}
            label={label}
            error={this.state.errors[DTMType]}
            info={fieldsInfo['dtmLayerIdOfAdd']}
            isMandatoy={true}
            type={DTMType}
            readOnly={!this.state.epsg}
          />
        </div>
      </div>
    )
  }
  getTrueFalseDropDown(isUseSpatiallyIndexing = false) {
    const filesArr = [{ code: "true", value: "True" }, { code: "false", value: "False" }];

    const options = filesArr.map(item => {
      return {
        id: item.code,
        title: item.value,
        selected: this.state.type === 'RAW_3D_MODEL' ? item.value === this.state.orthometricHeights : item.value === this.state.useSpatiallyIndexing,
        key: isUseSpatiallyIndexing ? 'useSpatiallyIndexing' : 'orthometricHeights'
      }
    })
    const dropDownData = {
      onChange: this.handleBoolValueChanged,
      label: this.state.type === 'RAW_3D_MODEL' ? this.context.dict.orthometricHeights : this.context.dict.useSpatiallyIndexing,
      title: (this.state.type === 'RAW_3D_MODEL' ? this.state.orthometricHeights : this.state.useSpatiallyIndexing) || "True",
      list: options,
      fieldNames: { code: 'code', value: 'value' },
      isMandatory: false,
      info: this.state.type === 'RAW_3D_MODEL' ? fieldsInfo['orthometricHeights'] : fieldsInfo['useSpatiallyIndexing']
    };

    return (
      <div className={cn.Row}>
        <div className={cn.DropDownWrapper}>
          <Dropdown {...dropDownData} />
        </div>
      </div>
    )
  }

  //#endregion

  renderLayout() {
    if (this.state.mcPackageData) {
      this.state.type = this.state.mcPackageData.MapLayerConfig.LayerType;
    }

    const { mcPackageData } = this.state;
    return (
      <>
        {this.state.type == "NATIVE_MCPACKAGE" ?
          <div >
            {this.getInput('McPackage', this.context.dict.McPackage, { maxLength: '100', readOnly: true })}
          </div>
          :
          <div className={cn.Wrapper}>
            <div className={cn.Split}>
              {this.getInput('layerId', this.context.dict.layer + " " + this.context.dict.id, { maxLength: '100', mandatory: true, readOnly: !!mcPackageData }, true)}
              {/* {this.getInput('title', this.context.dict.title, { maxLength: '100', mandatory: true })} */}
              {this.getInput('title', this.context.dict.title, { maxLength: '100', mandatory: ['S-57'].includes(this.state.type) ? false : true, onFocus: this.onTitleFocus }, true)}
              {this.state.type.startsWith('RAW_VECTOR') || this.isMcPackageRawVector() ? this.getDataSource() : null}
              {this.state.type.startsWith('RAW_VECTOR') || this.isMcPackageRawVector() ? this.getEPSGDropDown('sourceEpsg') : null}
              {this.state.type.startsWith('RAW') || this.state.type === 'S-57' || this.isMcPackageRaw() ? this.getEPSGDropDown('epsg') : null}
              {['RAW_VECTOR', 'S-57'].includes(this.state.type) || this.isMcPackageRawVector() ? this.getVersionCompatibility() : null}
              {this.state.type === 'RAW_VECTOR_3D_EXTRUSION' || this.isMcPackageRawVector3DExtrusion() ? this.getDTMDropDown('dtmLayerId') : null}

            </div>
            <div className={cn.Split}>
              {this.getInput('groupName', this.context.dict.groupName, { readOnly: true })}
              {/*this.getGroupDropDown()*/}
              {['RAW_VECTOR', 'NATIVE_VECTOR', 'RAW_RASTER', 'NATIVE_RASTER', 'S-57', 'NATIVE_TRAVERSABILITY', 'NATIVE_MATERIAL'].includes(this.state.layerType) ? this.getInput('drawPriority', this.context.dict.drawPriority, { type: 'number', maxLength: '100' }) : null}
              {this.state.type === 'RAW_VECTOR' || this.isMcPackageRawVector() ? this.getInput('minScale', this.context.dict.minScale, { type: 'number', maxLength: '20', readOnly: !!mcPackageData }) : null}
              {['RAW_VECTOR', 'RAW_DTM'].includes(this.state.type) || this.isMcPackageRawVector() ? this.getInput('maxScale', this.context.dict.maxScale, { type: 'number', maxLength: '20', readOnly: !!mcPackageData, isPositive: true }) : null}
              {['RAW_DTM'].includes(this.state.type) ? this.getInput('highestResolution', 'Highest Resolution', { type: 'number' }) : null}
              {this.state.type === 'RAW_VECTOR' || this.isMcPackageRawVector() ? this.getPointTextureFile() : null}
              {this.state.type === 'RAW_3D_MODEL' || this.isMcPackageRawModel() ? this.getTrueFalseDropDown() : null}
              {this.state.type === 'RAW_3D_MODEL' || this.isMcPackageRawModel() ? this.getInput('targetHighestResolution', this.context.dict.targetHighestResolution, { type: 'number' }) : null}
              {this.state.type === 'RAW_VECTOR_3D_EXTRUSION' ? this.getInput('heightColumn', 'Height Column', { mandatory: this.state.isHeightColumn, checkboxLabel: true, readOnly: !this.state.isHeightColumn }) : null}
              {this.state.type === 'RAW_VECTOR_3D_EXTRUSION' ? this.getInput('objectIdColumn', 'Object Id Column', { mandatory: this.state.isObjectIdColumn, checkboxLabel: true, readOnly: !this.state.isObjectIdColumn }) : null}
              {this.state.type === 'RAW_VECTOR_3D_EXTRUSION' || this.isMcPackageRawVector3DExtrusion() ? this.getTexture('roof') : null}
              {this.state.type === 'RAW_VECTOR_3D_EXTRUSION' || this.isMcPackageRawVector3DExtrusion() ? this.getTexture('side') : null}
              {['RAW_VECTOR', 'S-57'].includes(this.state.type) || this.isMcPackageRawS57() ? this.getLocale() : null}
              {/* {this.state.type === 'RAW_VECTOR' || this.isMcPackageRawVector() ? this.getSldFile() : null} */}
              {this.state.type === 'RAW_VECTOR_3D_EXTRUSION' || this.isMcPackageRawVector3DExtrusion() ? this.getTrueFalseDropDown(true) : null}
            </div>
          </div>
        }
      </>
    );
  }

  render() {
    if (this.state.type || this.state.mcPackageData) {
      return this.renderLayout();
    }
    return <Loader />;
  }

}
