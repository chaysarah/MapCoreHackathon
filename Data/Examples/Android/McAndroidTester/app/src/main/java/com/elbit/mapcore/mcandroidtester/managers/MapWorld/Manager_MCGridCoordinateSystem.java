package com.elbit.mapcore.mcandroidtester.managers.MapWorld;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;

import com.elbit.mapcore.Interfaces.Calculations.GridCoordSystem.IMcGridCoordinateSystem;

/**
 * Created by tc99382 on 14/07/2016.
 */
public class Manager_MCGridCoordinateSystem {
    private static Manager_MCGridCoordinateSystem instance;

    public HashMap<Object, Integer> getdGridCoordSys() {
        return dGridCoordSys;
    }

    public ArrayList<String> getmGridCoordSysNames() {
        return mGridCoordSysNames;
    }

    ArrayList<String> mGridCoordSysNames;
    private static HashMap<Object, Integer> dGridCoordSys;

    protected Manager_MCGridCoordinateSystem() {
        dGridCoordSys = new HashMap<>();
        mGridCoordSysNames = new ArrayList<>();
    }

    public void AddNewGridCoordinateSystem(IMcGridCoordinateSystem gridCoordSys) {
        if(gridCoordSys != null) {
            if (!dGridCoordSys.containsKey(gridCoordSys))
                dGridCoordSys.put(gridCoordSys, dGridCoordSys.size());
            mGridCoordSysNames.add(gridCoordSys.getClass().getSimpleName());
        }
    }

    public static Manager_MCGridCoordinateSystem getInstance() {
        if (instance == null) {
            instance = new Manager_MCGridCoordinateSystem();
        }
        return instance;
    }

    public static HashMap GetChildren(Object Parent) {
        HashMap<Object, Integer> Ret = new HashMap<>();

        return Ret;
    }

    public String getCoorSysName(Object key) {
        return getdGridCoordSys().get(key).toString();
    }

}
