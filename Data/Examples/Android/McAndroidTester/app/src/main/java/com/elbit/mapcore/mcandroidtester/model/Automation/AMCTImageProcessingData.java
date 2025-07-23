package com.elbit.mapcore.mcandroidtester.model.Automation;

public class AMCTImageProcessingData {
    public int Filter;
    public boolean IsEnableColorTableImageProcessing;
    public byte WhiteBalanceBrightnessR;
    public byte WhiteBalanceBrightnessG;
    public byte WhiteBalanceBrightnessB;
    public AMCTImageProcessingChannelData[] ChannelDatas;
    public AMCTImageProcessingCustomFilter CustomFilter;

    public AMCTImageProcessingData() {
        ChannelDatas = new AMCTImageProcessingChannelData[4];
    }
}
