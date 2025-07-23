package com.elbit.mapcore.mcandroidtester.utils.customviews;

import android.content.Context;
import android.content.res.TypedArray;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.elbit.mapcore.mcandroidtester.R;

import com.elbit.mapcore.Structs.SMcVector3D;

/**
 * Created by tc99382 on 28/11/2016.
 */
public class ThreeDVector extends LinearLayout {

    private boolean mIsEnabled = false;
    private double mX=0.0;
    private double mY=0.0;
    private double mZ=0.0;
    private NumericEditTextLabel mZET;
    private NumericEditTextLabel mYET;
    private NumericEditTextLabel mXET;

    public SMcVector3D getVector3D() {
        return new SMcVector3D(getmX(),getmY(),getmZ());
    }

    public void setVector3D(SMcVector3D vector3D) {
        setmX(vector3D.x);
        setmY(vector3D.y);
        setmZ(vector3D.z);
    }


    public ThreeDVector(Context context, AttributeSet attrs) {
        super(context, attrs);
        LayoutInflater inflater = (LayoutInflater) context
                .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        inflater.inflate(R.layout.cv_three_d_vector, this);
        if (!isInEditMode()) {
            initViews();
            setLabel(context, attrs);
            setHints(context, attrs);
        }
    }

    private void initViews() {
        mXET = (NumericEditTextLabel) findViewById(R.id.three_d_vector_x);
        mYET = (NumericEditTextLabel) findViewById(R.id.three_d_vector_y);
        mZET = (NumericEditTextLabel) findViewById(R.id.three_d_vector_z);
    }

    private void setHints(Context context, AttributeSet attrs) {
        TypedArray typedArray = context.obtainStyledAttributes(attrs, R.styleable.ThreeDVector);

        mXET.getEditText().setHint(typedArray.getText(R.styleable.ThreeDVector_three_d_vector_x_hint));
        mYET.getEditText().setHint(typedArray.getText(R.styleable.ThreeDVector_three_d_vector_y_hint));
        mZET.getEditText().setHint(typedArray.getText(R.styleable.ThreeDVector_three_d_vector_z_hint));
    }

    private void setLabel(Context context, AttributeSet attrs) {
        TypedArray typedArray = context.obtainStyledAttributes(attrs, R.styleable.ThreeDVector);
        TextView textView = (TextView) findViewById(R.id.three_d_vector_label);
        textView.setText(typedArray.getText(R.styleable.ThreeDVector_three_d_vector_label));
    }


    public double getmX() {
        String x= mXET.getText();
        return x.isEmpty()?0:Double.valueOf(x);
    }

    public void setmX(double mX) {
        this.mX = mX;
        mXET.setText(String.valueOf(mX));
    }

    public double getmY() {
        String y= mYET.getText();
        return y.isEmpty()?0:Double.valueOf(y);
    }

    public void setmY(double mY) {
        this.mY = mY;
        mYET.setText(String.valueOf(mY));

    }

    public double getmZ() {
        String z= mZET.getText();
        return z.isEmpty() ? 0 : Double.valueOf(z);
    }

    public void setmZ(double mZ) {
        this.mZ = mZ;
        mZET.setText(String.valueOf(mZ));


    }
    public boolean isEnabled() {
        return mIsEnabled;
    }

    public void setIsEnabled(boolean isEnabled) {
        this.mIsEnabled = isEnabled;
        mXET.setEnabled(isEnabled);
        mYET.setEnabled(isEnabled);
        mZET.setEnabled(isEnabled);

    }



}
