using MapCore;
using MCTester.ObjectWorld.Assit_Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCTester.Managers
{
    public static class Manager_MCPropertyDescription
    {
        private static frmPrivatePropertiesDescription frmPrivatePropertiesDescription;

        public static frmPrivatePropertiesDescription FrmPrivatePropertiesDescription
        {
            get { return Manager_MCPropertyDescription.frmPrivatePropertiesDescription; }
            set { Manager_MCPropertyDescription.frmPrivatePropertiesDescription = value; }
        }
        private static Dictionary<IDNMcObjectSchemeNode, List<KeyValuePair<uint, string>>> propertyDescription;

        static Manager_MCPropertyDescription()
        {
            propertyDescription = new Dictionary<IDNMcObjectSchemeNode, List<KeyValuePair<uint, string>>>();
        }

        public static Dictionary<IDNMcObjectSchemeNode, List<KeyValuePair<uint, string>>> PropertyDescription
        {
            get { return Manager_MCPropertyDescription.propertyDescription; }
            set { Manager_MCPropertyDescription.propertyDescription = value; }
        }

        public static void InsertNode(IDNMcObjectSchemeNode node, uint propertyId, string propertyDesc)
        {
            if (propertyId != DNMcConstants._MC_EMPTY_ID && node != null)
            {
                string newPropertyDesc = "";
                if (propertyDesc != "")
                    newPropertyDesc = propertyDesc.Replace("Get", "").Replace("Set", "");
                List<KeyValuePair<uint, string>> list;
                if (propertyDescription.ContainsKey(node))
                {
                    list = propertyDescription[node];
                }
                else
                {
                    list = new List<KeyValuePair<uint, string>>(); ;
                    propertyDescription.Add(node, list);
                }

                if (!list.Exists(x => x.Key == propertyId && x.Value == newPropertyDesc))
                {
                    KeyValuePair<uint, string> newKeyValuePropertyDesc = new KeyValuePair<uint, string>(propertyId, newPropertyDesc);
                    list.Add(newKeyValuePropertyDesc);
                }
            }
        }

        public static List<KeyValuePair<uint, string>> GetProprtyDescByNodeAndId(IDNMcObjectSchemeNode node, uint propertyId)
        { 
            List<KeyValuePair<uint, string>> list = new List<KeyValuePair<uint,string>>();
            if (propertyDescription.ContainsKey(node))
            {
                list = propertyDescription[node];
                if (list != null)
                {
                    List<KeyValuePair<uint, string>> listPropertyId = list.FindAll(x => x.Key == propertyId);
                    return listPropertyId;
                }
            }
            return null;
        }

        public static void RemoveNode(IDNMcObjectSchemeNode node)
        {
            if (propertyDescription.ContainsKey(node))
            {
                propertyDescription.Remove(node);
            }
        }
    }
}
