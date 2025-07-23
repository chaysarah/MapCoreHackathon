package com.elbit.mapcore.mcandroidtester.ui.map;

import android.app.Activity;
import android.content.Context;
import android.graphics.Point;
import android.opengl.GLSurfaceView;
import javax.microedition.khronos.egl.EGL10;
import javax.microedition.khronos.egl.EGLConfig;
import javax.microedition.khronos.egl.EGLContext;
import javax.microedition.khronos.egl.EGLDisplay;
import androidx.annotation.NonNull;
import android.util.AttributeSet;
import android.view.ActionMode;
import android.view.Display;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.MotionEvent;
import android.view.SurfaceHolder;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.IEditModeFragmentCallback;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_AMCTMapForm;
import com.elbit.mapcore.mcandroidtester.model.AMCTGLThreadEvents;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.MapFragment;
import com.elbit.mapcore.mcandroidtester.ui.objects.ObjectPropertiesBase;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.BaseApplication;
import com.elbit.mapcore.mcandroidtester.model.AMCTMapDevice;

import com.elbit.mapcore.Classes.Calculations.SMcScanPointGeometry;
import com.elbit.mapcore.Enums.EMcPointCoordSystem;
import com.elbit.mapcore.General.CMcEnumBitField;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.General.ObjectRef;
import com.elbit.mapcore.Interfaces.Calculations.IMcSpatialQueries;
import com.elbit.mapcore.Interfaces.General.IMcEditMode;
import com.elbit.mapcore.Interfaces.Map.IMcMapCamera;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeItem;
import com.elbit.mapcore.Structs.SMcPoint;
import com.elbit.mapcore.Structs.SMcVector3D;


import java.util.ArrayList;
import java.util.Collection;
import java.util.Iterator;
import java.util.Queue;

// MapCore EGLConfig chooser for setting OpenGL-ES context parameters, mainly MSAA
class MapCoreConfigChooser implements GLSurfaceView.EGLConfigChooser {
    private int m_fsaaMode;
    MapCoreConfigChooser(int fsaaMode)
    {
        m_fsaaMode = fsaaMode;
    }
    @Override
    public EGLConfig chooseConfig(EGL10 egl, EGLDisplay display) {
        // try to select FSAA config, else fallback to "default"
        int default_attribs[] = {
                EGL10.EGL_LEVEL, 0,
                EGL10.EGL_RENDERABLE_TYPE, 4,  // EGL_OPENGL_ES2_BIT
                EGL10.EGL_COLOR_BUFFER_TYPE, EGL10.EGL_RGB_BUFFER,
                EGL10.EGL_RED_SIZE, 8,
                EGL10.EGL_GREEN_SIZE, 8,
                EGL10.EGL_BLUE_SIZE, 8,
                EGL10.EGL_ALPHA_SIZE, 8,
                EGL10.EGL_DEPTH_SIZE, 24,
                EGL10.EGL_STENCIL_SIZE, 8,
                EGL10.EGL_NONE
        };

        int fsaa_default[] = {
                EGL10.EGL_LEVEL, 0,
                EGL10.EGL_RENDERABLE_TYPE, 4,  // EGL_OPENGL_ES2_BIT
                EGL10.EGL_COLOR_BUFFER_TYPE, EGL10.EGL_RGB_BUFFER,
                EGL10.EGL_RED_SIZE, 8,
                EGL10.EGL_GREEN_SIZE, 8,
                EGL10.EGL_BLUE_SIZE, 8,
                EGL10.EGL_ALPHA_SIZE, 8,
                EGL10.EGL_DEPTH_SIZE, 24,
                EGL10.EGL_STENCIL_SIZE, 8,
                EGL10.EGL_SAMPLE_BUFFERS, 1,
                EGL10.EGL_SAMPLES, m_fsaaMode,
                EGL10.EGL_NONE
        };

        EGLConfig[] configs = new EGLConfig[1];
        int[] configCounts = new int[1];
        egl.eglChooseConfig(display, fsaa_default, configs, 1, configCounts);
        if (configCounts[0] == 0) {
            // Failed! try fallback config
            egl.eglChooseConfig(display, default_attribs, configs, 1, configCounts);
            if (configCounts[0] == 0)
                return null;
            else
                return configs[0];
        } else {
            return configs[0];
        }
    }
}

// MapCore Context Factory for life-cycle support
class MapCoreGLContextFactory implements GLSurfaceView.EGLContextFactory {
    private static int EGL_CONTEXT_CLIENT_VERSION = 0x3098;
    private static EGLContext m_Context ;
    private static boolean m_bDestroyContext = false;
    public EGLContext createContext(EGL10 egl, EGLDisplay display, EGLConfig eglConfig) {
        if (m_Context == null)
        {
            // EGL_CONTEXT_CLIENT_VERSION values: 2->OpenGL-ES 2.0  3->OpenGL-ES 3.0
            int[] attrib_list = {EGL_CONTEXT_CLIENT_VERSION, 3, EGL10.EGL_NONE};
            m_Context = egl.eglCreateContext(display, eglConfig, EGL10.EGL_NO_CONTEXT, attrib_list);
            //   checkEglError("After eglCreateContext", egl);
        }
        return m_Context;
    }

    public void destroyContext(EGL10 egl, EGLDisplay display, EGLContext context) {
        if (m_bDestroyContext) {
            egl.eglDestroyContext(display, context);
        }
    }

    public void notifyContextDestroy()
    {
        m_bDestroyContext = true;
    }

}

// MapCore GLSurfaceView implementation
public class AMcGLSurfaceView extends GLSurfaceView {
    private static final String TAG = "AMcGLSurfaceView";
    public static AMcGLSurfaceViewRenderer mRenderer;
    //public AMcGLSurfaceViewRenderer mRenderer;
    private Context mContext;
    private IMcEditMode m_EditMode;
    private ActionMode.Callback mActionModeCallback;
    private ActionMode mActionMode = null;
    private MapFragment mMapFragment;
    private Queue<AMCTGLThreadEvents> editModeEvents;

    @Override
    protected void onWindowVisibilityChanged(int visibility) {
        super.onWindowVisibilityChanged(visibility);
    }

    public MapFragment getMapFragment()
    {
       return mMapFragment;
    }

    public AMcGLSurfaceView(Context context, AttributeSet attrs) {
        super(context, attrs);
        if(!isInEditMode()) {
            mContext = context;

            if (mContext instanceof MapsContainerActivity)
                mMapFragment = ((MapsContainerActivity) mContext).mMapFragment;
            setRender();

            setOnLongClickListener(null);

            mActionModeCallback = new ActionMode.Callback() {

                // Called when the action mode is created; startActionMode() was called
                @Override
                public boolean onCreateActionMode(ActionMode mode, Menu menu) {
                    // Inflate a menu resource providing context menu items
                    MenuInflater inflater = mode.getMenuInflater();
                    inflater.inflate(R.menu.menu_map_editmode, menu);

                    return true;
                }

                // Called each time the action mode is shown. Always called after onCreateActionMode, but
                // may be called multiple times if the mode is invalidated.
                @Override
                public boolean onPrepareActionMode(ActionMode mode, Menu menu) {
                    return false; // Return false if nothing is done
                }

                // Called when the user selects a contextual menu item
                @Override
                public boolean onActionItemClicked(ActionMode mode, MenuItem item) {
                    boolean returnValue = false;
                    int id = item.getItemId();
                    if (id == R.id.cnt_mnu_editmode_finish) {
                        mode.finish(); // Action picked, so close the CAB
                        ExitCurrentAction(false);
                        mMapFragment.setIsOpenedEditModeActionMode(false);
                        returnValue = true;
                    }
                    else if (id == R.id.cnt_mnu_editmode_cancel) {
                        mode.finish(); // Action picked, so close the CAB
                        ExitCurrentAction(true);
                        mMapFragment.setIsOpenedEditModeActionMode(false);
                    }
                    else if (id == R.id.cnt_mnu_editmode_delete_vertex) {
                        OnKeyPress(IMcEditMode.EKeyEvent.EKE_DELETE_VERTEX);
                        returnValue = true;
                    }
                    else{
                        returnValue = false;
                    }
                    mMapFragment.SetScanType(MapFragment.ScanType.None);

                    return returnValue;
                }

                // Called when the user exits the action mode
                @Override
                public void onDestroyActionMode(ActionMode mode) {
                    mActionMode = null;
               }
            };

            editModeEvents = new Queue<AMCTGLThreadEvents>() {
                @Override
                public boolean add(AMCTGLThreadEvents amctglThreadEvents) {
                    return false;
                }

                @Override
                public boolean offer(AMCTGLThreadEvents amctglThreadEvents) {
                    return false;
                }

                @Override
                public AMCTGLThreadEvents remove() {
                    return null;
                }

                @Override
                public AMCTGLThreadEvents poll() {
                    return null;
                }

                @Override
                public AMCTGLThreadEvents element() {
                    return null;
                }

                @Override
                public AMCTGLThreadEvents peek() {
                    return null;
                }

                @Override
                public boolean addAll(@NonNull Collection<? extends AMCTGLThreadEvents> collection) {
                    return false;
                }

                @Override
                public void clear() {

                }

                @Override
                public boolean contains(Object o) {
                    return false;
                }

                @Override
                public boolean containsAll(@NonNull Collection<?> collection) {
                    return false;
                }

                @Override
                public boolean isEmpty() {
                    return false;
                }

                @NonNull
                @Override
                public Iterator<AMCTGLThreadEvents> iterator() {
                    return null;
                }

                @Override
                public boolean remove(Object o) {
                    return false;
                }

                @Override
                public boolean removeAll(@NonNull Collection<?> collection) {
                    return false;
                }

                @Override
                public boolean retainAll(@NonNull Collection<?> collection) {
                    return false;
                }

                @Override
                public int size() {
                    return 0;
                }

                @NonNull
                @Override
                public Object[] toArray() {
                    return new Object[0];
                }

                @NonNull
                @Override
                public <T> T[] toArray(@NonNull T[] ts) {
                    return null;
                }
            };
        }
    }


    private void setRender() {
        setEGLContextClientVersion(3);
        // create GL ContextFactory to support Android device rotation life-cycle 
        setEGLContextFactory(new MapCoreGLContextFactory());

        if (mRenderer == null) {
            mRenderer = new AMcGLSurfaceViewRenderer(this);
            setPreserveEGLContextOnPause(true);
            Display display = ((Activity) mContext).getWindowManager().getDefaultDisplay();
            final Point size = new Point();
            display.getSize(size);
            mRenderer.InitViewportSize(size.x, size.y);
            // setup FSAA/MSAA context according to ImcMapDevice relevant enum from Android
            int fsaa_mode = 0;
            switch(AMCTMapDevice.getInstance().getViewportAntiAliasingLevel())
            {
                case EAAL_NONE:
                    fsaa_mode=0;break;
                case EAAL_1:
                case EAAL_2:
                case EAAL_3:
                    fsaa_mode=2;break;
                case EAAL_4:
                case EAAL_5:
                case EAAL_6:
                case EAAL_7:
                    fsaa_mode=4;break;
                case EAAL_8:
                case EAAL_8_QUALITY:
                case EAAL_9:
                case EAAL_9_QUALITY:
                case EAAL_10:
                case EAAL_10_QUALITY:
                case EAAL_11:
                case EAAL_11_QUALITY:
                case EAAL_12:
                case EAAL_12_QUALITY:
                case EAAL_13:
                case EAAL_13_QUALITY:
                case EAAL_14:
                case EAAL_14_QUALITY:
                case EAAL_15:
                case EAAL_15_QUALITY:
                    fsaa_mode=8;break;
                case EAAL_16:
                case EAAL_16_QUALITY:
                    fsaa_mode=16;break;
            };
            setEGLConfigChooser(new MapCoreConfigChooser(fsaa_mode));
        }
        mRenderer.setGlSurfaceView(this);

        setRenderer(mRenderer);
        setRenderMode(GLSurfaceView.RENDERMODE_CONTINUOUSLY);
        mRenderer.Render();
        getEditMode();
    }

    public IMcEditMode getEditMode() {
        if (Manager_AMCTMapForm.getInstance().getCurMapForm() != null)
            m_EditMode = Manager_AMCTMapForm.getInstance().getCurMapForm().getEditMode();
        else
            m_EditMode = null;
        return m_EditMode;
    }

    private void ExitCurrentAction(final boolean isDiscard)
    {
        //if its not on queue event-jni exception
        queueEvent(new Runnable() {
            @Override
            public void run() {
                try {
                    m_EditMode.ExitCurrentAction(isDiscard);
                }catch (MapCoreException e)
                {
                    AlertMessages.ShowMapCoreErrorMessage(BaseApplication .getCurrActivityContext(), e, "ExitCurrentAction");
                }
                catch (Exception e) {
                    e.printStackTrace();
                }
            }});
    }

    @Override
    public boolean onTouchEvent(final MotionEvent event) {

        super.onTouchEvent(event);
        final MotionEvent event1 = MotionEvent.obtain(event);
        queueEvent(new Runnable() {
            @Override
            public void run() {
                onTouchEvent2(event1);
            }
        });

        return true;
    }

    public void ScanEditMode(SMcPoint touchPositionViewport, int eventAction ) {
        float eventX = touchPositionViewport.x;
        float eventY = touchPositionViewport.y;

        if (eventAction == MotionEvent.ACTION_UP) {
            SMcVector3D pointItem = new SMcVector3D((double) eventX, (double) eventY, 0);
            SMcScanPointGeometry scanPointGeometry = new SMcScanPointGeometry(EMcPointCoordSystem.EPCS_SCREEN, pointItem, 0);

            IMcSpatialQueries.SQueryParams queryParams = new IMcSpatialQueries.SQueryParams();
            queryParams.uTargetsBitMask = new CMcEnumBitField<>(IMcSpatialQueries.EIntersectionTargetType.EITT_OVERLAY_MANAGER_OBJECT);
            //queryParams.eTerrainPrecision = IMcSpatialQueries.EQueryPrecision.EQP_DEFAULT;
            //queryParams.uItemTypeFlagsBitField = 0;

             IMcMapViewport currViewport = Manager_AMCTMapForm.getInstance().getCurViewport();
            try {
                IMcSpatialQueries.STargetFound[] TargetFound = currViewport.ScanInGeometry(scanPointGeometry, false, queryParams);
               if (TargetFound != null)
                   if(TargetFound.length > 0) {
                       IMcObject selectedObject = TargetFound[0].ObjectItemData.pObject;
                       IEditModeFragmentCallback.EType type = IEditModeFragmentCallback.EType.Init_Mode;
                       //IMcObjectSchemeItem selectedItem = TargetFound[0].ObjectItemData.pItem;
                       if (mMapFragment.GetScanType() == MapFragment.ScanType.EditMode)
                           type = IEditModeFragmentCallback.EType.Edit_Mode;
                       mMapFragment.startScanMode(type, selectedObject);
                       mMapFragment.SetScanType(MapFragment.ScanType.None);
                   }
            } catch (MapCoreException e) {
                AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "ScanInGeometry");
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    public void surfaceCreated(SurfaceHolder holder) {
        super.surfaceCreated(holder);
    }

    public void surfaceDestroyed(SurfaceHolder holder) {
        super.surfaceDestroyed(holder);
    }

    public void surfaceChanged(SurfaceHolder holder, int format, int w, int h) {
        super.surfaceChanged(holder, format, w, h);
    }

    @Override
    public void onResume() {
        super.onResume();
        mRenderer.onResume();
    }

    @Override
    public void onPause() {
        mRenderer.onPause();
        super.onPause();
    }

    @Override
    protected void onSizeChanged(int w, int h, int oldw, int oldh) {
        super.onSizeChanged(w, h, oldw, oldh);

        if( w!= oldw || h != oldh) {
            if (!isInEditMode()) {
                final int nWidth = w;
                final int nHeight = h;
                queueEvent(new Runnable() {
                    @Override
                    public void run() {
                        mRenderer.ViewportResized(nWidth, nHeight);
                    }
                });
            }
        }
    }

    public void StartEditMode(final IMcObject object, final IMcObjectSchemeItem objectSchemeItem)
    {
           queueEvent(new Runnable() {
               @Override
               public void run() {
                   try {
                   getEditMode().StartEditObject(object, objectSchemeItem);
                   OpenEditModeActionMode();
                   } catch (MapCoreException e) {
                       AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(),e,"StartEditObject");
                   }catch (Exception e) {
                       e.printStackTrace();
                   }
               }
           });


    }

    public void CloseEditModeActionMode() {
        if (mContext instanceof MapsContainerActivity) {
            ((MapsContainerActivity) mContext).runOnUiThread(
                    new Runnable() {
                        public void run() {
                            if (mActionMode != null) {
                                mActionMode.finish();
                                mMapFragment.setIsOpenedEditModeActionMode(false);
                                mActionMode = null;
                            }
                        }
                    });
        }
    }

    public void CheckIsNeedToOpenEditModeActionMode()
    {
        if(mMapFragment.getIsOpenedEditModeActionMode())
        {
            OpenEditModeActionMode();
        }
    }

    public void OpenEditModeActionMode() {
        if (mContext instanceof MapsContainerActivity) {
            if (mActionMode != null) {
                return;
            }
            ((MapsContainerActivity) mContext).runOnUiThread(
                    new Runnable() {
                        public void run() {
                            mActionMode = ((MapsContainerActivity) mContext).startActionMode(mActionModeCallback);
                            mMapFragment.setIsOpenedEditModeActionMode(true);
                        }
                    });
        }
    }

    public void dynamicZoom() {
        queueEvent(new Runnable() {
            @Override
            public void run() {
                try {
                    getEditMode().StartDynamicZoom(0.001f, true, new SMcPoint(), null, IMcMapCamera.ESetVisibleArea3DOperation.ESVAO_ROTATE_AND_MOVE);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "IMcEditMode.StartDynamicZoom");

                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    public void distanceDirectionMeasure() {
        queueEvent(new Runnable() {
            @Override
            public void run() {
                try {
                    IMcEditMode.SDistanceDirectionMeasureParams params = getEditMode().GetDistanceDirectionMeasureParams();
                    getEditMode().StartDistanceDirectionMeasure(true,
                            true,
                            new SMcPoint());
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "IMcEditMode.StartDynamicZoom");

                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    public void StartInitMode(final IMcObject object, final IMcObjectSchemeItem objectSchemeItem)
    {
        queueEvent(new Runnable() {
            @Override
            public void run() {
                try {
                    CloseEditModeActionMode();
                    getEditMode().StartInitObject(object, objectSchemeItem);
                    switch (objectSchemeItem.GetNodeType())
                    {
                        case ARROW_ITEM:
                        case LINE_EXPANSION_ITEM:
                        case LINE_ITEM:
                        case POLYGON_ITEM:
                            OpenEditModeActionMode();
                            break;
                        default:
                            break;
                    }
                    if(ObjectPropertiesBase.Text.getInstance().mIsWaitForCreate) {
                        ObjectPropertiesBase.Text.getInstance().mPreviousFragmentText = ObjectPropertiesBase.Text.PreviousFragmentText.ObjectProperties;
                        ObjectPropertiesBase.Text.getInstance().mIsWaitForCreate = false;
                    }

                }
                catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(),e,"StartInitObject");

                }catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });

    }

    public boolean IsEditModeActive()
    {
        try {
            return (getEditMode() != null && getEditMode().IsEditingActive() == true);
        }catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "IMcEditMode.IsEditingActive");
            return false;
        }
        catch (Exception e) {
            AlertMessages.ShowErrorMessage(BaseApplication.getCurrActivityContext(), e.getMessage(), "IMcEditMode.IsEditingActive");
            return false;
        }
    }

    public void onTouchEvent2(MotionEvent event) {
        try {
            int nCount = event.getPointerCount();
            SMcPoint touchPositionWindow = new SMcPoint((int) event.getX(0), (int) event.getY(0));

            SMcPoint touchPositionViewport = new SMcPoint();
            ObjectRef<Boolean> bInViewport = new ObjectRef<>();
            ObjectRef<Boolean> bInViewportRect = new ObjectRef<>();
            ObjectRef<IMcMapViewport> pPixelViewport = new ObjectRef<>();
            IMcMapViewport currViewport = Manager_AMCTMapForm.getInstance().getCurViewport();
            if (currViewport != null) {
                int eventAction = event.getActionMasked();

                if (eventAction == MotionEvent.ACTION_DOWN) {
                    currViewport.WindowPixelToViewportPixel(touchPositionWindow, touchPositionViewport, bInViewport, bInViewportRect, pPixelViewport);
                    IMcMapViewport mapViewport = pPixelViewport.getValue();
                    if (mapViewport != null && mapViewport != currViewport) {
                        mapViewport.SetTopMostZOrderInWindow();
                        Manager_AMCTMapForm.getInstance().activateMapForm(mapViewport);
                    }
                    mMapFragment.TouchMap(touchPositionViewport, event.getActionMasked());
                } else {
                    currViewport.WindowPixelToViewportPixel(touchPositionWindow, touchPositionViewport, bInViewport);
                }
                IMcEditMode.EMouseEvent mouseEventEditMode = null;

                if (IsEditModeActive()) {
                    switch (eventAction) {
                        case MotionEvent.ACTION_DOWN:
                            mouseEventEditMode = IMcEditMode.EMouseEvent.EME_BUTTON_PRESSED;
                            break;
                        case MotionEvent.ACTION_MOVE:
                            mouseEventEditMode = IMcEditMode.EMouseEvent.EME_MOUSE_MOVED_BUTTON_DOWN;
                            break;
                        case MotionEvent.ACTION_UP:
                            mouseEventEditMode = IMcEditMode.EMouseEvent.EME_BUTTON_RELEASED;
                            break;
                        case MotionEvent.ACTION_POINTER_DOWN:
                            mouseEventEditMode = IMcEditMode.EMouseEvent.EME_SECOND_TOUCH_PRESSED;
                            break;
                        case MotionEvent.ACTION_POINTER_UP:
                            mouseEventEditMode = IMcEditMode.EMouseEvent.EME_SECOND_TOUCH_RELEASED;
                            break;
                    }
                    if (mouseEventEditMode != null) {
                        SMcPoint secondTouchPositionWindow = null;
                        SMcPoint secondTouchPositionViewport = null;

                        if (nCount > 1) {
                            secondTouchPositionViewport = new SMcPoint();
                            secondTouchPositionWindow = new SMcPoint((int) event.getX(1), (int) event.getY(1));
                            currViewport.WindowPixelToViewportPixel(secondTouchPositionWindow, secondTouchPositionViewport, bInViewport);
                        }
                        ObjectRef<Boolean> bRenderNeeded = new ObjectRef<Boolean>();

                        ObjectRef<IMcEditMode.ECursorType> cursorType = new ObjectRef<IMcEditMode.ECursorType>();
                        try {
                            getEditMode().OnMouseEvent(mouseEventEditMode,
                                    touchPositionViewport,
                                    false,
                                    (short) 0,
                                    bRenderNeeded,
                                    cursorType,
                                    secondTouchPositionViewport);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "IMcEditMode.OnMouseEvent");
                        } catch (Exception e) {
                            AlertMessages.ShowErrorMessage(BaseApplication.getCurrActivityContext(), e.getMessage(), "IMcEditMode.OnMouseEvent");
                        }
                    }
                } else {

                    if (mMapFragment != null && mMapFragment.GetScanType() != MapFragment.ScanType.None) {
                        ScanEditMode(touchPositionViewport, event.getActionMasked());
                    } else
                        getEditMode().StartNavigateMap(false, true, false, touchPositionViewport);
                }
            }
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(),e,"IMcEditMode.OnMouseEvent");
        } catch (Exception e) {
            AlertMessages.ShowErrorMessage(BaseApplication.getCurrActivityContext(), e.getMessage(), "IMcEditMode.OnMouseEvent");
        }
    }

    public void CancelEditMode()
    {
        queueEvent(new Runnable() {
            @Override
            public void run() {
                mMapFragment.setIsOpenedEditModeActionMode(false);
                OnKeyPress(IMcEditMode.EKeyEvent.EKE_ABORT);
            }
        });
    }

    public void OnKeyPress(final IMcEditMode.EKeyEvent keyEvent) {
        queueEvent(new Runnable() {
            @Override
            public void run() {
                try {
                    getEditMode().OnKeyEvent(keyEvent);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "IMcEditMode.OnKeyEvent");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    public void RemoveCurrentViewport() {
        queueEvent(new Runnable() {
            @Override
            public void run() {
                mRenderer.RemoveCurrentViewport();

                if (Manager_AMCTMapForm.getInstance().isAnyViewportExist()) {
                    ArrayList<IMcMapViewport> viewports = Manager_AMCTMapForm.getInstance().getAllImcViewports();
                    int vpId = viewports.get(0).hashCode();
                    mRenderer.changeViewPort(vpId);
                }
            }
        });
    }

    public void SetCenterMap(final Context context) {
        queueEvent(new Runnable() {
            @Override
            public void run() {
                mRenderer.SetCenterMap(context);
            }
        });
    }
}