package com.elbit.mapcore.mcandroidtester.utils;

import android.app.Activity;
import android.content.Context;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import android.os.Handler;
import android.util.SparseBooleanArray;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ExpandableListView;
import android.widget.ListAdapter;
import android.widget.ListView;

import com.elbit.mapcore.General.ObjectRef;
import com.elbit.mapcore.Interfaces.Calculations.IMcSpatialQueries;
import com.elbit.mapcore.Interfaces.Map.IMcMapCamera;
import com.elbit.mapcore.Interfaces.Map.IMcMapDevice;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Structs.SMcVector3D;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.managers.ObjectWorld.Manager_MCConditionalSelectorObjects;
import com.elbit.mapcore.mcandroidtester.model.AMCTMapDevice;
import com.elbit.mapcore.mcandroidtester.ui.map.AMcGLSurfaceViewRenderer;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.MapFragment;
import com.elbit.mapcore.mcandroidtester.ui.objects.ObjectPropertiesBase;

import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;

import com.elbit.mapcore.Enums.EMcPointCoordSystem;
import com.elbit.mapcore.General.CMcEnumBitField;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcConditionalSelector;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcLocationConditionalSelector;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectScheme;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeNode;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;

/**
 * Created by tc99382 on 13/07/2016.
 */
public class Funcs {
    public static void enableDisableViewGroup(ViewGroup viewGroup, boolean enabled) {
        int childCount = viewGroup.getChildCount();
        for (int i = 0; i < childCount; i++) {
            View view = viewGroup.getChildAt(i);
            view.setEnabled(enabled);
            if (view instanceof ViewGroup) {
                enableDisableViewGroup((ViewGroup) view, enabled);
            }
        }
    }

    public static void runMapCoreFunc(Runnable mapCoreRunnable) {
        if (AMcGLSurfaceViewRenderer.getmGlSurfaceView() != null) {
            AMcGLSurfaceViewRenderer.getmGlSurfaceView().queueEvent(mapCoreRunnable);
        } else
            mapCoreRunnable.run();
    }

    public static Runnable getPerformPendingCalculationsRunnable(Handler handler, Context context) {
        return new Runnable() {
            @Override
            public void run() {
                try {
                    // Enqueue the task to the GLThread queue
                    Funcs.runMapCoreFunc(new Runnable() {
                        @Override
                        public void run() {
                            try {
                                // Perform calculations in GLThread
                                if (AMCTMapDevice.getInstance().getDevice() != null) {
                                    IMcMapDevice.Static.PerformPendingCalculations();
                                }
                            } catch (MapCoreException e) {
                                // Handle MapCoreException
                                AlertMessages.ShowMapCoreErrorMessage(context, e, "PerformPendingCalculations");
                            } catch (Exception e) {
                                // Handle any unexpected exception
                                e.printStackTrace();
                            }
                        }
                    });

                    // Schedule the next execution after 1 second
                    handler.postDelayed(this, 1000);

                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        };
    }

    public static void removeCallbacks(Handler handler)
    {
        // Stop the timer
        handler.removeCallbacksAndMessages(null);
    }

    public static MapFragment getMapFragment()
    {
        if (AMcGLSurfaceViewRenderer.getmGlSurfaceView() != null)
            return AMcGLSurfaceViewRenderer.getmGlSurfaceView().getMapFragment();
        return null;
    }

    /****
     * Method for Setting the Height of the ListView dynamically.
     * *** Hack to fix the issue of not showing all the items of the ListView
     * *** when placed inside a ScrollView
     ****/

    public static void setListViewHeightBasedOnChildren(ListView listView) {
        ListAdapter listAdapter = listView.getAdapter();
        if (listAdapter == null)
            return;

        int desiredWidth = View.MeasureSpec.makeMeasureSpec(listView.getWidth(), View.MeasureSpec.UNSPECIFIED);
        int totalHeight = 0;
        View view = null;
        for (int i = 0; i < listAdapter.getCount(); i++) {
            view = listAdapter.getView(i, view, listView);
            if (i == 0)
                view.setLayoutParams(new ViewGroup.LayoutParams(desiredWidth, ViewGroup.LayoutParams.WRAP_CONTENT));

            view.measure(desiredWidth, View.MeasureSpec.UNSPECIFIED);
            int measuredHeight = view.getMeasuredHeight();
            int height = view.getHeight();
            totalHeight += measuredHeight;
        }
        ViewGroup.LayoutParams params = listView.getLayoutParams();
        params.height = totalHeight + listAdapter.getCount() * 20 + (listView.getDividerHeight() * (listAdapter.getCount() - 1));
        listView.setLayoutParams(params);
    }

    public static void setExpandableListViewHeightBasedOnChildren(ExpandableListView listView) {
        ListAdapter listAdapter = listView.getAdapter();
        if (listAdapter == null)
            return;

        int desiredWidth = View.MeasureSpec.makeMeasureSpec(listView.getWidth(), View.MeasureSpec.AT_MOST);
        int totalHeight = 0;
        View view = null;
        for (int i = 0; i < listAdapter.getCount(); i++) {
            view = listAdapter.getView(i, null, listView);
            if (i == 0)
                view.setLayoutParams(new ViewGroup.LayoutParams(desiredWidth, ViewGroup.LayoutParams.WRAP_CONTENT));

           /*int widthMeasureSpec = View.MeasureSpec.makeMeasureSpec(ViewGroup.LayoutParams.MATCH_PARENT, View.MeasureSpec.EXACTLY);
            int heightMeasureSpec = View.MeasureSpec.makeMeasureSpec(ViewGroup.LayoutParams.WRAP_CONTENT, View.MeasureSpec.EXACTLY);*/

            view.measure(desiredWidth, View.MeasureSpec.AT_MOST);
            totalHeight += view.getMeasuredHeight();
        }
        ViewGroup.LayoutParams params = listView.getLayoutParams();
        params.height = totalHeight + (listView.getDividerHeight() * (listAdapter.getCount() - 1));
        listView.setLayoutParams(params);
    }

    public static String camelCasify(String in) {
        StringBuilder sb = new StringBuilder();
        boolean capitalizeNext = false;
        for (char c : in.toCharArray()) {
            if (c == '_') {
                capitalizeNext = true;
            } else {
                if (capitalizeNext) {
                    sb.append(Character.toUpperCase(c));
                    capitalizeNext = false;
                } else {
                    sb.append(c);
                }
            }
        }
        return sb.toString();
    }

    public static byte[] getStatesFromString(String txtObjectStates) {
        byte[] abyStates = null;
        if (txtObjectStates != null && !txtObjectStates.trim().isEmpty()) {
            String txRepObjectStates = txtObjectStates;
            while (((txRepObjectStates = txtObjectStates.replace("  ", " "))) != txtObjectStates) {
                txtObjectStates = txRepObjectStates;
            }
            String[] antxStates = txtObjectStates.split(" ");
            abyStates = new byte[antxStates.length];
            for (int i = 0; i < antxStates.length; i++) {
                abyStates[i] = (byte)Integer.parseInt(antxStates[i]);// Byte.parseByte(antxStates[i]);
            }
        }
        return abyStates;
    }

    public static String getStateFromByteArray(byte[] states) {
        String strState = "";
        if (states != null) {
            for (byte state : states)
                strState += (Byte.toString(state) + " ");
        }
        return strState.trim();
    }

    public static byte[] getByteArrFromFilePath(String textFileName) {
        File file = new File(textFileName);

        int size = (int) file.length();
        byte bytes[] = new byte[size];
        byte tmpBuff[] = new byte[size];
        FileInputStream fis = null;
        try {
            fis = new FileInputStream(file);
            ;
            int read = fis.read(bytes, 0, size);
            if (read < size) {
                int remain = size - read;
                while (remain > 0) {
                    read = fis.read(tmpBuff, 0, remain);
                    System.arraycopy(tmpBuff, 0, bytes, size - remain, read);
                    remain -= read;
                }
            }
        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            try {
                fis.close();
            } catch (IOException e) {
                e.printStackTrace();
            }
        }

        return bytes;

    }

    public static String[] splitToStringArr(String stringToSplit, String splitter) {
        String[] stringArr = stringToSplit.split(splitter);
        if (stringArr.length == 1 && stringArr[0].equals(""))
            stringArr = new String[0];
        return stringArr;
    }

    public static String ConvertIntArrToString(int[] values) {
        String Ret = "";

        if (values != null) {
            for (int val : values)
                Ret += val + " ";
        }

        // remove last comma
        if (Ret.length() > 0)
            Ret = Ret.substring(0, Ret.length() - 1);

        return Ret;
    }

    public static int[] ConvertStringToIntArr(String values) {

        String splitedValues[] = values.split(" ");
        int length = 0;
        if (!values.isEmpty())
            length = splitedValues.length;
        int[] results = new int[length];
        for (int i = 0; i < length; i++) {
            results[i] = Integer.parseInt(splitedValues[i]);
        }

        return results;
    }

    public static void addFragmentAndHidePrevious(FragmentManager fragmentManager, Fragment fragmentToAdd, Fragment fragmentToHide, int containerViewId, Object obj) {
        if (fragmentToAdd instanceof FragmentWithObject && obj != null)
            ((FragmentWithObject) fragmentToAdd).setObject(obj);
        FragmentTransaction transaction = fragmentManager.beginTransaction();
        transaction.hide(fragmentToHide);
        transaction.add(containerViewId, fragmentToAdd, fragmentToAdd.getClass().getSimpleName());
        transaction.addToBackStack(fragmentToAdd.getClass().getSimpleName());
        transaction.commit();
    }

    public static String getFilePathByProperties(String fileName) {
        String fullFilePath = Consts.SCHEMES_FOLDERS_MAIN;
        SparseBooleanArray itemSubTypeFlags = ObjectPropertiesBase.getItemSubTypeFlags();
        EMcPointCoordSystem locationCoordSys = ObjectPropertiesBase.mLocationCoordSys;

        boolean isSubTypeScreen = itemSubTypeFlags.valueAt(IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN.ordinal());
        boolean isSubTypeWorld = itemSubTypeFlags.valueAt(IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_WORLD.ordinal());
        boolean isSubTypeAttachToTerrain = itemSubTypeFlags.valueAt(IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_ATTACHED_TO_TERRAIN.ordinal());

        if (isSubTypeAttachToTerrain && isSubTypeScreen)
            fullFilePath = fullFilePath.concat(Consts.SCHEMES_FOLDERS_SCREEN_ATTACH_TO_WORLD);
        else if (isSubTypeAttachToTerrain && isSubTypeWorld)
            fullFilePath = fullFilePath.concat(Consts.SCHEMES_FOLDERS_WORLD_ATTACH_TO_WORLD);
        else if (isSubTypeScreen) {
            fullFilePath = fullFilePath.concat(Consts.SCHEMES_FOLDERS_SCREEN);
            if (locationCoordSys == EMcPointCoordSystem.EPCS_SCREEN)
                fullFilePath = fullFilePath.concat(Consts.SCHEMES_FOLDERS_SCREEN);
            else if (locationCoordSys == EMcPointCoordSystem.EPCS_WORLD)
                fullFilePath = fullFilePath.concat(Consts.SCHEMES_FOLDERS_WORLD);
        } else if (isSubTypeWorld)
            fullFilePath = fullFilePath.concat(Consts.SCHEMES_FOLDERS_WORLD_WORLD);

        fullFilePath = fullFilePath.concat(fileName);
        return fullFilePath;

    }

    public static void removeObject(final IMcObject objToRemove, final Activity activity) {
        try {
            IMcObjectScheme scheme = objToRemove.GetScheme();
            CMcEnumBitField bitField = new CMcEnumBitField(IMcObjectSchemeNode.ENodeKindFlags.ENKF_ANY_NODE);
            IMcObjectSchemeNode[] nodes = scheme.GetNodes(bitField);
            if (nodes != null)
                for (IMcObjectSchemeNode node : nodes)
                    node.Release();

            IMcOverlayManager OM = objToRemove.GetOverlayManager();
            IMcConditionalSelector[] selectorArr = OM.GetConditionalSelectors();
            if (selectorArr != null) {
                for (IMcConditionalSelector selector : selectorArr) {
                    if (OM.IsConditionalSelectorLocked(selector) == false)
                        selector.Release();
                    if (selector instanceof IMcLocationConditionalSelector)
                        Manager_MCConditionalSelectorObjects.getInstance().removeObjectFromHM((IMcLocationConditionalSelector) selector);
                }
            }
            scheme.Release();
            objToRemove.Release();
            objToRemove.Remove();
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(activity, e, "IMcViewportConditionalSelector.Static.Create/SetConditionalSelectorLock");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public static void SetObjectFromBundle(Bundle savedInstanceState, FragmentWithObject fragmentWithObject) {
        if(savedInstanceState != null)
        {
            AMCTSerializableObject mcObject = (AMCTSerializableObject)savedInstanceState.getSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT);
            if(mcObject != null)
                fragmentWithObject.setObject(mcObject.getMcObject());
        }
    }

    // must run on gl thread
    public static void MoveToMapCenter(SMcVector3D centerPoint, IMcMapViewport pViewport,  Context context) {
        MoveToCenter(centerPoint, pViewport, context, false);
    }

    // must run on gl thread
    public static void MoveToLayerCenter(SMcVector3D centerPoint, IMcMapViewport pViewport,  Context context) {
        MoveToCenter(centerPoint, pViewport, context, true);
    }

    private static void MoveToCenter(SMcVector3D centerPoint, IMcMapViewport pViewport,  Context context, boolean flagLayer)
    {
        try {

            if (pViewport.GetMapType() == IMcMapCamera.EMapType.EMT_2D)
                pViewport.SetCameraPosition(centerPoint, false);
            else {
                IMcSpatialQueries.SQueryParams queryParams = new IMcSpatialQueries.SQueryParams();
                queryParams.eTerrainPrecision = IMcSpatialQueries.EQueryPrecision.EQP_HIGHEST;
                ObjectRef<Boolean> pbHeightFound = new ObjectRef<>();
                ObjectRef<Double> pHeight = new ObjectRef<>();
                SMcVector3D normal = new SMcVector3D();

                pViewport.GetTerrainHeight(centerPoint, pbHeightFound, pHeight, normal, queryParams);
                if (pbHeightFound.getValue()) {

                    centerPoint.z = pHeight.getValue() + 20;
                    pViewport.SetCameraPosition(centerPoint, false);
                    pViewport.SetCameraClipDistances(1, 0, true);
                    pViewport.SetCameraOrientation(0, 0, 0, false);
                }
                else if(flagLayer)
                    AlertMessages.ShowMessage(context, "Move To Center" , "No height found, maybe missing dtm map layer?");

            }
        } catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(context, mcEx, "GetTerrainHeight");
            mcEx.printStackTrace();
        } catch (Exception mcEx) {
            mcEx.printStackTrace();
        }
    }

    public static String getFileExtension(String fileName) {
        String extension = "";

        int i = fileName.lastIndexOf('.');
        int p = Math.max(fileName.lastIndexOf('/'), fileName.lastIndexOf('\\'));

        if (i > p) {
            extension = fileName.substring(i + 1);
        }
        return extension;
    }

    public static IMcOverlayManager.EStorageFormat getEStorageFormatByFileExtension(String fileName) {
        final String fileExt = Funcs.getFileExtension(fileName);
        IMcOverlayManager.EStorageFormat storageFormat = IMcOverlayManager.EStorageFormat.ESF_MAPCORE_BINARY;
        if(fileExt.equalsIgnoreCase("json"))
            storageFormat = IMcOverlayManager.EStorageFormat.ESF_JSON;
        return  storageFormat;
    }
}
