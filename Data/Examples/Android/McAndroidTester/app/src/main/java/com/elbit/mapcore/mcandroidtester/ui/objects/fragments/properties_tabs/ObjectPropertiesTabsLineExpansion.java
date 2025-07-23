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
 * {@link ObjectPropertiesTabsLineExpansion.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link ObjectPropertiesTabsLineExpansion#newInstance} factory method to
 * create an instance of this fragment.
 *
 */
public class ObjectPropertiesTabsLineExpansion extends Fragment {
    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;

    private OnFragmentInteractionListener mListener;
    private View mRootView;
    private SpinnerWithLabel mLineExpansionType;
    private NumericEditTextLabel mRadius;
    private Button mSaveBttn;
    private SpinnerWithLabel mCoordinateSys;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment ObjectPropertiesTabsLineExpansion.
     */
    // TODO: Rename and change types and number of parameters
    public static ObjectPropertiesTabsLineExpansion newInstance(String param1, String param2) {
        ObjectPropertiesTabsLineExpansion fragment = new ObjectPropertiesTabsLineExpansion();
        Bundle args = new Bundle();
        args.putString(ARG_PARAM1, param1);
        args.putString(ARG_PARAM2, param2);
        fragment.setArguments(args);
        return fragment;
    }
    public ObjectPropertiesTabsLineExpansion() {
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
        mRootView=inflater.inflate(R.layout.fragment_object_properties_tabs_line_expansion, container, false);
        initViews();
        return mRootView;
    }

    private void initViews() {
        inflateViews();
        initSaveBttn();
        initLineExpansionCoordSys();
        initLineExpansionType();
        initLineExpansionRadius();
    }

    private void initLineExpansionCoordSys() {
        mCoordinateSys.setAdapter(new ArrayAdapter<>(getActivity(),android.R.layout.simple_spinner_item, EMcPointCoordSystem.values()));
        mCoordinateSys.setSelection(ObjectPropertiesBase.mLineExpansionCoordinateSystem.getValue());
    }

    private void initLineExpansionRadius() {
        mRadius.setFloat(ObjectPropertiesBase.mLineExpansionRadius);
    }

    private void initLineExpansionType() {
        mLineExpansionType.setAdapter(new ArrayAdapter<>(getActivity(),android.R.layout.simple_spinner_item, IMcObjectSchemeItem.EGeometryType.values()));
        mLineExpansionType.setSelection(ObjectPropertiesBase.mLineExpansionType.getValue());
    }

    private void initSaveBttn() {
        mSaveBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveLineExpansionCoordSys();
                saveLineExpansionType();
                saveLineExpansionRadius();
            }
        });
    }

    private void saveLineExpansionCoordSys() {
        ObjectPropertiesBase.mLineExpansionCoordinateSystem= (EMcPointCoordSystem) mCoordinateSys.getSelectedItem();
    }

    private void saveLineExpansionType() {
        ObjectPropertiesBase.mLineExpansionType= (IMcObjectSchemeItem.EGeometryType) mLineExpansionType.getSelectedItem();
    }

    private void saveLineExpansionRadius() {
        ObjectPropertiesBase.mLineExpansionRadius=mRadius.getFloat();
    }

    private void inflateViews() {
        mSaveBttn=(Button)mRootView.findViewById(R.id.object_properties_line_expansion_save_bttn);
        mCoordinateSys=(SpinnerWithLabel)mRootView.findViewById(R.id.line_expansion_coord_sys_swl);
        mLineExpansionType=(SpinnerWithLabel)mRootView.findViewById(R.id.line_expansion_type_swl);
        mRadius=(NumericEditTextLabel)mRootView.findViewById(R.id.line_expansion_radius_net);
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
