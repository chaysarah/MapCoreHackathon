package com.elbit.mapcore.mcandroidtester.ui.adapters;

import android.app.Activity;
import android.content.Context;
import android.content.DialogInterface;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;
import androidx.appcompat.app.AlertDialog;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.TextView;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcFont;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcMesh;
import com.elbit.mapcore.Structs.SMcVariantString;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.private_properties.FontPropertyDialogFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.private_properties.MeshPropertyDialogFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.private_properties.PropertiesIdListFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.private_properties.TexturePropertyDialogFragment;
import com.elbit.mapcore.mcandroidtester.ui.objects.ObjectPropertiesBase;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SelectColor;
import com.elbit.mapcore.mcandroidtester.utils.customviews.TwoDFVector;

import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Set;

import com.elbit.mapcore.Classes.OverlayManager.McConditionalSelector;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectScheme;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcProperty;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcProperty.EPropertyType;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcProperty.SArrayProperty;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcTexture;
import com.elbit.mapcore.Structs.SMcAnimation;
import com.elbit.mapcore.Structs.SMcAttenuation;
import com.elbit.mapcore.Structs.SMcBColor;
import com.elbit.mapcore.Structs.SMcFColor;
import com.elbit.mapcore.Structs.SMcFVector2D;
import com.elbit.mapcore.Structs.SMcFVector3D;
import com.elbit.mapcore.Structs.SMcRotation;
import com.elbit.mapcore.Structs.SMcSubItemData;
import com.elbit.mapcore.Structs.SMcVector2D;
import com.elbit.mapcore.Structs.SMcVector3D;
import com.elbit.mapcore.mcandroidtester.utils.customviews.TwoDVector;

/**
 * Created by tc99382 on 20/03/2017.
 */
public class PrivatePropertiesListAdapter extends ArrayAdapter<IMcProperty.SPropertyNameIDType>  {
    private final PropertiesIdListFragment mPropertiesIdListFragment;
    private Method[] mImcObjectMethodsArr;
    private Method[] mObjSchemeMethodsArr;
    private Context mContext;
    private int mResourceId;
    private IMcProperty.SPropertyNameIDType[] mPropertyIdArr;
    private int mPropertiesType;
    private IMcObjectScheme mObjectScheme;
    private IMcObject mImcObject;
    //saveTheUpdatedRowsHolder
    private HashMap<Integer, PropertiesHolder> mUpdatedRowsHM;
    //save the properties values
    private HashMap<Integer, Object> mPropertiesValues;
    private PropertiesHolder mCurTextureHolder;
    private PropertiesHolder mCurFontHolder;
    private PropertiesHolder mCurMeshHolder;

    public HashMap<Integer, String> getmUpdatedPropertiesNames() {
        return mUpdatedPropertiesNames;
    }

    private HashMap<Integer, String> mUpdatedPropertiesNames;
    private Object propertyVal;

    public Set<Integer> getUpdatedPropertiesIds() {
        return mUpdatedRowsHM.keySet();
    }

    public HashMap<Integer, Object> getPropertiesValues() {
        return mPropertiesValues;
    }

    public HashMap<Integer, PropertiesHolder> getUpdatedRowsHM() {
        return mUpdatedRowsHM;
    }

    public void setUpdatedRowsHM(HashMap<Integer, PropertiesHolder> mUpdatedRowsHM) {
        this.mUpdatedRowsHM = mUpdatedRowsHM;
    }


    /**
     * Constructor
     *
     * @param propertiesIdListFragment
     * @param context                  The current context.
     * @param resource                 The resource ID for a layout file containing a TextView to use when
     *                                 instantiating views.
     * @param objects                  The objects to represent in the ListView.
     * @param propertyType
     * @param objectScheme
     */
    public PrivatePropertiesListAdapter(
            PropertiesIdListFragment propertiesIdListFragment,
            Context context,
            int resource,
            IMcProperty.SPropertyNameIDType[] objects,
            int propertyType,
            IMcObjectScheme objectScheme,
            IMcObject imcObject)
    {
        super(context, resource, objects);
        mContext = context;
        mResourceId = resource;
        mPropertyIdArr = objects;
        mPropertiesType = propertyType;
        mObjectScheme = objectScheme;
        mImcObject = imcObject;
        mUpdatedRowsHM = new HashMap<>();
        mPropertiesValues = new HashMap<>();
        mUpdatedPropertiesNames = new HashMap<>();
        mPropertiesIdListFragment = propertiesIdListFragment;
        if (mImcObject != null)
            mImcObjectMethodsArr = mImcObject.getClass().getMethods();
        if (mObjectScheme != null)
            mObjSchemeMethodsArr = mObjectScheme.getClass().getMethods();
    }


    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        View row = convertView;
        PropertiesHolder holder = null;
        if (row == null) {
            row = ((Activity) mContext).getLayoutInflater().inflate(mResourceId, parent, false);
            holder = new PropertiesHolder();
            holder.propertyId = (TextView) row.findViewById(R.id.private_properties_id);
            holder.type = (TextView) row.findViewById(R.id.private_properties_type);
            holder.name = (TextView) row.findViewById(R.id.private_properties_name);
            holder.value = (Button) row.findViewById(R.id.private_properties_change_value_bttn);
            holder.useDefault = (CheckBox) row.findViewById(R.id.private_properties_use_default);
            holder.useDefault.setVisibility(mPropertiesType == Consts.SCHEME_PROPERTIES ? View.GONE : View.VISIBLE);
            holder.propertyValue = (TextView) row.findViewById(R.id.private_properties_property_value);
            holder.isChanged = (CheckBox) row.findViewById(R.id.private_properties_is_changed);
            holder.toReset = (CheckBox) row.findViewById(R.id.private_properties_to_reset);
            holder.toReset.setVisibility(mPropertiesType == Consts.SCHEME_PROPERTIES ? View.GONE : View.VISIBLE);
        } else {
            holder = (PropertiesHolder) row.getTag();
            //take holder from updated rows table if exist
            PropertiesHolder updatedPropertiesRow = mUpdatedRowsHM.get(Integer.valueOf(String.valueOf(holder.propertyId.getText())));
            if (updatedPropertiesRow != null)
                holder = updatedPropertiesRow;
            else
                holder = (PropertiesHolder) row.getTag();
        }
        final IMcProperty.SPropertyNameIDType curProperty = mPropertyIdArr[position];
        holder = setCurRowValues(curProperty, holder);
        row.setTag(holder);
        return row;
    }

    @Override

    public int getViewTypeCount() {

        return getCount();
    }

    @Override
    public int getItemViewType(int position) {

        return position;
    }

    private PropertiesHolder setCurRowValues(final IMcProperty.SPropertyNameIDType curProperty, final PropertiesHolder holder) {
        holder.propertyId.setText(String.valueOf(curProperty.uID));
        holder.name.setText(getPropertyName(curProperty.uID));
        holder.type.setText(String.valueOf(curProperty.eType));
        if (String.valueOf(holder.propertyValue.getText()).isEmpty())
            holder.propertyValue.setText(getCurPropertyValueForTV(curProperty, holder));
        if (curProperty.eType != EPropertyType.EPT_BCOLOR)
            holder.propertyValue.setBackgroundColor(Color.TRANSPARENT);
        final PropertiesHolder finalHolder = holder;
        holder.value.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                openSetValueDialog(curProperty.eType, curProperty.uID, finalHolder.propertyValue, holder);
            }
        });
        holder.name.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {

            }

            @Override
            public void afterTextChanged(Editable s) {
                //detect if the text is changed by user or programmatically
                if (holder.name.hasFocus()) {
                    holder.isChanged.setChecked(true);
                    mUpdatedRowsHM.put(curProperty.uID, holder);
                    // mPropertiesValues.put(curProperty.uID, Consts.PROPERTY_NAME_PREFIX + s);
                    mUpdatedPropertiesNames.put(curProperty.uID, String.valueOf(s));
                }
            }
        });
        return holder;

    }

    private String getCurPropertyValueForTV(IMcProperty.SPropertyNameIDType curProperty, PropertiesHolder holder) {
        SArrayProperty<?> arrPropertyVal = null;
        // Object propertyVal = null;
        if (mPropertiesType == Consts.SCHEME_PROPERTIES) {
            arrPropertyVal = getDefaultArrPropertiesMethod(curProperty.eType, curProperty.uID);
            if (arrPropertyVal.aElements == null)
                propertyVal = getDefaultPropertiesMethod(curProperty.eType, curProperty.uID, curProperty.strName);
            else
                mPropertiesValues.put(curProperty.uID, arrPropertyVal);
        } else if (mPropertiesType == Consts.OBJECT_PROPERTIES) {
            try {
                holder.useDefault.setChecked(mImcObject.IsPropertyDefault(curProperty.uID));
            } catch (MapCoreException e) {
                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "IsPropertyDefault");
            } catch (Exception e) {
                e.printStackTrace();
            }
            arrPropertyVal = getArrPropertiesMethods(curProperty.eType, curProperty.uID, curProperty.strName);

            if (arrPropertyVal.aElements == null)
                propertyVal = getPropertiesMethods(curProperty.eType, curProperty.uID, curProperty.strName);
            else
                mPropertiesValues.put(curProperty.uID, arrPropertyVal);
        }
        String propAsString = null;
        if (arrPropertyVal.aElements != null)
            propAsString = getArrPropertiesAsString(curProperty.eType, arrPropertyVal);
        else if (propertyVal != null) {
            {
                mPropertiesValues.put(curProperty.uID, propertyVal);
                if (holder.propertyValue.getTag() == null)
                    propAsString = getPropertyAsString(curProperty.eType, propertyVal, holder.propertyValue);
                else
                    propAsString = getPropertyAsString(curProperty.eType, holder.propertyValue.getTag(), holder.propertyValue);
            }
        }
        return propAsString;
    }

    private void openSetValueDialog(EPropertyType eType, int uID, TextView propertyValue, PropertiesHolder holder) {
        switch (eType) {
            case EPT_INT:
            case EPT_DOUBLE:
            case EPT_SBYTE:
            case EPT_BYTE:
            case EPT_FLOAT:
            case EPT_ENUM:
            case EPT_UINT:
                openSetNumericValueDialog(uID, eType, propertyValue, holder);
                break;
            case EPT_BCOLOR:
                openSetColorValueDialog(uID, propertyValue, holder);
                break;
            case EPT_FONT:
                openSetFontValueDialog(uID, propertyValue, holder);
                break;
            case EPT_STRING:
                openSetStringValueDialog(uID, holder);
                break;
            case EPT_MESH:
                openSetMeshValueDialog(uID, propertyValue, holder);
                break;
            case EPT_TEXTURE:
                openSetTextureValueDialog(uID, propertyValue, holder);
                break;
            case EPT_FVECTOR2D:
                openFVector2DValueDialog(uID, holder, eType);
                break;
            case EPT_VECTOR2D:
                openFVector2DValueDialog(uID, holder, eType);
                break;
            case EPT_BOOL:
                openBooleanValueDialog(uID, propertyValue, holder);
                break;
            case EPT_SUBITEM_ARRAY:
                openSetSubItemArrValueDialog(uID, eType, propertyValue, holder);
                break;

        }
    }

    private void openSetSubItemArrValueDialog(final int propertyId, EPropertyType eType, TextView propertyValue, final PropertiesHolder holder) {
        final View dialogView = ((Activity) mContext).getLayoutInflater().inflate(R.layout.sub_item_arr_property_type, null);
        TextView propertyIdTV = (TextView) dialogView.findViewById(R.id.sub_item_arr_property_id);
        final EditText subItemsDataIds = (EditText) dialogView.findViewById(R.id.sub_item_arr_property_sub_items_data_ids);
        final EditText subItemsDataIndexes = (EditText) dialogView.findViewById(R.id.sub_item_arr_property_sub_items_data_start_indexes);
        propertyIdTV.setText(String.valueOf(propertyId));
        initSubItemsData(propertyId, subItemsDataIds, subItemsDataIndexes);

        AlertDialog.Builder builder = new AlertDialog.Builder(getContext());
        builder.setTitle("Sub Item Array property type").setView(dialogView)
                .setCancelable(false)
                .setPositiveButton("Ok", new DialogInterface.OnClickListener() {

                    @Override
                    public void onClick(DialogInterface dialog, int id) {
                        SArrayProperty<SMcSubItemData> subItemDataSArrayProperty = getNewSubItemsData(subItemsDataIds, subItemsDataIndexes);
                        if (subItemDataSArrayProperty != null) {
                            String newValueAsString = getSubItemsDataAsString(subItemDataSArrayProperty);
                            setNewPropertyValue(propertyId, EPropertyType.EPT_SUBITEM_ARRAY, newValueAsString, holder, subItemDataSArrayProperty);
                        }
                    }
                });
        builder.show();
    }

    private SArrayProperty<SMcSubItemData> getNewSubItemsData(EditText subItemsDataIds, EditText subItemsDataIndexes) {
        SArrayProperty<SMcSubItemData> subItemDataSArr = new SArrayProperty<>();
        ArrayList<SMcSubItemData> subItemDataArr = new ArrayList<>();
        String[] subItemsDataIdsStringArr = Funcs.splitToStringArr(String.valueOf(subItemsDataIds.getText()), " ");
        String[] subItemsDataIndexesStringArr = Funcs.splitToStringArr(String.valueOf(subItemsDataIndexes.getText()), " ");
        if (subItemsDataIdsStringArr.length != subItemsDataIndexesStringArr.length) {
            AlertMessages.ShowGenericMessage(getContext(), "sub items data error", "Id's number different from points start index number\nChange input data and save again");
            return null;
        } else {
            if (subItemsDataIdsStringArr.length > 0 && subItemsDataIndexesStringArr.length > 0) {
                for (int i = 0; i < subItemsDataIdsStringArr.length; i++) {
                    try {
                        Integer subItemId = Integer.parseInt(subItemsDataIdsStringArr[i]);
                        Integer pointStartIndex = Integer.parseInt(subItemsDataIndexesStringArr[i]);
                        if (subItemDataArr != null) {
                            subItemDataArr.add(new SMcSubItemData(subItemId, pointStartIndex));
                        }
                    } catch (NumberFormatException numberFormatEx) {
                        AlertMessages.ShowErrorMessage(getContext(), String.valueOf(numberFormatEx.getMessage()), "Sub Items Data-invalid input");
                        subItemDataArr = null;
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            }
            if (subItemDataArr != null) {
                SMcSubItemData[] sMcSubItemData = new SMcSubItemData[subItemDataArr.size()];
                subItemDataSArr.aElements = subItemDataArr.toArray(sMcSubItemData);
            } else
                return null;
        }
        return subItemDataSArr;
    }


    private void initSubItemsData(int propertyId, EditText subItemsDataIds, EditText subItemsDataIndexes) {
        SArrayProperty<SMcSubItemData> subItemsData = (SArrayProperty<SMcSubItemData>) mPropertiesValues.get(propertyId);
        if (subItemsData != null && subItemsData.aElements != null) {
            for (int i = 0; i < subItemsData.aElements.length; i++) {
                subItemsDataIds.getText().append(String.valueOf(subItemsData.aElements[i].uSubItemID) + " ");
                subItemsDataIndexes.getText().append(String.valueOf(subItemsData.aElements[i].nPointsStartIndex) + " ");
            }
        }
    }

    private void openBooleanValueDialog(final int propertyId, TextView propertyValue, final PropertiesHolder holder) {
        final View dialogView = ((Activity) mContext).getLayoutInflater().inflate(R.layout.bool_property_type, null);
        TextView propertyIdTV = (TextView) dialogView.findViewById(R.id.boolean_property_id);
        propertyIdTV.setText(String.valueOf(propertyId));
        final CheckBox isCheckedVal = (CheckBox) dialogView.findViewById(R.id.boolean_property_value);
        isCheckedVal.setChecked((Boolean) mPropertiesValues.get(propertyId));

        AlertDialog.Builder builder = new AlertDialog.Builder(getContext());
        builder.setTitle("Boolean property type").setView(dialogView)
                .setCancelable(false)
                .setPositiveButton("Ok", new DialogInterface.OnClickListener() {

                    @Override
                    public void onClick(DialogInterface dialog, int id) {
                        boolean boolVal = isCheckedVal.isChecked();
                        String newValueAsString = String.valueOf(boolVal);
                        setNewPropertyValue(propertyId, EPropertyType.EPT_BOOL, newValueAsString, holder, newValueAsString);
                    }
                });
        builder.show();
    }

    private void openSetTextureValueDialog(int propertyId, TextView propertyValue, PropertiesHolder holder) {
        IMcTexture texture = (IMcTexture) mPropertiesValues.get(propertyId);
        TexturePropertyDialogFragment textureDialog = TexturePropertyDialogFragment.newInstance();
        textureDialog.setCurTexture(propertyId, texture);
        mCurTextureHolder = holder;
        textureDialog.show(mPropertiesIdListFragment.getFragmentManager().beginTransaction(), TexturePropertyDialogFragment.class.getSimpleName());
    }

    private void openSetFontValueDialog(int propertyId, TextView propertyValue, PropertiesHolder holder) {

        IMcFont font = (IMcFont) mPropertiesValues.get(propertyId);
        ObjectPropertiesBase.Text.getInstance().mPreviousFragmentText = ObjectPropertiesBase.Text.PreviousFragmentText.PropertiesList;

        FontPropertyDialogFragment fontDialog = FontPropertyDialogFragment.newInstance();
        fontDialog.setCurFont(propertyId, font);
        mCurFontHolder = holder;
        fontDialog.show(mPropertiesIdListFragment.getFragmentManager().beginTransaction(), FontPropertyDialogFragment.class.getSimpleName());
    }

    private void openSetMeshValueDialog(int propertyId, TextView propertyValue, PropertiesHolder holder) {

        IMcMesh mesh = (IMcMesh) mPropertiesValues.get(propertyId);
        MeshPropertyDialogFragment meshDialog = MeshPropertyDialogFragment.newInstance();
        meshDialog.setCurrentMesh(propertyId, mesh);
        mCurMeshHolder = holder;
        meshDialog.show(mPropertiesIdListFragment.getFragmentManager().beginTransaction(), MeshPropertyDialogFragment.class.getSimpleName());
    }

    private void openFVector2DValueDialog(final int propertyId, final PropertiesHolder holder, EPropertyType eType) {
        final View dialogView = ((Activity) mContext).getLayoutInflater().inflate(R.layout.fvector_2d_property_type, null);
        TextView propertyIdTV = (TextView) dialogView.findViewById(R.id.fvector2d_property_id);
        propertyIdTV.setText(String.valueOf(propertyId));
        final TwoDFVector propertyValue2DF = (TwoDFVector) dialogView.findViewById(R.id.fvector2d_property_value);
        if(eType == EPropertyType.EPT_VECTOR2D)
        {
            propertyValue2DF.setVisibility(View.INVISIBLE);
            final TwoDVector propertyValue2D = (TwoDVector) dialogView.findViewById(R.id.vector2d_property_value);
            propertyValue2D.setVisibility(View.VISIBLE);
            propertyValue2D.setVector2D((SMcVector2D) mPropertiesValues.get(propertyId));
        }
        else {
            propertyValue2DF.setVector2D((SMcFVector2D) mPropertiesValues.get(propertyId));
        }
        String title = "FVector2D property type";
        if(eType == EPropertyType.EPT_VECTOR2D)
            title = "Vector2D property type";

        AlertDialog.Builder builder = new AlertDialog.Builder(getContext());
        builder.setTitle(title).setView(dialogView)
                .setCancelable(false)
                .setPositiveButton("Ok", new DialogInterface.OnClickListener() {

                    @Override
                    public void onClick(DialogInterface dialog, int id) {
                        //  View dialogView = ((Activity) mContext).getLayoutInflater().inflate(R.layout.fvector_2d_property_type, null);
                        //  TwoDFVector newVal = (TwoDFVector) dialogView.findViewById(R.id.fvector2d_property_value);
                        SMcFVector2D newVal = propertyValue2DF.getVector2D();
                        String newValueAsString = "X = " + newVal.x + "; Y = " + newVal.y;
                        setNewPropertyValue(propertyId, null, newValueAsString, holder, newVal);
                    }
                });
        builder.show();
    }

    private void openSetColorValueDialog(final int propertyId, final TextView propertyValue, final PropertiesHolder holder) {
        final View dialogView = ((Activity) mContext).getLayoutInflater().inflate(R.layout.color_property_type, null);
        TextView propertyIdTV = (TextView) dialogView.findViewById(R.id.color_property_id);
        propertyIdTV.setText(String.valueOf(propertyId));
        SelectColor colorSC = (SelectColor) dialogView.findViewById(R.id.color_property_sc);
        colorSC.setmBColor(((ColorDrawable) propertyValue.getBackground()).getColor());
        colorSC.enableButtons(true);

        AlertDialog.Builder builder = new AlertDialog.Builder(getContext());
        builder.setTitle("Color property type").setView(dialogView)
                .setCancelable(false)
                .setPositiveButton("Ok", new DialogInterface.OnClickListener() {

                    @Override
                    public void onClick(DialogInterface dialog, int id) {
                        SelectColor colorSC = (SelectColor) dialogView.findViewById(R.id.color_property_sc);
                        propertyValue.setBackgroundColor(colorSC.getmSelectedColor());
                        String newValueAsString = colorSC.getmBColor().toString();
                        SMcBColor newVal = colorSC.getmBColor();
                        setNewPropertyValue(propertyId, null, newValueAsString, holder, newVal);
                    }
                });
        builder.show();
    }

    private void openSetNumericValueDialog(final int propertyId, final EPropertyType eType, final TextView propertyValueTV, final PropertiesHolder holder) {
        final View dialogView = ((Activity) mContext).getLayoutInflater().inflate(R.layout.numeric_property_type, null);
        TextView propertyIdTV = (TextView) dialogView.findViewById(R.id.numeric_property_id);
        propertyIdTV.setText(String.valueOf(propertyId));
        NumericEditTextLabel propertyValueET = (NumericEditTextLabel) dialogView.findViewById(R.id.numeric_property_value_et);
        propertyValueET.setText(String.valueOf(propertyValueTV.getText()));

        final AlertDialog.Builder builder = new AlertDialog.Builder(getContext());
        builder.setTitle("Numeric property type").setView(dialogView)
                .setCancelable(false)
                .setPositiveButton("Ok", new DialogInterface.OnClickListener() {

                    @Override
                    public void onClick(DialogInterface dialog, int id) {
                        NumericEditTextLabel newPropertyValue = (NumericEditTextLabel) dialogView.findViewById(R.id.numeric_property_value_et);
                        String newValueAsString = String.valueOf(newPropertyValue.getText());
                        Object newVal = null;
                        switch (eType) {
                            case EPT_SBYTE:
                                newVal = newPropertyValue.getByte();
                                break;
                            case EPT_DOUBLE:
                                newVal = newPropertyValue.getDouble();
                                break;
                            case EPT_FLOAT:
                                newVal = newPropertyValue.getFloat();
                                break;
                            case EPT_BYTE:
                                newVal = newPropertyValue.getByte();
                                break;
                            case EPT_ENUM:
                                newVal = newPropertyValue.getInt();
                                break;
                            case EPT_INT:
                                newVal = newPropertyValue.getInt();
                                break;
                            case EPT_UINT:
                                newVal = newPropertyValue.getUInt();
                                break;
                        }
                        setNewPropertyValue(propertyId, eType, newValueAsString, holder, newVal);
                    }
                });
        builder.show();
    }

    private void openSetStringValueDialog(final int propertyId, final PropertiesHolder holder) {
        final View dialogView = ((Activity) mContext).getLayoutInflater().inflate(R.layout.string_property_type, null);
        TextView propertyIdTV = (TextView) dialogView.findViewById(R.id.string_property_id);
        NumericEditTextLabel propertyValueET = (NumericEditTextLabel) dialogView.findViewById(R.id.string_property_value_et);

        propertyIdTV.setText(String.valueOf(propertyId));
        //if(mPropertiesValues.containsKey(propertyId))

        final SMcVariantString variantString = (SMcVariantString) mPropertiesValues.get(propertyId);
        if(variantString != null && variantString.str != null && variantString.str.length > 0)
            propertyValueET.setText(variantString.str[0]);

        final AlertDialog.Builder builder = new AlertDialog.Builder(getContext());
        builder.setTitle("string property type").setView(dialogView)
                .setCancelable(false)
                .setPositiveButton("Ok", new DialogInterface.OnClickListener() {

                    @Override
                    public void onClick(DialogInterface dialog, int id) {
                        NumericEditTextLabel newPropertyValue = (NumericEditTextLabel) dialogView.findViewById(R.id.string_property_value_et);
                        String newValueAsString = newPropertyValue.getText();
                        SMcVariantString newVariantString = new SMcVariantString(newValueAsString, variantString == null? false: variantString.isUnicode);
                        setNewPropertyValue(propertyId, EPropertyType.EPT_STRING,  newValueAsString, holder, newVariantString);
                    }
                });
        builder.show();
    }

    private void setNewPropertyValue(int propertyId, EPropertyType eType, String newValAsString, PropertiesHolder holder, Object newVal) {
        holder.propertyValue.setText(newValAsString);
        mUpdatedRowsHM.put(propertyId, holder);
        String boolVal;
        if (eType != null && eType == EPropertyType.EPT_BOOL) {
            boolVal = new String((String) newVal);
            newVal = new Boolean(boolVal);
        }
        mPropertiesValues.put(propertyId, newVal);
        holder.isChanged.setChecked(true);
    }

    private String getArrPropertiesAsString(EPropertyType propType, SArrayProperty<?> arrPropertyVal) {
        String propValAsString = "";
        switch (propType) {
            case EPT_BCOLOR_ARRAY:
                SArrayProperty<SMcBColor> colors = (SArrayProperty<SMcBColor>) arrPropertyVal;
                if (colors.aElements != null) {
                    for (SMcBColor color : colors.aElements) {
                        propValAsString += "(" + color.toString() + ") ";
                    }
                }
                break;
            case EPT_UINT_ARRAY:
                SArrayProperty<Integer> numbers_arr = (SArrayProperty<Integer>) arrPropertyVal;
                if (numbers_arr.aElements != null) {
                    for (Integer element : numbers_arr.aElements) {
                        propValAsString += element.toString() + " ";
                    }
                }

                break;
            case EPT_SUBITEM_ARRAY:
                SArrayProperty<SMcSubItemData> subItemsData = (SArrayProperty<SMcSubItemData>) arrPropertyVal;
                propValAsString = getSubItemsDataAsString(subItemsData);

                break;
            case EPT_VECTOR2D_ARRAY:
                SArrayProperty<SMcVector2D> vector2D_arr = (SArrayProperty<SMcVector2D>) arrPropertyVal;
                if (vector2D_arr.aElements != null) {
                    for (SMcVector2D element : vector2D_arr.aElements) {
                        propValAsString = "(X = " + element.x + ", Y = " + element.y + ") ";
                    }
                }

                break;
            case EPT_FVECTOR2D_ARRAY:
                SArrayProperty<SMcFVector2D> fvector2D_arr = (SArrayProperty<SMcFVector2D>) arrPropertyVal;
                if (fvector2D_arr.aElements != null) {
                    for (SMcFVector2D element : fvector2D_arr.aElements) {
                        propValAsString += "(X = " + element.x + ", Y = " + element.y + ") ";
                    }
                }

                break;
            case EPT_VECTOR3D_ARRAY:
                SArrayProperty<SMcVector3D> vector3D_arr = (SArrayProperty<SMcVector3D>) arrPropertyVal;
                if (vector3D_arr.aElements != null) {
                    for (SMcVector3D element : vector3D_arr.aElements) {
                        propValAsString += "(X = " + element.x + ", Y = " + element.y + ", Z = " + element.z + ") ";
                    }
                }

                break;
            case EPT_FVECTOR3D_ARRAY:
                SArrayProperty<SMcFVector3D> fvector3D_arr = (SArrayProperty<SMcFVector3D>) arrPropertyVal;
                if (fvector3D_arr.aElements != null) {
                    for (SMcFVector3D element : fvector3D_arr.aElements) {
                        propValAsString += "(X = " + element.x + ", Y = " + element.y + ", Z = " + element.z + ") ";
                    }
                }

        }
        return propValAsString;
    }

    private String getSubItemsDataAsString(SArrayProperty<SMcSubItemData> subItemsData) {
        String propValAsString = null;
        if (subItemsData.aElements != null && subItemsData.aElements.length > 0) {
            String ids = "ID: ";
            String indexes = "Points Start Index: ";
            for (SMcSubItemData itemData : subItemsData.aElements) {
                ids += itemData.uSubItemID + " ";
                indexes += itemData.nPointsStartIndex + " ";
            }
            propValAsString = ids + "\n" + indexes;
        }
        return propValAsString;
    }

    private String getPropertyAsString(EPropertyType propType, Object propVal, View propertyView) {
        String propValAsString = "";
        switch (propType) {
            case EPT_ANIMATION:
                SMcAnimation animation = (SMcAnimation) propVal;
                propValAsString = "Animation Name:\t" + animation.strAnimationName +
                        "\nIs Loop:\t" + animation.bLoop;
                break;
            case EPT_ATTENUATION:
                SMcAttenuation attenuation = (SMcAttenuation) propVal;
                propValAsString = "Const:\t" + attenuation.fConst +
                        "\nLinear:\t" + attenuation.fLinear +
                        "\nSquare:\t" + attenuation.fSquare +
                        "\nRange:\t" + attenuation.fRange;
                break;
            case EPT_BOOL:
                propValAsString = propVal.toString();
                break;
            case EPT_INT:
            case EPT_UINT:
            case EPT_FLOAT:
            case EPT_SBYTE:
            case EPT_DOUBLE:
            case EPT_BYTE:
            case EPT_ENUM:
                propValAsString = propVal.toString();
                break;
            case EPT_CONDITIONALSELECTOR:
                try {
                    propValAsString = "ID = " + ((McConditionalSelector) propVal).GetID() +
                            "\n" + "Type = " + ((McConditionalSelector) propVal).GetConditionalSelectorType().toString();
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetConditionalSelectorType/GetID");
                } catch (Exception e) {
                    e.printStackTrace();
                }
                break;
            case EPT_FCOLOR:
                propValAsString = ((SMcFColor) propVal).toString();
                break;
             case EPT_FONT:
               try {
                    IMcFont.EFontType fontType = ((IMcFont)propVal).GetFontType();
                    propValAsString = fontType.toString();
                   /* if(fontType == IMcFont.EFontType.EFT_LOG_FONT)
                    {
                        double fontSizeInPoints = 0;
                        fontSizeInPoints = Math.round(((IMcLogFont) propVal).GetLogFont().LogFont.lfHeight);
                        propValAsString = ((IMcLogFont) propVal).GetLogFont().LogFont.lfFaceName +
                                "\n" + "lfHeight = " + fontSizeInPoints;
                    }
                    else
                    {
                        IMcFileFont fileFont = (IMcFileFont)propVal;
                        SMcFileSource fileSource = new SMcFileSource();
                        Integer height = 0;
                        fileFont.GetFontFileAndHeight(fileSource, height);

                        propValAsString = fileSource.strFileName + "\n" + "Height = " + height;;
                        if(fileSource.bIsMemoryBuffer)
                            propValAsString+= "(is memory buffer)";
                    }*/
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetFont");
                }catch (Exception e) {
                    e.printStackTrace();
                }

                break;
            case EPT_MESH:
                try {
                    propValAsString = ((IMcMesh)propVal).GetMeshType().toString();
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetMeshType");
                }catch (Exception e) {
                    e.printStackTrace();
                }
                break;
            case EPT_FVECTOR3D:
                SMcFVector3D fvect3d = (SMcFVector3D) propVal;
                propValAsString = "X = " + fvect3d.x + "; Y = " + fvect3d.y + "; Z = " + fvect3d.z;
                break;
            case EPT_ROTATION:
                SMcRotation rotation = (SMcRotation) propVal;
                propValAsString = "Yaw = " + rotation.fYaw +
                        "\n" + "Pitch = " + rotation.fPitch +
                        "\n" + "Roll = " + rotation.fRoll +
                        "\n" + "Relative To Curr Orientation: " + rotation.bRelativeToCurrOrientation;
                break;
            case EPT_STRING:
                SMcVariantString variantString = (SMcVariantString) propVal;
                if(variantString.str != null && variantString.str.length > 0)
                    propValAsString = variantString.str[0];
                break;
            case EPT_VECTOR2D:
                SMcVector2D vect2d = (SMcVector2D) propVal;
                propValAsString = "X = " + vect2d.x + "; Y = " + vect2d.y;
                break;
            case EPT_VECTOR3D:
                SMcVector3D vect3d = (SMcVector3D) propVal;
                propValAsString = "X = " + vect3d.x + "; Y = " + vect3d.y + "; Z = " + vect3d.z;
                break;
            case EPT_BCOLOR:
                SMcBColor colorVal = null;
                colorVal = (SMcBColor) propVal;
                propValAsString = "(" + colorVal.a + "," + colorVal.r + "," + colorVal.g + "," + colorVal.b + ")";
                propertyView.setBackgroundColor(Color.argb(colorVal.a, colorVal.r, colorVal.g, colorVal.b));
                break;
            case EPT_TEXTURE:
                try {
                    propValAsString = ((IMcTexture) propVal).GetName();
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "(IMcTexture)propVal).GetName()");
                } catch (Exception e) {
                    e.printStackTrace();
                }
                break;
            case EPT_FVECTOR2D:
                SMcFVector2D fvect2d = (SMcFVector2D) propVal;
                propValAsString = "X = " + fvect2d.x + "; Y = " + fvect2d.y;
                break;
            default:
                propValAsString = String.valueOf(propVal);
        }

        return propValAsString;
    }

    private SArrayProperty<?> getArrPropertiesMethods(EPropertyType propType, int propID, String strName) {
        IMcObject currObject = mImcObject;

        SArrayProperty<?> propVal = new SArrayProperty<>();
        if (propType == EPropertyType.EPT_SUBITEM_ARRAY) {
            try {
                propVal = ((IMcObject) currObject).GetArrayProperty(propID, EPropertyType.EPT_SUBITEM_ARRAY);
            } catch (MapCoreException e) {
                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetArrayProperty(propID,EPropertyType.EPT_SUBITEM_ARRAY )");
            } catch (Exception e) {
                e.printStackTrace();
            }
        } else if (propType == EPropertyType.EPT_UINT_ARRAY) {
            try {
                propVal = currObject.GetArrayProperty(propID, EPropertyType.EPT_UINT_ARRAY);
            } catch (MapCoreException e) {
                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetArrayProperty(propID, EPropertyType.EPT_UINT_ARRAY )");
            } catch (Exception e) {
                e.printStackTrace();
            }
        } else if (propType == EPropertyType.EPT_FVECTOR2D_ARRAY) {
            try {
                propVal = currObject.GetArrayProperty(propID, EPropertyType.EPT_FVECTOR2D_ARRAY);
            } catch (MapCoreException e) {
                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetArrayProperty(propID,EPropertyType.EPT_FVECTOR2D_ARRAY )");
            } catch (Exception e) {
                e.printStackTrace();
            }
        } else if (propType == EPropertyType.EPT_VECTOR2D_ARRAY) {
            try {
                propVal = currObject.GetArrayProperty(propID, EPropertyType.EPT_VECTOR2D_ARRAY);
            } catch (MapCoreException e) {
                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetArrayProperty(propID,EPropertyType.EPT_VECTOR2D_ARRAY )");
            } catch (Exception e) {
                e.printStackTrace();
            }
        } else if (propType == EPropertyType.EPT_FVECTOR3D_ARRAY) {
            try {
                propVal = currObject.GetArrayProperty(propID, EPropertyType.EPT_FVECTOR3D_ARRAY);
            } catch (MapCoreException e) {
                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetArrayProperty(propID, EPropertyType.EPT_FVECTOR3D_ARRAY)");
            } catch (Exception e) {
                e.printStackTrace();
            }
        } else if (propType == EPropertyType.EPT_VECTOR3D_ARRAY) {
            try {
                propVal = currObject.GetArrayProperty(propID, EPropertyType.EPT_VECTOR3D_ARRAY);
            } catch (MapCoreException e) {
                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetArrayProperty(propID, EPropertyType.EPT_VECTOR3D_ARRAY");
            } catch (Exception e) {
                e.printStackTrace();
            }
        } else if (propType == EPropertyType.EPT_BCOLOR_ARRAY) {
            try {
                propVal = currObject.GetArrayProperty(propID, EPropertyType.EPT_BCOLOR_ARRAY);

            } catch (MapCoreException e) {
                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetArrayProperty(propID, EPropertyType.EPT_BCOLOR_ARRAY) )");
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
        return propVal;
    }

    private Object getPropertiesMethods(EPropertyType propType, int propID, String strName) {
        boolean propValBool;
        IMcObject currObject = mImcObject;
        Object propVal = null;
        // Method[] MiArray = currObject.getClass().getMethods();
        String propName;
        for (Method mi : mImcObjectMethodsArr) {
            propName = propType.toString();
            propName = propName.toLowerCase();
            propName = propName.replace("ept_", ""); //intentionally small letters

            if (mi.getName().toLowerCase().contains("get" + propName + "property")) {
                Type[] types = mi.getGenericParameterTypes();
                if(types != null && types.length > 0) {
                    Type type = types[0];
                    if(type ==  strName.getClass())
                    {
                        Object[] paramsArr = new Object[1];
                        paramsArr[0] = strName;
                        try {
                            if (propName.equals("bool")) {
                                try {
                                    propValBool = mImcObject.GetBoolProperty(strName);
                                    //return Boolean.valueOf(propValBool);
                                    return propValBool ? new Boolean(true) : new Boolean(false);
                                } catch (MapCoreException e) {
                                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetBoolProperty(propID, EPropertyType.EPT_BCOLOR_ARRAY) )");
                                } catch (Exception e) {
                                    e.printStackTrace();
                                }
                            } else {
                                propVal = mi.invoke(currObject, paramsArr);
                                int i = 2;
                                i++;
                            }
                        } catch (IllegalAccessException e) {

                        } catch (InvocationTargetException e) {
                            e.printStackTrace();
                        }
                        break;

                    }
                }

            }
        }

        return propVal;
    }

    //todo merge this func with getPropertiesMethod
    public Object getDefaultPropertiesMethod(EPropertyType propType, int propID, String strName) {
        Object propVal = null;
        boolean propValBool;
        // Method[] miArray = mObjectScheme.getClass().getMethods();
        String propName;
        for (Method mi : mObjSchemeMethodsArr) {
            propName = propType.toString();
            propName = propName.toLowerCase();
            propName = propName.replace("ept_", ""); //intentionally small letters

            if (mi.getName().toLowerCase().contains("get" + propName + "propertydefault")) {
                //this is the function we want to invoke
                Type[] types = mi.getGenericParameterTypes();
                if(types != null && types.length > 0) {
                    Type type = types[0];
                    if(type ==  strName.getClass()) {
                        Object[] paramsArr = new Object[1];
                        paramsArr[0] = strName;
                        if (propName.equals("bool")) {
                            try {
                                propValBool = mObjectScheme.GetBoolPropertyDefault(strName);
                                // return Boolean.valueOf(propValBool);
                                return propValBool ? new Boolean(true) : new Boolean(false);
                            } catch (MapCoreException e) {
                                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetBoolPropertyDefault )");
                            } catch (Exception e) {
                                e.printStackTrace();
                            }
                        }
                        try {
                            propVal = mi.invoke(mObjectScheme, paramsArr);
                        } catch (IllegalAccessException e) {
                            e.printStackTrace();
                        } catch (InvocationTargetException e) {
                            e.printStackTrace();
                        }
                        break;
                    }
                }
            }

        }
        return propVal;
    }

    public SArrayProperty<?> getDefaultArrPropertiesMethod(EPropertyType propType, int propID) {
        SArrayProperty<?> propVal = new SArrayProperty<>();
        try {
            if (propType == IMcProperty.EPropertyType.EPT_SUBITEM_ARRAY) {
                propVal = mObjectScheme.GetArrayPropertyDefault(propID, EPropertyType.EPT_SUBITEM_ARRAY);
            } else if (propType == EPropertyType.EPT_UINT_ARRAY) {
                propVal = mObjectScheme.GetArrayPropertyDefault(propID, EPropertyType.EPT_UINT_ARRAY);
            } else if (propType == EPropertyType.EPT_FVECTOR2D_ARRAY) {
                propVal = mObjectScheme.GetArrayPropertyDefault(propID, EPropertyType.EPT_FVECTOR2D_ARRAY);
            } else if (propType == EPropertyType.EPT_VECTOR2D_ARRAY) {
                propVal = mObjectScheme.GetArrayPropertyDefault(propID, EPropertyType.EPT_VECTOR2D_ARRAY);
            } else if (propType == EPropertyType.EPT_FVECTOR3D_ARRAY) {
                propVal = mObjectScheme.GetArrayPropertyDefault(propID, EPropertyType.EPT_FVECTOR3D_ARRAY);
            } else if (propType == EPropertyType.EPT_VECTOR3D_ARRAY) {
                propVal = mObjectScheme.GetArrayPropertyDefault(propID, EPropertyType.EPT_VECTOR3D_ARRAY);
            } else if (propType == EPropertyType.EPT_BCOLOR_ARRAY) {
                propVal = mObjectScheme.GetArrayPropertyDefault(propID, EPropertyType.EPT_BCOLOR_ARRAY);
            }
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetArrayPropertyDefault");
        } catch (Exception e) {
            e.printStackTrace();
        }
        return propVal;

    }

    public String getPropertyName(int uID) {
        String propertyName = "";
        //if prop name exist in updated names, take updated val
        propertyName = mUpdatedPropertiesNames.get(uID);
        if (propertyName == null) {
            try {
                propertyName = mObjectScheme.GetPropertyNameByID(uID);
            } catch (MapCoreException e) {
                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetPropertyNameByID");
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
        return propertyName;
    }

    public void updateTextureProperty(int propertyId, IMcTexture texture) {
        String textureName = null;
        try {
            textureName = texture.GetName();
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetName");
        } catch (Exception e) {
            e.printStackTrace();
        }

        setNewPropertyValue(propertyId, EPropertyType.EPT_TEXTURE, textureName, mCurTextureHolder, texture);

    }

    public void updateFontProperty(int propertyId, IMcFont font) {
        setNewPropertyValue(propertyId, EPropertyType.EPT_FONT, getPropertyAsString(EPropertyType.EPT_FONT,font,null), mCurFontHolder, font);
    }

    public void updateMeshProperty(int propertyId, IMcMesh Mesh) {
        setNewPropertyValue(propertyId, EPropertyType.EPT_MESH, getPropertyAsString(EPropertyType.EPT_MESH,Mesh,null), mCurMeshHolder, Mesh);
    }

    public class PropertiesHolder {
        public TextView propertyId;
        public TextView type;
        public TextView name;
        public Button value;
        public CheckBox useDefault;
        public TextView propertyValue;
        public CheckBox isChanged;
        public CheckBox toReset;
    }

    @Override
    public int getCount() {
        return mPropertyIdArr.length;
    }
}
