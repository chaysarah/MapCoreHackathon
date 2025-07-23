package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.layers_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentTransaction;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ListView;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;
import com.elbit.mapcore.Interfaces.Map.IMcRawRasterMapLayer;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.ui.adapters.RawLayerComponentAdapter;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.RawLayerComponentsFragment;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

import java.util.Arrays;
import java.util.List;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link RawRasterMapLayerFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link RawRasterMapLayerFragment#newInstance} factory method to
 * create an instance of this fragment.
 *
 */
public class RawRasterMapLayerFragment extends Fragment implements FragmentWithObject {

    private OnFragmentInteractionListener mListener;
    private IMcRawRasterMapLayer mRawRasterMapLayer;
    private Button mSaveBttn;
    private ListView mGetComponentLV;
    private Button mGetComponentBttn;
    private RawLayerComponentsFragment rawLayerComponentsFragment;
    private View mView;

    public static RawRasterMapLayerFragment newInstance() {
        RawRasterMapLayerFragment fragment = new RawRasterMapLayerFragment();
        return fragment;
    }

    public RawRasterMapLayerFragment() {
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
        mView = inflater.inflate(R.layout.fragment_raw_raster_map_layer, container, false);
        Funcs.SetObjectFromBundle(savedInstanceState, this );
        InitControls();
        addComponenetFragment();
        initGetComponentList();
        return mView;
    }

    private void addComponenetFragment() {
        FragmentTransaction transaction;
        rawLayerComponentsFragment = (RawLayerComponentsFragment) getActivity().getSupportFragmentManager().findFragmentByTag("rawLayerComponentsFragment");
        if (rawLayerComponentsFragment == null) {
            rawLayerComponentsFragment = RawLayerComponentsFragment.newInstance();
            transaction = getActivity().getSupportFragmentManager().beginTransaction().add(R.id.raw_raster_layer_params_container, rawLayerComponentsFragment, "rawLayerComponentsFragment");
        } else
            transaction = getActivity().getSupportFragmentManager().beginTransaction().show(rawLayerComponentsFragment);

        transaction.addToBackStack("rawLayerComponentsFragment");
        transaction.commit();
    }


    private void InitControls()
    {
        mSaveBttn = (Button) mView.findViewById(R.id.raw_raster_save_changes_bttn);
        mGetComponentLV = (ListView) mView.findViewById(R.id.raw_layers_get_components_lv);

        mGetComponentBttn = (Button) mView.findViewById(R.id.raw_layers_get_components_bttn);
        mGetComponentBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                initGetComponentList();
            }
        });
        mSaveBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                List<IMcMapLayer.SComponentParams> componentParamsList = rawLayerComponentsFragment.getmCompParamsList();
                IMcMapLayer.SComponentParams[] componentParams = componentParamsList.toArray(new IMcMapLayer.SComponentParams[componentParamsList.size()]);
                try {
                    mRawRasterMapLayer.AddComponents(componentParams);
                }catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "AddComponents()");
                } catch (Exception e) {
                    e.printStackTrace();
                }

            }
        });
    }

    private void initGetComponentList()
    {
        try{
           IMcMapLayer.SComponentParams[] components =  mRawRasterMapLayer.GetComponents();
           mGetComponentLV.setAdapter(new RawLayerComponentAdapter(getContext(), R.layout.list_item, Arrays.asList(components)));
            Funcs.setListViewHeightBasedOnChildren(mGetComponentLV);
        }
        catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetComponents()");
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
        mRawRasterMapLayer = (IMcRawRasterMapLayer) obj;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
         outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mRawRasterMapLayer));
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
