package com.elbit.mapcore.mcandroidtester.utils.customviews;

import android.content.Context;
import android.content.res.TypedArray;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.elbit.mapcore.mcandroidtester.R;

import com.elbit.mapcore.Structs.SMcFVector2D;

/**
 * Created by tc99382 on 30/11/2016.
 */
public class TwoDFVector extends LinearLayout {
    private float mX;
    private float mY;

    private NumericEditTextLabel mXET;
    private NumericEditTextLabel mYET;

    public TwoDFVector(Context context, AttributeSet attrs) {
        super(context, attrs);
        LayoutInflater inflater = (LayoutInflater) context
                .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        inflater.inflate(R.layout.cv_two_d_f_vector, this);
        if (!isInEditMode()) {
            initViews();
            setLabel(context, attrs);
        }

    }

    @Override
    public String toString() {
        return "X = " + this.mX + "; Y = " + this.mY;

    }

    public SMcFVector2D getVector2D() {
        return new SMcFVector2D(getmX(),getmY());
    }

    public void setVector2D(SMcFVector2D vector2D) {
        setmX(vector2D.x);
        setmY(vector2D.y);
    }
    private void setLabel(Context context, AttributeSet attrs) {
        TypedArray typedArray = context.obtainStyledAttributes(attrs, R.styleable.TwoDFVector);
        TextView textView = (TextView) findViewById(R.id.two_d_f_vector_tv);
        textView.setText(typedArray.getText(R.styleable.TwoDFVector_two_d_f_vector_label));
    }

    private void initViews() {
        mXET = (NumericEditTextLabel) findViewById(R.id.two_d_f_vector_X);
        mYET = (NumericEditTextLabel) findViewById(R.id.two_d_f_vector_Y);
    }

    public void setmX(float mX) {
        this.mX = mX;
        mXET.setText(String.valueOf(mX));
    }

    public float getmX() {
        if(mXET.getText().isEmpty())
            return 0;
        return Float.valueOf(mXET.getText());
    }

    public void setmY(float mY) {
        this.mY = mY;
        mYET.setText(String.valueOf(mY));
    }

    public float getmY() {
        if(mYET.getText().isEmpty())
            return 0;
        return Float.valueOf(mYET.getText());
    }

}
