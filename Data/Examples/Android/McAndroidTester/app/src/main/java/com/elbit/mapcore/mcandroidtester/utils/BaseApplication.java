package com.elbit.mapcore.mcandroidtester.utils;

import android.app.Activity;
import android.app.Application;
import android.content.Context;

import com.elbit.mapcore.mcandroidtester.ui.map.AMcGLSurfaceView;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.MapFragment;

/**
 * Created by tc99382 on 12/09/2016.
 */
public class BaseApplication extends Application {

    public static MapFragment getmMapFragment() {
        return mMapFragment;
    }

    public static void setmMapFragment(MapFragment mMapFragment) {
        BaseApplication.mMapFragment = mMapFragment;
    }

    private static MapFragment mMapFragment;
    private static Context mContext;

    public static AMcGLSurfaceView getMcGLSurfaceView() {
        return mcGLSurfaceView;
    }

    public static void setMcGLSurfaceView(AMcGLSurfaceView mcGLSurfaceView) {
        BaseApplication.mcGLSurfaceView = mcGLSurfaceView;
    }

    private static AMcGLSurfaceView mcGLSurfaceView;

    public void onCreate() {
        super.onCreate();
        BaseApplication.mContext = getApplicationContext();
    }

    public static Context getAppContext() {
        return BaseApplication.mContext;
    }

    public static Context getCurrActivityContext() {
        return BaseApplication.mContext;
    }
    public static Context setCurrActivityContext(Context context) {
        return BaseApplication.mContext = context;
    }

    private static Activity mMainActivity;

    public static void setMainActivity(Activity activity) {
        BaseApplication.mMainActivity = activity;
    }
    public static Activity getMainActivity() {
        return BaseApplication.mMainActivity;
    }
}
