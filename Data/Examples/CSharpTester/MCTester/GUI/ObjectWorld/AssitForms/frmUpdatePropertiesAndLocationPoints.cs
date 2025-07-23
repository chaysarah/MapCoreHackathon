using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UnmanagedWrapper;
using MapCore;
using MapCore.Common;
using MCTester.ObjectWorld.ObjectsUserControls;

namespace MCTester.ObjectWorld.Assit_Forms
{
    public partial class frmUpdatePropertiesAndLocationPoints : Form
    {
        private enum ActionType { Update,Get }

        private ActionType m_ActionType;
        private List<DNSMcVector3D> m_NewLocationPoints;
        private uint m_StartIndex;
        private uint m_LocationIndex;
        private bool m_IsOKButtonClicked;
        private IDNMcObject m_CurrObject;
        private List<DNSVariantProperty> m_ChangedPropertiesList;
        private DNSMcVector3D[] m_LocationPoints;
        private frmPropertiesIDList m_FrmPropertiesIDList;
        public frmUpdatePropertiesAndLocationPoints()
        {
            InitializeComponent();
            m_NewLocationPoints = new List<DNSMcVector3D>();
            m_IsOKButtonClicked = false;
        }

        public frmUpdatePropertiesAndLocationPoints(IDNMcObject _currObject, List<DNSVariantProperty> _changedPropertiesList) :this()
        {
            m_CurrObject = _currObject;
            m_ChangedPropertiesList = _changedPropertiesList;
            m_ActionType = ActionType.Update;
            SetLocationGeometricType(0);
        }

        public frmUpdatePropertiesAndLocationPoints(IDNMcObject _currObject, List<DNSVariantProperty> _changedPropertiesList, DNSMcVector3D[] paLocationPoints) : this(_currObject, _changedPropertiesList)
        {
            m_ActionType = ActionType.Get;
            m_LocationPoints = paLocationPoints;
            LoadPoints(m_LocationPoints);

            pnlLocations.Visible = false;
            ctrlNewLocationPoints.ChangeReadOnly();
        }

        public void SetFrmPropertiesIDList(frmPropertiesIDList frmPropertiesIDList)
        {
            m_FrmPropertiesIDList = frmPropertiesIDList;
        }

        private void SetLocationGeometricType(uint locationIndex)
        {
            try
            {
                ctrlNewLocationPoints.SetPointCoordSystem(m_CurrObject.GetScheme().GetObjectLocation(locationIndex).GetGeometryCoordinateSystem(m_CurrObject));
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetObjectLocation", McEx);
            }

        }

        private void LoadPoints(DNSMcVector3D[] mLocationPoints)
        {
            ctrlNewLocationPoints.SetPoints(mLocationPoints);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                DNSMcVector3D[] points;
                if (ctrlNewLocationPoints.GetPoints(out points))
                {
                    StartIndex = ntxStartIndex.GetUInt32();
                    LocationIndex = ntxLocationPointsIndex.GetUInt32();
                    try
                    {
                        m_CurrObject.UpdatePropertiesAndLocationPoints(m_ChangedPropertiesList.ToArray(),
                                                                        points,
                                                                        StartIndex,
                                                                        LocationIndex);

                        m_IsOKButtonClicked = true;
                        this.Close();
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("UpdatePropertiesAndLocationPoints", McEx);
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("UpdateLocationPoints", McEx);
            }
        }

        public uint StartIndex
        {
            get { return m_StartIndex; }
            set { m_StartIndex = value; }
        }

        public uint LocationIndex
        {
            get { return m_LocationIndex; }
            set { m_LocationIndex = value; }
        }

        private void frmUpdatePropertiesAndLocationPoints_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_IsOKButtonClicked == true)
            {
                this.DialogResult = DialogResult.OK;
            }
            else if (e.CloseReason == CloseReason.None)
            {
                e.Cancel = true;
            }
            else
            {
                if (m_ActionType == ActionType.Update)
                {
                    DialogResult dlgReasult = MessageBox.Show("Closing form will cause losing data end exit the function\nWould you like to continue?", "Form Closing", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (DialogResult.Yes == dlgReasult)
                    {
                        this.DialogResult = DialogResult.Abort;
                    }
                    else
                        e.Cancel = true;
                }
            }               
        }

        private void btnGetLocationPoints_Click(object sender, EventArgs e)
        {
            try
            {
                DNSVariantProperty[] paProperties = new DNSVariantProperty[0];
                DNSMcVector3D[] paLocationPoints = new DNSMcVector3D[0];
                m_CurrObject.GetPropertiesAndLocationPoints(out paProperties, out paLocationPoints, LocationIndex);

                if (paLocationPoints != null)
                {
                    LoadPoints(paLocationPoints);
                    SetLocationGeometricType(LocationIndex);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetPropertiesAndLocationPoints", McEx);
            }
        }

        private void frmUpdatePropertiesAndLocationPoints_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(this.DialogResult == DialogResult.OK && m_FrmPropertiesIDList != null)
            {
                m_FrmPropertiesIDList.SetIsChangedAllGrid(false);
            }
        }
    }
}