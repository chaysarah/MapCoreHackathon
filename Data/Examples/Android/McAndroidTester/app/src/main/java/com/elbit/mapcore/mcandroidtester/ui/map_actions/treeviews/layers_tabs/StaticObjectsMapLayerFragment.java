package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.layers_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.ListView;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.Map.IMcStaticObjectsMapLayer;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.interfaces.OnSelectViewportFromListListener;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.ViewportsList;

import java.util.ArrayList;
import java.util.List;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link StaticObjectsMapLayerFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link StaticObjectsMapLayerFragment#newInstance} factory method to
 * create an instance of this fragment.
 *
 */
public class StaticObjectsMapLayerFragment extends Fragment implements FragmentWithObject, OnSelectViewportFromListListener {

    private OnFragmentInteractionListener mListener;
    private IMcStaticObjectsMapLayer mStaticObjectsMapLayer;

    private View mView;

    private CheckBox mDisplayingItemsAttachedCB;
    private CheckBox mDisplayingDtmVisualizationCB;

    private Button mDisplayingItemsAttachedBtn;
    private Button mDisplayingDtmVisualizationBtn;

    private ViewportsList mViewportsLV;
    private List<IMcMapViewport> mSelectedViewportsLst = new ArrayList<>();

    public static StaticObjectsMapLayerFragment newInstance() {
        StaticObjectsMapLayerFragment fragment = new StaticObjectsMapLayerFragment();
        return fragment;
    }

    public StaticObjectsMapLayerFragment() {
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
        mView = inflater.inflate(R.layout.fragment_static_objects_map_layer, container, false);
        Funcs.SetObjectFromBundle(savedInstanceState, this );
        InitControls();
        InitControlsOperations();

        return mView;
    }

    private void InitControls()
    {
        mDisplayingItemsAttachedCB = (CheckBox) mView.findViewById(R.id.static_objects_details_displaying_items_attached_cb);
        mDisplayingDtmVisualizationCB = (CheckBox) mView.findViewById(R.id.static_objects_details_displaying_dtm_visualization_cb);

        mDisplayingItemsAttachedBtn = (Button) mView.findViewById(R.id.static_objects_details_displaying_items_attached_apply_bttn);
        mDisplayingDtmVisualizationBtn = (Button) mView.findViewById(R.id.static_objects_details_displaying_dtm_visualization_apply_bttn);

        mViewportsLV = (ViewportsList) mView.findViewById(R.id.static_objects_details_viewports_lv);
    }

    private void InitControlsOperations()
    {
        mDisplayingItemsAttachedBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            if (mSelectedViewportsLst.size() > 0) {
                                for( IMcMapViewport vp :mSelectedViewportsLst)
                                    mStaticObjectsMapLayer.SetDisplayingItemsAttachedToTerrain(mDisplayingItemsAttachedCB.isChecked(), vp);
                            } else
                                mStaticObjectsMapLayer.SetDisplayingItemsAttachedToTerrain(mDisplayingItemsAttachedCB.isChecked());
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetDisplayingItemsAttachedToTerrain");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });

        mDisplayingDtmVisualizationBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            if (mSelectedViewportsLst.size() > 0) {
                                for( IMcMapViewport vp :mSelectedViewportsLst)
                                    mStaticObjectsMapLayer.SetDisplayingDtmVisualization(mDisplayingDtmVisualizationCB.isChecked(), vp);
                            } else
                                mStaticObjectsMapLayer.SetDisplayingDtmVisualization(mDisplayingDtmVisualizationCB.isChecked());
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetDisplayingDtmVisualization");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });

        try {
            mViewportsLV.initViewportsList(this,
                    Consts.ListType.MULTIPLE_CHECK,
                    ListView.CHOICE_MODE_MULTIPLE,
                    null,
                    mStaticObjectsMapLayer,
                    null);
        } catch (Exception e) {
            e.printStackTrace();
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
        } else {
//            throw new RuntimeException(context.toString()
//                    + " must implement OnFragmentInteractionListener");
        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    @Override
    public void setObject(Object obj) {
        mStaticObjectsMapLayer = (IMcStaticObjectsMapLayer) obj;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
         outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mStaticObjectsMapLayer));
    }

    @Override
    public void onSelectViewportFromList(IMcMapViewport mcSelectedViewport, boolean isChecked) {
        // mSelectedViewport = mcSelectedViewport;
        if (isChecked)
            mSelectedViewportsLst.add(mcSelectedViewport);
         else
            mSelectedViewportsLst.remove(mcSelectedViewport);
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
