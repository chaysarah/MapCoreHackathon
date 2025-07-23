using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;
using MCTester.GUI;
using MCTester.GUI.Map;
using MCTester.MapWorld.LoadingMapScheme;

namespace MCTester.MapWorld
{
    [Serializable]
    public class MCTMapViewport
    {
        private int m_ViewportId;
        private List<int> m_TerrainsIDs;
        private DNSCreateDataMV m_CreateData;
        private DNEMapType m_eMapType;
        private int m_DeviceID;
        private int m_ImageCalcID = -1;
        private int m_CoordSysID;
        private DNSMcVector3D m_CameraPosition;
        private bool m_ShowGeoInMetricProportion;
        private string m_DtmUsageAndPrecision;
        private float m_TerrainObjectBestResolution;
        private string m_Name;

        #region Public Properties
        public int ID
        {
            get { return m_ViewportId; }
            set { m_ViewportId = value; }
        }

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public DNEMapType MapType
        {
            get { return m_eMapType; }
            set { m_eMapType = value; }
        }
                
        public int DeviceID
        {
            get { return m_DeviceID; }
            set { m_DeviceID = value; }
        }

        public int CoordSysID
        {
            get { return m_CoordSysID; }
            set { m_CoordSysID = value; }
        }

        public DNSMcVector3D CameraPosition
        {
            get { return m_CameraPosition; }
            set { m_CameraPosition = value; }
        }
        
        public int ImageCalcID
        {
            get { return m_ImageCalcID; }
            set { m_ImageCalcID = value; }
        }       
                
        public bool ShowGeoInMetricProportion
        {
            get { return m_ShowGeoInMetricProportion; }
            set { m_ShowGeoInMetricProportion = value; }
        }

        public string DtmUsageAndPrecision
        {
            get { return m_DtmUsageAndPrecision; }
            set { m_DtmUsageAndPrecision = value; }
        }

        public float TerrainObjectBestResolution
        {
            get { return m_TerrainObjectBestResolution; }
            set { m_TerrainObjectBestResolution = value; }
        }

        public List<int> TerrainsIDs
        {
            get { return m_TerrainsIDs; }
            set { m_TerrainsIDs = value; }
        }
        #endregion
        
        public MCTMapViewport()
        {
            m_TerrainsIDs = new List<int>();
        }

        public void CreateViewportMono(out IDNMcMapViewport CreatedViewport,
                                        out IDNMcMapCamera CreatedCamera, 
                                        IDNMcMapTerrain[] Terrains,
                                        IDNMcImageCalc ImageCalc, 
                                        IDNMcMapDevice m_MapDevice, 
                                        IDNMcOverlayManager OM, 
                                        IDNMcGridCoordinateSystem coordSys, 
                                        bool IsWpfWindow,
                                        IDNMcDtmMapLayer[] apQuerySecondaryDtmLayers = null)
        {
            IDNMcMapViewport Viewport = null;
            IDNMcMapCamera Camera = null;
            
            MCTMapForm NewMapForm = new MCTMapForm(IsWpfWindow);
            
            m_CreateData = new DNSCreateDataMV(this.MapType);
            m_CreateData.hWnd = NewMapForm.MapPointer;
            m_CreateData.CoordinateSystem = coordSys;
            m_CreateData.uViewportID = (uint)this.ID;
            
            m_CreateData.pGrid = null;
            m_CreateData.bShowGeoInMetricProportion = this.ShowGeoInMetricProportion;
            
            if (ImageCalc != null)
                m_CreateData.pImageCalc = ImageCalc;

            m_CreateData.pDevice = m_MapDevice;                        
            m_CreateData.pOverlayManager = OM;
            m_CreateData.eDisplayingItemsAttachedToTerrain = DNEDisplayingItemsAttachedToTerrain._EDIATT_ON_TERRAIN_ONLY;
            m_CreateData.fTerrainResolutionFactor = Managers.Manager_MCGeneralDefinitions.m_TerrainResolutionFactor;

            if (m_CreateData.hWnd == IntPtr.Zero)
            {
                m_CreateData.uWidth = (uint)NewMapForm.Width;
                m_CreateData.uHeight = (uint)NewMapForm.Height;
            }
            DNMcMapViewport.Create(ref Viewport, ref Camera, m_CreateData, Terrains, apQuerySecondaryDtmLayers);

            CreatedViewport = Viewport;
            CreatedCamera = Camera;
            NewMapForm.Viewport = Viewport;
            NewMapForm.CreateEditMode(Viewport);
            NewMapForm.Show();

            MCTester.Managers.MapWorld.MCTMapFormManager.AddMapForm(NewMapForm);

            Managers.Manager_MCGeneralDefinitions.m_ShowGeoInMetricProportion = ShowGeoInMetricProportion;
           
        }
    }
}
