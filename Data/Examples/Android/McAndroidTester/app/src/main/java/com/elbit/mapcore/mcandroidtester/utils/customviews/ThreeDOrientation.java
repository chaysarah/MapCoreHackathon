package com.elbit.mapcore.mcandroidtester.utils.customviews;

import android.content.Context;
import android.content.res.TypedArray;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.elbit.mapcore.mcandroidtester.R;

/**
 * Created by tc99382 on 29/11/2016.
 */
public class ThreeDOrientation extends LinearLayout {
    private NumericEditTextLabel mRollET;
    private NumericEditTextLabel mPitchET;
    private NumericEditTextLabel mYawET;
    private float mYaw;
    private float mPitch;
    private float mRoll;

    public ThreeDOrientation(Context context, AttributeSet attrs) {
        super(context, attrs);
        LayoutInflater inflater = (LayoutInflater) context
                .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        inflater.inflate(R.layout.cv_3_d_orientation, this);
        if (!isInEditMode()) {
            initViews();
            setLabel(context, attrs);
            setHints(context, attrs);
        }
    }

    private void setHints(Context context, AttributeSet attrs) {
        TypedArray typedArray = context.obtainStyledAttributes(attrs, R.styleable.ThreeDVector);

        mYawET.getEditText().setHint(typedArray.getText(R.styleable.ThreeDOrientation_three_d_orientation_raw_hint));
        mPitchET.getEditText().setHint(typedArray.getText(R.styleable.ThreeDOrientation_three_d_orientation_pitch_hint));
        mRollET.getEditText().setHint(typedArray.getText(R.styleable.ThreeDOrientation_three_d_orientation_roll_hint));
    }

    private void setLabel(Context context, AttributeSet attrs) {
        TypedArray typedArray = context.obtainStyledAttributes(attrs, R.styleable.ThreeDOrientation);
        TextView textView = (TextView) findViewById(R.id.three_d_orientation_label);
        textView.setText(typedArray.getText(R.styleable.ThreeDOrientation_three_d_orientation_label));
    }


    private void initViews() {
        mYawET = (NumericEditTextLabel) findViewById(R.id.three_d_orientation_yaw);
        mPitchET = (NumericEditTextLabel) findViewById(R.id.three_d_orientation_pitch);
        mRollET = (NumericEditTextLabel) findViewById(R.id.three_d_orientation_roll);
    }
    public float getmRoll() {
        float roll = 0;
        try {
            roll = Float.valueOf(mRollET.getText());
        } catch (Exception ex) {
            roll = 0;
        }
        return roll;
    }

    public void setmRoll(float mRoll) {
        this.mRoll = mRoll;
        mRollET.setText(String.valueOf(mRoll));
    }

    public float getmPitch() {
        float pitch = 0;
        try {
            pitch = Float.valueOf(mPitchET.getText());
        } catch (Exception ex) {
            pitch = 0;
        }
        return pitch;
    }

    public void setmPitch(float mPitch) {
        this.mPitch = mPitch;
        mPitchET.setText(String.valueOf(mPitch));
    }

    public float getmYaw() {
        float yaw = 0;
        try {
            yaw = Float.valueOf(mYawET.getText());
        } catch (Exception ex) {
            yaw = 0;
        }
        return yaw;
    }

    public void setmYaw(float mYaw) {
        this.mYaw = mYaw;
        mYawET.setText(String.valueOf(mYaw));
    }


}
