package com.elbit.mapcore.mcandroidtester.utils.customviews;

import android.content.Context;
import android.content.res.TypedArray;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.elbit.mapcore.mcandroidtester.R;

import com.elbit.mapcore.Structs.SMcVector2D;

/**
 * Created by TC99382 on 15/12/2016.
 */
public class TwoDVector extends LinearLayout {
    private double mX;
    private double mY;

    private NumericEditTextLabel mXET;
    private NumericEditTextLabel mYET;

    public TwoDVector(Context context, AttributeSet attrs) {
        super(context, attrs);
        LayoutInflater inflater = (LayoutInflater) context
                .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        inflater.inflate(R.layout.cv_two_d_vector, this);
        if (!isInEditMode()) {
            initViews();
            setLabel(context, attrs);
        }

    }
    public SMcVector2D getVector2D() {
        return new SMcVector2D(getmX(),getmY());
    }

    public void setVector2D(SMcVector2D vector2D) {
        setmX(vector2D.x);
        setmY(vector2D.y);
    }
    private void setLabel(Context context, AttributeSet attrs) {
        TypedArray typedArray = context.obtainStyledAttributes(attrs, R.styleable.TwoDVector);
        TextView textView = (TextView) findViewById(R.id.two_d_vector_tv);
        textView.setText(typedArray.getText(R.styleable.TwoDVector_two_d_vector_label));
    }

    private void initViews() {
        mXET = (NumericEditTextLabel) findViewById(R.id.two_d_vector_X);
        mYET = (NumericEditTextLabel) findViewById(R.id.two_d_vector_Y);
    }

    public void setmX(double mX) {
        this.mX = mX;
        mXET.setText(String.valueOf(mX));
    }

    public double getmX() {
        String z = mXET.getText();
        return z.isEmpty() ? 0 : Double.valueOf(z);
    }

    public void setmY(double mY) {
        this.mY = mY;
        mYET.setText(String.valueOf(mY));
    }

    public double getmY() {
        String z = mYET.getText();
        return z.isEmpty() ? 0 : Double.valueOf(z);
    }
}
