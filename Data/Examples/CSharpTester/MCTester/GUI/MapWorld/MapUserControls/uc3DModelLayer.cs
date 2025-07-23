using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MapCore.Common;
using MCTester.Managers.MapWorld;

namespace MCTester.MapWorld.MapUserControls
{
    public partial class uc3DModelLayer : ucStaticObjectsLayer, IUserControlItem
    {
        IDNMc3DModelMapLayer m_CurrentObject;

        public uc3DModelLayer()
        {
            InitializeComponent();

            listViewports3DModel.DisplayMember = "ViewportsTextList";
            listViewports3DModel.ValueMember = "ViewportsValueList";

            chxResolvingConflictsWithDtmAndRaster.CheckState = CheckState.Indeterminate;
        }

        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMc3DModelMapLayer)aItem;
            base.LoadItem(aItem);
            LoadViewportsToListBox();
        }

        protected override void SaveItem()
        {
            base.SaveItem();
        }
        #endregion

        private void LoadViewportsToListBox()
        {
            listViewports3DModel.Items.AddRange(ViewportsTextList.ToArray());
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            listViewports3DModel.ClearSelected();
        }

        private void btnResolutionFactor_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewports3DModel.SelectedIndex >= 0)
                {
                    foreach (int i in listViewports3DModel.SelectedIndices)
                    {
                        m_CurrentObject.SetResolutionFactor(ntbResolutionFactor1.GetFloat(), GetViewportsValueList()[i]);
                    }
                }
                else
                    m_CurrentObject.SetResolutionFactor(ntbResolutionFactor1.GetFloat());

                // turn on all viewports render needed flags
                Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetResolutionFactor", McEx);
            }
        }

        private void btnResolvingConflictsWithDtmAndRaster_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewports3DModel.SelectedIndex >= 0)
                {
                    foreach (int i in listViewports3DModel.SelectedIndices)
                    {
                        m_CurrentObject.SetResolvingConflictsWithDtmAndRaster(chxResolvingConflictsWithDtmAndRaster1.Checked, GetViewportsValueList()[i]);
                    }
                }
                else
                    m_CurrentObject.SetResolvingConflictsWithDtmAndRaster(chxResolvingConflictsWithDtmAndRaster1.Checked);

                // turn on all viewports render needed flags
                Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetResolvingConflictsWithDtmAndRaster", McEx);
            }
        }
    }
}
