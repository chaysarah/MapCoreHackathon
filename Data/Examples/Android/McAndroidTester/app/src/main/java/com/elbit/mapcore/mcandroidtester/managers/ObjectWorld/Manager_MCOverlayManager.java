package com.elbit.mapcore.mcandroidtester.managers.ObjectWorld;

import android.util.Log;

import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_AMCTMapForm;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.BaseApplication;

import java.util.HashMap;
import java.util.Hashtable;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Calculations.GridCoordSystem.IMcGridCoordinateSystem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcCollection;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcConditionalSelector;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectScheme;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlay;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;

/**
 * Created by tc99382 on 04/08/2016.
 */
public class Manager_MCOverlayManager {

    private HashMap<IMcOverlayManager, IMcOverlay> mOM_Overlay = new HashMap<>();

    private static Manager_MCOverlayManager instance;

    public IMcOverlayManager getActiveOverlayManager() {
        if(Manager_AMCTMapForm.getInstance().getCurMapForm()!=null&&Manager_AMCTMapForm.getInstance().getCurViewport()!=null)
            try {
                return Manager_AMCTMapForm.getInstance().getCurViewport().GetOverlayManager();
            }  catch (MapCoreException McEx) {
                AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(),McEx,"GetOverlayManager");
                McEx.printStackTrace();
            }catch (Exception e) {
                e.printStackTrace();
            }
        return null;
    }

    public IMcOverlayManager CreateOverlayManager(IMcGridCoordinateSystem gridCoordSysDef, boolean isCreateOverlay) {
        IMcOverlayManager activeOverlayManager = null;
        try {
            activeOverlayManager = IMcOverlayManager.Static.Create(gridCoordSysDef);
            IMcOverlay newOverlay = null;
            if (isCreateOverlay)
                newOverlay = IMcOverlay.Static.Create(activeOverlayManager);
            mOM_Overlay.put(activeOverlayManager, newOverlay);

        } catch (MapCoreException McEx) {
            AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), McEx, "McOverlayManager.Create");
            McEx.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return activeOverlayManager;
    }

    public void updateOverlayManager(IMcOverlayManager overlayManager, IMcOverlay overlay) {
        if(overlayManager!=null)
        {
            mOM_Overlay.put(overlayManager,overlay);
        }
    }

    public IMcOverlay getActiveOverlay() {
        IMcOverlayManager overlayManager = getActiveOverlayManager();
        if (overlayManager != null) {
            return mOM_Overlay.get(overlayManager);
        } else
            return null;
    }



    public HashMap<Object, Integer> getAllParams()
    {
        HashMap<Object, Integer> Ret = new HashMap<>();
            int i = 0;
            for(IMcOverlayManager keyOM : mOM_Overlay.keySet())
            {
                Ret.put(keyOM, i);
                i++;
            }
            return Ret;
    }

    public Hashtable<Object, Integer> GetChildren(Object Parent) {
        IMcOverlayManager Om = (IMcOverlayManager) Parent;
        Hashtable<Object, Integer> Ret = new Hashtable<Object, Integer>();

        if (Om == null)
            return Ret;

        IMcOverlay[] Overlays = new IMcOverlay[0];
        try {
            Overlays = Om.GetOverlays();
        }catch (MapCoreException McEx) {
            McEx.printStackTrace();
        }
        catch (Exception e) {
            e.printStackTrace();
        }

        int i = 0;
        for(IMcOverlay overlay : Overlays)
        {
            Ret.put(overlay, i++);
        }

        IMcCollection[] collections = new IMcCollection[0];
        try {
            collections = Om.GetCollections();
        }
    catch (MapCoreException McEx) {
        McEx.printStackTrace();
    }catch (Exception e) {
            e.printStackTrace();
        }
        for(IMcCollection col : collections)
        {
            Ret.put(col, i++);
        }

        IMcObjectScheme[] schemes = new IMcObjectScheme[0];
        try {
            schemes = Om.GetObjectSchemes();
        }catch (MapCoreException McEx) {
            McEx.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
        for(IMcObjectScheme scheme : schemes)
        {
            Ret.put(scheme, i++);
        }

        IMcConditionalSelector[] selectors = new IMcConditionalSelector[0];
        try {
            selectors = Om.GetConditionalSelectors();
        } catch (MapCoreException McEx) {
        McEx.printStackTrace();
    }catch (Exception e) {
            e.printStackTrace();
        }
        for(IMcConditionalSelector selector : selectors)
        {
            Ret.put(selector, i++);
        }
//TODO uncomment this
     /*   if (Manager_MCVectorial.lMapProductionParams.Count > 0) {
            IDNMcObject[] vectorObjects;
            for (int idx = 0; idx < Manager_MCVectorial.lMapProductionParams.Count; idx++) {
                vectorObjects = Manager_MCVectorial.lMapProductionParams[idx].lObjectsToUpdate.ToArray();
                foreach(IDNMcObject vectorObj in vectorObjects)
                {
                    if (!Ret.ContainsKey(vectorObj))
                        Ret.Add(vectorObj, i++);
                }

            }
        }*/

        return Ret;
    }

    public HashMap<IMcOverlayManager, IMcOverlay> getHMOverlayManagerOverlay()
    {
        return mOM_Overlay;
    }

    public void RemoveOverlayManager(IMcOverlayManager overlayManager)
    {
        mOM_Overlay.remove(overlayManager);
    }

    public static Manager_MCOverlayManager getInstance() {
        if (instance == null) {
            instance = new Manager_MCOverlayManager();
        }
        return instance;
    }
}

