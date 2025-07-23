package com.elbit.mapcore.mcandroidtester.ui.adapters;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ListView;
import android.widget.TextView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.utils.Consts;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;

/**
 * Created by tc97803 on 01/05/2017.
 */

public class ConditionalSelectorHashMapAdapter extends BaseAdapter {

    private ArrayList mData;
    Context mContext;
    LayoutInflater mInflater;
    int mListType;

    public ConditionalSelectorHashMapAdapter(Context context, HashMap<Integer, Object> data, int listType) {
        mData = new ArrayList();
        mData.addAll(data.entrySet());
        mContext = context;
        mInflater = (LayoutInflater) mContext.getSystemService(mContext.LAYOUT_INFLATER_SERVICE);
        mListType = listType;
    }

    @Override
    public int getCount() {
        return mData.size();
    }

    @Override
    public Map.Entry<Object, Object> getItem(int position) {
        return (Map.Entry<Object, Object>) mData.get(position);
    }

    @Override
    public long getItemId(int i) {
        return 0;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        View result = null;

        if (convertView == null) {
            switch (mListType) {
                case Consts.ListType.NON_CHECK:
                    result = LayoutInflater.from(parent.getContext()).inflate(R.layout.list_item, parent, false);
                    break;
                case Consts.ListType.MULTIPLE_CHECK:
                    result = LayoutInflater.from(parent.getContext()).inflate(R.layout.checkable_list_item, parent, false);
                    break;
                case Consts.ListType.SINGLE_CHECK:
                    result = LayoutInflater.from(parent.getContext()).inflate(R.layout.radio_bttn_list_item, parent, false);
                    break;
            }

        } else {
            result = convertView;
        }

        Map.Entry<Object, Object> item = getItem(position);
        ((TextView) result.findViewById(R.id.list_view)).setText(item.getKey() + "  " + item.getValue().getClass().getSimpleName());

        return result;
    }
}
