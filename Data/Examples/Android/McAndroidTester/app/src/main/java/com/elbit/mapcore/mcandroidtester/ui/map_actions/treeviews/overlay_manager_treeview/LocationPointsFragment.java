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

import com.elbit.mapcore.General.ObjectRef;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectLocation;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeItem;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.managers.ObjectWorld.Manager_MCObjectSchemeItem;
import com.elbit.mapcore.mcandroidtester.managers.ObjectWorld.Manager_MCOverlayManager;
import com.elbit.mapcore.mcandroidtester.model.AMCTViewPort;
import com.elbit.mapcore.mcandroidtester.ui.adapters.HashMapAdapter;
import com.elbit.mapcore.mcandroidtester.ui.adapters.NewLocationPointsAdapter;
import com.elbit.mapcore.mcandroidtester.ui.adapters.OverlayAdapter;
import com.elbit.mapcore.mcandroidtester.ui.adapters.SchemeAdapter;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SampleLocationPointsBttn;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SectionSeparator;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SpinnerWithLabel;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.Map;

import com.elbit.mapcore.Enums.EMcPointCoordSystem;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Calculations.IMcSpatialQueries;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectScheme;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlay;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;
import com.elbit.mapcore.Structs.SMcVector3D;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link LocationPointsFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link LocationPointsFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class LocationPointsFragment extends Fragment {

    private OnFragmentInteractionListener mListener;
    private View mRootView;
    private ListView mOverlayManagerLV;
    private ListView mDestinationOverlayLV;
    private ListView mSchemesLV;
    private ListView mStandAloneItemsLV;
    private CheckBox mLocationRelativeToDTMCB;
    private SpinnerWithLabel mLocationCoordSysSWL;
    private ListView mItemLocationPointsLV;
    private Button mSaveChangesBttn;
    private HashMapAdapter mOverlayManagerAdapter;
    private HashMapAdapter mItemsAdapter;
    private SampleLocationPointsBttn mSamplePointLocationBttn;
    private SMcVector3D[] mArrayLocationPoints;
    private IMcObjectScheme mSelectedScheme;
    private IMcObjectSchemeItem mSelectedSchemeItem;
    private IMcOverlay mSelectedOverlay;
    private NewObjectOperation mNewObjectOperation;

    private SectionSeparator mStandaloneItemsSS;
    private SectionSeparator mSchemesSS;

    public enum NewObjectOperation{BasedOnExistingScheme, WithNewSchemeContainingOneLocation, WithNewSchemeContainingOneLocationAndOneItem}

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment LocationPointsFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static LocationPointsFragment newInstance(String param1, String param2) {
        LocationPointsFragment fragment = new LocationPointsFragment();
        return fragment;
    }

    public LocationPointsFragment() {
        // Required empty public constructor
    }

    public void SetNewObjectOperation(NewObjectOperation newObjectOperation)
    {
        mNewObjectOperation = newObjectOperation;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        mRootView = inflater.inflate(R.layout.fragment_location_points, container, false);
        inflateViews();
        initViews();
        return mRootView;
    }

    private void initViews() {
        initSampleLocationPoint();
        initOverlayManagerLV();
        initItemLocationPointsLV();
        initLocationCoordSys();
        initItemsLV();
        initSaveBttn();

        if(mNewObjectOperation == NewObjectOperation.BasedOnExistingScheme)
        {
            mStandAloneItemsLV.setVisibility(View.GONE);
            mStandaloneItemsSS.setVisibility(View.GONE);
            mLocationCoordSysSWL.setEnabled(false);
            mLocationRelativeToDTMCB.setEnabled(false);
        }
        else if(mNewObjectOperation == NewObjectOperation.WithNewSchemeContainingOneLocation)
        {
            mStandAloneItemsLV.setVisibility(View.GONE);
            mStandaloneItemsSS.setVisibility(View.GONE);
            mSchemesLV.setVisibility(View.GONE);
            mSchemesSS.setVisibility(View.GONE);
        }
        else
        {
            mSchemesLV.setVisibility(View.GONE);
            mSchemesSS.setVisibility(View.GONE);
        }
    }

    private void initSampleLocationPoint() {
        mSamplePointLocationBttn.initBttn(LocationPointsFragment.class.getSimpleName(),true,Double.MAX_VALUE, IMcSpatialQueries.EQueryPrecision.EQP_DEFAULT,EMcPointCoordSystem.EPCS_WORLD,false,-1,R.id.location_points_item_location_points_lv);
    }

    private void initSaveBttn() {
        mSaveChangesBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                mArrayLocationPoints = ((NewLocationPointsAdapter) mItemLocationPointsLV.getAdapter()).getCurRowsData();
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            if (mNewObjectOperation == NewObjectOperation.BasedOnExistingScheme)
                                IMcObject.Static.Create(mSelectedOverlay, mSelectedScheme, mArrayLocationPoints);
                            else if(mNewObjectOperation == NewObjectOperation.WithNewSchemeContainingOneLocation) {
                                ObjectRef<IMcObjectLocation> objectLocationObjectRef = new ObjectRef<>();
                                IMcObject.Static.Create(objectLocationObjectRef, mSelectedOverlay, (EMcPointCoordSystem) mLocationCoordSysSWL.getSelectedItem(), mArrayLocationPoints, mLocationRelativeToDTMCB.isChecked());
                            }
                            else if (mNewObjectOperation == NewObjectOperation.WithNewSchemeContainingOneLocationAndOneItem) {
                                IMcObject.Static.Create(mSelectedOverlay, mSelectedSchemeItem, (EMcPointCoordSystem) mLocationCoordSysSWL.getSelectedItem(), mArrayLocationPoints, mLocationRelativeToDTMCB.isChecked());
                                Manager_MCObjectSchemeItem.getInstance().RemoveItem(mSelectedSchemeItem);
                            }
                        } catch (MapCoreException mcEx) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "IMcObject.Static.Create");
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
        mLocationCoordSysSWL.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                EMcPointCoordSystem ptCoordSys = (EMcPointCoordSystem) mLocationCoordSysSWL.getSelectedItem();
                switch (ptCoordSys) {
                    case EPCS_IMAGE:
                        mSamplePointLocationBttn.setPointCoordSystem(EMcPointCoordSystem.EPCS_IMAGE);
                        break;
                    case EPCS_SCREEN:
                        mSamplePointLocationBttn.setPointCoordSystem(EMcPointCoordSystem.EPCS_SCREEN);
                        break;
                    case EPCS_WORLD:
                        mSamplePointLocationBttn.setPointCoordSystem(EMcPointCoordSystem.EPCS_WORLD);
                        break;
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });

    }

    private void initItemLocationPointsLV() {
        SMcVector3D[] locationPointsArr = new SMcVector3D[1];
        locationPointsArr[0] = new SMcVector3D();
        ArrayList<SMcVector3D> locationPointsArrList = new ArrayList<>(Arrays.asList(locationPointsArr));
        mItemLocationPointsLV.setAdapter(new NewLocationPointsAdapter(getContext(), R.layout.new_location_point_row, locationPointsArrList));
        Funcs.setListViewHeightBasedOnChildren(mItemLocationPointsLV);
    }

    private void initOverlayManagerLV() {
        mOverlayManagerLV = (ListView) (mRootView.findViewById(R.id.location_points_overlay_manager_lv));
        mOverlayManagerLV.setChoiceMode(ListView.CHOICE_MODE_SINGLE);
        mOverlayManagerAdapter = new HashMapAdapter(getActivity(), Manager_MCOverlayManager.getInstance().getAllParams(), Consts.ListType.SINGLE_CHECK);
        mOverlayManagerLV.setAdapter(null);
        mOverlayManagerLV.setAdapter(mOverlayManagerAdapter);
        Funcs.setListViewHeightBasedOnChildren(mOverlayManagerLV);
        mOverlayManagerLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                Map.Entry<Object, Integer> item = (Map.Entry<Object, Integer>) mOverlayManagerLV.getAdapter().getItem(position);
                IMcOverlayManager checkedOM = ((IMcOverlayManager) item.getKey());
                SelectedOverlayManager(checkedOM);
               
            }
        });
        selectCurOverlayManager();
    }

    private void initSchemesLV(IMcOverlayManager checkedOM) {
        IMcObjectScheme[] schemes = new IMcObjectScheme[0];
        try {
            schemes = checkedOM.GetObjectSchemes();
        } catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "GetObjectSchemes");
        } catch (Exception e1) {
            e1.printStackTrace();
        }
        if(schemes!=null&&schemes.length>0) {
            mSchemesLV.setAdapter(null);
            mSchemesLV.setAdapter(new SchemeAdapter(schemes));
            mSchemesLV.setChoiceMode(AbsListView.CHOICE_MODE_SINGLE);
            Funcs.setListViewHeightBasedOnChildren(mSchemesLV);
            mSchemesLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                @Override
                public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                    mSelectedScheme = (IMcObjectScheme) mSchemesLV.getAdapter().getItem(position);
                    initCoordSys(mSelectedScheme);
                    initRelativeToDTM(mSelectedScheme);
                }
            });
        }
    }

    private void initItemsLV() {
        HashMap<IMcObjectSchemeItem, Integer> map =  Manager_MCObjectSchemeItem.getInstance().getAllParams();
        if(map != null && map.size() > 0) {
            mStandAloneItemsLV.setAdapter(null);
            mItemsAdapter = new HashMapAdapter(getActivity(), Manager_MCObjectSchemeItem.getInstance().getAllObjectParams(), Consts.ListType.SINGLE_CHECK);
            mStandAloneItemsLV.setAdapter(mItemsAdapter);
            mStandAloneItemsLV.setChoiceMode(AbsListView.CHOICE_MODE_SINGLE);
            Funcs.setListViewHeightBasedOnChildren(mStandAloneItemsLV);
            mStandAloneItemsLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                @Override
                public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                    Map.Entry<Object, Integer> item = (Map.Entry<Object, Integer>) mStandAloneItemsLV.getAdapter().getItem(position);
                    mSelectedSchemeItem = ((IMcObjectSchemeItem) item.getKey());
                }
            });
        }
    }

    private void initRelativeToDTM(IMcObjectScheme selectedScheme) {
        ObjectRef<Boolean> relativeToDTM = new ObjectRef<>();
        ObjectRef<Long> propId = new ObjectRef<>();; // TODO: properly support RelativeToDTM as PrivateProperty!
        try {
            selectedScheme.GetObjectLocation(0).GetRelativeToDTM(relativeToDTM, propId);
            mLocationRelativeToDTMCB.setChecked(relativeToDTM.getValue());
        }
        catch(MapCoreException mcEx)
        {
            AlertMessages.ShowMapCoreErrorMessage(getContext(),mcEx,"GetObjectLocation/GetCoordSystem");
        }catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initCoordSys(IMcObjectScheme selectedScheme) {
        EMcPointCoordSystem coordSys = null;
        try {
            coordSys=selectedScheme.GetObjectLocation(0).GetCoordSystem();
            mLocationCoordSysSWL.setSelection(coordSys.getValue());
            ObjectRef<Boolean> relativeToDTM = new ObjectRef<>();
            ObjectRef<Long> propId = new ObjectRef<>();
            selectedScheme.GetObjectLocation(0).GetRelativeToDTM(relativeToDTM,propId);
            mLocationRelativeToDTMCB.setChecked(relativeToDTM.getValue());
            
        } catch(MapCoreException mcEx)
        {
            AlertMessages.ShowMapCoreErrorMessage(getContext(),mcEx,"GetObjectLocation/GetCoordSystem");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }


    private void initOverlaysLV(IMcOverlayManager checkedOM) {
        try {
            IMcOverlay[] overlays = checkedOM.GetOverlays();
            mDestinationOverlayLV.setAdapter(null);
            mDestinationOverlayLV.setAdapter(new OverlayAdapter(overlays));
            mDestinationOverlayLV.setChoiceMode(AbsListView.CHOICE_MODE_SINGLE);
            Funcs.setListViewHeightBasedOnChildren(mDestinationOverlayLV);
            mDestinationOverlayLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                @Override
                public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                    mSelectedOverlay = (IMcOverlay) mDestinationOverlayLV.getAdapter().getItem(position);
                }

            });

        } catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "GetOverlays");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void selectCurOverlayManager() {
        IMcOverlayManager currOverlayManager = AMCTViewPort.getViewportInCreation().getmOverlayManager();
        if (currOverlayManager != null) {
            int i;
            for (i = 0; i < mOverlayManagerAdapter.getCount(); i++) {
                if (mOverlayManagerAdapter.getItem(i).getKey().equals(currOverlayManager)) {
                    mOverlayManagerLV.setItemChecked(i, true);
                    SelectedOverlayManager(currOverlayManager);
                }
            }
        }
    }

    private void SelectedOverlayManager(IMcOverlayManager selectedOverlayManager)
    {
        initOverlaysLV(selectedOverlayManager);
        initSchemesLV(selectedOverlayManager);
    }

    private void inflateViews() {
        mOverlayManagerLV = (ListView) mRootView.findViewById(R.id.location_points_overlay_manager_lv);
        mDestinationOverlayLV = (ListView) mRootView.findViewById(R.id.location_points_destination_overlay_lv);
        mSchemesLV = (ListView) mRootView.findViewById(R.id.location_points_schemas_lv);
        mStandAloneItemsLV = (ListView) mRootView.findViewById(R.id.location_points_standalone_items_lv);
        mLocationRelativeToDTMCB = (CheckBox) mRootView.findViewById(R.id.location_points_location_relative_to_dtm_cb);
        mLocationCoordSysSWL = (SpinnerWithLabel) mRootView.findViewById(R.id.location_points_location_coordinate_system);
        mItemLocationPointsLV = (ListView) mRootView.findViewById(R.id.location_points_item_location_points_lv);
        mSaveChangesBttn = (Button) mRootView.findViewById(R.id.location_points_save_changes_bttn);
        mSamplePointLocationBttn = (SampleLocationPointsBttn) mRootView.findViewById(R.id.location_points_sample_location_point_bttn);
        mStandaloneItemsSS = (SectionSeparator) mRootView.findViewById(R.id.location_points_standalone_items_ss);
        mSchemesSS = (SectionSeparator) mRootView.findViewById(R.id.location_points_schemas_ss);

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
            ((MapsContainerActivity) context).mCurFragmentTag = LocationPointsFragment.class.getSimpleName();
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
