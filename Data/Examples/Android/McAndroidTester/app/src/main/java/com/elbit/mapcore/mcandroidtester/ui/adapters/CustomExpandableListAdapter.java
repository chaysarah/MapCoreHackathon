package com.elbit.mapcore.mcandroidtester.ui.adapters;

import android.content.Context;
import android.graphics.Typeface;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseExpandableListAdapter;
import android.widget.TextView;

import com.elbit.mapcore.mcandroidtester.R;

import java.util.HashMap;
import java.util.List;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeNode;

/**
 * Created by tc97803 on 21/11/2016.
 */
public class CustomExpandableListAdapter extends BaseExpandableListAdapter {
    private Context context;
    private List<IMcObject> expandableListTitle;
    private HashMap<IMcObject, List<IMcObjectSchemeNode>> expandableListDetail;

    public CustomExpandableListAdapter(Context context, List<IMcObject> expandableListTitle,
                                       HashMap<IMcObject, List<IMcObjectSchemeNode>> expandableListDetail) {
        this.context = context;
        this.expandableListTitle = expandableListTitle;
        this.expandableListDetail = expandableListDetail;
    }

    @Override
    public Object getChild(int listPosition, int expandedListPosition) {
        return this.expandableListDetail.get(this.expandableListTitle.get(listPosition))
                .get(expandedListPosition);
    }

    @Override
    public long getChildId(int listPosition, int expandedListPosition) {
        return expandedListPosition;
    }

    @Override
    public View getChildView(int listPosition, final int expandedListPosition,
                             boolean isLastChild, View convertView, ViewGroup parent) {
        final IMcObjectSchemeNode expandedNode = (IMcObjectSchemeNode) getChild(listPosition, expandedListPosition);
        if (convertView == null) {
            LayoutInflater layoutInflater = (LayoutInflater) this.context
                    .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
            convertView = layoutInflater.inflate(R.layout.list_item, null);
        }
        TextView expandedListTextView = (TextView) convertView
                .findViewById(R.id.list_view);
       // expandedListTextView.setTypeface(null, Typeface.BOLD);
        String itemType ="";
        try {
            itemType = expandedNode.GetNodeType().toString();
        } catch (Exception e) {
            e.printStackTrace();
        }
        expandedListTextView.setText(itemType+"  "+expandedNode.hashCode());
        return convertView;
    }

    @Override
    public int getChildrenCount(int listPosition) {
        return this.expandableListDetail.get(this.expandableListTitle.get(listPosition))
                .size();
    }

    @Override
    public Object getGroup(int listPosition) {
        return this.expandableListTitle.get(listPosition);
    }

    @Override
    public int getGroupCount() {
        return this.expandableListTitle.size();
    }

    @Override
    public long getGroupId(int listPosition) {
        return listPosition;
    }

    @Override
    public View getGroupView(int listPosition, boolean isExpanded,
                             View convertView, ViewGroup parent) {
        IMcObject listTitle = (IMcObject) getGroup(listPosition);
        if (convertView == null) {
            LayoutInflater layoutInflater = (LayoutInflater) this.context.
                    getSystemService(Context.LAYOUT_INFLATER_SERVICE);
            convertView = layoutInflater.inflate(R.layout.list_item, null);
        }
        TextView listTitleTextView = (TextView) convertView
                .findViewById(R.id.list_view);
        listTitleTextView.setTypeface(null, Typeface.BOLD);
        listTitleTextView.setPadding(0,6,0,6);
        listTitleTextView.setText(listTitle.getClass().getSimpleName()+"  "+listTitle.hashCode());
        return convertView;
    }

    @Override
    public boolean hasStableIds() {
        return false;
    }

    @Override
    public boolean isChildSelectable(int listPosition, int expandedListPosition) {
        return true;
    }

}
