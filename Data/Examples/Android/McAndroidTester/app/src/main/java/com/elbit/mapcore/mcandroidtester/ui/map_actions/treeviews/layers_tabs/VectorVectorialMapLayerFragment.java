package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.layers_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.elbit.mapcore.Interfaces.Map.IMcVectorMapLayer;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link VectorVectorialMapLayerFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link VectorVectorialMapLayerFragment#newInstance} factory method to
 * create an instance of this fragment.
 *
 */
public class VectorVectorialMapLayerFragment extends Fragment implements FragmentWithObject {

    private OnFragmentInteractionListener mListener;
    private IMcVectorMapLayer mVectorsMapLayer;

    private View mView;

    public static VectorVectorialMapLayerFragment newInstance() {
        VectorVectorialMapLayerFragment fragment = new VectorVectorialMapLayerFragment();
        return fragment;
    }

    public VectorVectorialMapLayerFragment() {
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
        mView = inflater.inflate(R.layout.fragment_vector_vectorial_map_layer, container, false);
        Funcs.SetObjectFromBundle(savedInstanceState, this );
       // InitControls();
       // InitControlsOperations();

        return mView;
    }

   /* private void InitControls()
    {
        mExtrusionHeightChangeSupportedCB = (CheckBox) mView.findViewById(R.id.static_objects_details_extrusion_height_change_supported_cb);
    }*/

   /* private void InitControlsOperations() {
        try {
            mExtrusionHeightChangeSupportedCB.setChecked(mVectorsMapLayer.IsExtrusionHeightChangeSupported());
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IsExtrusionHeightChangeSupported");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }*/

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
        } else {
//            throw new RuntimeException(context.toString()
//                    + " must implement OnFragmentInteractionListener");
        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    @Override
    public void setObject(Object obj) {
        mVectorsMapLayer = (IMcVectorMapLayer) obj;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
         outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mVectorsMapLayer));
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
