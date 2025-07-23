using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers;
using MapCore.Common;
using System.Linq;
using MCTester.General_Forms;
using MCTester.Managers.MapWorld;
using MCTester.ObjectWorld.Scan;

namespace MCTester.GUI.Forms
{
    public partial class ScanItemsFoundForm : Form, IGetScanExtendedDataCallback
    {
        private DNSTargetFound[] mFoundItems;
        private DNSMcScanGeometry mScanGeometry;
        private IDNMcMapViewport mMapViewport = null;
        private System.ComponentModel.BindingList<ScanTargetFound> m_lBinding = new System.ComponentModel.BindingList<ScanTargetFound>();
        private List<DNSObjectColor> mVector3DExtrusionOldColors = new List<DNSObjectColor>();
        private List<DNSObjectExtrusionHeight> mVector3DExtrusionOldHeights = new List<DNSObjectExtrusionHeight>();
        private List<IDNMcObject> mCurrentPolygonContours = new List<IDNMcObject>();
        private bool bTimerSet = false;
        private ColorDialog m_ColorDialog;

        static Color mColor;
        static Color mVector3DExtrusionContoursColor;
        static byte mAlpha;
        static bool mIsChangeAlpha;
        static DNSMcBColor mMcColor;

        static float mHeight = -1;

        static byte mVector3DExtrusionContoursAlpha;
        static bool mVector3DExtrusionContoursIsChangeAlpha;
        static DNSMcBColor mMcVector3DExtrusionContoursColor;
        private static List<IDNMcStaticObjectsMapLayer> m_lstColorsStaticMapLayer = new List<IDNMcStaticObjectsMapLayer>();
        private static List<IDNMcVector3DExtrusionMapLayer> m_lstHeightsStaticMapLayer = new List<IDNMcVector3DExtrusionMapLayer>();
        private static List<IDNMcObject> mNotRemovedPolygonContours = new List<IDNMcObject>();

        byte alphaInitValue = 180;
        byte mVector3DExtrusionContoursAlphaInitValue = 255;
       // uint mBitCount = 32;
        List<string> m_lstVectoryFields = new List<string>();
        List<uint> m_lstVectoryFieldIDs = new List<uint>();
        static string m_SelectedVectorField = "";
        private bool m_IsLoadVectorField = false;
        private string strNone = "None";


        public ScanItemsFoundForm(DNSTargetFound[] foundItems, DNSMcScanGeometry scanGeometry, IDNMcMapViewport mapViewport)
        {
            InitializeComponent();
            m_ColorDialog = new ColorDialog();
            m_ColorDialog.FullOpen = true;

            mFoundItems = foundItems;
            mScanGeometry = scanGeometry;
            mMapViewport = mapViewport;

            if (mHeight != -1)
                ntbHeight.SetFloat(mHeight);
           
            bool isExistStaticObjectsMapLayer = false;
            bool isExistVector3DExtrusionMapLayer = false;
           // List<string> vectoryFields = new List<string>();
            string pstrName;
            DNEFieldType peFieldType;

            cbVectorFields.Items.Add(strNone);
           

            m_IsLoadVectorField = true;
            List<IDNMcVectorMapLayer> lstVectorMapLayers = new List<IDNMcVectorMapLayer>();
            if (mFoundItems != null)
            {
                int index = 0;
                foreach (DNSTargetFound target in mFoundItems)
                {
                    if (target.eTargetType == DNEIntersectionTargetType._EITT_STATIC_OBJECTS_LAYER )
                    {
                        isExistStaticObjectsMapLayer = true;
                        mVector3DExtrusionOldColors.Add(
                            new DNSObjectColor(target.uTargetID,
                                ((IDNMcStaticObjectsMapLayer)target.pTerrainLayer).GetObjectColor(target.uTargetID)));

                        if (target.pTerrainLayer is IDNMcVector3DExtrusionMapLayer)
                        {
                            isExistVector3DExtrusionMapLayer = true;
                            DNSObjectExtrusionHeight objectExtrusionHeight = new DNSObjectExtrusionHeight();
                            objectExtrusionHeight.uObjectID = target.uTargetID;
                            objectExtrusionHeight.fHeight = ((IDNMcVector3DExtrusionMapLayer)target.pTerrainLayer).GetObjectExtrusionHeight(target.uTargetID);
                            mVector3DExtrusionOldHeights.Add(objectExtrusionHeight);
                        }
                    }
                    else if (target.pTerrainLayer is IDNMcVectorMapLayer)
                    {
                        
                        if(lstVectorMapLayers == null)
                            lstVectorMapLayers = new List<IDNMcVectorMapLayer>();
                       
                        try
                        {
                            IDNMcVectorMapLayer vectory = (IDNMcVectorMapLayer)target.pTerrainLayer;
                            if (!lstVectorMapLayers.Contains(vectory))
                                lstVectorMapLayers.Add(vectory);

                            uint numFields = vectory.GetNumFields();
                            if (numFields > 0)
                            {
                                for (uint i = 0; i < numFields; i++)
                                {
                                    vectory.GetFieldData(i, out pstrName, out peFieldType);
                                    if (!m_lstVectoryFields.Contains(pstrName))
                                    {
                                        m_lstVectoryFieldIDs.Add(i+1);
                                        m_lstVectoryFields.Add(pstrName);
                                        cbVectorFields.Items.Add(pstrName);

                                        if (pstrName.ToLower() == m_SelectedVectorField.ToLower())
                                            cbVectorFields.SelectedIndex = (int)i+1;
                                    }
                                }
                            }
                        }
                        catch (MapCoreException) { }
                    }
                    
                    m_lBinding.Add(new ScanTargetFound(target, GetBitCount(target.pTerrainLayer), index++));
                }
            }

            if(lstVectorMapLayers != null && lstVectorMapLayers.Count > 0)
            {
                foreach(IDNMcVectorMapLayer vectory in lstVectorMapLayers)
                {
                    try
                    {
                        uint numFields = vectory.GetNumFields();
                        if (numFields > 0)
                        {
                            for (uint i = 0; i < numFields; i++)
                            {
                                vectory.GetFieldData(i, out pstrName, out peFieldType);
                                if (!m_lstVectoryFields.Contains(pstrName))
                                {
                                    m_lstVectoryFieldIDs.Add(i + 1);
                                    m_lstVectoryFields.Add(pstrName);
                                    cbVectorFields.Items.Add(pstrName);

                                    if (pstrName.ToLower() == m_SelectedVectorField.ToLower())
                                        cbVectorFields.SelectedIndex = (int)i + 1;
                                }
                            }
                        }
                    }
                    catch (MapCoreException) { }
                }
            }
            m_IsLoadVectorField = false;

            if (cbVectorFields.SelectedIndex > 0)
                VectorFieldSet();
            else if (m_SelectedVectorField == "" || m_SelectedVectorField == strNone || !m_lstVectoryFields.Contains(m_SelectedVectorField))
            {
                cbVectorFields.SelectedIndex = 0;
            }

            gbVectoryFields.Enabled = cbVectorFields.Items.Count > 1 ;  // exist values from metadata

            if (!tsbSetColor.Enabled && isExistStaticObjectsMapLayer)
            {
                tsbSetColor.Enabled = true;
                tsbRemoveColor.Enabled = false;
                tsbBlink.Enabled = false;
                chxRemoveOnClosure.Enabled = true;
                tsbRemoveAll.Enabled = false;
            }

            groupBox2.Enabled = isExistVector3DExtrusionMapLayer;

            dgvFoundItemsData.DataSource = m_lBinding;

            if (mColor.IsEmpty)
            {
                mColor = Color.Red;
            }
            btnChooseColor.BackColor = mColor;

            if (!mIsChangeAlpha)
            {
                mAlpha = alphaInitValue;
            }
            nudAlpha.Value = mAlpha;

            mMcColor = new DNSMcBColor(mColor.R, mColor.G, mColor.B, mAlpha);

            if (mVector3DExtrusionContoursColor.IsEmpty)
            {
                mVector3DExtrusionContoursColor = Color.Black;
            }
            btnChooseStaticObjectsContoursColor.BackColor = mVector3DExtrusionContoursColor;

            if (!mVector3DExtrusionContoursIsChangeAlpha)
            {
                mVector3DExtrusionContoursAlpha = mVector3DExtrusionContoursAlphaInitValue;
            }
            nudChooseStaticObjectsContoursAlpha.Value = mVector3DExtrusionContoursAlpha;

            mMcVector3DExtrusionContoursColor = new DNSMcBColor(
                mVector3DExtrusionContoursColor.R,
                mVector3DExtrusionContoursColor.G,
                mVector3DExtrusionContoursColor.B,
                mVector3DExtrusionContoursAlpha);

            MCTSScanFormParams scanParams = btnScanForm.ScanFormParams;
            if ((scanParams.ScanSQParams == null) || ((scanParams.ScanSQParams != null) && (btnScanForm.ScanFormParams.ScanSQParams.bAddStaticObjectContours == false)))
                gbStaticObjectsContours.Enabled = false;
        }

        private Dictionary<IDNMcMapLayer, uint> m_LayerBitCountDic = new Dictionary<IDNMcMapLayer, uint>();

        public uint GetBitCount(IDNMcMapLayer pTerrainLayer)
        {
            uint bitCount = 32;
            if (pTerrainLayer != null)
            {
                if (m_LayerBitCountDic.ContainsKey(pTerrainLayer))
                    bitCount = m_LayerBitCountDic[pTerrainLayer];
                else
                {
                    if (pTerrainLayer is IDNMcVector3DExtrusionMapLayer)
                    {
                        bitCount = ((IDNMcVector3DExtrusionMapLayer)pTerrainLayer).GetObjectIDBitCount();
                    }
                    else if (pTerrainLayer is IDNMcRaw3DModelMapLayer)
                    {
                        bitCount = 128;
                    }
                    else if (pTerrainLayer is IDNMcVectorMapLayer)
                    {
                        bitCount = 64;
                    }
                    m_LayerBitCountDic.Add(pTerrainLayer, bitCount);
                }
            }
            return bitCount;
        }

        private void DrawPolygon(DNSMcVector3D[] points)
        {
            try
            {
                IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;
                DNSMcVector3D[] locationPoints = new DNSMcVector3D[0];

                DNEItemSubTypeFlags subTypeFlags = DNEItemSubTypeFlags._EISTF_SCREEN | DNEItemSubTypeFlags._EISTF_ACCURATE_3D_SCREEN_WIDTH;

                mMcVector3DExtrusionContoursColor = new DNSMcBColor(
                   mVector3DExtrusionContoursColor.R,
                   mVector3DExtrusionContoursColor.G,
                   mVector3DExtrusionContoursColor.B,
                   mVector3DExtrusionContoursAlpha);

                IDNMcObjectSchemeItem ObjSchemeItem = DNMcPolygonItem.Create(subTypeFlags,
                                                                                DNELineStyle._ELS_SOLID,
                                                                                mMcVector3DExtrusionContoursColor,
                                                                                3f,
                                                                                null,
                                                                                new DNSMcFVector2D(0, -1),
                                                                                1f,
                                                                                DNEFillStyle._EFS_NONE);
                IDNMcObject obj = DNMcObject.Create(activeOverlay,
                                                    ObjSchemeItem,
                                                    DNEMcPointCoordSystem._EPCS_WORLD,
                                                    points,
                                                    false);

                mCurrentPolygonContours.Add(obj);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
            }
        }

        private void tsbSetColor_Click(object sender, EventArgs e)
        {
            // set static vars
            mAlpha = (byte)nudAlpha.Value;
            if (mAlpha != alphaInitValue)
                mIsChangeAlpha = true;

            Dictionary<IDNMcStaticObjectsMapLayer, DNSMcBColor> layerColors = new Dictionary<IDNMcStaticObjectsMapLayer, DNSMcBColor>();
            List<DNSMcBColor> colors = new List<DNSMcBColor>();
            Dictionary<IDNMcStaticObjectsMapLayer, List<DNSObjectColor>> layerObjsColors =
                new Dictionary<IDNMcStaticObjectsMapLayer, List<DNSObjectColor>>();

            try
            {
                int index = 0;
                mMcColor = new DNSMcBColor(mColor.R, mColor.G, mColor.B, mAlpha);
                RemoveObjectStaticContours(mCurrentPolygonContours);
                foreach (DNSTargetFound itemFound in mFoundItems)
                {
                    if (itemFound.eTargetType == DNEIntersectionTargetType._EITT_STATIC_OBJECTS_LAYER )
                    {
                        IDNMcStaticObjectsMapLayer staticObjectsMapLayer = itemFound.pTerrainLayer as IDNMcStaticObjectsMapLayer;
                        if (!m_lstColorsStaticMapLayer.Contains(staticObjectsMapLayer))
                            m_lstColorsStaticMapLayer.Add(staticObjectsMapLayer);

                        if (!itemFound.uTargetID.IsEmpty())
                        {
                            IDNMcStaticObjectsMapLayer layer = ((IDNMcStaticObjectsMapLayer)itemFound.pTerrainLayer);
                            if (!layerColors.ContainsKey(layer))
                            {
                                layerColors.Add(layer, mMcColor);
                                layerObjsColors.Add(layer, new List<DNSObjectColor>());
                            }

                            if (dgvFoundItemsData.SelectedRows.Count == 0)
                            {
                                DNSObjectColor objColor = new DNSObjectColor(itemFound.uTargetID, layerColors[layer]);
                                layerObjsColors[layer].Add(objColor);
                            }
                            else
                            {
                                for (int i = 0; i < dgvFoundItemsData.SelectedRows.Count; i++)
                                {
                                    if (IsExistIndexRowInSelectedRows(index))
                                    {
                                        DNSObjectColor objColor = new DNSObjectColor(itemFound.uTargetID, layerColors[layer]);
                                        layerObjsColors[layer].Add(objColor);
                                    }
                                }
                            }
                        }
                    }
                    index++;

                    // Draw Static Objects Contours
                    if (itemFound.aStaticObjectContours != null)
                    {
                        foreach (DNSStaticObjectContour sVector3DExtrusionContours in itemFound.aStaticObjectContours)
                        {
                            DrawPolygon(sVector3DExtrusionContours.aPoints);
                        }
                    }
                }
                foreach (IDNMcStaticObjectsMapLayer layer in layerColors.Keys)
                {
                    layer.SetObjectsColors(layerObjsColors[layer].ToArray());
                }

                UpdateColorsInGrid();
                dgvFoundItemsData.Refresh();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Set/GetObjectColor", McEx);
            }
            //tsbSetColor.Enabled = false;
            tsbRemoveColor.Enabled = true;
            tsbBlink.Enabled = true;
            tsbRemoveAll.Enabled = true;
        }

        private void tsbRemoveColor_Click(object sender, EventArgs e)
        {
            int objectColorIndex = 0;
            int index = 0;
            Dictionary<IDNMcMapLayer, List<DNSTargetFound>> layers =
                            new Dictionary<IDNMcMapLayer, List<DNSTargetFound>>();
            Dictionary<IDNMcMapLayer, List<DNSObjectColor>> oldColors =
                            new Dictionary<IDNMcMapLayer, List<DNSObjectColor>>();

            if (mFoundItems != null)
            {
                foreach (var itemFound in mFoundItems)
                {
                    if (itemFound.eTargetType == DNEIntersectionTargetType._EITT_STATIC_OBJECTS_LAYER &&
                        itemFound.pTerrainLayer is IDNMcStaticObjectsMapLayer &&
                        !itemFound.uTargetID.IsEmpty())
                    {
                        if (!layers.ContainsKey(itemFound.pTerrainLayer))
                        {
                            layers.Add(itemFound.pTerrainLayer, new List<DNSTargetFound>());
                            oldColors.Add(itemFound.pTerrainLayer, new List<DNSObjectColor>());
                        }

                        if (dgvFoundItemsData.SelectedRows.Count == 0 || sender == null)  // if came from closing form
                        {
                            layers[itemFound.pTerrainLayer].Add(itemFound);
                            oldColors[itemFound.pTerrainLayer].Add(mVector3DExtrusionOldColors[objectColorIndex]);
                        }
                        else
                        {
                            for (int i = 0; i < dgvFoundItemsData.SelectedRows.Count; i++)
                            {
                                if (IsExistIndexRowInSelectedRows(index))
                                {
                                    layers[itemFound.pTerrainLayer].Add(itemFound);
                                    oldColors[itemFound.pTerrainLayer].Add(mVector3DExtrusionOldColors[objectColorIndex]);
                                }
                            }
                        }
                        objectColorIndex++;
                    }
                    index++;
                }
            }
            try
            {
                if (layers != null)
                {
                    foreach (IDNMcMapLayer layer in layers.Keys)
                    {
                        ((IDNMcStaticObjectsMapLayer)layer).SetObjectsColors(oldColors[layer].ToArray());
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SetObjectColor", McEx);
            }

            tsbSetColor.Enabled = true;
            tsbRemoveColor.Enabled = false;
            tsbBlink.Enabled = false;

            RemoveObjectStaticContours(mCurrentPolygonContours);

            if (sender != null)
                UpdateColorsInGrid();
        }

        private void RemoveObjectStaticContours(List<IDNMcObject> lstObjects)
        {
            try
            {
                foreach (IDNMcObject obj in lstObjects)
                {
                    obj.Remove();
                }
                lstObjects.Clear();
            }
            catch (MapCoreException mcEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("IDNMcObject.Remove", mcEx);
            }

        }

        private void ScanItemsFoundForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopBlink();
            if (chxRemoveOnClosure.Checked)
            {
                tsbRemoveColor_Click(null, null);
            }
            if (chxRemoveHeightsOnClosure.Checked)
            {
                btnRemoveHeight_Click(null, null);
            }
            else
            {
                mNotRemovedPolygonContours.AddRange(mCurrentPolygonContours);
            }
        }

        private void tsbBlink_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                bTimerSet = true;
                timer1.Start();
            }
            else
            {
                StopBlink();
            }
        }

        private void StopBlink()
        {
            timer1.Stop();
            tsbSetColor.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (bTimerSet)
            {
                tsbSetColor_Click(sender, e);
            }
            else
            {
                tsbRemoveColor_Click(sender, e);

            }

            bTimerSet = !bTimerSet;
        }

        private void tsbRemoveAll_Click(object sender, EventArgs e)
        {
            StopBlink();
            try
            {
                foreach (IDNMcStaticObjectsMapLayer layer in m_lstColorsStaticMapLayer)
                {
                    layer.RemoveAllObjectsColors();
                    /*DNSObjectColor[] colors = layer.GetObjectsColors();
                    if (colors != null)
                    {
                        for (int i = 0; i < colors.Length; i++)
                        {
                            colors[i].Color = DNSMcBColor.bcWhiteOpaque;
                        }

                        layer.SetObjectsColors(colors);
                    }*/
                }
            }
            catch (MapCoreException mcEx)
            {
                Utilities.ShowErrorMessage("IDNMcStaticObjectsMapLayer.RemoveAllObjectsColors", mcEx);
            }

            RemoveObjectStaticContours(mNotRemovedPolygonContours);
            RemoveObjectStaticContours(mCurrentPolygonContours);
            UpdateColorsInGrid(true);
            m_lstColorsStaticMapLayer.Clear();
            tsbSetColor.Enabled = true;
            tsbRemoveColor.Enabled = false;
            tsbBlink.Enabled = false;
            tsbRemoveAll.Enabled = false;
        }

        private void ScanItemsFoundForm_Load(object sender, EventArgs e)
        {
            tsbSetColor.PerformClick();
        }

        private void btnChooseColor_Click(object sender, EventArgs e)
        {
            m_ColorDialog.Color = mColor;
            if (m_ColorDialog.ShowDialog() == DialogResult.OK)
            {
                mColor = m_ColorDialog.Color;
                btnChooseColor.BackColor = mColor;
                mMcColor = new DNSMcBColor(mColor.R, mColor.G, mColor.B, (byte)mAlpha);
            }
        }

        private void nudAlpha_ValueChanged(object sender, EventArgs e)
        {
            mAlpha = (byte)nudAlpha.Value;
            mMcColor = new DNSMcBColor(mColor.R, mColor.G, mColor.B, mAlpha);
            mIsChangeAlpha = true;
        }

        private void btnChooseVector3DExtrusionContoursColor_Click(object sender, EventArgs e)
        {
            m_ColorDialog.Color = mVector3DExtrusionContoursColor;
            if (m_ColorDialog.ShowDialog() == DialogResult.OK)
            {
                mVector3DExtrusionContoursColor = m_ColorDialog.Color;
                btnChooseStaticObjectsContoursColor.BackColor = mVector3DExtrusionContoursColor;
                mMcVector3DExtrusionContoursColor = new DNSMcBColor(mVector3DExtrusionContoursColor.R, mVector3DExtrusionContoursColor.G, mVector3DExtrusionContoursColor.B, (byte)mVector3DExtrusionContoursAlpha);
            }
        }

        private void nudChooseStaticObjectsContoursAlpha_ValueChanged(object sender, EventArgs e)
        {
            mVector3DExtrusionContoursAlpha = (byte)nudChooseStaticObjectsContoursAlpha.Value;
            mMcVector3DExtrusionContoursColor = new DNSMcBColor(
                mVector3DExtrusionContoursColor.R,
                mVector3DExtrusionContoursColor.G,
                mVector3DExtrusionContoursColor.B,
                mVector3DExtrusionContoursAlpha);
            mVector3DExtrusionContoursIsChangeAlpha = true;
        }

        private bool IsExistIndexRowInSelectedRows(int index)
        {
            List<DataGridViewRow> lst = dgvFoundItemsData.SelectedRows
                                           .Cast<DataGridViewRow>()
                                           .Where(row => row.Index == index).ToList();
            return (lst != null && lst.Count == 1);
        }

        private void btnSetHeight_Click(object sender, EventArgs e)
        {
            mHeight = ntbHeight.GetFloat();
            Dictionary<IDNMcVector3DExtrusionMapLayer, List<DNSObjectExtrusionHeight>> layerObjsHeights =
               new Dictionary<IDNMcVector3DExtrusionMapLayer, List<DNSObjectExtrusionHeight>>();
            int index = 0;

            foreach (DNSTargetFound itemFound in mFoundItems)
            {
                if (itemFound.eTargetType == DNEIntersectionTargetType._EITT_STATIC_OBJECTS_LAYER &&
                    itemFound.pTerrainLayer is IDNMcVector3DExtrusionMapLayer)
                {
                    IDNMcVector3DExtrusionMapLayer vector3DExtrusionMapLayer = itemFound.pTerrainLayer as IDNMcVector3DExtrusionMapLayer;
                    if (!m_lstHeightsStaticMapLayer.Contains(vector3DExtrusionMapLayer))
                        m_lstHeightsStaticMapLayer.Add(vector3DExtrusionMapLayer);
                    if (!itemFound.uTargetID.IsEmpty())
                    {
                        if (!layerObjsHeights.ContainsKey(vector3DExtrusionMapLayer))
                        {
                            layerObjsHeights.Add(vector3DExtrusionMapLayer, new List<DNSObjectExtrusionHeight>());
                        }

                        if (dgvFoundItemsData.SelectedRows.Count == 0)
                        {
                            DNSObjectExtrusionHeight objectExtrusionHeight = new DNSObjectExtrusionHeight();
                            objectExtrusionHeight.uObjectID = itemFound.uTargetID;
                            objectExtrusionHeight.fHeight = mHeight;
                            layerObjsHeights[vector3DExtrusionMapLayer].Add(objectExtrusionHeight);
                        }
                        else
                        {
                            for (int i = 0; i < dgvFoundItemsData.SelectedRows.Count; i++)
                            {
                                if (IsExistIndexRowInSelectedRows(index))
                                {
                                    DNSObjectExtrusionHeight objectExtrusionHeight = new DNSObjectExtrusionHeight();
                                    objectExtrusionHeight.fHeight = mHeight;
                                    objectExtrusionHeight.uObjectID = itemFound.uTargetID;
                                    layerObjsHeights[vector3DExtrusionMapLayer].Add(objectExtrusionHeight);
                                }
                            }
                        }
                    }
                }
                index++;
            }
            try
            {
                foreach (IDNMcVector3DExtrusionMapLayer layer in layerObjsHeights.Keys)
                {
                    if (layerObjsHeights[layer].Count == 1)
                    {
                        DNSObjectExtrusionHeight objectExtrusionHeight = layerObjsHeights[layer][0];
                        layer.SetObjectExtrusionHeight(objectExtrusionHeight.uObjectID, objectExtrusionHeight.fHeight);
                    }
                    else
                    {
                        layer.SetObjectsExtrusionHeights(layerObjsHeights[layer].ToArray());
                    }
                }

                UpdateHeightsInGrid();
            }
            catch (MapCoreException mcEx)
            {
                Utilities.ShowErrorMessage("IDNMcVector3DExtrusionMapLayer.SetObjectExtrusionHeight", mcEx);
            }
        }

        private void UpdateHeightsInGrid(bool isRemovedAll = false)
        {
            try
            {
                for (int i = 0; i < m_lBinding.Count; i++)
                {
                    if (mFoundItems[i].eTargetType == DNEIntersectionTargetType._EITT_STATIC_OBJECTS_LAYER &&
                        mFoundItems[i].pTerrainLayer is IDNMcVector3DExtrusionMapLayer)
                    {
                        IDNMcVector3DExtrusionMapLayer Vector3DExtrusionMapLayer = ((IDNMcVector3DExtrusionMapLayer)mFoundItems[i].pTerrainLayer);
                        DNSObjectExtrusionHeight[] objectExtrusionHeightsArr = Vector3DExtrusionMapLayer.GetObjectsExtrusionHeights();
                        float height = float.MaxValue;

                        if (objectExtrusionHeightsArr != null)
                        {
                            List<DNSObjectExtrusionHeight> objectExtrusionHeightsLst = new List<DNSObjectExtrusionHeight>(objectExtrusionHeightsArr);
                            uint bitCount = GetBitCount(mFoundItems[i].pTerrainLayer);
                            string targetId = ScanTargetFound.GetTargetIdByBitCount(mFoundItems[i].uTargetID, bitCount, mFoundItems[i].pTerrainLayer);
                            DNSObjectExtrusionHeight objectExtrusionHeight = objectExtrusionHeightsLst.Find(x => ScanTargetFound.GetTargetIdByBitCount(x.uObjectID, bitCount, mFoundItems[i].pTerrainLayer) == targetId);
                            if (ScanTargetFound.GetTargetIdByBitCount(objectExtrusionHeight.uObjectID, bitCount, mFoundItems[i].pTerrainLayer) == targetId)
                                height = objectExtrusionHeight.fHeight;
                        }
                        m_lBinding[i].ItemHeight = (height == float.MaxValue ? "Original" : height.ToString());
                        if (isRemovedAll && i < mVector3DExtrusionOldHeights.Count)
                            mVector3DExtrusionOldHeights[i] = new DNSObjectExtrusionHeight(mFoundItems[i].uTargetID, height);
                    }
                }
                dgvFoundItemsData.Refresh();
            }
            catch (MapCoreException mcEx)
            {
                Utilities.ShowErrorMessage("IDNMcVector3DExtrusionMapLayer.GetObjectsExtrusionHeights", mcEx);
            }
        }

        private void UpdateColorsInGrid(bool isRemovedAll = false)
        {
            try
            {
                for (int i = 0; i < m_lBinding.Count; i++)
                {
                    if (mFoundItems[i].eTargetType == DNEIntersectionTargetType._EITT_STATIC_OBJECTS_LAYER &&
                        mFoundItems[i].pTerrainLayer is IDNMcStaticObjectsMapLayer)
                    {
                        IDNMcStaticObjectsMapLayer Vector3DExtrusionMapLayer = ((IDNMcStaticObjectsMapLayer)mFoundItems[i].pTerrainLayer);
                        DNSObjectColor[] objectsColorsArr = Vector3DExtrusionMapLayer.GetObjectsColors();
                        DNSObjectColor defualtObjectColor = new DNSObjectColor(mFoundItems[i].uTargetID, DNSMcBColor.bcWhiteOpaque);
                        DNSObjectColor objectColor = defualtObjectColor;
                        uint bitCount = GetBitCount(mFoundItems[i].pTerrainLayer);
                        string targetId = ScanTargetFound.GetTargetIdByBitCount(mFoundItems[i].uTargetID, bitCount, mFoundItems[i].pTerrainLayer);

                        if (objectsColorsArr != null)
                        {
                            List<DNSObjectColor> objectColorsLst = new List<DNSObjectColor>(objectsColorsArr);
                            objectColor = objectColorsLst.Find(x => ScanTargetFound.GetTargetIdByBitCount(x.uObjectID, bitCount, mFoundItems[i].pTerrainLayer) == targetId);

                            if (!(ScanTargetFound.GetTargetIdByBitCount(objectColor.uObjectID, bitCount, mFoundItems[i].pTerrainLayer) == targetId))
                                objectColor = defualtObjectColor;
                        }
                        m_lBinding[i].ItemColor = objectColor.Color.ToString();
                        if (isRemovedAll && i < mVector3DExtrusionOldColors.Count)
                        {
                            mVector3DExtrusionOldColors[i] = objectColor;
                        }
                    }
                }
                dgvFoundItemsData.Refresh();
            }
            catch (MapCoreException mcEx)
            {
                Utilities.ShowErrorMessage("IDNMcStaticObjectsMapLayer.GetObjectsColors", mcEx);
            }
        }


        private void btnRemoveHeight_Click(object sender, EventArgs e)
        {
            int objectHeightIndex = 0;
            int index = 0;
            Dictionary<IDNMcMapLayer, List<DNSObjectExtrusionHeight>> oldHeights =
                            new Dictionary<IDNMcMapLayer, List<DNSObjectExtrusionHeight>>();
            if (mFoundItems != null)
            {
                foreach (var itemFound in mFoundItems)
                {
                    if (itemFound.eTargetType == DNEIntersectionTargetType._EITT_STATIC_OBJECTS_LAYER &&
                        itemFound.pTerrainLayer is IDNMcVector3DExtrusionMapLayer &&
                        !itemFound.uTargetID.IsEmpty())
                    {
                        if (!oldHeights.ContainsKey(itemFound.pTerrainLayer))
                        {
                            oldHeights.Add(itemFound.pTerrainLayer, new List<DNSObjectExtrusionHeight>());
                        }

                        if (dgvFoundItemsData.SelectedRows.Count == 0 || sender == null)
                        {
                            oldHeights[itemFound.pTerrainLayer].Add(mVector3DExtrusionOldHeights[objectHeightIndex]);
                        }
                        else
                        {
                            for (int i = 0; i < dgvFoundItemsData.SelectedRows.Count; i++)
                            {
                                if (IsExistIndexRowInSelectedRows(index))
                                {
                                    oldHeights[itemFound.pTerrainLayer].Add(mVector3DExtrusionOldHeights[objectHeightIndex]);
                                }
                            }
                        }

                        objectHeightIndex++;

                    }
                    index++;
                }
            }
            try
            {
                if (oldHeights != null)
                {
                    foreach (IDNMcMapLayer layer in oldHeights.Keys)
                    {
                        ((IDNMcVector3DExtrusionMapLayer)layer).SetObjectsExtrusionHeights(oldHeights[layer].ToArray());
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                if (!(McEx.ErrorCode == DNEMcErrorCode.NOT_SUPPORTED_FOR_THIS_LAYER && sender == null))
                    Utilities.ShowErrorMessage("SetObjectsExtrusionHeights", McEx);
            }
            if (sender != null)
                UpdateHeightsInGrid();
        }

        private void btnRemoveAllHeights_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (IDNMcVector3DExtrusionMapLayer layer in m_lstHeightsStaticMapLayer)
                {
                    layer.RemoveAllObjectsExtrusionHeights();
                }
            }
            catch (MapCoreException mcEx)
            {
                Utilities.ShowErrorMessage("IDNMcVector3DExtrusionMapLayer.RemoveAllObjectsExtrusionHeights", mcEx);
            }
            UpdateHeightsInGrid(true);
            m_lstHeightsStaticMapLayer.Clear();
        }

        public void GetScanExtendedData(DNSVectorItemFound[] VectorItems, DNSMcVector3D[] unifiedVectorItemsPoints, DNSTargetFound itemFound, IDNMcOverlay overlay, ScanTargetFound scanTargetFound)
        {
            ScanItemsFoundFormDetails details = new ScanItemsFoundFormDetails(itemFound, scanTargetFound, VectorItems, unifiedVectorItemsPoints, mMapViewport);
            details.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            /*bool isCanceled = true;
            DataGridViewSelectedRowCollection rows = dgvFoundItemsData.SelectedRows;
            if (rows.Count > 0)
            {
                int index = rows[0].Index;
                DNSTargetFound itemFound = mFoundItems[index];

                if (itemFound.eTargetType == DNEIntersectionTargetType._EITT_VISIBLE_VECTOR_LAYER && itemFound.pTerrainLayer != null)
                {
                    isCanceled = false;
                }
            }
            e.Cancel = isCanceled;*/
        }

        private void dgvFoundItemsData_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            var dataGrid = (DataGridView)sender;
            if (e.Button == MouseButtons.Right && e.RowIndex != -1)
            {
                var row = dataGrid.Rows[e.RowIndex];
                dataGrid.CurrentCell = row.Cells[e.ColumnIndex == -1 ? 1 : e.ColumnIndex];
                row.Selected = true;
                dataGrid.Focus();
            }
        }

        private void dgvFoundItemsData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {



            //  dgvFoundItemsData[dgvFoundItemsData.ColumnCount - 1, e.RowIndex].Style.Font = new Font()

            //e.
        }

        private void dgvFoundItemsData_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {

        }

        private void dgvFoundItemsData_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dgvFoundItemsData[dgvFoundItemsData.ColumnCount - 1, e.RowIndex].Style = new DataGridViewCellStyle()
            {
                BackColor = Color.White,
                Font = new Font("Tahoma", 8F, FontStyle.Underline | FontStyle.Bold),
                ForeColor = Color.Blue,
                //SelectionBackColor = Color.Red,
                //SelectionForeColor = SystemColors.HighlightText
            };
        }

        private void dgvFoundItemsData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // DataGridViewSelectedRowCollection rows = dgvFoundItemsData.SelectedRows;
            if (e.RowIndex != -1 && e.ColumnIndex == dgvFoundItemsData.ColumnCount - 1)
            {
                int index = e.RowIndex;
                ScanTargetFound scanTargetFound = m_lBinding[index];
                DNSTargetFound itemFound = mFoundItems[index];

                if (itemFound.pTerrainLayer != null && itemFound.eTargetType == DNEIntersectionTargetType._EITT_VISIBLE_VECTOR_LAYER)
                {
                    DNSVectorItemFound[] VectorItems;
                    DNSMcVector3D[] unifiedVectorItemsPoints;
                    DNSTargetFound itemFoundCopy = itemFound;
                    try
                    {
                        MCTAsyncOperationCallback mctAsyncOperationCallback = null;
                        if (btnScanForm.ScanFormParams.IsAsync)
                        {
                            mctAsyncOperationCallback = MCTAsyncOperationCallback.GetInstance();

                            mctAsyncOperationCallback.scanExtendedDataCallback = this;
                            mctAsyncOperationCallback.itemFound = itemFound;
                            mctAsyncOperationCallback.scanTargetFound = scanTargetFound;
                        }

                        ((IDNMcVectorMapLayer)itemFound.pTerrainLayer).GetScanExtendedData(mScanGeometry, ref itemFoundCopy, mMapViewport,
                            out VectorItems, out unifiedVectorItemsPoints, mctAsyncOperationCallback);

                        if (!btnScanForm.ScanFormParams.IsAsync)
                            GetScanExtendedData(VectorItems, unifiedVectorItemsPoints, itemFound, null, scanTargetFound);

                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("GetScanExtendedData", McEx);
                    }
                }
                else if (itemFound.eTargetType == DNEIntersectionTargetType._EITT_OVERLAY_MANAGER_OBJECT)
                {
                    ScanItemsFoundFormDetailsObjects details = new ScanItemsFoundFormDetailsObjects(itemFound, scanTargetFound);
                    details.ShowDialog();
                }
                else if (itemFound.pTerrainLayer != null && itemFound.eTargetType == DNEIntersectionTargetType._EITT_STATIC_OBJECTS_LAYER && itemFound.pTerrainLayer.LayerType == DNELayerType._ELT_RAW_3D_MODEL)
                {
                    ScanItemsFoundFormHistoryBuildings details = new ScanItemsFoundFormHistoryBuildings(itemFound);
                    details.ShowDialog();
                }
            }
        }

        private void VectorFieldSet()
        {
            if (!m_IsLoadVectorField)
            { 
                int index = 0;
                foreach (DNSTargetFound itemFound in mFoundItems)
                {
                    if ((itemFound.eTargetType == DNEIntersectionTargetType._EITT_NON_VISIBLE_VECTOR_LAYER ||
                        itemFound.eTargetType == DNEIntersectionTargetType._EITT_VISIBLE_VECTOR_LAYER) &&
                        itemFound.pTerrainLayer is IDNMcVectorMapLayer)
                    {
                        IDNMcVectorMapLayer vectorMapLayer = itemFound.pTerrainLayer as IDNMcVectorMapLayer;

                        if (cbVectorFields.SelectedIndex > 0)
                        {
                            uint fieldid = (uint)cbVectorFields.SelectedIndex - 1;
                            try
                            {
                                MCTAsyncOperationCallback mctAsyncOperationCallback = null;
                                if (btnScanForm.ScanFormParams.IsAsync)
                                {
                                    mctAsyncOperationCallback = new MCTAsyncOperationCallback();

                                    mctAsyncOperationCallback.scanExtendedDataCallback = this;
                                    mctAsyncOperationCallback.itemFound = itemFound;
                                    mctAsyncOperationCallback.index = index;
                                }

                                string value = vectorMapLayer.GetVectorItemFieldValueAsWString(itemFound.uTargetID.u64Bit, fieldid, mctAsyncOperationCallback);
                                if (!btnScanForm.ScanFormParams.IsAsync)
                                {
                                    GetVectorItemFieldValueAsWString(value, index);
                                }
                            }
                            catch (MapCoreException McEx)
                            {
                                Utilities.ShowErrorMessage("GetVectorItemFieldValueAsWString", McEx);
                            }
                        }
                        else if (cbVectorFields.SelectedIndex == 0)
                        {
                            GetVectorItemFieldValueAsWString("", index);
                        }

                        index++;
                    }
                }
            }
        }

        public void GetVectorItemFieldValueAsWString(object pValue, int index)
        {
            m_lBinding[index].VectorField = pValue == null ? "" : pValue.ToString();
             dgvFoundItemsData.Refresh();
        }

        private void cbVectorFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbVectorFields.Items != null && cbVectorFields.Items.Count > 1)
                m_SelectedVectorField = cbVectorFields.SelectedItem.ToString();
            VectorFieldSet();
        }
    }

    public class ScanTargetFound
    {
        private string mNumRow;
        private string mIntersectionTargetType;
        private double mIntersection_Pt_X;
        private double mIntersection_Pt_Y;
        private double mIntersection_Pt_Z;
        private string mIntersectionCoordinateSystem;
        private string mTerrainID;
        private string mLayerID;
        private string mTargetID;
        private string mObjectID;
        private string mItemID;
        private string mHeight;
        private string mSubItemID;
        private string mPartFound;
        private string mItemType;
        private uint mPartIndex;
        private string mColor;
        private string mMore;
        private string mVectoryFields;

        private string CheckEmpty(uint id)
        {
            if (id == DNMcConstants._MC_EMPTY_ID)
                return "EMPTY";
            else
                return id.ToString();
        }

        public ScanTargetFound(DNSTargetFound targetFound, uint bitCount, int numRow)
        {
            No = numRow.ToString();
            Target_Type = targetFound.eTargetType.ToString();
            Intersection_Pt_X = targetFound.IntersectionPoint.x;
            Intersection_Pt_Y = targetFound.IntersectionPoint.y;
            Intersection_Pt_Z = targetFound.IntersectionPoint.z;
            IntersectionCoordinateSystem = targetFound.eIntersectionCoordSystem.ToString();
            try
            {
                switch (targetFound.eTargetType)
                {
                    case DNEIntersectionTargetType._EITT_ANY_TARGET:
                        break;
                    case DNEIntersectionTargetType._EITT_DTM_LAYER:
                        TerrainID = CheckEmpty(targetFound.pTerrain.GetID()) + " (" + Manager_MCNames.GetIdByObject(targetFound.pTerrain) + ")";
                        LayerID = CheckEmpty(targetFound.pTerrainLayer.GetID()) + " (" + Manager_MCNames.GetIdByObject(targetFound.pTerrainLayer) + ")";
                        break;
                    case DNEIntersectionTargetType._EITT_NONE:
                        break;
                    case DNEIntersectionTargetType._EITT_OVERLAY_MANAGER_OBJECT:
                        mMore = "More...";
                        ObjectID = CheckEmpty(targetFound.ObjectItemData.pObject.GetID()) + " (" + Manager_MCNames.GetIdByObject(targetFound.ObjectItemData.pObject) + ")";
                        if (targetFound.ObjectItemData.pItem != null)
                        {
                            ItemID = CheckEmpty(targetFound.ObjectItemData.pItem.GetID()) + " (" + Manager_MCNames.GetIdByObject(targetFound.ObjectItemData.pItem) + ")";
                        }
                        SubItemID = CheckEmpty(targetFound.ObjectItemData.uSubItemID);
                        PartFound = targetFound.ObjectItemData.ePartFound.ToString();
                        if (targetFound.ObjectItemData.pItem != null)
                        {
                            ItemType = targetFound.ObjectItemData.pItem.GetNodeType().ToString();
                        }
                        PartIndex = targetFound.ObjectItemData.uPartIndex;
                        break;
                    case DNEIntersectionTargetType._EITT_STATIC_OBJECTS_LAYER:
                        TerrainID = CheckEmpty(targetFound.pTerrain.GetID()) + " (" + Manager_MCNames.GetIdByObject(targetFound.pTerrain) + ")";
                        LayerID = CheckEmpty(targetFound.pTerrainLayer.GetID()) + " (" + Manager_MCNames.GetIdByObject(targetFound.pTerrainLayer) + ")";
                        TargetID = GetTargetIdByBitCount(targetFound.uTargetID, bitCount, targetFound.pTerrainLayer);
                        if (targetFound.pTerrainLayer is IDNMcStaticObjectsMapLayer)
                        {
                            ItemColor = ((IDNMcStaticObjectsMapLayer)targetFound.pTerrainLayer).GetObjectColor(targetFound.uTargetID).ToString();

                            if (targetFound.pTerrainLayer is IDNMcVector3DExtrusionMapLayer)
                            {
                                float height = ((IDNMcVector3DExtrusionMapLayer)targetFound.pTerrainLayer).GetObjectExtrusionHeight(targetFound.uTargetID);
                                ItemHeight = (height == float.MaxValue ? "Original" : height.ToString());
                            }
                            else if (targetFound.pTerrainLayer is IDNMcRaw3DModelMapLayer)
                            {
                                mMore = "More...";
                            }
                        }
                        
                        break;
                    case DNEIntersectionTargetType._EITT_VISIBLE_VECTOR_LAYER:
                        mMore = "More...";
                        LayerID = CheckEmpty(targetFound.pTerrainLayer.GetID()) + " (" + Manager_MCNames.GetIdByObject(targetFound.pTerrainLayer) + ")";
                        TargetID = GetTargetIdByBitCount(targetFound.uTargetID, bitCount, targetFound.pTerrainLayer);
                        SubItemID = CheckEmpty(targetFound.ObjectItemData.uSubItemID);
                        PartFound = targetFound.ObjectItemData.ePartFound.ToString();
                        PartIndex = targetFound.ObjectItemData.uPartIndex;
                        break;
                    case DNEIntersectionTargetType._EITT_NON_VISIBLE_VECTOR_LAYER:
                        LayerID = CheckEmpty(targetFound.pTerrainLayer.GetID()) + " (" + Manager_MCNames.GetIdByObject(targetFound.pTerrainLayer) + ")";
                        TargetID = GetTargetIdByBitCount(targetFound.uTargetID, bitCount, targetFound.pTerrainLayer);
                        break;
                }
            }
            catch (MapCoreException mcEx)
            {
                Utilities.ShowErrorMessage("Get item data", mcEx);
            }
        }

        public static string GetTargetIdByBitCount(DNSMcVariantID uTargetID, uint bitCount, IDNMcMapLayer pTerrainLayer)
        {
            string sTargetID = "";
            try
            {
                if (!uTargetID.IsEmpty())
                {
                    switch (bitCount)
                    {
                        case 32:
                            sTargetID = uTargetID.u32Bit.ToString(); break;
                        case 64:
                            if (pTerrainLayer != null && pTerrainLayer is IDNMcVectorMapLayer)
                            {
                                IDNMcVectorMapLayer mcVectorMapLayer = (IDNMcVectorMapLayer)pTerrainLayer;
                                DNMcNullableOut<String[]> pstrDataSourceNames = new DNMcNullableOut<string[]>();
                                mcVectorMapLayer.GetLayerDataSources(pstrDataSourceNames);
                                if (pstrDataSourceNames.Value != null && pstrDataSourceNames.Value.Length > 1)
                                {
                                    DNMcNullableOut<UInt32> puDataSourceID = new DNMcNullableOut<uint>();
                                    DNMcNullableOut<String> pstrDataSourceName = new DNMcNullableOut<string>();
                                    UInt32 puOriginalID = 0;
                                    mcVectorMapLayer.VectorItemIDToOriginalID(uTargetID.u64Bit, out puOriginalID, pstrDataSourceName, puDataSourceID);
                                    sTargetID = uTargetID.u64Bit.ToString() + " (" + puOriginalID + ", " + puDataSourceID.Value.ToString() + ", " + pstrDataSourceName.Value + ")";
                                }
                                else
                                    sTargetID = uTargetID.u64Bit.ToString();
                            }
                            else
                                sTargetID = uTargetID.u64Bit.ToString();
                            break;
                        case 128:
                            sTargetID = uTargetID.Get128bitAsUUIDString(); break;
                        default:
                            sTargetID = uTargetID.u32Bit.ToString(); break;
                    }
                }
            }
            catch (MapCoreException mcEx)
            {
                Utilities.ShowErrorMessage("VectorItemIDToOriginalID", mcEx);
            }
            return sTargetID;
        }

        public string No
        {
            get { return mNumRow; }
            set { mNumRow = value; }
        }

        public string Target_Type
        {
            get { return mIntersectionTargetType; }
            set { mIntersectionTargetType = value; }
        }

        public double Intersection_Pt_X
        {
            get { return mIntersection_Pt_X; }
            set { mIntersection_Pt_X = value; }
        }

        public double Intersection_Pt_Y
        {
            get { return mIntersection_Pt_Y; }
            set { mIntersection_Pt_Y = value; }
        }

        public double Intersection_Pt_Z
        {
            get { return mIntersection_Pt_Z; }
            set { mIntersection_Pt_Z = value; }
        }

        public string IntersectionCoordinateSystem
        {
            get { return mIntersectionCoordinateSystem; }
            set { mIntersectionCoordinateSystem = value; }
        }

        public string TerrainID
        {
            get { return mTerrainID; }
            set { mTerrainID = value; }
        }

        public string LayerID
        {
            get { return mLayerID; }
            set { mLayerID = value; }
        }

        public string TargetID 
        {
            get { return mTargetID; }
            set { mTargetID = value; }
        }

        public string ObjectID
        {
            get { return mObjectID; }
            set { mObjectID = value; }
        }

        public string ItemID
        {
            get { return mItemID; }
            set { mItemID = value; }
        }

        public string ItemHeight
        {
            get { return mHeight; }
            set { mHeight = value; }
        }

        public string SubItemID 
        {
            get { return mSubItemID; }
            set { mSubItemID = value; }
        }

        public string PartFound 
        {
            get { return mPartFound; }
            set { mPartFound = value; }
        }

        public string ItemType 
        {
            get { return mItemType; }
            set { mItemType = value; }
        }

        public uint PartIndex
        {
            get { return mPartIndex; }
            set { mPartIndex = value; }
        }

        public string ItemColor
        {
            get { return mColor; }
            set { mColor = value; }
        }

        public string VectorField
        {
            get { return mVectoryFields; }
            set { mVectoryFields = value; }
        }

        public string MoreDetails
        {
            get { return mMore; }
            set { mMore = value; }
        }

    }

 }