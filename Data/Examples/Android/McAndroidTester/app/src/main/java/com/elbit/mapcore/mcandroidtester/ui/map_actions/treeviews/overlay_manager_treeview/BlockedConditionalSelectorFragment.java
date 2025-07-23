package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlay_manager_treeview;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.elbit.mapcore.mcandroidtester.R;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcBlockedConditionalSelector;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link BlockedConditionalSelectorFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link BlockedConditionalSelectorFragment#newInstance} factory method to
 * create an instance of this fragment.
 *
 */
public class BlockedConditionalSelectorFragment extends BaseConditionalSelectorFragment {

    private IMcBlockedConditionalSelector mCurrentCondSelector;
    private OnFragmentInteractionListener mListener;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     * @return A new instance of fragment BlockedConditionalSelectorFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static BlockedConditionalSelectorFragment newInstance(String param1, String param2) {
        BlockedConditionalSelectorFragment fragment = new BlockedConditionalSelectorFragment();
        return fragment;
    }
    public BlockedConditionalSelectorFragment() {
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
        mRootview=inflater.inflate(R.layout.fragment_blocked_conditional_selector, container, false);
        inflateViews();
        initViews();
        return mRootview;
    }

    @Override
    protected void initViews() {
        super.initViews();
    }

    @Override
    protected void inflateViews() {
        super.inflateViews();
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

    @Override
    public void setObject(Object obj) {
        mCurrentCondSelector=(IMcBlockedConditionalSelector)obj;
        super.setObject(obj);
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mCurrentCondSelector));
    }

}
