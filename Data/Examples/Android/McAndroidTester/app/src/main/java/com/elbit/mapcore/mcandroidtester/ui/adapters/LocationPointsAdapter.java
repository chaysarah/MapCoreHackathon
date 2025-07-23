package com.elbit.mapcore.mcandroidtester.ui.adapters;

import android.app.Activity;
import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import com.elbit.mapcore.mcandroidtester.R;

import java.util.List;

import com.elbit.mapcore.Structs.SMcVector3D;

/**
 * Created by TC99382 on 14/06/2017.
 */
public class LocationPointsAdapter extends ArrayAdapter<SMcVector3D> {
    private final SMcVector3D[] mLocationPoints;
    private final int mResourceId;
    private final Context mContext;

    /**
     * Constructor
     *
     * @param context  The current context.
     * @param resource The resource ID for a layout file containing a TextView to use when
     *                 instantiating views.
     * @param objects  The objects to represent in the ListView.
     */
    public LocationPointsAdapter(Context context, int resource, List<SMcVector3D> objects) {
        super(context, resource, objects);
        SMcVector3D[] locationPoints = new SMcVector3D[objects.size()];
        mLocationPoints = objects.toArray(locationPoints);
        mContext = context;
        mResourceId = resource;
    }


    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        RowHolder rowHolder;
        if (convertView == null) {
            convertView = ((Activity) mContext).getLayoutInflater().inflate(mResourceId, parent, false);
            rowHolder = new RowHolder();
            rowHolder.xET = (TextView) convertView.findViewById(R.id.location_point_row_x);
            rowHolder.yET = (TextView) convertView.findViewById(R.id.location_point_row_y);
            rowHolder.zET = (TextView) convertView.findViewById(R.id.location_point_row_z);
        } else {
            rowHolder = (RowHolder) convertView.getTag();
        }
        rowHolder.xET.setText(String.valueOf(mLocationPoints[position].x));
        rowHolder.yET.setText(String.valueOf(mLocationPoints[position].y));
        rowHolder.zET.setText(String.valueOf(mLocationPoints[position].z));
        convertView.setTag(rowHolder);
        return convertView;
    }

    private class RowHolder {
        TextView xET;
        TextView yET;
        TextView zET;
    }
}
