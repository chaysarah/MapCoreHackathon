using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers;
using System.Xml.Serialization;
using System.IO;
using MCTester.MapWorld;
using System.Windows.Forms;
using MCTester.MapWorld.LoadingMapScheme;
using MCTester.Managers.MapWorld;
using MCTester.Managers.ObjectWorld;

namespace MCTester.Managers
{
    [Serializable]
    public class MapLoaderDefinitionClass
    {
        private MCTMapTerrains m_MapTerrains;
        private MCTMapLayers m_MapLayers;
        private MCTMapViewports m_Viewports;
        private MCTMapSchemas m_MapSchemas;
        private MCTMapDevices m_MapDevices;
        private MCTImageCalcs m_ImageCalcs;
        private MCTOverlayManagers m_OverlayManagers;
        private MCTGridCoordinateSystems m_GridCoordinateSystems;

        Dictionary<int, IDNMcMapTerrain> lstTerrainId = new Dictionary<int, IDNMcMapTerrain>();

        public MapLoaderDefinitionClass()
        {
            m_MapTerrains = new MCTMapTerrains();
            m_MapLayers = new MCTMapLayers();
            m_Viewports = new MCTMapViewports();
            m_MapSchemas = new MCTMapSchemas();
            m_MapDevices = new MCTMapDevices();
            m_ImageCalcs = new MCTImageCalcs();
            m_OverlayManagers = new MCTOverlayManagers();
            m_GridCoordinateSystems = new MCTGridCoordinateSystems();
        }

        public void LoadSchema(MCTMapSchema schema, bool isWpfWindow)
        {
            MCTGridCoordinateSystem overlayManagerCoordSys = null;
            IDNMcGridCoordinateSystem overlayManagerCoordSystem = null;
            IDNMcOverlayManager currSchemaOM = null;
            MCTOverlayManager currOverlayManager = null; 
            try
            {
                int[] viewportsIDs = schema.ViewportsIDs.ToArray();
                MCTMapViewport[] viewportsToLoad = new MCTMapViewport[viewportsIDs.Length];
                if (isWpfWindow == true)
                {
                    MainForm.MapLoaderDefinitionManager.MapDevices.Device[0].MultiThreadedDevice = true;
                }
                IDNMcMapDevice currSchemaDevice = MainForm.MapLoaderDefinitionManager.MapDevices.Device[0].CreateDevice();
                
                currOverlayManager = m_OverlayManagers.GetOverlayManager(schema.OverlayManagerID);
                if (currOverlayManager != null)
                {
                    overlayManagerCoordSys = m_GridCoordinateSystems.GetCoordSys(currOverlayManager.CoordSysID);
                    if (overlayManagerCoordSys != null)
                    {
                        overlayManagerCoordSystem = overlayManagerCoordSys.GetGridCoordSys();
                        currSchemaOM = currOverlayManager.CreateOverlayManager(overlayManagerCoordSystem);
                    }
                }

                List<IDNMcMapTerrain> lViewportTerrains = new List<IDNMcMapTerrain>();
                
                if (viewportsIDs.Length > 0)
                {
                    int vpID = viewportsIDs[0];

                    MCTMapViewport currViewport = m_Viewports.GetViewport(vpID);

                    if (currViewport != null)
                    {
                        IDNMcGridCoordinateSystem viewportCoordSystem = currOverlayManager!= null && currOverlayManager.CoordSysID == currViewport.CoordSysID  ? overlayManagerCoordSystem : null;
                        if (viewportCoordSystem == null)
                        {
                            MCTGridCoordinateSystem viewportCoordSys = m_GridCoordinateSystems.GetCoordSys(currViewport.CoordSysID);
                            if (viewportCoordSys != null)
                                viewportCoordSystem = viewportCoordSys.GetGridCoordSys();
                        }
                        lViewportTerrains = GetTerrainsOfViewport(currViewport, currViewport.CoordSysID, viewportCoordSystem);

                        IDNMcImageCalc currViewportImageCalc = null;

                        if (currViewport.ImageCalcID != -1)
                        {
                            MCTImageCalc viewportImageCalc = m_ImageCalcs.GetImageCalc(currViewport.ImageCalcID);
                            currViewportImageCalc = viewportImageCalc.CreateImageCalc();
                        }
                        
                        IDNMcMapViewport newViewport;
                        IDNMcMapCamera newCamera;

                       // currViewport.ID =(int) Manager_MCViewports.GetViewportCounter();
                        currViewport.CreateViewportMono(out newViewport,
                                                            out newCamera,
                                                            lViewportTerrains.ToArray(),
                                                            currViewportImageCalc,
                                                            currSchemaDevice,
                                                            currSchemaOM,
                                                            viewportCoordSystem,
                                                            isWpfWindow);

                        if (newViewport != null)
                        {
                            Manager_MCViewports.AddViewport(newViewport);

                            newCamera.SetCameraPosition(currViewport.CameraPosition, false);
                            if (newViewport.MapType == DNEMapType._EMT_3D)
                                newCamera.SetCameraClipDistances(1, 0, true);

                            // turn on viewport render needed flag
                            Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(newViewport);
                        }                        
                    }
                }

                for (int i = 1; i < viewportsIDs.Length; i++)
                {
                    int vpID = viewportsIDs[i];

                    MCTMapViewport currViewport = m_Viewports.GetViewport(vpID);

                    if (currViewport != null)
                    {
                        IDNMcGridCoordinateSystem viewportCoordSystem = currOverlayManager != null && currOverlayManager.CoordSysID == currViewport.CoordSysID ? overlayManagerCoordSystem : null;
                        if (viewportCoordSystem == null)
                        {
                            MCTGridCoordinateSystem viewportCoordSys = m_GridCoordinateSystems.GetCoordSys(currViewport.CoordSysID);
                            if (viewportCoordSys != null)
                                viewportCoordSystem = viewportCoordSys.GetGridCoordSys();
                        }

                        IDNMcImageCalc currViewportImageCalc = null;

                        if (currViewport.ImageCalcID != -1)
                        {
                            MCTImageCalc viewportImageCalc = m_ImageCalcs.GetImageCalc(currViewport.ImageCalcID);
                            currViewportImageCalc = viewportImageCalc.CreateImageCalc();
                        }
                        
                        IDNMcMapViewport newViewport;
                        IDNMcMapCamera newCamera;

                        lViewportTerrains = GetTerrainsOfViewport(currViewport, currViewport.CoordSysID, viewportCoordSystem);

                        currViewport.CreateViewportMono(out newViewport,
                                                            out newCamera,
                                                            lViewportTerrains.ToArray(),
                                                            currViewportImageCalc,
                                                            currSchemaDevice,
                                                            currSchemaOM,
                                                            viewportCoordSystem,
                                                            isWpfWindow);

                        if (newViewport != null)
                        {
                            Manager_MCViewports.AddViewport(newViewport);

                            newCamera.SetCameraPosition(currViewport.CameraPosition, false);
                            if (newViewport.MapType == DNEMapType._EMT_3D)
                                newCamera.SetCameraClipDistances(1, 0, true);

                            // turn on viewport render needed flag
                            Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(newViewport);
                        }
                    }
                }
                lstTerrainId.Clear();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Load Map Schema", McEx);
            }
        }


        private List<IDNMcMapTerrain> GetTerrainsOfViewport(MCTMapViewport currViewport, int vpCoordSys, IDNMcGridCoordinateSystem viewportCoordSys)
        {
            List<IDNMcMapTerrain> lViewportTerrains = new List<IDNMcMapTerrain>();
            int[] terrainsIDs = currViewport.TerrainsIDs.ToArray();
            MCTMapTerrain[] terrainsToLoad = new MCTMapTerrain[terrainsIDs.Length];

            foreach (int terrainID in terrainsIDs)
            {
                MCTMapTerrain currTerrain = m_MapTerrains.GetTerrain(terrainID);

                if (currTerrain != null)
                {
                    if (lstTerrainId.ContainsKey(terrainID))
                        lViewportTerrains.Add(lstTerrainId[terrainID]);
                    else
                    {
                        int[] layersIDs = currTerrain.LayersIDs.ToArray();
                        MCTMapLayer[] layersToLoad = new MCTMapLayer[layersIDs.Length];
                        List<IDNMcMapLayer> lTerrainLayers = new List<IDNMcMapLayer>();
                        foreach (int layerID in layersIDs)
                        {
                            MCTMapLayer currLayer = m_MapLayers.GetLayer(layerID);
                            IDNMcMapLayer newLayer = null;
                            MCTGridCoordinateSystem sourceRawVectorLayerCoordSys;
                            MCTGridCoordinateSystem targetRawVectorLayerCoordSys;
                            if (currLayer != null)
                            {
                                switch (currLayer.LayerType)
                                {
                                    case DNELayerType._ELT_NATIVE_DTM:
                                        newLayer = currLayer.CreateNativeDTMLayer();
                                        break;
                                    case DNELayerType._ELT_NATIVE_RASTER:
                                        newLayer = currLayer.CreateNativeRasterLayer();
                                        break;
                                    case DNELayerType._ELT_NATIVE_VECTOR:
                                        newLayer = currLayer.CreateNativeVectorLayer();
                                        break;
                                    case DNELayerType._ELT_NATIVE_VECTOR_3D_EXTRUSION:
                                        newLayer = currLayer.CreateNativeVector3DExtrusionLayer();
                                        break;
                                    case DNELayerType._ELT_NATIVE_3D_MODEL:
                                        newLayer = currLayer.CreateNative3DModelLayer();
                                        break;
                                    case DNELayerType._ELT_NATIVE_HEAT_MAP:
                                        newLayer = currLayer.CreateHeatMapLayer();
                                        break;
                                    case DNELayerType._ELT_NATIVE_MATERIAL:
                                        newLayer = currLayer.CreateMaterialLayer();
                                        break;
                                    case DNELayerType._ELT_NATIVE_TRAVERSABILITY:
                                        newLayer = currLayer.CreateTraversabilityLayer();
                                        break;
                                    case DNELayerType._ELT_RAW_DTM:
                                        IDNMcGridCoordinateSystem rawDtmCoordSys = currLayer.CoordSysID == vpCoordSys ? viewportCoordSys : null;

                                        MCTGridCoordinateSystem rawDtmLayerCoordSys = null;
                                        if (rawDtmCoordSys == null)
                                        {
                                            rawDtmLayerCoordSys = m_GridCoordinateSystems.GetCoordSys(currLayer.CoordSysID);
                                            if (rawDtmLayerCoordSys != null)
                                                rawDtmCoordSys = rawDtmLayerCoordSys.GetGridCoordSys();
                                        }
                                        newLayer = currLayer.CreateRawDtmLayer(rawDtmCoordSys);
                                        break;
                                    case DNELayerType._ELT_RAW_RASTER:
                                        IDNMcGridCoordinateSystem rawRasterCoordSys = currLayer.CoordSysID == vpCoordSys ? viewportCoordSys : null;

                                        MCTGridCoordinateSystem rawRasterLayerCoordSys = null;
                                        if (rawRasterCoordSys == null)
                                        {
                                            rawRasterLayerCoordSys = m_GridCoordinateSystems.GetCoordSys(currLayer.CoordSysID);
                                            if (rawRasterLayerCoordSys != null)
                                                rawRasterCoordSys = rawRasterLayerCoordSys.GetGridCoordSys();
                                        }
                                        newLayer = currLayer.CreateRawRasterLayer(rawRasterCoordSys);
                                        break;
                                    case DNELayerType._ELT_RAW_VECTOR:
                                        IDNMcGridCoordinateSystem coordSys = currLayer.CoordSysID == vpCoordSys ? viewportCoordSys : null;
                                        IDNMcGridCoordinateSystem targetCoordSys = currLayer.TargetCoordinateSystemID == vpCoordSys ? viewportCoordSys : null;

                                        if (coordSys == null)
                                        {
                                            MCTGridCoordinateSystem rawVectorLayerCoordSys = m_GridCoordinateSystems.GetCoordSys(currLayer.CoordSysID);
                                            if (rawVectorLayerCoordSys != null)
                                            {
                                                coordSys = rawVectorLayerCoordSys.GetGridCoordSys();
                                                
                                                if (targetCoordSys == null && currLayer.TargetCoordinateSystemID > 0)
                                                {
                                                    if (currLayer.TargetCoordinateSystemID == currLayer.CoordSysID)
                                                        targetCoordSys = coordSys;
                                                    else
                                                    {
                                                        targetRawVectorLayerCoordSys = m_GridCoordinateSystems.GetCoordSys(currLayer.TargetCoordinateSystemID);
                                                        if (targetRawVectorLayerCoordSys != null)
                                                            targetCoordSys = targetRawVectorLayerCoordSys.GetGridCoordSys();
                                                    }
                                                }
                                            }
                                        }
                                        newLayer = currLayer.CreateRawVectorLayer(coordSys, targetCoordSys);
                                        break;
                                    case DNELayerType._ELT_RAW_3D_MODEL:
                                        IDNMcGridCoordinateSystem raw3DModelCoordSys = currLayer.CoordSysID == vpCoordSys ? viewportCoordSys : null;
                                        MCTGridCoordinateSystem raw3DModelLayerCoordSys = null;
                                        if (raw3DModelCoordSys == null)
                                        {
                                            raw3DModelLayerCoordSys = m_GridCoordinateSystems.GetCoordSys(currLayer.CoordSysID);
                                            if (raw3DModelLayerCoordSys != null)
                                                raw3DModelCoordSys = raw3DModelLayerCoordSys.GetGridCoordSys();
                                        }
                                        newLayer = currLayer.CreateRaw3DModelLayer(raw3DModelCoordSys);
                                        break;
                                    case DNELayerType._ELT_RAW_VECTOR_3D_EXTRUSION:
                                        sourceRawVectorLayerCoordSys = m_GridCoordinateSystems.GetCoordSys(currLayer.SourceCoordinateSystemID);
                                        targetRawVectorLayerCoordSys = m_GridCoordinateSystems.GetCoordSys(currLayer.TargetCoordinateSystemID);

                                        newLayer = currLayer.CreateRawVector3DExtrusionLayer(
                                            sourceRawVectorLayerCoordSys != null ? sourceRawVectorLayerCoordSys.GetGridCoordSys() : null,
                                            targetRawVectorLayerCoordSys != null ? targetRawVectorLayerCoordSys.GetGridCoordSys() : null);
                                        break;
                                    case DNELayerType._ELT_NATIVE_SERVER_RASTER:
                                        newLayer = currLayer.CreateNativeServerRaster();
                                        break;
                                    case DNELayerType._ELT_NATIVE_SERVER_DTM:
                                        newLayer = currLayer.CreateNativeServerDTM();
                                        break;
                                    case DNELayerType._ELT_NATIVE_SERVER_VECTOR:
                                        newLayer = currLayer.CreateNativeServerVectorLayer();
                                        break;
                                    case DNELayerType._ELT_NATIVE_SERVER_VECTOR_3D_EXTRUSION:
                                        newLayer = currLayer.CreateNativeServerVector3DExtrusionLayer();
                                        break;
                                    case DNELayerType._ELT_NATIVE_SERVER_3D_MODEL:
                                        newLayer = currLayer.CreateNativeServer3DModelLayer();
                                        break;
                                    case DNELayerType._ELT_NATIVE_SERVER_TRAVERSABILITY:
                                        newLayer = currLayer.CreateNativeServerTraversabilityLayer();
                                        break;
                                    case DNELayerType._ELT_NATIVE_SERVER_MATERIAL:
                                        newLayer = currLayer.CreateNativeServerMaterialLayer();
                                        break;
                                    case DNELayerType._ELT_WEB_SERVICE_DTM:
                                        MCTGridCoordinateSystem WSDTMLayerCoordSys = m_GridCoordinateSystems.GetCoordSys(currLayer.CoordSysID);
                                        newLayer = currLayer.CreateWebServiceDTMLayer(WSDTMLayerCoordSys.GetGridCoordSys());
                                        break;
                                    case DNELayerType._ELT_WEB_SERVICE_RASTER:
                                        MCTGridCoordinateSystem WSRasterLayerCoordSys = m_GridCoordinateSystems.GetCoordSys(currLayer.CoordSysID);
                                        newLayer = currLayer.CreateWebServiceRasterLayer(WSRasterLayerCoordSys.GetGridCoordSys());

                                        break;
                                    default:
                                        if(currLayer.LayerType.ToString() == "_ELT_NATIVE_STATIC_OBJECTS")
                                            newLayer = currLayer.CreateNativeVector3DExtrusionLayer();
                                        break;
                                }
                                if (newLayer != null)
                                    lTerrainLayers.Add(newLayer);
                            }
                        }
                        IDNMcGridCoordinateSystem trnCoordSystem = currTerrain.CoordSysID == vpCoordSys ? viewportCoordSys : null;
                        MCTGridCoordinateSystem terrainCoordSys = null;
                        if (trnCoordSystem == null)
                        {
                            terrainCoordSys = m_GridCoordinateSystems.GetCoordSys(currTerrain.CoordSysID);
                            if (terrainCoordSys != null)
                                trnCoordSystem = terrainCoordSys.GetGridCoordSys();
                        }
                        IDNMcMapTerrain newTerrain = currTerrain.CreateTerrain(lTerrainLayers.ToArray(), trnCoordSystem);
                        lstTerrainId.Add(terrainID, newTerrain);
                        lViewportTerrains.Add(newTerrain);
                    }
                }

            }
            return lViewportTerrains;
        }

        public void CreateXMLFile(string folderName)
        {
            string fileName = @"\PrivateMapDefinitionsDatabase.xml";
            string tmpFileName = @"\TempPrivateMapDefinitionsDatabase.xml";

            string PathName = folderName + fileName;
            string PathTempName = folderName + tmpFileName;

            try
            {
                TextWriter SW = new StreamWriter(PathTempName);
                XmlSerializer Xser = new XmlSerializer(typeof(MapLoaderDefinitionClass));
                Xser.Serialize(SW, this);
                SW.Close();

                try
                {
                    if (File.Exists(PathName))
                        File.Delete(PathName);

                    File.Move(PathTempName, PathName);
                }

                catch (Exception ex)
                {
                    MessageBox.Show(String.Format("Error in file - " + PathName + ", can't save changes, reason - {0}", ex.Message));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Error open file - " + PathTempName + ", can't save changes , reason - {0}", ex.Message));
            }
        }

        public static MapLoaderDefinitionClass LoadXmlFile(string PathName)
        {
            MapLoaderDefinitionClass ret = null;
            try
            {
                StreamReader SR = new StreamReader(PathName);
                XmlSerializer Xser = new XmlSerializer(typeof(MapLoaderDefinitionClass));

                ret = (MapLoaderDefinitionClass)Xser.Deserialize(SR);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("LoadXmlFile", McEx);
            }
            catch (Exception ex)
            { MessageBox.Show(String.Format("Error open file - " + PathName + " , open defualt file, reason - {0}", ex.Message)); }

            return ret;
        }

        #region Public Properties

        public MCTMapTerrains Terrains
        {
            get { return m_MapTerrains; }
            set { m_MapTerrains = value; }
        }

        public MCTMapLayers Layers
        {
            get { return m_MapLayers; }
            set { m_MapLayers = value; }
        }

        public MCTMapViewports Viewports
        {
            get { return m_Viewports; }
            set { m_Viewports = value; }
        }
        
        public MCTMapSchemas Schemas
        {
            get { return m_MapSchemas; }
            set { m_MapSchemas = value; }
        }

        public MCTMapDevices MapDevices
        {
            get { return m_MapDevices; }
            set { m_MapDevices = value; }
        }

        public MCTImageCalcs ImageCalcs
        {
            get { return m_ImageCalcs; }
            set { m_ImageCalcs = value; }
        }

        public MCTGridCoordinateSystems GridCoordynateSystems
        {
            get { return m_GridCoordinateSystems; }
            set { m_GridCoordinateSystems = value; }
        }

        public MCTOverlayManagers OverlayManagers
        {
            get { return m_OverlayManagers; }
            set { m_OverlayManagers = value; }
        }        
        #endregion

    }
}
