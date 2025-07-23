using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;
using System.Windows.Forms;
using MCTester.GUI;
using System.Drawing;
using MCTester.GUI.Map;

namespace MCTester.Managers.MapWorld
{
    public static class Manager_StatusBar
    {
        static StatusStrip StatusBar;
        static StatusStrip StatusBarStatistics;

        static Manager_StatusBar()
        {
            StatusBar = MainForm.MainFormStatusBar;
            StatusBarStatistics = MainForm.MainFormStatusBarStatistics;

            MCTMapForm.OnMapClicked += new ClickOnMapEventArgs(MCTMapForm_OnMapClicked);
        }

        static void MCTMapForm_OnMapClicked(Point PointOnMap, DNSMcVector3D PointIn3D, DNSMcVector3D PointInImage, bool IsHasIntersection)
        {
            if (IsHasIntersection)
            {
                WorldCoordX.Text = (Math.Round(PointIn3D.x, 2)).ToString();
                WorldCoordY.Text = (Math.Round(PointIn3D.y, 2)).ToString();
                WorldCoordZ.Text = (Math.Round(PointIn3D.z, 2)).ToString();
            }
            else
            {
                WorldCoordX.Text = "";
                WorldCoordY.Text = "";
                WorldCoordZ.Text = "";
            }
            ScreenPt.Text = PointOnMap.X.ToString() + "/" + PointOnMap.Y.ToString();
            ImagePt.Text = ((int)PointInImage.x).ToString() + "/" + ((int)PointInImage.y).ToString();
            if (MCTMapFormManager.MapForm != null && MCTMapFormManager.MapForm.Viewport != null)
            {
                ViewportID.Text = MCTMapFormManager.MapForm.Viewport.ViewportID.ToString();
                UpdateScale();
            }
        }

        public static void UpdateScale()
        {
            try
            {
                float newScale = 0.0F;
                if (MCTMapFormManager.MapForm.Viewport.MapType == DNEMapType._EMT_3D)
                {
                    if (WorldCoordX.Text != "" && WorldCoordY.Text != "" && WorldCoordZ.Text != "")
                        newScale = MCTMapFormManager.MapForm.Viewport.GetCameraScale(new DNSMcVector3D(double.Parse(WorldCoordX.Text), double.Parse(WorldCoordY.Text), double.Parse(WorldCoordZ.Text)));
                }
                else
                    newScale = MCTMapFormManager.MapForm.Viewport.GetCameraScale();

                //New style - scale in unit per pixel
                ScaleBox.Text = "1:" + Math.Round((double)newScale, 3); 

                //Old style - scale as customary in paper maps
                MapScaleBox.Text = "1:" + (newScale / MCTMapFormManager.MapForm.Viewport.GetPixelPhysicalHeight()).ToString();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetCameraScale", McEx);
            }            
        }

        public static void UpdateAverageFPS(double newFPS)
        {
            AvrFPS.Text = ((int)newFPS).ToString();
        }

        public static void UpdateMsg(string Msg)
        {
            MsgBox.Text = Msg;
        }

        public static void UpdateStatisticsStatusBar(DNSRenderStatistics statistics)
        {
            StatusBarStatistics.Items["sbLastFPSBox"].Text = statistics.fLastFPS.ToString();
            StatusBarStatistics.Items["sbAvgFPSBox"].Text = statistics.fAverageFPS.ToString();
            StatusBarStatistics.Items["sbBestFPSBox"].Text = statistics.fBestFPS.ToString();
            StatusBarStatistics.Items["sbWorstFPSBox"].Text = statistics.fWorstFPS.ToString();
            StatusBarStatistics.Items["sbNumLastFrameTrianglesBox"].Text = statistics.uNumLastFrameTriangles.ToString();
            StatusBarStatistics.Items["sbNumLastFrameBatchesBox"].Text = statistics.uNumLastFrameBatches.ToString();
        }
        
        #region Public Properties
        public static ToolStripItem WorldCoordX
        {
            get { return StatusBar.Items["sbWorldCoordXBox"]; }
        }

        public static ToolStripItem WorldCoordY
        {
            get { return StatusBar.Items["sbWorldCoordYBox"]; }
        }

        public static ToolStripItem WorldCoordZ
        {
            get { return StatusBar.Items["sbWorldCoordZBox"]; }
        }

        public static ToolStripItem ScreenPt
        {
            get { return StatusBar.Items["sbScreenCoordBox"]; }
        }

        public static ToolStripItem ImagePt
        {
            get { return StatusBar.Items["sbImageCoordBox"]; }
        }

        public static ToolStripItem ViewportID
        {
            get { return StatusBar.Items["sbViewportIDBox"]; }
        }

        public static ToolStripItem ScaleBox
        {
            get { return StatusBar.Items["sbScaleBox"]; }
        }

        public static ToolStripItem MapScaleBox
        {
            get { return StatusBar.Items["sbMapScaleBox"]; }
        }

        public static ToolStripItem AvrFPS
        {
            get { return StatusBar.Items["sbAvrFPSBox"]; }
        }

        public static ToolStripItem MsgBox
        {
            get { return StatusBar.Items["sbMsgBox"]; }
        }

        public static StatusStrip MainStatusBarStatistics
        {
            get { return StatusBarStatistics; }
        }
        
        #endregion 
    
    }
}
