package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlay_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.DialogFragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CheckedTextView;
import android.widget.EditText;
import android.widget.ListView;

import com.elbit.mapcore.Enums.EGeometry;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;
import com.elbit.mapcore.Structs.SMcFileInMemory;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.model.AMCTUserDataFactory;
import com.elbit.mapcore.mcandroidtester.ui.adapters.HashMapAdapter;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.DirectoryChooserDialog;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SaveParamsData;

import java.io.File;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.General.ObjectRef;
import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlay;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SpinnerWithLabel;


/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link OverlayObjectsFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link OverlayObjectsFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class OverlayObjectsFragment extends Fragment implements FragmentWithObject, IViewportsListContainSpecificOverlayFragmentCallback , ISaveObjectsAsRawVectorOverlayFragmentCallback{

   private OnFragmentInteractionListener mListener;
    private static IMcOverlay mOverlay;
    private View mRootView;
    private ListView mObjectsLV;
    private HashMapAdapter mObjectsAdapter;
    private Button mLoadFromFileBttn;
    private Button mLoadFromBufferBttn;
    private Button mSaveAllToFileBttn;
    private Button mSaveAllToBufferBttn;
    private Button mSaveToFileBttn;
    private Button mSaveToBufferBttn;
    private Button mObjectsClearBttn;

    private NumericEditTextLabel mSizeFactorForVectorMapLayerMinET;
    private NumericEditTextLabel mSizeFactorForVectorMapLayerMaxET;
    private Button mSaveAsNativeVectorBttn;
    private Button mSaveAllAsNativeVectorBttn;
    private Button mSaveAsRawVectorBttn;
    private Button mSaveAllAsRawVectorBttn;
    private EditText mSubItemsIdsET;
    private CheckBox mSubItemsIdsVisibleCB;
    private Button mSubItemsIdsGetBttn;
    private Button mSubItemsIdsSetBttn;
    private SpinnerWithLabel mStorgeFormatSWL;
    private Button mGetObjByIdBttn;
    private NumericEditTextLabel mGetObjByIdET;
    private List<IMcObject> lstSelectedObjects;

    private String mFilePath;
    private SaveParamsData cvSaveParamsData;

    private float mMinScale;
    private float mMaxScale;
    private IMcMapViewport mSelectedViewport;

    private IMcMapLayer.STilingScheme mTilingScheme;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment OverlayObjectsFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static OverlayObjectsFragment newInstance() {
        OverlayObjectsFragment fragment = new OverlayObjectsFragment();
        return fragment;
    }

    public OverlayObjectsFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        mRootView = inflater.inflate(R.layout.fragment_overlay_objects, container, false);
        initViews();
        return mRootView;
    }

    private void initViews() {
        inflateViews();
        initObjectsList();
        initObjListBttns();
        initSubItemsVisibility();
    }

    private void initObjListBttns() {
        initSaveAsNativeVectorBttn();
        initSaveAllAsNativeVectorBttn();
        initGtObjByIdBttn();
        initSaveAsRawVectorBttn();
        initSaveAllAsRawVectorBttn();

        mLoadFromFileBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                DirectoryChooserDialog directoryChooserDialog =
                        new DirectoryChooserDialog(getContext(),
                                new DirectoryChooserDialog.ChosenDirectoryListener() {
                                    @Override
                                    public void onChosenDir(String chosenDir) {
                                        loadObject(chosenDir);
                                    }
                                }, true);
                 directoryChooserDialog.chooseDirectory("");
            }
        });

        mLoadFromBufferBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            AMCTUserDataFactory UDF = new AMCTUserDataFactory();
                            final IMcObject[] objects = mOverlay.LoadObjects(MapsContainerActivity.overlayManagerBuffer, UDF);
                            getActivity().runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    addObjectsToList(objects);
                                }
                            });
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlay.LoadObjects");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });

        mSaveAllToBufferBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final IMcOverlayManager.ESavingVersionCompatibility savingVersionCompatibility = cvSaveParamsData.getSavingVersionCompatibility();
                final boolean saveUserData = cvSaveParamsData.getSaveUserData();
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            MapsContainerActivity.overlayManagerBuffer =
                                    mOverlay.SaveAllObjects(
                                            (IMcOverlayManager.EStorageFormat) mStorgeFormatSWL.getSelectedItem(),
                                            savingVersionCompatibility);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlay.SaveAllObjects");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });
        mSaveToBufferBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final boolean saveUserData = cvSaveParamsData.getSaveUserData();
                final IMcOverlayManager.ESavingVersionCompatibility savingVersionCompatibility = cvSaveParamsData.getSavingVersionCompatibility();
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            MapsContainerActivity.overlayManagerBuffer =
                                    mOverlay.SaveObjects(getSelectedObjects(),
                                            (IMcOverlayManager.EStorageFormat) mStorgeFormatSWL.getSelectedItem(),
                                            savingVersionCompatibility);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlay.SaveObjects");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });

        mSaveAllToFileBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveToFile(Consts.SAVE_ALL_FILE_REQUEST_CODE);
            }
        });

        mSaveToFileBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveToFile(Consts.SAVE_FILE_REQUEST_CODE);
            }
        });

        mObjectsClearBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                clearListItems();
            }
        });
    }

    private void clearSelection() {
        for (int i = 0; i < mObjectsAdapter.getCount(); i++) {
            mObjectsLV.setItemChecked(i, false);
        }
        lstSelectedObjects.clear();
    }

    private void clearListItems()
    {
        HashMapAdapter adapter = (HashMapAdapter)mObjectsLV.getAdapter();
        adapter.clearData();
        // refresh the View
        adapter.notifyDataSetChanged();
    }

    private void loadObject(final String ObjectFile) {

        final AMCTUserDataFactory UDF = new AMCTUserDataFactory();
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    final IMcObject[] objects = mOverlay.LoadObjects(ObjectFile,UDF);

                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            addObjectsToList(objects);
                        }
                    });
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlayManager.LoadObjects");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void addObjectsToList(IMcObject[] objects) {
        if(objects != null && objects.length > 0) {
            if (mObjectsAdapter == null)
                initObjectAdapter(objects);
            else {
                HashMap<Object, Integer> hashMapObjects = GetHashMapObjects(objects);
                if (hashMapObjects != null)
                    mObjectsAdapter.addItems(hashMapObjects);
            }
            initObjectsListWithAdapter();
        }
    }

    private void saveObject(final String objectFile,final int requestCode) {
        final boolean saveUserData = cvSaveParamsData.getSaveUserData();
        final IMcOverlayManager.ESavingVersionCompatibility savingVersionCompatibility = cvSaveParamsData.getSavingVersionCompatibility();
        final IMcOverlayManager.EStorageFormat eStorageFormat = Funcs.getEStorageFormatByFileExtension(objectFile);

        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    if (requestCode == Consts.SAVE_FILE_REQUEST_CODE) {
                        mOverlay.SaveObjects(
                                getSelectedObjects(),
                                objectFile,
                                eStorageFormat,
                                savingVersionCompatibility);
                    } else if (requestCode == Consts.SAVE_ALL_FILE_REQUEST_CODE)
                        mOverlay.SaveAllObjects(objectFile,
                                eStorageFormat,
                                savingVersionCompatibility);

                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlay.SaveAllObjects");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void saveToFile(final int requestCode) {

        if (requestCode == Consts.SAVE_FILE_REQUEST_CODE && lstSelectedObjects.size()== 0)
        {
            AlertMessages.ShowGenericMessage(getContext(), "Objects ", "There is no selected object s");
            return;
        }
            DirectoryChooserDialog directoryChooserDialog =
                new DirectoryChooserDialog(getContext(),
                        new DirectoryChooserDialog.ChosenDirectoryListener() {
                            @Override
                            public void onChosenDir(String chosenDir) {
                                saveObject(chosenDir, requestCode);
                            }
                        }, true, true);
        directoryChooserDialog.chooseDirectory("");
    }

    private IMcObject[] getSelectedObjects() {
        IMcObject[] selectedObjects = new IMcObject[lstSelectedObjects.size()];
        lstSelectedObjects.toArray(selectedObjects);
        return selectedObjects;
    }


    private void initGtObjByIdBttn() {
        mGetObjByIdBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                clearListItems();
                IMcObject obj = null;
                try {
                    obj = mOverlay.GetObjectByID( mGetObjByIdET.getUInt());
                    IMcObject objs[] = new IMcObject[1];
                    objs[0] = obj;
                    initObjectAdapter(objs);
                    initObjectsListWithAdapter();
                }
                catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlay.GetObjectByID");
                }catch (Exception e) {
                    e.printStackTrace();
                }

               ;
            }
        });
    }

    private int mCurrentActionCode;
    private void initSaveAllAsNativeVectorBttn() {
        mSaveAllAsNativeVectorBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                mCurrentActionCode = Consts.SAVE_ALL_FILE_AS_NATIVE_REQUEST_CODE;
                OpenViewportsListContainSpecificOverlayFragment(Consts.SELECT_VIEWPORT_ACTION_SAVE_AS_NATIVE);
            }
        });
    }

    private void initSaveAsNativeVectorBttn() {
        mSaveAsNativeVectorBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                mCurrentActionCode = Consts.SAVE_FILE_AS_NATIVE_REQUEST_CODE;
                OpenViewportsListContainSpecificOverlayFragment(Consts.SELECT_VIEWPORT_ACTION_SAVE_AS_NATIVE);
            }
        });
    }

    private void initSaveAsRawVectorBttn() {
        mSaveAsRawVectorBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                mCurrentActionCode = Consts.SAVE_OBJECTS_AS_RAW_VECTOR_REQUEST_CODE;
                OpenSaveObjectsAsRawVectorFragment(Consts.SAVE_OBJECTS_AS_RAW_VECTOR_REQUEST_CODE);
            }
        });
    }

    private void initSaveAllAsRawVectorBttn() {
        mSaveAllAsRawVectorBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                mCurrentActionCode = Consts.SAVE_ALL_OBJECTS_AS_RAW_VECTOR_REQUEST_CODE;
                OpenSaveObjectsAsRawVectorFragment(Consts.SAVE_ALL_OBJECTS_AS_RAW_VECTOR_REQUEST_CODE);
            }
        });
    }


    private void OpenViewportsListContainSpecificOverlayFragment(int actionCode)
    {
        String className = ViewportsListContainSpecificOverlayFragment.class.getName();
        DialogFragment dialog = (DialogFragment) DialogFragment.instantiate(getActivity(), className);
        Bundle args = new Bundle();
        args.putInt("actionCode", actionCode);
        dialog.setArguments(args);
        ((FragmentWithObject) dialog).setObject(mOverlay);
        dialog.setTargetFragment(OverlayObjectsFragment.this, 1);
        dialog.show(getFragmentManager(), className);
    }

    private void OpenSaveObjectsAsRawVectorFragment(int actionCode)
    {
        String className = SaveObjectsAsRawVectorFragment.class.getName();
        DialogFragment dialog = (DialogFragment) DialogFragment.instantiate(getActivity(), className);
        Bundle args = new Bundle();
        args.putInt("actionCode", actionCode);
        dialog.setArguments(args);
        ((FragmentWithObject) dialog).setObject(mOverlay);
        dialog.setTargetFragment(OverlayObjectsFragment.this, 1);
        dialog.show(getFragmentManager(), className);
    }

    @Override
    public void callbackViewportsListContainSpecificOverlay(IMcMapViewport vp, float minScale, float maxScale,IMcMapLayer.STilingScheme tilingScheme, int actionMode) {
        mSelectedViewport = vp;
        mMinScale = minScale;
        mMaxScale = maxScale;
        mTilingScheme = tilingScheme;

        if (actionMode == Consts.SELECT_VIEWPORT_ACTION_SAVE_AS_NATIVE) {
            openAntExplorerFolderPickerDialog();
        } else if (actionMode == Consts.SELECT_VIEWPORT_ACTION_GET_SUB_ITEM_VISIBILITY) {
            ObjectRef<int[]> subItemsIDs = new ObjectRef<>();
            ObjectRef<Boolean> visibility = new ObjectRef<>();
            try {
                mOverlay.GetSubItemsVisibility(subItemsIDs, visibility, mSelectedViewport);
                mSubItemsIdsET.setText(Funcs.ConvertIntArrToString(subItemsIDs.getValue()));
                mSubItemsIdsVisibleCB.setChecked(visibility.getValue());
            } catch (Exception e) {
                e.printStackTrace();
            }
        } else if (actionMode == Consts.SELECT_VIEWPORT_ACTION_SET_SUB_ITEM_VISIBILITY) {
            final int[] subItemsID = Funcs.ConvertStringToIntArr(mSubItemsIdsET.getText().toString());
            final boolean isSubItemsIds = mSubItemsIdsVisibleCB.isChecked();
            Funcs.runMapCoreFunc(new Runnable() {
                @Override
                public void run() {
                    try {
                        mOverlay.SetSubItemsVisibility(subItemsID, isSubItemsIds, mSelectedViewport);
                    } catch (Exception e){
                        e.printStackTrace();
                    }
                }
            });
        }
    }

    private void openAntExplorerFolderPickerDialog() {
        DirectoryChooserDialog directoryChooserDialog =
                new DirectoryChooserDialog(this.getContext(),
                        new DirectoryChooserDialog.ChosenDirectoryListener() {
                            @Override
                            public void onChosenDir(String chosenDir) {
                                SaveAsNativeVector(chosenDir);
                            }
                        }, false);
        // Load directory chooser dialog for initial 'mChosenDir' directory.
        // The registered callback will be called upon final directory selection.
        directoryChooserDialog.chooseDirectory("");

    }

    private void SaveAsNativeVector(final String chosenDir) {
        final float sizeFactorForVectorMin = mSizeFactorForVectorMapLayerMinET.getFloat();
        final float sizeFactorForVectorMax = mSizeFactorForVectorMapLayerMaxET.getFloat();
        final IMcOverlayManager.ESavingVersionCompatibility savingVersionCompatibility = cvSaveParamsData.getSavingVersionCompatibility();
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    if (mCurrentActionCode == Consts.SAVE_FILE_AS_NATIVE_REQUEST_CODE)
                        mOverlay.SaveObjectsAsNativeVectorMapLayer(getSelectedObjects(),
                                chosenDir,
                                mSelectedViewport,
                                mMinScale,
                                mMaxScale,
                                1,
                                sizeFactorForVectorMin,
                                sizeFactorForVectorMax,
                                mTilingScheme,
                                savingVersionCompatibility);
                    else if (mCurrentActionCode == Consts.SAVE_ALL_FILE_AS_NATIVE_REQUEST_CODE)
                        mOverlay.SaveAllObjectsAsNativeVectorMapLayer(chosenDir,
                                mSelectedViewport,
                                mMinScale,
                                mMaxScale,
                                1,
                                sizeFactorForVectorMin,
                                sizeFactorForVectorMax,
                                mTilingScheme,
                                savingVersionCompatibility);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlay.SaveAllObjectsAsNativeVectorMapLayer");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void initSubItemsVisibility() {
        mSubItemsIdsGetBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                OpenViewportsListContainSpecificOverlayFragment(Consts.SELECT_VIEWPORT_ACTION_GET_SUB_ITEM_VISIBILITY);
            }
        });
        mSubItemsIdsSetBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                OpenViewportsListContainSpecificOverlayFragment(Consts.SELECT_VIEWPORT_ACTION_SET_SUB_ITEM_VISIBILITY);
            }
        });
    }

    private void inflateViews() {
        mObjectsLV = (ListView) mRootView.findViewById(R.id.overlay_objects_lv);
        mLoadFromFileBttn = (Button) mRootView.findViewById(R.id.overlay_objects_list_load_from_file_bttn);
        mLoadFromBufferBttn = (Button) mRootView.findViewById(R.id.overlay_objects_list_load_from_buffer_bttn);
        mSaveAllToFileBttn = (Button) mRootView.findViewById(R.id.overlay_objects_list_save_all_to_file_bttn);
        mSaveAllToBufferBttn = (Button) mRootView.findViewById(R.id.overlay_objects_list_save_all_to_buffer_bttn);
        mSaveToFileBttn = (Button) mRootView.findViewById(R.id.overlay_objects_list_save_to_file_bttn);
        mSaveToBufferBttn = (Button) mRootView.findViewById(R.id.overlay_objects_list_save_to_buffer_bttn);
        mObjectsClearBttn = (Button) mRootView.findViewById(R.id.overlay_objects_list_clear_bttn);
        mSizeFactorForVectorMapLayerMinET = (NumericEditTextLabel) mRootView.findViewById(R.id.overlay_objects_size_factor_for_vector_min_et);
        mSizeFactorForVectorMapLayerMaxET = (NumericEditTextLabel) mRootView.findViewById(R.id.overlay_objects_size_factor_for_vector_max_et);
        mSaveAsNativeVectorBttn = (Button) mRootView.findViewById(R.id.overlay_objects_save_as_native_vector_bttn);
        mSaveAllAsNativeVectorBttn = (Button) mRootView.findViewById(R.id.overlay_objects_save_all_as_native_vector_bttn);
        mSaveAsRawVectorBttn = (Button) mRootView.findViewById(R.id.overlay_objects_save_as_raw_vector_map_layer_bttn);
        mSaveAllAsRawVectorBttn = (Button) mRootView.findViewById(R.id.overlay_objects_save_all_as_raw_vector_map_layer_bttn);
        mSubItemsIdsET = (EditText) mRootView.findViewById(R.id.overlay_objects_sub_items_ids_et);
        mSubItemsIdsVisibleCB = (CheckBox) mRootView.findViewById(R.id.overlay_object_sub_items_visibility_cb);
        mStorgeFormatSWL = (SpinnerWithLabel) mRootView.findViewById(R.id.overlay_objects_list_storage_format_swl);
        mSubItemsIdsGetBttn = (Button) mRootView.findViewById(R.id.overlay_object_sub_items_visibility_get_bttn);
        mSubItemsIdsSetBttn = (Button) mRootView.findViewById(R.id.overlay_object_sub_items_visibility_set_bttn);
        mGetObjByIdBttn = (Button) mRootView.findViewById(R.id.overlay_objects_get_obj_by_id_bttn);
        mGetObjByIdET = (NumericEditTextLabel) mRootView.findViewById(R.id.overlay_objects_get_obj_by_id_et);
        lstSelectedObjects = new ArrayList<>();
        cvSaveParamsData = (SaveParamsData) mRootView.findViewById(R.id.overlay_objects_save_params_data);
        cvSaveParamsData.setCBSavePropertiesDisable();
        mSizeFactorForVectorMapLayerMinET.setFloat(1.0f);
        mSizeFactorForVectorMapLayerMaxET.setFloat(1.0f);

        mStorgeFormatSWL.setAdapter(new ArrayAdapter<>(getContext(), android.R.layout.simple_spinner_item, IMcOverlayManager.EStorageFormat.values()));
       // mStorgeFormatSWL.setSelection(IMcOverlayManager.EStorageFormat.ESF_MAPCORE_BINARY.getValue());
    }

    private HashMap<Object, Integer> GetHashMapObjects(IMcObject[] arrayObjects)
    {
        HashMap<Object, Integer> hashMapObjects = null;
        if (arrayObjects != null && arrayObjects.length > 0) {
            hashMapObjects = new HashMap<Object, Integer>();
            for (IMcObject obj : arrayObjects) {
                hashMapObjects.put(obj, obj.hashCode());
            }
        }
        return hashMapObjects;
    }

    private void initObjectAdapter(IMcObject[] arrayObjects) {
        HashMap<Object, Integer> hashMapObjects = GetHashMapObjects(arrayObjects);
        mObjectsAdapter = new HashMapAdapter(null, hashMapObjects, Consts.ListType.MULTIPLE_CHECK);
    }

    private void initObjectsList() {

        try {
            IMcObject[] arrayObjects = mOverlay.GetObjects();
            initObjectAdapter(arrayObjects);
            initObjectsListWithAdapter();

        } catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "IMcOverlay.GetObjects");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initObjectsListWithAdapter() {
        mObjectsLV.setChoiceMode(ListView.CHOICE_MODE_MULTIPLE);
        mObjectsLV.setAdapter(mObjectsAdapter);
        mObjectsLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int index, long l) {
                Map.Entry<Object, Object> item = mObjectsAdapter.getItem(index);
                if (((CheckedTextView) view).isChecked()) {
                    lstSelectedObjects.add((IMcObject) item.getKey());
                } else
                    lstSelectedObjects.remove(item.getKey());
            }
        });
        mObjectsLV.deferNotifyDataSetChanged();
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
    public void setObject(Object obj) {
        mOverlay = (IMcOverlay) obj;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
    }


    private IMcMapViewport mSaveAsRawVP;
    private float mSaveAsRawCameraYawAngle;
    private float mSaveAsRawCameraScale;
    private String mSaveAsRawLayerName;
    private boolean mSaveAsRawIsMemoryBuffer;
    private EGeometry mSaveAsRawGeometryFilter;

    @Override
    public void callbackSaveObjectsAsRawVectorOverlay(final IMcMapViewport vp, final float cameraYawAngle, final float cameraScale,final String layerName, final boolean isMemoryBuffer, final EGeometry geometryFilter,  int actionCode) {
        mSaveAsRawVP = vp;
        mSaveAsRawCameraYawAngle = cameraYawAngle;
        mSaveAsRawCameraScale = cameraScale;
        mSaveAsRawLayerName = layerName;
        mSaveAsRawIsMemoryBuffer = isMemoryBuffer;
        mSaveAsRawGeometryFilter = geometryFilter;

        DirectoryChooserDialog directoryChooserDialog =
                new DirectoryChooserDialog(this.getContext(),
                        new DirectoryChooserDialog.ChosenDirectoryListener() {
                            @Override
                            public void onChosenDir(String chosenDir) {

                                mFilePath = chosenDir;
                                Funcs.runMapCoreFunc(new Runnable() {
                                    @Override
                                    public void run() {
                                        try {
                                            File f = new File(mFilePath);
                                            String filename = f.getName();
                                            if (mCurrentActionCode == Consts.SAVE_OBJECTS_AS_RAW_VECTOR_REQUEST_CODE) {
                                                if (mSaveAsRawIsMemoryBuffer) {
                                                    ObjectRef<byte[]> pauFileMemoryBuffer = new ObjectRef<>();
                                                    ObjectRef<SMcFileInMemory[]> paAdditionalFiles = new ObjectRef<>();
                                                    mOverlay.SaveObjectsAsRawVectorData(getSelectedObjects(),
                                                            mSaveAsRawVP,
                                                            mSaveAsRawCameraYawAngle,
                                                            mSaveAsRawCameraScale,
                                                            mSaveAsRawLayerName,
                                                            filename,
                                                            pauFileMemoryBuffer,
                                                            paAdditionalFiles,
                                                            null,
                                                            mSaveAsRawGeometryFilter);

                                                } else {
                                                    ObjectRef<String[]> paAdditionalFiles = new ObjectRef<>();
                                                    mOverlay.SaveObjectsAsRawVectorData(getSelectedObjects(),
                                                            mSaveAsRawVP,
                                                            mSaveAsRawCameraYawAngle,
                                                            mSaveAsRawCameraScale,
                                                            mSaveAsRawLayerName,
                                                            mFilePath,
                                                            paAdditionalFiles,
                                                            null,
                                                            mSaveAsRawGeometryFilter);
                                                }
                                            } else if (mCurrentActionCode == Consts.SAVE_ALL_OBJECTS_AS_RAW_VECTOR_REQUEST_CODE)
                                                if (mSaveAsRawIsMemoryBuffer) {
                                                    ObjectRef<byte[]> pauFileMemoryBuffer = new ObjectRef<>();
                                                    ObjectRef<SMcFileInMemory[]> paAdditionalFiles = new ObjectRef<>();

                                                    mOverlay.SaveAllObjectsAsRawVectorData(mSaveAsRawVP,
                                                            mSaveAsRawCameraYawAngle,
                                                            mSaveAsRawCameraScale,
                                                            mSaveAsRawLayerName,
                                                            filename,
                                                            pauFileMemoryBuffer,
                                                            paAdditionalFiles,
                                                            null,
                                                            mSaveAsRawGeometryFilter);

                                                } else {
                                                    ObjectRef<String[]> paAdditionalFiles = new ObjectRef<>();
                                                    mOverlay.SaveAllObjectsAsRawVectorData(mSaveAsRawVP,
                                                            mSaveAsRawCameraYawAngle,
                                                            mSaveAsRawCameraScale,
                                                            mSaveAsRawLayerName,
                                                            mFilePath,
                                                            paAdditionalFiles,
                                                            null,
                                                            mSaveAsRawGeometryFilter);
                                                }
                                        } catch (MapCoreException e) {
                                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlay.SaveObjectsAsRawVectorData");
                                        } catch (Exception e) {
                                            e.printStackTrace();
                                        }
                                    }
                                });

                            }
                        }, true, true);
        // Load directory chooser dialog for initial 'mChosenDir' directory.
        // The registered callback will be called upon final directory selection.
        directoryChooserDialog.chooseDirectory("");
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
