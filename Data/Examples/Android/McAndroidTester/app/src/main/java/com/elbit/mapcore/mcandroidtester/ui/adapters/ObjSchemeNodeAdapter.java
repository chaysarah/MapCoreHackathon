package com.elbit.mapcore.mcandroidtester.ui.adapters;

import android.app.Activity;
import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.CheckedTextView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.managers.Manager_MCNames;

import java.util.List;

/**
 * Created by TC99382 on 19/06/2017.
 */
public class ObjSchemeNodeAdapter extends ArrayAdapter {
    private final Context mContext;
    private final int mResourceId;

    /**
     * Constructor
     *
     * @param context  The current context.
     * @param resource The resource ID for a layout file containing a TextView to use when
     *                 instantiating views.
     * @param objects  The objects to represent in the ListView.
     */
    public ObjSchemeNodeAdapter(Context context, int resource, List objects) {
        super(context, resource, objects);
        mContext=context;
        mResourceId=resource;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        convertView = ((Activity)getContext()).getLayoutInflater().inflate(mResourceId, parent, false);
        if (getCount() > 0&&(getItem(position)!=null)) {
            ((CheckedTextView) convertView.findViewById(R.id.list_view)).setText(Manager_MCNames.getInstance().getNameByObject(getItem(position)));
        }
        return convertView;
    }

}
