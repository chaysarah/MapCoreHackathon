package com.elbit.mapcore.mcandroidtester.ui.adapters;

import android.app.Activity;
import android.content.Context;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AbsListView;
import android.widget.BaseAdapter;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.TextView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.model.AMCTColorOverriding;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.objectscheme_tabs.SchemeGeneralTabFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.objectscheme_tabs.StateNameRow;

import java.util.ArrayList;

public class StateNameAdapter extends BaseAdapter {

    private final Context mContext;

    public ArrayList<StateNameRow> getStateNameRows() {
        return mStateNameRows;
    }

    private final ArrayList<StateNameRow> mStateNameRows;
    private SchemeGeneralTabFragment mFragment;

    public StateNameAdapter(Context mContext, ArrayList<StateNameRow> stateNameRows, SchemeGeneralTabFragment fragment) {
        this.mContext = mContext;
        this.mStateNameRows = stateNameRows;
        mFragment = fragment;
    }

    /**
     * How many items are in the data set represented by this Adapter.
     *
     * @return Count of items.
     */
    @Override
    public int getCount() {
        return mStateNameRows.size();
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
        return mStateNameRows.get(position);
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
        StateNameAdapter.RowViewHolder viewHolder;
        if (rowView == null) {
            LayoutInflater inflater = ((Activity) mContext).getLayoutInflater();
            rowView = inflater.inflate(R.layout.state_name_row, null);
            viewHolder = new StateNameAdapter.RowViewHolder();
            viewHolder.state = (EditText) rowView.findViewById(R.id.state_row_et);
            viewHolder.state.addTextChangedListener(new TextWatcher() {
                @Override
                public void beforeTextChanged(CharSequence s, int start, int count, int after) {

                }

                @Override
                public void onTextChanged(CharSequence s, int start, int before, int count) {

                }

                @Override
                public void afterTextChanged(Editable s) {
                    mFragment.AddEmptyRow();
                }
            });
            viewHolder.stateName = (EditText) rowView.findViewById(R.id.state_name_row_et);
            viewHolder = setCurrRowValues(viewHolder, position);
        } else {
            viewHolder = (StateNameAdapter.RowViewHolder) rowView.getTag();
        }
        rowView.setTag(viewHolder);
        return rowView;
    }

    private StateNameAdapter.RowViewHolder setCurrRowValues(final StateNameAdapter.RowViewHolder holder, final int position) {
        StateNameRow row = mStateNameRows.get(position);
        if(row.State > 0 && row.StateName != null) {
            holder.state.setText(Byte.toString(row.State));
            holder.stateName.setText(row.StateName);
        }
        return holder;
    }

    private class RowViewHolder {
        EditText state;
        EditText stateName;
    }

}
