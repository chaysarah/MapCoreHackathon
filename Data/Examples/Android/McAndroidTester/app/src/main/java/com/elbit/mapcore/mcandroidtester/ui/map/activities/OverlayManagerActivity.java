package com.elbit.mapcore.mcandroidtester.ui.map.activities;

import android.net.Uri;
import androidx.appcompat.app.AppCompatActivity;
import android.os.Bundle;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.OnCreateCoordinateSystemListener;
import com.elbit.mapcore.mcandroidtester.managers.ObjectWorld.Manager_MCOverlayManager;
import com.elbit.mapcore.mcandroidtester.model.AMCTViewPort;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.GridCoordinateSysFragment;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.OverlayManagerFragment;
import com.elbit.mapcore.mcandroidtester.utils.BaseApplication;

import com.elbit.mapcore.Interfaces.Calculations.GridCoordSystem.IMcGridCoordinateSystem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

public class OverlayManagerActivity extends AppCompatActivity implements OverlayManagerFragment.OnFragmentInteractionListener,OnCreateCoordinateSystemListener,GridCoordinateSysFragment.OnFragmentInteractionListener {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_overlay_manager);
        BaseApplication.setCurrActivityContext(this);
    }

    @Override
    public void onFragmentInteraction(Uri uri) {

    }

    @Override
    public void onCoordSysCreated(final IMcGridCoordinateSystem mNewGridCoordinateSystem) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                IMcOverlayManager imcOverlayManager = Manager_MCOverlayManager.getInstance().CreateOverlayManager(mNewGridCoordinateSystem, true);
                if (imcOverlayManager != null) {
                    AMCTViewPort.getViewportInCreation().setOverlayManager(imcOverlayManager);
                    runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            onBackPressed();
                        }
                    });
                }
            }
        });
    }
}
