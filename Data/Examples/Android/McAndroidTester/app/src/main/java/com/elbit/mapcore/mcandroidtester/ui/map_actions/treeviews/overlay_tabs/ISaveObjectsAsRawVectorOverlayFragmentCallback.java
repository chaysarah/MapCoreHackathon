package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlay_tabs;

import com.elbit.mapcore.Enums.EGeometry;
import com.elbit.mapcore.Enums.GEOMETRIC_SHAPE;
import com.elbit.mapcore.Enums.GEOMETRY_TYPE;
import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;

/**
 * Created by tc97803 on 05/06/2017.
 */

public interface ISaveObjectsAsRawVectorOverlayFragmentCallback {

    void callbackSaveObjectsAsRawVectorOverlay(IMcMapViewport vp,
                                                     float cameraYawAngle,
                                                     float cameraScale,
                                                     String layerName,
                                                     boolean isMemoryBuffer,
                                                     EGeometry geometryFilter,
                                                     int actionCode);
}
