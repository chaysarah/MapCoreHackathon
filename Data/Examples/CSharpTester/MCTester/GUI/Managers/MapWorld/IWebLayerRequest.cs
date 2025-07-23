using MapCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTester.Managers.MapWorld
{
    public interface IWebLayerRequest
    {
        void GetWebServerLayers(DNEMcErrorCode eStatus, string strServerURL, DNEWebMapServiceType eWebMapServiceType,
            DNSServerLayerInfo[] aLayers, string[] astrServiceMetadataURLs, string UrlServer, string strServiceProviderName);

    }
}
