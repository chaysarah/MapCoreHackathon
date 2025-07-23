package com.elbit.mapcore.mcandroidtester.utils.customviews;

import android.app.Activity;
import android.content.Context;
import android.content.res.TypedArray;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;
import androidx.appcompat.app.AppCompatActivity;
import android.util.AttributeSet;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;

import com.elbit.mapcore.General.ObjectRef;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_AMCTMapForm;
import com.elbit.mapcore.mcandroidtester.model.SamplePoint;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.MapFragment;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.BaseApplication;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

import java.util.Observable;
import java.util.Observer;

import com.elbit.mapcore.Enums.EMcPointCoordSystem;
import com.elbit.mapcore.General.IMcErrors;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Calculations.IMcSpatialQueries;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Structs.SMcVector2D;
import com.elbit.mapcore.Structs.SMcVector3D;

/**
 * Created by TC99382 on 20/06/2017.
 */
public class SampleLocationPointsBttn extends Button implements Observer {
    private final OnSamplePointListener mSamplePointListener;
    private final Context mContext;
    private String mContainerFragment;
    float mX;
    float mY;
    private IMcSpatialQueries.EQueryPrecision mQueryPrecision = IMcSpatialQueries.EQueryPrecision.EQP_DEFAULT;
    private boolean mPointInOverlayManagerCoordSys;
    private double mPointZValue;
    private boolean mSampleOnePoint;
    private int mUserControlToUpdate;
    private EMcPointCoordSystem mPointCoordSystem;
    private int mListViewIdToUpdate;

    public void initBttn(String containerFragmentName,Boolean pointInOMCoordSys,double pointZValue,IMcSpatialQueries.EQueryPrecision queryPrecision,EMcPointCoordSystem pointCoordSystem,boolean sampleOnePoint,int userControlToUpdate,int listViewIdToUpdate)
    {
        mContainerFragment=containerFragmentName;
        mPointInOverlayManagerCoordSys=pointInOMCoordSys;
        mPointZValue=pointZValue;
        mQueryPrecision=queryPrecision;
        mPointCoordSystem=pointCoordSystem;
        mSampleOnePoint=sampleOnePoint;
        mUserControlToUpdate=userControlToUpdate;
        mListViewIdToUpdate=listViewIdToUpdate;
    }

    public EMcPointCoordSystem getPointCoordSystem() {
        return mPointCoordSystem;
    }

    public void setPointCoordSystem(EMcPointCoordSystem mPointCoordSystem) {
        this.mPointCoordSystem = mPointCoordSystem;
    }

    public double getPointZValue() {
        return mPointZValue;
    }

    public void setPointZValue(double mPointZValue) {
        this.mPointZValue = mPointZValue;
    }

    public boolean isPointInOverlayManagerCoordSys() {
        return mPointInOverlayManagerCoordSys;
    }

    public void setPointInOverlayManagerCoordSys(boolean mPointInOverlayManagerCoordSys) {
        this.mPointInOverlayManagerCoordSys = mPointInOverlayManagerCoordSys;
    }

    public IMcSpatialQueries.EQueryPrecision getQueryPrecision() {
        return mQueryPrecision;
    }

    public void setQueryPrecision(IMcSpatialQueries.EQueryPrecision mQueryPrecision) {
        this.mQueryPrecision = mQueryPrecision;
    }


    public SampleLocationPointsBttn(Context context, AttributeSet attrs) {
        super(context, attrs);
        mContext = context;
        setText("...");

        if (context instanceof OnSamplePointListener) {
            mSamplePointListener = (OnSamplePointListener) context;
        } else {
            throw new RuntimeException(context.toString()
                    + " must implement OnSamplePointListener");
        }
        //initStyleAttr(context, attrs);
        this.setOnClickListener(new OnClickListener() {
            @Override
            public void onClick(View v) {
                mSamplePointListener.onSamplePoint(mContainerFragment, SampleLocationPointsBttn.this);
                goToMapFragment();
            }
        });


    }

    private void initStyleAttr(Context context, AttributeSet attrs) {
        TypedArray typedArray = context.obtainStyledAttributes(attrs, R.styleable.SampleLocationPointsBttn);
        mContainerFragment = typedArray.getString(R.styleable.SampleLocationPointsBttn_container_fragment_name);
        try {
            mQueryPrecision = getEQueryPrecisionInstance(typedArray.getInt(R.styleable.SampleLocationPointsBttn_query_precision, IMcSpatialQueries.EQueryPrecision.EQP_DEFAULT.getValue()));
        } catch (MapCoreException e) {
            e.printStackTrace();
        }
        mPointInOverlayManagerCoordSys = typedArray.getBoolean(R.styleable.SampleLocationPointsBttn_point_in_overlay_manager_coord_sys, true);
        mPointZValue = Double.parseDouble(typedArray.getString(R.styleable.SampleLocationPointsBttn_point_z_value));
        try {
            mPointCoordSystem = getEMcPointCoordSystemInstance(typedArray.getInt(R.styleable.SampleLocationPointsBttn_point_coord_system, EMcPointCoordSystem.EPCS_WORLD.getValue()));
        } catch (MapCoreException e) {
            e.printStackTrace();
        }
        mListViewIdToUpdate = typedArray.getResourceId(R.styleable.SampleLocationPointsBttn_list_view_id_to_update, -1);
        mSampleOnePoint = typedArray.getBoolean(R.styleable.SampleLocationPointsBttn_sample_one_point, false);

        mUserControlToUpdate=typedArray.getResourceId(R.styleable.SampleLocationPointsBttn_user_control_name,-1);

    }

    //TODO ask to public this func in api
    private IMcSpatialQueries.EQueryPrecision getEQueryPrecisionInstance(int val) throws MapCoreException {
        switch (val) {
            case 0:
                return IMcSpatialQueries.EQueryPrecision.EQP_DEFAULT;
            case 1:
                return IMcSpatialQueries.EQueryPrecision.EQP_DEFAULT_PLUS_LOWEST;
            case 2:
                return IMcSpatialQueries.EQueryPrecision.EQP_HIGHEST;
            case 3:
                return IMcSpatialQueries.EQueryPrecision.EQP_HIGH;
            case 4:
                return IMcSpatialQueries.EQueryPrecision.EQP_MEDIUM;
            case 5:
                return IMcSpatialQueries.EQueryPrecision.EQP_LOW;
            case 6:
                return IMcSpatialQueries.EQueryPrecision.EQP_LOWEST;
            default:
                throw new MapCoreException(IMcErrors.ECode.INVALID_ARGUMENT, "value out of range");
        }

    }

    private EMcPointCoordSystem getEMcPointCoordSystemInstance(int val) throws MapCoreException {
        switch (val) {
            case 0:
                return EMcPointCoordSystem.EPCS_IMAGE;
            case 1:
                return EMcPointCoordSystem.EPCS_WORLD;
            case 2:
                return EMcPointCoordSystem.EPCS_SCREEN;
            default:
                throw new MapCoreException(IMcErrors.ECode.INVALID_ARGUMENT, "value out of range");
        }
    }

    private void goToMapFragment() {
        FragmentManager fragmentManager = ((AppCompatActivity) mContext).getSupportFragmentManager();
        Fragment fragmentToHide = fragmentManager.findFragmentByTag(mContainerFragment);
        MapFragment mapFragment = (MapFragment) fragmentManager.findFragmentByTag(MapFragment.class.getSimpleName());
        FragmentTransaction transaction = fragmentManager.beginTransaction();
        transaction.hide(fragmentToHide);
        transaction.show(mapFragment).commit();
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
        mX = ((SamplePoint) observable).mX;
        mY = ((SamplePoint) observable).mY;

        IMcMapViewport viewPort = Manager_AMCTMapForm.getInstance().getCurViewport();
        ObjectRef<Boolean> isIntersect = new ObjectRef<>();
        SMcVector3D worldPoint = new SMcVector3D();
        final SMcVector3D screenPoint = new SMcVector3D();
        SMcVector3D imagePoint = new SMcVector3D();
        screenPoint.x = mX;
        screenPoint.y = mY;
        IMcSpatialQueries.SQueryParams queryParams = new IMcSpatialQueries.SQueryParams();
        queryParams.eTerrainPrecision = mQueryPrecision;

        try {
            viewPort.ScreenToWorldOnTerrain(screenPoint, worldPoint, isIntersect, queryParams);
            if (isIntersect.getValue() == false)
                viewPort.ScreenToWorldOnPlane(screenPoint, worldPoint, isIntersect);
            if (isIntersect.getValue() == false) {
                worldPoint.x = 0;
                worldPoint.y = 0;
                worldPoint.z = 0;
            }

            if (viewPort.GetImageCalc() != null) {

                imagePoint = worldPoint;
                imagePoint.z = 0;

                if (mPointCoordSystem == EMcPointCoordSystem.EPCS_WORLD) {
                    worldPoint = viewPort.GetImageCalc().ImagePixelToCoordWorld(new SMcVector2D(imagePoint.x, imagePoint.y));
                }
            }

            if (viewPort.GetOverlayManager() != null && mPointInOverlayManagerCoordSys) {
                worldPoint = viewPort.ViewportToOverlayManagerWorld(worldPoint, viewPort.GetOverlayManager());
            }
            if (mPointZValue != Double.MAX_VALUE) {
                worldPoint.z = mPointZValue;
            }
        } catch (MapCoreException e) {
            e.printStackTrace();
            AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "ViewportToOverlayManagerWorld()");
        } catch (Exception e) {
            e.printStackTrace();
        }

        final SMcVector3D finalWorldPoint = worldPoint;
        final SMcVector3D finalImagePoint = imagePoint;
        final SMcVector3D finalWorldPoint1 = worldPoint;
        final SMcVector3D finalImagePoint1 = imagePoint;
        final SMcVector3D finalWorldPoint2 = worldPoint;
        final SMcVector3D finalImagePoint2 = imagePoint;
        ((Activity) mContext).runOnUiThread(new Runnable() {
            @Override
            public void run() {
                if (mSampleOnePoint) {
                    Object userControl=getRootView().findViewById(mUserControlToUpdate);
                    if(userControl instanceof ThreeDVector)
                    {
                        ThreeDVector control= (ThreeDVector) userControl;
                        switch (mPointCoordSystem)
                        {
                            case EPCS_WORLD: control.setVector3D(finalWorldPoint1);
                                break;
                            case EPCS_SCREEN: control.setVector3D(new SMcVector3D(Double.parseDouble(String.valueOf(screenPoint.x)),Double.parseDouble(String.valueOf(screenPoint.y)),0));
                                break;
                            case EPCS_IMAGE:control.setVector3D(finalImagePoint1);
                        }
                    }
                    if(userControl instanceof TwoDVector)
                    {
                        TwoDVector control= (TwoDVector) userControl;
                        switch (mPointCoordSystem)
                        {
                            case EPCS_WORLD:
                                control.setmX(finalWorldPoint2.x);
                                control.setmY(finalWorldPoint2.y);
                                break;
                            case EPCS_SCREEN: control.setVector2D(new SMcVector2D(Double.parseDouble(String.valueOf(screenPoint.x)),Double.parseDouble(String.valueOf(screenPoint.y))));
                                break;
                            case EPCS_IMAGE:
                                control.setmX(finalImagePoint2.x);
                                control.setmY(finalImagePoint2.y);
                        }
                    }

                } else {
                    ListView lv = (ListView) getRootView().findViewById(mListViewIdToUpdate);
                    SMcVector3D zeroRow = (SMcVector3D) ((ArrayAdapter) lv.getAdapter()).getItem(0);

                    if (zeroRow.x + zeroRow.y + zeroRow.z == 0)
                        ((ArrayAdapter) lv.getAdapter()).remove(zeroRow);
                    switch (mPointCoordSystem) {
                        case EPCS_WORLD:
                            ((ArrayAdapter) lv.getAdapter()).add(finalWorldPoint);
                            break;
                        case EPCS_SCREEN:
                            ((ArrayAdapter) lv.getAdapter()).add(screenPoint);
                            break;
                        case EPCS_IMAGE:
                            ((ArrayAdapter) lv.getAdapter()).add(finalImagePoint);
                            break;
                    }
                    Funcs.setListViewHeightBasedOnChildren(lv);
                }
            }
        });
    }

    public interface OnSamplePointListener {
        void onSamplePoint(String sampleBttnContainerFragmentTag, Observer observer);
    }

}
