package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.viewport_tabs;

import android.content.Context;
import android.graphics.Color;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.ListView;

import com.elbit.mapcore.General.ObjectRef;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.ui.adapters.HeightColorsRowAdapter;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.HeightColorItem;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;

import java.util.ArrayList;
import java.util.List;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Structs.SMcBColor;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link ViewPortDTMVisualizationFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link ViewPortDTMVisualizationFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class ViewPortDTMVisualizationFragment extends Fragment implements FragmentWithObject {
    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;

    private OnFragmentInteractionListener mListener;
    private IMcMapViewport mViewport;
    private View mRootView;
    private CheckBox mDtmVisualizationWithoutRasterEnable;
    private NumericEditTextLabel mHeightColorsHeightOriginET;
    private NumericEditTextLabel mHeightColorsHeightStepET;
    private NumericEditTextLabel mShadingHeightFactorET;
    private NumericEditTextLabel mShadingLightSourceYawET;
    private NumericEditTextLabel mShadingLightSourcePitchET;
    private NumericEditTextLabel mHeightColorsTransparencyET;
    private NumericEditTextLabel mShadingTransparencyET;
    private CheckBox mHeightColorsInterpolationCB;
    private CheckBox mDtmVisualizationAboveRasterCB;
    private NumericEditTextLabel mDtmVisualizationWithoutRasterMinHeightET;
    private NumericEditTextLabel mDtmVisualizationWithoutRasterMaxHeightET;
    private CheckBox mDtmTransparencyWithoutRasterCB;
    private Button mDtmVisualizationWithoutRasterOkBttn;
    private Button mResetFormBttn;
    private float mMaxHeight = -500;
    private float mMinHeight = -9500;
    private ListView mDtmVisualColorsLV;
    private Button mDtmVisualizationWithoutRasterDefaultOKBttn;
    private Button mDtmTransparencyWithoutRasterBttn;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment ViewPortDTMVisualizationFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static ViewPortDTMVisualizationFragment newInstance(String param1, String param2) {
        ViewPortDTMVisualizationFragment fragment = new ViewPortDTMVisualizationFragment();
        Bundle args = new Bundle();
        args.putString(ARG_PARAM1, param1);
        args.putString(ARG_PARAM2, param2);
        fragment.setArguments(args);
        return fragment;
    }

    public ViewPortDTMVisualizationFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if (getArguments() != null) {
            mParam1 = getArguments().getString(ARG_PARAM1);
            mParam2 = getArguments().getString(ARG_PARAM2);
        }
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        Funcs.SetObjectFromBundle(savedInstanceState, this );
        mRootView = inflater.inflate(R.layout.fragment_view_port_dtmvisualization, container, false);
        initViews();
        return mRootView;
    }

    private void initViews() {
        inflateViews();
        initDtmVisualizationParams();
        initDtmTransparencyWithoutRasterCB();
        initDtmTransparencyWithoutRasterBttn();
        initDtmVisualizationWithoutRasterDefaultOkBttn();
        initSetDtmVisualizationBttn();//==btnDtmVisualizationWithoutRasterOK
        initResetFormBttn();
    }

    private void initDtmVisualizationWithoutRasterDefaultOkBttn() {
        mDtmVisualizationWithoutRasterDefaultOKBttn = (Button) mRootView.findViewById(R.id.dtm_visualization_dtm_visualization_without_raster_default_OK_bttn);
        mDtmVisualizationWithoutRasterDefaultOKBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveDtmVisualizationWithoutRasterDefault();
            }
        });
    }

    private void initResetFormBttn() {
        mResetFormBttn = (Button) mRootView.findViewById(R.id.dtm_visualization_reset_form_bttn);
        mResetFormBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                IMcMapViewport.SDtmVisualizationParams DtmVisualizationParams = new IMcMapViewport.SDtmVisualizationParams();
                loadDtmVisualization(DtmVisualizationParams);
            }
        });

    }

    private void initDtmTransparencyWithoutRasterBttn() {
        mDtmTransparencyWithoutRasterBttn = (Button) mRootView.findViewById(R.id.dtm_visualization_transparency_without_raster_bttn);
        mDtmTransparencyWithoutRasterBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveDtmTransparencyWithoutRaster();
            }
        });
    }

    private void saveDtmTransparencyWithoutRaster() {
        final boolean mDtm = mDtmTransparencyWithoutRasterCB.isChecked();
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    mViewport.SetDtmTransparencyWithoutRaster(mDtm);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetDtmTransparencyWithoutRaster");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void initSetDtmVisualizationBttn() {
        mDtmVisualizationWithoutRasterOkBttn = (Button) mRootView.findViewById(R.id.dtm_visualization_set_dtm_visualization_bttn);
        mDtmVisualizationWithoutRasterOkBttn.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                final IMcMapViewport.SDtmVisualizationParams dtmVisualizationParams = getDtmVisualizationParams();
                final boolean bDtmVisualizationWithoutRasterEnable = mDtmVisualizationWithoutRasterEnable.isChecked();
                ((MapsContainerActivity) getActivity()).mMapFragment.mView.queueEvent(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            mViewport.SetDtmVisualization(bDtmVisualizationWithoutRasterEnable, dtmVisualizationParams);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetDtmVisualization");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }

                });
            }
        });
    }


    private void saveDtmVisualizationWithoutRasterDefault() {
        final IMcMapViewport.SDtmVisualizationParams dtmVisualizationParams = getDtmVisualizationParams();

        final float minHeight = mDtmVisualizationWithoutRasterMinHeightET.getFloat();
        final float maxHeight = mDtmVisualizationWithoutRasterMaxHeightET.getFloat();
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                dtmVisualizationParams.SetDefaultHeightColors(minHeight, maxHeight);
                getActivity().runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        loadDtmVisualization(dtmVisualizationParams);
                    }
                });
            }
        });


    }

    private IMcMapViewport.SDtmVisualizationParams getDtmVisualizationParams() {
        IMcMapViewport.SDtmVisualizationParams DtmVisualizationParams = new IMcMapViewport.SDtmVisualizationParams();
        DtmVisualizationParams.fHeightColorsHeightOrigin = mHeightColorsHeightOriginET.getFloat();
        DtmVisualizationParams.fHeightColorsHeightStep = mHeightColorsHeightStepET.getFloat();
        DtmVisualizationParams.fShadingHeightFactor = mShadingHeightFactorET.getFloat();
        DtmVisualizationParams.fShadingLightSourceYaw = mShadingLightSourceYawET.getFloat();
        DtmVisualizationParams.fShadingLightSourcePitch = mShadingLightSourcePitchET.getFloat();
        DtmVisualizationParams.uHeightColorsTransparency = mHeightColorsTransparencyET.getByte();
        DtmVisualizationParams.uShadingTransparency = mShadingTransparencyET.getByte();
        DtmVisualizationParams.bHeightColorsInterpolation = mHeightColorsInterpolationCB.isChecked();
        DtmVisualizationParams.bDtmVisualizationAboveRaster = mDtmVisualizationAboveRasterCB.isChecked();
        //TODO add colors list
        //region add colors list
        //loop on table color
        List<IMcMapViewport.SHeigtColor> listColors = new ArrayList<>();
        try {
            for (int i = 0; i < mDtmVisualColorsLV.getCount(); i++) {
                if (mDtmVisualColorsLV.getChildAt(i) != null) {
                    int color = ((HeightColorItem) mDtmVisualColorsLV.getAdapter().getItem(i)).getmColor();
                    int alpha = ((HeightColorItem) mDtmVisualColorsLV.getAdapter().getItem(i)).getmAlpha();
                    int height = ((HeightColorItem) mDtmVisualColorsLV.getAdapter().getItem(i)).getmHeight();

                    SMcBColor mcBColor = new SMcBColor(Color.red(color),  Color.green(color),  Color.blue(color), alpha);
                    IMcMapViewport.SHeigtColor row = new IMcMapViewport.SHeigtColor();
                    row.Color = mcBColor;
                    row.nHeightInSteps = height;
                    listColors.add(row);
                }
            }
        } catch (Exception exp) {
        }
        DtmVisualizationParams.aHeightColors = listColors.toArray(new IMcMapViewport.SHeigtColor[listColors.size()]);
        return DtmVisualizationParams;

    }

    private void initDtmTransparencyWithoutRasterCB() {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    mDtmTransparencyWithoutRasterCB.setChecked(mViewport.GetDtmTransparencyWithoutRaster());
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetDtmTransparencyWithoutRaster");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });

    }

    private void initDtmVisualizationParams() {
        ObjectRef<Boolean> isEnable = new ObjectRef<>();
        IMcMapViewport.SDtmVisualizationParams dtmVisualizationParams = new IMcMapViewport.SDtmVisualizationParams();
        try {
            mViewport.GetDtmVisualization(isEnable, dtmVisualizationParams);
            mDtmVisualizationWithoutRasterEnable.setChecked(isEnable.getValue());
            loadDtmVisualization(dtmVisualizationParams);
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetDtmVisualization");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void inflateViews() {
        inflateEditTexts();
        inflateCheckBoxes();
    }

    private void inflateCheckBoxes() {
        mHeightColorsInterpolationCB = (CheckBox) mRootView.findViewById(R.id.dtm_visualization_height_colors_interpolation_cb);
        mDtmVisualizationAboveRasterCB = (CheckBox) mRootView.findViewById(R.id.dtm_visualization_above_raster_cb);
        mDtmVisualizationWithoutRasterEnable = (CheckBox) mRootView.findViewById(R.id.dtm_visualization_is_enabled_cb);
        mDtmTransparencyWithoutRasterCB = (CheckBox) mRootView.findViewById(R.id.dtm_visualization_id_dtm_transparency_without_raster_cb);
    }

    private void inflateEditTexts() {
        mDtmVisualizationWithoutRasterMinHeightET = (NumericEditTextLabel) mRootView.findViewById(R.id.dtm_visualization_default_height_colors_min_height);
        mDtmVisualizationWithoutRasterMinHeightET.setFloat(-500);
        mDtmVisualizationWithoutRasterMaxHeightET = (NumericEditTextLabel) mRootView.findViewById(R.id.dtm_visualization_default_height_colors_max_height);
        mDtmVisualizationWithoutRasterMaxHeightET.setFloat(9500);
        mHeightColorsHeightOriginET = (NumericEditTextLabel) mRootView.findViewById(R.id.dtm_visualization_height_color_height_origin);
        mHeightColorsHeightStepET = (NumericEditTextLabel) mRootView.findViewById(R.id.dtm_visualization_height_color_height_step);
        mShadingHeightFactorET = (NumericEditTextLabel) mRootView.findViewById(R.id.dtm_visualization_shading_height_factor);
        mShadingLightSourceYawET = (NumericEditTextLabel) mRootView.findViewById(R.id.dtm_visualization_shading_light_source_yaw);
        mShadingLightSourcePitchET = (NumericEditTextLabel) mRootView.findViewById(R.id.dtm_visualization_shading_light_source_pitch);
        mHeightColorsTransparencyET = (NumericEditTextLabel) mRootView.findViewById(R.id.dtm_visualization_height_color_transparency);
        mShadingTransparencyET = (NumericEditTextLabel) mRootView.findViewById(R.id.dtm_visualization_shading_transparency);
    }

    private void loadDtmVisualization(IMcMapViewport.SDtmVisualizationParams dtmVisualizationParams) {
        Log.d("loadDtmVisualization","fHeightColorsHeightOrigin:"+dtmVisualizationParams.fHeightColorsHeightOrigin);
        //mMinHeight = dtmVisualizationParams.;
        //mMaxHeight = 9500;
        mDtmVisualColorsLV = (ListView) mRootView.findViewById(R.id.dtm_visualization_height_colors_lv);
        loadColorsList(dtmVisualizationParams);
        //mDtmVisualizationWithoutRasterMinHeightET.setFloat(mMinHeight);
        //mDtmVisualizationWithoutRasterMaxHeightET.setFloat(mMaxHeight);
        mHeightColorsHeightOriginET.setFloat(dtmVisualizationParams.fHeightColorsHeightOrigin);
        mHeightColorsHeightStepET.setFloat(dtmVisualizationParams.fHeightColorsHeightStep);
        mShadingHeightFactorET.setFloat(dtmVisualizationParams.fShadingHeightFactor);
        mShadingLightSourceYawET.setFloat(dtmVisualizationParams.fShadingLightSourceYaw);
        mShadingLightSourcePitchET.setFloat(dtmVisualizationParams.fShadingLightSourcePitch);
        mHeightColorsTransparencyET.setByte(dtmVisualizationParams.uHeightColorsTransparency);
        mShadingTransparencyET.setByte(dtmVisualizationParams.uShadingTransparency);
        mHeightColorsInterpolationCB.setChecked(dtmVisualizationParams.bHeightColorsInterpolation);
        mDtmVisualizationAboveRasterCB.setChecked(dtmVisualizationParams.bDtmVisualizationAboveRaster);
    }

    private void loadColorsList(IMcMapViewport.SDtmVisualizationParams dtmVisualizationParams) {
        if(mDtmVisualColorsLV.getAdapter()!=null)
            ((HeightColorsRowAdapter)mDtmVisualColorsLV.getAdapter()).clear();
        ArrayList<HeightColorItem> heightColorItems = new ArrayList<>();
        if (dtmVisualizationParams.aHeightColors != null && dtmVisualizationParams.aHeightColors.length != 0) {
            mMinHeight = dtmVisualizationParams.fHeightColorsHeightOrigin +
                    dtmVisualizationParams.aHeightColors[0].nHeightInSteps *
                            dtmVisualizationParams.fHeightColorsHeightStep;
            mMaxHeight = dtmVisualizationParams.fHeightColorsHeightOrigin +
                    dtmVisualizationParams.aHeightColors[dtmVisualizationParams.aHeightColors.length - 1].nHeightInSteps *
                            dtmVisualizationParams.fHeightColorsHeightStep;
            IMcMapViewport.SHeigtColor[] arrColors = dtmVisualizationParams.aHeightColors;

            for (int i = 0; i < arrColors.length; i++) {
                SMcBColor mcColor = arrColors[i].Color;
                int color = Color.rgb(mcColor.r,mcColor.g, mcColor.b);
                heightColorItems.add(new HeightColorItem(color, mcColor.a, arrColors[i].nHeightInSteps));
            }


        }
        HeightColorsRowAdapter heightColorsRowAdapter = new HeightColorsRowAdapter(getContext(), R.layout.cv_height_colors_row, heightColorItems);
       // if (heightColorItems.isEmpty())
            //heightColorsRowAdapter.addEmptyValuesRow();
        mDtmVisualColorsLV.setAdapter(heightColorsRowAdapter);
        Funcs.setListViewHeightBasedOnChildren(mDtmVisualColorsLV);

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
    /*    if (context instanceof OnFragmentInteractionListener) {
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
        mViewport = (IMcMapViewport) obj;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mViewport));
    }

    public interface OnSetDtmVisualizationListener {
        void setDtmVisualization(IMcMapViewport.SDtmVisualizationParams sDtmVisualizationParams,boolean isChecked);
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
