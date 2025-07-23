package com.elbit.mapcore.mcandroidtester.ui.map.activities;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.FragmentTransaction;
import androidx.appcompat.app.AppCompatActivity;
import android.text.TextUtils;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.Toast;

import com.elbit.mapcore.Interfaces.Map.IMcRaw3DModelMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcRawDtmMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcRawRasterMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcRawVector3DExtrusionMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcRawVectorMapLayer;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_MCLayers;
import com.elbit.mapcore.mcandroidtester.model.AMCTMapTerrain;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.GridCoordinateSysFragment;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.Raw3DModelParamsFragment;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.RawLayersParamsFragment;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.RawVectorLayerParamsFragment;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.RawVector3DExtrusionParamsFragment;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.TilingSchemeFragment;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.BaseApplication;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.FileChooserEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SpinnerWithLabel;

import java.util.ArrayList;

import com.elbit.mapcore.Classes.Map.McMapLayer;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;

public class LayersActivity extends AppCompatActivity implements GridCoordinateSysFragment.OnFragmentInteractionListener{
    private Spinner mLayersSpinner;
    private String mLayerFileName;
    private ArrayList<IMcMapLayer> mNewLayers;
    private RawLayersParamsFragment mRawLayersParamsFragment;
    private RawVectorLayerParamsFragment mRawVectorLayerParamsFragment;
    private RawVector3DExtrusionParamsFragment mRawVector3DExtrusionLayerParamsFragment;
    private Raw3DModelParamsFragment mRaw3DModelLayerParamsFragment;
    private TilingSchemeFragment mTilingSchemeFragment;
    private FileChooserEditTextLabel mFileChooserEditTextLabel;
    private SpinnerWithLabel mSWL;
    private NumericEditTextLabel mFirstLowerQualityLevelET;
    private IMcMapLayer mNewLayer;

    private CheckBox mIsUseCallbackCB;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_layers);
        setLayerTypeSpinner();
        setBttns();
        BaseApplication.setCurrActivityContext(this);
    }

    private void setLayerTypeSpinner() {
        mFileChooserEditTextLabel = (FileChooserEditTextLabel) findViewById(R.id.fel_layer_file_name);
        final ArrayList<IMcMapLayer.ELayerType> layerTypes = hideIrrelevantLayerTypes();
        mSWL = (SpinnerWithLabel) findViewById(R.id.layer_type_swl);
        mLayersSpinner = (Spinner) mSWL.findViewById(R.id.spinner_in_cv);
        mLayersSpinner.setAdapter(new ArrayAdapter<>(this, android.R.layout.simple_spinner_item, layerTypes));
 
        mLayersSpinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                IMcMapLayer.ELayerType layerType = layerTypes.get(position);
                if (layerType.equals(IMcMapLayer.ELayerType.ELT_RAW_DTM) || layerType.equals(IMcMapLayer.ELayerType.ELT_RAW_RASTER))
                    addRawLayersParamsFragment();
                else
                    hideRawLayersParamsFragment();

                if (layerType.equals(IMcMapLayer.ELayerType.ELT_RAW_VECTOR)) {
                    addRawVectorLayerParamsFragment();
                } else {
                    hideRawVectorLayerParamsFragment();
                }

                if (layerType.equals(IMcMapLayer.ELayerType.ELT_RAW_VECTOR_3D_EXTRUSION)) {
                    addRawVector3DExtrusionLayerParamsFragment();
                } else {
                    hideRawVector3DExtrusionLayerParamsFragment();
                }

                if (layerType.equals(IMcMapLayer.ELayerType.ELT_RAW_3D_MODEL)) {
                    addRaw3DModelLayerParamsFragment();
                } else {
                    hideRaw3DModelLayerParamsFragment();
                }

                if (layerType.equals(IMcMapLayer.ELayerType.ELT_RAW_RASTER) || layerType.equals(IMcMapLayer.ELayerType.ELT_RAW_DTM) || layerType.equals(IMcMapLayer.ELayerType.ELT_RAW_VECTOR)) {
                    addTilingSchemeFragment();
                } else {
                    hideTilingSchemeFragment();
                }
            }


            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });
    }

    private void addRawVectorLayerParamsFragment()
    {
        FragmentTransaction transaction;
        mRawVectorLayerParamsFragment = (RawVectorLayerParamsFragment) getSupportFragmentManager().findFragmentByTag("rawVectorLayerParamsFragment");
        if (mRawVectorLayerParamsFragment == null) {
            mRawVectorLayerParamsFragment = RawVectorLayerParamsFragment.newInstance();
            transaction = getSupportFragmentManager().beginTransaction().add(R.id.create_new_layer_raw_vector_layer_params_container, mRawVectorLayerParamsFragment, "rawVectorLayerParamsFragment");
        } else
            transaction = getSupportFragmentManager().beginTransaction().show(mRawVectorLayerParamsFragment);

        transaction.addToBackStack("rawVectorLayerParamsFragment");
        transaction.commit();
    }

    private void hideRawVectorLayerParamsFragment() {
        if (mRawVectorLayerParamsFragment != null) {
            FragmentTransaction transaction = getSupportFragmentManager().beginTransaction().hide(mRawVectorLayerParamsFragment);
            transaction.addToBackStack("rawVectorLayerParamsFragment");
            transaction.commit();
        }
    }

    private void addRawVector3DExtrusionLayerParamsFragment()
    {
        FragmentTransaction transaction;
        mRawVector3DExtrusionLayerParamsFragment = (RawVector3DExtrusionParamsFragment) getSupportFragmentManager().findFragmentByTag("rawVector3DExtrusionLayerParamsFragment");
        if (mRawVector3DExtrusionLayerParamsFragment == null) {
            mRawVector3DExtrusionLayerParamsFragment = RawVector3DExtrusionParamsFragment.newInstance();
            transaction = getSupportFragmentManager().beginTransaction().add(R.id.create_new_layer_raw_vector_3d_extrusion_layer_params_container, mRawVector3DExtrusionLayerParamsFragment, "rawVector3DExtrusionLayerParamsFragment");
        } else
            transaction = getSupportFragmentManager().beginTransaction().show(mRawVector3DExtrusionLayerParamsFragment);
      //  mRawVector3DExtrusionLayerParamsFragment.HideSaveButton();
        transaction.addToBackStack("rawVector3DExtrusionLayerParamsFragment");
        transaction.commit();
    }

    private void hideRawVector3DExtrusionLayerParamsFragment() {
        if (mRawVector3DExtrusionLayerParamsFragment != null) {
            FragmentTransaction transaction = getSupportFragmentManager().beginTransaction().hide(mRawVector3DExtrusionLayerParamsFragment);
            transaction.addToBackStack("rawVector3DExtrusionLayerParamsFragment");
            transaction.commit();
        }
    }

    private void addRaw3DModelLayerParamsFragment()
    {
        FragmentTransaction transaction;
        mRaw3DModelLayerParamsFragment = (Raw3DModelParamsFragment) getSupportFragmentManager().findFragmentByTag("raw3DModelLayerParamsFragment");
        if (mRaw3DModelLayerParamsFragment == null) {
            mRaw3DModelLayerParamsFragment = Raw3DModelParamsFragment.newInstance();
            transaction = getSupportFragmentManager().beginTransaction().add(R.id.create_new_layer_raw_vector_3d_extrusion_layer_params_container, mRaw3DModelLayerParamsFragment, "raw3DModelLayerParamsFragment");
        } else
            transaction = getSupportFragmentManager().beginTransaction().show(mRaw3DModelLayerParamsFragment);
      //  mRaw3DModelLayerParamsFragment.HideSaveButton();
        transaction.addToBackStack("raw3DModelLayerParamsFragment");
        transaction.commit();
    }

    private void hideRaw3DModelLayerParamsFragment() {
        if (mRaw3DModelLayerParamsFragment != null) {
            FragmentTransaction transaction = getSupportFragmentManager().beginTransaction().hide(mRaw3DModelLayerParamsFragment);
            transaction.addToBackStack("raw3DModelLayerParamsFragment");
            transaction.commit();
        }
    }

    private void addTilingSchemeFragment()
    {
        FragmentTransaction transaction;
        mTilingSchemeFragment = (TilingSchemeFragment) getSupportFragmentManager().findFragmentByTag("tilingSchemeFragment");
        if (mTilingSchemeFragment == null) {
            mTilingSchemeFragment = TilingSchemeFragment.newInstance();
            transaction = getSupportFragmentManager().beginTransaction().add(R.id.create_new_layer_tiling_scheme_container, mTilingSchemeFragment, "tilingSchemeFragment");
        } else
            transaction = getSupportFragmentManager().beginTransaction().show(mTilingSchemeFragment);
        //  mRawVector3DExtrusionLayerParamsFragment.HideSaveButton();
        transaction.addToBackStack("tilingSchemeFragment");
        transaction.commit();
    }

    private void hideTilingSchemeFragment() {
        if (mTilingSchemeFragment != null) {
            FragmentTransaction transaction = getSupportFragmentManager().beginTransaction().hide(mTilingSchemeFragment);
            transaction.addToBackStack("tilingSchemeFragment");
            transaction.commit();
        }
    }

    private void addRawLayersParamsFragment() {
        FragmentTransaction transaction;
        mRawLayersParamsFragment = (RawLayersParamsFragment) getSupportFragmentManager().findFragmentByTag("rawLayersParamsFragment");
        if (mRawLayersParamsFragment == null) {
            mRawLayersParamsFragment = RawLayersParamsFragment.newInstance();
            transaction = getSupportFragmentManager().beginTransaction().add(R.id.create_new_layer_raw_layers_params_container, mRawLayersParamsFragment, "rawLayersParamsFragment");
        } else
            transaction = getSupportFragmentManager().beginTransaction().show(mRawLayersParamsFragment);

        transaction.addToBackStack("rawLayersParamsFragment");
        transaction.commit();
    }

    private void hideRawLayersParamsFragment() {
        if (mRawLayersParamsFragment != null) {
            FragmentTransaction transaction = getSupportFragmentManager().beginTransaction().hide(mRawLayersParamsFragment);
            transaction.addToBackStack("rawLayersParamsFragment");
            transaction.commit();
        }
    }

    private ArrayList<McMapLayer.ELayerType> hideIrrelevantLayerTypes() {
        //add relevant layer types
        ArrayList<McMapLayer.ELayerType> layerTypes = new ArrayList<>();
       
        layerTypes.add(McMapLayer.ELayerType.ELT_NATIVE_DTM);
        layerTypes.add(McMapLayer.ELayerType.ELT_RAW_DTM);
        layerTypes.add(McMapLayer.ELayerType.ELT_NATIVE_RASTER);
        layerTypes.add(McMapLayer.ELayerType.ELT_RAW_RASTER);
        layerTypes.add(McMapLayer.ELayerType.ELT_NATIVE_VECTOR);
        layerTypes.add(McMapLayer.ELayerType.ELT_RAW_VECTOR);
        layerTypes.add(McMapLayer.ELayerType.ELT_NATIVE_3D_MODEL);
        layerTypes.add(McMapLayer.ELayerType.ELT_NATIVE_VECTOR_3D_EXTRUSION);
        layerTypes.add(McMapLayer.ELayerType.ELT_RAW_3D_MODEL);
        layerTypes.add(McMapLayer.ELayerType.ELT_RAW_VECTOR_3D_EXTRUSION);
        return layerTypes;
    }

    private void setBttns() {
        mIsUseCallbackCB = (CheckBox)findViewById(R.id.is_use_callback_cb) ;
        mIsUseCallbackCB.setChecked(true);

        mFirstLowerQualityLevelET = (NumericEditTextLabel) findViewById(R.id.first_lower_quality_level_et);
        mFirstLowerQualityLevelET.setText("Max");
    }

    public void createLayer(View view) throws MapCoreException {
        createNewLayer();
    }

    public void createNewLayer() throws MapCoreException {
        IMcMapLayer.SLocalCacheLayerParams localCacheLayerParams = null;

        mLayerFileName = ((EditText) mFileChooserEditTextLabel.findViewById(R.id.edittext_in_cv_fel)).getText().toString();
        final int numLevelsToIgnore = Integer.valueOf(((EditText) findViewById(R.id.num_of_levels_to_ignore_et)).getText().toString());
        final float extrusionHeightMaxAddition = Float.valueOf(((EditText) findViewById(R.id.extrusion_height_max_addition_et)).getText().toString());
        final int numFirstLowerQualityLevel = mFirstLowerQualityLevelET.getUInt();
        final boolean bThereAreMissingFile = ((CheckBox) findViewById(R.id.there_are_missing_files_cb)).isChecked();
        final boolean bEnhanceBorderOverlap = ((CheckBox) findViewById(R.id.enhance_border_overlap_cb)).isChecked();

        Manager_MCLayers.getInstance().setIsUseCallback(mIsUseCallbackCB.isChecked());

        mNewLayers = new ArrayList<>();
        EditText localCache = (EditText) findViewById(R.id.local_cache_sub_folder_et);
        if (!TextUtils.isEmpty(localCache.getText())) {
            localCacheLayerParams.strLocalCacheSubFolder = localCache.getText().toString();
        }
        final IMcMapLayer.SLocalCacheLayerParams finalLocalCacheLayerParams = localCacheLayerParams;
        final IMcMapLayer.ELayerType layerTypeToCreate = (IMcMapLayer.ELayerType) mLayersSpinner.getSelectedItem();
        final Context context = this;
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {

                for (int i = 0; i < Integer.valueOf(((EditText) findViewById(R.id.num_of_layers_et)).getText().toString()); i++) {
                    mNewLayer = null;

                    if (layerTypeToCreate == IMcMapLayer.ELayerType.ELT_NATIVE_DTM)
                        mNewLayer = Manager_MCLayers.getInstance().CreateNativeDTMLayer(mLayerFileName, numLevelsToIgnore, finalLocalCacheLayerParams, context);
                    else if (layerTypeToCreate == IMcMapLayer.ELayerType.ELT_NATIVE_RASTER)
                        mNewLayer = Manager_MCLayers.getInstance().CreateNativeRasterLayer(mLayerFileName, numFirstLowerQualityLevel, bThereAreMissingFile, numLevelsToIgnore, bEnhanceBorderOverlap, finalLocalCacheLayerParams, context);
                    else if (layerTypeToCreate == IMcMapLayer.ELayerType.ELT_NATIVE_3D_MODEL)
                        mNewLayer = Manager_MCLayers.getInstance().CreateNative3DModelLayer(mLayerFileName, numLevelsToIgnore, context);
                    else if (layerTypeToCreate == IMcMapLayer.ELayerType.ELT_RAW_3D_MODEL) {
                        if (mRaw3DModelLayerParamsFragment.IsUseBuiltIndexingDataDir()) {
                            mNewLayer = Manager_MCLayers.getInstance().CreateRaw3DModelLayer(
                                    mLayerFileName,
                                    mRaw3DModelLayerParamsFragment.getOrthometicHeight(),
                                    numLevelsToIgnore,
                                    context,
                                    mRaw3DModelLayerParamsFragment.getNonDefaultIndexDir());

                        } else {
                            mNewLayer = Manager_MCLayers.getInstance().CreateRaw3DModelLayer(
                                    mLayerFileName,
                                    mRaw3DModelLayerParamsFragment.getGridCoordinateSystem(),
                                    mRaw3DModelLayerParamsFragment.getOrthometicHeight(),
                                    mRaw3DModelLayerParamsFragment.getClipRect(),
                                    mRaw3DModelLayerParamsFragment.getTilingScheme(),
                                    mRaw3DModelLayerParamsFragment.getTargetHighestResolution(),
                                    context);
                        }
                    }
                    else if (layerTypeToCreate == IMcMapLayer.ELayerType.ELT_NATIVE_VECTOR_3D_EXTRUSION)
                        mNewLayer = Manager_MCLayers.getInstance().CreateNativeVector3DExtrusionLayer(mLayerFileName, numLevelsToIgnore, extrusionHeightMaxAddition, context);
                    else if (layerTypeToCreate == IMcMapLayer.ELayerType.ELT_RAW_VECTOR_3D_EXTRUSION) {

                        if(mRawVector3DExtrusionLayerParamsFragment.IsUseBuiltIndexingDataDir()) {
                            IMcRawVector3DExtrusionMapLayer.SGraphicalParams graphicalParams = mRawVector3DExtrusionLayerParamsFragment.getGraphicalParams();
                            mNewLayer = Manager_MCLayers.getInstance().CreateRawVector3DExtrusionLayer(mLayerFileName,
                                    graphicalParams,
                                    extrusionHeightMaxAddition,
                                    mRawVector3DExtrusionLayerParamsFragment.getNonDefaultIndexDir(),
                                    context);

                        }
                        else {
                            IMcRawVector3DExtrusionMapLayer.SParams params = mRawVector3DExtrusionLayerParamsFragment.getParams();
                            if (params == null)
                                params = new IMcRawVector3DExtrusionMapLayer.SParams();
                            params.strDataSource = mLayerFileName;

                            mNewLayer = Manager_MCLayers.getInstance().CreateRawVector3DExtrusionLayer(params, extrusionHeightMaxAddition, context);
                        }
                    }
                    else if (layerTypeToCreate == IMcMapLayer.ELayerType.ELT_RAW_VECTOR) {
                        if (mRawVectorLayerParamsFragment.getSourceGridCoordinateSystem() == null || mRawVectorLayerParamsFragment.getViewportGridCoordinateSystem() == null) {
                            AlertMessages.ShowErrorMessage(context, "Missing Grid Coordinate System", "No Grid Coordinate System was specified.\nYou have to choose one!\n");
                            return;
                        }
                        IMcRawVectorMapLayer.SParams params = new IMcRawVectorMapLayer.SParams(mLayerFileName,
                                mRawVectorLayerParamsFragment.getSourceGridCoordinateSystem(),
                                mRawVectorLayerParamsFragment.getMinScale(),
                        mRawVectorLayerParamsFragment.getMaxScale(),
                        mRawVectorLayerParamsFragment.getPointTextureFile(),
                        mRawVectorLayerParamsFragment.getLocaleStr(),
                        0,
                        mRawVectorLayerParamsFragment.getWorldLimit(),
                        new IMcRawVectorMapLayer.SInternalStylingParams());

                        params.uMaxNumVerticesPerTile = mRawVectorLayerParamsFragment.getMaxNumVerticesPerTile();
                        params.uMaxNumVisiblePointObjectsPerTile = mRawVectorLayerParamsFragment.getMaxNumVisiblePointObjectsPerTile();
                        params.uMinPixelSizeForObjectVisibility = mRawVectorLayerParamsFragment.getMinPixelSizeForObjectVisibility();
                        params.fOptimizationMinScale = mRawVectorLayerParamsFragment.getOptimizationMinScale();

                        mNewLayer = Manager_MCLayers.getInstance().CreateRawVectorLayer(params,
                                mRawVectorLayerParamsFragment.getViewportGridCoordinateSystem(),
                                mTilingSchemeFragment.getParams(),
                                finalLocalCacheLayerParams,
                                context);
                    }
                    else if(layerTypeToCreate == IMcMapLayer.ELayerType.ELT_NATIVE_VECTOR)
                    {
                        mNewLayer = Manager_MCLayers.getInstance().CreateNativeVectorLayer(mLayerFileName,null,context);
                    }
                    else if (layerTypeToCreate == IMcMapLayer.ELayerType.ELT_RAW_DTM || layerTypeToCreate == IMcMapLayer.ELayerType.ELT_RAW_RASTER) {
                        if (mRawLayersParamsFragment == null) {
                            AlertMessages.ShowGenericMessage(context, "raw layer error", "invalid raw layers params");
                            return;
                        } else {
                            if (mRawLayersParamsFragment.getGridCoordinateSystem() == null) {
                                AlertMessages.ShowGenericMessage(context, "raw layer error", "No Grid Coordinate System was specified.\nYou have to choose one!\n");
                                return;
                            }
                            IMcMapLayer.SRawParams dnParams = mRawLayersParamsFragment.getParams();
                            dnParams.pTilingScheme = mTilingSchemeFragment.getParams();

                            if (layerTypeToCreate == IMcMapLayer.ELayerType.ELT_RAW_DTM) {
                                mNewLayer = Manager_MCLayers.getInstance().CreateRawDTMLayer(dnParams, null, context);
                            } else if (layerTypeToCreate == IMcMapLayer.ELayerType.ELT_RAW_RASTER) {
                                mNewLayer = Manager_MCLayers.getInstance().CreateRawRasterLayer(dnParams, mRawLayersParamsFragment.getIsImageCoordSys(), finalLocalCacheLayerParams, context);
                            }
                        }
                    }

                    if (mNewLayer != null) {
                        mNewLayers.add(mNewLayer);
                    } else {
                        AlertMessages.ShowErrorMessage(context, "create new layer", "Failed to create new layer");
                        return;
                    }
                }
                if (!mNewLayers.isEmpty()) {
                    AMCTMapTerrain.getInstance().addLayer(mNewLayer);
                    Manager_MCLayers.getInstance().AddLayer(mNewLayer);
                    runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            Toast.makeText(context, "layer created successfully", Toast.LENGTH_LONG).show();
                            onBackPressed();
                            if (mNewLayer instanceof IMcRawRasterMapLayer ||
                                    mNewLayer instanceof IMcRawDtmMapLayer ||
                                    mNewLayer instanceof IMcRawVectorMapLayer ||
                                    mNewLayer instanceof IMcRaw3DModelMapLayer ||
                                    mNewLayer instanceof IMcRawVector3DExtrusionMapLayer) {
                                onBackPressed();
                                onBackPressed();
                                if (mNewLayer instanceof IMcRawVector3DExtrusionMapLayer ||
                                        mNewLayer instanceof IMcRaw3DModelMapLayer)
                                    onBackPressed();
                            }
                        }
                    });

                } else
                    AlertMessages.ShowErrorMessage(context, "create new layer", "Failed to create new layer");
            }
        });
    }

    @Override
    public void onFragmentInteraction(Uri uri) {

    }
}
