package com.elbit.mapcore.mcandroidtester.utils.customviews;

import android.content.Context;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.CheckBox;
import android.widget.LinearLayout;

import com.elbit.mapcore.mcandroidtester.R;

import com.elbit.mapcore.Interfaces.Map.IMcMapCamera;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;

/**
 * Created by tc97803 on 17/01/2017.
 */
public class SaveParamsData extends LinearLayout {
    private Context mContext;
    private CheckBox mSaveUserDataCB;
    private CheckBox mSavePropertiesNamesCB;
    private SpinnerWithLabel mVersionCompabilitySpinner;
    private View mView;
    private ArrayAdapter<IMcOverlayManager.ESavingVersionCompatibility> versionCompabilityAdapter;

    public SaveParamsData(Context context, AttributeSet attrs, int defStyleAttr) {
        super(context, attrs, defStyleAttr);
    }

    public SaveParamsData(Context context, AttributeSet attrs) {
        super(context, attrs);
        InflateLayout(context);
    }

    private void InflateLayout(Context context)
    {
        LayoutInflater inflater = (LayoutInflater) context
                .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        mView = inflater.inflate(R.layout.cv_save_params_data, this);
        mContext = context;

        inflateGeneralPropertiesViews();
    }

    private void inflateGeneralPropertiesViews() {
        mSaveUserDataCB=(CheckBox) mView.findViewById(R.id.cv_save_data_params_save_user_data_cb);
        mSavePropertiesNamesCB = (CheckBox) mView.findViewById(R.id.cv_save_data_params_save_properties_names_cb);
        mVersionCompabilitySpinner=(SpinnerWithLabel)mView.findViewById(R.id.cv_save_data_params_version_compatibility_swl);
        versionCompabilityAdapter = new ArrayAdapter<>(getContext(), android.R.layout.simple_spinner_item, IMcOverlayManager.ESavingVersionCompatibility.values());
        mVersionCompabilitySpinner.setAdapter(versionCompabilityAdapter);
        mVersionCompabilitySpinner.setSelection(versionCompabilityAdapter.getPosition(IMcOverlayManager.ESavingVersionCompatibility.ESVC_LATEST));
    }

    public boolean getSaveUserData()
    {
        return mSaveUserDataCB.isChecked();
    }

    public boolean getSavePropertiesNames()
    {
        return mSavePropertiesNamesCB.isChecked();
    }

    public IMcOverlayManager.ESavingVersionCompatibility getSavingVersionCompatibility()
    {
        int position = mVersionCompabilitySpinner.getSelectedItemPosition();
        return versionCompabilityAdapter.getItem(position);
    }

    public void setCBSavePropertiesDisable() {
        mSavePropertiesNamesCB.setEnabled(false);
    }
}
