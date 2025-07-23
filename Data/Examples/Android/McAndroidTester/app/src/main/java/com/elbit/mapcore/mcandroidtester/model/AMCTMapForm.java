package com.elbit.mapcore.mcandroidtester.model;

import com.elbit.mapcore.mcandroidtester.managers.EditmodeCallbackManager;
import com.elbit.mapcore.mcandroidtester.model.Automation.AMCTAutomationParams;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.BaseApplication;

import com.elbit.mapcore.Classes.General.McEditMode;
import com.elbit.mapcore.General.CMcEnumBitField;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.General.IMcEditMode;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;

import java.io.File;

//Created by TC99382 on 10/05/2017.


public class AMCTMapForm {

    private IMcMapViewport mMapViewport;
    private IMcEditMode mEditMode;
    private EditmodeCallbackManager mEditModeManagerCallback;
    private boolean mIsRenderAllMode = false;
    private IMcMapViewport mSecondMapViewport;

    private boolean mIsAutomation = false;
    private AMCTAutomationParams mAutomationParams;
    private String mAutomationPrintViewportPath;
    private String mJsonFolderPath;
    private File mAutomationLogFile;

    public AMCTMapForm(IMcMapViewport mapViewport)
    {
        this.mMapViewport = mapViewport;
    }

    public void setAutomationParams(AMCTAutomationParams automationParams) {
        mAutomationParams = automationParams;
    }
    public AMCTAutomationParams getAutomationParams()
    {
        return mAutomationParams;
    }

    public void setIsAutomation(boolean isAutomation) {
        mIsAutomation = isAutomation;
    }
    public boolean getIsAutomation()
    {
        return  mIsAutomation;
    }



    public void setAutomationPrintViewportPath(String automationPrintViewportPath) {
        mAutomationPrintViewportPath = automationPrintViewportPath;
    }
    public String getAutomationPrintViewportPath() {
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


    public IMcMapViewport getmMapViewport() {
        return mMapViewport;
    }
    public void setMapViewport(IMcMapViewport mapViewport) {
        this.mMapViewport = mapViewport;
    }

    public void setSecondMapViewport(IMcMapViewport mapViewport) {
        this.mSecondMapViewport = mapViewport;
    }
    public IMcMapViewport getSecondMapViewport() {
        return mSecondMapViewport;
    }

    public IMcMapViewport[] getViewports() {
        if (mMapViewport != null && mSecondMapViewport != null) {
            IMcMapViewport viewports[] = new IMcMapViewport[]{mMapViewport, mSecondMapViewport};
            return viewports;
        } else if(mMapViewport != null){
            IMcMapViewport viewports[] = new IMcMapViewport[]{mMapViewport};
            return viewports;
        }
        return null;
    }

    public boolean getIsRenderAllMode() {
        return mIsRenderAllMode;
    }
    public void setIsRenderAllMode(boolean isRenderAllMode) {
        this.mIsRenderAllMode = isRenderAllMode;
    }

    public void setEditMode(IMcEditMode editMode) {
        this.mEditMode = editMode;
    }
    public IMcEditMode getEditMode() {
        return mEditMode;
    }

    public void setEditModeManagerCallback(EditmodeCallbackManager editModeManagerCallback) {
        this.mEditModeManagerCallback = editModeManagerCallback;
    }
    public EditmodeCallbackManager getEditModeManagerCallback() {
        return mEditModeManagerCallback;
    }

    public IMcMapViewport createEditMode(IMcMapViewport viewport) {
        try {
            this.mEditMode = McEditMode.Static.Create(viewport);
            this.mEditMode.SetMouseMoveUsageForMultiPointItem(IMcEditMode.EMouseMoveUsage.EMMU_IGNORED);

           /* CMcEnumBitField<IMcOverlayManager.ESizePropertyType> eSizePropertyType = new CMcEnumBitField<>(IMcOverlayManager.ESizePropertyType.ESPT_LINE_WIDTH);
            eSizePropertyType.Set(IMcOverlayManager.ESizePropertyType.ESPT_OFFSET, IMcOverlayManager.ESizePropertyType.ESPT_PICTURE_SIZE);
            viewport.GetOverlayManager().SetItemSizeFactors(eSizePropertyType, 3.0f);*/
            mEditModeManagerCallback = new EditmodeCallbackManager(mEditMode);
            return viewport;
        } catch (MapCoreException e) {
            e.printStackTrace();
            AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "McEditMode.Static.Create");
        } catch (Exception e) {
            e.printStackTrace();
        }
        return viewport;
    }
}
