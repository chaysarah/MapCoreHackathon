package com.elbit.mapcore.mcandroidtester.managers;


import java.util.ArrayList;
import java.util.List;

import com.elbit.mapcore.Interfaces.General.IMcEditMode;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeItem;
import com.elbit.mapcore.Structs.SMcVector3D;

/**
 * Created by tc97803 on 28/09/2016.
 */
public class EditmodeCallbackManager implements IMcEditMode.ICallback {

    private List<IMcEditMode.ICallback> m_registeredComponents;

    private IMcEditMode m_editMode;

    public EditmodeCallbackManager(IMcEditMode editMode)
    {
        this.m_editMode = editMode;
        try {
            m_editMode.SetEventsCallback(this);
        } catch (Exception e) {
            e.printStackTrace();
        }

        m_registeredComponents = new ArrayList<IMcEditMode.ICallback>();
    }

    public void SetEventsCallback(IMcEditMode.ICallback callback)
    {
        if (!m_registeredComponents.contains(callback))
            m_registeredComponents.add(callback);
    }

    public void UnregisterEventsCallback(IMcEditMode.ICallback callback)
    {
        if (callback == null)
            m_registeredComponents.clear();
        else
        {
            if (m_registeredComponents.contains(callback))
                m_registeredComponents.remove(callback);
        }
    }


    @Override
    public void ExitAction(int nExitCode) {
        for (IMcEditMode.ICallback callback : m_registeredComponents)
        {
            callback.ExitAction(nExitCode);
        }
    }

    @Override
    public void NewVertex(IMcObject iMcObject, IMcObjectSchemeItem iMcObjectSchemeItem, SMcVector3D sMcVector3D, SMcVector3D sMcVector3D1, int i, double v) {
        for (IMcEditMode.ICallback callback : m_registeredComponents)
        {
            callback.NewVertex(iMcObject, iMcObjectSchemeItem, sMcVector3D, sMcVector3D1, i, v);
        }
    }

    @Override
    public void PointDeleted(IMcObject iMcObject, IMcObjectSchemeItem iMcObjectSchemeItem, SMcVector3D sMcVector3D, SMcVector3D sMcVector3D1, int i) {
        for (IMcEditMode.ICallback callback : m_registeredComponents)
        {
            callback.PointDeleted(iMcObject, iMcObjectSchemeItem, sMcVector3D, sMcVector3D1, i);
        }
    }

    @Override
    public void PointNewPos(IMcObject iMcObject, IMcObjectSchemeItem iMcObjectSchemeItem, SMcVector3D sMcVector3D, SMcVector3D sMcVector3D1, int i, double v, boolean b) {
        for (IMcEditMode.ICallback callback : m_registeredComponents)
        {
            callback.PointNewPos(iMcObject,  iMcObjectSchemeItem,  sMcVector3D,  sMcVector3D1,  i,  v,  b);
        }
    }

    @Override
    public void ActiveIconChanged(IMcObject iMcObject, IMcObjectSchemeItem iMcObjectSchemeItem, IMcEditMode.EPermission ePermission, int i) {
        for (IMcEditMode.ICallback callback : m_registeredComponents)
        {
            callback.ActiveIconChanged(iMcObject,  iMcObjectSchemeItem,  ePermission,  i);
        }
    }

    @Override
    public void InitItemResults(IMcObject iMcObject, IMcObjectSchemeItem iMcObjectSchemeItem, int i) {
        for (IMcEditMode.ICallback callback : m_registeredComponents)
        {
            callback.InitItemResults(iMcObject, iMcObjectSchemeItem, i);
        }
    }

    @Override
    public void EditItemResults(IMcObject iMcObject, IMcObjectSchemeItem iMcObjectSchemeItem, int i) {
        for (IMcEditMode.ICallback callback : m_registeredComponents)
        {
            callback.EditItemResults(iMcObject, iMcObjectSchemeItem, i);
        }
    }

    @Override
    public void DragMapResults(IMcMapViewport iMcMapViewport, SMcVector3D sMcVector3D) {
        for (IMcEditMode.ICallback callback : m_registeredComponents)
        {
            callback.DragMapResults(iMcMapViewport, sMcVector3D);
        }
    }

    @Override
    public void RotateMapResults(IMcMapViewport iMcMapViewport, float v, float v1) {
        for (IMcEditMode.ICallback callback : m_registeredComponents)
        {
            callback.RotateMapResults(iMcMapViewport, v, v1);
        }
    }

    @Override
    public void DistanceDirectionMeasureResults(IMcMapViewport iMcMapViewport, SMcVector3D sMcVector3D, SMcVector3D sMcVector3D1, double v, double v1) {
        for (IMcEditMode.ICallback callback : m_registeredComponents)
        {
            callback.DistanceDirectionMeasureResults(iMcMapViewport, sMcVector3D, sMcVector3D1, v, v1);
        }
    }

    @Override
    public void DynamicZoomResults(IMcMapViewport iMcMapViewport, float v, SMcVector3D sMcVector3D) {
        for (IMcEditMode.ICallback callback : m_registeredComponents)
        {
            callback.DynamicZoomResults(iMcMapViewport, v, sMcVector3D);
        }
    }

    @Override
    public void CalculateHeightResults(IMcMapViewport iMcMapViewport, double v, SMcVector3D[] sMcVector3Ds, int i) {
        for (IMcEditMode.ICallback callback : m_registeredComponents)
        {
            callback.CalculateHeightResults(iMcMapViewport, v, sMcVector3Ds, i);
        }
    }

    @Override
    public void CalculateVolumeResults(IMcMapViewport iMcMapViewport, double v, SMcVector3D[] sMcVector3Ds, int i) {
        for (IMcEditMode.ICallback callback : m_registeredComponents)
        {
            callback.CalculateVolumeResults(iMcMapViewport, v, sMcVector3Ds, i);
        }
    }


}
