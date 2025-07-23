using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using System.Drawing.Printing;
using System.Collections;
using MCTester.General_Forms;
using System.Diagnostics;
using System.IO;
using System.Drawing.Imaging;
using System.Security;
using MCTester.ObjectWorld.OverlayManagerWorld;
using MCTester.ObjectWorld.ObjectsUserControls;
using MCTester.Managers.MapWorld;
using MCTester.MapWorld.MapUserControls;


namespace MCTester.GUI.Forms
{

    public enum PrintType { Screen, World }

    public partial class btnPrintForm : Form/*, IDNPrintCallback*/
    {
        private Stopwatch mMCTime;
        private Stopwatch mWinTime;
        private IDNMcMapViewport m_ActiveViewport;
        private List<IDNMcObject> CalcPrintObjectsList = new List<IDNMcObject>();
        private DNEPrintDlgType m_PrintDlgType;
        private IDNMcOverlay m_activeOverlay;
        private DNSPrintRectPagesCalc2D m_PrintRectPagesCalc2D;
        private IDNMcObjectScheme m_ScreenAnnotationScheme = null;
        private IDNMcObjectScheme m_WorldAnnotationScheme = null;
        private PrintType m_PrintType;
        private MCTPrintCallback m_LastPrintCallback;
       
        public btnPrintForm()
        {
            InitializeComponent();
            cmbPrintDlgType.Items.AddRange(Enum.GetNames(typeof(DNEPrintDlgType)));
            cmbPrintDlgType.Text = DNEPrintDlgType._EPDT_NONE.ToString();

            rdbPrint.Checked = true;
            m_ActiveViewport = MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.Viewport;
            m_activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;

            this.Text = Text + " Of " + Manager_MCViewports.GetFullNameOfViewport(m_ActiveViewport, false);

            mMCTime = new Stopwatch();
            mWinTime = new Stopwatch();

            ntxRect2DResolutionFactor.SetFloat(1);
            ntxScreenResolutionFactor.SetFloat(1);
            string filter = "TIFF files(*.tif, *.tiff) | *.tif; *.tiff;| Pdf files(*.pdf) | *.pdf| Bitmap files(*.bmp) |*.bmp| JPEG files(*.jpg, *.jpeg) | *.jpg; *.jpeg;| PNG files(*.png) |*.png| All Files |*.*";
            ctrlBrowseScreen.Filter = filter;
            ctrlBrowseRect2D.Filter = filter;

        }

        private bool IsExistOverlay()
        {
            bool isExistOverlay = false;
            m_activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.GetActiveOverlayOfOverlayManager(m_ActiveViewport.OverlayManager);
            isExistOverlay = m_activeOverlay != null;

            return isExistOverlay;
        }

        private void btnCalcPrintRect_Click(object sender, EventArgs e)
        {
            if (IsExistOverlay())
            {
                try
                {
                    DNSPrintScreenPagesCalc PrintScreenPagesCalc;

                    m_ActiveViewport.CalcPrintScreenPages(PrintDlgType,
                                                            ntxPagesX.GetUInt32(),
                                                            ntxPagesY.GetUInt32(),
                                                            out PrintScreenPagesCalc);


                    GetCalcPrintRectScreenArray(PrintScreenPagesCalc.aPagesScreenRects1, new DNSMcBColor(255, 0, 0, 255), 3);
                    GetCalcPrintRectScreenArray(PrintScreenPagesCalc.aPagesScreenRects2, new DNSMcBColor(0, 255, 0, 255), 2);
                    GetCalcPrintRectScreenArray(PrintScreenPagesCalc.aPagesScreenRects3, new DNSMcBColor(0, 0, 255, 255), 1);

                    MessageBox.Show("Print Scale:\t" + PrintScreenPagesCalc.fPrintScale.ToString() + "\n" +
                                                "Page Pixel Size (Width):\t" + PrintScreenPagesCalc.PagePixelSize.Width.ToString() + "\n" +
                                                "Page Pixel Size (Height):\t" + PrintScreenPagesCalc.PagePixelSize.Height.ToString());

                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("CalcPrintScreenPages", McEx);
                }
            }
            else
                MessageBox.Show("Missing active overlay", "Calc Print Rect");
        }

        private void GetCalcPrintRectScreenArray(DNSMcBox[] outputBoxes ,DNSMcBColor mcBColor, short drawPriority)
        {
            foreach (DNSMcBox pagesCalc in outputBoxes)
            {
                GetCalcPrintRectScreen(pagesCalc, mcBColor, drawPriority);
            }
        }

        private void GetCalcPrintRectScreen(DNSMcBox pagesCalc, DNSMcBColor mcBColor,short drawPriority,  bool isScreenPoint = true)
        {
            try
            {
                IDNMcObjectSchemeItem ObjSchemeItem = GetCalcPrintRectScreenScheme(mcBColor);
                
                IDNMcObject obj = GetCalcPrintRectScreenObject(pagesCalc, ObjSchemeItem, isScreenPoint);

                if (obj != null)
                {
                    CalcPrintObjectsList.Add(obj);
                    obj.SetDrawPriority(drawPriority);
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
            }
        }

        private IDNMcObjectSchemeItem GetCalcPrintRectScreenScheme(DNSMcBColor mcBColor)
        {
            MapCore.DNEItemSubTypeFlags uItemSubTypeBitField;
            DNEMcPointCoordSystem eRectangleCoordinateSystem;
            DNEItemGeometryType eRectangleType;

            if (m_ActiveViewport.MapType == DNEMapType._EMT_2D)
            {
                uItemSubTypeBitField = DNEItemSubTypeFlags._EISTF_SCREEN | DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN;
                eRectangleCoordinateSystem = DNEMcPointCoordSystem._EPCS_WORLD;
                eRectangleType = DNEItemGeometryType._EGT_GEOMETRIC_IN_OVERLAY_MANAGER;
            }
            else
            {
                uItemSubTypeBitField = DNEItemSubTypeFlags._EISTF_SCREEN;
                eRectangleCoordinateSystem = DNEMcPointCoordSystem._EPCS_SCREEN;
                eRectangleType = DNEItemGeometryType._EGT_GEOMETRIC_IN_VIEWPORT;
            }

            return DNMcRectangleItem.Create(uItemSubTypeBitField,
                                            eRectangleCoordinateSystem,
                                            eRectangleType,
                                            DNERectangleDefinition._ERD_RECTANGLE_DIAGONAL_POINTS,
                                            0f,
                                            0f,
                                            DNELineStyle._ELS_DASH_DOT,
                                            mcBColor,
                                            3f,
                                            null,
                                            new DNSMcFVector2D(0, -1),
                                            1f,
                                            DNEFillStyle._EFS_NONE);


        }

        private IDNMcObject GetCalcPrintRectScreenObject(DNSMcBox pagesCalc, IDNMcObjectSchemeItem ObjSchemeItem, bool isScreenPoint = true)
        {
            DNSMcVector3D[] locationPoints = new DNSMcVector3D[2];
            IDNMcObject obj = null;

            if (m_ActiveViewport.MapType == DNEMapType._EMT_2D)
            {
                locationPoints[0] = ConvertScreenToWorld(pagesCalc.MinVertex, isScreenPoint);
                locationPoints[1] = ConvertScreenToWorld(pagesCalc.MaxVertex, isScreenPoint);

                obj = DNMcObject.Create(m_activeOverlay,
                                        ObjSchemeItem,
                                        DNEMcPointCoordSystem._EPCS_WORLD,
                                        locationPoints,
                                        false);
            }
            else
            {
                locationPoints[0].x = pagesCalc.MinVertex.x;
                locationPoints[0].y = pagesCalc.MinVertex.y;
                locationPoints[1].x = pagesCalc.MaxVertex.x;
                locationPoints[1].y = pagesCalc.MaxVertex.y;

                obj = DNMcObject.Create(m_activeOverlay,
                                        ObjSchemeItem,
                                        DNEMcPointCoordSystem._EPCS_SCREEN,
                                        locationPoints,
                                        false);
            }

            return obj;
        }

        private DNSMcVector3D ConvertScreenToWorld(DNSMcVector3D screenPoint, bool isScreenPoint = true)
        {
            DNSMcVector3D worldPoint = new DNSMcVector3D();
            bool isIntersect;

            if (isScreenPoint)
            {
                try
                {
                    m_ActiveViewport.ScreenToWorldOnPlane(screenPoint, out worldPoint, out isIntersect);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("ScreenToWorldOnPlane", McEx);
                }
            }
            else
                worldPoint = screenPoint;

            try
            {
                worldPoint = m_ActiveViewport.ViewportToOverlayManagerWorld(worldPoint);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("ViewportToOverlayManagerWorld", McEx);
            }
            return worldPoint;
        }

        private void btnPrintScreen_Click(object sender, EventArgs e)
        {
            try
            {
                mMCTime.Reset();
                mMCTime.Start();
                m_PrintType = PrintType.Screen;

                MCTPrintCallback printCallback = null;
                if (chxPrintScreenAsync.Checked)
                {
                    printCallback = new MCTPrintCallback();
                    m_LastPrintCallback = printCallback;
                }
                m_ActiveViewport.PrintScreen(PrintDlgType,
                                                ntxPagesX.GetUInt32(),
                                                ntxPagesY.GetUInt32(),
                                                false,
                                                chxPrintScreenAsync.Checked,
                                                printCallback,
                                                ntxPageToPrintScreen.GetUInt32());

                mMCTime.Stop();

                if (!chxPrintScreenAsync.Checked)
                    MessageBox.Show("Print duration:  " + mMCTime.ElapsedMilliseconds.ToString() + " ms", "Printing Finished Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("PrintScreen", McEx);
            }
        }

        private void btnPrintScreenToMem_Click(object sender, EventArgs e)
        {
            MCTPrintCallback printCallback = null;
            try
            {
                m_PrintType = PrintType.Screen;
                printCallback = new MCTPrintCallback(m_ActiveViewport, m_PrintType, ntxPagesX.GetUInt32(), ntxPagesY.GetUInt32(), ntxPageToPrintScreen.GetUInt32(), chxPrintScreenAsync.Checked);
                m_LastPrintCallback = printCallback;

                m_ActiveViewport.PrintScreen(PrintDlgType,
                                                        ntxPagesX.GetUInt32(),
                                                        ntxPagesY.GetUInt32(),
                                                        true,
                                                        chxPrintScreenAsync.Checked,
                                                        printCallback,
                                                        ntxPageToPrintScreen.GetUInt32());
            }
            catch (MapCoreException McEx)
            {
                if(printCallback != null)
                    printCallback.DeleteDirectory();
                MapCore.Common.Utilities.ShowErrorMessage("PrintScreen", McEx);
            }
        }

        private DNEPrintDlgType PrintDlgType
        {
            get { return (DNEPrintDlgType)Enum.Parse(typeof(DNEPrintDlgType), cmbPrintDlgType.Text); }
            set { m_PrintDlgType = value; }
        }

        private void btnPrinterSetting_Click(object sender, EventArgs e)
        {
            PrintDialog PrintDialogForm = new PrintDialog();
            try
            {
                DNSPrinterSettings dnPrinterSettings = m_ActiveViewport.GetPrinterSettings();

                PrintDialogForm.PrinterSettings = new PrinterSettings();
                if (dnPrinterSettings.hPrinterDeviceNames != IntPtr.Zero)
                    PrintDialogForm.PrinterSettings.SetHdevnames(dnPrinterSettings.hPrinterDeviceNames);
                if (dnPrinterSettings.hPrinterDeviceMode != IntPtr.Zero)
                    PrintDialogForm.PrinterSettings.SetHdevmode(dnPrinterSettings.hPrinterDeviceMode);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetPrinterSettings", McEx);
            }

            if (rdbPrint.Checked == true)
            {
                PrintDialogForm.UseEXDialog = true;
            }
            else
            {
                PrintDialogForm.UseEXDialog = false;
            }

            if (PrintDialogForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DNSPrinterSettings mcPrinterSettings = new DNSPrinterSettings();
                    mcPrinterSettings.hPrinterDeviceMode = PrintDialogForm.PrinterSettings.GetHdevmode();
                    mcPrinterSettings.hPrinterDeviceNames = PrintDialogForm.PrinterSettings.GetHdevnames();
                    m_ActiveViewport.SetPrinterSettings(mcPrinterSettings, true);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("SetPrinterSettings", McEx);
                }
            }
        }

        private void btnCalcPrintRectPages2D_Click(object sender, EventArgs e)
        {
            if (IsExistOverlay())
            {
                try
                {
                    DNSPrintRectPagesCalc2D PrintRectPagesCalc2D;

                    m_ActiveViewport.CalcPrintRectPages2D(PrintDlgType,
                                                            ctrl2DVectorPrintWorldRectCenter.GetVector2D(),
                                                            ctrl2DVectorPrintWorldRectSize.GetVector2D(),
                                                            ntxPrintWorldRectAngle.GetFloat(),
                                                            ntxPrintScale.GetFloat(),
                                                            out PrintRectPagesCalc2D);


                    m_PrintRectPagesCalc2D = PrintRectPagesCalc2D;

                    CalcPrintRectPages2DArray(m_PrintRectPagesCalc2D.aPagesWorldRects1, new DNSMcBColor(255, 0, 0, 255), 3);
                    CalcPrintRectPages2DArray(m_PrintRectPagesCalc2D.aPagesWorldRects2, new DNSMcBColor(0, 255, 0, 255), 2);
                    CalcPrintRectPages2DArray(m_PrintRectPagesCalc2D.aPagesWorldRects3, new DNSMcBColor(0, 0, 255, 255), 1);

                    MessageBox.Show("Pixel Size (Width):\t" + m_PrintRectPagesCalc2D.PagePixelSize.Width.ToString() + "\n" +
                                            "Pixel Size (Height):\t" + m_PrintRectPagesCalc2D.PagePixelSize.Height.ToString() + "\n" +
                                            "Num Pages X:\t" + m_PrintRectPagesCalc2D.uNumPagesX.ToString() + "\n" +
                                            "Num Pages Y:\t" + m_PrintRectPagesCalc2D.uNumPagesY.ToString());
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("CalcPrintRectPages2D", McEx);
                }
            }
            else
                MessageBox.Show("Missing active overlay", "Calc Print Rect Pages 2D");

        }

        private IDNMcObject CalcPrintRectPages2D(DNSMcVector3D[] pageRect, DNSMcBColor McBColor , float yaw = 0, short drawPriority = 1)
        {
            IDNMcObject obj = null;
            try
            {
                IDNMcObjectSchemeItem ObjSchemeItem = DNMcPolygonItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN | DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN,
                                                                                DNELineStyle._ELS_DASH_DOT,
                                                                                McBColor,
                                                                                3f,
                                                                                null,
                                                                                new DNSMcFVector2D(0, -1),
                                                                                1f,
                                                                                DNEFillStyle._EFS_NONE);

                (ObjSchemeItem as IDNMcPolygonItem).SetRotationYaw(yaw, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                // Convert page points from viewport coordinates to overlayManager coordinates
                DNSMcVector3D[] pointInOMCoord = new DNSMcVector3D[4];
                pointInOMCoord[0] = m_ActiveViewport.ViewportToOverlayManagerWorld(pageRect[0]);
                pointInOMCoord[1] = m_ActiveViewport.ViewportToOverlayManagerWorld(pageRect[1]);
                pointInOMCoord[2] = m_ActiveViewport.ViewportToOverlayManagerWorld(pageRect[2]);
                pointInOMCoord[3] = m_ActiveViewport.ViewportToOverlayManagerWorld(pageRect[3]);

                obj = DNMcObject.Create(m_activeOverlay,
                                                        ObjSchemeItem,
                                                        DNEMcPointCoordSystem._EPCS_WORLD,
                                                        pointInOMCoord,
                                                        false);
                obj.SetDrawPriority(drawPriority);
                if (obj != null)
                    CalcPrintObjectsList.Add(obj);

            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
            }
            return obj;
        }

        private void CalcPrintRectPages2DArray(DNSMcVector3D[][] pagesWorldRect, DNSMcBColor McBColor, short drawPriority = 1)
        {
            foreach (DNSMcVector3D[] pageRect in pagesWorldRect)
            {
                CalcPrintRectPages2D(pageRect, McBColor, drawPriority);
            }
        }

        private void btnPrintRect2D_Click(object sender, EventArgs e)
        {

            try
            {
                mMCTime.Reset();
                mMCTime.Start();
                m_PrintType = PrintType.World;

                MCTPrintCallback printCallback = null;
                if (chxPrintRect2DAsync.Checked)
                {
                    printCallback = new MCTPrintCallback();
                    m_LastPrintCallback = printCallback;
                }

                m_ActiveViewport.PrintRect2D(PrintDlgType,
                                                ctrl2DVectorPrintWorldRectCenter.GetVector2D(),
                                                ctrl2DVectorPrintWorldRectSize.GetVector2D(),
                                                ntxPrintWorldRectAngle.GetFloat(),
                                                ntxPrintScale.GetFloat(),
                                                false,
                                                chxPrintRect2DAsync.Checked,
                                                printCallback,
                                                ntxPageToPrintWorld.GetUInt32());


                mMCTime.Stop();
                if (!chxPrintRect2DAsync.Checked)
                    MessageBox.Show("Print duration:  " + mMCTime.ElapsedMilliseconds.ToString() + " ms", "Printing Finished Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("PrintRect2D", McEx);
            }

        }

        private void btnPrintRect2DToMemory_Click(object sender, EventArgs e)
        {
            
                MCTPrintCallback printCallback = null;

                try
                {

                    m_PrintType = PrintType.World;
                    printCallback = new MCTPrintCallback(m_ActiveViewport, m_PrintType, ntxPageToPrintWorld.GetUInt32(), chxPrintRect2DAsync.Checked);
                    m_LastPrintCallback = printCallback;

                    m_ActiveViewport.PrintRect2D(PrintDlgType,
                                                    ctrl2DVectorPrintWorldRectCenter.GetVector2D(),
                                                    ctrl2DVectorPrintWorldRectSize.GetVector2D(),
                                                    ntxPrintWorldRectAngle.GetFloat(),
                                                    ntxPrintScale.GetFloat(),
                                                    true,
                                                    chxPrintRect2DAsync.Checked,
                                                    printCallback,
                                                    ntxPageToPrintWorld.GetUInt32());
                }
                catch (MapCoreException McEx)
                {
                    if (printCallback != null)
                        printCallback.DeleteDirectory();
                    MapCore.Common.Utilities.ShowErrorMessage("PrintRect2D", McEx);
                }
        }

        private void btnGoToRectLocation_Click(object sender, EventArgs e)
        {
            DNSMcVector3D destinationPoint = new DNSMcVector3D(ctrl2DVectorPrintWorldRectCenter.X, ctrl2DVectorPrintWorldRectCenter.Y, 0);
            GoToLocation(destinationPoint);
        }

        private void btnPrintRect2DRawRasterGoToLoc_Click(object sender, EventArgs e)
        {
            DNSMcVector3D destinationPoint = new DNSMcVector3D(ctrl2DVectorPrintRect2DAsRawRasterCenter.X, ctrl2DVectorPrintRect2DAsRawRasterCenter.Y, 0);
            GoToLocation(destinationPoint);
        }

        private void GoToLocation(DNSMcVector3D destinationPoint)
        {
            try
            {

                bool ifFoundHeight = false;
                if (m_ActiveViewport.MapType == DNEMapType._EMT_2D)
                {
                    m_ActiveViewport.SetCameraPosition(destinationPoint, false);
                }
                else if (m_ActiveViewport.MapType == DNEMapType._EMT_3D)
                {
                    DNMcNullableOut<DNSMcVector3D> normal = null;

                    bool pbHeightFound = false;
                    double height;
                    m_ActiveViewport.GetTerrainHeight(destinationPoint, out pbHeightFound, out height, normal);
                    ifFoundHeight = pbHeightFound;
                    destinationPoint.z = height;
                    if (ifFoundHeight)
                    {
                        destinationPoint.z = destinationPoint.z + 500;
                        m_ActiveViewport.SetCameraPosition(destinationPoint, false);
                        m_ActiveViewport.SetCameraOrientation(0, -90, 0, false);
                    }
                }
                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_ActiveViewport);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetCameraPosition", McEx);
            }
        }

        private void frmPrintMap_FormClosing(object sender, FormClosingEventArgs e)
        {
			foreach (IDNMcObject obj in CalcPrintObjectsList)
            {
                obj.Remove();
            }
            if(m_LastPrintCallback != null)
                m_LastPrintCallback.DeleteDirectory();

            if (MainForm.BtnPrintForms.Contains(this))
                MainForm.BtnPrintForms.Remove(this);
        }

        private void btnScreenAnnotationScheme_Click(object sender, EventArgs e)
        {
            frmPrintObjectSchemeList PrintObjectSchemeListForm = new frmPrintObjectSchemeList(m_ScreenAnnotationScheme);
            if (PrintObjectSchemeListForm.ShowDialog() == DialogResult.OK)
            {
                m_ScreenAnnotationScheme = PrintObjectSchemeListForm.SelectedScheme;

                if (PrintObjectSchemeListForm.SelectedScheme != null)
                    lblScreenAnnotationScheme.Text = "Selected";
                else
                    lblScreenAnnotationScheme.Text = "Null";
            }
        }

        private void btnWorldAnnotationScheme_Click(object sender, EventArgs e)
        {
            frmPrintObjectSchemeList PrintObjectSchemeListForm = new frmPrintObjectSchemeList(m_WorldAnnotationScheme);
            if (PrintObjectSchemeListForm.ShowDialog() == DialogResult.OK)
            {
                m_WorldAnnotationScheme = PrintObjectSchemeListForm.SelectedScheme;

                if (PrintObjectSchemeListForm.SelectedScheme != null)
                    lblWorldAnnotationScheme.Text = "Selected";
                else
                    lblWorldAnnotationScheme.Text = "Null";
            }
        }

        private void btnPrintedPageSetting_Click(object sender, EventArgs e)
        {
            DNSPageSettings printedPageSettings = new DNSPageSettings();
            printedPageSettings.fBottomMargin = ntxBottomMargin.GetFloat();
            printedPageSettings.fLeftMargin = ntxLeftMargin.GetFloat();
            printedPageSettings.fRightMargin = ntxRightMargin.GetFloat();
            printedPageSettings.fTopMargin = ntxTopMargin.GetFloat();
            printedPageSettings.fOverlappingSize = ntxOverlappingSize.GetFloat();
            printedPageSettings.pScreenAnnotationScheme = m_ScreenAnnotationScheme;
            printedPageSettings.pWorldAnnotationScheme = m_WorldAnnotationScheme;
            printedPageSettings.uPageNumberTextPropertyID = ntxPageNumberTextPropertyID.GetUInt32();
            
            try
            {
                m_ActiveViewport.SetPrintedPageSettings(printedPageSettings);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetPrintedPageSettings", McEx);
            }
        }

        private void frmPrintMap_Load(object sender, EventArgs e)
        {
            try
            {
                DNSPageSettings printedPageSettings = m_ActiveViewport.GetPrintedPageSettings();
                ntxBottomMargin.SetFloat(printedPageSettings.fBottomMargin);
                ntxLeftMargin.SetFloat(printedPageSettings.fLeftMargin);
                ntxRightMargin.SetFloat(printedPageSettings.fRightMargin);
                ntxTopMargin.SetFloat(printedPageSettings.fTopMargin);
                ntxOverlappingSize.SetFloat(printedPageSettings.fOverlappingSize);
                ntxPageNumberTextPropertyID.SetUInt32(printedPageSettings.uPageNumberTextPropertyID);

                if (printedPageSettings.pScreenAnnotationScheme != null)
                {
                    m_ScreenAnnotationScheme = printedPageSettings.pScreenAnnotationScheme;
                    lblScreenAnnotationScheme.Text = "Selected";
                }

                if (printedPageSettings.pWorldAnnotationScheme != null)
                {
                    m_WorldAnnotationScheme = printedPageSettings.pWorldAnnotationScheme;
                    lblWorldAnnotationScheme.Text = "Selected";
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetPrintedPageSettings", McEx);
            }
        }

        private void btnDeletePrintRects_Click(object sender, EventArgs e)
        {
            if (IsExistOverlay())
            {
                try
                {
                    foreach (IDNMcObject obj in CalcPrintObjectsList)
                    {
                        obj.Remove();
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("obj Remove", McEx);
                }
            }
            else
            {
                CalcPrintObjectsList.Clear();
                MessageBox.Show("Missing active overlay", "Calc Print Rect");
            }
        }

        public void HandleRemoveObject(IDNMcObject mcObject)
        {
            if (CalcPrintObjectsList.Contains(mcObject))
                CalcPrintObjectsList.Remove(mcObject);
        }

        private void btnPrintScreenToRawRasterData_Click(object sender, EventArgs e)
        {
            try
            {
                DNMcNullableOut<byte[]> FileMemoryBuffer = cbScreenAsRawRasterPrintToFileInMemory.Checked ? new DNMcNullableOut<byte[]>() : null ;
                DNMcNullableOut<byte[]> WorldFileMemoryBuffer = new DNMcNullableOut<byte[]>();
                MCTPrintCallback printCallback = null;
                if (chxPrintScreenToRawRasterDataAsync.Checked)
                {
                    printCallback = new MCTPrintCallback(ctrlBrowseScreen.FileName);
                    m_LastPrintCallback = printCallback;
                }

                m_ActiveViewport.PrintScreenToRawRasterData(
                     ntxScreenResolutionFactor.GetFloat(),
                     cbScreenAsRawRasterPrintToFileInMemory.Checked ? GetExtension(ctrlBrowseScreen.FileName) : ctrlBrowseScreen.FileName,
                     cbScreenAsRawRasterPrintToFileInMemory.Checked ? FileMemoryBuffer : null, 
                     cbScreenAsRawRasterPrintToFileInMemory.Checked ? WorldFileMemoryBuffer : null, 
                     printCallback,
                     m_options.ToArray());

                if (!chxPrintScreenToRawRasterDataAsync.Checked)
                {
                    if (cbScreenAsRawRasterPrintToFileInMemory.Checked)
                        SavePrintToFile(ctrlBrowseScreen.FileName, FileMemoryBuffer.Value, WorldFileMemoryBuffer.Value);
                    else
                        MessageBox.Show("Action completed successfully", "Print Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("PrintScreenToRawRasterData", McEx);
            }
        }

        public static void SavePrintToFile(string path, byte[] FileMemoryBuffer, byte[] WorldFileMemoryBuffer)
        {
            if (FileMemoryBuffer != null)
            {
                File.WriteAllBytes(@path, FileMemoryBuffer);
                if (WorldFileMemoryBuffer != null)
                {
                    string worldFile = path.Replace(Path.GetExtension(path), ".wld");
                    File.WriteAllBytes(@worldFile, WorldFileMemoryBuffer);
                }
                MessageBox.Show("Action completed successfully", "Print Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        internal IDNMcMapViewport GetActiveViewport()
        {
            return m_ActiveViewport;
        }

        private void btnPrintRect2DToRawRasterData_Click(object sender, EventArgs e)
        {
            try
            {
                DNMcNullableOut<byte[]> FileMemoryBuffer = new DNMcNullableOut<byte[]>();
                DNMcNullableOut<byte[]> WorldFileMemoryBuffer = new DNMcNullableOut<byte[]>();
                MCTPrintCallback printCallback = null;

                if (chxPrintRect2DToRawRasterDataAsync.Checked)
                {
                    printCallback = new MCTPrintCallback(ctrlBrowseRect2D.FileName);
                    m_LastPrintCallback = printCallback;
                }

                m_ActiveViewport.PrintRect2DToRawRasterData(ctrl2DVectorPrintRect2DAsRawRasterCenter.GetVector2D(),
                    ctrl2DVectorPrintRect2DRawRasterSize.GetVector2D(),
                    ntxPrintRect2DRawRasterAngle.GetFloat(),
                    ntxPrintRawRasterScale.GetFloat(),
                    ntxRect2DResolutionFactor.GetFloat(),
                    cbRect2DAsRawRasterPrintToFileInMemory.Checked ? GetExtension(ctrlBrowseRect2D.FileName) : ctrlBrowseRect2D.FileName,
                    cbRect2DAsRawRasterPrintToFileInMemory.Checked ? FileMemoryBuffer : null, 
                    cbRect2DAsRawRasterPrintToFileInMemory.Checked ? WorldFileMemoryBuffer : null, 
                    printCallback,
                    m_options.ToArray(),
                    chxPrintGeoInMetricProportion.Checked);

                if (!chxPrintRect2DToRawRasterDataAsync.Checked)
                {
                    if (cbRect2DAsRawRasterPrintToFileInMemory.Checked)
                        SavePrintToFile(ctrlBrowseRect2D.FileName, FileMemoryBuffer.Value, WorldFileMemoryBuffer.Value);
                    else
                        MessageBox.Show("Action completed successfully", "Print Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("PrintRect2DToRawRasterData", McEx);
            }
        }

        private string GetExtension(string fileName)
        {
            string ext = "";
            try
            {
                fileName = Path.GetExtension(fileName);
                if (fileName.StartsWith("."))
                    ext = fileName.Substring(1).Trim();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return fileName;
            }
            return ext;
        }

        private void btnCancelAsyncPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_LastPrintCallback != null)
                    m_ActiveViewport.CancelAsyncPrint(m_LastPrintCallback);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("PrintRect2DToRawRasterData", McEx);
            }
        }

        private void btnCalculatePrintRectRawRaster_Click(object sender, EventArgs e)
        {
            if (IsExistOverlay())
            {
                DNSMcBox pagesCalc = new DNSMcBox();
                try
                {
                    if (m_ActiveViewport.MapType == DNEMapType._EMT_2D)
                    {
                        pagesCalc = m_ActiveViewport.GetCameraWorldVisibleArea();
                    }
                    else
                    {
                        uint height, width;
                        m_ActiveViewport.GetViewportSize(out width, out height);
                        pagesCalc.MinVertex = new DNSMcVector3D(0, 0, 0);
                        pagesCalc.MaxVertex = new DNSMcVector3D(width - 1, height - 1, 0);
                    }
                    GetCalcPrintRectScreen(pagesCalc, new DNSMcBColor(255, 0, 0, 255), 1, false);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetCameraWorldVisibleArea", McEx);
                }
            }
            else
                MessageBox.Show("Missing active overlay", "Calc Print Rect");
        }

        private void btnCalculatePrintRect2DRawRaster_Click(object sender, EventArgs e)
        {
            if (IsExistOverlay())
            {
                DNSMcVector2D center = ctrl2DVectorPrintRect2DAsRawRasterCenter.GetVector2D();
                DNSMcVector2D size = ctrl2DVectorPrintRect2DRawRasterSize.GetVector2D() / 2;

                double angle = ntxPrintRect2DRawRasterAngle.GetDouble();
                IDNMcObjectSchemeItem ObjSchemeItem = null;
                DNSMcVector3D[] pointInOMCoord = new DNSMcVector3D[1];
                try
                {
                    if (chxPrintGeoInMetricProportion.Checked)
                    {

                        ObjSchemeItem = DNMcRectangleItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN | DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN,
                                                    DNEMcPointCoordSystem._EPCS_WORLD,
                                                    DNEItemGeometryType._EGT_GEOMETRIC_IN_VIEWPORT,
                                                    DNERectangleDefinition._ERD_RECTANGLE_CENTER_DIMENSIONS,
                                                    (float)size.x,
                                                    (float)size.y,
                                                    DNELineStyle._ELS_DASH_DOT,
                                                    new DNSMcBColor(255, 0, 0, 255),
                                                    3f,
                                                    null,
                                                    new DNSMcFVector2D(0, -1),
                                                    1f,
                                                    DNEFillStyle._EFS_NONE);

                        (ObjSchemeItem as IDNMcRectangleItem).SetRotationYaw(ntxPrintRect2DRawRasterAngle.GetFloat(), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                        // Convert page points from viewport coordinates to overlayManager coordinates
                        pointInOMCoord[0] = m_ActiveViewport.ViewportToOverlayManagerWorld(new DNSMcVector3D(center.x, center.y, 0));

                       
                    }
                    else
                    {
                        DNSMcVector3D vector3D1 = new DNSMcVector3D(size.x, 0, 0);
                        DNSMcVector3D vector3D2 = new DNSMcVector3D(0, size.y , 0);

                        vector3D1.RotateByDegreeYawAngle(angle);
                        vector3D2.RotateByDegreeYawAngle(angle);

                        DNSMcVector3D point0 = center - vector3D1 - vector3D2;
                        DNSMcVector3D point1 = center - vector3D1 + vector3D2;
                        DNSMcVector3D point2 = center + vector3D1 + vector3D2;
                        DNSMcVector3D point3 = center + vector3D1 - vector3D2;

                        DNSMcVector3D[] polypoints = new DNSMcVector3D[4] { point0, point1, point2 , point3};

                        ObjSchemeItem = DNMcPolygonItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN | DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN,
                                                                                    DNELineStyle._ELS_DASH_DOT,
                                                                                   new DNSMcBColor(255, 0, 0, 255),
                                                                                    3f,
                                                                                    null,
                                                                                    new DNSMcFVector2D(0, -1),
                                                                                    1f,
                                                                                    DNEFillStyle._EFS_NONE,
                                                                                    new DNSMcBColor(238, 130, 238, 100),   //135, 206, 250
                                                                                    null,
                                                                                    new DNSMcFVector2D(0, 0));
                        pointInOMCoord = new DNSMcVector3D[4];
                        for (int i = 0; i < polypoints.Length; i++)
                        {
                            pointInOMCoord[i] = m_ActiveViewport.ViewportToOverlayManagerWorld(polypoints[i]);
                        }
                       

                    }
                    IDNMcObject obj = DNMcObject.Create(m_activeOverlay,
                                                         ObjSchemeItem,
                                                         DNEMcPointCoordSystem._EPCS_WORLD,
                                                         pointInOMCoord,
                                                         false);
                    if (obj != null)
                        CalcPrintObjectsList.Add(obj);

                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
                }
            }
            else
                MessageBox.Show("Missing active overlay", "Calc Print Rect");
        }

        List<string> m_options = new List<string>();

        private void btnGdalOptions_Click(object sender, EventArgs e)
        {
            frmGdalOptions frmGdalOptions = new frmGdalOptions(m_options);
            if(frmGdalOptions.ShowDialog() == DialogResult.OK)
            {
                m_options = frmGdalOptions.GetOptions();
            }
        }
    }

    public class MCTPrintCallback : IDNPrintCallback
    {
        private string m_fullFilePath;
        private bool m_isMemory = true;
        private bool m_isAsync = true;
        private PrintType m_PrintType;
        private uint m_PagesX;       
        private uint m_PagesY;       
        private IDNMcMapViewport m_ActiveViewport;
        private uint m_PageToPrint;  
        private uint m_PageNumber;

        private Stopwatch m_WinTime = new Stopwatch();
        private Stopwatch m_MCTime = new Stopwatch();
        private DirectoryInfo m_PrintDirectoryInfo;
        private string m_PrefixFolderName = "Print";

        // print not memory, screen or world, async, needed for show user msg on finish
        public MCTPrintCallback()
        {
            m_isMemory = false;
        }

        // print to raw raster
        public MCTPrintCallback(string filePath)
        {
            m_isMemory = false;
            m_fullFilePath = filePath;
        }

        // print to memory screen
        public MCTPrintCallback(IDNMcMapViewport activeViewport, PrintType printType, uint pagesX, uint pagesY, uint pageToPrint, bool isAsync)
             : this(activeViewport, printType, pageToPrint, isAsync)
        {
            m_PagesX = pagesX;
            m_PagesY = pagesY;
        }

        // print to memory world 
        public MCTPrintCallback(IDNMcMapViewport activeViewport, PrintType printType, uint pageToPrint, bool isAsync)
        {
            m_isMemory = true;
            m_ActiveViewport = activeViewport;
            m_PrintType = printType;
            m_PageToPrint = pageToPrint;
          //  if (m_PrintType == PrintType.Screen)
            InitForPrintToMem();

            m_isAsync = isAsync;

            if (!m_isAsync)
            {
                m_MCTime.Reset();
                m_MCTime.Start();
            }
        }

        private void InitForPrintToMem()
        {
            string folderName = "";
            try
            {
                DateTime dateTime = DateTime.Now;
                folderName = m_PrefixFolderName + "_" + dateTime.Day.ToString() + "_" + dateTime.Month.ToString() + "_" + dateTime.Year.ToString() + "_" + dateTime.Hour.ToString() + "_" + dateTime.Minute.ToString() + "_" + dateTime.Second.ToString();
                if (!Directory.Exists(folderName))
                    m_PrintDirectoryInfo = Directory.CreateDirectory(folderName);
                else if(m_PrintDirectoryInfo == null)
                    m_PrintDirectoryInfo = new DirectoryInfo(folderName);
                m_fullFilePath = m_PrintDirectoryInfo.FullName;
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error in create directory: " + folderName + Environment.NewLine + ex.Message, "Error in create directory");
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Error in create directory: " + folderName + Environment.NewLine + ex.Message, "Error in create directory");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Error in create directory: " + folderName + Environment.NewLine + ex.Message, "Error in create directory");
            }
        }

        public void PrintScreenToMemory()
        {
            try
            {


                PrintDocument printerDoc = new PrintDocument();

                DNSPrinterSettings dnPrinterSettings = m_ActiveViewport.GetPrinterSettings();

                printerDoc.PrinterSettings = new PrinterSettings();
                if (dnPrinterSettings.hPrinterDeviceNames != IntPtr.Zero)
                    printerDoc.PrinterSettings.SetHdevnames(dnPrinterSettings.hPrinterDeviceNames);
                if (dnPrinterSettings.hPrinterDeviceMode != IntPtr.Zero)
                    printerDoc.PrinterSettings.SetHdevmode(dnPrinterSettings.hPrinterDeviceMode);

                printerDoc.PrintPage += new PrintPageEventHandler(printerDoc_PrintPage);
                m_PageNumber = 1;

                m_WinTime.Reset();
                m_WinTime.Start();
                printerDoc.Print();
                m_WinTime.Stop();
                printerDoc.PrintPage -= new PrintPageEventHandler(printerDoc_PrintPage);

                if (!m_isAsync)
                    MessageBox.Show("MapCore print duration:  " + m_MCTime.ElapsedMilliseconds.ToString() + " ms\nWindows print duration:  " + m_WinTime.ElapsedMilliseconds.ToString() + " ms", "Printing Finished Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Invalid Printer Settings");

                DeleteDirectory();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error when call print, reason : " + Environment.NewLine + ex.Message, "Print Doc");
            }
        }

        void printerDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                e.Graphics.PageUnit = GraphicsUnit.Pixel;
                Dictionary<int, int> dPages = new Dictionary<int, int>();
                uint totalPages = m_PagesX * m_PagesY;

                DNSPrinterSettings dnPrinterSettings = m_ActiveViewport.GetPrinterSettings();
                e.PageSettings.PrinterSettings = new PrinterSettings();

                if (dnPrinterSettings.hPrinterDeviceNames != IntPtr.Zero)
                    e.PageSettings.PrinterSettings.SetHdevnames(dnPrinterSettings.hPrinterDeviceNames);
                if (dnPrinterSettings.hPrinterDeviceMode != IntPtr.Zero)
                    e.PageSettings.PrinterSettings.SetHdevmode(dnPrinterSettings.hPrinterDeviceMode);

                e.HasMorePages = false;

                if (m_PageToPrint != 0)
                {
                    m_PageNumber = m_PageToPrint;
                    PrintPage(e);
                }
                else
                {
                    PrintPage(e);

                    if (m_PrintType == PrintType.Screen && m_PageNumber < totalPages)
                        e.HasMorePages = true;
                    if (m_PrintType == PrintType.World)
                    {
                        DirectoryInfo[] subDir = m_PrintDirectoryInfo.GetDirectories();
                        if (m_PageNumber < subDir.Length)
                            e.HasMorePages = true;
                    }
                    m_PageNumber++;
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetPrinterSettings", McEx);
            }
            catch (DirectoryNotFoundException ex)
            {
                MessageBox.Show("Error when get directories of" + m_PrintDirectoryInfo.Name + Environment.NewLine + ex.Message, "Get Directories");
            }
            catch (SecurityException ex)
            {
                MessageBox.Show("Error when get directories of" + m_PrintDirectoryInfo.Name + Environment.NewLine + ex.Message, "Get Directories");
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Error when get directories of" + m_PrintDirectoryInfo.Name + Environment.NewLine + ex.Message, "Get Directories");
            }
        }

        private void PrintPage(PrintPageEventArgs e)
        {
            try
            {
                string fullName = m_PrintDirectoryInfo.FullName + "\\" + m_PageNumber.ToString();
                string[] files = Directory.GetFiles(fullName);

                foreach (string path in files)
                {
                    string fileName = path.Replace(fullName + "\\", "");
                    string[] data = fileName.Trim().Split('_');
                    if (data.Length > 1)
                    {
                        Bitmap bitmap = new Bitmap(path);
                        e.Graphics.DrawImage(bitmap, float.Parse(data[0]), float.Parse(data[1]), bitmap.Width, bitmap.Height);
                        bitmap.Dispose();
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "Error in print page");
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Error in print page");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error in print page");
            }
        }

        public void DeleteDirectory()
        {
            if (m_PrintDirectoryInfo != null && m_PrintDirectoryInfo.Exists)
            {
                m_PrintDirectoryInfo.Delete(true);
                m_PrintDirectoryInfo = null;
            }
        }

        public int OnPrintTileReceived(IntPtr hTileBitmap, int nPage, int nX, int nY)
        {
            string path = m_fullFilePath + "\\" + nPage.ToString();
            try
            {
                Bitmap pBitmap = Image.FromHbitmap(hTileBitmap);
                if (pBitmap != null)
                {
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    pBitmap.Save(path + "\\" + nX.ToString() + "_" + nY.ToString());
                    pBitmap.Dispose();
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error in create directory: " + path + Environment.NewLine + ex.Message, "Error in create directory");
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Error in create directory: " + path + Environment.NewLine + ex.Message, "Error in create directory");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Error in create directory: " + path + Environment.NewLine + ex.Message, "Error in create directory");
            }
            return 0;
        }

        public void OnPrintFinished(DNEMcErrorCode eStatus, string strFileNameOrRasterDataFormat, byte[] auRasterFileMemoryBuffer, byte[] auWorldFileMemoryBuffer)
        {
            if(!m_isAsync)
                m_MCTime.Stop();

            if(eStatus == DNEMcErrorCode.SUCCESS)
            {
                // print as raw raster
                if (auRasterFileMemoryBuffer != null && auRasterFileMemoryBuffer.Length > 0)
                {
                    File.WriteAllBytes(@m_fullFilePath, auRasterFileMemoryBuffer);
                    string worldFile = m_fullFilePath.Replace(Path.GetExtension(m_fullFilePath) ,".wld");
                    if (auWorldFileMemoryBuffer != null && auWorldFileMemoryBuffer.Length > 0)
                    {
                        File.WriteAllBytes(@worldFile, auWorldFileMemoryBuffer);
                    }
                }
                else if (m_isMemory)
                {
                    PrintScreenToMemory();
                }
                MessageBox.Show("Action completed successfully", "Print Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(IDNMcErrors.ErrorCodeToString(eStatus), "Print Finished", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}