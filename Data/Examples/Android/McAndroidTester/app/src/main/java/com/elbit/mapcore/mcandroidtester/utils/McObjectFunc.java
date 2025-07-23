package com.elbit.mapcore.mcandroidtester.utils;

import androidx.fragment.app.FragmentActivity;

import com.elbit.mapcore.General.ObjectRef;
import com.elbit.mapcore.Interfaces.Calculations.IMcImageCalc;
import com.elbit.mapcore.Structs.SMcVector2D;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_AMCTMapForm;

import com.elbit.mapcore.Enums.EMcPointCoordSystem;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Map.IMcMapCamera;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectLocation;
import com.elbit.mapcore.Structs.SMcVector3D;


/**
 * Created by tc97803 on 21/08/2017.
 */

public class McObjectFunc {
    public static void MoveToLocation(final FragmentActivity activity, final IMcObject mcObject) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    SMcVector3D point = new SMcVector3D();
                    SMcVector3D[] objectLocations = mcObject.GetLocationPoints(0);

                    SMcVector3D min = new SMcVector3D(), max = new SMcVector3D();
                    SMcVector3D curr;
                    IMcMapViewport currViewport;
                    EMcPointCoordSystem locationCoordSys;
                    IMcObjectLocation objLocation;
                    SMcVector3D worldPoint = new SMcVector3D();
                    SMcVector3D normal = null;
                    SMcVector3D pointInVP;

                    // point - calc average of bounding box of object
                    if (objectLocations != null && objectLocations.length > 0) {
                        if (objectLocations.length == 1)
                            point = objectLocations[0];
                        else if (objectLocations.length > 1) {
                            min = objectLocations[0];
                            max = objectLocations[1];
                        }

                        if (objectLocations.length > 2) {
                            for (int i = 0; i < objectLocations.length; i++) {
                                curr = objectLocations[i];

                                if (curr.x > max.x && curr.y > max.y)
                                    max = curr;
                                else if (curr.x < min.x && curr.y < min.y)
                                    min = curr;
                            }
                        }
                        if (objectLocations.length > 1)
                            point = SMcVector3D.divide((SMcVector3D.plus(min, max)), 2);

                        currViewport = Manager_AMCTMapForm.getInstance().getCurViewport();

                        objLocation = mcObject.GetScheme().GetObjectLocation(0);
                        locationCoordSys = objLocation.GetCoordSystem();
                        IMcImageCalc mcViewportImageCalc = currViewport.GetImageCalc();
                        boolean isConvert = true;

                        if (locationCoordSys == EMcPointCoordSystem.EPCS_WORLD)
                        {
                            // if viewport is regular do this
                            if (mcViewportImageCalc == null)
                                worldPoint = currViewport.OverlayManagerToViewportWorld(point);
                            else // else viewport is image so do om convert worldtoimage
                            {
                                if (currViewport.GetOverlayManager() != null && point != null)
                                {
                                    worldPoint = currViewport.GetOverlayManager().ConvertWorldToImage(point, mcViewportImageCalc, null);
                                }
                                else
                                    isConvert = false;
                            }
                        }
                        if (locationCoordSys == EMcPointCoordSystem.EPCS_SCREEN) {
                            try {
                                SMcVector3D worldPoint2 = new SMcVector3D();
                                ObjectRef<Boolean> intersection = new ObjectRef<>();
                                currViewport.ScreenToWorldOnTerrain(point, worldPoint2, intersection);

                                if (intersection.getValue() == false)
                                    currViewport.ScreenToWorldOnPlane(point, worldPoint2, intersection);
                                if (intersection.getValue() == false) {
                                    worldPoint.x = 0;
                                    worldPoint.y = 0;
                                    worldPoint.z = 0;
                                }
                            } catch (MapCoreException e) {
                                AlertMessages.ShowMapCoreErrorMessage(activity, e, "ScreenToWorldOnTerrain/ScreenToWorldOnPlane");
                                return;
                            } catch (Exception e) {
                                e.printStackTrace();
                            }
                        }
                         if (locationCoordSys == EMcPointCoordSystem.EPCS_IMAGE) {
                             try
                             {
                                 IMcImageCalc objImageCalc = mcObject.GetImageCalc();
                                 if (objImageCalc == null)
                                 {
                                     isConvert = false;
                                 }
                                 // need to convert points from imagecalc to world viewport.
                                 else if (mcViewportImageCalc != null || !objImageCalc.IsEqual(mcViewportImageCalc))
                                 {
                                     Boolean isDTM = new Boolean(true);

                                     worldPoint = objImageCalc.ImagePixelToCoordWorld(new SMcVector2D(point));//, isDTM, null, null);

                                     worldPoint = currViewport.ImageCalcWorldToViewport(worldPoint, objImageCalc);
                                     SMcVector2D imagePoint;
                                     // if image calc of viewport is diff from image calc of object  ( not is equal return true)
                                     if (currViewport.GetImageCalc() != null)
                                     {
                                         imagePoint = mcViewportImageCalc.WorldCoordToImagePixel(worldPoint);
                                         worldPoint = new SMcVector3D(imagePoint);
                                     }
                                 }
                                 else
                                 {
                                     worldPoint = point;
                                 }
                             }
                             catch (MapCoreException McEx)
                             {
                                 AlertMessages.ShowMapCoreErrorMessage(activity, McEx, "Move To Location");
                                 isConvert = false;
                             }
                        }

                        pointInVP = currViewport.OverlayManagerToViewportWorld(worldPoint);

                        if (currViewport.GetMapType() == IMcMapCamera.EMapType.EMT_2D) {
                            currViewport.SetCameraPosition(pointInVP, false);
                        } else if (currViewport.GetMapType() == IMcMapCamera.EMapType.EMT_3D) {
                            ObjectRef<Boolean> isRelative = new ObjectRef<>();
                            ObjectRef<Long> PropId = new ObjectRef<>();
                            objLocation.GetRelativeToDTM(isRelative, PropId);
                            ObjectRef<Boolean> bHeightFound = new ObjectRef<>();
                            if (isRelative.getValue() == true) {
                                ObjectRef<Double> height = new ObjectRef<>();
                                currViewport.GetTerrainHeight(pointInVP, bHeightFound, height, normal);
                                pointInVP.z = height.getValue();
                            }
                            if (bHeightFound.getValue()) {
                                pointInVP.z = pointInVP.z + 500;
                                currViewport.SetCameraPosition(pointInVP, false);
                                currViewport.SetCameraOrientation(0, -90, 0, false);
                            }
                        }
                    }

                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(activity, e, "Move To Location");
                } catch (Exception e) {
                    e.printStackTrace();
                }

            }
        });
    }
}
