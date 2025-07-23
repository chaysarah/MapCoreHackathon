package com.elbit.mapcore.mcandroidtester.model;

import com.elbit.mapcore.mcandroidtester.ui.objects.ObjectPropertiesBase;

import java.io.Serializable;
import java.util.ArrayList;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcLogFont;
import com.elbit.mapcore.Structs.SMcVariantLogFont;

/**
 * Created by tc99382 on 23/02/2017.
 */
public class AMCTFontMapping implements Serializable {
public IMcLogFont.SLogFontToTtfFile[] getLogFontToTtfFromSavedMapping()
{
    IMcLogFont.SLogFontToTtfFile[] logFontMappings=new IMcLogFont.SLogFontToTtfFile[this.logFontMappings.size()];
    for(int i=0;i<logFontMappings.length;i++)
    {
        logFontMappings[i]=new IMcLogFont.SLogFontToTtfFile();
        logFontMappings[i].LogFont=new SMcVariantLogFont();
        logFontMappings[i].LogFont.LogFont=new SMcVariantLogFont.LOGFONT();
        logFontMappings[i].strTtfFileFullPathName=this.logFontMappings.get(i).strTtfFileFullPathName;
        logFontMappings[i].LogFont.LogFont.lfHeight=this.logFontMappings.get(i).lfHeight;
        logFontMappings[i].LogFont.LogFont.lfWeight=this.logFontMappings.get(i).lfWeight;
        logFontMappings[i].LogFont.LogFont.lfFaceName=this.logFontMappings.get(i).lfFaceName;
        logFontMappings[i].LogFont.LogFont.lfItalic=this.logFontMappings.get(i).lfItalic;
        logFontMappings[i].LogFont.LogFont.lfUnderline=this.logFontMappings.get(i).lfUnderline;
    }
    return logFontMappings;
}
    public AMCTFontMapping() {
        IMcLogFont.SLogFontToTtfFile[] logFontMappings;
        if(ObjectPropertiesBase.Text.getInstance().mTextFontsMap !=null)
         logFontMappings=ObjectPropertiesBase.Text.getInstance().mTextFontsMap;
        else
        logFontMappings=new IMcLogFont.SLogFontToTtfFile[0];

        this.logFontMappings=new ArrayList<>(logFontMappings.length);
        for(IMcLogFont.SLogFontToTtfFile font : logFontMappings)
        {
            LogFontToTtfFile curFont=new LogFontToTtfFile();
            curFont.lfFaceName=font.LogFont.LogFont.lfFaceName;
            curFont.lfHeight=font.LogFont.LogFont.lfHeight;
            curFont.lfWeight=font.LogFont.LogFont.lfWeight;
            curFont.lfItalic=font.LogFont.LogFont.lfItalic;
            curFont.lfUnderline=font.LogFont.LogFont.lfUnderline;
            curFont.strTtfFileFullPathName=font.strTtfFileFullPathName;
            this.logFontMappings.add(curFont);
        }
        this.selectedMappingPos=ObjectPropertiesBase.Text.getInstance().mTextCurMappingPos;
    }
    ArrayList<LogFontToTtfFile> logFontMappings;

    public int getSelectedMappingPos() {
        return selectedMappingPos;
    }

    int selectedMappingPos;



    static class LogFontToTtfFile implements Serializable
    {
        public String strTtfFileFullPathName;
        public int lfHeight;
        public int lfWeight;
        public byte lfItalic;
        public byte lfUnderline;
        public String lfFaceName;
    }
}
