package com.elbit.mapcore.mcandroidtester.ui.objects;

import androidx.fragment.app.Fragment;

import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_AMCTMapForm;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;

import java.util.Observable;

import com.elbit.mapcore.Classes.Calculations.SMcScanPolygonGeometry;
import com.elbit.mapcore.Enums.EMcPointCoordSystem;
import com.elbit.mapcore.General.CMcEnumBitField;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Calculations.IMcSpatialQueries;
import com.elbit.mapcore.Interfaces.General.IMcEditMode;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectScheme;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeNode;
import com.elbit.mapcore.Structs.SMcVector3D;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

/**
 * Created by tc99382 on 20/08/2017.
 */
public class ScanPolygonGeometry extends ScanGeometryBase implements IMcEditMode.ICallback/*, Observer*/ {
    private final boolean mCompletelyInside;
    private final IMcSpatialQueries.SQueryParams mSpatialQueryParams;
    private final float mTolerance;
    private final EMcPointCoordSystem mScanCoordSys;
    private final Fragment mFragment;
    private SMcScanPolygonGeometry mScanPolygonGeometry;

    public ScanPolygonGeometry(Fragment fragment, EMcPointCoordSystem coordSys, IMcSpatialQueries.SQueryParams queryParams, boolean completelyInsideOnly, float tolerance) {
        mCompletelyInside = completelyInsideOnly;
        mSpatialQueryParams = queryParams;
        mScanCoordSys = coordSys;
        mTolerance = tolerance;
        mFragment = fragment;
    }

    public void startPolyScan()
    {
        if(Manager_AMCTMapForm.getInstance().getCurMapForm() != null) {
            Manager_AMCTMapForm.getInstance().getCurMapForm().getEditModeManagerCallback().SetEventsCallback(ScanPolygonGeometry.this);
            goToMapFragment(mFragment);
            drawPolygon();
        }
    }
    private void drawPolygon() {
        ((MapsContainerActivity) mFragment.getActivity()).mMapFragment.DrawPolygonForScan();
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
        if (Manager_AMCTMapForm.getInstance().getCurMapForm() != null) {
            Manager_AMCTMapForm.getInstance().getCurMapForm().getEditModeManagerCallback().UnregisterEventsCallback(this);
            try {
                final IMcMapViewport curViewport = Manager_AMCTMapForm.getInstance().getCurViewport();
                IMcObjectScheme objScheme = iMcObject.GetScheme();
                IMcObjectSchemeNode[] objSchemeNodes = objScheme.GetNodes(new CMcEnumBitField<IMcObjectSchemeNode.ENodeKindFlags>(IMcObjectSchemeNode.ENodeKindFlags.ENKF_ANY_ITEM));
                SMcVector3D[] coords = objSchemeNodes[0].GetCoordinates(curViewport, mScanCoordSys, iMcObject);
                mScanPolygonGeometry = new SMcScanPolygonGeometry(mScanCoordSys, coords);
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            iMcObject.Remove();
                            IMcSpatialQueries.STargetFound[] targetFound = null;
                            targetFound = curViewport.ScanInGeometry(mScanPolygonGeometry, mCompletelyInside, mSpatialQueryParams);
                            showScanResults(mFragment, mScanPolygonGeometry, targetFound);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(mFragment.getContext(), e, "InitItemResults");
                            e.printStackTrace();
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }});
            } catch (MapCoreException e) {
                AlertMessages.ShowMapCoreErrorMessage(mFragment.getContext(), e, "InitItemResults");
                e.printStackTrace();
            } catch (Exception e) {
                e.printStackTrace();
            }
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

    @Override
    public void update(Observable observable, Object data) {
        if (observable instanceof DrawPolygonSchemeForScanResult) {
            mObjSchemeItem = (((DrawPolygonSchemeForScanResult) data).getObjScheme());
            mObject = (((DrawPolygonSchemeForScanResult) data).getObj());
        }
    }*/
}
