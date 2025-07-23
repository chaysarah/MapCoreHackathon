package com.elbit.mapcore.mcandroidtester.model.Automation;

import com.elbit.mapcore.mcandroidtester.model.AMCTMapDevice;

public class AMCTAutomationParams {
    public AMCTMapDevice MapDevice;
    public AMCTMapViewportData MapViewport;
    public AMCTOverlayManager OverlayManager;
    public AMCTMapsBaseDirectory MapsBaseDirectory;

    public AMCTAutomationParams()
    {
        MapViewport = new AMCTMapViewportData();
        OverlayManager = new AMCTOverlayManager();
        MapsBaseDirectory = new AMCTMapsBaseDirectory();
    }
}
