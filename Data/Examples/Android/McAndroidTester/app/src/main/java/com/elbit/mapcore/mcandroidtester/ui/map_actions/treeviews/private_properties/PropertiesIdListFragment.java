package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.private_properties;

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
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.HeaderViewListAdapter;
import android.widget.ListView;
import android.widget.RadioGroup;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcMesh;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.ui.adapters.PrivatePropertiesListAdapter;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Consts;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Set;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcConditionalSelector;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcFont;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectScheme;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcProperty;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcTexture;
import com.elbit.mapcore.Structs.SMcAnimation;
import com.elbit.mapcore.Structs.SMcAttenuation;
import com.elbit.mapcore.Structs.SMcBColor;
import com.elbit.mapcore.Structs.SMcFColor;
import com.elbit.mapcore.Structs.SMcFVector2D;
import com.elbit.mapcore.Structs.SMcFVector3D;
import com.elbit.mapcore.Structs.SMcRotation;
import com.elbit.mapcore.Structs.SMcVariantString;
import com.elbit.mapcore.Structs.SMcVector2D;
import com.elbit.mapcore.Structs.SMcVector3D;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link PropertiesIdListFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link PropertiesIdListFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class PropertiesIdListFragment extends Fragment implements FragmentWithObject {

    private OnFragmentInteractionListener mListener;
    private IMcObjectScheme mObjectScheme;
    private View mRootView;
    private ListView mPropertiesLV;
    private RadioGroup mSetChangesRG;
    private RadioGroup mResetChangesRG;
    private Button mSetNamesBttn;
    private Button mSetChangesBttn;
    private Button mResetChangesBttn;
    private int mPropertiesType = -1;
    private IMcProperty.SPropertyNameIDType[] mPropertyIdArr;
    private IMcObject mImcObject;

    public PropertiesIdListFragment() {
        // Required empty public constructor
    }

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment PropertiesIdListFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static PropertiesIdListFragment newInstance() {
        PropertiesIdListFragment fragment = new PropertiesIdListFragment();
        return fragment;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        setTitle();
        mRootView = inflater.inflate(R.layout.fragment_properties_id_list, container, false);

        Funcs.SetObjectFromBundle(savedInstanceState, this );

        initViews();
        if (mPropertiesType == Consts.SCHEME_PROPERTIES)
            loadSchemePropertyTable();
        else if (mPropertiesType == Consts.OBJECT_PROPERTIES)
            loadObjectPropertyTable();
        return mRootView;
    }

    private void setTitle() {
        getActivity().setTitle("properties id list");
    }

    private void loadObjectPropertyTable() {
        mPropertyIdArr = new IMcProperty.SPropertyNameIDType[0];
        mSetChangesBttn.setEnabled(true);
        mResetChangesRG.setEnabled(true);
        mResetChangesBttn.setEnabled(true);

        View listHeader = getActivity().getLayoutInflater().inflate(R.layout.private_properties_table_header, null);
        try {
            mPropertyIdArr = mObjectScheme.GetProperties();
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "mObjectScheme.GetProperties()");
        } catch (Exception e) {
            e.printStackTrace();
        }

        if (mPropertyIdArr != null && mPropertyIdArr.length > 0) {
            mPropertiesLV.setAdapter(
                    new PrivatePropertiesListAdapter(this,
                            getContext(),
                            R.layout.private_properties_table_row,
                            mPropertyIdArr,
                            Consts.OBJECT_PROPERTIES,
                            mObjectScheme,
                            mImcObject));

            mPropertiesLV.addHeaderView(listHeader);
            mPropertiesLV.setRecyclerListener(new AbsListView.RecyclerListener() {
                @Override
                public void onMovedToScrapHeap(View view) {
                    if (view.hasFocus()) {
                        view.clearFocus(); //we can put it inside the second if as well, but it makes sense to do it to all scraped views
                        //Optional: also hide keyboard in that case
                        if (view instanceof EditText) {
                            InputMethodManager imm = (InputMethodManager) view.getContext().getSystemService(Context.INPUT_METHOD_SERVICE);
                            imm.hideSoftInputFromWindow(view.getWindowToken(), 0);
                        }
                    }
                }
            });
        }
    }

    private void loadSchemePropertyTable() {
        mPropertyIdArr = new IMcProperty.SPropertyNameIDType[0];
        mSetChangesBttn.setEnabled(true);
        mResetChangesRG.setEnabled(false);
        mResetChangesBttn.setEnabled(false);
        View listHeader = getActivity().getLayoutInflater().inflate(R.layout.private_properties_scheme_table_header, null);
        try {
            mPropertyIdArr = mObjectScheme.GetProperties();
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "mObjectScheme.GetProperties()");
        } catch (Exception e) {
            e.printStackTrace();
        }

        if (mPropertyIdArr != null && mPropertyIdArr.length > 0) {
            mPropertiesLV.addHeaderView(listHeader);
            mPropertiesLV.setAdapter(new PrivatePropertiesListAdapter(this, getContext(), R.layout.private_properties_table_row, mPropertyIdArr, Consts.SCHEME_PROPERTIES, mObjectScheme, null));
            mPropertiesLV.setRecyclerListener(new AbsListView.RecyclerListener() {
                @Override
                public void onMovedToScrapHeap(View view) {
                    if (view.hasFocus()) {
                        view.clearFocus(); //we can put it inside the second if as well, but it makes sense to do it to all scraped views
                        //Optional: also hide keyboard in that case
                        if (view instanceof EditText) {
                            InputMethodManager imm = (InputMethodManager) view.getContext().getSystemService(Context.INPUT_METHOD_SERVICE);
                            imm.hideSoftInputFromWindow(view.getWindowToken(), 0);
                        }
                    }
                }
            });
        }
    }

    private void initViews() {
        inflateViews();
        setChangedPropertiesBttn();
        setResetChangesBttn();
        setEachChangedNameBttn();
    }

    private void setEachChangedNameBttn() {
        mSetNamesBttn.setOnClickListener(new View.OnClickListener() {
                                             @Override
                                             public void onClick(View v) {
                                                 PrivatePropertiesListAdapter propertiesListAdapter = (PrivatePropertiesListAdapter) ((HeaderViewListAdapter) mPropertiesLV.getAdapter()).getWrappedAdapter();
                                                 Set<Integer> updatedProps = propertiesListAdapter.getUpdatedPropertiesIds();
                                                 HashMap<Integer, String> updatedPropertiesNames = propertiesListAdapter.getmUpdatedPropertiesNames();
                                                 if (updatedProps == null || updatedProps.isEmpty())
                                                     AlertMessages.ShowErrorMessage(getContext(), "No Row Changed", "Set Names");
                                                 else {
                                                     for (final Integer propertyId : updatedProps) {
                                                         final String newName = updatedPropertiesNames.get(propertyId);
                                                         if (newName != null) {
                                                             Funcs.runMapCoreFunc(new Runnable() {
                                                                 @Override
                                                                 public void run() {
                                                                     try {
                                                                         mObjectScheme.SetPropertyName(newName, propertyId);
                                                                     } catch (MapCoreException e) {
                                                                         AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetPropertyName");
                                                                     } catch (Exception e) {
                                                                         e.printStackTrace();
                                                                     }
                                                                 }});
                                                         } else
                                                             AlertMessages.ShowErrorMessage(getContext(), "No Name Changed", "Set Names");
                                                     }
                                                 }
                                             }
                                         }
        );
    }

    private void setResetChangesBttn() {
        mResetChangesBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                int checkedResetChangesRB = mResetChangesRG.getCheckedRadioButtonId();
                if (checkedResetChangesRB == R.id.properties_reset_all_properties_rb) {
                    resetAllProperties();
                }
                else if (checkedResetChangesRB == R.id.properties_reset_each_selected_property_rb) {
                    resetEachSelectedProperty();
                }
            }
        });
    }

    private void resetEachSelectedProperty() {
        View rowView;
        CheckBox resetCb;
        PrivatePropertiesListAdapter.PropertiesHolder rowHolder;
        PrivatePropertiesListAdapter propertiesListAdapter = (PrivatePropertiesListAdapter) ((HeaderViewListAdapter) mPropertiesLV.getAdapter()).getWrappedAdapter();
        final HashMap<Integer, PrivatePropertiesListAdapter.PropertiesHolder> updatePropertiesHM = propertiesListAdapter.getUpdatedRowsHM();
        boolean isAnyRowChecked = false;
        for (int i = 0; i < mPropertiesLV.getCount(); i++) {
            rowView = mPropertiesLV.getChildAt(i);
            if (rowView != null) {
                rowHolder = (PrivatePropertiesListAdapter.PropertiesHolder) rowView.getTag();
                if (rowHolder != null) {
                    resetCb = rowHolder.toReset;
                    if (resetCb.isChecked()) {
                        isAnyRowChecked = true;
                        final int propIdToReset = Integer.valueOf(String.valueOf(rowHolder.propertyId.getText()));
                        Funcs.runMapCoreFunc(new Runnable() {
                            @Override
                            public void run() {
                                try {
                                    mImcObject.ResetProperty(propIdToReset);
                                    //reset the changed property in hm
                                    getActivity().runOnUiThread(new Runnable() {
                                        @Override
                                        public void run() {
                                            updatePropertiesHM.remove(propIdToReset);
                                        }
                                    });
                                } catch (MapCoreException e) {
                                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "ResetProperty");
                                } catch (Exception e) {
                                    e.printStackTrace();
                                }
                            }
                        });


                    }
                }
            }
        }

        if (isAnyRowChecked) {
            propertiesListAdapter.setUpdatedRowsHM(updatePropertiesHM);
            mPropertiesLV.setAdapter(null);
            mPropertiesLV.setAdapter(propertiesListAdapter);
            mPropertiesLV.setRecyclerListener(new AbsListView.RecyclerListener() {
                @Override
                public void onMovedToScrapHeap(View view) {
                    if (view.hasFocus()) {
                        view.clearFocus(); //we can put it inside the second if as well, but it makes sense to do it to all scraped views
                        //Optional: also hide keyboard in that case
                        if (view instanceof EditText) {
                            InputMethodManager imm = (InputMethodManager) view.getContext().getSystemService(Context.INPUT_METHOD_SERVICE);
                            imm.hideSoftInputFromWindow(view.getWindowToken(), 0);
                        }
                    }
                }
            });
        } else
            AlertMessages.ShowErrorMessage(getContext(), "No Row Changed", "Reset Each Property");

    }

    private void resetAllProperties() {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    mImcObject.ResetAllProperties();

                    getActivity().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            PrivatePropertiesListAdapter propertiesListAdapter = (PrivatePropertiesListAdapter) ((HeaderViewListAdapter) mPropertiesLV.getAdapter()).getWrappedAdapter();
                            //reset the changed properties hm
                            propertiesListAdapter.setUpdatedRowsHM(new HashMap<Integer, PrivatePropertiesListAdapter.PropertiesHolder>());
                            mPropertiesLV.setAdapter(null);
                            mPropertiesLV.setAdapter(propertiesListAdapter);
                            //to solve the exception of "parameter must be a descendant of this view" when scrolling focused view
                            mPropertiesLV.setRecyclerListener(new AbsListView.RecyclerListener() {
                                @Override
                                public void onMovedToScrapHeap(View view) {
                                    if (view.hasFocus())
                                        view.clearFocus();
                                }
                            });
                        }
                    });
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "ResetAllProperties");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }


    private void setChangedPropertiesBttn() {
        mSetChangesBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                int checkedSetChangeRB = mSetChangesRG.getCheckedRadioButtonId();
                if (checkedSetChangeRB == R.id.properties_set_all_changed_properties_as_variant_rb) {
                    setAllChangedPropertiesAsVariant();
                }
                else if (checkedSetChangeRB == R.id.properties_set_each_changed_property_as_variant_rb) {
                    setEachChangedPropertyAsVariant();
                }
                else if (checkedSetChangeRB == R.id.properties_set_each_changed_property_as_type_rb) {
                    setEachChangedPropertyAsType();
                }
            }
        });
    }

    private ArrayList<IMcProperty.SVariantProperty> getChangedProperties() {
        ArrayList<IMcProperty.SVariantProperty> changedPropertiesList = new ArrayList<>();
        IMcProperty.SVariantProperty variantProperty;
        PrivatePropertiesListAdapter propertiesListAdapter = (PrivatePropertiesListAdapter) ((HeaderViewListAdapter) mPropertiesLV.getAdapter()).getWrappedAdapter();
        Set<Integer> updatedProps = propertiesListAdapter.getUpdatedPropertiesIds();
        if (updatedProps == null || updatedProps.isEmpty())
            return null;

        for (int i = 0; i < mPropertyIdArr.length; i++) {
            if (updatedProps.contains(mPropertyIdArr[i].uID)) {
                variantProperty = new IMcProperty.SVariantProperty();
                variantProperty.eType = mPropertyIdArr[i].eType;
                variantProperty.uID = mPropertyIdArr[i].uID;
                variantProperty.Value = propertiesListAdapter.getPropertiesValues().get(mPropertyIdArr[i].uID);
                changedPropertiesList.add(variantProperty);
            }
        }
        return changedPropertiesList;
    }

    private void setAllChangedPropertiesAsVariant() {
        final ArrayList<IMcProperty.SVariantProperty> changedPropertiesList = getChangedProperties();
        if (changedPropertiesList == null || changedPropertiesList.isEmpty())
            AlertMessages.ShowErrorMessage(getContext(), "No Row Changed", "Set All Changed Properties As Variant");
        else {
            Funcs.runMapCoreFunc(new Runnable() {
                @Override
                public void run() {
                    try {
                        IMcProperty.SVariantProperty[] componentParams = new IMcProperty.SVariantProperty[changedPropertiesList.size()];
                        if (mPropertiesType == Consts.OBJECT_PROPERTIES) {
                            mImcObject.SetProperties(changedPropertiesList.toArray(componentParams));
                        } else if (mPropertiesType == Consts.SCHEME_PROPERTIES) {
                            mObjectScheme.SetPropertyDefaults(changedPropertiesList.toArray(componentParams));
                        }
                    } catch (MapCoreException e) {
                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetProperties/SetPropertyDefaults");
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            });
        }
    }

    private void setEachChangedPropertyAsVariant() {
        IMcProperty.SVariantProperty variantProperty;
        PrivatePropertiesListAdapter propertiesListAdapter = (PrivatePropertiesListAdapter) ((HeaderViewListAdapter) mPropertiesLV.getAdapter()).getWrappedAdapter();
        Set<Integer> updatedProps = propertiesListAdapter.getUpdatedPropertiesIds();
        if (updatedProps == null || updatedProps.size() == 0)
            AlertMessages.ShowGenericMessage(getContext(), "Set Each Changed Property As Variant", "No Row Changed");
        else {
            for (int i = 0; i < mPropertyIdArr.length; i++) {
                if (updatedProps.contains(mPropertyIdArr[i].uID)) {
                    variantProperty = new IMcProperty.SVariantProperty();
                    variantProperty.eType = mPropertyIdArr[i].eType;
                    variantProperty.uID = mPropertyIdArr[i].uID;
                    variantProperty.Value = propertiesListAdapter.getPropertiesValues().get(mPropertyIdArr[i].uID);
                    final IMcProperty.SVariantProperty finalVariantProperty = variantProperty;
                    Funcs.runMapCoreFunc(new Runnable() {
                        @Override
                        public void run() {
                            try {
                                if (mPropertiesType == Consts.OBJECT_PROPERTIES)
                                    mImcObject.SetProperty(finalVariantProperty);
                                else if (mPropertiesType == Consts.SCHEME_PROPERTIES) {
                                    mObjectScheme.SetPropertyDefault(finalVariantProperty);
                                }
                            } catch (MapCoreException e) {
                                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetProperty/SetPropertyDefault");
                            } catch (Exception e) {
                                e.printStackTrace();
                            }
                        }});
                }
            }
        }
    }

    private void setEachChangedPropertyAsType() {
        ArrayList<IMcProperty.SVariantProperty> changedPropertiesList = getChangedProperties();
        if (changedPropertiesList == null || changedPropertiesList.isEmpty())
            AlertMessages.ShowErrorMessage(getContext(), "No Row Changed", "Set Each Changed Property As Type");
        else {
            for (IMcProperty.SVariantProperty changedProperty : changedPropertiesList) {
                final IMcProperty.EPropertyType propType = changedProperty.eType;
                final int propId = changedProperty.uID;
                final Object propVal = changedProperty.Value;
                if (mPropertiesType == Consts.OBJECT_PROPERTIES) {
                    Funcs.runMapCoreFunc(new Runnable() {
                        @Override
                        public void run() {

                            switch (propType) {
                                case EPT_ANIMATION:
                                    try {
                                        mImcObject.SetAnimationProperty(propId, (SMcAnimation) propVal);
                                    } catch (MapCoreException e) {
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetAnimationProperty");
                                    } catch (Exception e) {
                                        e.printStackTrace();
                                    }
                                    break;
                                case EPT_ATTENUATION:
                                    try {
                                        mImcObject.SetAttenuationProperty(propId, (SMcAttenuation) propVal);
                                    } catch (MapCoreException e) {
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetAttenuationProperty");
                                    } catch (Exception e) {
                                        e.printStackTrace();
                                    }
                                    break;
                                case EPT_BCOLOR:
                                    try {
                                        mImcObject.SetBColorProperty(propId, (SMcBColor) propVal);
                                    } catch (MapCoreException e) {
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetBColorProperty");
                                    } catch (Exception e) {
                                        e.printStackTrace();
                                    }
                                    break;
                                case EPT_BCOLOR_ARRAY:
                                    try {
                                        mImcObject.SetArrayProperty(propId, IMcProperty.EPropertyType.EPT_BCOLOR_ARRAY, (IMcProperty.SArrayProperty<SMcBColor>) propVal);
                                    } catch (MapCoreException e) {
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetColorProperty");
                                    } catch (Exception e) {
                                        e.printStackTrace();
                                    }
                                    break;
                                case EPT_BOOL:
                                    try {
                                        mImcObject.SetBoolProperty(propId, (boolean) propVal);
                                    } catch (MapCoreException e) {
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetBoolProperty");
                                    } catch (Exception e) {
                                        e.printStackTrace();
                                    }
                                    break;
                                case EPT_BYTE:
                                    try {
                                        mImcObject.SetByteProperty(propId, (short) propVal);
                                    } catch (MapCoreException e) {
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetByteProperty");
                                    } catch (Exception e) {
                                        e.printStackTrace();
                                    }
                                    break;
                                case EPT_ENUM:
                                    try {
                                        mImcObject.SetEnumProperty(propId, (int) propVal);
                                    } catch (MapCoreException e) {
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetEnumProperty");
                                    } catch (Exception e) {
                                        e.printStackTrace();
                                    }
                                    break;
                                case EPT_CONDITIONALSELECTOR:
                                    try {
                                        mImcObject.SetConditionalSelectorProperty(propId, (IMcConditionalSelector) propVal);
                                    } catch (MapCoreException e) {
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetConditionalSelectorProperty");
                                    } catch (Exception e) {
                                        e.printStackTrace();
                                    }
                                    break;
                                case EPT_DOUBLE:
                                    try {
                                        mImcObject.SetDoubleProperty(propId, (double) propVal);
                                    } catch (MapCoreException e) {
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetDoubleProperty");
                                    } catch (Exception e) {
                                        e.printStackTrace();
                                    }
                                    break;
                                case EPT_FCOLOR:
                                    try {
                                        mImcObject.SetFColorProperty(propId, (SMcFColor) propVal);
                                    } catch (MapCoreException e) {
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetFColorProperty");
                                    } catch (Exception e) {
                                        e.printStackTrace();
                                    }
                                    break;
                                case EPT_FLOAT:
                                    try {
                                        mImcObject.SetFloatProperty(propId, (float) propVal);
                                    } catch (MapCoreException e) {
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetFloatProperty");
                                    } catch (Exception e) {
                                        e.printStackTrace();
                                    }
                                    break;
                                case EPT_FONT:
                                    try {
                                        mImcObject.SetFontProperty(propId, (IMcFont) propVal);
                                    } catch (MapCoreException e) {
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetFontProperty");
                                    } catch (Exception e) {
                                        e.printStackTrace();
                                    }
                                    break;
                                case EPT_MESH:
                                    try {
                                        mImcObject.SetMeshProperty(propId, (IMcMesh) propVal);
                                    } catch (MapCoreException e) {
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetFontProperty");
                                    } catch (Exception e) {
                                        e.printStackTrace();
                                    }
                                    break;
                                case EPT_FVECTOR2D:
                                    try {
                                        mImcObject.SetFVector2DProperty(propId, (SMcFVector2D) propVal);
                                    } catch (MapCoreException e) {
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetFVector2DProperty");
                                    } catch (Exception e) {
                                        e.printStackTrace();
                                    }
                                    break;
                                case EPT_FVECTOR2D_ARRAY:
                                    try {
                                        mImcObject.SetArrayProperty(propId, IMcProperty.EPropertyType.EPT_FVECTOR2D_ARRAY, (IMcProperty.SArrayProperty<SMcFVector2D>) propVal);
                                    } catch (MapCoreException e) {
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetFVector2DProperty");
                                    } catch (Exception e) {
                                        e.printStackTrace();
                                    }
                                    break;
                                case EPT_FVECTOR3D:
                                    try {
                                        mImcObject.SetFVector3DProperty(propId, (SMcFVector3D) propVal);
                                    } catch (MapCoreException e) {
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetFVector3DProperty");
                                    } catch (Exception e) {
                                        e.printStackTrace();
                                    }
                                    break;
                                case EPT_FVECTOR3D_ARRAY:
                                    try {
                                        mImcObject.SetArrayProperty(propId, IMcProperty.EPropertyType.EPT_FVECTOR3D_ARRAY, (IMcProperty.SArrayProperty<SMcFVector3D>) propVal);
                                    } catch (MapCoreException e) {
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetFVector3DProperty");
                                    } catch (Exception e) {
                                        e.printStackTrace();
                                    }
                                    break;
                                case EPT_UINT:
                                    try {
                                        mImcObject.SetUIntProperty(propId, (int) propVal);
                                    } catch (MapCoreException e) {
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetUIntProperty");
                                    } catch (Exception e) {
                                        e.printStackTrace();
                                    }
                                    break;
                                case EPT_INT:
                                    try {
                                        mImcObject.SetIntProperty(propId, (int) propVal);
                                    } catch (MapCoreException e) {
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetIntProperty");
                                    } catch (Exception e) {
                                        e.printStackTrace();
                                    }
                                    break;
                                case EPT_SBYTE:
                                    try {
                                        mImcObject.SetSByteProperty(propId, (byte) propVal);
                                    } catch (MapCoreException e) {
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetSByteProperty");
                                    } catch (Exception e) {
                                        e.printStackTrace();
                                    }
                                    break;
                                case EPT_ROTATION:
                                    try {
                                        mImcObject.SetRotationProperty(propId, (SMcRotation) propVal);
                                    } catch (MapCoreException e) {
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetRotationProperty");
                                    } catch (Exception e) {
                                        e.printStackTrace();
                                    }
                                    break;
                                case EPT_STRING:
                                    try {
                                        mImcObject.SetStringProperty(propId, (SMcVariantString) propVal);
                                    } catch (MapCoreException e) {
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetStringProperty");
                                    } catch (Exception e) {
                                        e.printStackTrace();
                                    }
                                    break;
                                case EPT_SUBITEM_ARRAY:
                                    try {
                                        mImcObject.SetArrayProperty(propId, IMcProperty.EPropertyType.EPT_SUBITEM_ARRAY, (IMcProperty.SArrayProperty<?>) propVal);
                                    } catch (MapCoreException e) {
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetSubItemsDataProperty");
                                    } catch (Exception e) {
                                        e.printStackTrace();
                                    }
                                    break;
                                case EPT_TEXTURE:
                                    try {
                                        mImcObject.SetTextureProperty(propId, (IMcTexture) propVal);
                                    } catch (MapCoreException e) {
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetTextureProperty");
                                    } catch (Exception e) {
                                        e.printStackTrace();
                                    }
                                    break;
                                case EPT_VECTOR2D:
                                    try {
                                        mImcObject.SetVector2DProperty(propId, (SMcVector2D) propVal);
                                    } catch (MapCoreException e) {
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetVector2DProperty");
                                    } catch (Exception e) {
                                        e.printStackTrace();
                                    }
                                    break;
                                case EPT_VECTOR2D_ARRAY:
                                    try {
                                        mImcObject.SetArrayProperty(propId, IMcProperty.EPropertyType.EPT_VECTOR2D_ARRAY, (IMcProperty.SArrayProperty<SMcVector2D>) propVal);
                                    } catch (MapCoreException e) {
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetVector2DProperty");
                                    } catch (Exception e) {
                                        e.printStackTrace();
                                    }
                                    break;
                                case EPT_VECTOR3D:
                                    try {
                                        mImcObject.SetVector3DProperty(propId, (SMcVector3D) propVal);
                                    } catch (MapCoreException e) {
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetVector3DProperty");
                                    } catch (Exception e) {
                                        e.printStackTrace();
                                    }
                                    break;
                                case EPT_VECTOR3D_ARRAY:
                                    try {
                                        mImcObject.SetArrayProperty(propId, IMcProperty.EPropertyType.EPT_VECTOR3D_ARRAY, (IMcProperty.SArrayProperty<SMcVector3D>) propVal);
                                    } catch (MapCoreException e) {
                                        AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetVector3DProperty");
                                    } catch (Exception e) {
                                        e.printStackTrace();
                                    }
                                    break;
                            }
                        }});
                } else
                    setPropertiesValuesAtSchema(propType, propId, propVal);
            }
        }
    }

    private void setPropertiesValuesAtSchema(final IMcProperty.EPropertyType propType, final int propId, final Object propVal) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                switch (propType) {
                    case EPT_ANIMATION:
                        try {
                            mObjectScheme.SetAnimationPropertyDefault(propId, (SMcAnimation) propVal);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetAnimationPropertyDefault");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EPT_ATTENUATION:
                        try {
                            mObjectScheme.SetAttenuationPropertyDefault(propId, (SMcAttenuation) propVal);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetAttenuationPropertyDefault");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EPT_BCOLOR:
                        try {
                            mObjectScheme.SetBColorPropertyDefault(propId, (SMcBColor) propVal);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetBColorPropertyDefault");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EPT_BCOLOR_ARRAY:
                        try {
                            mObjectScheme.SetArrayPropertyDefault(propId, IMcProperty.EPropertyType.EPT_BCOLOR_ARRAY, (IMcProperty.SArrayProperty<SMcBColor>) propVal);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetColorPropertyDefault");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EPT_BOOL:
                        try {
                            mObjectScheme.SetBoolPropertyDefault(propId, (Boolean) propVal);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetBoolPropertyDefault");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EPT_ENUM:
                        try {
                            mObjectScheme.SetEnumPropertyDefault(propId, (Integer) propVal);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetEnumPropertyDefault");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EPT_BYTE:
                        try {
                            mObjectScheme.SetBytePropertyDefault(propId, (Byte) propVal);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetBytePropertyDefault");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EPT_CONDITIONALSELECTOR:
                        try {
                            mObjectScheme.SetConditionalSelectorPropertyDefault(propId, (IMcConditionalSelector) propVal);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetConditionalSelectorPropertyDefault");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EPT_DOUBLE:
                        try {
                            mObjectScheme.SetDoublePropertyDefault(propId, (Double) propVal);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetDoublePropertyDefault");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EPT_FCOLOR:
                        try {
                            mObjectScheme.SetFColorPropertyDefault(propId, (SMcFColor) propVal);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetFColorPropertyDefault");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EPT_FLOAT:
                        try {
                            mObjectScheme.SetFloatPropertyDefault(propId, (Float) propVal);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetFloatPropertyDefault");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EPT_FONT:
                        try {
                            mObjectScheme.SetFontPropertyDefault(propId, (IMcFont) propVal);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetFontPropertyDefault");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EPT_MESH:
                        try {
                            mObjectScheme.SetMeshPropertyDefault(propId, (IMcMesh) propVal);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetMeshPropertyDefault");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EPT_FVECTOR2D:
                        try {
                            mObjectScheme.SetFVector2DPropertyDefault(propId, (SMcFVector2D) propVal);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetFVector2DPropertyDefault");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EPT_FVECTOR2D_ARRAY:
                        try {
                            mObjectScheme.SetArrayPropertyDefault(propId, IMcProperty.EPropertyType.EPT_FVECTOR2D_ARRAY, (IMcProperty.SArrayProperty<SMcFVector2D>) propVal);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetFVector2DPropertyDefault");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EPT_FVECTOR3D:
                        try {
                            mObjectScheme.SetFVector3DPropertyDefault(propId, (SMcFVector3D) propVal);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetFVector3DPropertyDefault");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EPT_FVECTOR3D_ARRAY:
                        try {
                            mObjectScheme.SetArrayPropertyDefault(propId, IMcProperty.EPropertyType.EPT_FVECTOR3D_ARRAY, (IMcProperty.SArrayProperty<SMcFVector3D>) propVal);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetFVector3DPropertyDefault");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EPT_UINT:
                        try {
                            mObjectScheme.SetUIntPropertyDefault(propId, (Integer) propVal);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetUIntPropertyDefault");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    case EPT_INT:
                        try {
                            mObjectScheme.SetIntPropertyDefault(propId, (Integer) propVal);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetIntPropertyDefault");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EPT_SBYTE:
                        try {
                            mObjectScheme.SetBytePropertyDefault(propId, (Byte) propVal);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetBytePropertyDefault");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EPT_ROTATION:
                        try {
                            mObjectScheme.SetRotationPropertyDefault(propId, (SMcRotation) propVal);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetRotationPropertyDefault");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EPT_STRING:
                        try {
                            mObjectScheme.SetStringPropertyDefault(propId, (SMcVariantString) propVal);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetStringPropertyDefault");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EPT_SUBITEM_ARRAY:
                        try {
                            mObjectScheme.SetArrayPropertyDefault(propId, IMcProperty.EPropertyType.EPT_SUBITEM_ARRAY, (IMcProperty.SArrayProperty<?>) propVal);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetSubItemsDataPropertyDefault");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EPT_TEXTURE:
                        try {
                            mObjectScheme.SetTexturePropertyDefault(propId, (IMcTexture) propVal);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetTexturePropertyDefault");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EPT_VECTOR2D:
                        try {
                            mObjectScheme.SetVector2DPropertyDefault(propId, (SMcVector2D) propVal);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetVector2DPropertyDefault");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EPT_VECTOR2D_ARRAY:
                        try {
                            mObjectScheme.SetArrayPropertyDefault(propId, IMcProperty.EPropertyType.EPT_VECTOR2D_ARRAY, (IMcProperty.SArrayProperty<SMcVector2D>) propVal);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetVector2DPropertyDefault");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EPT_VECTOR3D:
                        try {
                            mObjectScheme.SetVector3DPropertyDefault(propId, (SMcVector3D) propVal);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetVector3DPropertyDefault");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EPT_VECTOR3D_ARRAY:
                        try {
                            mObjectScheme.SetArrayPropertyDefault(propId, IMcProperty.EPropertyType.EPT_VECTOR3D_ARRAY, (IMcProperty.SArrayProperty<SMcVector3D>) propVal);
                        } catch (MapCoreException e) {
                            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetVector3DPropertyDefault");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                }
            }});
    }

    private void inflateViews() {
        mPropertiesLV = (ListView) mRootView.findViewById(R.id.properties_id_list_lv);
        mSetChangesRG = (RadioGroup) mRootView.findViewById(R.id.properties_set_rg);
        mResetChangesRG = (RadioGroup) mRootView.findViewById(R.id.properties_reset_rg);
        mSetNamesBttn = (Button) mRootView.findViewById(R.id.properties_set_names_bttn);
        mSetChangesBttn = (Button) mRootView.findViewById(R.id.properties_set_bttn);
        mResetChangesBttn = (Button) mRootView.findViewById(R.id.properties_reset_bttn);
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
     /*   if (context instanceof OnFragmentInteractionListener) {
            mListener = (OnFragmentInteractionListener) context;
        } else {
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
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);

        AMCTSerializableObject object = new AMCTSerializableObject();
        if(mPropertiesType == Consts.SCHEME_PROPERTIES)
            object.setMcObject(mObjectScheme);
        else
            object.setMcObject(mImcObject);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, object);
    }

    @Override
    public void setObject(Object obj) {
        if (obj instanceof IMcObjectScheme) {
            mObjectScheme = (IMcObjectScheme) obj;
            mPropertiesType = Consts.SCHEME_PROPERTIES;
        } else if (obj instanceof IMcObject)
            try {
                mImcObject = (IMcObject) obj;
               /* //toDo remove after scheme.GetProperties will be added to api
                mProperties=new IMcProperty.SVariantProperty[0];
                mProperties=((IMcObject) obj).GetProperties();*/
                mObjectScheme = ((IMcObject) obj).GetScheme();
                mPropertiesType = Consts.OBJECT_PROPERTIES;
            } catch (MapCoreException e) {
                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "(IMcObject) obj).GetScheme()");
            } catch (Exception e) {
                e.printStackTrace();
            }

    }

    public void updateTextureProperty(int mPropertyId, IMcTexture mCreatedTexture) {
        ((PrivatePropertiesListAdapter) ((HeaderViewListAdapter) mPropertiesLV.getAdapter()).getWrappedAdapter()).updateTextureProperty(mPropertyId, mCreatedTexture);
    }

    public void updateFontProperty(int mPropertyId, IMcFont mCreatedFont) {
        ((PrivatePropertiesListAdapter) ((HeaderViewListAdapter) mPropertiesLV.getAdapter()).getWrappedAdapter()).updateFontProperty(mPropertyId, mCreatedFont);
    }

    public void updateMeshProperty(int mPropertyId, IMcMesh mCreatedMesh) {
        ((PrivatePropertiesListAdapter) ((HeaderViewListAdapter) mPropertiesLV.getAdapter()).getWrappedAdapter()).updateMeshProperty(mPropertyId, mCreatedMesh);
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
    public void onHiddenChanged(boolean hidden) {
        super.onHiddenChanged(hidden);
        if (!hidden)
            setTitle();
    }
}
