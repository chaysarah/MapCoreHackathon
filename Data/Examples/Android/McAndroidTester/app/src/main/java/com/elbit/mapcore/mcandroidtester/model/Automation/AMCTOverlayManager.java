package com.elbit.mapcore.mcandroidtester.model.Automation;

import java.util.ArrayList;

public class AMCTOverlayManager {

    public AMCTGridCoordinateSystem GridCoordinateSystem ;
    public ArrayList<String> Overlays ;

    public AMCTOverlayManager()
    {
        GridCoordinateSystem = new AMCTGridCoordinateSystem();
        Overlays = new ArrayList<>();
    }
}
