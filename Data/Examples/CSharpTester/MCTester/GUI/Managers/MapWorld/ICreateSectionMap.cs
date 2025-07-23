using MapCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTester.Managers.MapWorld
{
    public interface ICreateSectionMap
    {
        // void CreateSectionMap(bool isCameFromGetTerrainHeightsAlongLine, List<IDNMcMapTerrain> terrains, DNSMcVector3D[] sectionRoutePoints, DNSMcVector3D[] heightPoints);
        void CreateSectionMap(bool isCameFromGetTerrainHeightsAlongLine, DNSMcVector3D[] heightPoints);
    }
}
