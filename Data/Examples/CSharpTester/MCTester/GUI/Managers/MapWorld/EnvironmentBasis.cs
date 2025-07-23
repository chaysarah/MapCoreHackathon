using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.Managers.MapWorld
{
    public class EnvironmentBasis
    {
        public EnvironmentBasis()
        {
            //c-tor
            

            m_AmbientLight = new DNSMcFColor(DNSMcFColor.fcWhiteOpaque);

            m_SkyColor = new DNSMcFColor();

            m_LensFlareEnabled = false;

            m_FogColor = new DNSMcFColor(0.75f, 0.75f, 0.75f, 1.0f);
            m_ExpDensity = 0.001f;
            m_LinearStart = 0.0f;
            m_LinearEnd = 50000.0f;

            m_CloudCover = 0.3f;

            m_CloudsSpeed = new DNSMcVector2D(0.000005f, -0.000009f);

            m_AbsoluteTime = new DateTime();
            m_IncrementSecTime = 15 * 60;
            m_EnableTimeAutoUpdate = true;
            m_TimeAutoUpdateFactor = 2000.0f; // 1 day in 43 seconds
        }


        #region Data Members
        //General Members
        IDNMcMapEnvironment m_MapEnvironment;
        DNSMcFColor m_AmbientLight;
        

        //Sky Members
        DNSMcFColor m_SkyColor;

        //Sun Members
        bool m_LensFlareEnabled;

        //Fog Members
        DNSMcFColor m_FogColor;
        float m_ExpDensity;
        float m_LinearStart;
        float m_LinearEnd;

        //Clouds Members
        float m_CloudCover;
        DNSMcVector2D m_CloudsSpeed;

        //Time Members
        DateTime m_AbsoluteTime;
        int m_IncrementSecTime;
        bool m_EnableTimeAutoUpdate;
        float m_TimeAutoUpdateFactor;

        #endregion

        #region Public Properties
        public IDNMcMapEnvironment MapEnvironment
        {
            get { return m_MapEnvironment; }
            set { m_MapEnvironment = value; }
        }

        public DNSMcFColor AmbientLight
        {
            get { return m_AmbientLight; }
            set { m_AmbientLight = value; }
        }

        public DNSMcFColor SkyColor
        {
            get { return m_SkyColor; }
            set { m_SkyColor = value; }
        }

        public bool LensFlareEnabled
        {
            get { return m_LensFlareEnabled; }
            set { m_LensFlareEnabled = value; }
        }

        public DNSMcFColor FogColor
        {
            get { return m_FogColor; }
            set { m_FogColor = value; }
        }

        public float ExpDensity
        {
            get { return m_ExpDensity; }
            set { m_ExpDensity = value; }
        }

        public float LinearStart
        {
            get { return m_LinearStart; }
            set { m_LinearStart = value; }
        }

        public float LinearEnd
        {
            get { return m_LinearEnd; }
            set { m_LinearEnd = value; }
        }

        public float CloudCover
        {
            get { return m_CloudCover; }
            set { m_CloudCover = value; }
        }

        public DNSMcVector2D CloudsSpeed
        {
            get { return m_CloudsSpeed; }
            set { m_CloudsSpeed = value; }
        }

        public DateTime AbsoluteTime
        {
            get { return m_AbsoluteTime; }
            set { m_AbsoluteTime = value; }
        }

        public int IncrementSecTime
        {
            get { return m_IncrementSecTime; }
            set { m_IncrementSecTime = value; }
        }

        public bool EnableTimeAutoUpdate
        {
            get { return m_EnableTimeAutoUpdate; }
            set { m_EnableTimeAutoUpdate = value; }
        }

        public float TimeAutoUpdateFactor
        {
            get { return m_TimeAutoUpdateFactor; }
            set { m_TimeAutoUpdateFactor = value; }
        }

        #endregion
       

    }
}
