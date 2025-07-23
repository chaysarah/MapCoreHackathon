using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.ObjectWorld.ObjectsUserControls.ConditionalSelectorForms;
using MCTester.General_Forms;
using MCTester.Managers;

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class ucObjectSchemeNode : UserControl,IUserControlItem
    {
        private IDNMcObjectSchemeNode m_CurrObject;
        private IDNMcObjectScheme m_CurrObjectScheme;
        private string[] m_ObjectsNames = null;
        private List<IDNMcObject> m_lstObjectsItems = null;
        private bool m_IsInLoadForm = false;

        public ucObjectSchemeNode()
        {
            InitializeComponent();
        }

        #region IUserControlItem Members

        public void LoadItem(object aItem)
        {
            m_CurrObject = (IDNMcObjectSchemeNode)aItem;
            ctrlBoundingRectScreen.BoundingRectSchemeNode = m_CurrObject;
            ctrlBoundingRectWorld.BoundingRectSchemeNode = m_CurrObject;

            // get objects to this node.
            IDNMcObjectScheme scheme = m_CurrObject.GetScheme();
            if(scheme != null)
            {
                IDNMcObject[] objectsItems = scheme.GetObjects();
                if (objectsItems != null)
                {
                    m_lstObjectsItems = new List<IDNMcObject>();
                    m_lstObjectsItems.Add(null);

                    m_ObjectsNames = new string[objectsItems.Length+1];
                    m_ObjectsNames[0] = "Any (if possible)"; 

                    int i = 1;
                    foreach (IDNMcObject obj in objectsItems)
                    {
                        m_lstObjectsItems.Add(obj);
                        m_ObjectsNames[i] = Manager_MCNames.GetNameByObject(obj);
                        i++;
                    }
                    cbObjects.Items.AddRange(m_ObjectsNames);
                    m_IsInLoadForm = true;
                    if(m_lstObjectsItems.Count == 2)
                        cbObjects.SelectedIndex = 1;
                    m_IsInLoadForm = false;

                }
            }
            

            try
            {
                if (m_CurrObject is IDNMcObjectLocation)
                    ctrlObjStatePropertyDicSelectorCond.InitControl(true);
                else
                    ctrlObjStatePropertyDicSelectorCond.InitControl(false);

                ctrlObjStatePropertyDicSelectorCond.Load(m_CurrObject);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetConditionalSelector", McEx);
            }

            
            try
            {
                txtNodeKind.Text = m_CurrObject.GetNodeKind().ToString();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetNodeKind", McEx);
            }

            try
            {
                txtNodeType.Text = m_CurrObject.GetNodeType().ToString();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetNodeType", McEx);
            }

            try
            {
                ntxNodeID.SetUInt32(m_CurrObject.GetID());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetID", McEx);
            }

          //  GetGeometryCoordinateSystem();

            try
            {
                ctrlObjStatePropertyVisibilityOption.Load(m_CurrObject.GetVisibilityOption);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetVisibilityOption", McEx);
            }


            try
            {
                ctrlObjStatePropertyTransformOption.Load(m_CurrObject.GetTransformOption);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetTransformOption", McEx);
            }
            
            try
            {
                IDNMcUserData UD = m_CurrObject.GetUserData();
                if (UD!=null)
                {
                    TesterUserData TUD = (TesterUserData)UD;
                    ctrlUserData.UserDataByte = TUD.UserDataBuffer;
                }
                
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetUserData", McEx);
            }            
        }

        #endregion

        protected virtual void SaveItem()
        {
            try
            {
                if (m_CurrObject.GetParents().Length == 0 && !(m_CurrObject is IDNMcObjectLocation))
                    Managers.ObjectWorld.Manager_MCObjectSchemeItem.SetStandaloneItemID((IDNMcObjectSchemeItem)m_CurrObject, ntxNodeID.GetUInt32());

                //Set ID for standalone items
                m_CurrObject.SetID(ntxNodeID.GetUInt32());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetID", McEx);
            }

            try
            {
                TesterUserData TUD = null;
                if (ctrlUserData.UserDataByte != null && ctrlUserData.UserDataByte.Length != 0)
                    TUD = new TesterUserData(ctrlUserData.UserDataByte);

                m_CurrObject.SetUserData(TUD);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetUserData", McEx);
            }

            try
            {
                ctrlObjStatePropertyVisibilityOption.Save(m_CurrObject.SetVisibilityOption);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetVisibilityOption", McEx);
            }

            try
            {
                ctrlObjStatePropertyTransformOption.Save(m_CurrObject.SetTransformOption);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetTransformOption", McEx);
            }
            try
            {
                ctrlObjStatePropertyDicSelectorCond.Save();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetConditionalSelector", McEx);
            }

            // turn on all viewports render needed flags
            if (m_CurrObjectScheme != null)
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_CurrObjectScheme.GetOverlayManager());
        }

        public IDNMcObjectScheme CurrObjectScheme
        {
            get
            {
                if (m_CurrObject != null)
                    m_CurrObjectScheme = m_CurrObject.GetScheme();
                else
                    m_CurrObjectScheme = null;
                return m_CurrObjectScheme;
            }
            set { m_CurrObjectScheme = value; }
        }

        #region Public Property

        public DNEActionOptions PropertyVisibility
        {
            get { return (DNEActionOptions)ctrlObjStatePropertyVisibilityOption.RegEnumVal; }
            set { ctrlObjStatePropertyVisibilityOption.RegEnumVal = value; }
        }

        public DNEActionOptions PropertyTransform
        {
            get { return (DNEActionOptions)ctrlObjStatePropertyTransformOption.RegEnumVal; }
            set { ctrlObjStatePropertyTransformOption.RegEnumVal = value; }
        }

        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.btnApply_Click(sender, e);
            GeneralFuncs.CloseParentForm(this);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            SaveItem();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GeneralFuncs.CloseParentForm(this);
        }

        private void btnIDList_Click(object sender, EventArgs e)
        {
            if (m_CurrObject.GetScheme() != null)
            {
                frmPropertiesIDList PropertyIDListForm = new frmPropertiesIDList(m_CurrObject);
                PropertyIDListForm.Show();
            }
            else
            {
                MessageBox.Show("This item not connect to scheme, please connect and try again", "Missing Scheme");
            }
        }

        private void btnEffectiveVisibilityObject_Click(object sender, EventArgs e)
        {
            frmSchemeNodeEffectiveVisibilityInViewport EffectiveVisibilityInViewport = new frmSchemeNodeEffectiveVisibilityInViewport(m_CurrObject);
            if (EffectiveVisibilityInViewport.ShowDialog() == DialogResult.OK)
            {
                bool IsVisible;

                try
                {
                    m_CurrObject.GetEffectiveVisibilityInViewport(EffectiveVisibilityInViewport.SelectedObject,
                                                                    EffectiveVisibilityInViewport.SelectedViewport,
                                                                    out IsVisible);

                    chxEffectiveVisibilityInViewport.Checked = IsVisible;
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetEffectiveVisibilityInViewport", McEx);
                }
            }
        }

        private void btnShowBasePointsCoordinates_Click(object sender, EventArgs e)
        {
            frmCalculatedCoordinates CalculatedCoordinates = new frmCalculatedCoordinates(m_CurrObject);
            CalculatedCoordinates.ShowDialog();
        }

        private void cbObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbObjects.SelectedIndex >= 0)
                GetGeometryCoordinateSystem(m_lstObjectsItems[cbObjects.SelectedIndex]);
        }

        private void GetGeometryCoordinateSystem(IDNMcObject mcObject = null)
        {
            try
            {
                txtGeometryCoordinateSystem.Text = m_CurrObject.GetGeometryCoordinateSystem(mcObject).ToString();
            }
            catch (MapCoreException McEx)
            {
                if (McEx.ErrorCode == DNEMcErrorCode.INVALID_PARAMETERS)
                    txtGeometryCoordinateSystem.Text = IDNMcErrors.ErrorCodeToString(McEx.ErrorCode);
                
                if(!m_IsInLoadForm)
                    MapCore.Common.Utilities.ShowErrorMessage("GetGeometryCoordinateSystem", McEx);
            }
        }
    }
}
