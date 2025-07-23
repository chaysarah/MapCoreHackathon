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
using System.IO;

namespace MCTester.Controls
{
    public partial class CtrlRawRasterComponentParams : UserControl
    {
        private List<DNSComponentParams> m_CompParamsList;
        private string m_BaseDirectory = "";

        public CtrlRawRasterComponentParams()
        {
            InitializeComponent();

            m_CompParamsList = new List<DNSComponentParams>();

            cmbComponentType.Items.AddRange(Enum.GetNames(typeof(DNEComponentType)));
            cmbComponentType.Text = DNEComponentType._ECT_FILE.ToString();

            browseLayerCtrl.FileNameSelected += BrowseLayerCtrl_FileNameSelected;

       

            
        }

        private void BrowseLayerCtrl_FileNameSelected(object sender, EventArgs e)
        {
            if (m_BaseDirectory != "")
            {
                browseLayerCtrl.FileName = browseLayerCtrl.FileName.Replace(m_BaseDirectory, "");
                if (browseLayerCtrl.FileName.StartsWith("\\"))
                    browseLayerCtrl.FileName = browseLayerCtrl.FileName.Substring(1);
            }
        }  

        private void btnAddComponentParams_Click(object sender, EventArgs e)
        {
            m_CompParamsList.Add(GetComponentParams());
            lstComponentParams.Items.Add(GetComponentType().ToString() + "-" + GetComponentName());
        }

        public void SetBaseDirectory(string baseDir)
        {
            m_BaseDirectory = baseDir;
        }

        public string GetComponentName()
        {
            return browseLayerCtrl.FileName;
        }

        public List<DNSComponentParams> GetComponentParamsList()
        {
            return m_CompParamsList;
        }

        public void SetComponentParamsList(List<DNSComponentParams> value)
        {
            lstComponentParams.Items.Clear();
            m_CompParamsList = value;
            foreach(DNSComponentParams param in value)
                lstComponentParams.Items.Add(param.eType.ToString() + "-" + param.strName);
        }

        public DNSComponentParams GetComponentParams()
        {
            DNSComponentParams m_ComponentParams = new DNSComponentParams();

            m_ComponentParams.eType = GetComponentType();
            m_ComponentParams.strName = browseLayerCtrl.FileName;
            m_ComponentParams.WorldLimit.MinVertex = GetMinWorldLimit();
            m_ComponentParams.WorldLimit.MaxVertex = GetMaxWorldLimit();

            return m_ComponentParams;
        }

        public void SetDefaultValues()
        {
            cmbComponentType.Text = DNEComponentType._ECT_FILE.ToString();
            browseLayerCtrl.FileName = "";
            ctrl2DMinWorldBoundingBox.SetEmptyValue();
            ctrl2DMaxWorldBoundingBox.SetEmptyValue();
            SetComponentParamsList(new List<DNSComponentParams>());
        }

        public DNEComponentType GetComponentType()
        {
            return (DNEComponentType)Enum.Parse(typeof(DNEComponentType), cmbComponentType.Text);
        }

        public DNSMcVector3D GetMinWorldLimit()
        {
            return ctrl2DMinWorldBoundingBox.GetVector2D();
        }

        public DNSMcVector3D GetMaxWorldLimit()
        {
            return ctrl2DMaxWorldBoundingBox.GetVector2D();
        }

        private void cmbComponentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            browseLayerCtrl.IsFolderDialog = (GetComponentType() == DNEComponentType._ECT_DIRECTORY);
        }

        private void mnuItemRemove_Click(object sender, EventArgs e)
        {
            m_CompParamsList.RemoveAt(lstComponentParams.SelectedIndex);
            lstComponentParams.Items.RemoveAt(lstComponentParams.SelectedIndex);
        }

        private void mnuComponentItems_Opening(object sender, CancelEventArgs e)
        {
            mnuItemRemove.Enabled = lstComponentParams.SelectedIndex >= 0;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            m_CompParamsList.Clear();
            lstComponentParams.Items.Clear();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public void HideAddToListBtn()
        {
            btnAddComponentParams.Visible = false;
            btnClear.Visible = false;

            /*cmbComponentType.Enabled = false;
            ctrl3DMaxWorldBoundingBox.Enabled = false;
            ctrl3DMinWorldBoundingBox.Enabled = false;
            browseLayerCtrl.Enabled = false;*/
        }
    }
}
