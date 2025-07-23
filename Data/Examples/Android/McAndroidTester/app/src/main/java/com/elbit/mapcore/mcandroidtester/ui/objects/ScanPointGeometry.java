package com.elbit.mapcore.mcandroidtester.ui.objects;

import android.util.Log;

import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_AMCTMapForm;
import com.elbit.mapcore.mcandroidtester.model.SamplePoint;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.ScanFragment;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SampleLocationPointsBttn;

import java.util.Observable;
import java.util.Observer;

import com.elbit.mapcore.Classes.Calculations.SMcScanPointGeometry;
import com.elbit.mapcore.Enums.EMcPointCoordSystem;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Calculations.IMcSpatialQueries;
import com.elbit.mapcore.Structs.SMcVector3D;

/**
 * Created by tc99382 on 20/08/2017.
 */
public class ScanPointGeometry extends ScanGeometryBase implements Observer {
    private final boolean mCompletelyInside;
    private final IMcSpatialQueries.SQueryParams mSpatialQueryParams;
    private final float mTolerance;
    private final EMcPointCoordSystem mScanCoordSys;
    private final SMcVector3D mManualPoint;
    private final SampleLocationPointsBttn.OnSamplePointListener mSamplePointListener;
    private float mX;
    private float mY;
    private SMcVector3D mSelectedPoint;
    private SMcScanPointGeometry mScanPointGeometry;
    private ScanFragment mScanFragment;

    public ScanPointGeometry(ScanFragment scanFragment, EMcPointCoordSystem coordSys, IMcSpatialQueries.SQueryParams queryParams, boolean completelyInsideOnly, float tolerance, SMcVector3D point) {
        mCompletelyInside = completelyInsideOnly;
        mSpatialQueryParams = queryParams;
        mTolerance = tolerance;
        mScanCoordSys = coordSys;
        mManualPoint = point;
        mScanFragment = scanFragment;
        if (scanFragment.getContext() instanceof SampleLocationPointsBttn.OnSamplePointListener) {
            mSamplePointListener = (SampleLocationPointsBttn.OnSamplePointListener) scanFragment.getContext();
        } else {
            throw new RuntimeException(scanFragment.getContext().toString()
                    + " must implement OnSamplePointListener");
        }
    }

    public void startPointScan() {
        mSamplePointListener.onSamplePoint(null, ScanPointGeometry.this);
        goToMapFragment(mScanFragment);
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
        double x = mX;
        double y = mY;
        mSelectedPoint = new SMcVector3D(x, y, 0);
        mScanPointGeometry = new SMcScanPointGeometry(mScanCoordSys, mSelectedPoint, mTolerance);
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    IMcSpatialQueries.STargetFound[] targetFounds = null;
                    targetFounds = Manager_AMCTMapForm.getInstance().getCurViewport().ScanInGeometry(mScanPointGeometry, mCompletelyInside, mSpatialQueryParams);

                    showScanResults(mScanFragment, mScanPointGeometry, targetFounds);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(mScanFragment.getContext(), e, "ScanInGeometry");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    public void startManualPointScan() {
        mScanPointGeometry = new SMcScanPointGeometry(mScanCoordSys, mManualPoint, mTolerance);
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    IMcSpatialQueries.STargetFound[] targetFounds = Manager_AMCTMapForm.getInstance().getCurViewport().ScanInGeometry(mScanPointGeometry, mCompletelyInside, mSpatialQueryParams);
                    goToMapFragment(mScanFragment);
                    showScanResults(mScanFragment, mScanPointGeometry, targetFounds);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(mScanFragment.getContext(), e, "ScanInGeometry");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }
}
