package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.viewport_tabs.viewport_as_camera_tabs;

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

import com.elbit.mapcore.Interfaces.Map.IMcMapCamera;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link ViewPortAsCameraTabsFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link ViewPortAsCameraTabsFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class ViewPortAsCameraTabsFragment extends Fragment implements FragmentWithObject {

    private OnFragmentInteractionListener mListener;
    private IMcMapCamera mCurrentObject;
    private SectionsPagerAdapter mSectionsPagerAdapter;
    private ViewPager mViewPager;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment ViewPortAsCameraTabsFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static ViewPortAsCameraTabsFragment newInstance(String param1, String param2) {
        ViewPortAsCameraTabsFragment fragment = new ViewPortAsCameraTabsFragment();
        return fragment;
    }

    public ViewPortAsCameraTabsFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        Funcs.SetObjectFromBundle(savedInstanceState, this );
        View rootView = inflater.inflate(R.layout.fragment_view_port_as_camera, container, false);
        initTabs(rootView);
        return rootView;
    }

    public void initTabs(View rootView) {
        mSectionsPagerAdapter = new SectionsPagerAdapter(getChildFragmentManager());
        mViewPager = (ViewPager) rootView.findViewById(R.id.viewport_as_camera_tabs_container);
        mViewPager.setAdapter(mSectionsPagerAdapter);
        mViewPager.setOffscreenPageLimit(5);
        TabLayout tabLayout = (TabLayout) rootView.findViewById(R.id.viewport_as_camera_tabs_tabs);
        tabLayout.setupWithViewPager(mViewPager);
    }

    public void onButtonPressed(Uri uri) {
        if (mListener != null) {
            mListener.onFragmentInteraction(uri);
        }
    }

    @Override
    public void onAttach(Context context) {
        super.onAttach(context);
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    @Override
    public void setObject(Object obj) {
        if(obj instanceof IMcMapViewport)
            mCurrentObject = (IMcMapViewport) obj;
        else
            mCurrentObject = (IMcMapCamera) obj;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mCurrentObject));
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
                    Fragment cameraConversionFragment = VpAsCameraConversionsFragment.newInstance();
                    ((FragmentWithObject) cameraConversionFragment).setObject(mCurrentObject);
                    return cameraConversionFragment;
                case 1:
                    Fragment vpAsCameraAnyMapTypeFragment = VpAsCameraAnyMapTypeFragment.newInstance();
                    ((FragmentWithObject) vpAsCameraAnyMapTypeFragment).setObject(mCurrentObject);
                    return vpAsCameraAnyMapTypeFragment;
                case 2:
                    Fragment vpAsCamera2DMapFragment = VpAsCamera2DMapFragment.newInstance();
                    ((FragmentWithObject) vpAsCamera2DMapFragment).setObject(mCurrentObject);
                    return vpAsCamera2DMapFragment;
                case 3:
                    Fragment vpAsCamera3DMapFragment = VpAsCamera3DMapFragment.newInstance();
                    ((FragmentWithObject) vpAsCamera3DMapFragment).setObject(mCurrentObject);
                    return vpAsCamera3DMapFragment;
                case 4:
                    Fragment vpAsCameraAttachmentFragment = VpAsCameraAttachmentFragment.newInstance();
                    ((FragmentWithObject) vpAsCameraAttachmentFragment).setObject(mCurrentObject);
                    return vpAsCameraAttachmentFragment;
                default:
                    return null;
            }
        }

        @Override
        public int getCount() {
            return 5;
        }

        @Override
        public CharSequence getPageTitle(int position) {
            switch (position) {
                case 0:
                    return "Camera Conversions";
                case 1:
                    return "Any Map Type";
                case 2:
                    return "2D Map";
                case 3:
                    return "3D MAp";
                case 4:
                    return "Camera Attachment";
            }
            return null;
        }
    }

}
