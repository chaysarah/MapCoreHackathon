using MapCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTester.Managers.MapWorld
{
    public interface IOnMapClick
    {

        //void OnMapClick(DNSMcVector3D screenLocation, DNSMcVector3D location, DNEMcPointCoordSystem locationCoordSystem);
        void OnMapClick(DNSMcVector3D screenLocation, DNSMcVector3D location, DNEMcPointCoordSystem locationCoordSystem, bool isRelativeToDTM);

        void OnMapClickError(DNEMcErrorCode mcErrorCode, string functionName);
    }
}
