using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers;

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class ucLineBasedItem : ucSymbolicItem, IUserControlItem
    {
        private IDNMcLineBasedItem m_CurrentObject;
        
        private List<IDNMcObjectSchemeNode> m_lClippingNodes;


        public ucLineBasedItem()
            : base()
        {
            InitializeComponent();
            cmbShapeType.Items.AddRange(Enum.GetNames(typeof(DNEShapeType)));

            m_lClippingNodes = new List<IDNMcObjectSchemeNode>();
        }

        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentObject = (IDNMcLineBasedItem)aItem;
            base.LoadItem(aItem);
           
            ucLineBasedSightPresentationItemParams.LoadItem(aItem);

            try
            {
                cmbShapeType.Text = m_CurrentObject.GetShapeType().ToString();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetShapeType", McEx);
            }

            try
            {
                ctrlObjStatePropertyLineStyle.Load(m_CurrentObject.GetLineStyle);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetLineStyle", McEx);
            }

            try
            {
                ctrlObjStatePropertyLineColor.Load(m_CurrentObject.GetLineColor);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetLineColor", McEx);
            }

            try
            {
                ctrlObjStatePropertyLineWidth.Load(m_CurrentObject.GetLineWidth);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetLineWidth", McEx);
            }

            try
            {
                ctrlObjStatePropertyLineTexture.Load(m_CurrentObject.GetLineTexture);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetLineTexture", McEx);
            }

            try
            {
                ctrlObjStatePropertyTextureHeightRange.Load(m_CurrentObject.GetLineTextureHeightRange);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetLineTextureHeightRange", McEx);
            }

            try
            {
                ctrlObjStatePropertyTextureScale.Load(m_CurrentObject.GetLineTextureScale);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetLineTextureScale", McEx);
            }
            
            try
            {
                ctrlObjStatePropertyNumSmoothingLevels.Load(m_CurrentObject.GetNumSmoothingLevels);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetNumSmoothingLevels", McEx);
            }
                        
            try
            {
                ntxGreatCirclePrecision.SetFloat(m_CurrentObject.GetGreatCirclePrecision());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetGreatCirclePrecision", McEx);
            }

            try
            {
                IDNMcObjectScheme currScheme = m_CurrentObject.GetScheme();
                if (currScheme != null)
                {
                    IDNMcObjectSchemeNode[] nodes = currScheme.GetNodes(DNENodeKindFlags._ENKF_SYMBOLIC_ITEM);
                    foreach (IDNMcObjectSchemeNode node in nodes)
                    {
                        if (node.GetNodeType() == DNEObjectSchemeNodeType._TEXT_ITEM || node.GetNodeType() == DNEObjectSchemeNodeType._PICTURE_ITEM)
                        {
                            clstClippingItems.Items.Add(Manager_MCNames.GetNameByObject(node));
                            m_lClippingNodes.Add(node);
                        }
                    }

                    IDNMcObjectSchemeItem[] clippingItems;
                    bool bSelfClippingOnly;
                    m_CurrentObject.GetClippingItems(out clippingItems, out bSelfClippingOnly);

                    chxSelfClippingOnly.Checked = bSelfClippingOnly;
                    foreach (IDNMcObjectSchemeItem item in clippingItems)
                    {
                        if (m_lClippingNodes.Contains(item))
                            clstClippingItems.SetItemCheckState(m_lClippingNodes.IndexOf(item), CheckState.Checked);
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetClippingItems", McEx);
            }



            try
            {
                ctrlObjStatePropertyOutlineColor.Load(m_CurrentObject.GetOutlineColor);

            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetOutlineColor", McEx);
            }

            try
            {
                ctrlObjStatePropertyOutlineWidth.Load(m_CurrentObject.GetOutlineWidth);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetOutlineWidth", McEx);
            }

            try
            {
                ctrlObjStatePropertySidesFillStyle.Load(m_CurrentObject.GetSidesFillStyle);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetSidesFillStyle", McEx);
            }

            try
            {
                ctrlObjStatePropertySidesFillColor.Load(m_CurrentObject.GetSidesFillColor);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetSidesFillColor", McEx);
            }

            try
            {
                ctrlObjStatePropertySidesFillTexture.Load(m_CurrentObject.GetSidesFillTexture);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetFillTexture", McEx);
            }

            try
            {
                ctrlObjStatePropertySidesFillTextureScale.Load(m_CurrentObject.GetSidesFillTextureScale);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetSidesFillTextureScale", McEx);
            }

            try
            {
                ctrlObjStatePropertyVerticalHeight.Load(m_CurrentObject.GetVerticalHeight);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetVerticalHeight", McEx);
            }

            try
            {
                ctrlObjStatePropertyEPointOrderReverseMode1.Load(m_CurrentObject.GetPointOrderReverseMode);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetPointOrderReverseMode", McEx);
            }
        }

        #endregion

        protected override void SaveItem()
        {
            base.SaveItem();
            ucLineBasedSightPresentationItemParams.SaveItem();

            try
            {
                DNEShapeType shapeType = (DNEShapeType)Enum.Parse(typeof(DNEShapeType), cmbShapeType.Text);
                m_CurrentObject.SetShapeType(shapeType);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetShapeType", McEx);
            }
            try
            {
                ctrlObjStatePropertyLineStyle.Save(m_CurrentObject.SetLineStyle);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetLineStyle", McEx);
            }

            try
            {
                ctrlObjStatePropertyLineColor.Save(m_CurrentObject.SetLineColor);
               
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetLineColor", McEx);
            }

            try
            {
                ctrlObjStatePropertyLineWidth.Save(m_CurrentObject.SetLineWidth);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetLineWidth", McEx);
            }

            try
            {
                ctrlObjStatePropertyLineTexture.Save(m_CurrentObject.SetLineTexture);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetLineTexture", McEx);
            }

            try
            {
                ctrlObjStatePropertyTextureHeightRange.Save(m_CurrentObject.SetLineTextureHeightRange);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetLineTextureHeightRange", McEx);
            }

            try
            {
                ctrlObjStatePropertyTextureScale.Save(m_CurrentObject.SetLineTextureScale);      
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetLineTextureScale", McEx);
            }
            
            try
            {
                ctrlObjStatePropertyNumSmoothingLevels.Save(m_CurrentObject.SetNumSmoothingLevels);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetNumSmoothingLevels", McEx);
            }

            try
            {
                m_CurrentObject.SetGreatCirclePrecision(ntxGreatCirclePrecision.GetFloat());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetGreatCirclePrecision", McEx);
            }

            try
            {
                List<IDNMcObjectSchemeItem> clippingItems = new List<IDNMcObjectSchemeItem>();
                for (int i = 0; i < clstClippingItems.Items.Count; i++ )
                {
                    if (clstClippingItems.GetItemChecked(i))
                        clippingItems.Add((IDNMcObjectSchemeItem)m_lClippingNodes[i]);
                }

                m_CurrentObject.SetClippingItems(clippingItems.ToArray(), chxSelfClippingOnly.Checked);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetClippingItems", McEx);
            }

            try
            {
                ctrlObjStatePropertyOutlineColor.Save(m_CurrentObject.SetOutlineColor);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetOutlineColor", McEx);
            }


            try
            {
                ctrlObjStatePropertyOutlineWidth.Save(m_CurrentObject.SetOutlineWidth);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetOutlineWidth", McEx);
            }

            try
            {
                ctrlObjStatePropertySidesFillStyle.Save(m_CurrentObject.SetSidesFillStyle);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetSidesFillStyle", McEx);
            }

            try
            {
                ctrlObjStatePropertySidesFillColor.Save(m_CurrentObject.SetSidesFillColor);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetSidesFillColor", McEx);
            }


            try
            {
                ctrlObjStatePropertySidesFillTexture.Save(m_CurrentObject.SetSidesFillTexture);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetSidesFillTexture", McEx);
            }

            try
            {
                ctrlObjStatePropertySidesFillTextureScale.Save(m_CurrentObject.SetSidesFillTextureScale);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetSidesFillTextureScale", McEx);
            }

            try
            {
                ctrlObjStatePropertyVerticalHeight.Save(m_CurrentObject.SetVerticalHeight);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetVerticalHeight", McEx);
            }

            try
            {
                ctrlObjStatePropertyEPointOrderReverseMode1.Save(m_CurrentObject.SetPointOrderReverseMode);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetPointOrderReverseMode", McEx);
            }

            // turn on all viewports render needed flags
            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }

        
    }
}
