package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlay_manager_treeview;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentTransaction;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ListView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_AMCTMapForm;
import com.elbit.mapcore.mcandroidtester.managers.ObjectWorld.Manager_MCConditionalSelectorObjects;
import com.elbit.mapcore.mcandroidtester.model.DrawObjectResult;
import com.elbit.mapcore.mcandroidtester.ui.adapters.NewLocationPointsAdapter;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.MapFragment;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Observable;
import java.util.Observer;

import com.elbit.mapcore.General.CMcEnumBitField;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.General.IMcEditMode;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcLocationConditionalSelector;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectScheme;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeNode;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;
import com.elbit.mapcore.Structs.SMcVector3D;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link LocationConditionalSelectorFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link LocationConditionalSelectorFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class LocationConditionalSelectorFragment extends BaseConditionalSelectorFragment implements FragmentWithObject, IMcEditMode.ICallback, Observer {

    private OnFragmentInteractionListener mListener;
    private IMcLocationConditionalSelector mCurrentCondSelector;
    private IMcOverlayManager mOverlayManager;
    private IMcEditMode mEditMode;
    private Button mDrawPolygonBttn;
    private ListView mPolygonPointsLV;
    private IMcObjectSchemeItem mObjSchemeItem;
    private IMcObject mObject;
    DrawObjectResult mDrawPolygonResult;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment LocationConditionalSelectorFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static LocationConditionalSelectorFragment newInstance(String param1, String param2) {
        LocationConditionalSelectorFragment fragment = new LocationConditionalSelectorFragment();
        return fragment;
    }

    public LocationConditionalSelectorFragment() {
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
        mRootview = inflater.inflate(R.layout.fragment_location_conditional_selector, container, false);
        inflateViews();
        initViews();
        initOverlayManager();
        return mRootview;
    }

    private void initOverlayManager() {
        try {
            mOverlayManager = mCurrentCondSelector.GetOverlayManager();
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetOverlayManager");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    @Override
    protected void inflateViews() {
        super.inflateViews();
        mDrawPolygonBttn = (Button) mRootview.findViewById(R.id.location_conditional_selector_draw_polygon_bttn);
        mPolygonPointsLV = (ListView) mRootview.findViewById(R.id.location_conditional_selector_polygon_points);
    }

    @Override
    protected void initViews() {
        super.initViews();
        if (Manager_AMCTMapForm.getInstance().getCurViewport() != null)
            mEditMode = Manager_AMCTMapForm.getInstance().getCurMapForm().getEditMode();
        initPolygonPointsLV();
        initDrawPolygonBttn();

    }

    private void initDrawPolygonBttn() {
        mDrawPolygonBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                try {
                    if( Manager_AMCTMapForm.getInstance().getCurMapForm()!= null) {
                        Manager_AMCTMapForm.getInstance().getCurMapForm().getEditModeManagerCallback().SetEventsCallback(LocationConditionalSelectorFragment.this);
                        goToMapFragment();
                        drawPolygon();
                    }
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }


    private void drawPolygon() throws Exception {
        int numAction = 1;
        mObject = Manager_MCConditionalSelectorObjects.getInstance().getObjectOfSelector(mCurrentCondSelector);

        if (mObject == null)
            createPolygon(null);
        else {
            IMcObjectScheme scheme = mObject.GetScheme();
            if (scheme != null) {
                IMcObjectSchemeNode[] nodes = scheme.GetNodes(new CMcEnumBitField<>(IMcObjectSchemeNode.ENodeKindFlags.ENKF_ANY_ITEM));
                if (nodes != null && nodes.length > 0)
                    mObjSchemeItem = (IMcObjectSchemeItem) nodes[0];
            }
        }

        if (mObject != null && mObjSchemeItem != null) {
            ((MapsContainerActivity) getActivity()).mMapFragment.mView.StartInitMode(mObject, mObjSchemeItem);
        }


    }

    private void createPolygon(final SMcVector3D[] points) {
        final LocationConditionalSelectorFragment thisFragment = this;
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    mCurrentCondSelector.GetOverlayManager().SetConditionalSelectorLock(mCurrentCondSelector, true);
                    ((MapsContainerActivity) getActivity()).mMapFragment.DrawObjectForCS(points, thisFragment);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetConditionalSelectorLock");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }});
    }

    private void goToMapFragment() {
        Fragment fragmentToHide = getFragmentManager().findFragmentByTag(LocationConditionalSelectorFragment.class.getSimpleName());
        getFragmentManager().executePendingTransactions();
        MapFragment mapFragment = (MapFragment) getFragmentManager().findFragmentByTag(MapFragment.class.getSimpleName());
        FragmentTransaction transaction = getFragmentManager().beginTransaction();
        transaction.hide(fragmentToHide);
        transaction.show(mapFragment).commit();
    }

    private void initPolygonPointsLV() {
        try {
            SMcVector3D[] points = mCurrentCondSelector.GetPolygonPoints();
            if (points == null)
                points = new SMcVector3D[0];
            ArrayList<SMcVector3D> pointsArrList = new ArrayList<>(Arrays.asList(points));
            mPolygonPointsLV.setAdapter(new NewLocationPointsAdapter(getContext(), R.layout.new_location_point_row, pointsArrList));
            //Funcs.setListViewHeightBasedOnChildren(mPolygonPointsLV);

        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetPolygonPoints");
            e.printStackTrace();
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
        }/* else {
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
    public void ExitAction(int i) {

    }

    @Override
    public void NewVertex(IMcObject iMcObject, IMcObjectSchemeItem iMcObjectSchemeItem, SMcVector3D sMcVector3D, SMcVector3D sMcVector3D1, int i, double v) {

    }

    @Override
    public void PointDeleted(IMcObject iMcObject, IMcObjectSchemeItem iMcObjectSchemeItem, SMcVector3D sMcVector3D, SMcVector3D sMcVector3D1, int i) {

    }

    @Override
    public void PointNewPos(IMcObject iMcObject, IMcObjectSchemeItem iMcObjectSchemeItem, SMcVector3D sMcVector3D, SMcVector3D sMcVector3D1, int i, double v, boolean b) {

    }

    @Override
    public void ActiveIconChanged(IMcObject iMcObject, IMcObjectSchemeItem iMcObjectSchemeItem, IMcEditMode.EPermission ePermission, int i) {

    }

    @Override
    public void InitItemResults(final IMcObject iMcObject, IMcObjectSchemeItem iMcObjectSchemeItem, int i) {
        if(Manager_AMCTMapForm.getInstance().getCurMapForm() != null) {
            Manager_AMCTMapForm.getInstance().getCurMapForm().getEditModeManagerCallback().UnregisterEventsCallback(this);
            closeMapFragment();

            if (iMcObjectSchemeItem != null) {
                getActivity().runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        try {

                            Manager_MCConditionalSelectorObjects.getInstance().addNewItem(mCurrentCondSelector, mObject);
                            setPolygonPoints(iMcObject.GetLocationPoints());
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetLocationPoints");
                            e.printStackTrace();
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        }
    }

    private void setPolygonPoints(SMcVector3D[] sMcVector3Ds) {
        ArrayList<SMcVector3D> pointsArrList = new ArrayList<>(Arrays.asList(sMcVector3Ds));
        mPolygonPointsLV.setAdapter(new NewLocationPointsAdapter(getContext(), R.layout.new_location_point_row, pointsArrList));
       // Funcs.setListViewHeightBasedOnChildren(mPolygonPointsLV);
    }

    private void closeMapFragment() {
        Fragment fragment = getFragmentManager().findFragmentByTag(LocationConditionalSelectorFragment.class.getSimpleName());
        if (fragment != null) {
            ((MapsContainerActivity) getActivity()).mCurFragmentTag = LocationConditionalSelectorFragment.class.getSimpleName();
            MapFragment mapFragment = (MapFragment) getFragmentManager().findFragmentByTag(MapFragment.class.getSimpleName());
            getFragmentManager().beginTransaction().hide(mapFragment).show(fragment).commit();
        }
    }

    @Override
    public void EditItemResults(IMcObject iMcObject, IMcObjectSchemeItem iMcObjectSchemeItem, int i) {

    }

    @Override
    public void DragMapResults(IMcMapViewport iMcMapViewport, SMcVector3D sMcVector3D) {

    }

    @Override
    public void RotateMapResults(IMcMapViewport iMcMapViewport, float v, float v1) {

    }

    @Override
    public void DistanceDirectionMeasureResults(IMcMapViewport iMcMapViewport, SMcVector3D sMcVector3D, SMcVector3D sMcVector3D1, double v, double v1) {

    }

    @Override
    public void DynamicZoomResults(IMcMapViewport iMcMapViewport, float v, SMcVector3D sMcVector3D) {

    }

    @Override
    public void CalculateHeightResults(IMcMapViewport iMcMapViewport, double v, SMcVector3D[] sMcVector3Ds, int i) {

    }

    @Override
    public void CalculateVolumeResults(IMcMapViewport iMcMapViewport, double v, SMcVector3D[] sMcVector3Ds, int i) {

    }

    /**
     * This method is called if the specified {@code Observable} object's
     * {@code notifyObservers} method is called (because the {@code Observable}
     * object has been updated.
     *
     * @param observable the {@link Observable} object.
     * @param data       the data passed to {@link Observable#notifyObservers(Object)}.
     */
    @Override
    public void update(Observable observable, Object data) {
        if (observable instanceof DrawObjectResult) {
            mObjSchemeItem = (((DrawObjectResult) data).getObjScheme());
            mObject = (((DrawObjectResult) data).getObj());
        }
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

    @Override
    protected void save() {
        super.save();
        final SMcVector3D[] points = getPolygonPoints();
        if (points != null) {
            Funcs.runMapCoreFunc(new Runnable() {
                @Override
                public void run() {
                    try {
                        mCurrentCondSelector.SetPolygonPoints(points);
                    } catch (MapCoreException e) {
                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetPolygonPoints");
                        e.printStackTrace();
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            });
            if (mObject == null && points != null && points.length > 0) {
                createPolygon(points);
                Manager_MCConditionalSelectorObjects.getInstance().addItem(mCurrentCondSelector, mObject);
            }
        }
    }

    private SMcVector3D[] getPolygonPoints() {
        return ((NewLocationPointsAdapter) mPolygonPointsLV.getAdapter()).getCurRowsData();
    }

    @Override
    public void setObject(Object obj) {
        mCurrentCondSelector = (IMcLocationConditionalSelector) obj;
        super.setObject(obj);
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mCurrentCondSelector));
    }

}
