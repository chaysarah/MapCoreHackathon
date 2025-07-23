using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapCore;
using System.IO;
using MCTester.Managers.ObjectWorld;
using MapCore.Common;

namespace MCTester.Controls
{
    public partial class CtrlPointsGrid : UserControl
    {
        public delegate void delegateUpdatePoints();
        public delegate void delegateSelectionChanged(bool isSelectRow, bool isSelectConsistencyRows);

        int m_colNum = 0;
        int m_colX= 1;
        int m_colY = 2;
        int m_colZ = 3;
        bool m_IsHideZ = false;
        private delegateUpdatePoints m_delegateUpdatePoints;
        private delegateSelectionChanged m_delegateSelectionChanged;

        public CtrlPointsGrid()
        {
            InitializeComponent();
            ctrlSampleLocationPoints.SetXYZColumnsIndexes(m_colX, m_colY, m_colZ);
        }

        public void ChangeSize(int numRows)
        {
            int dgvLocationPointsHeight = 20 + numRows * 20;
            dgvLocationPoints.Size = new Size(dgvLocationPoints.Size.Width, dgvLocationPointsHeight);
            int buttonLocationY = dgvLocationPointsHeight + 5;
            btnClearDGV.Location = new Point(btnClearDGV.Location.X, buttonLocationY);
            btnImportLocationPointsFromCSV.Location = new Point(btnImportLocationPointsFromCSV.Location.X, buttonLocationY);
            btnExportLocationPointsFromCSV.Location = new Point(btnExportLocationPointsFromCSV.Location.X, buttonLocationY);
            ctrlSampleLocationPoints.Location = new Point(ctrlSampleLocationPoints.Location.X, buttonLocationY);
        }

        public void SetUpdateDelegate(delegateUpdatePoints _delegateUpdatePoints)
        {
            m_delegateUpdatePoints = _delegateUpdatePoints;
        }

        public void SetSelectionChanged(delegateSelectionChanged _delegateSelectionChanged)
        {
            m_delegateSelectionChanged = _delegateSelectionChanged;
        }

        public void ChangeReadOnly()
        {
            dgvLocationPoints.ReadOnly = true;
            btnImportLocationPointsFromCSV.Visible = ctrlSampleLocationPoints.Visible = btnClearDGV.Visible = dgvLocationPoints.AllowUserToAddRows = false;
        }

        public void HideZ()
        {
            m_IsHideZ = true;
            dgvLocationPoints.Columns[m_colZ].Visible = false;
            //dgvLocationPoints.Size = new Size(dgvLocationPoints.Size.Width - 100, dgvLocationPoints.Size.Height);
        }

        private void ShowErrorMessage(string msg, string title)
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public bool GetSelectedPoints(out DNSMcVector3D[] locationPoints, out uint startIndex)
        {
            startIndex = 0;
            DataGridViewSelectedRowCollection selectedRowCollection = dgvLocationPoints.SelectedRows;
            int pointsCount = selectedRowCollection.Count;
            if (pointsCount == 0)
            {
                MessageBox.Show("error - should select rows");
                locationPoints = null; 
                return false;
            }
            else
            {
                locationPoints = new DNSMcVector3D[pointsCount];
                for (int i = 0; i < pointsCount; i++)
                {
                    if (!dgvLocationPoints.Rows[i].IsNewRow)
                    {
                        int index = selectedRowCollection[i].Index;
                        if (i == 0)
                        startIndex = (uint)index;

                    
                        DNSMcVector3D point;
                        if (CheckRowValue(i, out point))
                        {
                            locationPoints[i] = point;
                        }
                    }

                }
            }
            return true;
        }

        private bool CheckRowValue(int row, out DNSMcVector3D point)
        {
            point = DNSMcVector3D.v3Zero;
            double resultX, resultY, resultZ = 0;
            if(CheckCellValue(m_colX, row, "X", out resultX) && 
                CheckCellValue(m_colY, row, "Y", out resultY) && 
                (m_IsHideZ || CheckCellValue(m_colZ, row, "Z", out resultZ)))
            {
                point = new DNSMcVector3D(resultX, resultY, resultZ);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckCellValue(int col, int row, string colName, out double result)
        {
            bool isParseSucc;
            result = 0;

            if (dgvLocationPoints[col, row].Value != null)
            {
                isParseSucc = double.TryParse(dgvLocationPoints[col, row].Value.ToString(), out result);
                if (!isParseSucc)
                {
                    ShowErrorMessage(colName + " value in line " + row.ToString() + " is invalid", "Get Points");
                    return false;
                }
            }
            else
            {
                ShowErrorMessage(colName + " value in line " + row.ToString() + " is null", "Get Points");
                return false;
            }
            return true;
        }

        public bool GetPoints(out DNSMcVector3D[] locationPoints, IDNMcMapViewport viewport = null)
        {

            if (dgvLocationPoints.RowCount > 1)
            {
                locationPoints = new DNSMcVector3D[dgvLocationPoints.RowCount - 1];
            }
            else
            {
                if (dgvLocationPoints.RowCount == 0 || dgvLocationPoints.Rows[0].IsNewRow)
                    locationPoints = new DNSMcVector3D[0];
                else
                    locationPoints = new DNSMcVector3D[1];
            }

            for (int i = 0; i < dgvLocationPoints.RowCount; i++)
            {
                if (!dgvLocationPoints.Rows[i].IsNewRow)
                {
                    DNSMcVector3D point;
                    if (CheckRowValue(i, out point))
                    {
                        if (viewport != null)
                        {
                            try
                            {
                                locationPoints[i] = viewport.OverlayManagerToViewportWorld(point);
                            }
                            catch (MapCoreException McEx)
                            {
                                Utilities.ShowErrorMessage("OverlayManagerToViewportWorld", McEx);
                                return false;
                            }
                        }
                        else
                            locationPoints[i] = point;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        internal void SetPointCoordSystem(DNEMcPointCoordSystem dNEMcPointCoordSystem)
        {
            ctrlSampleLocationPoints.PointCoordSystem = dNEMcPointCoordSystem; 
        }

        internal void SetIsRelativeToDTM(bool _IsRelativeToDTM)
        {
            ctrlSampleLocationPoints._IsRelativeToDTM = _IsRelativeToDTM;
        }

        public void ClearDGV()
        {
            dgvLocationPoints.Rows.Clear();
            dgvLocationPoints.CurrentCell = dgvLocationPoints[0, 0];
            dgvLocationPoints.Select();
        }

        private void btnImportLocationPointsFromCSV_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                char[] delimeters = new char[1] {','};
                try
                {
                    string[] readerData = File.ReadAllLines(ofd.FileName);
                    if (readerData != null)
                    {
                        dgvLocationPoints.Rows.Clear();
                        for (int i = 0; i < readerData.Length; i++)
                        {
                            string[] line = readerData[i].Split(delimeters);
                            if (line.Length > 0)
                            {
                                string strX = line[0];
                                string strY = line.Length > 1 ? line[1] : "";
                                string strZ = line.Length > 2 ? line[2] : "";

                                if (strX == null || strY == null || strX == "" || strY == "" /*|| strZ == null || strZ == ""*/)
                                {
                                    MessageBox.Show("Import location points from file failed.\nThe data '" + readerData[i].ToString() + "' in line " + i + " is invalid",
                                               "Import Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                double resultX = 0, resultY = 0, resultZ = 0;
                                bool IsParseSuccX = double.TryParse(strX, out resultX);
                                bool IsParseSuccY = double.TryParse(strY, out resultY);
                                bool IsParseSuccZ = double.TryParse(strZ, out resultZ);
                                if (IsParseSuccX && IsParseSuccY)
                                {
                                    dgvLocationPoints.Rows.Add(i, strX, strY, strZ);
                                }
                                else
                                {

                                    MessageBox.Show("Import location points from file failed.\nThe data '" + readerData[i].ToString() + "' is invalid",
                                               "Import Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;

                                }
                            }
                        }

                        if (m_delegateUpdatePoints != null)
                            m_delegateUpdatePoints();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Import Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
        }

        private void btnClearDGV_Click(object sender, EventArgs e)
        {
            ClearDGV();
            if (m_delegateUpdatePoints != null)
                m_delegateUpdatePoints();
        }


        private void btnExportLocationPointsToCSV_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV File (*.csv) | *.csv";
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StreamWriter stw = new StreamWriter(sfd.FileName);
                string locationPoints = "";

                for (int i = 0; i < dgvLocationPoints.Rows.Count - 1; i++)
                {
                    if (!dgvLocationPoints.Rows[i].IsNewRow)
                    {
                        object objX, objY, objZ;
                        objX = dgvLocationPoints[m_colX, i].Value;
                        objY = dgvLocationPoints[m_colY, i].Value;
                        objZ = dgvLocationPoints[m_colZ, i].Value;

                        if (objX == null)
                        {
                            MessageBox.Show("X value in line " + i.ToString() + " is null.\nPlease fix it and try again",
                                                                      "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            stw.Close();
                            return;
                        }
                        if (objY == null)
                        {
                            MessageBox.Show("Y value in line " + i.ToString() + " is null.\nPlease fix it and try again",
                                                                      "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            stw.Close();
                            return;
                        }
                        if (!m_IsHideZ && objZ == null)
                        {
                            MessageBox.Show("Z value in line " + i.ToString() + " is null.\nPlease fix it and try again",
                                                                      "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            stw.Close();
                            return;
                        }

                        locationPoints = objX.ToString() + "," +
                                         objY.ToString()+ "," +
                                        (objZ == null ? "0" : objZ.ToString());
                        
                        stw.WriteLine(locationPoints);
                    }

                }
               

                stw.Close();
            }
        }

        internal void SetPoints(DNSMcVector3D[] aPoints)
        {
            dgvLocationPoints.Rows.Clear();
            if (aPoints != null)
            {
                for (int i = 0; i < aPoints.Length; i++)
                    dgvLocationPoints.Rows.Add(i.ToString(), aPoints[i].x, aPoints[i].y, aPoints[i].z);
            }
        }

        private void dgvLocationPoints_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dgvLocationPoints.Rows[e.RowIndex].Cells[m_colNum].Value = (e.RowIndex).ToString();

        }


        KeyEventArgs keyEventArgs;

        private void dgvLocationPoints_KeyDown(object sender, KeyEventArgs e)
        {
            keyEventArgs = e;
        }

        private void dgvLocationPoints_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if user clicked Shift+Ins or Ctrl+V (paste from clipboard)
            if (keyEventArgs.Control && keyEventArgs.KeyCode == Keys.V)
            {
                ObjectPropertiesBase.CopyDataFromClipboard(dgvLocationPoints, true);
            }
        }


        private void dgvLocationPoints_KeyUp(object sender, KeyEventArgs e)
        {
            keyEventArgs = null;
        }

        private void tsmiSelectAll_Click(object sender, EventArgs e)
        {
            dgvLocationPoints.SelectAll();
        }

        private void tsmiCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(
                   this.dgvLocationPoints.GetClipboardContent());
        }

        private void tsmiPaste_Click(object sender, EventArgs e)
        {
            ObjectPropertiesBase.CopyDataFromClipboard(dgvLocationPoints, true);
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            int insertIndex = GetSelectedRowIndex();
            if (insertIndex >= 0 && insertIndex < dgvLocationPoints.Rows.Count)
            {
                dgvLocationPoints.Rows.RemoveAt(insertIndex);
            }
        }

        public int GetSelectedRowIndex()
        {
            DataGridViewSelectedRowCollection selectedRowCollection = dgvLocationPoints.SelectedRows;
            int pointsCount = selectedRowCollection.Count;

            if (pointsCount == 1)
            {
                int index = selectedRowCollection[0].Index;
                if (index >= 0 && index < dgvLocationPoints.Rows.Count)
                    return index;
                else
                {
                    MessageBox.Show("error - invalid select row index");
                    return -1;
                }
            }
            else
            {
                MessageBox.Show("error - should select one row");
                return -1;
            }
        }

        internal void SelectGrid()
        {
            dgvLocationPoints.Select();
        }

        internal int GetSelectedRowsCount()
        {
            return dgvLocationPoints.SelectedRows.Count;
        }

        private void dgvLocationPoints_SelectionChanged(object sender, EventArgs e)
        {
            bool isSelectRows = false;
            int selectedRowsCount = dgvLocationPoints.SelectedRows.Count;

            // check if consistency select rows
            if (selectedRowsCount > 1)
            {
                DataGridViewRow[] dataGridViewRows = new DataGridViewRow[dgvLocationPoints.SelectedRows.Count];
                dgvLocationPoints.SelectedRows.CopyTo(dataGridViewRows, 0); ;
                List<DataGridViewRow> dataGridViews = new List<DataGridViewRow>(dataGridViewRows);
                List<int> mSelectedRowsIndexes = dataGridViews.Select(x => x.Index).ToList();
                mSelectedRowsIndexes.Remove(dgvLocationPoints.Rows.Count - 1);
                mSelectedRowsIndexes.Sort();
                if (mSelectedRowsIndexes.Count > 1)
                {
                    isSelectRows = true;
                    for (int i = 0; i < mSelectedRowsIndexes.Count - 1; i++)
                    {
                        int index = mSelectedRowsIndexes[i];
                        int nextIndex = mSelectedRowsIndexes[i + 1];
                        if (index + 1 != nextIndex)
                        {
                            isSelectRows = false;
                            break;
                        }
                    }
                }
            }
            if (m_delegateSelectionChanged != null)
                m_delegateSelectionChanged(selectedRowsCount == 1, isSelectRows);
        }

      
    }
}
