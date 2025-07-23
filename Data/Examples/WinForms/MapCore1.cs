using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UnmanagedWrapper;
using MapCore;

namespace MapCoreWinFormExample
{

    public partial class MapCore1 : Form
    {
        public MapCore1()
        {
            InitializeComponent();
        }

        #region private members
        private IDNMcMapViewport mapViewport2d;
        private IDNMcMapViewport mapViewport3d;
        private IDNMcEditMode editMode2d;
        private IDNMcEditMode editMode3d;
        private IDNMcMapViewport activeViewport;
        private IDNMcEditMode activeEditMode;
        private IDNMcOverlay overlay;
        private bool m_bWaitForScan;
        #endregion

        #region MapCore open map
        private void btnOpenMap_Click(object sender, EventArgs e)
        {
            // 1. Define a coordinate system for the maps (layers and terrain).
            // The coordinate system must be the same for all layers in the same viewport,
            // Different Viewports can be from different coord sys.
            IDNMcGridUTM coordSys = DNMcGridUTM.Create(36, DNEDatumType._EDT_ED50_ISRAEL);

            // 2. add terrain with native raster and DTM 
            IDNMcNativeRasterMapLayer rasterLayer =
                DNMcNativeRasterMapLayer.Create(@"C:\maps\Israel\Raster");
            IDNMcNativeDtmMapLayer dtmLayer = DNMcNativeDtmMapLayer.Create(@"C:\maps\Israel\Dtm");

            IDNMcMapLayer[] layers = new IDNMcMapLayer[] { rasterLayer, dtmLayer };
            IDNMcMapTerrain mapTerrain = DNMcMapTerrain.Create(coordSys, layers);

            // 3. If you need graphical objects create an overlay manager and at least one overlay
            IDNMcOverlayManager overlayMngr = DNMcOverlayManager.Create(coordSys);
            overlay = DNMcOverlay.Create(overlayMngr);


            // 4 + 5. Create a graphical device and then create the viewports
            IDNMcMapCamera mapCamera = null;
            DNSCreateDataMV paramsVP = new DNSCreateDataMV(DNEMapType._EMT_2D);
            paramsVP.hWnd = splitContainer.Panel1.Handle;
            paramsVP.pDevice = DNMcMapDevice.Create(new DNSInitParams()); ;
            paramsVP.CoordinateSystem = coordSys;
            paramsVP.pOverlayManager = overlayMngr;

            // create the 2D viewport
            DNMcMapViewport.Create(ref mapViewport2d, ref mapCamera, paramsVP, new IDNMcMapTerrain[] { mapTerrain });

            // create the 3D viewport, only change the flag
            paramsVP.eMapType = DNEMapType._EMT_3D;
            paramsVP.hWnd = splitContainer.Panel2.Handle;
            DNMcMapViewport.Create(ref mapViewport3d, ref mapCamera, paramsVP, new IDNMcMapTerrain[] { mapTerrain });

            activeViewport = mapViewport2d;

            // 7. Position the 2D and 3D map in the center of the raster layer, 1Km above the ground
            DNSMcVector3D rasterCtr = rasterLayer.BoundingBox.CenterPoint();
            DNSMcVector3D new2DPos = new DNSMcVector3D(
                rasterCtr.x, rasterCtr.y, mapViewport2d.GetCameraPosition().z + 1000.0);
            mapViewport2d.SetCameraPosition(new2DPos);
            mapViewport3d.SetCameraPosition(new2DPos);
            // The following line rotates the 3D camera to look at the map center
            mapViewport3d.SetCameraLookAtPoint(rasterCtr);

            // 8. Optional: Create an edit mode 
            editMode2d = DNMcEditMode.Create(mapViewport2d);
            editMode3d = DNMcEditMode.Create(mapViewport3d);
            activeEditMode = editMode2d;
            m_bWaitForScan = false;

            // 9. start render timer
            renderTimer.Enabled = true;

            PrintScale();
        }
        private void renderTimer_Tick(object sender, EventArgs e)
        {
            // Render the viewports
            mapViewport2d.Render();
            mapViewport3d.Render();
        }
        #endregion

        #region MouseEvents

        // All events are caught by the application and sent to MapCore's EditMode interface via API
        void Panel1_MouseUp(object sender, MouseEventArgs e)
        {
            MCEvent(1, DNEMouseEventEditMode._EME_EM_BUTTON_RELEASED, e.Delta);
        }

        void Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MCEvent(1, DNEMouseEventEditMode._EME_EM_MOUSE_MOVED_BUTTON_DOWN, e.Delta);
            }
            else
            {
                MCEvent(1, DNEMouseEventEditMode._EME_EM_MOUSE_MOVED_BUTTON_UP, e.Delta);
            }
        }
        void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            activeViewport = mapViewport2d;
            activeEditMode = editMode2d;
            MCEvent(1, DNEMouseEventEditMode._EME_EM_BUTTON_PRESSED, e.Delta);
        }

        private void splitContainer1_Panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MCEvent(1, DNEMouseEventEditMode._EME_EM_BUTTON_DOUBLE_CLICK, e.Delta);
        }

        void Panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MCEvent(2, DNEMouseEventEditMode._EME_EM_MOUSE_MOVED_BUTTON_DOWN, e.Delta);
            }
            else
            {
                MCEvent(2, DNEMouseEventEditMode._EME_EM_MOUSE_MOVED_BUTTON_UP, e.Delta);
            }
        }
        void Panel2_MouseUp(object sender, MouseEventArgs e)
        {
            MCEvent(2, DNEMouseEventEditMode._EME_EM_BUTTON_RELEASED, e.Delta);
        }
        void Panel2_MouseDown(object sender, MouseEventArgs e)
        {
            activeViewport = mapViewport3d;
            activeEditMode = editMode3d;
            MCEvent(2, DNEMouseEventEditMode._EME_EM_BUTTON_PRESSED, e.Delta);
        }


        private void splitContainer1_Panel2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MCEvent(2, DNEMouseEventEditMode._EME_EM_BUTTON_DOUBLE_CLICK, e.Delta);
        }


        // handle events in MapCore
        private void MCEvent(int nPanel, DNEMouseEventEditMode eEvent, int Delta)
        {
            // Print Coordinates to status bar
            PrintCoord(nPanel);

            // used for editing existing objects
            if (m_bWaitForScan == true && eEvent == DNEMouseEventEditMode._EME_EM_BUTTON_PRESSED)
            {
                ScanAndEdit(nPanel);
            }
            else if (activeEditMode != null && activeEditMode.IsEditingActive)
            {
                bool isRenderNeeded;
                DNECursorTypeEditMode cursorType;

                //API for notifying MapCore of the event
                activeEditMode.OnMouseEvent(eEvent, GetMousePos(nPanel), ModifierKeys.Equals(Keys.Control),
                   (short)Delta, out isRenderNeeded, out cursorType);
            }
            // Add application events handling logic
        }

        private DNSMcPoint GetMousePos(int nPanel)
        {
            if (nPanel == 1)
            {
                return new DNSMcPoint(splitContainer.Panel1.PointToClient(MousePosition));
            }
            else
            {
                return new DNSMcPoint(splitContainer.Panel2.PointToClient(MousePosition));
            }
        }

        // Print Coordinates to status bar
        void PrintCoord(int nPanel)
        {
            if (activeViewport != null)
            {
                DNSMcVector3D world = new DNSMcVector3D(0, 0, 0);
                DNSMcPoint screen = GetMousePos(nPanel);
                DNSMcVector3D ScreenVec = new DNSMcVector3D(screen.X, screen.Y, 0);
                bool bIntersect = false;
                activeViewport.ScreenToWorldOnTerrain(ScreenVec, out world, out bIntersect);

                lblCoords.Text = "X: " + ((int)world.x).ToString() + " Y: " + ((int)world.y).ToString() + " Z: " + ((int)world.z).ToString();


            }
        }

        void PrintScale()
        {
            if (activeViewport != null)
            {
                float f = activeViewport.GetCameraScale();
                lblScale.Text = "   Scale: " + ((int)f).ToString();
            }
        }

        //Scans (Hit Test) for any objects under the mouse point
        // If found edit them using MapCore's EditMode
        private void ScanAndEdit(int nPanel)
        {
            m_bWaitForScan = false;

            // find mouse point
            DNSMcVector3D pointItem = new DNSMcVector3D((Double)GetMousePos(nPanel).X, (Double)GetMousePos(nPanel).Y, 0);

            // scan under the mouse point with 2 pixels tolerance
            DNSMcScanPointGeometry scanPointGeometry = new DNSMcScanPointGeometry(DNEMcPointCoordSystem._EPCS_SCREEN, pointItem, 2);
            DNSTargetFound[] TargetFound = activeViewport.ScanInGeometry(scanPointGeometry, false);

            if (TargetFound.Length > 0)
            {
                // if any objects found, send the 1st one to editing
                activeEditMode.StartEditObject(TargetFound[0].ObjectItemData.pObject, TargetFound[0].ObjectItemData.pItem);
            }

        }


        #endregion

        #region MapActions

        const double MAX_QUERY_INTERSECTION_DISTANCE = 20000.0;

        // Zoom in
        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            bool bFound = false;
            DNMcNullableOut<DNSMcVector3D> pInterectionPoint = null, pNormal = null;
            DNMcNullableOut<double> dblDistance = new DNMcNullableOut<double>();

            if (activeViewport == mapViewport2d)
            {
                // For 2D simply changing the scale
                activeViewport.SetCameraScale(activeViewport.GetCameraScale() * 0.8f);
                PrintScale();
            }
            else
            {
                // For 3D, move along the line of sight by 10% of the distance to ground
                DNSMcVector3D lineOfSight = activeViewport.GetCameraForwardVector();
                activeViewport.GetRayIntersection(
                    activeViewport.GetCameraPosition(), lineOfSight,
                    MAX_QUERY_INTERSECTION_DISTANCE, out bFound,
                    pInterectionPoint, pNormal, dblDistance);
                if (bFound && dblDistance.Value > 1.0)
                {
                    lineOfSight = new DNSMcVector3D(0, 0.1 * dblDistance.Value, 0.0);
                    activeViewport.MoveCameraRelativeToOrientation(lineOfSight);
                }
            }
        }

        // Zoom out
        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            bool bFound = false;
            DNMcNullableOut<DNSMcVector3D> pInterectionPoint = null, pNormal = null;
            DNMcNullableOut<double> dblDistance = new DNMcNullableOut<double>();

            if (activeViewport == mapViewport2d)
            {
                activeViewport.SetCameraScale(activeViewport.GetCameraScale() * 1.25f);
                PrintScale();
            }
            else
            {
                // For 3D, move opposite to the line of sight by 10% of the distance to ground
                DNSMcVector3D lineOfSight = activeViewport.GetCameraForwardVector();
                activeViewport.GetRayIntersection(
                    activeViewport.GetCameraPosition(), lineOfSight,
                    MAX_QUERY_INTERSECTION_DISTANCE, out bFound,
                    pInterectionPoint, pNormal, dblDistance);
                if (bFound)
                {
                    lineOfSight = new DNSMcVector3D(0, -0.1 * dblDistance.Value, 0.0);
                    activeViewport.MoveCameraRelativeToOrientation(lineOfSight);
                }
            }
        }

        // Use Edit Mode to navigate map
        private void btnNavigate_Click(object sender, EventArgs e)
        {
            activeEditMode.StartNavigateMap(false);
        }

        // Use Edit Mode to calculate bearing and distance 
        private void btnMeasure_Click(object sender, EventArgs e)
        {
            activeEditMode.StartDistanceDirectionMeasure();
        }
        #endregion

        #region graphic items

        // Use Edit Mode to draw a line
        private void btnLine_Click(object sender, EventArgs e)
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
        private void btnAddRectangle_Click(object sender, EventArgs e)
        {
            // create an object consisting of a rect item and use edit mode
            IDNMcRectangleItem Item = DNMcRectangleItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN, DNEMcPointCoordSystem._EPCS_WORLD);
            IDNMcObject mcObj = DNMcObject.Create(overlay, Item, DNEMcPointCoordSystem._EPCS_WORLD, new DNSMcVector3D[] { }, true);
            activeEditMode.StartInitObject(mcObj, Item);
        }

        // Use Edit Mode to draw an ellipse which shows line of sights
        private void toolEllipseButton1_Click(object sender, EventArgs e)
        {
            // create an ellipse item
            IDNMcEllipseItem Item = DNMcEllipseItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN, DNEMcPointCoordSystem._EPCS_WORLD);

            //create a graphical object consisting of the ellipse item
            IDNMcObject mcObj = DNMcObject.Create(overlay, Item, DNEMcPointCoordSystem._EPCS_WORLD, new DNSMcVector3D[] { }, true);

            // set the ellipse to show sight presentation using GPU calculations
            Item.SetSightPresentationType(DNESightPresentationType._ESPT_GPU, 1, false);

            // set seen areas to be red with transparency
            Item.SetSightColor(DNEPointVisibility._EPV_SEEN, new DNSMcBColor(255, 0, 0, 150), 2, false);

            // set unseen areas to be green with transparency
            Item.SetSightColor(DNEPointVisibility._EPV_UNSEEN, new DNSMcBColor(0, 255, 0, 150), 3, false);

            // use edit mode
            activeEditMode.StartInitObject(mcObj, Item);
        }

        // Use Edit Mode to edit existing items
        private void btnEdit_Click(object sender, EventArgs e)
        {
            m_bWaitForScan = true;
        }

        private void btnBrightness_Click(object sender, EventArgs e)
        {
            //             activeViewport.SetEnableImageProcessing(true);
            // TBD Pavel: add per-layer processing
            activeViewport.SetColorTableBrightness(null, DNEColorChannel._ECC_MULTI_CHANNEL, 0.25);

        }

        #endregion

        private void btnOffsetIcon_Click(object sender, EventArgs e)
        {
            // Create an object scheme containing of 1 object location (the root of the scheme graph) 
            // the location is in world coordinates
            IDNMcObjectLocation mcLocation = null;
            IDNMcOverlayManager overlayMngr = mapViewport2d.OverlayManager;
            IDNMcObjectScheme mcScheme = DNMcObjectScheme.Create(ref mcLocation, overlayMngr,
                DNEMcPointCoordSystem._EPCS_WORLD, true);

            // create a picture item from a bitmap handle (read by the application as a file)
            IntPtr bitmapHandle = IntPtr.Zero;
            Bitmap currentBitmap = new Bitmap(@"C:\Prj\MapCore7\Dev\Development\Test\Media\Base\terrain\blue.bmp");
            bitmapHandle = currentBitmap.GetHbitmap();

            IDNMcBitmapHandleTexture mcTexture = DNMcBitmapHandleTexture.Create(bitmapHandle, true);

             IDNMcPictureItem mcPicture  = DNMcPictureItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN, DNEMcPointCoordSystem._EPCS_SCREEN, mcTexture);
            // connect the picture item to the root-location
             mcPicture.Connect(mcLocation);

            // create a second picture item (in this case from the same bitmap texture)
             IDNMcPictureItem mcPicture2 = DNMcPictureItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN, DNEMcPointCoordSystem._EPCS_SCREEN, mcTexture);

             // connect the picture item to the root-location
             mcPicture2.Connect(mcLocation);

            // the 1st picture remains in place,
            // the 2nd picture is offset by X=50 Y=20 Z=0 pixels.
            // because the origin (root location) is in world coord` we need to notify 
            // MapCore that the offset is in screen-pixels and not in meters.

            mcPicture.SetCoordinateSystemConversion(DNEMcPointCoordSystem._EPCS_SCREEN, true);

            mcPicture.SetOffset(new DNSMcFVector3D(50, 20, 0), 0, false);

            // create the object using the existing scheme, enter a valid coordinate. in this case
            // it is the map center.
            // If you want to use edit mode, sent the 1st picture item toedit mode
            IDNMcObject mcObj = DNMcObject.Create(overlay, mcScheme, new DNSMcVector3D[] { mapViewport2d.GetCameraPosition() });

        }

        private void Panel1_Resize(object sender, EventArgs e)
        {
            if (mapViewport2d != null)
            {
                mapViewport2d.ViewportResized();
            }
        }

        private void Panel2_Resize(object sender, EventArgs e)
        {
            if (mapViewport3d != null)
            {
                mapViewport3d.ViewportResized();
            }
        }
    }
}
