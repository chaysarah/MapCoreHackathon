using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using MapCore.Common;

namespace MCTester.MapWorld.MapUserControls
{
    public partial class ucRasterRawLayer : ucRasterLayer, IUserControlItem
    {
        private IDNMcRawRasterMapLayer m_CurrentObject;

        public ucRasterRawLayer()
        {
            InitializeComponent();

        }

        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcRawRasterMapLayer)aItem;
            base.LoadItem(aItem);

            ctrlRawComponents1.SetMCObject(m_CurrentObject);

            string[] channels = new string[3];
            channels[0] = "Default (original channel)";
            channels[1] = "Channel defined by index";
            channels[2] = "Channel unused";

            cmbRChannel.Items.AddRange(channels);
            cmbBChannel.Items.AddRange(channels);
            cmbGChannel.Items.AddRange(channels);

            try
            {
                uint puRChannelIndex, puGChannelIndex, puBChannelIndex;
                m_CurrentObject.GetColorChannels(out puRChannelIndex, out puGChannelIndex, out puBChannelIndex);
                
                SetRChannels(puRChannelIndex);
                SetGChannels(puGChannelIndex);
                SetBChannels(puBChannelIndex);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetColorChannels", McEx);
            }
        }
        #endregion

        private void SetRChannels(uint RChannelIndex)
        {
            cmbRChannel.SelectedIndex = RChannelIndex == DNMcConstants._MC_EMPTY_ID ? 0 : RChannelIndex == DNMcConstants._MC_EMPTY_ID - 1 ? 2 : 1;
            if (cmbRChannel.SelectedIndex == 1)
                ntbRChannelIndex.SetUInt32(RChannelIndex);
        }

        private void SetGChannels(uint GChannelIndex, bool isEmpty = false)
        {
            cmbGChannel.SelectedIndex = GChannelIndex == DNMcConstants._MC_EMPTY_ID ? 0 : GChannelIndex == DNMcConstants._MC_EMPTY_ID - 1 ? 2 : 1;
            if (cmbGChannel.SelectedIndex == 1)
                ntbGChannelIndex.SetUInt32(GChannelIndex);
        }

        private void SetBChannels(uint BChannelIndex, bool isEmpty = false)
        {
            cmbBChannel.SelectedIndex = BChannelIndex == DNMcConstants._MC_EMPTY_ID ? 0 : BChannelIndex == DNMcConstants._MC_EMPTY_ID - 1 ? 2 : 1;
            if (cmbBChannel.SelectedIndex == 1)
                ntbBChannelIndex.SetUInt32(BChannelIndex);
        }

        protected override void SaveItem()
        {
            base.SaveItem();
        }

        private void btnAddComponents_Click(object sender, EventArgs e)
        {
            try
            {
                List<DNSComponentParams> compParamsList = ctrlRawRasterComponentParams1.GetComponentParamsList();
                m_CurrentObject.AddComponents(compParamsList.ToArray());
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("AddComponents", McEx);
            }
        }

        private void btnColorChannelsSet_Click(object sender, EventArgs e)
        {
            try
            {
                uint r = 0;
                switch(cmbRChannel.SelectedIndex)
                {
                    case 0:
                        r = DNMcConstants._MC_EMPTY_ID; break;
                    case 1:
                        r = ntbRChannelIndex.GetUInt32(); break;
                    case 2:
                        r = DNMcConstants._MC_EMPTY_ID -1; break;

                }
                uint g = 0;
                switch (cmbGChannel.SelectedIndex)
                {
                    case 0:
                        g = DNMcConstants._MC_EMPTY_ID; break;
                    case 1:
                        g = ntbGChannelIndex.GetUInt32(); break;
                    case 2:
                        g = DNMcConstants._MC_EMPTY_ID - 1; break;

                }

                uint b = 0;
                switch (cmbBChannel.SelectedIndex)
                {
                    case 0:
                        b = DNMcConstants._MC_EMPTY_ID; break;
                    case 1:
                        b = ntbBChannelIndex.GetUInt32(); break;
                    case 2:
                        b = DNMcConstants._MC_EMPTY_ID - 1; break;

                }

                m_CurrentObject.SetColorChannels(r, g, b);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetColorChannels", McEx);
            }
        }

        private void cmbRChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            ntbRChannelIndex.Enabled = cmbRChannel.SelectedIndex == 1;
            if (!ntbRChannelIndex.Enabled)
                ntbRChannelIndex.Text = "";
        }

        private void cmbGChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            ntbGChannelIndex.Enabled = cmbGChannel.SelectedIndex == 1;
            if (!ntbGChannelIndex.Enabled)
                ntbGChannelIndex.Text = "";
        }

        private void cmbBChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            ntbBChannelIndex.Enabled = cmbBChannel.SelectedIndex == 1;
            if (!ntbBChannelIndex.Enabled)
                ntbBChannelIndex.Text = "";
        }
    }
}
