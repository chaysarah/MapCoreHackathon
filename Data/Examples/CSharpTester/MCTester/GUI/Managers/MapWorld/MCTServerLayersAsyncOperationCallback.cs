using MapCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTester.Managers.MapWorld
{
    class MCTServerLayersAsyncOperationCallback : IDNMapLayerAsyncOperationCallback
    {

        public IWebLayerRequest FormWebLayerRequest;
        public string UrlServer;

        public MCTServerLayersAsyncOperationCallback(IWebLayerRequest formWebLayerRequest, string sUrlServer)
        {
            FormWebLayerRequest = formWebLayerRequest;
            UrlServer = sUrlServer;
        }

        public void OnScanExtendedDataResult(IDNMcMapLayer pLayer, DNEMcErrorCode eStatus, DNSVectorItemFound[] aVectorItems, DNSMcVector3D[] aUnifiedVectorItemsPoints)
        {
            throw new NotImplementedException();
        }

        public void OnVectorItemPointsResult(IDNMcMapLayer pLayer, DNEMcErrorCode eStatus, DNSMcVector3D[][] aaPoints)
        {
            throw new NotImplementedException();
        }

        public void OnFieldUniqueValuesResult(IDNMcMapLayer pLayer, DNEMcErrorCode eStatus, DNEVectorFieldReturnedType eReturnedType, object paUniqueValues)
        {
            throw new NotImplementedException();
        }

        public void OnVectorItemFieldValueResult(IDNMcMapLayer pLayer, DNEMcErrorCode eStatus, DNEVectorFieldReturnedType eReturnedType, object pValue)
        {
            throw new NotImplementedException();
        }

        public void OnVectorQueryResult(IDNMcMapLayer pLayer, DNEMcErrorCode eStatus, UInt64[] auVectorItemsID)
        {
            throw new NotImplementedException();
        }

        public void OnWebServerLayersResults(DNEMcErrorCode eStatus, string strServerURL, DNEWebMapServiceType eWebMapServiceType,
            DNSServerLayerInfo[] aLayers, string[] astrServiceMetadataURLs, string strServiceProviderName)
        {
            FormWebLayerRequest.GetWebServerLayers(eStatus, strServerURL, eWebMapServiceType, aLayers, astrServiceMetadataURLs, UrlServer, strServiceProviderName);
        }

        public void On3DModelSmartRealityDataResults(DNEMcErrorCode eStatus, string strServerURL, DNSMcVariantID uObjectID, DNSSmartRealityBuildingHistory[] aBuildingHistory)
        {
            throw new NotImplementedException();
        }
    }
}
