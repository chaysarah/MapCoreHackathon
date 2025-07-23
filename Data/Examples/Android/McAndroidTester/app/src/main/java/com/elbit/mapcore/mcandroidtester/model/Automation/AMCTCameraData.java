package com.elbit.mapcore.mcandroidtester.model.Automation;

public class AMCTCameraData {
    public AMCTCameraClipDistances CameraClipDistances;
    public AMCTCameraPosition CameraPosition;
    public AMCTCameraOrientation CameraOrientation;
    public float CameraScale;
    public float CameraFieldOfView;

    public AMCTCameraData() {
        CameraClipDistances = new AMCTCameraClipDistances();
        CameraPosition = new AMCTCameraPosition();
        CameraOrientation = new AMCTCameraOrientation();
    }
}
