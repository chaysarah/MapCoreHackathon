package com.elbit.mapcore.mcandroidtester.ui.adapters;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseExpandableListAdapter;
import android.widget.CheckedTextView;

import com.elbit.mapcore.mcandroidtester.R;

import java.util.ArrayList;
import java.util.HashMap;

import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;

/**
 * Created by tc99382 on 04/04/2017.
 */
public class ViewPortsObjectsELAdapter extends BaseExpandableListAdapter {
    private HashMap<Object, Integer> mLstObjects;
    private HashMap<Object, ArrayList<IMcObject>> mViewPortObjectsHM;
    private IMcMapViewport mMultiSelectedViewport;
    private Context mContext;
    private ArrayList mViewPorts;
    int mCheckedPos = -1;
    private HashMap<Integer, ArrayList<Integer>>  mViewPortObjectsSelectedHM = new HashMap<>();

    public ViewPortsObjectsELAdapter(HashMap<Object, Integer> hashMapVp, HashMap<Object, ArrayList<IMcObject>> viewPortObjectsHM, Context context) {
        mContext = context;
        mLstObjects = new HashMap<>();
        mViewPorts = new ArrayList();
        mViewPorts.addAll(hashMapVp.keySet());
        mViewPortObjectsHM = viewPortObjectsHM;

    }

    @Override
    public int getGroupCount() {
        return mViewPorts.size();
    }

    @Override
    public int getChildrenCount(int groupPosition) {
        return mViewPortObjectsHM.get(mViewPorts.get(groupPosition)).size();
    }

    /**
     * Gets the data associated with the given group.
     *
     * @param groupPosition the position of the group
     * @return the data child for the specified group
     */
    @Override
    public Object getGroup(int groupPosition) {
        return this.mViewPorts.get(groupPosition);
    }

    /**
     * Gets the data associated with the given child within the given group.
     *
     * @param groupPosition the position of the group that the child resides in
     * @param childPosition the position of the child with respect to other
     *                      children in the group
     * @return the data of the child
     */
    @Override
    public Object getChild(int groupPosition, int childPosition) {
        return mViewPortObjectsHM.get(mViewPorts.get(groupPosition)).get(childPosition);
    }

    /**
     * Gets the ID for the group at the given position. This group ID must be
     * unique across groups. The combined ID (see
     * {@link #getCombinedGroupId(long)}) must be unique across ALL items
     * (groups and all children).
     *
     * @param groupPosition the position of the group for which the ID is wanted
     * @return the ID associated with the group
     */
    @Override
    public long getGroupId(int groupPosition) {
        return mViewPorts.get(groupPosition).hashCode();
    }

    /**
     * Gets the ID for the given child within the given group. This ID must be
     * unique across all children within the group. The combined ID (see
     * {@link #getCombinedChildId(long, long)}) must be unique across ALL items
     * (groups and all children).
     *
     * @param groupPosition the position of the group that contains the child
     * @param childPosition the position of the child within the group for which
     *                      the ID is wanted
     * @return the ID associated with the child
     */
    @Override
    public long getChildId(int groupPosition, int childPosition) {
        return mViewPortObjectsHM.get(mViewPorts.get(groupPosition)).get(childPosition).hashCode();
    }


    @Override
    public boolean hasStableIds() {
        return false;
    }

    /**
     * Gets a View that displays the given group. This View is only for the
     * group--the Views for the group's children will be fetched using
     * {@link #getChildView(int, int, boolean, View, ViewGroup)}.
     *
     * @param groupPosition the position of the group for which the View is
     *                      returned
     * @param isExpanded    whether the group is expanded or collapsed
     * @param convertView   the old view to reuse, if possible. You should check
     *                      that this view is non-null and of an appropriate type before
     *                      using. If it is not possible to convert this view to display
     *                      the correct data, this method can create a new view. It is not
     *                      guaranteed that the convertView will have been previously
     *                      created by
     *                      {@link #getGroupView(int, boolean, View, ViewGroup)}.
     * @param parent        the parent that this view will eventually be attached to
     * @return the View corresponding to the group at the specified position
     */
    @Override
    public View getGroupView(int groupPosition, boolean isExpanded, View convertView, final ViewGroup parent) {
        IMcMapViewport viewport = (IMcMapViewport) getGroup(groupPosition);
        if (convertView == null) {
            LayoutInflater infalInflater = (LayoutInflater) this.mContext
                    .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
            convertView = infalInflater.inflate(R.layout.checkable_list_item, null);
        }

        final CheckedTextView viewportHeader = (CheckedTextView) convertView.findViewById(R.id.list_view);

        viewportHeader.setPadding(10, 10, 10, 10);
        viewportHeader.setText(viewport.getClass().getSimpleName() + "  " + viewport.hashCode());

        return convertView;
    }

    /**
     * Gets a View that displays the data for the given child within the given
     * group.
     *
     * @param groupPosition the position of the group that contains the child
     * @param childPosition the position of the child (for which the View is
     *                      returned) within the group
     * @param isLastChild   Whether the child is the last child within the group
     * @param convertView   the old view to reuse, if possible. You should check
     *                      that this view is non-null and of an appropriate type before
     *                      using. If it is not possible to convert this view to display
     *                      the correct data, this method can create a new view. It is not
     *                      guaranteed that the convertView will have been previously
     *                      created by
     *                      {@link #getChildView(int, int, boolean, View, ViewGroup)}.
     * @param parent        the parent that this view will eventually be attached to
     * @return the View corresponding to the child at the specified position
     */



    @Override
    public View getChildView(final int groupPosition, final int childPosition, boolean isLastChild, View convertView, ViewGroup parent) {
        IMcObject object = (IMcObject) getChild(groupPosition, childPosition);
        if (convertView == null) {
            LayoutInflater infalInflater = (LayoutInflater) this.mContext
                    .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
            convertView = infalInflater.inflate(R.layout.checkable_list_item, null);
        }
        final CheckedTextView objectHeader = (CheckedTextView) convertView.findViewById(R.id.list_view);
        objectHeader.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                ArrayList<Integer> lst = new ArrayList<>();
                boolean isChecked = !objectHeader.isChecked();
                objectHeader.setChecked(isChecked);
                if(mViewPortObjectsSelectedHM.containsKey(groupPosition))
                    lst = mViewPortObjectsSelectedHM.get(groupPosition);

                if(isChecked)
                {
                    lst.add(childPosition);
                }
                else
                {
                    if(lst.contains(childPosition))
                        lst.remove(new Integer(childPosition));
                }
                mViewPortObjectsSelectedHM.put(groupPosition,lst);
            }
        });

        objectHeader.setPadding(10, 10, 10, 10);
        objectHeader.setText("          " + object.getClass().getSimpleName() + "  " + object.hashCode());

        return convertView;
    }

    /**
     * Whether the child at the specified position is selectable.
     *
     * @param groupPosition the position of the group that contains the child
     * @param childPosition the position of the child within the group
     * @return whether the child is selectable.
     */
    @Override
    public boolean isChildSelectable(int groupPosition, int childPosition) {
        return true;
    }

     public HashMap<Integer, ArrayList<Integer>> getViewPortObjectsSelectedHM2() {
        return mViewPortObjectsSelectedHM;
    }

}
