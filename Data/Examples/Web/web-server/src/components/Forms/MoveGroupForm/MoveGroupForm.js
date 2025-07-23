import React, { PureComponent } from "react";
import cn from './MoveGroupForm.module.css';
import { fieldsValidation, validationFunctions, validationMessages } from './MoveGroupFormFields';
import ApplicationContext from '../../../context/applicationContext';
import { DropdownMultiple as MultipleSelect } from '../../MultipleSelect';
import config from "../../../config";

export default class MoveGroupFrom extends PureComponent {
  static contextType = ApplicationContext;
  constructor(props) {
    super(props);
    this.state = {
      isShowGroupName: false,
      selectedGroups: this.getSelectedGroups(props.layer),//props.layer.Group.split(','),
      layerType: props.layer.LayerType,
      errors: {},
    };
  }

  getSelectedGroups(layerDetails) {
    if (this.props.selctedGroupsLayersNodesArr[1].length > 1) {//for multi selection - select groups
      let groupsArr = [];
      let groupsArrOfFirstLayer = this.props.selctedGroupsLayersNodesArr[1][0].Group.split(',');
      groupsArrOfFirstLayer.forEach(group => {
        let isSharedGroup = true;
        this.props.selctedGroupsLayersNodesArr[1].forEach(layer => {
          let layerG = layer.Group.split(',');
          if (!layerG.includes(group)) {
            isSharedGroup = false;
          }
        });
        if (isSharedGroup) {
          groupsArr.push(group);
        }
      })
      return groupsArr;
    }
    else if (layerDetails.parentFolder === config.LAYERS_BACKLOGS_TITLE) {
      return [];
    }
    return layerDetails.Group.split(',')
  }



  setDefaultFieldsStateByType(layerType) {
    // set the dropDowns data with the value they are getting on render
    if (this.props.selctedGroupsLayersNodesArr[1].length > 1 || this.context.selectedLayers.length > 1) {//for multi selection - select groups
      let layersArr = this.props.selctedGroupsLayersNodesArr[1].length > 1 ? this.props.selctedGroupsLayersNodesArr[1] : this.props.findLayersNodesOfMultiBacklog();
      let arrForState = [];
      layersArr.forEach(layer => {
        let fieldsOfOneLayer = this.setDefaultFieldsStateByTypeForOneLayer(layer?.LayerType, layer)
        arrForState.push(fieldsOfOneLayer);
      });
      this.setState({ selctedLayersFieldsArr: arrForState })
    }
    else {
      let fields = this.setDefaultFieldsStateByTypeForOneLayer(layerType)
      this.setState({ ...fields });
    }

  }
  setDefaultFieldsStateByTypeForOneLayer(layerType, oneLayerFromArr = null) {
    let finalLayer = oneLayerFromArr ? oneLayerFromArr : this.props.layer;
    const fields = {
      groupName: finalLayer.parentFolder,
      layerId: finalLayer.LayerId,
      title: finalLayer.Title,
      drawPriority: finalLayer.DrawPriority,
    }
    if (layerType.startsWith('RAW_')) {
      fields.epsg = finalLayer.RawLayerInfo && finalLayer.RawLayerInfo.CoordinateSystem ?
        finalLayer.RawLayerInfo.CoordinateSystem.Code : '';
    }
    if (layerType === 'RAW_VECTOR') {
      fields.dataSource = finalLayer.RawLayerInfo && finalLayer.RawLayerInfo.Vector ?
        finalLayer.RawLayerInfo.Vector.DataSource : '';
      fields.sourceEpsg = finalLayer.RawLayerInfo && finalLayer.RawLayerInfo.Vector && finalLayer.RawLayerInfo.Vector.SourceCoordinateSystem ?
        finalLayer.RawLayerInfo.Vector.SourceCoordinateSystem.Code : '';
      fields.minScale = finalLayer.RawLayerInfo && finalLayer.RawLayerInfo.Vector ?
        finalLayer.RawLayerInfo.Vector.MinScale : '';
      fields.maxScale = finalLayer.RawLayerInfo && finalLayer.RawLayerInfo.Vector ?
        finalLayer.RawLayerInfo.Vector.MaxScale : '';
      fields.textureFile = finalLayer.RawLayerInfo && finalLayer.RawLayerInfo.Vector ?
        finalLayer.RawLayerInfo.Vector.PointTextureFile : '';
    }
    if (layerType === 'RAW_VECTOR_3D_EXTRUSION') {
      fields.dataSource = finalLayer.RawLayerInfo && finalLayer.RawLayerInfo.Vector3DExt ?
        finalLayer.RawLayerInfo.Vector3DExt.DataSource : '';
      fields.dtmLayerId = finalLayer.RawLayerInfo && finalLayer.RawLayerInfo.Vector3DExt ?
        finalLayer.RawLayerInfo.Vector3DExt.DtmLayerId : '';
      fields.heightColumn = finalLayer.RawLayerInfo && finalLayer.RawLayerInfo.Vector3DExt ?
        finalLayer.RawLayerInfo.Vector3DExt.HeightColumn : '';
      fields.textureSide = finalLayer.RawLayerInfo && finalLayer.RawLayerInfo.Vector3DExt ?
        finalLayer.RawLayerInfo.Vector3DExt?.SideDefaultTexture?.TextureFile : '';
      fields.textureRoof = finalLayer.RawLayerInfo && finalLayer.RawLayerInfo.Vector3DExt ?
        finalLayer.RawLayerInfo.Vector3DExt?.RoofDefaultTexture?.TextureFile : '';
    }
    return fields;
  }

  componentDidMount() {
    this.props.setParentHook(this.getEditFormData);
    this.setDefaultFieldsStateByType(this.state.layerType);
  }

  validateForm() {
    if (this.props.selctedGroupsLayersNodesArr[1].length > 1 || this.context.selectedLayers.length > 1) {//for multi selection - select groups
      let layersArr = this.props.selctedGroupsLayersNodesArr[1].length > 1 ? this.props.selctedGroupsLayersNodesArr[1] : this.props.findLayersNodesOfMultiBacklog();
      let isAllLayersrOk = true;
      layersArr.forEach((layer, i) => {
        if (!this.validateOneLayer(layer, i)) {
          isAllLayersrOk = false;
        }
      });
      return isAllLayersrOk;
    }
    else {
      return this.validateOneLayer();
    }
  }
  validateOneLayer(oneLayerFromArr = null, i = -1) {
    let statePlace = oneLayerFromArr ? this.state.selctedLayersFieldsArr[i] : this.state;
    const errors = {};
    Object.keys(fieldsValidation).forEach(field => {
      // Check only for fields that are inside the state (we put those fiels in) setDefaultFieldsStateByType
      if (statePlace[field]) {
        const valueToValidate = statePlace[field];
        const validationsToRun = fieldsValidation[field];
        validationsToRun.forEach(validation => {
          const isValid = validationFunctions[validation](valueToValidate);
          if (!isValid) {
            errors[field] = validationMessages[validation];
          }
        })
      }
    });
    this.setState({ errors });
    return Object.keys(errors).length > 0 ? false : true;
  }

  getEditFormData = () => {
    const isValid = this.validateForm();

    if (isValid) {
      if (this.props.selctedGroupsLayersNodesArr[1].length > 1 || this.context.selectedLayers.length > 1) {//for multi selection - select groups
        let data = [];
        let layersArr = this.props.selctedGroupsLayersNodesArr[1].length > 1 ? this.props.selctedGroupsLayersNodesArr[1] : this.props.findLayersNodesOfMultiBacklog();
        layersArr.forEach((layer, i) => {
          let oneLayerData = this.getEditFormDataForOneLayer(layer, i);
          data.push(oneLayerData);
        });
        return data;
      } else {
        return this.getEditFormDataForOneLayer();
      }
    }

  }

  getEditFormDataForOneLayer(oneLayerFromArr = null, i = -1) {
    let statePlace = oneLayerFromArr ? this.state.selctedLayersFieldsArr[i] : this.state;
    let data = false;
    data = {
      layerId: statePlace.layerId,
      title: statePlace.title,
      drawPriority: statePlace.drawPriority || '',
      group: this.state.selectedGroups?.join()
    };

    if (this.state.layerType.startsWith('RAW_')) {
      data.epsg = statePlace.epsg;
    }

    if (this.state.layerType === 'RAW_VECTOR') {
      data.dataSource = statePlace.dataSource;
      data.sourceEpsg = statePlace.sourceEpsg;
      data.minScale = statePlace.minScale || '';
      data.maxScale = statePlace.maxScale || '';
      data.textureFile = statePlace.textureFile;
    }

    if (this.state.layerType === 'RAW_VECTOR_3D_EXTRUSION') {
      data.dataSource = statePlace.dataSource;
      data.dtmLayerId = statePlace.dtmLayerId;
      data.heightColumn = statePlace.heightColumn;
      data.sideTexture = statePlace.sideTexture;
      data.roofTexture = statePlace.roofTexture;
    }

    return data;
  }

  handleGroupsDropDownChanged = (selectedGroup) => {
    if (this.state.selectedGroups.includes(selectedGroup)) {
      if (this.state.selectedGroups.length == 1) {
        this.props.isNotEmptyAndChanged(false);
      }
      this.setState({ selectedGroups: this.state.selectedGroups.filter(group => group !== selectedGroup) })
    } else {
      this.props.isNotEmptyAndChanged(true);
      const newSelectedGroups = [...this.state.selectedGroups];
      newSelectedGroups.push(selectedGroup);
      this.setState({ selectedGroups: newSelectedGroups })
    }
  }

  isGroupSelected = (group) => {
    return this.state.selectedGroups.includes(group);
  }

  getGroupsNames() {

    const groupsList = this.props.groupsTree.filter(group => group.title !== config.LAYERS_BACKLOGS_TITLE).map(group =>
    ({
      id: group.title,
      key: group.title,
      title: group.title,
      selected: this.isGroupSelected(group.title)
    }
    )
    );

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

  render() {
    return (
      <div className={cn.Wrapper}>
        {this.getGroupsNames('selectedGroups', this.context.dict.groupName)}
      </div>
    );
  }

}
