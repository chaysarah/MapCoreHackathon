package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.layers_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import com.google.android.material.tabs.TabLayout;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentPagerAdapter;
import androidx.viewpager.widget.ViewPager;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.RelativeLayout;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;

import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link BaseLayerDetailsTabsFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link BaseLayerDetailsTabsFragment#newInstance} factory method to
 * create an instance of this fragment.
 *
 */
public class BaseLayerDetailsTabsFragment extends Fragment implements FragmentWithObject{
    private OnFragmentInteractionListener mListener;
    protected SectionsPagerAdapter mSectionsPagerAdapter;
    private ViewPager mViewPager;
    protected IMcMapLayer mMapLayer;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment BaseLayerDetailsTabsFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static BaseLayerDetailsTabsFragment newInstance(String param1, String param2) {
        BaseLayerDetailsTabsFragment fragment = new BaseLayerDetailsTabsFragment();
        return fragment;
    }
    public BaseLayerDetailsTabsFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
     }
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        Funcs.SetObjectFromBundle(savedInstanceState, this );
        View rootView = inflater.inflate(R.layout.fragment_base_layer_details_tabs, container, false);
        initTabs(rootView);
        return rootView;
    }
    public void initTabs(View rootView) {
        ((RelativeLayout)getActivity().findViewById(R.id.maps_container_rl)).setPadding(0,0,0,0);
        mSectionsPagerAdapter = getSectionPagerAdapter();
        mViewPager = (ViewPager) rootView.findViewById(R.id.container);
        mViewPager.setAdapter(mSectionsPagerAdapter);
        mViewPager.setOffscreenPageLimit(1);
        TabLayout tabLayout = (TabLayout) rootView.findViewById(R.id.tabs);
        tabLayout.setupWithViewPager(mViewPager);
    }

    protected SectionsPagerAdapter getSectionPagerAdapter() {
        return new SectionsPagerAdapter(getChildFragmentManager());
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

    @Override
    public void setObject(Object obj) {
        mMapLayer = (IMcMapLayer) obj;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mMapLayer));
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

    public class SectionsPagerAdapter extends FragmentPagerAdapter {

        public SectionsPagerAdapter(FragmentManager fm) {
            super(fm);
        }

        @Override
        public Fragment getItem(int position) {
            switch (position) {
                case 0:
                    MapLayerFragment mapLayerFragment =MapLayerFragment.newInstance("","");
                    ((FragmentWithObject)mapLayerFragment).setObject(mMapLayer);
                    return mapLayerFragment;
                default:
                    return null;
            }
        }

        @Override
        public int getCount() {
            return 1;
        }

        @Override
        public CharSequence getPageTitle(int position) {
            switch (position) {
                case 0:
                    return "Map Layer";
            }
            return null;
        }
    }
}
