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
import android.widget.CheckBox;

import com.elbit.mapcore.Classes.OverlayManager.McMesh;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.General.ObjectRef;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcMesh;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcNativeMeshFile;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.IMeshTab;
import com.elbit.mapcore.mcandroidtester.managers.ObjectWorld.Manager_MCMeshes;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.MapFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.private_properties.MeshPropertyDialogFragment;
import com.elbit.mapcore.mcandroidtester.ui.objects.ObjectPropertiesBase;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.FileChooserEditTextLabel;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link NativeMeshFileFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link NativeMeshFileFragment#newInstance} factory method to
 * create an instance of this fragment.
 *
 */
public class NativeMeshFileFragment extends Fragment implements IMeshTab {

    private View mRootView;
    private OnFragmentInteractionListener mListener;
   // public IMcNativeMeshFile mNativeMeshFile;
    public FileChooserEditTextLabel mMeshFileFC;
    private IMcNativeMeshFile mCurrentMesh;
    private Button mCreateButton;
    private CheckBox mUseExisting;
    private MeshPropertyDialogFragment mMeshPropertyDialogFragment;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment NativeMeshFileFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static NativeMeshFileFragment newInstance() {
        NativeMeshFileFragment fragment = new NativeMeshFileFragment();
        return fragment;
    }
    public NativeMeshFileFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        mRootView = inflater.inflate(R.layout.fragment_native_mesh_file, container, false);;
        // Inflate the layout for this fragment
        initViews();
        return mRootView;
    }

    private void initViews() {
        inflateViews();
        if (mCurrentMesh != null)
            initMeshFile();
    }

    private void initMeshFile() {
       /* try {
            SMcFileSource sImageSource = ((IMcImageFileMesh) mCurrentMesh).GetImageFile();
            mImageFileFC.setDirPath(sImageSource.strFileName);
        } catch (MapCoreException McEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), McEx, "GetImageFile");
        } catch (Exception e) {
            e.printStackTrace();
        }*/
    }

    private void inflateViews() {
        mMeshFileFC = (FileChooserEditTextLabel) mRootView.findViewById(R.id.native_mesh_file_chooser);
        mUseExisting = (CheckBox) mRootView.findViewById(R.id.native_mesh_use_existing);
        mCreateButton = (Button) mRootView.findViewById(R.id.btn_create_native_mesh);

        mCreateButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            ObjectRef<Boolean> existingUsed = new ObjectRef<>();
                            IMcNativeMeshFile nativeMeshFile = IMcNativeMeshFile.Static.Create(mMeshFileFC.getDirPath(), mUseExisting.isChecked(), existingUsed);
                            ObjectPropertiesBase.mMesh = nativeMeshFile;
                            mCurrentMesh = nativeMeshFile;
                            if (!existingUsed.getValue()) {
                                Manager_MCMeshes.getInstance().addToDictionary(nativeMeshFile);
                            }
                            saveMesh();
                            goBackAfterCreate();
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcNativeMeshFile.Static.Create");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
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
                ObjectPropertiesBase.mMesh = mCurrentMesh;
                if (getActivity() instanceof MapsContainerActivity)
                    ((MapsContainerActivity) getActivity()).MeshTabsFragmentInteraction((McMesh) mCurrentMesh, objPropertiesTabTag);
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

        FragmentManager fragManager = parentFragment.getParentFragmentManager();
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
        }/* else {
            throw new RuntimeException(context.toString()
                    + " must implement OnFragmentInteractionListener");
        }*/
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    @Override
    public void setCurrentMesh(IMcMesh curMesh) {
        mCurrentMesh = (IMcNativeMeshFile) curMesh;
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
