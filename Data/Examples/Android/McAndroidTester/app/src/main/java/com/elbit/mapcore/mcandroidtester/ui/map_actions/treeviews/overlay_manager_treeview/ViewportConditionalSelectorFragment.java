package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlay_manager_treeview;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AbsListView;
import android.widget.ArrayAdapter;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.ListView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.ui.adapters.ENumAdapter;
import com.elbit.mapcore.mcandroidtester.ui.adapters.ViewportTypeAdapter;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import com.elbit.mapcore.General.CMcEnumBitField;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.General.ObjectRef;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcViewportConditionalSelector;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link ViewportConditionalSelectorFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link ViewportConditionalSelectorFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class ViewportConditionalSelectorFragment extends BaseConditionalSelectorFragment implements FragmentWithObject {

    private OnFragmentInteractionListener mListener;
    private IMcViewportConditionalSelector mCurrentCondSelector;
    private int[] mViewportIDs;
    private ListView mViewportTypeLV;
    private ListView mViewportCoordSysLV;
    private EditText mViewportIDsET;
    private CheckBox mIDsInclusiveCB;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment ViewportConditionalSelectorFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static ViewportConditionalSelectorFragment newInstance() {
        ViewportConditionalSelectorFragment fragment = new ViewportConditionalSelectorFragment();
        return fragment;
    }

    public ViewportConditionalSelectorFragment() {
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
        mRootview = inflater.inflate(R.layout.fragment_viewport_conditional_selector, container, false);
        inflateViews();
        initViews();
        return mRootview;
    }

    @Override
    protected void inflateViews() {
        super.inflateViews();
        mViewportTypeLV = (ListView) mRootview.findViewById(R.id.viewport_conditional_selector_viewport_type_lv);
        mViewportCoordSysLV = (ListView) mRootview.findViewById(R.id.viewport_conditional_selector_viewport_coord_sys_lv);
        mViewportIDsET = (EditText) mRootview.findViewById(R.id.viewport_conditional_selector_viewport_ids);
        mIDsInclusiveCB = (CheckBox) mRootview.findViewById(R.id.viewport_conditional_selector_viewport_ids_inclusive_cb);
    }

    @Override
    protected void initViews() {
        super.initViews();
        initViewportTypeLV();
        initViewportCoordSys();
        initViewportIds();

    }

    private void initViewportIds() {
        ObjectRef<int[]> viewportIDs = new ObjectRef<>();
        ObjectRef<Boolean> isInclusive = new ObjectRef();
        try {
            mCurrentCondSelector.GetSpecificViewports(viewportIDs, isInclusive);
            mIDsInclusiveCB.setChecked(isInclusive.getValue());
            mViewportIDsET.setText(Funcs.ConvertIntArrToString(viewportIDs.getValue()));
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetSpecificViewports");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void markViewportTypeCB(IMcViewportConditionalSelector.EViewportTypeFlags currType) {
        int currTypePos = ((ArrayAdapter) mViewportTypeLV.getAdapter()).getPosition(currType);
        mViewportTypeLV.setItemChecked(currTypePos, true);
    }

    private void initViewportCoordSys() {
        IMcViewportConditionalSelector.EViewportCoordinateSystem[] allCoordSystem = IMcViewportConditionalSelector.EViewportCoordinateSystem.values();
        List<IMcViewportConditionalSelector.EViewportCoordinateSystem> allCoordSystemList = new ArrayList<>(Arrays.asList(allCoordSystem));
        allCoordSystemList.remove(IMcViewportConditionalSelector.EViewportCoordinateSystem.EVCS_ALL_COORDINATE_SYSTEMS);
        mViewportCoordSysLV.setAdapter(new ENumAdapter(getContext(), R.layout.checkable_list_item, allCoordSystemList));
        Funcs.setListViewHeightBasedOnChildren(mViewportCoordSysLV);
        mViewportCoordSysLV.setChoiceMode(AbsListView.CHOICE_MODE_MULTIPLE);
        checkCoordSys();
    }

    private void checkCoordSys() {
        try {
            CMcEnumBitField<IMcViewportConditionalSelector.EViewportCoordinateSystem> viewportCoordinateSys = mCurrentCondSelector.GetViewportCoordinateSystemBitField();
            IMcViewportConditionalSelector.EViewportCoordinateSystem currCoordSys;
            if (viewportCoordinateSys.IsSet(IMcViewportConditionalSelector.EViewportCoordinateSystem.EVCS_ALL_COORDINATE_SYSTEMS)) {
                currCoordSys = IMcViewportConditionalSelector.EViewportCoordinateSystem.EVCS_ALL_COORDINATE_SYSTEMS;
                markViewportCoordSysCb(currCoordSys);
            }
            if (viewportCoordinateSys.IsSet(IMcViewportConditionalSelector.EViewportCoordinateSystem.EVCS_GEO_COORDINATE_SYSTEM)) {
                currCoordSys = IMcViewportConditionalSelector.EViewportCoordinateSystem.EVCS_GEO_COORDINATE_SYSTEM;
                markViewportCoordSysCb(currCoordSys);
            }
            if (viewportCoordinateSys.IsSet(IMcViewportConditionalSelector.EViewportCoordinateSystem.EVCS_UTM_COORDINATE_SYSTEM)) {
                currCoordSys = IMcViewportConditionalSelector.EViewportCoordinateSystem.EVCS_UTM_COORDINATE_SYSTEM;
                markViewportCoordSysCb(currCoordSys);
            }
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetViewportCoordinateSystemBitField");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void markViewportCoordSysCb(IMcViewportConditionalSelector.EViewportCoordinateSystem currCoordSys) {
        int currCoordSysPos = ((ArrayAdapter) mViewportCoordSysLV.getAdapter()).getPosition(currCoordSys);
        mViewportTypeLV.setItemChecked(currCoordSysPos, true);
    }

    private void initViewportTypeLV() {
        IMcViewportConditionalSelector.EViewportTypeFlags[] allVisibilities = IMcViewportConditionalSelector.EViewportTypeFlags.values();
        ArrayList<IMcViewportConditionalSelector.EViewportTypeFlags> allVisibilitiesList = new ArrayList<>(Arrays.asList(allVisibilities));
        allVisibilitiesList.remove(IMcViewportConditionalSelector.EViewportTypeFlags.EVT_NONE);
        mViewportTypeLV.setAdapter(new ViewportTypeAdapter(getContext(), R.layout.checkable_list_item, allVisibilitiesList));
        Funcs.setListViewHeightBasedOnChildren(mViewportTypeLV);
        mViewportTypeLV.setChoiceMode(AbsListView.CHOICE_MODE_MULTIPLE);
        checkTypes();
    }

    private void checkTypes() {
        if (mCurrentCondSelector != null) {
            try {
                CMcEnumBitField<IMcViewportConditionalSelector.EViewportTypeFlags> viewportType = mCurrentCondSelector.GetViewportTypeBitField();
                IMcViewportConditionalSelector.EViewportTypeFlags currType = IMcViewportConditionalSelector.EViewportTypeFlags.EVT_NONE;
                if (viewportType.IsSet(IMcViewportConditionalSelector.EViewportTypeFlags.EVT_2D_VIEWPORT)) {
                    currType = IMcViewportConditionalSelector.EViewportTypeFlags.EVT_2D_VIEWPORT;
                    markViewportTypeCB(currType);
                }
                if (viewportType.IsSet(IMcViewportConditionalSelector.EViewportTypeFlags.EVT_2D_IMAGE_VIEWPORT)) {
                    currType = IMcViewportConditionalSelector.EViewportTypeFlags.EVT_2D_IMAGE_VIEWPORT;
                    markViewportTypeCB(currType);
                }
                if (viewportType.IsSet(IMcViewportConditionalSelector.EViewportTypeFlags.EVT_2D_SECTION_VIEWPORT)) {
                    currType = IMcViewportConditionalSelector.EViewportTypeFlags.EVT_2D_SECTION_VIEWPORT;
                    markViewportTypeCB(currType);
                }
                if (viewportType.IsSet(IMcViewportConditionalSelector.EViewportTypeFlags.EVT_2D_REGULAR_VIEWPORT)) {
                    currType = IMcViewportConditionalSelector.EViewportTypeFlags.EVT_2D_REGULAR_VIEWPORT;
                    markViewportTypeCB(currType);
                }
                if (viewportType.IsSet(IMcViewportConditionalSelector.EViewportTypeFlags.EVT_3D_VIEWPORT)) {
                    currType = IMcViewportConditionalSelector.EViewportTypeFlags.EVT_3D_VIEWPORT;
                    markViewportTypeCB(currType);
                }
                if (viewportType.IsSet(IMcViewportConditionalSelector.EViewportTypeFlags.EVT_ALL_VIEWPORTS)) {
                    currType = IMcViewportConditionalSelector.EViewportTypeFlags.EVT_ALL_VIEWPORTS;
                    markViewportTypeCB(currType);
                }
                if (viewportType.IsSet(IMcViewportConditionalSelector.EViewportTypeFlags.EVT_NONE)) {
                    currType = IMcViewportConditionalSelector.EViewportTypeFlags.EVT_NONE;
                    markViewportTypeCB(currType);
                }

            } catch (MapCoreException e) {
                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetViewportTypeBitField");
                e.printStackTrace();
            } catch (Exception e) {
                e.printStackTrace();
            }
        }

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

    @Override
    protected void save() {
        super.save();
        saveViewportType();
        saveCoordSys();
        saveViewportIDs();
    }

    private void saveViewportIDs() {
        final int[] viewportIDs = Funcs.ConvertStringToIntArr(String.valueOf(mViewportIDsET.getText()));
        final boolean isIDsInclusive = mIDsInclusiveCB.isChecked();
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                if (mCurrentCondSelector != null)
                    try {
                        mCurrentCondSelector.SetSpecificViewports(viewportIDs, isIDsInclusive);
                    } catch (MapCoreException e) {
                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetSpecificViewports");
                        e.printStackTrace();
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
            }});
    }

    private void saveCoordSys() {
        CMcEnumBitField<IMcViewportConditionalSelector.EViewportCoordinateSystem> coordSys = new CMcEnumBitField<>();
        ENumAdapter coordSysAdapter = (ENumAdapter) mViewportCoordSysLV.getAdapter();
        for (int i = 0; i < coordSysAdapter.getCount(); i++) {
            if (mViewportCoordSysLV.isItemChecked(i))
                coordSys.Set((IMcViewportConditionalSelector.EViewportCoordinateSystem) coordSysAdapter.getItem(i));
        }
        final CMcEnumBitField<IMcViewportConditionalSelector.EViewportCoordinateSystem> finalCoordSys = coordSys;
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                if (mCurrentCondSelector != null)
                    try {
                        mCurrentCondSelector.SetViewportCoordinateSystemBitField(finalCoordSys);
                    } catch (MapCoreException e) {
                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetViewportCoordinateSystemBitField");
                        e.printStackTrace();
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
            }
        });
    }

    private void saveViewportType() {
        CMcEnumBitField<IMcViewportConditionalSelector.EViewportTypeFlags> viewportTypes = new CMcEnumBitField<>();
        ViewportTypeAdapter adapter = (ViewportTypeAdapter) mViewportTypeLV.getAdapter();
        for (int i = 0; i < adapter.getCount(); i++) {
            if (mViewportTypeLV.isItemChecked(i))
                viewportTypes.Set(adapter.getItem(i));
        }
        final CMcEnumBitField<IMcViewportConditionalSelector.EViewportTypeFlags> finalViewportTypes = viewportTypes;
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                if (mCurrentCondSelector != null)
                    try {
                        mCurrentCondSelector.SetViewportTypeBitField(finalViewportTypes);
                    } catch (MapCoreException e) {
                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetViewportTypeBitField");
                        e.printStackTrace();
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
            }
        });
    }

    @Override
    public void setObject(Object obj) {
        mCurrentCondSelector = (IMcViewportConditionalSelector) obj;
        super.setObject(obj);
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mCurrentCondSelector));
    }
}
