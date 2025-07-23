package com.elbit.mapcore.mcandroidtester.utils.customviews;

import android.content.Context;
import android.os.Bundle;
import android.os.Parcelable;
import androidx.annotation.IdRes;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;
import androidx.appcompat.app.AppCompatActivity;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;

import com.elbit.mapcore.Interfaces.Map.IMcRawVector3DExtrusionMapLayer;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.GridCoordinateSysFragment;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by tc97803 on 05/01/2017.
 */
public class RawVector3DExtrusionParamsDetails extends LinearLayout {

    View mView;
    Fragment mFragment;
    private IMcRawVector3DExtrusionMapLayer.SParams mParams;
    private IMcRawVector3DExtrusionMapLayer.SGraphicalParams mGraphicalParams;
    private Button mSaveButton;
    private LinearLayout mParamsLinearLayout;
    private Context mContext;
    private List<GridCoordinateSysFragment> mGridCoordinateSysFragmentList = new ArrayList<>();
    private EditText strHeightColumn,strObjectIDColumn, strRoofTextureIndexColumn,strSideTextureIndexColumn;
    private WorldBoundingBox mClipRect;
    private ExtrusionTexture mRoofDefaultTexture;
    private ExtrusionTexture mSideDefaultTexture;
    private TilingScheme mTilingScheme;
    // mUseBuiltIndexingDataCB mNonDefaultIndexDirCB mIndexDir
    public RawVector3DExtrusionParamsDetails(Context context) {
        super(context);
        InflateLayout(context);
    }

    public RawVector3DExtrusionParamsDetails(Context context, AttributeSet attrs) {
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

    private void InflateLayout(Context context) {
        LayoutInflater inflater = (LayoutInflater) context
                .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        mView = inflater.inflate(R.layout.cv_raw_vector_3d_extrusion_params_details, this);
        mContext = context;
        mSaveButton = (Button) mView.findViewById(R.id.raw_vector_3d_extrusion_params_save_btn);
        mParamsLinearLayout = (LinearLayout) mView.findViewById(R.id.raw_vector_3d_extrusion_params_ll);
        mParamsLinearLayout.setVisibility(View.VISIBLE);
        addCoordSysFragment(R.id.grid_coord_sys_source_container_fragment, "gridSourceCoordinateSysFragment");
        addCoordSysFragment(R.id.grid_coord_sys_target_container_fragment, "gridTargetCoordinateSysFragment");

        strHeightColumn = (EditText) mView.findViewById(R.id.str_height_column);
        strObjectIDColumn = (EditText) mView.findViewById(R.id.str_object_ID_column);
        strRoofTextureIndexColumn = (EditText) mView.findViewById(R.id.str_roof_texture_index_column);
        strSideTextureIndexColumn = (EditText) mView.findViewById(R.id.str_side_texture_index_column);
        mClipRect = (WorldBoundingBox)  mView.findViewById(R.id.raw_params_layers_clip_rect);
        mRoofDefaultTexture = (ExtrusionTexture)  mView.findViewById(R.id.roof_default_texture);
        mSideDefaultTexture = (ExtrusionTexture)  mView.findViewById(R.id.side_default_texture);
        mTilingScheme = (TilingScheme) mView.findViewById(R.id.raw_params_tiling_scheme);

        initSave();
    }

    private void initSave() {
        mSaveButton.setVisibility(INVISIBLE);
        mSaveButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                SaveParams();
            }
        });
    }

    private void addCoordSysFragment(@IdRes int containerViewId, String tag) {
        FragmentManager fragmentManager = ((AppCompatActivity) mContext).getSupportFragmentManager();
        GridCoordinateSysFragment gridCoordinateSysFragment = new GridCoordinateSysFragment();
        mGridCoordinateSysFragmentList.add(gridCoordinateSysFragment);
        FragmentTransaction transaction = fragmentManager.beginTransaction();
        transaction.addToBackStack(tag);// "gridCoordinateSysFragment"
        transaction.add(containerViewId, gridCoordinateSysFragment, tag).commit();
    }

    /*public void HideSaveButton()
    {
        mSaveButton.setVisibility(INVISIBLE);
    }*/

    public IMcRawVector3DExtrusionMapLayer.SParams getParamsToFragment()
    {
        SaveParams();
        return mParams;
    }

    public IMcRawVector3DExtrusionMapLayer.SGraphicalParams getGraphicalParamsToFragment()
    {
        SaveGraphicalParams();
        return mGraphicalParams;
    }

    private void SaveParams()
    {
        mParams = new IMcRawVector3DExtrusionMapLayer.SParams();
        mParams.pSourceCoordinateSystem = mGridCoordinateSysFragmentList.get(0).getSelectedGridCoordinateSystem();
        mParams.pTargetCoordinateSystem = mGridCoordinateSysFragmentList.get(1).getSelectedGridCoordinateSystem();
        mParams.strHeightColumn = strHeightColumn.getText().toString();
        mParams.strObjectIDColumn = strObjectIDColumn.getText().toString();
        mParams.strRoofTextureIndexColumn = strRoofTextureIndexColumn.getText().toString();
        mParams.strSideTextureIndexColumn = strSideTextureIndexColumn.getText().toString();
        mParams.pClipRect = mClipRect.mWorldBoundingBox;
        mParams.RoofDefaultTexture = mRoofDefaultTexture.getExtrusionTexture();
        mParams.SideDefaultTexture = mSideDefaultTexture.getExtrusionTexture();
        mParams.pTilingScheme = mTilingScheme.getTilingScheme();
    }

    private void SaveGraphicalParams()
    {
        mGraphicalParams = new IMcRawVector3DExtrusionMapLayer.SGraphicalParams();
        mGraphicalParams.strHeightColumn = strHeightColumn.getText().toString();
        mGraphicalParams.strObjectIDColumn = strObjectIDColumn.getText().toString();
        mGraphicalParams.strRoofTextureIndexColumn = strRoofTextureIndexColumn.getText().toString();
        mGraphicalParams.strSideTextureIndexColumn = strSideTextureIndexColumn.getText().toString();
        mGraphicalParams.RoofDefaultTexture = mRoofDefaultTexture.getExtrusionTexture();
        mGraphicalParams.SideDefaultTexture = mSideDefaultTexture.getExtrusionTexture();
    }

}
