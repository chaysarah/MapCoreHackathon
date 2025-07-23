package com.elbit.mapcore.mcandroidtester.ui.adapters;

import android.app.Activity;
import android.content.Context;
//import android.support.annotation.NonNull;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.CheckedTextView;


import androidx.annotation.NonNull;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcFont;
import com.elbit.mapcore.mcandroidtester.R;
import java.util.List;

public class CharRangeAdapter extends ArrayAdapter<IMcFont.SCharactersRange> {
    private int mResourceId;

    public CharRangeAdapter(@NonNull Context context, int resource) {
        super(context, resource);
        mResourceId = resource;
    }

    public CharRangeAdapter(@NonNull Context context, int resource, @NonNull List<IMcFont.SCharactersRange> objects) {
        super(context, resource, objects);
        mResourceId = resource;
    }
    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        convertView = ((Activity) getContext()).getLayoutInflater().inflate(mResourceId, parent, false);
        if (getCount() > 0 && (getItem(position) != null)) {
            try {
                IMcFont.SCharactersRange font = getItem(position);
                String text = "'" + font.nFrom + "'" + " - " + "'" + font.nTo  + "'"   + " (" + (int)font.nFrom + " - " + (int)font.nTo + ")" ;

                CheckedTextView checkedTextView = ((CheckedTextView) convertView.findViewById(R.id.list_view));
                checkedTextView.setText(text);

            } catch (Exception e) {
                e.printStackTrace();
            }
        }
        return convertView;
    }

}
