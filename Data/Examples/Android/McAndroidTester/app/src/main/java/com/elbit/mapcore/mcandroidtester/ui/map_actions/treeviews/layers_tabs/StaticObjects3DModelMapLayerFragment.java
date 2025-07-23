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
import com.elbit.mapcore.Interfaces.Map.IMc3DModelMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.interfaces.OnSelectViewportFromListListener;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.ViewportsList;

import java.util.ArrayList;
import java.util.List;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link StaticObjects3DModelMapLayerFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link StaticObjects3DModelMapLayerFragment#newInstance} factory method to
 * create an instance of this fragment.
 *
 */
public class StaticObjects3DModelMapLayerFragment extends Fragment implements FragmentWithObject, OnSelectViewportFromListListener {

    private OnFragmentInteractionListener mListener;
    private IMc3DModelMapLayer m3DModelMapLayer;

    private View mView;

    private CheckBox mResolvingConflictsWithDtmAndRasterCB;

    private Button mResolvingConflictsWithDtmAndRasterBtn;
    private Button mResolutionFactorBtn;

    private NumericEditTextLabel mResolutionFactorEt;

    private ViewportsList mViewportsLV;
    private List<IMcMapViewport> mSelectedViewportsLst = new ArrayList<>();

    public static StaticObjects3DModelMapLayerFragment newInstance() {
        StaticObjects3DModelMapLayerFragment fragment = new StaticObjects3DModelMapLayerFragment();
        return fragment;
    }

    public StaticObjects3DModelMapLayerFragment() {
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
        mView = inflater.inflate(R.layout.fragment_static_objects_3d_model_map_layer, container, false);
        Funcs.SetObjectFromBundle(savedInstanceState, this );
        InitControls();
        InitControlsOperations();

        return mView;
    }

    private void InitControls()
    {
        mResolvingConflictsWithDtmAndRasterCB = (CheckBox) mView.findViewById(R.id.static_objects_details_resolving_conflicts_with_dtm_and_raster_cb);
        mResolvingConflictsWithDtmAndRasterBtn = (Button) mView.findViewById(R.id.static_objects_details_resolving_conflicts_with_dtm_and_raster_apply_bttn);

        mViewportsLV = (ViewportsList) mView.findViewById(R.id.static_objects_details_viewports_lv);

        mResolutionFactorEt = (NumericEditTextLabel) mView.findViewById(R.id.static_objects_details_resolution_factor_et);
        mResolutionFactorBtn = (Button) mView.findViewById(R.id.static_objects_details_resolution_factor_bttn);

    }

    private void InitControlsOperations()
    {
        mResolvingConflictsWithDtmAndRasterBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            if (mSelectedViewportsLst.size() > 0) {
                                for(IMcMapViewport vp :mSelectedViewportsLst)
                                    m3DModelMapLayer.SetResolvingConflictsWithDtmAndRaster(mResolvingConflictsWithDtmAndRasterCB.isChecked(), vp);
                            } else
                                m3DModelMapLayer.SetResolvingConflictsWithDtmAndRaster(mResolvingConflictsWithDtmAndRasterCB.isChecked());
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetResolvingConflictsWithDtmAndRaster");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                });
            }
        });

        mResolutionFactorBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            if (mSelectedViewportsLst.size() > 0) {
                                for (IMcMapViewport vp : mSelectedViewportsLst)
                                    m3DModelMapLayer.SetResolutionFactor(mResolutionFactorEt.getFloat(), vp);
                            } else
                                m3DModelMapLayer.SetResolutionFactor(mResolutionFactorEt.getFloat());
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetResolutionFactor");
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
                    m3DModelMapLayer,
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
        m3DModelMapLayer = (IMc3DModelMapLayer) obj;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
         outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(m3DModelMapLayer));
    }

    @Override
    public void onSelectViewportFromList(IMcMapViewport mcSelectedViewport, boolean isChecked) {
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
