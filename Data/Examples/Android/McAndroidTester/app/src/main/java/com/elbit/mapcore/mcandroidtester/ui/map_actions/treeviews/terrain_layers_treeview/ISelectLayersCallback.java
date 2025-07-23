package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.terrain_layers_treeview;

import java.util.ArrayList;

import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcMapTerrain;

/**
 * Created by tc97803 on 17/01/2018.
 */

public interface ISelectLayersCallback {
    void callbackSelectLayers(IMcMapTerrain terrain, ArrayList<IMcMapLayer> selectLayers);

}
