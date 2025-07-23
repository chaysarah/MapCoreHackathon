package com.elbit.mapcore.mcandroidtester.utils.customviews;

import android.content.Context;
import android.util.AttributeSet;
import android.util.SparseBooleanArray;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.AbsListView;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.ListView;

import com.elbit.mapcore.General.CMcEnumBitField;
import com.elbit.mapcore.Interfaces.Map.IMcRawVector3DExtrusionMapLayer;
import com.elbit.mapcore.Structs.SMcFVector2D;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

public class ExtrusionTexture extends LinearLayout {

    private Context mContext;

    private FileChooserEditTextLabel mDataSource;
    private ListView mXPlacementLV;
    private ListView mYPlacementLV;

    private TwoDFVector mTextureScale;
    private Button mSaveBttn;

    private CMcEnumBitField<IMcRawVector3DExtrusionMapLayer.ETexturePlacementFlags> mCurrXPlacement;
    private CMcEnumBitField<IMcRawVector3DExtrusionMapLayer.ETexturePlacementFlags> mCurrYPlacement;

   // private IMcRawVector3DExtrusionMapLayer.SExtrusionTexture mExtrusionTexture;

    public ExtrusionTexture(Context context, AttributeSet attrs) {
        super(context, attrs);
        LayoutInflater inflater = (LayoutInflater) context
                .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        inflater.inflate(R.layout.cv_extrusion_texture, this);
        mContext = context;

        if (!isInEditMode()) {
            initViews();
           // initSave();
        }
    }

    public void setExtrusionTexture(IMcRawVector3DExtrusionMapLayer.SExtrusionTexture extrusionTexture)
    {
        if(extrusionTexture!= null) {
            mDataSource.setDirPath(extrusionTexture.strTexturePath);
            mTextureScale.setVector2D(extrusionTexture.TextureScale);
            initXPlacement(extrusionTexture.uXPlacementBitField);
            initYPlacement(extrusionTexture.uYPlacementBitField);
        }
    }

    public IMcRawVector3DExtrusionMapLayer.SExtrusionTexture getExtrusionTexture() {
        if(!mDataSource.getDirPath().isEmpty()) {
            IMcRawVector3DExtrusionMapLayer.SExtrusionTexture mExtrusionTexture = new IMcRawVector3DExtrusionMapLayer.SExtrusionTexture();
            mExtrusionTexture.strTexturePath = mDataSource.getDirPath();
            if (mTextureScale.getVector2D() != SMcFVector2D.getV2Zero())
                mExtrusionTexture.TextureScale = mTextureScale.getVector2D();
            mExtrusionTexture.uXPlacementBitField = getPlacement(mXPlacementLV);
            mExtrusionTexture.uYPlacementBitField = getPlacement(mYPlacementLV);
            return mExtrusionTexture;
        }
        else
            return null;
    }

    private void initXPlacement( CMcEnumBitField<IMcRawVector3DExtrusionMapLayer.ETexturePlacementFlags> uPlacementBitField) {
        mXPlacementLV.setAdapter(new ArrayAdapter<>(getContext(), android.R.layout.simple_list_item_multiple_choice, IMcRawVector3DExtrusionMapLayer.ETexturePlacementFlags.values()));
        mXPlacementLV.setChoiceMode(AbsListView.CHOICE_MODE_MULTIPLE);
        Funcs.setListViewHeightBasedOnChildren(mXPlacementLV);
        for (int i = 0; i < mXPlacementLV.getCount(); i++) {
            if (uPlacementBitField.IsSet((IMcRawVector3DExtrusionMapLayer.ETexturePlacementFlags) mXPlacementLV.getAdapter().getItem(i)))
                mXPlacementLV.setItemChecked(i, true);
        }
        mXPlacementLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                if (((IMcRawVector3DExtrusionMapLayer.ETexturePlacementFlags) mXPlacementLV.getAdapter().getItem(position)).compareTo(IMcRawVector3DExtrusionMapLayer.ETexturePlacementFlags.ETPF_NONE) == 0) {
                    SparseBooleanArray checkedItems = mXPlacementLV.getCheckedItemPositions();
                    for (int i = 0; i < checkedItems.size(); i++) {
                        if (checkedItems.valueAt(i) && i != position)
                            mXPlacementLV.setItemChecked(i, false);
                    }
                } else
                    mXPlacementLV.setItemChecked((IMcRawVector3DExtrusionMapLayer.ETexturePlacementFlags.ETPF_NONE.getValue()), false);
            }
        });
    }

    private void initYPlacement( CMcEnumBitField<IMcRawVector3DExtrusionMapLayer.ETexturePlacementFlags> uPlacementBitField) {
        mYPlacementLV.setAdapter(new ArrayAdapter<>(getContext(), android.R.layout.simple_list_item_multiple_choice, IMcRawVector3DExtrusionMapLayer.ETexturePlacementFlags.values()));
        mYPlacementLV.setChoiceMode(AbsListView.CHOICE_MODE_MULTIPLE);
        Funcs.setListViewHeightBasedOnChildren(mYPlacementLV);
        for (int i = 0; i < mYPlacementLV.getCount(); i++) {
            if (uPlacementBitField.IsSet((IMcRawVector3DExtrusionMapLayer.ETexturePlacementFlags) mXPlacementLV.getAdapter().getItem(i)))
                mYPlacementLV.setItemChecked(i, true);
        }
        mYPlacementLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                if (((IMcRawVector3DExtrusionMapLayer.ETexturePlacementFlags) mYPlacementLV.getAdapter().getItem(position)).compareTo(IMcRawVector3DExtrusionMapLayer.ETexturePlacementFlags.ETPF_NONE) == 0) {
                    SparseBooleanArray checkedItems = mYPlacementLV.getCheckedItemPositions();
                    for (int i = 0; i < checkedItems.size(); i++) {
                        if (checkedItems.valueAt(i) && i != position)
                            mYPlacementLV.setItemChecked(i, false);
                    }
                } else
                    mYPlacementLV.setItemChecked((IMcRawVector3DExtrusionMapLayer.ETexturePlacementFlags.ETPF_NONE.getValue()), false);
            }
        });
    }

    private CMcEnumBitField<IMcRawVector3DExtrusionMapLayer.ETexturePlacementFlags> getPlacement(ListView placementLV) {
        CMcEnumBitField<IMcRawVector3DExtrusionMapLayer.ETexturePlacementFlags> mCurrPlacement;
        mCurrPlacement = new CMcEnumBitField<>(IMcRawVector3DExtrusionMapLayer.ETexturePlacementFlags.ETPF_NONE);
        SparseBooleanArray checked = null;
        int len = placementLV.getCount();
        checked = placementLV.getCheckedItemPositions();
        for (int i = 0; i < len; i++) {
            if (checked.get(i)) {
                mCurrPlacement.Set((IMcRawVector3DExtrusionMapLayer.ETexturePlacementFlags) placementLV.getItemAtPosition(i));
            }
        }
        return mCurrPlacement;
    }

    private void initViews() {
        mDataSource = (FileChooserEditTextLabel) findViewById(R.id.extrusion_texture_data_source);
        mTextureScale = (TwoDFVector) findViewById(R.id.extrusion_texture_texture_scale_2dfvector);
        mXPlacementLV = (ListView) findViewById(R.id.extrusion_texture_uXPlacement);
        mYPlacementLV = (ListView) findViewById(R.id.extrusion_texture_uYPlacement);

        initXPlacement(new CMcEnumBitField<>(IMcRawVector3DExtrusionMapLayer.ETexturePlacementFlags.ETPF_NONE));
        initYPlacement(new CMcEnumBitField<>(IMcRawVector3DExtrusionMapLayer.ETexturePlacementFlags.ETPF_NONE));
    }
}
