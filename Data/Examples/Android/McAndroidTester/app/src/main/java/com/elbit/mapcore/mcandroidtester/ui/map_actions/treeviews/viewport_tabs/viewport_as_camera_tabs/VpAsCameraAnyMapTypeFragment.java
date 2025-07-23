package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.viewport_tabs.viewport_as_camera_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentTransaction;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;

import com.elbit.mapcore.General.ObjectRef;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.viewport_tabs.ViewPortDetailsTabsFragment;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SampleLocationPointsBttn;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SpinnerWithLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.ThreeDOrientation;
import com.elbit.mapcore.mcandroidtester.utils.customviews.ThreeDVector;
import com.elbit.mapcore.mcandroidtester.utils.customviews.TwoDFVector;

import com.elbit.mapcore.Enums.EMcPointCoordSystem;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Calculations.IMcSpatialQueries;
import com.elbit.mapcore.Interfaces.Map.IMcMapCamera;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Structs.SMcBox;
import com.elbit.mapcore.Structs.SMcRect;
import com.elbit.mapcore.Structs.SMcVector3D;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link VpAsCameraAnyMapTypeFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link VpAsCameraAnyMapTypeFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class VpAsCameraAnyMapTypeFragment extends Fragment implements FragmentWithObject {

    private OnFragmentInteractionListener mListener;
    private IMcMapCamera mCurrentObject;
    private View mRootView;
    private EditText mMapTypeET;
    private ThreeDVector mCameraWorldVisibleAreaMinPoint3D;
    private ThreeDVector mCameraWorldVisibleAreaMaxPoint3D;
    private ThreeDVector mCameraScaleWorldPoint3D;
    private ThreeDVector mCameraPosition3D;
    private ThreeDVector mCameraUpVector3D;
    private ThreeDVector mMoveRelativeToOrientation3D;
    private ThreeDVector mRotateCameraAroundWorldPivot3D;
    private ThreeDOrientation mRotateCameraAroundWorldDelta3D;
    private ThreeDOrientation mCameraOrientation3D;
    private TwoDFVector mTLScreenVisibleArea2D;
    private TwoDFVector mBRScreenVisibleArea2D;
    private TwoDFVector mCameraCenterOffset2D;
    private SpinnerWithLabel mScreenVisibleAreaOperationSWL;
    private CheckBox mCameraPositionIsRelativeCB;
    private CheckBox mCameraUpVectorIsRelativeCB;
    private CheckBox mCameraOrientationIsRelativeCb;
    private CheckBox mRotateCameraAroundWorldPointRelativeToOrientationCb;

    private CheckBox mXYDirectionOnlyCb;
    private NumericEditTextLabel mCameraScaleEt;
    private NumericEditTextLabel mRectangleYawET;
    private NumericEditTextLabel mScreenMarginET;
    private SampleLocationPointsBttn mCameraPositionSLPB;


    public VpAsCameraAnyMapTypeFragment() {
        // Required empty public constructor
    }

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment VpAsCameraAnyMapTypeFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static VpAsCameraAnyMapTypeFragment newInstance() {
        VpAsCameraAnyMapTypeFragment fragment = new VpAsCameraAnyMapTypeFragment();
       
        return fragment;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        Funcs.SetObjectFromBundle(savedInstanceState, this );
        mRootView = inflater.inflate(R.layout.fragment_vp_as_camera_any_map_type, container, false);
        initViews();
        return mRootView;
    }

    private void initViews() {
        initSampleLocationPointBttn();
        initEditTexts();
        init3DVectors();
        init2DVectors();
        initCheckBoxes();
        initSpinners();
        initButtons();

    }

    private void initSampleLocationPointBttn() {
        mCameraPositionSLPB=(SampleLocationPointsBttn)mRootView.findViewById(R.id.camera_any_map_type_camera_position_sample_location_bttn);
        mCameraPositionSLPB.initBttn(ViewPortDetailsTabsFragment.class.getSimpleName(),true,Double.MAX_VALUE, IMcSpatialQueries.EQueryPrecision.EQP_DEFAULT, EMcPointCoordSystem.EPCS_WORLD,true,R.id.any_map_type_camera_position,-1);
    }

    private void init2DVectors() {
        mBRScreenVisibleArea2D = (TwoDFVector) mRootView.findViewById(R.id.any_map_type_bottom_right);
        mTLScreenVisibleArea2D = (TwoDFVector) mRootView.findViewById(R.id.any_map_type_top_left);
        mCameraCenterOffset2D = (TwoDFVector) mRootView.findViewById(R.id.any_map_type_camera_center_offset);
        try {
            mCameraCenterOffset2D.setVector2D(mCurrentObject.GetCameraCenterOffset());
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetCameraCenterOffset");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initSpinners() {
        mScreenVisibleAreaOperationSWL = (SpinnerWithLabel) mRootView.findViewById(R.id.any_map_type_screen_visible_area_operation);
        ArrayAdapter<IMcMapCamera.ESetVisibleArea3DOperation> screenVisibleAreaOperationAdapter = new ArrayAdapter<>(getContext(), android.R.layout.simple_spinner_item, IMcMapViewport.ESetVisibleArea3DOperation.values());
        mScreenVisibleAreaOperationSWL.setAdapter(screenVisibleAreaOperationAdapter);
        mScreenVisibleAreaOperationSWL.setSelection(screenVisibleAreaOperationAdapter.getPosition(IMcMapCamera.ESetVisibleArea3DOperation.ESVAO_ROTATE_AND_MOVE));

    }

    private void initButtons() {
        initSaveCameraPositionBttn();
        initSaveCameraUpVectorBttn();
        initSaveCameraOrientationBttn();
        initSaveMoveRelativeToOrientationBttn();
        initSaveCameraScaleBttn();
        initSaveScreenVisibleAreaBttn();
        initSaveCameraWorldVisibleAreaBttn();
        initSaveCameraCenterOffsetBttn();
        initSaveRotateCameraAroundWorldPointBttn();
        initGetCameraScaleBttn();
    }

    private void initSaveRotateCameraAroundWorldPointBttn() {
        Button saveRotateCameraAroundWorldPointBttn = (Button) mRootView.findViewById(R.id.any_map_type_rotate_camera_around_world_relative_to_orientation_OK_bttn);
        saveRotateCameraAroundWorldPointBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveRotateCameraAroundWorld();
                initViews();
            }});
    }

    private void initSaveCameraCenterOffsetBttn() {
        Button saveCameraCenterOffsetBttn = (Button) mRootView.findViewById(R.id.any_map_type_camera_center_offset_OK_bttn);
        saveCameraCenterOffsetBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveCameraCenterOffset();
                initViews();

            }});
    }

    private void initSaveCameraWorldVisibleAreaBttn() {
        Button saveScreenVisibleAreaBttn = (Button) mRootView.findViewById(R.id.any_map_type_camera_world_visible_area_OK_bttn);
        saveScreenVisibleAreaBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveCameraWorldVisibleArea();
                initViews();

            }});
    }


    private void initSaveScreenVisibleAreaBttn() {
        Button saveScreenVisibleAreaBttn = (Button) mRootView.findViewById(R.id.any_map_type_screen_visible_area_OK_bttn);
        saveScreenVisibleAreaBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveScreenVisibleArea();
                initViews();

            }
        });
    }

    private void initSaveCameraScaleBttn() {
        Button saveCameraScaleBttn = (Button) mRootView.findViewById(R.id.any_map_type_in_3d_enter_world_point_OK_bttn);
        saveCameraScaleBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveCameraScale();
                initViews();

            }
        });
    }

    private void initSaveMoveRelativeToOrientationBttn() {
        Button saveMoveRelativeToOrientationBttn = (Button) mRootView.findViewById(R.id.any_map_type_move_relative_to_orientation_ok_bttn);
        saveMoveRelativeToOrientationBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveMoveRelativeToOrientation();
                initViews();

            }
        });
    }

    private void initSaveCameraOrientationBttn() {
        Button saveCameraOrientationBttn = (Button) mRootView.findViewById(R.id.any_map_type_camera_up_vector_ok_bttn);
        saveCameraOrientationBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveCameraOrientation();
                initViews();

            }
        });
    }

    private void initSaveCameraUpVectorBttn() {
        Button saveCameraUpVectorBttn = (Button) mRootView.findViewById(R.id.any_map_type_camera_up_vector_ok_bttn);
        saveCameraUpVectorBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveCameraUpVector();
                initViews();

            }
        });
    }

    private void initSaveCameraPositionBttn() {
        Button saveCameraPositionBttn = (Button) mRootView.findViewById(R.id.any_map_type_camera_position_ok_bttn);
        saveCameraPositionBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveCameraPosition();
                initViews();
            }
        });
    }

    private void initGetCameraScaleBttn() {
        if (mCameraScaleWorldPoint3D.getmX() != 0 && mCameraScaleWorldPoint3D.getmY() != 0) {
            SMcVector3D scaleWorldPoint = mCameraScaleWorldPoint3D.getVector3D();
            try {
                mCameraScaleEt.setFloat(mCurrentObject.GetCameraScale(scaleWorldPoint));
            } catch (MapCoreException e) {
                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetCameraScale");
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    private void initEditTexts() {
        initMapType();
        initCameraScale();
        initCameraWorldVisibleAreaETs();

    }

    private void initCameraWorldVisibleAreaETs() {
        mRectangleYawET = (NumericEditTextLabel) mRootView.findViewById(R.id.any_map_type_rectangle_Yaw);
        mScreenMarginET = (NumericEditTextLabel) mRootView.findViewById(R.id.any_map_type_screen_margin);
    }

    private void initCameraScale() {
        mCameraScaleEt = (NumericEditTextLabel) mRootView.findViewById(R.id.any_map_type_camera_scale_et);
        try {
            if (mCurrentObject.GetMapType() == IMcMapCamera.EMapType.EMT_2D)
                mCameraScaleEt.setFloat(mCurrentObject.GetCameraScale());
            if (mCurrentObject.GetMapType() == IMcMapCamera.EMapType.EMT_3D)
            {
                if(Funcs.getMapFragment()!= null) {
                    SMcVector3D worldPoint = Funcs.getMapFragment().getCurrentWorldPoint();
                    float cameraScale = mCurrentObject.GetCameraScale(worldPoint);
                    mCameraScaleEt.setFloat(cameraScale);
                }
            }


        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetMapType/GetCameraScale");
        } catch (Exception e) {
            e.printStackTrace();
        }

    }

    private void initCheckBoxes() {
        mCameraPositionIsRelativeCB = (CheckBox) mRootView.findViewById(R.id.any_map_type_camera_position_is_relative_cb);
        mCameraUpVectorIsRelativeCB = (CheckBox) mRootView.findViewById(R.id.any_map_type_camera_up_vector_is_relative_to_orientation);
        mCameraOrientationIsRelativeCb = (CheckBox) mRootView.findViewById(R.id.any_map_type_camera_orientation_is_relative_cb);
        mXYDirectionOnlyCb = (CheckBox) mRootView.findViewById(R.id.any_map_type_xy_direction_only_cb);
        mRotateCameraAroundWorldPointRelativeToOrientationCb=(CheckBox)mRootView.findViewById(R.id.any_map_type_rotate_camera_around_world_relative_to_orientation_cb);
    }

    private void saveRotateCameraAroundWorld() {
        try {
            mCurrentObject.RotateCameraAroundWorldPoint(mRotateCameraAroundWorldPivot3D.getVector3D(),mRotateCameraAroundWorldDelta3D.getmYaw(),mRotateCameraAroundWorldDelta3D.getmPitch(),mRotateCameraAroundWorldDelta3D.getmRoll(),mRotateCameraAroundWorldPointRelativeToOrientationCb.isChecked());
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "RotateCameraAroundWorldPoint");
        }catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void saveCameraCenterOffset() {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    mCurrentObject.SetCameraCenterOffset(mCameraCenterOffset2D.getVector2D());
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetCameraCenterOffset");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });

    }

    private void saveCameraWorldVisibleArea() {
        final float rectangleYaw = mRectangleYawET.getFloat();
        final SMcBox visibleArea = new SMcBox(mCameraWorldVisibleAreaMinPoint3D.getVector3D(), mCameraWorldVisibleAreaMaxPoint3D.getVector3D());
        final int screenMargin = mScreenMarginET.getInt();
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    mCurrentObject.SetCameraWorldVisibleArea(visibleArea, screenMargin, rectangleYaw);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetCameraWorldVisibleArea");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void saveScreenVisibleArea() {

        final SMcRect rectVisibleArea = new SMcRect(((Float) mTLScreenVisibleArea2D.getmX()).intValue(), ((Float) mTLScreenVisibleArea2D.getmY()).intValue(), ((Float) mBRScreenVisibleArea2D.getmX()).intValue(), ((Float) mBRScreenVisibleArea2D.getmY()).intValue());
        final IMcMapCamera.ESetVisibleArea3DOperation visibleArea3DOperation = (IMcMapCamera.ESetVisibleArea3DOperation) mScreenVisibleAreaOperationSWL.getSelectedItem();
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    mCurrentObject.SetCameraScreenVisibleArea(rectVisibleArea, visibleArea3DOperation);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetCameraScreenVisibleArea");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void saveCameraScale() {
        final float cameraScale = mCameraScaleEt.getFloat();
        final SMcVector3D cameraScaleWorldPoint3D = mCameraScaleWorldPoint3D.getVector3D();
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    if (mCurrentObject.GetMapType() == IMcMapCamera.EMapType.EMT_2D) {
                        mCurrentObject.SetCameraScale(cameraScale);
                    }
                    if (mCurrentObject.GetMapType() == IMcMapCamera.EMapType.EMT_3D) {
                        mCurrentObject.SetCameraScale(cameraScale, cameraScaleWorldPoint3D);
                    }
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetMapType/SetCameraScale");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });

    }

    private void refreshFragment() {
        FragmentTransaction ft = getFragmentManager().beginTransaction();
        ft.detach(this).attach(this).commit();
    }

    private void saveMoveRelativeToOrientation() {
        try {
            mCurrentObject.MoveCameraRelativeToOrientation(mMoveRelativeToOrientation3D.getVector3D(), mXYDirectionOnlyCb.isChecked());
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "MoveCameraRelativeToOrientation");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void saveCameraOrientation() {
        //todo change to function that get float, boolean when will be fixed in api
        final float yaw = mCameraOrientation3D.getmYaw();
        final float pitch = mCameraOrientation3D.getmPitch();
        final float roll = mCameraOrientation3D.getmRoll();
        final boolean isRelative = mCameraOrientationIsRelativeCb.isChecked();

        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    if (mCurrentObject.GetMapType() == IMcMapCamera.EMapType.EMT_2D)
                        mCurrentObject.SetCameraOrientation(yaw, pitch, roll, isRelative);
                    if (mCurrentObject.GetMapType() == IMcMapCamera.EMapType.EMT_3D)
                        mCurrentObject.SetCameraOrientation(yaw, pitch, roll, isRelative);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetCameraOrientation");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void saveCameraUpVector() {
        final SMcVector3D cameraUpVector = mCameraUpVector3D.getVector3D();
        final boolean isRelative = mCameraUpVectorIsRelativeCB.isChecked();
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    mCurrentObject.SetCameraUpVector(cameraUpVector, isRelative);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetCameraUpVector");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });

    }

    private void saveCameraPosition() {
        final SMcVector3D cameraPosition = mCameraPosition3D.getVector3D();
        final boolean isRelative = mCameraPositionIsRelativeCB.isChecked();
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    mCurrentObject.SetCameraPosition(cameraPosition, isRelative);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetCameraPosition");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void init3DVectors() {
        initCamerPosition3D();
        initCameraUpVector3D();
        initCameraOrientation3D();
        initMoveRelativeToOrientation3D();
        initCameraScaleWorldPoint3D();
        //todo uncomment when GetCameraWorldVisibleArea will be fixed in api
         initCameraWorldVisibleArea();
        initRotateCameraAroundWorld();
    }

    private void initRotateCameraAroundWorld() {
        mRotateCameraAroundWorldDelta3D = (ThreeDOrientation) mRootView.findViewById(R.id.any_map_type_rotate_camera_around_world_delta);
        mRotateCameraAroundWorldPivot3D = (ThreeDVector) mRootView.findViewById(R.id.any_map_type_rotate_camera_around_world_pivot_point);
    }

    private void initCameraWorldVisibleArea() {
        mCameraWorldVisibleAreaMinPoint3D = (ThreeDVector) mRootView.findViewById(R.id.any_map_type_camera_world_visible_area_min_point);
        mCameraWorldVisibleAreaMaxPoint3D = (ThreeDVector) mRootView.findViewById(R.id.any_map_type_camera_world_visible_area_max_point);
        try {
            if (mCurrentObject.GetMapType() == IMcMapCamera.EMapType.EMT_2D) {
                SMcBox worldVisibleArea = mCurrentObject.GetCameraWorldVisibleArea();
                mCameraWorldVisibleAreaMinPoint3D.setVector3D(worldVisibleArea.MinVertex);
                mCameraWorldVisibleAreaMaxPoint3D.setVector3D(worldVisibleArea.MaxVertex);
            }
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetMapType/GetCameraWorldVisibleArea");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initCameraScaleWorldPoint3D() {
        mCameraScaleWorldPoint3D = (ThreeDVector) mRootView.findViewById(R.id.any_map_type_in_3d_enter_world_point);
        //todo change to position on screen after create manager status bar
        try {
            mCameraScaleWorldPoint3D.setVector3D(mCurrentObject.GetCameraPosition());
            SMcVector3D scaleWorldPoint = mCameraScaleWorldPoint3D.getVector3D();
            mCameraScaleEt.setFloat(mCurrentObject.GetCameraScale(scaleWorldPoint));
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetCameraPosition/GetCameraScale");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initMoveRelativeToOrientation3D() {
        mMoveRelativeToOrientation3D = (ThreeDVector) mRootView.findViewById(R.id.any_map_type_move_relative_to_orientation);

    }

    private void initCameraOrientation3D() {
        ObjectRef<Float> yaw = new ObjectRef<>();
        ObjectRef<Float> pitch = new ObjectRef<>();
        ObjectRef<Float> roll = new ObjectRef<>();
        mCameraOrientation3D = (ThreeDOrientation) mRootView.findViewById(R.id.any_map_type_camera_orientation);
        try {
            if (mCurrentObject.GetMapType() == IMcMapCamera.EMapType.EMT_2D) {
                mCurrentObject.GetCameraOrientation(yaw);
                mCameraOrientation3D.setmYaw(yaw.getValue());
            }
            if (mCurrentObject.GetMapType() == IMcMapCamera.EMapType.EMT_3D) {
                mCurrentObject.GetCameraOrientation(yaw, pitch, roll);
                mCameraOrientation3D.setmYaw(yaw.getValue());
                mCameraOrientation3D.setmPitch(pitch.getValue());
                mCameraOrientation3D.setmRoll(roll.getValue());
            }
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetCameraOrientation");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initCameraUpVector3D() {
        mCameraUpVector3D = (ThreeDVector) mRootView.findViewById(R.id.any_map_type_camera_up_vector);
        try {
            mCameraUpVector3D.setVector3D(mCurrentObject.GetCameraUpVector());
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetCameraUpVector()");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initCamerPosition3D() {
        mCameraPosition3D = (ThreeDVector) mRootView.findViewById(R.id.any_map_type_camera_position);
        try {
            mCameraPosition3D.setVector3D(mCurrentObject.GetCameraPosition());
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetCameraPosition()");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initMapType() {
        mMapTypeET = (EditText) mRootView.findViewById(R.id.any_map_type_map_type);
        try {
            mMapTypeET.setText(mCurrentObject.GetMapType().toString());
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetMapType()");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    // TODO: Rename method, update argument and hook method into UI event
    public void onButtonPressed(Uri uri) {
        if (mListener != null) {
            mListener.onFragmentInteraction(uri);
        }
    }

    @Override
    public void onAttach(Context context) {
        super.onAttach(context);
  /*      if (context instanceof OnFragmentInteractionListener) {
            mListener = (OnFragmentInteractionListener) context;
        } else {
            throw new RuntimeException(context.toString()
                    + " must implement OnFragmentInteractionListener");
        }*/
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    @Override
    public void setObject(Object obj) {
        if(obj instanceof IMcMapViewport)
            mCurrentObject = (IMcMapViewport) obj;
        else
            mCurrentObject = (IMcMapCamera) obj;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mCurrentObject));
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
}
