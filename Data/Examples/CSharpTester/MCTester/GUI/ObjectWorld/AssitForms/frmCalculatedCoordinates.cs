using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers;
using MCTester.Managers.MapWorld;
using MCTester.Managers.ObjectWorld;

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class frmCalculatedCoordinates : Form
    {
        IDNMcObjectSchemeNode mObjectSchemeNode;
        IDNMcSymbolicItem mSymbolicItem;
        IDNMcObject[] mObjects;
        ParentFormType mParentFormType;
        bool mIsLoadPoints = false;

        public enum ParentFormType { objectschemenode, symbolicitem }

        public frmCalculatedCoordinates()
        {
            InitializeComponent();
            cmbPointCoordSys.Items.AddRange(Enum.GetNames(typeof(DNEMcPointCoordSystem)));
            ctrlPointsGrid1.ChangeReadOnly();
        }

        public frmCalculatedCoordinates(IDNMcObjectSchemeNode objectSchemeNode) : this()
        {
            mObjectSchemeNode = objectSchemeNode;
            mParentFormType = ParentFormType.objectschemenode;
            this.Text = "Base Points Coordinates";
            cmbPointCoordSys.Text = DNEMcPointCoordSystem._EPCS_WORLD.ToString();
            LoadObjects(mObjectSchemeNode);
            chxWithAddedPoints.Visible = false;
        }

        public frmCalculatedCoordinates(IDNMcSymbolicItem symbolicItem) : this()
        {
            chxWithAddedPoints.Visible = true;
            mSymbolicItem = symbolicItem;
            mParentFormType = ParentFormType.symbolicitem;
            this.Text = "Calculated Points Coordinates";
            label16.Text = "Point Coord System";
            LoadObjects(mSymbolicItem);
            cmbPointCoordSys.Enabled = false;
        }

        private void LoadObjects(IDNMcObjectSchemeNode objectSchemeNode)
        {
            if (objectSchemeNode.GetScheme() != null)
            {
                mObjects = objectSchemeNode.GetScheme().GetObjects();
                foreach (IDNMcObject obj in mObjects)
                    lstObjects.Items.Add(Manager_MCNames.GetNameByObject(obj));
                if (mObjects.Length == 1)
                    lstObjects.SelectedIndex = 0;
            }
        }

        private void btnCalcCoordOK_Click(object sender, EventArgs e)
        {
            ShowResults();
        }

        private void ShowResults()
        {
            if (lstObjects.SelectedItem != null)
            {
                IDNMcObject mcObject = mObjects[lstObjects.SelectedIndex];
                IDNMcMapViewport activeViewport = MCTMapFormManager.MapForm.Viewport;

                if (mParentFormType == ParentFormType.symbolicitem)
                {
                    try
                    {
                        DNEMcPointCoordSystem pointsCoordSys;
                        DNSMcVector3D[] nodeCalcPoints = null;
                        uint[] indices = null;
                        if (chxWithAddedPoints.Checked)
                        {
                             mSymbolicItem.GetAllCalculatedPoints(activeViewport, mcObject, out nodeCalcPoints, out pointsCoordSys);
                        }
                        else
                        {
                           mSymbolicItem.GetAllCalculatedPoints(activeViewport, mcObject, out nodeCalcPoints, out pointsCoordSys,out indices);
                        }
                        mIsLoadPoints = true;
                        cmbPointCoordSys.Text = pointsCoordSys.ToString();
                        LoadPoints(nodeCalcPoints, indices);
                        mIsLoadPoints = false;
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("GetAllCalculatedPoints", McEx);
                    }
                }
                else
                {
                    try
                    {
                        DNEMcPointCoordSystem pointsCoordSys = (DNEMcPointCoordSystem)Enum.Parse(typeof(DNEMcPointCoordSystem), cmbPointCoordSys.Text);
                        DNSMcVector3D[] nodeCalcPoints = mObjectSchemeNode.GetCoordinates(activeViewport, pointsCoordSys, mcObject);

                        LoadPoints(nodeCalcPoints);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("GetCoordinates", McEx);
                    }
                }
            }
        }

        private void LoadPoints(DNSMcVector3D[] nodeCalcPoints, uint[] indices = null)
        {
            int numPoints = 0;

            if (indices == null)
            {
                ctrlPointsGrid1.SetPoints(nodeCalcPoints);
                if (nodeCalcPoints != null)
                {
                    numPoints = nodeCalcPoints.Length;
                }
            }
            if (indices != null)
            {
                DNSMcVector3D[] newPoints = new DNSMcVector3D[indices.Length];
                for (int i = 0; i < indices.Length; i++)
                {
                    newPoints[i] = nodeCalcPoints[indices[i]];
                }
                ctrlPointsGrid1.SetPoints(newPoints);
                numPoints = indices.Length;
            }

            txtNoPoints.Text = numPoints.ToString();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        KeyEventArgs keyEventArgs;

        private void dgvNodeCalcCoordinates_KeyUp(object sender, KeyEventArgs e)
        {
            keyEventArgs = null;
        }

        private void dgvNodeCalcCoordinates_KeyDown(object sender, KeyEventArgs e)
        {
            keyEventArgs = e;
        }

        private void lstObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowResults();
        }

        private void cmbPointCoordSys_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!mIsLoadPoints)
                ShowResults();
        }

        private void chxIndices_CheckedChanged(object sender, EventArgs e)
        {
            if (!mIsLoadPoints)
                ShowResults();
        }
    }
}