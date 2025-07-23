using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.GUI.Trees;
using MapCore.Common;

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class ucSightPresentationItemParams : UserControl ,IUserControlItem
    {
        private IDNMcSightPresentationItemParams m_CurrentObject;
		
        public ucSightPresentationItemParams()
        {
            InitializeComponent();
            cbSightNoDTMResult.Items.AddRange(Enum.GetNames(typeof(DNENoDTMResult)));
        }

        #region IUserControlItem Members
        
        public void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcSightPresentationItemParams)aItem;
            try
            {
                ctrlObjStatePropertySightColor.Load(m_CurrentObject.GetSightColor);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetSightColor", McEx);
            }

            try
            {
                ctrlObjStatePropertySightPresentationType.Load(m_CurrentObject.GetSightPresentationType);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetSightPresentationType", McEx);
            }

            try
            {
                ctrlObjStatePropertySightObserverHeight.Load(m_CurrentObject.GetSightObserverHeight);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetSightObserverHeight", McEx);
            }

            try
            {
                ctrlObjStatePropertySightObservedHeight.Load(m_CurrentObject.GetSightObservedHeight);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetSightObservedHeight", McEx);
            }

            try
            {

                ctrlObjStatePropertySightObserverMinPitch.Load(m_CurrentObject.GetSightObserverMinPitch);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetSightObserverMinPitch", McEx);
            }

            try
            {
                ctrlObjStatePropertySightObserverMaxPitch.Load(m_CurrentObject.GetSightObserverMaxPitch);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetSightObserverMaxPitch", McEx);
            }            

            try
            {
                ctrlObjStatePropertySightObservedHeightAbsolute.Load(m_CurrentObject.GetIsSightObservedHeightAbsolute);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetIsSightObservedHeightAbsolute", McEx);
            }

            try
            {
                ctrlObjStatePropertyIsSightObserverHeightAbsolute.Load( m_CurrentObject.GetIsSightObserverHeightAbsolute);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetIsSightObserverHeightAbsolute", McEx);
            }

            try
            {
                ctrlObjStatePropertySightNumEllipseRays.Load(m_CurrentObject.GetSightNumEllipseRays);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetSightNumEllipseRays", McEx);
            }

            try
            {
                ctrlObjStatePropertySightQueryPrecision.Load(m_CurrentObject.GetSightQueryPrecision);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetSightQueryPrecision", McEx);
            }

            try
            {
                ctrlObjStatePropertyTextureResolution.Load(m_CurrentObject.GetSightTextureResolution);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetSightTextureResolution", McEx);
            }


            try
            {
                 ctrlObjStatePropertySightObserverPosition1.Load(m_CurrentObject.GetSightObserverPosition);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetSightObserverPosition", McEx);
            }

            try
            {
                cbSightNoDTMResult.Text = m_CurrentObject.GetSightNoDTMResult().ToString();

            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetSightNoDTMResult", McEx);
            }


        }

        #endregion
        
      
        public void SaveItem()
        {
            try
            {
                m_CurrentObject.SetSightNoDTMResult((DNENoDTMResult)Enum.Parse(typeof(DNENoDTMResult), cbSightNoDTMResult.Text));
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetSightNoDTMResult", McEx);
            }    
            try
            {
                //ctrlObjStatePropertyEnumBColor1.Save(m_CurrentObject.SetSightColor);
                ctrlObjStatePropertySightColor.Save(m_CurrentObject.SetSightColor);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetSightColor", McEx);
            }    

            try
            {
                ctrlObjStatePropertySightPresentationType.Save(m_CurrentObject.SetSightPresentationType);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetSightPresentationType", McEx);
            }        

            try
            {
                ctrlObjStatePropertySightObserverHeight.Save(m_CurrentObject.SetSightObserverHeight);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetSightObserverHeight", McEx);
            }

            try
            {
                ctrlObjStatePropertySightObservedHeight.Save(m_CurrentObject.SetSightObservedHeight);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetSightObservedHeight", McEx);
            }

            try
            {
                ctrlObjStatePropertySightObserverMinPitch.Save(m_CurrentObject.SetSightObserverMinPitch);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetSightObserverMinPitch", McEx);
            }

            try
            {
                ctrlObjStatePropertySightObserverMaxPitch.Save(m_CurrentObject.SetSightObserverMaxPitch);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetSightObserverMaxPitch", McEx);
            }
            
            try
            {
                ctrlObjStatePropertySightObservedHeightAbsolute.Save(m_CurrentObject.SetIsSightObservedHeightAbsolute);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetIsSightObservedHeightAbsolute", McEx);
            }

            try
            {
                ctrlObjStatePropertyIsSightObserverHeightAbsolute.Save(m_CurrentObject.SetIsSightObserverHeightAbsolute);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetIsSightObserverHeightAbsolute", McEx);
            }

            try
            {
                ctrlObjStatePropertySightNumEllipseRays.Save(m_CurrentObject.SetSightNumEllipseRays);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetSightNumEllipseRays", McEx);
            }

            try
            {
                ctrlObjStatePropertySightQueryPrecision.Save(m_CurrentObject.SetSightQueryPrecision);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetSightQueryPrecision", McEx);
            }

            try
            {
                ctrlObjStatePropertyTextureResolution.Save(m_CurrentObject.SetSightTextureResolution);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetSightTextureResolution", McEx);
            }

            try
            {
                ctrlObjStatePropertySightObserverPosition1.Save(m_CurrentObject.SetSightObserverPosition);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetSightObserverPosition", McEx);
            }

        }

        public void SetSightColor(DNEPointVisibility eVisibilityType, DNSMcBColor color, uint uPropertyID, DNSByteBool uObjectStateToServe)
        {
            try
            {
                m_CurrentObject.SetSightColor(eVisibilityType, color, uPropertyID, uObjectStateToServe.AsByte);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetSightColor", McEx);
            }
        }

       
        public void GetSightColor(DNEPointVisibility eVisibilityType, out DNSMcBColor pColor, out uint puPropertyID, DNSByteBool uObjectStateToServe)
        {
            pColor = new DNSMcBColor(Color.Black.R, Color.Black.G, Color.Black.B, Color.Black.A);
            puPropertyID = 0;

            try
            {
                m_CurrentObject.GetSightColor(eVisibilityType, out pColor, out puPropertyID, uObjectStateToServe.AsByte);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetSightColor", McEx);
            }
        }

        private void ctrlObjStatePropertySightNumEllipseRays_Load(object sender, EventArgs e)
        {

        }

        private void btnReleaseSightPresentationParameters_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to release sight presentation parameters?" + Environment.NewLine +
                "The properties are replaced by shared properties with default values, states properties are removed",
                "Release Sight Presentation Parameters",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if(dialogResult == DialogResult.Yes)
            {
                try
                {
                    m_CurrentObject.ReleaseSightPresentationParameters();
                    LoadItem(m_CurrentObject);
                    /*IDNMcObjectSchemeNode objectSchemeNode = (IDNMcObjectSchemeNode)m_CurrentObject;
                    Control control = Parent.Parent.Parent.Parent.Parent.Parent;
                    MCOverlayMangerTreeViewForm mcOverlayMangerTreeView = control as MCOverlayMangerTreeViewForm;
                    if (mcOverlayMangerTreeView != null)
                    {
                        //mcOverlayMangerTreeView.SelectNodeInTreeNode((uint)objectSchemeNode.GetHashCode(), -1);
                        
                    }*/
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("ReleaseSightPresentationParameters", McEx);
                }
            }
        }
    }
}
