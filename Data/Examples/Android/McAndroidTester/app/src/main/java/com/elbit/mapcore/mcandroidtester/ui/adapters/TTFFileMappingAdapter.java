package com.elbit.mapcore.mcandroidtester.ui.adapters;

import android.app.Activity;
import android.content.Context;
//import android.support.annotation.NonNull;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.CheckedTextView;
import android.widget.TextView;

import androidx.annotation.NonNull;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcLogFont;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;

import java.util.List;

public class TTFFileMappingAdapter extends ArrayAdapter<IMcLogFont.SLogFontToTtfFile> {
    private int mResourceId;
    public TTFFileMappingAdapter(@NonNull Context context, int resource) {
        super(context, resource);
        mResourceId = resource;
    }

    public TTFFileMappingAdapter(@NonNull Context context, int resource, @NonNull List<IMcLogFont.SLogFontToTtfFile> objects) {
        super(context, resource, objects);
        mResourceId = resource;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        convertView = ((Activity) getContext()).getLayoutInflater().inflate(mResourceId, parent, false);
        if (getCount() > 0 ) {
            IMcLogFont.SLogFontToTtfFile item = getItem(position);
            if (item != null) {
                try {
                    ((TextView) convertView.findViewById(android.R.id.text1)).setText(item.strTtfFileFullPathName + " " + item.LogFont.LogFont.lfFaceName);
                }  catch (Exception e) {
                    e.printStackTrace();
                }
            }
        }
        return convertView;
    }
}
