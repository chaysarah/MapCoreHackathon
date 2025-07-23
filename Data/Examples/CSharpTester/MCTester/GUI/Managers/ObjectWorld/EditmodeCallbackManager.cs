using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.Managers.ObjectWorld
{
    public delegate void ExitActionEventArgs(int nExitCode);
    public delegate void NewVertexEventArgs(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, DNSMcVector3D WorldVertex, DNSMcVector3D ScreenVertex, uint uVertexIndex, double dAngle);
    public delegate void PointDeletedEventArgs(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, DNSMcVector3D WorldVertex, DNSMcVector3D ScreenVertex, uint uVertexIndex);
    public delegate void PointNewPosEventArgs(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, DNSMcVector3D WorldVertex, DNSMcVector3D ScreenVertex, uint uVertexIndex, double dAngle, bool bDownOnHeadPoint);
    public delegate void ActiveIconChangedEventArgs(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, DNEPermission eIconPermission, uint uIconIndex);
    public delegate void InitItemResultsEventArgs(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, int nExitCode);
    public delegate void EditItemResultsEventArgs(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, int nExitCode);
    public delegate void DragMapResultsEventArgs(IDNMcMapViewport pViewport, DNSMcVector3D NewCenter);
    public delegate void RotateMapResultsEventArgs(IDNMcMapViewport pViewport, float fNewYaw, float fNewPitch);
    public delegate void DistanceDirectionMeasureResultsEventArgs(IDNMcMapViewport pViewport, DNSMcVector3D WorldVertex1, DNSMcVector3D WorldVertex2, double dDistance, double dAngle);
    public delegate void DynamicZoomResultsEventArgs(IDNMcMapViewport pViewport, float fNewScale, DNSMcVector3D NewCenter);
    public delegate void CalculateHeightResultsEventArgs(IDNMcMapViewport pViewport, double dHeight, DNSMcVector3D[] coords, int status);
    public delegate void CalculateVolumeResultsEventArgs(IDNMcMapViewport pViewport, double dVolume, DNSMcVector3D[] coords, int status);

    public class EditmodeCallbackManager : IDNEditModeCallback
    {
        public event ExitActionEventArgs ExitActionEvent;
        public event NewVertexEventArgs NewVertexEvent;
        public event PointDeletedEventArgs PointDeletedEvent;
        public event PointNewPosEventArgs PointNewPosEvent;
        public event ActiveIconChangedEventArgs ActiveIconChangedEvent;
        public event InitItemResultsEventArgs InitItemResultsEvent;
        public event EditItemResultsEventArgs EditItemResultsEvent;
        public event DragMapResultsEventArgs DragMapResultsEvent;
        public event RotateMapResultsEventArgs RotateMapResultsEvent;
        public event DistanceDirectionMeasureResultsEventArgs DistanceDirectionMeasureResultsEvent;
        public event DynamicZoomResultsEventArgs DynamicZoomResultsEvent;
        public event CalculateHeightResultsEventArgs CalculateHeightResultsEvent;
        public event CalculateVolumeResultsEventArgs CalculateVolumeResultsEvent;

        private List<IDNEditModeCallback> m_registeredComponents;

        private IDNMcEditMode m_editMode;

        public EditmodeCallbackManager(IDNMcEditMode editMode)
        {
            this.m_editMode = editMode;
            m_editMode.SetEventsCallback(this);

            m_registeredComponents = new List<IDNEditModeCallback>();
        }

        public void SetEventsCallback(IDNEditModeCallback callback)
        {
            if (!m_registeredComponents.Contains(callback))
                m_registeredComponents.Add(callback);
        }

        public void UnregisterEventsCallback(IDNEditModeCallback callback)
        {
            if (callback == null)
                m_registeredComponents.Clear();
            else
            {
                if (m_registeredComponents.Contains(callback))
                    m_registeredComponents.Remove(callback);
            }                
        }

        #region IDNEditModeCallback Members

        public void ExitAction(int nExitCode)
        {
            if (ExitActionEvent!=null)
            {
                ExitActionEvent(nExitCode);
            }
            
            foreach (IDNEditModeCallback callback in m_registeredComponents)
            {
                callback.ExitAction(nExitCode);
            }
        }

        public void NewVertex(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, DNSMcVector3D WorldVertex, DNSMcVector3D ScreenVertex, uint uVertexIndex, double dAngle)
        {
            if (NewVertexEvent!=null)
            {
                NewVertexEvent(pObject, pItem, WorldVertex, ScreenVertex, uVertexIndex, dAngle);
            }

            foreach (IDNEditModeCallback callback in m_registeredComponents)
            {
                callback.NewVertex(pObject, pItem, WorldVertex, ScreenVertex, uVertexIndex, dAngle);
            }
        }

        public void PointDeleted(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, DNSMcVector3D WorldVertex, DNSMcVector3D ScreenVertex, uint uVertexIndex)
        {
            if (PointDeletedEvent!=null)
            {
                PointDeletedEvent(pObject, pItem, WorldVertex, ScreenVertex, uVertexIndex);
            }

            foreach (IDNEditModeCallback callback in m_registeredComponents)
            {
                callback.PointDeleted(pObject, pItem, WorldVertex, ScreenVertex, uVertexIndex);
            }
        }

        public void PointNewPos(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, DNSMcVector3D WorldVertex, DNSMcVector3D ScreenVertex, uint uVertexIndex, double dAngle, bool bDownOnHeadPoint)
        {
            if (PointNewPosEvent != null)
            {
                PointNewPosEvent(pObject, pItem, WorldVertex, ScreenVertex, uVertexIndex, dAngle, bDownOnHeadPoint);
            }

            foreach (IDNEditModeCallback callback in m_registeredComponents)
            {
                callback.PointNewPos(pObject, pItem, WorldVertex, ScreenVertex, uVertexIndex, dAngle, bDownOnHeadPoint);
            }
        }

        public void ActiveIconChanged(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, DNEPermission eIconPermission, uint uIconIndex)
        {
            if (ActiveIconChangedEvent != null)
            {
                ActiveIconChangedEvent(pObject, pItem, eIconPermission, uIconIndex);
            }

            foreach (IDNEditModeCallback callback in m_registeredComponents)
            {
                callback.ActiveIconChanged(pObject, pItem, eIconPermission, uIconIndex);
            }
        }

        public void InitItemResults(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, int nExitCode)
        {
            if (InitItemResultsEvent!=null)
            {
                InitItemResultsEvent(pObject, pItem, nExitCode);
            }

            foreach (IDNEditModeCallback callback in m_registeredComponents)
            {
                callback.InitItemResults(pObject, pItem, nExitCode);
            }
        }

        public void EditItemResults(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, int nExitCode)
        {
            if (EditItemResultsEvent != null)
            {
                EditItemResultsEvent(pObject, pItem, nExitCode);
            }

            foreach (IDNEditModeCallback callback in m_registeredComponents)
            {
                callback.EditItemResults(pObject, pItem, nExitCode);
            }
        }

		public void DragMapResults(IDNMcMapViewport pViewport, DNSMcVector3D NewCenter)
        {
            if (DragMapResultsEvent != null)
            {
                DragMapResultsEvent(pViewport, NewCenter);
            }

            foreach (IDNEditModeCallback callback in m_registeredComponents)
            {
                callback.DragMapResults(pViewport, NewCenter);
            }
        }

		public void RotateMapResults(IDNMcMapViewport pViewport, float fNewYaw, float fNewPitch)
        {
            if (RotateMapResultsEvent != null)
            {
                RotateMapResultsEvent(pViewport, fNewYaw, fNewPitch);
            }

            foreach (IDNEditModeCallback callback in m_registeredComponents)
            {
                callback.RotateMapResults(pViewport, fNewYaw, fNewPitch);
            }
        }

		public void DistanceDirectionMeasureResults(IDNMcMapViewport pViewport, DNSMcVector3D WorldVertex1, DNSMcVector3D WorldVertex2, double dDistance, double dAngle)
        {
            if (DistanceDirectionMeasureResultsEvent != null)
            {
                DistanceDirectionMeasureResultsEvent(pViewport, WorldVertex1, WorldVertex2, dDistance, dAngle);
            }

            foreach (IDNEditModeCallback callback in m_registeredComponents)
            {
                callback.DistanceDirectionMeasureResults(pViewport, WorldVertex1, WorldVertex2, dDistance, dAngle);
            }
        }

		public void DynamicZoomResults(IDNMcMapViewport pViewport, float fNewScale, DNSMcVector3D NewCenter)
        {
            if (DynamicZoomResultsEvent != null)
            {
                DynamicZoomResultsEvent(pViewport, fNewScale, NewCenter);
            }

            foreach (IDNEditModeCallback callback in m_registeredComponents)
            {
                callback.DynamicZoomResults(pViewport, fNewScale, NewCenter);
            }
        }

		public void CalculateHeightResults(IDNMcMapViewport pViewport, double dHeight, DNSMcVector3D[] coords, int status)
        {
            if (CalculateHeightResultsEvent != null)
            {
                CalculateHeightResultsEvent(pViewport, dHeight, coords, status);
            }

            foreach (IDNEditModeCallback callback in m_registeredComponents)
            {
                callback.CalculateHeightResults(pViewport, dHeight, coords, status);
            }
        }

		public void CalculateVolumeResults(IDNMcMapViewport pViewport, double dVolume, DNSMcVector3D[] coords, int status)
        {
            if (CalculateVolumeResultsEvent != null)
            {
                CalculateVolumeResultsEvent(pViewport, dVolume, coords, status);
            }

            foreach (IDNEditModeCallback callback in m_registeredComponents)
            {
                callback.CalculateVolumeResults(pViewport, dVolume, coords, status);
            }
        }

        #endregion
    }
}
