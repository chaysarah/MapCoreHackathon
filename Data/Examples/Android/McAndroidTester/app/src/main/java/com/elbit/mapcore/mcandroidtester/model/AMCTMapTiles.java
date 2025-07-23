package com.elbit.mapcore.mcandroidtester.model;

import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_AMCTMapForm;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.BaseApplication;

import java.util.ArrayList;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

/**
 * Created by TC97803 on 18/02/2018.
 */

public class AMCTMapTiles {
    public enum MapTilesKey {
        D, B, W, S, Stat;
    }

    public static void SetDebugOption(final MapTilesKey key) {
        final IMcMapViewport viewport = Manager_AMCTMapForm.getInstance().getCurViewport();
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    int debugOptionsKey = Integer.MAX_VALUE;
                    int debugOptionsVal = 0;
                    if (key == MapTilesKey.D) {
                        debugOptionsKey = 0/*ELO_BOX_DRAW_MODE*/;
                        debugOptionsVal = viewport.GetDebugOption(debugOptionsKey);
                        debugOptionsVal = (debugOptionsVal + 1) % 3;
                    } else if (key == MapTilesKey.B) {
                        debugOptionsKey = 2/*ELO_OVERLAY_OBJECTS_BOX_DRAW_MODE*/;
                        debugOptionsVal = viewport.GetDebugOption(debugOptionsKey);
                        if (debugOptionsVal == 0) {
                            debugOptionsVal = 1;
                        } else {
                            debugOptionsVal = 0;
                        }
                    } else if (key == MapTilesKey.W) {
                        debugOptionsKey = 21; //ELO_WIREFRAME_MODE
                        debugOptionsVal = viewport.GetDebugOption(debugOptionsKey);
                        debugOptionsVal = (debugOptionsVal + 1) % 3;
                    } else if ((key == MapTilesKey.S)) {
                        debugOptionsKey = 4; //ELO_SAVE_INTERSECTING_TILE
                        debugOptionsVal = viewport.GetDebugOption(debugOptionsKey);
                        debugOptionsVal = (debugOptionsVal + 1) % 2;
                    }
                    else if ((key == MapTilesKey.Stat)) {
                        debugOptionsKey = 24; //EGO_VIEWPORT_STATS
                        debugOptionsVal = viewport.GetDebugOption(debugOptionsKey);
                        debugOptionsVal = (debugOptionsVal + 1) % 2;
                    }
                    if (debugOptionsKey != Integer.MAX_VALUE) {
                        viewport.SetDebugOption(debugOptionsKey, debugOptionsVal);
                    }
                }
                catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "GetCameras");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }
}

