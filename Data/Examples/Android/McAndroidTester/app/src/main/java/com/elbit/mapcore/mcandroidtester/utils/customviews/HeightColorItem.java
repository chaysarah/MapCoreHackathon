package com.elbit.mapcore.mcandroidtester.utils.customviews;

/**
 * Created by TC99382 on 15/12/2016.
 */
public class HeightColorItem {
    int mColor;

    public void setmAlpha(int mAlpha) {
        this.mAlpha = mAlpha;
    }

    public void setmColor(int mColor) {
        this.mColor = mColor;
    }

    public void setmHeight(int mHeight) {
        this.mHeight = mHeight;
    }

    int mAlpha;
    int mHeight;

    public int getmAlpha() {
        return mAlpha;
    }

    public int getmColor() {
        return mColor;
    }

    public int getmHeight() {
        return mHeight;
    }



    public HeightColorItem(int mColor, int mAlpha, int mHeight) {
        this.mColor = mColor;
        this.mAlpha = mAlpha;
        this.mHeight = mHeight;
    }
}
