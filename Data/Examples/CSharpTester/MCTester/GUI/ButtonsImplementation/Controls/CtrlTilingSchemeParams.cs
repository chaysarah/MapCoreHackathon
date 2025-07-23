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

namespace MCTester.Controls
{
    public partial class CtrlTilingSchemeParams : UserControl
    {
        private bool m_IsInLoad = false;
        private bool m_IsInOnTextChanged = false;

        private DNSTilingScheme m_UserTilingScheme = new DNSTilingScheme();
        private string m_LastTilingScheme = "";

        public CtrlTilingSchemeParams()
        {
            m_IsInLoad = true; 
            InitializeComponent();

            cmbETilingSchemeType.Items.AddRange(Enum.GetNames(typeof(DNETilingSchemeType)));
            ntxLargestTileSizeInMapUnits.OnTextChangedEvent += new EventHandler(OnTextChanged);

            cmbETilingSchemeType.Text = DNETilingSchemeType._ETST_MAPCORE.ToString();

            m_IsInLoad = false;
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            
        }

        private DNETilingSchemeType GetETilingSchemeType()
        {
            return (DNETilingSchemeType)Enum.Parse(typeof(DNETilingSchemeType), cmbETilingSchemeType.Text); 
        }

        public void SetETilingSchemeTypeByValue(DNSTilingScheme tilingScheme)
        {
            if (tilingScheme != null)
            {
                DNSTilingScheme tilingScheme1 = DNMcMapLayer.GetStandardTilingScheme(DNETilingSchemeType._ETST_MAPCORE);
                DNSTilingScheme tilingScheme2 = DNMcMapLayer.GetStandardTilingScheme(DNETilingSchemeType._ETST_GLOBAL_LOGICAL);
                DNSTilingScheme tilingScheme3 = DNMcMapLayer.GetStandardTilingScheme(DNETilingSchemeType._ETST_GOOGLE_CRS84_QUAD);
                DNSTilingScheme tilingScheme4 = DNMcMapLayer.GetStandardTilingScheme(DNETilingSchemeType._ETST_GOOGLE_MAPS_COMPATIBLE);

                if (CompareTilingScheme(tilingScheme, tilingScheme1))
                    cmbETilingSchemeType.Text = DNETilingSchemeType._ETST_MAPCORE.ToString();
                else if (CompareTilingScheme(tilingScheme, tilingScheme2))
                    cmbETilingSchemeType.Text = DNETilingSchemeType._ETST_GLOBAL_LOGICAL.ToString();
                else if (CompareTilingScheme(tilingScheme, tilingScheme3))
                    cmbETilingSchemeType.Text = DNETilingSchemeType._ETST_GOOGLE_CRS84_QUAD.ToString();
                else if (CompareTilingScheme(tilingScheme, tilingScheme4))
                    cmbETilingSchemeType.Text = DNETilingSchemeType._ETST_GOOGLE_MAPS_COMPATIBLE.ToString();
                else
                    cmbETilingSchemeType.Text = "";
            }
        }

        public string GetETilingSchemeTypeNameByValue(DNSTilingScheme tilingScheme)
        {
            string strETilingSchemeTypeName = "";
            if (tilingScheme != null)
            {
                DNSTilingScheme tilingScheme1 = DNMcMapLayer.GetStandardTilingScheme(DNETilingSchemeType._ETST_MAPCORE);
                DNSTilingScheme tilingScheme2 = DNMcMapLayer.GetStandardTilingScheme(DNETilingSchemeType._ETST_GLOBAL_LOGICAL);
                DNSTilingScheme tilingScheme3 = DNMcMapLayer.GetStandardTilingScheme(DNETilingSchemeType._ETST_GOOGLE_CRS84_QUAD);
                DNSTilingScheme tilingScheme4 = DNMcMapLayer.GetStandardTilingScheme(DNETilingSchemeType._ETST_GOOGLE_MAPS_COMPATIBLE);

                if (CompareTilingScheme(tilingScheme, tilingScheme1))
                    strETilingSchemeTypeName = DNETilingSchemeType._ETST_MAPCORE.ToString();
                else if (CompareTilingScheme(tilingScheme, tilingScheme2))
                    strETilingSchemeTypeName = DNETilingSchemeType._ETST_GLOBAL_LOGICAL.ToString();
                else if (CompareTilingScheme(tilingScheme, tilingScheme3))
                    strETilingSchemeTypeName = DNETilingSchemeType._ETST_GOOGLE_CRS84_QUAD.ToString();
                else if (CompareTilingScheme(tilingScheme, tilingScheme4))
                    strETilingSchemeTypeName = DNETilingSchemeType._ETST_GOOGLE_MAPS_COMPATIBLE.ToString();
                
            }
            return strETilingSchemeTypeName;
        }


        public static DNETilingSchemeType GetETilingSchemeType(DNSTilingScheme tilingScheme)
        {
            if (tilingScheme != null)
            {
                DNSTilingScheme tilingScheme1 = DNMcMapLayer.GetStandardTilingScheme(DNETilingSchemeType._ETST_MAPCORE);
                DNSTilingScheme tilingScheme2 = DNMcMapLayer.GetStandardTilingScheme(DNETilingSchemeType._ETST_GLOBAL_LOGICAL);
                DNSTilingScheme tilingScheme3 = DNMcMapLayer.GetStandardTilingScheme(DNETilingSchemeType._ETST_GOOGLE_CRS84_QUAD);
                DNSTilingScheme tilingScheme4 = DNMcMapLayer.GetStandardTilingScheme(DNETilingSchemeType._ETST_GOOGLE_MAPS_COMPATIBLE);

                if (CompareTilingScheme(tilingScheme, tilingScheme1))
                    return DNETilingSchemeType._ETST_MAPCORE;
                else if (CompareTilingScheme(tilingScheme, tilingScheme2))
                    return DNETilingSchemeType._ETST_GLOBAL_LOGICAL;
                else if (CompareTilingScheme(tilingScheme, tilingScheme3))
                    return DNETilingSchemeType._ETST_GOOGLE_CRS84_QUAD;
                else if (CompareTilingScheme(tilingScheme, tilingScheme4))
                    return DNETilingSchemeType._ETST_GOOGLE_MAPS_COMPATIBLE;
                else
                    return DNETilingSchemeType._ETST_MAPCORE;
            }
            return DNETilingSchemeType._ETST_MAPCORE;
        }

        private static bool CompareTilingScheme(DNSTilingScheme tilingScheme1, DNSTilingScheme tilingScheme2)
        {
            return (tilingScheme1.dLargestTileSizeInMapUnits == tilingScheme2.dLargestTileSizeInMapUnits &&
                    tilingScheme1.TilingOrigin == tilingScheme2.TilingOrigin &&
                    tilingScheme1.uRasterTileMarginInPixels == tilingScheme2.uRasterTileMarginInPixels &&
                    tilingScheme1.uTileSizeInPixels == tilingScheme2.uTileSizeInPixels &&
                    tilingScheme1.uNumLargestTilesX == tilingScheme2.uNumLargestTilesX &&
                    tilingScheme1.uNumLargestTilesY == tilingScheme2.uNumLargestTilesY);
        }

        public void SetTilingScheme(DNSTilingScheme tilingScheme)
        {
            if (tilingScheme != null)
            {
                m_IsInOnTextChanged = true;

                ctrl2DVectorTilingOrigin.SetVector2D(tilingScheme.TilingOrigin);
                ntxLargestTileSizeInMapUnits.SetDouble(tilingScheme.dLargestTileSizeInMapUnits);
                ntxTileSizeInPixels.SetUInt32(tilingScheme.uTileSizeInPixels);
                ntxRasterTileMarginInPixels.SetUInt32(tilingScheme.uRasterTileMarginInPixels);
                ntxNumLargestTilesX.SetUInt32(tilingScheme.uNumLargestTilesX);
                ntxNumLargestTilesY.SetUInt32(tilingScheme.uNumLargestTilesY);
                
                if(!m_IsUserChangeType)
                    SetETilingSchemeTypeByValue(tilingScheme);
                CheckCustomTilingScheme();

                m_IsInOnTextChanged = false;
            }

        }

        public DNSTilingScheme GetTilingScheme()
        {
            return new DNSTilingScheme(ctrl2DVectorTilingOrigin.GetVector2D(),
                   ntxLargestTileSizeInMapUnits.GetDouble(),
                   ntxNumLargestTilesX.GetUInt32(),
                   ntxNumLargestTilesY.GetUInt32(),
                   ntxTileSizeInPixels.GetUInt32(),
                   ntxRasterTileMarginInPixels.GetUInt32());
        }

        public void SetStandardTilingScheme()
        {
            if (cmbETilingSchemeType.Text != "")
            {
                DNETilingSchemeType type = GetETilingSchemeType();
                SetTilingScheme(DNMcMapLayer.GetStandardTilingScheme(type));
            }
            else
                SetTilingScheme(m_UserTilingScheme);    
        }
        private bool m_IsUserChangeType = false;
        private void cmbETilingSchemeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!m_IsInOnTextChanged)
            {
                m_IsUserChangeType = true;
                if (!m_IsInLoad && m_LastTilingScheme == "")
                    m_UserTilingScheme = GetTilingScheme();
                m_LastTilingScheme = cmbETilingSchemeType.Text;
                SetStandardTilingScheme();
                m_IsUserChangeType = false;
            }
        }
        

        private void CheckCustomTilingScheme()
        {
            m_IsInOnTextChanged = true;
            if (!m_IsInLoad && !m_IsUserChangeType && cmbETilingSchemeType.Items != null && cmbETilingSchemeType.Items.Count > 0 && cmbETilingSchemeType.Items[cmbETilingSchemeType.Items.Count - 1].ToString() != "")
            {
                string type = GetETilingSchemeTypeNameByValue(GetTilingScheme());
                if (type == "")
                {
                    cmbETilingSchemeType.Items.Add("");
                    cmbETilingSchemeType.SelectedIndex = cmbETilingSchemeType.Items.Count - 1;
                    m_LastTilingScheme = "";
                }
            }
            m_IsInOnTextChanged = false;
        }

        private void ntxNumLargestTilesX_Leave(object sender, EventArgs e)
        {
            CheckCustomTilingScheme();
        }

        private void ntxNumLargestTilesY_Leave(object sender, EventArgs e)
        {
            CheckCustomTilingScheme();

        }

        private void ntxRasterTileMarginInPixels_Leave(object sender, EventArgs e)
        {
            CheckCustomTilingScheme();

        }

        private void ntxLargestTileSizeInMapUnits_Leave(object sender, EventArgs e)
        {
            CheckCustomTilingScheme();

        }

        private void ntxTileSizeInPixels_Leave(object sender, EventArgs e)
        {
            CheckCustomTilingScheme();

        }

        private void ctrl2DVectorTilingOrigin_Leave(object sender, EventArgs e)
        {
            CheckCustomTilingScheme();

        }

        private void CtrlTilingSchemeParams_Load(object sender, EventArgs e)
        {

        }
    }
}
