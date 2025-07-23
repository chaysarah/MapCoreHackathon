package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlay_manager_treeview;

import android.app.Dialog;
import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import android.os.Handler;

import androidx.annotation.NonNull;
import androidx.fragment.app.DialogFragment;
import androidx.fragment.app.Fragment;
import androidx.appcompat.app.AlertDialog;

import android.view.View;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.managers.ObjectWorld.Manager_MCOverlayManager;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.GridCoordinateSysFragment;

import com.elbit.mapcore.Interfaces.Calculations.GridCoordSystem.IMcGridCoordinateSystem;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link NewOverlayManagerFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link NewOverlayManagerFragment#newInstance} factory method to
 * create an instance of this fragment.
 *
 */
public class NewOverlayManagerFragment extends DialogFragment {

    private OnFragmentInteractionListener mListener;
    private Handler mHandler;
    private static final int CLOSE_DIALOG = 1;
    private View mDialogView;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     ** @return A new instance of fragment NewOverlayManagerFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static NewOverlayManagerFragment newInstance() {
        NewOverlayManagerFragment fragment = new NewOverlayManagerFragment();
        return fragment;
    }

    public NewOverlayManagerFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
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
            //throw new RuntimeException(context.toString()
            //      + " must implement OnFragmentInteractionListener");
        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    @Override
    public void onDestroyView() {
        super.onDestroyView();
        Fragment f = getFragmentManager().findFragmentById(R.id.create_new_om_grid_coordinate_sys_fragment);
        if (f != null)
            getFragmentManager().beginTransaction().remove(f).commit();
    }

    @NonNull
    @Override
    public Dialog onCreateDialog(Bundle savedInstanceState) {

        mDialogView = getActivity().getLayoutInflater().inflate(R.layout.fragment_new_overlay_manager, null);

        AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());
        mDialogView.findViewById(R.id.btn_new_overlay_manager_ok).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                GridCoordinateSysFragment gcsFragment = (GridCoordinateSysFragment)getFragmentManager().findFragmentById(R.id.create_new_om_grid_coordinate_sys_fragment);
                final IMcGridCoordinateSystem gridCoordinateSystem = gcsFragment.getSelectedGridCoordinateSystem();
                if(gridCoordinateSystem != null)
                {
                    Funcs.runMapCoreFunc(new Runnable() {
                        @Override
                        public void run() {
                            Manager_MCOverlayManager.getInstance().CreateOverlayManager(gridCoordinateSystem , false);

                            getActivity().runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    IOverlayManagerCreatedCallback callback = (IOverlayManagerCreatedCallback) getTargetFragment();
                                    callback.OnOverlayManagerCreated();

                                    dismiss();
                                }
                            });
                        }
                    });

                }
            }
        });
        builder.setView(mDialogView);
        return builder.create();
    }

    /**
     * This interface must be implemented by activities that contain this
     * fragment to allow an interaction in this fragment to be communicated
     * to the activity and potentially other fragments contained in that
     * activity.
     * <p>
     * See the Android Training lesson <a href=
     * "http://developer.android.com/training/basics/fragments/communicating.html"
     * >Communicating with Other Fragments</a> for more information.
     */
    public interface OnFragmentInteractionListener {
        // TODO: Update argument type and name
        void onFragmentInteraction(Uri uri);
    }
}
