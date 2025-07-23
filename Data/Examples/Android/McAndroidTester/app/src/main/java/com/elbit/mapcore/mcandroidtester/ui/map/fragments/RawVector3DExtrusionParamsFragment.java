package com.elbit.mapcore.mcandroidtester.ui.map.fragments;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.elbit.mapcore.Interfaces.Map.IMcRawVector3DExtrusionMapLayer;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.utils.customviews.RawStaticObjectsParamsDetails;
import com.elbit.mapcore.mcandroidtester.utils.customviews.RawVector3DExtrusionParamsDetails;

public class RawVector3DExtrusionParamsFragment extends Fragment {

    private View mFragmentView;
    private OnFragmentInteractionListener mListener;

    private RawVector3DExtrusionParamsDetails mRawVector3DExtrusionParamsDetails;
    private RawStaticObjectsParamsDetails mRawStaticObjectsParamsDetails;

    public RawVector3DExtrusionParamsFragment() {
        // Required empty public constructor
    }

    public static RawVector3DExtrusionParamsFragment newInstance() {
        RawVector3DExtrusionParamsFragment fragment = new RawVector3DExtrusionParamsFragment();
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
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        mFragmentView = inflater.inflate(R.layout.fragment_raw_vector_3d_extrusion_params, container, false);

        mRawVector3DExtrusionParamsDetails = (RawVector3DExtrusionParamsDetails) mFragmentView.findViewById(R.id.raw_vector_3d_extrusion_params);
        mRawStaticObjectsParamsDetails = (RawStaticObjectsParamsDetails) mFragmentView.findViewById(R.id.raw_static_objects_params);

        mRawStaticObjectsParamsDetails.IsUseBuiltIndexingDataDir(true);
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
        if (context instanceof RawVector3DExtrusionParamsFragment.OnFragmentInteractionListener) {
            mListener = (RawVector3DExtrusionParamsFragment.OnFragmentInteractionListener) context;
        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    /*@Override
    public void onSRawVector3DExtrusionParamsCreated(IMcRawVector3DExtrusionMapLayer.SParams params) {
        Fragment parentFragment = getParentFragment();
        if(parentFragment instanceof OnCreateSRawVector3DExtrusionParamsListener) {
            OnCreateSRawVector3DExtrusionParamsListener onCreateCoordSysListenerInHostedFrag = ((OnCreateSRawVector3DExtrusionParamsListener) parentFragment);
            if (onCreateCoordSysListenerInHostedFrag != null)
                onCreateCoordSysListenerInHostedFrag.onSRawVector3DExtrusionParamsCreated(params);
        }
    }

    @Override
    public void setObject(Object obj) {
    }
*/

    public IMcRawVector3DExtrusionMapLayer.SParams getParams() {
        return mRawVector3DExtrusionParamsDetails.getParamsToFragment();
    }

    public IMcRawVector3DExtrusionMapLayer.SGraphicalParams getGraphicalParams() {
        return mRawVector3DExtrusionParamsDetails.getGraphicalParamsToFragment();
    }

    public boolean IsUseBuiltIndexingDataDir() {
        return mRawStaticObjectsParamsDetails.IsUseBuiltIndexingDataDir();
    }

    public boolean IsNonDefaultIndexDir() {
        return mRawStaticObjectsParamsDetails.IsNonDefaultIndexDir();
    }

    public String getNonDefaultIndexDir(){
        return mRawStaticObjectsParamsDetails.getNonDefaultIndexDir();
    }

    public interface OnFragmentInteractionListener {
        // TODO: Update argument type and name
        void onFragmentInteraction(Uri uri);
    }
}
