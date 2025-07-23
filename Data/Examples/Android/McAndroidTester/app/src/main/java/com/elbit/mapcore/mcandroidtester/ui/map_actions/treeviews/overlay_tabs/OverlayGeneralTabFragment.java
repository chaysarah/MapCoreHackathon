package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlay_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
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
import android.widget.TextView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.interfaces.OnSelectViewportFromListListener;
import com.elbit.mapcore.mcandroidtester.model.AMCTUserData;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.ConditionalSelectorByActionType;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.ViewportsList;

import java.util.ArrayList;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.General.IMcUserData;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcCollection;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcConditionalSelector;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlay;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link OverlayGeneralTabFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link OverlayGeneralTabFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class OverlayGeneralTabFragment extends Fragment implements FragmentWithObject, OnSelectViewportFromListListener {

    private OnFragmentInteractionListener mListener;
    private IMcOverlay  mOverlay;
    private View mRootView;
    private NumericEditTextLabel mOverlayId;
    private CheckBox mDetectibilityCB;
    private Button mDetectibilityBttn;
    private NumericEditTextLabel mDrawPriorityNET;
    private Button mDrawPrioritiBttn;
    private EditText mStateET;
    private Button mStateBttn;
    private Spinner mVisibilitySpinner;
    private Button mVisibilityBttn;
    private ViewportsList mViewportList;
    private CheckBox mEffectiveVisibilityInViewportCB;
    private Button mEffectiveVisibilityInSelectedVpButton;
    private TextView mOverlayManagerTV;
    private ListView mCollectionsLV;
    private EditText mUserDataEt;
    private Button mSaveBttn;
    private IMcMapViewport mSelectedViewport;

    private final IMcConditionalSelector.EActionOptions[] mcEActionOptionsValues = IMcConditionalSelector.EActionOptions.values();
    private ConditionalSelectorByActionType mOverlaySelectors;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
    * @return A new instance of fragment OverlayGeneralTabFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static OverlayGeneralTabFragment newInstance() {
        OverlayGeneralTabFragment fragment = new OverlayGeneralTabFragment();
        return fragment;
    }

    public OverlayGeneralTabFragment() {
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
        mRootView = inflater.inflate(R.layout.fragment_overlay_general_tab, container, false);
        initViews();
        return mRootView;
    }

    @Override
    public void onHiddenChanged(boolean hidden) {
        super.onHiddenChanged(hidden);
    }

    private void initViews() {
        inflateViews();
        initSaveChangesBttn();
        initOverlayId();
        initViewportOperations();
        initViewportsList();
        initOverlayManager();
        initCollections();
        initUserData();
        initConditionalSelector();
    }

    private void initSaveChangesBttn() {
        mSaveBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final int id = mOverlayId.getUInt();
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            mOverlay.SetID(id);
                        } catch (MapCoreException McEx) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), McEx, "SetID");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });

                String str = mUserDataEt.getText().toString();
                final AMCTUserData TUD = new AMCTUserData(str.getBytes());

                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            mOverlay.SetUserData(TUD);
                        } catch (MapCoreException McEx) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), McEx, "SetUserData");
                            McEx.printStackTrace();
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
                mOverlaySelectors.Save();
            }
        });
    }

    private void initConditionalSelector() {
    }

    private void initUserData() {
        try
        {
            IMcUserData UD = mOverlay.GetUserData();
            if (UD != null)
            {
                AMCTUserData TUD = (AMCTUserData)UD;
                String s = new String(TUD.getUserDataBuffer());
                mUserDataEt.setText(s);
            }
        }
        catch (MapCoreException McEx)
        {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), McEx, "GetUserData");
        }catch (Exception McEx)
        {}
    }

    private void initCollections() {
        try {
            IMcCollection[] collections=mOverlay.GetCollections();
            if(collections!=null) {
                ArrayList collectionsNames = new ArrayList();
                for (IMcCollection col : collections) {
                    collectionsNames.add(col.hashCode() + "  " + col.getClass().getSimpleName());
                }
            }
        }  catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetCollections");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initOverlayManager() {
        try {
            mOverlayManagerTV.setText(mOverlay.GetOverlayManager().hashCode()+ "  "+mOverlay.GetOverlayManager().getClass().getSimpleName());
        }  catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetOverlayManager");
        }catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initViewportsList() {
        try {
            mViewportList.initViewportsList(this,
                    Consts.ListType.SINGLE_CHECK,
                    ListView.CHOICE_MODE_SINGLE,
                    mOverlay.GetOverlayManager());
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initViewportOperations() {
        initDetectability();
        initDrawPriority();
        initOverlayState();
        initVisibility();
    }

    IMcConditionalSelector.EActionOptions mcEActionOptionsSelected;

    private void initVisibility() {
        initVisibilityValue();
        initVisibilitySpinner();
    }

    private void initVisibilityValue()
    {
        ArrayAdapter visibilityAdapter = new ArrayAdapter(getContext(), android.R.layout.simple_spinner_item, IMcConditionalSelector.EActionOptions.values());
        mVisibilitySpinner.setAdapter(visibilityAdapter);
        IMcConditionalSelector.EActionOptions mcEActionOptions;
        try {
            if (mSelectedViewport == null) {
                mcEActionOptions = mOverlay.GetVisibilityOption();
            } else {
                mcEActionOptions = mOverlay.GetVisibilityOption(mSelectedViewport);
            }
            mVisibilitySpinner.setSelection(visibilityAdapter.getPosition(mcEActionOptions));

        }catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetVisibilityOption");
            e.printStackTrace();
        }
        catch (Exception e)
        {
            e.printStackTrace();
        }
    }

    private void initVisibilitySpinner() {
        mVisibilitySpinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                mcEActionOptionsSelected = mcEActionOptionsValues[i];
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });

        mVisibilityBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            if (mSelectedViewport == null) {
                                mOverlay.SetVisibilityOption(mcEActionOptionsSelected);
                            } else {
                                mOverlay.SetVisibilityOption(mcEActionOptionsSelected, mSelectedViewport);
                            }
                        } catch (MapCoreException mcEx) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "SetVisibilityOption");
                        } catch (Exception ex) {
                            ex.printStackTrace();
                        }
                    }
                });
            }
        });
    }

    private void initOverlayState() {
        initOverlayStateValue();
        initOverlayStateBttn();
    }

    private void initOverlayStateValue()
    {
        byte[] states = null;
        try {
            if (mSelectedViewport == null) {
                states = mOverlay.GetState();
            } else {
                states = mOverlay.GetState(mSelectedViewport);
            }
            mStateET.setText(Funcs.getStateFromByteArray(states));
        }
        catch (MapCoreException mcEx)
        {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "GetState");
            mcEx.printStackTrace();
        }
        catch (Exception e){ e.printStackTrace();}
    }

    private void initOverlayStateBttn() {
        mStateBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final byte[] abyStates = Funcs.getStatesFromString(mStateET.getText().toString());

                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            if (mSelectedViewport == null) {
                                if (abyStates != null && abyStates.length == 1)
                                    mOverlay.SetState(abyStates[0]);
                                else
                                    mOverlay.SetState(abyStates);
                            } else {
                                if (abyStates != null && abyStates.length == 1)
                                    mOverlay.SetState(abyStates[0], mSelectedViewport);
                                else
                                    mOverlay.SetState(abyStates, mSelectedViewport);
                            }
                        } catch (MapCoreException mcEx) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), mcEx, "SetState");
                            mcEx.printStackTrace();
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });
    }

    private void initDrawPriority() {
        initDrawPriorityValue();
        initDrawPriorityBttn();
    }

    private void initDrawPriorityValue() {
        try {
            short drawPriority =0;
            if(mSelectedViewport == null)
                drawPriority = mOverlay.GetDrawPriority();
            else
                drawPriority = mOverlay.GetDrawPriority(mSelectedViewport);
            mDrawPriorityNET.setShort(drawPriority);

        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetDrawPriority");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initDrawPriorityBttn() {
        mDrawPrioritiBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final short drawPriority = (short) mDrawPriorityNET.getShort();
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            if (mSelectedViewport == null)
                                mOverlay.SetDrawPriority(drawPriority);
                            else
                                mOverlay.SetDrawPriority(drawPriority, mSelectedViewport);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetDrawPriority");
                            e.printStackTrace();
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });
    }

    private void initDetectability() {
        initDetectabilityValue();
        initDetectabilityBttn();
    }

    private void initDetectabilityValue() {
        try {
            boolean detectability = false;
            if (mSelectedViewport == null)
                detectability = mOverlay.GetDetectibility();
            else
                detectability = mOverlay.GetDetectibility(mSelectedViewport);
            mDetectibilityCB.setChecked(detectability);
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetDetectibility");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initDetectabilityBttn() {
        mDetectibilityBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final boolean detectability = mDetectibilityCB.isChecked();
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            if (mSelectedViewport == null)
                                mOverlay.SetDetectibility(detectability);
                            else
                                mOverlay.SetDetectibility(detectability, mSelectedViewport);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetDetectibility");
                            e.printStackTrace();
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });
    }

    private void initOverlayId() {
        try {
            mOverlayId.setUInt(mOverlay.GetID());
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetID");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void inflateViews() {
        mOverlayId = (NumericEditTextLabel) mRootView.findViewById(R.id.overlay_general_overlay_id);
        mDetectibilityCB = (CheckBox) mRootView.findViewById(R.id.overlay_general_detectibility_cb);
        mDetectibilityBttn = (Button) mRootView.findViewById(R.id.overlay_general_detectibility_bttn);
        mDrawPriorityNET = (NumericEditTextLabel) mRootView.findViewById(R.id.overlay_general_draw_priority_etl);
        mDrawPrioritiBttn = (Button) mRootView.findViewById(R.id.overlay_general_draw_priority_apply_bttn);
        mStateET = (EditText) mRootView.findViewById(R.id.overlay_general_state_et);
        mStateBttn = (Button) mRootView.findViewById(R.id.overlay_general_state_apply_bttn);
        mVisibilitySpinner = (Spinner) mRootView.findViewById(R.id.overlay_general_visibility_spinner);
        mVisibilityBttn = (Button) mRootView.findViewById(R.id.overlay_general_visibility_apply_bttn);
        mEffectiveVisibilityInViewportCB = (CheckBox) mRootView.findViewById(R.id.overlay_general_effective_visibility_in_selected_vp_cb);
        mViewportList = (ViewportsList) mRootView.findViewById(R.id.overlay_general_viewports_lv);
        mOverlayManagerTV=(TextView)mRootView.findViewById(R.id.overlay_general_overlay_manager_tv);
        mCollectionsLV=(ListView)mRootView.findViewById(R.id.overlay_general_collection_lv);
        mUserDataEt=(EditText)mRootView.findViewById(R.id.overlay_general_user_data_et);
        mSaveBttn=(Button)mRootView.findViewById(R.id.overlay_general_save_bttn);
        mOverlaySelectors =(ConditionalSelectorByActionType) mRootView.findViewById(R.id.overlay_general_conditional_selectors);
        mOverlaySelectors.setObject(mOverlay);
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
        } /*else {
            throw new RuntimeException(context.toString()
                    + " must implement OnFragmentInteractionListener");
        }*/
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    @Override
    public void setObject(Object obj) {
        mOverlay = (IMcOverlay) obj;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        if(mIsVisibleToUser)
            outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mOverlay));
    }
    private boolean mIsVisibleToUser;

    @Override
    public void setUserVisibleHint(boolean isVisibleToUser) {
        super.setUserVisibleHint(isVisibleToUser);
        mIsVisibleToUser = isVisibleToUser;
    }

    @Override
    public void onSelectViewportFromList(IMcMapViewport mcSelectedViewport, boolean isChecked) {
        if (isChecked)
            mSelectedViewport = mcSelectedViewport;
        else
            mSelectedViewport = null;

        initDetectabilityValue();
        initDrawPriorityValue();
        initOverlayStateValue();
        initVisibilityValue();

        if (mSelectedViewport != null) {
            try {
                if (mOverlay.GetEffectiveVisibilityInViewport(mSelectedViewport)) {
                    mEffectiveVisibilityInViewportCB.setChecked(true);
                } else {
                    mEffectiveVisibilityInViewportCB.setChecked(false);
                }
            } catch (MapCoreException e) {
                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetEffectiveVisibilityInViewport");
                e.printStackTrace();
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
        else
            mEffectiveVisibilityInViewportCB.setChecked(false);
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
