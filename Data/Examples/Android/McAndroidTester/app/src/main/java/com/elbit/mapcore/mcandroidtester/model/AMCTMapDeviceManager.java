package com.elbit.mapcore.mcandroidtester.model;

/**
 * Created by tc99382 on 11/07/2016.
 */
public class AMCTMapDeviceManager {

    private static AMCTMapDevice instance;

    protected AMCTMapDeviceManager() {
    }


    public static AMCTMapDevice getInstance() {
        if (instance == null)
            instance = new AMCTMapDevice();
            return instance;
    }
}