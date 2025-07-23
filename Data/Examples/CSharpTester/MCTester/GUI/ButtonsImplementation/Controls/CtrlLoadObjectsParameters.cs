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
using MCTester.GUI.Map;
using MCTester.Managers.ObjectWorld;
using MCTester.Managers;

namespace MCTester.Controls
{
    public partial class CtrlLoadObjectsParameters : UserControl
    {
        ILoadObjectsParameters m_ucLoadObjectsParameters;

        public CtrlLoadObjectsParameters()
        {
            InitializeComponent();

            cmbStorageFormatAfterLoad.Items.AddRange(Enum.GetNames(typeof(DNEStorageFormat)));

            chxIsShowVersion_CheckedChanged(null, null);
            chxIsShowStorageFormat_CheckedChanged(null, null);
        }

        public void SetUI(bool LoadIsShowVersion, bool IsLoadIsShowStorageFormat, string LoadVersion, string LoadStorageFormat, ILoadObjectsParameters ucLoadObjectsParameters)
        {
            chxIsShowVersion.Checked = LoadIsShowVersion;
            chxIsShowStorageFormat.Checked = IsLoadIsShowStorageFormat;

            ntxVersion2.Text = LoadVersion;
            cmbStorageFormatAfterLoad.Text = LoadStorageFormat;

            m_ucLoadObjectsParameters = ucLoadObjectsParameters;
        }

        public void ShowLoadParameters(string sVersion, string sStorageFormat)
        {
            ntxVersion2.Text = sVersion;
            cmbStorageFormatAfterLoad.Text = sStorageFormat;
        }

        private void chxIsShowVersion_CheckedChanged(object sender, EventArgs e)
        {
            //m_LoadIsShowVersion = chxIsShowVersion.Checked;
            if(m_ucLoadObjectsParameters != null)
                m_ucLoadObjectsParameters.SetLoadIsShowVersion(chxIsShowVersion.Checked);

            chxIsShowVersion.ForeColor = chxIsShowVersion.Checked ? Color.Black : Color.Gray;

        }

        private void chxIsShowStorageFormat_CheckedChanged(object sender, EventArgs e)
        {
            if (m_ucLoadObjectsParameters != null)
                m_ucLoadObjectsParameters.SetLoadIsShowStorageFormat(chxIsShowStorageFormat.Checked);
            chxIsShowStorageFormat.ForeColor = chxIsShowStorageFormat.Checked ? Color.Black : Color.Gray;
        }

        internal bool GetIsShowVersion()
        {
            return chxIsShowVersion.Checked;
        }

        internal bool GetIsShowStorageFormat()
        {
            return chxIsShowStorageFormat.Checked;
        }
    }
}
