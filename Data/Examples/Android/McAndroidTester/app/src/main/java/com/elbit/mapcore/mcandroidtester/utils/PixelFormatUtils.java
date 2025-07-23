package com.elbit.mapcore.mcandroidtester.utils;

import android.content.Context;
import android.graphics.Bitmap;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcTexture;

/**
 * Created by tc99382 on 06/02/2017.
 */
public class PixelFormatUtils {

    public static Bitmap.Config convertEPixelFormatToPixelFormat(Context context, IMcTexture.EPixelFormat ePixelFormat) {
        Bitmap.Config pixFormat = null;

        switch (ePixelFormat) {
            case EPF_R5G6B5:
                pixFormat = Bitmap.Config.RGB_565;
                break;
            case EPF_A8R8G8B8:
                pixFormat = Bitmap.Config.ARGB_8888;
                break;
            case EPF_A8:
                pixFormat = Bitmap.Config.ALPHA_8;
                break;
            default:
                AlertMessages.ShowErrorMessage(context, "Pixel Format", "Pixel format not supported");
        }
        return pixFormat;
    }

    public static IMcTexture.EPixelFormat convertPixelFormatToEPixelFormat(Bitmap.Config pixelFormat) {
        IMcTexture.EPixelFormat eRet;
        switch (pixelFormat) {
            case RGB_565:
                eRet = IMcTexture.EPixelFormat.EPF_R5G6B5;
                break;
            case ARGB_8888:
                eRet = IMcTexture.EPixelFormat.EPF_A8R8G8B8;
                break;
            case ALPHA_8:
                eRet = IMcTexture.EPixelFormat.EPF_A8;
            default:
                eRet = IMcTexture.EPixelFormat.EPF_UNKNOWN;
        }
        return eRet;

    }
}