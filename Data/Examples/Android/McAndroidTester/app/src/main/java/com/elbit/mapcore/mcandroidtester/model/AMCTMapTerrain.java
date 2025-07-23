package com.elbit.mapcore.mcandroidtester.model;

import java.util.ArrayList;

import com.elbit.mapcore.Interfaces.Calculations.GridCoordSystem.IMcGridCoordinateSystem;
import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;

/**
 * Created by tc99382 on 08/08/2016.
 */
public class AMCTMapTerrain {
    private IMcGridCoordinateSystem mGridCoordinateSystem;
    private ArrayList<IMcMapLayer> mMapLayers;
    private static AMCTMapTerrain instance;

    public IMcMapLayer[] getMapLayersAsArr() {
        IMcMapLayer[] mMapLayersArr = new IMcMapLayer[mMapLayers.size()];
        int i = 0;
        for (IMcMapLayer layer : mMapLayers) {
            mMapLayersArr[i++] = layer;
        }
        return mMapLayersArr;
    }

    private AMCTMapTerrain() {
        mMapLayers = new ArrayList<>();
    }

    public ArrayList<IMcMapLayer> getmMapLayers() {
        return mMapLayers;
    }

    public void addLayer(IMcMapLayer layer)
    {
        mMapLayers.add(layer);
    }

    public void removeLayer(IMcMapLayer layer)
    {
        mMapLayers.remove(layer);
    }

    public boolean containsLayer(IMcMapLayer layer)
    {
        return mMapLayers.contains(layer);
    }

    public int getSize()
    {
        return mMapLayers.size();
    }

    public void removeAllLayers()
    {
        mMapLayers.clear();
    }

    public void setmMapLayers(ArrayList<IMcMapLayer> mMapLayers) {
        this.mMapLayers = mMapLayers;
    }

    public IMcGridCoordinateSystem getmGridCoordinateSystem() {
        return mGridCoordinateSystem;
    }

    public void setmGridCoordinateSystem(IMcGridCoordinateSystem mGridCoordinateSystem) {
        this.mGridCoordinateSystem = mGridCoordinateSystem;
    }

    public static AMCTMapTerrain getInstance() {
        if (instance == null)
            instance = new AMCTMapTerrain();
        return instance;
    }
}
