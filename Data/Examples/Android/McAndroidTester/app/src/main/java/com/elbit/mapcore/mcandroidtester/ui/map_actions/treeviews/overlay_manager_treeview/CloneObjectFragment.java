package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlay_manager_treeview;

import android.app.Dialog;
import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.annotation.NonNull;
import androidx.fragment.app.DialogFragment;
import androidx.appcompat.app.AlertDialog;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.CheckBox;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

/**
 * Created by tc97803 on 07/02/2018.
 */

public class CloneObjectFragment extends DialogFragment implements FragmentWithObject{
    private IMcObject mCurrentObject;
    private CloneObjectFragment.OnFragmentInteractionListener mListener;
    private CheckBox mCloneObjectSchemeCB;
    private CheckBox mCloneRelativeToOrientationCB;

    public CloneObjectFragment() {
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
        View inflaterView = inflater.inflate(R.layout.fragment_clone_object, container, false);
        return inflaterView;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        AMCTSerializableObject object = new AMCTSerializableObject(mCurrentObject);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, object);
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
        if (context instanceof CloneObjectFragment.OnFragmentInteractionListener) {
            mListener = (CloneObjectFragment.OnFragmentInteractionListener) context;
        } else {
            //throw new RuntimeException(context.toString()
            //      + " must implement OnFragmentInteractionListener");
        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    @NonNull
    @Override
    public Dialog onCreateDialog(Bundle savedInstanceState) {

       Funcs.SetObjectFromBundle(savedInstanceState, this );

        View view = getActivity().getLayoutInflater().inflate(R.layout.fragment_clone_object, null);
        mCloneObjectSchemeCB = (CheckBox) view.findViewById(R.id.clone_object_clone_object_scheme);
        mCloneRelativeToOrientationCB = (CheckBox) view.findViewById(R.id.clone_object_clone_location_points);

        Button okBtn = (Button) (view.findViewById(R.id.btn_clone_object));
        okBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                final boolean bIsCloneObjectScheme = mCloneObjectSchemeCB.isChecked();
                final boolean bIsCloneRelativeToOrientation = mCloneRelativeToOrientationCB.isChecked();

                Funcs.runMapCoreFunc(new Runnable() {
                                         @Override
                                         public void run() {
                                             try {
                                                 mCurrentObject.Clone(mCurrentObject.GetOverlay(), bIsCloneObjectScheme, bIsCloneRelativeToOrientation);
                                                 getActivity().runOnUiThread(new Runnable() {
                                                     @Override
                                                     public void run() {
                                                         ICloneObjectCallback callback = (ICloneObjectCallback) getTargetFragment();
                                                         callback.CloneObjectCallback();
                                                         dismiss();
                                                     }
                                                 });
                                             } catch (MapCoreException e) {
                                                 AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "IMcOverlay.Remove");
                                                 e.printStackTrace();
                                             } catch (Exception e) {
                                                 e.printStackTrace();
                                             }
                                         }
                                     }
                );
            }
        });

        AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());
        builder.setView(view);

        return builder.create();
    }

    @Override
    public void setObject(Object obj) {
        mCurrentObject = (IMcObject) obj;
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
