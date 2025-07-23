package com.elbit.mapcore.mcandroidtester.model;

import java.util.Observable;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeItem;

/**
 * Created by tc99382 on 12/07/2017.
 */
public class DrawPolygonSchemeForScanResult extends Observable{

    IMcObjectSchemeItem mObjectSchemeItem;
    IMcObject mObj;

    public DrawPolygonSchemeForScanResult() {
    }

    public void updateDrawPolygonResult(IMcObjectSchemeItem objectSchemeItem, IMcObject obj)
    {
        mObj=obj;
        mObjectSchemeItem=objectSchemeItem;
        setChanged();
        notifyObservers(this);
    }

    public IMcObjectSchemeItem getObjScheme() {
        return mObjectSchemeItem;
    }

    public IMcObject getObj() {
        return mObj;
    }

    /*public DrawPolygonSchemeForScanResult(IMcObjectSchemeItem mObjScheme, IMcObject mObj) {
        this.mObjectSchemeItem = mObjScheme;
        this.mObj = mObj;
    }*/
}