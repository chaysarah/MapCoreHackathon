package com.elbit.mapcore.mcandroidtester.utils.expandableNavigationDrawer.data;

/**
 * Created by awidiyadew on 12/09/16.
 */
public class GroupItem extends BaseItem {
    private int mLevel;

    public GroupItem(String name,int icon) {
        super(name,icon);
        mLevel = 0;
    }
    public GroupItem(String name,int icon,int level) {
        super(name,icon);
        mLevel = level;
    }
    public void setLevel(int level){
        mLevel = level;
    }

    public int getLevel(){
        return mLevel;
    }
}
