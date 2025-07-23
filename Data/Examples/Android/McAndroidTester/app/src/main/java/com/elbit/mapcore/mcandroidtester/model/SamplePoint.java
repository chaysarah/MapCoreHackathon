package com.elbit.mapcore.mcandroidtester.model;

import java.util.Observable;

/**
 * Created by TC99382 on 22/06/2017.
 */
public class SamplePoint extends Observable {
    public float mX;
    public float mY;
    public void updateSamplePoint(float x, float y)
    {
        mX=x;
        mY=y;
        setChanged();
        notifyObservers();
    }

}
