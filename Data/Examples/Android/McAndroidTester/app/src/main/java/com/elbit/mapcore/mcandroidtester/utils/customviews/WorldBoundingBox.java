package com.elbit.mapcore.mcandroidtester.utils.customviews;

import android.content.Context;
import android.content.res.TypedArray;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.EditText;
import android.widget.LinearLayout;

import com.elbit.mapcore.mcandroidtester.R;

import com.elbit.mapcore.Structs.SMcBox;
import com.elbit.mapcore.Structs.SMcVector3D;

/**
 * Created by tc99382 on 15/11/2016.
 */
public class WorldBoundingBox extends LinearLayout {
    SMcBox mWorldBoundingBox;
    boolean mIsEnabled;

    public WorldBoundingBox(Context context, AttributeSet attrs) {
        super(context, attrs);
        inflateLayout(context);
        initIsEnabledAttr(context, attrs);

    }

    @Override
    public void setEnabled(boolean enabled) {
        super.setEnabled(enabled);

        // Disable or enable all child views
        for (int i = 0; i < getChildCount(); i++) {
            View child = getChildAt(i);
            child.setEnabled(enabled);
        }

        // Optionally, change appearance to indicate the disabled state
        setAlpha(enabled ? 1.0f : 0.5f);
    }

    private void initIsEnabledAttr(Context context, AttributeSet attrs) {
        TypedArray typedArray = context.obtainStyledAttributes(attrs, R.styleable.WorldBoundingBox);
        mIsEnabled = typedArray.getBoolean(R.styleable.WorldBoundingBox_world_bounding_box_enable, true);
    }

    private void inflateLayout(Context context) {
        LayoutInflater inflater = (LayoutInflater) context
                .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        inflater.inflate(R.layout.cv_world_bounding_box, this);
    }

    private void initBoundingBoxET(int editTextId, double value) {
        EditText editText = ((EditText) this.findViewById(editTextId));
        editText.setText(String.valueOf(value));
        editText.setEnabled(mIsEnabled);
    }

    private void initMinVertex(SMcVector3D minVertex) {
        initBoundingBoxET(R.id.min_point_x, minVertex.x);
        initBoundingBoxET(R.id.min_point_y, minVertex.y);
        initBoundingBoxET(R.id.min_point_z, minVertex.z);
    }

    private void initMaxVertex(SMcVector3D maxVertex) {
        initBoundingBoxET(R.id.max_point_x, maxVertex.x);
        initBoundingBoxET(R.id.max_point_y, maxVertex.x);
        initBoundingBoxET(R.id.max_point_z, maxVertex.x);

    }

    public void initWorldBoundingBox(SMcBox worldBoundingBox) {
        mWorldBoundingBox = worldBoundingBox;
        initMaxVertex(worldBoundingBox.MaxVertex);
        initMinVertex(worldBoundingBox.MinVertex);
    }

    public double getETVal(int editTextId) {
        EditText editText = ((EditText) this.findViewById(editTextId));
        return Double.valueOf(String.valueOf(editText.getText()));
    }

    public SMcBox getWorldBoundingBox() {
        return new SMcBox(getETVal(R.id.min_point_x), getETVal(R.id.min_point_y), getETVal(R.id.min_point_z), getETVal(R.id.max_point_x), getETVal(R.id.max_point_y), getETVal(R.id.max_point_z));
    }
}
