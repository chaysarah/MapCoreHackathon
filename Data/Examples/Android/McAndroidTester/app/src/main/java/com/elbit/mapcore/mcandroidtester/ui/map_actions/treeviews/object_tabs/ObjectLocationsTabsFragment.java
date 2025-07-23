package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.object_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.ListView;

import com.elbit.mapcore.Structs.SMcFVector2D;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.interfaces.OnSelectViewportFromListListener;
import com.elbit.mapcore.mcandroidtester.ui.adapters.LocationPointsAdapter;
import com.elbit.mapcore.mcandroidtester.ui.adapters.NewLocationPointsAdapter;
import com.elbit.mapcore.mcandroidtester.ui.adapters.ObjSchemeNodeAdapter;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.MapFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.private_properties.PropertiesIdListFragment;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SampleLocationPointsBttn;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SpinnerWithLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.ThreeDVector;
import com.elbit.mapcore.mcandroidtester.utils.customviews.TwoDFVector;
import com.elbit.mapcore.mcandroidtester.utils.customviews.ViewportsList;

import java.util.ArrayList;
import java.util.Arrays;

import com.elbit.mapcore.Enums.EMcPointCoordSystem;
import com.elbit.mapcore.General.CMcEnumBitField;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Calculations.IMcSpatialQueries;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectScheme;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeNode;
import com.elbit.mapcore.Structs.SMcVector3D;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link ObjectLocationsTabsFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link ObjectLocationsTabsFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class ObjectLocationsTabsFragment extends Fragment implements FragmentWithObject, OnSelectViewportFromListListener {
    private static final int INDEX_NUM_DEF_VAL = -1;

    private OnFragmentInteractionListener mListener;
    private IMcObject mObject;
    private View mRootView;
    private ListView mLocationPointsLV;
    private NumericEditTextLabel mIndexNETL;
    private Button mRemoveLocationPointBttn;
    private NumericEditTextLabel mInsertedPositionNETL;
    private Button mAddLocationPointBttn;
    private NumericEditTextLabel mLocIndexNETL;
    private Button mUpdateLocationPointBttn;
    private ThreeDVector m3DLocationPoint;
    private Button mMoveAllpointsBttn;
    private ListView mNewLocationPointsLV;
    private NumericEditTextLabel mStartIndexNETL;
    private Button mUpdateLocationPointsBttn;
    private NumericEditTextLabel mLocationPointsIndexNumNET;
    private Button mGetLocationPointsBttn;
    private CheckBox mIsRelativeToDtmCb;
    private Button mSetLocationPointsBttn;
    private Button mClearNewLocationPointLVBttn;
    private SampleLocationPointsBttn mSampleLocationPointsBttn;
    private Button mPropertiesBttn;
    private Button mGetLocationIndexByIdBttn;
    private NumericEditTextLabel mLocationIndexNET;
    private NumericEditTextLabel mNodeIdNET;
    private NumericEditTextLabel mNumLocationPointsIndexNET;
    private NumericEditTextLabel mNumLocationPointsNET;
    private Button mSetNumLocationPointsBttn;
    private Button mGetScreenArrangementOffsetBttn;
    private ViewportsList mScreenArrangementOffsetViewportsLV;
    private TwoDFVector mScreenArrangementOffset2DFVector;
    private Button mSetScreenArrangementOffsetBttn;
    private ListView mObjSchemeNodeLV;
    private SpinnerWithLabel mPointCoordSysSWL;
    private ListView mNodeCalcCoordinatesLV;
    private Button mCalcCoordBttn;
    private IMcMapViewport mSelectedViewport;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment ObjectLocationsTabsFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static ObjectLocationsTabsFragment newInstance() {
        ObjectLocationsTabsFragment fragment = new ObjectLocationsTabsFragment();
        return fragment;
    }

    public ObjectLocationsTabsFragment() {
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
        mRootView = inflater.inflate(R.layout.fragment_object_locations_tabs, container, false);
        inflateViews();
        initViews();
        return mRootView;
    }

    private void initViews() {
        mIndexNETL.setInt(INDEX_NUM_DEF_VAL);
        mSampleLocationPointsBttn.initBttn(ObjectTabsFragment.class.getSimpleName(), true,Double.MAX_VALUE, IMcSpatialQueries.EQueryPrecision.EQP_DEFAULT, EMcPointCoordSystem.EPCS_WORLD,false ,-1, R.id.object_locations_new_location_points_lv);

        initLocationPointsLV();
        initNewLocationPointsLV();
        initScreenArrangementOffsetViewportsLV();
        initRemoveBttn();
        initAddBttn();
        initUpdateLocationPointBttn();
        initUpdateLocationPointsBttn();
        initMoveAllPointsBttn();
        initGetLocationPointsBttn();
        initSetLocationPointsBttn();
        initClearNewLocationPointsBttn();
        initPrivatePropertiesIdList();
        initGetLocationIndexByIdBttn();
        initSetNumLocationPointsBttn();
        initGetScreenArrangementOffsetBttn();
        initSetScreenArrangementOffsetBttn();
        initCalculatedCoordinates();
    }

    private void initCalculatedCoordinates() {
        initPointCoordSysSWL();
        initObjSchemeNodeLV();
    }

    private void initObjSchemeNodeLV() {
        IMcObjectSchemeNode[] objectSchemeNode;

        try {
            IMcObjectScheme objectScheme = mObject.GetScheme();
            CMcEnumBitField<IMcObjectSchemeNode.ENodeKindFlags> eNodeKindFlag = new CMcEnumBitField<>(IMcObjectSchemeNode.ENodeKindFlags.ENKF_ANY_NODE);
            objectSchemeNode = objectScheme.GetNodes(eNodeKindFlag);
            ArrayList<IMcObjectSchemeNode> objectSchemeNodeArr = new ArrayList<>(Arrays.asList(objectSchemeNode));
            mObjSchemeNodeLV.setAdapter(new ObjSchemeNodeAdapter(getContext(), R.layout.radio_bttn_list_item, objectSchemeNodeArr));
            Funcs.setListViewHeightBasedOnChildren(mObjSchemeNodeLV);
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetScheme/GetNodes");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initPointCoordSysSWL() {
        mPointCoordSysSWL.setAdapter(new ArrayAdapter<>(getContext(), android.R.layout.simple_spinner_item, EMcPointCoordSystem.values()));
        mPointCoordSysSWL.setSelection(EMcPointCoordSystem.EPCS_WORLD.getValue());
    }

    private void initScreenArrangementOffsetViewportsLV() {
        try {
            mScreenArrangementOffsetViewportsLV.initViewportsList(this,
                    Consts.ListType.SINGLE_CHECK,
                    ListView.CHOICE_MODE_SINGLE,
                    mObject.GetOverlayManager(), true);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initSetScreenArrangementOffsetBttn() {
        mSetScreenArrangementOffsetBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final SMcFVector2D vector = mScreenArrangementOffset2DFVector.getVector2D();
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            mObject.SetScreenArrangementOffset(mSelectedViewport, vector);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetScreenArrangementOffset");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }});
            }
        });
    }

    private void initGetScreenArrangementOffsetBttn() {
        mGetScreenArrangementOffsetBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (mSelectedViewport != null) {
                    try {
                        mScreenArrangementOffset2DFVector.setVector2D(mObject.GetScreenArrangementOffset(mSelectedViewport));
                    } catch (MapCoreException e) {
                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetScreenArrangementOffset");
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            }
        });
    }

    private void initSetNumLocationPointsBttn() {
        mSetNumLocationPointsBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final int numLocationPoints = mNumLocationPointsNET.getUInt();
                final int numLocationPointsIndex = mNumLocationPointsIndexNET.getUInt();
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            mObject.SetNumLocationPoints(numLocationPoints, numLocationPointsIndex);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetNumLocationPoints");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });
    }

    private void initGetLocationIndexByIdBttn() {
        mGetLocationIndexByIdBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                try {
                    mLocationIndexNET.setUInt(mObject.GetLocationIndexByID(mNodeIdNET.getUInt()));
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetLocationIndexByID");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void initPrivatePropertiesIdList() {
        mPropertiesBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                FragmentWithObject propertiesIdListFragment = PropertiesIdListFragment.newInstance();
                propertiesIdListFragment.setObject(mObject);
                FragmentTransaction transaction = getFragmentManager().beginTransaction();
                //  transaction.hide(ObjectGeneralTabFragment.this);
                transaction.add(R.id.object_locations_properties_fragment_container, (Fragment) propertiesIdListFragment, PropertiesIdListFragment.class.getSimpleName());
                transaction.addToBackStack(PropertiesIdListFragment.class.getSimpleName());
                transaction.commit();
            }
        });
    }


    private void goToMapFragment() {
        String backStateName = MapFragment.class.getSimpleName();
        FragmentManager manager = getActivity().getSupportFragmentManager();
        boolean fragmentPopped = manager.popBackStackImmediate(backStateName, 0);
        int i = 0;
    }

    private void initClearNewLocationPointsBttn() {
        mClearNewLocationPointLVBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                ((ArrayAdapter) mNewLocationPointsLV.getAdapter()).clear();
                ((ArrayAdapter) mNewLocationPointsLV.getAdapter()).add(new SMcVector3D());
                Funcs.setListViewHeightBasedOnChildren(mNewLocationPointsLV);
            }
        });
    }

    private void initSetLocationPointsBttn() {
        mSetLocationPointsBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final SMcVector3D[] newLocationPoints = getCurRowsData();
                final int locationPointsIndex = mLocationPointsIndexNumNET.getUInt();
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            mObject.SetLocationPoints(newLocationPoints, locationPointsIndex);
                            invalidateLocationPoints();
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "UpdateLocationPoints");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });
    }

    private void initGetLocationPointsBttn() {
        mGetLocationPointsBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                invalidateLocationPoints();
            }
        });
    }

    private void initUpdateLocationPointsBttn() {
        mUpdateLocationPointsBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final SMcVector3D[] newLocationPoints = getCurRowsData();
                final int startIndex =  mStartIndexNETL.getUInt();
                final int locationPointsIndex = mLocationPointsIndexNumNET.getUInt();

                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            mObject.UpdateLocationPoints(newLocationPoints,startIndex, locationPointsIndex);
                            invalidateLocationPoints();
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "UpdateLocationPoints");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }});
            }
        });
    }

    public SMcVector3D[] getCurRowsData() {
        NewLocationPointsAdapter adapter= (NewLocationPointsAdapter) mNewLocationPointsLV.getAdapter();
        SMcVector3D[] curRowsValues;
        double x,y,z;
        SMcVector3D lastItem = adapter.getItem(adapter.getCount() - 1);
        if ((lastItem.x + lastItem.y + lastItem.z) > 0||adapter.getCount()==1)
            curRowsValues = new SMcVector3D[mNewLocationPointsLV.getCount()];
        else
            curRowsValues = new SMcVector3D[mNewLocationPointsLV.getCount() - 1];
        for (int i = 0; i < curRowsValues.length; i++) {
            x=adapter.getItem(i).x;
            y =adapter.getItem(i).y;
            z =adapter.getItem(i).z;
           /* View curRow = mNewLocationPointsLV.getChildAt(i);
            double x = Double.valueOf(String.valueOf(((NumericEditText) curRow.findViewById(R.id.new_location_point_row_x)).getText()));
            double y = Double.valueOf(String.valueOf(((NumericEditText) curRow.findViewById(R.id.new_location_point_row_y)).getText()));
            double z = Double.valueOf(String.valueOf(((NumericEditText) curRow.findViewById(R.id.new_location_point_row_z)).getText()));*/
            curRowsValues[i] = new SMcVector3D(x, y, z);
        }
        return curRowsValues;
    }

    private void initUpdateLocationPointBttn() {
        mUpdateLocationPointBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final int index = mIndexNETL.getUInt();
                final SMcVector3D locationPoint = m3DLocationPoint.getVector3D();
                final int locationIndex = mLocIndexNETL.getUInt();
                if(index >= 0) {
                    Funcs.runMapCoreFunc(new Runnable() {
                        @Override
                        public void run() {
                            try {
                                mObject.UpdateLocationPoint(index, locationPoint, locationIndex);
                                invalidateLocationPoints();
                            } catch (MapCoreException e) {
                                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "UpdateLocationPoint");
                            } catch (Exception e) {
                                e.printStackTrace();
                            }
                        }
                    });
                }
            }
        });
    }

    private void initAddBttn() {
        mAddLocationPointBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final int index = mIndexNETL.getUInt();
                final SMcVector3D locationPoint = m3DLocationPoint.getVector3D();
                final int locationIndex = mLocIndexNETL.getUInt();

                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            final int insertedPos = mObject.AddLocationPoint(index, locationPoint, locationIndex);
                            getActivity().runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    mInsertedPositionNETL.setUInt(insertedPos);
                                    invalidateLocationPoints();
                                }
                            });
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "AddLocationPoint");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });
    }

    private void invalidateLocationPoints() {
        getActivity().runOnUiThread(new Runnable() {
            @Override
            public void run() {
                try {
                    SMcVector3D[] locationPointsArr = mObject.GetLocationPoints(mLocationPointsIndexNumNET.getUInt());
                    getRelativeToDTM();
                    ArrayList<SMcVector3D> locationPointsArrList = new ArrayList<>(Arrays.asList(locationPointsArr));
                    mLocationPointsLV.setAdapter(null);
                    mLocationPointsLV.setAdapter(new LocationPointsAdapter(getContext(), R.layout.location_point_row, locationPointsArrList));
                    Funcs.setListViewHeightBasedOnChildren(mLocationPointsLV);
                    mLocationPointsLV.invalidateViews();
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetLocationPoints");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });

    }

    private void getRelativeToDTM() {
        //TODO add object location to api
    }

    private void initRemoveBttn() {
        mRemoveLocationPointBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final int index = mIndexNETL.getUInt();
                final int location = mLocIndexNETL.getUInt();
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            mObject.RemoveLocationPoint(index, location);
                            invalidateLocationPoints();
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "RemoveLocationPoint");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });
    }

    public void initMoveAllPointsBttn() {
        mMoveAllpointsBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final SMcVector3D locationPoint = m3DLocationPoint.getVector3D();
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            mObject.MoveAllLocationsPoints(locationPoint);
                            invalidateLocationPoints();
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "MoveAllLocationsPoints");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });

            }
        });
    }

    private void initNewLocationPointsLV() {
        try {
            SMcVector3D[] locationPointsArr = new SMcVector3D[1];
            locationPointsArr[0] = new SMcVector3D();
            ArrayList<SMcVector3D> locationPointsArrList = new ArrayList<>(Arrays.asList(locationPointsArr));
            mNewLocationPointsLV.setAdapter(new NewLocationPointsAdapter(getContext(), R.layout.new_location_point_row, locationPointsArrList));
            Funcs.setListViewHeightBasedOnChildren(mNewLocationPointsLV);

        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initLocationPointsLV() {
        try {
            SMcVector3D[] locationPointsArr = mObject.GetLocationPoints(mLocationPointsIndexNumNET.getUInt());
            ArrayList<SMcVector3D> locationPointsArrList = new ArrayList<>(Arrays.asList(locationPointsArr));
            mLocationPointsLV.setAdapter(new LocationPointsAdapter(getContext(), R.layout.location_point_row, locationPointsArrList));
            Funcs.setListViewHeightBasedOnChildren(mLocationPointsLV);
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetLocationPoints");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void inflateViews() {
        mLocationPointsLV = (ListView) mRootView.findViewById(R.id.object_locations_points_lv);
        mIndexNETL = (NumericEditTextLabel) mRootView.findViewById(R.id.object_locations_index_net);
        mRemoveLocationPointBttn = (Button) mRootView.findViewById(R.id.object_locations_remove_location_point_bttn);
        mInsertedPositionNETL = (NumericEditTextLabel) mRootView.findViewById(R.id.object_locations_inserted_position_net);
        mAddLocationPointBttn = (Button) mRootView.findViewById(R.id.object_locations_add_location_point_bttn);
        mLocIndexNETL = (NumericEditTextLabel) mRootView.findViewById(R.id.object_locations_loc_index_net);
        mUpdateLocationPointBttn = (Button) mRootView.findViewById(R.id.object_locations_update_location_point_bttn);
        m3DLocationPoint = (ThreeDVector) mRootView.findViewById(R.id.object_locations_3d_location_point);
        mMoveAllpointsBttn = (Button) mRootView.findViewById(R.id.object_locations_move_all_points_bttn);
        mNewLocationPointsLV = (ListView) mRootView.findViewById(R.id.object_locations_new_location_points_lv);
        mStartIndexNETL = (NumericEditTextLabel) mRootView.findViewById(R.id.object_locations_start_index_net);
        mUpdateLocationPointsBttn = (Button) mRootView.findViewById(R.id.object_locations_update_location_points_bttn);
        mLocationPointsIndexNumNET = (NumericEditTextLabel) mRootView.findViewById(R.id.object_locations_location_points_index_net);
        mGetLocationPointsBttn = (Button) mRootView.findViewById(R.id.object_locations_get_location_points_bttn);
        mIsRelativeToDtmCb = (CheckBox) mRootView.findViewById(R.id.object_locations_is_relative_to_dtm_cb);
        mSetLocationPointsBttn = (Button) mRootView.findViewById(R.id.object_locations_set_location_points_bttn);
        mClearNewLocationPointLVBttn = (Button) mRootView.findViewById(R.id.object_locations_clear_new_location_points_lv_bttn);
        mSampleLocationPointsBttn = (SampleLocationPointsBttn) mRootView.findViewById(R.id.object_locations_sample_location_points_custom_bttn);
        mPropertiesBttn = (Button) mRootView.findViewById(R.id.object_locations_object_properties_id_list_bttn);
        mGetLocationIndexByIdBttn = (Button) mRootView.findViewById(R.id.object_locations_get_location_index_by_id_bttn);
        mLocationIndexNET = (NumericEditTextLabel) mRootView.findViewById(R.id.object_locations_location_index_net);
        mNodeIdNET = (NumericEditTextLabel) mRootView.findViewById(R.id.object_locations_node_id_net);
        mSetNumLocationPointsBttn = (Button) mRootView.findViewById(R.id.object_locations_set_num_location_points_ok_bttn);
        mNumLocationPointsNET = (NumericEditTextLabel) mRootView.findViewById(R.id.object_locations_num_location_points_net);
        mNumLocationPointsIndexNET = (NumericEditTextLabel) mRootView.findViewById(R.id.object_locations_num_location_points_index_net);
        mGetScreenArrangementOffsetBttn = (Button) mRootView.findViewById(R.id.object_locations_get_screen_arrangement_offset_bttn);
        mSetScreenArrangementOffsetBttn = (Button) mRootView.findViewById(R.id.object_locations_set_screen_arrangement_offset_bttn);
        mScreenArrangementOffsetViewportsLV = (ViewportsList) mRootView.findViewById(R.id.object_locations_screen_arrangement_offset_viewports_lv);
        mScreenArrangementOffset2DFVector = (TwoDFVector) mRootView.findViewById(R.id.object_locations_screen_arrangement_offset_2dfvector);
        mObjSchemeNodeLV = (ListView) mRootView.findViewById(R.id.obj_scheme_node_lv);
        mPointCoordSysSWL = (SpinnerWithLabel) mRootView.findViewById(R.id.point_coord_sys_swl);
        mNodeCalcCoordinatesLV = (ListView) mRootView.findViewById(R.id.node_calc_coordinates_lv);
        mCalcCoordBttn = (Button) mRootView.findViewById(R.id.object_locations_calc_coord_ok_bttn);
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
            ((MapsContainerActivity) context).mCurFragmentTag = ObjectLocationsTabsFragment.class.getSimpleName();
        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    @Override
    public void setObject(Object obj) {
        mObject = (IMcObject) obj;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mObject));
    }
    @Override
    public void onSelectViewportFromList(IMcMapViewport mcSelectedViewport, boolean isChecked) {
        if(isChecked)
            mSelectedViewport = mcSelectedViewport;
        else
            mSelectedViewport = null;
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
