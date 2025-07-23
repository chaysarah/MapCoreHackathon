package com.elbit.mapcore.mcandroidtester.model.Automation;

import com.elbit.mapcore.Structs.SMcBColor;

import java.util.ArrayList;
import java.util.HashMap;

public class AMCTMapViewportData {

    public AMCTCameraData CameraData;
    public int ViewportID;
    public SMcBColor ViewportBackgroundColor;
    public AMCTMapViewportSize ViewportSize;
    public String ViewportMapType;
    public boolean ViewportShowGeoInMetricProportion;
    public AMCTGridCoordinateSystem GridCoordinateSystem;
    public boolean TransparencyOrderingMode;
    public ArrayList<String> PostProcess;
    public String MaterialSchemeName;
    public HashMap<String, String> MaterialSchemeDefinition;
    public HashMap<Integer, Integer> DebugOptions;
    public int ShadowMode;
    public byte OneBitAlphaMode;
    public ArrayList<String> Terrains;
    public ArrayList<AMCTKeyValueMapTerrainData> MapTerrainsData;
    public AMCTDtmVisualizationParams DtmVisualization;
    public AMCTHeightLines HeightLines;
    public AMCTMapGrid MapGrid;
    public ArrayList<AMCTKeyValueImageProcessingData> ImageProccessingData;
    public AMCTImageProcessingData ViewportImageProccessingData;

    public AMCTMapViewportData() {
        CameraData = new AMCTCameraData();
        ViewportSize = new AMCTMapViewportSize();
        GridCoordinateSystem = new AMCTGridCoordinateSystem();
        PostProcess = new ArrayList<>();
        MaterialSchemeName = "";
        MaterialSchemeDefinition = new HashMap<>();
        DebugOptions = new HashMap<>();
        DtmVisualization = new AMCTDtmVisualizationParams();
        Terrains = new ArrayList<>();
        MapTerrainsData = new ArrayList<>();
        HeightLines = new AMCTHeightLines();
        ImageProccessingData = new ArrayList<AMCTKeyValueImageProcessingData>();
        ViewportImageProccessingData = new AMCTImageProcessingData();
    }

}
