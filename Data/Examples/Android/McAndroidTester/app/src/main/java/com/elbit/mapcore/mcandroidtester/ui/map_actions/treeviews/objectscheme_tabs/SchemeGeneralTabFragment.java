package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.objectscheme_tabs;

import android.content.DialogInterface;
import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentTransaction;
import androidx.appcompat.app.AlertDialog;
import android.util.SparseBooleanArray;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AbsListView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.Toast;

import com.elbit.mapcore.General.CMcEnumBitField;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.General.IMcUserData;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcConditionalSelector;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectScheme;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeNode;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcProperty;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.interfaces.OnSelectViewportFromListListener;
import com.elbit.mapcore.mcandroidtester.managers.Manager_MCNames;
import com.elbit.mapcore.mcandroidtester.model.AMCTUserData;
import com.elbit.mapcore.mcandroidtester.ui.adapters.ObjectStateModifierAdapter;
import com.elbit.mapcore.mcandroidtester.ui.adapters.StateNameAdapter;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.private_properties.PropertiesIdListFragment;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SpinnerWithLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.ViewportsList;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;

/**
 * A simple {@link Fragment} subclass.
 * Use the {@link SchemeGeneralTabFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class SchemeGeneralTabFragment extends Fragment implements FragmentWithObject, OnSelectViewportFromListListener {

    private View mRootView;
    private IMcObjectScheme mcObjectScheme;
    private NumericEditTextLabel mSchemeID;
    private NumericEditTextLabel mNumberOfLocation;

    private NumericEditTextLabel mGetLocationIndexByID;
    private NumericEditTextLabel mLocationIndex;
    private Button mGetLocationIndexByIDBtn;

    private NumericEditTextLabel mGetNodeByID;
    private NumericEditTextLabel mGetNodeByName;
    private NumericEditTextLabel mNodeName;
    private Button mGetNodeByIDBtn;
    private Button mGetNodeByNameBtn;

    private CheckBox mTerrainItemsConsistency;
    private CheckBox mGroupingItemsByDrawPriorityWithinObjects;
    private Button mPropertiesBttn;
    private SpinnerWithLabel mObjectRotationItemSWL;
    private SpinnerWithLabel mObjectScreenArrangementSWL;
    private SpinnerWithLabel mEditModeDefaultItemSWL;
    private Button mObjectRotationItemBtn;
    private Button mObjectScreenArrangementBtn;
    private Button mEditModeDefaultItemBtn;
    private EditText mUserDataEt;

    private ArrayList m_lstSchemeValue;
    private ArrayList m_lstSchemeText;

    private NumericEditTextLabel mStateNum;
    private NumericEditTextLabel mStateName;
    private NumericEditTextLabel mStateForAllObjects;
    private Button mStateForAllObjectsBtn;

    private ListView mTerrainObjectsConsiderationFlagsLV;
    private Button mTerrainObjectsClearBtn;
    private Button mTerrainObjectsApplyBtn;

    private ListView mStateNamesLV;
    private StateNameAdapter mStateNamesAdapter;
    private ArrayList<StateNameRow> mStateNameRowData;
     private Button mtateNamesBtn;
    private Button mGetStateByNameBtn;
    private Button mGetNameByStateBtn;
    private ViewportsList mViewportsLV;
    private IMcMapViewport mSelectedViewport;

    private NumericEditTextLabel mPropertyId;
    private NumericEditTextLabel mPropertyType;
    private CheckBox mNoFailOnNotExist;
    private Button mGetPropertyTypeBtn;
    private CheckBox mIgnoreUpdating;
    private Button mIgnoreUpdatingBtn;

    private ListView mObjectModifierStateLV;
    private IMcObjectScheme.SObjectStateModifier[] mObjectStateModifierData;
    private ArrayList<IMcObjectScheme.SObjectStateModifier> mObjectStateModifierLst;

    private ObjectStateModifierAdapter mObjectStateModifierAdapter = null;
    private Button mModifierAddBtn;
    private Button mModifierEditBtn;
    private Button mModifierInsertBtn;
    private Button mModifierRemoveBtn;
    private Button mModifierClearBtn;
    private Button mModifierApplyBtn;

    private ArrayList m_lstModifiersValue;
    private ArrayList m_lstModifiersText;
    private Button mSaveChangesBttn;

    public SchemeGeneralTabFragment() {
        // Required empty public constructor
    }

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment SchemeGeneralTabFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static SchemeGeneralTabFragment newInstance() {
        SchemeGeneralTabFragment fragment = new SchemeGeneralTabFragment();
        return fragment;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        mRootView = inflater.inflate(R.layout.fragment_scheme_general_tab, container, false);
        inflateViews();
        loadView();
        loadStateNames();
        initSaveBttn();
        return mRootView;
    }

    @Override
    public void setObject(Object obj) {
        mcObjectScheme = (IMcObjectScheme) obj;
    }

    private void inflateViews() {
        mSchemeID = (NumericEditTextLabel) mRootView.findViewById(R.id.scheme_general_scheme_id_net);
        mNumberOfLocation = (NumericEditTextLabel) mRootView.findViewById(R.id.scheme_general_number_of_locations_net);

        mGetLocationIndexByID = (NumericEditTextLabel) mRootView.findViewById(R.id.scheme_general_node_id_net);
        mLocationIndex = (NumericEditTextLabel) mRootView.findViewById(R.id.scheme_general_location_index_net);
        mGetLocationIndexByIDBtn = (Button) mRootView.findViewById(R.id.scheme_general_location_index_bttn);

        mGetNodeByID = (NumericEditTextLabel) mRootView.findViewById(R.id.scheme_general_get_node_by_id_net);
        mGetNodeByName= (NumericEditTextLabel) mRootView.findViewById(R.id.scheme_general_get_node_by_name_net);
        mNodeName = (NumericEditTextLabel) mRootView.findViewById(R.id.scheme_general_get_node_name_net);

        mTerrainItemsConsistency = (CheckBox) mRootView.findViewById(R.id.scheme_general_terrain_items_consistency_cb);
        mGroupingItemsByDrawPriorityWithinObjects = (CheckBox) mRootView.findViewById(R.id.scheme_general_grouping_items_by_draw_priority_within_objects_cb);

        mGetNodeByIDBtn = (Button) mRootView.findViewById(R.id.scheme_general_get_node_by_id_bttn);
        mGetNodeByNameBtn = (Button) mRootView.findViewById(R.id.scheme_general_get_node_by_name_bttn);

        mObjectRotationItemSWL = (SpinnerWithLabel) mRootView.findViewById(R.id.scheme_general_scheme_rotation_items);
        mObjectScreenArrangementSWL = (SpinnerWithLabel) mRootView.findViewById(R.id.scheme_general_scheme_screen_arrangement_items);
        mEditModeDefaultItemSWL = (SpinnerWithLabel) mRootView.findViewById(R.id.scheme_general_scheme_edit_mode_default_items);
        mPropertiesBttn = (Button) mRootView.findViewById(R.id.scheme_general_scheme_properties_id_list_bttn);
        mSaveChangesBttn = (Button) mRootView.findViewById(R.id.scheme_general_save_bttn);

        mObjectRotationItemBtn = (Button) mRootView.findViewById(R.id.scheme_general_scheme_set_rotation_items);
        mObjectScreenArrangementBtn = (Button) mRootView.findViewById(R.id.scheme_general_scheme_set_screen_arrangement_items);
        mEditModeDefaultItemBtn = (Button) mRootView.findViewById(R.id.scheme_general_scheme_set_edit_mode_default_items);
        mUserDataEt = (EditText) mRootView.findViewById(R.id.scheme_general_user_data_et);

        mStateNamesLV = (ListView) mRootView.findViewById(R.id.scheme_general_state_names_lv);
        mtateNamesBtn = (Button) mRootView.findViewById(R.id.scheme_general_set_state_names_bttn);
        mStateNum = (NumericEditTextLabel) mRootView.findViewById(R.id.scheme_general_state_net);
        mStateName = (NumericEditTextLabel) mRootView.findViewById(R.id.scheme_general_state_name_net);
        mStateForAllObjects = (NumericEditTextLabel) mRootView.findViewById(R.id.scheme_general_state_all_objects_net);
        mGetStateByNameBtn = (Button) mRootView.findViewById(R.id.scheme_general_get_state_by_name_bttn);
        mGetNameByStateBtn = (Button) mRootView.findViewById(R.id.scheme_general_get_name_by_state_bttn);
        mViewportsLV = (ViewportsList) mRootView.findViewById(R.id.scheme_general_state_viewports_lv);
        mStateForAllObjectsBtn = (Button) mRootView.findViewById(R.id.scheme_general_state_all_objects_apply_btn);

        mPropertyId = (NumericEditTextLabel) mRootView.findViewById(R.id.scheme_general_property_id_net);
        mPropertyType = (NumericEditTextLabel) mRootView.findViewById(R.id.scheme_general_property_type_net);
        mGetPropertyTypeBtn = (Button) mRootView.findViewById(R.id.scheme_general_get_property_type_bttn);
        mNoFailOnNotExist = (CheckBox) mRootView.findViewById(R.id.scheme_general_no_fail_on_not_exist_cb);

        mIgnoreUpdatingBtn = (Button) mRootView.findViewById(R.id.scheme_general_ignore_updating_bttn);
        mIgnoreUpdating = (CheckBox) mRootView.findViewById(R.id.scheme_general_ignore_updating_cb);

        mTerrainObjectsConsiderationFlagsLV = (ListView) mRootView.findViewById(R.id.scheme_general_terrain_objects_consideration_flags_lv);
        mTerrainObjectsClearBtn =  (Button) mRootView.findViewById(R.id.scheme_general_terrain_objects_clear_btn);
        mTerrainObjectsApplyBtn =  (Button) mRootView.findViewById(R.id.scheme_general_terrain_objects_apply_btn);

        mObjectModifierStateLV = (ListView) mRootView.findViewById(R.id.object_modifier_state_lv);
        mModifierAddBtn = (Button) mRootView.findViewById(R.id.object_modifier_state_add_btn);
        mModifierEditBtn = (Button) mRootView.findViewById(R.id.object_modifier_state_edit_btn);
        mModifierInsertBtn = (Button) mRootView.findViewById(R.id.object_modifier_state_insert_btn);
        mModifierRemoveBtn = (Button) mRootView.findViewById(R.id.object_modifier_state_remove_btn);
        mModifierClearBtn = (Button) mRootView.findViewById(R.id.object_modifier_state_clear_btn);
        mModifierApplyBtn = (Button) mRootView.findViewById(R.id.object_modifier_state_apply_btn);
    }

    private void loadView()
    {
        try {
            mSchemeID.setUInt(mcObjectScheme.GetID());
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetID");
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            mNumberOfLocation.setUInt(mcObjectScheme.GetNumObjectLocations());
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetNumObjectLocations");
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            mTerrainItemsConsistency.setChecked(mcObjectScheme.GetTerrainItemsConsistency());
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetTerrainItemsConsistency");
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            mGroupingItemsByDrawPriorityWithinObjects.setChecked(mcObjectScheme.GetGroupingItemsByDrawPriorityWithinObjects());
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetGroupingItemsByDrawPriorityWithinObjects");
        } catch (Exception e) {
            e.printStackTrace();
        }

        initGetLocationIndexByID();
        initGetNodeByIDOrName();
        initPrivatePropertiesIdList();
        initObjectSchemeOperations();
        initUserData();
        initGetPropertyType();
        initIgnoreUpdating();
        initTerrainObjectsConsiderationFlags();
        initModifiersBtn();
    }

    private void initModifiersBtn() {

        mObjectStateModifierLst = new ArrayList<>();

        mModifierRemoveBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                int index = GetSelectedModifier();
                if (index >= 0) {
                    mObjectStateModifierLst.remove(index);
                    LoadModifiers();
                    EnableButtonAfterSelectModifier(false);
                }
            }
        });
        mModifierAddBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                openModifierDialog();
            }
        });

        mModifierEditBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                int index = GetSelectedModifier();
                if (index >= 0) {
                    openModifierDialog(mObjectStateModifierLst.get(index));
                }
            }
        });
        mModifierInsertBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                int index = GetSelectedModifier();
                if (index >= 0) {
                    openModifierDialog(index);
                }
            }
        });

        mModifierClearBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                mObjectStateModifierAdapter = null;
               LoadModifiers();
            }
        });

        mModifierApplyBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                try {
                    if (mObjectStateModifierLst != null){
                        mObjectStateModifierData = mObjectStateModifierLst.toArray(new IMcObjectScheme.SObjectStateModifier[0]);
                    }

                    mcObjectScheme.SetObjectStateModifiers(mObjectStateModifierData);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetObjectStateModifiers");
                } catch (Exception e) {
                    e.printStackTrace();
                }
             }
        });
    }
    private void openModifierDialog()
    {
        openModifierDialog(null, -1);
    }
    private void openModifierDialog(IMcObjectScheme.SObjectStateModifier modifier)
    {
        openModifierDialog(modifier, -1);
    }
    private void openModifierDialog(int index)
    {
        openModifierDialog(null, index);
    }
    private void openModifierDialog(final IMcObjectScheme.SObjectStateModifier modifier,final int index) {

        m_lstModifiersValue = new ArrayList();
        m_lstModifiersText = new ArrayList();

        m_lstModifiersValue.add(null);
        m_lstModifiersText.add("None");

        final View dialogView =  getActivity().getLayoutInflater().inflate(R.layout.modifiers, null);
        final SpinnerWithLabel conditionalSelectorSWL = (SpinnerWithLabel) dialogView.findViewById(R.id.modifiers_conditional_selector_swl);
        final NumericEditTextLabel stateNETL = (NumericEditTextLabel) dialogView.findViewById(R.id.modifiers_state_netl);
        final CheckBox actionOnResultCTV =  (CheckBox) dialogView.findViewById(R.id.modifiers_action_on_result_cb);

        IMcConditionalSelector[] conditionalSelectors = null;
        int indexConditionalSelector = 0;
        int i=1;
        try {
            conditionalSelectors = mcObjectScheme.GetOverlayManager().GetConditionalSelectors();
            if (conditionalSelectors != null && conditionalSelectors.length > 0) {
                for (IMcConditionalSelector conditionalSelector : conditionalSelectors) {
                    String name = Manager_MCNames.getInstance().getNameByObject(conditionalSelector);
                    if(modifier != null && modifier.pConditionalSelector == conditionalSelector)
                        indexConditionalSelector = i;
                    m_lstModifiersText.add(name);
                    m_lstModifiersValue.add(conditionalSelector);
                    i++;
                }

                conditionalSelectorSWL.setSpinner(m_lstModifiersText.toArray(), indexConditionalSelector);
            }
        }
        catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetConditionalSelectors");
        } catch (Exception e) {
            e.printStackTrace();
        }

        if(modifier != null)
        {
            stateNETL.setByte(modifier.uObjectState);
            actionOnResultCTV.setChecked(modifier.bActionOnResult);
            dialogView.setTag(modifier);
        }

        AlertDialog.Builder builder = new AlertDialog.Builder(getContext());
        builder.setTitle("Modifiers").setView(dialogView)
                .setCancelable(false)
                .setPositiveButton("Ok", new DialogInterface.OnClickListener() {

                    @Override
                    public void onClick(DialogInterface dialog, int id) {
                        IMcConditionalSelector conditionalSelector = (IMcConditionalSelector) m_lstModifiersValue.get(conditionalSelectorSWL.getSelectedItemPosition());
                        IMcObjectScheme.SObjectStateModifier newModifier = null;
                        if(modifier != null)
                            newModifier = modifier;
                        else
                            newModifier = new IMcObjectScheme.SObjectStateModifier();
                        newModifier.uObjectState = stateNETL.getByte();
                        newModifier.pConditionalSelector = conditionalSelector;
                        newModifier.bActionOnResult = actionOnResultCTV.isChecked();
                        if(modifier == null) {
                            if(index>= 0)
                                mObjectStateModifierLst.add(index, newModifier);
                            else
                                mObjectStateModifierLst.add(newModifier);
                        }
                        LoadModifiers();
                    }
                });
        builder.show();
    }

    public void EnableButtonAfterSelectModifier(boolean isSelected)
    {
        mModifierRemoveBtn.setEnabled(isSelected);
        mModifierEditBtn.setEnabled(isSelected);
        mModifierInsertBtn.setEnabled(isSelected);
        mModifierClearBtn.setEnabled(isSelected);
        mModifierAddBtn.setEnabled(!isSelected);
    }

    private int GetSelectedModifier() {
        return mObjectStateModifierAdapter.mSelectedIndex;
    }

    private void initTerrainObjectsConsiderationFlags() {

        IMcObjectScheme.ETerrainObjectsConsiderationFlags[] values = IMcObjectScheme.ETerrainObjectsConsiderationFlags.values();
        IMcObjectScheme.ETerrainObjectsConsiderationFlags[] values2 = new IMcObjectScheme.ETerrainObjectsConsiderationFlags[values.length - 1];
        int i = 0;
        for (IMcObjectScheme.ETerrainObjectsConsiderationFlags val : values) {
            if (val != IMcObjectScheme.ETerrainObjectsConsiderationFlags.ETOCF_NONE) {
                values2[i] = val;
                i++;
            }
        }
        mTerrainObjectsConsiderationFlagsLV.setAdapter(new ArrayAdapter<>(getActivity(), android.R.layout.simple_list_item_multiple_choice, values2));
        mTerrainObjectsConsiderationFlagsLV.setChoiceMode(AbsListView.CHOICE_MODE_MULTIPLE);
        Funcs.setListViewHeightBasedOnChildren(mTerrainObjectsConsiderationFlagsLV);

        try {
            CMcEnumBitField<IMcObjectScheme.ETerrainObjectsConsiderationFlags> flags = mcObjectScheme.GetTerrainObjectsConsideration();
            if (flags != null) {
                for (int j = 0; j < mTerrainObjectsConsiderationFlagsLV.getCount(); j++) {
                    if (flags.IsSet((IMcObjectScheme.ETerrainObjectsConsiderationFlags) mTerrainObjectsConsiderationFlagsLV.getAdapter().getItem(j))) {
                        mTerrainObjectsConsiderationFlagsLV.setItemChecked(j, true);
                    }
                }
            }
        }
        catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetTerrainObjectsConsideration");
        } catch (Exception e) {
            e.printStackTrace();
        }

        mTerrainObjectsClearBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                for (int i = 0; i < mTerrainObjectsConsiderationFlagsLV.getCount(); i++) {
                    mTerrainObjectsConsiderationFlagsLV.setItemChecked(i, false);
                }
            }
        });

        mTerrainObjectsApplyBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                CMcEnumBitField<IMcObjectScheme.ETerrainObjectsConsiderationFlags> eTerrainObjectsConsiderationFlags = new CMcEnumBitField<>(IMcObjectScheme.ETerrainObjectsConsiderationFlags.ETOCF_NONE);
                SparseBooleanArray checked = null;
                int len = mTerrainObjectsConsiderationFlagsLV.getCount();
                checked = mTerrainObjectsConsiderationFlagsLV.getCheckedItemPositions();
                for (int i = 0; i < len; i++) {
                    if (checked.get(i)) {
                        eTerrainObjectsConsiderationFlags.Set((IMcObjectScheme.ETerrainObjectsConsiderationFlags) mTerrainObjectsConsiderationFlagsLV.getItemAtPosition(i));
                    }
                }

                final CMcEnumBitField<IMcObjectScheme.ETerrainObjectsConsiderationFlags> finalTerrainObjectsConsiderationFlags = eTerrainObjectsConsiderationFlags;
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            mcObjectScheme.SetTerrainObjectsConsideration(finalTerrainObjectsConsiderationFlags);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetTerrainObjectsConsideration");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });
    }

    private void initIgnoreUpdating()
    {
        try {
            mIgnoreUpdating.setChecked(IMcObjectScheme.Static.GetIgnoreUpdatingNonExistentProperty());
        } /*catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetIgnoreUpdatingNonExistentProperty");
        } */catch (Exception e) {
            e.printStackTrace();
        }

        mIgnoreUpdatingBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            IMcObjectScheme.Static.SetIgnoreUpdatingNonExistentProperty(mIgnoreUpdating.isChecked());
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetIgnoreUpdatingNonExistentProperty");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });

            }
        });
    }

    private void initGetPropertyType()
    {
        mGetPropertyTypeBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                try {
                    IMcProperty.EPropertyType propertyType = mcObjectScheme.GetPropertyType(mPropertyId.getUInt(), mNoFailOnNotExist.isChecked());
                    mPropertyType.setText(propertyType.name());
                }  catch (MapCoreException e) {
                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetID");
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
            }
        });
    }

    private void initViewportsLV() {
        try {
            mViewportsLV.initViewportsList(this,
                    Consts.ListType.SINGLE_CHECK,
                    ListView.CHOICE_MODE_SINGLE,
                     mcObjectScheme.GetOverlayManager());
        }catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetOverlayManager");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void loadStateNames() {

        initViewportsLV();

        mGetStateByNameBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                try
                {
                    if (!mStateName.getText().isEmpty())
                        mStateNum.setInt(mcObjectScheme.GetObjectStateByName(mStateName.getText()));
                    else
                    {
                        Toast.makeText(getContext(), "Please insert State name", Toast.LENGTH_LONG).show();
                        mStateNum.setText("");
                    }
                }
                catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetObjectStateByName");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });

        mGetNameByStateBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                try
                {
                    mStateName.setText(mcObjectScheme.GetObjectStateName(mStateNum.getByte()));
                }
                catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetObjectStateName");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });

        mStateForAllObjectsBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final byte[] abyStates = Funcs.getStatesFromString(mStateForAllObjects.getText());
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            if (mSelectedViewport != null) {
                                if (abyStates != null && abyStates.length == 1)
                                    mcObjectScheme.SetObjectsState(abyStates[0], mSelectedViewport);
                                else
                                    mcObjectScheme.SetObjectsState(abyStates, mSelectedViewport);
                            } else {
                                if (abyStates != null && abyStates.length == 1)
                                    mcObjectScheme.SetObjectsState(abyStates[0]);
                                else
                                    mcObjectScheme.SetObjectsState(abyStates);
                            }
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IMcOverlayManager.SetObjectsState");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });

        mStateNameRowData = new ArrayList<>();
        String name = "";
        try {
            for (int i = 1; i <= 255; i++) {
                name = mcObjectScheme.GetObjectStateName((byte) i);
                if (name != null && !name.isEmpty()) {
                    StateNameRow row = new StateNameRow();
                    row.State = (byte) i;
                    row.StateName = name;

                    mStateNameRowData.add(row);
                }
                if (i == 255)
                    break;
            }
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetObjectStateName");
        } catch (Exception e) {
            e.printStackTrace();
        }
        if (mStateNameRowData.size() != 255) {
            StateNameRow row = new StateNameRow();
            mStateNameRowData.add(row);
        }
        /*if(mStateNameRowData.size() == 1) {
            for (int i = 0; i < 3; i++) {
                StateNameRow row = new StateNameRow();
                mStateNameRowData.add(row);
            }
        }*/
        mStateNamesAdapter = new StateNameAdapter(getContext(), mStateNameRowData, this);
        mStateNamesLV.setAdapter(mStateNamesAdapter);
        Funcs.setListViewHeightBasedOnChildren(mStateNamesLV);

        mtateNamesBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final HashMap<Byte, String> map = new HashMap<>();
                for (int i = 0; i < mStateNamesLV.getCount(); i++) {
                    // get rows from list
                    EditText stateET = (EditText) mStateNamesLV.getChildAt(i).findViewById(R.id.state_row_et);
                    EditText stateNameET = (EditText) mStateNamesLV.getChildAt(i).findViewById(R.id.state_name_row_et);
                    String stateStr = String.valueOf(stateET.getText());
                    String stateNameStr = String.valueOf(stateNameET.getText());

                    if (stateStr != null && !stateStr.isEmpty() && stateNameStr != null && !stateNameStr.isEmpty()) {
                        final byte state = Byte.parseByte(stateStr);
                        //final String stateName = String.valueOf(stateNameET.getText());
                        map.put(state, stateNameStr);
                    }
                }
                for (int i = 0; i <= 255; i++) {
                    if (map.containsKey((byte) i)) {
                        final int index = i;
                        Funcs.runMapCoreFunc(new Runnable() {
                            @Override
                            public void run() {
                                try {
                                    mcObjectScheme.SetObjectStateName(map.get((byte) index), (byte) index);
                                } catch (MapCoreException e) {
                                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetObjectStateName");
                                } catch (Exception e) {
                                    e.printStackTrace();
                                }
                            }
                        });
                    }
                    if (i == 255)
                        break;
                }
            }
        });

        try {
           /* IMcConditionalSelector[] selectors = mcObjectScheme.GetOverlayManager().GetConditionalSelectors();
            if(selectors != null && selectors.length> 0)
            {
                IMcObjectScheme.SObjectStateModifier modifier = new IMcObjectScheme.SObjectStateModifier();
                modifier.pConditionalSelector = selectors[0];
                modifier.uObjectState = 0;
                IMcObjectScheme.SObjectStateModifier[] modifiers = new IMcObjectScheme.SObjectStateModifier[1];
                modifiers[0] = modifier;
                mcObjectScheme.SetObjectStateModifiers(modifiers);
            }*/
            mObjectStateModifierData = mcObjectScheme.GetObjectStateModifiers();

            //ArrayList<IMcObjectScheme.SObjectStateModifier> list = (ArrayList<IMcObjectScheme.SObjectStateModifier>) Arrays.asList(mObjectStateModifierData);
            if(mObjectStateModifierData != null && mObjectStateModifierData.length > 0) {
                mObjectStateModifierLst = new ArrayList<>(Arrays.asList(mObjectStateModifierData));
                LoadModifiers();
            }
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetObjectStateModifiers");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public void LoadModifiers()
    {
        /*if(mObjectStateModifierAdapter != null) {
            mObjectModifierStateLV.setAdapter(null);
           // mObjectStateModifierAdapter.clearData();
            mObjectStateModifierAdapter.notifyDataSetChanged();
        }
        mObjectStateModifierAdapter = new ObjectStateModifierAdapter(getActivity(), mObjectStateModifierLst, this);
        mObjectModifierStateLV.setAdapter(mObjectStateModifierAdapter);
*/
        if(mObjectStateModifierAdapter == null) {
            EnableButtonAfterSelectModifier(false);
            mObjectStateModifierAdapter = new ObjectStateModifierAdapter(getActivity(), mObjectStateModifierLst, this);
            mObjectModifierStateLV.setAdapter(mObjectStateModifierAdapter);
        }
        mObjectStateModifierAdapter.notifyDataSetChanged();
        Funcs.setListViewHeightBasedOnChildren(mObjectModifierStateLV);
        EnableButtonAfterSelectModifier(false);
    }

    public void AddEmptyRow() {
        int size = mStateNamesLV.getCount();
        if (size > 0) {
            View getChildAt = mStateNamesLV.getChildAt(size - 1);
            if (getChildAt != null) {
                EditText stateET = (EditText) getChildAt.findViewById(R.id.state_row_et);
                String stateStr = String.valueOf(stateET.getText());
                if (stateStr != null && !stateStr.isEmpty()) {
                    mStateNameRowData.add(new StateNameRow());
                    mStateNamesAdapter.notifyDataSetChanged();
                    Funcs.setListViewHeightBasedOnChildren(mStateNamesLV);
                }
            }
        }
    }

    private int getItemPosition(IMcObjectSchemeItem item) {
        int i = 1;
        for (Object _item : m_lstSchemeValue) {
            if (((IMcObjectSchemeItem) _item) == item)
                return i;
            i++;
        }
        return 0;
    }

    private void initGetLocationIndexByID()
    {
        mGetLocationIndexByIDBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
               try {
                    int locationIndex = mcObjectScheme.GetObjectLocationIndexByID(mGetLocationIndexByID.getUInt());
                    mLocationIndex.setUInt(locationIndex);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetObjectLocationIndexByID");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void initGetNodeByIDOrName()
    {
        mGetNodeByIDBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                try {
                    IMcObjectSchemeNode node = mcObjectScheme.GetNodeByID(mGetNodeByID.getUInt());
                    if(node != null)
                        mNodeName.setText(Manager_MCNames.getInstance().getIdByObject(node));
                    else
                        Toast.makeText(getContext(), "Not exist node with ID " + mGetNodeByID.getText(), Toast.LENGTH_LONG).show();

                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetObjectLocationIndexByID");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });

        mGetNodeByNameBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                try {
                    IMcObjectSchemeNode node = mcObjectScheme.GetNodeByName(mGetNodeByName.getText());
                    if(node != null)
                        mNodeName.setText(Manager_MCNames.getInstance().getIdByObject(node));
                    else
                        Toast.makeText(getContext(), "Not exist node with name " + mGetNodeByName.getText(), Toast.LENGTH_LONG).show();
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetObjectLocationIndexByID");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void initObjectSchemeOperations()
    {
        m_lstSchemeValue = new ArrayList();
        m_lstSchemeText = new ArrayList();

        m_lstSchemeValue.add(null);
        m_lstSchemeText.add("None");

        try {
            IMcObjectSchemeNode[] schemeNodeArr = mcObjectScheme.GetNodes(new CMcEnumBitField<>(IMcObjectSchemeNode.ENodeKindFlags.ENKF_ANY_ITEM));
            for (IMcObjectSchemeNode schemeNode : schemeNodeArr)
            {
                String name = Manager_MCNames.getInstance().getNameByObject(schemeNode);

                m_lstSchemeText.add(name);
                m_lstSchemeValue.add(schemeNode);

                mObjectRotationItemSWL.setSpinner(m_lstSchemeText.toArray(),0);
                mObjectScreenArrangementSWL.setSpinner(m_lstSchemeText.toArray(),0);
                mEditModeDefaultItemSWL.setSpinner(m_lstSchemeText.toArray(),0);
            }
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetNodes");
        } catch (Exception e) {
            e.printStackTrace();
        }
         try {
          IMcObjectSchemeItem item = mcObjectScheme.GetObjectRotationItem();
          mObjectScreenArrangementSWL.setSelection(getItemPosition(item));
    } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetObjectRotationItem");
        } catch (Exception e) {
            e.printStackTrace();
        }
        try {
          IMcObjectSchemeItem item = mcObjectScheme.GetObjectScreenArrangementItem();
          mEditModeDefaultItemSWL.setSelection(getItemPosition(item));
    } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetObjectScreenArrangementItem");
        } catch (Exception e) {
            e.printStackTrace();
        }
        try {
          IMcObjectSchemeItem item = mcObjectScheme.GetEditModeDefaultItem();
          mObjectRotationItemSWL.setSelection(getItemPosition(item));
    } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetEditModeDefaultItem");
        } catch (Exception e) {
            e.printStackTrace();
        }

        mObjectRotationItemBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                int position = mObjectRotationItemSWL.getSelectedItemPosition();
                if(position>=0 && position< m_lstSchemeValue.size())
                {
                    final IMcObjectSchemeItem item = (IMcObjectSchemeItem) m_lstSchemeValue.get(position);
                    Funcs.runMapCoreFunc(new Runnable() {
                        @Override
                        public void run() {
                            try{
                                mcObjectScheme.SetObjectRotationItem(item);
                            } catch (MapCoreException e) {
                                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetObjectRotationItem");
                            } catch (Exception e) {
                                e.printStackTrace();
                            }
                        }
                    });

                }
            }
        });

        mObjectScreenArrangementBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                int position = mObjectScreenArrangementSWL.getSelectedItemPosition();
                if(position>=0 && position< m_lstSchemeValue.size()) {
                    final IMcObjectSchemeItem item = (IMcObjectSchemeItem) m_lstSchemeValue.get(position);
                     Funcs.runMapCoreFunc(new Runnable() {
                        @Override
                        public void run() {
                            try{
                                mcObjectScheme.SetObjectScreenArrangementItem(item);
                            } catch (MapCoreException e) {
                                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetObjectScreenArrangementItem");
                            } catch (Exception e) {
                                e.printStackTrace();
                            }
                        }
                    });
                }
            }
        });

        mEditModeDefaultItemBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                int position = mEditModeDefaultItemSWL.getSelectedItemPosition();
                if(position>=0 && position< m_lstSchemeValue.size()) {
                    final IMcObjectSchemeItem item = (IMcObjectSchemeItem) m_lstSchemeValue.get(position);
                     Funcs.runMapCoreFunc(new Runnable() {
                        @Override
                        public void run() {
                            try{
                                mcObjectScheme.SetEditModeDefaultItem(item);
                            } catch (MapCoreException e) {
                                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetEditModeDefaultItem");
                            } catch (Exception e) {
                                e.printStackTrace();
                            }
                        }
                    });
                }
            }
        });
    }

    private void saveUserData() {
        final String str = mUserDataEt.getText().toString();
         Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    AMCTUserData TUD = new AMCTUserData(str.getBytes());
                    mcObjectScheme.SetUserData(TUD);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetUserData");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void initUserData() {
        try {
            IMcUserData UD = mcObjectScheme.GetUserData();
            if (UD != null) {
                AMCTUserData TUD = (AMCTUserData) UD;
                String s = new String(TUD.getUserDataBuffer());
                mUserDataEt.setText(s);
            }
        } catch (MapCoreException McEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), McEx, "GetUserData");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initPrivatePropertiesIdList() {
        final Fragment fragment = this;
        mPropertiesBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                FragmentWithObject propertiesIdListFragment = PropertiesIdListFragment.newInstance();
                propertiesIdListFragment.setObject(mcObjectScheme);
                FragmentTransaction transaction = getFragmentManager().beginTransaction();
                transaction.add(R.id.scheme_properties_fragment_container, (Fragment) propertiesIdListFragment, PropertiesIdListFragment.class.getSimpleName());
                transaction.addToBackStack(PropertiesIdListFragment.class.getSimpleName());
                transaction.commit();
            }
        });
    }

    private void initSaveBttn() {
        mSaveChangesBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveUserData();
            }
        });
    }

    @Override
    public void onSelectViewportFromList(IMcMapViewport mcSelectedViewport, boolean isChecked) {
        if(isChecked)
            mSelectedViewport = mcSelectedViewport;
        else
            mSelectedViewport = null;
    }
}