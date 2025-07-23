package com.elbit.mapcore.mcandroidtester.managers.ObjectWorld;

import java.util.HashMap;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcTexture;

/**
 * Created by tc99382 on 24/01/2017.
 */
public class Manager_MCTextures {
    public  HashMap<Object, Integer> getTextures() {
        return mTextures;
    }

    static HashMap<Object,Integer> mTextures;
    private static Manager_MCTextures instance;

    private Manager_MCTextures() {
        mTextures =new HashMap<>();
    }
    public void addToDictionary(IMcTexture texture)
    {
        //Add texture to dictionary in case it doesn't exist already.
        if(!mTextures.containsValue(texture))
        {
            mTextures.put(texture,texture.GetTextureType().getValue());
        }
    }
    public void removeFromDictionary(IMcTexture texture)
    {
        //Remove texture from dictionary
        mTextures.remove(texture);
    }




    public static Manager_MCTextures getInstance() {
        if (instance == null) {
            instance = new Manager_MCTextures();
        }
        return instance;
    }
}
