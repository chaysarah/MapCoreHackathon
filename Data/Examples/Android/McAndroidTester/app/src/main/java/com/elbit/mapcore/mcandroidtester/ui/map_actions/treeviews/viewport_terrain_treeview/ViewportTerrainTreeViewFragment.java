package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.viewport_terrain_treeview;

import android.content.Context;
import android.content.DialogInterface;
import android.net.Uri;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.os.Message;
import androidx.annotation.Nullable;
import androidx.fragment.app.DialogFragment;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentTransaction;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.LinearLayout;
import android.widget.PopupMenu;

import com.elbit.mapcore.Structs.SMcBColor;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.interfaces.ITreeView;
import com.elbit.mapcore.mcandroidtester.managers.Manager_MCNames;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_AMCTMapForm;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_MCLayers;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_MCMapGrid;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_MCMapHeightLines;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_MCTerrain;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.TerrainsFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.terrain_tabs.TerrainDetailsTabsFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.viewport_tabs.ViewPortDetailsTabsFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.viewport_tabs.viewport_as_camera_tabs.ViewPortAsCameraTabsFragment;
import com.elbit.mapcore.mcandroidtester.ui.objects.ObjectPropertiesBase;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.BaseApplication;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.ChangingNamesDialogFragment;
import com.elbit.mapcore.mcandroidtester.utils.treeview.holder.IconTreeItemHolder;
import com.elbit.mapcore.mcandroidtester.utils.treeview.model.TreeNode;
import com.elbit.mapcore.mcandroidtester.utils.treeview.view.AndroidTreeView;

import java.util.ArrayList;
import java.util.Arrays;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Map.IMcMapCamera;
import com.elbit.mapcore.Interfaces.Map.IMcMapGrid;
import com.elbit.mapcore.Interfaces.Map.IMcMapGrid.SGridRegion;
import com.elbit.mapcore.Interfaces.Map.IMcMapHeightLines;
import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcMapTerrain;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Structs.SMcVector3D;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link ViewportTerrainTreeViewFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link ViewportTerrainTreeViewFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class ViewportTerrainTreeViewFragment extends Fragment implements ITreeView, ISelectGridCallback, ISelectHeightLinesCallback , ChangingNamesDialogFragment.OnNameChangedListener {

    private OnFragmentInteractionListener mListener;
    IMcMapViewport mCurViewport;
    private TreeNode mRoot;
    private View mRootView;
    private Handler mHandler;
    private static final int CREATE_TREE = 1;
    public static int SelectedNodeHashCode = 0;
    private AndroidTreeView mViewportTerrainsTreeView;

    public ViewportTerrainTreeViewFragment() {
        // Required empty public constructor
    }

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment MapWorldTreeViewFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static ViewportTerrainTreeViewFragment newInstance() {
        ViewportTerrainTreeViewFragment fragment = new ViewportTerrainTreeViewFragment();
        return fragment;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setHasOptionsMenu(true);
        initHandler();
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        setTitle();
        mRootView = inflater.inflate(R.layout.fragment_map_world_tree_view, container, false);
        if (savedInstanceState != null)
            SelectedNodeHashCode = savedInstanceState.getInt(AndroidTreeView.SELECTED_TREE_VIEW_NODE);
        else
            SelectedNodeHashCode = 0;
        createViewportTerrainTree();

        return mRootView;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putInt(AndroidTreeView.SELECTED_TREE_VIEW_NODE, SelectedNodeHashCode);
    }

    private void initHandler() {
        mHandler = new Handler(Looper.getMainLooper()) {
            @Override
            public void handleMessage(Message msg) {
                switch (msg.what) {
                    case CREATE_TREE:
                        createViewportTerrainTree();
                        break;
                }
            }
        };
    }

    private void setTitle() {
        getActivity().setTitle("viewport terrain tree view");
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
        } else {
            throw new RuntimeException(context.toString()
                    + " must implement OnFragmentInteractionListener");
        }
        if (context instanceof MapsContainerActivity) {
            this.mCurViewport = Manager_AMCTMapForm.getInstance().getCurViewport();
            ((MapsContainerActivity) context).mCurFragmentTag = ViewportTerrainTreeViewFragment.class.getSimpleName();
        }
    }

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
    }

    private TreeNode.TreeNodeLongClickListener nodeLongClickListener = new TreeNode.TreeNodeLongClickListener() {
        @Override
        public boolean onLongClick(TreeNode node, Object value) {
            IconTreeItemHolder.IconTreeItem item = (IconTreeItemHolder.IconTreeItem) value;
            startRelatedFragment(item);
            //Toast.makeText(getActivity(), "Long click: " + item.mText, Toast.LENGTH_SHORT).show();
            return true;
        }
    };

    private void startRelatedFragment(IconTreeItemHolder.IconTreeItem item) {
        if (!item.mFragmentName.isEmpty()) {
            FragmentWithObject fragment = (FragmentWithObject) Fragment.instantiate(getActivity(), item.mFragmentName);
            fragment.setObject(item.mImcObj);
            FragmentTransaction transaction = getFragmentManager().beginTransaction();
            transaction.hide(this);
            String fragSimpleName = item.mFragmentName.substring(item.mFragmentName.lastIndexOf(".") + 1);
            transaction.add(R.id.fragment_container, (Fragment) fragment, fragSimpleName);
            transaction.addToBackStack(fragSimpleName);
            transaction.commit();
        }
    }

    private void createViewportTerrainTree() {
        if (mRoot != null) {
            mRoot.getViewHolder().getTreeView().removeAllNodes();
        }
        mRoot = TreeNode.root();
        addViewportsToTree();
        addGridsToTree();
        addHeightLinesToTree();
        mViewportTerrainsTreeView = new AndroidTreeView(getActivity(), mRoot);
        mViewportTerrainsTreeView.setDefaultAnimation(false);
        mViewportTerrainsTreeView.setDefaultContainerStyle(R.style.TreeNodeStyleCustom);
        mViewportTerrainsTreeView.setDefaultViewHolder(IconTreeItemHolder.class);
        mViewportTerrainsTreeView.setDefaultNodeLongClickListener(nodeLongClickListener);
        ((LinearLayout) mRootView).addView(mViewportTerrainsTreeView.getView());

        mViewportTerrainsTreeView.expandNodeTillRoot(SelectedNodeHashCode);
    }

    private void addViewportsToTree() {
        ArrayList<IMcMapViewport> viewPorts = Manager_AMCTMapForm.getInstance().getAllImcViewports();

        for (IMcMapViewport viewPort : viewPorts) {
            TreeNode parentVP = new TreeNode(
                    new IconTreeItemHolder.IconTreeItem(
                            R.string.ic_map,
                            Manager_MCNames.getInstance().getNameByObject(viewPort),
                            IconTreeItemHolder.IconTreeItem.TreeViewType.VT,
                            ViewPortDetailsTabsFragment.class.getCanonicalName(),
                            viewPort,
                            R.menu.treeview_viewport_item,
                            getViewPortMenuClickListener(viewPort)));
            addTerrainsToTree(viewPort, parentVP);
            addCamerasToTree(viewPort, parentVP);
            addNodesOfVpToTree(viewPort, parentVP);
            mRoot.addChild(parentVP);
        }

    }

    private void addNodesOfVpToTree(IMcMapViewport viewPort, TreeNode parentVP) {
        try {
            IMcMapGrid grid = viewPort.GetGrid();
            if (grid != null) {
                TreeNode gridNode = new TreeNode(
                        new IconTreeItemHolder.IconTreeItem(
                                R.string.ic_camera,
                                Manager_MCNames.getInstance().getNameByObject(grid),
                                IconTreeItemHolder.IconTreeItem.TreeViewType.VT));
                parentVP.addChild(gridNode);
            }
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "GetGrid");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            IMcMapHeightLines heightLines = viewPort.GetHeightLines();
            if (heightLines != null) {
                TreeNode HeightLinesNode = new TreeNode(
                        new IconTreeItemHolder.IconTreeItem(
                                R.string.ic_camera,
                                Manager_MCNames.getInstance().getNameByObject(heightLines),
                                IconTreeItemHolder.IconTreeItem.TreeViewType.VT ));
                parentVP.addChild(HeightLinesNode);
            }
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "GetHeightLines");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void addCamerasToTree(IMcMapViewport viewport, TreeNode parentVP) {
        try {
            for (IMcMapCamera camera : viewport.GetCameras()) {
                IconTreeItemHolder.IconTreeItem cameraIconTree=
                        new IconTreeItemHolder.IconTreeItem(
                                R.string.ic_camera,
                                Manager_MCNames.getInstance().getNameByObject(camera),
                                IconTreeItemHolder.IconTreeItem.TreeViewType.VT,
                                ViewPortAsCameraTabsFragment.class.getCanonicalName(),
                                camera,
                                R.menu.treeview_camera_item,
                                getCameraMenuClickListener(camera,viewport));
                TreeNode cameraNode = new TreeNode(cameraIconTree);
                parentVP.addChild(cameraNode);

            }
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "GetCameras");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void addTerrainsToTree(IMcMapViewport iMcMapViewport, TreeNode parentVP) {
        IMcMapTerrain[] terrains = new IMcMapTerrain[0];
        try {
            terrains = iMcMapViewport.GetTerrains();
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, ".GetTerrains");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
        if (terrains != null) {
            for (int i = 0; i < terrains.length; i++) {
                IconTreeItemHolder.IconTreeItem iconTreeItem = new IconTreeItemHolder.IconTreeItem(
                        R.string.ic_terrain,
                        Manager_MCNames.getInstance().getNameByObject(terrains[i]),
                        IconTreeItemHolder.IconTreeItem.TreeViewType.VT,
                        TerrainDetailsTabsFragment.class.getCanonicalName(),
                        terrains[i],
                        R.menu.treeview_terrain_item);

                TreeNode terrainNode = new TreeNode(iconTreeItem);
                iconTreeItem.setOnMenuItemClickListener(getTerrainMenuClickListener(terrains[i], iMcMapViewport, terrainNode));
                parentVP.addChild(terrainNode);
                try {
                    addTerrainLayersToTree(terrains[i].GetLayers(), terrainNode, terrains[i]);
                } catch (MapCoreException e) {
                    e.printStackTrace();
                    AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "terrains[i].GetLayers()");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        }
    }

    private void addTerrainLayersToTree(IMcMapLayer[] layers, TreeNode terrainNode, IMcMapTerrain mcMapTerrain) {
         for (int j = 0; j < layers.length; j++) {
            TreeNode layerNode = null;
            try {
                //String layerName = layers[j].GetLayerType().name();
                String layerTitle = layers[j].hashCode() + " " + layers[j].GetLayerType().name() + " Map Layer";
                //String relatedFragmentName = MapLayerFragment.class.getPackage().getName() + "." + getLayerFragNameFromLayerName(layerName);
                String relatedFragmentName =  Manager_MCLayers.getInstance().GetLayerFragmentByType( layers[j], this.getContext());

                layerNode = new TreeNode(new IconTreeItemHolder.IconTreeItem(
                        R.string.ic_label,
                        layerTitle,
                        IconTreeItemHolder.IconTreeItem.TreeViewType.VT,
                        relatedFragmentName,
                        layers[j],
                        R.menu.treeview_layer_item,
                        getLayerMenuClickListener(mcMapTerrain, layers[j])));
            } catch (MapCoreException e) {
                e.printStackTrace();
                AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "GetLayerType()");
            } catch (Exception e) {
                e.printStackTrace();
            }
            terrainNode.addChild(layerNode);
        }
    }

    private void addGridsToTree() {
        for (Object gridObj : Arrays.asList(Manager_MCMapGrid.getInstance().getAllParams().keySet().toArray())) {
            IMcMapGrid grid = (IMcMapGrid) gridObj;
            TreeNode gridNode = new TreeNode(
                    new IconTreeItemHolder.IconTreeItem(
                            R.string.ic_camera,
                            grid.hashCode() + " Map Grid",
                            IconTreeItemHolder.IconTreeItem.TreeViewType.VT ));

            mRoot.addChild(gridNode);
        }
    }

    private void addHeightLinesToTree() {
        for (Object heightLinesObj : Arrays.asList(Manager_MCMapHeightLines.getInstance().getAllParams().keySet().toArray())) {
            IMcMapHeightLines heightLines = (IMcMapHeightLines) heightLinesObj;
            TreeNode heightLinesNode = new TreeNode(
                    new IconTreeItemHolder.IconTreeItem(
                            R.string.ic_camera,
                            heightLines.hashCode() + " Map Height Lines",
                            IconTreeItemHolder.IconTreeItem.TreeViewType.VT));
            mRoot.addChild(heightLinesNode);
        }
    }

    private PopupMenu.OnMenuItemClickListener getViewPortMenuClickListener(final IMcMapViewport viewport) {
        PopupMenu.OnMenuItemClickListener menuItemClickListener = new PopupMenu.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(MenuItem item) {
                int id = item.getItemId();
                if (id == R.id.tv_vp_add_terrain) {
                    openAddTerrainFrag(viewport);
                }
                else if (id == R.id.tv_vp_create_camera) {
                    handleCreateCamera(viewport);
                }
                else if (id == R.id.tv_vp_set_grid) {
                    handleSetGrid(viewport);
                }
                else if (id == R.id.tv_vp_remove_grid) {
                    handleRemoveGrid(viewport);
                }
                else if (id == R.id.tv_vp_set_height_lines) {
                    handleSetHeightLines(viewport);
                }
                else if (id == R.id.tv_vp_remove_height_lines) {
                        handleRemoveHeightLines(viewport);
                }
                else if (id == R.id.tv_vp_rename) {
                    handleRename(viewport);
                }
                else if (id == R.id.tv_vp_delete_name) {
                    handleDeleteName(viewport);
                }
                return true;
            }
        };
        return menuItemClickListener;
    }

    private void handleCreateCamera(final IMcMapViewport mapViewport) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {

                try {
                    IMcMapCamera newCamera = mapViewport.CreateCamera();

                    IMcMapTerrain[] terrainInViewport = mapViewport.GetTerrains();
                    SMcVector3D minVertex = terrainInViewport[0].GetBoundingBox().MinVertex;
                    SMcVector3D maxVertex = terrainInViewport[0].GetBoundingBox().MaxVertex;

                    SMcVector3D newCameraPosition = new SMcVector3D();
                    newCameraPosition.x = (maxVertex.x + minVertex.x) / 2;
                    newCameraPosition.y = (maxVertex.y + minVertex.y) / 2;
                    newCameraPosition.z = (maxVertex.z + minVertex.z) / 2;

                    newCamera.SetCameraPosition(newCameraPosition, false);

                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            createViewportTerrainTree();
                        }
                    });
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "handleCreateCamera");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });


    }

    private void handleCreateGrid() {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    IMcMapGrid newGrid = IMcMapGrid.Static.Create(new SGridRegion[0], new IMcMapGrid.SScaleStep[0], ObjectPropertiesBase.Grid_IsUsingBasicItemPropertiesOnly);
                    Manager_MCMapGrid.getInstance().AddNewMapGrid(newGrid);
                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            createViewportTerrainTree();
                        }
                    });
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "McMapGrid.Create");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void handleCreateHeightLines() {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    IMcMapHeightLines.SScaleStep[] heightLinesScaleStep;
                    heightLinesScaleStep = new IMcMapHeightLines.SScaleStep[4];
                    SMcBColor[] scaleColorsFirst = new SMcBColor[3];

                    scaleColorsFirst[0] = new SMcBColor(255, 0, 0, 255);
                    scaleColorsFirst[1] = new SMcBColor(0, 255, 0, 255);
                    scaleColorsFirst[2] = new SMcBColor(0, 0, 255, 255);

                    SMcBColor[] scaleColorsSecond = new SMcBColor[3];

                    scaleColorsSecond[0] = new SMcBColor(255, 255, 0, 255);
                    scaleColorsSecond[1] = new SMcBColor(0, 255, 255, 255);
                    scaleColorsSecond[2] = new SMcBColor(255, 0, 255, 255);

                    heightLinesScaleStep[0] = new IMcMapHeightLines.SScaleStep();
                    heightLinesScaleStep[0].fMaxScale = 10;
                    heightLinesScaleStep[0].fLineHeightGap = 10;
                    heightLinesScaleStep[0].aColors = scaleColorsFirst;

                    heightLinesScaleStep[1] = new IMcMapHeightLines.SScaleStep();
                    heightLinesScaleStep[1].fMaxScale = 25;
                    heightLinesScaleStep[1].fLineHeightGap = 25;
                    heightLinesScaleStep[1].aColors = scaleColorsSecond;

                    heightLinesScaleStep[2] = new IMcMapHeightLines.SScaleStep();
                    heightLinesScaleStep[2].fMaxScale = 50;
                    heightLinesScaleStep[2].fLineHeightGap = 50;
                    heightLinesScaleStep[2].aColors = scaleColorsFirst;

                    heightLinesScaleStep[3] = new IMcMapHeightLines.SScaleStep();
                    heightLinesScaleStep[3].fMaxScale = 300;
                    heightLinesScaleStep[3].fLineHeightGap = 100;
                    heightLinesScaleStep[3].aColors = scaleColorsSecond;

                    //IMcMapHeightLines newHeightLines = IMcMapHeightLines.Static.Create(heightLinesScaleStep, 1);
                    IMcMapHeightLines newHeightLines = IMcMapHeightLines.Static.Create(new IMcMapHeightLines.SScaleStep[0], 1);
                    Manager_MCMapHeightLines.getInstance().AddNewHeightLines(newHeightLines);
                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            createViewportTerrainTree();
                        }
                    });
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "McMapHeightLines.Create");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void handleSetGrid(IMcMapViewport mapViewport) {
        String className = GridsListFragment.class.getName();
        GridsListFragment dialog = (GridsListFragment) DialogFragment.instantiate(getActivity(), className);
        dialog.setObject(mapViewport);
        dialog.setTargetFragment(ViewportTerrainTreeViewFragment.this, 1);
        dialog.show(getFragmentManager(), className);
    }

    private void handleRemoveGrid(IMcMapViewport mapViewport) {
        callbackSelectGrid(mapViewport, null);
    }

    private void handleRemoveHeightLines(IMcMapViewport mapViewport) {
        callbackSelectHeightLines(mapViewport, null);
    }

    private void handleSetHeightLines(IMcMapViewport mapViewport) {
        String className = HeightLinesListFragment.class.getName();
        HeightLinesListFragment dialog = (HeightLinesListFragment) DialogFragment.instantiate(getActivity(), className);
        dialog.setObject(mapViewport);
        dialog.setTargetFragment(ViewportTerrainTreeViewFragment.this, 1);
        dialog.show(getFragmentManager(), className);
    }

    private void handleDeleteName(final Object mcObj) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                if (Manager_MCNames.getInstance().removeName(mcObj)) {
                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            createViewportTerrainTree();
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

    @Override
    public void onHiddenChanged(boolean hidden) {
        super.onHiddenChanged(hidden);
        if (!hidden)
            createViewportTerrainTree();
    }

    @Override
    public void onResume() {
        super.onResume();
        if (!getUserVisibleHint()) {
            return;
        }
        createViewportTerrainTree();
    }

    private PopupMenu.OnMenuItemClickListener getTerrainMenuClickListener(final IMcMapTerrain terrain, final IMcMapViewport viewport, final TreeNode terrainNode) {
        PopupMenu.OnMenuItemClickListener menuItemClickListener = new PopupMenu.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(MenuItem item) {
                int id = item.getItemId();
                if (id == R.id.tv_terrain_remove_terrain) {
                    handleRemoveTerrain(terrain, viewport, terrainNode);
                }
                else if (id == R.id.tv_terrain_rename) {
                    handleRename(terrain);
                }
                else if (id == R.id.tv_terrain_delete_name) {
                    handleDeleteName(terrain);
                }
                return true;
            }
        };
        return menuItemClickListener;
    }

    private PopupMenu.OnMenuItemClickListener getCameraMenuClickListener(final IMcMapCamera camera, final IMcMapViewport viewport) {
        PopupMenu.OnMenuItemClickListener menuItemClickListener = new PopupMenu.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(MenuItem item) {
                int id = item.getItemId();
                if (id == R.id.tv_vp_destroy_camera) {
                    handleDestroyCamera(camera, viewport);
                }
                else if (id == R.id.tv_vp_set_camera) {
                    handleSetCamera(camera, viewport);
                }
                return true;
            }
        };
        return menuItemClickListener;
    }

    private void handleRemoveTerrain(final IMcMapTerrain terrain, final IMcMapViewport viewport, final TreeNode terrainNode) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {

                try {
                    viewport.RemoveTerrain(terrain);
                    Manager_MCTerrain.getInstance().RemoveTerrain(terrain);
                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            mRoot.getViewHolder().getTreeView().removeNode(terrainNode);
                        }
                    });
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "RemoveTerrain");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void handleDestroyCamera(final IMcMapCamera camera, final IMcMapViewport viewport) {

        AlertMessages.ShowYesNoMessage(
                getActivity(),
                "Destroy Camera",
                "Do You Want To Destroy Camera?",
                /*Yes click*/ new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {
                        Funcs.runMapCoreFunc(new Runnable() {
                            @Override
                            public void run() {

                                try {
                                    viewport.DestroyCamera(camera);
                                    getActivity().runOnUiThread(new Runnable() {
                                        @Override
                                        public void run() {
                                            createViewportTerrainTree();
                                        }
                                    });
                                } catch (MapCoreException e) {
                                    AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "DestroyCamera");
                                    e.printStackTrace();
                                } catch (Exception e) {
                                    e.printStackTrace();
                                }
                            }
                        });
                    }
                },
                null);
    }

    private void handleSetCamera(final IMcMapCamera camera, final IMcMapViewport viewport) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {

                try {
                    viewport.SetActiveCamera(camera);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "SetActiveCamera");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private PopupMenu.OnMenuItemClickListener getLayerMenuClickListener(final IMcMapTerrain terrain, final IMcMapLayer layer) {
        PopupMenu.OnMenuItemClickListener menuItemClickListener = new PopupMenu.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(MenuItem item) {
                int id = item.getItemId();
                if (id == R.id.tv_layer_remove_layer) {
                    handleRemoveLayer(terrain, layer);
                }
                else if (id == R.id.tv_layer_move_to_center) {
                    handleMoveToCenter(layer);
                }
                else if (id == R.id.tv_layer_rename) {
                    handleRename(layer);
                }
                else if (id == R.id.tv_layer_delete_name) {
                    handleDeleteName(layer);
                }
                return true;
            }
        };
        return menuItemClickListener;
    }

    private void handleRemoveLayer(final IMcMapTerrain terrain, final IMcMapLayer layer) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    if (terrain != null)
                        terrain.RemoveLayer(layer);
                    layer.Release();
                } catch (MapCoreException e) {
                    e.printStackTrace();
                    AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "RemoveLayer");
                } catch (Exception e) {
                    e.printStackTrace();
                }
                getActivity().runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        createViewportTerrainTree();
                    }
                });
            }
        });
    }

    private void handleMoveToCenter(final IMcMapLayer layer) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try{
                    SMcVector3D centerPoint = layer.GetBoundingBox().GetCenterPoint();

                    Funcs.MoveToLayerCenter(centerPoint ,Manager_AMCTMapForm.getInstance().getCurViewport(), getContext());
                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            getActivity().onBackPressed();
                        }
                    });
                } catch (MapCoreException mcEx) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "GetBoundingBox");
                    mcEx.printStackTrace();
                } catch (Exception mcEx) {
                    mcEx.printStackTrace();
                }
            }
        });
    }

    private String getLayerFragNameFromLayerName(String layerName) {
        //replace underscore with uppercase letter
        String relatedFragmentName = Funcs.camelCasify(layerName.toLowerCase());
        //remove elt prefix
        relatedFragmentName = relatedFragmentName.substring(3);
        relatedFragmentName += "TabsFragment"/*"MapLayerFragment"*/;
        return relatedFragmentName;
    }

    public void openAddTerrainFrag(IMcMapViewport viewport) {
        TerrainsFragment terrainsFragment = TerrainsFragment.newInstance(false);
        terrainsFragment.setObject(viewport);
        FragmentTransaction transaction = getFragmentManager().beginTransaction().add(R.id.fragment_container, terrainsFragment, terrainsFragment.getClass().getSimpleName());
        transaction.addToBackStack("add" + terrainsFragment.getClass().getSimpleName());
        transaction.hide(getFragmentManager().findFragmentByTag(ViewportTerrainTreeViewFragment.class.getSimpleName()));
        transaction.commit();
    }

    @Override
    public void onCreateOptionsMenu(Menu menu, MenuInflater inflater) {
        menu.clear();
        getActivity().getMenuInflater().inflate(R.menu.menu_viewports_terrains_tv, menu);
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        int id = item.getItemId();
        if (id == R.id.viewport_terrain_tv_nav_create_new_grid) {
            handleCreateGrid();
            return true;
        } else if (id == R.id.viewport_terrain_tv_nav_create_new_height_lines) {
            handleCreateHeightLines();
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
    public void callbackSelectGrid(final IMcMapViewport viewport, final IMcMapGrid selectGrid) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    viewport.SetGrid(selectGrid);
                    Message msg = mHandler.obtainMessage(CREATE_TREE);
                    msg.sendToTarget();
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "SetGrid");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    @Override
    public void callbackSelectHeightLines(final IMcMapViewport viewport, final IMcMapHeightLines selectHeightLines) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    viewport.SetHeightLines(selectHeightLines);
                    Message msg = mHandler.obtainMessage(CREATE_TREE);
                    msg.sendToTarget();
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "SetHeightLines");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    @Override
    public void onNameChanged() {
        createViewportTerrainTree();
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
