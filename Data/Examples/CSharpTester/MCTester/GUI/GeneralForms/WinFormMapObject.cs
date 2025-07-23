using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MCTester.General_Forms;
using UnmanagedWrapper;
using MapCore;

namespace MCTester.GUI.Map
{
    public partial class WinFormMapObject : UserControl, IMapObject
    {
        private MCTMapForm m_MapFormParent;

        public WinFormMapObject(MCTMapForm mapFormParent)
        {
            m_MapFormParent = mapFormParent;
            InitializeComponent();                     
        }

        private void WinFormMapObject_Validated(object sender, EventArgs e)
        {
            // turn on all viewports render needed flags
            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }

        private void WinFormMapObject_Paint(object sender, PaintEventArgs e)
        {
            // turn on all viewports render needed flags
            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
            if(m_MapFormParent!= null)
                m_MapFormParent.CheckRender();
        }

        #region IMapObject Members

        public event MouseMoveEventArgs MouseMoveEvent;

        public event MouseWheelButtonEventArgs MouseWheelEvent;

        public event MouseClickEventArgs MouseClickEvent;

        public event MouseUpEventArgs MouseUpEvent;

        public event MouseDownEventArgs MouseDownEvent;

        public event MouseDoubleClickEventArgs MouseDoubleClickEvent;

        public event PreKeyDownEventArgs PreviewKeyDownEvent;

        public event KeyDownEventArgs KeyDownEvent;

        public event KeyUpEventArgs KeyUpEvent;

       // public event MapControlResizeEventArgs MapControlResizeEvent;
        #endregion

        private void WinFormMapObject_Load(object sender, EventArgs e)
        {
            this.MouseWheel += new MouseEventHandler(WinFormMapObject_MouseWheel);
        }

        private void WinFormMapObject_KeyDown(object sender, KeyEventArgs e)
        {
            m_MapFormParent.KeyDownOnMap(sender, e.KeyCode.ToString(), e.Shift, e.Control);

            if (KeyDownEvent != null)
                KeyDownEvent(sender, e.KeyCode.ToString(), e.Shift, e.Control);
        }
        
        private void WinFormMapObject_KeyUp(object sender, KeyEventArgs e)
        {            
            m_MapFormParent.KeyUpOnMap(sender, e.KeyCode.ToString());

            if (KeyUpEvent != null)
                KeyUpEvent(sender, e.KeyCode.ToString()); 
        }

        private void WinFormMapObject_MouseMove(object sender, MouseEventArgs e)
        {
            m_MapFormParent.MouseMoveOnMap(sender, e.Location, e.Button, e.Delta);
            
            if (MouseMoveEvent != null)
                MouseMoveEvent(sender, e.Location, e.Button, e.Delta);
        }

        void WinFormMapObject_MouseWheel(object sender, MouseEventArgs e)
        {
            m_MapFormParent.MouseWheel(sender, e.Location, e.Delta);

            if (MouseWheelEvent != null)
                MouseWheelEvent(sender, e.Location, e.Delta);
        }

        private void WinFormMapObject_MouseClick(object sender, MouseEventArgs e)
        {
            m_MapFormParent.OnMapClick(sender, e.Location, e.Button);

            if (MouseClickEvent != null)
                MouseClickEvent(sender, e.Location);
        }

        private void WinFormMapObject_MouseDown(object sender, MouseEventArgs e)
        {
            m_MapFormParent.MouseDownOnMap(sender, e.Location, e.Button, e.Delta);

            if (MouseDownEvent != null)
                MouseDownEvent(sender, e.Location, e.Button, e.Delta);
        }

        private void WinFormMapObject_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            m_MapFormParent.MouseDoubleClickOnMap(sender, e.Location, e.Delta);

            if (MouseDoubleClickEvent != null)
                MouseDoubleClickEvent(sender, e.Location, e.Delta);
        }

        private void WinFormMapObject_MouseUp(object sender, MouseEventArgs e)
        {
            m_MapFormParent.MouseUpOnMap(sender, e.Location, e.Delta);

            if (MouseUpEvent != null)
                MouseUpEvent(sender, e.Location, e.Delta);
        }

        private void WinFormMapObject_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            m_MapFormParent.PreviewKeyDownOnMap(sender, e.KeyCode.ToString());

            if (PreviewKeyDownEvent != null)
                PreviewKeyDownEvent(sender, e.KeyCode.ToString());
        }
               
        private void WinFormMapObject_Resize(object sender, EventArgs e)
        {
            if (m_MapFormParent != null)
            {
                IDNMcMapViewport vp = m_MapFormParent.Viewport;
	
	            if (vp != null)
	            {
	                try
	                {
	                    vp.ViewportResized();
	
	                    // turn on all viewports render needed flags
	                    MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(vp.OverlayManager);
	                }
	                catch (MapCoreException McEx)
	                {
	                    MapCore.Common.Utilities.ShowErrorMessage("ViewportResized", McEx);
	                }	
	            }
            }

        }

        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Down || keyData == Keys.Up ||
            keyData == Keys.Left || keyData == Keys.Right)
            {
                return true;
            }
            else
            {
                return base.IsInputKey(keyData);
            }
        }
    }
}
