package com.elbit.mapcore.mcandroidtester.ui.map.fragments;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.RadioButton;
import android.widget.RadioGroup;

import com.elbit.mapcore.Interfaces.Map.IMcMapProduction;
import com.elbit.mapcore.Interfaces.Map.IMcRawVector3DExtrusionMapLayer;
import com.elbit.mapcore.Structs.SMcBox;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.OnCreateCoordinateSystemListener;
import com.elbit.mapcore.mcandroidtester.model.AMCTViewPort;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.ViewPortWithSeveralLayersActivity;
import com.elbit.mapcore.mcandroidtester.utils.customviews.FileChooserEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.RawStaticObjectsParamsDetails;
import com.elbit.mapcore.mcandroidtester.utils.customviews.TwoDVector;

import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.ArrayList;

import com.elbit.mapcore.Interfaces.Calculations.GridCoordSystem.IMcGridCoordinateSystem;
import com.elbit.mapcore.Interfaces.Map.IMcMapCamera;
import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;
import com.elbit.mapcore.Structs.SMcFVector2D;
import com.elbit.mapcore.Structs.SMcVector2D;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link SeveralLayersFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link SeveralLayersFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class SeveralLayersFragment extends Fragment implements OnCreateCoordinateSystemListener/*, OnCreateSRawVector3DExtrusionParamsListener*/ {

    private OnFragmentInteractionListener mListener;
    private ArrayList<IMcMapLayer> mLayersList;
    private Button mCreateCoordSysBttn;
    private Button mCreateRawParamsBttn;
    private Button mCreateRaw3dModelParamsBttn;
    private Button mCreateTilingSchemeRawBttn;
    private CheckBox mImageCoordSysForRawLayersCB;
    private TwoDVector topLeftPoint;
    private TwoDVector bottomRightPoint;

    // private LinearLayout m3DModelll;
    private LinearLayout mCanvas1ll;
    private LinearLayout mCanvas2ll;
    private LinearLayout mCanvas3ll;
    private View mView;
    private CheckBox mIsUseCallbackCB;
    private RawVector3DExtrusionParamsFragment mRawVector3DExtrusionParamsFragment;
    private Raw3DModelParamsFragment mRaw3DModelParamsFragment;
    private TilingSchemeFragment mTilingSchemeFragment;
   // private RawStaticObjectsParamsDetails m3DModelParams;

    public SMcFVector2D getTopLeftViewportPoint()
    {
        return new SMcFVector2D((float) topLeftPoint.getmX(), (float)topLeftPoint.getmY());
    }

    public SMcFVector2D getBottomRightViewportPoint()
    {
        return new SMcFVector2D((float) bottomRightPoint.getmX(), (float)bottomRightPoint.getmY());
    }

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment SeveralLayersFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static SeveralLayersFragment newInstance() {
        SeveralLayersFragment fragment = new SeveralLayersFragment();
        return fragment;
    }

    public SeveralLayersFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        View inflaterView = inflater.inflate(R.layout.fragment_several_layers, container, false);
        mView = inflaterView;
        return inflaterView;
    }

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);

        initEditTxts();
        initRadioBttns();
        initBttns();
        initCB();
        initCanvasPointsBttn();
    }

    private RadioButton mFullScreenRB;
    private RadioButton mOtherRB;

    public void setLayersPath(String strNativeRaster1Path,
                              String strNativeRaster2Path,
                              String strNativeRaster3Path,
                              String strNativeDTMPath,
                              String strNativeVector3DExtrusionPath,
                              String strNative3DModelPath,
                              String strNativeVector1Path,
                              String strNativeVector2Path,
                              String strNativeVector3Path,
                              String strRawRasterPath,
                              String strRawDTMPath,
                              String strRawVectorPath,
                              String strRawVector3DExtrusionPath,
                              String strRaw3DModelPath)
    {
       ((FileChooserEditTextLabel) (mView.findViewById(R.id.native_raster_1))).setStrFolderPath(strNativeRaster1Path);
       ((FileChooserEditTextLabel) (mView.findViewById(R.id.native_raster_2))).setStrFolderPath(strNativeRaster2Path);
       ((FileChooserEditTextLabel) (mView.findViewById(R.id.native_raster_3))).setStrFolderPath(strNativeRaster3Path);
       ((FileChooserEditTextLabel) (mView.findViewById(R.id.native_dtm))).setStrFolderPath(strNativeDTMPath);
        ((FileChooserEditTextLabel) (mView.findViewById(R.id.native_vector_3d_extrusion))).setStrFolderPath(strNativeVector3DExtrusionPath);
        ((FileChooserEditTextLabel) (mView.findViewById(R.id.native_3d_model))).setStrFolderPath(strNative3DModelPath);
        ((FileChooserEditTextLabel) (mView.findViewById(R.id.native_vector_1))).setStrFolderPath(strNativeVector1Path);
        ((FileChooserEditTextLabel) (mView.findViewById(R.id.native_vector_2))).setStrFolderPath(strNativeVector2Path);
        ((FileChooserEditTextLabel) (mView.findViewById(R.id.native_vector_3))).setStrFolderPath(strNativeVector3Path);
        ((FileChooserEditTextLabel) (mView.findViewById(R.id.raw_raster))).setStrFolderPath(strRawRasterPath);
        ((FileChooserEditTextLabel) (mView.findViewById(R.id.raw_dtm))).setStrFolderPath(strRawDTMPath);
        ((FileChooserEditTextLabel) (mView.findViewById(R.id.raw_vector))).setStrFolderPath(strRawVectorPath);
        ((FileChooserEditTextLabel) (mView.findViewById(R.id.raw_vector_3d_extrusion))).setStrFolderPath(strRawVector3DExtrusionPath);
        ((FileChooserEditTextLabel) (mView.findViewById(R.id.raw_3d_model))).setStrFolderPath(strRaw3DModelPath);
    }

    public AMCTViewPort.ViewportSpace getViewportSpaceChoice()
    {
        if(((RadioButton) getView().findViewById(R.id.sl_2D_3D)).isChecked())
            return AMCTViewPort.ViewportSpace.Other;
        else
            return AMCTViewPort.ViewportSpace.FullScreen;
    }

    private void setViewportSpaceParametersVisibility(boolean isVisibility)
    {
        int visibility = isVisibility? View.VISIBLE : View.GONE;
        mCanvas1ll.setVisibility(visibility);
        mCanvas2ll.setVisibility(visibility);
        mCanvas3ll.setVisibility(visibility);
    }

    private void initCanvasPointsBttn()
    {
        topLeftPoint =(TwoDVector) getView().findViewById(R.id.sl_top_left_corner_point);
        bottomRightPoint =(TwoDVector) getView().findViewById(R.id.sl_bottom_right_corner_point);
        topLeftPoint.setVector2D(new SMcVector2D());
        bottomRightPoint.setVector2D(new SMcVector2D());

        mFullScreenRB = (RadioButton) getView().findViewById(R.id.sl_full_screen);

        mOtherRB = (RadioButton) getView().findViewById(R.id.sl_other);
        mOtherRB.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton compoundButton, boolean b) {
                setViewportSpaceParametersVisibility(b);
            }
        });
        mCanvas1ll = (LinearLayout)  getView().findViewById(R.id.sl_canvas_1_ll);
        mCanvas2ll = (LinearLayout)  getView().findViewById(R.id.sl_canvas_2_ll);
        mCanvas3ll = (LinearLayout)  getView().findViewById(R.id.sl_canvas_3_ll);

        mFullScreenRB.setChecked(true);
        setViewportSpaceParametersVisibility(false);

        getView().findViewById(R.id.sl_two_canvas_vertical_1).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                SetViewportPoints(new SMcVector2D(0, 0),new SMcVector2D(0.5, 1));
            }
        });

        getView().findViewById(R.id.sl_two_canvas_vertical_2).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                SetViewportPoints(new SMcVector2D(0.5, 0),new SMcVector2D(1, 1));
            }
        });

        getView().findViewById(R.id.sl_two_canvas_horizontal_1).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                SetViewportPoints(new SMcVector2D(0, 0),new SMcVector2D(1, 0.5));
            }
        });

        getView().findViewById(R.id.sl_two_canvas_horizontal_2).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                SetViewportPoints(new SMcVector2D(0, 0.5),new SMcVector2D(1, 1));
            }
        });

        getView().findViewById(R.id.sl_four_canvas_1).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                SetViewportPoints(new SMcVector2D(0, 0),new SMcVector2D(0.5, 0.5));
            }
        });
        getView().findViewById(R.id.sl_four_canvas_2).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                SetViewportPoints(new SMcVector2D(0, 0.5),new SMcVector2D(0.5, 1));
            }
        });
        getView().findViewById(R.id.sl_four_canvas_3).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                SetViewportPoints(new SMcVector2D(0.5, 0),new SMcVector2D(1, 0.5));
            }
        });
        getView().findViewById(R.id.sl_four_canvas_4).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                SetViewportPoints(new SMcVector2D(0.5, 0.5),new SMcVector2D(1, 1));
            }
        });

        getView().findViewById(R.id.sl_three_canvas_horizontal_1).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                SetViewportPoints(new SMcVector2D(0, 0),new SMcVector2D(1, 0.33));
            }
        });
        getView().findViewById(R.id.sl_three_canvas_horizontal_2).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                SetViewportPoints(new SMcVector2D(0, 0.33),new SMcVector2D(1, 0.66));
            }
        });
        getView().findViewById(R.id.sl_three_canvas_horizontal_3).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                SetViewportPoints(new SMcVector2D(0, 0.66),new SMcVector2D(1, 1));
            }
        });

        getView().findViewById(R.id.sl_three_canvas_vertical_1).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                SetViewportPoints(new SMcVector2D(0, 0),new SMcVector2D(0.33, 1));
            }
        });
        getView().findViewById(R.id.sl_three_canvas_vertical_2).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                SetViewportPoints(new SMcVector2D(0.33, 0),new SMcVector2D(0.66, 1));
            }
        });
        getView().findViewById(R.id.sl_three_canvas_vertical_3).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                SetViewportPoints(new SMcVector2D(0.66, 0),new SMcVector2D(1, 1));
            }
        });
    }

    private void SetViewportPoints(SMcVector2D topLeft, SMcVector2D bottomRight)
    {
        topLeftPoint.setVector2D(topLeft);
        bottomRightPoint.setVector2D(bottomRight);
    }

    private void initCB() {
        initImageCoordSysForRawLayers();
        initIsUseCallback();
    }

    private void initImageCoordSysForRawLayers() {
        mImageCoordSysForRawLayersCB=(CheckBox)getView().findViewById(R.id.several_layers_image_coord_sys_for_raw_layers_cb);
        mImageCoordSysForRawLayersCB.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                ((ViewPortWithSeveralLayersActivity) getActivity()).setmIsImageCoordSysForRawLayers(isChecked);
            }
        });
    }

    private void initIsUseCallback() {
        mIsUseCallbackCB = (CheckBox) getView().findViewById(R.id.several_layers_is_use_callback_cb);
        mIsUseCallbackCB.setChecked(true);
        ((ViewPortWithSeveralLayersActivity) getActivity()).setmIsUseCallback(true);
        mIsUseCallbackCB.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                ((ViewPortWithSeveralLayersActivity) getActivity()).setmIsUseCallback(isChecked);
            }
        });
    }

    private void initBttns() {
        initCreateCoordSysForRawLayers();
        initParametersForRawVector3DExtrusionLayers();
        initTilingSchemeRawLayers();
    }

    private void initCreateCoordSysForRawLayers() {
        mCreateCoordSysBttn=(Button)getView().findViewById(R.id.several_layers_raw_coord_sys_bttn);
        mCreateCoordSysBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                GridCoordinateSysFragment gridCoordinateSysFragment = new GridCoordinateSysFragment();
                FragmentTransaction transaction = getChildFragmentManager().beginTransaction();
                transaction.addToBackStack("gridCoordinateSysFragment");
                transaction.add(R.id.several_layers_container_fragment, gridCoordinateSysFragment, "gridCoordinateSysFragment").commit();
            }
        });
    }

    private void initParametersForRawVector3DExtrusionLayers() {
        mCreateRawParamsBttn=(Button)getView().findViewById(R.id.several_layers_raw_3d_extrusion_params_bttn);
        mCreateRawParamsBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                mRawVector3DExtrusionParamsFragment = new RawVector3DExtrusionParamsFragment();
                FragmentTransaction transaction = getChildFragmentManager().beginTransaction();
                transaction.addToBackStack("rawVector3DExtrusionParamsFragment");
                transaction.add(R.id.several_layers_container_raw_3d_extrusion_params_fragment, mRawVector3DExtrusionParamsFragment, "rawVector3DExtrusionParamsFragment").commit();
            }
        });
        mCreateRaw3dModelParamsBttn=(Button)getView().findViewById(R.id.several_layers_raw_3d_model_params_bttn);
        mCreateRaw3dModelParamsBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                mRaw3DModelParamsFragment = new Raw3DModelParamsFragment();
                mRaw3DModelParamsFragment.setTinyVisibility();
                FragmentTransaction transaction = getChildFragmentManager().beginTransaction();
                String tag = "raw3DModelParamsFragment";
                transaction.addToBackStack(tag);
                transaction.add(R.id.several_layers_container_raw_3d_model_params_fragment, mRaw3DModelParamsFragment, tag).commit();
            }
        });
    }

    private void initTilingSchemeRawLayers() {
        mCreateTilingSchemeRawBttn=(Button)getView().findViewById(R.id.several_layers_tiling_scheme_bttn);
        mCreateTilingSchemeRawBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                mTilingSchemeFragment = new TilingSchemeFragment();
                FragmentTransaction transaction = getChildFragmentManager().beginTransaction();
                transaction.addToBackStack("tilingSchemeParamsFragment");
                transaction.add(R.id.several_layers_container_tiling_scheme_fragment, mTilingSchemeFragment, "tilingSchemeParamsFragment").commit();
            }
        });
    }
    private void initRadioBttns() {
        ((RadioGroup)getView().findViewById(R.id.sl_map_type_rg)).setOnCheckedChangeListener(new RadioGroup.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(RadioGroup group, int checkedId) {
                int id = checkedId;
                if (id == R.id.sl_2D) {
                    ((ViewPortWithSeveralLayersActivity) getActivity()).setMapType(IMcMapCamera.EMapType.EMT_2D);
                }
                else if (id == R.id.sl_3D) {
                    ((ViewPortWithSeveralLayersActivity) getActivity()).setMapType(IMcMapCamera.EMapType.EMT_3D);
                }
                else if (id == R.id.sl_2D_3D) {
                    ((ViewPortWithSeveralLayersActivity) getActivity()).setMapType(IMcMapCamera.EMapType.EMT_2D);

                }
                else{
                    ((ViewPortWithSeveralLayersActivity) getActivity()).setMapType(IMcMapCamera.EMapType.EMT_2D);
                }
            }
        });
        //((RadioGroup) this.getActivity().findViewById(R.id.sl_map_type_rg)).check((((ViewPortWithSeveralLayersActivity) getActivity()).mSCreateData.eMapType)==IMcMapCamera.EMapType.EMT_2D ? R.id.sl_2D:R.id.sl_3D);
        ((RadioGroup)getView().findViewById(R.id.sl_map_type_rg)).check(R.id.sl_2D);
    }

    private void initEditTxts() {
        java.lang.reflect.Method method;
     try {
            method = ((ViewPortWithSeveralLayersActivity) getActivity()).getClass().getMethod("setmRaster1Path", String.class);
            //todo uncomment, ewmove the def val (added for testing only)
            initEditTxt(R.id.native_raster_1,method,getActivity());
            //initEditTxtWithDefVal(R.id.native_raster_1,method,getActivity(),"/storage/emulated/0/Maps/Raster");
           // initEditTxtWithDefVal(R.id.native_raster_1,method,getActivity(),"/sdcard/Maps/NetanyaGeoWgs84/RasterJpg");
            method = ((ViewPortWithSeveralLayersActivity) getActivity()).getClass().getMethod("setmRaster2Path", String.class);
            initEditTxt(R.id.native_raster_2,method,getActivity());
            method = ((ViewPortWithSeveralLayersActivity) getActivity()).getClass().getMethod("setmRaster3Path", String.class);
            initEditTxt(R.id.native_raster_3,method,getActivity());

            method = ((ViewPortWithSeveralLayersActivity) getActivity()).getClass().getMethod("setmDtmPath", String.class);
            //initEditTxtWithDefVal(R.id.native_dtm,method,getActivity(),"/storage/emulated/0/Maps/NetanyaGeoWgs84/Dtm");
         // initEditTxtWithDefVal(R.id.native_dtm,method,getActivity(),"/storage/emulated/0/Maps/NetanyaGeoWgs84/Dtm");

         //initEditTxtWithDefVal(R.id.native_dtm,method,getActivity(),"/storage/emulated/0/MapCore/Maccabim/Maccabim/DTM");
            //initEditTxtWithDefVal(R.id.native_dtm,method,getActivity(),"/storage/emulated/0/Maps/IsraelGeo/DTM");
            initEditTxt(R.id.native_dtm,method,getActivity());
            method = ((ViewPortWithSeveralLayersActivity) getActivity()).getClass().getMethod("setmRawDtmPath", String.class);
            initEditTxt(R.id.raw_dtm,method,getActivity());
            method = ((ViewPortWithSeveralLayersActivity) getActivity()).getClass().getMethod("setmRawRasterPath", String.class);
            initEditTxt(R.id.raw_raster,method,getActivity());
         //   initEditTxtWithDefVal(R.id.raw_raster,method,getActivity(),"/storage/emulated/0/maps/Maccabim/Source/Raster");

            method = ((ViewPortWithSeveralLayersActivity) getActivity()).getClass().getMethod("setmNativeVector1Path", String.class);
            initEditTxt(R.id.native_vector_1,method,getActivity());
            //initEditTxtWithDefVal(R.id.native_vector_1,method,getActivity(),"/storage/emulated/0/maps/is_gas_POINTS");
            method = ((ViewPortWithSeveralLayersActivity) getActivity()).getClass().getMethod("setmNativeVector2Path", String.class);
            initEditTxt(R.id.native_vector_2,method,getActivity());
            method = ((ViewPortWithSeveralLayersActivity) getActivity()).getClass().getMethod("setmNativeVector3Path", String.class);
            initEditTxt(R.id.native_vector_3,method,getActivity());

            method = ((ViewPortWithSeveralLayersActivity) getActivity()).getClass().getMethod("setmNativeVector3DExtrusionPath", String.class);
            // initEditTxtWithDefVal(R.id.native_vector_3d_extrusion,method,getActivity(),"/storage/emulated/0/MapCore/Maps/HeightsNetanya");
            initEditTxt(R.id.native_vector_3d_extrusion,method,getActivity());

            method = ((ViewPortWithSeveralLayersActivity) getActivity()).getClass().getMethod("setmRawVector3DExtrusionPath", String.class);
            initEditTxt(R.id.raw_vector_3d_extrusion,method,getActivity());
            //initEditTxtWithDefVal(R.id.raw_vector_3d_extrusion,method,getActivity(),"/storage/emulated/0/Maps/SqLiteBuildings/small/db3.sqlite");

            method = ((ViewPortWithSeveralLayersActivity) getActivity()).getClass().getMethod("setmNative3DModelPath", String.class);
            initEditTxt(R.id.native_3d_model,method,getActivity());
            //initEditTxtWithDefVal(R.id.native_3d_model,method,getActivity(),"/storage/emulated/0/MapCore/Maps/StaticObjects3dModel/Native/Elyakim");

			// raw 3D model 
             method = ((ViewPortWithSeveralLayersActivity) getActivity()).getClass().getMethod("setmRaw3DModelPath", String.class);
             initEditTxt(R.id.raw_3d_model,method,getActivity());
          //   initEditTxtWithDefVal(R.id.raw_3d_model,method,getActivity(),"/storage/emulated/0/Maps/App_cesium/raw_UE");
             //initEditTxtWithDefVal(R.id.raw_3d_model,method,getActivity(),"/storage/emulated/0/Maps/Test_2_tiled_CESIUM/raw_UE");

            method = ((ViewPortWithSeveralLayersActivity) getActivity()).getClass().getMethod("setmRawVectorPath", String.class);
            initEditTxt(R.id.raw_vector,method,getActivity());
            //initEditTxtWithDefVal(R.id.raw_vector,method,getActivity(),"/storage/emulated/0/Maps/KML/Arizona_GMU_Polygons0/Arizona_GMU_Polygons0.kml");
            //initEditTxtWithDefVal(R.id.raw_vector,method,getActivity(),"/storage/emulated/0/MapCore/Maps/Shapes_ED_50_2/is_brd_LINES.SHP");
            //initEditTxtWithDefVal(R.id.raw_vector,method,getActivity(),"/storage/emulated/0/MapCore/Maps/MaccabimRoads-Vector");
        }
        catch (NoSuchMethodException e) {
            e.printStackTrace();
        }

    }
    public void initEditTxt(int id, final Method method, final Object objToInvoke)
    {
        ((EditText) (getView().findViewById(id)).findViewById(R.id.edittext_in_cv_fel)).addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {

            }

            @Override
            public void afterTextChanged(Editable s) {
                try {
                    method.invoke(objToInvoke,s.toString());
                } catch (IllegalAccessException e) {
                    e.printStackTrace();
                } catch (InvocationTargetException e) {
                    e.printStackTrace();
                }
            }
        });
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
    }

    @Override
    public void onPause() {
        super.onPause();
    }

    //todo remove this func after testing
    public void initEditTxtWithDefVal(int id, final Method method, final Object objToInvoke,String defVal)
    {
        initEditTxt(id,method,objToInvoke);
        ((FileChooserEditTextLabel) (getView().findViewById(id))).setStrFolderPath(defVal);
    }

    // TODO: Rename method, update argument and hook method into UI event
    public void onButtonPressed(Uri uri) {
        if (mListener != null) {
            mListener.onFragmentInteraction(uri);
        }
    }

    @Override
    public void onAttach(Context context) {
        ((ViewPortWithSeveralLayersActivity) context).mSeveralLayersFragment = SeveralLayersFragment.this;

        super.onAttach(context);
        if (context instanceof OnFragmentInteractionListener) {
            mListener = (OnFragmentInteractionListener) context;
        } else {
            throw new RuntimeException(context.toString()
                    + " must implement OnFragmentInteractionListener");
        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    @Override
    public void onCoordSysCreated(IMcGridCoordinateSystem mNewGridCoordinateSystem) {
        ((ViewPortWithSeveralLayersActivity) getActivity()).setmCoordSysForRawLayers(mNewGridCoordinateSystem);
    }

 /*   @Override
    public void onSRawVector3DExtrusionParamsCreated(IMcRawVector3DExtrusionMapLayer.SParams params) {
        ((ViewPortWithSeveralLayersActivity) getActivity()).setParamsForRawVectorExtrusionLayers(params);
    }*/

    public IMcRawVector3DExtrusionMapLayer.SParams getRawVector3DExtrusionParams() {
        if(mRawVector3DExtrusionParamsFragment != null)
            return mRawVector3DExtrusionParamsFragment.getParams();
        else
            return null;
    }

    public IMcRawVector3DExtrusionMapLayer.SGraphicalParams getRawVector3DExtrusionGraphicalParams() {
        if(mRawVector3DExtrusionParamsFragment != null)
            return mRawVector3DExtrusionParamsFragment.getGraphicalParams();
        else
            return null;
    }

    public boolean isExtUseBuiltIndexingDataDir() {
        if(mRawVector3DExtrusionParamsFragment != null)
            return mRawVector3DExtrusionParamsFragment.IsUseBuiltIndexingDataDir();
        else
            return false;
    }

    public String getExtNonDefaultIndexDir() {
        if(mRawVector3DExtrusionParamsFragment != null)
            return mRawVector3DExtrusionParamsFragment.getNonDefaultIndexDir();
        else
            return "";
    }

    public boolean is3DMModelUseBuiltIndexingDataDir() {
        return mRaw3DModelParamsFragment != null && mRaw3DModelParamsFragment.IsUseBuiltIndexingDataDir();
    }

    public String get3DMModelNonDefaultIndexDir() {
        return mRaw3DModelParamsFragment != null ? mRaw3DModelParamsFragment.getNonDefaultIndexDir() : "";
    }

    public boolean is3DMModelOrthometicHeight() {
        return mRaw3DModelParamsFragment != null ? mRaw3DModelParamsFragment.getOrthometicHeight() : new IMcMapProduction.S3DModelConvertParams().bOrthometricHeights;
    }

    public float get3DMModelTargetHighestResolution()
    {
        return mRaw3DModelParamsFragment != null?  mRaw3DModelParamsFragment.getTargetHighestResolution() : new IMcMapProduction.S3DModelConvertParams().fTargetHighestResolution;
    }

    public IMcGridCoordinateSystem get3DMModelGridCoordinateSystem()
    {
        return mRaw3DModelParamsFragment != null ? mRaw3DModelParamsFragment.getGridCoordinateSystem() : null;
    }

    public SMcBox get3DMModelClipRect()
    {
        return mRaw3DModelParamsFragment != null ? mRaw3DModelParamsFragment.getClipRect() : null;
    }

    public IMcMapLayer.STilingScheme getSTilingScheme() {
        return mTilingSchemeFragment != null ? mTilingSchemeFragment.getParams() : null;
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
