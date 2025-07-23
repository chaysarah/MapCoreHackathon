package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.viewport_tabs;

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
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.viewport_tabs.viewport_as_camera_tabs.ViewPortAsCameraTabsFragment;

import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link ViewPortDetailsTabsFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link ViewPortDetailsTabsFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class ViewPortDetailsTabsFragment extends Fragment implements FragmentWithObject{
    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";
    private static final int TABS_NUN=5;
    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;

    private OnFragmentInteractionListener mListener;
    private IMcMapViewport mViewport;
    private SectionsPagerAdapter mSectionsPagerAdapter;
    private ViewPager mViewPager;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment ViewPortDetailsTabsFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static ViewPortDetailsTabsFragment newInstance(String param1, String param2) {
        ViewPortDetailsTabsFragment fragment = new ViewPortDetailsTabsFragment();
        Bundle args = new Bundle();
        args.putString(ARG_PARAM1, param1);
        args.putString(ARG_PARAM2, param2);
        fragment.setArguments(args);
        return fragment;
    }

    public ViewPortDetailsTabsFragment() {
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
        Funcs.SetObjectFromBundle(savedInstanceState, this );
        View view=inflater.inflate(R.layout.fragment_view_port_details_tabs, container, false);
        initTabs(view);
        return view;
    }
    public void initTabs(View rootView) {
        ((RelativeLayout)getActivity().findViewById(R.id.maps_container_rl)).setPadding(0,0,0,0);
        mSectionsPagerAdapter = new SectionsPagerAdapter(getChildFragmentManager());
        mViewPager = (ViewPager) rootView.findViewById(R.id.viewport_details_tabs_container);
        mViewPager.setAdapter(mSectionsPagerAdapter);
        mViewPager.setOffscreenPageLimit(TABS_NUN);
        TabLayout tabLayout = (TabLayout) rootView.findViewById(R.id.viewport_details_tabs_tabs);
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
            throw new RuntimeException(context.toString()
                    + " must implement OnFragmentInteractionListener");
        }
        if (context instanceof MapsContainerActivity)
            ((MapsContainerActivity) context).mCurFragmentTag = ViewPortDetailsTabsFragment.class.getSimpleName();

    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    @Override
    public void setObject(Object obj) {
        mViewport = (IMcMapViewport) obj;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mViewport));
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
                    Fragment viewportDetailsFragment= ViewPortDetailsFragment.newInstance("","");
                    ((FragmentWithObject)viewportDetailsFragment).setObject(mViewport);
                    return viewportDetailsFragment;
                case 1:
                    Fragment viewportTerrainsFragment= ViewPortTerrainsFragment.newInstance("","");
                    ((FragmentWithObject)viewportTerrainsFragment).setObject(mViewport);
                    return viewportTerrainsFragment;
                case 2:
                    Fragment viewPortAsCameraFragment= ViewPortAsCameraTabsFragment.newInstance("","");
                    ((FragmentWithObject)viewPortAsCameraFragment).setObject(mViewport);
                    return viewPortAsCameraFragment;
                case 3:
                    Fragment viewPortRenderingFragment= ViewPortRenderingFragment.newInstance();
                    ((FragmentWithObject)viewPortRenderingFragment).setObject(mViewport);
                    return viewPortRenderingFragment;
                case 4:
                    Fragment viewPortDTMVisualizationFragment= ViewPortDTMVisualizationFragment.newInstance("","");
                    ((FragmentWithObject)viewPortDTMVisualizationFragment).setObject(mViewport);
                    return viewPortDTMVisualizationFragment;
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
                    return "Terrains";
                case 2:
                    return "Viewport As Camera";
                case 3:
                    return "Rendering";
                case 4:
                    return "DTM Visualization";
            }
            return null;
        }
    }
}
