using MapCore;
using NavCore.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTester.Managers
{
    public static class Manager_NavCore
    {
        private static string m_NavCoreFolderPath = "";
        // private static NavCoreBaseWrapper m_NavCoreBaseWrapper;
        private static string m_TraversabilityFolderPath = "";
        private static byte m_TraversabilityTransparency = 50;
        private static DNSMcBox m_BoundingBox = new DNSMcBox();
        private static IDNMcGridCoordinateSystem m_GridCoordinateSystem;
        private static bool m_IsCreateStandaloneSpatialQueries = false;
        private static float m_SoilCost = 1;
        private static float m_RoadsCost = 1;
        private static float m_LimitedPassabilityAreasCost = 1;
        private static float m_VeryLimitedPassabilityAreasCost = 1;
        private static float m_LineWidth = 6;

        public static float SoilCost
        {
            get { return m_SoilCost; }
            set { m_SoilCost = value; }
        }

        public static float RoadsCost
        {
            get { return m_RoadsCost; }
            set { m_RoadsCost = value; }
        }

        public static float LimitedPassabilityAreasCost
        {
            get { return m_LimitedPassabilityAreasCost; }
            set { m_LimitedPassabilityAreasCost = value; }
        }

        public static float VeryLimitedPassabilityAreasCost
        {
            get { return m_VeryLimitedPassabilityAreasCost; }
            set { m_VeryLimitedPassabilityAreasCost = value; }
        }

        public static byte TraversabilityTransparency
        {
            get { return m_TraversabilityTransparency; }
            set { m_TraversabilityTransparency = value; }
        }

        public static float LineWidth
        {
            get { return m_LineWidth; }
            set { m_LineWidth = value; }
        }

        public static string NavCoreFolderPath
        {
            get { return m_NavCoreFolderPath; }
            set { m_NavCoreFolderPath = value; }
        }

        public static string TraversabilityFolderPath
        {
            get { return m_TraversabilityFolderPath; }
            set { m_TraversabilityFolderPath = value; }
        }

        public static DNSMcBox BoundingBox
        {
            get { return m_BoundingBox; }
            set { m_BoundingBox = value; }
        }

        public static IDNMcGridCoordinateSystem GridCoordinateSystem
        {
            get { return m_GridCoordinateSystem; }
            set { m_GridCoordinateSystem = value; }
        }
        public static bool IsCreateStandaloneSpatialQueries
        {
            get { return m_IsCreateStandaloneSpatialQueries; }
            set { m_IsCreateStandaloneSpatialQueries = value; }
        }

        static Manager_NavCore()
        {

        }
    }
}
