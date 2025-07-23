package com.elbit.mapcore.mcandroidtester.model;

import com.elbit.mapcore.Structs.SMcVector3D;
import com.elbit.mapcore.mcandroidtester.model.Automation.AMCTAutomationParams;

import java.io.File;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;

import com.elbit.mapcore.Interfaces.Calculations.GridCoordSystem.IMcGridCoordinateSystem;
import com.elbit.mapcore.Interfaces.Map.IMcMapCamera;
import com.elbit.mapcore.Interfaces.Map.IMcMapDevice;
import com.elbit.mapcore.Interfaces.Map.IMcMapTerrain;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;

/**
 * Created by tc99382 on 09/08/2016.
 */
public class AMCTViewPort {
    private static AMCTViewPort mCurViewPort;
    private IMcMapViewport.SCreateData mSCreateData;

    private IMcMapDevice mMapDevice;
    private IMcGridCoordinateSystem mGridCoordinateSystem;
    private List<IMcMapTerrain> mTerrainsArrList;
    private IMcMapViewport mViewport;
    private IMcOverlayManager mOverlayManager;
    private ViewportSpace mViewportSpace = ViewportSpace.FullScreen;
    private boolean mShowGeoInMetricProportion;
    private IMcMapCamera.EMapType mMapType;

    private boolean mIsAutomation = false;
    private AMCTAutomationParams mAutomationParams;
    private String mAutomationPrintViewportPath;
    private String mJsonFolderPath;
    private File mAutomationLogFile;
    private boolean mAutomationIsShowMsg;

    private boolean mIsSectionMap;
    private SMcVector3D[] mSectionMapPoints;

    public void setAutomationParams(AMCTAutomationParams automationParams) {
        mAutomationParams = automationParams;
    }

    public AMCTAutomationParams getAutomationParams()
    {
        return mAutomationParams;
    }

    public void setAutomationPrintViewportPath(String automationPrintViewportPath){
        mAutomationPrintViewportPath = automationPrintViewportPath;
    }

    public String getAutomationPrintViewportPath(){
        return mAutomationPrintViewportPath;
    }

    public void setJsonFolderPath(String jsonFolderPath) {
        mJsonFolderPath  = jsonFolderPath;
    }
    public String getJsonFolderPath()
    {
        return  mJsonFolderPath;
    }

    public void setAutomationLogFile(File automationLogFile) {
        mAutomationLogFile  = automationLogFile;
    }
    public File getAutomationLogFile()
    {
        return mAutomationLogFile;
    }

    public void setAutomationIsShowMsg(boolean automationIsShowMsg) {
        mAutomationIsShowMsg  = automationIsShowMsg;
    }
    public boolean getAutomationIsShowMsg()
    {
        return mAutomationIsShowMsg;
    }

    public enum ViewportSpace{
        FullScreen, Other;
    }

    public void setViewportSpace(ViewportSpace viewportSpace)
    {
        mViewportSpace = viewportSpace;
    }

    public ViewportSpace getViewportSpace()
    {
        return mViewportSpace;
    }

    public void resetCurViewPort() {
        mCurViewPort.removeAllTerrains();
        mIsAutomation = false;
        mCurViewPort = null;
    }

    public AMCTViewPort() {
	    mMapDevice = AMCTMapDevice.getInstance().getDevice();
		mTerrainsArrList = new ArrayList<>();
        mSCreateData = new IMcMapViewport.SCreateData(IMcMapCamera.EMapType.EMT_2D);
        mSCreateData.bFullScreen = false;
        mSCreateData.bShowGeoInMetricProportion = false;
        mSCreateData.hWnd = -1;
        mSCreateData.bExternalGLControl = false;
        mIsSectionMap = false;
        mViewportSpace = ViewportSpace.FullScreen;
    }

    public IMcMapTerrain[] getTerrainsAsArr() {
        IMcMapTerrain[] terrainArr = new IMcMapTerrain[mTerrainsArrList.size()];
        for (int i = 0; i < mTerrainsArrList.size(); i++) {
            terrainArr[i] = mTerrainsArrList.get(i);
        }
        return terrainArr;
    }

    public boolean containsTerrain(IMcMapTerrain terrain)
    {
        return mTerrainsArrList.contains(terrain);
    }


    public IMcGridCoordinateSystem getGridCoordinateSystem() {
        return mGridCoordinateSystem;
    }

    public void setGridCoordinateSystem(IMcGridCoordinateSystem mGridCoordinateSystem) {
        this.mGridCoordinateSystem = mGridCoordinateSystem;
    }

    public IMcMapDevice getMapDevice() {
        return mMapDevice;
    }

    public void setMapDevice(IMcMapDevice mMapDevice) {
        this.mMapDevice = mMapDevice;
    }

    public IMcMapViewport getViewport() {
        return mViewport;
    }

    public void setViewport(IMcMapViewport mViewport) {
        this.mViewport = mViewport;
    }

    public IMcOverlayManager getmOverlayManager() {
        return mOverlayManager;
    }

    public void setOverlayManager(IMcOverlayManager mOverlayManager) {
        this.mOverlayManager = mOverlayManager;
    }

    public void removeAllTerrains()
    {
        mTerrainsArrList.clear();
    }

    public void addTerrainToList(IMcMapTerrain terrain)
    {
        mTerrainsArrList.add(terrain);
    }

    public void addTerrainToList(IMcMapTerrain[] terrain)
    {
        mTerrainsArrList.addAll(Arrays.asList(terrain));
    }

    public void setIsSectionMap(boolean isSectionMap)
    {
        mIsSectionMap = isSectionMap;
    }

    public boolean getIsSectionMap()
    {
        return mIsSectionMap;
    }

    public void setSectionMapPoints(SMcVector3D[] points)
    {
        mSectionMapPoints = points;
    }

    public SMcVector3D[] getSectionMapPoints()
    {
        return mSectionMapPoints;
    }

    public IMcMapViewport.SCreateData getSCreateData(int width, int height) {
        mSCreateData.pCoordinateSystem = mGridCoordinateSystem;
        mSCreateData.pDevice = mMapDevice;
        mSCreateData.uWidth = width;
        mSCreateData.uHeight = height;
        mSCreateData.pOverlayManager = mOverlayManager;
        mSCreateData.bShowGeoInMetricProportion = mShowGeoInMetricProportion;
        mSCreateData.eMapType = mMapType;

        return mSCreateData;
    }

    public IMcMapViewport.SCreateData getSCreateData() {
        return mSCreateData;
    }

    public void setSCreateData(IMcMapViewport.SCreateData sCreateData)
    {
        mSCreateData = sCreateData;
    }

    public boolean getIsAutomation()
    {
       return  mIsAutomation;
    }

    public void setIsAutomation(boolean isAutomation)
    {
        mIsAutomation = isAutomation;
    }

    public static AMCTViewPort getViewportInCreation() {
        if (mCurViewPort == null) {
            mCurViewPort = new AMCTViewPort();
        }
        return mCurViewPort;
    }

    public AMCTViewPort cloneViewportInCreation() {
        AMCTViewPort cloneAMCTViewPort = new AMCTViewPort();
        cloneAMCTViewPort.mGridCoordinateSystem = this.mGridCoordinateSystem;
        cloneAMCTViewPort.mMapDevice = this.mMapDevice;
        cloneAMCTViewPort.mOverlayManager = this.mOverlayManager;
        cloneAMCTViewPort.mTerrainsArrList = mTerrainsArrList;
        cloneAMCTViewPort.mSCreateData = mSCreateData;
        return cloneAMCTViewPort;
    }

    public IMcMapCamera.EMapType getMapType()
    {
        return mMapType;
    }

    public void setMapType(IMcMapCamera.EMapType mapType)
    {
        mMapType = mapType;
    }

    public void setShowGeoInMetricProportion(boolean showGeoInMetricProportion)
    {
        mShowGeoInMetricProportion = showGeoInMetricProportion;
    }

    public boolean getShowGeoInMetricProportion()
    {
        return mShowGeoInMetricProportion;
    }

}
