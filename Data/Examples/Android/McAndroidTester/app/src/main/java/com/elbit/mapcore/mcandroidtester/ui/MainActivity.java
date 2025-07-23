package com.elbit.mapcore.mcandroidtester.ui;

import android.Manifest;
import android.app.Activity;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.os.Bundle;
import android.os.Environment;
import android.os.Handler;
import androidx.core.app.ActivityCompat;
import androidx.core.content.ContextCompat;
import androidx.core.view.GravityCompat;
import androidx.drawerlayout.widget.DrawerLayout;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;

import com.elbit.mapcore.Classes.Calculations.GridCoordSystem.McGridCoordSystemGeographic;
import com.elbit.mapcore.Classes.Calculations.GridCoordSystem.McGridGeneric;
import com.elbit.mapcore.Classes.Calculations.GridCoordSystem.McGridNewIsrael;
import com.elbit.mapcore.Classes.Calculations.GridCoordSystem.McGridUTM;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Calculations.GridCoordSystem.IMcGridCoordinateSystem;
import com.elbit.mapcore.Interfaces.Map.IMcMapCamera;
import com.elbit.mapcore.Interfaces.Map.IMcMapDevice;
import com.elbit.mapcore.Interfaces.Map.IMcMapTerrain;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcLogFont;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_AMCTMapForm;
import com.elbit.mapcore.mcandroidtester.managers.ObjectWorld.Manager_MCOverlayManager;
import com.elbit.mapcore.mcandroidtester.model.AMCTFontMapping;
import com.elbit.mapcore.mcandroidtester.model.AMCTMapDevice;
import com.elbit.mapcore.mcandroidtester.model.AMCTMapLayerReadCallback;
import com.elbit.mapcore.mcandroidtester.model.AMCTUserDataFactory;
import com.elbit.mapcore.mcandroidtester.model.AMCTViewPort;
import com.elbit.mapcore.mcandroidtester.model.Automation.AMCTAutomationParams;
import com.elbit.mapcore.mcandroidtester.model.Automation.AMCTGridCoordinateSystem;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.ui.objects.ObjectPropertiesBase;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.OptionalDataException;
import java.io.StreamCorruptedException;

import com.elbit.mapcore.mcandroidtester.utils.BaseApplication;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.google.gson.Gson;


public class MainActivity extends AppCompatActivity
{
    static {
        System.loadLibrary("JniMapCore7_D");
    }

    boolean doubleBackToExitPressedOnce = false;

    @Override
    public void onBackPressed() {
        //super.onBackPressed();
        AlertMessages.ShowYesNoMessage(this, "Exit", "Do you want to close the tester? all your data will be deleted.", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                closeApp();
            }
        });
    }

    private void closeApp() {
        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        if (drawer.isDrawerOpen(GravityCompat.START)) {
            drawer.closeDrawer(GravityCompat.START);
        } else {
            this.finishAffinity();
        }
        int p = android.os.Process.myPid();
        android.os.Process.killProcess(p);
    }

    private void doubleBackToExit() {
        if (doubleBackToExitPressedOnce) {
            DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
            if (drawer.isDrawerOpen(GravityCompat.START)) {
                drawer.closeDrawer(GravityCompat.START);
            } else {
                this.finishAffinity();
                // super.onBackPressed();
            }
            // super.onBackPressed();
            int p = android.os.Process.myPid();
            android.os.Process.killProcess(p);
            return;
        }

        this.doubleBackToExitPressedOnce = true;
        Toast.makeText(this, "Please click BACK again to exit", Toast.LENGTH_SHORT).show();

        new Handler().postDelayed(new Runnable() {

            @Override
            public void run() {
                doubleBackToExitPressedOnce = false;
            }
        }, 2000);
    }


    @Override
    protected void onCreate(Bundle savedInstanceState) {

        BaseApplication.setMainActivity(this);

        if (ContextCompat.checkSelfPermission(this, Manifest.permission.READ_EXTERNAL_STORAGE) != PackageManager.PERMISSION_GRANTED) {
            ActivityCompat.requestPermissions(this, new String[]{Manifest.permission.READ_EXTERNAL_STORAGE}, 0);
        }
        if (ContextCompat.checkSelfPermission(this, Manifest.permission.WRITE_EXTERNAL_STORAGE) != PackageManager.PERMISSION_GRANTED) {
            ActivityCompat.requestPermissions(this, new String[]{Manifest.permission.WRITE_EXTERNAL_STORAGE}, 0);
        }

        if (ContextCompat.checkSelfPermission(this, Manifest.permission.SYSTEM_ALERT_WINDOW) != PackageManager.PERMISSION_GRANTED) {
            ActivityCompat.requestPermissions(this, new String[]{Manifest.permission.SYSTEM_ALERT_WINDOW}, 0);
        }

        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        try {

            String version = IMcMapDevice.Static.GetVersion();
            setTitle("Maps Container ( " + version + ")");
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(this, e, "GetVersion");
        } catch (Exception e) {
            e.printStackTrace();
        }

        //AutoRender("/sdcard/MapCore_Vtest/3D_TileFilter_Sharp_High/FullViewportDataParams_GLES_Master.json false GLES_Android.png");

        Bundle b = getIntent().getExtras();
        if(b != null) {
            String jsonParams = b.getString("jsonParams");
            AutoRender(jsonParams);
        }
        getMappingFromFile();

      /*  try {
            //AutoRenderFromFile("/sdcard/maps/temp/5/FullViewportDataParams_GLES_Master.json", "");
            //AutoRenderFromFile("/sdcard/MapCore_Vtest/2D_DTMVis_1/FullViewportDataParams_GLES_Master.json","");
            AutoRenderFromFile("/sdcard/MapCore_Vtest/2D_DTMVis_1/FullViewportDataParams_GLES_Master.json",false,"a.bmp");

        } catch (IOException e) {
            e.printStackTrace();
        }*/
    }

    private void AutoRender(String jsonParams) {
        String jsonfile = "";
        String printFileName = "";
        boolean isShowMsg = false;

        if (jsonParams != null) {

            String args[] = jsonParams.split(" ");

            if (args.length == 3) {
                jsonfile = args[0];

                if (new File(jsonfile).exists()) {
                    if (args[1].toLowerCase().compareTo(Boolean.TRUE.toString().toLowerCase()) == 0 ||
                            args[1].toLowerCase().compareTo(Boolean.FALSE.toString().toLowerCase()) == 0)
                        isShowMsg = Boolean.parseBoolean(args[1]);
                    else {
                        AutoRenderInvalidParamsMsg(jsonParams, "missing 'isShowMsg' param");
                        return;
                    }
                    printFileName = args[2];
                    try {
                        AutoRenderFromFile(jsonfile, isShowMsg, printFileName);
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                }
                else
                {
                    AutoRenderInvalidParamsMsg(jsonParams, "json file not exist");
                    return;
                }

            } else {
                AutoRenderInvalidParamsMsg(jsonParams, "missing param");
                return;
            }
        }
    }

    private void AutoRenderInvalidParamsMsg(String jsonParams, String msg)
    {
        AlertMessages.ShowMessage(this, "Auto Render : Invalid input parameters", msg + ", params = " + jsonParams);
    }

    private void AutoRenderFromFile(String jsonFilePath, boolean isShowMsg, String printNameFile) throws IOException {
        Gson gson = new Gson();
        StringBuilder text2 = new StringBuilder();

        File sdcard = Environment.getExternalStorageDirectory();

        File jsonFile = new File(jsonFilePath);
        if (jsonFile.exists()) {
            //Get the text file
            String folderPath = jsonFile.getParentFile().toString();
            String printPath = "";
            if (!printNameFile.isEmpty()) {
                printPath = new File(folderPath, printNameFile).toString();
            }
            BufferedReader br2 = new BufferedReader(new FileReader(jsonFile));
            String line2;

            while ((line2 = br2.readLine()) != null) {
                text2.append(line2);
            }
            br2.close();

            AMCTAutomationParams automationParams = gson.fromJson(text2.toString(), AMCTAutomationParams.class);
            String maps = "";
            if(automationParams.MapsBaseDirectory != null)
                maps = automationParams.MapsBaseDirectory.Android;
            if(maps == null || maps.isEmpty())
                maps = sdcard.getPath() + "/maps/";
            File fileMaps = new File(maps);

            //Date currentTime = Calendar.getInstance().getTime();
            //String logFileName = "log_" + currentTime.toString()+ ".txt";
            File log_file = new File(folderPath, AlertMessages.AutoRender_FileNameLog);
            if(log_file.exists()) log_file.delete();
            log_file.createNewFile();

            // this actions done outside (from test ui)
            //File failed_file = new File(folderPath, AlertMessages.AutoRender_FileNameFailed);
            //if(failed_file.exists()) failed_file.delete();

            //File success_file = new File(folderPath, AlertMessages.AutoRender_FileNameSuccess);
            //if(success_file.exists()) success_file.delete();

            int width = automationParams.MapViewport.ViewportSize.ViewportWidth;
            int height =  automationParams.MapViewport.ViewportSize.ViewportHeight;
            int uViewportID = automationParams.MapViewport.ViewportID;
            IMcMapCamera.EMapType eMapType = IMcMapCamera.EMapType.valueOf(ConvertEnumValue(automationParams.MapViewport.ViewportMapType));
            IMcMapViewport.SCreateData createData = new IMcMapViewport.SCreateData(eMapType);
            createData.uViewportID = uViewportID;
            createData.uWidth = width;
            createData.uHeight = height;
            createData.pGrid = null;
            createData.bShowGeoInMetricProportion = automationParams.MapViewport.ViewportShowGeoInMetricProportion;
            createData.pImageCalc = null;
            createData.hWnd = -1;

            AMCTMapDevice MapDevice = automationParams.MapDevice;

            if (MapDevice.getConfigFilesDirectory() == null || MapDevice.getConfigFilesDirectory().isEmpty())
            {
                MapDevice.setConfigFilesDirectory(sdcard.getPath() + "/MapCore");
            }
            if (MapDevice.getLogFileDirectory() == null || MapDevice.getLogFileDirectory().isEmpty())
            {
                MapDevice.setLogFileDirectory(folderPath);
                MapDevice.setLoggingLevel(IMcMapDevice.ELoggingLevel.ELL_HIGH);
            }

            createData.pDevice = MapDevice.CreateDevice();

            if (createData.pDevice == null)
                return;

            createData.pCoordinateSystem = CreateGridCoordinateSystem(automationParams.MapViewport.GridCoordinateSystem, this);
            if (createData.pCoordinateSystem == null) {
                AlertMessages.AutomationFinish(log_file, folderPath,isShowMsg,this,"Load Map From File : Create grid coordinate system of viewport" , "Invalid MapViewport.GridCoordinateSystem");
                return;
            }
            if (automationParams.MapViewport.Terrains != null) {
                // Read number of terrains
                int numTerrains = automationParams.MapViewport.Terrains.size();
                IMcMapTerrain[] terrainArr = null;
                try {
                    // Collect terrain array
                    if (numTerrains > 0) {
                        terrainArr = new IMcMapTerrain[numTerrains];
                        AMCTUserDataFactory UDF = new AMCTUserDataFactory();

                        for (int i = 0; i < numTerrains; i++) {
                            String folderName = automationParams.MapViewport.Terrains.get(i);

                            if (!new File(folderName).isAbsolute())
                                folderName = new File(folderPath, folderName).toString();
                            terrainArr[i] = IMcMapTerrain.Static.Load(folderName, fileMaps.toString(), UDF);
                            AMCTViewPort.getViewportInCreation().addTerrainToList(terrainArr[i]);
                        }
                    }
                } catch (MapCoreException McEx) {
                    McEx.printStackTrace();
                    AlertMessages.AutomationFinish(log_file, folderPath,isShowMsg,this,"Load Map From File : IMcMapTerrain.Static.Load()" ,McEx);
                    return;
                } catch (Exception McEx) {
                    McEx.printStackTrace();
                    AlertMessages.AutomationFinish(log_file, folderPath,isShowMsg,this,"Load Map From File : IMcMapTerrain.Static.Load()" ,McEx);
                    return;
                }
            }

            // om
            IMcGridCoordinateSystem overlayManagerCoordSys = CreateGridCoordinateSystem(automationParams.OverlayManager.GridCoordinateSystem, this);
            if (overlayManagerCoordSys == null) {
                AlertMessages.AutomationFinish(log_file, folderPath ,isShowMsg, this, "Load Map From File : Create grid coordinate system of overlay manager", "Invalid OverlayManager.GridCoordinateSystem");
                return;
            }

            createData.pOverlayManager = Manager_MCOverlayManager.getInstance().CreateOverlayManager(overlayManagerCoordSys, false);

            AMCTViewPort.getViewportInCreation().setSCreateData(createData);
            AMCTViewPort.getViewportInCreation().setIsAutomation(true);
            AMCTViewPort.getViewportInCreation().setAutomationParams(automationParams);
            AMCTViewPort.getViewportInCreation().setAutomationPrintViewportPath(printPath);
            AMCTViewPort.getViewportInCreation().setJsonFolderPath(folderPath);
            AMCTViewPort.getViewportInCreation().setAutomationLogFile(log_file);
            AMCTViewPort.getViewportInCreation().setAutomationIsShowMsg(isShowMsg);

            startMapContainerActivity();
        }
    }

    public static String ConvertEnumValue(String dnEnumVal)
    {
        if(!dnEnumVal.isEmpty())
        {
            return dnEnumVal.substring(1);
        }
        return dnEnumVal;
    }

    public static IMcGridCoordinateSystem CreateGridCoordinateSystem(AMCTGridCoordinateSystem gridCoordinateSystem, Context context) {

        String strEnumValType = ConvertEnumValue(gridCoordinateSystem.GridCoordinateSystemType);
        String strEnumValDatum = ConvertEnumValue(gridCoordinateSystem.Datum);

        IMcGridCoordinateSystem.EGridCoordSystemType selectedCoordSys = IMcGridCoordinateSystem.EGridCoordSystemType.valueOf(strEnumValType);
        IMcGridCoordinateSystem.EDatumType datum = IMcGridCoordinateSystem.EDatumType.valueOf(strEnumValDatum);

        switch (selectedCoordSys) {
            case EGCS_GEOGRAPHIC:
                try
                {
                    return McGridCoordSystemGeographic.Static.Create(datum);
                }
                catch (MapCoreException McEx)
                {
                    AlertMessages.ShowMapCoreErrorMessage(context, McEx, "McGridCoordSystemGeographic.Static.Create");
                }

                break;
            case EGCS_UTM:
                try
                {
                    return McGridUTM.Static.Create(gridCoordinateSystem.Zone, datum);
                }
                catch (MapCoreException McEx)
                {
                    AlertMessages.ShowMapCoreErrorMessage(context, McEx, "McGridUTM.Static.Create");
                }
                break;
            case EGCS_GENERIC_GRID:
                try
                {
                    if (gridCoordinateSystem.EpsgCode != null && gridCoordinateSystem.EpsgCode != "")
                        return McGridGeneric.Static.Create(gridCoordinateSystem.EpsgCode);
                    else
                        return null;
                }
                catch (MapCoreException McEx)
                {
                    AlertMessages.ShowMapCoreErrorMessage(context, McEx, "McGridGeneric.Static.Create");
                } catch (Exception e) {
                    e.printStackTrace();
                }

                break;
            case EGCS_NEW_ISRAEL:
                try
                {
                    return McGridNewIsrael.Static.Create();
                }
                catch (MapCoreException McEx)
                {
                    AlertMessages.ShowMapCoreErrorMessage(context, McEx, "");
                } catch (Exception e) {
                    e.printStackTrace();
                }

                break;

            default:
                AlertMessages.ShowErrorMessage(context, "Viewport coordinate system not supported", "Current viewport coordinate system not supported in load viewport");

                return null;

        }
        return null;
    }

    private IMcLogFont.SLogFontToTtfFile[] getMappingFromFile()
    {
        FileInputStream fis = null;
        try {
            fis = this.openFileInput("fontMapping");
            ObjectInputStream is = new ObjectInputStream(fis);
            AMCTFontMapping fontMapping = (AMCTFontMapping) is.readObject();
            ObjectPropertiesBase.Text.getInstance().mTextCurMappingPos = fontMapping.getSelectedMappingPos();
            IMcLogFont.SLogFontToTtfFile[] savedMappingArr = fontMapping.getLogFontToTtfFromSavedMapping();
            ObjectPropertiesBase.Text.getInstance().mTextFontsMap = savedMappingArr;
            if (ObjectPropertiesBase.Text.getInstance().mTextCurMappingPos != -1)
                ObjectPropertiesBase.Text.getInstance().mTextCurMapping = savedMappingArr[ObjectPropertiesBase.Text.getInstance().mTextCurMappingPos];
            final Activity activity = this;
            Funcs.runMapCoreFunc(new Runnable() {
                @Override
                public void run() {
                    try{
                        IMcLogFont.Static.SetLogFontToTtfFileMap(ObjectPropertiesBase.Text.getInstance().mTextFontsMap);
                    }
                    catch (MapCoreException e) {
                        AlertMessages.ShowMapCoreErrorMessage(activity, e, "SetLogFontToTtfFileMap");
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            });
            is.close();
            fis.close();
            return savedMappingArr;
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        } catch (OptionalDataException e) {
            e.printStackTrace();
        } catch (StreamCorruptedException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        } catch (ClassNotFoundException e) {
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return null;
    }

    private void setBttnsVisibility() {
        if (AMCTMapDevice.getInstance().getDevice() == null) {
            findViewById(R.id.create_map_bttn).setEnabled(false);
        } else {
            findViewById(R.id.create_map_bttn).setEnabled(true);
            ((Button) findViewById(R.id.device_bttn)).setText("Device (created)");
        }
    }

    @Override
    protected void onResume() {
        super.onResume();
        setBttnsVisibility();
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_open_map_container) {
            if (Manager_AMCTMapForm.getInstance().isAnyViewportExist())
                startMapContainerActivity();
            else
                Toast.makeText(this, "no viewport created", Toast.LENGTH_LONG).show();
            return true;
        }

        return super.onOptionsItemSelected(item);
    }


    private void startMapContainerActivity() {
        Intent viewPortIntent = new Intent(this, MapsContainerActivity.class);
        viewPortIntent.putExtra("newViewPort",true);
        //viewPortIntent.setFlags(Intent.FLAG_ACTIVITY_REORDER_TO_FRONT);
        startActivity(viewPortIntent);
    }


/*    @SuppressWarnings("StatementWithEmptyBody")
    @Override
    public boolean onNavigationItemSelected(MenuItem item) {
        // Handle navigation view item clicks here.
        int id = item.getItemId();

        if (id == R.id.nav_camera) {
            // Handle the camera action
        } else if (id == R.id.nav_gallery) {

        } else if (id == R.id.nav_slideshow) {

        } else if (id == R.id.nav_manage) {

        } else if (id == R.id.nav_share) {

        } else if (id == R.id.nav_send) {

        }

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        drawer.closeDrawer(GravityCompat.START);
        return true;
    }*/

    public void openDeviceTabs(View view) {
        Intent openDeviceTabsIntent = new Intent(this, com.elbit.mapcore.mcandroidtester.ui.device.activities.DeviceTabsActivity.class);
        startActivity(openDeviceTabsIntent);
    }

    public void openCreateMapTabs(View view) {
        Intent CreateMapIntent = new Intent(this, com.elbit.mapcore.mcandroidtester.ui.map.activities.MapTabsActivity.class);
        startActivity(CreateMapIntent);

    }

    public void createVpWithSeveralLayers(View view) {
        Intent createVpIntent = new Intent(this, com.elbit.mapcore.mcandroidtester.ui.map.activities.ViewPortWithSeveralLayersActivity.class);
        startActivity(createVpIntent);
    }
}