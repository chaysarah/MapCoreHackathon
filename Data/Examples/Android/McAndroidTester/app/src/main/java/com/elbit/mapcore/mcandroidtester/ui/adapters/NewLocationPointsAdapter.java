package com.elbit.mapcore.mcandroidtester.ui.adapters;

import android.app.Activity;
import android.content.Context;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ListView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditText;

import java.util.List;

import com.elbit.mapcore.Structs.SMcVector3D;

/**
 * Created by TC99382 on 14/06/2017.
 */
public class NewLocationPointsAdapter extends ArrayAdapter<SMcVector3D> {
    // private final SMcVector3D[] mLocationPoints;
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
    public NewLocationPointsAdapter(Context context, int resource, List<SMcVector3D> objects) {
        super(context, resource, objects);
        SMcVector3D[] locationPoints = new SMcVector3D[objects.size()];
        //  mLocationPoints = objects.toArray(locationPoints);
        mContext = context;
        mResourceId = resource;
    }
    public SMcVector3D[] getCurRowsData() {
        if(getCount()>0) {
            SMcVector3D[] curRowsValues;
            double x, y, z;
            SMcVector3D lastItem = getItem(getCount() - 1);
            if ((lastItem.x + lastItem.y + lastItem.z) > 0 || getCount() == 1)
                curRowsValues = new SMcVector3D[getCount()];
            else
                curRowsValues = new SMcVector3D[getCount() - 1];
            for (int i = 0; i < curRowsValues.length; i++) {
                x = getItem(i).x;
                y = getItem(i).y;
                z = getItem(i).z;
                curRowsValues[i] = new SMcVector3D(x, y, z);
            }
            return curRowsValues;
        }
        return null;
    }

    @Override
    public View getView(final int position, View convertView, final ViewGroup parent) {
        final RowHolder rowHolder;
        if (getCount() > 0) {
          //  if (convertView == null) {
                convertView = ((Activity) mContext).getLayoutInflater().inflate(mResourceId, parent, false);
                rowHolder = new RowHolder();
                rowHolder.xET = (NumericEditText) convertView.findViewById(R.id.new_location_point_row_x);
                rowHolder.yET = (NumericEditText) convertView.findViewById(R.id.new_location_point_row_y);
                rowHolder.zET = (NumericEditText) convertView.findViewById(R.id.new_location_point_row_z);

            /*} else {
                rowHolder = (RowHolder) convertView.getTag();
            }*/
                rowHolder.xET.getEditText().addTextChangedListener(new TextWatcher() {
                    @Override
                    public void beforeTextChanged(CharSequence s, int start, int count, int after) {

                    }

                    @Override
                    public void onTextChanged(CharSequence s, int start, int before, int count) {

                    }

                    @Override
                    public void afterTextChanged(Editable s) {
                        if (s.length() == 1 && position == (getCount() - 1)) {
                            add(new SMcVector3D());
                            Funcs.setListViewHeightBasedOnChildren((ListView) parent);
                            rowHolder.xET.post(new Runnable() {
                                @Override
                                public void run() {
                                    rowHolder.xET.setFocusableInTouchMode(true);
                                    rowHolder.xET.requestFocus();
                                }
                            });
                        }
                        try {
                            if (s.length() > 0)
                                getItem(position).x = Double.parseDouble(String.valueOf(s));
                        } catch (NumberFormatException e) {
                        }
                    }
                });
                rowHolder.yET.getEditText().addTextChangedListener(new TextWatcher() {
                    @Override
                    public void beforeTextChanged(CharSequence s, int start, int count, int after) {

                    }

                    @Override
                    public void onTextChanged(CharSequence s, int start, int before, int count) {

                    }

                    @Override
                    public void afterTextChanged(Editable s) {
                        try {
                            if (s.length() > 0)
                                getItem(position).y = Double.parseDouble(String.valueOf(s));
                        } catch (NumberFormatException e) {
                        }
                    }
                });
                rowHolder.zET.getEditText().addTextChangedListener(new TextWatcher() {
                    @Override
                    public void beforeTextChanged(CharSequence s, int start, int count, int after) {

                    }

                    @Override
                    public void onTextChanged(CharSequence s, int start, int before, int count) {

                    }

                    @Override
                    public void afterTextChanged(Editable s) {
                        try {
                            if (s.length() > 0)
                                getItem(position).z = Double.parseDouble(String.valueOf(s));
                        } catch (NumberFormatException e) {
                        }
                    }
                });
                rowHolder.xET.setText(String.valueOf(getItem(position).x));
                rowHolder.yET.setText(String.valueOf(getItem(position).y));
                rowHolder.zET.setText(String.valueOf(getItem(position).z));
                // convertView.setTag(rowHolder);
            }
      //  }
        return convertView;
    }


    private void saveCurEditedRow(int position) {

    }

    private class RowHolder {
        NumericEditText xET;
        NumericEditText yET;
        NumericEditText zET;
        int ref;
    }
}
