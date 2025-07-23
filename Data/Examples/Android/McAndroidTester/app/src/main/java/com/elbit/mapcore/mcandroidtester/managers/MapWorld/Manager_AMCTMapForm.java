package com.elbit.mapcore.mcandroidtester.managers.MapWorld;

import android.content.Context;
import android.graphics.Bitmap;
import android.util.Size;

import com.elbit.mapcore.Classes.OverlayManager.McTexture;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcTexture;
import com.elbit.mapcore.Structs.SMcRect;
import com.elbit.mapcore.Structs.SMcVector2D;
import com.elbit.mapcore.mcandroidtester.model.AMCTMapForm;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;

import java.io.File;
import java.io.FileOutputStream;
import java.nio.ByteBuffer;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Set;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;

import androidx.appcompat.widget.AppCompatImageView;
/**
 * Created by tc97803 on 06/02/2018.
 */

public class Manager_AMCTMapForm {
    private static Manager_AMCTMapForm instance;
    private AMCTMapForm mCurMapForm;
    private int mLastAddedViewportHashCode;
    private IMcMapViewport mCurViewport;
    private HashMap<IMcMapViewport, AMCTMapForm> mAllForms;

    public int getLastAddedViewportHashCode() {
        return mLastAddedViewportHashCode;
    }

    public AMCTMapForm getCurMapForm() {
        return mCurMapForm;
    }

    public IMcMapViewport getCurViewport() {
        return mCurViewport;
    }

    public HashMap<IMcMapViewport, AMCTMapForm> getAllForms() {
        return mAllForms;
    }

    private Manager_AMCTMapForm() {
        mAllForms = new HashMap<>();
    }

    public ArrayList<IMcMapViewport> getAllImcViewports() {
        ArrayList<IMcMapViewport> iMcMapViewports = new ArrayList<>(mAllForms.size());
        iMcMapViewports.addAll(mAllForms.keySet());
        return iMcMapViewports;
    }

    public boolean isAnyViewportExist() {
        return mAllForms != null && mAllForms.size() > 0;
    }

    public Set<Integer> getViewPortsHashCodes() {
        Set<Integer> viewPortsHashCodes = new HashSet<>(mAllForms.size());
        for (IMcMapViewport vp : mAllForms.keySet()) {
            viewPortsHashCodes.add(vp.hashCode());
        }
        return viewPortsHashCodes;
    }

    public IMcMapViewport getViewportById(int viewportId) {
        for (IMcMapViewport vp : mAllForms.keySet()) {
            if (viewportId == vp.hashCode())
                return vp;
        }
        return null;
    }

    public void AllViewportsResize(int width, int height) {
        for (IMcMapViewport vp : mAllForms.keySet()) {
            try {
                vp.ViewportResized(width, height);
            } catch (MapCoreException e) {
                e.printStackTrace();
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    public void addMapForm(IMcMapViewport pViewport, AMCTMapForm currentMap) {
        //Add the map to the dictionary
        mAllForms.put(pViewport, currentMap);
        mLastAddedViewportHashCode = pViewport.hashCode();
        //activateMapForm(pViewport);
    }

    public void activateMapForm(IMcMapViewport curVp) {
        //set the active map
        if (curVp != null) {
            if (mAllForms.get(curVp) != null) {
                mCurMapForm = mAllForms.get(curVp);
                mCurViewport = curVp;
            }
        }
    }

    public AMCTMapForm getMapFormByVp(IMcMapViewport curVp) {
        if (curVp != null) {
            if (mAllForms.containsKey(curVp)) {
                return mAllForms.get(curVp);
            }
        }
        return null;
    }

    public void removeViewport(IMcMapViewport viewport) {
        AMCTMapForm mapForm = mAllForms.get(viewport);


        if (mapForm != null)
            removeEditMode(mapForm);
        mAllForms.remove(viewport);
    }

    public void ResetCurrViewport() {
        mCurViewport = null;
        mCurMapForm = null;
    }

    private void removeEditMode(AMCTMapForm mapForm) {
        if (mapForm != null) {
            if (mapForm.getEditMode() != null) {
                mapForm.getEditMode().Destroy();
                mapForm.setEditMode(null);
            }
        }
    }

    public ArrayList<IMcMapViewport> getOMViewports(Context context, IMcObject iMcObject) {
        ArrayList<IMcMapViewport> allVP = getAllImcViewports();
        ArrayList<IMcMapViewport> omViewports = new ArrayList<>();
        try {
            IMcOverlayManager overlayManager = iMcObject.GetOverlayManager();
            for (IMcMapViewport vp : allVP) {

                if (vp.GetOverlayManager() == overlayManager) {
                    omViewports.add(vp);
                }
            }
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(context, e, "GetOverlayManager");
        } catch (Exception e) {
            e.printStackTrace();
        }

        return omViewports;
    }

    public static Manager_AMCTMapForm getInstance() {
        if (instance == null) {
            instance = new Manager_AMCTMapForm();
        }
        return instance;
    }

    /*public void RenderScreenRectToBuffer(IMcMapViewport viewport,
                                                int numBufferRawPitch,
                                                int widthDimension,
                                                int heightDimension,
                                                SMcVector2D pointTopLeft,
                                                SMcVector2D pointBottomRight,
                                                Context context)
    {
        RenderScreenRectToBuffer(viewport,
         numBufferRawPitch,
         widthDimension,
         heightDimension,
         pointTopLeft,
         pointBottomRight,
                context,
                "");
    }

    public  void RenderScreenRectToBuffer(IMcMapViewport viewport,
                                                int numBufferRawPitch,
                                                int widthDimension,
                                                int heightDimension,
                                                SMcVector2D pointTopLeft,
                                                SMcVector2D pointBottomRight,
                                                Context context,
                                                String fileName)
    {
        RenderScreenRectToBuffer(viewport,
                numBufferRawPitch,
                widthDimension,
                heightDimension,
                pointTopLeft,
                pointBottomRight,
                context,
                fileName,
                false);
    }*/

    // must run on gl thread
    public void RenderScreenRectToBuffer(final IMcMapViewport mViewport,
                                         final int numBufferRawPitch,
                                         final int widthDimension,
                                         final int heightDimension,
                                         final SMcVector2D pointTopLeft,
                                         final SMcVector2D pointBottomRight,
                                         final Context context,
                                         final String fileName,
                                         final boolean isClosePic,
                                         final IMcTexture.EPixelFormat pixelFormat) {

        Integer pixelFormatByteCount = 0;
        try {
            pixelFormatByteCount = McTexture.Static.GetPixelFormatByteCount(pixelFormat);
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(context, e, "RenderScreenRectToBuffer");
        } catch (Exception e) {
            e.printStackTrace();
        }

        Integer stride = 0;
        if (numBufferRawPitch == 0)
            stride = widthDimension * pixelFormatByteCount;
        else
            stride = numBufferRawPitch * pixelFormatByteCount;

        final int bufferSize = stride * heightDimension;

        final IMcTexture.EPixelFormat finalPixelFormat = pixelFormat;
        try {
            Bitmap.Config pixFormat;

            switch (finalPixelFormat) {
                case EPF_R5G6B5:
                case EPF_A1R5G5B5:
                case EPF_B5G6R5:
                    pixFormat = Bitmap.Config.RGB_565;
                    break;
                case EPF_R8G8B8:
                case EPF_B8G8R8:
                case EPF_X8B8G8R8:
                case EPF_X8R8G8B8:
                case EPF_A8R8G8B8:
                case EPF_A8B8G8R8:
                case EPF_B8G8R8A8:
                case EPF_R8G8B8A8:
                    pixFormat = Bitmap.Config.ARGB_8888;
                    break;
                case EPF_A4R4G4B4:
                    pixFormat = Bitmap.Config.ARGB_4444;
                    break;
                case EPF_A8:
                    pixFormat = Bitmap.Config.ALPHA_8;
                    break;
                default:
                    AlertMessages.ShowErrorMessage(context, "Pixel Format", "Pixel format not supported");
                    return;
            }
            byte[] arrBuffer = new byte[bufferSize];
            SMcRect rect = new SMcRect((int) pointTopLeft.x, (int) pointTopLeft.y, (int) pointBottomRight.x, (int) pointBottomRight.y);
            mViewport.RenderScreenRectToBuffer(rect,
                    widthDimension,
                    heightDimension,
                    pixelFormat,
                    numBufferRawPitch,
                    arrBuffer);

            if (widthDimension >= 1 && heightDimension >= 1) {
                final Size imageSize = new Size(widthDimension, heightDimension);
                final byte[] arrBuffer2 = arrBuffer;
                final Bitmap.Config finalPixFormat = pixFormat;

                Bitmap bitmap = Bitmap.createBitmap(imageSize.getWidth(), imageSize.getHeight(), finalPixFormat);
                ByteBuffer buffer = ByteBuffer.wrap(arrBuffer2);
                bitmap.copyPixelsFromBuffer(buffer);
                final Bitmap finalBitmap = bitmap;
                try {
                    FileOutputStream out = new FileOutputStream(fileName);
                    File file = new File(fileName);
                    String extension = file.getAbsolutePath().substring(file.getAbsolutePath().lastIndexOf(".") + 1);
                    Bitmap.CompressFormat format = Bitmap.CompressFormat.PNG;
                    if (extension.toLowerCase().compareTo("jpeg") == 0 || extension.toLowerCase().compareTo("jpg") == 0)
                        format = Bitmap.CompressFormat.JPEG;
                    finalBitmap.compress(format, 90, out);
                    out.flush();
                    out.close();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(context, e, "RenderScreenRectToBuffer");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
