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
    class MCTImageProcessingChannelData
    {
        [DataMember]
        public DNEColorChannel Channel { get; set; }

        [DataMember]
        public byte[] UserColorValues { get; set; }

        [DataMember]
        public bool UserColorValuesUse { get; set; }

        [DataMember]
        public double Brightness { get; set; }

        [DataMember]
        public double Contrast { get; set; }

        [DataMember]
        public bool Negative { get; set; }

        [DataMember]
        public double Gamma { get; set; }

        [DataMember]
        public bool HistogramEqualization{ get; set; }

        [DataMember]
        public Int64[] ReferenceHistogram{ get; set; }

        [DataMember]
        public bool VisibleAreaOriginalHistogram { get; set; }

        [DataMember]
        public bool ReferenceHistogramUse { get; set; }

        [DataMember]
        public bool IsOriginalHistogramSet { get; set; }


        [DataMember]
        public Int64[] OriginalHistogram { get; set; }

        [DataMember]
        public bool HistogramNormalizationUse{ get; set; }

        [DataMember]
        public double HistogramNormalizationMean{ get; set; }

        [DataMember]
        public double HistogramNormalizationStdev{ get; set; }

        public MCTImageProcessingChannelData()
        {
            
        }
    }
}
