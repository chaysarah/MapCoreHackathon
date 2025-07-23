package com.elbit.mapcore.mcandroidtester.utils.customviews;

import android.content.Context;
import android.content.res.TypedArray;

import com.google.android.material.textfield.TextInputEditText;
import com.google.android.material.textfield.TextInputLayout;
import android.text.InputType;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.widget.LinearLayout;

import com.elbit.mapcore.General.Constants;
import com.elbit.mapcore.mcandroidtester.R;

/**
 * Created by tc97803 on 04/09/2016.
 */
public class NumericEditTextLabel extends LinearLayout {

    public NumericEditTextLabel(Context context, AttributeSet attrs) {
        super(context, attrs);
        InflateLayout(context);
        getEditText().setSingleLine();

        if (!isInEditMode()) {
            setLabel(context, attrs);
            setHint(context,attrs);
            setKeyboard(context,attrs);
            setEnableMode(context,attrs);
        }
    }

    private void setEnableMode(Context context, AttributeSet attrs) {
        TypedArray typedArray = context.obtainStyledAttributes(attrs, R.styleable.NumericEditTextLabel);
        boolean isEnabled=typedArray.getBoolean(R.styleable.NumericEditTextLabel_netl_enable_mode,true);
        {
           getEditText().setEnabled(isEnabled);
        }
    }

    private void setKeyboard(Context context, AttributeSet attrs) {
        TypedArray typedArray = context.obtainStyledAttributes(attrs, R.styleable.NumericEditTextLabel);
        if(typedArray.getBoolean(R.styleable.NumericEditTextLabel_netl_numeric_keyboard,false))
        {
            getEditText().setRawInputType(InputType.TYPE_CLASS_NUMBER|InputType.TYPE_NUMBER_FLAG_SIGNED|InputType.TYPE_NUMBER_FLAG_DECIMAL);
            //getEditText().setShowSoftInputOnFocus(true);
        }
    }

    private void setHint(Context context, AttributeSet attrs) {
            TypedArray typedArray = context.obtainStyledAttributes(attrs, R.styleable.NumericEditTextLabel);
            getEditText().setHint(typedArray.getText(R.styleable.NumericEditTextLabel_netl_hint));


    }

    private void InflateLayout(Context context) {
        LayoutInflater inflater = (LayoutInflater) context
                .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        inflater.inflate(R.layout.cv_numeric_edittext_label, this);
    }

    private void setLabel(Context context, AttributeSet attrs) {
        TypedArray typedArray = context.obtainStyledAttributes(attrs, R.styleable.NumericEditTextLabel);
        ((TextInputLayout) findViewById(R.id.cv_netl_label)).setHint(typedArray.getText(R.styleable.NumericEditTextLabel_netl_labelText));
    }

    public String getText() {
        return getEditText().getText().toString();
    }

    public void setText(String text) {
        getEditText().setText(text);
    }

    public TextInputEditText getEditText() {
        return (TextInputEditText) findViewById(R.id.cv_netl_edittext);
    }

    public void setFloat(float value) {
        TextInputEditText editText = getEditText();

        if (value == Float.MAX_VALUE)
            editText.setText("MAX");
        else
            editText.setText(Float.toString(value));
    }

    public void setInt(int value) {
        TextInputEditText editText = getEditText();

        if (value == Integer.MAX_VALUE)
            editText.setText("MAX");
        else
            editText.setText(Integer.toString(value));
    }

    public void setUInt(int value) {
        TextInputEditText editText = getEditText();

        if (value == Constants.UINT_MAX)
            editText.setText("MAX");
        else
            editText.setText(Integer.toString(value));
    }

    public void setShort(short value) {
        TextInputEditText editText = getEditText();

        if (value == Short.MAX_VALUE)
            editText.setText("MAX");
        else
            editText.setText(Short.toString(value));
    }

    public float getFloat() {
        try {
            String strValue = getEditText().getText().toString();
            if (strValue.equals("MAX"))
                return Float.MAX_VALUE;
            else if (strValue.isEmpty())
                return 0f;
            else
                return Float.parseFloat(strValue);
        } catch (Exception e) {
            e.printStackTrace();
            return 0f;
        }
    }

    public void setDouble(double val)
    {
        setText(String.valueOf(val));
    }

    public double getDouble() {
        double dParam;
        String strValue = getEditText().getText().toString();
        if (strValue.equals("MAX"))
            dParam = Double.MAX_VALUE;
        else if (strValue.isEmpty())
            dParam = 0.0D;
        else
            dParam = Double.valueOf(strValue);
        return dParam;
    }

    public void setLong(long val)
    {
        setText(String.valueOf(val));
    }

    public long getLong() {
        long lParam;
        String strValue = getEditText().getText().toString();
        if (strValue.equals("MAX"))
            lParam = Long.MAX_VALUE;
        else if (strValue.isEmpty())
            lParam = 0;
        else
            lParam = Long.valueOf(strValue);
        return lParam;
    }

    public short getShort() {
        try {
            String strValue = getEditText().getText().toString();
            if (strValue.equals("MAX"))
                return Short.MAX_VALUE;
            else if (strValue.isEmpty())
                return 0;
            else
                return Short.parseShort(strValue);
        } catch (Exception e) {
            e.printStackTrace();
            return 0;
        }
    }

    public int getInt() {
        try {
            String strValue = getEditText().getText().toString();
            if (strValue.equals("MAX"))
                return Integer.MAX_VALUE;
            else if (strValue.isEmpty())
                return 0;
            else
                return Integer.parseInt(strValue);
        } catch (Exception e) {
            e.printStackTrace();
            return 0;
        }
    }

    public int getUInt() {
        try {
            String strValue = getEditText().getText().toString();
            if (strValue.equals("MAX"))
                return Constants.UINT_MAX;
            else if (strValue.isEmpty())
                return 0;

            int value = Integer.parseInt(strValue);
            if (value < 0)
                return 0;
            else
                return value;
        } catch (Exception e) {
            e.printStackTrace();
            return 0;
        }
    }

    public void setByte(byte val)
    {
        int val2 = val & 0xFF;
        setText(String.valueOf(val2));
    }

    public byte getByte() {
        try {
            String strValue = getEditText().getText().toString();
            if (strValue.equals("MAX"))
                return Byte.MAX_VALUE;
            else if (strValue.isEmpty())
                return 0;
            else {
                int val = Integer.parseInt(strValue);
                byte byteVal = (byte)val;
                return byteVal;
            }
        } catch (Exception e) {
            e.printStackTrace();
            return 0;
        }
    }


}
