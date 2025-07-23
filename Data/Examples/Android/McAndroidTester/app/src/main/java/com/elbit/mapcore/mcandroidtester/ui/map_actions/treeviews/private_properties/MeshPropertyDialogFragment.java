package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.private_properties;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.annotation.Nullable;
import androidx.fragment.app.DialogFragment;
import androidx.fragment.app.FragmentTransaction;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcMesh;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.ui.objects.fragments.create_mesh_tabs.CreateMeshTabsFragment;
import com.elbit.mapcore.mcandroidtester.utils.Consts;

public class MeshPropertyDialogFragment extends DialogFragment {
    private MeshPropertyDialogFragment.OnFragmentInteractionListener mListener;
    private View mRootView;
    private int mPropertyId;
    private IMcMesh mCurrentMesh;
    private TextView mPropertyIdTV;
    private Button mCreateMeshBttn;
    private Button mDeleteMeshBttn;
    private IMcMesh mCreatedMesh;
    private Button mOkBttn;

    public static MeshPropertyDialogFragment newInstance() {
        MeshPropertyDialogFragment fragment = new MeshPropertyDialogFragment();
        return fragment;
    }

    public MeshPropertyDialogFragment() {
        // Required empty public constructor
    }

    public void setCurrentMesh(int propertyId, IMcMesh Mesh) {
        mPropertyId = propertyId;
        mCurrentMesh = Mesh;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_mesh_property_dialog, container);
    }

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        inflateViews(view);
        initViews();
        view.findViewById(R.id.mesh_property_id);
    }

    private void inflateViews(View view) {
        mPropertyIdTV = (TextView) view.findViewById(R.id.mesh_property_id);
        mCreateMeshBttn = (Button) view.findViewById(R.id.mesh_property_create_mesh);
        mDeleteMeshBttn = (Button) view.findViewById(R.id.mesh_property_delete_mesh);

        mOkBttn = (Button) view.findViewById(R.id.mesh_property_dialog_ok_bttn);
    }

    private void initViews() {
        mOkBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                returnToUpdatePropertiesList();
                MeshPropertyDialogFragment.this.dismiss();
            }
        });
        mPropertyIdTV.setText(String.valueOf(mPropertyId));
        final MeshPropertyDialogFragment meshPropertyDialogFragment = this;
        mCreateMeshBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                CreateMeshTabsFragment createMeshTabsFragment = CreateMeshTabsFragment.newInstance();
                createMeshTabsFragment.setmMeshPropertyDialogFragment(meshPropertyDialogFragment);
                createMeshTabsFragment.setCurrentMesh(mCurrentMesh);
                FragmentTransaction transaction = getChildFragmentManager().beginTransaction().add(R.id.mesh_fragment_container, createMeshTabsFragment, Consts.MeshFragmentsTags.MESH_FROM_PROPERTIES_LIST);
                transaction.addToBackStack(Consts.MeshFragmentsTags.MESH_FROM_PROPERTIES_LIST);
                transaction.commit();
            }
        });

        mDeleteMeshBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                mCurrentMesh = null;
            }
        });
    }

    private void returnToUpdatePropertiesList() {
        PropertiesIdListFragment propertiesIdListFragment = (PropertiesIdListFragment) getFragmentManager().findFragmentByTag(PropertiesIdListFragment.class.getSimpleName());
        if (propertiesIdListFragment != null) {
            propertiesIdListFragment.updateMeshProperty(mPropertyId, mCreatedMesh);
        }

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
        if (context instanceof MeshPropertyDialogFragment.OnFragmentInteractionListener) {
            mListener = (MeshPropertyDialogFragment.OnFragmentInteractionListener) context;
        }/* else {
            throw new RuntimeException(conMesh.toString()
                    + " must implement OnFragmentInteractionListener");
        }*/
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    public void onMeshActionsCompleted(IMcMesh createdMesh) {
        mCreatedMesh = createdMesh;
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
