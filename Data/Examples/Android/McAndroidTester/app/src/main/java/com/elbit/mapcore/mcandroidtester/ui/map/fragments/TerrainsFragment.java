package com.elbit.mapcore.mcandroidtester.ui.map.fragments;

import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import androidx.annotation.Nullable;
import com.google.android.material.tabs.TabLayout;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.CheckedTextView;
import android.widget.ListView;
import android.widget.RadioButton;
import android.widget.RadioGroup;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_MCTerrain;
import com.elbit.mapcore.mcandroidtester.model.AMCTViewPort;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.TerrainActivity;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.ui.adapters.HashMapAdapter;

import java.util.ArrayList;
import java.util.List;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Map.IMcMapTerrain;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link TerrainsFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link TerrainsFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class TerrainsFragment extends Fragment implements FragmentWithObject {

    private ListView mTerrainsLV;
    private Button mAddNewTerrainBttn;
    private OnFragmentInteractionListener mListener;
    private IMcOverlayManager mOverlayManager;
    private ArrayList mLstTerrainText;
    private HashMapAdapter mTerrainHMA;
    private IMcMapViewport mMcMapViewport;
    private List<IMcMapTerrain> mSelectedTerrainsLst = new ArrayList<>();
    private boolean mIsAddNew = true;
    private IMcMapViewport mMapViewport;
    private boolean mIsOpenNewVP;
    public static final String SELECTED_TERRAINS_LIST = "SELECTED_TERRAINS_LIST";
    private boolean mIsVisibleToUser;
    private RadioButton mCreateNewTerrainRB;
    private RadioButton mUseExitingTerrainRB;
    public TerrainsFragment() {
        // Required empty public constructor
    }

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment TerrainsFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static TerrainsFragment newInstance(boolean isOpenNewVP) {
        TerrainsFragment fragment = new TerrainsFragment();
        fragment.mIsOpenNewVP = isOpenNewVP;
        return fragment;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View inflaterView = inflater.inflate(R.layout.fragment_terrains, container, false);

        Funcs.SetObjectFromBundle(savedInstanceState, this );
        if(savedInstanceState != null)
        {
            AMCTSerializableObject terrainList = (AMCTSerializableObject) savedInstanceState.getSerializable(SELECTED_TERRAINS_LIST);
            if(terrainList != null)
                mSelectedTerrainsLst = (List<IMcMapTerrain>)terrainList.getMcObject();
        }

        initTerrainsLV(inflaterView);
        initAddTerrainBttn(inflaterView);
        initFinishBttn(inflaterView);
        initRadioBttns(inflaterView);
        return inflaterView;
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
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
    }

    private void initFinishBttn(View inflaterView) {
        inflaterView.findViewById(R.id.finish_terrain_bttn).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        if (!mIsAddNew && mSelectedTerrainsLst.size() > 0) {
                            for (final IMcMapTerrain terrain : mSelectedTerrainsLst) {
                                try {
                                    if (mMapViewport != null) {
                                        mMapViewport.AddTerrain(terrain);
                                    } else if (mIsOpenNewVP) {
                                        AMCTViewPort.getViewportInCreation().addTerrainToList(terrain);
                                    }
                                } catch (MapCoreException e) {
                                    e.printStackTrace();
                                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "AddTerrain");
                                } catch (Exception e) {
                                    e.printStackTrace();
                                }
                            }
                        }
                        getActivity().runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                TabLayout tabLayout = ((TabLayout) getActivity().findViewById(R.id.tabs));
                                if (tabLayout != null)
                                    ((TabLayout) getActivity().findViewById(R.id.tabs)).getTabAt(Consts.OVERLAY_MANAGER_TAB_INDEX).select();
                                else
                                    getActivity().onBackPressed();
                            }
                        });
                    }
                });
            }
        });
    }

    private void initAddTerrainBttn(View inflaterView) {
        mAddNewTerrainBttn = (Button) inflaterView.findViewById(R.id.add_terrain_bttn);
        mAddNewTerrainBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(TerrainsFragment.this.getActivity(), TerrainActivity.class);
                if(mMapViewport != null)
                    intent.putExtra(Consts.TERRAIN_ACTIVITY_VIEWPORT_PARAM, mMapViewport.hashCode());
                if(mIsOpenNewVP)
                    intent.putExtra(Consts.OPEN_NEW_VIEWPORT_PARAM, true);
                startActivity(intent);
            }
        });
    }

    private void initTerrainsLV(View inflaterView) {
        //TODO multiple selection
        mTerrainsLV = (ListView) (inflaterView.findViewById(R.id.terrains_lv));
        mTerrainsLV.setChoiceMode(ListView.CHOICE_MODE_MULTIPLE);
        mTerrainsLV.setItemsCanFocus(false);
        mTerrainHMA = new HashMapAdapter(getActivity(), Manager_MCTerrain.getInstance().getTerrains()/*.mdTerrain*/, Consts.ListType.MULTIPLE_CHECK);
        mTerrainsLV.setAdapter(null);
        mTerrainsLV.setAdapter(mTerrainHMA);
        mTerrainsLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                if (!mIsAddNew) {
                    IMcMapTerrain terrain = (IMcMapTerrain) mTerrainHMA.getItem(position).getKey();
                    if (((CheckedTextView) view).isChecked()) {
                        mSelectedTerrainsLst.add(terrain);
                    } else
                        mSelectedTerrainsLst.remove(terrain);
                }
            }
        });
        selectCurTerrains();
    }

    private void selectCurTerrains() {
        if ((AMCTViewPort.getViewportInCreation() != null && AMCTViewPort.getViewportInCreation().getTerrainsAsArr().length > 0)
                || !mSelectedTerrainsLst.isEmpty()) {
            for (int i = 0; i < mTerrainHMA.getCount(); i++) {
                if (AMCTViewPort.getViewportInCreation().containsTerrain((IMcMapTerrain)mTerrainHMA.getItem(i).getKey())
                        || (mSelectedTerrainsLst.contains(mTerrainHMA.getItem(i).getKey())) ) {
                    mTerrainsLV.setItemChecked(i, true);
                    mUseExitingTerrainRB.setChecked(true);
                }
            }
        }
    }

    private void initRadioBttns(View view) {
        mCreateNewTerrainRB = (RadioButton) view.findViewById(R.id.create_new_terrain_rb);
        mUseExitingTerrainRB = (RadioButton) view.findViewById(R.id.use_existing_terrain_rb);
        mCreateNewTerrainRB.setChecked(true);
        useCreateNewTerrain();
        ((RadioGroup) view.findViewById(R.id.terrains_options_rg)).setOnCheckedChangeListener(new RadioGroup.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(RadioGroup group, int checkedId) {
                int id = checkedId;
                if (id == R.id.use_existing_terrain_rb) {
                    useExistingTerrain();
                }
                else if (id == R.id.create_new_terrain_rb) {
                    useCreateNewTerrain();
                }
            }
        });
    }

    private void isAddNew(boolean flag) {
        mTerrainsLV.setEnabled(!flag);
        mAddNewTerrainBttn.setEnabled(flag);
        mIsAddNew = flag;
    }

    private void useCreateNewTerrain() {
        isAddNew(true);
    }

    private void useExistingTerrain() {
        isAddNew(false);
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    @Override
    public void onResume() {
        super.onResume();
        if (mTerrainHMA != null && mTerrainsLV != null) {
            initTerrainsLV(getView());
        }
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
         if(mIsVisibleToUser) {
            outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mMapViewport));
            outState.putSerializable(SELECTED_TERRAINS_LIST, new AMCTSerializableObject(mSelectedTerrainsLst));
        }
    }

    @Override
    public void setObject(Object obj) {
        mMapViewport = (IMcMapViewport) obj;
    }

    @Override
    public void setUserVisibleHint(boolean isVisibleToUser) {
        super.setUserVisibleHint(isVisibleToUser);
        mIsVisibleToUser = isVisibleToUser;
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