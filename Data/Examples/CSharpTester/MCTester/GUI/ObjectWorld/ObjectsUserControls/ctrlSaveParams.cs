using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;
using MCTester.ObjectWorld.OverlayManagerWorld;
using System.IO;
using MapCore.Common;

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class ctrlSaveParams : UserControl
    {
       
        //bool load = false;
        public ctrlSaveParams()
        {
            InitializeComponent();
            string[] versions = Enum.GetNames(typeof(DNESavingVersionCompatibility));
            string[] versions2 = new string[versions.Length+1];
            versions2[0] = SaveParamsData.OriginalVersionText;
            versions2[1] = DNESavingVersionCompatibility._ESVC_LATEST.ToString();
            for (int i = 2; i < versions2.Length; i++)
            {
                versions2[i] = versions[i-1].ToString();
            }
            cbSavingVersionCompatibility.Items.AddRange(versions2);
             if (SaveParamsData.IsSaveWithOriginalVersion)
                 cbSavingVersionCompatibility.Text = SaveParamsData.OriginalVersionText;
             else
                 cbSavingVersionCompatibility.Text = SaveParamsData.SavingVersionCompatibility.ToString();
          
            chxSaveLoadPropertiesCSV.Checked  = SaveParamsData.IsSaveLoadPropertiesCSV;
        }

    
        private void cbSavingVersionCompatibility_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSavingVersionCompatibility.Text != "")
            {
                if (cbSavingVersionCompatibility.Text == SaveParamsData.OriginalVersionText)
                    SaveParamsData.IsSaveWithOriginalVersion = true;
                else
                {
                    SaveParamsData.IsSaveWithOriginalVersion = false;

                    SaveParamsData.SavingVersionCompatibility = (DNESavingVersionCompatibility)Enum.Parse(typeof(DNESavingVersionCompatibility), cbSavingVersionCompatibility.Text);
                }
            }
        }

        public DNESavingVersionCompatibility GetSavingVersionCompatibility()
        {
            if (SaveParamsData.IsSaveWithOriginalVersion)
                return DNESavingVersionCompatibility._ESVC_LATEST;
            else
                return SaveParamsData.SavingVersionCompatibility;
        }

        private void chxSaveLoadPropertiesCSV_CheckedChanged(object sender, EventArgs e)
        {
            SaveParamsData.IsSaveLoadPropertiesCSV = chxSaveLoadPropertiesCSV.Checked;
        }
    }

    public static class SaveParamsData
    {
        public static string OriginalVersionText = "Object/scheme's current version";
        public static bool IsSaveUserData;
        public static bool IsSavePropertiesNames = true;
        public static bool IsSaveLoadPropertiesCSV;
        public static bool IsSaveWithOriginalVersion = true;

        public static DNESavingVersionCompatibility SavingVersionCompatibility = DNESavingVersionCompatibility._ESVC_LATEST;

        public static void SavePropertiesCSV(string filePath, IDNMcObjectScheme scheme)
        {
            try
            {
                if (SaveParamsData.IsSaveLoadPropertiesCSV == true)
                {
                    DNSPropertyNameIDType[] propId = scheme.GetProperties();
                    DNSPropertyNameID[] names = scheme.GetPropertyNames();

                    PrivatePropertyTable propTable = new PrivatePropertyTable(propId.Length);
                    propTable.ArrPropertyId = propId;

                    // fill table name column
                    for (int currName = 0; currName < names.Length; currName++)
                    {
                        uint currId = names[currName].uID;
                        for (int i = 0; i < propTable.ArrPropertyId.Length; i++)
                        {
                            if (currId == propTable.ArrPropertyId[i].uID)
                                propTable.ArrPropertyName[i] = names[currName].strName;
                        }
                    }

                    string csvPath = Path.GetDirectoryName(filePath) + "\\" + Path.GetFileNameWithoutExtension(filePath) + ".csv";
                    StreamWriter STW = new StreamWriter(csvPath);
                    string outputLine = "";

                    for (int tableRow = 0; tableRow < propTable.ArrPropertyId.Length; tableRow++)
                    {
                        outputLine = propTable.ArrPropertyId[tableRow].uID.ToString() + "," +
                                        propTable.ArrPropertyId[tableRow].eType.ToString() + "," +
                                        propTable.ArrPropertyName[tableRow];

                        STW.WriteLine(outputLine);
                    }

                    STW.Close();
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetProperties Or GetPropertyNames", McEx);
            }

        }
    }


}
