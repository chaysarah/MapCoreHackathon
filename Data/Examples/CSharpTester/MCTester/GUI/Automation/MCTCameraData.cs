using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace MCTester.Automation
{
    [DataContract]
    class MCTCameraData
    {
        [DataMember]
        public MCTCameraClipDistances CameraClipDistances { get; set; }

        [DataMember]
        public MCTCameraPosition CameraPosition { get; set; }

        [DataMember]
        public MCTCameraOrientation CameraOrientation { get; set; }

        [DataMember]
        public float CameraScale { get; set; }

        [DataMember]
        public float CameraFieldOfView { get; set; }

        public MCTCameraData()
        {
            CameraClipDistances = new MCTCameraClipDistances();
            CameraPosition = new MCTCameraPosition();
            CameraOrientation = new MCTCameraOrientation();
        }
    }

    [DataContract]
    class MCTCameraClipDistances
    {
        [DataMember]
        public float Min { get; set; }

        [DataMember]
        public float Max { get; set; }

        [DataMember]
        public bool RenderInTwoSessions { get; set; }

    }


    [DataContract]
    class MCTCameraPosition
    {
        [DataMember]
        public double X { get; set; }

        [DataMember]
        public double Y { get; set; }

        [DataMember]
        public double Z { get; set; }
    }

    [DataContract]
    class MCTCameraOrientation
    {
        [DataMember]
        public float Yaw { get; set; }

        [DataMember]
        public float Pitch { get; set; }

        [DataMember]
        public float Roll { get; set; }
    }
}
