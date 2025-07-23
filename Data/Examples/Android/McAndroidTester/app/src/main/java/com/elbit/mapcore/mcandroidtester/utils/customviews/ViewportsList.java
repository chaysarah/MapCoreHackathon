package com.elbit.mapcore.mcandroidtester.utils.customviews;

import android.content.Context;
import androidx.fragment.app.Fragment;

import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.CheckedTextView;
import android.widget.LinearLayout;
import android.widget.ListView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.OnSelectViewportFromListListener;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_AMCTMapForm;
import com.elbit.mapcore.mcandroidtester.ui.adapters.ViewPortsListAdapter;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

import java.lang.reflect.Method;
import java.util.ArrayList;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;

/**
 * Created by TC97803 on 29/08/2017.
 */

public class ViewportsList extends LinearLayout {
    private ListView mViewportsLV;
    private ArrayList<IMcMapViewport> mViewportList;
    private ArrayList<Boolean> mIsCheckedByVp;
    private Button mViewportListClearBttn;
    public IMcMapViewport mSelectedViewport = null;
    private Fragment mContainerFragment;

    public IMcMapViewport GetSelectedViewport()
    {
        return mSelectedViewport;
    }
    public ArrayList<IMcMapViewport> GetViewportOfOM() { return mViewportList; }

    public ViewportsList(Context context, AttributeSet attrs) {
        super(context, attrs);
        LayoutInflater inflater = (LayoutInflater) context
                .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        View view = inflater.inflate(R.layout.cv_viewports_list, this);
        mViewportsLV = (ListView ) view.findViewById(R.id.cv_viewports_lv);
        mViewportListClearBttn = (Button) view.findViewById(R.id.cv_viewport_list_clear_bttn);
        if (!isInEditMode()) {}
    }

    public void initViewportsList(Fragment containerFragment, int listType, int choiceMode) {
        initViewportsList(containerFragment, listType, choiceMode, null);
    }

    public void initViewportsList(Fragment containerFragment, int listType, int choiceMode,
                                  IMcOverlayManager mcOverlayManager) {
        initViewportsList(containerFragment, listType, choiceMode, mcOverlayManager, null,null);
    }

    public void initViewportsList(Fragment containerFragment, int listType, int choiceMode,
                                  IMcOverlayManager mcOverlayManager, boolean isNotShowClearButton) {
        initViewportsList(containerFragment, listType, choiceMode, mcOverlayManager, null, null);
        if (isNotShowClearButton)
            mViewportListClearBttn.setVisibility(INVISIBLE);
    }

    public void initViewportsList(Fragment containerFragment, final int listType, int choiceMode,
                                  IMcOverlayManager mcOverlayManager,
                                  Object mcObjectMethod,
                                  Method method, boolean isNotShowClearButton)
    {
        initViewportsList(containerFragment, listType, choiceMode, mcOverlayManager, mcObjectMethod, method);
        if (isNotShowClearButton)
            mViewportListClearBttn.setVisibility(INVISIBLE);
    }

    public void initViewportsList(Fragment containerFragment, final int listType, int choiceMode,
                                  IMcOverlayManager mcOverlayManager,
                                  Object mcObjectMethod,
                                  Method method) {

        mContainerFragment = containerFragment;
        mViewportList = new ArrayList<>();
        if (method != null) {
            mIsCheckedByVp = new ArrayList<>();
            mViewportListClearBttn.setVisibility(INVISIBLE);
        }
        ArrayList<IMcMapViewport> viewports = Manager_AMCTMapForm.getInstance().getAllImcViewports();

        for (IMcMapViewport vp : viewports) {
            try {
                IMcMapViewport currVp = null;
                if (mcOverlayManager == null)
                    currVp = vp;
                else if (vp.GetOverlayManager() == mcOverlayManager)
                    currVp = vp;

                if (currVp != null) {
                    mViewportList.add(currVp);
                    if (method != null) {
                        Boolean result = (Boolean)method.invoke(mcObjectMethod, vp);
                        mIsCheckedByVp.add(result.booleanValue());
                    }
                }
            } catch (MapCoreException e) {
                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetOverlayManager/" + mcObjectMethod.toString());
            } catch (Exception e) {
                e.printStackTrace();
            }
        }

        final ViewPortsListAdapter viewPortsListAdapter = new ViewPortsListAdapter(
                getContext(),
                mViewportList,
                listType);

        mViewportsLV.setAdapter(viewPortsListAdapter);
        mViewportsLV.setChoiceMode(choiceMode);
        Funcs.setListViewHeightBasedOnChildren(mViewportsLV);

        if(mIsCheckedByVp != null && mIsCheckedByVp.size() > 0)
        {
            for(int i=0;i<mIsCheckedByVp.size();i++)
                mViewportsLV.setItemChecked(i,mIsCheckedByVp.get(i));
        }

        mViewportsLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int index, long l) {
                IMcMapViewport item = viewPortsListAdapter.getItem(index);
                if (((CheckedTextView) view).isChecked())
                    SetSelectedViewport(item, true);
                else
                {
                    if (listType == Consts.ListType.SINGLE_CHECK)
                        SetSelectedViewport(null, false);
                    else if (listType == Consts.ListType.MULTIPLE_CHECK)
                        SetSelectedViewport(item, false);
                }
            }
        });

        mViewportListClearBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                SetSelectedViewport(null, false);
                mViewportsLV.setItemChecked(-1, true);
            }
        });
    }

    private void SetSelectedViewport(IMcMapViewport selectedViewport, boolean isChecked)
    {
        mSelectedViewport = selectedViewport;
        if(mContainerFragment instanceof OnSelectViewportFromListListener)
            ((OnSelectViewportFromListListener) mContainerFragment).onSelectViewportFromList(mSelectedViewport,isChecked);
    }
}
