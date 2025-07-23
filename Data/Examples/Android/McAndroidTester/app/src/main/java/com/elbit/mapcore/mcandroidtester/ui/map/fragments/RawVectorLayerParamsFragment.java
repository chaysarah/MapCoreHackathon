package com.elbit.mapcore.mcandroidtester.ui.map.fragments;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;

import com.elbit.mapcore.Interfaces.Map.IMcRawVectorMapLayer;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.OnCreateCoordinateSystemListener;
import com.elbit.mapcore.mcandroidtester.utils.customviews.FileChooserEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.WorldBoundingBox;

import com.elbit.mapcore.Interfaces.Calculations.GridCoordSystem.IMcGridCoordinateSystem;
import com.elbit.mapcore.Structs.SMcBox;
import com.elbit.mapcore.Structs.SMcVector3D;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link RawVectorLayerParamsFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link RawVectorLayerParamsFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class RawVectorLayerParamsFragment extends Fragment implements OnCreateCoordinateSystemListener{

    private OnFragmentInteractionListener mListener;
    private View mRootView;
    private WorldBoundingBox mWorldLimit;
    private NumericEditTextLabel mMaxScaleNET;
    private NumericEditTextLabel mMinScaleNET;
    private EditText mLocaleStrET;
    private FileChooserEditTextLabel mPointTextureFile;
    private NumericEditTextLabel mMaxNumVerticesPerTileNET;
    private NumericEditTextLabel mMaxNumVisiblePointObjectsPerTileNET;
    private NumericEditTextLabel mMinPixelSizeForObjectVisibilityNET;
    private NumericEditTextLabel mOptimizationMinScaleNET;
    private IMcRawVectorMapLayer.SParams mParams;
    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment RawLayersParamsFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static RawVectorLayerParamsFragment newInstance() {
        RawVectorLayerParamsFragment fragment = new RawVectorLayerParamsFragment();
        return fragment;
    }

    public RawVectorLayerParamsFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        mRootView = inflater.inflate(R.layout.fragment_raw_vector_layer_params, container, false);

        initViews();
        return mRootView;
    }

    private void initViews() {
        inflateViews();
        mParams = new IMcRawVectorMapLayer.SParams();
        initScales();
        initWorldLimit();
        initParams();
    }

    private void initScales() {
        mMaxScaleNET.setFloat(Float.MAX_VALUE);
        mMinScaleNET.setFloat(mParams.fMinScale);
    }

    private void initWorldLimit() {
        mWorldLimit.initWorldBoundingBox(new SMcBox(new SMcVector3D(0, 0, 0), new SMcVector3D(0, 0, 0)));
    }

    private void initParams() {
        mMaxNumVerticesPerTileNET.setUInt(mParams.uMaxNumVerticesPerTile);
        mMinPixelSizeForObjectVisibilityNET.setUInt(mParams.uMinPixelSizeForObjectVisibility);
        mMaxNumVisiblePointObjectsPerTileNET.setUInt(mParams.uMaxNumVisiblePointObjectsPerTile);
        mOptimizationMinScaleNET.setFloat(mParams.fOptimizationMinScale);
    }

    public float getMaxScale() {
        return mMaxScaleNET.getFloat();
    }

    public float getMinScale() {
        return mMinScaleNET.getFloat();
    }

    public int getMaxNumVerticesPerTile() {
        return mMaxNumVerticesPerTileNET.getUInt();
    }

    public int getMaxNumVisiblePointObjectsPerTile() {
        return mMaxNumVisiblePointObjectsPerTileNET.getUInt();
    }

    public int getMinPixelSizeForObjectVisibility() {
        return mMinPixelSizeForObjectVisibilityNET.getUInt();
    }

    public float getOptimizationMinScale() {
        return mOptimizationMinScaleNET.getFloat();
    }

    public String getLocaleStr()
    {
        return mLocaleStrET.getText().toString();
    }
    public SMcBox getWorldLimit()
    {
        return mWorldLimit.getWorldBoundingBox();
    }
    public String getPointTextureFile()
    {
        return mPointTextureFile.getDirPath();
    }

    private void inflateViews() {
        mWorldLimit = (WorldBoundingBox) mRootView.findViewById(R.id.raw_vector_layer_world_limit);
        mMaxScaleNET = (NumericEditTextLabel) mRootView.findViewById(R.id.raw_vector_layer_max_scale_net);
        mMinScaleNET = (NumericEditTextLabel) mRootView.findViewById(R.id.raw_vector_layer_min_scale_net);
        mLocaleStrET = (EditText) mRootView.findViewById(R.id.raw_vector_layer_locale_str_et);
        mPointTextureFile = (FileChooserEditTextLabel) mRootView.findViewById(R.id.fel_raw_vector_layer_point_texture_file_name);
        mPointTextureFile.setEnableFileChoosing(true);
        mMaxNumVerticesPerTileNET =  (NumericEditTextLabel) mRootView.findViewById(R.id.raw_vector_layer_max_num_vertices_per_tile_net);
        mMaxNumVisiblePointObjectsPerTileNET =  (NumericEditTextLabel) mRootView.findViewById(R.id.raw_vector_layer_max_num_visible_point_objects_per_tile_net);
        mMinPixelSizeForObjectVisibilityNET =  (NumericEditTextLabel) mRootView.findViewById(R.id.raw_vector_layer_min_pixel_size_for_object_visibility_net);
        mOptimizationMinScaleNET =  (NumericEditTextLabel) mRootView.findViewById(R.id.raw_vector_layer_optimization_min_scale_net);
    }

    // TODO: Rename method, update argument and hook method into UI event
    public void onButtonPressed(Uri uri) {
        if (mListener != null) {
            mListener.onFragmentInteraction(uri);
        }
    }

    @Override
    public void onAttach(Context context) {
        super.onAttach(context);
       /* if (context instanceof OnFragmentInteractionListener) {
            mListener = (OnFragmentInteractionListener) context;
        } else {
            throw new RuntimeException(context.toString()
                    + " must implement OnFragmentInteractionListener");
        }*/
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    @Override
    public void onDestroyView() {
        super.onDestroyView();

        // Remove child fragments using getChildFragmentManager()
        Fragment f1 = getChildFragmentManager().findFragmentById(R.id.raw_vector_layer_source_grid_coordinate_sys_fragment);
        if (f1 != null) {
            getChildFragmentManager().beginTransaction().remove(f1).commitAllowingStateLoss();
        }

        Fragment f2 = getChildFragmentManager().findFragmentById(R.id.raw_vector_layer_viewport_grid_coordinate_sys_fragment);
        if (f2 != null) {
            getChildFragmentManager().beginTransaction().remove(f2).commitAllowingStateLoss();
        }
    }

    public IMcGridCoordinateSystem getSourceGridCoordinateSystem() {
        GridCoordinateSysFragment gcsFragment = (GridCoordinateSysFragment)getChildFragmentManager().findFragmentById(R.id.raw_vector_layer_source_grid_coordinate_sys_fragment);
        IMcGridCoordinateSystem gridCoordinateSystem = null;
        if(gcsFragment != null)
            gridCoordinateSystem = gcsFragment.getSelectedGridCoordinateSystem();
        return gridCoordinateSystem;
    }

    public IMcGridCoordinateSystem getViewportGridCoordinateSystem() {
        GridCoordinateSysFragment gcsFragment = (GridCoordinateSysFragment)getChildFragmentManager().findFragmentById(R.id.raw_vector_layer_viewport_grid_coordinate_sys_fragment);
        IMcGridCoordinateSystem gridCoordinateSystem = null;
        if(gcsFragment != null)
            gridCoordinateSystem = gcsFragment.getSelectedGridCoordinateSystem();
        return gridCoordinateSystem;
    }

    @Override
    public void onCoordSysCreated(IMcGridCoordinateSystem mNewGridCoordinateSystem) {

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
