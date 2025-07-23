package com.elbit.mapcore.mcandroidtester.utils.customviews;

import android.content.Context;
import android.content.res.TypedArray;
import androidx.fragment.app.Fragment;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.model.AMCTMapDevice;
import com.elbit.mapcore.mcandroidtester.utils.DirectoryChooserDialog;

import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;

/**
 * Created by chevishv on 22/06/2016.
 */
public class FileChooserEditTextLabel extends LinearLayout {
    public EditText getmFilePathEt() {
        return mFilePathEt;
    }

    private final EditText mFilePathEt;
    Context mContext;
    Method mFilePathSaveMethod;
    private String mChosenDir = "";
    boolean mEnableFileChoosing;
    private TypedArray mTypedArray;
    private String mStrFolderPath = "";

    public void setStrFolderPath(String strFolderPath) {
        this.mStrFolderPath = strFolderPath;
        setDirPath(strFolderPath);
    }


    public Button getMfilePickerButton() {
        return mfilePickerButton;
    }

    private Button mfilePickerButton;

    public String getDirPath() {
        return ((EditText) this.findViewById(R.id.edittext_in_cv_fel)).getText().toString();
    }
    public void setDirPath(String path) {
        ((EditText) this.findViewById(R.id.edittext_in_cv_fel)).setText(path);
    }
    Fragment mContainerFragmnt;

    public Fragment getmContainerFragmnt() {
        return mContainerFragmnt;
    }

    public void setmContainerFragmnt(Fragment mContainerFragmnt) {
        this.mContainerFragmnt = mContainerFragmnt;
    }


    public FileChooserEditTextLabel(Context context, AttributeSet attrs) {
        super(context, attrs);
        mContext = context;
        LayoutInflater inflater = (LayoutInflater) context
                .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        inflater.inflate(R.layout.cv_file_chooser_edittext_label, this);
        mFilePathEt=((EditText) this.findViewById(R.id.edittext_in_cv_fel));
        if (!isInEditMode()) {
            mTypedArray = context.obtainStyledAttributes(attrs, R.styleable.FileChooserEditTextLabel);
            setLabel();
            setEnableFileChoosing();
            setBttn();
        }
    }

    private void setEnableFileChoosing() {
        mEnableFileChoosing = (mTypedArray.getBoolean(R.styleable.FileChooserEditTextLabel_fel_enable_file_choosing, false));
    }

    public void setEnableFileChoosing(boolean bEnableFileChoosing)
    {
        this.mEnableFileChoosing = bEnableFileChoosing;
    }

    public void setmFilePathSaveMethod(Method mFilePathSaveMethod) {
        this.mFilePathSaveMethod = mFilePathSaveMethod;
    }

    private void setBttn() {
        mfilePickerButton = (Button) findViewById(R.id.filePickerBttn);
        mfilePickerButton.setOnClickListener(new OnClickListener() {
            @Override
            public void onClick(View v) {
                //openFolderPicker();
                openAntExplorerFolderPickerDialog();
            }
        });

    }

    private void openAntExplorerFolderPickerDialog() {
        DirectoryChooserDialog directoryChooserDialog =
                new DirectoryChooserDialog(mContext,
                        new DirectoryChooserDialog.ChosenDirectoryListener() {
                            @Override
                            public void onChosenDir(String chosenDir) {
                                mChosenDir = chosenDir;
                                EditText folderPathET = (EditText) FileChooserEditTextLabel.this.findViewById(R.id.edittext_in_cv_fel);
                                if (folderPathET != null && mChosenDir != null)
                                    folderPathET.setText(mChosenDir);
                                //((OnFolderChosenListener)mContainerFragmnt).onFolderChosen(getId(),mChosenDir);

                            }
                        },mEnableFileChoosing,getDirPath());
        // Toggle new folder button enabling
        //directoryChooserDialog.setNewFolderEnabled(m_newFolderEnabled);
        // Load directory chooser dialog for initial 'mChosenDir' directory.
        // The registered callback will be called upon final directory selection.
        mChosenDir = getDirPath();
        directoryChooserDialog.chooseDirectory(mChosenDir);
        //m_newFolderEnabled = ! m_newFolderEnabled;

    }




    private void setLabel() {
        TextView textView = (TextView) findViewById(R.id.label_in_cv);
        textView.setText(mTypedArray.getText(R.styleable.FileChooserEditTextLabel_fel_labelText));
    }

    //@Override
    public void saveDirPath(Method filePathSaveMethod) {
        try {
            filePathSaveMethod.invoke(AMCTMapDevice.getInstance(), getDirPath());
        } catch (IllegalAccessException e) {
            e.printStackTrace();
        } catch (InvocationTargetException e) {
            e.printStackTrace();
        }
    }


}