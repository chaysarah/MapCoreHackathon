package com.elbit.mapcore.mcandroidtester.ui.objects.fragments.properties_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.util.SparseBooleanArray;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.Spinner;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.ui.objects.ObjectPropertiesBase;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SpinnerWithLabel;

import java.util.ArrayList;

import com.elbit.mapcore.Enums.EMcPointCoordSystem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcConditionalSelector;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcSymbolicItem;
import com.elbit.mapcore.Structs.SMcFVector2D;
import com.elbit.mapcore.Structs.SMcSubItemData;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link ObjectPropertiesTabsGeneralFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link ObjectPropertiesTabsGeneralFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class ObjectPropertiesTabsGeneralFragment extends Fragment {
    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;
    private View mFragmentView;
    private OnFragmentInteractionListener mListener;

    private Spinner mDrawPriorityGroupSpinner;
    private IMcSymbolicItem.EDrawPriorityGroup mDrawPriorityGroupValue;
    private final IMcSymbolicItem.EDrawPriorityGroup[] eDrawPriorityGroupValues = IMcSymbolicItem.EDrawPriorityGroup.values();
    private NumericEditTextLabel mMinLineTextureHeightRange;
    private NumericEditTextLabel mMaxLineTextureHeightRange;
    private NumericEditTextLabel mLocationMaxPoints;
    private Button mSaveBttn;
    private ListView mItemSubTypeBitFieldLV;
    private CheckBox mLocationRelativeToDtm;
    private SpinnerWithLabel mLocationCoordinateSystem;
    private SpinnerWithLabel mVisibilitySWL;
    private SpinnerWithLabel mTransformSWL;
    private EditText mBlockedTransparency;
    private EditText mSubItemsDataIds;
    private EditText mSubItemsDataIndexes;
    private CheckBox mGridUsingBasicItemPropertiesOnly;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment ObjectPropertiesTabsGeneralFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static ObjectPropertiesTabsGeneralFragment newInstance(String param1, String param2) {
        ObjectPropertiesTabsGeneralFragment fragment = new ObjectPropertiesTabsGeneralFragment();
        Bundle args = new Bundle();
        args.putString(ARG_PARAM1, param1);
        args.putString(ARG_PARAM2, param2);
        fragment.setArguments(args);
        return fragment;
    }

    public ObjectPropertiesTabsGeneralFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if (getArguments() != null) {
            mParam1 = getArguments().getString(ARG_PARAM1);
            mParam2 = getArguments().getString(ARG_PARAM2);
        }
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        mFragmentView = inflater.inflate(R.layout.fragment_object_properties_tabs_general, container, false);
        initViews();
        return mFragmentView;
    }

    private void initViews() {
        inflateViews();
        initSubItemsData();
        initVisibilityOption();
        initTransformOption();
        initBlockedTransparency();
        initItemSubTypeBitField();
        initLocationCoordinateSys();
        initLocationParams();
        initDrawPriorityGroupSpinner();
        initTextureHeightRange();
        initSaveBttn();

    }

    private void initSubItemsData() {
        ArrayList<SMcSubItemData> subItemsData = ObjectPropertiesBase.mSubItemsData;
        for (SMcSubItemData itemData : subItemsData) {
            mSubItemsDataIds.getText().append(String.valueOf(itemData.uSubItemID) + " ");
            mSubItemsDataIndexes.getText().append(String.valueOf(itemData.nPointsStartIndex) + " ");
        }

    }

    private void initBlockedTransparency() {
        mBlockedTransparency.setText(String.valueOf(ObjectPropertiesBase.mBlockedTransparency));
    }

    private void initTransformOption() {
        mTransformSWL.setAdapter(new ArrayAdapter<>(getContext(), android.R.layout.simple_spinner_item, IMcConditionalSelector.EActionOptions.values()));
        mTransformSWL.setSelection(ObjectPropertiesBase.mTransformOption.ordinal());
    }

    private void initVisibilityOption() {
        mVisibilitySWL.setAdapter(new ArrayAdapter<>(getContext(), android.R.layout.simple_spinner_item, IMcConditionalSelector.EActionOptions.values()));
        mVisibilitySWL.setSelection(ObjectPropertiesBase.mVisibilityOption.ordinal());
    }

    private void initLocationCoordinateSys() {
        mLocationCoordinateSystem.setAdapter(new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, EMcPointCoordSystem.values()));
        mLocationCoordinateSystem.setSelection(ObjectPropertiesBase.mLocationCoordSys.ordinal()/*getValue()*/);

    }

    private void initLocationParams() {
        mLocationRelativeToDtm.setChecked(ObjectPropertiesBase.mLocationRelativeToDtm);
        mLocationMaxPoints.setInt(ObjectPropertiesBase.mLocationMaxPoints);

    }

    private void initItemSubTypeBitField() {
        mItemSubTypeBitFieldLV.setAdapter(new ArrayAdapter<>(getActivity(), android.R.layout.simple_list_item_multiple_choice, IMcObjectSchemeItem.EItemSubTypeFlags.values()));
        SparseBooleanArray checked = ObjectPropertiesBase.getItemSubTypeFlags();
        for (int i = 0; i < checked.size(); i++) {
            mItemSubTypeBitFieldLV.setItemChecked(i, checked.valueAt(i));
        }
        //mItemSubTypeBitFieldLV.setSelection(ObjectPropertiesBase.mItemSubTypeFlags.ordinal());
        //Funcs.setListViewHeightBasedOnChildren(mItemSubTypeBitFieldLV);
    }

    private void initSaveBttn() {
        mSaveBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveSubItemsData();
                saveVisibilityOption();
                saveTransformOption();
                saveBlockedTransparency();
                saveLocationCoordinateSys();
                saveItemSubTypeBitField();
                saveLocationParams();
                saveDrawPriorityGroup();
                saveTextureHeightRange();
                ObjectPropertiesBase.Grid_IsUsingBasicItemPropertiesOnly = mGridUsingBasicItemPropertiesOnly.isChecked();
            }
        });
    }

    private void saveSubItemsData() {
        String[] subItemsDataIdsStringArr = Funcs.splitToStringArr(String.valueOf(mSubItemsDataIds.getText()), " ");
        String[] subItemsDataIndexesStringArr = Funcs.splitToStringArr(String.valueOf(mSubItemsDataIndexes.getText()), " ");
        if (subItemsDataIdsStringArr.length != subItemsDataIndexesStringArr.length) {
            AlertMessages.ShowGenericMessage(getContext(), "sub items data error", "Id's number different from points start index number\nChange input data and save again");
        } else {
            if (subItemsDataIdsStringArr.length > 0 && subItemsDataIndexesStringArr.length > 0) {
                ObjectPropertiesBase.mSubItemsData = new ArrayList<>();
                for (int i = 0; i < subItemsDataIdsStringArr.length; i++) {
                    try {
                        Integer subItemId = Integer.parseInt(subItemsDataIdsStringArr[i]);
                        Integer pointStartIndex = Integer.parseInt(subItemsDataIndexesStringArr[i]);
                        ObjectPropertiesBase.mSubItemsData.add(new SMcSubItemData(subItemId, pointStartIndex));
                    } catch (NumberFormatException numberFormatEx) {
                        AlertMessages.ShowErrorMessage(getContext(), String.valueOf(numberFormatEx.getMessage()), "Sub Items Data-invalid input");
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            } else if (subItemsDataIdsStringArr.length == 0 && subItemsDataIndexesStringArr.length == 0)
                ObjectPropertiesBase.mSubItemsData.clear();
        }
    }

    private void saveBlockedTransparency() {
        ObjectPropertiesBase.mBlockedTransparency = Byte.valueOf(String.valueOf(mBlockedTransparency.getText()));
    }

    private void saveTransformOption() {
        ObjectPropertiesBase.mTransformOption = (IMcConditionalSelector.EActionOptions) mTransformSWL.getSelectedItem();
    }

    private void saveVisibilityOption() {
        ObjectPropertiesBase.mVisibilityOption = (IMcConditionalSelector.EActionOptions) mVisibilitySWL.getSelectedItem();
    }

    private void saveLocationCoordinateSys() {
        ObjectPropertiesBase.mLocationCoordSys = (EMcPointCoordSystem) mLocationCoordinateSystem.getSelectedItem();
    }

    private void saveLocationParams() {
        ObjectPropertiesBase.mLocationRelativeToDtm = mLocationRelativeToDtm.isChecked();
        ObjectPropertiesBase.mLocationMaxPoints = mLocationMaxPoints.getInt();
    }

    private void saveItemSubTypeBitField() {
        ObjectPropertiesBase.setItemSubTypeFlags(mItemSubTypeBitFieldLV.getCheckedItemPositions());
    }

    private void saveTextureHeightRange() {
        ObjectPropertiesBase.mLineTextureHeightRange = new SMcFVector2D(mMinLineTextureHeightRange.getFloat(), mMaxLineTextureHeightRange.getFloat());
    }

    private void saveDrawPriorityGroup() {
        ObjectPropertiesBase.mDrawPriorityGroup = mDrawPriorityGroupValue;
    }

    private void initTextureHeightRange() {
        mMinLineTextureHeightRange.setFloat(ObjectPropertiesBase.mLineTextureHeightRange.x);
        mMaxLineTextureHeightRange.setFloat(ObjectPropertiesBase.mLineTextureHeightRange.y);
    }

    private void inflateViews() {

        mBlockedTransparency = (EditText) mFragmentView.findViewById(R.id.object_general_blocked_transparency);
        mVisibilitySWL = (SpinnerWithLabel) mFragmentView.findViewById(R.id.visibility_option_swl);
        mTransformSWL = (SpinnerWithLabel) mFragmentView.findViewById(R.id.transform_option_swl);
        mItemSubTypeBitFieldLV = (ListView) mFragmentView.findViewById(R.id.object_properties_item_sub_type_bit_field_lv);
        mLocationCoordinateSystem = (SpinnerWithLabel) mFragmentView.findViewById(R.id.object_properties_location_coord_sys_swl);
        mLocationRelativeToDtm = (CheckBox) mFragmentView.findViewById(R.id.object_properties_location_relative_to_dtm_cb);
        mLocationMaxPoints = (NumericEditTextLabel) mFragmentView.findViewById(R.id.object_properties_location_max_points);
        SpinnerWithLabel SWL = (SpinnerWithLabel) mFragmentView.findViewById(R.id.draw_priority_group_swl);
        mDrawPriorityGroupSpinner = (Spinner) SWL.findViewById(R.id.spinner_in_cv);
        mMinLineTextureHeightRange = (NumericEditTextLabel) mFragmentView.findViewById(R.id.min_texture_height_range);
        mMaxLineTextureHeightRange = (NumericEditTextLabel) mFragmentView.findViewById(R.id.max_texture_height_range);
        mSubItemsDataIds = (EditText) mFragmentView.findViewById(R.id.object_general_sub_items_data_ids);
        mSubItemsDataIndexes = (EditText) mFragmentView.findViewById(R.id.object_general_sub_items_data_start_indexes);
        mGridUsingBasicItemPropertiesOnly = (CheckBox) mFragmentView.findViewById(R.id.object_general_grid_Is_Using_Basic_Item_Properties_Only);
        mGridUsingBasicItemPropertiesOnly.setChecked(ObjectPropertiesBase.Grid_IsUsingBasicItemPropertiesOnly);

        mSaveBttn = (Button) mFragmentView.findViewById(R.id.object_properties_tabs_general_save_bttn);
    }

    private void initDrawPriorityGroupSpinner() {

        mDrawPriorityGroupSpinner.setAdapter(new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, eDrawPriorityGroupValues));
        mDrawPriorityGroupSpinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                mDrawPriorityGroupValue = eDrawPriorityGroupValues[position];
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });
        mDrawPriorityGroupSpinner.setSelection(ObjectPropertiesBase.mDrawPriorityGroup.ordinal()/*getValue()*/);
    }

    // TODO: Rename method, update argument and hook method into UI event
    public void onButtonPressed(Uri uri) {
        if (mListener != null) {
            mListener.onFragmentInteraction(uri);
        }
    }

    @Override
    public void onAttach(Context context) {
        super.onAttach(context);
        if (context instanceof OnFragmentInteractionListener) {
            mListener = (OnFragmentInteractionListener) context;
        } else {
            // throw new RuntimeException(context.toString()
            //         + " must implement OnFragmentInteractionListener");
        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    /**
     * This interface must be implemented by activities that contain this
     * fragment to allow an interaction in this fragment to be communicated
     * to the activity and potentially other fragments contained in that
     * activity.
     * <p/>
     * See the Android Training lesson <a href=
     * "http://developer.android.com/training/basics/fragments/communicating.html"
     * >Communicating with Other Fragments</a> for more information.
     */
    public interface OnFragmentInteractionListener {
        // TODO: Update argument type and name
        void onFragmentInteraction(Uri uri);
    }
}
