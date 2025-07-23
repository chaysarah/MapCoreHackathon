using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace MCTester.General_Forms
{
    [Serializable]
    public class MCTStressTest
    {
        private cGeneralDefinitions m_GeneralDefinitions;
        private List<cTestCase> m_TestCase;
       
        public MCTStressTest()
        {            
        }

        #region Public Properties
        public cGeneralDefinitions GeneralDefinitions
        {
            get { return m_GeneralDefinitions; }
            set { m_GeneralDefinitions = value; }
        }

        public List<cTestCase> TestCase
        {
            get { return m_TestCase; }
            set { m_TestCase = value; }
        }

        #endregion

    }


    #region cTestCase
    [Serializable]
    public class cTestCase
    {
        private cLoadObjects m_LoadObjects;
        private cRemoveObjects m_RemoveObjects;
        private cStartObjcetsAnimation m_StartObjcetsAnimation;
        private cStopObjcetsAnimation m_StopObjcetsAnimation;
        private cMapZoom m_MapZoom;

        private List<string> m_TestManager;

        public cTestCase()
        {
            m_TestManager = new List<string>();
        }

        public List<string> TestManager
        {
            get { return m_TestManager; }
            set 
            {
                m_TestManager = value;
            }
        }

        public cLoadObjects LoadObjects
        {
            get { return m_LoadObjects; }
            set 
            {
                m_LoadObjects = value;
                m_TestManager.Add("LoadObjects");

            }
        }

        public cRemoveObjects RemoveObjects
        {
            get { return m_RemoveObjects; }
            set 
            { 
                m_RemoveObjects = value;
                m_TestManager.Add("RemoveObjects");

            }
        }

        public cStartObjcetsAnimation StartObjcetsAnimation
        {
            get { return m_StartObjcetsAnimation; }
            set 
            { 
                m_StartObjcetsAnimation = value;
                m_TestManager.Add("StartObjcetsAnimation");
            }
        }

        public cStopObjcetsAnimation StopObjcetsAnimation
        {
            get { return m_StopObjcetsAnimation; }
            set
            { 
                m_StopObjcetsAnimation = value;
                m_TestManager.Add("StopObjcetsAnimation");
            }
        }

        public cMapZoom MapZoom
        {
            get { return m_MapZoom; }
            set 
            {
                m_MapZoom = value;
                m_TestManager.Add("MapZoom");
            }
        }
    }
    #endregion

    #region cGeneralDefinitions
    [Serializable]
    public class cGeneralDefinitions
    {
        private int m_GapBetweenActions;
        private int m_TestIterationsNumber;

        public cGeneralDefinitions()
        {
        }

        public int GapBetweenActions
        {
            get { return m_GapBetweenActions; }
            set { m_GapBetweenActions = value; }
        }

        public int TestIterationsNumber
        {
            get { return m_TestIterationsNumber; }
            set { m_TestIterationsNumber = value; }
        }
    }
    #endregion

    #region cLoadObjects
    [Serializable]
    public class cLoadObjects
    {
        private cLoadingArea m_BoxLoadingArea;
        private cDelays m_ObjectDelays;
        private List<cObjectsParams> m_lcLoadedObjectsParams;

        public cLoadObjects()
        {
//            MCTStressTest.TestManager.Add("LoadObjects");
        }

        public cLoadingArea BoxLoadingArea
        {
            get { return m_BoxLoadingArea; }
            set { m_BoxLoadingArea = value; }
        }

        public cDelays ObjectDelays
        {
            get { return m_ObjectDelays; }
            set { m_ObjectDelays = value; }
        }

        public List<cObjectsParams> LoadedObjectsParams
        {
            get { return m_lcLoadedObjectsParams; }
            set { m_lcLoadedObjectsParams = value; }
        }

    }
    #endregion

    #region cObjectsParams
    [Serializable]
    public class cObjectsParams
    {
        private string m_ObjectPath;
        private int m_AmountObjects;

        public cObjectsParams()
        {
        }

        public string ObjectPath
        {
            get { return m_ObjectPath; }
            set { m_ObjectPath = value; }
        }

        public int AmountObjects
        {
            get { return m_AmountObjects; }
            set { m_AmountObjects = value; }
        }
    }
    #endregion

    #region cRemoveObjects
    [Serializable]
    public class cRemoveObjects
    {
        private int m_AmountToRemove;
        
        public cRemoveObjects()
        {
            //MCTStressTest.TestManager.Add("RemoveObjects");
        }

        public int AmountToRemove
        {
            get { return m_AmountToRemove; }
            set { m_AmountToRemove = value; }
        }

    }
    #endregion

    #region cStartObjcetsAnimation
    [Serializable]
    public class cStartObjcetsAnimation
    {
        private bool m_Loop;
        private bool m_AutomaticRotation;
        private float m_StartingTimePoint;
        private DNEPositionInterpolationMode m_PositionInterpulationMode;
        private DNERotationInterpolationMode m_RotationInterpulationMode;
        private List<cPathAnimationNode> m_lPathAnimationNode;
        private float m_RotationAdditionalYaw;
        private int m_NumObjectToAnimate;

        public cStartObjcetsAnimation()
        {
            //MCTStressTest.TestManager.Add("StartObjcetsAnimation");
        }

        public bool Loop
        {
            get { return m_Loop; }
            set { m_Loop = value; }
        }

        public bool AutomaticRotation
        {
            get { return m_AutomaticRotation; }
            set { m_AutomaticRotation = value; }
        }

        public float StartingTimePoint
        {
            get { return m_StartingTimePoint; }
            set { m_StartingTimePoint = value; }
        }

        public float RotationAdditionalYaw
        {
            get { return m_RotationAdditionalYaw; }
            set { m_RotationAdditionalYaw = value; }
        }

        public DNEPositionInterpolationMode PositionInterpulationMode
        {
            get { return m_PositionInterpulationMode; }
            set { m_PositionInterpulationMode = value; }
        }

        public DNERotationInterpolationMode RotationInterpulationMode
        {
            get { return m_RotationInterpulationMode; }
            set { m_RotationInterpulationMode = value; }
        }

        public List<cPathAnimationNode> lPathAnimationNode
        {
            get { return m_lPathAnimationNode; }
            set { m_lPathAnimationNode = value; }
        }

        public int NumObjectToAnimate
        {
            get { return m_NumObjectToAnimate; }
            set { m_NumObjectToAnimate = value; }
        }
    }
    #endregion

    #region cStopObjcetsAnimation
    [Serializable]
    public class cStopObjcetsAnimation
    {
        private int m_NumObjectToStop;

        public cStopObjcetsAnimation()
        {
            //MCTStressTest.TestManager.Add("StopObjcetsAnimation");
        }

        public int NumObjectToStop
        {
            get { return m_NumObjectToStop; }
            set { m_NumObjectToStop = value; }
        }
    }
    #endregion

    #region cMapZoom
    [Serializable]
    public class cMapZoom
    {
        private float m_ZoomFactor;
        private int m_ZoomIterations;

        public cMapZoom()
        {
            
        }

        public float ZoomFactor
        {
            get { return m_ZoomFactor; }
            set { m_ZoomFactor = value; }
        }

        public int ZoomIterations
        {
            get { return m_ZoomIterations; }
            set { m_ZoomIterations = value; }
        }
    }
    
    #endregion

    #region cPathAnimationNode
    [Serializable]
    public class cPathAnimationNode
    {
        private double m_PointX;
        private double m_PointY;
        private double m_PointZ;
        private float m_Time;
        private float m_Yaw;
        private float m_Pitch;
        private float m_Roll;
        private bool m_RelativeToCurrentOrientation;

        public cPathAnimationNode()
        {

        }

        public double PointX
        {
            get { return m_PointX; }
            set { m_PointX = value; }
        }

        public double PointY
        {
            get { return m_PointY; }
            set { m_PointY = value; }
        }

        public double PointZ
        {
            get { return m_PointZ; }
            set { m_PointZ = value; }
        }

        public float Time
        {
            get { return m_Time; }
            set { m_Time = value; }
        }

        public float Yaw
        {
            get { return m_Yaw; }
            set { m_Yaw = value; }
        }

        public float Pitch
        {
            get { return m_Pitch; }
            set { m_Pitch = value; }
        }

        public float Roll
        {
            get { return m_Roll; }
            set { m_Roll = value; }
        }

        public bool RelativeToCurrentOrientation
        {
            get { return m_RelativeToCurrentOrientation; }
            set { m_RelativeToCurrentOrientation = value; }
        }

    }
    #endregion

    #region LoadingArea
    [Serializable]
    public class cLoadingArea
    {
        private double m_MinX;
        private double m_MinY;
        private double m_MinZ;
        private double m_MaxX;
        private double m_MaxY;
        private double m_MaxZ;

        public double MinX
        {
            get { return m_MinX; }
            set { m_MinX = value; }
        }

        public double MinY
        {
            get { return m_MinY; }
            set { m_MinY = value; }
        }

        public double MinZ
        {
            get { return m_MinZ; }
            set { m_MinZ = value; }
        }

        public double MaxX
        {
            get { return m_MaxX; }
            set { m_MaxX = value; }
        }

        public double MaxY
        {
            get { return m_MaxY; }
            set { m_MaxY = value; }
        }

        public double MaxZ
        {
            get { return m_MaxZ; }
            set { m_MaxZ = value; }
        }

        [XmlIgnore]
        public DNSMcBox ObjectLoadingArea
        {
            get
            {
                DNSMcBox box = new DNSMcBox(MinX, MinY, MinZ, MaxX, MaxY, MaxZ);
                return box;
            }
        }
    }
    #endregion

    #region Delays
    [Serializable]
    public class cDelays
    {
        private uint m_ObjectConditionNumToUpdate = 0;
        private uint m_ObjectUpdateNumToUpdate = 0;

        public uint ObjectConditionNumToUpdate
        {
            get { return m_ObjectConditionNumToUpdate; }
            set { m_ObjectConditionNumToUpdate = value; }
        }

        public uint ObjectUpdateNumToUpdate
        {
            get { return m_ObjectUpdateNumToUpdate; }
            set { m_ObjectUpdateNumToUpdate = value; }
        }
    }
    #endregion
}
