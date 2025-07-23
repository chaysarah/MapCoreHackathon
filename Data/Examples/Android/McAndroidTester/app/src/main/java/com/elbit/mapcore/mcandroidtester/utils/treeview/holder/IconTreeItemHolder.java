package com.elbit.mapcore.mcandroidtester.utils.treeview.holder;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.MenuInflater;
import android.view.View;
import android.widget.PopupMenu;
import android.widget.TextView;

import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlay_manager_treeview.OverlayManagerTreeViewFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.terrain_layers_treeview.TerrainLayersTreeViewFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.viewport_terrain_treeview.ViewportTerrainTreeViewFragment;
import com.github.johnkil.print.PrintView;
import com.elbit.mapcore.mcandroidtester.utils.treeview.model.TreeNode;
import com.elbit.mapcore.mcandroidtester.R;


public class IconTreeItemHolder extends TreeNode.BaseNodeViewHolder<IconTreeItemHolder.IconTreeItem> {
    private TextView mTvValue;
    private PrintView mArrowView;

    public IconTreeItemHolder(Context context) {
        super(context);
    }

    @Override
    public View createNodeView(final TreeNode node, final IconTreeItem value) {
        final LayoutInflater inflater = LayoutInflater.from(context);
        final View view = inflater.inflate(R.layout.layout_icon_node, null, false);
        mTvValue = (TextView) view.findViewById(R.id.icon_node_node_value);
        if(mTvValue != null)
        {
            mTvValue.setText(value.mText);
        }

        final PrintView iconView = (PrintView) view.findViewById(R.id.icon);
        if(iconView != null)
        {
            iconView.setIconText(context.getResources().getString(value.mIcon));
        }

        mArrowView = (PrintView) view.findViewById(R.id.arrow_icon);


        view.findViewById(R.id.btn_menu).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                showPopup(v,value.mMenuId,value.onMenuItemClickListener);
            }
        });

        //if My computer
        if (node.getLevel() == 1) {
           // view.findViewById(R.id.btn_delete).setVisibility(View.GONE);
        }
        //if no menu
        if (value.mMenuId <=0) {
            view.findViewById(R.id.btn_menu).setVisibility(View.GONE);
        }
        return view;
    }


    public void showPopup(View v, int mMenuId, PopupMenu.OnMenuItemClickListener menuClickListener) {
        PopupMenu popup = new PopupMenu(context, v);
        if(mMenuId>0) {
            MenuInflater inflater = popup.getMenuInflater();
            inflater.inflate(mMenuId, popup.getMenu());
            popup.setOnMenuItemClickListener(menuClickListener);
            popup.show();

        }
    }
    @Override
    public void toggle(boolean active) {
        mArrowView.setIconText(context.getResources().getString(active ? R.string.ic_keyboard_arrow_down : R.string.ic_keyboard_arrow_right));
    }

    public static class IconTreeItem {
        public int mIcon;
        public String mText;
        public String mFragmentName;
        public Object mImcObj;
        public int mMenuId;
        public PopupMenu.OnMenuItemClickListener onMenuItemClickListener;
        public TreeViewType mTreeViewType;

        public enum TreeViewType {
            OM,     // Overlay Manager
            TL,     // Terrains-Layers
            VT,     // Viewports-Terrains
            None;
        }

        public PopupMenu.OnMenuItemClickListener getOnMenuItemClickListener() {
            return onMenuItemClickListener;
        }

        public void setOnMenuItemClickListener(PopupMenu.OnMenuItemClickListener onMenuItemClickListener) {
            this.onMenuItemClickListener = onMenuItemClickListener;
        }

        public IconTreeItem(int icon, String text) {
            this.mIcon = icon;
            this.mText = text;
            mTreeViewType = TreeViewType.None;
        }

        public IconTreeItem(int icon, String text, TreeViewType treeViewType) {
            this.mIcon = icon;
            this.mText = text;
            mTreeViewType = treeViewType;
        }

       /* public IconTreeItem(int icon, String text, TreeViewType treeViewType, Object imcObj) {
            this.mIcon = icon;
            this.mText = text;
            mTreeViewType = treeViewType;
            this.mImcObj = imcObj;
        }*/

        public IconTreeItem(int icon, String text, Object imcObj) {
            this.mIcon = icon;
            this.mText = text;
            this.mImcObj = imcObj;
        }

        public IconTreeItem(int icon, String text, TreeViewType treeViewType, String fragmentName, Object imcObj, Integer menuId) {
            this.mIcon = icon;
            this.mText = text;
            mTreeViewType = treeViewType;
            mFragmentName = fragmentName;
            mImcObj=imcObj;
            mMenuId=menuId;
        }

        public IconTreeItem(int icon, String text, TreeViewType treeViewType, Object imcObj, Integer menuId, PopupMenu.OnMenuItemClickListener clickListener) {
            this.mIcon = icon;
            this.mText = text;
            mTreeViewType = treeViewType;
            mImcObj=imcObj;
            mMenuId=menuId;
            onMenuItemClickListener =clickListener;
        }

        public IconTreeItem(int icon, String text, TreeViewType treeViewType, String fragmentName, Object imcObj, Integer menuId, PopupMenu.OnMenuItemClickListener clickListener) {
            this.mIcon = icon;
            this.mText = text;
            mTreeViewType = treeViewType;
            mFragmentName = fragmentName;
            mImcObj=imcObj;
            mMenuId=menuId;
            onMenuItemClickListener =clickListener;
        }
    }
}
