using MapCore;
using MCTester.MapWorld;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCTester.Controls;
using MapCore.Common;
using MCTester.Managers;
using MCTester.Managers.MapWorld;

namespace MCTester.General_Forms
{
    public partial class frmLayerParams : Form
    {

        public frmLayerParams(IDNMcMapLayer layer)
        {
            InitializeComponent();
            if (layer != null)
                LoadLayerParams(layer);
            ucLayerParams1.SetReadOnlyBrowseIndexingDataDirectory();

            GeneralFuncs.SetControlsReadonly(this);  
            ucLayerParams1.IsReadOnly(true);
        }

        
        private void LoadLayerParams(IDNMcMapLayer layer)
        {
            ucLayerParams1.HideNoOfLayers();
            try
            {
                uint puFirstLowerQualityLevel;
                bool pbThereAreMissingFiles;
                uint puNumLevelsToIgnore;
                float pfTargetHighestResolution;
                bool pbEnhanceBorderOverlap;
                float pfExtrusionHeightMaxAddition;
                bool pbOrthometricHeights;
                string pstrRawDataDirectory;
                string pstrIndexingDataDirectory;
                bool pbNonDefaultIndexingDataDirectory;
                DNSMcVector3D PositionOffset;
                DNSMcKeyStringValue[] paRequestParams;
                IDNMcGridCoordinateSystem ppTargetCoordinateSystem;
                DNSTilingScheme ppTilingScheme;

                DNSRawParams rawParams;

                bool bImageCoordinateSystem;
                ucLayerParams1.SetMapLayerType(layer.LayerType.ToString().Replace("_ELT_", ""));
                ucLayerParams1.SetLayerFileName(layer.GetDirectory());

                if (MCTMapDevice.IsInitilizeDeviceLocalCache())
                    ucLayerParams1.SetLocalCacheLayerParams(layer.GetLocalCacheLayerParams());

                switch (layer.LayerType)
                {
                    case DNELayerType._ELT_NATIVE_DTM:
                        (layer as IDNMcNativeDtmMapLayer).GetCreateParams(out puNumLevelsToIgnore);
                        ucLayerParams1.SetNumLevelsToIgnore(puNumLevelsToIgnore);
                        break;
                    case DNELayerType._ELT_NATIVE_MATERIAL:
                        (layer as IDNMcNativeMaterialMapLayer).GetCreateParams(out pbThereAreMissingFiles);
                        ucLayerParams1.SetThereAreMissingFiles(pbThereAreMissingFiles);
                        break;
                    case DNELayerType._ELT_NATIVE_TRAVERSABILITY:
                        (layer as IDNMcNativeTraversabilityMapLayer).GetCreateParams(out pbThereAreMissingFiles);
                        ucLayerParams1.SetThereAreMissingFiles(pbThereAreMissingFiles);
                        break;
                    case DNELayerType._ELT_NATIVE_HEAT_MAP:
                        (layer as IDNMcNativeHeatMapLayer).GetCreateParams(out puFirstLowerQualityLevel,
                                        out pbThereAreMissingFiles,
                                        out puNumLevelsToIgnore,
                                        out pbEnhanceBorderOverlap);

                        ucLayerParams1.SetFirstLowerQualityLevel(puFirstLowerQualityLevel);
                        ucLayerParams1.SetThereAreMissingFiles(pbThereAreMissingFiles);
                        ucLayerParams1.SetNumLevelsToIgnore(puNumLevelsToIgnore);
                        ucLayerParams1.SetEnhanceBorderOverlap2(pbEnhanceBorderOverlap);
                        break;
                    case DNELayerType._ELT_NATIVE_RASTER:
                        (layer as IDNMcNativeRasterMapLayer).GetCreateParams(out puFirstLowerQualityLevel,
                                       out pbThereAreMissingFiles,
                                       out puNumLevelsToIgnore,
                                       out pbEnhanceBorderOverlap);

                        ucLayerParams1.SetFirstLowerQualityLevel(puFirstLowerQualityLevel);
                        ucLayerParams1.SetThereAreMissingFiles(pbThereAreMissingFiles);
                        ucLayerParams1.SetNumLevelsToIgnore(puNumLevelsToIgnore);
                        ucLayerParams1.SetEnhanceBorderOverlap2(pbEnhanceBorderOverlap);
                        break;
                    case DNELayerType._ELT_NATIVE_3D_MODEL:
                        (layer as IDNMcNative3DModelMapLayer).GetCreateParams(out puNumLevelsToIgnore);
                        ucLayerParams1.SetNumLevelsToIgnore(puNumLevelsToIgnore);
                        break;
                    case DNELayerType._ELT_NATIVE_VECTOR_3D_EXTRUSION:
                        (layer as IDNMcNativeVector3DExtrusionMapLayer).GetCreateParams(out puNumLevelsToIgnore, out pfExtrusionHeightMaxAddition);

                        ucLayerParams1.SetNumLevelsToIgnore(puNumLevelsToIgnore);
                        ucLayerParams1.SetExtrusionHeightMaxAddition(pfExtrusionHeightMaxAddition);
                        break;
                    case DNELayerType._ELT_NATIVE_VECTOR:
                    case DNELayerType._ELT_NATIVE_SERVER_RASTER:
                    case DNELayerType._ELT_NATIVE_SERVER_DTM:
                    case DNELayerType._ELT_NATIVE_SERVER_VECTOR:
                    case DNELayerType._ELT_NATIVE_SERVER_3D_MODEL:
                    case DNELayerType._ELT_NATIVE_SERVER_TRAVERSABILITY:
                    case DNELayerType._ELT_NATIVE_SERVER_VECTOR_3D_EXTRUSION:
                        break;
                    case DNELayerType._ELT_RAW_RASTER:
                        (layer as IDNMcRawRasterMapLayer).GetCreateParams(out rawParams, out bImageCoordinateSystem);
                        ucLayerParams1.SetRawParams(rawParams);
                        ucLayerParams1.SetImageCoordSys(bImageCoordinateSystem);
                        break;
                    case DNELayerType._ELT_RAW_DTM:
                        (layer as IDNMcRawDtmMapLayer).GetCreateParams(out rawParams);
                        ucLayerParams1.SetRawParams(rawParams);
                        break;
                    case DNELayerType._ELT_RAW_MATERIAL:
                        rawParams = (layer as IDNMcRawMaterialMapLayer).GetCreateParams();
                        ucLayerParams1.SetRawParams(rawParams);
                        break;
                    case DNELayerType._ELT_RAW_TRAVERSABILITY:
                        rawParams = (layer as IDNMcRawTraversabilityMapLayer).GetCreateParams();
                        ucLayerParams1.SetRawParams(rawParams);
                        break;
                    case DNELayerType._ELT_RAW_VECTOR:
                        DNSRawVectorParams pRawVectorParams;

                        (layer as IDNMcRawVectorMapLayer).GetCreateParams(out pRawVectorParams, out ppTargetCoordinateSystem, out ppTilingScheme);

                        ucLayerParams1.SetRawVectorParams(pRawVectorParams);
                        ucLayerParams1.SetTilingScheme(ppTilingScheme);
                        ucLayerParams1.SetTargetGridCoordinateSystem(ppTargetCoordinateSystem);

                        break;
                    case DNELayerType._ELT_RAW_VECTOR_3D_EXTRUSION:

                        DNSRawVector3DExtrusionParams pRawVector3DExtrusionParams;
                        (layer as IDNMcRawVector3DExtrusionMapLayer).GetCreateParams(out pRawVector3DExtrusionParams, out pfExtrusionHeightMaxAddition, 
                            out pstrIndexingDataDirectory, out pbNonDefaultIndexingDataDirectory);

                        ucLayerParams1.SetRawVector3DExtrusionParams(pRawVector3DExtrusionParams, pfExtrusionHeightMaxAddition);
                        ucLayerParams1.SetExtrusionIndexingDataDirectoryFromLayer(pstrIndexingDataDirectory, pbNonDefaultIndexingDataDirectory);
                       
                        break;
                    case DNELayerType._ELT_RAW_3D_MODEL:
                        DNSMcBox? ppClipRect;
                        (layer as IDNMcRaw3DModelMapLayer).GetCreateParams(
                            out pstrRawDataDirectory, out pbOrthometricHeights, out puNumLevelsToIgnore,
                            out ppTargetCoordinateSystem, out ppClipRect,
                            out pfTargetHighestResolution, out pstrIndexingDataDirectory, 
                            out pbNonDefaultIndexingDataDirectory, out paRequestParams, out PositionOffset);

                        ucLayerParams1.SetLayerFileName(pstrRawDataDirectory);
                        ucLayerParams1.SetNumLevelsToIgnore(puNumLevelsToIgnore);

                        MCTRaw3DModelParams raw3DModelParams = new MCTRaw3DModelParams();
                        raw3DModelParams.OrthometricHeights = pbOrthometricHeights;
                        raw3DModelParams.IsUseIndexing = pstrIndexingDataDirectory != null && pstrIndexingDataDirectory != "";
                        raw3DModelParams.pTargetCoordinateSystem = ppTargetCoordinateSystem;
                        raw3DModelParams.pClipRect = ppClipRect;
                        raw3DModelParams.fTargetHighestResolution = pfTargetHighestResolution;
                        raw3DModelParams.NonDefaultIndexingDataDirectory = pstrIndexingDataDirectory;
                        raw3DModelParams.IsUseNonDefaultIndexDirectory = pbNonDefaultIndexingDataDirectory;
                        raw3DModelParams.aRequestParams = paRequestParams;
                        raw3DModelParams.PositionOffset = PositionOffset;

                        ucLayerParams1.SetRaw3DModelParams(raw3DModelParams);

                        break;
                    case DNELayerType._ELT_WEB_SERVICE_DTM:
                        IDNMcWebServiceDtmMapLayer webServiceDtmMapLayer = (layer as IDNMcWebServiceDtmMapLayer);
                        DNEWebMapServiceType webMapServiceType = webServiceDtmMapLayer.GetWebMapServiceType();
                        ucLayerParams1.SetWMSTypeSelectedLayer(webMapServiceType);
                        switch (webMapServiceType)
                        {
                            case DNEWebMapServiceType._EWMS_WMS:
                                ucLayerParams1.SetWMSParams(webServiceDtmMapLayer.GetWMSParams());
                                break;
                            case DNEWebMapServiceType._EWMS_WMTS:
                                ucLayerParams1.SetWMTSParams(webServiceDtmMapLayer.GetWMTSParams());
                                break;
                            case DNEWebMapServiceType._EWMS_WCS:
                                ucLayerParams1.SetWCSParams(webServiceDtmMapLayer.GetWCSParams());
                                break;
                        }
                        break;
                    case DNELayerType._ELT_WEB_SERVICE_RASTER:
                        IDNMcWebServiceRasterMapLayer webServiceRasterMapLayer = (layer as IDNMcWebServiceRasterMapLayer);
                        DNEWebMapServiceType rasterType = webServiceRasterMapLayer.GetWebMapServiceType();
                        ucLayerParams1.SetWMSTypeSelectedLayer(rasterType);
                        DNSWMTSParams layerParams = null;
                        bool pbUsedServerTilingScheme;
                        switch (rasterType)
                        {
                            case DNEWebMapServiceType._EWMS_WMS:
                                ucLayerParams1.SetWMSParams(webServiceRasterMapLayer.GetWMSParams());
                                break;
                            case DNEWebMapServiceType._EWMS_WMTS:
                                try
                                {
                                    layerParams = webServiceRasterMapLayer.GetWMTSParams(out pbUsedServerTilingScheme);
                                    ucLayerParams1.SetWMTSParams(layerParams);
                                    ucLayerParams1.SetUsedServerTilingScheme(pbUsedServerTilingScheme);
                                }
                                catch(MapCoreException McEx)
                                {
                                    if (McEx.ErrorCode == DNEMcErrorCode.NOT_INITIALIZED)
                                        ucLayerParams1.SetUsedServerTilingScheme(null);
                                    else
                                        throw McEx;
                                }
                                break;
                            case DNEWebMapServiceType._EWMS_WCS:
                                ucLayerParams1.SetWCSParams(webServiceRasterMapLayer.GetWCSParams());
                                break;
                        }
                        break;
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Load Layer Params", McEx);
            }
        }

    }
}
