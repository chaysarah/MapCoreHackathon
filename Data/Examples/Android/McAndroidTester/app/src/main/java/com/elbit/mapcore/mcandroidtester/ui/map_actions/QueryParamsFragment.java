package com.elbit.mapcore.mcandroidtester.ui.map_actions;

import android.content.Context;
import android.net.Uri;
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

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_AMCTMapForm;
import com.elbit.mapcore.mcandroidtester.ui.adapters.OverlayAdapter;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SpinnerWithLabel;

import com.elbit.mapcore.General.CMcEnumBitField;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Calculations.IMcSpatialQueries;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeNode;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlay;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link QueryParamsFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link QueryParamsFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class QueryParamsFragment extends Fragment {

    private OnFragmentInteractionListener mListener;
    private View mRootView;
    private ListView mIntersectionTargetTypeLV;
    private ListView mOverlayLV;
    private ListView mItemTypeLV;
    private ListView mItemKindLV;
    private SpinnerWithLabel mTerrainPrecisionSWL;
    private SpinnerWithLabel mNoDTMResultSWL;
    private Button mSaveBttn;
    private IMcSpatialQueries.SQueryParams mQueryParams;
    private CheckBox mUseMeshBoundingBoxOnlyCB;
    private CheckBox mAddStaticObjectContoursCB;
    private NumericEditTextLabel mBoundingBoxExpansionDistNETL;
    private CMcEnumBitField<IMcSpatialQueries.EIntersectionTargetType> mCurrIntersectionTargetType;
    private CheckBox mUseFlatEarthCB;
    private NumericEditTextLabel mGreatCirclePrecisionNETL;
    private CMcEnumBitField<IMcObjectSchemeNode.ENodeKindFlags> mCurrItemKind;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment QueryParamsFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static QueryParamsFragment newInstance() {
        QueryParamsFragment fragment = new QueryParamsFragment();
        return fragment;
    }

    public QueryParamsFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        mRootView = inflater.inflate(R.layout.fragment_query_params, container, false);
        inflateViews();
        initViews();
        // Inflate the layout for this fragment
        return mRootView;
    }

    private void initViews() {
        initIntersectionTargetType();

        mItemTypeLV.setAdapter(new ArrayAdapter<>(getActivity(), android.R.layout.simple_list_item_multiple_choice, IMcObjectSchemeNode.EObjectSchemeNodeType.values()));
        Funcs.setListViewHeightBasedOnChildren(mItemTypeLV);
        mItemTypeLV.setChoiceMode(AbsListView.CHOICE_MODE_MULTIPLE);
        initItemKindLV();
        initOverlaysLV();
        initTerrainPrecision();
        initNoDTM();
        initSave();
    }

    private void initItemKindLV() {
        mItemKindLV.setAdapter(new ArrayAdapter<>(getActivity(), android.R.layout.simple_list_item_multiple_choice, IMcObjectSchemeNode.ENodeKindFlags.values()));
        Funcs.setListViewHeightBasedOnChildren(mItemKindLV);
        mItemKindLV.setChoiceMode(AbsListView.CHOICE_MODE_MULTIPLE);
        for (int i = 0; i < mItemKindLV.getCount(); i++) {
            if (((IMcObjectSchemeNode.ENodeKindFlags) mItemKindLV.getAdapter().getItem(i)).compareTo(IMcObjectSchemeNode.ENodeKindFlags.ENKF_NONE) != 0)
                mItemKindLV.setItemChecked(i, true);
        }
        mItemKindLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                if (((IMcObjectSchemeNode.ENodeKindFlags) mItemKindLV.getAdapter().getItem(position)).compareTo(IMcObjectSchemeNode.ENodeKindFlags.ENKF_NONE) == 0) {
                    SparseBooleanArray checkedItems = mItemKindLV.getCheckedItemPositions();
                    for (int i = 0; i < checkedItems.size(); i++) {
                        if (checkedItems.valueAt(i) && i != position)
                            mItemKindLV.setItemChecked(i, false);
                    }
                } else
                    mItemKindLV.setItemChecked((IMcObjectSchemeNode.ENodeKindFlags.ENKF_NONE.getValue()), false);
            }
        });
    }

    private void initIntersectionTargetType() {
        mIntersectionTargetTypeLV.setAdapter(new ArrayAdapter<>(getActivity(), android.R.layout.simple_list_item_multiple_choice, IMcSpatialQueries.EIntersectionTargetType.values()));
        mIntersectionTargetTypeLV.setChoiceMode(AbsListView.CHOICE_MODE_MULTIPLE);
        Funcs.setListViewHeightBasedOnChildren(mIntersectionTargetTypeLV);
        for (int i = 0; i < mIntersectionTargetTypeLV.getCount(); i++) {
            if (((IMcSpatialQueries.EIntersectionTargetType) mIntersectionTargetTypeLV.getAdapter().getItem(i)).compareTo(IMcSpatialQueries.EIntersectionTargetType.EITT_NONE) != 0)
                mIntersectionTargetTypeLV.setItemChecked(i, true);
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

    private void initOverlaysLV() {
        IMcMapViewport currViewport = Manager_AMCTMapForm.getInstance().getCurViewport();
        IMcOverlayManager currOverlayManager = null;
        try {
            currOverlayManager = currViewport.GetOverlayManager();
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "GetOverlayManager");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
        if (currOverlayManager != null) {
            IMcOverlay[] overlays = new IMcOverlay[0];
            try {
                overlays = currOverlayManager.GetOverlays();
            } catch (MapCoreException e) {
                AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "GetOverlays");
                e.printStackTrace();
            } catch (Exception e) {
                e.printStackTrace();
            }
            mOverlayLV.setAdapter(new OverlayAdapter(overlays));
            mOverlayLV.setChoiceMode(AbsListView.CHOICE_MODE_SINGLE);
            Funcs.setListViewHeightBasedOnChildren(mOverlayLV);
        }
    }

    //==setQueryParams
    private void initSave() {
        mSaveBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                mQueryParams = new IMcSpatialQueries.SQueryParams();
                mQueryParams.bUseMeshBoundingBoxOnly = mUseMeshBoundingBoxOnlyCB.isChecked();
                mQueryParams.eTerrainPrecision = (IMcSpatialQueries.EQueryPrecision) mTerrainPrecisionSWL.getSelectedItem();
                mQueryParams.fBoundingBoxExpansionDist = mBoundingBoxExpansionDistNETL.getFloat();
                mQueryParams.uItemTypeFlagsBitField = getItemTypes();
                mQueryParams.uItemKindsBitField = getItemKinds();

                mQueryParams.uTargetsBitMask = getIntersectionTargetType();
                mQueryParams.bUseFlatEarth = mUseFlatEarthCB.isChecked();
                mQueryParams.fGreatCirclePrecision = mGreatCirclePrecisionNETL.getFloat();
                mQueryParams.eNoDTMResult = (IMcSpatialQueries.ENoDTMResult) mNoDTMResultSWL.getSelectedItem();
                mQueryParams.bAddStaticObjectContours = mAddStaticObjectContoursCB.isChecked();

                if (mOverlayLV.getSelectedItem() != null)
                    mQueryParams.pOverlayFilter = (IMcOverlay) mOverlayLV.getSelectedItem();
                else
                    mQueryParams.pOverlayFilter = null;

                ScanFragment scanFragment = (ScanFragment) getFragmentManager().findFragmentByTag(ScanFragment.class.getSimpleName());
                scanFragment.setObject(mQueryParams);
                getActivity().onBackPressed();
            }

        });
    }

    private CMcEnumBitField<IMcSpatialQueries.EIntersectionTargetType> getIntersectionTargetType() {
        mCurrIntersectionTargetType = new CMcEnumBitField<>(IMcSpatialQueries.EIntersectionTargetType.EITT_NONE);
        SparseBooleanArray checked = null;
        int len = mIntersectionTargetTypeLV.getCount();
        checked = mIntersectionTargetTypeLV.getCheckedItemPositions();
        for (int i = 0; i < len; i++) {
            if (checked.get(i)) {
                mCurrIntersectionTargetType.Set((IMcSpatialQueries.EIntersectionTargetType) mIntersectionTargetTypeLV.getItemAtPosition(i));
            }
        }
        return mCurrIntersectionTargetType;
    }

    private CMcEnumBitField<IMcObjectSchemeNode.ENodeKindFlags> getItemKinds() {
        mCurrItemKind = new CMcEnumBitField<>(IMcObjectSchemeNode.ENodeKindFlags.ENKF_NONE);
        SparseBooleanArray checked = null;
        int len = mItemKindLV.getCount();
        checked = mItemKindLV.getCheckedItemPositions();
        for (int i = 0; i < len; i++) {
            if (checked.get(i)) {
                mCurrItemKind.Set((IMcObjectSchemeNode.ENodeKindFlags) mItemKindLV.getItemAtPosition(i));
            }
        }
        return mCurrItemKind;
    }

    private int getItemTypes() {
        int m_CurrItemTypes = 0;
        SparseBooleanArray checkedItems = mItemTypeLV.getCheckedItemPositions();
        int checkedItemsLength = checkedItems.size();

        for (int i = 0; i < checkedItemsLength; i++) {
            IMcObjectSchemeNode.EObjectSchemeNodeType eNodeType = (IMcObjectSchemeNode.EObjectSchemeNodeType) mItemTypeLV.getItemAtPosition(checkedItems.keyAt(i));
            int bitMask = IMcSpatialQueries.SQueryParams.NodeTypeToItemTypeBit(eNodeType);
            m_CurrItemTypes = m_CurrItemTypes | bitMask;
        }

        return m_CurrItemTypes;
    }

    private void initNoDTM() {
        mNoDTMResultSWL.setAdapter(new ArrayAdapter<>(getContext(), android.R.layout.simple_spinner_item, IMcSpatialQueries.ENoDTMResult.values()));
        mNoDTMResultSWL.setSelection(IMcSpatialQueries.ENoDTMResult.ENDR_FAIL.getValue());
    }

    private void initTerrainPrecision() {
        mTerrainPrecisionSWL.setAdapter(new ArrayAdapter<>(getContext(), android.R.layout.simple_spinner_item, IMcSpatialQueries.EQueryPrecision.values()));
        mTerrainPrecisionSWL.setSelection(IMcSpatialQueries.EQueryPrecision.EQP_DEFAULT.getValue());
    }

    private void inflateViews() {
        mUseMeshBoundingBoxOnlyCB = (CheckBox) mRootView.findViewById(R.id.query_params_use_mesh_bounding_box_only_cb);
        mGreatCirclePrecisionNETL = (NumericEditTextLabel) mRootView.findViewById(R.id.query_params_great_circle_precision_netl);
        mUseFlatEarthCB = (CheckBox) mRootView.findViewById(R.id.query_params_use_flat_earth_cb);
        mBoundingBoxExpansionDistNETL = (NumericEditTextLabel) mRootView.findViewById(R.id.query_params_bounding_box_expansion_dist_netl);
        mIntersectionTargetTypeLV = (ListView) mRootView.findViewById(R.id.query_params_intersection_target_type);
        mItemTypeLV = (ListView) mRootView.findViewById(R.id.query_params_item_type);
        mItemKindLV = (ListView) mRootView.findViewById(R.id.query_params_item_kind);
        mTerrainPrecisionSWL = (SpinnerWithLabel) mRootView.findViewById(R.id.query_params_terrain_precision_swl);
        mNoDTMResultSWL = (SpinnerWithLabel) mRootView.findViewById(R.id.query_params_no_dtm_result_swl);
        mSaveBttn = (Button) mRootView.findViewById(R.id.query_params_save_bttn);
        mOverlayLV = (ListView) mRootView.findViewById(R.id.query_params_overlay_filter_lv);
        mAddStaticObjectContoursCB = (CheckBox) mRootView.findViewById(R.id.query_params_add_static_object_contours_cb);

        //
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
        if (context instanceof MapsContainerActivity) {
            ((MapsContainerActivity) context).mCurFragmentTag = QueryParamsFragment.class.getSimpleName();
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
