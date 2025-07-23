using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;


namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class CtrlObjectSchemeItem : MCTester.ObjectWorld.ObjectsUserControls.ucObjectSchemeNode,IUserControlItem
    {
        private IDNMcObjectSchemeItem m_CurrentObject;
        // private uint PropID;
        //private byte m_byteParam;

        public CtrlObjectSchemeItem():base()
        {
            InitializeComponent();
        }
        
        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcObjectSchemeItem)aItem;
            base.LoadItem(aItem);

            try
            {
                DNEItemSubTypeFlags[] itemSubTypeFlags = ((DNEItemSubTypeFlags[])Enum.GetValues(typeof(DNEItemSubTypeFlags)));
                foreach (DNEItemSubTypeFlags subType in itemSubTypeFlags)
                    clstItemSubTypeBitField.Items.Add(subType);

                DNEItemSubTypeFlags itemsubtype = m_CurrentObject.GetItemSubType();
                int index = 0;

                foreach (DNEItemSubTypeFlags flag in itemSubTypeFlags)
                {
                    if ((itemsubtype & flag) == flag)
                        clstItemSubTypeBitField.SetItemChecked(index, true);

                    ++index;
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetItemSubType", McEx);
            }

            try
            {
                chxIsDetectibility.Checked = m_CurrentObject.Detectibility;
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Detectibility", McEx);
            }

            try
            {
                chxHiddenIfViewportOverloaded.Checked = m_CurrentObject.HiddenIfViewportOverloaded;
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("HiddenIfViewportOverloaded", McEx);
            }

            try
            {
                ctrlObjStatePropertyBlockedTransparency.Load(m_CurrentObject.GetBlockedTransparency);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetBlockedTransparency", McEx);
            }

            try
            {
                chxParticipationInSightQueries.Checked = m_CurrentObject.GetParticipationInSightQueries();	
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetParticipationInSightQueries", McEx);
            }
        }

        #endregion

        protected override void SaveItem()
        {
            base.SaveItem();

            try
            {
                m_CurrentObject.HiddenIfViewportOverloaded = chxHiddenIfViewportOverloaded.Checked;

            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("HiddenIfViewportOverloaded", McEx);
            }

            try
            {
                m_CurrentObject.Detectibility = chxIsDetectibility.Checked;

            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Detectibility", McEx);
            }

            try
            {
                ctrlObjStatePropertyBlockedTransparency.Save(m_CurrentObject.SetBlockedTransparency);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetBlockedTransparency", McEx);
            }

            try
            {
                m_CurrentObject.SetParticipationInSightQueries(chxParticipationInSightQueries.Checked);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetParticipationInSightQueries", McEx);
            }

            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }

        private void CtrlObjectSchemeItem_Load(object sender, EventArgs e)
        {

        }
    }
}
