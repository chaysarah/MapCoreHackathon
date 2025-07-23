package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.layers_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.EditText;
import android.widget.ExpandableListAdapter;
import android.widget.ExpandableListView;
import android.widget.ListView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_AMCTMapForm;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_MCGridCoordinateSystem;
import com.elbit.mapcore.mcandroidtester.ui.adapters.HashMapAdapter;
import com.elbit.mapcore.mcandroidtester.ui.adapters.ViewPortsTerrainsELAdapter;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.WorldBoundingBox;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcMapTerrain;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link MapLayerFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link MapLayerFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class MapLayerFragment extends Fragment implements FragmentWithObject {
    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;

    private OnFragmentInteractionListener mListener;
    private IMcMapLayer mMapLayer;
    private NumericEditTextLabel mLayerIdET;
    private EditText mLayerTypeET;
    private HashMapAdapter mGridCoordSysAdapter;
    private ListView mGridCoordSysLV;
    private NumericEditTextLabel mBgThreadIndexET;
    private ExpandableListAdapter mViewPortsAdapter;
    private ExpandableListView mViewPortsList;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment MapLayerFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static MapLayerFragment newInstance(String param1, String param2) {
        MapLayerFragment fragment = new MapLayerFragment();
        Bundle args = new Bundle();
        args.putString(ARG_PARAM1, param1);
        args.putString(ARG_PARAM2, param2);
        fragment.setArguments(args);
        return fragment;
    }

    public MapLayerFragment() {
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
        setTitle();
        View view = inflater.inflate(R.layout.fragment_map_layer, container, false);
        initEditTxts(view);
        initWorldBoundingBox(view);
        initBgThreadIndexEt(view);
        loadCoordinateSysList(view);
        loadViewPortsList(view);
        // Inflate the layout for this fragment
        return view;
    }

    private void setTitle() {
        getActivity().setTitle("map layer details");
    }

    private void initBgThreadIndexEt(View view) {
        mBgThreadIndexET = (NumericEditTextLabel) view.findViewById(R.id.map_layer_bg_thread_index);
        try {
            mBgThreadIndexET.setText(String.valueOf(mMapLayer.GetBackgroundThreadIndex()));
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "mMapLayer.GetBackgroundThreadIndex");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }

    }
    @Override
    public void onHiddenChanged(boolean hidden) {
        super.onHiddenChanged(hidden);
        if(!hidden)
            setTitle();
    }
    private void loadViewPortsList(View view) {
        ArrayList<IMcMapViewport> viewPortList = Manager_AMCTMapForm.getInstance().getAllImcViewports();
        HashMap<IMcMapViewport, List<IMcMapTerrain>> viewPortsTerrains=createViewPortsTerrainsList();
        ViewPortsTerrainsELAdapter viewportsTerrainsAdapter = new ViewPortsTerrainsELAdapter(getContext(),viewPortList,viewPortsTerrains);
        mViewPortsList = (ExpandableListView) view.findViewById(R.id.map_layer_viewport_terrains_list);
        mViewPortsList.setAdapter(viewportsTerrainsAdapter);
        mViewPortsList.setOnTouchListener(new View.OnTouchListener() {
            // Setting on Touch Listener for handling the touch inside ScrollView
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                // Disallow the touch request for parent scroll on touch of child view
                v.getParent().requestDisallowInterceptTouchEvent(true);
                return false;
            }
        });
        Funcs.setExpandableListViewHeightBasedOnChildren(mViewPortsList);
    }


    private HashMap<IMcMapViewport, List<IMcMapTerrain>> createViewPortsTerrainsList() {

        ArrayList<IMcMapViewport> viewPortList = Manager_AMCTMapForm.getInstance().getAllImcViewports();
        HashMap<IMcMapViewport, List<IMcMapTerrain>> viewPortTerrainsList = new HashMap<>();
        try {
            for (IMcMapViewport viewport : viewPortList) {
                List<IMcMapTerrain> terrainsList = new ArrayList<>(Arrays.asList(viewport.GetTerrains()));
                List<IMcMapTerrain> terrainsWithCurrLayer=new ArrayList<>();
                for (IMcMapTerrain terrain : terrainsList) {
                    List layersList = new ArrayList<>(Arrays.asList((terrain).GetLayers()));
                    if (layersList.contains(mMapLayer))
                        terrainsWithCurrLayer.add(terrain);
                }
                viewPortTerrainsList.put(viewport, terrainsWithCurrLayer);
            }
        } catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), mcEx, "createViewPortsTerrainsList");
        } catch (Exception e) {
            e.printStackTrace();
        }
        return viewPortTerrainsList;
    }


    private void loadCoordinateSysList(View view) {
        mGridCoordSysAdapter = new HashMapAdapter(getActivity(), Manager_MCGridCoordinateSystem.getInstance().getdGridCoordSys(), Consts.ListType.NON_CHECK);
        if (Manager_MCGridCoordinateSystem.getInstance().getdGridCoordSys() != null) {
            mGridCoordSysLV = (ListView) view.findViewById(R.id.map_layer_coord_sys_list);
            mGridCoordSysLV.setAdapter(null);
            mGridCoordSysLV.setAdapter(mGridCoordSysAdapter);
            mGridCoordSysLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                @Override
                public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                    Map.Entry<Object, Integer> item = (Map.Entry<Object, Integer>) mGridCoordSysLV.getAdapter().getItem(position);
                    //show the selected coordinate system corresponding params (datum etc.)
                    // showSelectedCoordSysParams((IMcGridCoordinateSystem) item.getKey());
                }
            });
        }
        mGridCoordSysLV.deferNotifyDataSetChanged();
        mGridCoordSysLV.setOnTouchListener(new View.OnTouchListener() {
            // Setting on Touch Listener for handling the touch inside ScrollView
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                // Disallow the touch request for parent scroll on touch of child view
                v.getParent().requestDisallowInterceptTouchEvent(true);
                return false;
            }
        });
        Funcs.setListViewHeightBasedOnChildren(mGridCoordSysLV);
    }

    private void initWorldBoundingBox(View view) {
        try {
            WorldBoundingBox worldBoundingBox = (WorldBoundingBox) view.findViewById(R.id.map_layer_world_bounding_box);
            worldBoundingBox.initWorldBoundingBox(mMapLayer.GetBoundingBox());
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "mMapLayer.GetBoundingBox()");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initEditTxts(View view) {

        initLayerIdET(view);
        initLayerType(view);
    }

    private void initLayerType(View view) {
        mLayerTypeET = (EditText) view.findViewById(R.id.map_layer_layer_type);
        try {
            mLayerTypeET.setText(String.valueOf(mMapLayer.GetLayerType()));
        } catch (Exception e) {
            e.printStackTrace();
        }

    }

    private void initLayerIdET(View view) {
        mLayerIdET = (NumericEditTextLabel) view.findViewById(R.id.map_layer_layer_id);
        try {
            mLayerIdET.setText(String.valueOf(mMapLayer.GetID()));
        } catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), mcEx, "mMapLayer.GetID()");
        } catch (Exception e) {
            e.printStackTrace();
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
        if (context instanceof OnFragmentInteractionListener) {
            mListener = (OnFragmentInteractionListener) context;
        } else {
            throw new RuntimeException(context.toString()
                    + " must implement OnFragmentInteractionListener");
        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    @Override
    public void setObject(Object obj) {
        mMapLayer = (IMcMapLayer) obj;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mMapLayer));
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
