package com.elbit.mapcore.mcandroidtester.ui.adapters;

import android.app.Activity;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.ImageView;
import android.widget.TextView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.utils.expandableNavigationDrawer.data.BaseItem;
import com.elbit.mapcore.mcandroidtester.utils.expandableNavigationDrawer.data.MenuDataProvider;
import com.elbit.mapcore.mcandroidtester.utils.expandableNavigationDrawer.views.LevelBeamView;

import java.util.List;

import pl.openrnd.multilevellistview.ItemInfo;
import pl.openrnd.multilevellistview.MultiLevelListAdapter;

/**
 * Created by tc99382 on 15/03/2017.
 */
public class MenuListAdapter extends MultiLevelListAdapter {
    private Activity mListContainerActivity;
    String mCheckedGroup = null;

    public MenuListAdapter(Activity listContainerActivity, String checkedGroup) {
        mListContainerActivity = listContainerActivity;
        mCheckedGroup = checkedGroup;
    }

    private class ViewHolder {
        TextView nameView;
        TextView infoView;
        ImageView arrowIV;
        LevelBeamView levelBeamView;
        ImageView iconIV;
        CheckBox showInViewPortCB;
    }

    @Override
    public List<?> getSubObjects(Object object) {
        return MenuDataProvider.getSubItems((BaseItem) object, mListContainerActivity);
    }

    @Override
    public boolean isExpandable(Object object) {
        return MenuDataProvider.isExpandable((BaseItem) object);
    }

    @Override
    public View getViewForObject(int i, final Object object, View convertView, final ItemInfo itemInfo) {
        final ViewHolder viewHolder;
        //TODO check why view holder not working
        convertView = null;
        /*if (convertView == null) */{
            viewHolder = new ViewHolder();
            convertView = LayoutInflater.from(mListContainerActivity).inflate(R.layout.data_item, null);
            viewHolder.nameView = (TextView) convertView.findViewById(R.id.dataItemName);
            viewHolder.arrowIV = (ImageView) convertView.findViewById(R.id.dataItemArrow);
            viewHolder.levelBeamView = (LevelBeamView) convertView.findViewById(R.id.dataItemLevelBeam);
            viewHolder.iconIV = (ImageView) convertView.findViewById(R.id.dataItemIcon);
            viewHolder.showInViewPortCB = (CheckBox) convertView.findViewById(R.id.data_item_cb);
            viewHolder.showInViewPortCB.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
                @Override
                public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                    //only if we on the root level
                    if (itemInfo.getLevel() == 0) {
                        if (isChecked) {
                            mCheckedGroup = ((BaseItem) object).getName();
                            ((MapsContainerActivity) mListContainerActivity).mMapFragment.addMenuBttnsToFragment(((BaseItem) object).getName());
                            MenuListAdapter.this.notifyDataSetChanged();
                        } else if (((BaseItem) object).getName().equals(mCheckedGroup)) {
                            mCheckedGroup = "";
                            ((MapsContainerActivity) mListContainerActivity).mMapFragment.removeMenuBttnsFromScreen();
                        }
                        ((MapsContainerActivity) mListContainerActivity).setMenuListCheckGroup(mCheckedGroup);
                    }
                }
            });
            convertView.setTag(viewHolder);
        } /*else {
            viewHolder = (ViewHolder) convertView.getTag();
        }*/

        viewHolder.nameView.setText(((BaseItem) object).getName());
        int icon = ((BaseItem) object).getIcon();
        if (icon != -1)
            viewHolder.iconIV.setImageResource(icon);

        viewHolder.showInViewPortCB.setChecked(((BaseItem) object).getName().equals(mCheckedGroup));
        if (itemInfo.isExpandable()) {
            viewHolder.iconIV.setVisibility(View.GONE);
            viewHolder.showInViewPortCB.setVisibility(View.VISIBLE);
            viewHolder.arrowIV.setVisibility(View.VISIBLE);
            viewHolder.arrowIV.setImageResource(itemInfo.isExpanded() ?
                    R.drawable.ic_expand_less : R.drawable.ic_expand_more);
        } else {
            viewHolder.iconIV.setVisibility(View.VISIBLE);
            viewHolder.showInViewPortCB.setVisibility(View.GONE);
            viewHolder.arrowIV.setVisibility(View.GONE);
        }
        viewHolder.levelBeamView.setLevel(itemInfo.getLevel());
        return convertView;
    }
}
