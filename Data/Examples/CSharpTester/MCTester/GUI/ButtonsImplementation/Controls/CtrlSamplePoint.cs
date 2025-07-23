using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MCTester.GUI;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers;
using MCTester.General_Forms;
using MCTester.Managers.MapWorld;
using MCTester.MapWorld.MapUserControls;

namespace MCTester.Controls
{
    public partial class CtrlSamplePoint : Button, IDNAsyncQueryCallback
    {
        public delegate void delegateFinishMapClick();
        private delegateFinishMapClick m_delegateFinishMapClick;

        private Form m_containerForm;
        private DNEMcPointCoordSystem m_PointCoordSystem;
        private string m_UserControlName;
        private string m_DgvControlName;
        private bool m_SampleOnePoint;
        private bool m_PointInOverlayManagerCoordSys;
        private DNEQueryPrecision m_QueryPrecision;
        private bool m_IsChangeEnableWhenUserSelectPoint = false;
        private double m_PointZValue = double.MaxValue;
        private bool m_IsAsync = false;
        private bool m_IsRelativeToDTM = false;
        private int m_colXIndex = 0;
        private int m_colYIndex = 1;
        private int m_colZIndex = 2;
        private bool m_IsMustImageCalc = false;
        private IDNMcImageCalc m_mcImageCalc = null;

        public CtrlSamplePoint()
        {
            this.Text = "...";
            this.MouseClick += new MouseEventHandler(CtrlSamplePoint_MouseClick);
            PointCoordSystem = DNEMcPointCoordSystem._EPCS_WORLD;
            m_SampleOnePoint = true;
            _PointInOverlayManagerCoordSys = true;
            m_QueryPrecision = DNEQueryPrecision._EQP_DEFAULT;
        }

        public void SetDelegateFinishMapClick(delegateFinishMapClick delegateFinishMapClick)
        {
            m_delegateFinishMapClick = delegateFinishMapClick;
        }


        public void SetXYZColumnsIndexes(int _colXIndex, int _colYIndex, int _colZIndex)
        {
            m_colXIndex = _colXIndex;
            m_colYIndex = _colYIndex;
            m_colZIndex = _colZIndex;
        }

        public void SetIsMustImageCalc(bool isMustImageCalc)
        {
            m_IsMustImageCalc = isMustImageCalc;
        }

        public void SetImageCalc(IDNMcImageCalc mcImageCalc)
        {
            m_mcImageCalc = mcImageCalc;
        }

        public void CtrlSamplePoint_MouseClick(object sender, MouseEventArgs e)
        {
            if(m_IsMustImageCalc && m_mcImageCalc == null)
            {
                MessageBox.Show("this action must select image calc from the list", "Select Point");
                return;
            }
            if (MCTMapFormManager.MapForm != null && MCTMapFormManager.MapForm.Viewport != null)
            {
                m_containerForm = GetParentForm(this);
                //hide the container form
                m_containerForm.Hide();

                if (MCTMapFormManager.MapForm != null)
                {
                    MCTMapFormManager.MapForm.MapObjectCtrl.MouseClickEvent += new MouseClickEventArgs(MapObjectCtrl_MouseClickEvent);
                }
            }
        }

        void MapObjectCtrl_MouseClickEvent(object sender, Point mouseLocation)
        {
            MCTMapFormManager.MapForm.MapObjectCtrl.MouseClickEvent -= new MouseClickEventArgs(MapObjectCtrl_MouseClickEvent);       

            IDNMcMapViewport Viewport = MCTMapFormManager.MapForm.Viewport;

            bool isIntersect = false;
            DNSMcVector3D worldPoint = new DNSMcVector3D();
            DNSMcVector3D imagePoint = new DNSMcVector3D();
            DNSMcVector3D screenPoint = new DNSMcVector3D();
            screenPoint.x = mouseLocation.X;
            screenPoint.y = mouseLocation.Y;

            bool isUseCallback = false;
            if (PointCoordSystem != DNEMcPointCoordSystem._EPCS_SCREEN)
            {
                DNSQueryParams queryParams = new DNSQueryParams();
                queryParams.eTerrainPrecision = _QueryPrecision;

                Viewport.ScreenToWorldOnTerrain(screenPoint, out worldPoint, out isIntersect, queryParams);

                if (isIntersect == false)
                    Viewport.ScreenToWorldOnPlane(screenPoint, out worldPoint, out isIntersect);
                if (isIntersect == false)
                {
                    worldPoint.x = 0;
                    worldPoint.y = 0;
                    worldPoint.z = 0;
                }

                if (Viewport.GetImageCalc() != null)
                {
                    try
                    {
                        imagePoint = worldPoint;
                        imagePoint.z = 0;

                        if (PointCoordSystem == DNEMcPointCoordSystem._EPCS_WORLD)
                        {
                            isUseCallback = true;

                            DNMcNullableOut<bool> isDTM = new DNMcNullableOut<bool>();
                            m_imagePoint = imagePoint;
                            m_screenPoint = screenPoint;

                            Viewport.GetImageCalc().ImagePixelToCoordWorld(new DNSMcVector2D(imagePoint.x, imagePoint.y), isDTM, null, this);
                        }
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("ImagePixelToCoordWorld", McEx);
                    }
                }
            }
            else
                isIntersect = true;

            if(!isUseCallback)
                ContinueClickMap(isIntersect, worldPoint, imagePoint, screenPoint, Viewport);
        }

        DNSMcVector3D m_imagePoint;
        DNSMcVector3D m_screenPoint;

        private void ContinueClickMap(bool bIntersectionFound, DNSMcVector3D worldPoint, DNSMcVector3D imagePoint, DNSMcVector3D screenPoint, IDNMcMapViewport Viewport)
        {
            try
            {
                if (Viewport.OverlayManager != null && PointCoordSystem != DNEMcPointCoordSystem._EPCS_SCREEN)
                {
                    if (_PointInOverlayManagerCoordSys == true)
                    {
                        worldPoint = Viewport.ViewportToOverlayManagerWorld(worldPoint, Viewport.OverlayManager);
                    }
                }

                if (m_IsMustImageCalc && m_mcImageCalc != null && Viewport.CoordinateSystem != m_mcImageCalc.GetGridCoordSys())
                {
                    IDNMcGridConverter mcGridConverter = DNMcGridConverter.Create(Viewport.CoordinateSystem, m_mcImageCalc.GetGridCoordSys());
                    DNSMcVector3D icPoint;
                    int zone;
                    mcGridConverter.ConvertAtoB(worldPoint, out icPoint, out zone);
                    worldPoint = icPoint;
                }

                if (m_PointZValue != double.MaxValue)
                    worldPoint.z = m_PointZValue;
                UserControl uc = null;
                DataGridView DataGridViewControl = null;

                if (_SampleOnePoint == true)
                {
                    uc = GetUserControl();
                    if (uc == null)
                        return;
                }
                else
                {
                    DataGridViewControl = GetDataGridView();
                    if (DataGridViewControl == null)
                        return;
                }

                if (m_IsRelativeToDTM)
                {
                    worldPoint.z = 0;
                }

                if (DataGridViewControl != null && bIntersectionFound)
                {
                    DataGridView DGV = DataGridViewControl;
                    int lastRow = DGV.Rows.GetLastRow(DataGridViewElementStates.None);
                    DGV.Rows.Add();
                    switch (PointCoordSystem)
                    {
                        case DNEMcPointCoordSystem._EPCS_WORLD:
                            DGV[m_colXIndex, lastRow].Value = worldPoint.x;
                            DGV[m_colYIndex, lastRow].Value = worldPoint.y;
                            DGV[m_colZIndex, lastRow].Value = worldPoint.z;
                            break;
                        case DNEMcPointCoordSystem._EPCS_SCREEN:
                            DGV[m_colXIndex, lastRow].Value = screenPoint.x;
                            DGV[m_colYIndex, lastRow].Value = screenPoint.y;
                            DGV[m_colZIndex, lastRow].Value = 0;
                            break;
                        case DNEMcPointCoordSystem._EPCS_IMAGE:
                            DGV[m_colXIndex, lastRow].Value = imagePoint.x;
                            DGV[m_colYIndex, lastRow].Value = imagePoint.y;
                            DGV[m_colZIndex, lastRow].Value = 0;
                            break;
                    }
                }
                else
                {
                    if (uc is MCTester.Controls.Ctrl3DVector)
                    {
                        MCTester.Controls.Ctrl3DVector control = (MCTester.Controls.Ctrl3DVector)uc;
                        
                            switch (PointCoordSystem)
                            {
                                case DNEMcPointCoordSystem._EPCS_WORLD:
                                    control.SetVector3D(worldPoint);
                                    break;

                                case DNEMcPointCoordSystem._EPCS_SCREEN:
                                    control.SetVector3D(new DNSMcVector3D(double.Parse(screenPoint.x.ToString()), double.Parse(screenPoint.y.ToString()), 0));
                                    break;

                                case DNEMcPointCoordSystem._EPCS_IMAGE:
                                    control.SetVector3D(imagePoint);
                                    break;
                            }
                        

                        if (IsChangeEnableWhenUserSelectPoint)
                            control.Enabled = !control.Enabled;
                    }

                    if (uc is MCTester.Controls.Ctrl3DFVector)
                    {
                        MCTester.Controls.Ctrl3DFVector control = (MCTester.Controls.Ctrl3DFVector)uc;
                        
                            switch (PointCoordSystem)
                            {
                                case DNEMcPointCoordSystem._EPCS_WORLD:
                                    control.SetVector3D(new DNSMcFVector3D(float.Parse(worldPoint.x.ToString()), float.Parse(worldPoint.y.ToString()), float.Parse(worldPoint.z.ToString())));
                                    break;

                                case DNEMcPointCoordSystem._EPCS_SCREEN:
                                    control.SetVector3D(new DNSMcFVector3D(float.Parse(screenPoint.x.ToString()), float.Parse(screenPoint.y.ToString()), 0f));
                                    break;

                                case DNEMcPointCoordSystem._EPCS_IMAGE:
                                    control.SetVector3D(new DNSMcFVector3D(float.Parse(imagePoint.x.ToString()), float.Parse(imagePoint.y.ToString()), 0f));
                                    break;
                            }
                       
                    }
                    if (uc is MCTester.Controls.Ctrl2DFVector)
                    {
                        MCTester.Controls.Ctrl2DFVector control = (MCTester.Controls.Ctrl2DFVector)uc;
                        
                            switch (PointCoordSystem)
                            {
                                case DNEMcPointCoordSystem._EPCS_WORLD:
                                    control.SetVector2D(new DNSMcFVector2D(float.Parse(worldPoint.x.ToString()), float.Parse(worldPoint.y.ToString())));
                                    break;

                                case DNEMcPointCoordSystem._EPCS_SCREEN:
                                    control.SetVector2D(new DNSMcFVector2D(float.Parse(screenPoint.x.ToString()), float.Parse(screenPoint.y.ToString())));
                                    break;

                                case DNEMcPointCoordSystem._EPCS_IMAGE:
                                    control.SetVector2D(new DNSMcFVector2D(float.Parse(imagePoint.x.ToString()), float.Parse(imagePoint.y.ToString())));
                                    break;
                            }
                        
                    }
                    if (uc is MCTester.Controls.Ctrl2DVector)
                    {
                        MCTester.Controls.Ctrl2DVector control = (MCTester.Controls.Ctrl2DVector)uc;
                       
                            switch (PointCoordSystem)
                            {
                                case DNEMcPointCoordSystem._EPCS_WORLD:
                                    control.X = worldPoint.x;
                                    control.Y = worldPoint.y;
                                    break;

                                case DNEMcPointCoordSystem._EPCS_SCREEN:
                                    control.SetVector2D(new DNSMcVector2D(double.Parse(screenPoint.x.ToString()), double.Parse(screenPoint.y.ToString())));
                                    break;

                                case DNEMcPointCoordSystem._EPCS_IMAGE:
                                    control.X = imagePoint.x;
                                    control.Y = imagePoint.y;
                                    break;
                            }
                        
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage(IDNMcErrors.ErrorCodeToString(DNEMcErrorCode.FAILURE), McEx);
            }
            //show the form
            m_containerForm.Show();

            if (m_delegateFinishMapClick != null)
                m_delegateFinishMapClick();

            if (!bIntersectionFound && PointCoordSystem != DNEMcPointCoordSystem._EPCS_SCREEN)
                MessageBox.Show(IDNMcErrors.ErrorCodeToString(DNEMcErrorCode.FAILURE) + " ,no intersection found", "Error Get Point");
        }

        public DNEMcPointCoordSystem PointCoordSystem
        {
            get { return m_PointCoordSystem; }
            set { m_PointCoordSystem = value; }
        }

        public double _PointZValue
        {
            get { return m_PointZValue; }
            set { m_PointZValue = value; }
        }

        public string _UserControlName
        {
            get { return m_UserControlName; }
            set { m_UserControlName = value; }
        }

        public string _DgvControlName
        {
            get { return m_DgvControlName; }
            set { m_DgvControlName = value; }
        }

        public bool _SampleOnePoint
        {
            get { return m_SampleOnePoint; }
            set { m_SampleOnePoint = value; }
        }

        public bool _PointInOverlayManagerCoordSys
        {
            get { return m_PointInOverlayManagerCoordSys; }
            set { m_PointInOverlayManagerCoordSys = value; }
        }

        public DNEQueryPrecision _QueryPrecision
        {
            get { return m_QueryPrecision; }
            set { m_QueryPrecision = value; }
        }

        public bool _IsRelativeToDTM
        {
            get { return m_IsRelativeToDTM; }
            set { m_IsRelativeToDTM = value; }
        }

        public bool IsAsync
        {
            get { return m_IsAsync; }
            set { m_IsAsync = value; }
        }

        public bool IsChangeEnableWhenUserSelectPoint
        {
            get { return m_IsChangeEnableWhenUserSelectPoint; }
            set { m_IsChangeEnableWhenUserSelectPoint = value; }

        }

        private Form GetParentForm(Control ctr)
        {
            if (ctr.Parent is Form)
                return ctr.Parent as Form;
            else
                return GetParentForm(ctr.Parent);
        }



        private UserControl GetUserControl()
        {
            Control parentControl = this.Parent;

            foreach (Control ctr in parentControl.Controls)
            {
                if (ctr.Name == _UserControlName)
                {
                    return ctr as UserControl;
                }
            }

            return null;
        }

        private DataGridView GetDataGridView()
        {
            Control parentControl = this.Parent;

            foreach (Control ctr in parentControl.Controls)
            {
                if (ctr.Name == _DgvControlName)
                {
                    return ctr as DataGridView;
                }
            }

            return null;
        }

        public void OnTerrainHeightResults(bool bHeightFound, double dHeight, DNSMcVector3D? pNormal)
        {
            throw new NotImplementedException();
        }

        public void OnTerrainHeightsAlongLineResults(DNSMcVector3D[] aPointsWithHeights, float[] afSlopes, DNSSlopesData? pSlopesData)
        {
            throw new NotImplementedException();
        }

        public void OnExtremeHeightPointsInPolygonResults(bool bPointsFound, DNSMcVector3D? pHighestPoint, DNSMcVector3D? pLowestPoint)
        {
            throw new NotImplementedException();
        }

        public void OnTerrainHeightMatrixResults(double[] adHeightMatrix)
        {
            throw new NotImplementedException();
        }

        public void OnTerrainAnglesResults(double dPitch, double dRoll)
        {
            throw new NotImplementedException();
        }

        public void OnRayIntersectionResults(bool bIntersectionFound, DNSMcVector3D? pIntersection, DNSMcVector3D? pNormal, double? pdDistance)
        {
            if (pIntersection != null && 
                pIntersection.HasValue && 
                MCTMapFormManager.MapForm != null && 
                MCTMapFormManager.MapForm.Viewport != null)
                ContinueClickMap(bIntersectionFound, pIntersection.Value, m_imagePoint, m_screenPoint, MCTMapFormManager.MapForm.Viewport);
            else if (m_containerForm != null)
                //show the form
                m_containerForm.Show();
                
        }

        public void OnRayIntersectionTargetsResults(DNSTargetFound[] aIntersections)
        {
            throw new NotImplementedException();
        }

        public void OnLineOfSightResults(DNSLineOfSightPoint[] aPoints, double dCrestClearanceAngle, double dCrestClearanceDistance)
        {
            throw new NotImplementedException();
        }

        public void OnPointVisibilityResults(bool bIsTargetVisible, double? pdMinimalTargetHeightForVisibility, double? pdMinimalScouterHeightForVisibility)
        {
            throw new NotImplementedException();
        }

        public void OnAreaOfSightResults(IDNAreaOfSight pAreaOfSight, DNSLineOfSightPoint[][] aLinesOfSight, DNSPolygonsOfSight? pSeenPolygons, DNSPolygonsOfSight? pUnseenPolygons, DNSStaticObjectsIDs[] aSeenStaticObjects)
        {
            throw new NotImplementedException();
        }

        public void OnBestScoutersLocationsResults(DNSMcVector3D[] aScouters)
        {
            throw new NotImplementedException();
        }

        public void OnLocationFromTwoDistancesAndAzimuthResults(DNSMcVector3D Target)
        {
            throw new NotImplementedException();
        }

        public void OnDtmLayerTileGeometryByKeyResults(DNSTileGeometry TileGeometry)
        {
            throw new NotImplementedException();
        }

        public void OnRasterLayerTileBitmapByKeyResults(DNEPixelFormat eBitmapPixelFormat, bool bBitmapFromTopToBottom, DNSMcSize BitmapSize, DNSMcSize BitmapMargins, Byte[] aBitmapBits)
        {
            throw new NotImplementedException();
        }

        public void OnRasterLayerColorByPointResults(DNSMcBColor Color)
        {
            throw new NotImplementedException();
        }

        public void OnTraversabilityAlongLineResults(DNSTraversabilityPoint[] aTraversabilitySegments)
        {
            throw new NotImplementedException();
        }

        public void OnError(DNEMcErrorCode eErrorCode)
        {
            if(m_containerForm != null)
                m_containerForm.Show();
        }

       
    }
}
