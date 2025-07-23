package com.elbit.mapcore.mcandroidtester.ui.map.fragments;

import android.content.Context;
import android.content.Intent;
import android.content.res.AssetManager;
import android.net.Uri;
import android.opengl.GLSurfaceView;
import android.os.Bundle;
import androidx.annotation.Nullable;
import androidx.fragment.app.DialogFragment;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentTransaction;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.elbit.mapcore.Classes.Map.McMapHeightLines;
import com.elbit.mapcore.General.ObjectRef;
import com.elbit.mapcore.Interfaces.Map.IMcMapDevice;
import com.elbit.mapcore.Interfaces.Map.IMcMapHeightLines;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcLightBasedItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcLocationBasedLightItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectLocation;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcPhysicalItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcPictureItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcProperty;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcSymbolicItem;
import com.elbit.mapcore.Structs.SMcPoint;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.interfaces.IEditModeFragmentCallback;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_AMCTMapForm;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_MCMapHeightLines;
import com.elbit.mapcore.mcandroidtester.managers.ObjectWorld.Manager_MCOverlayManager;
import com.elbit.mapcore.mcandroidtester.model.AMCTGrid;
import com.elbit.mapcore.mcandroidtester.model.AMCTMapTiles;
import com.elbit.mapcore.mcandroidtester.model.DrawObjectResult;
import com.elbit.mapcore.mcandroidtester.ui.map.AMcGLSurfaceView;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.EditModeFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.MovementFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.ScanFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.edit_mode_properties.EditModePropertiesTabsFragment;
import com.elbit.mapcore.mcandroidtester.ui.objects.ObjectPropertiesBase;
import com.elbit.mapcore.mcandroidtester.ui.objects.fragments.ObjectPropertiesFragment;
import com.elbit.mapcore.mcandroidtester.ui.objects.fragments.create_mesh_tabs.CreateMeshTabsFragment;
import com.elbit.mapcore.mcandroidtester.ui.objects.fragments.create_text_tabs.CreateTextTabsFragment;
import com.elbit.mapcore.mcandroidtester.ui.objects.fragments.create_texture_tabs.CreateTextureTabsFragment;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.BaseApplication;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.expandableNavigationDrawer.data.MenuDataProvider;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.HashMap;
import java.util.Observable;
import java.util.Observer;

import com.elbit.mapcore.Classes.OverlayManager.McViewportConditionalSelector;
import com.elbit.mapcore.Enums.EMcPointCoordSystem;
import com.elbit.mapcore.General.CMcEnumBitField;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Calculations.IMcSpatialQueries;
import com.elbit.mapcore.Interfaces.Map.IMcMapCamera;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcArcItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcClosedShapeItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcConditionalSelector;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcEllipseItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcLineBasedItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcLineItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectScheme;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeNode;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlay;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcPolygonItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcRectangleItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcViewportConditionalSelector;
import com.elbit.mapcore.Structs.SMcBColor;
import com.elbit.mapcore.Structs.SMcFVector2D;
import com.elbit.mapcore.Structs.SMcVariantString;
import com.elbit.mapcore.Structs.SMcVector3D;

import static com.elbit.mapcore.mcandroidtester.interfaces.IPrivatePropertiesId.Id;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link MapFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link MapFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class MapFragment extends Fragment implements IEditModeFragmentCallback, Observer {
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String IsOpenedEditModeActionModeStr = "IsOpenedEditModeActionMode";

    IMcObject m_LastObjInitMode = null;
    private OnFragmentInteractionListener mListener;

    public AMcGLSurfaceView mView;

    private ScanType scanType;
    private HashMap<String, View> mLayoutsForMenuGroups;
    private View mRootView;
    private TextView mWorldCoordX;
    private TextView mWorldCoordY;
    private TextView mWorldCoordZ;
    private TextView mScale;
    private TextView mFPSAvg;
    private IMcMapViewport.SDtmVisualizationParams msDtmVisualizationParams;

    private SMcVector3D mCurrentWorldPoint;
    private boolean mIsOpenedEditModeActionMode;

    public enum DrawObjectType {
        polygon,
        rectangle,
        line
    }

    public void removeMenuBttnsFromScreen() {
        LinearLayout menuLayout = (LinearLayout) mRootView.findViewById(R.id.fragment_amctmap_menu_container);
        menuLayout.removeAllViews();
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
        if (observable instanceof ObjectPropertiesBase.Text) {
            LoadScheme(Consts.SCHEMES_FILES_TEXT);
        }
    }

    public boolean getIsOpenedEditModeActionMode() {
        return mIsOpenedEditModeActionMode;
    }

    public void setIsOpenedEditModeActionMode(boolean isOpenedEditModeActionMode) {
        this.mIsOpenedEditModeActionMode = isOpenedEditModeActionMode;
    }

    public enum ScanType {
        None,
        InitMode,
        EditMode
    }

    public ScanType GetScanType() {
        return scanType;
    }

    public void SetScanType(ScanType _scanType) {
        scanType = _scanType;
    }

    // TODO: Rename and change types and number of parameters
    public static MapFragment newInstance() {
        MapFragment fragment = new MapFragment();
        return fragment;
    }

    public MapFragment() {
        // Required empty public constructor
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {

        super.onSaveInstanceState(outState);
        outState.putString(IsOpenedEditModeActionModeStr, String.valueOf(mIsOpenedEditModeActionMode));
    }
    String value;
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setHasOptionsMenu(true);

        if(savedInstanceState != null) {
            value = savedInstanceState.getString(IsOpenedEditModeActionModeStr);
            mIsOpenedEditModeActionMode = Boolean.parseBoolean(value);
        }
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        setTitle();
        // Inflate the layout for this fragment
        mRootView = inflater.inflate(R.layout.fragment_amctmap, container, false);
        initMenuLayoutIdForGroupName(inflater);
        inflateWorldPointViews();
        scanType = ScanType.None;
        return mRootView;
    }

    private void handleMapClick(float x, float y) {
        updateWorldPoint(x, y);
        updateScale();
    }

    private void updateScale() {
        final float scale = getScale();
        getActivity().runOnUiThread(new Runnable() {
            @Override
            public void run() {
                String scaleVal = "1:" + ((float) Math.round((double) scale * 1000) / 1000);
                mScale.setText(scaleVal);
            }
        });
    }

    private void updateWorldPoint(float x, float y) {
        final SMcVector3D worldPoint = getWorldPoint(x, y);
        mCurrentWorldPoint = worldPoint;
        getActivity().runOnUiThread(new Runnable() {
            @Override
            public void run() {
                mWorldCoordX.setText(String.valueOf((int) worldPoint.x));
                mWorldCoordY.setText(String.valueOf((int) worldPoint.y));
                mWorldCoordZ.setText(String.valueOf((int) worldPoint.z));
            }
        });
    }

    public SMcVector3D getCurrentWorldPoint()
    {
        return mCurrentWorldPoint;
    }

    public void updateFPSAvg(final long fpsAvg) {
        getActivity().runOnUiThread(new Runnable() {
            @Override
            public void run() {
                mFPSAvg.setText(String.valueOf(fpsAvg));
            }
        });
    }

    @Override
    public void onCreateOptionsMenu(Menu menu, MenuInflater inflater) {
        menu.clear();
        inflater.inflate(R.menu.activity_map_container_drawer, menu);
    }

    private float getScale() {
        IMcMapViewport currVP = Manager_AMCTMapForm.getInstance().getCurViewport();
        float newScale = 0.0F;
        if (currVP != null) {
            try {
                if (currVP.GetMapType() == IMcMapCamera.EMapType.EMT_3D) {
                    if (!mWorldCoordX.getText().equals("") && !mWorldCoordY.getText().equals("") && !mWorldCoordZ.getText().equals(""))
                        newScale = currVP.GetCameraScale(new SMcVector3D(Double.parseDouble(String.valueOf(mWorldCoordX.getText())), Double.parseDouble(String.valueOf(mWorldCoordY.getText())), Double.parseDouble(String.valueOf(mWorldCoordZ.getText()))));
                } else
                    newScale = currVP.GetCameraScale();

            } catch (MapCoreException e) {
                e.printStackTrace();
                AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "GetMapType()/GetCameraScale");
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
        return newScale;
    }

    private SMcVector3D getWorldPoint(float x, float y) {
        IMcMapViewport viewPort = Manager_AMCTMapForm.getInstance().getCurViewport();

        ObjectRef<Boolean> isIntersect = new ObjectRef<>();
        SMcVector3D screenPoint = new SMcVector3D();
        screenPoint.x = x;
        screenPoint.y = y;
        SMcVector3D worldPoint = new SMcVector3D();

        if (viewPort != null) {
            try {
                viewPort.ScreenToWorldOnTerrain(screenPoint, worldPoint, isIntersect);
                if (!isIntersect.getValue())
                    viewPort.ScreenToWorldOnPlane(screenPoint, worldPoint, isIntersect);
                if (!isIntersect.getValue()) {
                    worldPoint.x = 0;
                    worldPoint.y = 0;
                    worldPoint.z = 0;
                }
                if (viewPort.GetOverlayManager() != null)
                    worldPoint = viewPort.ViewportToOverlayManagerWorld(worldPoint, viewPort.GetOverlayManager());
            } catch (MapCoreException e) {
                e.printStackTrace();
                AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "ViewportToOverlayManagerWorld()");
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
        return worldPoint;
    }

    private void inflateWorldPointViews() {
        mWorldCoordX = (TextView) mRootView.findViewById(R.id.fragment_amctmap_world_coord_x_value);
        mWorldCoordY = (TextView) mRootView.findViewById(R.id.fragment_amctmap_world_coord_y_value);
        mWorldCoordZ = (TextView) mRootView.findViewById(R.id.fragment_amctmap_world_coord_Z_value);
        mScale = (TextView) mRootView.findViewById(R.id.fragment_amctmap_map_scale_value);
        mFPSAvg = (TextView) mRootView.findViewById(R.id.fragment_amctmap_map_fpsavg_value);
    }

    public byte[] readBytes(InputStream inputStream) {
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

    private byte[] ReadFileFromAsset(View v, String path) {
        AssetManager assetManager = v.getContext().getAssets();
        InputStream schemeBuffer = null;
        byte[] buffer = null;
        try {
            schemeBuffer = assetManager.open(path);
            buffer = readBytes(schemeBuffer);
        } catch (Exception e) {
            e.printStackTrace();
        }
        return buffer;
    }

    private void CreateItemsForTests() {
        mView.queueEvent(new Runnable() {
            @Override
            public void run() {
               /* IMcArcItem arc;
                IMcArrowItem arrow;
                IMcLineExpansionItem lineExp;
                IMcPolygonItem poly;
                IMcEllipseItem ellipse;
                try {


                    CMcEnumBitField<IMcObjectSchemeItem.EItemSubTypeFlags> itemSubType = new CMcEnumBitField<>(IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN);

                    //arc = IMcArcItem.Static.Create(itemSubType,ObjectPropertiesBase.mLocationCoordSys);
                    //arrow = IMcArrowItem.Static.Create(itemSubType,ObjectPropertiesBase.mLocationCoordSys);
                    //poly = IMcPolygonItem.Static.Create(itemSubType);


                    //ellipse = IMcEllipseItem.Static.Create(itemSubType, ObjectPropertiesBase.mLocationCoordSys);
                    //lineExp = IMcLineExpansionItem.Static.Create(itemSubType, ObjectPropertiesBase.mLocationCoordSys, IMcObjectSchemeItem.EGeometryType.EGT_GEOGRAPHIC);

                    //IMcPictureItem pic = IMcPictureItem.Static.Create(itemSubType, EMcPointCoordSystem.EPCS_SCREEN, ObjectPropertiesBase.mFillTexture);
                    //IMcTextItem textItem = IMcTextItem.Static.Create(itemSubType,EMcPointCoordSystem.EPCS_SCREEN, ObjectPropertiesBase.Text.getInstance().mTextFont);
                   // IMcRectangleItem rectangleItem = IMcRectangleItem.Static.Create(itemSubType,ObjectPropertiesBase.mLocationCoordSys);
                    //IMcLineItem lineItem = IMcLineItem.Static.Create(itemSubType);

                    int i = 0;
                    int v = 9 + i;
                } catch (MapCoreException e) {
                    e.printStackTrace();
                    AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "GetOverlayManager()");
                } catch (Exception e) {
                    e.printStackTrace();
                }*/

                //IMcObjectScheme[] objectSchemesRect = null;
                IMcObjectScheme[] objectSchemesLine = null;
                ///IMcObjectScheme[] objectSchemesText = null;
                //IMcObjectScheme[] objectSchemesPicture = null;
                SMcVector3D[] locationPoints = new SMcVector3D[0];
                IMcOverlayManager activeOverlayManager = null;
                try {
                    activeOverlayManager = Manager_AMCTMapForm.getInstance().getCurViewport().GetOverlayManager();
                } catch (MapCoreException e) {
                    e.printStackTrace();
                    AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "GetOverlayManager()");
                } catch (Exception e) {
                    e.printStackTrace();
                }
                if (activeOverlayManager != null) {
                    IMcOverlay activeOverlay = Manager_MCOverlayManager.getInstance().getActiveOverlay();
                    if (activeOverlay != null) {
                        try {
                            //byte[] bufferRect = ReadFileFromAsset(mRootView, Funcs.getFilePathByProperties(Consts.SCHEMES_FILES_RECTANGLE));
                            byte[] bufferLine = ReadFileFromAsset(mRootView, Funcs.getFilePathByProperties(Consts.SCHEMES_FILES_LINE));
                            //byte[] bufferText = ReadFileFromAsset(mRootView, Funcs.getFilePathByProperties(Consts.SCHEMES_FILES_TEXT));
                            //byte[] bufferPicture = ReadFileFromAsset(mRootView, Funcs.getFilePathByProperties(Consts.SCHEMES_FILES_PICTURE));
                            //objectSchemesRect = activeOverlayManager.LoadObjectSchemes(bufferRect);
                            objectSchemesLine = activeOverlayManager.LoadObjectSchemes(bufferLine);
                            //objectSchemesText = activeOverlayManager.LoadObjectSchemes(bufferText);
                            //objectSchemesPicture = activeOverlayManager.LoadObjectSchemes(bufferPicture);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(mRootView.getContext(), e, "IMcOverlayManager.LoadObjectSchemes");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        // IMcObjectScheme schemeRect = objectSchemesRect[0];
                        IMcObjectScheme schemeLine = objectSchemesLine[0];

                        // IMcObjectScheme schemeText = objectSchemesText[0];
                        // IMcObjectScheme schemePicture = objectSchemesPicture[0];

                        try {
                            schemeLine.SetID(450);
                            int lineId = schemeLine.GetID();
                            //   IMcRectangleItem nodeRect = (IMcRectangleItem) schemeRect.GetNodeByID(Id.Item_ObjectSchemeNode_NodeId.getValue());
                            IMcLineItem nodeLine = (IMcLineItem) schemeLine.GetNodeByID(Id.Item_ObjectSchemeNode_NodeId.getValue());
                            nodeLine.SetID(555);
                            int lineId2 = nodeLine.GetID();
                            //   IMcTextItem nodeText = (IMcTextItem) schemeText.GetNodeByID(Id.Item_ObjectSchemeNode_NodeId.getValue());
                            //   IMcPictureItem nodePicture = (IMcPictureItem) schemePicture.GetNodeByID(Id.Item_ObjectSchemeNode_NodeId.getValue());

                            // setPrivateProperties(mRootView, obj, node);
                            /*    mView.getEditMode().SetUtilityItems(nodeRect,nodeLine,nodeText);
                                ObjectRef<IMcRectangleItem> rect = new ObjectRef<IMcRectangleItem>();
                                ObjectRef<IMcLineItem> line = new ObjectRef<IMcLineItem>();
                                ObjectRef<IMcTextItem> text = new ObjectRef<IMcTextItem>();

                                mView.getEditMode().GetUtilityItems(rect,line,text);

                                mView.getEditMode().SetUtilityPicture(nodePicture, IMcEditMode.EUtilityPictureType.EUPT_ITEM_ROTATE_ACTIVE);
                                IMcPictureItem picture = mView.getEditMode().GetUtilityPicture(IMcEditMode.EUtilityPictureType.EUPT_ITEM_ROTATE_ACTIVE);

                                mView.getEditMode().SetUtility3DEditItem(nodeLine, IMcEditMode.EUtility3DEditItemType.EUEIT_MOVE_ITEM_CENTER_REGULAR);
                                IMcLineItem item = (IMcLineItem)mView.getEditMode().GetUtility3DEditItem(IMcEditMode.EUtility3DEditItemType.EUEIT_MOVE_ITEM_CENTER_REGULAR);
                            int i=0;
                                int j = i+9;
*/
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(mRootView.getContext(), e, "McMapDevice.GetNodeByID");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        // }
                    } else
                        AlertMessages.ShowGenericMessage(mRootView.getContext(), "Missing Overlay", "There is no active overlay");
                } else
                    AlertMessages.ShowGenericMessage(mRootView.getContext(), "Missing Overlay Manager", "There is no active overlay manager");
            }
        });
    }

    private void LoadScheme(final String schemeFile) {
        mView.queueEvent(new Runnable() {
            @Override
            public void run() {
                m_LastObjInitMode = null;
                IMcObjectScheme[] objectSchemes = null;
                SMcVector3D[] locationPoints = new SMcVector3D[0];
                IMcOverlayManager activeOverlayManager = null;
                try {
                    activeOverlayManager = Manager_AMCTMapForm.getInstance().getCurViewport().GetOverlayManager();
                } catch (final MapCoreException e) {
                    e.printStackTrace();
                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "GetOverlayManager()");
                        }
                    });
                } catch (Exception e) {
                    e.printStackTrace();
                }
                if (activeOverlayManager != null) {
                    IMcOverlay activeOverlay = Manager_MCOverlayManager.getInstance().getActiveOverlay();
                    if (activeOverlay != null) {
                        try {
                            byte[] buffer = ReadFileFromAsset(mRootView, Funcs.getFilePathByProperties(schemeFile));
                            objectSchemes = activeOverlayManager.LoadObjectSchemes(buffer);
                        } catch (final MapCoreException e) {
                            getActivity().runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    AlertMessages.ShowMapCoreErrorMessage(mRootView.getContext(), e, "IMcOverlayManager.LoadObjectSchemes");
                                }
                            });
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        if (objectSchemes != null && objectSchemes.length > 0) {
                            IMcObjectScheme scheme = objectSchemes[0];

                            IMcObject obj = null;

                            try {

                                obj = IMcObject.Static.Create(activeOverlay,
                                        scheme,
                                        locationPoints);

                            } catch (final MapCoreException e) {
                                getActivity().runOnUiThread(new Runnable() {
                                    @Override
                                    public void run() {
                                        AlertMessages.ShowMapCoreErrorMessage(mRootView.getContext(), e, "IMcObject.Create");
                                    }
                                });
                            } catch (Exception e) {
                                e.printStackTrace();
                            }
                            try {
                                IMcObjectSchemeItem node = (IMcObjectSchemeItem) scheme.GetNodeByID(Id.Item_ObjectSchemeNode_NodeId.getValue());
                                setPrivateProperties(mRootView, obj, node);
                                mView.StartInitMode(obj, node);

                                m_LastObjInitMode = obj;
                            } catch (final MapCoreException e) {
                                getActivity().runOnUiThread(new Runnable() {
                                    @Override
                                    public void run() {
                                        AlertMessages.ShowMapCoreErrorMessage(mRootView.getContext(), e, "McMapDevice.GetNodeByID");
                                    }
                                });
                            } catch (Exception e) {
                                e.printStackTrace();
                            }
                        }
                    } else
                        getActivity().runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                AlertMessages.ShowGenericMessage(mRootView.getContext(), "Missing Overlay", "There is no active overlay");
                            }
                        });
                } else
                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            AlertMessages.ShowGenericMessage(mRootView.getContext(), "Missing Overlay Manager", "There is no active overlay manager");
                        }
                    });
            }

        });
    }

    public void DrawPolygonForScan() {
        DrawObject(null, null, DrawObjectType.polygon, true);
    }

    public void DrawBoxForScan() {
        DrawObject(null, null, DrawObjectType.rectangle, true);
    }

    public void DrawObjectForCS(final SMcVector3D[] points, Observer observer) {
        DrawObject(points, observer,  DrawObjectType.polygon, false);
    }

    public void DrawObjectForSectionMap(final SMcVector3D[] points, Observer observer) {
        DrawObject(points, observer,  DrawObjectType.line, false);
    }

    private void DrawObject(final SMcVector3D[] points, Observer observer, final DrawObjectType drawObjectType, final boolean isForScan) {
        final DrawObjectResult drawObjectResult = new DrawObjectResult();
        if (observer != null)
            drawObjectResult.addObserver(observer);
        mView.queueEvent(new Runnable() {
            @Override
            public void run() {
                m_LastObjInitMode = null;
                SMcVector3D[] locationPoints = new SMcVector3D[0];
                IMcOverlayManager activeOverlayManager = null;
                try {
                    activeOverlayManager = Manager_AMCTMapForm.getInstance().getCurViewport().GetOverlayManager();
                } catch (MapCoreException e) {
                    e.printStackTrace();
                    AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "GetOverlayManager()");
                } catch (Exception e) {
                    e.printStackTrace();
                }
                if (activeOverlayManager != null) {
                    IMcOverlay activeOverlay = Manager_MCOverlayManager.getInstance().getActiveOverlay();
                    if (activeOverlay != null) {
                        CMcEnumBitField<IMcObjectSchemeItem.EItemSubTypeFlags> itemSubType = new CMcEnumBitField<>(IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN);
                        IMcClosedShapeItem.EFillStyle itemFillStyle = IMcClosedShapeItem.EFillStyle.EFS_SOLID;
                        EMcPointCoordSystem pointCoordSystem = EMcPointCoordSystem.EPCS_SCREEN;
                        if (!isForScan) {
                            itemSubType.Set(IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_ATTACHED_TO_TERRAIN);
                            itemFillStyle = IMcClosedShapeItem.EFillStyle.EFS_NONE;
                            pointCoordSystem = EMcPointCoordSystem.EPCS_WORLD;
                        }
                        try {
                            IMcObjectSchemeItem objectSchemeItem = null;
                            if (drawObjectType == DrawObjectType.polygon)
                                objectSchemeItem = IMcPolygonItem.Static.Create(itemSubType,
                                        IMcLineBasedItem.ELineStyle.ELS_SOLID,
                                        SMcBColor.bcBlackOpaque,
                                        2f,
                                        null,
                                        new SMcFVector2D(0, -1),
                                        1f,
                                        itemFillStyle,
                                        new SMcBColor(0, 255, 100, 100));
                            else if (drawObjectType == DrawObjectType.rectangle)
                                objectSchemeItem = IMcRectangleItem.Static.Create(itemSubType,
                                        EMcPointCoordSystem.EPCS_SCREEN,
                                        IMcObjectSchemeItem.EGeometryType.EGT_GEOMETRIC_IN_VIEWPORT,
                                        IMcRectangleItem.ERectangleDefinition.ERD_RECTANGLE_DIAGONAL_POINTS,
                                        0f,
                                        0f,
                                        IMcLineBasedItem.ELineStyle.ELS_SOLID,
                                        SMcBColor.bcBlackOpaque,
                                        2f,
                                        null,
                                        new SMcFVector2D(0, -1),
                                        1f,
                                        IMcClosedShapeItem.EFillStyle.EFS_SOLID,
                                        new SMcBColor(0, 255, 100, 100));

                            else if(drawObjectType == DrawObjectType.line)
                            {
                                objectSchemeItem = IMcLineItem.Static.Create(itemSubType);
                            }

                            IMcObject obj = IMcObject.Static.Create(activeOverlay,
                                    objectSchemeItem,
                                    pointCoordSystem,
                                    (points == null) ? locationPoints : points);

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

                            obj.SetConditionalSelector(IMcConditionalSelector.EActionType.EAT_VISIBILITY,
                                    true,
                                    viewportSelector);

                            drawObjectResult.updateDrawObjectResult(objectSchemeItem, obj);

                            mView.StartInitMode(obj, objectSchemeItem);
                            m_LastObjInitMode = obj;
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(mRootView.getContext(), e, "PolyScan");

                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    } else
                        AlertMessages.ShowGenericMessage(mRootView.getContext(), "Missing Overlay", "There is no active overlay");
                } else
                    AlertMessages.ShowGenericMessage(mRootView.getContext(), "Missing Overlay Manager", "There is no active overlay manager");
            }
        });
    }

    private void setPrivateProperties(final View v, final IMcObject obj, IMcObjectSchemeItem objectSchemeItem) {
        //checkNewWrappedSymbolicItemFunctions(v, obj, objectSchemeItem);
        //checkNewWrappedPhysicalItem(objectSchemeItem);
        try {
            //IMcProperty.SVariantProperty[] all = obj.GetProperties();

            // General
            obj.SetBoolProperty(Id.ObjectLocation_ObjectLocation_RelativeToDTM.getValue(), ObjectPropertiesBase.mLocationRelativeToDtm);
            obj.SetUIntProperty(Id.ObjectLocation_ObjectLocation_MaxNumOfPoints.getValue(), ObjectPropertiesBase.mLocationMaxPoints);
            obj.SetEnumProperty(Id.Item_ObjectSchemeNode_VisibilityOption.getValue(), (int) ObjectPropertiesBase.mVisibilityOption.getValue());
            obj.SetEnumProperty(Id.Item_ObjectSchemeNode_TransformOption.getValue(), (int) ObjectPropertiesBase.mTransformOption.getValue());
            obj.SetByteProperty(Id.Item_ObjectSchemeItem_BlockedTransparency.getValue(), ObjectPropertiesBase.mBlockedTransparency);

            if(objectSchemeItem instanceof IMcSymbolicItem) {
                obj.SetEnumProperty(Id.Item_SymbolicItem_General_DrawPriorityGroup.getValue(), (int) ObjectPropertiesBase.mDrawPriorityGroup.getValue());
                obj.SetSByteProperty(Id.Item_SymbolicItem_General_DrawPriority.getValue(), (byte) ObjectPropertiesBase.mDrawPriority);
                obj.SetSByteProperty(Id.Item_SymbolicItem_General_Conplanar3dPriority.getValue(), (byte) ObjectPropertiesBase.mConplanar3dPriority);
                obj.SetEnumProperty(Id.Item_SymbolicItem_AttachPoint_PointType.getValue(), (int) ObjectPropertiesBase.mAttachPointType.getValue());
                obj.SetEnumProperty(Id.Item_SymbolicItem_AttachPoint_BoundingBoxType.getValue(), (int) ObjectPropertiesBase.mBoundingBoxType.getValue());
                obj.SetIntProperty(Id.Item_SymbolicItem_AttachPoint_NumPoints.getValue(), (int) ObjectPropertiesBase.mAttachPointNumPoints);
                obj.SetIntProperty(Id.Item_SymbolicItem_AttachPoint_PointIndex.getValue(), (int) ObjectPropertiesBase.mAttachPointIndex);
                obj.SetFloatProperty(Id.Item_SymbolicItem_AttachPoint_PositionValue.getValue(), (int) ObjectPropertiesBase.mAttachPointPositionValue);
                obj.SetByteProperty(Id.Item_SymbolicItem_General_Transparency.getValue(), ObjectPropertiesBase.mTransparency);
                obj.SetFloatProperty(Id.Item_SymbolicItem_General_MoveIfBlockedMax.getValue(), ObjectPropertiesBase.mMoveIfBlockedMax);
                obj.SetFloatProperty(Id.Item_SymbolicItem_General_MoveIfBlockedHeight.getValue(), ObjectPropertiesBase.mMoveIfBlockedHeight);
                obj.SetUIntProperty(Id.Item_SymbolicItem_Transform_VectorTransformSegment.getValue(), ObjectPropertiesBase.mVectorTransformSegment);
                obj.SetEnumProperty(Id.Item_SymbolicItem_Offset_VectorOffsetCalc.getValue(), (int) ObjectPropertiesBase.mVectorOffsetCalc.getValue());
                obj.SetEnumProperty(Id.Item_SymbolicItem_Offset_OffsetOrientation.getValue(), (int) ObjectPropertiesBase.mOrientation.getValue());
                obj.SetFloatProperty(Id.Item_SymbolicItem_Offset_VectorOffsetValue.getValue(), ObjectPropertiesBase.mVectorOffsetValue);
                obj.SetFVector3DProperty(Id.Item_SymbolicItem_Offset_Offset.getValue(), ObjectPropertiesBase.mOffset);
                obj.SetFloatProperty(Id.Item_SymbolicItem_Rotation_Yaw.getValue(), ObjectPropertiesBase.mRotationYaw);
                obj.SetFloatProperty(Id.Item_SymbolicItem_Rotation_Pitch.getValue(), ObjectPropertiesBase.mRotationPitch);
                obj.SetFloatProperty(Id.Item_SymbolicItem_Rotation_Roll.getValue(), ObjectPropertiesBase.mRotationRoll);
                obj.SetBoolProperty(Id.Item_SymbolicItem_Rotation_VectorRotation.getValue(), ObjectPropertiesBase.mVectorRotation);
                obj.SetArrayProperty(Id.Item_SymbolicItem_Offset_PointsIndicesAndDuplications.getValue(), IMcProperty.EPropertyType.EPT_INT_ARRAY, ObjectPropertiesBase.mPointsIndicesAndDuplications);
                obj.SetArrayProperty(Id.Item_SymbolicItem_Offset_PointsOffset.getValue(), IMcProperty.EPropertyType.EPT_FVECTOR3D_ARRAY, ObjectPropertiesBase.mPointsOffset);

            }
            // Line Based
            if (objectSchemeItem instanceof IMcLineBasedItem) {

                obj.SetFloatProperty(Id.Item_LineBased_LineWidth.getValue(), ObjectPropertiesBase.mLineWidth);
                obj.SetBColorProperty(Id.Item_LineBased_LineColor.getValue(), ObjectPropertiesBase.mLineColor);
                obj.SetEnumProperty(Id.Item_LineBased_LineStyle.getValue(),(int) ObjectPropertiesBase.mLineStyle.getValue());
                obj.SetFVector2DProperty(Id.Item_LineBased_LineTextureHeightRange.getValue(), ObjectPropertiesBase.mLineTextureHeightRange);
                obj.SetTextureProperty(Id.Item_LineBased_LineTexture.getValue(), ObjectPropertiesBase.mLineTexture);
                obj.SetByteProperty(Id.Item_LineBased_NumSmoothingLevels.getValue(), ObjectPropertiesBase.mLineBasedSmoothingLevels);
                obj.SetFloatProperty(Id.Item_LineBased_OutlineWidth.getValue(), ObjectPropertiesBase.mLineOutlineWidth);
                obj.SetBColorProperty(Id.Item_LineBased_OutlineColor.getValue(), ObjectPropertiesBase.mLineOutlineColor);
                ((IMcLineBasedItem) objectSchemeItem).SetGreatCirclePrecision(ObjectPropertiesBase.mLineBasedGreatCirclePrecision);
                obj.SetFloatProperty(Id.Item_LineBased_TextureScale.getValue(), ObjectPropertiesBase.mLineTextureScale);
                obj.SetEnumProperty(Id.Item_LineBased_LineTextureFlipMode.getValue(), (int) ObjectPropertiesBase.mLineTextureFlipMode.getValue());

                obj.SetEnumProperty(Id.Item_SightPresentation_SightPresentationType.getValue(), (int) ObjectPropertiesBase.SightPresentation.type.getValue());

                if (ObjectPropertiesBase.SightPresentation.colorsByVisibility.containsKey(IMcSpatialQueries.EPointVisibility.EPV_SEEN))
                    obj.SetBColorProperty(Id.Item_SightPresentation_SightColor_Seen.getValue(), ObjectPropertiesBase.SightPresentation.colorsByVisibility.get(IMcSpatialQueries.EPointVisibility.EPV_SEEN));
                if (ObjectPropertiesBase.SightPresentation.colorsByVisibility.containsKey(IMcSpatialQueries.EPointVisibility.EPV_UNSEEN))
                    obj.SetBColorProperty(Id.Item_SightPresentation_SightColor_Unseen.getValue(), ObjectPropertiesBase.SightPresentation.colorsByVisibility.get(IMcSpatialQueries.EPointVisibility.EPV_UNSEEN));
                if (ObjectPropertiesBase.SightPresentation.colorsByVisibility.containsKey(IMcSpatialQueries.EPointVisibility.EPV_UNKNOWN))
                    obj.SetBColorProperty(Id.Item_SightPresentation_SightColor_Unknown.getValue(), ObjectPropertiesBase.SightPresentation.colorsByVisibility.get(IMcSpatialQueries.EPointVisibility.EPV_UNKNOWN));
                if (ObjectPropertiesBase.SightPresentation.colorsByVisibility.containsKey(IMcSpatialQueries.EPointVisibility.EPV_OUT_OF_QUERY_AREA))
                    obj.SetBColorProperty(Id.Item_SightPresentation_SightColor_OutOfQueryArea.getValue(), ObjectPropertiesBase.SightPresentation.colorsByVisibility.get(IMcSpatialQueries.EPointVisibility.EPV_OUT_OF_QUERY_AREA));
                if (ObjectPropertiesBase.SightPresentation.colorsByVisibility.containsKey(IMcSpatialQueries.EPointVisibility.EPV_SEEN_STATIC_OBJECT))
                    obj.SetBColorProperty(Id.Item_SightPresentation_SightColor_StaticObject.getValue(), ObjectPropertiesBase.SightPresentation.colorsByVisibility.get(IMcSpatialQueries.EPointVisibility.EPV_SEEN_STATIC_OBJECT));
                if (ObjectPropertiesBase.SightPresentation.colorsByVisibility.containsKey(IMcSpatialQueries.EPointVisibility.EPV_ASYNC_CALCULATING))
                    obj.SetBColorProperty(Id.Item_SightPresentation_SightColor_AsyncCalculation.getValue(), ObjectPropertiesBase.SightPresentation.colorsByVisibility.get(IMcSpatialQueries.EPointVisibility.EPV_ASYNC_CALCULATING));

                obj.SetFloatProperty(Id.Item_SightPresentation_SightObserverHeight.getValue(), ObjectPropertiesBase.SightPresentation.observerHeight);
                obj.SetFloatProperty(Id.Item_SightPresentation_SightObservedHeight.getValue(), ObjectPropertiesBase.SightPresentation.observedHeight);
                obj.SetFloatProperty(Id.Item_SightPresentation_SightObserverMaxPitch.getValue(), ObjectPropertiesBase.SightPresentation.maxPitch);
                obj.SetFloatProperty(Id.Item_SightPresentation_SightObserverMinPitch.getValue(), ObjectPropertiesBase.SightPresentation.minPitch);
                obj.SetBoolProperty(Id.Item_SightPresentation_IsSightObserverHeightAbsolute.getValue(), ObjectPropertiesBase.SightPresentation.isObserverHeightAbsolute);
                obj.SetBoolProperty(Id.Item_SightPresentation_IsSightObservedHeightAbsolute.getValue(), ObjectPropertiesBase.SightPresentation.isObservedHeightAbsolute);
                obj.SetUIntProperty(Id.Item_SightPresentation_SightNumEllipseRays.getValue(), ObjectPropertiesBase.SightPresentation.numEllipseRays);
                obj.SetEnumProperty(Id.Item_SightPresentation_SightQueryPrecision.getValue(), (int) ObjectPropertiesBase.SightPresentation.precision.getValue());
                obj.SetFloatProperty(Id.Item_SightPresentation_TextureResolution.getValue(), ObjectPropertiesBase.SightPresentation.sightTextureResolution);

                obj.SetFloatProperty(Id.Item_LineBased_VerticalHeight.getValue(), ObjectPropertiesBase.mVerticalHeight);
                obj.SetBColorProperty(Id.Item_LineBased_SidesFillColor.getValue(), ObjectPropertiesBase.mSidesFillColor);
                obj.SetEnumProperty(Id.Item_LineBased_SidesFillStyle.getValue(), (int) ObjectPropertiesBase.mSidesFillStyle.getValue());
                obj.SetTextureProperty(Id.Item_LineBased_SidesFillTexture.getValue(), ObjectPropertiesBase.mSidesFillTexture);
                obj.SetFVector2DProperty(Id.Item_LineBased_SidesFillTextureScale.getValue(), ObjectPropertiesBase.mSidesFillTextureScale);
                ((IMcLineBasedItem)objectSchemeItem).SetShapeType(ObjectPropertiesBase.mShapeType);
            }

            // Closed Shaped
            if (objectSchemeItem instanceof IMcClosedShapeItem) {

                obj.SetBColorProperty(Id.Item_ClosedShape_FillColor.getValue(), ObjectPropertiesBase.mFillColor);
                obj.SetEnumProperty(Id.Item_ClosedShape_FillStyle.getValue(), (int) ObjectPropertiesBase.mFillStyle.getValue());
                obj.SetTextureProperty(Id.Item_ClosedShape_FillTexture.getValue(), ObjectPropertiesBase.mFillTexture);
                obj.SetFVector2DProperty(Id.Item_ClosedShape_FillTextureScale.getValue(), ObjectPropertiesBase.mFillTextureScale);
            }

            IMcObjectSchemeNode.EObjectSchemeNodeType type = objectSchemeItem.GetNodeType();
            switch (type) {
                case ARC_ITEM:
                    ((IMcArcItem) objectSchemeItem).SetEllipseDefinition(ObjectPropertiesBase.mArcEllipseDefinition);
                    obj.SetFloatProperty(Id.Item_Arc_StartAngle.getValue(), ObjectPropertiesBase.mArcStartAngle);
                    obj.SetFloatProperty(Id.Item_Arc_EndAngle.getValue(), ObjectPropertiesBase.mArcEndAngle);
                    obj.SetFloatProperty(Id.Item_Arc_RadiusX.getValue(), ObjectPropertiesBase.mArcRadiusX);
                    obj.SetFloatProperty(Id.Item_Arc_RadiusY.getValue(), ObjectPropertiesBase.mArcRadiusY);
                    break;
                case ARROW_ITEM:
                    obj.SetFloatProperty(Id.Item_Arrow_HeadAngle.getValue(), ObjectPropertiesBase.mArrowHeadAngle);
                    obj.SetFloatProperty(Id.Item_Arrow_HeadSize.getValue(), ObjectPropertiesBase.mArrowHeadSize);
                    obj.SetFloatProperty(Id.Item_Arrow_GapSize.getValue(), ObjectPropertiesBase.mArrowGapSize);
                    break;
                case LINE_EXPANSION_ITEM:
                    obj.SetFloatProperty(Id.Item_LineExpansionItem_Radius.getValue(), ObjectPropertiesBase.mLineExpansionRadius);
                    break;
                case PICTURE_ITEM:
                    obj.SetFloatProperty(Id.Item_Picture_Width.getValue(), ObjectPropertiesBase.mPicWidth);
                    obj.SetFloatProperty(Id.Item_Picture_Height.getValue(), ObjectPropertiesBase.mPicHeight);
                    obj.SetTextureProperty(Id.Item_Picture_Texture.getValue(), ObjectPropertiesBase.mPicTexture);
                    obj.SetBColorProperty(Id.Item_Picture_TextureColor.getValue(), ObjectPropertiesBase.mPicTextureColor);
                    obj.SetEnumProperty(Id.Item_Picture_RectAlignment.getValue(), (int) ObjectPropertiesBase.mPicRectAlignment.getValue());
                    obj.SetBoolProperty(Id.Item_Picture_IsSizeFactor.getValue(), ObjectPropertiesBase.mPictureIsSizeFactor);
                    obj.SetBoolProperty(Id.Item_Picture_NeverUpsideDown.getValue(), ObjectPropertiesBase.mPictureNeverUpsideDown);
                    break;
                case POLYGON_ITEM:
                    break;
                case TEXT_ITEM:
                    SMcVariantString strValue = new SMcVariantString(ObjectPropertiesBase.Text.getInstance().mTextString, ObjectPropertiesBase.Text.getInstance().mTextIsUnicode);
                    obj.SetStringProperty(Id.Item_Text_Text.getValue(), strValue);
                    //strValue = obj.GetStringProperty(Id.Item_Text_Text.getValue());

                    obj.SetFontProperty(Id.Item_Text_Font.getValue(), ObjectPropertiesBase.Text.getInstance().mTextFont);
                    //IMcFont font = obj.GetFontProperty(Id.Item_Text_Font.getValue());
                    obj.SetFVector2DProperty(Id.Item_TextBoundingRect_Scale.getValue(), ObjectPropertiesBase.Text.getInstance().mTextScale);
                    //obj.GetFVector2DProperty(Id.Item_TextBoundingRect_Scale.getValue());
                    obj.SetUIntProperty(Id.Item_TextBoundingRect_Margin.getValue(), ObjectPropertiesBase.Text.getInstance().mTextMargin);
                    obj.SetBColorProperty(Id.Item_Text_Color.getValue(), ObjectPropertiesBase.Text.getInstance().mTextColor);
                    obj.SetBColorProperty(Id.Item_TextBoundingRect_BackgroungColor.getValue(), ObjectPropertiesBase.Text.getInstance().mTextBackgroundColor);
                    obj.SetEnumProperty(Id.Item_Text_Alignment.getValue(), (int) ObjectPropertiesBase.Text.getInstance().mTextAlignment.getValue());
                    obj.SetBoolProperty(Id.Item_Text_RightToLeftReadingOrder.getValue(), ObjectPropertiesBase.Text.getInstance().mTextRtlReadingOrder);
                    obj.SetBColorProperty(Id.Item_Text_OutlineColor.getValue(), ObjectPropertiesBase.Text.getInstance().mTextOutlineColor);
                    obj.SetEnumProperty(Id.Item_Text_NeverUpsideDown.getValue(), ObjectPropertiesBase.Text.getInstance().mNeverUpsideDown.getValue());
                    obj.SetUIntProperty(Id.Item_TextBoundingRect_MarginY.getValue(), ObjectPropertiesBase.Text.getInstance().mTextMarginY);
                    obj.SetEnumProperty(Id.Item_TextBoundingRect_RectAlignment.getValue(), (int) ObjectPropertiesBase.Text.getInstance().mTextRectAlignment.getValue());
                    obj.SetEnumProperty(Id.Item_TextBoundingRect_BackgroundShape.getValue(), (int) ObjectPropertiesBase.Text.getInstance().mBackgroundShape.getValue());
                    break;

                case ELLIPSE_ITEM:
                    ((IMcEllipseItem) objectSchemeItem).SetEllipseDefinition(ObjectPropertiesBase.mEllipseDefinition);
                    obj.SetFloatProperty(Id.Item_Ellipse_InnerRadiusFactor.getValue(), ObjectPropertiesBase.mEllipseInnerRadiusFactor);
                    obj.SetFloatProperty(Id.Item_Ellipse_StartAngle.getValue(), ObjectPropertiesBase.mEllipseStartAngle);
                    obj.SetFloatProperty(Id.Item_Ellipse_EndAngle.getValue(), ObjectPropertiesBase.mEllipseEndAngle);
                    obj.SetFloatProperty(Id.Item_Ellipse_RadiusX.getValue(), ObjectPropertiesBase.mEllipseRadiusX);
                    obj.SetFloatProperty(Id.Item_Ellipse_RadiusY.getValue(), ObjectPropertiesBase.mEllipseRadiusY);
                    break;
                case RECTANGLE_ITEM:
                    ((IMcRectangleItem) objectSchemeItem).SetRectangleDefinition(ObjectPropertiesBase.mRectangleDefinition);
                    obj.SetFloatProperty(Id.Item_Rectangle_RadiusX.getValue(), ObjectPropertiesBase.mRectRadiusX);
                    obj.SetFloatProperty(Id.Item_Rectangle_RadiusY.getValue(), ObjectPropertiesBase.mRectRadiusY);
                    break;
                case MESH_ITEM:
                    obj.SetMeshProperty(Id.Item_Mesh_Mesh.getValue(), ObjectPropertiesBase.mMesh);
                    break;
            }

        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(v.getContext(), e, "IMcObject.SetProperty");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void checkNewWrappedSymbolicItemFunctions(final View v, final IMcObject obj, IMcObjectSchemeItem objectSchemeItem) {
        try {
            // General

            CMcEnumBitField<IMcObjectSchemeItem.EItemSubTypeFlags> itemSubType = new CMcEnumBitField<>(IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN);
            // *OK* CMcEnumBitField<IMcObjectSchemeItem.EItemSubTypeFlags> itemSubType = objectSchemeItem.GetItemSubType();
            // *OK* boolean bHiddenIfViewportOverloaded = objectSchemeItem.GetHiddenIfViewportOverloaded();
            // *OK* objectSchemeItem.SetHiddenIfViewportOverloaded(true);
            // *OK* bHiddenIfViewportOverloaded = objectSchemeItem.GetHiddenIfViewportOverloaded();

            if(objectSchemeItem instanceof IMcSymbolicItem) {
                /*ObjectRef<Float> fPositionValue = null;
                ((IMcSymbolicItem) objectSchemeItem).GetAttachPointPositionValue(0, fPositionValue);
                fPositionValue = new ObjectRef<>();
                ((IMcSymbolicItem) objectSchemeItem).GetAttachPointPositionValue(0, fPositionValue);
                ObjectRef<Long> id = new ObjectRef();
                ((IMcSymbolicItem) objectSchemeItem).GetAttachPointPositionValue(0, fPositionValue, id);

                int i=0;
               int j=i+1;*/
                /*// *OK* IMcSymbolicItem CloneItemWithoutObject = ((IMcSymbolicItem) objectSchemeItem).Clone();
                // *OK* IMcSymbolicItem CloneItem = ((IMcSymbolicItem) objectSchemeItem).Clone(obj);

                // *OK* IMcSymbolicItem.EAttachPointType eType = ((IMcSymbolicItem) objectSchemeItem).GetAttachPointType(0);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetAttachPointType(0, IMcSymbolicItem.EAttachPointType.EAPT_NONE);
                // *OK* eType = ((IMcSymbolicItem) objectSchemeItem).GetAttachPointType(0);

                // *OK* CMcEnumBitField<IMcSymbolicItem.EBoundingBoxPointFlags> uBoundingBoxPointBitField = ((IMcSymbolicItem) objectSchemeItem).GetBoundingBoxAttachPointType(0);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetBoundingBoxAttachPointType(0, new CMcEnumBitField<IMcSymbolicItem.EBoundingBoxPointFlags>());
                // *OK* uBoundingBoxPointBitField = ((IMcSymbolicItem) objectSchemeItem).GetBoundingBoxAttachPointType(0);

                // Integer nPointIndex = new Integer(2);
                // Long puPropertyID = new Long(0);
                // byte uObjectStateToServe1 = 1;
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetAttachPointIndex(0, nPointIndex);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetAttachPointIndex(0, nPointIndex, puPropertyID, uObjectStateToServe1);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetAttachPointIndex(0, 1);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetAttachPointIndex(0, nPointIndex);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetAttachPointIndex(0, 2, 3L, uObjectStateToServe1);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetAttachPointIndex(0, nPointIndex, puPropertyID, uObjectStateToServe1);

                // Integer nNumPoints = new Integer(2);
                // puPropertyID = new Long(0);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetNumAttachPoints(0, nNumPoints);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetNumAttachPoints(0, nNumPoints, puPropertyID, uObjectStateToServe1);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetNumAttachPoints(0, 1);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetNumAttachPoints(0, nNumPoints);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetNumAttachPoints(0, 2, 3L, uObjectStateToServe1);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetNumAttachPoints(0, nNumPoints, puPropertyID, uObjectStateToServe1);

                // Float fPositionValue = new Float(2);
                // puPropertyID = new Long(0);

                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetAttachPointPositionValue(0, fPositionValue, puPropertyID, uObjectStateToServe1);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetAttachPointPositionValue(0, 1F);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetAttachPointPositionValue(0, fPositionValue);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetAttachPointPositionValue(0, 2F, 4L, uObjectStateToServe1);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetAttachPointPositionValue(0, fPositionValue, puPropertyID, uObjectStateToServe1);

                // *OK* IMcObjectSchemeItem.EGeometryType eOffsetType = ((IMcSymbolicItem) objectSchemeItem).GetOffsetType();
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetOffsetType(IMcObjectSchemeItem.EGeometryType.EGT_GEOMETRIC_IN_OVERLAY_MANAGER);
                // *OK* eOffsetType = ((IMcSymbolicItem) objectSchemeItem).GetOffsetType();

                // *OK* int uParentIndex = ((IMcSymbolicItem) objectSchemeItem).GetVectorTransformParentIndex();
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetVectorTransformParentIndex(2);
                // *OK* uParentIndex = ((IMcSymbolicItem) objectSchemeItem).GetVectorTransformParentIndex();

                // Integer uSegmentIndexOrType = new Integer(2);
                // puPropertyID = new Long(0);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetVectorTransformSegment(uSegmentIndexOrType);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetVectorTransformSegment(uSegmentIndexOrType, puPropertyID, uObjectStateToServe1);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetVectorTransformSegment(1);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetVectorTransformSegment(uSegmentIndexOrType);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetVectorTransformSegment(2, 3L, uObjectStateToServe1);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetVectorTransformSegment(uSegmentIndexOrType, puPropertyID, uObjectStateToServe1);

                 // *OK* IMcSymbolicItem.EVectorOffsetCalc eCalc = ((IMcSymbolicItem) objectSchemeItem).GetVectorOffsetCalc();
                 // *OK* ((IMcSymbolicItem) objectSchemeItem).SetVectorOffsetCalc(IMcSymbolicItem.EVectorOffsetCalc.EVOC_PARALLEL_RATIO);
                 // *OK* eCalc = ((IMcSymbolicItem) objectSchemeItem).GetVectorOffsetCalc();

                // ObjectRef<EMcPointCoordSystem> eCoordinateSystem = new ObjectRef<EMcPointCoordSystem>();
                // Boolean bEnabled = new Boolean(false);
                 // *OK* ((IMcSymbolicItem) objectSchemeItem).GetCoordinateSystemConversion(eCoordinateSystem);
                 // *OK* ((IMcSymbolicItem) objectSchemeItem).GetCoordinateSystemConversion(eCoordinateSystem, bEnabled);
                 // *OK* ((IMcSymbolicItem) objectSchemeItem).SetCoordinateSystemConversion(ObjectPropertiesBase.mLocationCoordSys);
                 // *OK* ((IMcSymbolicItem) objectSchemeItem).GetCoordinateSystemConversion(eCoordinateSystem);
                 // *OK* ((IMcSymbolicItem) objectSchemeItem).SetCoordinateSystemConversion(ObjectPropertiesBase.mArcCoordSys, true);
                 // *OK* ((IMcSymbolicItem) objectSchemeItem).GetCoordinateSystemConversion(eCoordinateSystem, bEnabled);

                // ObjectRef<EMcPointCoordSystem> eAlignToCoordinateSystem = new ObjectRef<EMcPointCoordSystem>();
                // Boolean bAlignYaw = new Boolean(false);
                // Boolean bAlignPitch = new Boolean(false);
                // Boolean bAlignRoll = new Boolean(false);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetRotationAlignment(eAlignToCoordinateSystem);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetRotationAlignment(eAlignToCoordinateSystem, bAlignYaw, bAlignPitch, bAlignRoll);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetRotationAlignment(ObjectPropertiesBase.mLocationCoordSys);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetRotationAlignment(eAlignToCoordinateSystem);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetRotationAlignment(ObjectPropertiesBase.mArcCoordSys, true, true, true);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetRotationAlignment(eAlignToCoordinateSystem, bAlignYaw, bAlignPitch, bAlignRoll);

                // Float fVectorOffsetValue = new Float(0);
                // puPropertyID = new Long(0);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetVectorOffsetValue(fVectorOffsetValue);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetVectorOffsetValue(fVectorOffsetValue, puPropertyID, uObjectStateToServe1);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetVectorOffsetValue(1F);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetVectorOffsetValue(fVectorOffsetValue);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetVectorOffsetValue(2F, 4L, uObjectStateToServe1);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetVectorOffsetValue(fVectorOffsetValue, puPropertyID, uObjectStateToServe1);

                // IMcProperty.SArrayProperty<Integer> anPointIndicesAndDuplicates =  new IMcProperty.SArrayProperty<Integer>();
                // puPropertyID = new Long(0);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetPointsDuplication(anPointIndicesAndDuplicates);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetPointsDuplication(anPointIndicesAndDuplicates, puPropertyID, uObjectStateToServe1);
                // Integer[] integer = new Integer[2];
                // integer[0] = new Integer(4);
                // integer[1] = new Integer(5);
                // IMcProperty.SArrayProperty<Integer> anPointIndicesAndDuplicates1 = new IMcProperty.SArrayProperty<Integer>(integer);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetPointsDuplication(anPointIndicesAndDuplicates1);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetPointsDuplication(anPointIndicesAndDuplicates);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetPointsDuplication(anPointIndicesAndDuplicates, 5L, uObjectStateToServe1);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetPointsDuplication(anPointIndicesAndDuplicates, puPropertyID, uObjectStateToServe1);

                // IMcProperty.SArrayProperty<SMcFVector3D> aDuplicationOffsets =  new IMcProperty.SArrayProperty<SMcFVector3D>();
                // puPropertyID = new Long(0);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetPointsDuplicationOffsets(aDuplicationOffsets);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetPointsDuplicationOffsets(aDuplicationOffsets, puPropertyID, uObjectStateToServe1);
                // SMcFVector3D[] vector3D = new SMcFVector3D[1];
                // vector3D[0] = new SMcFVector3D();
                // IMcProperty.SArrayProperty<SMcFVector3D> aDuplicationOffsets1 = new IMcProperty.SArrayProperty<>(vector3D);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetPointsDuplicationOffsets(aDuplicationOffsets1);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetPointsDuplicationOffsets(aDuplicationOffsets);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetPointsDuplicationOffsets(aDuplicationOffsets, 6L, uObjectStateToServe1);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetPointsDuplicationOffsets(aDuplicationOffsets, puPropertyID, uObjectStateToServe1);

                // *OK* boolean Enabled = ((IMcSymbolicItem) objectSchemeItem).GetVectorRotation();
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetVectorRotation(true);
                // *OK* Enabled = ((IMcSymbolicItem) objectSchemeItem).GetVectorRotation();

                // Float fRoll = new Float(2);
                // puPropertyID = new Long(0);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetRotationRoll(fRoll);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetRotationRoll(fRoll, puPropertyID, uObjectStateToServe1);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetRotationRoll(1F);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetRotationRoll(fRoll);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetRotationRoll(2F, 4L, uObjectStateToServe1);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetRotationRoll(fRoll, puPropertyID, uObjectStateToServe1);

                // Float fPitch = new Float(2);
                // puPropertyID = new Long(0);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetRotationPitch(fPitch);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetRotationPitch(fPitch, puPropertyID, uObjectStateToServe1);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetRotationPitch(1F);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetRotationPitch(fPitch);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetRotationPitch(2F, 4L, uObjectStateToServe1);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetRotationPitch(fPitch, puPropertyID, uObjectStateToServe1);

                // Float fYaw = new Float(2);
                // puPropertyID = new Long(0);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetRotationYaw(fYaw);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetRotationYaw(fYaw, puPropertyID, uObjectStateToServe1);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetRotationYaw(1F);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetRotationYaw(fYaw);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetRotationYaw(2F, 4L, uObjectStateToServe1);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetRotationYaw(fYaw, puPropertyID, uObjectStateToServe1);

                // IMcProperty.SArrayProperty<SMcSubItemData> SubItemsData =  new IMcProperty.SArrayProperty<SMcSubItemData>();
                // puPropertyID = new Long(0);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetSubItemsData(SubItemsData);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetSubItemsData(SubItemsData, puPropertyID, uObjectStateToServe1);
                // SMcSubItemData[] subItemData = new SMcSubItemData[1];
                // subItemData[0] = new SMcSubItemData();
                // IMcProperty.SArrayProperty<SMcSubItemData> SubItemsData1 = new IMcProperty.SArrayProperty<>(subItemData);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetSubItemsData(SubItemsData1);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetSubItemsData(SubItemsData);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetSubItemsData(SubItemsData, 7L, uObjectStateToServe1);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetSubItemsData(SubItemsData, puPropertyID, uObjectStateToServe1);

                // ObjectRef<IMcSymbolicItem.EDrawPriorityGroup> eDrawPriorityGroup = new ObjectRef<IMcSymbolicItem.EDrawPriorityGroup>();
                // puPropertyID = new Long(0);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetDrawPriorityGroup(eDrawPriorityGroup);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetDrawPriorityGroup(eDrawPriorityGroup, puPropertyID, uObjectStateToServe1);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetDrawPriorityGroup(IMcSymbolicItem.EDrawPriorityGroup.EDPG_TOP_MOST);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetDrawPriorityGroup(eDrawPriorityGroup);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetDrawPriorityGroup(IMcSymbolicItem.EDrawPriorityGroup.EDPG_SCREEN_BACKGROUND, 8L, uObjectStateToServe1);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetDrawPriorityGroup(eDrawPriorityGroup, puPropertyID, uObjectStateToServe1);

                // *OK* short nPriority = ((IMcSymbolicItem) objectSchemeItem).GetDrawPriority();
                // *OK* short i = 1;
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetDrawPriority(i);
                // *OK* nPriority = ((IMcSymbolicItem) objectSchemeItem).GetDrawPriority();

                // *OK* nPriority = ((IMcSymbolicItem) objectSchemeItem).GetCoplanar3DPriority();
                // *OK* i = 2;
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetCoplanar3DPriority(i);
                // *OK* nPriority = ((IMcSymbolicItem) objectSchemeItem).GetCoplanar3DPriority();

                // *OK* ObjectRef<IMcSymbolicItem.ETextureFilter> eMinFilter = new ObjectRef<IMcSymbolicItem.ETextureFilter>();
                // *OK* ObjectRef<IMcSymbolicItem.ETextureFilter> eMagFilter = new ObjectRef<IMcSymbolicItem.ETextureFilter>();
                // *OK* ObjectRef<IMcSymbolicItem.ETextureFilter> eMipmapFilter = new ObjectRef<IMcSymbolicItem.ETextureFilter>();
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetTextureFiltering(eMinFilter, eMagFilter, eMipmapFilter);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetTextureFiltering(IMcSymbolicItem.ETextureFilter.ETF_POINT, IMcSymbolicItem.ETextureFilter.ETF_POINT, IMcSymbolicItem.ETextureFilter.ETF_POINT);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetTextureFiltering(eMinFilter, eMagFilter, eMipmapFilter);

                // WrapperString strSpecialMaterial = new WrapperString("a");
                // Boolean bSpecialMaterialUseItemTexture = new Boolean(false);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetSpecialMaterial(strSpecialMaterial, bSpecialMaterialUseItemTexture);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetSpecialMaterial("aaa", true);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetSpecialMaterial(strSpecialMaterial, bSpecialMaterialUseItemTexture);

                // Float fMaxChange = new Float(0);
                // puPropertyID = new Long(0);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetMoveIfBlockedMaxChange(fMaxChange);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetMoveIfBlockedMaxChange(fMaxChange, puPropertyID, uObjectStateToServe1);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetMoveIfBlockedMaxChange(1F);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetMoveIfBlockedMaxChange(fMaxChange);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetMoveIfBlockedMaxChange(2F, 4L, uObjectStateToServe1);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetMoveIfBlockedMaxChange(fMaxChange, puPropertyID, uObjectStateToServe1);

                // Float fHeightAboveObstacle = new Float(0);
                // puPropertyID = new Long(0);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetMoveIfBlockedHeightAboveObstacle(fHeightAboveObstacle);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetMoveIfBlockedHeightAboveObstacle(fHeightAboveObstacle, puPropertyID, uObjectStateToServe1);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetMoveIfBlockedHeightAboveObstacle(1F);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetMoveIfBlockedHeightAboveObstacle(fHeightAboveObstacle);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).SetMoveIfBlockedHeightAboveObstacle(2F, 4L, uObjectStateToServe1);
                // *OK* ((IMcSymbolicItem) objectSchemeItem).GetMoveIfBlockedHeightAboveObstacle(fHeightAboveObstacle, puPropertyID, uObjectStateToServe1);*/
            }

            // Line Based
            if (objectSchemeItem instanceof IMcLineBasedItem) {
                /*
                // ObjectRef<IMcLineBasedItem.ELineStyle> objectRef= new ObjectRef<IMcLineBasedItem.ELineStyle>();
                // IMcLineBasedItem.ELineStyle LineStyle = ObjectPropertiesBase.mLineStyle;
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetLineStyle(objectRef);
                Long puPropertyID = new Long(0);
                byte uObjectStateToServe1 = 1;
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetLineStyle(objectRef, puPropertyID, uObjectStateToServe1);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).SetLineStyle(IMcLineBasedItem.ELineStyle.ELS_DASH);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetLineStyle(objectRef);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).SetLineStyle(IMcLineBasedItem.ELineStyle.ELS_DASH_DOT_DOT, 3L, uObjectStateToServe1);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetLineStyle(objectRef, puPropertyID, uObjectStateToServe1);

                // *OK* SMcBColor TextColor = ObjectPropertiesBase.mLineColor;
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetLineColor(TextColor);
                // *OK* puPropertyID = new Long(0);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetLineColor(TextColor, puPropertyID, uObjectStateToServe1);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).SetLineColor(new SMcBColor(0, 0, 0, 0));
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetLineColor(TextColor);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).SetLineColor(new SMcBColor(3, 3, 3, 3), 4L, uObjectStateToServe1);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetLineColor(TextColor, puPropertyID, uObjectStateToServe1);

                // *OK* SMcBColor OutlineColor = ObjectPropertiesBase.mLineColor;
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetOutlineColor(OutlineColor);
                // *OK* puPropertyID = new Long(0);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetOutlineColor(OutlineColor, puPropertyID, uObjectStateToServe1);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).SetOutlineColor(new SMcBColor(0, 0, 0, 0));
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetOutlineColor(OutlineColor);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).SetOutlineColor(new SMcBColor(3, 3, 3, 3), 5L, uObjectStateToServe1);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetOutlineColor(OutlineColor, puPropertyID, uObjectStateToServe1);

                // *OK* Float fLineWidth = new Float(ObjectPropertiesBase.mLineWidth);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetLineWidth(fLineWidth);
                // *OK* puPropertyID = new Long(0);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetLineWidth(fLineWidth, puPropertyID, uObjectStateToServe1);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).SetLineWidth(1F);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetLineWidth(fLineWidth);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).SetLineWidth(4F, 6L, uObjectStateToServe1);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetLineWidth(fLineWidth, puPropertyID, uObjectStateToServe1);

                // *OK* Float fOutlineWidth = new Float(ObjectPropertiesBase.mLineOutlineWidth);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetOutlineWidth(fOutlineWidth);
                // *OK* puPropertyID = new Long(0);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetOutlineWidth(fOutlineWidth, puPropertyID, uObjectStateToServe1);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).SetOutlineWidth(1F);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetOutlineWidth(fOutlineWidth);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).SetOutlineWidth(4F, 7L, uObjectStateToServe1);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetOutlineWidth(fOutlineWidth, puPropertyID, uObjectStateToServe1);

                // *OK* IMcTexture pLineTexture = ObjectPropertiesBase.mLineTexture;
                // *OK* ObjectRef<IMcTexture> objectRef1= new ObjectRef<IMcTexture>();
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetLineTexture(objectRef1);
                // *OK* puPropertyID = new Long(0);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetLineTexture(objectRef1, puPropertyID, uObjectStateToServe1);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).SetLineTexture(ObjectPropertiesBase.mFillTexture);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetLineTexture(objectRef1);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).SetLineTexture(ObjectPropertiesBase.mLineTexture, 8L, uObjectStateToServe1);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetLineTexture(objectRef1, puPropertyID, uObjectStateToServe1);

                // *OK* SMcFVector2D LineTextureHeightRange = ObjectPropertiesBase.mLineTextureHeightRange;
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetLineTextureHeightRange(LineTextureHeightRange);
                // *OK* puPropertyID = new Long(0);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetLineTextureHeightRange(LineTextureHeightRange, puPropertyID, uObjectStateToServe1);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).SetLineTextureHeightRange(new SMcFVector2D(1, 1));
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetLineTextureHeightRange(LineTextureHeightRange);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).SetLineTextureHeightRange(new SMcFVector2D(1, 1), 9L, uObjectStateToServe1);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetLineTextureHeightRange(LineTextureHeightRange, puPropertyID, uObjectStateToServe1);

                // *OK* Float fLineTextureScale = new Float(ObjectPropertiesBase.mLineTextureScale);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetLineTextureScale(fLineTextureScale);
                // *OK* puPropertyID = new Long(0);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetLineTextureScale(fLineTextureScale, puPropertyID, uObjectStateToServe1);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).SetLineTextureScale(1F);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetLineTextureScale(fLineTextureScale);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).SetLineTextureScale(4F, 4L, uObjectStateToServe1);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetLineTextureScale(fLineTextureScale, puPropertyID, uObjectStateToServe1);

                // *OK* Float fVerticalHeight = new Float(ObjectPropertiesBase.mVerticalHeight);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetVerticalHeight(fVerticalHeight);
                // *OK* puPropertyID = new Long(0);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetVerticalHeight(fVerticalHeight, puPropertyID, uObjectStateToServe1);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).SetVerticalHeight(1F);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetVerticalHeight(fVerticalHeight);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).SetVerticalHeight(4F, 11L, uObjectStateToServe1);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetVerticalHeight(fVerticalHeight, puPropertyID, uObjectStateToServe1);

                // *OK* IMcLineBasedItem.EFillStyle eSidesFillStyle = ObjectPropertiesBase.mSidesFillStyle;
                // *OK* ObjectRef<IMcLineBasedItem.EFillStyle> objectRef2= new ObjectRef<IMcLineBasedItem.EFillStyle>();
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetSidesFillStyle(objectRef2);
                // *OK* puPropertyID = new Long(0);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetSidesFillStyle(objectRef2, puPropertyID, uObjectStateToServe1);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).SetSidesFillStyle(IMcLineBasedItem.EFillStyle.EFS_VERTICAL);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetSidesFillStyle(objectRef2);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).SetSidesFillStyle(IMcLineBasedItem.EFillStyle.EFS_BDIAGONAL, 12L, uObjectStateToServe1);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetSidesFillStyle(objectRef2, puPropertyID, uObjectStateToServe1);

                // *OK* SMcBColor SidesFillColor = ObjectPropertiesBase.mSidesFillColor;
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetSidesFillColor(SidesFillColor);
                // *OK* puPropertyID = new Long(0);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetSidesFillColor(SidesFillColor, puPropertyID, uObjectStateToServe1);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).SetSidesFillColor(new SMcBColor(0, 0, 0, 0));
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetSidesFillColor(SidesFillColor);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).SetSidesFillColor(new SMcBColor(3, 3, 3, 3), 13L, uObjectStateToServe1);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetSidesFillColor(SidesFillColor, puPropertyID, uObjectStateToServe1);

                // *OK* ObjectRef<IMcTexture> objectRef3 = new ObjectRef<IMcTexture>();
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetSidesFillTexture(objectRef3);
                // *OK* puPropertyID = new Long(0);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetSidesFillTexture(objectRef3, puPropertyID, uObjectStateToServe1);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).SetSidesFillTexture(ObjectPropertiesBase.mFillTexture);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetSidesFillTexture(objectRef3);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).SetSidesFillTexture(ObjectPropertiesBase.mLineTexture, 14L, uObjectStateToServe1);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetSidesFillTexture(objectRef3, puPropertyID, uObjectStateToServe1);

                // *OK* SMcFVector2D SidesFillTextureScale = ObjectPropertiesBase.mSidesFillTextureScale;
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetSidesFillTextureScale(SidesFillTextureScale);
                // *OK* puPropertyID = new Long(0);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetSidesFillTextureScale(SidesFillTextureScale, puPropertyID, uObjectStateToServe1);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).SetSidesFillTextureScale(new SMcFVector2D(-1, -1));
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetSidesFillTextureScale(SidesFillTextureScale);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).SetSidesFillTextureScale(new SMcFVector2D(1, 1), 15L, uObjectStateToServe1);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetSidesFillTextureScale(SidesFillTextureScale, puPropertyID, uObjectStateToServe1);


                // *OK* float fGreatCirclePrecision = ((IMcLineBasedItem) objectSchemeItem).GetGreatCirclePrecision();
                // *OK* ((IMcLineBasedItem) objectSchemeItem).SetGreatCirclePrecision(1F);
                // *OK* fGreatCirclePrecision = ((IMcLineBasedItem) objectSchemeItem).GetGreatCirclePrecision();

                 // *OK* Byte uNumSmoothingLevels = new Byte(ObjectPropertiesBase.mLineBasedSmoothingLevels);
                ObjectRef<Byte> var1 = new ObjectRef<>();
                ObjectRef<Long> var2 = new ObjectRef<>();
                  ((IMcLineBasedItem) objectSchemeItem).GetNumSmoothingLevels(var1, var2);
                Byte uNumSmoothingLevels = new Byte(ObjectPropertiesBase.mLineBasedSmoothingLevels);
                int u = uNumSmoothingLevels + 8;
                 // *OK* puPropertyID = new Long(0);
                 // *OK* ((IMcLineBasedItem) objectSchemeItem).GetNumSmoothingLevels(uNumSmoothingLevels, puPropertyID, uObjectStateToServe1);
                 // *OK* byte b = 0;
                 // *OK* ((IMcLineBasedItem) objectSchemeItem).GetNumSmoothingLevels(uNumSmoothingLevels);
                 // *OK* b = 0;
                 // *OK* ((IMcLineBasedItem) objectSchemeItem).SetNumSmoothingLevels(b, 30L, uObjectStateToServe1);
                 // *OK* ((IMcLineBasedItem) objectSchemeItem).GetNumSmoothingLevels(uNumSmoothingLevels, puPropertyID, uObjectStateToServe1);

                // *OK* ObjectRef<IMcObjectSchemeItem[]> papClippingItems = new ObjectRef<IMcObjectSchemeItem[]>();
                // *OK* Boolean bSelfClippingOnly = false;
                // *OK* IMcTextItem textItem =  IMcTextItem.Static.Create(itemSubType,ObjectPropertiesBase.Text.getInstance().mTextFont);
                // *OK* textItem.Connect(objectSchemeItem);
                // *OK* IMcObjectSchemeItem[] apClippingItems = new IMcObjectSchemeItem[1];
                // *OK* apClippingItems[0] = textItem;
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetClippingItems(papClippingItems);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetClippingItems(papClippingItems, bSelfClippingOnly);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).SetClippingItems(apClippingItems);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetClippingItems(papClippingItems);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).SetClippingItems(apClippingItems, true);
                // *OK* ((IMcLineBasedItem) objectSchemeItem).GetClippingItems(papClippingItems, bSelfClippingOnly);
*/
            }

            // Closed Shaped
            if (objectSchemeItem instanceof IMcClosedShapeItem) {
                /*

                // *OK* IMcLineBasedItem.EFillStyle eFillStyle = ObjectPropertiesBase.mFillStyle;
                // *OK* ObjectRef<IMcLineBasedItem.EFillStyle> objectRef= new ObjectRef<IMcLineBasedItem.EFillStyle>();
                // *OK* ((IMcClosedShapeItem) objectSchemeItem).GetFillStyle(objectRef);
                Long puPropertyID = new Long(0);
                byte uObjectStateToServe1 = 1;
                // *OK* ((IMcClosedShapeItem) objectSchemeItem).GetFillStyle(objectRef, puPropertyID, uObjectStateToServe1);
                // *OK* ((IMcClosedShapeItem) objectSchemeItem).SetFillStyle(IMcLineBasedItem.EFillStyle.EFS_HORIZONTAL);
                // *OK* ((IMcClosedShapeItem) objectSchemeItem).GetFillStyle(objectRef);
                // *OK* ((IMcClosedShapeItem) objectSchemeItem).SetFillStyle(IMcLineBasedItem.EFillStyle.EFS_VERTICAL, 3L, uObjectStateToServe1);
                // *OK* ((IMcClosedShapeItem) objectSchemeItem).GetFillStyle(objectRef, puPropertyID, uObjectStateToServe1);

                // *OK* IMcTexture texture = ObjectPropertiesBase.mFillTexture;
                // *OK* ObjectRef<IMcTexture> objectRef1 = new ObjectRef<IMcTexture>();
                // *OK* ((IMcClosedShapeItem) objectSchemeItem).GetFillTexture(objectRef1);
                // *OK* puPropertyID = new Long(0);
                // *OK* ((IMcClosedShapeItem) objectSchemeItem).GetFillTexture(objectRef1, puPropertyID, uObjectStateToServe1);
                // *OK* ((IMcClosedShapeItem) objectSchemeItem).SetFillTexture(ObjectPropertiesBase.mLineTexture);
                // *OK* ((IMcClosedShapeItem) objectSchemeItem).GetFillTexture(objectRef1);
                // *OK* ((IMcClosedShapeItem) objectSchemeItem).SetFillTexture(ObjectPropertiesBase.mFillTexture, 3L, uObjectStateToServe1);
                // *OK* ((IMcClosedShapeItem) objectSchemeItem).GetFillTexture(objectRef1, puPropertyID, uObjectStateToServe1);

                // *OK* SMcFVector2D FillTextureScale = ObjectPropertiesBase.mFillTextureScale;
                // *OK* ((IMcClosedShapeItem) objectSchemeItem).GetFillTextureScale(FillTextureScale);
                // *OK* puPropertyID = new Long(0);
                // *OK* ((IMcClosedShapeItem) objectSchemeItem).GetFillTextureScale(FillTextureScale, puPropertyID, uObjectStateToServe1);
                // *OK* ((IMcClosedShapeItem) objectSchemeItem).SetFillTextureScale(new SMcFVector2D(1, 1));
                // *OK* ((IMcClosedShapeItem) objectSchemeItem).GetFillTextureScale(FillTextureScale);
                // *OK* ((IMcClosedShapeItem) objectSchemeItem).SetFillTextureScale(ObjectPropertiesBase.mFillTextureScale, 3L, uObjectStateToServe1);
                // *OK* ((IMcClosedShapeItem) objectSchemeItem).GetFillTextureScale(FillTextureScale, puPropertyID, uObjectStateToServe1);
*/
            }

            IMcObjectSchemeNode.EObjectSchemeNodeType type = objectSchemeItem.GetNodeType();
            switch (type) {
                case ARC_ITEM:
                    /*
                    // *OK* IMcArcItem arcCloneItemWithoutObject = ((IMcArcItem) objectSchemeItem).Clone();
                    // *OK* IMcArcItem arcCloneItem = ((IMcArcItem) objectSchemeItem).Clone(obj);
                    // *OK* EMcPointCoordSystem arcPointCoordSystem = ((IMcArcItem) objectSchemeItem).GetEllipseCoordinateSystem();
                    // *OK* IMcObjectSchemeItem.EGeometryType arcEllipseType = ((IMcArcItem) objectSchemeItem).GetEllipseType();

                    // *OK* Float fStartAngle = new Float(ObjectPropertiesBase.mArcStartAngle);
                    // *OK* ((IMcArcItem) objectSchemeItem).GetStartAngle(fStartAngle);
                    Long puPropertyID = new Long(0);
                    byte uObjectStateToServe1 = 1;
                    // *OK* ((IMcArcItem) objectSchemeItem).GetStartAngle(fStartAngle, puPropertyID, uObjectStateToServe1);
                    // *OK* ((IMcArcItem) objectSchemeItem).SetStartAngle(1F);
                    // *OK* ((IMcArcItem) objectSchemeItem).GetStartAngle(fStartAngle);
                    // *OK* ((IMcArcItem) objectSchemeItem).SetStartAngle(4F, 3L, uObjectStateToServe1);
                    // *OK* ((IMcArcItem) objectSchemeItem).GetStartAngle(fStartAngle, puPropertyID, uObjectStateToServe1);


                    // *OK* Float fEndAngle = new Float(ObjectPropertiesBase.mArcEndAngle);
                    // *OK* ((IMcArcItem) objectSchemeItem).GetEndAngle(fEndAngle);
                    // *OK* puPropertyID = new Long(0);
                    // *OK* ((IMcArcItem) objectSchemeItem).GetEndAngle(fEndAngle, puPropertyID, uObjectStateToServe1);
                    // *OK* ((IMcArcItem) objectSchemeItem).SetEndAngle(1F);
                    // *OK* ((IMcArcItem) objectSchemeItem).GetEndAngle(fEndAngle);
                    // *OK* ((IMcArcItem) objectSchemeItem).SetEndAngle(4F, 3L, uObjectStateToServe1);
                    // *OK* ((IMcArcItem) objectSchemeItem).GetEndAngle(fEndAngle, puPropertyID, uObjectStateToServe1);

                    // *OK* Float fRadiusX = new Float(ObjectPropertiesBase.mArcRadiusX);
                    // *OK* ((IMcArcItem) objectSchemeItem).GetRadiusX(fRadiusX);
                    // *OK* puPropertyID = new Long(0);
                    // *OK* ((IMcArcItem) objectSchemeItem).GetRadiusX(fRadiusX, puPropertyID, uObjectStateToServe1);
                    // *OK* ((IMcArcItem) objectSchemeItem).SetRadiusX(1F);
                    // *OK* ((IMcArcItem) objectSchemeItem).GetRadiusX(fRadiusX);
                    // *OK* ((IMcArcItem) objectSchemeItem).SetRadiusX(4F, 3L, uObjectStateToServe1);
                    // *OK* ((IMcArcItem) objectSchemeItem).GetRadiusX(fRadiusX, puPropertyID, uObjectStateToServe1);

                    // *OK* Float fRadiusY = new Float(ObjectPropertiesBase.mArcRadiusY);
                    // *OK* ((IMcArcItem) objectSchemeItem).GetRadiusY(fRadiusY);
                    // *OK* puPropertyID = new Long(0);
                    // *OK* ((IMcArcItem) objectSchemeItem).GetRadiusY(fRadiusY, puPropertyID, uObjectStateToServe1);
                    // *OK* ((IMcArcItem) objectSchemeItem).SetRadiusY(1F);
                    // *OK* ((IMcArcItem) objectSchemeItem).GetRadiusY(fRadiusY);
                    // *OK* ((IMcArcItem) objectSchemeItem).SetRadiusY(2F, 3L, uObjectStateToServe1);
                    // *OK* ((IMcArcItem) objectSchemeItem).GetRadiusY(fRadiusY, puPropertyID, uObjectStateToServe1);
*/
                    break;
                case ARROW_ITEM:
                    /*
                    // *OK* IMcArrowItem ArrowCloneItemWithoutObject = ((IMcArrowItem) objectSchemeItem).Clone();
                    // *OK* IMcArrowItem ArrowCloneItem = ((IMcArrowItem) objectSchemeItem).Clone(obj);
                    // *OK* EMcPointCoordSystem ArrowPointCoordSystem = ((IMcArrowItem) objectSchemeItem).GetArrowCoordinateSystem();

                    // *OK* Float fHeadSize = new Float(ObjectPropertiesBase.mArrowHeadSize);
                    // *OK* ((IMcArrowItem) objectSchemeItem).GetHeadSize(fHeadSize);
                    puPropertyID = new Long(0);
                    uObjectStateToServe1 = 1;
                    // *OK* ((IMcArrowItem) objectSchemeItem).GetHeadSize(fHeadSize, puPropertyID, uObjectStateToServe1);
                    // *OK* ((IMcArrowItem) objectSchemeItem).SetHeadSize(1F);
                    // *OK* ((IMcArrowItem) objectSchemeItem).GetHeadSize(fHeadSize);
                    // *OK* ((IMcArrowItem) objectSchemeItem).SetHeadSize(2F, 1L, uObjectStateToServe1);
                    // *OK* ((IMcArrowItem) objectSchemeItem).GetHeadSize(fHeadSize, puPropertyID, uObjectStateToServe1);

                    // *OK* Float fHeadAngle = new Float(ObjectPropertiesBase.mArrowHeadSize);
                    // *OK* ((IMcArrowItem) objectSchemeItem).GetHeadAngle(fHeadAngle);
                    // *OK* puPropertyID = new Long(0);
                    // *OK* ((IMcArrowItem) objectSchemeItem).GetHeadAngle(fHeadAngle, puPropertyID, uObjectStateToServe1);
                    // *OK* ((IMcArrowItem) objectSchemeItem).SetHeadAngle(1F);
                    // *OK* ((IMcArrowItem) objectSchemeItem).GetHeadAngle(fHeadAngle);
                    // *OK* ((IMcArrowItem) objectSchemeItem).SetHeadAngle(2F, 1L, uObjectStateToServe1);
                    // *OK* ((IMcArrowItem) objectSchemeItem).GetHeadAngle(fHeadAngle, puPropertyID, uObjectStateToServe1);

                    // *OK* Float fGapSize = new Float(ObjectPropertiesBase.mArrowGapSize);
                    // *OK* ((IMcArrowItem) objectSchemeItem).GetGapSize(fGapSize);
                    // *OK* puPropertyID = new Long(0);
                    // *OK* ((IMcArrowItem) objectSchemeItem).GetGapSize(fGapSize, puPropertyID, uObjectStateToServe1);
                    // *OK* ((IMcArrowItem) objectSchemeItem).SetGapSize(1F);
                    // *OK* ((IMcArrowItem) objectSchemeItem).GetGapSize(fGapSize);
                    // *OK* ((IMcArrowItem) objectSchemeItem).SetGapSize(2F, 1L, uObjectStateToServe1);
                    // *OK* ((IMcArrowItem) objectSchemeItem).GetGapSize(fGapSize, puPropertyID, uObjectStateToServe1);

                    // *OK* IMcLineBasedItem.SSlopePresentationColor[] aColors = ((IMcArrowItem) objectSchemeItem).GetSlopePresentationColors();
                    // *OK* IMcLineBasedItem.SSlopePresentationColor[] aColors1 = new IMcLineBasedItem.SSlopePresentationColor[1];
                    // *OK* aColors1[0] = new IMcLineBasedItem.SSlopePresentationColor();
                    // *OK* aColors1[0].fMaxSlope = 80;
                    // *OK* aColors1[0].Color = new SMcBColor(0, 255, 100, 100);
                    // *OK* ((IMcArrowItem) objectSchemeItem).SetSlopePresentationColors(aColors1);
                    // *OK* aColors = ((IMcArrowItem) objectSchemeItem).GetSlopePresentationColors();

                    // *OK* ObjectRef<IMcSpatialQueries.EQueryPrecision> objectRef= new ObjectRef<IMcSpatialQueries.EQueryPrecision>();
                    // *OK* puPropertyID = new Long(0);
                    // *OK* ((IMcArrowItem) objectSchemeItem).GetSlopeQueryPrecision(objectRef);
                    // *OK* ((IMcArrowItem) objectSchemeItem).GetSlopeQueryPrecision(objectRef, puPropertyID, uObjectStateToServe1);
                    // *OK* ((IMcArrowItem) objectSchemeItem).SetSlopeQueryPrecision(IMcSpatialQueries.EQueryPrecision.EQP_DEFAULT_PLUS_LOWEST);
                    // *OK* ((IMcArrowItem) objectSchemeItem).GetSlopeQueryPrecision(objectRef);
                    // *OK* ((IMcArrowItem) objectSchemeItem).SetSlopeQueryPrecision(IMcSpatialQueries.EQueryPrecision.EQP_HIGH, 201L, uObjectStateToServe1);
                    // *OK* ((IMcArrowItem) objectSchemeItem).GetSlopeQueryPrecision(objectRef, puPropertyID, uObjectStateToServe1);

                   // *OK* Boolean bShowSlopePresentation = new Boolean(false);
                   // *OK* ((IMcArrowItem) objectSchemeItem).GetShowSlopePresentation(bShowSlopePresentation);
                   // *OK* puPropertyID = new Long(0);
                   // *OK* ((IMcArrowItem) objectSchemeItem).GetShowSlopePresentation(bShowSlopePresentation, puPropertyID, uObjectStateToServe1);
                   // *OK* ((IMcArrowItem) objectSchemeItem).SetShowSlopePresentation(true);
                   // *OK* ((IMcArrowItem) objectSchemeItem).GetShowSlopePresentation(bShowSlopePresentation);
                   // *OK* ((IMcArrowItem) objectSchemeItem).SetShowSlopePresentation(false, 1L, uObjectStateToServe1);
                   // *OK* ((IMcArrowItem) objectSchemeItem).GetShowSlopePresentation(bShowSlopePresentation, puPropertyID, uObjectStateToServe1);
*/
                    break;
                case LINE_EXPANSION_ITEM:
                    /*
                    // *OK* IMcLineExpansionItem LineExpansionCloneItemWithoutObject = ((IMcLineExpansionItem) objectSchemeItem).Clone();
                    // *OK* IMcLineExpansionItem LineExpansionCloneItem = ((IMcLineExpansionItem) objectSchemeItem).Clone(obj);
                    // *OK* EMcPointCoordSystem LineExpansionPointCoordSystem = ((IMcLineExpansionItem) objectSchemeItem).GetLineExpansionCoordinateSystem();
                    // *OK* IMcObjectSchemeItem.EGeometryType LineExpansionType = ((IMcLineExpansionItem) objectSchemeItem).GetLineExpansionType();

                    // *OK* Float fRadius = new Float(ObjectPropertiesBase.mLineExpansionRadius);
                    // *OK* ((IMcLineExpansionItem) objectSchemeItem).GetRadius(fRadius);
                    // *OK* puPropertyID = new Long(0);
                    // *OK* uObjectStateToServe1 = 1;
                    // *OK* ((IMcLineExpansionItem) objectSchemeItem).GetRadius(fRadius, puPropertyID, uObjectStateToServe1);
                    // *OK* ((IMcLineExpansionItem) objectSchemeItem).SetRadius(1F);
                    // *OK* ((IMcLineExpansionItem) objectSchemeItem).GetRadius(fRadius);
                    // *OK* ((IMcLineExpansionItem) objectSchemeItem).SetRadius(4F, 3L, uObjectStateToServe1);
                    // *OK* ((IMcLineExpansionItem) objectSchemeItem).GetRadius(fRadius, puPropertyID, uObjectStateToServe1);
*/
                    break;
                case PICTURE_ITEM:
                    /*
                    // *OK* IMcPictureItem PictureCloneItemWithoutObject = ((IMcPictureItem) objectSchemeItem).Clone();
                    // *OK* IMcPictureItem PictureCloneItem = ((IMcPictureItem) objectSchemeItem).Clone(obj);

                    // *OK* Float fWidth = new Float(ObjectPropertiesBase.mPicWidth);
                    // *OK* ((IMcPictureItem) objectSchemeItem).GetWidth(fWidth);
                    // *OK* puPropertyID = new Long(0);
                    //uObjectStateToServe1 = 1;
                    // *OK* ((IMcPictureItem) objectSchemeItem).GetWidth(fWidth, puPropertyID, uObjectStateToServe1);
                    // *OK* ((IMcPictureItem) objectSchemeItem).SetWidth(1F);
                    // *OK* ((IMcPictureItem) objectSchemeItem).GetWidth(fWidth);
                    // *OK* ((IMcPictureItem) objectSchemeItem).SetWidth(2F, 1L, uObjectStateToServe1);
                    // *OK* ((IMcPictureItem) objectSchemeItem).GetWidth(fWidth, puPropertyID, uObjectStateToServe1);
                    // *OK* Float fHeight = new Float(ObjectPropertiesBase.mPicHeight);
                    // *OK* ((IMcPictureItem) objectSchemeItem).GetHeight(fHeight);
                    // *OK* puPropertyID = new Long(0);
                    // *OK* uObjectStateToServe1 = 1;
                    // *OK* ((IMcPictureItem) objectSchemeItem).GetHeight(fHeight, puPropertyID, uObjectStateToServe1);
                    // *OK* ((IMcPictureItem) objectSchemeItem).SetHeight(1F);
                    // *OK* ((IMcPictureItem) objectSchemeItem).GetHeight(fHeight);
                    // *OK* ((IMcPictureItem) objectSchemeItem).SetHeight(2F, 1L, uObjectStateToServe1);
                    // *OK* ((IMcPictureItem) objectSchemeItem).GetHeight(fHeight, puPropertyID, uObjectStateToServe1);

                    // *OK* boolean isSizeFactor = ((IMcPictureItem) objectSchemeItem).IsSizeFactor();
                    // *OK* boolean isUsingTextureGeoReferencing = ((IMcPictureItem) objectSchemeItem).IsUsingTextureGeoReferencing();

                    // *OK* ObjectRef<IMcTexture> pTexture = new ObjectRef<IMcTexture>();
                    // *OK* ((IMcPictureItem) objectSchemeItem).GetTexture(pTexture);
                    // *OK* puPropertyID = new Long(0);
                    // *OK* ((IMcPictureItem) objectSchemeItem).GetTexture(pTexture, puPropertyID, uObjectStateToServe1);
                    // *OK* ((IMcPictureItem) objectSchemeItem).SetTexture(ObjectPropertiesBase.mPicTexture);
                    // *OK* ((IMcPictureItem) objectSchemeItem).GetTexture(pTexture);
                    // *OK* ((IMcPictureItem) objectSchemeItem).SetTexture(ObjectPropertiesBase.mLineTexture, 3L, uObjectStateToServe1);
                    // *OK* ((IMcPictureItem) objectSchemeItem).GetTexture(pTexture, puPropertyID, uObjectStateToServe1);

                    // *OK* SMcBColor TextureColor = ObjectPropertiesBase.Text.getInstance().mTextColor;
                    // *OK* ((IMcPictureItem) objectSchemeItem).GetTextureColor(TextureColor);
                    // *OK* puPropertyID = new Long(0);
                    // *OK* ((IMcPictureItem) objectSchemeItem).GetTextureColor(TextureColor, puPropertyID, uObjectStateToServe1);
                    // *OK* ((IMcPictureItem) objectSchemeItem).SetTextureColor(new SMcBColor(0, 0, 0, 0));
                    // *OK* ((IMcPictureItem) objectSchemeItem).GetTextureColor(TextureColor);
                    // *OK* ((IMcPictureItem) objectSchemeItem).SetTextureColor(new SMcBColor(3, 3, 3, 3), 3L, uObjectStateToServe1);
                    // *OK* ((IMcPictureItem) objectSchemeItem).GetTextureColor(TextureColor, puPropertyID, uObjectStateToServe1);
*/
                    break;
                case POLYGON_ITEM:
                    /*
                    // *OK* IMcPolygonItem PolygonCloneItemWithoutObject = ((IMcPolygonItem) objectSchemeItem).Clone();
                    // *OK* IMcPolygonItem PolygonCloneItem = ((IMcPolygonItem) objectSchemeItem).Clone(obj);
*/
                    break;
                case TEXT_ITEM:
                    /*
                    // *OK* IMcTextItem TextCloneItemWithoutObject = ((IMcTextItem) objectSchemeItem).Clone();
                    // *OK* IMcTextItem TextCloneItem = ((IMcTextItem) objectSchemeItem).Clone(obj);

                    // *OK* SMcVariantString Text = new SMcVariantString();
                    // *OK* ((IMcTextItem) objectSchemeItem).GetText(Text);
                    // *OK* puPropertyID = new Long(0);
                    // *OK* uObjectStateToServe1 = 1;
                    // *OK* ((IMcTextItem) objectSchemeItem).GetText(Text, puPropertyID, uObjectStateToServe1);
                    // *OK* ((IMcTextItem) objectSchemeItem).SetText(new SMcVariantString("aaa", false));
                    // *OK* ((IMcTextItem) objectSchemeItem).GetText(Text);
                    // *OK* ((IMcTextItem) objectSchemeItem).SetText(new SMcVariantString("bbb", false), 3L, uObjectStateToServe1);
                    // *OK* ((IMcTextItem) objectSchemeItem).GetText(Text, puPropertyID, uObjectStateToServe1);

                    // *OK* ObjectRef<EAxisXAlignment> peTextAlignment = new ObjectRef<EAxisXAlignment>();
                    // *OK* ((IMcTextItem) objectSchemeItem).GetTextAlignment(peTextAlignment);
                    // *OK* puPropertyID = new Long(0);
                    // *OK* ((IMcTextItem) objectSchemeItem).GetTextAlignment(peTextAlignment, puPropertyID, uObjectStateToServe1);
                    // *OK* ((IMcTextItem) objectSchemeItem).SetTextAlignment(EAxisXAlignment.EXA_CENTER);
                    // *OK* ((IMcTextItem) objectSchemeItem).GetTextAlignment(peTextAlignment);
                    // *OK* ((IMcTextItem) objectSchemeItem).SetTextAlignment(EAxisXAlignment.EXA_LEFT, 4L, uObjectStateToServe1);
                    // *OK* ((IMcTextItem) objectSchemeItem).GetTextAlignment(peTextAlignment, puPropertyID, uObjectStateToServe1);

                    // *OK* Boolean bRightToLeftReadingOrder = new Boolean(ObjectPropertiesBase.Text.getInstance().mTextRtlReadingOrder);
                    // *OK* puPropertyID = new Long(0);
                    // *OK* ((IMcTextItem) objectSchemeItem).GetRightToLeftReadingOrder(bRightToLeftReadingOrder);
                    // *OK* ((IMcTextItem) objectSchemeItem).GetRightToLeftReadingOrder(bRightToLeftReadingOrder, puPropertyID, uObjectStateToServe1);
                    // *OK* ((IMcTextItem) objectSchemeItem).SetRightToLeftReadingOrder(ObjectPropertiesBase.Text.getInstance().mTextRtlReadingOrder);
                    // *OK* ((IMcTextItem) objectSchemeItem).GetRightToLeftReadingOrder(bRightToLeftReadingOrder);
                    // *OK* ((IMcTextItem) objectSchemeItem).SetRightToLeftReadingOrder(true, 5L, uObjectStateToServe1);
                    // *OK* ((IMcTextItem) objectSchemeItem).GetRightToLeftReadingOrder(bRightToLeftReadingOrder, puPropertyID, uObjectStateToServe1);

                    // *OK* IMcSymbolicItem.EBoundingBoxPointFlags eBoundingBoxPointFlags= ((IMcTextItem) objectSchemeItem).GetRectAlignment();
                    // *OK* ((IMcTextItem) objectSchemeItem).SetRectAlignment(IMcSymbolicItem.EBoundingBoxPointFlags.EBBPF_TOP_LEFT);
                    // *OK* eBoundingBoxPointFlags= ((IMcTextItem) objectSchemeItem).GetRectAlignment();

                    // *OK* Integer uMargin = new Integer(ObjectPropertiesBase.Text.getInstance().mTextMargin);
                    // *OK* puPropertyID = new Long(0);
                    // *OK* ((IMcTextItem) objectSchemeItem).GetMargin(uMargin);
                    // *OK* ((IMcTextItem) objectSchemeItem).GetMargin(uMargin, puPropertyID, uObjectStateToServe1);
                    // *OK* ((IMcTextItem) objectSchemeItem).SetMargin(ObjectPropertiesBase.Text.getInstance().mTextMargin);
                    // *OK* ((IMcTextItem) objectSchemeItem).GetMargin(uMargin);
                    // *OK* ((IMcTextItem) objectSchemeItem).SetMargin(1, 6L, uObjectStateToServe1);
                    // *OK* ((IMcTextItem) objectSchemeItem).GetMargin(uMargin, puPropertyID, uObjectStateToServe1);

                    // *OK*
*/
                    /*ObjectRef<IMcTextItem.ENeverUpsideDownMode> var1 = new ObjectRef<>();
                    ObjectRef<Long> var2 = new ObjectRef<Long>();
                    ((IMcTextItem) objectSchemeItem).GetNeverUpsideDownMode(var1, var2);
                    int u=0;
                    int y=u+9*/;
                    break;
                case ELLIPSE_ITEM:
                    /*
                    // *OK* IMcEllipseItem EllipseCloneItemWithoutObject = ((IMcEllipseItem) objectSchemeItem).Clone();
                    // *OK* IMcEllipseItem EllipseCloneItem = ((IMcEllipseItem) objectSchemeItem).Clone(obj);
                    // *OK* EMcPointCoordSystem EllipsePointCoordSystem = ((IMcEllipseItem) objectSchemeItem).GetEllipseCoordinateSystem();
                    // *OK* IMcObjectSchemeItem.EGeometryType EllipseEllipseType = ((IMcEllipseItem) objectSchemeItem).GetEllipseType();

                    // *OK* boolean bPolar = ((IMcEllipseItem) objectSchemeItem).GetFillTexturePolarMapping();
                    // *OK* ((IMcEllipseItem) objectSchemeItem).SetFillTexturePolarMapping(true);
                    // *OK* bPolar = ((IMcEllipseItem) objectSchemeItem).GetFillTexturePolarMapping();

                    // *OK* Float fStartAngle = new Float(ObjectPropertiesBase.mEllipseStartAngle);
                    // *OK* ((IMcEllipseItem) objectSchemeItem).GetStartAngle(fStartAngle);
                    // *OK* puPropertyID = new Long(0);
                    // *OK* uObjectStateToServe1 = 1;
                    // *OK* ((IMcEllipseItem) objectSchemeItem).GetStartAngle(fStartAngle, puPropertyID, uObjectStateToServe1);
                    // *OK* ((IMcEllipseItem) objectSchemeItem).SetStartAngle(1F);
                    // *OK* ((IMcEllipseItem) objectSchemeItem).GetStartAngle(fStartAngle);
                    // *OK* ((IMcEllipseItem) objectSchemeItem).SetStartAngle(2F, 1L, uObjectStateToServe1);
                    // *OK* ((IMcEllipseItem) objectSchemeItem).GetStartAngle(fStartAngle, puPropertyID, uObjectStateToServe1);

                    // *OK* Float fEndAngle = new Float(ObjectPropertiesBase.mEllipseEndAngle);
                    // *OK* ((IMcEllipseItem) objectSchemeItem).GetEndAngle(fEndAngle);
                    // *OK* puPropertyID = new Long(0);
                    // *OK* ((IMcEllipseItem) objectSchemeItem).GetEndAngle(fEndAngle, puPropertyID, uObjectStateToServe1);
                    // *OK* ((IMcEllipseItem) objectSchemeItem).SetEndAngle(1F);
                    // *OK* ((IMcEllipseItem) objectSchemeItem).GetEndAngle(fEndAngle);
                    // *OK* ((IMcEllipseItem) objectSchemeItem).SetEndAngle(2F, 1L, uObjectStateToServe1);
                    // *OK* ((IMcEllipseItem) objectSchemeItem).GetEndAngle(fEndAngle, puPropertyID, uObjectStateToServe1);

                    // *OK* Float fRadiusX = new Float(ObjectPropertiesBase.mEllipseRadiusX);
                    // *OK* ((IMcEllipseItem) objectSchemeItem).GetRadiusX(fRadiusX);
                    // *OK* puPropertyID = new Long(0);
                    // *OK* ((IMcEllipseItem) objectSchemeItem).GetRadiusX(fRadiusX, puPropertyID, uObjectStateToServe1);
                    // *OK* ((IMcEllipseItem) objectSchemeItem).SetRadiusX(1F);
                    // *OK* ((IMcEllipseItem) objectSchemeItem).GetRadiusX(fRadiusX);
                    // *OK* ((IMcEllipseItem) objectSchemeItem).SetRadiusX(2F, 1L, uObjectStateToServe1);
                    // *OK* ((IMcEllipseItem) objectSchemeItem).GetRadiusX(fRadiusX, puPropertyID, uObjectStateToServe1);

                    // *OK* Float fRadiusY = new Float(ObjectPropertiesBase.mEllipseRadiusY);
                    // *OK* ((IMcEllipseItem) objectSchemeItem).GetRadiusY(fRadiusY);
                    // *OK* puPropertyID = new Long(0);
                    // *OK* ((IMcEllipseItem) objectSchemeItem).GetRadiusY(fRadiusY, puPropertyID, uObjectStateToServe1);
                    // *OK* ((IMcEllipseItem) objectSchemeItem).SetRadiusY(1F);
                    // *OK* ((IMcEllipseItem) objectSchemeItem).GetRadiusY(fRadiusY);
                    // *OK* ((IMcEllipseItem) objectSchemeItem).SetRadiusY(2F, 1L, uObjectStateToServe1);
                    // *OK* ((IMcEllipseItem) objectSchemeItem).GetRadiusY(fRadiusY, puPropertyID, uObjectStateToServe1);

                    // *OK* Float fInnerRadiusFactor = new Float(ObjectPropertiesBase.mEllipseRadiusY);
                    // *OK* ((IMcEllipseItem) objectSchemeItem).GetInnerRadiusFactor(fInnerRadiusFactor);
                    // *OK* puPropertyID = new Long(0);
                    // *OK* ((IMcEllipseItem) objectSchemeItem).GetInnerRadiusFactor(fInnerRadiusFactor, puPropertyID, uObjectStateToServe1);
                    // *OK* ((IMcEllipseItem) objectSchemeItem).SetInnerRadiusFactor(1F);
                    // *OK* ((IMcEllipseItem) objectSchemeItem).GetInnerRadiusFactor(fInnerRadiusFactor);
                    // *OK* ((IMcEllipseItem) objectSchemeItem).SetInnerRadiusFactor(2F, 1L, uObjectStateToServe1);
                    // *OK* ((IMcEllipseItem) objectSchemeItem).GetInnerRadiusFactor(fInnerRadiusFactor, puPropertyID, uObjectStateToServe1);
*/
                    break;
                case RECTANGLE_ITEM:
                    /*
                    // *OK* IMcRectangleItem RectangleCloneItemWithoutObject = ((IMcRectangleItem) objectSchemeItem).Clone();
                    // *OK* IMcRectangleItem RectangleCloneItem = ((IMcRectangleItem) objectSchemeItem).Clone(obj);
                    // *OK* EMcPointCoordSystem RectanglePointCoordSystem = ((IMcRectangleItem) objectSchemeItem).GetRectangleCoordinateSystem();
                    // *OK* IMcObjectSchemeItem.EGeometryType RectangleEllipseType = ((IMcRectangleItem) objectSchemeItem).GetRectangleType();

                    // *OK* Float fRectRadiusX = new Float(ObjectPropertiesBase.mRectRadiusX);
                    // *OK* ((IMcRectangleItem) objectSchemeItem).GetRadiusX(fRectRadiusX);
                    // *OK* puPropertyID = new Long(0);
                    // *OK* uObjectStateToServe1 = 1;
                    // *OK* ((IMcRectangleItem) objectSchemeItem).GetRadiusX(fRectRadiusX, puPropertyID, uObjectStateToServe1);
                    // *OK* ((IMcRectangleItem) objectSchemeItem).SetRadiusX(1F);
                    // *OK* ((IMcRectangleItem) objectSchemeItem).GetRadiusX(fRectRadiusX);
                    // *OK* ((IMcRectangleItem) objectSchemeItem).SetRadiusX(2F, 1L, uObjectStateToServe1);
                    // *OK* ((IMcRectangleItem) objectSchemeItem).GetRadiusX(fRectRadiusX, puPropertyID, uObjectStateToServe1);

                    // *OK* Float fRectRadiusY = new Float(ObjectPropertiesBase.mRectRadiusY);
                    // *OK* ((IMcRectangleItem) objectSchemeItem).GetRadiusY(fRectRadiusY);
                    // *OK* puPropertyID = new Long(0);
                    // *OK* uObjectStateToServe1 = 1;
                    // *OK* ((IMcRectangleItem) objectSchemeItem).GetRadiusY(fRectRadiusY, puPropertyID, uObjectStateToServe1);
                    // *OK* ((IMcRectangleItem) objectSchemeItem).SetRadiusY(1F);
                    // *OK* ((IMcRectangleItem) objectSchemeItem).GetRadiusY(fRectRadiusY);
                    // *OK* ((IMcRectangleItem) objectSchemeItem).SetRadiusY(2F, 1L, uObjectStateToServe1);
                    // *OK* ((IMcRectangleItem) objectSchemeItem).GetRadiusY(fRectRadiusY, puPropertyID, uObjectStateToServe1);
*/
                    break;
                case MESH_ITEM:
                    /*//
                    // *OK* IMcMeshItem MeshCloneItemWithoutObject = ((IMcMeshItem) objectSchemeItem).Clone();
                    // *OK* IMcMeshItem MeshCloneItem = ((IMcMeshItem) objectSchemeItem).Clone(obj);

                    // *OK* ObjectRef<IMcMesh> pMesh = new ObjectRef<IMcMesh>();
                    Long puPropertyID = new Long(0);
                    byte uObjectStateToServe1 = 1;
                    // *OK* ((IMcMeshItem) objectSchemeItem).GetMesh(pMesh);
                    // *OK* ((IMcMeshItem) objectSchemeItem).GetMesh(pMesh, puPropertyID, uObjectStateToServe1);
                    // *OK* ((IMcMeshItem) objectSchemeItem).SetMesh(ObjectPropertiesBase.mMesh);
                    // *OK* ((IMcMeshItem) objectSchemeItem).GetMesh(pMesh);
                    // *OK* ((IMcMeshItem) objectSchemeItem).SetMesh(ObjectPropertiesBase.mMesh, 3L, uObjectStateToServe1);
                    // *OK* ((IMcMeshItem) objectSchemeItem).GetMesh(pMesh, puPropertyID, uObjectStateToServe1);

                    // *OK* SMcAnimation Animation = new SMcAnimation();
                    // *OK* puPropertyID = new Long(0);
                    // *OK* ((IMcMeshItem) objectSchemeItem).GetAnimation(Animation);
                    // *OK* ((IMcMeshItem) objectSchemeItem).GetAnimation(Animation, puPropertyID);
                    // *OK* ((IMcMeshItem) objectSchemeItem).SetAnimation(new SMcAnimation("Hi", true));
                    // *OK* ((IMcMeshItem) objectSchemeItem).GetAnimation(Animation);
                    // *OK* ((IMcMeshItem) objectSchemeItem).SetAnimation(new SMcAnimation("B", false), 4L);
                    // *OK* ((IMcMeshItem) objectSchemeItem).GetAnimation(Animation, puPropertyID);

                    IMcOverlayManager activeOverlayManager = null;
                    try {
                        activeOverlayManager = Manager_AMCTMapForm.getInstance().getCurViewport().GetOverlayManager();
                    }
                    catch (final MapCoreException e) {
                        e.printStackTrace();
                        getActivity().runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "GetOverlayManager()");
                            }
                        });
                    } catch (Exception e) {
                        e.printStackTrace();
                    }

                    IMcAnimationState[] animationStates = ((IMcMeshItem) objectSchemeItem).GetAnimationStates(obj);

                    // *OK* Boolean bCastShadows = new Boolean(false);
                    // *OK* puPropertyID = new Long(0);
                    // *OK* ((IMcMeshItem) objectSchemeItem).GetCastShadows(bCastShadows);
                    // *OK* ((IMcMeshItem) objectSchemeItem).GetCastShadows(bCastShadows, puPropertyID, uObjectStateToServe1);
                    // *OK* ((IMcMeshItem) objectSchemeItem).SetCastShadows(false);
                    // *OK* ((IMcMeshItem) objectSchemeItem).GetCastShadows(bCastShadows);
                    // *OK* ((IMcMeshItem) objectSchemeItem).SetCastShadows(true, 5L, uObjectStateToServe1);
                    // *OK* ((IMcMeshItem) objectSchemeItem).GetCastShadows(bCastShadows, puPropertyID, uObjectStateToServe1);

                    // *OK* SMcFVector3D Offset = new SMcFVector3D();
                    // *OK* puPropertyID = new Long(0);
                    // *OK* ((IMcMeshItem) objectSchemeItem).GetSubPartOffset(0, Offset);
                    // *OK* ((IMcMeshItem) objectSchemeItem).GetSubPartOffset(0, Offset, puPropertyID);
                    // *OK* ((IMcMeshItem) objectSchemeItem).SetSubPartOffset(0, new SMcFVector3D(2,2,2));
                    // *OK* ((IMcMeshItem) objectSchemeItem).GetSubPartOffset(0, Offset);
                    // *OK* ((IMcMeshItem) objectSchemeItem).SetSubPartOffset(0, new SMcFVector3D(3,3,3), 6L);
                    // *OK* ((IMcMeshItem) objectSchemeItem).GetSubPartOffset(0, Offset, puPropertyID);

                    // *OK* SMcRotation rotation = ((IMcMeshItem) objectSchemeItem).GetSubPartCurrRotation(Manager_AMCTMapForm.getInstance().getCurViewport(), obj, 0, true);
                    // *OK* rotation = IMcMeshItem.Static.GetSubPartCurrRotation(Manager_AMCTMapForm.getInstance().getCurViewport(), 6L, obj, true);

                    // *OK* Boolean bInheritsParentRotation = new Boolean(false);
                    // *OK* puPropertyID = new Long(0);
                    // *OK* ((IMcMeshItem) objectSchemeItem).GetSubPartInheritsParentRotation(0, bInheritsParentRotation);
                    // *OK* ((IMcMeshItem) objectSchemeItem).GetSubPartInheritsParentRotation(0, bInheritsParentRotation, puPropertyID);
                    // *OK* ((IMcMeshItem) objectSchemeItem).SetSubPartInheritsParentRotation(0, false);
                    // *OK* ((IMcMeshItem) objectSchemeItem).GetSubPartInheritsParentRotation(0, bInheritsParentRotation);
                    // *OK* ((IMcMeshItem) objectSchemeItem).SetSubPartInheritsParentRotation(0, true, 7L);
                    // *OK* ((IMcMeshItem) objectSchemeItem).GetSubPartInheritsParentRotation(0, bInheritsParentRotation, puPropertyID);

                    // *OK* SMcFVector2D vector2D = new SMcFVector2D();
                    // *OK* puPropertyID = new Long(0);
                    // *OK* ((IMcMeshItem) objectSchemeItem).GetTextureScrollSpeed(0, vector2D);
                    // *OK* ((IMcMeshItem) objectSchemeItem).GetTextureScrollSpeed(0, vector2D, puPropertyID, uObjectStateToServe1);
                    // *OK* ((IMcMeshItem) objectSchemeItem).SetTextureScrollSpeed(0, new SMcFVector2D(2,2));
                    // *OK* ((IMcMeshItem) objectSchemeItem).GetTextureScrollSpeed(0, vector2D);
                    // *OK* ((IMcMeshItem) objectSchemeItem).SetTextureScrollSpeed(0, new SMcFVector2D(3,3), 8L, uObjectStateToServe1);
                    // *OK* ((IMcMeshItem) objectSchemeItem).GetTextureScrollSpeed(0, vector2D, puPropertyID, uObjectStateToServe1);

                    // *OK* ObjectRef<IMcMeshItem.EBasePointAlignment> eBasePointAlignment = new ObjectRef<IMcMeshItem.EBasePointAlignment>();
                    // *OK* puPropertyID = new Long(0);
                    // *OK* ((IMcMeshItem) objectSchemeItem).GetBasePointAlignment(eBasePointAlignment);
                    // *OK* ((IMcMeshItem) objectSchemeItem).GetBasePointAlignment(eBasePointAlignment, puPropertyID, uObjectStateToServe1);
                    // *OK* ((IMcMeshItem) objectSchemeItem).SetBasePointAlignment(IMcMeshItem.EBasePointAlignment.EBPA_MESH_ZERO_LOWERED);
                    // *OK* ((IMcMeshItem) objectSchemeItem).GetBasePointAlignment(eBasePointAlignment);
                    // *OK* ((IMcMeshItem) objectSchemeItem).SetBasePointAlignment(IMcMeshItem.EBasePointAlignment.EBPA_BOUNDING_BOX_CENTER, 9L, uObjectStateToServe1);
                    // *OK* ((IMcMeshItem) objectSchemeItem).GetBasePointAlignment(eBasePointAlignment, puPropertyID, uObjectStateToServe1);

                    // *OK* boolean bParticipatesInTerrainHeight = ((IMcMeshItem) objectSchemeItem).GetParticipationInTerrainHeight();
                    // *OK* boolean bStatic = ((IMcMeshItem) objectSchemeItem).GetStatic();
                    // *OK* boolean bDisplaysItemsAttachedToTerrain = ((IMcMeshItem) objectSchemeItem).GetDisplayingItemsAttachedToTerrain();
                    // *OK* bDisplaysItemsAttachedToTerrain = ((IMcMeshItem) objectSchemeItem).GetDisplayingItemsAttachedToTerrain();
*/
                    break;
                case LINE_ITEM:
                   /*
                     // *OK* IMcLineItem LineCloneItemWithoutObject = ((IMcLineItem) objectSchemeItem).Clone();
                     // *OK* IMcLineItem LineCloneItem = ((IMcLineItem) objectSchemeItem).Clone(obj);

                    // *OK* IMcLineBasedItem.SSlopePresentationColor[] LineaColors = ((IMcLineItem) objectSchemeItem).GetSlopePresentationColors();
                    // *OK* IMcLineBasedItem.SSlopePresentationColor[] LineaColors1 = new IMcLineBasedItem.SSlopePresentationColor[1];
                    // *OK* LineaColors1[0] = new IMcLineBasedItem.SSlopePresentationColor();
                    // *OK* LineaColors1[0].fMaxSlope = 80;
                    // *OK* LineaColors1[0].Color = new SMcBColor(0, 255, 100, 100);
                    // *OK* ((IMcLineItem) objectSchemeItem).SetSlopePresentationColors(LineaColors1);
                    // *OK* LineaColors = ((IMcLineItem) objectSchemeItem).GetSlopePresentationColors();

                    // *OK* ObjectRef<IMcSpatialQueries.EQueryPrecision> oreQueryPrecision = new ObjectRef<IMcSpatialQueries.EQueryPrecision>();
                    // *OK* ((IMcLineItem) objectSchemeItem).GetSlopeQueryPrecision(oreQueryPrecision);
                    // *OK* puPropertyID = new Long(0);
                    uObjectStateToServe1 = 1;
                    // *OK* ((IMcLineItem) objectSchemeItem).GetSlopeQueryPrecision(oreQueryPrecision, puPropertyID, uObjectStateToServe1);
                    // *OK* ((IMcLineItem) objectSchemeItem).SetSlopeQueryPrecision(IMcSpatialQueries.EQueryPrecision.EQP_DEFAULT_PLUS_LOWEST);
                    // *OK* ((IMcLineItem) objectSchemeItem).GetSlopeQueryPrecision(oreQueryPrecision);
                    // *OK* ((IMcLineItem) objectSchemeItem).SetSlopeQueryPrecision(IMcSpatialQueries.EQueryPrecision.EQP_HIGHEST, 3L, uObjectStateToServe1);
                    // *OK* ((IMcLineItem) objectSchemeItem).GetSlopeQueryPrecision(oreQueryPrecision, puPropertyID, uObjectStateToServe1);

                    // *OK* Boolean LinebShowSlopePresentation = new Boolean(false);
                    // *OK* ((IMcLineItem) objectSchemeItem).GetShowSlopePresentation(LinebShowSlopePresentation);
                    // *OK* puPropertyID = new Long(0);
                    // *OK* ((IMcLineItem) objectSchemeItem).GetShowSlopePresentation(LinebShowSlopePresentation, puPropertyID, uObjectStateToServe1);
                    // *OK* ((IMcLineItem) objectSchemeItem).SetShowSlopePresentation(true);
                    // *OK* ((IMcLineItem) objectSchemeItem).GetShowSlopePresentation(LinebShowSlopePresentation);
                    // *OK* ((IMcLineItem) objectSchemeItem).SetShowSlopePresentation(true, 1L, uObjectStateToServe1);
                    // *OK* ((IMcLineItem) objectSchemeItem).GetShowSlopePresentation(LinebShowSlopePresentation, puPropertyID, uObjectStateToServe1);
*/
                    break;
            }

        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(v.getContext(), e, "IMcObject.SetProperty");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void checkNewWrappedSymbolicItem() {
        mView.queueEvent(new Runnable() {
            @Override
            public void run() {
                CMcEnumBitField<IMcObjectSchemeItem.EItemSubTypeFlags> itemSubType = new CMcEnumBitField<>(IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN);
                try {
                    // *OK* IMcManualGeometryItem manualGeometryItem = IMcManualGeometryItem.Static.Create(itemSubType, ObjectPropertiesBase.mLocationCoordSys, IMcProceduralGeometryItem.ERenderingMode.ERM_POINTS);

                    // empty symbolic
                    // *OK* IMcEmptySymbolicItem emptySymbolicItem = IMcEmptySymbolicItem.Static.Create();
                    //

                    // procedural geometry
                    // *OK* EMcPointCoordSystem ProceduralGeometryPointCoordSystem = ((IMcProceduralGeometryItem) manualGeometryItem).GetProceduralGeometryCoordinateSystem();
                    //

                    // manual geometry
                    // IMcOverlay activeOverlay =Manager_MCOverlayManager.getInstance().getActiveOverlay();
                    // IMcOverlayManager overlayManager = Manager_AMCTMapForm.getInstance().getCurViewport().GetOverlayManager();
                    // IMcObjectScheme objectScheme = IMcObjectScheme.Static.Create(overlayManager, manualGeometryItem, EMcPointCoordSystem.EPCS_WORLD, true);
                    // IMcObject obj = IMcObject.Static.Create(activeOverlay, objectScheme);
                    // *OK* IMcManualGeometryItem ManualGeometryCloneItemWithoutObject = manualGeometryItem.Clone();
                    // *OK* IMcManualGeometryItem ManualGeometryCloneItem = manualGeometryItem.Clone(obj);

                    // *OK* ObjectRef<IMcProceduralGeometryItem.ERenderingMode> eRenderingMode = new ObjectRef<IMcProceduralGeometryItem.ERenderingMode>();
                    // *OK* manualGeometryItem.GetRenderingMode(eRenderingMode);
                    // Long puPropertyID = new Long(0);
                    // byte uObjectStateToServe1 = 1;
                    // *OK* manualGeometryItem.GetRenderingMode(eRenderingMode, puPropertyID, uObjectStateToServe1);
                    // *OK* manualGeometryItem.SetRenderingMode(IMcProceduralGeometryItem.ERenderingMode.ERM_POINTS);
                    // *OK* manualGeometryItem.GetRenderingMode(eRenderingMode);
                    // *OK* manualGeometryItem.SetRenderingMode(IMcProceduralGeometryItem.ERenderingMode.ERM_LINES, 3L, uObjectStateToServe1);
                    // *OK* manualGeometryItem.GetRenderingMode(eRenderingMode, puPropertyID, uObjectStateToServe1);

                    // *OK* ObjectRef<IMcTexture> pTexture = new ObjectRef<IMcTexture>();
                    // *OK* manualGeometryItem.GetTexture(pTexture);
                    // *OK* puPropertyID = new Long(0);
                    // *OK* manualGeometryItem.GetTexture(pTexture, puPropertyID, uObjectStateToServe1);
                    // *OK* manualGeometryItem.SetTexture(ObjectPropertiesBase.mFillTexture);
                    // *OK* manualGeometryItem.GetTexture(pTexture);
                    // *OK* manualGeometryItem.SetTexture(ObjectPropertiesBase.mLineTexture, 3L, uObjectStateToServe1);
                    // *OK* manualGeometryItem.GetTexture(pTexture, puPropertyID, uObjectStateToServe1);

                    // *OK* IMcProperty.SArrayProperty<Integer> auConnectionIndices = new IMcProperty.SArrayProperty<>();
                    // *OK* manualGeometryItem.GetConnectionIndices(auConnectionIndices);
                    // *OK* puPropertyID = new Long(0);
                    // *OK* Integer[] integer = new Integer[1];
                    // *OK* integer[0] = new Integer(0);
                    // *OK* manualGeometryItem.GetConnectionIndices(auConnectionIndices, puPropertyID, uObjectStateToServe1);
                    // *OK* auConnectionIndices = new IMcProperty.SArrayProperty<>(integer);
                    // *OK* manualGeometryItem.SetConnectionIndices(auConnectionIndices);
                    // *OK* manualGeometryItem.GetConnectionIndices(auConnectionIndices);
                    // *OK* manualGeometryItem.SetConnectionIndices(auConnectionIndices, 3L, uObjectStateToServe1);
                    // *OK* manualGeometryItem.GetConnectionIndices(auConnectionIndices, puPropertyID, uObjectStateToServe1);

                    // *OK* IMcProperty.SArrayProperty<SMcVector3D> aPointsCoordinates = new IMcProperty.SArrayProperty<>();
                    // *OK* manualGeometryItem.GetPointsCoordinates(aPointsCoordinates);
                    // *OK* puPropertyID = new Long(0);
                    // *OK* SMcVector3D[] vector3D = new SMcVector3D[1];
                    // *OK* vector3D[0] = new SMcVector3D();
                    // *OK* manualGeometryItem.GetPointsCoordinates(aPointsCoordinates, puPropertyID, uObjectStateToServe1);
                    // *OK* aPointsCoordinates = new IMcProperty.SArrayProperty<>(vector3D);
                    // *OK* manualGeometryItem.SetPointsCoordinates(aPointsCoordinates);
                    // *OK* manualGeometryItem.GetPointsCoordinates(aPointsCoordinates);
                    // *OK* manualGeometryItem.SetPointsCoordinates(aPointsCoordinates, 4L, uObjectStateToServe1);
                    // *OK* manualGeometryItem.GetPointsCoordinates(aPointsCoordinates, puPropertyID, uObjectStateToServe1);

                    // *OK* IMcProperty.SArrayProperty<SMcFVector2D> aPointsTextureCoordinates = new IMcProperty.SArrayProperty<>();
                    // *OK* manualGeometryItem.GetPointsTextureCoordinates(aPointsTextureCoordinates);
                    // *OK* puPropertyID = new Long(0);
                    // *OK* SMcFVector2D[] fVector2D = new SMcFVector2D[1];
                    // *OK* fVector2D[0] = new SMcFVector2D();
                    // *OK* manualGeometryItem.GetPointsTextureCoordinates(aPointsTextureCoordinates, puPropertyID, uObjectStateToServe1);
                    // *OK* aPointsTextureCoordinates = new IMcProperty.SArrayProperty<>(fVector2D);
                    // *OK* manualGeometryItem.SetPointsTextureCoordinates(aPointsTextureCoordinates);
                    // *OK* manualGeometryItem.GetPointsTextureCoordinates(aPointsTextureCoordinates);
                    // *OK* manualGeometryItem.SetPointsTextureCoordinates(aPointsTextureCoordinates, 5L, uObjectStateToServe1);
                    // *OK* manualGeometryItem.GetPointsTextureCoordinates(aPointsTextureCoordinates, puPropertyID, uObjectStateToServe1);

                    // *OK* IMcProperty.SArrayProperty<SMcBColor> aPointsColors = new IMcProperty.SArrayProperty<>();
                    // *OK* manualGeometryItem.GetPointsColors(aPointsColors);
                    // *OK* puPropertyID = new Long(0);
                    // *OK* SMcBColor[] bColor = new SMcBColor[1];
                    // *OK* bColor[0] = new SMcBColor();
                    // *OK* manualGeometryItem.GetPointsColors(aPointsColors, puPropertyID, uObjectStateToServe1);
                    // *OK* aPointsColors = new IMcProperty.SArrayProperty<>(bColor);
                    // *OK* manualGeometryItem.SetPointsColors(aPointsColors);
                    // *OK* manualGeometryItem.GetPointsColors(aPointsColors);
                    // *OK* manualGeometryItem.SetPointsColors(aPointsColors, 6L, uObjectStateToServe1);
                    // *OK* manualGeometryItem.GetPointsColors(aPointsColors, puPropertyID, uObjectStateToServe1);

                    // *OK* ObjectRef<SMcVector3D[]> paPointsCoordinates = new ObjectRef<SMcVector3D[]>();
                    // *OK* ObjectRef<SMcFVector2D[]> paPointsTextureCoordinates =  new ObjectRef<SMcFVector2D[]>();
                    // *OK* ObjectRef<SMcBColor[]> paPointsColors = new ObjectRef<SMcBColor[]>();
                    // *OK* SMcVector3D PointsCoordinates[] = new SMcVector3D[1];
                    // *OK* PointsCoordinates[0] = new SMcVector3D();
                    // *OK* SMcFVector2D PointsTextureCoordinates[] = new SMcFVector2D[1];
                    // *OK* PointsTextureCoordinates[0] = new SMcFVector2D();
                    // *OK* SMcBColor PointsColors[] = new SMcBColor[1];
                    // *OK* PointsColors[0] = new SMcBColor();
                    // *OK* manualGeometryItem.GetPointsData(paPointsCoordinates, paPointsTextureCoordinates, paPointsColors);
                    // *OK* manualGeometryItem.SetPointsData(PointsCoordinates, PointsTextureCoordinates , PointsColors);
                    // *OK* manualGeometryItem.GetPointsData(paPointsCoordinates, paPointsTextureCoordinates, paPointsColors);

                } catch (Exception e) {
                    e.printStackTrace();
                }
            }});
    }

    private void checkNewWrappedPhysicalItem(final IMcObjectSchemeItem objectSchemeItem ) {
        mView.queueEvent(new Runnable() {
            @Override
            public void run() {
                try {
                    IMcOverlayManager activeOverlayManager = null;
                    try {
                        activeOverlayManager = Manager_AMCTMapForm.getInstance().getCurViewport().GetOverlayManager();
                    }
                    catch (final MapCoreException e) {
                        e.printStackTrace();
                        getActivity().runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "GetOverlayManager()");
                            }
                        });
                    } catch (Exception e) {
                        e.printStackTrace();
                    }

                    // IMcObjectSchemeItem objectSchemeItem = null;
                    // *OK* objectSchemeItem = (IMcLightBasedItem) IMcDirectionalLightItem.Static.Create();
                    // *OK* objectSchemeItem = (IMcLocationBasedLightItem) IMcPointLightItem.Static.Create();
                    // *OK* objectSchemeItem = IMcSpotLightItem.Static.Create();
                    // *OK* objectSchemeItem = IMcParticleEffectItem.Static.Create("hello");
                    // *OK* objectSchemeItem = IMcEmptyPhysicalItem.Static.Create();
                    // *OK* objectSchemeItem = IMcProjectorItem.Static.Create(ObjectPropertiesBase.mLineTexture, 1F, 1F);
                    // *OK* objectSchemeItem = IMcSoundItem.Static.Create( "soundName");

                    //IMcObjectScheme objectScheme = IMcObjectScheme.Static.Create(activeOverlayManager, objectSchemeItem, EMcPointCoordSystem.EPCS_WORLD, true);
                    //IMcOverlay activeOverlay = Manager_MCOverlayManager.getInstance().getActiveOverlay();
                    //IMcObject obj = IMcObject.Static.Create(activeOverlay, objectScheme);

                    if (objectSchemeItem instanceof IMcPhysicalItem)
                    {
                        /*try{
                            ObjectRef<Integer> var1 = new ObjectRef<>();
                            ObjectRef<Long> var2 = new ObjectRef<>();
                        ((IMcPhysicalItem) objectSchemeItem).GetAttachPoint(var1, var2);
                        int i=0;
                        int h=i+9;
                    }
                    catch (final MapCoreException e) {
                        e.printStackTrace();


                    } catch (Exception e) {
                        e.printStackTrace();
                    }*/
                        /*
                        // *OK* IMcPhysicalItem CloneItemWithoutObject = ((IMcPhysicalItem) objectSchemeItem).Clone();
                        // *OK* IMcPhysicalItem CloneItem = ((IMcPhysicalItem) objectSchemeItem).Clone(obj);

                        // *OK* Integer uAttachPoint = new Integer(2);
                        Long puPropertyID = new Long(0);
                        byte uObjectStateToServe1 = 1;
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).GetAttachPoint(uAttachPoint);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).GetAttachPoint(uAttachPoint, puPropertyID, uObjectStateToServe1);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).SetAttachPoint(1);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).GetAttachPoint(uAttachPoint);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).SetAttachPoint(2, 3L, uObjectStateToServe1);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).GetAttachPoint(uAttachPoint, puPropertyID, uObjectStateToServe1);

                        // *OK* SMcFVector3D Offset = new SMcFVector3D();
                        // *OK* puPropertyID = new Long(0);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).GetOffset(Offset);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).GetOffset(Offset, puPropertyID, uObjectStateToServe1);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).SetOffset(new SMcFVector3D(2,2,2));
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).GetOffset(Offset);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).SetOffset(new SMcFVector3D(3,3,3), 4L, uObjectStateToServe1);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).GetOffset(Offset, puPropertyID, uObjectStateToServe1);

                        // *OK* SMcRotation Rotation = new SMcRotation();
                        // *OK* Long puPropertyID = new Long(0);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).GetRotation(Rotation);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).GetRotation(Rotation, puPropertyID);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).SetRotation(new SMcRotation(2,2,2));
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).GetRotation(Rotation);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).SetRotation(new SMcRotation(3,3,3), 5L);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).GetRotation(Rotation, puPropertyID);

                        // *OK* Rotation = ((IMcPhysicalItem) objectSchemeItem).GetCurrRotation(Manager_AMCTMapForm.getInstance().getCurViewport(), obj, true);
                        // *OK* Rotation = IMcPhysicalItem.Static.GetCurrRotation(Manager_AMCTMapForm.getInstance().getCurViewport(), 5L, obj, true);

                        // *OK* Boolean bInheritsParentRotation= new Boolean(false);
                        // *OK* puPropertyID = new Long(0);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).GetInheritsParentRotation(bInheritsParentRotation);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).GetInheritsParentRotation(bInheritsParentRotation, puPropertyID);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).SetInheritsParentRotation(true);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).GetInheritsParentRotation(bInheritsParentRotation);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).SetInheritsParentRotation(false, 6L);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).GetInheritsParentRotation(bInheritsParentRotation, puPropertyID);

                        // *OK* SMcFVector3D Scale = new SMcFVector3D();
                        // *OK* puPropertyID = new Long(0);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).GetScale(Scale);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).GetScale(Scale, puPropertyID, uObjectStateToServe1);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).SetScale(new SMcFVector3D(2,2,2));
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).GetScale(Scale);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).SetScale(new SMcFVector3D(3,3,3), 7L, uObjectStateToServe1);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).GetScale(Scale, puPropertyID, uObjectStateToServe1);

                        // *OK* Boolean bParallelToTerrain = new Boolean(false);
                        // *OK* puPropertyID = new Long(0);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).GetParallelToTerrain(bParallelToTerrain);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).GetParallelToTerrain(bParallelToTerrain, puPropertyID, uObjectStateToServe1);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).SetParallelToTerrain(true);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).GetParallelToTerrain(bParallelToTerrain);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).SetParallelToTerrain(false, 8L, uObjectStateToServe1);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).GetParallelToTerrain(bParallelToTerrain, puPropertyID, uObjectStateToServe1);

                        // *OK* Boolean pbEnabled = new Boolean(false);
                        // *OK* SMcFColor Color = new SMcFColor();
                        // *OK* Float pfloat = new Float(0);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).GetColorModulateEffect(obj, pbEnabled, Color, pfloat);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).SetColorModulateEffect(obj, new SMcFColor(2,2,2,2), 2F);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).GetColorModulateEffect(obj, pbEnabled, Color, pfloat);

                        // *OK* ((IMcPhysicalItem) objectSchemeItem).RemoveColorModulateEffect(obj);

                        // *OK* Float fFadeTimeMS = new Float(0);
                        // *OK* Boolean bWireFrameOnly = new Boolean(false);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).GetWireFrameEffect(obj, pbEnabled, Color, fFadeTimeMS, bWireFrameOnly);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).SetWireFrameEffect(obj, new SMcFColor(3,3,3,3), 2F, true);
                        // *OK* ((IMcPhysicalItem) objectSchemeItem).GetWireFrameEffect(obj, pbEnabled, Color, fFadeTimeMS, bWireFrameOnly);

                        // *OK* ((IMcPhysicalItem) objectSchemeItem).RemoveWireFrameEffect(obj);
*/
                    }

                    if (objectSchemeItem instanceof IMcLightBasedItem) {
                        /*

                        // *OK* SMcFColor DiffuseColor = new SMcFColor();
                        // *OK* Long puPropertyID = new Long(0);
                        // *OK* byte uObjectStateToServe1 = 1;
                        // *OK* ((IMcLightBasedItem) objectSchemeItem).GetDiffuseColor(DiffuseColor);
                        // *OK* ((IMcLightBasedItem) objectSchemeItem).GetDiffuseColor(DiffuseColor, puPropertyID, uObjectStateToServe1);
                        // *OK* ((IMcLightBasedItem) objectSchemeItem).SetDiffuseColor(new SMcFColor(2,2,2,2));
                        // *OK* ((IMcLightBasedItem) objectSchemeItem).GetDiffuseColor(DiffuseColor);
                        // *OK* ((IMcLightBasedItem) objectSchemeItem).SetDiffuseColor(new SMcFColor(3,3,3,3), 3L, uObjectStateToServe1);
                        // *OK* ((IMcLightBasedItem) objectSchemeItem).GetDiffuseColor(DiffuseColor, puPropertyID, uObjectStateToServe1);

                        // *OK* SMcFColor SpecularColor = new SMcFColor();
                        // *OK* puPropertyID = new Long(0);
                        // *OK* ((IMcLightBasedItem) objectSchemeItem).GetSpecularColor(SpecularColor);
                        // *OK* ((IMcLightBasedItem) objectSchemeItem).GetSpecularColor(SpecularColor, puPropertyID, uObjectStateToServe1);
                        // *OK* ((IMcLightBasedItem) objectSchemeItem).SetSpecularColor(new SMcFColor(2,2,2,2));
                        // *OK* ((IMcLightBasedItem) objectSchemeItem).GetSpecularColor(SpecularColor);
                        // *OK* ((IMcLightBasedItem) objectSchemeItem).SetSpecularColor(new SMcFColor(3,3,3,3), 3L, uObjectStateToServe1);
                        // *OK* ((IMcLightBasedItem) objectSchemeItem).GetSpecularColor(SpecularColor, puPropertyID, uObjectStateToServe1);
*/
                    }

                    if (objectSchemeItem instanceof IMcLocationBasedLightItem) {
                        /*

                        // *OK* SMcAttenuation Attenuation = new SMcAttenuation();
                        // *OK* Long puPropertyID = new Long(0);
                        // *OK* byte uObjectStateToServe1 = 1;
                        // *OK* ((IMcLocationBasedLightItem) objectSchemeItem).GetAttenuation(Attenuation);
                        // *OK* ((IMcLocationBasedLightItem) objectSchemeItem).GetAttenuation(Attenuation, puPropertyID, uObjectStateToServe1);
                        // *OK* ((IMcLocationBasedLightItem) objectSchemeItem).SetAttenuation(new SMcAttenuation(1,2,3,4));
                        // *OK* ((IMcLocationBasedLightItem) objectSchemeItem).GetAttenuation(Attenuation);
                        // *OK* ((IMcLocationBasedLightItem) objectSchemeItem).SetAttenuation(new SMcAttenuation(2,3,4,5), 3L, uObjectStateToServe1);
                        // *OK* ((IMcLocationBasedLightItem) objectSchemeItem).GetAttenuation(Attenuation, puPropertyID, uObjectStateToServe1);
*/
                    }

                    IMcObjectSchemeNode.EObjectSchemeNodeType type = objectSchemeItem.GetNodeType();
                    switch (type) {
                        case POINT_LIGHT:
                            /*
                            // *OK* IMcPointLightItem pointLightCloneItemWithoutObject = ((IMcPointLightItem) objectSchemeItem).Clone();
                            // *OK* IMcPointLightItem pointLightCloneItem = ((IMcPointLightItem) objectSchemeItem).Clone(obj);
*/

                        case SPOT_LIGHT:
                            /*
                            // *OK* IMcSpotLightItem SpotLightCloneItemWithoutObject = ((IMcSpotLightItem) objectSchemeItem).Clone();
                            // *OK* IMcSpotLightItem SpotLightCloneItem = ((IMcSpotLightItem) objectSchemeItem).Clone(obj);

                            // *OK* SMcFVector3D Direction = new SMcFVector3D();
                            Long puPropertyID = new Long(0);
                            byte uObjectStateToServe1 = 1;
                            // *OK* ((IMcSpotLightItem) objectSchemeItem).GetDirection(Direction);
                            // *OK* ((IMcSpotLightItem) objectSchemeItem).GetDirection(Direction, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcSpotLightItem) objectSchemeItem).SetDirection(new SMcFVector3D(1,2,3));
                            // *OK* ((IMcSpotLightItem) objectSchemeItem).GetDirection(Direction);
                            // *OK* ((IMcSpotLightItem) objectSchemeItem).SetDirection(new SMcFVector3D(4,5,6), 3L, uObjectStateToServe1);
                            // *OK* ((IMcSpotLightItem) objectSchemeItem).GetDirection(Direction, puPropertyID, uObjectStateToServe1);

                            // *OK* Float fHalfOuterAngle = new Float(0);
                            // *OK* puPropertyID = new Long(0);
                            // *OK* ((IMcSpotLightItem) objectSchemeItem).GetHalfOuterAngle(fHalfOuterAngle);
                            // *OK* ((IMcSpotLightItem) objectSchemeItem).GetHalfOuterAngle(fHalfOuterAngle, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcSpotLightItem) objectSchemeItem).SetHalfOuterAngle(2F);
                            // *OK* ((IMcSpotLightItem) objectSchemeItem).GetHalfOuterAngle(fHalfOuterAngle);
                            // *OK* ((IMcSpotLightItem) objectSchemeItem).SetHalfOuterAngle(3F, 4L, uObjectStateToServe1);
                            // *OK* ((IMcSpotLightItem) objectSchemeItem).GetHalfOuterAngle(fHalfOuterAngle, puPropertyID, uObjectStateToServe1);

                            // *OK* Float fHalfInnerAngle = new Float(0);
                            // *OK* puPropertyID = new Long(0);
                            // *OK* ((IMcSpotLightItem) objectSchemeItem).GetHalfInnerAngle(fHalfInnerAngle);
                            // *OK* ((IMcSpotLightItem) objectSchemeItem).GetHalfInnerAngle(fHalfInnerAngle, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcSpotLightItem) objectSchemeItem).SetHalfInnerAngle(2F);
                            // *OK* ((IMcSpotLightItem) objectSchemeItem).GetHalfInnerAngle(fHalfInnerAngle);
                            // *OK* ((IMcSpotLightItem) objectSchemeItem).SetHalfInnerAngle(3F, 4L, uObjectStateToServe1);
                            // *OK* ((IMcSpotLightItem) objectSchemeItem).GetHalfInnerAngle(fHalfInnerAngle, puPropertyID, uObjectStateToServe1);
*/

                        case DIRECTIONAL_LIGHT:
                            /*
                            // *OK* IMcDirectionalLightItem directionalCloneItemWithoutObject = ((IMcDirectionalLightItem) objectSchemeItem).Clone();
                            // *OK* IMcDirectionalLightItem directionalCloneItem = ((IMcDirectionalLightItem) objectSchemeItem).Clone(obj);

                            // *OK* Direction = new SMcFVector3D();
                            // *OK* puPropertyID = new Long(0);
                            // *OK* uObjectStateToServe1 = 1;
                            // *OK* ((IMcDirectionalLightItem) objectSchemeItem).GetDirection(Direction);
                            // *OK* ((IMcDirectionalLightItem) objectSchemeItem).GetDirection(Direction, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcDirectionalLightItem) objectSchemeItem).SetDirection(new SMcFVector3D(1,2,3));
                            // *OK* ((IMcDirectionalLightItem) objectSchemeItem).GetDirection(Direction);
                            // *OK* ((IMcDirectionalLightItem) objectSchemeItem).SetDirection(new SMcFVector3D(2,3, 4), 4L, uObjectStateToServe1);
                            // *OK* ((IMcDirectionalLightItem) objectSchemeItem).GetDirection(Direction, puPropertyID, uObjectStateToServe1);
*/

                        case PARTICLE_EFFECT_ITEM:
                            /*
                            // *OK* IMcParticleEffectItem ParticleEffectCloneItemWithoutObject = ((IMcParticleEffectItem) objectSchemeItem).Clone();
                            // *OK* IMcParticleEffectItem ParticleEffectCloneItem = ((IMcParticleEffectItem) objectSchemeItem).Clone(obj);

                            // *OK* WrapperString strEffectName = new WrapperString();
                            // *OK* Long puPropertyID = new Long(0);
                            // *OK* byte uObjectStateToServe1 = 1;
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetEffectName(strEffectName);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetEffectName(strEffectName, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).SetEffectName(new String("Hi"));
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetEffectName(strEffectName);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).SetEffectName(new String("world"), 3L, uObjectStateToServe1);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetEffectName(strEffectName, puPropertyID, uObjectStateToServe1);

                            // *OK* ObjectRef<IMcParticleEffectItem.EState> peState = new ObjectRef<IMcParticleEffectItem.EState>();
                            // *OK* puPropertyID = new Long(0);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetState(peState);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetState(peState, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).SetState(IMcParticleEffectItem.EState.ES_RUNNING);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetState(peState);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).SetState(IMcParticleEffectItem.EState.ES_PAUSED, 4L, uObjectStateToServe1);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetState(peState, puPropertyID, uObjectStateToServe1);

                            // *OK* Float fSeconds = new Float(0);
                            // *OK* puPropertyID = new Long(0);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetStartingTimePoint(fSeconds);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetStartingTimePoint(fSeconds, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).SetStartingTimePoint(2F);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetStartingTimePoint(fSeconds);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).SetStartingTimePoint(3F, 5L, uObjectStateToServe1);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetStartingTimePoint(fSeconds, puPropertyID, uObjectStateToServe1);

                            // *OK* puPropertyID = new Long(0);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetStartingDelay(fSeconds);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetStartingDelay(fSeconds, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).SetStartingDelay(2F);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetStartingDelay(fSeconds);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).SetStartingDelay(3F, 5L, uObjectStateToServe1);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetStartingDelay(fSeconds, puPropertyID, uObjectStateToServe1);

                            // *OK* puPropertyID = new Long(0);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetSamplingStep(fSeconds);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetSamplingStep(fSeconds, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).SetSamplingStep(2F);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetSamplingStep(fSeconds);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).SetSamplingStep(3F, 5L, uObjectStateToServe1);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetSamplingStep(fSeconds, puPropertyID, uObjectStateToServe1);

                            // *OK* Float fFactor = new Float(0);
                            // *OK* puPropertyID = new Long(0);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetTimeFactor(fFactor);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetTimeFactor(fFactor, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).SetTimeFactor(2F);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetTimeFactor(fFactor);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).SetTimeFactor(3F, 5L, uObjectStateToServe1);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetTimeFactor(fFactor, puPropertyID, uObjectStateToServe1);

                            // *OK* Float fVelocity = new Float(0);
                            // *OK* puPropertyID = new Long(0);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetParticleVelocity(fVelocity);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetParticleVelocity(fVelocity, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).SetParticleVelocity(2F);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetParticleVelocity(fVelocity);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).SetParticleVelocity(3F, 5L, uObjectStateToServe1);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetParticleVelocity(fVelocity, puPropertyID, uObjectStateToServe1);

                            // *OK* Float f = new Float(0);
                            // *OK* puPropertyID = new Long(0);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetParticleVelocity(f);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetParticleVelocity(f, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).SetParticleVelocity(2F);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetParticleVelocity(f);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).SetParticleVelocity(3F, 5L, uObjectStateToServe1);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetParticleVelocity(f, puPropertyID, uObjectStateToServe1);

                            // *OK* Float fAngleDegrees = new Float(0);;
                            // *OK* puPropertyID = new Long(0);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetParticleAngle(fAngleDegrees);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetParticleAngle(fAngleDegrees, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).SetParticleAngle(2F);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetParticleAngle(fAngleDegrees);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).SetParticleAngle(3F, 5L, uObjectStateToServe1);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetParticleAngle(fAngleDegrees, puPropertyID, uObjectStateToServe1);

                            // *OK* Float fEmissionRate = new Float(0);;
                            // *OK* puPropertyID = new Long(0);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetParticleEmissionRate(fEmissionRate);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetParticleEmissionRate(fEmissionRate, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).SetParticleEmissionRate(2F);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetParticleEmissionRate(fEmissionRate);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).SetParticleEmissionRate(3F, 5L, uObjectStateToServe1);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetParticleEmissionRate(fEmissionRate, puPropertyID, uObjectStateToServe1);

                            // *OK* Float fSeconds = new Float(0);
                            // *OK* puPropertyID = new Long(0);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetTimeToLive(fSeconds);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetTimeToLive(fSeconds, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).SetTimeToLive(2F);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetTimeToLive(fSeconds);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).SetTimeToLive(3F, 5L, uObjectStateToServe1);
                            // *OK* ((IMcParticleEffectItem) objectSchemeItem).GetTimeToLive(fSeconds, puPropertyID, uObjectStateToServe1);
*/

                        case PROJECTOR_ITEM:
                            /*
                            // *OK* IMcProjectorItem ProjectorCloneItemWithoutObject = ((IMcProjectorItem) objectSchemeItem).Clone();
                            // *OK* IMcProjectorItem ProjectorCloneItem = ((IMcProjectorItem) objectSchemeItem).Clone(obj);

                            // *OK* ObjectRef<IMcTexture> pTexture = new ObjectRef<IMcTexture>();
                            Long puPropertyID = new Long(0);
                            byte uObjectStateToServe1 = 1;
                            // *OK* ((IMcProjectorItem) objectSchemeItem).GetTexture(pTexture);
                            // *OK* ((IMcProjectorItem) objectSchemeItem).GetTexture(pTexture, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcProjectorItem) objectSchemeItem).SetTexture(ObjectPropertiesBase.mFillTexture);
                            // *OK* ((IMcProjectorItem) objectSchemeItem).GetTexture(pTexture);
                            // *OK* ((IMcProjectorItem) objectSchemeItem).SetTexture(ObjectPropertiesBase.mLineTexture, 3L, uObjectStateToServe1);
                            // *OK* ((IMcProjectorItem) objectSchemeItem).GetTexture(pTexture, puPropertyID, uObjectStateToServe1);

                            // *OK* boolean bIsUsingTextureMetadata = ((IMcProjectorItem) objectSchemeItem).IsUsingTextureMetadata();

                            // *OK* Float fHalfFieldOfViewHorizAngle = new Float(0);
                            // *OK* puPropertyID = new Long(0);
                            // *OK* ((IMcProjectorItem) objectSchemeItem).GetFieldOfView(fHalfFieldOfViewHorizAngle);
                            // *OK* ((IMcProjectorItem) objectSchemeItem).GetFieldOfView(fHalfFieldOfViewHorizAngle, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcProjectorItem) objectSchemeItem).SetFieldOfView(2F);
                            // *OK* ((IMcProjectorItem) objectSchemeItem).GetFieldOfView(fHalfFieldOfViewHorizAngle);
                            // *OK* ((IMcProjectorItem) objectSchemeItem).SetFieldOfView(3F, 3L, uObjectStateToServe1);
                            // *OK* ((IMcProjectorItem) objectSchemeItem).GetFieldOfView(fHalfFieldOfViewHorizAngle, puPropertyID, uObjectStateToServe1);

                            // *OK* Float fAspectRatio = new Float(0);
                            // *OK* puPropertyID = new Long(0);
                            // *OK* ((IMcProjectorItem) objectSchemeItem).GetAspectRatio(fAspectRatio);
                            // *OK* ((IMcProjectorItem) objectSchemeItem).GetAspectRatio(fAspectRatio, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcProjectorItem) objectSchemeItem).SetAspectRatio(2F);
                            // *OK* ((IMcProjectorItem) objectSchemeItem).GetAspectRatio(fAspectRatio);
                            // *OK* ((IMcProjectorItem) objectSchemeItem).SetAspectRatio(3F, 3L, uObjectStateToServe1);
                            // *OK* ((IMcProjectorItem) objectSchemeItem).GetAspectRatio(fAspectRatio, puPropertyID, uObjectStateToServe1);

                            // *OK* ObjectRef<CMcEnumBitField<IMcProjectorItem.ETargetTypesFlags> > uTargetTypesBitField = new ObjectRef<CMcEnumBitField<IMcProjectorItem.ETargetTypesFlags> >();
                            // *OK* puPropertyID = new Long(0);
                            // *OK* ((IMcProjectorItem) objectSchemeItem).GetTargetTypes(uTargetTypesBitField);
                            // *OK* ((IMcProjectorItem) objectSchemeItem).GetTargetTypes(uTargetTypesBitField, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcProjectorItem) objectSchemeItem).SetTargetTypes(new CMcEnumBitField<>(IMcProjectorItem.ETargetTypesFlags.ETTF_STATIC_OBJECTS));
                            // *OK* ((IMcProjectorItem) objectSchemeItem).GetTargetTypes(uTargetTypesBitField);
                            // *OK* ((IMcProjectorItem) objectSchemeItem).SetTargetTypes(new CMcEnumBitField<>(IMcProjectorItem.ETargetTypesFlags.ETTF_UNBLOCKED_ONLY), 4L, uObjectStateToServe1);
                            // *OK* ((IMcProjectorItem) objectSchemeItem).GetTargetTypes(uTargetTypesBitField, puPropertyID, uObjectStateToServe1);

                            // *OK* Float pfLeft = new Float(0), pfTop = new Float(0), pfRight = new Float(0), pfBottom = new Float(0);
                            // *OK* ((IMcProjectorItem) objectSchemeItem).GetProjectionBorders(pfLeft, pfTop, pfRight, pfBottom);
                            // *OK* ((IMcProjectorItem) objectSchemeItem).SetProjectionBorders(2F, 2F, 2F, 2F);
                            // *OK* ((IMcProjectorItem) objectSchemeItem).GetProjectionBorders(pfLeft, pfTop, pfRight, pfBottom);
                            // *OK* ((IMcProjectorItem) objectSchemeItem).GetProjectionBorders(pfLeft, pfTop, pfRight, pfBottom);
*/

                        case SOUND_ITEM:
                            /*
                            // *OK* IMcSoundItem SoundCloneItemWithoutObject = ((IMcSoundItem) objectSchemeItem).Clone();
                            // *OK* IMcSoundItem SoundCloneItem = ((IMcSoundItem) objectSchemeItem).Clone(obj);

                            // *OK* WrapperString strSoundName = new WrapperString();
                            // Long puPropertyID = new Long(0);
                            // byte uObjectStateToServe1 = 1;
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetSoundName(strSoundName);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetSoundName(strSoundName, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetSoundName("hello");
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetSoundName(strSoundName);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetSoundName("world", 3L, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetSoundName(strSoundName, puPropertyID, uObjectStateToServe1);

                            // *OK* ObjectRef<IMcSoundItem.EState> eState = new ObjectRef<IMcSoundItem.EState>();
                            // *OK* puPropertyID = new Long(0);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetState(eState);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetState(eState, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetState(IMcSoundItem.EState.ES_RUNNING);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetState(eState);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetState(IMcSoundItem.EState.ES_PAUSED, 4L, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetState(eState, puPropertyID, uObjectStateToServe1);

                            // *OK* Float fSeconds = new Float(0);
                            // *OK* puPropertyID = new Long(0);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetStartingTimePoint(fSeconds);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetStartingTimePoint(fSeconds, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetStartingTimePoint(3F);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetStartingTimePoint(fSeconds);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetStartingTimePoint(2F, 5L, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetStartingTimePoint(fSeconds, puPropertyID, uObjectStateToServe1);

                            // *OK* fSeconds = new Float(0);
                            // *OK* puPropertyID = new Long(0);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetTimePoint(fSeconds);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetTimePoint(fSeconds, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetTimePoint(3F);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetTimePoint(fSeconds);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetTimePoint(2F, 6L, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetTimePoint(fSeconds, puPropertyID, uObjectStateToServe1);

                            // *OK* float fCurrTimePoint = ((IMcSoundItem) objectSchemeItem).GetCurrTimePoint(Manager_AMCTMapForm.getInstance().getCurViewport(), obj);
                            // *OK* fCurrTimePoint = IMcSoundItem.Static.GetCurrTimePoint(Manager_AMCTMapForm.getInstance().getCurViewport(), 5L, obj);

                            // *OK* Boolean bLoop = new Boolean(false);
                            // *OK* puPropertyID = new Long(0);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetLoop(bLoop);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetLoop(bLoop, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetLoop(true);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetLoop(bLoop);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetLoop(false, 8L, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetLoop(bLoop, puPropertyID, uObjectStateToServe1);

                            // *OK* Float fVolume = new Float(0);
                            // *OK* puPropertyID = new Long(0);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetVolume(fVolume);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetVolume(fVolume, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetVolume(3F);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetVolume(fVolume);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetVolume(2F, 9L, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetVolume(fVolume, puPropertyID, uObjectStateToServe1);

                            // *OK* Float fMinVolume = new Float(0);
                            // *OK* puPropertyID = new Long(0);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetMinVolume(fMinVolume);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetMinVolume(fMinVolume, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetMinVolume(3F);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetMinVolume(fMinVolume);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetMinVolume(2F, 9L, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetMinVolume(fMinVolume, puPropertyID, uObjectStateToServe1);

                            // *OK* Float fMaxVolume = new Float(0);
                            // *OK* puPropertyID = new Long(0);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetMaxVolume(fMaxVolume);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetMaxVolume(fMaxVolume, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetMaxVolume(3F);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetMaxVolume(fMaxVolume);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetMaxVolume(2F, 9L, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetMaxVolume(fMaxVolume, puPropertyID, uObjectStateToServe1);

                            // *OK* Float fRollOffFactor = new Float(0);
                            // *OK* puPropertyID = new Long(0);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetRollOffFactor(fRollOffFactor);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetRollOffFactor(fRollOffFactor, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetRollOffFactor(3F);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetRollOffFactor(fRollOffFactor);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetRollOffFactor(2F, 9L, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetRollOffFactor(fRollOffFactor, puPropertyID, uObjectStateToServe1);

                            // *OK* Float fMaxDistance = new Float(0);
                            // *OK* puPropertyID = new Long(0);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetMaxDistance(fMaxDistance);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetMaxDistance(fMaxDistance, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetMaxDistance(3F);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetMaxDistance(fMaxDistance);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetMaxDistance(2F, 9L, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetMaxDistance(fMaxDistance, puPropertyID, uObjectStateToServe1);

                            // *OK* Float fHalfVolumeDistance = new Float(0);
                            // *OK* puPropertyID = new Long(0);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetHalfVolumeDistance(fHalfVolumeDistance);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetHalfVolumeDistance(fHalfVolumeDistance, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetHalfVolumeDistance(3F);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetHalfVolumeDistance(fHalfVolumeDistance);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetHalfVolumeDistance(2F, 9L, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetHalfVolumeDistance(fHalfVolumeDistance, puPropertyID, uObjectStateToServe1);

                            // *OK* Float fHalfOuterAngle = new Float(0);
                            // *OK* puPropertyID = new Long(0);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetHalfOuterAngle(fHalfOuterAngle);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetHalfOuterAngle(fHalfOuterAngle, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetHalfOuterAngle(3F);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetHalfOuterAngle(fHalfOuterAngle);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetHalfOuterAngle(2F, 9L, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetHalfOuterAngle(fHalfOuterAngle, puPropertyID, uObjectStateToServe1);

                            // *OK* Float fHalfInnerAngle = new Float(0);
                            // *OK* puPropertyID = new Long(0);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetHalfInnerAngle(fHalfInnerAngle);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetHalfInnerAngle(fHalfInnerAngle, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetHalfInnerAngle(3F);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetHalfInnerAngle(fHalfInnerAngle);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetHalfInnerAngle(2F, 9L, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetHalfInnerAngle(fHalfInnerAngle, puPropertyID, uObjectStateToServe1);

                            // *OK* Float fOuterAngleVolume = new Float(0);
                            // *OK* puPropertyID = new Long(0);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetOuterAngleVolume(fOuterAngleVolume);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetOuterAngleVolume(fOuterAngleVolume, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetOuterAngleVolume(3F);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetOuterAngleVolume(fOuterAngleVolume);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetOuterAngleVolume(2F, 9L, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetOuterAngleVolume(fOuterAngleVolume, puPropertyID, uObjectStateToServe1);

                            // *OK* SMcFVector3D Velocity = new SMcFVector3D();
                            // *OK* puPropertyID = new Long(0);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetVelocity(Velocity);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetVelocity(Velocity, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetVelocity(new SMcFVector3D(1,2,3));
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetVelocity(Velocity);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetVelocity(new SMcFVector3D(1,1,1), 11L, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetVelocity(Velocity, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetVelocity(Velocity, puPropertyID, uObjectStateToServe1);

                            // *OK* Float fPitch = new Float(0);
                            // *OK* puPropertyID = new Long(0);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetPitch(fPitch);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetPitch(fPitch, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetPitch(3F);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetPitch(fPitch);
                            // *OK* ((IMcSoundItem) objectSchemeItem).SetPitch(2F, 9L, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetPitch(fPitch, puPropertyID, uObjectStateToServe1);
                            // *OK* ((IMcSoundItem) objectSchemeItem).GetPitch(fPitch, puPropertyID, uObjectStateToServe1);
*/

                    }


                }catch (Exception e) {
                    e.printStackTrace();
                }
            }});
    }

    private void openTextureFrag() {
        CreateTextureTabsFragment createTextureTabsFragment;
        createTextureTabsFragment = CreateTextureTabsFragment.newInstance();
        createTextureTabsFragment.setCurrentTexture(null);

        FragmentTransaction transaction = getFragmentManager().beginTransaction();
        transaction.hide(this);
        transaction.addToBackStack("hide" + this.getClass().getSimpleName());
        transaction.add(R.id.fragment_container, createTextureTabsFragment, Consts.TextureFragmentsTags.TEXTURE_FROM_CREATE_NEW_PICTURE);
        transaction.addToBackStack(Consts.TextureFragmentsTags.TEXTURE_FROM_CREATE_NEW_PICTURE);
        transaction.commit();
    }

    public void RemoveObjectInitMode() {
        if (m_LastObjInitMode != null) {
            mView.queueEvent(new Runnable() {
                @Override
                public void run() {
                    try {
                        m_LastObjInitMode.Remove();
                    } catch (MapCoreException e) {
                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcObject.Remove");
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            });
        }
    }


    //region draw actions
    public void drawLine() {

        if (mView != null) {
            LoadScheme(Consts.SCHEMES_FILES_LINE);
        }
    }

    public void drawRect() {
        // checkNewWrappedSymbolicItem();
        // checkNewWrappedPhysicalItem();
        if (mView != null)
            LoadScheme(Consts.SCHEMES_FILES_RECTANGLE);
    }

    public void drawEllipse() {
        if (mView != null)
            LoadScheme(Consts.SCHEMES_FILES_ELLIPSE);
    }

    public void drawLineExpansion() {
        if (mView != null)
            LoadScheme(Consts.SCHEMES_FILES_LINE_EXPANTION);
    }

    public void drawPicture() {
        if (mView != null) {
            openTextureFrag();
        }
    }

    public void drawPictureAfterCreateTexture() {
        if (mView != null) {
            LoadScheme(Consts.SCHEMES_FILES_PICTURE);
        }
    }

    public void drawMeshAfterCreateMesh() {
        if (mView != null) {
            LoadScheme(Consts.SCHEMES_FILES_MESH);
        }
    }

    public void drawPolygon() {
        if (mView != null)
            LoadScheme(Consts.SCHEMES_FILES_POLYGON);
    }

    public void drawArc() {
        if (mView != null)
            LoadScheme(Consts.SCHEMES_FILES_ARC);
    }

    public void drawArrow() {
        if (mView != null)
            LoadScheme(Consts.SCHEMES_FILES_ARROW);
    }

    public void drawText() {
        if (mView != null)
            openTextFrag();
        //LoadScheme(mRootView, Consts.SCHEMES_FILES_TEXT);
    }

    public void drawMesh() {
        if (mView != null)
            openMeshFrag();
    }

    private void openTextFrag() {
        ObjectPropertiesBase.Text.getInstance().addObserver(this);
        CreateTextTabsFragment textFragment = CreateTextTabsFragment.newInstance();
        ObjectPropertiesBase.Text.getInstance().mPreviousFragmentText = ObjectPropertiesBase.Text.PreviousFragmentText.CreateNewText;

        FragmentTransaction transaction = getFragmentManager().beginTransaction();
        transaction.hide(this);
        transaction.addToBackStack("hide" + this.getClass().getSimpleName());
        transaction.add(R.id.fragment_container, textFragment, CreateTextTabsFragment.class.getSimpleName());
        transaction.addToBackStack(CreateTextTabsFragment.class.getSimpleName());
        transaction.commit();
    }

    private void openMeshFrag() {
        CreateMeshTabsFragment createMeshTabsFragment;
        createMeshTabsFragment = CreateMeshTabsFragment.newInstance();
        createMeshTabsFragment.setCurrentMesh(null);

        FragmentTransaction transaction = getFragmentManager().beginTransaction();
        transaction.hide(this);
        transaction.addToBackStack("hide" + this.getClass().getSimpleName());
        transaction.add(R.id.fragment_container, createMeshTabsFragment, Consts.MeshFragmentsTags.MESH_FROM_CREATE_NEW_MESH);
        transaction.addToBackStack(Consts.MeshFragmentsTags.MESH_FROM_CREATE_NEW_MESH);
        transaction.commit();
    }

    public void zoomIn() {
        if (mView != null) {
            mView.queueEvent(new Runnable() {
                @Override
                public void run() {
                    try {
                        IMcMapViewport currVP = Manager_AMCTMapForm.getInstance().getCurViewport();
                        IMcMapCamera.EMapType mapType = currVP.GetMapType();
                        if (mapType == IMcMapCamera.EMapType.EMT_2D) {
                            float fScale = currVP.GetCameraScale();
                            currVP.SetCameraScale(fScale / 2);
                        } else // 3D
                        {
                            float fov = Manager_AMCTMapForm.getInstance().getCurViewport().GetCameraFieldOfView();
                            Manager_AMCTMapForm.getInstance().getCurViewport().SetCameraFieldOfView(fov - 5);
                        }
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                    updateScale();
                }
            });
        }

    }

    public void centerMap() {
        if (mView != null) {
            mView.SetCenterMap(getContext());
        }
    }

    public void dynamicZoom() {
        if (mView != null) {
            mView.dynamicZoom();
        }
    }

    public void distanceDirectionMeasure() {
        if (mView != null) {
            mView.distanceDirectionMeasure();
        }
    }

    public void navigateMap() {
        String className = MovementFragment.class.getName();
        DialogFragment dialog = (DialogFragment) DialogFragment.instantiate(getActivity(), className);
        dialog.setTargetFragment(MapFragment.this, 1);
        dialog.show(getFragmentManager(), className);
    }

    public void zoomOut() {
        if (mView != null) {
            mView.queueEvent(new Runnable() {
                @Override
                public void run() {
                    try {
                        IMcMapViewport currVP = Manager_AMCTMapForm.getInstance().getCurViewport();
                        IMcMapCamera.EMapType mapType = currVP.GetMapType();
                        if (mapType == IMcMapCamera.EMapType.EMT_2D) {
                            float fScale = currVP.GetCameraScale();
                            currVP.SetCameraScale(fScale * 2);
                        } else // 3D
                        {
                            float fov = Manager_AMCTMapForm.getInstance().getCurViewport().GetCameraFieldOfView();
                            Manager_AMCTMapForm.getInstance().getCurViewport().SetCameraFieldOfView(fov + 5);
                        }
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                    updateScale();
                }

            });
        }
    }

    public void heightLines()
    {
        if (mView != null) {
            mView.queueEvent(new Runnable() {
                @Override
                public void run() {
                    try {
                        IMcMapViewport currVP = Manager_AMCTMapForm.getInstance().getCurViewport();
                       /* boolean visible = currVP.GetHeightLinesVisibility();
                        currVP.SetHeightLinesVisibility(!visible);
*/
                        IMcMapHeightLines mapHeightLines = currVP.GetHeightLines();
                        if (mapHeightLines == null) {
                            IMcMapHeightLines.SScaleStep[] heightLinesScaleStep;
                            heightLinesScaleStep = new IMcMapHeightLines.SScaleStep[4];
                            SMcBColor[] scaleColorsFirst = new SMcBColor[3];

                            scaleColorsFirst[0] = new SMcBColor(255, 0, 0, 255);
                            scaleColorsFirst[1] = new SMcBColor(0, 255, 0, 255);
                            scaleColorsFirst[2] = new SMcBColor(0, 0, 255, 255);

                            SMcBColor[] scaleColorsSecond = new SMcBColor[3];

                            scaleColorsSecond[0] = new SMcBColor(255, 255, 0, 255);
                            scaleColorsSecond[1] = new SMcBColor(0, 255, 255, 255);
                            scaleColorsSecond[2] = new SMcBColor(255, 0, 255, 255);

                            heightLinesScaleStep[0] = new IMcMapHeightLines.SScaleStep();
                            heightLinesScaleStep[0].fMaxScale = 10;
                            heightLinesScaleStep[0].fLineHeightGap = 10;
                            heightLinesScaleStep[0].aColors = scaleColorsFirst;

                            heightLinesScaleStep[1] = new IMcMapHeightLines.SScaleStep();
                            heightLinesScaleStep[1].fMaxScale = 25;
                            heightLinesScaleStep[1].fLineHeightGap = 25;
                            heightLinesScaleStep[1].aColors = scaleColorsSecond;

                            heightLinesScaleStep[2] = new IMcMapHeightLines.SScaleStep();
                            heightLinesScaleStep[2].fMaxScale = 50;
                            heightLinesScaleStep[2].fLineHeightGap = 50;
                            heightLinesScaleStep[2].aColors = scaleColorsFirst;

                            heightLinesScaleStep[3] = new IMcMapHeightLines.SScaleStep();
                            heightLinesScaleStep[3].fMaxScale = 300;
                            heightLinesScaleStep[3].fLineHeightGap = 100;
                            heightLinesScaleStep[3].aColors = scaleColorsSecond;

                            try {
                                mapHeightLines = McMapHeightLines.Static.Create(heightLinesScaleStep, 1);
                                Manager_MCMapHeightLines.getInstance().AddNewHeightLines(mapHeightLines);

                                currVP.SetHeightLines(mapHeightLines);
                                currVP.SetHeightLinesVisibility(true);

                            } catch (MapCoreException e) {
                                AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "Get/SetHeightLinesVisibility");
                            }
                        }
                        else {
                            currVP.SetHeightLines(null);
                            mapHeightLines.Release();
                        }
                    } catch (MapCoreException e) {
                        AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "Get/SetHeightLinesVisibility");
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }

            });
        }
    }

    //endregion

    private void initMenuLayoutIdForGroupName(LayoutInflater inflater) {
        mLayoutsForMenuGroups = new HashMap<>();
        mLayoutsForMenuGroups.put(MenuDataProvider.MAP_GROUP, inflater.inflate(R.layout.map_fragment_map_menu_items, null));
        mLayoutsForMenuGroups.put(MenuDataProvider.OBJECTS_GROUP, inflater.inflate(R.layout.map_fragment_object_menu_items, null));
        mLayoutsForMenuGroups.put(MenuDataProvider.EDIT_GROUP, inflater.inflate(R.layout.map_fragment_edit_menu_items, null));
        mLayoutsForMenuGroups.put(MenuDataProvider.ZOOMING_GROUP, inflater.inflate(R.layout.map_fragment_zooming_menu_items, null));
        mLayoutsForMenuGroups.put(MenuDataProvider.OBJECT_PROPERTIES_GROUP, inflater.inflate(R.layout.map_fragment_object_properties_menu_items, null));
        mLayoutsForMenuGroups.put(MenuDataProvider.MAP_GRID_GROUP, inflater.inflate(R.layout.map_fragment_map_grid_menu_items, null));
        mLayoutsForMenuGroups.put(MenuDataProvider.SCAN_GROUP, inflater.inflate(R.layout.map_fragment_scan_menu_items, null));
        mLayoutsForMenuGroups.put(MenuDataProvider.MAP_TILES_GROUP, inflater.inflate(R.layout.map_fragment_map_tiles_menu_items, null));
    }

    public View getMenuLayoutFromGroupName(String name) {
        return mLayoutsForMenuGroups.get(name);
    }

    public void addMenuBttnsToFragment(String menuGroupName) {
        LinearLayout menuLayout = (LinearLayout) mRootView.findViewById(R.id.fragment_amctmap_menu_container);
        menuLayout.removeAllViews();
        View child = getMenuLayoutFromGroupName(menuGroupName);
        menuLayout.addView(child);
        initButtons(child, menuGroupName);
    }

    private void initButtons(View inflaterView, String menuGroupName) {
        if (mView != null) {
            switch (menuGroupName) {
                case MenuDataProvider.MAP_GROUP: {

                    ((ImageView) inflaterView.findViewById(R.id.create_map_with_several_layers)).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(View v) {
                            Intent createVpIntent = new Intent(getActivity(), com.elbit.mapcore.mcandroidtester.ui.map.activities.ViewPortWithSeveralLayersActivity.class);
                            getActivity().startActivity(createVpIntent);
                        }
                    });
                    ((ImageView) inflaterView.findViewById(R.id.create_map_manually)).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(View v) {
                            Intent CreateMapIntent = new Intent(getActivity(), com.elbit.mapcore.mcandroidtester.ui.map.activities.MapTabsActivity.class);
                            getActivity().startActivity(CreateMapIntent);
                        }
                    });
                    ((ImageView) inflaterView.findViewById(R.id.create_device)).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(View v) {
                            Intent openDeviceTabsIntent = new Intent(getActivity(), com.elbit.mapcore.mcandroidtester.ui.device.activities.DeviceTabsActivity.class);
                            getActivity().startActivity(openDeviceTabsIntent);
                        }
                    });
                    ((TextView) inflaterView.findViewById(R.id.create_section_map)).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(View v) {
                            OpenSectionMap();
                        }
                    });
                }
                break;
                case MenuDataProvider.ZOOMING_GROUP: {
                    ((ImageView) inflaterView.findViewById(R.id.navigate_map)).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(View v) {
                            navigateMap();
                        }
                    });
                    ((ImageView) inflaterView.findViewById(R.id.zoom_in)).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(View v) {
                            zoomIn();
                        }
                    });
                    ((ImageView) inflaterView.findViewById(R.id.zoom_out)).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(View v) {
                            zoomOut();
                        }
                    });
                    ((ImageView) inflaterView.findViewById(R.id.center_map)).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(View v) {
                            centerMap();
                        }
                    });
                    ((ImageView) inflaterView.findViewById(R.id.dynamic_zoom)).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(View v) {
                            dynamicZoom();
                        }
                    });
                    ((ImageView) inflaterView.findViewById(R.id.distance_direction_measure)).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(View v) {
                            distanceDirectionMeasure();
                        }
                    });
                }
                break;
                case MenuDataProvider.OBJECTS_GROUP:
                    ((ImageView) inflaterView.findViewById(R.id.draw_line)).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(final View v) {
                            drawLine();
                        }
                    });
                    ((ImageView) inflaterView.findViewById(R.id.draw_rect)).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(final View v) {
                            drawRect();
                        }
                    });
                    ((ImageView) inflaterView.findViewById(R.id.draw_ellipse)).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(final View v) {
                            drawEllipse();
                        }
                    });
                    ((ImageView) inflaterView.findViewById(R.id.draw_lineExpansion)).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(final View v) {
                            drawLineExpansion();
                        }
                    });
                    ((ImageView) inflaterView.findViewById(R.id.draw_picture)).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(final View v) {
                            drawPicture();
                        }
                    });
                    ((ImageView) inflaterView.findViewById(R.id.draw_poligon)).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(final View v) {
                            drawPolygon();
                        }
                    });
                    ((ImageView) inflaterView.findViewById(R.id.draw_arc)).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(final View v) {
                            drawArc();
                        }
                    });
                    ((ImageView) inflaterView.findViewById(R.id.draw_arrow)).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(final View v) {
                            drawArrow();
                        }
                    });
                    ((ImageView) inflaterView.findViewById(R.id.draw_text)).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(final View v) {
                            drawText();
                        }
                    });
                    ((ImageView) inflaterView.findViewById(R.id.draw_mesh)).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(final View v) {
                            drawMesh();
                        }
                    });
                    break;
                case MenuDataProvider.EDIT_GROUP:
                    ((ImageView) inflaterView.findViewById(R.id.initMode)).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(final View v) {
                            startInitMode();
                        }
                    });
                    ((ImageView) inflaterView.findViewById(R.id.editMode)).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(final View v) {
                            startEditMode();
                        }
                    });
                    ((ImageView) inflaterView.findViewById(R.id.editModeProperties)).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(final View v) {
                            OpenEditModeProperties();
                        }
                    });
                    break;
                case MenuDataProvider.OBJECT_PROPERTIES_GROUP:
                    ((ImageView) inflaterView.findViewById(R.id.objectProperties)).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(final View v) {
                            OpenObjectProperties();
                        }
                    });
                    break;
                case MenuDataProvider.MAP_GRID_GROUP:
                    ((TextView) inflaterView.findViewById(R.id.height_lines)).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(final View v) {
                            heightLines();
                        }
                    });
                    ((TextView) inflaterView.findViewById(R.id.mgns_grid)).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(final View v) {
                            AMCTGrid.createGrid((MapsContainerActivity) getActivity(), Consts.GridType.MGRS_GRID);
                        }
                    });
                    ((TextView) inflaterView.findViewById(R.id.utm_grid)).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(final View v) {
                            AMCTGrid.createGrid((MapsContainerActivity) getActivity(), Consts.GridType.UTM_GRID);
                        }
                    });
                    ((TextView) inflaterView.findViewById(R.id.geo_grid)).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(final View v) {
                            AMCTGrid.createGrid((MapsContainerActivity) getActivity(), Consts.GridType.GEO_GRID);
                        }
                    });
                    ((TextView) inflaterView.findViewById(R.id.nzmg_grid)).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(final View v) {
                            AMCTGrid.createGrid((MapsContainerActivity) getActivity(), Consts.GridType.NZMG_GRID);
                        }
                    });
                    break;
                case MenuDataProvider.SCAN_GROUP:
                    ((ImageView) inflaterView.findViewById(R.id.scan)).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(final View v) {
                            openScan();
                        }
                    });
                    break;
                case MenuDataProvider.MAP_TILES_GROUP:
                    inflaterView.findViewById(R.id.map_tiles_d).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(final View v) {
                            AMCTMapTiles.SetDebugOption(AMCTMapTiles.MapTilesKey.D);
                        }
                    });
                    inflaterView.findViewById(R.id.map_tiles_b).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(final View v) {
                            AMCTMapTiles.SetDebugOption(AMCTMapTiles.MapTilesKey.B);
                        }
                    });
                    inflaterView.findViewById(R.id.map_tiles_w).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(final View v) {
                            AMCTMapTiles.SetDebugOption(AMCTMapTiles.MapTilesKey.W);
                        }
                    });
                    inflaterView.findViewById(R.id.map_tiles_s).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(final View v) {
                            AMCTMapTiles.SetDebugOption(AMCTMapTiles.MapTilesKey.S);
                        }
                    });
                    inflaterView.findViewById(R.id.map_tiles_stat).setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(final View v) {
                            AMCTMapTiles.SetDebugOption(AMCTMapTiles.MapTilesKey.Stat);
                        }
                    });
            }
        }
    }

    public void startEditMode() {
        OpenInitEditModeFragment(EType.Edit_Mode.ordinal());
    }

    public void startScanMode(IEditModeFragmentCallback.EType type, IMcObject scanObject) {
        OpenInitEditModeFragment(type.ordinal(), scanObject);
    }

    public void startInitMode() {
        OpenInitEditModeFragment(EType.Init_Mode.ordinal());
    }

    private void OpenInitEditModeFragment(final int eTypeValue) {
        OpenInitEditModeFragment(eTypeValue, null);
    }

    private void OpenInitEditModeFragment(final int eTypeValue, IMcObject scanObject) {
        String className = EditModeFragment.class.getName();
        DialogFragment dialog = (DialogFragment) DialogFragment.instantiate(getActivity(), className);
        Bundle args = new Bundle();
        args.putInt("type", eTypeValue);
        if (scanObject != null)
            ((FragmentWithObject) dialog).setObject(scanObject);

        dialog.setArguments(args);
        dialog.setTargetFragment(MapFragment.this, 1);
        dialog.show(getFragmentManager(), className);
    }

    public void OpenObjectProperties() {
        ObjectPropertiesBase.Text.getInstance().mPreviousFragmentText = ObjectPropertiesBase.Text.PreviousFragmentText.ObjectProperties;
        ObjectPropertiesFragment objectPropertiesFragment = ObjectPropertiesFragment.newInstance("", "");
        FragmentTransaction transaction = getFragmentManager().beginTransaction();
        transaction.hide(this);
        transaction.addToBackStack("hide" + this.getClass().getSimpleName());
        transaction.add(R.id.fragment_container, objectPropertiesFragment, objectPropertiesFragment.getClass().getSimpleName());
        transaction.addToBackStack(objectPropertiesFragment.getClass().getSimpleName());
        transaction.commit();
    }

    public void OpenSectionMap() {
        SectionMapFragment sectionMapFragment = SectionMapFragment.newInstance();
        FragmentTransaction transaction = getFragmentManager().beginTransaction();
        transaction.hide(this);
        transaction.addToBackStack("hide" + this.getClass().getSimpleName());
        transaction.add(R.id.fragment_container, sectionMapFragment, sectionMapFragment.getClass().getSimpleName());
        transaction.addToBackStack(sectionMapFragment.getClass().getSimpleName());
        transaction.commit();
    }

    public void OpenEditModeProperties() {
        EditModePropertiesTabsFragment editModePropertiesTabsFragment = EditModePropertiesTabsFragment.newInstance();
        FragmentTransaction transaction = getFragmentManager().beginTransaction();
        transaction.hide(this);
        transaction.addToBackStack("hide" + this.getClass().getSimpleName());
        transaction.add(R.id.fragment_container, editModePropertiesTabsFragment, editModePropertiesTabsFragment.getClass().getSimpleName());
        transaction.addToBackStack(editModePropertiesTabsFragment.getClass().getSimpleName());
        transaction.commit();
    }


    public void openScan() {
        ScanFragment scanFragment = ScanFragment.newInstance();
        FragmentTransaction transaction = getFragmentManager().beginTransaction();
        transaction.hide(this);
        transaction.addToBackStack("hide" + this.getClass().getSimpleName());
        transaction.add(R.id.fragment_container, scanFragment, ScanFragment.class.getSimpleName());
        transaction.addToBackStack(scanFragment.getClass().getSimpleName());
        transaction.commit();
    }

    // TODO: Rename method, update argument and hook method into UI event
    public void onButtonPressed(Uri uri) {
        if (mListener != null) {
            mListener.onFragmentInteraction(uri);
        }
    }

    @Override
    public void setUserVisibleHint(boolean visible) {
        super.setUserVisibleHint(visible);
        if (visible && isResumed()) {
            //Only manually call onResume if fragment is already visible
            //Otherwise allow natural fragment lifecycle to call onResume
            onResume();

        }
    }

    @Override
    public void onAttach(Context context) {
        super.onAttach(context);
        if (context instanceof OnFragmentInteractionListener) {
            mListener = (OnFragmentInteractionListener) context;
        } else {
            throw new RuntimeException(context.toString()
                    + " must implement OnFragmentInteractionListener");
        }
        if (context instanceof MapsContainerActivity) {
            ((MapsContainerActivity) context).mMapFragment = MapFragment.this;
            ((MapsContainerActivity) context).mCurFragmentTag = MapFragment.class.getSimpleName();

        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    @Override
    public void callBackEditMode(final EType type, final IMcObject selectObject, final IMcObjectSchemeItem selectedItem) {
        mView.queueEvent(new Runnable() {
            @Override
            public void run() {
                try {
                    if (type == EType.Init_Mode) {
                        mView.StartInitMode(selectObject, selectedItem);
                    } else if (type == EType.Edit_Mode) {
                        mView.StartEditMode(selectObject, selectedItem);
                    } else if (type == EType.Scan_From_Init)// scan
                        scanType = ScanType.InitMode;
                    else if (type == EType.Scan_From_Edit)
                        scanType = ScanType.EditMode;
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    @Override
    public void callBackScan() {
        scanType = ScanType.EditMode;
    }

    /**
     * This interface must be implemented by activities that contain this
     * fragment to allow an interaction in this fragment to be communicated
     * to the activity and potentially other fragments contained in that
     * activity.
     * <p/>
     * See the Android Training lesson <a href=
     * "http://developer.android.com/training/basics/fragments/communicating.html"
     * >Communicating with Other Fragments</a> for more information.
     */
    public interface OnFragmentInteractionListener {
        // TODO: Update argument type and name
        void onFragmentInteraction(Uri uri);
    }

    public interface OnLocationPointUpdatedListener {
        void onLocationPointUpdated(float x, float y);
    }

    @Override
    public void onResume() {
        super.onResume();
        if (mView != null)
            mView.onResume();
    }

    @Override
    public void onHiddenChanged(boolean hidden) {
        super.onHiddenChanged(hidden);
        if (!hidden) {
            setTitle();
            ((MapsContainerActivity) getActivity()).mCurFragmentTag = MapFragment.class.getSimpleName();
            if (mView != null)
                mView.setRenderMode(GLSurfaceView.RENDERMODE_CONTINUOUSLY);
        }
    }

    public void setTitle() {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    final String version = IMcMapDevice.Static.GetVersion();
                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            getActivity().setTitle("Maps Container D(" + version +")" );
                        }
                    });
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "GetVersion");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    @Override
    public void onPause() {
        if (mView != null)
            mView.onPause();
        super.onPause();
    }

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        mView = (AMcGLSurfaceView) mRootView.findViewById(R.id.amctmap_surface);
        if(mView!= null)
            mView.CheckIsNeedToOpenEditModeActionMode();
//        if(mLastMenuGroupName != null && !mLastMenuGroupName.isEmpty())
//            addMenuBttnsToFragment(mLastMenuGroupName);
    }

    public void TouchMap(final SMcPoint touchPositionViewport,final int eventAction )
    {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                handleMapClick(touchPositionViewport.x, touchPositionViewport.y);
                if (eventAction == MotionEvent.ACTION_DOWN)
                    ((MapsContainerActivity) getActivity()).onLocationPointUpdated(touchPositionViewport.x, touchPositionViewport.y);
            }
        });
    }
}

