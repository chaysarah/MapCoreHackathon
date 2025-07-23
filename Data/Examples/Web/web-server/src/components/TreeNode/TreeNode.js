import React, { PureComponent } from 'react'
import cn from './TreeNode.module.css';
import folderOpen from '../../assets/images/folder-open.svg';
import folderClose from '../../assets/images/folder-close.svg';
import mapDtm from '../../assets/images/map-dtm.svg';
import mapVector from '../../assets/images/map-vector.svg';
import mapRaster from '../../assets/images/map-raster.svg';
import mapTraversability from '../../assets/images/map-traversability.svg';
import mapDefault from '../../assets/images/map-default.svg';
import mapModel from '../../assets/images/map-model.svg';
import config from '../../../src/config';
import ApplicationContext from '../../context/applicationContext';

export default class TreeNode extends PureComponent {

    constructor(props) {
        super(props);
        this.isToggleImgFolder = false
    }


    state = {
        visible: false,
    };


    static contextType = ApplicationContext;

    onNodeClick = (layerId, group, ctrlKey, childNodes, currentNode, shiftKey, mapLayersTree, dots3 = false) => {
        // let isDrag=false;//connected to mouse move
        let flagIsImgFolder = this.isToggleImgFolder;
        if (!flagIsImgFolder) {
            this.context.selectTreeLayer(currentNode, layerId, group, ctrlKey, childNodes, shiftKey, mapLayersTree, dots3);
            this.context.setLastClickedWithoutShiftNode(currentNode);
        }
        this.isToggleImgFolder = false;
    }
    // handleDragOver = (layerId,group) => {//TODO check....
    //     let isDrag=true;
    //     let isCtrl=false;
    //     this.context.selectTreeLayer(layerId, group,isCtrl,isDrag);
    // }
    toggle = () => {
        this.setState({ visible: !this.state.visible });
    }

    getMapImage = () => {
        let icon = mapDefault;

        const layerType = this.props.node.LayerType;
        if (layerType) {
            if (layerType.endsWith('RASTER')) {
                icon = mapRaster;
            } else if (layerType.endsWith('VECTOR')) {
                icon = mapVector;
            } else if (layerType.endsWith('DTM')) {
                icon = mapDtm;
            } else if (layerType.endsWith('TRAVERSABILITY')) {
                icon = mapTraversability;
            } else if (['NATIVE_STATIC_OBJECT', 'NATIVE_3D_MODEL', 'NATIVE_VECTOR_3D_EXTRUSION', 'RAW_3D_MODEL'].includes(layerType)) {
                icon = mapModel;
            }
        }
        return <img src={icon} className={cn.MapIcon} draggable="false" />;
    }

    onClearClick = (e) => {
        e.stopPropagation();
        this.props.onClearClick();
    }

    onEditClick = (e) => {
        e.stopPropagation();
        this.props.onEditClick(this.props.node);
    }

    onRemoveClick = (e) => {
        e.stopPropagation();
        this.props.onRemoveClick(this.props.node);
    }

    getEditBtn() {
        return (
            <button className={cn.ActionBtn} title='Edit' onClick={this.onEditClick}>
                <span className={`${cn.Icon} ${cn.EditIcon}`}></span>
            </button>
        )
    }

    getRemoveBtn() {
        return (
            <button className={cn.ActionBtn} title='Remove' onClick={this.onRemoveClick}>
                <span className={`${cn.Icon} ${cn.RemoveIcon}`}></span>
            </button>
        )
    }

    getChildNodeButtons() {
        return (
            <div className={cn.ChildNodeButtonsWrapper}>
                {this.getEditBtn()}
                {this.getRemoveBtn()}
            </div>
        )
    }
    toggleImgFolder = () => {
        this.isToggleImgFolder = true;
        this.toggle();
    }
    on3DotsClick(e, layerId, mapLayersTree) {
        let event = e;
        let nativeEventX = e.nativeEvent.x;
        let nativeEventY = e.nativeEvent.y;
        let dots3 = true;
        this.onNodeClick(layerId, layerId, e.ctrlKey, null, this.props.node, e.shiftKey, mapLayersTree, dots3);
        if (!this.props.node.childNodes) {
            this.onNodeClick(this.props.node.LayerId, this.props.node.parentFolder, e.ctrlKey, this.props.node.childNodes, this.props.node, e.shiftKey, mapLayersTree, dots3)
        }
        this.props.openContextMenu(event, this.props.nodeLevel, this.props.node, nativeEventX, nativeEventY);
    }
    render() {
        let childNodesHTMLElements, img, actionButtons, childsNum;
        const { childNodes, title, DisplayedTitle } = this.props.node;
        let isSelectedClassGroup = '';
        if (childNodes) {
            isSelectedClassGroup = this.context.selectedLayers.includes(`${title}${config.selectedLayerDelimiter}*`) ? ` ${cn.Selected}` : '';
        }
        else {
            isSelectedClassGroup = this.context.selectedLayers.includes(`${title}${config.selectedLayerDelimiter}${title}`) ? ` ${cn.Selected}` : '';
        }

        const { mapLayersTree } = this.props;
        if (childNodes != null) {
            //Folder
            childNodesHTMLElements = childNodes.map((node, index) => {
                const isSelectedClass = this.context.selectedLayers.includes(`${title}${config.selectedLayerDelimiter}${node.LayerId}`)
                    ||
                    this.context.selectedLayers.includes(`${title}${config.selectedLayerDelimiter}*`)
                    ? ` ${cn.Selected}` : '';
                return (<li className={`${cn.Child}${isSelectedClass}`} key={index} onClick={e => { e.preventDefault(); this.onNodeClick(node.LayerId, title, e.ctrlKey, childNodes, node, e.shiftKey, mapLayersTree, false, true); }} >
                    {/* onDragOver={e => this.handleDragOver(node.LayerId, title)} */}
                    <TreeNode
                        mapLayersTree={this.props.mapLayersTree}
                        node={node}
                        openContextMenu={this.props.openContextMenu}
                        nodeLevel={config.nodesLevel.layer}
                    />
                </li>)
            });

            actionButtons = this.context.selectedLayers.length > 0 ? <button className={cn.ClearBtn} onClick={this.onClearClick}>Clear</button> : null;
            childsNum = <span className={cn.ChildsNum}>{` (${childNodes.length})`}</span>;

            if (this.state.visible) {
                img = <img src={folderOpen} className={cn.FolderIcon} draggable="false" onClick={this.toggleImgFolder} />
            } else {
                img = <img src={folderClose} className={cn.FolderIcon} draggable="false" onClick={this.toggleImgFolder} />
            }
        } else {
            // child node
            img = this.getMapImage();
        }

        let style = {};
        if (!this.state.visible) {
            style = { display: "none" };
        }

        return (
            <div className={cn.Wrapper} style={{ userSelect: 'none' }} onDragOver={(e) => { e.preventDefault() }}>
                <div className={`${cn.Line}${isSelectedClassGroup}`} onDoubleClick={this.toggle} onClick={e => { e.preventDefault(); this.onNodeClick(this.props.node.LayerId || title, this.props.node.parentFolder || title, e.ctrlKey, null, this.props.node, e.shiftKey, mapLayersTree) }} >
                    {/* onDragOver={e => this.handleDragOver(title, title)} */}
                    <div className={cn.ImageTitleWrapper} >
                        {img}
                        {childsNum ?
                            <div className={cn.Title} title={title}>{title}{childsNum}</div>
                            : <div className={cn.Title} title={title}>{DisplayedTitle}</div>}
                    </div>
                    {/* {(this.props.node.title === 'Layer Backlog' && this.props.node.childNodes.length == 0) ? */}
                    {/* <span className={`${cn.Icon} ${cn.ContextIcon}`} /> : */}
                    <span className={`${cn.Icon} ${cn.ContextIcon}`} onClick={(e) => {
                        this.on3DotsClick(e, this.props.node.LayerId || title, mapLayersTree)
                    }} />
                    {/* } */}
                </div>
                {childNodesHTMLElements && childNodesHTMLElements.length > 0 ? <ul className={cn.List} style={style}>{childNodesHTMLElements}</ul> : null}
            </div>
        );
    }
}