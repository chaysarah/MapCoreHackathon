package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.object_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentTransaction;
import android.text.Editable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.ListView;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.interfaces.OnSelectViewportFromListListener;
import com.elbit.mapcore.mcandroidtester.model.AMCTUserData;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.private_properties.PropertiesIdListFragment;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.ConditionalSelectorByActionType;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SpinnerWithLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.ViewportsList;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.General.IMcUserData;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcCollection;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcConditionalSelector;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link ObjectGeneralTabFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link ObjectGeneralTabFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class ObjectGeneralTabFragment extends Fragment implements FragmentWithObject , OnSelectViewportFromListListener {

    private OnFragmentInteractionListener mListener;
    private IMcObject mObject;
    private View mRootView;
    private CheckBox mDetectibilityCB;
    private Button mDetectibilityBttn;
    private ViewportsList mViewportsLV;
    private EditText mDrawPriorittyET;
    private Button mDrawPriorittyBttn;
    private EditText mStateET;
    private Button mStateBttn;
    private SpinnerWithLabel mVisibilitySWL;
    private Button mVisibilityBttn;
    private CheckBox mEffectiveVisibilityCB;
    private EditText mEffectiveStateET;
    private NumericEditTextLabel mObjectIdNET;
    private Button mSaveChangesBttn;
    private NumericEditTextLabel mLocationsNumNET;
    private ListView mMemberInCollectionsLV;
    private CheckBox mViewportVisibilityMaxScaleCB;
    private ConditionalSelectorByActionType mObjectSelectors;
    private EditText mUserDataEt;
    private Button mPropertiesBttn;
    private IMcMapViewport mSelectedViewport;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment ObjectGeneralTabFragment.
     */
    public static ObjectGeneralTabFragment newInstance() {
        ObjectGeneralTabFragment fragment = new ObjectGeneralTabFragment();
        return fragment;
    }

    public ObjectGeneralTabFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        Funcs.SetObjectFromBundle(savedInstanceState, this );
        mRootView = inflater.inflate(R.layout.fragment_object_general_tab, container, false);
        inflateViews();
        initViewportsLV();
        initMemberInCollectionsLV();
        initObjectId();
        initLocationsNum();
        initViewportVisibilityMaxScale();
        initUserData();
        initViews();
        initPrivatePropertiesIdList();
        initSaveBttn();
        return mRootView;
    }

    private void initPrivatePropertiesIdList() {
        mPropertiesBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                FragmentWithObject propertiesIdListFragment = PropertiesIdListFragment.newInstance();
                propertiesIdListFragment.setObject(mObject);
                FragmentTransaction transaction = getFragmentManager().beginTransaction();
                transaction.add(R.id.properties_fragment_container, (Fragment) propertiesIdListFragment, PropertiesIdListFragment.class.getSimpleName());
                transaction.addToBackStack(PropertiesIdListFragment.class.getSimpleName());
                transaction.commit();
            }
        });
    }

    private void initViewportVisibilityMaxScale() {
        try {
            boolean ignoreViewPortVisibilityMaxScale = mObject.GetIgnoreViewportVisibilityMaxScale();
            mViewportVisibilityMaxScaleCB.setChecked(ignoreViewPortVisibilityMaxScale);
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetIgnoreViewportVisibilityMaxScale");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initMemberInCollectionsLV() {
        try {
            IMcCollection[] collections = mObject.GetCollections();
            if (collections != null)
                mMemberInCollectionsLV.setAdapter(new ArrayAdapter<>(getContext(), android.R.layout.simple_list_item_1, collections));
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetCollections");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initLocationsNum() {
        int locationNum;
        try {
            mLocationsNumNET.setUInt(mObject.GetNumLocations());
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetID");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initSaveBttn() {
        mSaveChangesBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveObjectId();
                saveViewportVisibilityMaxScale();
                saveUserData();
                saveSelector();

            }
        });
    }

    private void saveSelector() {
        mObjectSelectors.Save();
    }

    private void saveUserData() {
        final String str = mUserDataEt.getText().toString();
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    AMCTUserData TUD = new AMCTUserData(str.getBytes());
                    mObject.SetUserData(TUD);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetUserData");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void saveViewportVisibilityMaxScale() {
        final boolean visibility = mViewportVisibilityMaxScaleCB.isChecked();
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    mObject.SetIgnoreViewportVisibilityMaxScale(visibility);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetIgnoreViewportVisibilityMaxScale");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void saveObjectId() {
        final int objId = mObjectIdNET.getUInt();
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    mObject.SetID(objId);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetID");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void inflateViews() {
        mViewportsLV = (ViewportsList) mRootView.findViewById(R.id.object_general_viewports_lv);

        mDetectibilityCB = (CheckBox) mRootView.findViewById(R.id.object_general_detectibility_cb);
        mDetectibilityBttn = (Button) mRootView.findViewById(R.id.object_general_detectibility_apply_bttn);

        mDrawPriorittyET = (EditText) mRootView.findViewById(R.id.object_general_draw_priority_et);
        mDrawPriorittyBttn = (Button) mRootView.findViewById(R.id.object_general_draw_priority_apply_bttn);

        mStateET = (EditText) mRootView.findViewById(R.id.object_general_state_et);
        mStateBttn = (Button) mRootView.findViewById(R.id.object_general_state_apply_bttn);

        mVisibilitySWL = (SpinnerWithLabel) mRootView.findViewById(R.id.object_general_visibility_swl);
        mVisibilityBttn = (Button) mRootView.findViewById(R.id.object_general_visibility_apply_bttn);

        mEffectiveVisibilityCB = (CheckBox) mRootView.findViewById(R.id.object_general_effective_visibility_cb);
        mEffectiveStateET = (EditText) mRootView.findViewById(R.id.object_general_effective_state_in_selected_viewport_et);

        mObjectIdNET = (NumericEditTextLabel) mRootView.findViewById(R.id.object_general_object_id_net);
        mLocationsNumNET = (NumericEditTextLabel) mRootView.findViewById(R.id.object_general_number_of_locations_net);

        mMemberInCollectionsLV = (ListView) mRootView.findViewById(R.id.object_general_member_in_collection_lv);
        mViewportVisibilityMaxScaleCB = (CheckBox) mRootView.findViewById(R.id.object_general_ignore_viewport_visibility_max_scale_cb);

        mObjectSelectors = (ConditionalSelectorByActionType) mRootView.findViewById(R.id.object_general_conditional_selectors);
        mObjectSelectors.setObject(mObject);
        mUserDataEt = (EditText) mRootView.findViewById(R.id.object_general_user_data_et);

        mPropertiesBttn = (Button) mRootView.findViewById(R.id.object_genera_object_properties_id_list_bttn);
        mSaveChangesBttn = (Button) mRootView.findViewById(R.id.object_general_save_bttn);

    }

    private void initViews() {
        initDetectibility();
        initDrawPriority();
        initState();
        initVisibility();
        initEffectiveState();
        initEffectiveVisibility();
    }

    private void initUserData() {
        try {
            IMcUserData UD = mObject.GetUserData();
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

    private void initObjectId() {
        int objectId;
        try {
            mObjectIdNET.setUInt(mObject.GetID());
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetID");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initEffectiveState() {
        String effectiveStates = "";
        if (mSelectedViewport != null) {
            byte[] states = new byte[0];
            try {
                states = mObject.GetEffectiveState(mSelectedViewport);
            } catch (MapCoreException e) {
                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetEffectiveState");
            } catch (Exception e) {
                e.printStackTrace();
            }
            effectiveStates = Funcs.getStateFromByteArray(states);
        }
        mEffectiveStateET.setText(effectiveStates);

    }

    private void initEffectiveVisibility() {
        boolean isEffectiveVisibilityChecked = false;
        try {
            if(mSelectedViewport!= null)
                isEffectiveVisibilityChecked = mObject.GetEffectiveVisibilityInViewport(mSelectedViewport);
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetEffectiveVisibilityInViewport");
        } catch (Exception e) {
            e.printStackTrace();
        }
        mEffectiveVisibilityCB.setChecked(isEffectiveVisibilityChecked);
    }

    private void initVisibility() {
        mVisibilitySWL.setAdapter(new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, IMcConditionalSelector.EActionOptions.values()));
        IMcConditionalSelector.EActionOptions selectedOption;
        try {
            if (mSelectedViewport == null)
                selectedOption = mObject.GetVisibilityOption();
            else
                selectedOption = mObject.GetVisibilityOption(mSelectedViewport);
            mVisibilitySWL.setSelection(selectedOption.getValue());
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetVisibilityOption");
        } catch (Exception e) {
            e.printStackTrace();
        }
        mVisibilityBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

               final IMcConditionalSelector.EActionOptions selectedOption = (IMcConditionalSelector.EActionOptions) mVisibilitySWL.getSelectedItem();

                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            if (mSelectedViewport == null)
                                mObject.SetVisibilityOption(selectedOption);
                            else
                                mObject.SetVisibilityOption(selectedOption, mSelectedViewport);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetVisibilityOption");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });
    }

    private void initState() {
        byte[] states = new byte[0];
        String statesAsString = "";
        try {
            if (mSelectedViewport == null)
                states = mObject.GetState();
            else
                states = mObject.GetState(mSelectedViewport);
            if (states != null && states.length > 0) {
                statesAsString = Funcs.getStateFromByteArray(states);
            }
            mStateET.setText(statesAsString.trim());
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetState");
        } catch (Exception e) {
            e.printStackTrace();
        }
        mStateBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final Editable editable = mStateET.getText();
                final byte[] states = Funcs.getStatesFromString(String.valueOf(editable));
                    Funcs.runMapCoreFunc(new Runnable() {
                        @Override
                        public void run() {
                            try {
                                if (mSelectedViewport != null) {
                                    if (states != null && states.length == 1)
                                        mObject.SetState(states[0], mSelectedViewport);
                                    else
                                        mObject.SetState(states, mSelectedViewport);
                                } else {
                                    if (states != null && states.length == 1)
                                        mObject.SetState(states[0]);
                                    else
                                        mObject.SetState(states);
                                }
                            } catch (MapCoreException e) {
                                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetState");
                            } catch (Exception e) {
                                e.printStackTrace();
                            }
                        }
                    });
                }
           // }
        });
    }

    private void initDrawPriority() {
        try {
            int drawPriority;
            if (mSelectedViewport == null)
                drawPriority = mObject.GetDrawPriority();
            else
                drawPriority = mObject.GetDrawPriority(mSelectedViewport);
            mDrawPriorittyET.setText(String.valueOf(drawPriority));
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetDrawPriority");
        } catch (Exception e) {
            e.printStackTrace();
        }
        mDrawPriorittyBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            IMcMapViewport curViewport = mSelectedViewport;
                            short curDrawPriority = Short.valueOf(String.valueOf(mDrawPriorittyET.getText()));
                            if (curViewport == null)
                                mObject.SetDrawPriority(curDrawPriority);
                            else
                                mObject.SetDrawPriority(curDrawPriority, curViewport);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetDrawPriority");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });
    }

    private void initDetectibility() {
        boolean isDetectibilityChecked = false;
        try {
            if (mSelectedViewport == null)
                isDetectibilityChecked = mObject.GetDetectibility();
            else
                isDetectibilityChecked = mObject.GetDetectibility(mSelectedViewport);
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetDetectibility");
        } catch (Exception e) {
            e.printStackTrace();
        }
        mDetectibilityCB.setChecked(isDetectibilityChecked);
        mDetectibilityBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final boolean isChecked = mDetectibilityCB.isChecked();
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            mObject.SetDetectibility(isChecked, mSelectedViewport);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetDetectibility");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });

            }
        });
    }

    private void initViewportsLV() {
        try {
            mViewportsLV.initViewportsList(this,
                    Consts.ListType.SINGLE_CHECK,
                    ListView.CHOICE_MODE_SINGLE,
                    mObject.GetOverlayManager());
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onSelectViewportFromList(IMcMapViewport mcSelectedViewport, boolean isChecked) {
        if(isChecked)
            mSelectedViewport = mcSelectedViewport;
        else {
            mSelectedViewport = null;
        }
        initViews();
    }

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
        }/* else {
            throw new RuntimeException(context.toString()
                    + " must implement OnFragmentInteractionListener");
        }*/
        if (context instanceof MapsContainerActivity) {
            ((MapsContainerActivity) context).mCurFragmentTag = ObjectLocationsTabsFragment.class.getSimpleName();

        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    @Override
    public void setObject(Object obj) {
        mObject = (IMcObject) obj;
        if(mObjectSelectors != null)
            mObjectSelectors.setObject(mObject);
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mObject));
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
