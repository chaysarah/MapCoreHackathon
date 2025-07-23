package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.viewport_terrain_treeview;

import com.elbit.mapcore.Interfaces.Map.IMcMapGrid;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;

/**
 * Created by tc97803 on 07/02/2018.
 */

public interface ISelectGridCallback {
    void callbackSelectGrid(IMcMapViewport viewport, IMcMapGrid selectGrid);
}
