using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;
using System.Drawing;
using MCTester.GUI.Forms;

namespace MCTester.ObjectWorld
{
    public class ScanPointGeometry : MCTester.ObjectWorld.ScanGeometryBase
    {
        DNSMcScanPointGeometry m_ScanPointGeometry;
        IDNMcEditMode m_EditMode;
        Point m_SelectedPoint;
        DNSMcVector3D m_PointItem;
        private DNSQueryParams m_SpatialQueryParams;
        bool m_CompletelyInside;
        float m_Tolerance;
        DNEMcPointCoordSystem mScanCoordSys;
        private DNSMcVector3D m_ManualPoint;

        public ScanPointGeometry(DNEMcPointCoordSystem coordSys, DNSQueryParams SQParams, bool CompletelyInsideOnly, float Tolerance, DNSMcVector3D point)
        {
            m_CompletelyInside = CompletelyInsideOnly;
            m_SpatialQueryParams = SQParams;
            m_EditMode = MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.EditMode;
            m_Tolerance = Tolerance;
            mScanCoordSys = coordSys;
            m_ManualPoint = point;
        }

        public void StartPointScan()
        {
            try
            {
                MCTester.GUI.Map.MCTMapForm.OnMapClicked += new MCTester.GUI.Map.ClickOnMapEventArgs(MCTMapForm_OnMapClicked);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("RectScan", McEx);
            }
        }

        public void StartManualPointScan()
        {

            m_ScanPointGeometry = new DNSMcScanPointGeometry(mScanCoordSys, m_ManualPoint, m_Tolerance);
            DNSTargetFound[] TargetFound = null;
            try
            {
                TargetFound = CurrViewport.ScanInGeometry(m_ScanPointGeometry, m_CompletelyInside, m_SpatialQueryParams);
                ShowScanResult(m_ScanPointGeometry, TargetFound);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("ScanInGeometry", McEx);
            }
        }

        void MCTMapForm_OnMapClicked(System.Drawing.Point PointOnMap, DNSMcVector3D PointIn3D, DNSMcVector3D PointInImage, bool IsHasIntersection)
        {
            if (PointIn3D != null)
            {
                m_SelectedPoint = PointOnMap;

                if (mScanCoordSys == DNEMcPointCoordSystem._EPCS_SCREEN)
                    btnScanForm.ScanFormParams.SpecificPoint = new DNSMcVector3D(PointOnMap.X, PointOnMap.Y, 0);
                else if (mScanCoordSys == DNEMcPointCoordSystem._EPCS_WORLD)
                    btnScanForm.ScanFormParams.SpecificPoint = PointIn3D;
                else // Image
                    btnScanForm.ScanFormParams.SpecificPoint = PointInImage;

                m_PointItem = new DNSMcVector3D((Double)m_SelectedPoint.X, (Double)m_SelectedPoint.Y,0);

                MCTester.GUI.Map.MCTMapForm.OnMapClicked -= new MCTester.GUI.Map.ClickOnMapEventArgs(MCTMapForm_OnMapClicked);
                m_ScanPointGeometry = new DNSMcScanPointGeometry(mScanCoordSys, m_PointItem, m_Tolerance);
            }

            DNSTargetFound[] TargetFound = null;
            try
            {
                TargetFound = CurrViewport.ScanInGeometry(m_ScanPointGeometry, m_CompletelyInside, m_SpatialQueryParams);
                ShowScanResult(m_ScanPointGeometry, TargetFound);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("ScanInGeometry", McEx);
            }
            
        }

    }
}
