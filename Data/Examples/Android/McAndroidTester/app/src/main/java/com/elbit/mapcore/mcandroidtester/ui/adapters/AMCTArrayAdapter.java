package com.elbit.mapcore.mcandroidtester.ui.adapters;

/**
 * Created by tc99382 on 21/07/2016.
 */


import android.app.Activity;
import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.CheckedTextView;

import com.elbit.mapcore.mcandroidtester.R;
import java.util.List;

public class AMCTArrayAdapter extends ArrayAdapter{

    private final Context mContext;
    private int mResourceId;

    /**
     * Constructor
     *
     * @param context  The current context.
     * @param resource The resource ID for a layout file containing a TextView to use when
     *                 instantiating views.
     * @param objects  The objects to represent in the ListView.
     */
    public AMCTArrayAdapter(Context context, int resource, List objects) {
        super(context, resource, objects);
        mResourceId = resource;
        mContext = context;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        convertView = ((Activity) getContext()).getLayoutInflater().inflate(mResourceId, parent, false);
        if (getCount() > 0 && (getItem(position) != null)) {
                ((CheckedTextView) convertView.findViewById(R.id.list_view)).setText(
                        getItem(position).hashCode() + "  " + getItem(position).getClass().getSimpleName());
        }
        return convertView;
    }
}