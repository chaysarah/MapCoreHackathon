package com.elbit.mapcore.mcsimpletester;


import android.content.Context;
import android.content.res.AssetManager;
import android.opengl.GLSurfaceView;
import android.util.Log;
import android.view.MotionEvent;
import android.view.Window;

import com.elbit.mapcore.Interfaces.Map.IMcNativeStaticObjectsMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcStaticObjectsMapLayer;
import com.elbit.mapcore.Utils.IMcGLDeviceCamera;
import com.elbit.mapcore.Utils.IMcLocationUpdates;
import com.elbit.mapcore.Utils.IMcTelemetryUpdates;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.Random;

import javax.microedition.khronos.opengles.GL10;

import com.elbit.mapcore.General.IMcErrors;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Calculations.GridCoordSystem.IMcGridCoordSystemGeographic;
import com.elbit.mapcore.Interfaces.Calculations.GridCoordSystem.IMcGridCoordinateSystem;
import com.elbit.mapcore.Interfaces.Map.IMcMapCamera;
import com.elbit.mapcore.Interfaces.Map.IMcMapDevice;
import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcMapTerrain;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.Map.IMcNativeDtmMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcNativeRasterMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcRasterMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcRawVectorMapLayer;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcConditionalSelector;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcImageFileTexture;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcLogFont;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcMemoryBufferTexture;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectScheme;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlay;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;
import com.elbit.mapcore.Interfaces.General.IMcEditMode;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcTexture;
import com.elbit.mapcore.Structs.SMcBColor;
import com.elbit.mapcore.Structs.SMcBox;
import com.elbit.mapcore.Structs.SMcFileSource;
import com.elbit.mapcore.Structs.SMcPoint;
import com.elbit.mapcore.Structs.SMcVariantLogFont;
import com.elbit.mapcore.Structs.SMcVariantString;
import com.elbit.mapcore.Structs.SMcVector3D;
import static com.elbit.mapcore.Interfaces.General.IMcEditMode.ECursorType.ECT_DEFAULT_CURSOR;


public class McGLSurfaceViewRenderer implements GLSurfaceView.Renderer,IMcTelemetryUpdates.IMcTelemetryUpdatesListener, IMcLocationUpdates.IMcLocationUpdatesListener {

    private final static String TAG = "SimpleTester";
    private McGLSurfaceView mView;
    private IMcMapDevice m_pMapDevice;
    private IMcGridCoordinateSystem m_pGridCoordinateSystem;
    private IMcRawVectorMapLayer m_pRawVectorMapLayer;
    private IMcNativeRasterMapLayer m_pNativeRasterLayer;
    private IMcNativeStaticObjectsMapLayer m_pSOLMapLayer;
    private IMcNativeDtmMapLayer m_pNativeDtmLayer;
    private IMcMapTerrain m_pTerrain;
    public IMcMapViewport m_pViewport;
    private IMcMapViewport m_p2DViewport;
    private IMcMapViewport m_p3DViewport;
    private IMcMapViewport m_pARViewport;
    private IMcObject m_CameraOrientationText;
    private IMcOverlayManager m_pOverlayManager;
    private IMcEditMode m_pEditMode;
    private IMcOverlay m_pOverlay;
    private int m_nWidth;
    private int m_nHeight;
    private IMcMemoryBufferTexture m_PreviewTexture;
    private Context m_Context;
    private IMcObject mTextObject;
    private int tmp;
    private EDisplayType m_eDisplayType;
    public static float m_fYaw;
    public static float m_fPitch;
    public static float m_fRoll;

    //  private MainRenderer m_Renderer;

    public void Render() {
        try {
            if (m_pViewport != null) {
                //               Log.d(TAG,"m_pViewport.Render() , Thread ID:" + String.valueOf(android.os.Process.myTid()));
                m_pViewport.SetBackgroundColor(SMcBColor.bcWhiteOpaque);
                m_pViewport.Render();
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public float GetCameraScale() {
        float fCameraScale = 1;
        try {
            fCameraScale = m_pViewport.GetCameraScale();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return fCameraScale;
    }

    public void SetCameraScale(float fScale) {
        try {
            m_pViewport.SetCameraScale(fScale);

        } catch (Exception e) {
            e.printStackTrace();
        }
    }


    private void CreateMapDevice() throws Exception {
        IMcMapDevice.SInitParams InitParams = new IMcMapDevice.SInitParams();
        InitParams.eLoggingLevel = IMcMapDevice.ELoggingLevel.ELL_MEDIUM;
        InitParams.strConfigFilesDirectory = "//sdcard/MapCore";
        InitParams.strLogFileDirectory = "//sdcard/MapCore";
        InitParams.uNumBackgroundThreads = 1;
        m_pMapDevice = IMcMapDevice.Static.Create(InitParams);

    }

    private void CreateGridCoordinates() throws MapCoreException {
        IMcGridCoordSystemGeographic gridGeo = null;
        IMcGridCoordinateSystem.SDatumParams pDatumParams = new IMcGridCoordinateSystem.SDatumParams();
        m_pGridCoordinateSystem = IMcGridCoordSystemGeographic.Static.Create(IMcGridCoordinateSystem.EDatumType.EDT_WGS84, pDatumParams);

    }

    private void CreateNativeRasterLayer() throws Exception {
        String StrDir = "/sdcard/MapCore/NetanyaGeoWgs84/RasterEtc";
        m_pNativeRasterLayer = IMcNativeRasterMapLayer.Static.Create(StrDir);

    }

    private void CreateSOLLayer() throws Exception {
        String StrDir = "/sdcard/MapCore/NetanyaGeoWgs84/BuildingsSol";
        m_pSOLMapLayer = IMcNativeStaticObjectsMapLayer.Static.Create(StrDir);

    }


    private void CreateNativeDtmLayer() throws Exception {
        String StrDir = "123";

        m_pNativeDtmLayer = IMcNativeDtmMapLayer.Static.Create(StrDir);

    }

    private void CreateMapTerrain() throws Exception {

        IMcMapLayer[] apLayers = new IMcMapLayer[1];
     //   apLayers[0] = m_pNativeRasterLayer;
        apLayers[0] = m_pSOLMapLayer;

        //      apLayers[1] = m_pRawVectorMapLayer;
        m_pTerrain = IMcMapTerrain.Static.Create(m_pGridCoordinateSystem, apLayers);

    }

    public void InitViewportSize(int nWidth, int nHeight) {
        m_nHeight = nHeight;
        m_nWidth = nWidth;

    }

    public void SetViewportSize(int nWidth, int nHeight) {
        m_nHeight = nHeight;
        m_nWidth = nWidth;

        if (m_pViewport != null) {
            try {
                m_pViewport.ViewportResized(m_nWidth, m_nHeight);
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    public void CreateViewport() {

  //      if (m_pViewport != null) {
  //          return;
  //      }
        IMcMapViewport.SCreateData createData = new IMcMapViewport.SCreateData(IMcMapCamera.EMapType.EMT_2D);

        createData.bFullScreen = false;
        createData.bShowGeoInMetricProportion = false;
        createData.pCoordinateSystem = m_pGridCoordinateSystem;
        createData.pDevice = m_pMapDevice;
        createData.hWnd = -1;
        createData.bExternalGLControl = true;
        //  createData.uWidth = 2000;
        //  createData.uHeight = 2000;

        createData.uWidth = m_nWidth;
        createData.uHeight = m_nHeight;
        createData.pOverlayManager = m_pOverlayManager;
        ///       IMcMapCamera pCamera=null;

        IMcMapTerrain[] apTerrains = new IMcMapTerrain[1];
        apTerrains[0] = m_pTerrain;
        try {
            //        Log.d(TAG,"mc- IMcMapViewport.Static.Create() , Thread ID:" + String.valueOf(android.os.Process.myTid()));
            createData.eMapType = IMcMapCamera.EMapType.EMT_2D;
            m_p2DViewport = IMcMapViewport.Static.Create(null, createData, apTerrains);
            createData.eMapType = IMcMapCamera.EMapType.EMT_3D;
            m_p3DViewport = IMcMapViewport.Static.Create(null, createData, apTerrains);
            m_pARViewport = IMcMapViewport.Static.Create(null, createData, null);

            if (m_eDisplayType == EDisplayType.EDT_2D) {
                m_pViewport = m_p2DViewport;
                m_pViewport.SetCameraScale(3);

            } else if (m_eDisplayType == EDisplayType.EDT_3D) {
                m_pViewport = m_p3DViewport;
            } else if (m_eDisplayType == EDisplayType.EDT_AR) {
                m_pViewport = m_pARViewport;

            }

     ///       m_pViewport.SetEnableColorTableImageProcessing(m_pNativeRasterLayer,true);
     ///       byte [] Bytes  = new byte[256];
     ///       m_pViewport.SetUserColorValues(m_pNativeRasterLayer, IMcImageProcessing.EColorChannel.ECC_GREEN,Bytes,true);
   //         m_pViewport.SetUserColorValues(m_pNativeRasterLayer, IMcImageProcessing.EColorChannel.ECC_BLUE,Bytes,true);

///Tsahi SMcVector3D Position = new SMcVector3D(34.8645096*100000,32.2877197*100000,40);
///Tsahi m_pViewport.SetCameraPosition(Position);


            ////           m_pViewport.SetCameraScale(3);
        } catch (Exception e) {
            e.printStackTrace();
        }


    }

    private void CreateOverlayManager() throws Exception {
        m_pOverlayManager = IMcOverlayManager.Static.Create(m_pGridCoordinateSystem);
    }

    public void CreateEditMode()  throws Exception {
        m_pEditMode = IMcEditMode.Static.Create(m_pViewport);
        m_pEditMode.StartNavigateMap(false);
    }

    private void CreateOverlay() throws Exception {
        m_pOverlay = IMcOverlay.Static.Create(m_pOverlayManager);
    }


    McGLSurfaceViewRenderer(McGLSurfaceView view, Context context) {
        //           try {
        m_Context = context;
        mView = view;

        IMcLocationUpdates.Create(m_Context, this);
        IMcTelemetryUpdates.Create(m_Context, this);

        ///    m_Renderer = new MainRenderer(m_Context);
        //         } catch (Exception ex) {
        //            String str = ex.getMessage();
        //         }

    }

    @Override
    public void onSurfaceCreated(GL10 gl, javax.microedition.khronos.egl.EGLConfig config) {

        try {

            //           Log.d(TAG,"mc- Init Map (device,terrain,Overlay) , Thread ID:" + String.valueOf(android.os.Process.myTid()));
            if (m_pViewport == null)
            {
                CreateMapDevice();

                CreateGridCoordinates();
                CreateNativeRasterLayer();
                CreateSOLLayer();
                CreateMapTerrain();
                CreateOverlayManager();
                CreateOverlay();
                m_eDisplayType = EDisplayType.EDT_2D;
                CreateViewport();
    ///            CreateEditMode();
                //         CreateLineObject();
    ///            CreatePicObject();
    ///            CreateWorldPicObject();
                CreateTelemetryText();
        //        CreateLineObject();
            }
      /*      else
            {
                m_pViewport.RemoveTerrain(m_pTerrain);
                CreateNativeRasterLayer();
                CreateMapTerrain();
                m_pViewport.AddTerrain(m_pTerrain);
            }*/

///            m_Renderer.SurfaceCreated();
             //         CreateCameraPreviewObject(m_pOverlayManager,m_pOverlay);

               IMcGLDeviceCamera.Create(m_Context);


        } catch (Exception e) {
            e.printStackTrace();
        }

    }

    public void onResume() {
        //      if (m_Renderer !=null)
        //      {
        //         m_Renderer.onResume();
        //     }
        IMcGLDeviceCamera.onResume();
        IMcLocationUpdates.onResume();
        IMcTelemetryUpdates.onResume();

    }

    public void onPause() {
        //       if (m_Renderer !=null)
        //       {
        //           m_Renderer.onPause();
        //       }
        IMcGLDeviceCamera.onPause();
        IMcLocationUpdates.onPause();
        IMcTelemetryUpdates.onPause();

    }


    public void onDrawFrame(GL10 unused) {

        if (m_eDisplayType == EDisplayType.EDT_AR) {
            IMcGLDeviceCamera.Render();
        }
        if ((m_pViewport != null) && (m_eDisplayType != EDisplayType.EDT_2D)) {
            try {
     //           m_pViewport.SetCameraOrientation(m_fYaw, m_fPitch, m_fRoll);
            } catch (Exception e) {
                e.printStackTrace();
            }
        }

        Render();


    }


    public void onSurfaceChanged(GL10 unused, int width, int height) {
        //       GLES20.glViewport(0, 0, width, height);
    }

    public void CreatePicObject() throws Exception {
        IMcObjectScheme[] LoadedSchemes = LoadObjectSchemes("Schemes/OnePicScm.m");

        SMcVector3D[] Locs = new SMcVector3D[1];
        SMcVector3D Pos = new SMcVector3D(200, 200, 0.0D);
        Locs[0] = Pos;
        //      Log.d(TAG,"mc- IMcObject.Static.Create() , Thread ID:" + String.valueOf(android.os.Process.myTid()));
        IMcObject PicObject = IMcObject.Static.Create(m_pOverlay, LoadedSchemes[0], Locs);

        //    PicObject.SetTextureProperty(1, m_PreviewTexture);


    }

    public void CreateWorldPicObject() throws Exception {
        IMcObjectScheme[] LoadedSchemes = LoadObjectSchemes("Schemes/OnePicScmWorld.m");

        SMcVector3D[] Locs = new SMcVector3D[1];
 /*       SMcVector3D Pos = new SMcVector3D(34.8640356 * 100000 , 32.2872633 * 100000, 40);
        Locs[0] = Pos;
        IMcObject PicObject = IMcObject.Static.Create(m_pOverlay, LoadedSchemes[0], Locs);

        Locs[0] = new SMcVector3D(34.8640456 * 100000 , 32.2872733 * 100000, 40);
        Overlay.IMcObject.Static.Create(m_pOverlay, LoadedSchemes[0], Locs);

        Locs[0] = new SMcVector3D(34.8640356 * 100000 , 32.2872733 * 100000, 40);
        Overlay.IMcObject.Static.Create(m_pOverlay, LoadedSchemes[0], Locs);

        Locs[0] = new SMcVector3D(34.8640356 * 100000 , 32.2872633 * 100000, 40);
        Overlay.IMcObject.Static.Create(m_pOverlay, LoadedSchemes[0], Locs);
*/
        Locs[0] = new SMcVector3D(34.864092 * 100000, 32.287991 * 100000, 40);
        IMcObject.Static.Create(m_pOverlay, LoadedSchemes[0], Locs);

        //    PicObject.SetTextureProperty(1, m_PreviewTexture);


        //      Render();
    }

    public void CreateLineObject() throws Exception {
        IMcObjectScheme[] LoadedSchemes = LoadObjectSchemes("Schemes/MapScaleScm15.m");


        SMcVector3D[] Locs = new SMcVector3D[2];
    //    SMcVector3D Pos1 = m_p2DViewport.GetCameraPosition();
    //    SMcVector3D Pos2 = new SMcVector3D (Pos1.x+1000 , Pos1.y+1000, Pos1.z);
        SMcVector3D Pos1 = new SMcVector3D(600, 600, 0.0D);
        SMcVector3D Pos2 = new SMcVector3D(1000, 1000, 0.0D);
        Locs[0] = Pos1;
        Locs[1] = Pos2;
        //     Log.d(TAG,"mc- IMcObject.Static.Create() , Thread ID:" + String.valueOf(android.os.Process.myTid()));
        IMcObject LineObject = IMcObject.Static.Create(m_pOverlay, LoadedSchemes[0], Locs);

        //    PicObject.SetTextureProperty(1, m_PreviewTexture);


        //      Render();
    }

    public void CreateTelemetryText() throws Exception {

        IMcLogFont.SLogFontToTtfFile[] aLogFontsMap = new IMcLogFont.SLogFontToTtfFile[1];
        aLogFontsMap[0] = new IMcLogFont.SLogFontToTtfFile();
        aLogFontsMap[0].LogFont = new SMcVariantLogFont();
        aLogFontsMap[0].LogFont.LogFont = new SMcVariantLogFont.LOGFONT();

        aLogFontsMap[0].LogFont.LogFont.lfItalic = 0;
        aLogFontsMap[0].LogFont.LogFont.lfUnderline = 0;
        aLogFontsMap[0].LogFont.LogFont.lfHeight = 400;
        aLogFontsMap[0].LogFont.LogFont.lfFaceName = "Microsoft Sans Serif";
        aLogFontsMap[0].strTtfFileFullPathName = "/system/fonts/Roboto-Regular.ttf";
        //       IMcLogFont.Static.SetLogFontToTtfFileMap(aLogFontsMap);
        IMcObjectScheme[] LoadedSchemes = LoadObjectSchemes("Schemes/OneTextScm.m");

        SMcVector3D[] Locs = new SMcVector3D[1];
        //       SMcVector3D Pos = new SMcVector3D(150, 150, 0.0D);
        SMcVector3D Pos = new SMcVector3D(300, 300, 0.0D);
        Locs[0] = Pos;
        if (m_CameraOrientationText != null) {
            m_CameraOrientationText.Remove();
        }
        m_CameraOrientationText = IMcObject.Static.Create(m_pOverlay, LoadedSchemes[0], Locs);
        //SMcVariantString Str = new SMcVariantString("",false);
        //m_CameraOrientationText.SetStringProperty(1, Str);
        DisplayTelemetryText();


    }

    private void DisplayTelemetryText() {
        try {
            SMcVariantString Str = new SMcVariantString("", false);
            m_CameraOrientationText.SetStringProperty(1, Str);

             if (m_CameraOrientationText != null) {
                  Float fYaw = 0.0f;
                Float fPitch = 0.0f;
                Float fRoll = 0.0f;
                m_pViewport.GetCameraOrientation(fYaw, fPitch, fRoll);
                String StrTelemetry = String.format("Yaw:%5.0f Pitch:%5.0f Roll:%5.0f", fYaw, fPitch, fRoll);

                 Str = new SMcVariantString(StrTelemetry, true);

                m_CameraOrientationText.SetStringProperty(1, Str);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

    }
    private IMcObjectScheme[] LoadObjectSchemes(String SchemePath)
    {
        AssetManager assetManager = m_Context.getAssets();
        InputStream SchemeBuffer = null;
        IMcObjectScheme[] LoadedSchemes = null;
        try {
            SchemeBuffer = assetManager.open( SchemePath);
            byte [] Buffer = readBytes(SchemeBuffer);
            LoadedSchemes = m_pOverlayManager.LoadObjectSchemes(Buffer);
         } catch (MapCoreException e) {
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return LoadedSchemes;
    }

    private byte[] ReadImageBytes(String ImagePath)
    {
        AssetManager assetManager = m_Context.getAssets();
        InputStream ImageBuffer = null;
        byte [] Buffer = null;
        try {
            ImageBuffer = assetManager.open( ImagePath);
            Buffer = readBytes(ImageBuffer);
         } catch (Exception e) {
            e.printStackTrace();
        }
        return Buffer;
    }
    @Override
    public void OnTelemetryUpdate(float fYaw, float fPitch, float fRoll) {
        Log.d(TAG, " Yaw :" + fYaw + " Pitch :" + fPitch + " Roll :" + fRoll);
        McGLSurfaceViewRenderer.m_fYaw = fYaw;
        McGLSurfaceViewRenderer.m_fPitch = fPitch;
        McGLSurfaceViewRenderer.m_fRoll = fRoll;
        mView.queueEvent(new Runnable() {
            @Override
            public void run() {
                DisplayTelemetryText();
             //   float fScale = mRenderer.GetCameraScale();
            //    mRenderer.SetCameraScale(fScale/2);
            }});
 //       DisplayTelemetryText();
    }

    @Override
    public void OnLocationUpdate(double Lon, double Lat) {
        Log.d("loc", "OnLocationUpdate Lon: " + Lon + " Lat:" + Lat);
    }

    public byte[] readBytes(InputStream inputStream)  {
        // this dynamically extends to take the bytes you read
        ByteArrayOutputStream byteBuffer = new ByteArrayOutputStream();

        // this is storage overwritten on each iteration with bytes
        int bufferSize = 1024;
        byte[] buffer = new byte[bufferSize];

        // we need to know how may bytes were read to write them to the byteBuffer
        int len = 0;
        try {
            while ((len = inputStream.read(buffer)) != -1) {
                byteBuffer.write(buffer, 0, len);
            }
        } catch (IOException e) {
            e.printStackTrace();
        }

        // and then we can return your byte array.
        return byteBuffer.toByteArray();
    }

    public void ChangeViewport(EDisplayType eDisplayType) {
        try {
            m_eDisplayType = eDisplayType;
            if (eDisplayType == EDisplayType.EDT_2D) {
                m_pViewport = m_p2DViewport;
     //           m_pViewport.SetCameraOrientation(0, 0, 0);

                //         m_pEditMode = m_p2DEditMode;

            } else if (eDisplayType == EDisplayType.EDT_3D) {
                m_pViewport = m_p3DViewport;
                CreateEditMode();
            //    m_pEditMode = m_p3DEditMode;

                SMcVector3D CameraPosition;
                CameraPosition = m_p2DViewport.GetCameraPosition();
                CameraPosition.z = 100;
                m_pViewport.SetCameraPosition(CameraPosition);
                CameraPosition.z = 0;
                m_pViewport.SetCameraLookAtPoint(CameraPosition);

            } else if (eDisplayType == EDisplayType.EDT_AR) {
                m_pViewport = m_pARViewport;
                //        m_pEditMode = m_pAREditMode;
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

    }

    void AddObjects() {
        try {
     //       IMcObjectScheme[] Schemes = LoadObjectSchemes("Schemes/ScreenPictureLegScheme.m");

    //       byte [] Bytes = ReadImageBytes("Images/champ.bmp");
    //       IMcMemoryBufferTexture pTexture = IMcMemoryBufferTexture.Static.Create(64,64, IMcTexture.EPixelFormat.EPF_X8R8G8B8, IMcTexture.EUsage.EU_STATIC_WRITE_ONLY_IN_ATLAS,false,Bytes);

            IMcImageFileTexture pTexture = IMcImageFileTexture.Static.Create(new SMcFileSource("//sdcard/MapCore/EditMode/iconVertexRegular.ico"),false);

            IMcObjectScheme[] Schemes = LoadObjectSchemes("Schemes/MemBuf.m");

            IMcObject pObj = null;
            SMcVector3D Center;
            Center = m_pViewport.GetCameraPosition();
            int nRange = 5000;
            Random rand = new Random();
            SMcVector3D aLocationPoints[] = new SMcVector3D[1];
            aLocationPoints[0] = new SMcVector3D();

            for (int i=0 ; i<100 ; i++) {
                int nRand = rand.nextInt(nRange);
                aLocationPoints[0].x = nRand + (int) (Center.x - nRange / 2);
                nRand = rand.nextInt(nRange);
                aLocationPoints[0].y = nRand + (int) (Center.y  - nRange / 2) ;
                aLocationPoints[0].z = 0;

                pObj = IMcObject.Static.Create(m_pOverlay, Schemes[0]);
                pObj.SetLocationPoints(aLocationPoints);
                pObj.SetTextureProperty(1,pTexture);
            }

        }
        catch (Exception e) {
            e.printStackTrace();
        }
    }


    public void TouchEvent(MotionEvent event)
    {

        int eventAction = event.getActionMasked();
        IMcEditMode.EMouseEvent eMouseEvent= null;

        switch (eventAction) {
            case MotionEvent.ACTION_DOWN:
                Log.d(TAG, "MotionEvent.ACTION_DOWN" );
                eMouseEvent = IMcEditMode.EMouseEvent.EME_BUTTON_PRESSED;
                break;
            case MotionEvent.ACTION_UP:
                Log.d(TAG, "MotionEvent.ACTION_UP" );
                eMouseEvent = IMcEditMode.EMouseEvent.EME_BUTTON_RELEASED;
                break;
            case MotionEvent.ACTION_MOVE:
                Log.d(TAG, "MotionEvent.ACTION_MOVE" );
                eMouseEvent = IMcEditMode.EMouseEvent.EME_MOUSE_MOVED_BUTTON_DOWN;
                break;
            case MotionEvent.ACTION_POINTER_DOWN:
                Log.d(TAG, "MotionEvent.ACTION_POINTER_DOWN" );
                eMouseEvent = IMcEditMode.EMouseEvent.EME_SECOND_TOUCH_PRESSED;
                break;
            case MotionEvent.ACTION_POINTER_UP:
                Log.d(TAG, "MotionEvent.ACTION_POINTER_UP" );
                eMouseEvent = IMcEditMode.EMouseEvent.EME_SECOND_TOUCH_RELEASED;
                break;
        }

        int nCount = event.getPointerCount();
        SMcPoint TouchPosition= new SMcPoint( (int)event.getX(0),(int)event.getY(0));
        SMcPoint SecondTouchPosition = null;
        int x=0 , y=0;
        if (nCount > 1) {
            SecondTouchPosition= new SMcPoint( (int)event.getX(1),(int)event.getY(1));
        }
        Boolean bRenderNeeded = new Boolean(false);
        IMcEditMode.ECursorType eCursorType = ECT_DEFAULT_CURSOR;
        try {
            Log.d(TAG, "TouchPosition    x :" + TouchPosition.x + " y :" + TouchPosition.y);
            if (SecondTouchPosition != null) {
                Log.d(TAG, "SecondTouchPosition    x :" + SecondTouchPosition.x + " y :" + SecondTouchPosition.y);
            }
            else
            {
                Log.d(TAG, "No SecondTouchPosition");
            }
            m_pEditMode.OnMouseEvent(eMouseEvent,TouchPosition,false,(short)0,bRenderNeeded,eCursorType,SecondTouchPosition);
 
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

}
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////


