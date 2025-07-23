package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.viewport_tabs.viewport_as_camera_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Map.IMcMapCamera;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link VpAsCamera2DMapFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link VpAsCamera2DMapFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class VpAsCamera2DMapFragment extends Fragment implements FragmentWithObject{
    NumericEditTextLabel mScrollCameraDeltaX;
    NumericEditTextLabel mScrollCameraDeltaY;


    private OnFragmentInteractionListener mListener;
    private IMcMapCamera mCurrentObject;
    private View mRootView;
    private Button mSaveBttn;

    public VpAsCamera2DMapFragment() {
        // Required empty public constructor
    }

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment VpAsCamera2DMapFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static VpAsCamera2DMapFragment newInstance() {
        VpAsCamera2DMapFragment fragment = new VpAsCamera2DMapFragment();
        return fragment;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        mRootView=inflater.inflate(R.layout.fragment_vp_as_camera_2d_map, container, false);

        Funcs.SetObjectFromBundle(savedInstanceState, this );

        initViews();
        return mRootView;
    }

    private void initViews() {
        initEditTxt();
       initSaveBttn();
    }

    private void initSaveBttn() {
        mSaveBttn =(Button)mRootView.findViewById(R.id.map_2D_save_bttn);
        mSaveBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                try {
                    mCurrentObject.ScrollCamera(mScrollCameraDeltaX.getInt(),mScrollCameraDeltaY.getInt());
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "ScrollCamera");
                }catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void initEditTxt() {
        mScrollCameraDeltaX= (NumericEditTextLabel) mRootView.findViewById(R.id.map_2D_delta_x);
        mScrollCameraDeltaY= (NumericEditTextLabel) mRootView.findViewById(R.id.map_2D_delta_y);
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
 /*       if (context instanceof OnFragmentInteractionListener) {
            mListener = (OnFragmentInteractionListener) context;
        } else {
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
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mCurrentObject));
    }

    @Override
    public void setObject(Object obj) {
        if(obj instanceof IMcMapViewport)
            mCurrentObject = (IMcMapViewport) obj;
        else
            mCurrentObject = (IMcMapCamera) obj;
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
