import React, { Component } from 'react';
import ApplicationContext from './applicationContext';
import config from '../config';
import axios from 'axios';


class ApplicationState extends Component {

    constructor(props) {
        super(props);

        this.selectedLayersLocal = [];

        this.state = {
            selectedLang: window.localStorage.getItem('appUILang') || "en",
            dict: null,
            epsgCodes: [],
            DTMs: [],
            backlogDTMs: [],
            initialDataError: false,
            isMapCoreSDKLoaded: false,
            mapToPreview: '',
            selectedLayers: [],
            sideNotifications: [],
            activeMapPreview: 'Map',
            mapScaleFactor: null,
            tableData: [],
            tmpTableData: [],
            tilingScheme: [],
            rowNum: null,
            tilingSchemeChanged: false,
            SSLChanged: false,
            lastClickedWithoutShiftNode: null,
        }
    }

    _socket = {}
    updateRow = [

    ]


    findWsUrl(path) {
        let v = window.location.href;
        if (v.toLowerCase().startsWith("https")) {
            v = "wss" + v.substr(5);
        }
        else if (v.toLowerCase().startsWith("http")) {
            v = "ws" + v.substr(4);
        }
        return v;
    }

    setMessages = (msgs) => {
        this.setState({
            lastRecvWsMessages: msgs,
            newWsMessageArrived: msgs.length > 0
        })
    }

    changeLang = selectedLang => {
        if (selectedLang) {
            this.setState({ selectedLang });
            window.localStorage.setItem('appUILang', selectedLang);
        }
    };

    setMapPreviewDetails = (item) => {
        this.setState({ mapToPreview: item });
    }

    closeMapPreview = () => {
        this.setState({ mapToPreview: '' });
    }

    setMapPreviewCurrentAction = (activeMapPreview) => {
        this.setState({ activeMapPreview });
    }


    setMapCoreSDKLoaded = () => {
        this.setState({ isMapCoreSDKLoaded: true });
    }
    setInitialDataError = (initialDataError) => {
        this.setState({ initialDataError: initialDataError })
    }

    async componentDidMount() {
        try {
            // const promises = [
            //     axios.get(config.urls.dictionary),
            //     axios.get(config.urls.epsgCodes),
            //     axios.get(config.urls.compatibeVersions)
            // ]

            // const [dictionaryRes, epsgCodesRes, compatibleVersionsRes] = await axios.all(promises);
            const dictionaryRes = await axios.get(config.urls.dictionary);
            const compatibleVersionsRes = await axios.get(config.urls.compatibeVersions);
            const epsgCodesRes = await axios.get(config.urls.epsgCodes);

            let v = this.findWsUrl(config.urls.wsBase);

            this.setState(
                {
                    dict: dictionaryRes.data,
                    epsgCodes: epsgCodesRes.data.filter(item => item.code !== 0),
                    compatibeVersions: compatibleVersionsRes.data,
                    wsUrl: v,
                }
            )


        } catch (err) {
            console.error(err);
            this.setState({ initialDataError: 'Error when trying to load initial data' })

        }
        // this.getDTMLayers()
        this.getAdvancedData()
    }

    getDTMLayers = async () => {
        try {
            let DTMLayersRespone = await axios.get(config.urls.getDTMs);
            if (DTMLayersRespone && DTMLayersRespone.data && DTMLayersRespone.data.MapLayerConfig) {
                let DTMLayers = DTMLayersRespone.data.MapLayerConfig.filter(item => item.Group !== "BACKLOG:");
                let backlogDTMs = DTMLayersRespone.data.MapLayerConfig?.filter(item => item.Group === "BACKLOG:");
                this.setState({ DTMs: DTMLayers, backlogDTMs });
                return DTMLayers
            }

        } catch (e) {
            console.log(e);
        }
    }
    getAdvancedData = () => {
        return this.state.advancedData;
    }
    setAdvancedData = (data) => {
        this.setState({ advancedData: data });
    }
    getAdvancedDataType = () => {
        return this.state.advancedDataType;
    }
    setAdvancedDataType = (type) => {
        this.setState({ advancedDataType: type });
    }
    getAdvancedPopupType = () => {
        return this.state.advancedPopupType;
    }
    setAdvancedPopupType = (popupType) => {
        this.setState({ advancedPopupType: popupType });
    }
    getSSLData = () => {
        return this.state.SSLData;
    }
    setSSLData = (data) => {
        this.setState({ SSLData: data });
    }
    setReadOnly = (data) => {
        this.setState({ readOnly: data });
    }
    setReadOnlyTilingScheme = (data) => {
        this.setState({ readOnlyTilingScheme: data });
    }
    setIsDtmPreviewActivated = (data) => {
        this.setState({ isDtmPreviewActivated: data });
    }
    setReadOnlyAfterTilingScheme = (data) => {
        this.setState({ readOnlyAfterTilingScheme: data });
    }
    getMapScaleFactorValue = () => {
        return this.state.mapScaleFactor;
    }
    setMapScaleFactorValue = (data) => {
        this.setState({ mapScaleFactor: data });
    }
    getData = () => {
        return this.state.data;
    }
    setConfigData = (data) => {
        this.setState({ configData: data });
    }
    getConfigData = () => {
        return this.state.configData;
    }
    setTableData = (data) => {
        this.setState({ tmpTableData: [...this.state.tmpTableData, data], tableData: [...this.state.tableData, data] })
    }
    initTableData = (tableData) => {
        this.setState({ tableData })
    }
    setTableDataEdit = (data) => {
        let tableData = [...this.state.tableData];
        let item = { ...tableData[this.state.rowNum] }
        item.epsg = data.epsg;
        item.epsgTitle = data.epsgTitle;
        item.tilingScheme = data.tilingScheme;
        tableData[this.state.rowNum] = item;
        this.setState({ tableData })
    }
    getTableData = () => {
        return this.state.tableData;
    }
    setTilingScheme = () => {
        this.setState({ tilingScheme: this.state.tableData })
    }
    setTilingSchemeChanged = (bool) => {
        this.setState({ tilingSchemeChanged: bool })
    }
    setSSLChanged = (bool) => {
        this.setState({ SSLChanged: bool })
    }
    getTilingScheme = () => {
        return this.state.tilingScheme
    }
    cleanTableData = () => {
        this.setState({ tmpTableData: [] })
    }
    cancelTmpTableData = () => {
        this.state.tableData.splice(this.state.tableData.length - this.state.tmpTableData.length, this.state.tmpTableData.length)
        this.setState({ tmpTableData: [] },)
    }
    deleteTableData = (ondelete) => {
        this.setState({ tableData: [] }, () => { ondelete() })
    }
    setIsRequireTableData = (bool) => {
        this.setState({ isRequireDataTable: bool })
    }
    setRowNum = (num) => {
        this.setState({ rowNum: num })
    }
    setDeleteRow = (key, ondelete) => {
        const tableData = this.state.tableData.filter((_, index) => index != key)
        this.setState({
            tableData
        }, () => {
            ondelete(tableData)
        })
    }
    setData = (data) => {
        this.setState({ data: data });
    }
    getDevice = () => {
        return this.state.device;
    }
    setDevice = (data) => {
        this.setState({ device: data });
    }
    // #region Selection
    setLastClickedWithoutShiftNode = (node) => {
        this.setState({ lastClickedWithoutShiftNode: node }, () => {
        })
    }

    selectGroupChildNodesInsteadAllGroup(selectedLayersKeys, childNodes, selectedLayer, group) {
        let finalSelectedLayersKeys = selectedLayersKeys.filter(layer => layer != `${group}${config.selectedLayerDelimiter}*`);//take out the group
        if (childNodes) {
            childNodes.forEach((layer) => {
                if (layer.LayerId != selectedLayer) {
                    const key = `${group}${config.selectedLayerDelimiter}${layer.LayerId}`;
                    finalSelectedLayersKeys = finalSelectedLayersKeys.concat(key);
                }
            });
            return finalSelectedLayersKeys;
        }
        return selectedLayersKeys;
    }
    findLastNodeClickedId(tmpLastClickedWithoutShiftNode = false) {
        let lastNodeId = "";
        let groupOrLayer = "";
        let lastNode = tmpLastClickedWithoutShiftNode ? tmpLastClickedWithoutShiftNode : this.state.lastClickedWithoutShiftNode;
        if (lastNode && lastNode.childNodes) {
            lastNodeId = lastNode.title;
            groupOrLayer = "group";
        }
        else if (lastNode) {
            lastNodeId = lastNode.LayerId;
            groupOrLayer = "layer";
        }
        return [lastNodeId, groupOrLayer];
    }
    findLastNodeClickedGroup() {
        let lastNodeId = "";
        if (this.state.lastClickedWithoutShiftNode && this.state.lastClickedWithoutShiftNode.childNodes) {
            lastNodeId = this.state.lastClickedWithoutShiftNode.title;
        }
        else if (this.state.lastClickedWithoutShiftNode) {
            lastNodeId = this.state.lastClickedWithoutShiftNode.parentFolder;
        }
        return lastNodeId;
    }
    selectLayersByShiftKeyFromGrpToGrp(currentNode, mapLayersTree, selectedLayer, group, lastNodeId) {
        let flag = false;
        mapLayersTree.forEach((groupIn) => {
            if (groupIn.title == group) {//end choice in this group
                if (groupIn.title == selectedLayer && currentNode.childNodes) {//end choice is this group
                    flag = false;
                    let ctrlKey = true;
                    this.selectTreeLayer(groupIn, groupIn.title, groupIn.title, ctrlKey, groupIn.childNodes, false, mapLayersTree);
                }
                else {//end choice is one of the layers
                    this.selectTreeLayer(groupIn.childNodes[0], groupIn.childNodes[0].LayerId, groupIn.childNodes[0].parentFolder, true, null, false, mapLayersTree, false, true);
                    this.selectLayersByShiftKeyFromLyrToLyr(currentNode, mapLayersTree, selectedLayer, group, groupIn.childNodes[0].LayerId);
                    flag = false;
                }
            }
            if (flag) {
                let ctrlKey = true;
                this.selectTreeLayer(groupIn, groupIn.title, groupIn.title, ctrlKey, groupIn.childNodes, false, mapLayersTree);
            }
            if (groupIn.title == lastNodeId) {//start choice
                flag = true;
            }
        });

    }
    selectLayersByShiftKeyFromLyrToLyr(currentNode, mapLayersTree, selectedLayer, group, lastNodeId, groupOfLastSelectedLayer = group) {
        let flag = false;
        mapLayersTree.forEach((groupIn, index) => {
            if (groupIn.title == group) {
                groupIn.childNodes.forEach((layer) => {
                    if (flag) { //between...
                        let ctrlKey = true;
                        this.selectTreeLayer(layer, layer.LayerId, layer.parentFolder, ctrlKey, null, false, mapLayersTree, false, true);
                    }
                    if (layer.LayerId == selectedLayer && !currentNode.childNodes && layer.parentFolder == groupOfLastSelectedLayer) {//end choice
                        flag = false;
                    }
                    if (layer.LayerId == lastNodeId) {//start choice
                        flag = true;
                    }
                    if (layer.LayerId == lastNodeId && layer.LayerId == selectedLayer && groupOfLastSelectedLayer == group) {//start and end chices are the same layerId in the same group
                        this.selectTreeLayer(layer, layer.LayerId, layer.parentFolder, true, null, false, mapLayersTree, false, true);
                        flag = false;
                    }
                });
                if (flag && groupOfLastSelectedLayer == selectedLayer) {
                    this.selectLayersByShiftKeyFromGrpToGrp(currentNode, mapLayersTree, selectedLayer, selectedLayer, mapLayersTree[index].title);
                }
                else if (flag) {
                    this.selectLayersByShiftKeyFromGrpToGrp(currentNode, mapLayersTree, selectedLayer, groupOfLastSelectedLayer, mapLayersTree[index].title);
                }
            }
        });

    }
    setSelectedLayerNodeInsteadLastClickedWithoutShiftNode(selectedLayer, group, mapLayersTree) {
        let groupIndex = mapLayersTree.findIndex(groupIn => groupIn.title == group);
        let node = mapLayersTree[groupIndex].childNodes.find(layer => layer.LayerId == selectedLayer);
        this.setState({ lastClickedWithoutShiftNode: node });
        return node;
    }
    isUpSelectionByShiftKey(startGroup, endGroup, mapLayersTree, selectedLayer, lastLayer) {
        let startI = mapLayersTree.findIndex(group => group.title == startGroup);
        let endI = mapLayersTree.findIndex(group => group.title == endGroup);
        if (startI == endI) {//selection in the same group
            let begin = mapLayersTree[startI].childNodes.findIndex(layer => layer.LayerId == selectedLayer);
            let end = mapLayersTree[startI].childNodes.findIndex(layer => layer.LayerId == lastLayer);
            return begin < end;
        }
        return startI > endI;
    }
    filterToUniqueKeys(selectedLayers) {
        const uniqueMap = new Map();
        let uniqueSelectedLayers = [];
        selectedLayers.forEach(layerKey => {
            if (!uniqueMap.has(layerKey)) {
                uniqueSelectedLayers.push(layerKey);
                uniqueMap.set(layerKey, true);
            }
        });
        return uniqueSelectedLayers;
    }
    setGroupInstaedAllItsLayers(selectedLayersUp, mapLayersTree) {
        selectedLayersUp = this.filterToUniqueKeys(selectedLayersUp);
        mapLayersTree.forEach(group => {
            let counter = 0;
            for (let index = 0; index < selectedLayersUp.length; index++) {
                if (selectedLayersUp[index].split('###')[0] == group.title) {
                    counter++;
                }
            }
            let groupKey = `${group.title}###*`;
            if (counter == group.childNodes.length && counter != 0) {
                selectedLayersUp = selectedLayersUp.filter(layerKey => layerKey.split('###')[0] !== group.title);
                selectedLayersUp = [...selectedLayersUp, groupKey];
            }
        });
        return selectedLayersUp;
    }
    setUpSelectionLayersInSelectedLayersState(mapLayersTree, selectedLayerFrom, lastNodeIdTo, group) {
        let mapLayersTreeFlat = mapLayersTree.map((group) => group.childNodes).flat();
        let startI = mapLayersTreeFlat.findIndex(layer => layer.LayerId == selectedLayerFrom && layer.parentFolder == group);
        let endI = mapLayersTreeFlat.findIndex(layer => layer.LayerId == lastNodeIdTo && layer.parentFolder == this.findLastNodeClickedGroup());
        let selectedLayersUp = this.selectedLayersLocal;
        if (selectedLayerFrom != lastNodeIdTo && startI != -1) {
            for (let index = startI; index < endI; index++) {
                let layerKey = `${mapLayersTreeFlat[index]?.parentFolder}###${mapLayersTreeFlat[index]?.LayerId}`;
                selectedLayersUp = [...selectedLayersUp, layerKey];
            }
            selectedLayersUp = this.setGroupInstaedAllItsLayers(selectedLayersUp, mapLayersTree);
            this.selectedLayersLocal = selectedLayersUp;
        }
        this.setState({ selectedLayers: selectedLayersUp });
    }
    setUpSelectionGroupToGroup(group1, group2, mapLayersTree) {
        let mapLayersTreeFlat = mapLayersTree.map((group) => group.title).flat();
        let startI = mapLayersTreeFlat.findIndex(group => group == group1);
        let endI = mapLayersTreeFlat.findIndex(group => group == group2);
        let selectedLayersUp = this.selectedLayersLocal;
        for (let index = startI; index < endI; index++) {
            let layerKey = `${mapLayersTreeFlat[index]}###*`;
            selectedLayersUp = [...selectedLayersUp, layerKey];
        }
        this.selectedLayersLocal = selectedLayersUp;
        this.setState({ selectedLayers: selectedLayersUp });
    }
    getFirstLayerNodeInGroupByGroupId(group, mapLayersTree, isLast = false) {
        let ind = mapLayersTree.findIndex(groupIn => groupIn.title == group);
        let LastOrFirst = isLast ? mapLayersTree[ind].childNodes.length - 1 : 0;
        return mapLayersTree[ind].childNodes[LastOrFirst].LayerId;
    }
    selectLayersByShiftKey(currentNode, mapLayersTree, selectedLayer, group, ctrlKey) {
        let switchedGroup = "";
        let [lastNodeId, groupOrLayer] = this.findLastNodeClickedId();
        let isUpSelection = this.isUpSelectionByShiftKey(this.findLastNodeClickedGroup(), group, mapLayersTree, selectedLayer, this.state.lastClickedWithoutShiftNode.LayerId);
        if (isUpSelection) {
            if (groupOrLayer == "layer" && (selectedLayer != group || !currentNode.childNodes)) {//start layer - end layer
                this.setUpSelectionLayersInSelectedLayersState(mapLayersTree, selectedLayer, lastNodeId, group);
            }
            else if (groupOrLayer == "group" && (selectedLayer != group || !currentNode.childNodes)) {//start group - end layer
                let length = this.state.lastClickedWithoutShiftNode.childNodes.length;

                this.setUpSelectionLayersInSelectedLayersState(mapLayersTree, selectedLayer, this.state.lastClickedWithoutShiftNode.childNodes[length - 1].LayerId, group);
            }
            else if (groupOrLayer == "layer" && selectedLayer == group && currentNode.childNodes) {//start layer - end group
                if (group == this.state.lastClickedWithoutShiftNode.parentFolder) {//from layer to its group
                    this.setState({ selectedLayers: [`${group}###*`] });
                }
                else {
                    let finalLayer = this.getFirstLayerNodeInGroupByGroupId(selectedLayer, mapLayersTree);
                    this.setUpSelectionLayersInSelectedLayersState(mapLayersTree, finalLayer, this.state.lastClickedWithoutShiftNode.LayerId, group);
                }
            }
            else if (groupOrLayer == "group" && selectedLayer == group && currentNode.childNodes) {//start group - end group
                this.setUpSelectionGroupToGroup(group, this.state.lastClickedWithoutShiftNode.title, mapLayersTree);
            }
        }
        else {
            switch (groupOrLayer) {
                case "group":
                    this.selectLayersByShiftKeyFromGrpToGrp(currentNode, mapLayersTree, selectedLayer, group, lastNodeId);
                    break;
                case "layer":
                    this.selectLayersByShiftKeyFromLyrToLyr(currentNode, mapLayersTree, selectedLayer, switchedGroup || this.state.lastClickedWithoutShiftNode.parentFolder, lastNodeId, group);
                    break;
                default:
                    break;
            }
        }
    }
    isSelectedLayersContainNotBacklogLayers() {
        let flag = false;
        this.state.selectedLayers.forEach((layer) => {
            if (layer.split("###")[0] !== config.LAYERS_BACKLOGS_TITLE) {
                flag = true;
            }
        });
        return flag;
    }
    isHasOneLayerInGroup(group, mapLayersTree) {
        let groupNode = mapLayersTree.find(groupIn => groupIn.title == group);
        return groupNode ? groupNode.childNodes.length == 1 : false;
    }
    selectTreeLayer = (currentNode, selectedLayer, group, ctrlKey, childNodes, shiftKey, mapLayersTree, dots3 = false, shiftLayers = false) => {
        if (group == config.LAYERS_BACKLOGS_TITLE && group == selectedLayer && currentNode.childNodes) {
            let selectedLayers = [];
            let key = `${group}${config.selectedLayerDelimiter}*`;
            selectedLayers = [key];
            this.selectedLayersLocal = selectedLayers
            this.setState({ selectedLayers })
        }
        else if (group == config.LAYERS_BACKLOGS_TITLE && (group !== selectedLayer || !currentNode.childNodes) && (this.state.selectedLayers.length == 0 || this.isSelectedLayersContainNotBacklogLayers())) {
            let selectedLayers = [];
            let key = `${group}${config.selectedLayerDelimiter}${selectedLayer}`;
            selectedLayers = [key];
            this.selectedLayersLocal = selectedLayers
            this.setState({ selectedLayers })
        }
        else if (group !== config.LAYERS_BACKLOGS_TITLE && !this.isSelectedLayersContainNotBacklogLayers()) {
            let selectedLayers = [];
            let finalKey = currentNode.childNodes ? '*' : selectedLayer;
            let key = `${group}${config.selectedLayerDelimiter}${finalKey}`;
            selectedLayers = [key];
            this.selectedLayersLocal = selectedLayers
            this.setState({ selectedLayers })
        }
        else if (shiftKey) {
            this.selectLayersByShiftKey(currentNode, mapLayersTree, selectedLayer, group, ctrlKey);
        }
        else {
            let selectedLayers = [];
            let finalKey = currentNode.childNodes ? '*' : selectedLayer;
            let key = `${group}${config.selectedLayerDelimiter}${finalKey}`;
            let fatherKey = "";
            if (selectedLayer !== group || !currentNode.childNodes) {//layer
                fatherKey = `${group}${config.selectedLayerDelimiter}*`;
            }
            // if (isDrag) {
            //     selectedLayers = this.state.selectedLayers.concat(key);
            // }
            if (this.state.selectedLayers.includes(key) && ctrlKey) {
                selectedLayers = this.state.selectedLayers.length == 1 ? selectedLayers = [key] : this.state.selectedLayers.filter(layer => layer !== key);
            }//||dots3
            else if (this.state.selectedLayers.includes(key)) {
                // selectedLayers = this.state.selectedLayers.length == 1 ? this.state.selectedLayers.filter(layer => layer !== key) : selectedLayers = [key];
                selectedLayers = dots3 ? this.state.selectedLayers : [key];
            }
            else {//the layer/group wasnt chosen
                if (this.state.selectedLayers == []) { if (!dots3) { selectedLayers = [key]; } }//maybe should use this.selectedLayersLocal in this line -check it sm
                else if (ctrlKey) {
                    if (selectedLayer == group && currentNode.childNodes) {//group
                        this.selectedLayersLocal.forEach((layerKey, ind) => {
                            let [groupName, layerId] = layerKey.split(config.selectedLayerDelimiter);
                            if (groupName !== group) { selectedLayers = [...selectedLayers, this.selectedLayersLocal[ind]]; }
                        });
                        selectedLayers = [...selectedLayers, key];
                    }
                    else {//layer
                        if (this.state.selectedLayers.includes(fatherKey)) {
                            selectedLayers = this.selectGroupChildNodesInsteadAllGroup(this.state.selectedLayers, childNodes, selectedLayer, group)
                        }
                        else {
                            selectedLayers = shiftLayers ? this.selectedLayersLocal.concat(key) : this.state.selectedLayers.concat(key);
                        }
                    }
                }
                else {
                    selectedLayers = [key];
                }
            }
            selectedLayers = this.filterToUniqueKeys(selectedLayers);
            // selectedLayers = this.isHasOneLayerInGroup(group, mapLayersTree) ? selectedLayers : this.setGroupInstaedAllItsLayers(selectedLayers, mapLayersTree);
            this.selectedLayersLocal = selectedLayers;
            this.setState({ selectedLayers });
        }
    }

    clearSelectedLayers = () => {
        this.setState({ selectedLayers: [] })
    }
    // #endregion

    showSideNotification = (notification) => {
        this.setState({ sideNotifications: [notification, ...this.state.sideNotifications] });
    }

    removeAllSideNotifications = () => {
        this.setState({ sideNotifications: [] });
    }

    removeSideNotification = (text) => {
        const sideNotifications = this.state.sideNotifications.filter(notification => notification.text != text);
        this.setState({ sideNotifications });
    }

    render() {
        const allLanguages = this.state.dict ? Object.keys(this.state.dict) : [];
        return (
            <ApplicationContext.Provider
                value={
                    {
                        setDevice: this.setDevice,
                        getDevice: this.getDevice,
                        allLanguages,
                        readOnly: this.state.readOnly,
                        readOnlyTilingScheme: this.state.readOnlyTilingScheme,
                        isDtmPreviewActivated: this.state.isDtmPreviewActivated,
                        readOnlyAfterTilingScheme: this.state.readOnlyAfterTilingScheme,
                        rowNum: this.state.rowNum,
                        isRequireDataTable: this.state.isRequireDataTable,
                        mapScaleFactor: this.state.mapScaleFactor,
                        selectedLang: this.state.selectedLang,
                        dict: this.state.dict && this.state.dict[this.state.selectedLang],
                        epsgCodes: this.state.epsgCodes,
                        DTMs: this.state.DTMs,
                        backlogDTMs: this.state.backlogDTMs,
                        compatibeVersions: this.state.compatibeVersions,
                        initialDataError: this.state.initialDataError,
                        setInitialDataError: this.setInitialDataError,
                        mapToPreview: this.state.mapToPreview,
                        isMapCoreSDKLoaded: this.state.isMapCoreSDKLoaded,
                        selectedLayers: this.state.selectedLayers,
                        wsUrl: this.state.wsUrl,
                        tilingSchemeChanged: this.state.tilingSchemeChanged,
                        SSLChanged: this.state.SSLData,
                        tmpTableData: this.state.tmpTableData,
                        lastClickedWithoutShiftNode: this.state.lastClickedWithoutShiftNode,
                        setReadOnly: this.setReadOnly,
                        setReadOnlyTilingScheme: this.setReadOnlyTilingScheme,
                        setIsDtmPreviewActivated: this.setIsDtmPreviewActivated,
                        setReadOnlyAfterTilingScheme: this.setReadOnlyAfterTilingScheme,
                        getConfigData: this.getConfigData,
                        setConfigData: this.setConfigData,
                        getSSLData: this.getSSLData,
                        setSSLData: this.setSSLData,
                        getAdvancedData: this.getAdvancedData,
                        setAdvancedData: this.setAdvancedData,
                        getAdvancedDataType: this.getAdvancedDataType,
                        setAdvancedDataType: this.setAdvancedDataType,
                        getAdvancedPopupType: this.getAdvancedPopupType,
                        setAdvancedPopupType: this.setAdvancedPopupType,
                        setMapScaleFactorValue: this.setMapScaleFactorValue,
                        getMapScaleFactorValue: this.getMapScaleFactorValue,
                        getData: this.getData,
                        setData: this.setData,
                        getTableData: this.getTableData,
                        setTableData: this.setTableData,
                        initTableData: this.initTableData,
                        setTilingScheme: this.setTilingScheme,
                        setTilingSchemeChanged: this.setTilingSchemeChanged,
                        setSSLChanged: this.setSSLChanged,
                        getTilingScheme: this.getTilingScheme,
                        setTableDataEdit: this.setTableDataEdit,
                        cleanTableData: this.cleanTableData,
                        cancelTmpTableData: this.cancelTmpTableData,
                        deleteTableData: this.deleteTableData,
                        setIsRequireTableData: this.setIsRequireTableData,
                        setRowNum: this.setRowNum,
                        setDeleteRow: this.setDeleteRow,
                        refreshDTMLayers: this.getDTMLayers,
                        selectTreeLayer: this.selectTreeLayer,
                        setLastClickedWithoutShiftNode: this.setLastClickedWithoutShiftNode,
                        clearSelectedLayers: this.clearSelectedLayers,
                        setMapCoreSDKLoaded: this.setMapCoreSDKLoaded,
                        changeLang: this.changeLang,
                        setMapPreviewDetails: this.setMapPreviewDetails,
                        sideNotifications: this.state.sideNotifications,
                        showSideNotification: this.showSideNotification,
                        removeAllSideNotifications: this.removeAllSideNotifications,
                        removeSideNotification: this.removeSideNotification,
                        closeMapPreview: this.closeMapPreview,
                        setMapPreviewCurrentAction: this.setMapPreviewCurrentAction,
                        onMessageArrived: this.onMessageArrived,
                    }
                }
            >
                {this.props.children}
            </ApplicationContext.Provider>
        )
    }
}

export default ApplicationState;