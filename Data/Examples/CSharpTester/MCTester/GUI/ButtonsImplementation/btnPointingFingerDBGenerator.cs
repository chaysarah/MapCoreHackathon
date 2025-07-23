using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnmanagedWrapper;
using MapCore;
using System.Windows.Forms;
using MCTester.Managers.ObjectWorld;
using MCTester.Managers.MapWorld;
using System.Drawing;
using System.IO;
using MCTester.General_Forms;

namespace MCTester.ButtonsImplementation
{
    public class btnPointingFingerDBGenerator
    {
        //private OpenFileDialog OFD;
        private StreamWriter m_SW;
        private StreamWriter m_PointsFile;
        private int m_PicNum = 0;
        private List<DNSMcVector3D> m_lCheckPointInWorldCord = new List<DNSMcVector3D>();
        private List<int> m_lPointsRadius = new List<int>();
        private IDNMcMapCamera activeCamera;
        private frmPointingFingerDBGenerator m_RunPArams;
        //private float m_RadiusFactor = 1;
        //private List<float> m_lStartPointsScale = new List<float>();
        //private List<float> m_lPointsScale = new List<float>();

        public btnPointingFingerDBGenerator(frmPointingFingerDBGenerator PFParams)
        {
            m_RunPArams = PFParams;            
        }


        public void ExecuteAction()
        {
            MCTMapFormManager.MapForm.Cursor = Cursors.WaitCursor;

            // read world target points
            StreamReader SR = new StreamReader(@"C:\PointingFingerDB\CheckPointInWorldCord.txt");
            while (!SR.EndOfStream)
            {
                string line = SR.ReadLine();
                string[] coordinate = line.Split(',');
                DNSMcVector3D newCoord = new DNSMcVector3D(double.Parse(coordinate[0]), double.Parse(coordinate[1]), double.Parse(coordinate[2]));
                m_lCheckPointInWorldCord.Add(newCoord);
                m_lPointsRadius.Add(int.Parse(coordinate[3]));
            }
            SR.Close();
            
            // set camera to start position            
            activeCamera = MCTMapFormManager.MapForm.Viewport.ActiveCamera;
            activeCamera.SetCameraRelativeHeightLimits(10, 5000, true);

            DNSMcVector3D lookAtPoint = m_RunPArams.ctrl3DVectorLookAtPt.GetVector3D();
            DNSMcVector3D cameraLocation = lookAtPoint;
            cameraLocation.z = lookAtPoint.z + 50;
            activeCamera.SetCameraFieldOfView(8);

            MCTMapFormManager.MapForm.Viewport.PerformPendingUpdates();
            MCTMapFormManager.MapForm.Viewport.Render();
                        
            // loop the action between min distance to max distance in meters from the target
            for (int range = -(m_RunPArams.ntxDisMin.GetInt32()); range >= -(m_RunPArams.ntxDisMax.GetInt32()); range -= (m_RunPArams.ntxDisGap.GetInt32()))
            {
                cameraLocation.x = lookAtPoint.x;
                cameraLocation.y = lookAtPoint.y + range;
                activeCamera.SetCameraPosition(cameraLocation, false);

                MCTMapFormManager.MapForm.Viewport.PerformPendingUpdates();
                MCTMapFormManager.MapForm.Viewport.Render();
                
                activeCamera.SetCameraLookAtPoint(lookAtPoint);
                
                MCTMapFormManager.MapForm.Viewport.PerformPendingUpdates();
                MCTMapFormManager.MapForm.Viewport.Render();

                // zero the start angle position
                try
                {
                    float startPosition = m_RunPArams.ntxRotationAngleMin.GetInt32() - m_RunPArams.ntxRotationAngleDelta.GetInt32();
                    activeCamera.RotateCameraAroundWorldPoint(lookAtPoint,
                                                        startPosition);

                    MCTMapFormManager.MapForm.Viewport.PerformPendingUpdates();
                    MCTMapFormManager.MapForm.Viewport.Render();
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("RotateCameraAroundWorldPoint", McEx);
                }

                // loop the action between min angle to max angle degrees relative to the lookAtPoint
                for (int yaw = m_RunPArams.ntxRotationAngleMin.GetInt32(); yaw <= m_RunPArams.ntxRotationAngleMax.GetInt32(); yaw += m_RunPArams.ntxRotationAngleDelta.GetInt32())
                {
                    m_PicNum++;
                    
                    try
                    {
                        activeCamera.RotateCameraAroundWorldPoint(lookAtPoint,
                                                            m_RunPArams.ntxRotationAngleDelta.GetInt32());

                        MCTMapFormManager.MapForm.Viewport.PerformPendingUpdates();
                        MCTMapFormManager.MapForm.Viewport.Render();
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("RotateCameraAroundWorldPoint", McEx);
                    }
                                        
                    uint pWidht, pHeight;
                    MCTMapFormManager.MapForm.Viewport.GetViewportSize(out pWidht, out pHeight);

                    // take viewport picture using mapCore
                    uint stride = pWidht * 4;
                    int bufferSize = (int)stride * (int)pHeight;
                    IntPtr ptr = System.Runtime.InteropServices.Marshal.AllocHGlobal(bufferSize);
                    DNSMcRect rect = new DNSMcRect(0, 0, (int)pWidht, (int)pHeight);
                    MCTMapFormManager.MapForm.Viewport.RenderScreenRectToBuffer(rect, pWidht, pHeight, DNEPixelFormat._EPF_X8R8G8B8, 0, ptr);
                    Bitmap bmp = new Bitmap((int)pWidht, (int)pHeight, (int)stride, System.Drawing.Imaging.PixelFormat.Format32bppRgb, ptr);
                    
                    float currYaw;
                    float currPitch;
                    float currRoll;
                    activeCamera.GetCameraOrientation(out currYaw, out currPitch, out currRoll);

                    // convert yaw angle from UTM_WGS84 (actually it is ED_50 but treated as WGS84) to GEO_WGS84 
                    int zone = 36;
                    double geoAzimuth = 0;
                    IDNMcGridConverter gridCnvrt;

                    IDNMcGridCoordinateSystem UTMCoordSys = DNMcGridUTM.Create(zone, DNEDatumType._EDT_WGS84);
                    IDNMcGeographicCalculations UTMGeographicCalculations = DNMcGeographicCalculations.Create(UTMCoordSys);
                    IDNMcGridCoordinateSystem GeoCoordSys = DNMcGridCoordSystemGeographic.Create(DNEDatumType._EDT_WGS84);
                    

                    try
                    {
                        geoAzimuth = UTMGeographicCalculations.ConvertAzimuthFromGridToGeo(cameraLocation,
                                                                                            currYaw,
                                                                                            false);
                                                                                                            
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("ConvertAzimuthFromGridToGeo", McEx);
                    }

                    DNSMcVector3D currPos = activeCamera.GetCameraPosition();

                    DNSMcVector3D locationAfterConvertion = new DNSMcVector3D();
                    DNSMcVector3D lookAtPointAfterConvertion = new DNSMcVector3D();
                    int zoneB;

                    try
                    {
                        gridCnvrt = DNMcGridConverter.Create(UTMCoordSys, GeoCoordSys, false);
                        
                        gridCnvrt.ConvertAtoB(currPos, out locationAfterConvertion, out zoneB);
                        gridCnvrt.ConvertAtoB(lookAtPoint, out lookAtPointAfterConvertion, out zoneB);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("DNMcGridConverter.Create", McEx);
                    }

                    string fileName = m_PicNum + "_" + range.ToString() + "_" + yaw.ToString();
                    bmp.Save(m_RunPArams.ctrlGenerateOutputToDir.FileName + "\\" + fileName + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);

                    double verFOV = 2 * Math.Atan(Math.Tan(activeCamera.GetCameraFieldOfView() * Math.PI / 180 / 2) * pHeight / pWidht) * 180 / Math.PI;

                    m_SW = new StreamWriter(m_RunPArams.ctrlGenerateOutputToDir.FileName + "\\" + fileName + ".ini");
                    m_SW.WriteLine("[Data]");
                    m_SW.WriteLine("Channel=Gunner Night"); //Channel
                    m_SW.WriteLine("North=0"); //North
                    m_SW.WriteLine("East=0"); //East
                    m_SW.WriteLine("Height=0"); //Height
                    m_SW.WriteLine("UTM_North=" + locationAfterConvertion.y.ToString()); //UTM_North
                    m_SW.WriteLine("UTM_East=" + locationAfterConvertion.x.ToString()); //UTM_East
                    m_SW.WriteLine("UTM_Height=" + locationAfterConvertion.z.ToString()); //UTM_Height
                    m_SW.WriteLine("HullPitch=0"); //HullPitch
                    m_SW.WriteLine("HullRoll=0"); //HullRoll
                    m_SW.WriteLine("HullCourse=0"); //HullCourse
                    m_SW.WriteLine("Azimuth=" + geoAzimuth.ToString()); //Azimuth that convert to GEO_WGS84
                    m_SW.WriteLine("Elevation=" + currPitch.ToString()); //Elevation
                    m_SW.WriteLine("Ver FOV=" + verFOV.ToString()); //Ver FOV
                    m_SW.WriteLine("Hor FOV=" + activeCamera.GetCameraFieldOfView().ToString()); //Hor FOV
                    m_SW.WriteLine("target pos=" + lookAtPointAfterConvertion.x.ToString() + "," + lookAtPointAfterConvertion.y.ToString() + "," + lookAtPointAfterConvertion.z.ToString()); //target pos

                    m_SW.Close();

                    //m_lPointsScale.Clear();
                    //for (int ptNum = 0; ptNum < m_lCheckPointInWorldCord.Count; ptNum++)
                    //{
                    //    m_lPointsScale.Add(activeCamera.GetCameraScale(m_lCheckPointInWorldCord[ptNum]));
                    //}

                    //if (range == -m_RunPArams.ntxDisMin.GetInt32())
                    //{
                    //    for (int t = 0; t < m_lPointsScale.Count; t++)
                    //    {
                    //        m_lStartPointsScale.Add(m_lPointsScale[t]);
                    //    }                         
                    //}
                    
                    ExportChackPoints(fileName);
                }
            }

            MCTMapFormManager.MapForm.Cursor = Cursors.Default;
        }


        private void ExportChackPoints(string FileName)
        {
            List<DNSMcVector3D> lCheckPointInScreenCord = new List<DNSMcVector3D>();
            List<double> lRanges = new List<double>();
            for(int i=0; i < m_lCheckPointInWorldCord.Count; i++)
            {
                try
                {
                    lRanges.Add((activeCamera.GetCameraPosition() - m_lCheckPointInWorldCord[i]).Length());
                    lCheckPointInScreenCord.Add(activeCamera.WorldToScreen(m_lCheckPointInWorldCord[i]));
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("WorldToScreen", McEx);
                }
            }

            m_PointsFile = new StreamWriter(m_RunPArams.ctrlGenerateOutputToDir.FileName + "\\" + FileName + ".txt");
            for(int i=0; i < lCheckPointInScreenCord.Count; i++)
            {
                //m_RadiusFactor = m_lPointsScale[i] / m_lStartPointsScale[i];
                m_PointsFile.WriteLine(lCheckPointInScreenCord[i].x.ToString() + "," + lCheckPointInScreenCord[i].y.ToString() + "," + Math.Abs(lRanges[i]).ToString() + "," + m_lPointsRadius[i].ToString());
            }

            m_PointsFile.Close();
        }
    }
}
