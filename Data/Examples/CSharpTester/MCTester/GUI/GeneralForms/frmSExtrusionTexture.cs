using MapCore;
using MCTester.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCTester.General_Forms
{
    public partial class frmSExtrusionTexture : Form
    {

        private DNSExtrusionTexture mExtrusionTexture;

        public frmSExtrusionTexture()
        {
            InitializeComponent();

            ctrlXPlacement.LoadList(Enum.GetNames(typeof(DNETexturePlacementFlags)), 6, 0, -1);
            ctrlYPlacement.LoadList(Enum.GetNames(typeof(DNETexturePlacementFlags)), 6, 0, -1);

            mExtrusionTexture = new DNSExtrusionTexture();
        }

        public frmSExtrusionTexture(DNSExtrusionTexture extrusionTexture) : this()
        {
            mExtrusionTexture = extrusionTexture;
            ctrlStrTexturePath.FileName = mExtrusionTexture.strTexturePath;
            ctrlTextureScale.SetVector2D(mExtrusionTexture.TextureScale);
            SetETexturePlacementFlags(mExtrusionTexture.uXPlacementBitField, ctrlXPlacement);
            SetETexturePlacementFlags(mExtrusionTexture.uYPlacementBitField, ctrlYPlacement);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            mExtrusionTexture.strTexturePath = ctrlStrTexturePath.FileName;
            mExtrusionTexture.TextureScale = ctrlTextureScale.GetVector2D();
            mExtrusionTexture.uXPlacementBitField = GetETexturePlacementFlags(ctrlXPlacement);
            mExtrusionTexture.uYPlacementBitField = GetETexturePlacementFlags(ctrlYPlacement);

            this.Close();
        }

        public DNSExtrusionTexture GetExtrusionTexture()
        {
            return mExtrusionTexture;
        }

        private DNETexturePlacementFlags GetETexturePlacementFlags(CtrlCheckedListBox checkedListBox)
        {
            DNETexturePlacementFlags m_CurrTexturePlacementFlags = DNETexturePlacementFlags._ETPF_NONE;

            CheckedListBox.CheckedItemCollection checkedItems = checkedListBox.CheckedItems;
            int checkedItemsLength = checkedItems.Count;

            for (int i = 0; i < checkedItemsLength; i++)
            {
                DNETexturePlacementFlags bitMask = (DNETexturePlacementFlags)Enum.Parse(typeof(DNETexturePlacementFlags), checkedItems[i].ToString());
                m_CurrTexturePlacementFlags = m_CurrTexturePlacementFlags | bitMask;
            }

            return m_CurrTexturePlacementFlags;
        }

        private void SetETexturePlacementFlags(DNETexturePlacementFlags texturePlacementFlags, CtrlCheckedListBox checkedListBox)
        {
            Array TargetsArr = Enum.GetValues(typeof(DNETexturePlacementFlags));
            int length = TargetsArr.Length;
            for (int i = 0; i < TargetsArr.Length; i++)
            {
                DNETexturePlacementFlags dNETexturePlacementFlags = (DNETexturePlacementFlags)TargetsArr.GetValue(i);
                if ((texturePlacementFlags & dNETexturePlacementFlags) == dNETexturePlacementFlags)
                    checkedListBox.SetItemChecked(i, true);
            }
        }
    }
}
