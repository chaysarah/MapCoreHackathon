package com.elbit.mapcore.mcandroidtester.managers.MapWorld;

import java.util.HashMap;

import com.elbit.mapcore.Interfaces.Map.IMcMapGrid;

/**
 * Created by tc97803 on 07/02/2018.
 */

public class Manager_MCMapGrid {
    private static Manager_MCMapGrid mInstance;
    public static HashMap<Object, Integer> mdGrid;

    Manager_MCMapGrid()
    {
        mdGrid = new HashMap<>();
    }

    public void AddNewMapGrid(IMcMapGrid mapGrid)
    {
        if (!mdGrid.containsKey(mapGrid))
            mdGrid.put(mapGrid, mdGrid.size());
    }

    public HashMap<Object, Integer> getAllParams()
    {
       return mdGrid;
    }

    public static Manager_MCMapGrid getInstance() {
        if (mInstance == null) {
            mInstance = new Manager_MCMapGrid();
        }
        return mInstance;
    }
}
