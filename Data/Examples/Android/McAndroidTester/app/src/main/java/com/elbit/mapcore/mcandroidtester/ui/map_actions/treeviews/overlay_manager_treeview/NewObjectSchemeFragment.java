package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlay_manager_treeview;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AbsListView;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.ListView;

import com.elbit.mapcore.Enums.EMcPointCoordSystem;
import com.elbit.mapcore.General.CMcEnumBitField;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.General.ObjectRef;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectLocation;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectScheme;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.managers.ObjectWorld.Manager_MCObjectSchemeItem;
import com.elbit.mapcore.mcandroidtester.ui.adapters.HashMapAdapter;
import com.elbit.mapcore.mcandroidtester.ui.adapters.TerrainObjectsConsiderationFlagsAdapter;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SectionSeparator;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SpinnerWithLabel;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.Map;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link NewObjectSchemeFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link NewObjectSchemeFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class NewObjectSchemeFragment extends Fragment {

    private OnFragmentInteractionListener mListener;
    private View mRootView;
    private IMcOverlayManager mOverlayManager;
    private CheckBox mLocationRelativeToDTMCB;
    private SpinnerWithLabel mLocationCoordSysSWL;
    private Button mSaveChangesBttn;
    private ListView mStandaloneItemsLV;
    private HashMapAdapter mItemsAdapter;
    private ListView mTerrainFlagsLV;
    private TerrainObjectsConsiderationFlagsAdapter mTerrainFlagsAdapter;
    private IMcObjectSchemeItem mSelectedSchemeItem;
    private NewObjectSchemeOperation mNewObjectSchemeOperation;
    private SectionSeparator mStandaloneItemsSS;
    private SectionSeparator mTerrainFlagsSS;
    public enum NewObjectSchemeOperation{SchemeWithOneLocation, SchemeWithOneLocationAndOneItem}

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment LocationPointsFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static NewObjectSchemeFragment newInstance() {
        NewObjectSchemeFragment fragment = new NewObjectSchemeFragment();
        return fragment;
    }

    public NewObjectSchemeFragment() {
        // Required empty public constructor
    }

    public void SetNewObjectSchemeParams(IMcOverlayManager overlayManager, NewObjectSchemeOperation newObjectOperation)
    {
        mOverlayManager = overlayManager;
        mNewObjectSchemeOperation = newObjectOperation;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        mRootView = inflater.inflate(R.layout.fragment_new_object_scheme, container, false);
        inflateViews();
        initViews();
        return mRootView;
    }

    private void initViews() {
        initLocationCoordSys();
        initTerrainFlagsLV();
        initItemsLV();
        initSaveBttn();

    }

    private void initSaveBttn() {
        mSaveChangesBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                CMcEnumBitField<IMcObjectScheme.ETerrainObjectsConsiderationFlags> terrainFlags = new CMcEnumBitField<>();
                for (int i = 0; i < mTerrainFlagsAdapter.getCount(); i++) {
                    if (mTerrainFlagsLV.isItemChecked(i))
                        terrainFlags.Set(mTerrainFlagsAdapter.getItem(i));
                }
                final CMcEnumBitField<IMcObjectScheme.ETerrainObjectsConsiderationFlags> finalTerrainFlags = terrainFlags;
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            if (mNewObjectSchemeOperation == NewObjectSchemeOperation.SchemeWithOneLocation) {
                                ObjectRef<IMcObjectLocation> objectLocationObjectRef = new ObjectRef<>();
                                IMcObjectScheme.Static.Create(objectLocationObjectRef, mOverlayManager, (EMcPointCoordSystem) mLocationCoordSysSWL.getSelectedItem(), mLocationRelativeToDTMCB.isChecked(), finalTerrainFlags);
                            } else {
                                IMcObjectScheme.Static.Create(mOverlayManager,mSelectedSchemeItem, (EMcPointCoordSystem) mLocationCoordSysSWL.getSelectedItem(), mLocationRelativeToDTMCB.isChecked());
                            }
                        } catch (MapCoreException mcEx) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "IMcObjectScheme.Static.Create");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });
    }

    private void initLocationCoordSys() {
        mLocationCoordSysSWL.setAdapter(new ArrayAdapter<>(getContext(), android.R.layout.simple_spinner_item, EMcPointCoordSystem.values()));
        mLocationCoordSysSWL.setSelection(EMcPointCoordSystem.EPCS_WORLD.getValue());
    }

    private void initTerrainFlagsLV()
    {
       // if(mNewObjectSchemeOperation == NewObjectSchemeOperation.SchemeWithOneLocation) {
            IMcObjectScheme.ETerrainObjectsConsiderationFlags[] allTerrainFlags = IMcObjectScheme.ETerrainObjectsConsiderationFlags.values();
            ArrayList<IMcObjectScheme.ETerrainObjectsConsiderationFlags> allTerrainFlagsList = new ArrayList<>(Arrays.asList(allTerrainFlags));
            allTerrainFlagsList.remove(IMcObjectScheme.ETerrainObjectsConsiderationFlags.ETOCF_NONE);
            mTerrainFlagsAdapter = new TerrainObjectsConsiderationFlagsAdapter(getContext(), R.layout.checkable_list_item, allTerrainFlagsList);
            mTerrainFlagsLV.setAdapter(mTerrainFlagsAdapter);
            Funcs.setListViewHeightBasedOnChildren(mTerrainFlagsLV);
            mTerrainFlagsLV.setChoiceMode(AbsListView.CHOICE_MODE_MULTIPLE);
            mTerrainFlagsLV.setItemChecked(0, true);
      /*  }
        else
        {
            mTerrainFlagsLV.setVisibility(View.GONE);
            mTerrainFlagsSS.setVisibility(View.GONE);
        }*/
    }

    private void initItemsLV() {
        if(mNewObjectSchemeOperation == NewObjectSchemeOperation.SchemeWithOneLocation) {
            mStandaloneItemsLV.setVisibility(View.GONE);
            mStandaloneItemsSS.setVisibility(View.GONE);
        }
        else {
            HashMap<IMcObjectSchemeItem, Integer> map = Manager_MCObjectSchemeItem.getInstance().getAllParams();

            if (map != null && map.size() > 0) {
                mStandaloneItemsLV.setAdapter(null);
                mItemsAdapter = new HashMapAdapter(getActivity(), Manager_MCObjectSchemeItem.getInstance().getAllObjectParams(), Consts.ListType.SINGLE_CHECK);
                mStandaloneItemsLV.setAdapter(mItemsAdapter);
                mStandaloneItemsLV.setChoiceMode(AbsListView.CHOICE_MODE_SINGLE);
                Funcs.setListViewHeightBasedOnChildren(mStandaloneItemsLV);
                mStandaloneItemsLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                    @Override
                    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                        Map.Entry<Object, Integer> item = (Map.Entry<Object, Integer>) mStandaloneItemsLV.getAdapter().getItem(position);
                        mSelectedSchemeItem = ((IMcObjectSchemeItem) item.getKey());
                    }
                });
            }
        }
    }

    private void inflateViews() {
        mStandaloneItemsLV = (ListView) mRootView.findViewById(R.id.new_object_scheme_standalone_items_lv);
        mLocationRelativeToDTMCB = (CheckBox) mRootView.findViewById(R.id.new_object_scheme_location_relative_to_dtm_cb);
        mLocationCoordSysSWL = (SpinnerWithLabel) mRootView.findViewById(R.id.new_object_scheme_location_coordinate_system);
        mTerrainFlagsLV = (ListView) mRootView.findViewById(R.id.new_object_scheme_terrain_objects_consideration_flags_lv);
        mSaveChangesBttn = (Button) mRootView.findViewById(R.id.new_object_scheme_save_changes_bttn);
        mStandaloneItemsSS = (SectionSeparator) mRootView.findViewById(R.id.new_object_scheme_standalone_items_ss);
        mTerrainFlagsSS = (SectionSeparator) mRootView.findViewById(R.id.new_object_scheme_terrain_objects_consideration_flags_ss);
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
        if (context instanceof MapsContainerActivity)
            ((MapsContainerActivity) context).mCurFragmentTag = NewObjectSchemeFragment.class.getSimpleName();
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
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
