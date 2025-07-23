using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers.MapWorld;
using MCTester.General_Forms;
using MapCore.Common;
using MCTester.Managers.ObjectWorld;
using MCTester.GUI.Trees;

namespace MCTester.ObjectWorld.ObjectsUserControls.ConditionalSelectorForms
{
    public partial class ucLocationConditionalSelector : ucBaseConditionalSelector, IUserControlItem
    {
        private IDNMcLocationConditionalSelector m_CurrentCondSelector;
        private IDNMcOverlayManager m_OverlayManager;
        private IDNMcEditMode m_EditMode;
        private IDNMcObject m_ReturnObject;
        private IDNMcObjectSchemeItem m_ReturnItem;
        DNSMcVector3D[] m_points;
        private int m_numAction = 0;

        private int m_ExitStatus = 0;

        public ucLocationConditionalSelector()
        {
            InitializeComponent();        
        }

        public new void LoadItem(object aItem)
        {
            m_CurrentCondSelector = (IDNMcLocationConditionalSelector)aItem;
            try
            {
                m_OverlayManager = m_CurrentCondSelector.GetOverlayManager();
            }
            catch (Exception e)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetOverlayManager", e);
            }


            base.LoadItem(aItem);

            if (MCTester.Managers.MapWorld.MCTMapFormManager.MapForm != null)
            {
                m_EditMode = MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.EditMode;
            }

            try
            {
                m_points = m_CurrentCondSelector.GetPolygonPoints();
                SetPolygonPoints(m_points);
            }
            catch(MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetPolygonPoints", McEx);
            }
        }

        public void InitItemResults(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, int nExitCode)
        {
            m_ReturnObject = pObject;
            m_ReturnItem = pItem;
            m_ExitStatus = nExitCode;

            MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.EditModeManagerCallback.UnregisterEventsCallback(null);
            MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.EditModeManagerCallback.InitItemResultsEvent -= new MCTester.Managers.ObjectWorld.InitItemResultsEventArgs(InitItemResults);
        }

        private Form GetParentForm(Control ctr)
        {
            if (ctr.Parent is Form)
                return ctr.Parent as Form;
            else
                return GetParentForm(ctr.Parent);
        }

        IDNMcObjectSchemeItem m_ObjSchemeItem;
        IDNMcObject m_obj;

        private void btnDrawPolygon_Click(object sender, EventArgs e)
        {
            m_numAction = 1;
            Form frmParent = this.GetParentForm(this);
            frmParent.Hide();
            MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.EditModeManagerCallback.InitItemResultsEvent += new MCTester.Managers.ObjectWorld.InitItemResultsEventArgs(InitItemResults);
           
            m_ExitStatus = 0;
            
            try
            {
                m_obj = Manager_MCConditionalSelectorObjects.GetObjectOfSelector(m_CurrentCondSelector);
                if (m_obj == null)
                {
                    CreatePolygon(null);                   
                }
                else
                {
                    IDNMcObjectScheme scheme = m_obj.GetScheme();
                    if(scheme != null)
                    {
                        IDNMcObjectSchemeNode[] nodes = scheme.GetNodes(DNENodeKindFlags._ENKF_ANY_ITEM);
                        if(nodes != null && nodes.Length > 0)
                            m_ObjSchemeItem = (IDNMcObjectSchemeItem)nodes[0];
                    }
                }

                if(m_obj != null && m_ObjSchemeItem != null)
                    m_EditMode.StartInitObject(m_obj, m_ObjSchemeItem);

                while (m_ExitStatus == 0)
                    System.Windows.Forms.Application.DoEvents();

                if (m_ReturnItem != null)
                {
                    try
                    {
                        
                        Manager_MCConditionalSelectorObjects.AddNewItem(m_CurrentCondSelector, m_obj);
                        SetPolygonPoints(m_ReturnObject.GetLocationPoints(0));
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("GetLocationPoints", McEx);
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Draw Polygon", McEx);
            }

            frmParent.Show();
        }

        private void SetPolygonPoints(DNSMcVector3D[] polygonPoints)
        {
            ctrlPolygonPoints.SetPoints(polygonPoints);
        }

        private void CreatePolygon(DNSMcVector3D[] points)
        {
            try
            {
                m_CurrentCondSelector.GetOverlayManager().SetConditionalSelectorLock(m_CurrentCondSelector, true);
            }
            catch (MapCoreException ex)
            {
                Utilities.ShowErrorMessage("GetOverlayManager/SetConditionalSelectorLock", ex);
            }

            try
            {
                IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;
                DNSMcVector3D[] locationPoints = new DNSMcVector3D[0];

                DNEItemSubTypeFlags subTypeFlags = 0;
                subTypeFlags |= DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN;
                subTypeFlags |= DNEItemSubTypeFlags._EISTF_SCREEN;

                m_ObjSchemeItem = DNMcPolygonItem.Create(subTypeFlags,
                                                            DNELineStyle._ELS_SOLID,
                                                            DNSMcBColor.bcBlackOpaque,
                                                            3f,
                                                            null,
                                                            new DNSMcFVector2D(0, -1),
                                                            1f,
                                                            DNEFillStyle._EFS_NONE,
                                                            DNSMcBColor.bcWhiteOpaque,
                                                            null,
                                                            new DNSMcFVector2D(1, 1));

                m_obj = DNMcObject.Create(activeOverlay,
                                                    m_ObjSchemeItem,
                                                    DNEMcPointCoordSystem._EPCS_WORLD,
                                                    (points == null)? locationPoints : points,
                                                    true);
            }
            catch (MapCoreException ex)
            {
                Utilities.ShowErrorMessage("DNMcPolygonItem.Create/DNMcObject.Create", ex);
            }

        }

        public void Cancel()
        {
            ucLocationConditionalSelector_Leave(null, null);
        }

        protected override void Save()
        {
            base.Save();
            m_numAction = 2;
            DNSMcVector3D[] points = null;
            if (ctrlPolygonPoints.GetPoints(out points))
            {
                if (base.m_IsCanCloseFrom == true)
                {
                    try
                    {
                        m_CurrentCondSelector.SetPolygonPoints(points);
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("SetPolygonPoints", McEx);
                    }
                }

                if (m_obj == null && points != null && points.Length > 0)
                {
                    CreatePolygon(points);
                    Manager_MCConditionalSelectorObjects.AddItem(m_CurrentCondSelector, m_obj);
                }
                else if (m_obj != null)
                {
                    m_obj.SetLocationPoints(points, 0);
                }
            }
            else
                base.m_IsCanCloseFrom = false;
        }

        public void ucLocationConditionalSelector_Leave(object sender, EventArgs e)
        {
            try
            {
                // drew new polygon but not click save - need to cancel the new polygon points.
                if (m_numAction == 1 && m_obj != null)
                {
                    m_obj.SetLocationPoints(m_points, 0);
                }
            }
            catch(MapCoreException ex)
            {
                Utilities.ShowErrorMessage("SetPolygonPoints", ex);
            }
        }

        
    }
}
