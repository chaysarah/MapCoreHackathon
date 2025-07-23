class EditModePropertiesBase {
    /**** General ****/
    /** Any Map Type **/
    DiscardChanges: boolean = false;
    EnableAddingNewPoints: boolean = false;
    EnableDistanceDirectionMeasure: boolean = false;
    /** 3D Map Only **/
    UtilityItemsOptionalScreenSize: number = 0;
    UseLocalAxesAtEditing: boolean = true;
    KeepScaleRatioAtEditing: boolean = false;

    /**** Map Manipulations Operations ****/
    /** Navigate Map **/
    DrawLine: boolean = true;
    OneOperationOnly: boolean = true;
    NavigateMapWaitForMouseClick: boolean = true;
    /** Dynamic Zoom **/ 
    DynamicZoomMinScale: number = 0.001;
    DynamicZoomWaitForMouseClick: boolean = false;
    DynamicZoomRectangle: MapCore.IMcRectangleItem = null;
    DynamicZoomOperation: MapCore.IMcMapCamera.ESetVisibleArea3DOperation = MapCore.IMcMapCamera.ESetVisibleArea3DOperation.ESVAO_ROTATE_AND_MOVE;

    /** Distance Direction Measure **/
    ShowResult: boolean = true;
    DistanceDirectionWaitForMouseClick: boolean = false;
}
export default EditModePropertiesBase;