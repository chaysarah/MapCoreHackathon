package com.elbit.mapcore.mcandroidtester.ui.map.fragments;

import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.CheckedTextView;
import android.widget.ListView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_AMCTMapForm;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_MCLayers;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_MCTerrain;
import com.elbit.mapcore.mcandroidtester.model.AMCTMapTerrain;
import com.elbit.mapcore.mcandroidtester.model.AMCTViewPort;
import com.elbit.mapcore.mcandroidtester.ui.adapters.AMCTArrayAdapter;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.LayersActivity;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcMapTerrain;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;

import static com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT;

public class LayersFragment extends Fragment implements FragmentWithObject {

    private OnFragmentInteractionListener mListener;
    private ListView mLayersLV;
    private AMCTArrayAdapter mLayersHMA;
    private IMcMapTerrain mMapTerrain;
    private Button mFinishLayerBttn;
    private IMcMapViewport mMapViewport;
    private boolean mIsOpenNewVP;
    private boolean mIsOpenNewLayer = false;
    public static final String AMCT_SERIALIZABLE_OBJECT_VP = AMCT_SERIALIZABLE_OBJECT+"_VP";
    public static final String AMCT_SERIALIZABLE_OBJECT_IS_NEW_VP = AMCT_SERIALIZABLE_OBJECT+"_IS_NEW_VP";

    public static LayersFragment newInstance() {
        LayersFragment fragment = new LayersFragment();
        return fragment;
    }

    public static LayersFragment newInstance(Integer viewportHashcode, boolean isOpenNewVP) {
        LayersFragment fragment = new LayersFragment();
        fragment.mMapViewport = Manager_AMCTMapForm.getInstance().getViewportById(viewportHashcode);
        fragment.mIsOpenNewVP = isOpenNewVP;
        return fragment;
    }

    public LayersFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View inflaterView=inflater.inflate(R.layout.fragment_layers, container, false);

        Funcs.SetObjectFromBundle(savedInstanceState, this );
        if(savedInstanceState != null)
        {
            AMCTSerializableObject mcObject = (AMCTSerializableObject)savedInstanceState.getSerializable(AMCT_SERIALIZABLE_OBJECT_VP);
            if(mcObject != null)
                mMapViewport = (IMcMapViewport) mcObject.getMcObject();

            mIsOpenNewVP = Boolean.parseBoolean(savedInstanceState.getString(AMCT_SERIALIZABLE_OBJECT_IS_NEW_VP));
        }
        AMCTMapTerrain.getInstance().removeAllLayers();

        initLayersLV(inflaterView);
        initAddLayerBttn(inflaterView);
        initFinishBttn(inflaterView);
        return inflaterView;
    }

    private void initFinishBttn(View inflaterView) {
        mFinishLayerBttn = (Button) inflaterView.findViewById(R.id.finish_layers_bttn);
        mFinishLayerBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Funcs.runMapCoreFunc(new Runnable() {
                    @Override
                    public void run() {
                        if (mMapTerrain == null) {
                            IMcMapTerrain mcMapTerrain = Manager_MCTerrain.getInstance().CreateTerrain(
                                    AMCTMapTerrain.getInstance().getmGridCoordinateSystem(),
                                    AMCTMapTerrain.getInstance().getMapLayersAsArr(), getContext());
                            if (mcMapTerrain != null) {
                                if (mMapViewport != null) {
                                    try {
                                        mMapViewport.AddTerrain(mcMapTerrain);
                                    } catch (MapCoreException e) {
                                        e.printStackTrace();
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "AddTerrain");
                                    } catch (Exception e) {
                                        e.printStackTrace();
                                    }
                                } else if (mIsOpenNewVP)
                                    AMCTViewPort.getViewportInCreation().addTerrainToList(mcMapTerrain);
                            } else {
                                return;
                            }
                        }
                        // add layers to exist terrain
                        else {
                            try {
                                for (IMcMapLayer layer : AMCTMapTerrain.getInstance().getmMapLayers()) {
                                    mMapTerrain.AddLayer(layer);
                                    Manager_MCLayers.getInstance().removeStandaloneLayer(layer);
                                }
                            } catch (MapCoreException McEx) {
                                McEx.printStackTrace();
                                AlertMessages.ShowMapCoreErrorMessage(getContext(), McEx, "AddLayer");
                                return;
                            } catch (Exception e) {
                                e.printStackTrace();
                            }
                        }
                        AMCTMapTerrain.getInstance().removeAllLayers();
                        getActivity().runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                getActivity().onBackPressed();
                            }
                        });
                    }
                });
            }
        });
    }

    private void initAddLayerBttn(View inflaterView) {
        inflaterView.findViewById(R.id.add_layer_bttn).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                mIsOpenNewLayer = true;
                Intent intent=new Intent(LayersFragment.this.getActivity(),LayersActivity.class);
                startActivity(intent);
            }
        });
    }

    @Override
    public void setUserVisibleHint(boolean visible)
    {
        super.setUserVisibleHint(visible);
        if (visible && isResumed())
        {
            //Only manually call onResume if fragment is already visible
            //Otherwise allow natural fragment lifecycle to call onResume
            onResume();
        }
    }
    private void initLayersLV(View inflaterView) {
        mLayersLV=(ListView)(inflaterView.findViewById(R.id.layers_lv));
        mLayersLV.setItemsCanFocus(false);
        mLayersHMA = new AMCTArrayAdapter(getActivity(), R.layout.checkable_list_item, Manager_MCLayers.getInstance().getAllLayers());
        mLayersLV.setAdapter(mLayersHMA);
        mLayersLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                IMcMapLayer item = (IMcMapLayer) mLayersLV.getAdapter().getItem(position);
                if(((CheckedTextView)view).isChecked())
                    AMCTMapTerrain.getInstance().addLayer(item);
                else
                    AMCTMapTerrain.getInstance().removeLayer(item);
            }
        });
        Funcs.setListViewHeightBasedOnChildren(mLayersLV);
        selectCurLayers();
    }

    private void selectCurLayers() {
        if (AMCTMapTerrain.getInstance() != null && AMCTMapTerrain.getInstance().getSize() > 0) {
            for (int i = 0; i < mLayersHMA.getCount(); i++) {
                if (AMCTMapTerrain.getInstance().containsLayer((IMcMapLayer) mLayersHMA.getItem(i)))
                    mLayersLV.setItemChecked(i, true);
            }
        }
    }

    public void onButtonPressed(Uri uri) {
        if (mListener != null) {
            mListener.onFragmentInteraction(uri);
        }
    }

    @Override
    public void onResume() {
        super.onResume();
        mIsOpenNewLayer = false;
        if (!getUserVisibleHint())
        {
            return;
        }
        if (mLayersHMA != null && mLayersLV != null) {
            initLayersLV(getView());
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
    }

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
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
        if(!mIsOpenNewLayer) {
            outState.putSerializable(AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mMapTerrain));
            outState.putSerializable(AMCT_SERIALIZABLE_OBJECT_VP, new AMCTSerializableObject(mMapViewport));
            outState.putString(AMCT_SERIALIZABLE_OBJECT_IS_NEW_VP, String.valueOf(mIsOpenNewVP));
        }
    }

    public interface OnFragmentInteractionListener {
        // TODO: Update argument type and name
        void onFragmentInteraction(Uri uri);
    }

    @Override
    public boolean getUserVisibleHint() {
        return super.getUserVisibleHint();
    }

}
