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

namespace MCTester.GUI.Forms
{
    public partial class EventCallBackForm : Form, IDNEditModeCallback
    {
        IDNMcEditMode m_cuurEditMode;
        ListView m_CurrEventList;

        public EventCallBackForm()
        {
            InitializeComponent();
            this.TopMost = true;
            m_CurrEventList = new ListView();

            m_cuurEditMode = MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.EditMode;

            MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.EditModeManagerCallback.SetEventsCallback(this);
            
        }

        #region Public Methods
        
        public void AddEventToList(string eventName)
        {
            if (this != null)
            {
                int eventIndex = lsvEventCallBack.Items.Count + 1;
                ListViewItem newItem = new ListViewItem(); //(eventIndex.ToString() + "-" + eventName);
                
                newItem.Text = eventIndex.ToString();
                newItem.SubItems.Add(eventName);

                if (eventName.Contains("ExitAction"))
                    newItem.BackColor = Color.LightGray;
                 
                lsvEventCallBack.Items.Add(newItem);
                newItem.EnsureVisible();

            }
        }

        #endregion

        private void btnClearEventList_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to save content to a file\n before erasion?","Clear List",
                MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SaveFileDialog SFD = new SaveFileDialog();
                SFD.Filter = "Word Document (*.doc)|*.*";
                SFD.FileName = "EventCallBackList.doc";

                if (SFD.ShowDialog() == DialogResult.OK)
                {
                    string sEventText = "";
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(SFD.FileName);

                    foreach (ListViewItem item in lsvEventCallBack.Items)
                    {
                        sEventText = sEventText + "\r\n" + item.Text + "    " + item.SubItems[1].Text;
                    }  

                    sw.Close();
                }                
            }

            lsvEventCallBack.Items.Clear();
        }


        #region IDNEditModeCallback Members

        public void ExitAction(int nExitCode)
        {
            string eventText;

			
				eventText = "EditMode - ExitAction(), exit code:" + nExitCode.ToString();

            this.AddEventToList(eventText);
            MCTMapFormManager.MapForm.RenderMap();
        }

        public void NewVertex(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, DNSMcVector3D WorldVertex, DNSMcVector3D ScreenVertex, uint uVertexIndex, double dAngle)
        {
            string eventText = "EditMode - NewVertex() " +
                " Item Code:" + Manager_MCNames.GetIdByObject(pItem) +
                " Vertex index:" + uVertexIndex.ToString() +
                " World(x:" + Math.Round(WorldVertex.x).ToString() +
                " y:" + Math.Round(WorldVertex.y).ToString() +
                " z:" + Math.Round(WorldVertex.z).ToString() +
                ") Screen(x:" + Math.Round(ScreenVertex.x).ToString() +
                " y:" + Math.Round(ScreenVertex.y).ToString() +
                ") Angle:" + dAngle.ToString();

            this.AddEventToList(eventText);
            MCTMapFormManager.MapForm.RenderMap();
        }

        public void PointDeleted(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, DNSMcVector3D WorldVertex, DNSMcVector3D ScreenVertex, uint uVertexIndex)
        {
            string eventText = "EditMode - PointDeleted() " +
                " Item Code:" + Manager_MCNames.GetIdByObject(pItem) +
                " Vertex Index:" + uVertexIndex.ToString() +
                " World(x:" + Math.Round(WorldVertex.x).ToString() +
                           " y:" + Math.Round(WorldVertex.y).ToString() +
                           " z:" + Math.Round(WorldVertex.z).ToString() +
                ") Screen(x:" + Math.Round(ScreenVertex.x).ToString() +
                           " y:" + Math.Round(ScreenVertex.y).ToString() + ")";

            this.AddEventToList(eventText);
            MCTMapFormManager.MapForm.RenderMap();
        }

        public void PointNewPos(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, DNSMcVector3D WorldVertex, DNSMcVector3D ScreenVertex, uint uVertexIndex, double dAngle, bool bDownOnHeadPoint)
        {
            string eventText = "EditMode - PointNewPos() " +
                " Item Code:" + Manager_MCNames.GetIdByObject(pItem) +
                " Vertex Index:" + uVertexIndex.ToString() +
                " World(x:" + Math.Round(WorldVertex.x).ToString() +
                           " y:" + Math.Round(WorldVertex.y).ToString() +
                           " z:" + Math.Round(WorldVertex.z).ToString() +
                ") Screen(x:" + Math.Round(ScreenVertex.x).ToString() +
                           " y:" + Math.Round(ScreenVertex.y).ToString() +
                ") Angle:" + dAngle.ToString() +
                " DownOnHeadPoint:" + bDownOnHeadPoint.ToString();

            this.AddEventToList(eventText);
            MCTMapFormManager.MapForm.RenderMap();
        }

        public void ActiveIconChanged(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, DNEPermission eIconPermission, uint uIconIndex)
        {
            string eventText = "EditMode - ActiveIconChanged() " +
                " Item Code:" + Manager_MCNames.GetIdByObject(pItem) +
                " Icon Permission:" + eIconPermission.ToString() +
                " Icon Index:" + uIconIndex.ToString();

            this.AddEventToList(eventText);
            MCTMapFormManager.MapForm.RenderMap();
        }

        public void InitItemResults(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, int nExitCode)
        {
            string eventText = "EditMode - InitItemResults() " +
                " Item Code:" + Manager_MCNames.GetIdByObject(pItem) +
                " exit code:" + nExitCode.ToString();

            this.AddEventToList(eventText);
            MCTMapFormManager.MapForm.RenderMap();
        }

        public void EditItemResults(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, int nExitCode)
        {
            string eventText = "EditMode - EditItemResults() " +
                " Item Code:" + Manager_MCNames.GetIdByObject(pItem) +
                " exit code:" + nExitCode.ToString();

            this.AddEventToList(eventText);
            MCTMapFormManager.MapForm.RenderMap();
        }

		public void DragMapResults(IDNMcMapViewport pViewport, DNSMcVector3D NewCenter)
        {
            string eventText = "EditMode - DragMapResults() " +
                                " Viewport:" + pViewport.ToString() +
                                " New Center(x:" + Math.Round(NewCenter.x).ToString() +
                                " y:" + Math.Round(NewCenter.y).ToString() +
                                " z:" + Math.Round(NewCenter.z).ToString();

            this.AddEventToList(eventText);
            MCTMapFormManager.MapForm.RenderMap();
        }

		public void RotateMapResults(IDNMcMapViewport pViewport, float fNewYaw, float fNewPitch)
        {
            string eventText = "EditMode - RotateMapResults() " +
                " Viewport:" + pViewport.ToString() +
                " New Yaw:" + fNewYaw.ToString() +
                " New Pitch:" + fNewPitch.ToString();

            this.AddEventToList(eventText);
            MCTMapFormManager.MapForm.RenderMap();
        }

		public void DistanceDirectionMeasureResults(IDNMcMapViewport pViewport, DNSMcVector3D WorldVertex1, DNSMcVector3D WorldVertex2, double dDistance, double dAngle)
        {
            string eventText = "EditMode - DistanceDirectionMeasureResults() " +
                           " world vertex1(x:" + Math.Round(WorldVertex1.x).ToString() +
                           " y:" + Math.Round(WorldVertex1.y).ToString() +
                           " z:" + Math.Round(WorldVertex1.z).ToString() +
                           ") world vertex2(x:" + Math.Round(WorldVertex2.x).ToString() +
                           " y:" + Math.Round(WorldVertex2.y).ToString() +
                           " z:" + Math.Round(WorldVertex2.z).ToString() +
                           ") distance:" + Math.Round(dDistance).ToString() +
                           " angle:" + dAngle.ToString();

            this.AddEventToList(eventText);
            MCTMapFormManager.MapForm.RenderMap();
        }

		public void DynamicZoomResults(IDNMcMapViewport pViewport, float fNewScale, DNSMcVector3D NewCenter)
        {
            string eventText = "EditMode - DynamicZoomResults() " +
                " Viewport:" + pViewport.ToString() +
                " New Scale:" + fNewScale.ToString() +
                " New Center(x:" + Math.Round(NewCenter.x).ToString() +
                           " y:" + Math.Round(NewCenter.y).ToString() + 
                           " z:" + Math.Round(NewCenter.z).ToString() + ")";

            this.AddEventToList(eventText);
            MCTMapFormManager.MapForm.RenderMap();

            //if (TesterCallbackObject != null)
            //{
            //    ((MapCore.GUI.MCButtons.EditMode.DynamicZoom)TesterCallbackObject).OnDeactivate();
            //    TesterCallbackObject = null;
            //}
        }

		public void CalculateHeightResults(IDNMcMapViewport pViewport, double dHeight, DNSMcVector3D[] coords, int status)
        {
            string eventText = null;

            try
            {
                eventText = "EditMode - CalculateHeightResults()\n" +
                "Map Code:" + Manager_MCNames.GetIdByObject(pViewport) + "\n" +
                "Returned Height: " + dHeight.ToString() + "\n";

                int CoordNum = 1;
                foreach (DNSMcVector3D currCord in coords)
                {
                    eventText += "-------------------------------------------\n";
                    eventText += "Coord Number " + CoordNum.ToString() + "\n";
                    eventText += "X=" + currCord.x.ToString() + ",Y=" + currCord.y.ToString() + ",Z=" + currCord.z.ToString() + "\n";
                }
            }
            catch
            {
                eventText = null;
            }
            finally
            {
                if (eventText != null)
                {
                    this.AddEventToList(eventText);
                    MCTMapFormManager.MapForm.RenderMap();
                    MessageBox.Show(eventText, "HeightCallback", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

		public void CalculateVolumeResults(IDNMcMapViewport pViewport, double dVolume, DNSMcVector3D[] coords, int status)
        {
            string eventText = null;

            try
            {
                eventText = "EditMode - CalculateVolumeResults()\n" +
                "Map Code:" + Manager_MCNames.GetIdByObject(pViewport) + "\n" +
                "Returned Volume: " + dVolume.ToString() + "\n";

                int CoordNum = 1;
                foreach (DNSMcVector3D currCord in coords)
                {
                    eventText += "-------------------------------------------\n";
                    eventText += "Coord Number " + CoordNum.ToString() + "\n";
                    eventText += "X=" + currCord.x.ToString() + ",Y=" + currCord.y.ToString() + ",Z=" + currCord.z.ToString() + "\n";
                }
            }
            catch
            {
                eventText = null;
            }
            finally
            {
                if (eventText != null)
                {
                    this.AddEventToList(eventText);
                    MCTMapFormManager.MapForm.RenderMap();
                    MessageBox.Show(eventText, "HeightCallback", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        #endregion

        private void EventCallBackForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.EditModeManagerCallback.UnregisterEventsCallback(this);
        }
    }
}