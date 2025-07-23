package com.elbit.mapcore.mcandroidtester.managers.MapWorld;

import android.content.Context;

import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.BaseApplication;

import java.util.HashMap;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Calculations.GridCoordSystem.IMcGridCoordinateSystem;
import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcMapTerrain;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

/**
 * Created by tc99382 on 02/08/2016.
 */
public class Manager_MCTerrain {

    static int currentTerrainID = 0;
    private static Manager_MCTerrain instance;

    private HashMap<Object, Integer> mdTerrain;

    public HashMap<Object, Integer> getTerrains() {
        return mdTerrain;
    }

    private Manager_MCTerrain()
    {
        mdTerrain = new HashMap<>();
    }

    public IMcMapTerrain CreateTerrain(IMcGridCoordinateSystem gridCoordSys, IMcMapLayer[] layerList, Context context)
    {
        IMcMapTerrain ret = null;
        try
        {
            ret = IMcMapTerrain.Static.Create(gridCoordSys, layerList);
            for (IMcMapLayer layer: layerList)
                Manager_MCLayers.getInstance().removeStandaloneLayer(layer);
            if (ret != null) {
                AddTerrain(ret);
            }
            else {
                AlertMessages.ShowErrorMessage(context, "Terrain creation", "Terrain creation failed!!!");
            }
        }
        catch (MapCoreException McEx)
        {
            AlertMessages.ShowMapCoreErrorMessage(context, McEx, "McMapTerrain.Create");
            McEx.printStackTrace();
        }
       catch (Exception e) {
            e.printStackTrace();
        }
        return ret;
    }

    public void AddTerrain(IMcMapTerrain terrain)
    {
        mdTerrain.put(terrain,currentTerrainID++);
    }

    public void RemoveTerrain(final IMcMapTerrain terrain) {
        mdTerrain.remove(terrain);
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                terrain.Release();
            }
        });
    }

    public HashMap<Object, Integer> getAllParams() {
       return mdTerrain;
    }

    public HashMap<Object, Integer> GetChildren(Object Parent)
    {
        HashMap<Object, Integer> Ret = new HashMap<>();
        if (Parent instanceof IMcMapTerrain)
        {
            IMcMapTerrain Terrain = (IMcMapTerrain)Parent;
            IMcMapLayer[] LayersInTerrain;
            try {
                LayersInTerrain = Terrain.GetLayers();
                for (int i=0;i<=LayersInTerrain.length;i++)
                {
                    Ret.put(LayersInTerrain[i], i++);
                }
            } catch (MapCoreException McEx) {
                McEx.printStackTrace();
                AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(),McEx,"IMcMapLayer[] GetLayers()");
            }
            catch (Exception e) {
                e.printStackTrace();
            }
        }
        return Ret;
    }

    public static Manager_MCTerrain getInstance() {
        if (instance == null) {
            instance = new Manager_MCTerrain();
        }
        return instance;
    }
}
