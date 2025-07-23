package com.elbit.mapcore.mcandroidtester.model.Automation;

import com.elbit.mapcore.Interfaces.Map.IMcImageProcessing;

public class AMCTImageProcessingChannelData {
    public int Channel;
    public byte[] UserColorValues;
    public boolean UserColorValuesUse;
    public double Brightness;
    public double Contrast;
    public boolean Negative;
    public double Gamma;
    public boolean HistogramEqualization;
    public long[] ReferenceHistogram;
    public boolean VisibleAreaOriginalHistogram ;
    public boolean ReferenceHistogramUse ;
    public boolean IsOriginalHistogramSet ;
    public long[] OriginalHistogram ;
    public boolean HistogramNormalizationUse;
    public double HistogramNormalizationMean;
    public double HistogramNormalizationStdev;
    public AMCTImageProcessingChannelData()
    {

    }
}
