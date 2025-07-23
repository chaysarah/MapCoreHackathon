package com.elbit.mapcore.mcandroidtester.managers.ObjectWorld;

import java.util.HashMap;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;

/**
 * Created by tc97803 on 26/12/2017.
 */

public class Manager_MCObjectSchemeItem {
    private HashMap<IMcObjectSchemeItem, Integer> dObjectSchemeItem = new HashMap<>();
    private static int currentLayerID = 0;
    private static Manager_MCObjectSchemeItem instance;

    public static Manager_MCObjectSchemeItem getInstance() {
        if (instance == null) {
            instance = new Manager_MCObjectSchemeItem();
        }
        return instance;
    }

    public void AddNewItem(IMcObjectSchemeItem item)
    {
        if (item != null)
        {
            if (!dObjectSchemeItem.containsKey(item))
            {
                try {
                    dObjectSchemeItem.put(item, item.GetID());
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        }
    }

    public void RemoveItem(IMcObjectSchemeItem item)
    {
        if (item != null)
            dObjectSchemeItem.remove(item);
    }

    public void SetStandaloneItemID(IMcObjectSchemeItem item, int newID)
    {
        dObjectSchemeItem.put(item, newID);
    }

    public HashMap<IMcObjectSchemeItem, Integer> getAllParams()
    {
        return dObjectSchemeItem;
    }

    public HashMap<Object, Integer> getAllObjectParams()
    {
        HashMap<Object, Integer> Ret = new HashMap<>();
        int i = 0;
        for(IMcObjectSchemeItem item : dObjectSchemeItem.keySet())
        {
            Ret.put(item, i);
            i++;
        }
        return Ret;
    }
}
