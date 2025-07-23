package com.elbit.mapcore.mcandroidtester.ui.adapters;

import android.app.Activity;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.BaseAdapter;
import android.widget.Spinner;
import android.widget.TextView;

import com.elbit.mapcore.mcandroidtester.R;

import com.elbit.mapcore.Enums.EMcPointCoordSystem;
import com.elbit.mapcore.Interfaces.Calculations.IMcSpatialQueries;

/**
 * Created by tc99382 on 03/09/2017.
 */
public class UtilityItemsAdapter extends BaseAdapter {
    EMcPointCoordSystem[] mTypeValues;
    IMcSpatialQueries.EQueryPrecision[] mQueryPrecisions;
    Context mContext;

    public UtilityItemsAdapter(Context context, EMcPointCoordSystem[] values, IMcSpatialQueries.EQueryPrecision[] eQueryPrecisions) {
        mTypeValues=values;
        mQueryPrecisions=eQueryPrecisions;
        mContext=context;
    }

    /**
     * How many items are in the data set represented by this Adapter.
     *
     * @return Count of items.
     */
    @Override
    public int getCount() {
        return mTypeValues.length;
    }

    /**
     * Get the data item associated with the specified position in the data set.
     *
     * @param position Position of the item whose data we want within the adapter's
     *                 data set.
     * @return The data at the specified position.
     */
    @Override
    public Object getItem(int position) {
        return mTypeValues[position];
    }

    /**
     * Get the row id associated with the specified position in the list.
     *
     * @param position The position of the item within the adapter's data set whose row id we want.
     * @return The id of the item at the specified position.
     */
    @Override
    public long getItemId(int position) {
        return 0;
    }

    /**
     * Get a View that displays the data at the specified position in the data set. You can either
     * create a View manually or inflate it from an XML layout file. When the View is inflated, the
     * parent View (GridView, ListView...) will apply default layout parameters unless you use
     * {@link LayoutInflater#inflate(int, ViewGroup, boolean)}
     * to specify a root view and to prevent attachment to the root.
     *
     * @param position    The position of the item within the adapter's data set of the item whose view
     *                    we want.
     * @param convertView The old view to reuse, if possible. Note: You should check that this view
     *                    is non-null and of an appropriate type before using. If it is not possible to convert
     *                    this view to display the correct data, this method can create a new view.
     *                    Heterogeneous lists can specify their number of view types, so that this View is
     *                    always of the right type (see {@link #getViewTypeCount()} and
     *                    {@link #getItemViewType(int)}).
     * @param parent      The parent that this view will eventually be attached to
     * @return A View corresponding to the data at the specified position.
     */
    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        RowHolder rowHolder;
        if (convertView == null) {
            convertView = ((Activity) mContext).getLayoutInflater().inflate(R.layout.utility_row, parent, false);
            rowHolder = new RowHolder();
            rowHolder.mTypeTV = (TextView) convertView.findViewById(R.id.item_type_tv);
            rowHolder.mItemSpinner = (Spinner) convertView.findViewById(R.id.item_spinner);
        } else {
            rowHolder = (RowHolder) convertView.getTag();
        }
        rowHolder.mTypeTV.setText(mTypeValues[position].name());
        rowHolder.mItemSpinner.setAdapter(new ArrayAdapter<>(mContext, android.R.layout.simple_spinner_item,mQueryPrecisions));
        convertView.setTag(rowHolder);
        return convertView;
    }
    public class RowHolder
    {
        TextView mTypeTV;
        Spinner mItemSpinner;

    }
}
