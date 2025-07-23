package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.viewport_tabs;

import android.content.Context;
import android.graphics.Color;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.Spinner;

import com.elbit.mapcore.General.ObjectRef;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_MCGridCoordinateSystem;
import com.elbit.mapcore.mcandroidtester.ui.adapters.HashMapAdapter;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

import com.elbit.mapcore.mcandroidtester.utils.colorpicker.ColorPickerDialog;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SpinnerWithLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.WorldBoundingBox;

import java.util.Map;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Structs.SMcBColor;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link ViewPortDetailsFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link ViewPortDetailsFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class ViewPortDetailsFragment extends Fragment implements FragmentWithObject {
    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;

    private OnFragmentInteractionListener mListener;
    private IMcMapViewport mViewport;
    private EditText mMapType;
    private EditText mViewPortId;
    private EditText mWindowHanlde;
    private EditText mViewportHeight;
    private EditText mOneBitAlphaMode;
    private CheckBox mTransparencyOrderingMode;
    private EditText mSpatialPartitionNum;
    private CheckBox mFullScreenMode;
    private CheckBox mGridVisibility;
    private CheckBox mGridAboveVectorLayers;
    private EditText mViewportWidth;
    private Button mAddPostProcessBttn;
    private Button mDeletePostProcessBttn;
    private View mRootView;
    private EditText mPostProcessEt;
    private Button mBgColorBttn;
    private Button mSaveBttn;
    private HashMapAdapter mGridCoordSysAdapter;
    private ListView mGridCoordSysLV;
    private CheckBox mHeightLinesCB;
    private EditText mBrightnessET;
    private float imageProcAll, imageProcRasterLayers, imageProcWithoutObjects;
    private SpinnerWithLabel mBrightnessSWL;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment ViewPortDetailsFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static ViewPortDetailsFragment newInstance(String param1, String param2) {
        ViewPortDetailsFragment fragment = new ViewPortDetailsFragment();
        Bundle args = new Bundle();
        args.putString(ARG_PARAM1, param1);
        args.putString(ARG_PARAM2, param2);
        fragment.setArguments(args);
        return fragment;
    }

    public ViewPortDetailsFragment() {
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
        setTitle();
        mRootView = inflater.inflate(R.layout.fragment_view_port_details, container, false);
        initSaveBttn();
        initGeneralDetailsViews();
        initPostProcessViews();
        initBgColorViews();
        loadCoordinateSysList();
        initHeightLines();
        initWorldBoundingBox();
        initBrightness();
        // Inflate the layout for this fragment
        return mRootView;
    }

    private void setTitle() {
        getActivity().setTitle("view port details ");

    }
    @Override
    public void onHiddenChanged(boolean hidden) {
        super.onHiddenChanged(hidden);
        if(!hidden)
            setTitle();
    }
    private void initBrightness() {
        mBrightnessET = (EditText) mRootView.findViewById(R.id.view_port_details_brightness_et);
        mBrightnessET.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
                try {
                    String sVal = String.valueOf(s);
                    if (s != null && !sVal.isEmpty() && !sVal.equals("-"))
                        Float.valueOf(sVal);
                }
                catch (NumberFormatException ex)
                {
                    AlertMessages.ShowErrorMessage(getContext(),"Number Format Exception","Invalid brightness value");
                }
            }

            @Override
            public void afterTextChanged(Editable s) {
                String sVal = String.valueOf(s);
                if (s != null && !sVal.isEmpty() && !sVal.equals("-"))
                    saveBrightnessETVal(Float.valueOf(sVal));
            }
        });
        mBrightnessSWL = (SpinnerWithLabel) mRootView.findViewById(R.id.view_port_details_image_processing_stage_swl);
        mBrightnessSWL.setAdapter(new ArrayAdapter<>(getContext(), android.R.layout.simple_spinner_item, IMcMapViewport.EImageProcessingStage.values()));
        Spinner brightnessSpinner = (Spinner) mBrightnessSWL.findViewById(R.id.spinner_in_cv);
        brightnessSpinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                IMcMapViewport.EImageProcessingStage imageProc = IMcMapViewport.EImageProcessingStage.values()[position];
                switch (imageProc) {
                    case EIPS_ALL:
                        mBrightnessET.setText(String .valueOf(imageProcAll));
                        break;
                    case EIPS_RASTER_LAYERS:
                        mBrightnessET.setText(String .valueOf(imageProcRasterLayers));
                        break;
                    case EIPS_WITHOUT_OBJECTS:
                        mBrightnessET.setText(String .valueOf(imageProcWithoutObjects));
                        break;
                }
            }
            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });

        try {
            imageProcAll=mViewport.GetBrightness(IMcMapViewport.EImageProcessingStage.EIPS_ALL);
            imageProcRasterLayers=mViewport.GetBrightness(IMcMapViewport.EImageProcessingStage.EIPS_RASTER_LAYERS);
            imageProcWithoutObjects=mViewport.GetBrightness(IMcMapViewport.EImageProcessingStage.EIPS_WITHOUT_OBJECTS);
            mBrightnessSWL.setSelection(IMcMapViewport.EImageProcessingStage.EIPS_ALL.getValue());
            //mBrightnessET.setText(String.valueOf(imageProcAll));
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetBrightness");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void saveBrightnessETVal(Float aFloat) {
        IMcMapViewport.EImageProcessingStage imageProc = (IMcMapViewport.EImageProcessingStage) mBrightnessSWL.getSelectedItem();
        switch (imageProc)
        {
            case EIPS_ALL:
                imageProcAll = aFloat;
                break;
            case EIPS_RASTER_LAYERS:
                imageProcRasterLayers =aFloat;
                break;
            case EIPS_WITHOUT_OBJECTS:
                imageProcWithoutObjects = aFloat;
                break;
        }
    }


    private void initWorldBoundingBox() {
        WorldBoundingBox worldBoundingBox = (WorldBoundingBox) mRootView.findViewById(R.id.vieW_port_details_world_bounding_box);
        try {
            worldBoundingBox.initWorldBoundingBox(mViewport.GetTerrainsBoundingBox());
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetTerrainsBoundingBox");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initHeightLines() {
        mHeightLinesCB = (CheckBox) mRootView.findViewById(R.id.view_port_details_height_lines_visibility_cb);
        try {
            mHeightLinesCB.setChecked(mViewport.GetHeightLinesVisibility());
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetHeightLinesVisibility");
        } catch (Exception e) {
            e.printStackTrace();
        }
        mHeightLinesCB.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, final boolean isChecked) {
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            mViewport.SetHeightLinesVisibility(isChecked);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetHeightLinesVisibility");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });
    }

    private void initSaveBttn() {
        mSaveBttn = (Button) mRootView.findViewById(R.id.view_port_details_save_bttn);
        mSaveBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveEditTxtsValues();
                saveBrightnessValues();
            }
        });
    }

    private void saveBrightnessValues() {

           /* IMcMapViewport.EImageProcessingStage imageProc = (IMcMapViewport.EImageProcessingStage) mBrightnessSWL.getSelectedItem();
            switch (imageProc)
            {
                case EIPS_ALL:
                    imageProcAll = Float.valueOf(mBrightnessET.getText().toString());
                    break;
                case EIPS_RASTER_LAYERS:
                    imageProcRasterLayers = Float.valueOf(mBrightnessET.getText().toString());
                    break;
                case EIPS_WITHOUT_OBJECTS:
                    imageProcWithoutObjects = Float.valueOf(mBrightnessET.getText().toString());
                    break;
            }*/
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    mViewport.SetBrightness(IMcMapViewport.EImageProcessingStage.EIPS_ALL, imageProcAll);
                    mViewport.SetBrightness(IMcMapViewport.EImageProcessingStage.EIPS_RASTER_LAYERS, imageProcRasterLayers);
                    mViewport.SetBrightness(IMcMapViewport.EImageProcessingStage.EIPS_WITHOUT_OBJECTS, imageProcWithoutObjects);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetBrightness");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }


    //region General Details
    private void initGeneralDetailsViews() {
        initGeneralDetailsEditTxts();
        initGeneralDetailsCB();
    }

    private void initGeneralDetailsCB() {
        mFullScreenMode = (CheckBox) mRootView.findViewById(R.id.view_port_details_full_screen_mode);
        mGridVisibility = (CheckBox) mRootView.findViewById(R.id.view_port_details_grid_visibility);
        mGridAboveVectorLayers = (CheckBox) mRootView.findViewById(R.id.view_port_details_grid_above_vector_layers);

        try {
            mFullScreenMode.setChecked(mViewport.GetFullScreenMode());
            mGridVisibility.setChecked(mViewport.GetGridVisibility());
            mGridAboveVectorLayers.setChecked(mViewport.GetGridAboveVectorLayers());

            mFullScreenMode.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
                @Override
                public void onCheckedChanged(CompoundButton buttonView, final boolean isChecked) {
                    Funcs.runMapCoreFunc(new Runnable() {
                                             @Override
                                             public void run() {
                                                 try {
                                                     mViewport.SetFullScreenMode(isChecked);
                                                 } catch (MapCoreException e) {
                                                     AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetFullScreenMode");
                                                 } catch (Exception e) {
                                                     e.printStackTrace();
                                                 }
                                             }
                                         }
                    );

                }
            });

            mGridVisibility.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
                @Override
                public void onCheckedChanged(CompoundButton buttonView, final boolean isChecked) {
                  Funcs.runMapCoreFunc(new Runnable() {
                      @Override
                      public void run() {
                          try {
                              mViewport.SetGridVisibility(isChecked);
                          } catch (MapCoreException e) {
                              AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetGridVisibility");
                          } catch (Exception e) {
                              e.printStackTrace();
                          }
                      }
                  });

                }
            });

            mGridAboveVectorLayers.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
                @Override
                public void onCheckedChanged(CompoundButton buttonView, final boolean isChecked) {
                    Funcs.runMapCoreFunc(new Runnable() {
                        @Override
                        public void run() {
                            try {
                                mViewport.SetGridAboveVectorLayers(isChecked);
                            } catch (MapCoreException e) {
                                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetGridAboveVectorLayers");
                            } catch (Exception e) {
                                e.printStackTrace();
                            }
                        }
                    });

                }
            });
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "FullScreenMode");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initGeneralDetailsEditTxts() {
        mMapType = (EditText) mRootView.findViewById(R.id.view_port_details_map_type);
        mViewPortId = (EditText) mRootView.findViewById(R.id.view_port_details_view_port_id);
        mWindowHanlde = (EditText) mRootView.findViewById(R.id.view_port_details_window_handle);
        mViewportHeight = (EditText) mRootView.findViewById(R.id.view_port_details_viewport_height);
        mViewportWidth = (EditText) mRootView.findViewById(R.id.view_port_details_viewport_width);
        mOneBitAlphaMode = (EditText) mRootView.findViewById(R.id.view_port_details_one_bit_alpha_mode);
        mTransparencyOrderingMode = (CheckBox) mRootView.findViewById(R.id.view_port_details_transparency_ordering_mode);
        mSpatialPartitionNum = (EditText) mRootView.findViewById(R.id.view_port_details_spatial_partition_num);
        try {
            mMapType.setText(String.valueOf(mViewport.GetMapType()));
            mViewPortId.setText(String.valueOf(mViewport.GetViewportID()));
            mWindowHanlde.setText(String.valueOf(mViewport.GetWindowHandle()));
            ObjectRef<Integer> width = new ObjectRef<Integer>(-1);
            ObjectRef<Integer> height = new ObjectRef<Integer>(-1);
            mViewport.GetViewportSize(width, height);
            mViewportWidth.setText(String.valueOf(width.getValue()));
            mViewportHeight.setText(String.valueOf(height.getValue()));
            mOneBitAlphaMode.setText(String.valueOf(mViewport.GetOneBitAlphaMode()));
            mTransparencyOrderingMode.setChecked(mViewport.GetTransparencyOrderingMode());
            mSpatialPartitionNum.setText(String.valueOf(mViewport.GetSpatialPartitionNumCacheNodes()));
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GeneralDetails");
        } catch (Exception e) {
            e.printStackTrace();
        }

    }
    //endregion

    //region Post Process
    private void initPostProcessViews() {
        initPostProcessET();
        initAddPostProcessBttn();
        initDeletePostProcessBttn();
    }

    private void initPostProcessET() {
        mPostProcessEt = (EditText) mRootView.findViewById(R.id.view_port_details_post_process);
    }

    private void initDeletePostProcessBttn() {
        mDeletePostProcessBttn = (Button) mRootView.findViewById(R.id.view_port_details_post_process_delete_bttn);
        mDeletePostProcessBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final String postProcess = mPostProcessEt.getText().toString();
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            mViewport.RemovePostProcess(postProcess);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "mViewport.RemovePostProcess");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });
    }

    private void initAddPostProcessBttn() {
        mAddPostProcessBttn = (Button) mRootView.findViewById(R.id.view_port_details_post_process_add_bttn);
        mAddPostProcessBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final String postProcess = mPostProcessEt.getText().toString();
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            mViewport.AddPostProcess(postProcess);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "mViewport.AddPostProcess");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });

            }
        });
    }
    //endregion

    //region Background color
    private void initBgColorViews() {
        mBgColorBttn = (Button) mRootView.findViewById(R.id.view_port_details_bg_color_bttn);
        try {
            SMcBColor backgroundColor = mViewport.GetBackgroundColor();
            mBgColorBttn.setTextColor(Color.argb(backgroundColor.a, backgroundColor.r, backgroundColor.g, backgroundColor.b));

        } catch (Exception e) {
            e.printStackTrace();
        }


        mBgColorBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                ColorPickerDialog colorPickerDialog = new ColorPickerDialog(getActivity(), 0, new ColorPickerDialog.OnColorSelectedListener() {

                    @Override
                    public void onColorSelected(int color) {
                        final SMcBColor backgroundColor = new SMcBColor();
                        backgroundColor.r = (byte) Color.red(color);
                        backgroundColor.g = (byte) Color.green(color);
                        backgroundColor.b = (byte) Color.blue(color);
                        backgroundColor.a = (byte) Color.alpha(color);

                        Funcs.runMapCoreFunc(new Runnable() {
                            @Override
                            public void run() {
                                try {
                                    mViewport.SetBackgroundColor(backgroundColor);
                                } catch (MapCoreException e) {
                                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "mViewport.SetBackgroundColor");
                                } catch (Exception e) {
                                    e.printStackTrace();
                                }
                            }
                        });

                        mBgColorBttn.setTextColor(color);
                    }

                });
                colorPickerDialog.show();
            }
        });
    }
    //endregion

    private void loadCoordinateSysList() {
        mGridCoordSysAdapter = new HashMapAdapter(getActivity(), Manager_MCGridCoordinateSystem.getInstance().getdGridCoordSys(), Consts.ListType.SINGLE_CHECK);
        if (Manager_MCGridCoordinateSystem.getInstance().getdGridCoordSys() != null) {
            mGridCoordSysLV = (ListView) mRootView.findViewById(R.id.view_port_details_grid_coord_sys);
            mGridCoordSysLV.setChoiceMode(ListView.CHOICE_MODE_SINGLE);
            mGridCoordSysLV.setAdapter(null);
            mGridCoordSysLV.setAdapter(mGridCoordSysAdapter);
            mGridCoordSysLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                @Override
                public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                    Map.Entry<Object, Integer> item = (Map.Entry<Object, Integer>) mGridCoordSysLV.getAdapter().getItem(position);
                    //show the selected coordinate system corresponding params (datum etc.)
                    // showSelectedCoordSysParams((IMcGridCoordinateSystem) item.getKey());
                }
            });
        }
        selectCurrGridCoordSys();
        mGridCoordSysLV.deferNotifyDataSetChanged();
        Funcs.setListViewHeightBasedOnChildren(mGridCoordSysLV);
    }

    private void selectCurrGridCoordSys() {
        {
            int i;
            for (i = 0; i < mGridCoordSysAdapter.getCount(); i++) {
                try {
                    if (mGridCoordSysAdapter.getItem(i).getKey().equals(mViewport.GetCoordinateSystem()))
                        mGridCoordSysLV.setItemChecked(i, true);
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }

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
    public void setObject(Object obj) {
        mViewport = (IMcMapViewport) obj;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mViewport));
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

    @Override
    public void onResume() {
        super.onResume();
        if (!getUserVisibleHint()) {
            //  saveEditTxtsValues();
            return;
        }
    }

    private void saveEditTxtsValues() {

        if (mOneBitAlphaMode != null) {
            final byte oneBitAlphaMode = Byte.decode(mOneBitAlphaMode.getText().toString());
            Funcs.runMapCoreFunc(new Runnable() {
                @Override
                public void run() {
                    try {
                        mViewport.SetOneBitAlphaMode(oneBitAlphaMode);
                    } catch (MapCoreException e) {
                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetOneBitAlphaMode");
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            });
        }
        if (mTransparencyOrderingMode != null) {
            final boolean transparencyOrderingMode = mTransparencyOrderingMode.isChecked();
            Funcs.runMapCoreFunc(new Runnable() {
                @Override
                public void run() {
                    try {
                        mViewport.SetTransparencyOrderingMode(transparencyOrderingMode);
                    } catch (MapCoreException e) {
                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetTransparencyOrderingMode");
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            });
        }
        if (mSpatialPartitionNum != null) {

            final int spatialPartitionNumCacheNodes = Integer.valueOf(mSpatialPartitionNum.getText().toString());
            Funcs.runMapCoreFunc(new Runnable() {
                @Override
                public void run() {
                    try {
                        mViewport.SetSpatialPartitionNumCacheNodes(spatialPartitionNumCacheNodes);
                    } catch (MapCoreException e) {
                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetSpatialPartitionNumCacheNodes");
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            });
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
}
