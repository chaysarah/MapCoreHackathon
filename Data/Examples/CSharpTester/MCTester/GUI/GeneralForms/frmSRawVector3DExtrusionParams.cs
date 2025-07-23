using MapCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCTester.General_Forms
{
    public partial class frmSRawVector3DExtrusionParams : Form
    {
        public frmSRawVector3DExtrusionParams()
        {
            InitializeComponent();
        }

        public void SetLayerFileName(string fileName)
        {
            ctrlRawVector3DExtrusionParams1.SetLayerFileName(fileName);
        }

        public bool GetIsUseIndexing()
        {
            return ctrlRawVector3DExtrusionParams1.GetIsUseIndexing();
        }

       /* public void SetIsUseIndexing(bool isUseIndexingBuildingData)
        {
            ctrlRawVector3DExtrusionParams1.SetIsUseIndexing(isUseIndexingBuildingData);
        }*/

        public DNSRawVector3DExtrusionParams ExtrusionParams
        {
            get { return ctrlRawVector3DExtrusionParams1.GetExtrusionParams(); }
            set { ctrlRawVector3DExtrusionParams1.SetExtrusionParams(value); }
        }

        public DNSRawVector3DExtrusionGraphicalParams ExtrusionGraphicalParams
        {
            get { return ctrlRawVector3DExtrusionParams1.GetExtrusionGraphicalParams(); }
            set { ctrlRawVector3DExtrusionParams1.SetExtrusionGraphicalParams(value); }
        }

        public string GetIndexingDataDirectory()
        {
            return ctrlRawVector3DExtrusionParams1.GetIndexingDataDirectory();
        }

        public void SetIndexingData(bool isUseIndexingBuildingData, string strIndexingData)
        {
            ctrlRawVector3DExtrusionParams1.SetIndexingData(isUseIndexingBuildingData, strIndexingData, strIndexingData != "") ;
        }

        public frmSRawVector3DExtrusionParams(string fileName, DNSRawVector3DExtrusionParams extrusionParams): this()
        {
            SetLayerFileName(fileName);
            ExtrusionParams = extrusionParams;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ctrlRawVector3DExtrusionParams1.CheckValidity())
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
