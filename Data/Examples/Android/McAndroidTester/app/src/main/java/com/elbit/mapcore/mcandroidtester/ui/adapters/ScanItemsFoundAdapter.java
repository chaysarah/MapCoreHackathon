package com.elbit.mapcore.mcandroidtester.ui.adapters;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import com.elbit.mapcore.Interfaces.Map.IMcVector3DExtrusionMapLayer;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.managers.Manager_MCNames;

import com.elbit.mapcore.Interfaces.Calculations.IMcSpatialQueries;
import com.elbit.mapcore.Interfaces.Map.IMcStaticObjectsMapLayer;
import com.elbit.mapcore.Structs.SMcVariantID;

/**
 * Created by tc99382 on 22/08/2017.
 */
public class ScanItemsFoundAdapter extends ArrayAdapter<IMcSpatialQueries.STargetFound> {
    /**
     * Constructor
     *
     * @param context  The current context.
     * @param resource The resource ID for a layout file containing a TextView to use when
     *                 instantiating views.
     * @param objects  The objects to represent in the ListView.
     */
    public ScanItemsFoundAdapter(Context context, int resource, IMcSpatialQueries.STargetFound[] objects) {
        super(context, resource, objects);
    }

    /**
     * Get the row id associated with the specified position in the list.
     *
     * @param position The position of the item within the adapter's data set whose row id we want.
     * @return The id of the item at the specified position.
     */
    @Override
    public long getItemId(int position) {
        return 0;
    }

    /**
     * Get a View that displays the data at the specified position in the data set. You can either
     * create a View manually or inflate it from an XML layout file. When the View is inflated, the
     * parent View (GridView, ListView...) will apply default layout parameters unless you use
     * {@link LayoutInflater#inflate(int, ViewGroup, boolean)}
     * to specify a root view and to prevent attachment to the root.
     *
     * @param position    The position of the item within the adapter's data set of the item whose view
     *                    we want.
     * @param convertView The old view to reuse, if possible. Note: You should check that this view
     *                    is non-null and of an appropriate type before using. If it is not possible to convert
     *                    this view to display the correct data, this method can create a new view.
     *                    Heterogeneous lists can specify their number of view types, so that this View is
     *                    always of the right type (see {@link #getViewTypeCount()} and
     *                    {@link #getItemViewType(int)}).
     * @param parent      The parent that this view will eventually be attached to
     * @return A View corresponding to the data at the specified position.
     */
    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        int bitCount = 32;

        RowHolder rowHolder;
        if (convertView == null) {
            convertView = LayoutInflater.from(parent.getContext()).inflate(R.layout.scan_item_found_row, parent, false);
            rowHolder = new RowHolder();
            rowHolder.targetType = (TextView) convertView.findViewById(R.id.scan_item_found_target_type);
            rowHolder.intersectionPtX = (TextView) convertView.findViewById(R.id.scan_item_found_intersection_pt_x);
            rowHolder.intersectionPtY = (TextView) convertView.findViewById(R.id.scan_item_found_intersection_pt_y);
            rowHolder.intersectionPtZ = (TextView) convertView.findViewById(R.id.scan_item_found_intersection_pt_z);
            rowHolder.intersectionCoordSys = (TextView) convertView.findViewById(R.id.scan_item_found_intersection_coord_sys);
            rowHolder.terrainId = (TextView) convertView.findViewById(R.id.scan_item_found_terrain_id);
            rowHolder.layerId = (TextView) convertView.findViewById(R.id.scan_item_found_layer_id);
            rowHolder.targetId = (TextView) convertView.findViewById(R.id.scan_item_found_target_id);
            rowHolder.objectId = (TextView) convertView.findViewById(R.id.scan_item_found_object_id);
            rowHolder.itemId = (TextView) convertView.findViewById(R.id.scan_item_found_item_id);
            rowHolder.subItemId = (TextView) convertView.findViewById(R.id.scan_item_found_sub_item_id);
            rowHolder.partFound = (TextView) convertView.findViewById(R.id.scan_item_found_part_found);
            rowHolder.itemType = (TextView) convertView.findViewById(R.id.scan_item_found_item_type);
            rowHolder.partIndex = (TextView) convertView.findViewById(R.id.scan_item_found_part_index);
            rowHolder.itemColor = (TextView) convertView.findViewById(R.id.scan_item_found_item_color);
        } else {
            rowHolder = (RowHolder) convertView.getTag();
        }
        try {
            rowHolder.targetType.setText(getItem(position).eTargetType.name());
            rowHolder.intersectionPtX.setText(String.valueOf(getItem(position).IntersectionPoint.x));
            rowHolder.intersectionPtY.setText(String.valueOf(getItem(position).IntersectionPoint.z));
            rowHolder.intersectionPtZ.setText(String.valueOf(getItem(position).IntersectionPoint.y));
            rowHolder.intersectionCoordSys.setText(getItem(position).eIntersectionCoordSystem.name());
            IMcSpatialQueries.STargetFound curRowData = getItem(position);
            switch (curRowData.eTargetType) {
                case EITT_ANY_TARGET:
                    break;
                case EITT_DTM_LAYER:
                    rowHolder.terrainId.setText(String.valueOf(curRowData.pTerrain.GetID()) + " (" + Manager_MCNames.getInstance().getIdByObject(curRowData.pTerrain) + ")");
                    rowHolder.layerId.setText(String.valueOf(curRowData.pTerrainLayer.GetID()) + " (" + Manager_MCNames.getInstance().getIdByObject(curRowData.pTerrainLayer) + ")");
                    break;
                case EITT_NONE:
                    break;
                case EITT_OVERLAY_MANAGER_OBJECT:
                    rowHolder.objectId.setText(curRowData.ObjectItemData.pObject.GetID() + " (" + Manager_MCNames.getInstance().getIdByObject(curRowData.ObjectItemData.pObject) + ")");
                    if (curRowData.ObjectItemData.pItem != null) {
                        rowHolder.itemId.setText(curRowData.ObjectItemData.pItem.GetID() + " (" + Manager_MCNames.getInstance().getIdByObject(curRowData.ObjectItemData.pItem) + ")");
                    }
                    rowHolder.subItemId.setText(String.valueOf(curRowData.ObjectItemData.uSubItemID));
                    rowHolder.partFound.setText(curRowData.ObjectItemData.ePartFound.name());
                    if (curRowData.ObjectItemData.pItem != null) {
                        rowHolder.itemType.setText(curRowData.ObjectItemData.pItem.GetNodeType().name());
                    }
                    rowHolder.partIndex.setText(String.valueOf(curRowData.ObjectItemData.uPartIndex));
                    break;
                case EITT_STATIC_OBJECTS_LAYER:
                    if(curRowData.pTerrainLayer instanceof IMcVector3DExtrusionMapLayer) {
                        bitCount = ((IMcVector3DExtrusionMapLayer) curRowData.pTerrainLayer).GetObjectIDBitCount();
                        rowHolder.itemColor.setText(((IMcVector3DExtrusionMapLayer) curRowData.pTerrainLayer).GetObjectColor(curRowData.uTargetID).toString());
                    }
                    rowHolder.terrainId.setText(curRowData.pTerrain.GetID() + " (" + Manager_MCNames.getInstance().getIdByObject(curRowData.pTerrain) + ")");
                        rowHolder.layerId.setText(String.valueOf(curRowData.pTerrainLayer.GetID()) + " (" + Manager_MCNames.getInstance().getIdByObject(curRowData.pTerrainLayer) + ")");
                        rowHolder.targetId.setText(getTargetIdByBitCount(curRowData.uTargetID, bitCount));

                    break;
                case EITT_VISIBLE_VECTOR_LAYER:
                    rowHolder.layerId.setText(String.valueOf(curRowData.pTerrainLayer.GetID()) + " (" + Manager_MCNames.getInstance().getIdByObject(curRowData.pTerrainLayer) + ")");
                    rowHolder.targetId.setText(getTargetIdByBitCount(curRowData.uTargetID, bitCount));
                    rowHolder.subItemId.setText(String.valueOf(curRowData.ObjectItemData.uSubItemID));
                    rowHolder.partFound.setText(curRowData.ObjectItemData.ePartFound.name());
                    rowHolder.partIndex.setText(String.valueOf(curRowData.ObjectItemData.uPartIndex));
                    break;
                case EITT_NON_VISIBLE_VECTOR_LAYER:
                    rowHolder.layerId.setText(String.valueOf(curRowData.pTerrainLayer.GetID()) + " (" + Manager_MCNames.getInstance().getIdByObject(curRowData.pTerrainLayer) + ")");
                    rowHolder.targetId.setText(getTargetIdByBitCount(curRowData.uTargetID, bitCount));
                    break;
            }
        } catch (Exception e) {
            e.printStackTrace();
        }


        convertView.setTag(rowHolder);
        return convertView;
    }

    private String getTargetIdByBitCount(SMcVariantID uTargetIndex, int bitCount) {
        String sTargetID;
        switch (bitCount) {
            case 32:
                sTargetID = String.valueOf(uTargetIndex.Get32Bit());
                break;
            case 64:
                sTargetID = String.valueOf(uTargetIndex.Get64Bit());break;
            case 128:
                sTargetID = String.valueOf(uTargetIndex.Get128Bit());break;
            default:
                sTargetID = String.valueOf(uTargetIndex.Get32Bit());
                break;
        }
        return sTargetID;
    }

    private class RowHolder {
        TextView targetType;
        TextView intersectionPtX;
        TextView intersectionPtY;
        TextView intersectionPtZ;
        TextView intersectionCoordSys;
        TextView terrainId;
        TextView layerId;
        TextView targetId;
        TextView objectId;
        TextView itemId;
        TextView subItemId;
        TextView partFound;
        TextView itemType;
        TextView partIndex;
        TextView itemColor;
    }
}
