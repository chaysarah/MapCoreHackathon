package com.elbit.mapcore.mcandroidtester.ui.map.fragments;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.DialogFragment;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.LinearLayout;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.customviews.GridCoordinateSystemDetails;

import com.elbit.mapcore.Interfaces.Calculations.GridCoordSystem.IMcGridCoordinateSystem;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link GridCoordinateSystemDetailsFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link GridCoordinateSystemDetailsFragment#newInstance} factory method to
 * create an instance of this fragment.
 *
 */
public class GridCoordinateSystemDetailsFragment extends DialogFragment implements FragmentWithObject {

    View mView;
    IMcGridCoordinateSystem mCurrGridCoordSystem;
    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;

    private OnFragmentInteractionListener mListener;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment GridCoordinateSystemDetailsFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static GridCoordinateSystemDetailsFragment newInstance(String param1, String param2) {
        GridCoordinateSystemDetailsFragment fragment = new GridCoordinateSystemDetailsFragment();
        Bundle args = new Bundle();
        args.putString(ARG_PARAM1, param1);
        args.putString(ARG_PARAM2, param2);
        fragment.setArguments(args);
        return fragment;
    }
    public GridCoordinateSystemDetailsFragment() {
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
        mView = inflater.inflate(R.layout.fragment_grid_coordinate_system_details, container, false);

        return mView;
    }

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);

        if(savedInstanceState != null)
        {
            AMCTSerializableObject mcObject = (AMCTSerializableObject)savedInstanceState.getSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT);
            if(mcObject != null)
                setObject(mcObject.getMcObject());
        }
        final LinearLayout createNewLL = (LinearLayout) mView.findViewById(R.id.create_new_ll);
        createNewLL.setVisibility(View.VISIBLE);
        if(mCurrGridCoordSystem != null) {
            GridCoordinateSystemDetails cvGridCoordinateSystemDetails = (GridCoordinateSystemDetails) mView.findViewById(R.id.cv_grid_coordinate_system_details);
            cvGridCoordinateSystemDetails.ShowCurrGridCoordSysParams(mCurrGridCoordSystem);
            cvGridCoordinateSystemDetails.SetEnabled(false);
        }
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
            /*throw new RuntimeException(context.toString()
                    + " must implement OnFragmentInteractionListener");*/
        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        AMCTSerializableObject object = new AMCTSerializableObject(mCurrGridCoordSystem);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, object);
    }

    @Override
    public void setObject(Object obj) {
        mCurrGridCoordSystem = (IMcGridCoordinateSystem) obj;
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
