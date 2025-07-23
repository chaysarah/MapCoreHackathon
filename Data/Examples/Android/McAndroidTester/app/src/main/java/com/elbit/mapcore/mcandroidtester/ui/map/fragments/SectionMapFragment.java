package com.elbit.mapcore.mcandroidtester.ui.map.fragments;

import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentTransaction;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.ListView;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.General.IMcEditMode;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;
import com.elbit.mapcore.Structs.SMcVector3D;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_AMCTMapForm;
import com.elbit.mapcore.mcandroidtester.managers.ObjectWorld.Manager_MCOverlayManager;
import com.elbit.mapcore.mcandroidtester.model.AMCTMapForm;
import com.elbit.mapcore.mcandroidtester.model.AMCTViewPort;
import com.elbit.mapcore.mcandroidtester.model.DrawObjectResult;
import com.elbit.mapcore.mcandroidtester.ui.adapters.NewLocationPointsAdapter;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Observable;
import java.util.Observer;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link SectionMapFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link SectionMapFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class SectionMapFragment extends Fragment implements IMcEditMode.ICallback, Observer {

    private IMcMapViewport mViewport;
    private OnFragmentInteractionListener mListener;
    private Button mSectionMapDrawLineBttn;
    private Button mSectionMapCreateBttn;
    private ListView mSectionMapPointsLV;
    private IMcObjectSchemeItem mObjSchemeItem;
    private IMcObject mObject;
    private CheckBox mIsCheckHeight;

    // TODO: Rename and change types and number of parameters
    public static SectionMapFragment newInstance() {
        SectionMapFragment fragment = new SectionMapFragment();
        return fragment;
    }

    public SectionMapFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        View view = inflater.inflate(R.layout.fragment_section_map, container, false);
        mSectionMapDrawLineBttn = (Button) view.findViewById(R.id.section_map_draw_line_bttn);
        mSectionMapCreateBttn = (Button) view.findViewById(R.id.section_map_create_bttn);
        mSectionMapPointsLV = (ListView) view.findViewById(R.id.section_map_points_lv);
        mIsCheckHeight = (CheckBox) view.findViewById(R.id.section_map_height_cb);

        mSectionMapDrawLineBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                try {
                    RemoveObject();

                    if (Manager_AMCTMapForm.getInstance().getCurMapForm() != null) {
                        Manager_AMCTMapForm.getInstance().getCurMapForm().getEditModeManagerCallback().SetEventsCallback(SectionMapFragment.this);
                        goToMapFragment();
                        drawLine();
                    }
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });

        mSectionMapCreateBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                AMCTMapForm amctMapForm = Manager_AMCTMapForm.getInstance().getCurMapForm();
                IMcMapViewport vp = amctMapForm.getmMapViewport();
                try {
                    IMcMapViewport.SCreateData createData = new IMcMapViewport.SCreateData(vp.GetMapType());
                    createData.pDevice = vp.GetDevice();
                    createData.pCoordinateSystem = vp.GetCoordinateSystem();
                    createData.pOverlayManager = IMcOverlayManager.Static.Create(vp.GetCoordinateSystem());

                    AMCTViewPort.getViewportInCreation().resetCurViewPort();
                    AMCTViewPort.getViewportInCreation().addTerrainToList(vp.GetTerrains());
                    AMCTViewPort.getViewportInCreation().setGridCoordinateSystem(vp.GetCoordinateSystem());
                    AMCTViewPort.getViewportInCreation().setMapDevice(vp.GetDevice());
                    IMcOverlayManager imcOverlayManager = Manager_MCOverlayManager.getInstance().CreateOverlayManager(vp.GetCoordinateSystem(), true);
                    AMCTViewPort.getViewportInCreation().setOverlayManager(imcOverlayManager);
                    AMCTViewPort.getViewportInCreation().setIsSectionMap(true);
                    AMCTViewPort.getViewportInCreation().setSectionMapPoints(mObject.GetLocationPoints());

                    Intent viewPortIntent = new Intent(getContext(), MapsContainerActivity.class);
                    viewPortIntent.putExtra("newViewPort", true);
                    startActivity(viewPortIntent);
                    goToMapFragment();
                    RemoveObject();

                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "Get viewport data");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
        return view;
    }

    private void RemoveObject()
    {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    if (mObject != null) {
                        mObject.Remove();
                        mObject = null;
                    }
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "Remove Object");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }


    private void goToMapFragment() {
        Fragment fragmentToHide = getFragmentManager().findFragmentByTag(this.getClass().getSimpleName());
        getFragmentManager().executePendingTransactions();
        MapFragment mapFragment = (MapFragment) getFragmentManager().findFragmentByTag(MapFragment.class.getSimpleName());
        FragmentTransaction transaction = getFragmentManager().beginTransaction();
        transaction.hide(fragmentToHide);
        transaction.show(mapFragment).commit();
    }

    private void drawLine() throws Exception {
        ((MapsContainerActivity) getActivity()).mMapFragment.DrawObjectForSectionMap(null, this);


       /* if (mObject != null && mObjSchemeItem != null) {
            ((MapsContainerActivity) getActivity()).mMapFragment.mView.StartInitMode(mObject, mObjSchemeItem);
        }*/


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
            //throw new RuntimeException(context.toString()
            //        + " must implement OnFragmentInteractionListener");
        }
        if (context instanceof MapsContainerActivity) {
            ((MapsContainerActivity) context).mCurFragmentTag = SectionMapFragment.class.getSimpleName();
        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
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

            if (iMcObject != null) {
                getActivity().runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            ArrayList<SMcVector3D> pointsArrList = new ArrayList<>(Arrays.asList(iMcObject.GetLocationPoints()));
                            mSectionMapPointsLV.setAdapter(new NewLocationPointsAdapter(getContext(), R.layout.new_location_point_row, pointsArrList));
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

    private void closeMapFragment() {
        Fragment fragment = getFragmentManager().findFragmentByTag(this.getClass().getSimpleName());
        if (fragment != null) {
            ((MapsContainerActivity) getActivity()).mCurFragmentTag = this.getClass().getSimpleName();
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
    public void CalculateHeightResults(IMcMapViewport iMcMapViewport, double v, SMcVector3D[] sMcVector3DS, int i) {

    }

    @Override
    public void CalculateVolumeResults(IMcMapViewport iMcMapViewport, double v, SMcVector3D[] sMcVector3DS, int i) {

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
