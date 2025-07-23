package com.elbit.mapcore.mcandroidtester.utils.customviews;

import android.content.Context;

import androidx.annotation.IdRes;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.appcompat.app.AppCompatActivity;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.LinearLayout;

import com.elbit.mapcore.mcandroidtester.R;

/**
 * Created by tc97803 on 05/01/2017.
 */
public class RawStaticObjectsParamsDetails extends LinearLayout {

    View mView;
    Fragment mFragment;
    private LinearLayout mParamsLinearLayout;
    private Context mContext;
    private CheckBox mUseBuiltIndexingDataCB;
    private CheckBox mNonDefaultIndexDirCB;
    private FileChooserEditTextLabel mIndexDir;
    public RawStaticObjectsParamsDetails(Context context) {
        super(context);
        InflateLayout(context);
    }

    public RawStaticObjectsParamsDetails(Context context, AttributeSet attrs) {
        super(context, attrs);
        InflateLayout(context);
    }

    private void InflateLayout(Context context) {
        LayoutInflater inflater = (LayoutInflater) context
                .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        mView = inflater.inflate(R.layout.cv_raw_static_objects_params_details, this);
        mContext = context;
        mParamsLinearLayout = (LinearLayout) mView.findViewById(R.id.raw_static_objects_params_ll);
        mParamsLinearLayout.setVisibility(View.VISIBLE);
        mUseBuiltIndexingDataCB = (CheckBox) mView.findViewById(R.id.raw_static_objects_use_built);
        mNonDefaultIndexDirCB = (CheckBox) mView.findViewById(R.id.raw_static_objects_non_default_index_cb);
        mIndexDir = (FileChooserEditTextLabel) mView.findViewById(R.id.raw_static_objects_non_default_index_dir);
        //mIndexDir.setEnabled(false);
        // mUseBuiltIndexingDataCB.setChecked(true);
        mUseBuiltIndexingDataCB.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                mNonDefaultIndexDirCB.setEnabled(isChecked);
            }
        });
        mNonDefaultIndexDirCB.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                mIndexDir.setEnabled(isChecked);
            }
        });
    }

    public void SetVisibleUseBuilt(boolean isVisibleUseBuilt)
    {
        mUseBuiltIndexingDataCB.setVisibility(isVisibleUseBuilt? VISIBLE : INVISIBLE);
    }

    public boolean IsUseBuiltIndexingDataDir()
    {
        return mUseBuiltIndexingDataCB.isChecked();
    }

    public void IsUseBuiltIndexingDataDir(boolean isUseBuiltIndexingDataDir)
    {
         mUseBuiltIndexingDataCB.setChecked(isUseBuiltIndexingDataDir);
    }

    public boolean IsNonDefaultIndexDir() {
        return mNonDefaultIndexDirCB.isChecked();
    }

    public String getNonDefaultIndexDir(){
        return mIndexDir.getDirPath();
    }
}
