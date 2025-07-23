package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlaymanager_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.ExpandableListView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.TextView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.interfaces.OnSelectViewportFromListListener;
import com.elbit.mapcore.mcandroidtester.ui.adapters.SizeFactorAdapter;
import com.elbit.mapcore.mcandroidtester.ui.adapters.ViewPortsObjectsELAdapter;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.ViewportsList;

import java.util.ArrayList;

import com.elbit.mapcore.General.CMcEnumBitField;
import com.elbit.mapcore.General.IMcErrors;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link OverlayManagerSizeFactorFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link OverlayManagerSizeFactorFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class OverlayManagerSizeFactorFragment extends Fragment implements FragmentWithObject, OnSelectViewportFromListListener {

    private OnFragmentInteractionListener mListener;

    private ExpandableListView mViewPortsObjectsELV;
    private ViewPortsObjectsELAdapter mViewPortsObjectsAdapter;
    private View mRootView;
    private ViewportsList mViewportsLV;
    private ListView mObjectsLV;
    private Button mObjectsApplyBttn;
    private Button mObjectsSelectAllBttn;
    private Button mObjectsRemoveSelectionBttn;
    private Button mObjectsSetSelectedSizeFactorBttn;
    private EditText mObjectsSetSelectedSizeFactorET;
    private ListView mVectorItemsLV;
    private Button mVectorItemsApplyBttn;
    private Button mVectorItemsSelectAllBttn;
    private Button mVectorItemsRemoveSelectionBttn;
    private Button mVectorItemsSetSelectedSizeFactorBttn;
    private EditText mVectorItemsSetSelectedSizeFactorET;
    private IMcOverlayManager mOverlayManager;
    private ArrayList<SizeFactorRow> mObjectsSizeFactorData;
    private ArrayList<SizeFactorRow> mVectorItemSizeFactorData;
    private IMcMapViewport mSelectedViewport;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment BaseLayerDetailsTabsFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static OverlayManagerSizeFactorFragment newInstance() {
        OverlayManagerSizeFactorFragment fragment = new OverlayManagerSizeFactorFragment();
        return fragment;
    }

    public OverlayManagerSizeFactorFragment() {
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
        // Inflate the layout for this fragment
        mRootView = inflater.inflate(R.layout.fragment_overlay_manager_size_factor, container, false);
        inflateViews();
        initViews();

        return mRootView;
    }

    private void initSizeFactor() {
        mObjectsSizeFactorData = initSizeFactorData(false);
        mVectorItemSizeFactorData = initSizeFactorData(true);

        mObjectsLV.setAdapter(new SizeFactorAdapter(getContext(), mObjectsSizeFactorData));
        Funcs.setListViewHeightBasedOnChildren(mObjectsLV);

        mVectorItemsLV.setAdapter(new SizeFactorAdapter(getContext(), mVectorItemSizeFactorData));
        Funcs.setListViewHeightBasedOnChildren(mVectorItemsLV);
    }

    private ArrayList<SizeFactorRow> initSizeFactorData(boolean isVectorItem) {
        ArrayList<SizeFactorRow> sizeFactorData = new ArrayList<>();
        IMcOverlayManager.ESizePropertyType[] sizePropertyTypes = IMcOverlayManager.ESizePropertyType.values();
        for (int i = 0; i < sizePropertyTypes.length; i++) {
            float itemSizeFactor = 0;
            SizeFactorRow sizeFactorRow = new SizeFactorRow();
            try {
                itemSizeFactor = mOverlayManager.GetItemSizeFactor(sizePropertyTypes[i],mSelectedViewport, isVectorItem);
            } catch (MapCoreException e) {
                if (e.getErrorCode() != null && e.getErrorCode() != IMcErrors.ECode.INVALID_ARGUMENT)
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetItemSizeFactor");
            } catch (Exception e) {
                e.printStackTrace();
            }
            sizeFactorRow.eSizePropertyType = sizePropertyTypes[i];
            sizeFactorRow.sizeFactor = itemSizeFactor;
            sizeFactorRow.isChecked = false;

            sizeFactorData.add(sizeFactorRow);
        }
        return sizeFactorData;
    }

    private void initViews() {
        initViewportsLV();
        initSizeFactor();
        initBttns();
    }

    private void initBttns() {
        initObjectBttns();
        initVectorItemsBttns();
    }

    private void initObjectBttns() {
        initApplyBttn(mObjectsApplyBttn, mObjectsLV, false);
        initSelectAllBttn(mObjectsSelectAllBttn, mObjectsLV, true);
        initRemoveSelectionBttn(mObjectsRemoveSelectionBttn, mObjectsLV, false);
        initSetSelectedSizeFactorBttn(mObjectsSetSelectedSizeFactorBttn, mObjectsSetSelectedSizeFactorET, mObjectsLV, false);
    }

    private void initVectorItemsBttns() {
        initApplyBttn(mVectorItemsApplyBttn, mVectorItemsLV, true);
        initSelectAllBttn(mVectorItemsSelectAllBttn, mVectorItemsLV, true);
        initRemoveSelectionBttn(mVectorItemsRemoveSelectionBttn, mVectorItemsLV, false);
        initSetSelectedSizeFactorBttn(mVectorItemsSetSelectedSizeFactorBttn, mVectorItemsSetSelectedSizeFactorET, mVectorItemsLV, true);
    }

    private void initSetSelectedSizeFactorBttn(Button setSelectedSizeFactorBttn, final EditText setSelectedSizeFactorET, final ListView sizeFactorLV, final boolean isVectorItem) {
        setSelectedSizeFactorBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (!((String.valueOf(setSelectedSizeFactorET.getText())).equals(""))) {
                    final float newSizeFactor = Float.valueOf(String.valueOf(setSelectedSizeFactorET.getText()));
                    IMcOverlayManager.ESizePropertyType propertyType;
                    CMcEnumBitField<IMcOverlayManager.ESizePropertyType> enumBitField = new CMcEnumBitField<>();
                    TextView esptType;
                    boolean isAnyCheckedCb = false;
                    if (newSizeFactor > 0) {
                        for (int i = 0; i < sizeFactorLV.getCount(); i++) {
                            CheckBox cb = (CheckBox) sizeFactorLV.getChildAt(i).findViewById(R.id.size_factor_row_cb);
                            if (cb.isChecked()) {
                                esptType = (TextView) sizeFactorLV.getChildAt(i).findViewById(R.id.size_factor_row_espt_type_tv);
                                propertyType = IMcOverlayManager.ESizePropertyType.valueOf(String.valueOf(esptType.getText()));
                                enumBitField.Set(propertyType);
                                isAnyCheckedCb = true;
                            }
                        }
                    }
                    if (isAnyCheckedCb) {
                        final CMcEnumBitField<IMcOverlayManager.ESizePropertyType> finalEnumBitField = enumBitField;
                        Funcs.runMapCoreFunc(new Runnable() {
                            @Override
                            public void run() {
                                try {
                                    mOverlayManager.SetItemSizeFactors(finalEnumBitField, newSizeFactor, mSelectedViewport, isVectorItem);

                                    getActivity().runOnUiThread(new Runnable() {
                                        @Override
                                        public void run() {
                                            invalidateLV(sizeFactorLV, isVectorItem);
                                        }
                                    });
                                } catch (MapCoreException e) {
                                    if (e.getErrorCode() != IMcErrors.ECode.INVALID_ARGUMENT)
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetItemSizeFactors");
                                } catch (Exception e) {
                                    e.printStackTrace();
                                }
                            }
                        });
                    } else {
                        AlertMessages.ShowErrorMessage(getContext(), "Error", "No Selected Values");
                    }
                }
            }
        });
    }

    private void initRemoveSelectionBttn(Button selectAllBttn, final ListView sizeFactorLV, final boolean toCheck) {
        selectAllBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                setAllCbs(sizeFactorLV, toCheck);
            }
        });
    }

    private void initSelectAllBttn(Button selectAllBttn, final ListView sizeFactorLV, final boolean toCheck) {
        selectAllBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                setAllCbs(sizeFactorLV, toCheck);
            }
        });
    }

    private void initApplyBttn(Button applyBttn, final ListView sizeFactorLV, final boolean isVectorItem) {
        applyBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveSizeFactor(sizeFactorLV, isVectorItem);
                //invalidateLV(sizeFactorLV, isVectorItem);
            }
        });
    }

    private void invalidateLV(ListView sizeFactorLV, boolean isVectorItem) {
        ArrayList<SizeFactorRow> sizeFactorRows = initSizeFactorData(isVectorItem);
        sizeFactorLV.setAdapter(null);
        sizeFactorLV.setAdapter(new SizeFactorAdapter(getContext(), sizeFactorRows));
    }

    private void setAllCbs(ListView sizeFactorLV, boolean isChecked) {
        for (int i = 0; i < sizeFactorLV.getCount(); i++) {
            CheckBox cb = (CheckBox) sizeFactorLV.getChildAt(i).findViewById(R.id.size_factor_row_cb);
            cb.setChecked(isChecked);
        }
        sizeFactorLV.invalidateViews();
    }

    private void saveSizeFactor(ListView sizeFactorLV, final boolean isVectorItem) {
        for (int i = 0; i < sizeFactorLV.getCount(); i++) {
            TextView esptType = (TextView) sizeFactorLV.getChildAt(i).findViewById(R.id.size_factor_row_espt_type_tv);
            IMcOverlayManager.ESizePropertyType propertyType = IMcOverlayManager.ESizePropertyType.valueOf(String.valueOf(esptType.getText()));
            EditText sizeFactorET = (EditText) sizeFactorLV.getChildAt(i).findViewById(R.id.size_factor_row_factor_et);
            if (!((String.valueOf(sizeFactorET.getText())).equals(""))) {
                final float sizeFactorVal = Float.valueOf(String.valueOf(sizeFactorET.getText()));
                if (sizeFactorVal > 0) {
                    final CMcEnumBitField<IMcOverlayManager.ESizePropertyType> enumBitField = new CMcEnumBitField<>(propertyType);
                    Funcs.runMapCoreFunc(new Runnable() {
                        @Override
                        public void run() {
                            try {
                                mOverlayManager.SetItemSizeFactors(enumBitField, sizeFactorVal, mSelectedViewport, isVectorItem);
                            } catch (MapCoreException e) {
                                if (e.getErrorCode() != IMcErrors.ECode.INVALID_ARGUMENT)
                                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetItemSizeFactors");
                            } catch (Exception e) {
                                e.printStackTrace();
                            }
                        }
                    });
                }
            }
        }
    }

    private void initViewportsLV() {
        try {
            mViewportsLV.initViewportsList(this,
                    Consts.ListType.SINGLE_CHECK,
                    ListView.CHOICE_MODE_SINGLE,
                    mOverlayManager);
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
        initSizeFactor();
    }

    private void inflateViews() {
        inflateViewportListViews();
        inflateObjectsViews();
        inflateVectorItemsViews();
    }

    private void inflateVectorItemsViews() {
        mVectorItemsLV = (ListView) mRootView.findViewById(R.id.om_size_factor_vector_items_lv);
        LinearLayout vectorItemsBttns = (LinearLayout) mRootView.findViewById(R.id.om_size_factor_vector_items_bttns);
        mVectorItemsApplyBttn = (Button) vectorItemsBttns.findViewById(R.id.size_factor_table_bttns_apply_bttn);
        mVectorItemsSelectAllBttn = (Button) vectorItemsBttns.findViewById(R.id.size_factor_table_bttns_select_all_bttn);
        mVectorItemsRemoveSelectionBttn = (Button) vectorItemsBttns.findViewById(R.id.size_factor_table_bttns_remove_selection_bttn);
        mVectorItemsSetSelectedSizeFactorBttn = (Button) vectorItemsBttns.findViewById(R.id.size_factor_table_bttns_set_selected_size_factor_bttn);
        mVectorItemsSetSelectedSizeFactorET = (EditText) vectorItemsBttns.findViewById(R.id.size_factor_table_bttns_set_selected_size_factor_et);
    }

    private void inflateObjectsViews() {
        mObjectsLV = (ListView) mRootView.findViewById(R.id.om_size_factor_objects_lv);
        LinearLayout objectsBttns = (LinearLayout) mRootView.findViewById(R.id.om_size_factor_objects_bttns);
        mObjectsApplyBttn = (Button) objectsBttns.findViewById(R.id.size_factor_table_bttns_apply_bttn);
        mObjectsSelectAllBttn = (Button) objectsBttns.findViewById(R.id.size_factor_table_bttns_select_all_bttn);
        mObjectsRemoveSelectionBttn = (Button) objectsBttns.findViewById(R.id.size_factor_table_bttns_remove_selection_bttn);
        mObjectsSetSelectedSizeFactorBttn = (Button) objectsBttns.findViewById(R.id.size_factor_table_bttns_set_selected_size_factor_bttn);
        mObjectsSetSelectedSizeFactorET = (EditText) objectsBttns.findViewById(R.id.size_factor_table_bttns_set_selected_size_factor_et);
    }

    private void inflateViewportListViews() {
        mViewportsLV = (ViewportsList) mRootView.findViewById(R.id.om_size_factor_viewports_lv);
    }

    @Override
    public void onHiddenChanged(boolean hidden) {
        super.onHiddenChanged(hidden);
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
        mOverlayManager = (IMcOverlayManager) obj;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        if(mIsVisibleToUser)
            outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mOverlayManager));
    }
    private boolean mIsVisibleToUser;

    @Override
    public void setUserVisibleHint(boolean isVisibleToUser) {
        super.setUserVisibleHint(isVisibleToUser);
        mIsVisibleToUser = isVisibleToUser;
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
