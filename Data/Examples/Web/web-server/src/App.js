import React, { PureComponent } from "react";
import logo from "../src/assets/images/newLogo.svg";
import classNames from "./App.module.css";
import Clock from "./components/Clock/Clock";
import MapRepositoryTree from "./components/MapRepositoryTree/MapRepositoryTree";
import RunningProcesses from "./components/RunningProcesses/RunningProcesses";
import MapPreviewActions from "./components/MapPreviewActions/MapPreviewActions";
import searchIcon from "./assets/images/search.svg";
import config, { popupTypes, popupSize, layerTypesStrings, mapActions } from "./config";
import Popup from "./components/Popup/Popup";
import { ImportForm, EditForm, ExportForm, AddRenameGroupForm, RemoveForm, PreviewConfigurationForm, ServerConfigurationForm, MoveGroupForm, Dropzone, VectorIndexingForm, AdvancedForm, SSLForm, TilingSchemeForm, TilingSchemeTableForm } from './components/Forms';
import Error from './components/Error/Error';
import axios from "axios";
import Select from './components/Select/Select';
import ApplicationContext from './context/applicationContext';
import MapContainer from '../src/components/MapContainer/MapContainer';
import ContextMenu from '../src/components/ContextMenu/ContextMenu';
import removeIcon from './assets/images/delete.svg';
import editIcon from './assets/images/edit.svg';
import addIcon from './assets/images/add.svg';
import exportIcon from './assets/images/export.svg';
import configIcon from './assets/images/config.svg';
import aboutIcon from './assets/images/about.svg';
import mapDtm from './assets/images/map-dtm.svg';
import mapVector from './assets/images/map-vector.svg';
import mapRaster from './assets/images/map-raster.svg';
import mapDefault from './assets/images/map-default.svg';
import mapModel from './assets/images/map-model.svg';
import mapIcon from './assets/images/map.svg';
import mapS57 from './assets/images/map-s57.svg';
import moveGroupIcon from './assets/images/move-group.svg';
import vectorIndexingIcon from './assets/images/build-indexes.svg';
import Loader from './components/LoaderAlt/LoaderAlt';
import About from './components/Forms/About/About';
import { OpenMapService, ViewportService, MapCoreData, MapCoreService } from 'mapcore-lib';
import { connect } from "react-redux";
import { SaveMapToPreview, SetActionDtmMapPreview, SetActionMapPreview, SetErrorInPreview, SetOpenMapService } from "./redux/MapContainer/MapContainerAction";
import dots3 from './assets/images/context.svg';
import { treatError } from "./errorHandler";

var _socket = null;
var valid = null;

class App extends PureComponent {
    static contextType = ApplicationContext;
    constructor(props) {
        super(props);

        this.repositoryTree = React.createRef();
        this.contextMenu = React.createRef();
        this.mapContainer = React.createRef();
        this.DELIMITER = '%';
        this.block = true;
        this.oncloseClick = this.hadneleBeforeunload();
        this.keepAliveInterval = null;
        this.KEEP_ALIVE_TIMEOUT = 10 * 60 * 1000;
        this.flag = false;
        this.selectedLayersNodes = []
        this.selectedGroupsNodes = []
        this.selectedGroupsKeys = []
        this.selectedLayersKeys = []
        this.isUploadingFile = false;
        this.isExportingFile = false;
        this.isCompletedUploadInServer = true;
        this.timerIsCompletedUploadInServer = null;
        this.isSecondRemovePopup = false;
        this.isLayerRemoveFromPreviewedLayers = false;
        this.isDTMLayerExistingFlag = true;


        this.state = {
            searchValue: '',
            currentlyUploadedFiles: [],
            currentlyRecvWsMessages: [],
            currentlyProcessedFiles: [],
            layerGroupsTree: [],
            repositoryData: {},
            lastRecvMessage: {},
            generalErrorMessage: false,
            popupToOpen: null,
            messageFilters: {},
            uploadingLayerId: "",
            isNameChanged: false,
            refreshInterval: '',
            closeErrorPopup: false,
            isValid: null,
            backlogGroup: {},
            onClearClick: null,
            selctedLayersFromContext: [],
            NativeUploadLayerType: "",
            isCompletedUploadInServer: true,
            isErrorFromWebSocket: false,
            isStaticObjectsBin: false,
            isBin3DModelOr3DExtr: "",
            popupToOpenTmpStaticObj: null,
            mcPackageDefaultLayerId: null,
            finalDefaultNameForLargeMcPackage: null,
            webSocketErrorMessagesInUpload: [],
            layersToPreview: [],
            logs: [],
            uploadRawHeader: '',
        };

        axios.interceptors.request.use(request => this.requestHandler(request));
    }
    async componentDidMount() {
        window.onbeforeunload = () => {
            //openpopup
            //window.close();
            if (this.isUploadingFile || this.isExportingFile) {
                return "you have an ongoing upload. are you sure you want to leave?"
            }
        }
        try {
            await window.McStartMapCore();
        } catch (error) {
            let errorMessage = error.message;
            const assertionIndex = errorMessage.indexOf(' Build with -sASSERTIONS for more info.');
            if (assertionIndex !== -1) {
                errorMessage = errorMessage.substring(0, assertionIndex).trim();
            }
            this.context.setInitialDataError(`MapCore Wasm files cannot be loaded. ${errorMessage}`)
        }

        MapCoreData.onErrorCallback = treatError;
        let openMapService = new OpenMapService()
        this.props.SetOpenMapService(openMapService)
        MapCoreService.initMapCore();
        MapCoreService.initDevice(new window.MapCore.IMcMapDevice.SInitParams());
    }

    async componentDidUpdate() {
        if (this.context.mapToPreview?.data?.RawLayerInfo) {
            this.isDTMLayerExistingFlag = await this.isDTMLayerExisting(this.context.mapToPreview.data?.RawLayerInfo?.Vector3DExt?.DtmLayerId);
        }
    }

    componentWillUnmount() {
        this.handleComponentClose();
        window.onbeforeunload = null;
        clearInterval(this.timerIsCompletedUploadInServer);

    }

    handleComponentClose() {
        this.stopKeepAlive();
        if (_socket != null && window.sessionStorage.getItem('sessionId') != null) {
            this.websocket.send("unsubscribe " + window.sessionStorage.getItem('sessionId'));
        }
    }
    requestHandler = request => {
        request.headers['X-CALLING-APP'] = 'management';
        return request;
    };
    isValid = (bool) => {
        valid = bool;
        this.setState({ isValid: bool })
        return bool;
    }
    renderInitialErrorMessage() {
        return (
            <div className={classNames.App} >
                <Popup
                    buttonOk='OK'
                    header='Error'
                    hideXButton
                    onOk={this.closeErrorPopup}>
                    <Error errorMsg={this.context.initialDataError} />
                </Popup>
            </div>
        );
    }
    //#region Log region
    getLogHeader() {
        return (
            <div className={classNames.LogHeader}>
                <header style={{ position: 'relative', left: '8px' }}>Log Messages:</header>
                <img src={dots3} className={classNames.LogContext} onClick={this.getLogContextMenu} />
            </div>
        )
    }
    getLogContextMenu = (e) => {
        let isLog = true;
        this.openContextMenu(e, "layer", null, 0, 0, isLog);
    }
    clearLogMessages = () => {
        this.setState({ logs: [] });
    }
    getLog() {
        return (
            <div className={classNames.Log}>
                {this.getLogHeader()}
                <div className={classNames.LogMessages}>
                    {this.state.logs.map((item, i) => {
                        let color = item.color == 'red' ? 'rgb(253, 47, 47)' : 'rgba(255, 255, 255, 0.966)';
                        return (
                            <div style={{ color: `${color}`, fontSize: '0.78em' }}>{item.message}</div>
                        )
                    })}
                </div>
            </div>
        );
    }
    //#endregion

    //#region getMainHeader
    getMainHeader() {
        return (
            <header className={classNames.AppHeader}>
                <img src={logo} alt='logo' />
                <div className={classNames.RightHeader}>
                    {/*this.getLangSelector()*/}
                    <Clock />
                </div>
            </header>
        );
    }
    getLangSelector() {
        const langs = this.context.allLanguages.map(lang => ({
            code: lang,
            value: lang.toUpperCase()
        }));

        const dropDownData = {
            styles: { borderColor: '#d9d9d929', padding: '5px 10px' },
            options: [...langs],
            onChange: this.handleLangChanged,
            fieldNames: { code: 'code', value: 'value' },
            defaultCode: this.context.selectedLang,
            isSmall: true
        };

        return (
            <div className={classNames.LangSelect}>
                <Select {...dropDownData} />
            </div>
        );
    }
    handleLangChanged = selectedCode => {
        this.context.changeLang(selectedCode);
    };
    //#endregion

    //#region getMainContent 
    getMainContent() {
        return (
            <div className={classNames.MainContentWrapper}>
                <div className={`${classNames.Split} ${classNames.Left}`}>
                    {this.getMainLeftPane()}
                </div>
                <div className={`${classNames.Split} ${classNames.Right}`}>
                    {this.getMainRightPane()}
                </div>
            </div>
        );
    }

    getMainLeftPane() {
        return (
            <span>
                <MapRepositoryTree
                    setOnClearClick={getOnClearClick => (this.setState({ onClearClick: getOnClearClick }))}
                    setBacklogGroup={getSetBacklogGroup => (this.state.backlogGroup = getSetBacklogGroup)}
                    ref={this.repositoryTree}
                    searchValue={this.state.searchValue.trim()}
                    openContextMenu={this.openContextMenu}
                    onTreeChange={this.getRepositoryTree}
                    updateRepositoryData={this.updateRepositoryData}
                    setParentHook={getChildState =>
                        (this.mapTreeRepoSetLayers = getChildState)
                    }
                />
            </span>
        );
    }
    getRepositoryTree = layerGroupsTree => {
        this.setState({ layerGroupsTree });
    };

    updateRepositoryData = repositoryData => {
        this.setState({ repositoryData });
        let newTableData = [];
        let tableData = repositoryData.TilingSchemePolicy.TilingSchemesByCRS;
        for (let i = 0; i < tableData.length; i++) {
            newTableData[i] = { epsgTitle: tableData[i].CoordinateSystem.Code + ":" + tableData[i].CoordinateSystem.SRIDType, tilingScheme: tableData[i].TilingScheme };
        }
        this.context.initTableData(newTableData);
    };
    getProcessesPane() {
        return <span>
            <RunningProcesses
                uploadFiles={this.state.currentlyUploadedFiles}
                processedFiles={this.state.currentlyProcessedFiles}
                convertedFiles={this.state.currentlyProcessedFiles}
                onClearClick={this.clearProcesses}
            />
            {this.getLog()}
        </span>
    }
    getMainRightPane() {
        const { mapToPreview, closeMapPreview } = this.context;
        const { closeErrorPopup, noCoordSystemInNewLayer } = this.state;
        let rightPane = null;

        const rules = [
            {
                name: 'noMapToShow',
                priority: 1,
                condition: () => !mapToPreview || closeErrorPopup || this.isLayerRemoveFromPreviewedLayers,
                action: () => {
                    rightPane = this.getProcessesPane();
                }
            },
            {
                name: 'missingDtm',
                priority: 2,
                condition: () => mapToPreview?.layerType === "RAW_VECTOR_3D_EXTRUSION" && !this.isDTMLayerExistingFlag,
                action: () => {
                    const backlogDTMs = this.context?.backlogDTMs || [];
                    const dtmLayerId = this.context.mapToPreview?.data?.RawLayerInfo?.Vector3DExt?.DtmLayerId;
                    const isInBacklog = backlogDTMs.some(item => item.LayerId === dtmLayerId);
                    const errorMsg = isInBacklog ? "The DTM you requested is in the backlog and cannot be displayed."
                        : "The DTM you requested, does not exist.";

                    this.setState({ generalErrorMessage: errorMsg }, () => this.getGeneralErrorPopup());
                }
            },
            {
                name: 'tooManyDtms',
                priority: 3,
                condition: () => (mapToPreview?.type === "group" || mapToPreview?.type === "group and layer") && this.isMoreOneDTM(mapToPreview.type),
                action: () => {
                    this.setState({ generalErrorMessage: "Group with more than one DTM layer cannot be previewed" }, () => this.getGeneralErrorPopup());
                    closeMapPreview();
                }
            },
            {
                name: 'missingCoordSystem',
                priority: 4,
                condition: () => (noCoordSystemInNewLayer),
                action: () => {
                    this.setState({ generalErrorMessage: "The uploaded layer has no coordinate system. Preview is not possible" }, () => this.getGeneralErrorPopup());
                    closeMapPreview();
                }
            }
        ];
        const defaultAction = () => {
            rightPane = <MapContainer ref={this.mapContainer} />;
        };

        //Runs the condition with the highest priority that was true.
        const matchedRule = rules.sort((a, b) => a.priority - b.priority).find(rule => rule.condition());
        (matchedRule?.action || defaultAction)();

        return rightPane;
    }
    isDTMLayerExisting = async (dtmId) => {
        const dtms = await this.context.refreshDTMLayers();
        let isFound = dtms?.find(item => item.LayerId === dtmId);
        return isFound;
    }

    isMoreOneDTM = (mapToPreviewType) => {
        let arrOfLayers = mapToPreviewType == "group" ?
            this.context.mapToPreview.data.childNodes : [...this.context.mapToPreview.data[0].map(g => g.childNodes).flat(), ...this.context.mapToPreview.data[1]];
        let dtms = arrOfLayers.filter((layer) => { return layer.LayerType.includes('DTM') })
        return dtms.length > 1 ? true : false;
    }
    clearProcesses = (processType, layerTitles) => {
        const currentlyUploadedFiles = [...this.state.currentlyUploadedFiles];

        for (let j = 0; j < currentlyUploadedFiles.length; j++) {
            const process = currentlyUploadedFiles[j];

            if (
                layerTitles.includes(
                    process.layersParams.title +
                    this.DELIMITER +
                    process.keyForUploadedLayer
                )
            ) {
                process.isShown = false;
            }
        }

        this.setState({ currentlyUploadedFiles }, () => {
            if (!this.isUploading) {
                this.clearUnShownFiles();
            }
        });
    };

    filterToUniqueSelectedLayers(fullSelectedLayers, isOnlyKeys = false) {
        const uniqueMap = new Map();
        let uniqueFullSelectedLayers = [];
        fullSelectedLayers.forEach(layer => {
            const uniqueKey = isOnlyKeys ? layer : layer?.LayerId;
            if (!uniqueMap.has(uniqueKey)) {
                uniqueFullSelectedLayers.push(layer);
                uniqueMap.set(uniqueKey, true);
            }
        });
        return uniqueFullSelectedLayers;
    }
    findLayersNodesOfMultiBacklog = () => {
        let nodesArr = [];
        this.context.selectedLayers.forEach(layerKey => {
            let tmpArr = layerKey.split(config.selectedLayerDelimiter);
            let backlogGroup = this.state.layerGroupsTree.filter(g => g.title == "Layer Backlog");
            let layerNode = backlogGroup[0].childNodes?.filter(layer => layer.LayerId == tmpArr[1])[0];
            nodesArr.push(layerNode);
        });
        return nodesArr;
    }
    findGroupsOrLayersToRemoveOrExport() {
        const selectedLayers = this.state.selctedLayersFromContext;
        const treeNodes = this.state.layerGroupsTree;
        let fullSelectedGroups = treeNodes.filter(group => selectedLayers.includes(`${group.title}${config.selectedLayerDelimiter}*`));
        let selectedGroupsKeys = [];
        fullSelectedGroups.forEach(selctedGroup => { selectedGroupsKeys.push(`${selctedGroup.title}${config.selectedLayerDelimiter}*`); });
        let fullSelectedLayers = [];
        treeNodes.forEach(group => {
            group.childNodes.forEach(layer => {
                let finalGroup = layer.Group;
                if (layer.Group.includes(",")) {
                    let allLayerGroups = layer.Group.split(",");
                    allLayerGroups.forEach((groupInSome) => {
                        if (selectedLayers.includes(`${groupInSome}${config.selectedLayerDelimiter}${layer.LayerId}`)) {
                            fullSelectedLayers = fullSelectedLayers.concat(layer);
                        }
                    });
                }
                if (selectedLayers.includes(`${finalGroup}${config.selectedLayerDelimiter}${layer.LayerId}`)) {
                    fullSelectedLayers = fullSelectedLayers.concat(layer);
                }
            })
        });
        let uniqueFullSelectedLayers = this.filterToUniqueSelectedLayers(fullSelectedLayers);
        let selectedLayersKeys = [];
        uniqueFullSelectedLayers.forEach(selctedLayer => { selectedLayersKeys.push(`${selctedLayer.Group}${config.selectedLayerDelimiter}${selctedLayer.LayerId}`); });
        this.selectedLayersNodes = uniqueFullSelectedLayers;
        this.selectedGroupsNodes = fullSelectedGroups;
        this.selectedGroupsKeys = selectedGroupsKeys;
        this.selectedLayersKeys = selectedLayersKeys;
    }
    updatePopupForStaticObjects() {
        if (this.state.isStaticObjectsBin) {
            this.setState({ popupToOpen: this.state.popupToOpenTmpStaticObj })
        }
    }
    //#endregion

    //#region getPopup 
    getPopup() {
        let selctedLayersFromContext = this.context.selectedLayers;
        this.setState({ selctedLayersFromContext }, () => { this.findGroupsOrLayersToRemoveOrExport() })
        this.updatePopupForStaticObjects();
        if (this.state.popupToOpen && !this.state.isStaticObjectsBin) {
            if (this.state.popupToOpen.name == "Add Layer") {
                let rawHeader = this.state.popupToOpen.layerType == "Native Layer" ? "" : "Raw ";
                let finalLayerType = this.state.popupToOpen.layerType;
                this.setState({
                    popupToOpen: {
                        ...this.state.popupToOpen,
                        name: "Add " + rawHeader + finalLayerType
                    }
                });
            }
            return (
                <Popup
                    readOnly={this.context.readOnly}
                    disabledOk={this.state.isNameChanged}
                    buttonOk={this.getPopupOkButtonLabel()}
                    buttonCancel={this.getCancelButton()}
                    linkAdvanced={this.getAdvanced()}
                    header={this.state.NativeUploadLayerType != "" ? this.state.NativeUploadLayerType : this.state.popupToOpen.name}
                    onCancel={this.closePopup}
                    onOk={this.getPopupFunction()}
                    onAdvanced={this.getAdvancedPopup}
                    noBodyOverflow={this.getPopupBodyOverflow()}
                    size={this.getPopupSize()}>
                    {this.getPopupBody()}
                </Popup>
            );
        }
        else if (this.state.isStaticObjectsBin) {
            return (
                <Popup
                    buttonOk={"Vector 3D Extrusion"}
                    buttonCancel={"3D Model"}
                    header={"Static Objects Type"}
                    onCancel={this.onStaticObjects3DModelChosen}
                    onOk={this.onStaticObjects3DExtrChosen}
                >
                    {<div>Is the uploaded layer type 3D Model or Vector 3D Extrusion?</div>}
                </Popup>
            );
        }
        return null;
    }
    onStaticObjects3DModelChosen = () => {
        this.setState({ isBin3DModelOr3DExtr: "NATIVE_3D_MODEL" })
        this.setState({ isStaticObjectsBin: false })
    }
    onStaticObjects3DExtrChosen = () => {
        this.setState({ isBin3DModelOr3DExtr: "NATIVE_VECTOR_3D_EXTRUSION" })
        this.setState({ isStaticObjectsBin: false })
    }
    getPopupBody() {
        let popupToOpen = this.state.popupToOpen;
        let node = popupToOpen.node;

        if (popupTypes.EDIT == popupToOpen.type) {
            node = { ...node, drawPriority: 0 }
        }
        switch (popupToOpen.type) {
            case popupTypes.SHOW_DROPZONE:
                this.setState({ isStaticObjectsBin: false });
                this.setState({ isBin3DModelOr3DExtr: "" });
                return (
                    <Dropzone
                        layerType={this.state.popupToOpen.layerType}
                        onFolderSelected={this.onFolderSelected}
                    // onError={this.handleImportFormError}
                    />
                )

            case popupTypes.ABOUT:
                return (
                    <About
                        detailsAbout={this.state.detailsAbout}
                    />
                )

            case popupTypes.EDIT:
                return (
                    <EditForm
                        groupsTree={this.state.layerGroupsTree}
                        layer={node}
                        setParentHook={getChildState =>
                            (this.editFormGetState = getChildState)
                        }
                        isChanged={() =>
                            this.setState({ isNameChanged: true })
                        }
                        checkShp={this.state?.popupToOpen?.node?.ClientMetadata?.McSubLayerName}
                    />
                );

            case popupTypes.EXPORT:
                return <ExportForm
                    setParentHook={getChildState =>
                        (this.exportFormGetState = getChildState)
                    }
                    node={node}
                    nodeLevel={popupToOpen.nodeLevel}
                    findLayersNodesOfMultiBacklog={this.findLayersNodesOfMultiBacklog}
                />;

            case popupTypes.SERVER_CONFIG:
                return (
                    <ServerConfigurationForm
                        repositoryData={this.state.repositoryData}
                        setParentHook={getChildState =>
                            (this.setScleFactorValue = getChildState)
                        }
                        onSSL={this.getSSLPopup}
                        onTilingScheme={this.getTilingSchemePopup}
                        onEditConfig={() => { this.setState({ generalErrorMessage: "Changing the server configuration might cause it to be activated misproperly. Continue?" }) }}
                    />
                );
            case popupTypes.PREVIEW_CONFIG:
                return (
                    <PreviewConfigurationForm
                        repositoryData={this.state.repositoryData}
                        setParentHook={getChildState =>
                            (this.setPreviewScaleFactorValue = getChildState)
                        }
                    />
                );

            case popupTypes.REMOVE:
                return (
                    <RemoveForm
                        changeRemoveCheckBox={(isCheck) => { this.state.isCheck = isCheck }}
                        selectedlayers={this.contextMenu.current.context.selectedLayers}
                        backlogGroupNodes={this.contextMenu.current.props.groupsTree[0].childNodes}
                        node={this.isOnlyOneGroupOrLayerSelected() ? node : [this.selectedGroupsNodes, this.selectedLayersNodes]}
                        nodeLevel={popupToOpen.nodeLevel}
                        nodeName={this.nodeNameToRemove(popupToOpen.nodeLevel, node)}
                        groupsTree={this.contextMenu.current.props.groupsTree}
                        layersToRemove={this.listLayersToRemove(popupToOpen.nodeLevel, popupToOpen.node)}
                        setParentHook={getChildState =>
                            (this.getExtrusionsBasedOnDtms = getChildState)
                        }
                        setParentSecondRemove={() => { this.isSecondRemovePopup = !this.isSecondRemovePopup }}
                        isSecondRemove={this.isSecondRemovePopup}
                        isDtmWithoutAllExtrAndSilentImportRemove={this.isDtmWithoutAllExtrAndSilentImportBackRemove(this.listLayersToRemove(popupToOpen.nodeLevel, popupToOpen.node))}
                    />
                );

            case popupTypes.RENAME:
                return (
                    <AddRenameGroupForm
                        oldGroupName={node.title}
                        updateNewName={newGroupName => {
                            let diff = false
                            let popupToOpen = this.state.popupToOpen;
                            popupToOpen.newGroupName = newGroupName;
                            if (popupToOpen.node.title == newGroupName || !newGroupName) {
                                diff = true;
                            } else {
                                diff = false;
                            }
                            this.state.layerGroupsTree.map(group =>
                                ((group.title?.toLowerCase() === newGroupName.toLowerCase()) ? diff = true : '')
                            )
                            diff
                                ? this.setState({ isNameChanged: false })
                                : this.setState({ isNameChanged: true });
                            this.setState({ popupToOpen });
                        }}
                    />
                );

            case popupTypes.ADD:
                if (popupToOpen.nodeLevel === config.nodesLevel.repository && popupToOpen.selectedFiles) {
                    return (<ImportForm
                        layerUploading={this.layerUploading}
                        fileList={popupToOpen.selectedFiles}
                        selectedImportLayer={popupToOpen.layerType}
                        selectedGroup={popupToOpen.node.title}
                        groupsTree={this.state.layerGroupsTree}
                        setParentHook={getChildState =>
                            (this.importFormGetState = getChildState)
                        }
                        advancedData={this.state.advancedParams}
                        onError={this.handleImportFormError}
                        onErrorCancel={this.closePopupErr}
                    />)
                }
                else if (popupToOpen.nodeLevel === config.nodesLevel.repository) {
                    return (
                        <AddRenameGroupForm
                            updateNewName={newGroupName => {
                                let diff = false
                                let popupToOpen = this.state.popupToOpen;
                                if (!newGroupName) {
                                    diff = true;
                                }
                                popupToOpen.node.map(group =>
                                    ((group.title.toLowerCase() === newGroupName.toLowerCase()) ? diff = true : '')
                                )
                                diff ? this.setState({ isNameChanged: false }) : this.setState({ isNameChanged: true });
                                diff = false;
                                popupToOpen.newGroupName = newGroupName;
                                this.setState({ popupToOpen });
                            }}
                        />
                    );
                } else {

                    return popupToOpen.selectedFiles ? (
                        <ImportForm
                            setLayerTypeHook={getChildState =>
                                (this.setState({ NativeUploadLayerType: getChildState }))
                            }
                            setIsStaticObjectsBin={isStaticObjectsBin =>
                                (this.setState({ isStaticObjectsBin }))
                            }
                            layerUploading={this.layerUploading}
                            fileList={popupToOpen.selectedFiles}
                            selectedImportLayer={popupToOpen.layerType}
                            selectedGroup={popupToOpen.node.title}
                            groupsTree={this.state.layerGroupsTree}
                            setParentHook={getChildState =>
                                (this.importFormGetState = getChildState)
                            }
                            advancedData={this.state.advancedParams}
                            onError={this.handleImportFormError}
                            onErrorCancel={this.closePopupErr}
                            popupToOpenForStaticObj={this.state.popupToOpen}
                            setPopupToOpenForStaticObj={popupToOpenTmpStaticObj =>
                                (this.setState({ popupToOpenTmpStaticObj }))
                            }
                            isBin3DModelOr3DExtrusion={this.state.isBin3DModelOr3DExtr}
                        />
                    ) : null;
                }
            case popupTypes.ADVANCED:
                return (
                    <AdvancedForm
                        // onError={this.handleImportFormError}
                        // selectedImportLayer={popupToOpen.layerType}
                        setParentHook={getChildState =>
                            (this.advancedFormGetState = getChildState)
                        }
                        isChanged={() =>
                            this.setState({ isNameChanged: true })
                        }
                        setFilledFields={filledFields =>
                            (this.filledAdvancedFields = filledFields)
                        }
                        filledFields={this.filledAdvancedFields}
                    />
                )
            case popupTypes.SSL_INFO:
                return (
                    <SSLForm
                        // onError={this.handleImportFormError}
                        setParentHook={getChildState =>
                            (this.SSLFormGetState = getChildState)
                        }
                        isValid={this.isValid}
                    />
                )
            case popupTypes.TILING_SCHEME:
                return (
                    <TilingSchemeForm
                        // onError={this.handleImportFormError}
                        setParentHook={getChildState =>
                            (this.tilingSchemeFormGetState = getChildState)
                        }

                        onTilingSchemeTable={this.getTilingSchemeTablePopup}

                    />
                )
            case popupTypes.TILING_SCHEME_TABLE:
                return (
                    <TilingSchemeTableForm
                        // onError={this.handleImportFormError}
                        setParentHook={getChildState =>
                            (this.tilingSchemeTableFormGetState = getChildState)
                        }
                        isValid={this.isValid}
                    />
                )
            case popupTypes.MOVE_TO_GROUP:
                return <MoveGroupForm
                    groupsTree={this.state.layerGroupsTree}
                    layer={node}
                    setParentHook={getChildState =>
                        (this.editFormGetState = getChildState)
                    }
                    findLayersNodesOfMultiBacklog={this.findLayersNodesOfMultiBacklog}
                    selctedGroupsLayersNodesArr={[this.selectedGroupsNodes, this.selectedLayersNodes]}
                    isNotEmptyAndChanged={(bool) => {
                        this.setState({ isNameChanged: bool })
                    }
                    }
                />
            case popupTypes.VECTOR_INDEXING:
                return <VectorIndexingForm
                    setParentHook={getChildState =>
                        (this.getFieldsToIndex = getChildState)
                    }
                    indexedFields={popupToOpen.indexedFields}
                    allFields={popupToOpen.allFields}
                    layerId={popupToOpen.node.LayerId}
                />
            case popupTypes.NATIVE_LAYER_UPLOAD_INSTEAD_RAW:
                return <div>
                    The selected data folder seems to be a Native Layer folder. Do you want to continue?
                </div>
            default:
        }
    }

    isOnlyOneGroupOrLayerSelected() {
        if ((this.selectedGroupsKeys.length == 0 && this.selectedLayersKeys.length == 0) || (this.selectedGroupsKeys.length == 1 && this.selectedLayersKeys.length == 0)
            // || (this.selectedLayersKeys.length == 1 && this.selectedGroupsKeys.length == 0 )
            || (this.selectedGroupsKeys.length == 0 && this.areLayersFromSameGroup(this.selectedLayersNodes))) {
            return true;
        }
        return false;
    }

    areLayersFromSameGroup(layersNodes) {
        let splitedGroups = layersNodes[0].Group.split(",");
        let flag = false;
        for (let i = 0; i < layersNodes.length; i++) {
            let g = splitedGroups[i]
            for (let j = 0; j < layersNodes.length; j++) {
                if (layersNodes[j].Group.includes(`,${g}`) || layersNodes[j].Group.includes(`${g},`) || layersNodes[j].Group.includes(`,${g},`) || layersNodes[j].Group.includes(`${g}`)) {
                    flag = true;
                }
                else {
                    flag = false;
                }
            }
            if (flag) {
                return true;
            }
        }
        return false;
    }
    handleImportFormError = errorMsg => {
        this.closePopup();
        this.setState({ generalErrorMessage: errorMsg, headerGeneralErrorMessage: "Invalid Data" });
    };
    onFolderSelected = files => {
        if (files.length > 0) {
            if (this.state.popupToOpen.layerType != 'Native Layer' && this.checkNative(files)) {
                this.setState({ uploadRawHeader: this.state.popupToOpen.name })
                this.setState({
                    popupToOpen: {
                        ...this.state.popupToOpen,
                        type: popupTypes.NATIVE_LAYER_UPLOAD_INSTEAD_RAW,
                        name: popupTypes.NATIVE_LAYER_UPLOAD_INSTEAD_RAW,
                        selectedFiles: [...files]
                    }
                });
            }
            else {
                this.setState({
                    popupToOpen: {
                        ...this.state.popupToOpen,
                        type: popupTypes.ADD,
                        selectedFiles: [...files]
                    }
                });
            }
        }
    };
    nodeNameToRemove(nodeLevel, node) {
        switch (nodeLevel) {
            case config.nodesLevel.repository:
                return this.context.dict.repository;
            case config.nodesLevel.group:
                return node.title;
            case config.nodesLevel.layer:
                return node.LayerId;
            default:
                return '';
        }
    }
    closePopupErr = () => {
        return (
            <>
                <Popup
                    buttonOk='OK'
                    header={this.context.dict.applicationError}
                    hideXButton
                    onOk={this.closeErrorDTMPopup}
                >
                    <Error errorMsg='No DTMS are found, A DTM must be coupled with the layer.' />
                </Popup>

            </>
        )
    };
    closeErrorDTMPopup = () => {
        this.setState({
            generalErrorMessage: false
        });
        this.closePopup()
    };
    getPopupOkButtonLabel() {
        const popupToOpen = this.state.popupToOpen;

        if (popupToOpen.isSaving) {
            return 'Saving...';
        }

        switch (popupToOpen.type) {
            case popupTypes.EXPORT:
                return 'Export';
            case popupTypes.VECTOR_INDEXING:
                return 'Build'
            case popupTypes.SHOW_DROPZONE:
                return null;
            default:
                return this.context.dict.ok;
        }
    }
    getPopupBodyOverflow() {
        if (this.state.popupToOpen.type === popupTypes.MOVE_TO_GROUP) {
            return true;
        }
        return false;
    }
    getCancelButton() {
        if (this.state.popupToOpen.type == "about") {
            return null;
        }
        return this.context.dict.cancel;
    }
    getAdvanced() {
        const native = this.checkNative();
        if ((['Raster', "Vector", "S-57"].includes(this.state.popupToOpen.layerType) || ["RAW_RASTER"].includes(this.state.popupToOpen.node?.LayerType)) && (this.state.popupToOpen.type == "Add" || this.state.popupToOpen.type == "Edit"))//&& !native
            // if ((['Vector', 'Raster'].includes(this.state.popupToOpen.layerType) || ['NATIVE_VECTOR', "RAW_RASTER"].includes(this.state.popupToOpen.node.LayerType)) && (this.state.popupToOpen.type == "Add" || this.state.popupToOpen.type == "Edit") && !native)
            return this.context.dict.advanced;
        else
            return null;
    }
    checkNative = (filesParam = false) => {
        const files = filesParam ? filesParam : this.state?.popupToOpen?.selectedFiles;
        let native = false;
        for (let i = 0; i < files?.length; i++) {
            const upperCaseName = files[i]?.name.toUpperCase();
            if (upperCaseName.endsWith('.BIN') && ['MATERIAL', 'TRAVERSABILITY', '3DMODEL', 'DTM', 'RASTER', 'VECTOR', 'VECTOR3DEXTRUSION', 'STATICOBJECTS'].includes(upperCaseName.split('.BIN')[0])) {
                // if (upperCaseName.endsWith('.BIN')) {
                native = true;
                break;
            }
        }
        return native;
    }
    getAdvancedPopup = () => {
        if (this.state.popupToOpen.type == "Edit") {
            this.editFormGetState();
        } else {
            this.importFormGetState();
        }
        let label = '';
        if (this.state.popupToOpen.layerType) {
            label = `${this.state.popupToOpen.layerType} Advanced`;
        }
        else {
            label = this.state.popupToOpen.node.LayerType.toUpperCase().includes('RASTER') ? `Raster Advanced` : 'Vector Advanced';
        }
        this.setState({
            popupToOpen: {
                ...this.state.popupToOpen,
                type: popupTypes.ADVANCED,
                name: label
            }
        });
    };
    getSSLPopup = () => {
        // this.importFormGetState();
        this.setState({
            popupToOpen: {
                ...this.state.popupToOpen,
                type: popupTypes.SSL_INFO,
                name: 'SSL Info'
            }
        });
    };
    getTilingSchemePopup = () => {
        this.setState({
            popupToOpen: {
                ...this.state.popupToOpen,
                type: popupTypes.TILING_SCHEME,
                name: 'Tiling Scheme'
            }
        });
    }
    getTilingSchemeTablePopup = () => {
        this.setState({
            popupToOpen: {
                ...this.state.popupToOpen,
                type: popupTypes.TILING_SCHEME_TABLE,
                name: 'Tiling Scheme Table'
            }
        });
    }

    getPopupSize() {
        switch (this.state.popupToOpen.type) {
            case popupTypes.ADD:
                return this.state.popupToOpen.nodeLevel === config.nodesLevel.repository ? popupSize.small : null;
            case popupTypes.RENAME:
            case popupTypes.MOVE_TO_GROUP:
            case popupTypes.EXPORT:
            case popupTypes.VECTOR_INDEXING:
            case popupTypes.PREVIEW_CONFIG:
                return popupSize.small;
            case popupTypes.ADVANCED:
                return this.state.popupToOpen.layerType === "Vector" ? popupSize.small : null;
            default:
                return null;
        }
    }
    openPopup = (type, name, nodeLevel, node, layerType) => {
        if (type === popupTypes.ADD) {
            if (nodeLevel === config.nodesLevel.group) {
                // only in case we do something before opening the popup                
                name = this.context.dict.add + ' ' + this.context.dict.layer + ' - ' + layerType;
            }
        } else if (type === popupTypes.EXPORT && nodeLevel === config.nodesLevel.group && (node.length == 1 || !node.length)) {
            name = "Export Group";
        } else if (type === popupTypes.EXPORT && nodeLevel === config.nodesLevel.group) {
            name = "Export Groups";
        } else if (type === popupTypes.EXPORT && name === "Group and Layer" && node[0].length > 1 && node[1].length > 1) {
            name = "Export Groups And Layers";
        } else if (type === popupTypes.EXPORT && name === "Group and Layer" && node[0].length > 1 && node[1].length == 1) {
            name = "Export Groups And Layer";
        } else if (type === popupTypes.EXPORT && name === "Group and Layer" && node[0].length == 1 && node[1].length > 1) {
            name = "Export Group And Layers";
        } else if (type === popupTypes.EXPORT && name === "Export Layer" && node.LayerId) {
            name = "Export Layers";
        } else if (type === popupTypes.EDIT && nodeLevel === config.nodesLevel.layer) {
            name = `${this.context.dict.edit} ${this.context.dict.layer} - Raw ${node.LayerType == "RAW_VECTOR_3D_EXTRUSION" || node.LayerType == "NATIVE_VECTOR_3D_EXTRUSION" ? 'Vector 3D Extrusion' : layerTypesStrings[node.LayerType]}`
        } else if (name == "Import MC Package") {
            layerType = "MC Package";
        }
        else if (name == "Export Repository") {
            node = node.filter(group => group.title != config.LAYERS_BACKLOGS_TITLE);
        }
        this.setState({ NativeUploadLayerType: "" })
        this.setState({
            popupToOpen: { type, name, nodeLevel, node, layerType }
        });
    };
    closePopup = () => {
        switch (this.state.popupToOpen?.type) {
            case popupTypes.ADD:
                this.setState({ isNameChanged: false })
                this.clearData();
                this.filledAdvancedFields = null;
                this.setState({ popupToOpen: null });
                break;
            case popupTypes.EDIT:
                this.setState({ isNameChanged: false })
                this.clearData();
                this.filledAdvancedFields = null;
                this.setState({ popupToOpen: null });
                break;
            case popupTypes.ADVANCED:
                const popupType = this.context.getAdvancedPopupType();
                let typeLabel = this.state.popupToOpen.name.split('Advanced')[0];
                if (popupType == 'add') {
                    this.setState({
                        popupToOpen: {
                            ...this.state.popupToOpen,
                            type: popupTypes.ADD,
                            name: 'Add Layer'
                        }
                    });
                } else {
                    this.setState({
                        popupToOpen: {
                            ...this.state.popupToOpen,
                            type: popupTypes.EDIT,
                            name: `Edit Layer - Raw ${typeLabel}`
                        }
                    });
                }
                break;
            case popupTypes.TILING_SCHEME:
                this.context.cancelTmpTableData()
                this.setState({
                    popupToOpen: {
                        ...this.state.popupToOpen,
                        name: this.context.dict.serverConfiguration,
                        type: popupTypes.SERVER_CONFIG,
                    }
                });
                break;
            case popupTypes.TILING_SCHEME_TABLE:
                this.setState({
                    popupToOpen: {
                        ...this.state.popupToOpen,
                        type: popupTypes.TILING_SCHEME,
                        name: 'Tiling Scheme'
                    }
                });
                break;
            case popupTypes.SSL_INFO:
                this.setState({
                    popupToOpen: {
                        ...this.state.popupToOpen,
                        name: this.context.dict.serverConfiguration,
                        type: popupTypes.SERVER_CONFIG,
                    }
                });
                break;
            default:
                this.setState({ popupToOpen: null });
                break;

        }
    }

    clearData = () => {
        this.context.setData(null);
        this.context.setAdvancedData(null);
        this.context.setAdvancedDataType(null);
    }
    //#region getPopupFunction 

    getPopupFunction() {
        let popupToOpen = this.state.popupToOpen;
        switch (popupToOpen.type) {
            case popupTypes.REMOVE:
                return !this.isSecondRemovePopup && this.isDtmWithoutAllExtrAndSilentImportBackRemove(this.listLayersToRemove(popupToOpen.nodeLevel, popupToOpen.node)) ?
                    this.openSecondDtmRemovePopup : this.callRemoveLayers;
            case popupTypes.ADVANCED:
                return this.callAdvanced;
            case popupTypes.SSL_INFO:
                return this.callSSLInfo;
            case popupTypes.TILING_SCHEME:
                return this.callTilingScheme;
            case popupTypes.TILING_SCHEME_TABLE:
                return this.callTilingSchemeTable;
            case popupTypes.RENAME:
                return this.callRenameGroup;
            case popupTypes.ADD:
                if (popupToOpen.nodeLevel === config.nodesLevel.repository && popupToOpen.selectedFiles) {
                    return this.callAddNewLayer;
                }
                else {
                    return popupToOpen.nodeLevel === config.nodesLevel.repository
                        ? this.callAddNewGroup
                        : this.callAddNewLayer;
                }
            case popupTypes.EDIT:
                return this.callEditLayer;
            case popupTypes.EXPORT:
                return this.callExportLayer;
            case popupTypes.MOVE_TO_GROUP:
                return popupToOpen.node.Group === config.BACKLOG_PREFIX
                    ? this.callEditLayer
                    : this.callMoveToGroupLayer;
            case popupTypes.VECTOR_INDEXING:
                return this.callVectorIndexing;
            case popupTypes.SERVER_CONFIG:
                return this.callServerConfig;
            case popupTypes.PREVIEW_CONFIG:
                return this.callPreviewConfig;
            case popupTypes.NATIVE_LAYER_UPLOAD_INSTEAD_RAW:
                return this.callSetPopupAdd;
            default:
                return this.closePopup;
        }
    }
    callSetPopupAdd = () => {
        this.setState({
            popupToOpen: {
                ...this.state.popupToOpen,
                type: popupTypes.ADD,
                name: this.state.uploadRawHeader,
            }
        });
    }
    callPreviewConfig = () => {
        const previewConfigData = this.setPreviewScaleFactorValue();
        this.context.setMapScaleFactorValue(previewConfigData.MapScaleFactor)
        this.closePopup();
    }
    callServerConfig = async () => {
        const configData = this.setScleFactorValue();
        this.context.setReadOnly(false);
        this.context.setTilingSchemeChanged(false);
        this.context.setSSLChanged(false);
        this.context.setConfigData(null);
        try {
            const response = await axios.post(config.urls.updateConfig, configData, { headers: { "X-CALLING-APP": "management" } });
            // const response = await axios.post(config.urls.updateConfig, { configData }, { headers: { "X-CALLING-APP": "management" } });
            if (response.data) {
                this.setState({ generalErrorMessage: response.data["user-message"], headerGeneralErrorMessage: "User Massage" },
                    this.getGeneralErrorPopup())
            }
        } catch (e) {
            this.closePopup();

        }
        if (configData) {
            localStorage.setItem('MapScaleFactor', configData.MapScaleFactor);
            this.context.setMapScaleFactorValue(configData.MapScaleFactor);
            this.closePopup();
        }
    }
    callTilingSchemeTable = () => {
        this.tilingSchemeTableFormGetState()
        if (valid)
            this.setState({
                popupToOpen: {
                    ...this.state.popupToOpen,
                    type: popupTypes.TILING_SCHEME,
                    name: 'Tiling Scheme'
                }
            });
    }
    callTilingScheme = () => {
        this.tilingSchemeFormGetState();
        this.context.cleanTableData();
        // this.context.setReadOnly(false);
        this.context.setTilingScheme();
        this.context.setTilingSchemeChanged(true);
        this.setState({
            popupToOpen: {
                ...this.state.popupToOpen,
                name: this.context.dict.serverConfiguration,
                type: popupTypes.SERVER_CONFIG,
            }
        });
    }
    openSecondDtmRemovePopup = () => {
        let { type, name, nodeLevel, node, layerType } = this.state.popupToOpen;
        this.closePopup();
        this.setState({
            popupToOpen: { type, name, nodeLevel, node, layerType }
        });
    }
    callRemoveLayers = () => {
        const { popupToOpen } = this.state;
        // Unsubscribe the WebSocket to prevent unnecessary repository refresh
        // if (_socket != null && window.sessionStorage.getItem('sessionId') != null) {
        //     this.stopKeepAlive();
        //     _socket.send("unsubscribe " + window.sessionStorage.getItem('sessionId'));
        // }
        let isIncludeBasisDtms = false;
        let storedGroupNames = JSON.parse(localStorage.getItem('newGroups')) || [];
        // if (!popupToOpen.node.length) {
        let isRemoveSucceed = true;
        let uniqueLayerIdNodesToRemove = [];
        if (popupToOpen.nodeLevel === config.nodesLevel.group && storedGroupNames.includes(popupToOpen.node.title)) {//one group remove
            storedGroupNames = storedGroupNames.filter(group => group !== popupToOpen.node.title);
            localStorage.setItem('newGroups', JSON.stringify(storedGroupNames));
            this.repositoryTree.current.refreshRepositoryTree();
        } else {//layers remove
            uniqueLayerIdNodesToRemove = this.listLayersToRemove(popupToOpen.nodeLevel, popupToOpen.node);
            if (this.getExtrusionsBasedOnDtms?.length > 0) {
                isIncludeBasisDtms = true;
            }
            uniqueLayerIdNodesToRemove.forEach(async layer => {
                const layerGroups = layer?.Group.split(',');
                try {
                    if ((layerGroups.length === 1 && layerGroups[0] === config.BACKLOG_PREFIX) || layer.deleteExtrusionFromBacklog) {
                        // remove permanently
                        const response = await axios.post(config.urls.removeLayer, {
                            layerId: layer?.LayerId,
                            deleteFromDisk: this.state.isCheck ? "true" : "false",
                            DeleteDependencies: isIncludeBasisDtms ? "true" : "false"
                        });

                        if (response.data && response.data.MapLayerConfig && response.data.MapLayerConfig.length >= 0) {
                            this.mapTreeRepoSetLayers(response.data);
                            // if (layer.LayerType.includes('DTM'))
                            // this.context.refreshDTMLayers();
                        }
                    } else {
                        const data = {
                            layerId: layer?.LayerId,
                            title: layer?.Title,
                            drawPriority: layer?.DrawPriority || '',
                        };

                        if (layer?.LayerType === 'RAW_VECTOR') {
                            data.dataSource = layer?.RawLayerInfo.Vector.DataSource;
                            data.textureFile = layer?.RawLayerInfo.Vector.PointTextureFile;
                            data.minScale = layer?.RawLayerInfo.Vector.MinScale || '';
                            data.maxScale = layer?.RawLayerInfo.Vector.MaxScale || '';
                            if (layer.RawLayerInfo.Vector.SourceCoordinateSystem && layer.RawLayerInfo.Vector.SourceCoordinateSystem.Code) {
                                data.sourceEpsg = layer.RawLayerInfo.Vector.SourceCoordinateSystem.Code;
                            }
                        }

                        if (layer?.LayerType === 'RAW_VECTOR_3D_EXTRUSION') {
                            data.dataSource = layer.RawLayerInfo.Vector3DExt.DataSource;
                            data.dtmLayerId = layer.RawLayerInfo.Vector3DExt.DtmLayerId;
                            data.heightColumn = layer.RawLayerInfo.Vector3DExt.HeightColumn;
                            data.sideTexture = layer.RawLayerInfo.Vector3DExt.SideDefaultTexture?.TextureFile;//to check the ? if working good
                            data.roofTexture = layer.RawLayerInfo.Vector3DExt.RoofDefaultTexture?.TextureFile;//to check the ? if working good
                        }
                        data.group = config.BACKLOG_PREFIX;
                        const formData = new FormData();
                        Object.keys(data).forEach(key => {
                            formData.append(key, data[key]);
                        });
                        this.setState({ popupToOpen: { ...this.state.popupToOpen, isSaving: true } })
                        const response = data.group == config.BACKLOG_PREFIX ?
                            await axios.post(config.urls.backlogOps, { layerId: layer?.LayerId, operation: "SET" }) :
                            await axios.post(config.urls.editLayerInfo, formData);
                        if (response.data && response.data.MapLayerConfig && response.data.MapLayerConfig.length > 0) {
                            this.mapTreeRepoSetLayers(response.data);
                            // if (layer.LayerType.includes('DTM'))
                            // this.context.refreshDTMLayers();
                        }
                    }
                    this.state.isCheck = false;
                }
                catch (e) {
                    isRemoveSucceed = false;
                    this.closePopup();
                    this.setState({
                        generalErrorMessage:
                            (e.response &&
                                e.response.data &&
                                e.response.data.errorMessage) ||
                            'An error occurred while trying to perform the action.'
                    });
                }
            });
        }
        let isThereLayerToRemoveThatPreviewed = this.isThereLayerToRemoveThatPreviewed(uniqueLayerIdNodesToRemove);
        if (isRemoveSucceed && isThereLayerToRemoveThatPreviewed) {
            this.setState({ layersToPreview: [] });
            this.isLayerRemoveFromPreviewedLayers = true;
            this.context.closeMapPreview();
        }
        this.closePopup();
        // subscribe back to enable 
        // if (_socket != null && window.sessionStorage.getItem('sessionId') != null) {
        //     _socket.send("subscribe " + window.sessionStorage.getItem('sessionId'));
        //     this.startKeepAlive();
        // }
        // this.repositoryTree.current.refreshRepositoryTree();
    };
    isThereLayerToRemoveThatPreviewed(LayersToRemove) {
        let flag = false;
        LayersToRemove.forEach((layer, i) => {
            let founded = this.state.layersToPreview.find(l => l.LayerId == layer.LayerId);
            if (founded) {
                flag = true;
            }
        })
        return flag;
    }
    isExtrExistInLayersToRemove = (layer3DExtr, layersToRemove) => {
        let layer = layersToRemove.find(layer => layer.LayerId === layer3DExtr.LayerId);
        if (layer?.LayerId == layer3DExtr.LayerId) {
            return true;
        }
        return false;
    }

    getExtrusionsBasedOnSpesificArr(dtmLayerId, layersToRemove) {//and put then in state arr
        let ExtrusionsBasedOnSpesificArr = [];
        let flatGroupsTree = this.state.layerGroupsTree.map(group => group.childNodes).flat();
        flatGroupsTree.map(layer => {
            if (layer.LayerType == 'RAW_VECTOR_3D_EXTRUSION' && layer.RawLayerInfo?.Vector3DExt?.DtmLayerId == dtmLayerId && !this.isExtrExistInLayersToRemove(layer, layersToRemove)) {
                ExtrusionsBasedOnSpesificArr.push({ ...layer, deleteExtrusionFromBacklog: true });
            }
        });
        return ExtrusionsBasedOnSpesificArr;
    }
    isDtmWithoutAllExtrAndSilentImportBackRemove(layersToRemove) {
        let numOfDtmsWithExtrBasedOn = false;
        let isHasSilent = false;
        // let extrusionBasedOnDtmsArr = [];
        layersToRemove.map((layer) => {
            if (layer?.LayerType?.includes('DTM')) {
                let tmpExtrArr = this.getExtrusionsBasedOnSpesificArr(layer.LayerId, layersToRemove);
                if (tmpExtrArr.length !== 0) {
                    // extrusionBasedOnDtmsArr = [...extrusionBasedOnDtmsArr, ...tmpExtrArr];
                    numOfDtmsWithExtrBasedOn = numOfDtmsWithExtrBasedOn ? numOfDtmsWithExtrBasedOn + 1 : 1;
                }
            }
            if (layer?.LayerImportType == "SILENTLY_IMPORTED" && layer?.Group == config.BACKLOG_PREFIX) {
                isHasSilent = true;
            }
        });
        // props.setParentHook(extrusionBasedOnDtmsArr);
        return numOfDtmsWithExtrBasedOn && isHasSilent;
    }
    listLayersOfGroup(node) {
        let layersToRemove = [];
        node.childNodes.forEach(layer => {
            layersToRemove.push(layer);
        });
        return layersToRemove;
    }
    listLayersOfSomeGroups(node) {
        let layersToRemove = [];
        node[0].forEach((group) => {
            this.listLayersOfGroup(group).forEach(layer => layersToRemove.push(layer));
        });
        return layersToRemove;
    }
    listLayersToRemove(nodeLevel, node) {
        let layersToRemove = [];
        if (!node.length) {
            switch (nodeLevel) {
                case config.nodesLevel.repository:
                    node.forEach(group => {
                        group.childNodes.forEach(layer => {
                            layersToRemove.push(layer);
                        });
                    });
                    break;
                case config.nodesLevel.group:
                    layersToRemove = this.listLayersOfGroup(node);
                    break;
                case config.nodesLevel.layer:
                    if (this.context.selectedLayers.length > 0) {
                        this.context.selectedLayers.forEach(key => {
                            const [groupName, layerId] = key.split(config.selectedLayerDelimiter);
                            const groupNode = this.state.layerGroupsTree.find(g => g.title === groupName);
                            const layer = groupNode.childNodes.find(layer => layer.LayerId === layerId);
                            if (layer) {
                                layersToRemove.push(layer);
                            }
                        })
                        // this.context.clearSelectedLayers();
                    }
                    else {
                        layersToRemove.push(node);
                    }
                    break;

                default:
                    break;
            }
        }
        else {
            if (node[1].length == 0) {//some groups
                layersToRemove = this.listLayersOfSomeGroups(node);
            }
            else if (node[0].length !== 0 && node[1].length !== 0) {//group/s and layer/s
                layersToRemove = this.listLayersOfSomeGroups(node);
                node[1].forEach(layer => layersToRemove.push(layer));
            }
            else {
                node[1].forEach(layer => layersToRemove.push(layer));
            }
        }
        let uniqueLayersToRemove = this.filterToUniqueSelectedLayers(layersToRemove);
        return uniqueLayersToRemove;
    }

    callAddNewGroup = () => {
        this.setState({ isNameChanged: false });
        let storedGroupNames = JSON.parse(localStorage.getItem('newGroups')) || [];
        storedGroupNames.push(this.state.popupToOpen.newGroupName);
        localStorage.setItem('newGroups', JSON.stringify(storedGroupNames));
        this.closePopup();
        this.repositoryTree.current.refreshRepositoryTree();
    };
    callRenameGroup = async () => {
        let oldGroupName = this.state.popupToOpen.node.title;
        let newGroupName = this.state.popupToOpen.newGroupName;
        let storedGroupNames = JSON.parse(localStorage.getItem('newGroups')) || [];

        if (storedGroupNames.includes(oldGroupName)) {
            storedGroupNames = storedGroupNames.filter(group => group !== oldGroupName);
            storedGroupNames.push(newGroupName);
            localStorage.setItem('newGroups', JSON.stringify(storedGroupNames));
        } else {
            try {
                const response = await axios.post(config.urls.renameGroup, { oldGroupName, newGroupName });

                if (
                    response.data &&
                    response.data.MapLayerConfig &&
                    response.data.MapLayerConfig.length >= 0) {
                    this.mapTreeRepoSetLayers(response.data);
                }
            } catch (e) {
                this.closePopup();
                this.setState({
                    generalErrorMessage:
                        (e.response &&
                            e.response.data &&
                            e.response.data.errorMessage) ||
                        'An error occurred while trying to perform the action.'
                });
            }
        }
        this.setState({ isNameChanged: false });
        this.closePopup();
        this.repositoryTree.current.refreshRepositoryTree();
    };
    callAdvanced = () => {
        const popupType = this.context.getAdvancedPopupType();
        let isValid = this.advancedFormGetState ? this.advancedFormGetState() : true;
        if (isValid) {
            let typeLabel = this.state.popupToOpen.name.split('Advanced')[0];
            if (popupType == 'add') {
                this.setState({
                    popupToOpen: {
                        ...this.state.popupToOpen,
                        type: popupTypes.ADD,
                        name: 'Add Layer'
                    }
                });
            } else {
                this.setState({
                    popupToOpen: {
                        ...this.state.popupToOpen,
                        type: popupTypes.EDIT,
                        name: `Edit Layer - Raw ${typeLabel}`
                    }
                });
            }
        }
    }
    callSSLInfo = () => {
        this.SSLFormGetState()
        this.context.setSSLChanged(true)
        if (valid) {
            this.setState({
                popupToOpen: {
                    ...this.state.popupToOpen,
                    name: this.context.dict.serverConfiguration,
                    type: popupTypes.SERVER_CONFIG,
                }
            });
        }
    }
    callAddNewLayer = async () => {
        if (this.advancedFormGetState) {
            this.advancedFormGetState();
        }
        this.filledAdvancedFields = null;
        let layersParams = this.importFormGetState();
        const keyForUploadedLayer = parseInt(Math.random() * 1000000);
        const selectedFiles = this.state.popupToOpen.selectedFiles;
        if (layersParams) {
            this.setState(
                {
                    popupToOpen: null,
                    currentlyUploadedFiles: this.state.currentlyUploadedFiles.concat(
                        selectedFiles.map((f, i) => ({
                            keyForUploadedLayer,
                            fileObj: f,
                            fileName: f.webkitRelativePath || f.fileFullPath || f.name,
                            layersParams,
                            fileIndex: i,
                            totalFiles: selectedFiles.length,
                            percent: 0,
                            isShown: true
                        }))
                    )
                },
                () => {
                    this.context.closeMapPreview()
                    this.uploadFiles(layersParams)
                }
            );
            this.clearData()
        }
    };
    createDateHourString() {
        const date = new Date();
        const year = date.getFullYear();
        let month = date.getMonth() + 1;
        month = month < 10 ? `0${month}` : month;
        let day = date.getDate();
        day = day < 10 ? `0${day}` : day;
        let dateHourString = `_${year}_${month}_${day}_${date.toLocaleTimeString(navigator.language, { hour12: false })}`;
        return dateHourString;
    }
    validateExportRequestUrl(finalUrl) {
        let arrLayers = [];
        while (finalUrl.includes("&layerId=")) {
            let ind = finalUrl.indexOf("&layerId=")
            arrLayers.push(finalUrl.substring(0, ind));
            finalUrl = finalUrl.substring(ind + 9)
        }
        arrLayers.push(finalUrl);
        let filteredExportRequestLayers = this.filterToUniqueSelectedLayers(arrLayers, true)
        let fineUrl = this.OrganizeOneGroupToExport(filteredExportRequestLayers, true);
        return fineUrl;
    }
    OrganizeOneGroupToExport = (layersToExport, isOnlyLayersId = false) => {
        let layersUrl = "";
        for (let key of layersToExport) {
            let keyIn = isOnlyLayersId ? key : key.LayerId;
            layersUrl += `${keyIn}&layerId=`;
        }
        return layersUrl;
    }
    callExportLayer = async () => {
        const layersToExport = this.exportFormGetState();
        this.closePopup();
        if (layersToExport.LayerId) {//export unselected layer
            await this.exportSingleLayer(layersToExport.LayerId);
        }
        else if (layersToExport[0].LayerId) {//export selected layer/s
            let layersUrl = "";
            for await (let key of layersToExport) {
                layersUrl += `${key.LayerId}&layerId=`;
            }
            let finalLayersUrl = layersUrl.substring(0, layersUrl.length - 9);
            await this.exportSingleLayer(finalLayersUrl);
        }
        else if (!layersToExport[0].childNodes && !layersToExport[0][0]?.childNodes) {//only one group
            let layersUrl = "";
            layersUrl = this.OrganizeOneGroupToExport(layersToExport);
            let finalLayersUrl = layersUrl.substring(0, layersUrl.length - 9);
            await this.exportSingleLayer(finalLayersUrl);
        }
        else if (layersToExport[1] && layersToExport[1][0]?.LayerId) {//group and layer
            let layersUrl = "";
            for (let group of layersToExport[0]) {
                layersUrl += this.OrganizeOneGroupToExport(group.childNodes);
            }
            layersUrl += this.OrganizeOneGroupToExport(layersToExport[1]);
            let finalLayersUrl = layersUrl.substring(0, layersUrl.length - 9);
            let finalLayersUrlValidated = this.validateExportRequestUrl(finalLayersUrl);
            finalLayersUrl = finalLayersUrlValidated.substring(0, finalLayersUrlValidated.length - 9);
            await this.exportSingleLayer(finalLayersUrl, false);
        }
        else {//some groups
            let layersUrl = "";
            for (let group of layersToExport) {
                layersUrl += this.OrganizeOneGroupToExport(group.childNodes);
            }
            let finalLayersUrl = layersUrl.substring(0, layersUrl.length - 9);
            let finalLayersUrlValidated = this.validateExportRequestUrl(finalLayersUrl);
            finalLayersUrl = finalLayersUrlValidated.substring(0, finalLayersUrlValidated.length - 9);
            await this.exportSingleLayer(finalLayersUrl);
        }
    }

    async exportSingleLayer(layerId, isGroup = false) {
        let dateString = this.createDateHourString();
        dateString = dateString.replace(/:/g, "_")
        this.isExportingFile = true;
        try {
            if (isGroup) {
                this.context.showSideNotification({ text: `Preparing to export ${isGroup} group` });
            }
            else if (layerId.includes('&layerId=')) {
                this.context.showSideNotification({ text: `Preparing to export layers` });
            }
            else {
                this.context.showSideNotification({ text: `Preparing to export layer ${layerId}` });
            }

            const response = await axios.get(`${config.urls.export}${layerId}`, { timeout: 0, });
            this.context.removeAllSideNotifications()
            if (response && response.data && response.data.fileName) {
                const fileLink = document.createElement('a');
                let fileSize = 0;
                await fetch(response.data.fileName, { method: 'HEAD' })
                    .then((response) => {
                        fileSize = response.headers.get('content-length');
                    });
                //create the name of downloaded file
                let fileNameBeginStr = "";
                if (isGroup) {
                    fileNameBeginStr = `group_${isGroup}`;
                }
                else if (layerId.includes('&layerId=')) {
                    let layerIdCopy = layerId;
                    let numOfLayers = 1;
                    while (layerIdCopy.includes('&layerId=')) {
                        numOfLayers += 1;
                        let index = layerIdCopy.indexOf('&layerId=');
                        layerIdCopy = layerIdCopy.substring(index + 8, layerIdCopy.length);
                    }
                    fileNameBeginStr = `McPackage_${numOfLayers}layers_`;
                }
                else {
                    fileNameBeginStr = `${layerId}`;
                }
                //regular export start
                if (response.data.numSubFiles <= 1) {
                    fileLink.href = response.data.fileName;//"Export/Packages/material.mcpkg";
                    const fileName = `${fileNameBeginStr}${dateString}.mcpkg`;
                    fileLink.setAttribute('download', fileName); // will work only when client and server on same origin. and also not need the server to set the content-disposition attachment header
                    document.body.appendChild(fileLink);
                    fileLink.click();
                    fileLink.remove();
                }
                //udge export start
                else {
                    await this.downloadBigFileInSlices(response.data.fileName, response.data.numSubFiles, dateString, fileNameBeginStr);
                }
            }
        } catch (e) {
            this.context.removeAllSideNotifications()
            console.error('Error when trying to export ' + layerId, e);
        }
        this.isExportingFile = false;
    }
    downloadBigFileInSlices = async (url, numSubFiles, dateString, fileNameBeginStr) => {
        let fileContent = [];
        let flag = true;
        for (let index = 0; index < numSubFiles; index++) {
            try {
                const response = await fetch(`${url}.${index}`);
                const chunkData = await response.arrayBuffer();
                fileContent.push(chunkData);
            }
            catch (e) {
                console.log("Big export failed")
                alert("Big export failed")
                console.log(e)
                flag = false;
                break;
            }
        }
        if (flag) {
            const combinedBlob = new Blob(fileContent);
            const combinedUrl = URL.createObjectURL(combinedBlob);

            // Trigger the download
            const downloadLink = document.createElement('a');
            downloadLink.href = combinedUrl;
            const finalFileName = `${fileNameBeginStr}${dateString}.mcpkg`
            downloadLink.download = finalFileName // Replace with your desired file name
            downloadLink.click();
        }
    }
    async MoveToGroupLayerForOneLayer(layersParams) {
        try {
            // this.setState({ popupToOpen: { ...this.state.popupToOpen, isSaving: true } })
            const response = await axios.post(config.urls.changeLayerGroup, {
                operation: "move-layer",
                layerId: layersParams.layerId,
                group: layersParams.group
            }
            );
            if (response.data && response.data.MapLayerConfig && response.data.MapLayerConfig.length > 0) {
                this.mapTreeRepoSetLayers(response.data);
            }
            this.closePopup();
        } catch (e) {
            this.closePopup();
            this.setState({
                generalErrorMessage:
                    (e.response &&
                        e.response.data &&
                        e.response.data.errorMessage) ||
                    'An error occurred while trying to perform the action.'
            });
        }
    }
    callMoveToGroupLayer = () => {
        let layersParams = this.editFormGetState();
        if (layersParams) {
            if (this.selectedLayersNodes.length > 1) {//for multi selection - select groups
                layersParams.forEach((layer, i) => {
                    this.MoveToGroupLayerForOneLayer(layer);
                });
            }
            else {
                this.MoveToGroupLayerForOneLayer(layersParams);
            }
            this.clearData();
        }
    };
    async callEditLayerForOneLayer(layersParams) {
        const formData = new FormData();
        Object.keys(layersParams).forEach(key => {
            formData.append(key, layersParams[key]);
        });
        this.setState({ isNameChanged: false });
        try {
            this.setState({ popupToOpen: { ...this.state.popupToOpen, isSaving: true } })
            const response = await axios.post(config.urls.editLayerInfo, formData);
            if (response.data && response.data.MapLayerConfig && response.data.MapLayerConfig.length > 0) {
                this.mapTreeRepoSetLayers(response.data);
            }
            this.closePopup();
        } catch (e) {
            this.closePopup();
            this.setState({
                generalErrorMessage:
                    (e.response &&
                        e.response.data &&
                        e.response.data.errorMessage) ||
                    'An error occurred while trying to perform the action.'
            });
        }
    }
    callEditLayer = () => {
        if (this.advancedFormGetState) {
            this.advancedFormGetState();
        }
        this.filledAdvancedFields = null;
        let layersParams = this.editFormGetState();
        if (layersParams) {
            if (this.context.selectedLayers.length > 1) {//for multi selection - select groups
                layersParams.forEach((layer, i) => {
                    this.callEditLayerForOneLayer(layer);
                });
            }
            else {
                this.callEditLayerForOneLayer(layersParams);
            }
            this.clearData();
        }
    };
    callVectorIndexing = async () => {
        const { fields, layerId, isAlreadyIndexed } = this.getFieldsToIndex();
        this.closePopup();

        //if indexed - remove prev indexing
        isAlreadyIndexed && await this.vectorIndexingOps({ operation: config.indexOps.api.removeLayer, layerId });

        if (fields && fields.length > 0) {
            let response = await this.vectorIndexingOps({ operation: config.indexOps.api.addLayer, layerId, fields });
            if (response && !response.errorMessage) {
                this.checkVectorIndexingProgress(layerId);
            }
        }
    }

    checkVectorIndexingProgress = async (layerId) => {
        let response = await this.vectorIndexingOps({ operation: config.indexOps.api.getStatus, layerId });

        if (response.IndexStatus == config.indexOps.statusTypes.indexed) {
            return;
        }
        setTimeout(() => this.checkVectorIndexingProgress(layerId), 10000);
    }
    async vectorIndexingOps(indexOpsParams) {
        try {
            //this.context.showSideNotification({ text: `vector indexing: ` + indexOpsParams.operation + " for layer: " + indexOpsParams.layerId });
            const response = await axios.post(config.urls.indexOps, indexOpsParams);
            if (response && response.data) {
                return response.data;
            }
            this.context.removeAllSideNotifications();

        } catch (e) {
            this.context.showSideNotification({ text: `Error when trying to vectorIndexing: ` + indexOpsParams.operation + " for layer: " + indexOpsParams.layerId });
            console.error('Error when trying to vectorIndexing ' + indexOpsParams.layerId, e);
        }
    }
    //#endregion

    //#endregion

    //#region getGeneralErrorPopup
    getGeneralErrorPopup() {
        const { generalErrorMessage, headerGeneralErrorMessage } = this.state;
        const { errorInPreview, SetErrorInPreview } = this.props;
        const { dict, closeMapPreview } = this.context;

        if (generalErrorMessage) {
            return this.renderErrorPopup(headerGeneralErrorMessage || dict.applicationError, generalErrorMessage, this.closeErrorPopup);
        }
        if (errorInPreview != null) {
            return this.renderErrorPopup("Preview", errorInPreview, () => {
                SetErrorInPreview(null);
                closeMapPreview();
            });
        }
        return null;
    }
    renderErrorPopup(header, message, onOk) {
        return <Popup
            buttonOk="OK"
            header={header}
            hideXButton
            onOk={onOk}
        >
            <Error errorMsg={message} />
        </Popup>
    }
    closeErrorPopup = () => {
        this.setState({
            generalErrorMessage: false,
            closeErrorPopup: true,
        });

    };
    //#endregion

    //#region getActionsBar

    getActionsBar() {
        return (
            <div className={classNames.ActionsBar}>
                {this.getSearchBar()}
                {this.getPreviewActions()}
            </div>
        );
    }
    getSearchBar() {
        return <>{this.getSearchComp()}</>;
    }
    getSearchComp() {
        return (
            <label className={classNames.SearchLabel}>
                <input
                    maxLength='50'
                    className={classNames.SearchInput}
                    type='text'
                    value={this.state.searchValue}
                    onChange={(event) => this.setState({ searchValue: event.target.value })}
                    placeholder={this.context.dict.searchRepository}
                />
                <img
                    className={classNames.SearchIcon}
                    src={searchIcon}
                    alt='search'
                />
            </label>
        );
    }
    getPreviewActions() {
        if (this.context.mapToPreview) {
            return (
                <MapPreviewActions
                    onActionClick={this.handleMapPreviewActionClick}
                    onCloseClick={() => {
                        this.context.closeMapPreview();
                        this.mapContainer.current && this.mapContainer.current.closeMap();
                    }} />
            );
        }
    }
    SetActionDtmOnClick() {
        if (this.props.activeDtmMapPreview === mapActions.SHOW_DTM_MAP) {
            this.props.SetActionDtmMapPreview(false)
            this.mapContainer.current.doDtmVisualization(false);
        }
        else {
            this.props.SetActionDtmMapPreview(mapActions.SHOW_DTM_MAP)
            this.mapContainer.current.doDtmVisualization(mapActions.SHOW_DTM_MAP);
        }
    }
    handleMapPreviewActionClick = actionType => {
        if (!this.mapContainer || !this.mapContainer.current) return;
        if (actionType === mapActions.MAP) {
            this.mapContainer.current.saveCameraDetails()
            this.props.SetActionMapPreview(actionType)
            this.mapContainer.current.closeMap();
            this.mapContainer.current.openMap(window.MapCore.IMcMapCamera.EMapType.EMT_2D, true, this.props.activeDtmMapPreview);
        } else if (actionType === mapActions.THREE_D) {
            this.mapContainer.current.saveCameraDetails()
            this.props.SetActionMapPreview(actionType)
            this.mapContainer.current.closeMap();
            this.mapContainer.current.openMap(window.MapCore.IMcMapCamera.EMapType.EMT_3D, true, this.props.activeDtmMapPreview);
        } else if (actionType === mapActions.SHOW_DTM_MAP) {
            this.SetActionDtmOnClick();
        }
        // else if (actionType === mapActions.DESCRIPTION && this.context.activeMapPreview === mapActions.DESCRIPTION) {
        //     this.context.setMapPreviewCurrentAction('');
        // }
    };

    //#endregion

    //#region ContextMenu
    getContextMenu() {
        return (
            <ContextMenu
                ref={this.contextMenu}
                onContextMenuItemClick={this.onContextMenuItemClick}
                groupsTree={this.state.layerGroupsTree}
                selctedGroupsLayersNodesArr={[this.selectedGroupsNodes, this.selectedLayersNodes]}
            />
        );
    }

    openContextMenu = (e, nodeLevel, node, nativeEventX = 0, nativeEventY = 0, isLog = false) => {
        e.preventDefault();
        e.stopPropagation();
        if (nodeLevel) {
            let menuItemsList = [];
            const { dict } = this.context;
            const preview = {
                name: dict.previewMap,
                type: 'previewMap',
                iconType: mapIcon
            };
            const serverConfiguration = {
                name: dict.serverConfiguration,
                type: popupTypes.SERVER_CONFIG,
                iconType: configIcon
            };
            const previewConfiguration = {
                name: dict.previewConfiguration,
                type: popupTypes.PREVIEW_CONFIG,
                iconType: configIcon
            };
            const about = {
                name: "About",
                type: popupTypes.ABOUT,
                iconType: aboutIcon
            };
            const rename = {
                name: 'Rename Group',
                type: popupTypes.RENAME,
                iconType: editIcon
            };
            const edit = {
                name: dict.edit + ' ' + dict[nodeLevel],
                type: popupTypes.EDIT,
                iconType: editIcon
            };
            const exportAction = {
                name: dict.export + ' ' + dict[nodeLevel],
                type: popupTypes.EXPORT,
                iconType: exportIcon
            };
            const remove = {
                name: dict.remove + ' ' + dict[nodeLevel],
                type: popupTypes.REMOVE,
                iconType: removeIcon
            };
            const moveToGroup = {
                name: 'Select Groups',
                type: popupTypes.MOVE_TO_GROUP,
                iconType: moveGroupIcon
            };
            const vectorIndexing = {
                name: 'Build Search Indexes',
                type: popupTypes.VECTOR_INDEXING,
                iconType: vectorIndexingIcon
            };
            const importMcPackage = {
                name: 'Import MC Package',
                type: popupTypes.SHOW_DROPZONE,
                iconType: mapModel
            };
            const exportAllRepository = {
                name: 'Export Repository',
                type: popupTypes.EXPORT,
                iconType: exportIcon
            };
            const clearLog = {
                name: 'Clear Log Window',
                type: popupTypes.CLEAR_LOG,
                iconType: removeIcon
            };
            const exportLog = {
                name: 'Export Log',
                type: popupTypes.EXPORT,
                iconType: exportIcon
            };
            const add = {
                name: `${dict.add} ${nodeLevel === config.nodesLevel.repository ? dict.group : dict.layer}`,
                type: nodeLevel === config.nodesLevel.group ? popupTypes.SHOW_DROPZONE : popupTypes.ADD,
                iconType: addIcon,
                subMenuItems:
                    nodeLevel === config.nodesLevel.group
                        ? [
                            {
                                name: "Native Layer",
                                iconType: mapDefault
                            },
                            {
                                name: "Raw " + dict.raster,
                                iconType: mapRaster
                            },
                            {
                                name: "Raw " + dict.dtm,
                                iconType: mapDtm
                            },
                            {
                                name: "Raw " + dict.vector,
                                iconType: mapVector
                            },
                            {
                                name: "Raw S-57",
                                iconType: mapS57
                            },
                            {
                                name: "Raw " + dict.model,
                                iconType: mapModel
                            },
                            {
                                name: "Raw Vector 3D Extrusion",
                                iconType: mapVector
                            },
                        ]
                        : null
            };
            if (isLog) { nodeLevel = "log" }
            switch (nodeLevel) {
                case config.nodesLevel.repository:
                    menuItemsList.push(add);
                    menuItemsList.push(importMcPackage);
                    menuItemsList.push(exportAllRepository);
                    menuItemsList.push(serverConfiguration);
                    menuItemsList.push(previewConfiguration);
                    menuItemsList.push(about);
                    break;
                case config.nodesLevel.group:
                    if (node && node.childNodes?.length === 0 && node.title !== config.LAYERS_BACKLOGS_TITLE) {
                        // Empty group
                        menuItemsList.push(add);
                        menuItemsList.push(rename);
                        menuItemsList.push(remove);
                    } else if (node && node.childNodes?.length === 0) {
                        //Empty layer backlog group
                        remove.name = 'Clear Backlog';
                        menuItemsList.push(remove);
                    } else if (node.title !== config.LAYERS_BACKLOGS_TITLE) {
                        // Regular group
                        menuItemsList.push(preview);
                        menuItemsList.push(add);
                        menuItemsList.push(exportAction);
                        menuItemsList.push(rename);
                        menuItemsList.push(remove);
                    } else {
                        // Layer Backlog Group
                        remove.name = 'Clear Backlog';
                        menuItemsList.push(remove);
                    }
                    if (this.selectedGroupsNodes.length > 1) {
                        menuItemsList = menuItemsList.filter(menuItem => menuItem.name != "Rename Group" && menuItem.name != "Add Layer");
                    }
                    break;
                case config.nodesLevel.layer:
                    if (this.context.selectedLayers.length > 1) {
                        menuItemsList.push(preview);
                        exportAction.name = 'Export Layers';
                        menuItemsList.push(exportAction);
                        if (this.selectedGroupsNodes.length == 0) {
                            menuItemsList.push(moveToGroup);
                        }
                        if (node.Group.startsWith(config.BACKLOG_PREFIX)) {
                            remove.name = 'Delete Layers'
                            moveToGroup.name = 'Move To Groups'
                        } else {
                            remove.name = 'Remove Layers'
                        }
                        menuItemsList.push(remove);
                    } else {
                        if (layerTypesStrings[node.LayerType] == 'Vector') {
                            menuItemsList.push(vectorIndexing);
                        }
                        menuItemsList.push(preview);
                        menuItemsList.push(edit);
                        menuItemsList.push(exportAction);
                        menuItemsList.push(moveToGroup);
                        menuItemsList.push(remove);
                        if (node.Group.startsWith(config.BACKLOG_PREFIX)) {
                            remove.name = 'Delete Layer'
                            moveToGroup.name = 'Move To Groups'

                        }
                    }
                    break;
                case "log":
                    menuItemsList.push(clearLog);
                    menuItemsList.push(exportLog);
                    break;
                default:
                    break;
            }
            nativeEventX = e.nativeEvent?.x ? e.nativeEvent.x : nativeEventX;
            nativeEventY = e.nativeEvent?.y ? e.nativeEvent.y : nativeEventY;
            this.contextMenu.current.updatePosition(
                nativeEventX,
                nativeEventY,
                menuItemsList,
                nodeLevel,
                node
            );
        }
    };

    onContextMenuItemClick = data => {
        // this.isLayerRemoveFromPreviewedLayers = false;

        if (data.type === config.actions.previewMap) {
            this.isLayerRemoveFromPreviewedLayers = false;
            this.setState({ closeErrorPopup: false });
            ViewportService.closeViewport(1);
            let mapPreview = {};
            let actions = {};
            let title;
            actions[mapActions.MAP] = mapActions.MAP;
            if (this.selectedLayersNodes.length == 1 && this.selectedLayersNodes[0].LayerId == data.node.LayerId && this.selectedGroupsNodes.length == 0
                || this.selectedGroupsNodes.length == 1 && this.selectedGroupsNodes[0] == data.node && this.selectedLayersNodes.length == 0) {//one group or one layer preview
                if (data.nodeLevel === "group") {
                    // group preview
                    let isHasDTM = data.node.childNodes.find(c => c.LayerType.includes('DTM'));
                    let isHasModel = data.node.childNodes.find(c => c.LayerType.includes('3D_MODEL'));
                    let isHasV3DExtrusion = data.node.childNodes.find(c => c.LayerType.includes('VECTOR_3D_EXTRUSION'));
                    if (isHasDTM || isHasModel || isHasV3DExtrusion) {
                        actions[mapActions.THREE_D] = mapActions.THREE_D;
                        actions[mapActions.SHOW_DTM_MAP] = mapActions.SHOW_DTM_MAP;
                    } else if (isHasModel || isHasV3DExtrusion) {
                        actions[mapActions.THREE_D] = mapActions.THREE_D;
                        actions[mapActions.SHOW_DTM_MAP] = mapActions.SHOW_DTM_MAP;
                    }
                    title = data.node.title;
                } else {
                    // single layer preview
                    title = data.node.parentFolder;
                    mapPreview.layerType = data.node.LayerType;
                    mapPreview.group = data.node.parentFolder;

                    if (data.node.LayerType.includes('3D_MODEL') || data.node.LayerType.includes('VECTOR_3D_EXTRUSION') || data.node.LayerType.includes('DTM')) {
                        actions[mapActions.THREE_D] = mapActions.THREE_D;
                        actions[mapActions.SHOW_DTM_MAP] = mapActions.SHOW_DTM_MAP;
                    }
                }
                mapPreview.data = data.node;
                mapPreview.type = data.nodeLevel;

            } else {//for all other options of multi selection.
                let flatedLayersToPreviewArr = [...this.selectedGroupsNodes.map(g => g.childNodes).flat(), ...this.selectedLayersNodes];

                let isHasDTM = flatedLayersToPreviewArr.find(c => c.LayerType.includes('DTM'));
                let isHasModel = flatedLayersToPreviewArr.find(c => c.LayerType.includes('3D_MODEL'));
                let isHasV3DExtrusion = flatedLayersToPreviewArr.find(c => c.LayerType.includes('VECTOR_3D_EXTRUSION'));
                if (isHasDTM || isHasModel || isHasV3DExtrusion) {
                    actions[mapActions.THREE_D] = mapActions.THREE_D;
                    actions[mapActions.SHOW_DTM_MAP] = mapActions.SHOW_DTM_MAP;
                } else if (isHasModel || isHasV3DExtrusion) {
                    actions[mapActions.THREE_D] = mapActions.THREE_D;
                    actions[mapActions.SHOW_DTM_MAP] = mapActions.SHOW_DTM_MAP;
                }
                title = "group/s and layer/s";
                mapPreview.data = [this.selectedGroupsNodes, this.selectedLayersNodes];
                mapPreview.type = "group and layer";
            }

            mapPreview.title = title;
            mapPreview.mapActions = actions;

            this.props.SetActionMapPreview(mapActions.MAP)
            this.context.setMapPreviewDetails(mapPreview);
            this.props.setMapPreview(mapPreview);
            this.setState({ layersToPreview: this.flatDataToPreview(mapPreview.data) })
        } else if (data.type === popupTypes.EXPORT) {
            this.handleExportCommand(data);
        } else if (data.type === popupTypes.VECTOR_INDEXING) {
            this.handleVectorIndexingCommand(data);
            // } else if (data.type === popupTypes.ABOUT) {
            //     this.aboutMapCore();
        }
        else if (data.type === popupTypes.REMOVE) {
            this.handleRemoveCommand(data);
        }
        else if (data.type === popupTypes.CLEAR_LOG) {
            this.clearLogMessages();
        }
        else {
            // open popups
            const { type, name, nodeLevel, node, layerType } = data;
            this.openPopup(type, name, nodeLevel, node, layerType);
        }
    };
    flatDataToPreview(data) {
        let layersToPreview = [];
        if (data.length) {//arr
            layersToPreview = data[1];
            data[0].forEach((g, ind) => {
                layersToPreview = [...layersToPreview, ...g.childNodes];
            })
        }
        else {//group or layer
            layersToPreview = data.childNodes ? data.childNodes : [data];
        }
        let uniqueLayersToPreview = this.filterToUniqueSelectedLayers(layersToPreview, false)
        return uniqueLayersToPreview;
    }
    isGroupContainsDTM(groupName) {
        const group = this.state.layerGroupsTree.find(node => node.title === groupName);
        const isGroupContainsDTM = group.childNodes.some(map => map.LayerType.includes('DTM'));
        return isGroupContainsDTM;
    }
    getDTMMapFromGroup(groupName) {
        const group = this.state.layerGroupsTree.find(node => node.title === groupName);
        const dtmMap = group.childNodes.filter(map => map.LayerType.includes('DTM'));
        if (dtmMap) {
            return dtmMap;
        }
        return null;
    }
    async handleVectorIndexingCommand(data) {
        const layerId = data.node.LayerId;
        let response = await this.vectorIndexingOps({ operation: config.indexOps.api.getStatus, layerId });
        if (!response) {
            this.context.showSideNotification({ text: `Error when trying to vectorIndexing: no response from server` });
            console.error(`Error when trying to vectorIndexing: no response from server`);
        }
        let indexedFields = [];
        const statusTypes = config.indexOps.statusTypes;
        switch (response.IndexStatus) {
            case statusTypes.noSuchLayer:
                {
                    this.context.showSideNotification({ text: `Error when trying to vectorIndexing: noSuchLayer` });
                    console.error('Error when trying to vectorIndexing : noSuchLayer');
                    return;
                }
            case statusTypes.inProgress:
                {
                    this.context.showSideNotification({ text: `Indexing already in progress` });
                    console.error('Indexing already in progress');
                    return;
                }
            case statusTypes.indexed:
                {
                    const getInfoResponse = await this.vectorIndexingOps({ operation: config.indexOps.api.getInfo, layerId });
                    if (getInfoResponse &&
                        getInfoResponse.TextDataIndexConfig) {
                        indexedFields = getInfoResponse.TextDataIndexConfig.ParticipatingFieldNames;
                    }
                    break;
                }
        }

        let allFields = await this.vectorIndexingOps({ operation: config.indexOps.api.getFields, layerId });

        if (!allFields || allFields.length == 0) {
            this.context.showSideNotification({ text: `Error when trying to vectorIndexing : no fields on vector` });
            console.error('Error when trying to vectorIndexing : no fields on vector');
            return;
        }

        const { type, name, nodeLevel, node, layerType } = data;
        this.openPopup(type, name, nodeLevel, node, layerType);
        this.setState({ popupToOpen: { ...this.state.popupToOpen, allFields, indexedFields } });
        this.context.removeAllSideNotifications();
    }
    handleRemoveCommand(data) {
        const { type, name, nodeLevel, node, layerType } = data;
        let finalNode = this.isOnlyOneGroupOrLayerSelected() ? node : [this.selectedGroupsNodes, this.selectedLayersNodes];
        this.openPopup(type, name, nodeLevel, finalNode, layerType);
    }
    // Function to convert the array of objects to a text format
    arrayToTxt = () => {
        const { logs } = this.state;
        const txtContent = logs.map((item, i) => {
            let header = "";
            if (i == 0) {
                header = "\n  Logs : \n";
            }
            // return `${header}\n${i + 1}. ${item.color == 'red' ? 'ERROR :' : ''} ${item.message}`;
            return `${header}\n${i + 1}. ${item.message}`;
        }).join('');
        return txtContent;
    };
    handleLogDownloadAsTxt = () => {
        const txtContent = this.arrayToTxt();
        const blob = new Blob([txtContent], { type: 'text/plain' });
        const url = URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        let dateString = this.createDateHourString();
        dateString = dateString.replace(/:/g, "_")
        a.download = `logs${dateString}.txt`;
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
        URL.revokeObjectURL(url);
    };

    async handleExportCommand(data) {
        const { selectedLayers, clearSelectedLayers } = this.context;
        const { selectedLayersNodes, selectedGroupsNodes, selectedGroupsKeys, selectedLayersKeys } = this;
        if (!data.node) {
            this.handleLogDownloadAsTxt();
        }
        else if (data.nodeLevel === config.nodesLevel.layer && selectedGroupsNodes.length == 0) {//only layer/s
            if (selectedLayersKeys.length === 0 && this.context.selectedLayers.length < 2) {//export one unselected layer
                let { type, name, nodeLevel, node, layerType } = data;
                clearSelectedLayers();
                this.openPopup(type, name, nodeLevel, node, layerType);
            }
            else {//export selected layer/s from same group or not
                let { type, name, nodeLevel, node, layerType } = data;
                let filteredNodes = selectedLayersKeys.length === 0 ? this.findLayersNodesOfMultiBacklog() : selectedLayersNodes;
                clearSelectedLayers();
                this.openPopup(type, name, nodeLevel, filteredNodes, layerType);
            }
        }
        else if (data.nodeLevel === config.nodesLevel.group && selectedLayersNodes.length == 0) {//only group/s
            if (selectedGroupsNodes.length == 0) {//one unselected group export
                const { type, name, nodeLevel, node, layerType } = data;
                let filteredNodes = node;
                clearSelectedLayers();
                this.openPopup(type, name, nodeLevel, filteredNodes, layerType);
            }
            else {//some groups export
                const { type, name, nodeLevel, node, layerType } = data;
                let filteredNodes = JSON.parse(JSON.stringify(selectedGroupsNodes));
                clearSelectedLayers();
                this.openPopup(type, "Groups", nodeLevel, filteredNodes, layerType);
            }
        }
        else if (data.nodeLevel === config.nodesLevel.repository) {//repository export
            const { type, name, nodeLevel, node, layerType } = data;
            let filteredNodes = node;
            this.openPopup(type, name, nodeLevel, filteredNodes, layerType);
        }
        else {
            //group and layers export
            let filteredSelectedGroupsAndLayersNodes = [selectedGroupsNodes, selectedLayersNodes];
            clearSelectedLayers();
            const { type, name, nodeLevel, node, layerType } = data;
            this.openPopup(type, "Group and Layer", nodeLevel, filteredSelectedGroupsAndLayersNodes, layerType);
        }
    }
    //#endregion

    //#region  WebSockets
    initializeWebSockets() {
        _socket = new WebSocket(this.context.wsUrl);
        _socket.onopen = this.handleWSOpen;
        _socket.onclose = this.handleWSClose;
        clearInterval(this.refreshInterval);

        _socket.addEventListener('message', event => {
            if (this.isJsonString(event.data)) {
                let value = JSON.parse(event.data);
                let hourString = this.createDateHourString().replace("_", ":");
                hourString = hourString.substring(hourString.length - 8, hourString.length);
                if (value.message.hasOwnProperty('LogMessageType')) {
                    let logObj = value.message.LogMessageType == "Warning" ?
                        { message: hourString + " - WARNING: " + value.message.Message, color: 'yellow' } :
                        value.message.LogMessageType == "Log" ?
                            { message: hourString + ' : ' + value.message.Message, color: 'yellow' } :
                            { message: hourString + " - ERROR: " + value.message.Message, color: 'red' };
                    this.setState({ logs: [...this.state.logs, logObj] })
                    if (value.message.LogMessageType == "Error" && value.message.hasOwnProperty('LayerId')) {
                        let layerIds = this.state.currentlyUploadedFiles.map((partOfLayer) => partOfLayer.layersParams.layerId).flat();
                        let lastIndexOfLayerId = layerIds.lastIndexOf(value.message.LayerId);
                        if (lastIndexOfLayerId != -1) {
                            this.isCompletedUploadInServer = true;
                            this.setState({ isCompletedUploadInServer: true })
                            this.setState({ isErrorFromWebSocket: true })
                            this.setUploadFileProgress(
                                'error',
                                this.state.currentlyUploadedFiles[lastIndexOfLayerId].fileName +
                                this.state.currentlyUploadedFiles[lastIndexOfLayerId]
                                    .keyForUploadedLayer,
                                value.message.Message
                            );
                            clearInterval(this.timerIsCompletedUploadInServer);
                            this.setState({ isErrorFromWebSocket: false })
                        }
                    }
                }
                else if (value.message.hasOwnProperty('layer_add')) {
                    if (value.message.layer_add.is_ok) {
                        this.isCompletedUploadInServer = true;
                        this.setState({ isCompletedUploadInServer: true })
                        this.context.removeAllSideNotifications()
                        this.repositoryTree.current.refreshRepositoryTree();
                        // if (value.message.layer_add.layerType.includes('DTM'))
                        //     this.context.refreshDTMLayers();
                        let logObj = { message: hourString + " - Layer " + value.message.layer_add.layerId + " - was added succesfully.", color: 'yellow' }
                        this.setState({ logs: [...this.state.logs, logObj] })
                    }
                    else if (value.message.layer_add.reason) {
                        let layerIds = this.state.currentlyUploadedFiles.map((partOfLayer) => partOfLayer.layersParams.layerId).flat();
                        let lastIndexOfLayerId = layerIds.lastIndexOf(value.message.layer_add.layerId);
                        if (lastIndexOfLayerId != -1) {
                            this.context.removeAllSideNotifications()
                            this.isCompletedUploadInServer = true;
                            this.setState({ isCompletedUploadInServer: true })
                            this.setState({ isErrorFromWebSocket: true })
                            this.setUploadFileProgress(
                                'error',
                                this.state.currentlyUploadedFiles[lastIndexOfLayerId].fileName +
                                this.state.currentlyUploadedFiles[lastIndexOfLayerId]
                                    .keyForUploadedLayer,
                                value.message.layer_add.reason
                            );
                            clearInterval(this.timerIsCompletedUploadInServer);
                            let finalError = value.message.layer_add.layerId + " - " + value.message.layer_add.reason;
                            this.setState({ webSocketErrorMessagesInUpload: [...this.state.webSocketErrorMessagesInUpload, finalError] });
                            let logObj = { message: hourString + ` - Layer Id ${value.message.layer_add.layerId} - ${value.message.layer_add.reason}.`, color: 'red' }
                            this.setState({ logs: [...this.state.logs, logObj] })
                        }
                    }
                }
                else if (value.message.hasOwnProperty('layer_delete')) {
                    this.repositoryTree.current.refreshRepositoryTree();
                    let layerId = value.message.layer_delete.layerId ? value.message.layer_delete.layerId : "One layer";
                    let logObj = { message: hourString + " - Layer " + layerId + " - was deleted succesfully.", color: 'yellow' }
                    this.setState({ logs: [...this.state.logs, logObj] })
                }
                else if (value.message.hasOwnProperty('group_rename')) {
                    this.repositoryTree.current.refreshRepositoryTree();
                    let logObj = { message: hourString + " - Group " + value.message.group_rename.oldGroupName + " - was renamed to " + value.message.group_rename.newGroupName + " succesfully.", color: 'yellow' }
                    this.setState({ logs: [...this.state.logs, logObj] })
                }
                else if (value.message.hasOwnProperty('layer_param_changed')) {
                    this.repositoryTree.current.refreshRepositoryTree();
                }
                else if (value.message.hasOwnProperty('vector_processing') &&
                    value.message.vector_processing.raw_layer_id === this.state.uploadingLayerId) {
                    if (!value.message.vector_processing.is_ok) {
                        clearInterval(this.timerIsCompletedUploadInServer);
                        this.timerIsCompletedUploadInServer = null;
                        let msgUi = value.message.vector_processing.state == "No Error" ? "failed" : value.message.vector_processing.state;
                        this.setUpConvertLayerProgress(value, 100,
                            value.message.vector_processing.title,
                            msgUi);

                        this.setUploadFileProgress('error',
                            this.state.currentlyUploadedFiles[this.state.currentlyUploadedFiles.length - 1].fileName +
                            this.state.currentlyUploadedFiles[this.state.currentlyUploadedFiles.length - 1].keyForUploadedLayer,
                            msgUi);
                    }
                    else if (value.message.vector_processing.state == "Identifying layers") {
                        this.setUpConvertLayerProgress(value, 10, value.message.vector_processing.title, 'Identifying...');
                    }
                    else if (value.message.vector_processing.state == "converting") {
                        let precent = 10 + 90 *
                            (1 + value.message.vector_processing.sub_layer_no) / (2 * value.message.vector_processing.num_of_layers);
                        this.setUpConvertLayerProgress(value, precent, value.message.vector_processing.title,
                            "converting " + value.message.vector_processing.sub_layer_id);
                    }
                    else if (value.message.vector_processing.state == "converted") {

                        let precent = 10 + 90 *
                            (1 + value.message.vector_processing.sub_layer_no) / value.message.vector_processing.num_of_layers;

                        if (precent < 100) {
                            this.setUpConvertLayerProgress(value, precent,
                                value.message.vector_processing.title,
                                value.message.vector_processing.is_ok ?
                                    value.message.vector_processing.sub_layer_id :
                                    'Conversion failed for : ' + value.vector_processing.sub_layer_id);
                        }
                        else {
                            this.setUploadFileProgress(
                                100,
                                this.state.currentlyUploadedFiles[this.state.currentlyUploadedFiles.length - 1].fileName +
                                this.state.currentlyUploadedFiles[this.state.currentlyUploadedFiles.length - 1]
                                    .keyForUploadedLayer);
                            this.clearUnShownFiles();
                            this.setState({ currentlyProcessedFiles: [] });
                            this.setState({ uploadingLayerId: '' });

                        }
                        //this.block = false;                        
                    }
                }
            }
            else {
                console.log(`${event.data} is not a valid JSON string`);
            }
        });

        _socket.addEventListener('error', event => {
            console.log("WebSocket error:" + event);
        })
    }
    isJsonString(testString) {
        let result = true;
        try {
            let val = JSON.parse(testString);
        }
        catch (e) {
            result = false;
        }
        finally {
            return result;
        }
    }
    clearUnShownFiles() {
        const newArr = this.state.currentlyUploadedFiles.filter(f => f.isShown);
        this.setState({ currentlyUploadedFiles: newArr });
    }
    setUpConvertLayerProgress(value, percent, layerTitle, errorMessage) {
        const currentlyProcessedFiles = [...this.state.currentlyProcessedFiles];

        const currentlyProcessIndex = currentlyProcessedFiles.findIndex(
            currentlyProcessedFile =>
                currentlyProcessedFile.message.vector_processing.hasOwnProperty('title') &&
                currentlyProcessedFile.message.vector_processing.title === layerTitle);

        const currentlyProcessed = {
            ...value,
            percent,
            errorMessage
        };

        if (currentlyProcessIndex >= 0) {
            currentlyProcessedFiles.splice(currentlyProcessIndex, 1, currentlyProcessed);
        }
        else {
            currentlyProcessedFiles.push(currentlyProcessed);
        }
        this.setState({ currentlyProcessedFiles });
    }
    generateDefaultLayerIdToMcPackage = (type) => {
        let pattern = `${layerTypesStrings[type]}-`;
        let serialNum = 0;
        if (this.state.mcPackageDefaultLayerId) {
            serialNum = this.state.mcPackageDefaultLayerId + 1;
            this.setState({ mcPackageDefaultLayerId: serialNum })
        }
        else {
            const currentGroupLayers = this.state.layerGroupsTree.map((node) => node.childNodes).flat();
            if (currentGroupLayers && currentGroupLayers.length > 0) {
                currentGroupLayers.forEach(layer => {
                    if (layer.LayerId.startsWith(pattern)) {
                        let num = parseInt(layer.LayerId.substring(layer.LayerId.lastIndexOf('-') + 1, layer.LayerId.length));
                        if (!isNaN(num) && num > serialNum) {
                            serialNum = num;
                        }
                    }
                });
            }
            serialNum++;
            this.setState({ mcPackageDefaultLayerId: serialNum })
        }

        // if (this.props.layerUploading && this.props.layerUploading.layerId && this.props.layerUploading.layerId.startsWith(pattern))
        //   serialNum = parseInt(this.props.layerUploading.layerId.substring(this.props.layerUploading.layerId.lastIndexOf('-') + 1, this.props.layerUploading.layerId.length)) + 1;
        this.setState({ finalDefaultNameForLargeMcPackage: `${pattern}${serialNum}` })
        return `${pattern}${serialNum}`;
    }
    uploadFiles = async (layersParams) => {
        // TODO: to stop the layer next files upload if one of the files return error.
        this.isCompletedUploadInServer = false;
        this.setState({ isCompletedUploadInServer: false });
        if (layersParams.McPackage) {
            this.context.showSideNotification({ text: `The McPackage upload is under processing. Once completed, the repository tree will be refreshed.` });
        }
        this.layerUploading = layersParams;
        if (this.isUploading) return;
        this.isUploadingFile = true;
        this.isUploading = true;
        this.isInProgress = false;
        this.precentage = 100;
        this.hasError = false;
        this.setState({ uploadingLayerId: layersParams.layerId });

        this.isAsyncProcess = layersParams.layerType == 'RAW_VECTOR';

        // For Async process mark it as -1 percent to cause the synchronous style to be shown.
        if (this.isAsyncProcess) {
            let value = {
                message: {
                    vector_processing: {
                        raw_layer_id: layersParams.layerId,
                        title: layersParams.title,
                        state: "initializing"
                    }
                }
            };
            this.setUpConvertLayerProgress(value, -1, layersParams.title, '');
        }

        for (let i = 0; i < this.state.currentlyUploadedFiles.length; i++) {
            if (this.state.currentlyUploadedFiles[i].percent === 0) {
                if (
                    this.state.currentlyUploadedFiles[i].fileObj.size >
                    config.FILE_UPLOAD_MAX_SIZE_IN_BYTES
                ) {
                    // LARGE SIZE FILE UPLOAD - START
                    const numOfSequences = Math.ceil(
                        this.state.currentlyUploadedFiles[i].fileObj.size /
                        config.FILE_UPLOAD_MAX_SIZE_IN_BYTES
                    );

                    for (
                        let fileSequence = 0;
                        fileSequence < numOfSequences;
                        fileSequence++
                    ) {
                        const fileChunk = this.state.currentlyUploadedFiles[
                            i
                        ].fileObj.slice(
                            fileSequence * config.FILE_UPLOAD_MAX_SIZE_IN_BYTES,
                            (fileSequence + 1) *
                            config.FILE_UPLOAD_MAX_SIZE_IN_BYTES
                        );

                        const formData = new FormData();
                        Object.keys(
                            this.state.currentlyUploadedFiles[i].layersParams
                        ).forEach(key => {
                            formData.append(
                                key,
                                this.state.currentlyUploadedFiles[i]
                                    .layersParams[key]
                            );
                        });

                        let idLayer = "";
                        if (layersParams.layerId) { idLayer = this.state.currentlyUploadedFiles[i].layersParams.layerId }
                        else if (!this.state.finalDefaultNameForLargeMcPackage) { idLayer = this.generateDefaultLayerIdToMcPackage("McPackage"); }
                        else { idLayer = this.state.finalDefaultNameForLargeMcPackage }

                        formData.set(
                            'layerId',
                            idLayer
                        );
                        formData.set(
                            'fileNum',
                            this.state.currentlyUploadedFiles[i].fileIndex
                        );
                        formData.set(
                            'totalFiles',
                            this.state.currentlyUploadedFiles[i].totalFiles
                        );
                        formData.set('fileSequence', fileSequence);
                        formData.set('numOfSequences', numOfSequences);
                        formData.set(
                            'mapFile',
                            fileChunk,
                            this.state.currentlyUploadedFiles[i].fileName
                        );
                        try {
                            console.log(formData);
                            const response = await axios.post(
                                config.urls.uploadFiles,
                                formData
                            );
                            const fileUploadProgress =
                                ((fileSequence + 1) / numOfSequences) * 100;
                            if (!this.state.isErrorFromWebSocket) {
                                this.setUploadFileProgress(
                                    fileUploadProgress,
                                    this.state.currentlyUploadedFiles[i].fileName +
                                    this.state.currentlyUploadedFiles[i]
                                        .keyForUploadedLayer
                                );
                            }
                            if (
                                response.data &&
                                response.data.MapLayerConfig &&
                                response.data.MapLayerConfig.length > 0
                            ) {
                                this.mapTreeRepoSetLayers(response.data);
                            } else if (response && response.data === 'IN-PROGRESS') {
                                const layerTitle = this.state.currentlyUploadedFiles[i].layersParams.title ? this.state.currentlyUploadedFiles[i].layersParams.title :
                                    this.state.currentlyUploadedFiles[i].layersParams.McPackage;
                                layerTitle = layerTitle ? layerTitle : "";
                                if (layerTitle !== "") { this.context.showSideNotification({ text: `Upload ${layerTitle} is under processing. Once completed, the repository tree will be refreshed.` }); }
                                else { this.context.showSideNotification({ text: `The upload is under processing. Once completed, the repository tree will be refreshed.` }); }
                                setTimeout(() => {
                                    this.context.removeAllSideNotifications();
                                }, 10000)
                            }
                        } catch (e) {
                            if (e.isAxiosError) {
                                const errorMessage =
                                    (e.response &&
                                        e.response.data &&
                                        e.response.data.errorMessage) ||
                                    'Failed';
                                this.isAsyncProcess = false;
                                this.hasError = true;
                                this.setUploadFileProgress(
                                    'error',
                                    this.state.currentlyUploadedFiles[i]
                                        .fileName +
                                    this.state.currentlyUploadedFiles[i]
                                        .keyForUploadedLayer,
                                    errorMessage
                                );
                            }
                            break;
                        }
                    }
                    // LARGE SIZE FILE UPLOAD - END
                } else {
                    // NORMAL SIZE FILE UPLOAD - START
                    const formData = new FormData();
                    Object.keys(
                        this.state.currentlyUploadedFiles[i].layersParams
                    ).forEach(key => {
                        formData.append(
                            key,
                            this.state.currentlyUploadedFiles[i].layersParams[
                            key
                            ]
                        );
                    });
                    let idLayer = "";
                    if (layersParams.layerId) { idLayer = idLayer = this.state.currentlyUploadedFiles[i].layersParams.layerId }
                    else { idLayer = this.generateDefaultLayerIdToMcPackage("McPackage"); }

                    formData.set(
                        'layerId',
                        idLayer
                    );
                    formData.set(
                        'fileNum',
                        this.state.currentlyUploadedFiles[i].fileIndex
                    );
                    formData.set(
                        'totalFiles',
                        this.state.currentlyUploadedFiles[i].totalFiles
                    );
                    formData.set(
                        'mapFile',
                        this.state.currentlyUploadedFiles[i].fileObj,
                        this.state.currentlyUploadedFiles[i].fileName
                    );
                    try {
                        const response = await axios.post(
                            config.urls.uploadFiles,
                            formData,
                            {
                                onUploadProgress: progressEvent => {
                                    const percentUpload = layersParams.McPackage ? 40 : Math.round((progressEvent.loaded / progressEvent.total) * 100);
                                    if (percentUpload < 100) {
                                        this.setUploadFileProgress(
                                            percentUpload,
                                            this.state.currentlyUploadedFiles[i]
                                                .fileName +
                                            this.state
                                                .currentlyUploadedFiles[i]
                                                .keyForUploadedLayer
                                        );
                                    }
                                }
                            }
                        );
                        // setTimeout(() => this.checkVectorIndexingProgress(layerId), 10000);
                        if (!this.state.isErrorFromWebSocket) {
                            this.setUploadFileProgress(
                                100,
                                this.state.currentlyUploadedFiles[i].fileName +
                                this.state.currentlyUploadedFiles[i]
                                    .keyForUploadedLayer
                            );
                        }

                        if (
                            response.data &&
                            response.data.MapLayerConfig &&
                            response.data.MapLayerConfig.length > 0
                        ) {
                            this.mapTreeRepoSetLayers(response.data);
                        } else if (response && response.data === 'IN-PROGRESS') {
                            const layerTitle = this.state.currentlyUploadedFiles[i].layersParams.title;
                            this.isInProgress = true;
                            this.context.showSideNotification({ text: `Upload ${layerTitle} is under processing. Once completed, the repository tree will be refreshed.` });
                            setTimeout(() => {
                                this.context.removeAllSideNotifications();
                            }, 10000)
                        }
                    } catch (e) {
                        if (e.isAxiosError) {
                            const errorMessage =
                                (e.response &&
                                    e.response.data &&
                                    e.response.data.errorMessage) ||
                                'Failed';
                            this.isAsyncProcess = false;
                            this.hasError = true;
                            if (!this.state.isErrorFromWebSocket) {
                                for (let index = i; index < this.state.currentlyUploadedFiles.length; index++) {
                                    if (this.findLastFileInSpecificUpload(this.state.currentlyUploadedFiles[i].keyForUploadedLayer, this.state.currentlyUploadedFiles) >= index) {
                                        this.setUploadFileProgress('error', this.state.currentlyUploadedFiles[index].fileName + this.state.currentlyUploadedFiles[i].keyForUploadedLayer, errorMessage);
                                    }
                                }

                            }
                        }
                        break;
                    }
                    // NORMAL SIZE FILE UPLOAD - END
                }
            }
        }

        if (this.isInProgress && this.isAsyncProcess) {
            this.precentage = 99;
        }
        if (!this.hasError && !this.state.isErrorFromWebSocket) {
            this.setUploadFileProgress(
                this.precentage,
                this.state.currentlyUploadedFiles[this.state.currentlyUploadedFiles.length - 1].fileName +
                this.state.currentlyUploadedFiles[this.state.currentlyUploadedFiles.length - 1]
                    .keyForUploadedLayer);
        }
        if (this.precentage == 100) {
            this.clearUnShownFiles();
            // if (layersParams.layerType.includes('DTM')) {
            // this.context.refreshDTMLayers();
            // }
        }
        this.setState({ finalDefaultNameForLargeMcPackage: null })
        this.isUploading = false;
        this.isUploadingFile = false;
        this.setState({ isErrorFromWebSocket: false })
    };
    findLastFileInSpecificUpload(currentUploadKey, currentlyUploadedFiles) {
        let tmpArr = [];
        currentlyUploadedFiles.forEach((item, index) => {
            let ind = item.keyForUploadedLayer == currentUploadKey ? -1 : index;
            tmpArr = [...tmpArr, ind];
        });
        let indexOfLastFileInSpecificUpload = tmpArr.lastIndexOf(-1);
        return indexOfLastFileInSpecificUpload
    }
    isCurrentUploadFilelastFileInSpecificUpload(currentyUploadedIndex, currentlyUploadedFiles) {
        let spesificKey = currentlyUploadedFiles[currentyUploadedIndex].keyForUploadedLayer;
        let tmpArr = [];
        currentlyUploadedFiles.forEach((item, index) => {
            let ind = item.keyForUploadedLayer == spesificKey ? -1 : index;
            tmpArr = [...tmpArr, ind];
        });
        let indexOfLastFileInSpecificUpload = tmpArr.lastIndexOf(-1);
        return indexOfLastFileInSpecificUpload > currentyUploadedIndex;
    }
    setUploadFileProgress(percent, fileUniqeId, errorMessage) {
        // This is to keep the element in the same place at the array so the process list component elemnets will not loop
        const currentlyUploadedFiles = [...this.state.currentlyUploadedFiles];
        const currentyUplodedIndex = currentlyUploadedFiles.findIndex(
            currentlyUploadedFile =>
                currentlyUploadedFile.fileName +
                currentlyUploadedFile.keyForUploadedLayer ===
                fileUniqeId
        );

        const currentyUploded = {
            ...currentlyUploadedFiles[currentyUplodedIndex],
            percent,
            errorMessage
        };
        if (!currentlyUploadedFiles[currentyUplodedIndex].percent || currentlyUploadedFiles[currentyUplodedIndex].percent != 100) {
            currentlyUploadedFiles.splice(currentyUplodedIndex, 1, currentyUploded);

            if (this.isCompletedUploadInServer || this.state.isCompletedUploadInServer || this.state.isErrorFromWebSocket || percent < 100 || percent == "error" || this.isCurrentUploadFilelastFileInSpecificUpload(currentyUplodedIndex, currentlyUploadedFiles)) {
                this.setState({ currentlyUploadedFiles });
            }
            else if (!this.isCompletedUploadInServer && !this.state.isCompletedUploadInServer && !this.state.isErrorFromWebSocket && !this.timerIsCompletedUploadInServer) {
                this.timerIsCompletedUploadInServer = setInterval(() => {
                    console.log("timerIsCompletedUploadInServer...");
                    if (this.isCompletedUploadInServer || this.state.isCompletedUploadInServer || this.state.isErrorFromWebSocket) {
                        let updatedUploadFiles = this.replaceFilesInUpload(currentlyUploadedFiles);
                        this.setState({ currentlyUploadedFiles: updatedUploadFiles });
                        console.log('before clearInterval');
                        console.log(this.timerIsCompletedUploadInServer);
                        clearInterval(this.timerIsCompletedUploadInServer);
                        this.timerIsCompletedUploadInServer = null;
                    }
                }, 3000);
                console.log('after set interval:');
                console.log(this.timerIsCompletedUploadInServer);
            }
        }
    }
    replaceFilesInUpload(oldUploadedFiles) {
        let updatedUploadedFiles = [...this.state.currentlyUploadedFiles];

        for (let index = 0; index < oldUploadedFiles.length; index++) {
            const file = oldUploadedFiles[index];
            let fileUniqeId = file.fileName + file.keyForUploadedLayer;
            const currentyUplodedIndex = updatedUploadedFiles.findIndex(
                currentlyUploadedFile =>
                    currentlyUploadedFile.fileName +
                    currentlyUploadedFile.keyForUploadedLayer ===
                    fileUniqeId
            );
            if (file.percent >= updatedUploadedFiles[currentyUplodedIndex].percent) {
                updatedUploadedFiles.splice(currentyUplodedIndex, 1, file);
            }

        }
        return updatedUploadedFiles;
    }
    startKeepAlive() {
        this.keepAliveInterval = setInterval(
            () => {
                if (_socket != null && window.sessionStorage.getItem('sessionId') != null) {
                    _socket.send('alive ' + window.sessionStorage.getItem('sessionId'));
                }
            },
            this.KEEP_ALIVE_TIMEOUT
        );
    }
    stopKeepAlive() {
        if (this.keepAliveInterval != null) {
            clearInterval(this.keepAliveInterval);
            this.keepAliveInterval = null;
        }
    }
    handleWSOpen = event => {
        if (window.sessionStorage.getItem('sessionId') == null) {
            window.sessionStorage.setItem('sessionId', Date.now());
        }
        this.websocket = event.target;
        this.websocket.send("subscribe " + window.sessionStorage.getItem('sessionId'));
        this.startKeepAlive();
    }
    handleWSClose = event => {
        this.stopKeepAlive();
        this.websocket = event.target;
        if (window.sessionStorage.getItem('sessionId') != null) {
            this.websocket.send("unsubscribe " + window.sessionStorage.getItem('sessionId'));
        }
        this.timerServerUp(event)
    }
    timerServerUp(event) {
        this.refreshInterval = setInterval(
            () => {
                if (event.code > 1014 || event.code < 1001) {
                    this.initializeWebSockets();
                    clearInterval(this.refreshInterval);
                }
            }
            , 30000)
    }

    hadneleBeforeunload = event => {
        this.stopKeepAlive();
        if (_socket != null && window.sessionStorage.getItem('sessionId') != null) {
            this.websocket.send("unsubscribe " + window.sessionStorage.getItem('sessionId'));
        }
    }
    //#endregion


    render() {
        if (this.context.initialDataError) {
            return this.renderInitialErrorMessage();
        }
        if (!this.context || !this.context.dict || !this.context.epsgCodes || this.context.epsgCodes.length === 0 ||
            !this.context.compatibeVersions || this.context.compatibeVersions.length === 0) {
            return <Loader loadingMessage={'initializing...'} />;
        }
        if (_socket == null) {
            this.initializeWebSockets();
        }
        return (
            //  beforeunload={this.hadneleBeforeunload}
            <div className={classNames.App}>
                {this.getGeneralErrorPopup()}
                {this.getPopup()}
                {this.getMainHeader()}
                {this.getActionsBar()}
                {this.getMainContent()}
                {this.getContextMenu()}
            </div>
        );
    }
}

const mapDispatchToProps = (dispacth) => {
    return {
        setMapPreview: (mapPreview) => dispacth(SaveMapToPreview(mapPreview)),
        SetActionMapPreview: (activeMapPreview) => dispacth(SetActionMapPreview(activeMapPreview)),
        SetActionDtmMapPreview: (activeDtmMapPreview) => dispacth(SetActionDtmMapPreview(activeDtmMapPreview)),
        SetErrorInPreview: (error) => dispacth(SetErrorInPreview(error)),
        SetOpenMapService: (openMapService) => dispacth(SetOpenMapService(openMapService)),
    }
};
const mapStateToProps = (state) => {
    return {
        activeMapPreview: state.MapContainerReducer.activeMapPreview,
        activeDtmMapPreview: state.MapContainerReducer.activeDtmMapPreview,
        errorInPreview: state.MapContainerReducer.errorInPreview,
        openMapService: state.MapContainerReducer.openMapService,
    }
};
export default connect(mapStateToProps, mapDispatchToProps)(App);


