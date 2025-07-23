package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.layers_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.ListView;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Map.IMcVectorMapLayer;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.model.AMCTMapLayerAsyncOperationCallback;
import com.elbit.mapcore.mcandroidtester.ui.adapters.HashMapAdapter;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;

import java.util.Arrays;
import java.util.HashMap;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link VectorMetadataMapLayerFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link VectorMetadataMapLayerFragment#newInstance} factory method to
 * create an instance of this fragment.
 *
 */
public class VectorMetadataMapLayerFragment extends Fragment implements FragmentWithObject  {

    private Context mContext;

    private OnFragmentInteractionListener mListener;
    public IMcVectorMapLayer mVectorsMapLayer;
    private NumericEditTextLabel mVectorItemsCountET;
    private Button mVectorItemsCountBtn;
    private NumericEditTextLabel mVectorItemIdET;
    private NumericEditTextLabel mFieldIdET;
    private NumericEditTextLabel mVectorItemValueET;
    private CheckBox mAsyncCB;
    private Button mGetValueByIntBtn;
    private Button mGetValueByDoubleBtn;
    private Button mGetValueByStringBtn;
    private Button mGetValueByWStringBtn;

    private NumericEditTextLabel mUniqueFieldIdET;
    private CheckBox mUniqueAsync;

    private Button mGetUniqueValueByIntBtn;
    private Button mGetUniqueValueByDoubleBtn;
    private Button mGetUniqueValueByStringBtn;
    private Button mGetUniqueValueByWStringBtn;

    private ListView mUniqueValuesList;
    private View mView;
    private HashMapAdapter mUniqueValuesAdapter;
    private ArrayAdapter<String> mUniqueValuesAdapter2;

    private Button mClearBtn;

    public static VectorMetadataMapLayerFragment newInstance() {
        VectorMetadataMapLayerFragment fragment = new VectorMetadataMapLayerFragment();
        return fragment;
    }

    public VectorMetadataMapLayerFragment() {
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
        mView = inflater.inflate(R.layout.fragment_vector_metadata_map_layer, container, false);
        //Funcs.SetObjectFromBundle(savedInstanceState, this );
        AMCTMapLayerAsyncOperationCallback.getInstance(this.getContext()).VectorMetadataMapLayerFragment = this;
        mContext = this.getContext();
        InitControls();
        InitControlsOperations();

        return mView;
    }

   private void InitControls()
    {
        mVectorItemsCountBtn = (Button) mView.findViewById(R.id.vector_map_layer_get_vector_items_count);
        mVectorItemsCountET = (NumericEditTextLabel) mView.findViewById(R.id.vector_map_layer_vector_items_count);

        mVectorItemIdET = (NumericEditTextLabel) mView.findViewById(R.id.vector_map_layer_vector_item_id);
        mFieldIdET = (NumericEditTextLabel) mView.findViewById(R.id.vector_map_layer_field_id);
        mAsyncCB = (CheckBox) mView.findViewById(R.id.vector_map_layer_get_value_async_btn);
        mVectorItemValueET = (NumericEditTextLabel) mView.findViewById(R.id.vector_map_layer_vector_item_value);

        mGetValueByIntBtn = (Button) mView.findViewById(R.id.vector_map_layer_get_value_as_int_btn);
        mGetValueByDoubleBtn = (Button) mView.findViewById(R.id.vector_map_layer_get_value_as_double_btn);
        mGetValueByStringBtn = (Button) mView.findViewById(R.id.vector_map_layer_get_value_as_string_btn);
        mGetValueByWStringBtn = (Button) mView.findViewById(R.id.vector_map_layer_get_value_as_wstring_btn);

        mUniqueFieldIdET = (NumericEditTextLabel) mView.findViewById(R.id.vector_map_layer_unique_field_id);
        mUniqueAsync = (CheckBox) mView.findViewById(R.id.vector_map_layer_unique_async_btn);

        mGetUniqueValueByIntBtn = (Button) mView.findViewById(R.id.vector_map_layer_get_unique_value_as_int_btn);
        mGetUniqueValueByDoubleBtn = (Button) mView.findViewById(R.id.vector_map_layer_get_unique_value_as_double_btn);
        mGetUniqueValueByStringBtn = (Button) mView.findViewById(R.id.vector_map_layer_get_unique_value_as_string_btn);
        mGetUniqueValueByWStringBtn = (Button) mView.findViewById(R.id.vector_map_layer_get_unique_value_as_wstring_btn);
        mUniqueValuesList = (ListView) mView.findViewById(R.id.vector_map_layer_unique_lv);
        mClearBtn = (Button) mView.findViewById(R.id.vector_map_layer_unique_list_clear_bttn);

    }

    public void InitUniqueValuesList() {
        mUniqueValuesAdapter = new HashMapAdapter(null, Consts.ListType.NON_CHECK, true);
        mUniqueValuesList.setChoiceMode(ListView.CHOICE_MODE_SINGLE);
        mUniqueValuesList.setAdapter(mUniqueValuesAdapter);
        mUniqueValuesList.deferNotifyDataSetChanged();
        Funcs.setListViewHeightBasedOnChildren(mUniqueValuesList);
    }

    private boolean CheckNegativeValue(NumericEditTextLabel numericTextBox, String fieldName)
    {
        if (numericTextBox.getInt() < 0)
        {
            AlertMessages.ShowMessage(this.getActivity(), fieldName + " cannot be negative", "Invalid value");
            numericTextBox.setFocusable(true);
            return true;
        }
        return false;
    }

    private void InitControlsOperations() {
        InitUniqueValuesList();
        mVectorItemsCountBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                try {
                    mVectorItemsCountET.setUInt(mVectorsMapLayer.GetVectorItemsCount());
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetVectorItemsCount");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });

        InitGetVectorItemFieldValueControls();

        InitGetVectorItemFieldUniqueValueControls();
    }

    private void InitGetVectorItemFieldValueControls()
    {
        mGetValueByIntBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (CheckNegativeValue(mVectorItemIdET, "Vector Item Id") || CheckNegativeValue(mFieldIdET, "Field Id"))
                    return;
                try {
                    if (mAsyncCB.isChecked()) {
                        mVectorsMapLayer.GetVectorItemFieldValueAsInt(
                                mVectorItemIdET.getLong(),
                                mFieldIdET.getUInt(),
                                AMCTMapLayerAsyncOperationCallback.getInstance(mContext));
                    } else {
                        int res = mVectorsMapLayer.GetVectorItemFieldValueAsInt(mVectorItemIdET.getLong(),
                                mFieldIdET.getUInt());
                        GetVectorItemFieldValuesAsInt(res);
                    }
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetVectorItemFieldValueAsInt");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });

        mGetValueByDoubleBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (CheckNegativeValue(mVectorItemIdET, "Vector Item Id") || CheckNegativeValue(mFieldIdET, "Field Id"))
                    return;
                try {
                    if (mAsyncCB.isChecked()) {
                        mVectorsMapLayer.GetVectorItemFieldValueAsDouble(
                                mVectorItemIdET.getLong(),
                                mFieldIdET.getUInt(),
                                AMCTMapLayerAsyncOperationCallback.getInstance(mContext));
                    } else {
                        double res = mVectorsMapLayer.GetVectorItemFieldValueAsDouble(mVectorItemIdET.getLong(),
                                mFieldIdET.getUInt());
                        GetVectorItemFieldValuesAsDouble(res);
                    }
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetVectorItemFieldValueAsDouble");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });

        mGetValueByStringBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (CheckNegativeValue(mVectorItemIdET, "Vector Item Id") || CheckNegativeValue(mFieldIdET, "Field Id"))
                    return;
                try {
                    if (mAsyncCB.isChecked()) {
                        mVectorsMapLayer.GetVectorItemFieldValueAsString(
                                mVectorItemIdET.getLong(),
                                mFieldIdET.getUInt(),
                                AMCTMapLayerAsyncOperationCallback.getInstance(mContext));
                    } else {
                        String res = mVectorsMapLayer.GetVectorItemFieldValueAsString(mVectorItemIdET.getLong(),
                                mFieldIdET.getUInt());
                        GetVectorItemFieldValuesAsString(res);
                    }
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetVectorItemFieldValueAsString");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });

        mGetValueByWStringBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (CheckNegativeValue(mVectorItemIdET, "Vector Item Id") || CheckNegativeValue(mFieldIdET, "Field Id"))
                    return;
                try {
                    if (mAsyncCB.isChecked()) {
                        mVectorsMapLayer.GetVectorItemFieldValueAsWString(
                                mVectorItemIdET.getLong(),
                                mFieldIdET.getUInt(),
                                AMCTMapLayerAsyncOperationCallback.getInstance(mContext));
                    } else {
                        String res = mVectorsMapLayer.GetVectorItemFieldValueAsWString(mVectorItemIdET.getLong(),
                                mFieldIdET.getUInt());
                        GetVectorItemFieldValuesAsString(res);
                    }
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetVectorItemFieldValueAsWString");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void InitGetVectorItemFieldUniqueValueControls() {

        mGetUniqueValueByIntBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (CheckNegativeValue(mUniqueFieldIdET, "Field Id"))
                    return;
                try {
                    if (mAsyncCB.isChecked()) {
                        mVectorsMapLayer.GetFieldUniqueValuesAsInt(mUniqueFieldIdET.getUInt(),
                                AMCTMapLayerAsyncOperationCallback.getInstance(mContext));
                    } else {
                        int[] res = mVectorsMapLayer.GetFieldUniqueValuesAsInt(mUniqueFieldIdET.getUInt());
                        GetFieldUniqueValuesAsInt(res);
                    }
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetFieldUniqueValuesAsInt");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });

        mGetUniqueValueByDoubleBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (CheckNegativeValue(mUniqueFieldIdET, "Field Id"))
                    return;
                try {
                    if (mAsyncCB.isChecked()) {
                        mVectorsMapLayer.GetFieldUniqueValuesAsDouble(mUniqueFieldIdET.getUInt(),
                                AMCTMapLayerAsyncOperationCallback.getInstance(mContext));
                    } else {
                        double[] res = mVectorsMapLayer.GetFieldUniqueValuesAsDouble(mUniqueFieldIdET.getUInt());
                        GetFieldUniqueValuesAsDouble(res);
                    }
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetFieldUniqueValuesAsDouble");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });

        mGetUniqueValueByStringBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (CheckNegativeValue(mUniqueFieldIdET, "Field Id"))
                    return;
                try {
                    if (mAsyncCB.isChecked()) {
                        mVectorsMapLayer.GetFieldUniqueValuesAsString(mUniqueFieldIdET.getUInt(),
                                AMCTMapLayerAsyncOperationCallback.getInstance(mContext));
                    } else {
                        String[] res = mVectorsMapLayer.GetFieldUniqueValuesAsString(mUniqueFieldIdET.getUInt(),
                                AMCTMapLayerAsyncOperationCallback.getInstance(mContext));
                        GetFieldUniqueValuesAsString(res);
                    }
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetFieldUniqueValuesAsString");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });

        mGetUniqueValueByWStringBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (CheckNegativeValue(mUniqueFieldIdET, "Field Id"))
                    return;
                try {
                    if (mAsyncCB.isChecked()) {
                        mVectorsMapLayer.GetFieldUniqueValuesAsWString(mUniqueFieldIdET.getUInt(),
                                AMCTMapLayerAsyncOperationCallback.getInstance(mContext));
                    } else {
                        String[] res = mVectorsMapLayer.GetFieldUniqueValuesAsWString(mUniqueFieldIdET.getUInt(),
                                AMCTMapLayerAsyncOperationCallback.getInstance(mContext));
                        GetFieldUniqueValuesAsString(res);
                    }
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetFieldUniqueValuesAsWString");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });

        mGetUniqueValueByWStringBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (CheckNegativeValue(mUniqueFieldIdET, "Field Id"))
                    return;
                try {
                    if (mAsyncCB.isChecked()) {
                        mVectorsMapLayer.GetFieldUniqueValuesAsWString(mUniqueFieldIdET.getUInt(),
                                AMCTMapLayerAsyncOperationCallback.getInstance(mContext));
                    } else {
                        String[] res = mVectorsMapLayer.GetFieldUniqueValuesAsWString(mUniqueFieldIdET.getUInt(),
                                AMCTMapLayerAsyncOperationCallback.getInstance(mContext));
                        GetFieldUniqueValuesAsString(res);
                    }
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetFieldUniqueValuesAsWString");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
        mGetUniqueValueByWStringBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (CheckNegativeValue(mUniqueFieldIdET, "Field Id"))
                    return;
                try {
                    if (mAsyncCB.isChecked()) {
                        mVectorsMapLayer.GetFieldUniqueValuesAsWString(mUniqueFieldIdET.getUInt(),
                                AMCTMapLayerAsyncOperationCallback.getInstance(mContext));
                    } else {
                        String[] res = mVectorsMapLayer.GetFieldUniqueValuesAsWString(mUniqueFieldIdET.getUInt(),
                                AMCTMapLayerAsyncOperationCallback.getInstance(mContext));
                        GetFieldUniqueValuesAsString(res);
                    }
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetFieldUniqueValuesAsWString");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });

        mClearBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                clearListItems();
            }
        });
    }

    public void GetVectorItemFieldValuesAsInt(int value)
    {
        mVectorItemValueET.setInt(value);
    }

    public void GetVectorItemFieldValuesAsDouble(double value)
    {
        mVectorItemValueET.setDouble(value);
    }

    public void GetVectorItemFieldValuesAsString(String value)
    {
        if (value != null)
            mVectorItemValueET.setText(value);
    }

    public void GetFieldUniqueValuesAsInt(int[] uniqueValues)
    {
        if (uniqueValues != null && uniqueValues.length > 0)
        {
           // AddItemsToUniqueValues(Arrays.asList(uniqueValues));
            String str = Arrays.toString(uniqueValues).replaceAll("\\s+", "");                       // [1, 2, 3, 4, 5]
                                                                                                                                // [1,2,3,4,5]
            String strArray[] = str.substring(1, str.length() - 1).split(",");                                                             // 1,2,3,4,5

            AddItemsToUniqueValues(strArray);
        }
    }

    public void GetFieldUniqueValuesAsDouble(double[] uniqueValues)
    {
        if (uniqueValues != null && uniqueValues.length > 0)
        {
            String str = Arrays.toString(uniqueValues).replaceAll("\\s+", "");                       // [1, 2, 3, 4, 5]
            // [1,2,3,4,5]
            String strArray[] = str.substring(1, str.length() - 1).split(",");                                                             // 1,2,3,4,5

            AddItemsToUniqueValues(strArray);
        }
    }

    public void GetFieldUniqueValuesAsString(String[] uniqueValues)
    {
        if (uniqueValues != null && uniqueValues.length > 0)
        {
            AddItemsToUniqueValues(uniqueValues);
        }
    }

    private void AddItemsToUniqueValues(Object[] items)
    {
        clearListItems();
        HashMap<Object, Integer> hashMapItems = new HashMap<>();
        for (Object obj : items)
            hashMapItems.put(obj, obj.hashCode());

        mUniqueValuesAdapter.addItems(hashMapItems);
        mUniqueValuesList.deferNotifyDataSetChanged();
        Funcs.setListViewHeightBasedOnChildren(mUniqueValuesList);
    }

    private void clearListItems()
    {
        //HashMapAdapter adapter = (HashMapAdapter)mObjectsLV.getAdapter();
        mUniqueValuesAdapter.clearData();
        // refresh the View
        mUniqueValuesAdapter.notifyDataSetChanged();
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
        mVectorsMapLayer = (IMcVectorMapLayer) obj;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
         outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mVectorsMapLayer));
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
