using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using System.IO;
using MCTester.Managers;

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class ucProjectorItem : ucPhysicalItem, IUserControlItem
    {
        private IDNMcProjectorItem m_CurrentObject;
        private Timer tmr;
        private string[] BMPFilesNames;
        private string[] TMEFilesNames;
        private List<TelemtryCorrelation> TelemetryParamsList;
        private int numPictureInDir;
        private int currPicture;
        private IDNMcGridConverter gridCnvrt;
        private IDNMcImageFileTexture [] TextureArr;
        
        public ucProjectorItem()
        {
            InitializeComponent();

            //Create IDNMcGridConverter member for all projector points conversions
            IDNMcGridCoordinateSystem gridCoordSysA = DNMcGridCoordSystemGeographic.Create(DNEDatumType._EDT_ED50_ISRAEL);
            IDNMcGridCoordinateSystem gridCoordSysB = DNMcGridUTM.Create(36, DNEDatumType._EDT_ED50_ISRAEL);

            gridCnvrt = DNMcGridConverter.Create(gridCoordSysA, gridCoordSysB, false);            
        }

        protected override void SaveItem()
        {
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);

            base.SaveItem();

            try
            {
                ctrlObjStatePropertyProjectorFOV.Save(m_CurrentObject.SetFieldOfView);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetFieldOfView", McEx);
            }

            try
            {
                ctrlObjStatePropertyTexture.Save(m_CurrentObject.SetTexture);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetTexture", McEx);
            }

            try
            {
                ctrlObjStatePropertyAspectRatio.Save(m_CurrentObject.SetAspectRatio);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetAspectRatio", McEx);
            }

            try
            {
                m_CurrentObject.SetProjectionBorders(ntxProjectorBorderLeft.GetFloat(),
                                                        ntxProjectorBorderTop.GetFloat(),
                                                        ntxProjectorBorderRight.GetFloat(),
                                                        ntxProjectorBorderBottom.GetFloat());


            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetProjectionBorders", McEx);
            }

            try
            {
                ctrlObjStatePropertyProjectorTargetTypes.Save(m_CurrentObject.SetTargetTypes);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetTargetTypes", McEx);
            }

            // turn on all viewports render needed flags
            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }

        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcProjectorItem)aItem;
            Manager_MCPropertyDescription.RemoveNode(m_CurrentObject);
            base.LoadItem(aItem);

            try
            {
                ctrlObjStatePropertyProjectorFOV.Load(m_CurrentObject.GetFieldOfView);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetFieldOfView", McEx);
            }

            try
            {
                ctrlObjStatePropertyTexture.Load(m_CurrentObject.GetTexture);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetTexture", McEx);
            }

            try
            {
                ctrlObjStatePropertyAspectRatio.Load(m_CurrentObject.GetAspectRatio);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetAspectRatio", McEx);
            }

            try
            {
                float fLeft, fTop, fRight, fBottom;

                m_CurrentObject.GetProjectionBorders(out fLeft,
                                                        out fTop,
                                                        out fRight,
                                                        out fBottom);


                ntxProjectorBorderLeft.SetFloat(fLeft);
                ntxProjectorBorderTop.SetFloat(fTop);
                ntxProjectorBorderRight.SetFloat(fRight);
                ntxProjectorBorderBottom.SetFloat(fBottom);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetProjectionBorders", McEx);
            }

            try
            {
                ctrlObjStatePropertyProjectorTargetTypes.Load(m_CurrentObject.GetTargetTypes);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetTargetTypes", McEx);
            }

            try
            {
                chxIsUsingTextureMetadata.Checked = m_CurrentObject.IsUsingTextureMetadata();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("IsUsingTextureMetadata", McEx);
            }
        }

        #endregion

        private void chxProjectorDemo_CheckedChanged(object sender, EventArgs e)
        {
            if (chxProjectorDemo.Checked == true && ctrlBrowseDomoFilesDirectory.FileName != "")
            {
                chxProjectorDemo.Text = "Stop Demo";
                BMPFilesNames = Directory.GetFiles(ctrlBrowseDomoFilesDirectory.FileName, "*.bmp");
                numPictureInDir = BMPFilesNames.Length;
                currPicture = 0;
                TextureArr = new IDNMcImageFileTexture[numPictureInDir];

                for (int i = 0; i < numPictureInDir; i++ )
                {
                    TextureArr[i] = DNMcImageFileTexture.Create(new DNSMcFileSource(BMPFilesNames[i]), false);
                }

                TMEFilesNames = Directory.GetFiles(ctrlBrowseDomoFilesDirectory.FileName, "*.tme");

                TelemetryParamsList = new List<TelemtryCorrelation>();
                string[] textureTelemetryParams;
                string currLine;
                int currParam;

                for (int i = 0; i < TMEFilesNames.Length; i++ )
                {
                    // create reader & open file
                    System.IO.TextReader tr = new StreamReader(TMEFilesNames[i]);

                    textureTelemetryParams = new string[11];
                    currParam = 0;
                    currLine = tr.ReadLine();
                    while (currLine != null)
                    {
                        textureTelemetryParams[currParam] = currLine.Trim();
                        currLine = tr.ReadLine();
                        currParam++;
                    }

                    TelemetryParamsList.Add(GetParsesTelemetryParams(textureTelemetryParams));

                    //Set converted coordinates
                    DNSMcVector3D convertedCoords = ConvertGeoCoordToUTM(TelemetryParamsList[i].CoordinatX, TelemetryParamsList[i].CoordinatY, TelemetryParamsList[i].CoordinatZ);
                    TelemetryParamsList[i].CoordinatX = convertedCoords.x;
                    TelemetryParamsList[i].CoordinatY = convertedCoords.y;
                    TelemetryParamsList[i].CoordinatZ = convertedCoords.z;

                    //Set calculated Yaw
                    TelemetryParamsList[i].Yaw = CalcYaw(TelemetryParamsList[i].SinBearing, TelemetryParamsList[i].CosBearing);

                    
                    //Set calculated FOV
                    TelemetryParamsList[i].Zoom = CalcFOV(TelemetryParamsList[i].Zoom);
                    
                    // close the stream
                    tr.Close();
                }
                
                tmr = new Timer();
                tmr.Interval = 60;
                tmr.Tick += new EventHandler(tmr_Tick);
                tmr.Start();
            }
            else
            {
                if (tmr != null)
                {
                    tmr.Stop();
                    tmr.Tick -= new EventHandler(tmr_Tick);
                    chxProjectorDemo.Text = "Run Demo";
                }                
            }
        }

        void tmr_Tick(object sender, EventArgs e)
        {
            if (currPicture < numPictureInDir)
            {
                try
                {
                    m_CurrentObject.SetTexture((IDNMcTexture)TextureArr[currPicture],
                                            (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID,
                                            false);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("SetTexture", McEx);
                }                
                
                try
                {
                    DNSMcVector3D[] newLocation = new DNSMcVector3D[] { new DNSMcVector3D(TelemetryParamsList[currPicture].CoordinatX, TelemetryParamsList[currPicture].CoordinatY, TelemetryParamsList[currPicture].CoordinatZ) };
                    (m_CurrentObject.GetScheme().GetObjects())[0].SetLocationPoints(newLocation, 0);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetScheme/GetObjects/SetLocationPoints", McEx);
                }

                try
                {
                    //Set projector rotation (based on camera params only treated as absolute angles)
                    m_CurrentObject.SetRotation(new DNSMcRotation((float)TelemetryParamsList[currPicture].Yaw, (float)TelemetryParamsList[currPicture].Elevation, (float)TelemetryParamsList[currPicture].Roll, false),
                                                    (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("SetRotation", McEx);
                }

                try
                {
                    m_CurrentObject.SetFieldOfView((float)(TelemetryParamsList[currPicture].Zoom),
                                                            (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID,
                                                            false);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("SetFieldOfView", McEx);
                }

                currPicture++;
            }
            else
                currPicture = 0;
        }

        private TelemtryCorrelation GetParsesTelemetryParams(string[] textureTelemetryParams)
        {
            TelemtryCorrelation textureTelemetry = new TelemtryCorrelation();

            //Read parameters from file
            textureTelemetry.CoordinatY = double.Parse(textureTelemetryParams[0].ToString());
            textureTelemetry.CoordinatX = double.Parse(textureTelemetryParams[1].ToString());
            textureTelemetry.CoordinatZ = double.Parse(textureTelemetryParams[2].ToString());
            textureTelemetry.Zoom = double.Parse(textureTelemetryParams[3].ToString());
            textureTelemetry.Pitch = double.Parse(textureTelemetryParams[4].ToString());
            textureTelemetry.Roll = double.Parse(textureTelemetryParams[5].ToString());
            textureTelemetry.Heading = double.Parse(textureTelemetryParams[6].ToString());
            textureTelemetry.SinBearing = double.Parse(textureTelemetryParams[7].ToString());
            textureTelemetry.CosBearing = double.Parse(textureTelemetryParams[8].ToString());
            textureTelemetry.Elevation = double.Parse(textureTelemetryParams[9].ToString());
            textureTelemetry.DayNight = int.Parse(textureTelemetryParams[10].ToString());

            return textureTelemetry;
        }

        private DNSMcVector3D ConvertGeoCoordToUTM(double coordX, double coordY, double coordZ)
        {
            DNSMcVector3D LocationA = new DNSMcVector3D();
            LocationA.x = coordX * 100000;
            LocationA.y = coordY * 100000;
            LocationA.z = coordZ;

            DNSMcVector3D LocationB = new DNSMcVector3D();
            int zoneB;

            try
            {
                gridCnvrt.ConvertAtoB(LocationA,
                                        out LocationB,
                                        out zoneB);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("ConvertAtoB", McEx);
            }

            return LocationB;

        }

        private double CalcFOV(double zoom)
        {
            double FOV = 0.75 * System.Math.Exp((System.Math.Abs(255 - zoom) - 30.078) / 87.822);
            return FOV;
        }

        private double CalcYaw(double sin, double cos)
        {
            double Yaw = System.Math.Atan2(sin, cos) * 180 / System.Math.PI; 
            return Yaw;
        }

    }

    public class TelemtryCorrelation
    {
        public double CoordinatX;
        public double CoordinatY;
        public double CoordinatZ;
        public double Zoom;
        public double Yaw;
        public double Pitch;
        public double Roll;
        public double Heading;
        public double SinBearing;
        public double CosBearing;
        public double Elevation;
        public int DayNight;

    }
}
