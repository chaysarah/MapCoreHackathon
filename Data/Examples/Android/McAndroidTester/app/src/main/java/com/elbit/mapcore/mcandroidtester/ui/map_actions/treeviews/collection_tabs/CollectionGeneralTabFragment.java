package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.collection_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.ListView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.interfaces.OnSelectViewportFromListListener;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.ViewportsList;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcCollection;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;

/**
 * Created by TC97803 on 07/09/2017.
 */
/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link CollectionGeneralTabFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link CollectionGeneralTabFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class CollectionGeneralTabFragment extends Fragment implements FragmentWithObject, OnSelectViewportFromListListener {

    private OnFragmentInteractionListener mListener;
    private IMcOverlayManager mMcOverlayManager;
    private IMcCollection mMcCollection;
    private ViewportsList cvViewportList;
    private View mRootView;
    private Button mVisibilityApplyBttn;
    private CheckBox mDefaultVisibilityCB;

    public static CollectionGeneralTabFragment newInstance() {
        CollectionGeneralTabFragment fragment = new CollectionGeneralTabFragment();
        return fragment;
    }

    public CollectionGeneralTabFragment(){}

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        Funcs.SetObjectFromBundle(savedInstanceState, this );
        // Inflate the layout for this fragment
        mRootView = inflater.inflate(R.layout.fragment_collection_general_tab, container, false);
        mVisibilityApplyBttn = (Button) mRootView.findViewById(R.id.collection_default_visibility_apply_bttn);
        cvViewportList = (ViewportsList) mRootView.findViewById(R.id.collection_vp_lv);
        mDefaultVisibilityCB = (CheckBox) mRootView.findViewById(R.id.collection_default_visibility_cb);
        initViewportsList();
        return mRootView;
    }

    private void initViewportsList() {
        final Fragment fragment = this;
        getActivity().runOnUiThread(new Runnable() {
            @Override
            public void run() {
                try {
                    cvViewportList.initViewportsList(fragment,
                            Consts.ListType.MULTIPLE_CHECK,
                            ListView.CHOICE_MODE_MULTIPLE,
                            mMcOverlayManager,
                            mMcCollection,
                            mMcCollection.getClass().getMethod("GetCollectionVisibility", IMcMapViewport.class));
                } catch (NoSuchMethodException e) {
                    e.printStackTrace();
                }
            }
        });
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
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        initDefaultVisibility();
    }

    private void initDefaultVisibility() {
        try {
            mDefaultVisibilityCB.setChecked(mMcCollection.GetCollectionVisibility());
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "IMcCollection.GetCollectionVisibility");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }

        mVisibilityApplyBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                final boolean mVisibility = mDefaultVisibilityCB.isChecked();
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            mMcCollection.SetCollectionVisibility(mVisibility);
                            initViewportsList();
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "IMcCollection.SetCollectionVisibility");
                            e.printStackTrace();
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });
    }

    @Override
    public void onAttach(Context context) {
        super.onAttach(context);
        if (context instanceof OnFragmentInteractionListener) {
            mListener = (OnFragmentInteractionListener) context;
        } else {
           /* throw new RuntimeException(context.toString()
                    + " must implement OnFragmentInteractionListener");*/
        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }
    @Override
    public void setObject(Object obj) {
        mMcCollection = (IMcCollection) obj;
        try {
            mMcOverlayManager = mMcCollection.GetOverlayManager();
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "GetOverlayManager");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mMcCollection));
    }

    @Override
    public void onSelectViewportFromList(final IMcMapViewport mcSelectedViewport, final boolean isChecked) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    mMcCollection.SetCollectionVisibility(isChecked, mcSelectedViewport);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "IMcCollection.SetCollectionVisibility");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }});
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
