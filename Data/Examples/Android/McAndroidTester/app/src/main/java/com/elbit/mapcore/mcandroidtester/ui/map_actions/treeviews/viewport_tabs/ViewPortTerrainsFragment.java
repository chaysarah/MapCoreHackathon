package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.viewport_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.ListView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.ui.adapters.OverlayAdapter;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.treeview.holder.IconTreeItemHolder;
import com.elbit.mapcore.mcandroidtester.utils.treeview.model.TreeNode;
import com.elbit.mapcore.mcandroidtester.utils.treeview.view.AndroidTreeView;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcMapTerrain;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link ViewPortTerrainsFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link ViewPortTerrainsFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class ViewPortTerrainsFragment extends Fragment implements FragmentWithObject {
    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;

    private OnFragmentInteractionListener mListener;
    private IMcMapViewport mViewport;
    private View mRootView;
    private TreeNode mRoot;
    private ListView mVisibleOverlaysList;
    private EditText mDrawPriority;
    private EditText mNumTiles;
    private IMcMapTerrain mCurTerrain;
    private Button mSaveBttn;
    private Button mSaveTerrainDrawPriorityBttn;
    private Button mTerrainNumCacheTilesBttn;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment ViewPortTerrainsFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static ViewPortTerrainsFragment newInstance(String param1, String param2) {
        ViewPortTerrainsFragment fragment = new ViewPortTerrainsFragment();
        Bundle args = new Bundle();
        args.putString(ARG_PARAM1, param1);
        args.putString(ARG_PARAM2, param2);
        fragment.setArguments(args);
        return fragment;
    }

    public ViewPortTerrainsFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if (getArguments() != null) {
            mParam1 = getArguments().getString(ARG_PARAM1);
            mParam2 = getArguments().getString(ARG_PARAM2);
        }
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        Funcs.SetObjectFromBundle(savedInstanceState, this );
        mRootView = inflater.inflate(R.layout.fragment_view_port_terrains, container, false);
        initEditTxts();
        loadTerrainLayersTree();
        loadOverlaysList();
        initSaveBttns();
        // Inflate the layout for this fragment
        return mRootView;
    }

    private void initSaveBttns() {
        initSaveTerrainDrawPriorityBttn();
        initSaveTerrainNumCacheTilesBttn();

    }

    private void initSaveTerrainNumCacheTilesBttn() {
        mTerrainNumCacheTilesBttn = (Button) mRootView.findViewById(R.id.view_port_details_num_tiles_ok_bttn);
        mTerrainNumCacheTilesBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final int numTiles =  Integer.valueOf(mNumTiles.getText().toString());
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            mViewport.SetTerrainNumCacheTiles(mCurTerrain,false, numTiles);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetTerrainNumCacheTiles");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });
    }

    private void initSaveTerrainDrawPriorityBttn() {
        mSaveTerrainDrawPriorityBttn = (Button) mRootView.findViewById(R.id.view_port_terrains_draw_priority__ok_bttn);
        mSaveTerrainDrawPriorityBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final byte drawPriority = Byte.valueOf(mDrawPriority.getText().toString());
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            mViewport.SetTerrainDrawPriority(mCurTerrain, drawPriority);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetTerrainDrawPriority");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });
    }

    private void initEditTxts() {
        mDrawPriority = (EditText) mRootView.findViewById(R.id.view_port_terrains_draw_priority_et);
        mNumTiles = (EditText) mRootView.findViewById(R.id.view_port_details_num_tiles_et);
    }

    private void loadOverlaysList() {
        mVisibleOverlaysList = (ListView) mRootView.findViewById(R.id.view_port_terrains_visible_overlays_lv);
        try {
            OverlayAdapter adapter = new OverlayAdapter(mViewport.GetVisibleOverlays());
            mVisibleOverlaysList.setAdapter(adapter);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void loadTerrainLayersTree() {
        IMcMapTerrain[] terrains = new IMcMapTerrain[0];
        if (mRoot != null) {
            mRoot.getViewHolder().getTreeView().removeAllNodes();
        }
        mRoot = TreeNode.root();
        try {
            terrains = mViewport.GetTerrains();
            for (IMcMapTerrain terrain : terrains) {
                TreeNode parentVP = new TreeNode(new IconTreeItemHolder.IconTreeItem(R.string.ic_terrain, "Map Terrain  " + terrain.hashCode(), terrain));
                addTerrainLayersToTree(mViewport.GetVisibleLayers(terrain),parentVP);
                parentVP.setLongClickListener(new TreeNode.TreeNodeLongClickListener() {
                    @Override
                    public boolean onLongClick(TreeNode node, Object value) {
                        // node.getViewHolder().getView().setBackgroundColor(getResources().getColor(R.color.text_lgray));
                        mCurTerrain = (IMcMapTerrain) ((IconTreeItemHolder.IconTreeItem) value).mImcObj;
                        try {
                            mDrawPriority.setText(String.valueOf(mViewport.GetTerrainDrawPriority(mCurTerrain)));
                            //TODO uncomment after GetTerrainNumCacheTile will be fixed
                             mNumTiles.setText(String.valueOf(mViewport.GetTerrainNumCacheTiles(mCurTerrain,true)));
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        return true;
                    }
                });
                mRoot.addChild(parentVP);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        AndroidTreeView tView = new AndroidTreeView(getActivity(), mRoot);
        tView.setDefaultAnimation(true);
        tView.setDefaultContainerStyle(R.style.TreeNodeStyleCustom);
        tView.setDefaultViewHolder(IconTreeItemHolder.class);
        ((LinearLayout) mRootView.findViewById(R.id.view_port_terrains_tree_view_container)).addView(tView.getView());
    }

    private void addTerrainLayersToTree(IMcMapLayer[] iMcMapLayers, TreeNode terrainNode) {
        IMcMapLayer[] layers = iMcMapLayers;
        for (int j = 0; j < layers.length; j++) {
            TreeNode layerNode = null;
            try {
                String layerName = layers[j].GetLayerType().name();
                String layerTitle = layers[j].hashCode() + " " + layerName + " Map Layer";
                layerNode = new TreeNode(new IconTreeItemHolder.IconTreeItem(R.string.ic_label, layerTitle));
            } catch (MapCoreException e) {
                e.printStackTrace();
                AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "GetLayerType()");
            } catch (Exception e) {
                e.printStackTrace();
            }
            terrainNode.addChild(layerNode);
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
   /*     if (context instanceof OnFragmentInteractionListener) {
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

    @Override
    public void setObject(Object obj) {
        mViewport = (IMcMapViewport) obj;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mViewport));
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
