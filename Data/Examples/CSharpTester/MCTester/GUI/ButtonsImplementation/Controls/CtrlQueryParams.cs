using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.Controls
{
    public partial class CtrlQueryParams : UserControl
    {
        private DNSQueryParams m_queryParams;
        
        private List<string> m_ItemTypeFlagsText = new List<string>();
        private List<DNEObjectSchemeNodeType> m_ItemTypeFlagsValue = new List<DNEObjectSchemeNodeType>();

        private List<string> m_ItemKindsText = new List<string>();
        private List <DNENodeKindFlags> m_ItemKindsValue = new List <DNENodeKindFlags>();

        public CtrlQueryParams()
        {
            InitializeComponent();
            m_queryParams = new DNSQueryParams();
            cmbTerrainPrecsion.Items.AddRange(Enum.GetNames(typeof(DNEQueryPrecision)));
            cmbTerrainPrecsion.Text = DNEQueryPrecision._EQP_DEFAULT.ToString();
            cmbNoDTMResult.Items.AddRange(Enum.GetNames(typeof(DNENoDTMResult)));
            cmbNoDTMResult.Text = DNENoDTMResult._ENDR_FAIL.ToString();

            clsbIntersectionTargetType.LoadList(Enum.GetNames(typeof(DNEIntersectionTargetType)), 7, 0, 6);
            clstItemKindBitField.LoadList(Enum.GetNames(typeof(DNENodeKindFlags)), 6, 0, 5);
            clstTypes.LoadList(Enum.GetNames(typeof(DNEObjectSchemeNodeType)), 21, 0, -1);
        }

        public void SetQueryParams(DNSQueryParams queryParams)
        {
            if (queryParams != null)
            {
                chxUseMeshBoundingBoxOnly.Checked = queryParams.bUseMeshBoundingBoxOnly;
                cmbTerrainPrecsion.Text = queryParams.eTerrainPrecision.ToString();
                ntxBoundingBoxExpansionDist.SetFloat(queryParams.fBoundingBoxExpansionDist);
                SetItemTypes(queryParams.uItemTypeFlagsBitField);
                SetItemKinds(queryParams.eItemKindsBitField);
                SetIntersectionTargetType(queryParams.eTargetsBitMask);
                chxUseFlatEarth.Checked = queryParams.bUseFlatEarth;
                ntxGreatCirclePrecision.SetFloat(queryParams.fGreatCirclePrecision);
                cmbNoDTMResult.Text = queryParams.eNoDTMResult.ToString();
                chxAddStaticObjectContours.Checked = queryParams.bAddStaticObjectContours;

                if (queryParams.pOverlayFilter != null)
                   lstOverlayFilter.SelectedItem = queryParams.pOverlayFilter;
            }
        }

        public DNSQueryParams GetQueryParams()
        {
            m_queryParams.bUseMeshBoundingBoxOnly = chxUseMeshBoundingBoxOnly.Checked;
            m_queryParams.eTerrainPrecision = (DNEQueryPrecision)Enum.Parse(typeof(DNEQueryPrecision), cmbTerrainPrecsion.Text);
            m_queryParams.fBoundingBoxExpansionDist = ntxBoundingBoxExpansionDist.GetFloat();
            m_queryParams.uItemTypeFlagsBitField = GetItemTypes();
            m_queryParams.eItemKindsBitField = GetItemKinds();
            m_queryParams.eTargetsBitMask = GetIntersectionTargetType();
            m_queryParams.bUseFlatEarth = chxUseFlatEarth.Checked;
            m_queryParams.fGreatCirclePrecision = ntxGreatCirclePrecision.GetFloat();
            m_queryParams.eNoDTMResult = (DNENoDTMResult)Enum.Parse(typeof(DNENoDTMResult), cmbNoDTMResult.Text);
            m_queryParams.bAddStaticObjectContours = chxAddStaticObjectContours.Checked;

            if (lstOverlayFilter.SelectedItem != null)
                m_queryParams.pOverlayFilter = (IDNMcOverlay)lstOverlayFilter.SelectedItem;
            else
                m_queryParams.pOverlayFilter = null;
            
            return m_queryParams;
        }

        private void SetItemKinds(DNENodeKindFlags eItemKindsBitField)
        {
            Array TargetsArr = Enum.GetValues(typeof(DNENodeKindFlags));
            int length = TargetsArr.Length;
            for (int i = 0; i < TargetsArr.Length; i++)
            {
                DNENodeKindFlags dNENodeKindFlags = (DNENodeKindFlags)TargetsArr.GetValue(i);
                if ((eItemKindsBitField & dNENodeKindFlags) == dNENodeKindFlags)
                    clstItemKindBitField.SetItemChecked(i, true);
            }
        }

        private void SetIntersectionTargetType(DNEIntersectionTargetType eTargetsBitMask)
        {
            Array TargetsArr = Enum.GetValues(typeof(DNEIntersectionTargetType));
            int length = TargetsArr.Length;
            for (int i = 0; i < TargetsArr.Length; i++)
            {
                DNEIntersectionTargetType dNEIntersectionTargetType = (DNEIntersectionTargetType)TargetsArr.GetValue(i);
                if ((eTargetsBitMask & dNEIntersectionTargetType) == dNEIntersectionTargetType)
                    clsbIntersectionTargetType.SetItemChecked(i, true);
            }
        }

        private void SetItemTypes(uint uItemTypeFlagsBitField)
        {
            Array NodeTypeArr = Enum.GetValues(typeof(DNEObjectSchemeNodeType));
            int length = NodeTypeArr.Length;
            uint bitMaskNone = DNSQueryParams.NodeTypeToItemTypeBit(DNEObjectSchemeNodeType._NONE);
            if (uItemTypeFlagsBitField == bitMaskNone)
            {
                clstTypes.SetItemChecked(0, true);
            }

            else
            {
                for (int i = 1; i < length; i++)
                {
                    uint bitMask = DNSQueryParams.NodeTypeToItemTypeBit((DNEObjectSchemeNodeType)NodeTypeArr.GetValue(i));

                    if ((uItemTypeFlagsBitField & bitMask) == bitMask)
                    {
                        clstTypes.SetItemChecked(i, true);
                    }
                }
            }
        }

        private DNEIntersectionTargetType GetIntersectionTargetType()
        {
            DNEIntersectionTargetType m_CurrIntersectionTargetType = DNEIntersectionTargetType._EITT_NONE;
                
            CheckedListBox.CheckedItemCollection checkedItems = clsbIntersectionTargetType.CheckedItems;
            int checkedItemsLength = checkedItems.Count;
            
            for (int i = 0; i < checkedItemsLength; i++)
            {
                DNEIntersectionTargetType bitMask = (DNEIntersectionTargetType)Enum.Parse(typeof(DNEIntersectionTargetType), checkedItems[i].ToString());
                m_CurrIntersectionTargetType = m_CurrIntersectionTargetType | bitMask;
            }
                
            return m_CurrIntersectionTargetType;
        }

       
        private uint GetItemTypes()
        {
            uint m_CurrItemTypes = 0;

            CheckedListBox.CheckedItemCollection checkedItems = clstTypes.CheckedItems;
            int checkedItemsLength = checkedItems.Count;

            for (int i = 0; i < checkedItemsLength; i++)
            {
                DNEObjectSchemeNodeType bitMaskEnum = (DNEObjectSchemeNodeType)Enum.Parse(typeof(DNEObjectSchemeNodeType), checkedItems[i].ToString());
                uint bitMask = DNSQueryParams.NodeTypeToItemTypeBit(bitMaskEnum);
                m_CurrItemTypes = m_CurrItemTypes | bitMask;
            }

            return m_CurrItemTypes;
        }

        private DNENodeKindFlags GetItemKinds()
        {
            DNENodeKindFlags m_CurrItemKinds = DNENodeKindFlags._ENKF_NONE;

            CheckedListBox.CheckedItemCollection checkedItems = clstItemKindBitField.CheckedItems;
            int checkedItemsLength = checkedItems.Count;

            for (int i = 0; i < checkedItemsLength; i++)
            {
                DNENodeKindFlags bitMask = (DNENodeKindFlags)Enum.Parse(typeof(DNENodeKindFlags), checkedItems[i].ToString());

                m_CurrItemKinds = m_CurrItemKinds | bitMask;
            }

            return m_CurrItemKinds;
        }
                
        private void btnClearSelection_Click_1(object sender, EventArgs e)
        {
            lstOverlayFilter.ClearSelected();
        }

        public List<string> GetItemTypeFlagsText()
        {
            return m_ItemTypeFlagsText;
        }

        public void SetItemTypeFlagsText(List<string> value)
        {
            m_ItemTypeFlagsText = value;
        }

        public List<DNEObjectSchemeNodeType> GetItemTypeFlagsValue()
        {
            return m_ItemTypeFlagsValue;
        }

        public void SetItemTypeFlagsValue(List<DNEObjectSchemeNodeType> value)
        {
            m_ItemTypeFlagsValue = value;
        }
         

        #region IUserControlItem Members


        public void LoadItem(IDNMcOverlayManager overlayManager)
        {
            if (overlayManager != null)
            {
                IDNMcOverlay[] overlays = overlayManager.GetOverlays();

                lstOverlayFilter.Items.Clear();
                for (int i = 0; i < overlays.Length; i++)
                {
                    lstOverlayFilter.Items.Add((object)overlays[i]);
                }
            }
        }
        #endregion
    }
}
