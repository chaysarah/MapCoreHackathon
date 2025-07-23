package com.elbit.mapcore.mcandroidtester.managers.MapWorld;

import java.util.HashMap;

import com.elbit.mapcore.Interfaces.Map.IMcMapHeightLines;

/**
 * Created by tc97803 on 07/02/2018.
 */

public class Manager_MCMapHeightLines {

    private static Manager_MCMapHeightLines instance;
    public HashMap<Object, Integer> mdHeightLines;

    Manager_MCMapHeightLines()
    {
        mdHeightLines = new HashMap<>();
    }

    public void AddNewHeightLines(IMcMapHeightLines mapGrid)
    {
        if (!mdHeightLines.containsKey(mapGrid))
            mdHeightLines.put(mapGrid, mdHeightLines.size());
    }

    public HashMap<Object, Integer> getAllParams()
    {
        return mdHeightLines;
    }

    public static Manager_MCMapHeightLines getInstance() {
        if (instance == null) {
            instance = new Manager_MCMapHeightLines();
        }
        return instance;
    }
}
