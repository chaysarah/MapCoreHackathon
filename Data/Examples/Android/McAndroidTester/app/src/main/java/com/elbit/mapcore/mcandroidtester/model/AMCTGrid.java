package com.elbit.mapcore.mcandroidtester.model;

import com.elbit.mapcore.Enums.EMcPointCoordSystem;
import com.elbit.mapcore.General.CMcEnumBitField;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcTextItem;
import com.elbit.mapcore.Structs.SMcFVector2D;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_AMCTMapForm;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.ui.objects.ObjectPropertiesBase;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.BaseApplication;
import com.elbit.mapcore.mcandroidtester.utils.Consts;

import com.elbit.mapcore.Classes.Calculations.GridCoordSystem.McGridCoordSystemGeographic;
import com.elbit.mapcore.Classes.Calculations.GridCoordSystem.McGridMGRS;
import com.elbit.mapcore.Classes.Calculations.GridCoordSystem.McGridNZMG;
import com.elbit.mapcore.Classes.Calculations.GridCoordSystem.McGridUTM;
import com.elbit.mapcore.Classes.Map.McMapGrid;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Calculations.GridCoordSystem.IMcGridCoordinateSystem;
import com.elbit.mapcore.Interfaces.Map.IMcMapGrid;
import com.elbit.mapcore.Structs.SMcBox;
import com.elbit.mapcore.Structs.SMcVector2D;
import com.elbit.mapcore.Structs.SMcVector3D;

import static com.elbit.mapcore.Interfaces.Map.IMcMapGrid.EAngleFormat;
import static com.elbit.mapcore.Interfaces.Map.IMcMapGrid.SGridRegion;
import static com.elbit.mapcore.Interfaces.Map.IMcMapGrid.SScaleStep;

//Created by tc99382 on 13/07/2017.


public class AMCTGrid {

    public static void createGrid(final MapsContainerActivity mapsContainerActivity, final int gridType) {
        mapsContainerActivity.mMapFragment.mView.queueEvent(new Runnable() {
            @Override
            public void run() {
                GridData gridData = null;
                switch (gridType) {
                    case Consts.GridType.GEO_GRID:
                        gridData = getGeoMapGrid();
                        break;
                    case Consts.GridType.UTM_GRID:
                        gridData = getUtmMapGrid();
                        break;
                    case Consts.GridType.MGRS_GRID:
                        gridData = getMGRSMapGrid();
                        break;
                    case Consts.GridType.NZMG_GRID:
                        gridData = getNZMGMapGrid();
                        break;
                }
                if (gridData != null) {
                    try {
                        IMcMapGrid newGrid = IMcMapGrid.Static.Create(gridData.getRegion(), gridData.getScale(), ObjectPropertiesBase.Grid_IsUsingBasicItemPropertiesOnly);
                        boolean isUsing = newGrid.IsUsingBasicItemPropertiesOnly();
                        Manager_AMCTMapForm.getInstance().getCurViewport().SetGrid(newGrid);
                        Manager_AMCTMapForm.getInstance().getCurViewport().SetGridVisibility(true);
                    } catch (MapCoreException mcEx) {
                        AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), mcEx, "McMapGrid.Static.Create");
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            }
        });
    }

    private static GridData getUtmMapGrid() {
        SGridRegion[] region = new SGridRegion[60];
        SScaleStep[] scale = new SScaleStep[6];
        for (int i = 0; i < 60; i++) {
            try {
                region[i] = new SGridRegion();
                region[i].pCoordinateSystem = McGridUTM.Static.Create(i + 1, IMcGridCoordinateSystem.EDatumType.EDT_ED50_ISRAEL);
            } catch (MapCoreException mcEx) {
                AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), mcEx, "McGridUTM.Static.Create");
            }
            region[i].GeoLimit = new SMcBox();
            region[i].GeoLimit.MaxVertex = new SMcVector3D();
            region[i].GeoLimit.MinVertex = new SMcVector3D();
            region[i].GeoLimit.MinVertex.x = -18000000 + 600000 * i;
            region[i].GeoLimit.MinVertex.y = 0;
            region[i].GeoLimit.MaxVertex.x = -17400001 + 600000 * i;
            region[i].GeoLimit.MaxVertex.y = 8400000;
            region[i].uFirstScaleStepIndex = 0;
            region[i].uLastScaleStepIndex = 5;
        }
        scale[0] = new SScaleStep(12.5f, new SMcVector2D(1000, 1000), 2, 2, 5, 5, 3, EAngleFormat.EAF_DECIMAL_DEG);
        scale[1] = new SScaleStep(25f, new SMcVector2D(1000, 1000), 2, 2, 10, 10, 3, EAngleFormat.EAF_DECIMAL_DEG);
        scale[2] = new SScaleStep(125f, new SMcVector2D(20000, 20000), 2, 2, 2, 2, 3, EAngleFormat.EAF_DECIMAL_DEG);
        scale[3] = new SScaleStep(625f, new SMcVector2D(50000, 50000), 2, 2, 2, 2, 3, EAngleFormat.EAF_DECIMAL_DEG);
        scale[4] = new SScaleStep(1250f, new SMcVector2D(100000, 100000), 2, 2, 2, 2, 3, EAngleFormat.EAF_DECIMAL_DEG);
        scale[5] = new SScaleStep(2500f, new SMcVector2D(200000, 200000), 2, 2, 2, 2, 3, EAngleFormat.EAF_DECIMAL_DEG);

        return new GridData(region, scale);
    }

    private static GridData getGeoMapGrid() {
        SGridRegion[] region = new SGridRegion[1];
        SScaleStep[] scale = new SScaleStep[6];

        IMcGridCoordinateSystem gridCoordSys = null;
        try {
            gridCoordSys = McGridCoordSystemGeographic.Static.Create(IMcGridCoordinateSystem.EDatumType.EDT_ED50_ISRAEL);
        } catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), mcEx, "McGridCoordSystemGeographic.Static.Create");
            mcEx.printStackTrace();
        }
        region[0] = new SGridRegion();

        region[0].pCoordinateSystem = gridCoordSys;
        region[0].GeoLimit = new SMcBox();
        region[0].GeoLimit.MaxVertex = new SMcVector3D();
        region[0].GeoLimit.MinVertex = new SMcVector3D();
        region[0].GeoLimit.MinVertex.x = 0;
        region[0].GeoLimit.MinVertex.y = 0;
        region[0].GeoLimit.MaxVertex.x = 0;
        region[0].GeoLimit.MaxVertex.y = 0;
        region[0].uFirstScaleStepIndex = 0;
        region[0].uLastScaleStepIndex = 5;

        scale[0] = new SScaleStep(14.64f, new SMcVector2D(833, 833), 2, 2, 5, 5, 3, EAngleFormat.EAF_DEG_MIN);
        scale[1] = new SScaleStep(29.296f, new SMcVector2D(833, 833), 2, 2, 10, 10, 3, EAngleFormat.EAF_DEG_MIN);
        scale[2] = new SScaleStep(146.48f, new SMcVector2D(20000, 20000), 2, 2, 2, 2, 3, EAngleFormat.EAF_DEG_MIN);
        scale[3] = new SScaleStep(723.42f, new SMcVector2D(50000, 50000), 2, 2, 2, 2, 3, EAngleFormat.EAF_DEG_MIN);
        scale[4] = new SScaleStep(1464f, new SMcVector2D(100000, 100000), 2, 2, 2, 2, 3, EAngleFormat.EAF_DEG_MIN);
        scale[5] = new SScaleStep(2929f, new SMcVector2D(200000, 200000), 2, 2, 2, 2, 3, EAngleFormat.EAF_DEG_MIN);
        return new GridData(region, scale);
    }

    private static GridData getMGRSMapGrid() {
        SGridRegion[] region = new SGridRegion[2];
        SScaleStep[] scale = new SScaleStep[2];

        IMcGridCoordinateSystem gridCoordSys = null;
        try {
            gridCoordSys = McGridMGRS.Static.Create();
        } catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), mcEx, "McGridMGRS.Static.Create().Static.Create");
        } catch (Exception e) {
            e.printStackTrace();
        }
        region[0] = new SGridRegion();

        region[0].pCoordinateSystem = gridCoordSys;
        region[0].GeoLimit = new SMcBox();
        region[0].GeoLimit.MaxVertex = new SMcVector3D();
        region[0].GeoLimit.MinVertex = new SMcVector3D();
        region[0].GeoLimit.MinVertex.x = 0;
        region[0].GeoLimit.MinVertex.y = 0;
        region[0].GeoLimit.MaxVertex.x = 0;
        region[0].GeoLimit.MaxVertex.y = 0;
        region[0].uFirstScaleStepIndex = 0;
        region[0].uLastScaleStepIndex = 0;

        region[1] = new SGridRegion();
        region[1].pCoordinateSystem = gridCoordSys;
        region[1].GeoLimit = new SMcBox();
        region[1].GeoLimit.MaxVertex = new SMcVector3D();
        region[1].GeoLimit.MinVertex = new SMcVector3D();
        region[1].GeoLimit.MinVertex.x = 0;
        region[1].GeoLimit.MinVertex.y = 0;
        region[1].GeoLimit.MaxVertex.x = 0;
        region[1].GeoLimit.MaxVertex.y = 0;
        region[1].uFirstScaleStepIndex = 1;
        region[1].uLastScaleStepIndex = 1;

        scale[0] = new SScaleStep(1000f, new SMcVector2D(100000, 100000), 2, 2, 2, 2, 2, EAngleFormat.EAF_DECIMAL_DEG);
        scale[1] = new SScaleStep(1000f, new SMcVector2D(20000, 20000), 2, 2, 2, 2, 2, EAngleFormat.EAF_DECIMAL_DEG);
        return new GridData(region, scale);
    }

    private static GridData getNZMGMapGrid() {
        SGridRegion[] region = new SGridRegion[1];
        SScaleStep[] scale = new SScaleStep[1];

        IMcGridCoordinateSystem gridCoordSys = null;
        try {
            gridCoordSys = McGridNZMG.Static.Create();
        } catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), mcEx, "McGridNZMG.Static.Create");
        } catch (Exception e) {
            e.printStackTrace();
        }
        region[0] = new SGridRegion();
        region[0].GeoLimit = new SMcBox();
        region[0].GeoLimit.MaxVertex = new SMcVector3D();
        region[0].GeoLimit.MinVertex = new SMcVector3D();
        region[0].pCoordinateSystem = gridCoordSys;
        region[0].GeoLimit.MinVertex.x = 0;
        region[0].GeoLimit.MinVertex.y = 0;
        region[0].GeoLimit.MaxVertex.x = 0;
        region[0].GeoLimit.MaxVertex.y = 0;
        region[0].uFirstScaleStepIndex = 0;
        region[0].uLastScaleStepIndex = 0;

        scale[0] = new SScaleStep(100000f, new SMcVector2D(10000, 10000), 2, 2, 2, 2, 2, EAngleFormat.EAF_DECIMAL_DEG);
        return new GridData(region, scale);
    }

}
