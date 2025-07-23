package com.elbit.mapcore.mcandroidtester.ui.objects.fragments;

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
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.ui.objects.fragments.create_text_tabs.CreateTextTabsFragment;
import com.elbit.mapcore.mcandroidtester.ui.objects.fragments.properties_tabs.ObjectPropertiesTabsArcFragment;
import com.elbit.mapcore.mcandroidtester.ui.objects.fragments.properties_tabs.ObjectPropertiesTabsArrow;
import com.elbit.mapcore.mcandroidtester.ui.objects.fragments.properties_tabs.ObjectPropertiesTabsClosedShapeFragment;
import com.elbit.mapcore.mcandroidtester.ui.objects.fragments.properties_tabs.ObjectPropertiesTabsEllipseFragment;
import com.elbit.mapcore.mcandroidtester.ui.objects.fragments.properties_tabs.ObjectPropertiesTabsGeneralFragment;
import com.elbit.mapcore.mcandroidtester.ui.objects.fragments.properties_tabs.ObjectPropertiesTabsLineBasedFragment;
import com.elbit.mapcore.mcandroidtester.ui.objects.fragments.properties_tabs.ObjectPropertiesTabsLineExpansion;
import com.elbit.mapcore.mcandroidtester.ui.objects.fragments.properties_tabs.ObjectPropertiesTabsPictureFragment;
import com.elbit.mapcore.mcandroidtester.ui.objects.fragments.properties_tabs.ObjectPropertiesTabsRectangle;
import com.elbit.mapcore.mcandroidtester.ui.objects.fragments.properties_tabs.ObjectPropertiesTabsSightPresentationFragment;

import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link ObjectPropertiesFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link ObjectPropertiesFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class ObjectPropertiesFragment extends Fragment {
    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;

    private static final int TABS_NUN = 11;

    private IMcMapViewport mViewport;
    private SectionsPagerAdapter mSectionsPagerAdapter;
    private ViewPager mViewPager;
    private OnFragmentInteractionListener mListener;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment ObjectPropertiesFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static ObjectPropertiesFragment newInstance(String param1, String param2) {
        ObjectPropertiesFragment fragment = new ObjectPropertiesFragment();
        Bundle args = new Bundle();
        args.putString(ARG_PARAM1, param1);
        args.putString(ARG_PARAM2, param2);
        fragment.setArguments(args);
        return fragment;
    }

    public ObjectPropertiesFragment() {
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
        View view = inflater.inflate(R.layout.fragment_object_properties, container, false);
        initTabs(view);
        return view;
    }

    public void initTabs(View rootView) {
        ((RelativeLayout) getActivity().findViewById(R.id.maps_container_rl)).setPadding(0, 0, 0, 0);
        mSectionsPagerAdapter = new SectionsPagerAdapter(getChildFragmentManager());
        mViewPager = (ViewPager) rootView.findViewById(R.id.object_properties_container);
        mViewPager.setAdapter(mSectionsPagerAdapter);
        mViewPager.setOffscreenPageLimit(TABS_NUN);
        TabLayout tabLayout = (TabLayout) rootView.findViewById(R.id.object_properties_tabs);
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
        } else {
            //throw new RuntimeException(context.toString()
            //        + " must implement OnFragmentInteractionListener");
        }
        if (context instanceof MapsContainerActivity) {
            ((MapsContainerActivity) context).mCurFragmentTag = ObjectPropertiesFragment.class.getSimpleName();
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

    public class SectionsPagerAdapter extends FragmentPagerAdapter {

        public SectionsPagerAdapter(FragmentManager fm) {
            super(fm);
        }

        @Override
        public Fragment getItem(int position) {
            switch (position) {
                case 0:
                    ObjectPropertiesTabsGeneralFragment generalFragment = ObjectPropertiesTabsGeneralFragment.newInstance("", "");
                    return generalFragment;
                case 1:
                    ObjectPropertiesTabsLineBasedFragment lineBasedFragment = ObjectPropertiesTabsLineBasedFragment.newInstance();
                    return lineBasedFragment;
                case 2:
                    ObjectPropertiesTabsClosedShapeFragment closedShapeFragment = ObjectPropertiesTabsClosedShapeFragment.newInstance();
                    return closedShapeFragment;
                case 3:
                    ObjectPropertiesTabsArcFragment arcFragment = ObjectPropertiesTabsArcFragment.newInstance("", "");
                    return arcFragment;
                case 4:
                    ObjectPropertiesTabsEllipseFragment ellipseFragment = ObjectPropertiesTabsEllipseFragment.newInstance("", "");
                    return ellipseFragment;
                case 5:
                    ObjectPropertiesTabsRectangle rectangleFragment = ObjectPropertiesTabsRectangle.newInstance("", "");
                    return rectangleFragment;
                case 6:
                    CreateTextTabsFragment textFragment = CreateTextTabsFragment.newInstance();
                    return textFragment;
                case 7:
                    ObjectPropertiesTabsArrow arrowFragment = ObjectPropertiesTabsArrow.newInstance("", "");
                    return arrowFragment;
                case 8:
                    ObjectPropertiesTabsPictureFragment pictureFragment = ObjectPropertiesTabsPictureFragment.newInstance();
                    return pictureFragment;
                case 9:
                    ObjectPropertiesTabsLineExpansion lineExpansionFragment = ObjectPropertiesTabsLineExpansion.newInstance("", "");
                    return lineExpansionFragment;
                case 10:
                    ObjectPropertiesTabsSightPresentationFragment sightPresentationFragment = ObjectPropertiesTabsSightPresentationFragment.newInstance();
                    return sightPresentationFragment;
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
                    return "Line Based";
                case 2:
                    return "Closed Shapes";
                case 3:
                    return "Arc";
                case 4:
                    return "Ellipse";
                case 5:
                    return "Rectangle";
                case 6:
                    return "Text";
                case 7:
                    return "Arrow";
                case 8:
                    return "Picture";
                case 9:
                    return "Line Expansion";
                case 10:
                    return "Sight Presentation";
            }
            return null;
        }
    }
}
