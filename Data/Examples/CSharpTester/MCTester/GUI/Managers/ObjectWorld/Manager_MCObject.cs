using System;
using System.Collections.Generic;
using System.Text;
using UnmanagedWrapper;
using MapCore;
using MapCore.Common;
using System.Drawing;
using MCTester.General_Forms;
using MCTester.Managers.MapWorld;
using System.Windows;
using System.IO;
using MCTester.ObjectWorld.OverlayManagerWorld;

namespace MCTester.Managers.ObjectWorld
{
    public static class Manager_MCObject 
    {
        static Dictionary<object, uint> dObject;

        static Manager_MCObject()
        {
            dObject = new Dictionary<object, uint >();
        }

        #region IManagersGetter Members

        public static Dictionary<object, uint> AllParams
        {
            get { return dObject; }
        }

        public static Dictionary<object, uint> GetChildren(object Parent)
		{
			Dictionary<object, uint > Ret = new Dictionary<object, uint >();
            
            return Ret;
		}

        public static uint TEXT_PROPERTY_ID = 1;
        public static uint COLOR_PROPERTY_ID = 2;

        public static void MoveToLocation(IDNMcObject mcObject,uint locationIndex = 0, IDNMcMapViewport viewport = null, bool isNeedToRenderVp = true)
        {
            try
            {
                DNSMcVector3D point = new DNSMcVector3D();
                DNSMcVector3D[] objectLocations = mcObject.GetLocationPoints(locationIndex);

                DNSMcVector3D min = new DNSMcVector3D(), max = new DNSMcVector3D();
                DNSMcVector3D curr;
                IDNMcMapViewport currViewport; 

                // point - calc average of bounding box of object
                if (objectLocations != null && objectLocations.Length > 0)
                {
                    if (objectLocations.Length == 1)
                        point = objectLocations[0];
                    else if (objectLocations.Length > 1)
                    {
                        min = objectLocations[0];
                        max = objectLocations[1];
                    }

                    if (objectLocations.Length > 2)
                    {
                        for (int i = 0; i < objectLocations.Length; i++)
                        {
                            curr = objectLocations[i];
                            point += objectLocations[i];

                            if (curr.x >= max.x && curr.y >= max.y)
                                max = curr;
                            else if (curr.x <= min.x && curr.y <= min.y)
                                min = curr;
                        }
                    }
                    if (objectLocations.Length > 1)
                        point = (min + max) / 2;

                    if (viewport != null)
                        currViewport = viewport;
                    else
                        currViewport = MapWorld.MCTMapFormManager.MapForm.Viewport;

                    if (currViewport != null)
                    {
                        MCTMapClick.ConvertObjectLocationToCameraLocation(currViewport, point, mcObject, locationIndex, isNeedToRenderVp);
                    }
                    else
                    {
                        MessageBox.Show("Missing viewport");
                    }
                 
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("MoveToLocation", McEx);
            }
        }


        public static IDNMcObjectScheme GetTempObjectSchemeScreenPoints(IDNMcMapViewport pMapViewport)
        {
            Font font = new Font(FontFamily.GenericSansSerif, 14,System.Drawing.FontStyle.Bold);
            DNSMcLogFont logFont = new DNSMcLogFont();
            font.ToLogFont(logFont);

            DNSCharactersRange[] aCharachterRanges = new DNSCharactersRange[1];
            aCharachterRanges[0] = new DNSCharactersRange();
            aCharachterRanges[0].nFrom = '0';
            aCharachterRanges[0].nTo = '9';

            IDNMcLogFont DefaultFont = DNMcLogFont.Create(new DNMcVariantLogFont(logFont, false), false, aCharachterRanges);

            IDNMcTextItem textItem = DNMcTextItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN, DNEMcPointCoordSystem._EPCS_SCREEN, DefaultFont);
            IDNMcObjectScheme mcObjectScheme = DNMcObjectScheme.Create(pMapViewport.OverlayManager, textItem, DNEMcPointCoordSystem._EPCS_SCREEN);

            textItem.SetText(new DNMcVariantString("", true), TEXT_PROPERTY_ID, false);
            textItem.SetTextColor(new DNSMcBColor(), COLOR_PROPERTY_ID, false);
            textItem.SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
            textItem.SetRectAlignment(DNEBoundingRectanglePoint._EBRP_BOTTOM_MIDDLE, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, 0);

            Manager_MCTSymbology.GetTempOverlay(pMapViewport.OverlayManager).SetVisibilityOption(DNEActionOptions._EAO_FORCE_FALSE);
            Manager_MCTSymbology.GetTempOverlay(pMapViewport.OverlayManager).SetVisibilityOption(DNEActionOptions._EAO_FORCE_TRUE, pMapViewport);

            return mcObjectScheme;
        }

        public static DNEStorageFormat GetStorageFormatByFileName(string fileName)
        {
            DNEStorageFormat dNEStorageFormat = DNEStorageFormat._ESF_MAPCORE_BINARY;
            string fileExtension = Path.GetExtension(fileName);
            if (fileExtension.ToLower() == ".json")
                dNEStorageFormat = DNEStorageFormat._ESF_JSON;
            return dNEStorageFormat;
        }

        #endregion


    }
}

