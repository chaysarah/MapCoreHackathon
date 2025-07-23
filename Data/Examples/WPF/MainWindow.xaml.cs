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
using UnmanagedWrapper;
using MapCore;

namespace WPFMapCoreExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region private members

        // change to true to support Microsoft Remote Desktop (RDP) 
        // note: in this mode, lost device (e.g. after Ctrl+Alt+Del or Win+L) will not be supported even without RDP
        private const bool m_bRDPSupport = false;

        private D3DImage d3dImage;
        private IntPtr m_ViewportRenderSurface;
        private IDNMcMapViewport mapViewport2d;
        private Size m_SurfaceSize;
        private IDNMcEditMode activeEditMode;
        private IDNMcOverlayManager overlayMngr;
        private IDNMcOverlay overlay;
        private bool m_bWaitForScan = false;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }

        #region MapCore open map
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            // 1. Define a coordinate system for the maps (layers and terrain).
            // The coordinate system must be the same for all layers in the same viewport,
            // Different Viewports can be from different coord sys.
            IDNMcGridUTM coordSys = DNMcGridUTM.Create(36, DNEDatumType._EDT_ED50_ISRAEL);

#if USE_RAW_IMAGES // define USE_RAW_IMAGES if you want to use TIFF, JPG, PNG, BMP etc.
                   // Important, raw images should have their geographical data as well (externally or internally)
            // 2a. add terrain with raw raster (native layers are much easier)
            DNSComponentParams CompParams = new DNSComponentParams();

            // ENTER YOUR RAW FILE'S PATH HERE
            CompParams.strName = <Put your raster file name here>;
            CompParams.eType = DNEComponentType._ECT_FILE;
            // if it is a GeoTiff or a tif+tfw then WorldLimit is not necessary and input v3MaxDouble
            CompParams.WorldLimit = new MapCore.DNSMcBox(DNSMcVector3D.v3MaxDouble, DNSMcVector3D.v3MaxDouble);

            DNSComponentParams[] arrCompParams = new DNSComponentParams[] { CompParams };

            DNSRawParams dnParams = new DNSRawParams();
            dnParams.pCoordinateSystem = coordSys;
            dnParams.fMaxScale = float.MaxValue;
            dnParams.strDirectory = null;
            dnParams.aComponents = arrCompParams;
            dnParams.uMaxNumOpenFiles = 0;
            dnParams.fFirstPyramidResolution = 0.0F;
            dnParams.auPyramidResolutions = null;

            IDNMcRawRasterMapLayer rasterLayer = DNMcRawRasterMapLayer.Create(dnParams);
#else

            // 2b. add terrain with native raster 
            IDNMcNativeRasterMapLayer rasterLayer =
            DNMcNativeRasterMapLayer.Create(@"C:\maps\Israel\Raster");

            IDNMcMapLayer[] layers = new IDNMcMapLayer[] { rasterLayer };
            IDNMcMapTerrain mapTerrain = DNMcMapTerrain.Create(coordSys, layers);
#endif


            // 3. If you need graphical objects create an overlay manager and at least one overlay
            overlayMngr = DNMcOverlayManager.Create(coordSys);
            overlay = DNMcOverlay.Create(overlayMngr);

            // 4. Create a graphical device
            DNSInitParams InitParams = new DNSInitParams();

            // for WPF multi-threading is a must, with WinForms it is optional
            InitParams.bMultiThreadedDevice = true;
            IDNMcMapDevice device = DNMcMapDevice.Create(InitParams);

            // 5. create the viewport
            IDNMcMapCamera mapCamera = null;
            DNSCreateDataMV paramsVP = new DNSCreateDataMV(DNEMapType._EMT_2D);

            // when using WPF the window handle is IntPtr.Zero
            paramsVP.hWnd = IntPtr.Zero;
            paramsVP.pDevice = device;
            paramsVP.CoordinateSystem = coordSys;
            paramsVP.pOverlayManager = overlayMngr;
            paramsVP.uHeight = (uint)m_SurfaceSize.Height;
            paramsVP.uWidth = (uint)m_SurfaceSize.Width;
            mapViewport2d = null;
            mapCamera = null;
            DNMcMapViewport.Create(ref mapViewport2d, ref mapCamera, paramsVP, new IDNMcMapTerrain[] { mapTerrain });

            // 6. Optional: Create an edit mode 
            activeEditMode = DNMcEditMode.Create(mapViewport2d);

            // 7. Set a D3DImage for WPF
            InitD3dImage();
        }
#endregion

#region WPF special D3DImage usage
        private void InitD3dImage()
        {
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

            // make a brush of the scene available as a resource on the window
            //Resources["MapSurfaceResource"] = new ImageBrush(d3dImage);
            MapCanvas.Background = new ImageBrush(d3dImage);


        }

        private void BeginRenderingScene()
        {
            if (m_bRDPSupport || d3dImage.IsFrontBufferAvailable)
            {
                // create a custom D3D scene and get a pointer to its surface
                // (this is a call into our custom unmanaged library)
                //_scene = InitializeScene();
                m_ViewportRenderSurface = mapViewport2d.GetRenderSurface();

                // set the back buffer using the new scene
                d3dImage.Lock();
                d3dImage.SetBackBuffer(D3DResourceType.IDirect3DSurface9, m_ViewportRenderSurface, m_bRDPSupport);
                d3dImage.Unlock();

                // leverage the Rendering event of WPF's composition target to
                // update the custom D3D scene
                CompositionTarget.Rendering += OnRendering;
            }
        }

        private void OnIsFrontBufferAvailableChanged(object sender, DependencyPropertyChangedEventArgs e)
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
                // lock the D3DImage
                d3dImage.Lock();

                // Force Render the map.
                try
                {
                    mapViewport2d.Render();
                }
                catch (MapCoreException McEx)
                {
                }

                // invalidate the updated region of the D3DImage (in this case, the whole image)
                d3dImage.AddDirtyRect(new System.Windows.Int32Rect(0, 0, (int)m_SurfaceSize.Width, (int)m_SurfaceSize.Height));

                // unlock the D3DImage
                d3dImage.Unlock();
            }
        }

        private void ReInitRenderTarget()
        {
            if (m_SurfaceSize.Width == 0 || m_SurfaceSize.Height == 0 || d3dImage == null)
                return;

            d3dImage.Lock();
            d3dImage.SetBackBuffer(D3DResourceType.IDirect3DSurface9, new IntPtr(0), m_bRDPSupport);

            IntPtr newSurface = IntPtr.Zero;
            // Resize the rendering surface and get the new one.                
            mapViewport2d.ResizeRenderSurface((uint)m_SurfaceSize.Width, (uint)m_SurfaceSize.Height, out newSurface);

            if (newSurface != IntPtr.Zero)
            {
                // A new render surface has arrived.
                m_ViewportRenderSurface = newSurface;

                // set the back buffer using the new scene pointer
                d3dImage.SetBackBuffer(D3DResourceType.IDirect3DSurface9, m_ViewportRenderSurface, m_bRDPSupport);

            }

            d3dImage.Unlock();

        }

        private void MapCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            m_SurfaceSize = e.NewSize;

            ReInitRenderTarget();
        }

#endregion

#region MouseEvents

        // All events are caught by the application and sent to MapCore's EditMode interface via API
        private void MapCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            MCEvent(DNEMouseEventEditMode._EME_EM_MOUSE_WHEEL, e.Delta);

            // Zoom In / Out using mouse wheel, zooming factor can be set according to application's wish
            float fCamScale = mapViewport2d.GetCameraScale();
            float scaleFactor = 0.0005f * e.Delta;
            if (scaleFactor > 0.5f)
                scaleFactor = 0.5f;

            if (scaleFactor < -0.5f)
                scaleFactor = -0.5f;

            fCamScale -= scaleFactor * fCamScale;

            if (fCamScale > float.MinValue && fCamScale < float.MaxValue)
            {
                mapViewport2d.SetCameraScale(fCamScale);
            }
        }

        private void MapCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MCEvent(DNEMouseEventEditMode._EME_EM_BUTTON_RELEASED);
        }

        private void MapCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                MCEvent(DNEMouseEventEditMode._EME_EM_MOUSE_MOVED_BUTTON_DOWN);
            }
            else
            {
                MCEvent(DNEMouseEventEditMode._EME_EM_MOUSE_MOVED_BUTTON_UP);
            }
        }


        private void MapCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                MCEvent(DNEMouseEventEditMode._EME_EM_BUTTON_DOUBLE_CLICK);
            }
            else
            {
                MCEvent(DNEMouseEventEditMode._EME_EM_BUTTON_PRESSED);
            }
        }


        private void MCEvent(DNEMouseEventEditMode eEvent)
        {
            MCEvent(eEvent, 0);
        }


        // handle events in MapCore
        private void MCEvent(DNEMouseEventEditMode eEvent, int Delta)
        {
            // Print Coordinates to status bar
            PrintCoord();

            // used for editing existing objects
            if (m_bWaitForScan == true && eEvent == DNEMouseEventEditMode._EME_EM_BUTTON_PRESSED)
            {
                ScanAndEdit();
            }
            else if (activeEditMode != null && activeEditMode.IsEditingActive)
            {
                bool isRenderNeeded;
                DNECursorTypeEditMode cursorType;

                //API for notifying MapCore of the event
                activeEditMode.OnMouseEvent(eEvent, GetMousePos(), Keyboard.Modifiers == ModifierKeys.Control,
                   (short)Delta, out isRenderNeeded, out cursorType);

            }
            // Add application events handling logic
        }

        private DNSMcPoint GetMousePos()
        {
            Point point = Mouse.GetPosition(MapCanvas);
            return new DNSMcPoint((int)point.X, (int)point.Y);
        }


        private void PrintCoord()
        {
            if (mapViewport2d != null)
            {
                DNSMcVector3D world = new DNSMcVector3D(0, 0, 0);
                Point screen = Mouse.GetPosition(MapCanvas);
                DNSMcVector3D ScreenVec = new DNSMcVector3D(screen.X, screen.Y, 0);
                bool bIntersect = false;

                // If the terrain has a DTM layer than get the coordinate including height
                //                 mapViewport2d.ScreenToWorldOnTerrain(ScreenVec, out  world, out  bIntersect);

                // If no DTM available or heights are not needed use :
                mapViewport2d.ScreenToWorldOnPlane(ScreenVec, out world, out bIntersect);

                textBox1.Text = ((int)world.x).ToString() + " " + ((int)world.y).ToString() + " " + ((int)world.z).ToString();
            }
        }


        //Scans (Hit Test) for any objects under the mouse point
        // If found edit them using MapCore's EditMode
        private void ScanAndEdit()
        {
            m_bWaitForScan = false;

            // find mouse point
            DNSMcVector3D pointItem = new DNSMcVector3D((Double)GetMousePos().X, (Double)GetMousePos().Y, 0);

            // scan under the mouse point with 2 pixels tolerance
            DNSMcScanPointGeometry scanPointGeometry = new DNSMcScanPointGeometry(DNEMcPointCoordSystem._EPCS_SCREEN, pointItem, 2);
            DNSTargetFound[] TargetFound = mapViewport2d.ScanInGeometry(scanPointGeometry, false);

            if (TargetFound.Length > 0)
            {
                // if any objects found, send the 1st one to editing
                activeEditMode.StartEditObject(TargetFound[0].ObjectItemData.pObject, TargetFound[0].ObjectItemData.pItem);
            }

        }

#endregion

#region Map Operations
        // Use Edit Mode to navigate map
        private void btnDragMap_Click(object sender, RoutedEventArgs e)
        {
            activeEditMode.StartNavigateMap(false);
        }

        // Use Edit Mode to calculate bearing and distance 
        private void btnAzDist_Click(object sender, RoutedEventArgs e)
        {
            activeEditMode.StartDistanceDirectionMeasure();
        }

#endregion

#region graphic items

        // Use Edit Mode to draw a line
        private void btnLine_Click(object sender, RoutedEventArgs e)
        {
            // create a line item
            IDNMcLineItem Item = DNMcLineItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN | DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN);

            //create a graphical object consisting of the line item
            IDNMcObject mcObj = DNMcObject.Create(overlay, Item, DNEMcPointCoordSystem._EPCS_WORLD, new DNSMcVector3D[] { }, true);

            // set line color to red
            Item.SetLineColor(new DNSMcBColor(255, 0, 0, 255), 1, false);

            // set line width to 4 pixels
            Item.SetLineWidth(4, 2, false);

            // use edit mode
            activeEditMode.StartInitObject(mcObj, Item);
        }

        // Use Edit Mode to draw a rectangle
        private void btnRect_Click(object sender, RoutedEventArgs e)
        {
            // create an object consisting of a rect item and use edit mode
            IDNMcRectangleItem Item = DNMcRectangleItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN, DNEMcPointCoordSystem._EPCS_WORLD);
            IDNMcObject mcObj = DNMcObject.Create(overlay, Item, DNEMcPointCoordSystem._EPCS_WORLD, new DNSMcVector3D[] { }, true);
            activeEditMode.StartInitObject(mcObj, Item);
        }

        // Use Edit Mode to edit existing items
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            m_bWaitForScan = true;
        }

        private void LineOffset_Click(object sender, RoutedEventArgs e)
        {
            // Create an object scheme containing of 1 object location (the root of the scheme graph) 
            IDNMcObjectLocation mcLocation = null;
            IDNMcObjectScheme mcScheme = DNMcObjectScheme.Create(ref mcLocation, overlayMngr, 
                DNEMcPointCoordSystem._EPCS_WORLD, true);
            
            // create an empty item (invisible but allows transformations)
            IDNMcEmptySymbolicItem mcEmpty = DNMcEmptySymbolicItem.Create();

            // connect the empty item to the root-location
            mcEmpty.Connect(mcLocation);

            // specify the empty item transformation: geographic and its rotation relative to the item
            //  the offset is fDistane forward to the orientation
            float fDistance = 50; //meters because offset type is geographic
            mcEmpty.SetOffsetType(DNEItemGeometryType._EGT_GEOGRAPHIC);
            mcEmpty.SetOffsetOrientation(DNEOffsetOrientation._EOO_RELATIVE_TO_ITEM_ROTATION, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false); // TBD: Bracha
            mcEmpty.SetOffset(new DNSMcFVector3D(0, fDistance, 0), 0, false);

            // set the empty item rotation, this effects the offset because it was set as _EOO_RELATIVE_TO_ITEM_ROTATION
            float fAzimuth = 20; // degrees clockwise from north
            mcEmpty.SetRotationYaw(fAzimuth, 1, false);

            // create a line item using defaults
            IDNMcLineItem mcLine = DNMcLineItem.Create(DNEItemSubTypeFlags._EISTF_WORLD);

            // connect the line item to the root location AND the empty item which was transfomed
            // from the root at fAzimuth by fDistance
            mcLine.Connect(new DNMcObjectSchemeNode[] { (DNMcObjectSchemeNode)mcLocation, (DNMcObjectSchemeNode)mcEmpty });

            // create the object using the existing scheme, enter a valid coordinate. in this case
            // it is the map center.
            // If you want to use edit mode, you need a new empty item and connect it directly
            // to the root location with no offsets. this item will be sent to edit mode
            IDNMcObject mcObj = DNMcObject.Create(overlay, mcScheme, new DNSMcVector3D[] { mapViewport2d.GetCameraPosition() });

        }
#endregion

    }
}
