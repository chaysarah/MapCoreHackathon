using MapCore;
using MCTester.GUI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTester.ObjectWorld.Scan
{
    public interface IGetScanExtendedDataCallback
    {
        void GetScanExtendedData(DNSVectorItemFound[] VectorItems, DNSMcVector3D[] unifiedVectorItemsPoints, DNSTargetFound itemFound, IDNMcOverlay overlay, ScanTargetFound scanTargetFound);
        void GetVectorItemFieldValueAsWString(object pValue, int index);
    }

    
}
