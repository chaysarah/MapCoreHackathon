package com.elbit.mapcore.mcandroidtester.ui.map.fragments;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.annotation.Nullable;
import com.google.android.material.tabs.TabLayout;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.LinearLayout;
import android.widget.RadioButton;
import android.widget.RadioGroup;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.OnCreateCoordinateSystemListener;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_MCGridCoordinateSystem;
import com.elbit.mapcore.mcandroidtester.model.AMCTMapTerrain;
import com.elbit.mapcore.mcandroidtester.model.AMCTViewPort;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.GridCoordinateSystemDetails;
import com.elbit.mapcore.mcandroidtester.utils.customviews.GridCoordinateSystemList;


import com.elbit.mapcore.Interfaces.Calculations.GridCoordSystem.IMcGridCoordinateSystem;


public class GridCoordinateSysFragment extends Fragment implements OnCreateCoordinateSystemListener {

    public static final String SELECTED_RADIO_BUTTON_OPTION = "SELECTED_RADIO_BUTTON_OPTION";

    public RadioButtonOptions getRadioButtonOptions() {
        return mRadioButtonOptions;
    }

    public enum RadioButtonOptions{CreateNew,SelectFromList};
    private int mCoordSysType;
    private View mFragmentView;
    private GridCoordinateSystemList mGridCoordinateSystemList;
    private GridCoordinateSystemDetails mGridCoordinateSystemDetails;
    private OnFragmentInteractionListener mListener;
    private IMcGridCoordinateSystem mSelectedGridCoordinateSystem;
    private RadioButtonOptions mRadioButtonOptions;
    private RadioGroup mGCSRadioGroup;

    public GridCoordinateSysFragment() {
        // Required empty public constructor
    }

    public static GridCoordinateSysFragment newInstance(int coordinateSysType) {
        GridCoordinateSysFragment fragment = new GridCoordinateSysFragment();
        Bundle args = new Bundle();
        args.putInt(Consts.COORD_SYS_TYPE, coordinateSysType);
        fragment.setArguments(args);
        return fragment;
    }

    public IMcGridCoordinateSystem getSelectedGridCoordinateSystem() {
        return mSelectedGridCoordinateSystem;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        if (getArguments() != null) {
            mCoordSysType = getArguments().getInt(Consts.COORD_SYS_TYPE);
        }
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
        mGridCoordinateSystemDetails.initComponents();
        initRadioBttns();
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


    private void initRadioBttns() {

        mGCSRadioGroup.setOnCheckedChangeListener(new RadioGroup.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(RadioGroup group, int checkedId) {
                int id = checkedId;
                if (id == R.id.select_from_list_rb) {
                    openSelectCoordSysFromListOption();
                }
                else if (id == R.id.create_new_rb) {
                    openCreateNewCoordSysOption();
                }
            }
        });
        boolean isCreateNew = (Manager_MCGridCoordinateSystem.getInstance().getdGridCoordSys().size() == 0 || mRadioButtonOptions == RadioButtonOptions.CreateNew);
        SetRadioGroupOptions(!isCreateNew);
    }

    private void SetRadioGroupOptions(boolean isSelectFromList)
    {
        if(isSelectFromList)
            mGCSRadioGroup.check(R.id.select_from_list_rb);
        else
            mGCSRadioGroup.check(R.id.create_new_rb);
    }

    private void openCreateNewCoordSysOption() {
        final LinearLayout selectFromListLL = (LinearLayout) mFragmentView.findViewById(R.id.select_from_list_ll);
        final LinearLayout createNewLL = (LinearLayout) mFragmentView.findViewById(R.id.create_new_ll);
        mGridCoordinateSystemDetails.initSpinners();
        selectFromListLL.setVisibility(View.GONE);
        createNewLL.setVisibility(View.VISIBLE);
        mRadioButtonOptions = RadioButtonOptions.CreateNew;
        mFragmentView.findViewById(R.id.create_coord_sys_bttn).setVisibility(View.VISIBLE);
        ((RadioButton)mFragmentView.findViewById(R.id.create_new_rb)).setChecked(true);
        Funcs.enableDisableViewGroup((LinearLayout) mFragmentView.findViewById(R.id.create_new_ll), true);
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);

        outState.putString(SELECTED_RADIO_BUTTON_OPTION, mRadioButtonOptions.toString());
    }

    private void openSelectCoordSysFromListOption() {
        final LinearLayout selectFromListLL = (LinearLayout) mFragmentView.findViewById(R.id.select_from_list_ll);
        final LinearLayout createNewLL = (LinearLayout) mFragmentView.findViewById(R.id.create_new_ll);
        selectFromListLL.setVisibility(View.VISIBLE);
        createNewLL.setVisibility(View.GONE);
        mRadioButtonOptions = RadioButtonOptions.SelectFromList;
        ((RadioButton)mFragmentView.findViewById(R.id.select_from_list_rb)).setChecked(true);
        mGridCoordinateSystemList.LoadGridCoordinateSystemList();
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        mFragmentView = inflater.inflate(R.layout.fragment_grid_coordinate_sys, container, false);
        if (savedInstanceState != null) {
            String strEnum = savedInstanceState.getString(SELECTED_RADIO_BUTTON_OPTION);
            if (strEnum != "")
                mRadioButtonOptions = RadioButtonOptions.valueOf(strEnum);
        }
        mGridCoordinateSystemDetails = (GridCoordinateSystemDetails) mFragmentView.findViewById(R.id.grid_coordinate_sys_details);
        mGridCoordinateSystemDetails.SetFragment(this);
        mGridCoordinateSystemList = (GridCoordinateSystemList) mFragmentView.findViewById(R.id.gcs_lv);
        mGridCoordinateSystemList.SetFragment(this);
        mGCSRadioGroup = ((RadioGroup) mFragmentView.findViewById(R.id.gcs_options_rg));
        initRadioBttns();
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
        if (context instanceof OnFragmentInteractionListener) {
            mListener = (OnFragmentInteractionListener) context;
        }
        /*else {
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
    public void onCoordSysCreated(IMcGridCoordinateSystem mNewGridCoordinateSystem) {
        //coord sys in nested fragment
        mSelectedGridCoordinateSystem = mNewGridCoordinateSystem;
        Fragment parentFragment = getParentFragment();
        if(parentFragment instanceof OnCreateCoordinateSystemListener) {
            OnCreateCoordinateSystemListener onCreateCoordSysListenerInHostedFrag = ((OnCreateCoordinateSystemListener) parentFragment);
            if (onCreateCoordSysListenerInHostedFrag != null)
                onCreateCoordSysListenerInHostedFrag.onCoordSysCreated(mNewGridCoordinateSystem);
        }
        else if(getActivity() instanceof OnCreateCoordinateSystemListener)
        {
            ((OnCreateCoordinateSystemListener)getActivity()).onCoordSysCreated(mNewGridCoordinateSystem);
        }
        else {
            switch (mCoordSysType) {
                case Consts.VIEWPORT_COORDINATE_SYS: {
                    ((TabLayout) getActivity().findViewById(R.id.tabs)).getTabAt(Consts.TERRAINS_TAB_INDEX).select();
                    AMCTViewPort.getViewportInCreation().setGridCoordinateSystem(mNewGridCoordinateSystem);
                    break;
                }
                case Consts.TERRAIN_COORDINATE_SYS: {
                    AMCTMapTerrain.getInstance().setmGridCoordinateSystem(mNewGridCoordinateSystem);
                    ((TabLayout) getActivity().findViewById(R.id.terrains_tabs)).getTabAt(Consts.LAYERS_TAB_INDEX).select();

                    break;
                }
            }
        }
        openSelectCoordSysFromListOption();
        mGridCoordinateSystemList.selectCurrGridCoordSys(mNewGridCoordinateSystem);
    }

    public interface OnFragmentInteractionListener {
        // TODO: Update argument type and name
        void onFragmentInteraction(Uri uri);

    }
}
