package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.viewport_tabs.viewport_as_camera_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.CheckBox;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.ThreeDVector;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Map.IMcMapCamera;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link VpAsCameraAttachmentFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link VpAsCameraAttachmentFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class VpAsCameraAttachmentFragment extends Fragment implements FragmentWithObject {

    private OnFragmentInteractionListener mListener;
    private IMcMapCamera mCurrentObject;
    private View mRootView;
    private CheckBox mCameraAttachmentEnabledCB;
    private NumericEditTextLabel mAdditionalPitchET;
    private NumericEditTextLabel mAdditionalYawET;
    private NumericEditTextLabel mAdditionalRollET;

    private Button mSaveButton;
    private ThreeDVector mAttachmentCameraOffset3D;

    public VpAsCameraAttachmentFragment() {
        // Required empty public constructor
    }

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment VpAsCameraAttachmentFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static VpAsCameraAttachmentFragment newInstance() {
        VpAsCameraAttachmentFragment fragment = new VpAsCameraAttachmentFragment();
        return fragment;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        mRootView = inflater.inflate(R.layout.fragment_vp_as_camera_attachment, container, false);

        Funcs.SetObjectFromBundle(savedInstanceState, this );

        initViews();
        return mRootView;
    }

    private void initViews() {
        //inflating all the views (findViewById)
        initCheckBoxes();
        initEditTxts();
        init3DVectors();
        //init with values (eg taken from map core)
        initCameraAttachmentEnabled();
        loadCameraAttachmentTree();

        initSaveButton();
    }

    private void loadCameraAttachmentTree() {

    }

    private void init3DVectors() {
        mAttachmentCameraOffset3D = (ThreeDVector) mRootView.findViewById(R.id.attachment_offset_3D);
    }

    private void initEditTxts() {
        mAdditionalPitchET = (NumericEditTextLabel) mRootView.findViewById(R.id.attachment_additional_pitch);
        mAdditionalYawET = (NumericEditTextLabel) mRootView.findViewById(R.id.attachment_additional_yaw);
        mAdditionalRollET = (NumericEditTextLabel) mRootView.findViewById(R.id.attachment_additional_roll);

    }

    private void initCameraAttachmentEnabled() {
        try {
            mCameraAttachmentEnabledCB.setChecked(mCurrentObject.GetCameraAttachmentEnabled());
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetCameraAttachmentEnabled");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initSaveButton() {
        mSaveButton = (Button) mRootView.findViewById(R.id.attachment_save_bttn);
        mSaveButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveCameraAttachmentEnabled();
            }
        });
    }

    private void saveCameraAttachmentEnabled() {
        final boolean isEnabled = mCameraAttachmentEnabledCB.isChecked();
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    mCurrentObject.SetCameraAttachmentEnabled(isEnabled);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetCameraAttachmentEnabled");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });

    }

    private void initCheckBoxes() {
        mCameraAttachmentEnabledCB = (CheckBox) mRootView.findViewById(R.id.attachment_camera_attachment_enabled);
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
  /*      if (context instanceof OnFragmentInteractionListener) {
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
