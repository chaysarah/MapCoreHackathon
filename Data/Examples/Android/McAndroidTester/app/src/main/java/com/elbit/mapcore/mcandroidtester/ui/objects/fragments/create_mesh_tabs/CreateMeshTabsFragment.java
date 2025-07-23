package com.elbit.mapcore.mcandroidtester.ui.objects.fragments.create_mesh_tabs;

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

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.IMeshTab;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcMesh;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.private_properties.MeshPropertyDialogFragment;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link CreateMeshTabsFragment.OnMeshTabsFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link CreateMeshTabsFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class CreateMeshTabsFragment extends Fragment implements IMeshTab {

    private int TABS_NUN = 2;

    private SectionsPagerAdapter mSectionsPagerAdapter;
    private ViewPager mViewPager;
    private OnMeshTabsFragmentInteractionListener mCreateMeshTabsListener;
    private IMcMesh mCurrentMesh;
    private String mMeshName;
    private MeshPropertyDialogFragment mMeshPropertyDialogFragment;

    public IMcMesh getmCurrentMesh() {
        return mCurrentMesh;
    }

    public void setmCurrentMesh(IMcMesh mCurrentMesh) {
        this.mCurrentMesh = mCurrentMesh;
    }


    // TODO: Rename and change types and number of parameters
    public static CreateMeshTabsFragment newInstance() {
        CreateMeshTabsFragment fragment = new CreateMeshTabsFragment();
        return fragment;
    }

    public CreateMeshTabsFragment() {
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
        View view = inflater.inflate(R.layout.fragment_mesh_tabs, container, false);
        initTabs(view);
        return view;
    }

    public void initTabs(View rootView) {
        (getActivity().findViewById(R.id.maps_container_rl)).setPadding(0, 0, 0, 0);
        mSectionsPagerAdapter = new SectionsPagerAdapter(getChildFragmentManager());
        mViewPager = (ViewPager) rootView.findViewById(R.id.mesh_container);
        mViewPager.setAdapter(mSectionsPagerAdapter);
        mViewPager.setOffscreenPageLimit(TABS_NUN);
        TabLayout tabLayout = (TabLayout) rootView.findViewById(R.id.mesh_tabs);
        tabLayout.setupWithViewPager(mViewPager);
        mSectionsPagerAdapter.notifyDataSetChanged();
    }

    @Override
    public void onAttach(Context context) {
        super.onAttach(context);
        if (context instanceof OnMeshTabsFragmentInteractionListener) {
            mCreateMeshTabsListener = (OnMeshTabsFragmentInteractionListener) context;
        } else {
            //throw new RuntimeException(context.toString()
            //        + " must implement OnFragmentInteractionListener");
        }
        if (context instanceof MapsContainerActivity)
            ((MapsContainerActivity) context).mCurFragmentTag = CreateMeshTabsFragment.class.getSimpleName();
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mCreateMeshTabsListener = null;
    }

    @Override
    public void setCurrentMesh(IMcMesh currentMesh) {
        mCurrentMesh = currentMesh;
    }

    public void setmMeshPropertyDialogFragment(MeshPropertyDialogFragment meshPropertyDialogFragment)
    {
        mMeshPropertyDialogFragment = meshPropertyDialogFragment;
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
    public interface OnMeshTabsFragmentInteractionListener {
        // TODO: Update argument type and name
        void MeshTabsFragmentInteraction(IMcMesh createdMesh);
    }

    public class SectionsPagerAdapter extends FragmentPagerAdapter {

        public SectionsPagerAdapter(FragmentManager fm) {
            super(fm);
        }

        @Override
        public Fragment getItem(int position) {
            switch (position) {
                case 0:
                    NativeMeshFileFragment nativeMeshFileFragment = NativeMeshFileFragment.newInstance();
                    nativeMeshFileFragment.setCurrentMesh(mCurrentMesh);
                    nativeMeshFileFragment.setmMeshPropertyDialogFragment(mMeshPropertyDialogFragment);
                    return nativeMeshFileFragment;
                case 1:
                    MeshFromListFragment textureFromListFragment = MeshFromListFragment.newInstance();
                    textureFromListFragment.setCurrentMesh(mCurrentMesh);
                    textureFromListFragment.setmMeshPropertyDialogFragment(mMeshPropertyDialogFragment);
                    return textureFromListFragment;
                default:
                    return null;
            }
        }

        @Override
        public int getCount() {
           /* if(mCurrentMesh==null)
                TABS_NUN = 2;
            else if (mCurrentMesh instanceof IMcNativeMeshFile)
                TABS_NUN = 1;
*/
            return TABS_NUN;
        }

        @Override
        public CharSequence getPageTitle(int position) {
            switch (position) {
                case 0:
                    return "Native Mesh File";
                case 1:
                    return "From List";
            }
           /* if (mCurrentMesh instanceof IMcImageFileMesh)
                return "Image File";
            if (mCurrentMesh instanceof IMcMemoryBufferMesh)
                return "Memory Buffer";
            else {
                switch (position) {
                    case 0:
                        return "Image File";
                    case 1:
                        return "Memory Buffer";
                    case 2:
                        return "From List";
                }
            }*/
            return null;
        }
    }
}
