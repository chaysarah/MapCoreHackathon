package com.elbit.mapcore.mcandroidtester.ui.adapters;

import android.content.Context;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.TextView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.utils.colorpicker.ColorPickerDialog;
import com.elbit.mapcore.mcandroidtester.utils.customviews.HeightColorItem;

import java.util.ArrayList;

/**
 * Created by TC99382 on 15/12/2016.
 */
public class HeightColorsRowAdapter extends ArrayAdapter<HeightColorItem> {
    private final ArrayList<HeightColorItem> mHeightColorsList;

    Context mContext;
    int mLayoutResource;

    public HeightColorsRowAdapter(Context context, int resource, ArrayList<HeightColorItem> heightColors) {
        super(context, resource, heightColors);
        mHeightColorsList = heightColors;
        mContext = context;
        mLayoutResource = resource;

    }


    @Override
    public int getCount() {
        return mHeightColorsList.size();
    }

    /**
     * Remove all elements from the list.
     */
    @Override
    public void clear() {
        super.clear();
    }

    public void addEmptyValuesRow() {
        //clear();
        add(new HeightColorItem(0, 255, 0));
    }

    @Override
    public HeightColorItem getItem(int position) {
        return mHeightColorsList.get(position);
    }

    @Override
    public long getItemId(int position) {
        return 0;
    }

    private static class ViewHolder {
        TextView color;
        EditText alpha;
        EditText height;
        HeightColorItem heightColorItem;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        final ViewHolder holder;
        View view = convertView;
        if (view == null) {
            holder=new ViewHolder();
            LayoutInflater layoutInflater = LayoutInflater.from(getContext());
            view = layoutInflater.inflate(mLayoutResource, null);
            holder.alpha=(EditText)view.findViewById(R.id.cv_height_colors_row_alpha_et);
            holder.height=(EditText)view.findViewById(R.id.cv_height_colors_row_height_et);
            holder.color=(TextView)view.findViewById(R.id.cv_height_colors_row_color_iv);
            view.setTag(holder);
        }else {
            holder = (ViewHolder) view.getTag();
        }

        holder.heightColorItem = getItem(position);

        if (holder.heightColorItem != null) {
            holder.color.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(final View v) {
                    ColorPickerDialog colorPickerDialog = new ColorPickerDialog(mContext, 0, new ColorPickerDialog.OnColorSelectedListener() {

                        @Override
                        public void onColorSelected(int color) {
                            holder.heightColorItem.setmColor(color);
                            v.setBackgroundColor(color);
                        }

                    });
                    colorPickerDialog.show();
                }
            });


            if (holder.color != null) {
                holder.color.setBackgroundColor(holder.heightColorItem.getmColor());
            }

            if (holder.alpha != null) {
                holder.alpha.setText(String.valueOf(holder.heightColorItem.getmAlpha()));
                holder.alpha.addTextChangedListener(new TextWatcher() {
                    @Override
                    public void beforeTextChanged(CharSequence s, int start, int count, int after) {

                    }

                    @Override
                    public void onTextChanged(CharSequence s, int start, int before, int count) {

                    }

                    @Override
                    public void afterTextChanged(Editable s) {
                        if (s != null && !String.valueOf(s).isEmpty()&&!String.valueOf(s).equals("-"))
                            holder.heightColorItem.setmAlpha(Integer.valueOf(String.valueOf(s)));

                    }
                });
            }

            if (holder.height != null) {
                holder.height.setText(String.valueOf(holder.heightColorItem.getmHeight()));
                holder.height.addTextChangedListener(new
                                                      TextWatcher() {
                                                          @Override
                                                          public void beforeTextChanged(CharSequence s, int start, int count, int after) {

                                                          }

                                                          @Override
                                                          public void onTextChanged(CharSequence s, int start, int before, int count) {

                                                          }

                                                          @Override
                                                          public void afterTextChanged(Editable s) {
                                                              if (s != null && !String.valueOf(s).isEmpty()&&!String.valueOf(s).equals("-"))
                                                                  holder.heightColorItem.setmHeight(Integer.valueOf(s.toString()));
                                                          }
                                                      });
            }
        }

        return view;

    }
}
