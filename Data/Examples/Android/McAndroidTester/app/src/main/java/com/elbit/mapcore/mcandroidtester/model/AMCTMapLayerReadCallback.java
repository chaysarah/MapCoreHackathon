package com.elbit.mapcore.mcandroidtester.model;

import android.content.Context;

import com.elbit.mapcore.General.ObjectRef;
import com.elbit.mapcore.Interfaces.General.McErrors;
import com.elbit.mapcore.Structs.SMcVector3D;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;

import com.elbit.mapcore.General.IMcErrors;
import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;

/**
 * Created by tc97803 on 21/01/2018.
 */

public class AMCTMapLayerReadCallback implements IMcMapLayer.IReadCallback {

    public static int counterDelayLoadLayers = 0;
    private static AMCTMapLayerReadCallback mInstance;
    private static Context mContext;

    public static AMCTMapLayerReadCallback getInstance(Context context) {
        mContext = context;
        counterDelayLoadLayers++;
        if (mInstance == null)
            mInstance = new AMCTMapLayerReadCallback();
        return mInstance;
    }

    public static boolean IsLayersInitialized() {
        return counterDelayLoadLayers == 0;
    }

    public AMCTMapLayerReadCallback() {
    }

    @Override
    public void OnInitialized(IMcMapLayer iMcMapLayer, IMcErrors.ECode eCode, String strAdditionalDataString) {
        counterDelayLoadLayers--;
        if (eCode != IMcErrors.ECode.SUCCESS) {
            ShowError(eCode, strAdditionalDataString, "On Initialized Map Layer Failed");
        }
    }

    @Override
    public void OnReadError(IMcMapLayer iMcMapLayer, IMcErrors.ECode eCode, String strAdditionalDataString) {
        ShowError(eCode, strAdditionalDataString, "On Read Error Map Layer");
    }

    private void ShowError(IMcErrors.ECode eCode, String strAdditionalDataString, String title) {
        AlertMessages.ShowErrorMessage(mContext, title, McErrors.ErrorCodeToString(eCode) + ", " + strAdditionalDataString);
    }

    @Override
    public void OnNativeServerLayerNotValid(IMcMapLayer iMcMapLayer, boolean bLayerVersionUpdated) {

    }

    @Override
    public void OnRemoved(IMcMapLayer iMcMapLayer, IMcErrors.ECode eCode, String strAdditionalDataString) {

    }

    @Override
    public void OnReplaced(IMcMapLayer iMcMapLayer, IMcMapLayer iMcMapLayer1, IMcErrors.ECode eCode, String strAdditionalDataString) {

    }

    @Override
    public void OnPostProcessSourceData(IMcMapLayer iMcMapLayer, boolean b, IMcMapLayer.STilePostProcessData[] sTilePostProcessData) {

    }

    @Override
    public int GetSaveBufferSize() {
        return Byte.BYTES;
    }

    @Override
    public byte[] SaveToBuffer() {
        return new byte[] { 0 };
    }

}
