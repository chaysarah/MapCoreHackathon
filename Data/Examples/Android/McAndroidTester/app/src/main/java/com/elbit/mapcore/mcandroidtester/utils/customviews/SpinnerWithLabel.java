package com.elbit.mapcore.mcandroidtester.utils.customviews;
import android.content.Context;
import android.content.res.TypedArray;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.LinearLayout;
import android.widget.Spinner;
import android.widget.SpinnerAdapter;
import android.widget.TextView;

import com.elbit.mapcore.mcandroidtester.R;

import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;

/**
 * Created by chevishv on 21/06/2016.
 */
public class SpinnerWithLabel extends LinearLayout{
    Context mContext;
    Spinner mSpinner;


    public SpinnerWithLabel(Context context, AttributeSet attrs) {
        super(context, attrs);

        LayoutInflater inflater = (LayoutInflater) context
                .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        inflater.inflate(R.layout.cv_spinner_with_label, this);
        if(!isInEditMode())
        {
            mContext=context;
            mSpinner=(Spinner) findViewById(R.id.spinner_in_cv);
            setEnable(mContext,attrs);
            setLabel(context, attrs);


        }
    }

    private void setEnable(Context context, AttributeSet attrs) {
        TypedArray typedArray= context.obtainStyledAttributes(attrs, R.styleable.SpinnerWithLabel);
        mSpinner.setEnabled(typedArray.getBoolean(R.styleable.SpinnerWithLabel_swl_enable,true));
    }

    public SpinnerAdapter getAdapter()
    {
        return mSpinner.getAdapter();
    }
    public void setAdapter(SpinnerAdapter adapter)
    {
      mSpinner.setAdapter(adapter);
    }

    private void setLabel(Context context, AttributeSet attrs) {
        TypedArray typedArray= context.obtainStyledAttributes(attrs, R.styleable.SpinnerWithLabel);
        TextView textView=(TextView)findViewById(R.id.label_in_cv);
        textView.setText(typedArray.getText(R.styleable.SpinnerWithLabel_swl_labelText));
    }
    public Object getSelectedItem()
    {
        return  mSpinner.getSelectedItem();
    }

    public void setSelection(int position)
    {
        mSpinner.setSelection(position);
    }

    public int getSelectedItemPosition()
    {
        return mSpinner.getSelectedItemPosition();
    }
    /**
     * set spinner with values from corresponding enum
     */

    public void setOnItemSelectedListener(AdapterView.OnItemSelectedListener onItemSelectedListener)
    {
        mSpinner.setOnItemSelectedListener(onItemSelectedListener);
    }
    public void setSpinner(final Object curObject, final Object[] values, final Method method, int defVal) {
        Spinner spinner = (Spinner) findViewById(R.id.spinner_in_cv);
        spinner.setAdapter(new ArrayAdapter<>(mContext, android.R.layout.simple_spinner_item, values));
        spinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                try {
                    method.invoke(curObject,values[position]);
                } catch (IllegalAccessException e) {
                    e.printStackTrace();
                } catch (InvocationTargetException e) {
                    e.printStackTrace();
                }
            }
            @Override
            public void onNothingSelected(AdapterView<?> parent) {
            }
            });
        spinner.setSelection(defVal);
    }

    public void setSpinner(final Object[] values, int defVal) {
        Spinner spinner = (Spinner) findViewById(R.id.spinner_in_cv);
        spinner.setAdapter(new ArrayAdapter<>(mContext, android.R.layout.simple_spinner_item, values));
        spinner.setSelection(defVal);
    }





}
