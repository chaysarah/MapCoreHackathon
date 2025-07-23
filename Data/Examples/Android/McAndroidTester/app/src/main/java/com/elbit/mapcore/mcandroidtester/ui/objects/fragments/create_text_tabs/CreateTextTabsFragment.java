package com.elbit.mapcore.mcandroidtester.ui.objects.fragments.create_text_tabs;

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

import com.elbit.mapcore.Interfaces.OverlayManager.IMcFont;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.IFontTab;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link CreateTextTabsFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link CreateTextTabsFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class CreateTextTabsFragment extends Fragment implements IFontTab {

    private static int TABS_NUN = 2;

    private SectionsPagerAdapter mSectionsPagerAdapter;
    private ViewPager mViewPager;
    private OnFragmentInteractionListener mListener;
    private IMcFont mCurrentFont;
    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment ObjectPropertiesFragment.
     */

    public static CreateTextTabsFragment newInstance() {
        CreateTextTabsFragment fragment = new CreateTextTabsFragment();
        return fragment;
    }

    public CreateTextTabsFragment() {
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
        View view = inflater.inflate(R.layout.fragment_text_tabs, container, false);
        initTabs(view);
        return view;
    }

    public void initTabs(View rootView) {
        ((RelativeLayout) getActivity().findViewById(R.id.maps_container_rl)).setPadding(0, 0, 0, 0);
        mSectionsPagerAdapter = new SectionsPagerAdapter(getChildFragmentManager());
        mViewPager = (ViewPager) rootView.findViewById(R.id.text_container);
        mViewPager.setAdapter(mSectionsPagerAdapter);
        mViewPager.setOffscreenPageLimit(TABS_NUN);
        TabLayout tabLayout = (TabLayout) rootView.findViewById(R.id.text_tabs);
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
            ((MapsContainerActivity) context).mCurFragmentTag = CreateTextTabsFragment.class.getSimpleName();
        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    @Override
    public void setCurrentFont(IMcFont currentFont) {
        mCurrentFont = currentFont;
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
            if(mCurrentFont == null) {
                switch (position) {
                    case 0:
                        ObjectPropertiesTabsText objectPropertiesTabsText = ObjectPropertiesTabsText.newInstance();
                        return objectPropertiesTabsText;
                    case 1:
                        TextMappingFragment textMappingFragment = TextMappingFragment.newInstance();
                        return textMappingFragment;
                    default:
                        return null;
                }
            }
            else
            {
                ObjectPropertiesTabsText objectPropertiesTabsText = ObjectPropertiesTabsText.newInstance();
                objectPropertiesTabsText.setCurrentFont(mCurrentFont);
                return objectPropertiesTabsText;
            }
        }

        @Override
        public int getCount() {
            if(mCurrentFont != null)
                TABS_NUN = 1;
            else
                TABS_NUN = 2;
            return TABS_NUN;
        }

        @Override
        public CharSequence getPageTitle(int position) {
            if(mCurrentFont != null)
                return  "Text";
            else {
                switch (position) {
                    case 0:
                        return "Text";
                    case 1:
                        return "Mapping";
                }
            }
            return null;
        }
    }
}
