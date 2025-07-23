using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using MapCore;
using UnmanagedWrapper;
using System.ComponentModel;
using System.IO;
using MCTester.Managers.MapWorld;

namespace MCTester.MapWorld
{
    /// <summary>
    /// A singleton holds current data source
    /// </summary>
    public class DataSourceType
    {
        static DataSourceType m_instance = null;
        IDNMcRawVectorMapLayer m_layer = null;

        protected DataSourceType()
        {
        }

        public static DataSourceType Instance
        {
            get 
            {
                if (m_instance == null)
                {
                    m_instance = new DataSourceType();
                }
                return m_instance;
            }
            
        }

        public IDNMcRawVectorMapLayer Layer
        {
            get
            {
                return m_layer;
            }
            set
            {
                m_layer = value;
            }
        }

        public bool Open(string fileName)
        {
            //if (File.Exists(fileName)) // Raw Vector datasource may be a directory or may be a file containing suffix...
            {
                try
                {
                    Close();

                    IDNMcGridCoordSystemGeographic coordSys = DNMcGridCoordSystemGeographic.Create(DNEDatumType._EDT_WGS84);
                    Layer = DNMcRawVectorMapLayer.Create(new DNSRawVectorParams(fileName, coordSys), coordSys);

                    return true;
                }
                catch(MapCoreException )
                {
                    return false;
                }
                
            }
            //else
            //{
            //    return false;
            //}
        }

        public void Close()
        {
            if (m_layer != null)
            {
                Layer.Dispose();
                Layer = null;
            }
        }

        public string[] FieldNames
        {
            get
            {
                List<string> result = new List<string>();

                if (Layer != null)
                {
                    uint numFields = Layer.GetNumFields();
                    for (uint i=0; i<numFields; i++)
                    {
                        string fieldName;
                        DNEFieldType fieldType;
                        Layer.GetFieldData(i, out fieldName, out fieldType);
                        result.Add(fieldName);
                    }
                }

                return result.ToArray();
            }
        }
    }

    /// <summary>
    /// Represents a scale structure
    /// </summary>
    [Browsable(true)]
    public class XmlScaleRangeNode
    {
        [DisplayName("A. Min")]
        [Description("Minimal scale")]
        public float fMin { get; set; }

        [DisplayName("B. Max")]
        [Description("Maximal scale")]
        public float fMax { get; set; }

        public static explicit operator DNSXmlScaleRangeNode (XmlScaleRangeNode arg)
        {
            DNSXmlScaleRangeNode result = new DNSXmlScaleRangeNode();
            if (arg != null)
            {
                result.fMax = arg.fMax;
                result.fMin = arg.fMin;
                if (result.fMax == 0f)
                {
                    result.fMax = float.MaxValue;
                }
                return result;
            }
            else
            {
                result.fMin = 0f;
                result.fMax = float.MaxValue;
            }
            return result;
        }

        public static explicit operator XmlScaleRangeNode(DNSXmlScaleRangeNode arg)
        {
            XmlScaleRangeNode result = new XmlScaleRangeNode();
            result.fMax = arg.fMax;
            result.fMin = arg.fMin;
            if (result.fMax == 0f)
            {
                result.fMax = float.MaxValue;
            }
            return result;
        }

    }

    /// <summary>
    /// A single field conditional node
    /// </summary>
    [Browsable(true)]
    public class XmlFieldConditionNode
    {
        [Category("Field condition")]
        [DisplayName("A. Field Name")]
        [Editor(typeof(FieldNamesEditorType),typeof(UITypeEditor))]
        public string strFieldName { get; set; }

        [Category("Field condition")]
        [TypeConverter(typeof(ComparsionOperatorConverter))]
        [DisplayName("B. Operator")]
        public DNEComparisonOperator eOperator { get; set; }

        [Category("Field condition")]
        [DisplayName("C. Operand")]
        [Editor(typeof(FieldValuesEditorType),typeof(UITypeEditor))]
        public string strOperand { get; set; }

        [Category("Field condition")]
        [DisplayName("D. Additional Operand")]
        [Editor(typeof(FieldValuesEditorType),typeof(UITypeEditor))]
        public string strAdditionalOperand { get; set; }

        public static explicit operator XmlFieldConditionNode(DNSXmlFieldConditionNode arg)
        {
            XmlFieldConditionNode result = new XmlFieldConditionNode();
            if (arg != null)
            {
                result.strFieldName = arg.strFieldName;
                result.eOperator = arg.eOperator;
                result.strOperand = arg.strOperand;
                result.strAdditionalOperand = arg.strAdditionalOperand;
            }
            return result;
        }

        public static explicit operator DNSXmlFieldConditionNode(XmlFieldConditionNode arg)
        {
            DNSXmlFieldConditionNode result = new DNSXmlFieldConditionNode();
            if (arg != null)
            {
                result.strFieldName = arg.strFieldName;
                result.eOperator = arg.eOperator;
                result.strOperand = arg.strOperand;
                result.strAdditionalOperand = arg.strAdditionalOperand;
            }
            return result;
        }

    }


    /// <summary>
    /// A boolean conditional node
    /// </summary>
    [Browsable(true)]
    public class XmlBoolConditionNode
    {
        [DisplayName("Sub Boolean Conditions")]
        [TypeConverter(typeof(BoolCondConverter))]
        public XmlBoolConditionNode[] aBoolConditions { get; set; }

        [DisplayName("Sub Field Conditions")]
        [TypeConverter(typeof(FieldCondConverter))]
        public XmlFieldConditionNode[] aFieldConditions { get; set; }

        [DisplayName("Operator")]
        [TypeConverter(typeof(BoolOperatorConverter))]
        public DNEBoolOperator eBoolOperator { get; set; }

        public static XmlBoolConditionNode[] FromDNS(DNSXmlBoolConditionNode[] arg)
        {
            List<XmlBoolConditionNode> result = new List<XmlBoolConditionNode>();

            foreach(DNSXmlBoolConditionNode item in arg)
            {
                result.Add((XmlBoolConditionNode)item);
            }

            return result.ToArray();
        }

        public static XmlFieldConditionNode[] FromDNS(DNSXmlFieldConditionNode[] arg)
        {
            List<XmlFieldConditionNode> result = new List<XmlFieldConditionNode>();

            foreach (DNSXmlFieldConditionNode item in arg)
            {
                result.Add((XmlFieldConditionNode)item);
            }

            return result.ToArray();
        }


        public static DNSXmlBoolConditionNode[] FromXml(XmlBoolConditionNode[] arg)
        {
            List<DNSXmlBoolConditionNode> result = new List<DNSXmlBoolConditionNode>();

            if (arg != null)
            {
                foreach (XmlBoolConditionNode item in arg)
                {
                    result.Add((DNSXmlBoolConditionNode)item);
                }
            }

            return result.ToArray();
        }

        public static DNSXmlFieldConditionNode[] FromXml(XmlFieldConditionNode[] arg)
        {
            List<DNSXmlFieldConditionNode> result = new List<DNSXmlFieldConditionNode>();

            if (arg != null)
            {
                foreach (XmlFieldConditionNode item in arg)
                {
                    result.Add((DNSXmlFieldConditionNode)item);
                }
            }

            return result.ToArray();
        }


        public static explicit operator XmlBoolConditionNode (DNSXmlBoolConditionNode arg)
        {
            XmlBoolConditionNode result = new XmlBoolConditionNode();
            result.aBoolConditions = FromDNS(arg.aBoolConditions);
            result.aFieldConditions = FromDNS(arg.aFieldConditions);
            result.eBoolOperator = arg.eBoolOperator;
            return result;
        }

        public static explicit operator DNSXmlBoolConditionNode(XmlBoolConditionNode arg)
        {
            DNSXmlBoolConditionNode result = new DNSXmlBoolConditionNode();
            result.aBoolConditions = FromXml(arg.aBoolConditions);
            result.aFieldConditions = FromXml(arg.aFieldConditions);
            result.eBoolOperator = arg.eBoolOperator;
            return result;
        }

    }

    /// <summary>
    /// Base property node. This is a basis for all properties supported by the vector map production system
    /// </summary>
    [Browsable(true)]
    public class XmlBasePropertyNode
    {
        protected DNEPropertyType _propType;

        public XmlBasePropertyNode()
        {
            IsUsed = true;
        }

        /// <summary>
        /// Converts from MC XML
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static explicit operator XmlBasePropertyNode(DNSXmlBaseProperyNode arg)
        {
            XmlBasePropertyNode result = null;
            if (arg != null)
            {
                switch (arg.GetPropertyType())
                {
                    case DNEPropertyType._EPT_BOOL:
                        result = new XmlPropertyBoolNode();
                        result.PropertyName = ((DNSXmlProperyNode<bool>)arg).strPropertyName;
                        result.SwitchFieldName = ((DNSXmlProperyNode<bool>)arg).strSwitchFieldName;
                        if (((DNSXmlProperyNode<bool>)arg).strFieldName != null &&
                            ((DNSXmlProperyNode<bool>)arg).strFieldName != string.Empty)
                        {
                            ((XmlPropertyBoolNode)result).FieldName = ((DNSXmlProperyNode<bool>)arg).strFieldName;
                        }
                        else
                        {
                            ((XmlPropertyBoolNode)result).Value = ((DNSXmlProperyNode<bool>)arg).Value;
                        }
                        if (((DNSXmlProperyNode<bool>)arg).aPropertySwitchCases != null && 
                            ((DNSXmlProperyNode<bool>)arg).aPropertySwitchCases.Length > 0)
                        {
                            ((XmlPropertyBoolNode)result).Case = 
                                new XmlPropertyBoolNode.XmlPropertyCaseBoolNode[((DNSXmlProperyNode<bool>)arg).aPropertySwitchCases.Length];
                            for (int i=0; i<((XmlPropertyBoolNode)result).Case.Length; i++)
                            {
                                ((XmlPropertyBoolNode)result).Case[i] = new XmlPropertyBoolNode.XmlPropertyCaseBoolNode();
                                ((XmlPropertyBoolNode)result).Case[i].FieldStringValue =
                                    ((DNSXmlProperyNode<bool>)arg).aPropertySwitchCases[i].strFieldValue;
                                ((XmlPropertyBoolNode)result).Case[i].PropertyValue =
                                    ((DNSXmlProperyNode<bool>)arg).aPropertySwitchCases[i].PropertyValue;
                            }
                        }
                        break;

                    case DNEPropertyType._EPT_BYTE:
                        result = new XmlPropertyByteNode();
                        result.PropertyName = ((DNSXmlProperyNode<byte>)arg).strPropertyName;
                        result.SwitchFieldName = ((DNSXmlProperyNode<byte>)arg).strSwitchFieldName;
                        if (((DNSXmlProperyNode<byte>)arg).strFieldName != null &&
                            ((DNSXmlProperyNode<byte>)arg).strFieldName != string.Empty)
                        {
                            ((XmlPropertyByteNode)result).FieldName = ((DNSXmlProperyNode<byte>)arg).strFieldName;
                        }
                        else
                        {
                            ((XmlPropertyByteNode)result).Value = ((DNSXmlProperyNode<byte>)arg).Value;
                        }
                        if (((DNSXmlProperyNode<byte>)arg).aPropertySwitchCases != null &&
                            ((DNSXmlProperyNode<byte>)arg).aPropertySwitchCases.Length > 0)
                        {
                            ((XmlPropertyByteNode)result).Case =
                                new XmlPropertyByteNode.XmlPropertyCaseByteNode[((DNSXmlProperyNode<byte>)arg).aPropertySwitchCases.Length];
                            for (int i = 0; i < ((XmlPropertyByteNode)result).Case.Length; i++)
                            {
                                ((XmlPropertyByteNode)result).Case[i] = new XmlPropertyByteNode.XmlPropertyCaseByteNode();
                                ((XmlPropertyByteNode)result).Case[i].FieldStringValue =
                                    ((DNSXmlProperyNode<byte>)arg).aPropertySwitchCases[i].strFieldValue;
                                ((XmlPropertyByteNode)result).Case[i].PropertyValue =
                                    ((DNSXmlProperyNode<byte>)arg).aPropertySwitchCases[i].PropertyValue;
                            }
                        }
                        break;
                    case DNEPropertyType._EPT_SBYTE:
                        result = new XmlPropertyByteNode();
                        result.PropertyName = ((DNSXmlProperyNode<sbyte>)arg).strPropertyName;
                        result.SwitchFieldName = ((DNSXmlProperyNode<sbyte>)arg).strSwitchFieldName;
                        if (((DNSXmlProperyNode<sbyte>)arg).strFieldName != null &&
                            ((DNSXmlProperyNode<sbyte>)arg).strFieldName != string.Empty)
                        {
                            ((XmlPropertyByteNode)result).FieldName = ((DNSXmlProperyNode<sbyte>)arg).strFieldName;
                        }
                        else
                        {
                            ((XmlPropertySByteNode)result).Value = ((DNSXmlProperyNode<sbyte>)arg).Value;
                        }
                        if (((DNSXmlProperyNode<sbyte>)arg).aPropertySwitchCases != null &&
                            ((DNSXmlProperyNode<sbyte>)arg).aPropertySwitchCases.Length > 0)
                        {
                            ((XmlPropertySByteNode)result).Case =
                                new XmlPropertySByteNode.XmlPropertyCaseSByteNode[((DNSXmlProperyNode<sbyte>)arg).aPropertySwitchCases.Length];
                            for (int i = 0; i < ((XmlPropertyByteNode)result).Case.Length; i++)
                            {
                                ((XmlPropertySByteNode)result).Case[i] = new XmlPropertySByteNode.XmlPropertyCaseSByteNode();
                                ((XmlPropertySByteNode)result).Case[i].FieldStringValue =
                                    ((DNSXmlProperyNode<sbyte>)arg).aPropertySwitchCases[i].strFieldValue;
                                ((XmlPropertySByteNode)result).Case[i].PropertyValue =
                                    ((DNSXmlProperyNode<sbyte>)arg).aPropertySwitchCases[i].PropertyValue;
                            }
                        }
                        break;
                    case DNEPropertyType._EPT_INT:
                        result = new XmlPropertyIntNode();
                        result.PropertyName = ((DNSXmlProperyNode<int>)arg).strPropertyName;
                        result.SwitchFieldName = ((DNSXmlProperyNode<int>)arg).strSwitchFieldName;
                        if (((DNSXmlProperyNode<int>)arg).strFieldName != null &&
                            ((DNSXmlProperyNode<int>)arg).strFieldName != string.Empty)
                        {
                            ((XmlPropertyIntNode)result).FieldName = ((DNSXmlProperyNode<int>)arg).strFieldName;
                        }
                        else
                        {
                            ((XmlPropertyIntNode)result).Value = ((DNSXmlProperyNode<int>)arg).Value;
                        }
                        if (((DNSXmlProperyNode<int>)arg).aPropertySwitchCases != null &&
                            ((DNSXmlProperyNode<int>)arg).aPropertySwitchCases.Length > 0)
                        {
                            ((XmlPropertyIntNode)result).Case =
                                new XmlPropertyIntNode.XmlPropertyCaseIntNode[((DNSXmlProperyNode<int>)arg).aPropertySwitchCases.Length];
                            for (int i = 0; i < ((XmlPropertyIntNode)result).Case.Length; i++)
                            {
                                ((XmlPropertyIntNode)result).Case[i] = new XmlPropertyIntNode.XmlPropertyCaseIntNode();
                                ((XmlPropertyIntNode)result).Case[i].FieldStringValue =
                                    ((DNSXmlProperyNode<int>)arg).aPropertySwitchCases[i].strFieldValue;
                                ((XmlPropertyIntNode)result).Case[i].PropertyValue =
                                    ((DNSXmlProperyNode<int>)arg).aPropertySwitchCases[i].PropertyValue;
                            }
                        }
                        break;

                    case DNEPropertyType._EPT_ENUM:  
                        result = new XmlPropertyEnumNode();
                        result.PropertyName = ((DNSXmlProperyNode<UInt32>)arg).strPropertyName;
                        result.SwitchFieldName = ((DNSXmlProperyNode<UInt32>)arg).strSwitchFieldName;
                        if (((DNSXmlProperyNode<UInt32>)arg).strFieldName != null &&
                            ((DNSXmlProperyNode<UInt32>)arg).strFieldName != string.Empty)
                        {
                            ((XmlPropertyEnumNode)result).FieldName = ((DNSXmlProperyNode<UInt32>)arg).strFieldName;
                        }
                        else
                        {
                            ((XmlPropertyEnumNode)result).Value = ((DNSXmlProperyNode<UInt32>)arg).Value;
                        }
                        if (((DNSXmlProperyNode<UInt32>)arg).aPropertySwitchCases != null &&
                            ((DNSXmlProperyNode<UInt32>)arg).aPropertySwitchCases.Length > 0)
                        {
                            ((XmlPropertyEnumNode)result).Case =
                                new XmlPropertyEnumNode.XmlPropertyCaseEnumNode[((DNSXmlProperyNode<UInt32>)arg).aPropertySwitchCases.Length];
                            for (int i = 0; i < ((XmlPropertyEnumNode)result).Case.Length; i++)
                            {
                                ((XmlPropertyEnumNode)result).Case[i] = new XmlPropertyEnumNode.XmlPropertyCaseEnumNode();
                                ((XmlPropertyEnumNode)result).Case[i].FieldStringValue =
                                    ((DNSXmlProperyNode<UInt32>)arg).aPropertySwitchCases[i].strFieldValue;
                                ((XmlPropertyEnumNode)result).Case[i].PropertyValue =
                                    ((DNSXmlProperyNode<UInt32>)arg).aPropertySwitchCases[i].PropertyValue;
                            }
                        }
                        break;
                    case DNEPropertyType._EPT_UINT:
                        result = new XmlPropertyUIntNode();
                        result.PropertyName = ((DNSXmlProperyNode<UInt32>)arg).strPropertyName;
                        result.SwitchFieldName = ((DNSXmlProperyNode<UInt32>)arg).strSwitchFieldName;
                        if (((DNSXmlProperyNode<UInt32>)arg).strFieldName != null &&
                            ((DNSXmlProperyNode<UInt32>)arg).strFieldName != string.Empty)
                        {
                            ((XmlPropertyUIntNode)result).FieldName = ((DNSXmlProperyNode<UInt32>)arg).strFieldName;
                        }
                        else
                        {
                            ((XmlPropertyUIntNode)result).Value = ((DNSXmlProperyNode<UInt32>)arg).Value;
                        }
                        if (((DNSXmlProperyNode<UInt32>)arg).aPropertySwitchCases != null &&
                            ((DNSXmlProperyNode<UInt32>)arg).aPropertySwitchCases.Length > 0)
                        {
                            ((XmlPropertyUIntNode)result).Case =
                                new XmlPropertyUIntNode.XmlPropertyCaseUIntNode[((DNSXmlProperyNode<UInt32>)arg).aPropertySwitchCases.Length];
                            for (int i = 0; i < ((XmlPropertyUIntNode)result).Case.Length; i++)
                            {
                                ((XmlPropertyUIntNode)result).Case[i] = new XmlPropertyUIntNode.XmlPropertyCaseUIntNode();
                                ((XmlPropertyUIntNode)result).Case[i].FieldStringValue =
                                    ((DNSXmlProperyNode<UInt32>)arg).aPropertySwitchCases[i].strFieldValue;
                                ((XmlPropertyUIntNode)result).Case[i].PropertyValue =
                                    ((DNSXmlProperyNode<UInt32>)arg).aPropertySwitchCases[i].PropertyValue;
                            }
                        }
                        break;

                    case DNEPropertyType._EPT_FLOAT:
                        result = new XmlPropertyFloatNode();
                        result.PropertyName = ((DNSXmlProperyNode<float>)arg).strPropertyName;
                        result.SwitchFieldName = ((DNSXmlProperyNode<float>)arg).strSwitchFieldName;
                        if (((DNSXmlProperyNode<float>)arg).strFieldName != null &&
                            ((DNSXmlProperyNode<float>)arg).strFieldName != string.Empty)
                        {
                            ((XmlPropertyFloatNode)result).FieldName = ((DNSXmlProperyNode<float>)arg).strFieldName;
                        }
                        else
                        {
                            ((XmlPropertyFloatNode)result).Value = ((DNSXmlProperyNode<float>)arg).Value;
                        }
                        if (((DNSXmlProperyNode<float>)arg).aPropertySwitchCases != null &&
                            ((DNSXmlProperyNode<float>)arg).aPropertySwitchCases.Length > 0)
                        {
                            ((XmlPropertyFloatNode)result).Case =
                                new XmlPropertyFloatNode.XmlPropertyCaseFloatNode[((DNSXmlProperyNode<float>)arg).aPropertySwitchCases.Length];
                            for (int i = 0; i < ((XmlPropertyFloatNode)result).Case.Length; i++)
                            {
                                ((XmlPropertyFloatNode)result).Case[i] = new XmlPropertyFloatNode.XmlPropertyCaseFloatNode();
                                ((XmlPropertyFloatNode)result).Case[i].FieldStringValue =
                                    ((DNSXmlProperyNode<float>)arg).aPropertySwitchCases[i].strFieldValue;
                                ((XmlPropertyFloatNode)result).Case[i].PropertyValue =
                                    ((DNSXmlProperyNode<float>)arg).aPropertySwitchCases[i].PropertyValue;
                            }
                        }
                        break;

                    case DNEPropertyType._EPT_DOUBLE:
                        result = new XmlPropertyDoubleNode();
                        result.PropertyName = ((DNSXmlProperyNode<double>)arg).strPropertyName;
                        result.SwitchFieldName = ((DNSXmlProperyNode<double>)arg).strSwitchFieldName;
                        if (((DNSXmlProperyNode<double>)arg).strFieldName != null &&
                            ((DNSXmlProperyNode<double>)arg).strFieldName != string.Empty)
                        {
                            ((XmlPropertyDoubleNode)result).FieldName = ((DNSXmlProperyNode<double>)arg).strFieldName;
                        }
                        else
                        {
                            ((XmlPropertyDoubleNode)result).Value = ((DNSXmlProperyNode<double>)arg).Value;
                        }
                        if (((DNSXmlProperyNode<double>)arg).aPropertySwitchCases != null &&
                            ((DNSXmlProperyNode<double>)arg).aPropertySwitchCases.Length > 0)
                        {
                            ((XmlPropertyDoubleNode)result).Case =
                                new XmlPropertyDoubleNode.XmlPropertyCaseDoubleNode[((DNSXmlProperyNode<double>)arg).aPropertySwitchCases.Length];
                            for (int i = 0; i < ((XmlPropertyDoubleNode)result).Case.Length; i++)
                            {
                                ((XmlPropertyDoubleNode)result).Case[i] = new XmlPropertyDoubleNode.XmlPropertyCaseDoubleNode();
                                ((XmlPropertyDoubleNode)result).Case[i].FieldStringValue =
                                    ((DNSXmlProperyNode<double>)arg).aPropertySwitchCases[i].strFieldValue;
                                ((XmlPropertyDoubleNode)result).Case[i].PropertyValue =
                                    ((DNSXmlProperyNode<double>)arg).aPropertySwitchCases[i].PropertyValue;
                            }
                        }
                        break;

                    case DNEPropertyType._EPT_STRING:
                        result = new XmlPropertyStringNode();
                        result.PropertyName = ((DNSXmlProperyNode<string>)arg).strPropertyName;
                        result.SwitchFieldName = ((DNSXmlProperyNode<string>)arg).strSwitchFieldName;
                        if (((DNSXmlProperyNode<string>)arg).strFieldName != null &&
                            ((DNSXmlProperyNode<string>)arg).strFieldName != string.Empty)
                        {
                            ((XmlPropertyStringNode)result).FieldName = ((DNSXmlProperyNode<string>)arg).strFieldName;
                        }
                        else
                        {
                            ((XmlPropertyStringNode)result).Value = ((DNSXmlProperyNode<string>)arg).Value;
                        }
                        if (((DNSXmlProperyNode<string>)arg).aPropertySwitchCases != null &&
                            ((DNSXmlProperyNode<string>)arg).aPropertySwitchCases.Length > 0)
                        {
                            ((XmlPropertyStringNode)result).Case =
                                new XmlPropertyStringNode.XmlPropertyCaseStringNode[((DNSXmlProperyNode<string>)arg).aPropertySwitchCases.Length];
                            for (int i = 0; i < ((XmlPropertyStringNode)result).Case.Length; i++)
                            {
                                ((XmlPropertyStringNode)result).Case[i] = new XmlPropertyStringNode.XmlPropertyCaseStringNode();
                                ((XmlPropertyStringNode)result).Case[i].FieldStringValue =
                                    ((DNSXmlProperyNode<string>)arg).aPropertySwitchCases[i].strFieldValue;
                                ((XmlPropertyStringNode)result).Case[i].PropertyValue =
                                    ((DNSXmlProperyNode<string>)arg).aPropertySwitchCases[i].PropertyValue;
                            }
                        }
                        break;

                    case DNEPropertyType._EPT_BCOLOR:
                        result = new XmlPropertyColorNode();
                        result.PropertyName = ((DNSXmlProperyNode<DNSMcBColor>)arg).strPropertyName;
                        result.SwitchFieldName = ((DNSXmlProperyNode<DNSMcBColor>)arg).strSwitchFieldName;
                        if (((DNSXmlProperyNode<DNSMcBColor>)arg).strFieldName != null &&
                            ((DNSXmlProperyNode<DNSMcBColor>)arg).strFieldName != string.Empty)
                        {
                            ((XmlPropertyColorNode)result).FieldName = ((DNSXmlProperyNode<DNSMcBColor>)arg).strFieldName;
                        }
                        else
                        {
                            ((XmlPropertyColorNode)result).Value =
                                Color.FromArgb(
                                    ((DNSXmlProperyNode<DNSMcBColor>)arg).Value.a,
                                    ((DNSXmlProperyNode<DNSMcBColor>)arg).Value.r,
                                    ((DNSXmlProperyNode<DNSMcBColor>)arg).Value.g,
                                    ((DNSXmlProperyNode<DNSMcBColor>)arg).Value.b);
                        }
                        if (((DNSXmlProperyNode<DNSMcBColor>)arg).aPropertySwitchCases != null &&
                            ((DNSXmlProperyNode<DNSMcBColor>)arg).aPropertySwitchCases.Length > 0)
                        {
                            ((XmlPropertyColorNode)result).Case =
                                new XmlPropertyColorNode.XmlPropertyCaseColorNode[((DNSXmlProperyNode<DNSMcBColor>)arg).aPropertySwitchCases.Length];
                            for (int i = 0; i < ((XmlPropertyColorNode)result).Case.Length; i++)
                            {
                                ((XmlPropertyColorNode)result).Case[i] = new XmlPropertyColorNode.XmlPropertyCaseColorNode();
                                ((XmlPropertyColorNode)result).Case[i].FieldStringValue =
                                    ((DNSXmlProperyNode<DNSMcBColor>)arg).aPropertySwitchCases[i].strFieldValue;
                                ((XmlPropertyColorNode)result).Case[i].PropertyValue = Color.FromArgb(
                                    ((DNSXmlProperyNode<DNSMcBColor>)arg).aPropertySwitchCases[i].PropertyValue.a,
                                    ((DNSXmlProperyNode<DNSMcBColor>)arg).aPropertySwitchCases[i].PropertyValue.r,
                                    ((DNSXmlProperyNode<DNSMcBColor>)arg).aPropertySwitchCases[i].PropertyValue.g,
                                    ((DNSXmlProperyNode<DNSMcBColor>)arg).aPropertySwitchCases[i].PropertyValue.b);
                            }
                        }
                        break;

                    case DNEPropertyType._EPT_FVECTOR2D:
                        result = new XmlPropertyFVector2DNode();
                        result.PropertyName = ((DNSXmlFVector2DProperyNode)arg).strPropertyName;
                        result.SwitchFieldName = ((DNSXmlFVector2DProperyNode)arg).strSwitchFieldName;
                        if (((DNSXmlFVector2DProperyNode)arg).X.strFieldName != null &&
                            ((DNSXmlFVector2DProperyNode)arg).X.strFieldName != string.Empty)
                        {
                            ((XmlPropertyFVector2DNode)result).FieldName.XFieldName = ((DNSXmlFVector2DProperyNode)arg).X.strFieldName;
                        }
                        else
                        {
                            ((XmlPropertyFVector2DNode)result).Value.x = ((DNSXmlFVector2DProperyNode)arg).X.Value;
                        }

                        if (((DNSXmlFVector2DProperyNode)arg).Y.strFieldName != null &&
                            ((DNSXmlFVector2DProperyNode)arg).Y.strFieldName != string.Empty)
                        {
                            ((XmlPropertyFVector2DNode)result).FieldName.YFieldName = ((DNSXmlFVector2DProperyNode)arg).Y.strFieldName;
                        }
                        else
                        {
                            ((XmlPropertyFVector2DNode)result).Value.y = ((DNSXmlFVector2DProperyNode)arg).Y.Value;
                        }

                        if (((DNSXmlFVector2DProperyNode)arg).aPropertySwitchCases != null &&
                            ((DNSXmlFVector2DProperyNode)arg).aPropertySwitchCases.Length > 0)
                        {
                            ((XmlPropertyFVector2DNode)result).Case =
                                new XmlPropertyFVector2DNode.XmlPropertyCaseFVector2DNode[
                                    ((DNSXmlFVector2DProperyNode)arg).aPropertySwitchCases.Length];
                            for (int i = 0; i < ((XmlPropertyFVector2DNode)result).Case.Length; i++)
                            {
                                ((XmlPropertyFVector2DNode)result).Case[i] = new XmlPropertyFVector2DNode.XmlPropertyCaseFVector2DNode();
                                ((XmlPropertyFVector2DNode)result).Case[i].FieldStringValue = 
                                    ((DNSXmlFVector2DProperyNode)arg).aPropertySwitchCases[i].strFieldValue;
                                ((XmlPropertyFVector2DNode)result).Case[i].PropertyValue.x =
                                    ((DNSXmlFVector2DProperyNode)arg).aPropertySwitchCases[i].PropertyValue.x;
                                ((XmlPropertyFVector2DNode)result).Case[i].PropertyValue.y =
                                    ((DNSXmlFVector2DProperyNode)arg).aPropertySwitchCases[i].PropertyValue.y;
                            }
                        }
                        break;

                    case DNEPropertyType._EPT_VECTOR2D:
                        result = new XmlPropertyVector2DNode();
                        result.PropertyName = ((DNSXmlVector2DProperyNode)arg).strPropertyName;
                        result.SwitchFieldName = ((DNSXmlVector2DProperyNode)arg).strSwitchFieldName;
                        if (((DNSXmlVector2DProperyNode)arg).X.strFieldName != null &&
                            ((DNSXmlVector2DProperyNode)arg).X.strFieldName != string.Empty)
                        {
                            ((XmlPropertyVector2DNode)result).FieldName.XFieldName = ((DNSXmlVector2DProperyNode)arg).X.strFieldName;
                        }
                        else
                        {
                            ((XmlPropertyVector2DNode)result).Value.x = ((DNSXmlVector2DProperyNode)arg).X.Value;
                        }

                        if (((DNSXmlVector2DProperyNode)arg).Y.strFieldName != null &&
                            ((DNSXmlVector2DProperyNode)arg).Y.strFieldName != string.Empty)
                        {
                            ((XmlPropertyVector2DNode)result).FieldName.YFieldName = ((DNSXmlVector2DProperyNode)arg).Y.strFieldName;
                        }
                        else
                        {
                            ((XmlPropertyVector2DNode)result).Value.y = ((DNSXmlVector2DProperyNode)arg).Y.Value;
                        }

                        if (((DNSXmlVector2DProperyNode)arg).aPropertySwitchCases != null &&
                            ((DNSXmlVector2DProperyNode)arg).aPropertySwitchCases.Length > 0)
                        {
                            ((XmlPropertyVector2DNode)result).Case =
                                new XmlPropertyVector2DNode.XmlPropertyCaseVector2DNode[
                                    ((DNSXmlVector2DProperyNode)arg).aPropertySwitchCases.Length];
                            for (int i = 0; i < ((XmlPropertyVector2DNode)result).Case.Length; i++)
                            {
                                ((XmlPropertyVector2DNode)result).Case[i] = new XmlPropertyVector2DNode.XmlPropertyCaseVector2DNode();
                                ((XmlPropertyVector2DNode)result).Case[i].FieldStringValue =
                                    ((DNSXmlVector2DProperyNode)arg).aPropertySwitchCases[i].strFieldValue;
                                ((XmlPropertyVector2DNode)result).Case[i].PropertyValue.x =
                                    ((DNSXmlVector2DProperyNode)arg).aPropertySwitchCases[i].PropertyValue.x;
                                ((XmlPropertyVector2DNode)result).Case[i].PropertyValue.y =
                                    ((DNSXmlVector2DProperyNode)arg).aPropertySwitchCases[i].PropertyValue.y;
                            }
                        }
                        break;

                    case DNEPropertyType._EPT_FVECTOR3D:
                        result = new XmlPropertyFVector3DNode();
                        result.PropertyName = ((DNSXmlFVector3DProperyNode)arg).strPropertyName;
                        result.SwitchFieldName = ((DNSXmlFVector3DProperyNode)arg).strSwitchFieldName;
                        if (((DNSXmlFVector3DProperyNode)arg).X.strFieldName != null &&
                            ((DNSXmlFVector3DProperyNode)arg).X.strFieldName != string.Empty)
                        {
                            ((XmlPropertyFVector3DNode)result).FieldName.XFieldName = ((DNSXmlFVector3DProperyNode)arg).X.strFieldName;
                        }
                        else
                        {
                            ((XmlPropertyFVector3DNode)result).Value.x = ((DNSXmlFVector3DProperyNode)arg).X.Value;
                        }

                        if (((DNSXmlFVector3DProperyNode)arg).Y.strFieldName != null &&
                            ((DNSXmlFVector3DProperyNode)arg).Y.strFieldName != string.Empty)
                        {
                            ((XmlPropertyFVector3DNode)result).FieldName.YFieldName = ((DNSXmlFVector3DProperyNode)arg).Y.strFieldName;
                        }
                        else
                        {
                            ((XmlPropertyFVector3DNode)result).Value.y = ((DNSXmlFVector3DProperyNode)arg).Y.Value;
                        }

                        if (((DNSXmlFVector3DProperyNode)arg).Z.strFieldName != null &&
                            ((DNSXmlFVector3DProperyNode)arg).Z.strFieldName != string.Empty)
                        {
                            ((XmlPropertyFVector3DNode)result).FieldName.ZFieldName = ((DNSXmlFVector3DProperyNode)arg).Z.strFieldName;
                        }
                        else
                        {
                            ((XmlPropertyFVector3DNode)result).Value.z = ((DNSXmlFVector3DProperyNode)arg).Z.Value;
                        }

                        if (((DNSXmlFVector3DProperyNode)arg).aPropertySwitchCases != null &&
                            ((DNSXmlFVector3DProperyNode)arg).aPropertySwitchCases.Length > 0)
                        {
                            ((XmlPropertyFVector3DNode)result).Case =
                                new XmlPropertyFVector3DNode.XmlPropertyCaseFVector3DNode[
                                    ((DNSXmlFVector3DProperyNode)arg).aPropertySwitchCases.Length];
                            for (int i = 0; i < ((XmlPropertyFVector3DNode)result).Case.Length; i++)
                            {
                                ((XmlPropertyFVector3DNode)result).Case[i] = new XmlPropertyFVector3DNode.XmlPropertyCaseFVector3DNode();
                                ((XmlPropertyFVector3DNode)result).Case[i].FieldStringValue =
                                    ((DNSXmlFVector3DProperyNode)arg).aPropertySwitchCases[i].strFieldValue;
                                ((XmlPropertyFVector3DNode)result).Case[i].PropertyValue.x =
                                    ((DNSXmlFVector3DProperyNode)arg).aPropertySwitchCases[i].PropertyValue.x;
                                ((XmlPropertyFVector3DNode)result).Case[i].PropertyValue.y =
                                    ((DNSXmlFVector3DProperyNode)arg).aPropertySwitchCases[i].PropertyValue.y;
                                ((XmlPropertyFVector3DNode)result).Case[i].PropertyValue.z =
                                    ((DNSXmlFVector3DProperyNode)arg).aPropertySwitchCases[i].PropertyValue.z;
                            }
                        }
                        break;

                    case DNEPropertyType._EPT_VECTOR3D:
                        result = new XmlPropertyVector3DNode();
                        result.PropertyName = ((DNSXmlVector3DProperyNode)arg).strPropertyName;
                        result.SwitchFieldName = ((DNSXmlVector3DProperyNode)arg).strSwitchFieldName;
                        if (((DNSXmlVector3DProperyNode)arg).X.strFieldName != null &&
                            ((DNSXmlVector3DProperyNode)arg).X.strFieldName != string.Empty)
                        {
                            ((XmlPropertyVector3DNode)result).FieldName.XFieldName = ((DNSXmlVector3DProperyNode)arg).X.strFieldName;
                        }
                        else
                        {
                            ((XmlPropertyVector3DNode)result).Value.x = ((DNSXmlVector3DProperyNode)arg).X.Value;
                        }

                        if (((DNSXmlVector3DProperyNode)arg).Y.strFieldName != null &&
                            ((DNSXmlVector3DProperyNode)arg).Y.strFieldName != string.Empty)
                        {
                            ((XmlPropertyVector3DNode)result).FieldName.YFieldName = ((DNSXmlVector3DProperyNode)arg).Y.strFieldName;
                        }
                        else
                        {
                            ((XmlPropertyVector3DNode)result).Value.y = ((DNSXmlVector3DProperyNode)arg).Y.Value;
                        }

                        if (((DNSXmlVector3DProperyNode)arg).Z.strFieldName != null &&
                            ((DNSXmlVector3DProperyNode)arg).Z.strFieldName != string.Empty)
                        {
                            ((XmlPropertyVector3DNode)result).FieldName.ZFieldName = ((DNSXmlVector3DProperyNode)arg).Z.strFieldName;
                        }
                        else
                        {
                            ((XmlPropertyVector3DNode)result).Value.z = ((DNSXmlVector3DProperyNode)arg).Z.Value;
                        }

                        if (((DNSXmlVector3DProperyNode)arg).aPropertySwitchCases != null &&
                            ((DNSXmlVector3DProperyNode)arg).aPropertySwitchCases.Length > 0)
                        {
                            ((XmlPropertyVector3DNode)result).Case =
                                new XmlPropertyVector3DNode.XmlPropertyCaseVector3DNode[
                                    ((DNSXmlVector3DProperyNode)arg).aPropertySwitchCases.Length];
                            for (int i = 0; i < ((XmlPropertyVector3DNode)result).Case.Length; i++)
                            {
                                ((XmlPropertyVector3DNode)result).Case[i] = new XmlPropertyVector3DNode.XmlPropertyCaseVector3DNode();
                                ((XmlPropertyVector3DNode)result).Case[i].FieldStringValue =
                                    ((DNSXmlVector3DProperyNode)arg).aPropertySwitchCases[i].strFieldValue;
                                ((XmlPropertyVector3DNode)result).Case[i].PropertyValue.x =
                                    ((DNSXmlVector3DProperyNode)arg).aPropertySwitchCases[i].PropertyValue.x;
                                ((XmlPropertyVector3DNode)result).Case[i].PropertyValue.y =
                                    ((DNSXmlVector3DProperyNode)arg).aPropertySwitchCases[i].PropertyValue.y;
                                ((XmlPropertyVector3DNode)result).Case[i].PropertyValue.z =
                                    ((DNSXmlVector3DProperyNode)arg).aPropertySwitchCases[i].PropertyValue.z;
                            }
                        }
                        break;

                    case DNEPropertyType._EPT_TEXTURE:
                        result = new XmlPropertyTextureNode();
                        result.PropertyName = ((DNSXmlTextureProperyNode)arg).strPropertyName;
                        result.SwitchFieldName = ((DNSXmlTextureProperyNode)arg).strSwitchFieldName;
                        if (((DNSXmlTextureProperyNode)arg).IgnoreTransparentMargin.strFieldName != null &&
                            ((DNSXmlTextureProperyNode)arg).IgnoreTransparentMargin.strFieldName != string.Empty)
                        {
                            ((XmlPropertyTextureNode)result).FieldName.IgnoreTransparentMargin = 
                                ((DNSXmlTextureProperyNode)arg).IgnoreTransparentMargin.strFieldName;
                        }
                        else
                        {
                            ((XmlPropertyTextureNode)result).Value.IgnoreTransparentMargin =
                                ((DNSXmlTextureProperyNode)arg).IgnoreTransparentMargin.Value;
                        }

                        if (((DNSXmlTextureProperyNode)arg).ImageFileName.strFieldName != null &&
                            ((DNSXmlTextureProperyNode)arg).ImageFileName.strFieldName != string.Empty)
                        {
                            ((XmlPropertyTextureNode)result).FieldName.ImageFileName =
                                ((DNSXmlTextureProperyNode)arg).ImageFileName.strFieldName;
                        }
                        else
                        {
                            ((XmlPropertyTextureNode)result).Value.ImageFileName =
                                ((DNSXmlTextureProperyNode)arg).ImageFileName.Value;
                        }

                        if (((DNSXmlTextureProperyNode)arg).IsTransparentColorEnabled.strFieldName != null &&
                            ((DNSXmlTextureProperyNode)arg).IsTransparentColorEnabled.strFieldName != string.Empty)
                        {
                            ((XmlPropertyTextureNode)result).FieldName.IsTransparentColorEnabled =
                                ((DNSXmlTextureProperyNode)arg).IsTransparentColorEnabled.strFieldName;
                        }
                        else
                        {
                            ((XmlPropertyTextureNode)result).Value.IsTransparentColorEnabled =
                                ((DNSXmlTextureProperyNode)arg).IsTransparentColorEnabled.Value;
                        }

                        if (((DNSXmlTextureProperyNode)arg).TransparentColor.strFieldName != null &&
                            ((DNSXmlTextureProperyNode)arg).TransparentColor.strFieldName != string.Empty)
                        {
                            ((XmlPropertyTextureNode)result).FieldName.TransparentColor =
                                ((DNSXmlTextureProperyNode)arg).TransparentColor.strFieldName;
                        }
                        else
                        {
                            ((XmlPropertyTextureNode)result).Value.TransparentColor =
                                Color.FromArgb(
                                    ((DNSXmlTextureProperyNode)arg).TransparentColor.Value.a,
                                    ((DNSXmlTextureProperyNode)arg).TransparentColor.Value.r,
                                    ((DNSXmlTextureProperyNode)arg).TransparentColor.Value.g,
                                    ((DNSXmlTextureProperyNode)arg).TransparentColor.Value.b);
                        }


                        if (((DNSXmlTextureProperyNode)arg).aPropertySwitchCases != null &&
                            ((DNSXmlTextureProperyNode)arg).aPropertySwitchCases.Length > 0)
                        {
                            ((XmlPropertyTextureNode)result).Case =
                                new XmlPropertyTextureNode.XmlPropertyCaseTextureNode[
                                    ((DNSXmlTextureProperyNode)arg).aPropertySwitchCases.Length];
                            for (int i = 0; i < ((XmlPropertyTextureNode)result).Case.Length; i++)
                            {
                                ((XmlPropertyTextureNode)result).Case[i] = new XmlPropertyTextureNode.XmlPropertyCaseTextureNode();
                                ((XmlPropertyTextureNode)result).Case[i].FieldStringValue =
                                    ((DNSXmlTextureProperyNode)arg).aPropertySwitchCases[i].strFieldValue;
                                ((XmlPropertyTextureNode)result).Case[i].PropertyValue.IgnoreTransparentMargin =
                                    ((DNSXmlTextureProperyNode)arg).aPropertySwitchCases[i].PropertyValue.bIgnoreTransparentMargin;
                                ((XmlPropertyTextureNode)result).Case[i].PropertyValue.ImageFileName =
                                    ((DNSXmlTextureProperyNode)arg).aPropertySwitchCases[i].PropertyValue.strImageFileName;
                                ((XmlPropertyTextureNode)result).Case[i].PropertyValue.TransparentColor =
                                    Color.FromArgb(
                                        ((DNSXmlTextureProperyNode)arg).aPropertySwitchCases[i].PropertyValue.TransparentColor.Value.a,
                                        ((DNSXmlTextureProperyNode)arg).aPropertySwitchCases[i].PropertyValue.TransparentColor.Value.r,
                                        ((DNSXmlTextureProperyNode)arg).aPropertySwitchCases[i].PropertyValue.TransparentColor.Value.g,
                                        ((DNSXmlTextureProperyNode)arg).aPropertySwitchCases[i].PropertyValue.TransparentColor.Value.b);
                            }
                        }
                        break;

                    case DNEPropertyType._EPT_FONT:
                        result = new XmlPropertyFontTypeNode();
                        result.PropertyName = ((DNSXmlFontProperyNode)arg).strPropertyName;
                        result.SwitchFieldName = ((DNSXmlFontProperyNode)arg).strSwitchFieldName;
                        if (((DNSXmlFontProperyNode)arg).FaceName.strFieldName != null &&
                            ((DNSXmlFontProperyNode)arg).FaceName.strFieldName != string.Empty)
                        {
                            ((XmlPropertyFontTypeNode)result).FieldName.FaceName =
                                ((DNSXmlFontProperyNode)arg).FaceName.strFieldName;
                        }
                        else
                        {
                            ((XmlPropertyFontTypeNode)result).Value.FaceName =
                                ((DNSXmlFontProperyNode)arg).FaceName.Value;
                        }

                        if (((DNSXmlFontProperyNode)arg).Height.strFieldName != null &&
                            ((DNSXmlFontProperyNode)arg).Height.strFieldName != string.Empty)
                        {
                            ((XmlPropertyFontTypeNode)result).FieldName.Height =
                                ((DNSXmlFontProperyNode)arg).Height.strFieldName;
                        }
                        else
                        {
                            ((XmlPropertyFontTypeNode)result).Value.FontHeight =
                                ((DNSXmlFontProperyNode)arg).Height.Value;
                        }

                        if (((DNSXmlFontProperyNode)arg).CharSet.strFieldName != null &&
                            ((DNSXmlFontProperyNode)arg).CharSet.strFieldName != string.Empty)
                        {
                            ((XmlPropertyFontTypeNode)result).FieldName.CharSet =
                                ((DNSXmlFontProperyNode)arg).CharSet.strFieldName;
                        }
                        else
                        {
                            ((XmlPropertyFontTypeNode)result).Value.CharSet =
                                ((DNSXmlFontProperyNode)arg).CharSet.Value;
                        }


                        if (((DNSXmlFontProperyNode)arg).aPropertySwitchCases != null &&
                            ((DNSXmlFontProperyNode)arg).aPropertySwitchCases.Length > 0)
                        {
                            ((XmlPropertyFontTypeNode)result).Case =
                                new XmlPropertyFontTypeNode.XmlPropertyCaseFontTypeNode[
                                    ((DNSXmlFontProperyNode)arg).aPropertySwitchCases.Length];
                            for (int i = 0; i < ((XmlPropertyFontTypeNode)result).Case.Length; i++)
                            {
                                ((XmlPropertyFontTypeNode)result).Case[i] = new XmlPropertyFontTypeNode.XmlPropertyCaseFontTypeNode();
                                ((XmlPropertyFontTypeNode)result).Case[i].FieldStringValue =
                                    ((DNSXmlFontProperyNode)arg).aPropertySwitchCases[i].strFieldValue;
                                ((XmlPropertyFontTypeNode)result).Case[i].PropertyValue.FaceName =
                                    ((DNSXmlFontProperyNode)arg).aPropertySwitchCases[i].PropertyValue.strFaceName;
                                ((XmlPropertyFontTypeNode)result).Case[i].PropertyValue.FontHeight =
                                    ((DNSXmlFontProperyNode)arg).aPropertySwitchCases[i].PropertyValue.nHeight;
                                ((XmlPropertyFontTypeNode)result).Case[i].PropertyValue.CharSet =
                                    ((DNSXmlFontProperyNode)arg).aPropertySwitchCases[i].PropertyValue.uCharSet;
                            }
                        }
                        break;

                    case DNEPropertyType._EPT_CONDITIONALSELECTOR:
                        result = new XmlPropertyScaleCondSelectorNode();
                        result.PropertyName = ((DNSXmlScaleConditionalSelectorProperyNode)arg).strPropertyName;
                        result.SwitchFieldName = ((DNSXmlScaleConditionalSelectorProperyNode)arg).strSwitchFieldName;
                        if (((DNSXmlScaleConditionalSelectorProperyNode)arg).Max.strFieldName != null &&
                            ((DNSXmlScaleConditionalSelectorProperyNode)arg).Max.strFieldName != string.Empty)
                        {
                            ((XmlPropertyScaleCondSelectorNode)result).FieldName.MaxFieldName =
                                ((DNSXmlScaleConditionalSelectorProperyNode)arg).Max.strFieldName;
                        }
                        else
                        {
                            ((XmlPropertyScaleCondSelectorNode)result).Value.Max =
                                ((DNSXmlScaleConditionalSelectorProperyNode)arg).Max.Value;
                        }

                        if (((DNSXmlScaleConditionalSelectorProperyNode)arg).Min.strFieldName != null &&
                            ((DNSXmlScaleConditionalSelectorProperyNode)arg).Min.strFieldName != string.Empty)
                        {
                            ((XmlPropertyScaleCondSelectorNode)result).FieldName.MinFieldName =
                                ((DNSXmlScaleConditionalSelectorProperyNode)arg).Min.strFieldName;
                        }
                        else
                        {
                            ((XmlPropertyScaleCondSelectorNode)result).Value.Min =
                                ((DNSXmlScaleConditionalSelectorProperyNode)arg).Min.Value;
                        }

                        if (((DNSXmlScaleConditionalSelectorProperyNode)arg).CancelScaleMode.strFieldName != null &&
                            ((DNSXmlScaleConditionalSelectorProperyNode)arg).CancelScaleMode.strFieldName != string.Empty)
                        {
                            ((XmlPropertyScaleCondSelectorNode)result).FieldName.CancelScaleModeFieldName =
                                ((DNSXmlScaleConditionalSelectorProperyNode)arg).CancelScaleMode.strFieldName;
                        }
                        else
                        {
                            ((XmlPropertyScaleCondSelectorNode)result).Value.CancelScaleMode =
                                ((DNSXmlScaleConditionalSelectorProperyNode)arg).CancelScaleMode.Value;
                        }

                        if (((DNSXmlScaleConditionalSelectorProperyNode)arg).CancelScaleModeResult.strFieldName != null &&
                            ((DNSXmlScaleConditionalSelectorProperyNode)arg).CancelScaleModeResult.strFieldName != string.Empty)
                        {
                            ((XmlPropertyScaleCondSelectorNode)result).FieldName.CancelScaleModeResult =
                                ((DNSXmlScaleConditionalSelectorProperyNode)arg).CancelScaleModeResult.strFieldName;
                        }
                        else
                        {
                            ((XmlPropertyScaleCondSelectorNode)result).Value.CancelScaleModeResult =
                                ((DNSXmlScaleConditionalSelectorProperyNode)arg).CancelScaleModeResult.Value;
                        }


                        if (((DNSXmlScaleConditionalSelectorProperyNode)arg).aPropertySwitchCases != null &&
                            ((DNSXmlScaleConditionalSelectorProperyNode)arg).aPropertySwitchCases.Length > 0)
                        {
                            ((XmlPropertyScaleCondSelectorNode)result).Case =
                                new XmlPropertyScaleCondSelectorNode.XmlPropertyCaseScaleCondSelectorNode[
                                    ((DNSXmlScaleConditionalSelectorProperyNode)arg).aPropertySwitchCases.Length];
                            for (int i = 0; i < ((XmlPropertyScaleCondSelectorNode)result).Case.Length; i++)
                            {
                                ((XmlPropertyScaleCondSelectorNode)result).Case[i] = new XmlPropertyScaleCondSelectorNode.XmlPropertyCaseScaleCondSelectorNode();
                                ((XmlPropertyScaleCondSelectorNode)result).Case[i].FieldStringValue =
                                    ((DNSXmlScaleConditionalSelectorProperyNode)arg).aPropertySwitchCases[i].strFieldValue;
                                ((XmlPropertyScaleCondSelectorNode)result).Case[i].PropertyValue.CancelScaleMode =
                                    ((DNSXmlScaleConditionalSelectorProperyNode)arg).aPropertySwitchCases[i].PropertyValue.uCancelScaleMode;
                                ((XmlPropertyScaleCondSelectorNode)result).Case[i].PropertyValue.CancelScaleModeResult =
                                    ((DNSXmlScaleConditionalSelectorProperyNode)arg).aPropertySwitchCases[i].PropertyValue.uCancelScaleModeResult;
                                ((XmlPropertyScaleCondSelectorNode)result).Case[i].PropertyValue.Max =
                                    ((DNSXmlScaleConditionalSelectorProperyNode)arg).aPropertySwitchCases[i].PropertyValue.fMaxScale;
                                ((XmlPropertyScaleCondSelectorNode)result).Case[i].PropertyValue.Min =
                                    ((DNSXmlScaleConditionalSelectorProperyNode)arg).aPropertySwitchCases[i].PropertyValue.fMinScale;
                            }
                        }
                        break;
                }
            }
            return result;
        }

        /// <summary>
        /// Convers to MC Xml property
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static explicit operator DNSXmlBaseProperyNode (XmlBasePropertyNode arg)
        {
            DNSXmlBaseProperyNode result = null;

            switch (arg._propType)
            {
                case DNEPropertyType._EPT_BOOL:
                    result = new DNSXmlProperyNode<bool>();
                    ((DNSXmlProperyNode<bool>)result).strFieldName = ((XmlPropertyBoolNode)arg).FieldName;
                    ((DNSXmlProperyNode<bool>)result).strPropertyName = ((XmlPropertyBoolNode)arg).PropertyName;
                    ((DNSXmlProperyNode<bool>)result).Value = ((XmlPropertyBoolNode)arg).Value;

                    if (((XmlPropertyBoolNode)arg).Case != null &&
                        ((XmlPropertyBoolNode)arg).Case.Length > 0)
                    {
                        ((DNSXmlProperyNode<bool>)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<bool>[
                            ((XmlPropertyBoolNode)arg).Case.Length];
                        for (int i=0; i<((XmlPropertyBoolNode)arg).Case.Length; i++)
                        {
                            ((DNSXmlProperyNode<bool>)result).aPropertySwitchCases[i] = new DNSXmlProperySwitchCase<bool>();
                            ((DNSXmlProperyNode<bool>)result).aPropertySwitchCases[i].strFieldValue =
                                ((XmlPropertyBoolNode)arg).Case[i].FieldStringValue;
                            ((DNSXmlProperyNode<bool>)result).aPropertySwitchCases[i].PropertyValue =
                                ((XmlPropertyBoolNode)arg).Case[i].PropertyValue;
                        }
                    }
                    else
                    {
                        ((DNSXmlProperyNode<bool>)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<bool>[0];
                    }

                    ((DNSXmlProperyNode<bool>)result).strSwitchFieldName = arg.SwitchFieldName;
                    break;

                case DNEPropertyType._EPT_BYTE:
                    result = new DNSXmlProperyNode<byte>();
                    ((DNSXmlProperyNode<byte>)result).strFieldName = ((XmlPropertyByteNode)arg).FieldName;
                    ((DNSXmlProperyNode<byte>)result).strPropertyName = ((XmlPropertyByteNode)arg).PropertyName;
                    ((DNSXmlProperyNode<byte>)result).Value = ((XmlPropertyByteNode)arg).Value;

                    if (((XmlPropertyByteNode)arg).Case != null &&
                        ((XmlPropertyByteNode)arg).Case.Length > 0)
                    {
                        ((DNSXmlProperyNode<byte>)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<byte>[
                            ((XmlPropertyByteNode)arg).Case.Length];
                        for (int i = 0; i < ((XmlPropertyByteNode)arg).Case.Length; i++)
                        {
                            ((DNSXmlProperyNode<byte>)result).aPropertySwitchCases[i] = new DNSXmlProperySwitchCase<byte>();
                            ((DNSXmlProperyNode<byte>)result).aPropertySwitchCases[i].strFieldValue =
                                ((XmlPropertyByteNode)arg).Case[i].FieldStringValue;
                            ((DNSXmlProperyNode<byte>)result).aPropertySwitchCases[i].PropertyValue =
                                ((XmlPropertyByteNode)arg).Case[i].PropertyValue;
                        }
                    }
                    else
                    {
                        ((DNSXmlProperyNode<byte>)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<byte>[0];
                    }

                    ((DNSXmlProperyNode<byte>)result).strSwitchFieldName = arg.SwitchFieldName;
                    break;

                case DNEPropertyType._EPT_SBYTE:
                    result = new DNSXmlProperyNode<sbyte>();
                    ((DNSXmlProperyNode<sbyte>)result).strFieldName = ((XmlPropertySByteNode)arg).FieldName;
                    ((DNSXmlProperyNode<sbyte>)result).strPropertyName = ((XmlPropertySByteNode)arg).PropertyName;
                    ((DNSXmlProperyNode<sbyte>)result).Value = ((XmlPropertySByteNode)arg).Value;

                    if (((XmlPropertySByteNode)arg).Case != null &&
                        ((XmlPropertySByteNode)arg).Case.Length > 0)
                    {
                        ((DNSXmlProperyNode<sbyte>)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<sbyte>[
                            ((XmlPropertySByteNode)arg).Case.Length];
                        for (int i = 0; i < ((XmlPropertySByteNode)arg).Case.Length; i++)
                        {
                            ((DNSXmlProperyNode<sbyte>)result).aPropertySwitchCases[i] = new DNSXmlProperySwitchCase<sbyte>();
                            ((DNSXmlProperyNode<sbyte>)result).aPropertySwitchCases[i].strFieldValue =
                                ((XmlPropertySByteNode)arg).Case[i].FieldStringValue;
                            ((DNSXmlProperyNode<sbyte>)result).aPropertySwitchCases[i].PropertyValue =
                                ((XmlPropertySByteNode)arg).Case[i].PropertyValue;
                        }
                    }
                    else
                    {
                        ((DNSXmlProperyNode<sbyte>)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<sbyte>[0];
                    }

                    ((DNSXmlProperyNode<sbyte>)result).strSwitchFieldName = arg.SwitchFieldName;
                    break;
                case DNEPropertyType._EPT_INT:
                    result = new DNSXmlProperyNode<int>();
                    ((DNSXmlProperyNode<int>)result).strFieldName = ((XmlPropertyIntNode)arg).FieldName;
                    ((DNSXmlProperyNode<int>)result).strPropertyName = ((XmlPropertyIntNode)arg).PropertyName;
                    ((DNSXmlProperyNode<int>)result).Value = ((XmlPropertyIntNode)arg).Value;

                    if (((XmlPropertyIntNode)arg).Case != null &&
                        ((XmlPropertyIntNode)arg).Case.Length > 0)
                    {
                        ((DNSXmlProperyNode<int>)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<int>[
                            ((XmlPropertyIntNode)arg).Case.Length];
                        for (int i = 0; i < ((XmlPropertyIntNode)arg).Case.Length; i++)
                        {
                            ((DNSXmlProperyNode<int>)result).aPropertySwitchCases[i] = new DNSXmlProperySwitchCase<int>();
                            ((DNSXmlProperyNode<int>)result).aPropertySwitchCases[i].strFieldValue =
                                ((XmlPropertyIntNode)arg).Case[i].FieldStringValue;
                            ((DNSXmlProperyNode<int>)result).aPropertySwitchCases[i].PropertyValue =
                                ((XmlPropertyIntNode)arg).Case[i].PropertyValue;
                        }
                    }
                    else
                    {
                        ((DNSXmlProperyNode<int>)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<int>[0];
                    }

                    ((DNSXmlProperyNode<int>)result).strSwitchFieldName = arg.SwitchFieldName;
                    break;

                case DNEPropertyType._EPT_ENUM:
                    result = new DNSXmlProperyNode<UInt32>();
                    ((DNSXmlProperyNode<UInt32>)result).strFieldName = ((XmlPropertyEnumNode)arg).FieldName;
                    ((DNSXmlProperyNode<UInt32>)result).strPropertyName = ((XmlPropertyEnumNode)arg).PropertyName;
                    ((DNSXmlProperyNode<UInt32>)result).Value = ((XmlPropertyEnumNode)arg).Value;

                    if (((XmlPropertyEnumNode)arg).Case != null &&
                        ((XmlPropertyEnumNode)arg).Case.Length > 0)
                    {
                        ((DNSXmlProperyNode<UInt32>)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<UInt32>[
                            ((XmlPropertyEnumNode)arg).Case.Length];
                        for (int i = 0; i < ((XmlPropertyEnumNode)arg).Case.Length; i++)
                        {
                            ((DNSXmlProperyNode<UInt32>)result).aPropertySwitchCases[i] = new DNSXmlProperySwitchCase<UInt32>();
                            ((DNSXmlProperyNode<UInt32>)result).aPropertySwitchCases[i].strFieldValue =
                                ((XmlPropertyEnumNode)arg).Case[i].FieldStringValue;
                            ((DNSXmlProperyNode<UInt32>)result).aPropertySwitchCases[i].PropertyValue =
                                ((XmlPropertyEnumNode)arg).Case[i].PropertyValue;
                        }
                    }
                    else
                    {
                        ((DNSXmlProperyNode<UInt32>)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<UInt32>[0];
                    }

                    ((DNSXmlProperyNode<UInt32>)result).strSwitchFieldName = arg.SwitchFieldName;
                    break;

                case DNEPropertyType._EPT_UINT:
                    result = new DNSXmlProperyNode<UInt32>();
                    ((DNSXmlProperyNode<UInt32>)result).strFieldName = ((XmlPropertyUIntNode)arg).FieldName;
                    ((DNSXmlProperyNode<UInt32>)result).strPropertyName = ((XmlPropertyUIntNode)arg).PropertyName;
                    ((DNSXmlProperyNode<UInt32>)result).Value = ((XmlPropertyUIntNode)arg).Value;

                    if (((XmlPropertyUIntNode)arg).Case != null &&
                        ((XmlPropertyUIntNode)arg).Case.Length > 0)
                    {
                        ((DNSXmlProperyNode<UInt32>)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<UInt32>[
                            ((XmlPropertyUIntNode)arg).Case.Length];
                        for (int i = 0; i < ((XmlPropertyUIntNode)arg).Case.Length; i++)
                        {
                            ((DNSXmlProperyNode<UInt32>)result).aPropertySwitchCases[i] = new DNSXmlProperySwitchCase<UInt32>();
                            ((DNSXmlProperyNode<UInt32>)result).aPropertySwitchCases[i].strFieldValue =
                                ((XmlPropertyUIntNode)arg).Case[i].FieldStringValue;
                            ((DNSXmlProperyNode<UInt32>)result).aPropertySwitchCases[i].PropertyValue =
                                ((XmlPropertyUIntNode)arg).Case[i].PropertyValue;
                        }
                    }
                    else
                    {
                        ((DNSXmlProperyNode<UInt32>)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<UInt32>[0];
                    }

                    ((DNSXmlProperyNode<UInt32>)result).strSwitchFieldName = arg.SwitchFieldName;
                    break;

                case DNEPropertyType._EPT_FLOAT:
                    result = new DNSXmlProperyNode<float>();
                    ((DNSXmlProperyNode<float>)result).strFieldName = ((XmlPropertyFloatNode)arg).FieldName;
                    ((DNSXmlProperyNode<float>)result).strPropertyName = ((XmlPropertyFloatNode)arg).PropertyName;
                    ((DNSXmlProperyNode<float>)result).Value = ((XmlPropertyFloatNode)arg).Value;

                    if (((XmlPropertyFloatNode)arg).Case != null &&
                        ((XmlPropertyFloatNode)arg).Case.Length > 0)
                    {
                        ((DNSXmlProperyNode<float>)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<float>[
                            ((XmlPropertyFloatNode)arg).Case.Length];
                        for (int i = 0; i < ((XmlPropertyFloatNode)arg).Case.Length; i++)
                        {
                            ((DNSXmlProperyNode<float>)result).aPropertySwitchCases[i] = new DNSXmlProperySwitchCase<float>();
                            ((DNSXmlProperyNode<float>)result).aPropertySwitchCases[i].strFieldValue =
                                ((XmlPropertyFloatNode)arg).Case[i].FieldStringValue;
                            ((DNSXmlProperyNode<float>)result).aPropertySwitchCases[i].PropertyValue =
                                ((XmlPropertyFloatNode)arg).Case[i].PropertyValue;
                        }
                    }
                    else
                    {
                        ((DNSXmlProperyNode<float>)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<float>[0];
                    }

                    ((DNSXmlProperyNode<float>)result).strSwitchFieldName = arg.SwitchFieldName;
                    break;

                case DNEPropertyType._EPT_DOUBLE:
                    result = new DNSXmlProperyNode<double>();
                    ((DNSXmlProperyNode<double>)result).strFieldName = ((XmlPropertyDoubleNode)arg).FieldName;
                    ((DNSXmlProperyNode<double>)result).strPropertyName = ((XmlPropertyDoubleNode)arg).PropertyName;
                    ((DNSXmlProperyNode<double>)result).Value = ((XmlPropertyDoubleNode)arg).Value;

                    if (((XmlPropertyDoubleNode)arg).Case != null &&
                        ((XmlPropertyDoubleNode)arg).Case.Length > 0)
                    {
                        ((DNSXmlProperyNode<double>)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<double>[
                            ((XmlPropertyDoubleNode)arg).Case.Length];
                        for (int i = 0; i < ((XmlPropertyDoubleNode)arg).Case.Length; i++)
                        {
                            ((DNSXmlProperyNode<double>)result).aPropertySwitchCases[i] = new DNSXmlProperySwitchCase<double>();
                            ((DNSXmlProperyNode<double>)result).aPropertySwitchCases[i].strFieldValue =
                                ((XmlPropertyDoubleNode)arg).Case[i].FieldStringValue;
                            ((DNSXmlProperyNode<double>)result).aPropertySwitchCases[i].PropertyValue =
                                ((XmlPropertyDoubleNode)arg).Case[i].PropertyValue;
                        }
                    }
                    else
                    {
                        ((DNSXmlProperyNode<double>)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<double>[0];
                    }

                    ((DNSXmlProperyNode<double>)result).strSwitchFieldName = arg.SwitchFieldName;
                    break;

                case DNEPropertyType._EPT_STRING:
                    result = new DNSXmlProperyNode<string>();
                    ((DNSXmlProperyNode<string>)result).strFieldName = ((XmlPropertyStringNode)arg).FieldName;
                    ((DNSXmlProperyNode<string>)result).strPropertyName = ((XmlPropertyStringNode)arg).PropertyName;
                    ((DNSXmlProperyNode<string>)result).Value = ((XmlPropertyStringNode)arg).Value;

                    if (((XmlPropertyStringNode)arg).Case != null &&
                        ((XmlPropertyStringNode)arg).Case.Length > 0)
                    {
                        ((DNSXmlProperyNode<string>)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<string>[
                            ((XmlPropertyStringNode)arg).Case.Length];
                        for (int i = 0; i < ((XmlPropertyStringNode)arg).Case.Length; i++)
                        {
                            ((DNSXmlProperyNode<string>)result).aPropertySwitchCases[i] = new DNSXmlProperySwitchCase<string>();
                            ((DNSXmlProperyNode<string>)result).aPropertySwitchCases[i].strFieldValue =
                                ((XmlPropertyStringNode)arg).Case[i].FieldStringValue;
                            ((DNSXmlProperyNode<string>)result).aPropertySwitchCases[i].PropertyValue =
                                ((XmlPropertyStringNode)arg).Case[i].PropertyValue;
                        }
                    }
                    else
                    {
                        ((DNSXmlProperyNode<string>)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<string>[0];
                    }

                    ((DNSXmlProperyNode<string>)result).strSwitchFieldName = arg.SwitchFieldName;
                    break;

                case DNEPropertyType._EPT_BCOLOR:
                    result = new DNSXmlProperyNode<DNSMcBColor>();

                    ((DNSXmlProperyNode<DNSMcBColor>)result).strPropertyName = ((XmlPropertyColorNode)arg).PropertyName;

                    if (((XmlPropertyColorNode)arg).FieldName != null &&
                        ((XmlPropertyColorNode)arg).FieldName != string.Empty)
                    {
                        ((DNSXmlProperyNode<DNSMcBColor>)result).strFieldName =
                                            ((XmlPropertyColorNode)arg).FieldName;
                    }
                    else
                    {
                        ((DNSXmlProperyNode<DNSMcBColor>)result).Value = new DNSMcBColor(
                            ((XmlPropertyColorNode)arg).Value.B,
                            ((XmlPropertyColorNode)arg).Value.G,
                            ((XmlPropertyColorNode)arg).Value.R,
                            ((XmlPropertyColorNode)arg).Value.A
                            );
                    }

                    if (((XmlPropertyColorNode)arg).Case != null &&
                        ((XmlPropertyColorNode)arg).Case.Length > 0)
                    {
                        ((DNSXmlProperyNode<DNSMcBColor>)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<DNSMcBColor>[
                            ((XmlPropertyColorNode)arg).Case.Length];
                        for (int i = 0; i < ((XmlPropertyColorNode)arg).Case.Length; i++)
                        {
                            ((DNSXmlProperyNode<DNSMcBColor>)result).aPropertySwitchCases[i] = new DNSXmlProperySwitchCase<DNSMcBColor>();
                            ((DNSXmlProperyNode<DNSMcBColor>)result).aPropertySwitchCases[i].strFieldValue =
                                ((XmlPropertyColorNode)arg).Case[i].FieldStringValue;

                            ((DNSXmlProperyNode<DNSMcBColor>)result).aPropertySwitchCases[i].PropertyValue =
                                new DNSMcBColor(
                                    ((XmlPropertyColorNode)arg).Case[i].PropertyValue.R,
                                    ((XmlPropertyColorNode)arg).Case[i].PropertyValue.G,
                                    ((XmlPropertyColorNode)arg).Case[i].PropertyValue.B,
                                    ((XmlPropertyColorNode)arg).Case[i].PropertyValue.A
                                    );
                        }
                    }
                    else
                    {
                        ((DNSXmlProperyNode<DNSMcBColor>)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<DNSMcBColor>[0];
                    }

                    ((DNSXmlProperyNode<DNSMcBColor>)result).strSwitchFieldName = arg.SwitchFieldName;
                    break;

                case DNEPropertyType._EPT_FVECTOR2D:
                    result = new DNSXmlFVector2DProperyNode();

                    ((DNSXmlFVector2DProperyNode)result).strPropertyName = ((XmlPropertyFVector2DNode)arg).PropertyName;

                    if (((XmlPropertyFVector2DNode)arg).FieldName.XFieldName != null &&
                        ((XmlPropertyFVector2DNode)arg).FieldName.XFieldName != string.Empty)
                    {
                        ((DNSXmlFVector2DProperyNode)result).X.strFieldName = ((XmlPropertyFVector2DNode)arg).FieldName.XFieldName;
                    }
                    else
                    {
                        ((DNSXmlFVector2DProperyNode)result).X.Value = ((XmlPropertyFVector2DNode)arg).Value.x;
                    }

                    if (((XmlPropertyFVector2DNode)arg).FieldName.YFieldName != null &&
                        ((XmlPropertyFVector2DNode)arg).FieldName.YFieldName != string.Empty)
                    {
                        ((DNSXmlFVector2DProperyNode)result).Y.strFieldName = ((XmlPropertyFVector2DNode)arg).FieldName.YFieldName;
                    }
                    else
                    {
                        ((DNSXmlFVector2DProperyNode)result).Y.Value = ((XmlPropertyFVector2DNode)arg).Value.y;
                    }


                    if (((XmlPropertyFVector2DNode)arg).Case != null &&
                        ((XmlPropertyFVector2DNode)arg).Case.Length > 0)
                    {
                        ((DNSXmlFVector2DProperyNode)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<DNSMcFVector2D>[
                            ((XmlPropertyFVector2DNode)arg).Case.Length];
                        for (int i = 0; i < ((XmlPropertyFVector2DNode)arg).Case.Length; i++)
                        {
                            ((DNSXmlFVector2DProperyNode)result).aPropertySwitchCases[i] = new DNSXmlProperySwitchCase<DNSMcFVector2D>();
                            ((DNSXmlFVector2DProperyNode)result).aPropertySwitchCases[i].strFieldValue =
                                ((XmlPropertyFVector2DNode)arg).Case[i].FieldStringValue;

                            ((DNSXmlFVector2DProperyNode)result).aPropertySwitchCases[i].PropertyValue.x =
                                ((XmlPropertyFVector2DNode)arg).Case[i].PropertyValue.x;
                            ((DNSXmlFVector2DProperyNode)result).aPropertySwitchCases[i].PropertyValue.y =
                                ((XmlPropertyFVector2DNode)arg).Case[i].PropertyValue.y;
                        }
                    }
                    else
                    {
                        ((DNSXmlFVector2DProperyNode)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<DNSMcFVector2D>[0];
                    }

                    ((DNSXmlFVector2DProperyNode)result).strSwitchFieldName = arg.SwitchFieldName;
                    break;

                case DNEPropertyType._EPT_VECTOR2D:
                    result = new DNSXmlVector2DProperyNode();

                    ((DNSXmlVector2DProperyNode)result).strPropertyName = ((XmlPropertyVector2DNode)arg).PropertyName;

                    if (((XmlPropertyVector2DNode)arg).FieldName.XFieldName != null &&
                        ((XmlPropertyVector2DNode)arg).FieldName.XFieldName != string.Empty)
                    {
                        ((DNSXmlVector2DProperyNode)result).X.strFieldName = ((XmlPropertyVector2DNode)arg).FieldName.XFieldName;
                    }
                    else
                    {
                        ((DNSXmlVector2DProperyNode)result).X.Value = ((XmlPropertyVector2DNode)arg).Value.x;
                    }

                    if (((XmlPropertyVector2DNode)arg).FieldName.YFieldName != null &&
                        ((XmlPropertyVector2DNode)arg).FieldName.YFieldName != string.Empty)
                    {
                        ((DNSXmlVector2DProperyNode)result).Y.strFieldName = ((XmlPropertyVector2DNode)arg).FieldName.YFieldName;
                    }
                    else
                    {
                        ((DNSXmlVector2DProperyNode)result).Y.Value = ((XmlPropertyVector2DNode)arg).Value.y;
                    }


                    if (((XmlPropertyVector2DNode)arg).Case != null &&
                        ((XmlPropertyVector2DNode)arg).Case.Length > 0)
                    {
                        ((DNSXmlVector2DProperyNode)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<DNSMcVector2D>[
                            ((XmlPropertyVector2DNode)arg).Case.Length];
                        for (int i = 0; i < ((XmlPropertyVector2DNode)arg).Case.Length; i++)
                        {
                            ((DNSXmlVector2DProperyNode)result).aPropertySwitchCases[i] = new DNSXmlProperySwitchCase<DNSMcVector2D>();
                            ((DNSXmlVector2DProperyNode)result).aPropertySwitchCases[i].strFieldValue =
                                ((XmlPropertyVector2DNode)arg).Case[i].FieldStringValue;
                            ((DNSXmlVector2DProperyNode)result).aPropertySwitchCases[i].PropertyValue.x =
                                ((XmlPropertyVector2DNode)arg).Case[i].PropertyValue.x;
                            ((DNSXmlVector2DProperyNode)result).aPropertySwitchCases[i].PropertyValue.y =
                                ((XmlPropertyVector2DNode)arg).Case[i].PropertyValue.y;
                        }
                    }
                    else
                    {
                        ((DNSXmlVector2DProperyNode)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<DNSMcVector2D>[0];
                    }

                    ((DNSXmlVector2DProperyNode)result).strSwitchFieldName = arg.SwitchFieldName;
                    break;

                case DNEPropertyType._EPT_FVECTOR3D:
                    result = new DNSXmlFVector3DProperyNode();

                    ((DNSXmlFVector3DProperyNode)result).strPropertyName = ((XmlPropertyFVector3DNode)arg).PropertyName;

                    if (((XmlPropertyFVector3DNode)arg).FieldName.XFieldName != null &&
                        ((XmlPropertyFVector3DNode)arg).FieldName.XFieldName != string.Empty)
                    {
                        ((DNSXmlFVector3DProperyNode)result).X.strFieldName = ((XmlPropertyFVector3DNode)arg).FieldName.XFieldName;
                    }
                    else
                    {
                        ((DNSXmlFVector3DProperyNode)result).X.Value = ((XmlPropertyFVector3DNode)arg).Value.x;
                    }

                    if (((XmlPropertyFVector3DNode)arg).FieldName.YFieldName != null &&
                        ((XmlPropertyFVector3DNode)arg).FieldName.YFieldName != string.Empty)
                    {
                        ((DNSXmlFVector3DProperyNode)result).Y.strFieldName = ((XmlPropertyFVector3DNode)arg).FieldName.YFieldName;
                    }
                    else
                    {
                        ((DNSXmlFVector3DProperyNode)result).Y.Value = ((XmlPropertyFVector3DNode)arg).Value.y;
                    }

                    if (((XmlPropertyFVector3DNode)arg).FieldName.ZFieldName != null &&
                        ((XmlPropertyFVector3DNode)arg).FieldName.ZFieldName != string.Empty)
                    {
                        ((DNSXmlFVector3DProperyNode)result).Z.strFieldName = ((XmlPropertyFVector3DNode)arg).FieldName.ZFieldName;
                    }
                    else
                    {
                        ((DNSXmlFVector3DProperyNode)result).Z.Value = ((XmlPropertyFVector3DNode)arg).Value.z;
                    }

                    if (((XmlPropertyFVector3DNode)arg).Case != null &&
                        ((XmlPropertyFVector3DNode)arg).Case.Length > 0)
                    {
                        ((DNSXmlFVector3DProperyNode)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<DNSMcFVector3D>[
                            ((XmlPropertyFVector3DNode)arg).Case.Length];
                        for (int i = 0; i < ((XmlPropertyFVector3DNode)arg).Case.Length; i++)
                        {
                            ((DNSXmlFVector3DProperyNode)result).aPropertySwitchCases[i] = new DNSXmlProperySwitchCase<DNSMcFVector3D>();
                            ((DNSXmlFVector3DProperyNode)result).aPropertySwitchCases[i].strFieldValue =
                                ((XmlPropertyFVector3DNode)arg).Case[i].FieldStringValue;
                            ((DNSXmlFVector3DProperyNode)result).aPropertySwitchCases[i].PropertyValue.x =
                                ((XmlPropertyFVector3DNode)arg).Case[i].PropertyValue.x;
                            ((DNSXmlFVector3DProperyNode)result).aPropertySwitchCases[i].PropertyValue.y =
                                ((XmlPropertyFVector3DNode)arg).Case[i].PropertyValue.y;
                            ((DNSXmlFVector3DProperyNode)result).aPropertySwitchCases[i].PropertyValue.z =
                                ((XmlPropertyFVector3DNode)arg).Case[i].PropertyValue.z;
                        }
                    }
                    else
                    {
                        ((DNSXmlFVector3DProperyNode)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<DNSMcFVector3D>[0];
                    }

                    ((DNSXmlFVector3DProperyNode)result).strSwitchFieldName = arg.SwitchFieldName;
                    break;

                case DNEPropertyType._EPT_VECTOR3D:
                    result = new DNSXmlVector3DProperyNode();

                    ((DNSXmlVector3DProperyNode)result).strPropertyName = ((XmlPropertyVector3DNode)arg).PropertyName;

                    if (((XmlPropertyVector3DNode)arg).FieldName.XFieldName != null &&
                        ((XmlPropertyVector3DNode)arg).FieldName.XFieldName != string.Empty)
                    {
                        ((DNSXmlVector3DProperyNode)result).X.strFieldName = ((XmlPropertyVector3DNode)arg).FieldName.XFieldName;
                    }
                    else
                    {
                        ((DNSXmlVector3DProperyNode)result).X.Value = ((XmlPropertyVector3DNode)arg).Value.x;
                    }

                    if (((XmlPropertyVector3DNode)arg).FieldName.YFieldName != null &&
                        ((XmlPropertyVector3DNode)arg).FieldName.YFieldName != string.Empty)
                    {
                        ((DNSXmlVector3DProperyNode)result).Y.strFieldName = ((XmlPropertyVector3DNode)arg).FieldName.YFieldName;
                    }
                    else
                    {
                        ((DNSXmlVector3DProperyNode)result).Y.Value = ((XmlPropertyVector3DNode)arg).Value.y;
                    }

                    if (((XmlPropertyVector3DNode)arg).FieldName.ZFieldName != null &&
                        ((XmlPropertyVector3DNode)arg).FieldName.ZFieldName != string.Empty)
                    {
                        ((DNSXmlVector3DProperyNode)result).Z.strFieldName = ((XmlPropertyVector3DNode)arg).FieldName.ZFieldName;
                    }
                    else
                    {
                        ((DNSXmlVector3DProperyNode)result).Z.Value = ((XmlPropertyVector3DNode)arg).Value.z;
                    }

                    if (((XmlPropertyVector3DNode)arg).Case != null &&
                        ((XmlPropertyVector3DNode)arg).Case.Length > 0)
                    {
                        ((DNSXmlVector3DProperyNode)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<DNSMcVector3D>[
                            ((XmlPropertyVector3DNode)arg).Case.Length];
                        for (int i = 0; i < ((XmlPropertyVector3DNode)arg).Case.Length; i++)
                        {
                            ((DNSXmlVector3DProperyNode)result).aPropertySwitchCases[i] = new DNSXmlProperySwitchCase<DNSMcVector3D>();
                            ((DNSXmlVector3DProperyNode)result).aPropertySwitchCases[i].strFieldValue =
                                ((XmlPropertyVector3DNode)arg).Case[i].FieldStringValue;
                            ((DNSXmlVector3DProperyNode)result).aPropertySwitchCases[i].PropertyValue.x =
                                ((XmlPropertyVector3DNode)arg).Case[i].PropertyValue.x;
                            ((DNSXmlVector3DProperyNode)result).aPropertySwitchCases[i].PropertyValue.y =
                                ((XmlPropertyVector3DNode)arg).Case[i].PropertyValue.y;
                            ((DNSXmlVector3DProperyNode)result).aPropertySwitchCases[i].PropertyValue.z =
                                ((XmlPropertyVector3DNode)arg).Case[i].PropertyValue.z;
                        }
                    }
                    else
                    {
                        ((DNSXmlVector3DProperyNode)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<DNSMcVector3D>[0];
                    }

                    ((DNSXmlVector3DProperyNode)result).strSwitchFieldName = arg.SwitchFieldName;
                    break;

                case DNEPropertyType._EPT_TEXTURE:
                    result = new DNSXmlTextureProperyNode();

                    ((DNSXmlTextureProperyNode)result).strPropertyName = ((XmlPropertyTextureNode)arg).PropertyName;

                    if (((XmlPropertyTextureNode)arg).FieldName.IgnoreTransparentMargin != null &&
                        ((XmlPropertyTextureNode)arg).FieldName.IgnoreTransparentMargin != string.Empty)
                    {
                        ((DNSXmlTextureProperyNode)result).IgnoreTransparentMargin.strFieldName = 
                                            ((XmlPropertyTextureNode)arg).FieldName.IgnoreTransparentMargin;
                    }
                    else
                    {
                        ((DNSXmlTextureProperyNode)result).IgnoreTransparentMargin.Value =
                                            ((XmlPropertyTextureNode)arg).Value.IgnoreTransparentMargin;
                    }

                    if (((XmlPropertyTextureNode)arg).FieldName.ImageFileName != null &&
                        ((XmlPropertyTextureNode)arg).FieldName.ImageFileName != string.Empty)
                    {
                        ((DNSXmlTextureProperyNode)result).ImageFileName.strFieldName = ((XmlPropertyTextureNode)arg).FieldName.ImageFileName;
                    }
                    else
                    {
                        ((DNSXmlTextureProperyNode)result).ImageFileName.Value = ((XmlPropertyTextureNode)arg).Value.ImageFileName;
                    }

                    if (((XmlPropertyTextureNode)arg).FieldName.IsTransparentColorEnabled != null &&
                        ((XmlPropertyTextureNode)arg).FieldName.IsTransparentColorEnabled != string.Empty)
                    {
                        ((DNSXmlTextureProperyNode)result).IsTransparentColorEnabled.strFieldName = 
                                        ((XmlPropertyTextureNode)arg).FieldName.IsTransparentColorEnabled;
                    }
                    else
                    {
                        ((DNSXmlTextureProperyNode)result).IsTransparentColorEnabled.Value = 
                                        ((XmlPropertyTextureNode)arg).Value.IsTransparentColorEnabled;
                    }
                    if (((XmlPropertyTextureNode)arg).FieldName.TransparentColor != null &&
                        ((XmlPropertyTextureNode)arg).FieldName.TransparentColor != string.Empty)
                    {
                        ((DNSXmlTextureProperyNode)result).TransparentColor.strFieldName =
                                       ((XmlPropertyTextureNode)arg).FieldName.TransparentColor;
                    }
                    else { 
                        ((DNSXmlTextureProperyNode)result).TransparentColor.Value =
                            new DNSMcBColor(
                                        ((XmlPropertyTextureNode)arg).Value.TransparentColor.R,
                                        ((XmlPropertyTextureNode)arg).Value.TransparentColor.G,
                                        ((XmlPropertyTextureNode)arg).Value.TransparentColor.B,
                                        ((XmlPropertyTextureNode)arg).Value.TransparentColor.A);
                    }


                    if (((XmlPropertyTextureNode)arg).Case != null &&
                        ((XmlPropertyTextureNode)arg).Case.Length > 0)
                    {
                        ((DNSXmlTextureProperyNode)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<DNSXmlTextureDef>[
                            ((XmlPropertyTextureNode)arg).Case.Length];
                        for (int i = 0; i < ((XmlPropertyTextureNode)arg).Case.Length; i++)
                        {
                            ((DNSXmlTextureProperyNode)result).aPropertySwitchCases[i] = new DNSXmlProperySwitchCase<DNSXmlTextureDef>();

                            ((DNSXmlTextureProperyNode)result).aPropertySwitchCases[i].strFieldValue =
                                ((XmlPropertyTextureNode)arg).Case[i].FieldStringValue;
                            ((DNSXmlTextureProperyNode)result).aPropertySwitchCases[i].PropertyValue.bIgnoreTransparentMargin =
                                ((XmlPropertyTextureNode)arg).Case[i].PropertyValue.IgnoreTransparentMargin;
                            ((DNSXmlTextureProperyNode)result).aPropertySwitchCases[i].PropertyValue.strImageFileName =
                                ((XmlPropertyTextureNode)arg).Case[i].PropertyValue.ImageFileName;
                            ((DNSXmlTextureProperyNode)result).aPropertySwitchCases[i].PropertyValue.TransparentColor =
                                new DNSMcBColor(
                                    ((XmlPropertyTextureNode)arg).Case[i].PropertyValue.TransparentColor.R,
                                    ((XmlPropertyTextureNode)arg).Case[i].PropertyValue.TransparentColor.G,
                                    ((XmlPropertyTextureNode)arg).Case[i].PropertyValue.TransparentColor.B,
                                    ((XmlPropertyTextureNode)arg).Case[i].PropertyValue.TransparentColor.A);
                        }
                    }
                    else
                    {
                        ((DNSXmlTextureProperyNode)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<DNSXmlTextureDef>[0];
                    }

                    ((DNSXmlTextureProperyNode)result).strSwitchFieldName = arg.SwitchFieldName;
                    break;

                case DNEPropertyType._EPT_FONT:
                    result = new DNSXmlFontProperyNode();

                    ((DNSXmlFontProperyNode)result).strPropertyName = ((XmlPropertyFontTypeNode)arg).PropertyName;

                    if (((XmlPropertyFontTypeNode)arg).FieldName.CharSet != null &&
                        ((XmlPropertyFontTypeNode)arg).FieldName.CharSet != string.Empty)
                    {
                        ((DNSXmlFontProperyNode)result).CharSet.strFieldName =
                                            ((XmlPropertyFontTypeNode)arg).FieldName.CharSet;
                    }
                    else
                    {
                        ((DNSXmlFontProperyNode)result).CharSet.Value =
                                            ((XmlPropertyFontTypeNode)arg).Value.CharSet;
                    }

                    if (((XmlPropertyFontTypeNode)arg).FieldName.FaceName != null &&
                        ((XmlPropertyFontTypeNode)arg).FieldName.FaceName != string.Empty)
                    {
                        ((DNSXmlFontProperyNode)result).FaceName.strFieldName = ((XmlPropertyFontTypeNode)arg).FieldName.FaceName;
                    }
                    else
                    {
                        ((DNSXmlFontProperyNode)result).FaceName.Value = ((XmlPropertyFontTypeNode)arg).Value.FaceName;
                    }

                    if (((XmlPropertyFontTypeNode)arg).FieldName.Height != null &&
                        ((XmlPropertyFontTypeNode)arg).FieldName.Height != string.Empty)
                    {
                        ((DNSXmlFontProperyNode)result).Height.strFieldName =
                                        ((XmlPropertyFontTypeNode)arg).FieldName.Height;
                    }
                    else
                    {
                        ((DNSXmlFontProperyNode)result).Height.Value =
                                        (int)((XmlPropertyFontTypeNode)arg).Value.FontHeight;
                    }

                    if (((XmlPropertyFontTypeNode)arg).Case != null &&
                        ((XmlPropertyFontTypeNode)arg).Case.Length > 0)
                    {
                        ((DNSXmlFontProperyNode)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<DNSXmlFontDef>[
                            ((XmlPropertyFontTypeNode)arg).Case.Length];
                        for (int i = 0; i < ((XmlPropertyFontTypeNode)arg).Case.Length; i++)
                        {
                            ((DNSXmlFontProperyNode)result).aPropertySwitchCases[i] = new DNSXmlProperySwitchCase<DNSXmlFontDef>();

                            ((DNSXmlFontProperyNode)result).aPropertySwitchCases[i].strFieldValue =
                                ((XmlPropertyFontTypeNode)arg).Case[i].FieldStringValue;
                            ((DNSXmlFontProperyNode)result).aPropertySwitchCases[i].PropertyValue.nHeight =
                                (int)((XmlPropertyFontTypeNode)arg).Case[i].PropertyValue.FontHeight;
                            ((DNSXmlFontProperyNode)result).aPropertySwitchCases[i].PropertyValue.strFaceName =
                                ((XmlPropertyFontTypeNode)arg).Case[i].PropertyValue.FaceName;
                            ((DNSXmlFontProperyNode)result).aPropertySwitchCases[i].PropertyValue.uCharSet =
                                ((XmlPropertyFontTypeNode)arg).Case[i].PropertyValue.CharSet;
                        }
                    }
                    else
                    {
                        ((DNSXmlFontProperyNode)result).aPropertySwitchCases = new DNSXmlProperySwitchCase<DNSXmlFontDef>[0];
                    }

                    ((DNSXmlFontProperyNode)result).strSwitchFieldName = arg.SwitchFieldName;
                    break;

                case DNEPropertyType._EPT_CONDITIONALSELECTOR:
                    result = new DNSXmlScaleConditionalSelectorProperyNode();

                    ((DNSXmlScaleConditionalSelectorProperyNode)result).strPropertyName = ((XmlPropertyScaleCondSelectorNode)arg).PropertyName;

                    if (((XmlPropertyScaleCondSelectorNode)arg).FieldName.CancelScaleModeFieldName != null &&
                        ((XmlPropertyScaleCondSelectorNode)arg).FieldName.CancelScaleModeFieldName != string.Empty)
                    {
                        ((DNSXmlScaleConditionalSelectorProperyNode)result).CancelScaleMode.strFieldName =
                                            ((XmlPropertyScaleCondSelectorNode)arg).FieldName.CancelScaleModeFieldName;
                    }
                    else
                    {
                        ((DNSXmlScaleConditionalSelectorProperyNode)result).CancelScaleMode.Value =
                                            ((XmlPropertyScaleCondSelectorNode)arg).Value.CancelScaleMode;
                    }

                    if (((XmlPropertyScaleCondSelectorNode)arg).FieldName.CancelScaleModeResult != null &&
                        ((XmlPropertyScaleCondSelectorNode)arg).FieldName.CancelScaleModeResult != string.Empty)
                    {
                        ((DNSXmlScaleConditionalSelectorProperyNode)result).CancelScaleModeResult.strFieldName = 
                            ((XmlPropertyScaleCondSelectorNode)arg).FieldName.CancelScaleModeResult;
                    }
                    else
                    {
                        ((DNSXmlScaleConditionalSelectorProperyNode)result).CancelScaleModeResult.Value =
                            ((XmlPropertyScaleCondSelectorNode)arg).Value.CancelScaleModeResult;
                    }

                    if (((XmlPropertyScaleCondSelectorNode)arg).FieldName.MaxFieldName != null &&
                        ((XmlPropertyScaleCondSelectorNode)arg).FieldName.MaxFieldName != string.Empty)
                    {
                        ((DNSXmlScaleConditionalSelectorProperyNode)result).Max.strFieldName =
                                        ((XmlPropertyScaleCondSelectorNode)arg).FieldName.MaxFieldName;
                    }
                    else
                    {
                        ((DNSXmlScaleConditionalSelectorProperyNode)result).Max.Value =
                                        (int)((XmlPropertyScaleCondSelectorNode)arg).Value.Max;
                    }

                    if (((XmlPropertyScaleCondSelectorNode)arg).FieldName.MinFieldName != null &&
                        ((XmlPropertyScaleCondSelectorNode)arg).FieldName.MinFieldName != string.Empty)
                    {
                        ((DNSXmlScaleConditionalSelectorProperyNode)result).Min.Value =
                                        (int)((XmlPropertyScaleCondSelectorNode)arg).Value.Min;
                    }
                    else
                    {
                        ((DNSXmlScaleConditionalSelectorProperyNode)result).Min.strFieldName =
                                        ((XmlPropertyScaleCondSelectorNode)arg).FieldName.MinFieldName;
                    }


                    if (((XmlPropertyScaleCondSelectorNode)arg).Case != null &&
                        ((XmlPropertyScaleCondSelectorNode)arg).Case.Length > 0)
                    {
                        ((DNSXmlScaleConditionalSelectorProperyNode)result).aPropertySwitchCases = 
                            new DNSXmlProperySwitchCase<DNSXmlScaleConditionalSelectorDef>[
                                ((XmlPropertyScaleCondSelectorNode)arg).Case.Length];
                        for (int i = 0; i < ((XmlPropertyScaleCondSelectorNode)arg).Case.Length; i++)
                        {
                            ((DNSXmlScaleConditionalSelectorProperyNode)result).aPropertySwitchCases[i] =
                                new DNSXmlProperySwitchCase<DNSXmlScaleConditionalSelectorDef>();
                            ((DNSXmlScaleConditionalSelectorProperyNode)result).aPropertySwitchCases[i].strFieldValue =
                                ((XmlPropertyScaleCondSelectorNode)arg).Case[i].FieldStringValue;
                            ((DNSXmlScaleConditionalSelectorProperyNode)result).aPropertySwitchCases[i].PropertyValue.fMaxScale =
                                (int)((XmlPropertyScaleCondSelectorNode)arg).Case[i].PropertyValue.Max;
                            ((DNSXmlScaleConditionalSelectorProperyNode)result).aPropertySwitchCases[i].PropertyValue.fMinScale =
                                ((XmlPropertyScaleCondSelectorNode)arg).Case[i].PropertyValue.Min;
                            ((DNSXmlScaleConditionalSelectorProperyNode)result).aPropertySwitchCases[i].PropertyValue.uCancelScaleMode =
                                ((XmlPropertyScaleCondSelectorNode)arg).Case[i].PropertyValue.CancelScaleMode;
                            ((DNSXmlScaleConditionalSelectorProperyNode)result).aPropertySwitchCases[i].PropertyValue.uCancelScaleModeResult =
                                ((XmlPropertyScaleCondSelectorNode)arg).Case[i].PropertyValue.CancelScaleModeResult;
                        }
                    }
                    else
                    {
                        ((DNSXmlScaleConditionalSelectorProperyNode)result).aPropertySwitchCases = 
                            new DNSXmlProperySwitchCase<DNSXmlScaleConditionalSelectorDef>[0];
                    }

                    ((DNSXmlScaleConditionalSelectorProperyNode)result).strSwitchFieldName = arg.SwitchFieldName;
                    break;
            }

            return result;
        }

        public static XmlBasePropertyNode[] FromDNS(DNSXmlBaseProperyNode[] arg)
        {
            List<XmlBasePropertyNode> result = new List<XmlBasePropertyNode>();

            if (arg != null)
            {
                foreach (DNSXmlBaseProperyNode item in arg)
                {
                    XmlBasePropertyNode xmlItem = (XmlBasePropertyNode)item;
                    xmlItem.IsUsed = true;
                    result.Add(xmlItem);
                }
            }

            return result.ToArray();
        }


        [DisplayName("- Property Type")]
        public DNEPropertyType PropertyType
        {
            get
            {
                return _propType;
            }
            set
            {
                _propType = value;
            }
        }

        public static DNSXmlBaseProperyNode[] ToDNS(XmlBasePropertyNode[] arg)
        {
            List<DNSXmlBaseProperyNode> result = new List<DNSXmlBaseProperyNode>();

            if (arg != null)
            {
                foreach (XmlBasePropertyNode item in arg)
                {
                    if (item != null && item.IsUsed)
                    {
                        result.Add((DNSXmlBaseProperyNode)item);
                    }
                }
            }

            return result.ToArray();
        }
        [DisplayName("A. Is used")]
        [Description("Is the property used to generate the schema")]
        public bool IsUsed { get; set; }
        [DisplayName("- Property name")]
        [Description("The property name as taken from the scheme")]
        [ReadOnly(true)]
        public string PropertyName { get; set; }
        [DisplayName("D. Switch field name")]
        [Editor(typeof(FieldNamesEditorType), typeof(UITypeEditor))]
        public virtual string SwitchFieldName { get; set; }

    }

    [Browsable(true)]
    public class XmlPropertyBoolNode : XmlBasePropertyNode
    {
        string m_fieldName;

        public struct XmlPropertyCaseBoolNode
        {
            [DisplayName("Field string value")]
            [Editor(typeof(FieldValuesEditorType), typeof(UITypeEditor))]
            public string FieldStringValue { get; set; }
            [DisplayName("Property value")]
            public bool PropertyValue { get; set; }

            internal static string CaseFieldName { get; set; } // TODO: fix to support a case of 2 PrivateProperties of the same type
        }

        public XmlPropertyBoolNode()
            : base()
        {
            Case = new XmlPropertyCaseBoolNode[0];
            _propType = DNEPropertyType._EPT_BOOL;
        }

        [DisplayName("B. Field name")]
        [Description("The data source(s) field used to fill the property")]
        [Editor(typeof(FieldNamesEditorType), typeof(UITypeEditor))]
        public string FieldName { 
            get
            {
                return m_fieldName;
            }
            set
            {
                m_fieldName = value;
                XmlPropertyCaseBoolNode.CaseFieldName = value;
            }
        }
        [DisplayName("C. Value")]
        public bool Value { get; set; }

        public override string SwitchFieldName
        {
            get
            {
                return base.SwitchFieldName;
            }
            set
            {
                base.SwitchFieldName = value;
                XmlPropertyCaseBoolNode.CaseFieldName = value;
            }
        }

        [DisplayName("E. Case")]
        public XmlPropertyCaseBoolNode[] Case { get; set; }
    }

    [Browsable(true)]
    public class XmlPropertyByteNode : XmlBasePropertyNode
    {
        public struct XmlPropertyCaseByteNode
        {
            [DisplayName("Field string value")]
            [Editor(typeof(FieldValuesEditorType), typeof(UITypeEditor))]
            public string FieldStringValue { get; set; }
            [DisplayName("Property value")]
            public byte PropertyValue { get; set; }

            internal static string CaseFieldName { get; set; }
        }

        public XmlPropertyByteNode()
            : base()
        {
            Case = new XmlPropertyCaseByteNode[0];
            _propType = DNEPropertyType._EPT_BYTE;
        }

        [DisplayName("B. Field name")]
        [Description("The data source(s) field used to fill the property")]
        [Editor(typeof(FieldNamesEditorType), typeof(UITypeEditor))]
        public string FieldName { get; set; }

        [DisplayName("C. Value")]
        public byte Value { get; set; }

        public override string SwitchFieldName
        {
            get
            {
                return base.SwitchFieldName;
            }
            set
            {
                base.SwitchFieldName = value;
                XmlPropertyCaseByteNode.CaseFieldName = value;
            }
        }

        [DisplayName("E. Case")]
        public XmlPropertyCaseByteNode[] Case { get; set; }
    }

    [Browsable(true)]
    public class XmlPropertySByteNode : XmlBasePropertyNode
    {
        public struct XmlPropertyCaseSByteNode
        {
            [DisplayName("Field string value")]
            [Editor(typeof(FieldValuesEditorType), typeof(UITypeEditor))]
            public string FieldStringValue { get; set; }
            [DisplayName("Property value")]
            public sbyte PropertyValue { get; set; }

            internal static string CaseFieldName { get; set; }
        }

        public XmlPropertySByteNode()
            : base()
        {
            Case = new XmlPropertyCaseSByteNode[0];
            _propType = DNEPropertyType._EPT_SBYTE;
        }

        [DisplayName("B. Field name")]
        [Description("The data source(s) field used to fill the property")]
        [Editor(typeof(FieldNamesEditorType), typeof(UITypeEditor))]
        public string FieldName { get; set; }

        [DisplayName("C. Value")]
        public sbyte Value { get; set; }

        public override string SwitchFieldName
        {
            get
            {
                return base.SwitchFieldName;
            }
            set
            {
                base.SwitchFieldName = value;
                XmlPropertyCaseSByteNode.CaseFieldName = value;
            }
        }

        [DisplayName("E. Case")]
        public XmlPropertyCaseSByteNode[] Case { get; set; }
    }

    [Browsable(true)]
    public class XmlPropertyIntNode : XmlBasePropertyNode
    {
        public struct XmlPropertyCaseIntNode
        {
            [DisplayName("Field string value")]
            [Editor(typeof(FieldValuesEditorType), typeof(UITypeEditor))]
            public string FieldStringValue { get; set; }
            [DisplayName("Property value")]
            public int PropertyValue { get; set; }

            internal static string CaseFieldName { get; set; }
        }

        public XmlPropertyIntNode()
            : base()
        {
            Case = new XmlPropertyCaseIntNode[0];
            _propType = DNEPropertyType._EPT_INT;
        }

        [DisplayName("B. Field name")]
        [Description("The data source(s) field used to fill the property")]
        [Editor(typeof(FieldNamesEditorType), typeof(UITypeEditor))]
        public string FieldName { get; set; }

        [DisplayName("C. Value")]
        public int Value { get; set; }

        public override string SwitchFieldName
        {
            get
            {
                return base.SwitchFieldName;
            }
            set
            {
                base.SwitchFieldName = value;
                XmlPropertyCaseIntNode.CaseFieldName = value;
            }
        }

        [DisplayName("E. Case")]
        public XmlPropertyCaseIntNode[] Case { get; set; }
    }

    [Browsable(true)]
    public class XmlPropertyUIntNode : XmlBasePropertyNode
    {
        public struct XmlPropertyCaseUIntNode
        {
            [DisplayName("Field string value")]
            [Editor(typeof(FieldValuesEditorType), typeof(UITypeEditor))]
            public string FieldStringValue { get; set; }
            [DisplayName("Property value")]
            public UInt32 PropertyValue { get; set; }

            internal static string CaseFieldName { get; set; }
        }

        public XmlPropertyUIntNode()
            : base()
        {
            Case = new XmlPropertyCaseUIntNode[0];
            _propType = DNEPropertyType._EPT_UINT;
        }

        [DisplayName("B. Field name")]
        [Description("The data source(s) field used to fill the property")]
        [Editor(typeof(FieldNamesEditorType), typeof(UITypeEditor))]
        public string FieldName { get; set; }

        [DisplayName("C. Value")]
        public UInt32 Value { get; set; }

        public override string SwitchFieldName
        {
            get
            {
                return base.SwitchFieldName;
            }
            set
            {
                base.SwitchFieldName = value;
                XmlPropertyCaseUIntNode.CaseFieldName = value;
            }
        }

        [DisplayName("E. Case")]
        public XmlPropertyCaseUIntNode[] Case { get; set; }
    }

    [Browsable(true)]
    public class XmlPropertyEnumNode : XmlBasePropertyNode
    {
        public struct XmlPropertyCaseEnumNode
        {
            [DisplayName("Field string value")]
            [Editor(typeof(FieldValuesEditorType), typeof(UITypeEditor))]
            public string FieldStringValue { get; set; }
            [DisplayName("Property value")]
            public UInt32 PropertyValue { get; set; }

            internal static string CaseFieldName { get; set; }
        }

        public XmlPropertyEnumNode()
            : base()
        {
            Case = new XmlPropertyCaseEnumNode[0];
            _propType = DNEPropertyType._EPT_ENUM;
        }

        [DisplayName("B. Field name")]
        [Description("The data source(s) field used to fill the property")]
        [Editor(typeof(FieldNamesEditorType), typeof(UITypeEditor))]
        public string FieldName { get; set; }

        [DisplayName("C. Value")]
        public UInt32 Value { get; set; }

        public override string SwitchFieldName
        {
            get
            {
                return base.SwitchFieldName;
            }
            set
            {
                base.SwitchFieldName = value;
                XmlPropertyCaseEnumNode.CaseFieldName = value;
            }
        }

        [DisplayName("E. Case")]
        public XmlPropertyCaseEnumNode[] Case { get; set; }
    }


    [Browsable(true)]
    public class XmlPropertyFloatNode : XmlBasePropertyNode
    {
        public struct XmlPropertyCaseFloatNode
        {
            [DisplayName("Field string value")]
            [Editor(typeof(FieldValuesEditorType), typeof(UITypeEditor))]
            public string FieldStringValue { get; set; }
            [DisplayName("Property value")]
            public float PropertyValue { get; set; }

            internal static string CaseFieldName { get; set; }
        }

        public XmlPropertyFloatNode()
            : base()
        {
            Case = new XmlPropertyCaseFloatNode[0];
            _propType = DNEPropertyType._EPT_FLOAT;
        }

        [DisplayName("B. Field name")]
        [Description("The data source(s) field used to fill the property")]
        [Editor(typeof(FieldNamesEditorType), typeof(UITypeEditor))]
        public string FieldName { get; set; }

        [DisplayName("C. Value")]
        public float Value { get; set; }

        public override string SwitchFieldName
        {
            get
            {
                return base.SwitchFieldName;
            }
            set
            {
                base.SwitchFieldName = value;
                XmlPropertyCaseFloatNode.CaseFieldName = value;
            }
        }

        [DisplayName("E. Case")]
        public XmlPropertyCaseFloatNode[] Case { get; set; }
    }

    [Browsable(true)]
    public class XmlPropertyDoubleNode : XmlBasePropertyNode
    {
        public struct XmlPropertyCaseDoubleNode
        {
            [DisplayName("Field string value")]
            [Editor(typeof(FieldValuesEditorType), typeof(UITypeEditor))]
            public string FieldStringValue { get; set; }
            [DisplayName("Property value")]
            public double PropertyValue { get; set; }

            internal static string CaseFieldName { get; set; }
        }

        public XmlPropertyDoubleNode()
            : base()
        {
            Case = new XmlPropertyCaseDoubleNode[0];
            _propType = DNEPropertyType._EPT_DOUBLE;
        }

        [DisplayName("B. Field name")]
        [Description("The data source(s) field used to fill the property")]
        [Editor(typeof(FieldNamesEditorType), typeof(UITypeEditor))]
        public string FieldName { get; set; }

        [DisplayName("C. Value")]
        public double Value { get; set; }

        public override string SwitchFieldName
        {
            get
            {
                return base.SwitchFieldName;
            }
            set
            {
                base.SwitchFieldName = value;
                XmlPropertyCaseDoubleNode.CaseFieldName = value;
            }
        }

        [DisplayName("E. Case")]
        public XmlPropertyCaseDoubleNode[] Case { get; set; }
    }

    [Browsable(true)]
    public class XmlPropertyStringNode : XmlBasePropertyNode
    {
        public class XmlPropertyCaseStringNode
        {
            public XmlPropertyCaseStringNode()
            {
                PropertyValue = "";
            }

            [DisplayName("Field string value")]
            [Editor(typeof(FieldValuesEditorType), typeof(UITypeEditor))]
            public string FieldStringValue { get; set; }
            [DisplayName("Property value")]
            public string PropertyValue { get; set; }

            internal static string CaseFieldName { get; set; }
        }

        public XmlPropertyStringNode()
            : base()
        {
            Case = new XmlPropertyCaseStringNode[0];
            _propType = DNEPropertyType._EPT_STRING;
            Value = "";
        }

        [DisplayName("B. Field name")]
        [Description("The data source(s) field used to fill the property")]
        [Editor(typeof(FieldNamesEditorType), typeof(UITypeEditor))]
        public string FieldName { get; set; }

        [DisplayName("C. Value")]
        public string Value { get; set; }

        public override string SwitchFieldName
        {
            get
            {
                return base.SwitchFieldName;
            }
            set
            {
                base.SwitchFieldName = value;
                XmlPropertyCaseStringNode.CaseFieldName = value;
            }
        }

        [DisplayName("E. Case")]
        public XmlPropertyCaseStringNode[] Case { get; set; }
    }

    [Browsable(true)]
    public class XmlPropertyColorNode : XmlBasePropertyNode
    {
        public struct XmlPropertyCaseColorNode
        {
            [DisplayName("Field string value")]
            [Editor(typeof(FieldValuesEditorType), typeof(UITypeEditor))]
            public string FieldStringValue { get; set; }
            [DisplayName("Property value")]
            public Color PropertyValue { get; set; }

            internal static string CaseFieldName { get; set; }
        }

        public XmlPropertyColorNode()
            : base()
        {
            Case = new XmlPropertyCaseColorNode[0];
            _propType = DNEPropertyType._EPT_BCOLOR;
        }

        [DisplayName("B. Field name")]
        [Description("The data source(s) field used to fill the property")]
        [Editor(typeof(FieldNamesEditorType), typeof(UITypeEditor))]
        public string FieldName { get; set; }

        [DisplayName("C. Value")]
        public Color Value { get; set; }

        public override string SwitchFieldName
        {
            get
            {
                return base.SwitchFieldName;
            }
            set
            {
                base.SwitchFieldName = value;
                XmlPropertyCaseColorNode.CaseFieldName = value;
            }
        }

        [DisplayName("E. Case")]
        public XmlPropertyCaseColorNode[] Case { get; set; }
    }

    [Browsable(true)]
    public class XmlPropertyFVector2DNode : XmlBasePropertyNode
    {
        public class XmlPropertyCaseFVector2DNode
        {

            public XmlPropertyCaseFVector2DNode()
            {
                PropertyValue = new XmlFVector2D();
            }

            [DisplayName("Field string names")]
            [Editor(typeof(FieldValuesEditorType), typeof(UITypeEditor))]
            public string FieldStringValue { get; set; }
            [DisplayName("Property values")]
            [TypeConverter(typeof(Vector2DConverter))]
            public XmlFVector2D PropertyValue { get; set; }

            internal static string CaseFieldName { get; set; }
        }

        public XmlPropertyFVector2DNode()
            : base()
        {
            Case = new XmlPropertyCaseFVector2DNode[0];
            _propType = DNEPropertyType._EPT_FVECTOR2D;
            FieldName = new XmlVector2DFieldNames();
            Value = new XmlFVector2D();
        }
        [DisplayName("B. Field names")]
        [Description("The data source(s) field used to fill the property")]
        [TypeConverter(typeof(Vector2DFieldNamesConverter))]
        public XmlVector2DFieldNames FieldName { get; set; }

        [DisplayName("C. Value")]
        [TypeConverter(typeof(Vector2DConverter))]
        public XmlFVector2D Value { get; set; }

        public override string SwitchFieldName
        {
            get
            {
                return base.SwitchFieldName;
            }
            set
            {
                base.SwitchFieldName = value;
                XmlPropertyCaseFVector2DNode.CaseFieldName = value;
            }
        }
        [DisplayName("E. Case")]
        public XmlPropertyCaseFVector2DNode[] Case { get; set; }
    }

    [Browsable(true)]
    public class XmlPropertyVector2DNode : XmlBasePropertyNode
    {
        public class XmlPropertyCaseVector2DNode
        {
            public XmlPropertyCaseVector2DNode()
            {
                PropertyValue = new XmlVector2D();
            }

            [DisplayName("Field string values")]
            [Editor(typeof(FieldValuesEditorType), typeof(UITypeEditor))]
            public string FieldStringValue { get; set; }
            [DisplayName("Property value")]
            [TypeConverter(typeof(Vector2DConverter))]
            public XmlVector2D PropertyValue { get; set; }

            internal static string CaseFieldName { get; set; }
        }

        public XmlPropertyVector2DNode()
            : base()
        {
            Case = new XmlPropertyCaseVector2DNode[0];
            _propType = DNEPropertyType._EPT_VECTOR2D;
            FieldName = new XmlVector2DFieldNames();
            Value = new XmlVector2D();
        }
        [DisplayName("B. Field names")]
        [Description("The data source(s) field used to fill the property")]
        [TypeConverter(typeof(Vector2DFieldNamesConverter))]
        public XmlVector2DFieldNames FieldName { get; set; }

        [DisplayName("C. Value")]
        [TypeConverter(typeof(Vector2DConverter))]
        public XmlVector2D Value { get; set; }

        public override string SwitchFieldName
        {
            get
            {
                return base.SwitchFieldName;
            }
            set
            {
                base.SwitchFieldName = value;
                XmlPropertyCaseVector2DNode.CaseFieldName = value;
            }
        }

        [DisplayName("E. Case")]
        public XmlPropertyCaseVector2DNode[] Case { get; set; }
    }

    [Browsable(true)]
    public class XmlPropertyFVector3DNode : XmlBasePropertyNode
    {
        public class XmlPropertyCaseFVector3DNode
        {
            public XmlPropertyCaseFVector3DNode()
            {
                PropertyValue = new XmlFVector3D();
            }

            [DisplayName("Field string values")]
            [Editor(typeof(FieldValuesEditorType), typeof(UITypeEditor))]
            public string FieldStringValue { get; set; }
            [DisplayName("Property value")]
            [TypeConverter(typeof(Vector3DConverter))]
            public XmlFVector3D PropertyValue { get; set; }

            internal static string CaseFieldName { get; set; }
        }

        public XmlPropertyFVector3DNode()
            : base()
        {
            Case = new XmlPropertyCaseFVector3DNode[0];
            _propType = DNEPropertyType._EPT_FVECTOR3D;
            FieldName = new XmlVector3DFieldNames();
            Value = new XmlFVector3D();
        }

        [DisplayName("B. Field names")]
        [Description("The data source(s) field used to fill the property")]
        [TypeConverter(typeof(Vector3DFieldNamesConverter))]
        public XmlVector3DFieldNames FieldName { get; set; }

        [DisplayName("C. Value")]
        [TypeConverter(typeof(Vector3DConverter))]
        public XmlFVector3D Value { get; set; }

        public override string SwitchFieldName
        {
            get
            {
                return base.SwitchFieldName;
            }
            set
            {
                base.SwitchFieldName = value;
                XmlPropertyCaseFVector3DNode.CaseFieldName = value;
            }
        }

        [DisplayName("E. Case")]
        public XmlPropertyCaseFVector3DNode[] Case { get; set; }
    }

    [Browsable(true)]
    public class XmlPropertyVector3DNode : XmlBasePropertyNode
    {
        public class XmlPropertyCaseVector3DNode
        {
            public XmlPropertyCaseVector3DNode()
            {
                PropertyValue = new XmlVector3D();
            }

            [DisplayName("Field string value")]
            [Editor(typeof(FieldValuesEditorType), typeof(UITypeEditor))]
            public string FieldStringValue { get; set; }
            [DisplayName("Property value")]
            [TypeConverter(typeof(Vector3DConverter))]
            public XmlVector3D PropertyValue { get; set; }

            internal static string CaseFieldName { get; set; }
        }

        public XmlPropertyVector3DNode()
            : base()
        {
            Case = new XmlPropertyCaseVector3DNode[0];
            _propType = DNEPropertyType._EPT_VECTOR3D;
            FieldName = new XmlVector3DFieldNames();
            Value = new XmlVector3D();
        }

        [DisplayName("B. Field names")]
        [Description("The data source(s) field used to fill the property")]
        [TypeConverter(typeof(Vector3DFieldNamesConverter))]
        public XmlVector3DFieldNames FieldName { get; set; }

        [DisplayName("C. Value")]
        [TypeConverter(typeof(Vector3DConverter))]
        public XmlVector3D Value { get; set; }

        public override string SwitchFieldName
        {
            get
            {
                return base.SwitchFieldName;
            }
            set
            {
                base.SwitchFieldName = value;
                XmlPropertyCaseVector3DNode.CaseFieldName = value;
            }
        }

        [DisplayName("E. Case")]
        public XmlPropertyCaseVector3DNode[] Case { get; set; }
    }

    [Browsable(true)]
    public class XmlPropertyTextureNode : XmlBasePropertyNode
    {
        public class XmlPropertyCaseTextureNode
        {
            public XmlPropertyCaseTextureNode()
            {
                PropertyValue = new XmlTextureType();
            }

            [DisplayName("Field string value")]
            [Editor(typeof(FieldValuesEditorType), typeof(UITypeEditor))]
            public string FieldStringValue { get; set; }
            [DisplayName("Property value")]
            [TypeConverter(typeof(TextureConverter))]
            public XmlTextureType PropertyValue { get; set; }

            internal static string CaseFieldName { get; set; }
        }

        public XmlPropertyTextureNode()
            : base()
        {
            Case = new XmlPropertyCaseTextureNode[0];
            _propType = DNEPropertyType._EPT_TEXTURE;
            FieldName = new XmlTextureFieldNames();
            Value = new XmlTextureType();
        }

        [DisplayName("B. Field names")]
        [Description("The data source(s) field used to fill the property")]
        [TypeConverter(typeof(TextureConverter))]
        public XmlTextureFieldNames FieldName {get; set;}

        [DisplayName("C. Value")]
        [TypeConverter(typeof(TextureConverter))]
        public XmlTextureType Value { get; set; }

        public override string SwitchFieldName
        {
            get
            {
                return base.SwitchFieldName;
            }
            set
            {
                base.SwitchFieldName = value;
                XmlPropertyCaseTextureNode.CaseFieldName = value;
            }
        }

        [DisplayName("E. Case")]
        public XmlPropertyCaseTextureNode[] Case { get; set; }
    }

    [Browsable(true)]
    public class XmlPropertyFontTypeNode : XmlBasePropertyNode
    {
        public class XmlPropertyCaseFontTypeNode
        {
            //XmlFontTypeFieldNames m_fieldNames;

            public XmlPropertyCaseFontTypeNode()
            {
                PropertyValue = new XmlFontType();
            }

            [DisplayName("Field string values")]
            [Editor(typeof(FieldValuesEditorType), typeof(UITypeEditor))]
            public string FieldStringValue { get; set; }
            [DisplayName("Property value")]
            [TypeConverter(typeof(FontTypeConverter))]
            public XmlFontType PropertyValue { get; set; }

            internal static string CaseFieldName { get; set; }
        }

        public XmlPropertyFontTypeNode()
            : base()
        {
            Case = new XmlPropertyCaseFontTypeNode[0];
            _propType = DNEPropertyType._EPT_FONT;
            FieldName = new XmlFontTypeFieldNames();
            Value = new XmlFontType();
        }

        [DisplayName("B. Field names")]
        [Description("The data source(s) field used to fill the property")]
        [TypeConverter(typeof(FontTypeConverter))]
        public XmlFontTypeFieldNames FieldName { get; set; }

        [DisplayName("C. Value")]
        [TypeConverter(typeof(FontTypeConverter))]
        public XmlFontType Value { get; set; }

        public override string SwitchFieldName
        {
            get
            {
                return base.SwitchFieldName;
            }
            set
            {
                base.SwitchFieldName = value;
                XmlPropertyCaseFontTypeNode.CaseFieldName = value;
            }
        }

        [DisplayName("E. Case")]
        public XmlPropertyCaseFontTypeNode[] Case { get; set; }
    }

    [Browsable(true)]
    public class XmlPropertyScaleCondSelectorNode : XmlBasePropertyNode
    {
        public class XmlPropertyCaseScaleCondSelectorNode
        {
            public XmlPropertyCaseScaleCondSelectorNode()
            {
                PropertyValue = new XmlScaleCondSelectorType();
            }

            [DisplayName("Field string value")]
            [Editor(typeof(FieldValuesEditorType), typeof(UITypeEditor))]
            public string FieldStringValue { get; set; }
            [DisplayName("Property value")]
            [TypeConverter(typeof(CondSelectorConverter))]
            public XmlScaleCondSelectorType PropertyValue { get; set; }

            internal static string CaseFieldName { get; set; }
        }

        public XmlPropertyScaleCondSelectorNode()
            : base()
        {
            Case = new XmlPropertyCaseScaleCondSelectorNode[0];
            _propType = DNEPropertyType._EPT_CONDITIONALSELECTOR;
            FieldName = new XmlScaleCondSelectorFieldNames();
            Value = new XmlScaleCondSelectorType();
        }

        [DisplayName("B. Field names")]
        [Description("The data source(s) field used to fill the property")]
        [TypeConverter(typeof(CondSelectorConverter))]
        public XmlScaleCondSelectorFieldNames FieldName { get; set; }

        [DisplayName("C. Value")]
        [TypeConverter(typeof(CondSelectorConverter))]
        public XmlScaleCondSelectorType Value { get; set; }

        public override string SwitchFieldName
        {
            get
            {
                return base.SwitchFieldName;
            }
            set
            {
                base.SwitchFieldName = value;
                XmlPropertyCaseScaleCondSelectorNode.CaseFieldName = value;
            }
        }

        [DisplayName("E. Case")]
        public XmlPropertyCaseScaleCondSelectorNode[] Case { get; set; }
    }



    // Note: XmlFVector2DFieldNames is same as XmlVector2DFieldNames

    [Browsable(true)]
    public class XmlFVector2D
    {
        public XmlFVector2D()
        {
            x = 0f;
            y = 0f;
        }
        public float x { get; set; }
        public float y { get; set; }

        public static explicit operator DNSMcFVector2D(XmlFVector2D arg)
        {
            Vector2DConverter conv = new Vector2DConverter();
            return (DNSMcFVector2D)conv.ConvertTo(null, System.Globalization.CultureInfo.CurrentCulture, arg, typeof(DNSMcFVector2D));
        }

        public static explicit operator XmlFVector2D(DNSMcFVector2D arg)
        {
            Vector2DConverter conv = new Vector2DConverter();
            return (XmlFVector2D)conv.ConvertTo(null, System.Globalization.CultureInfo.CurrentCulture, arg, typeof(XmlFVector2D));
        }
    }

    [Browsable(true)]
    public class XmlVector2DFieldNames
    {
        [DisplayName("X field")]
        [Editor(typeof(FieldNamesEditorType), typeof(UITypeEditor))]
        public string XFieldName { get; set; }

        [DisplayName("Y field")]
        [Editor(typeof(FieldNamesEditorType), typeof(UITypeEditor))]
        public string YFieldName { get; set; }
    }

    [Browsable(true)]
    public class XmlVector2D
    {
        public XmlVector2D()
        {
            x = 0.0;
            y = 0.0;
        }
        public double x { get; set; }
        public double y { get; set; }

        public static explicit operator DNSMcVector2D(XmlVector2D arg)
        {
            Vector2DConverter conv = new Vector2DConverter();
            return (DNSMcVector2D)conv.ConvertTo(null, System.Globalization.CultureInfo.CurrentCulture, arg, typeof(DNSMcVector2D));
        }

        public static explicit operator XmlVector2D(DNSMcVector2D arg)
        {
            Vector2DConverter conv = new Vector2DConverter();
            return (XmlVector2D)conv.ConvertTo(null, System.Globalization.CultureInfo.CurrentCulture, arg, typeof(XmlVector2D));
        }

    }

    // Note: XmlFVector3DFieldNames is same as XmlVector3DFieldNames

    [Browsable(true)]
    public class XmlFVector3D
    {
        public XmlFVector3D()
        {
            x = 0f;
            y = 0f;
            z = 0f;
        }
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        public static explicit operator DNSMcFVector3D(XmlFVector3D arg)
        {
            Vector3DConverter conv = new Vector3DConverter();
            return (DNSMcFVector3D)conv.ConvertTo(null, System.Globalization.CultureInfo.CurrentCulture, arg, typeof(DNSMcFVector3D));
        }

        public static explicit operator XmlFVector3D(DNSMcFVector3D arg)
        {
            Vector3DConverter conv = new Vector3DConverter();
            return (XmlFVector3D)conv.ConvertTo(null, System.Globalization.CultureInfo.CurrentCulture, arg, typeof(XmlFVector3D));
        }
    }

    [Browsable(true)]
    public class XmlVector3DFieldNames
    {
        [DisplayName("X field")]
        [Editor(typeof(FieldNamesEditorType), typeof(UITypeEditor))]
        public string XFieldName { get; set; }

        [DisplayName("Y field")]
        [Editor(typeof(FieldNamesEditorType), typeof(UITypeEditor))]
        public string YFieldName { get; set; }

        [DisplayName("Z field")]
        [Editor(typeof(FieldNamesEditorType), typeof(UITypeEditor))]
        public string ZFieldName { get; set; }
    }

    [Browsable(true)]
    public class XmlVector3D
    {
        public XmlVector3D()
        {
            x = 0.0;
            y = 0.0;
            z = 0.0;
        }
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }

        public static explicit operator DNSMcVector3D(XmlVector3D arg)
        {
            Vector3DConverter conv = new Vector3DConverter();
            return (DNSMcVector3D)conv.ConvertTo(null, System.Globalization.CultureInfo.CurrentCulture, arg, typeof(DNSMcVector3D));
        }

        public static explicit operator XmlVector3D(DNSMcFVector3D arg)
        {
            Vector3DConverter conv = new Vector3DConverter();
            return (XmlVector3D)conv.ConvertTo(null, System.Globalization.CultureInfo.CurrentCulture, arg, typeof(XmlVector3D));
        }
    }

    [Browsable(true)]
    public class XmlTextureFieldNames
    {
        [DisplayName("Image file field")]
        [Editor(typeof(FieldNamesEditorType), typeof(UITypeEditor))]
        public string ImageFileName { get; set; }

        [DisplayName("Transparent Color field")]
        [Editor(typeof(FieldNamesEditorType), typeof(UITypeEditor))]
        public string TransparentColor { get; set; }

        [DisplayName("Transparent enabled field")]
        [Editor(typeof(FieldNamesEditorType), typeof(UITypeEditor))]
        public string IsTransparentColorEnabled { get; set; }

        [DisplayName("Transparent margin ignored field")]
        [Editor(typeof(FieldNamesEditorType), typeof(UITypeEditor))]
        public string IgnoreTransparentMargin { get; set; }
    }

    [Browsable(true)]
    public class XmlTextureType
    {
        [DisplayName("Image File name")]
        [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
        public string ImageFileName { get; set; }

        [DisplayName("Transparent Color")]
        public Color TransparentColor { get; set; }

        [DisplayName("Transparent color enabled")]
        public bool IsTransparentColorEnabled { get; set; }

        [DisplayName("Transparent margin ignored")]
        public bool IgnoreTransparentMargin { get; set; }
    }

    [Browsable(true)]
    public class XmlFontTypeFieldNames
    {
        [DisplayName("Font face name")]
        [Editor(typeof(FieldNamesEditorType), typeof(UITypeEditor))]
        public string FaceName { get; set; }

        [DisplayName("Height")]
        [Editor(typeof(FieldNamesEditorType), typeof(UITypeEditor))]
        public string Height { get; set; }

        [DisplayName("GDI Char set")]
        [Editor(typeof(FieldNamesEditorType), typeof(UITypeEditor))]
        public string CharSet { get; set; }
    }

    [Browsable(true)]
    public class XmlFontType
    {
        Font m_theFont;
        float m_fontHeight;
        string m_faceName;
        uint m_charSet = 1;

        public XmlFontType()
        {
            if (Application.OpenForms.Count > 0)
            {
                m_theFont = Application.OpenForms[0].Font;
            }
            else
            {
                m_theFont = SystemInformation.MenuFont;
            }
        }

        public static explicit operator DNSXmlFontDef(XmlFontType arg)
        {
            FontTypeConverter conv = new FontTypeConverter();
            return (DNSXmlFontDef)conv.ConvertTo(null, Application.CurrentCulture, arg, typeof(DNSXmlFontDef));
        }

        public static explicit operator XmlFontType(DNSXmlFontDef arg)
        {
            FontTypeConverter conv = new FontTypeConverter();
            return (XmlFontType)conv.ConvertTo(null, Application.CurrentCulture, arg, typeof(XmlFontType));
        }

        [DisplayName("Font")]
        public Font TheFont
        {
            get
            {
                return m_theFont;
            }
            set
            {
                m_theFont = value;
                m_fontHeight = m_theFont.Height;
                m_faceName = m_theFont.FontFamily.Name;
                m_charSet = (uint)m_theFont.GdiCharSet;
            }
        }

        [DisplayName("Font Face")]
        [Description("Font face name")]
        [ReadOnly(true)]
        public string FaceName
        {
            get
            {
                return m_faceName;
            }
            set
            {
                m_faceName = value;
                if (m_fontHeight != 0)
                {
                    m_theFont = new Font(m_faceName, m_fontHeight, FontStyle.Regular, GraphicsUnit.Display, (byte)m_charSet);
                }
            }
        }

        [DisplayName("Height")]
        [Description("Font height")]
        [ReadOnly(true)]
        public float FontHeight
        {
            get
            {
                return m_fontHeight;
            }
            set
            {
                m_fontHeight = value;
                if (m_faceName != null && m_faceName != string.Empty)
                {
                    m_theFont = new Font(m_faceName, m_fontHeight, FontStyle.Regular, GraphicsUnit.Display, (byte)m_charSet);
                }
            }
        }

        [DisplayName("GDI Character set")]
        [Description("Graphical system character set")]
        [ReadOnly(true)]
        public uint CharSet
        {
            get
            {
                return m_charSet;
            }
            set
            {
                m_charSet = value;
                if (m_faceName != null && m_faceName != string.Empty && m_fontHeight != 0)
                {
                    m_theFont = new Font(m_faceName, m_fontHeight, FontStyle.Regular, GraphicsUnit.Display, (byte)m_charSet);
                }
            }
        }
    }

    [Browsable(true)]
    public class XmlScaleCondSelectorFieldNames
    {
        [DisplayName("A. Min field")]
        [Editor(typeof(FieldNamesEditorType), typeof(UITypeEditor))]
        public string MinFieldName { get; set; }

        [DisplayName("B. Max field")]
        [Editor(typeof(FieldNamesEditorType), typeof(UITypeEditor))]
        public string MaxFieldName { get; set; }

        [DisplayName("C. Cancel scale mode field")]
        [Editor(typeof(FieldNamesEditorType), typeof(UITypeEditor))]
        public string CancelScaleModeFieldName { get; set; }

        [DisplayName("D. Cancel mode result field")]
        [Editor(typeof(FieldNamesEditorType), typeof(UITypeEditor))]
        public string CancelScaleModeResult { get; set; }
    }

    [Browsable(true)]
    public class XmlScaleCondSelectorType
    {
        [DisplayName("A. Min")]
        public float Min { get; set; }

        [DisplayName("B. Max")]
        public float Max { get; set; }

        [DisplayName("C. Cancel scale mode")]
        public uint CancelScaleMode { get; set; }

        [DisplayName("D. Cancel mode result")]
        public uint CancelScaleModeResult { get; set; }
    }

    

    [Browsable(true)]
    public class XmlObjectNode
    {
        public XmlObjectNode()
        {
            ScaleRange = new XmlScaleRangeNode();
            ScaleRange.fMax = float.MaxValue;
            apProperties = new XmlBasePropertyNode[0];
        }

        string schemaFile;

        [DisplayName("Key field")]
        [Editor(typeof(FieldNamesEditorType),typeof(UITypeEditor))]
        public string strKeyFieldName { get; set; }

        [DisplayName("Properties")]
        public XmlBasePropertyNode[] apProperties { get; set; }

        [DisplayName("Scale range")]
        [TypeConverter(typeof(ScaleConverter))]
        public XmlScaleRangeNode ScaleRange { get; set; }

        [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
        [DisplayName("Objects scheme file")]
        public string strSchemeFile 
        { 
            get
            {
                return schemaFile;
            }
            set
            {
                schemaFile = value;
                if (File.Exists(schemaFile))
                {
                    // Create an overlay manager to load object scheme. Coordinates system doesn't matter for this operation
                    IDNMcOverlayManager overlayMgr = DNMcOverlayManager.Create(
                        DNMcGridCoordSystemGeographic.Create(DNEDatumType._EDT_WGS84));
                    IDNMcObjectScheme[] schemes = overlayMgr.LoadObjectSchemes(schemaFile);
                    if (schemes.Length == 1)
                    {
                        DNSPropertyNameIDType[] props = schemes[0].GetProperties();
                        apProperties = new XmlBasePropertyNode[props.Length];

                        for (int i=0; i<props.Length; i++)
                        {
                            switch (props[i].eType)
                            {
                                case DNEPropertyType._EPT_BOOL:
                                    apProperties[i] = new XmlPropertyBoolNode();
                                    break;
                                case DNEPropertyType._EPT_BYTE:
                                    apProperties[i] = new XmlPropertyByteNode();
                                    break;
                                case DNEPropertyType._EPT_SBYTE:
                                    apProperties[i] = new XmlPropertySByteNode();
                                    break;
                                case DNEPropertyType._EPT_INT:
                                    apProperties[i] = new XmlPropertyIntNode();
                                    break;
                                case DNEPropertyType._EPT_ENUM:
                                    apProperties[i] = new XmlPropertyEnumNode();
                                    break;
                                case DNEPropertyType._EPT_UINT:
                                    apProperties[i] = new XmlPropertyUIntNode();
                                    break;
                                case DNEPropertyType._EPT_FLOAT:
                                    apProperties[i] = new XmlPropertyFloatNode();
                                    break;
                                case DNEPropertyType._EPT_DOUBLE:
                                    apProperties[i] = new XmlPropertyDoubleNode();
                                    break;
                                case DNEPropertyType._EPT_STRING:
                                    apProperties[i] = new XmlPropertyStringNode();
                                    break;
                                case DNEPropertyType._EPT_BCOLOR:
                                    apProperties[i] = new XmlPropertyColorNode();
                                    break;
                                case DNEPropertyType._EPT_FVECTOR2D:
                                    apProperties[i] = new XmlPropertyFVector2DNode();
                                    break;
                                case DNEPropertyType._EPT_VECTOR2D:
                                    apProperties[i] = new XmlPropertyVector2DNode();
                                    break;
                                case DNEPropertyType._EPT_FVECTOR3D:
                                    apProperties[i] = new XmlPropertyFVector3DNode();
                                    break;
                                case DNEPropertyType._EPT_VECTOR3D:
                                    apProperties[i] = new XmlPropertyVector3DNode();
                                    break;
                                case DNEPropertyType._EPT_TEXTURE:
                                    apProperties[i] = new XmlPropertyTextureNode();
                                    break;
                                case DNEPropertyType._EPT_FONT:
                                    apProperties[i] = new XmlPropertyFontTypeNode();
                                    break;
                                case DNEPropertyType._EPT_CONDITIONALSELECTOR:
                                    apProperties[i] = new XmlPropertyScaleCondSelectorNode();
                                    break;
                                default:
                                    continue;
                            }
                            apProperties[i].PropertyName = schemes[0].GetPropertyNameByID(props[i].uID);
                        }
                    }
                    schemes[0].Dispose();
                    overlayMgr.Dispose();
                }


            }
        }

        [DisplayName("Auto Unify Labels")]
        public bool bAutoUnifyLabels { get; set; }

        public static explicit operator XmlObjectNode(DNSXmlObjectNode arg)
        {
            XmlObjectNode result = new XmlObjectNode();

            if (arg != null)
            {
                result.strSchemeFile = arg.strSchemeFile;
                result.apProperties = XmlBasePropertyNode.FromDNS(arg.apProperties);
                result.ScaleRange = (XmlScaleRangeNode)arg.ScaleRange;
                result.strKeyFieldName = arg.strKeyFieldName;
                result.bAutoUnifyLabels = arg.bAutoUnifyLabels;
            }

            return result;
        }

        public static explicit operator DNSXmlObjectNode(XmlObjectNode arg)
        {
            DNSXmlObjectNode result = new DNSXmlObjectNode();

            if (arg != null)
            {
                result.apProperties = XmlBasePropertyNode.ToDNS(arg.apProperties);
                result.ScaleRange = (DNSXmlScaleRangeNode)arg.ScaleRange;
                result.strKeyFieldName = arg.strKeyFieldName;
                result.strSchemeFile = arg.strSchemeFile;
                result.bAutoUnifyLabels = arg.bAutoUnifyLabels;
            }

            return result;
        }
    }

    [Browsable(true)]
    public class XmlObjectConditionNode
    {
        public XmlObjectConditionNode()
        {
            BoolCondition = new XmlBoolConditionNode();
            ObjectDefinition = new XmlObjectNode();
        }

        [DisplayName("Boolean Condition")]
        [TypeConverter(typeof(BoolCondConverter))]
        public XmlBoolConditionNode BoolCondition { get; set; }

        [DisplayName("Object Definition")]
        [TypeConverter(typeof(ObjectNodeConverter))]
        public XmlObjectNode ObjectDefinition { get; set; }

        public static explicit operator XmlObjectConditionNode (DNSXmlObjectConditionNode arg)
        {
            XmlObjectConditionNode result = new XmlObjectConditionNode();
            result.BoolCondition = (XmlBoolConditionNode)arg.BoolCondition;
            result.ObjectDefinition = (XmlObjectNode)arg.ObjectDefinition;
            return result;
        }

        public static explicit operator DNSXmlObjectConditionNode(XmlObjectConditionNode arg)
        {
            DNSXmlObjectConditionNode result = new DNSXmlObjectConditionNode();
            result.BoolCondition = (DNSXmlBoolConditionNode)arg.BoolCondition;
            result.ObjectDefinition = (DNSXmlObjectNode)arg.ObjectDefinition;
            return result;
        }
    }

    internal class ComparsionOperatorConverter : EnumConverter
    {
        internal static string[] aValues = new string[] { "==", "!=", "<", "<=", ">", ">=", "<>", "*", "~", "Num" }; // Note: order must be the same as DNEComparisonOperator !!!
        internal static string[] aDesc = new string[] { "\t(Equal)", "\t(Not Equal)", "\t(Less Than)", "\t(Less Equal Than)", "\t(Greater Than)", "\t(Greater Equal Than)", "\t(Between)", "\t(Sub String)", "\t(Always)", "" }; // Note: order must be the same as DNEComparisonOperator !!!

        public ComparsionOperatorConverter()
            : base(typeof(DNEComparisonOperator))
        {
            Values = new StandardValuesCollection(aValues);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value is DNEComparisonOperator)
                {
                    DNEComparisonOperator currVal = (DNEComparisonOperator)value;
                    int nIdx = (int)currVal;
                    return Values[nIdx] + aDesc[nIdx];
                }
                else if (value is string)
                {
                    for (int i=0; i<Values.Count; i++)
                    {
                        if (Values[i].ToString() == value.ToString())
                        {
                            DNEComparisonOperator op = (DNEComparisonOperator)i;
                            return op.ToString();
                        }
                    }
                    return value;
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                string strValue = value.ToString().ToUpper();
                for (int i = 0; i < Values.Count; i++)
                {
                    if (Values[i].ToString().ToUpper() == strValue)
                    {
                        return (DNEComparisonOperator)i;
                    }
                }
                return base.ConvertFrom(context, culture, value);
            }
            return base.ConvertFrom(context, culture, value);
        }
    }

    internal class BoolOperatorConverter : EnumConverter
    {
        internal static string[] aValues = new string[] { "And", "Or", "Not", "Num" }; // Note: order must be the same as DNEBoolOperator !!!

        public BoolOperatorConverter()
            : base(typeof(DNEBoolOperator))
        {
            Values = new StandardValuesCollection(aValues);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value is DNEBoolOperator)
                {
                    DNEBoolOperator currVal = (DNEBoolOperator)value;
                    int nIdx = (int)currVal;
                    return Values[nIdx];
                }
                else if (value is string)
                {
                    string val = "_EBO_" + value.ToString();
                    DNEBoolOperator oper;
                    if (Enum.TryParse<DNEBoolOperator>(val,out oper))
                    {
                        return val;
                    }
                    else
                    {
                        return value;
                    }
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                string strValue = value.ToString().ToUpper();
                for (int i = 0; i < Values.Count; i++)
                {
                    if (Values[i].ToString().ToUpper() == strValue)
                    {
                        return (DNEBoolOperator)i;
                    }
                }
                return base.ConvertFrom(context, culture, value);
            }
            return base.ConvertFrom(context, culture, value);
        }
    }

    internal class ScaleConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context,
                                         System.Globalization.CultureInfo culture,
                                         object value, Type destType)
        {
            if (destType == typeof(string) && value is XmlScaleRangeNode)
            {
                XmlScaleRangeNode val = (XmlScaleRangeNode)value;
                string result = "Range: Min = " + val.fMin.ToString("#########.000") + ", Max = ";
                if (val.fMax == float.MaxValue)
                {
                    result += "MAX";
                }
                else
                {
                    result += val.fMax.ToString("#########.000");
                }
                return result;
            }
            else
            {
                return base.ConvertTo(context, culture, value, destType);
            }
        }
    }

    internal class ObjectNodeConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(
            ITypeDescriptorContext context,
            System.Globalization.CultureInfo culture,
            object value,
            Type destinationType)
        {
            if (value is XmlObjectNode && destinationType == typeof(string))
            {
                return "(" + ((XmlObjectNode)value).strKeyFieldName + ")";
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    internal class FieldCondConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(
            ITypeDescriptorContext context,
            System.Globalization.CultureInfo culture,
            object value,
            Type destinationType)
        {
            if (destinationType == typeof(string) && value is XmlFieldConditionNode)
            {
                XmlFieldConditionNode val = (XmlFieldConditionNode)value;
                ComparsionOperatorConverter conv = new ComparsionOperatorConverter();

                string result = val.strFieldName + " " + conv.ConvertToString(val.eOperator);
                if (val.eOperator != DNEComparisonOperator._ECO_ALWAYS)
                {
                    result += (" " + val.strOperand);
                    if (val.eOperator != DNEComparisonOperator._ECO_BETWEEN)
                    {
                        result += (", " + val.strAdditionalOperand);
                    }
                }

                return result;
            }
            else if (destinationType == typeof(string) && value is XmlFieldConditionNode[])
            {
                if (((XmlFieldConditionNode[])value).Length == 0)
                {
                    return "No items.";
                }
                else if (((XmlFieldConditionNode[])value).Length == 1)
                {
                    return "1 Item.";
                }
                else
                {
                    return ((XmlFieldConditionNode[])value).Length + " items.";
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

    }

    internal class BoolCondConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(
            ITypeDescriptorContext context, 
            System.Globalization.CultureInfo culture, 
            object value, 
            Type destinationType)
        {
            if (destinationType == typeof(string) && value is XmlBoolConditionNode)
            {
                XmlBoolConditionNode val = (XmlBoolConditionNode)value;
                BoolOperatorConverter conv = new BoolOperatorConverter();
                
                string result = "Operator: " + conv.ConvertToString(val.eBoolOperator);

                return result;
            }
            else if (destinationType == typeof(string) && value is XmlBoolConditionNode[])
            {
                if (((XmlBoolConditionNode[])value).Length == 0)
                {
                    return "No items.";
                }
                else if (((XmlBoolConditionNode[])value).Length == 1)
                {
                    return "1 Item.";
                }
                else
                {
                    return ((XmlBoolConditionNode[])value).Length + " items.";
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    internal class Vector2DFieldNamesConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(
            ITypeDescriptorContext context,
            System.Globalization.CultureInfo culture,
            object value,
            Type destinationType)
        {
            int count = 2;
            if (value != null)
            {
                if (destinationType == typeof(string) && value is XmlVector2DFieldNames)
                {
                    if (((XmlVector2DFieldNames)value).XFieldName == string.Empty || ((XmlVector2DFieldNames)value).XFieldName == null)
                    {
                        count--;
                    }
                    if (((XmlVector2DFieldNames)value).YFieldName == string.Empty || ((XmlVector2DFieldNames)value).YFieldName == null)
                    {
                        count--;
                    }

                    if (count <= 0)
                    {
                        return "N/A";
                    }
                    else if (count == 1)
                    {
                        return "1 Field name selected";
                    }
                    else
                    {
                        return string.Format("{0} field names selected", count);
                    }
                }

            }
            return base.ConvertFrom(context, culture, value);
        }
    }

    internal class Vector2DConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context,
                                         System.Globalization.CultureInfo culture,
                                         object value, Type destType)
        {
            if (value != null)
            {
                if (destType == typeof(string) && value is XmlFVector2D)
                {
                    return string.Format("X = {0},Y = {1}", ((XmlFVector2D)value).x.ToString(), ((XmlFVector2D)value).y.ToString());
                }
                if (destType == typeof(string) && value is XmlVector2D)
                {
                    return string.Format("X = {0},Y = {1}", ((XmlVector2D)value).x.ToString(), ((XmlVector2D)value).y.ToString());
                }
                else if (destType == typeof(DNSMcFVector2D) && value is XmlFVector2D)
                {
                    DNSMcFVector2D result = new DNSMcFVector2D(((XmlFVector2D)value).x, ((XmlFVector2D)value).y);
                    return result;
                }
                else if (destType == typeof(DNSMcVector2D) && value is XmlVector2D)
                {
                    DNSMcVector2D result = new DNSMcVector2D(((XmlVector2D)value).x, ((XmlVector2D)value).y);
                    return result;
                }
                else if (destType == typeof(XmlFVector2D) && value is DNSMcFVector2D)
                {
                    XmlFVector2D result = new XmlFVector2D();
                    result.x = ((DNSMcFVector2D)value).x;
                    result.y = ((DNSMcFVector2D)value).y;
                }
                else if (destType == typeof(XmlVector2D) && value is DNSMcVector2D)
                {
                    XmlVector2D result = new XmlVector2D();
                    result.x = ((DNSMcVector2D)value).x;
                    result.y = ((DNSMcVector2D)value).y;
                }
            }

            return base.ConvertTo(context, culture, value, destType);
        }
    }

    internal class Vector3DFieldNamesConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(
            ITypeDescriptorContext context,
            System.Globalization.CultureInfo culture,
            object value,
            Type destinationType)
        {
            int count = 3;
            if (value != null)
            {
                if (destinationType == typeof(string) && value is XmlVector3DFieldNames)
                {
                    if (((XmlVector3DFieldNames)value).XFieldName == string.Empty || ((XmlVector3DFieldNames)value).XFieldName == null)
                    {
                        count--;
                    }
                    if (((XmlVector3DFieldNames)value).YFieldName == string.Empty || ((XmlVector3DFieldNames)value).YFieldName == null)
                    {
                        count--;
                    }
                    if (((XmlVector3DFieldNames)value).ZFieldName == string.Empty || ((XmlVector3DFieldNames)value).ZFieldName == null)
                    {
                        count--;
                    }

                    if (count <= 0)
                    {
                        return "N/A";
                    }
                    else if (count == 1)
                    {
                        return "1 Field name selected";
                    }
                    else
                    {
                        return string.Format("{0} field names selected", count);
                    }
                }
            }
            return base.ConvertFrom(context, culture, value);
        }
    }

    internal class Vector3DConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context,
                                         System.Globalization.CultureInfo culture,
                                         object value, Type destType)
        {
            if (value != null)
            {
                if (destType == typeof(string) && value is XmlFVector3D)
                {
                    return string.Format("X = {0},Y = {1} Z = {2}", 
                        ((XmlFVector3D)value).x.ToString(), ((XmlFVector3D)value).y.ToString(), ((XmlFVector3D)value).z.ToString());
                }
                else if (destType == typeof(string) && value is XmlVector3D)
                {
                    return string.Format("X = {0},Y = {1} Z = {2}",
                        ((XmlVector3D)value).x.ToString(), ((XmlVector3D)value).y.ToString(), ((XmlVector3D)value).z.ToString());
                }
                else if (destType == typeof(DNSMcFVector3D) && value is XmlFVector3D)
                {
                    DNSMcFVector3D result = new DNSMcFVector3D(((XmlFVector3D)value).x, ((XmlFVector3D)value).y, ((XmlFVector3D)value).z);
                    return result;
                }
                else if (destType == typeof(DNSMcVector3D) && value is XmlVector3D)
                {
                    DNSMcVector3D result = new DNSMcVector3D(((XmlVector3D)value).x, ((XmlVector3D)value).y, ((XmlVector3D)value).z);
                    return result;
                }
                else if (destType == typeof(XmlFVector3D) && value is DNSMcFVector3D)
                {
                    XmlFVector3D result = new XmlFVector3D();
                    result.x = ((DNSMcFVector3D)value).x;
                    result.y = ((DNSMcFVector3D)value).y;
                    result.z = ((DNSMcFVector3D)value).z;
                }
                else if (destType == typeof(XmlVector3D) && value is DNSMcVector3D)
                {
                    XmlVector3D result = new XmlVector3D();
                    result.x = ((DNSMcVector3D)value).x;
                    result.y = ((DNSMcVector3D)value).y;
                    result.z = ((DNSMcVector3D)value).z;
                }
            }

            return base.ConvertTo(context, culture, value, destType);
        }
    }

    internal class TextureFieldNamesConverter : ExpandableObjectConverter // :???!!!: is this need to be called instead of TextureConverter before 'public XmlTextureFieldNames FieldName {get; set;}'?... for Font & ScaleCondSelector as well?...
    {
        public override object ConvertTo(
            ITypeDescriptorContext context,
            System.Globalization.CultureInfo culture,
            object value,
            Type destinationType)
        {
            int count = 4;
            if (value != null)
            {
                if (destinationType == typeof(string) && value is XmlTextureFieldNames)
                {
                    if (((XmlTextureFieldNames)value).IgnoreTransparentMargin == string.Empty ||
                        ((XmlTextureFieldNames)value).IgnoreTransparentMargin == null)
                    {
                        count--;
                    }
                    if (((XmlTextureFieldNames)value).ImageFileName == string.Empty ||
                        ((XmlTextureFieldNames)value).ImageFileName == null)
                    {
                        count--;
                    }
                    if (((XmlTextureFieldNames)value).IsTransparentColorEnabled == string.Empty ||
                        ((XmlTextureFieldNames)value).IsTransparentColorEnabled == null)
                    {
                        count--;
                    }
                    if (((XmlTextureFieldNames)value).TransparentColor == string.Empty ||
                        ((XmlTextureFieldNames)value).TransparentColor == null)
                    {
                        count--;
                    }

                    if (count <= 0)
                    {
                        return "N/A";
                    }
                    else if (count == 1)
                    {
                        return "1 Field name selected";
                    }
                    else
                    {
                        return string.Format("{0} field names selected", count);
                    }
                }
            }
            return base.ConvertFrom(context, culture, value);
        }
    }

    internal class TextureConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(
            ITypeDescriptorContext context,
            System.Globalization.CultureInfo culture,
            object value,
            Type destinationType)
        {

            if (value is XmlTextureType && destinationType == typeof(string))
            {
                return string.Format("Image: {0}", new FileInfo(((XmlTextureType)value).ImageFileName).Name);
            }
            else if (value is DNSXmlTextureDef && destinationType == typeof(XmlTextureType))
            {
                XmlTextureType result = new XmlTextureType();
                result.IgnoreTransparentMargin = ((DNSXmlTextureDef)value).bIgnoreTransparentMargin;
                result.ImageFileName = ((DNSXmlTextureDef)value).strImageFileName;
                result.IsTransparentColorEnabled = ((DNSXmlTextureDef)value).TransparentColor.HasValue;
                if (result.IsTransparentColorEnabled)
                {
                    result.TransparentColor = Color.FromArgb(
                        ((DNSXmlTextureDef)value).TransparentColor.Value.a,
                        ((DNSXmlTextureDef)value).TransparentColor.Value.r,
                        ((DNSXmlTextureDef)value).TransparentColor.Value.g,
                        ((DNSXmlTextureDef)value).TransparentColor.Value.b);
                }
                return result;
            }
            else if (value is XmlTextureType && destinationType == typeof(DNSXmlTextureDef))
            {
                DNSXmlTextureDef result = new DNSXmlTextureDef();
                result.bIgnoreTransparentMargin = ((XmlTextureType)value).IgnoreTransparentMargin;
                result.strImageFileName = ((XmlTextureType)value).ImageFileName;
                if (((XmlTextureType)value).IsTransparentColorEnabled)
                {
                    result.TransparentColor = new DNSMcBColor(
                        ((XmlTextureType)value).TransparentColor.R,
                        ((XmlTextureType)value).TransparentColor.G,
                        ((XmlTextureType)value).TransparentColor.B,
                        ((XmlTextureType)value).TransparentColor.A);
                }
                return result;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    internal class FontTypeConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(
            ITypeDescriptorContext context,
            System.Globalization.CultureInfo culture,
            object value,
            Type destinationType)
        {
            if (value != null)
            {
                if (value is XmlFontType && destinationType == typeof(string))
                {
                    string result = string.Format("Name={0}, Height={1}, CharSet={2}",
                        ((XmlFontType)value).FaceName,
                        ((XmlFontType)value).FontHeight,
                        ((XmlFontType)value).CharSet);
                    return result;
                }
                else if (value is XmlFontType && destinationType == typeof(DNSXmlFontDef))
                {
                    DNSXmlFontDef result = new DNSXmlFontDef();
                    result.uCharSet = ((XmlFontType)value).CharSet;
                    result.strFaceName = ((XmlFontType)value).FaceName;
                    result.nHeight = (int)((XmlFontType)value).FontHeight;
                    return result;
                }
                else if (value is DNSXmlFontDef && destinationType == typeof(XmlFontType))
                {
                    XmlFontType result = new XmlFontType();
                    result.TheFont = new Font(
                            ((DNSXmlFontDef)value).strFaceName,
                            (float)((DNSXmlFontDef)value).nHeight,
                            FontStyle.Regular,
                            GraphicsUnit.Display,
                            (byte)((DNSXmlFontDef)value).uCharSet
                        );
                    return result;
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    internal class CondSelectorConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(
            ITypeDescriptorContext context,
            System.Globalization.CultureInfo culture,
            object value,
            Type destinationType)
        {
            if (value != null)
            {
                if (value is XmlScaleCondSelectorType && destinationType == typeof(string))
                {
                    return string.Format("Min={0}, Max={1}, CancelMode={2}, CancelModeResult={3}",
                        ((XmlScaleCondSelectorType)value).Min,
                        ((XmlScaleCondSelectorType)value).Max,
                        ((XmlScaleCondSelectorType)value).CancelScaleMode,
                        ((XmlScaleCondSelectorType)value).CancelScaleModeResult);
                }
                else if (value is XmlScaleCondSelectorType && destinationType == typeof(DNSXmlScaleConditionalSelectorDef))
                {
                    DNSXmlScaleConditionalSelectorDef result = new DNSXmlScaleConditionalSelectorDef();
                    result.fMaxScale = ((XmlScaleCondSelectorType)value).Max;
                    result.fMinScale = ((XmlScaleCondSelectorType)value).Min;
                    result.uCancelScaleMode = ((XmlScaleCondSelectorType)value).CancelScaleMode;
                    result.uCancelScaleModeResult = ((XmlScaleCondSelectorType)value).CancelScaleModeResult;
                    return result;
                }
                else if (value is DNSXmlScaleConditionalSelectorDef && destinationType == typeof(XmlScaleCondSelectorType))
                {
                    XmlScaleCondSelectorType result = new XmlScaleCondSelectorType();
                    result.Min = ((DNSXmlScaleConditionalSelectorDef)value).fMinScale;
                    result.Max = ((DNSXmlScaleConditionalSelectorDef)value).fMaxScale;
                    result.CancelScaleMode = ((DNSXmlScaleConditionalSelectorDef)value).uCancelScaleMode;
                    result.CancelScaleModeResult = ((DNSXmlScaleConditionalSelectorDef)value).uCancelScaleModeResult;
                    return result;
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    public class FieldNamesEditorType : UITypeEditor
    {
        private IWindowsFormsEditorService _editorService;

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            _editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            // use a list box
            ListBox lb = new ListBox();
            lb.SelectionMode = SelectionMode.One;
            lb.SelectedValueChanged += OnListBoxSelectedValueChanged;

            lb.DisplayMember = "Field Name";

            string[] fieldNames = DataSourceType.Instance.FieldNames;
            foreach (string fieldName in fieldNames)
            {
                lb.Items.Add(fieldName);
            }

            // Show data
            _editorService.DropDownControl(lb);

            // If no selection, return passed in value as is
            if (lb.SelectedItem == null)
            {
                return value;
            }
            return lb.SelectedItem;
        }

        private void OnListBoxSelectedValueChanged(object sender, EventArgs e)
        {
            // close the drop down as soon as something is clicked
            _editorService.CloseDropDown();
        }
    }

    public class FieldValuesEditorType : UITypeEditor
    {
        private IWindowsFormsEditorService _editorService;

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            _editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            // use a list box
            ListBox lb = new ListBox();
            lb.SelectionMode = SelectionMode.One;
            lb.SelectedValueChanged += OnListBoxSelectedValueChanged;

            lb.DisplayMember = "Field Value";

            // Lookup for a field name to get its values
            PropertyInfo [] props = context.Instance.GetType().GetProperties();
            PropertyInfo typeProperty = null;
            foreach (PropertyInfo pinfo in props)
            {
                if (pinfo.Name.ToUpper().Contains("FIELD") && pinfo.Name.ToUpper().Contains("NAME"))
                {
                    typeProperty = pinfo;
                    break;
                }
            }

            string fieldName = null;

            if (typeProperty == null)
            {
                if (context.Instance is XmlPropertyBoolNode.XmlPropertyCaseBoolNode)
                {
                    fieldName = XmlPropertyBoolNode.XmlPropertyCaseBoolNode.CaseFieldName;
                }
                else if (context.Instance is XmlPropertyByteNode.XmlPropertyCaseByteNode)
                {
                    fieldName = XmlPropertyByteNode.XmlPropertyCaseByteNode.CaseFieldName;
                }
                else if (context.Instance is XmlPropertyIntNode.XmlPropertyCaseIntNode)
                {
                    fieldName = XmlPropertyIntNode.XmlPropertyCaseIntNode.CaseFieldName;
                }
                else if (context.Instance is XmlPropertyUIntNode.XmlPropertyCaseUIntNode)
                {
                    fieldName = XmlPropertyUIntNode.XmlPropertyCaseUIntNode.CaseFieldName;
                }
                else if (context.Instance is XmlPropertyFloatNode.XmlPropertyCaseFloatNode)
                {
                    fieldName = XmlPropertyFloatNode.XmlPropertyCaseFloatNode.CaseFieldName;
                }
                else if (context.Instance is XmlPropertyDoubleNode.XmlPropertyCaseDoubleNode)
                {
                    fieldName = XmlPropertyDoubleNode.XmlPropertyCaseDoubleNode.CaseFieldName;
                }
                else if (context.Instance is XmlPropertyStringNode.XmlPropertyCaseStringNode)
                {
                    fieldName = XmlPropertyStringNode.XmlPropertyCaseStringNode.CaseFieldName;
                }
                else if (context.Instance is XmlPropertyColorNode.XmlPropertyCaseColorNode)
                {
                    fieldName = XmlPropertyColorNode.XmlPropertyCaseColorNode.CaseFieldName;
                }
                else if (context.Instance is XmlPropertyFVector2DNode.XmlPropertyCaseFVector2DNode)
                {
                    fieldName = XmlPropertyFVector2DNode.XmlPropertyCaseFVector2DNode.CaseFieldName;
                }
                else if (context.Instance is XmlPropertyVector2DNode.XmlPropertyCaseVector2DNode)
                {
                    fieldName = XmlPropertyVector2DNode.XmlPropertyCaseVector2DNode.CaseFieldName;
                }
                else if (context.Instance is XmlPropertyFVector3DNode.XmlPropertyCaseFVector3DNode)
                {
                    fieldName = XmlPropertyFVector3DNode.XmlPropertyCaseFVector3DNode.CaseFieldName;
                }
                else if (context.Instance is XmlPropertyVector3DNode.XmlPropertyCaseVector3DNode)
                {
                    fieldName = XmlPropertyVector3DNode.XmlPropertyCaseVector3DNode.CaseFieldName;
                }
                else if (context.Instance is XmlPropertyTextureNode.XmlPropertyCaseTextureNode)
                {
                    fieldName = XmlPropertyTextureNode.XmlPropertyCaseTextureNode.CaseFieldName;
                }
                else if (context.Instance is XmlPropertyFontTypeNode.XmlPropertyCaseFontTypeNode)
                {
                    fieldName = XmlPropertyFontTypeNode.XmlPropertyCaseFontTypeNode.CaseFieldName;
                }
                else if (context.Instance is XmlPropertyScaleCondSelectorNode.XmlPropertyCaseScaleCondSelectorNode)
                {
                    fieldName = XmlPropertyScaleCondSelectorNode.XmlPropertyCaseScaleCondSelectorNode.CaseFieldName;
                }
                else
                {
                    return value;
                }
            }
            else
            {
                if (typeProperty.GetValue(context.Instance, null) != null)
                {
                    fieldName = typeProperty.GetValue(context.Instance, null).ToString();
                }
            }

            if (fieldName == null)
            {
                MessageBox.Show("No field was chosen, cannot get values.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return value;
            }

            uint fieldId = 0;
            DNEFieldType fieldType;
            string currField = "";

            for (uint i=0; i<DataSourceType.Instance.Layer.GetNumFields(); i++)
            {
                DataSourceType.Instance.Layer.GetFieldData(i,out currField, out fieldType);
                if (currField == fieldName)
                {
                    fieldId = i;
                    break;
                }
            }

            Type t = context.PropertyDescriptor.PropertyType;

            if (t == typeof(int))
            {
                int[] items = DataSourceType.Instance.Layer.GetFieldUniqueValuesAsInt(fieldId);
                foreach (int item in items)
                {
                    lb.Items.Add(item);
                }
            }
            else if (t == typeof(double))
            {
                double[] items = DataSourceType.Instance.Layer.GetFieldUniqueValuesAsDouble(fieldId);
                foreach (double item in items)
                {
                    lb.Items.Add(item);
                }
            }
            else if (t == typeof(string))
            {
                if (typeProperty == null)
                {
                    lb.Items.Add("McDefaultValue"); // _MC_XML_DEFAULT_VALUE
                }

                string[] items = DataSourceType.Instance.Layer.GetFieldUniqueValuesAsString(fieldId);
                foreach (string item in items)
                {
                    lb.Items.Add(item);
                }
            }
            else
            {
                return value;
            }

            // Show data
            _editorService.DropDownControl(lb);

            // If no selection, return passed in value as is
            if (lb.SelectedItem == null)
            {
                return value;
            }
            return lb.SelectedItem;
        }


        private void OnListBoxSelectedValueChanged(object sender, EventArgs e)
        {
            // close the drop down as soon as something is clicked
            _editorService.CloseDropDown();
        }

    }

    [Browsable(true)]
    public class XmlVectorLayerGraphicalSettings
    {
        DNSXmlVectorLayerGraphicalSettings m_settings;
        string m_dataSource;
        // string m_dataSourceStatus;

        public XmlVectorLayerGraphicalSettings()
        {
            m_settings = new DNSXmlVectorLayerGraphicalSettings();
            ScaleRange = new XmlScaleRangeNode();
            ScaleRange.fMin = 0;
            ScaleRange.fMax = float.MaxValue;
            fMaxScaleFactor = 1f;
            fEquidistantMinScale = 1f;
            MultiCondition = false;
        }

        public XmlVectorLayerGraphicalSettings(DNSXmlVectorLayerGraphicalSettings settings)
        {
            m_settings = settings;
            fEquidistantMinScale = m_settings.fEquidistantMinScale;
            fMaxScaleFactor = m_settings.fMaxScaleFactor;
            ScaleRange = (XmlScaleRangeNode)m_settings.ScaleRange;
            //m_dataSource
            //strDataSource = m_settings.strDataSource;
            CheckDataSource(m_settings.strDataSource);
            aObjectConditions = FromDNSArray(m_settings.aObjectConditions);
            MultiCondition = m_settings.bMultiCondition;
        }

        public static explicit operator DNSXmlVectorLayerGraphicalSettings(XmlVectorLayerGraphicalSettings args)
        {
            args.m_settings = new DNSXmlVectorLayerGraphicalSettings();
            args.m_settings.aObjectConditions = args.ToXmlArray(args.aObjectConditions);
            args.m_settings.fEquidistantMinScale = args.fEquidistantMinScale;
            args.m_settings.fMaxScaleFactor = args.fMaxScaleFactor;
            args.m_settings.ScaleRange = (DNSXmlScaleRangeNode)args.ScaleRange;
            args.m_settings.strDataSource = /*args.strDataSource*/args.DataSourceToXML();
            args.m_settings.bMultiCondition = args.MultiCondition;

            return args.m_settings;
        }


        public static explicit operator XmlVectorLayerGraphicalSettings(DNSXmlVectorLayerGraphicalSettings args)
        {
            return new XmlVectorLayerGraphicalSettings(args);
        }

        [Category("Settings")]
        [DisplayName("5. Conditions")]
        [Description("Conditions to assign object(s) to the layer")]
        public XmlObjectConditionNode[] aObjectConditions { set; get; }
        //{ 
        //    get { return FromDNSArray(m_settings.aObjectConditions); }
        //    set { m_settings.aObjectConditions = ToXmlArray(value); }
        //}

        [Category("Settings")]
        [DisplayName("2. Equidistant Minimal Scale")]
        [Description("Equidistant Minimal Scale")]
        public float fEquidistantMinScale { get; set; }
        //{
        //    get { return m_settings.fEquidistantMinScale; }
        //    set { m_settings.fEquidistantMinScale = value; }
        //}

        [Category("Settings")]
        [DisplayName("3. Max Scale Factor")]
        [Description("Range for scales")]
        public float fMaxScaleFactor { get; set; }
        //{
        //    get { return m_settings.fMaxScaleFactor; }
        //    set { m_settings.fMaxScaleFactor = value; }
        //}

        [Category("Settings")]
        [DisplayName("4. Scale Range")]
        [Description("Range for scales")]
        [TypeConverter(typeof(ScaleConverter))]
        public XmlScaleRangeNode ScaleRange { get; set; }
        //{
        //    get { return (XmlScaleRangeNode)m_settings.ScaleRange; }
        //    set { m_settings.ScaleRange = (DNSXmlScaleRangeNode)value; }
        //}

        [Category("Settings")]
        [DisplayName("1. Data Source")]
        [Description("URI for data vector data source")]
        [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
        public string strDataSource
        {
            get { return m_dataSource /*+ m_dataSourceStatus;*/ ; }
            set 
            {
                m_dataSource = value;
                if (DataSourceType.Instance.Open(value))
                {
                    string path = Manager_MCLayers.CheckRawVector(value, false, false);
                    DataSourceSuffix = "";
                    if (path.Contains(Manager_MCLayers.RawVectorSingleSplitStr))
                    {
                        DataSourceSuffix = path.Replace(m_dataSource, "");
                    }
                    DataSourceStatus = "(Open OK)";
                }
                else
                {
                    if (m_dataSource != "")
                    {
                        DataSourceStatus = "(Open Fail)";
                        MessageBox.Show("Vector Layer's data source could not be opened.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        [Category("Settings")]
        [DisplayName("6. Data Source Suffix")]
        [Description("Data Source Suffix (optional, if data source include sub layers)")]
        public string DataSourceSuffix { get; set; }

        [Category("Settings")]
        [DisplayName("7. Data Source Status")]
        [Description("Data Source Status")]
        public string DataSourceStatus { get; set; }

        [Category("Settings")]
        [DisplayName("8. Multi Condition")]
        [Description("Multi Condition")]
        public bool MultiCondition { get; set; }

        private string DataSourceToXML()
        {
            return m_dataSource + DataSourceSuffix;
        }

        private void CheckDataSource(string dataSource/*, bool isOpenSubLyersForm, bool isSetDataSource*/)
        {
            if (dataSource != "" && dataSource.Contains(Manager_MCLayers.RawVectorSingleSplitChar))
            {
                int index = dataSource.IndexOf(Manager_MCLayers.RawVectorSingleSplitChar);
                m_dataSource = dataSource.Substring(0, index);
                DataSourceSuffix = dataSource.Replace(m_dataSource, "");
            }
            else
                m_dataSource = dataSource;

            if (DataSourceType.Instance.Open(m_dataSource))
            {
                DataSourceStatus = "(Open OK)";
            }
            else
            {
                if (m_dataSource != "")
                {
                    DataSourceStatus = "(Open Fail)";
                    MessageBox.Show("Vector Layer's data source could not be opened.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private XmlObjectConditionNode[] FromDNSArray(DNSXmlObjectConditionNode[] arg)
        {
            List<XmlObjectConditionNode> result = new List<XmlObjectConditionNode>();

            if (arg != null)
            {
                foreach (DNSXmlObjectConditionNode item in arg)
                {
                    result.Add((XmlObjectConditionNode)item);
                }
            }

            return result.ToArray();
        }

        private DNSXmlObjectConditionNode[] ToXmlArray(XmlObjectConditionNode[] arg)
        {
            List<DNSXmlObjectConditionNode> result = new List<DNSXmlObjectConditionNode>();

            if (arg != null)
            {
                foreach (XmlObjectConditionNode item in arg)
                {
                    result.Add((DNSXmlObjectConditionNode)item);
                }
            }

            return result.ToArray();
        }
    }
}
