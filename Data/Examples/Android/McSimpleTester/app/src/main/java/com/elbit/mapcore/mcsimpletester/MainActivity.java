package com.elbit.mapcore.mcsimpletester;

//import android.support.v7.app.ActionBar;
//import android.support.v7.app.AppCompatActivity;
import android.app.Activity;
import android.os.Bundle;


import android.opengl.GLSurfaceView;

import android.view.Gravity;
import android.view.MenuInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.Menu;
import android.view.MenuItem;
import android.view.Window;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.Toast;


import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Calculations.GridCoordSystem.IMcGridCoordSystemGeographic;
import com.elbit.mapcore.Interfaces.Calculations.GridCoordSystem.IMcGridCoordinateSystem;
import com.elbit.mapcore.Interfaces.Calculations.GridCoordSystem.IMcGridUTM;
import com.elbit.mapcore.Interfaces.Calculations.IMcGridConverter;
import com.elbit.mapcore.Interfaces.Map.IMcMapCamera;
import com.elbit.mapcore.Interfaces.Map.IMcMapDevice;
import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcMapTerrain;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.Map.IMcNativeDtmMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcNativeRasterMapLayer;
import com.elbit.mapcore.Structs.SMcVector3D;

public class MainActivity extends Activity {
    static {
        System.loadLibrary("JniMapCore7D");
    }

    private GLSurfaceView glSurfaceView;
    private IMcMapDevice m_pMapDevice;
    private IMcGridCoordinateSystem m_pGridCoordinateSystem;
    private IMcNativeRasterMapLayer m_pNativeRasterLayer;
    private IMcNativeDtmMapLayer m_pNativeDtmLayer;
    private IMcMapTerrain m_pTerrain;
    private IMcMapViewport m_pViewport;

    private McGLSurfaceView mView;

    @Override
    public boolean onTouchEvent(MotionEvent event) {
        mView.TouchEvent(event);


        return true;
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        int ui = getWindow().getDecorView().getSystemUiVisibility();
        ui = ui | View.SYSTEM_UI_FLAG_HIDE_NAVIGATION | View.SYSTEM_UI_FLAG_FULLSCREEN | View.SYSTEM_UI_FLAG_IMMERSIVE_STICKY;
        getWindow().getDecorView().setSystemUiVisibility(ui);
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON, WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);
        mView = new McGLSurfaceView(this);
        setContentView ( mView );
        LinearLayout ll = new LinearLayout(this);
        ll.setGravity(Gravity.CENTER_HORIZONTAL | Gravity.TOP);

        Button btn2D = new Button(this);
        btn2D.setText("2D");
        /*
        btn2D.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                mView.ChangeViewport(EDisplayType.EDT_2D);
            }
        });
*/
        btn2D.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                SMcVector3D screen = new SMcVector3D(500,500,0);
                SMcVector3D world = new SMcVector3D();
                try {

                    mView.mRenderer.m_pViewport.ScreenToWorldOnPlane(screen, world, true);
                } catch (Exception e) {
                    e.printStackTrace();
                }
                int i = 5;

                mView.ChangeViewport(EDisplayType.EDT_2D);
            }
        });

        ll.addView(btn2D);
        this.addContentView(ll,
                new LinearLayout.LayoutParams(WindowManager.LayoutParams.MATCH_PARENT, LinearLayout.LayoutParams.MATCH_PARENT));

        Button btn3D = new Button(this);
        btn3D.setText("3D");
        btn3D.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                mView.ChangeViewport(EDisplayType.EDT_3D);
///                mView.Render();
            }
        });
        ll.addView(btn3D);

        Button btnAR = new Button(this);
        btnAR.setText("AR");
        btnAR.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                mView.ChangeViewport(EDisplayType.EDT_AR);

//                mView.ZoomOut();
                //               mView.Render();
            }
        });
        ll.addView(btnAR);

        Button btnObjects = new Button(this);
        btnObjects.setText("Objects");
        btnObjects.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                mView.AddObjects();

//                mView.ZoomOut();
                //               mView.Render();
            }
        });
        ll.addView(btnObjects);


    }




    private void callConverterCalc()
    {
        IMcGridConverter converter= null;
        IMcGridConverter converter2= null;
        IMcGridConverter converter3= null;
        IMcGridCoordinateSystem.SDatumParams pDatumParams = new IMcGridCoordinateSystem.SDatumParams();
        IMcGridUTM gridUTM1= null,gridUTM2 = null;
        IMcGridCoordSystemGeographic gridGeo = null;

        try {
            gridUTM1 = IMcGridUTM.Static.Create(18, IMcGridCoordinateSystem.EDatumType.EDT_ED50_ISRAEL, pDatumParams);
            //McGridUTM.GetGridUTM2(gridUTM2, gridUTM1.GetPtrMc());
            //gridUTM2 = (IMcGridUTM)IMcGridUTMStatic.GetGridUTM(gridUTM1.GetPtrMc());

            gridUTM2 = IMcGridUTM.Static.Create(36, IMcGridCoordinateSystem.EDatumType.EDT_ED50_ISRAEL, pDatumParams);
            gridGeo = IMcGridCoordSystemGeographic.Static.Create(IMcGridCoordinateSystem.EDatumType.EDT_WGS84, pDatumParams);
        } catch (MapCoreException e1) {
            // TODO Auto-generated catch block
            e1.printStackTrace();
        };

        try {
            //Create
            converter = IMcGridConverter.Static.Create(gridUTM1, gridUTM2);
            converter2 = IMcGridConverter.Static.Create(gridUTM2, gridGeo);
            //         converter3 = (IMcGridConverter)IMcGridConverter.Static.GetGridConverter(converter.GetPtrMc());

            // SetCheckGridLimits
            converter.SetCheckGridLimits(false);

            // ConvertAtoB
            SMcVector3D LocationA = new SMcVector3D(500000,3651342.7,0);
            SMcVector3D pLocationB = new SMcVector3D();
            // WrapperInt pnZoneB = new WrapperInt();
            Integer pnZoneB=new Integer(0);
            converter.ConvertAtoB(LocationA, pLocationB, pnZoneB);
            // result b is like a

            // ConvertBtoA
            SMcVector3D LocationB2 = new SMcVector3D(500000, 3651342.7, 0);
            SMcVector3D pLocationA2 = new SMcVector3D();
            //WrapperInt pnZoneA2 = new  WrapperInt();
            Integer pnZoneA2=new Integer(0);
            converter.ConvertBtoA(LocationB2, pLocationA2, pnZoneA2);
            // result a is like b
            // GetCheckGridLimits
            boolean isCheckGridLimit = converter.GetCheckGridLimits();

            // SetCheckGridLimits
            converter2.SetCheckGridLimits(true);

            // ConvertAtoB
            LocationA = new SMcVector3D(500000, 3651342.7, 0);
            pLocationB = new SMcVector3D();
            //pnZoneB = new WrapperInt();
            //    Integer pnZoneB=new Integer(0);
            converter2.ConvertAtoB(LocationA, pLocationB, pnZoneB);
            // result b (3299965.16, 3299892.72, -2.1011) zone = 0

            // ConvertBtoA
            LocationB2 = new SMcVector3D(3299965.16, 3299892.72, -2.1011);
            pLocationA2 = new SMcVector3D();
            //       pnZoneA2 = new  WrapperInt();
            converter2.ConvertBtoA(LocationB2, pLocationA2, pnZoneA2);
            // result a is like LocationA zone = 36

        } catch (MapCoreException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
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
    @Override
    protected void onDestroy(){
        super.onDestroy();


    }

  /*  @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.menu_myactivity, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        Toast.makeText(getApplicationContext(),	item.getTitle() + " selected", Toast.LENGTH_SHORT).show();

        switch (item.getItemId()) {
            case R.id.mapMenu:
                // do something
                break;
            case R.id.favMenu:
                // do something
                break;
            case R.id.listMenu:
                // do something
                break;
            case R.id.settingsMenu:
                // do something
                break;
        }
        return true;
    }
*/
}


