package com.elbit.mapcore.mcandroidtester.ui.map.fragments;

import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;

import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapTabsActivity;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.google.android.material.textfield.TextInputEditText;
import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.RadioButton;

import com.elbit.mapcore.Interfaces.Map.IMcMapCamera;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_AMCTMapForm;
import com.elbit.mapcore.mcandroidtester.model.AMCTViewPort;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link MapSettingsFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link MapSettingsFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class MapSettingsFragment extends Fragment {

    private OnFragmentInteractionListener mListener;
    private View mFragmentView;
    private RadioButton rbMapType2D;
    private RadioButton rbMapType3D;
    private RadioButton rbMapType2D3D;
    public MapSettingsFragment() {
        // Required empty public constructor
    }

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment MapSettingsFragment.
     */
    public static MapSettingsFragment newInstance() {
        MapSettingsFragment fragment = new MapSettingsFragment();
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
        mFragmentView = inflater.inflate(R.layout.fragment_map_settings, container, false);
        rbMapType2D = (RadioButton)mFragmentView.findViewById(R.id.sl_vp_2D);
        rbMapType3D = (RadioButton)mFragmentView.findViewById(R.id.sl_vp_3D);
        rbMapType2D3D = (RadioButton)mFragmentView.findViewById(R.id.sl_vp_2D_3D);
        rbMapType2D.setChecked(true);

        initOpenVpBttn();
        ((TextInputEditText) mFragmentView.findViewById(R.id.view_port_id_et)).setText(String.valueOf(Manager_AMCTMapForm.getInstance().getLastAddedViewportHashCode()));
        return mFragmentView;
    }

    private void initOpenVpBttn() {
        mFragmentView.findViewById(R.id.open_vp_bttn).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                boolean isOk = checkViewPortData();
                IMcMapCamera.EMapType mapType = IMcMapCamera.EMapType.EMT_2D;
                if (rbMapType3D.isChecked())
                    mapType = IMcMapCamera.EMapType.EMT_3D;
                AMCTViewPort.getViewportInCreation().setMapType(mapType);

                AMCTViewPort.ViewportSpace viewportSpace = AMCTViewPort.ViewportSpace.FullScreen;
                if (rbMapType2D3D.isChecked())
                    viewportSpace = AMCTViewPort.ViewportSpace.Other;
                AMCTViewPort.getViewportInCreation().setViewportSpace(viewportSpace);

                if (isOk) {
                    Intent viewPortIntent = new Intent(getActivity(), MapsContainerActivity.class);
                    viewPortIntent.putExtra("newViewPort", true);
                    startActivity(viewPortIntent);
                    Funcs.removeCallbacks(((MapTabsActivity)getActivity()).getHandler());
                }
            }
        });
    }

    private boolean checkViewPortData() {
        if (AMCTViewPort.getViewportInCreation().getGridCoordinateSystem() == null) {
            AlertMessages.ShowErrorMessage(getActivity(), "view port creation error", "No Grid Coordinate System was specified.\nYou have to choose one!\n");
            return false;
        }
        else
            return true;
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
