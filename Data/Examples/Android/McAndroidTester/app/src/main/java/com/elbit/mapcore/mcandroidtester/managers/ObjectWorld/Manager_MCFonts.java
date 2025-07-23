package com.elbit.mapcore.mcandroidtester.managers.ObjectWorld;

import java.util.HashMap;
import java.util.Set;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcFont;

/**
 * Created by TC97803 on 16/11/2017.
 */

public class Manager_MCFonts {
    public HashMap<IMcFont, Integer> getFonts() {
        return mFonts;
    }

    static HashMap<IMcFont,Integer> mFonts;
    private static Manager_MCFonts instance;

    private Manager_MCFonts() {
        mFonts =new HashMap<>();
    }
    public void addToDictionary(IMcFont Font)
    {
        //Add Font to dictionary in case it doesn't exist already.
        if(!mFonts.containsValue(Font))
        {
            try {
                mFonts.put(Font,Font.GetFontType().getValue());
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }
    public void removeFromDictionary(IMcFont Font)
    {
        //Remove Font from dictionary
        mFonts.remove(Font);
    }

    public int getItemIndex(IMcFont font)
    {
        int index = 0;
        Set<IMcFont> fontSet = mFonts.keySet();
        for (IMcFont f:fontSet ) {
            if (f == font)
                return index;
            index++;
        }
        return -1;
    }

    public static Manager_MCFonts getInstance() {
        if (instance == null) {
            instance = new Manager_MCFonts();
        }
        return instance;
    }
}
