using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;
using System.Windows.Forms;

namespace MCTester.ObjectWorld
{
    public class ScanBoxGeometry : MCTester.ObjectWorld.ScanGeometryBase, IDNEditModeCallback
    {
        DNSMcScanBoxGeometry m_ScanBoxGeometry;
        IDNMcEditMode m_EditMode;
        private DNSQueryParams m_SpatialQueryParams;
        bool m_CompletelyInside;
        DNEMcPointCoordSystem mScanCoordSys;
        private IDNEditModeCallback m_Callback;
        public enum ActionType { Scan = 1, HeatMapPoints = 2 } ;
        private ActionType m_ActionType;
        private SaveFileDialog SFD;

        // for heat map convert (from map production)
        DataGridView m_dgvHeatMapPoints;
        int m_row;

        private Control m_ParentForm;

        public ScanBoxGeometry(DNEMcPointCoordSystem coordSys)
        {
            m_EditMode = MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.EditMode;
            m_Callback = null;
            SFD = new SaveFileDialog();
            mScanCoordSys = coordSys;
        }

        public ScanBoxGeometry(DNEMcPointCoordSystem coordSys, DataGridView dgvHeatMapPoints, int row)
            : this(coordSys)
        {
            m_ActionType = ActionType.HeatMapPoints;
            m_dgvHeatMapPoints = dgvHeatMapPoints;
            m_row = row;
        }

        public ScanBoxGeometry(DNEMcPointCoordSystem coordSys, DNSQueryParams SQParams, bool CompletelyInsideOnly, float Tolerance)
            : this(coordSys)
        {
            m_ActionType = ActionType.Scan;
            m_CompletelyInside = CompletelyInsideOnly;
            m_SpatialQueryParams = SQParams;
            m_EditMode = MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.EditMode;
            m_Callback = null;
        }

        public void StartRectScan()
        {
           PaintRect(DNEItemSubTypeFlags._EISTF_SCREEN, DNEMcPointCoordSystem._EPCS_SCREEN, DNEMcPointCoordSystem._EPCS_SCREEN);
        }

        public void StartSelectRect(Control parentForm)
        {
            m_ParentForm = parentForm;//.Parent.Parent.Parent;
            m_ParentForm.Hide();

            PaintRect(DNEItemSubTypeFlags._EISTF_WORLD, DNEMcPointCoordSystem._EPCS_WORLD, DNEMcPointCoordSystem._EPCS_WORLD);
        }


        private void PaintRect(DNEItemSubTypeFlags rectItemSubTypeBitField, DNEMcPointCoordSystem rectRectangleCoordinateSystem, DNEMcPointCoordSystem objLocationCoordSystem)
        {
            try
            {

                IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;
                DNSMcVector3D[] locationPoints = new DNSMcVector3D[0];


                IDNMcObjectSchemeItem ObjSchemeItem = DNMcRectangleItem.Create(rectItemSubTypeBitField,
                                                                                rectRectangleCoordinateSystem,
                                                                                DNEItemGeometryType._EGT_GEOMETRIC_IN_VIEWPORT,
                                                                                DNERectangleDefinition._ERD_RECTANGLE_DIAGONAL_POINTS,
                                                                                0f,
                                                                                0f,
                                                                                DNELineStyle._ELS_SOLID,
                                                                                DNSMcBColor.bcBlackOpaque,
                                                                                2f,
                                                                                null,
                                                                                new DNSMcFVector2D(0, -1),
                                                                                1f,
                                                                                DNEFillStyle._EFS_SOLID,
                                                                                new DNSMcBColor(0, 255, 100, 100));



                IDNMcObject obj = DNMcObject.Create(activeOverlay,
                                                    ObjSchemeItem,
                                                    objLocationCoordSystem,
                                                    locationPoints,
                                                    false);


                //In order to prevent retrieval of the rectangle in the scan 
                ObjSchemeItem.Detectibility = false;
                ((IDNMcRectangleItem)ObjSchemeItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                ((IDNMcRectangleItem)ObjSchemeItem).SetDrawPriority(SByte.MaxValue, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false); 

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
                MapCore.Common.Utilities.ShowErrorMessage("Failed in creating rectangle item", McEx);
            }
        }
       
        #region IDNEditModeCallback Members

        public void  ExitAction(int nExitCode)
        {
            m_EditMode.SetEventsCallback(m_Callback);
        }

        public void  NewVertex(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, DNSMcVector3D WorldVertex, DNSMcVector3D ScreenVertex, uint uVertexIndex, double dAngle)
        {
        }

        public void  PointDeleted(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, DNSMcVector3D WorldVertex, DNSMcVector3D ScreenVertex, uint uVertexIndex)
        {
        }

        public void  PointNewPos(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, DNSMcVector3D WorldVertex, DNSMcVector3D ScreenVertex, uint uVertexIndex, double dAngle, bool bDownOnHeadPoint)
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

                DNSMcVector3D[] Coords = objSchemeNode[0].GetCoordinates(CurrViewport, mScanCoordSys, pObject);
                for (int idx = 0; idx < Coords.Length; ++idx)
                    Coords[idx].z = 0;

                //Remove rectangle Item from the map
                pObject.Remove();
                objScheme.Dispose();

                if (Coords.Length == 2)
                {
                    DNSMcBox boxCoord = new DNSMcBox(Coords[0], Coords[1]);

                    if (m_ActionType == ActionType.Scan)
                    {
                        m_ScanBoxGeometry = new DNSMcScanBoxGeometry(mScanCoordSys, boxCoord);
                        m_ScanBoxGeometry.uGeometryType = DNGEOMETRY_TYPE.Box;

                        DNSTargetFound[] TargetFound = null;
                        try
                        {
                            TargetFound = CurrViewport.ScanInGeometry(m_ScanBoxGeometry, m_CompletelyInside, m_SpatialQueryParams);
                            ShowScanResult(m_ScanBoxGeometry, TargetFound);
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("ScanInGeometry", McEx);
                        }


                    }
                    else if (m_ActionType == ActionType.HeatMapPoints)
                    {
                        m_dgvHeatMapPoints[0, m_row].Tag = boxCoord;
                        m_dgvHeatMapPoints[0, m_row].Value = "Selected";
                        m_ParentForm.Show();
                    }
                }
            }
        }

        private string SaveFileDlg()
        {
            SFD.Filter = "MapCore Object Files (*.mcobj, *.mcobj.json , *.m, *.json) |*.mcobj; *.mcobj.json; *.m; *.json;|" +
            "MapCore Object Binary Files (*.mcobj,*.m)|*.mcobj;*.m;|" +
            "MapCore Object Json Files (*.mcobj.json, *.json)|*.mcobj.json;*.json;|" +
            "All Files|*.*";

            SFD.RestoreDirectory = true;
            if (SFD.ShowDialog() ==  DialogResult.OK)
            {
                return SFD.FileName;
            }
            else
                return String.Empty;
        }
       
        public void  EditItemResults(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, int nExitCode)
        {
        }

        public void  DragMapResults(IDNMcMapViewport pViewport, DNSMcVector3D NewCenter)
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
