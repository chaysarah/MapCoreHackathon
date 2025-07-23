using MapCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTester.MapWorld.MagicForm
{
    class MCTSLayerData
    {
        public DNELayerType LayerType;
        public string LayerPath;
        public DNSLocalCacheLayerParams? LayerSubFolderLocalCache;
        public uint NoOfLayers = 1;
    }
    class MCTSLayerDataTraversability : MCTSLayerData
    {
        public bool ThereAreMissingFiles;
    }

    // Raster and Heat
    class MCTSLayerDataRaster : MCTSLayerDataTraversability
    {
        public uint FirstLowerQualityLevel;
        public uint NumLevelsToIgnore;
        public bool EnhanceBorderOverlap;
    }

    class MCTSLayerDataDTM : MCTSLayerData
    {
        public uint NumLevelsToIgnore;
    }

    class MCTSLayerDataVector3DExtrusion : MCTSLayerDataDTM
    {
        public float ExtrusionHeightMaxAddition;
    }

    class MCTSLayerDataRawDTM : MCTSLayerData
    {
        public DNSRawParams Params;
    }

    class MCTSLayerDataRawRaster : MCTSLayerDataRawDTM
    {
        public bool ImageCoordSys;
    }

    class MCTSLayerDataRawVector : MCTSLayerData
    {
        public DNSRawVectorParams Params;
        public IDNMcGridCoordinateSystem GridCoordSys;
        public DNSTilingScheme TilingScheme;
    }
    
    class MCTSLayerDataRawVector3DExtrusionGraphical : MCTSLayerData
    {
        public DNSRawVector3DExtrusionGraphicalParams Params;
        public float ExtrusionHeightMaxAddition;
        public string StrIndexingDataDirectory;
    }

    class MCTSLayerDataRawVector3DExtrusion : MCTSLayerData
    {
        public DNSRawVector3DExtrusionParams Params;
        public float ExtrusionHeightMaxAddition;
    }

    class MCTSLayerDataRaw3DModel : MCTSLayerDataDTM
    {
        public float ExtrusionHeightMaxAddition;
        public bool OrthometricHeights;
        public string StrIndexingDataDirectory;
    }

    class MCTSLayerDataWebService : MCTSLayerData
    {
        public DNEWebMapServiceType WebMapServiceType;
        public DNSWebMapServiceParams Params;
    }

   /* class MCTSLayerDataWMS : MCTSLayerData
    {
      //  public DNEWebMapServiceType webMapServiceType
        public DNSWMSParams Params;
    }

    class MCTSLayerDataWMTS : MCTSLayerData
    {
        public DNSWMTSParams Params;
    }

    class MCTSLayerDataWCS : MCTSLayerData
    {
        public DNSWMSParams Params;
    }*/
}
