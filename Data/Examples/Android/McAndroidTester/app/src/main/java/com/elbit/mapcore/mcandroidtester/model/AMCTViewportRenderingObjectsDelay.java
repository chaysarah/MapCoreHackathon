package com.elbit.mapcore.mcandroidtester.model;

public class AMCTViewportRenderingObjectsDelay {

    int mNumToUpdate;
    boolean mRenderingEnabled;

    public AMCTViewportRenderingObjectsDelay(){}
    public AMCTViewportRenderingObjectsDelay(boolean renderingEnabled, int numToUpdate){
        mNumToUpdate = numToUpdate;
        mRenderingEnabled = renderingEnabled;
    }

    public int getNumToUpdate() {
        return mNumToUpdate;
    }

    public boolean isRenderingEnabled() {
        return mRenderingEnabled;
    }
}
