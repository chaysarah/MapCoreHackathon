package com.elbit.mapcore.mcandroidtester.utils.customviews;

import android.content.Context;
import android.content.res.TypedArray;
import android.graphics.Color;
import com.google.android.material.textfield.TextInputLayout;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.utils.colorpicker.ColorPickerDialog;

import com.elbit.mapcore.Structs.SMcBColor;

/**
 * Created by tc99382 on 11/01/2017.
 */
public class SelectColor extends LinearLayout {
    View mRootView;
    Context mContext;
    int mSelectedColor = -1;
    SMcBColor mBColor;
    EditText mAlphaET;
    private Runnable mOnColorSelectedAction;

    public TextInputLayout getAlphaTIL() {
        return alphaTIL;
    }

    TextInputLayout alphaTIL;

    public SMcBColor getmBColor() {
        int alpha = 0;
        String strAlpha = String.valueOf(mAlphaET.getText());
        if(!strAlpha.isEmpty())
            alpha = Integer.valueOf(strAlpha);
        if(mSelectedColor == -1)
            return  null;
        return new SMcBColor(Color.red(mSelectedColor), Color.green(mSelectedColor), Color.blue(mSelectedColor), alpha);
    }

    public EditText getmAlphaET() {
        return mAlphaET;
    }

    public void setmBColor(SMcBColor color) {
        int alpha = color.a;
        if (color.a > 255)
            alpha = 255;
        else if (color.a < 0)
            alpha = 0;
        this.mSelectedColor = Color.argb(color.a, color.r, color.g, color.b);
        mAlphaET.setText(String.valueOf(alpha));
        mButton.setBackgroundColor(Color.argb(255, color.r, color.g, color.b));
    }

    public void setmBColor(int color) {
        int alpha = Color.alpha(color);
        if (Color.alpha(color) > 255)
            alpha = 255;
        else if (Color.alpha(color) < 0)
            alpha = 0;
        this.mSelectedColor = Color.argb(Color.alpha(color), Color.red(color), Color.green(color), Color.blue(color));
        mAlphaET.setText(String.valueOf(alpha));
        mButton.setBackgroundColor(Color.argb(255, Color.red(color), Color.green(color), Color.blue(color)));
    }


    public void setmSelectedColor(int mSelectedColor) {
        this.mSelectedColor = mSelectedColor;
        mButton.setTextColor(mSelectedColor);
    }

    public int getmSelectedColor() {
        return mSelectedColor;
    }

    private Button mButton;

    public SelectColor(Context context, AttributeSet attrs) {
        super(context, attrs);
        mContext = context;
        LayoutInflater inflater = (LayoutInflater) context
                .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        mRootView = inflater.inflate(R.layout.cv_color_picker, this);
        if (!isInEditMode()) {
            initViews();
            setBttnText(context, attrs);
            mButton.setBackgroundColor(mSelectedColor);
            mAlphaET.setText(String.valueOf(Color.alpha(mSelectedColor)));
            setAlphaVisibility(context, attrs);
            enableButtons(false);
        }
    }

    private void setAlphaVisibility(Context context, AttributeSet attrs) {
        TypedArray typedArray = context.obtainStyledAttributes(attrs, R.styleable.SelectColor);
        Boolean alphaVisibility = typedArray.getBoolean(R.styleable.SelectColor_color_picker_alpha_visibility, true);
        mRootView.findViewById(R.id.cv_color_picker_alpha_til).setVisibility(alphaVisibility == true ? View.VISIBLE : View.INVISIBLE);
    }

    public void enableButtons(boolean isEnabled) {
        mButton.setEnabled(isEnabled);
        mAlphaET.setEnabled(isEnabled);
    }

    private void setBttnText(Context context, AttributeSet attrs) {
        TypedArray typedArray = context.obtainStyledAttributes(attrs, R.styleable.SelectColor);
        //mButton.setText(typedArray.getText(R.styleable.ColorPickerBttn_color_picker_bttn_text));
        ((TextView) mRootView.findViewById(R.id.cv_color_picker_label_tv)).setText(typedArray.getText(R.styleable.SelectColor_color_picker_label_text));
    }

    public void setOnColorSelectedAction(Runnable action) {
        mOnColorSelectedAction = action;
    }

    private void initViews() {
        alphaTIL = (TextInputLayout) mRootView.findViewById(R.id.cv_color_picker_alpha_til);
        mAlphaET = ((EditText) mRootView.findViewById(R.id.cv_color_picker_alpha_et));
        mAlphaET.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence charSequence, int i, int i1, int i2) {

            }

            @Override
            public void onTextChanged(CharSequence charSequence, int i, int i1, int i2) {

            }

            @Override
            public void afterTextChanged(Editable editable) {
                if(mOnColorSelectedAction != null)
                    mOnColorSelectedAction.run();
            }
        });

        mButton = (Button) mRootView.findViewById(R.id.cv_color_picker_bttn);
        mButton.setOnClickListener(new OnClickListener() {
            @Override
            public void onClick(View v) {
                ColorPickerDialog colorPickerDialog = new ColorPickerDialog(mContext, 0, new ColorPickerDialog.OnColorSelectedListener() {
                    @Override
                    public void onColorSelected(int color) {
                        mButton.setBackgroundColor(color);
                        mSelectedColor = color;
                        //mAlphaET.setText(String.valueOf(Color.alpha(color)));
                        if (mOnColorSelectedAction != null)
                            mOnColorSelectedAction.run();
                    }
                });
                colorPickerDialog.show();
            }
        });
    }

    public void setAlpha(int alpha) {
        ((EditText) mRootView.findViewById(R.id.cv_color_picker_alpha_et)).setText(String.valueOf(alpha));
    }
}
