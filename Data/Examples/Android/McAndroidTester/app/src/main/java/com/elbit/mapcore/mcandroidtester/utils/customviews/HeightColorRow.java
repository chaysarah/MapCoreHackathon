package com.elbit.mapcore.mcandroidtester.utils.customviews;

import android.content.Context;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.EditText;
import android.widget.LinearLayout;

import com.elbit.mapcore.mcandroidtester.R;

/**
 * Created by TC99382 on 15/12/2016.
 */
public class HeightColorRow extends LinearLayout{

    private View mColor;
    private EditText mAlpha;
    private EditText mHeight;

    public HeightColorRow(Context context, AttributeSet attrs) {
        super(context, attrs);
        LayoutInflater inflater = (LayoutInflater) context
                .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        inflater.inflate(R.layout.cv_height_colors_row, this);
        if (!isInEditMode()) {
            initViews();
        }
    }

    private void initViews() {
        mColor=findViewById(R.id.cv_height_colors_row_color_iv);
        mAlpha=(EditText)findViewById(R.id.cv_height_colors_row_alpha_et);
        mHeight=(EditText)findViewById(R.id.cv_height_colors_row_height_et);
    }

    public EditText getmHeight() {
        return mHeight;
    }

    public void setmHeight(EditText mHeight) {
        this.mHeight = mHeight;
    }

    public View getmColor() {
        return mColor;
    }

    public void setmColor(View mColor) {
        this.mColor = mColor;
    }

    public EditText getmAlpha() {
        return mAlpha;
    }

    public void setmAlpha(EditText mAlpha) {
        this.mAlpha = mAlpha;
    }
}
