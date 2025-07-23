import { useDispatch, useSelector } from "react-redux"
import { ReactElement, useEffect, useState } from "react"
import { Button } from "primereact/button"
import { Dialog } from "primereact/dialog"

import { LayerCreationTargets, LayerService, Layerslist, MapCoreData, OpenMapService } from 'mapcore-lib';
import QuerySecondaryDtmLayers from "../../openMap/layerSelector/querySecondaryDtmLayers"
import DeviceAndSpatialQueriesTabs from "../../shared/deviceAndSpatialQueriesTabs"
import SelectExistingTerrains from "../../shared/ControlsForMapcoreObjects/terrainCtrl/selectExistingTerrain"
import SpatialQueriesFooter from "../../treeView/mapWorldTree/viewportForms/spatialQueries/spatialQueriesFooter"
import SpatialQueriesForm from "../../treeView/mapWorldTree/viewportForms/spatialQueries/spatialQueriesForm"
import MapTreeNode from "../../../shared/models/map-tree-node.model"
import { AppState } from "../../../../redux/combineReducer"
import { runCodeSafely, runMapCoreSafely } from "../../../../common/services/error-handling/errorHandler"
import generalService from "../../../../services/general.service"
import mapWorldTreeService from "../../../../services/mapWorldTreeService"
import { setMapWorldTree } from "../../../../redux/MapWorldTree/mapWorldTreeActions"

export default function StandAloneSpatialQueries(props: { footerHook: (footer: () => ReactElement) => void }) {
    const dispatch = useDispatch();
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [selectedTerrains, setSelectedTerrains] = useState<{ mcTerrain: MapCore.IMcMapTerrain, isNew: boolean }[]>([]);
    const [sqCalculationsVisible, setSqCalculationsVisible] = useState(false);
    const [currentSpatialQueries, setCurrentSpatialQueries] = useState<MapCore.IMcSpatialQueries>(null);
    const [SCreateData, setSCreateData] = useState<MapCore.IMcMapViewport.SCreateData>(new MapCore.IMcMapViewport.SCreateData(MapCore.IMcMapCamera.EMapType.EMT_2D));
    //Secondary DTM
    const [secondaryDtmDialogVisible, setSecondaryDtmDialogVisible] = useState<boolean>(false);
    const [secondaryDtmArr, setSecondaryDtmArr] = useState<Layerslist>(new Layerslist([], []));

    useEffect(() => {
        const root = document.documentElement;
        let pixelWidth = window.innerHeight * 0.4 * globalSizeFactor;
        root.style.setProperty('--stand-alone-spatial-queries-dialog-width', `${pixelWidth}px`);

        props.footerHook(getFooter)
    }, [SCreateData, selectedTerrains, secondaryDtmArr])
    useEffect(() => {
        if (sqCalculationsVisible) {
            let buildedMapTree: MapTreeNode = mapWorldTreeService.buildTree();
            dispatch(setMapWorldTree(buildedMapTree));

            const root = document.documentElement;
            let pixelWidth = window.innerHeight * 8.5 * globalSizeFactor;
            root.style.setProperty('--stand-alone-spatial-queries-dialog-width', `${pixelWidth}px`);

            props.footerHook(getSqCalculationsFooter)
        }
    }, [sqCalculationsVisible])

    const getFooter = () => {
        return <div className='form__footer-padding' >
            <Button label='Create' onClick={handleCreateClick}></Button>
        </div>
    }
    const getSqCalculationsFooter = () => {
        return <SpatialQueriesFooter />;
    }
    const getSelectedTerrains = (selectedTerrains: { mcTerrain: MapCore.IMcMapTerrain, isNew: boolean }[]) => {
        runCodeSafely(() => {
            selectedTerrains.forEach(terrainObj => {
                if (terrainObj.isNew) {
                    MapCoreData.standAloneTerrains = MapCoreData.standAloneTerrains.filter(terrain => terrain != terrainObj.mcTerrain)
                }
            })
            setSelectedTerrains(selectedTerrains)
        }, 'standAloneSpatialQueries.getSelectedTerrains')
    }
    const getNewTerrains = () => {
        let newTerrains: MapCore.IMcMapTerrain[] = [];
        runCodeSafely(() => {
            selectedTerrains.forEach(terrainObj => {
                if (terrainObj.isNew) {
                    newTerrains = [...newTerrains, terrainObj.mcTerrain];
                }
            })
        }, 'standAloneSpatialQueries.getNewTerrains')
        return newTerrains;
    }

    const handleCreateClick = () => {
        runCodeSafely(() => {
            //Create data
            let sqSCreateData = new MapCore.IMcSpatialQueries.SCreateData();
            sqSCreateData.pCoordinateSystem = SCreateData.pCoordinateSystem;
            sqSCreateData.pDevice = MapCoreData.device;
            sqSCreateData.pOverlayManager = SCreateData.pOverlayManager;
            sqSCreateData.uViewportID = SCreateData.uViewportID;
            //Secondary DTM
            let secondaryDtmMcLayers = generalService.OpenMapService.createQuerySecondaryDtmLayers(secondaryDtmArr);
            //Spatial query creation
            let mcSelectedTerrains = selectedTerrains.map(terrain => terrain.mcTerrain);
            let mcSpatialQueries: MapCore.IMcSpatialQueries = null;
            runMapCoreSafely(() => {
                mcSpatialQueries = MapCore.IMcSpatialQueries.Create(sqSCreateData, mcSelectedTerrains, secondaryDtmMcLayers);
            }, 'standAloneSpatialQueries.handleCreateClick => IMcSpatialQueries.Create', true)
            LayerService.releaseStandAloneLayers(secondaryDtmMcLayers);
            selectedTerrains.forEach(terrainObj => {
                if (terrainObj.isNew)
                    runMapCoreSafely(() => { terrainObj.mcTerrain.Release() }, "standAloneSpatialQueries.filterNewTerrains => IMcBase.Release", true)
            })
            setCurrentSpatialQueries(mcSpatialQueries);
            setSqCalculationsVisible(true);
        }, 'standAloneSpatialQueries.handleCreateClick => onClick')
    }

    return <div>
        {sqCalculationsVisible ?
            <SpatialQueriesForm viewport={currentSpatialQueries} />
            :
            <div className="form__column-container">
                <DeviceAndSpatialQueriesTabs initialSCreateData={SCreateData} setSCreateData={(SCreateDataLocal) => { setSCreateData(SCreateDataLocal) }} />
                <br />
                <SelectExistingTerrains getSelectedTerrains={getSelectedTerrains} saveStandAlone={false} externalTerrains={getNewTerrains()} />
                <Button className="form__aligm-self-center" onClick={() => { setSecondaryDtmDialogVisible(true) }} label="Query Secondary Dtm Layers" />

                <Dialog visible={secondaryDtmDialogVisible} onHide={() => setSecondaryDtmDialogVisible(false)}>
                    <QuerySecondaryDtmLayers body={null} requestParams={[]} DTMlayerslist={secondaryDtmArr} getSelectedLayer={(DTMlayerslist: Layerslist) => {
                        setSecondaryDtmArr(DTMlayerslist)
                        setSecondaryDtmDialogVisible(false)
                    }} />
                </Dialog>
            </div>
        }
    </div>
}