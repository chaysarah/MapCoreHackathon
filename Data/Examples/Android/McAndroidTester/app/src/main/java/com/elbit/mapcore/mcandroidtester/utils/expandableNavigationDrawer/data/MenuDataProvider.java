package com.elbit.mapcore.mcandroidtester.utils.expandableNavigationDrawer.data;

import android.app.Activity;
import android.content.Intent;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Map.IMcMapGrid;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_AMCTMapForm;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_MCMapGrid;
import com.elbit.mapcore.mcandroidtester.model.AMCTGrid;
import com.elbit.mapcore.mcandroidtester.model.AMCTMapTiles;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.ui.objects.ObjectPropertiesBase;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.BaseApplication;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by awidiyadew on 15/09/16.
 */
public class MenuDataProvider {
    public static final String MAP_GROUP = "Map";
    public static final String OBJECTS_GROUP = "Objects";
    public static final String EDIT_GROUP = "Edit";
    public static final String ZOOMING_GROUP = "Zooming";
    public static final String OBJECT_PROPERTIES_GROUP = "Object Properties";
    public static final String MAP_GRID_GROUP = "Map Grid";
    public static final String SCAN_GROUP = "Scan";
    public static final String MAP_TILES_GROUP = "Map Tiles (Shift+Key)";

    private static final int MAX_LEVELS = 3;

    public static final int LEVEL_1 = 1;
    public static final int LEVEL_2 = 2;
    public static final int LEVEL_3 = 3;

    private static List<BaseItem> mMenu = new ArrayList<>();

    public static List<BaseItem> getInitialItems() {
        //return getSubItems(new GroupItem("root"));

        List<BaseItem> rootMenu = new ArrayList<>();

        /*
        * ITEM = TANPA CHILD
        * GROUPITEM = DENGAN CHILD
        * */
        rootMenu.add(new GroupItem(MAP_GROUP, -1));
        rootMenu.add(new GroupItem(OBJECTS_GROUP, -1));
        rootMenu.add(new GroupItem(EDIT_GROUP, -1));
        rootMenu.add(new GroupItem(ZOOMING_GROUP, -1));
        rootMenu.add(new GroupItem(OBJECT_PROPERTIES_GROUP, -1));
        rootMenu.add(new GroupItem(MAP_GRID_GROUP, -1));
        rootMenu.add(new GroupItem(SCAN_GROUP, -1));
        rootMenu.add(new GroupItem(MAP_TILES_GROUP, -1));
        return rootMenu;
    }

    public static List<BaseItem> getSubItems(BaseItem baseItem, Activity mapsContainerActivity) {


        List<BaseItem> result = new ArrayList<>();
        int level = ((GroupItem) baseItem).getLevel() + 1;
        String menuItem = baseItem.getName();

        if (!(baseItem instanceof GroupItem)) {
            throw new IllegalArgumentException("GroupItem required");
        }

        GroupItem groupItem = (GroupItem) baseItem;
        if (groupItem.getLevel() >= MAX_LEVELS) {
            return null;
        }

        /*
        * HANYA UNTUK GROUP-ITEM
        * */
        switch (level) {
            case LEVEL_1:
                switch (menuItem) {
                    case MAP_GROUP:
                        result = getMapItems((MapsContainerActivity) mapsContainerActivity);
                        break;
                    case OBJECTS_GROUP:
                        result = getObjectsItems((MapsContainerActivity) mapsContainerActivity);
                        break;
                    case EDIT_GROUP:
                        result = getEditItems((MapsContainerActivity) mapsContainerActivity);
                        break;
                    case ZOOMING_GROUP:
                        result = getZoomingItems((MapsContainerActivity) mapsContainerActivity);
                        break;
                    case OBJECT_PROPERTIES_GROUP:
                        result = getObjectPropertiesItems((MapsContainerActivity) mapsContainerActivity);
                        break;
                    case MAP_GRID_GROUP:
                        result = getMapGridGroupItems((MapsContainerActivity) mapsContainerActivity);
                        break;
                    case SCAN_GROUP:
                        result = getScanGroupItems((MapsContainerActivity) mapsContainerActivity);
                        break;
                    case MAP_TILES_GROUP:
                        result = getMapTilesGroupItems((MapsContainerActivity) mapsContainerActivity);
                        break;
                }
                break;

            case LEVEL_2:
                switch (menuItem) {
                    case "GROUP 1":
                        result = getListGroup1();
                        break;
                    case "GROUP X":
                        result = getListGroupX();
                        break;
                }
                break;
        }

        return result;
    }

    private static List<BaseItem> getMapTilesGroupItems(final MapsContainerActivity mapsContainerActivity) {
        List<BaseItem> list = new ArrayList<>();
        Runnable setBoxDrawMode = new Runnable() {
            @Override
            public void run() {
                AMCTMapTiles.SetDebugOption(AMCTMapTiles.MapTilesKey.D);
            }
        };
        Runnable setOverlayObjectsBoxDrawMode = new Runnable() {
            @Override
            public void run() {
                AMCTMapTiles.SetDebugOption(AMCTMapTiles.MapTilesKey.B);
            }
        };
        Runnable setWireFrameMode = new Runnable() {
            @Override
            public void run() {
                AMCTMapTiles.SetDebugOption(AMCTMapTiles.MapTilesKey.W);
            }
        };
        Runnable setSaveIntersectingTile = new Runnable() {
            @Override
            public void run() {
                AMCTMapTiles.SetDebugOption(AMCTMapTiles.MapTilesKey.S);
            }
        };
        Runnable setViewportStatistic  = new Runnable() {
            @Override
            public void run() {
                AMCTMapTiles.SetDebugOption(AMCTMapTiles.MapTilesKey.Stat);
            }
        };
        list.add(new Item("Box Draw Mode (D)", android.R.drawable.ic_dialog_map, setBoxDrawMode));
        list.add(new Item("Overlay Objects Box Draw Mode (B)", android.R.drawable.ic_dialog_map, setOverlayObjectsBoxDrawMode));
        list.add(new Item("Wireframe Mode (W)", android.R.drawable.ic_dialog_map, setWireFrameMode));
        list.add(new Item("Save Intersection Tile (S)", android.R.drawable.ic_dialog_map, setSaveIntersectingTile));
        list.add(new Item("Viewport Statistic (Stat)", android.R.drawable.ic_dialog_map, setViewportStatistic));

        return list;
    }

    private static List<BaseItem> getScanGroupItems(final MapsContainerActivity mapsContainerActivity) {
        List<BaseItem> list = new ArrayList<>();

        Runnable openScanAction = new Runnable() {
            @Override
            public void run() {
                mapsContainerActivity.mMapFragment.openScan();
            }
        };
        list.add(new Item("scan", R.drawable.img_scan, openScanAction));

        return list;
    }

    private static List<BaseItem> getMapGridGroupItems(final MapsContainerActivity mapsContainerActivity) {
        List<BaseItem> list = new ArrayList<>();
        Runnable showHeightLinesAction = new Runnable() {
            @Override
            public void run() {
                mapsContainerActivity.mMapFragment.heightLines();
            }
        };
        Runnable showUtmGridAction = new Runnable() {
            @Override
            public void run() {
                AMCTGrid.createGrid(mapsContainerActivity,Consts.GridType.UTM_GRID);
            }
        };
        Runnable showGeoGridAction = new Runnable() {
            @Override
            public void run() {
                AMCTGrid.createGrid(mapsContainerActivity,Consts.GridType.GEO_GRID);
            }
        };
        Runnable showMgnsGridAction = new Runnable() {
            @Override
            public void run() {
                AMCTGrid.createGrid(mapsContainerActivity,Consts.GridType.MGRS_GRID);
            }
        };
        Runnable showNzmgGridAction = new Runnable() {
            @Override
            public void run() {
                AMCTGrid.createGrid(mapsContainerActivity,Consts.GridType.NZMG_GRID);
            }
        };
        Runnable removeGridAction = new Runnable() {
            @Override
            public void run() {
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            Manager_AMCTMapForm.getInstance().getCurViewport().SetGrid(null);
                            Manager_AMCTMapForm.getInstance().getCurViewport().SetGridVisibility(false);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "SetGrid/SetGridVisibility");
                            e.printStackTrace();
                        } catch (Exception e) {
                            e.printStackTrace();
                        }

                    }
                });
            }
        };
        list.add(new Item("Height Lines", R.drawable.img_height_lines, showHeightLinesAction));
        list.add(new Item("UTM grid", android.R.drawable.ic_dialog_map, showUtmGridAction));
        list.add(new Item("GEO grid", android.R.drawable.ic_menu_mapmode, showGeoGridAction));
        list.add(new Item("MGNS grid", android.R.drawable.ic_menu_camera, showMgnsGridAction));
        list.add(new Item("NZMG grid", android.R.drawable.ic_menu_camera, showNzmgGridAction));
        list.add(new Item("remove grid", android.R.drawable.ic_menu_camera, removeGridAction));


        return list;
    }

    private static List<BaseItem> getMapItems(final MapsContainerActivity mapsContainerActivity) {
        List<BaseItem> list = new ArrayList<>();

        Runnable openSectionMapAction = new Runnable() {
            @Override
            public void run() {
                mapsContainerActivity.mMapFragment.OpenSectionMap();
            }
        };
        Runnable openDeviceTabsAction = new Runnable() {
            @Override
            public void run() {
                Intent openDeviceTabsIntent = new Intent(mapsContainerActivity, com.elbit.mapcore.mcandroidtester.ui.device.activities.DeviceTabsActivity.class);
                mapsContainerActivity.startActivity(openDeviceTabsIntent);
            }
        };
        Runnable createVpWithSeveralLayersAction = new Runnable() {
            @Override
            public void run() {
                Intent createVpIntent = new Intent(mapsContainerActivity, com.elbit.mapcore.mcandroidtester.ui.map.activities.ViewPortWithSeveralLayersActivity.class);
                mapsContainerActivity.startActivity(createVpIntent);
            }
        };
        Runnable openCreateMapTabsAction = new Runnable() {
            @Override
            public void run() {
                Intent CreateMapIntent = new Intent(mapsContainerActivity, com.elbit.mapcore.mcandroidtester.ui.map.activities.MapTabsActivity.class);
                mapsContainerActivity.startActivity(CreateMapIntent);
            }
        };
        list.add(new Item("open map with several layers", android.R.drawable.ic_dialog_map, createVpWithSeveralLayersAction));
        list.add(new Item("open map manually", android.R.drawable.ic_menu_mapmode, openCreateMapTabsAction));
        list.add(new Item("open device tabs", android.R.drawable.ic_menu_camera, openDeviceTabsAction));
        list.add(new Item("open section map", android.R.drawable.ic_menu_camera, openSectionMapAction));

        return list;
    }

    private static List<BaseItem> getZoomingItems(final MapsContainerActivity mapsContainerActivity) {
        List<BaseItem> list = new ArrayList<>();
        Runnable navigateMapAction = new Runnable() {
            @Override
            public void run() {
                mapsContainerActivity.mMapFragment.navigateMap();
            }
        };
        Runnable zoomInAction = new Runnable() {
            @Override
            public void run() {
                mapsContainerActivity.mMapFragment.zoomIn();
            }
        };
        Runnable zoomOutAction = new Runnable() {
            @Override
            public void run() {
                mapsContainerActivity.mMapFragment.zoomOut();
            }
        };
        Runnable centerPointAction = new Runnable() {
            @Override
            public void run() {
                mapsContainerActivity.mMapFragment.centerMap();
            }
        };
        Runnable dynamicZoomAction = new Runnable() {
            @Override
            public void run() {
                mapsContainerActivity.mMapFragment.dynamicZoom();
            }
        };
        Runnable distanceDirectionMeasureAction = new Runnable() {
            @Override
            public void run() {
                mapsContainerActivity.mMapFragment.distanceDirectionMeasure();
            }
        };

        list.add(new Item("navigate map", R.drawable.img_navigate_map, navigateMapAction));
        list.add(new Item("zooming in", R.drawable.img_zoom_in, zoomInAction));
        list.add(new Item("zooming out", R.drawable.img_zoom_out, zoomOutAction));
        list.add(new Item("center map", R.drawable.img_center_point, centerPointAction));
        list.add(new Item("dynamic zoom", R.drawable.img_dynamic_zoom, dynamicZoomAction));
        list.add(new Item("distance direction measure", R.drawable.img_distance_direction_measure, distanceDirectionMeasureAction));

        return list;
    }

    public static boolean isExpandable(BaseItem baseItem) {
        return baseItem instanceof GroupItem;
    }

    private static List<BaseItem> getObjectsItems(final MapsContainerActivity mapsContainerActivity) {

        List<BaseItem> list = new ArrayList<>();
       /* GroupItem groupItem = new GroupItem("GROUP 1");
        groupItem.setLevel(groupItem.getLevel() + 1);*/
        Runnable drawLineAction = new Runnable() {
            @Override
            public void run() {
                mapsContainerActivity.mMapFragment.drawLine();
            }
        };
        Runnable drawPolygonAction = new Runnable() {
            @Override
            public void run() {
                mapsContainerActivity.mMapFragment.drawPolygon();
            }
        };
        Runnable drawEllipseAction = new Runnable() {
            @Override
            public void run() {
                mapsContainerActivity.mMapFragment.drawEllipse();
            }
        };
        Runnable drawPictureAction = new Runnable() {
            @Override
            public void run() {
                mapsContainerActivity.mMapFragment.drawPicture();
            }
        };
        Runnable drawArcAction = new Runnable() {
            @Override
            public void run() {
                mapsContainerActivity.mMapFragment.drawArc();
            }
        };
        Runnable drawArrowAction = new Runnable() {
            @Override
            public void run() {
                mapsContainerActivity.mMapFragment.drawArrow();
            }
        };
        Runnable drawLineExpansionAction = new Runnable() {
            @Override
            public void run() {
                mapsContainerActivity.mMapFragment.drawLineExpansion();
            }
        };
        Runnable drawRectAction = new Runnable() {
            @Override
            public void run() {
                mapsContainerActivity.mMapFragment.drawRect();
            }
        };
        Runnable drawTextAction = new Runnable() {
            @Override
            public void run() {
                mapsContainerActivity.mMapFragment.drawText();
            }
        };
        Runnable drawMeshAction = new Runnable() {
            @Override
            public void run() {
                mapsContainerActivity.mMapFragment.drawMesh();
            }
        };

        list.add(new Item("line item", R.drawable.img_line_item, drawLineAction));
        list.add(new Item("arrow item", R.drawable.img_arrow_item, drawArrowAction));
        list.add(new Item("line expansion item", R.drawable.img_line_expansion_item, drawLineExpansionAction));
        list.add(new Item("polygon item", R.drawable.img_polygon_item, drawPolygonAction));
        list.add(new Item("ellipse item", R.drawable.img_ellipse_item, drawEllipseAction));
        list.add(new Item("arc item", R.drawable.img_arc_item, drawArcAction));
        list.add(new Item("rectangle item", R.drawable.img_rectangle_item, drawRectAction));
        list.add(new Item("picture item", R.drawable.img_picture_item, drawPictureAction));
        list.add(new Item("text item", R.drawable.img_text_item, drawTextAction));
        list.add(new Item("mesh item", R.drawable.img_mesh_item, drawMeshAction));

        //list.add(groupItem);

        return list;
    }

    private static List<BaseItem> getEditItems(final MapsContainerActivity mapsContainerActivity) {

        List<BaseItem> list = new ArrayList<>();
       /* GroupItem groupItem = new GroupItem("GROUP 1");
        groupItem.setLevel(groupItem.getLevel() + 1);*/

        Runnable initModeAction = new Runnable() {
            @Override
            public void run() {
                mapsContainerActivity.mMapFragment.startInitMode();
            }
        };
        Runnable editModeAction = new Runnable() {
            @Override
            public void run() {
                mapsContainerActivity.mMapFragment.startEditMode();
            }
        };
        Runnable editModePropertiesAction = new Runnable() {
            @Override
            public void run() {
                mapsContainerActivity.mMapFragment.OpenEditModeProperties();
            }
        };
        list.add(new Item("edit object", R.drawable.img_edit_object, editModeAction));
        list.add(new Item("init object", R.drawable.img_init_object, initModeAction));
        list.add(new Item("Edit Mode Properties", R.drawable.img_edit_mode_properties, editModePropertiesAction));

        return list;
    }

    private static List<BaseItem> getObjectPropertiesItems(final MapsContainerActivity mapsContainerActivity) {

        List<BaseItem> list = new ArrayList<>();

        Runnable openObjPropertiesAction = new Runnable() {
            @Override
            public void run() {
                mapsContainerActivity.mMapFragment.OpenObjectProperties();
            }
        };
        list.add(new Item("object properties", R.drawable.img_object_properties, openObjPropertiesAction));

        return list;
    }

    private static List<BaseItem> getListGroup1() {
        List<BaseItem> list = new ArrayList<>();
        list.add(new Item("CHILD OF G1-A", -1));
        list.add(new Item("CHILD OF G1-B", -1));

        return list;
    }

    private static List<BaseItem> getListGroupX() {
        List<BaseItem> list = new ArrayList<>();
        list.add(new Item("CHILD OF GX-A", -1));
        list.add(new Item("CHILD OF GX-B", -1));

        return list;
    }

}
