using MapCore;
using MapCore.Common;
using MCTester.General_Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UnmanagedWrapper;

namespace MCTester.Managers.ObjectWorld
{
    public static class Manager_MCTSymbology
    {
        static uint TEXT_PROPERTY_ID = 1;
        static Dictionary<IDNMcOverlayManager, IDNMcOverlay> dTempOverlays = new Dictionary<IDNMcOverlayManager, IDNMcOverlay>();
        static Dictionary<IDNMcOverlayManager, IDNMcObjectScheme> dAnchorPointScheme = new Dictionary<IDNMcOverlayManager, IDNMcObjectScheme>();

        static Dictionary<IDNMcObject, List<IDNMcObject>> dTempAnchorPoints = new Dictionary<IDNMcObject, List<IDNMcObject>>();
        static Dictionary<frmCreateSymbology, IDNMcObject> dfrmCreateSymbologyObjects = new Dictionary<frmCreateSymbology, IDNMcObject>();

        static Dictionary<IDNMcOverlayManager, DNESymbologyStandard> dSymbologyStandard = new Dictionary<IDNMcOverlayManager, DNESymbologyStandard>();
        public static bool IgnoreNonExistentAmplifiers = true;
        public static bool ShowGeoInMetricProportion = true;

        public class SymbologyStandardFlags
        {
            public DNESymbologyStandard SymbologyStandard;
            public bool IgnoreNonExistentAmplifiers = true;
            public bool ShowGeoInMetricProportion = true;

            public SymbologyStandardFlags(DNESymbologyStandard eSymbologyStandard, bool bShowGeoInMetricProportion, bool bIgnoreNonExistentAmplifiers)
            {
                SymbologyStandard = eSymbologyStandard;
                ShowGeoInMetricProportion = bShowGeoInMetricProportion;
                IgnoreNonExistentAmplifiers = bIgnoreNonExistentAmplifiers;
            }
        }

        private static Dictionary<IDNMcOverlayManager, List<SymbologyStandardFlags>> dSymbologyStandardFlags = new Dictionary<IDNMcOverlayManager, List<SymbologyStandardFlags>>();

        public static void InitSymbologyStandardFlags(IDNMcOverlayManager overlayManager)
        {
            if(!dSymbologyStandardFlags.ContainsKey(overlayManager))
            {
                Array values = Enum.GetValues(typeof(DNESymbologyStandard));
                List<SymbologyStandardFlags> dFlags = new List<SymbologyStandardFlags>();
                foreach (object obj in values)
                {
                    dFlags.Add(new SymbologyStandardFlags((DNESymbologyStandard)obj, true, true));
                }
                dSymbologyStandardFlags.Add(overlayManager, dFlags);
            }
            if(!dSymbologyStandard.ContainsKey(overlayManager))
            {
                dSymbologyStandard.Add(overlayManager, DNESymbologyStandard._ESS_APP6D);
            }
        }

        public static void SetSymbologyStandardFlags(IDNMcOverlayManager overlayManager, DNESymbologyStandard symbologyStandard, bool bShowGeoInMetricProportion, bool bIgnoreNonExistentAmplifiers)
        {
            SymbologyStandardFlags standardFlags = dSymbologyStandardFlags[overlayManager].Find(x => x.SymbologyStandard == symbologyStandard);
            standardFlags.ShowGeoInMetricProportion = bShowGeoInMetricProportion;
            standardFlags.IgnoreNonExistentAmplifiers = bIgnoreNonExistentAmplifiers;

        }

        public static SymbologyStandardFlags GetSymbologyStandardFlags(IDNMcOverlayManager overlayManager, DNESymbologyStandard symbologyStandard)
        {
            return dSymbologyStandardFlags[overlayManager].Find(x => x.SymbologyStandard == symbologyStandard);
        }

        public static DNESymbologyStandard GetSymbologyStandard(IDNMcOverlayManager overlayManager)
        {
             if (dSymbologyStandard.ContainsKey(overlayManager))
                 return dSymbologyStandard[overlayManager];
             else
                 return DNESymbologyStandard._ESS_NONE;
        }

        public static void SetSymbologyStandard(IDNMcOverlayManager overlayManager, DNESymbologyStandard symbologyStandard)
        {
            if (dSymbologyStandard.ContainsKey(overlayManager))
                dSymbologyStandard[overlayManager] = symbologyStandard;
            else
                dSymbologyStandard.Add(overlayManager, symbologyStandard);
        }

        public static void AddFormCreateSymbologyObjects(frmCreateSymbology formCreateSymbology, IDNMcObject mcObject)
        {
            if (dfrmCreateSymbologyObjects.ContainsKey(formCreateSymbology))
                dfrmCreateSymbologyObjects[formCreateSymbology] = mcObject;
            else
                dfrmCreateSymbologyObjects.Add(formCreateSymbology, mcObject);
        }

        public static void CloseFormCreateSymbologyObjects(IDNMcObject mcObject)
        {
            KeyValuePair<frmCreateSymbology, IDNMcObject> keyValuePair;
            if (dfrmCreateSymbologyObjects.ContainsValue(mcObject))
            {
                keyValuePair = dfrmCreateSymbologyObjects.First(x => x.Value == mcObject);
                frmCreateSymbology form  = (keyValuePair.Key as frmCreateSymbology);
                form.Close();
                dfrmCreateSymbologyObjects.Remove(form);
            }           
        }

        public static void AddTempAnchorPoints(IDNMcObject mcObject, IDNMcObject mcTempAnchorPoint)
        {
            if (dTempAnchorPoints.ContainsKey(mcObject))
            {
                dTempAnchorPoints[mcObject].Add(mcTempAnchorPoint);
            }
            else
            {
                dTempAnchorPoints.Add(mcObject, new List<IDNMcObject>() { mcTempAnchorPoint });
            }
        }

        public static void HandleRemoveObject(IDNMcObject mcObject)
        {
            RemoveTempAnchorPoints(mcObject);
            CloseFormCreateSymbologyObjects(mcObject);
        }


        public static bool IsExistAnchorPoints(IDNMcObject mcObject)
        {
            if (dTempAnchorPoints.ContainsKey(mcObject) && dTempAnchorPoints[mcObject] != null && dTempAnchorPoints[mcObject].Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void RemoveAllTempAnchorPoints()
        {
            try
            {
                foreach (KeyValuePair<IDNMcObject, List<IDNMcObject>> entry in dTempAnchorPoints)
                {
                    foreach (IDNMcObject obj in entry.Value)
                    {
                        obj.Remove();
                        obj.Dispose();
                    }
                }
                dTempAnchorPoints.Clear();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("IDNMcObject.Remove()", McEx);
            }
        }

        public static void RemoveTempAnchorPoints(IDNMcObject mcObject)
        {
            try
            {
                if (dTempAnchorPoints.ContainsKey(mcObject))
                {
                    foreach (IDNMcObject obj in dTempAnchorPoints[mcObject])
                    {
                        obj.Remove();
                        obj.Dispose();
                    }
                    dTempAnchorPoints.Remove(mcObject);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("IDNMcObject.Remove()", McEx);
            }
        }

        public static void ShowAnchorPoints(IDNMcObject symbologyObject)
        {
            if (symbologyObject != null)
            {
                IDNMcOverlayManager pOverlayManager = symbologyObject.GetOverlayManager();
                try
                {
                    DNSMcVector3D[] points;
                    DNSMcKeyFloatValue[] aGeometricAmplifiers;
                    symbologyObject.GetSymbologyAnchorPointsAndGeometricAmplifiers(out points, out aGeometricAmplifiers);

                    if (points != null)
                    {
                        for (int i = 0; i < points.Length; i++)
                        {
                            DNSMcVector3D[] locationPoints = new DNSMcVector3D[1];
                            locationPoints[0] = points[i];
                            IDNMcObject obj = DNMcObject.Create(GetTempOverlay(pOverlayManager), GetAnchorPointScheme(pOverlayManager), locationPoints);
                            obj.SetStringProperty(TEXT_PROPERTY_ID, new DNMcVariantString("P" + (i + 1).ToString(), true));

                            AddTempAnchorPoints(symbologyObject, obj);
                        }
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetSymbologyAnchorPointsAndGeometricAmplifiers", McEx);
                }
            }
        }

        public static IDNMcOverlay GetTempOverlay(IDNMcOverlayManager overlayManager)
        {
            IDNMcOverlay tempOverlay = null;
            if (overlayManager != null)
            {
                try
                {
                    if (dTempOverlays.ContainsKey(overlayManager))
                        tempOverlay = dTempOverlays[overlayManager];
                    else
                    {
                        tempOverlay = DNMcOverlay.Create(overlayManager, true);
                        dTempOverlays.Add(overlayManager, tempOverlay);
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("DNMcOverlay.Create", McEx);
                }
            }
            else
                MessageBox.Show("Missing overlay manager and this operation required overlay manager.");
            return tempOverlay;
        }

        public static IDNMcObjectScheme GetAnchorPointScheme(IDNMcOverlayManager pOverlayManager)
        {
            if (dAnchorPointScheme.ContainsKey(pOverlayManager))
                return dAnchorPointScheme[pOverlayManager];
            else
            {
                Font font = new Font(FontFamily.GenericSansSerif, 12);
                DNSMcLogFont logFont = new DNSMcLogFont();
                font.ToLogFont(logFont);

                IDNMcLogFont DefaultFont = DNMcLogFont.Create(new DNMcVariantLogFont(logFont, false));

                IDNMcTextItem textItem = DNMcTextItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN | DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN, DNEMcPointCoordSystem._EPCS_SCREEN, DefaultFont);
                IDNMcObjectScheme mcObjectScheme = DNMcObjectScheme.Create(pOverlayManager, textItem, DNEMcPointCoordSystem._EPCS_WORLD);

                // textItem.SetText(new DNMcVariantString("P" + (i + 1).ToString(), true), 1, ObjectPropertiesBase.TextIsUnicode);
                textItem.SetText(new DNMcVariantString("", true), TEXT_PROPERTY_ID, 0);
                textItem.SetTextColor(new DNSMcBColor(255, 0, 0, 255), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                textItem.SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                mcObjectScheme.SetName("Anchor Point");

                dAnchorPointScheme.Add(pOverlayManager, mcObjectScheme);
                return mcObjectScheme;
            }
        }
    }
}
