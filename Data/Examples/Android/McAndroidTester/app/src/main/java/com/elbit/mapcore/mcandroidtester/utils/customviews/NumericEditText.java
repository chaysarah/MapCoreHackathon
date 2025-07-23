package com.elbit.mapcore.mcandroidtester.utils.customviews;

import android.content.Context;
import android.content.res.TypedArray;
import com.google.android.material.textfield.TextInputEditText;
import android.text.InputType;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.LinearLayout;

import com.elbit.mapcore.mcandroidtester.R;

/**
 * Created by tc97803 on 29/08/2016.
 */
public class NumericEditText extends LinearLayout {

    private Context m_Context;
    public NumericEditText(Context context)
    {
        super(context);
        InflateLayout(context);
    }

    public NumericEditText(Context context, AttributeSet attrs, int defStyleAttr, int defStyleRes) {
        super(context, attrs, defStyleAttr, defStyleRes);

        InflateLayout(context);
    }

    public NumericEditText(Context context, AttributeSet attrs) {
        super(context, attrs);
        InflateLayout(context);

        if(!isInEditMode())
        {
            setLabel(context, attrs);
            setHint(context,attrs);
            setKeyboard(context,attrs);
            setEnableMode(context,attrs);
        }
    }

    private void setEnableMode(Context context, AttributeSet attrs) {
        TypedArray typedArray = context.obtainStyledAttributes(attrs, R.styleable.NumericEditText);
        boolean isEnabled=typedArray.getBoolean(R.styleable.NumericEditText_net_enable_mode,true);
        {
            getEditText().setEnabled(isEnabled);
        }
    }

    private void setKeyboard(Context context, AttributeSet attrs) {
        TypedArray typedArray = context.obtainStyledAttributes(attrs, R.styleable.NumericEditText);
        if(typedArray.getBoolean(R.styleable.NumericEditText_net_numeric_keyboard,false))
        {
            getEditText().setRawInputType(InputType.TYPE_CLASS_NUMBER|InputType.TYPE_NUMBER_FLAG_SIGNED|InputType.TYPE_NUMBER_FLAG_DECIMAL);
            //getEditText().setShowSoftInputOnFocus(true);
        }
    }

    private void setHint(Context context, AttributeSet attrs) {
        TypedArray typedArray = context.obtainStyledAttributes(attrs, R.styleable.NumericEditText);
        getEditText().setHint(typedArray.getText(R.styleable.NumericEditText_net_hint));


    }
    public void setText(String text) {
        getEditText().setText(text);
    }
    private void InflateLayout(Context context)
    {
        LayoutInflater inflater = (LayoutInflater) context
                .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        inflater.inflate(R.layout.cv_numeric_edittext, this);
        m_Context = context;
        getEditText().setOnFocusChangeListener(editTextFocusChange);
    }

    public void setLabel(Context context, AttributeSet attrs) {
        TypedArray typedArray = context.obtainStyledAttributes(attrs, R.styleable.NumericEditText);
        //getEditText().setHint(typedArray.getText(R.styleable.NumericEditText_net_labelText));
    }

    public void setLabel(CharSequence text) {
       // getEditText().setHint(text);
    }

    public TextInputEditText getEditText()
    {
        return (TextInputEditText)findViewById(R.id.cv_numeric_edittext_edittext);
    }

    private OnFocusChangeListener editTextFocusChange = new OnFocusChangeListener() {
        @Override
        public void onFocusChange(View v, boolean hasFocus) {
            TextInputEditText editText = getEditText();
            if (hasFocus == false && editText.getText().toString().isEmpty())
                editText.setText("");
        }
    };

    public void setFloat(float value)
    {
        TextInputEditText editText = getEditText();

        if(value == Float.MAX_VALUE)
            editText.setText("MAX");
        else
            editText.setText(Float.toString(value));
    }
    public String getText() {
        return getEditText().getText().toString();
    }

    public void setInt(int value)
    {
        TextInputEditText editText = getEditText();

        if(value == Integer.MAX_VALUE)
            editText.setText("MAX");
        else
            editText.setText(Integer.toString(value));
    }

    public float getFloat()
    {
        try {
            String strValue = getEditText().getText().toString();
            if(strValue == "MAX")
                return Float.MAX_VALUE;
            else if(strValue.isEmpty())
                return 0f;
            else
                return Float.parseFloat(strValue);
        }
        catch (Exception e)
        {
            return 0f;
        }
    }


}
