package com.elbit.mapcore.mcandroidtester.utils.customviews;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Color;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.LinearLayout;


import com.elbit.mapcore.General.ObjectRef;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.managers.ObjectWorld.Manager_MCTextures;
import com.elbit.mapcore.mcandroidtester.ui.adapters.HashMapAdapter;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.MapFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.private_properties.TexturePropertyDialogFragment;
import com.elbit.mapcore.mcandroidtester.ui.objects.ObjectPropertiesBase;
import com.elbit.mapcore.mcandroidtester.ui.objects.fragments.create_texture_tabs.BaseTextureTabFragment;
import com.elbit.mapcore.mcandroidtester.ui.objects.fragments.create_texture_tabs.TextureFromListFragment;
import com.elbit.mapcore.mcandroidtester.ui.objects.fragments.create_texture_tabs.TextureImageFileFragment;
import com.elbit.mapcore.mcandroidtester.ui.objects.fragments.create_texture_tabs.TextureMemoryBufferFragment;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

import java.nio.ByteBuffer;

import com.elbit.mapcore.Classes.OverlayManager.McTexture;
import com.elbit.mapcore.General.IMcErrors;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcImageFileTexture;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcMemoryBufferTexture;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcTexture;
import com.elbit.mapcore.Structs.SMcBColor;
import com.elbit.mapcore.Structs.SMcFileSource;

/**
 * Created by tc99382 on 22/01/2017.
 */
public class CreateTextureBottom extends LinearLayout {
    private final Context mContext;
    private Fragment mCurFragment;
    private final View mRootView;
    private SelectColor mTransparentColorSC;
    private SelectColor mColorToSubstituteSC;
    private SelectColor mSubstituteColorSC;

    public void setmUniqueNameET(EditText mUniqueNameET) {
        this.mUniqueNameET = mUniqueNameET;
    }

    private EditText mUniqueNameET;
    private CheckBox mUseExisting;
    private CheckBox mIsMemoryBuffer;
    private IMcTexture mCurTexture;
    private SMcBColor mTransparentColor;

    private SMcBColor mSubColor;
    private SMcBColor mColorToSub;

    public CheckBox getmDirectXTextureCB() {
        return mDirectXTextureCB;
    }

    private CheckBox mDirectXTextureCB;

    public LinearLayout getmColorsLL() {
        return mColorsLL;
    }

    public LinearLayout getmCbLL() {
        return mCbLL;
    }

    private LinearLayout mColorsLL;
    private LinearLayout mCbLL;

    public CheckBox getmFillPattern() {
        return mFillPattern;
    }

    public CheckBox getmIgnoreTransparentMargin() {
        return mIgnoreTransparentMargin;
    }

    public CheckBox getmUseExisting() {
        return mUseExisting;
    }

    public Button getmCreateBttn() {
        return mCreateBttn;
    }

    private CheckBox mFillPattern;
    private CheckBox mIgnoreTransparentMargin;
    private Button mCreateBttn;
    private Button mBackBttn;

    private TexturePropertyDialogFragment mTexturePropertyDialogFragment;
    public void setmTexturePropertyDialogFragment(TexturePropertyDialogFragment TexturePropertyDialogFragment)
    {
        mTexturePropertyDialogFragment = TexturePropertyDialogFragment;
    }

    public CreateTextureBottom(Context context, AttributeSet attrs) {
        super(context, attrs);
        mContext = context;
        LayoutInflater inflater = (LayoutInflater) context
                .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        mRootView = inflater.inflate(R.layout.texture_bottom, this);

        if (!isInEditMode()) {
            ;
        }
    }

    @Override
    protected void onAttachedToWindow() {
        super.onAttachedToWindow();
        if (!isInEditMode()) {
            initCurrentTexture();
            initViews();
        }
    }


    private void initCurrentTexture() {
        if (mCurFragment != null)
            mCurTexture = ((BaseTextureTabFragment) mCurFragment).mCurrentTexture;
    }

    private void initViews() {
        inflateViews();
        if (((BaseTextureTabFragment) mCurFragment) != null) {
            initBackBttn();
            initCreateBttn();
            initSelectColorBttns();
            initSelectColorsAphaVisibility();
            initCheckBoxes();
            initColorsFromTexture();
            initTextureUniqueName();
        }
    }

    private void initSelectColorsAphaVisibility() {
        mTransparentColorSC.getAlphaTIL().setVisibility(INVISIBLE);
        mColorToSubstituteSC.getAlphaTIL().setVisibility(INVISIBLE);
        mSubstituteColorSC.getAlphaTIL().setVisibility(INVISIBLE);

    }

    private String getTextureName(IMcTexture currTexture) {
        String retValue = "";
        try {
            retValue = mCurTexture.GetName();
        } catch (MapCoreException McEx) {
            if (McEx.getErrorCode() != IMcErrors.ECode.NOT_INITIALIZED)
                AlertMessages.ShowMapCoreErrorMessage(getContext(), McEx, "GetName");

        } catch (Exception e) {
            e.printStackTrace();
        }
        return retValue;
    }

    private void initTextureUniqueName() {
        if (mCurTexture != null)
            mUniqueNameET.setText(getTextureName(mCurTexture));

        if (mCurFragment instanceof TextureMemoryBufferFragment) {
            if (mCurTexture != null)
                mUniqueNameET.setEnabled(false);
            else
                mUniqueNameET.setEnabled(true);
        } else if (mCurFragment instanceof TextureFromListFragment) {
            //   if(mCreateBttn.getText().equals("update"))
            //deselect fromList Tab

            //else {
            mUniqueNameET.setEnabled(false);
            //}
        }
    }

    private void initColorsFromTexture() {
        mColorToSub = new SMcBColor();
        mSubColor = new SMcBColor();
        mTransparentColor = new SMcBColor();

        ObjectRef<Boolean> isTransparentColorEnabled = new ObjectRef<>();
        ObjectRef<Boolean> isColorSubstituteEnabled = new ObjectRef<>();

        if (((BaseTextureTabFragment) mCurFragment).mCurrentTexture != null) {
            try {
                mCurTexture.GetTransparentColor(mTransparentColor, isTransparentColorEnabled);
                IMcTexture.SColorSubstitution[] aColorSubstitution = mCurTexture.GetColorSubstitutions();
                if( aColorSubstitution != null && aColorSubstitution.length > 0) {
                    mColorToSub = aColorSubstitution[0].ColorToSubstitute;
                    mSubColor = aColorSubstitution[0].SubstituteColor;
                }

            } catch (MapCoreException McEx) {
                AlertMessages.ShowMapCoreErrorMessage(getContext(), McEx, "GetTransparentColor/GetColorSubstitutions");
            } catch (Exception e) {
                e.printStackTrace();
            }

            if (isTransparentColorEnabled.getValue() && mTransparentColor != null)
                mTransparentColorSC.setmBColor(mTransparentColor);
            else
                mTransparentColorSC.setmBColor(Color.TRANSPARENT);


            if (isColorSubstituteEnabled.getValue() && mColorToSub != null && mSubColor != null) {
                mColorToSubstituteSC.setmBColor(mColorToSub);
                mSubstituteColorSC.setmBColor(mSubColor);
            } else {
                mColorToSubstituteSC.setmBColor(Color.TRANSPARENT);
                mSubstituteColorSC.setmBColor(Color.TRANSPARENT);
            }

        }


    }

    private void initCheckBoxes() {
        if (((BaseTextureTabFragment) mCurFragment).mCurrentTexture == null) {
            mUseExisting.setVisibility(View.VISIBLE);
            mUseExisting.setChecked(true);
            mFillPattern.setEnabled(true);
            mIgnoreTransparentMargin.setEnabled(true);
        } else {
            mUseExisting.setVisibility(View.INVISIBLE);
            mFillPattern.setEnabled(false);
            mIgnoreTransparentMargin.setEnabled(false);

            try {
                mFillPattern.setChecked(mCurTexture.IsFillPattern());
                mIgnoreTransparentMargin.setChecked(mCurTexture.IsTransparentMarginIgnored());
            } catch (MapCoreException McEx) {
                AlertMessages.ShowMapCoreErrorMessage(getContext(), McEx, "IsFillPattern/IsTransparentMarginIgnored");
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    private void initCreateBttn() {
        if (mCurFragment instanceof TextureFromListFragment)
            mCreateBttn.setText("take selected");
        else {
            if (((BaseTextureTabFragment) mCurFragment).mCurrentTexture == null)
                mCreateBttn.setText("Create");
            else
                mCreateBttn.setText("Update");


        }
        mCreateBttn.setOnClickListener(new OnClickListener() {
            @Override
            public void onClick(View v) {
                ((MapsContainerActivity) mContext).getWindow().setFlags(WindowManager.LayoutParams.FLAG_NOT_TOUCHABLE,
                        WindowManager.LayoutParams.FLAG_NOT_TOUCHABLE);

                createTexture();

            }
        });
    }

    private void goBackAfterCreate()
    {
        ((MapsContainerActivity) mContext).runOnUiThread(new Runnable() {
            @Override
            public void run() {
                goBack();
                ((MapsContainerActivity) mContext).getWindow().clearFlags(WindowManager.LayoutParams.FLAG_NOT_TOUCHABLE);
            }
        });
    }

    private void goBack() {
        if (mCurFragment != null && mCurFragment.getParentFragment().getParentFragment() != null) {
            returnToContainerFrag();
        } else {
            //if the texture tabs was open by hiding another fragment, show here the hidden frag
            if (mContext instanceof MapsContainerActivity)
                showHiddenMapFragment();
        }
    }

    private void saveTexture() {
        String objPropertiesTabTag = mCurFragment.getParentFragment().getTag();
        switch (objPropertiesTabTag) {
            case Consts.TextureFragmentsTags.TEXTURE_FROM_LINE:
                ObjectPropertiesBase.mLineTexture = (McTexture) mCurTexture;
                break;
            case Consts.TextureFragmentsTags.TEXTURE_FROM_CLOSED_SHAPE:
                ObjectPropertiesBase.mFillTexture = (McTexture) mCurTexture;
                break;
            case Consts.TextureFragmentsTags.TEXTURE_FROM_CLOSED_SHAPE_SIDES:
                ObjectPropertiesBase.mSidesFillTexture = (McTexture) mCurTexture;
                break;
            case Consts.TextureFragmentsTags.TEXTURE_FROM_PICTURE:
                ObjectPropertiesBase.mPicTexture = (McTexture) mCurTexture;
                break;
            case Consts.TextureFragmentsTags.TEXTURE_FROM_CREATE_NEW_PICTURE:
                ObjectPropertiesBase.mPicTexture = (McTexture) mCurTexture;
                if (mContext instanceof MapsContainerActivity)
                    ((MapsContainerActivity) mContext).TextureTabsFragmentInteraction(mCurTexture, objPropertiesTabTag);
                break;
            case Consts.TextureFragmentsTags.TEXTURE_FROM_PROPERTIES_LIST: {
               /* if (mContext instanceof MapsContainerActivity)
                    ((MapsContainerActivity) mContext).TextureTabsFragmentInteraction(mCurTexture, objPropertiesTabTag);
                */
                if (mTexturePropertyDialogFragment != null)
                    mTexturePropertyDialogFragment.onTextureActionsCompleted(mCurTexture);
               break;
            }
        }
    }

    private void createTexture() {

        IMcTexture.ETextureType textureType = null;
        String textFileName = null;
        if (mCurFragment instanceof TextureImageFileFragment) {
            FileChooserEditTextLabel imageFile = ((TextureImageFileFragment) mCurFragment).mImageFileFC;
            textureType = IMcTexture.ETextureType.ETT_IMAGE_FILE;
            textFileName = imageFile.getDirPath().isEmpty() ? null : imageFile.getDirPath();
        } else if (mCurFragment instanceof TextureMemoryBufferFragment) {
            FileChooserEditTextLabel memoryBufferFile = ((TextureMemoryBufferFragment) mCurFragment).mMemoryBufferFileFC;
            textureType = IMcTexture.ETextureType.ETT_MEMORY_BUFFER;
            textFileName = memoryBufferFile.getDirPath().isEmpty() ? null : memoryBufferFile.getDirPath();
        } else if (mCurFragment instanceof TextureFromListFragment) {
            int selectedTexturePos = ((TextureFromListFragment) mCurFragment).mExistingTexturesLV.getCheckedItemPosition();
            IMcTexture curTexture = (IMcTexture) ((HashMapAdapter) ((TextureFromListFragment) mCurFragment).mExistingTexturesLV.getAdapter()).getItem(selectedTexturePos).getKey();
            if (curTexture == null) {
                AlertMessages.ShowGenericMessage(getContext(), "MCAndroidTester", "You must choose texture file first");
                return;
            } else
                textureType = curTexture.GetTextureType();
        }
        //init the texture colors
        setColors();
        IMcTexture.SColorSubstitution[] aCurrentColorSubstitution = new IMcTexture.SColorSubstitution[1];
        aCurrentColorSubstitution[0] = new IMcTexture.SColorSubstitution();
        aCurrentColorSubstitution[0].ColorToSubstitute = mColorToSub;
        aCurrentColorSubstitution[0].SubstituteColor = mSubColor;
        if (mCurTexture == null) {

            // case we take an existing texture from list
            if (mCurFragment instanceof TextureFromListFragment) {
                int selectedTexturePos = ((TextureFromListFragment) mCurFragment).mExistingTexturesLV.getCheckedItemPosition();
                mCurTexture = (IMcTexture) ((HashMapAdapter) ((TextureFromListFragment) mCurFragment).mExistingTexturesLV.getAdapter()).getItem(selectedTexturePos).getKey();
                initColorsFromTexture();
                initTextureUniqueName();
                // returnToContainerFrag();
                saveTexture();
                goBackAfterCreate();
            } else {
                // Create Texture
                final boolean useExisting = mUseExisting.isChecked();
                final ObjectRef<Boolean> existingUsed = new ObjectRef<>();

                switch (textureType) {
                    case ETT_IMAGE_FILE: {
                        final boolean isMemoryBuffer = mIsMemoryBuffer.isChecked();
                        final TextureImageFileFragment textureImageFileFragment = ((TextureImageFileFragment) mCurFragment);
                        final String dirPath = textureImageFileFragment.mImageFileFC.getDirPath();
                        final boolean fillPattern = mFillPattern.isChecked();
                        final boolean ignoreTransparentMargin = mIgnoreTransparentMargin.isChecked();
                        final String finalTextFileName = textFileName;

                        if (!isMemoryBuffer) {
                            final SMcFileSource sImageSource = new SMcFileSource(dirPath);
                            Funcs.runMapCoreFunc(new Runnable() {
                                @Override
                                public void run() {
                                    try {
                                        textureImageFileFragment.mImageTexture = IMcImageFileTexture.Static.Create(sImageSource, fillPattern, ignoreTransparentMargin, mTransparentColor, aCurrentColorSubstitution, useExisting, existingUsed);
                                        mCurTexture = textureImageFileFragment.mImageTexture;
                                        if (!existingUsed.getValue()) {
                                            Manager_MCTextures.getInstance().addToDictionary(textureImageFileFragment.mImageTexture);
                                        }
                                        saveTexture();
                                        goBackAfterCreate();
                                    } catch (MapCoreException e) {
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "McImageFileTexture.Create");
                                    } catch (Exception e) {
                                        e.printStackTrace();
                                    }
                                }
                            });
                        } else {
                            final byte[] fileAsByteArr = Funcs.getByteArrFromFilePath(finalTextFileName);
                            Funcs.runMapCoreFunc(new Runnable() {
                                @Override
                                public void run() {
                                    try {
                                        textureImageFileFragment.mImageTexture = IMcImageFileTexture.Static.Create(new SMcFileSource(fileAsByteArr), fillPattern, ignoreTransparentMargin, mTransparentColor, aCurrentColorSubstitution, useExisting, existingUsed);
                                        mCurTexture = textureImageFileFragment.mImageTexture;
                                        saveTexture();
                                        goBackAfterCreate();
                                    } catch (MapCoreException e) {
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "McImageFileTexture.Create");
                                    } catch (Exception e) {
                                        e.printStackTrace();
                                    }
                                }
                            });
                        }

                        break;
                    }
                    case ETT_MEMORY_BUFFER:
                        final TextureMemoryBufferFragment textureMemoryBufferFragment = ((TextureMemoryBufferFragment) mCurFragment);
                        if (textureMemoryBufferFragment.mBmpOriginalFormatSWL.getSelectedItem().equals(IMcTexture.EPixelFormat.EPF_UNKNOWN)) {
                            AlertMessages.ShowGenericMessage(getContext(), "Format Not Defined", "Buffer format is Not Defined");
                        } else {

                            final int width = Integer.valueOf(String.valueOf(textureMemoryBufferFragment.mMemBufferWidth.getText()));
                            final int height = Integer.valueOf(String.valueOf(textureMemoryBufferFragment.mMemBufferHeight.getText()));
                            if (textureMemoryBufferFragment.mBMP != null) {
                                final IMcTexture.EUsage textureUsage = (IMcTexture.EUsage) textureMemoryBufferFragment.mMemBufferTextureUsageSWL.getSelectedItem();
                                ByteBuffer buffer = ByteBuffer.allocate(textureMemoryBufferFragment.mBMP.getHeight() * textureMemoryBufferFragment.mBMP.getRowBytes());
                                textureMemoryBufferFragment.mBMP.copyPixelsToBuffer(buffer);
                                final byte[] finalBufferAsArr = buffer.array();
                                final IMcTexture.EPixelFormat bufferPixelFormat = IMcTexture.EPixelFormat.valueOf(String.valueOf(textureMemoryBufferFragment.mMemBufferPixelFormat.getText()));
                                final boolean directXTexture = mDirectXTextureCB.isChecked();
                                final boolean mipMap = mDirectXTextureCB.isChecked();
                                final String uniqueName = String.valueOf(mUniqueNameET.getText());

                                Funcs.runMapCoreFunc(new Runnable() {
                                    @Override
                                    public void run() {
                                        try {
                                            final int rowPitch = textureMemoryBufferFragment.mBMP.getRowBytes() / IMcTexture.Static.GetPixelFormatByteCount(bufferPixelFormat);
                                            textureMemoryBufferFragment.mMemoryBufferTexture = IMcMemoryBufferTexture.Static.Create(width, height, bufferPixelFormat, textureUsage, mipMap, finalBufferAsArr, rowPitch, uniqueName);
                                            mCurTexture = textureMemoryBufferFragment.mMemoryBufferTexture;
                                            Manager_MCTextures.getInstance().addToDictionary(mCurTexture);
                                            if (directXTexture) {
                                                long ptr = textureMemoryBufferFragment.mMemoryBufferTexture.GetDirectXTexture();
                                                AlertMessages.ShowGenericMessage(getContext(), "DirectX Texture", "DirectX Texture created successfully \nIntPte: " + String.valueOf(ptr));
                                            }
                                            saveTexture();
                                            goBackAfterCreate();
                                        } catch (MapCoreException e) {
                                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetDirectXTexture");
                                        } catch (Exception e) {
                                            e.printStackTrace();
                                        }
                                    }
                                });
                            }
                        }
                }
            }
        } else
            //update texture
            switch (textureType) {
                //TODO arrange the color init in funcs
                case ETT_IMAGE_FILE:
                    mColorToSub = mColorToSubstituteSC.getmBColor();
                    mSubColor = mSubstituteColorSC.getmBColor();
                    mTransparentColor = mTransparentColorSC.getmBColor();
                    final String finalTextFileName = textFileName;


                    final TextureImageFileFragment textureImageFileFragment = ((TextureImageFileFragment) mCurFragment);
                    if (mIsMemoryBuffer.isChecked() == false) {
                        final SMcFileSource sImageSource = new SMcFileSource(textureImageFileFragment.mImageFileFC.getDirPath());
                        Funcs.runMapCoreFunc(new Runnable() {
                            @Override
                            public void run() {
                                try {
                                    textureImageFileFragment.mImageTexture.SetImageFile(sImageSource, mTransparentColor, aCurrentColorSubstitution);
                                    goBackAfterCreate();
                                } catch (MapCoreException e) {
                                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetImageFile");
                                } catch (Exception e) {
                                    e.printStackTrace();
                                }
                            }
                        });
                    } else {
                        final byte[] fileByte = Funcs.getByteArrFromFilePath(finalTextFileName);
                        Funcs.runMapCoreFunc(new Runnable() {
                            @Override
                            public void run() {
                                try {
                                    textureImageFileFragment.mImageTexture.SetImageFile(new SMcFileSource(fileByte), mTransparentColor, aCurrentColorSubstitution);
                                    goBackAfterCreate();
                                } catch (MapCoreException e) {
                                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetImageFile");
                                } catch (Exception e) {
                                    e.printStackTrace();
                                }
                            }
                        });
                    }
                    break;
                case ETT_MEMORY_BUFFER:
                    final TextureMemoryBufferFragment textureMemoryBufferFragment = ((TextureMemoryBufferFragment) mCurFragment);
                    ByteBuffer buffer = ByteBuffer.allocate(textureMemoryBufferFragment.mBMP.getHeight() * textureMemoryBufferFragment.mBMP.getRowBytes());
                    textureMemoryBufferFragment.mBMP.copyPixelsToBuffer(buffer);
                    final IMcTexture.EPixelFormat bufferPixelFormat = IMcTexture.EPixelFormat.valueOf(String.valueOf(textureMemoryBufferFragment.mMemBufferPixelFormat.getText()));

                    final Integer width = Integer.valueOf(String.valueOf(textureMemoryBufferFragment.mMemBufferWidth.getText()));
                    final Integer height = Integer.valueOf(String.valueOf(textureMemoryBufferFragment.mMemBufferHeight.getText()));
                    final byte[] finalBufferAsArr = buffer.array();
                    Funcs.runMapCoreFunc(new Runnable() {
                        @Override
                        public void run() {
                            try {
                                int rowPitch = textureMemoryBufferFragment.mBMP.getRowBytes() / IMcTexture.Static.GetPixelFormatByteCount(bufferPixelFormat);
                                textureMemoryBufferFragment.mMemoryBufferTexture.UpdateFromMemoryBuffer(width, height, bufferPixelFormat, rowPitch, finalBufferAsArr);
                                mCurTexture = textureMemoryBufferFragment.mMemoryBufferTexture;
                                Manager_MCTextures.getInstance().addToDictionary(mCurTexture);
                                goBackAfterCreate();
                            } catch (MapCoreException e) {
                                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "UpdateFromMemoryBuffer");
                            } catch (Exception e) {
                                e.printStackTrace();
                            }
                        }
                    });
                    break;
            }
    }

    private void createBitmap() {
        TextureMemoryBufferFragment textureMemoryBufferFragment = ((TextureMemoryBufferFragment) mCurFragment);
        Bitmap bmp = BitmapFactory.decodeFile(textureMemoryBufferFragment.mMemoryBufferFileFC.getDirPath());
    }

    private void setColors() {
        mColorToSub = mColorToSubstituteSC.getmBColor();
        mSubColor = mSubstituteColorSC.getmBColor();
        mTransparentColor = mTransparentColorSC.getmBColor();
    }

    private void initSelectColorBttns() {
        mTransparentColorSC.enableButtons(true);
        mColorToSubstituteSC.enableButtons(true);
        mSubstituteColorSC.enableButtons(true);

    }

    private void initBackBttn() {
        mBackBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                goBack();
            }
        });
    }

    private void showHiddenMapFragment() {
        // Get the FragmentManager of the parent fragment
        Fragment parentFragment = mCurFragment.getParentFragment();
        if (parentFragment == null) {
            throw new IllegalStateException("Parent fragment is null.");
        }

        FragmentManager fragManager = parentFragment.getParentFragmentManager();
        FragmentTransaction transaction = fragManager.beginTransaction();

        // Ensure the parentFragment is in the same FragmentManager
        if (fragManager.findFragmentById(parentFragment.getId()) != null) {
            transaction.hide(parentFragment);
            transaction.addToBackStack("hide" + parentFragment.getClass().getSimpleName());
        }

        // Show the MapFragment by tag
        Fragment mapFragment = fragManager.findFragmentByTag(MapFragment.class.getSimpleName());
        if (mapFragment != null) {
            transaction.show(mapFragment);
            transaction.addToBackStack(MapFragment.class.getSimpleName());
        }

        transaction.commit();
    }

    private void returnToContainerFrag() {
        Fragment parentFragment = mCurFragment.getParentFragment();
        if (parentFragment == null) {
            throw new IllegalStateException("Parent fragment is null.");
        }

        FragmentManager fragManager = parentFragment.getParentFragmentManager();
        FragmentTransaction transaction = fragManager.beginTransaction();

        // Ensure the parentFragment is in the same FragmentManager
        if (fragManager.findFragmentById(parentFragment.getId()) != null) {
            transaction.hide(parentFragment);
            transaction.addToBackStack("texture");
        }

        transaction.commit();
    }

    private void inflateViews() {
        mUniqueNameET = (EditText) mRootView.findViewById(R.id.texture_bottom_unique_name_et);
        mTransparentColorSC = (SelectColor) mRootView.findViewById(R.id.texture_bottom_transparent_color);
        mColorToSubstituteSC = (SelectColor) mRootView.findViewById(R.id.texture_bottom_color_to_substitute);
        mSubstituteColorSC = (SelectColor) mRootView.findViewById(R.id.texture_bottom_substitute_color);
        mUseExisting = (CheckBox) mRootView.findViewById(R.id.texture_bottom_use_existing_cb);
        mFillPattern = (CheckBox) mRootView.findViewById(R.id.texture_bottom_fill_pattern_cb);
        mIgnoreTransparentMargin = (CheckBox) mRootView.findViewById(R.id.texture_bottom_ignore_transparent_margin_cb);
        mCreateBttn = (Button) mRootView.findViewById(R.id.create_texture_create_bttn);
        mBackBttn = (Button) mRootView.findViewById(R.id.create_texture_back_bttn);
        mColorsLL = (LinearLayout) mRootView.findViewById(R.id.texture_bottom_colors_ll);
        mCbLL = (LinearLayout) mRootView.findViewById(R.id.texture_bottom_cbs);
        mDirectXTextureCB = (CheckBox) mRootView.findViewById(R.id.texture_bottom_direct_x_cb);
        mIsMemoryBuffer = (CheckBox) mRootView.findViewById(R.id.texture_bottom_is_memory_buffer_cb);
 
    }

    public void setmCurFragment(Fragment fragment) {
        mCurFragment = fragment;
    }

    public SelectColor getmSubstituteColorSC() {
        return mSubstituteColorSC;
    }

    public SelectColor getmTransparentColorSC() {
        return mTransparentColorSC;
    }

    public SelectColor getmColorToSubstituteSC() {
        return mColorToSubstituteSC;
    }

    public EditText getmUniqueNameET() {
        return mUniqueNameET;
    }
}
