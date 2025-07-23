using System;
using System.Collections.Generic;
using System.Text;
using UnmanagedWrapper;
using MapCore;
using System.Windows.Forms;
using MCTester.Managers.MapWorld;
using MCTester.ObjectWorld.Scan;
using MCTester.GUI.Forms;

namespace MCTester.ObjectWorld
{
    public class ScanGeometryBase: IGetScanExtendedDataCallback
    {
        private DNEMcPointCoordSystem m_PointCoordSystem;
        private IDNMcMapViewport m_CurrViewport;

        private bool m_IsGetScanExtendedDataAsync;

        public ScanGeometryBase()
        {
            m_CurrViewport = MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.Viewport;
            m_PointCoordSystem = DNEMcPointCoordSystem._EPCS_SCREEN;
        }

        public DNEMcPointCoordSystem VertexCoordinateSystem
        {
            get { return m_PointCoordSystem; }
        }

        public IDNMcMapViewport CurrViewport
        {
            get { return m_CurrViewport; }
        }

        public bool IsGetScanExtendedDataAsync
        {
            get { return m_IsGetScanExtendedDataAsync; }
            set { m_IsGetScanExtendedDataAsync = value; }
        }

        public void ShowScanResult(DNSMcScanGeometry scanGeometry, DNSTargetFound[] itemsFoundList)
        {
            MCTester.GUI.Forms.ScanItemsFoundForm itemsFound = new MCTester.GUI.Forms.ScanItemsFoundForm(itemsFoundList, scanGeometry, m_CurrViewport);
            IDNMcOverlayManager overlayManager = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlayManager;
            IDNMcOverlay overlay = null;
            if (overlayManager != null)
            {
                overlay = DNMcOverlay.Create(MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlayManager);

                if (itemsFoundList != null)
                {
                    foreach (DNSTargetFound itemFound in itemsFoundList)
                    {
                        if (itemFound.eTargetType == DNEIntersectionTargetType._EITT_VISIBLE_VECTOR_LAYER)
                        {
                            DNSVectorItemFound[] VectorItems;
                            DNSMcVector3D[] unifiedVectorItemsPoints;
                            DNSTargetFound itemFoundCopy = itemFound;
                            try
                            {
                                MCTAsyncOperationCallback mctAsyncOperationCallback = null;
                                if (IsGetScanExtendedDataAsync)
                                {
                                    mctAsyncOperationCallback = MCTAsyncOperationCallback.GetInstance();

                                    mctAsyncOperationCallback.scanExtendedDataCallback = this;
                                    mctAsyncOperationCallback.mcOverlay = overlay;
                                    mctAsyncOperationCallback.itemFound = itemFound;
                                }

                                ((IDNMcVectorMapLayer)itemFound.pTerrainLayer).GetScanExtendedData(scanGeometry, ref itemFoundCopy, m_CurrViewport,
                                    out VectorItems, out unifiedVectorItemsPoints, mctAsyncOperationCallback);

                                if (!IsGetScanExtendedDataAsync)
                                    GetScanExtendedData(VectorItems, unifiedVectorItemsPoints, itemFound, overlay, null);

                            }
                            catch (MapCoreException McEx)
                            {
                                MapCore.Common.Utilities.ShowErrorMessage("GetScanExtendedData", McEx);
                            }
                        }
                    }
                }
            }
            else
                MessageBox.Show("Missing Overlay Manager", "Get Scan Extended Data");
            itemsFound.ShowDialog();
            if(overlay != null)
                overlay.Remove();
        }


        public void GetScanExtendedData(DNSVectorItemFound[] VectorItems, DNSMcVector3D[] unifiedVectorItemsPoints, DNSTargetFound itemFound, IDNMcOverlay overlay, ScanTargetFound scanTargetFound)
        {
            byte colorDelta = 0;

            bool isNeedToDoConnectItems = true;

           if ( unifiedVectorItemsPoints != null && unifiedVectorItemsPoints.Length != 0)
            {              
                IDNMcSymbolicItem unifiedItem;

                if (unifiedVectorItemsPoints.Length > 1)
                {
                    bool bIfDefaultItemIsPolygon = false;
                    if (itemFound.ObjectItemData.pItem != null)
                    {
                        bIfDefaultItemIsPolygon = itemFound.ObjectItemData.pItem.GetNodeType() == DNEObjectSchemeNodeType._POLYGON_ITEM;
                        if (!bIfDefaultItemIsPolygon)  // check if default item (= first item in the first object location) is polygon 
                        { 
                            IDNMcObjectScheme mcObjectScheme = itemFound.ObjectItemData.pItem.GetScheme();
                            if (mcObjectScheme != null)
                            {
                                IDNMcObjectSchemeNode defaultItem = mcObjectScheme.GetEditModeDefaultItem();
                                if (defaultItem == null)
                                {
                                    IDNMcObjectLocation mcObjectLocation = mcObjectScheme.GetObjectLocation(0);
                                    if (mcObjectLocation != null)
                                    {
                                        IDNMcObjectSchemeNode[] mcObjectSchemeNodes = mcObjectLocation.GetChildren();
                                        if (mcObjectSchemeNodes != null && mcObjectSchemeNodes.Length > 0)
                                            defaultItem = mcObjectSchemeNodes[0];
                                    }
                                }

                                if (defaultItem != null)
                                {
                                    bIfDefaultItemIsPolygon = defaultItem.GetNodeType() == DNEObjectSchemeNodeType._POLYGON_ITEM;
                                }
                            }
                        }
                    }
                    if (bIfDefaultItemIsPolygon)
                    {
                        unifiedItem = DNMcPolygonItem.Create(
                                                  DNEItemSubTypeFlags._EISTF_SCREEN | DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN,
                                                  DNELineStyle._ELS_SOLID, new DNSMcBColor(0, 0, 255, 255), 2, null,
                                                  new DNSMcFVector2D(0, -1), 1, DNEFillStyle._EFS_SOLID, new DNSMcBColor(255, 255, 255, 80));

                        if (VectorItems != null &&new List<DNSVectorItemFound>(VectorItems).Exists(x => x.uVectorItemID == DNMcConstants.MC_EXTRA_CONTOUR_VECTOR_ITEM_ID)) // check if exist empty polygon - to declare them as sub items
                        {
                            isNeedToDoConnectItems = false;

                            DNSMcSubItemData[] mcSubItemData = new DNSMcSubItemData[VectorItems.Length];
                            mcSubItemData[0] = new DNSMcSubItemData();
                            mcSubItemData[0].uSubItemID = DNMcConstants._MC_EMPTY_ID;
                            mcSubItemData[0].nPointsStartIndex = 0;

                            for (int i = 1; i < VectorItems.Length; i++)
                            {
                                mcSubItemData[i] = new DNSMcSubItemData();
                                mcSubItemData[i].uSubItemID = DNMcConstants._MC_EXTRA_CONTOUR_SUB_ITEM_ID;
                                mcSubItemData[i].nPointsStartIndex = (int)VectorItems[i].uVectorItemFirstPointIndex;
                            }

                            unifiedItem.SetSubItemsData(new DNSArrayProperty<DNSMcSubItemData>(mcSubItemData), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        }
                    }
                    else
                    {
                        unifiedItem = DNMcLineItem.Create(
                                DNEItemSubTypeFlags._EISTF_SCREEN | DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN,
                                DNELineStyle._ELS_SOLID, new DNSMcBColor(0, 0, 255, 255), 2);
                    }
                }
                else
                {
                    unifiedItem = DNMcEllipseItem.Create(
                        DNEItemSubTypeFlags._EISTF_SCREEN | DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN,
                        DNEMcPointCoordSystem._EPCS_SCREEN, DNEItemGeometryType._EGT_GEOMETRIC_IN_VIEWPORT, 10, 10,
                        0, 360, 0, DNELineStyle._ELS_SOLID, new DNSMcBColor(0, 0, 255, 255), 2, null,
                        new DNSMcFVector2D(0, -1), 1, DNEFillStyle._EFS_NONE);
                }

                unifiedItem.SetDrawPriority(1, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false); 
                IDNMcObject obj = DNMcObject.Create(overlay, unifiedItem,
                    DNEMcPointCoordSystem._EPCS_WORLD, unifiedVectorItemsPoints, false);

                if (isNeedToDoConnectItems && VectorItems != null && VectorItems.Length != 0)
                {
                    for (int i = 0; i < VectorItems.Length; ++i)
                    {
                        IDNMcLineItem line = DNMcLineItem.Create(
                            DNEItemSubTypeFlags._EISTF_SCREEN | DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN,
                            DNELineStyle._ELS_SOLID,
                            new DNSMcBColor((byte)(255 - colorDelta), colorDelta, colorDelta, 255), 10);
                        colorDelta = (byte)(((uint)colorDelta + 64) % 256);
                        line.Connect(unifiedItem);
                        line.SetAttachPointType(0, DNEAttachPointType._EAPT_INDEX_POINTS, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID);
                        line.SetAttachPointIndex(0, (int)VectorItems[i].uVectorItemFirstPointIndex,
                            (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        line.SetNumAttachPoints(0,
                            (int)(VectorItems[i].uVectorItemLastPointIndex + 1 - VectorItems[i].uVectorItemFirstPointIndex),
                            (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    }
                }
            }
        }

        public void GetVectorItemFieldValueAsWString(object pValue, int index)
        {
            
        }
    }
}
