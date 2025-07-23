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
import com.elbit.mapcore.Interfaces.OverlayManager.IMcRectangleItem;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link ObjectPropertiesTabsRectangle.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link ObjectPropertiesTabsRectangle#newInstance} factory method to
 * create an instance of this fragment.
 */
public class ObjectPropertiesTabsRectangle extends Fragment {
    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;

    private OnFragmentInteractionListener mListener;
    private View mRootView;
    private SpinnerWithLabel mRectangleDefinition;
    private SpinnerWithLabel mCoordinateSys;
    private SpinnerWithLabel mRectType;
    private Button mSaveBttn;
    private NumericEditTextLabel mRadiusXNET;
    private NumericEditTextLabel mRadiusYNET;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment ObjectPropertiesTabsRectangle.
     */
    // TODO: Rename and change types and number of parameters
    public static ObjectPropertiesTabsRectangle newInstance(String param1, String param2) {
        ObjectPropertiesTabsRectangle fragment = new ObjectPropertiesTabsRectangle();
        Bundle args = new Bundle();
        args.putString(ARG_PARAM1, param1);
        args.putString(ARG_PARAM2, param2);
        fragment.setArguments(args);
        return fragment;
    }

    public ObjectPropertiesTabsRectangle() {
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
        mRootView = inflater.inflate(R.layout.fragment_object_properties_tabs_rectangle, container, false);
        initViews();
        return mRootView;
    }

    private void initViews() {
        inflateViews();
        initSaveBttn();
        initRectDefinition();
        initRectCoordSys();
        initRectType();
        initRadiusX();
        initRadiusY();
    }

    private void initRadiusY() {
        mRadiusYNET.setFloat(ObjectPropertiesBase.mRectRadiusY);
    }

    private void initRadiusX() {
        mRadiusXNET.setFloat(ObjectPropertiesBase.mRectRadiusX);
    }

    private void initRectType() {
        mRectType.setAdapter(new ArrayAdapter<>(getActivity(),android.R.layout.simple_spinner_item, IMcObjectSchemeItem.EGeometryType.values()));
        mRectType.setSelection(ObjectPropertiesBase.mRectangleType.getValue());
    }

    private void initRectCoordSys() {
        mCoordinateSys.setAdapter(new ArrayAdapter<>(getActivity(),android.R.layout.simple_spinner_item, EMcPointCoordSystem.values()));
        mCoordinateSys.setSelection(ObjectPropertiesBase.mRectangleCoordSys.getValue());
    }

    private void initSaveBttn() {
        mSaveBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveRectDefinition();
                saveRectCoordSys();
                saveRectType();
                saveRadiusX();
                saveRadiusY();
            }
        });
    }

    private void saveRadiusY() {
        ObjectPropertiesBase.mRectRadiusY = mRadiusYNET.getFloat();

    }

    private void saveRadiusX() {
        ObjectPropertiesBase.mRectRadiusX = mRadiusXNET.getFloat();
    }

    private void saveRectType() {
        ObjectPropertiesBase.mRectangleType = (IMcObjectSchemeItem.EGeometryType) mRectType.getSelectedItem();
    }

    private void saveRectCoordSys() {
        ObjectPropertiesBase.mRectangleCoordSys = (EMcPointCoordSystem) mCoordinateSys.getSelectedItem();
    }

    private void saveRectDefinition() {
        ObjectPropertiesBase.mRectangleDefinition = (IMcRectangleItem.ERectangleDefinition) mRectangleDefinition.getSelectedItem();
    }

    private void initRectDefinition() {
        mRectangleDefinition.setAdapter(new ArrayAdapter<>(getActivity(),android.R.layout.simple_spinner_item, IMcRectangleItem.ERectangleDefinition.values()));
        mRectangleDefinition.setSelection(ObjectPropertiesBase.mRectangleDefinition.getValue());
    }

    private void inflateViews() {
        mRectangleDefinition = (SpinnerWithLabel) mRootView.findViewById(R.id.rectangle_definition);
        mCoordinateSys = (SpinnerWithLabel) mRootView.findViewById(R.id.object_properties_rectangle_coordinate_sys_swl);
        mRectType = (SpinnerWithLabel) mRootView.findViewById(R.id.object_properties_rectangle_type_swl);
        mSaveBttn = (Button) mRootView.findViewById(R.id.object_properties_rect_save_bttn);
        mRadiusXNET = (NumericEditTextLabel) mRootView.findViewById(R.id.rect_radius_x_net);
        mRadiusYNET = (NumericEditTextLabel) mRootView.findViewById(R.id.rect_radius_y_net);
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
