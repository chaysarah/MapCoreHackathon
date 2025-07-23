using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers.MapWorld;
using MCTester.Managers.ObjectWorld;
using MCTester.GUI.Map;

namespace MCTester.Controls
{
    public partial class CtrlSMcBox : UserControl, IDNEditModeCallback
    {
        private DNEItemSubTypeFlags m_rectItemSubTypeBitField;
        private DNEMcPointCoordSystem m_objLocationCoordSystem;
        private IDNMcObject m_BBObject;
        private IDNMcEditMode m_EditMode;
        private IDNEditModeCallback m_Callback;
        private IDNMcOverlay m_tempOverlay = null;

        IDNMcMapViewport m_CurrViewport;

        public CtrlSMcBox()
        {
            InitializeComponent();

            this.ctrlSamplePointBottomRight._UserControlName = ctrl3DVectorMax.Name;
            this.ctrlSamplePointTopLeft._UserControlName = ctrl3DVectorMin.Name;

            ctrlSamplePointTopLeft.SetDelegateFinishMapClick(CheckBBObject);
            ctrlSamplePointBottomRight.SetDelegateFinishMapClick(CheckBBObject);

            m_rectItemSubTypeBitField = DNEItemSubTypeFlags._EISTF_WORLD;
            m_objLocationCoordSystem = DNEMcPointCoordSystem._EPCS_WORLD;
        }

        public string GroupBoxText
        {
            get
            {
                return gbSMcBox.Text;
            }
            set
            {
                gbSMcBox.Text = value;
            }
        }

        public void SetPointData(IDNMcMapViewport CurrViewport, bool isReadOnly = false)
        {
            bool bIsReadOnly = isReadOnly;
            m_CurrViewport = CurrViewport;

            
            if (m_CurrViewport != null)
            {
                if (m_CurrViewport == MCTMapFormManager.MapForm.Viewport)
                {
                    m_EditMode = MCTMapFormManager.MapForm.EditMode;
                }
                else
                {
                    foreach (MCTMapForm mapForm in MCTMapFormManager.AllMapForms)
                    {
                        if (mapForm.Viewport == m_CurrViewport)
                        {
                            m_EditMode = mapForm.EditMode;
                            break;
                        }
                    }
                }

                if (m_CurrViewport.GetImageCalc() != null)
                {
                    m_objLocationCoordSystem = DNEMcPointCoordSystem._EPCS_IMAGE;
                    ctrlSamplePointTopLeft.PointCoordSystem = DNEMcPointCoordSystem._EPCS_IMAGE;
                    ctrlSamplePointBottomRight.PointCoordSystem = DNEMcPointCoordSystem._EPCS_IMAGE;
                }
            }
            else
            {
                chxIsShowBoundingBox.Visible = false;
                bIsReadOnly = true;
            }
            ctrlSamplePointTopLeft.Visible = !bIsReadOnly;
            ctrlSamplePointBottomRight.Visible = !bIsReadOnly;
            btnPaintRect.Visible = !bIsReadOnly;
        }

        public DNSMcBox GetBoxValue()
        {
            return new DNSMcBox(ctrl3DVectorMin.GetVector3D(), ctrl3DVectorMax.GetVector3D()); ;
        }

        public void SetBoxValue(DNSMcBox value)
        {
            ctrl3DVectorMin.SetVector3D(value.MinVertex);
            ctrl3DVectorMax.SetVector3D(value.MaxVertex);
        }

        public void SetBoxValue(DNSMcBox? value)
        {
            if (value.HasValue)
            {
                ctrl3DVectorMin.SetVector3D(value.Value.MinVertex);
                ctrl3DVectorMax.SetVector3D(value.Value.MaxVertex);
            }
        }

        public void SetReadOnly()
        {
            EnableButtons(false);
            ctrl3DVectorMin.IsReadOnly = true;
            ctrl3DVectorMax.IsReadOnly = true;
        }

        public void EnableButtons(bool isEnabled)
        {
            ctrlSamplePointTopLeft.Enabled = isEnabled;
            ctrlSamplePointBottomRight.Enabled = isEnabled;
        }

        public void DisabledZ()
        {
            ctrl3DVectorMin.DisabledZ();
            ctrl3DVectorMax.DisabledZ();
        }

        public void HideZ()
        {
            ctrl3DVectorMin.HideZ();
            ctrl3DVectorMax.HideZ();
        }
        Form m_activeForm;

        private void btnPaintRect_Click(object sender, EventArgs e)
        {
            try
            {
                RemoveBBObject();
                m_activeForm = FindForm();
                if (m_activeForm != null)
                    m_activeForm.Hide();

                IDNMcObject obj;
                IDNMcObjectSchemeItem ObjSchemeItem;
                CreateBBObject(out obj, out ObjSchemeItem);

                m_EditMode.StartInitObject(obj, ObjSchemeItem);

                m_Callback = m_EditMode.GetEventsCallback();
                m_EditMode.SetEventsCallback(this);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Failed in creating rectangle item", McEx);
            }
        }

        private void CreateBBObject(out IDNMcObject obj, out IDNMcObjectSchemeItem ObjSchemeItem, DNSMcVector3D[] locationPoints = null)
        {
            obj = null;
            ObjSchemeItem = null;
            if (m_CurrViewport != null)
            {
                try
                {
                    if (m_tempOverlay == null)
                        m_tempOverlay = Manager_MCTSymbology.GetTempOverlay(m_CurrViewport.OverlayManager); 

                    if (locationPoints == null)
                        locationPoints = new DNSMcVector3D[0];

                    ObjSchemeItem = DNMcRectangleItem.Create(m_rectItemSubTypeBitField,
                                                            m_objLocationCoordSystem,
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

                    obj = DNMcObject.Create(m_tempOverlay,
                                                        ObjSchemeItem,
                                                        m_objLocationCoordSystem,
                                                        locationPoints,
                                                        false);

                    //In order to prevent retrieval of the rectangle in the scan 
                    ObjSchemeItem.Detectibility = false;
                    ((IDNMcRectangleItem)ObjSchemeItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    ((IDNMcRectangleItem)ObjSchemeItem).SetDrawPriority(SByte.MaxValue, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    obj.SetImageCalc(m_CurrViewport.GetImageCalc());
                    obj.SetDrawPriority(SByte.MaxValue);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("Failed in creating rectangle item", McEx);
                }
            }
            else
                MessageBox.Show("Failed in creating rectangle item, Missing active viewport", "Creating rectangle item");
        }

        public void ExitAction(int nExitCode)
        {
            try
            {
                m_EditMode.SetEventsCallback(m_Callback);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Failed in creating rectangle item", McEx);
            }
        }

        public bool GetIsShowBBObject() { return chxIsShowBoundingBox.Checked; }

        public void SetIsShowBBObject(bool value) { chxIsShowBoundingBox.Checked = value; }

        public void HideBBHide()
        {
            chxIsShowBoundingBox.Checked = false;
        }

        public void ShowBBHide()
        {
            chxIsShowBoundingBox.Checked = true;
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
            if (m_activeForm != null)
                m_activeForm.Show();
            try
            {
                if (nExitCode != 0)
                {
                    IDNMcObjectScheme objScheme = pObject.GetScheme();
                    IDNMcObjectSchemeNode[] objSchemeNode = objScheme.GetNodes(DNENodeKindFlags._ENKF_ANY_ITEM);

                    DNSMcVector3D[] Coords = objSchemeNode[0].GetCoordinates(m_CurrViewport, m_objLocationCoordSystem, pObject);
                    if (Coords != null)
                    {
                        for (int idx = 0; idx < Coords.Length; ++idx)
                        {
                            Coords[idx].z = 0;
                        }

                        if (Coords.Length == 2)
                        {
                            SetBoxValue(new DNSMcBox(Coords[0], Coords[1]));
                        }
                    }
                    if (chxIsShowBoundingBox.Checked)
                    {
                        RemoveBBObject();
                        m_BBObject = pObject;
                    }
                    else
                    {
                        //Remove rectangle item from the map
                        pObject.Remove();
                        objScheme.Dispose();
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Failed in creating rectangle item", McEx);
            }
        }

        public void RemoveBBObject()
        {
            try
            {
                if (m_BBObject != null)
                {
                    m_BBObject.Remove();
                    m_BBObject.Dispose();
                    m_BBObject = null;
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Failed in creating rectangle item", McEx);
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

        public void CalculateHeightResults(IDNMcMapViewport pViewport, double dHeight, DNSMcVector3D[] aCoords, int nStatus)
        {
        }

        public void CalculateVolumeResults(IDNMcMapViewport pViewport, double dVolume, DNSMcVector3D[] aCoords, int nStatus)
        {
        }

        private void chxIsShowBoundingBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBBObject();
        }

        public void CheckBBObject()
        {
            try
            {
                if (chxIsShowBoundingBox.Checked)
                {
                    RemoveBBObject();
                    IDNMcObject obj;
                    IDNMcObjectSchemeItem ObjSchemeItem;

                    DNSMcVector3D[] points = new DNSMcVector3D[2] { ctrl3DVectorMin.GetVector3D(), ctrl3DVectorMax.GetVector3D() };

                    CreateBBObject(out obj, out ObjSchemeItem, points);
                    m_BBObject = obj;
                }
                else
                {
                    RemoveBBObject();
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Failed in creating rectangle item", McEx);
            }
        }
    
        private void ctrl3DVectorMin_Leave(object sender, EventArgs e)
        {
            CheckBBObject();
        }

        private void ctrl3DVectorMax_Leave(object sender, EventArgs e)
        {
            CheckBBObject();
        }

    }
}
