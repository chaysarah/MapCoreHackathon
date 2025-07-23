package com.elbit.mapcore.mcandroidtester.ui.adapters;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseExpandableListAdapter;
import android.widget.TextView;

import com.elbit.mapcore.mcandroidtester.R;

import java.util.HashMap;
import java.util.List;

import com.elbit.mapcore.Interfaces.Map.IMcMapTerrain;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;

/**
 * Created by tc99382 on 16/11/2016.
 */
public class ViewPortsTerrainsELAdapter extends BaseExpandableListAdapter {

    private Context mContext;
    private List<IMcMapViewport> mViewPortsList; // header titles
    // child data in format of header title, child title
    private HashMap<IMcMapViewport, List<IMcMapTerrain>> viewPortsTerrainsList;

    public ViewPortsTerrainsELAdapter(Context context, List<IMcMapViewport> viewPortsList,
                                 HashMap<IMcMapViewport, List<IMcMapTerrain>> viewPortsTerrainsList) {
        this.mContext = context;
        this.mViewPortsList = viewPortsList;
        this.viewPortsTerrainsList = viewPortsTerrainsList;
    }

    @Override
    public Object getChild(int groupPosition, int childPosititon) {
        return this.viewPortsTerrainsList.get(this.mViewPortsList.get(groupPosition))
                .get(childPosititon);
    }

    @Override
    public long getChildId(int groupPosition, int childPosition) {
        return childPosition;
    }

    @Override
    public View getChildView(int groupPosition, final int childPosition,
                             boolean isLastChild, View convertView, ViewGroup parent) {

        final IMcMapTerrain terrain = (IMcMapTerrain) getChild(groupPosition, childPosition);

        if (convertView == null) {
            LayoutInflater infalInflater = (LayoutInflater) this.mContext.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
            convertView = infalInflater.inflate(R.layout.list_item, null);
        }

        TextView txtListChild = (TextView) convertView.findViewById(R.id.list_view);

        txtListChild.setText(terrain.getClass().getSimpleName()+" "+terrain.hashCode());
        return convertView;
    }

    @Override
    public int getChildrenCount(int groupPosition) {
        return this.viewPortsTerrainsList.get(this.mViewPortsList.get(groupPosition))
                .size();
    }

    @Override
    public Object getGroup(int groupPosition) {
        return this.mViewPortsList.get(groupPosition);
    }

    @Override
    public int getGroupCount() {
        return this.mViewPortsList.size();
    }

    @Override
    public long getGroupId(int groupPosition) {
        return groupPosition;
    }

    @Override
    public View getGroupView(int groupPosition, boolean isExpanded,
                             View convertView, ViewGroup parent) {
        IMcMapViewport viewport = (IMcMapViewport) getGroup(groupPosition);
        if (convertView == null) {
            LayoutInflater infalInflater = (LayoutInflater) this.mContext
                    .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
            convertView = infalInflater.inflate(R.layout.list_item, null);
        }

        TextView viewportHeader = (TextView) convertView.findViewById(R.id.list_view);
        viewportHeader.setPadding(0,6,0,6);
        //viewportHeader.setTypeface(null, Typeface.BOLD);
        viewportHeader.setText(viewport.getClass().getSimpleName()+"  "+viewport.hashCode());

        return convertView;
    }

    @Override
    public boolean hasStableIds() {
        return false;
    }

    @Override
    public boolean isChildSelectable(int groupPosition, int childPosition) {
        return true;
    }
}
