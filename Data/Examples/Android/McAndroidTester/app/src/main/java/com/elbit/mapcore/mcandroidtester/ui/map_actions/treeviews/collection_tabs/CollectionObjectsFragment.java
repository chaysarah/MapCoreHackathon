package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.collection_tabs;

import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AbsListView;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckedTextView;
import android.widget.EditText;
import android.widget.ListView;

import com.elbit.mapcore.Structs.SMcVector3D;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.managers.Manager_MCNames;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SpinnerWithLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.ThreeDVector;
import com.elbit.mapcore.mcandroidtester.utils.customviews.ViewportsList;

import java.util.ArrayList;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcCollection;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcConditionalSelector;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlay;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;

/**
 * Created by TC97803 on 07/09/2017.
 */

public class CollectionObjectsFragment extends Fragment implements FragmentWithObject {

    private IMcCollection mMcCollection;
    private IMcOverlayManager mMcOverlayManager;
    private ListView mCollectionObjectsLV;
    private ListView mAllObjectsLV;
    private View mView;
    private ArrayList<IMcObject> mMcOverlayObjects;
    private ArrayList<String> mMcOverlayObjectsNames;
    private ArrayList<IMcObject> mMcCollectionObjects;
    private ArrayList<String> mMcCollectionObjectsNames;

    private ListView mCollectionOverlaysLV;
    private ListView mAllOverlaysLV;

    private ArrayList<IMcOverlay> mMcAllOverlays;
    private ArrayList<String> mMcAllOverlaysNames;
    private ArrayList<IMcOverlay> mMcCollectionOverlays;
    private ArrayList<String> mMcCollectionOverlaysNames;

    private ArrayAdapter<String> mObjectCollectionAdapter;
    private ArrayAdapter<String> mObjectOverlayAdapter;
    private ArrayAdapter<String> mOverlayCollectionAdapter;
    private ArrayAdapter<String> mOverlayAdapter;

    private Button mBtnClearCollection;
    private Button mBtnAddObjectToCollection;
    private Button mBtnRemoveObjectFromCollection;
    private Button mBtnRemoveObjectsFromTheirOverlays;
    private Button mBtnAddOverlayToCollection;
    private Button mBtnRemoveOverlayFromCollection;
    private Button mBtnRemoveOverlaysFromOM;

    private ViewportsList mObjectsViewportsList;
    private ViewportsList mOverlaysViewportsList;

    private IMcObject mMcSelectedCollectionObject;
    private ArrayList<IMcObject> selectedObjectsOverlay = new ArrayList();
    private ArrayList<IMcOverlay> selectedOverlays = new ArrayList();
    private IMcOverlay[] mMcOverlays;
    private SpinnerWithLabel mObjectsVisibilitySpinner;
    private SpinnerWithLabel mOverlaysVisibilitySpinner;
    private IMcConditionalSelector.EActionOptions[] mcEActionOptionsValues = IMcConditionalSelector.EActionOptions.values();
    private IMcConditionalSelector.EActionOptions mcObjectsEActionOptionsSelected;
    private IMcConditionalSelector.EActionOptions mcOverlaysEActionOptionsSelected;
    private Button mObjectsVisibilityApplyBttn;
    private Button mOverlaysVisibilityApplyBttn;
    private Button mObjectsStateApplyBttn;
    private EditText mStateET;

    private ThreeDVector mMoveAllObjectsCv;
    private Button mMoveAllObjectsBttn;
    private IMcOverlay mMcSelectedCollectionOverlay;

    public static CollectionObjectsFragment newInstance() {
        CollectionObjectsFragment fragment = new CollectionObjectsFragment();
        return fragment;
    }

    public CollectionObjectsFragment() {
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
        mView = inflater.inflate(R.layout.fragment_collection_objects, container, false);
        mCollectionObjectsLV = (ListView) mView.findViewById(R.id.collection_objects_collection_objects_lv);
        mAllObjectsLV = (ListView) mView.findViewById(R.id.collection_objects_all_objects_lv);
        mBtnClearCollection = (Button) mView.findViewById(R.id.collection_objects_clear_collection_bttn);
        mBtnAddObjectToCollection = (Button) mView.findViewById(R.id.collection_objects_add_object_bttn);
        mBtnRemoveObjectFromCollection = (Button) mView.findViewById(R.id.collection_objects_remove_object_bttn);
        mBtnRemoveObjectsFromTheirOverlays = (Button) mView.findViewById(R.id.collection_objects_remove_objects_from_their_overlays_bttn);
        mBtnRemoveOverlaysFromOM = (Button) mView.findViewById(R.id.collection_overlays_remove_overlays_from_om_bttn);
        mObjectsViewportsList = (ViewportsList) mView.findViewById(R.id.collection_objects_vp_lv);
        mObjectsVisibilitySpinner = (SpinnerWithLabel) mView.findViewById(R.id.collection_objects_visibility_swl);
        mOverlaysVisibilitySpinner = (SpinnerWithLabel) mView.findViewById(R.id.collection_overlays_visibility_swl);
        mObjectsVisibilityApplyBttn = (Button) mView.findViewById(R.id.collection_objects_visibility_bttn);
        mOverlaysVisibilityApplyBttn = (Button) mView.findViewById(R.id.collection_overlays_visibility_bttn);
        mObjectsStateApplyBttn = (Button) mView.findViewById(R.id.collection_objects_state_bttn);
        mStateET = (EditText) mView.findViewById(R.id.collection_objects_state_et);
        mMoveAllObjectsCv = (ThreeDVector) mView.findViewById(R.id.collection_objects_move_all_objects);
        mMoveAllObjectsBttn = (Button) mView.findViewById(R.id.collection_objects_move_all_objects_bttn);

        mAllOverlaysLV = (ListView) mView.findViewById(R.id.collection_overlays_all_overlays_lv);
        mCollectionOverlaysLV = (ListView) mView.findViewById(R.id.collection_overlays_collection_overlays_lv);

        mBtnAddOverlayToCollection = (Button) mView.findViewById(R.id.collection_overlays_add_overlay_bttn);
        mBtnRemoveOverlayFromCollection = (Button) mView.findViewById(R.id.collection_overlays_remove_overlay_bttn);
        mBtnRemoveOverlaysFromOM = (Button) mView.findViewById(R.id.collection_overlays_remove_overlays_from_om_bttn);
        mOverlaysViewportsList = (ViewportsList) mView.findViewById(R.id.collection_overlays_vp_lv);

        initControls();
        initObjectViewportControls();
        initOverlayViewportControls();
        return mView;
    }

    private void initControls() {

        mObjectsViewportsList.initViewportsList(this,
                Consts.ListType.SINGLE_CHECK,
                ListView.CHOICE_MODE_SINGLE);


        mOverlaysViewportsList.initViewportsList(this,
                Consts.ListType.SINGLE_CHECK,
                ListView.CHOICE_MODE_SINGLE);

        initCollectionLists();

        mBtnClearCollection.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                ArrayList<IMcObject> objects = new ArrayList<IMcObject>(mMcCollectionObjects);
                RemoveObjectsFromCollection(objects);

                ArrayList<IMcOverlay> overlays = new ArrayList<IMcOverlay>(mMcCollectionOverlays);
                RemoveOverlaysFromCollection(overlays);
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            mMcCollection.Clear();
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "Clear Collection");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });

        mBtnAddObjectToCollection.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                final IMcObject[] objects = selectedObjectsOverlay.toArray(new IMcObject[selectedObjectsOverlay.size()]);
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            mMcCollection.AddObjects(objects);

                            for (IMcObject object : selectedObjectsOverlay) {
                                String name = Manager_MCNames.getInstance().getNameByObject(object);
                                mMcCollectionObjects.add(object);
                                mMcCollectionObjectsNames.add(name);

                                mMcOverlayObjects.remove(object);
                                mMcOverlayObjectsNames.remove(name);
                            }

                            getActivity().runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    mObjectCollectionAdapter.notifyDataSetChanged();
                                    mObjectOverlayAdapter.notifyDataSetChanged();
                                    selectedObjectsOverlay.clear();
                                    mAllObjectsLV.clearChoices();
                                }
                            });
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "AddObject to Collection");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });

        mBtnRemoveObjectFromCollection.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        if (mMcSelectedCollectionObject != null) {

                            try {
                                mMcCollection.RemoveObjectFromCollection(mMcSelectedCollectionObject);
                                getActivity().runOnUiThread(new Runnable() {
                                    @Override
                                    public void run() {
                                        ArrayList<IMcObject> arr = new ArrayList<>();
                                        arr.add(mMcSelectedCollectionObject);
                                        RemoveObjectsFromCollection(arr);
                                    }
                                });
                            } catch (MapCoreException e) {
                                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "RemoveObjectFromCollection");
                            } catch (Exception e) {
                                e.printStackTrace();
                            }
                            mMcSelectedCollectionObject = null;
                        }
                    }
                });
            }
        });

        mBtnRemoveObjectsFromTheirOverlays.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            mMcCollection.RemoveObjectsFromTheirOverlays();
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "RemoveObjectsFromTheirOverlays");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });

        mBtnAddOverlayToCollection.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            IMcOverlay[] overlays = selectedOverlays.toArray(new IMcOverlay[selectedOverlays.size()]);

                            mMcCollection.AddOverlays(overlays);
                            for (IMcOverlay overlay : selectedOverlays) {
                                String name = Manager_MCNames.getInstance().getNameByObject(overlay);
                                mMcCollectionOverlays.add(overlay);
                                mMcCollectionOverlaysNames.add(name);

                                mMcAllOverlays.remove(overlay);
                                mMcAllOverlaysNames.remove(name);
                            }
                            getActivity().runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    mOverlayCollectionAdapter.notifyDataSetChanged();
                                    mOverlayAdapter.notifyDataSetChanged();
                                    selectedOverlays.clear();
                                    mAllOverlaysLV.clearChoices();
                                }
                            });

                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "AddOverlays");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });

        mBtnRemoveOverlayFromCollection.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        if (mMcSelectedCollectionOverlay != null) {
                            try {

                                ArrayList<IMcOverlay> arr = new ArrayList<>();
                                arr.add(mMcSelectedCollectionOverlay);
                                RemoveOverlaysFromCollection(arr);
                                mMcCollection.RemoveOverlayFromCollection(mMcSelectedCollectionOverlay);

                            } catch (MapCoreException e) {
                                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "RemoveOverlayFromCollection");
                            } catch (Exception e) {
                                e.printStackTrace();
                            }
                        }
                    }
                });
            }
        });

        mBtnRemoveOverlaysFromOM.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            mMcCollection.RemoveOverlaysFromOverlayManager();
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "RemoveOverlaysFromOverlayManager");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });
    }

    private void RemoveObjectsFromCollection(ArrayList<IMcObject> objectsToRemove) {
        for (IMcObject obj : objectsToRemove) {
            String name = Manager_MCNames.getInstance().getNameByObject(obj);

            mMcOverlayObjects.add(obj);
            mMcOverlayObjectsNames.add(name);

            mMcCollectionObjects.remove(obj);
            mMcCollectionObjectsNames.remove(name);
        }
        getActivity().runOnUiThread(new Runnable() {
            @Override
            public void run() {
                mObjectCollectionAdapter.notifyDataSetChanged();
                mObjectOverlayAdapter.notifyDataSetChanged();
                mCollectionObjectsLV.clearChoices();
            }
        });
    }

    private void RemoveOverlaysFromCollection(ArrayList<IMcOverlay> overlaysToRemove) {
        for (IMcOverlay overlay : overlaysToRemove) {
            String name = Manager_MCNames.getInstance().getNameByObject(overlay);

            mMcAllOverlays.add(overlay);
            mMcAllOverlaysNames.add(name);

            mMcCollectionOverlays.remove(overlay);
            mMcCollectionOverlaysNames.remove(name);
        }
        getActivity().runOnUiThread(new Runnable() {
            @Override
            public void run() {
                mOverlayCollectionAdapter.notifyDataSetChanged();
                mOverlayAdapter.notifyDataSetChanged();
                mCollectionOverlaysLV.clearChoices();
            }
        });
    }

    @Override
    public void setObject(Object obj) {
        mMcCollection = (IMcCollection) obj;
        try {
            mMcOverlayManager = mMcCollection.GetOverlayManager();
        }catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "GetObjects");
        }  catch (Exception e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mMcCollection));
    }

    private void initCollectionLists() {

        mMcCollectionObjects = new ArrayList<>();
        mMcCollectionObjectsNames = new ArrayList<>();
        IMcObject[] collectionObjects = null;
        try {
            collectionObjects = mMcCollection.GetObjects();
        } catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "GetObjects");
        } catch (Exception e) {
            e.printStackTrace();
        }
        if (collectionObjects != null) {
            for (IMcObject obj : collectionObjects) {
                mMcCollectionObjects.add(obj);
                mMcCollectionObjectsNames.add(Manager_MCNames.getInstance().getNameByObject(obj));
            }
        }
        mObjectCollectionAdapter = new ArrayAdapter(getContext(),
                R.layout.radio_bttn_list_item,
                mMcCollectionObjectsNames);

        mCollectionObjectsLV.setAdapter(mObjectCollectionAdapter);
        mCollectionObjectsLV.setChoiceMode(AbsListView.CHOICE_MODE_SINGLE);
        //Funcs.setListViewHeightBasedOnChildren(mCollectionObjectsLV);

        //setListViewHeight(mCollectionObjectsLV,150);
        mCollectionObjectsLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                mMcSelectedCollectionObject = mMcCollectionObjects.get(position);
            }
        });

        mMcCollectionOverlays = new ArrayList<>();
        mMcCollectionOverlaysNames = new ArrayList<>();

        IMcOverlay[] collectionOverlays = null;
        try {
            collectionOverlays = mMcCollection.GetOverlays();
        } catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "IMcCollection.GetOverlays");
        } catch (Exception e) {
            e.printStackTrace();
        }
        if (collectionOverlays != null) {
            for (IMcOverlay obj : collectionOverlays) {
                mMcCollectionOverlays.add(obj);
                mMcCollectionOverlaysNames.add(Manager_MCNames.getInstance().getNameByObject(obj));
            }
        }
        mOverlayCollectionAdapter = new ArrayAdapter(getContext(),
                R.layout.radio_bttn_list_item,
                mMcCollectionOverlaysNames);

        mCollectionOverlaysLV.setAdapter(mOverlayCollectionAdapter);
        mCollectionOverlaysLV.setChoiceMode(AbsListView.CHOICE_MODE_SINGLE);
        //Funcs.setListViewHeightBasedOnChildren(mCollectionOverlaysLV);

        mCollectionOverlaysLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                mMcSelectedCollectionOverlay = mMcCollectionOverlays.get(position);
            }
        });

        mMcAllOverlays = new ArrayList<>();
        mMcAllOverlaysNames = new ArrayList<>();

        try {
            mMcOverlays = mMcOverlayManager.GetOverlays();
        } catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "IMcOverlayManager.GetOverlays");
        } catch (Exception e) {
            e.printStackTrace();
        }
        if (mMcOverlays != null) {
            mMcOverlayObjects = new ArrayList<>();
            mMcOverlayObjectsNames = new ArrayList<>();
            for (IMcOverlay overlay : mMcOverlays) {
                if (!mMcCollectionOverlays.contains(overlay)) {
                    mMcAllOverlays.add(overlay);
                    mMcAllOverlaysNames.add(Manager_MCNames.getInstance().getNameByObject(overlay));
                }
                IMcObject[] objects = null;
                try {
                    objects = overlay.GetObjects();
                } catch (MapCoreException mcEx) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "IMcOverlay.GetObjects");
                } catch (Exception e) {
                    e.printStackTrace();
                }

                if (objects != null) {
                    for (IMcObject obj : objects) {
                        if (!mMcCollectionObjects.contains(obj)) {
                            mMcOverlayObjectsNames.add(Manager_MCNames.getInstance().getNameByObject(obj));
                            mMcOverlayObjects.add(obj);
                        }
                    }
                }
            }

            mObjectOverlayAdapter = new ArrayAdapter(getContext(),
                    R.layout.checkable_list_item,
                    mMcOverlayObjectsNames);
            mAllObjectsLV.setAdapter(mObjectOverlayAdapter);
            mAllObjectsLV.setChoiceMode(AbsListView.CHOICE_MODE_MULTIPLE);
            //Funcs.setListViewHeightBasedOnChildren(mAllObjectsLV);
            //setListViewHeight(mAllObjectsLV,150);
            //setListViewHeight(mCollectionObjectsLV,150);
            mAllObjectsLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                @Override
                public void onItemClick(AdapterView<?> adapterView, View view, int position, long l) {
                    IMcObject item = mMcOverlayObjects.get(position);
                    if (((CheckedTextView) view).isChecked()) {
                        if (!selectedObjectsOverlay.contains(item))
                            selectedObjectsOverlay.add(item);
                        else {
                            if (selectedObjectsOverlay.contains(item))
                                selectedObjectsOverlay.remove(item);
                        }
                    }
                }
            });

            mOverlayAdapter = new ArrayAdapter(getContext(),
                    R.layout.checkable_list_item,
                    mMcAllOverlaysNames);
            mAllOverlaysLV.setAdapter(mOverlayAdapter);
            mAllOverlaysLV.setChoiceMode(AbsListView.CHOICE_MODE_MULTIPLE);
            //Funcs.setListViewHeightBasedOnChildren(mAllOverlaysLV);
            mAllOverlaysLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                @Override
                public void onItemClick(AdapterView<?> adapterView, View view, int position, long l) {
                    IMcOverlay item = mMcAllOverlays.get(position);
                    if (((CheckedTextView) view).isChecked()) {
                        if (!selectedOverlays.contains(item))
                            selectedOverlays.add(item);
                        else {
                            if (selectedOverlays.contains(item))
                                selectedOverlays.remove(item);
                        }
                    }
                }
            });
        }
    }

    private void initObjectViewportControls() {
        ArrayAdapter visibilityAdapter = new ArrayAdapter(getContext(), android.R.layout.simple_spinner_item, mcEActionOptionsValues);
        mObjectsVisibilitySpinner.setAdapter(visibilityAdapter);
        mObjectsVisibilitySpinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                mcObjectsEActionOptionsSelected = mcEActionOptionsValues[i];
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });

        mObjectsVisibilityApplyBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            if (mObjectsViewportsList.mSelectedViewport == null) {
                                mMcCollection.SetObjectsVisibilityOption(mcObjectsEActionOptionsSelected);
                            } else {
                                mMcCollection.SetObjectsVisibilityOption(mcObjectsEActionOptionsSelected,
                                        mObjectsViewportsList.mSelectedViewport);
                            }
                        } catch (MapCoreException mcEx) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "SetObjectsVisibilityOption");
                        } catch (Exception ex) {
                            ex.printStackTrace();
                        }
                    }
                });
            }
        });

        mObjectsStateApplyBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                final byte[] states = Funcs.getStatesFromString(String.valueOf(mStateET.getText()));
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            if (mObjectsViewportsList.mSelectedViewport != null) {
                                if (states != null && states.length == 1)
                                    mMcCollection.SetObjectsState(states[0], mObjectsViewportsList.mSelectedViewport);
                                else
                                    mMcCollection.SetObjectsState(states, mObjectsViewportsList.mSelectedViewport);
                            } else {
                                if (states != null && states.length == 1)
                                    mMcCollection.SetObjectsState(states[0]);
                                else
                                    mMcCollection.SetObjectsState(states);
                            }
                        } catch (MapCoreException mcEx) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "SetObjectsState");
                        } catch (Exception ex) {
                            ex.printStackTrace();
                        }
                    }
                });
            }
        });

        mMoveAllObjectsBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                final SMcVector3D vector3D = mMoveAllObjectsCv.getVector3D();
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            mMcCollection.MoveObjects(vector3D);
                        } catch (MapCoreException mcEx) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "MoveObjects");
                        } catch (Exception ex) {
                            ex.printStackTrace();
                        }
                    }
                });
            }
        });
    }

    private void initOverlayViewportControls() {
        ArrayAdapter visibilityAdapter = new ArrayAdapter(getContext(), android.R.layout.simple_spinner_item, mcEActionOptionsValues);
        mOverlaysVisibilitySpinner.setAdapter(visibilityAdapter);
        mOverlaysVisibilitySpinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                mcOverlaysEActionOptionsSelected = mcEActionOptionsValues[i];
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });

        mOverlaysVisibilityApplyBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            if (mOverlaysViewportsList.mSelectedViewport == null) {
                                mMcCollection.SetOverlaysVisibilityOption(mcOverlaysEActionOptionsSelected);
                            } else {
                                mMcCollection.SetOverlaysVisibilityOption(mcOverlaysEActionOptionsSelected, mOverlaysViewportsList.mSelectedViewport);
                            }
                        } catch (MapCoreException mcEx) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "SetOverlaysVisibilityOption");
                        } catch (Exception ex) {
                            ex.printStackTrace();
                        }
                    }
                });
            }
        });
    }
}
