package com.elbit.mapcore.mcandroidtester.ui.device.fragments;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.CheckBox;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.model.AMCTMapDevice;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.FileChooserEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SpinnerWithLabel;

import com.elbit.mapcore.Interfaces.Map.IMcMapDevice;

public class CreateDeviceFragment extends Fragment {

    private OnFragmentInteractionListener mListener;

    public CreateDeviceFragment() {
        // Required empty public constructor
    }

    public static CreateDeviceFragment newInstance() {
        CreateDeviceFragment fragment = new CreateDeviceFragment();
        return fragment;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        View inflaterView = inflater.inflate(R.layout.fragment_create_device, container, false);
        if (AMCTMapDevice.getInstance().getDevice() != null) {
           Funcs.enableDisableViewGroup((ViewGroup)(inflaterView.findViewById(R.id.fcd_container)),false);
        }
        return inflaterView;
    }

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        InitializeComponents();
    }

    public void SaveData()
    {
        AMCTMapDevice.getInstance().setLoggingLevel((IMcMapDevice.ELoggingLevel)getSpinnerSelection(R.id.logging_level_cv));
        AMCTMapDevice.getInstance().setTerrainObjectsQuality((IMcMapDevice.ETerrainObjectsQuality)getSpinnerSelection(R.id.terrain_object_quality_cv));
        AMCTMapDevice.getInstance().setRenderingSystem((IMcMapDevice.ERenderingSystem)getSpinnerSelection(R.id.rendering_system_cv));
        AMCTMapDevice.getInstance().setViewportAntiAliasingLevel((IMcMapDevice.EAntiAliasingLevel)getSpinnerSelection(R.id.viewport_anti_aliasing_level_cv));
        AMCTMapDevice.getInstance().setTerrainObjectsAntiAliasingLevel((IMcMapDevice.EAntiAliasingLevel)getSpinnerSelection(R.id.terrain_object_anti_aliasing_level_cv));
        AMCTMapDevice.getInstance().setStaticObjectsVisibilityQueryPrecision((IMcMapDevice.EStaticObjectsVisibilityQueryPrecision)getSpinnerSelection(R.id.static_objects_visibility_query_precision_cv));
        AMCTMapDevice.getInstance().setLogFileDirectory(getStringValue(R.id.log_dir));
        AMCTMapDevice.getInstance().setConfigFilesDirectory(getStringValue(R.id.config_dir));
        AMCTMapDevice.getInstance().setPrefixForPathsInResourceFile(getStringValue(R.id.prefix_for_paths_in_resource_file_et));
        AMCTMapDevice.getInstance().setMapLayersLocalCacheFolder(getStringValue(R.id.local_cache_folder));
        AMCTMapDevice.getInstance().setNumBackgroundThreads(getIntValue(R.id.num_background_threads_et));
        AMCTMapDevice.getInstance().setNumTerrainTileRenderTargets(getIntValue(R.id.num_terrain_tile_render_targets_et));
        AMCTMapDevice.getInstance().setObjectsBatchGrowthRatio(getFloatValue(R.id.objects_batch_growth_ratio_et));
        AMCTMapDevice.getInstance().setObjectsTexturesAtlasSize(getIntValue(R.id.objects_texture_atlas_size));
        AMCTMapDevice.getInstance().setObjectsBatchInitialNumVertices(getIntValue(R.id.objects_batch_initial_num_vertices_et));
        AMCTMapDevice.getInstance().setObjectsBatchInitialNumIndices(getIntValue(R.id.objects_batch_initial_num_indices_et));
        AMCTMapDevice.getInstance().setMapLayersLocalCacheSizeInMB(getIntValue(R.id.max_size_in_MB_et));
        AMCTMapDevice.getInstance().setDtmVisualizationPrecision(getIntValue(R.id.dtm_visualization_precision_et));
        AMCTMapDevice.getInstance().setMainMonitorIndex(getIntValue(R.id.main_monitor_index_et));
        AMCTMapDevice.getInstance().setShaderBasedRenderingSystem(getBooleanValue(R.id.shader_based_rendering_system_cb));
        AMCTMapDevice.getInstance().setOpenConfigWindow(getBooleanValue(R.id.open_config_window_cb));
        AMCTMapDevice.getInstance().setObjectsTexturesAtlas16bit(getBooleanValue(R.id.objects_texture_atlas_16_bit_cb));
        AMCTMapDevice.getInstance().setDisableDepthBuffer(getBooleanValue(R.id.disable_depth_buffer_cb));
        AMCTMapDevice.getInstance().setEnableObjectsBatchEnlarging(getBooleanValue(R.id.enable_objects_batch_enlarging_cb));
        AMCTMapDevice.getInstance().setEnableGLQuadBufferStereo(getBooleanValue(R.id.enable_GL_quad_buffer_stereo_cb));
        AMCTMapDevice.getInstance().setAlignScreenSizeObjects(getBooleanValue(R.id.align_screen_size_objects_cb));
        AMCTMapDevice.getInstance().setPreferUseTerrainTileRenderTargets(getBooleanValue(R.id.prefer_use_terrain_tile_render_targets_cb));
        AMCTMapDevice.getInstance().setIgnoreRasterLayerMipmaps(getBooleanValue(R.id.ignore_raster_layer_mipmaps_cb));
        AMCTMapDevice.getInstance().setMultiThreadedDevice(getBooleanValue(R.id.multi_threaded_device_cb));
        AMCTMapDevice.getInstance().setMultiScreenDevice(getBooleanValue(R.id.multi_screen_device_cb));
        AMCTMapDevice.getInstance().setFullScreen(getBooleanValue(R.id.full_screen_cb));
    }

    private void InitializeComponents() {
        initSpinners();
        initCheckBoxes();
        initFileChoosers();
        initEditTexts();
    }

    private void initEditTexts() {
        setDefaultValue(R.id.num_background_threads_et, AMCTMapDevice.getInstance().getNumBackgroundThreads());
        setDefaultValue(R.id.num_terrain_tile_render_targets_et, AMCTMapDevice.getInstance().getNumTerrainTileRenderTargets());
        setDefaultValue(R.id.objects_batch_growth_ratio_et, AMCTMapDevice.getInstance().getObjectsBatchGrowthRatio());
        setDefaultValue(R.id.objects_texture_atlas_size, AMCTMapDevice.getInstance().getObjectsTexturesAtlasSize());
        setDefaultValue(R.id.objects_batch_initial_num_vertices_et, AMCTMapDevice.getInstance().getObjectsBatchInitialNumVertices());
        setDefaultValue(R.id.objects_batch_initial_num_indices_et, AMCTMapDevice.getInstance().getObjectsBatchInitialNumIndices());
        setDefaultValue(R.id.max_size_in_MB_et, AMCTMapDevice.getInstance().getMapLayersLocalCacheSizeInMB());
        setDefaultValue(R.id.dtm_visualization_precision_et, AMCTMapDevice.getInstance().getDtmVisualizationPrecision());
        setDefaultValue(R.id.main_monitor_index_et, AMCTMapDevice.getInstance().getMainMonitorIndex());
        setDefaultValue(R.id.prefix_for_paths_in_resource_file_et, AMCTMapDevice.getInstance().getPrefixForPathsInResourceFile());
    }

    public void setDefaultValue(int id, int defVal) {
        ((NumericEditTextLabel) getActivity().findViewById(id)).setText(String.valueOf(defVal));
    }

    public void setDefaultValue(int id, float defVal) {
        ((NumericEditTextLabel) getActivity().findViewById(id)).setText(String.valueOf(defVal));
    }

    private void initFileChoosers() {
        setDefaultValue(R.id.log_dir, AMCTMapDevice.getInstance().getLogFileDirectory());
        setDefaultValue(R.id.config_dir, AMCTMapDevice.getInstance().getConfigFilesDirectory());
        setDefaultValue(R.id.local_cache_folder, AMCTMapDevice.getInstance().getMapLayersLocalCacheFolder());
    }

    private void initCheckBoxes() {

        java.lang.reflect.Method method;
        setDefaultValue(R.id.shader_based_rendering_system_cb, AMCTMapDevice.getInstance().getIsShaderBasedRenderingSystem());
        setDefaultValue(R.id.open_config_window_cb, AMCTMapDevice.getInstance().getIsOpenConfigWindow());
        setDefaultValue(R.id.objects_texture_atlas_16_bit_cb, AMCTMapDevice.getInstance().getIsObjectsTexturesAtlas16bit());
        setDefaultValue(R.id.disable_depth_buffer_cb, AMCTMapDevice.getInstance().getIsDisableDepthBuffer());
        setDefaultValue(R.id.enable_objects_batch_enlarging_cb, AMCTMapDevice.getInstance().getIsEnableObjectsBatchEnlarging());
        setDefaultValue(R.id.enable_GL_quad_buffer_stereo_cb, AMCTMapDevice.getInstance().getIsEnableGLQuadBufferStereo());
        setDefaultValue(R.id.align_screen_size_objects_cb, AMCTMapDevice.getInstance().getIsAlignScreenSizeObjects());
        setDefaultValue(R.id.prefer_use_terrain_tile_render_targets_cb, AMCTMapDevice.getInstance().getIsPreferUseTerrainTileRenderTargets());
        setDefaultValue(R.id.multi_screen_device_cb, AMCTMapDevice.getInstance().getIsMultiScreenDevice());
        setDefaultValue(R.id.ignore_raster_layer_mipmaps_cb, AMCTMapDevice.getInstance().getIsIgnoreRasterLayerMipmaps());
        setDefaultValue(R.id.multi_threaded_device_cb, AMCTMapDevice.getInstance().getIsMultiThreadedDevice());
        setDefaultValue(R.id.full_screen_cb, AMCTMapDevice.getInstance().getIsFullScreen());
    }

    private void setDefaultValue(int id, boolean defVal) {
        ((CheckBox) this.getActivity().findViewById(id)).setChecked(defVal);
    }

    private void setDefaultValue(int id, String defVal) {
        View view = this.getActivity().findViewById(id);
        if(view instanceof FileChooserEditTextLabel)
            ((FileChooserEditTextLabel)view).setDirPath(defVal);
        else if(view instanceof NumericEditTextLabel)
            ((NumericEditTextLabel)view).setText(defVal);
    }

    private String getStringValue(int id) {
        View view = this.getActivity().findViewById(id);
        if(view instanceof FileChooserEditTextLabel)
            return ((FileChooserEditTextLabel)view).getDirPath();
        else if(view instanceof NumericEditTextLabel)
            return ((NumericEditTextLabel)view).getText();
        return "";
    }

    private float getFloatValue(int id) {
        return ((NumericEditTextLabel) this.getActivity().findViewById(id)).getFloat();
    }

    private int getIntValue(int id) {
        return ((NumericEditTextLabel) this.getActivity().findViewById(id)).getInt();
    }

    private boolean getBooleanValue(int id) {
        return ((CheckBox) this.getActivity().findViewById(id)).isChecked();
    }

    public void initSpinners() {
        setSpinner(R.id.logging_level_cv, IMcMapDevice.ELoggingLevel.values(), AMCTMapDevice.getInstance().getLoggingLevel().getValue());
        //setSpinner(R.id.logging_level_cv, IMcMapDevice.ELoggingLevel.values(), 3); // HIGH
        setSpinner(R.id.terrain_object_quality_cv, IMcMapDevice.ETerrainObjectsQuality.values(), AMCTMapDevice.getInstance().getTerrainObjectsQuality().getValue());
        setSpinner(R.id.rendering_system_cv, IMcMapDevice.ERenderingSystem.values(), AMCTMapDevice.getInstance().getRenderingSystem().getValue());
        setSpinner(R.id.viewport_anti_aliasing_level_cv, IMcMapDevice.EAntiAliasingLevel.values(), AMCTMapDevice.getInstance().getViewportAntiAliasingLevel().getValue());
        setSpinner(R.id.terrain_object_anti_aliasing_level_cv, IMcMapDevice.EAntiAliasingLevel.values(), AMCTMapDevice.getInstance().getTerrainObjectsAntiAliasingLevel().getValue());
        setSpinner(R.id.static_objects_visibility_query_precision_cv, IMcMapDevice.EStaticObjectsVisibilityQueryPrecision.values(), AMCTMapDevice.getInstance().getStaticObjectsVisibilityQueryPrecision().getValue());
    }

    private void setSpinner(int id, Object[] values, int defVal) {
        SpinnerWithLabel SWL = (SpinnerWithLabel) this.getActivity().findViewById(id);
        SWL.setSpinner(values,  defVal);
    }

    private Object getSpinnerSelection(int id) {
        SpinnerWithLabel SWL = (SpinnerWithLabel) this.getActivity().findViewById(id);
        return SWL.getSelectedItem();
    }

    @Override
    public void onAttach(Context context) {
        super.onAttach(context);
        if (context instanceof OnFragmentInteractionListener) {
            mListener = (OnFragmentInteractionListener) context;
        } else {
            throw new RuntimeException(context.toString()
                    + " must implement OnFragmentInteractionListener");
        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    /**
     * This interface must be implemented by activities that contain this
     * fragment to allow an interaction in this fragment to be communicated
     * to the activity and potentially other fragments contained in that
     * activity.
     * <p/>
     * See the Android Training lesson <a href=
     * "http://developer.android.com/training/basics/fragments/communicating.html"
     * >Communicating with Other Fragments</a> for more information.
     */
    public interface OnFragmentInteractionListener {
        // TODO: Update argument type and name
        void onFragmentInteraction(Uri uri);
    }
}
