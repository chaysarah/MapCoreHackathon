package com.elbit.mapcore.mcandroidtester.ui.map.fragments;

import android.content.Context;
import android.content.SharedPreferences;
import android.net.Uri;
import android.os.Bundle;
import android.preference.PreferenceManager;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.CheckBox;
import android.widget.EditText;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.model.AMCTViewPort;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.ViewPortWithSeveralLayersActivity;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SpinnerWithLabel;

import com.elbit.mapcore.Interfaces.Map.IMcMapDevice;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link SeveralLayersSetingsFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link SeveralLayersSetingsFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class SeveralLayersSetingsFragment extends Fragment {
    public CheckBox mEnhanceBorderOverlapCb;
    public CheckBox mShowGeoInMetricProportionCb;
    public SpinnerWithLabel mAntiAliasingLevelSpinner;
    public SpinnerWithLabel mTerrainAntiAliasingLevelSpinner;
    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;

    private OnFragmentInteractionListener mListener;
    public EditText mNumBgThreadsET;
    public EditText mTerrainPrecisionFactorET;
    public EditText mMaxScaleET;
    private IMcMapViewport.SCreateData mSCreateData;
    private IMcMapDevice.SInitParams mSInitParams;
    public boolean isFirstVisible=true;


    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment SeveralLayersSetingsFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static SeveralLayersSetingsFragment newInstance(String param1, String param2) {
        SeveralLayersSetingsFragment fragment = new SeveralLayersSetingsFragment();
        Bundle args = new Bundle();
        args.putString(ARG_PARAM1, param1);
        args.putString(ARG_PARAM2, param2);
        fragment.setArguments(args);
        return fragment;
    }

    public SeveralLayersSetingsFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if (getArguments() != null) {
            mParam1 = getArguments().getString(ARG_PARAM1);
            mParam2 = getArguments().getString(ARG_PARAM2);
        }
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_several_layers_setings, container, false);
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
        ((ViewPortWithSeveralLayersActivity) context).mSettingsFragment = SeveralLayersSetingsFragment.this;

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

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        /*boolean isSpExist = checkForSp();*/
        initSpinners();
        initCheckboxes();
        initEditTxts();
    }

    private boolean checkForSp() {
        SharedPreferences preferences = PreferenceManager.getDefaultSharedPreferences(getActivity());
        return preferences.getBoolean("toRestore", false);
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

    private void initCheckboxes() {
        mEnhanceBorderOverlapCb = (CheckBox) getView().findViewById(R.id.sl_enhance_border_overlap_cb);
        mShowGeoInMetricProportionCb = (CheckBox) getView().findViewById(R.id.sl_show_geo_in_metric_proportion_cb);
        //set def values


    }

    public void initSpinners() {
        mAntiAliasingLevelSpinner = (SpinnerWithLabel) getView().findViewById(R.id.sl_viewport_anti_aliasing_level);
        mAntiAliasingLevelSpinner.setAdapter(new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, IMcMapDevice.EAntiAliasingLevel.values()));

        mTerrainAntiAliasingLevelSpinner = (SpinnerWithLabel) getView().findViewById(R.id.sl_terrain_object_anti_aliasing_level);
        mTerrainAntiAliasingLevelSpinner.setAdapter(new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, IMcMapDevice.EAntiAliasingLevel.values()));
    }

    @Override
    public void setUserVisibleHint(boolean visible) {
        super.setUserVisibleHint(visible);
        if (visible && isResumed()) {
            //Only manually call onResume if fragment is already visible
            //Otherwise allow natural fragment lifecycle to call onResume
            onResume();
        }
    }

    public void initDefaultDataFromMC() {
        mSCreateData = new IMcMapViewport.SCreateData(AMCTViewPort.getViewportInCreation().getMapType());
        mSInitParams= new IMcMapDevice.SInitParams();
    }

    @Override
    public void onResume() {
        super.onResume();
        if (!getUserVisibleHint()) {
            return;
        }
        else
        //if it's first time the fragment is visible
        if(getUserVisibleHint()&&isFirstVisible==true) {

            isFirstVisible = false;
            initDefaultDataFromMC();
        }
        Log.d("AMCTester","on resume");
        initFieldsWithDefaultData();
    }

    public void initFieldsWithDefaultData() {
        mNumBgThreadsET.setText(String.valueOf(mSInitParams.uNumBackgroundThreads));
        mTerrainPrecisionFactorET.setText(String.valueOf(mSCreateData.fTerrainResolutionFactor));
        mAntiAliasingLevelSpinner.setSelection(mSInitParams.eViewportAntiAliasingLevel.getValue());
        mTerrainAntiAliasingLevelSpinner.setSelection(mSInitParams.eTerrainObjectsAntiAliasingLevel.getValue());
        ((CheckBox) getView().findViewById(R.id.sl_show_geo_in_metric_proportion_cb)).setChecked(mSCreateData.bShowGeoInMetricProportion);

    }

    private void initEditTxts() {
        mNumBgThreadsET = (EditText) getView().findViewById(R.id.sl_number_background_threads_et);
        mTerrainPrecisionFactorET = (EditText) getView().findViewById(R.id.sl_terrain_precision_factor_et);
        mMaxScaleET = (EditText) getView().findViewById(R.id.sl_max_scal_et);

        // }

    }
}
