package com.elbit.mapcore.mcandroidtester.ui.adapters;

import android.app.Activity;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.TextView;

import com.elbit.mapcore.mcandroidtester.R;

/**
 * Created by tc99382 on 10/01/2017.
 */
public class ObjectLocationPointAdapter extends BaseAdapter {

    private final Context mContext;
    //private final SMcVector3D mPoint;

 /*   public ObjectLocationPointAdapter(Context context, SMcVector3D point) {
        mContext=context;
        mPoint=point;
    }*/

    public ObjectLocationPointAdapter(Context context) {

        mContext=context;
    }

    /**
     * How many items are in the data set represented by this Adapter.
     *
     * @return Count of items.
     */
    @Override
    public int getCount() {
        //TODO orit, change it to the num of exist objects
        return 0;
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
        return null;
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

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        final ViewHolder viewHolder;
        LayoutInflater inflater = ((Activity)mContext).getLayoutInflater();

        if (convertView == null) {
            viewHolder=new ViewHolder();
            convertView = inflater.inflate(R.layout.cv_object_location_point_row, null);
            viewHolder.objectTV= (TextView) convertView.findViewById(R.id.obj_location_point_row_object);
            viewHolder.objectXTV= (TextView) convertView.findViewById(R.id.obj_location_point_row_point_x_tv);
            viewHolder.objectYTV= (TextView) convertView.findViewById(R.id.obj_location_point_row_point_y_tv);
            viewHolder.objectZTV= (TextView) convertView.findViewById(R.id.obj_location_point_row_point_z_tv);
            viewHolder.choosePointBttn=(Button)convertView.findViewById(R.id.obj_location_point_row_choose_point_bttn);
            convertView.setTag(viewHolder);
        }
        else
            viewHolder= (ViewHolder) convertView.getTag();

        //TODO add here the fields init, for example
        viewHolder.choosePointBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

            }
        });



        return convertView;
    }
    private static class ViewHolder
    {
        TextView objectTV;
        TextView objectXTV;
        TextView objectYTV;
        TextView objectZTV;
        Button choosePointBttn;
    }
}
