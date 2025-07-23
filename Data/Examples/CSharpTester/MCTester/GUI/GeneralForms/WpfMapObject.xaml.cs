using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;
using MCTester.GUI.Map;
using MCTester.Managers.MapWorld;
using MapCore;

namespace MCTester.General_Forms
{
    /// <summary>
    /// Interaction logic for WpfMapObject.xaml
    /// </summary>
    public partial class WpfMapObject : UserControl, IMapObject
    {
        // change to true to support Microsoft Remote Desktop (RDP) 
        // note: in this mode, lost device (e.g. after Ctrl+Alt+Del or Win+L) will not be supported even without RDP
        private const bool m_bRDPSupport = false;

        private D3DImage d3dImage;
        private MCTMapForm m_MapFormParent;
        private IntPtr m_BackBuffer;
        private DependencyPropertyChangedEventHandler isFrontBufferChangedHandler;
        private IntPtr m_ViewportRenderSurface;
        private Size m_SurfaceSize;

        public WpfMapObject(MCTMapForm mapFormParent)
        {
            m_MapFormParent = mapFormParent;
            m_BackBuffer = IntPtr.Zero;

            // create a D3DImage to host the scene and
            // monitor it for changes in front buffer availability
            d3dImage = new D3DImage();

            if (!m_bRDPSupport)
            {
                d3dImage.IsFrontBufferAvailableChanged += OnIsFrontBufferAvailableChanged;
            }

            // make a brush of the scene available as a resource on the window
            Resources["MapSurfaceResource"] = new ImageBrush(d3dImage);

            // parse the XAML
            InitializeComponent();

            // begin rendering the custom D3D scene into the D3DImage
            BeginRenderingScene();
        }

        #region Private Events
        private void WpfMapControl_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            Key k = e.ImeProcessedKey;
            System.Windows.Forms.KeysConverter kc = new System.Windows.Forms.KeysConverter();
            string keyString = kc.ConvertToString(e.Key);

            bool IsShiftPressed = Keyboard.IsKeyDown(Key.LeftShift);
            bool IsControlPressed = Keyboard.IsKeyDown(Key.LeftCtrl);

            m_MapFormParent.KeyDownOnMap(sender, keyString, IsShiftPressed, IsControlPressed);

            if (KeyDownEvent != null)
                KeyDownEvent(sender, keyString, IsShiftPressed, IsControlPressed);
        }

        private void WpfMapControl_KeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            System.Windows.Forms.KeysConverter kc = new System.Windows.Forms.KeysConverter();
            string keyString = kc.ConvertToString(e.Key);

            m_MapFormParent.KeyUpOnMap(sender, keyString);

            if (KeyUpEvent != null)
                KeyUpEvent(sender, keyString);
        }

        private System.Windows.Forms.MouseButtons GetMouseButtons(MouseEventArgs e)
        {
            System.Windows.Forms.MouseButtons currMouseButton;

            if (e.LeftButton == MouseButtonState.Pressed)
                currMouseButton = System.Windows.Forms.MouseButtons.Left;
            else if (e.RightButton == MouseButtonState.Pressed)
                currMouseButton = System.Windows.Forms.MouseButtons.Right;
            else if (e.MiddleButton == MouseButtonState.Pressed)
                currMouseButton = System.Windows.Forms.MouseButtons.Middle;
            else
                currMouseButton = System.Windows.Forms.MouseButtons.None;

            return currMouseButton;
        }

        private void WpfMapControl_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.MouseButtons currMouseButton = GetMouseButtons(e);

            int mouseDelta = 1;
            System.Drawing.Point mousePoint = new System.Drawing.Point((int)(e.GetPosition(this).X), (int)(e.GetPosition(this).Y));

            m_MapFormParent.MouseMoveOnMap(sender, mousePoint, currMouseButton, mouseDelta);
            
            if (MouseMoveEvent != null)
                MouseMoveEvent(sender, mousePoint, currMouseButton, mouseDelta);
        }

        private void WpfMapControl_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            Point position = e.GetPosition(null);
            System.Drawing.Point mousePosition = new System.Drawing.Point((int)position.X,(int)position.Y);

            m_MapFormParent.MouseWheel(sender, mousePosition, e.Delta);
            
            if (MouseWheelEvent != null)
                MouseWheelEvent(sender, mousePosition, e.Delta);
        }

        private void WpfMapControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Drawing.Point mousePoint = new System.Drawing.Point((int)(e.GetPosition(this).X), (int)(e.GetPosition(this).Y));
            int mouseDelta = 1;

            System.Windows.Forms.MouseButtons currMouseButton;

            if (e.LeftButton == MouseButtonState.Pressed)
                currMouseButton = System.Windows.Forms.MouseButtons.Left;
            else if (e.RightButton == MouseButtonState.Pressed)
                currMouseButton = System.Windows.Forms.MouseButtons.Right;
            else
                currMouseButton = System.Windows.Forms.MouseButtons.None;

            m_MapFormParent.MouseDownOnMap(sender, mousePoint, currMouseButton, mouseDelta);

            if (MouseDownEvent != null)
                MouseDownEvent(sender, mousePoint, currMouseButton, mouseDelta);
        }

        // Operate on the UserControl itself because the WpfMapControl dos'nt have MouseDoubleClick event
        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            System.Drawing.Point mousePoint = new System.Drawing.Point((int)(e.GetPosition(this).X), (int)(e.GetPosition(this).Y));
            int mouseDelta = 1;

            m_MapFormParent.MouseDoubleClickOnMap(sender, mousePoint, mouseDelta);

            if (MouseDoubleClickEvent != null)
                MouseDoubleClickEvent(sender, mousePoint, mouseDelta);
            
        }

        private void WpfMapControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            System.Drawing.Point mousePoint = new System.Drawing.Point((int)(e.GetPosition(this).X), (int)(e.GetPosition(this).Y));
            int mouseDelta = 1;

            m_MapFormParent.MouseUpOnMap(sender, mousePoint, mouseDelta);

            if (MouseUpEvent != null)
                MouseUpEvent(sender, mousePoint, mouseDelta);
        }

        private void WpfMapControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            System.Windows.Forms.KeysConverter kc = new System.Windows.Forms.KeysConverter();
            string keyString = kc.ConvertToString(e.Key);

            m_MapFormParent.PreviewKeyDownOnMap(sender, keyString);

            if (PreviewKeyDownEvent != null)
                PreviewKeyDownEvent(sender, keyString);
        }
        
        private void WpfMapControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Drawing.Point mousePoint = new System.Drawing.Point((int)(e.GetPosition(this).X), (int)(e.GetPosition(this).Y));
            System.Windows.Forms.MouseButtons currMouseButton = GetMouseButtons(e);

            m_MapFormParent.OnMapClick(sender, mousePoint, currMouseButton);

            if (MouseClickEvent != null)
                MouseClickEvent(sender, mousePoint);
        }

        private void WpfMapControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            m_SurfaceSize = e.NewSize;

            ReInitRenderTarget();            
        }

        #endregion

        #region IMapObject Members

        public new event MouseMoveEventArgs MouseMoveEvent;

        public new event MouseWheelButtonEventArgs MouseWheelEvent;

        public event MouseClickEventArgs MouseClickEvent;

        public new event MouseUpEventArgs MouseUpEvent;

        public new event MouseDownEventArgs MouseDownEvent;

        public new event MouseDoubleClickEventArgs MouseDoubleClickEvent;

        public new event PreKeyDownEventArgs PreviewKeyDownEvent;

        public new event KeyDownEventArgs KeyDownEvent;

        public new event KeyUpEventArgs KeyUpEvent;
        
        //public new event MapControlResizeEventArgs MapControlResizeEvent;
        #endregion


        private void BeginRenderingScene()
        {
            if (m_bRDPSupport || d3dImage.IsFrontBufferAvailable)
            {
                if (m_MapFormParent != null && m_MapFormParent.Viewport != null)
                {
                    try
                    {
                        // create a custom D3D scene and get a pointer to its surface
                        // (this is a call into our custom unmanaged library)
                        //_scene = InitializeScene();
                        m_ViewportRenderSurface = m_MapFormParent.Viewport.GetRenderSurface();

                        // set the back buffer using the new scene
                        d3dImage.Lock();
                        d3dImage.SetBackBuffer(D3DResourceType.IDirect3DSurface9, m_ViewportRenderSurface, m_bRDPSupport);
                        d3dImage.Unlock();

                        // leverage the Rendering event of WPF's composition target to
                        // update the custom D3D scene
                        CompositionTarget.Rendering += OnRendering;	
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("GetRenderSurface", McEx);
                    }
                }
            }
        }

        private void OnIsFrontBufferAvailableChanged(object sender,DependencyPropertyChangedEventArgs e)
        {
            // if the front buffer is available, then WPF has just created a new
            // D3D device, so we need to start rendering our custom scene
            if (d3dImage.IsFrontBufferAvailable)
            {
                ReInitRenderTarget();            
                BeginRenderingScene();
            }
            else
            {
                // If the front buffer is no longer available, then WPF has lost 
                // its D3D device so there is no reason to waste cycles rendering
                // our custom scene until a new device is created.
                StopRenderingScene();
            }
        }

        private void StopRenderingScene()
        {
            // This method is called when WPF loses its D3D device.
            // In such a circumstance, it is very likely that we have lost 
            // our custom D3D device also, so we should just release the scene.
            // We will create a new scene when a D3D device becomes 
            // available again.
            CompositionTarget.Rendering -= OnRendering;

            // release the scene
            m_ViewportRenderSurface = IntPtr.Zero;
        }

        private void OnRendering(object sender, EventArgs e)
        {
            // when WPF's composition target is about to render, we update our 
            // custom render target so that it can be blended with the WPF target
            UpdateScene();
        }

        public void UpdateScene()
        {
            if ((d3dImage != null) &&
                (m_bRDPSupport || d3dImage.IsFrontBufferAvailable) &&
                (m_ViewportRenderSurface != IntPtr.Zero))
            {
                //// lock the D3DImage
                //d3dImage.Lock();

                //// Force Render the map.
                //Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
                ////MCTMapFormManager.MapForm.Viewport.Render();

                //// invalidate the updated region of the D3DImage (in this case, the whole image)
                //d3dImage.AddDirtyRect(new Int32Rect(0, 0, (int)d3dImage.PixelWidth, (int)d3dImage.PixelHeight));

                //// unlock the D3DImage
                //d3dImage.Unlock();

                MCTMapFormManager.MapForm.RenderMap();
            }
        }

        private void ReInitRenderTarget()
        {
            if (m_SurfaceSize.Width == 0 || m_SurfaceSize.Height ==0)
                return;

            try
            {
                d3dImage.Lock();
                d3dImage.SetBackBuffer(D3DResourceType.IDirect3DSurface9, new IntPtr(0), m_bRDPSupport);

                IntPtr newSurface = IntPtr.Zero;
                // Resize the rendering surface and get the new one.  
                if (m_MapFormParent.Viewport != null)
                {
                    m_MapFormParent.Viewport.ResizeRenderSurface((uint)m_SurfaceSize.Width, (uint)m_SurfaceSize.Height, out newSurface);
                }
                //else
                //{
                //}
                if (newSurface != IntPtr.Zero)
                {
                    // A new render surface has arrived.
                    m_ViewportRenderSurface = newSurface;

                    // set the back buffer using the new scene pointer
                    d3dImage.SetBackBuffer(D3DResourceType.IDirect3DSurface9, m_ViewportRenderSurface, m_bRDPSupport);

                }

                d3dImage.Unlock();

                // turn on viewport render needed flag
                Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();

            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("ResizeRenderSurface", McEx);
            }                 
        }

        public void LoadMap()
        {
            // begin rendering the custom D3D scene into the D3DImage
            BeginRenderingScene();

            // parse the XAML
            InitializeComponent();

            // Create the front buffer availability event handler and subscribe.
            d3dImage.IsFrontBufferAvailableChanged += isFrontBufferChangedHandler;

            // make a brush of the scene available as a resource on the window
            //Resources["MapSurfaceResource"] = new ImageBrush(d3dImage);
            this.WpfMapControl.Background = new ImageBrush(d3dImage);

            // force control focus in order to activates mouse events
            WpfMapControl.Focus();            
        }

        public D3DImage D3DImage
        {
            get { return d3dImage; }            
        }
    }
}
