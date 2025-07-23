using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;
using MCTester.MapWorld;
using MapCore.Common;
using System.Windows.Forms;
using MCTester.General_Forms;
using System.IO;
using MCTester.MapWorld.WebMapLayers;
using System.Net;
using System.Xml.Serialization;
using MCTester.GUI.Forms;
using System.Security;
using MCTester.MapWorld.WizardForms;
using System.Linq;
using MCTester.ButtonsImplementation;
using MCTester.Controls;


namespace MCTester.Managers.MapWorld
{
    public static class Manager_MCLayers
    {
        // dLayers contains standalone layers 
        // and created layers until they are added to terrains. after they are added, I removed them from this dic.
        private static Dictionary<IDNMcMapLayer, uint> dLayers;
        private static uint currentLayerID = 0;

        public static DNSWMTSParams mcCurrSWMTSParams = new DNSWMTSParams();
        public static DNSWMSParams mcCurrSWMSParams = new DNSWMSParams();
        public static DNSWCSParams mcCurrSWCSParams = new DNSWCSParams();

        public static DNSWebMapServiceParams mcCurrSWebMapServiceParams = new DNSWebMapServiceParams();
        public static DNSNonNativeParams mcCurrSNonNativeParams = new DNSNonNativeParams();
        public static DNEWebMapServiceType mcLastWMSLayerType = DNEWebMapServiceType._EWMS_WMTS;

        private static Dictionary<IDNMcMapLayer, int> mServerLayersPriority = new Dictionary<IDNMcMapLayer, int>();

        public static string UrlMapCoreServer ="http://localhost:6767/map/opr";
        public static string UrlWMTSServer = "http://localhost:6767/OGC/WMTS/1.0.0"; 
        public static string UrlWCSServer = "http://localhost:8080/geoserver/wcs";
        public static string UrlWMSServer = "http://localhost:8080/geoserver/wms";
        public static string UrlCSWServer = "";
        public static string UrlFlightHistoryServer = "";

        public static string RawVectorSplitStr = ">";
        public static char RawVectorSplitChar = '>';
        public static string RawVectorSingleSplitStr = "*";
        public static char RawVectorSingleSplitChar = '*';
        public static string RawVectorStylingSplitStr = "|";
        public static char RawVectorStylingSplitChar = '|';
        public static char RawVectorLayersSplitChar = ';';
        public static string RawVectorMultiple = "Multiple";

        public static string MultiSelection = "multi selection";

        public static string RawVector3DExtrusion_HeightColumnText = "height";
        public static string WMTSFromCSWType = "WMTS_SERVER_URL";

        public class MCTWebServerUserSelection
        {
            public string GroupName;
            public DNSServerLayerInfo ServerLayerInfo;
        }

        public class MCTServerSelectLayersData
        {
            public MCTWebServerUserSelection CSWSelectedLayer; // for WMTS from CSW
            public List<MCTWebServerUserSelection> SelectedLayers;
            public DNEWebMapServiceType WebMapServiceType;
            public DNSWebMapServiceParams WebMapServiceParams;
            public bool OpenAllLayersAsOne;
        }

        static Manager_MCLayers()
        {
            dLayers = new Dictionary<IDNMcMapLayer, uint>();
            mcCurrSWMTSParams.bUseServerTilingScheme = true;
        }

        #region IManagersGetter Members

        public static Dictionary<object, uint> AllParams
        {
            get
            {
                Dictionary<object, uint> ret = new Dictionary<object, uint>();

                //Add to the dictionary the standalone layers
                foreach (IDNMcMapLayer keyLayer in dLayers.Keys)
                {
                    ret.Add(keyLayer, dLayers[keyLayer]);
                }

                return ret;
            }
        }

        public static List<IDNMcMapLayer> AllLayers()
        {
            List<IDNMcMapLayer> lst = new List<IDNMcMapLayer>();
            List<IDNMcMapTerrain> terrainList = Manager_MCTerrain.AllTerrains();
            foreach (IDNMcMapTerrain terr in terrainList)
            {
                IDNMcMapLayer[] layersInTerr = terr.GetLayers();
                foreach (IDNMcMapLayer layer in layersInTerr)
                {
                    if (!lst.Contains(layer))
                        lst.Add(layer);
                }
            }

            foreach (IDNMcMapLayer layer in AllParams.Keys)
            {
                if (!lst.Contains(layer))
                    lst.Add(layer);
            }
            return lst;
        }

        public static Dictionary<object, uint> GetChildren(object Parent)
        {
            Dictionary<object, uint> Ret = new Dictionary<object, uint>();

            return Ret;
        }
        #endregion

        public static void AddLayer(IDNMcMapLayer layer)
        {
            dLayers.Add(layer, currentLayerID++);
        }

        public static void RemoveStandaloneLayer(IDNMcMapLayer layer)
        {
            if (dLayers.ContainsKey(layer))
            {
                dLayers.Remove(layer);
                //layer.Dispose();
            }
        }

        public static void AddLayerToServerLayersPriorityDic(IDNMcMapLayer layer, int priority)
        {
            // draw priority
            mServerLayersPriority.Add(layer, priority);
        }

        public static void RemoveLayerFromServerLayersPriorityDic(IDNMcMapLayer layer, IDNMcMapLayer newLayer = null)
        {
            int priority = 0;
            if (layer != null && mServerLayersPriority.ContainsKey(layer))
            {
                priority = mServerLayersPriority[layer];
                mServerLayersPriority.Remove(layer);
                if(newLayer != null)
                    mServerLayersPriority.Add(newLayer, priority);
            }
        }

        public static void ResetServerLayersPriorityDic()
        {
            // draw priority
            mServerLayersPriority.Clear();
        }

        public static void DrawPriorityServerLayer(IDNMcMapTerrain terrain)
        {
            if (mServerLayersPriority != null && mServerLayersPriority.Count > 0)
            {
                foreach (KeyValuePair<IDNMcMapLayer, int> layerPriority in mServerLayersPriority)
                {
                    DNSLayerParams layerParams = new DNSLayerParams();
                    layerParams.nDrawPriority = (sbyte)layerPriority.Value;
                    terrain.SetLayerParams(layerPriority.Key, layerParams);
                }
            }
            ResetServerLayersPriorityDic();
        }

        public static void RemoveLayerFromTester(IDNMcMapLayer layer, IDNMcMapLayer newLayer = null)
        {
            if (layer != null)
            {
                RemoveLayerFromServerLayersPriorityDic(layer, newLayer);
                NoticeLayerRemoved(layer);
                if (dLayers.ContainsKey(layer))
                {
                    dLayers.Remove(layer);
                    if (newLayer != null)
                        AddLayer(newLayer);
                }
                try
                {
                    layer.Dispose();
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("Dispose Layer", McEx);
                }
            }
            MainForm.RebuildMapWorldTree();
        }

        public static bool IsAllLayersInitialized(List<IDNMcMapLayer> layers)
        {
            foreach (IDNMcMapLayer mcMapLayer in layers)
            {
                if (!mcMapLayer.IsInitialized())
                    return false;
            }
            return true;
        }

        public static bool IsAllLayersInitialized(IDNMcMapLayer[] layers)
        {
            if (layers == null || layers.Length == 0) return true;
            else
                return IsAllLayersInitialized(layers.ToList());
        }


        public static void RemoveAllStandaloneLayers()
        {
            dLayers.Clear();
        }

        public static IDNMcMapLayer CreateNativeDTMLayer(string LayerFileName, uint NumLevelstoIgnore, DNSLocalCacheLayerParams? localCacheLayerParams, bool isAddLayer = true)
        {
            IDNMcMapLayer NewLayer = null;
            try
            {
                MCTMapLayerReadCallback layerReadCallback = MCTMapLayerReadCallback.getInstance();
                NewLayer = DNMcNativeDtmMapLayer.Create(LayerFileName, NumLevelstoIgnore, layerReadCallback, localCacheLayerParams);
                CheckingAfterCreateLayer(NewLayer, isAddLayer);

            }
            catch (MapCoreException McEx)
            {
                
                Utilities.ShowErrorMessage("NativeDTM Creation", "Folder name: " + LayerFileName, McEx);
            }
            return NewLayer;

        }

        public static IDNMcMapLayer CreateNativeServerDTMLayer(string LayerFileName, bool isAddLayer = true)
        {
            IDNMcMapLayer NewLayer = null;
            try
            {
                MCTMapLayerReadCallback layerReadCallback = MCTMapLayerReadCallback.getInstance();
                NewLayer = DNMcNativeServerDtmMapLayer.Create(LayerFileName, layerReadCallback);
                CheckingAfterCreateLayer(NewLayer, isAddLayer);

            }
            catch (MapCoreException McEx)
            {
                
                Utilities.ShowErrorMessage("NativeDTM Creation", McEx);
            }
                 return NewLayer;
       }

        public static IDNMcMapLayer CreateNative3DModelMapLayer(string LayerFileName, uint NumLevelsToIgnore, bool isAddLayer = true)
        {
            IDNMcMapLayer NewLayer = null;
            try
            {
                MCTMapLayerReadCallback layerReadCallback = MCTMapLayerReadCallback.getInstance();

                NewLayer = DNMcNative3DModelMapLayer.Create(LayerFileName, NumLevelsToIgnore, layerReadCallback);
                CheckingAfterCreateLayer(NewLayer, isAddLayer);

            }
            catch (MapCoreException McEx)
            {
                
                Utilities.ShowErrorMessage("DNMcNative3DModelMapLayer Creation", "Folder name: " + LayerFileName, McEx);
            }
                return NewLayer;
        }

        public static IDNMcMapLayer CreateNativeVector3DExtrusionMapLayer(string LayerFileName, uint NumLevelsToIgnore, float fExtrusionHeightMaxAddition, bool isAddLayer = true)
        {
            IDNMcMapLayer NewLayer = null;
            try
            {
                MCTMapLayerReadCallback layerReadCallback = MCTMapLayerReadCallback.getInstance();

                NewLayer = DNMcNativeVector3DExtrusionMapLayer.Create(LayerFileName, NumLevelsToIgnore, fExtrusionHeightMaxAddition, layerReadCallback);
                CheckingAfterCreateLayer(NewLayer, isAddLayer);

            }
            catch (MapCoreException McEx)
            {
                
                Utilities.ShowErrorMessage("DNMcNativeVector3DExtrusionMapLayer Creation", "Folder name: " + LayerFileName, McEx);
            }
                return NewLayer;
        }

       

        public static IDNMcMapLayer CreateNativeServerVector3DExtrusionLayer(string LayerFileName, bool isAddLayer = true)
        {
            IDNMcMapLayer NewLayer = null;
            try
            {
                MCTMapLayerReadCallback layerReadCallback = MCTMapLayerReadCallback.getInstance();
                NewLayer = DNMcNativeServerVector3DExtrusionMapLayer.Create(LayerFileName, layerReadCallback);
                CheckingAfterCreateLayer(NewLayer, isAddLayer);

            }
            catch (MapCoreException McEx)
            {
                
                Utilities.ShowErrorMessage("DNMcNativeServer3DModelMapLayer Creation", McEx);
            }
                 return NewLayer;
       }


        public static IDNMcMapLayer CreateNativeServer3DModelLayer(string LayerFileName, bool isAddLayer = true)
        {
            IDNMcMapLayer NewLayer = null;
            try
            {
                MCTMapLayerReadCallback layerReadCallback = MCTMapLayerReadCallback.getInstance();
                NewLayer = DNMcNativeServer3DModelMapLayer.Create(LayerFileName, layerReadCallback);
                CheckingAfterCreateLayer(NewLayer, isAddLayer);

            }
            catch (MapCoreException McEx)
            {
                
                Utilities.ShowErrorMessage("DNMcNativeServer3DModelMapLayer Creation", McEx);
            }
            return NewLayer;
        }

        public static IDNMcMapLayer CreateRaw3DModelLayer(string strRawDataDirectory, bool bOrthometricHeights, uint uNumLevelsToIgnore, string strIndexingDataDirectory, bool isAddLayer = true)
        {
            IDNMcMapLayer NewLayer = null;
            try
            {
                MCTMapLayerReadCallback layerReadCallback = MCTMapLayerReadCallback.getInstance();
                NewLayer = DNMcRaw3DModelMapLayer.Create(strRawDataDirectory, bOrthometricHeights, uNumLevelsToIgnore, layerReadCallback, strIndexingDataDirectory);
                CheckingAfterCreateLayer(NewLayer, isAddLayer);

            }
            catch (MapCoreException McEx)
            {
                
                Utilities.ShowErrorMessage("DNMcRaw3DModelMapLayer Creation", McEx);
            }
                 return NewLayer;
       }

        public static IDNMcMapLayer CreateRaw3DModelLayer(string strRawDataDirectory, IDNMcGridCoordinateSystem gridCoordinateSystem, bool bOrthometricHeights, DNSMcBox? pClipRect, float fTargetHighestResolution, DNSMcKeyStringValue[] aRequestParams, bool isAddLayer = true)
        {
            return CreateRaw3DModelLayer(strRawDataDirectory, gridCoordinateSystem, bOrthometricHeights, pClipRect, fTargetHighestResolution, aRequestParams, DNSMcVector3D.v3Zero, isAddLayer);
        }

        public static IDNMcMapLayer CreateRaw3DModelLayer(string strRawDataDirectory, IDNMcGridCoordinateSystem gridCoordinateSystem, bool bOrthometricHeights, DNSMcBox? pClipRect, float fTargetHighestResolution, DNSMcKeyStringValue[] aRequestParams, DNSMcVector3D PositionOffset, bool isAddLayer = true)
        {
            IDNMcMapLayer NewLayer = null;
            try
            {
                MCTMapLayerReadCallback layerReadCallback = MCTMapLayerReadCallback.getInstance();
                NewLayer = DNMcRaw3DModelMapLayer.Create(strRawDataDirectory, gridCoordinateSystem, bOrthometricHeights, pClipRect, fTargetHighestResolution, layerReadCallback, aRequestParams, PositionOffset);
                CheckingAfterCreateLayer(NewLayer, isAddLayer);

            }
            catch (MapCoreException McEx)
            {
                
                Utilities.ShowErrorMessage("DNMcRaw3DModelMapLayer Creation", McEx);
            }
                return NewLayer;
        }

        public static IDNMcMapLayer CreateRawVector3DExtrusionLayer(string strDataSource, DNSRawVector3DExtrusionGraphicalParams Params, float fExtrusionHeightMaxAddition, string strIndexingDataDirectory, bool isAddLayer = true)
        {
            IDNMcMapLayer NewLayer = null;
            try
            {
                MCTMapLayerReadCallback layerReadCallback = MCTMapLayerReadCallback.getInstance();

                NewLayer = DNMcRawVector3DExtrusionMapLayer.Create(strDataSource, Params, fExtrusionHeightMaxAddition, layerReadCallback, strIndexingDataDirectory);
                CheckingAfterCreateLayer(NewLayer, isAddLayer);

            }
            catch (MapCoreException McEx)
            {
                
                Utilities.ShowErrorMessage("DNMcRawVector3DExtrusionMapLayer Creation", McEx);
            }
                return NewLayer;
        }

        public static IDNMcMapLayer CreateRawVector3DExtrusionLayer(DNSRawVector3DExtrusionParams rawVector3DExtrusionParams, float fExtrusionHeightMaxAddition, bool isAddLayer = true)
        {
            IDNMcMapLayer NewLayer = null;
            try
            {
                MCTMapLayerReadCallback layerReadCallback = MCTMapLayerReadCallback.getInstance();

                NewLayer = DNMcRawVector3DExtrusionMapLayer.Create(rawVector3DExtrusionParams, fExtrusionHeightMaxAddition, layerReadCallback);
                CheckingAfterCreateLayer(NewLayer, isAddLayer);

            }
            catch (MapCoreException McEx)
            {
                
                Utilities.ShowErrorMessage("DNMcRawVector3DExtrusionMapLayer Creation", McEx);
            }
                return NewLayer;
        }

        public static IDNMcMapLayer CreateNativeVectorLayer(string LayerFileName, DNSLocalCacheLayerParams? localCacheLayerParams, bool isAddLayer = true)
        {
            IDNMcMapLayer NewLayer = null;
            try
            {
                MCTMapLayerReadCallback layerReadCallback = MCTMapLayerReadCallback.getInstance();
                NewLayer = DNMcNativeVectorMapLayer.Create(LayerFileName, layerReadCallback, localCacheLayerParams);
                CheckingAfterCreateLayer(NewLayer, isAddLayer);

            }
            catch (MapCoreException McEx)
            {
                
                Utilities.ShowErrorMessage("NativeVector Creation", "Folder name: " + LayerFileName, McEx);
            }
                return NewLayer;
        }

        public static IDNMcMapLayer CreateNativeServerVectorLayer(string LayerFileName, bool isAddLayer = true)
        {
            IDNMcMapLayer NewLayer = null;
            try
            {
                MCTMapLayerReadCallback layerReadCallback = MCTMapLayerReadCallback.getInstance();
                NewLayer = DNMcNativeServerVectorMapLayer.Create(LayerFileName, layerReadCallback);
                CheckingAfterCreateLayer(NewLayer, isAddLayer);

            }
            catch (MapCoreException McEx)
            {
                
                Utilities.ShowErrorMessage("Native Server Vector Creation", McEx);
            }
            return NewLayer;
        }

        public static IDNMcMapLayer CreateNativeRasterLayer(string LayerFileName,
            uint FirstLowerQualityLevel,
            bool ThereAreMissingFiles,
            uint NumLevelstoIgnore,
            bool bEnhanceBorderOverlap,
            DNSLocalCacheLayerParams? localCacheLayerParams, bool isAddLayer = true)
        {
            IDNMcMapLayer NewLayer = null;
            try
            {
                MCTMapLayerReadCallback layerReadCallback = MCTMapLayerReadCallback.getInstance();
                NewLayer = DNMcNativeRasterMapLayer.Create(LayerFileName, FirstLowerQualityLevel, ThereAreMissingFiles, NumLevelstoIgnore, bEnhanceBorderOverlap, layerReadCallback, localCacheLayerParams);
                CheckingAfterCreateLayer(NewLayer, isAddLayer);

            }
            catch (MapCoreException McEx)
            {
                
                Utilities.ShowErrorMessage("Native Raster Creation","Folder name: " + LayerFileName,  McEx );
            }
                 return NewLayer;
       }

        public static IDNMcMapLayer CreateNativeServerRasterLayer(string LayerFileName, bool isAddLayer = true)
        {
            IDNMcMapLayer NewLayer = null;
            try
            {
                MCTMapLayerReadCallback layerReadCallback = MCTMapLayerReadCallback.getInstance();
                NewLayer = DNMcNativeServerRasterMapLayer.Create(LayerFileName, layerReadCallback);
                CheckingAfterCreateLayer(NewLayer, isAddLayer);

            }
            catch (MapCoreException McEx)
            {
                
                Utilities.ShowErrorMessage("Native Server Raster Creation", McEx);
            }
                return NewLayer;
        }

        public static IDNMcMapLayer CreateNativeHeatMapLayer(string LayerFileName,
            uint FirstLowerQualityLevel,
            bool ThereAreMissingFiles,
            uint NumLevelstoIgnore,
            bool bEnhanceBorderOverlap, 
            DNSLocalCacheLayerParams? localCacheLayerParams,
            bool isAddLayer = true)
        {
            IDNMcMapLayer NewLayer = null;
            try
            {
                MCTMapLayerReadCallback layerReadCallback = MCTMapLayerReadCallback.getInstance();
                NewLayer = DNMcNativeHeatMapLayer.Create(LayerFileName, FirstLowerQualityLevel, ThereAreMissingFiles, NumLevelstoIgnore, bEnhanceBorderOverlap, layerReadCallback);
                CheckingAfterCreateLayer(NewLayer, isAddLayer);

            }
            catch (MapCoreException McEx)
            {
                
                Utilities.ShowErrorMessage("NativeRaster Creation", "Folder name: " + LayerFileName, McEx);
            }
                return NewLayer;
        }

      
        public static IDNMcMapLayer CreateNativeTraversabilityLayer(string LayerFileName, bool isAddLayer = true)
        {
            IDNMcMapLayer NewLayer = null;
            try
            {
                MCTMapLayerReadCallback layerReadCallback = MCTMapLayerReadCallback.getInstance();
                NewLayer = DNMcNativeTraversabilityMapLayer.Create(LayerFileName, false, layerReadCallback);
                CheckingAfterCreateLayer(NewLayer, isAddLayer);

            }
            catch (MapCoreException McEx)
            {
                
                Utilities.ShowErrorMessage("Traversability Creation", "Folder name: " + LayerFileName, McEx);
            }
            return NewLayer;
        }

        public static IDNMcMapLayer CreateNativeMaterialLayer(string LayerFileName, bool ThereAreMissingFiles,
            DNSLocalCacheLayerParams? localCacheLayerParams,
            bool isAddLayer = true)
        {
            IDNMcMapLayer NewLayer = null;
            try
            {
                MCTMapLayerReadCallback layerReadCallback = MCTMapLayerReadCallback.getInstance();
                NewLayer = DNMcNativeMaterialMapLayer.Create(LayerFileName, ThereAreMissingFiles, layerReadCallback, localCacheLayerParams);
                CheckingAfterCreateLayer(NewLayer, isAddLayer);

            }
            catch (MapCoreException McEx)
            {
                
                Utilities.ShowErrorMessage("Material Creation", "Folder name: " + LayerFileName, McEx);
            }
                return NewLayer;
        }

        public static IDNMcMapLayer CreateNativeTraversabilityLayer(string LayerFileName, bool ThereAreMissingFiles,
            DNSLocalCacheLayerParams? localCacheLayerParams,
            bool isAddLayer = true)
        {
            IDNMcMapLayer NewLayer = null;
            try
            {
                MCTMapLayerReadCallback layerReadCallback = MCTMapLayerReadCallback.getInstance();
                NewLayer = DNMcNativeTraversabilityMapLayer.Create(LayerFileName, ThereAreMissingFiles, layerReadCallback, localCacheLayerParams);
                CheckingAfterCreateLayer(NewLayer, isAddLayer);

            }
            catch (MapCoreException McEx)
            {
                
                Utilities.ShowErrorMessage("Traversability Creation", "Folder name: " + LayerFileName, McEx);
            }
                return NewLayer;
        }

        public static IDNMcMapLayer CreateNativeServerTraversabilityLayer(string LayerFileName, bool isAddLayer = true)
        {
            IDNMcMapLayer NewLayer = null;
            try
            {
                MCTMapLayerReadCallback layerReadCallback = MCTMapLayerReadCallback.getInstance();
                NewLayer = DNMcNativeServerTraversabilityMapLayer.Create(LayerFileName, layerReadCallback);
                CheckingAfterCreateLayer(NewLayer, isAddLayer);

            }
            catch (MapCoreException McEx)
            {
                
                Utilities.ShowErrorMessage("Traversability Creation", "Folder name: " + LayerFileName, McEx);
            }
                 return NewLayer;
       }

        public static IDNMcMapLayer CreateNativeServerMaterialLayer(string LayerFileName, bool isAddLayer = true)
        {
            IDNMcMapLayer NewLayer = null;
            try
            {
                MCTMapLayerReadCallback layerReadCallback = MCTMapLayerReadCallback.getInstance();
                NewLayer = DNMcNativeServerMaterialMapLayer.Create(LayerFileName, layerReadCallback);
                CheckingAfterCreateLayer(NewLayer, isAddLayer);

            }
            catch (MapCoreException McEx)
            {
                
                Utilities.ShowErrorMessage("Material Creation", "Folder name: " + LayerFileName, McEx);
            }
                 return NewLayer;
       }

        public static IDNMcMapLayer CreateRawDTMLayer(
           DNSRawParams dnParams,
           DNSLocalCacheLayerParams? localCacheLayerParams,
           bool isAddLayer = true)
        {
            IDNMcMapLayer NewLayer = null;
            try
            {
                MCTMapLayerReadCallback.SetMapLayerReadCallback(dnParams);
                NewLayer = DNMcRawDtmMapLayer.Create(dnParams, localCacheLayerParams);
                CheckingAfterCreateLayer(NewLayer, isAddLayer);

            }
            catch (MapCoreException McEx)
            {
                
                Utilities.ShowErrorMessage("Raw Dtm Creation", McEx);
            }
                return NewLayer;
        }

        public static IDNMcMapLayer CreateRawRasterLayer(
            DNSRawParams dnParams,
            bool imageCoordSys,
            DNSLocalCacheLayerParams? localCacheLayerParams,
            bool isAddLayer = true)
        {
            IDNMcMapLayer NewLayer = null;
            try
            {
                MCTMapLayerReadCallback.SetMapLayerReadCallback(dnParams);
                NewLayer = DNMcRawRasterMapLayer.Create(dnParams, imageCoordSys, localCacheLayerParams);
                CheckingAfterCreateLayer(NewLayer, isAddLayer);

            }
            catch (MapCoreException McEx)
            {
                
                Utilities.ShowErrorMessage("Raw Raster Creation", McEx);
            }
                 return NewLayer;
       }

     

        public static IDNMcMapLayer CreateRawMaterialLayer(
            DNSRawParams dnParams,
            DNSLocalCacheLayerParams? localCacheLayerParams,
            bool isAddLayer = true)
        {
            IDNMcMapLayer NewLayer = null;
            try
            {
                MCTMapLayerReadCallback.SetMapLayerReadCallback(dnParams);
                NewLayer = DNMcRawMaterialMapLayer.Create(dnParams, localCacheLayerParams);
                CheckingAfterCreateLayer(NewLayer, isAddLayer);

            }
            catch (MapCoreException McEx)
            {
                
                Utilities.ShowErrorMessage("Raw Material Creation", McEx);
            }
                 return NewLayer;
       }

        public static IDNMcMapLayer CreateRawTraversabilityLayer(
            DNSRawParams dnParams,
            DNSLocalCacheLayerParams? localCacheLayerParams,
            bool isAddLayer = true)
        {
            IDNMcMapLayer NewLayer = null;
            try
            {
                MCTMapLayerReadCallback.SetMapLayerReadCallback(dnParams);
                NewLayer = DNMcRawTraversabilityMapLayer.Create(dnParams, localCacheLayerParams);
                CheckingAfterCreateLayer(NewLayer, isAddLayer);

            }
            catch (MapCoreException McEx)
            {
                
                Utilities.ShowErrorMessage("Raw Traversability Creation", McEx);
            }
                return NewLayer;
        }

        public static List<string> CheckMultiRawVectorLayer(string strLayerRawVector )
        {
            List<string> layers = new List<string>();
            try
            {
                if (strLayerRawVector.Contains(RawVectorSplitStr))
                {
                    string[] data = strLayerRawVector.Split(RawVectorSplitChar);
                    if (data.Length > 1)
                    {
                        string strDataSource = data[0];
                        string[] dataLayers = data[1].TrimEnd(RawVectorLayersSplitChar).Split(Manager_MCLayers.RawVectorLayersSplitChar);

                        foreach (string dataLayer in dataLayers)
                        {
                            if (dataLayer != "")
                            {
                                layers.Add(strDataSource + dataLayer);
                            }
                        }
                    }
                }
                else
                    layers.Add(strLayerRawVector);
            }
            catch (Exception)
            {
                
            }
            return layers;
        }

        public static IDNMcMapLayer CreateRawVectorLayer(
            DNSRawVectorParams rawVectorParams,
            IDNMcGridCoordinateSystem gridCoordSys,
            DNSTilingScheme tilingScheme,
            DNSLocalCacheLayerParams? localCacheLayerParams,
            bool isAddLayer = true)
        {
            IDNMcMapLayer NewLayer = null;
            try
            {
                MCTMapLayerReadCallback layerReadCallback = MCTMapLayerReadCallback.getInstance();
                NewLayer = DNMcRawVectorMapLayer.Create(
                    rawVectorParams,
                    gridCoordSys,
                    tilingScheme,
                    layerReadCallback,
                    localCacheLayerParams);
                CheckingAfterCreateLayer(NewLayer, isAddLayer);

            }
            catch (MapCoreException McEx)
            {
                
                Utilities.ShowErrorMessage("RawVector Creation", McEx);
            }
                 return NewLayer;
       }

        internal static IDNMcMapLayer CreateWebServiceRasterLayer(DNSWMSParams layerParams, DNSLocalCacheLayerParams? localCacheLayerParams, bool isAddLayer = true)
        {
            IDNMcMapLayer NewLayer = null;
            try
            {
                MCTMapLayerReadCallback.SetMapLayerReadCallback(layerParams);
                NewLayer = DNMcWebServiceRasterMapLayer.Create(layerParams, localCacheLayerParams);
                CheckingAfterCreateLayer(NewLayer, isAddLayer);

            }
            catch (MapCoreException McEx)
            {
                
                Utilities.ShowErrorMessage("WebServiceRaster Creation", McEx);
            }
                  return NewLayer;
      }

        internal static IDNMcMapLayer CreateWebServiceRasterLayer(DNSWMTSParams layerParams, DNSLocalCacheLayerParams? localCacheLayerParams, bool isAddLayer = true)
         {
            IDNMcMapLayer NewLayer = null;
            try
            {
                MCTMapLayerReadCallback.SetMapLayerReadCallback(layerParams);
                NewLayer = DNMcWebServiceRasterMapLayer.Create(layerParams, localCacheLayerParams);
                CheckingAfterCreateLayer(NewLayer, isAddLayer);

            }
            catch (MapCoreException McEx)
            {
                
                Utilities.ShowErrorMessage("WMTSRaster Creation", McEx);
            }
                 return NewLayer;
       }

        internal static IDNMcMapLayer CreateWebServiceRasterLayer(DNSWCSParams layerParams, DNSLocalCacheLayerParams? localCacheLayerParams, bool isAddLayer = true)
        {
            IDNMcMapLayer NewLayer = null;
            try
            {
                MCTMapLayerReadCallback.SetMapLayerReadCallback(layerParams);
                NewLayer = DNMcWebServiceRasterMapLayer.Create(layerParams, localCacheLayerParams);
                CheckingAfterCreateLayer(NewLayer, isAddLayer);
            }
            catch (MapCoreException McEx)
            {
                
                Utilities.ShowErrorMessage("WCSRaster Creation", McEx);
            }
            return NewLayer;
        }

        public static void CheckingAfterCreateLayer(IDNMcMapLayer NewLayer, bool isAddLayer)
        {
            MCTMapLayerReadCallback.CheckIfNewLayerExistInDontShowErrorMsg(NewLayer.GetBaseUnmanagedPtr());
            if (isAddLayer)
                AddLayer(NewLayer);
        }

        public static void CheckingAfterLoadTerrain(IDNMcMapTerrain loadedTerrain, bool isAddTerrain = false)
        {
            if (loadedTerrain != null)
            {
                foreach (IDNMcMapLayer mcMapLayer in loadedTerrain.GetLayers())
                {
                    CheckingAfterCreateLayer(mcMapLayer, false);
                }

                if (isAddTerrain)
                    Manager_MCTerrain.AddTerrain(loadedTerrain);
            }
        }

        internal static IDNMcMapLayer CreateWebServiceDtmLayer(DNSWMSParams layerParams, DNSLocalCacheLayerParams? localCacheLayerParams, bool isAddLayer = true)
        {
            IDNMcMapLayer NewLayer = null;
            try
            {
                MCTMapLayerReadCallback.SetMapLayerReadCallback(layerParams);
                NewLayer = DNMcWebServiceDtmMapLayer.Create(layerParams, localCacheLayerParams);
                CheckingAfterCreateLayer(NewLayer, isAddLayer);
            }
            catch (MapCoreException McEx)
            {
                
                Utilities.ShowErrorMessage("WebServiceDtm Creation", McEx);
            }
                 return NewLayer;
       }

        internal static IDNMcMapLayer CreateWebServiceDtmLayer(DNSWMTSParams layerParams, DNSLocalCacheLayerParams? localCacheLayerParams, bool isAddLayer = true)
        {
            IDNMcMapLayer NewLayer = null;
            try
            {
                MCTMapLayerReadCallback.SetMapLayerReadCallback(layerParams);
                NewLayer = DNMcWebServiceDtmMapLayer.Create(layerParams, localCacheLayerParams);
                CheckingAfterCreateLayer(NewLayer, isAddLayer);

            }
            catch (MapCoreException McEx)
            {
                
                Utilities.ShowErrorMessage("WMTSDtm Creation", McEx);
            }
                 return NewLayer;
       }

        internal static IDNMcMapLayer CreateWebServiceDtmLayer(DNSWCSParams layerParams, DNSLocalCacheLayerParams? localCacheLayerParams, bool isAddLayer = true)
        {
            IDNMcMapLayer NewLayer = null;
            try
            {
                MCTMapLayerReadCallback.SetMapLayerReadCallback(layerParams);
                NewLayer = DNMcWebServiceDtmMapLayer.Create(layerParams, localCacheLayerParams);
                CheckingAfterCreateLayer(NewLayer, isAddLayer);

            }
            catch (MapCoreException McEx)
            {
                
                Utilities.ShowErrorMessage("WCSDtm Creation", McEx);
            }
                 return NewLayer;
       }

        public static String stringDelimiter = "----------------------------------------------------------------------------";
        static int[] delimeterIndexes = new int[2] { 8, 16 };

        public static void LoadCmbLayers(ComboBox comboBox, bool filterDTMLayer = false)
        {
            comboBox.Items.Clear(); 
            comboBox.Items.AddRange(GetLayerNames(filterDTMLayer));
            comboBox.Text = DNELayerType._ELT_NATIVE_RASTER.ToString().Replace(LayerTypePrefix, "");
        }

        public static string[] GetLayerNames(bool filterDTMLayer = false)
        {
            string[] names = new string[]
            {
                DNELayerType._ELT_NATIVE_RASTER.ToString(),                 // 0
                DNELayerType._ELT_NATIVE_DTM.ToString(),
                DNELayerType._ELT_NATIVE_VECTOR.ToString(),
                DNELayerType._ELT_NATIVE_VECTOR_3D_EXTRUSION.ToString(),
                DNELayerType._ELT_NATIVE_3D_MODEL.ToString(),
                DNELayerType._ELT_NATIVE_HEAT_MAP.ToString(),               // 5
                DNELayerType._ELT_NATIVE_MATERIAL.ToString(),
                DNELayerType._ELT_NATIVE_TRAVERSABILITY.ToString(),
                stringDelimiter,
                DNELayerType._ELT_RAW_RASTER.ToString(),
                DNELayerType._ELT_RAW_DTM.ToString(),                       // 10
                DNELayerType._ELT_RAW_VECTOR.ToString(),
                DNELayerType._ELT_RAW_VECTOR_3D_EXTRUSION.ToString(),
                DNELayerType._ELT_RAW_3D_MODEL.ToString(),
                DNELayerType._ELT_RAW_MATERIAL.ToString(),
                DNELayerType._ELT_RAW_TRAVERSABILITY.ToString(),            // 15
                DNELayerType._ELT_WEB_SERVICE_RASTER.ToString(),
                DNELayerType._ELT_WEB_SERVICE_DTM.ToString(),
                stringDelimiter,
                DNELayerType._ELT_NATIVE_SERVER_RASTER.ToString(),
                DNELayerType._ELT_NATIVE_SERVER_DTM.ToString(),             // 20
                DNELayerType._ELT_NATIVE_SERVER_VECTOR.ToString(),
                DNELayerType._ELT_NATIVE_SERVER_VECTOR_3D_EXTRUSION.ToString(),
                DNELayerType._ELT_NATIVE_SERVER_3D_MODEL.ToString(),
                DNELayerType._ELT_NATIVE_SERVER_TRAVERSABILITY.ToString(),
                DNELayerType._ELT_NATIVE_SERVER_MATERIAL.ToString()         // 25
            };
            if (filterDTMLayer)
            {
                List<string> lstNames = new List<string>(names);
                names = lstNames.FindAll(x => x.ToLower().Contains("dtm")).ToArray();
            }
            for (int i = 0; i < names.Length; i++)
            {
                names[i] = names[i].Replace(LayerTypePrefix, "");
            }
            return names;
        }

        public static string LayerTypePrefix = "_ELT_";

        public static string CheckRawVector(string filename, bool isEnableStyling  , bool isMultiSelect = true)
        {
            string result = filename;
            try
            {
                if (/*Path.GetExtension(filename) != "" &&*/ !filename.Contains("*"))
                {
                    DNSDataSourceSubLayersProperties vectorDataSourceProperties = DNMcRawVectorMapLayer.GetDataSourceSubLayersProperties(filename, true/*bSuffixWithLayerName*/);
                    if (vectorDataSourceProperties.aLayersProperties.Length > 1)
                    {
                        frmRawVectorMultiLayers frmRawVectorMultiLayers = new frmRawVectorMultiLayers(filename, isEnableStyling, isMultiSelect);
                        if (frmRawVectorMultiLayers.ShowDialog() == DialogResult.OK)
                        {
                            result = frmRawVectorMultiLayers.SelectLayers;
                        }
                    }
                }
            }
            catch (MapCoreException )
            {
                // comment this msg if user change or write file path, not show error msg
                //Utilities.ShowErrorMessage("GetDataSourceSubLayersProperties", McEx);
            }
            catch(Exception )
            {

            }
            return result;
        }

        public static Capabilities LoadWebLayers(string serverPath)
        {
            Capabilities capabilities = null;
            UrlMapCoreServer = serverPath.Trim();
            try
            {
                string sURL = serverPath.Trim() + "?service=wmts&request=GetCapabilities";   // http://localhost:6767/wmtsCapabilities.xml
                WebRequest wrGETURL= WebRequest.Create(sURL);
                wrGETURL.ContentType = "text/xml";

                try
                {
                    string pstrToken = "";
                    string pstrSessionID = "";
                    DNMcMapLayer.GetNativeServerCredentials(ref pstrToken, ref pstrSessionID);
                    wrGETURL.Headers["Token"] = pstrToken;
                    wrGETURL.Headers["SessionId"] = pstrSessionID;
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("DNMcMapLayer.GetNativeServerCredentials", McEx);
                }               

                Stream objStream = wrGETURL.GetResponse().GetResponseStream();
                StreamReader objReader = new StreamReader(objStream);
                XmlSerializer Xser = new XmlSerializer(typeof(Capabilities));
                capabilities = (Capabilities)Xser.Deserialize(objReader);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Get Capabilities From Server");
            }
            catch (NotSupportedException ex)
            {
                MessageBox.Show(ex.Message, "Get Capabilities From Server");
            }
            catch(UriFormatException ex)
            {
                MessageBox.Show(ex.Message, "Get Capabilities From Server");
            }
            catch (SecurityException ex)
            {
                MessageBox.Show(ex.Message, "Get Capabilities From Server");
            }
            return capabilities;
        }

        public static void NoticeLayerRemoved(IDNMcMapLayer layerToRemove)
        {
            foreach(SpatialQueriesForm form in ListSpatialQueriesForms)
            {
                form.NoticeLayerRemoved(layerToRemove);
            }

            foreach (AddLayerForm layerForm in lstAddLayerForms)
            {
                layerForm.NoticeLayerRemoved(layerToRemove);
            }

          /*  foreach (AddLayerCtrl layerCtrl in lstAddLayerCtrls)
            {
                layerCtrl.NoticeLayerRemoved(layerToRemove);
            }*/

            foreach (TerrainWizzardForm terrainForm in lstTerrainWizzardForms)
            {
                terrainForm.NoticeLayerRemoved(layerToRemove);
            }

            foreach (CtrlLayers ctrlLayers in lstCtrlLayers)
            {
                ctrlLayers.NoticeLayerRemoved(layerToRemove);
            }
        }

        public static IDNMcDtmMapLayer[] GetLayersAsDTM(List<IDNMcMapLayer> layers)
        {
            IDNMcDtmMapLayer[] DTMLayers = null;
            int i = 0;
            if (layers != null && layers.Count > 0)
            {
                DTMLayers = new IDNMcDtmMapLayer[layers.Count];
                foreach (IDNMcMapLayer layer in layers)
                {
                    if(layer is IDNMcDtmMapLayer)
                        DTMLayers[i++] = ((IDNMcDtmMapLayer)layer);
                }
            }
            return DTMLayers;
        }


        private static List<SpatialQueriesForm> lstSpatialQueriesForms = new List<SpatialQueriesForm>();

        public static List<SpatialQueriesForm> ListSpatialQueriesForms
        {
            get { return lstSpatialQueriesForms; }
        }


        // save opened forms to handle if layer return from OnInitilized with failure
        // so remove the layer from AddLayerForm and update TerrainWizard
        private static List<AddLayerForm> lstAddLayerForms = new List<AddLayerForm>();
        private static List<CtrlLayers> lstCtrlLayers = new List<CtrlLayers>();

      //  private static List<AddLayerCtrl> lstAddLayerCtrls = new List<AddLayerCtrl>();

        public static List<AddLayerForm> ListAddLayerForms
        {
            get { return lstAddLayerForms; }
        }

        public static void AddLayerFormToList(AddLayerForm layerForm)
        {
            lstAddLayerForms.Add(layerForm);
        }

        public static void AddCtrlLayersToList(CtrlLayers ctrlLayers)
        {
            lstCtrlLayers.Add(ctrlLayers);
        }

        public static void RemoveCtrlLayersFromList(CtrlLayers ctrlLayers)
        {
            if (lstCtrlLayers.Contains(ctrlLayers))
                lstCtrlLayers.Remove(ctrlLayers);
        }

        /*public static void AddLayerCtrlToList(AddLayerCtrl layerCtrl)
        {
            lstAddLayerCtrls.Add(layerCtrl);
        }*/

        public static void RemoveLayerFormFromList(AddLayerForm layerForm)
        {
            if(lstAddLayerForms.Contains(layerForm))
                lstAddLayerForms.Remove(layerForm);
        }

        private static List<TerrainWizzardForm> lstTerrainWizzardForms = new List<TerrainWizzardForm>();
        public static List<TerrainWizzardForm> ListTerrainWizzardForms
        {
            get { return lstTerrainWizzardForms; }
        }

        public static void AddTerrainWizzardFormToList(TerrainWizzardForm terrainForm)
        {
            if (lstTerrainWizzardForms.Contains(terrainForm))
                lstTerrainWizzardForms.Add(terrainForm);
        }

        public static void RemoveTerrainWizzardFormFromList(TerrainWizzardForm terrainForm)
        {
            if (lstTerrainWizzardForms.Contains(terrainForm))
                lstTerrainWizzardForms.Remove(terrainForm);
        }

        public static List<string> GetRecursiveFolders(string folderPath)
        {
            List<string> results = new List<string>();
            try
            {
                if (folderPath != "")
                {
                    // check server path
                    if (folderPath.ToLower().Trim().StartsWith("http") || folderPath.ToLower().Trim().StartsWith("ftp"))
                        results.Add(folderPath);
                    else if (Directory.GetDirectories(folderPath).Length > 0)  // local folder
                    {
                        if (IsMapCoreFolder(folderPath))
                            results.Add(folderPath);
                        else
                        {
                            string[] dirs = Directory.GetDirectories(folderPath);
                            foreach (string dir in dirs)
                            {
                                if (IsMapCoreFolder(dir))
                                    results.Add(dir);
                                else
                                    results.AddRange(GetRecursiveFolders(dir));
                            }
                        }
                    }
                    else // Directory.GetDirectories(folderPath).Length = 0
                        results.Add(folderPath);

                }
            }
            catch (Exception )
            {
                MessageBox.Show(folderPath, "Get Recursive Folders - Invalid path", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return results;
        }

        public static bool IsMapCoreFolder(string folderPath)
        {
            string[] files = Directory.GetFiles(folderPath);
            List<string> lstFiles = new List<string>(files);
            return lstFiles.Exists(x => Path.GetExtension(x) == ".bin");
        }

        public static string[] GetFolders(string folderPath)
        {
            List<string> results = GetRecursiveFolders(folderPath);

            return results.ToArray();
        }

        public static IDNMcGridCoordinateSystem GetGCS(DNSServerLayerInfo layer)
        {
            DNSTileMatrixSet? tileMatrixSet = GetTileMatrixSet(layer);
            IDNMcGridCoordinateSystem pCoordinateSystem = null;
            if(tileMatrixSet.HasValue)
                pCoordinateSystem = tileMatrixSet.Value.pCoordinateSystem;
           
            if (pCoordinateSystem == null && layer.pCoordinateSystem != null)
                pCoordinateSystem = layer.pCoordinateSystem;
            return pCoordinateSystem;
        }

        public static string GetGCSStr(DNSServerLayerInfo layer)
        {
            DNSTileMatrixSet? tileMatrixSet = GetTileMatrixSet(layer);
            IDNMcGridCoordinateSystem pCoordinateSystem = GetGCS(layer);
            string strCoordinateSystem = "";
            if (pCoordinateSystem != null && tileMatrixSet.HasValue)
                strCoordinateSystem = tileMatrixSet.Value.strName;
            if (pCoordinateSystem != null && strCoordinateSystem == "")
            {
                strCoordinateSystem = GetGCSStr(pCoordinateSystem);
            }
            return strCoordinateSystem;
        }

        public static string GetGCSStr(IDNMcGridCoordinateSystem pCoordinateSystem)
        {
            string strCoordinateSystem = "";
           
            if (pCoordinateSystem != null)
            {
                if (pCoordinateSystem.GetGridCoorSysType() == DNEGridCoordSystemType._EGCS_GENERIC_GRID)
                {
                    IDNMcGridGeneric mcGridGeneric = (IDNMcGridGeneric)pCoordinateSystem;
                    string[] pastrCreateParams;
                    bool isSRID;
                    mcGridGeneric.GetCreateParams(out pastrCreateParams, out isSRID);
                    if (pastrCreateParams != null && pastrCreateParams.Length > 0)
                    {
                        if (isSRID)
                        {
                            strCoordinateSystem = pastrCreateParams[0];
                        }
                        else
                        {
                            for (int i = 0; i < pastrCreateParams.Length; i++)
                            {
                                strCoordinateSystem += pastrCreateParams[i] + " ";
                            }
                        }
                    }

                    

                   
                }
                else
                    strCoordinateSystem = GeneralFuncs.GetDirectInterfaceName(pCoordinateSystem.GetType()).Replace("IDN", "");
            }
            return strCoordinateSystem;
        }

        public static DNSTileMatrixSet? GetTileMatrixSet(DNSServerLayerInfo layer, string strName = "")
        {
            DNSTileMatrixSet? tileMatrixSet = null;

            if (layer.aTileMatrixSets != null && layer.aTileMatrixSets.Length > 0)
            {
                if (strName == "")
                    tileMatrixSet = layer.aTileMatrixSets[0];
                else
                    tileMatrixSet = layer.aTileMatrixSets.First(x => x.strName == strName);
            }

            return tileMatrixSet;
        }

        public static DNSMcBox GetBB(DNSServerLayerInfo layer)
        {
            DNSTileMatrixSet? tileMatrixSet = GetTileMatrixSet(layer);
            DNSMcBox BoundingBox = layer.BoundingBox;
            if (tileMatrixSet != null && tileMatrixSet.Value.bHasBoundingBox && tileMatrixSet.Value.BoundingBox != null)
            {
                BoundingBox = tileMatrixSet.Value.BoundingBox;
            }
            return BoundingBox;
        }

        public static string GetBBStr(DNSMcBox boundingBox)
        {
            string strBoundingBox = "";
            if (boundingBox != null)
            {
                strBoundingBox = "((" + Math.Round(boundingBox.MinVertex.x, 2) + "," + Math.Round(boundingBox.MinVertex.y, 2)
                               + "),(" + Math.Round(boundingBox.MaxVertex.x, 2) + "," + Math.Round(boundingBox.MaxVertex.y, 2) + "))";
            }
            return strBoundingBox;
        }

        public static List<IDNMcMapLayer> CheckAndCreateWebLayersData(Dictionary<string, List<MCTWebServerUserSelection>> dicServerSelectLayers,
          Dictionary<string, DNEWebMapServiceType> dicServerSelectLayersType,
          bool isWMTSOpenAllLayersAsOne, bool isWMSOpenAllLayersAsOne,
          DNSWMTSParams SWMTSParams,
          DNSWMSParams SWMSParams,
          DNSWCSParams SWCSParams,
          MCTRaw3DModelParams raw3DModelParams,
          bool isWCSRaster,
          bool isFilterDTMLayer,
          bool isAddLayerToDic,
          out bool Enabled,
          Dictionary<string, MCTServerSelectLayersData> dicWMTSFromCSWLayersData = null)
        {
            Enabled = false;
            List<IDNMcMapLayer> layers = new List<IDNMcMapLayer>();
            DNEWebMapServiceType eWebMapServiceType;
            if (dicServerSelectLayers != null)
            {
                foreach (string serverUrl in dicServerSelectLayers.Keys)
                {
                    eWebMapServiceType = dicServerSelectLayersType[serverUrl];

                    List<IDNMcMapLayer> newLayers = CreateWebLayer(dicServerSelectLayers[serverUrl],
                         isWMTSOpenAllLayersAsOne,
                         isWMSOpenAllLayersAsOne,
                         SWMTSParams,
                         SWMSParams,
                         SWCSParams,
                         raw3DModelParams,
                         isWCSRaster,
                         out Enabled,
                         eWebMapServiceType,
                         serverUrl,
                         isFilterDTMLayer,
                         isAddLayerToDic);

                    if (newLayers.Count > 0)
                        layers.AddRange(newLayers);
                }
            }
            if (dicWMTSFromCSWLayersData != null)
            {
                foreach (string serverUrl in dicWMTSFromCSWLayersData.Keys)
                {
                    MCTServerSelectLayersData serverSelectLayersData = dicWMTSFromCSWLayersData[serverUrl];
                    eWebMapServiceType = serverSelectLayersData.WebMapServiceType;

                    List<IDNMcMapLayer> newLayers = CreateWebLayer(serverSelectLayersData.SelectedLayers,
                         isWMTSOpenAllLayersAsOne,
                         isWMSOpenAllLayersAsOne,
                        (DNSWMTSParams)serverSelectLayersData.WebMapServiceParams,
                         SWMSParams,
                         SWCSParams,
                         raw3DModelParams,
                         isWCSRaster,
                         out Enabled,
                         eWebMapServiceType,
                         serverUrl,
                         isFilterDTMLayer,
                         isAddLayerToDic);

                    if (newLayers.Count > 0)
                        layers.AddRange(newLayers);
                }
            }
            return layers;
        }

        public static List<IDNMcMapLayer> CreateWebLayer(List<MCTWebServerUserSelection> serverSelectLayers,
           bool isWMTSOpenAllLayersAsOne,
           bool isWMSOpenAllLayersAsOne,
           DNSWMTSParams SWMTSParams,
           DNSWMSParams SWMSParams,
           DNSWCSParams SWCSParams,
           MCTRaw3DModelParams raw3DModelParams,
           bool isWCSRaster,
           out bool Enabled,
           DNEWebMapServiceType eWebMapServiceType,
           string serverUrl,
           bool isFilterDTMLayer,
           bool isAddLayerToDic)
        {
            Enabled = false;
            IDNMcMapLayer layer = null;
            List<IDNMcMapLayer> newLayers = new List<IDNMcMapLayer>();
            if (serverSelectLayers != null && serverSelectLayers.Count > 0)
            {
                if ((eWebMapServiceType == DNEWebMapServiceType._EWMS_WMTS && isWMTSOpenAllLayersAsOne)
                  || (eWebMapServiceType == DNEWebMapServiceType._EWMS_WMS && isWMSOpenAllLayersAsOne))
                {
                    if (isFilterDTMLayer)
                    {
                        MessageBox.Show("Error create server map layer, " + Environment.NewLine
                          + "Non DTM layer can not be chosen as Query Secondary DTM Layer.",
                          "Create server layer", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Enabled = true;
                        return newLayers;
                    }

                    if (eWebMapServiceType == DNEWebMapServiceType._EWMS_WMTS)
                        layer = CreateWebServiceRasterLayer(SWMTSParams, null);
                    else
                        layer = CreateWebServiceRasterLayer(SWMSParams, null);

                    if (layer == null)
                    {
                        MessageBox.Show("Error create server map layer, " + Environment.NewLine
                            + " details: Identifier = " + SWMTSParams.strLayersList + Environment.NewLine
                            + ", layer type = " + SWMTSParams.strImageFormat,
                            "Create server layer", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Enabled = true;
                        return newLayers;
                    }
                    else
                    {
                        newLayers.Add(layer);

                        AddLayerToServerLayersPriorityDic(layer, serverSelectLayers[0].ServerLayerInfo.nDrawPriority);
                    }
                }
                else
                {
                    bool isMultiSelection = serverSelectLayers.Count > 1;
                    bool isTakeTileMatrixSet = eWebMapServiceType == DNEWebMapServiceType._EWMS_WMTS && SWMTSParams.strServerCoordinateSystem == MultiSelection;
                    bool isTakeImageFormat = eWebMapServiceType == DNEWebMapServiceType._EWMS_WMTS && SWMTSParams.strImageFormat == MultiSelection;
                    bool isTakeStyle = eWebMapServiceType == DNEWebMapServiceType._EWMS_WMTS && SWMTSParams.strStylesList == MultiSelection;
                    if (isFilterDTMLayer && (
                        (eWebMapServiceType == DNEWebMapServiceType._EWMS_WMTS) ||
                        (eWebMapServiceType == DNEWebMapServiceType._EWMS_WMS) ||
                        (eWebMapServiceType == DNEWebMapServiceType._EWMS_CSW) ||
                        (eWebMapServiceType == DNEWebMapServiceType._EWMS_WCS && isWCSRaster)))
                    {
                        MessageBox.Show("Error create server map layer, " + Environment.NewLine
                          + "Non DTM layer can not be chosen as Query Secondary DTM Layer.",
                          "Create server layer", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Enabled = true;
                        return newLayers;
                    }

                    foreach (MCTWebServerUserSelection serverLayerInfo in serverSelectLayers)
                    {
                        if (eWebMapServiceType == DNEWebMapServiceType._EWMS_MAPCORE)
                        {
                            string layerName = serverUrl + "/" + serverLayerInfo.ServerLayerInfo.strLayerId;
                            string strLayerType = serverLayerInfo.ServerLayerInfo.strLayerType;
                            if (strLayerType == "" && serverLayerInfo.ServerLayerInfo.astrImageFormats != null && serverLayerInfo.ServerLayerInfo.astrImageFormats.Length > 0)
                                strLayerType = serverLayerInfo.ServerLayerInfo.astrImageFormats[0];

                            string mcLayerType = strLayerType.Replace("MapCoreServer", "");
                            if (isFilterDTMLayer && mcLayerType != "DTM")
                            {
                                MessageBox.Show("Error create server map layer, " + Environment.NewLine
                                  + "Non DTM layer can not be chosen as Query Secondary DTM Layer.",
                                  "Create server layer", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                Enabled = true;
                                return newLayers;
                            }
                            switch (mcLayerType)
                            {
                                case "Raster":
                                    layer = CreateNativeServerRasterLayer(layerName);
                                    break;
                                case "DTM":
                                    layer = CreateNativeServerDTMLayer(layerName, isAddLayerToDic);
                                    break;
                                case "Vector":
                                    layer = CreateNativeServerVectorLayer(layerName);
                                    break;
                                case "Vector3DExtrusion":
                                    layer = CreateNativeServerVector3DExtrusionLayer(layerName);
                                    break;
                                case "3DModel":
                                    layer = CreateNativeServer3DModelLayer(layerName);
                                    break;
                                case "Traversability":
                                    layer = CreateNativeServerTraversabilityLayer(layerName);
                                    break;
                                case "Material":
                                    layer = CreateNativeServerMaterialLayer(layerName);
                                    break;
                                case "StaticObjects":
                                    frmStaticObjectsQuestion frmStaticObjectsQuestion1 = new frmStaticObjectsQuestion(serverLayerInfo.ServerLayerInfo.strTitle);
                                    DialogResult dialogResult = frmStaticObjectsQuestion1.ShowDialog();
                                    if (dialogResult == DialogResult.Yes)
                                        layer = CreateNativeServer3DModelLayer(layerName);
                                    else if (dialogResult == DialogResult.No)
                                        layer = CreateNativeServerVector3DExtrusionLayer(layerName);
                                    break;
                            }
                        }
                        else if (eWebMapServiceType == DNEWebMapServiceType._EWMS_WMTS)
                        {
                            DNSWMTSParams newWMTSParams = new DNSWMTSParams();
                            SetDNSWebMapServiceParams(newWMTSParams, SWMTSParams);
                            newWMTSParams.strInfoFormat = SWMTSParams.strInfoFormat;
                            newWMTSParams.bUseServerTilingScheme = SWMTSParams.bUseServerTilingScheme;
                            newWMTSParams.bExtendBeyondDateLine = SWMTSParams.bExtendBeyondDateLine;
                            newWMTSParams.eCapabilitiesBoundingBoxAxesOrder = SWMTSParams.eCapabilitiesBoundingBoxAxesOrder;

                            newWMTSParams.strLayersList = serverLayerInfo.ServerLayerInfo.strLayerId;
                            if (isTakeTileMatrixSet && serverLayerInfo.ServerLayerInfo.aTileMatrixSets != null && serverLayerInfo.ServerLayerInfo.aTileMatrixSets.Length > 0)
                            {
                                newWMTSParams.strServerCoordinateSystem = serverLayerInfo.ServerLayerInfo.aTileMatrixSets[0].strName;
                                newWMTSParams.BoundingBox = serverLayerInfo.ServerLayerInfo.aTileMatrixSets[0].BoundingBox;
                            }
                            if (isTakeImageFormat)
                            {
                                if (serverLayerInfo.ServerLayerInfo.astrImageFormats != null && serverLayerInfo.ServerLayerInfo.astrImageFormats.Length > 0)
                                    newWMTSParams.strImageFormat = serverLayerInfo.ServerLayerInfo.astrImageFormats[0];
                                else
                                    newWMTSParams.strImageFormat = "";
                            }
                            if (isTakeStyle)
                            {
                                string styles = "";
                                if (serverLayerInfo.ServerLayerInfo.astrStyles != null && serverLayerInfo.ServerLayerInfo.astrStyles.Length > 0)
                                {

                                    for (int i = 0; i < serverLayerInfo.ServerLayerInfo.astrStyles.Length; i++)
                                    {
                                        styles += serverLayerInfo.ServerLayerInfo.astrStyles[i] + ",";
                                    }
                                    styles = styles.TrimEnd(',');

                                }
                                newWMTSParams.strStylesList = styles;
                            }

                            layer = CreateWebServiceRasterLayer(newWMTSParams, null);
                        }
                        else if (eWebMapServiceType == DNEWebMapServiceType._EWMS_WCS)
                        {
                            DNSWCSParams newWCSParams = new DNSWCSParams();
                            SetDNSWebMapServiceParams(newWCSParams, SWCSParams);
                            newWCSParams.strWCSVersion = SWCSParams.strWCSVersion;
                            newWCSParams.bDontUseServerInterpolation = SWCSParams.bDontUseServerInterpolation;

                            newWCSParams.strLayersList = serverLayerInfo.ServerLayerInfo.strLayerId;

                            if (isWCSRaster)
                                layer = CreateWebServiceRasterLayer(newWCSParams, null);
                            else
                                layer = CreateWebServiceDtmLayer(newWCSParams, null, isAddLayerToDic);
                        }
                        else if (eWebMapServiceType == DNEWebMapServiceType._EWMS_WMS)
                        {
                            DNSWMSParams newWMSParams = new DNSWMSParams();
                            SetDNSWebMapServiceParams(newWMSParams, SWMSParams);
                            newWMSParams.strWMSVersion = SWMSParams.strWMSVersion;
                            newWMSParams.uBlockWidth = SWMSParams.uBlockWidth;
                            newWMSParams.uBlockHeight = SWMSParams.uBlockHeight;
                            newWMSParams.fMinScale = SWMSParams.fMinScale;

                            newWMSParams.strLayersList = serverLayerInfo.ServerLayerInfo.strLayerId;

                            layer = CreateWebServiceRasterLayer(newWMSParams, null);
                        }
                        else if (eWebMapServiceType == DNEWebMapServiceType._EWMS_CSW && serverLayerInfo.ServerLayerInfo.strLayerType != Manager_MCLayers.WMTSFromCSWType)
                        {

                            layer = CreateRaw3DModelLayer(serverLayerInfo.ServerLayerInfo.strLayerId,
                                raw3DModelParams.pTargetCoordinateSystem,
                                raw3DModelParams.OrthometricHeights,
                                raw3DModelParams.pClipRect,
                                raw3DModelParams.fTargetHighestResolution,
                                raw3DModelParams.aRequestParams);
                        }

                        if (serverLayerInfo.ServerLayerInfo.strLayerType != Manager_MCLayers.WMTSFromCSWType)  // if serverLayerInfo.ServerLayerInfo.strLayerType = Manager_MCLayers.WMTSFromCSWType is csw layer to wmts selection layer
                        {
                            if (layer == null)
                            {
                                MessageBox.Show("Error create server map layer, " + Environment.NewLine
                                    + " details: Identifier = " + serverLayerInfo.ServerLayerInfo.strLayerId + Environment.NewLine
                                    + ", layer type = " + serverLayerInfo.ServerLayerInfo.strLayerType,
                                    "Create server layer", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                Enabled = true;
                                return newLayers;
                            }
                            else
                            {
                                newLayers.Add(layer);
                                AddLayerToServerLayersPriorityDic(layer, serverLayerInfo.ServerLayerInfo.nDrawPriority);
                            }
                        }
                    }
                }


            }
            return newLayers;

        }


        public static void SetDNSWebMapServiceParams(DNSWebMapServiceParams newParams, DNSWebMapServiceParams oldParams)
        {
            newParams.aRequestParams = oldParams.aRequestParams;
            newParams.bEnhanceBorderOverlap = oldParams.bEnhanceBorderOverlap;
            newParams.bFillEmptyTilesByLowerResolutionTiles = oldParams.bFillEmptyTilesByLowerResolutionTiles;
            newParams.BoundingBox = oldParams.BoundingBox;
            newParams.bResolveOverlapConflicts = oldParams.bResolveOverlapConflicts;
            newParams.bSkipSSLCertificateVerification = oldParams.bSkipSSLCertificateVerification;
            newParams.bTransparent = oldParams.bTransparent;
            newParams.byTransparentColorPrecision = oldParams.byTransparentColorPrecision;
            newParams.bZeroBlockOnServerException = oldParams.bZeroBlockOnServerException;
            newParams.fMaxScale = oldParams.fMaxScale;
            newParams.pCoordinateSystem = oldParams.pCoordinateSystem;
            newParams.pReadCallback = oldParams.pReadCallback;
            newParams.pTilingScheme = oldParams.pTilingScheme;
            newParams.strImageFormat = oldParams.strImageFormat;
            newParams.strLayersList = oldParams.strLayersList;
            newParams.strOptionalUserAndPassword = oldParams.strOptionalUserAndPassword;
            newParams.strServerCoordinateSystem = oldParams.strServerCoordinateSystem;
            newParams.strServerURL = oldParams.strServerURL;
            newParams.strStylesList = oldParams.strStylesList;
            newParams.strZeroBlockHttpCodes = oldParams.strZeroBlockHttpCodes;
            newParams.TransparentColor = oldParams.TransparentColor;
            newParams.uTimeoutInSec = oldParams.uTimeoutInSec;

        }


        public static void OpenServerLayers_Click(TextBox txtServerPath, DNEWebMapServiceType webMapServiceType, IWebLayerRequest formWebLayerRequest, List<DNSMcKeyStringValue> requestParams)
        {
            string sURL = txtServerPath.Text.Trim();

            if (sURL == "")
            {
                MessageBox.Show("Server path field is empty, please fill it.", "Missing server path");
                txtServerPath.Select();
                return;
            }

            try
            {
                if (webMapServiceType == DNEWebMapServiceType._EWMS_MAPCORE)
                    UrlMapCoreServer = sURL;
                else if (webMapServiceType == DNEWebMapServiceType._EWMS_WCS)
                    UrlWCSServer = sURL;
                else if (webMapServiceType == DNEWebMapServiceType._EWMS_WMS)
                    UrlWMSServer = sURL;
                else if (webMapServiceType == DNEWebMapServiceType._EWMS_WMTS)
                    UrlWMTSServer = sURL;
                else if (webMapServiceType == DNEWebMapServiceType._EWMS_CSW)
                    UrlCSWServer = sURL;

                MCTServerLayersAsyncOperationCallback mctAsyncOperationCallback = new MCTServerLayersAsyncOperationCallback(formWebLayerRequest, sURL);

                DNMcMapDevice.GetWebServerLayers(sURL, webMapServiceType, requestParams == null ? null : requestParams.ToArray(), mctAsyncOperationCallback);

            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcMapDevice.GetWebServerLayers", McEx);
            }
        }

        internal static void RemoveStandaloneLayers(IDNMcMapLayer[] iDNMcMapLayers)
        {
            if (iDNMcMapLayers != null)
            {
                foreach (IDNMcMapLayer iLayer in iDNMcMapLayers)
                {
                    if (iLayer != null)
                    {
                        RemoveStandaloneLayer(iLayer);
                    }
                }
            }
        }

    }
}
