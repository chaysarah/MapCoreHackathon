package com.elbit.mapcore.mcandroidtester.model;

/**
 * Created by tc99382 on 17/07/2017.
 */
public class MenuAction {
    private boolean mToRunOnQueueEvent;
    private Runnable mRunnable;

    public MenuAction(boolean mToRunOnQueueEvent, Runnable mRunnable) {
        this.mToRunOnQueueEvent = mToRunOnQueueEvent;
        this.mRunnable = mRunnable;
    }

    public boolean isToRunOnQueueEvent() {
        return mToRunOnQueueEvent=false;
    }
    public Runnable getRunnable() {
        return mRunnable;
    }


}
