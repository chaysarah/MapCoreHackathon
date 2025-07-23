package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.layers_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;

import com.elbit.mapcore.Interfaces.Map.IMc3DModelMapLayer;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link StaticObjects3DModelTabsFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link StaticObjects3DModelTabsFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class StaticObjects3DModelTabsFragment extends StaticObjectsTabsFragment {

    private OnFragmentInteractionListener mListener;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment StaticObjectsTabsFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static StaticObjects3DModelTabsFragment newInstance() {
        StaticObjects3DModelTabsFragment fragment = new StaticObjects3DModelTabsFragment();
        return fragment;
    }

    public StaticObjects3DModelTabsFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        Funcs.SetObjectFromBundle(savedInstanceState, this );
    }

    @Override
    protected SectionsPagerAdapter getSectionPagerAdapter() {
        return new StaticObjects3DModelAdapter(super.getChildFragmentManager());
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
        }
       /* else {
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
        mMapLayer = (IMc3DModelMapLayer) obj;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mMapLayer));
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

    public class StaticObjects3DModelAdapter extends StaticObjectsAdapter {

        public StaticObjects3DModelAdapter(FragmentManager fm) {
            super(fm);
        }

        @Override
        public Fragment getItem(int position) {
            switch (position) {
                case 2:
                    StaticObjects3DModelMapLayerFragment staticObjects3DModelMapLayerFragment = StaticObjects3DModelMapLayerFragment.newInstance();
                    staticObjects3DModelMapLayerFragment.setObject(mMapLayer);
                    return staticObjects3DModelMapLayerFragment;
                default:
                    return super.getItem(position);
            }
        }

        @Override
        public int getCount() {
            return 3;
        }

        @Override
        public CharSequence getPageTitle(int position) {
            switch (position) {
                case 2:
                    return "3D Model";
                default:
                    return super.getPageTitle(position);
            }
        }
    }

}
