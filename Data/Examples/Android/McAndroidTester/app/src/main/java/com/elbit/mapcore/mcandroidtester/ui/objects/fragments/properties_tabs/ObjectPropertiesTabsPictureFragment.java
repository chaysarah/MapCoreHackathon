package com.elbit.mapcore.mcandroidtester.ui.objects.fragments.properties_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentTransaction;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AbsListView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.ListView;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcSymbolicItem;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.ui.objects.ObjectPropertiesBase;
import com.elbit.mapcore.mcandroidtester.ui.objects.fragments.create_texture_tabs.CreateTextureTabsFragment;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SelectColor;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link ObjectPropertiesTabsPictureFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link ObjectPropertiesTabsPictureFragment#newInstance} factory method to
 * create an instance of this fragment.
 *
 */
public class ObjectPropertiesTabsPictureFragment extends Fragment {

    private OnFragmentInteractionListener mListener;
    private View mRootView;
    private Button mSaveBttn;
    private CheckBox mIsSizeFactorCB;
    private NumericEditTextLabel mWidth;
    private NumericEditTextLabel mHeight;
    private CheckBox mIsUseTextureGeoReferencing;
    private SelectColor mTextureColor;
    private Button mTextureBttn;
    private CheckBox mTextureNoneCB;
    private ListView mRectAlignLV;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     * @return A new instance of fragment ObjectPropertiesTabsPictureFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static ObjectPropertiesTabsPictureFragment newInstance() {
        ObjectPropertiesTabsPictureFragment fragment = new ObjectPropertiesTabsPictureFragment();
        return fragment;
    }
    public ObjectPropertiesTabsPictureFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
     }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        mRootView=inflater.inflate(R.layout.fragment_object_properties_tabs_picture, container, false);
        initViews();
        return mRootView;
    }

    private void initViews() {
        inflateViews();
        initSaveBttn();
        initIsSizeFactor();
        initWidth();
        initHeight();
        initIsUseTextureGeoReferencing();
        initTexture();
        initTextureColor();
        initRectAlignmentLV();
    }

    private void initTexture() {
        if (ObjectPropertiesBase.mPicTexture == null)
            mTextureNoneCB.setChecked(true);
        else
            mTextureNoneCB.setChecked(false);
        mTextureBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                CreateTextureTabsFragment createTextureTabsFragment;
                if (ObjectPropertiesBase.mPicTexture == null) {
                    createTextureTabsFragment = CreateTextureTabsFragment.newInstance();
                    createTextureTabsFragment.setCurrentTexture(null);
                } else {
                    createTextureTabsFragment = CreateTextureTabsFragment.newInstance();
                    createTextureTabsFragment.setCurrentTexture(ObjectPropertiesBase.mPicTexture);
                }

                FragmentTransaction transaction = getChildFragmentManager().beginTransaction().add(R.id.pic_texture_container, createTextureTabsFragment, Consts.TextureFragmentsTags.TEXTURE_FROM_PICTURE);
                transaction.addToBackStack(Consts.TextureFragmentsTags.TEXTURE_FROM_PICTURE);
                transaction.commit();
                //ObjectPropertiesBase.mLineTexture=(McTexture)createTextureTabsFragment.getmCurrentTexture();
            }
        });

        mTextureNoneCB.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                if (mTextureNoneCB.isChecked()) {
                    mTextureBttn.setEnabled(false);
                    ObjectPropertiesBase.mFillTexture = null;
                } else
                    mTextureBttn.setEnabled(true);
            }
        });
    }

    private void initTextureColor() {
        mTextureColor.enableButtons(true);
        mTextureColor.setmBColor(ObjectPropertiesBase.mPicTextureColor);
    }

    private void initIsUseTextureGeoReferencing() {
        mIsUseTextureGeoReferencing.setChecked(ObjectPropertiesBase.mPictureIsUseTextureGeoReferencing);
    }

    private void initHeight() {
        mHeight.setFloat(ObjectPropertiesBase.mPicHeight);
    }

    private void initWidth() {
        mWidth.setFloat(ObjectPropertiesBase.mPicWidth);
    }

    private void initIsSizeFactor() {
        mIsSizeFactorCB.setChecked(ObjectPropertiesBase.mPictureIsSizeFactor);
    }

    private void initRectAlignmentLV() {
        mRectAlignLV.setAdapter(new ArrayAdapter<>(getActivity(), android.R.layout.simple_list_item_single_choice, IMcSymbolicItem.EBoundingBoxPointFlags.values()));
        Funcs.setListViewHeightBasedOnChildren(mRectAlignLV);
        mRectAlignLV.setChoiceMode(AbsListView.CHOICE_MODE_SINGLE);
        for (int i = 0; i < mRectAlignLV.getCount(); i++) {
            if (((IMcSymbolicItem.EBoundingBoxPointFlags) mRectAlignLV.getAdapter().getItem(i)).compareTo(ObjectPropertiesBase.mPicRectAlignment) != 0)
                mRectAlignLV.setItemChecked(i, true);
        }
    }

    private void initSaveBttn() {
        mSaveBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveIsSizeFactor();
                saveWidth();
                saveHeight();
                saveIsUseTextureGeoReferencing();
                //saveTexture();
                saveTextureColor();
                saveRectAlignment();
            }
        });
    }

    private void saveTextureColor() {
        ObjectPropertiesBase.mPicTextureColor = mTextureColor.getmBColor();
    }

    private void saveRectAlignment() {
        int position = mRectAlignLV.getSelectedItemPosition();
        ObjectPropertiesBase.mPicRectAlignment =  (IMcSymbolicItem.EBoundingBoxPointFlags) mRectAlignLV.getAdapter().getItem(position);
        //ObjectPropertiesBase.mPicRectAlignment = rectAlign;
    }

    private void saveIsUseTextureGeoReferencing() {
        ObjectPropertiesBase.mPictureIsUseTextureGeoReferencing = mIsUseTextureGeoReferencing.isChecked();
    }

    private void saveHeight() {
        ObjectPropertiesBase.mPicHeight = mHeight.getFloat();
    }

    private void saveWidth() {
        ObjectPropertiesBase.mPicWidth = mWidth.getFloat();
    }

    private void saveIsSizeFactor() {
        ObjectPropertiesBase.mPictureIsSizeFactor = mIsSizeFactorCB.isChecked();
    }

    private void inflateViews() {
        mSaveBttn = (Button)mRootView.findViewById(R.id.object_properties_picture_save_bttn);
        mIsSizeFactorCB = (CheckBox)mRootView.findViewById(R.id.picture_is_size_factor_cb);
        mWidth = (NumericEditTextLabel)mRootView.findViewById(R.id.picture_width);
        mHeight = (NumericEditTextLabel)mRootView.findViewById(R.id.picture_height);
        mIsUseTextureGeoReferencing = (CheckBox)mRootView.findViewById(R.id.picture_is_use_texture_geo_referencing_cb);
        mTextureColor = (SelectColor)mRootView.findViewById(R.id.obj_properties_picture_texture_color);
        mTextureBttn = (Button) mRootView.findViewById(R.id.obj_properties_picture_texture_bttn);
        mTextureNoneCB = (CheckBox)mRootView.findViewById(R.id.obj_properties_picture_texture_none_rb);
        mRectAlignLV = (ListView)mRootView.findViewById(R.id.obj_properties_picture_texture_color_rect_align);
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
        } /*else {
            throw new RuntimeException(context.toString()
                    + " must implement OnFragmentInteractionListener");
        }*/
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
     * <p>
     * See the Android Training lesson <a href=
     * "http://developer.android.com/training/basics/fragments/communicating.html"
     * >Communicating with Other Fragments</a> for more information.
     */
    public interface OnFragmentInteractionListener {
        // TODO: Update argument type and name
        void onFragmentInteraction(Uri uri);
    }
}
