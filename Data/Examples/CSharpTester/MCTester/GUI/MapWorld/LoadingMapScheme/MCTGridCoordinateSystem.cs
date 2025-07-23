using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.MapWorld.LoadingMapScheme
{
    [Serializable]
    public class MCTGridCoordinateSystem
    {
        private IDNMcGridCoordinateSystem m_GridCoordinateSystems;
        private DNEGridCoordSystemType m_GridCoordinateSystemType;
        private DNEDatumType m_DatumType;
        private int m_Zone;
        private int m_GridCoordSysID;
        private DNSDatumParams m_DatumParams;
        private DNSTMGridParams m_TMGridParams;
        private bool m_IsSRID;
        private string[] m_AStrCreateParams;

        public MCTGridCoordinateSystem()
        {
            m_GridCoordinateSystems = null;
        }

        public IDNMcGridCoordinateSystem GetGridCoordSys()
        {
           // if (m_GridCoordinateSystems == null)
          //  {
                switch (GridCoordinateSystemType)
                {
                    case DNEGridCoordSystemType._EGCS_GEOCENTRIC:
                        m_GridCoordinateSystems = DNMcGridCoordSystemGeocentric.Create(DatumType);
                        break;
                    case DNEGridCoordSystemType._EGCS_GEOGRAPHIC:
                        m_GridCoordinateSystems = DNMcGridCoordSystemGeographic.Create(DatumType);
                        break;
                    case DNEGridCoordSystemType._EGCS_UTM:
                        m_GridCoordinateSystems = DNMcGridUTM.Create(Zone, DatumType);
                        break;
                    case DNEGridCoordSystemType._EGCS_NEW_ISRAEL:
                        m_GridCoordinateSystems = DNMcGridNewIsrael.Create();
                        break;
                    case DNEGridCoordSystemType._EGCS_RT90:
                        m_GridCoordinateSystems = DNMcGridRT90.Create();
                        break;
                    case DNEGridCoordSystemType._EGCS_S42:
                        m_GridCoordinateSystems = DNMcGridS42.Create(Zone, DatumType);
                        break;
                    case DNEGridCoordSystemType._EGCS_GENERIC_GRID:
                        if(IsSRID && AStrCreateParams!= null && AStrCreateParams.Length > 0)
                            m_GridCoordinateSystems = DNMcGridGeneric.Create(AStrCreateParams[0], IsSRID);
                        else
                            m_GridCoordinateSystems = DNMcGridGeneric.Create(AStrCreateParams);
                        break;
                    case DNEGridCoordSystemType._EGCS_TM_USER_DEFINED:
                    case DNEGridCoordSystemType._EGCS_INDIA_LCC:
                    case DNEGridCoordSystemType._EGCS_KKJ:
                    case DNEGridCoordSystemType._EGCS_RSO_SINGAPORE:
                        break;
                }

                Managers.MapWorld.Manager_MCGridCoordinateSystem.AddNewGridCoordinateSystem(m_GridCoordinateSystems);
                return m_GridCoordinateSystems;
          //  }
         //   else
         //       return m_GridCoordinateSystems;            
        }

        public DNEGridCoordSystemType GridCoordinateSystemType
        {
            get { return m_GridCoordinateSystemType; }
            set { m_GridCoordinateSystemType = value; }
        }

        public DNEDatumType DatumType
        {
            get { return m_DatumType; }
            set { m_DatumType = value; }
        }

        public int Zone
        {
            get { return m_Zone; }
            set { m_Zone = value; }
        }

        public int ID
        {
            get { return m_GridCoordSysID; }
            set { m_GridCoordSysID = value; }
        }

        public DNSDatumParams DatumParams
        {
            get { return m_DatumParams; }
            set { m_DatumParams = value; }
        }

        public DNSTMGridParams TMGridParams
        {
            get { return m_TMGridParams; }
            set { m_TMGridParams = value; }
        }

        public bool IsSRID
        {
            get { return m_IsSRID; }
            set { m_IsSRID = value; }
        }

        public string[] AStrCreateParams
        {
            get { return m_AStrCreateParams; }
            set { m_AStrCreateParams = value; }
        }

        public static MCTGridCoordinateSystem CreateMCTGridCoordinateSystem(IDNMcGridCoordinateSystem gridCoordinateSystem)
        {
            MCTGridCoordinateSystem currGridCoorSys = new MCTGridCoordinateSystem();
            currGridCoorSys.ID = MainForm.MapLoaderDefinitionManager.GridCoordynateSystems.GetNextID();
            currGridCoorSys.GridCoordinateSystemType = gridCoordinateSystem.GetGridCoorSysType();

            if (currGridCoorSys.GridCoordinateSystemType != DNEGridCoordSystemType._EGCS_GENERIC_GRID)
            {
                currGridCoorSys.DatumType = gridCoordinateSystem.GetDatum();
                currGridCoorSys.Zone = gridCoordinateSystem.GetZone();
                currGridCoorSys.DatumParams = gridCoordinateSystem.GetDatumParams();
                if (gridCoordinateSystem is IDNMcGridCoordSystemTraverseMercator)
                    currGridCoorSys.TMGridParams = (gridCoordinateSystem as IDNMcGridCoordSystemTraverseMercator).GetTMParams();
            }
            else if (gridCoordinateSystem is IDNMcGridGeneric)
            {
                IDNMcGridGeneric mcGridGeneric = (IDNMcGridGeneric)gridCoordinateSystem;
                string[] pastrCreateParams;
                bool isSRID;
                mcGridGeneric.GetCreateParams(out pastrCreateParams, out isSRID);

                currGridCoorSys.IsSRID = isSRID;
                currGridCoorSys.AStrCreateParams = pastrCreateParams;
            }
            MainForm.MapLoaderDefinitionManager.GridCoordynateSystems.CoordinateSystem.Add(currGridCoorSys);
            return currGridCoorSys;
        }
    }
}
