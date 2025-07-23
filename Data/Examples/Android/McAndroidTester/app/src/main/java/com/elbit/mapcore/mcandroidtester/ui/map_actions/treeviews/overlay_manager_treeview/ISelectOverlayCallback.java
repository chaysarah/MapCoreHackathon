package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlay_manager_treeview;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlay;

/**
 * Created by TC97803 on 05/03/2018.
 */

public interface ISelectOverlayCallback {
    void SelectOverlayCallback(IMcOverlay overlay, IMcObject object);
}
