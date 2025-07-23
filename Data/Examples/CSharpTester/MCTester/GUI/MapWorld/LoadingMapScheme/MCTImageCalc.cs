using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using MCTester.Managers.MapWorld;
using UnmanagedWrapper;

namespace MCTester.MapWorld.LoadingMapScheme
{
    [Serializable]
    public class MCTImageCalc
    {
        private int m_ImageCalcID;
        private DNSMcVector3D m_CameraLocation;
        private double m_CameraOpeningAngleX;
        private double m_CameraYaw;
        private double m_CameraPitch;
        private double m_CameraRoll;
        private double m_PixelRatio;
        private int m_PixelsNumX;
        private int m_PixelsNumY;
        private double m_OffsetCenterPixelX;
        private double m_OffsetCenterPixelY;
        private string m_XmlPath;
        private string m_ImageType;
        private bool m_IsFileName = true;
        private string m_DtmMapLayerPath;
        private int m_DTMMapLayerID;
        private IDNMcDtmMapLayer m_DtmMapLayer = null;
        private string m_Name;
        private int m_CoordSysID;

        public MCTImageCalc()
        {
            DNSDatumParams datumParams = new DNSDatumParams();            
        }


        #region Public Properties
        public int ID
        {
            get { return m_ImageCalcID; }
            set { m_ImageCalcID = value; }
        }

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public string ImageType
        {
            get { return m_ImageType; }
            set { m_ImageType = value; }
        }

        public string DtmMapLayerPath
        {
            get { return m_DtmMapLayerPath; }
            set { m_DtmMapLayerPath = value; }
        }

        public int DTMMapLayerID
        {
            get { return m_DTMMapLayerID; }
            set { m_DTMMapLayerID = value; }
        }

        public int PixelsNumX
        {
            get { return m_PixelsNumX; }
            set { m_PixelsNumX = value; }
        }

        public double OffsetCenterPixelX
        {
            get { return m_OffsetCenterPixelX; }
            set { m_OffsetCenterPixelX = value; }
        }

        public double OffsetCenterPixelY
        {
            get { return m_OffsetCenterPixelY; }
            set { m_OffsetCenterPixelY = value; }
        }

        public int PixelsNumY
        {
            get { return m_PixelsNumY; }
            set { m_PixelsNumY = value; }
        }

        public double PixelRatio
        {
            get { return m_PixelRatio; }
            set { m_PixelRatio = value; }
        } 

        public double OpeningAngleX
        {
            get { return m_CameraOpeningAngleX; }
            set { m_CameraOpeningAngleX = value; }
        }  

        public DNSMcVector3D Location
        {
            get { return m_CameraLocation;}
            set { m_CameraLocation = value; }
        }     

        public double Yaw
        {
            get { return m_CameraYaw; }
            set { m_CameraYaw = value; }
        }

        public double Pitch
        {
            get { return m_CameraPitch; }
            set { m_CameraPitch = value; }
        }

        public double Roll
        {
            get { return m_CameraRoll; }
            set { m_CameraRoll = value; }
        }      

        public int CoordSysID
        {
            get { return m_CoordSysID; }
            set { m_CoordSysID = value; }   
        }

        public bool IsFileName
        {
            get { return m_IsFileName; }
            set { m_IsFileName = value; }
        }

        public string XmlPath
        {
            get { return m_XmlPath; }
            set { m_XmlPath = value; }
        }
        
        #endregion

        public IDNMcImageCalc CreateImageCalc()
        {
            IDNMcImageCalc imageCalc = null;

            MCTGridCoordinateSystem gridCoordSys = MainForm.MapLoaderDefinitionManager.GridCoordynateSystems.GetCoordSys(CoordSysID);
            IDNMcGridCoordinateSystem imageCoordSys = gridCoordSys.GetGridCoordSys();

            if (DtmMapLayerPath != "")
            {
                try
                {
                    m_DtmMapLayer = (IDNMcDtmMapLayer)Manager_MCLayers.CreateNativeDTMLayer(DtmMapLayerPath, 0, null, false);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("DNMcNativeDtmMapLayer.Create", McEx);
                }
            }
            else if (m_DTMMapLayerID > 0)
            {
                MCTMapLayer mctDtmLayer = MainForm.MapLoaderDefinitionManager.Layers.GetLayer(m_DTMMapLayerID);
                switch(mctDtmLayer.LayerType)
                {
                    case DNELayerType._ELT_NATIVE_DTM:
                        m_DtmMapLayer = mctDtmLayer.CreateNativeDTMLayer();
                        break;
                    case DNELayerType._ELT_NATIVE_SERVER_DTM:
                        m_DtmMapLayer = mctDtmLayer.CreateNativeServerDTM();
                        break;
                    case DNELayerType._ELT_RAW_DTM:
                        m_DtmMapLayer = mctDtmLayer.CreateRawDtmLayer(imageCoordSys);
                        break;
                    default:
                        break;
                }
            }

            switch(ImageType)
            {
                case "_EIT_FRAME":
                    try
                    {
                        DNSCameraParams cameraParams = new DNSCameraParams();
                        cameraParams.CameraPosition = Location;
                        cameraParams.dCameraOpeningAngleX = OpeningAngleX;
                        cameraParams.dCameraYaw = Yaw;
                        cameraParams.dCameraPitch = Pitch;
                        cameraParams.dCameraRoll = Roll;
                        cameraParams.dPixelRatio = PixelRatio;
                        cameraParams.nPixelesNumX = PixelsNumX;
                        cameraParams.nPixelesNumY = PixelsNumY;
                        cameraParams.dOffsetCenterPixelX = OffsetCenterPixelX;
                        cameraParams.dOffsetCenterPixelY = OffsetCenterPixelY;
                         
                        imageCalc = DNMcFrameImageCalc.Create(ref cameraParams,
                                                                    m_DtmMapLayer,
                                                                    imageCoordSys);

                        Managers.MapWorld.Manager_MCImageCalc.AddToDictionary(imageCalc);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("DNMcFrameImageCalc.Create", McEx);
                    }                    
                    break;
                case "_EIT_LOROP":
                    try
                    {
                        imageCalc = DNMcLoropImageCalc.Create(XmlPath,
                                                                IsFileName,
                                                                m_DtmMapLayer,
                                                                imageCoordSys);

                        Managers.MapWorld.Manager_MCImageCalc.AddToDictionary(imageCalc);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("DNMcLoropImageCalc.Create", McEx);
                    }
                    break;
            }

            return imageCalc;
        }
    }
}
