package com.elbit.mapcore.mcsimpletester;

import android.app.Activity;
import android.content.Context;
import android.graphics.Point;
import android.hardware.Sensor;
import android.hardware.SensorManager;
import android.opengl.GLES20;
import android.opengl.GLSurfaceView;
import android.util.AttributeSet;
import android.view.Display;
import android.view.MotionEvent;
import android.view.SurfaceHolder;

/**
 * Created by tc22949 on 09/06/2016.
 */
public class McGLSurfaceView extends GLSurfaceView {

     McGLSurfaceViewRenderer mRenderer;
    public McGLSurfaceView(Context context, AttributeSet attrs) {

        super(context, attrs);
        setEGLContextClientVersion ( 2 );
        setPreserveEGLContextOnPause(true);
        mRenderer = new McGLSurfaceViewRenderer(this,context);
        Display display = ((Activity)context).getWindowManager().getDefaultDisplay();
        final Point size = new Point();
        display.getSize(size);
        mRenderer.InitViewportSize(size.x,size.y );

        setRenderer ( mRenderer );
        setRenderMode ( GLSurfaceView.RENDERMODE_CONTINUOUSLY );


    }
     McGLSurfaceView(Context context ) {
        super ( context );
        setEGLContextClientVersion ( 2 );
        setPreserveEGLContextOnPause(true);
        mRenderer = new McGLSurfaceViewRenderer(this,context);
         Display display = ((Activity)context).getWindowManager().getDefaultDisplay();
         final Point size = new Point();
         display.getSize(size);
         mRenderer.InitViewportSize(size.x,size.y );

         setRenderer ( mRenderer );
        setRenderMode ( GLSurfaceView.RENDERMODE_CONTINUOUSLY );
    }

    public void Render()
    {
        queueEvent(new Runnable() {
            @Override
            public void run() {
                mRenderer.Render();
            }});

    }
    public void ZoomIn()
    {
        queueEvent(new Runnable() {
            @Override
            public void run() {
                float fScale = mRenderer.GetCameraScale();
                mRenderer.SetCameraScale(fScale/2);
            }});

    }
    public void ZoomOut()
    {
        queueEvent(new Runnable() {
            @Override
            public void run() {
                float fScale = mRenderer.GetCameraScale();
                mRenderer.SetCameraScale(fScale*2);
            }});

    }
    public void ChangeViewport(final EDisplayType eDisplayType)
    {
        queueEvent(new Runnable() {
            @Override
            public void run() {
                 mRenderer.ChangeViewport(eDisplayType);
            }});

    }

    public void AddObjects()
    {

     //   mRenderer.AddObjects();
        queueEvent(new Runnable() {
            @Override
            public void run() {
                mRenderer.AddObjects();
            }});

    }

    public void CreateViewport()
    {
        queueEvent(new Runnable() {
            @Override
            public void run() {
                mRenderer.CreateViewport();
            }});
        }
    public void surfaceCreated ( SurfaceHolder holder ) {
        super.surfaceCreated ( holder );
    }
    public void surfaceDestroyed ( SurfaceHolder holder ) {
        super.surfaceDestroyed ( holder );
    }

    public void surfaceChanged ( SurfaceHolder holder, int format, int w, int h ) {
        super.surfaceChanged ( holder, format, w, h );
    }

    @Override
    protected void onSizeChanged(int w, int h, int oldw, int oldh) {
        super.onSizeChanged(w, h, oldw, oldh);
        final int nWidth = w;
        final int nHeight = h;
        queueEvent(new Runnable() {
            @Override
            public void run() {
                mRenderer.SetViewportSize (nWidth, nHeight );
            }});
    }

    public void TouchEvent(final MotionEvent event)
    {
        queueEvent(new Runnable() {
            @Override
            public void run() {
                mRenderer.TouchEvent(event);
            }});

    }



    @Override
    public void onResume() {
        super.onResume();
        mRenderer.onResume();
    }
    @Override
    public void onPause() {
        super.onPause();
        mRenderer.onPause();

    }
}