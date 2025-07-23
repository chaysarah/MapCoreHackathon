package com.elbit.mapcore.mcandroidtester.utils;

import com.elbit.mapcore.mcandroidtester.managers.Manager_MCNames;

import com.elbit.mapcore.General.IMcErrors;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcTexture;

/**
 * Created by tc99382 on 24/01/2017.
 */
public class TextureFunc {
    public static String getTextureName(IMcTexture currTexture)
    {
        String retValue = "";
        try
        {
            retValue = currTexture.GetName();
        }
        catch (MapCoreException McEx)
        {
            if (McEx.getErrorCode() != IMcErrors.ECode.NOT_INITIALIZED)
                AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), McEx, "GetName()");
        } catch (Exception e) {
            e.printStackTrace();
        }
        return retValue;
    }
    public static String GetFullTextureDesc(IMcTexture currTexture)
    {
        return Manager_MCNames.getInstance().getNameByObject(currTexture);
    }
}
