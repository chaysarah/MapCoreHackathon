using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers.MapWorld;
using System.Reflection;

namespace MCTester.General_Forms
{
    public partial class frmVectorSchemePrivateValues : Form
    {
        private DNSXmlObjectNode m_XmlObjectNode;
        private DNSXmlObjectNode m_XmlAdvancedLabelNode;
        private bool m_IsAdvancedLabelDefinition = false;
        public frmVectorSchemePrivateValues(bool isAdvancedLabelDefinition, Object xmlNode):this(isAdvancedLabelDefinition)
        {
            if (isAdvancedLabelDefinition == false)
            {
                m_XmlObjectNode = (DNSXmlObjectNode)xmlNode;
                ctrlBrowseObjectScheme.FileName = m_XmlObjectNode.strSchemeFile;
                ntxScaleRangeMax.SetFloat(m_XmlObjectNode.ScaleRange.fMax);
                ntxScaleRangeMin.SetFloat(m_XmlObjectNode.ScaleRange.fMin);

                // dgvPropertyList.Rows.Add(m_XmlObjectNode.apProperties.Length);

                for (int i = 0; i < m_XmlObjectNode.apProperties.Length; i++)
                {
                    string PropType = dgvPropertyList[1, i].FormattedValue.ToString();

                    if (PropType == DNEPropertyType._EPT_BOOL.ToString())
                    {
                        dgvPropertyList[3, i].Tag = (DNSXmlProperyNode<bool>)m_XmlObjectNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_BYTE.ToString())
                    {
                        dgvPropertyList[3, i].Tag = (DNSXmlProperyNode<byte>)m_XmlObjectNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_SBYTE.ToString())
                    {
                        dgvPropertyList[3, i].Tag = (DNSXmlProperyNode<sbyte>)m_XmlObjectNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_INT.ToString())
                    {
                        dgvPropertyList[3, i].Tag = (DNSXmlProperyNode<int>)m_XmlObjectNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_UINT.ToString() || PropType == DNEPropertyType._EPT_ENUM.ToString())
                    {
                       dgvPropertyList[3, i].Tag  = (DNSXmlProperyNode<uint>)m_XmlObjectNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_FLOAT.ToString())
                    {
                        dgvPropertyList[3, i].Tag = (DNSXmlProperyNode<float>)m_XmlObjectNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_DOUBLE.ToString())
                    {
                      dgvPropertyList[3, i].Tag = (DNSXmlProperyNode<double>)m_XmlObjectNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_BCOLOR.ToString())
                    {
                        dgvPropertyList[3, i].Tag = (DNSXmlProperyNode<DNSMcBColor>)m_XmlObjectNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_STRING.ToString())
                    {
                        dgvPropertyList[3, i].Tag = (DNSXmlProperyNode<string>)m_XmlObjectNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_FVECTOR2D.ToString())
                    {
                       dgvPropertyList[3, i].Tag  = (DNSXmlFVector2DProperyNode)m_XmlObjectNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_VECTOR2D.ToString())
                    {
                      dgvPropertyList[3, i].Tag   = (DNSXmlVector2DProperyNode)m_XmlObjectNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_FVECTOR3D.ToString())
                    {
                      dgvPropertyList[3, i].Tag   = (DNSXmlFVector3DProperyNode)m_XmlObjectNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_VECTOR3D.ToString())
                    {
                       dgvPropertyList[3, i].Tag  = (DNSXmlVector3DProperyNode)m_XmlObjectNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_TEXTURE.ToString())
                    {
                      dgvPropertyList[3, i].Tag   = (DNSXmlTextureProperyNode)m_XmlObjectNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_FONT.ToString())
                    {
                      dgvPropertyList[3, i].Tag   = (DNSXmlFontProperyNode)m_XmlObjectNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_CONDITIONALSELECTOR.ToString())
                    {
                      dgvPropertyList[3, i].Tag   = (DNSXmlScaleConditionalSelectorProperyNode)m_XmlObjectNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_SUBITEM_ARRAY.ToString())
                    {
                       dgvPropertyList[3, i].Tag= (DNSXmlSubItemsDataProperyNode) m_XmlObjectNode.apProperties[i] ;
                    }

                    m_XmlObjectNode.apProperties[i].strPropertyName = dgvPropertyList[2, i].FormattedValue.ToString();
                    dgvPropertyList[3, i].Value = "OK";
                }
            }
            else
            {
                m_XmlAdvancedLabelNode = (DNSXmlObjectNode)xmlNode;

                   ctrlBrowseObjectScheme.FileName = m_XmlAdvancedLabelNode.strSchemeFile;
                ntxScaleRangeMax.SetFloat(m_XmlAdvancedLabelNode.ScaleRange.fMax);
                ntxScaleRangeMin.SetFloat(m_XmlAdvancedLabelNode.ScaleRange.fMin);

                for (int i = 0; i < m_XmlAdvancedLabelNode.apProperties.Length; i++)
                {
                    string PropType = dgvPropertyList[1, i].FormattedValue.ToString();

                    if (PropType == DNEPropertyType._EPT_BOOL.ToString())
                    {
                        dgvPropertyList[3, i].Tag = (DNSXmlProperyNode<bool>)m_XmlAdvancedLabelNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_BYTE.ToString())
                    {
                        dgvPropertyList[3, i].Tag = (DNSXmlProperyNode<byte>)m_XmlAdvancedLabelNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_SBYTE.ToString())
                    {
                        dgvPropertyList[3, i].Tag = (DNSXmlProperyNode<sbyte>)m_XmlAdvancedLabelNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_INT.ToString())
                    {
                        dgvPropertyList[3, i].Tag = (DNSXmlProperyNode<int>)m_XmlAdvancedLabelNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_UINT.ToString() || PropType == DNEPropertyType._EPT_ENUM.ToString())
                    {
                        dgvPropertyList[3, i].Tag = (DNSXmlProperyNode<uint>)m_XmlAdvancedLabelNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_FLOAT.ToString())
                    {
                        dgvPropertyList[3, i].Tag = (DNSXmlProperyNode<float>)m_XmlAdvancedLabelNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_DOUBLE.ToString())
                    {
                        dgvPropertyList[3, i].Tag = (DNSXmlProperyNode<double>)m_XmlAdvancedLabelNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_BCOLOR.ToString())
                    {
                        dgvPropertyList[3, i].Tag = (DNSXmlProperyNode<DNSMcBColor>)m_XmlAdvancedLabelNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_STRING.ToString())
                    {
                        dgvPropertyList[3, i].Tag = (DNSXmlProperyNode<string>)m_XmlAdvancedLabelNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_FVECTOR2D.ToString())
                    {
                        dgvPropertyList[3, i].Tag = (DNSXmlFVector2DProperyNode)m_XmlAdvancedLabelNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_VECTOR2D.ToString())
                    {
                        dgvPropertyList[3, i].Tag = (DNSXmlVector2DProperyNode)m_XmlAdvancedLabelNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_FVECTOR3D.ToString())
                    {
                        dgvPropertyList[3, i].Tag = (DNSXmlFVector3DProperyNode)m_XmlAdvancedLabelNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_VECTOR3D.ToString())
                    {
                        dgvPropertyList[3, i].Tag = (DNSXmlVector3DProperyNode)m_XmlAdvancedLabelNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_TEXTURE.ToString())
                    {
                        dgvPropertyList[3, i].Tag = (DNSXmlTextureProperyNode)m_XmlAdvancedLabelNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_FONT.ToString())
                    {
                        dgvPropertyList[3, i].Tag = (DNSXmlFontProperyNode)m_XmlAdvancedLabelNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_CONDITIONALSELECTOR.ToString())
                    {
                        dgvPropertyList[3, i].Tag = (DNSXmlScaleConditionalSelectorProperyNode)m_XmlAdvancedLabelNode.apProperties[i];
                    }
                    if (PropType == DNEPropertyType._EPT_SUBITEM_ARRAY.ToString())
                    {
                        dgvPropertyList[3, i].Tag = (DNSXmlSubItemsDataProperyNode)m_XmlAdvancedLabelNode.apProperties[i];
                    }

                    m_XmlAdvancedLabelNode.apProperties[i].strPropertyName = dgvPropertyList[2, i].FormattedValue.ToString();
                    dgvPropertyList[3, i].Value = "OK";
                }
            }

        }
        public frmVectorSchemePrivateValues(bool isAdvancedLabelDefinition)
        {
            InitializeComponent();
            m_IsAdvancedLabelDefinition = isAdvancedLabelDefinition;

            lblKeyFieldName.Visible = true;
            txtKeyFieldName.Visible = true;

            ctrlBrowseObjectScheme.FileNameChanged += new EventHandler(ctrlBrowseObjectScheme_FileNameChanged);
        }

        void ctrlBrowseObjectScheme_FileNameChanged(object sender, EventArgs e)
        {
            // Load data grid view content
            UserDataFactory UDF = new UserDataFactory();
            IDNMcGridCoordinateSystem coordSys = DNMcGridCoordSystemGeographic.Create(DNEDatumType._EDT_WGS84);
            IDNMcOverlayManager OM = DNMcOverlayManager.Create(coordSys);
            IDNMcObjectScheme[] objSchmes = null;
            try
            {
                objSchmes = OM.LoadObjectSchemes(ctrlBrowseObjectScheme.FileName, UDF);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("LoadObjectSchemes", McEx,"Please check if device created");
                return;
            }

            if (objSchmes != null && objSchmes.Length > 0)
            {
                DNSPropertyNameIDType[] arrPropertyID = objSchmes[0].GetProperties();

                if (arrPropertyID.Length == 0)
                    return;
                else
                {
                    dgvPropertyList.RowCount = arrPropertyID.Length;

                    for (int row = 0; row < arrPropertyID.Length; row++)
                    {
                        dgvPropertyList[0, row].Value = arrPropertyID[row].uID;
                        dgvPropertyList[1, row].Value = arrPropertyID[row].eType;

                        try
                        {
                            dgvPropertyList[2, row].Value = objSchmes[0].GetPropertyNameByID(arrPropertyID[row].uID).ToString();
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("GetPropertyNameByID", McEx);
                        }
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (m_IsAdvancedLabelDefinition == false)
            {
                m_XmlObjectNode = new DNSXmlObjectNode();
	            m_XmlObjectNode.strSchemeFile = ctrlBrowseObjectScheme.FileName;
	            m_XmlObjectNode.ScaleRange.fMax = ntxScaleRangeMax.GetFloat();
	            m_XmlObjectNode.ScaleRange.fMin = ntxScaleRangeMin.GetFloat();
	            m_XmlObjectNode.apProperties = new DNSXmlBaseProperyNode[dgvPropertyList.RowCount];
	            
	            for (int i = 0; i < dgvPropertyList.RowCount; i++ )
	            {                
	                string PropType = dgvPropertyList[1, i].FormattedValue.ToString();
	                
	                if (PropType == DNEPropertyType._EPT_BOOL.ToString())
	                {
	                    m_XmlObjectNode.apProperties[i] = (DNSXmlProperyNode<bool>)dgvPropertyList[3, i].Tag;      
	                }
	                if (PropType == DNEPropertyType._EPT_BYTE.ToString())
	                {
	                    m_XmlObjectNode.apProperties[i] = (DNSXmlProperyNode<byte>)dgvPropertyList[3, i].Tag;
	                }
                    if (PropType == DNEPropertyType._EPT_SBYTE.ToString())
                    {
                        m_XmlObjectNode.apProperties[i] = (DNSXmlProperyNode<sbyte>)dgvPropertyList[3, i].Tag;
                    }
                    if (PropType == DNEPropertyType._EPT_INT.ToString())
	                {
	                    m_XmlObjectNode.apProperties[i] = (DNSXmlProperyNode<int>)dgvPropertyList[3, i].Tag;
	                }
	                if (PropType == DNEPropertyType._EPT_UINT.ToString() || PropType == DNEPropertyType._EPT_ENUM.ToString())
	                {
	                    m_XmlObjectNode.apProperties[i] = (DNSXmlProperyNode<uint>)dgvPropertyList[3, i].Tag;
	                }
	                if (PropType == DNEPropertyType._EPT_FLOAT.ToString())
	                {
	                    m_XmlObjectNode.apProperties[i] = (DNSXmlProperyNode<float>)dgvPropertyList[3, i].Tag;                    
	                }
	                if (PropType == DNEPropertyType._EPT_DOUBLE.ToString())
	                {
	                    m_XmlObjectNode.apProperties[i] = (DNSXmlProperyNode<double>)dgvPropertyList[3, i].Tag;
	                }
	                if (PropType == DNEPropertyType._EPT_BCOLOR.ToString())
	                {
	                    m_XmlObjectNode.apProperties[i] = (DNSXmlProperyNode<DNSMcBColor>)dgvPropertyList[3, i].Tag;
	                }
	                if (PropType == DNEPropertyType._EPT_STRING.ToString())
	                {
	                    m_XmlObjectNode.apProperties[i] = (DNSXmlProperyNode<string>)dgvPropertyList[3, i].Tag;
	                }
	                if (PropType == DNEPropertyType._EPT_FVECTOR2D.ToString())
	                {
	                    m_XmlObjectNode.apProperties[i] = (DNSXmlFVector2DProperyNode)dgvPropertyList[3, i].Tag;
	                }
	                if (PropType == DNEPropertyType._EPT_VECTOR2D.ToString())
	                {
	                    m_XmlObjectNode.apProperties[i] = (DNSXmlVector2DProperyNode)dgvPropertyList[3, i].Tag;
	                }
	                if (PropType == DNEPropertyType._EPT_FVECTOR3D.ToString())
	                {
	                    m_XmlObjectNode.apProperties[i] = (DNSXmlFVector3DProperyNode)dgvPropertyList[3, i].Tag;
	                }
	                if (PropType == DNEPropertyType._EPT_VECTOR3D.ToString())
	                {
	                    m_XmlObjectNode.apProperties[i] = (DNSXmlVector3DProperyNode)dgvPropertyList[3, i].Tag;
	                }
	                if (PropType == DNEPropertyType._EPT_TEXTURE.ToString())
	                {
	                    m_XmlObjectNode.apProperties[i] = (DNSXmlTextureProperyNode)dgvPropertyList[3, i].Tag;
	                }
	                if (PropType == DNEPropertyType._EPT_FONT.ToString())
	                {
	                    m_XmlObjectNode.apProperties[i] = (DNSXmlFontProperyNode)dgvPropertyList[3, i].Tag;
	                }
	                if (PropType == DNEPropertyType._EPT_CONDITIONALSELECTOR.ToString())
	                {
	                    m_XmlObjectNode.apProperties[i] = (DNSXmlScaleConditionalSelectorProperyNode)dgvPropertyList[3, i].Tag;
	                }
	                if (PropType == DNEPropertyType._EPT_SUBITEM_ARRAY.ToString())
	                {
	                    m_XmlObjectNode.apProperties[i] = (DNSXmlSubItemsDataProperyNode)dgvPropertyList[3, i].Tag;
	                }
	                if(m_XmlObjectNode.apProperties[i] != null)
	                    m_XmlObjectNode.apProperties[i].strPropertyName = dgvPropertyList[2, i].FormattedValue.ToString();
                   
	            } 
                m_XmlObjectNode.strKeyFieldName = txtKeyFieldName.Text;
            } 
            else
            {
                m_XmlAdvancedLabelNode = new DNSXmlObjectNode();
                m_XmlAdvancedLabelNode.strSchemeFile = ctrlBrowseObjectScheme.FileName;
                m_XmlAdvancedLabelNode.ScaleRange.fMax = ntxScaleRangeMax.GetFloat();
                m_XmlAdvancedLabelNode.ScaleRange.fMin = ntxScaleRangeMin.GetFloat();
                m_XmlAdvancedLabelNode.apProperties = new DNSXmlBaseProperyNode[dgvPropertyList.RowCount];

                for (int i = 0; i < dgvPropertyList.RowCount; i++)
                {
                    string PropType = dgvPropertyList[1, i].FormattedValue.ToString();

                    if (PropType == DNEPropertyType._EPT_BOOL.ToString())
                    {
                        m_XmlAdvancedLabelNode.apProperties[i] = (DNSXmlProperyNode<bool>)dgvPropertyList[3, i].Tag;
                    }
                    if (PropType == DNEPropertyType._EPT_BYTE.ToString())
                    {
                        m_XmlAdvancedLabelNode.apProperties[i] = (DNSXmlProperyNode<byte>)dgvPropertyList[3, i].Tag;
                    }
                    if (PropType == DNEPropertyType._EPT_SBYTE.ToString())
                    {
                        m_XmlAdvancedLabelNode.apProperties[i] = (DNSXmlProperyNode<sbyte>)dgvPropertyList[3, i].Tag;
                    }
                    if (PropType == DNEPropertyType._EPT_INT.ToString())
                    {
                        m_XmlAdvancedLabelNode.apProperties[i] = (DNSXmlProperyNode<int>)dgvPropertyList[3, i].Tag;
                    }
                    if (PropType == DNEPropertyType._EPT_UINT.ToString() || PropType == DNEPropertyType._EPT_ENUM.ToString())
                    {
                        m_XmlAdvancedLabelNode.apProperties[i] = (DNSXmlProperyNode<uint>)dgvPropertyList[3, i].Tag;
                    }
                    if (PropType == DNEPropertyType._EPT_FLOAT.ToString())
                    {
                        m_XmlAdvancedLabelNode.apProperties[i] = (DNSXmlProperyNode<float>)dgvPropertyList[3, i].Tag;
                    }
                    if (PropType == DNEPropertyType._EPT_DOUBLE.ToString())
                    {
                        m_XmlAdvancedLabelNode.apProperties[i] = (DNSXmlProperyNode<double>)dgvPropertyList[3, i].Tag;
                    }
                    if (PropType == DNEPropertyType._EPT_BCOLOR.ToString())
                    {
                        m_XmlAdvancedLabelNode.apProperties[i] = (DNSXmlProperyNode<DNSMcBColor>)dgvPropertyList[3, i].Tag;
                    }
                    if (PropType == DNEPropertyType._EPT_STRING.ToString())
                    {
                        m_XmlAdvancedLabelNode.apProperties[i] = (DNSXmlProperyNode<string>)dgvPropertyList[3, i].Tag;
                    }
                    if (PropType == DNEPropertyType._EPT_FVECTOR2D.ToString())
                    {
                        m_XmlAdvancedLabelNode.apProperties[i] = (DNSXmlFVector2DProperyNode)dgvPropertyList[3, i].Tag;
                    }
                    if (PropType == DNEPropertyType._EPT_VECTOR2D.ToString())
                    {
                        m_XmlAdvancedLabelNode.apProperties[i] = (DNSXmlVector2DProperyNode)dgvPropertyList[3, i].Tag;
                    }
                    if (PropType == DNEPropertyType._EPT_FVECTOR3D.ToString())
                    {
                        m_XmlAdvancedLabelNode.apProperties[i] = (DNSXmlFVector3DProperyNode)dgvPropertyList[3, i].Tag;
                    }
                    if (PropType == DNEPropertyType._EPT_VECTOR3D.ToString())
                    {
                        m_XmlAdvancedLabelNode.apProperties[i] = (DNSXmlVector3DProperyNode)dgvPropertyList[3, i].Tag;
                    }
                    if (PropType == DNEPropertyType._EPT_TEXTURE.ToString())
                    {
                        m_XmlAdvancedLabelNode.apProperties[i] = (DNSXmlTextureProperyNode)dgvPropertyList[3, i].Tag;
                    }
                    if (PropType == DNEPropertyType._EPT_FONT.ToString())
                    {
                        m_XmlAdvancedLabelNode.apProperties[i] = (DNSXmlFontProperyNode)dgvPropertyList[3, i].Tag;
                    }
                    if (PropType == DNEPropertyType._EPT_CONDITIONALSELECTOR.ToString())
                    {
                        m_XmlAdvancedLabelNode.apProperties[i] = (DNSXmlScaleConditionalSelectorProperyNode)dgvPropertyList[3, i].Tag;
                    }
                    if (PropType == DNEPropertyType._EPT_SUBITEM_ARRAY.ToString())
                    {
                        m_XmlAdvancedLabelNode.apProperties[i] = (DNSXmlSubItemsDataProperyNode)dgvPropertyList[3, i].Tag;
                    }
                    if (m_XmlAdvancedLabelNode.apProperties[i] != null)
                        m_XmlAdvancedLabelNode.apProperties[i].strPropertyName = dgvPropertyList[2, i].FormattedValue.ToString();
                    m_XmlAdvancedLabelNode.strKeyFieldName = txtKeyFieldName.Text;
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public DNSXmlObjectNode NewXmlObjectNode
        {
            get { return m_XmlObjectNode; }
            set { m_XmlObjectNode = value; }
        }

        public DNSXmlObjectNode NewXmlAdvancedLabelNode
        {
            get { return m_XmlAdvancedLabelNode; }
            set { m_XmlAdvancedLabelNode = value; }
        }

        private void dgvPropertyList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                DNEPropertyType PropType = (DNEPropertyType)Enum.Parse(typeof(DNEPropertyType), dgvPropertyList[1, e.RowIndex].FormattedValue.ToString());
                frmXmlTypeProperyNode xmlTypeProperyNodeForm ;

                if (dgvPropertyList[3, e.RowIndex].Tag != null)
                {
                    xmlTypeProperyNodeForm = new frmXmlTypeProperyNode(PropType, dgvPropertyList[3, e.RowIndex].Tag);

                }
                else
                {
                    xmlTypeProperyNodeForm = new frmXmlTypeProperyNode(PropType);

                }
                if (xmlTypeProperyNodeForm.ShowDialog() == DialogResult.OK || xmlTypeProperyNodeForm.PropertyNode != null)
                {
                    dgvPropertyList[3, e.RowIndex].Value = "OK";
                    dgvPropertyList[3, e.RowIndex].Tag = xmlTypeProperyNodeForm.PropertyNode;
                }
                else
                    dgvPropertyList[3, e.RowIndex].Value = "Null";

            }
        }
    }
}
