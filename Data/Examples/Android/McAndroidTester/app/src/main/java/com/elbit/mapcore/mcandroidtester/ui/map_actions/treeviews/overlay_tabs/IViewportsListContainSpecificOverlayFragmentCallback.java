package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlay_tabs;

import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;

/**
 * Created by tc97803 on 05/06/2017.
 */

public interface IViewportsListContainSpecificOverlayFragmentCallback {

    void callbackViewportsListContainSpecificOverlay(IMcMapViewport vp,
                                                     float minScale,
                                                     float maxScale,
                                                     IMcMapLayer.STilingScheme tilingScheme,
                                                     int actionCode);
}
