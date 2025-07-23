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
    public partial class ucStaticObjectsLayer : ucLayer, IUserControlItem
    {
        IDNMcStaticObjectsMapLayer m_CurrentObject;

        public ucStaticObjectsLayer()
        {
            InitializeComponent();

            listViewports.DisplayMember = "ViewportsTextList";
            listViewports.ValueMember = "ViewportsValueList";

            chxDisplayingItemsAttached.CheckState = CheckState.Indeterminate;
            chxSetDisplayingDtmVisualization.CheckState = CheckState.Indeterminate;
        }

        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcStaticObjectsMapLayer)aItem;
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
            listViewports.Items.AddRange(ViewportsTextList.ToArray());
        }

        private void btnSetDisplayingItemsAttachedToTerrain_Click(object sender, EventArgs e)
        {
            bool isSetDisplayingItemsAttachedToTerrain = chxDisplayingItemsAttached.CheckState == CheckState.Checked;
            try
            {
                if (listViewports.SelectedIndex >= 0)
                {
                    foreach (int i in listViewports.SelectedIndices)
                    {
                        m_CurrentObject.SetDisplayingItemsAttachedToTerrain(isSetDisplayingItemsAttachedToTerrain, GetViewportsValueList()[i]);
                    }
                }
                else
                    m_CurrentObject.SetDisplayingItemsAttachedToTerrain(isSetDisplayingItemsAttachedToTerrain);

                // turn on all viewports render needed flags
                Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetDisplayingItemsAttachedToTerrain", McEx);
            }
        }

        private void btnSetDisplayingDtmVisualization_Click(object sender, EventArgs e)
        {
            bool isSetDisplayingItemsDtmVisualization = chxSetDisplayingDtmVisualization.CheckState == CheckState.Checked;

            try
            {
                if (listViewports.SelectedIndex >= 0)
                {
                    foreach (int i in listViewports.SelectedIndices)
                    {
                        m_CurrentObject.SetDisplayingDtmVisualization(isSetDisplayingItemsDtmVisualization, GetViewportsValueList()[i]);
                    }
                }
                else
                    m_CurrentObject.SetDisplayingDtmVisualization(isSetDisplayingItemsDtmVisualization);

                // turn on all viewports render needed flags
                Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetDisplayingDtmVisualization", McEx);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            listViewports.ClearSelected();
        }
    }
}
