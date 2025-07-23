/*
package com.elbit.mapcore.mcandroidtester.ui.map.activities;

import android.content.DialogInterface;
import android.content.Intent;
import android.opengl.GLSurfaceView;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Gravity;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.LinearLayout;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.managers.Manager_MCOverlayManager;
import com.elbit.mapcore.mcandroidtester.model.AMCTUserDataFactory;
import com.elbit.mapcore.mcandroidtester.model.AMCTViewPort;
import com.elbit.mapcore.mcandroidtester.ui.MainActivity;
import com.elbit.mapcore.mcandroidtester.ui.map.AMcGLSurfaceView;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectScheme;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeNode;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlay;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;
import com.elbit.mapcore.Structs.SMcVector3D;

public class ViewPortsActivity extends AppCompatActivity {
    private AMcGLSurfaceView mView;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        int ui = getWindow().getDecorView().getSystemUiVisibility();
        ui = ui | View.SYSTEM_UI_FLAG_HIDE_NAVIGATION | View.SYSTEM_UI_FLAG_FULLSCREEN | View.SYSTEM_UI_FLAG_IMMERSIVE_STICKY;
        getWindow().getDecorView().setSystemUiVisibility(ui);
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON, WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);
      //  mView = new AMcGLSurfaceView(this);
        //setContentView ( mView );
        setContentView ( R.layout.amc_gl_surface_view );

        mView = (AMcGLSurfaceView)findViewById(R.id.gl_surface);


        LinearLayout ll = new LinearLayout(this);

   */
/*     Button btnRender = new Button(this);
        btnRender.setText("Render");
        btnRender.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                mView.Render();
            }
        });

        ll.addView(btnRender);*//*

        ll.setGravity(Gravity.CENTER_HORIZONTAL | Gravity.CENTER_VERTICAL);
        this.addContentView(ll,
                new LinearLayout.LayoutParams(WindowManager.LayoutParams.MATCH_PARENT, LinearLayout.LayoutParams.MATCH_PARENT));

        Button btnScale = new Button(this);
        btnScale.setText("Zoom In");
        btnScale.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                mView.ZoomIn();
                //       mView.Render();
            }
        });
        ll.addView(btnScale);

        Button btnViewport = new Button(this);
        btnViewport.setText("Zoom Out");
        btnViewport.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                mView.ZoomOut();
                //        mView.Render();
            }
        });
        ll.addView(btnViewport);

        Button btnDrawLine = new Button(this);
        btnDrawLine.setText("line");
        btnDrawLine.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                IMcObjectScheme[] ObjectSchemes  = null;
                SMcVector3D[] locationPoints = new SMcVector3D[0];
                IMcOverlay overlays[] = null;
                IMcOverlayManager overlayManager = AMCTViewPort.getViewportInCreation().getmOverlayManager();
                AMCTUserDataFactory UDF = new AMCTUserDataFactory();
                try {
                    overlays = overlayManager.GetOverlays();
                } catch (Exception e) {
                    e.printStackTrace();
                }
                if(overlays != null && overlays.length > 0) {
                    IMcOverlay activeOverlay = overlays[0]; //Manager_MCOverlayManager.mActiveOverlay;
                    try {
                        ObjectSchemes = overlayManager.LoadObjectSchemes("/sdcard/MapCore/defualt_line999.m", UDF);
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                    if(ObjectSchemes != null && ObjectSchemes.length > 0) {
                        IMcObjectScheme scheme = ObjectSchemes[0];
                        try {
                            IMcObject obj = IMcObject.Static.Create(activeOverlay,
                                    scheme,
                                    locationPoints);
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        try {
                            IMcObjectSchemeNode node = scheme.GetNodeByID(999);
                        }
                        catch (MapCoreException e)
                        {
                            AlertMessages.ShowMapCoreErrorMessage(v.getContext(), e, "McMapDevice.GetNodeByID");
                        }
                        catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                }
            }
        });
        ll.addView(btnDrawLine);
    }

    @Override
    public void onBackPressed() {
        AlertMessages.ShowYesNoMessage(this, "Exit", "Do you want to close the viewport? all your data will be deleted.", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                AMCTViewPort.getViewportInCreation().resetCurViewPort();
                openMainActivity();
                finish();
            }
        });
        //doubleBackToExit();

    }

    private void openMainActivity() {
        Intent intent =new Intent(this, MainActivity.class);
        startActivity(intent);
    }

    @Override
    protected void onResume() {
        super.onResume();
        mView.onResume();
    }
    @Override
    protected void onPause() {
        mView.onPause();
        super.onPause();
    }

}
*/
