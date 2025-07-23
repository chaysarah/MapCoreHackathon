package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlay_manager_treeview;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;

import com.elbit.mapcore.Enums.EMcPointCoordSystem;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.General.ObjectRef;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectLocation;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectScheme;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SpinnerWithLabel;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link NewObjectLocationFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link NewObjectLocationFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class NewObjectLocationFragment extends Fragment {

    private OnFragmentInteractionListener mListener;
    private View mRootView;
    private CheckBox mLocationRelativeToDTMCB;
    private SpinnerWithLabel mLocationCoordSysSWL;
    private Button mSaveChangesBttn;
    private IMcObjectScheme mSelectedScheme;
    private NumericEditTextLabel mInsertAtIndexET;
    private NumericEditTextLabel mLocationIndexET;
    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment LocationPointsFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static NewObjectLocationFragment newInstance() {
        NewObjectLocationFragment fragment = new NewObjectLocationFragment();
        return fragment;
    }

    public NewObjectLocationFragment() {
        // Required empty public constructor
    }

    public void SetSelectedScheme(IMcObjectScheme selectedScheme)
    {
        mSelectedScheme = selectedScheme;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        mRootView = inflater.inflate(R.layout.fragment_new_object_location, container, false);
        inflateViews();
        initViews();
        return mRootView;
    }

    private void initViews() {
        initLocationCoordSys();
        initSaveBttn();
    }

    private void initSaveBttn() {
        mSaveChangesBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            ObjectRef<IMcObjectLocation> objectLocationObjectRef = new ObjectRef<>();
                            ObjectRef<Integer> locationIndex = new ObjectRef<>();
                            mSelectedScheme.AddObjectLocation(objectLocationObjectRef,
                                    (EMcPointCoordSystem) mLocationCoordSysSWL.getSelectedItem(),
                                    mLocationRelativeToDTMCB.isChecked(),
                                    locationIndex,
                                    mInsertAtIndexET.getUInt());
                            final int finalLocationIndex =  locationIndex.getValue();
                            getActivity().runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    mLocationIndexET.setUInt(finalLocationIndex);
                                }
                            });

                        } catch (MapCoreException mcEx) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "IMcObjectScheme.AddObjectLocation");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });
    }

    private void initLocationCoordSys() {
        mLocationCoordSysSWL.setAdapter(new ArrayAdapter<>(getContext(), android.R.layout.simple_spinner_item, EMcPointCoordSystem.values()));
        mLocationCoordSysSWL.setSelection(EMcPointCoordSystem.EPCS_WORLD.getValue());
    }

    private void inflateViews() {
        mLocationRelativeToDTMCB = (CheckBox) mRootView.findViewById(R.id.new_object_location_relative_to_dtm_cb);
        mLocationCoordSysSWL = (SpinnerWithLabel) mRootView.findViewById(R.id.new_object_location_coordinate_system);
        mInsertAtIndexET = (NumericEditTextLabel) mRootView.findViewById(R.id.new_object_location_insert_at_index);
        mLocationIndexET = (NumericEditTextLabel) mRootView.findViewById(R.id.new_object_location_location_index);
        mSaveChangesBttn = (Button) mRootView.findViewById(R.id.new_object_location_save_changes_bttn);
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
        if (context instanceof MapsContainerActivity)
            ((MapsContainerActivity) context).mCurFragmentTag = NewObjectLocationFragment.class.getSimpleName();
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
