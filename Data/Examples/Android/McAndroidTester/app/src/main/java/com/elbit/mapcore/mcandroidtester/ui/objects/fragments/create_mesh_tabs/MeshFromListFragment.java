package com.elbit.mapcore.mcandroidtester.ui.objects.fragments.create_mesh_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.ListView;

import com.elbit.mapcore.Classes.OverlayManager.McMesh;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcMesh;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.IMeshTab;
import com.elbit.mapcore.mcandroidtester.managers.ObjectWorld.Manager_MCMeshes;
import com.elbit.mapcore.mcandroidtester.ui.adapters.HashMapAdapter;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.MapFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.private_properties.MeshPropertyDialogFragment;
import com.elbit.mapcore.mcandroidtester.ui.objects.ObjectPropertiesBase;
import com.elbit.mapcore.mcandroidtester.utils.Consts;

import java.util.ArrayList;
import java.util.HashMap;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link MeshFromListFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link MeshFromListFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class MeshFromListFragment extends Fragment implements IMeshTab {

    private OnFragmentInteractionListener mListener;
    private View mRootview;
    public ListView mExistingMeshesLV;
    private ArrayList<IMcMesh> mLstExistingMeshesValues;
    private Button mSelectMeshButton;
    private IMcMesh mCurrentMesh;

    private MeshPropertyDialogFragment mMeshPropertyDialogFragment;


    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment MeshFromListFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static MeshFromListFragment newInstance() {
        MeshFromListFragment fragment = new MeshFromListFragment();
        return fragment;
    }

    public MeshFromListFragment() {
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
        mRootview = inflater.inflate(R.layout.fragment_mesh_from_list, container, false);
        initViews();
        return mRootview;
    }

    private void initViews() {
        inflateViews();
        initExistingMeshs();
    }

    private void initExistingMeshs() {
        mLstExistingMeshesValues = new ArrayList<>();
        HashMap<Object, Integer> Meshes = Manager_MCMeshes.getInstance().getMeshes();
        HashMapAdapter MeshsAdapter = new HashMapAdapter(getContext(), Meshes, Consts.ListType.SINGLE_CHECK);
        mExistingMeshesLV.setAdapter(MeshsAdapter);
        mExistingMeshesLV.setChoiceMode(ListView.CHOICE_MODE_SINGLE);
    }

    private void inflateViews() {
        mExistingMeshesLV = (ListView) mRootview.findViewById(R.id.mesh_from_list_existing_mesh_list);
        mSelectMeshButton = (Button) mRootview.findViewById(R.id.btn_select_mesh_from_list);

        mSelectMeshButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                int selectedTexturePos = mExistingMeshesLV.getCheckedItemPosition();
                mCurrentMesh = (IMcMesh) ((HashMapAdapter) mExistingMeshesLV.getAdapter()).getItem(selectedTexturePos).getKey();

                ObjectPropertiesBase.mMesh = (McMesh) mCurrentMesh;
                saveMesh();

                goBackAfterCreate();
            }
        });
    }

    private void saveMesh() {
        String objPropertiesTabTag = this.getParentFragment().getTag();
        switch (objPropertiesTabTag) {

          /*  case Consts.MeshFragmentsTags.MESH_FROM_MESH:
                ObjectPropertiesBase.mPicTexture = (McTexture) mCurTexture;
                break;*/
            case Consts.MeshFragmentsTags.MESH_FROM_CREATE_NEW_MESH:
                ObjectPropertiesBase.mMesh = (McMesh) mCurrentMesh;
                if (getActivity() instanceof MapsContainerActivity)
                    ((MapsContainerActivity) getActivity()).MeshTabsFragmentInteraction(mCurrentMesh, objPropertiesTabTag);
            case Consts.MeshFragmentsTags.MESH_FROM_PROPERTIES_LIST:
                if(mMeshPropertyDialogFragment != null)
                    mMeshPropertyDialogFragment.onMeshActionsCompleted(mCurrentMesh);
                break;
        }
    }

    private void goBackAfterCreate()
    {
        ((MapsContainerActivity) getActivity()).runOnUiThread(new Runnable() {
            @Override
            public void run() {
                goBack();
                ((MapsContainerActivity) getActivity()).getWindow().clearFlags(WindowManager.LayoutParams.FLAG_NOT_TOUCHABLE);
            }
        });
    }

    private void goBack() {
        if (this.getParentFragment().getParentFragment() != null) {
            returnToContainerFrag();
        } else {
            //if the texture tabs was open by hiding another fragment, show here the hidden frag
            if (getActivity() instanceof MapsContainerActivity)
                showHiddenMapFragment();
        }
    }

    private void showHiddenMapFragment() {
        // Get the FragmentManager of the parent fragment
        Fragment parentFragment = this.getParentFragment();
        if (parentFragment == null) {
            throw new IllegalStateException("Parent fragment is null.");
        }

        FragmentManager fragManager = parentFragment.getParentFragmentManager();
        FragmentTransaction transaction = fragManager.beginTransaction();

        // Ensure the parentFragment is in the same FragmentManager
        if (fragManager.findFragmentById(parentFragment.getId()) != null) {
            transaction.hide(parentFragment);
            transaction.addToBackStack("hide" + parentFragment.getClass().getSimpleName());
        }

        // Show the MapFragment by tag
        Fragment mapFragment = fragManager.findFragmentByTag(MapFragment.class.getSimpleName());
        if (mapFragment != null) {
            transaction.show(mapFragment);
            transaction.addToBackStack(MapFragment.class.getSimpleName());
        }

        transaction.commit();
    }


    private void returnToContainerFrag() {
        Fragment parentFragment = this.getParentFragment();
        if (parentFragment == null) {
            throw new IllegalStateException("Parent fragment is null.");
        }

        FragmentManager fragManager = this.getParentFragmentManager();
        FragmentTransaction transaction = fragManager.beginTransaction();

        // Ensure the parentFragment is in the same FragmentManager
        if (fragManager.findFragmentById(parentFragment.getId()) != null) {
            transaction.hide(parentFragment);
            transaction.addToBackStack("mesh");
        }

        transaction.commit();
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
        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    @Override
    public void setCurrentMesh(IMcMesh currentMesh) {
        mCurrentMesh = currentMesh;
    }

    public void setmMeshPropertyDialogFragment(MeshPropertyDialogFragment meshPropertyDialogFragment)
    {
        mMeshPropertyDialogFragment = meshPropertyDialogFragment;
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
