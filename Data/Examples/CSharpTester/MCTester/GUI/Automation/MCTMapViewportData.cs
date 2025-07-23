using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using MapCore;

namespace MCTester.Automation
{
    [DataContract]
    class MCTMapViewportData
    {
        [DataMember]
        public MCTCameraData CameraData { get; set; }

        [DataMember]
        public uint ViewportID { get; set; }

        [DataMember]
        public DNSMcBColor ViewportBackgroundColor { get; set; }

        [DataMember]
        public MCTMapViewportSize ViewportSize { get; set; }

        [DataMember]
        public string ViewportMapType { get; set; }

        [DataMember]
        public bool ViewportShowGeoInMetricProportion { get; set; }

        [DataMember]
        public MCTGridCoordinateSystem GridCoordinateSystem { get; set; }

        [DataMember]
        public bool TransparencyOrderingMode { get; set; }

 
        [DataMember]
        public List<string> PostProcess { get; set; }

        [DataMember]
        public string MaterialSchemeName { get; set; }

        [DataMember]
        public List<KeyValuePair<string, string>> MaterialSchemeDefinition { get; set; }

        [DataMember]
        public List<KeyValuePair<uint, int>> DebugOptions { get; set; }

        [DataMember]
        public DNEShadowMode ShadowMode { get; set; }

        [DataMember]
        public byte OneBitAlphaMode { get; set; }

        [DataMember]
        public List<string> Terrains { get; set; }

        [DataMember]
        public Dictionary<int,MCTMapTerrainData> MapTerrainsData { get; set; }

        [DataMember]
        public MCTDtmVisualizationParams DtmVisualization { get; set; }

        [DataMember]
        public MCTHeightLines HeightLines { get; set; }

        [DataMember]
        public MCTMapGrid MapGrid { get; set; }

        [DataMember]
        public Dictionary<KeyValuePair<int, int>, MCTImageProcessingData> ImageProccessingData { get; set; }

        [DataMember]
        public MCTImageProcessingData ViewportImageProccessingData { get; set; }

        [DataMember]
        public bool IsSectionMapViewport { get; set; }

        [DataMember]
        public MCTSectionMapViewport SectionMapViewportData { get; set; }

        [DataMember]
        public List<string> QuerySecondaryDTMLayers { get; set; }


        public MCTMapViewportData()
        {
            CameraData = new MCTCameraData();
            ViewportSize = new MCTMapViewportSize();
            GridCoordinateSystem = new MCTGridCoordinateSystem();
            PostProcess = new List<string>();
            MaterialSchemeName = "";
            MaterialSchemeDefinition = new List<KeyValuePair<string, string>>();
            DebugOptions = new List<KeyValuePair<uint, int>>();
            DtmVisualization = new MCTDtmVisualizationParams();
            Terrains = new List<string>();
            MapTerrainsData = new Dictionary<int, MCTMapTerrainData>();
            HeightLines = new MCTHeightLines();
            MapGrid = new MCTMapGrid();

            ImageProccessingData = new Dictionary<KeyValuePair<int, int>, MCTImageProcessingData>();
        }
    }


    [DataContract]
    class MCTMapViewportSize
    {
        [DataMember]
        public uint ViewportWidth { get; set; }

        [DataMember]
        public uint ViewportHeight { get; set; }
    }

    [DataContract]
    class MCTDtmVisualizationParams
    {
        [DataMember]
        public DNSDtmVisualizationParams Params { get; set; }

        [DataMember]
        public bool IsEnabled { get; set; }
    }

    [DataContract]
    class MCTMapGrid
    {
        [DataMember]
        public bool GridAboveVectorLayers { get; set; }

        [DataMember]
        public bool GridVisibility { get; set; }

        [DataMember]
        public bool IsUseBasicItemPropertiesOnly { get; set; }

        [DataMember]
        public List<DNSScaleStep> ScaleSteps { get; set; }

        [DataMember]
        public List<MCTGridRegion> GridRegions { get; set; }

        public MCTMapGrid()
        {
            ScaleSteps = new List<DNSScaleStep>();
            GridRegions = new List<MCTGridRegion>();
        }
    }

    [DataContract]
    class MCTGridRegion
    {
        [DataMember]
        public MCTGridCoordinateSystem pCoordinateSystem { get; set; }

        [DataMember]
        public string pGridLine { get; set; }

        [DataMember]
        public string pGridText { get; set; }

        [DataMember]
        public DNSMcBox GeoLimit { get; set; }

        [DataMember]
        public uint uFirstScaleStepIndex { get; set; }

        [DataMember]
        public uint uLastScaleStepIndex { get; set; }

        public MCTGridRegion() { }
    }

    [DataContract]
    class MCTHeightLines
    {
        [DataMember]
        public List<DNSHeightLinesScaleStep> ScaleSteps { get; set; }

        [DataMember]
        public float LineWidth { get; set; }

        [DataMember]
        public MCTColorInterpolationMode ColorInterpolationMode { get; set; }

        public MCTHeightLines()
        {
            ScaleSteps = new List<DNSHeightLinesScaleStep>();
            ColorInterpolationMode = new MCTColorInterpolationMode();
        }
    }

    [DataContract]
    class MCTColorInterpolationMode
    {
        [DataMember]
        public bool IsEnabled { get; set; }

        [DataMember]
        public float MinHeight { get; set; }

        [DataMember]
        public float MaxHeight { get; set; }

        
    }
}
