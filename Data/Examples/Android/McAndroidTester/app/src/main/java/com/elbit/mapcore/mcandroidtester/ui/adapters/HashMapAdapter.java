package com.elbit.mapcore.mcandroidtester.ui.adapters;

/**
 * Created by tc99382 on 21/07/2016.
 */

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.utils.Consts;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;


public class HashMapAdapter extends BaseAdapter {

    private ArrayList mData;
    Context mContext;
    LayoutInflater mInflater;
    int mListType;
    boolean mIsShowOnlyItem;

    public HashMapAdapter(Context context, HashMap<Object, Integer> data, int listType){
        mData  = new ArrayList();
        mData.addAll(data.entrySet());
        mListType=listType;
    }

    public HashMapAdapter(Context context, int listType){
        mData  = new ArrayList();
        mListType = listType;
    }

    public HashMapAdapter(Context context, int listType, boolean isShowOnlyItem){
        mData  = new ArrayList();
        mListType = listType;
        mIsShowOnlyItem = isShowOnlyItem;
    }

    public void clearData() {
        // clear the data
        mData.clear();
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
    public long getItemId(int arg0) {
        return 0;
    }

    public View getView(int position, View convertView, ViewGroup parent) {
         View result=null;

        if (convertView == null) {
            switch (mListType)
            {
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
        // TODO replace findViewById by ViewHolder
        if(mIsShowOnlyItem)
            ((TextView) result.findViewById(R.id.list_view)).setText(item.getKey().toString());
        else
            ((TextView) result.findViewById(R.id.list_view)).setText(item.getKey().getClass().getSimpleName()+"  "+item.getKey().hashCode());

        return result;
    }

    public void addItems(HashMap<Object, Integer> data)
    {
        mData.addAll(data.entrySet());
    }


}