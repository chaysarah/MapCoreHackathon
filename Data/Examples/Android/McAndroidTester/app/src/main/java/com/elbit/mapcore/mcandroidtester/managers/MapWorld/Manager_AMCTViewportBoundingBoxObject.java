package com.elbit.mapcore.mcandroidtester.managers.MapWorld;


import java.util.HashMap;

import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;

/**
 * Created by TC97803 on 22/03/2018.
 */

public class Manager_AMCTViewportBoundingBoxObject {
    private HashMap<IMcMapViewport, IMcObject> mViewportBoundingBoxObject;
    private static Manager_AMCTViewportBoundingBoxObject instance;

    public Manager_AMCTViewportBoundingBoxObject()
    {
        mViewportBoundingBoxObject = new HashMap<>();
    }

    public static Manager_AMCTViewportBoundingBoxObject getInstance() {
        if (instance == null)
            instance = new Manager_AMCTViewportBoundingBoxObject();
        return instance;
    }

    public boolean isExistObject(IMcMapViewport viewport)
    {
        return (mViewportBoundingBoxObject.get(viewport) != null);
    }

    public void addObject(IMcMapViewport viewport,IMcObject object)
    {
        mViewportBoundingBoxObject.put(viewport, object);
    }

    public IMcObject getObject(IMcMapViewport viewport)
    {
        return mViewportBoundingBoxObject.get(viewport);
    }

}
