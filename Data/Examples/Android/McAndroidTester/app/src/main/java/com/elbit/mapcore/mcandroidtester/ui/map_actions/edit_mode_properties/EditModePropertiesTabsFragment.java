package com.elbit.mapcore.mcandroidtester.ui.map_actions.edit_mode_properties;

import android.content.Context;
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
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcTexture;


public class EditModePropertiesTabsFragment extends Fragment {
    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;

    private int TABS_NUN=4;
    private SectionsPagerAdapter mSectionsPagerAdapter;
    private ViewPager mViewPager;
    private OnEditModePropertiesTabsInteractionListener mEditModePropertiesTabs;


    // TODO: Rename and change types and number of parameters
    public static EditModePropertiesTabsFragment newInstance() {
        EditModePropertiesTabsFragment fragment = new EditModePropertiesTabsFragment();
        return fragment;
    }

    public EditModePropertiesTabsFragment() {
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
        View view = inflater.inflate(R.layout.fragment_edit_mode_properties_tabs, container, false);
        initTabs(view);
        return view;
    }

    public void initTabs(View rootView) {
        ((RelativeLayout) getActivity().findViewById(R.id.maps_container_rl)).setPadding(0, 0, 0, 0);
        mSectionsPagerAdapter = new SectionsPagerAdapter(getChildFragmentManager());
        mViewPager = (ViewPager) rootView.findViewById(R.id.edit_mode_properties_container);
        mViewPager.setAdapter(mSectionsPagerAdapter);
        mViewPager.setOffscreenPageLimit(TABS_NUN);
        TabLayout tabLayout = (TabLayout) rootView.findViewById(R.id.edit_mode_properties_tabs);
        tabLayout.setupWithViewPager(mViewPager);
        mSectionsPagerAdapter.notifyDataSetChanged();


    }


    @Override
    public void onAttach(Context context) {
        super.onAttach(context);
        if (context instanceof OnEditModePropertiesTabsInteractionListener) {
            mEditModePropertiesTabs = (OnEditModePropertiesTabsInteractionListener) context;
        } else {
            //throw new RuntimeException(context.toString()
            //        + " must implement OnFragmentInteractionListener");
        }
        if (context instanceof MapsContainerActivity)
            ((MapsContainerActivity) context).mCurFragmentTag = EditModePropertiesTabsFragment.class.getSimpleName();
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mEditModePropertiesTabs = null;
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
    public interface OnEditModePropertiesTabsInteractionListener {
        // TODO: Update argument type and name
        void EditModePropertiesTabsInteractionListener(IMcTexture createdTexture);
    }

    public class SectionsPagerAdapter extends FragmentPagerAdapter {

        public SectionsPagerAdapter(FragmentManager fm) {
            super(fm);
        }

        @Override
        public Fragment getItem(int position) {
                switch (position) {
                    case 0:
                        EditModePropertiesGeneralTabFragment editModePropertiesGeneralTabFragment = EditModePropertiesGeneralTabFragment.newInstance();
                        return editModePropertiesGeneralTabFragment;
                    case 1:
                        UtilityItemsFragment utilityItemsFragment2 = UtilityItemsFragment.newInstance("", "");
                        return utilityItemsFragment2;

                    case 2:
                        UtilityItemsFragment utilityItemsFragment3 = UtilityItemsFragment.newInstance("", "");
                        return utilityItemsFragment3;
                    case 3:
                        UtilityItemsFragment utilityItemsFragment4 = UtilityItemsFragment.newInstance("", "");
                        return utilityItemsFragment4;
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
                        return "Map Manipulations Operations";
                    case 2:
                        return "Permissions";
                    case 3:
                        return "Utility Items";
                }
            return null;
        }
    }
}
