using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.ObjectWorld.ObjectsUserControls.ConditionalSelectorForms
{
    public partial class ucScaleConditionalSelector : ucBaseConditionalSelector, IUserControlItem
    {
        private IDNMcScaleConditionalSelector m_CurrentCondSelector;

        private uint MASK0 = 1;
        private uint MASK1 = 2;
        private uint MASK2 = 4;
        private uint MASK3 = 8;
        private uint MASK4 = 16;
        private uint MASK5 = 32;
        private uint MASK6 = 64;
        private uint MASK7 = 128;
        private uint MASK8 = 256;
        private uint MASK9 = 512;

        public ucScaleConditionalSelector()
        {
            InitializeComponent();
        }

        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentCondSelector = (IDNMcScaleConditionalSelector)aItem;
            base.LoadItem(aItem);

            try
            {
                ntxMinScale.SetFloat(m_CurrentCondSelector.MinScale);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("MinScale", McEx);
            }

            try
            {
                if (m_CurrentCondSelector.MaxScale == float.MaxValue)
                    ntxMaxScale.SetString("MAX");
                else
                    ntxMaxScale.SetFloat(m_CurrentCondSelector.MaxScale);                
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("MaxScale", McEx);
            }

            try
            {
                uint scaleMode = m_CurrentCondSelector.CancelScaleMode;

                chxCancelScale0.Checked = ((scaleMode & MASK0) > 0) ? true : false;
                chxCancelScale1.Checked = ((scaleMode & MASK1) > 0) ? true : false;
                chxCancelScale2.Checked = ((scaleMode & MASK2) > 0) ? true : false;
                chxCancelScale3.Checked = ((scaleMode & MASK3) > 0) ? true : false;
                chxCancelScale4.Checked = ((scaleMode & MASK4) > 0) ? true : false;
                chxCancelScale5.Checked = ((scaleMode & MASK5) > 0) ? true : false;
                chxCancelScale6.Checked = ((scaleMode & MASK6) > 0) ? true : false;
                chxCancelScale7.Checked = ((scaleMode & MASK7) > 0) ? true : false;
                chxCancelScale8.Checked = ((scaleMode & MASK8) > 0) ? true : false;
                chxCancelScale9.Checked = ((scaleMode & MASK9) > 0) ? true : false;

            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("CancelScaleMode", McEx);
            }

            try
            {
                uint scaleModeResult = m_CurrentCondSelector.CancelScaleModeResult;

                chxCancelScaleResult0.Checked = ((scaleModeResult & MASK0) > 0) ? true : false;
                chxCancelScaleResult1.Checked = ((scaleModeResult & MASK1) > 0) ? true : false;
                chxCancelScaleResult2.Checked = ((scaleModeResult & MASK2) > 0) ? true : false;
                chxCancelScaleResult3.Checked = ((scaleModeResult & MASK3) > 0) ? true : false;
                chxCancelScaleResult4.Checked = ((scaleModeResult & MASK4) > 0) ? true : false;
                chxCancelScaleResult5.Checked = ((scaleModeResult & MASK5) > 0) ? true : false;
                chxCancelScaleResult6.Checked = ((scaleModeResult & MASK6) > 0) ? true : false;
                chxCancelScaleResult7.Checked = ((scaleModeResult & MASK7) > 0) ? true : false;
                chxCancelScaleResult8.Checked = ((scaleModeResult & MASK8) > 0) ? true : false;
                chxCancelScaleResult9.Checked = ((scaleModeResult & MASK9) > 0) ? true : false;
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("CancelScaleModeResult", McEx);
            }
        }

        #endregion

        protected override void Save()
        {
            base.Save();

            try
            {
                m_CurrentCondSelector.MinScale = ntxMinScale.GetFloat();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("MinScale", McEx);
            }

            try
            {
                if (String.Compare(ntxMaxScale.Text, "MAX", true) == 0)
                    m_CurrentCondSelector.MaxScale = float.MaxValue;
                else
                    m_CurrentCondSelector.MaxScale = ntxMaxScale.GetFloat();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("MaxScale", McEx);
            }

            try
            {
                uint scaleMode = 0;

                scaleMode += (chxCancelScale0.Checked == true) ? MASK0 : 0;
                scaleMode += (chxCancelScale1.Checked == true) ? MASK1 : 0;
                scaleMode += (chxCancelScale2.Checked == true) ? MASK2 : 0;
                scaleMode += (chxCancelScale3.Checked == true) ? MASK3 : 0;
                scaleMode += (chxCancelScale4.Checked == true) ? MASK4 : 0;
                scaleMode += (chxCancelScale5.Checked == true) ? MASK5 : 0;
                scaleMode += (chxCancelScale6.Checked == true) ? MASK6 : 0;
                scaleMode += (chxCancelScale7.Checked == true) ? MASK7 : 0;
                scaleMode += (chxCancelScale8.Checked == true) ? MASK8 : 0;
                scaleMode += (chxCancelScale9.Checked == true) ? MASK9 : 0;

                m_CurrentCondSelector.CancelScaleMode = scaleMode;
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("CancelScaleMode", McEx);
            }

            try
            {
                uint scaleModeResult = 0;

                scaleModeResult += (chxCancelScaleResult0.Checked == true) ? MASK0 : 0;
                scaleModeResult += (chxCancelScaleResult1.Checked == true) ? MASK1 : 0;
                scaleModeResult += (chxCancelScaleResult2.Checked == true) ? MASK2 : 0;
                scaleModeResult += (chxCancelScaleResult3.Checked == true) ? MASK3 : 0;
                scaleModeResult += (chxCancelScaleResult4.Checked == true) ? MASK4 : 0;
                scaleModeResult += (chxCancelScaleResult5.Checked == true) ? MASK5 : 0;
                scaleModeResult += (chxCancelScaleResult6.Checked == true) ? MASK6 : 0;
                scaleModeResult += (chxCancelScaleResult7.Checked == true) ? MASK7 : 0;
                scaleModeResult += (chxCancelScaleResult8.Checked == true) ? MASK8 : 0;
                scaleModeResult += (chxCancelScaleResult9.Checked == true) ? MASK9 : 0;


                m_CurrentCondSelector.CancelScaleModeResult = scaleModeResult;
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("CancelScaleModeResult", McEx);
            }

            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }

        public IDNMcScaleConditionalSelector ScaleCondSelector
        {
            get { return m_CurrentCondSelector; }
            set { m_CurrentCondSelector = value; }
        }
    }
}
