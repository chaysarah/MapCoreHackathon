package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlay_manager_treeview;

import android.content.Context;
import android.content.DialogInterface;
import android.net.Uri;
import android.os.Bundle;
import androidx.annotation.Nullable;
import androidx.fragment.app.DialogFragment;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.SubMenu;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.PopupMenu;
import android.widget.Toast;
import android.widget.ToggleButton;

import com.elbit.mapcore.Enums.EMcPointCoordSystem;
import com.elbit.mapcore.General.ObjectRef;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectLocation;
import com.elbit.mapcore.Structs.SMcVector3D;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.interfaces.IObjectListBaseOnObjectSchemeItemFragmentCallback;
import com.elbit.mapcore.mcandroidtester.interfaces.ITreeView;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_AMCTMapForm;
import com.elbit.mapcore.mcandroidtester.managers.Manager_MCNames;
import com.elbit.mapcore.mcandroidtester.managers.ObjectWorld.Manager_MCObjectSchemeItem;
import com.elbit.mapcore.mcandroidtester.managers.ObjectWorld.Manager_MCOverlayManager;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.collection_tabs.CollectionTabsFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.object_tabs.ObjectTabsFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlay_tabs.OverlayTabsFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlaymanager_tabs.OverlayManagerTabsFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.objectscheme_tabs.SchemesTabsFragment;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.McObjectFunc;
import com.elbit.mapcore.mcandroidtester.utils.customviews.ChangingNamesDialogFragment;
import com.elbit.mapcore.mcandroidtester.utils.treeview.holder.IconTreeItemHolder;
import com.elbit.mapcore.mcandroidtester.utils.treeview.model.TreeNode;
import com.elbit.mapcore.mcandroidtester.utils.treeview.view.AndroidTreeView;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import com.elbit.mapcore.General.CMcEnumBitField;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcBlockedConditionalSelector;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcBooleanConditionalSelector;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcCollection;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcConditionalSelector;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcLocationConditionalSelector;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectScheme;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeNode;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectStateConditionalSelector;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlay;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcScaleConditionalSelector;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcSymbolicItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcViewportConditionalSelector;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link OverlayManagerTreeViewFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link OverlayManagerTreeViewFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class OverlayManagerTreeViewFragment extends Fragment implements ITreeView,ChangingNamesDialogFragment.OnNameChangedListener,IObjectListBaseOnObjectSchemeItemFragmentCallback, IOverlayManagerCreatedCallback, ICloneObjectCallback, ISelectOverlayCallback, ISelectSchemeCallback {

    private OnFragmentInteractionListener mListener;
    private IMcMapViewport mCurViewport;
    private TreeNode mRoot;
    private View mRootView;
    private Button mNewCollectionBttn;
    private ToggleButton mShowCoordTBttn;
    private AndroidTreeView mOverlayManagerTreeView;
    private List<IMcObjectSchemeNode> testConnectItems = new ArrayList<>();
    private TreeNode mSelectedNode;
    public static int SelectedNodeHashCode = 0;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment TerrainLayersTreeViewFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static OverlayManagerTreeViewFragment newInstance(String param1, String param2) {
        OverlayManagerTreeViewFragment fragment = new OverlayManagerTreeViewFragment();
        return fragment;
    }

    public OverlayManagerTreeViewFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setHasOptionsMenu(true);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        mRootView = inflater.inflate(R.layout.fragment_overlay_manager_tree_view, container, false);
        inflateViews();
        initViews();
        if (savedInstanceState != null)
            SelectedNodeHashCode = savedInstanceState.getInt(AndroidTreeView.SELECTED_TREE_VIEW_NODE);
        else
            SelectedNodeHashCode = 0;

        return mRootView;
    }

    private void inflateViews() {
        mShowCoordTBttn = (ToggleButton) mRootView.findViewById(R.id.om_tv_show_coordinates_tbttn);
        mShowCoordTBttn.setVisibility(View.GONE);
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putInt(AndroidTreeView.SELECTED_TREE_VIEW_NODE, SelectedNodeHashCode);
    }

    private void initViews() {
        initShowCoordTBttn();
    }

    private void initShowCoordTBttn() {
        mShowCoordTBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                boolean on = ((ToggleButton) view).isChecked();
                if (on) {

                } else {

                }

            }
        });
    }

    private void openLocationPointsFragment(LocationPointsFragment.NewObjectOperation newObjectOperation) {
        FragmentManager fragmentManager = getFragmentManager();
        FragmentTransaction transaction = fragmentManager.beginTransaction();
        transaction.hide(this);
        LocationPointsFragment locationPointsFragment = new LocationPointsFragment();
        locationPointsFragment.SetNewObjectOperation(newObjectOperation);
        transaction.add(R.id.fragment_container,locationPointsFragment , LocationPointsFragment.class.getSimpleName());
        transaction.addToBackStack(LocationPointsFragment.class.getSimpleName());
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
        } /*else {
            throw new RuntimeException(context.toString()
                    + " must implement OnFragmentInteractionListener");
        }*/
        if (context instanceof MapsContainerActivity) {
            this.mCurViewport = Manager_AMCTMapForm.getInstance().getCurViewport();
            ((MapsContainerActivity) context).mCurFragmentTag = OverlayManagerTreeViewFragment.class.getSimpleName();
        }
    }

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        setTitle();
    }

    private void setTitle() {
        getActivity().setTitle("overlay manager tree view ");
    }

    private TreeNode.TreeNodeLongClickListener nodeLongClickListener = new TreeNode.TreeNodeLongClickListener() {
        @Override
        public boolean onLongClick(TreeNode node, Object value) {
            IconTreeItemHolder.IconTreeItem item = (IconTreeItemHolder.IconTreeItem) value;
            startRelatedFragment(item);
            Toast.makeText(getActivity(), "Long click: " + item.mText, Toast.LENGTH_SHORT).show();
            return true;
        }
    };

    private void startRelatedFragment(IconTreeItemHolder.IconTreeItem item) {
        try {

            FragmentWithObject fragment = (FragmentWithObject) Fragment.instantiate(getActivity(), item.mFragmentName);
            fragment.setObject(item.mImcObj);
            FragmentTransaction transaction = getFragmentManager().beginTransaction();
            transaction.hide(this);
            String fragSimpleName = item.mFragmentName.substring(item.mFragmentName.lastIndexOf(".") + 1);
            transaction.add(R.id.fragment_container, (Fragment) fragment, fragSimpleName);
            transaction.addToBackStack(fragSimpleName);
            transaction.commit();

        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void createOverlayManagerOverlaysTree() {
        if (mRoot != null) {
            mRoot.getViewHolder().getTreeView().removeAllNodes();
        }
        mRoot = TreeNode.root();
        HashMap<Object, Integer> overlayManagerHashMap = Manager_MCOverlayManager.getInstance().getAllParams();
        for (Object overlayManager : overlayManagerHashMap.keySet()) {
            try {
                TreeNode parentVP = new TreeNode(new IconTreeItemHolder.IconTreeItem(
                        R.string.ic_archive,
                        Manager_MCNames.getInstance().getNameByObject(overlayManager),
                        IconTreeItemHolder.IconTreeItem.TreeViewType.OM,
                        OverlayManagerTabsFragment.class.getCanonicalName(),
                        overlayManager,
                        R.menu.treeview_overlay_manager_item,
                        getOverlayManagerMenuClickListener((IMcOverlayManager) overlayManager)));
                mRoot.addChild(parentVP);
                addOverlayManagerNodesTree((IMcOverlayManager) overlayManager, parentVP);
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
        mOverlayManagerTreeView = new AndroidTreeView(getActivity(), mRoot);
        mOverlayManagerTreeView.setDefaultAnimation(false);
        mOverlayManagerTreeView.setDefaultContainerStyle(R.style.TreeNodeStyleCustom);
        mOverlayManagerTreeView.setDefaultViewHolder(IconTreeItemHolder.class);
        mOverlayManagerTreeView.setDefaultNodeLongClickListener(nodeLongClickListener);
        ((LinearLayout) getView().findViewById(R.id.overlay_manager_tree_view_container_fl)).addView(mOverlayManagerTreeView.getView());

        mOverlayManagerTreeView.expandNodeTillRoot(SelectedNodeHashCode);
 }

    private PopupMenu.OnMenuItemClickListener getOverlayManagerMenuClickListener(final IMcOverlayManager overlayManager) {
        PopupMenu.OnMenuItemClickListener menuItemClickListener = new PopupMenu.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(MenuItem item) {
                int id = item.getItemId();
                if(id == R.id.tv_om_conditional_selector_blocked_conditional_selector) {
                    handleBlockedConditionalSelector(overlayManager);
                }
                else if (id == R.id.tv_om_conditional_selector_boolean_conditional_selector) {
                    handleBooleanConditionalSelector(overlayManager);
                }
                else if (id == R.id.tv_om_conditional_selector_location_conditional_selector) {
                    handleLocationConditionalSelector(overlayManager);
                }
                else if(id == R.id.tv_om_conditional_selector_object_state_conditional_selector) {
                    handleObjectStateConditionalSelector(overlayManager);
                }
                else if(id == R.id.tv_om_conditional_selector_viewport_conditional_selector){
                    handleViewportConditionalSelector(overlayManager);
                }
                else if(id == R.id.tv_om_conditional_selector_scale_conditional_selector) {
                    handleScaleConditionalSelector(overlayManager);
                }
                else if(id == R.id.tv_om_rename) {
                    handleRename(overlayManager);
                }
                else if(id == R.id.tv_om_delete_name) {
                    handleDeleteName(overlayManager);
                }
                else if(id == R.id.tv_om_create_overlay) {
                    handleCreateOverlay(overlayManager);
                }
                else if(id ==  R.id.tv_om_create_collection){
                    handleCreateCollection(overlayManager);
                }
                else if(id == R.id.tv_om_new_object_scheme_with_one_location) {
                    handleCreateObjectScheme(overlayManager, NewObjectSchemeFragment.NewObjectSchemeOperation.SchemeWithOneLocation);
                }
                else if(id == R.id.tv_om_new_object_scheme_with_one_location_and_one_item){
                    handleCreateObjectScheme(overlayManager, NewObjectSchemeFragment.NewObjectSchemeOperation.SchemeWithOneLocationAndOneItem);
                }
                else if(id ==  R.id.tv_om_remove_overlay_manager) {
                    handleRemoveOverlayManager(overlayManager);
                }
                return true;
            }
        };
        return menuItemClickListener;
    }

    private PopupMenu.OnMenuItemClickListener getCollectionMenuClickListener(final IMcCollection collection) {
        PopupMenu.OnMenuItemClickListener menuItemClickListener = new PopupMenu.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(MenuItem item) {
                int id = item.getItemId();
                if(id == R.id.tv_collection_rename) {
                        handleRename(collection);
                }
                else if (id == R.id.tv_collection_delete_name){
                        handleDeleteName(collection);
                }
                return true;
            }
        };
        return menuItemClickListener;
    }

    private PopupMenu.OnMenuItemClickListener getOverlayMenuClickListener(final IMcOverlay overlay) {
        PopupMenu.OnMenuItemClickListener menuItemClickListener = new PopupMenu.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(MenuItem item) {
                int id = item.getItemId();
                if(id ==  R.id.tv_overlay_remove_overlay) {
                        handleRemoveOverlay(overlay);
                }
                else if(id == R.id.tv_overlay_remove_all_objects) {
                    handleRemoveAllObjectsFromOverlay(overlay);
                }
                else if(id == R.id.tv_overlay_set_active) {
                    handleSetActive(overlay);
                }
                else if(id == R.id.tv_overlay_rename) {
                    handleRename(overlay);
                }
                else if(id == R.id.tv_overlay_delete_name) {
                        handleDeleteName(overlay);
                }
                return true;
            }
        };
        return menuItemClickListener;
    }

    private PopupMenu.OnMenuItemClickListener getConditionalSelectorMenuClickListener(final IMcConditionalSelector mcConditionalSelector) {
        PopupMenu.OnMenuItemClickListener menuItemClickListener = new PopupMenu.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(MenuItem item) {
                int id = item.getItemId();
                    if(id == R.id.tv_selector_remove_selector) {
                        handleRemoveConditionalSelector(mcConditionalSelector);
                    }
                    else if(id == R.id.tv_selector_rename) {
                        handleRename(mcConditionalSelector);
                    }
                    else if(id == R.id.tv_selector_delete_name) {
                        handleDeleteName(mcConditionalSelector);
                }
                return true;
            }
        };
        return menuItemClickListener;
    }

    private void handleRemoveOverlayManager(final IMcOverlayManager overlayManager) {
        int numViewportsUsingOM = 0;
        ArrayList<IMcMapViewport> viewports = Manager_AMCTMapForm.getInstance().getAllImcViewports();
        for (IMcMapViewport viewport : viewports) {
            try {
                if (viewport.GetOverlayManager() == overlayManager)
                    numViewportsUsingOM++;
            } catch (MapCoreException e) {
                AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "GetOverlayManager");
                e.printStackTrace();
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
        if (numViewportsUsingOM > 0) {
            AlertMessages.ShowErrorMessage(getContext(), "RemoveOwnedForm Overlay Manager", "Overlay Manager can't be removed In order to remove it, you have to close all viewports that using it first! \n RemoveOwnedForm Overlay Manager");
        }
        else
        {
            Manager_MCOverlayManager.getInstance().RemoveOverlayManager(overlayManager);
            Funcs.runMapCoreFunc(new Runnable() {
                @Override
                public void run() {
                    overlayManager.Release();

                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            createOverlayManagerOverlaysTree();
                        }
                    });
                }});


        }
    }

    private void handleCreateOverlay(final IMcOverlayManager overlayManager) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    IMcOverlay.Static.Create(overlayManager);
                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            createOverlayManagerOverlaysTree();
                        }
                    });

                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "IMcOverlay.Static.Create");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void handleCreateCollection(final IMcOverlayManager overlayManager) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    IMcCollection.Static.Create(overlayManager);
                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            createOverlayManagerOverlaysTree();
                        }
                    });

                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "IMcCollection.Static.Create");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void handleCreateObjectScheme(IMcOverlayManager overlayManager, NewObjectSchemeFragment.NewObjectSchemeOperation newObjectSchemeOperation) {
        FragmentManager fragmentManager = getFragmentManager();
        FragmentTransaction transaction = fragmentManager.beginTransaction();
        transaction.hide(this);
        NewObjectSchemeFragment newObjectSchemeFragment = new NewObjectSchemeFragment();
        newObjectSchemeFragment.SetNewObjectSchemeParams(overlayManager, newObjectSchemeOperation);
        transaction.add(R.id.fragment_container, newObjectSchemeFragment, NewObjectSchemeFragment.class.getSimpleName());
        transaction.addToBackStack(NewObjectSchemeFragment.class.getSimpleName());
        transaction.commit();

    }

    private void handleRemoveOverlay(final IMcOverlay overlay) {
        try {
            IMcOverlayManager OM = overlay.GetOverlayManager();

            //In case that the removed overlay is the active overlay, the active overlay change to Null.
            if (Manager_MCOverlayManager.getInstance().getHMOverlayManagerOverlay() == overlay)
                Manager_MCOverlayManager.getInstance().updateOverlayManager(OM, null);

            Funcs.runMapCoreFunc(new Runnable() {
                @Override
                public void run() {
                    try {
                        overlay.Remove();
                        overlay.Release();
                        getActivity().runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                createOverlayManagerOverlaysTree();
                            }
                        });
                    } catch (MapCoreException e) {
                        AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "IMcOverlay.Remove");
                        e.printStackTrace();
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            });
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "IMcOverlay.GetOverlayManager");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void handleRemoveConditionalSelector(final IMcConditionalSelector condSelector) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    IMcOverlayManager OM = condSelector.GetOverlayManager();
                    OM.SetConditionalSelectorLock(condSelector, false);
                    condSelector.Release();
                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            createOverlayManagerOverlaysTree();
                        }
                    });

                } catch (MapCoreException ex) {
                    AlertMessages.ShowMapCoreErrorMessage(getActivity(), ex, "IMcOverlay.GetOverlayManager");
                    ex.printStackTrace();
                } catch (Exception ex) {
                    ex.printStackTrace(); }
            }
        });
    }

    private void handleRemoveAllObjectsFromOverlay(final IMcOverlay overlay) {

        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    IMcObject[] objectsToRemove = overlay.GetObjects();

                    for(int i=0;i<objectsToRemove.length;i++)
                    {
                        Funcs.removeObject(objectsToRemove[i], getActivity());
                    }
                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            createOverlayManagerOverlaysTree();
                        }
                    });

                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "IMcOverlay.GetObjects");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }


    private void handleSetActive(IMcOverlay overlay)
    {
        try {
            Manager_MCOverlayManager.getInstance().updateOverlayManager(overlay.GetOverlayManager(),overlay);
        }
     catch (MapCoreException e) {
        AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "GetOverlayManager()");
        e.printStackTrace();
    } catch (Exception e) {
        e.printStackTrace();
    }
    }

    private void handleDeleteName(final Object mcObj) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                if (Manager_MCNames.getInstance().removeName(mcObj)) {
                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            createOverlayManagerOverlaysTree();
                        }
                    });
                }
            }
        });
    }

    private void handleRename(Object mcObj) {
        DialogFragment changingNameFragment = ChangingNamesDialogFragment.newInstance("", "");
        changingNameFragment.show(getChildFragmentManager(), ChangingNamesDialogFragment.class.getSimpleName());
        ((FragmentWithObject) changingNameFragment).setObject(mcObj);
    }

    private void handleLocationConditionalSelector(final IMcOverlayManager overlayManager) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    final IMcLocationConditionalSelector locationConditionalSelector = IMcLocationConditionalSelector.Static.Create(overlayManager);
                    overlayManager.SetConditionalSelectorLock(locationConditionalSelector, true);
                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            Funcs.addFragmentAndHidePrevious(getFragmentManager(), new LocationConditionalSelectorFragment(), OverlayManagerTreeViewFragment.this, R.id.fragment_container, locationConditionalSelector);
                        }
                    });
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "IMcViewportConditionalSelector.Static.Create/SetConditionalSelectorLock");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }});
    }

    private void handleViewportConditionalSelector(final IMcOverlayManager overlayManager) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    int[] viewportIDs = new int[0];
                    final IMcViewportConditionalSelector viewportConditionalSelector = IMcViewportConditionalSelector.Static.Create(overlayManager, new CMcEnumBitField<>(IMcViewportConditionalSelector.EViewportTypeFlags.EVT_ALL_VIEWPORTS), new CMcEnumBitField<>(IMcViewportConditionalSelector.EViewportCoordinateSystem.EVCS_ALL_COORDINATE_SYSTEMS), viewportIDs, false);
                    overlayManager.SetConditionalSelectorLock(viewportConditionalSelector, true);
                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            Funcs.addFragmentAndHidePrevious(getFragmentManager(), new ViewportConditionalSelectorFragment(), OverlayManagerTreeViewFragment.this, R.id.fragment_container, viewportConditionalSelector);
                        }
                    });
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "IMcViewportConditionalSelector.Static.Create/SetConditionalSelectorLock");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }});
    }

    private void handleObjectStateConditionalSelector(final IMcOverlayManager overlayManager) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    final IMcObjectStateConditionalSelector objectStateConditionalSelector = IMcObjectStateConditionalSelector.Static.Create(overlayManager);
                    overlayManager.SetConditionalSelectorLock(objectStateConditionalSelector, true);
                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            Funcs.addFragmentAndHidePrevious(getFragmentManager(), new ObjectStateConditionalSelectorFragment(), OverlayManagerTreeViewFragment.this, R.id.fragment_container, objectStateConditionalSelector);
                        }
                    });
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "IMcObjectStateConditionalSelector.Static.Create/SetConditionalSelectorLock");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void handleScaleConditionalSelector(final IMcOverlayManager overlayManager) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    final IMcScaleConditionalSelector scaleSelector = IMcScaleConditionalSelector.Static.Create(overlayManager, 0, Float.MAX_VALUE, 0, 0);
                    overlayManager.SetConditionalSelectorLock(scaleSelector, true);
                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            Funcs.addFragmentAndHidePrevious(getFragmentManager(), new ScaleConditionalSelectorFragment(), OverlayManagerTreeViewFragment.this, R.id.fragment_container, scaleSelector);
                        }
                    });
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "IMcScaleConditionalSelector.Static.Create/SetConditionalSelectorLock");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void handleBooleanConditionalSelector(final IMcOverlayManager overlayManager) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    final IMcBooleanConditionalSelector booleanSelector = IMcBooleanConditionalSelector.Static.Create(overlayManager, null, IMcBooleanConditionalSelector.EBooleanOp.EB_OR);
                    overlayManager.SetConditionalSelectorLock(booleanSelector, true);
                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            Funcs.addFragmentAndHidePrevious(getFragmentManager(), new BooleanConditionalSelectorFragment(), OverlayManagerTreeViewFragment.this, R.id.fragment_container, booleanSelector);
                        }
                    });
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "IMcBooleanConditionalSelector.Static.Create/SetConditionalSelectorLock");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void handleBlockedConditionalSelector(final IMcOverlayManager overlayManager) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    final IMcBlockedConditionalSelector blockedSelector = IMcBlockedConditionalSelector.Static.Create(overlayManager);
                    overlayManager.SetConditionalSelectorLock(blockedSelector, true);
                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            Funcs.addFragmentAndHidePrevious(getFragmentManager(), new BlockedConditionalSelectorFragment(), OverlayManagerTreeViewFragment.this, R.id.fragment_container, blockedSelector);
                        }
                    });
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "IMcBlockedConditionalSelector.Static.Create");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void addOverlayManagerNodesTree(IMcOverlayManager overlayManager, TreeNode parentVP) {
        IMcOverlay[] iMcOverlays = new IMcOverlay[0];
        IMcObjectScheme[] iMcSchemas = new IMcObjectScheme[0];
        try {
            iMcOverlays = overlayManager.GetOverlays();
            iMcSchemas = overlayManager.GetObjectSchemes();
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, ".GetOverlays");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
        if (iMcOverlays != null) {
            for (int i = 0; i < iMcOverlays.length; i++) {
                String name = Manager_MCNames.getInstance().getNameByObject(iMcOverlays[i]);
                IconTreeItemHolder.IconTreeItem iconTreeItem = new IconTreeItemHolder.IconTreeItem(
                        R.string.ic_satellite,
                        name,
                        IconTreeItemHolder.IconTreeItem.TreeViewType.OM,
                        OverlayTabsFragment.class.getCanonicalName(),
                        iMcOverlays[i],
                        R.menu.treeview_overlay_item,
                        getOverlayMenuClickListener(iMcOverlays[i]));

                TreeNode overlaysNode = new TreeNode(iconTreeItem);
                parentVP.addChild(overlaysNode);
                addObjectTree(iMcOverlays[i], overlaysNode);
            }
        }

        try {
            IMcConditionalSelector[] selectorArr = overlayManager.GetConditionalSelectors();
            if (selectorArr != null && selectorArr.length > 0) {
                for (IMcConditionalSelector selector : selectorArr) {
                    String name = Manager_MCNames.getInstance().getNameByObject(selector);
                    IconTreeItemHolder.IconTreeItem iconTreeItem = new IconTreeItemHolder.IconTreeItem(
                            R.string.ic_shuffle,
                            name,
                            IconTreeItemHolder.IconTreeItem.TreeViewType.OM,
                            BaseConditionalSelectorFragment.getSelectorFragNameFromSelectorClassName(selector.getClass().getSimpleName()),
                            selector,
                            R.menu.treeview_selector_item,
                            getConditionalSelectorMenuClickListener(selector));
                    TreeNode selectorNode = new TreeNode(iconTreeItem);
                    parentVP.addChild(selectorNode);
                }
            }
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, ".GetConditionalSelectors");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            IMcCollection[] collections = overlayManager.GetCollections();
            if (collections != null && collections.length > 0) {
                for (IMcCollection collection : collections) {
                    String name = Manager_MCNames.getInstance().getNameByObject(collection);
                    IconTreeItemHolder.IconTreeItem iconTreeItem = new IconTreeItemHolder.IconTreeItem(
                            R.string.ic_shuffle,
                            name,
                            IconTreeItemHolder.IconTreeItem.TreeViewType.OM,
                            CollectionTabsFragment.class.getCanonicalName(),
                            collection,
                            R.menu.treeview_collection_item,
                            getCollectionMenuClickListener(collection));
                    TreeNode collectionNode = new TreeNode(iconTreeItem);
                    parentVP.addChild(collectionNode);
                }
            }
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, ".GetCollections");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }

        if (iMcSchemas != null) {
            for (int i = 0; i < iMcSchemas.length; i++) {
                String name = Manager_MCNames.getInstance().getNameByObject(iMcSchemas[i]);
                IconTreeItemHolder.IconTreeItem iconTreeItem = new IconTreeItemHolder.IconTreeItem(
                        R.string.ic_shuffle,
                        name,
                        IconTreeItemHolder.IconTreeItem.TreeViewType.OM,
                        SchemesTabsFragment.class.getCanonicalName(),
                        iMcSchemas[i],
                        R.menu.treeview_scheme_item,
                        getSchemeMenuClickListener(iMcSchemas[i]));
                TreeNode schemeNode = new TreeNode(iconTreeItem);
                schemeNode = addLocationNodes(schemeNode, iMcSchemas[i]);
                parentVP.addChild(schemeNode);
            }
        }
        // standalone items
        HashMap<IMcObjectSchemeItem, Integer> itemsHashMap = Manager_MCObjectSchemeItem.getInstance().getAllParams();
        for (IMcObjectSchemeItem mcObjectSchemeItem : itemsHashMap.keySet()) {
            try {
                TreeNode treeNode = new TreeNode(
                        new IconTreeItemHolder.IconTreeItem(
                                R.string.ic_archive,
                                Manager_MCNames.getInstance().getNameByObject(mcObjectSchemeItem),
                                IconTreeItemHolder.IconTreeItem.TreeViewType.OM,
                                mcObjectSchemeItem,
                                R.menu.treeview_standalone_items_item,
                                getStandaloneItemMenuClickListener(mcObjectSchemeItem)));

                mRoot.addChild(treeNode);
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    private TreeNode addLocationNodes(TreeNode schemeNode, IMcObjectScheme iMcSchema) {
        try {
            IMcObjectSchemeNode[] objectLocations = iMcSchema.GetNodes(new CMcEnumBitField<>(IMcObjectSchemeNode.ENodeKindFlags.ENKF_OBJECT_LOCATION));
            for (IMcObjectSchemeNode currLocation : objectLocations) {
                IconTreeItemHolder.IconTreeItem locationIconTreeItem = new IconTreeItemHolder.IconTreeItem(
                        R.string.ic_shuffle,
                        Manager_MCNames.getInstance().getNameByObject(currLocation),
                        IconTreeItemHolder.IconTreeItem.TreeViewType.OM,
                        currLocation,
                        R.menu.treeview_object_location_item,
                        getObjectLocationMenuClickListener(currLocation));
                TreeNode locationNode = new TreeNode(locationIconTreeItem);
                if(testConnectItems.size()==0)
                    testConnectItems.add(currLocation);
                locationNode = addChildrenToLocationNode(locationNode, currLocation);
                schemeNode.addChild(locationNode);
            }
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "iMcSchemas[i].GetNodes");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return schemeNode;
    }

    private TreeNode addChildrenToLocationNode(TreeNode locationNode, IMcObjectSchemeNode currLocation) {
        try {
            IMcObjectSchemeNode[] objectItems = currLocation.GetChildren();
            if (objectItems == null)
                return locationNode;
            else {
                for (IMcObjectSchemeNode item : objectItems) {
                    if((!testConnectItems.contains(item)) && testConnectItems.size() == 1)
                        testConnectItems.add(item);
                    IconTreeItemHolder.IconTreeItem itemIconTreeItem =
                            new IconTreeItemHolder.IconTreeItem(
                                    R.string.ic_shuffle,
                                    Manager_MCNames.getInstance().getNameByObject(item),
                                    IconTreeItemHolder.IconTreeItem.TreeViewType.OM,
                                    item,
                                    R.menu.treeview_objectschemeitem_item,
                                    getObjectSchemeItemMenuClickListener((IMcObjectSchemeItem) item));
                    NestedItemTreeNode itemNode = new NestedItemTreeNode(itemIconTreeItem,item);
                    locationNode.addChild(itemNode);
                }
                return locationNode;
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return null;
    }

    @Override
    public void OnOverlayManagerCreated() {
        createOverlayManagerOverlaysTree();
    }

    @Override
    public void CloneObjectCallback() {
        createOverlayManagerOverlaysTree();
    }

    @Override
    public void SelectOverlayCallback(final IMcOverlay overlay,final IMcObject object) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    object.SetOverlay(overlay);
                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            createOverlayManagerOverlaysTree();
                        }
                    });

                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, ".GetObjects");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    @Override
    public void SelectSchemeCallback(final IMcObjectScheme scheme, final boolean bKeepRelevantProperties, final IMcObject object) {
        try {
            final IMcObjectScheme changedScheme = object.GetScheme();
            Funcs.runMapCoreFunc(new Runnable() {
                @Override
                public void run() {
                    try {
                        if (changedScheme.GetObjects().length < 2)
                            changedScheme.Release();
                        object.SetScheme(scheme, bKeepRelevantProperties);
                        getActivity().runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                createOverlayManagerOverlaysTree();
                            }
                        });

                    } catch (MapCoreException e) {
                        AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, ".GetObjects");
                        e.printStackTrace();
                    } catch (Exception ex) {ex.printStackTrace();
                    }
                }
            });
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, ".GetObjects");
            e.printStackTrace();
        } catch (Exception ex) {ex.printStackTrace();
        }
    }

    private class NestedItemTreeNode extends TreeNode {
        private boolean isNestedItemCreated = false;

        public NestedItemTreeNode(Object value, final IMcObjectSchemeNode iMcObjectSchemeNode) {
            super(value);
            setClickListener(new TreeNodeClickListener() {
                @Override
                public void onClick(TreeNode node, Object value) {
                    try {
                        IMcObjectSchemeNode[] iMcObjectSchemeNodes = iMcObjectSchemeNode.GetChildren();
                        if (iMcObjectSchemeNodes != null && !isNestedItemCreated) {
                            for (IMcObjectSchemeNode iMcObjectSchemeNode : iMcObjectSchemeNodes) {
                                IconTreeItemHolder.IconTreeItem itemIconTreeItem = new IconTreeItemHolder.IconTreeItem(
                                        R.string.ic_shuffle,
                                        Manager_MCNames.getInstance().getNameByObject(iMcObjectSchemeNode),
                                        IconTreeItemHolder.IconTreeItem.TreeViewType.OM);
                                NestedItemTreeNode nestedItemTreeNode = new NestedItemTreeNode(itemIconTreeItem, iMcObjectSchemeNode);
                                node.addChild(nestedItemTreeNode);
                                isNestedItemCreated = true;
                            }
                        }
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            });
        }
    }

    private void addObjectTree(IMcOverlay iMcOverlay, TreeNode overlaysNode) {

        IMcObject[] iMcObjects = new IMcObject[0];
        try {
            iMcObjects = iMcOverlay.GetObjects();
            //iMcObjects[0].GetProperties();
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, ".GetObjects");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
        if (iMcObjects != null) {
            for (int i = 0; i < iMcObjects.length; i++) {
                IconTreeItemHolder.IconTreeItem iconTreeItem = new IconTreeItemHolder.IconTreeItem(
                        R.string.ic_list,
                        Manager_MCNames.getInstance().getNameByObject(iMcObjects[i]),
                        IconTreeItemHolder.IconTreeItem.TreeViewType.OM,
                        ObjectTabsFragment.class.getCanonicalName(),
                        iMcObjects[i],
                        R.menu.treeview_object_item,
                        getObjectMenuClickListener(iMcObjects[i]));
                TreeNode objNode = new TreeNode(iconTreeItem);

                overlaysNode.addChild(objNode);
            }
        }
    }

    private PopupMenu.OnMenuItemClickListener getObjectMenuClickListener(final IMcObject iMcObject) {
        PopupMenu.OnMenuItemClickListener menuItemClickListener = new PopupMenu.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(MenuItem item) {
                int id =item.getItemId();
                if(id ==  R.id.tv_object_item_action_clone) {
                    handleCloneObject(iMcObject);
                }
                else if(id == R.id.tv_object_item_action_remove) {
                    handleRemoveObject(iMcObject);
                }
                else if(id == R.id.tv_object_item_action_replace_overlay) {
                    handleReplaceOverlay(iMcObject);
                }
                else if(id == R.id.tv_object_item_action_replace_scheme) {
                    handleReplaceScheme(iMcObject);
                }
                else if(id == R.id.tv_object_item_action_jump_to_scheme) {
                    handleJumpToScheme(iMcObject);
                }
                else if(id == R.id.tv_object_item_action_rename) {
                    handleRename(iMcObject);
                }
                else if(id == R.id.tv_object_item_action_delete_name) {
                    handleDeleteName(iMcObject);
                }
                else if(id == R.id.tv_object_item_action_move_to_location) {
                        handleMoveToLocation(iMcObject);
                }
                return true;
            }
        };
        return menuItemClickListener;

    }

    private void handleCloneObject(IMcObject mcObject)
    {
        String className = CloneObjectFragment.class.getName();
        CloneObjectFragment dialog = (CloneObjectFragment) DialogFragment.instantiate(getActivity(), className);
        dialog.setObject(mcObject);
        dialog.setTargetFragment(OverlayManagerTreeViewFragment.this, 1);
        dialog.show(getFragmentManager(), className);
    }

    private void handleRemoveObject(final IMcObject iMcObject) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                Funcs.removeObject(iMcObject, getActivity());
                getActivity().runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        createOverlayManagerOverlaysTree();
                    }
                });
            }
        });
    }

    private void handleReplaceOverlay(IMcObject mcObject)
    {
        String className = OverlaysListFragment.class.getName();
        OverlaysListFragment dialog = (OverlaysListFragment) DialogFragment.instantiate(getActivity(), className);
        dialog.setObject(mcObject);
        dialog.setTargetFragment(OverlayManagerTreeViewFragment.this, 1);
        dialog.show(getFragmentManager(), className);
    }

    private void handleReplaceScheme(IMcObject mcObject)
    {
        String className = SchemesListFragment.class.getName();
        SchemesListFragment dialog = (SchemesListFragment) DialogFragment.instantiate(getActivity(), className);
        dialog.setObject(mcObject);
        dialog.setTargetFragment(OverlayManagerTreeViewFragment.this, 1);
        dialog.show(getFragmentManager(), className);
    }

    private PopupMenu.OnMenuItemClickListener getSchemeMenuClickListener(final IMcObjectScheme iMcObjectScheme) {
        PopupMenu.OnMenuItemClickListener menuItemClickListener = new PopupMenu.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(MenuItem menuItem) {
                int id = menuItem.getItemId();
                if(id == R.id.tv_scheme_item_action_jump_to_object) {
                    handleJumpToObject(menuItem, iMcObjectScheme);
                }
                else if(id == R.id.tv_scheme_item_action_clone) {
                    handleClone(menuItem, iMcObjectScheme);
                }
                else if(id == R.id.tv_scheme_item_action_add_object_location) {
                        handleAddObjectLocation(menuItem, iMcObjectScheme);
                }
                return true;
            }
        };
        return menuItemClickListener;
    }

    private void handleJumpToObject(MenuItem menuItem, IMcObjectScheme iMcObjectScheme) {
        buildObjectListOfScheme(menuItem, iMcObjectScheme);
    }

    private void handleClone(MenuItem menuItem, final IMcObjectScheme iMcObjectScheme) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                        iMcObjectScheme.Clone();
                        getActivity().runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                createOverlayManagerOverlaysTree();
                            }
                        });
                    }catch (MapCoreException e) {
                        AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "Clone()");
                        e.printStackTrace();
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
            }
        });
    }

    private void handleAddObjectLocation(MenuItem menuItem, final IMcObjectScheme iMcObjectScheme) {
        FragmentTransaction transaction = getFragmentManager().beginTransaction();
        transaction.hide(this);
        NewObjectLocationFragment newObjectLocationFragment = new NewObjectLocationFragment();
        newObjectLocationFragment.SetSelectedScheme(iMcObjectScheme);
        transaction.add(R.id.fragment_container,newObjectLocationFragment , NewObjectLocationFragment.class.getSimpleName());
        transaction.addToBackStack(NewObjectLocationFragment.class.getSimpleName());
        transaction.commit();
    }

    private PopupMenu.OnMenuItemClickListener getObjectSchemeItemMenuClickListener(final IMcObjectSchemeItem iMcObjectSchemeItem) {
        PopupMenu.OnMenuItemClickListener menuItemClickListener = new PopupMenu.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(MenuItem menuItem) {
                int id = menuItem.getItemId();
                if(id == R.id.tv_objectschemeitem_item_action_connect) {
                    handleConnectItem(iMcObjectSchemeItem);
                }
                else if(id == R.id.tv_objectschemeitem_item_action_clone) {
                    handleCloneItem(iMcObjectSchemeItem);
                }
                else if(id == R.id.tv_objectschemeitem_item_action_disconnect) {
                    handleDisconnectItem(iMcObjectSchemeItem, false);
                }
                else if(id == R.id.tv_objectschemeitem_item_action_disconnectandkeep) {
                    handleDisconnectAndKeepItem(iMcObjectSchemeItem);
                }
                else if(id == R.id.tv_objectschemeitem_item_action_calculate_nodes) {
                        handleCalcluateNodes(iMcObjectSchemeItem);

                }
                return true;
            }
        };
        return menuItemClickListener;
    }

    private PopupMenu.OnMenuItemClickListener getStandaloneItemMenuClickListener(final IMcObjectSchemeItem iMcObjectSchemeItem) {
        PopupMenu.OnMenuItemClickListener menuItemClickListener = new PopupMenu.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(MenuItem menuItem) {
                int id = menuItem.getItemId();
                if(id ==  R.id.tv_standaloneitem_item_action_connect) {
                    handleConnectStandaloneItem(iMcObjectSchemeItem);
                }
                else if(id == R.id.tv_standaloneitem_item_action_clone) {
                    handleCloneStandaloneItem(iMcObjectSchemeItem);
                }
                else if(id == R.id.tv_standaloneitem_item_action_disconnect) {
                        handleDisconnectItem(iMcObjectSchemeItem, true);
                }
                    else if(id == R.id.tv_standaloneitem_item_action_disconnectandkeep) {
                        handleDisconnectAndKeepItem(iMcObjectSchemeItem);
                }
                return true;
            }
        };
        return menuItemClickListener;
    }

    private PopupMenu.OnMenuItemClickListener getObjectLocationMenuClickListener(final IMcObjectSchemeNode iMcObjectSchemeNode) {
        PopupMenu.OnMenuItemClickListener menuItemClickListener = new PopupMenu.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(MenuItem menuItem) {
                int id = menuItem.getItemId();
                if(id ==  R.id.tv_object_location_node_action_remove) {
                        handleRemoveObjectLocation(iMcObjectSchemeNode);
                }
                return true;
            }
        };
        return menuItemClickListener;
    }

    private void handleRemoveObjectLocation(final IMcObjectSchemeNode iMcObjectSchemeNode) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    IMcObjectScheme scheme = iMcObjectSchemeNode.GetScheme();
                    scheme.RemoveObjectLocation(((IMcObjectLocation)iMcObjectSchemeNode).GetIndex());
                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            createOverlayManagerOverlaysTree();
                        }
                    });
                }catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, ".GetObjects");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void handleConnectStandaloneItem(final IMcObjectSchemeItem iMcObjectSchemeItem) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                if (testConnectItems.size() > 0) {
                    try {
                        testConnectItems.remove(iMcObjectSchemeItem);
                        IMcObjectSchemeNode[] items = testConnectItems.toArray(new IMcObjectSchemeNode[testConnectItems.size()]);

                        ((IMcSymbolicItem) iMcObjectSchemeItem).Connect(items);
                        Manager_MCObjectSchemeItem.getInstance().RemoveItem(iMcObjectSchemeItem);
                        getActivity().runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                createOverlayManagerOverlaysTree();
                            }
                        });
                    }catch (MapCoreException e) {
                        AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, ".GetObjects");
                        e.printStackTrace();
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            }
        });

    }

    private void handleConnectItem(IMcObjectSchemeItem iMcObjectSchemeItem) {

        if (Manager_MCObjectSchemeItem.getInstance().getAllParams() != null && Manager_MCObjectSchemeItem.getInstance().getAllParams().size() > 0) {
            Object[] items = Manager_MCObjectSchemeItem.getInstance().getAllParams().keySet().toArray();
            try {

                ((IMcSymbolicItem) iMcObjectSchemeItem).Connect((IMcObjectSchemeItem) items[0]);
                Manager_MCObjectSchemeItem.getInstance().RemoveItem((IMcObjectSchemeItem) items[0]);
            } catch (MapCoreException e) {
                AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "Connect");
                e.printStackTrace();
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    private void handleCloneStandaloneItem(IMcObjectSchemeItem iMcObjectSchemeItem) {
        CloneItem(iMcObjectSchemeItem,null);
    }

    private void handleCloneItem(final IMcObjectSchemeItem iMcObjectSchemeItem) {

        AlertMessages.ShowYesNoMessage(
                getActivity(),
                "Clone item",
                "Clone object properties as well?",
                   /*Yes click*/ new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {

                        String className = GetObjectListBaseOnObjectSchemeItemFragment.class.getName();
                        GetObjectListBaseOnObjectSchemeItemFragment dialog = (GetObjectListBaseOnObjectSchemeItemFragment) DialogFragment.instantiate(getActivity(), className);
                        dialog.setObject(iMcObjectSchemeItem);

                        dialog.setTargetFragment(OverlayManagerTreeViewFragment.this, 1);
                        dialog.show(getFragmentManager(), className);
                    }
                },
                   /*No click*/new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {
                        CloneItem(iMcObjectSchemeItem, null);
                    }
                });
    }

    @Override
    public void callbackObjectListBaseOnObjectSchemeItem(IMcObject obj,IMcObjectSchemeItem iMcObjectSchemeItem) {
        CloneItem(iMcObjectSchemeItem,obj);
    }

    private void CloneItem(final IMcObjectSchemeItem iMcObjectSchemeItem, final IMcObject obj) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    IMcObjectSchemeItem cloneItem = iMcObjectSchemeItem.Clone(obj);
                    Manager_MCObjectSchemeItem.getInstance().AddNewItem(cloneItem);
                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            createOverlayManagerOverlaysTree();
                        }
                    });

                }catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, ".GetObjects");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void handleDisconnectAndKeepItem(final IMcObjectSchemeItem iMcObjectSchemeItem) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    m_Disconnecteodes.add(iMcObjectSchemeItem);
                    GetAllNodeChildren(iMcObjectSchemeItem);

                    for (IMcObjectSchemeNode item : m_Disconnecteodes) {
                        Manager_MCObjectSchemeItem.getInstance().AddNewItem((IMcObjectSchemeItem) item);
                    }

                    iMcObjectSchemeItem.Disconnect();
                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            createOverlayManagerOverlaysTree();
                        }
                    });
                }
                catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "Disconnect");
                    e.printStackTrace();
                }
                catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });

    }


    private void handleCalcluateNodes(final IMcObjectSchemeItem iMcObjectSchemeItem) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    IMcObjectScheme currentScheme = iMcObjectSchemeItem.GetScheme();

                    IMcObject[] objs = currentScheme.GetObjects();
                    IMcObject obj = null;
                    if (objs != null && objs.length > 0)
                    {
                        obj = objs[0];
                        if(obj != null)
                        {
                            ObjectRef<SMcVector3D[]> nodeCalcPointsArr = new ObjectRef<>();
                            ObjectRef<EMcPointCoordSystem> pointCoordSystem = new ObjectRef<>();
                            ObjectRef<int[]> indices = new ObjectRef<>();;
                            if (iMcObjectSchemeItem instanceof IMcSymbolicItem) {
                                ((IMcSymbolicItem) iMcObjectSchemeItem).GetAllCalculatedPoints(Manager_AMCTMapForm.getInstance().getCurViewport()
                                        ,obj,
                                        nodeCalcPointsArr,
                                        pointCoordSystem,
                                        indices);
                            }
                        }
                    }
                }
                catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "GetAllCalculatedPoints");
                    e.printStackTrace();
                }
                catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });

    }

    private void handleDisconnectItem(final IMcObjectSchemeItem iMcObjectSchemeItem,final boolean isStandaloneItem) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    iMcObjectSchemeItem.Disconnect();
                    if (isStandaloneItem) {
                        Manager_MCObjectSchemeItem.getInstance().RemoveItem(iMcObjectSchemeItem);
                    }
                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            createOverlayManagerOverlaysTree();
                        }
                    });

                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "Disconnect");
                    e.printStackTrace();
                }catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });

    }

    private List<IMcObjectSchemeNode> m_Disconnecteodes = new ArrayList<>();

    private void GetAllNodeChildren(IMcObjectSchemeItem currNode) {
        try {
            IMcObjectSchemeNode[] schemeNodeArr = currNode.GetChildren();
            for(IMcObjectSchemeNode node : schemeNodeArr)
                m_Disconnecteodes.add(node);

            while (schemeNodeArr.length > 0 && schemeNodeArr != null) {
                for (IMcObjectSchemeNode node : schemeNodeArr) {
                    GetAllNodeChildren((IMcObjectSchemeItem) node);
                }
                break;
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void buildObjectListOfScheme(MenuItem menuItem, IMcObjectScheme iMcObjectScheme) {
        SubMenu subMenu = menuItem.getSubMenu();
        subMenu.clear();
        try {
            IMcObject[] schemeObjects = iMcObjectScheme.GetObjects();
            for (final IMcObject object : schemeObjects) {
                subMenu.add(Manager_MCNames.getInstance().getNameByObject(object)).setOnMenuItemClickListener(new MenuItem.OnMenuItemClickListener() {
                    @Override
                    public boolean onMenuItemClick(MenuItem item) {
                        jumpToObj(object.hashCode());
                        return false;
                    }
                });
            }
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, ".GetObjects");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void jumpToObj(int key) {
        if (mRoot.treeNodeToSelect != null) {
            mRoot.treeNodeToSelect.setSelected(false);
            mRoot.treeNodeToSelect = null;
        }
        TreeNode objectNode = mRoot.getNodeByObjectHashCode(mRoot, key);
        if (objectNode != null) {
            if (objectNode.getParent() != null && !objectNode.getParent().isExpanded())
                mOverlayManagerTreeView.expandNode(objectNode.getParent());
            objectNode.setSelected(true);
        }
    }

    private void handleMoveToLocation(IMcObject iMcObject) {
        McObjectFunc.MoveToLocation(getActivity(),iMcObject);
        getActivity().onBackPressed();
    }

    private void handleJumpToScheme(IMcObject iMcObject) {
        try {
            int key = iMcObject.GetScheme().hashCode();
            if (mRoot.treeNodeToSelect != null) {
                mRoot.treeNodeToSelect.setSelected(false);
                mRoot.treeNodeToSelect = null;
            }
            TreeNode schemeNode = mRoot.getNodeByObjectHashCode(mRoot, key);
            if (schemeNode != null)
                schemeNode.setSelected(true);
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, ".getNodeByObjectHashCode");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void handleCreateNewOverlayManager()
    {
        String className = NewOverlayManagerFragment.class.getName();
        NewOverlayManagerFragment dialog = (NewOverlayManagerFragment) DialogFragment.instantiate(getActivity(), className);
        dialog.setTargetFragment(OverlayManagerTreeViewFragment.this, 1);
        dialog.show(getFragmentManager(), className);
    }

    @Override
    public void onHiddenChanged(boolean hidden) {
        super.onHiddenChanged(hidden);
        if (!hidden) {
            setTitle();
            createOverlayManagerOverlaysTree();
        }
    }

    @Override
    public void onCreateOptionsMenu(Menu menu, MenuInflater inflater) {
        menu.clear();
       // if(!((MapsContainerActivity)getActivity()).mIsMainMenu) {
            getActivity().getMenuInflater().inflate(R.menu.menu_overlay_manager_tv, menu);
        ;
        //}
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        int id = item.getItemId();
        if(id == R.id.nav_om_tv_new_overlay_manager)
        {
            handleCreateNewOverlayManager();
        }
        else if(id == R.id.nav_new_object_based_on_existing_scheme)
        {
            openLocationPointsFragment(LocationPointsFragment.NewObjectOperation.BasedOnExistingScheme);
            return true;
        }
        else if(id == R.id.nav_new_object_with_new_scheme_containing_one_location)
        {
            openLocationPointsFragment(LocationPointsFragment.NewObjectOperation.WithNewSchemeContainingOneLocation);
            return true;
        }
        else if(id == R.id.nav_new_object_with_new_scheme_containing_one_location_and_one_item)
        {
            openLocationPointsFragment(LocationPointsFragment.NewObjectOperation.WithNewSchemeContainingOneLocationAndOneItem);
            return true;
        }
        return super.onOptionsItemSelected(item);
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;

    }

    @Override
    public void setUserVisibleHint(boolean visible) {
        super.setUserVisibleHint(visible);
        if (visible && isResumed()) {
            //Only manually call onResume if fragment is already visible
            //Otherwise allow natural fragment lifecycle to call onResume
            onResume();
        }
    }

    @Override
    public void onResume() {
        super.onResume();
        if (!getUserVisibleHint()) {
            return;
        }
        createOverlayManagerOverlaysTree();
    }

    @Override
    public void onNameChanged() {
        createOverlayManagerOverlaysTree();
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
