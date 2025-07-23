package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlay_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import com.google.android.material.tabs.TabLayout;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentPagerAdapter;
import androidx.fragment.app.FragmentTabHost;
import androidx.viewpager.widget.ViewPager;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.RelativeLayout;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;

import com.elbit.mapcore.Interfaces.Map.IMcMapTerrain;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlay;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

public class OverlayTabsFragment extends Fragment implements FragmentWithObject {
    private static final int TABS_NUN = 4;

    private OnFragmentInteractionListener mListener;
    private FragmentTabHost mTabHost;
    private IMcMapTerrain mMapTerrain;
    public SectionsPagerAdapter mSectionsPagerAdapter;
    public ViewPager mViewPager;
    private IMcOverlay mOverlay;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment TerrainDetailsTabsFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static OverlayTabsFragment newInstance() {
        OverlayTabsFragment fragment = new OverlayTabsFragment();
       return fragment;
    }

    public OverlayTabsFragment() {
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
        getActivity().setTitle("overlay tabs");

        View rootView = inflater.inflate(R.layout.fragment_overlay_tabs, container, false);
        initTabs(rootView);
        return rootView;
    }

    public void initTabs(View rootView) {
        ((RelativeLayout)getActivity().findViewById(R.id.maps_container_rl)).setPadding(0,0,0,0);
        mSectionsPagerAdapter = new SectionsPagerAdapter(getChildFragmentManager());
        mViewPager = (ViewPager) rootView.findViewById(R.id.overlay_tabs_container);
        mViewPager.setAdapter(mSectionsPagerAdapter);
        mViewPager.setOffscreenPageLimit(TABS_NUN);

        TabLayout tabLayout = (TabLayout) rootView.findViewById(R.id.overlay_tabs);
        tabLayout.setupWithViewPager(mViewPager);
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
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    @Override
    public void setObject(Object obj) {
        mOverlay = (IMcOverlay) obj;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        if(mIsVisibleToUser)
            outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mOverlay));
    }

    private boolean mIsVisibleToUser;

    @Override
    public void setUserVisibleHint(boolean isVisibleToUser) {
        super.setUserVisibleHint(isVisibleToUser);
        mIsVisibleToUser = isVisibleToUser;
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
                    Fragment overlayGeneralFragment= OverlayGeneralTabFragment.newInstance();
                    ((FragmentWithObject)overlayGeneralFragment).setObject(mOverlay);
                    return overlayGeneralFragment;
                case 1:
                    Fragment overlayObjectsFragment= OverlayObjectsFragment.newInstance();
                    ((FragmentWithObject)overlayObjectsFragment).setObject(mOverlay);
                    return overlayObjectsFragment;
                case 2:
                    Fragment overlayObjectChangingFragment= OverlayObjectChangingTabFragment.newInstance();
                    ((FragmentWithObject)overlayObjectChangingFragment).setObject(mOverlay);
                    return overlayObjectChangingFragment;
                case 3:
                    Fragment overlayColorOverridingFragment= FragmentOverlayColorOverridingFragment.newInstance();
                    ((FragmentWithObject)overlayColorOverridingFragment).setObject(mOverlay);
                    return overlayColorOverridingFragment;
                default:
                    return null;
            }
        }

        @Override
        public int getCount() {
            return TABS_NUN;
        }

        @Override
        public CharSequence getPageTitle(int position) {
            switch (position) {
                case 0:
                    return "General";
                case 1:
                    return "Objects";
                case 2:
                    return "Object Changing";
                case 3:
                    return "Color Overriding";
            }
            return null;
        }
    }
}
