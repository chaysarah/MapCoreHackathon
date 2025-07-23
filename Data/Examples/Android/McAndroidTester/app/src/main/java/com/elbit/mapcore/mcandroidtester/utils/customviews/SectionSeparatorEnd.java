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
public class SectionSeparatorEnd extends LinearLayout{
    public SectionSeparatorEnd(Context context, AttributeSet attrs) {
        super(context, attrs);
        inflateLayout(context);
    }

    private void inflateLayout(Context context)
    {
        LayoutInflater inflater = (LayoutInflater) context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        inflater.inflate(R.layout.cv_section_separator_end, this);
    }

}



