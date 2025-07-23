using MapCore;
using MCTester.GUI.Forms;
using MCTester.MapWorld.MapUserControls;
using MCTester.MapWorld.WebMapLayers;
using MCTester.ObjectWorld;
using MCTester.ObjectWorld.Scan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnmanagedWrapper;

namespace MCTester.Managers.MapWorld
{
    class MCTAsyncOperationCallback : IDNMapLayerAsyncOperationCallback
    {
        private static MCTAsyncOperationCallback mInstance;

        public ucVectorLayer vectorLayerForm;

        // for OnScanExtendedDataResult
        public IDNMcOverlay mcOverlay;
        public IGetScanExtendedDataCallback scanExtendedDataCallback;
        public DNSTargetFound itemFound;
        public ScanTargetFound scanTargetFound;
        public int index = -1;

        public static MCTAsyncOperationCallback GetInstance()
        {
            if (mInstance == null)
                mInstance = new MCTAsyncOperationCallback();
            return mInstance;
        }

        public void OnScanExtendedDataResult(IDNMcMapLayer pLayer, DNEMcErrorCode eStatus, DNSVectorItemFound[] aVectorItems, DNSMcVector3D[] aUnifiedVectorItemsPoints)
        {
            if (eStatus == DNEMcErrorCode.SUCCESS)
            {
                scanExtendedDataCallback.GetScanExtendedData(aVectorItems, aUnifiedVectorItemsPoints, itemFound, mcOverlay, scanTargetFound);
            }
            else
                MessageBox.Show(IDNMcErrors.ErrorCodeToString(eStatus), "On Scan Extended Data Result", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        public void OnVectorItemPointsResult(IDNMcMapLayer pLayer, DNEMcErrorCode eStatus, DNSMcVector3D[][] aaPoints)
        {
            if (eStatus == DNEMcErrorCode.SUCCESS)
            {
                vectorLayerForm.GetVectorPoints(aaPoints, (IDNMcVectorMapLayer)pLayer);
            }
            else
                MessageBox.Show(IDNMcErrors.ErrorCodeToString(eStatus), "On Vector Item Points Result Callback", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        public void OnFieldUniqueValuesResult(IDNMcMapLayer pLayer, DNEMcErrorCode eStatus, DNEVectorFieldReturnedType eReturnedType, object paUniqueValues)
        {
            if (eStatus == DNEMcErrorCode.SUCCESS)
            {
                if (eReturnedType == DNEVectorFieldReturnedType._EVFRT_INT && paUniqueValues != null)
                    vectorLayerForm.GetFieldUniqueValuesAsInt((int[])paUniqueValues);
                if (eReturnedType == DNEVectorFieldReturnedType._EVFRT_DOUBLE && paUniqueValues != null)
                    vectorLayerForm.GetFieldUniqueValuesAsDouble((double[])paUniqueValues);
                if ((eReturnedType == DNEVectorFieldReturnedType._EVFRT_STRING || eReturnedType == DNEVectorFieldReturnedType._EVFRT_WSTRING) && paUniqueValues != null)
                    vectorLayerForm.GetFieldUniqueValuesAsString((string[])paUniqueValues);
            }
            else
                MessageBox.Show(IDNMcErrors.ErrorCodeToString(eStatus), "Field Unique Values Result Callback", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        public void OnVectorItemFieldValueResult(IDNMcMapLayer pLayer, DNEMcErrorCode eStatus, DNEVectorFieldReturnedType eReturnedType, object pValue)
        {
            if (eStatus == DNEMcErrorCode.SUCCESS)
            {
                if (vectorLayerForm != null)
                {
                    if (eReturnedType == DNEVectorFieldReturnedType._EVFRT_INT && pValue != null)
                        vectorLayerForm.GetVectorItemFieldValuesAsInt((int)pValue);
                    if (eReturnedType == DNEVectorFieldReturnedType._EVFRT_DOUBLE && pValue != null)
                        vectorLayerForm.GetVectorItemFieldValuesAsDouble((double)pValue);
                    if ((eReturnedType == DNEVectorFieldReturnedType._EVFRT_STRING || eReturnedType == DNEVectorFieldReturnedType._EVFRT_WSTRING) && pValue != null)
                        vectorLayerForm.GetVectorItemFieldValuesAsString((string)pValue);
                }
                else if(scanExtendedDataCallback != null)
                {
                    scanExtendedDataCallback.GetVectorItemFieldValueAsWString(pValue, index);
                }
            }
            else
                MessageBox.Show(IDNMcErrors.ErrorCodeToString(eStatus), "Vector Item Field Value Result Callback", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        public void OnVectorQueryResult(IDNMcMapLayer pLayer, DNEMcErrorCode eStatus, UInt64[] auVectorItemsID)
        {
            if (eStatus == DNEMcErrorCode.SUCCESS)
            {
                vectorLayerForm.GetQuery(auVectorItemsID);
            }
            else
                MessageBox.Show(IDNMcErrors.ErrorCodeToString(eStatus), "On Vector Query Result Callback", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void OnWebServerLayersResults(DNEMcErrorCode eStatus, string strServerURL, DNEWebMapServiceType eWebMapServiceType,
            DNSServerLayerInfo[] aLayers, string[] astrServiceMetadataURLs, string strServiceProviderName)
        {
            
        }

        public void On3DModelSmartRealityDataResults(DNEMcErrorCode eStatus, string strServerURL, DNSMcVariantID uObjectID, DNSSmartRealityBuildingHistory[] aBuildingHistory)
        {
            throw new NotImplementedException();
        }
    }
}
