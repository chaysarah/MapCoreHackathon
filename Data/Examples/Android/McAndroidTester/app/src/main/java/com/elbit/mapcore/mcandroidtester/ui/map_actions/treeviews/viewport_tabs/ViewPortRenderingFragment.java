package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.viewport_tabs;

import android.content.Context;
import android.graphics.Bitmap;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import androidx.appcompat.widget.AppCompatImageView;

import android.util.Size;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.Spinner;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.model.AMCTViewportRenderingObjectsDelay;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.DirectoryChooserDialog;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SpinnerWithLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.TwoDVector;

import java.io.File;
import java.io.FileOutputStream;
import java.nio.ByteBuffer;
import java.util.HashMap;

import com.elbit.mapcore.Classes.OverlayManager.McTexture;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.General.ObjectRef;
import com.elbit.mapcore.Interfaces.Map.IMcMapCamera;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcTexture;
import com.elbit.mapcore.Structs.SMcRect;
import com.elbit.mapcore.Structs.SMcVector2D;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link ViewPortRenderingFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link ViewPortRenderingFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class ViewPortRenderingFragment extends Fragment implements FragmentWithObject {

    private OnFragmentInteractionListener mListener;
    private IMcMapViewport mViewport;
    private View mRootView;
    private SpinnerWithLabel mObjDelayTypeSpinnerSWL;
    private CheckBox mDelayEnabledCB;
    private NumericEditTextLabel mNumToUpdatePerRenderET;
    private CheckBox mOverloadModeEnabledCB;
    private NumericEditTextLabel mMinNumItemForOverloadET;
    private NumericEditTextLabel mStaticObjectVisibilityMaxScaleET;
    private NumericEditTextLabel mObjectVisibilityMaxScaleET;
    private NumericEditTextLabel mThresholdInPixelsET;
    private NumericEditTextLabel mScreenSizeTerrainObjectsFactorET;
    private SpinnerWithLabel mBufferPixelFormatSWL;
    private NumericEditTextLabel mBufferRawPitchET;
    private NumericEditTextLabel mRenderScreenrectToBufferWidthET;
    private NumericEditTextLabel mRenderScreenrectToBufferHeightET;
    private TwoDVector mRenderScreenRectToBufferTL2D;
    private TwoDVector mRenderScreenRectToBufferBR2D;
    private AppCompatImageView mRenderScreenRectToBufferImg;
    private HashMap<IMcMapViewport.EObjectDelayType, AMCTViewportRenderingObjectsDelay> mDicObjectsDelayData;
    private IMcMapViewport.EObjectDelayType[] mEObjectDelayTypeValues;
    private IMcMapViewport.EObjectDelayType mLastDelayType;
    private CheckBox mRenderScreenRectToBuffersCB;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment ViewPortRenderingFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static ViewPortRenderingFragment newInstance() {
        ViewPortRenderingFragment fragment = new ViewPortRenderingFragment();
        return fragment;
    }

    public ViewPortRenderingFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        Funcs.SetObjectFromBundle(savedInstanceState, this );
        mRootView = inflater.inflate(R.layout.fragment_view_port_rendering, container, false);
        mDicObjectsDelayData = new HashMap<>();
        mEObjectDelayTypeValues = IMcMapViewport.EObjectDelayType.values();
        initViews();
        return mRootView;
    }

    private void initViews() {
        initEditTxts();
        initSpinners();
        initBttns();
        initCheckBoxes();

        initObjectDelayType();
        initMinNumItemsForOverload();
        initStaticObjectVisibility();
        initObjectVisibility();
        initThresholdInPixels();
        initScreenSizeTerrainObjectsFactor();
        initRenderScreenRectToBuffer();
        updateNumObjToUpdate();

        mRenderScreenRectToBufferImg = (AppCompatImageView) mRootView.findViewById(R.id.view_port_rendering_render_screen_img);
        mRenderScreenRectToBuffersCB = (CheckBox) mRootView.findViewById(R.id.view_port_rendering_render_screen_save_to_file_CB);
    }



    private void initRenderScreenRectToBuffer() {
        mRenderScreenRectToBufferTL2D = (TwoDVector) mRootView.findViewById(R.id.view_port_rendering_top_left);
        mRenderScreenRectToBufferBR2D = (TwoDVector) mRootView.findViewById(R.id.view_port_rendering_bottom_right);

        ArrayAdapter< IMcTexture.EPixelFormat> bufferPixelFormatAdapter = new ArrayAdapter<>(getContext(), android.R.layout.simple_spinner_item, IMcTexture.EPixelFormat.values());
        mBufferPixelFormatSWL.setAdapter(bufferPixelFormatAdapter);
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    ObjectRef<IMcTexture.EPixelFormat> defualtFormat = new ObjectRef<>();
                    mViewport.GetRenderToBufferNativePixelFormat(defualtFormat);
                    final IMcTexture.EPixelFormat format = defualtFormat.getValue();
                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            mBufferPixelFormatSWL.setSelection(format.getValue());
                        }
                    });
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetRenderToBufferNativePixelFormat");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });




        ObjectRef<Integer> widthDimension = new ObjectRef<>();
        ObjectRef<Integer> heightDimension = new ObjectRef<>();
        mRenderScreenRectToBufferTL2D.setVector2D(new SMcVector2D(0, 0));

        try {
            mViewport.GetViewportSize(widthDimension, heightDimension);
            mRenderScreenrectToBufferWidthET.setUInt(widthDimension.getValue());
            mRenderScreenrectToBufferHeightET.setUInt(heightDimension.getValue());
            SMcVector2D vec = new SMcVector2D(widthDimension.getValue(), heightDimension.getValue());
            mRenderScreenRectToBufferBR2D.setVector2D(vec);
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetViewportSize");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initScreenSizeTerrainObjectsFactor() {
        try {
            if (mViewport.GetMapType() == IMcMapCamera.EMapType.EMT_3D)
                mScreenSizeTerrainObjectsFactorET.setFloat(mViewport.GetScreenSizeTerrainObjectsFactor());
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetScreenSizeTerrainObjectsFactor");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initThresholdInPixels() {
        try {
            mThresholdInPixelsET.setUInt(mViewport.GetObjectsMovementThreshold());
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetObjectsMovementThreshold");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initObjectVisibility() {
        try {
            mObjectVisibilityMaxScaleET.setFloat(mViewport.GetObjectsVisibilityMaxScale());
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetObjectsVisibilityMaxScale");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initStaticObjectVisibility() {
        try {
            mStaticObjectVisibilityMaxScaleET.setFloat(mViewport.GetVector3DExtrusionVisibilityMaxScale());
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetVector3DExtrusionVisibilityMaxScale");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initEditTxts() {
        mNumToUpdatePerRenderET = (NumericEditTextLabel) mRootView.findViewById(R.id.view_port_rendering_num_obj_to_update);
        mMinNumItemForOverloadET = (NumericEditTextLabel) mRootView.findViewById(R.id.view_port_rendering_min_num_item_for_overload);
        mStaticObjectVisibilityMaxScaleET = (NumericEditTextLabel) mRootView.findViewById(R.id.view_port_rendering_static_object_visibility_max_scale);
        mObjectVisibilityMaxScaleET = (NumericEditTextLabel) mRootView.findViewById(R.id.view_port_rendering_object_visibility_max_scale);
        mScreenSizeTerrainObjectsFactorET = (NumericEditTextLabel) mRootView.findViewById(R.id.view_port_rendering_screen_size_terrain_obj_factor);
        mBufferRawPitchET = (NumericEditTextLabel) mRootView.findViewById(R.id.view_port_rendering_buffer_raw_pitch_et);
        mRenderScreenrectToBufferWidthET = (NumericEditTextLabel) mRootView.findViewById(R.id.view_port_rendering_width_et);
        mRenderScreenrectToBufferHeightET = (NumericEditTextLabel) mRootView.findViewById(R.id.view_port_rendering_bottom_height_et);
    }

    private void initCheckBoxes() {
        mDelayEnabledCB = (CheckBox) mRootView.findViewById(R.id.view_port_rendering_delay_enabled_cb);
        mOverloadModeEnabledCB = (CheckBox) mRootView.findViewById(R.id.view_port_rendering_overload_mode_enabled_cb);
    }

    private void initBttns() {
        initSaveBttn();

        Button renderBttn = (Button) mRootView.findViewById(R.id.view_port_rendering_render_screen_bttn);
        renderBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveRenderScreenRectToBuffer();
            }
        });
    }

    private void initMinNumItemsForOverload() {
        ObjectRef<Boolean> overloadEnabled = new ObjectRef<>();
        ObjectRef<Integer> minNumItem = new ObjectRef<>();;
        try {
            mViewport.GetOverloadMode(overloadEnabled, minNumItem);
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetOverloadMode");
        } catch (Exception e) {
            e.printStackTrace();
        }

        mOverloadModeEnabledCB.setChecked(overloadEnabled.getValue());
        mMinNumItemForOverloadET.setUInt(minNumItem.getValue());
    }

    private void updateNumObjToUpdate() {
        try {
            saveHashMapObjDelayType();
            IMcMapViewport.EObjectDelayType delayType = mEObjectDelayTypeValues[mObjDelayTypeSpinnerSWL.getSelectedItemPosition()];
            mLastDelayType = delayType;
            if(mDicObjectsDelayData.containsKey(mLastDelayType))
            {
                boolean renderingEnabled = mDicObjectsDelayData.get(mLastDelayType).isRenderingEnabled();
                int numToUpdate = mDicObjectsDelayData.get(mLastDelayType).getNumToUpdate();
                SetDelayObjects(renderingEnabled, numToUpdate);
            }
            else
            {
                ObjectRef<Boolean> renderingEnabled = new ObjectRef<>();
                ObjectRef<Integer> numToUpdate = new ObjectRef<>();
                mViewport.GetObjectsDelay(delayType, renderingEnabled, numToUpdate);
                SetDelayObjects(renderingEnabled.getValue(), numToUpdate.getValue());
            }

        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetObjectsDelay");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void SetDelayObjects(boolean renderingEnabled, int numToUpdate)
    {
        mDelayEnabledCB.setChecked(renderingEnabled);
        mNumToUpdatePerRenderET.setUInt(numToUpdate);
    }

    private void saveHashMapObjDelayType()
    {
        if(mLastDelayType!= null) {
            mDicObjectsDelayData.put(mLastDelayType,
                    new AMCTViewportRenderingObjectsDelay(mDelayEnabledCB.isChecked(), mNumToUpdatePerRenderET.getUInt()));
        }
    }

    private void initSaveBttn() {
        Button saveBttn = (Button) mRootView.findViewById(R.id.view_port_rendering_save_bttn);
        saveBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveObjDelayType();
                //saveRenderScreenRectToBuffer();
            }
        });
    }

    private void saveRenderScreenRectToBuffer()
    {
        Integer pixelFormatByteCount = 0;
        IMcTexture.EPixelFormat pixelFormat = IMcTexture.EPixelFormat.EPF_A1R5G5B5;
        ObjectRef<IMcTexture.EPixelFormat> defualtFormat = new ObjectRef<>();
        try {
            mViewport.GetRenderToBufferNativePixelFormat(defualtFormat);
            pixelFormat = (IMcTexture.EPixelFormat) mBufferPixelFormatSWL.getSelectedItem();
            pixelFormatByteCount = McTexture.Static.GetPixelFormatByteCount(pixelFormat);
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetPixelFormatByteCount");
        } catch (Exception e) {
            e.printStackTrace();
        }


        Integer stride = 0;
        if (mBufferRawPitchET.getUInt() == 0)
            stride = mRenderScreenrectToBufferWidthET.getUInt() * pixelFormatByteCount;
        else
            stride = mBufferRawPitchET.getUInt() * pixelFormatByteCount;

        final int bufferSize = (int) stride * mRenderScreenrectToBufferHeightET.getUInt();

        final IMcTexture.EPixelFormat finalPixelFormat= pixelFormat;
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    Bitmap.Config pixFormat;

                    switch (finalPixelFormat) {
                        case EPF_R5G6B5:
                        case EPF_A1R5G5B5:
                        case EPF_B5G6R5:
                            pixFormat = Bitmap.Config.RGB_565;
                            break;
                        case EPF_R8G8B8:
                        case EPF_B8G8R8:
                        case EPF_X8B8G8R8:
                        case EPF_X8R8G8B8:
                        case EPF_A8R8G8B8:
                        case EPF_A8B8G8R8:
                        case EPF_B8G8R8A8:
                        case EPF_R8G8B8A8:
                            pixFormat = Bitmap.Config.ARGB_8888;
                            break;
                        case EPF_A4R4G4B4:
                            pixFormat = Bitmap.Config.ARGB_4444;
                            break;
                        case EPF_A8:
                            pixFormat = Bitmap.Config.ALPHA_8;
                            break;
                        default:
                            AlertMessages.ShowErrorMessage(getContext(), "Pixel Format", "Pixel format not supported");
                            return;
                    }
                    byte[] arrBuffer = new byte[bufferSize];
                    int width = mRenderScreenrectToBufferWidthET.getUInt();
                    int height = mRenderScreenrectToBufferHeightET.getUInt();
                    SMcRect rect = new SMcRect((int) mRenderScreenRectToBufferTL2D.getmX(),
                            (int) mRenderScreenRectToBufferTL2D.getmY(),
                            (int) mRenderScreenRectToBufferBR2D.getmX(),
                            (int) mRenderScreenRectToBufferBR2D.getmY());

                    mViewport.RenderScreenRectToBuffer(rect,
                            width,
                            height,
                            finalPixelFormat,
                            mBufferRawPitchET.getUInt(),
                            arrBuffer);

                    if (mRenderScreenrectToBufferWidthET.getUInt() >= 1 && mRenderScreenrectToBufferHeightET.getUInt() >= 1) {
                        final Size imageSize = new Size(width,height);
                        final byte[] arrBuffer2 = arrBuffer;
                        final Bitmap.Config finalPixFormat = pixFormat;
                        final Context context = getContext();
                        getActivity().runOnUiThread(new Runnable() {
                            @Override
                            public void run() {

                                Bitmap bitmap = Bitmap.createBitmap(imageSize.getWidth(), imageSize.getHeight(), finalPixFormat);
                                ByteBuffer buffer = ByteBuffer.wrap(arrBuffer2);
                                bitmap.copyPixelsFromBuffer(buffer);
                                mRenderScreenRectToBufferImg.setImageBitmap(bitmap);
                                final Bitmap finalBitmap = bitmap;
                                if(mRenderScreenRectToBuffersCB.isChecked())
                                {
                                    DirectoryChooserDialog directoryChooserDialog =
                                            new DirectoryChooserDialog(context,
                                                    new DirectoryChooserDialog.ChosenDirectoryListener() {
                                                        @Override
                                                        public void onChosenDir(String chosenDir) {
                                                            try {
                                                                FileOutputStream out = new FileOutputStream(chosenDir);
                                                                File file = new File(chosenDir);
                                                                String extension = file.getAbsolutePath().substring(file.getAbsolutePath().lastIndexOf(".")+1);
                                                                Bitmap.CompressFormat format = Bitmap.CompressFormat.PNG;
                                                                if(extension.toLowerCase().compareTo("jpeg") == 0 || extension.toLowerCase().compareTo("jpg") == 0)
                                                                    format = Bitmap.CompressFormat.JPEG;
                                                                finalBitmap.compress(format, 90, out);
                                                                out.flush();
                                                                out.close();
                                                            } catch (Exception e) {
                                                                e.printStackTrace();
                                                            }

                                                        }
                                                    }, true, true);
                                    // Load directory chooser dialog for initial 'mChosenDir' directory.
                                    // The registered callback will be called upon final directory selection.
                                    directoryChooserDialog.chooseDirectory("");

                                }
                            }
                        });
                    }
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "RenderScreenRectToBuffer");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });

    }


   /* public void saveModelBuffer(String panoramaName) {
        try {
            Size imageSize = new Size(200,200);
            byte[] modelBuffer = m_activeViewport.renderScreenRectToBuffer(imageSize.getWidth(), imageSize.getHeight());
            Bitmap bitmap = Bitmap.createBitmap(imageSize.getWidth(), imageSize.getHeight(), Bitmap.Config.ARGB_8888);
            ByteBuffer buffer = ByteBuffer.wrap(modelBuffer);
            bitmap.copyPixelsFromBuffer(buffer);
          //  BitmapUtil.save(bitmap, AnalyticsManager.GLOBAL_ANALYZING_DIRECTORY_PATH + "/" +panoramaName+"_unregistered_model.bmp");

        } catch (IOException e) {
            e.printStackTrace();
        }
*/

        public int[] convert(byte buf[]) {
        int intArr[] = new int[buf.length / 4];
        int offset = 0;
        for(int i = 0; i < intArr.length; i++) {
            intArr[i] = (buf[3 + offset] & 0xFF) | ((buf[2 + offset] & 0xFF) << 8) |
                    ((buf[1 + offset] & 0xFF) << 16) | ((buf[0 + offset] & 0xFF) << 24);
            offset += 4;
        }
        return intArr;
    }

    private void saveObjDelayType() {
        saveHashMapObjDelayType();
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    for (IMcMapViewport.EObjectDelayType delayType : mDicObjectsDelayData.keySet()) {
                        AMCTViewportRenderingObjectsDelay viewportRenderingObjectsDelay = mDicObjectsDelayData.get(delayType);
                        mViewport.SetObjectsDelay(delayType, viewportRenderingObjectsDelay.isRenderingEnabled(),
                                viewportRenderingObjectsDelay.getNumToUpdate());
                    }
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetObjectsDelay");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void initSpinners() {
        mObjDelayTypeSpinnerSWL = (SpinnerWithLabel) mRootView.findViewById(R.id.view_port_rendering_object_delay_type);
        mBufferPixelFormatSWL = (SpinnerWithLabel) mRootView.findViewById(R.id.view_port_rendering_buffer_pixel_format);
    }

    private void initObjectDelayType() {
        ArrayAdapter<IMcMapViewport.EObjectDelayType> ObjDelayTypeAdapter = new ArrayAdapter<>(getContext(), android.R.layout.simple_spinner_item, IMcMapViewport.EObjectDelayType.values());
        mObjDelayTypeSpinnerSWL.setAdapter(ObjDelayTypeAdapter);
        Spinner objDelayTypeSpinner = (Spinner) mObjDelayTypeSpinnerSWL.findViewById(R.id.spinner_in_cv);
        objDelayTypeSpinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                updateNumObjToUpdate();
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });
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
