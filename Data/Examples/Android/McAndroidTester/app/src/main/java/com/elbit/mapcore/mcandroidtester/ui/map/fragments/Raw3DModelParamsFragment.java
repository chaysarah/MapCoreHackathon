package com.elbit.mapcore.mcandroidtester.ui.map.fragments;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.CheckBox;

import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;

import com.elbit.mapcore.Interfaces.Calculations.GridCoordSystem.IMcGridCoordinateSystem;
import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcMapProduction;
import com.elbit.mapcore.Interfaces.Map.IMcRaw3DModelMapLayer;
import com.elbit.mapcore.Structs.SMcBox;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.RawStaticObjectsParamsDetails;
import com.elbit.mapcore.mcandroidtester.utils.customviews.Raw3DModelParamsDetails;

public class Raw3DModelParamsFragment extends Fragment {

    private View mFragmentView;
    private OnFragmentInteractionListener mListener;

    private boolean mTinyVisibility = false;

    private CheckBox mOrthometicHeight;

    private Raw3DModelParamsDetails mRaw3DModelParamsDetails;
    private RawStaticObjectsParamsDetails mRawStaticObjectsParamsDetails;

    public Raw3DModelParamsFragment() {
        // Required empty public constructor
    }

    public static Raw3DModelParamsFragment newInstance() {
        Raw3DModelParamsFragment fragment = new Raw3DModelParamsFragment();
        return fragment;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
    }

    @Override
    public void onStart() {
        super.onStart();

    }

    @Override
    public void onResume() {
        super.onResume();
        if (!getUserVisibleHint())
        {
            return;
        }
        //mGridCoordinateSystemDetails.initComponents();
        //initRadioBttns();
    }

    @Override
    public void setUserVisibleHint(boolean visible)
    {
        super.setUserVisibleHint(visible);
        if (visible && isResumed())
        {
            //Only manually call onResume if fragment is already visible
            //Otherwise allow natural fragment lifecycle to call onResume
            onResume();
        }
    }
    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);

        //outState.putString(SELECTED_RADIO_BUTTON_OPTION, mRadioButtonOptions.toString());
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        mFragmentView = inflater.inflate(R.layout.fragment_raw_3d_model_params, container, false);
        mOrthometicHeight = (CheckBox)  mFragmentView.findViewById(R.id.raw_3d_model_orthometric_heights_cb);
        mOrthometicHeight.setChecked(new IMcMapProduction.S3DModelConvertParams().bOrthometricHeights);

        mRaw3DModelParamsDetails = (Raw3DModelParamsDetails) mFragmentView.findViewById(R.id.raw_3d_model_params);
        if(mTinyVisibility)
            mRaw3DModelParamsDetails.setTinyVisibility();
        mRawStaticObjectsParamsDetails = (RawStaticObjectsParamsDetails) mFragmentView.findViewById(R.id.raw_3d_model_static_objects_params);
        return mFragmentView;
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
        if (context instanceof Raw3DModelParamsFragment.OnFragmentInteractionListener) {
            mListener = (Raw3DModelParamsFragment.OnFragmentInteractionListener) context;
        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    /*@Override
    public void onSRaw3DModelParamsCreated(IMcRaw3DModelMapLayer.SParams params) {
        Fragment parentFragment = getParentFragment();
        if(parentFragment instanceof OnCreateSRaw3DModelParamsListener) {
            OnCreateSRaw3DModelParamsListener onCreateCoordSysListenerInHostedFrag = ((OnCreateSRaw3DModelParamsListener) parentFragment);
            if (onCreateCoordSysListenerInHostedFrag != null)
                onCreateCoordSysListenerInHostedFrag.onSRaw3DModelParamsCreated(params);
        }
    }

    @Override
    public void setObject(Object obj) {
    }
*/

    public boolean IsUseBuiltIndexingDataDir() {
        return mRawStaticObjectsParamsDetails.IsUseBuiltIndexingDataDir();
    }

    public boolean IsNonDefaultIndexDir() {
        return mRawStaticObjectsParamsDetails.IsNonDefaultIndexDir();
    }

    public String getNonDefaultIndexDir(){
        return mRawStaticObjectsParamsDetails.getNonDefaultIndexDir();
    }

    public boolean getOrthometicHeight()
    {
        return ((CheckBox) mFragmentView.findViewById(R.id.raw_3d_model_orthometric_heights_cb)).isChecked();
    }

    public float getTargetHighestResolution()
    {
        return mRaw3DModelParamsDetails.getTargetHighestResolution();
    }

    public IMcGridCoordinateSystem getGridCoordinateSystem()
    {
        return mRaw3DModelParamsDetails.getGridCoordinateSystem();
    }

    public SMcBox getClipRect()
    {
        return mRaw3DModelParamsDetails.getClipRect();
    }

    public IMcMapLayer.STilingScheme getTilingScheme()
    {
        return mRaw3DModelParamsDetails.getTilingScheme();
    }

    public void setTinyVisibility() {
        mTinyVisibility = true;
    }

    public interface OnFragmentInteractionListener {
        // TODO: Update argument type and name
        void onFragmentInteraction(Uri uri);
    }
}
