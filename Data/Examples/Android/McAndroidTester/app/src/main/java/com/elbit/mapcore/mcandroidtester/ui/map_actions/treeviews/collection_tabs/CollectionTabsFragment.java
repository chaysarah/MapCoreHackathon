package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.collection_tabs;

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

import com.elbit.mapcore.Interfaces.OverlayManager.IMcCollection;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

/**
 * Created by TC97803 on 07/09/2017.
 */

public class CollectionTabsFragment extends Fragment implements FragmentWithObject {

    public CollectionSectionsPagerAdapter mSectionsPagerAdapter;
    public ViewPager mViewPager;
    private static final int TABS_NUN = 2;
    private IMcCollection mCollection;
    private OnFragmentInteractionListener mListener;

    public static CollectionTabsFragment newInstance() {
        CollectionTabsFragment fragment = new CollectionTabsFragment();
        return fragment;
    }

    public CollectionTabsFragment() {
        // Required empty public constructor
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        getActivity().setTitle("Collection");
        Funcs.SetObjectFromBundle(savedInstanceState, this );
        View rootView = inflater.inflate(R.layout.fragment_collection_tabs, container, false);
        initTabs(rootView);
        return rootView;
    }

    public void initTabs(View rootView) {
        ((RelativeLayout)getActivity().findViewById(R.id.maps_container_rl)).setPadding(0,0,0,0);
        mSectionsPagerAdapter = new CollectionSectionsPagerAdapter(getChildFragmentManager());
        mViewPager = (ViewPager) rootView.findViewById(R.id.collection_tabs_container);
        mViewPager.setAdapter(mSectionsPagerAdapter);
        mViewPager.setOffscreenPageLimit(TABS_NUN);

        TabLayout tabLayout = (TabLayout) rootView.findViewById(R.id.collection_tabs);
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
        mCollection = (IMcCollection) obj;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mCollection));
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

    public class CollectionSectionsPagerAdapter extends FragmentPagerAdapter {

        public CollectionSectionsPagerAdapter(FragmentManager fm) {
            super(fm);
        }

        @Override
        public Fragment getItem(int position) {
            switch (position) {
                case 0:
                    Fragment collectionGeneralFragment = CollectionGeneralTabFragment.newInstance();
                    ((FragmentWithObject)collectionGeneralFragment).setObject(mCollection);
                    return collectionGeneralFragment;
                case 1:
                    Fragment collectionObjectsFragment = CollectionObjectsFragment.newInstance();
                    ((FragmentWithObject)collectionObjectsFragment).setObject(mCollection);
                    return collectionObjectsFragment;
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
                    return "Collection";

            }
            return null;
        }
    }
}
