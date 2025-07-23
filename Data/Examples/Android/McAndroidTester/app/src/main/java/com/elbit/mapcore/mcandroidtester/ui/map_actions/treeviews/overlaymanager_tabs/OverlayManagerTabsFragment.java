package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlaymanager_tabs;

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

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link OverlayManagerTabsFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link OverlayManagerTabsFragment#newInstance} factory method to
 * create an instance of this fragment.
 *
 */
public class OverlayManagerTabsFragment extends Fragment implements FragmentWithObject {
    private static final int TABS_NUN = 2;

    private OnFragmentInteractionListener mListener;
    public SectionsPagerAdapter mSectionsPagerAdapter;
    public ViewPager mViewPager;
    private IMcOverlayManager mOverlayManager;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment TerrainDetailsTabsFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static OverlayManagerTabsFragment newInstance(String param1, String param2) {
        OverlayManagerTabsFragment fragment = new OverlayManagerTabsFragment();
        return fragment;
    }

    public OverlayManagerTabsFragment() {
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
        getActivity().setTitle("overlay manager tabs");

        View rootView = inflater.inflate(R.layout.fragment_overlay_manager_tabs, container, false);
        initTabs(rootView);
        return rootView;
    }

    public void initTabs(View rootView) {
        (getActivity().findViewById(R.id.maps_container_rl)).setPadding(0,0,0,0);
        mSectionsPagerAdapter = new SectionsPagerAdapter(getChildFragmentManager());
        mViewPager = (ViewPager) rootView.findViewById(R.id.overlay_manager_tabs_container);
        mViewPager.setAdapter(mSectionsPagerAdapter);
        mViewPager.setOffscreenPageLimit(TABS_NUN);

        TabLayout tabLayout = (TabLayout) rootView.findViewById(R.id.overlay_manager_tabs);
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
        mOverlayManager = (IMcOverlayManager) obj;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        if(mIsVisibleToUser) {

         outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mOverlayManager));
        }
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
                    Fragment overlayManagerDetailsFragment = OverlayManagerDetailsFragment.newInstance();
                    ((FragmentWithObject)overlayManagerDetailsFragment).setObject(mOverlayManager);
                    return overlayManagerDetailsFragment;
                case 1:
                    Fragment overlayManagerSizeFactorFragment = OverlayManagerSizeFactorFragment.newInstance();
                    ((FragmentWithObject)overlayManagerSizeFactorFragment).setObject(mOverlayManager);
                    return overlayManagerSizeFactorFragment;
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
                    return "Size Factor";
                case 2:
                    return "Object Changing";
            }
            return null;
        }
    }
}
