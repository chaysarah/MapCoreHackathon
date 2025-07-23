package com.elbit.mapcore.mcandroidtester.managers.MapWorld;

import android.content.Context;

import com.elbit.mapcore.Interfaces.Map.IMcNative3DModelMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcNativeVector3DExtrusionMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcRaw3DModelMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcRawVector3DExtrusionMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcVectorMapLayer;
import com.elbit.mapcore.Structs.SMcBox;
import com.elbit.mapcore.mcandroidtester.model.AMCTMapLayerReadCallback;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.layers_tabs.DtmTabsFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.layers_tabs.RasterTabsFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.layers_tabs.RawRasterTabsFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.layers_tabs.StaticObjects3DModelTabsFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.layers_tabs.Vector3DExtrusionTabsFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.layers_tabs.VectorTabsFragment;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;

import java.util.ArrayList;
import java.util.Enumeration;
import java.util.HashMap;
import java.util.Hashtable;
import java.util.Map;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Calculations.GridCoordSystem.IMcGridCoordinateSystem;
import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcMapTerrain;
import com.elbit.mapcore.Interfaces.Map.IMcNativeDtmMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcNativeHeatMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcNativeMaterialMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcNativeRasterMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcNativeVectorMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcRawDtmMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcRawRasterMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcRawVectorMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcWebServiceDtmMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcWebServiceRasterMapLayer;

/**
 * Created by tc99382 on 02/08/2016.
 */
public class Manager_MCLayers {
    private static Hashtable<IMcMapLayer, Integer> dLayers;
    private static int currentLayerID = 0;
    private static IMcMapLayer NewLayer;
    private static Manager_MCLayers instance;

    private boolean mIsUsedCallback = false;

    private Manager_MCLayers() {
        dLayers = new Hashtable<IMcMapLayer, Integer>();

        InitLayersFragmentNames();
    }

    public void setIsUseCallback(boolean isUseCallback)
    {
        mIsUsedCallback = isUseCallback;
    }

    //get standalone layers==allparams
    public Map<Object, Integer> getAllStandaloneLayers() {
        Map<Object, Integer> ret = new HashMap<>();
        Enumeration<IMcMapLayer> enumKey = dLayers.keys();
        while (enumKey.hasMoreElements()) {
            IMcMapLayer keyLayer = enumKey.nextElement();
            //Add to the dictionary the standalone layers
            ret.put(keyLayer, dLayers.get(keyLayer));
        }
        return ret;
    }

    //get layers in terrain and standalone layers
    public ArrayList<IMcMapLayer> getAllLayers() {
        ArrayList<IMcMapLayer> lst = new ArrayList<>();

        for (Object terrainObj : Manager_MCTerrain.getInstance().getTerrains().keySet()) {
            IMcMapTerrain terrain = (IMcMapTerrain)terrainObj;
                try {
                    IMcMapLayer[] layersInTerr = terrain.GetLayers();
                    for (IMcMapLayer layer : layersInTerr) {
                        if (!lst.contains(layer))
                            lst.add(layer);
                    }
                } catch (MapCoreException mcEx) {
                    mcEx.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
        }
        for (Object layer : getAllStandaloneLayers().keySet()) {
            if (!lst.contains(layer))
                lst.add((IMcMapLayer) layer);
        }
        return lst;
    }

    public Map<Object, Integer> getChildren(Object parent) {
        Map<Object, Integer> Ret = new HashMap<>();

        return Ret;
    }

    public void AddLayer(IMcMapLayer layer) {
        dLayers.put(layer, currentLayerID++);
    }

    public void removeStandaloneLayer(IMcMapLayer layer) {
        if (dLayers.containsKey(layer)) {
            dLayers.remove(layer);
        }
    }

    public void removeAllStandaloneLayer() {
        dLayers.clear();
    }

    public IMcMapLayer CreateNativeDTMLayer(String LayerFileName,
                                            int NumLevelsToIgnore,
                                            IMcMapLayer.SLocalCacheLayerParams localCacheLayerParams,
                                            Context context) {
        try {
            NewLayer = null;
            IMcMapLayer.IReadCallback readCallback = null;
            if(mIsUsedCallback)
                readCallback = AMCTMapLayerReadCallback.getInstance(context);
            NewLayer = IMcNativeDtmMapLayer.Static.Create(LayerFileName, NumLevelsToIgnore, readCallback, localCacheLayerParams);
            AddLayer(NewLayer);
        } catch (MapCoreException McEx) {
            AlertMessages.ShowMapCoreErrorMessage(context, McEx, "NativeDTM Creation");
            McEx.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return NewLayer;
    }

    public IMcMapLayer CreateNativeRasterLayer(String LayerFileName,
                                               int FirstLowerQualityLevel,
                                               boolean ThereAreMissingFiles,
                                               int NumLevelsToIgnore,
                                               boolean bEnhanceBorderOverlap,
                                               IMcMapLayer.SLocalCacheLayerParams localCacheLayerParams,
                                               Context context)
    {
        try
        {
            NewLayer = null;
            IMcMapLayer.IReadCallback readCallback = null;
            if(mIsUsedCallback)
                readCallback = AMCTMapLayerReadCallback.getInstance(context);
            NewLayer = IMcNativeRasterMapLayer.Static.Create(
                    LayerFileName,
                    FirstLowerQualityLevel,
                    ThereAreMissingFiles,
                    NumLevelsToIgnore,
                    bEnhanceBorderOverlap,
                    readCallback,
                    localCacheLayerParams);
            AddLayer(NewLayer);
        }
        catch (MapCoreException McEx) {
            AlertMessages.ShowMapCoreErrorMessage(context, McEx, "NativeRaster Creation");
            McEx.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return NewLayer;
    }

    public IMcMapLayer CreateNativeVector3DExtrusionLayer(String LayerFileName, int NumLevelsToIgnore, float fExtrusionHeightMaxAddition, Context context)
    {
        try
        {
            NewLayer = null;
            IMcMapLayer.IReadCallback readCallback = null;
            if(mIsUsedCallback)
                readCallback = AMCTMapLayerReadCallback.getInstance(context);
            NewLayer = IMcNativeVector3DExtrusionMapLayer.Static.Create(LayerFileName,
                    NumLevelsToIgnore,
                    fExtrusionHeightMaxAddition,
                    readCallback);

            AddLayer(NewLayer);
         }
        catch (MapCoreException McEx) {
            AlertMessages.ShowMapCoreErrorMessage(context, McEx, "IMcNativeVector3DExtrusionMapLayer Creation");
            McEx.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return NewLayer;
    }

    public IMcMapLayer CreateNative3DModelLayer(String LayerFileName, int NumLevelsToIgnore, Context context)
    {
        try
        {
            NewLayer = null;
            IMcMapLayer.IReadCallback readCallback = null;
            if(mIsUsedCallback)
                readCallback = AMCTMapLayerReadCallback.getInstance(context);
            NewLayer = IMcNative3DModelMapLayer.Static.Create(LayerFileName,
                    NumLevelsToIgnore,
                    readCallback);

            AddLayer(NewLayer);
        }
        catch (MapCoreException McEx) {
            AlertMessages.ShowMapCoreErrorMessage(context, McEx, "IMcNative3DModelMapLayer Creation");
            McEx.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return NewLayer;
    }

    public IMcMapLayer CreateRawVector3DExtrusionLayer(IMcRawVector3DExtrusionMapLayer.SParams params, float fExtrusionHeightMaxAddition, Context context)
    {
        try
        {
            NewLayer = null;
            IMcMapLayer.IReadCallback readCallback = null;
            if(mIsUsedCallback)
                readCallback = AMCTMapLayerReadCallback.getInstance(context);

            NewLayer = IMcRawVector3DExtrusionMapLayer.Static.Create(params, fExtrusionHeightMaxAddition, readCallback);
            AddLayer(NewLayer);
        }
        catch (MapCoreException McEx) {
            AlertMessages.ShowMapCoreErrorMessage(context, McEx, "IMcRawVector3DExtrusionMapLayer Creation");
            McEx.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return NewLayer;
    }

    public IMcMapLayer CreateRawVector3DExtrusionLayer(String strDataSource, IMcRawVector3DExtrusionMapLayer.SGraphicalParams graphicalParams, float fExtrusionHeightMaxAddition, String strIndexindData, Context context)
    {

        try
        {
            NewLayer = null;
            IMcMapLayer.IReadCallback readCallback = null;
            if(mIsUsedCallback)
                readCallback = AMCTMapLayerReadCallback.getInstance(context);

            NewLayer = IMcRawVector3DExtrusionMapLayer.Static.Create(strDataSource, graphicalParams, fExtrusionHeightMaxAddition, readCallback, strIndexindData);
            AddLayer(NewLayer);
        }
        catch (MapCoreException McEx) {
            AlertMessages.ShowMapCoreErrorMessage(context, McEx, "IMcRawVector3DExtrusionMapLayer Creation");
            McEx.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return NewLayer;
    }

    public IMcMapLayer CreateRaw3DModelLayer(String LayerFileName, boolean bOrthometricHeights, int NumLevelsToIgnore, Context context, String strIndexingData)
    {
        try
        {
            NewLayer = null;
            IMcMapLayer.IReadCallback readCallback = null;
            if(mIsUsedCallback)
                readCallback = AMCTMapLayerReadCallback.getInstance(context);
            NewLayer = IMcRaw3DModelMapLayer.Static.Create(LayerFileName,
                    bOrthometricHeights,
                    NumLevelsToIgnore,
                    readCallback,
                    strIndexingData);

            AddLayer(NewLayer);
        }
        catch (MapCoreException McEx) {
            AlertMessages.ShowMapCoreErrorMessage(context, McEx, "IMcRaw3DModelMapLayer Creation");
            McEx.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return NewLayer;
    }

    public IMcMapLayer CreateRaw3DModelLayer(String LayerFileName, IMcGridCoordinateSystem gridCoordinateSystem, boolean bOrthometricHeights, SMcBox clipRect, IMcMapLayer.STilingScheme tilingScheme, float fTargetHighestResolution, Context context)
    {
        try
        {
            NewLayer = null;
            IMcMapLayer.IReadCallback readCallback = mIsUsedCallback ? AMCTMapLayerReadCallback.getInstance(context) : null;

            NewLayer = IMcRaw3DModelMapLayer.Static.Create(LayerFileName,
                    gridCoordinateSystem,
                    bOrthometricHeights,
                    clipRect,
                    fTargetHighestResolution,
                    readCallback) ;

            AddLayer(NewLayer);
        }
        catch (MapCoreException McEx) {
            AlertMessages.ShowMapCoreErrorMessage(context, McEx, "IMcRaw3DModelMapLayer Creation");
            McEx.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return NewLayer;
    }

    public IMcMapLayer CreateRaw3DModelLayer(String LayerFileName, boolean bOrthometricHeights, int NumLevelsToIgnore, Context context)
    {
        return CreateRaw3DModelLayer(LayerFileName, bOrthometricHeights, NumLevelsToIgnore, context, "");
    }

    public IMcMapLayer CreateNativeVectorLayer(String LayerFileName,
                                               IMcMapLayer.SLocalCacheLayerParams localCacheLayerParams,
                                               Context context) {
        NewLayer = null;
        try {
            IMcMapLayer.IReadCallback readCallback = null;
            if(mIsUsedCallback)
                readCallback = AMCTMapLayerReadCallback.getInstance(context);
            NewLayer = IMcNativeVectorMapLayer.Static.Create(
                    LayerFileName,
                    readCallback,
                    localCacheLayerParams);
            AddLayer(NewLayer);

        } catch (MapCoreException McEx) {
            AlertMessages.ShowMapCoreErrorMessage(context, McEx, "Native Vector Creation");
            McEx.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return NewLayer;
    }

    public IMcMapLayer CreateRawDTMLayer(
            IMcMapLayer.SRawParams dnParams,
            IMcMapLayer.SLocalCacheLayerParams localCacheLayerParams,
            Context context)
    {
        try {
            NewLayer = null;
            if(mIsUsedCallback)
                dnParams.pReadCallback = AMCTMapLayerReadCallback.getInstance(context);
            NewLayer = IMcRawDtmMapLayer.Static.Create(dnParams, localCacheLayerParams);
            AddLayer(NewLayer);
        }
        catch (MapCoreException McEx) {
            AlertMessages.ShowMapCoreErrorMessage(context, McEx, "Raw DTM Creation");
            McEx.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return NewLayer;
    }

    public IMcMapLayer CreateRawRasterLayer(
            IMcMapLayer.SRawParams dnParams,
            boolean imageCoordSys,
            IMcMapLayer.SLocalCacheLayerParams localCacheLayerParams,
            Context context)
    {
        try
        {
            NewLayer = null;
            if(mIsUsedCallback)
                dnParams.pReadCallback = AMCTMapLayerReadCallback.getInstance(context);
            NewLayer = IMcRawRasterMapLayer.Static.Create(dnParams, imageCoordSys, localCacheLayerParams);
            AddLayer(NewLayer);
        }
        catch (MapCoreException McEx) {
            AlertMessages.ShowMapCoreErrorMessage(context, McEx, "IMcRawRasterMapLayer Creation");
            McEx.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return NewLayer;
    }

   /* public IMcMapLayer CreateRawVector3DExtrusionMapLayer(
            IMcRawVector3DExtrusionMapLayer.SParams params,
            float fExtrusionHeightMaxAddition,
            Context context)
    {
        try
        {
            NewLayer = null;
            NewLayer = IMcRawVector3DExtrusionMapLayer.Static.Create(params,fExtrusionHeightMaxAddition, AMCTMapLayerReadCallback.getInstance(context));
            AddLayer(NewLayer);
        }
        catch (MapCoreException McEx) {
            AlertMessages.ShowMapCoreErrorMessage(context, McEx, "IMcRawVector3DExtrusionMapLayer Creation");
            McEx.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return NewLayer;
    }*/


    public IMcMapLayer CreateRawVectorLayer(
            IMcRawVectorMapLayer.SParams params,
            IMcGridCoordinateSystem gridCoordSys,
            IMcMapLayer.STilingScheme tilingScheme,
            IMcMapLayer.SLocalCacheLayerParams localCacheLayerParams,
            Context context)
    {
        try {
            NewLayer = null;
            IMcMapLayer.IReadCallback readCallback = null;
            if(mIsUsedCallback)
                readCallback = AMCTMapLayerReadCallback.getInstance(context);
            NewLayer = IMcRawVectorMapLayer.Static.Create(params,
                    gridCoordSys,
                    tilingScheme,
                    readCallback,
                    localCacheLayerParams);
            AddLayer(NewLayer);

            // needed for check vectory functions    mcVectorMapLayer = (IMcVectorMapLayer)NewLayer;
        }
        catch (MapCoreException McEx) {
            AlertMessages.ShowMapCoreErrorMessage(context, McEx, "IMcRawVectorMapLayer Creation");
            McEx.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }

        return NewLayer;
    }

    public static  IMcVectorMapLayer mcVectorMapLayer ;


    public IMcMapLayer CreateNativeHeatMapLayer(String LayerFileName,
                                                int FirstLowerQualityLevel,
                                                boolean ThereAreMissingFiles,
                                                int NumLevelsToIgnore,
                                                boolean bEnhanceBorderOverlap,
                                                IMcMapLayer.SLocalCacheLayerParams localCacheLayerParams,
                                                Context context)
    {
        NewLayer = null;
        try
        {
            IMcMapLayer.IReadCallback readCallback = null;
            if(mIsUsedCallback)
                readCallback = AMCTMapLayerReadCallback.getInstance(context);
            NewLayer = IMcNativeHeatMapLayer.Static.Create(
                    LayerFileName,
                    FirstLowerQualityLevel,
                    ThereAreMissingFiles,
                    NumLevelsToIgnore,
                    bEnhanceBorderOverlap,
                    readCallback,
                    localCacheLayerParams);
            AddLayer(NewLayer);
        }
        catch (MapCoreException McEx) {
            AlertMessages.ShowMapCoreErrorMessage(context, McEx, "NativeHeat Creation");
            McEx.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return NewLayer;
    }

    public IMcMapLayer CreateNativeMaterialLayer(String LayerFileName,
                                                 int FirstLowerQualityLevel,
                                                 boolean ThereAreMissingFiles,
                                                 int NumLevelsToIgnore,
                                                 IMcMapLayer.SLocalCacheLayerParams localCacheLayerParams,
                                                 Context context)
    {
        NewLayer = null;
        try
        {
            IMcMapLayer.IReadCallback readCallback = null;
            if(mIsUsedCallback)
                readCallback = AMCTMapLayerReadCallback.getInstance(context);
            NewLayer = IMcNativeMaterialMapLayer.Static.Create(
                    LayerFileName,
                    ThereAreMissingFiles,
                    readCallback,
                    localCacheLayerParams);
            AddLayer(NewLayer);
        }
        catch (MapCoreException McEx) {
            AlertMessages.ShowMapCoreErrorMessage(context, McEx, "NativeMaterial Creation");
            McEx.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return NewLayer;
    }

    IMcMapLayer CreateWMSRasterLayer(IMcWebServiceRasterMapLayer.SWMSParams layerParams,
                                     IMcMapLayer.SLocalCacheLayerParams localCacheLayerParams,
                                     Context context) {
        NewLayer = null;
        try {
           if(mIsUsedCallback)
                layerParams.pReadCallback = AMCTMapLayerReadCallback.getInstance(context);;
            NewLayer = IMcWebServiceRasterMapLayer.Static.Create(layerParams, localCacheLayerParams);
            AddLayer(NewLayer);
        } catch (MapCoreException McEx) {
            AlertMessages.ShowMapCoreErrorMessage(context, McEx, "WMSRaster Creation");
            McEx.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return NewLayer;
    }

    public static Manager_MCLayers getInstance() {
        if (instance == null) {
            instance = new Manager_MCLayers();
        }
        return instance;
    }

    private Hashtable<IMcMapLayer.ELayerType,String> layersFragmentNames = new Hashtable<>();
    private void InitLayersFragmentNames()
    {
        layersFragmentNames.clear();
        layersFragmentNames.put(IMcMapLayer.ELayerType.ELT_NATIVE_RASTER, RasterTabsFragment.class.getName());
        layersFragmentNames.put(IMcMapLayer.ELayerType.ELT_RAW_RASTER, RawRasterTabsFragment.class.getName());
        layersFragmentNames.put(IMcMapLayer.ELayerType.ELT_NATIVE_DTM, DtmTabsFragment.class.getName());
        layersFragmentNames.put(IMcMapLayer.ELayerType.ELT_RAW_DTM, DtmTabsFragment.class.getName());
        layersFragmentNames.put(IMcMapLayer.ELayerType.ELT_NATIVE_VECTOR_3D_EXTRUSION, Vector3DExtrusionTabsFragment.class.getName());
        layersFragmentNames.put(IMcMapLayer.ELayerType.ELT_RAW_VECTOR_3D_EXTRUSION, Vector3DExtrusionTabsFragment.class.getName());
        layersFragmentNames.put(IMcMapLayer.ELayerType.ELT_NATIVE_3D_MODEL, StaticObjects3DModelTabsFragment.class.getName());
        layersFragmentNames.put(IMcMapLayer.ELayerType.ELT_RAW_3D_MODEL, StaticObjects3DModelTabsFragment.class.getName());
        layersFragmentNames.put(IMcMapLayer.ELayerType.ELT_NATIVE_VECTOR, VectorTabsFragment.class.getName());
        layersFragmentNames.put(IMcMapLayer.ELayerType.ELT_RAW_VECTOR, VectorTabsFragment.class.getName());
    }

    public String GetLayerFragmentByType(IMcMapLayer layer, Context context)
    {
        try {
            return layersFragmentNames.get(layer.GetLayerType());
        }catch (MapCoreException McEx) {
            AlertMessages.ShowMapCoreErrorMessage(context, McEx, "GetLayerType");
            McEx.printStackTrace();
        }  catch (Exception e) {
            e.printStackTrace();
        }
        return "";
    }
}



