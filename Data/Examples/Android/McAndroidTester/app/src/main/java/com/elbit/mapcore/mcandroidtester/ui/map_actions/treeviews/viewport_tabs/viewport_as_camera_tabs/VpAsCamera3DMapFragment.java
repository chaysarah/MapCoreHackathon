package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.viewport_tabs.viewport_as_camera_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.CheckBox;

import com.elbit.mapcore.General.ObjectRef;
import com.elbit.mapcore.Structs.SMcVector3D;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.viewport_tabs.ViewPortDetailsTabsFragment;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SampleLocationPointsBttn;
import com.elbit.mapcore.mcandroidtester.utils.customviews.ThreeDOrientation;
import com.elbit.mapcore.mcandroidtester.utils.customviews.ThreeDVector;

import com.elbit.mapcore.Enums.EMcPointCoordSystem;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Calculations.IMcSpatialQueries;
import com.elbit.mapcore.Interfaces.Map.IMcMapCamera;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link VpAsCamera3DMapFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link VpAsCamera3DMapFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class VpAsCamera3DMapFragment extends Fragment implements FragmentWithObject {

    private OnFragmentInteractionListener mListener;
    private IMcMapCamera mCurrentObject;
    private View mRootView;
    private ThreeDVector mLookAt3D;
    private ThreeDVector mForwardVector3D;
    private CheckBox mRenderInTwoSessionsCb;
    private CheckBox mVectorRelativeCb;
    private NumericEditTextLabel mMinCameraClipDistancesET;
    private NumericEditTextLabel mMaxCameraClipDistancesET;
    private NumericEditTextLabel mFieldOfView;
    private ThreeDOrientation mRotateCamera3D;
    private CheckBox mRotateCamera3DYawAbsoluteCB;
    private NumericEditTextLabel mMinRelativeHeightLimitsET;
    private NumericEditTextLabel mMaxRelativeHeightLimitsET;
    private CheckBox mCameraRelativeHeightLimitsCB;
    private CheckBox mFootprintIsDefinedCB;
    private IMcMapCamera.EMapType mMapType;
    private SampleLocationPointsBttn mSampleLocationPoint;

    public VpAsCamera3DMapFragment() {
        // Required empty public constructor
    }

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment VpAsCamera3DMapFragment.
     */
    public static VpAsCamera3DMapFragment newInstance() {
        VpAsCamera3DMapFragment fragment = new VpAsCamera3DMapFragment();
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
        mRootView = inflater.inflate(R.layout.fragment_vp_as_camera_3d_map, container, false);
        initViews();
        return mRootView;
    }

    private void initViews() {
        initMapType();
        init3DVectors();
        initSaveButtons();
        initCB();
        initEditTxts();
        initSampleLocationBttn();
    }

    private void initSampleLocationBttn() {
        mSampleLocationPoint=(SampleLocationPointsBttn)mRootView.findViewById(R.id.map_3D_look_at_sample_location_bttn);
        mSampleLocationPoint.initBttn(ViewPortDetailsTabsFragment.class.getSimpleName(),true,Double.MAX_VALUE, IMcSpatialQueries.EQueryPrecision.EQP_DEFAULT, EMcPointCoordSystem.EPCS_WORLD,true,R.id.map_3D_look_at,-1);
    }

    private void initMapType() {
        try {
            mMapType= mCurrentObject.GetMapType();
        }catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetMapType");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initEditTxts() {
        if(mMapType==IMcMapCamera.EMapType.EMT_3D) {
            initFieldOfView();
            initHeightLimits();
            initCameraClipDistances();
        }
        initFootprintIsDefined();
    }

    private void initFootprintIsDefined() {
        mFootprintIsDefinedCB=(CheckBox)mRootView.findViewById(R.id.map_3D_defined_footprint_cb);
    }

    private void initCameraClipDistances() {
        mMinCameraClipDistancesET = (NumericEditTextLabel) mRootView.findViewById(R.id.map_3D_camera_clip_distances_min);
        mMaxCameraClipDistancesET = (NumericEditTextLabel) mRootView.findViewById(R.id.map_3D_camera_clip_distances_max);
        ObjectRef<Float> minDistances = new ObjectRef<>();
        ObjectRef<Float> maxDistances = new ObjectRef<>();
        ObjectRef<Boolean> twoSessionsRender = new ObjectRef<>();
        try {
            mCurrentObject.GetCameraClipDistances(minDistances, maxDistances, twoSessionsRender);
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetCameraClipDistances");
        } catch (Exception e) {
            e.printStackTrace();
        }
        mMinCameraClipDistancesET.setFloat(minDistances.getValue());
        mMaxCameraClipDistancesET.setFloat(maxDistances.getValue());
        mRenderInTwoSessionsCb.setChecked(twoSessionsRender.getValue());
    }

    private void initHeightLimits() {
        mMinRelativeHeightLimitsET = (NumericEditTextLabel) mRootView.findViewById(R.id.map_3D_height_limits_min);
        mMaxRelativeHeightLimitsET = (NumericEditTextLabel) mRootView.findViewById(R.id.map_3D_height_limits_max);
        //Boolean enableRelative = false;
        ObjectRef<Double> minHeight = new ObjectRef<>();
        ObjectRef<Double> maxHeight = new ObjectRef<>();
        ObjectRef<Boolean> enableRelative = new ObjectRef<>();
        //Double minHeight = new Double(0), maxHeight = new Double(0);
        try {
            mCurrentObject.GetCameraRelativeHeightLimits(minHeight, maxHeight, enableRelative);
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetCameraRelativeHeightLimits");
        } catch (Exception e) {
            e.printStackTrace();
        }
        mMinRelativeHeightLimitsET.setDouble(minHeight.getValue());
        mMaxRelativeHeightLimitsET.setDouble(maxHeight.getValue());
        mCameraRelativeHeightLimitsCB.setChecked(enableRelative.getValue());
    }

    private void initFieldOfView() {
        mFieldOfView = (NumericEditTextLabel) mRootView.findViewById(R.id.map_3D_field_of_view);
        try {
            mFieldOfView.setFloat(mCurrentObject.GetCameraFieldOfView());
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetCameraFieldOfView");

        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initCB() {
        mVectorRelativeCb = (CheckBox) mRootView.findViewById(R.id.map_3D_forward_vector_relative_to_orientation);
        mCameraRelativeHeightLimitsCB = (CheckBox) mRootView.findViewById(R.id.map_3D_height_limits_enabled_cb);
        mRenderInTwoSessionsCb = (CheckBox) mRootView.findViewById(R.id.map_3D_render_in_2_sessions_cb);
    }

    private void initSaveButtons() {
        initSaveLookAtBttn();
        initSaveForwardVectorBttn();
        initSaveFieldOfViewBttn();
        initSaveRotateCameraRelativeToOrientationBttn();
        initSaveHeightLimitsBttn();
        initSaveCameraClipDistancesBttn();
        initSaveFootprintIsDefinedBttn();
    }



    private void initSaveFootprintIsDefinedBttn() {
        Button saveFootprintIsDefinedBttn=(Button)mRootView.findViewById(R.id.map_3D_defined_footprint_OK_bttn);
        saveFootprintIsDefinedBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveFootprintIsDefined();
                initViews();
            }
        });
    }

    private void initSaveCameraClipDistancesBttn() {
        Button saveCameraClipDistancesBttn=(Button)mRootView.findViewById(R.id.map_3D_camera_clip_distances_OK_bttn);
        saveCameraClipDistancesBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveCameraClipDistances();
                initViews();
            }
        });
    }

    private void initSaveHeightLimitsBttn() {
        Button saveHeightLimitsBttn=(Button)mRootView.findViewById(R.id.map_3D_height_limits_OK_bttn);
        saveHeightLimitsBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveHeightLimits();
                initViews();
            }
        });
    }

    private void initSaveRotateCameraRelativeToOrientationBttn() {
        Button saveRotateCameraRelativeToOrientationBttn=(Button)mRootView.findViewById(R.id.map_3D_rotate_camera_OK_bttn);
        saveRotateCameraRelativeToOrientationBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveRotateCameraRelativeToOrientation();
                initViews();
            }
        });
    }

    private void initSaveFieldOfViewBttn() {
        Button saveFieldOfViewBttn=(Button)mRootView.findViewById(R.id.map_3D_field_of_view_OK_bttn);
        saveFieldOfViewBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveFieldOfView();
                initViews();
            }
        });
    }

    private void initSaveForwardVectorBttn() {
        Button saveForwardVectorBttn=(Button)mRootView.findViewById(R.id.map_3D_forward_vector_OK_bttn);
        saveForwardVectorBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveForwardVector();
                initViews();
            }
        });
    }

    private void initSaveLookAtBttn() {
        Button saveLookAtBttn=(Button)mRootView.findViewById(R.id.map_3D_look_at_OK_bttn);
        saveLookAtBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveLookAt();
                initViews();
            }
        });
    }

    private void saveFootprintIsDefined() {
        //TODO when to use this value?didn't find any usage in the tester
    }

    private void saveHeightLimits() {

        if (mMapType == IMcMapCamera.EMapType.EMT_3D) {
            final double minRelativeHeight = mMinRelativeHeightLimitsET.getDouble();
            final double maxRelativeHeight = mMaxRelativeHeightLimitsET.getDouble();
            final boolean isCameraRelativeHeightLimits = mCameraRelativeHeightLimitsCB.isChecked();
            Funcs.runMapCoreFunc(new Runnable() {
                @Override
                public void run() {
                    try {
                        mCurrentObject.SetCameraRelativeHeightLimits(minRelativeHeight, maxRelativeHeight, isCameraRelativeHeightLimits);
                    } catch (MapCoreException e) {
                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetMapType/SetCameraRelativeHeightLimits");
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            });
        } else
            AlertMessages.ShowErrorMessage(getContext(), "saveHeightLimits", "Only for 3D map");
    }

    private void saveCameraClipDistances() {
        final float minCameraClipDistances = mMinCameraClipDistancesET.getFloat();
        final float maxCameraClipDistances = mMaxCameraClipDistancesET.getFloat();
        final boolean renderInTwoSessionsCb = mRenderInTwoSessionsCb.isChecked();

        if (mMapType == IMcMapCamera.EMapType.EMT_3D) {

          Funcs.runMapCoreFunc(new Runnable() {
              @Override
              public void run() {
                  try {
                      mCurrentObject.SetCameraClipDistances(minCameraClipDistances, maxCameraClipDistances, renderInTwoSessionsCb);
                  } catch (MapCoreException e) {
                      AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetCameraClipDistances");
                  } catch (Exception e) {
                      e.printStackTrace();
                  }
              }
          });
        }
        else
            AlertMessages.ShowErrorMessage(getContext(), "SetCameraClipDistances", "Only for 3D map");
    }

    private void saveRotateCameraRelativeToOrientation() {

        final float yaw = mRotateCamera3D.getmYaw();
        final float pitch = mRotateCamera3D.getmPitch();
        final float roll = mRotateCamera3D.getmRoll();
        final boolean yawAbsolute = mRotateCamera3DYawAbsoluteCB.isChecked();
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    mCurrentObject.RotateCameraRelativeToOrientation(yaw, pitch, roll, yawAbsolute);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "RotateCameraRelativeToOrientation");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void saveFieldOfView() {

        if (mMapType == IMcMapCamera.EMapType.EMT_3D) {

            final float fieldOfView = mFieldOfView.getFloat();
            Funcs.runMapCoreFunc(new Runnable() {
                @Override
                public void run() {
                    try {
                        mCurrentObject.SetCameraFieldOfView(fieldOfView);
                    } catch (MapCoreException e) {
                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetCameraFieldOfView");
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            });
        } else
            AlertMessages.ShowErrorMessage(getContext(), "CameraFieldOfView", "Only for 3D map");
    }

    private void saveForwardVector() {

        if (mMapType == IMcMapCamera.EMapType.EMT_3D) {
            final SMcVector3D cameraForwardVector = mForwardVector3D.getVector3D();
            final boolean vectorRelative = mVectorRelativeCb.isChecked();

            Funcs.runMapCoreFunc(new Runnable() {
                @Override
                public void run() {
                    try {
                        mCurrentObject.SetCameraForwardVector(cameraForwardVector, vectorRelative);
                    } catch (MapCoreException e) {
                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetCameraForwardVector");
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            });
        }
    }

    private void saveLookAt() {
        final SMcVector3D lookAt = mLookAt3D.getVector3D();
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    mCurrentObject.SetCameraLookAtPoint(lookAt);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetCameraLookAtPoint");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void init3DVectors() {
        mLookAt3D = (ThreeDVector) mRootView.findViewById(R.id.map_3D_look_at);
        mForwardVector3D = (ThreeDVector) mRootView.findViewById(R.id.map_3D_forward_vector);
        mRotateCamera3D = (ThreeDOrientation) mRootView.findViewById(R.id.map_3D_rotate_camera_relative_to_orientation);
        mRotateCamera3DYawAbsoluteCB = (CheckBox) mRootView.findViewById(R.id.map_3D_rotate_camera_relative_to_orientation_yaw_obsolute);
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
