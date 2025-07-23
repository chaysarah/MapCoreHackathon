using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers.MapWorld;
using MapCore.Common;
using MCTester.Controls;

namespace MCTester.MapWorld
{
    [Serializable]
    public class MCTMapLayer
    {
        private int m_LayerId;
        private string m_LayerName;
        private bool m_IsSaveFromMap;
        private bool m_RasterImageCoordinateSystem = false;
        private int m_CoordSysID;
        private DNELayerType m_LayerType;
        private string m_Path;
        private uint m_FirstLowerQualityLevel = DNMcConstants._MC_EMPTY_ID;
        private bool m_IsCallback = false;
        private MCTMapLayerReadCallback m_MapLayerReadCallback;
        private DNSLocalCacheLayerParams? m_LocalCacheLayerParams;
        private uint m_NumLevelsToIgnore;
        private bool m_ThereAreMissingFiles;
        private bool m_EnhanceBorderOverlap;

        // IMcNativeVector3DExtrusionMapLayer
        private float m_ExtrusionHeightMaxAddition;

        private DNSTilingScheme m_TilingScheme;
        private DNETilingSchemeType m_TilingSchemeType;
        private int m_TargetCoordinateSystemID;
        private int m_SourceCoordinateSystemID;


        //DNSRawParams
        private List<DNSComponentParams> m_RComponents = new List<DNSComponentParams>();
        private float m_FirstPyramidResolution = 0;
        private List<uint> m_RPyramidResolutions;
        private DNSMcBox m_RMaxWorldLimit = new DNSMcBox(double.MaxValue, double.MaxValue, double.MaxValue, double.MaxValue, double.MaxValue, double.MaxValue);
        private float m_RHighestResolution = 0;
        private bool m_RIgnoreRasterPalette = false;
        private uint m_RMaxNumOpenFiles = 0;

        // DNSNonNativeParams
        private float m_NNMaxScale = float.MaxValue;
        private bool m_NNResolveOverlapConflicts = false;
        //private bool m_NNEnhanceBorderOverlap = false;
        private bool m_NNFillEmptyTilesByLowerResolutionTiles = false;
        private DNSMcBColor m_NNTransparentColor = new DNSMcBColor(0, 0, 0, 0);
        private Byte m_NNTransparentColorPrecision = 0;

        //DNSRawVectorParams
        private string m_RVStrDataSource;
        private float m_RVMinScale;
        private float m_RVMaxScale;
        private string m_RVStrPointTextureFile;
        private string m_RVStrLocaleStr;
        private double m_RVSimplificationTolerance;
        private DNEAutoStylingType m_RVAutoStylingType;
        private string m_RVStrCustomStylingFolder;
        private uint m_RVMaxNumVerticesPerTile;
        private uint m_RVMaxNumVisiblePointObjectsPerTile;
        private DNSMcBox? m_RVClipRect;
        private uint m_RVMinPixelSizeForObjectVisibility;
        private float m_RVOptimizationMinScale;

        // DNSRawVector3DExtrusionParams
        private string m_RV3DStrDataSource;
        private DNSExtrusionTexture m_RV3DRoofDefaultTexture;
        private DNSExtrusionTexture m_RV3DSideDefaultTexture;
        private DNSExtrusionTexture[] m_RV3DaSpecificTextures;
        private string m_RV3DStrHeightColumn;
        //private int m_RV3DSourceCoordinateSystemID;
        private string m_RV3DStrObjectIDColumn;
        private string m_RV3DStrRoofTextureIndexColumn;
        private string m_RV3DStrSideTextureIndexColumn;
        private DNSMcBox? m_RV3DClipRect;

        // Raw3DModelParams
        private bool m_R3DOrthometricHeights = false;
        private string m_RSOIndexingDataDir = "";
        private bool m_IsUseIndexing = false;
        private bool m_RSONonDefaultIndexingDataDir = false;
        private float m_TargetHighestResolution;
        private DNSMcBox? m_R3DClipRect;
        private DNSMcVector3D m_R3DPositionOffset;
        //DNSWebMapServiceParams

        private string m_WSStrServerURL;
        private string m_WSStrOptionalUserAndPassword;
        private DNSMcKeyStringValue[] m_WSRequestParams;
        private uint m_WSTimeoutInSec = 300;
        private string m_WSStrLayersList;
        private string m_WSStrStylesList;
        private DNSMcBox m_WSBoundingBox;
        private string m_WSStrServerCoordinateSystem;
        private string m_WSStrImageFormat;
        private bool m_WSTransparent;
        private string m_WSStrZeroBlockHttpCodes = "204,404";
        private bool m_WSZeroBlockOnServerException = true;

        // DNSWMSParams
        private DNEWebMapServiceType m_WSWebMapServiceType;
        
        private string m_WMSStrWMSVersion = "1.3.0";
        private uint m_WMSBlockWidth = 256;
        private uint m_WMSBlockHeight = 256;
        private float m_WMSMinScale = 1;

        // DNSWMTSParams
        private string m_WMTSStrInfoFormat = null;
        private bool m_WMTSUseServerTilingScheme = false;
        private bool m_WMTSExtendBeyondDateLine = false;
        private DNECoordinateAxesOrder m_WMTSCapabilitiesBoundingBoxAxesOrder = DNECoordinateAxesOrder._ECAO_DEFAULT;

        // DNSWCSParams
        private string m_WCSStrWCSVersion = "1.0.0";
        private bool m_WCSDontUseServerInterpolation = false;
        private bool m_WSSkipSSLCertificateVerification;

        public static string DefualtPointTexturePath = "..\\..\\..\\Tools\\MCVectorEditor\\Points\\Default.ico";

        #region Public Properties

        public DNEWebMapServiceType WSWebMapServiceType
        {
            get { return m_WSWebMapServiceType; }
            set { m_WSWebMapServiceType = value; }
        }

        public string WMSStrWMSVersion
        {
            get { return m_WMSStrWMSVersion; }
            set { m_WMSStrWMSVersion = value; }
        }
        public uint WMSBlockWidth
        {
            get { return m_WMSBlockWidth; }
            set { m_WMSBlockWidth = value; }
        }
        public uint WMSBlockHeight
        {
            get { return m_WMSBlockHeight; }
            set { m_WMSBlockHeight = value; }
        }
        public float WMSMinScale
        {
            get { return m_WMSMinScale; }
            set { m_WMSMinScale = value; }
        }
      
        public string WMTSStrInfoFormat
        {
            get { return m_WMTSStrInfoFormat; }
            set { m_WMTSStrInfoFormat = value; }
        }
       public bool WMTSUseServerTilingScheme
        {
            get { return m_WMTSUseServerTilingScheme; }
            set { m_WMTSUseServerTilingScheme = value; }
        }
        public bool WMTSExtendBeyondDateLine
        {
            get { return m_WMTSExtendBeyondDateLine; }
            set { m_WMTSExtendBeyondDateLine = value; }
        }

        public MapCore.DNECoordinateAxesOrder WMTSCapabilitiesBoundingBoxAxesOrder
        {
            get { return m_WMTSCapabilitiesBoundingBoxAxesOrder; }
            set { m_WMTSCapabilitiesBoundingBoxAxesOrder = value; }
        }
        
        public string WCSStrWCSVersion
        {
            get { return m_WCSStrWCSVersion; }
            set { m_WCSStrWCSVersion = value; }
        }
        public bool WCSDontUseServerInterpolation
        {
            get { return m_WCSDontUseServerInterpolation; }
            set { m_WCSDontUseServerInterpolation = value; }
        }
        

        public string WSStrServerURL
        {
            get { return m_WSStrServerURL; }
            set { m_WSStrServerURL = value; }
        }
        public string WSStrOptionalUserAndPassword
        {
            get { return m_WSStrOptionalUserAndPassword; }
            set { m_WSStrOptionalUserAndPassword = value; }
        }
        public bool WSSkipSSLCertificateVerification
        {
            get { return m_WSSkipSSLCertificateVerification; }
            set { m_WSSkipSSLCertificateVerification = value; }
        }
        public DNSMcKeyStringValue[] WSRequestParams
        {
            get { return m_WSRequestParams; }
            set { m_WSRequestParams = value; }
        }
        public uint WSTimeoutInSec
        {
            get { return m_WSTimeoutInSec; }
            set { m_WSTimeoutInSec = value; }
        }
        public string WSStrLayersList
        {
            get { return m_WSStrLayersList; }
            set { m_WSStrLayersList = value; }
        }
        public string WSStrStylesList
        {
            get { return m_WSStrStylesList; }
            set { m_WSStrStylesList = value; }
        }

        public MapCore.DNSMcBox WSBoundingBox
        {
            get { return m_WSBoundingBox; }
            set { m_WSBoundingBox = value; }
        }
        public string WSStrServerCoordinateSystem
        {
            get { return m_WSStrServerCoordinateSystem; }
            set { m_WSStrServerCoordinateSystem = value; }
        }
        public string WSStrImageFormat
        {
            get { return m_WSStrImageFormat; }
            set { m_WSStrImageFormat = value; }
        }
        public bool WSTransparent
        {
            get { return m_WSTransparent; }
            set { m_WSTransparent = value; }
        }
        public string WSStrZeroBlockHttpCodes
        {
            get { return m_WSStrZeroBlockHttpCodes; }
            set { m_WSStrZeroBlockHttpCodes = value; }
        }
        public bool WSZeroBlockOnServerException
        {
            get { return m_WSZeroBlockOnServerException; }
            set { m_WSZeroBlockOnServerException = value; }
        }


        public bool IsSaveFromMap
        {
            get { return m_IsSaveFromMap; }
            set { m_IsSaveFromMap = value; }
        }

        public int ID
        {
            get { return m_LayerId; }
            set { m_LayerId = value; }
        }

        public string Name
        {
            get { return m_LayerName; }
            set { m_LayerName = value; }
        }

        public DNELayerType LayerType
        {
            get { return m_LayerType; }
            set { m_LayerType = value; }
        }

        public string Path
        {
            get { return m_Path; }
            set { m_Path = value; }
        }

        public uint FirstLowerQualityLevel
        {
            get { return m_FirstLowerQualityLevel; }
            set { m_FirstLowerQualityLevel = value; }
        }

        public int CoordSysID
        {
            get { return m_CoordSysID; }
            set { m_CoordSysID = value; }
        }

        public bool IsCallback
        {
            get { return m_IsCallback; }
            set { m_IsCallback = value; }
        }

        public MCTMapLayerReadCallback MapLayerReadCallback
        {
            get { return m_MapLayerReadCallback; }
            set { m_MapLayerReadCallback = value; }
        }

        public DNSLocalCacheLayerParams? LocalCacheLayerParams
        {
            get { return m_LocalCacheLayerParams; }
            set { m_LocalCacheLayerParams = value; }
        }

        public uint NumLevelsToIgnore
        {
            get { return m_NumLevelsToIgnore; }
            set { m_NumLevelsToIgnore = value; }
        }

        public bool ThereAreMissingFiles
        {
            get { return m_ThereAreMissingFiles; }
            set { m_ThereAreMissingFiles = value; }
        }

        public bool EnhanceBorderOverlap
        {
            get { return m_EnhanceBorderOverlap; }
            set { m_EnhanceBorderOverlap = value; }
        }

        public DNSTilingScheme TilingScheme
        {
            get { return m_TilingScheme; }
            set { m_TilingScheme = value; }
        }

        public DNETilingSchemeType TilingSchemeType
        {
            get { return m_TilingSchemeType; }
            set { m_TilingSchemeType = value; }
        }

        public int SourceCoordinateSystemID
        {
            get { return m_SourceCoordinateSystemID; }
            set { m_SourceCoordinateSystemID = value; }
        }

        public int TargetCoordinateSystemID
        {
            get { return m_TargetCoordinateSystemID; }
            set { m_TargetCoordinateSystemID = value; }
        }

        public float NNMaxScale
        {
            get { return m_NNMaxScale; }
            set { m_NNMaxScale = value; }
        }

        public bool NNResolveOverlapConflicts
        {
            get { return m_NNResolveOverlapConflicts; }
            set { m_NNResolveOverlapConflicts = value; }
        }

        public bool NNFillEmptyTilesByLowerResolutionTiles
        {
            get { return m_NNFillEmptyTilesByLowerResolutionTiles; }
            set { m_NNFillEmptyTilesByLowerResolutionTiles = value; }
        }

        public DNSMcBColor NNTransparentColor
        {
            get { return m_NNTransparentColor; }
            set { m_NNTransparentColor = value; }
        }

        public Byte NNTransparentColorPrecision
        {
            get { return m_NNTransparentColorPrecision; }
            set { m_NNTransparentColorPrecision = value; }
        }

        public bool RasterImageCoordinateSystem
        {
            get { return m_RasterImageCoordinateSystem; }
            set { m_RasterImageCoordinateSystem = value; }
        }

        public List<DNSComponentParams> lComponents
        {
            get { return m_RComponents; }
            set { m_RComponents = value; }
        }

        public float FirstPyramidResolution
        {
            get { return m_FirstPyramidResolution; }
            set { m_FirstPyramidResolution = value; }
        }

        public float HighestResolution
        {
            get { return m_RHighestResolution; }
            set { m_RHighestResolution = value; }
        }

        public List<uint> lPyramidResolutions
        {
            get { return m_RPyramidResolutions; }
            set { m_RPyramidResolutions = value; }
        }

        public DNSMcBox MaxWorldLimit
        {
            get { return m_RMaxWorldLimit; }
            set { m_RMaxWorldLimit = value; }
        }

        public bool IgnoreRasterPalette
        {
            get { return m_RIgnoreRasterPalette; }
            set { m_RIgnoreRasterPalette = value; }
        }

        public uint MaxNumOpenFiles
        {
            get { return m_RMaxNumOpenFiles; }
            set { m_RMaxNumOpenFiles = value; }
        }

        public float ExtrusionHeightMaxAddition
        {
            get { return m_ExtrusionHeightMaxAddition; }
            set { m_ExtrusionHeightMaxAddition = value; }
        }

        public string RV3DStrDataSource
        {
            get { return m_RV3DStrDataSource; }
            set { m_RV3DStrDataSource = value; }
        }

        public DNSExtrusionTexture RV3DRoofDefaultTexture
        {
            get { return m_RV3DRoofDefaultTexture; }
            set { m_RV3DRoofDefaultTexture = value; }
        }

        public DNSExtrusionTexture RV3DSideDefaultTexture
        {
            get { return m_RV3DSideDefaultTexture; }
            set { m_RV3DSideDefaultTexture = value; }
        }

        public DNSExtrusionTexture[] RV3DaSpecificTextures
        {
            get { return m_RV3DaSpecificTextures; }
            set { m_RV3DaSpecificTextures = value; }
        }

        public string RV3DStrHeightColumn
        {
            get { return m_RV3DStrHeightColumn; }
            set { m_RV3DStrHeightColumn = value; }
        }

        public string RV3DStrObjectIDColumn
        {
            get { return m_RV3DStrObjectIDColumn; }
            set { m_RV3DStrObjectIDColumn = value; }
        }

        public string RV3DStrRoofTextureIndexColumn
        {
            get { return m_RV3DStrRoofTextureIndexColumn; }
            set { m_RV3DStrRoofTextureIndexColumn = value; }
        }

        public string RV3DStrSideTextureIndexColumn
        {
            get { return m_RV3DStrSideTextureIndexColumn; }
            set { m_RV3DStrSideTextureIndexColumn = value; }
        }

        public DNSMcBox? RV3DClipRect
        {
            get { return m_RV3DClipRect; }
            set { m_RV3DClipRect = value; }
        }

        // IMcStaticObjectsMapLayer
        public string RSOIndexingDataDir
        {
            get { return m_RSOIndexingDataDir; }
            set { m_RSOIndexingDataDir = value; }
        }

        public bool RSONonDefaultIndexingDataDir
        {
            get { return m_RSONonDefaultIndexingDataDir; }
            set { m_RSONonDefaultIndexingDataDir = value; }
        }

        // IMcRaw3DModelMapLayer
        public bool R3DOrthometricHeights
        {
            get { return m_R3DOrthometricHeights; }
            set { m_R3DOrthometricHeights = value; }
        }

        public bool IsUseIndexing
        {
            get { return m_IsUseIndexing; }
            set { m_IsUseIndexing = value; }
        }

        public float TargetHighestResolution
        {
            get { return m_TargetHighestResolution; }
            set { m_TargetHighestResolution = value; }
        }
        public DNSMcBox? R3DClipRect
        {
            get { return m_R3DClipRect; }
            set { m_R3DClipRect = value; }
        }

        public DNSMcVector3D R3DPositionOffset
        {
            get { return m_R3DPositionOffset; }
            set { m_R3DPositionOffset = value; }
        }

        // IMcRawVectorMapLayer
        public string RVStrDataSource
        {
            get { return m_RVStrDataSource; }
            set { m_RVStrDataSource = value; }
        }

        public float RVMinScale
        {
            get { return m_RVMinScale; }
            set { m_RVMinScale = value; }
        }

        public float RVMaxScale
        {
            get { return m_RVMaxScale; }
            set { m_RVMaxScale = value; }
        }

        public string RVStrPointTextureFile
        {
            get { return m_RVStrPointTextureFile; }
            set { m_RVStrPointTextureFile = value; }
        }

        public string RVStrLocaleStr
        {
            get { return m_RVStrLocaleStr; }
            set { m_RVStrLocaleStr = value; }
        }

        public double RVSimplificationTolerance
        {
            get { return m_RVSimplificationTolerance; }
            set { m_RVSimplificationTolerance = value; }
        }

        public DNSMcBox? RVClipRect
        {
            get { return m_RVClipRect; }
            set { m_RVClipRect = value; }
        }

        public DNEAutoStylingType RVAutoStylingType
        {
            get { return m_RVAutoStylingType; }
            set { m_RVAutoStylingType = value; }
        }

        public string RVStrCustomStylingFolder
        {
            get { return m_RVStrCustomStylingFolder; }
            set { m_RVStrCustomStylingFolder = value; }
        }

        /*private DNSInternalStylingParams m_RVStylingParams;
        public DNSInternalStylingParams RVStylingParams
        {
            get { return m_RVStylingParams; }
            set { m_RVStylingParams = value; }
        }*/

        public uint RVMaxNumVerticesPerTile
        {
            get { return m_RVMaxNumVerticesPerTile; }
            set { m_RVMaxNumVerticesPerTile = value; }
        }

        public uint RVMaxNumVisiblePointObjectsPerTile
        {
            get { return m_RVMaxNumVisiblePointObjectsPerTile; }
            set { m_RVMaxNumVisiblePointObjectsPerTile = value; }
        }

        public uint RVMinPixelSizeForObjectVisibility
        {
            get { return m_RVMinPixelSizeForObjectVisibility; }
            set { m_RVMinPixelSizeForObjectVisibility = value; }
        }

        public float RVOptimizationMinScale
        {
            get { return m_RVOptimizationMinScale; }
            set { m_RVOptimizationMinScale = value; }
        }

      

        #endregion


        public MCTMapLayer()
        {
            m_RPyramidResolutions = new List<uint>();
        }

        public void SetRawParams(DNSRawParams pParams, int coordId)
        {
            FirstPyramidResolution = pParams.fFirstPyramidResolution;
            IgnoreRasterPalette = pParams.bIgnoreRasterPalette;
            MaxWorldLimit = pParams.MaxWorldLimit;
            HighestResolution = pParams.fHighestResolution;
            MaxNumOpenFiles = pParams.uMaxNumOpenFiles;
            lComponents = new List<DNSComponentParams>();
            if (pParams.aComponents != null)
                lComponents.AddRange(pParams.aComponents);
            lPyramidResolutions = new List<uint>();
            if (pParams.auPyramidResolutions != null)
                lPyramidResolutions.AddRange(pParams.auPyramidResolutions);

            SetNonNativeParams(pParams, coordId);
           /* CoordSysID = coordId;
            EnhanceBorderOverlap = pParams.bEnhanceBorderOverlap;
            NNFillEmptyTilesByLowerResolutionTiles = pParams.bFillEmptyTilesByLowerResolutionTiles;
            NNMaxScale = pParams.fMaxScale;
            NNResolveOverlapConflicts = pParams.bResolveOverlapConflicts;
            TilingScheme = pParams.pTilingScheme;
            this.TilingSchemeType = TilingSchemeType;
            NNTransparentColor = pParams.TransparentColor;
            NNTransparentColorPrecision = pParams.byTransparentColorPrecision;*/

        }

        public void SetNonNativeParams(DNSNonNativeParams pParams, int coordId)
        {
            CoordSysID = coordId;
            EnhanceBorderOverlap = pParams.bEnhanceBorderOverlap;
            NNFillEmptyTilesByLowerResolutionTiles = pParams.bFillEmptyTilesByLowerResolutionTiles;
            NNMaxScale = pParams.fMaxScale;
            NNResolveOverlapConflicts = pParams.bResolveOverlapConflicts;
            TilingScheme = pParams.pTilingScheme;
            TilingSchemeType = CtrlTilingSchemeParams.GetETilingSchemeType(pParams.pTilingScheme);
            NNTransparentColor = pParams.TransparentColor;
            NNTransparentColorPrecision = pParams.byTransparentColorPrecision;

        }

        public DNSRawParams GetRawParamsFromLayer(IDNMcGridCoordinateSystem coordSys)
        {
            DNSRawParams rawParams = new DNSRawParams();

            rawParams.strDirectory = Path;

            if (lComponents != null)
                rawParams.aComponents = lComponents.ToArray();
            if (lPyramidResolutions != null)
                rawParams.auPyramidResolutions = lPyramidResolutions.ToArray();

            rawParams.bIgnoreRasterPalette = IgnoreRasterPalette;
            rawParams.fFirstPyramidResolution = FirstPyramidResolution;
            rawParams.fHighestResolution = HighestResolution;
            rawParams.MaxWorldLimit = MaxWorldLimit;
            rawParams.uMaxNumOpenFiles = MaxNumOpenFiles;

            GetNonNativeParamsFromLayer(rawParams, coordSys);
           /* rawParams.pCoordinateSystem = coordSys;
            rawParams.bEnhanceBorderOverlap = EnhanceBorderOverlap;
            rawParams.bFillEmptyTilesByLowerResolutionTiles = NNFillEmptyTilesByLowerResolutionTiles;
            rawParams.bResolveOverlapConflicts = NNResolveOverlapConflicts;
            rawParams.fMaxScale = NNMaxScale;
            rawParams.pTilingScheme = TilingScheme;
            rawParams.byTransparentColorPrecision = NNTransparentColorPrecision;
            rawParams.TransparentColor = NNTransparentColor;*/

            return rawParams;
        }

        public void GetNonNativeParamsFromLayer(DNSNonNativeParams nonNativeParams, IDNMcGridCoordinateSystem coordSys)
        {
            nonNativeParams.pCoordinateSystem = coordSys;
            nonNativeParams.bEnhanceBorderOverlap = EnhanceBorderOverlap;
            nonNativeParams.bFillEmptyTilesByLowerResolutionTiles = NNFillEmptyTilesByLowerResolutionTiles;
            nonNativeParams.bResolveOverlapConflicts = NNResolveOverlapConflicts;
            nonNativeParams.fMaxScale = NNMaxScale;
            nonNativeParams.pTilingScheme = TilingScheme;
            nonNativeParams.byTransparentColorPrecision = NNTransparentColorPrecision;
            nonNativeParams.TransparentColor = NNTransparentColor;

           // return nonNativeParams;
        }

        public DNSRawVectorParams GetRawVectorParamsFromLayer(IDNMcGridCoordinateSystem coordSys)
        {
            DNSRawVectorParams rawVectorParams = new DNSRawVectorParams("", coordSys);
            rawVectorParams.strPointTextureFile = RVStrPointTextureFile;
            rawVectorParams.strLocaleStr = RVStrLocaleStr;
            rawVectorParams.eAutoStylingType = RVAutoStylingType;
            rawVectorParams.pClipRect = RVClipRect;
            rawVectorParams.uMaxNumVerticesPerTile = RVMaxNumVerticesPerTile;
            rawVectorParams.uMaxNumVisiblePointObjectsPerTile = RVMaxNumVisiblePointObjectsPerTile;
            rawVectorParams.fMaxScale = RVMaxScale;
            rawVectorParams.uMinPixelSizeForObjectVisibility = RVMinPixelSizeForObjectVisibility;
            rawVectorParams.fMinScale = RVMinScale;
            rawVectorParams.fOptimizationMinScale = RVOptimizationMinScale;
            rawVectorParams.dSimplificationTolerance = RVSimplificationTolerance;
            rawVectorParams.strCustomStylingFolder = RVStrCustomStylingFolder;
            rawVectorParams.strDataSource = RVStrDataSource;

            return rawVectorParams;

        }

        public void SetRawVectorParamsToLayer(DNSRawVectorParams rawVectorParams, int sourceGridCoordId, int targetGridCoordId)
        {
            SourceCoordinateSystemID = sourceGridCoordId;
            TargetCoordinateSystemID = targetGridCoordId;

            RVStrPointTextureFile = rawVectorParams.strPointTextureFile;
            RVStrLocaleStr = rawVectorParams.strLocaleStr;
            RVAutoStylingType = rawVectorParams.eAutoStylingType;
            RVClipRect = rawVectorParams.pClipRect;
            RVMaxNumVerticesPerTile = rawVectorParams.uMaxNumVerticesPerTile;
            RVMaxNumVisiblePointObjectsPerTile = rawVectorParams.uMaxNumVisiblePointObjectsPerTile;
            RVMaxScale = rawVectorParams.fMaxScale;
            RVMinPixelSizeForObjectVisibility = rawVectorParams.uMinPixelSizeForObjectVisibility;
            RVMinScale = rawVectorParams.fMinScale;
            RVOptimizationMinScale = rawVectorParams.fOptimizationMinScale;
            RVSimplificationTolerance = rawVectorParams.dSimplificationTolerance;
            RVStrCustomStylingFolder = rawVectorParams.strCustomStylingFolder;
            RVStrDataSource = rawVectorParams.strDataSource;
        }

        public DNSRawVector3DExtrusionParams GetRawVector3DExtrusionParams(IDNMcGridCoordinateSystem mcSourceGridCoordinateSystem, IDNMcGridCoordinateSystem mcTargetGridCoordinateSystem)
        {
            DNSRawVector3DExtrusionParams mcExtrusionParams = new DNSRawVector3DExtrusionParams();
            mcExtrusionParams.strDataSource = RV3DStrDataSource;
            mcExtrusionParams.pSourceCoordinateSystem = mcSourceGridCoordinateSystem;
            mcExtrusionParams.pTargetCoordinateSystem = mcTargetGridCoordinateSystem;
            mcExtrusionParams.aSpecificTextures = RV3DaSpecificTextures;
            mcExtrusionParams.pClipRect = RV3DClipRect;
            mcExtrusionParams.RoofDefaultTexture = RV3DRoofDefaultTexture;
            mcExtrusionParams.SideDefaultTexture = RV3DSideDefaultTexture;
            mcExtrusionParams.strHeightColumn = RV3DStrHeightColumn;
            mcExtrusionParams.strObjectIDColumn = RV3DStrObjectIDColumn;
            mcExtrusionParams.strRoofTextureIndexColumn = RV3DStrRoofTextureIndexColumn;
            mcExtrusionParams.strSideTextureIndexColumn = RV3DStrSideTextureIndexColumn;
            mcExtrusionParams.pTilingScheme = TilingScheme;

            return mcExtrusionParams;
        }

        public DNSRawVector3DExtrusionGraphicalParams GetRawVector3DExtrusionGraphicalParams(IDNMcGridCoordinateSystem mcSourceGridCoordinateSystem, IDNMcGridCoordinateSystem mcTargetGridCoordinateSystem)
        {
            DNSRawVector3DExtrusionGraphicalParams mcExtrusionGraphicalParams = new DNSRawVector3DExtrusionGraphicalParams();
            mcExtrusionGraphicalParams.aSpecificTextures = RV3DaSpecificTextures;
            mcExtrusionGraphicalParams.RoofDefaultTexture = RV3DRoofDefaultTexture;
            mcExtrusionGraphicalParams.SideDefaultTexture = RV3DSideDefaultTexture;
            mcExtrusionGraphicalParams.strHeightColumn = RV3DStrHeightColumn;
            mcExtrusionGraphicalParams.strObjectIDColumn = RV3DStrObjectIDColumn;
            mcExtrusionGraphicalParams.strRoofTextureIndexColumn = RV3DStrRoofTextureIndexColumn;
            mcExtrusionGraphicalParams.strSideTextureIndexColumn = RV3DStrSideTextureIndexColumn;

            return mcExtrusionGraphicalParams;
        }

        public void SetRawVector3DExtrusionParams(DNSRawVector3DExtrusionParams mcExtrusionParams, int sourceGridCoordId, int targetGridCoordId)
        {
            RV3DStrDataSource = mcExtrusionParams.strDataSource;
            SourceCoordinateSystemID = sourceGridCoordId;
            TargetCoordinateSystemID = targetGridCoordId;
            RV3DaSpecificTextures = mcExtrusionParams.aSpecificTextures;
            RV3DClipRect = mcExtrusionParams.pClipRect;
            RV3DRoofDefaultTexture = mcExtrusionParams.RoofDefaultTexture;
            RV3DSideDefaultTexture = mcExtrusionParams.SideDefaultTexture;
            RV3DStrHeightColumn = mcExtrusionParams.strHeightColumn;
            RV3DStrObjectIDColumn = mcExtrusionParams.strObjectIDColumn;
            RV3DStrRoofTextureIndexColumn = mcExtrusionParams.strRoofTextureIndexColumn;
            RV3DStrSideTextureIndexColumn = mcExtrusionParams.strSideTextureIndexColumn;
            TilingScheme = mcExtrusionParams.pTilingScheme;
        }

        public void GetWebServiceParams(DNSWebMapServiceParams mcWMSParams, IDNMcGridCoordinateSystem mcSourceGridCoordinateSystem)
        {
            GetNonNativeParamsFromLayer(mcWMSParams, mcSourceGridCoordinateSystem);
            mcWMSParams.strServerURL = WSStrServerURL;
            mcWMSParams.strOptionalUserAndPassword = WSStrOptionalUserAndPassword;
            mcWMSParams.bSkipSSLCertificateVerification = WSSkipSSLCertificateVerification;
            mcWMSParams.aRequestParams = WSRequestParams;
            mcWMSParams.uTimeoutInSec = WSTimeoutInSec;
            mcWMSParams.strLayersList = WSStrLayersList;
            mcWMSParams.strStylesList = WSStrStylesList;
            mcWMSParams.BoundingBox = WSBoundingBox;
            mcWMSParams.strServerCoordinateSystem = WSStrServerCoordinateSystem;
            mcWMSParams.strImageFormat = WSStrImageFormat;
            mcWMSParams.bTransparent = WSTransparent;
            mcWMSParams.strZeroBlockHttpCodes = WSStrZeroBlockHttpCodes;
        }

        public DNSWMSParams GetWMSParams(IDNMcGridCoordinateSystem mcSourceGridCoordinateSystem)
        {
            DNSWMSParams mcWMSParams = new DNSWMSParams();

            GetWebServiceParams(mcWMSParams, mcSourceGridCoordinateSystem);

            mcWMSParams.strWMSVersion = WMSStrWMSVersion;
            mcWMSParams.uBlockWidth = WMSBlockWidth;
            mcWMSParams.uBlockHeight = WMSBlockHeight;
            mcWMSParams.fMinScale = WMSMinScale;

            return mcWMSParams;
        }

        public DNSWMTSParams GetWMTSParams(IDNMcGridCoordinateSystem mcSourceGridCoordinateSystem)
        {
            DNSWMTSParams mcWMTSParams = new DNSWMTSParams();
            GetWebServiceParams(mcWMTSParams, mcSourceGridCoordinateSystem);
            mcWMTSParams.strInfoFormat = WMTSStrInfoFormat;
            mcWMTSParams.bUseServerTilingScheme = WMTSUseServerTilingScheme;
            mcWMTSParams.bExtendBeyondDateLine = WMTSExtendBeyondDateLine;
            mcWMTSParams.eCapabilitiesBoundingBoxAxesOrder = WMTSCapabilitiesBoundingBoxAxesOrder;

            return mcWMTSParams;
        }

        public DNSWCSParams GetWCSParams(IDNMcGridCoordinateSystem mcSourceGridCoordinateSystem)
        {
            DNSWCSParams mcWCSParams = new DNSWCSParams();
            GetWebServiceParams(mcWCSParams, mcSourceGridCoordinateSystem);
            mcWCSParams.strWCSVersion = WCSStrWCSVersion;
            mcWCSParams.bDontUseServerInterpolation = WCSDontUseServerInterpolation;

            return mcWCSParams;
        }

        public void SetWebServiceParams(DNSWebMapServiceParams mcWMSParams, int gridCoordId)
        {
            SetNonNativeParams(mcWMSParams, gridCoordId);
            WSStrServerURL = mcWMSParams.strServerURL;
            WSStrOptionalUserAndPassword = mcWMSParams.strOptionalUserAndPassword;
            WSSkipSSLCertificateVerification = mcWMSParams.bSkipSSLCertificateVerification;
            WSRequestParams = mcWMSParams.aRequestParams;
            WSTimeoutInSec = mcWMSParams.uTimeoutInSec;
            WSStrLayersList = mcWMSParams.strLayersList;
            WSStrStylesList = mcWMSParams.strStylesList;
            WSBoundingBox = mcWMSParams.BoundingBox;
            WSStrServerCoordinateSystem = mcWMSParams.strServerCoordinateSystem;
            WSStrImageFormat = mcWMSParams.strImageFormat;
            WSTransparent = mcWMSParams.bTransparent;
            WSStrZeroBlockHttpCodes = mcWMSParams.strZeroBlockHttpCodes;
        }

        public void SetWMSParams(DNSWMSParams mcWMSParams, int gridCoordId)
        {

            WSWebMapServiceType = DNEWebMapServiceType._EWMS_WMS;
            SetWebServiceParams(mcWMSParams, gridCoordId);
            WMSStrWMSVersion = mcWMSParams.strWMSVersion;
            WMSBlockWidth = mcWMSParams.uBlockWidth;
            WMSBlockHeight = mcWMSParams.uBlockHeight;
            WMSMinScale = mcWMSParams.fMinScale;
        }

        public void SetWMTSParams(DNSWMTSParams mcWMTSParams, int gridCoordId)
        {
            WSWebMapServiceType = DNEWebMapServiceType._EWMS_WMTS;
            SetWebServiceParams(mcWMTSParams, gridCoordId);
            WMTSStrInfoFormat = mcWMTSParams.strInfoFormat;
            WMTSUseServerTilingScheme = mcWMTSParams.bUseServerTilingScheme;
            WMTSExtendBeyondDateLine = mcWMTSParams.bExtendBeyondDateLine;
            WMTSCapabilitiesBoundingBoxAxesOrder = mcWMTSParams.eCapabilitiesBoundingBoxAxesOrder;
        }

        public void SetWCSParams(DNSWCSParams mcWCSParams, int gridCoordId)
        {
            WSWebMapServiceType = DNEWebMapServiceType._EWMS_WCS;
            SetWebServiceParams(mcWCSParams, gridCoordId);
            WCSStrWCSVersion = mcWCSParams.strWCSVersion;
            WCSDontUseServerInterpolation = mcWCSParams.bDontUseServerInterpolation;
        }

        public IDNMcNativeDtmMapLayer CreateNativeDTMLayer()
        {
            IDNMcNativeDtmMapLayer ret = null;
            try
            {
                if (m_IsSaveFromMap)
                    ret = (IDNMcNativeDtmMapLayer)Manager_MCLayers.CreateNativeDTMLayer(m_Path, m_NumLevelsToIgnore, m_LocalCacheLayerParams, false); // DNMcNativeDtmMapLayer.Create(m_Path);
                else
                    ret = (IDNMcNativeDtmMapLayer)Manager_MCLayers.CreateNativeDTMLayer(m_Path, 0, null, false); // DNMcNativeDtmMapLayer.Create(m_Path);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcNativeDtmMapLayer.Create", McEx);
                throw McEx;
            }
            return ret;
        }

        public IDNMcNativeRasterMapLayer CreateNativeRasterLayer()
        {
            IDNMcNativeRasterMapLayer ret = null;
            try
            {
                //                if (m_IsSaveFromMap)
                ret = (IDNMcNativeRasterMapLayer)Manager_MCLayers.CreateNativeRasterLayer(m_Path, m_FirstLowerQualityLevel, m_ThereAreMissingFiles, m_NumLevelsToIgnore, m_EnhanceBorderOverlap, m_LocalCacheLayerParams, false);
                /*
                                else
                                    ret = (IDNMcNativeRasterMapLayer)Manager_MCLayers.CreateNativeRasterLayer(m_Path, DNMcConstants._MC_EMPTY_ID, false, 0, false, null, false);
                */
    }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcNativeRasterMapLayer.Create", McEx);
                throw McEx;
            }
            return ret;
        }

        public IDNMcNativeVectorMapLayer CreateNativeVectorLayer()
        {
            IDNMcNativeVectorMapLayer ret = null;
            try
            {
                ret = (IDNMcNativeVectorMapLayer)Manager_MCLayers.CreateNativeVectorLayer(m_Path, m_LocalCacheLayerParams, false);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcNativeVectorMapLayer.Create", McEx);
                throw McEx;
            }
            return ret;
        }

        public IDNMcNativeVector3DExtrusionMapLayer CreateNativeVector3DExtrusionLayer()
        {
            IDNMcNativeVector3DExtrusionMapLayer ret = null;
            try
            {
                if (m_IsSaveFromMap)
                    ret = (IDNMcNativeVector3DExtrusionMapLayer)Manager_MCLayers.CreateNativeVector3DExtrusionMapLayer(m_Path, m_NumLevelsToIgnore, m_ExtrusionHeightMaxAddition, false);
                else
                    ret = (IDNMcNativeVector3DExtrusionMapLayer)Manager_MCLayers.CreateNativeVector3DExtrusionMapLayer(m_Path, 0, 0, false);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("CreateNativeVector3DExtrusionMapLayer.Create", McEx);
                throw McEx;
            }
            return ret;
        }

        public IDNMcNative3DModelMapLayer CreateNative3DModelLayer()
        {
            IDNMcNative3DModelMapLayer ret = null;
            try
            {
                if (m_IsSaveFromMap)
                    ret = (IDNMcNative3DModelMapLayer)Manager_MCLayers.CreateNative3DModelMapLayer(m_Path, m_NumLevelsToIgnore, false);
                else
                    ret = (IDNMcNative3DModelMapLayer)Manager_MCLayers.CreateNative3DModelMapLayer(m_Path, 0, false);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("CreateNative3DModelMapLayer.Create", McEx);
                throw McEx;
            }
            return ret;
        }

        public IDNMcNativeHeatMapLayer CreateHeatMapLayer()
        {
            IDNMcNativeHeatMapLayer ret = null;
            try
            {
                ret = (IDNMcNativeHeatMapLayer)Manager_MCLayers.CreateNativeHeatMapLayer(m_Path, FirstLowerQualityLevel, ThereAreMissingFiles, NumLevelsToIgnore, EnhanceBorderOverlap, LocalCacheLayerParams, false);
            }

            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcNativeHeatMapLayer.Create", McEx);
                throw McEx;
            }
            return ret;
        }

        // check heat and Material Traversability params
        public IDNMcNativeMaterialMapLayer CreateMaterialLayer()
        {
            IDNMcNativeMaterialMapLayer ret = null;
            try
            {
                ret = (IDNMcNativeMaterialMapLayer)Manager_MCLayers.CreateNativeMaterialLayer(m_Path, ThereAreMissingFiles, LocalCacheLayerParams, false);
            }

            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcNativeMaterialMapLayer.Create", McEx);
                throw McEx;
            }
            return ret;
        }

        public IDNMcNativeTraversabilityMapLayer CreateTraversabilityLayer()
        {
            IDNMcNativeTraversabilityMapLayer ret = null;
            try
            {
                ret = (IDNMcNativeTraversabilityMapLayer)Manager_MCLayers.CreateNativeTraversabilityLayer(m_Path, ThereAreMissingFiles, LocalCacheLayerParams, false);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcNativeTraversabilityMapLayer.Create", McEx);
                throw McEx;
            }
            return ret;
        }


        public IDNMcRawDtmMapLayer CreateRawDtmLayer(IDNMcGridCoordinateSystem coordSys)
        {
            IDNMcRawDtmMapLayer ret = null;

            DNSRawParams dnParams = GetRawParamsFromLayer(coordSys);

            try
            {
                ret = (IDNMcRawDtmMapLayer)Manager_MCLayers.CreateRawDTMLayer(dnParams, null, false);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("IDNMcRawDtmMapLayer.Create", McEx);
                throw McEx;
            }
            return ret;
        }

        public IDNMcRawRasterMapLayer CreateRawRasterLayer(IDNMcGridCoordinateSystem coordSys)
        {
            IDNMcRawRasterMapLayer ret = null;

            DNSRawParams dnParams = GetRawParamsFromLayer(coordSys);

            try
            {
                ret = (IDNMcRawRasterMapLayer)Manager_MCLayers.CreateRawRasterLayer(dnParams, m_RasterImageCoordinateSystem, null, false);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("IDNMcRawRasterMapLayer.Create", McEx);
                throw McEx;
            }
            return ret;
        }

        public IDNMcRawVectorMapLayer CreateRawVectorLayer(IDNMcGridCoordinateSystem coordSys, IDNMcGridCoordinateSystem targetCoordSys)
        {
            IDNMcRawVectorMapLayer ret = null;
            try
            {
                DNSRawVectorParams rawVectorParams = GetRawVectorParamsFromLayer(coordSys);

                ret = (IDNMcRawVectorMapLayer)Manager_MCLayers.CreateRawVectorLayer(
                                                           rawVectorParams,
                                                            targetCoordSys,
                                                            TilingScheme,
                                                            LocalCacheLayerParams,
                                                            false);

            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("IDNMcRawVectorMapLayer.Create", McEx);
                throw McEx;
            }
            return ret;
        }

        public IDNMcRaw3DModelMapLayer CreateRaw3DModelLayer(IDNMcGridCoordinateSystem raw3DModelCoordSys)
        {
            IDNMcRaw3DModelMapLayer ret = null;
            try
            {
                if (IsUseIndexing)
                {
                    ret = (IDNMcRaw3DModelMapLayer)Manager_MCLayers.CreateRaw3DModelLayer(
                                                               Path,
                                                               R3DOrthometricHeights,
                                                               NumLevelsToIgnore,
                                                               RSONonDefaultIndexingDataDir ? RSOIndexingDataDir : "",
                                                               false);
                }
                else
                {
                    
                    ret = (IDNMcRaw3DModelMapLayer)Manager_MCLayers.CreateRaw3DModelLayer(
                                                               Path,
                                                               raw3DModelCoordSys,
                                                               R3DOrthometricHeights,
                                                               R3DClipRect,
                                                               TargetHighestResolution,
                                                               WSRequestParams,
                                                               R3DPositionOffset,
                                                               false);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("IDNMcRaw3DModelMapLayer.Create", McEx);
                throw McEx;
            }
            return ret;
        }

        public IDNMcRawVector3DExtrusionMapLayer CreateRawVector3DExtrusionLayer(IDNMcGridCoordinateSystem sourceCoordSys, IDNMcGridCoordinateSystem targetCoordSys)
        {
            IDNMcRawVector3DExtrusionMapLayer ret = null;
            try
            {
                DNSRawVector3DExtrusionParams rawVector3DExtrusionParams = GetRawVector3DExtrusionParams(sourceCoordSys, targetCoordSys);

                if (RSOIndexingDataDir == "")
                {
                    ret = (IDNMcRawVector3DExtrusionMapLayer)Manager_MCLayers.CreateRawVector3DExtrusionLayer(
                                                               rawVector3DExtrusionParams,
                                                                ExtrusionHeightMaxAddition,
                                                                false);
                }
                else
                {
                    ret = (IDNMcRawVector3DExtrusionMapLayer)Manager_MCLayers.CreateRawVector3DExtrusionLayer(
                                                                RV3DStrDataSource,
                                                                rawVector3DExtrusionParams,
                                                                ExtrusionHeightMaxAddition,
                                                                RSONonDefaultIndexingDataDir ? RSOIndexingDataDir : "",
                                                                false);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("IDNMcRawVector3DExtrusionMapLayer.Create", McEx);
                throw McEx;
            }
            return ret;
        }

        public IDNMcNativeServerRasterMapLayer CreateNativeServerRaster()
        {
            IDNMcNativeServerRasterMapLayer ret = null;
            try
            {
                ret = (IDNMcNativeServerRasterMapLayer)Manager_MCLayers.CreateNativeServerRasterLayer(m_Path, false);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("CreateNativeServerRasterLayer.Create", McEx);
                throw McEx;
            }
            return ret;
        }

        public IDNMcNativeServerDtmMapLayer CreateNativeServerDTM()
        {
            IDNMcNativeServerDtmMapLayer ret = null;
            try
            {
                ret = (IDNMcNativeServerDtmMapLayer)Manager_MCLayers.CreateNativeServerDTMLayer(m_Path, false);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("CreateNativeServerDtmLayer.Create", McEx);
                throw McEx;
            }
            return ret;
        }

        public IDNMcNativeServer3DModelMapLayer CreateNativeServer3DModelLayer()
        {
            IDNMcNativeServer3DModelMapLayer ret = null;
            try
            {
                ret = (IDNMcNativeServer3DModelMapLayer)Manager_MCLayers.CreateNativeServer3DModelLayer(m_Path, false);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("CreateNativeServer3DModelLayer.Create", McEx);
                throw McEx;
            }
            return ret;
        }

        public IDNMcNativeServerVectorMapLayer CreateNativeServerVectorLayer()
        {
            IDNMcNativeServerVectorMapLayer ret = null;
            try
            {
                ret = (IDNMcNativeServerVectorMapLayer)Manager_MCLayers.CreateNativeServerVectorLayer(m_Path, false);
            }

            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcNativeStaticObjectsMapLayer.Create", McEx);
                throw McEx;
            }
            return ret;
        }

        internal MCTRaw3DModelParams GetRaw3DModelParams()
        {
            MCTRaw3DModelParams raw3DModelParams = new MCTRaw3DModelParams();
            raw3DModelParams.OrthometricHeights = R3DOrthometricHeights;
            raw3DModelParams.IsUseNonDefaultIndexDirectory = RSONonDefaultIndexingDataDir;
            raw3DModelParams.NonDefaultIndexingDataDirectory = RSOIndexingDataDir;
            raw3DModelParams.aRequestParams = WSRequestParams;
            raw3DModelParams.fTargetHighestResolution = TargetHighestResolution;
            raw3DModelParams.IsUseIndexing = IsUseIndexing;
            raw3DModelParams.pClipRect = R3DClipRect;
            raw3DModelParams.PositionOffset = R3DPositionOffset;

            return raw3DModelParams;
        }

        internal void SetRaw3DModelParams(MCTRaw3DModelParams raw3DModelParams)
        {
            R3DOrthometricHeights = raw3DModelParams.OrthometricHeights;
            RSONonDefaultIndexingDataDir = raw3DModelParams.IsUseNonDefaultIndexDirectory;
            RSOIndexingDataDir = raw3DModelParams.NonDefaultIndexingDataDirectory;
            WSRequestParams = raw3DModelParams.aRequestParams;
            TargetHighestResolution = raw3DModelParams.fTargetHighestResolution;
            IsUseIndexing = raw3DModelParams.IsUseIndexing;
            R3DClipRect = raw3DModelParams.pClipRect;
            R3DPositionOffset = raw3DModelParams.PositionOffset;
        }

        public IDNMcNativeServerVector3DExtrusionMapLayer CreateNativeServerVector3DExtrusionLayer()
        {
            IDNMcNativeServerVector3DExtrusionMapLayer ret = null;
            try
            {
                ret = (IDNMcNativeServerVector3DExtrusionMapLayer)Manager_MCLayers.CreateNativeServerVector3DExtrusionLayer(m_Path, false);
            }

            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcNativeStaticObjectsMapLayer.Create", McEx);
                throw McEx;
            }
            return ret;
        }

        public IDNMcNativeServerTraversabilityMapLayer CreateNativeServerTraversabilityLayer()
        {
            IDNMcNativeServerTraversabilityMapLayer ret = null;
            try
            {
                ret = (IDNMcNativeServerTraversabilityMapLayer)Manager_MCLayers.CreateNativeServerTraversabilityLayer(m_Path, false);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcNativeServerTraversabilityMapLayer.Create", McEx);
                throw McEx;
            }
            return ret;

        }

        public IDNMcNativeServerMaterialMapLayer CreateNativeServerMaterialLayer()
        {
            IDNMcNativeServerMaterialMapLayer ret = null;
            try
            {
                ret = (IDNMcNativeServerMaterialMapLayer)Manager_MCLayers.CreateNativeServerMaterialLayer(m_Path, false);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcNativeServerMaterialMapLayer.Create", McEx);
                throw McEx;
            }
            return ret;

        }

        public IDNMcWebServiceDtmMapLayer CreateWebServiceDTMLayer(IDNMcGridCoordinateSystem coordSys)
        {
            IDNMcWebServiceDtmMapLayer ret = null;
            try
            {
                if(WSWebMapServiceType == DNEWebMapServiceType._EWMS_WMS)
                    ret = (IDNMcWebServiceDtmMapLayer)Manager_MCLayers.CreateWebServiceDtmLayer(GetWMSParams(coordSys), null, false);
                else if (WSWebMapServiceType == DNEWebMapServiceType._EWMS_WMTS)
                    ret = (IDNMcWebServiceDtmMapLayer)Manager_MCLayers.CreateWebServiceDtmLayer(GetWMTSParams(coordSys), null, false);
                else //  (WSWebMapServiceType == DNEWebMapServiceType._EWMS_WCS)
                    ret = (IDNMcWebServiceDtmMapLayer)Manager_MCLayers.CreateWebServiceDtmLayer(GetWCSParams(coordSys), null, false);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("CreateWebServiceDtmLayer.Create", McEx);
                throw McEx;
            }
            return ret;
        }

        public IDNMcWebServiceRasterMapLayer CreateWebServiceRasterLayer(IDNMcGridCoordinateSystem coordSys)
        {
            IDNMcWebServiceRasterMapLayer ret = null;
            try
            {
                if (WSWebMapServiceType == DNEWebMapServiceType._EWMS_WMS)
                    ret = (IDNMcWebServiceRasterMapLayer)Manager_MCLayers.CreateWebServiceRasterLayer(GetWMSParams(coordSys), null, false);
                else if (WSWebMapServiceType == DNEWebMapServiceType._EWMS_WMTS)
                    ret = (IDNMcWebServiceRasterMapLayer)Manager_MCLayers.CreateWebServiceRasterLayer(GetWMTSParams(coordSys), null, false);
                else //  (WSWebMapServiceType == DNEWebMapServiceType._EWMS_WCS)
                    ret = (IDNMcWebServiceRasterMapLayer)Manager_MCLayers.CreateWebServiceRasterLayer(GetWCSParams(coordSys), null, false);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("CreateWebServiceRasterLayer.Create", McEx);
                throw McEx;
            }
            return ret;
        }
       

      
    }
}
