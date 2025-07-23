package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlay_tabs;

import android.os.Bundle;
import androidx.annotation.Nullable;
import androidx.fragment.app.DialogFragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.ListView;

import com.elbit.mapcore.Enums.EGeometry;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlay;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.interfaces.OnSelectViewportFromListListener;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SpinnerWithLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.ViewportsList;

/**
 * Created by tc97803 on 25/05/2017.
 */

public class SaveObjectsAsRawVectorFragment extends DialogFragment implements FragmentWithObject , OnSelectViewportFromListListener {

    private IMcOverlay m_Overlay;

    private View inflaterView;
    private NumericEditTextLabel mCameraYawAngleEt;
    private NumericEditTextLabel mCameraScaleEt;
    private NumericEditTextLabel mLayerNameEt;
    private SpinnerWithLabel mGeometryFilterSWL;
    private CheckBox mAsMemoryBufferCB;
    private Button mOkBttn;
    private ViewportsList mViewportList;
    private int mActionCode;
    private IMcMapViewport mSelectedViewport;
    private IMcObject[] mSelectedObjects;

    public static SaveObjectsAsRawVectorFragment newInstance() {
        SaveObjectsAsRawVectorFragment fragment = new SaveObjectsAsRawVectorFragment();
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
        inflaterView = inflater.inflate(R.layout.fragment_save_objects_as_raw_vector, container, false);
        mViewportList = (ViewportsList) inflaterView.findViewById(R.id.cv_save_as_raw_vp_lv);
        mCameraYawAngleEt = (NumericEditTextLabel) inflaterView.findViewById(R.id.save_as_raw_camera_yaw_angle_netl);
        mCameraScaleEt = (NumericEditTextLabel) inflaterView.findViewById(R.id.save_as_raw_camera_scale_netl);
        mLayerNameEt = (NumericEditTextLabel) inflaterView.findViewById(R.id.save_as_raw_layer_name_netl);
        mGeometryFilterSWL = (SpinnerWithLabel) inflaterView.findViewById(R.id.save_as_raw_geometry_filter_swl);
        mAsMemoryBufferCB = (CheckBox) inflaterView.findViewById(R.id.save_as_raw_as_memory_buffer_cb);
        mOkBttn =  (Button) inflaterView.findViewById(R.id.save_as_raw_ok_btn);

        mGeometryFilterSWL.setAdapter(new ArrayAdapter<>(getContext(), android.R.layout.simple_spinner_item, EGeometry.values()));
      //  mGeometryFilterSWL.setSelection(EGeometry.UnSupportedGeometry.getValue());

        mOkBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                ISaveObjectsAsRawVectorOverlayFragmentCallback callback = (ISaveObjectsAsRawVectorOverlayFragmentCallback) getTargetFragment();
                dismiss();
                callback.callbackSaveObjectsAsRawVectorOverlay(
                        mSelectedViewport,
                        mCameraYawAngleEt.getFloat(),
                        mCameraScaleEt.getFloat(),
                        mLayerNameEt.getText(),
                        mAsMemoryBufferCB.isChecked(),
                        (EGeometry) mGeometryFilterSWL.getSelectedItem(),
                        mActionCode);
            }
        });

       /* mTilingScheme = (TilingScheme) inflaterView.findViewById(R.id.vp_contain_specific_overlay_tiling_scheme);
        if(mActionCode == Consts.SELECT_VIEWPORT_ACTION_SAVE_AS_NATIVE) {
            mTilingScheme.setVisibility(View.VISIBLE);
            mTilingScheme.SetTilingSchemeByType(IMcMapLayer.ETilingSchemeType.ETST_MAPCORE);
        }
        else
            mTilingScheme.setVisibility(View.INVISIBLE);*/

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
