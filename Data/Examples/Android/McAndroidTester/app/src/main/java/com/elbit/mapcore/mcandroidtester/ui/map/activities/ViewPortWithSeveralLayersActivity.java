package com.elbit.mapcore.mcandroidtester.ui.map.activities;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.net.Uri;
import android.os.Bundle;
import android.os.Handler;
import android.preference.PreferenceManager;

import com.elbit.mapcore.mcandroidtester.model.AMCTMapLayerReadCallback;
import com.google.android.material.tabs.TabLayout;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentPagerAdapter;
import androidx.viewpager.widget.ViewPager;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.elbit.mapcore.Interfaces.Map.IMcNative3DModelMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcNativeVector3DExtrusionMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcNativeVectorMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcRaw3DModelMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcRawVector3DExtrusionMapLayer;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_MCGridCoordinateSystem;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_MCLayers;
import com.elbit.mapcore.mcandroidtester.managers.ObjectWorld.Manager_MCOverlayManager;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_MCTerrain;
import com.elbit.mapcore.mcandroidtester.model.AMCTMapDevice;
import com.elbit.mapcore.mcandroidtester.model.AMCTViewPort;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.GridCoordinateSysFragment;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.SeveralLayersFragment;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.SeveralLayersSetingsFragment;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.BaseApplication;

import java.util.ArrayList;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Calculations.GridCoordSystem.IMcGridCoordinateSystem;
import com.elbit.mapcore.Interfaces.Map.IMcMapCamera;
import com.elbit.mapcore.Interfaces.Map.IMcMapDevice;
import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcMapTerrain;
import com.elbit.mapcore.Interfaces.Map.IMcNativeDtmMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcNativeRasterMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcRawDtmMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcRawRasterMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcRawVectorMapLayer;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;
import com.elbit.mapcore.Structs.SMcBox;
import com.elbit.mapcore.Structs.SMcVector3D;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

public class ViewPortWithSeveralLayersActivity extends AppCompatActivity implements SeveralLayersFragment.OnFragmentInteractionListener, SeveralLayersSetingsFragment.OnFragmentInteractionListener ,GridCoordinateSysFragment.OnFragmentInteractionListener{

    private static final String NativeRaster1Str ="native_raster1";
    private static final String NativeRaster2Str ="native_raster2";
    private static final String NativeRaster3Str ="native_raster3";
    private static final String NativeDTM1Str ="native_dtm";
    private static final String NativeVector1Str ="native_vector1";
    private static final String NativeVector2Str ="native_vector2";
    private static final String NativeVector3Str ="native_vector3";
    private static final String NativeVector3DExtrusionStr ="native_vector_3d_extrusion";
    private static final String Native3DModelStr ="native_3d_model";
    private static final String RawVector3DExtrusionStr ="raw_vector_3d_extrusion";
    private static final String Raw3DModelStr ="raw_3d_model";
    private static final String RawRasterStr ="raw_raster";
    private static final String RawDTMStr ="raw_dtm";
    private static final String RawVectorStr ="raw_vector";

    ///////////////////////////////layers members////////////////////////////
    private String mRaster1Path = "";
    private String mRaster2Path = "";
    private String mRaster3Path = "";
    private String mDtmPath = "";
    private String mRawRasterPath = "";
    private String mNativeVector3DExtrusionPath = "";
    private String mNative3DModelPath = "";
    private String mRawVector3DExtrusionPath = "";
    private String mRaw3DModelPath = "";
    private String mRawVectorPath = "";
    private String mNativeVector1Path = "";
    private String mNativeVector2Path = "";
    private String mNativeVector3Path = "";
    private String mRawDTMPath = "";

    public SeveralLayersSetingsFragment mSettingsFragment;
    public SeveralLayersFragment mSeveralLayersFragment;

    private IMcMapCamera.EMapType mapType = IMcMapCamera.EMapType.EMT_2D;
    private SectionsPagerAdapter mSectionsPagerAdapter;
    private ViewPager mViewPager;
    private ArrayList<IMcMapLayer> mLayersList;
    private int mDefaultFirstLowerQuality = Integer.MAX_VALUE;
    private boolean mDefaultThereAreMissingFiles = false;
    private int mDefaultNumLevelsToIgnore = 0;
    private float mFirstPyramidResolution = 0;
    private ArrayList<Integer> mlPyramidResolutions;
    private boolean isMcExThrown = false;
    private boolean isOneLayerAtLeast = false;
    private IMcGridCoordinateSystem mCoordSysForRawLayers;
    private IMcRawVector3DExtrusionMapLayer.SParams mRawVector3DExtrusionMapLayerParams;
    private boolean mIsUseCallback = false;
    private IMcMapCamera.EMapType mMapType;

    Runnable PerformPendingCalculationsRunnable = null;
    Handler timerHandler = new Handler();


    public void setmIsImageCoordSysForRawLayers(boolean mIsImageCoordSysForRawLayers) {
        this.mIsImageCoordSysForRawLayers = mIsImageCoordSysForRawLayers;
    }

    public void setmIsUseCallback(boolean isUseCallback) {
        this.mIsUseCallback = isUseCallback;
    }

    private boolean mIsImageCoordSysForRawLayers=false;
    private ArrayList<IMcMapLayer.SComponentParams> lComponents;

    public IMcGridCoordinateSystem getmCoordSysForRawLayers() {
        return mCoordSysForRawLayers;
    }

    public void setmCoordSysForRawLayers(IMcGridCoordinateSystem mCoordSysForRawLayers) {
        this.mCoordSysForRawLayers = mCoordSysForRawLayers;
    }

    public void setParamsForRawVectorExtrusionLayers(IMcRawVector3DExtrusionMapLayer.SParams params) {
        this.mRawVector3DExtrusionMapLayerParams = params;
    }

    public String getmRaster1Path() {
        return mRaster1Path;
    }

    public void setmRaster1Path(String mRaster1Path) {
        this.mRaster1Path = mRaster1Path;
    }

    public String getmRaster2Path() {
        return mRaster2Path;
    }

    public void setmRaster2Path(String mRaster2Path) {
        this.mRaster2Path = mRaster2Path;
    }

    public String getmRaster3Path() {
        return mRaster3Path;
    }

    public void setmRaster3Path(String mRaster3Path) {
        this.mRaster3Path = mRaster3Path;
    }

    public String getmDtmPath() {
        return mDtmPath;
    }

    public void setmDtmPath(String mDtmPath) {
        this.mDtmPath = mDtmPath;
    }
    public void setmRawDtmPath(String mRawDtmPath) {
        this.mRawDTMPath = mRawDtmPath;
    }
    public void setmRawRasterPath(String mRawRasterPath) {
        this.mRawRasterPath = mRawRasterPath;
    }
    public void setmNativeVector1Path(String mNativeVector1Path) {
        this.mNativeVector1Path = mNativeVector1Path;
    }
    public void setmNativeVector2Path(String mNativeVector2Path) {
        this.mNativeVector2Path = mNativeVector2Path;
    }
    public void setmNativeVector3Path(String mNativeVector3Path) {
        this.mNativeVector3Path = mNativeVector3Path;
    }
    public void setmNativeVector3DExtrusionPath(String mNativeVector3DExtrusionPath) { this.mNativeVector3DExtrusionPath = mNativeVector3DExtrusionPath; }
    public void setmRawVector3DExtrusionPath(String mRawVector3DExtrusionPath) { this.mRawVector3DExtrusionPath = mRawVector3DExtrusionPath; }
    public void setmNative3DModelPath(String mNative3DModelPath) { this.mNative3DModelPath = mNative3DModelPath; }
    public void setmRaw3DModelPath(String mRaw3DModelPath) { this.mRaw3DModelPath = mRaw3DModelPath; }
    public void setmRawVectorPath(String mRawVectorPath) { this.mRawVectorPath = mRawVectorPath; }

    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_view_port_with_several_layers);

        Toolbar toolbar = (Toolbar) findViewById(R.id.activity_several_layers_toolbar);
        setSupportActionBar(toolbar);
        // Create the adapter that will return a fragment for each of the three
        // primary sections of the activity.
        mSectionsPagerAdapter = new SectionsPagerAdapter(getSupportFragmentManager());

        // Set up the ViewPager with the sections adapter.
        mViewPager = (ViewPager) findViewById(R.id.activity_several_layers_container);
        mViewPager.setAdapter(mSectionsPagerAdapter);
        mViewPager.setOffscreenPageLimit(2);

        TabLayout tabLayout = (TabLayout) findViewById(R.id.activity_several_layers_tabs);
        tabLayout.setupWithViewPager(mViewPager);

        mLayersList = new ArrayList<IMcMapLayer>();
        lComponents=new ArrayList<>();
        mlPyramidResolutions=new ArrayList<>();
        BaseApplication.setCurrActivityContext(this);

        timerHandler.post(Funcs.getPerformPendingCalculationsRunnable(timerHandler, this));
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();

        // Stop the timer when the activity is destroyed
       Funcs.removeCallbacks(timerHandler);
    }


    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putString(NativeRaster1Str, mRaster1Path);
        outState.putString(NativeRaster2Str, mRaster2Path);
        outState.putString(NativeRaster3Str, mRaster3Path);
        outState.putString(NativeDTM1Str, mDtmPath);
        outState.putString(NativeVector1Str, mNativeVector1Path);
        outState.putString(NativeVector2Str, mNativeVector2Path);
        outState.putString(NativeVector3Str, mNativeVector3Path);
        outState.putString(NativeVector3DExtrusionStr, mNativeVector3DExtrusionPath);
        outState.putString(RawVector3DExtrusionStr, mRawVector3DExtrusionPath);
        outState.putString(Native3DModelStr, mNative3DModelPath);
        outState.putString(Raw3DModelStr, mRaw3DModelPath);
        outState.putString(RawRasterStr, mRawRasterPath);
        outState.putString(RawDTMStr, mRawDTMPath);
        outState.putString(RawVectorStr, mRawVectorPath);
    }

    @Override
    protected void onRestoreInstanceState(Bundle savedInstanceState) {
        super.onRestoreInstanceState(savedInstanceState);
        if(mSeveralLayersFragment != null)
        {
            mSeveralLayersFragment.setLayersPath(savedInstanceState.getString(NativeRaster1Str)
            ,savedInstanceState.getString(NativeRaster2Str)
            ,savedInstanceState.getString(NativeRaster3Str)
            ,savedInstanceState.getString(NativeDTM1Str)
            ,savedInstanceState.getString(NativeVector1Str)
            ,savedInstanceState.getString(NativeVector2Str)
            ,savedInstanceState.getString(NativeVector3Str)
            ,savedInstanceState.getString(NativeVector3DExtrusionStr)
            ,savedInstanceState.getString(Native3DModelStr)
            ,savedInstanceState.getString(RawRasterStr)
            ,savedInstanceState.getString(RawDTMStr)
            ,savedInstanceState.getString(RawVectorStr)
            ,savedInstanceState.getString(RawVector3DExtrusionStr)
            ,savedInstanceState.getString(Raw3DModelStr) );
        }
    }

    @Override
    public View onCreateView(View parent, String name, Context context, AttributeSet attrs) {
        View view = super.onCreateView(parent, name, context, attrs);
        return view;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }

    @Override
    public void onFragmentInteraction(Uri uri) {

    }

    private void setSelectedLayers() {
        mLayersList.clear();

        Manager_MCLayers.getInstance().setIsUseCallback(mIsUseCallback);

        addRasterLayers();
        addDtmLayer();
        addRawLayers();
        addStaticObjectsLayers();
        addNativeVectorLayers();
    }

    private void addStaticObjectsLayers() {

        if (!mNativeVector3DExtrusionPath.equals("")) {
            try {
                IMcNativeVector3DExtrusionMapLayer mcNativeVector3DExtrusionMapLayer = (IMcNativeVector3DExtrusionMapLayer) Manager_MCLayers.getInstance().CreateNativeVector3DExtrusionLayer(mNativeVector3DExtrusionPath, mDefaultNumLevelsToIgnore,0.0f, this);
                if (mcNativeVector3DExtrusionMapLayer != null) {
                    mLayersList.add(mcNativeVector3DExtrusionMapLayer);
                    Manager_MCLayers.getInstance().AddLayer(mcNativeVector3DExtrusionMapLayer);
                    isOneLayerAtLeast = true;
                }
            } catch (Exception e) {
                e.printStackTrace();
            }
        }

        if (!mNative3DModelPath.equals("")) {
            try {
                IMcNative3DModelMapLayer mcNative3DModelMapLayer = (IMcNative3DModelMapLayer) Manager_MCLayers.getInstance().CreateNative3DModelLayer(mNative3DModelPath, mDefaultNumLevelsToIgnore, this);
                if (mcNative3DModelMapLayer != null) {
                    mLayersList.add(mcNative3DModelMapLayer);
                    Manager_MCLayers.getInstance().AddLayer(mcNative3DModelMapLayer);
                    isOneLayerAtLeast = true;
                }
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    private IMcNativeRasterMapLayer createRasterLayer(String layerPath) {
        IMcNativeRasterMapLayer rasterMapLayer = (IMcNativeRasterMapLayer) Manager_MCLayers.getInstance().CreateNativeRasterLayer(layerPath,
                    mDefaultFirstLowerQuality,
                    mDefaultThereAreMissingFiles,
                    mDefaultNumLevelsToIgnore,
                    mSettingsFragment.mEnhanceBorderOverlapCb.isChecked(),
                    null,
                    this);

        return rasterMapLayer;
    }

    private void addDtmLayer() {
       final Context context = this;
        if (!mDtmPath.equals("")) {
            IMcNativeDtmMapLayer dtmMapLayer = (IMcNativeDtmMapLayer) Manager_MCLayers.getInstance().CreateNativeDTMLayer(
                    mDtmPath,
                    mDefaultNumLevelsToIgnore,
                    null, context);
            if (dtmMapLayer != null) {
                mLayersList.add(dtmMapLayer);
                isOneLayerAtLeast = true;
            }
        }
    }

    private void addRasterLayers() {
       // mRaster1Path = mSeveralLayersFragment.getRaster1Path();
        addRasterLayer(mRaster1Path);
        addRasterLayer(mRaster2Path);
        addRasterLayer(mRaster3Path);
    }

    private void addRawLayers() {
        addRawRasterLayer(mRawRasterPath);
        addRawDTMLayer(mRawDTMPath);
        addRawVectorLayer();
        addRawStaticObjectsLayer();
    }

    private void addNativeVectorLayers() {
        addNativeVectorLayer(mNativeVector1Path);
        addNativeVectorLayer(mNativeVector2Path);
        addNativeVectorLayer(mNativeVector3Path);
    }

    private void addRawDTMLayer(final String rawDTMPath) {
        if (!rawDTMPath.equals("")) {
            IMcRawDtmMapLayer rawDTMMapLayer = createRawDTMLayer(rawDTMPath);
            if (rawDTMMapLayer != null) {
                mLayersList.add(rawDTMMapLayer);
                Manager_MCLayers.getInstance().AddLayer(rawDTMMapLayer);
                isOneLayerAtLeast = true;
            }
        }
    }

    private void addRawVectorLayer() {
        if (!mRawVectorPath.equals("")) {

            IMcGridCoordinateSystem gcs = getmCoordSysForRawLayers();
            IMcRawVectorMapLayer.SParams params = new IMcRawVectorMapLayer.SParams(mRawVectorPath, gcs);
            IMcMapLayer.STilingScheme tilingScheme = mSeveralLayersFragment.getSTilingScheme();

            IMcRawVectorMapLayer rawVectorMapLayer = (IMcRawVectorMapLayer) Manager_MCLayers.getInstance().CreateRawVectorLayer(
                    params,
                    gcs,
                    tilingScheme,
                    null,
                    this
            );
            if (rawVectorMapLayer != null) {
                mLayersList.add(rawVectorMapLayer);
                Manager_MCLayers.getInstance().AddLayer(rawVectorMapLayer);
                isOneLayerAtLeast = true;
            }
        }
    }

    private IMcRawDtmMapLayer createRawDTMLayer(String rawDTMPath) {

        IMcMapLayer.SComponentParams aComp[] = new IMcMapLayer.SComponentParams[1];
        aComp[0] = new IMcMapLayer.SComponentParams();
        aComp[0].eType = IMcMapLayer.EComponentType.ECT_DIRECTORY;
        aComp[0].strName = rawDTMPath;
        IMcMapLayer.SRawParams sRawParams = new IMcMapLayer.SRawParams();
        sRawParams.aComponents = aComp;
        sRawParams.pCoordinateSystem = getmCoordSysForRawLayers();
        sRawParams.fMaxScale = Float.MAX_VALUE;
        sRawParams.pTilingScheme = mSeveralLayersFragment.getSTilingScheme();
        IMcRawDtmMapLayer NewLayer = (IMcRawDtmMapLayer) Manager_MCLayers.getInstance().CreateRawDTMLayer(sRawParams, null, this);
        if (NewLayer == null)
            isMcExThrown = true;

        return NewLayer;
    }

    private ArrayList<IMcMapLayer.SComponentParams> getCompParamsList(String rawPath) {
        ArrayList<IMcMapLayer.SComponentParams> CompParamsList = new ArrayList<>();
        IMcMapLayer.SComponentParams m_ComponentParams = new IMcMapLayer.SComponentParams();
        m_ComponentParams.eType = IMcMapLayer.EComponentType.ECT_DIRECTORY;
        m_ComponentParams.strName = rawPath;
        m_ComponentParams.WorldLimit=new SMcBox();
        m_ComponentParams.WorldLimit.MinVertex = SMcVector3D.getV3Zero();
        m_ComponentParams.WorldLimit.MaxVertex = SMcVector3D.getV3Zero();
        CompParamsList.add(m_ComponentParams);
        return CompParamsList;
    }

    private void addRawRasterLayer(final String rawRasterPath) {
        if (!rawRasterPath.equals("")) {
            IMcRawRasterMapLayer rawRasterMapLayer = createRawRasterLayer(rawRasterPath);
            if (rawRasterMapLayer != null) {
                mLayersList.add(rawRasterMapLayer);
                Manager_MCLayers.getInstance().AddLayer(rawRasterMapLayer);
                isOneLayerAtLeast = true;
            }
        }
    }

    private void addRawStaticObjectsLayer() {
        if (!mRawVector3DExtrusionPath.equals("")) {


            boolean isUseBuilt = mSeveralLayersFragment.isExtUseBuiltIndexingDataDir();
            if(isUseBuilt)
            {
                IMcRawVector3DExtrusionMapLayer.SGraphicalParams rawVector3DExtrusionGraphicalParams = mSeveralLayersFragment.getRawVector3DExtrusionGraphicalParams();
                IMcRawVector3DExtrusionMapLayer rawVector3DExtrusionMapLayer = (IMcRawVector3DExtrusionMapLayer) Manager_MCLayers.getInstance().CreateRawVector3DExtrusionLayer(
                        mRawVector3DExtrusionPath,rawVector3DExtrusionGraphicalParams, 0f,mSeveralLayersFragment.getExtNonDefaultIndexDir(), this);
                if (rawVector3DExtrusionMapLayer != null) {
                    mLayersList.add(rawVector3DExtrusionMapLayer);
                    Manager_MCLayers.getInstance().AddLayer(rawVector3DExtrusionMapLayer);
                    isOneLayerAtLeast = true;
                }
            }
            else {
                IMcRawVector3DExtrusionMapLayer.SParams rawVector3DExtrusionParams = mSeveralLayersFragment.getRawVector3DExtrusionParams();
                if (rawVector3DExtrusionParams == null) {
                    rawVector3DExtrusionParams = new IMcRawVector3DExtrusionMapLayer.SParams();
                }
                rawVector3DExtrusionParams.strDataSource = mRawVector3DExtrusionPath;

                IMcRawVector3DExtrusionMapLayer rawVector3DExtrusionMapLayer = (IMcRawVector3DExtrusionMapLayer) Manager_MCLayers.getInstance().CreateRawVector3DExtrusionLayer(rawVector3DExtrusionParams, 0f, this);
                if (rawVector3DExtrusionMapLayer != null) {
                    mLayersList.add(rawVector3DExtrusionMapLayer);
                    Manager_MCLayers.getInstance().AddLayer(rawVector3DExtrusionMapLayer);
                    isOneLayerAtLeast = true;
                }
            }
        }

        if (!mRaw3DModelPath.equals("")) {
            try {
                IMcRaw3DModelMapLayer raw3DModelMapLayer = null;
                if (mSeveralLayersFragment.is3DMModelUseBuiltIndexingDataDir()) {
                    raw3DModelMapLayer = (IMcRaw3DModelMapLayer) Manager_MCLayers.getInstance().CreateRaw3DModelLayer(
                            mRaw3DModelPath,
                            mSeveralLayersFragment.is3DMModelOrthometicHeight(),
                            mDefaultNumLevelsToIgnore, this,
                            mSeveralLayersFragment.get3DMModelNonDefaultIndexDir());

                } else {
                    raw3DModelMapLayer = (IMcRaw3DModelMapLayer) Manager_MCLayers.getInstance().CreateRaw3DModelLayer(
                            mRaw3DModelPath,
                            getmCoordSysForRawLayers(),
                            mSeveralLayersFragment.is3DMModelOrthometicHeight(),
                            mSeveralLayersFragment.get3DMModelClipRect(),
                            mSeveralLayersFragment.getSTilingScheme(),
                            mSeveralLayersFragment.get3DMModelTargetHighestResolution(),
                            this);
                }

                if (raw3DModelMapLayer != null) {
                    mLayersList.add(raw3DModelMapLayer);
                    Manager_MCLayers.getInstance().AddLayer(raw3DModelMapLayer);
                    isOneLayerAtLeast = true;
                }
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    private IMcRawRasterMapLayer createRawRasterLayer(String layerPath) {

          IMcRawRasterMapLayer NewLayer = null;

          IMcMapLayer.SComponentParams aComp[] = new IMcMapLayer.SComponentParams[1];
          aComp[0] = new IMcMapLayer.SComponentParams();
          aComp[0].eType = IMcMapLayer.EComponentType.ECT_DIRECTORY;
          aComp[0].strName = layerPath;
          IMcMapLayer.SRawParams sRawParams = new IMcMapLayer.SRawParams();
          sRawParams.aComponents = aComp;
          sRawParams.pCoordinateSystem = getmCoordSysForRawLayers();
          sRawParams.fMaxScale = Float.MAX_VALUE;
          sRawParams.pTilingScheme = mSeveralLayersFragment.getSTilingScheme();
          NewLayer = (IMcRawRasterMapLayer) Manager_MCLayers.getInstance().CreateRawRasterLayer(sRawParams, false, null, this);
          if (NewLayer == null)
              isMcExThrown = true;

          return NewLayer;
      }

    private void addRasterLayer(final String layerPath) {
        if (!layerPath.equals("")) {
            IMcNativeRasterMapLayer rasterMapLayer = createRasterLayer(layerPath);
            if (rasterMapLayer != null) {
                mLayersList.add(rasterMapLayer);
                isOneLayerAtLeast = true;
            }
        }
    }

    private void addNativeVectorLayer(final String layerPath) {
        final Context context = this;
        if (!layerPath.isEmpty()) {
            IMcNativeVectorMapLayer vectorMapLayer = (IMcNativeVectorMapLayer) Manager_MCLayers.getInstance().CreateNativeVectorLayer(layerPath,
                    null,
                    context);
            if (vectorMapLayer != null) {
                mLayersList.add(vectorMapLayer);
                isOneLayerAtLeast = true;
            }
        }
    }

    public void createVpWithLayers(View view) {
        if ((!mRawRasterPath.isEmpty() || !mRawDTMPath.isEmpty()) && getmCoordSysForRawLayers() == null) {
            AlertMessages.ShowGenericMessage(this, "Create Raw Layer", "No Grid Coordinate System for raw layer was specified.You have to choose one!");
            return;
        }
        if (mSettingsFragment.isFirstVisible) {
            mSettingsFragment.initDefaultDataFromMC();
            mSettingsFragment.initFieldsWithDefaultData();
        }
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                initDevice();
                setSelectedLayers();
               // initVpComponents();
                if (!mLayersList.isEmpty()) {
                    //if settings fragment was not visible and so that fields not initialize

                    //TODO handle reset in back or in invisible
                    AMCTViewPort.getViewportInCreation().resetCurViewPort();
                    if(!mLayersList.isEmpty() && AMCTMapLayerReadCallback.counterDelayLoadLayers >= mLayersList.size())
                        timerHandler.post(timerRunnable);
                    else
                        ContinueCreateViewport();
                }
            }
        });
    }

    private void ContinueCreateViewport()
    {
        initCoordSys();
        initTerrain();
        if (!isMcExThrown && isOneLayerAtLeast) {
            Funcs.removeCallbacks(timerHandler);
            Intent viewPortIntent = new Intent(this, MapsContainerActivity.class);
            viewPortIntent.putExtra("newViewPort",true);
            //viewPortIntent.setFlags(Intent.FLAG_ACTIVITY_REORDER_TO_FRONT);
            startActivity(viewPortIntent);

          //  startMapContainerActivity();// startViewPortActivity();
        } else {
            isMcExThrown = false;
        }
    }

    private Runnable timerRunnable = new Runnable() {
        @Override
        public void run() {
            try {
                if (AMCTMapLayerReadCallback.counterDelayLoadLayers >= mLayersList.size()) {
                    // Repeat this runnable every 1 second
                    timerHandler.postDelayed(this, 1000);
                }
                else
                {
                    ContinueCreateViewport();
                }
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    };
    private void saveSettingsInSP() {
        SharedPreferences prefs = PreferenceManager.getDefaultSharedPreferences(this);
        prefs.edit().putBoolean("toRestore", true).apply();
        prefs.edit().putBoolean("mEnhanceBorderOverlapCb", mSettingsFragment.mEnhanceBorderOverlapCb.isChecked()).apply();
        prefs.edit().putBoolean("mShowGeoInMetricProportionCb", mSettingsFragment.mShowGeoInMetricProportionCb.isChecked()).apply();
        prefs.edit().putString("mNumBgThreadsET", mSettingsFragment.mNumBgThreadsET.getText().toString()).apply();
        prefs.edit().putString("mTerrainPrecisionFactorET", mSettingsFragment.mTerrainPrecisionFactorET.getText().toString()).apply();
        prefs.edit().putString("mMaxScaleET", mSettingsFragment.mMaxScaleET.getText().toString()).apply();
        prefs.edit().putInt("mAntiAliasingLevelSpinner", mSettingsFragment.mAntiAliasingLevelSpinner.getSelectedItemPosition()).apply();
        prefs.edit().putInt("mTerrainAntiAliasingLevelSpinner", mSettingsFragment.mTerrainAntiAliasingLevelSpinner.getSelectedItemPosition()).apply();
    }

    private void startMapContainerActivity() {
        Intent viewPortIntent = new Intent(this, MapsContainerActivity.class);
        viewPortIntent.putExtra("newViewPort",true);
        //viewPortIntent.setFlags(Intent.FLAG_ACTIVITY_REORDER_TO_FRONT);
        startActivity(viewPortIntent);
    }

    private void initVpComponents() {
        // if the user input at least one layer
        if (mLayersList.size() > 0) {
            //if settings fragment was not visible and so that fields not initialize

            //TODO handle reset in back or in invisible
            AMCTViewPort.getViewportInCreation().resetCurViewPort();

            initCoordSys();
            initTerrain();
        }
    }

    private void initTerrain() {
        final Context context = this;

        IMcMapLayer[] arrLayers = mLayersList.toArray(new IMcMapLayer[mLayersList.size()]);
        // create new instance of IDNMcMapTerrain

        IMcMapTerrain mTerrain = Manager_MCTerrain.getInstance().CreateTerrain(AMCTViewPort.getViewportInCreation().getGridCoordinateSystem(), arrLayers, context);
        if (mTerrain != null) {
            AMCTViewPort.getViewportInCreation().addTerrainToList(mTerrain);
            // create new instance of IDNMcOverlayManager
            IMcOverlayManager newMcOverlayManager = Manager_MCOverlayManager.getInstance().CreateOverlayManager(AMCTViewPort.getViewportInCreation().getGridCoordinateSystem(), true);
            AMCTViewPort.getViewportInCreation().setOverlayManager(newMcOverlayManager);
            AMCTViewPort.getViewportInCreation().setShowGeoInMetricProportion(mSettingsFragment.mShowGeoInMetricProportionCb.isChecked());
            AMCTViewPort.getViewportInCreation().setMapType(mMapType);

            AMCTViewPort.getViewportInCreation().setViewportSpace(mSeveralLayersFragment.getViewportSpaceChoice());
        } else {
            isMcExThrown = true;
            mLayersList.clear();
            Manager_MCLayers.getInstance().removeAllStandaloneLayer();
        }
    }

    private IMcGridCoordinateSystem initCoordSys() {
        // get grid coordinate system from the first layer
        IMcGridCoordinateSystem gridCoordSys = getmCoordSysForRawLayers();

        try {
            //TODO temp until map core func will be fixed
            if(gridCoordSys == null && mLayersList.get(0).IsInitialized()) {
                gridCoordSys = mLayersList.get(0).GetCoordinateSystem();
            }

            Manager_MCGridCoordinateSystem.getInstance().AddNewGridCoordinateSystem(gridCoordSys);
            AMCTViewPort.getViewportInCreation().setGridCoordinateSystem(gridCoordSys);
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(this, e, "GetCoordinateSystem()");
            e.printStackTrace();
            isMcExThrown = true;
        } catch (Exception e) {
            e.printStackTrace();
        }
        return gridCoordSys;
    }

    private void initDevice() {

        // create new device if not exist
        if (AMCTMapDevice.getInstance().getDevice() == null) {
            AMCTMapDevice.getInstance().setNumBackgroundThreads(Integer.valueOf((mSettingsFragment.mNumBgThreadsET).getText().toString()));
            AMCTMapDevice.getInstance().setViewportAntiAliasingLevel((IMcMapDevice.EAntiAliasingLevel) mSettingsFragment.mAntiAliasingLevelSpinner.getSelectedItem());
            AMCTMapDevice.getInstance().setTerrainObjectsAntiAliasingLevel((IMcMapDevice.EAntiAliasingLevel) mSettingsFragment.mTerrainAntiAliasingLevelSpinner.getSelectedItem());
            IMcMapDevice device = AMCTMapDevice.getInstance().CreateDevice();
            AMCTViewPort.getViewportInCreation().setMapDevice(device);
        }
    }

    /**
     * A placeholder fragment containing a simple view.
     */
    public static class PlaceholderFragment extends Fragment {
        /**
         * The fragment argument representing the section number for this
         * fragment.
         */
        private static final String ARG_SECTION_NUMBER = "section_number";

        public PlaceholderFragment() {
        }

        /**
         * Returns a new instance of this fragment for the given section
         * number.
         */
        public static PlaceholderFragment newInstance(int sectionNumber) {
            PlaceholderFragment fragment = new PlaceholderFragment();
            Bundle args = new Bundle();
            args.putInt(ARG_SECTION_NUMBER, sectionNumber);
            fragment.setArguments(args);
            return fragment;
        }

        @Override
        public View onCreateView(LayoutInflater inflater, ViewGroup container,
                                 Bundle savedInstanceState) {
            View rootView = inflater.inflate(R.layout.fragment_view_port_with_several_layers, container, false);
            TextView textView = (TextView) rootView.findViewById(R.id.activity_several_layers_section_label);
            textView.setText(getString(R.string.section_format, getArguments().getInt(ARG_SECTION_NUMBER)));

            return rootView;
        }
    }

    /**
     * A {@link FragmentPagerAdapter} that returns a fragment corresponding to
     * one of the sections/tabs/pages.
     */
    public class SectionsPagerAdapter extends FragmentPagerAdapter {

        public SectionsPagerAdapter(FragmentManager fm) {
            super(fm);
        }

        @Override
        public Fragment getItem(int position) {
            switch (position) {
                case 0: // Fragment # 0 - This will show FirstFragment
                    return SeveralLayersFragment.newInstance();
                case 1: // Fragment # 0 - This will show FirstFragment different title
                    return SeveralLayersSetingsFragment.newInstance("2", "Page # 2");
                default:
                    return new Fragment();
            }
        }

        @Override
        public int getCount() {
            // Show 3 total pages.
            return 2;
        }

        @Override
        public CharSequence getPageTitle(int position) {
            switch (position) {
                case 0:
                    return "add several layers";
                case 1:
                    return "advanced";
                case 2:
                    return "SECTION 3";
            }
            return null;
        }
    }

    public void setMapType(IMcMapCamera.EMapType mapType) {
        mMapType = mapType;
    }
}
