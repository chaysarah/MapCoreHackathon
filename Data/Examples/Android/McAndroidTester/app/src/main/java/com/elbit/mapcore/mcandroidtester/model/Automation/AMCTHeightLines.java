package com.elbit.mapcore.mcandroidtester.model.Automation;

import com.elbit.mapcore.Interfaces.Map.IMcMapHeightLines;

import java.util.ArrayList;
import java.util.List;

public class AMCTHeightLines
    {
        public List<IMcMapHeightLines.SScaleStep> ScaleSteps ;
        public float LineWidth ;
        public AMCTColorInterpolationMode ColorInterpolationMode ;

        public AMCTHeightLines()
        {
            ScaleSteps = new ArrayList<>();
            ColorInterpolationMode = new AMCTColorInterpolationMode();
        }
    }
