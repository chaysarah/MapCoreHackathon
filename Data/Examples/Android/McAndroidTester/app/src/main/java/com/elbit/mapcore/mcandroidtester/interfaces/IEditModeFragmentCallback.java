package com.elbit.mapcore.mcandroidtester.interfaces;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeItem;

/**
 * Created by tc97803 on 28/11/2016.
 */
public interface IEditModeFragmentCallback {
    public enum EType {
        Init_Mode,
        Edit_Mode,
        Scan_From_Init,
        Scan_From_Edit};

    void callBackEditMode(EType type,
                          IMcObject selectObject,
                          IMcObjectSchemeItem selectedItem);

    void callBackScan();

}
