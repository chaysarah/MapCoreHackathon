import React, { PureComponent } from "react";
import axios from "axios";

import cn from './EditForm.module.css';
import { fieldsValidation, validationFunctions, validationMessages } from './EditFormFields';
import { fieldsInfo } from "../ImportForm/ImportFormFields";
import Input from '../../Input/Input';
import Select from '../../Select/Select';
import { Dropdown } from "../../MultipleSelect";
import { DropdownMultiple as MultipleSelect } from '../../MultipleSelect';
import ApplicationContext from '../../../context/applicationContext';
import config from "../../../config";

export default class EditForm extends PureComponent {
  static contextType = ApplicationContext;
  constructor(props) {
    super(props);
    this.inputRef = React.createRef()
    this.state = {
      isShowGroupName: false,
      selectedGroups: props.layer.Group.split(','),
      layerType: props.layer.LayerType,
      errors: {},
      isFirstExecute: true,

    };
  }

  getExtractedStringFromPath(path) {
    const lastIndexOfSlash = path.lastIndexOf('/');
    const lastIndexOfBackSlash = path.lastIndexOf('\\');
    if (lastIndexOfSlash > lastIndexOfBackSlash) {
      return path.substring(lastIndexOfSlash + 1);
    } else {
      return path.substring(lastIndexOfBackSlash + 1);
    }
  }

  async setDefaultFieldsStateByType(layerType) {
    let data = this.context.getData();
    // set the dropDowns data with the value they are getting on render
    const fields = {
      groupName: this.props.layer.parentFolder,
      layerId: this.props.layer.LayerId,
      title: data ? data.data.title : this.props.layer.Title,
      drawPriority: data ? data.data.drawPriority : this.props.layer.DrawPriority || 0,
    }
    if (layerType.startsWith('RAW_')) {
      fields.epsg = this.props.layer.RawLayerInfo && this.props.layer.RawLayerInfo.TargetCoordinateSystem ?
        this.props.layer.RawLayerInfo.TargetCoordinateSystem.Code : '';

      const epsgItem = this.context.epsgCodes.find(item => item.code == fields.epsg);
      if (epsgItem) {
        fields.epsgTitle = data && data.data.epsgTitle ? data.data.epsgTitle : `${epsgItem.code}: ${epsgItem.desc}`
      }
    }
    // if (layerType.includes('VECTOR')) {
    //   fields.displayedTitle = this.props.layer.DisplayedTitle;
    // }
    if (layerType === 'RAW_3D_MODEL') {
      let tmpOrthometricHeights = data ? data.data.orthometricHeights : this.props.layer.RawLayerInfo?.Model3D.OrthometricHeight;
      fields.orthometricHeights = tmpOrthometricHeights === true ? 'True' : 'False';
      fields.targetHighestResolution = data ? data.data.targetHighestResolution : this.props.layer.RawLayerInfo?.Model3D.TargetHighestResolution;
    }
    if (layerType === 'RAW_DTM') {
      fields.maxScale = data ? data.data.maxScale : this.props.layer.RawLayerInfo && this.props.layer.RawLayerInfo.Dtm ?
        this.props.layer.RawLayerInfo.Dtm.MaxScale : '';
      fields.highestResolution = data ? data.data.highestResolution : this.props.layer.RawLayerInfo && this.props.layer.RawLayerInfo.Dtm ?
        this.props.layer.RawLayerInfo.Dtm?.HighestResolution : '';
    }
    if (layerType === 'NATIVE_VECTOR') {
      fields.layerName = this.props.layer.ClientMetadata?.McSubLayerName
    }
    if (layerType === 'RAW_VECTOR') {
      fields.dataSource = data ? data.data.dataSource : this.props.layer.RawLayerInfo && this.props.layer.RawLayerInfo.Vector ?
        this.props.layer.RawLayerInfo.Vector.DataSource : '';
      if (fields.dataSource) {
        fields.dataSourceTitle = data ? data.data.dataSourceTitle : this.getExtractedStringFromPath(fields.dataSource);
      }
      fields.minScale = data ? data.data.minScale : this.props.layer.RawLayerInfo && this.props.layer.RawLayerInfo.Vector ?
        this.props.layer.RawLayerInfo.Vector.MinScale : '';
      fields.maxScale = data ? data.data.maxScale : this.props.layer.RawLayerInfo && this.props.layer.RawLayerInfo.Vector ?
        this.props.layer.RawLayerInfo.Vector.MaxScale : '';
      fields.textureFile = data ? data.data.textureFile : this.props.layer.RawLayerInfo && this.props.layer.RawLayerInfo.Vector ?
        this.props.layer.RawLayerInfo.Vector.PointTextureFile : '';

      if (fields.textureFile) {
        fields.textureFileTitle = this.getExtractedStringFromPath(fields.textureFile);
      }

      fields.DTM = data ? data.data.DTM : this.props.layer.RawLayerInfo && this.props.layer.RawLayerInfo.Vector && this.props.layer.RawLayerInfo.Vector.SourceCoordinateSystem ?
        this.props.layer.RawLayerInfo.Vector.SourceCoordinateSystem.Code : '';
      fields.sourceEpsg = this.props.layer.RawLayerInfo ? this.props.layer.RawLayerInfo.TargetCoordinateSystem.Code : '';
      if (fields.sourceEpsg) {
        const epsgItem = this.context.epsgCodes.find(item => item.code == fields.sourceEpsg);
        if (epsgItem) {
          fields.sourceEpsgTitle = data ? data.data.sourceEpsgTitle : `${epsgItem.code}: ${epsgItem.desc}`
        }
      }
    }
    if (layerType === 'RAW_VECTOR_3D_EXTRUSION') {
      fields.columnsOps = await this.getColumnsOps(this.props.layer.LayerId);
      fields.dataSource = data ? data.data.dataSource : this.props.layer.RawLayerInfo && this.props.layer.RawLayerInfo.Vector3DExt ?
        this.props.layer.RawLayerInfo.Vector3DExt.DataSource : '';
      fields.dtmLayerId = data ? data.data.dtmLayerId : this.props.layer.RawLayerInfo && this.props.layer.RawLayerInfo.Vector3DExt ?
        this.props.layer.RawLayerInfo.Vector3DExt.DtmLayerId : '';
      fields.sourceEpsg = data ? data.data.sourceEpsg : this.props.layer.RawLayerInfo && this.props.layer.RawLayerInfo.Vector3DExt ?
        this.props.layer.RawLayerInfo.Vector3DExt.SourceCoordinateSystem?.Code : '';
      if (data && data.data && data.data.UseSpatialIndexing) {
        fields.useSpatiallyIndexing = data.data.UseSpatialIndexing;
      }
      else if (this.props.layer.RawLayerInfo && this.props.layer.RawLayerInfo.Vector3DExt?.UseSpatialIndexing === false) {
        fields.useSpatiallyIndexing = "False";
      }
      else {
        fields.useSpatiallyIndexing = "True";
      }
      fields.heightColumn = data ? data.data.heightColumn : this.props.layer.RawLayerInfo && this.props.layer.RawLayerInfo.Vector3DExt ?
        this.props.layer?.RawLayerInfo?.Vector3DExt?.HeightColumn : '';
      fields.isHeightColumn = fields.heightColumn && fields.heightColumn != '' ? true : false;
      fields.objectIdColumn = data ? data.data.objectIdColumn : this.props.layer.RawLayerInfo && this.props.layer.RawLayerInfo.Vector3DExt ?
        this.props.layer?.RawLayerInfo?.Vector3DExt?.ObjectIdColumn : '';
      fields.isObjectIdColumn = fields.objectIdColumn && fields.objectIdColumn != '' ? true : false;
      fields.sideTexture = data ? data.data.sideTexture : this.props.layer.RawLayerInfo && this.props.layer.RawLayerInfo.Vector3DExt ?
        this.props.layer?.RawLayerInfo?.Vector3DExt?.SideDefaultTexture?.TextureFile : '';
      fields.roofTexture = data ? data.data?.roofTexture : this.props.layer?.RawLayerInfo && this.props.layer?.RawLayerInfo?.Vector3DExt ?
        this.props.layer.RawLayerInfo.Vector3DExt.RoofDefaultTexture?.TextureFile : '';

      if (fields.sourceEpsg) {
        const epsgItem = this.context.epsgCodes.find(item => item.code == fields.sourceEpsg);
        if (epsgItem) {
          fields.sourceEpsgTitle = data ? data.data.sourceEpsgTitle : `${epsgItem.code}: ${epsgItem.desc}`;
        }
      }
    }
    this.setState({ ...fields });
  }

  componentDidMount() {
    this.props.setParentHook(this.getEditFormData);
    this.setDefaultFieldsStateByType(this.state.layerType);
    if (['S-57', 'NATIVE_VECTOR', 'RAW_VECTOR_3D_EXTRUSION', 'RAW_RASTER'].includes(this.state.layerType)) {
      this.setDefaultAdvancedData()
    }
  }

  handleInputChange = (e) => {
    const reg = /^[a-zA-Z0-9_@#$%^&*()+=}{:;?. ,!-]*$/;

    let prevVal = this.state[e.target.name];
    let indexChange = this.inputRef.current.value?.length;
    let shortLength = prevVal?.length > e.target.value?.length ? e.target.value?.length : prevVal?.length;
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

    if ((e.target.name == 'layerId') && !reg.test(e.target.value) || (e.target.name == "drawPriority" && (e.target.value < (-128) || e.target.value > 127))) {
      this.inputRef.current.selectionStart = this.inputRef.current.selectionEnd = (indexChange - 1);
      return;
    } else {
      let prevVal = this.state[e.target.name];
      this.setState({ [e.target.name]: e.target.value }, () => {
        if (e.target.name == "title") {
          if (prevVal != this.state.inputName) {
            if (!flag) {
              indexChange = this.inputRef.current.value.length;
            }
            this.inputRef.current.selectionStart = this.inputRef.current.selectionEnd = indexChange;
          }
        }
      });
      this.props.isChanged();
    }
  }
  getTooltipName(name) {
    let tooltipName = name == 'maxScale' && this.state.layerType == 'RAW_DTM' ? 'maxDtmScale' : name;
    tooltipName = this.state.layerType === 'RAW_VECTOR' && name == 'dataSource' ? 'dataSourceVector' : tooltipName;
    if (this.state.layerType === 'RAW_VECTOR_3D_EXTRUSION' && name == 'dataSource') {
      tooltipName = 'dataSourceVector3DExtr';
    }
    return tooltipName;
  }
  isFileTypeValue(name) {
    let is = name === 'sideTexture' || name === 'roofTexture' || name == 'dataSource';
    return is;
  }
  getFileNameFromPath(path) {
    let fileName;
    if (path) {
      let lastIndexOfSlash = path.lastIndexOf('/');
      if (!lastIndexOfSlash) {
        lastIndexOfSlash = path.lastIndexOf('\\');
      }
      fileName = path.substring(lastIndexOfSlash + 1)
    }
    return fileName;
  }

  handleCheckboxChange = (name) => {
    let firstUpperLetter = name.substring(0, 1).toUpperCase();
    let nameWithoutFirst = name.substring(1);
    let nameIs = `is${firstUpperLetter}${nameWithoutFirst}`;
    this.setState({ [nameIs]: !this.state[nameIs] }, () => {
      if (!this.state[nameIs]) {
        this.setState({ [name]: '' })
      }
    })
    this.props.isChanged()
  }

  getInput(name, label, options, isRef = false) {
    let tooltipName = this.getTooltipName(name);
    let value = this.isFileTypeValue(name) ? this.getFileNameFromPath(this.state[name]) : this.state[name];

    return (
      <div className={cn.Row}>
        <Input
          parentRef={isRef ? this.inputRef : null}
          error={this.state.errors[name]}
          mandatory={options.mandatory}
          label={label}
          name={name}
          maxLength={options.maxLength || null}
          value={value}
          type={options.type || "text"}
          onChange={this.handleInputChange}
          info={fieldsInfo[tooltipName]}
          readOnly={options.readOnly}
          isPositive={options.isPositive}
          checkboxLabel={options.checkboxLabel}
          handleCheckboxChange={(e) => { this.handleCheckboxChange(name) }}
          checked={options.checked}
        />
      </div>
    );
  }

  validateForm() {
    const errors = {};
    Object.keys(fieldsValidation).forEach(field => {
      if (this.state.layerType == 'RAW_VECTOR_3D_EXTRUSION' && !this.state.isHeightColumn && field == "heightColumn") return;
      if (this.state.layerType == 'RAW_VECTOR_3D_EXTRUSION' && !this.state.isObjectIdColumn && field == "objectIdColumn") return;
      // Check only for fields that are inside the state (we put those fiels in) setDefaultFieldsStateByType
      if (this.state.hasOwnProperty(field)) {
        const valueToValidate = this.state[field];
        const validationsToRun = fieldsValidation[field];
        validationsToRun.forEach(validation => {
          const isValid = (validation === 'layerNameEmpty' && this.props.checkShp) ? true : validationFunctions[validation](valueToValidate);
          // const isValid = validationFunctions[validation](valueToValidate);
          if (!isValid) {
            errors[field] = validationMessages[validation];
          }
        })
      }
    });
    console.log(errors);
    this.setState({ errors });
    return Object.keys(errors).length > 0 ? false : true;
  }

  getEditFormData = () => {
    ['S-57', 'NATIVE_VECTOR', 'RAW_RASTER'].includes(this.state.layerType) && this.keepData();
    let data = false;
    const advancedData = this.context.getAdvancedData();
    const isValid = this.validateForm();

    if (isValid) {
      data = {
        layerId: this.state.layerId,
        title: this.state.title,
        drawPriority: this.state.drawPriority || 0,
        group: this.state.selectedGroups.join()
      };

      if (this.state.layerType.startsWith('RAW_')) {
        data.epsg = this.state.epsg != '' && this.state.epsg != undefined ? this.state.epsg : 0;
      }
      if (this.state.layerType === 'RAW_3D_MODEL') {
        data.orthometricHeights = this.state.orthometricHeights == "True" ? true : false;
        data.targetHighestResolution = this.state.targetHighestResolution;
      }
      if (this.state.layerType === 'RAW_DTM') {
        data.maxScale = this.state.maxScale || '';
        data.highestResolution = this.state.highestResolution || '';
      }

      if (this.state.layerType === 'RAW_VECTOR') {
        data.dataSource = this.state.dataSource;
        data.sourceEpsg = this.state.sourceEpsg != '' && this.state.sourceEpsg != undefined ? this.state.sourceEpsg : 0;
        data.minScale = this.state.minScale || '';
        data.maxScale = this.state.maxScale || '';
        data.textureFile = this.state.textureFile;
      }

      if (this.state.layerType === 'RAW_VECTOR_3D_EXTRUSION') {
        data.dataSource = this.state.dataSource;
        data.dtmLayerId = this.state.dtmLayerId;
        data.useSpatiallyIndexing = this.state.useSpatiallyIndexing == 'False' ? false : true;
        data.heightColumn = this.state.heightColumn || '';
        data.objectIdColumn = this.state.objectIdColumn || '';
        data.sideTexture = this.state.sideTexture;
        data.roofTexture = this.state.roofTexture;
        data.sourceEpsg = this.state.sourceEpsg != '' && this.state.sourceEpsg != undefined ? this.state.sourceEpsg : 0;
      }
    }
    data = data && { ...data, ...advancedData }
    return data;
  }

  keepData = () => {
    let data = {
      layerId: this.state.layerId,
      title: this.state.title,
      group: this.state.selectedGroups.join(),
      type: this.state.layerType,
      drawPriority: this.state.drawPriority,
      epsgTitle: this.state.epsgTitle,

    };
    if (['S-57', 'NATIVE_VECTOR'].includes(this.state.layerType)) {
      data.epsg = this.state.epsg;
      data.sourceEpsgTitle = this.state.sourceEpsgTitle;
      data.minScale = this.state.minScale;
      data.maxScale = this.state.maxScale;
      data.textureFile = this.state.textureFile;
      data.versionCompatibility = this.state.versionCompatibility;
      if (this.state.layerType === 'S-57') {
        data.sourceEpsg = this.state.sourceEpsg;
        data.layerType = 'RAW_VECTOR';
        data.dataSource = '';
        data.stylingFormat = 'S52';
        if (this.state.epsg) {
          data.epsg = this.state.epsg;
        }
        data.locale = this.state.locale;
        this.setState({ dataSource: data.dataSource })
      }
      else {
        data.locale = '';
      }
      if (this.state.layerType == 'NATIVE_VECTOR') {
        if (this.state.dataSource) data.dataSource = this.state.dataSource;
        if (this.state.dtmLayerId) data.dtmLayerId = this.state.dtmLayerId;
        data.heightColumn = this.state.heightColumn;
        data.sideTexture = this.state.sideTexture;
        data.roofTexture = this.state.roofTexture;
        // sourceEpsg
      }
    }
    this.context.setData({ data })
    this.setState({ ...data });

  }

  setDefaultAdvancedData = () => {
    let rawParams = new window.MapCore.IMcMapLayer.SRawParams();
    const checkAdvancedData = this.context.getAdvancedData();
    const fields = {};
    if (this.state.layerType === 'NATIVE_VECTOR') {
      fields.maxVerticesPerTile = this.props.layer.RawLayerInfo && this.props.layer.RawLayerInfo.maxVerticesPerTile || '100000';
      fields.maxVisiblePointsPerTile = this.props.layer.RawLayerInfo && this.props.layer.RawLayerInfo.maxVisiblePointsPerTile || '5000';
      fields.minSizeForObjVisibility = this.props.layer.RawLayerInfo && this.props.layer.RawLayerInfo.minSizeForObjVisibility || '8';
      fields.optimizationMinScale = this.props.layer.RawLayerInfo && this.props.layer.RawLayerInfo.optimizationMinScale || '0';
    } else if (this.state.layerType == 'RAW_RASTER') {
      fields.resolveOverlapConflicts = this.props.layer.RawLayerInfo && this.props.layer.RawLayerInfo.Raster.ResolveOverlapConflicts || rawParams.bResolveOverlapConflicts;
      fields.enhanceBorderOverlap = this.props.layer.RawLayerInfo && this.props.layer.RawLayerInfo.Raster.EnhanceBorderOverlap || rawParams.bEnhanceBorderOverlap;
      fields.fillEmptyTilesByLowerResolutionTiles = this.props.layer.RawLayerInfo && this.props.layer.RawLayerInfo.Raster.FillEmptyTilesByLowerResolutionTiles || rawParams.bFillEmptyTilesByLowerResolutionTiles;
      fields.transparentColorA = this.props.layer.RawLayerInfo && this.props.layer.RawLayerInfo.Raster.TransparentColor ? 255 : 0;
      fields.transparentColorR = this.props.layer.RawLayerInfo && this.props.layer.RawLayerInfo.Raster.TransparentColor?.Red || rawParams.TransparentColor.r;
      fields.transparentColorG = this.props.layer.RawLayerInfo && this.props.layer.RawLayerInfo.Raster.TransparentColor?.Green || rawParams.TransparentColor.g;
      fields.transparentColorB = this.props.layer.RawLayerInfo && this.props.layer.RawLayerInfo.Raster.TransparentColor?.Blue || rawParams.TransparentColor.b;
      fields.byTransparentColorPrecision = this.props.layer.RawLayerInfo && this.props.layer.RawLayerInfo.Raster.TransparentColor?.TransparentColorPrecision || rawParams.byTransparentColorPrecision;
      fields.ignoreRasterPalette = this.props.layer.RawLayerInfo && this.props.layer.RawLayerInfo.Raster.IgnoreRasterPalette || rawParams.bIgnoreRasterPalette;
      fields.highestResolution = this.props.layer.RawLayerInfo && this.props.layer.RawLayerInfo.Raster.HighestResolution || rawParams.fHighestResolution;
      fields.maxScale = this.props.layer.RawLayerInfo && this.props.layer.RawLayerInfo.Raster.MaxScale || '';
    }
    !checkAdvancedData && this.context.setAdvancedData(fields)
    !checkAdvancedData && this.context.setAdvancedDataType(this.state.layerType);
    this.context.setAdvancedPopupType('edit');
  }

  handleHeightCoulmnChange = (e) => {
    this.setState({
      isHeightColumn: e
    })
    if (this.state.isFirstExecute) {
      this.setState({ isFirstExecute: false })
    } else {
      this.props.isChanged()
    }
  }

  getHeightColumn() {
    const name = "isHeightColumn";
    const label = "Contour Heights' Source";
    let defaultCode = "false";
    const filesArr = [{ code: "true", value: "Database Column" }, { code: "false", value: "Contour points" },];
    if (this.props.layer.RawLayerInfo.Vector3DExt.HeightColumn)
      defaultCode = 'true';
    if (defaultCode == undefined) {
      return ""
    }
    const dropDownData = {
      options: [...filesArr],
      defaultCode,
      label,
      fieldNames: { code: 'code', value: 'value' },
      onChange: this.handleHeightCoulmnChange,
      error: this.state.errors[name],
      readOnly: this.state.readOnly,
    };

    return (
      <div className={cn.Row}>
        <Select {...dropDownData} />
      </div>
    )
  }

  handleGroupsDropDownChanged = (selectedGroup) => {
    if (this.state.selectedGroups.includes(selectedGroup)) {
      this.setState({ selectedGroups: this.state.selectedGroups.filter(group => group !== selectedGroup) })
    } else {
      const newSelectedGroups = [...this.state.selectedGroups];
      newSelectedGroups.push(selectedGroup);
      this.setState({ selectedGroups: newSelectedGroups })
    }
  }

  isGroupSelected = (group) => {
    return this.state.selectedGroups.includes(group);
  }

  getGroupsNames() {
    const groupsList = this.props.groupsTree.map(group => ({ id: group.title, key: group.title, title: group.title, selected: this.isGroupSelected(group.title) }));
    const dropDownData = {
      label: 'Groups',
      titleHelper: 'group',
      title: 'Select group',
      list: groupsList,
      error: this.state.errors['selectedGroups'],
      toggleItem: this.handleGroupsDropDownChanged,
    };

    return (
      <div className={cn.Row}>
        <MultipleSelect {...dropDownData} />
      </div>
    )
  }

  handleEpsgChange = (epsgType, e) => {
    let epsgToState = epsgType == 'epsgTitle' ? 'sourceEpsg' : 'epsg';
    let epsgTitleToState = epsgType == 'epsgTitle' ? 'sourceEpsgTitle' : 'epsgTitle';
    let item, title;
    if (e) {
      item = this.context.epsgCodes.find(item => item.code == e);
      title = `${item.code}: ${item.desc}`;
    }
    else {
      title = "Select coordinate system";
    }
    this.setState({ [epsgToState]: e, [epsgTitleToState]: title })
    this.props.isChanged();
  }
  getTooltipName(epsgType) {
    let tooltipName = epsgType;
    if (epsgType == 'epsg' && this.state.layerType === 'RAW_DTM') {
      tooltipName = 'epsgDtm';
    }
    if (epsgType == 'epsg' && this.state.layerType === 'RAW_3D_MODEL') {
      tooltipName = 'epsg3DModel';
    }
    if (epsgType == 'epsg' && this.state.layerType === 'RAW_VECTOR_3D_EXTRUSION') {
      tooltipName = 'epsgTarget';
    }
    if (epsgType === 'epsgTitle' && this.state.layerType === 'RAW_VECTOR_3D_EXTRUSION') {
      tooltipName = 'epsgSource';
    }
    return tooltipName;
  }
  getDropDownTitleValue(epsgType) {
    let titleTmp = '';
    if (epsgType == 'epsg') {
      titleTmp = this.state.epsgTitle || "Select coordinate system"
    }
    else {
      titleTmp = this.state.sourceEpsgTitle || "Select coordinate system"
    }
    return titleTmp;
  }
  getEpsgLabel(epsgType) {
    let label = this.context.dict.coordinateSystem;
    if (epsgType === 'epsgTitle') {
      label = `${this.context.dict.source} ${this.context.dict.coordinateSystem}`
    }
    else if (this.state.layerType == 'RAW_VECTOR_3D_EXTRUSION' || this.state.layerType == 'RAW_3D_MODEL') {
      label = `${this.context.dict.target} ${this.context.dict.coordinateSystem}`;
    }
    return label;
  }
  getEPSGDropDown(epsgType) {
    let label = this.getEpsgLabel(epsgType);

    if (this.context.epsgCodes.length < 1) {
      let readOnly = epsgType === 'epsgTitle' ? true : false;
      return this.getInput(epsgType, label, { readOnly: readOnly });
    }
    const options = this.context.epsgCodes.map(item => {
      return {
        id: item.code,
        title: `${item.code}: ${item.desc}`,
        selected: item.code === this.state[epsgType == 'epsg' ? 'epsg' : 'sourceEpsg'],
        key: epsgType
      }
    });

    let title = this.getDropDownTitleValue(epsgType);
    let tooltipName = this.getTooltipName(epsgType);

    return (
      <div className={cn.Row}>
        <div className={cn.DropDownWrapper}>
          <Dropdown
            searchable={["Search coordinate system...", "No matching values"]}
            title={title}
            list={options}
            onChange={e => this.handleEpsgChange(epsgType, e)}
            label={label}
            error={this.state.errors[epsgType]}
            info={fieldsInfo[tooltipName]}
            type={epsgType}
          />
        </div>
      </div>
    )
  }
  handleTrueFalseDropDownChanged = (fieldType, id, title, stateKey) => {
    id = id == 'true' ? 'True' : 'False';
    this.setState({ [fieldType]: id });
    this.props.isChanged();
  }
  getTrueFalseDropDown(fieldType) {
    const filesArr = [{ code: "true", value: "True" }, { code: "false", value: "False" }];
    const options = filesArr.map(item => {
      return {
        id: item.code,
        title: item.value,
        selected: item.value === this.state[fieldType],
        key: fieldType
      }
    })
    const dropDownData = {
      onChange: (id, title, stateKey) => this.handleTrueFalseDropDownChanged(fieldType, id, title, stateKey),
      label: this.context.dict[fieldType],
      title: this.state[fieldType] || "True",
      list: options,
      fieldNames: { code: 'code', value: 'value' },
      isMandatory: false,
      info: fieldsInfo[fieldType]
    };

    return (
      <div className={cn.Row}>
        <div className={cn.DropDownWrapper}>
          <Dropdown {...dropDownData} />
        </div>
      </div>
    )
  }
  handleDropDownChanged = (fieldType, id, title, stateKey) => {
    this.setState({ [fieldType]: id });
    this.props.isChanged();
  }
  getDropDown(name, label, options) {
    let filesArr = name == 'objectIdColumn' || name == 'heightColumn' ? this.state.columnsOps : options.dropDownData;
    const optionsToSelect = filesArr ? filesArr.map(item => {
      return {
        id: item.name,
        title: item.name,
        selected: item.name === this.state[name],
        key: item.name
      }
    }) : [];

    let fileNames = filesArr?.map(f => f.name).flat();
    let title = fileNames?.find(n => n == this.state[name]);

    const dropDownData = {
      onChange: (id, title, stateKey) => this.handleDropDownChanged(name, id, title, stateKey),
      label: label,
      title: title,//in dropdown title==value - sm
      defaultTitle: 'Select Field',
      list: optionsToSelect,
      searchable: ["Search Field", "No matching values"],
      // fieldNames: { code: 'code', value: 'value' },
      isMandatory: options.mandatory,
      readOnly: options.readOnly,
      info: fieldsInfo[name],
      error: this.state.errors[name],
      checkboxLabel: options.checkboxLabel,
      handleCheckboxChange: (e) => { this.handleCheckboxChange(name) },
      checked: options.checked
    };

    return (
      <div className={cn.Row}>
        <div className={cn.DropDownWrapper}>
          <Dropdown {...dropDownData} />
        </div>
      </div>
    )
  }

  async vectorIndexingOps(indexOpsParams) {
    try {
      const response = await axios.post(config.urls.indexOps, indexOpsParams);
      if (response && response.data) {
        return response.data;
      }
    } catch (e) {
      console.error('Error when trying to vectorIndexing ' + indexOpsParams.layerId, e);
    }
  }

  async getColumnsOps(layerId) {
    try {
      let response = await this.vectorIndexingOps({ operation: config.indexOps.api.getStatus, layerId });
      if (response) {
        let allFields = await this.vectorIndexingOps({ operation: config.indexOps.api.getFields, layerId });
        return allFields;
      }
    }
    catch (e) {
      console.log(e)
    }
  }

  render() {
    return (
      <div>
        {this.state.layerType.includes('VECTOR') ?
          <div style={{ marginBottom: '15px' }}>{this.props.layer.DisplayedTitle}</div> : ''}
        <div className={cn.Wrapper}>
          <div className={cn.Split}>

            {this.getInput('layerId', this.context.dict.layer + " " + this.context.dict.id, { maxLength: '100', readOnly: true })}

            {this.getInput('title', this.context.dict.title, { maxLength: '100', mandatory: ['RAW_VECTOR', 'NATIVE_VECTOR'].includes(this.state.layerType) && this.props.checkShp ? false : true }, true)}

            {this.state.layerType === 'NATIVE_VECTOR' ?
              this.getInput('layerName', 'Sub-layer Name', { readOnly: true }) : null}

            {this.state.layerType.startsWith('RAW_VECTOR') ?
              this.getInput('dataSource', this.context.dict.dataSource, { readOnly: true }) : null}

            {this.state.layerType === 'RAW_VECTOR' || this.state.layerType === 'RAW_VECTOR_3D_EXTRUSION' ?
              this.getEPSGDropDown('epsgTitle') : null}

            {this.state.layerType.startsWith('RAW') ?
              this.getEPSGDropDown('epsg') : null}

            {this.state.layerType === 'RAW_VECTOR_3D_EXTRUSION' ?
              this.getInput('dtmLayerId', this.context.dict.dtmLayerId, { readOnly: true }) : null}


          </div>
          <div className={cn.Split}>
            {/*this.getGroupDropDown()*/}
            {this.getInput('groupName', this.context.dict.groupName, { readOnly: true })}

            {['RAW_VECTOR', 'NATIVE_VECTOR', 'RAW_RASTER', 'NATIVE_RASTER', 'NATIVE_TRAVERSABILITY', 'NATIVE_MATERIAL'].includes(this.state.layerType) ?
              this.getInput('drawPriority', this.context.dict.drawPriority, { type: 'number', maxLength: '100' }) : null}

            {this.state.layerType === 'RAW_VECTOR' ?
              this.getInput('textureFile', this.context.dict.defaultPointImage, { readOnly: true }) : null}

            {this.state.layerType === 'RAW_VECTOR' ?
              this.getInput('minScale', this.context.dict.minScale, { type: 'number', maxLength: '20' }) : null}

            {['RAW_VECTOR', 'RAW_DTM'].includes(this.state.layerType) ?
              this.getInput('maxScale', this.context.dict.maxScale, { type: 'number', maxLength: '20', isPositive: true }) : null}

            {this.state.layerType === 'RAW_DTM' ?
              this.getInput('highestResolution', 'Highest Resolution', { type: 'number' }) : null}

            {this.state.layerType === 'RAW_VECTOR_3D_EXTRUSION' ?
              this.getDropDown('heightColumn', this.context.dict.heightColumn, { mandatory: this.state.isHeightColumn, checkboxLabel: true, readOnly: !this.state.isHeightColumn, checked: this.state.isHeightColumn }) : null}

            {this.state.layerType === 'RAW_VECTOR_3D_EXTRUSION' ?
              this.getDropDown('objectIdColumn', 'Object Id Column', { mandatory: this.state.isObjectIdColumn, checkboxLabel: true, readOnly: !this.state.isObjectIdColumn, checked: this.state.isObjectIdColumn }) : null}

            {this.state.layerType === 'RAW_VECTOR_3D_EXTRUSION' ?
              this.getInput('roofTexture', this.context.dict.roofTexture, { readOnly: true }) : null}

            {this.state.layerType === 'RAW_VECTOR_3D_EXTRUSION' ?
              this.getInput('sideTexture', this.context.dict.sideTexture, { readOnly: true }) : null}

            {this.state.layerType === 'RAW_VECTOR_3D_EXTRUSION' ?
              this.getTrueFalseDropDown('useSpatiallyIndexing') : null}

            {this.state.layerType === 'RAW_3D_MODEL' ? this.getTrueFalseDropDown('orthometricHeights') : null}

            {this.state.layerType === 'RAW_3D_MODEL' ?
              this.getInput('targetHighestResolution', this.context.dict.targetHighestResolution, { type: 'number' }) : null}

          </div>
        </div>
      </div>
    );
  }
}

