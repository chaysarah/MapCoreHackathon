using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using UnmanagedWrapper;
using MapCore;
using MCTester.Managers.ObjectWorld;
using MCTester.Controls;
using System.Diagnostics;
using MCTester.MapWorld;

namespace MCTester.General_Forms
{
    public partial class GeoCalcForm : Form
    {
        private OpenFileDialog OFD;
        private StreamReader STR;
        private StreamWriter STW;
        private List<string> lSourceLines;
        private string[] sourceLineValues;
        private IDNMcGeographicCalculations GeographicCalculations;
        private string outputLine;
        private IDNMcGridCoordinateSystem CurrGridParams;
        private IDNMcGridCoordinateSystem GridCoordinateSystem;

        private IDNMcOverlay m_activeOverlay;
        private DNSMcBColor m_ItemColor;
        private DNEFillStyle m_FillStyle;
        private DNELineStyle m_LineStyle;
        //private EDrawnItemType m_ItemType;
        private IDNMcObjectSchemeItem m_ObjSchemeItem;

        private Stopwatch mMCTime;

        string m_msgEx = "";

        public GeoCalcForm()
        {
            InitializeComponent();
            OFD = new OpenFileDialog();
            CurrGridParams = null;

            mMCTime = new Stopwatch();
        }

        private void ResetExData()
        {
            m_msgEx = DNEMcErrorCode.SUCCESS.ToString();
        }

        private void SetExData(MapCoreException mapCoreException)
        {
            m_msgEx = IDNMcErrors.ErrorCodeToString(mapCoreException.ErrorCode);
        }

        private bool GetSourceLines(string fileName = "")
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

        private void EndGeoCalcAction(string ActionName)
        {
            MessageBox.Show("Action completed", ActionName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CloseStreams()
        {
            STR.Close();
            STW.Close();
        }

        private void CreateGeographicCalculations()
        {

            DNEGridCoordSystemType gridCoordType = (DNEGridCoordSystemType)Enum.Parse(typeof(DNEGridCoordSystemType), sourceLineValues[0]);
            DNEDatumType datumType = (DNEDatumType)Enum.Parse(typeof(DNEDatumType), sourceLineValues[1]);
            int zone = gridCoordType != DNEGridCoordSystemType._EGCS_GENERIC_GRID ? int.Parse(sourceLineValues[2]) : 0;
            string srid = sourceLineValues[2];

            if (CurrGridParams != null)
            {
                if (gridCoordType == DNEGridCoordSystemType._EGCS_GENERIC_GRID)
                {
                    zone = 99999;
                }

                if (CurrGridParams.GetGridCoorSysType() != gridCoordType ||
                    CurrGridParams.GetDatum() != datumType ||
                    CurrGridParams.GetZone() != zone)
                {
                    GridCoordinateSystem = CreateGridCoordSys(gridCoordType, datumType, zone, srid);
                    CurrGridParams = GridCoordinateSystem;

                    GeographicCalculations = DNMcGeographicCalculations.Create(GridCoordinateSystem);
                }
            }
            else
            {

                CurrGridParams = CreateGridCoordSys(gridCoordType, datumType, zone, srid);
                GeographicCalculations = DNMcGeographicCalculations.Create(CurrGridParams);
            }


        }

        private void btnAzimuthAndDistanceBetweenTwoLocations_Click(object sender, EventArgs e)
        {
            CalcAzimuthAndDistanceBetweenTwoLocations();

            EndGeoCalcAction("Azimuth And Distance Between Two Locations");
        }

        private bool CalcAzimuthAndDistanceBetweenTwoLocations(string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            //Print header line
            outputLine = lSourceLines[0] + "," +
                            "(Output) Azimuth" + "," +
                            "Distance" + "," +
                            "Result";

            STW.WriteLine(outputLine);
            try
            {
                //Call to the relevant method and print the result
                for (int i = 1; i < lSourceLines.Count; i++)
                {
                    ResetExData();

                    sourceLineValues = lSourceLines[i].Split(',');

                    //Create new Geographic Calculation if necessary
                    CreateGeographicCalculations();

                    DNSMcVector3D sourceLocation = new DNSMcVector3D();
                    sourceLocation.x = double.Parse(sourceLineValues[3]);
                    sourceLocation.y = double.Parse(sourceLineValues[4]);
                    sourceLocation.z = double.Parse(sourceLineValues[5]);

                    DNSMcVector3D targetLocation = new DNSMcVector3D();
                    targetLocation.x = double.Parse(sourceLineValues[6]);
                    targetLocation.y = double.Parse(sourceLineValues[7]);
                    targetLocation.z = double.Parse(sourceLineValues[8]);

                    bool useHeights = bool.Parse(sourceLineValues[9]);

                    DNMcNullableOut<double> azimuth = new DNMcNullableOut<double>();
                    DNMcNullableOut<double> distance = new DNMcNullableOut<double>();
                    try
                    {
                        GeographicCalculations.AzimuthAndDistanceBetweenTwoLocations(sourceLocation,
                                                                                        targetLocation,
                                                                                        azimuth,
                                                                                        distance,
                                                                                        useHeights);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("AzimuthAndDistanceBetweenTwoLocations - Line: " + (i + 1).ToString(), McEx);
                        SetExData(McEx);
                    }

                    outputLine = lSourceLines[i] + "," +
                                    azimuth.Value.ToString() + "," +
                                    distance.Value.ToString() + "," +
                                    m_msgEx;

                    STW.WriteLine(outputLine);
                }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "CalcAzimuthAndDistanceBetweenTwoLocations");
                CloseStreams();
                return false;
            }
            CloseStreams();
            return true;
        }

        private void btnLocationFromAzimuthAndDistance_Click(object sender, EventArgs e)
        {
            LocationFromAzimuthAndDistance();
            EndGeoCalcAction("Location From Azimuth And Distance");
        }

        private bool LocationFromAzimuthAndDistance(string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            //Print header line
            outputLine = lSourceLines[0] + "," +
                                        "(Output) Target Location(X)" + "," +
                                        "Target Location(Y)" + "," +
                                        "Target Location(Z)" + "," +
                                        "Result";

            STW.WriteLine(outputLine);

            try
            {

                //Call to the relevant method and print the result
                for (int i = 1; i < lSourceLines.Count; i++)
                {
                    ResetExData();
                    sourceLineValues = lSourceLines[i].Split(',');

                    //Create new Geographic Calculation if necessary
                    CreateGeographicCalculations();

                    DNSMcVector3D sourceLocation = new DNSMcVector3D();
                    sourceLocation.x = double.Parse(sourceLineValues[3]);
                    sourceLocation.y = double.Parse(sourceLineValues[4]);
                    sourceLocation.z = double.Parse(sourceLineValues[5]);

                    double azimuth = double.Parse(sourceLineValues[6]);
                    double distance = double.Parse(sourceLineValues[7]);
                    bool useHeights = bool.Parse(sourceLineValues[8]);

                    DNSMcVector3D targetLocation = new DNSMcVector3D();
                    try
                    {
                        targetLocation = GeographicCalculations.LocationFromAzimuthAndDistance(sourceLocation,
                                                                                                azimuth,
                                                                                                distance,
                                                                                                useHeights);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("LocationFromAzimuthAndDistance - Line: " + (i + 1).ToString(), McEx);
                        SetExData(McEx);
                    }

                    outputLine = lSourceLines[i] + "," +
                                    targetLocation.x.ToString() + "," +
                                    targetLocation.y.ToString() + "," +
                                    targetLocation.z.ToString() + "," +
                                    m_msgEx;

                    STW.WriteLine(outputLine);
                }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "LocationFromAzimuthAndDistance");
                CloseStreams();
                return false;
            }
            CloseStreams();
            return true;
        }

        private void btnLocationFromLocationAndVector_Click(object sender, EventArgs e)
        {
            LocationFromLocationAndVector();

            EndGeoCalcAction("Location From Location And Vector");
        }

        private bool LocationFromLocationAndVector(string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            //Print header line
            outputLine = lSourceLines[0] + "," +
                            "(Output) X" + "," +
                            "Y" + "," +
                            "Z" + "," +
                            "Result";

            STW.WriteLine(outputLine);
            try { 
            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                ResetExData();
                sourceLineValues = lSourceLines[i].Split(',');

                //Create new Geographic Calculation if necessary
                CreateGeographicCalculations();

                DNSMcVector3D sourceLocation = new DNSMcVector3D();
                sourceLocation.x = double.Parse(sourceLineValues[3]);
                sourceLocation.y = double.Parse(sourceLineValues[4]);
                sourceLocation.z = double.Parse(sourceLineValues[5]);

                double vectorLengthInMeters = double.Parse(sourceLineValues[6]);
                double vectorAzimuth = double.Parse(sourceLineValues[7]);
                double vectorElevation = double.Parse(sourceLineValues[8]);

                DNSMcVector3D targetLocation = new DNSMcVector3D();
                try
                {
                    targetLocation = GeographicCalculations.LocationFromLocationAndVector(sourceLocation,
                                                                                            vectorLengthInMeters,
                                                                                            vectorAzimuth,
                                                                                            vectorElevation);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("LocationFromLocationAndVector - Line: " + (i + 1).ToString(), McEx);
                    SetExData(McEx);
                }

                outputLine = lSourceLines[i] + "," +
                                targetLocation.x.ToString() + "," +
                                targetLocation.y.ToString() + "," +
                                targetLocation.z.ToString() + "," +
                                m_msgEx;

                STW.WriteLine(outputLine);
            }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "LocationFromLocationAndVector");
                CloseStreams();
                return false;
            }
            CloseStreams();
            return true;

        }

        private void btnVectorFromTwoLocations_Click(object sender, EventArgs e)
        {
            if(VectorFromTwoLocations())
                EndGeoCalcAction("Vector From Two Locations");
        }

        private bool VectorFromTwoLocations(string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            //Print header line
            outputLine = lSourceLines[0] + "," +
                            "(Output) Vector Length In Meters" + "," +
                            "Vector Azimuth" + "," +
                            "Vector Elevation" + "," +
                            "Result";

            STW.WriteLine(outputLine);
            try
            {
                //Call to the relevant method and print the result
                for (int i = 1; i < lSourceLines.Count; i++)
                {
                    ResetExData();
                    sourceLineValues = lSourceLines[i].Split(',');

                    //Create new Geographic Calculation if necessary
                    CreateGeographicCalculations();

                    DNSMcVector3D sourceLocation = new DNSMcVector3D();
                    sourceLocation.x = double.Parse(sourceLineValues[3]);
                    sourceLocation.y = double.Parse(sourceLineValues[4]);
                    sourceLocation.z = double.Parse(sourceLineValues[5]);

                    DNSMcVector3D targetLocation = new DNSMcVector3D();
                    targetLocation.x = double.Parse(sourceLineValues[6]);
                    targetLocation.y = double.Parse(sourceLineValues[7]);
                    targetLocation.z = double.Parse(sourceLineValues[8]);

                    DNMcNullableOut<double> vectorLengthInMeters = new DNMcNullableOut<double>();
                    DNMcNullableOut<double> vectorAzimuth = new DNMcNullableOut<double>();
                    DNMcNullableOut<double> vectorElevation = new DNMcNullableOut<double>();
                    try
                    {
                        GeographicCalculations.VectorFromTwoLocations(sourceLocation,
                                                                        targetLocation,
                                                                        vectorLengthInMeters,
                                                                        vectorAzimuth,
                                                                        vectorElevation);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("VectorFromTwoLocations - Line: " + (i + 1).ToString(), McEx);
                        SetExData(McEx);
                    }

                    outputLine = lSourceLines[i] + "," +
                                    vectorLengthInMeters.Value.ToString() + "," +
                                    vectorAzimuth.Value.ToString() + "," +
                                    vectorElevation.Value.ToString() + "," +
                                    m_msgEx;

                    STW.WriteLine(outputLine);
                }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "VectorFromTwoLocations");
                CloseStreams();
                return false;
            }
            CloseStreams();
            return true;
        }

        private void btnCalcCenterAndLengthsFromRectangle_Click(object sender, EventArgs e)
        {
            if(CalcCenterAndLengthsFromRectangle())
                EndGeoCalcAction("Calc Center And Lengths From Rectangle");
        }

        private bool CalcCenterAndLengthsFromRectangle(string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            //Print header line
            outputLine = lSourceLines[0] + "," +
                            "(Output) Rectangle Center Point(X)" + "," +
                            "Rectangle Center Point(Y)" + "," +
                            "Rectangle Center Point(Z)" + "," +
                            "Rectangle Height" + "," +
                            "Rectangle Width" + "," +
                            "Rotation Azimuth Degrees" + "," +
                            "Result";

            STW.WriteLine(outputLine);
            try
            {
                //Call to the relevant method and print the result
                for (int i = 1; i < lSourceLines.Count; i++)
                {
                    ResetExData();
                    sourceLineValues = lSourceLines[i].Split(',');

                    //Create new Geographic Calculation if necessary
                    CreateGeographicCalculations();

                    DNSMcVector3D leftUp = new DNSMcVector3D();
                    leftUp.x = double.Parse(sourceLineValues[3]);
                    leftUp.y = double.Parse(sourceLineValues[4]);
                    leftUp.z = double.Parse(sourceLineValues[5]);

                    DNSMcVector3D rightUp = new DNSMcVector3D();
                    rightUp.x = double.Parse(sourceLineValues[6]);
                    rightUp.y = double.Parse(sourceLineValues[7]);
                    rightUp.z = double.Parse(sourceLineValues[8]);

                    DNSMcVector3D rightDown = new DNSMcVector3D();
                    rightDown.x = double.Parse(sourceLineValues[9]);
                    rightDown.y = double.Parse(sourceLineValues[10]);
                    rightDown.z = double.Parse(sourceLineValues[11]);

                    DNSMcVector3D leftDown = new DNSMcVector3D();
                    leftDown.x = double.Parse(sourceLineValues[12]);
                    leftDown.y = double.Parse(sourceLineValues[13]);
                    leftDown.z = double.Parse(sourceLineValues[14]);

                    bool bIsGeometric = bool.Parse(sourceLineValues[15]);

                    DNSMcVector3D rectMidPt = new DNSMcVector3D();
                    double rectHeight = 0;
                    double rectWidth = 0;
                    double rotationAzimuthDeg = 0;
                    try
                    {
                        GeographicCalculations.CalcCenterAndLengthsFromRectangle(leftUp,
                                                                                    rightUp,
                                                                                    rightDown,
                                                                                    leftDown,
                                                                                    out rectMidPt,
                                                                                    out rectHeight,
                                                                                    out rectWidth,
                                                                                    out rotationAzimuthDeg,
                                                                                    bIsGeometric);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("CalcCenterAndLengthsFromRectangle - Line: " + (i + 1).ToString(), McEx);
                        SetExData(McEx);
                    }

                    outputLine = lSourceLines[i] + "," +
                                    rectMidPt.x.ToString() + "," +
                                    rectMidPt.y.ToString() + "," +
                                    rectMidPt.z.ToString() + "," +
                                    rectHeight.ToString() + "," +
                                    rectWidth.ToString() + "," +
                                    rotationAzimuthDeg.ToString() + "," +
                                    m_msgEx;

                    STW.WriteLine(outputLine);
                }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "CalcCenterAndLengthsFromRectangle");
                CloseStreams();
                return false;
            }
            CloseStreams();
            return true;
        }

        private void btnCalcCenterAndLengthsFrom2PtRectangle_Click(object sender, EventArgs e)
        {
            if(CalcCenterAndLengthsFrom2PtRectangle())
                EndGeoCalcAction("Calc Center And Lengths From 2 Points Rectangle");
        }

        private bool CalcCenterAndLengthsFrom2PtRectangle(string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            //Print header line
            outputLine = lSourceLines[0] + "," +
                            "(Output) Rectangle Center Point(X)" + "," +
                            "Rectangle Center Point(Y)" + "," +
                            "Rectangle Center Point(Z)" + "," +
                            "Rectangle Height" + "," +
                            "Rectangle Width" + "," +
                            "Result";

            STW.WriteLine(outputLine);
            try
            {
                //Call to the relevant method and print the result
                for (int i = 1; i < lSourceLines.Count; i++)
                {
                    ResetExData();
                    sourceLineValues = lSourceLines[i].Split(',');

                    //Create new Geographic Calculation if necessary
                    CreateGeographicCalculations();

                    DNSMcVector3D leftUp = new DNSMcVector3D();
                    leftUp.x = double.Parse(sourceLineValues[3]);
                    leftUp.y = double.Parse(sourceLineValues[4]);
                    leftUp.z = double.Parse(sourceLineValues[5]);

                    DNSMcVector3D rightDown = new DNSMcVector3D();
                    rightDown.x = double.Parse(sourceLineValues[6]);
                    rightDown.y = double.Parse(sourceLineValues[7]);
                    rightDown.z = double.Parse(sourceLineValues[8]);

                    double rotationAzimuthDeg = double.Parse(sourceLineValues[9]);

                    bool bIsGeometric = bool.Parse(sourceLineValues[10]);

                    DNSMcVector3D rectMidPt = new DNSMcVector3D();
                    double rectHeight = 0;
                    double rectWidth = 0;

                    try
                    {
                        GeographicCalculations.CalcCenterAndLengthsFromRectangle(leftUp,
                                                                                    rightDown,
                                                                                    rotationAzimuthDeg,
                                                                                    out rectMidPt,
                                                                                    out rectHeight,
                                                                                    out rectWidth,
                                                                                    bIsGeometric);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("CalcCenterAndLengthsFromRectangle - Line: " + (i + 1).ToString(), McEx);
                        SetExData(McEx);
                    }

                    outputLine = lSourceLines[i] + "," +
                                    rectMidPt.x.ToString() + "," +
                                    rectMidPt.y.ToString() + "," +
                                    rectMidPt.z.ToString() + "," +
                                    rectHeight.ToString() + "," +
                                    rectWidth.ToString() + "," +
                                    m_msgEx;

                    STW.WriteLine(outputLine);
                }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "CalcCenterAndLengthsFrom2PtRectangle");
                CloseStreams();
                return false;
            }
            CloseStreams();
            return true;
        }

        private void btnCalcRectangleCenterFromCornerAndLengths_Click(object sender, EventArgs e)
        {
            if(CalcRectangleCenterFromCornerAndLengths())
                EndGeoCalcAction("Calc Rectangle Center From Corner And Lengths");
        }

        private bool CalcRectangleCenterFromCornerAndLengths(string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            //Print header line
            outputLine = lSourceLines[0] + "," +
                            "(Output) Rectangle Center Point(X)" + "," +
                            "Rectangle Center Point(Y)" + "," +
                            "Rectangle Center Point(Z)" + "," +
                            "Result";


            STW.WriteLine(outputLine);
            try
            {
                //Call to the relevant method and print the result
                for (int i = 1; i < lSourceLines.Count; i++)
                {
                    ResetExData();
                    sourceLineValues = lSourceLines[i].Split(',');

                    //Create new Geographic Calculation if necessary
                    CreateGeographicCalculations();

                    DNSMcVector3D rectCornerPt = new DNSMcVector3D();
                    rectCornerPt.x = double.Parse(sourceLineValues[3]);
                    rectCornerPt.y = double.Parse(sourceLineValues[4]);
                    rectCornerPt.z = double.Parse(sourceLineValues[5]);

                    double rectHeight = double.Parse(sourceLineValues[6]);
                    double rectWidth = double.Parse(sourceLineValues[7]);
                    double rotationAzimuthDeg = double.Parse(sourceLineValues[8]);

                    DNERectangleCorner cornerMeaninig = (DNERectangleCorner)Enum.Parse(typeof(DNERectangleCorner), sourceLineValues[9]);

                    DNSMcVector3D centerPt = new DNSMcVector3D();

                    try
                    {
                        GeographicCalculations.CalcRectangleCenterFromCornerAndLengths(rectCornerPt,
                                                                                        rectHeight,
                                                                                        rectWidth,
                                                                                        rotationAzimuthDeg,
                                                                                        cornerMeaninig,
                                                                                        out centerPt);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("CalcRectangleCenterFromCornerAndLengths - Line: " + (i + 1).ToString(), McEx);
                        SetExData(McEx);
                    }

                    outputLine = lSourceLines[i] + "," +
                                    centerPt.x.ToString() + "," +
                                    centerPt.y.ToString() + "," +
                                    centerPt.z.ToString() + "," +
                                    m_msgEx;

                    STW.WriteLine(outputLine);
                }
            }

            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "CalcRectangleCenterFromCornerAndLengths");
                CloseStreams();
                return false;
            }
            CloseStreams();
            return true;
        }

        private void btnConvertAtoB_Click(object sender, EventArgs e)
        {
            if(ConvertAtoB())
            EndGeoCalcAction("Convert A to B");
        }

        private bool ConvertAtoB(string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            //Print header line
            outputLine = lSourceLines[0] + "," +
                            "(Output) Location B(X)" + "," +
                            "Location B(Y)" + "," +
                            "Location B(Z)" + "," +
                            "Zone B" + "," +
                            "Check Grid Limits" + "," +
                            "Check Converting Height" + "," +
                            "Result";

            STW.WriteLine(outputLine);

            DNEGridCoordSystemType gridTypeA;
            DNEDatumType datumTypeA = DNEDatumType._EDT_ED50_ISRAEL;
            int zoneA;

            DNEGridCoordSystemType gridTypeB;
            DNEDatumType datumTypeB = DNEDatumType._EDT_ED50_ISRAEL;
            int zoneB;

            IDNMcGridCoordinateSystem gridCoordSysA;
            IDNMcGridCoordinateSystem gridCoordSysB;
            DNSMcBox boxA = new DNSMcBox();

            try
            {
                //Call to the relevant method and print the result
                for (int i = 1; i < lSourceLines.Count; i++)
                {
                    ResetExData();

                    sourceLineValues = lSourceLines[i].Split(',');

                    boxA.MinVertex.x = double.Parse(sourceLineValues[0]);
                    boxA.MinVertex.y = double.Parse(sourceLineValues[1]);
                    boxA.MinVertex.z = double.Parse(sourceLineValues[2]);
                    boxA.MaxVertex.x = double.Parse(sourceLineValues[3]);
                    boxA.MaxVertex.y = double.Parse(sourceLineValues[4]);
                    boxA.MaxVertex.z = double.Parse(sourceLineValues[5]);

                    gridTypeA = (DNEGridCoordSystemType)Enum.Parse(typeof(DNEGridCoordSystemType), sourceLineValues[6]);
                    if (sourceLineValues[7] != "")
                        datumTypeA = (DNEDatumType)Enum.Parse(typeof(DNEDatumType), sourceLineValues[7]);
                    string sridA = sourceLineValues[8];
                    if (gridTypeA == DNEGridCoordSystemType._EGCS_GENERIC_GRID)
                    {
                        zoneA = 0;
                    }
                    else
                    {
                        zoneA = int.Parse(sourceLineValues[8]);
                    }

                    gridTypeB = (DNEGridCoordSystemType)Enum.Parse(typeof(DNEGridCoordSystemType), sourceLineValues[9]);
                    if (sourceLineValues[10] != "")
                        datumTypeB = (DNEDatumType)Enum.Parse(typeof(DNEDatumType), sourceLineValues[10]);
                    if (gridTypeB == DNEGridCoordSystemType._EGCS_GENERIC_GRID)
                    {
                        zoneB = 0;
                    }
                    else
                    {
                        zoneB = int.Parse(sourceLineValues[11]);
                    }
                    string sridB = sourceLineValues[11];

                    DNSMcVector3D locationA = new DNSMcVector3D();
                    locationA.x = double.Parse(sourceLineValues[12]);
                    locationA.y = double.Parse(sourceLineValues[13]);
                    locationA.z = double.Parse(sourceLineValues[14]);

                    DNSMcVector3D locationB = new DNSMcVector3D();
                    int zoneResult = 0;
                    bool CheckGridLimit = false;
                    bool CheckConvertingHeight = false;

                    try
                    {
                        gridCoordSysA = CreateGridCoordSys(gridTypeA, datumTypeA, zoneA, sridA);

                        if (boxA.MinVertex.x != 0 || boxA.MinVertex.y != 0 || boxA.MinVertex.z != 0 ||
                            boxA.MaxVertex.x != 0 || boxA.MaxVertex.y != 0 || boxA.MaxVertex.z != 0)
                        {
                            gridCoordSysA.SetLegalValuesForGeographicCoordinates(boxA);
                        }
                        gridCoordSysB = CreateGridCoordSys(gridTypeB, datumTypeB, zoneB, sridB);

                        IDNMcGridConverter gridCnvrt = DNMcGridConverter.Create(gridCoordSysA, gridCoordSysB);
                        gridCnvrt.SetCheckGridLimits(bool.Parse(sourceLineValues[15]));
                        gridCnvrt.SetConvertingHeight(bool.Parse(sourceLineValues[16]));

                        CheckGridLimit = gridCnvrt.GetCheckGridLimits();
                        CheckConvertingHeight = gridCnvrt.GetConvertingHeight();

                        gridCnvrt.ConvertAtoB(locationA,
                                                out locationB,
                                                out zoneResult);

                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("ConvertAtoB - Line: " + (i + 1).ToString(), McEx);
                        SetExData(McEx);
                    }

                    outputLine = lSourceLines[i] + "," +
                                    locationB.x.ToString() + "," +
                                    locationB.y.ToString() + "," +
                                    locationB.z.ToString() + "," +
                                    zoneResult.ToString() + "," +
                                    CheckGridLimit.ToString() + "," +
                                    CheckConvertingHeight + "," +
                                    m_msgEx;

                    STW.WriteLine(outputLine);
                }

            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "ConvertAtoB");
                CloseStreams();
                return false;
            }

            CloseStreams();
            return true;
        }

        private void btnConvertBtoA_Click(object sender, EventArgs e)
        {
            if(ConvertBtoA())
                EndGeoCalcAction("Convert B to A");
        }

        private bool ConvertBtoA(string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            //Print header line
            outputLine = lSourceLines[0] + "," +
                            "(Output) Location A(X)" + "," +
                            "Location A(Y)" + "," +
                            "Location A(Z)" + "," +
                            "Zone A" + "," +
                            "Check Grid Limits" + "," +
                            "Check Converting Height" + "," +
                            "Result"; ;

            STW.WriteLine(outputLine);

            DNEGridCoordSystemType gridTypeA;
            DNEDatumType datumTypeA;
            int zoneA = 0;
            string sridA;

            DNEGridCoordSystemType gridTypeB;
            DNEDatumType datumTypeB;
            int zoneB = 0;
            string sridB;

            IDNMcGridCoordinateSystem gridCoordSysA;
            IDNMcGridCoordinateSystem gridCoordSysB;
            DNSMcBox boxB = new DNSMcBox();

            try
            {
                //Call to the relevant method and print the result
                for (int i = 1; i < lSourceLines.Count; i++)
                {
                    ResetExData();
                    sourceLineValues = lSourceLines[i].Split(',');

                    boxB.MinVertex.x = double.Parse(sourceLineValues[0]);
                    boxB.MinVertex.y = double.Parse(sourceLineValues[1]);
                    boxB.MinVertex.z = double.Parse(sourceLineValues[2]);
                    boxB.MaxVertex.x = double.Parse(sourceLineValues[3]);
                    boxB.MaxVertex.y = double.Parse(sourceLineValues[4]);
                    boxB.MaxVertex.z = double.Parse(sourceLineValues[5]);

                    gridTypeB = (DNEGridCoordSystemType)Enum.Parse(typeof(DNEGridCoordSystemType), sourceLineValues[6]);
                    datumTypeB = (DNEDatumType)Enum.Parse(typeof(DNEDatumType), sourceLineValues[7]);
                    if (gridTypeB == DNEGridCoordSystemType._EGCS_GENERIC_GRID)
                    {
                        zoneB = 0;
                    }
                    else
                    {
                        zoneB = int.Parse(sourceLineValues[8]);
                    }
                    sridB = sourceLineValues[8];

                    gridTypeA = (DNEGridCoordSystemType)Enum.Parse(typeof(DNEGridCoordSystemType), sourceLineValues[9]);
                    datumTypeA = (DNEDatumType)Enum.Parse(typeof(DNEDatumType), sourceLineValues[10]);
                    if (gridTypeA == DNEGridCoordSystemType._EGCS_GENERIC_GRID)
                    {
                        zoneA = 0;
                    }
                    else
                    {
                        zoneA = int.Parse(sourceLineValues[11]);
                    }
                    sridA = sourceLineValues[11];

                    DNSMcVector3D locationB = new DNSMcVector3D();
                    locationB.x = double.Parse(sourceLineValues[12]);
                    locationB.y = double.Parse(sourceLineValues[13]);
                    locationB.z = double.Parse(sourceLineValues[14]);

                    int zoneResult = 0;
                    DNSMcVector3D locationA = new DNSMcVector3D();
                    bool CheckGridLimit = false;
                    bool CheckConvertingHeight = false;
                    try
                    {
                        gridCoordSysB = CreateGridCoordSys(gridTypeB, datumTypeB, zoneB, sridB);


                        if (boxB.MinVertex.x != 0 || boxB.MinVertex.y != 0 || boxB.MinVertex.z != 0 ||
                            boxB.MaxVertex.x != 0 || boxB.MaxVertex.y != 0 || boxB.MaxVertex.z != 0)
                        {
                            gridCoordSysB.SetLegalValuesForGeographicCoordinates(boxB);
                        }
                        gridCoordSysA = CreateGridCoordSys(gridTypeA, datumTypeA, zoneA, sridA);

                        IDNMcGridConverter gridCnvrt = DNMcGridConverter.Create(gridCoordSysA, gridCoordSysB);

                        gridCnvrt.SetCheckGridLimits(bool.Parse(sourceLineValues[15]));
                        gridCnvrt.SetConvertingHeight(bool.Parse(sourceLineValues[16]));

                        CheckGridLimit = gridCnvrt.GetCheckGridLimits();
                        CheckConvertingHeight = gridCnvrt.GetConvertingHeight();

                        gridCnvrt.ConvertBtoA(locationB,
                                                out locationA,
                                                out zoneResult);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("ConvertBtoA - Line: " + (i + 1).ToString(), McEx);
                        SetExData(McEx);
                    }

                    outputLine = lSourceLines[i] + "," +
                                    locationA.x.ToString() + "," +
                                    locationA.y.ToString() + "," +
                                    locationA.z.ToString() + "," +
                                    zoneA.ToString() + "," +
                                    CheckGridLimit.ToString() + "," +
                                    CheckConvertingHeight + "," +
                                    m_msgEx;

                    STW.WriteLine(outputLine);
                }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "ConvertBtoA");
                CloseStreams();
                return false;
            }
            CloseStreams();
            return true;
        }

        private void btnPolygonExpand_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(PolygonExpand())
                EndGeoCalcAction("Polygon Expand");
        }

        private bool PolygonExpand(string filePath = "", DialogResult dlgResult = DialogResult.No)
        {
            if (!GetSourceLines(filePath))
                return false;

            int maxNumOfVertices = 0;

            List<DNSMcVector3D[]> lExpandedPolygonVertices = new List<DNSMcVector3D[]>();
            List<string> lResults = new List<string>();
            try
            {
                //Call to the relevant method and print the result
                for (int i = 1; i < lSourceLines.Count; i++)
                {
                    ResetExData();
                    sourceLineValues = lSourceLines[i].Split(',');


                    //Create new Geographic Calculation if necessary
                    CreateGeographicCalculations();

                    double expansionDistance = double.Parse(sourceLineValues[3]);
                    uint numPointsInArc = uint.Parse(sourceLineValues[4]);

                    List<DNSMcVector3D> lOriginPolygonVertices = new List<DNSMcVector3D>();
                    DNSMcVector3D vertice = new DNSMcVector3D();

                    //Collect the polygon vertices to a DNSMcVector3D list
                    for (int idx = 5; idx < sourceLineValues.Length; idx += 3)
                    {
                        vertice.x = double.Parse(sourceLineValues[idx]);
                        vertice.y = double.Parse(sourceLineValues[idx + 1]);
                        vertice.z = double.Parse(sourceLineValues[idx + 2]);

                        lOriginPolygonVertices.Add(vertice);
                    }

                    DNSMcVector3D[] polygonVertices = lOriginPolygonVertices.ToArray();

                    //Draw original polygon if demanded by the user
                    if (dlgResult == DialogResult.Yes)
                        DrawActionResult(true, true, polygonVertices);

                    DNSMcVector3D[] expandedPolygonVertices = null;
                    try
                    {
                        expandedPolygonVertices = GeographicCalculations.PolygonExpand(polygonVertices,
                                                                                       expansionDistance,
                                                                                       numPointsInArc);

                        //Fined the longest result in order to match the header line
                        if (expandedPolygonVertices.Length > maxNumOfVertices)
                            maxNumOfVertices = expandedPolygonVertices.Length;
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("PolygonExpand - Line: " + (i + 1).ToString(), McEx);
                        SetExData(McEx);
                    }

                    //Draw the expanded polygon if demanded by the user
                    if (dlgResult == DialogResult.Yes)
                        DrawActionResult(true, false, expandedPolygonVertices);

                    //Add the result to a result list that will be printed at the end of the process.
                    lExpandedPolygonVertices.Add(expandedPolygonVertices);
                    lResults.Add(m_msgEx);

                }

                outputLine = "(Output) Result ,";

                //Print result header line
                for (int i = 0; i < maxNumOfVertices; i++)
                {
                    outputLine += "X" + i + "," +
                                  "Y" + i + "," +
                                  "Z" + i + ",";

                }

                STW.WriteLine(outputLine);
                outputLine = "";

                for (int lIdx = 0; lIdx < lExpandedPolygonVertices.Count; lIdx++)
                {
                    if (lExpandedPolygonVertices[lIdx] != null)
                    {
                        outputLine += lResults[lIdx] + ",";
                        for (int ArrIdx = 0; ArrIdx < lExpandedPolygonVertices[lIdx].Length; ArrIdx++)
                        {
                            outputLine += lExpandedPolygonVertices[lIdx][ArrIdx].x.ToString() + "," +
                                            lExpandedPolygonVertices[lIdx][ArrIdx].y.ToString() + "," +
                                            lExpandedPolygonVertices[lIdx][ArrIdx].z.ToString() + ",";
                        }


                        STW.WriteLine(outputLine);
                        outputLine = "";
                    }
                }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "PolygonExpand");
                CloseStreams();
                return false;
            }
            CloseStreams();
            return true;
        }

        private void btnPolylineExpand_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(PolylineExpand("", dlgResult))
            EndGeoCalcAction("Polyline Expand");
        }

        private bool PolylineExpand(string filePath = "", DialogResult dlgResult = DialogResult.No)
        {
            if (!GetSourceLines(filePath))
                return false;

            int maxNumOfVertices = 0;

            List<DNSMcVector3D[]> lExpandedPolylineVertices = new List<DNSMcVector3D[]>();
            List<string> lResults = new List<string>();
            try
            {
                //Call to the relevant method and print the result
                for (int i = 1; i < lSourceLines.Count; i++)
                {
                    ResetExData();
                    sourceLineValues = lSourceLines[i].Split(',');


                    //Create new Geographic Calculation if necessary
                    CreateGeographicCalculations();


                    double expansionDistance = double.Parse(sourceLineValues[3]);
                    uint numPointsInArc = uint.Parse(sourceLineValues[4]);

                    List<DNSMcVector3D> lOriginPolylineVertices = new List<DNSMcVector3D>();
                    DNSMcVector3D vertice = new DNSMcVector3D();

                    //Collect the polyline vertices to a DNSMcVector3D list
                    for (int idx = 5; idx < sourceLineValues.Length; idx += 3)
                    {
                        vertice.x = double.Parse(sourceLineValues[idx]);
                        vertice.y = double.Parse(sourceLineValues[idx + 1]);
                        vertice.z = double.Parse(sourceLineValues[idx + 2]);

                        lOriginPolylineVertices.Add(vertice);
                    }

                    DNSMcVector3D[] polylineVertices = lOriginPolylineVertices.ToArray();

                    //Draw original polyline if demanded by the user
                    if (dlgResult == DialogResult.Yes)
                        DrawActionResult(false, true, polylineVertices);

                    DNSMcVector3D[] expandedPolylineVertices = null;
                    try
                    {
                        expandedPolylineVertices = GeographicCalculations.PolylineExpand(polylineVertices,
                                                                                           expansionDistance,
                                                                                           numPointsInArc);
                        //Fined the longest result in order to match the header line
                        if (expandedPolylineVertices.Length > maxNumOfVertices)
                            maxNumOfVertices = expandedPolylineVertices.Length;

                        //Draw the expanded polyline if demanded by the user
                        if (dlgResult == DialogResult.Yes)
                            DrawActionResult(false, false, expandedPolylineVertices);

                        //Add the result to a result list that will be printed at the end of the process.
                        lExpandedPolylineVertices.Add(expandedPolylineVertices);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("PolylineExpand - Line: " + (i + 1).ToString(), McEx);
                        SetExData(McEx);
                    }
                    lResults.Add(m_msgEx);
                }
                outputLine = "(Output) Result ,";
                //Print result header line
                for (int i = 0; i < maxNumOfVertices; i++)
                {
                    outputLine += "X" + i + "," +
                                    "Y" + i + "," +
                                    "Z" + i + ",";

                }

                STW.WriteLine(outputLine);
                outputLine = "";

                for (int lIdx = 0; lIdx < lExpandedPolylineVertices.Count; lIdx++)
                {
                    outputLine += lResults[lIdx] + ",";
                    for (int ArrIdx = 0; ArrIdx < lExpandedPolylineVertices[lIdx].Length; ArrIdx++)
                    {
                        outputLine += lExpandedPolylineVertices[lIdx][ArrIdx].x.ToString() + "," +
                                        lExpandedPolylineVertices[lIdx][ArrIdx].y.ToString() + "," +
                                        lExpandedPolylineVertices[lIdx][ArrIdx].z.ToString() + ",";
                    }

                    STW.WriteLine(outputLine);
                    outputLine = "";
                }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "PolylineExpand");
                CloseStreams();
                return false;
            }
            CloseStreams();
            return true;
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
                fillStyle = DNEFillStyle._EFS_BDIAGONAL;
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
                                                                                    DNSMcBColor.bcBlackOpaque,
                                                                                    2,
                                                                                    null,
                                                                                    new DNSMcFVector2D(1, 1),
                                                                                    1f,
                                                                                    fillStyle,
                                                                                    color);

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

        private void btnArcSample_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(CalcArcSample("", dlgResult))
                EndGeoCalcAction("Arc Sample");
        }

        private bool CalcArcSample(string filePath = "", DialogResult dlgResult = DialogResult.No)
        {
            if (!GetSourceLines(filePath))
                return false;

            int maxNumOfVertices = 0;
            List<DNSMcVector3D[]> lArcPoints = new List<DNSMcVector3D[]>();
            List<string> lResults = new List<string>();

            try
            {

                //Call to the relevant method and print the result
                for (int i = 1; i < lSourceLines.Count; i++)
                {
                    ResetExData();
                    sourceLineValues = lSourceLines[i].Split(',');

                    //Create new Geographic Calculation if necessary
                    CreateGeographicCalculations();

                    DNSEllipseArc ellipseArc = new DNSEllipseArc();
                    ellipseArc.Center.x = double.Parse(sourceLineValues[3]);
                    ellipseArc.Center.y = double.Parse(sourceLineValues[4]);
                    ellipseArc.Center.z = double.Parse(sourceLineValues[5]);
                    ellipseArc.dRadiusX = double.Parse(sourceLineValues[6]);
                    ellipseArc.dRadiusY = double.Parse(sourceLineValues[7]);
                    ellipseArc.dRotationAngle = double.Parse(sourceLineValues[8]);
                    ellipseArc.dInnerRadiusFactor = double.Parse(sourceLineValues[9]);
                    ellipseArc.bClockWise = bool.Parse(sourceLineValues[10]);
                    ellipseArc.dStartAzimuth = double.Parse(sourceLineValues[11]);
                    ellipseArc.dEndAzimuth = double.Parse(sourceLineValues[12]);

                    uint fullEllipseReqPoints = uint.Parse(sourceLineValues[13]);

                    DNSMcVector3D[] arcPoints = null;
                    try
                    {
                        arcPoints = GeographicCalculations.ArcSample(ellipseArc,
                                                                         fullEllipseReqPoints);

                        //Fined the longest result in order to match the header line
                        if (arcPoints.Length > maxNumOfVertices)
                            maxNumOfVertices = arcPoints.Length;
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("ArcSample - Line: " + (i + 1).ToString(), McEx);
                        SetExData(McEx);
                    }


                    //Draw the arc points if demanded by the user
                    if (dlgResult == DialogResult.Yes)
                        DrawActionResult(false, false, arcPoints);

                    //Add the result to a result list that will be printed at the end of the process.
                    lArcPoints.Add(arcPoints);
                    lResults.Add(m_msgEx);
                }
                outputLine = lSourceLines[0] + "," + "(Output) Result , ";

                //Print result header line
                for (int i = 0; i < maxNumOfVertices; i++)
                {
                    outputLine += "X" + i + "," +
                                  "Y" + i + "," +
                                  "Z" + i + ",";

                }

                STW.WriteLine(outputLine);
                outputLine = "";

                for (int lIdx = 0; lIdx < lArcPoints.Count; lIdx++)
                {
                    outputLine = lSourceLines[lIdx + 1] + "," + lResults[lIdx] + ",";

                    for (int ArrIdx = 0; ArrIdx < lArcPoints[lIdx].Length; ArrIdx++)
                    {
                        outputLine += lArcPoints[lIdx][ArrIdx].x.ToString() + "," +
                                        lArcPoints[lIdx][ArrIdx].y.ToString() + "," +
                                        lArcPoints[lIdx][ArrIdx].z.ToString() + ",";
                    }
                    STW.WriteLine(outputLine);
                    outputLine = "";
                }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "CalcArcSample");
                CloseStreams();
                return false;
            }
            CloseStreams();
            return true;
        }

        private void btnLineSample_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(LineSample("", dlgResult))
            EndGeoCalcAction("Line Sample");
        }

        private bool LineSample(string filePath = "", DialogResult dlgResult = DialogResult.No)
        {
            if (!GetSourceLines(filePath))
                return false;

            int maxNumOfVertices = 0;
            List<DNSMcVector3D[]> lLinePoints = new List<DNSMcVector3D[]>();
            List<string> lResults = new List<string>();

            try
            {

                //Call to the relevant method and print the result
                for (int i = 1; i < lSourceLines.Count; i++)
                {
                    ResetExData();
                    sourceLineValues = lSourceLines[i].Split(',');

                    //Create new Geographic Calculation if necessary
                    CreateGeographicCalculations();

                    DNSMcVector3D startPoint = new DNSMcVector3D();
                    startPoint.x = double.Parse(sourceLineValues[3]);
                    startPoint.y = double.Parse(sourceLineValues[4]);
                    startPoint.z = double.Parse(sourceLineValues[5]);

                    DNSMcVector3D endPoint = new DNSMcVector3D();
                    endPoint.x = double.Parse(sourceLineValues[6]);
                    endPoint.y = double.Parse(sourceLineValues[7]);
                    endPoint.z = double.Parse(sourceLineValues[8]);

                    double maxError = double.Parse(sourceLineValues[9]);

                    DNSMcVector3D[] linePoints = null;
                    try
                    {
                        linePoints = GeographicCalculations.LineSample(startPoint,
                                                                        endPoint,
                                                                        maxError);

                        //Fined the longest result in order to match the header line
                        if (linePoints.Length > maxNumOfVertices)
                            maxNumOfVertices = linePoints.Length;

                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("LineSample - Line: " + (i + 1).ToString(), McEx);
                        SetExData(McEx);
                    }


                    //Draw the line points if demanded by the user
                    if (dlgResult == DialogResult.Yes)
                        DrawActionResult(false, true, linePoints);

                    //Add the result to a result list that will be printed at the end of the process.
                    lLinePoints.Add(linePoints);
                    lResults.Add(m_msgEx);
                }

                outputLine = lSourceLines[0] + "," + "(Output) Result, ";
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
                    outputLine = lSourceLines[lIdx + 1] + "," + lResults[lIdx] + ",";
                    for (int ArrIdx = 0; ArrIdx < lLinePoints[lIdx].Length; ArrIdx++)
                    {
                        outputLine += lLinePoints[lIdx][ArrIdx].x.ToString() + "," +
                                        lLinePoints[lIdx][ArrIdx].y.ToString() + "," +
                                        lLinePoints[lIdx][ArrIdx].z.ToString() + ",";
                    }
                    STW.WriteLine(outputLine);
                    outputLine = "";
                }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "LineSample");
                CloseStreams();
                return false;
            }
            CloseStreams();
            return true;
        }

        private void btnPolyLineLength_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            PolyLineLength("", dlgResult);
            EndGeoCalcAction("PolyLine Length");
        }

        private void PolyLineLength(string filePath = "", DialogResult dlgResult = DialogResult.No)
        {
            if (!GetSourceLines(filePath))
                return;

            //Print header line
            outputLine = lSourceLines[0] + "," +
                            "(Output) Distance" + "," +
                            "Result";

            STW.WriteLine(outputLine);
            outputLine = "";
            try
            {
                //Call to the relevant method and print the result
                for (int i = 1; i < lSourceLines.Count; i++)
                {
                    ResetExData();
                    sourceLineValues = lSourceLines[i].Split(',');


                    //Create new Geographic Calculation if necessary
                    List<DNSMcVector3D> lOriginPolylineVertices = new List<DNSMcVector3D>();
                    DNSMcVector3D vertice = new DNSMcVector3D();
                    CreateGeographicCalculations();

                    bool useHeight = bool.Parse(sourceLineValues[3]);

                    //Collect the polyline vertices to a DNSMcVector3D list
                    for (int idx = 4; idx < sourceLineValues.Length; idx += 3)
                    {
                        vertice.x = double.Parse(sourceLineValues[idx]);
                        vertice.y = double.Parse(sourceLineValues[idx + 1]);
                        vertice.z = double.Parse(sourceLineValues[idx + 2]);

                        lOriginPolylineVertices.Add(vertice);
                    }

                    DNSMcVector3D[] polylineVertices = lOriginPolylineVertices.ToArray();

                    //Draw original polyline if demanded by the user
                    if (dlgResult == DialogResult.Yes)
                        DrawActionResult(false, true, polylineVertices);

                    double distance = 0;
                    try
                    {
                        GeographicCalculations.PolyLineLength(polylineVertices,
                                                                out distance,
                                                                useHeight);

                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("PolyLineLength - Line: " + (i + 1).ToString(), McEx);
                        SetExData(McEx);
                    }

                    outputLine = lSourceLines[i] + "," +
                                    distance.ToString() + "," +
                                    m_msgEx;


                    STW.WriteLine(outputLine);
                }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "PolyLineLength");
            }

            CloseStreams();
        }

        private void btnPolygonSphericArea_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            PolygonSphericArea("", dlgResult);
            EndGeoCalcAction("Polygon Spheric Area");
        }

        private void PolygonSphericArea(string filePath = "", DialogResult dlgResult = DialogResult.No)
        {
            if (!GetSourceLines(filePath))
                return;

            //Print header line
            outputLine = "(Output) Area" + "," +

                            "Result";

            STW.WriteLine(outputLine);
            try
            {
                //Call to the relevant method and print the result
                for (int i = 1; i < lSourceLines.Count; i++)
                {
                    ResetExData();
                    sourceLineValues = lSourceLines[i].Split(',');

                    //Create new Geographic Calculation if necessary
                    CreateGeographicCalculations();

                    double earthLocalRadius = double.Parse(sourceLineValues[3]);

                    List<DNSMcVector3D> lOriginPolygonVertices = new List<DNSMcVector3D>();
                    DNSMcVector3D vertice = new DNSMcVector3D();

                    //Collect the polygon vertices to a DNSMcVector3D list
                    for (int idx = 4; idx < sourceLineValues.Length; idx += 3)
                    {
                        vertice.x = double.Parse(sourceLineValues[idx]);
                        vertice.y = double.Parse(sourceLineValues[idx + 1]);
                        vertice.z = double.Parse(sourceLineValues[idx + 2]);

                        lOriginPolygonVertices.Add(vertice);
                    }

                    DNSMcVector3D[] polygonVertices = lOriginPolygonVertices.ToArray();

                    //Draw original polygon if demanded by the user
                    if (dlgResult == DialogResult.Yes)
                        DrawActionResult(true, true, polygonVertices);

                    double area = 0;
                    try
                    {
                        if (!GeographicCalculations.PolygonSphericArea(polygonVertices,
                                                                    earthLocalRadius,
                                                                    out area))
                        {
                            // emulate function's previous behavior
                            throw new MapCoreException(DNEMcErrorCode.CROSSING_POLYGONS, "Polygons intersect");
                        }
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("PolygonSphericArea - Line: " + (i + 1).ToString(), McEx);
                        SetExData(McEx);
                    }

                    outputLine = area.ToString() + "," +
                                    m_msgEx;

                    STW.WriteLine(outputLine);
                }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "PolygonSphericArea");
            }

            CloseStreams();
        }

        private void btnCalcLocalRadius_Click(object sender, EventArgs e)
        {
            CalcLocalRadius();
            EndGeoCalcAction("Calc Local Radius");
        }

        private void CalcLocalRadius(string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return;

            //Print header line
            outputLine = lSourceLines[0] + "," +
                            "(Output) Local Mean Radius" + "," +
                            "Result";

            STW.WriteLine(outputLine);
            try { 
            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                ResetExData();
                sourceLineValues = lSourceLines[i].Split(',');

                //Create new Geographic Calculation if necessary
                CreateGeographicCalculations();

                DNSMcVector3D basePoint = new DNSMcVector3D();
                basePoint.x = double.Parse(sourceLineValues[3]);
                basePoint.y = double.Parse(sourceLineValues[4]);
                basePoint.z = double.Parse(sourceLineValues[5]);

                double localMeanRadius = 0;
                try
                {
                    GeographicCalculations.CalcLocalRadius(basePoint,
                                                            out localMeanRadius);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("CalcLocalRadius - Line: " + (i + 1).ToString(), McEx);
                    SetExData(McEx);
                }


                outputLine = lSourceLines[i] + "," +
                                localMeanRadius.ToString() + "," +
                                m_msgEx;


                STW.WriteLine(outputLine);
            }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "CalcLocalRadius");
            }
            CloseStreams();
        }

        private void btnCalcLocalAzimuthRadius_Click(object sender, EventArgs e)
        {
            CalcLocalAzimuthRadius();
            EndGeoCalcAction("Calc Local Azimuth Radius");
        }

        private void CalcLocalAzimuthRadius(string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return;

            //Print header line
            outputLine = lSourceLines[0] + "," +
                            "(Output) Local Azimuth Radius" + "," +
                            "Result";

            STW.WriteLine(outputLine);
            try { 
            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                ResetExData();
                sourceLineValues = lSourceLines[i].Split(',');

                //Create new Geographic Calculation if necessary
                CreateGeographicCalculations();

                DNSMcVector3D basePoint = new DNSMcVector3D();
                basePoint.x = double.Parse(sourceLineValues[3]);
                basePoint.y = double.Parse(sourceLineValues[4]);
                basePoint.z = double.Parse(sourceLineValues[5]);

                double azimuth = double.Parse(sourceLineValues[6]);

                double LocalAzimuthRadius = 0;
                try
                {
                    GeographicCalculations.CalcLocalAzimuthRadius(basePoint,
                                                                    azimuth,
                                                                    out LocalAzimuthRadius);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("CalcLocalAzimuthRadius - Line: " + (i + 1).ToString(), McEx);
                    SetExData(McEx);
                }

                outputLine = lSourceLines[i] + "," +
                                LocalAzimuthRadius.ToString() + "," +
                                m_msgEx;

                STW.WriteLine(outputLine);
            }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "CalcLocalAzimuthRadius");
            }
            CloseStreams();
        }

   
        private void btnCalcSunDirection_Click(object sender, EventArgs e)
        {
            CalcSunDirection();
            EndGeoCalcAction("Calc Sun Direction");
        }

        private void CalcSunDirection(string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return;

            //Print header line
            outputLine = lSourceLines[0] + "," +
                            "(Output) Sun Azimuth" + "," +
                            "Sun Elevation" + "," +
                            "Result";

            STW.WriteLine(outputLine);
            try { 
            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                ResetExData();
                sourceLineValues = lSourceLines[i].Split(',');

                //Create new Geographic Calculation if necessary
                CreateGeographicCalculations();

                int year = int.Parse(sourceLineValues[3]);
                int month = int.Parse(sourceLineValues[4]);
                int day = int.Parse(sourceLineValues[5]);
                int hour = int.Parse(sourceLineValues[6]);
                int min = int.Parse(sourceLineValues[7]);
                int sec = int.Parse(sourceLineValues[8]);
                float timeZone = float.Parse(sourceLineValues[9]);

                DNSMcVector3D sunLocation = new DNSMcVector3D();
                sunLocation.x = double.Parse(sourceLineValues[10]);
                sunLocation.y = double.Parse(sourceLineValues[11]);
                sunLocation.z = double.Parse(sourceLineValues[12]);

                DNMcNullableOut<double> sunAzimuth = new DNMcNullableOut<double>();
                DNMcNullableOut<double> sunElevation = new DNMcNullableOut<double>();
                try
                {
                    GeographicCalculations.CalcSunDirection(year,
                                                                month,
                                                                day,
                                                                hour,
                                                                min,
                                                                sec,
                                                                timeZone,
                                                                sunLocation,
                                                                sunAzimuth,
                                                                sunElevation);

                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("CalcSunDirection - Line: " + (i + 1).ToString(), McEx);
                    SetExData(McEx);
                }


                outputLine = lSourceLines[i] + "," +
                                sunAzimuth.Value.ToString() + "," +
                                sunElevation.Value.ToString() + "," +
                                m_msgEx;

                STW.WriteLine(outputLine);
            }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "CalcSunDirection");
            }
            CloseStreams();
        }
    
        private void btnCalcRectangleFromCenterAndLengths_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            CalcRectangleFromCenterAndLengths("", dlgResult);
            EndGeoCalcAction("Calc Rectangle From Center And Lengths");
        }

        private void CalcRectangleFromCenterAndLengths(string filePath = "", DialogResult dlgResult = DialogResult.No)
        {
            if (!GetSourceLines(filePath))
                return;

            //Print result header line
            outputLine = "(Output) Left Up (X)" + "," +
                            "Left Up (Y)" + "," +
                            "Left Up (Z)" + "," +
                            "Right Up (X)" + "," +
                            "Right Up (Y)" + "," +
                            "Right Up (Z)" + "," +
                            "Right Down (X)" + "," +
                            "Right Down (Y)" + "," +
                            "Right Down (Z)" + "," +
                            "Left Down (X)" + "," +
                            "Left Down (Y)" + "," +
                            "Left Down (Z)" + "," +
                            "Result";

            STW.WriteLine(outputLine);
            try
            {

                //Call to the relevant method and print the result
                for (int i = 1; i < lSourceLines.Count; i++)
                {
                    ResetExData();
                    sourceLineValues = lSourceLines[i].Split(',');

                    //Create new Geographic Calculation if necessary
                    CreateGeographicCalculations();


                    DNSMcVector3D rectangleCenterPoint = new DNSMcVector3D();
                    rectangleCenterPoint.x = double.Parse(sourceLineValues[3]);
                    rectangleCenterPoint.y = double.Parse(sourceLineValues[4]);
                    rectangleCenterPoint.z = double.Parse(sourceLineValues[5]);

                    double rectangletHeight = double.Parse(sourceLineValues[6]);
                    double rectangleWidth = double.Parse(sourceLineValues[7]);
                    double rotationAzimutDeg = double.Parse(sourceLineValues[8]);
                    bool bIsGeometric = bool.Parse(sourceLineValues[9]);


                    DNSMcVector3D leftUp = new DNSMcVector3D();
                    DNSMcVector3D rightUp = new DNSMcVector3D();
                    DNSMcVector3D rightDown = new DNSMcVector3D();
                    DNSMcVector3D leftDown = new DNSMcVector3D();
                    try
                    {
                        GeographicCalculations.CalcRectangleFromCenterAndLengths(rectangleCenterPoint,
                                                                                    rectangletHeight,
                                                                                    rectangleWidth,
                                                                                    rotationAzimutDeg,
                                                                                    out leftUp,
                                                                                    out rightUp,
                                                                                    out rightDown,
                                                                                    out leftDown,
                                                                                    bIsGeometric);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("CalcRectangleFromCenterAndLengths - Line: " + (i + 1).ToString(), McEx);
                        SetExData(McEx);
                    }

                    //Draw the Geograhic Rectangle Polygons if demanded by the user
                    if (dlgResult == DialogResult.Yes)
                    {
                        DNSMcVector3D[] resultRectPoints = new DNSMcVector3D[4];
                        resultRectPoints[0] = leftUp;
                        resultRectPoints[1] = rightUp;
                        resultRectPoints[2] = rightDown;
                        resultRectPoints[3] = leftDown;

                        DrawActionResult(false, true, resultRectPoints);
                    }

                    outputLine = leftUp.x.ToString() + "," +
                                    leftUp.y.ToString() + "," +
                                    leftUp.z.ToString() + "," +
                                    rightUp.x.ToString() + "," +
                                    rightUp.y.ToString() + "," +
                                    rightUp.z.ToString() + "," +
                                    rightDown.x.ToString() + "," +
                                    rightDown.y.ToString() + "," +
                                    rightDown.z.ToString() + "," +
                                    leftDown.x.ToString() + "," +
                                    leftDown.y.ToString() + "," +
                                    leftDown.z.ToString() + "," +
                                    m_msgEx;

                    STW.WriteLine(outputLine);
                    outputLine = "";
                }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "CalcRectangleFromCenterAndLengths");
            }

            CloseStreams();
        }

        private void btnShortestDistPointArc_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            ShortestDistPointArc("", dlgResult);
            EndGeoCalcAction("Shortest Dist Point Arc");
        }

        private void ShortestDistPointArc(string filePath = "", DialogResult dlgResult = DialogResult.No)
        {
            if (!GetSourceLines(filePath))
                return;

            //Print result header line
            outputLine = "(Output) Nearest Point (x)" + "," +
                            "Nearest Point (y)" + "," +
                            "Nearest Point (z)" + "," +
                            "Distance" + "," +
                            "Result";

            STW.WriteLine(outputLine);
            try
            {
                //Call to the relevant method and print the result
                for (int i = 1; i < lSourceLines.Count; i++)
                {
                    ResetExData();
                    sourceLineValues = lSourceLines[i].Split(',');


                    //Create new Geographic Calculation if necessary
                    CreateGeographicCalculations();

                    DNSMcVector3D point = new DNSMcVector3D();
                    point.x = double.Parse(sourceLineValues[3]);
                    point.y = double.Parse(sourceLineValues[4]);
                    point.z = double.Parse(sourceLineValues[5]);

                    DNSEllipseArc ellipse = new DNSEllipseArc();

                    DNSMcVector3D center = new DNSMcVector3D();
                    center.x = double.Parse(sourceLineValues[6]);
                    center.y = double.Parse(sourceLineValues[7]);
                    center.z = double.Parse(sourceLineValues[8]);
                    ellipse.Center = center;

                    ellipse.dRadiusX = double.Parse(sourceLineValues[9]);
                    ellipse.dRadiusY = double.Parse(sourceLineValues[10]);
                    ellipse.dRotationAngle = double.Parse(sourceLineValues[11]);
                    ellipse.dInnerRadiusFactor = double.Parse(sourceLineValues[12]);
                    ellipse.bClockWise = bool.Parse(sourceLineValues[13]);
                    ellipse.dStartAzimuth = double.Parse(sourceLineValues[14]);
                    ellipse.dEndAzimuth = double.Parse(sourceLineValues[15]);


                    DNMcNullableOut<DNSMcVector3D> nearestPoint = new DNMcNullableOut<DNSMcVector3D>();
                    DNMcNullableOut<double> dist = new DNMcNullableOut<double>();
                    try
                    {
                        GeographicCalculations.ShortestDistPointArc(point,
                                                                        ellipse,
                                                                        nearestPoint,
                                                                        dist);

                        // draw result if demanded by the user
                        if (dlgResult == DialogResult.Yes)
                        {
                            // draw original ellipse
                            float startAngle, endAngle;
                            if (ellipse.bClockWise == true)
                            {
                                startAngle = (float)ellipse.dStartAzimuth;
                                endAngle = (float)ellipse.dEndAzimuth;
                            }
                            else
                            {
                                startAngle = (float)ellipse.dEndAzimuth;
                                endAngle = (float)ellipse.dStartAzimuth;
                            }

                            IDNMcObjectSchemeItem ObjSchemeItem = DNMcEllipseItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                            DNEMcPointCoordSystem._EPCS_WORLD,
                                                                                            DNEItemGeometryType._EGT_GEOMETRIC_IN_VIEWPORT,
                                                                                            (float)ellipse.dRadiusX,
                                                                                            (float)ellipse.dRadiusY,
                                                                                            startAngle,
                                                                                            endAngle,
                                                                                            (float)ellipse.dInnerRadiusFactor,
                                                                                            DNELineStyle._ELS_SOLID,
                                                                                            new DNSMcBColor(255, 0, 0, 255),
                                                                                            2,
                                                                                            null,
                                                                                            new DNSMcFVector2D(1, 1),
                                                                                            1,
                                                                                            DNEFillStyle._EFS_NONE);



                            IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;
                            DNSMcVector3D[] locationPoint = new DNSMcVector3D[1];
                            locationPoint[0] = ellipse.Center;

                            IDNMcObject obj = DNMcObject.Create(activeOverlay,
                                                                ObjSchemeItem,
                                                                DNEMcPointCoordSystem._EPCS_WORLD,
                                                                locationPoint,
                                                                false);

                            // draw result
                            IDNMcObjectSchemeItem LineItem = DNMcLineItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                    DNELineStyle._ELS_DASH,
                                                                                    new DNSMcBColor(0, 0, 255, 255),
                                                                                    2);

                            DNSMcVector3D[] LineLocationPoint = new DNSMcVector3D[2];
                            LineLocationPoint[0] = point;
                            LineLocationPoint[1] = nearestPoint.Value;

                            IDNMcObject obj2 = DNMcObject.Create(activeOverlay,
                                                                LineItem,
                                                                DNEMcPointCoordSystem._EPCS_WORLD,
                                                                LineLocationPoint,
                                                                false);

                            FontDialog Fd = new FontDialog();
                            DNSMcLogFont logFont = new DNSMcLogFont();
                            Fd.Font.ToLogFont(logFont);
                            IDNMcLogFont DefaultFont = DNMcLogFont.Create(new DNMcVariantLogFont(logFont, false));

                            IDNMcTextItem TextItem = DNMcTextItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN, DNEMcPointCoordSystem._EPCS_SCREEN,
                                                                                    DefaultFont);

                            TextItem.Connect(LineItem);
                            TextItem.SetAttachPointType(0, DNEAttachPointType._EAPT_MID_POINT, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID);
                            TextItem.SetText(new DNMcVariantString(dist.Value.ToString(), false), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        }
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("CalcRectangleFromCenterAndLengths - Line:" + (i + 1).ToString(), McEx);
                        SetExData(McEx);
                    }

                    outputLine = nearestPoint.Value.x.ToString() + "," +
                                        nearestPoint.Value.y.ToString() + "," +
                                        nearestPoint.Value.z.ToString() + "," +
                                        dist.Value.ToString() + "," +
                                        m_msgEx;

                    STW.WriteLine(outputLine);
                    outputLine = "";
                }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "ShortestDistPointArc");
            }
            CloseStreams();
        }

        private void btnShortestDistPoint2DLine_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            ShortestDistPoint2DLine("", dlgResult);
            EndGeoCalcAction("Shortest Dist Point 2D Line");
        }

        private void ShortestDistPoint2DLine(string filePath = "", DialogResult dlgResult = DialogResult.No)
        {
            if (!GetSourceLines(filePath))
                return;

            //Print result header line
            outputLine = "(Output) Nearest Point (x)" + "," +
                            "Nearest Point (y)" + "," +
                            "Nearest Point (z)" + "," +
                            "Distance" + "," +
                            "Result";

            STW.WriteLine(outputLine);
            try
            {
                //Call to the relevant method and print the result
                for (int i = 1; i < lSourceLines.Count; i++)
                {
                    ResetExData();
                    sourceLineValues = lSourceLines[i].Split(',');


                    //Create new Geographic Calculation if necessary
                    CreateGeographicCalculations();


                    DNSMcVector3D point = new DNSMcVector3D();
                    point.x = double.Parse(sourceLineValues[3]);
                    point.y = double.Parse(sourceLineValues[4]);
                    point.z = double.Parse(sourceLineValues[5]);

                    DNSMcVector3D[] polylineVertices = new DNSMcVector3D[(sourceLineValues.Length - 6) / 3];
                    int idx = 0;
                    for (int cell = 6; cell < sourceLineValues.Length; cell += 3)
                    {
                        polylineVertices[idx].x = double.Parse(sourceLineValues[cell]);
                        polylineVertices[idx].y = double.Parse(sourceLineValues[cell + 1]);
                        polylineVertices[idx].z = double.Parse(sourceLineValues[cell + 2]);

                        idx++;
                    }

                    DNMcNullableOut<DNSMcVector3D> nearestPoint = new DNMcNullableOut<DNSMcVector3D>();
                    DNMcNullableOut<double> dist = new DNMcNullableOut<double>();
                    try
                    {
                        GeographicCalculations.ShortestDistPoint2DLine(point,
                                                                        polylineVertices,
                                                                        nearestPoint,
                                                                        dist);

                        // draw result if demanded by the user
                        if (dlgResult == DialogResult.Yes)
                        {
                            IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;

                            IDNMcObjectSchemeItem srcLine = DNMcLineItem.Create(DNEItemSubTypeFlags._EISTF_WORLD,
                                                                                        DNELineStyle._ELS_SOLID,
                                                                                        new DNSMcBColor(255, 0, 0, 255),
                                                                                        2);



                            DNSMcVector3D[] locationPoint = polylineVertices;


                            IDNMcObject obj = DNMcObject.Create(activeOverlay,
                                                                srcLine,
                                                                DNEMcPointCoordSystem._EPCS_WORLD,
                                                                locationPoint,
                                                                false);

                            // draw result
                            IDNMcObjectSchemeItem resLine = DNMcLineItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                    DNELineStyle._ELS_DASH,
                                                                                    new DNSMcBColor(0, 0, 255, 255),
                                                                                    2);

                            DNSMcVector3D[] LineLocationPoint = new DNSMcVector3D[2];
                            LineLocationPoint[0] = point;
                            LineLocationPoint[1] = nearestPoint.Value;

                            IDNMcObject obj2 = DNMcObject.Create(activeOverlay,
                                                                resLine,
                                                                DNEMcPointCoordSystem._EPCS_WORLD,
                                                                LineLocationPoint,
                                                                false);

                            FontDialog Fd = new FontDialog();
                            DNSMcLogFont logFont = new DNSMcLogFont();
                            Fd.Font.ToLogFont(logFont);
                            IDNMcLogFont DefaultFont = DNMcLogFont.Create(new DNMcVariantLogFont(logFont, false));

                            IDNMcTextItem TextItem = DNMcTextItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN, DNEMcPointCoordSystem._EPCS_SCREEN,
                                                                                    DefaultFont);

                            TextItem.Connect(resLine);
                            TextItem.SetAttachPointType(0, DNEAttachPointType._EAPT_MID_POINT, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID);
                            TextItem.SetText(new DNMcVariantString(dist.Value.ToString(), false), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        }
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("ShortestDistPoint2DLine - Line:" + (i + 1).ToString(), McEx);
                        SetExData(McEx);
                    }

                    outputLine = nearestPoint.Value.x.ToString() + "," +
                                        nearestPoint.Value.y.ToString() + "," +
                                        nearestPoint.Value.z.ToString() + "," +
                                        dist.Value.ToString() + "," +
                                        m_msgEx;

                    STW.WriteLine(outputLine);
                    outputLine = "";
                }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "ShortestDistPoint2DLine");
            }
            CloseStreams();
        }

        private void btnIsPointOn2DLine_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            IsPointOn2DLine("", dlgResult);
            EndGeoCalcAction("Is Point On 2D Line");

        }

        private void IsPointOn2DLine(string filePath = "", DialogResult dlgResult = DialogResult.No)
        {
            if (!GetSourceLines(filePath))
                return;

            //Print result header line
            outputLine = "(Output) Is Point On 2D Line" + "," + "Result";

            STW.WriteLine(outputLine);
            try
            {
                //Call to the relevant method and print the result
                for (int i = 1; i < lSourceLines.Count; i++)
                {
                    ResetExData();
                    sourceLineValues = lSourceLines[i].Split(',');


                    //Create new Geographic Calculation if necessary
                    CreateGeographicCalculations();


                    DNSMcVector3D point = new DNSMcVector3D();
                    point.x = double.Parse(sourceLineValues[3]);
                    point.y = double.Parse(sourceLineValues[4]);
                    point.z = double.Parse(sourceLineValues[5]);

                    short lineAccuracy = short.Parse(sourceLineValues[6]);

                    DNSMcVector3D[] polylineVertices = new DNSMcVector3D[(sourceLineValues.Length - 7) / 3];
                    int idx = 0;
                    for (int cell = 7; cell < sourceLineValues.Length; cell += 3)
                    {
                        polylineVertices[idx].x = double.Parse(sourceLineValues[cell]);
                        polylineVertices[idx].y = double.Parse(sourceLineValues[cell + 1]);
                        polylineVertices[idx].z = double.Parse(sourceLineValues[cell + 2]);

                        idx++;
                    }

                    bool isPointOn2DLine = false;
                    try
                    {
                        isPointOn2DLine = GeographicCalculations.IsPointOn2DLine(point,
                                                                                    polylineVertices,
                                                                                    lineAccuracy);

                        // draw result if demanded by the user
                        if (dlgResult == DialogResult.Yes)
                        {
                            IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;

                            IDNMcObjectSchemeItem srcLine = DNMcLineItem.Create(DNEItemSubTypeFlags._EISTF_WORLD,
                                                                                        DNELineStyle._ELS_SOLID,
                                                                                        new DNSMcBColor(255, 0, 0, 255),
                                                                                        2);



                            DNSMcVector3D[] locationPoint = polylineVertices;


                            IDNMcObject obj = DNMcObject.Create(activeOverlay,
                                                                srcLine,
                                                                DNEMcPointCoordSystem._EPCS_WORLD,
                                                                locationPoint,
                                                                false);


                            FontDialog Fd = new FontDialog();
                            DNSMcLogFont logFont = new DNSMcLogFont();
                            Fd.Font.ToLogFont(logFont);
                            IDNMcLogFont DefaultFont = DNMcLogFont.Create(new DNMcVariantLogFont(logFont, false));

                            IDNMcTextItem TextItem = DNMcTextItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN, DNEMcPointCoordSystem._EPCS_SCREEN,
                                                                                    DefaultFont);

                            TextItem.SetText(new DNMcVariantString("Point" + i.ToString(), false), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                            TextItem.SetTextColor(new DNSMcBColor(0, 0, 255, 255), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                            TextItem.SetRectAlignment(DNEBoundingRectanglePoint._EBRP_CENTER, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                            DNSMcVector3D[] LineLocationPoint = new DNSMcVector3D[1];
                            LineLocationPoint[0] = point;

                            IDNMcObject obj2 = DNMcObject.Create(activeOverlay,
                                                                    TextItem,
                                                                    DNEMcPointCoordSystem._EPCS_WORLD,
                                                                    LineLocationPoint,
                                                                    false);
                        }
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("IsPointOn2DLine - Line: " + (i + 1).ToString(), McEx);
                        SetExData(McEx);
                    }
                    outputLine = isPointOn2DLine.ToString() + "," +
                                    m_msgEx;

                    STW.WriteLine(outputLine);
                    outputLine = "";
                }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "IsPointOn2DLine");
            }
            CloseStreams();
        }

        private void btnConvertAzimuthFromOtherCoordSys_Click(object sender, EventArgs e)
        {
            ConvertAzimuthFromOtherCoordSys();
            EndGeoCalcAction("Convert Azimuth From Other CoordSys");
        }

        private void ConvertAzimuthFromOtherCoordSys(string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return;

            //Print header line
            outputLine = lSourceLines[0] + "," +
                            "(Output) This Coordinate System Azimuth" + "," +
                            "Result";

            STW.WriteLine(outputLine);
            try { 
            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                ResetExData();
                sourceLineValues = lSourceLines[i].Split(',');

                //Create new Geographic Calculation if necessary
                CreateGeographicCalculations();

                DNSMcVector3D originalLocation = new DNSMcVector3D();
                originalLocation.x = double.Parse(sourceLineValues[3]);
                originalLocation.y = double.Parse(sourceLineValues[4]);
                originalLocation.z = double.Parse(sourceLineValues[5]);

                bool isLocationInOtherCoordSys = bool.Parse(sourceLineValues[6]);

                DNEGridCoordSystemType gridCoordType = (DNEGridCoordSystemType)Enum.Parse(typeof(DNEGridCoordSystemType), sourceLineValues[7]);
                DNEDatumType datumType = (DNEDatumType)Enum.Parse(typeof(DNEDatumType), sourceLineValues[8]);
                int zone = 0;
                if (gridCoordType == DNEGridCoordSystemType._EGCS_GENERIC_GRID)
                {
                    zone = 0;
                }
                else
                {
                    zone = int.Parse(sourceLineValues[9]);
                }
                string srid = sourceLineValues[9];
                IDNMcGridCoordinateSystem otherCoordSys = CreateGridCoordSys(gridCoordType, datumType, zone, srid);

                double otherCoordSysAzimuth = double.Parse(sourceLineValues[10]);

                double azimuth = 0;
                try
                {
                    azimuth = GeographicCalculations.ConvertAzimuthFromOtherCoordSys(originalLocation,
                                                                                        isLocationInOtherCoordSys,
                                                                                        otherCoordSys,
                                                                                        otherCoordSysAzimuth);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("ConvertAzimuthFromOtherCoordSys - Line: " + (i + 1).ToString(), McEx);
                    SetExData(McEx);
                }

                outputLine = lSourceLines[i] + "," +
                                azimuth.ToString() + "," +
                                m_msgEx;

                STW.WriteLine(outputLine);
            }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "ConvertAzimuthFromOtherCoordSys");

            }
            CloseStreams();
        }

        private void btnConvertAzimuthToOtherCoordSys_Click(object sender, EventArgs e)
        {
            ConvertAzimuthToOtherCoordSys();
            EndGeoCalcAction("Convert Azimuth To Other Coord Sys");
        }

        private void ConvertAzimuthToOtherCoordSys(string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return;

            //Print header line
            outputLine = lSourceLines[0] + "," +
                            "(Output) Other Coordinate System Azimuth" + "," +
                            "Result";

            STW.WriteLine(outputLine);
            try
            {
                //Call to the relevant method and print the result
                for (int i = 1; i < lSourceLines.Count; i++)
                {
                    ResetExData();
                    sourceLineValues = lSourceLines[i].Split(',');

                    //Create new Geographic Calculation if necessary
                    CreateGeographicCalculations();

                    DNSMcVector3D originalLocation = new DNSMcVector3D();
                    originalLocation.x = double.Parse(sourceLineValues[3]);
                    originalLocation.y = double.Parse(sourceLineValues[4]);
                    originalLocation.z = double.Parse(sourceLineValues[5]);

                    bool isLocationInOtherCoordSys = bool.Parse(sourceLineValues[6]);

                    DNEGridCoordSystemType gridCoordType = (DNEGridCoordSystemType)Enum.Parse(typeof(DNEGridCoordSystemType), sourceLineValues[7]);
                    DNEDatumType datumType = (DNEDatumType)Enum.Parse(typeof(DNEDatumType), sourceLineValues[8]);
                    int zone = 0;
                    if (gridCoordType == DNEGridCoordSystemType._EGCS_GENERIC_GRID)
                    {
                        zone = 0;
                    }
                    else
                    {
                        zone = int.Parse(sourceLineValues[9]);
                    }
                    string srid = sourceLineValues[9];
                    IDNMcGridCoordinateSystem otherCoordSys = CreateGridCoordSys(gridCoordType, datumType, zone, srid);

                    double thisCoordSysAzimuth = double.Parse(sourceLineValues[10]);

                    double otherCoordSysAzimuth = 0;
                    try
                    {
                        otherCoordSysAzimuth = GeographicCalculations.ConvertAzimuthToOtherCoordSys(originalLocation,
                                                                                                        isLocationInOtherCoordSys,
                                                                                                        otherCoordSys,
                                                                                                        thisCoordSysAzimuth);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("ConvertAzimuthToOtherCoordSys - Line: " + (i + 1).ToString(), McEx);
                        SetExData(McEx);
                    }

                    outputLine = lSourceLines[i] + "," +
                                    otherCoordSysAzimuth.ToString() + "," +
                                    m_msgEx;

                    STW.WriteLine(outputLine);
                }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "ConvertAzimuthToOtherCoordSys");
            }
            CloseStreams();
        }

        private void btnConvertAzimuthFromGridToGeo_Click(object sender, EventArgs e)
        {
            ConvertAzimuthFromGridToGeo();
            EndGeoCalcAction("Convert Azimuth From Grid To Geo");
        }

        private void ConvertAzimuthFromGridToGeo(string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return;

            //Print header line
            outputLine = lSourceLines[0] + "," +
                            "(Output) Geo Azimuth" + "," +
                            "Result";

            STW.WriteLine(outputLine);
            try
            {
                //Call to the relevant method and print the result
                for (int i = 1; i < lSourceLines.Count; i++)
                {
                    ResetExData();
                    sourceLineValues = lSourceLines[i].Split(',');

                    //Create new Geographic Calculation if necessary
                    CreateGeographicCalculations();

                    bool isOriginalLocationInGeoCoordinates = bool.Parse(sourceLineValues[3]);

                    DNSMcVector3D originalLocation = new DNSMcVector3D();
                    originalLocation.x = double.Parse(sourceLineValues[4]);
                    originalLocation.y = double.Parse(sourceLineValues[5]);
                    originalLocation.z = double.Parse(sourceLineValues[6]);

                    double gridAzimuth = double.Parse(sourceLineValues[7]);

                    double geoAzimuth = 0;
                    try
                    {
                        geoAzimuth = GeographicCalculations.ConvertAzimuthFromGridToGeo(originalLocation,
                                                                                        gridAzimuth,
                                                                                        isOriginalLocationInGeoCoordinates);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("ConvertAzimuthFromGridToGeo - Line: " + (i + 1).ToString(), McEx);
                        SetExData(McEx);
                    }

                    outputLine = lSourceLines[i] + "," +
                                    geoAzimuth.ToString() + "," +
                                    m_msgEx;

                    STW.WriteLine(outputLine);
                }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "ConvertAzimuthFromGridToGeo");
            }
            CloseStreams();
        }

        private void btnConvertAzimuthFromGeoToGrid_Click(object sender, EventArgs e)
        {
            ConvertAzimuthFromGeoToGrid();
            EndGeoCalcAction("Convert Azimuth From Geo To Grid");
        }

        private bool ConvertAzimuthFromGeoToGrid(string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return false;

            //Print header line
            outputLine = lSourceLines[0] + "," +
                            "(Output) Grid Azimuth" + "," +
                            "Result";

            STW.WriteLine(outputLine);
            try { 
            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                ResetExData();
                sourceLineValues = lSourceLines[i].Split(',');

                //Create new Geographic Calculation if necessary
                CreateGeographicCalculations();

                bool isOriginalLocationInGeoCoordinates = bool.Parse(sourceLineValues[3]);

                DNSMcVector3D originalLocation = new DNSMcVector3D();
                originalLocation.x = double.Parse(sourceLineValues[4]);
                originalLocation.y = double.Parse(sourceLineValues[5]);
                originalLocation.z = double.Parse(sourceLineValues[6]);

                double geoAzimuth = double.Parse(sourceLineValues[7]);

                double gridAzimuth = 0;
                try
                {
                    gridAzimuth = GeographicCalculations.ConvertAzimuthFromGeoToGrid(originalLocation,
                                                                                                geoAzimuth,
                                                                                                isOriginalLocationInGeoCoordinates);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("ConvertAzimuthFromGeoToGrid - Line: " + (i + 1).ToString(), McEx);
                    SetExData(McEx);
                }


                outputLine = lSourceLines[i] + "," +
                                gridAzimuth.ToString() + "," +
                                m_msgEx;


                STW.WriteLine(outputLine);
            }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "ConvertAzimuthFromGeoToGrid");
                CloseStreams();
                return false;
            }
            CloseStreams();
            return true;
        }

        private void btnIsPointInArc_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            IsPointInArc("", dlgResult);
            EndGeoCalcAction("Is Point In Arc");
        }

        private void IsPointInArc(string filePath = "", DialogResult dlgResult = DialogResult.No)
        {
            if (!GetSourceLines(filePath))
                return;

            //Print result header line
            outputLine = "(Output) Is Point In Arc" + "," +
                            "Result";
            STW.WriteLine(outputLine);
            try
            {
                //Call to the relevant method and print the result
                for (int i = 1; i < lSourceLines.Count; i++)
                {
                    ResetExData();
                    sourceLineValues = lSourceLines[i].Split(',');


                    //Create new Geographic Calculation if necessary
                    CreateGeographicCalculations();

                    DNSMcVector3D point = new DNSMcVector3D();
                    point.x = double.Parse(sourceLineValues[3]);
                    point.y = double.Parse(sourceLineValues[4]);
                    point.z = double.Parse(sourceLineValues[5]);

                    DNSMcVector3D center = new DNSMcVector3D();
                    center.x = double.Parse(sourceLineValues[6]);
                    center.y = double.Parse(sourceLineValues[7]);
                    center.z = double.Parse(sourceLineValues[8]);

                    DNSEllipseArc ellipseArc = new DNSEllipseArc();
                    ellipseArc.Center = center;
                    ellipseArc.dRadiusX = double.Parse(sourceLineValues[9]);
                    ellipseArc.dRadiusY = double.Parse(sourceLineValues[10]);
                    ellipseArc.dRotationAngle = double.Parse(sourceLineValues[11]);
                    ellipseArc.dInnerRadiusFactor = double.Parse(sourceLineValues[12]);
                    ellipseArc.bClockWise = bool.Parse(sourceLineValues[13]);
                    ellipseArc.dStartAzimuth = double.Parse(sourceLineValues[14]);
                    ellipseArc.dEndAzimuth = double.Parse(sourceLineValues[15]);


                    bool isPointInArc = false;
                    try
                    {
                        isPointInArc = GeographicCalculations.IsPointInArc(point,
                                                                            ellipseArc);

                        // draw result if demanded by the user
                        if (dlgResult == DialogResult.Yes)
                        {
                            IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;

                            // draw original ellipse
                            float startAngle, endAngle;
                            if (ellipseArc.bClockWise == true)
                            {
                                startAngle = (float)ellipseArc.dStartAzimuth;
                                endAngle = (float)ellipseArc.dEndAzimuth;
                            }
                            else
                            {
                                startAngle = (float)ellipseArc.dEndAzimuth;
                                endAngle = (float)ellipseArc.dStartAzimuth;
                            }


                            IDNMcObjectSchemeItem srcEllipse = DNMcEllipseItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                        DNEMcPointCoordSystem._EPCS_WORLD,
                                                                                        DNEItemGeometryType._EGT_GEOGRAPHIC,
                                                                                        (float)ellipseArc.dRadiusX,
                                                                                        (float)ellipseArc.dRadiusY,
                                                                                        startAngle,
                                                                                        endAngle,
                                                                                        (float)ellipseArc.dInnerRadiusFactor,
                                                                                        DNELineStyle._ELS_SOLID,
                                                                                        new DNSMcBColor(255, 0, 0, 255),
                                                                                        2,
                                                                                        null,
                                                                                        new DNSMcFVector2D(1, 1),
                                                                                        1,
                                                                                        DNEFillStyle._EFS_NONE);

                            // Set the ellipse rotation
                            ((IDNMcSymbolicItem)srcEllipse).SetRotationYaw((float)ellipseArc.dRotationAngle, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);


                            DNSMcVector3D[] locationPoint = new DNSMcVector3D[] { center };

                            IDNMcObject obj = DNMcObject.Create(activeOverlay,
                                                                srcEllipse,
                                                                DNEMcPointCoordSystem._EPCS_WORLD,
                                                                locationPoint,
                                                                false);


                            FontDialog Fd = new FontDialog();
                            DNSMcLogFont logFont = new DNSMcLogFont();
                            Fd.Font.ToLogFont(logFont);
                            IDNMcLogFont DefaultFont = DNMcLogFont.Create(new DNMcVariantLogFont(logFont, false));

                            IDNMcTextItem TextItem = DNMcTextItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN, DNEMcPointCoordSystem._EPCS_SCREEN,
                                                                                    DefaultFont);

                            TextItem.SetText(new DNMcVariantString("Point " + i.ToString() + ", " + isPointInArc.ToString(), false), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                            TextItem.SetTextColor(new DNSMcBColor(0, 255, 0, 255), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                            TextItem.SetRectAlignment(DNEBoundingRectanglePoint._EBRP_TOP_LEFT, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                            TextItem.SetBackgroundColor(new DNSMcBColor(0, 0, 255, 125), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                            TextItem.SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);



                            DNSMcVector3D[] textLocationPoint = new DNSMcVector3D[] { point };

                            IDNMcObject obj2 = DNMcObject.Create(activeOverlay,
                                                                    TextItem,
                                                                    DNEMcPointCoordSystem._EPCS_WORLD,
                                                                    textLocationPoint,
                                                                    false);

                            IDNMcObjectSchemeItem ellipseItem = DNMcEllipseItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                            DNEMcPointCoordSystem._EPCS_WORLD,
                                                                                            DNEItemGeometryType._EGT_GEOMETRIC_IN_OVERLAY_MANAGER,
                                                                                            1,
                                                                                            1,
                                                                                            0,
                                                                                            360,
                                                                                            0,
                                                                                            DNELineStyle._ELS_SOLID,
                                                                                            new DNSMcBColor(255, 0, 0, 100),
                                                                                            1,
                                                                                            null,
                                                                                            new DNSMcFVector2D(1, 1),
                                                                                            1,
                                                                                            DNEFillStyle._EFS_SOLID,
                                                                                            new DNSMcBColor(255, 0, 0, 100));


                            DNSMcVector3D[] ellipseLocationPoint = new DNSMcVector3D[] { point };

                            IDNMcObject ellipseObj = DNMcObject.Create(activeOverlay,
                                                                        ellipseItem,
                                                                        DNEMcPointCoordSystem._EPCS_WORLD,
                                                                        ellipseLocationPoint,
                                                                        false);
                        }
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("IsPointInArc - Line: " + (i + 1).ToString(), McEx);
                        SetExData(McEx);
                    }

                    outputLine = isPointInArc.ToString() + "," +
                                    m_msgEx;

                    STW.WriteLine(outputLine);
                    outputLine = "";
                }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "IsPointInArc");
               
            }
            CloseStreams();
        }

        private void btnSmallestBoundingRect_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            SmallestBoundingRect("", dlgResult);
            EndGeoCalcAction("PolyLine Length");
        }

        private void SmallestBoundingRect(string filePath = "", DialogResult dlgResult = DialogResult.No)
        {
            if (!GetSourceLines(filePath))
                return;

            //Print header line
            STW.WriteLine("Center Point X, Center Point Y, Center Point Z, Azimuth, Length, Width, Area, Result");
            try
            {
                //Call to the relevant method and print the result
                for (int i = 1; i < lSourceLines.Count; i++)
                {
                    ResetExData();
                    sourceLineValues = lSourceLines[i].Split(',');


                    //Create new Geographic Calculation if necessary
                    CreateGeographicCalculations();

                    double DeltaAngleToCheck = double.Parse(sourceLineValues[3]);

                    List<DNSMcVector3D> lPoints = new List<DNSMcVector3D>();
                    DNSMcVector3D vertice = new DNSMcVector3D();

                    //Collect the polyline vertices to a DNSMcVector3D list
                    for (int idx = 4; idx < sourceLineValues.Length; idx += 3)
                    {
                        vertice.x = double.Parse(sourceLineValues[idx]);
                        vertice.y = double.Parse(sourceLineValues[idx + 1]);
                        vertice.z = double.Parse(sourceLineValues[idx + 2]);

                        lPoints.Add(vertice);
                    }

                    if (dlgResult == DialogResult.Yes)
                        DrawActionResult(false, true, lPoints.ToArray());

                    outputLine = "(Output) ";
                    DNMcNullableOut<DNSMcVector3D> centerPoint = new DNMcNullableOut<DNSMcVector3D>();
                    DNMcNullableOut<double> azimuth = new DNMcNullableOut<double>();
                    DNMcNullableOut<double> rectLength = new DNMcNullableOut<double>();
                    DNMcNullableOut<double> rectWidth = new DNMcNullableOut<double>();
                    DNMcNullableOut<double> rectArea = new DNMcNullableOut<double>();
                    try
                    {
                        GeographicCalculations.SmallestBoundingRect(lPoints.ToArray(),
                                                                        DeltaAngleToCheck,
                                                                        centerPoint,
                                                                        azimuth,
                                                                        rectLength,
                                                                        rectWidth,
                                                                        rectArea);

                        outputLine = centerPoint.Value.x.ToString() + "," +
                                        centerPoint.Value.y.ToString() + "," +
                                        centerPoint.Value.z.ToString() + "," +
                                        azimuth.Value.ToString() + "," +
                                        rectLength.Value.ToString() + "," +
                                        rectWidth.Value.ToString() + "," +
                                        rectArea.Value.ToString();

                        //Draw the Geographic Rectangle Polygons if demanded by the user
                        if (dlgResult == DialogResult.Yes)
                        {
                            DrawActionResult(false, true, lPoints.ToArray());

                            DNSMcVector3D leftUp = new DNSMcVector3D();
                            DNSMcVector3D rightUp = new DNSMcVector3D();
                            DNSMcVector3D rightDown = new DNSMcVector3D();
                            DNSMcVector3D leftDown = new DNSMcVector3D();
                            try
                            {
                                GeographicCalculations.CalcRectangleFromCenterAndLengths(centerPoint.Value,
                                                                                    rectLength.Value,
                                                                                    rectWidth.Value,
                                                                                    azimuth.Value,
                                                                                    out leftUp,
                                                                                    out rightUp,
                                                                                    out rightDown,
                                                                                    out leftDown);

                                DNSMcVector3D[] resultRectPoints = new DNSMcVector3D[5];
                                resultRectPoints[0] = leftUp;
                                resultRectPoints[1] = rightUp;
                                resultRectPoints[2] = rightDown;
                                resultRectPoints[3] = leftDown;
                                resultRectPoints[4] = leftUp;

                                DrawActionResult(false, false, resultRectPoints);

                            }
                            catch (MapCoreException McEx)
                            {
                                MapCore.Common.Utilities.ShowErrorMessage("CalcRectangleFromCenterAndLengths", McEx);
                            }
                        }
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("SmallestBoundingRect - Line: " + (i + 1).ToString(), McEx);
                        SetExData(McEx);
                    }
                    outputLine += "," + m_msgEx;

                    STW.WriteLine(outputLine);
                }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "SmallestBoundingRect");
            }
            CloseStreams();
        }

        private void btnBoundingRectAtAngle_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            BoundingRectAtAngle("", dlgResult);
            EndGeoCalcAction("PolyLine Length");
        }

        private void BoundingRectAtAngle(string filePath = "", DialogResult dlgResult = DialogResult.No)
        {
            if (!GetSourceLines(filePath))
                return;

            //Print header line
            STW.WriteLine("Center Point X, Center Point Y, Center Point Z, Azimuth, Length, Width, Area, Result");
            try { 
            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                ResetExData();
                sourceLineValues = lSourceLines[i].Split(',');

                //Create new Geographic Calculation if necessary
                CreateGeographicCalculations();

                double RectAngle = double.Parse(sourceLineValues[3]);

                List<DNSMcVector3D> lPoints = new List<DNSMcVector3D>();
                DNSMcVector3D vertice = new DNSMcVector3D();

                //Collect the polyline vertices to a DNSMcVector3D list
                for (int idx = 4; idx < sourceLineValues.Length; idx += 3)
                {
                    vertice.x = double.Parse(sourceLineValues[idx]);
                    vertice.y = double.Parse(sourceLineValues[idx + 1]);
                    vertice.z = double.Parse(sourceLineValues[idx + 2]);

                    lPoints.Add(vertice);
                }

                outputLine = "(Output) ";
                DNMcNullableOut<DNSMcVector3D> centerPoint = new DNMcNullableOut<DNSMcVector3D>();
                DNMcNullableOut<double> azimuth = new DNMcNullableOut<double>();
                DNMcNullableOut<double> rectLength = new DNMcNullableOut<double>();
                DNMcNullableOut<double> rectWidth = new DNMcNullableOut<double>();
                DNMcNullableOut<double> rectArea = new DNMcNullableOut<double>();
                try
                {
                    GeographicCalculations.BoundingRectAtAngle(lPoints.ToArray(),
                                                                    RectAngle,
                                                                    centerPoint,
                                                                    azimuth,
                                                                    rectLength,
                                                                    rectWidth,
                                                                    rectArea);

                    outputLine = centerPoint.Value.x.ToString() + "," +
                                    centerPoint.Value.y.ToString() + "," +
                                    centerPoint.Value.z.ToString() + "," +
                                    azimuth.Value.ToString() + "," +
                                    rectLength.Value.ToString() + "," +
                                    rectWidth.Value.ToString() + "," +
                                    rectArea.Value.ToString();

                    //Draw the Geograhic Rectangle Polygons if demanded by the user
                    if (dlgResult == DialogResult.Yes)
                    {
                        DNSMcVector3D leftUp = new DNSMcVector3D();
                        DNSMcVector3D rightUp = new DNSMcVector3D();
                        DNSMcVector3D rightDown = new DNSMcVector3D();
                        DNSMcVector3D leftDown = new DNSMcVector3D();
                        try
                        {
                            GeographicCalculations.CalcRectangleFromCenterAndLengths(centerPoint.Value,
                                                                                rectLength.Value,
                                                                                rectWidth.Value,
                                                                                azimuth.Value,
                                                                                out leftUp,
                                                                                out rightUp,
                                                                                out rightDown,
                                                                                out leftDown);

                            DNSMcVector3D[] resultRectPoints = new DNSMcVector3D[5];
                            resultRectPoints[0] = leftUp;
                            resultRectPoints[1] = rightUp;
                            resultRectPoints[2] = rightDown;
                            resultRectPoints[3] = leftDown;
                            resultRectPoints[4] = leftUp;

                            DrawActionResult(false, false, resultRectPoints);

                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("CalcRectangleFromCenterAndLengths", McEx);
                        }
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("BoundingRectAtAngle - Line: " + (i + 1).ToString(), McEx);
                    SetExData(McEx);
                }
                outputLine += "," + m_msgEx;

                STW.WriteLine(outputLine);
            }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "BoundingRectAtAngle");
            }
            CloseStreams();
        }

        private IDNMcGridCoordinateSystem CreateGridCoordSys(DNEGridCoordSystemType gridType, DNEDatumType datumType, int zone, string srid)
        {
            if (gridType == DNEGridCoordSystemType._EGCS_GENERIC_GRID)
            {
                return DNMcGridGeneric.Create(srid);
            }
            else
            {
                return CreateGridCoordSys(gridType, datumType, zone);
            }
        }

        private IDNMcGridCoordinateSystem CreateGridCoordSys(DNEGridCoordSystemType gridType, DNEDatumType datumType, int zone)
        {
            IDNMcGridCoordinateSystem coordSys = null;

            switch (gridType)
            {
                case DNEGridCoordSystemType._EGCS_GEOCENTRIC:
                    coordSys = DNMcGridCoordSystemGeocentric.Create(datumType);
                    break;
                case DNEGridCoordSystemType._EGCS_GEOGRAPHIC:
                    coordSys = DNMcGridCoordSystemGeographic.Create(datumType);
                    break;
                case DNEGridCoordSystemType._EGCS_NEW_ISRAEL:
                    coordSys = DNMcGridNewIsrael.Create();
                    break;
                case DNEGridCoordSystemType._EGCS_RSO_SINGAPORE:
                    coordSys = DNMcGridRSOSingapore.Create();
                    break;
                case DNEGridCoordSystemType._EGCS_RT90:
                    coordSys = DNMcGridRT90.Create();
                    break;
                case DNEGridCoordSystemType._EGCS_S42:
                    coordSys = DNMcGridS42.Create(zone, datumType);
                    break;
                case DNEGridCoordSystemType._EGCS_UTM:
                    coordSys = DNMcGridUTM.Create(zone, datumType);
                    break;
                case DNEGridCoordSystemType._EGCS_INDIA_LCC:
                case DNEGridCoordSystemType._EGCS_KKJ:
                case DNEGridCoordSystemType._EGCS_TM_USER_DEFINED:
                    break;
                case DNEGridCoordSystemType._EGCS_MGRS:
                    coordSys = DNMcGridMGRS.Create();
                    break;
                case DNEGridCoordSystemType._EGCS_GEOREF:
                    coordSys = DNMcGridGEOREF.Create();
                    break;
            }

            return coordSys;
        }

        private void btnConvertAtoMGRS_Click(object sender, EventArgs e)
        {
            ConvertAtoMGRS();
            EndGeoCalcAction("Convert A to MGRS");

        }

        private void ConvertAtoMGRS(string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return;

            //Print header line
            outputLine = lSourceLines[0] + "," +
                            "(Output) Location B(X)" + "," +
                            "Location B(Y)" + "," +
                            "Location B(Z)" + "," +
                            "Square Zone" + "," +
                            "Square Band" + "," +
                            "Square Fst" + "," +
                            "Square Snd" + "," +
                            "Check Grid Limits" + "," +
                            "Result";

            STW.WriteLine(outputLine);

            DNEGridCoordSystemType gridType;
            DNEDatumType datumType;
            int zone;
            IDNMcGridCoordinateSystem gridCoordSysA;
            IDNMcGridCoordinateSystem gridCoordSysB;
            DNSMcBox boxA = new DNSMcBox();
            try
            {
                //Call to the relevant method and print the result
                for (int i = 1; i < lSourceLines.Count; i++)
                {
                    ResetExData();
                    sourceLineValues = lSourceLines[i].Split(',');

                    boxA.MinVertex.x = double.Parse(sourceLineValues[0]);
                    boxA.MinVertex.y = double.Parse(sourceLineValues[1]);
                    boxA.MinVertex.z = double.Parse(sourceLineValues[2]);
                    boxA.MaxVertex.x = double.Parse(sourceLineValues[3]);
                    boxA.MaxVertex.y = double.Parse(sourceLineValues[4]);
                    boxA.MaxVertex.z = double.Parse(sourceLineValues[5]);

                    gridType = (DNEGridCoordSystemType)Enum.Parse(typeof(DNEGridCoordSystemType), sourceLineValues[6]);
                    datumType = (DNEDatumType)Enum.Parse(typeof(DNEDatumType), sourceLineValues[7]);
                    zone = int.Parse(sourceLineValues[8]);
                    gridCoordSysA = CreateGridCoordSys(gridType, datumType, zone);


                    if (boxA.MinVertex.x != 0 || boxA.MinVertex.y != 0 || boxA.MinVertex.z != 0 ||
                        boxA.MaxVertex.x != 0 || boxA.MaxVertex.y != 0 || boxA.MaxVertex.z != 0)
                    {
                        gridCoordSysA.SetLegalValuesForGeographicCoordinates(boxA);
                    }

                    gridType = (DNEGridCoordSystemType)Enum.Parse(typeof(DNEGridCoordSystemType), sourceLineValues[9]);
                    datumType = (DNEDatumType)Enum.Parse(typeof(DNEDatumType), sourceLineValues[10]);
                    zone = int.Parse(sourceLineValues[11]);
                    gridCoordSysB = CreateGridCoordSys(gridType, datumType, zone);

                    IDNMcGridConverter gridCnvrt = DNMcGridConverter.Create(gridCoordSysA, gridCoordSysB);

                    DNSMcVector3D locationA = new DNSMcVector3D();
                    locationA.x = double.Parse(sourceLineValues[12]);
                    locationA.y = double.Parse(sourceLineValues[13]);
                    locationA.z = double.Parse(sourceLineValues[14]);
                    bool CheckGridLimits = bool.Parse(sourceLineValues[15]);

                    try
                    {
                        gridCnvrt.SetCheckGridLimits(CheckGridLimits);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("CheckGridLimits", McEx);
                    }

                    DNSMcVector3D locationB = new DNSMcVector3D();
                    int zoneB = 0;

                    try
                    {
                        gridCnvrt.ConvertAtoB(locationA,
                                                out locationB,
                                                out zoneB);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("ConvertAtoB - Line: " + (i + 1).ToString(), McEx);
                        SetExData(McEx);
                    }

                    DNSFullMGRS fullMGRS = new DNSFullMGRS();
                    try
                    {
                        if (gridCoordSysB is DNMcGridMGRS)
                        {
                            fullMGRS = ((IDNMcGridMGRS)gridCoordSysB).CoordToFullMGRS(locationB);
                        }
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("CoordToFullMGRS", McEx);
                    }

                    try
                    {
                        CheckGridLimits = gridCnvrt.GetCheckGridLimits();
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("GetCheckGridLimits", McEx);
                    }

                    outputLine = lSourceLines[i] + "," +
                                    fullMGRS.Coord.x.ToString() + "," +
                                    fullMGRS.Coord.y.ToString() + "," +
                                    fullMGRS.Coord.z.ToString() + "," +
                                    fullMGRS.Square.nZone.ToString() + "," +
                                    fullMGRS.Square.cBand.ToString() + "," +
                                    fullMGRS.Square.cSquareFst.ToString() + "," +
                                    fullMGRS.Square.cSquareSnd.ToString() + "," +
                                    CheckGridLimits.ToString() + "," +
                                    m_msgEx;

                    STW.WriteLine(outputLine);
                }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "BoundingRectAtAngle");
            }
            CloseStreams();
        }

        private void btnConvertMGRStoA_Click(object sender, EventArgs e)
        {
            ConvertMGRStoA();
            EndGeoCalcAction("Convert MGRS to A");
        }

        private void ConvertMGRStoA(string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return;

            //Print header line
            outputLine = lSourceLines[0] + "," +
                            "(Output) Location A(X)" + "," +
                            "Location A(Y)" + "," +
                            "Location A(Z)" + "," +
                            "Zone A" + "," +
                            "CheckGridLimits" + "," +
                            "Result";

            STW.WriteLine(outputLine);

            DNEGridCoordSystemType gridType;
            DNEDatumType datumType;
            int zone;
            IDNMcGridCoordinateSystem gridCoordSysA;
            IDNMcGridCoordinateSystem gridMGRS;
            DNSMcBox boxB = new DNSMcBox();
            try { 
            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                ResetExData();
                sourceLineValues = lSourceLines[i].Split(',');

                boxB.MinVertex.x = double.Parse(sourceLineValues[0]);
                boxB.MinVertex.y = double.Parse(sourceLineValues[1]);
                boxB.MinVertex.z = double.Parse(sourceLineValues[2]);
                boxB.MaxVertex.x = double.Parse(sourceLineValues[3]);
                boxB.MaxVertex.y = double.Parse(sourceLineValues[4]);
                boxB.MaxVertex.z = double.Parse(sourceLineValues[5]);

                gridType = (DNEGridCoordSystemType)Enum.Parse(typeof(DNEGridCoordSystemType), sourceLineValues[6]);
                datumType = (DNEDatumType)Enum.Parse(typeof(DNEDatumType), sourceLineValues[7]);
                zone = int.Parse(sourceLineValues[8]);
                gridMGRS = CreateGridCoordSys(gridType, datumType, zone);
                
                if (boxB.MinVertex.x != 0 || boxB.MinVertex.y != 0 || boxB.MinVertex.z != 0 ||
                    boxB.MaxVertex.x != 0 || boxB.MaxVertex.y != 0 || boxB.MaxVertex.z != 0)
                {
                    gridMGRS.SetLegalValuesForGeographicCoordinates(boxB);
                }

                gridType = (DNEGridCoordSystemType)Enum.Parse(typeof(DNEGridCoordSystemType), sourceLineValues[9]);
                datumType = (DNEDatumType)Enum.Parse(typeof(DNEDatumType), sourceLineValues[10]);
                zone = int.Parse(sourceLineValues[11]);
                gridCoordSysA = CreateGridCoordSys(gridType, datumType, zone);

                IDNMcGridConverter gridCnvrt = DNMcGridConverter.Create(gridCoordSysA, gridMGRS);
                DNSFullMGRS fullMGRS = new DNSFullMGRS();

                if (gridMGRS is IDNMcGridMGRS)
                {

                    fullMGRS.Coord.x = double.Parse(sourceLineValues[12]);
                    fullMGRS.Coord.y = double.Parse(sourceLineValues[13]);
                    fullMGRS.Coord.z = double.Parse(sourceLineValues[14]);
                    fullMGRS.Square.nZone = short.Parse(sourceLineValues[15]);
                    fullMGRS.Square.cBand = char.Parse(sourceLineValues[16]);
                    fullMGRS.Square.cSquareFst = char.Parse(sourceLineValues[17]);
                    fullMGRS.Square.cSquareSnd = char.Parse(sourceLineValues[18]);

                }
                else
                    return;

                DNSMcVector3D locationMGRS = new DNSMcVector3D();
                try
                {
                    locationMGRS = ((IDNMcGridMGRS)gridMGRS).FullMGRSToCoord(fullMGRS);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("FullMGRSToCoord", McEx);
                }

                bool CheckGridLimits = bool.Parse(sourceLineValues[19]);
                try
                {
                    gridCnvrt.SetCheckGridLimits(CheckGridLimits);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("SetCheckGridLimits", McEx);
                }

                DNSMcVector3D locationA = new DNSMcVector3D();
                int zoneA = 0;
                try
                {
                    gridCnvrt.ConvertBtoA(locationMGRS,
                                            out locationA,
                                            out zoneA);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("ConvertBtoA - Line: " + (i + 1).ToString(), McEx);
                    SetExData(McEx);
                }

                try
                {
                    CheckGridLimits = gridCnvrt.GetCheckGridLimits();
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetCheckGridLimits", McEx);
                }

                outputLine = lSourceLines[i] + "," +
                                locationA.x.ToString() + "," +
                                locationA.y.ToString() + "," +
                                locationA.z.ToString() + "," +
                                zoneA.ToString() + "," +
                                CheckGridLimits.ToString() + "," +
                                m_msgEx;

                STW.WriteLine(outputLine);
            }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "ConvertMGRStoA");
            }
            CloseStreams();
        }

        private void btnLocationFromTwoRays_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            LocationFromTwoRays();
            EndGeoCalcAction("Location From Two Rays");
        }

        private void LocationFromTwoRays(string filePath = "", DialogResult dlgResult = DialogResult.No)
        {
            if (!GetSourceLines(filePath))
                return;

            //Print header line
            outputLine = lSourceLines[0] + "," +
                            "(Output) Rays Origin Distance" + "," +
                            "Rays Shortest Distance" + "," +
                            "Location (X)" + "," +
                            "Location (Y)" + "," +
                            "Location (Z)" + "," +
                            "Result";

            STW.WriteLine(outputLine);
            try
            {
                //Call to the relevant method and print the result
                for (int i = 1; i < lSourceLines.Count; i++)
                {
                    ResetExData();
                    sourceLineValues = lSourceLines[i].Split(',');

                    //Create new Geographic Calculation if necessary
                    CreateGeographicCalculations();

                    DNSMcVector3D fstRayOrigin = new DNSMcVector3D();
                    fstRayOrigin.x = double.Parse(sourceLineValues[3]);
                    fstRayOrigin.y = double.Parse(sourceLineValues[4]);
                    fstRayOrigin.z = double.Parse(sourceLineValues[5]);

                    DNSMcVector3D fstRayOrientation = new DNSMcVector3D();
                    fstRayOrientation.x = double.Parse(sourceLineValues[6]);
                    fstRayOrientation.y = double.Parse(sourceLineValues[7]);
                    fstRayOrientation.z = double.Parse(sourceLineValues[8]);

                    DNSMcVector3D sndRayOrigin = new DNSMcVector3D();
                    sndRayOrigin.x = double.Parse(sourceLineValues[9]);
                    sndRayOrigin.y = double.Parse(sourceLineValues[10]);
                    sndRayOrigin.z = double.Parse(sourceLineValues[11]);

                    DNSMcVector3D sndRayOrientation = new DNSMcVector3D();
                    sndRayOrientation.x = double.Parse(sourceLineValues[12]);
                    sndRayOrientation.y = double.Parse(sourceLineValues[13]);
                    sndRayOrientation.z = double.Parse(sourceLineValues[14]);

                    DNMcNullableOut<double> raysOriginDistance = new DNMcNullableOut<double>();
                    DNMcNullableOut<double> raysShortestDistance = new DNMcNullableOut<double>();
                    DNMcNullableOut<DNSMcVector3D> location = new DNMcNullableOut<DNSMcVector3D>();
                    try
                    {
                        GeographicCalculations.LocationFromTwoRays(fstRayOrigin,
                                                                        fstRayOrientation,
                                                                        sndRayOrigin,
                                                                        sndRayOrientation,
                                                                        raysOriginDistance,
                                                                        raysShortestDistance,
                                                                        location);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("LocationFromTwoRays - Line: " + (i + 1).ToString(), McEx);
                        SetExData(McEx);
                    }


                    outputLine = lSourceLines[i] + "," +
                                    raysOriginDistance.Value.ToString() + "," +
                                    raysShortestDistance.Value.ToString() + "," +
                                    location.Value.x.ToString() + "," +
                                    location.Value.y.ToString() + "," +
                                    location.Value.z.ToString() + "," +
                                    m_msgEx;


                    STW.WriteLine(outputLine);

                    if (dlgResult == DialogResult.Yes)
                    {
                        m_activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;
                        DNSMcVector3D[] locationPoints = new DNSMcVector3D[2];

                        locationPoints[0] = fstRayOrigin;
                        locationPoints[1] = fstRayOrientation;
                        DrawActionResult(false, true, locationPoints);

                        locationPoints[0] = sndRayOrigin;
                        locationPoints[1] = sndRayOrientation;
                        DrawActionResult(false, false, locationPoints);

                        DNSMcVector3D[] locationIntersectionPoints = new DNSMcVector3D[1];
                        locationIntersectionPoints[0] = location.Value;
                        DrawFunction(true, EDrawnItemType.Point, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, locationIntersectionPoints, i, 0);
                    }
                }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "LocationFromTwoRays");
            }
            CloseStreams();
        }

        private void btnCirclesIntersection_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want to draw result on the map?", "Draw Action Result", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            CirclesIntersection("", dlgResult);
            EndGeoCalcAction("Circles Intersection");

        }

        private void CirclesIntersection(string filePath = "", DialogResult dlgResult = DialogResult.No)
        {
            if (!GetSourceLines(filePath))
                return;

            //Print header line
            outputLine = lSourceLines[0] + "," +
                            "Num Of Intersections" + "," +
                            "First Intersection (X)" + "," +
                            "First Intersection (Y)" + "," +
                            "First Intersection (Z)" + "," +
                            "Second Intersection (X)" + "," +
                            "Second Intersection (Y)" + "," +
                            "Second Intersection (Z)" + "," +
                            "Result";

            STW.WriteLine(outputLine);
            try
            {
                //Call to the relevant method and print the result
                for (int i = 1; i < lSourceLines.Count; i++)
                {
                    ResetExData();
                    sourceLineValues = lSourceLines[i].Split(',');

                    //Create new Geographic Calculation if necessary
                    CreateGeographicCalculations();

                    DNSMcVector3D fstCenter = new DNSMcVector3D();
                    fstCenter.x = double.Parse(sourceLineValues[3]);
                    fstCenter.y = double.Parse(sourceLineValues[4]);
                    fstCenter.z = double.Parse(sourceLineValues[5]);

                    double fstRadius = double.Parse(sourceLineValues[6]);

                    DNSMcVector3D sndCenter = new DNSMcVector3D();
                    sndCenter.x = double.Parse(sourceLineValues[7]);
                    sndCenter.y = double.Parse(sourceLineValues[8]);
                    sndCenter.z = double.Parse(sourceLineValues[9]);

                    double sndRadius = double.Parse(sourceLineValues[10]);
                    bool checkOnlyFstAzimuth = bool.Parse(sourceLineValues[11]);
                    double fstAzimut = double.Parse(sourceLineValues[12]);

                    DNMcNullableOut<UInt32> numOfIntersections = new DNMcNullableOut<uint>();
                    DNMcNullableOut<DNSMcVector3D> fstIntersection = new DNMcNullableOut<DNSMcVector3D>();
                    DNMcNullableOut<DNSMcVector3D> sndIntersection = new DNMcNullableOut<DNSMcVector3D>();
                    try
                    {
                        GeographicCalculations.CirclesIntersection(fstCenter,
                                                                        fstRadius,
                                                                        sndCenter,
                                                                        sndRadius,
                                                                        checkOnlyFstAzimuth,
                                                                        fstAzimut,
                                                                        numOfIntersections,
                                                                        fstIntersection,
                                                                        sndIntersection);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("CirclesIntersection - Line: " + (i + 1).ToString(), McEx);
                        SetExData(McEx);
                    }


                    outputLine = lSourceLines[i] + "," +
                                    numOfIntersections.Value.ToString() + "," +
                                    fstIntersection.Value.x.ToString() + "," +
                                    fstIntersection.Value.y.ToString() + "," +
                                    fstIntersection.Value.z.ToString() + "," +
                                    sndIntersection.Value.x.ToString() + "," +
                                    sndIntersection.Value.y.ToString() + "," +
                                    sndIntersection.Value.z.ToString() + "," +
                                    m_msgEx;

                    STW.WriteLine(outputLine);


                    if (dlgResult == DialogResult.Yes)
                    {
                        m_activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;
                        DNSMcVector3D[] locationPoints = new DNSMcVector3D[1];

                        locationPoints[0] = fstCenter;
                        DrawEllipse(m_activeOverlay, locationPoints, (float)fstRadius, new DNSMcBColor(255, 0, 0, 100));

                        locationPoints[0] = sndCenter;
                        DrawEllipse(m_activeOverlay, locationPoints, (float)sndRadius, new DNSMcBColor(0, 0, 255, 255));

                        // draw intersection points
                        if (numOfIntersections.Value > 0)
                        {
                            DNSMcVector3D[] locationIntersectionPoints = new DNSMcVector3D[numOfIntersections.Value];

                            locationIntersectionPoints[0] = fstIntersection.Value;
                            if (numOfIntersections.Value == 2)
                                locationIntersectionPoints[1] = sndIntersection.Value;
                            DrawFunction(true, EDrawnItemType.Point, DNGEOMETRIC_SHAPE._EG_GEOMETRIC_SHAPE_TYPE_NONE, locationIntersectionPoints, i, 0);
                        }
                    }
                }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "CirclesIntersection");
            }
            CloseStreams();
        }

        private void DrawEllipse(IDNMcOverlay activeOverlay, DNSMcVector3D[] locationPoints, float redius, DNSMcBColor color)
        {
            try
            {
                DNEFillStyle m_FillStyle = DNEFillStyle._EFS_CROSS;
                DNELineStyle m_LineStyle = DNELineStyle._ELS_SOLID;

                IDNMcObjectSchemeItem m_ObjSchemeItem = DNMcEllipseItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN | DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN,
                                                                                                         DNEMcPointCoordSystem._EPCS_WORLD,
                                                                                                         DNEItemGeometryType._EGT_GEOMETRIC_IN_OVERLAY_MANAGER,
                                                                                                         redius,
                                                                                                         redius,
                                                                                                         0,
                                                                                                         360,
                                                                                                         0,
                                                                                                         m_LineStyle,
                                                                                                         color,
                                                                                                         2,
                                                                                                         null,
                                                                                                         new DNSMcFVector2D(1, 1),
                                                                                                         2,
                                                                                                         m_FillStyle,
                                                                                                         color);


                ((IDNMcEllipseItem)m_ObjSchemeItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                IDNMcObject obj = DNMcObject.Create(activeOverlay,
                                           m_ObjSchemeItem,
                                           DNEMcPointCoordSystem._EPCS_WORLD,
                                          locationPoints,
                                          false);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Failed in creating ellipse item", McEx);
            }
        }

        private void DrawFunction(bool IsSourceParam, EDrawnItemType itemType, DNGEOMETRIC_SHAPE geometricShape, DNSMcVector3D[] vertices, int lineNumber, double ConnectorLength)
        {
            if (m_activeOverlay != null)
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

                Font font = new Font(FontFamily.GenericSansSerif, 12);
                DNSMcLogFont logFont = new DNSMcLogFont();
                font.ToLogFont(logFont);

                IDNMcLogFont defaultFont = DNMcLogFont.Create(new DNMcVariantLogFont(logFont, false));
                IDNMcTextItem defaultText = DNMcTextItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN, DNEMcPointCoordSystem._EPCS_SCREEN, defaultFont);
                IDNMcTexture defaultTexture = DNMcBitmapHandleTexture.Create(MCTester.Icons.NotationPoint.GetHbitmap(), false, false, new DNSMcBColor(0, 0, 0, 255));
                defaultText.SetText(new DNMcVariantString("Line - " + lineNumber.ToString() + shapeType, false), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                defaultText.SetBackgroundColor(new DNSMcBColor(255, 255, 255, 150), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                defaultText.SetTextColor(new DNSMcBColor(0, 128, 0, 255), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

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



                            DNMcObject.Create(m_activeOverlay,
                                                    EllipseBaseLineSchemeItem,
                                                    DNEMcPointCoordSystem._EPCS_WORLD,
                                                    vertices,
                                                    false);

                            ((IDNMcLineItem)EllipseBaseLineSchemeItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                            IDNMcTextItem verticeNumText = null;
                            defaultText.Clone(out verticeNumText);

                            DNMcObject.Create(m_activeOverlay,
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
                            m_ObjSchemeItem = DNMcPictureItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN, DNEMcPointCoordSystem._EPCS_SCREEN, defaultTexture);
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
                            defaultText.SetText(new DNMcVariantString(Math.Round(ConnectorLength, 2).ToString(), false), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("DNMcArrowItem.Create", McEx);
                            return;
                        }
                        break;
                }

                IDNMcObject obj = DNMcObject.Create(m_activeOverlay,
                                                    m_ObjSchemeItem,
                                                    DNEMcPointCoordSystem._EPCS_WORLD,
                                                    vertices,
                                                    false);


                defaultText.Connect(m_ObjSchemeItem);
                defaultText.SetAttachPointType(0, DNEAttachPointType._EAPT_CENTER_POINT, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID); 
                defaultText.SetBoundingBoxAttachPointType(0, DNEBoundingBoxPointFlags._EBBPF_BOTTOM_MIDDLE, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                defaultText.SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
            }
        }

        private void btnCalcMagneticElements_Click(object sender, EventArgs e)
        {
            if (!GetSourceLines())
                return;

            //Print header line
            outputLine = lSourceLines[0] + "," +
                            "(Output) dDecl" + "," +
                            "dIncl" + "," +
                            "dH" + "," +
                            "dX" + "," +
                            "dY" + "," +
                            "dZ" + "," +
                            "dF" + "," +
                           "dDecldot" + "," +
                            "dIncldot" + "," +
                            "dHdot" + "," +
                            "dXdot" + "," +
                            "dYdot" + "," +
                            "dZdot" + "," +
                            "dFdot" + "," +
                            "dGVdot" + "," +
                            "Result";

            STW.WriteLine(outputLine);
            try
            {
                //Call to the relevant method and print the result
                for (int i = 1; i < lSourceLines.Count; i++)
                {
                    ResetExData();
                    sourceLineValues = lSourceLines[i].Split(',');

                    //Create new Geographic Calculation if necessary
                    CreateGeographicCalculations();

                    DNSMcVector3D point = new DNSMcVector3D();
                    point.x = double.Parse(sourceLineValues[3]);
                    point.y = double.Parse(sourceLineValues[4]);
                    point.z = double.Parse(sourceLineValues[5]);

                    DateTime date = DateTime.Parse(sourceLineValues[6]);
                    DateTime time = DateTime.Parse(sourceLineValues[7]);
                    DateTime dateTime = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);

                    DNSMagneticElements elements = new DNSMagneticElements();
                    try
                    {
                        GeographicCalculations.CalcMagneticElements(point, dateTime, out elements);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("CalcMagneticElements - Line: " + (i + 1).ToString(), McEx);
                        SetExData(McEx);
                    }

                    outputLine = lSourceLines[i] + "," +
                                    Math.Round(elements.dDecl, 2).ToString() + "," +
                                    Math.Round(elements.dIncl, 2).ToString() + "," +
                                    Math.Round(elements.dH, 1).ToString() + "," +
                                    Math.Round(elements.dX, 1).ToString() + "," +
                                    Math.Round(elements.dY, 1).ToString() + "," +
                                    Math.Round(elements.dZ, 1).ToString() + "," +
                                    Math.Round(elements.dF, 1).ToString() + "," +
                                    Math.Round(elements.dDecldot, 1).ToString() + "," +
                                    Math.Round(elements.dIncldot, 1).ToString() + "," +
                                    Math.Round(elements.dHdot, 1).ToString() + "," +
                                    Math.Round(elements.dXdot, 1).ToString() + "," +
                                    Math.Round(elements.dYdot, 1).ToString() + "," +
                                    Math.Round(elements.dZdot, 1).ToString() + "," +
                                    Math.Round(elements.dFdot, 1).ToString() + "," +
                                    Math.Round(elements.dGVdot, 1).ToString() + "," +
                                    m_msgEx;

                    STW.WriteLine(outputLine);

                }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file ", "CalcMagneticElements");
            }
            CloseStreams();
            EndGeoCalcAction("Calc Magnetic Elements");
        }

        private void btnAutomaticCalculations_Click(object sender, EventArgs e)
        {
            FolderSelectDialog folderSelectDialog = new FolderSelectDialog();
            folderSelectDialog.Title = "Input folder to select";
            folderSelectDialog.InitialDirectory = @"c:\";

            if (folderSelectDialog.ShowDialog(IntPtr.Zero))
            {
                string inputFolderPath = folderSelectDialog.FileName;
                string[] filesPath = Directory.GetFiles(inputFolderPath, "*.csv");

                mMCTime.Reset();
                mMCTime.Start();

                foreach (string filePath in filesPath)
                {
                    string filePathWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                    string fileName = "";
                   
                    string[] names = filePathWithoutExt.Split('_');
                    if (names != null && names.Length >= 2)
                    {
                        fileName = names[1];
                        switch (fileName)
                        {
                            case "AzimuthAndDistanceBetweenTwoLoctions":
                                CalcAzimuthAndDistanceBetweenTwoLocations(filePath);
                                break;
                            case "ArcSample":
                                CalcArcSample(filePath);
                                break;
                            case "CalcCenterAndLenghtFromRectangle":
                                CalcCenterAndLengthsFromRectangle(filePath);
                                break;
                            case "CalcGeographicRectanglePolygon":
                                // throw map core exception - NOT IMPLEMENT
                                //CalcGeograhicRectanglePolygons(filePath);
                                break;
                            case "ConvertHeightFromEllipsoidToGeoid":
                                ConvertHeightFromEllipsoidToGeoid(filePath);
                                break;
                            case "ConvertHeightFromGeoidToEllipsoid":
                                ConvertHeightFromGeoidToEllipsoid(filePath);
                                break;
                            case "CalcLocalAzimuthRadius":
                                CalcLocalAzimuthRadius(filePath);
                                break;
                            case "CalcLocalRadius":
                                CalcLocalRadius(filePath);
                                break;
                            case "CalcRectangleFromCenterAndLengths":
                                CalcRectangleFromCenterAndLengths(filePath);
                                break;
                            case "CalcSunDirection":
                                // throw map core exception - NOT IMPLEMENT
                                //CalcSunDirection(filePath);
                                break;
                            case "ConvertAzimuthFromGeoToGrid":
                                ConvertAzimuthFromGeoToGrid(filePath);
                                break;
                            case "ConvertAzimuthFromGridToGeo":
                                ConvertAzimuthFromGridToGeo(filePath);
                                break;
                            case "ConvertAzimuthFromOtherCoordSys":
                                ConvertAzimuthFromOtherCoordSys(filePath);
                                break;
                            case "ConvertAzimuthToOtherCoordSys":
                                ConvertAzimuthToOtherCoordSys(filePath);
                                break;
                             case "ConvertAtoB":
                                ConvertAtoB(filePath);
                                break;
                            case "ConvertBtoA":
                                ConvertBtoA(filePath);
                                break;
                            case "ConvertAtoMGRS":
                                ConvertAtoMGRS(filePath);
                                break;
                            case "ConvertMGRSToA":
                                ConvertMGRStoA(filePath);
                                break;
                            case "ConvertAtoGeoref":
                                ConvertAtoGeoRef(filePath);
                                break;
                            case "ConvertGeorefToA":
                                ConvertGeoReftoA(filePath);
                                break;
                            case "IsPointInArc":
                                IsPointInArc(filePath);
                                break;
                            case "IsPointOn2DLine":
                                IsPointOn2DLine(filePath);
                                break;
                            case "LineSample":
                                LineSample(filePath);
                                break;
                            case "LocationFromAzimuthAndDistance":
                                LocationFromAzimuthAndDistance(filePath);
                                break;
                            case "LocationFromLocationAndVector":
                                LocationFromLocationAndVector(filePath);
                                break;
                            case "PolygonExpand":
                                PolygonExpand(filePath);
                                break;
                            case "PolygonSphericArea":
                                PolygonSphericArea(filePath);
                                break;
                            case "PolyLineExpand":
                                PolylineExpand(filePath);
                                break;
                            case "PolylineLength":
                                PolyLineLength(filePath);
                                break;
                            case "ShortestDistPoint2DLine":
                                ShortestDistPoint2DLine(filePath);
                                break;
                            case "ShortestDistPointArc":
                                ShortestDistPointArc(filePath);
                                break;
                            case "SmallestBoundingRectGeo":
                                SmallestBoundingRect(filePath);
                                break;
                            case "SmallestBoundingRectUTM":
                                SmallestBoundingRect(filePath);
                                break;
                            case "SplitPolylineOnDateline":
                                // throw map core exception - NOT IMPLEMENT
                                //SplitPolylineOnDateLine(filePath);
                                break;
                            case "VectorFromTwoLocations":
                                VectorFromTwoLocations(filePath);
                                break;
                        }
                    }
                    else
                        MessageBox.Show("Invalid file name", "Invalid file name " + filePathWithoutExt);
                }

                mMCTime.Stop();

                MessageBox.Show("calc duration:  " + mMCTime.ElapsedMilliseconds.ToString() + " ms", "Automatic Calculations Finished Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);

                EndGeoCalcAction("Automatic Calculation");
            }
        }

        private void btnConvertHeightFromGeoidToEllipsoid_Click(object sender, EventArgs e)
        {
            ConvertHeightFromGeoidToEllipsoid();
            EndGeoCalcAction("Convert Height From Geoid To Ellipsoid");
        }

        private void ConvertHeightFromEllipsoidToGeoid(string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return;

            //Print header line
            outputLine = lSourceLines[0] + "," +
                            "(Output) Height" + "," +
                            "Result";

            STW.WriteLine(outputLine);
            outputLine = "";
            try
            {
                //Call to the relevant method and print the result
                for (int i = 1; i < lSourceLines.Count; i++)
                {
                    ResetExData();
                    sourceLineValues = lSourceLines[i].Split(',');

                    //Create new Geographic Calculation if necessary
                    CreateGeographicCalculations();

                    DNSMcVector3D location = new DNSMcVector3D();
                    //Collect the polyline vertices to a DNSMcVector3D list
                    for (int idx = 3; (idx + 2) < sourceLineValues.Length; idx += 3)
                    {

                        location.x = double.Parse(sourceLineValues[idx]);
                        location.y = double.Parse(sourceLineValues[idx + 1]);
                        location.z = double.Parse(sourceLineValues[idx + 2]);

                    }

                    double height = 0;
                    try
                    {
                        height = GeographicCalculations.ConvertHeightFromEllipsoidToGeoid(location);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("ConvertHeightFromEllipsoidToGeoid - Line: " + (i + 1).ToString(), McEx);
                        SetExData(McEx);
                    }

                    outputLine = lSourceLines[i] + "," +
                                    height.ToString() + "," +
                                    m_msgEx;

                    STW.WriteLine(outputLine);
                }
            }
            catch (SystemException)
            {
                MessageBox.Show("invalid input file " + filePath, "Error in data");
            }

            CloseStreams();


        }

        private void ConvertHeightFromGeoidToEllipsoid(string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return;

            //Print header line
            outputLine = lSourceLines[0] + "," +
                            "(Output) Height" + "," +
                            "Result";

            STW.WriteLine(outputLine);
            outputLine = "";
            try
            {
                //Call to the relevant method and print the result
                for (int i = 1; i < lSourceLines.Count; i++)
                {
                    ResetExData();
                    sourceLineValues = lSourceLines[i].Split(',');

                    DNSMcVector3D location = new DNSMcVector3D();

                    //Create new Geographic Calculation if necessary
                    CreateGeographicCalculations();

                    //Collect the polyline vertices to a DNSMcVector3D list
                    for (int idx = 3; idx < sourceLineValues.Length; idx += 3)
                    {
                        location.x = double.Parse(sourceLineValues[idx]);
                        location.y = double.Parse(sourceLineValues[idx + 1]);
                        location.z = double.Parse(sourceLineValues[idx + 2]);
                    }

                    double height = 0;
                    try
                    {
                        height = GeographicCalculations.ConvertHeightFromGeoidToEllipsoid(location);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("ConvertHeightFromGeoidToEllipsoid - Line: " + (i + 1).ToString(), McEx);
                        SetExData(McEx);
                    }

                    outputLine = lSourceLines[i] + "," +
                                    height.ToString() + "," +
                                    m_msgEx;

                    STW.WriteLine(outputLine);
                }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "ConvertHeightFromGeoidToEllipsoid");
            }

            CloseStreams();
        }

        private void btnConvertHeightFromEllipsoidToGeoid_Click(object sender, EventArgs e)
        {
            ConvertHeightFromEllipsoidToGeoid();
            EndGeoCalcAction("Convert Height From Ellipsoid To Geoid");
        }

        private void btnConvertAtoGeoRef_Click(object sender, EventArgs e)
        {
            ConvertAtoGeoRef();
            EndGeoCalcAction("Convert A to GeoRef");
        }

        private void ConvertAtoGeoRef(string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return;

            //Print header line
            outputLine = lSourceLines[0] + "," +
                            "(Output) FullGEOREF" +","+
                            "CheckGridLimits" + "," +
                            "Result";

            STW.WriteLine(outputLine);

            DNEGridCoordSystemType gridType;
            DNEDatumType datumType;
            int zone;
            IDNMcGridCoordinateSystem gridCoordSysA;
            IDNMcGridCoordinateSystem gridCoordSysB;
            DNSMcBox boxA = new DNSMcBox();
            try { 
            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                ResetExData();
                sourceLineValues = lSourceLines[i].Split(',');

                boxA.MinVertex.x = double.Parse(sourceLineValues[0]);
                boxA.MinVertex.y = double.Parse(sourceLineValues[1]);
                boxA.MinVertex.z = double.Parse(sourceLineValues[2]);
                boxA.MaxVertex.x = double.Parse(sourceLineValues[3]);
                boxA.MaxVertex.y = double.Parse(sourceLineValues[4]);
                boxA.MaxVertex.z = double.Parse(sourceLineValues[5]);

                gridType = (DNEGridCoordSystemType)Enum.Parse(typeof(DNEGridCoordSystemType), sourceLineValues[6]);
                datumType = (DNEDatumType)Enum.Parse(typeof(DNEDatumType), sourceLineValues[7]);
                zone = int.Parse(sourceLineValues[8]);
                gridCoordSysA = CreateGridCoordSys(gridType, datumType, zone);


                if (boxA.MinVertex.x != 0 || boxA.MinVertex.y != 0 || boxA.MinVertex.z != 0 ||
                    boxA.MaxVertex.x != 0 || boxA.MaxVertex.y != 0 || boxA.MaxVertex.z != 0)
                {
                    gridCoordSysA.SetLegalValuesForGeographicCoordinates(boxA);
                }

                gridType = (DNEGridCoordSystemType)Enum.Parse(typeof(DNEGridCoordSystemType), sourceLineValues[9]);
                datumType = (DNEDatumType)Enum.Parse(typeof(DNEDatumType), sourceLineValues[10]);
                zone = int.Parse(sourceLineValues[11]);
                gridCoordSysB = CreateGridCoordSys(gridType, datumType, zone);

                IDNMcGridConverter gridCnvrt = DNMcGridConverter.Create(gridCoordSysA, gridCoordSysB);

                DNSMcVector3D locationA = new DNSMcVector3D();
                locationA.x = double.Parse(sourceLineValues[12]);
                locationA.y = double.Parse(sourceLineValues[13]);
                locationA.z = double.Parse(sourceLineValues[14]);
                bool CheckGridLimits = bool.Parse(sourceLineValues[15]);

                try
                {
                    gridCnvrt.SetCheckGridLimits(CheckGridLimits);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("CheckGridLimits", McEx);
                }

                DNSMcVector3D locationB = new DNSMcVector3D();
                int zoneB = 0;

                try
                {
                    gridCnvrt.ConvertAtoB(locationA,
                                            out locationB,
                                            out zoneB);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("ConvertAtoB - Line: " + (i + 1).ToString(), McEx);
                    SetExData(McEx);
                }

                String fullGEOREF = "";
                try
                {
                    if (gridCoordSysB is IDNMcGridGEOREF)
                    {
                        fullGEOREF = ((IDNMcGridGEOREF)gridCoordSysB).CoordToFullGEOREF(locationB);
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("CoordToFullGEOREF", McEx);
                }

                try
                {
                    CheckGridLimits = gridCnvrt.GetCheckGridLimits();
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetCheckGridLimits", McEx);
                }


                outputLine = lSourceLines[i] + "," +
                                fullGEOREF + "," +
                                CheckGridLimits.ToString() + "," +
                                m_msgEx;

                STW.WriteLine(outputLine);
            }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "ConvertAtoGeoRef");
            }
            CloseStreams();
        }

        private void btnConvertGeoReftoA_Click(object sender, EventArgs e)
        {
            ConvertGeoReftoA();
            EndGeoCalcAction("Convert GeoRef to A");
        }

        private void ConvertGeoReftoA(string filePath = "")
        {
            if (!GetSourceLines(filePath))
                return;

            //Print header line
            outputLine = lSourceLines[0] + "," +
                            "(Output) Location A(X)" + "," +
                            "Location A(Y)" + "," +
                            "Location A(Z)" + "," +
                            "Zone A" + "," +
                            "CheckGridLimits" + "," +
                            "Result";

            STW.WriteLine(outputLine);

            DNEGridCoordSystemType gridType;
            DNEDatumType datumType;
            int zone;
            IDNMcGridCoordinateSystem gridCoordSysA;
            IDNMcGridCoordinateSystem gridGeoref;
            DNSMcBox boxB = new DNSMcBox();
            try { 
            //Call to the relevant method and print the result
            for (int i = 1; i < lSourceLines.Count; i++)
            {
                ResetExData();
                sourceLineValues = lSourceLines[i].Split(',');

                boxB.MinVertex.x = double.Parse(sourceLineValues[0]);
                boxB.MinVertex.y = double.Parse(sourceLineValues[1]);
                boxB.MinVertex.z = double.Parse(sourceLineValues[2]);
                boxB.MaxVertex.x = double.Parse(sourceLineValues[3]);
                boxB.MaxVertex.y = double.Parse(sourceLineValues[4]);
                boxB.MaxVertex.z = double.Parse(sourceLineValues[5]);

                gridType = (DNEGridCoordSystemType)Enum.Parse(typeof(DNEGridCoordSystemType), sourceLineValues[6]);
                datumType = (DNEDatumType)Enum.Parse(typeof(DNEDatumType), sourceLineValues[7]);
                zone = int.Parse(sourceLineValues[8]);
                gridGeoref = CreateGridCoordSys(gridType, datumType, zone);


                if (boxB.MinVertex.x != 0 || boxB.MinVertex.y != 0 || boxB.MinVertex.z != 0 ||
                    boxB.MaxVertex.x != 0 || boxB.MaxVertex.y != 0 || boxB.MaxVertex.z != 0)
                {
                    gridGeoref.SetLegalValuesForGeographicCoordinates(boxB);
                }

                gridType = (DNEGridCoordSystemType)Enum.Parse(typeof(DNEGridCoordSystemType), sourceLineValues[9]);
                datumType = (DNEDatumType)Enum.Parse(typeof(DNEDatumType), sourceLineValues[10]);
                zone = int.Parse(sourceLineValues[11]);
                gridCoordSysA = CreateGridCoordSys(gridType, datumType, zone);

                IDNMcGridConverter gridCnvrt = DNMcGridConverter.Create(gridCoordSysA, gridGeoref);
                String strFullGeoref = "";

                if (gridGeoref is IDNMcGridGEOREF)
                {
                    strFullGeoref = sourceLineValues[12];
                }
                else
                    return;

                DNSMcVector3D locationGeoref = new DNSMcVector3D();
                try
                {
                    locationGeoref = ((IDNMcGridGEOREF)gridGeoref).FullGEOREFToCoord(strFullGeoref);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("FullGEOREFToCoord", McEx);
                }

                bool CheckGridLimits = bool.Parse(sourceLineValues[13]);
                try
                {
                    gridCnvrt.SetCheckGridLimits(CheckGridLimits);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("SetCheckGridLimits", McEx);
                }

                DNSMcVector3D locationA = new DNSMcVector3D();
                int zoneA = 0;
                try
                {
                    gridCnvrt.ConvertBtoA(locationGeoref,
                                            out locationA,
                                            out zoneA);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("ConvertGeoReftoA (ConvertBtoA) - Line: " + (i + 1).ToString(), McEx);
                    SetExData(McEx);
                }

                try
                {
                    CheckGridLimits = gridCnvrt.GetCheckGridLimits();
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetCheckGridLimits", McEx);
                }

                outputLine = lSourceLines[i] + "," +
                                locationA.x.ToString() + "," +
                                locationA.y.ToString() + "," +
                                locationA.z.ToString() + "," +
                                zoneA.ToString() + "," +
                                CheckGridLimits.ToString() + "," +
                                m_msgEx;




                STW.WriteLine(outputLine);
            }
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid input file " + filePath, "ConvertGeoReftoA");
            }
            CloseStreams();
        }

    }
}
