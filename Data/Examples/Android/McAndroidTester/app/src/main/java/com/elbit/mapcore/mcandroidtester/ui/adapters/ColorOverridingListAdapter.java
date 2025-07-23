package com.elbit.mapcore.mcandroidtester.ui.adapters;

import android.app.Activity;
import android.content.Context;
import android.graphics.Color;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.EditText;
import android.widget.TextView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.model.AMCTColorOverriding;
import com.elbit.mapcore.mcandroidtester.utils.colorpicker.ColorPickerDialog;

import com.elbit.mapcore.General.CMcEnumBitField;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlay;
import com.elbit.mapcore.Structs.SMcBColor;

/**
 * Created by tc97803 on 22/08/2017.
 */

public class ColorOverridingListAdapter extends ArrayAdapter<AMCTColorOverriding> {

    private Context mContext;
    private int mResourceId;
     public AMCTColorOverriding[] ColorOverridingArr;
    //private HashMap<Integer, ColorOverridingHolder> mUpdatedRowsHM;
    private int iMaxCount;

    /**
     * Constructor
     *
     * @param context                   The current context.
     * @param resource                  The resource ID for a layout file containing a TextView to use when
     *                                  instantiating views.
     * @param objects                   The objects to represent in the ListView.
     */
    public ColorOverridingListAdapter(
            Context context,
            int resource,
            AMCTColorOverriding[] objects)
    {
        super(context, resource, objects);
        mContext = context;
        mResourceId = resource;
        ColorOverridingArr = objects;
       // mUpdatedRowsHM = new HashMap<>();
    }


    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        View row = convertView;
        ColorOverridingHolder holder = null;
        if (row == null) {
            row = ((Activity) mContext).getLayoutInflater().inflate(mResourceId, parent, false);
            holder = new ColorOverridingHolder();
            holder.ecpt_type = (TextView) row.findViewById(R.id.color_overriding_ecpt_types);
            holder.enable = (CheckBox) row.findViewById(R.id.color_overriding_enable);
            holder.color = (Button)  row.findViewById(R.id.color_overriding_color_bttn);
            holder.alpha = (EditText) row.findViewById(R.id.color_overriding_alpha);
            holder.replace_rgb = (CheckBox) row.findViewById(R.id.color_overriding_replace_rgb);
            holder.replace_alpha = (CheckBox) row.findViewById(R.id.color_overriding_replace_alpha);
            holder.modulate_rgb = (CheckBox) row.findViewById(R.id.color_overriding_modulate_rgb);
            holder.modulate_alpha = (CheckBox) row.findViewById(R.id.color_overriding_modulate_alpha);
            holder.add_rgb = (CheckBox) row.findViewById(R.id.color_overriding_add_rgb);
            holder.add_alpha = (CheckBox) row.findViewById(R.id.color_overriding_add_alpha);
            holder.sub_rgb = (CheckBox) row.findViewById(R.id.color_overriding_sub_rgb);
            holder.sub_alpha = (CheckBox) row.findViewById(R.id.color_overriding_sub_alpha);
            holder.postprocess_add_rgb = (CheckBox) row.findViewById(R.id.color_overriding_postprocess_add_rgb);
            holder.postprocess_sub_rgb = (CheckBox) row.findViewById(R.id.color_overriding_postprocess_sub_rgb);

            holder = setCurrRowValues(holder, position);
            row.setTag(holder);
        }
        return row;
    }

    @Override
    public int getViewTypeCount() {
        return getCount();
    }

    @Override
    public int getItemViewType(int position) {
        return position;
    }

    @Override
    public int getCount() {
        return ColorOverridingArr.length;
    }

    private ColorOverridingHolder setCurrRowValues(final ColorOverridingHolder holderColorOverriding, final int position) {
        AMCTColorOverriding curColorOverridingData = ColorOverridingArr[position];
        final IMcOverlay.EColorPropertyType currType = curColorOverridingData.eColorPropertyType;
        final IMcOverlay.SColorPropertyOverriding curColorOverriding = curColorOverridingData.sColorPropertyOverriding;

        holderColorOverriding.ecpt_type.setText(currType.toString());
        holderColorOverriding.enable.setChecked(curColorOverriding.bEnabled);
        holderColorOverriding.alpha.setText(Integer.toString(curColorOverriding.Color.a));
        holderColorOverriding.color.setBackgroundColor(Color.argb(255,curColorOverriding.Color.r,curColorOverriding.Color.g,curColorOverriding.Color.b));

        SetColorOverridingFlags(holderColorOverriding.replace_rgb, curColorOverriding, IMcOverlay.EColorComponentFlags.ECCF_REPLACE_RGB);
        SetColorOverridingFlags(holderColorOverriding.replace_alpha, curColorOverriding, IMcOverlay.EColorComponentFlags.ECCF_REPLACE_ALPHA);
        SetColorOverridingFlags(holderColorOverriding.modulate_rgb, curColorOverriding, IMcOverlay.EColorComponentFlags.ECCF_MODULATE_RGB);
        SetColorOverridingFlags(holderColorOverriding.modulate_alpha, curColorOverriding, IMcOverlay.EColorComponentFlags.ECCF_MODULATE_ALPHA);
        SetColorOverridingFlags(holderColorOverriding.add_rgb, curColorOverriding, IMcOverlay.EColorComponentFlags.ECCF_ADD_RGB);
        SetColorOverridingFlags(holderColorOverriding.add_alpha, curColorOverriding, IMcOverlay.EColorComponentFlags.ECCF_ADD_ALPHA);
        SetColorOverridingFlags(holderColorOverriding.sub_rgb, curColorOverriding, IMcOverlay.EColorComponentFlags.ECCF_SUB_RGB);
        SetColorOverridingFlags(holderColorOverriding.sub_alpha, curColorOverriding, IMcOverlay.EColorComponentFlags.ECCF_SUB_ALPHA);
        SetColorOverridingFlags(holderColorOverriding.postprocess_add_rgb, curColorOverriding, IMcOverlay.EColorComponentFlags.ECCF_POSTPROCESS_ADD_RGB);
        SetColorOverridingFlags(holderColorOverriding.postprocess_sub_rgb, curColorOverriding, IMcOverlay.EColorComponentFlags.ECCF_POSTPROCESS_SUB_RGB);

        holderColorOverriding.color.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                ColorPickerDialog colorPickerDialog = new ColorPickerDialog(mContext, 0, new ColorPickerDialog.OnColorSelectedListener() {
                    @Override
                    public void onColorSelected(int color) {
                        holderColorOverriding.color.setBackgroundColor(color);
                        curColorOverriding.Color = new SMcBColor(Color.red(color), Color.green(color), Color.blue(color), curColorOverriding.Color.a);
                    }
                });
                colorPickerDialog.show();
            }
        });

        holderColorOverriding.enable.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton compoundButton, boolean b) {
                curColorOverriding.bEnabled = b;
            }
        });

        holderColorOverriding.alpha.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence charSequence, int i, int i1, int i2) {}

            @Override
            public void onTextChanged(CharSequence charSequence, int i, int i1, int i2) { }

            @Override
            public void afterTextChanged(Editable editable) {
                String value = String.valueOf(editable);
                if(value.isEmpty())
                    value = "0";
               curColorOverriding.Color.a = Integer.valueOf(value);
            }
        });

        return holderColorOverriding;
    }

    private void SetColorOverridingFlags(CheckBox cb, final IMcOverlay.SColorPropertyOverriding curColorOverriding, final IMcOverlay.EColorComponentFlags flag)
    {
        CMcEnumBitField<IMcOverlay.EColorComponentFlags> componentFlags = curColorOverriding.uColorComponentsBitField;
        boolean value = (componentFlags.IsSet(flag));
        cb.setChecked(value);

        cb.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton compoundButton, boolean bIsSet) {
                if(bIsSet)
                    curColorOverriding.uColorComponentsBitField.Set(flag);
                else
                    curColorOverriding.uColorComponentsBitField.UnSet(flag);
            }
        });
    }


    public class ColorOverridingHolder {
        public TextView ecpt_type;
        public CheckBox enable;
        public Button color;
        public EditText alpha;
        public CheckBox replace_rgb;
        public CheckBox replace_alpha;
        public CheckBox modulate_rgb;
        public CheckBox modulate_alpha;
        public CheckBox add_rgb;
        public CheckBox add_alpha;
        public CheckBox sub_rgb;
        public CheckBox sub_alpha;
        public CheckBox postprocess_add_rgb;
        public CheckBox postprocess_sub_rgb;


    }
}
