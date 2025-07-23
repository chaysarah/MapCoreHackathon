package com.elbit.mapcore.mcandroidtester.ui.adapters;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import com.elbit.mapcore.mcandroidtester.R;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectScheme;

/**
 * Created by tc99382 on 22/11/2016.
 */
public class SchemeAdapter extends BaseAdapter {
    private final IMcObjectScheme[] mSchemes;

    @Override
    public int getCount() {
        return mSchemes.length;
    }

    public SchemeAdapter(IMcObjectScheme[] schemes) {
        mSchemes = schemes;
    }

    @Override

    public Object getItem(int position) {
        return mSchemes[position];
    }

    @Override
    public long getItemId(int position) {
        return 0;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        View result = null;

        if (convertView == null)
            result = LayoutInflater.from(parent.getContext()).inflate(R.layout.radio_bttn_list_item, parent, false);
        else
            result = convertView;
        Object item = getItem(position);
        // TODO replace findViewById by ViewHolder
        ((TextView) result.findViewById(R.id.list_view)).setText("Scheme" +"  "+item.hashCode());
        return result;
    }
}
