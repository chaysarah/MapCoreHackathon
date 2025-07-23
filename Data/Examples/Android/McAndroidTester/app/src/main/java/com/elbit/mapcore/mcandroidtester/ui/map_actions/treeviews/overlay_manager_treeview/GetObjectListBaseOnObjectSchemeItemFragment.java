package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlay_manager_treeview;

import android.app.Dialog;
import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.annotation.NonNull;
import androidx.fragment.app.DialogFragment;
import androidx.fragment.app.Fragment;
import androidx.appcompat.app.AlertDialog;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AbsListView;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.Toast;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.interfaces.IObjectListBaseOnObjectSchemeItemFragmentCallback;
import com.elbit.mapcore.mcandroidtester.ui.adapters.HashMapAdapter;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.BaseApplication;
import com.elbit.mapcore.mcandroidtester.utils.Consts;

import java.util.HashMap;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeItem;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link GetObjectListBaseOnObjectSchemeItemFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link GetObjectListBaseOnObjectSchemeItemFragment#newInstance} factory method to
 * create an instance of this fragment.
 *
 */
public class GetObjectListBaseOnObjectSchemeItemFragment extends DialogFragment implements FragmentWithObject {

    private IMcObjectSchemeItem mObjectSchemeItem;
    private OnFragmentInteractionListener mListener;
    private ListView mObjectsLV;
    private HashMapAdapter mObjectsAdapter;
    IMcObject[] mObjectsArr;
    IMcObject mSelectedObjects;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     ** @return A new instance of fragment GetObjectListBaseOnObjectSchemeItemFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static GetObjectListBaseOnObjectSchemeItemFragment newInstance(String param1, String param2) {
        GetObjectListBaseOnObjectSchemeItemFragment fragment = new GetObjectListBaseOnObjectSchemeItemFragment();
        return fragment;
    }
    public GetObjectListBaseOnObjectSchemeItemFragment() {
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
        return inflater.inflate(R.layout.fragment_get_object_list_base_on_object_scheme_item, container, false);
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        AMCTSerializableObject object = new AMCTSerializableObject(mObjectSchemeItem);
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
        if (context instanceof OnFragmentInteractionListener) {
            mListener = (OnFragmentInteractionListener) context;
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

    @Override
    public void setObject(Object obj) {
        mObjectSchemeItem = (IMcObjectSchemeItem) obj;
    }

    @NonNull
    @Override
    public Dialog onCreateDialog(Bundle savedInstanceState) {

        Funcs.SetObjectFromBundle(savedInstanceState, this );
        View v = getActivity().getLayoutInflater().inflate(R.layout.fragment_get_object_list_base_on_object_scheme_item, null);
        mObjectsLV = (ListView) v.findViewById(R.id.om_objects_of_item_lv);

        try {
                mObjectsArr = mObjectSchemeItem.GetScheme().GetObjects();
                if (mObjectsArr != null && mObjectsArr.length > 0) {
                    HashMap<Object, Integer> hashMapSchemes = new HashMap<Object, Integer>();
                    for (Object object : mObjectsArr) {
                        hashMapSchemes.put(object, object.hashCode());
                    }
                    mObjectsAdapter = new HashMapAdapter(null, hashMapSchemes, Consts.ListType.SINGLE_CHECK);
                    mObjectsLV.setChoiceMode(ListView.CHOICE_MODE_SINGLE);
                    mObjectsLV.setAdapter(mObjectsAdapter);

                    mObjectsLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                        @Override
                        public void onItemClick(AdapterView<?> adapterView, View view, int index, long l) {
                            mSelectedObjects = mObjectsArr[index];
                        }
                    });
                }
            } catch (MapCoreException McEx) {
                McEx.printStackTrace();
                AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), McEx, "IMcOverlayManager.GetObjectSchemes/IsObjectSchemeLocked");
            } catch (Exception ex) {
                ex.printStackTrace();
            }


        mObjectsLV.setChoiceMode(AbsListView.CHOICE_MODE_MULTIPLE);
        mObjectsLV.deferNotifyDataSetChanged();
        v.findViewById(R.id.btn_objects_of_item_ok).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(mSelectedObjects != null)
                {
                    IObjectListBaseOnObjectSchemeItemFragmentCallback callback = (IObjectListBaseOnObjectSchemeItemFragmentCallback) getTargetFragment();
                    callback.callbackObjectListBaseOnObjectSchemeItem(mSelectedObjects,mObjectSchemeItem);
                    dismiss();
                }
                else
                {
                    Toast.makeText(getActivity(), "Please select an object from the list", Toast.LENGTH_SHORT).show();
                }
            }
        });

        AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());
        builder.setView(v);

        return builder.create();
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
