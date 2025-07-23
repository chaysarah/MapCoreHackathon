package com.elbit.mapcore.mcandroidtester.utils.customviews;


import android.content.Context;
import androidx.fragment.app.DialogFragment;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentActivity;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.ListView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.interfaces.OnCreateCoordinateSystemListener;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_MCGridCoordinateSystem;
import com.elbit.mapcore.mcandroidtester.ui.adapters.HashMapAdapter;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.GridCoordinateSystemDetailsFragment;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

import java.util.Map;

import com.elbit.mapcore.Interfaces.Calculations.GridCoordSystem.IMcGridCoordinateSystem;

/*import android.app.Fragment;
import android.support.v4.app.DialogFragment;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentActivity;
import android.support.v4.app.FragmentManager;*/

/**
 * TODO: document your custom view class.
 */
public class GridCoordinateSystemList extends LinearLayout {

    private ListView mGridCoordSysLV;
    private HashMapAdapter mGridCoordSysAdapter;
    private View mView;
    private Context mContext;
    private Fragment mFragment;
    private boolean mIsReadonly = false;
    private Button mRefreshButton;

    public void SetIsReadonly(boolean isReadonly) {
        mIsReadonly = isReadonly;
    }

    public GridCoordinateSystemList(Context context) {
        super(context);
        InflateLayout(context);
    }

    public GridCoordinateSystemList(Context context, AttributeSet attrs, int defStyleAttr, int defStyleRes) {
        super(context, attrs, defStyleAttr, defStyleRes);
        InflateLayout(context);
    }

    public GridCoordinateSystemList(Context context, AttributeSet attrs) {
        super(context, attrs);
        InflateLayout(context);
    }

    private void InflateLayout(Context context) {
        LayoutInflater inflater = (LayoutInflater) context
                .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        mView = inflater.inflate(R.layout.cv_grid_coordinate_system_list, this);
        mGridCoordSysLV = (ListView) mView.findViewById(R.id.cv_grid_coordinate_system_lv);
        mRefreshButton = (Button) mView.findViewById(R.id.refresh_coord_sys_bttn);
        mRefreshButton.setOnClickListener(new OnClickListener() {
            @Override
            public void onClick(View view) {
                LoadGridCoordinateSystemList();
            }
        });
        mContext = context;
        LoadGridCoordinateSystemList();
    }

    public void SetFragment(Fragment fragment) {
        mFragment = fragment;
    }

    public void LoadGridCoordinateSystemList() {
        mGridCoordSysAdapter = new HashMapAdapter(null, Manager_MCGridCoordinateSystem.getInstance().getdGridCoordSys(), Consts.ListType.SINGLE_CHECK);
        if (Manager_MCGridCoordinateSystem.getInstance().getdGridCoordSys() != null) {
            mGridCoordSysLV.setChoiceMode(ListView.CHOICE_MODE_SINGLE);
            mGridCoordSysLV.setAdapter(mGridCoordSysAdapter);
            mGridCoordSysLV.setOnItemLongClickListener(new AdapterView.OnItemLongClickListener() {
                @Override
                public boolean onItemLongClick(AdapterView<?> adapterView, View view, int i, long l) {
                    Map.Entry<Object, Integer> item = (Map.Entry<Object, Integer>) mGridCoordSysLV.getAdapter().getItem(i);
                    String className = GridCoordinateSystemDetailsFragment.class.getName();
                    DialogFragment gridCoordinateSystemDetailsFragment = GridCoordinateSystemDetailsFragment.newInstance("", "");
                    gridCoordinateSystemDetailsFragment.show(((FragmentActivity) mContext).getSupportFragmentManager(), className);
                    ((FragmentWithObject) gridCoordinateSystemDetailsFragment).setObject(item.getKey());

                    return false;
                }
            });

            mGridCoordSysLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                @Override
                public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                    Map.Entry<Object, Integer> item = (Map.Entry<Object, Integer>) mGridCoordSysLV.getAdapter().getItem(position);
                    //show the selected coordinate system corresponding params (datum etc.)
                    if (!mIsReadonly) {
                        if (mFragment != null) {
                            if (mFragment instanceof OnCreateCoordinateSystemListener)
                                ((OnCreateCoordinateSystemListener) mFragment).onCoordSysCreated((IMcGridCoordinateSystem) item.getKey());
                        }
                    }


                }
            });
        }
        mGridCoordSysLV.deferNotifyDataSetChanged();
        Funcs.setListViewHeightBasedOnChildren(mGridCoordSysLV);
    }

    public ListView getGridCoordSys() {
        return mGridCoordSysLV;
    }

    public void SetEnabled(boolean isEnabled) {
        mGridCoordSysLV.setEnabled(isEnabled);
    }
    public void setClickable(boolean isClickable) {
        mGridCoordSysLV.setClickable(isClickable);
    }
    public void setFocusable (boolean isFocusable) {
        mGridCoordSysLV.setFocusable(isFocusable);
    }

    public void setChoiceMode(int choiceMode) {
        mGridCoordSysLV.setChoiceMode(choiceMode);
    }

    public void selectCurrGridCoordSys(IMcGridCoordinateSystem coordSysType) {
        int i;
        for (i = 0; i < mGridCoordSysAdapter.getCount(); i++) {
            if (mGridCoordSysAdapter.getItem(i).getKey().equals(coordSysType))
                mGridCoordSysLV.setItemChecked(i, true);
        }
    }
}
