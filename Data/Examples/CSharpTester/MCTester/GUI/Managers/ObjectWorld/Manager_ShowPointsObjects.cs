using MapCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTester.Managers.ObjectWorld
{
    public class MCTShowPointsLocations
    {
        public DNEMcPointCoordSystem mcPointCoordSystem;
        public DNSMcVector3D[] mcPoints;

        public MCTShowPointsLocations() { }
    }


    public static class Manager_ShowPointsObjects
    {
        static List<IDNMcObjectScheme> m_ShowPointsSchemes = new List<IDNMcObjectScheme>();

        public static void AddShowPointsSchemes(IDNMcObjectScheme scheme)
        {
            if (!CheckIfSchemeExistInShowPointsSchemes(scheme))
                m_ShowPointsSchemes.Add(scheme);
        }

        public static bool CheckIfSchemeExistInShowPointsSchemes(IDNMcObjectScheme scheme)
        {
            return m_ShowPointsSchemes.Contains(scheme);
        }

        public static void ClearShowCoordinatesSchemes()
        {
            foreach (IDNMcObjectScheme scheme in m_ShowPointsSchemes)
                scheme.Dispose();

            m_ShowPointsSchemes.Clear();
        }
    }
}
