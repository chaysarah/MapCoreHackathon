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
import android.widget.EditText;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.ui.objects.ObjectPropertiesBase;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SpinnerWithLabel;

import com.elbit.mapcore.Enums.EMcPointCoordSystem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeItem;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link ObjectPropertiesTabsEllipseFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link ObjectPropertiesTabsEllipseFragment#newInstance} factory method to
 * create an instance of this fragment.
 *
 */
public class ObjectPropertiesTabsEllipseFragment extends Fragment {
    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;

    private OnFragmentInteractionListener mListener;
    private View mRootView;
    private SpinnerWithLabel mEllipseDefinition;
    private Button mSaveBttn;
    private NumericEditTextLabel mStartAngle;
    private NumericEditTextLabel mEndAngle;
    private NumericEditTextLabel mInnerRadiusFactor;
    private SpinnerWithLabel mEllipseType;
    private SpinnerWithLabel mCoordinateSys;
    private EditText mRadiusX;
    private EditText mRadiusY;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment ObjectPropertiesTabsEllipseFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static ObjectPropertiesTabsEllipseFragment newInstance(String param1, String param2) {
        ObjectPropertiesTabsEllipseFragment fragment = new ObjectPropertiesTabsEllipseFragment();
        Bundle args = new Bundle();
        args.putString(ARG_PARAM1, param1);
        args.putString(ARG_PARAM2, param2);
        fragment.setArguments(args);
        return fragment;
    }
    public ObjectPropertiesTabsEllipseFragment() {
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
        mRootView=inflater.inflate(R.layout.fragment_object_properties_ellipse, container, false);
        initViews();
        return mRootView;
    }

    private void initViews() {
        inflateViews();
        initSaveBttn();
        initCoordinateSys();
        initEllipseType();
        initEllipseDefinition();
        initStartAngle();
        initEndAngle();
        initRadius();
        initInnerRadiusFactor();
    }

    private void initRadius() {
        mRadiusX.setText(String.valueOf(ObjectPropertiesBase.mEllipseRadiusX));
        mRadiusY.setText(String.valueOf(ObjectPropertiesBase.mEllipseRadiusY));
    }

    private void initEllipseType() {
        mEllipseType.setAdapter(new ArrayAdapter<>(getActivity(),android.R.layout.simple_spinner_item, IMcObjectSchemeItem.EGeometryType.values()));
        mEllipseType.setSelection(ObjectPropertiesBase.mEllipseType.ordinal()/*getValue()*/);
    }

    private void initCoordinateSys() {
        mCoordinateSys.setAdapter(new ArrayAdapter<>(getActivity(),android.R.layout.simple_spinner_item, EMcPointCoordSystem.values()));
        mCoordinateSys.setSelection(ObjectPropertiesBase.mEllipseCoordSys.ordinal()/*getValue()*/);
    }
    private void initInnerRadiusFactor() {
        mInnerRadiusFactor.setFloat(ObjectPropertiesBase.mEllipseInnerRadiusFactor);
    }

    private void saveEllipseType() {
        ObjectPropertiesBase.mEllipseType= (IMcObjectSchemeItem.EGeometryType) mEllipseType.getSelectedItem();
    }

    private void saveCoordinateSys() {
        ObjectPropertiesBase.mEllipseCoordSys= (EMcPointCoordSystem) mCoordinateSys.getSelectedItem();
    }
    private void initEndAngle() {
        mEndAngle.setFloat(ObjectPropertiesBase.mEllipseEndAngle);
    }

    private void initStartAngle() {
        mStartAngle.setFloat(ObjectPropertiesBase.mEllipseStartAngle);
    }

    private void initSaveBttn() {
        mSaveBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveEllipseType();
                saveCoordinateSys();
                saveEllipseDefinition();
                saveStartAngle();
                saveEndAngle();
                saveRadius();
                saveInnerRadiusFactor();
            }


        });
    }

    private void saveRadius() {
        ObjectPropertiesBase.mEllipseRadiusX=Float.valueOf(String.valueOf(mRadiusX.getText()));
        ObjectPropertiesBase.mEllipseRadiusY=Float.valueOf(String.valueOf(mRadiusY.getText()));
    }

    private void saveInnerRadiusFactor() {
        ObjectPropertiesBase.mEllipseInnerRadiusFactor=mInnerRadiusFactor.getFloat();
    }

    private void saveEndAngle() {
        ObjectPropertiesBase.mEllipseEndAngle=mEndAngle.getFloat();

    }

    private void saveStartAngle() {
        ObjectPropertiesBase.mEllipseStartAngle=mStartAngle.getFloat();
    }

    private void saveEllipseDefinition() {
        ObjectPropertiesBase.mEllipseDefinition= (IMcObjectSchemeItem.EEllipseDefinition) mEllipseDefinition.getSelectedItem();
    }

    private void initEllipseDefinition() {
        mEllipseDefinition.setAdapter(new ArrayAdapter<>(getActivity(),android.R.layout.simple_spinner_item, IMcObjectSchemeItem.EEllipseDefinition.values()));
        mEllipseDefinition.setSelection(ObjectPropertiesBase.mEllipseDefinition.ordinal()/*getValue()*/);
    }

    private void inflateViews() {
        mSaveBttn=(Button)mRootView.findViewById(R.id.object_properties_ellipse_save_bttn);
        mCoordinateSys=(SpinnerWithLabel)mRootView.findViewById(R.id.object_properties_ellipse_coordinate_sys_swl);
        mEllipseType=(SpinnerWithLabel)mRootView.findViewById(R.id.object_properties_ellipse_type_swl);
        mEllipseDefinition=(SpinnerWithLabel)mRootView.findViewById(R.id.ellipse_ellipse_definition_swl);
        mStartAngle=(NumericEditTextLabel)mRootView.findViewById(R.id.ellipse_start_angle_net);
        mEndAngle=(NumericEditTextLabel)mRootView.findViewById(R.id.ellipse_end_angle_net);
        mInnerRadiusFactor=(NumericEditTextLabel)mRootView.findViewById(R.id.ellipse_inner_radius_factor_net);
        mRadiusX=(EditText)mRootView.findViewById(R.id.ellipse_radius_x);
        mRadiusY=(EditText)mRootView.findViewById(R.id.ellipse_radius_y);

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
}
