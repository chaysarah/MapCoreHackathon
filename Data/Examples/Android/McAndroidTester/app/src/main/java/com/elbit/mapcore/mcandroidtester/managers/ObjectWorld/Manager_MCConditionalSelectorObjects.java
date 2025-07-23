package com.elbit.mapcore.mcandroidtester.managers.ObjectWorld;

import android.app.Activity;

import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.BaseApplication;

import java.util.HashMap;

import com.elbit.mapcore.General.CMcEnumBitField;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcConditionalSelector;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcLocationConditionalSelector;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectScheme;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeNode;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

/**
 * Created by tc99382 on 05/07/2017.
 */
public class Manager_MCConditionalSelectorObjects {
    private static Manager_MCConditionalSelectorObjects instance;
    private HashMap<Integer, IMcObject> mLocationCondSelectorObjects;

    private Manager_MCConditionalSelectorObjects() {
        mLocationCondSelectorObjects = new HashMap<>();
    }

    public void addItem(IMcLocationConditionalSelector locationCondSelector, IMcObject mcObject) {
        int id = locationCondSelector.hashCode();
        if (checkIsItemExist(id))
            removeObject(id);
            mLocationCondSelectorObjects.put(id,mcObject);
    }

    public void addNewItem(final IMcLocationConditionalSelector locationCondSelector, final IMcObject mcObject) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                if (locationCondSelector != null && mcObject != null) {
                    int id = locationCondSelector.hashCode();

                    try {
                        locationCondSelector.GetOverlayManager().SetConditionalSelectorLock(locationCondSelector, true);
                    } catch (MapCoreException e) {
                        AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "SetConditionalSelectorLock");
                        e.printStackTrace();
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                    if (!checkIsItemExist(id))
                        mLocationCondSelectorObjects.put(id, mcObject);
                }
            }
        });
    }

    public IMcObject getObjectOfSelector(IMcLocationConditionalSelector locationCondSelector)
    {
        int id = locationCondSelector.hashCode();
        if (checkIsItemExist(id))
            return mLocationCondSelectorObjects.get(id);
        return null;
    }

    private void removeItem(final IMcLocationConditionalSelector locationCondSelector) {
        int id = locationCondSelector.hashCode();
        if (checkIsItemExist(id)) {
            removeObject(id);
            mLocationCondSelectorObjects.remove(id);
        }
    }

    private void removeObjectFromDic(IMcLocationConditionalSelector locationCondSelector)
    {
        int id = locationCondSelector.hashCode();
        if (checkIsItemExist(id))
            mLocationCondSelectorObjects.remove(id);
    }

    private void removeObject(IMcLocationConditionalSelector locationCondSelector)
    {
        int id = locationCondSelector.hashCode();
        if (checkIsItemExist(id))
        {
            removeObject(id);
        }
    }

    private void removeObject(final int id) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                IMcObject mcObject = mLocationCondSelectorObjects.get(id);
                if (mcObject != null) {
                    try {
                        IMcObjectScheme scheme = mcObject.GetScheme();

                        try {
                            for (IMcObjectSchemeNode node : scheme.GetNodes(new CMcEnumBitField<>(IMcObjectSchemeNode.ENodeKindFlags.ENKF_ANY_NODE))) {
                                node = null;
                            }
                            IMcOverlayManager overlayManager = mcObject.GetOverlayManager();
                            IMcConditionalSelector[] selectorArr = overlayManager.GetConditionalSelectors();
                            for (IMcConditionalSelector selector : selectorArr) {
                                if (overlayManager.IsConditionalSelectorLocked(selector) == false)
                                    selector = null;
                                if (selector instanceof IMcLocationConditionalSelector)
                                    removeObjectFromHM((IMcLocationConditionalSelector) selector);
                            }
                            scheme = null;
                            mcObject = null;
                            mcObject.Remove();
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "RemoveObject");
                            e.printStackTrace();
                        }
                    } catch (MapCoreException e) {
                        AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "IMcObject.GetScheme");
                        e.printStackTrace();
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            }
        });
    }

    public void removeObjectFromHM(final IMcLocationConditionalSelector selector) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                int id = selector.hashCode();
                if (checkIsItemExist(id))
                    mLocationCondSelectorObjects.get(id).Release();
            }
        });
    }

    private boolean checkIsItemExist(int id) {
        return mLocationCondSelectorObjects.containsKey(id);
    }

    public static Manager_MCConditionalSelectorObjects getInstance() {
        if (instance == null) {
            instance = new Manager_MCConditionalSelectorObjects();
        }
        return instance;
    }
}
