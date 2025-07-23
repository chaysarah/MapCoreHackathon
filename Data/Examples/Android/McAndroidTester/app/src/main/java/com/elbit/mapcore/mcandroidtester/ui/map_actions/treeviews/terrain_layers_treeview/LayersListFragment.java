package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.terrain_layers_treeview;

import android.app.Dialog;
import androidx.fragment.app.DialogFragment;
import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.annotation.NonNull;
import androidx.appcompat.app.AlertDialog;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AbsListView;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.CheckedTextView;
import android.widget.ListView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.ui.adapters.AMCTArrayAdapter;

import java.util.ArrayList;
import java.util.Arrays;

import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcMapTerrain;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

/**
 * Created by tc97803 on 15/01/2018.
 */

public class LayersListFragment extends DialogFragment implements FragmentWithObject {
    private IMcMapTerrain mMapTerrain;
    private IMcMapLayer mSelectedMapLayer;
    private ListView mLayersLV;
    private AMCTArrayAdapter mLayersAdapter;
    private OnFragmentInteractionListener mListener;
private ArrayList<IMcMapLayer> mSelectedLayers = new ArrayList<>();
    public static LayersListFragment newInstance() {
        LayersListFragment fragment = new LayersListFragment();
        return fragment;
    }

    public LayersListFragment() {
        // Required empty public constructor
    }

    @Override
    public void setObject(Object obj) {
        mMapTerrain = (IMcMapTerrain) obj;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mMapTerrain));
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        Funcs.SetObjectFromBundle(savedInstanceState, this );
        // Inflate the layout for this fragment
        View inflaterView = inflater.inflate(R.layout.fragment_layers_list, container, false);
        initLayersLV(inflaterView);
        return inflaterView;
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

    @NonNull
    @Override
    public Dialog onCreateDialog(Bundle savedInstanceState) {
        View view = getActivity().getLayoutInflater().inflate(R.layout.fragment_layers_list, null);
        mLayersLV = (ListView) (view.findViewById(R.id.lv_layers));
        mLayersLV.setItemsCanFocus(false);
        try {
            mLayersAdapter = new AMCTArrayAdapter(getActivity(), R.layout.checkable_list_item, Arrays.asList(mMapTerrain.GetLayers()));
        } catch (Exception e) {
            e.printStackTrace();
        }
        mLayersLV.setAdapter(mLayersAdapter);
        mLayersLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                IMcMapLayer mSelectedMapLayer = (IMcMapLayer) mLayersLV.getAdapter().getItem(position);
                if (((CheckedTextView) view).isChecked())
                    mSelectedLayers.add(mSelectedMapLayer);
                 else
                    mSelectedLayers.remove(mSelectedMapLayer);
            }
        });

        mLayersLV.setChoiceMode(AbsListView.CHOICE_MODE_MULTIPLE);
        mLayersLV.deferNotifyDataSetChanged();

        Button okBtn = (Button) (view.findViewById(R.id.btnSelectLayer));
        okBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                ISelectLayersCallback callback = (ISelectLayersCallback) getTargetFragment();
                callback.callbackSelectLayers(mMapTerrain, mSelectedLayers);
                dismiss();
            }
        });

        AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());
        builder.setView(view);

        return builder.create();
    }

    private void initLayersLV(View inflaterView) {

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
