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
using MCTester.Managers.ObjectWorld;
using MCTester.Managers;
using MCTester.GUI.Forms;

namespace MCTester.Controls
{
    public partial class CtrlEditModeUtility : UserControl
    {
        private IDNMcEditMode m_EditMode;
        private DNSObjectOperationsParams m_SObjectOperationsParams;

        private int m_dgvCellselectedIndex = 0;
        private int m_dgvPictureCellSelectedIndex = 0;
        private int m_dgvItemRectangleSelectedIndex = 0;
        private int m_dgvItemLineSelectedIndex = 0;
        private int m_dgvItemTextSelectedIndex = 0;

        private string[] m_Utility3DEditItemTypes;
        private string[] m_UtilityPictureTypes;

        private List<string> m_EditItemText = new List<string>();
        private List<IDNMcObjectSchemeItem> m_EditItemValue = new List<IDNMcObjectSchemeItem>();

        private List<string> m_PictureItemText = new List<string>();
        private List<IDNMcPictureItem> m_PictureItemValue = new List<IDNMcPictureItem>();

        private List<string> m_RectangleItemText = new List<string>();
        private List<IDNMcRectangleItem> m_RectangleItemValue = new List<IDNMcRectangleItem>();
        private List<string> m_LineItemText = new List<string>();
        private List<IDNMcLineItem> m_LineItemValue = new List<IDNMcLineItem>();
        private List<string> m_TextItemText = new List<string>();
        private List<IDNMcTextItem> m_TextItemValue = new List<IDNMcTextItem>();
        bool m_IsCameFromEditMode = false;
        ParentFormType m_ParentFormType;

        public enum ParentFormType { editmode, objectoperations};

        public CtrlEditModeUtility()
        {
            InitializeComponent();

            dgvUtility3DEditItem.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dgvUtility3DEditItem_EditingControlShowing);
            dgvUtilityPicture.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dgvUtilityPicture_EditingControlShowing);
            dgvUtilityItem.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dgvUtilityItem_EditingControlShowing);

            ((DataGridViewComboBoxColumn)dgvUtility3DEditItem.Columns[1]).DisplayMember = "EditItemTextList";
            ((DataGridViewComboBoxColumn)dgvUtility3DEditItem.Columns[1]).DisplayMember = "EditItemValueList";

            int lastRow = 0;

            // fill dgvUtility3DEditItem
            dgvUtility3DEditItem.Rows.Clear();

            m_Utility3DEditItemTypes = Enum.GetNames(typeof(DNEUtility3DEditItemType));

            for (int typeIndex = 0; typeIndex < (int)DNEUtility3DEditItemType._EUEIT_TYPES; typeIndex++)
            {
                dgvUtility3DEditItem.Rows.Add();
                lastRow = dgvUtility3DEditItem.Rows.GetLastRow(DataGridViewElementStates.Visible);
                dgvUtility3DEditItem[0, lastRow].Value = m_Utility3DEditItemTypes[typeIndex];
            }

            // fill combo box values
            m_EditItemText.Clear();
            m_EditItemValue.Clear();
            Dictionary<object, uint> dObjectSchemeItem = Manager_MCObjectSchemeItem.GetStandaloneItems();
            m_EditItemText.Add("Null");
            m_EditItemValue.Add(null);

            foreach (IDNMcObjectSchemeItem item in dObjectSchemeItem.Keys)
            {
                m_EditItemText.Add(Manager_MCNames.GetNameByObject(item));
                m_EditItemValue.Add(item);
            }

            ((DataGridViewComboBoxColumn)dgvUtility3DEditItem.Columns[1]).Items.Clear();
            ((DataGridViewComboBoxColumn)dgvUtility3DEditItem.Columns[1]).Items.AddRange(m_EditItemText.ToArray());

            // fill dgvUtlityPicture
            m_UtilityPictureTypes = Enum.GetNames(typeof(DNEUtilityPictureType));
            lastRow = 0;
            dgvUtilityPicture.Rows.Clear();
            for (int typeIndex = 0; typeIndex < (int)DNEUtilityPictureType._EUPT_TYPES; typeIndex++)
            {
                dgvUtilityPicture.Rows.Add();
                lastRow = dgvUtilityPicture.Rows.GetLastRow(DataGridViewElementStates.Visible);
                dgvUtilityPicture[0, lastRow].Value = m_UtilityPictureTypes[typeIndex];
            }

            // fill combo box values
            m_PictureItemText.Clear();
            m_PictureItemValue.Clear();
            ((DataGridViewComboBoxColumn)dgvUtilityPicture.Columns[1]).Items.Clear();

            m_PictureItemText.Add("Null");
            m_PictureItemValue.Add(null);

            foreach (IDNMcObjectSchemeItem item in dObjectSchemeItem.Keys)
            {
                if (item is IDNMcPictureItem)
                {
                    m_PictureItemText.Add(Manager_MCNames.GetNameByObject(item, "Picture"));
                    m_PictureItemValue.Add((IDNMcPictureItem)item);
                }
            }

            ((DataGridViewComboBoxColumn)dgvUtilityPicture.Columns[1]).Items.Clear();
            ((DataGridViewComboBoxColumn)dgvUtilityPicture.Columns[1]).Items.AddRange(m_PictureItemText.ToArray());

            // fill dgvUtilityItem
            dgvUtilityItem.Rows.Add(3);
            dgvUtilityItem[0, 0].Value = "Rectangle";
            dgvUtilityItem[0, 1].Value = "Line";
            dgvUtilityItem[0, 2].Value = "Text";

            // fill combo box values
            m_RectangleItemText.Clear();
            m_RectangleItemValue.Clear();

            m_LineItemText.Clear();
            m_LineItemValue.Clear();

            m_TextItemText.Clear();
            m_TextItemValue.Clear();

            m_RectangleItemText.Add("Null");
            m_RectangleItemValue.Add(null);

            m_LineItemText.Add("Null");
            m_LineItemValue.Add(null);

            m_TextItemText.Add("Null");
            m_TextItemValue.Add(null);

            foreach (IDNMcObjectSchemeItem item in dObjectSchemeItem.Keys)
            {
                if (item is IDNMcRectangleItem)
                {
                    m_RectangleItemText.Add(Manager_MCNames.GetNameByObject(item, "Rectangle"));
                    m_RectangleItemValue.Add((IDNMcRectangleItem)item);
                }
                if (item is IDNMcLineItem)
                {
                    m_LineItemText.Add(Manager_MCNames.GetNameByObject(item, "Line"));
                    m_LineItemValue.Add((IDNMcLineItem)item);
                }
                if (item is IDNMcTextItem)
                {
                    m_TextItemText.Add(Manager_MCNames.GetNameByObject(item, "Text"));
                    m_TextItemValue.Add((IDNMcTextItem)item);
                }
            }

            cbLineItem.Items.Clear();

            ((DataGridViewComboBoxCell)dgvUtilityItem[1, 0]).Items.Clear();
            ((DataGridViewComboBoxCell)dgvUtilityItem[1, 1]).Items.Clear();
            ((DataGridViewComboBoxCell)dgvUtilityItem[1, 2]).Items.Clear();

            cbLineItem.Items.AddRange(m_LineItemText.ToArray());

            ((DataGridViewComboBoxCell)dgvUtilityItem[1, 0]).Items.AddRange(m_RectangleItemText.ToArray());
            ((DataGridViewComboBoxCell)dgvUtilityItem[1, 1]).Items.AddRange(m_LineItemText.ToArray());
            ((DataGridViewComboBoxCell)dgvUtilityItem[1, 2]).Items.AddRange(m_TextItemText.ToArray());


        }

        public ParentFormType ParentFormTypeName {
            get { return m_ParentFormType; }
            set
            {
                m_ParentFormType = value;
                m_IsCameFromEditMode = ParentFormTypeName == ParentFormType.editmode;

                label1.Visible = !m_IsCameFromEditMode;
                cbLineItem.Visible = !m_IsCameFromEditMode;
                chxIsFieldEnableUtilityLine.Visible = !m_IsCameFromEditMode;
                label6.Visible = !m_IsCameFromEditMode;
                dgvUtilityItem.Visible = m_IsCameFromEditMode;
            }
        }

        public IDNMcEditMode EditMode
        {
            get { return m_EditMode; }
            set
            {
                m_EditMode = value;
                if (m_EditMode != null) LoadItem();
            }
        }

        public void SetSObjectOperationsParams(DNSObjectOperationsParams sObjectOperationsParams, bool isObjectScheme)
        {
            m_SObjectOperationsParams = sObjectOperationsParams;
            if (m_SObjectOperationsParams != null)
            {
                if (isObjectScheme)
                {
                    if (m_SObjectOperationsParams.pUtilityLine != null)
                        chxIsFieldEnableUtilityLine.Checked = true;
                    if (m_SObjectOperationsParams.ap3DEditUtilityItems != null && m_SObjectOperationsParams.ap3DEditUtilityItems.Length > 0)
                        chxIsFieldEnableUtility3DItems.Checked = true;
                    if (m_SObjectOperationsParams.apUtilityPictures != null && m_SObjectOperationsParams.apUtilityPictures.Length > 0)
                        chxIsFieldEnableUtilityPicture.Checked = true;
                }

                LoadItem();
            }
        }

        public void SetEnableButtons(bool isEnable)
        {
            cbLineItem.Enabled = isEnable;
            label1.Enabled = isEnable;
            EnableDGV(dgvUtilityPicture, isEnable);
            EnableDGV(dgvUtility3DEditItem, isEnable);

            chxIsFieldEnableUtilityLine.Checked = false;
            chxIsFieldEnableUtility3DItems.Checked = false;
            chxIsFieldEnableUtilityPicture.Checked = false;
        }

        private List<string> EditItemTextList
        {
            get { return m_EditItemText; }
            set { m_EditItemText = value; }
        }

        private List<IDNMcObjectSchemeItem> EditItemValueList
        {
            get { return m_EditItemValue; }
            set { m_EditItemValue = value; }
        }

        private List<string> PictureItemTextList
        {
            get { return m_PictureItemText; }
            set { m_PictureItemText = value; }
        }

        private List<IDNMcPictureItem> PictureItemValueList
        {
            get { return m_PictureItemValue; }
            set { m_PictureItemValue = value; }
        }

        private List<string> RectangleItemTextList
        {
            get { return m_RectangleItemText; }
            set { m_RectangleItemText = value; }
        }

        private List<IDNMcRectangleItem> RectangleItemValueList
        {
            get { return m_RectangleItemValue; }
            set { m_RectangleItemValue = value; }
        }

        private List<string> LineItemTextList
        {
            get { return m_LineItemText; }
            set { m_LineItemText = value; }
        }

        private List<IDNMcLineItem> LineItemValueList
        {
            get { return m_LineItemValue; }
            set { m_LineItemValue = value; }
        }

        private List<string> TextItemTextList
        {
            get { return m_TextItemText; }
            set { m_TextItemText = value; }
        }

        private List<IDNMcTextItem> TextItemValueList
        {
            get { return m_TextItemValue; }
            set { m_TextItemValue = value; }
        }

        public bool IsUserCheckedUtilityLine { get { return chxIsFieldEnableUtilityLine.Checked; } }

        public bool IsUserCheckedUtilityPicture { get { return chxIsFieldEnableUtilityPicture.Checked; } }

        public bool IsUserCheckedUtility3DItems { get { return chxIsFieldEnableUtility3DItems.Checked; } }

        private IDNMcObjectSchemeItem GetUtility3DEditItem(DNEUtility3DEditItemType itemType, int indexType)
        {
            IDNMcObjectSchemeItem objSchemeItem = null;
            if (m_SObjectOperationsParams != null)
            {
                if (m_SObjectOperationsParams.ap3DEditUtilityItems != null)
                    objSchemeItem = m_SObjectOperationsParams.ap3DEditUtilityItems[indexType];

            }
            else if (m_EditMode != null)
            {
                objSchemeItem = m_EditMode.GetUtility3DEditItem(itemType);
            }

            return objSchemeItem;
        }

        private IDNMcPictureItem GetUtilityPicture(DNEUtilityPictureType itemType, int indexType)
        {
            IDNMcPictureItem pictureItem = null;
            if (m_SObjectOperationsParams != null)
            {
                if (m_SObjectOperationsParams.apUtilityPictures != null)
                    pictureItem = m_SObjectOperationsParams.apUtilityPictures[indexType];
            }
            else if (m_EditMode != null)
            {
                pictureItem = m_EditMode.GetUtilityPicture(itemType);
            }

            return pictureItem;
        }

        private void LoadItem()
        {
           
            try
            {
                for (int i = 0; i < dgvUtility3DEditItem.RowCount; i++)
                {
                    DNEUtility3DEditItemType itemType = (DNEUtility3DEditItemType)Enum.Parse(typeof(DNEUtility3DEditItemType), m_Utility3DEditItemTypes[i]);
                    IDNMcObjectSchemeItem objSchemeItem = GetUtility3DEditItem(itemType, i); // m_EditMode.GetUtility3DEditItem(itemType);

                    int itemIndex = EditItemValueList.IndexOf(objSchemeItem);

                    dgvUtility3DEditItem[1, i].Value = EditItemTextList[itemIndex];
                    dgvUtility3DEditItem[1, i].Tag = objSchemeItem;
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetUtility3DEditItem", McEx);
            }

            try
            {
                for (int i = 0; i < dgvUtilityPicture.RowCount; i++)
                {
                    DNEUtilityPictureType picItemType = (DNEUtilityPictureType)Enum.Parse(typeof(DNEUtilityPictureType), m_UtilityPictureTypes[i]);
                    IDNMcPictureItem pictureItem = GetUtilityPicture(picItemType, i);

                    int pictureIndex = PictureItemValueList.IndexOf(pictureItem);
                    string pictureText = PictureItemTextList[pictureIndex];

                    dgvUtilityPicture[1, i].Value = pictureText;
                    dgvUtilityPicture[1, i].Tag = pictureItem;
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetUtilityPicture", McEx);
            }

            if (m_IsCameFromEditMode && m_EditMode != null)
            {
                chxIsFieldEnableUtilityLine.Visible = false;
                chxIsFieldEnableUtilityPicture.Visible = false;
                chxIsFieldEnableUtility3DItems.Visible = false;

                try
                {
                    IDNMcRectangleItem rectItem;
                    IDNMcLineItem lineItem;
                    IDNMcTextItem textItem;

                    m_EditMode.GetUtilityItems(out rectItem, out lineItem, out textItem);

                    int itemIndex;
                    itemIndex = RectangleItemValueList.IndexOf(rectItem);
                    dgvUtilityItem[1, 0].Value = RectangleItemTextList[itemIndex];
                    dgvUtilityItem[1, 0].Tag = rectItem;

                    itemIndex = LineItemValueList.IndexOf(lineItem);
                    dgvUtilityItem[1, 1].Value = LineItemTextList[itemIndex];
                    dgvUtilityItem[1, 1].Tag = lineItem;

                    itemIndex = TextItemValueList.IndexOf(textItem);
                    dgvUtilityItem[1, 2].Value = TextItemTextList[itemIndex];
                    dgvUtilityItem[1, 2].Tag = textItem;
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetUtilityItems", McEx);
                }
            }
            else if (m_SObjectOperationsParams != null)
            {
                IDNMcLineItem lineItem = m_SObjectOperationsParams.pUtilityLine;
                cbLineItem.SelectedIndex = LineItemValueList.IndexOf(lineItem);
            }
        }

        private IDNMcLineItem m_UtilityLineItem;
        private List<IDNMcObjectSchemeItem> m_Utility3DEditItems = new List<IDNMcObjectSchemeItem>();
        private List<IDNMcPictureItem> m_UtilityPictureItems = new List<IDNMcPictureItem>();

        public IDNMcLineItem UtilityLineItem
        {
            get { return m_UtilityLineItem; }
            set { m_UtilityLineItem = value; }
        }

        public List<IDNMcObjectSchemeItem> GetUtility3DEditItems()
        {
            return m_Utility3DEditItems;
        }

        public void SetUtility3DEditItems(List<IDNMcObjectSchemeItem> value)
        {
            m_Utility3DEditItems = value;
        }

        public List<IDNMcPictureItem> GetUtilityPictureItems()
        {
            return m_UtilityPictureItems;
        }

        public void SetUtilityPictureItems(List<IDNMcPictureItem> value)
        {
           m_UtilityPictureItems = value; 
        }

        public void SaveItem()
        {
            bool isCameFromEditMode = (m_SObjectOperationsParams == null);

            m_Utility3DEditItems.Clear();
            try
            {
                for (int i = 0; i < dgvUtility3DEditItem.RowCount; i++)
                {
                    DNEUtility3DEditItemType itemType = (DNEUtility3DEditItemType)Enum.Parse(typeof(DNEUtility3DEditItemType), m_Utility3DEditItemTypes[i]);
                    m_dgvCellselectedIndex = EditItemValueList.IndexOf((IDNMcObjectSchemeItem)dgvUtility3DEditItem[1, i].Tag);
                    if (isCameFromEditMode && m_EditMode != null)
                        m_EditMode.SetUtility3DEditItem(EditItemValueList[m_dgvCellselectedIndex], itemType);
                    else
                        m_Utility3DEditItems.Add(EditItemValueList[m_dgvCellselectedIndex]);
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetUtility3DEditItem", McEx);
            }

            m_UtilityPictureItems.Clear();
            try
            {
                for (int i = 0; i < dgvUtilityPicture.RowCount; i++)
                {
                    DNEUtilityPictureType picItemType = (DNEUtilityPictureType)Enum.Parse(typeof(DNEUtilityPictureType), m_UtilityPictureTypes[i]);
                    m_dgvPictureCellSelectedIndex = PictureItemValueList.IndexOf((IDNMcPictureItem)dgvUtilityPicture[1, i].Tag);
                    if (isCameFromEditMode && m_EditMode != null)
                        m_EditMode.SetUtilityPicture(PictureItemValueList[m_dgvPictureCellSelectedIndex], picItemType);
                    else
                        m_UtilityPictureItems.Add(PictureItemValueList[m_dgvPictureCellSelectedIndex]);
                }
            }

            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetUtilityPicture", McEx);
            }
            if (isCameFromEditMode)
            {
                if (m_EditMode != null)
                {
                    try
                    {
                        m_dgvItemRectangleSelectedIndex = RectangleItemValueList.IndexOf((IDNMcRectangleItem)dgvUtilityItem[1, 0].Tag);
                        m_dgvItemLineSelectedIndex = LineItemValueList.IndexOf((IDNMcLineItem)dgvUtilityItem[1, 1].Tag);
                        m_dgvItemTextSelectedIndex = TextItemValueList.IndexOf((IDNMcTextItem)dgvUtilityItem[1, 2].Tag);

                        m_EditMode.SetUtilityItems(RectangleItemValueList[m_dgvItemRectangleSelectedIndex],
                                                    LineItemValueList[m_dgvItemLineSelectedIndex],
                                                    TextItemValueList[m_dgvItemTextSelectedIndex]);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("SetUtilityItems", McEx);
                    }
                }
            }
            else
            {
                if (cbLineItem.SelectedIndex >= 0)
                {
                    m_UtilityLineItem = LineItemValueList[cbLineItem.SelectedIndex];
                }
            }
        }

        void dgvUtility3DEditItem_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvUtility3DEditItem.CurrentCell.ColumnIndex == 1)
            {
                // Check box column
                ComboBox comboBox = e.Control as ComboBox;
                comboBox.SelectedIndexChanged += new EventHandler(comboBox_SelectedIndexChanged);
            }
        }

        void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((ComboBox)sender).SelectedIndexChanged -= new EventHandler(comboBox_SelectedIndexChanged);

            m_dgvCellselectedIndex = ((ComboBox)sender).SelectedIndex;
            dgvUtility3DEditItem[1, dgvUtility3DEditItem.CurrentCell.RowIndex].Tag = EditItemValueList[m_dgvCellselectedIndex];

        }

        private void dgvUtilityPicture_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvUtilityPicture.CurrentCell.ColumnIndex == 1)
            {
                // Check box column
                ComboBox comboBox = e.Control as ComboBox;
                comboBox.SelectedIndexChanged += new EventHandler(pictureDGVcomboBox_SelectedIndexChanged);
            }
        }

        void pictureDGVcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((ComboBox)sender).SelectedIndexChanged -= new EventHandler(pictureDGVcomboBox_SelectedIndexChanged);

            m_dgvPictureCellSelectedIndex = ((ComboBox)sender).SelectedIndex;
            dgvUtilityPicture[1, dgvUtilityPicture.CurrentCell.RowIndex].Tag = PictureItemValueList[m_dgvPictureCellSelectedIndex];
        }

        private void dgvUtilityItem_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvUtilityItem.CurrentCell.ColumnIndex == 1)
            {
                // Check box column
                ComboBox comboBox = e.Control as ComboBox;

                if (dgvUtilityItem.CurrentCell.RowIndex == 0)
                    comboBox.SelectedIndexChanged += new EventHandler(RectItemcomboBox_SelectedIndexChanged);
                if (dgvUtilityItem.CurrentCell.RowIndex == 1)
                    comboBox.SelectedIndexChanged += new EventHandler(LineItemcomboBox_SelectedIndexChanged);
                if (dgvUtilityItem.CurrentCell.RowIndex == 2)
                    comboBox.SelectedIndexChanged += new EventHandler(TextItemcomboBox_SelectedIndexChanged);
            }
        }

        void RectItemcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((ComboBox)sender).SelectedIndexChanged -= new EventHandler(RectItemcomboBox_SelectedIndexChanged);

            m_dgvItemRectangleSelectedIndex = ((ComboBox)sender).SelectedIndex;
            dgvUtilityItem[1, 0].Tag = RectangleItemValueList[m_dgvItemRectangleSelectedIndex];
        }

        void LineItemcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((ComboBox)sender).SelectedIndexChanged -= new EventHandler(LineItemcomboBox_SelectedIndexChanged);

            m_dgvItemLineSelectedIndex = ((ComboBox)sender).SelectedIndex;
            dgvUtilityItem[1, 1].Tag = LineItemValueList[m_dgvItemLineSelectedIndex];
        }

        void TextItemcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((ComboBox)sender).SelectedIndexChanged -= new EventHandler(TextItemcomboBox_SelectedIndexChanged);

            m_dgvItemTextSelectedIndex = ((ComboBox)sender).SelectedIndex;
            dgvUtilityItem[1, 2].Tag = TextItemValueList[m_dgvItemTextSelectedIndex];
        }

        private void chxIsFieldEnableUtilityLine_CheckedChanged(object sender, EventArgs e)
        {
            label1.Enabled = chxIsFieldEnableUtilityLine.Checked;
            cbLineItem.Enabled = chxIsFieldEnableUtilityLine.Checked;
        }

        private void chxIsFieldEnableUtilityPicture_CheckedChanged(object sender, EventArgs e)
        {
            EnableDGV(dgvUtilityPicture, chxIsFieldEnableUtilityPicture.Checked);
        }

        private void EnableDGV(DataGridView dataGridView , bool isEnable)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                row.ReadOnly = !isEnable;
                DataGridViewComboBoxCell cmbCell = row.Cells[1] as DataGridViewComboBoxCell;

                if (row.ReadOnly)
                {
                    row.DefaultCellStyle.ForeColor = Color.Gray;
                    cmbCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                }
                else
                {
                    row.DefaultCellStyle.ForeColor = Color.Black;
                    cmbCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                }
            }
            dataGridView.Enabled = isEnable;
        }

        private void chxIsFieldEnableUtility3DItems_CheckedChanged(object sender, EventArgs e)
        {
            EnableDGV(dgvUtility3DEditItem, chxIsFieldEnableUtility3DItems.Checked);
        }
    }
}
