package com.elbit.mapcore.mcandroidtester.interfaces;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;

/**
 * Created by tc97803 on 06/02/2017.
 */

public interface IOverlayManagerShowLockFragment {

    public enum EItemsType {ObjectScheme , ConditionalSelector};

    void getObject(IMcOverlayManager overlayManager);
}
