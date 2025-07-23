package com.elbit.mapcore.mcandroidtester.ui.map.fragments;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.Toast;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.ui.adapters.RawLayerComponentAdapter;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.FileChooserEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SpinnerWithLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.WorldBoundingBox;

import java.util.ArrayList;

import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;
import com.elbit.mapcore.Structs.SMcBox;
import com.elbit.mapcore.Structs.SMcVector3D;

public class RawLayerComponentsFragment extends Fragment {

    private OnFragmentInteractionListener mListener;
    private View mRootView;
    private FileChooserEditTextLabel mCompDirFC;
    private SpinnerWithLabel mCompType;
    private WorldBoundingBox mWorldLimit;
    private ArrayList<IMcMapLayer.SComponentParams> mCompParamsList;
    private Button mAddComponentBttn;
    private ListView mComponentList;

    public static RawLayerComponentsFragment newInstance() {
        RawLayerComponentsFragment fragment = new RawLayerComponentsFragment();
        return fragment;
    }

    public RawLayerComponentsFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        mCompParamsList = new ArrayList<>();
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        mRootView = inflater.inflate(R.layout.fragment_raw_layer_add_component, container, false);
        initViews();
        return mRootView;
    }
    private void initViews() {
        inflateViews();
        initCompType();
        initWorldLimit();
        initAddComponentBttn();
        initComponentList();
        mCompParamsList = new ArrayList<>();
    }

    private void initAddComponentBttn() {
        mAddComponentBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                mCompParamsList.add(getNewComponent());
                initComponentList();
                getActivity().runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        Toast.makeText(getContext(), "Component added successfully", Toast.LENGTH_LONG).show();
                    }
                });
            }
        });
    }

    private IMcMapLayer.SComponentParams getNewComponent() {
        IMcMapLayer.SComponentParams curParams = new IMcMapLayer.SComponentParams();
        curParams.strName = mCompDirFC.getDirPath();
        curParams.eType = (IMcMapLayer.EComponentType) mCompType.getSelectedItem();
        curParams.WorldLimit = mWorldLimit.getWorldBoundingBox();
        return curParams;
    }

    public ArrayList<IMcMapLayer.SComponentParams> getmCompParamsList() {
        return mCompParamsList;
    }

    public void setmCompParamsList(ArrayList<IMcMapLayer.SComponentParams> mCompParamsList) {
        this.mCompParamsList = mCompParamsList;
    }

    private void initWorldLimit() {
        mWorldLimit.initWorldBoundingBox(new SMcBox(new SMcVector3D(0, 0, 0), new SMcVector3D(0, 0, 0)));
    }

    private void initCompType() {
        mCompType.setAdapter(new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, IMcMapLayer.EComponentType.values()));
        mCompType.setSelection(IMcMapLayer.EComponentType.ECT_FILE.getValue());
    }

    private void inflateViews() {
        mCompDirFC = (FileChooserEditTextLabel) mRootView.findViewById(R.id.raw_layers_directory_name_fc);
        mCompType = (SpinnerWithLabel) mRootView.findViewById(R.id.raw_layers_component_type_swl);
        mWorldLimit = (WorldBoundingBox) mRootView.findViewById(R.id.raw_layers_world_limit);
        mComponentList = (ListView) mRootView.findViewById(R.id.raw_layers_components_lv);

        mAddComponentBttn = (Button) mRootView.findViewById(R.id.raw_layers_add_component_bttn);

        mCompType.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                boolean is_file = ((IMcMapLayer.EComponentType) mCompType.getSelectedItem())== IMcMapLayer.EComponentType.ECT_FILE;
                mCompDirFC.setEnableFileChoosing(is_file);
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });
    }

    private void initComponentList()
    {
       // mComponentList.
        mComponentList.setAdapter(new RawLayerComponentAdapter(getContext(), R.layout.list_item, mCompParamsList));
        Funcs.setListViewHeightBasedOnChildren(mComponentList);
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
       /* if (context instanceof OnFragmentInteractionListener) {
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

    public interface OnFragmentInteractionListener {
        // TODO: Update argument type and name
        void onFragmentInteraction(Uri uri);
    }
}
