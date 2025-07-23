package com.elbit.mapcore.mcandroidtester.ui.map.fragments;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.utils.customviews.TilingScheme;

public class TilingSchemeFragment extends Fragment {

    private View mFragmentView;
    private OnFragmentInteractionListener mListener;

    private TilingScheme mTilingScheme;

    public TilingSchemeFragment() {
        // Required empty public constructor
    }

    public static TilingSchemeFragment newInstance() {
        TilingSchemeFragment fragment = new TilingSchemeFragment();
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
        mFragmentView = inflater.inflate(R.layout.fragment_tiling_scheme, container, false);
        mTilingScheme = (TilingScheme) mFragmentView.findViewById(R.id.tiling_scheme);
        return mFragmentView;
    }

   /* public void HideSaveButton()
    {
        mRawVector3DExtrusionParamsDetails.HideSaveButton();
    }*/

    // TODO: Rename method, update argument and hook method into UI event
    public void onButtonPressed(Uri uri) {
        if (mListener != null) {
            mListener.onFragmentInteraction(uri);
        }
    }

    @Override
    public void onAttach(Context context) {
        super.onAttach(context);
        if (context instanceof TilingSchemeFragment.OnFragmentInteractionListener) {
            mListener = (TilingSchemeFragment.OnFragmentInteractionListener) context;
        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    public IMcMapLayer.STilingScheme getParams()
    {
        return mTilingScheme.getTilingScheme();
    }

     public interface OnFragmentInteractionListener {
        // TODO: Update argument type and name
        void onFragmentInteraction(Uri uri);

    }
}
