package com.elbit.mapcore.mcandroidtester.ui.adapters;

import android.app.Activity;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.TextView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlaymanager_tabs.SizeFactorRow;

import java.util.ArrayList;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;

/**
 * Created by tc99382 on 06/06/2017.
 */
public class SizeFactorAdapter extends BaseAdapter {
    private final Context mContext;

    public ArrayList<SizeFactorRow> getSizeFactorRows() {
        return mSizeFactorRows;
    }

    private final ArrayList<SizeFactorRow> mSizeFactorRows;

    public SizeFactorAdapter(Context mContext, ArrayList<SizeFactorRow> sizeFactorRows) {
        this.mContext = mContext;
        this.mSizeFactorRows = sizeFactorRows;
    }

     /**
     * How many items are in the data set represented by this Adapter.
     *
     * @return Count of items.
     */
    @Override
    public int getCount() {
        return mSizeFactorRows.size();
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
        return mSizeFactorRows.get(position);
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
    public View getView(final int position, View convertView, ViewGroup parent) {
        View rowView = convertView;
        final RowViewHolder viewHolder;
        if (rowView == null) {
            LayoutInflater inflater = ((Activity) mContext).getLayoutInflater();
            rowView = inflater.inflate(R.layout.size_factor_row, null);
            viewHolder = new RowViewHolder();
            viewHolder.cb = (CheckBox) rowView.findViewById(R.id.size_factor_row_cb);
            viewHolder.eptTypeTV = (TextView) rowView.findViewById(R.id.size_factor_row_espt_type_tv);
            viewHolder.factorET = (EditText) rowView.findViewById(R.id.size_factor_row_factor_et);

        } else {
            viewHolder = (RowViewHolder) rowView.getTag();
        }
        viewHolder.eptTypeTV.setText(String.valueOf(mSizeFactorRows.get(position).eSizePropertyType.name()));
        float sizeFactor=mSizeFactorRows.get(position).sizeFactor;
        if(sizeFactor>0)
        viewHolder.factorET.setText(String.valueOf( mSizeFactorRows.get(position).sizeFactor));
        rowView.setTag(viewHolder);
        return rowView;

    }

    private class RowViewHolder {
        CheckBox cb;
        TextView eptTypeTV;
        EditText factorET;
    }

}
