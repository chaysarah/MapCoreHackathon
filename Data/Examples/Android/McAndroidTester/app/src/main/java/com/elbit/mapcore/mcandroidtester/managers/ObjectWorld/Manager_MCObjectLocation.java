package com.elbit.mapcore.mcandroidtester.managers.ObjectWorld;

import java.util.HashMap;

/**
 * Created by tc99382 on 18/07/2017.
 */
public class Manager_MCObjectLocation {
    private static Manager_MCObjectLocation instance;
    private HashMap<Object,Integer> mObjectLocation;

    public Manager_MCObjectLocation() {
        mObjectLocation = new HashMap<>();
    }

    public static Manager_MCObjectLocation getInstance() {
        if (instance == null)
            instance = new Manager_MCObjectLocation();
        return instance;
    }
}
