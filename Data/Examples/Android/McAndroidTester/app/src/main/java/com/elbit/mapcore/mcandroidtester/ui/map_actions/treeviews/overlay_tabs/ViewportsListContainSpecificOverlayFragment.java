package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlay_tabs;

import androidx.fragment.app.DialogFragment;
import android.os.Bundle;
import androidx.annotation.Nullable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ListView;

import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.interfaces.OnSelectViewportFromListListener;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.TilingScheme;
import com.elbit.mapcore.mcandroidtester.utils.customviews.ViewportsList;

import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlay;

/**
 * Created by tc97803 on 25/05/2017.
 */

public class ViewportsListContainSpecificOverlayFragment extends DialogFragment implements FragmentWithObject , OnSelectViewportFromListListener {

    private IMcOverlay m_Overlay;

    private View inflaterView;
    private NumericEditTextLabel mMinScaleEt;
    private NumericEditTextLabel mMaxScaleEt;
    private Button mOkBttn;
    private ViewportsList mViewportList;
    private int mActionCode;
    private IMcMapViewport mSelectedViewport;
    private TilingScheme mTilingScheme;

    public static ViewportsListContainSpecificOverlayFragment newInstance() {
        ViewportsListContainSpecificOverlayFragment fragment = new ViewportsListContainSpecificOverlayFragment();
        return fragment;
    }

    @Override
    public void setObject(Object obj) {
        m_Overlay = (IMcOverlay)obj;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(m_Overlay));
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        mActionCode = getArguments().getInt("actionCode");
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        Funcs.SetObjectFromBundle(savedInstanceState, this );
        // Inflate the layout for this fragment
        inflaterView = inflater.inflate(R.layout.fragment_viewports_list_contain_specific_overlay, container, false);
        mViewportList = (ViewportsList) inflaterView.findViewById(R.id.cv_vp_contain_specific_overlay_lv);
        mMinScaleEt = (NumericEditTextLabel) inflaterView.findViewById(R.id.vp_contain_specific_overlay_min_scale_net);
        mMinScaleEt.setFloat(1);
        mMaxScaleEt = (NumericEditTextLabel) inflaterView.findViewById(R.id.vp_contain_specific_overlay_max_scale_net);
        mMaxScaleEt.setFloat(1);
        mOkBttn =  (Button) inflaterView.findViewById(R.id.overlay_objects_select_vp_bttn);
        mOkBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                IViewportsListContainSpecificOverlayFragmentCallback callback = (IViewportsListContainSpecificOverlayFragmentCallback) getTargetFragment();
                dismiss();
                callback.callbackViewportsListContainSpecificOverlay(mSelectedViewport,
                        mMinScaleEt.getFloat(),
                        mMaxScaleEt.getFloat(),
                        mTilingScheme.getTilingScheme(),
                        mActionCode);
            }
        });
        mTilingScheme = (TilingScheme) inflaterView.findViewById(R.id.vp_contain_specific_overlay_tiling_scheme);
        if(mActionCode == Consts.SELECT_VIEWPORT_ACTION_SAVE_AS_NATIVE) {
            mTilingScheme.setVisibility(View.VISIBLE);
            mTilingScheme.SetTilingSchemeByType(IMcMapLayer.ETilingSchemeType.ETST_MAPCORE);
        }
        else
            mTilingScheme.setVisibility(View.INVISIBLE);

        return inflaterView;
    }

    @Override
    public void onViewCreated (View view, @Nullable Bundle savedInstanceState){
        super.onViewCreated(view, savedInstanceState);

        try {
            mViewportList.initViewportsList(this,
                    Consts.ListType.SINGLE_CHECK,
                    ListView.CHOICE_MODE_SINGLE,
                    m_Overlay.GetOverlayManager(), true);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onSelectViewportFromList(IMcMapViewport mcSelectedViewport, boolean isChecked) {
        if(isChecked)
            mSelectedViewport = mcSelectedViewport;
        else
            mSelectedViewport = null;
    }
}
