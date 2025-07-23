package com.elbit.mapcore.mcandroidtester.ui.map_actions.edit_mode_properties;

import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.util.SparseBooleanArray;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AbsListView;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.ListView;

import com.elbit.mapcore.Enums.EMcPointCoordSystem;
import com.elbit.mapcore.General.CMcEnumBitField;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.General.ObjectRef;
import com.elbit.mapcore.Interfaces.Calculations.IMcSpatialQueries;
import com.elbit.mapcore.Interfaces.General.IMcEditMode;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_AMCTMapForm;
import com.elbit.mapcore.mcandroidtester.ui.adapters.KeyStepTypeAdapter;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SpinnerWithLabel;

import java.util.ArrayList;
import java.util.Arrays;

/**
 * A simple {@link Fragment} subclass.
 * Use the {@link EditModePropertiesGeneralTabFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class EditModePropertiesGeneralTabFragment extends Fragment {

    View mRootView;
    Button mSaveBtn;
    CheckBox mAutoScrollCB;
    CheckBox mDiscardChangesCB;
    NumericEditTextLabel mMarginSizeNETL;
    NumericEditTextLabel mRotatePictureOffsetNETL;
    NumericEditTextLabel mPointAndLineClickToleranceNETL;
    CheckBox mRectangleResizeRelativeToCenterCB;
    CheckBox mAutoSuppressSightPresentationCB;
    NumericEditTextLabel mLastExitStatusNETL;
    NumericEditTextLabel mWorldCoordSystemNETL;
    NumericEditTextLabel mScreenCoordSystemNETL;
    NumericEditTextLabel mImageCoordSystemNETL;
    NumericEditTextLabel mMaxNumOfItemPointsNETL;
    CheckBox mForceFinishOnMaxPointsCB;
    SpinnerWithLabel mMouseMoveUsageSWL;
    CheckBox mEnableAddingNewPointsCB;
    ListView mIntersectionTargetTypeLV;
    NumericEditTextLabel mCameraMinPitchRangeNETL;
    NumericEditTextLabel mCameraMaxPitchRangeNETL;
    NumericEditTextLabel mUtilityItemsOptionalscreenSizeNETL;
    CheckBox mUseLocalAxesAtEditingCB;
    CheckBox mKeepScaleRatioAlongDifferentCB;
    ListView mKeyStepTypeLV;
    IMcEditMode mEditMode;

    public EditModePropertiesGeneralTabFragment() {
        // Required empty public constructor
    }

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment EditModePropertiesGeneralTabFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static EditModePropertiesGeneralTabFragment newInstance() {
        EditModePropertiesGeneralTabFragment fragment = new EditModePropertiesGeneralTabFragment();
        return fragment;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        mRootView = inflater.inflate(R.layout.fragment_edit_mode_properties_general_tab, container, false);;
        InitControls();
        LoadItem();
        return mRootView;
    }

    private void InitControls()
    {
        mEditMode = Manager_AMCTMapForm.getInstance().getCurMapForm().getEditMode();
        
        mSaveBtn = (Button) mRootView.findViewById(R.id.edit_mode_properties_general_save_bttn);
        mAutoScrollCB = (CheckBox) mRootView.findViewById(R.id.emp_general_auto_scroll_cb);
        mDiscardChangesCB = (CheckBox) mRootView.findViewById(R.id.emp_general_discard_changes_cb);
        mMarginSizeNETL = (NumericEditTextLabel) mRootView.findViewById(R.id.emp_general_margin_size_net);
        mRotatePictureOffsetNETL = (NumericEditTextLabel) mRootView.findViewById(R.id.emp_general_rotate_picture_offset_net);
        mPointAndLineClickToleranceNETL = (NumericEditTextLabel) mRootView.findViewById(R.id.emp_general_point_and_line_click_tolerance_net);
        mRectangleResizeRelativeToCenterCB = (CheckBox) mRootView.findViewById(R.id.emp_general_rectangle_resize_relative_to_center_cb);
        mAutoSuppressSightPresentationCB = (CheckBox) mRootView.findViewById(R.id.emp_general_auto_suppress_sight_presentation_map_tiles_web_requests);
        mLastExitStatusNETL = (NumericEditTextLabel) mRootView.findViewById(R.id.emp_general_last_exit_status_net);
        mWorldCoordSystemNETL = (NumericEditTextLabel) mRootView.findViewById(R.id.emp_general_max_radius_world_net);
        mScreenCoordSystemNETL = (NumericEditTextLabel) mRootView.findViewById(R.id.emp_general_max_radius_screen_net);
        mImageCoordSystemNETL = (NumericEditTextLabel) mRootView.findViewById(R.id.emp_general_max_radius_image_net);
        mMaxNumOfItemPointsNETL = (NumericEditTextLabel) mRootView.findViewById(R.id.emp_general_max_num_of_points_net);
        mForceFinishOnMaxPointsCB = (CheckBox) mRootView.findViewById(R.id.emp_general_force_finish_on_max_points_cb);
        mMouseMoveUsageSWL = (SpinnerWithLabel) mRootView.findViewById(R.id.emp_general_mouse_move_usage_cb);
        mEnableAddingNewPointsCB = (CheckBox) mRootView.findViewById(R.id.emp_general_enable_edit_new_point_cb);

        mMouseMoveUsageSWL.setAdapter(new ArrayAdapter<>(getContext(), android.R.layout.simple_spinner_item, IMcEditMode.EMouseMoveUsage.values()));

        mIntersectionTargetTypeLV = (ListView) mRootView.findViewById(R.id.emp_general_intersection_target_type);
        mCameraMinPitchRangeNETL = (NumericEditTextLabel) mRootView.findViewById(R.id.emp_general_camera_min_pitch_range_net);
        mCameraMaxPitchRangeNETL = (NumericEditTextLabel) mRootView.findViewById(R.id.emp_general_camera_max_pitch_range_net);
        mUtilityItemsOptionalscreenSizeNETL = (NumericEditTextLabel) mRootView.findViewById(R.id.emp_general_utility_items_optional_net);
        mUseLocalAxesAtEditingCB = (CheckBox) mRootView.findViewById(R.id.emp_general_use_local_axes_at_editing_cb);
        mKeepScaleRatioAlongDifferentCB = (CheckBox) mRootView.findViewById(R.id.emp_general_keep_scale_ratio_along_different_cb);
        mKeyStepTypeLV = (ListView) mRootView.findViewById(R.id.emp_general_key_step_type);

        mSaveBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        SaveItem();
                    }
                });
            }
        });
    }

    private void initKeyStepType()
    {
        mKeyStepTypeLV.setAdapter(new KeyStepTypeAdapter(getContext(), mEditMode));
        Funcs.setListViewHeightBasedOnChildren(mKeyStepTypeLV);
    }

    private void initIntersectionTargetType() {
        ArrayList<IMcSpatialQueries.EIntersectionTargetType> temp = new ArrayList<>(Arrays.asList(IMcSpatialQueries.EIntersectionTargetType.values()));
        temp.remove(IMcSpatialQueries.EIntersectionTargetType.EITT_NONE);
        mIntersectionTargetTypeLV.setAdapter(new ArrayAdapter<>(getActivity(), android.R.layout.simple_list_item_multiple_choice, temp));
        mIntersectionTargetTypeLV.setChoiceMode(AbsListView.CHOICE_MODE_MULTIPLE);
        Funcs.setListViewHeightBasedOnChildren(mIntersectionTargetTypeLV);

        try
        {
            CMcEnumBitField<IMcSpatialQueries.EIntersectionTargetType> intersectionType = mEditMode.GetIntersectionTargets();

            for (int i = 0; i < mIntersectionTargetTypeLV.getCount(); i++) {
                if (intersectionType.IsSet(temp.get(i)))
                    mIntersectionTargetTypeLV.setItemChecked(i, true);
            }
        }
        catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "GetIntersectionTargets");
        }  catch (Exception e) {
            e.printStackTrace();
        }

        mIntersectionTargetTypeLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
        @Override
        public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
            if (((IMcSpatialQueries.EIntersectionTargetType) mIntersectionTargetTypeLV.getAdapter().getItem(position)).compareTo(IMcSpatialQueries.EIntersectionTargetType.EITT_NONE) == 0) {
                SparseBooleanArray checkedItems = mIntersectionTargetTypeLV.getCheckedItemPositions();
                for (int i = 0; i < checkedItems.size(); i++) {
                    if (checkedItems.valueAt(i) && i != position)
                        mIntersectionTargetTypeLV.setItemChecked(i, false);
                }
            } else
                mIntersectionTargetTypeLV.setItemChecked((IMcSpatialQueries.EIntersectionTargetType.EITT_NONE.getValue()), false);
        }
    });
}

    private void LoadItem()
    {
        //editMode functions for any map type
        try
        {
            mAutoScrollCB.setChecked(mEditMode.GetAutoScrollMode());
        }
        catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "GetAutoScrollMode");
        }  catch (Exception e) {
            e.printStackTrace();
        }

        try
        {
            mMarginSizeNETL.setInt(mEditMode.GetMarginSize());
        }
        catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "GetMarginSize");
        }  catch (Exception e) {
            e.printStackTrace();
        }

        try
        {
            mRotatePictureOffsetNETL.setFloat(mEditMode.GetRotatePictureOffset());
        }
        catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "GetRotatePictureOffset");
        }  catch (Exception e) {
            e.printStackTrace();
        }

        try
        {
            ObjectRef<Integer> MaxPoints = new ObjectRef<>();
            ObjectRef<Boolean> ForceFinish = new ObjectRef<>();
            mEditMode.GetMaxNumberOfPoints(MaxPoints, ForceFinish);
            mMaxNumOfItemPointsNETL.setUInt(MaxPoints.getValue());
            mForceFinishOnMaxPointsCB.setChecked(ForceFinish.getValue());
        }
        catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "GetMaxNumberOfPoints");
        }  catch (Exception e) {
            e.printStackTrace();
        }

        try
        {
            mWorldCoordSystemNETL.setDouble(mEditMode.GetMaxRadius(EMcPointCoordSystem.EPCS_WORLD));
            mScreenCoordSystemNETL.setDouble(mEditMode.GetMaxRadius(EMcPointCoordSystem.EPCS_SCREEN));
            mImageCoordSystemNETL.setDouble(mEditMode.GetMaxRadius(EMcPointCoordSystem.EPCS_IMAGE));
        }
        catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "GetMaxRadius");
        }  catch (Exception e) {
            e.printStackTrace();
        }

        try
        {
            mLastExitStatusNETL.setInt(mEditMode.GetLastExitStatus());
        }
        catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "GetLastExitStatus");
        }  catch (Exception e) {
            e.printStackTrace();
        }

        try
        {
            mMouseMoveUsageSWL.setSelection(mEditMode.GetMouseMoveUsageForMultiPointItem().getValue());
        }
        catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "GetMouseMoveUsageForMultiPointItem");
        }  catch (Exception e) {
            e.printStackTrace();
        }

        try
        {
            mPointAndLineClickToleranceNETL.setUInt(mEditMode.GetPointAndLineClickTolerance());
        }
        catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "GetPointAndLineClickTolerance");
        }  catch (Exception e) {
            e.printStackTrace();
        }

        try
        {
            mRectangleResizeRelativeToCenterCB.setChecked(mEditMode.GetRectangleResizeRelativeToCenter());
        }
        catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "GetRectangleResizeRelativeToCenter");
        }  catch (Exception e) {
            e.printStackTrace();
        }

        try
        {
            mAutoSuppressSightPresentationCB.setChecked(mEditMode.GetAutoSuppressQueryPresentationMapTilesWebRequests());
        }
        catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "GetAutoSuppressSightPresentationMapTilesWebRequests");
        }  catch (Exception e) {
            e.printStackTrace();
        }

        initIntersectionTargetType();
        initKeyStepType();
        try
        {
            ObjectRef<Double> minPitch = new ObjectRef<>();
            ObjectRef<Double>  maxPitch = new ObjectRef<>();
            mEditMode.GetCameraPitchRange(minPitch, maxPitch);
            mCameraMinPitchRangeNETL.setDouble(minPitch.getValue());
            mCameraMaxPitchRangeNETL.setDouble(maxPitch.getValue());
        }
        catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "GetCameraPitchRange");
        }  catch (Exception e) {
            e.printStackTrace();
        }

        try
        {
            IMcEditMode.S3DEditParams editParams = mEditMode.Get3DEditParams();
            mUtilityItemsOptionalscreenSizeNETL.setFloat(editParams.fUtilityItemsOptionalScreenSize);
            mUseLocalAxesAtEditingCB.setChecked(editParams.bLocalAxes);
            mKeepScaleRatioAlongDifferentCB.setChecked(editParams.bKeepScaleRatio);
        }
        catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "Get3DEditParams");
        }  catch (Exception e) {
            e.printStackTrace();
        }

    }
    
    public void SaveItem() {
        //editMode functions for any map type
        try {
            mEditMode.AutoScroll(mAutoScrollCB.isChecked(), mMarginSizeNETL.getUInt());
        } catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "AutoScroll");
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            mEditMode.SetMaxNumberOfPoints(mMaxNumOfItemPointsNETL.getUInt(), mForceFinishOnMaxPointsCB.isChecked());
        } catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "SetMaxNumberOfPoints");
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            mEditMode.SetMaxRadius(mWorldCoordSystemNETL.getDouble(), EMcPointCoordSystem.EPCS_WORLD);
            mEditMode.SetMaxRadius(mScreenCoordSystemNETL.getDouble(), EMcPointCoordSystem.EPCS_SCREEN);
            mEditMode.SetMaxRadius(mImageCoordSystemNETL.getDouble(), EMcPointCoordSystem.EPCS_IMAGE);
        } catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "SetMaxRadius");
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            mEditMode.SetRotatePictureOffset(mRotatePictureOffsetNETL.getFloat());
        } catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "SetRotatePictureOffset");
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            mEditMode.SetMouseMoveUsageForMultiPointItem((IMcEditMode.EMouseMoveUsage) mMouseMoveUsageSWL.getSelectedItem());
        } catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "SetMouseMoveUsageForMultiPointItem");
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            mEditMode.SetPointAndLineClickTolerance(mPointAndLineClickToleranceNETL.getUInt());
        } catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "SetPointAndLineClickTolerance");
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            for (int i = 0; i < mKeyStepTypeLV.getAdapter().getCount(); i++) {
                IMcEditMode.EKeyStepType stepType = IMcEditMode.EKeyStepType.values()[i];
                float value = (float) mKeyStepTypeLV.getAdapter().getItem(i);
                mEditMode.SetKeyStep(stepType, value);
            }
        }
        catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "SetKeyStep");
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            mEditMode.SetRectangleResizeRelativeToCenter(mRectangleResizeRelativeToCenterCB.isChecked());
        } catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "SetRectangleResizeRelativeToCenter");
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            mEditMode.SetAutoSuppressQueryPresentationMapTilesWebRequests(mAutoSuppressSightPresentationCB.isChecked());
        } catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "GetAutoSuppressSightPresentationMapTilesWebRequests");
        } catch (Exception e) {
            e.printStackTrace();
        }

        CMcEnumBitField<IMcSpatialQueries.EIntersectionTargetType> mCurrIntersectionTargetType = new CMcEnumBitField<>(IMcSpatialQueries.EIntersectionTargetType.EITT_NONE);
        SparseBooleanArray checked = mIntersectionTargetTypeLV.getCheckedItemPositions();
        for (int i = 0; i < mIntersectionTargetTypeLV.getCount(); i++) {
            if (checked.get(i)) {
                mCurrIntersectionTargetType.Set((IMcSpatialQueries.EIntersectionTargetType) mIntersectionTargetTypeLV.getItemAtPosition(i));
            }
        }
        try {
            mEditMode.SetIntersectionTargets(mCurrIntersectionTargetType);
        } catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "SetIntersectionTargets");
        } catch (Exception e) {
            e.printStackTrace();
        }

        try
        {
            Double minPitch = new Double(mCameraMinPitchRangeNETL.getDouble());
            Double maxPitch = new Double(mCameraMaxPitchRangeNETL.getDouble());
            mEditMode.SetCameraPitchRange(minPitch, maxPitch);
        }
        catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "SetCameraPitchRange");
        }  catch (Exception e) {
            e.printStackTrace();
        }

        try
        {
            IMcEditMode.S3DEditParams editParams = new IMcEditMode.S3DEditParams();

            editParams.fUtilityItemsOptionalScreenSize = mUtilityItemsOptionalscreenSizeNETL.getFloat();
            editParams.bLocalAxes = mUseLocalAxesAtEditingCB.isChecked();
            editParams.bKeepScaleRatio = mKeepScaleRatioAlongDifferentCB.isChecked();
            mEditMode.Set3DEditParams(editParams);
        }
        catch (MapCoreException mcEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "Set3DEditParams");
        }  catch (Exception e) {
            e.printStackTrace();
        }



    }
}