package com.elbit.mapcore.mcandroidtester.ui.objects.fragments.properties_tabs;

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
import android.widget.CompoundButton;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcClosedShapeItem;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.ui.objects.ObjectPropertiesBase;
import com.elbit.mapcore.mcandroidtester.ui.objects.fragments.create_texture_tabs.CreateTextureTabsFragment;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SelectColor;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SpinnerWithLabel;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcLineBasedItem;
import com.elbit.mapcore.mcandroidtester.utils.customviews.TwoDFVector;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link ObjectPropertiesTabsLineBasedFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link ObjectPropertiesTabsLineBasedFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class ObjectPropertiesTabsLineBasedFragment extends Fragment {

    private OnFragmentInteractionListener mListener;
    private View mRootView;
    private NumericEditTextLabel mLineWidthNET;
    private Button mSaveBttn;
    private SpinnerWithLabel mLineStyleSpinner;
    private SelectColor mLineColor;
    private CheckBox mLineTextureCB;
    private Button mLineTextureBttn;
    private NumericEditTextLabel mLineSmoothingLevels;
    private NumericEditTextLabel mLineBaseGreatCirclePrecision;
    private NumericEditTextLabel mOutlineWidthNET;
    private SelectColor mLineOutlineColor;
    private NumericEditTextLabel mTextureScale;
    private SpinnerWithLabel mShapeTypeSWL;
    private SpinnerWithLabel mSidesFillStyleSWL;
    private SelectColor mSidesFillColorSC;
    private NumericEditTextLabel mVerticalHeightNET;
    private Button mClosedShapeSidesTexturebttn;
    private CheckBox mFillTextureSidesNoneCb;
    private TwoDFVector mSidesFillTextureScale;
    private SpinnerWithLabel mFillStyleSWL;
    /**
     * Use this factory method to create a new instance of
     * @return A new instance of fragment ObjectPropertiesTabsLineBasedFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static ObjectPropertiesTabsLineBasedFragment newInstance() {
        ObjectPropertiesTabsLineBasedFragment fragment = new ObjectPropertiesTabsLineBasedFragment();
        return fragment;
    }

    public ObjectPropertiesTabsLineBasedFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
     }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        mRootView = inflater.inflate(R.layout.fragment_object_properties_tabs_line_based, container, false);
        initViews();
        return mRootView;
    }

    private void initViews() {
        inflateViews();
        initSaveBttn();
        initLineWidth();
        initLineStyle();
        initLineColor();
        initLineTexture();
        initLineSmoothingLevels();
        initGreatCirclePrecision();
        initOutlineColor();
        initOutlineWidth();
        initShapeType();
        initSidesFillTextureScale();
        initSidesFillStyle();
        initSidesFillColor();
        initSidesFillTexture();
        initVerticalHeight();
        initFillStyle();
    }

    private void initSidesFillTextureScale() {
        mSidesFillTextureScale=(TwoDFVector)mRootView.findViewById(R.id.line_based_sides_fill_texture_scale);
        mSidesFillTextureScale.setVector2D(ObjectPropertiesBase.mSidesFillTextureScale);
    }

    private void initSidesFillTexture() {
        if (ObjectPropertiesBase.mSidesFillTexture == null)
            mFillTextureSidesNoneCb.setChecked(true);
        else
            mFillTextureSidesNoneCb.setChecked(false);
        mClosedShapeSidesTexturebttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                CreateTextureTabsFragment createTextureTabsFragment;
                if (ObjectPropertiesBase.mSidesFillTexture == null) {
                    createTextureTabsFragment = CreateTextureTabsFragment.newInstance();
                    createTextureTabsFragment.setCurrentTexture(null);
                } else {
                    createTextureTabsFragment = CreateTextureTabsFragment.newInstance();
                    createTextureTabsFragment.setCurrentTexture(ObjectPropertiesBase.mSidesFillTexture);
                }

                FragmentTransaction transaction = getChildFragmentManager().beginTransaction().add(R.id.closed_shape_texture_container, createTextureTabsFragment, Consts.TextureFragmentsTags.TEXTURE_FROM_CLOSED_SHAPE_SIDES);
                transaction.addToBackStack(Consts.TextureFragmentsTags.TEXTURE_FROM_CLOSED_SHAPE_SIDES);
                transaction.commit();
                //ObjectPropertiesBase.mLineTexture=(McTexture)createTextureTabsFragment.getmCurrentTexture();
            }
        });
        mFillTextureSidesNoneCb.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                if (mFillTextureSidesNoneCb.isChecked()) {
                    mClosedShapeSidesTexturebttn.setEnabled(false);
                    ObjectPropertiesBase.mSidesFillTexture = null;
                } else
                    mClosedShapeSidesTexturebttn.setEnabled(true);
            }
        });
    }

    private void initSidesFillStyle() {
        mSidesFillStyleSWL.setAdapter(new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, IMcLineBasedItem.EFillStyle.values()));
        if (ObjectPropertiesBase.mSidesFillStyle == null)
            ObjectPropertiesBase.mSidesFillStyle = IMcClosedShapeItem.EFillStyle.EFS_SOLID;
        mSidesFillStyleSWL.setSelection(ObjectPropertiesBase.mSidesFillStyle.ordinal()/*getValue()*/);
    }

    private void initShapeType() {
        mShapeTypeSWL.setAdapter(new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, IMcLineBasedItem.EShapeType.values()));
        if (ObjectPropertiesBase.mShapeType == null)
            ObjectPropertiesBase.mShapeType = IMcLineBasedItem.EShapeType.EST_2D;
        mShapeTypeSWL.setSelection(ObjectPropertiesBase.mShapeType.ordinal());
    }

    private void initSidesFillColor() {
        mSidesFillColorSC.enableButtons(true);
        mSidesFillColorSC.setmBColor(ObjectPropertiesBase.mSidesFillColor);
    }

    private void initVerticalHeight() {
        mVerticalHeightNET.setFloat(ObjectPropertiesBase.mVerticalHeight);
    }

    private void initOutlineColor() {
        mLineOutlineColor.setmBColor(ObjectPropertiesBase.mLineOutlineColor);
    }

    private void initOutlineWidth() {
        mOutlineWidthNET.setFloat(ObjectPropertiesBase.mLineOutlineWidth);
        mLineOutlineColor.enableButtons(true);
    }

    private void initGreatCirclePrecision() {
        mLineBaseGreatCirclePrecision.setFloat(ObjectPropertiesBase.mLineBasedGreatCirclePrecision);
    }

    private void initLineSmoothingLevels() {
        mLineSmoothingLevels.setShort(ObjectPropertiesBase.mLineBasedSmoothingLevels);
    }

    private void initLineTexture() {

        if (ObjectPropertiesBase.mLineTexture == null)
            mLineTextureCB.setChecked(true);
        else
            mLineTextureCB.setChecked(false);

        mLineTextureBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                CreateTextureTabsFragment createTextureTabsFragment;
                if (ObjectPropertiesBase.mLineTexture == null) {
                    createTextureTabsFragment = CreateTextureTabsFragment.newInstance();
                    createTextureTabsFragment.setCurrentTexture(null);
                } else {
                    createTextureTabsFragment = CreateTextureTabsFragment.newInstance();
                    createTextureTabsFragment.setCurrentTexture(ObjectPropertiesBase.mLineTexture);
                }

                FragmentTransaction transaction = getChildFragmentManager().beginTransaction().add(R.id.obj_properties_ll, createTextureTabsFragment, Consts.TextureFragmentsTags.TEXTURE_FROM_LINE);
                transaction.addToBackStack(Consts.TextureFragmentsTags.TEXTURE_FROM_LINE);
                transaction.commit();
            }
        });

        mLineTextureCB.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                if (mLineTextureCB.isChecked()) {
                    mLineTextureBttn.setEnabled(false);
                    ObjectPropertiesBase.mLineTexture = null;
                } else
                    mLineTextureBttn.setEnabled(true);
            }
        });
        mTextureScale.setFloat(ObjectPropertiesBase.mLineTextureScale);
    }

    private void initLineColor() {
        mLineColor.setmBColor(ObjectPropertiesBase.mLineColor);
        mLineColor.enableButtons(true);
    }

    private void initLineStyle() {
        mLineStyleSpinner.setAdapter(new ArrayAdapter<>(getContext(), android.R.layout.simple_spinner_item, IMcLineBasedItem.ELineStyle.values()));
        mLineStyleSpinner.setSelection(ObjectPropertiesBase.mLineStyle.ordinal()/*getValue()*/);
    }

    private void initSaveBttn() {
        mSaveBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveLineWidth();
                saveLineStyle();
                saveLineColor();
                saveLineTexture();
                saveLineSmoothingLevels();
                saveGreatCirclePrecision();
                saveOutlineColor();
                saveOutlineWidth();
                saveSidesFillTextureScale();
                saveShapeType();
                saveSidesFillStyle();
                saveSidesFillColor();
                saveVerticalHeight();
                saveFillStyle();
            }
        });
    }
    private void saveSidesFillTextureScale() {
        ObjectPropertiesBase.mSidesFillTextureScale.x = mSidesFillTextureScale.getmX();
        ObjectPropertiesBase.mSidesFillTextureScale.y = mSidesFillTextureScale.getmY();
    }

    private void saveFillStyle() {
        ObjectPropertiesBase.mFillStyle = (IMcClosedShapeItem.EFillStyle) mFillStyleSWL.getSelectedItem();
    }

    private void saveShapeType() {
        ObjectPropertiesBase.mShapeType = (IMcLineBasedItem.EShapeType) mShapeTypeSWL.getSelectedItem();
    }

    private void saveSidesFillStyle() {
        ObjectPropertiesBase.mSidesFillStyle = (IMcLineBasedItem.EFillStyle) mSidesFillStyleSWL.getSelectedItem();
    }

    private void saveSidesFillColor() {
        ObjectPropertiesBase.mSidesFillColor = mSidesFillColorSC.getmBColor();
    }

    private void saveVerticalHeight() {
        ObjectPropertiesBase.mVerticalHeight = mVerticalHeightNET.getFloat();
    }

    private void saveOutlineColor() {
        ObjectPropertiesBase.mLineOutlineColor = mLineOutlineColor.getmBColor();
    }

    private void saveOutlineWidth() {
        ObjectPropertiesBase.mLineOutlineWidth = mOutlineWidthNET.getFloat();

    }

    private void saveGreatCirclePrecision() {
        ObjectPropertiesBase.mLineBasedGreatCirclePrecision = mLineBaseGreatCirclePrecision.getFloat();
    }

    private void saveLineSmoothingLevels() {
        ObjectPropertiesBase.mLineBasedSmoothingLevels = mLineSmoothingLevels.getByte();
    }

    private void saveLineTexture() {
        ObjectPropertiesBase.mLineTextureScale = mTextureScale.getFloat();
    }

    private void saveLineColor() {
        ObjectPropertiesBase.mLineColor = mLineColor.getmBColor();
    }

    private void saveLineStyle() {
        IMcLineBasedItem.ELineStyle lineStyle = (IMcLineBasedItem.ELineStyle) mLineStyleSpinner.getSelectedItem();
        ObjectPropertiesBase.mLineStyle = lineStyle;
    }

    private void saveLineWidth() {
        ObjectPropertiesBase.mLineWidth = mLineWidthNET.getFloat();

    }

    private void initLineWidth() {
        mLineWidthNET.setFloat(ObjectPropertiesBase.mLineWidth);
    }

    private void initFillStyle() {
        mFillStyleSWL.setAdapter(new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, IMcClosedShapeItem.EFillStyle.values()));
        if (ObjectPropertiesBase.mFillStyle == null)
            ObjectPropertiesBase.mFillStyle = IMcClosedShapeItem.EFillStyle.EFS_SOLID;
        mFillStyleSWL.setSelection(ObjectPropertiesBase.mFillStyle.ordinal()/*getValue()*/);
    }

    private void inflateViews() {
        mSaveBttn = (Button) mRootView.findViewById(R.id.obj_properties_line_based_save_bttn);
        mLineWidthNET = (NumericEditTextLabel) mRootView.findViewById(R.id.obj_properties_line_based_line_width_et);
        mLineStyleSpinner = (SpinnerWithLabel) mRootView.findViewById(R.id.obj_properties_line_based_line_style_spinner);
        mLineColor = (SelectColor) mRootView.findViewById(R.id.obj_properties_line_based_line_color);
        mLineTextureCB = (CheckBox) mRootView.findViewById(R.id.obj_properties_line_based_texture_none_rb);
        mLineTextureBttn = (Button) mRootView.findViewById(R.id.obj_properties_line_based_texture_bttn);
        mTextureScale = (NumericEditTextLabel) mRootView.findViewById(R.id.obj_properties_line_based_texture_scale_et);
        mLineSmoothingLevels = (NumericEditTextLabel) mRootView.findViewById(R.id.obj_properties_line_based_line_smoothing_level_et);
        mLineBaseGreatCirclePrecision = (NumericEditTextLabel) mRootView.findViewById(R.id.obj_properties_line_based_great_circle_precision_et);
        mOutlineWidthNET = (NumericEditTextLabel) mRootView.findViewById(R.id.obj_properties_line_based_outline_width_et);
        mLineOutlineColor = (SelectColor) mRootView.findViewById(R.id.obj_properties_line_outline_color);
        mShapeTypeSWL = (SpinnerWithLabel) mRootView.findViewById(R.id.obj_properties_line_based_line_shape_type);
        mSidesFillStyleSWL = (SpinnerWithLabel) mRootView.findViewById(R.id.obj_properties_line_based_sides_fill_style);
        mSidesFillColorSC = (SelectColor) mRootView.findViewById(R.id.obj_properties_line_based_sides_fill_color_sc);
        mVerticalHeightNET = (NumericEditTextLabel) mRootView.findViewById(R.id.line_based_vertical_height_net);
        mClosedShapeSidesTexturebttn = (Button) mRootView.findViewById(R.id.obj_properties_line_based_sides_texture_bttn);
        mFillTextureSidesNoneCb = (CheckBox) mRootView.findViewById(R.id.obj_properties_line_based_sides_texture_none_rb);
        mFillStyleSWL = (SpinnerWithLabel) mRootView.findViewById(R.id.obj_properties_line_based_fill_style);
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
           /* throw new RuntimeException(context.toString()
                    + " must implement OnFragmentInteractionListener");*/
        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
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
