package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlay_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.inputmethod.InputMethodManager;
import android.widget.AbsListView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.interfaces.OnSelectViewportFromListListener;
import com.elbit.mapcore.mcandroidtester.model.AMCTColorOverriding;
import com.elbit.mapcore.mcandroidtester.ui.adapters.ColorOverridingListAdapter;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.ViewportsList;

import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlay;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link FragmentOverlayColorOverridingFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link FragmentOverlayColorOverridingFragment#newInstance} factory method to
 * create an instance of this fragment.
 *
 */
public class FragmentOverlayColorOverridingFragment extends Fragment implements FragmentWithObject,OnSelectViewportFromListListener {

    private int mTypesLen;
    private OnFragmentInteractionListener mListener;
    private IMcOverlay mOverlay;
    private View mRootView;
    private IMcOverlay.EColorPropertyType[] mColorPropertyTypesArr;
    private IMcOverlay.SColorPropertyOverriding[] mPropertyOverriding;
    private ListView mColorPropertyOverridingLV;
    private ColorOverridingListAdapter mColorOverridingListAdapter;
    private ViewportsList cvViewportList;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment FragmentOverlayColorOverridingFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static FragmentOverlayColorOverridingFragment newInstance() {
        FragmentOverlayColorOverridingFragment fragment = new FragmentOverlayColorOverridingFragment();
        return fragment;
    }
    public FragmentOverlayColorOverridingFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
         // Inflate the layout for this fragment
        Funcs.SetObjectFromBundle(savedInstanceState, this );
        mRootView = inflater.inflate(R.layout.fragment_fragment_overlay_color_overriding, container, false);
        mColorPropertyOverridingLV = (ListView) mRootView.findViewById(R.id.color_overriding_lv);
        Button save = (Button) mRootView.findViewById(R.id.color_overriding_save_bttn);
        cvViewportList = (ViewportsList) mRootView.findViewById(R.id.color_overriding_vp_lv);

        try {
            cvViewportList.initViewportsList(this,
                    Consts.ListType.SINGLE_CHECK,
                    ListView.CHOICE_MODE_SINGLE,
                    mOverlay.GetOverlayManager());
        } catch (Exception e) {
            e.printStackTrace();
        }

        save.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                AMCTColorOverriding[] objects = mColorOverridingListAdapter.ColorOverridingArr;
                IMcOverlay.SColorPropertyOverriding[] datas = new IMcOverlay.SColorPropertyOverriding[mTypesLen];
                for (int i = 0; i < mTypesLen; i++)
                    datas[i] = objects[i].sColorPropertyOverriding;
                final IMcOverlay.SColorPropertyOverriding[] finalDatas = datas;
                final IMcMapViewport selectedViewport = cvViewportList.GetSelectedViewport();
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            mOverlay.SetColorOverriding(finalDatas, selectedViewport);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetColorOverriding");
                            e.printStackTrace();
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });
        initColorOverridingLV();
        loadColorOverridingLV();
        return mRootView;
    }

    private void initColorOverridingLV()
    {
        View listHeader = getActivity().getLayoutInflater().inflate(R.layout.color_overriding_table_header, null);
        mColorPropertyOverridingLV.addHeaderView(listHeader);
        mColorPropertyTypesArr = IMcOverlay.EColorPropertyType.values();
    }

    private void loadColorOverridingLV() {
        try {
            mPropertyOverriding = mOverlay.GetColorOverriding(cvViewportList.GetSelectedViewport());
            mTypesLen = mColorPropertyTypesArr.length - 1;
            AMCTColorOverriding[] objects = new AMCTColorOverriding[mTypesLen];
            for (int i = 0; i < mTypesLen; i++) {
                objects[i] = new AMCTColorOverriding();
                objects[i].eColorPropertyType = mColorPropertyTypesArr[i];
                if (mPropertyOverriding != null)
                    objects[i].sColorPropertyOverriding = mPropertyOverriding[i];
            }

            mColorOverridingListAdapter = new ColorOverridingListAdapter(getContext(), R.layout.color_overriding_table_row, objects);
            mColorPropertyOverridingLV.setAdapter(mColorOverridingListAdapter);
            mColorPropertyOverridingLV.setRecyclerListener(new AbsListView.RecyclerListener() {
                @Override
                public void onMovedToScrapHeap(View view) {
                    if (view.hasFocus()) {
                        view.clearFocus();
                        if (view instanceof EditText) {
                            InputMethodManager imm = (InputMethodManager) view.getContext().getSystemService(Context.INPUT_METHOD_SERVICE);
                            imm.hideSoftInputFromWindow(view.getWindowToken(), 0);
                        }
                    }
                }
            });
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetColorOverriding");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
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
        loadColorOverridingLV();
    }

    /**
     * This interface must be implemented by activities that contain this
     * fragment to allow an interaction in this fragment to be communicated
     * to the activity and potentially other fragments contained in that
     * activity.
     * <p>
     * See the Android Training lesson <a href=
     * "http://developer.android.com/training/basics/fragments/communicating.html"
     * >Communicating with Other Fragments</a> for more information.
     */
    public interface OnFragmentInteractionListener {
        // TODO: Update argument type and name
        void onFragmentInteraction(Uri uri);
    }
}
