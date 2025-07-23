package com.elbit.mapcore.mcandroidtester.ui.adapters;

import android.app.Activity;
import android.content.Context;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.BaseAdapter;
import android.widget.Spinner;
import android.widget.TextView;

import com.elbit.mapcore.Enums.EMcPointCoordSystem;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Calculations.IMcSpatialQueries;
import com.elbit.mapcore.Interfaces.General.IMcEditMode;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditText;

/**
 * Created by tc99382 on 03/09/2017.
 */
public class KeyStepTypeAdapter extends BaseAdapter {
    float[] mTypeValues;
    IMcEditMode.EKeyStepType[] mKeyStepTypes;
    IMcEditMode mEditMode;
    Context mContext;

    public KeyStepTypeAdapter(Context context, IMcEditMode editMode ) {
        mKeyStepTypes = IMcEditMode.EKeyStepType.values();
        mTypeValues = new float[mKeyStepTypes.length];
        mEditMode = editMode;
        mContext = context;
    }

    /**
     * How many items are in the data set represented by this Adapter.
     *
     * @return Count of items.
     */
    @Override
    public int getCount() {
        return mKeyStepTypes.length;
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
    public View getView(final int position, View convertView, ViewGroup parent) {
        RowHolder rowHolder;
        if (convertView == null) {
            convertView = ((Activity) mContext).getLayoutInflater().inflate(R.layout.key_step_type_row, parent, false);
            rowHolder = new RowHolder();
            rowHolder.mTypeTV = (TextView) convertView.findViewById(R.id.key_step_type_type);
            rowHolder.mItemValue = (NumericEditText) convertView.findViewById(R.id.key_step_type_value);
        } else {
            rowHolder = (RowHolder) convertView.getTag();
        }
        rowHolder.mTypeTV.setText(mKeyStepTypes[position].name());
        try {
            float value = mEditMode.GetKeyStep(mKeyStepTypes[position]);
            mTypeValues[position] = value;
            rowHolder.mItemValue.setText(String.valueOf(value));
            rowHolder.mItemValue.getEditText().addTextChangedListener(new TextWatcher() {
                @Override
                public void beforeTextChanged(CharSequence s, int start, int count, int after) {

                }

                @Override
                public void onTextChanged(CharSequence s, int start, int before, int count) {

                }

                @Override
                public void afterTextChanged(Editable s) {
                    if (s != null && !String.valueOf(s).isEmpty())
                        mTypeValues[position] = Float.valueOf(s.toString());
                }
            });
        }
        catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(mContext, mcEx, "GetKeyStep");
        }  catch (Exception e) {
            e.printStackTrace();
        }

        convertView.setTag(rowHolder);
        return convertView;
    }

    public class RowHolder
    {
        TextView mTypeTV;
        NumericEditText mItemValue;

    }
}
