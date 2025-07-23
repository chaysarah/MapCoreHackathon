package com.elbit.mapcore.mcandroidtester.ui.objects;

import android.app.Activity;
import androidx.fragment.app.DialogFragment;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentTransaction;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.General.ObjectRef;
import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcVectorMapLayer;
import com.elbit.mapcore.Structs.SMcVector3D;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_AMCTMapForm;
import com.elbit.mapcore.mcandroidtester.model.AMCTMapLayerAsyncOperationCallback;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.MapFragment;
import com.elbit.mapcore.mcandroidtester.ui.objects.fragments.ScanItemFoundFragment;

import com.elbit.mapcore.Classes.Calculations.SMcScanGeometry;
import com.elbit.mapcore.Interfaces.Calculations.IMcSpatialQueries;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;

/**
 * Created by tc99382 on 20/08/2017.
 */
public class ScanGeometryBase {

    protected void goToMapFragment(final Fragment fragment) {
        final MapFragment mapFragment = (MapFragment) fragment.getFragmentManager().findFragmentByTag(MapFragment.class.getSimpleName());
        mapFragment.getActivity().runOnUiThread(new Runnable() {
            @Override
            public void run() {
                Fragment fragmentToHide = fragment.getFragmentManager().findFragmentByTag(fragment.getClass().getSimpleName());
                fragment.getFragmentManager().executePendingTransactions();
                FragmentTransaction transaction = fragment.getFragmentManager().beginTransaction();
                transaction.hide(fragmentToHide);
                transaction.show(mapFragment).commit();
            }
        });
    }

    protected void closeMapFragment(Fragment fragment) {
        if (fragment != null) {
            ((MapsContainerActivity) fragment.getActivity()).mCurFragmentTag =fragment.getClass().getSimpleName();
            MapFragment mapFragment = (MapFragment) fragment.getFragmentManager().findFragmentByTag(MapFragment.class.getSimpleName());
            fragment.getFragmentManager().beginTransaction().hide(mapFragment).show(fragment).commit();
        }
    }

    protected void showScanResults(Fragment fragment, SMcScanGeometry mScanGeometry, IMcSpatialQueries.STargetFound[] itemsFoundList) {
        if (itemsFoundList != null)
        {
            for (IMcSpatialQueries.STargetFound itemFound : itemsFoundList)
            {
                if (itemFound.eTargetType == IMcSpatialQueries.EIntersectionTargetType.EITT_VISIBLE_VECTOR_LAYER)
                {
                    ObjectRef<IMcMapLayer.SVectorItemFound[]> VectorItems = new ObjectRef<>();
                    ObjectRef< SMcVector3D[]> unifiedVectorItemsPoints = new ObjectRef<>();
                    IMcSpatialQueries.STargetFound itemFoundCopy = itemFound;
                    final MapFragment mapFragment = (MapFragment) fragment.getFragmentManager().findFragmentByTag(MapFragment.class.getSimpleName());
                    try
                    {
                        ((IMcVectorMapLayer)itemFound.pTerrainLayer).GetScanExtendedData(mScanGeometry,
                                itemFoundCopy,
                                Manager_AMCTMapForm.getInstance().getCurViewport(),
                                 VectorItems,  unifiedVectorItemsPoints, AMCTMapLayerAsyncOperationCallback.getInstance(mapFragment.getActivity()));
                    }
                    catch (MapCoreException McEx) {
                        AlertMessages.ShowMapCoreErrorMessage(mapFragment.getActivity(), McEx, "GetScanExtendedData");
                        McEx.printStackTrace();
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            }
        }
        openScanItemFoundForm(fragment,mScanGeometry,itemsFoundList);
    }

    private void openScanItemFoundForm(final Fragment fragment, final SMcScanGeometry mScanGeometry, final IMcSpatialQueries.STargetFound[] itemsFoundList) {

        final MapFragment mapFragment = (MapFragment) fragment.getFragmentManager().findFragmentByTag(MapFragment.class.getSimpleName());
        final Activity activity = mapFragment.getActivity();
        final String className = ScanItemFoundFragment.class.getName();
        activity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                DialogFragment dialog = (DialogFragment) DialogFragment.instantiate(activity, className);
                ((FragmentWithObject) dialog).setObject(itemsFoundList);
                dialog.setTargetFragment(mapFragment, 1);
                dialog.show(fragment.getFragmentManager(), className);
            }
        });
    }
}
