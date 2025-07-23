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
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_MCMapHeightLines;
import com.elbit.mapcore.mcandroidtester.ui.adapters.AMCTArrayAdapter;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.terrain_layers_treeview.LayersListFragment;

import java.util.Arrays;

import com.elbit.mapcore.Interfaces.Map.IMcMapHeightLines;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

/**
 * Created by tc97803 on 07/02/2018.
 */

public class HeightLinesListFragment extends DialogFragment implements FragmentWithObject{
    private IMcMapViewport mParentViewport;
    private IMcMapHeightLines mSelectedHeightLines;
    private ListView mHeightLinessLV;
    private AMCTArrayAdapter mHeightLinessAdapter;
    private HeightLinesListFragment.OnFragmentInteractionListener mListener;

    public static HeightLinesListFragment newInstance() {
        HeightLinesListFragment fragment = new HeightLinesListFragment();
        return fragment;
    }

    public HeightLinesListFragment() {
        // Required empty public constructor
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
        View inflaterView = inflater.inflate(R.layout.fragment_heightlines_list, container, false);
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
            mListener = (HeightLinesListFragment.OnFragmentInteractionListener) context;
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
        View view = getActivity().getLayoutInflater().inflate(R.layout.fragment_heightlines_list, null);
        mHeightLinessLV = (ListView) (view.findViewById(R.id.lv_heightlines));
        mHeightLinessLV.setItemsCanFocus(false);
        try {
            mHeightLinessAdapter = new AMCTArrayAdapter(getActivity(),
                    R.layout.radio_bttn_list_item,
                    Arrays.asList(Manager_MCMapHeightLines.getInstance().getAllParams().keySet().toArray()));
        } catch (Exception e) {
            e.printStackTrace();
        }
        mHeightLinessLV.setAdapter(mHeightLinessAdapter);
        mHeightLinessLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                mSelectedHeightLines = (IMcMapHeightLines) mHeightLinessLV.getAdapter().getItem(position);
            }
        });

        mHeightLinessLV.setChoiceMode(AbsListView.CHOICE_MODE_SINGLE);
        mHeightLinessLV.deferNotifyDataSetChanged();

        Button okBtn = (Button) (view.findViewById(R.id.btnSelectHeightLines));
        okBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                ISelectHeightLinesCallback callback = (ISelectHeightLinesCallback) getTargetFragment();
                callback.callbackSelectHeightLines(mParentViewport, mSelectedHeightLines);
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
