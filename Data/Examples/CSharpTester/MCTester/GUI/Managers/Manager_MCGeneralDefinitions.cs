using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;
using MCTester.GUI.Map;
using MCTester.Managers.MapWorld;


namespace MCTester.Managers
{
    public static class Manager_MCGeneralDefinitions
    {
        #region Private Static Members; 

        private static int nRenderInterval;
        private static bool bHasPendingChanges;
        private static DNEPendingUpdateType uUpdateTypeBitField;
        private static bool bGetRenderStatistics;
        private static bool m_UseBasicItemPropertiesOnly = true;

        public static bool  mFirstMapLoaderDefinition = true;
        public static uint  m_NumBackgroundThreads = new DNSInitParams().uNumBackgroundThreads;
        public static float m_TerrainResolutionFactor = 1f;
        public static bool  m_MultiScreenDevice = false;
        public static bool  m_MultiThreadedDevice = false;
        public static bool m_ShowGeoInMetricProportion = false;
        public static string m_StrMagneticDataFileName = "";
        #endregion

        static Manager_MCGeneralDefinitions()
        {
            nRenderInterval = 60;
            bHasPendingChanges = false;
            uUpdateTypeBitField = 0;
            bGetRenderStatistics = false;
        }

        #region Public Static Properties

        public static int RenderInterval
        {
            get { return nRenderInterval; }
            set 
            {
                nRenderInterval = value;
                System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer();
                tmr.Interval = nRenderInterval;
                MCTMapForm.RenderTimer = tmr;
            }
        }

        public static bool HasPendingChanges
        {
            get { return bHasPendingChanges; }
            set
            {
                bHasPendingChanges = value;

                if (value == false)
                    Manager_StatusBar.UpdateMsg("");
            }
        }

        public static DNEPendingUpdateType UpdateTypeBitField
        {
            get { return uUpdateTypeBitField; }
            set { uUpdateTypeBitField = value; }
        }

        public static bool GetRenderStatistics
        {
            get { return bGetRenderStatistics; }
            set 
            {
                try
                {
                    if (MCTMapFormManager.MapForm != null && MCTMapFormManager.MapForm.Viewport != null)
                        MCTMapFormManager.MapForm.Viewport.ResetRenderStatistics();	
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("ResetRenderStatistics", McEx);
                }

                MainForm.MainFormStatusBarStatistics.Visible = value;
                bGetRenderStatistics = value; 
            }
        }

        // NumAntialiasingAlphaLevels
        public static uint NumAntialiasingAlphaLevels
        {
            get
            {
                try
                {
                    return DNMcFont.GetNumAntialiasingAlphaLevels();
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetNumAntialiasingAlphaLevels", McEx);
                }
                return 0;
            }
            set
            {
                try
                {
                    DNMcFont.SetNumAntialiasingAlphaLevels(value);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("SetNumAntialiasingAlphaLevels", McEx);
                }
            }
        }

        // CharacterSpacing
        public static uint CharacterSpacing
        {
            get
            {
                try
                {
                    return DNMcFont.GetCharacterSpacing();
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetCharacterSpacing", McEx);
                }
                return 0;
            }
            set
            {
                try
                {
                    DNMcFont.SetCharacterSpacing(value);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("SetCharacterSpacing", McEx);
                }

            }
        }

        public static bool UseBasicItemPropertiesOnly
        {
            get { return m_UseBasicItemPropertiesOnly; }
            set { m_UseBasicItemPropertiesOnly = value; }
        }

        #endregion
    }
}
