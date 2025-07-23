using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using MCTester.Managers.ObjectWorld;
using UnmanagedWrapper;

namespace MCTester.ObjectWorld
{
    public class ScanPolygonGeometry : MCTester.ObjectWorld.ScanGeometryBase,IDNEditModeCallback
    {
        DNSMcScanPolygonGeometry m_ScanPolygonGeometry;
        IDNMcEditMode m_EditMode;
        private DNSQueryParams m_SpatialQueryParams;
        bool m_CompletelyInside;
        DNEMcPointCoordSystem mScanCoordSys;
        private IDNEditModeCallback m_Callback;

        public ScanPolygonGeometry(DNEMcPointCoordSystem coordSys, DNSQueryParams SQParams, bool CompletelyInsideOnly, float Tolerance)
        {
            m_CompletelyInside = CompletelyInsideOnly;
            m_SpatialQueryParams = SQParams;
            m_EditMode = MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.EditMode;
            mScanCoordSys = coordSys;
            m_Callback = null;
        }

        public void StartPolyScan()
        {
            try
            {
                IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;
                DNSMcVector3D[] locationPoints = new DNSMcVector3D[0];


                IDNMcObjectSchemeItem ObjSchemeItem = DNMcPolygonItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                DNELineStyle._ELS_SOLID,
                                                                                DNSMcBColor.bcBlackOpaque,
                                                                                2f,
                                                                                null,
																				new DNSMcFVector2D(0, -1),
                                                                                1f,
                                                                                DNEFillStyle._EFS_SOLID,
                                                                                new DNSMcBColor(0,255,100,100));
                    
                IDNMcObject obj = DNMcObject.Create(activeOverlay,
                                                    ObjSchemeItem,
                                                    DNEMcPointCoordSystem._EPCS_SCREEN,
                                                    locationPoints,
                                                    false);

                //In order to prevent retrieval of the polygon in the scan 
                ObjSchemeItem.Detectibility = false;
                ((IDNMcPolygonItem)ObjSchemeItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                ((IDNMcPolygonItem)ObjSchemeItem).SetDrawPriority(SByte.MaxValue, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false); 

                uint[] activeViewportID = new uint[1];
                activeViewportID[0] = MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.Viewport.ViewportID;
                IDNMcConditionalSelector viewportSelector = DNMcViewportConditionalSelector.Create(MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.Viewport.OverlayManager,
                                                                                                        DNEViewportType._EVT_ALL_VIEWPORTS,
                                                                                                        DNEViewportCoordinateSystem._EVCS_ALL_COORDINATE_SYSTEMS,
                                                                                                        activeViewportID,
                                                                                                        true);
                obj.SetConditionalSelector(DNEActionType._EAT_VISIBILITY,
                                            true,
                                            viewportSelector);
                obj.SetDrawPriority(SByte.MaxValue);

                MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.EditMode.StartInitObject(obj, ObjSchemeItem);

                m_Callback = m_EditMode.GetEventsCallback();
                m_EditMode.SetEventsCallback(this);

            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("PolyScan", McEx);
            }
        }


        #region IDNEditModeCallback Members

        public void ExitAction(int nExitCode)
        {
            m_EditMode.SetEventsCallback(m_Callback);
        }

        public void NewVertex(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, DNSMcVector3D WorldVertex, DNSMcVector3D ScreenVertex, uint uVertexIndex, double dAngle)
        {
        }

        public void PointDeleted(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, DNSMcVector3D WorldVertex, DNSMcVector3D ScreenVertex, uint uVertexIndex)
        {
        }

        public void PointNewPos(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, DNSMcVector3D WorldVertex, DNSMcVector3D ScreenVertex, uint uVertexIndex, double dAngle, bool bDownOnHeadPoint)
        {
        }

        public void ActiveIconChanged(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, DNEPermission eIconPermission, uint uIconIndex)
        {
        }

        public void InitItemResults(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, int nExitCode)
        {
            if (nExitCode != 0)
            {
                IDNMcMapViewport CurrViewport = MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.Viewport;

                IDNMcObjectScheme objScheme = pObject.GetScheme();
                IDNMcObjectSchemeNode[] objSchemeNode = objScheme.GetNodes(DNENodeKindFlags._ENKF_ANY_ITEM);

                DNSMcVector3D[] Coords = objSchemeNode[0].GetCoordinates(CurrViewport, /*DNEMcPointCoordSystem._EPCS_SCREEN*/mScanCoordSys, pObject);

                m_ScanPolygonGeometry = new DNSMcScanPolygonGeometry(mScanCoordSys, Coords);
                m_ScanPolygonGeometry.uGeometryType = DNGEOMETRY_TYPE.Polygon;

                //Remove polygon Item from the map
                pObject.Remove();
                objScheme.Dispose();

                DNSTargetFound[] TargetFound = null;
                try
                {
                    TargetFound = CurrViewport.ScanInGeometry(m_ScanPolygonGeometry, m_CompletelyInside, m_SpatialQueryParams);
                    ShowScanResult(m_ScanPolygonGeometry, TargetFound);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("ScanInGeometry", McEx);
                }
            }
        }

        public void EditItemResults(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, int nExitCode)
        {
        }

		public void DragMapResults(IDNMcMapViewport pViewport, DNSMcVector3D NewCenter)
        {
        }

		public void RotateMapResults(IDNMcMapViewport pViewport, float fNewYaw, float fNewPitch)
        {
        }

		public void DistanceDirectionMeasureResults(IDNMcMapViewport pViewport, DNSMcVector3D WorldVertex1, DNSMcVector3D WorldVertex2, double dDistance, double dAngle)
        {
        }

		public void DynamicZoomResults(IDNMcMapViewport pViewport, float fNewScale, DNSMcVector3D NewCenter)
        {
        }

		public void CalculateHeightResults(IDNMcMapViewport pViewport, double dHeight, DNSMcVector3D[] coords, int status)
        {
        }

		public void CalculateVolumeResults(IDNMcMapViewport pViewport, double dVolume, DNSMcVector3D[] coords, int status)
        {
        }

        #endregion
    }
}
