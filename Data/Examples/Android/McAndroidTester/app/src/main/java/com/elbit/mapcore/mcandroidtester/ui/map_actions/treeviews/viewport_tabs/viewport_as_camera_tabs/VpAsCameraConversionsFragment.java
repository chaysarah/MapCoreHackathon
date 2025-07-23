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
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.viewport_tabs.ViewPortDetailsTabsFragment;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SampleLocationPointsBttn;
import com.elbit.mapcore.mcandroidtester.utils.customviews.ThreeDVector;

import com.elbit.mapcore.Enums.EMcPointCoordSystem;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Calculations.IMcSpatialQueries;
import com.elbit.mapcore.Interfaces.Map.IMcMapCamera;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Structs.SMcVector3D;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link VpAsCameraConversionsFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link VpAsCameraConversionsFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class VpAsCameraConversionsFragment extends Fragment implements FragmentWithObject {

    private OnFragmentInteractionListener mListener;
    private IMcMapCamera mCurrentObject;
    private View mRootView;
    private Button mWorldToScreenBttn;
    private ThreeDVector mCameraConScreenPoint3D;
    private ThreeDVector mCameraConWorldPoint3D;
    private ThreeDVector mCameraConPlaneNormal3D;
    private Button mScreenToWorldOnTerrain;
    private Button mScreenToWorldOnPlane;
    private CheckBox mIntersectionFoundCB;
    private NumericEditTextLabel mPlaneLocationET;
    private SampleLocationPointsBttn mWorldSampleLicationBttn;
    private SampleLocationPointsBttn mScreenSampleLicationBttn;

    public VpAsCameraConversionsFragment() {
        // Required empty public constructor
    }

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment VpAsCameraConversionsFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static VpAsCameraConversionsFragment newInstance() {
        VpAsCameraConversionsFragment fragment = new VpAsCameraConversionsFragment();
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
        mRootView = inflater.inflate(R.layout.fragment_vp_as_camera_conversions, container, false);
        initViews();
        initBttns();
        return mRootView;
    }

    private void initViews() {
        initSampleLocationBttn();
        init3DVectorViews();
        mIntersectionFoundCB = (CheckBox) mRootView.findViewById(R.id.camera_conversions_is_intersection_cb);
        mPlaneLocationET = (NumericEditTextLabel) mRootView.findViewById(R.id.camera_conversions_plane_location_et);
        mPlaneLocationET.setDouble(0.0D);
    }

    private void initSampleLocationBttn() {
        mWorldSampleLicationBttn =(SampleLocationPointsBttn)mRootView.findViewById(R.id.camera_conversions_world_sample_location_bttn);
        mWorldSampleLicationBttn.initBttn(ViewPortDetailsTabsFragment.class.getSimpleName(),true,Double.MAX_VALUE, IMcSpatialQueries.EQueryPrecision.EQP_DEFAULT, EMcPointCoordSystem.EPCS_WORLD,true,R.id.camera_conversions_world,-1);
        mScreenSampleLicationBttn =(SampleLocationPointsBttn)mRootView.findViewById(R.id.camera_conversions_screen_sample_location_bttn);
        mScreenSampleLicationBttn.initBttn(ViewPortDetailsTabsFragment.class.getSimpleName(),true,Double.MAX_VALUE, IMcSpatialQueries.EQueryPrecision.EQP_DEFAULT, EMcPointCoordSystem.EPCS_SCREEN,true,R.id.camera_conversions_screen,-1);
    }

    private void init3DVectorViews() {
        mCameraConScreenPoint3D = (ThreeDVector) mRootView.findViewById(R.id.camera_conversions_screen);
        mCameraConScreenPoint3D.setVector3D(new SMcVector3D(0.0D,0.0D,0.0D));
        mCameraConWorldPoint3D = (ThreeDVector) mRootView.findViewById(R.id.camera_conversions_world);
        mCameraConWorldPoint3D.setVector3D(new SMcVector3D(0.0D,0.0D,0.0D));
        mCameraConPlaneNormal3D = (ThreeDVector) mRootView.findViewById(R.id.camera_conversions_plane_normal);
        mCameraConPlaneNormal3D.setVector3D(new SMcVector3D(0.0D,0.0D,1.0D));
    }

    private void initBttns() {
        initWorldToScreenBttn();
        initScreenToWorldOnTerrainBttn();
        initScreenToWorldOnPlaneBttn();
    }


    private void initScreenToWorldOnPlaneBttn() {
        mScreenToWorldOnPlane = (Button) mRootView.findViewById(R.id.camera_conversions_screen_to_world_on_plane_bttn);
        mScreenToWorldOnPlane.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                SMcVector3D worldPoint = new SMcVector3D();
                ObjectRef<Boolean> intersection = new ObjectRef<>();

                try {
                    mCurrentObject.ScreenToWorldOnPlane(mCameraConScreenPoint3D.getVector3D(),
                            worldPoint,
                            intersection,
                            Double.valueOf(mPlaneLocationET.getText().toString()),
                            mCameraConPlaneNormal3D.getVector3D());
                    mCameraConWorldPoint3D.setVector3D(worldPoint);
                    mIntersectionFoundCB.setChecked(intersection.getValue());
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "ScreenToWorldOnPlane");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void initScreenToWorldOnTerrainBttn() {
        mScreenToWorldOnTerrain = (Button) mRootView.findViewById(R.id.camera_conversions_screen_to_world_on_terrain_bttn);
        mScreenToWorldOnTerrain.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                SMcVector3D worldPoint = new SMcVector3D();
                ObjectRef<Boolean> intersection = new ObjectRef<>();
                try {
                    mCurrentObject.ScreenToWorldOnTerrain(mCameraConScreenPoint3D.getVector3D(), worldPoint, intersection);
                    mCameraConWorldPoint3D.setVector3D(worldPoint);
                    mIntersectionFoundCB.setChecked(intersection.getValue());
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "ScreenToWorldOnTerrain");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void initWorldToScreenBttn() {
        mWorldToScreenBttn = (Button) mRootView.findViewById(R.id.camera_conversions_world_to_screen_bttn);
        mWorldToScreenBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                try {
                    mCameraConScreenPoint3D.setVector3D(mCurrentObject.WorldToScreen(mCameraConWorldPoint3D.getVector3D()));
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "WorldToScreen");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    public void onButtonPressed(Uri uri) {
        if (mListener != null) {
            mListener.onFragmentInteraction(uri);
        }
    }

    @Override
    public void onAttach(Context context) {
        super.onAttach(context);
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
       void onFragmentInteraction(Uri uri);
    }
}
