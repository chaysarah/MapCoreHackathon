package com.elbit.mapcore.mcandroidtester.ui.objects.fragments;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.DialogFragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.ui.adapters.ScanItemsFoundAdapter;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;

import com.elbit.mapcore.Interfaces.Calculations.IMcSpatialQueries;


public class ScanItemFoundFragment extends DialogFragment implements FragmentWithObject{
    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ItemsFoundList = "ItemsFoundList";

    private OnFragmentInteractionListener mListener;
    private IMcSpatialQueries.STargetFound[] mItemsFoundList;
    private View mRootView;
    private ListView mFoundItemsLV;

    // TODO: Rename and change types and number of parameters
     public ScanItemFoundFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if(savedInstanceState != null)
        {
            mItemsFoundList = (IMcSpatialQueries.STargetFound[]) savedInstanceState.getSerializable(ItemsFoundList);
        }
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        mRootView=inflater.inflate(R.layout.fragment_scan_item_found, container, false);
        inflateViews();
        initViews();
        // Inflate the layout for this fragment
        return mRootView;
    }

    private void initViews() {
        if(mItemsFoundList == null)
            mItemsFoundList = new IMcSpatialQueries.STargetFound[0];
        mFoundItemsLV.setAdapter(new ScanItemsFoundAdapter(getContext(),R.layout.scan_item_found_row,mItemsFoundList));
        View header = getActivity().getLayoutInflater().inflate(R.layout.scan_item_found_header,null);
        mFoundItemsLV.addHeaderView(header);
    }

    private void inflateViews() {
        mFoundItemsLV=(ListView)mRootView.findViewById(R.id.found_items_lv);
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
        }/* else {
            throw new RuntimeException(context.toString()
                    + " must implement OnFragmentInteractionListener");
        }*/
        if (context instanceof MapsContainerActivity)
            ((MapsContainerActivity) context).mCurFragmentTag = ScanItemFoundFragment.class.getSimpleName();
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    @Override
    public void setObject(Object obj) {
        if(obj instanceof IMcSpatialQueries.STargetFound[])
            mItemsFoundList= (IMcSpatialQueries.STargetFound[]) obj;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(ItemsFoundList,mItemsFoundList );
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
