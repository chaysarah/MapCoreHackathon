using System;
using System.Windows.Forms;
using MapCore;
using MapCore.Common;
using UnmanagedWrapper;
using System.Linq;
using MCTester.Controls;
using System.Globalization;
using System.IO;

namespace MCTester.General_Forms
{
    public partial class frmSaveSchemeComponentInterface : Form
    {
        private SaveFileDialog mSFD;
        DNESchemeComponentKind mEObjectSchemeComponentKind;
        uint uComponentType;

        DNESchemeComponentKind[] mSchemeComponentKinds;
        DNEObjectSchemeNodeType[] mSchemeNodeTypes;
        DNEConditionalSelectorType[] mConditionalSelectorTypes;
        DNEFontType[] mFontTypes;
        DNEMeshType[] mMeshTypes;
        DNETextureType[] mTextureTypes;

        string[] mSchemeComponentKindsNames;
        string[] mSchemeNodeTypesNames;
        string[] mConditionalSelectorTypesNames;
        string[] mFontTypesNames;
        string[] mMeshTypesNames;
        string[] mTextureTypesNames;

        public frmSaveSchemeComponentInterface()
        {
            InitializeComponent();
            LoadList();
        }

        private void LoadList()
        {
            mSchemeComponentKinds = Enum.GetValues(typeof(DNESchemeComponentKind)).Cast<DNESchemeComponentKind>().ToArray();
            mSchemeNodeTypes = Enum.GetValues(typeof(DNEObjectSchemeNodeType)).Cast<DNEObjectSchemeNodeType>().ToArray();
            mConditionalSelectorTypes = Enum.GetValues(typeof(DNEConditionalSelectorType)).Cast<DNEConditionalSelectorType>().ToArray();
            mFontTypes = Enum.GetValues(typeof(DNEFontType)).Cast<DNEFontType>().ToArray();
            mMeshTypes = Enum.GetValues(typeof(DNEMeshType)).Cast<DNEMeshType>().ToArray();
            mTextureTypes = Enum.GetValues(typeof(DNETextureType)).Cast<DNETextureType>().ToArray();

            mSchemeComponentKindsNames = Enum.GetNames(typeof(DNESchemeComponentKind));
            mSchemeNodeTypesNames = Enum.GetNames(typeof(DNEObjectSchemeNodeType));
            mConditionalSelectorTypesNames = Enum.GetNames(typeof(DNEConditionalSelectorType));
            mFontTypesNames = Enum.GetNames(typeof(DNEFontType));
            mMeshTypesNames = Enum.GetNames(typeof(DNEMeshType));
            mTextureTypesNames = Enum.GetNames(typeof(DNETextureType));

            lstSchemeComponentKind.Items.AddRange(mSchemeComponentKindsNames);
            lstSchemeComponentKind.SelectedIndex = 0;
            mEObjectSchemeComponentKind = DNESchemeComponentKind._ESCK_OBJECT_SCHEME_NODE;
            uComponentType = (uint)DNEObjectSchemeNodeType._NONE;
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            mSFD = new SaveFileDialog();
            mSFD.Filter = "json files|*.json";
            mSFD.RestoreDirectory = true;
            if (mSFD.ShowDialog() == DialogResult.OK)
            {
                uComponentType = 0;

                if (lstComponentType.SelectedItem != null)
                {
                    string selectedComponentTypeStr = "";
                    
                    switch (mEObjectSchemeComponentKind)
                    {
                        default:
                        case DNESchemeComponentKind._ESCK_OBJECT_SCHEME_NODE:
                            {
                                if (lstComponentType.SelectedIndex < mSchemeNodeTypes.Length)
                                {
                                    selectedComponentTypeStr = mSchemeNodeTypes[lstComponentType.SelectedIndex].ToString();
                                    uComponentType = (uint)(DNEObjectSchemeNodeType)Enum.Parse(typeof(DNEObjectSchemeNodeType), selectedComponentTypeStr);
                                }
                                break;
                            }
                        case DNESchemeComponentKind._ESCK_CONDITIONAL_SELECTOR:
                            {
                                if (lstComponentType.SelectedIndex < mConditionalSelectorTypes.Length)
                                {
                                    selectedComponentTypeStr = mConditionalSelectorTypes[lstComponentType.SelectedIndex].ToString();
                                    uComponentType = (uint)(DNEConditionalSelectorType)Enum.Parse(typeof(DNEConditionalSelectorType), selectedComponentTypeStr); ;
                                }
                                break;
                            }
                        case DNESchemeComponentKind._ESCK_FONT:
                            {
                                if (lstComponentType.SelectedIndex < mFontTypes.Length)
                                {
                                    selectedComponentTypeStr = mFontTypes[lstComponentType.SelectedIndex].ToString();
                                    uComponentType = (uint)(DNEFontType)Enum.Parse(typeof(DNEFontType), selectedComponentTypeStr); ;
                                }
                                break;
                            }
                        case DNESchemeComponentKind._ESCK_MESH:
                            {
                                if (lstComponentType.SelectedIndex < mMeshTypes.Length)
                                {
                                    selectedComponentTypeStr = mMeshTypes[lstComponentType.SelectedIndex].ToString();
                                    uComponentType = (uint)(DNEMeshType)Enum.Parse(typeof(DNEMeshType), selectedComponentTypeStr); ;
                                }
                                break;
                            }
                        case DNESchemeComponentKind._ESCK_TEXTURE:
                            {
                                if (lstComponentType.SelectedIndex < mTextureTypes.Length)
                                {
                                    selectedComponentTypeStr = mTextureTypes[lstComponentType.SelectedIndex].ToString();
                                    uComponentType = (uint)(DNETextureType)Enum.Parse(typeof(DNETextureType), selectedComponentTypeStr); ;
                                }
                                break;
                            }
                        case DNESchemeComponentKind._ESCK_ENUMERATION:
                            {
                                uComponentType = 0;
                                break;
                            }
                    }
                }

                SaveSchemeComponentInterface(mEObjectSchemeComponentKind, uComponentType, mSFD.FileName);
            }
        }

        private void SaveSchemeComponentInterface(DNESchemeComponentKind eComponentKind, uint uComponentType, string strJsonFileName, bool toTitleCase = false)
        {
            try
            {
                if (cbGetString.Checked)
                {
                    byte[] outputBuffer = DNMcObjectScheme.SaveSchemeComponentInterface(eComponentKind, uComponentType);
                    System.IO.File.WriteAllBytes(@strJsonFileName, outputBuffer);
                }
                else
                {
                    DNMcObjectScheme.SaveSchemeComponentInterface(eComponentKind, uComponentType, strJsonFileName);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcObjectScheme.SaveSchemeComponentInterface", McEx);
            }
        }

        private void lstSchemeComponentKind_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSchemeComponentKind.SelectedIndex >= 0 && mSchemeComponentKinds.Length > lstSchemeComponentKind.SelectedIndex)
            {
                lstComponentType.Items.Clear();
                string selectedStr = mSchemeComponentKinds[lstSchemeComponentKind.SelectedIndex].ToString();
                mEObjectSchemeComponentKind = (DNESchemeComponentKind)Enum.Parse(typeof(DNESchemeComponentKind), selectedStr);
                switch(mEObjectSchemeComponentKind)
                {
                    default:
                    case DNESchemeComponentKind._ESCK_OBJECT_SCHEME_NODE:
                        {
                            lstComponentType.Items.AddRange(mSchemeNodeTypesNames);
                            uComponentType = (uint)DNEObjectSchemeNodeType._NONE;
                            break;
                        }
                    case DNESchemeComponentKind._ESCK_CONDITIONAL_SELECTOR:
                        {
                            lstComponentType.Items.AddRange(mConditionalSelectorTypesNames);
                            uComponentType = (uint)DNEConditionalSelectorType._EVST_NONE;
                            break;
                        }
                    case DNESchemeComponentKind._ESCK_FONT:
                        {
                            lstComponentType.Items.AddRange(mFontTypesNames);
                            uComponentType = (uint)DNEFontType._EFT_NONE;
                            break;
                        }
                    case DNESchemeComponentKind._ESCK_MESH:
                        {
                            lstComponentType.Items.AddRange(mMeshTypesNames);
                            uComponentType = (uint)DNEMeshType._EMT_NONE;
                            break;
                        }
                    case DNESchemeComponentKind._ESCK_TEXTURE:
                        {
                            lstComponentType.Items.AddRange(mTextureTypesNames);
                            uComponentType = (uint)DNEMeshType._EMT_NONE;
                            break;
                        }
                    case DNESchemeComponentKind._ESCK_ENUMERATION:
                        {
                            lstComponentType.Items.Clear();
                            uComponentType = 0;
                            break;
                        }
                }
                if (lstComponentType.Items.Count > 0)
                {
                    if (lstComponentType.Items[0].ToString().Contains("NONE"))
                    {
                        lstComponentType.Items.RemoveAt(0);
                        lstComponentType.Items.Insert(0, "List");
                    }
                    lstComponentType.SelectedIndex = 0;
                }
            }
        }

        private void btnSaveAllToFolder_Click(object sender, EventArgs e)
        {
            FolderSelectDialog FSD = new FolderSelectDialog();
            FSD.Title = "Folder to select";

            if (FSD.ShowDialog(IntPtr.Zero))
            {
                string destDir = FSD.FileName;
                string fileName = "";
                foreach (DNESchemeComponentKind kind in mSchemeComponentKinds)
                {
                    switch (kind)
                    {
                        case DNESchemeComponentKind._ESCK_OBJECT_SCHEME_NODE:
                            {
                                foreach (DNEObjectSchemeNodeType type in mSchemeNodeTypes)
                                {
                                    string strType = type.ToString();
                                    if (type == DNEObjectSchemeNodeType._NONE)
                                    {
                                        fileName = "Object_Scheme_Nodes";
                                    }
                                    else
                                    {
                                        fileName = ToTitleCase(strType.Remove(0, 1));
                                    }
                                    SaveSchemeComponentInterface(kind, (uint)type, Path.Combine(destDir, fileName +".json"));
                                }
                                break;
                            }
                        case DNESchemeComponentKind._ESCK_CONDITIONAL_SELECTOR:
                            {
                                foreach (DNEConditionalSelectorType type in mConditionalSelectorTypes)
                                {
                                    string strType = type.ToString();
                                    if (type == DNEConditionalSelectorType._EVST_NONE)
                                    {
                                        fileName = "Conditional_Selectors";
                                    }
                                    else
                                    {
                                        fileName = ToTitleCase(strType.Replace("_EVST_", ""));
                                    }
                                    SaveSchemeComponentInterface(kind, (uint)type, Path.Combine(destDir, fileName+ ".json"));
                                }
                            }
                            break;
                        case DNESchemeComponentKind._ESCK_FONT:
                            {
                                foreach (DNEFontType type in mFontTypes)
                                {
                                    string strType = type.ToString();
                                    if (type == DNEFontType._EFT_NONE)
                                    {
                                        fileName = "Fonts";
                                    }
                                    else
                                    {
                                        fileName = ToTitleCase(strType.Replace("_EFT_", ""));
                                    }
                                    SaveSchemeComponentInterface(kind, (uint)type, Path.Combine(destDir, fileName+ ".json"));
                                }
                            }
                            break;
                        case DNESchemeComponentKind._ESCK_MESH:
                            {
                                foreach (DNEMeshType type in mMeshTypes)
                                {
                                    string strType = type.ToString();
                                    if (type == DNEMeshType._EMT_NONE)
                                    {
                                        fileName = "Meshes";
                                    }
                                    else
                                    {
                                        fileName = ToTitleCase(strType.Replace("_EMT_", ""));
                                    }
                                    
                                    SaveSchemeComponentInterface(kind, (uint)type, Path.Combine(destDir, fileName+ ".json"));
                                }
                            }
                            break;
                        case DNESchemeComponentKind._ESCK_TEXTURE:
                            {
                                foreach (DNETextureType type in mTextureTypes)
                                {
                                    string strType = type.ToString();
                                    if (type == DNETextureType._ETT_NONE)
                                    {
                                        fileName = "Textures";
                                    }
                                    else
                                    {
                                        fileName = ToTitleCase(strType.Replace("_ETT_", ""));
                                    }
                                    
                                    SaveSchemeComponentInterface(kind, (uint)type, Path.Combine(destDir, fileName+ ".json"));
                                }
                            }
                            break;
                        case DNESchemeComponentKind._ESCK_ENUMERATION:
                            {
                                fileName = "Enums.json";
                                SaveSchemeComponentInterface(kind, (uint)0, Path.Combine(destDir, fileName));
                            }
                            break;
                    }
                }

            }
        }

        public string ToTitleCase(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }
    }
}
