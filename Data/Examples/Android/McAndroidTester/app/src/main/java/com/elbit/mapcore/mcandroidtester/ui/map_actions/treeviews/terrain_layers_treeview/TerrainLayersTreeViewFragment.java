package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.terrain_layers_treeview;

import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
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

import com.elbit.mapcore.Structs.SMcVector3D;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.interfaces.ITreeView;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_AMCTMapForm;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_MCLayers;
import com.elbit.mapcore.mcandroidtester.managers.Manager_MCNames;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_MCTerrain;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.LayersActivity;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.TerrainActivity;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.LayersFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.terrain_tabs.TerrainDetailsTabsFragment;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.ChangingNamesDialogFragment;
import com.elbit.mapcore.mcandroidtester.utils.treeview.holder.IconTreeItemHolder;
import com.elbit.mapcore.mcandroidtester.utils.treeview.model.TreeNode;
import com.elbit.mapcore.mcandroidtester.utils.treeview.view.AndroidTreeView;

import java.util.ArrayList;
import java.util.Set;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Map.IMcMapCamera;
import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcMapTerrain;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link TerrainLayersTreeViewFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link TerrainLayersTreeViewFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class TerrainLayersTreeViewFragment extends Fragment implements ITreeView, ISelectLayersCallback, ChangingNamesDialogFragment.OnNameChangedListener{

    private OnFragmentInteractionListener mListener;
    private IMcMapViewport mCurViewport;
    private TreeNode mRoot;
    private View mRootView;
    public static int SelectedNodeHashCode = 0;
    private AndroidTreeView mTerrainLayersTreeView;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment TerrainLayersTreeViewFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static TerrainLayersTreeViewFragment newInstance() {
        TerrainLayersTreeViewFragment fragment = new TerrainLayersTreeViewFragment();
        return fragment;
    }

    public TerrainLayersTreeViewFragment() {
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
        setTitle();
        // Inflate the layout for this fragment
        mRootView = inflater.inflate(R.layout.fragment_terrain_layers_tree_view, container, false);
        if (savedInstanceState != null)
            SelectedNodeHashCode = savedInstanceState.getInt(AndroidTreeView.SELECTED_TREE_VIEW_NODE);
        else
            SelectedNodeHashCode = 0;
        return mRootView;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putInt(AndroidTreeView.SELECTED_TREE_VIEW_NODE, SelectedNodeHashCode);
    }

    private void openLayersFragment(IMcMapTerrain terrain) {
        FragmentWithObject fragment =  (FragmentWithObject) Fragment.instantiate(getActivity(), LayersFragment.class.getName());
        fragment.setObject(terrain);
        FragmentTransaction transaction = getFragmentManager().beginTransaction();
        transaction.hide(this);
        transaction.add(R.id.fragment_container, (Fragment) fragment, LayersFragment.class.getSimpleName());
        transaction.addToBackStack(LayersFragment.class.getSimpleName());
        transaction.commit();
    }

    private void setTitle() {
        getActivity().setTitle("terrain layers tree view");
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
            ((MapsContainerActivity) context).mCurFragmentTag = TerrainLayersTreeViewFragment.class.getSimpleName();
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
            return true;
        }
    };

    private void startRelatedFragment(IconTreeItemHolder.IconTreeItem item) {
        if (!item.mFragmentName.isEmpty()) {
            try {
                FragmentWithObject fragment = (FragmentWithObject) Fragment.instantiate(getActivity(), item.mFragmentName);
                fragment.setObject(item.mImcObj);
                FragmentTransaction transaction = getFragmentManager().beginTransaction();
                transaction.hide(this);
                transaction.add(R.id.fragment_container, (Fragment) fragment, item.mFragmentName);
                transaction.addToBackStack(item.mFragmentName);
                transaction.commit();
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    private void createTerrainLayersTree() {
        //InitLayersFragmentNames();
        if (mRoot != null) {
            mRoot.getViewHolder().getTreeView().removeAllNodes();
        }
        mRoot = TreeNode.root();
        addTerrainsToTree(mRoot);
        addLayersToTree(mRoot);
        mTerrainLayersTreeView = new AndroidTreeView(getActivity(), mRoot);
        mTerrainLayersTreeView.setDefaultAnimation(false);
        mTerrainLayersTreeView.setDefaultContainerStyle(R.style.TreeNodeStyleCustom);
        mTerrainLayersTreeView.setDefaultViewHolder(IconTreeItemHolder.class);
        mTerrainLayersTreeView.setDefaultNodeLongClickListener(nodeLongClickListener);
        ((LinearLayout) getView().findViewById(R.id.terrain_layers_tree_view_container_fl)).addView(mTerrainLayersTreeView.getView());

        mTerrainLayersTreeView.expandNodeTillRoot(SelectedNodeHashCode);
    }

    private void addLayersToTree(TreeNode parentVP) {
        Set<Object> layers = Manager_MCLayers.getInstance().getAllStandaloneLayers().keySet();
        if (layers != null) {
            for (Object layer : layers) {
                TreeNode layerNode = null;
                String layerTitle = Manager_MCNames.getInstance().getNameByObject(layer);
                String relatedFragmentName = Manager_MCLayers.getInstance().GetLayerFragmentByType((IMcMapLayer) layer, this.getContext());
                layerNode = new TreeNode(new IconTreeItemHolder.IconTreeItem(
                        R.string.ic_label,
                        layerTitle,
                        IconTreeItemHolder.IconTreeItem.TreeViewType.TL,
                        relatedFragmentName,
                        layer,
                        R.menu.treeview_layer_item,
                        getLayerMenuClickListener(null, ((IMcMapLayer) layer))));

                parentVP.addChild(layerNode);
            }
        }
    }

    @Override
    public void onHiddenChanged(boolean hidden) {
        super.onHiddenChanged(hidden);
        if (!hidden)
        {
            createTerrainLayersTree();
        }
    }

    private PopupMenu.OnMenuItemClickListener getTerrainMenuClickListener(final IMcMapTerrain terrain, final TreeNode terrainNode) {
        PopupMenu.OnMenuItemClickListener menuItemClickListener = new PopupMenu.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(MenuItem item) {
                int id = item.getItemId();
                if (id == R.id.tv_terrain_layer_terrain_delete_terrain) {
                    Manager_MCTerrain.getInstance().RemoveTerrain(terrain);
                    createTerrainLayersTree();
                }
                else if (id == R.id.tv_terrain_layer_terrain_add_layer) {
                    openLayersFragment(terrain);
                }
                else if (id == R.id.tv_terrain_layer_terrain_remove_layer) {
                    handleRemoveLayerFromTerrain(terrain);
                }
                else if (id == R.id.tv_terrain_layer_terrain_rename) {
                    handleRename(terrain);
                }
                else if (id == R.id.tv_terrain_layer_terrain_delete_name) {
                    handleDeleteName(terrain);
                }
                return true;
            }
        };
        return menuItemClickListener;
    }

    private void handleRemoveLayerFromTerrain(IMcMapTerrain terrain) {
        String className = LayersListFragment.class.getName();
        LayersListFragment dialog = (LayersListFragment) DialogFragment.instantiate(getActivity(), className);
        if (terrain != null)
             dialog.setObject(terrain);
        dialog.setTargetFragment(TerrainLayersTreeViewFragment.this, 1);
        dialog.show(getFragmentManager(), className);
    }

    private void addCameraToTree(IMcMapCamera iMcMapCamera, TreeNode parentVP) {
        IMcMapCamera camera = iMcMapCamera;
        TreeNode cameraNode = new TreeNode(new IconTreeItemHolder.IconTreeItem(
                R.string.ic_camera,
                camera.hashCode() + " Map Camera",
                IconTreeItemHolder.IconTreeItem.TreeViewType.TL));
        parentVP.addChild(cameraNode);
    }

    private void addTerrainsToTree(TreeNode parentVP) {
        Set<Object> terrains;
        terrains = Manager_MCTerrain.getInstance().getTerrains().keySet();
        if (terrains != null) {
            for (Object terrain : terrains) {
                IconTreeItemHolder.IconTreeItem iconTreeItem = new IconTreeItemHolder.IconTreeItem(
                        R.string.ic_terrain,
                        Manager_MCNames.getInstance().getNameByObject(terrain),
                        IconTreeItemHolder.IconTreeItem.TreeViewType.TL,
                        TerrainDetailsTabsFragment.class.getCanonicalName(),
                        terrain,
                        R.menu.treeview_terrain_layer_terrain_item);
                TreeNode terrainNode = new TreeNode(iconTreeItem);
                iconTreeItem.setOnMenuItemClickListener(getTerrainMenuClickListener((IMcMapTerrain) terrain, terrainNode));
                parentVP.addChild(terrainNode);
                try {
                    addTerrainLayersToTree(((IMcMapTerrain) terrain).GetLayers(), terrainNode, (IMcMapTerrain) terrain);
                } catch (MapCoreException e) {
                    e.printStackTrace();
                    AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "terrains[i].GetLayers()");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        }
    }

    private void addTerrainLayersToTree(IMcMapLayer[] iMcMapLayers, TreeNode terrainNode, IMcMapTerrain terrain) {
        IMcMapLayer[] layers = iMcMapLayers;
        for (int j = 0; j < layers.length; j++) {
            TreeNode layerNode = null;

            String layerTitle = Manager_MCNames.getInstance().getNameByObject(layers[j]);
            String relatedFragmentName = Manager_MCLayers.getInstance().GetLayerFragmentByType(layers[j], this.getContext());
            layerNode = new TreeNode(new IconTreeItemHolder.IconTreeItem(
                    R.string.ic_label,
                    layerTitle,
                    IconTreeItemHolder.IconTreeItem.TreeViewType.TL,
                    relatedFragmentName,
                    layers[j],
                    R.menu.treeview_layer_item,
                    getLayerMenuClickListener(terrain, layers[j])));

            terrainNode.addChild(layerNode);
        }
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

    private void removeLayer(final IMcMapTerrain terrain, final IMcMapLayer layer) {
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
    }

    private void handleRemoveLayer(final IMcMapTerrain terrain, final IMcMapLayer layer) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                removeLayer(terrain, layer);
                getActivity().runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        createTerrainLayersTree();
                    }
                });
            }
        });
    }

    private void handleMoveToCenter(final IMcMapLayer layer) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    SMcVector3D centerPoint = layer.GetBoundingBox().GetCenterPoint();
                    Funcs.MoveToLayerCenter(centerPoint, Manager_AMCTMapForm.getInstance().getCurViewport(), getContext());
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

    private void handleDeleteName(final Object mcObj) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                if (Manager_MCNames.getInstance().removeName(mcObj)) {
                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            createTerrainLayersTree();
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

    private String getLayerFragNameFromLayerName(String layerName) {
        //replace underscore with uppercase letter
        String relatedFragmentName = Funcs.camelCasify(layerName.toLowerCase());
        //remove elt prefix
        relatedFragmentName = relatedFragmentName.substring(3);
        relatedFragmentName += "TabsFragment"/*"MapLayerFragment"*/;
        return relatedFragmentName;
    }

   /* public static Hashtable<IMcMapLayer.ELayerType,String> layersFragmentNames = new Hashtable<>();
    public static void InitLayersFragmentNames()
    {
        layersFragmentNames.clear();
        layersFragmentNames.put(IMcMapLayer.ELayerType.ELT_NATIVE_RASTER, RasterTabsFragment.class.getName());
        layersFragmentNames.put(IMcMapLayer.ELayerType.ELT_RAW_RASTER, RasterTabsFragment.class.getName());
        layersFragmentNames.put(IMcMapLayer.ELayerType.ELT_NATIVE_DTM, DtmTabsFragment.class.getName());
        layersFragmentNames.put(IMcMapLayer.ELayerType.ELT_RAW_DTM, DtmTabsFragment.class.getName());
        layersFragmentNames.put(IMcMapLayer.ELayerType.ELT_NATIVE_VECTOR_3D_EXTRUSION, Vector3DExtrusionTabsFragment.class.getName());
        layersFragmentNames.put(IMcMapLayer.ELayerType.ELT_RAW_VECTOR_3D_EXTRUSION, Vector3DExtrusionTabsFragment.class.getName());
        layersFragmentNames.put(IMcMapLayer.ELayerType.ELT_NATIVE_3D_MODEL, StaticObjects3DModelTabsFragment.class.getName());
        layersFragmentNames.put(IMcMapLayer.ELayerType.ELT_RAW_3D_MODEL, StaticObjects3DModelTabsFragment.class.getName());
        layersFragmentNames.put(IMcMapLayer.ELayerType.ELT_NATIVE_VECTOR, VectorTabsFragment.class.getName());
        layersFragmentNames.put(IMcMapLayer.ELayerType.ELT_RAW_VECTOR, VectorTabsFragment.class.getName());
    }*/

    @Override
    public void onCreateOptionsMenu(Menu menu, MenuInflater inflater) {
            menu.clear();
            getActivity().getMenuInflater().inflate(R.menu.menu_terrains_layers_tv, menu);
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        int id = item.getItemId();
        if (id == R.id.terrain_layers_tv_nav_create_new_terrain) {
            Intent intent = new Intent(getActivity(), TerrainActivity.class);

            startActivity(intent);
            return true;
        } else if (id == R.id.terrain_layers_tv_nav_create_new_layer) {
            // openLayersFragment();
            Intent intent = new Intent(getActivity(), LayersActivity.class);
            startActivity(intent);
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
        createTerrainLayersTree();
    }

    @Override
    public void callbackSelectLayers(final IMcMapTerrain terrain, final ArrayList<IMcMapLayer> selectLayers) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                for (IMcMapLayer layer:selectLayers) {
                    removeLayer(terrain,layer);
                }
                getActivity().runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        createTerrainLayersTree();
                    }
                });
            }
        });

    }

    @Override
    public void onNameChanged() {
        createTerrainLayersTree();
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
