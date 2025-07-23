import React, { PureComponent } from "react";
import cn from './ExportForm.module.css';
import mapDtm from '../../../assets/images/map-dtm.svg';
import mapVector from '../../../assets/images/map-vector.svg';
import mapRaster from '../../../assets/images/map-raster.svg';
import mapDefault from '../../../assets/images/map-default.svg';
import mapModel from '../../../assets/images/map-model.svg';
import folderClose from '../../../assets/images/folder-close.svg';

export default class EditForm extends PureComponent {

  constructor(props) {
    super(props);
    this.state = {
      layersToExport: this.props.node?.childNodes ? this.props.node.childNodes : this.props.node
    };
  }

  componentDidMount() {
    this.props.setParentHook(this.getLayersToExport);
  }

  getLayersToExport = () => {
    return this.state.layersToExport;
  }

  getMapImage = (type) => {
    let icon = mapDefault;

    if (type) {
      if (type.endsWith('RASTER')) {
        icon = mapRaster;
      } else if (type.endsWith('VECTOR')) {
        icon = mapVector;
      } else if (type.endsWith('DTM')) {
        icon = mapDtm;
      } else if (['NATIVE_STATIC_OBJECT', 'NATIVE_3D_MODEL', 'NATIVE_VECTOR_3D_EXTRUSION'].includes(type)) {
        icon = mapModel;
      } else if (type == "groups") {
        icon = folderClose;
      }
    }
    return <img src={icon} className={cn.MapIcon} />;
  }

  removeLayerFromExportList = layerIdToRemove => {
    if (this.state.layersToExport?.childNodes) {
      this.setState({
        layersToExport: this.state.layersToExport.filter(layer => layer.LayerId !== layerIdToRemove)
      })
    }
    else {
      this.setState({
        layersToExport: this.state.layersToExport?.filter(group => group.title !== layerIdToRemove)
      })
    }
  }

  filterToUniqueLayersToExport(fullSelectedLayers) {
    const uniqueMap = new Map();
    let uniqueFullSelectedLayers = [];
    fullSelectedLayers.forEach(layer => {
      const uniqueKey = layer.LayerId;
      if (!uniqueMap.has(uniqueKey)) {
        uniqueFullSelectedLayers.push(layer);
        uniqueMap.set(uniqueKey, true);
      }
    });
    return uniqueFullSelectedLayers.length;
  }

  getGroupLayers() {
    let layers = this.state.layersToExport;
    if (this.props.nodeLevel == "repository") {//repository export
      return <div className={cn.Header}>Are you sure you want to export all the repository?</div>;
    }
    else if (layers.LayerId) {//one layer
      return <div className={cn.Header}>Are you sure you want to export 1 layer?</div>;
    }
    else if (layers[0].LayerId && layers.length > 1) {//some layers selected
      let numLayers = this.filterToUniqueLayersToExport(layers);
      return <div className={cn.Header}>Are you sure you want to export {numLayers} layers?</div>;
    }
    else if (layers[0].LayerId) {//one selected layer
      return <div className={cn.Header}>Are you sure you want to export 1 layer? </div>;
    }
    else if (!layers[0].childNodes && !layers[0][0]?.childNodes) {//layer/s in unselected one group export
      let numLayers = layers[0][0] ? this.filterToUniqueLayersToExport(layers[1]) : this.filterToUniqueLayersToExport(layers);
      return <div className={cn.Header}>Are you sure you want to export {numLayers} layers?</div>;
    }
    else if (layers[1] && layers[1][0]?.LayerId) {//group and layer
      let layersArray = [];
      layers[1].forEach(layer => layersArray = [...layersArray, layer]);
      layers[0].forEach((group) => { group.childNodes.forEach(layer => layersArray = [...layersArray, layer]) });
      let numLayers = this.filterToUniqueLayersToExport(layersArray);
      return <div className={cn.Header}>Are you sure you want to export {numLayers} layers?</div>;
    }
    else {//group/s
      if (layers[1] && layers[1]?.length == 0) {
        layers = layers[0];
        this.setState({ layersToExport: layers });
      }
      let layersArray = [];
      layers.forEach((group) => { group.childNodes.forEach(layer => layersArray = [...layersArray, layer]) });
      let numLayers = this.filterToUniqueLayersToExport(layersArray);
      return <div className={cn.Header}>Are you sure you want to export {numLayers} layers?</div>;
    }
  }

  render() {
    return (
      <div className={cn.Wrapper}>
        {this.getGroupLayers()}
      </div>
    );
  }

}

