package com.elbit.mapcore.mcandroidtester.ui.objects.fragments.properties_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.ui.objects.ObjectPropertiesBase;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SpinnerWithLabel;

import com.elbit.mapcore.Enums.EMcPointCoordSystem;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link ObjectPropertiesTabsArrow.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link ObjectPropertiesTabsArrow#newInstance} factory method to
 * create an instance of this fragment.
 */
public class ObjectPropertiesTabsArrow extends Fragment {
    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;

    private OnFragmentInteractionListener mListener;
    private View mRootView;
    private Button mSaveButton;
    private NumericEditTextLabel mHeadAngle;
    private NumericEditTextLabel mHeadSize;
    private NumericEditTextLabel mGapSize;
    private SpinnerWithLabel mCoordinateSys;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment ObjectPropertiesTabsArrow.
     */
    // TODO: Rename and change types and number of parameters
    public static ObjectPropertiesTabsArrow newInstance(String param1, String param2) {
        ObjectPropertiesTabsArrow fragment = new ObjectPropertiesTabsArrow();
        Bundle args = new Bundle();
        args.putString(ARG_PARAM1, param1);
        args.putString(ARG_PARAM2, param2);
        fragment.setArguments(args);
        return fragment;
    }

    public ObjectPropertiesTabsArrow() {
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
        mRootView = inflater.inflate(R.layout.fragment_object_properties_tabs_arrow, container, false);
        initViews();
        return mRootView;
    }

    private void initViews() {
        inflateViews();
        initSaveBttn();
        initArrowCoordSys();
        initHeadAngle();
        initHeadSize();
        initGapSize();
    }

    private void initGapSize() {
        mGapSize.setFloat(ObjectPropertiesBase.mArrowGapSize);

    }

    private void initHeadSize() {
        mHeadSize.setFloat(ObjectPropertiesBase.mArrowHeadSize);

    }

    private void initHeadAngle() {
        mHeadAngle.setFloat(ObjectPropertiesBase.mArrowHeadAngle);
    }

    private void initSaveBttn() {
        mSaveButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveArrowCoordSys();
                saveHeadAngle();
                saveHeadSize();
                saveGapSize();
            }


        });
    }

    private void saveArrowCoordSys() {
        ObjectPropertiesBase.mArrowCoordSys = (EMcPointCoordSystem) mCoordinateSys.getSelectedItem();
    }

    private void initArrowCoordSys() {
        mCoordinateSys.setAdapter(new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, EMcPointCoordSystem.values()));
        mCoordinateSys.setSelection(ObjectPropertiesBase.mArrowCoordSys.getValue());
    }

    private void saveGapSize() {
        ObjectPropertiesBase.mArrowGapSize = mGapSize.getFloat();

    }

    private void saveHeadSize() {
        ObjectPropertiesBase.mArrowHeadSize = mHeadSize.getFloat();

    }

    private void saveHeadAngle() {
        ObjectPropertiesBase.mArrowHeadAngle = mHeadAngle.getFloat();
    }

    private void inflateViews() {
        mSaveButton = (Button) mRootView.findViewById(R.id.object_properties_arrow_save_bttn);
        mCoordinateSys = (SpinnerWithLabel) mRootView.findViewById(R.id.object_properties_arrow_coord_sys);
        mHeadAngle = (NumericEditTextLabel) mRootView.findViewById(R.id.arrow_head_angle_net);
        mHeadSize = (NumericEditTextLabel) mRootView.findViewById(R.id.arrow_head_size_net);
        mGapSize = (NumericEditTextLabel) mRootView.findViewById(R.id.arrow_gap_size_net);
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
}
