package com.elbit.mapcore.mcandroidtester.ui.adapters;

import android.app.Activity;
import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.CheckedTextView;
import android.widget.TextView;

import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;

import java.util.List;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcConditionalSelector;

/**
 * Created by tc99382 on 02/07/2017.
 */
public class RawLayerComponentAdapter extends ArrayAdapter<IMcMapLayer.SComponentParams> {
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
    public RawLayerComponentAdapter(Context context, int resource, List<IMcMapLayer.SComponentParams> objects) {
        super(context, resource, objects);
        mResourceId = resource;
        mContext = context;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        convertView = ((Activity) getContext()).getLayoutInflater().inflate(mResourceId, parent, false);
        if (getCount() > 0 && (getItem(position) != null)) {
            try {
                ((TextView) convertView.findViewById(R.id.list_view)).setText(getItem(position).eType.name() + "-" + getItem(position).strName);
            } catch (Exception e) {
                e.printStackTrace();
            }

        }
        ViewGroup.LayoutParams layoutParams = convertView.getLayoutParams();
        layoutParams.height = 150;
        convertView.setLayoutParams(layoutParams);

        return convertView;
    }
}