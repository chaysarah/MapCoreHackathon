package com.elbit.mapcore.mcandroidtester.ui.adapters;

import android.content.Context;
import androidx.fragment.app.FragmentActivity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ListView;
import android.widget.TextView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.utils.Consts;

import java.util.ArrayList;

import com.elbit.mapcore.Interfaces.Map.IMcMapTerrain;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
/**
 * Created by tc99382 on 17/05/2017.
 */
public class ViewPortsListAdapter extends BaseAdapter{
        private IMcMapTerrain mMapTerrain;
        private boolean mToShowHash;
        private ArrayList mData;
        Context mContext;
        LayoutInflater mInflater;
        int mListType;

        public ViewPortsListAdapter(Context context, ArrayList<IMcMapViewport> data, int listType) {
            mData = data;
            mContext = context;
            mInflater = (LayoutInflater) mContext.getSystemService(mContext.LAYOUT_INFLATER_SERVICE);
            mListType = listType;
        }

        public ViewPortsListAdapter(FragmentActivity context, ArrayList<IMcMapViewport> data, int listType, IMcMapTerrain mapTerrain) {
            mData=data;
            mContext = context;
            mInflater = (LayoutInflater) mContext.getSystemService(mContext.LAYOUT_INFLATER_SERVICE);
            mListType = listType;
            mMapTerrain = mapTerrain;
        }


        @Override
        public int getCount() {
            return mData.size();
        }

    @Override
    public IMcMapViewport getItem(int position) {
       return (IMcMapViewport) mData.get(position);
    }

    @Override
        public long getItemId(int arg0) {
            return 0;
        }

        public View getView(int position, View convertView, ViewGroup parent) {
            View result = null;

            if (convertView == null) {
                switch (mListType) {
                    case Consts.ListType.NON_CHECK:
                        result = LayoutInflater.from(parent.getContext()).inflate(R.layout.list_item, parent, false);
                        break;
                    case Consts.ListType.MULTIPLE_CHECK:
                        result = LayoutInflater.from(parent.getContext()).inflate(R.layout.checkable_list_item, parent, false);
                        break;
                    case Consts.ListType.SINGLE_CHECK:
                        result = LayoutInflater.from(parent.getContext()).inflate(R.layout.radio_bttn_list_item, parent, false);
                        break;
                }

            } else {
                result = convertView;
            }

            IMcMapViewport item = getItem(position);
            ((TextView) result.findViewById(R.id.list_view)).setText(item.hashCode() + "  " + item.getClass().getSimpleName());

            try {
                if (mMapTerrain != null)
                    ((ListView) parent).setItemChecked(position, mMapTerrain.GetVisibility(item));
                // ((CheckedTextView) result.findViewById(R.id.list_view)).setEnabled(mMapTerrain.GetVisibility((IMcMapViewport)item.getValue()));
            } catch (Exception e) {
                e.printStackTrace();
            }

            return result;
        }
}
