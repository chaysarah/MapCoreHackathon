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
    public partial class MovementFrm : Form
    {
        static float m_CurrAngle = 0.0f;
        private int MoveFactor3D = 40;
        private object m_ClickedButton;
        private Timer m_ClickedTimer;
        
        public MovementFrm()
        {
            InitializeComponent();
            
            if (MCTMapFormManager.MapForm.Viewport.MapType == DNEMapType._EMT_2D)
            {
                m_LoweringCameraBtn.Enabled = false;
                m_RaisingCameraBtn.Enabled = false;
            }

            m_ClickedTimer = new Timer();
            m_ClickedTimer.Interval = 50;
            m_ClickedTimer.Tick += new EventHandler(m_ClickedTimer_Elapsed);
            this.MouseDown += new MouseEventHandler(ContinuousBtn_MouseDown);
            this.MouseUp += new MouseEventHandler(ContinuousBtn_MouseUp);
        }
        #region Public Properties
        public int MoveMapDelta
        {
            get { return ntxMoveFactor.GetInt32(); }
        }

        public float RotateMapDelta
        {
            get { return ntxRotateFactor.GetFloat(); }
        }

        public float CurrAngle
        {
            get { return m_CurrAngle; }
            set { m_CurrAngle = value; }
        }
        #endregion

        #region Public Method
        public void RotateMap(float RotateDirection)
        {
            try
            {
                CurrAngle += RotateDirection;
                MCTMapFormManager.MapForm.Viewport.ActiveCamera.SetCameraOrientation(CurrAngle, false);

                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(MCTMapFormManager.MapForm.Viewport);
            }
            catch (MapCoreException McEx)
            {
                m_ClickedTimer.Stop();
                MapCore.Common.Utilities.ShowErrorMessage("SetCameraOrientation", McEx);
            }
        }
        //Scrool camera forward\backward and to the sides 
        public void MoveCamera(int deltaX, int deltaY, int deltaZ)
        {
            int deltaX3D = deltaX * MoveFactor3D;
            int deltaY3D = deltaY * MoveFactor3D;
            int deltaZ3D = deltaZ * MoveFactor3D;
            DNSMcVector3D VectorDelta = new DNSMcVector3D(deltaX3D, deltaY3D, deltaZ3D);

            try
            {
                if (MCTMapFormManager.MapForm.Viewport == null)
                {
                    return;
                }
                if (MCTMapFormManager.MapForm.Viewport.MapType == DNEMapType._EMT_2D)
                {
                    if (MCTMapFormManager.MapForm.Viewport.GetImageCalc() != null)
                        MCTMapFormManager.MapForm.Viewport.SetCameraPosition(VectorDelta, true);
                    else
                        MCTMapFormManager.MapForm.Viewport.MoveCameraRelativeToOrientation(VectorDelta, false);

                    // turn on all viewports render needed flags
                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(MCTMapFormManager.MapForm.Viewport);
                }
                if (MCTMapFormManager.MapForm.Viewport.MapType == DNEMapType._EMT_3D)
                {
                    //In 3D map the forward\backward movement on the Y axis is inverted to the 2D map 
                    if (deltaX == 0)
                        deltaY = -deltaY;

                    MCTMapFormManager.MapForm.Viewport.MoveCameraRelativeToOrientation(VectorDelta);

                    // turn on all viewports render needed flags
                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(MCTMapFormManager.MapForm.Viewport);
                }
            }
            catch (MapCoreException McEx)
            {
                m_ClickedTimer.Stop();
                MapCore.Common.Utilities.ShowErrorMessage("MoveCameraRelativeToOrientation", McEx);                
            }
        }
        #endregion

        #region Private Methods

        private void m_ClickedTimer_Elapsed(object sender, EventArgs e)
        {
            string senderText = ((Button)m_ClickedButton).Tag.ToString();
            m_ClickedTimer.Start();
            ExecuteAction(senderText);
        }

        private void ExecuteAction(string senderText)
        {
            switch (senderText)
            {
                case "RotateRight":
                    RotateMap(RotateMapDelta);
                    break;
                case "RotateLeft":
                    RotateMap(-RotateMapDelta);
                    break;
                case "Align":
                    RotateMap(-CurrAngle);
                    break;
                case "Left":
                    MoveCamera(-MoveMapDelta, 0, 0);
                    break;
                case "Right":
                    MoveCamera(MoveMapDelta, 0, 0);
                    break;
                case "Forward":
                    MoveCamera(0, MoveMapDelta, 0);
                    break;
                case "Backward":
                    MoveCamera(0, -MoveMapDelta, 0);
                    break;
                case "Lower":
                    MoveCamera(0, 0, -MoveMapDelta);
                    break;
                case "Raise":
                    MoveCamera(0, 0, MoveMapDelta);
                    break;
            }
        }

        #endregion

        #region Events
                
        void ContinuousBtn_MouseUp(object sender, MouseEventArgs e)
        {
            m_ClickedTimer.Stop();
        }

        void ContinuousBtn_MouseDown(object sender , MouseEventArgs e)
        {
            m_ClickedButton = sender;
            
            float currYaw;
            MCTMapFormManager.MapForm.Viewport.ActiveCamera.GetCameraOrientation(out currYaw);
            m_CurrAngle = currYaw;
           
            if (sender is Button)
            {
                string senderText = ((Button)sender).Tag.ToString();
                m_ClickedTimer.Start();
                ExecuteAction(senderText);
            }            
        }

        #endregion
                
    }
}