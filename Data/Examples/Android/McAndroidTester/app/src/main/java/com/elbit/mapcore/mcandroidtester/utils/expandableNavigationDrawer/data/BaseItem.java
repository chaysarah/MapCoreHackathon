package com.elbit.mapcore.mcandroidtester.utils.expandableNavigationDrawer.data;

/**
 * Created by awidiyadew on 12/09/16.
 */
public class BaseItem {
    private String mName;
    private int mIcon;

    public Runnable getAction() {
        return mAction;
    }

    private Runnable mAction;

    public int getIcon() {
        return mIcon;
    }

    public BaseItem(String name,int icon,Runnable action) {
        mName = name;
        mIcon=icon;
        mAction=action;
    }
    public BaseItem(String name,int icon) {
        mName = name;
        mIcon=icon;
    }
    public String getName() {
        return mName;
    }
}
