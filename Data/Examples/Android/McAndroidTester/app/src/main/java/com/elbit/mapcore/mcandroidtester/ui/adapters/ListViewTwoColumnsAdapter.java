package com.elbit.mapcore.mcandroidtester.ui.adapters;

/**
 * Created by tc97803 on 24/08/2016.
 */
import android.app.Activity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import com.elbit.mapcore.mcandroidtester.R;

import java.util.ArrayList;
import java.util.HashMap;

import static com.elbit.mapcore.mcandroidtester.utils.Consts.FIRST_COLUMN;
import static com.elbit.mapcore.mcandroidtester.utils.Consts.SECOND_COLUMN;

public class ListViewTwoColumnsAdapter extends BaseAdapter {
    public ArrayList<HashMap<String, String>> list;
    Activity activity;
    TextView txtFirst;
    TextView txtSecond;

    public ListViewTwoColumnsAdapter(Activity activity, ArrayList<HashMap<String, String>> list) {
        super();
        this.activity = activity;
        this.list = list;
    }

    @Override
    public int getCount() {
        // TODO Auto-generated method stub
        return list.size();
    }

    @Override
    public Object getItem(int position) {
        // TODO Auto-generated method stub
        return list.get(position);
    }

    @Override
    public long getItemId(int position) {
        // TODO Auto-generated method stub
        return 0;
    }


    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        // TODO Auto-generated method stub

        LayoutInflater inflater = activity.getLayoutInflater();

        if (convertView == null) {

            convertView = inflater.inflate(R.layout.listview_2columns_template, null);

            txtFirst = (TextView) convertView.findViewById(R.id.FIRST_COLUMN);
            txtSecond = (TextView) convertView.findViewById(R.id.SECOND_COLUMN);
        }

        HashMap<String, String> map = list.get(position);
        txtFirst.setText(map.get(FIRST_COLUMN));
        txtSecond.setText(map.get(SECOND_COLUMN));

        return convertView;
    }

}
