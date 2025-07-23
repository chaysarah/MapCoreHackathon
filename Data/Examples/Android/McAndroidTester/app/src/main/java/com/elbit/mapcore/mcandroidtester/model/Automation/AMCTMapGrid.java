package com.elbit.mapcore.mcandroidtester.model.Automation;

import com.elbit.mapcore.Interfaces.Map.IMcMapGrid;

import java.util.ArrayList;

public class AMCTMapGrid {

    public boolean GridAboveVectorLayers;
    public boolean GridVisibility;
    public boolean IsUseBasicItemPropertiesOnly;
    public ArrayList<IMcMapGrid.SScaleStep> ScaleStep;
    public ArrayList<AMCTGridRegion> GridRegion;

    public AMCTMapGrid()
    {
        ScaleStep = new ArrayList<IMcMapGrid.SScaleStep>();
        GridRegion = new ArrayList<AMCTGridRegion>();
    }
}
