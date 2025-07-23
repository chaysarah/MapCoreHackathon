using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using System.IO;
using MCTester.GUI.Map;
using MCTester.Managers.MapWorld;
using MCTester.Managers.ObjectWorld;
using MCTester.Controls;
using System.Diagnostics;

namespace MCTester.General_Forms
{
    public enum EDrawnItemType
    {
        Line,
        Circle,
        Rectangle,
        Polygon,
        Point,
        Connector
    }

    public partial class frmGeometricCalc : Form
    {
        private OpenFileDialog OFD;
        private StreamReader STR;
        private StreamWriter STW;
        private List<string> lSourceLines;
        private string[] sourceLineValues;
        private string outputLine;
        private IDNMcOverlay activeOverlay;
        private DNSMcBColor m_ItemColor;
        private DNEFillStyle m_FillStyle;
        private DNELineStyle m_LineStyle;
        //private EDrawnItemType m_ItemType;
        private IDNMcObjectSchemeItem m_ObjSchemeItem;
        private IDNMcTextItem m_DefaultText;
        private IDNMcLogFont m_DefaultFont;
        private IDNMcTexture m_DefaultTexture;
        private Stopwatch mMCTime;

        public frmGeometricCalc()
        {
            InitializeComponent();
            OFD = new OpenFileDialog();
            activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;

            if (activeOverlay != null)
            {
                FontDialog Fd = new FontDialog();
                DNSMcLogFont logFont = new DNSMcLogFont();
                Fd.Font.ToLogFont(logFont);   
                m_DefaultFont = DNMcLogFont.Create(new DNMcVariantLogFont(logFont, false));
            }
            mMCTime = new Stopwatch();
        }

        private bool GetSourceLines(string fileName)
        {
            bool bResult = false;
            string sFileName = "";
            if (fileName == "")
            {
                OFD.Filter = "csv files|*.csv";
                OFD.RestoreDirectory = true;
                if (OFD.ShowDialog() == DialogResult.OK)
                {
                    sFileName = OFD.FileName;
                }
            }
            else
                sFileName = fileName;
            try
            {
                if (sFileName != "")
                {
                    string filename = Path.GetFileNameWithoutExtension(sFileName);
                    string filePath = Path.GetDirectoryName(sFileName);
                    STR = new StreamReader(sFileName);
                    STW = new StreamWriter(Path.Combine(filePath, filename) + "_Output.csv");
                    lSourceLines = new List<string>();
                    char[] trimChar = new char[1];
                    trimChar[0] = ',';

                    while (!STR.EndOfStream)
                    {
                        string line = STR.ReadLine();
                        lSourceLines.Add(line.TrimEnd(trimChar));
                    }
                    bResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error open file, msg - " + ex.Message, "Error open file");
            }
            return bResult;
        }

        private void CloseStreams()
        {
            STR.Close();
            STW.Close();
        }

        /* private bool GetSourceLines()
         {
             bool bResult = false;
             OFD.Filter = "csv files|*.csv";
             OFD.RestoreDirectory = true;
             if (OFD.ShowDialog() == DialogResult.OK)
             {
                 char[] c = new char[4] { '.', 'c', 's', 'v' };
                 string t = OFD.FileName.TrimEnd(c);
                 STR = new StreamReader(OFD.FileName);
                 STW = new StreamWriter(t + "_Output.csv");
                 lSourceLines = new List<string>();
                 char[] trimChar = new char[1];
                 trimChar[0] = ',';

                 while (!STR.EndOfStream)
                 {
                     string line = STR.ReadLine();
                     lSourceLines.Add(line.TrimEnd(trimChar));
                 }

                 bResult = true;
             }

             return bResult;
         }
 */

        private void DrawFunction(bool IsSourceParam, EDrawnItemType itemType, DNGEOMETRIC_SHAPE geometricShape, DNSMcVector3D[] vertices, int lineNumber, double ConnectorLength)
        {
            if (activeOverlay != null)
            {
                if (IsSourceParam == true)
                {
                    m_ItemColor = new DNSMcBColor(60, 60, 60, 150);
                    m_FillStyle = DNEFillStyle._EFS_CROSS;
                    m_LineStyle = DNELineStyle._ELS_SOLID;
                }
                else
                {
                    m_ItemColor = new DNSMcBColor(0, 0, 255, 150);
                    m_FillStyle = DNEFillStyle._EFS_NONE;
                    m_LineStyle = DNELineStyle._ELS_DASH_DOT;
                }

                string shapeType = "";
                if (geometricShape != DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE)
                    shapeType = "\n" + geometricShape.ToString();

                m_DefaultText = DNMcTextItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN, DNEMcPointCoordSystem._EPCS_SCREEN,
                                                        m_DefaultFont);

                m_DefaultTexture = DNMcBitmapHandleTexture.Create(MCTester.Icons.NotationPoint.GetHbitmap(), false, false, new DNSMcBColor(0, 0, 0, 255));
                m_DefaultText.SetText(new DNMcVariantString("Line - " + lineNumber.ToString() + shapeType, false), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                m_DefaultText.SetBackgroundColor(new DNSMcBColor(255, 255, 255, 150), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                m_DefaultText.SetTextColor(new DNSMcBColor(0, 128, 0, 255), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                switch (itemType)
                {
                    case EDrawnItemType.Line:
                        try
                        {
                            m_ObjSchemeItem = DNMcLineItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                            m_LineStyle,
                                                                                            m_ItemColor,
                                                                                            2,
                                                                                            null,
                                                                                            new DNSMcFVector2D(1, 1),
                                                                                            1f);

                            ((IDNMcLineItem)m_ObjSchemeItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("DNMcLineItem.Create", McEx);
                            return;
                        }
                        break;
                    case EDrawnItemType.Circle:
                        try
                        {
                            DNSMcVector3D center = new DNSMcVector3D();
                            double radius = 0;

                            IDNMcGeometricCalculations.EG2DCircleFrom3Points(vertices[0],
                                                                        vertices[1],
                                                                        vertices[2],
                                                                        ref center,
                                                                        ref radius);


                            m_ObjSchemeItem = DNMcEllipseItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                            DNEMcPointCoordSystem._EPCS_WORLD,
                                                                                            DNEItemGeometryType._EGT_GEOMETRIC_IN_OVERLAY_MANAGER,
                                                                                            (float)radius,
                                                                                            (float)radius,
                                                                                            0,
                                                                                            360,
                                                                                            0,
                                                                                            m_LineStyle,
                                                                                            m_ItemColor,
                                                                                            2,
                                                                                            null,
                                                                                            new DNSMcFVector2D(1, 1),
                                                                                            2,
                                                                                            m_FillStyle,
                                                                                            m_ItemColor);


                            ((IDNMcEllipseItem)m_ObjSchemeItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                            // draw the points that the ellipse is based on 
                            IDNMcObjectSchemeItem EllipseBaseLineSchemeItem = DNMcLineItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                                                             m_LineStyle,
                                                                                                                             m_ItemColor,
                                                                                                                             2f,
                                                                                                                             null,
                                                                                                                             new DNSMcFVector2D(1, 1),
                                                                                                                             1f);



                            DNMcObject.Create(activeOverlay,
                                                    EllipseBaseLineSchemeItem,
                                                    DNEMcPointCoordSystem._EPCS_WORLD,
                                                    vertices,
                                                    false);

                            ((IDNMcLineItem)EllipseBaseLineSchemeItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                            IDNMcTextItem verticeNumText = null;
                            m_DefaultText.Clone(out verticeNumText);

                            DNMcObject.Create(activeOverlay,
                                                    verticeNumText,
                                                    DNEMcPointCoordSystem._EPCS_WORLD,
                                                    vertices,
                                                    false);

                            string[] astr = new string[] { "1", "2", "3" };
                            verticeNumText.SetText(new DNMcVariantString(astr, false), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                            verticeNumText.SetAttachPointType(0, DNEAttachPointType._EAPT_ALL_POINTS, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID); 

                            vertices = new DNSMcVector3D[] { center };

                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("EG2DCircleFrom3Points", McEx);
                            return;
                        }
                        break;
                    case EDrawnItemType.Polygon:
                        try
                        {
                            m_ObjSchemeItem = DNMcPolygonItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                        m_LineStyle,
                                                                                        m_ItemColor,
                                                                                        2,
                                                                                        null,
                                                                                        new DNSMcFVector2D(1, 1),
                                                                                        1f,
                                                                                        m_FillStyle,
                                                                                        m_ItemColor);

                            ((IDNMcPolygonItem)m_ObjSchemeItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("DNMcPolygonItem.Create", McEx);
                            return;
                        }
                        break;
                    case EDrawnItemType.Rectangle:
                        try
                        {
                            m_ObjSchemeItem = DNMcRectangleItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                                DNEMcPointCoordSystem._EPCS_WORLD,
                                                                                                DNEItemGeometryType._EGT_GEOMETRIC_IN_OVERLAY_MANAGER,
                                                                                                DNERectangleDefinition._ERD_RECTANGLE_DIAGONAL_POINTS,
                                                                                                0f,
                                                                                                0f,
                                                                                                m_LineStyle,
                                                                                                m_ItemColor,
                                                                                                2,
                                                                                                null,
                                                                                                new DNSMcFVector2D(1, 1),
                                                                                                1f,
                                                                                                m_FillStyle,
                                                                                                m_ItemColor);

                            ((IDNMcPolygonItem)m_ObjSchemeItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("DNMcRectangleItem.Create", McEx);
                            return;
                        }
                        break;
                    case EDrawnItemType.Point:
                        try
                        {
                            m_ObjSchemeItem = DNMcPictureItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN, DNEMcPointCoordSystem._EPCS_SCREEN, m_DefaultTexture);
                            ((IDNMcPictureItem)m_ObjSchemeItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("DNMcPictureItem.Create", McEx);
                            return;
                        }
                        break;
                    case EDrawnItemType.Connector:
                        try
                        {
                            m_ObjSchemeItem = DNMcArrowItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                            DNEMcPointCoordSystem._EPCS_SCREEN,
                                                                                            10f,
                                                                                            45f,
                                                                                            0,
                                                                                            m_LineStyle,
                                                                                            m_ItemColor,
                                                                                            3f);

                            ((IDNMcArrowItem)m_ObjSchemeItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                            m_DefaultText.SetText(new DNMcVariantString(Math.Round(ConnectorLength, 2).ToString(), false), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("DNMcArrowItem.Create", McEx);
                            return;
                        }
                        break;
                }

                IDNMcObject obj = DNMcObject.Create(activeOverlay,
                                                    m_ObjSchemeItem,
                                                    DNEMcPointCoordSystem._EPCS_WORLD,
                                                    vertices,
                                                    false);


                m_DefaultText.Connect(m_ObjSchemeItem);
                m_DefaultText.SetAttachPointType(0, DNEAttachPointType._EAPT_CENTER_POINT, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID); 
                m_DefaultText.SetBoundingBoxAttachPointType(0, DNEBoundingBoxPointFlags._EBBPF_BOTTOM_MIDDLE, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                m_DefaultText.SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
            }
        }

        private IDNMcObjectScheme GetResultTextScheme(string textToSet)
        {
            IDNMcObjectScheme retScheme = null;
            IDNMcTextItem clonedText = null;

            try
            {
                if (m_DefaultText == null)
                {
                    m_DefaultText = DNMcTextItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN, DNEMcPointCoordSystem._EPCS_SCREEN,
                                                            m_DefaultFont);
                }

                m_DefaultText.Clone(out clonedText);

                retScheme = DNMcObjectScheme.Create(MCTMapFormManager.MapForm.Viewport.OverlayManager, clonedText, DNEMcPointCoordSystem._EPCS_WORLD, false);
                Manager_MCObjectScheme.AddTempObjectScheme(retScheme);

                clonedText.SetBackgroundColor(new DNSMcBColor(255, 255, 255, 150), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                clonedText.SetAttachPointType(0, DNEAttachPointType._EAPT_CENTER_POINT, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID); 
                clonedText.SetBoundingBoxAttachPointType(0, DNEBoundingBoxPointFlags._EBBPF_BOTTOM_MIDDLE, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                clonedText.SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                ((IDNMcTextItem)clonedText).SetText(new DNMcVariantString(textToSet, false), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetResultTextScheme", McEx);
            }

            return retScheme;
        }

        private void DrawCircleBasedOnPointAndRadius(bool IsSourceCircle, DNSMcVector3D circleCenter, float radius)
        {
            try
            {
                if (activeOverlay != null)
                {
                    if (IsSourceCircle == true)
                    {
                        m_ItemColor = new DNSMcBColor(255, 0, 0, 150);
                        m_FillStyle = DNEFillStyle._EFS_SOLID;
                        m_LineStyle = DNELineStyle._ELS_SOLID;
                    }
                    else
                    {
                        m_ItemColor = new DNSMcBColor(192, 192, 192, 150);
                        m_FillStyle = DNEFillStyle._EFS_CROSS;
                        m_LineStyle = DNELineStyle._ELS_DASH_DOT;
                    }

                    IDNMcObjectSchemeItem ObjSchemeItem = null;
                    ObjSchemeItem = DNMcEllipseItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                            DNEMcPointCoordSystem._EPCS_WORLD,
                                                            DNEItemGeometryType._EGT_GEOMETRIC_IN_OVERLAY_MANAGER,
                                                            radius,
                                                            radius,
                                                            0,
                                                            360,
                                                            0,
                                                            m_LineStyle,
                                                            m_ItemColor,
                                                            2,
                                                            null,
                                                            new DNSMcFVector2D(1, 1),
                                                            1,
                                                            DNEFillStyle._EFS_NONE);


                    DNSMcVector3D[] vertices = new DNSMcVector3D[] { circleCenter };
                    DNMcObject.Create(activeOverlay,
                                        ObjSchemeItem,
                                        DNEMcPointCoordSystem._EPCS_WORLD,
                                        vertices,
                                        false);
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcEllipseItem.Create", McEx);
            }
        }

        private void DrawActionResult(bool IsPolygon, bool IsSourceParam, DNSMcVector3D[] vertices)
        {
            IDNMcOverlay activeOverlay = Manager_MCOverlayManager.ActiveOverlay;

            DNSMcBColor color;
            DNEFillStyle fillStyle;

            if (IsSourceParam == true)
            {
                color = new DNSMcBColor(255, 0, 0, 100);
                fillStyle = DNEFillStyle._EFS_SOLID;
            }
            else
            {
                color = new DNSMcBColor(0, 0, 255, 255);
                fillStyle = DNEFillStyle._EFS_CROSS;
            }

            if (Manager_MCOverlayManager.ActiveOverlayManager != null)
            {
                if (activeOverlay != null)
                {
                    DNSMcVector3D[] locationPoints = new DNSMcVector3D[0];
                    IDNMcObjectSchemeItem ObjSchemeItem = null;

                    if (IsPolygon == true)
                    {
                        ObjSchemeItem = DNMcPolygonItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                    DNELineStyle._ELS_SOLID,
                                                                                    m_ItemColor,
                                                                                    2,
                                                                                    null,
                                                                                    new DNSMcFVector2D(1, 1),
                                                                                    1f,
                                                                                    fillStyle,
                                                                                    color);

                        ((IDNMcPolygonItem)ObjSchemeItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                    }
                    else
                    {
                        ObjSchemeItem = DNMcLineItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                    DNELineStyle._ELS_SOLID,
                                                                                    color,
                                                                                    2,
                                                                                    null,
                                                                                    new DNSMcFVector2D(1, 1),
                                                                                    1f);

                        ((IDNMcLineItem)ObjSchemeItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    }


                    IDNMcObject obj = DNMcObject.Create(activeOverlay,
                                                        ObjSchemeItem,
                                                        DNEMcPointCoordSystem._EPCS_WORLD,
                                                        vertices,
                                                        false);


                }
                else
                    MessageBox.Show("There is no active overlay");
            }
            else
                MessageBox.Show("There is no active overlay manager");
        }

        private void EndGeometricCalcAction(string ActionName)
        {
            STR.Close();
            STW.Close();

            MessageBox.Show("Action completed", ActionName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #region Line Functions
        private void btnEGParallelLine_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEGParallelLine_Click(dlgResult))
                EndGeometricCalcAction("Parallel Line");
        }

        private bool btnEGParallelLine_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            int maxNumOfVertices = 0;
            List<DNSMcVector3D[]> lLinePoints = new List<DNSMcVector3D[]>();

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                double distance = double.Parse(sourceLineValues[0]);
                int lineNumPt = (sourceLineValues.Length - 1) / 3;

                DNSMcVector3D[] baseLine = new DNSMcVector3D[lineNumPt];
                int idx = 0;
                for (int cell = 1; cell < sourceLineValues.Length; cell += 3)
                {
                    baseLine[idx].x = double.Parse(sourceLineValues[cell]);
                    baseLine[idx].y = double.Parse(sourceLineValues[cell + 1]);
                    baseLine[idx].z = double.Parse(sourceLineValues[cell + 2]);

                    idx++;
                }

                DNSMcVector3D[] parallelLine = new DNSMcVector3D[0];
                try
                {
                    parallelLine = IDNMcGeometricCalculations.EGParallelLine(baseLine,
                                                                                distance);

                    //Fined the longest result in order to match the header line
                    if (parallelLine.Length > maxNumOfVertices)
                        maxNumOfVertices = parallelLine.Length;

                    //Add the result to a result list that will be printed at the end of the process.
                    lLinePoints.Add(parallelLine);

                    //Draw result line points if function succeed and demanded by the user
                    if (dlgResult == DialogResult.Yes)
                        DrawFunction(false, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, parallelLine, i, 0);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EGParallelLine - Line: " + (i + 1).ToString(), McEx);
                    lLinePoints.Add(parallelLine);
                }

                //Draw source line points if demanded by the user
                if (dlgResult == DialogResult.Yes)
                    DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, baseLine, i, 0);
            }

            //Print result header line
            for (int i = 0; i < maxNumOfVertices; i++)
            {
                outputLine += "X" + i + "," +
                                "Y" + i + "," +
                                "Z" + i + ",";

            }

            STW.WriteLine(outputLine);
            outputLine = "";

            for (int lIdx = 0; lIdx < lLinePoints.Count; lIdx++)
            {
                for (int ArrIdx = 0; ArrIdx < lLinePoints[lIdx].Length; ArrIdx++)
                {
                    outputLine += lLinePoints[lIdx][ArrIdx].x.ToString() + "," +
                                    lLinePoints[lIdx][ArrIdx].y.ToString() + "," +
                                    lLinePoints[lIdx][ArrIdx].z.ToString() + ",";
                }

                STW.WriteLine(outputLine);
                outputLine = "";
            }

            //EndGeometricCalcAction("Parallel Line");
            CloseStreams();
            return true;
        }

        private void btnEGPerpendicularLine_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEGPerpendicularLine_Click(dlgResult))
                EndGeometricCalcAction("Perpendicular Line");
        }

        private bool btnEGPerpendicularLine_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            List<DNSMcVector3D[]> lLinePoints = new List<DNSMcVector3D[]>();

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D throughPoint = new DNSMcVector3D();
                throughPoint.x = double.Parse(sourceLineValues[0]);
                throughPoint.y = double.Parse(sourceLineValues[1]);
                throughPoint.z = double.Parse(sourceLineValues[2]);

                double distance = double.Parse(sourceLineValues[3]);

                int lineNumPt = (sourceLineValues.Length - 4) / 3;
                DNSMcVector3D[] baseLine = new DNSMcVector3D[lineNumPt];
                int idx = 0;

                for (int cell = 4; cell < sourceLineValues.Length; cell += 3)
                {
                    baseLine[idx].x = double.Parse(sourceLineValues[cell]);
                    baseLine[idx].y = double.Parse(sourceLineValues[cell + 1]);
                    baseLine[idx].z = double.Parse(sourceLineValues[cell + 2]);

                    idx++;
                }

                DNSMcVector3D[] perpendicularLine = new DNSMcVector3D[0];
                try
                {
                    perpendicularLine = IDNMcGeometricCalculations.EGPerpendicularLine(baseLine,
                                                                                        distance,
                                                                                        throughPoint);


                    //Add the result to a result list that will be printed at the end of the process.
                    lLinePoints.Add(perpendicularLine);

                    //Draw result line points if function succeed and demanded by the user
                    if (dlgResult == DialogResult.Yes)
                        DrawFunction(false, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, perpendicularLine, i, 0);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EGPerpendicularLine - Line: " + (i + 1).ToString(), McEx);
                    lLinePoints.Add(perpendicularLine);
                }

                //Draw source line points if demanded by the user
                if (dlgResult == DialogResult.Yes)
                    DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, baseLine, i, 0);
            }

            //Print result header line
            outputLine += "X1, Y1, Z1, X2, Y2, Z2";
            STW.WriteLine(outputLine);
            outputLine = "";

            for (int lIdx = 0; lIdx < lLinePoints.Count; lIdx++)
            {
                for (int ArrIdx = 0; ArrIdx < lLinePoints[lIdx].Length; ArrIdx++)
                {
                    outputLine += lLinePoints[lIdx][ArrIdx].x.ToString() + "," +
                                    lLinePoints[lIdx][ArrIdx].y.ToString() + "," +
                                    lLinePoints[lIdx][ArrIdx].z.ToString() + ",";
                }

                STW.WriteLine(outputLine);
                outputLine = "";
            }

            //EndGeometricCalcAction("Perpendicular Line");
            CloseStreams();
            return true;
        }

        private void btnEG2DLineMove_Click(object sender, EventArgs e)
        {

            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DLineMove_Click(dlgResult))
                EndGeometricCalcAction("2D Line Move");
        }

        private bool btnEG2DLineMove_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            List<DNSMcVector3D[]> lLinePoints = new List<DNSMcVector3D[]>();

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                double dX = double.Parse(sourceLineValues[0]);
                double dY = double.Parse(sourceLineValues[1]);

                int lineNumPt = (sourceLineValues.Length - 2) / 3;
                DNSMcVector3D[] baseLine = new DNSMcVector3D[lineNumPt];
                int idx = 0;

                for (int cell = 2; cell < sourceLineValues.Length; cell += 3)
                {
                    baseLine[idx].x = double.Parse(sourceLineValues[cell]);
                    baseLine[idx].y = double.Parse(sourceLineValues[cell + 1]);
                    baseLine[idx].z = double.Parse(sourceLineValues[cell + 2]);

                    idx++;
                }

                DNSMcVector3D[] returnLine = new DNSMcVector3D[baseLine.Length];
                baseLine.CopyTo(returnLine, 0);
                try
                {
                    IDNMcGeometricCalculations.EG2DLineMove(ref returnLine,
                                                            dX,
                                                            dY);


                    //Add the result to a result list that will be printed at the end of the process.
                    lLinePoints.Add(returnLine);

                    //Draw result line points if function succeed and demanded by the user
                    if (dlgResult == DialogResult.Yes)
                        DrawFunction(false, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, returnLine, i, 0);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DLineMove - Line: " + (i + 1).ToString(), McEx);
                    returnLine = new DNSMcVector3D[0];
                    lLinePoints.Add(returnLine);
                }

                //Draw source line points if demanded by the user
                if (dlgResult == DialogResult.Yes)
                    DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, baseLine, i, 0);
            }

            //Print result header line
            outputLine += "X1, Y1, Z1, X2, Y2, Z2";
            STW.WriteLine(outputLine);
            outputLine = "";

            for (int lIdx = 0; lIdx < lLinePoints.Count; lIdx++)
            {
                for (int ArrIdx = 0; ArrIdx < lLinePoints[lIdx].Length; ArrIdx++)
                {
                    outputLine += lLinePoints[lIdx][ArrIdx].x.ToString() + "," +
                                    lLinePoints[lIdx][ArrIdx].y.ToString() + "," +
                                    lLinePoints[lIdx][ArrIdx].z.ToString() + ",";
                }

                STW.WriteLine(outputLine);
                outputLine = "";
            }

            CloseStreams();
            return true;
        }

        private void btnEG2DLineRotate_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DLineRotate_Click(dlgResult))
                EndGeometricCalcAction("2D Line Rotate");
        }

        private bool btnEG2DLineRotate_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            List<DNSMcVector3D[]> lLinePoints = new List<DNSMcVector3D[]>();

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D basePoint = new DNSMcVector3D();
                basePoint.x = double.Parse(sourceLineValues[0]);
                basePoint.y = double.Parse(sourceLineValues[1]);
                basePoint.z = double.Parse(sourceLineValues[2]);

                double angle = double.Parse(sourceLineValues[3]);

                int lineNumPt = (sourceLineValues.Length - 4) / 3;
                DNSMcVector3D[] baseLine = new DNSMcVector3D[lineNumPt];
                int idx = 0;

                for (int cell = 4; cell < sourceLineValues.Length; cell += 3)
                {
                    baseLine[idx].x = double.Parse(sourceLineValues[cell]);
                    baseLine[idx].y = double.Parse(sourceLineValues[cell + 1]);
                    baseLine[idx].z = double.Parse(sourceLineValues[cell + 2]);

                    idx++;
                }

                DNSMcVector3D[] returnLine = new DNSMcVector3D[baseLine.Length];
                baseLine.CopyTo(returnLine, 0);
                try
                {
                    IDNMcGeometricCalculations.EG2DLineRotate(ref returnLine,
                                                                angle,
                                                                basePoint);


                    //Add the result to a result list that will be printed at the end of the process.
                    lLinePoints.Add(returnLine);

                    //Draw result line points if function succeed and demanded by the user
                    if (dlgResult == DialogResult.Yes)
                        DrawFunction(false, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, returnLine, i, 0);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DLineRotate - Line: " + (i + 1).ToString(), McEx);
                    returnLine = new DNSMcVector3D[0];
                    lLinePoints.Add(returnLine);
                }

                //Draw source line points if demanded by the user
                if (dlgResult == DialogResult.Yes)
                    DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, baseLine, i, 0);
            }

            //Print result header line
            outputLine += "X1, Y1, Z1, X2, Y2, Z2";
            STW.WriteLine(outputLine);
            outputLine = "";

            for (int lIdx = 0; lIdx < lLinePoints.Count; lIdx++)
            {
                for (int ArrIdx = 0; ArrIdx < lLinePoints[lIdx].Length; ArrIdx++)
                {
                    outputLine += lLinePoints[lIdx][ArrIdx].x.ToString() + "," +
                                    lLinePoints[lIdx][ArrIdx].y.ToString() + "," +
                                    lLinePoints[lIdx][ArrIdx].z.ToString() + ",";
                }

                STW.WriteLine(outputLine);
                outputLine = "";
            }
            CloseStreams();
            return true;
        }

        private void btnEG2DIsPointOnLine_Click(object sender, EventArgs e)
        {

            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DIsPointOnLine_Click(dlgResult))
                EndGeometricCalcAction("2D Is Point On Line");
        }
        private bool btnEG2DIsPointOnLine_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            List<string> lStatusResults = new List<string>();

            //Print result header line
            STW.WriteLine("Status Result");

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D point = new DNSMcVector3D();
                point.x = double.Parse(sourceLineValues[0]);
                point.y = double.Parse(sourceLineValues[1]);
                point.z = double.Parse(sourceLineValues[2]);

                DNGEOMETRIC_SHAPE lineType = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[3]);
                double accuracy = double.Parse(sourceLineValues[4]);

                int lineNumPt = (sourceLineValues.Length - 5) / 3;
                DNSMcVector3D[] baseLine = new DNSMcVector3D[lineNumPt];
                int idx = 0;

                for (int cell = 5; cell < sourceLineValues.Length; cell += 3)
                {
                    baseLine[idx].x = double.Parse(sourceLineValues[cell]);
                    baseLine[idx].y = double.Parse(sourceLineValues[cell + 1]);
                    baseLine[idx].z = double.Parse(sourceLineValues[cell + 2]);

                    idx++;
                }

                try
                {
                    DNPOINT_LINE_STATUS statusResult = IDNMcGeometricCalculations.EG2DIsPointOnLine(point,
                                                                                                        baseLine,
                                                                                                        lineType,
                                                                                                        accuracy);


                    STW.WriteLine(statusResult);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DIsPointOnLine - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine("Failed");
                }

                //Draw source line points if demanded by the user
                if (dlgResult == DialogResult.Yes)
                {
                    DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, baseLine, i, 0);
                    DrawCircleBasedOnPointAndRadius(true, point, (float)accuracy);
                }
            }

            CloseStreams();
            return true;
        }

        private void btnEGDistancePointLine_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEGDistancePointLine_Click(dlgResult))
                EndGeometricCalcAction("Distance Point Line");
        }
        private bool btnEGDistancePointLine_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            List<string> lStatusResults = new List<string>();

            //Print result header line
            STW.WriteLine("Status Result");

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D point = new DNSMcVector3D();
                point.x = double.Parse(sourceLineValues[0]);
                point.y = double.Parse(sourceLineValues[1]);
                point.z = double.Parse(sourceLineValues[2]);

                DNGEOMETRIC_SHAPE lineType = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[3]);

                int lineNumPt = (sourceLineValues.Length - 4) / 3;
                DNSMcVector3D[] baseLine = new DNSMcVector3D[lineNumPt];
                int idx = 0;

                for (int cell = 4; cell < sourceLineValues.Length; cell += 3)
                {
                    baseLine[idx].x = double.Parse(sourceLineValues[cell]);
                    baseLine[idx].y = double.Parse(sourceLineValues[cell + 1]);
                    baseLine[idx].z = double.Parse(sourceLineValues[cell + 2]);

                    idx++;
                }

                double distance = 0;
                try
                {
                    distance = IDNMcGeometricCalculations.EGDistancePointLine(baseLine,
                                                                                lineType,
                                                                                point);


                    STW.WriteLine(distance.ToString());
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EGDistancePointLine - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine("Failed");
                }

                //Draw source line points if demanded by the user
                if (dlgResult == DialogResult.Yes)
                {
                    DNSMcVector3D[] pointVertex = new DNSMcVector3D[] { point };
                    DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, baseLine, i, 0);
                    DrawFunction(true, EDrawnItemType.Point, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, pointVertex, i, 0);
                }
            }

            CloseStreams();
            return true;
        }

        private void btnEGLineDistance_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (btnEGLineDistance_Click(dlgResult))
                EndGeometricCalcAction("Line Distance");
        }

        private bool btnEGLineDistance_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            //Print result header line
            outputLine = "Closes Point On 1 (X), Closes Point On 1 (Y), Closes Point On 1 (Z), Closes Point On 2 (X), Closes Point On 2 (Y), Closes Point On 2 (Z), Distance";
            STW.WriteLine(outputLine);

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D[] line1 = new DNSMcVector3D[2];
                line1[0].x = double.Parse(sourceLineValues[0]);
                line1[0].y = double.Parse(sourceLineValues[1]);
                line1[0].z = double.Parse(sourceLineValues[2]);
                line1[1].x = double.Parse(sourceLineValues[3]);
                line1[1].y = double.Parse(sourceLineValues[4]);
                line1[1].z = double.Parse(sourceLineValues[5]);
                DNGEOMETRIC_SHAPE line1Shape = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[6]);

                DNSMcVector3D[] line2 = new DNSMcVector3D[2];
                line2[0].x = double.Parse(sourceLineValues[7]);
                line2[0].y = double.Parse(sourceLineValues[8]);
                line2[0].z = double.Parse(sourceLineValues[9]);
                line2[1].x = double.Parse(sourceLineValues[10]);
                line2[1].y = double.Parse(sourceLineValues[11]);
                line2[1].z = double.Parse(sourceLineValues[12]);
                DNGEOMETRIC_SHAPE line2Shape = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[13]);

                DNSMcVector3D closesPointOn1 = new DNSMcVector3D();
                DNSMcVector3D closesPointOn2 = new DNSMcVector3D();
                double distance = 0;
                try
                {
                    IDNMcGeometricCalculations.EGLineDistance(line1,
                                                                line1Shape,
                                                                line2,
                                                                line2Shape,
                                                                ref closesPointOn1,
                                                                ref closesPointOn2,
                                                                ref distance);


                    outputLine = closesPointOn1.x.ToString() + "," +
                                    closesPointOn1.y.ToString() + "," +
                                    closesPointOn1.z.ToString() + "," +
                                    closesPointOn2.x.ToString() + "," +
                                    closesPointOn2.y.ToString() + "," +
                                    closesPointOn2.z.ToString() + "," +
                                    distance.ToString();

                    STW.WriteLine(outputLine);

                    DNSMcVector3D[] connectorLinePt = new DNSMcVector3D[2];
                    connectorLinePt[0] = closesPointOn1;
                    connectorLinePt[1] = closesPointOn2;

                    //Draw result line points if function succeed and demanded by the user
                    if (dlgResult == DialogResult.Yes)
                        DrawFunction(false, EDrawnItemType.Connector, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, connectorLinePt, i, distance);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EGLineDistance - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                //Draw source line points if demanded by the user
                if (dlgResult == DialogResult.Yes)
                {
                    DrawFunction(true, EDrawnItemType.Line, line1Shape, line1, i, 0);
                    DrawFunction(true, EDrawnItemType.Line, line2Shape, line2, i, 0);
                }
            }

            CloseStreams();
            return true;
        }

        private void btnEGSegmentsDistance_Click(object sender, EventArgs e)
        {

            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEGSegmentsDistance_Click(dlgResult))
                EndGeometricCalcAction("Segments Distance");
        }

        private bool btnEGSegmentsDistance_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            //Print result header line
            outputLine = "Closes Point On 1 (X), Closes Point On 1 (Y), Closes Point On 1 (Z), Closes Point On 2 (X), Closes Point On 2 (Y), Closes Point On 2 (Z), Distance";
            STW.WriteLine(outputLine);

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D[] segment1 = new DNSMcVector3D[2];
                segment1[0].x = double.Parse(sourceLineValues[0]);
                segment1[0].y = double.Parse(sourceLineValues[1]);
                segment1[0].z = double.Parse(sourceLineValues[2]);
                segment1[1].x = double.Parse(sourceLineValues[3]);
                segment1[1].y = double.Parse(sourceLineValues[4]);
                segment1[1].z = double.Parse(sourceLineValues[5]);

                DNSMcVector3D[] segment2 = new DNSMcVector3D[2];
                segment2[0].x = double.Parse(sourceLineValues[6]);
                segment2[0].y = double.Parse(sourceLineValues[7]);
                segment2[0].z = double.Parse(sourceLineValues[8]);
                segment2[1].x = double.Parse(sourceLineValues[9]);
                segment2[1].y = double.Parse(sourceLineValues[10]);
                segment2[1].z = double.Parse(sourceLineValues[11]);

                DNSMcVector3D closesPointOn1 = new DNSMcVector3D();
                DNSMcVector3D closesPointOn2 = new DNSMcVector3D();
                double distance = 0;
                try
                {
                    IDNMcGeometricCalculations.EGSegmentsDistance(segment1,
                                                                    segment2,
                                                                    ref closesPointOn1,
                                                                    ref closesPointOn2,
                                                                    ref distance);


                    outputLine = closesPointOn1.x.ToString() + "," +
                                    closesPointOn1.y.ToString() + "," +
                                    closesPointOn1.z.ToString() + "," +
                                    closesPointOn2.x.ToString() + "," +
                                    closesPointOn2.y.ToString() + "," +
                                    closesPointOn2.z.ToString() + "," +
                                    distance.ToString();

                    STW.WriteLine(outputLine);

                    DNSMcVector3D[] connectorSegmentPt = new DNSMcVector3D[2];
                    connectorSegmentPt[0] = closesPointOn1;
                    connectorSegmentPt[1] = closesPointOn2;

                    //Draw result line points if function succeed and demanded by the user
                    if (dlgResult == DialogResult.Yes)
                        DrawFunction(false, EDrawnItemType.Connector, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, connectorSegmentPt, i, distance);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EGSegmentsDistance - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                //Draw source line points if demanded by the user
                if (dlgResult == DialogResult.Yes)
                {
                    DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, segment1, i, 0);
                    DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, segment2, i, 0);
                }
            }

            CloseStreams();
            return true;
        }

        private void btnEG2DSegmentsRelation_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DSegmentsRelation_Click(dlgResult))
                EndGeometricCalcAction("2D Segments Relation");
        }

        private bool btnEG2DSegmentsRelation_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            //Print result header line
            outputLine = "First Point(X), First Point(Y), First Point(Z), Second Point(X), Second Point(Y), Second Point(Z), Intersect Points Num, Status";
            STW.WriteLine(outputLine);

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D[] segment1 = new DNSMcVector3D[2];
                segment1[0].x = double.Parse(sourceLineValues[0]);
                segment1[0].y = double.Parse(sourceLineValues[1]);
                segment1[0].z = double.Parse(sourceLineValues[2]);
                segment1[1].x = double.Parse(sourceLineValues[3]);
                segment1[1].y = double.Parse(sourceLineValues[4]);
                segment1[1].z = double.Parse(sourceLineValues[5]);

                DNSMcVector3D[] segment2 = new DNSMcVector3D[2];
                segment2[0].x = double.Parse(sourceLineValues[6]);
                segment2[0].y = double.Parse(sourceLineValues[7]);
                segment2[0].z = double.Parse(sourceLineValues[8]);
                segment2[1].x = double.Parse(sourceLineValues[9]);
                segment2[1].y = double.Parse(sourceLineValues[10]);
                segment2[1].z = double.Parse(sourceLineValues[11]);

                int intersectPointsNum = 0;
                DNSMcVector3D FstPoint = new DNSMcVector3D();
                DNSMcVector3D ScdPoint = new DNSMcVector3D();
                DNSL_SL_STATUS status = DNSL_SL_STATUS._INTERSECT_PARALLEL_SL;
                // _SEPARATE_SL _OVERLAP_SL
                try
                {
                    IDNMcGeometricCalculations.EG2DSegmentsRelation(segment1,
                                                                        segment2,
                                                                        ref intersectPointsNum,
                                                                        ref FstPoint,
                                                                        ref ScdPoint,
                                                                        ref status);

                    outputLine = (intersectPointsNum > 0 ? FstPoint.x.ToString() : "") + "," +
                                   (intersectPointsNum > 0 ? FstPoint.y.ToString() : "") + "," +
                                   (intersectPointsNum > 0 ? FstPoint.z.ToString() : "") + "," +
                                   (intersectPointsNum > 1 ? ScdPoint.x.ToString() : "") + "," +
                                   (intersectPointsNum > 1 ? ScdPoint.y.ToString() : "") + "," +
                                   (intersectPointsNum > 1 ? ScdPoint.z.ToString() : "") + "," +
                                    intersectPointsNum.ToString() + "," +
                                    status.ToString();

                    STW.WriteLine(outputLine);

                    DNSMcVector3D[] intersectionPts = new DNSMcVector3D[] { FstPoint, ScdPoint };
                    //Draw result intersection points if demanded by the user
                    if (dlgResult == DialogResult.Yes)
                        DrawFunction(true, EDrawnItemType.Point, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, intersectionPts, i, 0);

                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DSegmentsRelation - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                //Draw source line points if demanded by the user
                if (dlgResult == DialogResult.Yes)
                {
                    DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, segment1, i, 0);
                    DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, segment2, i, 0);
                }
            }
            CloseStreams();
            return true;
        }
        #endregion

        #region Angles Functions
        private void btnEGAngleBetween3Points_Click(object sender, EventArgs e)
        {

            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEGAngleBetween3Points_Click(dlgResult))
                EndGeometricCalcAction("Angle Between 3 Points");
        }

        private bool btnEGAngleBetween3Points_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            STW.WriteLine("Angle");

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D stFstPoint = new DNSMcVector3D();
                stFstPoint.x = double.Parse(sourceLineValues[0]);
                stFstPoint.y = double.Parse(sourceLineValues[1]);
                stFstPoint.z = double.Parse(sourceLineValues[2]);

                DNSMcVector3D stMidPoint = new DNSMcVector3D();
                stMidPoint.x = double.Parse(sourceLineValues[3]);
                stMidPoint.y = double.Parse(sourceLineValues[4]);
                stMidPoint.z = double.Parse(sourceLineValues[5]);

                DNSMcVector3D stScdPoint = new DNSMcVector3D();
                stScdPoint.x = double.Parse(sourceLineValues[6]);
                stScdPoint.y = double.Parse(sourceLineValues[7]);
                stScdPoint.z = double.Parse(sourceLineValues[8]);

                double pdAngle = 0;
                try
                {
                    pdAngle = IDNMcGeometricCalculations.EGAngleBetween3Points(stFstPoint,
                                                                                stMidPoint,
                                                                                stScdPoint);


                    // print result to CSV file
                    STW.WriteLine(pdAngle.ToString());
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EGAngleBetween3Points - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                if (activeOverlay != null && dlgResult == DialogResult.Yes)
                {
                    DNSMcVector3D[] locationPoint = new DNSMcVector3D[] { stFstPoint, stMidPoint, stScdPoint };
                    DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, locationPoint, i, 0);

                    try
                    {
                        DNMcObject.Create(activeOverlay, GetResultTextScheme(pdAngle.ToString()), locationPoint);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
                    }
                }
            }

            CloseStreams();
            return true;
        }

        private void btnEG2DAngleFromX_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DAngleFromX_Click(dlgResult))
                EndGeometricCalcAction("2D Angle From X");
        }

        private bool btnEG2DAngleFromX_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            STW.WriteLine("Angle");

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D stFstPointLine = new DNSMcVector3D();
                stFstPointLine.x = double.Parse(sourceLineValues[0]);
                stFstPointLine.y = double.Parse(sourceLineValues[1]);
                stFstPointLine.z = double.Parse(sourceLineValues[2]);

                DNSMcVector3D stScdPointLine = new DNSMcVector3D();
                stScdPointLine.x = double.Parse(sourceLineValues[3]);
                stScdPointLine.y = double.Parse(sourceLineValues[4]);
                stScdPointLine.z = double.Parse(sourceLineValues[5]);

                DNSMcVector3D[] stLine = new DNSMcVector3D[] { stFstPointLine, stScdPointLine };
                double pdAngle = 0;

                try
                {
                    pdAngle = IDNMcGeometricCalculations.EG2DAngleFromX(stLine);

                    // print result to CSV file
                    STW.WriteLine(pdAngle.ToString());
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DAngleFromX - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                if (activeOverlay != null && dlgResult == DialogResult.Yes)
                {
                    DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stLine, i, 0);

                    try
                    {
                        DNMcObject.Create(activeOverlay, GetResultTextScheme(pdAngle.ToString()), stLine);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
                    }
                }
            }

            CloseStreams();
            return true;
        }

        private void btnEG3DAngleFromXY_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG3DAngleFromXY_Click(dlgResult))
                EndGeometricCalcAction("3D Angle From XY");
        }

        private bool btnEG3DAngleFromXY_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            STW.WriteLine("Angle");

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D stFstPointLine = new DNSMcVector3D();
                stFstPointLine.x = double.Parse(sourceLineValues[0]);
                stFstPointLine.y = double.Parse(sourceLineValues[1]);
                stFstPointLine.z = double.Parse(sourceLineValues[2]);

                DNSMcVector3D stScdPointLine = new DNSMcVector3D();
                stScdPointLine.x = double.Parse(sourceLineValues[3]);
                stScdPointLine.y = double.Parse(sourceLineValues[4]);
                stScdPointLine.z = double.Parse(sourceLineValues[5]);

                DNSMcVector3D[] stLine = new DNSMcVector3D[] { stFstPointLine, stScdPointLine };
                double pdAngle = 0;

                try
                {
                    pdAngle = IDNMcGeometricCalculations.EG3DAngleFromXY(stLine);

                    // print result to CSV file
                    STW.WriteLine(pdAngle.ToString());
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG3DAngleFromXY - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                if (activeOverlay != null && dlgResult == DialogResult.Yes)
                {
                    DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stLine, i, 0);

                    try
                    {
                        DNMcObject.Create(activeOverlay, GetResultTextScheme(pdAngle.ToString()), stLine);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
                    }
                }
            }

            CloseStreams();
            return true;
        }

        #endregion

        #region Circles Functions
        private void btnEG2DTangentsThroughPoint_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DTangentsThroughPoint_Click(dlgResult))
                EndGeometricCalcAction("2D Tangents Through Point");
        }

        private bool btnEG2DTangentsThroughPoint_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            //Print result header line
            outputLine = "Tangents Num, Tangent1 - Point1(X), Tangent1 - Point1(Y), Tangent1 - Point1(Z), Tangent1 - Point2(X), Tangent1 - Point2(Y), Tangent1 - Point2(Z), Tangent2 - Point1(X), Tangent2 - Point1(Y), Tangent2 - Point1(Z), Tangent2 - Point2(X), Tangent2 - Point2(Y), Tangent2 - Point2(Z)";

            STW.WriteLine(outputLine);

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D circleCenter = new DNSMcVector3D();
                circleCenter.x = double.Parse(sourceLineValues[0]);
                circleCenter.y = double.Parse(sourceLineValues[1]);
                circleCenter.z = double.Parse(sourceLineValues[2]);

                double radius = double.Parse(sourceLineValues[3]);

                DNSMcVector3D throughPoint = new DNSMcVector3D();
                throughPoint.x = double.Parse(sourceLineValues[4]);
                throughPoint.y = double.Parse(sourceLineValues[5]);
                throughPoint.z = double.Parse(sourceLineValues[6]);


                DNSMcVector3D[] Tangent1 = new DNSMcVector3D[2];
                DNSMcVector3D[] Tangent2 = new DNSMcVector3D[2];
                int tangentsNum = 0;

                try
                {
                    IDNMcGeometricCalculations.EG2DTangentsThroughPoint(circleCenter,
                                                                            radius,
                                                                            throughPoint,
                                                                            ref tangentsNum,
                                                                            ref Tangent1,
                                                                            ref Tangent2);


                    outputLine = tangentsNum.ToString() + "," +
                                    Tangent1[0].x.ToString() + "," +
                                    Tangent1[0].y.ToString() + "," +
                                    Tangent1[0].z.ToString() + "," +
                                    Tangent1[1].x.ToString() + "," +
                                    Tangent1[1].y.ToString() + "," +
                                    Tangent1[1].z.ToString() + "," +
                                    Tangent2[0].x.ToString() + "," +
                                    Tangent2[0].y.ToString() + "," +
                                    Tangent2[0].z.ToString() + "," +
                                    Tangent2[1].x.ToString() + "," +
                                    Tangent2[1].y.ToString() + "," +
                                    Tangent2[1].z.ToString();

                    STW.WriteLine(outputLine);

                    //Draw result lines points if demanded by the user
                    if (dlgResult == DialogResult.Yes)
                    {
                        if (tangentsNum > 0)
                        {
                            DrawFunction(false, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, Tangent1, i, 0);
                            DrawFunction(false, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, Tangent2, i, 0);
                        }
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DTangentsThroughPoint - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                if (activeOverlay != null && dlgResult == DialogResult.Yes)
                {
                    DNSMcVector3D[] point = new DNSMcVector3D[] { throughPoint };
                    DrawCircleBasedOnPointAndRadius(true, circleCenter, (float)radius);
                    DrawFunction(true, EDrawnItemType.Point, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, point, i, 0);
                }
            }

            CloseStreams();
            return true;
        }

        private void btnEG2DTangents2Circles_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DTangents2Circles_Click(dlgResult))
                EndGeometricCalcAction("2D Tangents 2 Circles");
        }

        private bool btnEG2DTangents2Circles_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            //Print result header line
            outputLine = "Tangents Num, Tangent1 - Point1(X), Tangent1 - Point1(Y), Tangent1 - Point1(Z), Tangent1 - Point2(X), Tangent1 - Point2(Y), Tangent1 - Point2(Z), Tangent2 - Point1(X), Tangent2 - Point1(Y), Tangent2 - Point1(Z), Tangent2 - Point2(X), Tangent2 - Point2(Y), Tangent2 - Point2(Z)," +
                         "Tangent3 - Point1(X), Tangent3 - Point1(Y), Tangent3 - Point1(Z), Tangent3 - Point2(X), Tangent3 - Point2(Y), Tangent3 - Point2(Z), Tangent4 - Point1(X), Tangent4 - Point1(Y), Tangent4 - Point1(Z), Tangent4 - Point2(X), Tangent4 - Point2(Y), Tangent4 - Point2(Z)";

            STW.WriteLine(outputLine);

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D circleCenter1 = new DNSMcVector3D();
                circleCenter1.x = double.Parse(sourceLineValues[0]);
                circleCenter1.y = double.Parse(sourceLineValues[1]);
                circleCenter1.z = double.Parse(sourceLineValues[2]);

                double radius1 = double.Parse(sourceLineValues[3]);

                DNSMcVector3D circleCenter2 = new DNSMcVector3D();
                circleCenter2.x = double.Parse(sourceLineValues[4]);
                circleCenter2.y = double.Parse(sourceLineValues[5]);
                circleCenter2.z = double.Parse(sourceLineValues[6]);

                double radius2 = double.Parse(sourceLineValues[7]);

                DNSMcVector3D[] Tangent1 = new DNSMcVector3D[2];
                DNSMcVector3D[] Tangent2 = new DNSMcVector3D[2];
                DNSMcVector3D[] Tangent3 = new DNSMcVector3D[2];
                DNSMcVector3D[] Tangent4 = new DNSMcVector3D[2];
                int tangentsNum = 0;

                try
                {
                    IDNMcGeometricCalculations.EG2DTangents2Circles(circleCenter1,
                                                                        radius1,
                                                                        circleCenter2,
                                                                        radius2,
                                                                        ref tangentsNum,
                                                                        ref Tangent1,
                                                                        ref Tangent2,
                                                                        ref Tangent3,
                                                                        ref Tangent4);


                    outputLine = tangentsNum.ToString() + "," +
                                    Tangent1[0].x.ToString() + "," +
                                    Tangent1[0].y.ToString() + "," +
                                    Tangent1[0].z.ToString() + "," +
                                    Tangent1[1].x.ToString() + "," +
                                    Tangent1[1].y.ToString() + "," +
                                    Tangent1[1].z.ToString() + "," +
                                    Tangent2[0].x.ToString() + "," +
                                    Tangent2[0].y.ToString() + "," +
                                    Tangent2[0].z.ToString() + "," +
                                    Tangent2[1].x.ToString() + "," +
                                    Tangent2[1].y.ToString() + "," +
                                    Tangent2[1].z.ToString() + "," +
                                    Tangent3[0].x.ToString() + "," +
                                    Tangent3[0].y.ToString() + "," +
                                    Tangent3[0].z.ToString() + "," +
                                    Tangent3[1].x.ToString() + "," +
                                    Tangent3[1].y.ToString() + "," +
                                    Tangent3[1].z.ToString() + "," +
                                    Tangent4[0].x.ToString() + "," +
                                    Tangent4[0].y.ToString() + "," +
                                    Tangent4[0].z.ToString() + "," +
                                    Tangent4[1].x.ToString() + "," +
                                    Tangent4[1].y.ToString() + "," +
                                    Tangent4[1].z.ToString();

                    STW.WriteLine(outputLine);

                    //Draw source line points if demanded by the user
                    if (dlgResult == DialogResult.Yes)
                    {
                        DrawFunction(false, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, Tangent1, i, 0);
                        DrawFunction(false, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, Tangent2, i, 0);
                        DrawFunction(false, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, Tangent3, i, 0);
                        DrawFunction(false, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, Tangent4, i, 0);
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DTangents2Circles - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                if (dlgResult == DialogResult.Yes)
                {
                    DrawCircleBasedOnPointAndRadius(true, circleCenter1, (float)radius1);
                    DrawCircleBasedOnPointAndRadius(true, circleCenter2, (float)radius2);
                }
            }

            CloseStreams();
            return true;
        }

        private void btnEGArcLengthFromAngle_Click(object sender, EventArgs e)
        {
            if (btnEGArcLengthFromAngle_Click())
                EndGeometricCalcAction("Arc Length From Angle");
        }

        private bool btnEGArcLengthFromAngle_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            //Print result header line
            STW.WriteLine("Length");

            double length = 0;
            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                double angleDegree = double.Parse(sourceLineValues[0]);
                double radius = double.Parse(sourceLineValues[1]);

                try
                {
                    length = IDNMcGeometricCalculations.EGArcLengthFromAngle(angleDegree, radius);

                    STW.WriteLine(length.ToString());
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EGArcLengthFromAngle - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }
            }

            CloseStreams();
            return true;
        }

        private void btnEGArcAngleFromLength_Click(object sender, EventArgs e)
        {
            if (btnEGArcAngleFromLength_Click())
                EndGeometricCalcAction("Arc Angle From Length");
        }

        private bool btnEGArcAngleFromLength_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            //Print result header line
            STW.WriteLine("Angle");

            double angleDegree = 0;
            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                double length = double.Parse(sourceLineValues[0]);
                double radius = double.Parse(sourceLineValues[1]);

                try
                {
                    angleDegree = IDNMcGeometricCalculations.EGArcAngleFromLength(length, radius);

                    STW.WriteLine(angleDegree.ToString());
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EGArcLengthFromAngle - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }
            }

            CloseStreams();
            return true;
        }

        private void btnEG2DCircleFrom3Points_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DCircleFrom3Points_Click(dlgResult))
                EndGeometricCalcAction("2D Circle From 3 Points");
        }

        private bool btnEG2DCircleFrom3Points_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            //Print result header line
            STW.WriteLine("Center (X), Center (Y), Center (Z), Radius");

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D circle1stPoint = new DNSMcVector3D();
                circle1stPoint.x = double.Parse(sourceLineValues[0]);
                circle1stPoint.y = double.Parse(sourceLineValues[1]);
                circle1stPoint.z = double.Parse(sourceLineValues[2]);

                DNSMcVector3D circle2ndPoint = new DNSMcVector3D();
                circle2ndPoint.x = double.Parse(sourceLineValues[3]);
                circle2ndPoint.y = double.Parse(sourceLineValues[4]);
                circle2ndPoint.z = double.Parse(sourceLineValues[5]);

                DNSMcVector3D circle3rdPoint = new DNSMcVector3D();
                circle3rdPoint.x = double.Parse(sourceLineValues[6]);
                circle3rdPoint.y = double.Parse(sourceLineValues[7]);
                circle3rdPoint.z = double.Parse(sourceLineValues[8]);

                DNSMcVector3D center = new DNSMcVector3D();
                double radius = 0;

                try
                {
                    IDNMcGeometricCalculations.EG2DCircleFrom3Points(circle1stPoint,
                                                                        circle2ndPoint,
                                                                        circle3rdPoint,
                                                                        ref center,
                                                                        ref radius);


                    outputLine = center.x.ToString() + "," +
                                    center.y.ToString() + "," +
                                    center.z.ToString() + "," +
                                    radius.ToString();

                    STW.WriteLine(outputLine);

                    if (dlgResult == DialogResult.Yes)
                    {
                        DNSMcVector3D[] circlePts = new DNSMcVector3D[] { circle1stPoint, circle2ndPoint, circle3rdPoint };
                        DrawFunction(false, EDrawnItemType.Circle, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, circlePts, i, 0);
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DCircleFrom3Points - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                if (dlgResult == DialogResult.Yes)
                {
                    DNSMcVector3D[] circlePts = new DNSMcVector3D[] { circle1stPoint, circle2ndPoint, circle3rdPoint };
                    DrawFunction(false, EDrawnItemType.Point, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, circlePts, i, 0);
                }
            }

            CloseStreams();
            return true;
        }

        private void btnEG2D3PointsFromCircle_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2D3PointsFromCircle_Click(dlgResult))
                EndGeometricCalcAction("2D 3 Points From Circle");
        }

        private bool btnEG2D3PointsFromCircle_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            //Print result header line
            outputLine = "Circle 1st Point (X), Circle 1st Point (Y), Circle 1st Point (Z), " +
                            "Circle 2nd Point (X), Circle 2nd Point (Y), Circle 2nd Point (Z), " +
                            "Circle 3rd Point (X), Circle 3rd Point (Y), Circle 3rd Point (Z)";

            STW.WriteLine(outputLine);

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D center = new DNSMcVector3D();
                center.x = double.Parse(sourceLineValues[0]);
                center.y = double.Parse(sourceLineValues[1]);
                center.z = double.Parse(sourceLineValues[2]);

                double radius = double.Parse(sourceLineValues[3]);

                DNSMcVector3D circle1stPoint = new DNSMcVector3D();
                DNSMcVector3D circle2ndPoint = new DNSMcVector3D();
                DNSMcVector3D circle3rdPoint = new DNSMcVector3D();
                try
                {
                    IDNMcGeometricCalculations.EG2D3PointsFromCircle(ref circle1stPoint,
                                                                        ref circle2ndPoint,
                                                                        ref circle3rdPoint,
                                                                        center,
                                                                        radius);


                    outputLine = circle1stPoint.x.ToString() + "," +
                                    circle1stPoint.y.ToString() + "," +
                                    circle1stPoint.z.ToString() + "," +
                                    circle2ndPoint.x.ToString() + "," +
                                    circle2ndPoint.y.ToString() + "," +
                                    circle2ndPoint.z.ToString() + "," +
                                    circle3rdPoint.x.ToString() + "," +
                                    circle3rdPoint.y.ToString() + "," +
                                    circle3rdPoint.z.ToString();


                    STW.WriteLine(outputLine);

                    if (dlgResult == DialogResult.Yes)
                    {
                        DNSMcVector3D[] circlePts = new DNSMcVector3D[] { circle1stPoint, circle2ndPoint, circle3rdPoint };
                        DrawFunction(false, EDrawnItemType.Point, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, circlePts, i, 0);
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2D3PointsFromCircle - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                if (dlgResult == DialogResult.Yes)
                    DrawCircleBasedOnPointAndRadius(false, center, (float)radius);
            }

            CloseStreams();
            return true;
        }

        private void btnEG2DCircleCircleIntersection_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DCircleCircleIntersection_Click(dlgResult))
                EndGeometricCalcAction("2D Circle Circle Intersection");
        }

        private bool btnEG2DCircleCircleIntersection_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            int maxNumOfVertices = 0;

            List<DNSMcVector3D[]> lIntersectionPoints = new List<DNSMcVector3D[]>();

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D st1stCircle1stPoint = new DNSMcVector3D();
                st1stCircle1stPoint.x = double.Parse(sourceLineValues[0]);
                st1stCircle1stPoint.y = double.Parse(sourceLineValues[1]);
                st1stCircle1stPoint.z = double.Parse(sourceLineValues[2]);


                DNSMcVector3D st1stCircle2ndPoint = new DNSMcVector3D();
                st1stCircle2ndPoint.x = double.Parse(sourceLineValues[3]);
                st1stCircle2ndPoint.y = double.Parse(sourceLineValues[4]);
                st1stCircle2ndPoint.z = double.Parse(sourceLineValues[5]);


                DNSMcVector3D st1stCircle3rdPoint = new DNSMcVector3D();
                st1stCircle3rdPoint.x = double.Parse(sourceLineValues[6]);
                st1stCircle3rdPoint.y = double.Parse(sourceLineValues[7]);
                st1stCircle3rdPoint.z = double.Parse(sourceLineValues[8]);

                DNGEOMETRIC_SHAPE e1stCircleType = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[9]);

                DNSMcVector3D st2ndCircle1stPoint = new DNSMcVector3D();
                st2ndCircle1stPoint.x = double.Parse(sourceLineValues[10]);
                st2ndCircle1stPoint.y = double.Parse(sourceLineValues[11]);
                st2ndCircle1stPoint.z = double.Parse(sourceLineValues[12]);


                DNSMcVector3D st2ndCircle2ndPoint = new DNSMcVector3D();
                st2ndCircle2ndPoint.x = double.Parse(sourceLineValues[13]);
                st2ndCircle2ndPoint.y = double.Parse(sourceLineValues[14]);
                st2ndCircle2ndPoint.z = double.Parse(sourceLineValues[15]);


                DNSMcVector3D st2ndCircle3rdPoint = new DNSMcVector3D();
                st2ndCircle3rdPoint.x = double.Parse(sourceLineValues[16]);
                st2ndCircle3rdPoint.y = double.Parse(sourceLineValues[17]);
                st2ndCircle3rdPoint.z = double.Parse(sourceLineValues[18]);

                DNGEOMETRIC_SHAPE e2ndCircleType = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[19]);

                DNSMcVector3D[] intersectionPoints = new DNSMcVector3D[0];
                //DNSMcVector3D center1 = new DNSMcVector3D();
                //DNSMcVector3D center2 = new DNSMcVector3D();

                try
                {
                    intersectionPoints = IDNMcGeometricCalculations.EG2DCircleCircleIntersection(st1stCircle1stPoint,
                                                                                                    st1stCircle2ndPoint,
                                                                                                    st1stCircle3rdPoint,
                                                                                                    e1stCircleType,
                                                                                                    st2ndCircle1stPoint,
                                                                                                    st2ndCircle2ndPoint,
                                                                                                    st2ndCircle3rdPoint,
                                                                                                    e2ndCircleType);


                    if (dlgResult == DialogResult.Yes)
                        DrawFunction(true, EDrawnItemType.Point, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, intersectionPoints, i, 0);

                    //Fined the longest result in order to match the header line
                    if (intersectionPoints.Length > maxNumOfVertices)
                        maxNumOfVertices = intersectionPoints.Length;

                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DCircleCircleIntersection - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                if (dlgResult == DialogResult.Yes)
                {
                    DNSMcVector3D[] circle1Pts = new DNSMcVector3D[] { st1stCircle1stPoint, st1stCircle2ndPoint, st1stCircle3rdPoint };
                    DrawFunction(true, EDrawnItemType.Circle, e1stCircleType, circle1Pts, i, 0);

                    DNSMcVector3D[] circle2Pts = new DNSMcVector3D[] { st2ndCircle1stPoint, st2ndCircle2ndPoint, st2ndCircle3rdPoint };
                    DrawFunction(true, EDrawnItemType.Circle, e2ndCircleType, circle2Pts, i, 0);
                }

                //Add the result to a result list that will be printed at the end of the process.
                lIntersectionPoints.Add(intersectionPoints);
            }

            //Print result header line
            for (int i = 0; i < maxNumOfVertices; i++)
            {
                outputLine += "X" + i + "," +
                                "Y" + i + "," +
                                "Z" + i + ",";

            }

            STW.WriteLine(outputLine);
            outputLine = "";

            for (int lIdx = 0; lIdx < lIntersectionPoints.Count; lIdx++)
            {
                for (int ArrIdx = 0; ArrIdx < lIntersectionPoints[lIdx].Length; ArrIdx++)
                {
                    outputLine += lIntersectionPoints[lIdx][ArrIdx].x.ToString() + "," +
                                    lIntersectionPoints[lIdx][ArrIdx].y.ToString() + "," +
                                    lIntersectionPoints[lIdx][ArrIdx].z.ToString() + ",";
                }

                STW.WriteLine(outputLine);
                outputLine = "";
            }

            CloseStreams();
            return true;
        }

        private void btnEG2DCircleCircleDistance_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DCircleCircleDistance_Click(dlgResult))
                EndGeometricCalcAction("2D Circle Circle Distance");
        }

        private bool btnEG2DCircleCircleDistance_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            //Print result header line
            outputLine = "Closest On 1st(X), Closest On 1st(Y), Closest On 1st(Z), Closest On 2nd(X), Closest On 2nd(Y), Closest On 2nd(Z), Distance";
            STW.WriteLine(outputLine);

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D st1stCircle1stPoint = new DNSMcVector3D();
                st1stCircle1stPoint.x = double.Parse(sourceLineValues[0]);
                st1stCircle1stPoint.y = double.Parse(sourceLineValues[1]);
                st1stCircle1stPoint.z = double.Parse(sourceLineValues[2]);


                DNSMcVector3D st1stCircle2ndPoint = new DNSMcVector3D();
                st1stCircle2ndPoint.x = double.Parse(sourceLineValues[3]);
                st1stCircle2ndPoint.y = double.Parse(sourceLineValues[4]);
                st1stCircle2ndPoint.z = double.Parse(sourceLineValues[5]);


                DNSMcVector3D st1stCircle3rdPoint = new DNSMcVector3D();
                st1stCircle3rdPoint.x = double.Parse(sourceLineValues[6]);
                st1stCircle3rdPoint.y = double.Parse(sourceLineValues[7]);
                st1stCircle3rdPoint.z = double.Parse(sourceLineValues[8]);

                DNGEOMETRIC_SHAPE e1stCircleType = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[9]);

                DNSMcVector3D st2ndCircle1stPoint = new DNSMcVector3D();
                st2ndCircle1stPoint.x = double.Parse(sourceLineValues[10]);
                st2ndCircle1stPoint.y = double.Parse(sourceLineValues[11]);
                st2ndCircle1stPoint.z = double.Parse(sourceLineValues[12]);


                DNSMcVector3D st2ndCircle2ndPoint = new DNSMcVector3D();
                st2ndCircle2ndPoint.x = double.Parse(sourceLineValues[13]);
                st2ndCircle2ndPoint.y = double.Parse(sourceLineValues[14]);
                st2ndCircle2ndPoint.z = double.Parse(sourceLineValues[15]);


                DNSMcVector3D st2ndCircle3rdPoint = new DNSMcVector3D();
                st2ndCircle3rdPoint.x = double.Parse(sourceLineValues[16]);
                st2ndCircle3rdPoint.y = double.Parse(sourceLineValues[17]);
                st2ndCircle3rdPoint.z = double.Parse(sourceLineValues[18]);

                DNGEOMETRIC_SHAPE e2ndCircleType = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[19]);

                DNSMcVector3D closestOn1st = new DNSMcVector3D();
                DNSMcVector3D closestOn2nd = new DNSMcVector3D();
                double distance = 0;
                IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;
                try
                {
                    IDNMcGeometricCalculations.EG2DCircleCircleDistance(st1stCircle1stPoint,
                                                                                st1stCircle2ndPoint,
                                                                                st1stCircle3rdPoint,
                                                                                e1stCircleType,
                                                                                st2ndCircle1stPoint,
                                                                                st2ndCircle2ndPoint,
                                                                                st2ndCircle3rdPoint,
                                                                                e2ndCircleType,
                                                                                ref closestOn1st,
                                                                                ref closestOn2nd,
                                                                                ref distance);



                    outputLine = closestOn1st.x.ToString() + "," +
                                    closestOn1st.y.ToString() + "," +
                                    closestOn1st.z.ToString() + "," +
                                    closestOn2nd.x.ToString() + "," +
                                    closestOn2nd.y.ToString() + "," +
                                    closestOn2nd.z.ToString() + "," +
                                    distance.ToString();

                    STW.WriteLine(outputLine);

                    if (dlgResult == DialogResult.Yes && activeOverlay != null)
                    {
                        DNSMcVector3D[] distanceBetweenCircles = new DNSMcVector3D[] { closestOn1st, closestOn2nd };
                        DrawFunction(false, EDrawnItemType.Connector, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, distanceBetweenCircles, i, distance);
                        DrawFunction(false, EDrawnItemType.Point, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, distanceBetweenCircles, i, 0);
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DCircleCircleDistance - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                if (activeOverlay != null && dlgResult == DialogResult.Yes)
                {
                    DNSMcVector3D[] circle1Pts = new DNSMcVector3D[] { st1stCircle1stPoint, st1stCircle2ndPoint, st1stCircle3rdPoint };
                    DrawFunction(true, EDrawnItemType.Circle, e1stCircleType, circle1Pts, i, 0);

                    DNSMcVector3D[] circle2Pts = new DNSMcVector3D[] { st2ndCircle1stPoint, st2ndCircle2ndPoint, st2ndCircle3rdPoint };
                    DrawFunction(true, EDrawnItemType.Circle, e2ndCircleType, circle2Pts, i, 0);
                }
            }

            CloseStreams();
            return true;
        }

        private void btnEG2DCircleLineIntersection_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DCircleLineIntersection_Click(dlgResult))
                EndGeometricCalcAction("2D Circle Line Intersection");
        }

        private bool btnEG2DCircleLineIntersection_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            int maxNumOfVertices = 0;

            List<DNSMcVector3D[]> lIntersectionPoints = new List<DNSMcVector3D[]>();

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D st1stCircle1stPoint = new DNSMcVector3D();
                st1stCircle1stPoint.x = double.Parse(sourceLineValues[0]);
                st1stCircle1stPoint.y = double.Parse(sourceLineValues[1]);
                st1stCircle1stPoint.z = double.Parse(sourceLineValues[2]);

                DNSMcVector3D st1stCircle2ndPoint = new DNSMcVector3D();
                st1stCircle2ndPoint.x = double.Parse(sourceLineValues[3]);
                st1stCircle2ndPoint.y = double.Parse(sourceLineValues[4]);
                st1stCircle2ndPoint.z = double.Parse(sourceLineValues[5]);

                DNSMcVector3D st1stCircle3rdPoint = new DNSMcVector3D();
                st1stCircle3rdPoint.x = double.Parse(sourceLineValues[6]);
                st1stCircle3rdPoint.y = double.Parse(sourceLineValues[7]);
                st1stCircle3rdPoint.z = double.Parse(sourceLineValues[8]);

                DNGEOMETRIC_SHAPE e1stCircleType = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[9]);
                DNGEOMETRIC_SHAPE eLineType = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[10]);

                List<DNSMcVector3D> linePts = new List<DNSMcVector3D>();
                DNSMcVector3D point = new DNSMcVector3D();
                for (int cell = 11; cell < sourceLineValues.Length; cell += 3)
                {
                    point.x = double.Parse(sourceLineValues[cell]);
                    point.y = double.Parse(sourceLineValues[cell + 1]);
                    point.z = double.Parse(sourceLineValues[cell + 2]);

                    linePts.Add(point);
                }

                DNSMcVector3D[] intersectionPoints = new DNSMcVector3D[0];
                //DNSMcVector3D center1 = new DNSMcVector3D();
                //double radius1 = 0;
                IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;
                try
                {
                    intersectionPoints = IDNMcGeometricCalculations.EG2DCircleLineIntersection(st1stCircle1stPoint,
                                                                                                st1stCircle2ndPoint,
                                                                                                st1stCircle3rdPoint,
                                                                                                e1stCircleType,
                                                                                                linePts.ToArray(),
                                                                                                eLineType);


                    if (dlgResult == DialogResult.Yes)
                        DrawFunction(true, EDrawnItemType.Point, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, intersectionPoints, i, 0);

                    //Fined the longest result in order to match the header line
                    if (intersectionPoints.Length > maxNumOfVertices)
                        maxNumOfVertices = intersectionPoints.Length;

                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DCircleLineIntersection - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                if (dlgResult == DialogResult.Yes)
                {
                    DNSMcVector3D[] circle1Pts = new DNSMcVector3D[] { st1stCircle1stPoint, st1stCircle2ndPoint, st1stCircle3rdPoint };
                    DrawFunction(true, EDrawnItemType.Circle, e1stCircleType, circle1Pts, i, 0);

                    DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, linePts.ToArray(), i, 0);
                }

                //Add the result to a result list that will be printed at the end of the process.
                lIntersectionPoints.Add(intersectionPoints);
            }

            //Print result header line
            for (int i = 0; i < maxNumOfVertices; i++)
            {
                outputLine += "X" + i + "," +
                                "Y" + i + "," +
                                "Z" + i + ",";

            }

            STW.WriteLine(outputLine);
            outputLine = "";

            for (int lIdx = 0; lIdx < lIntersectionPoints.Count; lIdx++)
            {
                for (int ArrIdx = 0; ArrIdx < lIntersectionPoints[lIdx].Length; ArrIdx++)
                {
                    outputLine += lIntersectionPoints[lIdx][ArrIdx].x.ToString() + "," +
                                    lIntersectionPoints[lIdx][ArrIdx].y.ToString() + "," +
                                    lIntersectionPoints[lIdx][ArrIdx].z.ToString() + ",";
                }

                STW.WriteLine(outputLine);
                outputLine = "";
            }

            CloseStreams();
            return true;
        }

        private void btnEG2DCircleLineDistance_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DCircleLineDistance_Click(dlgResult))
                EndGeometricCalcAction("2D Circle Line Distance");
        }

        private bool btnEG2DCircleLineDistance_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            //Print result header line
            STW.WriteLine("Closest On Circle(X), Closest On Circle(Y), Closest On Circle(Z), Closest On Line(X), Closest On Line(Y), Closest On Line(Z), Distance");

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D st1stCircle1stPoint = new DNSMcVector3D();
                st1stCircle1stPoint.x = double.Parse(sourceLineValues[0]);
                st1stCircle1stPoint.y = double.Parse(sourceLineValues[1]);
                st1stCircle1stPoint.z = double.Parse(sourceLineValues[2]);

                DNSMcVector3D st1stCircle2ndPoint = new DNSMcVector3D();
                st1stCircle2ndPoint.x = double.Parse(sourceLineValues[3]);
                st1stCircle2ndPoint.y = double.Parse(sourceLineValues[4]);
                st1stCircle2ndPoint.z = double.Parse(sourceLineValues[5]);

                DNSMcVector3D st1stCircle3rdPoint = new DNSMcVector3D();
                st1stCircle3rdPoint.x = double.Parse(sourceLineValues[6]);
                st1stCircle3rdPoint.y = double.Parse(sourceLineValues[7]);
                st1stCircle3rdPoint.z = double.Parse(sourceLineValues[8]);

                DNGEOMETRIC_SHAPE e1stCircleType = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[9]);
                DNGEOMETRIC_SHAPE eLineType = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[10]);

                List<DNSMcVector3D> linePts = new List<DNSMcVector3D>();
                DNSMcVector3D point = new DNSMcVector3D();
                for (int cell = 11; cell < sourceLineValues.Length; cell += 3)
                {
                    point.x = double.Parse(sourceLineValues[cell]);
                    point.y = double.Parse(sourceLineValues[cell + 1]);
                    point.z = double.Parse(sourceLineValues[cell + 2]);

                    linePts.Add(point);
                }

                DNSMcVector3D[] intersectionPoints = new DNSMcVector3D[0];
                // DNSMcVector3D center1 = new DNSMcVector3D();
                //double radius1 = 0;
                DNSMcVector3D pstClosestOnCircle = new DNSMcVector3D();
                DNSMcVector3D pstClosestOnLine = new DNSMcVector3D();
                double distance = 0;

                IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;
                try
                {
                    IDNMcGeometricCalculations.EG2DCircleLineDistance(st1stCircle1stPoint,
                                                                        st1stCircle2ndPoint,
                                                                        st1stCircle3rdPoint,
                                                                        e1stCircleType,
                                                                        linePts.ToArray(),
                                                                        eLineType,
                                                                        ref pstClosestOnCircle,
                                                                        ref pstClosestOnLine,
                                                                        ref distance);


                    // print result to CSV file
                    STW.WriteLine(pstClosestOnCircle.x.ToString() + "," +
                                    pstClosestOnCircle.y.ToString() + "," +
                                    pstClosestOnCircle.z.ToString() + "," +
                                    pstClosestOnLine.x.ToString() + "," +
                                    pstClosestOnLine.y.ToString() + "," +
                                    pstClosestOnLine.z.ToString() + "," +
                                    distance.ToString());


                    if (dlgResult == DialogResult.Yes && activeOverlay != null)
                    {
                        DNSMcVector3D[] distanceBetweenItems = new DNSMcVector3D[] { pstClosestOnCircle, pstClosestOnLine };
                        DrawFunction(false, EDrawnItemType.Connector, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, distanceBetweenItems, i, distance);
                        DrawFunction(false, EDrawnItemType.Point, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, distanceBetweenItems, i, 0);
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DCircleLineDistance - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                if (activeOverlay != null && dlgResult == DialogResult.Yes)
                {
                    DNSMcVector3D[] circle1Pts = new DNSMcVector3D[] { st1stCircle1stPoint, st1stCircle2ndPoint, st1stCircle3rdPoint };
                    DrawFunction(true, EDrawnItemType.Circle, e1stCircleType, circle1Pts, i, 0);
                    DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, linePts.ToArray(), i, 0);
                }
            }

            CloseStreams();
            return true;
        }

        private void btnEG2DCirclePointDistance_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DCirclePointDistance_Click(dlgResult))
                EndGeometricCalcAction("2D Circle Point Distance");
        }

        private bool btnEG2DCirclePointDistance_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            //Print result header line
            STW.WriteLine("Closest On Circle(X), Closest On Circle(Y), Closest On Circle(Z), Distance");

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D st1stCircle1stPoint = new DNSMcVector3D();
                st1stCircle1stPoint.x = double.Parse(sourceLineValues[0]);
                st1stCircle1stPoint.y = double.Parse(sourceLineValues[1]);
                st1stCircle1stPoint.z = double.Parse(sourceLineValues[2]);

                DNSMcVector3D st1stCircle2ndPoint = new DNSMcVector3D();
                st1stCircle2ndPoint.x = double.Parse(sourceLineValues[3]);
                st1stCircle2ndPoint.y = double.Parse(sourceLineValues[4]);
                st1stCircle2ndPoint.z = double.Parse(sourceLineValues[5]);

                DNSMcVector3D st1stCircle3rdPoint = new DNSMcVector3D();
                st1stCircle3rdPoint.x = double.Parse(sourceLineValues[6]);
                st1stCircle3rdPoint.y = double.Parse(sourceLineValues[7]);
                st1stCircle3rdPoint.z = double.Parse(sourceLineValues[8]);

                DNGEOMETRIC_SHAPE e1stCircleType = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[9]);

                DNSMcVector3D stPoint = new DNSMcVector3D();
                stPoint.x = double.Parse(sourceLineValues[10]);
                stPoint.y = double.Parse(sourceLineValues[11]);
                stPoint.z = double.Parse(sourceLineValues[12]);


                //DNSMcVector3D center = new DNSMcVector3D();
                //double radius = 0;
                DNSMcVector3D pstClosestOnCircle = new DNSMcVector3D();
                double distance = 0;

                IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;
                try
                {
                    IDNMcGeometricCalculations.EG2DCirclePointDistance(st1stCircle2ndPoint,
                                                                        st1stCircle2ndPoint,
                                                                        st1stCircle3rdPoint,
                                                                        e1stCircleType,
                                                                        stPoint,
                                                                        ref pstClosestOnCircle,
                                                                        ref distance);


                    // print result to CSV file
                    STW.WriteLine(pstClosestOnCircle.x.ToString() + "," +
                                    pstClosestOnCircle.y.ToString() + "," +
                                    pstClosestOnCircle.z.ToString() + "," +
                                    distance.ToString());


                    if (dlgResult == DialogResult.Yes)
                    {
                        DNSMcVector3D[] closestPt = new DNSMcVector3D[] { pstClosestOnCircle, stPoint };
                        DrawFunction(false, EDrawnItemType.Connector, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, closestPt, i, distance);
                        DrawFunction(false, EDrawnItemType.Point, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, closestPt, i, 0);
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DCirclePointDistance - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                if (dlgResult == DialogResult.Yes)
                {
                    DNSMcVector3D[] circlePts = new DNSMcVector3D[] { st1stCircle2ndPoint, st1stCircle2ndPoint, st1stCircle3rdPoint };
                    DrawFunction(true, EDrawnItemType.Circle, e1stCircleType, circlePts, i, 0);
                }
            }

            CloseStreams();
            return true;
        }

        private void btnEG2DIsPointOnCircle_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DIsPointOnCircle_Click(dlgResult))
                EndGeometricCalcAction("2D Is Point On Circle");
        }

        private bool btnEG2DIsPointOnCircle_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            STW.WriteLine("Point Is On Circle");

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D st1stCircle1stPoint = new DNSMcVector3D();
                st1stCircle1stPoint.x = double.Parse(sourceLineValues[0]);
                st1stCircle1stPoint.y = double.Parse(sourceLineValues[1]);
                st1stCircle1stPoint.z = double.Parse(sourceLineValues[2]);

                DNSMcVector3D st1stCircle2ndPoint = new DNSMcVector3D();
                st1stCircle2ndPoint.x = double.Parse(sourceLineValues[3]);
                st1stCircle2ndPoint.y = double.Parse(sourceLineValues[4]);
                st1stCircle2ndPoint.z = double.Parse(sourceLineValues[5]);

                DNSMcVector3D st1stCircle3rdPoint = new DNSMcVector3D();
                st1stCircle3rdPoint.x = double.Parse(sourceLineValues[6]);
                st1stCircle3rdPoint.y = double.Parse(sourceLineValues[7]);
                st1stCircle3rdPoint.z = double.Parse(sourceLineValues[8]);

                DNGEOMETRIC_SHAPE e1stCircleType = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[9]);

                DNSMcVector3D stPoint = new DNSMcVector3D();
                stPoint.x = double.Parse(sourceLineValues[10]);
                stPoint.y = double.Parse(sourceLineValues[11]);
                stPoint.z = double.Parse(sourceLineValues[12]);

                double dAccuracy = double.Parse(sourceLineValues[13]);

                //DNSMcVector3D center = new DNSMcVector3D();
                //double radius = 0;
                IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;
                try
                {
                    bool isOn = IDNMcGeometricCalculations.EG2DIsPointOnCircle(stPoint,
                                                                                    st1stCircle1stPoint,
                                                                                    st1stCircle2ndPoint,
                                                                                    st1stCircle3rdPoint,
                                                                                    e1stCircleType,
                                                                                    dAccuracy);


                    // print result to CSV file
                    STW.WriteLine(isOn.ToString());

                    if (dlgResult == DialogResult.Yes)
                        DrawCircleBasedOnPointAndRadius(false, stPoint, (float)dAccuracy);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DIsPointOnCircle - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                if (dlgResult == DialogResult.Yes)
                {
                    DNSMcVector3D[] originPt = new DNSMcVector3D[] { stPoint };
                    DNSMcVector3D[] circlePts = new DNSMcVector3D[] { st1stCircle1stPoint, st1stCircle2ndPoint, st1stCircle3rdPoint };
                    DrawFunction(true, EDrawnItemType.Point, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, originPt, i, 0);
                    DrawFunction(true, EDrawnItemType.Circle, e1stCircleType, circlePts, i, 0);
                }
            }

            CloseStreams();
            return true;
        }

        private void btnEG2DIsPointInCircle_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DIsPointInCircle_Click(dlgResult))
                EndGeometricCalcAction("2D Is Point In Circle");
        }

        private bool btnEG2DIsPointInCircle_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            STW.WriteLine("Point-Circle Relation");

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D st1stCircle1stPoint = new DNSMcVector3D();
                st1stCircle1stPoint.x = double.Parse(sourceLineValues[0]);
                st1stCircle1stPoint.y = double.Parse(sourceLineValues[1]);
                st1stCircle1stPoint.z = double.Parse(sourceLineValues[2]);

                DNSMcVector3D st1stCircle2ndPoint = new DNSMcVector3D();
                st1stCircle2ndPoint.x = double.Parse(sourceLineValues[3]);
                st1stCircle2ndPoint.y = double.Parse(sourceLineValues[4]);
                st1stCircle2ndPoint.z = double.Parse(sourceLineValues[5]);

                DNSMcVector3D st1stCircle3rdPoint = new DNSMcVector3D();
                st1stCircle3rdPoint.x = double.Parse(sourceLineValues[6]);
                st1stCircle3rdPoint.y = double.Parse(sourceLineValues[7]);
                st1stCircle3rdPoint.z = double.Parse(sourceLineValues[8]);

                DNGEOMETRIC_SHAPE e1stCircleType = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[9]);

                DNSMcVector3D stPoint = new DNSMcVector3D();
                stPoint.x = double.Parse(sourceLineValues[10]);
                stPoint.y = double.Parse(sourceLineValues[11]);
                stPoint.z = double.Parse(sourceLineValues[12]);

                //DNSMcVector3D center = new DNSMcVector3D();
                //double radius = 0;
                IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;
                try
                {
                    DNPOINT_PG_STATUS isIn = IDNMcGeometricCalculations.EG2DIsPointInCircle(stPoint,
                                                                                                st1stCircle1stPoint,
                                                                                                st1stCircle2ndPoint,
                                                                                                st1stCircle3rdPoint,
                                                                                                e1stCircleType);


                    // print result to CSV file
                    STW.WriteLine(isIn.ToString());
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DIsPointInCircle - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                if (dlgResult == DialogResult.Yes)
                {
                    DNSMcVector3D[] originPt = new DNSMcVector3D[] { stPoint };
                    DNSMcVector3D[] circlePts = new DNSMcVector3D[] { st1stCircle1stPoint, st1stCircle2ndPoint, st1stCircle3rdPoint };
                    DrawFunction(true, EDrawnItemType.Point, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, originPt, i, 0);
                    DrawFunction(true, EDrawnItemType.Circle, e1stCircleType, circlePts, i, 0);
                }
            }

            CloseStreams();
            return true;
        }

        private void btnEG2DCircleBoundingRect_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DCircleBoundingRect_Click(dlgResult))
                EndGeometricCalcAction("2D Circle Bounding Rect");
        }

        private bool btnEG2DCircleBoundingRect_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            STW.WriteLine("Point-Circle Relation");

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D st1stCircle1stPoint = new DNSMcVector3D();
                st1stCircle1stPoint.x = double.Parse(sourceLineValues[0]);
                st1stCircle1stPoint.y = double.Parse(sourceLineValues[1]);
                st1stCircle1stPoint.z = double.Parse(sourceLineValues[2]);

                DNSMcVector3D st1stCircle2ndPoint = new DNSMcVector3D();
                st1stCircle2ndPoint.x = double.Parse(sourceLineValues[3]);
                st1stCircle2ndPoint.y = double.Parse(sourceLineValues[4]);
                st1stCircle2ndPoint.z = double.Parse(sourceLineValues[5]);

                DNSMcVector3D st1stCircle3rdPoint = new DNSMcVector3D();
                st1stCircle3rdPoint.x = double.Parse(sourceLineValues[6]);
                st1stCircle3rdPoint.y = double.Parse(sourceLineValues[7]);
                st1stCircle3rdPoint.z = double.Parse(sourceLineValues[8]);

                DNGEOMETRIC_SHAPE e1stCircleType = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[9]);

                // DNSMcVector3D center = new DNSMcVector3D();
                // double radius = 0;
                double pdLeft = 0;
                double pdRight = 0;
                double pdDown = 0;
                double pdUp = 0;

                try
                {
                    IDNMcGeometricCalculations.EG2DCircleBoundingRect(st1stCircle1stPoint,
                                                                        st1stCircle2ndPoint,
                                                                        st1stCircle3rdPoint,
                                                                        e1stCircleType,
                                                                        ref pdLeft,
                                                                        ref pdRight,
                                                                        ref pdDown,
                                                                        ref pdUp);


                    // print result to CSV file
                    STW.WriteLine(pdLeft.ToString() + "," +
                                    pdRight.ToString() + "," +
                                    pdDown.ToString() + "," +
                                    pdUp.ToString());

                    if (dlgResult == DialogResult.Yes)
                    {
                        DNSMcVector3D[] boundingRectPts = new DNSMcVector3D[4];
                        boundingRectPts[0].x = boundingRectPts[3].x = pdLeft;
                        boundingRectPts[1].x = boundingRectPts[2].x = pdRight;
                        boundingRectPts[0].y = boundingRectPts[1].y = pdUp;
                        boundingRectPts[2].y = boundingRectPts[3].y = pdDown;

                        DrawFunction(false, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, boundingRectPts, i, 0);
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DCircleBoundingRect - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                if (dlgResult == DialogResult.Yes)
                {
                    DNSMcVector3D[] circlePts = new DNSMcVector3D[] { st1stCircle1stPoint, st1stCircle2ndPoint, st1stCircle3rdPoint };
                    DrawFunction(true, EDrawnItemType.Circle, e1stCircleType, circlePts, i, 0);
                }
            }

            CloseStreams();
            return true;
        }

        private void btnEG2DCircleSample_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DCircleSample_Click(dlgResult))
                EndGeometricCalcAction("2D Circle Sample");
        }

        private bool btnEG2DCircleSample_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            int maxNumOfVertices = 0;
            List<DNSMcVector3D[]> lIntersectionPoints = new List<DNSMcVector3D[]>();

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D st1stCircle1stPoint = new DNSMcVector3D();
                st1stCircle1stPoint.x = double.Parse(sourceLineValues[0]);
                st1stCircle1stPoint.y = double.Parse(sourceLineValues[1]);
                st1stCircle1stPoint.z = double.Parse(sourceLineValues[2]);

                DNSMcVector3D st1stCircle2ndPoint = new DNSMcVector3D();
                st1stCircle2ndPoint.x = double.Parse(sourceLineValues[3]);
                st1stCircle2ndPoint.y = double.Parse(sourceLineValues[4]);
                st1stCircle2ndPoint.z = double.Parse(sourceLineValues[5]);

                DNSMcVector3D st1stCircle3rdPoint = new DNSMcVector3D();
                st1stCircle3rdPoint.x = double.Parse(sourceLineValues[6]);
                st1stCircle3rdPoint.y = double.Parse(sourceLineValues[7]);
                st1stCircle3rdPoint.z = double.Parse(sourceLineValues[8]);

                DNGEOMETRIC_SHAPE e1stCircleType = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[9]);

                uint unNumOfPoints = uint.Parse(sourceLineValues[10]);

                // DNSMcVector3D center = new DNSMcVector3D();
                // double radius = 0;
                DNSMcVector3D[] pstSamplingPoints = new DNSMcVector3D[0];
                try
                {
                    pstSamplingPoints = IDNMcGeometricCalculations.EG2DCircleSample(st1stCircle1stPoint,
                                                                                        st1stCircle2ndPoint,
                                                                                        st1stCircle3rdPoint,
                                                                                        e1stCircleType,
                                                                                        unNumOfPoints);



                    if (dlgResult == DialogResult.Yes)
                        DrawFunction(false, EDrawnItemType.Point, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, pstSamplingPoints, i, 0);

                    //Fined the longest result in order to match the header line
                    if (pstSamplingPoints.Length > maxNumOfVertices)
                        maxNumOfVertices = pstSamplingPoints.Length;

                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DCircleSample - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                if (dlgResult == DialogResult.Yes)
                {
                    DNSMcVector3D[] circlePts = new DNSMcVector3D[] { st1stCircle1stPoint, st1stCircle2ndPoint, st1stCircle3rdPoint };
                    DrawFunction(true, EDrawnItemType.Circle, e1stCircleType, circlePts, i, 0);
                }

                //Add the result to a result list that will be printed at the end of the process.
                lIntersectionPoints.Add(pstSamplingPoints);
            }

            //Print result header line
            for (int i = 0; i < maxNumOfVertices; i++)
            {
                outputLine += "X" + i + "," +
                                "Y" + i + "," +
                                "Z" + i + ",";

            }

            STW.WriteLine(outputLine);
            outputLine = "";

            for (int lIdx = 0; lIdx < lIntersectionPoints.Count; lIdx++)
            {
                for (int ArrIdx = 0; ArrIdx < lIntersectionPoints[lIdx].Length; ArrIdx++)
                {
                    outputLine += lIntersectionPoints[lIdx][ArrIdx].x.ToString() + "," +
                                    lIntersectionPoints[lIdx][ArrIdx].y.ToString() + "," +
                                    lIntersectionPoints[lIdx][ArrIdx].z.ToString() + ",";
                }

                STW.WriteLine(outputLine);
                outputLine = "";
            }

            CloseStreams();
            return true;
        }
        #endregion

        #region PolyLine Functions
        private void btnEG2DPolyLineMove_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DPolyLineMove_Click(dlgResult))
                EndGeometricCalcAction("2D PolyLine Move");
        }

        private bool btnEG2DPolyLineMove_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            int maxNumOfVertices = 0;
            List<DNSMcVector3D[]> lPolylinePoints = new List<DNSMcVector3D[]>();

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');
                DNSMcVector3D[] polyLinePoints = new DNSMcVector3D[(uint)((sourceLineValues.Length - 2) / 3)];


                double dX = double.Parse(sourceLineValues[0]);
                double dY = double.Parse(sourceLineValues[1]);

                int idx = 0;
                for (int cell = 2; cell < sourceLineValues.Length - 2; cell += 3)
                {
                    polyLinePoints[idx].x = double.Parse(sourceLineValues[cell]);
                    polyLinePoints[idx].y = double.Parse(sourceLineValues[cell + 1]);
                    polyLinePoints[idx].z = double.Parse(sourceLineValues[cell + 2]);

                    idx++;
                }

                if (dlgResult == DialogResult.Yes)
                    DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, polyLinePoints, i, 0);

                try
                {
                    IDNMcGeometricCalculations.EG2DPolyLineMove(ref polyLinePoints,
                                                                    dX,
                                                                    dY);


                    if (dlgResult == DialogResult.Yes)
                        DrawFunction(false, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, polyLinePoints, i, 0);


                    //Fined the longest result in order to match the header line
                    if (polyLinePoints.Length > maxNumOfVertices)
                        maxNumOfVertices = polyLinePoints.Length;
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DPolyLineMove - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                //Add the result to a result list that will be printed at the end of the process.
                lPolylinePoints.Add(polyLinePoints);
            }

            //Print result header line
            for (int i = 0; i < maxNumOfVertices; i++)
            {
                outputLine += "X" + i + "," +
                                "Y" + i + "," +
                                "Z" + i + ",";

            }

            STW.WriteLine(outputLine);
            outputLine = "";

            for (int lIdx = 0; lIdx < lPolylinePoints.Count; lIdx++)
            {
                for (int ArrIdx = 0; ArrIdx < lPolylinePoints[lIdx].Length; ArrIdx++)
                {
                    outputLine += lPolylinePoints[lIdx][ArrIdx].x.ToString() + "," +
                                    lPolylinePoints[lIdx][ArrIdx].y.ToString() + "," +
                                    lPolylinePoints[lIdx][ArrIdx].z.ToString() + ",";
                }

                STW.WriteLine(outputLine);
                outputLine = "";
            }


            CloseStreams();
            return true;
        }

        private void btnEG2DPolyLineRotate_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DPolyLineRotate_Click(dlgResult))
                EndGeometricCalcAction("2D Poly Line Rotate");
        }

        private bool btnEG2DPolyLineRotate_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            int maxNumOfVertices = 0;
            List<DNSMcVector3D[]> lPolylinePoints = new List<DNSMcVector3D[]>();

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');
                DNSMcVector3D[] polyLinePoints = new DNSMcVector3D[(uint)((sourceLineValues.Length - 4) / 3)];

                DNSMcVector3D basePoint = new DNSMcVector3D();
                basePoint.x = double.Parse(sourceLineValues[0]);
                basePoint.y = double.Parse(sourceLineValues[1]);
                basePoint.z = double.Parse(sourceLineValues[2]);

                double dAngle = double.Parse(sourceLineValues[3]);

                int idx = 0;
                for (int cell = 4; cell < sourceLineValues.Length; cell += 3)
                {
                    polyLinePoints[idx].x = double.Parse(sourceLineValues[cell]);
                    polyLinePoints[idx].y = double.Parse(sourceLineValues[cell + 1]);
                    polyLinePoints[idx].z = double.Parse(sourceLineValues[cell + 2]);

                    idx++;
                }

                // draw source line
                if (dlgResult == DialogResult.Yes)
                {
                    DNSMcVector3D[] basePt = new DNSMcVector3D[] { basePoint };
                    DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, polyLinePoints, i, 0);
                    DrawFunction(true, EDrawnItemType.Point, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, basePt, i, 0);
                }

                try
                {
                    IDNMcGeometricCalculations.EG2DPolyLineRotate(ref polyLinePoints,
                                                                    dAngle,
                                                                    basePoint);

                    //Fined the longest result in order to match the header line
                    if (polyLinePoints.Length > maxNumOfVertices)
                        maxNumOfVertices = polyLinePoints.Length;

                    if (dlgResult == DialogResult.Yes)
                        DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, polyLinePoints, i, 0);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DPolyLineRotate - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                //Add the result to a result list that will be printed at the end of the process.
                lPolylinePoints.Add(polyLinePoints);
            }

            //Print result header line
            for (int i = 0; i < maxNumOfVertices; i++)
            {
                outputLine += "X" + i + "," +
                                "Y" + i + "," +
                                "Z" + i + ",";

            }

            STW.WriteLine(outputLine);
            outputLine = "";

            for (int lIdx = 0; lIdx < lPolylinePoints.Count; lIdx++)
            {
                for (int ArrIdx = 0; ArrIdx < lPolylinePoints[lIdx].Length; ArrIdx++)
                {
                    outputLine += lPolylinePoints[lIdx][ArrIdx].x.ToString() + "," +
                                    lPolylinePoints[lIdx][ArrIdx].y.ToString() + "," +
                                    lPolylinePoints[lIdx][ArrIdx].z.ToString() + ",";
                }

                STW.WriteLine(outputLine);
                outputLine = "";
            }

            CloseStreams();
            return true;
        }

        private void btnEGPolyLinesRelation_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEGPolyLinesRelation_Click(dlgResult))
                EndGeometricCalcAction("PolyLines Relation");
        }

        private bool btnEGPolyLinesRelation_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            int maxNumOfVertices = 0;
            List<DNSMcVector3D[]> lIntersectionPoints = new List<DNSMcVector3D[]>();
            List<DNPL_PL_STATUS> lCrossResult = new List<DNPL_PL_STATUS>();

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');
                DNSMcVector3D[] intersectionPoints = new DNSMcVector3D[0];
                DNPL_PL_STATUS crossResult = new DNPL_PL_STATUS();

                uint dimension = uint.Parse(sourceLineValues[0]);
                uint pointsNumLine1 = uint.Parse(sourceLineValues[1]);

                DNSMcVector3D[] stPolyLine1 = new DNSMcVector3D[pointsNumLine1];
                int idx = 0;
                for (int cell = 2; cell < pointsNumLine1 * 3; cell += 3)
                {
                    stPolyLine1[idx].x = double.Parse(sourceLineValues[cell]);
                    stPolyLine1[idx].y = double.Parse(sourceLineValues[cell + 1]);
                    stPolyLine1[idx].z = double.Parse(sourceLineValues[cell + 2]);

                    idx++;
                }


                // read line 2 points
                int numPtCellPosition = (2 + (int)pointsNumLine1 * 3);
                uint pointsNumLine2 = uint.Parse(sourceLineValues[numPtCellPosition]);
                DNSMcVector3D[] stPolyLine2 = new DNSMcVector3D[pointsNumLine2];
                idx = 0;
                for (int cell = (numPtCellPosition + 1); cell < sourceLineValues.Length; cell += 3)
                {
                    stPolyLine2[idx].x = double.Parse(sourceLineValues[cell]);
                    stPolyLine2[idx].y = double.Parse(sourceLineValues[cell + 1]);
                    stPolyLine2[idx].z = double.Parse(sourceLineValues[cell + 2]);

                    idx++;
                }

                // draw source line
                if (dlgResult == DialogResult.Yes)
                {
                    DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPolyLine1, i, 0);
                    DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPolyLine2, i, 0);
                }

                try
                {
                    IDNMcGeometricCalculations.EGPolyLinesRelation(stPolyLine1,
                                                                    stPolyLine2,
                                                                    ref intersectionPoints,
                                                                    ref crossResult,
                                                                    dimension);


                    //Fined the longest result in order to match the header line
                    if (intersectionPoints.Length > maxNumOfVertices)
                        maxNumOfVertices = intersectionPoints.Length;

                    if (dlgResult == DialogResult.Yes)
                        DrawFunction(false, EDrawnItemType.Point, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, intersectionPoints, i, 0);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EGPolyLinesRelation - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                //Add the result to a result list that will be printed at the end of the process.
                lIntersectionPoints.Add(intersectionPoints);
                lCrossResult.Add(crossResult);
            }

            //Print result header line
            outputLine += "Cross Result,";
            for (int i = 0; i < maxNumOfVertices; i++)
            {
                outputLine += "Intersection X" + i + "," +
                                "Intersection Y" + i + "," +
                                "Intersection Z" + i + ",";

            }

            STW.WriteLine(outputLine);
            outputLine = "";

            for (int lIdx = 0; lIdx < lIntersectionPoints.Count; lIdx++)
            {
                outputLine += lCrossResult[lIdx].ToString();

                for (int ArrIdx = 0; ArrIdx < lIntersectionPoints[lIdx].Length; ArrIdx++)
                {
                    outputLine += "," + lIntersectionPoints[lIdx][ArrIdx].x.ToString() + "," +
                                    lIntersectionPoints[lIdx][ArrIdx].y.ToString() + "," +
                                    lIntersectionPoints[lIdx][ArrIdx].z.ToString() + ",";
                }

                STW.WriteLine(outputLine);
                outputLine = "";
            }

            CloseStreams();
            return true;
        }

        #endregion

        #region Polylines And Polygons

        private void btnEG2DIsPointOnPoly_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DIsPointOnPoly_Click(dlgResult))
                EndGeometricCalcAction("2D Is Point On Poly");
        }

        private bool btnEG2DIsPointOnPoly_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            List<string> lStatusResults = new List<string>();

            //Print result header line
            STW.WriteLine("Is On");

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D point = new DNSMcVector3D();
                point.x = double.Parse(sourceLineValues[0]);
                point.y = double.Parse(sourceLineValues[1]);
                point.z = double.Parse(sourceLineValues[2]);

                double accuracy = double.Parse(sourceLineValues[3]);
                DNGEOMETRIC_SHAPE polyType = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[4]);

                int shapeNumPt = (sourceLineValues.Length - 5) / 3;
                DNSMcVector3D[] stPoly = new DNSMcVector3D[shapeNumPt];
                int idx = 0;

                for (int cell = 5; cell < sourceLineValues.Length; cell += 3)
                {
                    stPoly[idx].x = double.Parse(sourceLineValues[cell]);
                    stPoly[idx].y = double.Parse(sourceLineValues[cell + 1]);
                    stPoly[idx].z = double.Parse(sourceLineValues[cell + 2]);

                    idx++;
                }

                try
                {
                    bool statusResult = IDNMcGeometricCalculations.EG2DIsPointOnPoly(point,
                                                                                        stPoly,
                                                                                        polyType,
                                                                                        accuracy);


                    STW.WriteLine(statusResult);

                    if (dlgResult == DialogResult.Yes)
                        DrawCircleBasedOnPointAndRadius(false, point, (float)accuracy);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DIsPointOnPoly - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine("Failed");
                }

                //Draw source shape points if demanded by the user
                if (dlgResult == DialogResult.Yes)
                {
                    if (polyType == DNGEOMETRIC_SHAPE._EG_POLYLINE)
                        DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPoly, i, 0);
                    else
                        DrawFunction(true, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPoly, i, 0);

                    DNSMcVector3D[] pointVertice = new DNSMcVector3D[] { point };
                    DrawFunction(true, EDrawnItemType.Point, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, pointVertice, i, 0);
                }
            }


            CloseStreams();
            return true;
        }

        private void btnEGDistancePoint2Poly_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEGDistancePoint2Poly_Click(dlgResult))
                EndGeometricCalcAction("Distance Point 2 Poly");
        }

        private bool btnEGDistancePoint2Poly_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            List<string> lStatusResults = new List<string>();

            //Print result header line
            STW.WriteLine("Closest Point X, Closest Point Y, Closest Point Z, Segment, Distance");

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D point = new DNSMcVector3D();
                point.x = double.Parse(sourceLineValues[0]);
                point.y = double.Parse(sourceLineValues[1]);
                point.z = double.Parse(sourceLineValues[2]);

                uint unDimension = uint.Parse(sourceLineValues[3]);
                DNGEOMETRIC_SHAPE polyType = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[4]);

                int polyNumPt = (sourceLineValues.Length - 5) / 3;
                DNSMcVector3D[] stPoly = new DNSMcVector3D[polyNumPt];
                int idx = 0;

                for (int cell = 5; cell < sourceLineValues.Length; cell += 3)
                {
                    stPoly[idx].x = double.Parse(sourceLineValues[cell]);
                    stPoly[idx].y = double.Parse(sourceLineValues[cell + 1]);
                    stPoly[idx].z = double.Parse(sourceLineValues[cell + 2]);

                    idx++;
                }

                DNSMcVector3D pstClosest = new DNSMcVector3D();
                uint puSegment = 0;
                double pdDistance = 0;

                try
                {
                    IDNMcGeometricCalculations.EGDistancePoint2Poly(point,
                                                                        stPoly,
                                                                        polyType,
                                                                        ref pstClosest,
                                                                        ref puSegment,
                                                                        ref pdDistance,
                                                                        unDimension);


                    STW.WriteLine(pstClosest.x.ToString() + "," +
                                    pstClosest.y.ToString() + "," +
                                    pstClosest.z.ToString() + "," +
                                    puSegment.ToString() + "," +
                                    pdDistance.ToString());

                    if (dlgResult == DialogResult.Yes)
                    {
                        DNSMcVector3D[] closestPt = new DNSMcVector3D[] { pstClosest, point };
                        DrawFunction(false, EDrawnItemType.Connector, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, closestPt, i, pdDistance);
                        DrawFunction(false, EDrawnItemType.Point, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, closestPt, i, 0);
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EGDistancePoint2Poly - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine("Failed");
                }

                //Draw source line points if demanded by the user
                if (dlgResult == DialogResult.Yes)
                {
                    if (polyType == DNGEOMETRIC_SHAPE._EG_POLYLINE)
                        DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPoly, i, 0);
                    else
                        DrawFunction(true, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPoly, i, 0);
                }
            }


            CloseStreams();
            return true;
        }

        private void btnEGDistancePoly2Poly_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEGDistancePoly2Poly_Click(dlgResult))
                EndGeometricCalcAction("Distance Poly 2 Poly");
        }

        private bool btnEGDistancePoly2Poly_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            //Print result header line
            outputLine = "Closes Point On 1 (X), Closes Point On 1 (Y), Closes Point On 1 (Z), Closes Point On 2 (X), Closes Point On 2 (Y), Closes Point On 2 (Z), Distance";
            STW.WriteLine(outputLine);

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');
                uint unDimension = uint.Parse(sourceLineValues[0]);

                DNGEOMETRIC_SHAPE ePolyType1 = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[1]);
                uint uPointsNum = uint.Parse(sourceLineValues[2]);

                int idx = 0;
                DNSMcVector3D[] stPoly1 = new DNSMcVector3D[uPointsNum];
                for (int poly1Pt = 3; poly1Pt < 3 + uPointsNum * 3; poly1Pt += 3)
                {
                    stPoly1[idx].x = double.Parse(sourceLineValues[poly1Pt]);
                    stPoly1[idx].y = double.Parse(sourceLineValues[poly1Pt + 1]);
                    stPoly1[idx].z = double.Parse(sourceLineValues[poly1Pt + 2]);

                    idx++;
                }

                int nextCell = 3 + (int)uPointsNum * 3;
                DNGEOMETRIC_SHAPE ePolyType2 = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[nextCell++]);
                uint uPointsNum2 = uint.Parse(sourceLineValues[nextCell++]);

                idx = 0;
                DNSMcVector3D[] stPoly2 = new DNSMcVector3D[uPointsNum2];
                for (int poly2Pt = nextCell; poly2Pt < nextCell + uPointsNum2 * 3; poly2Pt += 3)
                {
                    stPoly2[idx].x = double.Parse(sourceLineValues[poly2Pt]);
                    stPoly2[idx].y = double.Parse(sourceLineValues[poly2Pt + 1]);
                    stPoly2[idx].z = double.Parse(sourceLineValues[poly2Pt + 2]);

                    idx++;
                }

                DNSMcVector3D pstClosest1 = new DNSMcVector3D();
                DNSMcVector3D pstClosest2 = new DNSMcVector3D();
                double pdDistance = 0;

                try
                {
                    IDNMcGeometricCalculations.EGDistancePoly2Poly(stPoly1,
                                                                       ePolyType1,
                                                                       stPoly2,
                                                                       ePolyType2,
                                                                       ref pstClosest1,
                                                                       ref pstClosest2,
                                                                       ref pdDistance,
                                                                       unDimension);


                    outputLine = pstClosest1.x.ToString() + "," +
                                    pstClosest1.y.ToString() + "," +
                                    pstClosest1.z.ToString() + "," +
                                    pstClosest2.x.ToString() + "," +
                                    pstClosest2.y.ToString() + "," +
                                    pstClosest2.z.ToString() + "," +
                                    pdDistance.ToString();

                    STW.WriteLine(outputLine);

                    //Draw result line points if function succeed and demanded by the user
                    if (dlgResult == DialogResult.Yes)
                    {
                        DNSMcVector3D[] connectorPts = new DNSMcVector3D[] { pstClosest1, pstClosest2 };
                        DrawFunction(false, EDrawnItemType.Connector, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, connectorPts, i, pdDistance);
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EGDistancePoly2Poly - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                //Draw source line points if demanded by the user
                if (dlgResult == DialogResult.Yes)
                {
                    if (ePolyType1 == DNGEOMETRIC_SHAPE._EG_POLYLINE)
                        DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPoly1, i, 0);
                    else
                        DrawFunction(true, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPoly1, i, 0);

                    if (ePolyType2 == DNGEOMETRIC_SHAPE._EG_POLYLINE)
                        DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPoly2, i, 0);
                    else
                        DrawFunction(true, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPoly2, i, 0);
                }
            }

            CloseStreams();
            return true;
        }

        private void btnEGPolyLength_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEGPolyLength_Click(dlgResult))
                EndGeometricCalcAction("Poly Length");
        }

        private bool btnEGPolyLength_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            //Print result header line
            STW.WriteLine("Length");

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');
                uint unDimension = uint.Parse(sourceLineValues[0]);

                DNGEOMETRIC_SHAPE ePolyType = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[1]);

                int idx = 0;
                int numPoints = (sourceLineValues.Length - 2) / 3;
                DNSMcVector3D[] stPoly = new DNSMcVector3D[numPoints];
                for (int polyPt = 2; polyPt < sourceLineValues.Length; polyPt += 3)
                {
                    stPoly[idx].x = double.Parse(sourceLineValues[polyPt]);
                    stPoly[idx].y = double.Parse(sourceLineValues[polyPt + 1]);
                    stPoly[idx].z = double.Parse(sourceLineValues[polyPt + 2]);

                    idx++;
                }

                double pdLength = 0;

                try
                {
                    pdLength = IDNMcGeometricCalculations.EGPolyLength(stPoly,
                                                                        ePolyType,
                                                                        unDimension);


                    STW.WriteLine(pdLength.ToString());
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EGPolyLength - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                //Draw source line points if demanded by the user
                if (dlgResult == DialogResult.Yes)
                {
                    if (ePolyType == DNGEOMETRIC_SHAPE._EG_POLYLINE)
                        DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPoly, i, 0);
                    else
                        DrawFunction(true, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPoly, i, 0);
                }
            }

            CloseStreams();
            return true;
        }

        private void btnEG2DPolySelfIntersection_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DPolySelfIntersection_Click(dlgResult))
                EndGeometricCalcAction("2D Poly Self Intersection");
        }

        private bool btnEG2DPolySelfIntersection_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            //Print result header line
            STW.WriteLine("Is Self Intersect");

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');
                DNGEOMETRIC_SHAPE ePolyType = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[0]);

                int idx = 0;
                int numPoints = (sourceLineValues.Length - 1) / 3;
                DNSMcVector3D[] stPoly = new DNSMcVector3D[numPoints];
                for (int polyPt = 1; polyPt < sourceLineValues.Length; polyPt += 3)
                {
                    stPoly[idx].x = double.Parse(sourceLineValues[polyPt]);
                    stPoly[idx].y = double.Parse(sourceLineValues[polyPt + 1]);
                    stPoly[idx].z = double.Parse(sourceLineValues[polyPt + 2]);

                    idx++;
                }

                bool pbIsSelfInterect;

                try
                {
                    pbIsSelfInterect = IDNMcGeometricCalculations.EG2DPolySelfIntersection(stPoly,
                                                                                            ePolyType);


                    STW.WriteLine(pbIsSelfInterect.ToString());
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DPolySelfIntersection - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                //Draw source line points if demanded by the user
                if (dlgResult == DialogResult.Yes)
                {
                    if (ePolyType == DNGEOMETRIC_SHAPE._EG_POLYLINE)
                        DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPoly, i, 0);
                    else
                        DrawFunction(true, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPoly, i, 0);
                }
            }

            CloseStreams();
            return true;
        }

        private void btnEG2DLinePolyIntersection_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DLinePolyIntersection_Click(dlgResult))
                EndGeometricCalcAction("2D Line Poly Intersection");
        }

        private bool btnEG2DLinePolyIntersection_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return true;

            int maxNumOfVertices = 0;
            List<DNSMcVector3D[]> lIntersectionPoints = new List<DNSMcVector3D[]>();

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');
                DNGEOMETRIC_SHAPE eLineType = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[0]);
                uint uPointsNum = uint.Parse(sourceLineValues[1]);

                int idx = 0;
                DNSMcVector3D[] stLine = new DNSMcVector3D[uPointsNum];
                for (int LinePt = 2; LinePt < 2 + uPointsNum * 3; LinePt += 3)
                {
                    stLine[idx].x = double.Parse(sourceLineValues[LinePt]);
                    stLine[idx].y = double.Parse(sourceLineValues[LinePt + 1]);
                    stLine[idx].z = double.Parse(sourceLineValues[LinePt + 2]);

                    idx++;
                }

                int nextCell = 2 + (int)uPointsNum * 3;
                DNGEOMETRIC_SHAPE ePolyType = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[nextCell++]);
                uint uPointsNum2 = uint.Parse(sourceLineValues[nextCell++]);

                idx = 0;
                DNSMcVector3D[] stPoly = new DNSMcVector3D[uPointsNum2];
                for (int polyPt = nextCell; polyPt < nextCell + uPointsNum2 * 3; polyPt += 3)
                {
                    stPoly[idx].x = double.Parse(sourceLineValues[polyPt]);
                    stPoly[idx].y = double.Parse(sourceLineValues[polyPt + 1]);
                    stPoly[idx].z = double.Parse(sourceLineValues[polyPt + 2]);

                    idx++;
                }

                DNSMcVector3D[] pstIntersectionPoints;

                try
                {
                    pstIntersectionPoints = IDNMcGeometricCalculations.EG2DLinePolyIntersection(stPoly,
                                                                                                    ePolyType,
                                                                                                    stLine,
                                                                                                    eLineType);


                    //Fined the longest result in order to match the header line
                    if (pstIntersectionPoints.Length > maxNumOfVertices)
                        maxNumOfVertices = pstIntersectionPoints.Length;

                    //Add the result to a result list that will be printed at the end of the process.
                    lIntersectionPoints.Add(pstIntersectionPoints);

                    if (dlgResult == DialogResult.Yes)
                        DrawFunction(false, EDrawnItemType.Point, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, pstIntersectionPoints, i, 0);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DLinePolyIntersection - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                //Draw source line points if demanded by the user
                if (dlgResult == DialogResult.Yes)
                {
                    if (ePolyType == DNGEOMETRIC_SHAPE._EG_POLYLINE)
                        DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPoly, i, 0);
                    else
                        DrawFunction(true, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPoly, i, 0);

                    DrawFunction(true, EDrawnItemType.Line, eLineType, stLine, i, 0);
                }
            }

            //Print result header line
            for (int i = 0; i < maxNumOfVertices; i++)
            {
                outputLine += "X" + i + "," +
                                "Y" + i + "," +
                                "Z" + i + ",";

            }

            STW.WriteLine(outputLine);
            outputLine = "";

            for (int lIdx = 0; lIdx < lIntersectionPoints.Count; lIdx++)
            {
                for (int ArrIdx = 0; ArrIdx < lIntersectionPoints[lIdx].Length; ArrIdx++)
                {
                    outputLine += lIntersectionPoints[lIdx][ArrIdx].x.ToString() + "," +
                                    lIntersectionPoints[lIdx][ArrIdx].y.ToString() + "," +
                                    lIntersectionPoints[lIdx][ArrIdx].z.ToString() + ",";
                }

                STW.WriteLine(outputLine);
                outputLine = "";
            }

            CloseStreams();
            return true;
        }

        private void btnEG2DLinePolyDistance_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DLinePolyDistance_Click(dlgResult))
                EndGeometricCalcAction("2D Line Poly Distance");
        }

        private bool btnEG2DLinePolyDistance_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            //Print result header line
            outputLine = "Closest On Poly(X), Closest On Poly(Y), Closest On Poly(Z), Closest On Line(X), Closest On Line(Y), Closest On Line(Z), Distance";
            STW.WriteLine(outputLine);

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');
                DNGEOMETRIC_SHAPE eLineType = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[0]);
                uint uPointsNum = uint.Parse(sourceLineValues[1]);

                int idx = 0;
                DNSMcVector3D[] stLine = new DNSMcVector3D[uPointsNum];
                for (int LinePt = 2; LinePt < 2 + uPointsNum * 3; LinePt += 3)
                {
                    stLine[idx].x = double.Parse(sourceLineValues[LinePt]);
                    stLine[idx].y = double.Parse(sourceLineValues[LinePt + 1]);
                    stLine[idx].z = double.Parse(sourceLineValues[LinePt + 2]);

                    idx++;
                }

                int nextCell = 2 + (int)uPointsNum * 3;
                DNGEOMETRIC_SHAPE ePolyType = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[nextCell++]);
                uint uPointsNum2 = uint.Parse(sourceLineValues[nextCell++]);

                idx = 0;
                DNSMcVector3D[] stPoly = new DNSMcVector3D[uPointsNum2];
                for (int polyPt = nextCell; polyPt < nextCell + uPointsNum2 * 3; polyPt += 3)
                {
                    stPoly[idx].x = double.Parse(sourceLineValues[polyPt]);
                    stPoly[idx].y = double.Parse(sourceLineValues[polyPt + 1]);
                    stPoly[idx].z = double.Parse(sourceLineValues[polyPt + 2]);

                    idx++;
                }

                DNSMcVector3D pstClosestOnPoly = new DNSMcVector3D();
                DNSMcVector3D pstClosestOnLine = new DNSMcVector3D();
                double pdDistance = 0;

                try
                {
                    IDNMcGeometricCalculations.EG2DLinePolyDistance(stPoly,
                                                                        ePolyType,
                                                                        stLine,
                                                                        eLineType,
                                                                        ref pstClosestOnPoly,
                                                                        ref pstClosestOnLine,
                                                                        ref pdDistance);


                    outputLine = pstClosestOnPoly.x.ToString() + "," +
                                    pstClosestOnPoly.y.ToString() + "," +
                                    pstClosestOnPoly.z.ToString() + "," +
                                    pstClosestOnLine.x.ToString() + "," +
                                    pstClosestOnLine.y.ToString() + "," +
                                    pstClosestOnLine.z.ToString() + "," +
                                    pdDistance.ToString();

                    STW.WriteLine(outputLine);

                    DNSMcVector3D[] connectorSegmentPt = new DNSMcVector3D[2];
                    connectorSegmentPt[0] = pstClosestOnPoly;
                    connectorSegmentPt[1] = pstClosestOnLine;

                    //Draw result line points if function succeed and demanded by the user
                    if (dlgResult == DialogResult.Yes)
                    {
                        DNSMcVector3D[] closestPt = new DNSMcVector3D[] { pstClosestOnPoly, pstClosestOnLine };
                        DrawFunction(false, EDrawnItemType.Point, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, closestPt, i, 0);
                        DrawFunction(false, EDrawnItemType.Connector, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, closestPt, i, pdDistance);
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DLinePolyDistance - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                //Draw source line points if demanded by the user
                if (dlgResult == DialogResult.Yes)
                {
                    if (ePolyType == DNGEOMETRIC_SHAPE._EG_POLYLINE)
                        DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPoly, i, 0);
                    else
                        DrawFunction(true, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPoly, i, 0);

                    DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stLine, i, 0);
                }
            }

            CloseStreams();
            return true;
        }

        private void btnEG2DPolyCircleIntersection_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DPolyCircleIntersection_Click(dlgResult))
                EndGeometricCalcAction("2D Poly Circle Intersection");
        }

        private bool btnEG2DPolyCircleIntersection_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            int maxNumOfVertices = 0;
            List<DNSMcVector3D[]> lIntesectionPoints = new List<DNSMcVector3D[]>();
            IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');
                DNGEOMETRIC_SHAPE eCircleType = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[0]);

                DNSMcVector3D stCircle1st = new DNSMcVector3D();
                stCircle1st.x = double.Parse(sourceLineValues[1]);
                stCircle1st.y = double.Parse(sourceLineValues[2]);
                stCircle1st.z = double.Parse(sourceLineValues[3]);

                DNSMcVector3D stCircle2nd = new DNSMcVector3D();
                stCircle2nd.x = double.Parse(sourceLineValues[4]);
                stCircle2nd.y = double.Parse(sourceLineValues[5]);
                stCircle2nd.z = double.Parse(sourceLineValues[6]);

                DNSMcVector3D stCircle3rd = new DNSMcVector3D();
                stCircle3rd.x = double.Parse(sourceLineValues[7]);
                stCircle3rd.y = double.Parse(sourceLineValues[8]);
                stCircle3rd.z = double.Parse(sourceLineValues[9]);

                DNGEOMETRIC_SHAPE ePolyType = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[10]);

                int idx = 0;
                DNSMcVector3D[] stPoly = new DNSMcVector3D[(sourceLineValues.Length - 10) / 3];
                for (int LinePt = 11; LinePt < sourceLineValues.Length; LinePt += 3)
                {
                    stPoly[idx].x = double.Parse(sourceLineValues[LinePt]);
                    stPoly[idx].y = double.Parse(sourceLineValues[LinePt + 1]);
                    stPoly[idx].z = double.Parse(sourceLineValues[LinePt + 2]);

                    idx++;
                }

                DNSMcVector3D[] pstIntersectionPoints;
                //DNSMcVector3D center1 = new DNSMcVector3D();

                try
                {
                    pstIntersectionPoints = IDNMcGeometricCalculations.EG2DPolyCircleIntersection(stPoly,
                                                                                                    ePolyType,
                                                                                                    stCircle1st,
                                                                                                    stCircle2nd,
                                                                                                    stCircle3rd,
                                                                                                    eCircleType);


                    //Add the result to a result list that will be printed at the end of the process.
                    lIntesectionPoints.Add(pstIntersectionPoints);

                    //Fined the longest result in order to match the header line
                    if (pstIntersectionPoints.Length > maxNumOfVertices)
                        maxNumOfVertices = pstIntersectionPoints.Length;

                    if (dlgResult == DialogResult.Yes)
                        DrawFunction(false, EDrawnItemType.Point, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, pstIntersectionPoints, i, 0);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DPolyCircleIntersection - Line: " + (i + 1).ToString(), McEx);
                    lIntesectionPoints.Add(new DNSMcVector3D[0]);
                }

                if (dlgResult == DialogResult.Yes)
                {
                    if (ePolyType == DNGEOMETRIC_SHAPE._EG_POLYLINE)
                        DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPoly, i, 0);
                    else
                        DrawFunction(true, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPoly, i, 0);

                    DNSMcVector3D[] circlePt = new DNSMcVector3D[] { stCircle1st, stCircle2nd, stCircle3rd };
                    DrawFunction(true, EDrawnItemType.Circle, eCircleType, circlePt, i, 0);
                }
            }

            //Print result header line
            for (int i = 0; i < maxNumOfVertices; i++)
            {
                outputLine += "X" + i + "," +
                                "Y" + i + "," +
                                "Z" + i + ",";

            }

            STW.WriteLine(outputLine);
            outputLine = "";

            for (int lIdx = 0; lIdx < lIntesectionPoints.Count; lIdx++)
            {
                for (int ArrIdx = 0; ArrIdx < lIntesectionPoints[lIdx].Length; ArrIdx++)
                {
                    outputLine += lIntesectionPoints[lIdx][ArrIdx].x.ToString() + "," +
                                    lIntesectionPoints[lIdx][ArrIdx].y.ToString() + "," +
                                    lIntesectionPoints[lIdx][ArrIdx].z.ToString() + ",";
                }

                STW.WriteLine(outputLine);
                outputLine = "";
            }

            CloseStreams();
            return true;
        }

        private void btnEG2DPolyCircleDistance_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DPolyCircleDistance_Click(dlgResult))
                EndGeometricCalcAction("2D Poly Circle Distance");
        }

        private bool btnEG2DPolyCircleDistance_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            //Print result header line
            outputLine = "Closest On Poly(X), Closest On Poly(Y), Closest On Poly(Z), Closest On Circle(X), Closest On Circle(Y), Closest On Circle(Z), Distance";
            STW.WriteLine(outputLine);

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');
                DNGEOMETRIC_SHAPE eCircleType = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[0]);

                DNSMcVector3D stCircle1st = new DNSMcVector3D();
                stCircle1st.x = double.Parse(sourceLineValues[1]);
                stCircle1st.y = double.Parse(sourceLineValues[2]);
                stCircle1st.z = double.Parse(sourceLineValues[3]);

                DNSMcVector3D stCircle2nd = new DNSMcVector3D();
                stCircle2nd.x = double.Parse(sourceLineValues[4]);
                stCircle2nd.y = double.Parse(sourceLineValues[5]);
                stCircle2nd.z = double.Parse(sourceLineValues[6]);

                DNSMcVector3D stCircle3rd = new DNSMcVector3D();
                stCircle3rd.x = double.Parse(sourceLineValues[7]);
                stCircle3rd.y = double.Parse(sourceLineValues[8]);
                stCircle3rd.z = double.Parse(sourceLineValues[9]);

                DNGEOMETRIC_SHAPE ePolyType = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[10]);

                int idx = 0;
                DNSMcVector3D[] stPoly = new DNSMcVector3D[(sourceLineValues.Length - 10) / 3];
                for (int LinePt = 11; LinePt < sourceLineValues.Length; LinePt += 3)
                {
                    stPoly[idx].x = double.Parse(sourceLineValues[LinePt]);
                    stPoly[idx].y = double.Parse(sourceLineValues[LinePt + 1]);
                    stPoly[idx].z = double.Parse(sourceLineValues[LinePt + 2]);

                    idx++;
                }

                DNSMcVector3D pstClosestOnPoly = new DNSMcVector3D();
                DNSMcVector3D pstClosestOnCircle = new DNSMcVector3D();
                double pdDistance = 0;

                try
                {
                    IDNMcGeometricCalculations.EG2DPolyCircleDistance(stPoly,
                                                                        ePolyType,
                                                                        stCircle1st,
                                                                        stCircle2nd,
                                                                        stCircle3rd,
                                                                        eCircleType,
                                                                        ref pstClosestOnPoly,
                                                                        ref pstClosestOnCircle,
                                                                        ref pdDistance);


                    outputLine = pstClosestOnPoly.x.ToString() + "," +
                                    pstClosestOnPoly.y.ToString() + "," +
                                    pstClosestOnPoly.z.ToString() + "," +
                                    pstClosestOnCircle.x.ToString() + "," +
                                    pstClosestOnCircle.y.ToString() + "," +
                                    pstClosestOnCircle.z.ToString() + "," +
                                    pdDistance.ToString();

                    STW.WriteLine(outputLine);

                    if (dlgResult == DialogResult.Yes)
                    {
                        DNSMcVector3D[] connectorPts = new DNSMcVector3D[] { pstClosestOnPoly, pstClosestOnCircle };
                        DrawFunction(false, EDrawnItemType.Point, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, connectorPts, i, pdDistance);
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DPolyCircleDistance - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                if (dlgResult == DialogResult.Yes)
                {
                    if (ePolyType == DNGEOMETRIC_SHAPE._EG_POLYLINE)
                        DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPoly, i, 0);
                    else
                        DrawFunction(true, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPoly, i, 0);

                    DNSMcVector3D[] circlePt = new DNSMcVector3D[] { stCircle1st, stCircle2nd, stCircle3rd };
                    DrawFunction(true, EDrawnItemType.Circle, eCircleType, circlePt, i, 0);
                }
            }

            CloseStreams();
            return true;
        }

        private void btnEG2DPolyBoundingRect_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DPolyBoundingRect_Click(dlgResult))
                EndGeometricCalcAction("2D Poly Bounding Rect");
        }

        private bool btnEG2DPolyBoundingRect_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            STW.WriteLine("Left, Right, Down, Up");

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNGEOMETRIC_SHAPE ePolyType = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[0]);

                int idx = 0;
                DNSMcVector3D[] stPoly = new DNSMcVector3D[(sourceLineValues.Length - 1) / 3];
                for (int LinePt = 1; LinePt < sourceLineValues.Length; LinePt += 3)
                {
                    stPoly[idx].x = double.Parse(sourceLineValues[LinePt]);
                    stPoly[idx].y = double.Parse(sourceLineValues[LinePt + 1]);
                    stPoly[idx].z = double.Parse(sourceLineValues[LinePt + 2]);

                    idx++;
                }

                //double radius = 0;
                double pdLeft = 0;
                double pdRight = 0;
                double pdDown = 0;
                double pdUp = 0;

                try
                {
                    IDNMcGeometricCalculations.EG2DPolyBoundingRect(stPoly,
                                                                        ePolyType,
                                                                        ref pdLeft,
                                                                        ref pdRight,
                                                                        ref pdDown,
                                                                        ref pdUp);


                    // print result to CSV file
                    STW.WriteLine(pdLeft.ToString() + "," +
                                    pdRight.ToString() + "," +
                                    pdDown.ToString() + "," +
                                    pdUp.ToString());

                    if (dlgResult == DialogResult.Yes)
                    {
                        DNSMcVector3D[] boundingRectPts = new DNSMcVector3D[4];
                        boundingRectPts[0].x = boundingRectPts[3].x = pdLeft;
                        boundingRectPts[1].x = boundingRectPts[2].x = pdRight;
                        boundingRectPts[0].y = boundingRectPts[1].y = pdUp;
                        boundingRectPts[2].y = boundingRectPts[3].y = pdDown;

                        DrawFunction(false, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, boundingRectPts, i, 0);
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DPolyBoundingRect - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                if (dlgResult == DialogResult.Yes)
                {
                    if (ePolyType == DNGEOMETRIC_SHAPE._EG_POLYLINE)
                        DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPoly, i, 0);
                    else
                        DrawFunction(true, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPoly, i, 0);
                }
            }

            CloseStreams();
            return true;
        }

        private void btnEG2DPolySmoothingSample_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DPolySmoothingSample_Click(dlgResult))
                EndGeometricCalcAction("Poly Smoothing Sample");
        }

        private bool btnEG2DPolySmoothingSample_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            int maxNumOfVertices = 0;
            int maxNumOfIndices = 0;

            List<DNSMcVector3D[]> lPolySmoothingVertices = new List<DNSMcVector3D[]>();
            List<uint[]> lIndices = new List<uint[]>();

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNGEOMETRIC_SHAPE polyType = (DNGEOMETRIC_SHAPE)Enum.Parse(typeof(DNGEOMETRIC_SHAPE), sourceLineValues[0]);
                uint numSmoothingLevels = uint.Parse(sourceLineValues[1]);

                List<DNSMcVector3D> lOriginPolyVertices = new List<DNSMcVector3D>();
                DNSMcVector3D vertice = new DNSMcVector3D();

                //Collect the poly vertices to a DNSMcVector3D list
                for (int idx = 2; idx < sourceLineValues.Length; idx += 3)
                {
                    vertice.x = double.Parse(sourceLineValues[idx]);
                    vertice.y = double.Parse(sourceLineValues[idx + 1]);
                    vertice.z = double.Parse(sourceLineValues[idx + 2]);

                    lOriginPolyVertices.Add(vertice);
                }

                DNSMcVector3D[] polyVertices = lOriginPolyVertices.ToArray();

                //Draw original poly if demanded by the user
                if (dlgResult == DialogResult.Yes)
                {
                    if (polyType == DNGEOMETRIC_SHAPE._EG_POLYGON)
                        DrawFunction(true, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, polyVertices, i, 0);
                    else
                        DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, polyVertices, i, 0);
                }

                DNSMcVector3D[] PolySmoothingVertices = null;
                uint[] indices = null;

                try
                {

                    PolySmoothingVertices = IDNMcGeometricCalculations.EG2DPolySmoothingSample(polyVertices,
                                                                                                    polyType,
                                                                                                    numSmoothingLevels,
                                                                                                    ref indices);

                    //Find the longest result in order to match the header line
                    if (PolySmoothingVertices.Length > maxNumOfVertices)
                        maxNumOfVertices = PolySmoothingVertices.Length;

                    //Find the longest result in order to match the header line
                    if (indices != null && indices.Length > maxNumOfIndices)
                        maxNumOfIndices = indices.Length;

                    if (polyType == DNGEOMETRIC_SHAPE._EG_POLYGON)
                        DrawFunction(false, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, PolySmoothingVertices, i, 0);
                    else
                        DrawFunction(false, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, PolySmoothingVertices, i, 0);

                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DPolySmoothingSample - Line: " + (i + 1).ToString(), McEx);
                }

                //Draw the Smoothing Poly if demanded by the user
                if (dlgResult == DialogResult.Yes)
                {
                    if (polyType == DNGEOMETRIC_SHAPE._EG_POLYGON)
                        DrawFunction(true, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, polyVertices, i, 0);
                    else
                        DrawFunction(true, EDrawnItemType.Line, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, polyVertices, i, 0);
                }

                //Add the result to a result list that will be printed at the end of the process.
                lPolySmoothingVertices.Add(PolySmoothingVertices);
                lIndices.Add(indices);
            }

            //Print result header line
            for (int i = 0; i < maxNumOfVertices; i++)
            {
                outputLine += "X" + i + "," +
                                "Y" + i + "," +
                                "Z" + i + ",";

            }

            for (int i = 0; i < maxNumOfIndices; i++)
            {
                outputLine += "Indices" + i + ",";
            }


            STW.WriteLine(outputLine);
            outputLine = "";

            for (int lIdx = 0; lIdx < lPolySmoothingVertices.Count; lIdx++)
            {
                for (int ArrIdx = 0; ArrIdx < lPolySmoothingVertices[lIdx].Length; ArrIdx++)
                {
                    outputLine += lPolySmoothingVertices[lIdx][ArrIdx].x.ToString() + "," +
                                    lPolySmoothingVertices[lIdx][ArrIdx].y.ToString() + "," +
                                    lPolySmoothingVertices[lIdx][ArrIdx].z.ToString() + ",";
                }
                if (lIndices[lIdx] != null)
                {
                    for (int ArrIdx = 0; ArrIdx < lIndices[lIdx].Length; ArrIdx++)
                    {
                        outputLine += lIndices[lIdx][ArrIdx].ToString() + ",";
                    }
                }

                STW.WriteLine(outputLine);
                outputLine = "";
            }

            CloseStreams();
            return true;
        }
        #endregion

        #region PolyGons Functions
        private void btnEG2DPolyGonMove_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DPolyGonMove_Click(dlgResult))
                EndGeometricCalcAction("2D PolyGon Move");
        }

        private bool btnEG2DPolyGonMove_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            int maxNumOfVertices = 0;
            List<DNSMcVector3D[]> lPolygonPoints = new List<DNSMcVector3D[]>();

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                double dX = double.Parse(sourceLineValues[0]);
                double dY = double.Parse(sourceLineValues[1]);

                int polygonNumPt = (sourceLineValues.Length - 2) / 3;
                DNSMcVector3D[] basePolygon = new DNSMcVector3D[polygonNumPt];
                int idx = 0;

                for (int cell = 2; cell < sourceLineValues.Length; cell += 3)
                {
                    basePolygon[idx].x = double.Parse(sourceLineValues[cell]);
                    basePolygon[idx].y = double.Parse(sourceLineValues[cell + 1]);
                    basePolygon[idx].z = double.Parse(sourceLineValues[cell + 2]);

                    idx++;
                }

                DNSMcVector3D[] returnPolygon = new DNSMcVector3D[basePolygon.Length];
                basePolygon.CopyTo(returnPolygon, 0);
                try
                {
                    IDNMcGeometricCalculations.EG2DPolyGonMove(ref returnPolygon,
                                                                dX,
                                                                dY);


                    //Fined the longest result in order to match the header line
                    if (returnPolygon.Length > maxNumOfVertices)
                        maxNumOfVertices = returnPolygon.Length;

                    //Add the result to a result list that will be printed at the end of the process.
                    lPolygonPoints.Add(returnPolygon);

                    //Draw result polygon points if function succeed and demanded by the user
                    if (dlgResult == DialogResult.Yes)
                        DrawFunction(false, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, returnPolygon, i, 0);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DPolyGonMove - Line: " + (i + 1).ToString(), McEx);
                    returnPolygon = new DNSMcVector3D[0];
                    lPolygonPoints.Add(returnPolygon);
                }

                //Draw source polygon points if demanded by the user
                if (dlgResult == DialogResult.Yes)
                    DrawFunction(true, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, basePolygon, i, 0);
            }

            //Print result header line
            for (int i = 0; i < maxNumOfVertices; i++)
            {
                outputLine += "X" + i + "," +
                                "Y" + i + "," +
                                "Z" + i + ",";

            }

            STW.WriteLine(outputLine);
            outputLine = "";

            for (int lIdx = 0; lIdx < lPolygonPoints.Count; lIdx++)
            {
                for (int ArrIdx = 0; ArrIdx < lPolygonPoints[lIdx].Length; ArrIdx++)
                {
                    outputLine += lPolygonPoints[lIdx][ArrIdx].x.ToString() + "," +
                                    lPolygonPoints[lIdx][ArrIdx].y.ToString() + "," +
                                    lPolygonPoints[lIdx][ArrIdx].z.ToString() + ",";
                }

                STW.WriteLine(outputLine);
                outputLine = "";
            }

            CloseStreams();
            return true;
        }

        private void btnEG2DPolyGonRotate_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DPolyGonRotate_Click(dlgResult))
                EndGeometricCalcAction("2D PolyGon Rotate");
        }

        private bool btnEG2DPolyGonRotate_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            int maxNumOfVertices = 0;
            List<DNSMcVector3D[]> lPolygonPoints = new List<DNSMcVector3D[]>();

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');
                DNSMcVector3D[] polygonPoints = new DNSMcVector3D[(uint)((sourceLineValues.Length - 4) / 3)];

                DNSMcVector3D basePoint = new DNSMcVector3D();
                basePoint.x = double.Parse(sourceLineValues[0]);
                basePoint.y = double.Parse(sourceLineValues[1]);
                basePoint.z = double.Parse(sourceLineValues[2]);

                double dAngle = double.Parse(sourceLineValues[3]);

                int idx = 0;
                for (int cell = 4; cell < sourceLineValues.Length; cell += 3)
                {
                    polygonPoints[idx].x = double.Parse(sourceLineValues[cell]);
                    polygonPoints[idx].y = double.Parse(sourceLineValues[cell + 1]);
                    polygonPoints[idx].z = double.Parse(sourceLineValues[cell + 2]);

                    idx++;
                }

                // draw source line
                if (dlgResult == DialogResult.Yes)
                {
                    DNSMcVector3D[] basePt = new DNSMcVector3D[] { basePoint };
                    DrawFunction(true, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, polygonPoints, i, 0);
                    DrawFunction(true, EDrawnItemType.Point, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, basePt, i, 0);
                }

                try
                {
                    IDNMcGeometricCalculations.EG2DPolyGonRotate(ref polygonPoints,
                                                                    dAngle,
                                                                    basePoint);

                    //Fined the longest result in order to match the header line
                    if (polygonPoints.Length > maxNumOfVertices)
                        maxNumOfVertices = polygonPoints.Length;

                    if (dlgResult == DialogResult.Yes)
                        DrawFunction(true, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, polygonPoints, i, 0);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DPolyGonRotate - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                //Add the result to a result list that will be printed at the end of the process.
                lPolygonPoints.Add(polygonPoints);
            }

            //Print result header line
            for (int i = 0; i < maxNumOfVertices; i++)
            {
                outputLine += "X" + i + "," +
                                "Y" + i + "," +
                                "Z" + i + ",";

            }

            STW.WriteLine(outputLine);
            outputLine = "";

            for (int lIdx = 0; lIdx < lPolygonPoints.Count; lIdx++)
            {
                for (int ArrIdx = 0; ArrIdx < lPolygonPoints[lIdx].Length; ArrIdx++)
                {
                    outputLine += lPolygonPoints[lIdx][ArrIdx].x.ToString() + "," +
                                    lPolygonPoints[lIdx][ArrIdx].y.ToString() + "," +
                                    lPolygonPoints[lIdx][ArrIdx].z.ToString() + ",";
                }

                STW.WriteLine(outputLine);
                outputLine = "";
            }

            CloseStreams();
            return true;
        }

        private void btnEG2DIsPointInPolyGon_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DIsPointInPolyGon_Click(dlgResult))
                EndGeometricCalcAction("2D Is Point In PolyGon");
        }

        private bool btnEG2DIsPointInPolyGon_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            List<string> lStatusResults = new List<string>();

            //Print result header line
            STW.WriteLine("Is In");

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D point = new DNSMcVector3D();
                point.x = double.Parse(sourceLineValues[0]);
                point.y = double.Parse(sourceLineValues[1]);
                point.z = double.Parse(sourceLineValues[2]);

                int shapeNumPt = (sourceLineValues.Length - 3) / 3;
                DNSMcVector3D[] stPoly = new DNSMcVector3D[shapeNumPt];
                int idx = 0;

                for (int cell = 3; cell < sourceLineValues.Length; cell += 3)
                {
                    stPoly[idx].x = double.Parse(sourceLineValues[cell]);
                    stPoly[idx].y = double.Parse(sourceLineValues[cell + 1]);
                    stPoly[idx].z = double.Parse(sourceLineValues[cell + 2]);

                    idx++;
                }

                try
                {
                    DNPOINT_PG_STATUS statusResult = IDNMcGeometricCalculations.EG2DIsPointInPolyGon(point,
                                                                                                        stPoly);


                    STW.WriteLine(statusResult);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DIsPointInPolyGon - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine("Failed");
                }

                //Draw source shape points if demanded by the user
                if (dlgResult == DialogResult.Yes)
                {
                    DrawFunction(true, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPoly, i, 0);

                    DNSMcVector3D[] pointVertice = new DNSMcVector3D[] { point };
                    DrawFunction(true, EDrawnItemType.Point, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, pointVertice, i, 0);
                }
            }

            CloseStreams();
            return true;
        }

        private void btnEG2DPolyGonsRelation_Click(object sender, EventArgs e)
        {

            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DPolyGonsRelation_Click(dlgResult))
                EndGeometricCalcAction("2D PolyGons Relation");
        }

        private bool btnEG2DPolyGonsRelation_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            int maxNumOfVertices = 0;
            List<DNSMcVector3D[]> lIntersectionPoints = new List<DNSMcVector3D[]>();
            List<DNPL_PL_STATUS> lCrossResult = new List<DNPL_PL_STATUS>();
            List<DNPG_PG_STATUS> lPolygonStatus = new List<DNPG_PG_STATUS>();

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');
                DNSMcVector3D[] pstIntersectionPoints = new DNSMcVector3D[0];
                DNPL_PL_STATUS peCrossResult = new DNPL_PL_STATUS();
                DNPG_PG_STATUS pePolygonStatus = new DNPG_PG_STATUS();

                uint pointsNumPolygon1 = uint.Parse(sourceLineValues[0]);

                DNSMcVector3D[] stPolygon1 = new DNSMcVector3D[pointsNumPolygon1];
                int idx = 0;

                for (int cell = 1; cell < pointsNumPolygon1 * 3; cell += 3)
                {
                    stPolygon1[idx].x = double.Parse(sourceLineValues[cell]);
                    stPolygon1[idx].y = double.Parse(sourceLineValues[cell + 1]);
                    stPolygon1[idx].z = double.Parse(sourceLineValues[cell + 2]);

                    idx++;
                }

                // read polygon 2 points
                int numPtCellPosition = (1 + (int)pointsNumPolygon1 * 3);
                List<DNSMcVector3D> points = new List<DNSMcVector3D>();
                idx = 0;
                for (int cell = (numPtCellPosition); cell < sourceLineValues.Length; cell += 3)
                {
                    DNSMcVector3D point = new DNSMcVector3D();
                    point.x = double.Parse(sourceLineValues[cell]);
                    point.y = double.Parse(sourceLineValues[cell + 1]);
                    point.z = double.Parse(sourceLineValues[cell + 2]);

                    points.Add(point);
                    idx++;
                }
                DNSMcVector3D[] stPolygon2 = points.ToArray();
                // draw source polygons
                if (dlgResult == DialogResult.Yes)
                {
                    DrawFunction(true, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPolygon1, i, 0);
                    DrawFunction(true, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPolygon2, i, 0);
                }

                try
                {
                    IDNMcGeometricCalculations.EG2DPolyGonsRelation(stPolygon1,
                                                                    stPolygon2,
                                                                    ref pstIntersectionPoints,
                                                                    ref peCrossResult,
                                                                    ref pePolygonStatus);


                    //Fined the longest result in order to match the header line
                    if (pstIntersectionPoints.Length > maxNumOfVertices)
                        maxNumOfVertices = pstIntersectionPoints.Length;

                    if (dlgResult == DialogResult.Yes)
                    {
                        DrawFunction(false, EDrawnItemType.Point, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, pstIntersectionPoints, i, 0);
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DPolyGonsRelation - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                //Add the result to a result list that will be printed at the end of the process.
                lIntersectionPoints.Add(pstIntersectionPoints);
                lCrossResult.Add(peCrossResult);
                lPolygonStatus.Add(pePolygonStatus);
            }

            //Print result header line
            outputLine += "Cross Result, PolygonStatus,";
            for (int i = 0; i < maxNumOfVertices; i++)
            {
                outputLine += "Intersection X" + i + "," +
                                "Intersection Y" + i + "," +
                                "Intersection Z" + i + ",";

            }

            STW.WriteLine(outputLine);
            outputLine = "";

            for (int lIdx = 0; lIdx < lIntersectionPoints.Count; lIdx++)
            {
                outputLine += lCrossResult[lIdx].ToString() + ",";
                outputLine += lPolygonStatus[lIdx].ToString() + ",";

                for (int ArrIdx = 0; ArrIdx < lIntersectionPoints[lIdx].Length; ArrIdx++)
                {
                    outputLine += lIntersectionPoints[lIdx][ArrIdx].x.ToString() + "," +
                                    lIntersectionPoints[lIdx][ArrIdx].y.ToString() + "," +
                                    lIntersectionPoints[lIdx][ArrIdx].z.ToString() + ",";
                }

                STW.WriteLine(outputLine);
                outputLine = "";
            }

            CloseStreams();
            return true;
        }

        private void btnEG2DPolyGonArea_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DPolyGonArea_Click(dlgResult))
                EndGeometricCalcAction("2D PolyGon Area");
        }

        private bool btnEG2DPolyGonArea_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            //Print result header line
            STW.WriteLine("Area");

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D[] stPolygon = new DNSMcVector3D[sourceLineValues.Length / 3];
                int idx = 0;

                for (int cell = 0; cell < sourceLineValues.Length; cell += 3)
                {
                    stPolygon[idx].x = double.Parse(sourceLineValues[cell]);
                    stPolygon[idx].y = double.Parse(sourceLineValues[cell + 1]);
                    stPolygon[idx].z = double.Parse(sourceLineValues[cell + 2]);

                    idx++;
                }

                try
                {
                    double polygonArea = 0;
                    if (!IDNMcGeometricCalculations.EG2DPolyGonArea(stPolygon, ref polygonArea))
                    {
                        // emulate function's previous behavior
                        throw new MapCoreException(DNEMcErrorCode.CROSSING_POLYGONS, "Polygons intersect");
                    }

                    STW.WriteLine(polygonArea.ToString());

                    //Draw result polygon points if function succeed and demanded by the user
                    if (dlgResult == DialogResult.Yes)
                    {
                        try
                        {
                            DNMcObject.Create(activeOverlay, GetResultTextScheme(polygonArea.ToString()), stPolygon);
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
                        }
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DPolyGonArea - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                //Draw source polygon points if demanded by the user
                if (dlgResult == DialogResult.Yes)
                    DrawFunction(true, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPolygon, i, 0);
            }

            CloseStreams();
            return true;
        }

        private void btnEG2DPolyGonInflate_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DPolyGonInflate_Click(dlgResult))
                EndGeometricCalcAction("2D PolyGon Inflate");
        }

        private bool btnEG2DPolyGonInflate_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            int maxNumOfVertices = 0;
            List<DNSMcVector3D[]> lPolygonPoints = new List<DNSMcVector3D[]>();

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D basePoint = new DNSMcVector3D();
                basePoint.x = double.Parse(sourceLineValues[0]);
                basePoint.y = double.Parse(sourceLineValues[1]);
                basePoint.z = double.Parse(sourceLineValues[2]);

                double dProportion = double.Parse(sourceLineValues[3]);

                DNSMcVector3D[] polygonPoints = new DNSMcVector3D[(uint)((sourceLineValues.Length - 4) / 3)];
                int idx = 0;

                for (int cell = 4; cell < sourceLineValues.Length; cell += 3)
                {
                    polygonPoints[idx].x = double.Parse(sourceLineValues[cell]);
                    polygonPoints[idx].y = double.Parse(sourceLineValues[cell + 1]);
                    polygonPoints[idx].z = double.Parse(sourceLineValues[cell + 2]);

                    idx++;
                }

                // draw source line
                if (dlgResult == DialogResult.Yes)
                {
                    DNSMcVector3D[] basePt = new DNSMcVector3D[] { basePoint };
                    DrawFunction(true, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, polygonPoints, i, 0);
                    DrawFunction(true, EDrawnItemType.Point, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, basePt, i, 0);
                }

                try
                {
                    IDNMcGeometricCalculations.EG2DPolyGonInflate(ref polygonPoints,
                                                                    dProportion,
                                                                    basePoint);

                    //Fined the longest result in order to match the header line
                    if (polygonPoints.Length > maxNumOfVertices)
                        maxNumOfVertices = polygonPoints.Length;

                    if (dlgResult == DialogResult.Yes)
                        DrawFunction(false, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, polygonPoints, i, 0);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DPolyGonInflate - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                //Add the result to a result list that will be printed at the end of the process.
                lPolygonPoints.Add(polygonPoints);
            }

            //Print result header line
            for (int i = 0; i < maxNumOfVertices; i++)
            {
                outputLine += "X" + i + "," +
                                "Y" + i + "," +
                                "Z" + i + ",";

            }

            STW.WriteLine(outputLine);
            outputLine = "";

            for (int lIdx = 0; lIdx < lPolygonPoints.Count; lIdx++)
            {
                for (int ArrIdx = 0; ArrIdx < lPolygonPoints[lIdx].Length; ArrIdx++)
                {
                    outputLine += lPolygonPoints[lIdx][ArrIdx].x.ToString() + "," +
                                    lPolygonPoints[lIdx][ArrIdx].y.ToString() + "," +
                                    lPolygonPoints[lIdx][ArrIdx].z.ToString() + ",";
                }

                STW.WriteLine(outputLine);
                outputLine = "";
            }

            CloseStreams();
            return true;
        }
        private void btnEG2DPolyGonCenterOfGravity_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DPolyGonCenterOfGravity_Click(dlgResult))
                EndGeometricCalcAction("2D PolyGon Center Of Gravity");
        }

        private bool btnEG2DPolyGonCenterOfGravity_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            //Print result header line
            STW.WriteLine("CoG X, CoG Y, CoG Z");

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D[] stPolygon = new DNSMcVector3D[sourceLineValues.Length / 3];
                int idx = 0;

                for (int cell = 0; cell < sourceLineValues.Length; cell += 3)
                {
                    stPolygon[idx].x = double.Parse(sourceLineValues[cell]);
                    stPolygon[idx].y = double.Parse(sourceLineValues[cell + 1]);
                    stPolygon[idx].z = double.Parse(sourceLineValues[cell + 2]);

                    idx++;
                }

                try
                {
                    DNSMcVector3D centerOfGravity = IDNMcGeometricCalculations.EG2DPolyGonCenterOfGravity(stPolygon);

                    outputLine = centerOfGravity.x.ToString() + "," +
                                    centerOfGravity.y.ToString() + "," +
                                    centerOfGravity.z.ToString();

                    STW.WriteLine(outputLine);

                    //Draw result point if function succeed and demanded by the user
                    if (dlgResult == DialogResult.Yes)
                    {
                        DNSMcVector3D[] pointArr = new DNSMcVector3D[] { centerOfGravity };
                        DrawFunction(false, EDrawnItemType.Point, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, pointArr, i, 0);
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DPolyGonArea - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                //Draw source polygon points if demanded by the user
                if (dlgResult == DialogResult.Yes)
                    DrawFunction(true, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPolygon, i, 0);
            }

            CloseStreams();
            return true;
        }
        private void btnEG2DPolyGonTriangulation_Click(object sender, EventArgs e)
        {

            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DPolyGonTriangulation_Click(dlgResult))
                EndGeometricCalcAction("2D PolyGon Triangulation");
        }

        private bool btnEG2DPolyGonTriangulation_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            int maxNumOfVertices = 0;
            List<DNSMcVector3D[]> lStripPoints = new List<DNSMcVector3D[]>();

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D[] stPolygon = new DNSMcVector3D[sourceLineValues.Length / 3];
                int idx = 0;

                for (int cell = 0; cell < sourceLineValues.Length; cell += 3)
                {
                    stPolygon[idx].x = double.Parse(sourceLineValues[cell]);
                    stPolygon[idx].y = double.Parse(sourceLineValues[cell + 1]);
                    stPolygon[idx].z = double.Parse(sourceLineValues[cell + 2]);

                    idx++;
                }

                try
                {
                    DNSMcVector3D[][] pstStrips = null;
                    IDNMcGeometricCalculations.EG2DPolyGonTriangulation(stPolygon,
                                                                        ref pstStrips);


                    //Fined the longest result in order to match the header line
                    if (pstStrips[0].Length > maxNumOfVertices)
                        maxNumOfVertices = pstStrips[0].Length;

                    //Draw result polygon points if function succeed and demanded by the user
                    if (dlgResult == DialogResult.Yes)
                    {
                        DNSMcVector3D[] trianglePoints = new DNSMcVector3D[3];
                        for (int stripNum = 0; stripNum < pstStrips.GetLength(0); stripNum++)
                        {
                            lStripPoints.Add(pstStrips[stripNum]);

                            for (int point = 0; point < pstStrips[stripNum].Length - 2; point++)
                            {
                                trianglePoints[0] = pstStrips[stripNum][point];
                                trianglePoints[1] = pstStrips[stripNum][point + 1];
                                trianglePoints[2] = pstStrips[stripNum][point + 2];

                                DrawFunction(false, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, trianglePoints, i, 0);
                            }
                        }

                        // add an empty line between different polygons
                        lStripPoints.Add(new DNSMcVector3D[0]);
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DPolyGonTriangulation - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine("Failed");
                }

                //Draw source polygon points if demanded by the user
                if (dlgResult == DialogResult.Yes)
                    DrawFunction(true, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPolygon, i, 0);
            }

            //Print result header line
            for (int i = 0; i < maxNumOfVertices; i++)
            {
                outputLine += "X" + i + "," +
                                "Y" + i + "," +
                                "Z" + i + ",";

            }

            STW.WriteLine(outputLine);
            outputLine = "";

            for (int lIdx = 0; lIdx < lStripPoints.Count; lIdx++)
            {
                for (int ArrIdx = 0; ArrIdx < lStripPoints[lIdx].Length; ArrIdx++)
                {
                    outputLine += lStripPoints[lIdx][ArrIdx].x.ToString() + "," +
                                    lStripPoints[lIdx][ArrIdx].y.ToString() + "," +
                                    lStripPoints[lIdx][ArrIdx].z.ToString() + ",";
                }

                STW.WriteLine(outputLine);
                outputLine = "";
            }

            CloseStreams();
            return true;
        }


        private void btnEG2DClipPolyGon_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DClipPolyGon_Click(dlgResult))
                EndGeometricCalcAction("2D Clip PolyGon");
        }

        private bool btnEG2DClipPolyGon_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            int maxNumOfVertices = 0;
            List<DNSMcVector3D[]> lPolygonsPoints = new List<DNSMcVector3D[]>();

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                uint pointsNumPolygon1 = uint.Parse(sourceLineValues[0]);

                DNSMcVector3D[] stPolygon1 = new DNSMcVector3D[pointsNumPolygon1];
                int idx = 0;
                for (int cell = 1; cell < pointsNumPolygon1 * 3; cell += 3)
                {
                    stPolygon1[idx].x = double.Parse(sourceLineValues[cell]);
                    stPolygon1[idx].y = double.Parse(sourceLineValues[cell + 1]);
                    stPolygon1[idx].z = double.Parse(sourceLineValues[cell + 2]);

                    idx++;
                }

                // read polygon 2 points
                int numPtCellPosition = (1 + (int)pointsNumPolygon1 * 3);
                uint pointsNumLine2 = uint.Parse(sourceLineValues[numPtCellPosition]);
                DNSMcVector3D[] stPolygon2 = new DNSMcVector3D[pointsNumLine2];
                idx = 0;
                for (int cell = (numPtCellPosition + 1); cell < sourceLineValues.Length; cell += 3)
                {
                    stPolygon2[idx].x = double.Parse(sourceLineValues[cell]);
                    stPolygon2[idx].y = double.Parse(sourceLineValues[cell + 1]);
                    stPolygon2[idx].z = double.Parse(sourceLineValues[cell + 2]);

                    idx++;
                }

                DNSMcVector3D[][] arrAminB = new DNSMcVector3D[0][];
                DNSMcVector3D[][] arrBminA = new DNSMcVector3D[0][];
                DNSMcVector3D[][] arrAandB = new DNSMcVector3D[0][];
                DNSMcVector3D[][] arrAorB = new DNSMcVector3D[0][];

                try
                {
                    IDNMcGeometricCalculations.EG2DClipPolyGon(stPolygon1,
                                                                stPolygon2,
                                                                ref arrAminB,
                                                                ref arrBminA,
                                                                ref arrAandB,
                                                                ref arrAorB);


                    //Fined the longest result in order to match the header line
                    if (arrAminB.Length > 0 && arrAminB[0].Length > maxNumOfVertices)
                        maxNumOfVertices = arrAminB[0].Length;

                    if (arrBminA.Length > 0 && arrBminA[0].Length > maxNumOfVertices)
                        maxNumOfVertices = arrBminA[0].Length;

                    if (arrAandB.Length > 0 && arrAandB[0].Length > maxNumOfVertices)
                        maxNumOfVertices = arrAandB[0].Length;

                    if (arrAorB.Length > 0 && arrAorB[0].Length > maxNumOfVertices)
                        maxNumOfVertices = arrAorB[0].Length;

                    //Draw result polygon points if function succeed and demanded by the user
                    if (dlgResult == DialogResult.Yes)
                    {
                        foreach (DNSMcVector3D[] AminB in arrAminB)
                        {
                            DrawFunction(false, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, AminB, i, 0);
                            try
                            {
                                DNMcObject.Create(activeOverlay, GetResultTextScheme("AminB"), AminB);
                            }
                            catch (MapCoreException McEx)
                            {
                                MapCore.Common.Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
                            }

                            lPolygonsPoints.Add(AminB);
                        }

                        lPolygonsPoints.Add(new DNSMcVector3D[0]);

                        foreach (DNSMcVector3D[] BminA in arrBminA)
                        {
                            DrawFunction(false, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, BminA, i, 0);
                            try
                            {
                                DNMcObject.Create(activeOverlay, GetResultTextScheme("BminA"), BminA);
                            }
                            catch (MapCoreException McEx)
                            {
                                MapCore.Common.Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
                            }

                            lPolygonsPoints.Add(BminA);
                        }


                        lPolygonsPoints.Add(new DNSMcVector3D[0]);

                        foreach (DNSMcVector3D[] AandB in arrAandB)
                        {
                            DrawFunction(false, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, AandB, i, 0);
                            try
                            {
                                DNMcObject.Create(activeOverlay, GetResultTextScheme("AandB"), AandB);
                            }
                            catch (MapCoreException McEx)
                            {
                                MapCore.Common.Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
                            }

                            lPolygonsPoints.Add(AandB);
                        }

                        lPolygonsPoints.Add(new DNSMcVector3D[0]);

                        foreach (DNSMcVector3D[] AorB in arrAorB)
                        {
                            DrawFunction(false, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, AorB, i, 0);
                            try
                            {
                                DNMcObject.Create(activeOverlay, GetResultTextScheme("AorB"), AorB);
                            }
                            catch (MapCoreException McEx)
                            {
                                MapCore.Common.Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
                            }

                            lPolygonsPoints.Add(AorB);
                        }

                        // add two empty line between different polygons
                        lPolygonsPoints.Add(new DNSMcVector3D[0]);
                        lPolygonsPoints.Add(new DNSMcVector3D[0]);
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DClipPolyGon - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine("Failed");
                }

                //Draw source polygons points if demanded by the user
                if (dlgResult == DialogResult.Yes)
                {
                    DrawFunction(true, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPolygon1, i, 0);
                    DrawFunction(true, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPolygon2, i, 0);
                }
            }

            //Print result header line
            for (int i = 0; i < maxNumOfVertices; i++)
            {
                outputLine += "X" + i + "," +
                                "Y" + i + "," +
                                "Z" + i + ",";

            }

            STW.WriteLine(outputLine);
            outputLine = "";

            for (int lIdx = 0; lIdx < lPolygonsPoints.Count; lIdx++)
            {
                for (int ArrIdx = 0; ArrIdx < lPolygonsPoints[lIdx].Length; ArrIdx++)
                {
                    outputLine += lPolygonsPoints[lIdx][ArrIdx].x.ToString() + "," +
                                    lPolygonsPoints[lIdx][ArrIdx].y.ToString() + "," +
                                    lPolygonsPoints[lIdx][ArrIdx].z.ToString() + ",";
                }

                STW.WriteLine(outputLine);
                outputLine = "";
            }

            CloseStreams();
            return true;
        }

        private void btnEG2DPolyGonDirection_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DPolyGonDirection_Click(dlgResult))
                EndGeometricCalcAction("2D PolyGon Direction");
        }

        private bool btnEG2DPolyGonDirection_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            List<DNSMcVector3D[]> lPolygonPoints = new List<DNSMcVector3D[]>();

            //Print result header line
            STW.WriteLine("Check For Self Intersection");

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                bool bCheckForSelfIntersection = bool.Parse(sourceLineValues[0]);

                int polygonNumPt = (sourceLineValues.Length - 1) / 3;
                DNSMcVector3D[] stPolygon = new DNSMcVector3D[polygonNumPt];
                int idx = 0;

                for (int cell = 1; cell < sourceLineValues.Length; cell += 3)
                {
                    stPolygon[idx].x = double.Parse(sourceLineValues[cell]);
                    stPolygon[idx].y = double.Parse(sourceLineValues[cell + 1]);
                    stPolygon[idx].z = double.Parse(sourceLineValues[cell + 2]);

                    idx++;
                }

                try
                {
                    DNPG_DIRECTION pPolyGonDir = IDNMcGeometricCalculations.EG2DPolyGonDirection(stPolygon,
                                                                                                    bCheckForSelfIntersection);

                    STW.WriteLine(pPolyGonDir.ToString());

                    //Draw result if function succeed and demanded by the user
                    if (dlgResult == DialogResult.Yes)
                        DNMcObject.Create(activeOverlay, GetResultTextScheme(pPolyGonDir.ToString()), stPolygon);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DPolyGonDirection - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine("Failed");
                }

                //Draw source polygon points if demanded by the user
                if (dlgResult == DialogResult.Yes)
                    DrawFunction(true, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPolygon, i, 0);
            }

            CloseStreams();
            return true;

        }

        private void btnEG2DPolyGonIsConvex_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DPolyGonIsConvex_Click(dlgResult))
                EndGeometricCalcAction("2D PolyGon Is Convex");
        }

        private bool btnEG2DPolyGonIsConvex_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            //Print result header line
            STW.WriteLine("Is Convex Polygon");

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D[] stPolygon = new DNSMcVector3D[sourceLineValues.Length / 3];
                int idx = 0;

                for (int cell = 0; cell < sourceLineValues.Length; cell += 3)
                {
                    stPolygon[idx].x = double.Parse(sourceLineValues[cell]);
                    stPolygon[idx].y = double.Parse(sourceLineValues[cell + 1]);
                    stPolygon[idx].z = double.Parse(sourceLineValues[cell + 2]);

                    idx++;
                }

                try
                {
                    bool pbConvexPoly = IDNMcGeometricCalculations.EG2DPolyGonIsConvex(stPolygon);

                    STW.WriteLine(pbConvexPoly.ToString());

                    //Draw result polygon points if function succeed and demanded by the user
                    if (dlgResult == DialogResult.Yes)
                    {
                        try
                        {
                            DNMcObject.Create(activeOverlay, GetResultTextScheme(pbConvexPoly.ToString()), stPolygon);
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
                        }
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DPolyGonIsConvex - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                //Draw source polygon points if demanded by the user
                if (dlgResult == DialogResult.Yes)
                    DrawFunction(true, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPolygon, i, 0);
            }

            CloseStreams();
            return true;
        }

        private void btnEG2DPolyGonConvexHull_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DPolyGonConvexHull_Click(dlgResult))
                EndGeometricCalcAction("2D PolyGon Convex Hull");
        }

        private bool btnEG2DPolyGonConvexHull_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            int maxNumOfVertices = 0;
            List<DNSMcVector3D[]> lPolygonPoints = new List<DNSMcVector3D[]>();

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D[] stPolyGon = new DNSMcVector3D[sourceLineValues.Length / 3];
                int idx = 0;

                for (int cell = 0; cell < sourceLineValues.Length; cell += 3)
                {
                    stPolyGon[idx].x = double.Parse(sourceLineValues[cell]);
                    stPolyGon[idx].y = double.Parse(sourceLineValues[cell + 1]);
                    stPolyGon[idx].z = double.Parse(sourceLineValues[cell + 2]);

                    idx++;
                }

                try
                {
                    DNSMcVector3D[] pbConvexPoly = IDNMcGeometricCalculations.EG2DPolyGonConvexHull(stPolyGon);

                    //Fined the longest result in order to match the header line
                    if (pbConvexPoly.Length > maxNumOfVertices)
                        maxNumOfVertices = pbConvexPoly.Length;

                    //Add the result to a result list that will be printed at the end of the process.
                    lPolygonPoints.Add(pbConvexPoly);

                    //Draw result polygon points if function succeed and demanded by the user
                    if (dlgResult == DialogResult.Yes)
                        DrawFunction(false, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, pbConvexPoly, i, 0);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DPolyGonConvexHull - Line: " + (i + 1).ToString(), McEx);
                    lPolygonPoints.Add(new DNSMcVector3D[0]);
                }

                //Draw source polygon points if demanded by the user
                if (dlgResult == DialogResult.Yes)
                    DrawFunction(true, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPolyGon, i, 0);
            }

            //Print result header line
            for (int i = 0; i < maxNumOfVertices; i++)
            {
                outputLine += "X" + i + "," +
                                "Y" + i + "," +
                                "Z" + i + ",";

            }

            STW.WriteLine(outputLine);
            outputLine = "";

            for (int lIdx = 0; lIdx < lPolygonPoints.Count; lIdx++)
            {
                for (int ArrIdx = 0; ArrIdx < lPolygonPoints[lIdx].Length; ArrIdx++)
                {
                    outputLine += lPolygonPoints[lIdx][ArrIdx].x.ToString() + "," +
                                    lPolygonPoints[lIdx][ArrIdx].y.ToString() + "," +
                                    lPolygonPoints[lIdx][ArrIdx].z.ToString() + ",";
                }

                STW.WriteLine(outputLine);
                outputLine = "";
            }

            CloseStreams();
            return true;
        }

        private void btnEG2DPolygonExpandWithCurves_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DPolygonExpandWithCurves_Click(dlgResult))
                EndGeometricCalcAction("2D Polygon Expand With Curves");
        }

        private bool btnEG2DPolygonExpandWithCurves_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            int maxNumOfVertices = 0;
            List<DNSTGeneralShapePoint[]> lGeneralShapePt = new List<DNSTGeneralShapePoint[]>();

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                double dExpansionDistance = double.Parse(sourceLineValues[0]);

                int polygonNumPt = (sourceLineValues.Length - 1) / 3;
                DNSMcVector3D[] stPolyGon = new DNSMcVector3D[polygonNumPt];
                int idx = 0;

                for (int cell = 1; cell < sourceLineValues.Length; cell += 3)
                {
                    stPolyGon[idx].x = double.Parse(sourceLineValues[cell]);
                    stPolyGon[idx].y = double.Parse(sourceLineValues[cell + 1]);
                    stPolyGon[idx].z = double.Parse(sourceLineValues[cell + 2]);

                    idx++;
                }

                try
                {
                    DNSTGeneralShapePoint[] generalShapePointArr = IDNMcGeometricCalculations.EG2DPolygonExpandWithCurves(stPolyGon,
                                                                                                                            dExpansionDistance);

                    //Fined the longest result in order to match the header line
                    if (generalShapePointArr.Length > maxNumOfVertices)
                        maxNumOfVertices = generalShapePointArr.Length;

                    //Add the result to a result list that will be printed at the end of the process.
                    lGeneralShapePt.Add(generalShapePointArr);

                    //Draw result polygon points if function succeed and demanded by the user
                    if (dlgResult == DialogResult.Yes)
                    {
                        DNSMcVector3D[] polygonPoints = new DNSMcVector3D[generalShapePointArr.Length];
                        DNSMcVector3D[] locationPoint = new DNSMcVector3D[1];
                        for (int pt = 0; pt < generalShapePointArr.Length; pt++)
                        {
                            polygonPoints[pt] = locationPoint[0] = generalShapePointArr[i].stPoint;

                            try
                            {
                                DNMcObject.Create(activeOverlay, GetResultTextScheme(generalShapePointArr[i].ePointType.ToString()), locationPoint);
                            }
                            catch (MapCoreException McEx)
                            {
                                MapCore.Common.Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
                            }

                        }

                        DrawFunction(false, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, polygonPoints, i, 0);
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DPolygonExpandWithCurves - Line: " + (i + 1).ToString(), McEx);
                    lGeneralShapePt.Add(new DNSTGeneralShapePoint[0]);
                }

                //Draw source polygon points if demanded by the user
                if (dlgResult == DialogResult.Yes)
                    DrawFunction(true, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPolyGon, i, 0);
            }

            //Print result header line
            for (int i = 0; i < maxNumOfVertices; i++)
            {
                outputLine += "Point Type " + i + "," +
                                "X" + i + "," +
                                "Y" + i + "," +
                                "Z" + i + ",";

            }

            STW.WriteLine(outputLine);
            outputLine = "";

            for (int lIdx = 0; lIdx < lGeneralShapePt.Count; lIdx++)
            {
                for (int ArrIdx = 0; ArrIdx < lGeneralShapePt[lIdx].Length; ArrIdx++)
                {
                    outputLine += lGeneralShapePt[lIdx][ArrIdx].ePointType.ToString() + "," +
                                    lGeneralShapePt[lIdx][ArrIdx].stPoint.x.ToString() + "," +
                                    lGeneralShapePt[lIdx][ArrIdx].stPoint.y.ToString() + "," +
                                    lGeneralShapePt[lIdx][ArrIdx].stPoint.z.ToString() + ",";
                }

                STW.WriteLine(outputLine);
                outputLine = "";
            }

            CloseStreams();
            return true;
        }

        private void btnEG2DPolygonExpandWithCorners_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DPolygonExpandWithCorners_Click(dlgResult))
                EndGeometricCalcAction("2D Polygon Expand With Corners");
        }

        private bool btnEG2DPolygonExpandWithCorners_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            int maxNumOfVertices = 0;
            List<DNSMcVector3D[]> lPolygonPoints = new List<DNSMcVector3D[]>();

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                double dExpansionDistance = double.Parse(sourceLineValues[0]);

                int polygonNumPt = (sourceLineValues.Length - 1) / 3;
                DNSMcVector3D[] stPolyGon = new DNSMcVector3D[polygonNumPt];
                int idx = 0;

                for (int cell = 1; cell < sourceLineValues.Length; cell += 3)
                {
                    stPolyGon[idx].x = double.Parse(sourceLineValues[cell]);
                    stPolyGon[idx].y = double.Parse(sourceLineValues[cell + 1]);
                    stPolyGon[idx].z = double.Parse(sourceLineValues[cell + 2]);

                    idx++;
                }

                try
                {
                    DNSMcVector3D[] returnPolygon = IDNMcGeometricCalculations.EG2DPolygonExpandWithCorners(stPolyGon,
                                                                                                                dExpansionDistance);

                    //Fined the longest result in order to match the header line
                    if (returnPolygon.Length > maxNumOfVertices)
                        maxNumOfVertices = returnPolygon.Length;

                    //Add the result to a result list that will be printed at the end of the process.
                    lPolygonPoints.Add(returnPolygon);

                    //Draw result polygon points if function succeed and demanded by the user
                    if (dlgResult == DialogResult.Yes)
                    {
                        DrawFunction(false, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, returnPolygon, i, 0);
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DPolygonExpandWithCorners - Line: " + (i + 1).ToString(), McEx);
                    lPolygonPoints.Add(new DNSMcVector3D[0]);
                }

                //Draw source polygon points if demanded by the user
                if (dlgResult == DialogResult.Yes)
                    DrawFunction(true, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPolyGon, i, 0);
            }

            //Print result header line
            for (int i = 0; i < maxNumOfVertices; i++)
            {
                outputLine += "Point Type " + i + "," +
                                "X" + i + "," +
                                "Y" + i + "," +
                                "Z" + i + ",";

            }

            STW.WriteLine(outputLine);
            outputLine = "";

            for (int lIdx = 0; lIdx < lPolygonPoints.Count; lIdx++)
            {
                for (int ArrIdx = 0; ArrIdx < lPolygonPoints[lIdx].Length; ArrIdx++)
                {
                    outputLine += lPolygonPoints[lIdx][ArrIdx].ToString() + "," +
                                    lPolygonPoints[lIdx][ArrIdx].x.ToString() + "," +
                                    lPolygonPoints[lIdx][ArrIdx].y.ToString() + "," +
                                    lPolygonPoints[lIdx][ArrIdx].z.ToString() + ",";
                }

                STW.WriteLine(outputLine);
                outputLine = "";
            }

            CloseStreams();
            return true;
        }
        #endregion

        #region Circles Union
        private void btnCirclesUnion_Click(object sender, EventArgs e)
        {
            IDNMcEllipseItem[] m_SrcEllipses;
            DNSMcVector3D[] m_SrcEllpseLocationPoints;
            DNSTUnionArc[] m_UnionArcs = null;
            DNSTUnionShape[] m_UnionShapes = null;
            DNSTCircle[] m_InputCircles;

            frmGeometricCalcCirclesList GeometricCalcCirclesListForm = new frmGeometricCalcCirclesList();
            if (GeometricCalcCirclesListForm.ShowDialog() == DialogResult.OK)
            {
                m_SrcEllipses = GeometricCalcCirclesListForm.InputCircles;
                m_SrcEllpseLocationPoints = GeometricCalcCirclesListForm.CircleLocationPoint;
                m_InputCircles = new DNSTCircle[m_SrcEllipses.Length];
                float radiusX;
                uint propertyID;

                for (int i = 0; i < m_SrcEllipses.Length; i++)
                {
                    m_SrcEllipses[i].GetRadiusX(out radiusX, out propertyID, false);
                    m_InputCircles[i] = new DNSTCircle();
                    m_InputCircles[i].dRadius = (double)radiusX;

                    m_InputCircles[i].stCenter = m_SrcEllpseLocationPoints[i];
                }

                try
                {
                    IDNMcGeometricCalculations.EG2DCirclesUnion(m_InputCircles, true, ref m_UnionArcs, ref m_UnionShapes);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DCirclesUnion", McEx);
                }
                if (Manager_MCOverlayManager.ActiveOverlayManager != null)
                {
                    IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;

                    if (activeOverlay != null)
                    {
                        for (int i = 0; i < m_UnionArcs.Length; i++)
                        {

                            IDNMcObjectSchemeItem ObjSchemeItem = DNMcArcItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                        DNEMcPointCoordSystem._EPCS_WORLD,
                                                                                        DNEItemGeometryType._EGT_GEOMETRIC_IN_VIEWPORT,
                                                                                        (float)m_InputCircles[(int)m_UnionArcs[i].unCircleID].dRadius,
                                                                                        (float)m_InputCircles[(int)m_UnionArcs[i].unCircleID].dRadius,
                                                                                        90 - (float)m_UnionArcs[i].dEndAngle,
                                                                                        90 - (float)m_UnionArcs[i].dStartAngle,
                                                                                        DNELineStyle._ELS_SOLID,
                                                                                        new DNSMcBColor(255, 0, 0, 255),
                                                                                        2);



                            ((IDNMcArcItem)ObjSchemeItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                            DNSMcVector3D[] locationPoint = new DNSMcVector3D[1];
                            locationPoint[0] = m_InputCircles[(int)m_UnionArcs[i].unCircleID].stCenter;

                            IDNMcObject obj = DNMcObject.Create(activeOverlay,
                                                                ObjSchemeItem,
                                                                DNEMcPointCoordSystem._EPCS_WORLD,
                                                                locationPoint,
                                                                false);


                        }
                    }
                    else
                        MessageBox.Show("There is no active overlay");
                }
                else
                    MessageBox.Show("There is no active overlay manager");

                for (int i = 0; i < m_UnionShapes.Length; i++)
                {
                    uint[] participatingCircle = m_UnionShapes[i].aunParticipatingCirclesIDs;
                    for (int numCircle = 0; numCircle < participatingCircle.Length; numCircle++)
                    {
                        m_SrcEllipses[participatingCircle[numCircle]].SetFillColor(new DNSMcBColor(0, 0, 0, 125), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        m_SrcEllipses[participatingCircle[numCircle]].SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_REGULAR, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    }
                }
            }
        }

        #endregion

        #region Rectangle Functions
        private void btnGetRectanglePoints_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnGetRectanglePoints_Click(dlgResult))
                EndGeometricCalcAction("Get Rectangle Points");
        }

        private bool btnGetRectanglePoints_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            List<DNSMcVector3D[]> lRectanglePoints = new List<DNSMcVector3D[]>();

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D firstCornerInDiagonal = new DNSMcVector3D();
                firstCornerInDiagonal.x = double.Parse(sourceLineValues[0]);
                firstCornerInDiagonal.y = double.Parse(sourceLineValues[1]);
                firstCornerInDiagonal.z = double.Parse(sourceLineValues[2]);

                DNSMcVector3D secondtCornerInDiagonal = new DNSMcVector3D();
                secondtCornerInDiagonal.x = double.Parse(sourceLineValues[3]);
                secondtCornerInDiagonal.y = double.Parse(sourceLineValues[4]);
                secondtCornerInDiagonal.z = double.Parse(sourceLineValues[5]);

                double rotationAzimuthDeg = double.Parse(sourceLineValues[6]);

                DNSMcVector3D rotatedUpperLeft = new DNSMcVector3D();
                DNSMcVector3D rotatedUpperRight = new DNSMcVector3D();
                DNSMcVector3D rotatedLowerRight = new DNSMcVector3D();
                DNSMcVector3D rotatedLowerLeft = new DNSMcVector3D();

                try
                {
                    IDNMcGeometricCalculations.EGGetRectanglePoints(firstCornerInDiagonal,
                                                                        secondtCornerInDiagonal,
                                                                        rotationAzimuthDeg,
                                                                        ref rotatedUpperLeft,
                                                                        ref rotatedUpperRight,
                                                                        ref rotatedLowerRight,
                                                                        ref rotatedLowerLeft);

                    //Add the result to a result list that will be printed at the end of the process.
                    DNSMcVector3D[] rectPt = new DNSMcVector3D[] { rotatedUpperLeft, rotatedUpperRight, rotatedLowerRight, rotatedLowerLeft };
                    lRectanglePoints.Add(rectPt);

                    //Draw result line points if function succeed and demanded by the user
                    if (dlgResult == DialogResult.Yes)
                    {

                        DrawFunction(false, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, rectPt, i, 0);
                    }

                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EGGetRectanglePoints - Line: " + (i + 1).ToString(), McEx);
                }

                //Draw source line points if demanded by the user
                if (dlgResult == DialogResult.Yes)
                {
                    DNSMcVector3D[] srcRectPt = new DNSMcVector3D[] { firstCornerInDiagonal, secondtCornerInDiagonal };
                    DrawFunction(true, EDrawnItemType.Rectangle, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, srcRectPt, i, 0);
                }
            }

            //Print result header line
            outputLine += "Rotated Upper Left" + "," + "Rotated Upper Right" + "," + "Rotated Lower Right" + "," + "Rotated Lower Left";

            STW.WriteLine(outputLine);
            outputLine = "";

            for (int lIdx = 0; lIdx < lRectanglePoints.Count; lIdx++)
            {
                for (int ArrIdx = 0; ArrIdx < lRectanglePoints[lIdx].Length; ArrIdx++)
                {
                    outputLine += lRectanglePoints[lIdx][ArrIdx].x.ToString() + "," +
                                    lRectanglePoints[lIdx][ArrIdx].y.ToString() + "," +
                                    lRectanglePoints[lIdx][ArrIdx].z.ToString() + ",";
                }

                STW.WriteLine(outputLine);
                outputLine = "";
            }

            CloseStreams();
            return true;
        }
        #endregion

        private void btnAutomaticCalculations_Click(object sender, EventArgs e)
        {
            FolderSelectDialog folderSelectDialog = new FolderSelectDialog();
            folderSelectDialog.Title = "Input folder to select";
            folderSelectDialog.InitialDirectory = @"c:\";

            if (folderSelectDialog.ShowDialog(IntPtr.Zero))
            {
                string inputFolderPath = folderSelectDialog.FileName;
                string[] filesPath = Directory.GetFiles(inputFolderPath, "*.csv", SearchOption.AllDirectories);

                mMCTime.Reset();
                mMCTime.Start();

                foreach (string filePath in filesPath)
                {
                    string filePathWithoutExt = Path.GetFileNameWithoutExtension(filePath);

                    switch (filePathWithoutExt)
                    {
                        case "EG2DAngleFromX":
                            btnEG2DAngleFromX_Click(DialogResult.No, filePath);
                            break;
                        case "EG3DAngleFromXY":
                            btnEG3DAngleFromXY_Click(DialogResult.No, filePath);
                            break;
                        case "EGAngleBetween3Points":
                            btnEGAngleBetween3Points_Click(DialogResult.No, filePath);
                            break;
                        case "2DCircleBoundingRect":
                            btnEG2DCircleBoundingRect_Click(DialogResult.No, filePath);
                            break;
                        case "2DCircleCircleIntersection":
                            btnEG2DCircleCircleIntersection_Click(DialogResult.No, filePath);
                            break;
                        case "2DCircleFrom3Points":
                            btnEG2DCircleFrom3Points_Click(DialogResult.No, filePath);
                            break;
                        case "2DCircleLineDistance":
                            btnEG2DCircleLineDistance_Click(DialogResult.No, filePath);
                            break;
                        case "2DCircleLineIntersection":
                            btnEG2DCircleLineIntersection_Click(DialogResult.No, filePath);
                            break;
                        case "2DCirclePointDistance":
                            btnEG2DCirclePointDistance_Click(DialogResult.No, filePath);
                            break;
                        case "2DCircleSample":
                            btnEG2DCircleSample_Click(DialogResult.No, filePath);
                            break;
                        case "2DIsPointInCircle":
                            btnEG2DIsPointInCircle_Click(DialogResult.No, filePath);
                            break;
                        case "2DIsPointOnCircle":
                            btnEG2DIsPointOnCircle_Click(DialogResult.No, filePath);
                            break;
                        case "2DTangents2Circles":
                            btnEG2DTangents2Circles_Click(DialogResult.No, filePath);
                            break;
                        case "2DTangentsThroughPoint":
                            btnEG2DTangentsThroughPoint_Click(DialogResult.No, filePath);
                            break;
                        case "ArcAngleFromLength":
                            btnEGArcAngleFromLength_Click(DialogResult.No, filePath);
                            break;
                        case "ArcLengthFromAngle":
                            btnEGArcLengthFromAngle_Click(DialogResult.No, filePath);
                            break;
                        case "EG2D3PointsFromCircle":
                            btnEG2D3PointsFromCircle_Click(DialogResult.No, filePath);
                            break;
                        case "EG2DCircleCircleDistance":
                            btnEG2DCircleCircleDistance_Click(DialogResult.No, filePath);
                            break;
                        case "2DIsPointOnLine":
                            btnEG2DIsPointOnLine_Click(DialogResult.No, filePath);
                            break;
                        case "2DLineMove":
                            btnEG2DLineMove_Click(DialogResult.No, filePath);
                            break;
                        case "2DLineRotate":
                            btnEG2DLineRotate_Click(DialogResult.No, filePath);
                            break;
                        case "2DSegmentsRelation":
                            btnEG2DSegmentsRelation_Click(DialogResult.No, filePath);
                            break;
                        case "DistancePointLine":
                            btnEGDistancePointLine_Click(DialogResult.No, filePath);
                            break;
                        case "LineDistance":
                            btnEGLineDistance_Click(DialogResult.No, filePath);
                            break;
                        case "ParallelLine":
                            btnEGParallelLine_Click(DialogResult.No, filePath);
                            break;
                        case "PerpendicularLine":
                            btnEGPerpendicularLine_Click(DialogResult.No, filePath);
                            break;
                        case "SegmentsDistance":
                            btnEGSegmentsDistance_Click(DialogResult.No, filePath);
                            break;
                        case "2DPolygonArea":
                            btnEG2DPolyGonArea_Click(DialogResult.No, filePath);
                            break;
                        case "2DPolygonCenterOfGravity":
                            btnEG2DPolyGonCenterOfGravity_Click(DialogResult.No, filePath);
                            break;
                        case "2DPolyGonConvexHull":
                            btnEG2DPolyGonConvexHull_Click(DialogResult.No, filePath);
                            break;
                        case "2DPolyGonDirection":
                            btnEG2DPolyGonDirection_Click(DialogResult.No, filePath);
                            break;
                        case "2DPolyGonInflate":
                            btnEG2DPolyGonInflate_Click(DialogResult.No, filePath);
                            break;
                        case "2DPolyGonIsConvex":
                            btnEG2DPolyGonIsConvex_Click(DialogResult.No, filePath);
                            break;
                        case "2DPolygonMove":
                            btnEG2DPolyGonMove_Click(DialogResult.No, filePath);
                            break;
                        //case "2DPolygonRelations":
                        //    btnEG2DPolygonRelations(DialogResult.No, filePath);
                        //     break;
                        case "2DPolygonRotate":
                            btnEG2DPolyGonRotate_Click(DialogResult.No, filePath);
                            break;
                        case "EG2DClipPolyGon":
                            btnEG2DClipPolyGon_Click(DialogResult.No, filePath);
                            break;
                        case "EG2DPolygonExpandWithCorners":
                            btnEG2DPolygonExpandWithCorners_Click(DialogResult.No, filePath);
                            break;
                        case "EG2DPolygonExpandWithCurves":
                            btnEG2DPolygonExpandWithCurves_Click(DialogResult.No, filePath);
                            break;
                        case "EG2DPolyGonTriangulation":
                            btnEG2DPolyGonTriangulation_Click(DialogResult.No, filePath);
                            break;
                        case "IsPointInPolygon":
                            btnEG2DIsPointInPolyGon_Click(DialogResult.No, filePath);
                            break;
                        case "2DPolyLineMove":
                            btnEG2DPolyLineMove_Click(DialogResult.No, filePath);
                            break;
                        case "2DPolyLineRotate":
                            btnEG2DPolyLineRotate_Click(DialogResult.No, filePath);
                            break;
                        case "PolyLinesRelation":
                            btnEGPolyLinesRelation_Click(DialogResult.No, filePath);
                            break;
                        case "btnEG2DPolyCircleDistance":
                            btnEG2DPolyCircleDistance_Click(DialogResult.No, filePath);
                            break;
                        case "EG2DIsPointOnPoly":
                            btnEG2DIsPointOnPoly_Click(DialogResult.No, filePath);
                            break;
                        case "EG2DLinePolyDistance":
                            btnEG2DLinePolyDistance_Click(DialogResult.No, filePath);
                            break;
                        case "EG2DLinePolyIntersection":
                            btnEG2DLinePolyIntersection_Click(DialogResult.No, filePath);
                            break;
                        case "EG2DPolyBoundingRect":
                            btnEG2DPolyBoundingRect_Click(DialogResult.No, filePath);
                            break;
                        case "EG2DPolyCircleIntersection":
                            btnEG2DPolyCircleIntersection_Click(DialogResult.No, filePath);
                            break;
                        case "EG2DPolySelfIntersection":
                            btnEG2DPolySelfIntersection_Click(DialogResult.No, filePath);
                            break;
                        case "EGDistancePoint2Poly":
                            btnEGDistancePoint2Poly_Click(DialogResult.No, filePath);
                            break;
                        case "EGDistancePoly2Poly":
                            btnEGDistancePoly2Poly_Click(DialogResult.No, filePath);
                            break;
                        case "EGPolyLength":
                            btnEGPolyLength_Click(DialogResult.No, filePath);
                            break;
                        case "PolySmoothingSample":
                            btnEG2DPolySmoothingSample_Click(DialogResult.No, filePath);
                            break;
                    }
                    /* }
                     else
                         MessageBox.Show("Invalid file name", "Invalid file name " + filePathWithoutExt);*/
                }

                mMCTime.Stop();

                MessageBox.Show("calc duration:  " + mMCTime.ElapsedMilliseconds.ToString() + " ms", "Automatic Calculations Finished Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);

                EndGeometricCalcAction("Automatic Calculation");
            }
        }

        private void btnEG2DPolyPoleOfInaccessibility_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (btnEG2DPolyPoleOfInaccessibility_Click(dlgResult))
                EndGeometricCalcAction("2D PolyGon Center Of Gravity");
        }

        private bool btnEG2DPolyPoleOfInaccessibility_Click(DialogResult dlgResult = DialogResult.No, string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            //Print result header line
            STW.WriteLine("CoG X, CoG Y, CoG Z");

            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                sourceLineValues = lSourceLines[i].Split(',');

                DNSMcVector3D[] stPolygon = new DNSMcVector3D[sourceLineValues.Length / 3];
                int idx = 0;
                double dPrecision = double.Parse(sourceLineValues[0]);
                for (int cell = 1; cell < sourceLineValues.Length; cell += 3)
                {
                    stPolygon[idx].x = double.Parse(sourceLineValues[cell]);
                    stPolygon[idx].y = double.Parse(sourceLineValues[cell + 1]);
                    stPolygon[idx].z = double.Parse(sourceLineValues[cell + 2]);

                    idx++;
                }

                try
                {
                    DNSMcVector3D PoleOfInaccessibility = IDNMcGeometricCalculations.EG2DPolyPoleOfInaccessibility(stPolygon, dPrecision);

                    outputLine = PoleOfInaccessibility.x.ToString() + "," +
                                    PoleOfInaccessibility.y.ToString() + "," +
                                    PoleOfInaccessibility.z.ToString();

                    STW.WriteLine(outputLine);

                    //Draw result point if function succeed and demanded by the user
                    if (dlgResult == DialogResult.Yes)
                    {
                        DNSMcVector3D[] pointArr = new DNSMcVector3D[] { PoleOfInaccessibility };
                        DrawFunction(false, EDrawnItemType.Point, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, pointArr, i, 0);
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("EG2DPolyGonArea - Line: " + (i + 1).ToString(), McEx);
                    STW.WriteLine();
                }

                //Draw source polygon points if demanded by the user
                if (dlgResult == DialogResult.Yes)
                    DrawFunction(true, EDrawnItemType.Polygon, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, stPolygon, i, 0);
            }

            CloseStreams();
            return true;
        }

    }
}
