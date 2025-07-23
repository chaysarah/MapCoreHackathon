package com.elbit.mapcore.mcandroidtester.ui.map;

import android.app.Activity;
import android.content.Context;
import android.graphics.SurfaceTexture;
import android.opengl.GLES20;
import android.opengl.GLSurfaceView;

import com.elbit.mapcore.Classes.Map.McMapGrid;
import com.elbit.mapcore.Classes.Map.McMapHeightLines;
import com.elbit.mapcore.Classes.OverlayManager.McOverlay;
import com.elbit.mapcore.General.IMcErrors;
import com.elbit.mapcore.Interfaces.General.McErrors;
import com.elbit.mapcore.Interfaces.Map.IMcImageProcessing;
import com.elbit.mapcore.Interfaces.Map.IMcMapGrid;
import com.elbit.mapcore.Interfaces.Map.IMcMapHeightLines;
import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcRasterMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcSectionMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcLineItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectScheme;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeNode;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcTextItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcTexture;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_AMCTMapForm;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_AMCTViewportBoundingBoxObject;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_MCMapHeightLines;
import com.elbit.mapcore.mcandroidtester.managers.ObjectWorld.Manager_MCOverlayManager;
import com.elbit.mapcore.mcandroidtester.model.AMCTMapForm;
import com.elbit.mapcore.mcandroidtester.model.AMCTTimer;
import com.elbit.mapcore.mcandroidtester.model.AMCTUserDataFactory;
import com.elbit.mapcore.mcandroidtester.model.AMCTViewPort;
import com.elbit.mapcore.mcandroidtester.model.Automation.AMCTGridRegion;
import com.elbit.mapcore.mcandroidtester.model.Automation.AMCTKeyValueImageProcessingData;
import com.elbit.mapcore.mcandroidtester.model.Automation.AMCTKeyValueInteger;
import com.elbit.mapcore.mcandroidtester.model.Automation.AMCTAutomationParams;
import com.elbit.mapcore.mcandroidtester.model.Automation.AMCTImageProcessingChannelData;
import com.elbit.mapcore.mcandroidtester.model.Automation.AMCTImageProcessingData;
import com.elbit.mapcore.mcandroidtester.model.Automation.AMCTMapTerrainData;
import com.elbit.mapcore.mcandroidtester.ui.MainActivity;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.BaseApplication;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

import java.io.File;
import java.util.ArrayList;
import java.util.HashMap;

import javax.microedition.khronos.opengles.GL10;

import com.elbit.mapcore.Classes.OverlayManager.McViewportConditionalSelector;
import com.elbit.mapcore.Enums.EMcPointCoordSystem;
import com.elbit.mapcore.General.CMcEnumBitField;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.General.ObjectRef;
import com.elbit.mapcore.Interfaces.Map.IMcMapCamera;
import com.elbit.mapcore.Interfaces.Map.IMcMapTerrain;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcConditionalSelector;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcLineBasedItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlay;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcRectangleItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcViewportConditionalSelector;
import com.elbit.mapcore.Structs.SMcBColor;
import com.elbit.mapcore.Structs.SMcFVector2D;
import com.elbit.mapcore.Structs.SMcVector2D;
import com.elbit.mapcore.Structs.SMcVector3D;

public class AMcGLSurfaceViewRenderer implements GLSurfaceView.Renderer, SurfaceTexture.OnFrameAvailableListener {
    private static final String TAG = "AMcGLSurfaceViewRenderer";
    private static AMcGLSurfaceView mGlSurfaceView;
    public boolean mNewViewPort=false;

    public IMcMapViewport getM_pViewport() {
        return m_pViewport;
    }

    private IMcMapViewport m_pViewport;
    public int m_nWidth;
    public int m_nHeight;
    private IMcObjectSchemeItem mBoundingBoxScreenRectItem;
    //private IMcObject mBoundingBoxScreenRectObject;
//    private Timer FPS_Timer = new Timer();
    private int FPS_RenderCounter = 1;
    private AMCTTimer FPSTimer = new AMCTTimer();
    AMcGLSurfaceViewRenderer(AMcGLSurfaceView view) {
        mGlSurfaceView = view;
    }

    public void changeViewPort(int viewPortId) {
        try {
            m_pViewport = Manager_AMCTMapForm.getInstance().getViewportById(viewPortId);
            Manager_AMCTMapForm.getInstance().activateMapForm(m_pViewport);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public AMcGLSurfaceViewRenderer() {
    }

    public void Render() {
        try {
            boolean bRenderAll = false;
            //map container activity turns this flag to create new viewport
            if (mNewViewPort) {
                // m_pLastViewport = m_pViewport;
                m_pViewport = null;
                createViewport(mGlSurfaceView.getContext());
                AMCTViewPort.getViewportInCreation().resetCurViewPort();
                mNewViewPort = false;
            }

            //render only if surfaceViewIsVisible
            if (m_pViewport != null && mGlSurfaceView.isShown()) {
                if (FPS_RenderCounter == 1) {
                    FPSTimer.start();
                }
                FPS_RenderCounter++;
                long time = FPSTimer.getMeasureTime();
                if (FPS_RenderCounter >= 50 && time > 0) {
                    FPSTimer.stop();
                    long avg = 1000000000 / (time / 49);
                    mGlSurfaceView.getMapFragment().updateFPSAvg(avg);
                    FPS_RenderCounter = 1;
                    FPSTimer.reset();
                }
                if(Manager_AMCTMapForm.getInstance().getCurMapForm() != null && Manager_AMCTMapForm.getInstance().getCurMapForm().getIsRenderAllMode()) {
                    IMcMapViewport[] mcMapViewports = Manager_AMCTMapForm.getInstance().getCurMapForm().getViewports();
                    if (mcMapViewports != null)
                        IMcMapViewport.Static.Render(mcMapViewports);
                }
                else {
                    m_pViewport.Render();
                }
            }
            else if (m_pViewport == null)
            {
                // clear background using GL context only when there is NO MapCore view-port
                GLES20.glClearColor(0.0f,0.0f,0.0f,1.0f);
                GLES20.glClear(GLES20.GL_COLOR_BUFFER_BIT);
            }

            if (m_pViewport != null &&
                    Manager_AMCTMapForm.getInstance().getCurMapForm().getIsAutomation() &&
                    !Manager_AMCTMapForm.getInstance().getCurMapForm().getAutomationPrintViewportPath().isEmpty()) {
                if (!m_pViewport.HasPendingUpdates()) {
                    AMCTAutomationParams automationParams = Manager_AMCTMapForm.getInstance().getCurMapForm().getAutomationParams();

                    IMcTexture.EPixelFormat pixelFormat = IMcTexture.EPixelFormat.EPF_A8B8G8R8;

                    Manager_AMCTMapForm.getInstance().RenderScreenRectToBuffer(m_pViewport, 0,
                            automationParams.MapViewport.ViewportSize.ViewportWidth,
                            automationParams.MapViewport.ViewportSize.ViewportHeight,
                            new SMcVector2D(0, 0),
                            new SMcVector2D((int) automationParams.MapViewport.ViewportSize.ViewportWidth, (int) automationParams.MapViewport.ViewportSize.ViewportHeight),
                            BaseApplication.getCurrActivityContext(),
                            Manager_AMCTMapForm.getInstance().getCurMapForm().getAutomationPrintViewportPath(),
                            false,
                            pixelFormat);

                    Manager_AMCTMapForm.getInstance().getCurMapForm().setIsAutomation(false);
                    AlertMessages.AutomationFinish(Manager_AMCTMapForm.getInstance().getCurMapForm().getJsonFolderPath(), Manager_AMCTMapForm.getInstance().getCurMapForm().getAutomationLogFile(), BaseApplication.getCurrActivityContext());


                }
            }
        } catch (MapCoreException e) {
            e.printStackTrace();
            AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(),e,"IMcMapViewport.Static.Render");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public static AMcGLSurfaceView getmGlSurfaceView() {
        return mGlSurfaceView;
    }

    public static void setGlSurfaceView(AMcGLSurfaceView GLSurfaceView) {
        mGlSurfaceView = GLSurfaceView;
    }

    public void onDrawFrame(GL10 unused) {
        // all GL rendering code should be inside MapCore library
        Render();
    }

    @Override
    public void onSurfaceCreated(GL10 gl, javax.microedition.khronos.egl.EGLConfig config) {
        try {
            if (AMCTViewPort.getViewportInCreation().getViewport() == null) {
                //running on glThread
                createViewport(mGlSurfaceView.getContext());
            } else if (m_pViewport == null)
                m_pViewport = AMCTViewPort.getViewportInCreation().getViewport();

        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public void onSurfaceChanged(GL10 unused, int width, int height) {
        // Send resize event to MapCore
        try {

            if (Manager_AMCTMapForm.getInstance().getCurMapForm() != null && Manager_AMCTMapForm.getInstance().getCurMapForm().getIsAutomation()) {
            width = Manager_AMCTMapForm.getInstance().getCurMapForm().getAutomationParams().MapViewport.ViewportSize.ViewportWidth;
            height = Manager_AMCTMapForm.getInstance().getCurMapForm().getAutomationParams().MapViewport.ViewportSize.ViewportHeight;
        }

            Manager_AMCTMapForm.getInstance().AllViewportsResize(width, height);
        }
         catch (Exception e) {
            e.printStackTrace();
        }
    }

    public void InitViewportSize(int nWidth, int nHeight) {
        m_nHeight = nHeight;
        m_nWidth = nWidth;
    }

    public void ViewportResized(int nWidth, int nHeight) {
        if (m_nHeight != nHeight || m_nWidth != nWidth)
        {
            m_nHeight = nHeight;
            m_nWidth = nWidth;
        }
    }

    public void createViewport(Context context) {
        AMCTViewPort amctViewport = AMCTViewPort.getViewportInCreation();
        if (m_pViewport != null) {
            return;
        }

        IMcMapViewport.SCreateData createData = null;
        if(AMCTViewPort.getViewportInCreation().getIsAutomation())
            createData = AMCTViewPort.getViewportInCreation().getSCreateData();
        else
            createData = AMCTViewPort.getViewportInCreation().getSCreateData(m_nWidth, m_nHeight);

        boolean isRenderAllMode = amctViewport.getViewportSpace() == AMCTViewPort.ViewportSpace.Other;
        if (isRenderAllMode) {
            createData.TopLeftCornerInWindow = new SMcFVector2D(0f, 0f);
            createData.BottomRightCornerInWindow = new SMcFVector2D(1f, 0.67777f);
        }
        CreateOneViewport(amctViewport, createData, context, isRenderAllMode);

        if (m_pViewport != null && isRenderAllMode) {
            createData.pShareWindowViewport = m_pViewport;
            createData.TopLeftCornerInWindow = new SMcFVector2D(0f, 0.3333f);
            createData.BottomRightCornerInWindow = new SMcFVector2D(1f, 1);
            createData.eMapType = IMcMapCamera.EMapType.EMT_3D;
            CreateOneViewport(amctViewport, createData, context, isRenderAllMode);
        }
        AMCTViewPort.getViewportInCreation().removeAllTerrains();
    }

    private void CreateOneViewport( AMCTViewPort amctViewport, IMcMapViewport.SCreateData createData, Context context , boolean isRenderAllMode) {
        IMcMapViewport mapViewport = null;
        try {
            ObjectRef<IMcMapCamera> camera = new ObjectRef<>();
            IMcMapTerrain[] terrains = amctViewport.getTerrainsAsArr();
            if (terrains != null) {

                if (!amctViewport.getIsSectionMap()) {

                    mapViewport = IMcMapViewport.Static.Create(camera, createData, amctViewport.getTerrainsAsArr());
                } else {
                    mapViewport = IMcSectionMapViewport.Static.CreateSection(camera, createData, amctViewport.getTerrainsAsArr(), amctViewport.getSectionMapPoints());
                }
                if (amctViewport.getIsAutomation() &&
                        amctViewport.getAutomationParams() != null &&
                        amctViewport.getAutomationParams().MapViewport.MapTerrainsData != null &&
                        amctViewport.getAutomationParams().MapViewport.MapTerrainsData.size() > 0) {

                    try {
                        for (int i = 0; i < terrains.length; i++) {
                            if (amctViewport.getAutomationParams().MapViewport.MapTerrainsData.get(i).Key == i) {
                                AMCTMapTerrainData mapTerrainData = amctViewport.getAutomationParams().MapViewport.MapTerrainsData.get(i).Value;
                                mapViewport.SetTerrainDrawPriority(terrains[i], mapTerrainData.DrawPriority);
                                mapViewport.SetTerrainNumCacheTiles(terrains[i], false, mapTerrainData.NumCacheTiles);
                                mapViewport.SetTerrainNumCacheTiles(terrains[i], true, mapTerrainData.NumCacheTilesForStaticObjects);
                            }
                        }
                    }
                    catch (MapCoreException  McEx) {
                        AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(), mGlSurfaceView.getContext(), McEx, "IMcMapViewport.SetTerrainDrawPriority()/SetTerrainNumCacheTiles()");
                        McEx.printStackTrace();
                    } catch (Exception McEx) {
                        McEx.printStackTrace();
                    }
                }
            }

            if (mapViewport != null) {
                AMCTMapForm newMapForm = new AMCTMapForm(mapViewport);
                newMapForm.setIsRenderAllMode(isRenderAllMode);
                newMapForm.createEditMode(mapViewport);
                newMapForm.setAutomationParams(amctViewport.getAutomationParams());
                newMapForm.setIsAutomation(amctViewport.getIsAutomation());
                newMapForm.setAutomationPrintViewportPath(amctViewport.getAutomationPrintViewportPath());
                newMapForm.setJsonFolderPath(amctViewport.getJsonFolderPath());
                newMapForm.setAutomationLogFile(amctViewport.getAutomationLogFile());

                if (createData.pShareWindowViewport != null) {
                    newMapForm.setSecondMapViewport(createData.pShareWindowViewport);
                    AMCTMapForm parentMapForm = Manager_AMCTMapForm.getInstance().getMapFormByVp(createData.pShareWindowViewport);
                    if (parentMapForm != null)
                        parentMapForm.setSecondMapViewport(mapViewport);
                    Manager_AMCTMapForm.getInstance().addMapForm(mapViewport, newMapForm);

                } else {
                    m_pViewport = mapViewport;
                    amctViewport.setViewport(m_pViewport);
                    Manager_AMCTMapForm.getInstance().addMapForm(mapViewport, newMapForm);
                    Manager_AMCTMapForm.getInstance().activateMapForm(mapViewport);
                }

                if (amctViewport.getIsAutomation()) {

                    AMCTAutomationParams automationParams = amctViewport.getAutomationParams();
                    String folderPath = amctViewport.getJsonFolderPath();
                    if (automationParams.OverlayManager.Overlays != null) {
                        // Read number of overlays
                        int numOverlays = automationParams.OverlayManager.Overlays.size();

                        // Collect overlay array
                        try {
                            if (numOverlays > 0) {
                                AMCTUserDataFactory UDF = new AMCTUserDataFactory();

                                for (int i = 0; i < numOverlays; i++) {
                                    String folderName = automationParams.OverlayManager.Overlays.get(0);
                                    if (!new File(folderName).isAbsolute())
                                        folderName = new File(folderPath, folderName).toString();

                                    IMcOverlay overlay = McOverlay.Static.Create(createData.pOverlayManager);
                                    if (i == 0)
                                        Manager_MCOverlayManager.getInstance().updateOverlayManager(createData.pOverlayManager, overlay);
                                    overlay.LoadObjects(folderName, UDF);
                                }
                            }
                        } catch (MapCoreException McEx) {
                            AlertMessages.AutomationFinish(amctViewport.getAutomationLogFile(),amctViewport.getJsonFolderPath(),amctViewport.getAutomationIsShowMsg(),mGlSurfaceView.getContext(), "McOverlay.Static.Create", McEx);
                            McEx.printStackTrace();
                            return;
                        } catch (Exception McEx) {
                            McEx.printStackTrace();
                            AlertMessages.AutomationFinish(amctViewport.getAutomationLogFile(),amctViewport.getJsonFolderPath(),amctViewport.getAutomationIsShowMsg(),mGlSurfaceView.getContext(), "McOverlay.Static.Create", McEx);
                            return;
                        }
                    }

                    try {
                        if (automationParams.MapViewport.ViewportBackgroundColor != null && automationParams.MapViewport.ViewportBackgroundColor != SMcBColor.bcBlackTransparent)
                            m_pViewport.SetBackgroundColor(automationParams.MapViewport.ViewportBackgroundColor);
                    } catch (MapCoreException McEx) {
                        AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(), mGlSurfaceView.getContext(), McEx, "SetBackgroundColor");
                    }

                    try {
                        double x, y, z;
                        x = automationParams.MapViewport.CameraData.CameraPosition.X;
                        y = automationParams.MapViewport.CameraData.CameraPosition.Y;
                        z = automationParams.MapViewport.CameraData.CameraPosition.Z;

                        m_pViewport.SetCameraPosition(new SMcVector3D(x, y, z), false);
                    } catch (MapCoreException McEx) {
                        AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(),mGlSurfaceView.getContext(), McEx, "SetCameraPosition");
                    }

                    if (m_pViewport.GetMapType() == IMcMapCamera.EMapType.EMT_2D) {
                        try {
                            m_pViewport.SetCameraScale(automationParams.MapViewport.CameraData.CameraScale);
                        } catch (MapCoreException McEx) {
                            AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(),mGlSurfaceView.getContext(), McEx, "SetCameraScale");
                        }
                    } else {
                        try {
                            m_pViewport.SetCameraClipDistances(automationParams.MapViewport.CameraData.CameraClipDistances.Min,
                                    automationParams.MapViewport.CameraData.CameraClipDistances.Max,
                                    automationParams.MapViewport.CameraData.CameraClipDistances.RenderInTwoSessions);
                        } catch (MapCoreException McEx) {
                            AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(),mGlSurfaceView.getContext(), McEx, "SetCameraClipDistances");
                        }

                        try {
                            m_pViewport.SetCameraFieldOfView(automationParams.MapViewport.CameraData.CameraFieldOfView);
                        } catch (MapCoreException McEx) {
                            AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(),mGlSurfaceView.getContext(), McEx, "SetCameraFieldOfView");
                        }
                    }

                    try {
                        float yaw, pitch, roll;
                        yaw = automationParams.MapViewport.CameraData.CameraOrientation.Yaw;
                        pitch = automationParams.MapViewport.CameraData.CameraOrientation.Pitch;
                        roll = automationParams.MapViewport.CameraData.CameraOrientation.Roll;

                        m_pViewport.SetCameraOrientation(yaw, pitch, roll, false);
                    } catch (MapCoreException McEx) {
                        AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(),mGlSurfaceView.getContext(), McEx, "SetCameraOrientation");
                    }

                    try {
                        m_pViewport.SetTransparencyOrderingMode(automationParams.MapViewport.TransparencyOrderingMode);
                    } catch (MapCoreException McEx) {
                        AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(),mGlSurfaceView.getContext(), McEx, "SetTransparencyOrderingMode");
                    }

                    try {
                        m_pViewport.SetOneBitAlphaMode(automationParams.MapViewport.OneBitAlphaMode);
                    } catch (MapCoreException McEx) {
                        AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(),mGlSurfaceView.getContext(), McEx, "SetOneBitAlphaMode");
                    }

                    try {
                        m_pViewport.SetShadowMode(IMcMapViewport.EShadowMode.getInstance(automationParams.MapViewport.ShadowMode));
                    } catch (MapCoreException McEx) {
                        AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(),mGlSurfaceView.getContext(), McEx, "SetShadowMode");
                    }

                    try {
                        if (automationParams.MapViewport.PostProcess != null) {
                            for (String postProcessName : automationParams.MapViewport.PostProcess)
                                m_pViewport.AddPostProcess(postProcessName);
                        }
                    } catch (MapCoreException McEx) {
                        AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(),mGlSurfaceView.getContext(), McEx, "AddPostProcess");
                    } catch (Exception ex) {
                        ex.printStackTrace();
                    }

                    try {
                        if (automationParams.MapViewport.DebugOptions != null && automationParams.MapViewport.DebugOptions.size() > 0) {
                            Object[] keys = automationParams.MapViewport.DebugOptions.keySet().toArray();
                            for (int i = 0; i < keys.length; i++) {
                                m_pViewport.SetDebugOption((int) keys[i], automationParams.MapViewport.DebugOptions.get(keys[i]));
                            }
                        }
                    } catch (MapCoreException McEx) {
                        AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(),mGlSurfaceView.getContext(), McEx, "SetDebugOption");
                    }

                    try {
                        if (automationParams.MapViewport.MaterialSchemeName != null && !automationParams.MapViewport.MaterialSchemeName.isEmpty()) {
                            m_pViewport.SetMaterialScheme(automationParams.MapViewport.MaterialSchemeName);
                        }
                    } catch (MapCoreException McEx) {
                        AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(),mGlSurfaceView.getContext(), McEx, "SetMaterialScheme");
                    }

                    try {
                        if (automationParams.MapViewport.MaterialSchemeDefinition != null && automationParams.MapViewport.MaterialSchemeDefinition.size() > 0) {
                            Object[] keys = automationParams.MapViewport.MaterialSchemeDefinition.keySet().toArray();

                            for (int i = 0; i < keys.length; i++) {
                                m_pViewport.SetMaterialSchemeDefinition((String) keys[i], automationParams.MapViewport.MaterialSchemeDefinition.get(keys[i]));
                            }
                        }
                    } catch (MapCoreException McEx) {
                        AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(),mGlSurfaceView.getContext(), McEx, "SetMaterialSchemeDefinition");
                    }

                    try {
                        m_pViewport.SetDtmVisualization(automationParams.MapViewport.DtmVisualization.IsEnabled, automationParams.MapViewport.DtmVisualization.Params);
                    } catch (MapCoreException McEx) {
                        AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(),mGlSurfaceView.getContext(), McEx, "SetDtmVisualization");
                    }

                    try {
                        if (automationParams.MapViewport.HeightLines.ScaleSteps.size() > 0) {
                            IMcMapHeightLines.SScaleStep[] sScaleSteps = new IMcMapHeightLines.SScaleStep[automationParams.MapViewport.HeightLines.ScaleSteps.size()];
                            automationParams.MapViewport.HeightLines.ScaleSteps.toArray(sScaleSteps);

                            IMcMapHeightLines mapHeightLines = McMapHeightLines.Static.Create(sScaleSteps, automationParams.MapViewport.HeightLines.LineWidth);
                            mapHeightLines.SetColorInterpolationMode(automationParams.MapViewport.HeightLines.ColorInterpolationMode.IsEnabled,
                                    automationParams.MapViewport.HeightLines.ColorInterpolationMode.MinHeight,
                                    automationParams.MapViewport.HeightLines.ColorInterpolationMode.MaxHeight);
                            Manager_MCMapHeightLines.getInstance().AddNewHeightLines(mapHeightLines);

                            m_pViewport.SetHeightLines(mapHeightLines);
                            m_pViewport.SetHeightLinesVisibility(true);
                        }
                    } catch (MapCoreException McEx) {
                        AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(),mGlSurfaceView.getContext(), McEx, "SetHeightLines");
                    }

                    try {
                        if (automationParams.MapViewport.ViewportImageProccessingData != null)
                            SetImageProcessingData(m_pViewport, null, automationParams.MapViewport.ViewportImageProccessingData, amctViewport);
                    } catch (Exception ex) {
                        ex.printStackTrace();
                    }
                    if (automationParams.MapViewport.MapGrid != null) {
                        try {
                            m_pViewport.SetGridAboveVectorLayers(automationParams.MapViewport.MapGrid.GridAboveVectorLayers);
                        } catch (MapCoreException McEx) {
                            AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(), mGlSurfaceView.getContext(), McEx, "SetGridAboveVectorLayers");
                        }

                        try {
                            m_pViewport.SetGridVisibility(automationParams.MapViewport.MapGrid.GridVisibility);
                        } catch (MapCoreException McEx) {
                            AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(),mGlSurfaceView.getContext(), McEx, "GridVisibility");
                        }

                        try {
                            HashMap<String, IMcLineItem> dicLineItemsNames = new HashMap();
                            HashMap<String, IMcTextItem> dicTextItemsNames = new HashMap();

                            IMcMapGrid.SGridRegion[] mcGridRegions = null;
                            if (automationParams.MapViewport.MapGrid.GridRegion != null && automationParams.MapViewport.MapGrid.GridRegion.size() > 0) {
                                mcGridRegions = new IMcMapGrid.SGridRegion[automationParams.MapViewport.MapGrid.GridRegion.size()];
                                int i = 0;
                                for (AMCTGridRegion mctGridRegion : automationParams.MapViewport.MapGrid.GridRegion) {
                                    IMcMapGrid.SGridRegion mcGridRegion = new IMcMapGrid.SGridRegion();
                                    mcGridRegion.pCoordinateSystem = MainActivity.CreateGridCoordinateSystem(mctGridRegion.pCoordinateSystem, mGlSurfaceView.getContext());
                                    mcGridRegion.GeoLimit = mctGridRegion.GeoLimit;
                                    mcGridRegion.uFirstScaleStepIndex = mctGridRegion.uFirstScaleStepIndex;
                                    mcGridRegion.uLastScaleStepIndex = mctGridRegion.uLastScaleStepIndex;

                                    if (mctGridRegion.pGridLine != null && mctGridRegion.pGridLine != "") {
                                        if (dicLineItemsNames.containsKey(mctGridRegion.pGridLine)) {
                                            mcGridRegion.pGridLine = dicLineItemsNames.get(mctGridRegion.pGridLine);
                                        } else {
                                            String filePath = new File(folderPath, mctGridRegion.pGridLine).toString();
                                            IMcObjectScheme[] objectSchemes = m_pViewport.GetOverlayManager().LoadObjectSchemes(filePath);
                                            if (objectSchemes != null && objectSchemes.length > 0) {
                                                IMcObjectSchemeNode[] items = objectSchemes[0].GetNodes(new CMcEnumBitField<>(IMcObjectSchemeNode.ENodeKindFlags.ENKF_SYMBOLIC_ITEM));
                                                if (items != null && items.length > 0 && items[0] instanceof IMcLineItem) {
                                                    mcGridRegion.pGridLine = (IMcLineItem) items[0];
                                                    dicLineItemsNames.put(mctGridRegion.pGridLine, mcGridRegion.pGridLine);
                                                }

                                                objectSchemes[0].Release();
                                            }
                                        }
                                    }
                                    if (mctGridRegion.pGridText != null && mctGridRegion.pGridText != "") {
                                        if (dicTextItemsNames.containsKey(mctGridRegion.pGridText)) {
                                            mcGridRegion.pGridText = dicTextItemsNames.get(mctGridRegion.pGridText);
                                        } else {
                                            String filePath = new File(folderPath, mctGridRegion.pGridText).toString();
                                            IMcObjectScheme[] objectSchemes = m_pViewport.GetOverlayManager().LoadObjectSchemes(filePath);
                                            if (objectSchemes != null && objectSchemes.length > 0) {
                                                IMcObjectSchemeNode[] items = objectSchemes[0].GetNodes(new CMcEnumBitField<>(IMcObjectSchemeNode.ENodeKindFlags.ENKF_SYMBOLIC_ITEM));
                                                if (items != null && items.length > 0 && items[0] instanceof IMcTextItem) {
                                                    mcGridRegion.pGridText = (IMcTextItem) items[0];
                                                    dicTextItemsNames.put(mctGridRegion.pGridText, mcGridRegion.pGridText);
                                                }

                                                objectSchemes[0].Release();
                                            }
                                        }
                                    }

                                    mcGridRegions[i] = mcGridRegion;
                                    i++;
                                }
                            }
                            IMcMapGrid.SScaleStep[] mcScaleSteps = null;
                            if (automationParams.MapViewport.MapGrid.ScaleStep != null && automationParams.MapViewport.MapGrid.ScaleStep.size() > 0) {
                                mcScaleSteps = (IMcMapGrid.SScaleStep[]) automationParams.MapViewport.MapGrid.ScaleStep.toArray(new IMcMapGrid.SScaleStep[automationParams.MapViewport.MapGrid.ScaleStep.size()]);
                            }
                            if (mcGridRegions != null && mcScaleSteps != null) {
                                m_pViewport.SetGrid(McMapGrid.Static.Create(mcGridRegions, mcScaleSteps, automationParams.MapViewport.MapGrid.IsUseBasicItemPropertiesOnly));
                            }

                        } catch (MapCoreException McEx) {
                            AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(), mGlSurfaceView.getContext(), McEx, "MapGrid.Create");
                        }
                    }


                    try {
                        if (automationParams.MapViewport.ImageProccessingData != null) {
                            ArrayList<AMCTKeyValueImageProcessingData> imageProcessing = automationParams.MapViewport.ImageProccessingData;
                            IMcMapTerrain[] mapTerrains = m_pViewport.GetTerrains();

                            for (int i = 0; i < imageProcessing.size(); i++) {
                                AMCTKeyValueImageProcessingData val = imageProcessing.get(i);
                                AMCTKeyValueInteger keyValueInteger = val.Key;
                                IMcMapTerrain mcMapTerrain = null;
                                if (mapTerrains.length > keyValueInteger.key) {
                                    mcMapTerrain = mapTerrains[keyValueInteger.key];
                                    if (mcMapTerrain != null) {
                                        IMcMapLayer[] mapLayers = mcMapTerrain.GetLayers();
                                        if (mapLayers.length > keyValueInteger.value) {
                                            IMcMapLayer mcMapLayer = mapLayers[keyValueInteger.value];
                                            if (mcMapLayer != null && mcMapLayer instanceof IMcRasterMapLayer) {
                                                SetImageProcessingData(m_pViewport, (IMcRasterMapLayer) mcMapLayer, val.Value, amctViewport);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    } catch (Exception ex) {
                        AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(),mGlSurfaceView.getContext(), ex, "ImageProccessingData");
                        ex.printStackTrace();
                    }
                } else {
                    if(!amctViewport.getIsSectionMap())
                        SetCenterMap(context);
                    else
                        mapViewport.GetCameras()[0].SetCameraPosition(new SMcVector3D(), false);
                }
            }
        } catch (MapCoreException mcEx) {
            AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(), mGlSurfaceView.getContext(), mcEx, "IMcMapViewport.Static.Create");

        /*    if (amctViewport.getIsAutomation())
                AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(), mGlSurfaceView.getContext(), mcEx, "IMcMapViewport.Static.Create");
            else
                AlertMessages.ShowMapCoreErrorMessage(mGlSurfaceView.getContext(), mcEx,"IMcMapViewport.Static.Render");
*/
            mcEx.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void SetImageProcessingData(IMcMapViewport mapViewport, IMcRasterMapLayer layer, AMCTImageProcessingData imageProcessingData,  AMCTViewPort amctViewport) throws Exception {
        try
        {
            mapViewport.SetFilterImageProcessing(layer, IMcImageProcessing.EFilterProccessingOperation.getInstance(imageProcessingData.Filter));
        }
        catch (MapCoreException McEx)
        {
            AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(),mGlSurfaceView.getContext(), McEx, "");
        }

        try
        {
            if (imageProcessingData.CustomFilter != null)
            {
                mapViewport.SetCustomFilter(layer, imageProcessingData.CustomFilter.FilterXsize,
                        imageProcessingData.CustomFilter.FilterYsize,
                        imageProcessingData.CustomFilter.Filters,
                        imageProcessingData.CustomFilter.Bias,
                        imageProcessingData.CustomFilter.Divider);
            }
        }
        catch (MapCoreException McEx)
        {
            AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(), mGlSurfaceView.getContext(), McEx, "");
        }
        try
        {
            mapViewport.SetWhiteBalanceBrightness(layer, imageProcessingData.WhiteBalanceBrightnessR, imageProcessingData.WhiteBalanceBrightnessG, imageProcessingData.WhiteBalanceBrightnessB);
        }
        catch (MapCoreException McEx)
        {
            AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(), mGlSurfaceView.getContext(), McEx, "");
        }

        try
        {
            mapViewport.SetEnableColorTableImageProcessing(layer, imageProcessingData.IsEnableColorTableImageProcessing);
        }
        catch (MapCoreException McEx)
        {
            AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(), mGlSurfaceView.getContext(), McEx, "");
        }

        for (int i = 0; i < 3; i++)
        {
            AMCTImageProcessingChannelData processingChannelData = imageProcessingData.ChannelDatas[i];
            if(processingChannelData != null) {
                IMcImageProcessing.EColorChannel Channel = IMcImageProcessing.EColorChannel.getInstance(processingChannelData.Channel);
                try {
                    mapViewport.SetUserColorValues(layer, Channel, processingChannelData.UserColorValues, processingChannelData.UserColorValuesUse);
                } catch (MapCoreException McEx) {
                    AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(), mGlSurfaceView.getContext(), McEx, "SetUserColorValues");
                }

                try {
                    mapViewport.SetColorTableBrightness(layer, Channel, processingChannelData.Brightness);
                } catch (MapCoreException McEx) {
                    AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(), mGlSurfaceView.getContext(), McEx, "SetColorTableBrightness");
                }

                try {
                    mapViewport.SetContrast(layer, Channel, processingChannelData.Contrast);
                } catch (MapCoreException McEx) {
                    AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(), mGlSurfaceView.getContext(), McEx, "SetContrast");
                }

                try {
                    mapViewport.SetNegative(layer, Channel, processingChannelData.Negative);
                } catch (MapCoreException McEx) {
                    AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(), mGlSurfaceView.getContext(), McEx, "SetNegative");
                }

                try {
                    mapViewport.SetGamma(layer, Channel, processingChannelData.Gamma);
                } catch (MapCoreException McEx) {
                    AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(), mGlSurfaceView.getContext(), McEx, "SetGamma");
                }

                if (processingChannelData.IsOriginalHistogramSet) {
                    try {
                        mapViewport.SetOriginalHistogram(layer, Channel, processingChannelData.OriginalHistogram);
                    } catch (MapCoreException McEx) {
                        AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(), mGlSurfaceView.getContext(), McEx, "SetOriginalHistogram");
                    }
                }
                try {
                    mapViewport.SetHistogramNormalization(layer, Channel,
                            processingChannelData.HistogramNormalizationUse,
                            processingChannelData.HistogramNormalizationMean,
                            processingChannelData.HistogramNormalizationStdev);
                } catch (MapCoreException McEx) {
                    AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(), mGlSurfaceView.getContext(), McEx, "SetHistogramNormalization");
                }

                try {
                    mapViewport.SetVisibleAreaOriginalHistogram(layer, Channel,
                            processingChannelData.VisibleAreaOriginalHistogram);
                } catch (MapCoreException McEx) {
                    AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(), mGlSurfaceView.getContext(), McEx, "SetVisibleAreaOriginalHistogram");
                }

                try {
                    mapViewport.SetHistogramEqualization(layer, Channel, processingChannelData.HistogramEqualization);
                } catch (MapCoreException McEx) {
                    AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(), mGlSurfaceView.getContext(), McEx, "SetHistogramEqualization");
                }

                try {
                    mapViewport.SetHistogramFit(layer, Channel, processingChannelData.ReferenceHistogramUse, processingChannelData.ReferenceHistogram);
                } catch (MapCoreException McEx) {
                    AlertMessages.AutomationError(amctViewport.getAutomationLogFile(), amctViewport.getAutomationIsShowMsg(), mGlSurfaceView.getContext(), McEx, "SetHistogramFit");
                }
            }
        }
    }


    private void DrawBoundingBoxScreenRect()
    {
        //if(mBoundingBoxScreenRectObject != null)
        //    Funcs.removeObject(mBoundingBoxScreenRectObject, (Activity) BaseApplication.getCurrActivityContext());
        IMcObject mBoundingBoxScreenRectObject = Manager_AMCTViewportBoundingBoxObject.getInstance().getObject(m_pViewport);

        if(mBoundingBoxScreenRectObject != null)
        {
            Funcs.removeObject(mBoundingBoxScreenRectObject, (Activity) BaseApplication.getCurrActivityContext());
        }
        try {
            //SMcPoint topLeftCorner = new SMcPoint(0,0);
            ObjectRef<Integer> viewportWidth = new ObjectRef<>();
            ObjectRef<Integer> viewportHeight = new ObjectRef<>();

            m_pViewport.GetViewportSize(viewportWidth, viewportHeight);
            float radiusX = viewportWidth.getValue() / 2;
            float radiusY = viewportHeight.getValue() / 2;
            SMcVector3D[] locationPoints = new SMcVector3D[1];
            locationPoints[0] = new SMcVector3D(radiusX, radiusY, 0);
            IMcOverlayManager activeOverlayManager = null;
            try {
                activeOverlayManager = Manager_AMCTMapForm.getInstance().getCurViewport().GetOverlayManager();
            } catch (MapCoreException e) {
                e.printStackTrace();
                AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "GetOverlayManager()");
            } catch (Exception e) {
                e.printStackTrace();
            }

            IMcOverlay activeOverlay = Manager_MCOverlayManager.getInstance().getActiveOverlay();
            EMcPointCoordSystem pointCoordSystem = EMcPointCoordSystem.EPCS_SCREEN;

            try {
                IMcObjectSchemeItem objectSchemeItem = IMcRectangleItem.Static.Create(
                        new CMcEnumBitField<>(IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN),
                        pointCoordSystem,
                        IMcObjectSchemeItem.EGeometryType.EGT_GEOMETRIC_IN_VIEWPORT,
                        IMcRectangleItem.ERectangleDefinition.ERD_RECTANGLE_CENTER_DIMENSIONS,
                        radiusX,
                        radiusY,
                        IMcLineBasedItem.ELineStyle.ELS_SOLID,
                        SMcBColor.bcWhiteOpaque,
                        2f,
                        null,
                        new SMcFVector2D(0, -1),
                        1f,
                        IMcLineBasedItem.EFillStyle.EFS_NONE,
                        new SMcBColor(0, 255, 100, 100));

                mBoundingBoxScreenRectObject = IMcObject.Static.Create(activeOverlay,
                        objectSchemeItem,
                        pointCoordSystem,
                        locationPoints);

                Manager_AMCTViewportBoundingBoxObject.getInstance().addObject(m_pViewport, mBoundingBoxScreenRectObject);

                //In order to prevent retrieval of the polygon in the scan
                objectSchemeItem.SetDetectibility(false);

                int[] activeViewportID = new int[1];
                activeViewportID[0] = Manager_AMCTMapForm.getInstance().getCurViewport().GetViewportID();
                CMcEnumBitField<IMcViewportConditionalSelector.EViewportTypeFlags> vpTypes = new CMcEnumBitField<>(IMcViewportConditionalSelector.EViewportTypeFlags.EVT_ALL_VIEWPORTS);
                CMcEnumBitField<IMcViewportConditionalSelector.EViewportCoordinateSystem> vpCoordinateSystem = new CMcEnumBitField<>(IMcViewportConditionalSelector.EViewportCoordinateSystem.EVCS_ALL_COORDINATE_SYSTEMS);
                IMcConditionalSelector viewportSelector = McViewportConditionalSelector.Static.Create(activeOverlayManager,
                        vpTypes,
                        vpCoordinateSystem,
                        activeViewportID,
                        true);

                mBoundingBoxScreenRectObject.SetConditionalSelector(IMcConditionalSelector.EActionType.EAT_VISIBILITY,
                        true,
                        viewportSelector);


            } catch (MapCoreException e) {
                //AlertMessages.ShowMapCoreErrorMessage(mRootView.getContext(), e, "PolyScan");

            } catch (Exception e) {
                e.printStackTrace();
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public void onResume() {
    }

    public void onPause() {
    }

    @Override
    public void onFrameAvailable(SurfaceTexture surfaceTexture) {

    }

    public void RemoveCurrentViewport() {
        if (m_pViewport != null) {
            IMcMapViewport secondViewport = Manager_AMCTMapForm.getInstance().getMapFormByVp(m_pViewport).getSecondMapViewport();
            if(secondViewport != null) {
                Manager_AMCTMapForm.getInstance().removeViewport(secondViewport);
                secondViewport.Release();
            }
            Manager_AMCTMapForm.getInstance().removeViewport(m_pViewport);
            Manager_AMCTMapForm.getInstance().ResetCurrViewport();

            m_pViewport.Release();
            m_pViewport = null;
        }
    }

    public void SetCenterMap(Context context) {
        try {
            SMcVector3D centerPoint = m_pViewport.GetTerrainsBoundingBox().GetCenterPoint();
            Funcs.MoveToMapCenter(centerPoint, m_pViewport, context);
        } catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(mGlSurfaceView.getContext(), mcEx, "GetTerrainsBoundingBox");
            mcEx.printStackTrace();
        } catch (Exception mcEx) {
            mcEx.printStackTrace();
        }
    }
}


