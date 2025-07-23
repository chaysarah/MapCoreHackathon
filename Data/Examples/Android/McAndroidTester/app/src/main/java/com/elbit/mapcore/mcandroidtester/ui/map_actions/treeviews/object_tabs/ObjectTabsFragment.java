package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.object_tabs;

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
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.RelativeLayout;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;

import com.elbit.mapcore.Interfaces.Map.IMcMapTerrain;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link ObjectTabsFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link ObjectTabsFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class ObjectTabsFragment extends Fragment implements FragmentWithObject {
    private static final int TABS_NUN = 2;

    private IMcObject mObject;
    private View mRootView;
    private OnFragmentInteractionListener mListener;
    private FragmentTabHost mTabHost;
    private IMcMapTerrain mMapTerrain;
    public SectionsPagerAdapter mSectionsPagerAdapter;
    public ViewPager mViewPager;
    private CheckBox mDetectibilityCB;
    private Button mDetectibilityBttn;
    private Context mContext;

    public ObjectTabsFragment() {
        // Required empty public constructor
    }

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment ObjectTabsFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static ObjectTabsFragment newInstance() {
        ObjectTabsFragment fragment = new ObjectTabsFragment();
        return fragment;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        Funcs.SetObjectFromBundle(savedInstanceState, this );
        setTitle();        // Inflate the layout for this fragment
        mRootView = inflater.inflate(R.layout.fragment_object_tabs, container, false);
        initTabs(mRootView);
        return mRootView;
    }

    public void initTabs(View rootView) {
        ((RelativeLayout) getActivity().findViewById(R.id.maps_container_rl)).setPadding(0, 0, 0, 0);
        mSectionsPagerAdapter = new SectionsPagerAdapter(getChildFragmentManager());
        mViewPager = (ViewPager) rootView.findViewById(R.id.object_tabs_container);
        mViewPager.setAdapter(mSectionsPagerAdapter);
        mViewPager.setOffscreenPageLimit(TABS_NUN);

        TabLayout tabLayout = (TabLayout) rootView.findViewById(R.id.object_tabs);
        tabLayout.setupWithViewPager(mViewPager);
    }

    private void setTitle() {
        getActivity().setTitle("object tabs");

    }

    @Override
    public void onHiddenChanged(boolean hidden) {
        super.onHiddenChanged(hidden);
        if (!hidden) {
            setTitle();
            if (mContext instanceof MapsContainerActivity) {
                ((MapsContainerActivity) mContext).mCurFragmentTag = ObjectTabsFragment.class.getSimpleName();
            }
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
        mContext = context;
       /* if (context instanceof OnFragmentInteractionListener) {
            mListener = (OnFragmentInteractionListener) context;
        } else {
            throw new RuntimeException(context.toString()
                    + " must implement OnFragmentInteractionListener");
        }*/
        if (context instanceof MapsContainerActivity) {
            ((MapsContainerActivity) context).mCurFragmentTag = ObjectTabsFragment.class.getSimpleName();
        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    @Override
    public void setObject(Object obj) {
        mObject = (IMcObject) obj;

    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mObject));
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

    @Override
    public void onStart() {
        super.onStart();
        if (mContext instanceof MapsContainerActivity) {
            ((MapsContainerActivity) mContext).mCurFragmentTag = ObjectTabsFragment.class.getSimpleName();
        }
    }

    public class SectionsPagerAdapter extends FragmentPagerAdapter {

        public SectionsPagerAdapter(FragmentManager fm) {
            super(fm);
        }

        @Override
        public Fragment getItem(int position) {
            switch (position) {
                case 0:
                    Fragment objectGeneralFragment = ObjectGeneralTabFragment.newInstance();
                    ((FragmentWithObject) objectGeneralFragment).setObject(mObject);
                    return objectGeneralFragment;
                case 1:
                    Fragment objectLocationsObject = ObjectLocationsTabsFragment.newInstance();
                    ((FragmentWithObject) objectLocationsObject).setObject(mObject);
                    return objectLocationsObject;
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
                    return "Object Locations";
            }
            return null;
        }
    }


}
