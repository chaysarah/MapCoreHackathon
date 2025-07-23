package com.elbit.mapcore.mcandroidtester.managers.ObjectWorld;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcFont;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcMesh;

import java.util.HashMap;
import java.util.Set;

public class Manager_MCMeshes {
    public HashMap<Object, Integer> getMeshes() {
        return mMeshes;
    }

    static HashMap<Object, Integer> mMeshes;
    private static Manager_MCMeshes instance;

    private Manager_MCMeshes() {
        mMeshes = new HashMap<>();
    }
    public void addToDictionary(IMcMesh Mesh)
    {
        //Add Mesh to dictionary in case it doesn't exist already.
        if(!mMeshes.containsValue(Mesh))
        {
            try {
                mMeshes.put(Mesh,Mesh.GetMeshType().getValue());
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }
    public void removeFromDictionary(IMcMesh Mesh)
    {
        //Remove Mesh from dictionary
        mMeshes.remove(Mesh);
    }

    public int getItemIndex(IMcMesh Mesh)
    {
        int index = 0;
        Set<Object> MesheSet = mMeshes.keySet();
        for (Object f: MesheSet) {
            if (f == Mesh)
                return index;
            index++;
        }
        return -1;
    }

    public static Manager_MCMeshes getInstance() {
        if (instance == null) {
            instance = new Manager_MCMeshes();
        }
        return instance;
    }
}
