package com.elbit.mapcore.mcandroidtester.ui.adapters;

import android.app.Activity;
import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.CheckedTextView;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectScheme;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcViewportConditionalSelector;
import com.elbit.mapcore.mcandroidtester.R;

import java.util.List;

/**
 * Created by tc99382 on 02/07/2017.
 */
public class TerrainObjectsConsiderationFlagsAdapter extends ArrayAdapter<IMcObjectScheme.ETerrainObjectsConsiderationFlags> {
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
    public TerrainObjectsConsiderationFlagsAdapter(Context context, int resource, List<IMcObjectScheme.ETerrainObjectsConsiderationFlags> objects) {
        super(context, resource, objects);
        mResourceId = resource;
        mContext = context;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        convertView = ((Activity) getContext()).getLayoutInflater().inflate(mResourceId, parent, false);
        if (getCount() > 0 && (getItem(position) != null)) {
                ((CheckedTextView) convertView.findViewById(R.id.list_view)).setText(getItem(position).name());
        }
        return convertView;
    }
}