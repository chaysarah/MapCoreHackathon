package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.terrain_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import android.util.SparseBooleanArray;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CheckedTextView;
import android.widget.LinearLayout;
import android.widget.ListView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcMapTerrain;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link LayersDetailsFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link LayersDetailsFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class LayersDetailsFragment extends Fragment implements FragmentWithObject {

    private OnFragmentInteractionListener mListener;
    private IMcMapTerrain mMapTerrain;
    private ListView mLayersLV;
    private CheckBox mVisibilityCB;
    private CheckBox mNearestPixelMagFilterCB;
    private NumericEditTextLabel mMinScaleET;
    private NumericEditTextLabel mMaxScaleET;
    private NumericEditTextLabel mDrawPriorityET;
    private NumericEditTextLabel mTransparencyET;
    private IMcMapLayer[] mCurLayers;
    private ArrayAdapter<IMcMapLayer> mLayersAdapter;
    private Button mCheckLayersToRemoveBttn;
    private int mIndex;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment LayersDetailsFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static LayersDetailsFragment newInstance() {
        LayersDetailsFragment fragment = new LayersDetailsFragment();
        return fragment;
    }

    public LayersDetailsFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        initLayerDetailsViews();
        loadLayersList();
        initBttns();
    }

    private void initBttns() {
        Button setLayerParamsBttn = (Button) getView().findViewById(R.id.layer_detail_ok_bttn);
        setLayerParamsBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                setLayersParams((IMcMapLayer) (mLayersLV.getAdapter().getItem(mIndex)));
            }
        });
        mCheckLayersToRemoveBttn = (Button) getView().findViewById(R.id.layer_detail_removeLayers_bttn);
        mCheckLayersToRemoveBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (mLayersLV.getChoiceMode() == ListView.CHOICE_MODE_MULTIPLE) {
                    removeCheckedLayersFromTerrain();
                    setLayersParamsVisibility((LinearLayout)getView().findViewById(R.id.layer_details_layer_params_ll),true);
                    toggleListMode(ListView.CHOICE_MODE_SINGLE, R.layout.radio_bttn_list_item,"check layers to remove");
                }
                else
                if (mLayersLV.getChoiceMode() == ListView.CHOICE_MODE_SINGLE) {
                    setLayersParamsVisibility((LinearLayout)getView().findViewById(R.id.layer_details_layer_params_ll),false);
                    toggleListMode(ListView.CHOICE_MODE_MULTIPLE, R.layout.checkable_list_item,"remove selected layers");
                }

                }
        });
    }

    private void setLayersParamsVisibility(ViewGroup layerParamsGroup, boolean isEnable) {
        Funcs.enableDisableViewGroup(layerParamsGroup,isEnable);
    }

    private void toggleListMode(int choiceMode, int resId, String bttnTxt) {
        mLayersAdapter = new ArrayAdapter<>(getActivity(), resId, mCurLayers);
        mLayersLV.setChoiceMode(choiceMode);
        mLayersLV.setAdapter(mLayersAdapter);
        mLayersLV.invalidate();
        mLayersLV.invalidateViews();
        mCheckLayersToRemoveBttn.setText(bttnTxt);
    }

    private void removeCheckedLayersFromTerrain() {
        SparseBooleanArray checked = null;
        int len = mLayersLV.getCount();
        checked = mLayersLV.getCheckedItemPositions();
        for (int i = 0; i < len; i++) {
            if (checked.get(i)) {
                IMcMapLayer item = mCurLayers[i];
                try {
                    mMapTerrain.RemoveLayer(item);
                    mCurLayers=mMapTerrain.GetLayers();
                    mLayersAdapter.notifyDataSetChanged();
                }
                catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getActivity(),e,"mMapTerrain.RemoveLayer");
                    e.printStackTrace();
                }catch (Exception e) {
                    e.printStackTrace();
                }
            }
        }
    }

    private void setLayersParams(final IMcMapLayer selectedItem) {
        IMcMapTerrain.SLayerParams sLayerParams = new IMcMapTerrain.SLayerParams();

        sLayerParams.bVisibility = mVisibilityCB.isChecked();
        sLayerParams.byTransparency = mTransparencyET.getByte();
        sLayerParams.fMaxScale = mMaxScaleET.getFloat();
        sLayerParams.fMinScale = mMinScaleET.getFloat();
        sLayerParams.nDrawPriority = mDrawPriorityET.getByte();
        sLayerParams.bNearestPixelMagFilter = mNearestPixelMagFilterCB.isChecked();
        final IMcMapTerrain.SLayerParams finalLayerParams = sLayerParams;

        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    mMapTerrain.SetLayerParams(selectedItem, finalLayerParams);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "mMapTerrain.SetLayerParams()");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void initLayerDetailsViews() {
        View fragView = getView();
        mVisibilityCB = (CheckBox) fragView.findViewById(R.id.layer_detail_visibility_cb);
        mNearestPixelMagFilterCB = (CheckBox) fragView.findViewById(R.id.layer_detail_NearestPixelMagFilter_cb);
        mMinScaleET = (NumericEditTextLabel) fragView.findViewById(R.id.layer_detail_min_scale_visibility);
        mMaxScaleET = (NumericEditTextLabel) fragView.findViewById(R.id.layer_detail_max_scale_visibility);
        mDrawPriorityET = (NumericEditTextLabel) fragView.findViewById(R.id.layer_detail_draw_priority);
        mTransparencyET = (NumericEditTextLabel) fragView.findViewById(R.id.layer_detail_transparency);
        setLayerParamsValues(new IMcMapTerrain.SLayerParams());
    }

    private void setLayerParamsValues(IMcMapTerrain.SLayerParams layerParams) {
        mMaxScaleET.setFloat(layerParams.fMaxScale);
        mMinScaleET.setFloat(layerParams.fMinScale);
        mDrawPriorityET.setByte(layerParams.nDrawPriority);
        mTransparencyET.setByte(layerParams.byTransparency);
        mVisibilityCB.setChecked(layerParams.bVisibility);
        mNearestPixelMagFilterCB.setChecked(layerParams.bNearestPixelMagFilter);
    }

    private void loadLayersList() {
        try {
            mCurLayers = mMapTerrain.GetLayers();
            mLayersAdapter = new ArrayAdapter<>(getActivity(), R.layout.radio_bttn_list_item, mCurLayers);
            mLayersLV = (ListView) getView().findViewById(R.id.layers_details_layers_lv);
            mLayersLV.setChoiceMode(ListView.CHOICE_MODE_SINGLE);
            mLayersLV.setAdapter(null);
            mLayersLV.setAdapter(mLayersAdapter);
            mLayersLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                @Override
                public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                    if (((CheckedTextView) view).isChecked()) {
                        IMcMapLayer item = (IMcMapLayer) mLayersLV.getAdapter().getItem(position);
                        mIndex = position;
                        try {
                            IMcMapTerrain.SLayerParams layerParams = mMapTerrain.GetLayerParams(item);
                            setLayerParamsValues(layerParams);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "mMapTerrain.GetLayerParams()");
                            e.printStackTrace();
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                }
            });
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "mMapTerrain.GetLayers()");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        getActivity().setTitle("layers details");
        Funcs.SetObjectFromBundle(savedInstanceState, this );
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_layers_details, container, false);
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
            throw new RuntimeException(context.toString()
                    + " must implement OnFragmentInteractionListener");
        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    @Override
    public void setObject(Object obj) {
        mMapTerrain = (IMcMapTerrain) obj;
    }


    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mMapTerrain));
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

    private void selectCurrLayers(ArrayAdapter<IMcMapLayer> layersAdapter, IMcMapLayer[] curLayers) {
        for (int i = 0; i < layersAdapter.getCount(); i++) {
            for (IMcMapLayer layer : curLayers) {
                if (layersAdapter.getItem(i).equals(layer))
                    mLayersLV.setItemChecked(i, true);
            }
        }
    }
}
