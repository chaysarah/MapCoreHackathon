package com.elbit.mapcore.mcandroidtester.ui.map_actions.edit_mode_properties;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.ui.adapters.UtilityItemsAdapter;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

import com.elbit.mapcore.Enums.EMcPointCoordSystem;
import com.elbit.mapcore.Interfaces.Calculations.IMcSpatialQueries;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link UtilityItemsFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link UtilityItemsFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class UtilityItemsFragment extends Fragment {
    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;

    private OnFragmentInteractionListener mListener;
    private View mRootView;
    private ListView mUtilityItemsLV;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment UtilityItemsFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static UtilityItemsFragment newInstance(String param1, String param2) {
        UtilityItemsFragment fragment = new UtilityItemsFragment();
        Bundle args = new Bundle();
        args.putString(ARG_PARAM1, param1);
        args.putString(ARG_PARAM2, param2);
        fragment.setArguments(args);
        return fragment;
    }

    public UtilityItemsFragment() {
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
        mRootView=inflater.inflate(R.layout.fragment_utility_items, container, false);
        inflateViews();
        initViews();
        // Inflate the layout for this fragment
        return mRootView;
    }

    private void initViews() {
        mUtilityItemsLV.setAdapter(new UtilityItemsAdapter(getContext(),EMcPointCoordSystem.values(), IMcSpatialQueries.EQueryPrecision.values()));
        Funcs.setListViewHeightBasedOnChildren(mUtilityItemsLV);
    }

    private void inflateViews() {
        mUtilityItemsLV=(ListView)mRootView.findViewById(R.id.utility_items_lv);
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
        if (context instanceof MapsContainerActivity) {
            ((MapsContainerActivity) context).mCurFragmentTag = UtilityItemsFragment.class.getSimpleName();        /* else {
            throw new RuntimeException(context.toString()
                    + " must implement OnFragmentInteractionListener");
        }*/
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
