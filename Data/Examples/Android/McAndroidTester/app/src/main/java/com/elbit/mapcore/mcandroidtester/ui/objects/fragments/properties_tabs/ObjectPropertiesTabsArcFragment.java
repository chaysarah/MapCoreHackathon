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
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeItem;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link ObjectPropertiesTabsArcFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link ObjectPropertiesTabsArcFragment#newInstance} factory method to
 * create an instance of this fragment.
 *
 */
public class ObjectPropertiesTabsArcFragment extends Fragment {
    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;

    private OnFragmentInteractionListener mListener;
    private View mRootview;
    private SpinnerWithLabel mEllipseDefinition;
    private NumericEditTextLabel mStartAngleNET;
    private NumericEditTextLabel mEndAngleNET;
    private Button mSaveBttn;
    private SpinnerWithLabel mCoordinateSys;
    private SpinnerWithLabel mEllipseType;
    private NumericEditTextLabel mRadiusXNET;
    private NumericEditTextLabel mRadiusYNET;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment ObjectPropertiesTabsArcFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static ObjectPropertiesTabsArcFragment newInstance(String param1, String param2) {
        ObjectPropertiesTabsArcFragment fragment = new ObjectPropertiesTabsArcFragment();
        Bundle args = new Bundle();
        args.putString(ARG_PARAM1, param1);
        args.putString(ARG_PARAM2, param2);
        fragment.setArguments(args);
        return fragment;
    }
    public ObjectPropertiesTabsArcFragment() {
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
        mRootview=inflater.inflate(R.layout.fragment_object_properties_tabs_arc, container, false);
        initViews();
        return mRootview;
    }

    private void initViews() {
        inflateViews();
        initSaveBttn();
        initCoordinateSys();
        initEllipseType();
        initEllipseDefinition();
        initStartAngle();
        initEndAngle();
        initRadiusX();
        initRadiusY();

    }

    private void initRadiusY() {
        mRadiusYNET.setFloat(ObjectPropertiesBase.mArcRadiusY);
    }

    private void initRadiusX() {
        mRadiusXNET.setFloat(ObjectPropertiesBase.mArcRadiusX);
    }

    private void initEllipseType() {
        mEllipseType.setAdapter(new ArrayAdapter<>(getActivity(),android.R.layout.simple_spinner_item, IMcObjectSchemeItem.EGeometryType.values()));
        mEllipseType.setSelection(ObjectPropertiesBase.mArcEllipseType.ordinal()/*getValue()*/);
    }

    private void initCoordinateSys() {
        mCoordinateSys.setAdapter(new ArrayAdapter<>(getActivity(),android.R.layout.simple_spinner_item,EMcPointCoordSystem.values()));
        mCoordinateSys.setSelection(ObjectPropertiesBase.mArcCoordSys.ordinal()/*getValue()*/);
    }

    private void initSaveBttn() {
        mSaveBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveCoordinateSys();
                saveEllipseDefinition();
                saveEllipseType();
                saveStartAngle();
                saveEndAngle();
                saveRadiusX();
                saveRadiusY();
            }
        });
    }

    private void saveRadiusY() {
        ObjectPropertiesBase.mArcRadiusY=mRadiusYNET.getFloat();

    }

    private void saveRadiusX() {
        ObjectPropertiesBase.mArcRadiusX=mRadiusXNET.getFloat();
    }

    private void saveEllipseType() {
        ObjectPropertiesBase.mArcEllipseType= (IMcObjectSchemeItem.EGeometryType) mEllipseType.getSelectedItem();
    }

    private void saveCoordinateSys() {
        ObjectPropertiesBase.mArcCoordSys= (EMcPointCoordSystem) mCoordinateSys.getSelectedItem();
    }

    private void saveEndAngle() {
        ObjectPropertiesBase.mArcEndAngle=mEndAngleNET.getFloat();

    }

    private void saveStartAngle() {
        ObjectPropertiesBase.mArcStartAngle=mStartAngleNET.getFloat();
    }

    private void saveEllipseDefinition() {
        ObjectPropertiesBase.mArcEllipseDefinition= (IMcObjectSchemeItem.EEllipseDefinition) mEllipseDefinition.getSelectedItem();
    }

    private void initEndAngle() {
        mEndAngleNET.setFloat(ObjectPropertiesBase.mArcEndAngle);
    }

    private void initStartAngle() {
        mStartAngleNET.setFloat(ObjectPropertiesBase.mArcStartAngle);
    }

    private void initEllipseDefinition() {
        mEllipseDefinition.setAdapter(new ArrayAdapter<>(getActivity(),android.R.layout.simple_spinner_item, IMcObjectSchemeItem.EEllipseDefinition.values()));
        mEllipseDefinition.setSelection(ObjectPropertiesBase.mArcEllipseDefinition.ordinal()/*getValue()*/);
    }

    private void inflateViews() {
        mCoordinateSys=(SpinnerWithLabel)mRootview.findViewById(R.id.object_properties_arc_coordinate_sys_swl);
        mEllipseType=(SpinnerWithLabel)mRootview.findViewById(R.id.object_properties_arc_ellipse_type_swl);
        mEllipseDefinition=(SpinnerWithLabel)mRootview.findViewById(R.id.arc_ellipse_definition_swl);
        mStartAngleNET=(NumericEditTextLabel)mRootview.findViewById(R.id.arc_start_angle_net);
        mEndAngleNET=(NumericEditTextLabel)mRootview.findViewById(R.id.arc_end_angle_net);
        mRadiusXNET=(NumericEditTextLabel)mRootview.findViewById(R.id.arc_radius_x_net);
        mRadiusYNET=(NumericEditTextLabel)mRootview.findViewById(R.id.arc_radius_y_net);
        mSaveBttn=(Button)mRootview.findViewById(R.id.object_properties_arc_save_bttn);

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
