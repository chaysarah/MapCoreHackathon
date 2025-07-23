package com.elbit.mapcore.mcandroidtester.utils.customviews;

import android.content.Context;
import android.content.res.TypedArray;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.elbit.mapcore.mcandroidtester.R;

/**
 * Created by tc99382 on 17/11/2016.
 */
public class SectionSeparator extends LinearLayout{
    public SectionSeparator(Context context, AttributeSet attrs) {
        super(context, attrs);
        inflateLayout(context);
        if(!isInEditMode())
        {
            setLabel(context, attrs);
        }
    }
    private void inflateLayout(Context context)
    {
        LayoutInflater inflater = (LayoutInflater) context
                .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        inflater.inflate(R.layout.cv_section_separator, this);
    }
    private void setLabel(Context context, AttributeSet attrs) {
        TypedArray typedArray= context.obtainStyledAttributes(attrs, R.styleable.SectionSeparator);
        ((TextView)findViewById(R.id.cv_section_separator_text)).setText(typedArray.getText(R.styleable.SectionSeparator_section_separator_text));
    }
}



