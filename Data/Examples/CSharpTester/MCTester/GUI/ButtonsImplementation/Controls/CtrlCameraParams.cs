using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapCore;

namespace MCTester.Controls
{
    public partial class CtrlCameraParams : UserControl
    {
        public CtrlCameraParams()
        {
            InitializeComponent();
        }

        public void SetCameraParams(IDNMcImageCalc mcImageCalc)
        {
            DNSCameraParams frameCameraParams = new DNSCameraParams();
            frameCameraParams.dCameraOpeningAngleX = 45;
            frameCameraParams.dPixelRatio = 1;

            if (mcImageCalc != null && mcImageCalc.GetImageType() == DNEImageType._EIT_FRAME)
            {
                IDNMcFrameImageCalc mcFrameImageCalc = (IDNMcFrameImageCalc)mcImageCalc;
                if (mcFrameImageCalc != null)
                    frameCameraParams = mcFrameImageCalc.GetCameraParams();
            }
            SetCameraParams(frameCameraParams);
        }

        public void SetCameraParams(DNSCameraParams frameCameraParams)
        {
            ctrlCameraLocation.SetVector3D( frameCameraParams.CameraPosition);
            ntxCameraOpeningAngleX.SetDouble(frameCameraParams.dCameraOpeningAngleX);
            CameraOrientation.Yaw = (float)frameCameraParams.dCameraYaw;
            CameraOrientation.Pitch = (float)frameCameraParams.dCameraPitch;
            CameraOrientation.Roll = (float)frameCameraParams.dCameraRoll;
            ntxPixelRatio.SetDouble(frameCameraParams.dPixelRatio);
            ctrl2DVectorNumPixeles.SetVector2D(new DNSMcVector2D(frameCameraParams.nPixelesNumX, frameCameraParams.nPixelesNumY));
            ctrl2DFVectorOffsetCenterPixel.SetVector2D(new DNSMcVector2D(frameCameraParams.dOffsetCenterPixelX, frameCameraParams.dOffsetCenterPixelY));
        }

        public DNSCameraParams GetCameraParams()
        {
            DNSCameraParams frameCameraParams = new DNSCameraParams();
            frameCameraParams.CameraPosition = ctrlCameraLocation.GetVector3D();
            frameCameraParams.dCameraOpeningAngleX = ntxCameraOpeningAngleX.GetDouble();
            frameCameraParams.dCameraPitch = CameraOrientation.Pitch;
            frameCameraParams.dCameraRoll = CameraOrientation.Roll;
            frameCameraParams.dCameraYaw = CameraOrientation.Yaw;
            frameCameraParams.dPixelRatio = ntxPixelRatio.GetDouble();
            frameCameraParams.nPixelesNumX = (int)ctrl2DVectorNumPixeles.GetVector2D().x;
            frameCameraParams.nPixelesNumY = (int)ctrl2DVectorNumPixeles.GetVector2D().y;
            frameCameraParams.dOffsetCenterPixelX = ctrl2DFVectorOffsetCenterPixel.GetVector2D().x;
            frameCameraParams.dOffsetCenterPixelY = ctrl2DFVectorOffsetCenterPixel.GetVector2D().y;
            return frameCameraParams;
        }

    }
}
