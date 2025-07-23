package com.elbit.mapcore.mcandroidtester.model;

import com.elbit.mapcore.Interfaces.Map.IMcMapGrid;

/**
 * Created by tc99382 on 13/07/2017.
 */
public class GridData {
    public GridData(IMcMapGrid.SGridRegion[] region, IMcMapGrid.SScaleStep[] scale) {
        this.region = region;
        this.scale = scale;
    }

    public IMcMapGrid.SGridRegion[] getRegion() {
        return region;
    }

    public void setRegion(IMcMapGrid.SGridRegion[] region) {
        this.region = region;
    }

    public IMcMapGrid.SScaleStep[] getScale() {
        return scale;
    }

    public void setScale(IMcMapGrid.SScaleStep[] scale) {
        this.scale = scale;
    }

    IMcMapGrid.SGridRegion [] region;
    IMcMapGrid.SScaleStep [] scale;
}
