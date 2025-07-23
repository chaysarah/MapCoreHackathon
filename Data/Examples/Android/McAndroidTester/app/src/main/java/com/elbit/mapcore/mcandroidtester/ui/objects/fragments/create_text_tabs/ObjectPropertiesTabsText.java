package com.elbit.mapcore.mcandroidtester.ui.objects.fragments.create_text_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.view.WindowManager;
import android.widget.AbsListView;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CheckedTextView;
import android.widget.CompoundButton;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.RadioButton;

import com.elbit.mapcore.General.ObjectRef;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.IFontTab;
import com.elbit.mapcore.mcandroidtester.managers.ObjectWorld.Manager_MCFonts;
import com.elbit.mapcore.mcandroidtester.ui.adapters.CharRangeAdapter;
import com.elbit.mapcore.mcandroidtester.ui.adapters.FontsAdapter;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.private_properties.FontPropertyDialogFragment;
import com.elbit.mapcore.mcandroidtester.ui.objects.ObjectPropertiesBase;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.FileChooserEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SelectColor;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SpinnerWithLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.TwoDFVector;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import com.elbit.mapcore.Enums.EAxisXAlignment;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcFileFont;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcFont;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcLogFont;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcTextItem;
import com.elbit.mapcore.Structs.SMcFileSource;
import com.elbit.mapcore.Structs.SMcVariantLogFont;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link ObjectPropertiesTabsText.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link ObjectPropertiesTabsText#newInstance} factory method to
 * create an instance of this fragment.
 */
public class ObjectPropertiesTabsText extends Fragment implements IFontTab{
    public static final String ANDROID_FONTS_PATH = "/system/fonts/";
    private static final int FONT_DEFAULT_SIZE = 8;
    private IMcFont.EFontType mFontType;
    private OnFragmentInteractionListener mListener;
    private View mRootView;
    private Button mSaveBttn;
    private Button mAddCharRangeBttn;
    private Button mDeleteCharRangeBttn;
    private EditText mFontText;
    private TwoDFVector mTextScale;
    private NumericEditTextLabel mMargin;
    private SelectColor mTextColor;
    private SelectColor mBgColor;
    private SelectColor mOutlineColor;
    private CheckBox mLogFontIsUnicodeCb;
    private CheckBox mTxtIsUnicodeCb;
    private NumericEditTextLabel mOutlineWidthEt;
    private CheckBox mTxtUseExistingCb;
    private SpinnerWithLabel mNeverUpsideDownMode;
    private CheckBox mRtlReadingOrderCb;
    private SpinnerWithLabel mAlignmentSWL;
    private NumericEditTextLabel mFontSizeEt;
    private EditText mFontNameEt;
    private CheckBox mItalicCB;
    private CheckBox mBoldCB;
    private CheckBox mUnderLineCB;
    private RadioButton mLogFontRB;
    private RadioButton mFileFontRB;
    private LinearLayout mLogFontLL;
    private LinearLayout mFileFontLL;
    private FileChooserEditTextLabel mFileFontFileChooser;
    private NumericEditTextLabel mFileFontSizeET;
    private CheckBox mFileFontIsMemoryBufferCB;
    private ListView mSelectFromListLV;
    private Button mAddFontBttn;
    private Button mUpdateFontBttn;
    private Button mDeleteFontBttn;
    private FontsAdapter mFontsAdapter;
    private LinearLayout mTextParametersLL;
    private IMcFont mCurrentFont;
    private int mOutlineWidth = 1;
    private boolean mTextUseExisting = true;
    private int mCurrSelectFontIndex = -1;
    private ObjectPropertiesBase.Text.PreviousFragmentText mPreviousFragmentText;
    private CharRangeAdapter mCharRangeAdapter;
    private int mCurrCharRangeIndex = -1;
    private ListView mCharRangeLV;
    private EditText mCharRangeFrom;
    private EditText mCharRangeTo;
    private EditText mCharRangeFromVal;
    private EditText mCharRangeToVal;
    ArrayList<IMcFont.SCharactersRange> mCharactersRange;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment ObjectPropertiesTabsText.
     */
    // TODO: Rename and change types and number of parameters
    public static ObjectPropertiesTabsText newInstance() {
        ObjectPropertiesTabsText fragment = new ObjectPropertiesTabsText();
        return fragment;
    }

    public ObjectPropertiesTabsText() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        mRootView = inflater.inflate(R.layout.fragment_object_properties_text, container, false);
        initViews();
        return mRootView;
    }

    private void initViews() {
        mCharactersRange = new ArrayList<>();
        inflateViews();
        initTextEditor();
        initSaveBttn();
        initNeverUpsideDownMode();
        initTextScale();
        initMargin();
        initTextColor();
        initBackgroundColor();
        initOutlineColor();
        initTextIsUnicode();
        initRtlCB();
        initAlignment();
        initFontRB();
        initFontsLV();
    }

    private void initTextEditor()
    {
        if(ObjectPropertiesBase.Text.getInstance().mPreviousFragmentText != ObjectPropertiesBase.Text.PreviousFragmentText.CreateNewText)
            mFontText.setVisibility(View.GONE);
        if(ObjectPropertiesBase.Text.getInstance().mPreviousFragmentText == ObjectPropertiesBase.Text.PreviousFragmentText.PropertiesList){
            mTextParametersLL.setVisibility(View.GONE);
            mSaveBttn.setVisibility(View.GONE);
        }
    }

    private void initFontRB() {
        mLogFontRB.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton compoundButton, boolean isChecked) {
                mLogFontLL.setVisibility(isChecked ? View.VISIBLE : View.GONE);
            }
        });
        mFileFontRB.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton compoundButton, boolean isChecked) {
                mFileFontLL.setVisibility(isChecked ? View.VISIBLE : View.GONE);
            }
        });

        mAddFontBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                ((MapsContainerActivity)getContext()).getWindow().setFlags(WindowManager.LayoutParams.FLAG_NOT_TOUCHABLE,
                        WindowManager.LayoutParams.FLAG_NOT_TOUCHABLE);

                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        /*IMcFont.SCharactersRange[] charRange = new IMcFont.SCharactersRange[3];
                        // english ascii
                        charRange[0] = new IMcFont.SCharactersRange();
                        charRange[0].nFrom = 0x0000;
                        charRange[0].nTo = 0x007f;
                        // extended latin
                        charRange[1] = new IMcFont.SCharactersRange();
                        charRange[1].nFrom = 0x0080;
                        charRange[1].nTo = 0x00ff;
                        // hebrew
                        charRange[2] = new IMcFont.SCharactersRange();
                        charRange[2].nFrom = 0x0590;
                        charRange[2].nTo = 0x05ff;*/
                        IMcFont.SCharactersRange[] arrCharactersRange = null;
                        if( mCharactersRange != null && mCharactersRange.size() > 0) {
                            arrCharactersRange = new IMcFont.SCharactersRange[mCharactersRange.size()];
                            mCharactersRange.toArray(arrCharactersRange); // fill the arrCharactersRange
                        }
                        if (mLogFontRB.isChecked()) {

                            IMcLogFont font = null;
                            try {
                                SMcVariantLogFont logFont = getMcVariantLogFontFromView();
                                font = IMcLogFont.Static.Create(logFont, true, arrCharactersRange, 0, mTxtUseExistingCb.isChecked(), (ObjectRef)null, mOutlineWidthEt.getInt());
                                afterCreateFont(font);

                            } catch (MapCoreException e) {
                                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcLogFont.Static.Create");
                            } catch (Exception e) {
                                e.printStackTrace();
                            }
                        } else if (mFileFontRB.isChecked()) {
                            IMcFileFont font = null;
                            int weight = mFileFontSizeET.getInt();
                            SMcFileSource sImageSource = getMcFileSourceFromView();
                            try {
                                font = IMcFileFont.Static.Create(sImageSource, weight, true, arrCharactersRange, 0, mTxtUseExistingCb.isChecked(), null, mOutlineWidthEt.getInt());
                                afterCreateFont(font);
                            } catch (MapCoreException e) {
                                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcFileFont.Static.Create");
                            } catch (Exception e) {
                                e.printStackTrace();
                            }
                        }
                        getActivity().runOnUiThread(new Runnable() {
                            @Override
                           public void run() {
                                getActivity().getWindow().clearFlags(WindowManager.LayoutParams.FLAG_NOT_TOUCHABLE);
                                if (ObjectPropertiesBase.Text.getInstance().mPreviousFragmentText == ObjectPropertiesBase.Text.PreviousFragmentText.PropertiesList) {
                                    if (getActivity() instanceof MapsContainerActivity) {
                                        goBack();
                                    }
                                }
                                resetFontListAdapter();
                                resetFontViews();
                                resetCharRangeListAdapter();

                            }
                        });
                    }
                });
            }
        });

        mDeleteFontBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if (ObjectPropertiesBase.Text.getInstance().mTextFont != null) {
                    Manager_MCFonts.getInstance().removeFromDictionary(ObjectPropertiesBase.Text.getInstance().mTextFont);
                    Funcs.runMapCoreFunc(new Runnable() {
                        @Override
                        public void run() {
                            ObjectPropertiesBase.Text.getInstance().mTextFont.Release();

                            getActivity().runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    ObjectPropertiesBase.Text.getInstance().mTextFont = null;
                                    resetFontListAdapter();
                                    resetFontViews();
                                    setMappingBttnsVisible(false);
                                }
                            });
                        }
                    });
                }
            }
        });

        mUpdateFontBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                ((MapsContainerActivity)getContext()).getWindow().setFlags(WindowManager.LayoutParams.FLAG_NOT_TOUCHABLE,
                        WindowManager.LayoutParams.FLAG_NOT_TOUCHABLE);

                if (ObjectPropertiesBase.Text.getInstance().mTextFont != null) {
                    final IMcFont font = ObjectPropertiesBase.Text.getInstance().mTextFont;
                    SMcVariantLogFont variantLogFont = null;
                    SMcFileSource fileSource = null;

                    if (font instanceof IMcLogFont)
                        variantLogFont = getMcVariantLogFontFromView();
                    else if (font instanceof IMcFileFont)
                        fileSource = getMcFileSourceFromView();

                    final SMcVariantLogFont finalVariantLogFont = variantLogFont;
                    final SMcFileSource finalFileSource = fileSource;

                    Funcs.runMapCoreFunc(new Runnable() {
                        @Override
                        public void run() {
                            if (font instanceof IMcLogFont) {
                                try {
                                    ((IMcLogFont) font).SetLogFont(finalVariantLogFont);
                                } catch (MapCoreException e) {
                                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetLogFont");
                                } catch (Exception e) {
                                    e.printStackTrace();
                                }
                            } else if (font instanceof IMcFileFont) {

                                final int weight = mFileFontSizeET.getInt();

                                try {
                                    ((IMcFileFont) font).SetFontFileAndHeight(finalFileSource, weight);
                                } catch (MapCoreException e) {
                                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetFontFileAndHeight");
                                } catch (Exception e) {
                                    e.printStackTrace();
                                }

                            }
                            getActivity().runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    getActivity().getWindow().clearFlags(WindowManager.LayoutParams.FLAG_NOT_TOUCHABLE);

                                    resetFontListAdapter();
                                    resetFontViews();
                                    setMappingBttnsVisible(false);

                                }
                            });
                        }
                    });
                }
            }
        });

         setMappingBttnsVisible(false);
    }

    private void afterCreateFont(IMcFont font)
    {
        Manager_MCFonts.getInstance().addToDictionary(font);

        mCharactersRange.clear();

        if (ObjectPropertiesBase.Text.getInstance().mPreviousFragmentText != ObjectPropertiesBase.Text.PreviousFragmentText.PropertiesList)
            ObjectPropertiesBase.Text.getInstance().mTextFont = font;
        else
            mCurrentFont = font;
    }

    private void goBack() {
        if ( this.getParentFragment().getParentFragment() != null) {
            returnToContainerFrag();
        }
    }

    private void returnToContainerFrag() {
        FragmentManager fragManager = getChildFragmentManager();
        FragmentTransaction transaction = fragManager.beginTransaction();
        Fragment fragment = getParentFragment().getParentFragment();
        if(fragment instanceof FontPropertyDialogFragment)
        {
            FontPropertyDialogFragment fontPropertyDialogFragment = (FontPropertyDialogFragment)fragment;
            fontPropertyDialogFragment.onFontActionsCompleted(mCurrentFont);
        }
        transaction.hide(getParentFragment());
        transaction.addToBackStack("font");
        transaction.commit();
    }

    private void initFontsLV() {
        resetFontListAdapter();

       /* mSelectFromListLV.setOnTouchListener(new View.OnTouchListener() {
            // Setting on Touch Listener for handling the touch inside ScrollView
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                // Disallow the touch request for parent scroll on touch of child view
                v.getParent().requestDisallowInterceptTouchEvent(true);
                return false;
            }
        });*/
        mSelectFromListLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int index, long l) {
                setSelectedFont((ListView) adapterView, index);
            }
        });

        Funcs.setListViewHeightBasedOnChildren(mSelectFromListLV);
        resetCharRangeListAdapter();

      /*  mCharRangeLV.setOnTouchListener(new View.OnTouchListener() {
            // Setting on Touch Listener for handling the touch inside ScrollView
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                // Disallow the touch request for parent scroll on touch of child view
                v.getParent().requestDisallowInterceptTouchEvent(true);
                return false;
            }
        });*/

        mCharRangeLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                boolean isChecked = ((CheckedTextView)view).isChecked();
                mCurrCharRangeIndex = isChecked? position : -1;
                mDeleteCharRangeBttn.setEnabled(isChecked);
            }
        });

        mAddCharRangeBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                IMcFont.SCharactersRange charactersRange = new IMcFont.SCharactersRange();
                charactersRange.nFrom = mCharRangeFrom.getText().charAt(0);
                charactersRange.nTo = mCharRangeTo.getText().charAt(0);
                mCharactersRange.add(charactersRange);
                resetCharRangeListAdapter();

                mCharRangeFrom.setText("");
                mCharRangeFromVal.setText("");
                mCharRangeTo.setText("");
                mCharRangeToVal.setText("");
            }
        });

        mDeleteCharRangeBttn.setEnabled(false);
        mDeleteCharRangeBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (mCurrCharRangeIndex >= 0 && mCharactersRange.size() > mCurrCharRangeIndex) {
                    mCharactersRange.remove(mCurrCharRangeIndex);
                    mCurrCharRangeIndex = -1;
                    resetCharRangeListAdapter();
                    mDeleteCharRangeBttn.setEnabled(false);
                }
            }
        });
    }

    private void setSelectedFont(ListView listView, int index)
    {
        mCurrSelectFontIndex = index;
        IMcFont font = (IMcFont) listView.getItemAtPosition(index);
        if(ObjectPropertiesBase.Text.getInstance().mPreviousFragmentText != ObjectPropertiesBase.Text.PreviousFragmentText.PropertiesList)
            ObjectPropertiesBase.Text.getInstance().mTextFont = font;
        try {
            mFontType = font.GetFontType();
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcFont.GetFontType");
        } catch (Exception e) {
            e.printStackTrace();
        }
        setCurrentFont(font);
        setMappingBttnsVisible(true);
        setCurMappingViews(font);
    }

    private void setMappingBttnsVisible(boolean isEnabled) {
        if(ObjectPropertiesBase.Text.getInstance().mPreviousFragmentText == ObjectPropertiesBase.Text.PreviousFragmentText.PropertiesList){
            mDeleteFontBttn.setEnabled(false);
            mUpdateFontBttn.setEnabled(false);
            mAddFontBttn.setEnabled(true);
        }
        else {
            mDeleteFontBttn.setEnabled(isEnabled);
            mUpdateFontBttn.setEnabled(isEnabled);
            mAddFontBttn.setEnabled(!isEnabled);
        }


    }

    private void setCurrentFontLV(int index)
    {
        if(mCurrentFont != null) {
            setCurrentFontLVByFont(mCurrentFont);
            setSelectedFont(mSelectFromListLV, index);
        }
        else if(ObjectPropertiesBase.Text.getInstance().mTextFont != null) {
            setCurrentFontLVByFont(ObjectPropertiesBase.Text.getInstance().mTextFont);
        }
    }

    private void setCurrentFontLVByFont(IMcFont font) {
        int index = mFontsAdapter.getPosition(font);
        if (index >= 0)
            mSelectFromListLV.setItemChecked(index, true);
    }

    private void resetFontViews() {
        mLogFontRB.setChecked(true);
        mFontNameEt.setText("");
        mFontSizeEt.setText("");
        mItalicCB.setChecked(false);
        mBoldCB.setChecked(false);
        mUnderLineCB.setChecked(false);
       // mFileFontFileChooser.setStrFolderPath(ANDROID_FONTS_PATH);
        mFileFontIsMemoryBufferCB.setChecked(false);
        mFileFontSizeET.setText("");
    }

    private void setCurMappingViews(IMcFont font) {
        if (font instanceof IMcLogFont) {
            IMcLogFont logFont = (IMcLogFont) ObjectPropertiesBase.Text.getInstance().mTextFont;
            mLogFontRB.setChecked(true);
            try {
                SMcVariantLogFont variantLogFont = logFont.GetLogFont();

                mFontNameEt.setText(variantLogFont.LogFont.lfFaceName);
                if (variantLogFont.LogFont.lfItalic == 0)
                    mItalicCB.setChecked(false);
                else
                    mItalicCB.setChecked(true);

                if (variantLogFont.LogFont.lfUnderline == 0)
                    mUnderLineCB.setChecked(false);
                else
                    mUnderLineCB.setChecked(true);

                if ((variantLogFont.LogFont.lfWeight) > 400)
                    mBoldCB.setChecked(true);
                else
                    mBoldCB.setChecked(false);

                mFontSizeEt.setInt(variantLogFont.LogFont.lfHeight);
                mLogFontIsUnicodeCb.setChecked(variantLogFont.bIsUnicode);

            } catch (Exception e) {
                e.printStackTrace();
            }
        }
        else if(font instanceof IMcFileFont)
        {
            mFileFontRB.setChecked(true);
            IMcFileFont fileFont = (IMcFileFont) font;
            SMcFileSource fileSource = new SMcFileSource();
            ObjectRef<Integer> height = new ObjectRef<>();
            try {
                fileFont.GetFontFileAndHeight(fileSource, height);
                mFileFontIsMemoryBufferCB.setChecked(fileSource.bIsMemoryBuffer);
                mFileFontFileChooser.setDirPath(fileSource.strFileName);
                mFileFontSizeET.setInt(height.getValue());
            }
            catch (MapCoreException e){
                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetFontFileAndHeight()");
            }
            catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    private void resetFontListAdapter() {
        mSelectFromListLV.setAdapter(null);
        List<IMcFont> listFonts = new ArrayList<>(Manager_MCFonts.getInstance().getFonts().keySet());
        if(mCurrentFont!= null && !listFonts.contains(mCurrentFont))
            listFonts.add(mCurrentFont);
        mFontsAdapter = new FontsAdapter(getContext(), R.layout.checkable_list_item, listFonts);
        mSelectFromListLV.setAdapter(mFontsAdapter);
        mSelectFromListLV.setChoiceMode(AbsListView.CHOICE_MODE_SINGLE);
        int index = listFonts.indexOf(mCurrentFont);
        setCurrentFontLV(index);

        Funcs.setListViewHeightBasedOnChildren(mSelectFromListLV);
    }

    private void resetCharRangeListAdapter() {

        if(mCharRangeLV != null) {
            mCharRangeLV.setAdapter(null);
            try {
                mCharRangeAdapter = new CharRangeAdapter(getContext(), R.layout.checkable_list_item, mCharactersRange);
                mCharRangeLV.setAdapter(mCharRangeAdapter);
                mCharRangeLV.setChoiceMode(AbsListView.CHOICE_MODE_SINGLE);

                Funcs.setListViewHeightBasedOnChildren(mCharRangeLV);
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    private void initAlignment() {
        mAlignmentSWL.setAdapter(new ArrayAdapter<>(getContext(), android.R.layout.simple_spinner_item, EAxisXAlignment.values()));
        mAlignmentSWL.setSelection(ObjectPropertiesBase.Text.getInstance().mTextAlignment.getValue());
    }

    private void initRtlCB() {
        mRtlReadingOrderCb.setChecked(ObjectPropertiesBase.Text.getInstance().mTextRtlReadingOrder);
    }

    private void initNeverUpsideDownMode() {
        mNeverUpsideDownMode = (SpinnerWithLabel) mRootView.findViewById(R.id.object_properties_text_never_upside_down_mode_swl);
        mNeverUpsideDownMode.setAdapter(new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, IMcTextItem.ENeverUpsideDownMode.values()));
        mNeverUpsideDownMode.setSelection(ObjectPropertiesBase.Text.getInstance().mNeverUpsideDown.getValue());
    }

    private void initTextIsUnicode() {
        mTxtIsUnicodeCb.setChecked(ObjectPropertiesBase.Text.getInstance().mTextIsUnicode);
        mOutlineWidthEt.setUInt(mOutlineWidth);
        mTxtUseExistingCb.setChecked(mTextUseExisting);
    }

    private void initOutlineColor() {
        mOutlineColor.enableButtons(true);
        mOutlineColor.setmBColor(ObjectPropertiesBase.Text.getInstance().mTextOutlineColor);
    }

    private void initBackgroundColor() {
        mBgColor.enableButtons(true);
        mBgColor.setmBColor(ObjectPropertiesBase.Text.getInstance().mTextBackgroundColor);
    }

    private void initTextColor() {
        mTextColor.enableButtons(true);
        mTextColor.setmBColor(ObjectPropertiesBase.Text.getInstance().mTextColor);
    }

    private void initMargin() {
        mMargin.setUInt(ObjectPropertiesBase.Text.getInstance().mTextMargin);
    }

    private void initTextScale() {
        mTextScale.setmX(ObjectPropertiesBase.Text.getInstance().mTextScale.x);
        mTextScale.setmY(ObjectPropertiesBase.Text.getInstance().mTextScale.y);
    }

    private void initSaveBttn() {
        mSaveBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                ObjectPropertiesBase.Text.getInstance().mIsWaitForCreate = false;
                saveTextParams();
                if (ObjectPropertiesBase.Text.getInstance().mPreviousFragmentText == ObjectPropertiesBase.Text.PreviousFragmentText.CreateNewText) {
                    ObjectPropertiesBase.Text.getInstance().mIsWaitForCreate = true;
                   // ObjectPropertiesBase.Text.getInstance().updateTextCompleted();
                    getActivity().onBackPressed();
                }

                if(!ObjectPropertiesBase.Text.getInstance().mIsWaitForCreate)
                    ObjectPropertiesBase.Text.getInstance().mPreviousFragmentText = ObjectPropertiesBase.Text.PreviousFragmentText.ObjectProperties;
            }
        });
    }


    private void saveSelectedFont() {


    }

    private SMcVariantLogFont getMcVariantLogFontFromView() {
        SMcVariantLogFont logFont = new SMcVariantLogFont();
        logFont.LogFont = new SMcVariantLogFont.LOGFONT();
        logFont.LogFont.lfFaceName = String.valueOf(mFontNameEt.getText());
        logFont.LogFont.lfHeight = mFontSizeEt.getInt();
        logFont.LogFont.lfItalic = (byte) (mItalicCB.isChecked() ? 1 : 0);
        logFont.LogFont.lfUnderline = (byte) (mUnderLineCB.isChecked() ? 1 : 0);
        logFont.LogFont.lfWeight = (mBoldCB.isChecked() ? 700 : 400);
        logFont.bIsUnicode = mLogFontIsUnicodeCb.isChecked();

        return logFont;
    }

    private SMcFileSource getMcFileSourceFromView()
    {
        boolean isMemoryBuffer = mFileFontIsMemoryBufferCB.isChecked();
        String strFileName = mFileFontFileChooser.getDirPath();
        SMcFileSource sImageSource = null;
        if (isMemoryBuffer) {
            byte[] fileAsByteArr = Funcs.getByteArrFromFilePath(strFileName);
            sImageSource = new SMcFileSource(fileAsByteArr);
        } else {
            sImageSource = new SMcFileSource(strFileName);
        }
        return sImageSource;
    }

    private void saveTextParams() {
        ObjectPropertiesBase.Text.getInstance().mTextAlignment = (EAxisXAlignment) mAlignmentSWL.getSelectedItem();
        ObjectPropertiesBase.Text.getInstance().mNeverUpsideDown = (IMcTextItem.ENeverUpsideDownMode) mNeverUpsideDownMode.getSelectedItem();
        ObjectPropertiesBase.Text.getInstance().mTextRtlReadingOrder = mRtlReadingOrderCb.isChecked();
        ObjectPropertiesBase.Text.getInstance().mTextIsUnicode = mTxtIsUnicodeCb.isChecked();
        ObjectPropertiesBase.Text.getInstance().mTextOutlineColor = mOutlineColor.getmBColor();
        ObjectPropertiesBase.Text.getInstance().mTextBackgroundColor = mBgColor.getmBColor();
        ObjectPropertiesBase.Text.getInstance().mTextColor = mTextColor.getmBColor();
        ObjectPropertiesBase.Text.getInstance().mTextMargin = mMargin.getUInt();
        ObjectPropertiesBase.Text.getInstance().mTextScale = mTextScale.getVector2D();

        if(mFontText.getText() != null && mFontText.getText().length() > 0)
            ObjectPropertiesBase.Text.getInstance().mTextString = mFontText.getText().toString();
        else
            ObjectPropertiesBase.Text.getInstance().mTextString ="";
    }

    private void inflateViews() {
        mSaveBttn = (Button) mRootView.findViewById(R.id.object_properties_text_save_bttn);
        mFontText =  (EditText) mRootView.findViewById(R.id.text_text);
        mTextScale = (TwoDFVector) mRootView.findViewById(R.id.text_scale);
        mMargin = (NumericEditTextLabel) mRootView.findViewById(R.id.text_margin);
        mTextColor = (SelectColor) mRootView.findViewById(R.id.text_color_sc);
        mBgColor = (SelectColor) mRootView.findViewById(R.id.text_background_color_sc);
        mOutlineColor = (SelectColor) mRootView.findViewById(R.id.text_outline_color_sc);
        mLogFontIsUnicodeCb = (CheckBox) mRootView.findViewById(R.id.font_is_unicode_cb);
        mTxtIsUnicodeCb = (CheckBox) mRootView.findViewById(R.id.text_is_unicode_cb);
        mTxtUseExistingCb = (CheckBox) mRootView.findViewById(R.id.text_UseExisting_cb);
        mOutlineWidthEt = (NumericEditTextLabel) mRootView.findViewById(R.id.text_outline_width_et);
        mRtlReadingOrderCb = (CheckBox) mRootView.findViewById(R.id.text_rtl_reading_order_cb);
        mAlignmentSWL = (SpinnerWithLabel) mRootView.findViewById(R.id.object_properties_text_txt_alignment_swl);
        mFontSizeEt = (NumericEditTextLabel) mRootView.findViewById(R.id.text_font_size_et);
        mFontNameEt = (EditText) mRootView.findViewById(R.id.text_font_name_et);
        mItalicCB = (CheckBox) mRootView.findViewById(R.id.text_italic_cb);
        mBoldCB = (CheckBox) mRootView.findViewById(R.id.text_bold_cb);
        mUnderLineCB = (CheckBox) mRootView.findViewById(R.id.text_underline);
        mLogFontRB = (RadioButton) mRootView.findViewById(R.id.text_log_font_rb);
        mFileFontRB = (RadioButton) mRootView.findViewById(R.id.text_file_font_rb);
        mLogFontLL = (LinearLayout) mRootView.findViewById(R.id.text_log_font_ll);
        mFileFontLL = (LinearLayout) mRootView.findViewById(R.id.text_file_font_ll);
        mFileFontFileChooser = (FileChooserEditTextLabel) mRootView.findViewById(R.id.text_file_font_file_chooser);
        mFileFontSizeET = (NumericEditTextLabel) mRootView.findViewById(R.id.text_file_font_size_et);
        mFileFontIsMemoryBufferCB = (CheckBox) mRootView.findViewById(R.id.text_file_font_memory_buffer_cb);
        mSelectFromListLV = (ListView) mRootView.findViewById(R.id.text_font_list_lv);
        mAddFontBttn = (Button) mRootView.findViewById(R.id.text_add_font_bttn);
        mUpdateFontBttn = (Button) mRootView.findViewById(R.id.text_update_font_bttn);
        mDeleteFontBttn = (Button) mRootView.findViewById(R.id.text_delete_font_bttn);
        mTextParametersLL = (LinearLayout)mRootView.findViewById(R.id.object_properties_text_parameters);
        mAddCharRangeBttn = (Button) mRootView.findViewById(R.id.text_add_char_range_bttn);
        mDeleteCharRangeBttn = (Button) mRootView.findViewById(R.id.text_delete_char_range_bttn);
        mCharRangeLV = (ListView) mRootView.findViewById(R.id.text_characters_range_lv);

        mCharRangeFrom = (EditText) mRootView.findViewById(R.id.text_char_range_from);
        mCharRangeTo = (EditText) mRootView.findViewById(R.id.text_char_range_to);
        mCharRangeFromVal = (EditText) mRootView.findViewById(R.id.text_char_range_from_val);
        mCharRangeToVal = (EditText) mRootView.findViewById(R.id.text_char_range_to_val);

        final boolean[] isTextChanged = {false};

        mCharRangeFrom.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {

            }

            @Override
            public void afterTextChanged(Editable s) {
                if(!isTextChanged[0]) {
                    isTextChanged[0] = true;
                    if (!s.toString().isEmpty()) {
                        mCharRangeFromVal.setText(String.valueOf((int) s.charAt(0)));
                    }
                    else
                        mCharRangeFromVal.setText("");
                    isTextChanged[0] = false;
                }
            }
        });
        mCharRangeTo.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {

            }

            @Override
            public void afterTextChanged(Editable s) {
                if(!isTextChanged[0]) {
                    isTextChanged[0] = true;
                    if (!s.toString().isEmpty()) {
                        mCharRangeToVal.setText(String.valueOf((int) s.charAt(0)));
                    }
                    else
                        mCharRangeToVal.setText("");
                    isTextChanged[0] = false;
                }
            }
        });
        mCharRangeFromVal.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {

            }

            @Override
            public void afterTextChanged(Editable s) {
                if(!isTextChanged[0]) {
                    isTextChanged[0] = true;
                    if (!s.toString().isEmpty()) {
                        mCharRangeFrom.setText(Character.toString((char) (Integer.parseInt(s.toString()))));
                    }
                    else
                        mCharRangeFrom.setText("");
                    isTextChanged[0] = false;
                }
            }
        });
        mCharRangeToVal.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {

            }

            @Override
            public void afterTextChanged(Editable s) {
                if(!isTextChanged[0]) {
                    isTextChanged[0] = true;
                    if (!s.toString().isEmpty()) {
                        mCharRangeTo.setText(Character.toString((char) (Integer.parseInt(s.toString()))));
                    }
                    else
                        mCharRangeTo.setText("");
                    isTextChanged[0] = false;
                }
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
        if (context instanceof OnFragmentInteractionListener) {
            mListener = (OnFragmentInteractionListener) context;
        } /*else {
            throw new RuntimeException(context.toString()
                    + " must implement OnFragmentInteractionListener");
        }*/
        if (context instanceof MapsContainerActivity) {
            ((MapsContainerActivity) context).mCurFragmentTag = ObjectPropertiesTabsText.class.getSimpleName();}
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    // update from private properties table
    @Override
    public void setCurrentFont(IMcFont curFont) {
        mCurrentFont = curFont;
        if(mCharactersRange!= null && mCharactersRange.size() > 0)
            mCharactersRange.clear();
        if(mCurrentFont != null) {
            try {
                IMcFont.SCharactersRange[] getCharactersRanges = mCurrentFont.GetCharactersRanges();
                if(getCharactersRanges != null)
                    mCharactersRange.addAll(Arrays.asList(getCharactersRanges));
                resetCharRangeListAdapter();
            } catch (MapCoreException e) {
                e.printStackTrace();
            }
            catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    /*private void updateCharRangeLV()
    {
        mCharRangeLV.deferNotifyDataSetChanged();
        Funcs.setListViewHeightBasedOnChildren(mCharRangeLV);

    }*/

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
