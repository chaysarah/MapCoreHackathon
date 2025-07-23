package com.elbit.mapcore.mcandroidtester.ui.device.fragments;

import android.content.Context;
import android.content.DialogInterface;
import android.net.Uri;
import android.os.Bundle;
import androidx.annotation.Nullable;
import com.google.android.material.textfield.TextInputEditText;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.Toast;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.model.AMCTMapDevice;
import com.elbit.mapcore.mcandroidtester.ui.adapters.ListViewTwoColumnsAdapter;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;

import java.util.ArrayList;
import java.util.HashMap;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.General.ObjectRef;
import com.elbit.mapcore.General.WrapperPrimitiveTypes.WrapperString;
import com.elbit.mapcore.Interfaces.Map.IMcMapDevice;
import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;

import static com.elbit.mapcore.mcandroidtester.utils.Consts.FIRST_COLUMN;
import static com.elbit.mapcore.mcandroidtester.utils.Consts.SECOND_COLUMN;

;


/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link LocalCacheFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link LocalCacheFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class LocalCacheFragment extends Fragment {
    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;
    private ArrayList<HashMap<String, String>> list;
    private OnFragmentInteractionListener mListener;

    public LocalCacheFragment() {
        // Required empty public constructor
    }

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment LocalCacheFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static LocalCacheFragment newInstance(String param1, String param2) {
        LocalCacheFragment fragment = new LocalCacheFragment();
        Bundle args = new Bundle();
        args.putString(ARG_PARAM1, param1);
        args.putString(ARG_PARAM2, param2);
        fragment.setArguments(args);
        return fragment;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if (getArguments() != null) {
            mParam1 = getArguments().getString(ARG_PARAM1);
            mParam2 = getArguments().getString(ARG_PARAM2);
        }
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_local_cache, container, false);
    }

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        InitializeComponents();
    }

    private void InitializeComponents() {
        ListView listView = (ListView) this.getActivity().findViewById(R.id.ListView1);

        list = new ArrayList<HashMap<String, String>>();

        HashMap<String, String> temp = new HashMap<String, String>();
        //temp.put(FIRST_COLUMN, "C:\\Prj\\MapCore7\\Ogre1.12.12\\Development\\Jni");
        //temp.put(SECOND_COLUMN, "C:\\Prj\\MapCore7\\Ogre1.12.12\\Development\\Jni");
        list.add(temp);

        HashMap<String, String> temp2 = new HashMap<String, String>();
        temp2.put(FIRST_COLUMN, "Rajat Ghai");
        temp2.put(SECOND_COLUMN, "Male");
        list.add(temp2);

        HashMap<String, String> temp3 = new HashMap<String, String>();
        temp3.put(FIRST_COLUMN, "Karina Kaif");
        temp3.put(SECOND_COLUMN, "Female");
        list.add(temp3);

        ListViewTwoColumnsAdapter adapter = new ListViewTwoColumnsAdapter(this.getActivity(), list);
        listView.setAdapter(adapter);

        // btnRemoveMapLayerFromLocalCache
        Button btnRemoveMapLayerFromLocalCache = (Button) this.getActivity().findViewById(R.id.btnRemoveMapLayerFromLocalCache);
        btnRemoveMapLayerFromLocalCache.setOnClickListener(onRemoveMapLayerFromLocalCacheClick);

        // btnSetMapLayersLocalCacheSize
        Button btnSetMapLayersLocalCacheSize = (Button) this.getActivity().findViewById(R.id.btnSetMapLayersLocalCacheSize);
        btnSetMapLayersLocalCacheSize.setOnClickListener(onSetMaxSizeClick);

        // btnGetLocalCacheParams
        Button btnGetLocalCacheParams = (Button) this.getActivity().findViewById(R.id.btnGetLocalCacheParams);
        btnGetLocalCacheParams.setOnClickListener(onGetLocalCacheParams);

        // btnRemoveMapLayersLocalCache
        Button btnRemoveMapLayersLocalCache = (Button) this.getActivity().findViewById(R.id.btnRemoveMapLayersLocalCache);
        btnRemoveMapLayersLocalCache.setOnClickListener(onRemoveMapLayersLocalCache);

        loadLocalCacheParams(false);
    }

    protected View.OnClickListener onSetMaxSizeClick = new View.OnClickListener()
    {
        @Override
        public void onClick(View v) {

            Funcs.runMapCoreFunc(new Runnable() {
                @Override
                public void run() {
                    try {
                        NumericEditTextLabel editTextMaxSize = (NumericEditTextLabel)getActivity().findViewById(R.id.txtMaxSizeLocalCacheParams);
                        IMcMapDevice.Static.SetMapLayersLocalCacheSize(editTextMaxSize.getUInt());
                    }
                    catch (MapCoreException e){
                        AlertMessages.ShowMapCoreErrorMessage(getActivity(),e,"McMapDevice.SetMapLayersLocalCacheSize");
                    }
                    catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            });

        }
    };

    protected View.OnClickListener onRemoveMapLayerFromLocalCacheClick = new View.OnClickListener()
    {
        @Override
        public void onClick(View v) {
            try {
                String strMsg = "Are you sure to remove map layer from local cache?";
                TextInputEditText editText = (TextInputEditText) getActivity().findViewById(R.id.txtLocalCacheSubFolder);
                final String txtSubFolder = editText.getText().toString();
                if (txtSubFolder.isEmpty())
                    strMsg = "Are you sure to remove all map layers from local cache?";

                DialogInterface.OnClickListener OnRemoveMapLayerFromLocalCacheYes = new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        try {
                            String txtSubFolder = ((EditText) getActivity().findViewById(R.id.txtLocalCacheSubFolder)).getText().toString();
                            IMcMapDevice.Static.RemoveMapLayerFromLocalCache(txtSubFolder);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "McMapDevice.RemoveMapLayerFromLocalCache");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                };
                AlertMessages.ShowYesNoMessage(getActivity(), "Remove", strMsg, OnRemoveMapLayerFromLocalCacheYes);
            }
            catch (Exception e) {
                e.printStackTrace();
            }
        }
    };

    private void loadLocalCacheParams(boolean isLoadLayersParams)
    {
        WrapperString strLocalCacheFolder = new WrapperString();
        ObjectRef<Integer> iLocalCacheMaxSize = new ObjectRef<>();
        ObjectRef<Integer> iLocalCacheCurrentSize = new ObjectRef<>();

        ObjectRef<IMcMapLayer.SLocalCacheLayerParams[]> layerParams = new ObjectRef<IMcMapLayer.SLocalCacheLayerParams[]>();
        list = new ArrayList<HashMap<String, String>>();

        try {
            if (!isLoadLayersParams)
                IMcMapDevice.Static.GetMapLayersLocalCacheParams(strLocalCacheFolder, iLocalCacheMaxSize, iLocalCacheCurrentSize);
//            else
//                IMcMapDevice.Static.GetMapLayersLocalCacheParams(strLocalCacheFolder, iLocalCacheMaxSize, iLocalCacheCurrentSize, layerParams);

            if (strLocalCacheFolder.toString().isEmpty())
                AMCTMapDevice.getInstance().IsExistLocalCache = false;
            else
                AMCTMapDevice.getInstance().IsExistLocalCache = true;

            ((EditText) getActivity().findViewById(R.id.txtLocalCacheSubFolder)).setText(strLocalCacheFolder.GetValue());
            ((NumericEditTextLabel) getActivity().findViewById(R.id.txtMaxSizeLocalCacheParams)).setUInt(iLocalCacheMaxSize.getValue());
            ((NumericEditTextLabel) getActivity().findViewById(R.id.txtCurrentSizeLocalCacheParams)).setUInt(iLocalCacheCurrentSize.getValue());

            ListView listView = (ListView) this.getActivity().findViewById(R.id.ListView1);
            listView.removeAllViews();
            HashMap<String, String> temp;
            /*for (IMcMapLayer.SLocalCacheLayerParams params : layerParams) {
                temp = new HashMap<String, String>();
                temp.put(FIRST_COLUMN, params.strLocalCacheSubFolder);
                temp.put(SECOND_COLUMN, params.strOriginalFolder);
                list.add(temp);
            }*/
            ListViewTwoColumnsAdapter adapter = new ListViewTwoColumnsAdapter(this.getActivity(), list);
            listView.setAdapter(adapter);
        }
        catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "McMapDevice.GetMapLayersLocalCacheParams");
        }catch (Exception e) {
            e.printStackTrace();
        }
    }

    protected View.OnClickListener onGetLocalCacheParams = new View.OnClickListener()
    {
        @Override
        public void onClick(View v) {
            loadLocalCacheParams(true);
        }
    };

    protected View.OnClickListener onRemoveMapLayersLocalCache = new View.OnClickListener()
    {
        @Override
        public void onClick(View v) {
            try {
                DialogInterface.OnClickListener OnRemoveMapLayerLocalCacheYes = new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        try {
                            IMcMapDevice.Static.RemoveMapLayersLocalCache();
                            Toast.makeText(getActivity(), "Remove Map Layers Local Cache Succeed",Toast.LENGTH_SHORT);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowErrorMessage(getActivity(), e.getMessage(), "McMapDevice.RemoveMapLayersLocalCache");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                };
                AlertMessages.ShowYesNoMessage(getActivity(), "Remove Map Layers Local Cache", "Are you sure to remove map layers local cache?", OnRemoveMapLayerLocalCacheYes);
            }
            catch (Exception e) {
                e.printStackTrace();
            }
        }
    };

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
