package com.elbit.mapcore.mcandroidtester.managers;

import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;

/**
 * Created by tc99382 on 12/07/2016.
 */
public  class Manager_MCGeneralDefinitions {

        private static int nRenderInterval= 60;
        private static boolean bHasPendingChanges = false;
        private static IMcMapViewport.EPendingUpdateType uUpdateTypeBitField=IMcMapViewport.EPendingUpdateType.EPUT_ANY_UPDATE;;
        private static boolean bGetRenderStatistics;

        public static boolean  mFirstMapLoaderDefinition = true;
        public static int  m_NumBackgroundThreads = 1;
        public static float m_TerrainPrecisionFactor = 1f;
        public static boolean  m_MultiScreenDevice = false;
        public static boolean  m_MultiThreadedDevice = false;


}
