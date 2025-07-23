package com.elbit.mapcore.mcandroidtester.ui.objects.fragments.create_texture_tabs;

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
import com.elbit.mapcore.mcandroidtester.interfaces.ITextureTab;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcImageFileTexture;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcMemoryBufferTexture;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcTexture;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.private_properties.TexturePropertyDialogFragment;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link CreateTextureTabsFragment.OnTextureTabsFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link CreateTextureTabsFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class CreateTextureTabsFragment extends Fragment implements ITextureTab {

    private int TABS_NUN;

    private SectionsPagerAdapter mSectionsPagerAdapter;
    private ViewPager mViewPager;
    private OnTextureTabsFragmentInteractionListener mCreateTextureTabsListener;
    private IMcTexture mCurrentTexture;
    private String mTextureName;
    public IMcTexture getmCurrentTexture() {
        return mCurrentTexture;
    }

    private TexturePropertyDialogFragment mTexturePropertyDialogFragment;
    public void setmTexturePropertyDialogFragment(TexturePropertyDialogFragment TexturePropertyDialogFragment)
    {
        mTexturePropertyDialogFragment = TexturePropertyDialogFragment;
    }

    // TODO: Rename and change types and number of parameters
    public static CreateTextureTabsFragment newInstance() {
        CreateTextureTabsFragment fragment = new CreateTextureTabsFragment();
        return fragment;
    }

    public CreateTextureTabsFragment() {
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
        View view = inflater.inflate(R.layout.fragment_create_texture_tabs, container, false);
        initTabs(view);
        return view;
    }

    public void initTabs(View rootView) {
        (getActivity().findViewById(R.id.maps_container_rl)).setPadding(0, 0, 0, 0);
        mSectionsPagerAdapter = new SectionsPagerAdapter(getChildFragmentManager());
        mViewPager = (ViewPager) rootView.findViewById(R.id.create_texture_container);
        mViewPager.setAdapter(mSectionsPagerAdapter);
        mViewPager.setOffscreenPageLimit(TABS_NUN);
        TabLayout tabLayout = (TabLayout) rootView.findViewById(R.id.create_texture_tabs);
        tabLayout.setupWithViewPager(mViewPager);
        mSectionsPagerAdapter.notifyDataSetChanged();
    }


    @Override
    public void onAttach(Context context) {
        super.onAttach(context);
        if (context instanceof OnTextureTabsFragmentInteractionListener) {
            mCreateTextureTabsListener = (OnTextureTabsFragmentInteractionListener) context;
        } else {
            //throw new RuntimeException(context.toString()
            //        + " must implement OnFragmentInteractionListener");
        }
        if (context instanceof MapsContainerActivity)
            ((MapsContainerActivity) context).mCurFragmentTag = CreateTextureTabsFragment.class.getSimpleName();
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mCreateTextureTabsListener = null;
    }

    @Override
    public void setCurrentTexture(IMcTexture currentTexture) {
        mCurrentTexture = currentTexture;
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
    public interface OnTextureTabsFragmentInteractionListener {
        // TODO: Update argument type and name
        void TextureTabsFragmentInteraction(IMcTexture createdTexture);
    }

    public class SectionsPagerAdapter extends FragmentPagerAdapter {

        public SectionsPagerAdapter(FragmentManager fm) {
            super(fm);
        }

        @Override
        public Fragment getItem(int position) {
            if (mCurrentTexture == null) {
                switch (position) {
                    case 0:
                        TextureImageFileFragment textureImageFileFragment = TextureImageFileFragment.newInstance();
                        textureImageFileFragment.setmTexturePropertyDialogFragment(mTexturePropertyDialogFragment);
                        textureImageFileFragment.setCurrentTexture(mCurrentTexture);
                        return textureImageFileFragment;
                    case 1:
                        TextureMemoryBufferFragment textureMemoryBufferFragment = TextureMemoryBufferFragment.newInstance();
                        textureMemoryBufferFragment.setmTexturePropertyDialogFragment(mTexturePropertyDialogFragment);
                        textureMemoryBufferFragment.setCurrentTexture(mCurrentTexture);
                        return textureMemoryBufferFragment;
                    case 2:
                        TextureFromListFragment textureFromListFragment = TextureFromListFragment.newInstance();
                        textureFromListFragment.setmTexturePropertyDialogFragment(mTexturePropertyDialogFragment);
                        textureFromListFragment.setCurrentTexture(mCurrentTexture);
                        return textureFromListFragment;
                    default:
                        return null;
                }
            } else {
                if (mCurrentTexture instanceof IMcImageFileTexture) {
                    TextureImageFileFragment textureImageFileFragment = TextureImageFileFragment.newInstance();
                    textureImageFileFragment.setCurrentTexture(mCurrentTexture);
                    return textureImageFileFragment;
                }
                if (mCurrentTexture instanceof IMcMemoryBufferTexture) {
                    TextureMemoryBufferFragment textureMemoryBufferFragment = TextureMemoryBufferFragment.newInstance();
                    textureMemoryBufferFragment.setCurrentTexture(mCurrentTexture);
                    return textureMemoryBufferFragment;
                }
            }
            return null;
        }

        @Override
        public int getCount() {
            if(mCurrentTexture==null)
                TABS_NUN = 3;
            else if (mCurrentTexture instanceof IMcImageFileTexture)
                TABS_NUN = 1;
            else if (mCurrentTexture instanceof IMcMemoryBufferTexture)
                TABS_NUN = 1;

            return TABS_NUN;
        }

        @Override
        public CharSequence getPageTitle(int position) {
            if (mCurrentTexture instanceof IMcImageFileTexture)
                return "Image File";
            if (mCurrentTexture instanceof IMcMemoryBufferTexture)
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
            }
            return null;
        }
    }
}
