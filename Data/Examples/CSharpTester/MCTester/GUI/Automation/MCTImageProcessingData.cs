using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using MapCore;

namespace MCTester.Automation
{
    [DataContract]
    class MCTImageProcessingData
    {
        [DataMember]
        public DNEFilterProccessingOperation Filter { get; set; }

        [DataMember]
        public bool IsEnableColorTableImageProcessing { get; set; }

        [DataMember]
        public byte WhiteBalanceBrightnessR { get; set; }

        [DataMember]
        public byte WhiteBalanceBrightnessG { get; set; }

        [DataMember]
        public byte WhiteBalanceBrightnessB { get; set; }

        [DataMember]
        public MCTImageProcessingChannelData[] ChannelDatas { get; set; }

        [DataMember]
        public MCTImageProcessingCustomFilter CustomFilter { get; set; }


        public MCTImageProcessingData()
        {
            ChannelDatas = new MCTImageProcessingChannelData[4];
        }
    }
}
