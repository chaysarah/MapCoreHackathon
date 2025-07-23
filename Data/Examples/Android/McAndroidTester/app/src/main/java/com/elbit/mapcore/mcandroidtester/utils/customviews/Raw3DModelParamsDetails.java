package com.elbit.mapcore.mcandroidtester.utils.customviews;

import android.content.Context;
import android.os.Bundle;
import android.os.Parcelable;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.EditText;
import android.widget.FrameLayout;
import android.widget.LinearLayout;

import androidx.annotation.IdRes;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import com.elbit.mapcore.Interfaces.Calculations.GridCoordSystem.IMcGridCoordinateSystem;
import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcMapProduction;
import com.elbit.mapcore.Interfaces.Map.IMcRaw3DModelMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcRawVector3DExtrusionMapLayer;
import com.elbit.mapcore.Structs.SMcBox;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.OnCreateCoordinateSystemListener;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.GridCoordinateSysFragment;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by tc97803 on 05/01/2017.
 */
public class Raw3DModelParamsDetails extends LinearLayout {

    View mView;
    Fragment mFragment;
    private Button mSaveButton;
    private LinearLayout mParamsLinearLayout;
    private Context mContext;
    private CheckBox mNonDefaultClipRect;
    private WorldBoundingBox mClipRect;
    private CheckBox mNonDefaultTilingScheme;
    private TilingScheme mTilingScheme;
    private GridCoordinateSysFragment mGridCoordinateSysFragment;

    private NumericEditTextLabel mTargetHeightResolution;
    private boolean mTinyVisibility;

    public Raw3DModelParamsDetails(Context context) {
        super(context);
        InflateLayout(context);
    }

    public Raw3DModelParamsDetails(Context context, AttributeSet attrs) {
        super(context, attrs);
        InflateLayout(context);
    }

    @Nullable
    @Override
    protected Parcelable onSaveInstanceState() {
        Bundle bundle = new Bundle();
        bundle.putParcelable("superState2", super.onSaveInstanceState());

       /* if(mFragment.getClass() == GridCoordinateSysFragment.class
                && ((GridCoordinateSysFragment)mFragment).getRadioButtonOptions() == GridCoordinateSysFragment.RadioButtonOptions.CreateNew) {
            bundle.putSerializable(COORDINATE_SYSTEM, new AMCTSerializableObject( mCoordSysSpinner.getSelectedItem()));
            bundle.putSerializable(DATUM, new AMCTSerializableObject(mDatumSpinner.getSelectedItem()));
        }*/
        return bundle;
    }

   /*  @Override
    protected void onRestoreInstanceState(Parcelable state) {
       if (state instanceof Bundle) // implicit null check
        {
            Bundle bundle = (Bundle) state;
            AMCTSerializableObject mcObject = (AMCTSerializableObject) bundle.getSerializable(COORDINATE_SYSTEM);
            if (mcObject != null && mcObject.getMcObject() != null)
                mCoordSysSpinnerValue = ((IMcGridCoordinateSystem.EGridCoordSystemType) mcObject.getMcObject()).getValue();

            AMCTSerializableObject mcObjectDatum = (AMCTSerializableObject) bundle.getSerializable(DATUM);
            if (mcObjectDatum != null && mcObjectDatum.getMcObject() != null)
                mDatumSpinnerValue = ((IMcGridCoordinateSystem.EDatumType) mcObjectDatum.getMcObject()).getValue();

            state = bundle.getParcelable("superState2");
        }
        super.onRestoreInstanceState(state);
    }*/

    FrameLayout frameLayout;

    private void InflateLayout(Context context) {
        LayoutInflater inflater = (LayoutInflater) context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        mView = inflater.inflate(R.layout.cv_raw_3d_model_params_details, this);
        mContext = context;
        mSaveButton = (Button) mView.findViewById(R.id.raw_3d_model_params_save_btn);
        mParamsLinearLayout = (LinearLayout) mView.findViewById(R.id.raw_3d_model_params_ll);
        mParamsLinearLayout.setVisibility(View.VISIBLE);
        mNonDefaultClipRect = (CheckBox) mView.findViewById(R.id.raw_3d_model_non_default_clip_rect);
        mNonDefaultTilingScheme = (CheckBox) mView.findViewById(R.id.raw_3d_model_non_default_tiling_scheme);
        mTargetHeightResolution = (NumericEditTextLabel)mView.findViewById(R.id.raw_3d_model_target_highest_resolution);
        mTargetHeightResolution.setFloat(new IMcMapProduction.S3DModelConvertParams().fTargetHighestResolution);
        if(!mTinyVisibility) {
            FragmentManager fragmentManager = ((AppCompatActivity) mContext).getSupportFragmentManager();
            mGridCoordinateSysFragment = new GridCoordinateSysFragment();
            FragmentTransaction transaction = fragmentManager.beginTransaction();
            String tag = "gridRaw3DTargetCoordinateSysFragment";
            transaction.addToBackStack(tag);// "mGridCoordinateSysFragment"
            transaction.add(R.id.raw_3d_model_grid_coord_sys_target, mGridCoordinateSysFragment, tag).commit();
            frameLayout =  (FrameLayout) mView.findViewById(R.id.raw_3d_model_grid_coord_sys_target);
        }

        mClipRect = (WorldBoundingBox) mView.findViewById(R.id.raw_3d_model_clip_rect);
        mTilingScheme = (TilingScheme) mView.findViewById(R.id.raw_3d_model_tiling_scheme);

        mClipRect.setEnabled(false);
        mTilingScheme.setEnabled(false);

        mNonDefaultClipRect.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                mClipRect.setEnabled(isChecked);
            }
        });

        mNonDefaultTilingScheme.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                mTilingScheme.setEnabled(isChecked);
            }
        });
        initSave();
    }

    private void initSave() {
        mSaveButton.setVisibility(GONE);
        mSaveButton.setOnClickListener(new OnClickListener() {
            @Override
            public void onClick(View v) {

            }
        });
    }
    private void addCoordSysFragment(@IdRes int containerViewId, String tag) {
        FragmentManager fragmentManager = ((AppCompatActivity) mContext).getSupportFragmentManager();
        mGridCoordinateSysFragment = new GridCoordinateSysFragment();
        FragmentTransaction transaction = fragmentManager.beginTransaction();
        transaction.addToBackStack(tag);// "mGridCoordinateSysFragment"
        transaction.add(containerViewId, mGridCoordinateSysFragment, tag).commit();
    }

    public float getTargetHighestResolution()
    {
        return ((NumericEditTextLabel)mView.findViewById(R.id.raw_3d_model_target_highest_resolution)).getFloat();
    }

    public IMcGridCoordinateSystem getGridCoordinateSystem()
    {
        return mGridCoordinateSysFragment != null ? mGridCoordinateSysFragment.getSelectedGridCoordinateSystem() : null;
    }

    public SMcBox getClipRect()
    {
        return mNonDefaultClipRect.isChecked() ?  mClipRect.getWorldBoundingBox() : null;
    }

    public IMcMapLayer.STilingScheme getTilingScheme()
    {
        return mNonDefaultTilingScheme.isChecked() && mTilingScheme != null ?  mTilingScheme.getTilingScheme() : null;
    }

    public void setTinyVisibility() {
        mTinyVisibility = true;
        mNonDefaultTilingScheme.setVisibility(GONE);
        mTilingScheme.setVisibility(GONE);
        mNonDefaultTilingScheme.setVisibility(GONE);
        frameLayout.setVisibility(GONE);
    }
}
