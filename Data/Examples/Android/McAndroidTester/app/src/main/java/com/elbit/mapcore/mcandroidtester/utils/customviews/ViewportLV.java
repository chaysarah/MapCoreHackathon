/*
package com.elbit.mapcore.mcandroidtester.utils.customviews;

import android.content.Context;
import android.util.AttributeSet;
import android.view.View;
import android.widget.AdapterView;
import android.widget.LinearLayout;
import android.widget.ListView;

import com.elbit.mapcore.mcandroidtester.managers.Manager_AMCTMapForm;
import com.elbit.mapcore.mcandroidtester.ui.adapters.ViewPortsListAdapter;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

import java.util.ArrayList;

import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;

*/
/**
 * Created by TC99382 on 19/06/2017.
 *//*

public class ViewportLV extends LinearLayout{
    private OnViewportSelectedListener mOnViewportSelectedListener;
    private Context mContext;

    public ViewportLV(Context context, AttributeSet attrs) {
        super(context, attrs);
        mContext=context;
        initViewportsLV();
    }

    public void setViewportListType()
    {

    }

    private void initViewportsLV() {
        ArrayList<IMcMapViewport> viewPorts = Manager_AMCTMapForm.getInstance().getOMViewports(mContext,mObject);
        ViewPortsListAdapter viewPortsAdapter = new ViewPortsListAdapter(getActivity(), viewPorts, Consts.ListType.SINGLE_CHECK, null);
        mScreenArrangementOffsetViewportsLV.setChoiceMode(ListView.CHOICE_MODE_SINGLE);
        mScreenArrangementOffsetViewportsLV.setAdapter(null);
        mScreenArrangementOffsetViewportsLV.setAdapter(viewPortsAdapter);
        mScreenArrangementOffsetViewportsLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                IMcMapViewport curViewport = (IMcMapViewport) mScreenArrangementOffsetViewportsLV.getAdapter().getItem(position);
            }
        });
        Funcs.setListViewHeightBasedOnChildren(mScreenArrangementOffsetViewportsLV);

    }
    private IMcMapViewport getSelectedViewport() {
        int pos = mScreenArrangementOffsetViewportsLV.getCheckedItemPosition();
        if (pos == AdapterView.INVALID_POSITION)
            return null;
        else
            return (IMcMapViewport) mScreenArrangementOffsetViewportsLV.getAdapter().getItem(pos);
    }

    public interface OnViewportSelectedListener
    {
        public void onViewportSelected();
    }

    public void setOnViewPortSelectedListener(OnViewportSelectedListener onViewportSelectedListener)
    {
        this.mOnViewportSelectedListener=onViewportSelectedListener;
    }
}
*/
