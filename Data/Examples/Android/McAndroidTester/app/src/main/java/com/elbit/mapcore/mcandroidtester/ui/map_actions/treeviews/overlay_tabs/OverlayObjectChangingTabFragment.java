package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlay_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ListView;
import android.widget.Spinner;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.ui.adapters.ObjectLocationPointAdapter;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlay;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link OverlayGeneralTabFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link OverlayGeneralTabFragment#newInstance} factory method to
 * create an instance of this fragment.
 *
 */
public class OverlayObjectChangingTabFragment extends Fragment implements FragmentWithObject {

    private OnFragmentInteractionListener mListener;
    private IMcOverlay mOverlay;
    private View mRootView;
    private ListView mObjectLocationPointLV;
    private Button mObjectLocationPointOkBttn;
    private Spinner mObjectSpinner;
    private Button mOpenPrivatePropertiesBttn;
    private ListView mObjPropertiesLV;
    private Button mObjPropertiesOkBttn;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment OverlayGeneralTabFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static OverlayObjectChangingTabFragment newInstance() {
        OverlayObjectChangingTabFragment fragment = new OverlayObjectChangingTabFragment();
        return fragment;
    }
    public OverlayObjectChangingTabFragment() {
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
        mRootView = inflater.inflate(R.layout.fragment_overlay_object_changing, container, false);
        initViews();
        return mRootView;
    }

    private void initObjectsLocationPoint() {
        initObjectsLocationPointLV();
        initObjectLocationPointOkBttn();
    }

    private void initObjectLocationPointOkBttn() {
        mObjectLocationPointOkBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

            }
        });
    }

    private void initObjectsLocationPointLV() {
        View header = getActivity().getLayoutInflater().inflate(R.layout.cv_object_location_point_row_header,null);
        mObjectLocationPointLV.addHeaderView(header);
        ObjectLocationPointAdapter objectLocationPointAdapter=new ObjectLocationPointAdapter(getActivity());
        mObjectLocationPointLV.setAdapter(objectLocationPointAdapter);
    }

    private void initObjectsPrivateProperties() {
        initObjectsPrivatePropertiesLV();
        initObjectsSpinner();
        initOpenPrivatePropertiesBttn();
    }

    private void initOpenPrivatePropertiesBttn() {
        mOpenPrivatePropertiesBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

            }
        });
    }

    private void initObjectsSpinner() {

    }

    private void initObjectsPrivatePropertiesLV() {

    }

    private void initViews() {
        inflateViews();
        initObjectsLocationPoint();
        initObjectsPrivateProperties();
    }

    private void inflateViews() {
        mObjectLocationPointLV=(ListView)mRootView.findViewById(R.id.overlay_object_changing_location_point_lv);
        mObjectLocationPointOkBttn=(Button)mRootView.findViewById(R.id.overlay_object_changing_location_point_ok_bttn);
        mObjectSpinner=(Spinner)mRootView.findViewById(R.id.overlay_object_changing_objects_spinner);
        mOpenPrivatePropertiesBttn=(Button)mRootView.findViewById(R.id.overlay_object_changing_private_properties_form_bttn);
        mObjPropertiesLV=(ListView)mRootView.findViewById(R.id.overlay_object_changing_objects_lv);
        mObjPropertiesOkBttn=(Button)mRootView.findViewById(R.id.overlay_object_changing_objects_ok_bttn);
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
        } /*else {
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
        mOverlay = (IMcOverlay) obj;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        if(mIsVisibleToUser)
            outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mOverlay));
    }
    private boolean mIsVisibleToUser;

    @Override
    public void setUserVisibleHint(boolean isVisibleToUser) {
        super.setUserVisibleHint(isVisibleToUser);
        mIsVisibleToUser = isVisibleToUser;
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
}
