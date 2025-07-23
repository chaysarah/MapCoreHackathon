import React, { PureComponent } from 'react'
import cn from './MapRepositoryTree.module.css';
import TreeNode from '../TreeNode/TreeNode';
import Loader from '../Loader/Loader';
import axios from 'axios';
import ApplicationContext from '../../context/applicationContext';
import config from '../../config';
import _ from 'lodash';

export default class MapRepositoryTree extends PureComponent {

    static contextType = ApplicationContext;

    constructor(props) {
        super(props);
        this.REPOSITORY_REFRESH_TIMEOUT = 30000;
    }

    state = {
        mapLayerTree: null,
        isFetchError: false,
        apiResponse: null,
        backlogNode: null,
        isOpenBacklog: false,
    }

    componentDidMount() {
        this.props.setParentHook(this.setApiResponseInState);
        this.getMapLayers();

        // setInterval(() => {
        //     this.refreshRepositoryTree()
        // }, this.REPOSITORY_REFRESH_TIMEOUT);
    }

    removeGroupFromLocalStorage(layerGroups, localGroups) {
        let tempLocalGroup = [...localGroups];

        layerGroups.forEach(group => {
            if (localGroups.includes(group)) {
                tempLocalGroup = tempLocalGroup.filter(g => g !== group);
            }
        });

        localStorage.setItem("newGroups", JSON.stringify(tempLocalGroup));
        return tempLocalGroup;
    }

    sortTreeByTitle(groupsTree) {
        let backlogGroup = groupsTree.filter(g => g.title == config.LAYERS_BACKLOGS_TITLE)
        backlogGroup[0].childNodes.sort((a, b) => {
            let first = a.DisplayedTitle ? a.DisplayedTitle : a.Title;
            let second = b.DisplayedTitle ? b.DisplayedTitle : b.Title;
            return first.localeCompare(second);
        });
        let groupTreeWithoutBacklog = groupsTree.filter(g => g.title != config.LAYERS_BACKLOGS_TITLE)
        groupTreeWithoutBacklog.sort((a, b) => a.title.localeCompare(b.title));
        groupTreeWithoutBacklog.forEach(group => group.childNodes.sort((a, b) => {
            let first = a.DisplayedTitle ? a.DisplayedTitle : a.Title;
            let second = b.DisplayedTitle ? b.DisplayedTitle : b.Title;
            return first.localeCompare(second);
        }));
        return [...backlogGroup, ...groupTreeWithoutBacklog];
    }

    buildTreeLayer = () => {
        let storedGroups = JSON.parse(localStorage.getItem("newGroups")) || [];
        const mapTreeArr = [];
        const treeMap = new Map();
        const searchValue = this.props.searchValue.toLowerCase();

        for (let index = 0; index < this.state.apiResponse.length; index++) {
            let layer = this.state.apiResponse[index];
            const layerGroups = layer.Group.split(',');
            storedGroups = this.removeGroupFromLocalStorage(layerGroups, storedGroups);
            if (this.state.apiResponse[index]?.LayerType === 'NATIVE_VECTOR' && this.state.apiResponse[index].ClientMetadata) {
                layer = {
                    ...layer, DisplayedTitle: layer.Title ? layer.Title + ' (' + this.state.apiResponse[index].ClientMetadata.McSubLayerName + ' ' + this.state.apiResponse[index].ClientMetadata.McSubLayerGeometry + ")" : this.state.apiResponse[index].ClientMetadata.McSubLayerName + ' (' + this.state.apiResponse[index].ClientMetadata.McSubLayerGeometry + ")"
                };
            } else {
                layer = { ...layer, DisplayedTitle: layer.Title };
            }
            if (!searchValue || this.state.apiResponse[index].Title.toLowerCase().includes(searchValue)) {

                //layer.title = layer.Title;
                layer = { ...layer, title: layer.Title };
                for (let i = 0; i < layerGroups.length; i++) {
                    let group = layerGroups[i];
                    // layer.parentFolder = group; // used for knowing the group in edit layer screen
                    layer = {
                        ...layer,
                        parentFolder: group.startsWith(config.BACKLOG_PREFIX) ? config.LAYERS_BACKLOGS_TITLE : group
                    };
                    if (group.startsWith(config.BACKLOG_PREFIX)) {
                        group = config.LAYERS_BACKLOGS_TITLE;
                    }

                    if (treeMap.has(group)) {
                        const layersArr = treeMap.get(group);
                        layersArr.push(layer);
                    } else {
                        const layersArr = [layer];
                        treeMap.set(group, layersArr);
                    }
                }
            }
        }

        let containsLayersBacklogGroup = false;
        for (let [groupName, layersArr] of treeMap) {
            // set Layer Backlog as first group            
            const item = {
                title: groupName,
                childNodes: layersArr
            }
            if (groupName === config.LAYERS_BACKLOGS_TITLE) {
                containsLayersBacklogGroup = true;
                mapTreeArr.unshift(item);
            } else {
                mapTreeArr.push(item);
            }
        }

        if (!containsLayersBacklogGroup) {
            const item = {
                title: config.LAYERS_BACKLOGS_TITLE,
                childNodes: []
            }
            mapTreeArr.unshift(item);
        }

        storedGroups && storedGroups.forEach(groupName => {
            const item = {
                title: groupName,
                childNodes: []
            }
            if (!searchValue || item.title.includes(searchValue)) {
                mapTreeArr.push(item);
            }
        })
        let sortTree = this.sortTreeByTitle(mapTreeArr);
        return sortTree;
    }

    componentDidUpdate(prevProps) {
        if (prevProps.searchValue !== this.props.searchValue) {
            const filteredData = this.buildTreeLayer();
            this.setState({ mapLayerTree: filteredData });
        }
    }
    setApiResponseInState = (apiResponse) => {
        this.setState({ apiResponse: apiResponse.MapLayerConfig }, () => {
            const mapLayerTree = this.buildTreeLayer();
            this.setState({ mapLayerTree });
            this.props.onTreeChange(mapLayerTree);
            this.props.updateRepositoryData(apiResponse);
        });
    }
  
    getMapLayers = () => {
        this.setState({ isFetchError: false }, async () => {
            try {
                axios.get(config.urls.layersInfo,
                    {
                        headers: {
                            'Accept': 'application/json',
                            'Content-Type': 'application/json'
                        }
                    }
                ).then(mapLayersRespone => {
                    if (mapLayersRespone && mapLayersRespone.data && mapLayersRespone.data.MapLayerConfig) {
                        if (!localStorage.getItem('mapScaleFactor'))
                            localStorage.setItem('mapScaleFactor', mapLayersRespone.data.DefaultMaxScaleFactor);
                        this.setApiResponseInState(mapLayersRespone.data);
                        //const mapTreeLayer = this.buildTreeLayer(mock2.MapLayerConfig);
                    } else {
                    }
                }).catch(e => {
                    console.error(e);
                })

            }
            catch (e) {
                console.log(e);
                this.setState({ isFetchError: true });
                this.setState({ mapLayerTree: null });
                //this.setState({mapLayerTree: mock});
            }
        });
    }
    getMapsTree() {
        let backlogGroupTmp = this.state.mapLayerTree.filter(group => group.title == config.LAYERS_BACKLOGS_TITLE);
        let backlogGroup = backlogGroupTmp[0];
        this.setState({ backlogNode: backlogGroup });
        this.props.setBacklogGroup(backlogGroup);
        this.props.setOnClearClick(this.props.onClearClick);
        let filteredTree = this.state.mapLayerTree.filter(group => group.title != config.LAYERS_BACKLOGS_TITLE);
        const tree = filteredTree.map((folder, index) =>
            // const tree = this.state.mapLayerTree.map((folder, index) =>
            <TreeNode
                mapLayersTree={this.state.mapLayerTree}
                key={index}
                node={folder}
                searchedValue={this.props.searchedValue}
                onClearClick={this.props.onClearClick}
                openContextMenu={this.props.openContextMenu}
                nodeLevel={config.nodesLevel.group}
            />
        );
        return tree;
    }
    refreshRepositoryTree = () => {
        this.getMapLayers();
    }
    getTreeComponent() {
        let comp = null;
        if (this.state.mapLayerTree) {
            comp = this.getMapsTree();
        } else if (this.state.isFetchError) {
            comp = <div className={cn.Error}>{this.context.dict.errorFetchLayers}</div>;
        } else {
            comp = <Loader />;
        }
        return comp;
    }

    render() {
        return (
            <div className={cn.Wrapper}> {this.state.backlogNode ?
                <TreeNode
                    mapLayersTree={this.state.mapLayerTree}
                    key={0}
                    node={this.state.backlogNode}
                    searchedValue={this.props.searchedValue}
                    onClearClick={this.props.onClearClick}
                    openContextMenu={this.props.openContextMenu}
                    nodeLevel={config.nodesLevel.group}
                />
                : ""}
                <div className={cn.HeaderWrapper}>
                    <h2 className={cn.Header}>{this.context.dict.mapLayersRepository}</h2>
                    <div className={cn.Buttons}>
                        <button
                            className={cn.ActionBtn}
                            title={this.context.dict.showOptions}
                            onClick={(e) => this.props.openContextMenu(e, config.nodesLevel.repository, this.state.mapLayerTree)}
                        >
                            <span className={`${cn.Icon} ${cn.ContextIcon}`}></span>
                        </button>
                    </div>
                </div>
                <div>
                    {this.getTreeComponent()}
                </div>
            </div>
        )
    }
}