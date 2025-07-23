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
    public class MCTPerformanceTest
    {
        private OpenMap m_OpenMapTest;
        private LoadObjects m_LoadObjectsTest;
        private ObjectsParams m_ObjectsParamsTest;
        private MapZoom m_MapZoomTest;
        private MapRotate m_MapRotateTest;
        private MapMove m_MapMoveTest;
        public static List<string> TestManager;
        public static int LocationInQueue;

        public MCTPerformanceTest()
        {
            /*
            m_OpenMapTest = new OpenMap();
            m_LoadObjectsTest = new LoadObjects();
            m_MapZoomTest = new MapZoom();
            m_MapRotateTest = new MapRotate();
            m_MapMoveTest = new MapMove();
            */

            TestManager = new List<string>();
            LocationInQueue = 0;
        }

        #region Public Properties
        public OpenMap OpenMapTest
        {
            get { return m_OpenMapTest; }
            set { m_OpenMapTest = value; }
        }

        public LoadObjects LoadObjectsTest
        {
            get { return m_LoadObjectsTest; }
            set { m_LoadObjectsTest = value; }
        }

        public ObjectsParams ObjectsParamsTest
        {
            get { return m_ObjectsParamsTest; }
            set { m_ObjectsParamsTest = value; }
        }

        public MapZoom MapZoomTest
        {
            get { return m_MapZoomTest; }
            set {m_MapZoomTest = value;}
        }

        public MapRotate MapRotateTest
        {
            get { return m_MapRotateTest; }
            set { m_MapRotateTest = value; }
        }

        public MapMove MapMoveTest
        {
            get { return m_MapMoveTest; }
            set { m_MapMoveTest = value; }
        }
        #endregion
    }

    #region OpenMap
    [Serializable]
    public class OpenMap
    {
        private string m_MapType;
        private string m_MapTerrainPath;
        private string m_LoggingLevel;
        private uint m_TerrainNumCacheTiles;
        private GridCoordinateSystem m_MapGridCoordinateSystem;
        private OpeningPosition m_MapOpeningPosition;
        private OpeningOrientation m_CameraOpeningOrientation;
        private float m_ObjectsVisibilityMaxScale = float.MaxValue;

        public OpenMap()
        {
            MCTPerformanceTest.TestManager.Add("OpenMap");
        }

        public string MapTerrainPath
        {
            get { return m_MapTerrainPath; }
            set { m_MapTerrainPath = value; }
        }

        public string MapType
        {
            get { return m_MapType; }
            set { m_MapType = value; }
        }

        public float ObjectsVisibilityMaxScale
        {
            get { return m_ObjectsVisibilityMaxScale; }
            set { m_ObjectsVisibilityMaxScale = value; }
        }

        public GridCoordinateSystem MapGridCoordinateSystem
        {
            get { return m_MapGridCoordinateSystem; }
            set { m_MapGridCoordinateSystem = value; }
        }

        public OpeningPosition MapCameraOpeningPosition
        {
            get { return m_MapOpeningPosition; }
            set { m_MapOpeningPosition = value; }
        }

        public OpeningOrientation MapCameraOpeningOrientation
        {
            get { return m_CameraOpeningOrientation; }
            set { m_CameraOpeningOrientation = value; }
        }

        public string LoggingLevel
        {
            get { return m_LoggingLevel; }
            set { m_LoggingLevel = value; }
        }

        public uint TerrainNumCacheTiles
        {
            get { return m_TerrainNumCacheTiles; }
            set { m_TerrainNumCacheTiles = value; }
        }

        [XmlIgnore]
        public DNELoggingLevel eLoggingLevel
        {
            get
            {
                return (DNELoggingLevel)Enum.Parse(typeof(DNELoggingLevel), LoggingLevel);
            }
        }

        [XmlIgnore]
        public DNEMapType eMapType
        {
            get
            {
                return (DNEMapType)Enum.Parse(typeof(DNEMapType), MapType);
            }
        }
    }
    #endregion

    #region GridCoordinateSystem
    [Serializable]
    public class GridCoordinateSystem
    {
        private string m_GridCoordinateSystemType;
        private string m_DatumType;
        private int m_Zone;

        public string GridCoordinateSystemType
        {
            get { return m_GridCoordinateSystemType; }
            set { m_GridCoordinateSystemType = value; }
        }

        public string DatumType
        {
            get { return m_DatumType; }
            set { m_DatumType = value; }
        }

        public int Zone
        {
            get { return m_Zone; }
            set { m_Zone = value; }
        }

        [XmlIgnore]
        public IDNMcGridCoordinateSystem GridCoordSystem
        {
            get
            {
                IDNMcGridCoordinateSystem GridCoordinateSystem = null;

                DNEGridCoordSystemType GridType = (DNEGridCoordSystemType)Enum.Parse(typeof(DNEGridCoordSystemType), GridCoordinateSystemType);
                DNEDatumType Datum = (DNEDatumType)Enum.Parse(typeof(DNEDatumType), DatumType);

                switch (GridType)
                {
                    case DNEGridCoordSystemType._EGCS_GEOGRAPHIC:
                        try
                        {
                            GridCoordinateSystem = DNMcGridCoordSystemGeographic.Create(Datum);
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("DNMcGridCoordSystemGeographic.Creat", McEx);
                        }
                        break;
                    case DNEGridCoordSystemType._EGCS_GEOCENTRIC:
                        try
                        {
                            GridCoordinateSystem = DNMcGridCoordSystemGeocentric.Create(Datum);
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("DNMcGridCoordSystemGeocentric.Create", McEx);
                        }
                        break;
                    case DNEGridCoordSystemType._EGCS_UTM:
                        try
                        {
                            GridCoordinateSystem = DNMcGridUTM.Create(Zone, Datum);
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("DNMcGridUTM.Create", McEx);
                        }
                        break;
                    case DNEGridCoordSystemType._EGCS_NEW_ISRAEL:
                        try
                        {
                            GridCoordinateSystem = DNMcGridNewIsrael.Create();
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("DNMcGridNewIsrael.Create", McEx);
                        }
                        break;
                    case DNEGridCoordSystemType._EGCS_S42:
                        try
                        {
                            GridCoordinateSystem = DNMcGridS42.Create(Zone, Datum);
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("DNMcGridS42.Create", McEx);
                        }
                        break;
                    case DNEGridCoordSystemType._EGCS_RT90:
                        try
                        {
                            GridCoordinateSystem = DNMcGridRT90.Create();
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("DNMcGridRT90.Create", McEx);
                        }
                        break;
                    case DNEGridCoordSystemType._EGCS_TM_USER_DEFINED:
                    case DNEGridCoordSystemType._EGCS_KKJ:
                    case DNEGridCoordSystemType._EGCS_RSO_SINGAPORE:
                    case DNEGridCoordSystemType._EGCS_INDIA_LCC:
                        break;
                }

                if (GridCoordinateSystem == null)
                    MessageBox.Show("Map coordinate system failed to create", "Coordinate System Create Failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                return GridCoordinateSystem;
            }
        }
    }
    #endregion

    #region OpeningPosition
    [Serializable]
    public class OpeningPosition
    {
        private double m_X;
        private double m_Y;
        private double m_Z;

        public double X
        {
            get { return m_X; }
            set { m_X = value; }
        }

        public double Y
        {
            get { return m_Y; }
            set { m_Y = value; }
        }

        public double Z
        {
            get { return m_Z; }
            set { m_Z = value; }
        }

        [XmlIgnore]
        public DNSMcVector3D Position
        {
            get
            {
                return new DNSMcVector3D(X, Y, Z);
            }
        }
    }
    #endregion

    #region OpeningOrientation
    [Serializable]
    public class OpeningOrientation
    {
        private float m_Yaw;
        private float m_Pitch;
        private float m_Roll;

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

        [XmlIgnore]
        public DNSMcFVector3D Orientation
        {
            get
            {
                return new DNSMcFVector3D(Yaw, Pitch, Roll);
            }
        }
    }
    #endregion

    #region LoadObjects
    [Serializable]
    public class LoadObjects
    {
        private LoadingArea m_BoxLoadingArea;
        private Delays m_ObjectDelays;
        private List<ObjectsParams> m_lLoadedObjectsParams;

        public LoadObjects()
        {
            MCTPerformanceTest.TestManager.Add("LoadObjects");
        }

        public LoadingArea BoxLoadingArea
        {
            get { return m_BoxLoadingArea; }
            set { m_BoxLoadingArea = value; }
        }

        public Delays ObjectDelays
        {
            get { return m_ObjectDelays; }
            set { m_ObjectDelays = value; }
        }

        public List<ObjectsParams> LoadedObjectsParams
        {
            get { return m_lLoadedObjectsParams; }
            set { m_lLoadedObjectsParams = value; }
        }

    }
    #endregion

    #region ObjectsParams
    [Serializable]
    public class ObjectsParams
    {
        private string m_ObjectPath;
        private int m_AmountObjects;

        public ObjectsParams()
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

    #region LoadingArea
    [Serializable]
    public class LoadingArea
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
    public class Delays
    {
        private uint m_ObjectConditionNumToUpdate = 0;
        private uint m_ObjectUpdateNumToUpdate = 0;
        private uint m_HiddenObjectCollisionNumToCheck = 0; // TODO: rename ObjectAddition to HiddenObjectCollision in tester!

        public uint ObjectConditionNumToUpdate
        {
            get {return m_ObjectConditionNumToUpdate;}
            set {m_ObjectConditionNumToUpdate = value;}
        }

        public uint ObjectUpdateNumToUpdate
        {
            get {return m_ObjectUpdateNumToUpdate;}
            set {m_ObjectUpdateNumToUpdate = value;}
        }

        public uint HiddenObjectCollisionNumToCheck
        {
            get { return m_HiddenObjectCollisionNumToCheck; }
            set { m_HiddenObjectCollisionNumToCheck = value; }
        }
    }
    #endregion

    #region MapZoom
    [Serializable]
    public class MapZoom
    {
        private float m_Factor;
        private int m_NumIterations;

        public MapZoom()
        {
            MCTPerformanceTest.TestManager.Add("MapZoom");
        }

        public float Factor
        {
            get { return m_Factor; }
            set { m_Factor = value; }
        }

        public int NumIterations
        {
            get { return m_NumIterations; }
            set { m_NumIterations = value; }
        }
    }
    #endregion

    #region MapRotate
    public class MapRotate
    {
        private float m_Delta;
        private int m_NumIterations;

        public MapRotate()
        {
            MCTPerformanceTest.TestManager.Add("MapRotate");
        }

        public float Delta
        {
            get { return m_Delta; }
            set { m_Delta = value; }
        }

        public int NumIterations
        {
            get { return m_NumIterations; }
            set { m_NumIterations = value; }
        }
    }
    #endregion

    #region MapMove
    [Serializable]
    public class MapMove
    {
        private int m_Delta;
        private int m_NumIterations;

        public MapMove()
        {
            MCTPerformanceTest.TestManager.Add("MapMove");
        }

        public int Delta
        {
            get { return m_Delta; }
            set { m_Delta = value; }
        }

        public int NumIterations
        {
            get { return m_NumIterations; }
            set { m_NumIterations = value; }
        }
    }
    #endregion
}