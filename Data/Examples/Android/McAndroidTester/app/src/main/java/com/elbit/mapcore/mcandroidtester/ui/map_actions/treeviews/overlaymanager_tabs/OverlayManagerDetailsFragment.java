package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlaymanager_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.DialogFragment;
import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CheckedTextView;
import android.widget.EditText;
import android.widget.ExpandableListView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.Spinner;

import com.elbit.mapcore.General.ObjectRef;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.interfaces.IOverlayManagerShowLockFragment;
import com.elbit.mapcore.mcandroidtester.interfaces.OnSelectViewportFromListListener;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_AMCTMapForm;
import com.elbit.mapcore.mcandroidtester.model.AMCTUserDataFactory;
import com.elbit.mapcore.mcandroidtester.ui.adapters.HashMapAdapter;
import com.elbit.mapcore.mcandroidtester.ui.adapters.ViewPortsObjectsELAdapter;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.DirectoryChooserDialog;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.GridCoordinateSystemList;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SaveParamsData;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SpinnerWithLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.ViewportsList;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectScheme;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlay;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link OverlayManagerDetailsFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link OverlayManagerDetailsFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class OverlayManagerDetailsFragment extends Fragment implements FragmentWithObject, OnSelectViewportFromListListener {

    private OnFragmentInteractionListener mListener;
    private static IMcOverlayManager mOverlayManager;
    private View mRootView;
    private Button mLoadFromFileBttn;
    private Button mLoadFromBufferBttn;
    private Button mSaveAllToFileBttn;
    private Button mSaveAllToBufferBttn;
    private Button mSaveToFileBttn;
    private Button mSaveToBufferBttn;
    private GridCoordinateSystemList mCoordSysLV;
    private Button mTopMostModeApplyBttn;
    private Button mMoveIfBlockModeBttn;
    private Button mBlockedTransparencyModeBttn;
    private Button mCancelScaleModeBttn;
    private Button mCollectionModeBttn;
    private Button mScaleFactorBttn;
    private Button mEquidistantAttachPointsMinScaleBttn;
    private Button mStateBttn;
    private CheckBox mTopMostModeCb;
    private CheckBox mMoveIfBlockModeCb;
    private CheckBox mBlockedTransparencyModeCb;
    private CheckedTextView mCancelScaleMode0Cb;
    private CheckedTextView mCancelScaleMode1Cb;
    private CheckedTextView mCancelScaleMode2Cb;
    private CheckedTextView mCancelScaleMode3Cb;
    private CheckedTextView mCancelScaleMode4Cb;
    private CheckedTextView mCancelScaleMode5Cb;
    private CheckedTextView mCancelScaleMode6Cb;
    private CheckedTextView mCancelScaleMode7Cb;
    private CheckedTextView mCancelScaleMode8Cb;
    private CheckedTextView mCancelScaleMode9Cb;
    private NumericEditTextLabel mScaleFactorEt;
    private NumericEditTextLabel mEquidistantAttachPointMinScaleEt;
    private EditText mStateEt;
    private SpinnerWithLabel mCollectionModeSWL;
    private Spinner mCollectionModeSpinner;

    private Button mLockSchemeListShowBttn;
    private Button mLockConditionalSelectorListShowBttn;
    private Button mScreenTerraainItensConsistencyScaleStepsBttn;
    private EditText mScreenTerraainItensConsistencyScaleStepsET;
    private Button mSetScreenArrangementBttn;
    private Button mCancelScreenArrangementBttn;
    private Button mSchemesClearBttn;

    private ListView mSchemesLV;
    private ViewportsList mViewportsLV;

    private HashMapAdapter mSchemesAdapter;

    private List<IMcObjectScheme> lstSelectedSchemes;
    private IMcMapViewport mSelectedViewport;

    private String mFilePath;
    private SaveParamsData cvSaveParamsData;
    private final IMcOverlayManager.ECollectionsMode[] eECollectionsModeValues = IMcOverlayManager.ECollectionsMode.values();
    private IMcOverlayManager.ECollectionsMode mCollectionModeValue;

    private HashMap<Object, Integer> hashMapVp;
    private HashMap<Object, ArrayList<IMcObject>> mViewPortObjectsHM;
    private ArrayList<Integer> mViewportsSelected;

    private int MASK0 = 1;
    private int MASK1 = 2;
    private int MASK2 = 4;
    private int MASK3 = 8;
    private int MASK4 = 16;
    private int MASK5 = 32;
    private int MASK6 = 64;
    private int MASK7 = 128;
    private int MASK8 = 256;
    private int MASK9 = 512;
    private ExpandableListView mViewPortsObjectsELV;
    private ViewPortsObjectsELAdapter mViewPortsObjectsAdapter;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment BaseLayerDetailsTabsFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static OverlayManagerDetailsFragment newInstance() {
        OverlayManagerDetailsFragment fragment = new OverlayManagerDetailsFragment();
       return fragment;
    }

    public OverlayManagerDetailsFragment() {
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
        mRootView = inflater.inflate(R.layout.fragment_overlay_manager_details, container, false);
        lstSelectedSchemes = new ArrayList<>();

        initViews();
        return mRootView;
    }

    @Override
    public void onHiddenChanged(boolean hidden) {
        super.onHiddenChanged(hidden);
    }

    private void initViews() {

        initMultiSelectViewportList();
        initGridCoordinateSysList();
        initObjectSchemes();
        initOverlayOperations();
        initOMOperation();

        initGeneralPropertiesViews();
    }

    private void initGeneralPropertiesViews() {
        cvSaveParamsData = (SaveParamsData) mRootView.findViewById(R.id.om_details_save_params_data);
    }

    private void initMultiSelectViewportList() {
        mViewPortsObjectsELV = (ExpandableListView) mRootView.findViewById(R.id.om_details_viewports_objects_elv);

        mSetScreenArrangementBttn = (Button) mRootView.findViewById(R.id.om_details_om_set_screen_arrangement_bttn);
        mCancelScreenArrangementBttn = (Button) mRootView.findViewById(R.id.om_details_cancel_screen_arrangement_bttn);

        mSetScreenArrangementBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                HashMap<Integer, ArrayList<Integer>> mPair = mViewPortsObjectsAdapter.getViewPortObjectsSelectedHM2();
                Object[] indexVps = mPair.keySet().toArray();
                Object[] vps = hashMapVp.keySet().toArray();

                for (Object indexVpObj : indexVps) {
                    Integer indexVp = (Integer) indexVpObj;
                    final IMcMapViewport vp = (IMcMapViewport) vps[indexVp];
                    ArrayList<Integer> indexObjs = mViewPortsObjectsAdapter.getViewPortObjectsSelectedHM2().get(indexVp);
                    ArrayList<IMcObject> selectedObjs = new ArrayList<>();
                    ArrayList<IMcObject> objs = mViewPortObjectsHM.get(vp);
                    for (Integer indexObj : indexObjs) {
                        selectedObjs.add(objs.get(indexObj));
                    }

                    if (selectedObjs.size() > 0) {
                        IMcObject[] arrObjects = new IMcObject[selectedObjs.size()];
                        arrObjects = selectedObjs.toArray(arrObjects);
                        final IMcObject[] finalArrObjects = arrObjects.clone();
                        Funcs.runMapCoreFunc(new Runnable() {
                            @Override
                            public void run() {

                                try {
                                    mOverlayManager.SetScreenArrangement(vp, finalArrObjects);
                                } catch (MapCoreException e) {
                                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlayManager.SetScreenArrangement");
                                } catch (Exception e) {
                                    e.printStackTrace();
                                }
                            }});
                    }
                }
            }
        });

        mCancelScreenArrangementBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (mViewportsSelected.size() > 0) {
                    Object[] vps = hashMapVp.keySet().toArray();
                    for (Integer indexVp : mViewportsSelected) {
                        IMcMapViewport vp = (IMcMapViewport) vps[indexVp];
                        try {
                            mOverlayManager.CancelScreenArrangements(vp);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlayManager.CancelScreenArrangements");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                }
            }
        });

        initMultiSelectViewportListLV();
    }

    private void initMultiSelectViewportListLV() {
    }

    private void initGridCoordinateSysList() {
    }

    private void initObjectSchemes() {
        inflateSchemesViews();
        initSchemesList();
        initSchemesBttns();
        initContinueSavingCB();
    }

    private void initOverlayOperations() {
        inflateOverlayOperationsViews();
        initOverlayOperationsButtons();
        initOverlayOperationsCBs();
        initCollectionModeSpinner();
        initViewportsLV();
        initViewportsObjectsList();

        try {
            mViewportsLV.initViewportsList(this,
                    Consts.ListType.SINGLE_CHECK,
                    ListView.CHOICE_MODE_SINGLE,
                    mOverlayManager);
        } catch (Exception e) {
            e.printStackTrace();
        }
        mViewPortsObjectsAdapter = new ViewPortsObjectsELAdapter(hashMapVp, mViewPortObjectsHM, getContext());
        mViewPortsObjectsELV.setAdapter(mViewPortsObjectsAdapter);
        setViewPortsObjectsELVHeight();
        mViewportsSelected = new ArrayList<>();
        mViewPortsObjectsELV.setOnGroupClickListener(new ExpandableListView.OnGroupClickListener() {
            @Override
            public boolean onGroupClick(ExpandableListView expandableListView, View view, int i, long l) {
                final CheckedTextView viewportHeader = (CheckedTextView) view.findViewById(R.id.list_view);
                boolean isChecked = !viewportHeader.isChecked();
                viewportHeader.setChecked(isChecked);
                if(isChecked) {
                    if (!mViewportsSelected.contains(i))
                        mViewportsSelected.add(i);
                }
                else
                {
                    if (mViewportsSelected.contains(i))
                        mViewportsSelected.remove(new Integer(i));
                }
                return false;
            }
        });
        mViewPortsObjectsELV.setOnGroupExpandListener(new ExpandableListView.OnGroupExpandListener() {
            @Override
            public void onGroupExpand(int groupPosition) {
                LinearLayout.LayoutParams param = (LinearLayout.LayoutParams) mViewPortsObjectsELV.getLayoutParams();
                param.height = param.height+mViewPortsObjectsAdapter.getChildrenCount(groupPosition)*165;/*(getGroupAndChildrenCount() * 175*//* *//*mViewPortsObjectsELV.getHeight());*/
                mViewPortsObjectsELV.setLayoutParams(param);
                mViewPortsObjectsELV.refreshDrawableState();
                mRootView.findViewById(R.id.om_details_sv).refreshDrawableState();

            }
        });

        mViewPortsObjectsELV.setOnGroupCollapseListener(new ExpandableListView.OnGroupCollapseListener() {
            @Override
            public void onGroupCollapse(int groupPosition) {
                LinearLayout.LayoutParams param = (LinearLayout.LayoutParams) mViewPortsObjectsELV.getLayoutParams();
                //param.height = mViewPortsObjectsAdapter.getGroupCount()*175;
                param.height = param.height-mViewPortsObjectsAdapter.getChildrenCount(groupPosition)*165;
                mViewPortsObjectsELV.setLayoutParams(param);
                mViewPortsObjectsELV.refreshDrawableState();
                mRootView.findViewById(R.id.om_details_sv).refreshDrawableState();
            }
        });
        initOverlayOperationsValues();
    }

    private void setViewPortsObjectsELVHeight() {
        LinearLayout.LayoutParams param = (LinearLayout.LayoutParams) mViewPortsObjectsELV.getLayoutParams();
        param.height = (mViewPortsObjectsAdapter.getGroupCount() * 175);
        mViewPortsObjectsELV.setLayoutParams(param);
        mViewPortsObjectsELV.refreshDrawableState();
        mRootView.findViewById(R.id.om_details_sv).refreshDrawableState();
    }

    private void initOMOperation() {
        // inflateOMOperationViews();
        initOMOperationBttns();
        // initScreenTerrainItemsConsistencyScaleStepsET();
    }

    //region initObjectSchemes
    private void inflateSchemesViews() {
        mSchemesLV = (ListView) mRootView.findViewById(R.id.om_details_schemes_lv);
        mCoordSysLV = (GridCoordinateSystemList) mRootView.findViewById(R.id.om_details_coordinate_system_lv);
        mLoadFromFileBttn = (Button) mRootView.findViewById(R.id.om_details_schemes_load_from_file_bttn);
        mLoadFromBufferBttn = (Button) mRootView.findViewById(R.id.om_details_schemes_load_from_buffer_bttn);
        mSaveAllToFileBttn = (Button) mRootView.findViewById(R.id.om_details_schemes_save_all_to_file_bttn);
        mSaveAllToBufferBttn = (Button) mRootView.findViewById(R.id.om_details_schemes_save_all_to_buffer_bttn);
        mSaveToFileBttn = (Button) mRootView.findViewById(R.id.om_details_schemes_save_to_file_bttn);
        mSaveToBufferBttn = (Button) mRootView.findViewById(R.id.om_details_schemes_save_to_buffer_bttn);
        mSchemesClearBttn = (Button) mRootView.findViewById(R.id.om_details_schemes_clear_bttn);

        try {
            mCoordSysLV.selectCurrGridCoordSys(mOverlayManager.GetCoordinateSystemDefinition());
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlayManager.GetCoordinateSystemDefinition");
        } catch (Exception e) {
            e.printStackTrace();
        }
        mCoordSysLV.setFocusable(false);
        mCoordSysLV.setClickable(false);
    }

    private HashMap<Object, Integer> GetHashMapObjectsScheme(IMcObjectScheme[] arraySchemes)
    {
        HashMap<Object, Integer> hashMapSchemes = null;
        if (arraySchemes != null && arraySchemes.length > 0) {
            hashMapSchemes = new HashMap<Object, Integer>();
            for (IMcObjectScheme scheme : arraySchemes) {
                hashMapSchemes.put(scheme, scheme.hashCode());
            }
        }
        return hashMapSchemes;
    }

    private void initSchemeAdapter(IMcObjectScheme[] arraySchemes) {
        HashMap<Object, Integer> hashMapSchemes = GetHashMapObjectsScheme(arraySchemes);
        mSchemesAdapter = new HashMapAdapter(null, hashMapSchemes, Consts.ListType.MULTIPLE_CHECK);
    }

    private void initSchemesList() {

        try {
            IMcObjectScheme[] arraySchemes = mOverlayManager.GetObjectSchemes();
            initSchemeAdapter(arraySchemes);
            initSchemesListWithAdapter();

        } catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "IMcOverlayManager.GetObjectSchemes");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initSchemesListWithAdapter() {
        mSchemesLV.setChoiceMode(ListView.CHOICE_MODE_MULTIPLE);
        mSchemesLV.setAdapter(mSchemesAdapter);
        mSchemesLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int index, long l) {
                Map.Entry<Object, Object> item = mSchemesAdapter.getItem(index);
                if (((CheckedTextView) view).isChecked()) {
                    lstSelectedSchemes.add((IMcObjectScheme) item.getKey());
                } else
                    lstSelectedSchemes.remove(item.getKey());
            }
        });

        mSchemesLV.deferNotifyDataSetChanged();

    }

    private void initSchemesBttns() {

        mLoadFromFileBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
               // performFileSearch(Consts.LOAD_FILE_REQUEST_CODE);
                DirectoryChooserDialog directoryChooserDialog =
                        new DirectoryChooserDialog(getContext(),
                                new DirectoryChooserDialog.ChosenDirectoryListener() {
                                    @Override
                                    public void onChosenDir(String chosenDir) {
                                        loadScheme(chosenDir);
                                    }
                                }, true);
                // Load directory chooser dialog for initial 'mChosenDir' directory.
                // The registered callback will be called upon final directory selection.
                directoryChooserDialog.chooseDirectory("");
            }
        });

        mLoadFromBufferBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        ObjectRef<Boolean> pbObjectDataDetected = new ObjectRef<>();
                        try {
                            AMCTUserDataFactory UDF = new AMCTUserDataFactory();
                            final IMcObjectScheme[] objectSchemes = mOverlayManager.LoadObjectSchemes(MapsContainerActivity.overlayManagerBuffer, UDF, pbObjectDataDetected);
                            final ObjectRef<Boolean> pbObjectDataDetected2 = pbObjectDataDetected;
                            getActivity().runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    addSchemeToList(objectSchemes);
                                    if(pbObjectDataDetected2.getValue())
                                        AlertMessages.ShowMessage(getContext(),"Load object schemes from buffer","Warning: the memory buffer contains objects, their schemes only were loaded" );
                                }
                            });
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlayManager.LoadObjectSchemes");
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

                final boolean saveUserData = cvSaveParamsData.getSaveUserData();
                final boolean savePropertiesNames = cvSaveParamsData.getSavePropertiesNames();
                final IMcOverlayManager.ESavingVersionCompatibility savingVersionCompatibility = cvSaveParamsData.getSavingVersionCompatibility();
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            MapsContainerActivity.overlayManagerBuffer =
                                    mOverlayManager.SaveAllObjectSchemes(
                                            IMcOverlayManager.EStorageFormat.ESF_MAPCORE_BINARY,
                                            savingVersionCompatibility);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlayManager.SaveAllObjectSchemes");
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
                final boolean savePropertiesNames = cvSaveParamsData.getSavePropertiesNames();
                final IMcOverlayManager.ESavingVersionCompatibility savingVersionCompatibility = cvSaveParamsData.getSavingVersionCompatibility();
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {

                        try {
                            MapsContainerActivity.overlayManagerBuffer =
                                    mOverlayManager.SaveObjectSchemes(getSelectedScheme(),
                                            IMcOverlayManager.EStorageFormat.ESF_MAPCORE_BINARY,
                                            savingVersionCompatibility);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlayManager.SaveObjectSchemes");
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

        mSchemesClearBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                for (int i = 0; i < mSchemesAdapter.getCount(); i++) {
                    mSchemesLV.setItemChecked(i, false);
                }
                lstSelectedSchemes.clear();
            }
        });
    }

    private void saveToFile(final int requestCode) {
        if(requestCode == Consts.SAVE_FILE_REQUEST_CODE && lstSelectedSchemes.size() == 0)
        {
            AlertMessages.ShowGenericMessage(getContext(), "Objects Scheme", "There is no selected object schemes");
            return;
        }
        DirectoryChooserDialog directoryChooserDialog =
                new DirectoryChooserDialog(getContext(),
                        new DirectoryChooserDialog.ChosenDirectoryListener() {
                            @Override
                            public void onChosenDir(String chosenDir) {
                                saveScheme(chosenDir, requestCode);
                            }
                        }, true, true);
        directoryChooserDialog.chooseDirectory("");
    }

    private void loadScheme(final String schemeFile) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    IMcObjectScheme[] objectSchemes = null;
                    ObjectRef<Boolean> pbObjectDataDetected = new ObjectRef<>();
                    AMCTUserDataFactory UDF = new AMCTUserDataFactory();
                    objectSchemes = mOverlayManager.LoadObjectSchemes(schemeFile, UDF, pbObjectDataDetected);

                    final IMcObjectScheme[] objectSchemes2 = objectSchemes;
                    final ObjectRef<Boolean> pbObjectDataDetected2 = pbObjectDataDetected;
                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            if(objectSchemes2 != null)
                                addSchemeToList(objectSchemes2);
                            if(pbObjectDataDetected2.getValue())
                                AlertMessages.ShowMessage(getContext(),"Load object schemes from file","Warning: the file contains objects, their schemes only were loaded" );
                        }
                    });
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlayManager.LoadObjectSchemes");
                } catch (Exception e) {
                    e.printStackTrace();
                }

            }
        });
    }

    private void addSchemeToList(IMcObjectScheme[] objectSchemes) {
        if (mSchemesAdapter == null)
            initSchemeAdapter(objectSchemes);
        else {
            HashMap<Object, Integer> hashMapSchemes = GetHashMapObjectsScheme(objectSchemes);
            if (hashMapSchemes != null)
                mSchemesAdapter.addItems(hashMapSchemes);
        }
        initSchemesListWithAdapter();
    }

    private void saveScheme(final String schemeFile, final int requestCode) {
        final boolean saveUserData = cvSaveParamsData.getSaveUserData();
        final boolean savePropertiesNames = cvSaveParamsData.getSavePropertiesNames();
        final IMcOverlayManager.ESavingVersionCompatibility savingVersionCompatibility = cvSaveParamsData.getSavingVersionCompatibility();
        final IMcOverlayManager.EStorageFormat eStorageFormat = Funcs.getEStorageFormatByFileExtension(schemeFile);

        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    if (requestCode == Consts.SAVE_FILE_REQUEST_CODE) {
                        mOverlayManager.SaveObjectSchemes(
                                getSelectedScheme(),
                                schemeFile,
                                eStorageFormat,
                                savingVersionCompatibility);
                    } else if (requestCode == Consts.SAVE_ALL_FILE_REQUEST_CODE)
                        mOverlayManager.SaveAllObjectSchemes(schemeFile,
                                eStorageFormat,
                                savingVersionCompatibility);

                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlayManager.SaveAllObjectSchemes");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private IMcObjectScheme[] getSelectedScheme() {
        IMcObjectScheme[] selectedSchemes = new IMcObjectScheme[lstSelectedSchemes.size()];
        lstSelectedSchemes.toArray(selectedSchemes);
        return selectedSchemes;
    }

    private void initContinueSavingCB() {
    }

    private void initViewportsLV() {

        hashMapVp = new HashMap<>();
        for (IMcMapViewport vp : Manager_AMCTMapForm.getInstance().getAllImcViewports()) {
            try {
                if (vp.GetOverlayManager() == mOverlayManager)
                    hashMapVp.put(vp, vp.hashCode());
            }
            catch (MapCoreException e) {
                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlayManager.GetOverlayManager");
            }
            catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    private void initViewportsObjectsList() {
        mViewPortObjectsHM = new HashMap<>();
        ArrayList viewPortsList = new ArrayList();
        viewPortsList.addAll(hashMapVp.keySet());
        ArrayList<IMcObject> objectsList;

        for (Object vp : viewPortsList) {
            objectsList = new ArrayList<>();
            if (vp != null) {
                try {
                    IMcOverlay[] overlays = ((IMcMapViewport) vp).GetOverlayManager().GetOverlays();

                    for (IMcOverlay overlay : overlays) {
                        IMcObject[] objects = overlay.GetObjects();
                        for (IMcObject obj : objects) {
                            objectsList.add(obj);
                        }
                    }
                }
                catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlayManager.GetOverlays");
                }
                catch (Exception e) {
                    e.printStackTrace();
                }
            }
            mViewPortObjectsHM.put(vp, objectsList);
        }
    }

    private void initCollectionModeSpinner() {

        mCollectionModeSpinner = (Spinner) mCollectionModeSWL.findViewById(R.id.spinner_in_cv);

        mCollectionModeSpinner.setAdapter(new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, eECollectionsModeValues));
        mCollectionModeSpinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                mCollectionModeValue = eECollectionsModeValues[position];
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });
        mCollectionModeSpinner.setSelection(IMcOverlayManager.ECollectionsMode.ECM_OR.getValue());
    }

    private void initOverlayOperationsCBs() {

    }

    private void inflateOverlayOperationsViews() {
        mTopMostModeApplyBttn = (Button) mRootView.findViewById(R.id.om_details_overlay_operations_top_most_mode_apply_bttn);
        mMoveIfBlockModeBttn = (Button) mRootView.findViewById(R.id.om_details_overlay_operations_move_if_block_mode_apply_bttn);
        mBlockedTransparencyModeBttn = (Button) mRootView.findViewById(R.id.om_details_overlay_operations_blocked_transparency_mode_apply_bttn);
        mCancelScaleModeBttn = (Button) mRootView.findViewById(R.id.om_details_overlay_operations_cancel_scale_mode_apply_bttn);
        mCollectionModeBttn = (Button) mRootView.findViewById(R.id.om_details_overlay_operations_collection_mode_apply_bttn);
        mScaleFactorBttn = (Button) mRootView.findViewById(R.id.om_details_overlay_operations_scale_factor_apply_bttn);
        mEquidistantAttachPointsMinScaleBttn = (Button) mRootView.findViewById(R.id.om_details_overlay_operations_equidistant_attach_points_apply_bttn);
        mStateBttn = (Button) mRootView.findViewById(R.id.om_details_overlay_operations_state_apply_bttn);

        mCollectionModeSWL = (SpinnerWithLabel) mRootView.findViewById(R.id.om_details_overlay_operations_collection_mode_swl);
        mViewportsLV = (ViewportsList) mRootView.findViewById(R.id.om_details_overlay_operations_viewports_lv);
        mTopMostModeCb = (CheckBox) mRootView.findViewById(R.id.om_details_overlay_operations_top_most_mode_cb);
        mMoveIfBlockModeCb = (CheckBox) mRootView.findViewById(R.id.om_details_overlay_operations_move_if_block_mode_cb);
        mBlockedTransparencyModeCb = (CheckBox) mRootView.findViewById(R.id.om_details_overlay_operations_blocked_transparency_mode_cb);

        mCancelScaleMode0Cb = (CheckedTextView) mRootView.findViewById(R.id.om_details_overlay_operations_cancel_scale_mode_cb0);
        mCancelScaleMode1Cb = (CheckedTextView) mRootView.findViewById(R.id.om_details_overlay_operations_cancel_scale_mode_cb1);
        mCancelScaleMode2Cb = (CheckedTextView) mRootView.findViewById(R.id.om_details_overlay_operations_cancel_scale_mode_cb2);
        mCancelScaleMode3Cb = (CheckedTextView) mRootView.findViewById(R.id.om_details_overlay_operations_cancel_scale_mode_cb3);
        mCancelScaleMode4Cb = (CheckedTextView) mRootView.findViewById(R.id.om_details_overlay_operations_cancel_scale_mode_cb4);
        mCancelScaleMode5Cb = (CheckedTextView) mRootView.findViewById(R.id.om_details_overlay_operations_cancel_scale_mode_cb5);
        mCancelScaleMode6Cb = (CheckedTextView) mRootView.findViewById(R.id.om_details_overlay_operations_cancel_scale_mode_cb6);
        mCancelScaleMode7Cb = (CheckedTextView) mRootView.findViewById(R.id.om_details_overlay_operations_cancel_scale_mode_cb7);
        mCancelScaleMode8Cb = (CheckedTextView) mRootView.findViewById(R.id.om_details_overlay_operations_cancel_scale_mode_cb8);
        mCancelScaleMode9Cb = (CheckedTextView) mRootView.findViewById(R.id.om_details_overlay_operations_cancel_scale_mode_cb9);

        mScaleFactorEt = (NumericEditTextLabel) mRootView.findViewById(R.id.om_details_overlay_operations_scale_factor_et);
        mEquidistantAttachPointMinScaleEt = (NumericEditTextLabel) mRootView.findViewById(R.id.om_details_overlay_operations_equidistant_attach_points_et);
        mStateEt = (EditText) mRootView.findViewById(R.id.om_details_overlay_operations_state_et);
    }

    private void initOverlayOperationsButtons() {
        initTopMostModeBttn();
        initMoveIfBlockModeBttn();
        initBlockedTransparencyModeBttn();
        initCancelScaleModeBttn();
        initCollectionModeBttn();
        initScaleFactorBttn();
        initEquidistantAttachPointMinScaleBttn();
        initStateBttn();
    }

    private void initOverlayOperationsValues()
    {
        initStatesValues();
        initEquidistantAttachPointValue();
        initScaleFactorValue();
        initECollectionsModeValue();
        initCancelScaleModeValue();
        initBlockedTransparencyModeValue();
        initMoveIfBlockModeValue();
        initTopMostModeValue();
    }

    private void initStatesValues() {
        byte[] states = null;
        try {
            if (mSelectedViewport != null) {
                states = mOverlayManager.GetState(mSelectedViewport);
            } else {
                states = mOverlayManager.GetState();
            }
            mStateEt.setText(Funcs.getStateFromByteArray(states));
        }catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlayManager.GetState");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initEquidistantAttachPointValue() {
        float equidistantAttachPointMinScaleValue = 0;
        try {
            if (mSelectedViewport != null) {
                equidistantAttachPointMinScaleValue = mOverlayManager.GetEquidistantAttachPointsMinScale(mSelectedViewport);
            } else {
                equidistantAttachPointMinScaleValue = mOverlayManager.GetEquidistantAttachPointsMinScale();
            }
            mEquidistantAttachPointMinScaleEt.setFloat(equidistantAttachPointMinScaleValue);
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlayManager.GetEquidistantAttachPointsMinScale");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initScaleFactorValue() {
        float scaleFactor = 0;
        try {
            if (mSelectedViewport != null) {
                scaleFactor = mOverlayManager.GetScaleFactor(mSelectedViewport);
             } else {
               scaleFactor = mOverlayManager.GetScaleFactor();
              }
             mScaleFactorEt.setFloat(scaleFactor);
        }catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlayManager.GetScaleFactor");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initECollectionsModeValue() {
        IMcOverlayManager.ECollectionsMode eCollectionsMode;
        try {
            if (mSelectedViewport != null) {
                eCollectionsMode = mOverlayManager.GetCollectionsMode(mSelectedViewport);
            } else {
                eCollectionsMode = mOverlayManager.GetCollectionsMode();
            }
            mCollectionModeSWL.setSelection(eCollectionsMode.getValue());

        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlayManager.GetCollectionsMode");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initCancelScaleModeValue() {
        int cancelScaleMode;
        try {
            if (mSelectedViewport != null) {
                cancelScaleMode = mOverlayManager.GetCancelScaleMode(mSelectedViewport);
            } else {
                cancelScaleMode = mOverlayManager.GetCancelScaleMode(mSelectedViewport);
            }

            mCancelScaleMode0Cb.setChecked(((cancelScaleMode & MASK0) > 0) ? true : false);
            mCancelScaleMode1Cb.setChecked(((cancelScaleMode & MASK1) > 0) ? true : false);
            mCancelScaleMode2Cb.setChecked(((cancelScaleMode & MASK2) > 0) ? true : false);
            mCancelScaleMode3Cb.setChecked(((cancelScaleMode & MASK3) > 0) ? true : false);
            mCancelScaleMode4Cb.setChecked(((cancelScaleMode & MASK4) > 0) ? true : false);
            mCancelScaleMode5Cb.setChecked(((cancelScaleMode & MASK5) > 0) ? true : false);
            mCancelScaleMode6Cb.setChecked(((cancelScaleMode & MASK6) > 0) ? true : false);
            mCancelScaleMode7Cb.setChecked(((cancelScaleMode & MASK7) > 0) ? true : false);
            mCancelScaleMode8Cb.setChecked(((cancelScaleMode & MASK8) > 0) ? true : false);
            mCancelScaleMode9Cb.setChecked(((cancelScaleMode & MASK9) > 0) ? true : false);

        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlayManager.GetCancelScaleMode");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initBlockedTransparencyModeValue() {
        boolean blockedTransparencyMode = false;
        try {
            if (mSelectedViewport != null) {
                blockedTransparencyMode = mOverlayManager.GetBlockedTransparencyMode(mSelectedViewport);
            } else {
                blockedTransparencyMode = mOverlayManager.GetBlockedTransparencyMode();
            }
            mBlockedTransparencyModeCb.setChecked(blockedTransparencyMode);
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlayManager.GetBlockedTransparencyMode");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initMoveIfBlockModeValue() {

        boolean moveIfBlockModeValue = false;
        try {
            if (mSelectedViewport != null) {
                moveIfBlockModeValue = mOverlayManager.GetMoveIfBlockedMode(mSelectedViewport);
            } else {
                moveIfBlockModeValue = mOverlayManager.GetMoveIfBlockedMode();
            }
            mMoveIfBlockModeCb.setChecked(moveIfBlockModeValue);
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlayManager.GetMoveIfBlockedMode");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initTopMostModeValue() {
        boolean topMostModeValue;
        try {
            if (mSelectedViewport != null) {
                topMostModeValue = mOverlayManager.GetTopMostMode(mSelectedViewport);
            } else {
                topMostModeValue = mOverlayManager.GetTopMostMode();
            }
            mTopMostModeCb.setChecked(topMostModeValue);
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlayManager.GetTopMostMode");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initStateBttn() {
        mStateBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final byte[] abyStates = Funcs.getStatesFromString(mStateEt.getText().toString());
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            if (mSelectedViewport != null) {
                                if (abyStates != null && abyStates.length == 1)
                                    mOverlayManager.SetState(abyStates[0], mSelectedViewport);
                                else
                                    mOverlayManager.SetState(abyStates, mSelectedViewport);
                            } else {
                                if (abyStates != null && abyStates.length == 1)
                                    mOverlayManager.SetState(abyStates[0]);
                                else
                                    mOverlayManager.SetState(abyStates);
                            }
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlayManager.SetState");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });
    }

    private void initEquidistantAttachPointMinScaleBttn() {
        mEquidistantAttachPointsMinScaleBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final float minScale = mEquidistantAttachPointMinScaleEt.getFloat();
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            if (mSelectedViewport != null)
                                mOverlayManager.SetEquidistantAttachPointsMinScale(minScale, mSelectedViewport);
                            else
                                mOverlayManager.SetEquidistantAttachPointsMinScale(minScale);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlayManager.SetEquidistantAttachPointsMinScale");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });
    }

    private void initScaleFactorBttn() {
        mScaleFactorBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final float scale = mScaleFactorEt.getFloat();
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            if (mSelectedViewport != null)
                                mOverlayManager.SetScaleFactor(scale, mSelectedViewport);
                            else
                                mOverlayManager.SetScaleFactor(scale);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlayManager.SetScaleFactor");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });
    }

    private void initCollectionModeBttn() {
        mCollectionModeBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            if (mSelectedViewport != null)
                                mOverlayManager.SetCollectionsMode(mCollectionModeValue, mSelectedViewport);
                            else
                                mOverlayManager.SetCollectionsMode(mCollectionModeValue);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlayManager.SetCollectionsMode");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });
    }

    private void initCancelScaleModeBttn() {
        mCancelScaleModeBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                int scaleMode = 0;

                scaleMode += (mCancelScaleMode0Cb.isChecked() == true) ? MASK0 : 0;
                scaleMode += (mCancelScaleMode1Cb.isChecked() == true) ? MASK1 : 0;
                scaleMode += (mCancelScaleMode2Cb.isChecked() == true) ? MASK2 : 0;
                scaleMode += (mCancelScaleMode3Cb.isChecked() == true) ? MASK3 : 0;
                scaleMode += (mCancelScaleMode4Cb.isChecked() == true) ? MASK4 : 0;
                scaleMode += (mCancelScaleMode5Cb.isChecked() == true) ? MASK5 : 0;
                scaleMode += (mCancelScaleMode6Cb.isChecked() == true) ? MASK6 : 0;
                scaleMode += (mCancelScaleMode7Cb.isChecked() == true) ? MASK7 : 0;
                scaleMode += (mCancelScaleMode8Cb.isChecked() == true) ? MASK8 : 0;
                scaleMode += (mCancelScaleMode9Cb.isChecked() == true) ? MASK9 : 0;

                final int finalScaleMode = scaleMode;
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            if (mSelectedViewport != null)
                                mOverlayManager.SetCancelScaleMode(finalScaleMode, mSelectedViewport);
                            else
                                mOverlayManager.SetCancelScaleMode(finalScaleMode);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlayManager.SetCancelScaleMode");
                        } catch (Exception McEx) {

                        }
                    }
                });
            }
        });
    }

    private void initBlockedTransparencyModeBttn() {
        mBlockedTransparencyModeBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final boolean mBlockedTransparencyModeValue = mBlockedTransparencyModeCb.isChecked();
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            if (mSelectedViewport != null)
                                mOverlayManager.SetBlockedTransparencyMode(mBlockedTransparencyModeValue, mSelectedViewport);
                            else
                                mOverlayManager.SetBlockedTransparencyMode(mBlockedTransparencyModeValue);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlayManager.SetBlockedTransparencyMode");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });
    }

    private void initMoveIfBlockModeBttn() {
        mMoveIfBlockModeBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final boolean mMoveIfBlockModeValue = mMoveIfBlockModeCb.isChecked();
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            if (mSelectedViewport != null)
                                mOverlayManager.SetMoveIfBlockedMode(mMoveIfBlockModeValue, mSelectedViewport);
                            else
                                mOverlayManager.SetMoveIfBlockedMode(mMoveIfBlockModeValue);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlayManager.SetMoveIfBlockedMode");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });
    }

    private void initTopMostModeBttn() {
        mTopMostModeApplyBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final boolean mTopMostModeValue = mTopMostModeCb.isChecked();
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            if (mSelectedViewport != null)
                                mOverlayManager.SetTopMostMode(mTopMostModeValue, mSelectedViewport);
                            else
                                mOverlayManager.SetTopMostMode(mTopMostModeValue);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlayManager.SetTopMostMode");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });
    }
    //endregion

    //region initOMOperation

    private void initOMOperationBttns() {

        mLockSchemeListShowBttn = (Button) mRootView.findViewById(R.id.om_details_om_operations_lock_scheme_list_show_bttn);
        mLockConditionalSelectorListShowBttn = (Button) mRootView.findViewById(R.id.om_details_om_operations_lock_conditional_selector_list_show_bttn);
        mScreenTerraainItensConsistencyScaleStepsBttn = (Button) mRootView.findViewById(R.id.om_details_om_operations_screen_terrain_items_consistency_scale_steps_apply_bttn);
        mScreenTerraainItensConsistencyScaleStepsET = (EditText) mRootView.findViewById(R.id.om_details_om_operations_screen_terrain_items_consistency_scale_steps_et);

        try {
            mScreenTerraainItensConsistencyScaleStepsET.getText().clear();
            float[] scaleSteps = mOverlayManager.GetScreenTerrainItemsConsistencyScaleSteps();
            String txt = "";
            for (float step : scaleSteps)
                txt += Float.toString(step) + " ";
            mScreenTerraainItensConsistencyScaleStepsET.setText(txt);
        } catch (MapCoreException McEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), McEx, "IMcOverlayManager.GetScreenTerrainItemsConsistencyScaleSteps");
        } catch (Exception e) {
        }


        mScreenTerraainItensConsistencyScaleStepsBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final List<Float> lst = GetScreenTerrainItemsConsistencyScaleSteps();
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        float[] arr = new float[lst.size()];
                        for (int i = 0; i < arr.length; i++) {
                            arr[i] = lst.get(i);
                        }
                        try {
                            mOverlayManager.SetScreenTerrainItemsConsistencyScaleSteps(arr);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlayManager.SetScreenTerrainItemsConsistencyScaleSteps");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });
        mLockConditionalSelectorListShowBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                OpenShowLockFragment(IOverlayManagerShowLockFragment.EItemsType.ConditionalSelector.ordinal());
            }
        });

        mLockSchemeListShowBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                OpenShowLockFragment(IOverlayManagerShowLockFragment.EItemsType.ObjectScheme.ordinal());
            }
        });
    }

    private List<Float> GetScreenTerrainItemsConsistencyScaleSteps() {
        List<Float> Ret = new ArrayList<>();
        String[] ScaleSteps;
        String txt = mScreenTerraainItensConsistencyScaleStepsET.getText().toString();
        if (txt != "") {
            ScaleSteps = txt.trim().split(" ");
            if (ScaleSteps.length != 0) {
                float result;
                for (String step : ScaleSteps) {
                    result = Float.parseFloat(step);
                    Ret.add(result);
                }
            }
        }
        return Ret;
    }

    private void OpenShowLockFragment(final int eTypeValue) {
        String className = OverlayManagerShowLockFragment.class.getName();
        DialogFragment dialog = (DialogFragment) DialogFragment.instantiate(getActivity(), className);
        ((OverlayManagerShowLockFragment) dialog).getObject(mOverlayManager);

        Bundle args = new Bundle();
        args.putInt("type", eTypeValue);
        dialog.setArguments(args);

        dialog.setTargetFragment(OverlayManagerDetailsFragment.this, 1);
        dialog.show(getFragmentManager(), className);
    }
    //endregion

    // TODO: Rename method, update argument and hook method into UI event
    public void onButtonPressed(Uri uri) {
        if (mListener != null) {
            mListener.onFragmentInteraction(uri);
        }
    }

    @Override
    public void onResume() {
        super.onResume();
    }

    @Override
    public void onAttach(Context context) {
        super.onAttach(context);
        if (context instanceof OnFragmentInteractionListener) {
            mListener = (OnFragmentInteractionListener) context;
        } /*else {
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
        mOverlayManager = (IMcOverlayManager) obj;
    }

    @Override
    public void onSelectViewportFromList(IMcMapViewport mcSelectedViewport, boolean isChecked) {
        if(isChecked)
            mSelectedViewport = mcSelectedViewport;
        else
            mSelectedViewport = null;
        initOverlayOperationsValues();
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
