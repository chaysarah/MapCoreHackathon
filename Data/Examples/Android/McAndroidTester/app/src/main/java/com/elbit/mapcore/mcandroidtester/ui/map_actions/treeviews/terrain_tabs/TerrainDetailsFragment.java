package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.terrain_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.ListView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.interfaces.OnSelectViewportFromListListener;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_MCGridCoordinateSystem;
import com.elbit.mapcore.mcandroidtester.ui.adapters.HashMapAdapter;
import com.elbit.mapcore.mcandroidtester.ui.adapters.ViewPortsListAdapter;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.ViewportsList;
import com.elbit.mapcore.mcandroidtester.utils.customviews.WorldBoundingBox;

import java.util.Map;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Map.IMcMapTerrain;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;


/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link TerrainDetailsFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link TerrainDetailsFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class TerrainDetailsFragment extends Fragment implements FragmentWithObject , OnSelectViewportFromListListener {

    private OnFragmentInteractionListener mListener;
    private ViewPortsListAdapter mViewPortsAdapter;
    private IMcMapTerrain mMapTerrain;
    private HashMapAdapter mGridCoordSysAdapter;
    private ListView mGridCoordSysLV;
    private ViewportsList mViewportsLV;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
    * @return A new instance of fragment TerrainDetailsFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static TerrainDetailsFragment newInstance() {
        TerrainDetailsFragment fragment = new TerrainDetailsFragment();
        return fragment;
    }

    public TerrainDetailsFragment() {
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
        setTitle();
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_terrain_details, container, false);
    }

    private void setTitle() {
        getActivity().setTitle("terrain details");

    }
    @Override
    public void onHiddenChanged(boolean hidden) {
        super.onHiddenChanged(hidden);
        if(!hidden)
            setTitle();
    }
    // TODO: Rename method, update argument and hook method into UI event
    public void onButtonPressed(Uri uri) {
        if (mListener != null) {
            mListener.onFragmentInteraction(uri);
        }
    }

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        loadViewPortsList();
        loadCoordinateSysList();
        initDefaultVisibility();
        initWorldBoundingBox();
    }

    private void initWorldBoundingBox() {
        try {
            WorldBoundingBox worldBoundingBox = (WorldBoundingBox) getView().findViewById(R.id.terrain_details_world_bounding_box);
            worldBoundingBox.initWorldBoundingBox(mMapTerrain.GetBoundingBox());
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "mMapTerrain.GetBoundingBox()");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initDefaultVisibility() {
        CheckBox defaultVisibilityCB = (CheckBox) getView().findViewById(R.id.default_visibility_cb);
        try {
            defaultVisibilityCB.setChecked(mMapTerrain.GetVisibility());
            defaultVisibilityCB.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
                @Override
                public void onCheckedChanged(CompoundButton buttonView, final boolean isChecked) {
                    Funcs.runMapCoreFunc(new Runnable() {
                        @Override
                        public void run() {
                            try {
                                mMapTerrain.SetVisibility(isChecked);
                            } catch (MapCoreException e) {
                                AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "mMapTerrain.SetVisibility");
                                e.printStackTrace();
                            } catch (Exception e) {
                                e.printStackTrace();
                            }
                        }
                    });
                }
        });
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "mMapTerrain.GetVisibility");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
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
        mMapTerrain = (IMcMapTerrain) obj;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mMapTerrain));
    }

    @Override
    public void onSelectViewportFromList(final IMcMapViewport mcSelectedViewport,final boolean isChecked) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    mMapTerrain.SetVisibility(isChecked, mcSelectedViewport);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "mMapTerrain.SetVisibility");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
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

    private void loadViewPortsList() {
        mViewportsLV = (ViewportsList) getView().findViewById(R.id.terrain_details_viewports_lv);
        try {
            mViewportsLV.initViewportsList(this,
                    Consts.ListType.MULTIPLE_CHECK,
                    ListView.CHOICE_MODE_MULTIPLE,
                    null,
                    mMapTerrain,
                    mMapTerrain.getClass().getMethod("GetVisibility",IMcMapViewport.class),
                    true);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
/**/
    private void loadCoordinateSysList() {
        mGridCoordSysAdapter = new HashMapAdapter(getActivity(), Manager_MCGridCoordinateSystem.getInstance().getdGridCoordSys(), Consts.ListType.SINGLE_CHECK);
        if (Manager_MCGridCoordinateSystem.getInstance().getdGridCoordSys() != null) {
            mGridCoordSysLV = (ListView) getView().findViewById(R.id.gcs_lv);
            mGridCoordSysLV.setChoiceMode(ListView.CHOICE_MODE_SINGLE);
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
        selectCurrGridCoordSys();
        mGridCoordSysLV.deferNotifyDataSetChanged();
        Funcs.setListViewHeightBasedOnChildren(mGridCoordSysLV);
    }

    private void selectCurrGridCoordSys() {
        {
            int i;
            for (i = 0; i < mGridCoordSysAdapter.getCount(); i++) {
                try {
                    if (mGridCoordSysAdapter.getItem(i).getKey().equals(mMapTerrain.GetCoordinateSystem()))
                        mGridCoordSysLV.setItemChecked(i, true);
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }

        }

    }
}
