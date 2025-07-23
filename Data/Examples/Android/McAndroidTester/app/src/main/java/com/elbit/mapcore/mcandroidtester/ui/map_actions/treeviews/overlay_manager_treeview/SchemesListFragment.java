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
import android.widget.AbsListView;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.ListView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.ui.adapters.AMCTArrayAdapter;

import java.util.ArrayList;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectScheme;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

/**
 * Created by tc97803 on 07/02/2018.
 */

public class SchemesListFragment extends DialogFragment implements FragmentWithObject{
    private IMcObjectScheme mSelectedScheme;
    private ListView mSchemesLV;
    private AMCTArrayAdapter mSchemesAdapter;
    private SchemesListFragment.OnFragmentInteractionListener mListener;
    private ArrayList<IMcObjectScheme> mSchemeList = new ArrayList<>();
    private IMcObject mCurrentObject;
    private CheckBox mKeepRelevantPropertiesCB;

    public SchemesListFragment() {
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
        View inflaterView = inflater.inflate(R.layout.fragment_schemes_list, container, false);
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
        if (context instanceof SchemesListFragment.OnFragmentInteractionListener) {
            mListener = (SchemesListFragment.OnFragmentInteractionListener) context;
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

        View view = getActivity().getLayoutInflater().inflate(R.layout.fragment_schemes_list, null);

        mKeepRelevantPropertiesCB = (CheckBox)view.findViewById(R.id.select_scheme_keep_relevant_properties);

        mSchemesLV = (ListView) (view.findViewById(R.id.lv_schemes));
        mSchemesLV.setItemsCanFocus(false);
        try {
            IMcObjectScheme[] Schemes = mCurrentObject.GetOverlayManager().GetObjectSchemes();
                for (IMcObjectScheme Scheme : Schemes)
                    mSchemeList.add(Scheme);
        } catch (MapCoreException e) {
        } catch (Exception e) {
        }

        try {
            mSchemesAdapter = new AMCTArrayAdapter(getActivity(),
                    R.layout.radio_bttn_list_item,
                    mSchemeList);
        } catch (Exception e) {
            e.printStackTrace();
        }
        mSchemesLV.setAdapter(mSchemesAdapter);
        mSchemesLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                mSelectedScheme = (IMcObjectScheme) mSchemesLV.getAdapter().getItem(position);
            }
        });

        mSchemesLV.setChoiceMode(AbsListView.CHOICE_MODE_SINGLE);
        mSchemesLV.deferNotifyDataSetChanged();

        Button okBtn = (Button) (view.findViewById(R.id.btn_select_scheme));
        okBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                ISelectSchemeCallback callback = (ISelectSchemeCallback) getTargetFragment();
                callback.SelectSchemeCallback(mSelectedScheme, mKeepRelevantPropertiesCB.isChecked(), mCurrentObject);
                dismiss();
            }
        });

        AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());
        builder.setView(view);

        return builder.create();
    }

    @Override
    public void setObject(Object obj) {
        mCurrentObject = (IMcObject)obj;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        AMCTSerializableObject object = new AMCTSerializableObject(mCurrentObject);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, object);
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
