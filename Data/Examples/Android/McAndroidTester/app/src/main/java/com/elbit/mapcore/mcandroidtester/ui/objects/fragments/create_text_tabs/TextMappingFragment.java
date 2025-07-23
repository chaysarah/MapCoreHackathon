package com.elbit.mapcore.mcandroidtester.ui.objects.fragments.create_text_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AbsListView;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.ListView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.model.AMCTFontMapping;
import com.elbit.mapcore.mcandroidtester.ui.adapters.TTFFileMappingAdapter;
import com.elbit.mapcore.mcandroidtester.ui.objects.ObjectPropertiesBase;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SpinnerWithLabel;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.ObjectOutputStream;
import java.util.ArrayList;
import java.util.Arrays;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcLogFont;
import com.elbit.mapcore.Structs.SMcVariantLogFont;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link TextMappingFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link TextMappingFragment#newInstance} factory method to
 * create an instance of this fragment.
 *
 */
public class TextMappingFragment extends Fragment {

    private View mRootView;
    private EditText mMappingFontNameEt;
    private Button mAddMappingBttn;
    private SpinnerWithLabel mMappingAndroidFontsSWL;
    private CheckBox mMappingItalicCB;
    private CheckBox mMappingBoldCB;
    private CheckBox mMappingUnderLineCB;
    private ListView mMappingLV;
    private Button mDeleteMappingBttn;
    private Button mUpdateMappingBttn;
    private Button mSaveBttn;
    private OnFragmentInteractionListener mListener;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment TextMappingFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static TextMappingFragment newInstance() {
        TextMappingFragment fragment = new TextMappingFragment();
        return fragment;
    }
    public TextMappingFragment() {
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
        mRootView = inflater.inflate(R.layout.fragment_text_mapping, container, false);
        inflateViews();
        initMapping();
        return mRootView;
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
           // throw new RuntimeException(context.toString()
            //        + " must implement OnFragmentInteractionListener");
        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    private void initMapping() {

        initMappingDetailsViews();

        initMappingList();
        initAddMappingBttn();
        initDeleteBttn();
        initUpdateBttn();
        initSaveBttn();
    }

    private void initUpdateBttn() {
        mUpdateMappingBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveUpdateMapping();
            }
        });
    }

    private void saveUpdateMapping() {
        ArrayList<IMcLogFont.SLogFontToTtfFile> aLogFontsMapAsArr;

        IMcLogFont.SLogFontToTtfFile[] sLogFontToTtfFiles = ObjectPropertiesBase.Text.getInstance().mTextFontsMap /*IMcLogFont.Static.GetLogFontToTtfFileMap()*/;
        if (sLogFontToTtfFiles != null) {
            final int selectPosition = mMappingLV.getCheckedItemPosition();
            aLogFontsMapAsArr = new ArrayList<>(Arrays.asList(sLogFontToTtfFiles));
            IMcLogFont.SLogFontToTtfFile selectedMapping = aLogFontsMapAsArr.get(selectPosition);
            selectedMapping = setMappingValuesFromViews(selectedMapping);
            aLogFontsMapAsArr.set(selectPosition, selectedMapping);
            final IMcLogFont.SLogFontToTtfFile[] newSLogFontToTtfFiles = aLogFontsMapAsArr.toArray(new IMcLogFont.SLogFontToTtfFile[aLogFontsMapAsArr.size()]);
            Funcs.runMapCoreFunc(new Runnable() {
                @Override
                public void run() {
                    try {
                        IMcLogFont.Static.SetLogFontToTtfFileMap(newSLogFontToTtfFiles);
                        ObjectPropertiesBase.Text.getInstance().mTextFontsMap = newSLogFontToTtfFiles;
                    } catch (MapCoreException e) {
                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetLogFontToTtfFileMap");
                    } catch (Exception e) {
                        e.printStackTrace();
                    }

                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            resetMappingListAdapter();
                            resetMappingViews();
                            setMappingBttnsEnable(false);
                            mMappingLV.setItemChecked(selectPosition, false);
                        }
                    });

                }
            });

        }
    }

    private IMcLogFont.SLogFontToTtfFile setMappingValuesFromViews(IMcLogFont.SLogFontToTtfFile mapping) {
        mapping.LogFont.LogFont.lfItalic = (byte) (mMappingItalicCB.isChecked() ? 1 : 0);
        mapping.LogFont.LogFont.lfUnderline = (byte) (mMappingUnderLineCB.isChecked() ? 1 : 0);
        mapping.LogFont.LogFont.lfWeight = (mMappingBoldCB.isChecked() ? 700 : 400);
        mapping.LogFont.LogFont.lfFaceName = String.valueOf(mMappingFontNameEt.getText());
        mapping.strTtfFileFullPathName = ObjectPropertiesTabsText.ANDROID_FONTS_PATH + String.valueOf(mMappingAndroidFontsSWL.getSelectedItem());
        return mapping;
    }

    private void initSaveBttn()
    {
        mSaveBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                saveMappingToFile();
            }
        });
    }

    private void initDeleteBttn() {

        mDeleteMappingBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                ArrayList<IMcLogFont.SLogFontToTtfFile> aLogFontsMapAsArr;

                IMcLogFont.SLogFontToTtfFile[] sLogFontToTtfFiles = ObjectPropertiesBase.Text.getInstance().mTextFontsMap;
                if (sLogFontToTtfFiles != null) {
                    aLogFontsMapAsArr = new ArrayList<>(Arrays.asList(sLogFontToTtfFiles));
                    aLogFontsMapAsArr.remove(mMappingLV.getCheckedItemPosition());
                    mMappingLV.setItemChecked(mMappingLV.getCheckedItemPosition(), false);
                    final IMcLogFont.SLogFontToTtfFile[] lfa = aLogFontsMapAsArr.toArray(new IMcLogFont.SLogFontToTtfFile[aLogFontsMapAsArr.size()]);
                    final ArrayList<IMcLogFont.SLogFontToTtfFile> finalLogFontsMapAsArr = (ArrayList<IMcLogFont.SLogFontToTtfFile>) aLogFontsMapAsArr.clone();
                    Funcs.runMapCoreFunc(new Runnable() {
                        @Override
                        public void run() {
                            try {
                                IMcLogFont.Static.SetLogFontToTtfFileMap(lfa);
                            } catch (MapCoreException e) {
                                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetLogFontToTtfFileMap");
                            } catch (Exception e) {
                                e.printStackTrace();
                            }
                            getActivity().runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    ObjectPropertiesBase.Text.getInstance().mTextFontsMap = finalLogFontsMapAsArr.toArray(lfa);
                                    ObjectPropertiesBase.Text.getInstance().mTextCurMapping = null;
                                    ObjectPropertiesBase.Text.getInstance().mTextCurMappingPos = -1;
                                    resetMappingListAdapter();
                                    resetMappingViews();
                                    setMappingBttnsEnable(false);
                                }
                            });
                        }
                    });
                }
            }
        });
    }

    private void initAddMappingBttn() {
        mAddMappingBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                addNewMapping();
                resetMappingViews();
            }
        });
    }

    private void resetMappingViews() {
        mMappingItalicCB.setChecked(false);
        mMappingBoldCB.setChecked(false);
        mMappingUnderLineCB.setChecked(false);
        mMappingAndroidFontsSWL.setSelection(0);
        mMappingFontNameEt.setText("");;
    }

    private void initMappingDetailsViews() {
        initAndroidFonts();
        if (ObjectPropertiesBase.Text.getInstance().mTextCurMapping != null) {
            mMappingItalicCB.setChecked(ObjectPropertiesBase.Text.getInstance().mTextCurMapping.LogFont.LogFont.lfItalic == 1);
            mMappingBoldCB.setChecked(ObjectPropertiesBase.Text.getInstance().mTextCurMapping.LogFont.LogFont.lfItalic == 400);
            mMappingUnderLineCB.setChecked(ObjectPropertiesBase.Text.getInstance().mTextCurMapping.LogFont.LogFont.lfUnderline == 1);
        }
    }

    private void initAndroidFonts() {
        File file = new File(ObjectPropertiesTabsText.ANDROID_FONTS_PATH);
        String[] fontNames = file.list();
        ArrayAdapter<String> mappingAndroidFontAdapter = new ArrayAdapter<>(getContext(), android.R.layout.simple_spinner_item, fontNames);
        mMappingAndroidFontsSWL.setAdapter(mappingAndroidFontAdapter);
        if (ObjectPropertiesBase.Text.getInstance().mTextCurMapping != null) {
            String strTtfFileFullPathName = ObjectPropertiesBase.Text.getInstance().mTextCurMapping.strTtfFileFullPathName;
            if(strTtfFileFullPathName != null)
                mMappingAndroidFontsSWL.setSelection(mappingAndroidFontAdapter.getPosition(strTtfFileFullPathName.replace(ObjectPropertiesTabsText.ANDROID_FONTS_PATH, "")));
        }
    }

    private void initMappingList() {
       //  if (ObjectPropertiesBase.Text.getInstance().mTextFontsMap == null)
        //     ObjectPropertiesBase.Text.getInstance().mTextFontsMap = getMappingFromFile();
       /* mMappingLV.setAdapter(null);
        if (ObjectPropertiesBase.Text.getInstance().mTextFontsMap != null) {

            ArrayAdapter mappingAdapter = new ArrayAdapter<>(getContext(), android.R.layout.simple_list_item_checked, ObjectPropertiesBase.Text.getInstance().mTextFontsMap);
            mMappingLV.setAdapter(mappingAdapter);
            mMappingLV.setChoiceMode(AbsListView.CHOICE_MODE_SINGLE);
            Funcs.setListViewHeightBasedOnChildren(mMappingLV);

            if (ObjectPropertiesBase.Text.getInstance().mTextCurMapping != null) {
                mMappingLV.setItemChecked(ObjectPropertiesBase.Text.getInstance().mTextCurMappingPos, true);
                setMappingBttnsEnable(true);
            }
            else
                ObjectPropertiesBase.Text.getInstance().mTextCurMappingPos = -1;
        }*/
       resetMappingListAdapter();
        mMappingLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                setMappingBttnsEnable(true);
                IMcLogFont.SLogFontToTtfFile curFont = (IMcLogFont.SLogFontToTtfFile) parent.getItemAtPosition(position);
                ObjectPropertiesBase.Text.getInstance().mTextCurMappingPos = position;
                ObjectPropertiesBase.Text.getInstance().mTextCurMapping = curFont;
                setCurMappingViews(curFont);
            }
        });
    }

    private void setMappingBttnsEnable(boolean isEnabled) {
        mDeleteMappingBttn.setEnabled(isEnabled);
        mUpdateMappingBttn.setEnabled(isEnabled);
        mAddMappingBttn.setEnabled(!isEnabled);
    }

    private void setCurMappingViews(IMcLogFont.SLogFontToTtfFile curFont) {
        // String curWindowsFontName = ;
        //mMappingWindowsFontsSWL.setSelection(((ArrayAdapter) mMappingWindowsFontsSWL.getAdapter()).getPosition(curWindowsFontName));
        if(curFont != null && curFont.strTtfFileFullPathName != null) {
            String curAndroidFontName = curFont.strTtfFileFullPathName.replace(ObjectPropertiesTabsText.ANDROID_FONTS_PATH, "");
            mMappingAndroidFontsSWL.setSelection(((ArrayAdapter) mMappingAndroidFontsSWL.getAdapter()).getPosition(curAndroidFontName));
        }
        mMappingFontNameEt.setText(curFont.LogFont.LogFont.lfFaceName);
        mMappingBoldCB.setChecked(curFont.LogFont.LogFont.lfWeight == 400 ? false : true);
        mMappingItalicCB.setChecked(curFont.LogFont.LogFont.lfItalic == 1 ? true : false);
        mMappingUnderLineCB.setChecked(curFont.LogFont.LogFont.lfUnderline == 1 ? true : false);
    }

    private void addNewMapping() {
        ArrayList<IMcLogFont.SLogFontToTtfFile> aLogFontsMapAsArr;


        IMcLogFont.SLogFontToTtfFile[] sLogFontToTtfFiles = ObjectPropertiesBase.Text.getInstance().mTextFontsMap;
        if (sLogFontToTtfFiles != null) {
            aLogFontsMapAsArr = new ArrayList<>(Arrays.asList(sLogFontToTtfFiles));
        } else
            aLogFontsMapAsArr = new ArrayList<>();
        IMcLogFont.SLogFontToTtfFile newLogFontsMap = new IMcLogFont.SLogFontToTtfFile();
        newLogFontsMap.LogFont = new SMcVariantLogFont();
        newLogFontsMap.LogFont.LogFont = new SMcVariantLogFont.LOGFONT();
        newLogFontsMap = setMappingValuesFromViews(newLogFontsMap);
        aLogFontsMapAsArr.add(newLogFontsMap);
        final IMcLogFont.SLogFontToTtfFile[] lfa = aLogFontsMapAsArr.toArray(new IMcLogFont.SLogFontToTtfFile[aLogFontsMapAsArr.size()]);
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    IMcLogFont.Static.SetLogFontToTtfFileMap(lfa);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetLogFontToTtfFileMap");
                } catch (Exception e) {
                    e.printStackTrace();
                }

                getActivity().runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        ObjectPropertiesBase.Text.getInstance().mTextFontsMap = lfa;
                        resetMappingListAdapter();
                    }
                });
            }
        });
    }

    private void resetMappingListAdapter() {

        mMappingLV.setAdapter(null);
        if (ObjectPropertiesBase.Text.getInstance().mTextFontsMap != null) {

            mMappingLV.setChoiceMode(AbsListView.CHOICE_MODE_SINGLE);
            TTFFileMappingAdapter mappingAdapter = new TTFFileMappingAdapter(getContext(), android.R.layout.simple_list_item_checked, Arrays.asList((ObjectPropertiesBase.Text.getInstance().mTextFontsMap)));
            mMappingLV.setAdapter(mappingAdapter);
            if (ObjectPropertiesBase.Text.getInstance().mTextCurMapping != null)
            {
                mMappingLV.setItemChecked(ObjectPropertiesBase.Text.getInstance().mTextCurMappingPos, true);
                setMappingBttnsEnable(true);
                setCurMappingViews(ObjectPropertiesBase.Text.getInstance().mTextCurMapping);
            }
            else
                ObjectPropertiesBase.Text.getInstance().mTextCurMappingPos = -1;
        }
        Funcs.setListViewHeightBasedOnChildren(mMappingLV);
    }

    private void saveMappingToFile() {
        AMCTFontMapping fontMapping = new AMCTFontMapping();
        FileOutputStream fos = null;
        try {
            fos = getContext().openFileOutput("fontMapping", Context.MODE_PRIVATE);
            ObjectOutputStream os = new ObjectOutputStream(fos);
            os.writeObject(fontMapping);
            os.close();
            fos.close();
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void inflateViews() {
        mSaveBttn = (Button) mRootView.findViewById(R.id.text_mapping_save_to_file_bttn);
        mMappingFontNameEt = (EditText) mRootView.findViewById(R.id.text_windows_font_name_et);
        mAddMappingBttn = (Button) mRootView.findViewById(R.id.text_add_mapping_bttn);
        mDeleteMappingBttn = (Button) mRootView.findViewById(R.id.text_delete_mapping_bttn);
        mUpdateMappingBttn = (Button) mRootView.findViewById(R.id.text_update_mapping_bttn);
        mMappingAndroidFontsSWL = (SpinnerWithLabel) mRootView.findViewById(R.id.text_mapping_android_font_swl);
        mMappingItalicCB = (CheckBox) mRootView.findViewById(R.id.text_mapping_italic_cb);
        mMappingBoldCB = (CheckBox) mRootView.findViewById(R.id.text_mapping_bold_cb);
        mMappingUnderLineCB = (CheckBox) mRootView.findViewById(R.id.text_mapping_underline);
        mMappingLV = (ListView) mRootView.findViewById(R.id.text_mapping_lv);
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
