package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.viewport_terrain_treeview;

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
import android.widget.AbsListView;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.ListView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_MCMapGrid;
import com.elbit.mapcore.mcandroidtester.ui.adapters.AMCTArrayAdapter;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.terrain_layers_treeview.LayersListFragment;

import java.util.Arrays;
import com.elbit.mapcore.Interfaces.Map.IMcMapGrid;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;

/**
 * Created by tc97803 on 07/02/2018.
 */

public class GridsListFragment extends DialogFragment implements FragmentWithObject{
    private IMcMapViewport mParentViewport;
    private IMcMapGrid mSelectedGrid;
    private ListView mGridsLV;
    private AMCTArrayAdapter mGridsAdapter;
    private GridsListFragment.OnFragmentInteractionListener mListener;

    public GridsListFragment() {
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
        View inflaterView = inflater.inflate(R.layout.fragment_grids_list, container, false);
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
        if (context instanceof LayersListFragment.OnFragmentInteractionListener) {
            mListener = (GridsListFragment.OnFragmentInteractionListener) context;
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
        View view = getActivity().getLayoutInflater().inflate(R.layout.fragment_grids_list, null);
        mGridsLV = (ListView) (view.findViewById(R.id.lv_grids));
        mGridsLV.setItemsCanFocus(false);
        try {
            mGridsAdapter = new AMCTArrayAdapter(getActivity(),
                    R.layout.radio_bttn_list_item,
                    Arrays.asList(Manager_MCMapGrid.getInstance().getAllParams().keySet().toArray()));
        } catch (Exception e) {
            e.printStackTrace();
        }
        mGridsLV.setAdapter(mGridsAdapter);
        mGridsLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                mSelectedGrid = (IMcMapGrid) mGridsLV.getAdapter().getItem(position);
            }
        });

        mGridsLV.setChoiceMode(AbsListView.CHOICE_MODE_SINGLE);
        mGridsLV.deferNotifyDataSetChanged();

        Button okBtn = (Button) (view.findViewById(R.id.btnSelectGrid));
        okBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                ISelectGridCallback callback = (ISelectGridCallback) getTargetFragment();
                callback.callbackSelectGrid(mParentViewport, mSelectedGrid);
                dismiss();
            }
        });

        AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());
        builder.setView(view);

        return builder.create();
    }

    @Override
    public void setObject(Object obj) {
        mParentViewport = (IMcMapViewport) obj;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mParentViewport));
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
