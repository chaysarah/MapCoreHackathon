package com.elbit.mapcore.mcandroidtester.ui.map.fragments;

import android.content.Context;
import android.graphics.Color;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentTransaction;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.CheckBox;
import android.widget.EditText;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.OnCreateCoordinateSystemListener;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SelectColor;
import com.elbit.mapcore.mcandroidtester.utils.customviews.WorldBoundingBox;

import com.elbit.mapcore.Interfaces.Calculations.GridCoordSystem.IMcGridCoordinateSystem;
import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;
import com.elbit.mapcore.Structs.SMcBox;
import com.elbit.mapcore.Structs.SMcVector3D;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link RawLayersParamsFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link RawLayersParamsFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class RawLayersParamsFragment extends Fragment implements OnCreateCoordinateSystemListener {

    private OnFragmentInteractionListener mListener;
    private View mRootView;
    private WorldBoundingBox mMaxWorldLimit;
    private NumericEditTextLabel mMaxNumOpenFilesNET;
    private SelectColor mTransparentColor;
    private IMcGridCoordinateSystem mNewGridCoordinateSystem;
    private NumericEditTextLabel mMaxScale;
    private NumericEditTextLabel mHeighestResolution;
    private CheckBox mResolveOverlapConflicts;
    private CheckBox mEnhanceBorderOverlap;
    private CheckBox mFillEmptyTilesByLowerResolutionTiles;
    private NumericEditTextLabel mTransparentColorPrecisionNET;
    private NumericEditTextLabel mFirstPyramidResolutionNET;
    private EditText mPyramidResolutions;
    private CheckBox mImageCoordSys;
    private CheckBox mIgnoreRasterPalette;
    private RawLayerComponentsFragment mRawLayerComponentsFragment;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment RawLayersParamsFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static RawLayersParamsFragment newInstance() {
        RawLayersParamsFragment fragment = new RawLayersParamsFragment();
        return fragment;
    }

    public RawLayersParamsFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        mRootView = inflater.inflate(R.layout.fragment_raw_layers_params, container, false);
        initViews();
        return mRootView;
    }

    private void initViews() {
        inflateViews();
        initWorldLimit();
        initMaxNumOpenFiles();
        initTransparentColor();
        addCoordSysFragment();
        addComponenetFragment();
    }

    private void addComponenetFragment() {
        FragmentTransaction transaction;
        mRawLayerComponentsFragment = (RawLayerComponentsFragment) getActivity().getSupportFragmentManager().findFragmentByTag("rawLayerComponentsFragment");
        if (mRawLayerComponentsFragment == null) {
            mRawLayerComponentsFragment = mRawLayerComponentsFragment.newInstance();
            transaction = getActivity().getSupportFragmentManager().beginTransaction().add(R.id.raw_raster_componenet_container, mRawLayerComponentsFragment, "rawLayerComponentsFragment");
        } else
            transaction = getActivity().getSupportFragmentManager().beginTransaction().show(mRawLayerComponentsFragment);

        transaction.addToBackStack("rawLayerComponentsFragment");
        transaction.commit();
    }


    private void addCoordSysFragment() {
        GridCoordinateSysFragment gridCoordinateSysFragment = new GridCoordinateSysFragment();
        FragmentTransaction transaction = getChildFragmentManager().beginTransaction();
        transaction.addToBackStack("gridCoordinateSysFragment");
        transaction.add(R.id.rae_layers_grid_coord_sys_container_fragment, gridCoordinateSysFragment, "gridCoordinateSysFragment").commit();
    }

    public IMcMapLayer.SNonNativeParams getNonNativeParams(IMcMapLayer.SRawParams sRawParams) {
        sRawParams.pCoordinateSystem = mNewGridCoordinateSystem;
        sRawParams.fMaxScale = mMaxScale.getFloat();
        sRawParams.bResolveOverlapConflicts = mResolveOverlapConflicts.isChecked();
        sRawParams.bEnhanceBorderOverlap = mEnhanceBorderOverlap.isChecked();
        sRawParams.bFillEmptyTilesByLowerResolutionTiles = mFillEmptyTilesByLowerResolutionTiles.isChecked();
        sRawParams.TransparentColor = mTransparentColor.getmBColor();
        sRawParams.byTransparentColorPrecision = mTransparentColorPrecisionNET.getByte();
        sRawParams.fHighestResolution = mHeighestResolution.getFloat();
        sRawParams.MaxWorldLimit = mMaxWorldLimit.getWorldBoundingBox();
        return sRawParams;
    }

    private void initTransparentColor() {
        mTransparentColor.setmBColor(Color.BLACK);
        mTransparentColor.setAlpha(0);
        mTransparentColor.enableButtons(true);
    }

    private void initMaxNumOpenFiles() {
        mMaxNumOpenFilesNET.setFloat(5000);
    }

    private void initWorldLimit() {
        mMaxWorldLimit.initWorldBoundingBox(new SMcBox(new SMcVector3D(0, 0, 0), new SMcVector3D(0, 0, 0)));
    }

    public boolean getIsImageCoordSys() {
        return mImageCoordSys.isChecked();
    }

    public IMcMapLayer.SRawParams getParams()
    {
        IMcMapLayer.SRawParams dnParams = new IMcMapLayer.SRawParams();
        dnParams = (IMcMapLayer.SRawParams) getNonNativeParams(dnParams);
        IMcMapLayer.SComponentParams[] sComponentParamsArr = new IMcMapLayer.SComponentParams[mRawLayerComponentsFragment.getmCompParamsList().size()];
        dnParams.aComponents = mRawLayerComponentsFragment.getmCompParamsList().toArray(sComponentParamsArr);
        dnParams.uMaxNumOpenFiles = mMaxNumOpenFilesNET.getUInt();
        dnParams.fFirstPyramidResolution = mFirstPyramidResolutionNET.getFloat();
        dnParams.auPyramidResolutions = getPyramidResolutionArray();
        dnParams.bIgnoreRasterPalette = mIgnoreRasterPalette.isChecked();
        return dnParams;
    }

    public int[] getPyramidResolutionArray() {
        int[] pyramidArr = createPyramidResolutionArray();
        if (pyramidArr == null && !mPyramidResolutions.getText().toString().isEmpty()) {
            AlertMessages.ShowGenericMessage(getContext(), "raw later error", "Invalid Pyramid Resolution Values");
        }
        return pyramidArr;
    }

    private int[] createPyramidResolutionArray() {
        int[] pyramidArr = null;

        if (!mPyramidResolutions.getText().toString().isEmpty()) {
            String[] ids = (String.valueOf(mPyramidResolutions.getText())).trim().split(",");

            if (ids.length > 0) {
                pyramidArr = new int[ids.length];
                int i = 0;
                for (String strID : ids) {
                    int result = 0;
                    int parseInt = -1;
                    parseInt = Integer.valueOf(strID);
                    if (parseInt>=0)
                        pyramidArr[i] = result;
                    else {
                        mPyramidResolutions.selectAll();
                        pyramidArr = null;
                        break;
                    }
                    ++i;
                }
            }
        }

        return pyramidArr;
    }

    private void inflateViews() {

        mMaxWorldLimit = (WorldBoundingBox) mRootView.findViewById(R.id.raw_layers_max_world_limit);
        mMaxScale = (NumericEditTextLabel) mRootView.findViewById(R.id.raw_layers_max_scale_net);
        mHeighestResolution = (NumericEditTextLabel) mRootView.findViewById(R.id.raw_layers_highest_resolution_net);
        mResolveOverlapConflicts = (CheckBox) mRootView.findViewById(R.id.raw_layers_resolve_overlap_conflicts_cb);
        mEnhanceBorderOverlap = (CheckBox) mRootView.findViewById(R.id.raw_layers_enhance_border_overlap_cb);
        mMaxNumOpenFilesNET = (NumericEditTextLabel) mRootView.findViewById(R.id.raw_layers_max_num_open_files_net);
        mTransparentColor = (SelectColor) mRootView.findViewById(R.id.raw_layers_transparent_color_sc);
        mTransparentColorPrecisionNET = (NumericEditTextLabel) mRootView.findViewById(R.id.raw_layers_transparent_color_precision_net);
        mFirstPyramidResolutionNET = (NumericEditTextLabel) mRootView.findViewById(R.id.raw_layers_first_pyramid_resolution_net);
        mPyramidResolutions = (EditText) mRootView.findViewById(R.id.raw_layers_pyramid_resolution_et);
        mImageCoordSys = (CheckBox) mRootView.findViewById(R.id.raw_layers_image_coord_sys_cb);
        mFillEmptyTilesByLowerResolutionTiles = (CheckBox) mRootView.findViewById(R.id.raw_layers_fill_empty_tiles_by_lower_resolution_tiles_cb);
        mIgnoreRasterPalette = (CheckBox) mRootView.findViewById(R.id.raw_layers_ignore_raster_palette_cb);
    }

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

    public IMcGridCoordinateSystem getGridCoordinateSystem() {
        return mNewGridCoordinateSystem;
    }

    @Override
    public void onCoordSysCreated(IMcGridCoordinateSystem newGridCoordinateSystem) {
        mNewGridCoordinateSystem = newGridCoordinateSystem;
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
