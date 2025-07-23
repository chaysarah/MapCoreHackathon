package com.elbit.mapcore.mcandroidtester.ui.adapters;

import android.app.Activity;
import android.content.Context;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.CompoundButton;
import android.widget.EditText;
import android.widget.RadioButton;
import android.widget.TextView;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectScheme;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.managers.Manager_MCNames;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.objectscheme_tabs.SchemeGeneralTabFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.objectscheme_tabs.StateNameRow;

import java.util.ArrayList;

public class ObjectStateModifierAdapter extends BaseAdapter {

    private final Context mContext;
    private final ArrayList<IMcObjectScheme.SObjectStateModifier> mObjectStateModifiers;
    private SchemeGeneralTabFragment mFragment;
    public int mSelectedIndex = -1;
    public RadioButton mSelectedRB;

    public ArrayList<IMcObjectScheme.SObjectStateModifier> getObjectStateModifiers() {
        return mObjectStateModifiers;
    }

    public ObjectStateModifierAdapter(Context mContext, ArrayList<IMcObjectScheme.SObjectStateModifier> objectStateModifiers, SchemeGeneralTabFragment fragment) {
        this.mContext = mContext;
        this.mObjectStateModifiers = objectStateModifiers;
        mFragment = fragment;
    }

    @Override
    public int getCount() {
        return mObjectStateModifiers.size();
    }

    @Override
    public Object getItem(int position) {
        return mObjectStateModifiers.get(position);
    }

    public void clearData() {
        // clear the data
        mObjectStateModifiers.clear();
    }

    @Override
    public long getItemId(int position) {
        return 0;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        View rowView = convertView;
        ModifierRowViewHolder viewHolder;
       // if (rowView == null) {
            LayoutInflater inflater = ((Activity) mContext).getLayoutInflater();
            rowView = inflater.inflate(R.layout.object_state_modifier, null);
            viewHolder = new ModifierRowViewHolder();
            viewHolder.isSelected = (RadioButton) rowView.findViewById(R.id.modifiers_rb);
            viewHolder.ConditionalSelector = (TextView) rowView.findViewById(R.id.conditional_selector_row_et);
            viewHolder.ActionOnResult = (TextView) rowView.findViewById(R.id.action_on_result_row_et);
            viewHolder.State = (TextView) rowView.findViewById(R.id.modifiers_state_row_et);
            viewHolder = setCurrRowValues(viewHolder, position);
       /* } else {
            viewHolder = (ModifierRowViewHolder) rowView.getTag();
        }*/
        rowView.setTag(viewHolder);
        return rowView;
    }

    private ModifierRowViewHolder setCurrRowValues(final ModifierRowViewHolder holder, final int position) {
        IMcObjectScheme.SObjectStateModifier modifier = mObjectStateModifiers.get(position);
        String name ="(null)";
        if (modifier.pConditionalSelector != null) {
            String type = "";
            try {
                type = modifier.pConditionalSelector.GetConditionalSelectorType().toString();
                name = Manager_MCNames.getInstance().getNameByObject(modifier.pConditionalSelector, type);
                name = name.replace("ECST_", "");
            } catch (MapCoreException e) {
                e.printStackTrace();
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
        holder.ConditionalSelector.setText(name);
        holder.ActionOnResult.setText(Boolean.toString(modifier.bActionOnResult));
        holder.State.setText(Byte.toString(modifier.uObjectState));

        holder.isSelected.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                if (isChecked) {
                    mSelectedIndex = position;
                    if(mSelectedRB != null)
                        mSelectedRB.setChecked(false);
                    mSelectedRB = (RadioButton)buttonView;
                    mFragment.EnableButtonAfterSelectModifier(isChecked);
                }
            }
        });

        return holder;
    }

    public class ModifierRowViewHolder {
        public RadioButton isSelected;
        public TextView ConditionalSelector;
        public TextView ActionOnResult;
        public TextView State;
    }
}
