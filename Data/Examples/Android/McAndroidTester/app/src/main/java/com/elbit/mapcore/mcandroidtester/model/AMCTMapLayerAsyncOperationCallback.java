package com.elbit.mapcore.mcandroidtester.model;

import android.content.Context;

import com.elbit.mapcore.General.IMcErrors;
import com.elbit.mapcore.Interfaces.General.McErrors;
import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;
import com.elbit.mapcore.Structs.SMcVariantID;
import com.elbit.mapcore.Structs.SMcVector3D;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.layers_tabs.VectorMetadataMapLayerFragment;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;

/**
 * Created by tc97803 on 21/01/2018.
 */

public class AMCTMapLayerAsyncOperationCallback implements IMcMapLayer.IAsyncOperationCallback {

    private static AMCTMapLayerAsyncOperationCallback mInstance;
    private static Context mContext;
    public VectorMetadataMapLayerFragment VectorMetadataMapLayerFragment;

    public static AMCTMapLayerAsyncOperationCallback getInstance(Context context)
    {
        mContext = context;
        if (mInstance == null)
            mInstance = new AMCTMapLayerAsyncOperationCallback();
        return mInstance;
    }

    public AMCTMapLayerAsyncOperationCallback()
    {}

    @Override
    public void OnVectorItemPointsResult(IMcMapLayer iMcMapLayer, IMcErrors.ECode eCode, SMcVector3D[][] sMcVector3DS) {
        if (eCode == IMcErrors.ECode.SUCCESS)
        {
            //VectorMetadataMapLayerFragment.GetQuery(auVectorItemsID);
        }
        else
            AlertMessages.ShowMessage(mContext, "On Vector Item Points Result Callback", McErrors.ErrorCodeToString(eCode));

    }

    @Override
    public void OnScanExtendedDataResult(IMcMapLayer iMcMapLayer, IMcErrors.ECode eCode, IMcMapLayer.SVectorItemFound[] sVectorItemFounds, SMcVector3D[] sMcVector3D) {
        if (eCode == IMcErrors.ECode.SUCCESS)
        {
            //VectorMetadataMapLayerFragment.GetQuery(auVectorItemsID);
        }
        else
            AlertMessages.ShowMessage(mContext, "On Vector Query Result Callback", McErrors.ErrorCodeToString(eCode));

    }

    @Override
    public void OnFieldUniqueValuesResult(IMcMapLayer pMapLayer, IMcErrors.ECode eCode, IMcMapLayer.EVectorFieldReturnedType eReturnedType, Object paUniqueValues)
    {
        if (eCode == IMcErrors.ECode.SUCCESS)
        {
            if(VectorMetadataMapLayerFragment != null && VectorMetadataMapLayerFragment.mVectorsMapLayer == pMapLayer) {
                if (eReturnedType == IMcMapLayer.EVectorFieldReturnedType.EVFRT_INT && paUniqueValues != null)
                    VectorMetadataMapLayerFragment.GetFieldUniqueValuesAsInt((int[]) paUniqueValues);
                if (eReturnedType == IMcMapLayer.EVectorFieldReturnedType.EVFRT_DOUBLE && paUniqueValues != null)
                    VectorMetadataMapLayerFragment.GetFieldUniqueValuesAsDouble((double[]) paUniqueValues);
                if ((eReturnedType == IMcMapLayer.EVectorFieldReturnedType.EVFRT_STRING || eReturnedType == IMcMapLayer.EVectorFieldReturnedType.EVFRT_WSTRING) && paUniqueValues != null)
                    VectorMetadataMapLayerFragment.GetFieldUniqueValuesAsString((String[]) paUniqueValues);
            }
        }
        else
            AlertMessages.ShowMessage(mContext, "Field Unique Values Result Callback", McErrors.ErrorCodeToString(eCode));
    }

    @Override
    public void OnVectorItemFieldValueResult(IMcMapLayer pMapLayer, IMcErrors.ECode eCode, IMcMapLayer.EVectorFieldReturnedType eReturnedType, Object pValue)
    {
        if (eCode == IMcErrors.ECode.SUCCESS)
        {
            if(VectorMetadataMapLayerFragment != null && VectorMetadataMapLayerFragment.mVectorsMapLayer == pMapLayer) {
                if (eReturnedType == IMcMapLayer.EVectorFieldReturnedType.EVFRT_INT && pValue != null)
                    VectorMetadataMapLayerFragment.GetVectorItemFieldValuesAsInt((int) pValue);
                if (eReturnedType == IMcMapLayer.EVectorFieldReturnedType.EVFRT_DOUBLE && pValue != null)
                    VectorMetadataMapLayerFragment.GetVectorItemFieldValuesAsDouble((double) pValue);
                if ((eReturnedType == IMcMapLayer.EVectorFieldReturnedType.EVFRT_STRING || eReturnedType == IMcMapLayer.EVectorFieldReturnedType.EVFRT_WSTRING) && pValue != null)
                    VectorMetadataMapLayerFragment.GetVectorItemFieldValuesAsString((String) pValue);
            }
        }
        else
            AlertMessages.ShowMessage(mContext, "Vector Item Field Value Result Callback", McErrors.ErrorCodeToString(eCode));
    }

    @Override
    public void OnVectorQueryResult(IMcMapLayer iMcMapLayer, IMcErrors.ECode eCode, long[] ints) {
        if (eCode == IMcErrors.ECode.SUCCESS)
        {
            //VectorMetadataMapLayerFragment.GetQuery(auVectorItemsID);
        }
        else
            AlertMessages.ShowMessage(mContext, "On Vector Query Result Callback", McErrors.ErrorCodeToString(eCode));

    }

    @Override
    public void OnWebServerLayersResults(IMcErrors.ECode eCode, String s, IMcMapLayer.EWebMapServiceType eWebMapServiceType, IMcMapLayer.SServerLayerInfo[] sServerLayerInfos, String[][] strings, String s1) {

    }

    @Override
    public void On3DModelSmartRealityDataResults(IMcErrors.ECode eCode, String s, SMcVariantID sMcVariantID, IMcMapLayer.SSmartRealityBuildingHistory[] sSmartRealityBuildingHistories) {

    }
}
