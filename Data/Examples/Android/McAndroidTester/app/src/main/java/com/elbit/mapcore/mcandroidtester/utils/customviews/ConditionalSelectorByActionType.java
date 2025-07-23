package com.elbit.mapcore.mcandroidtester.utils.customviews;

import android.content.Context;
import android.os.Bundle;
import android.os.Parcelable;
import androidx.annotation.Nullable;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CheckedTextView;
import android.widget.LinearLayout;
import android.widget.ListView;

import com.elbit.mapcore.General.ObjectRef;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.ui.adapters.ConditionalSelectorHashMapAdapter;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

import java.util.HashMap;
import java.util.Map;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcConditionalSelector;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlay;

/**
 * TODO: document your custom view class.
 */
public class ConditionalSelectorByActionType extends LinearLayout {

    public enum Source {Overlay, Object};

    private Context mContext;
    private View mView;
    private CheckBox mActionOnResultCB;
    private SpinnerWithLabel mActionType;
    private Object mObject;
    private ListView mConditionalSelectorLV;
    private Button mBtnClear;

    private HashMap<Integer, Object> mConditionalSelectorList;
    private ConditionalSelectorHashMapAdapter mConditionalSelectorAdapter;
    //private IMcConditionalSelector mConditionalSelectorSelected;
    private IMcConditionalSelector mCurrConditionalSelector;

    public ConditionalSelectorByActionType(Context context) {
        super(context);
    }

    public ConditionalSelectorByActionType(Context context, AttributeSet attrs) {
        super(context, attrs);
        try {
            mContext = context;
            LayoutInflater inflater = (LayoutInflater) context
                    .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
            mView = inflater.inflate(R.layout.cv_conditional_selector_by_action_type, this);
            mActionOnResultCB = (CheckBox) mView.findViewById(R.id.cv_csbat_action_on_result_cb);
            mActionType = (SpinnerWithLabel) mView.findViewById(R.id.cv_csbat_selector_action_type_swl);
            mActionType.setAdapter(new ArrayAdapter<>(context, android.R.layout.simple_spinner_item, IMcConditionalSelector.EActionType.values()));
            mActionType.setSelection(IMcConditionalSelector.EActionType.EAT_VISIBILITY.getValue());
            mConditionalSelectorList = new HashMap<>();
            mConditionalSelectorLV = (ListView) mView.findViewById(R.id.cv_csbat_conditional_selector_lv);
            mBtnClear = (Button) mView.findViewById(R.id.cv_csbat_clear_btn);
            mBtnClear.setOnClickListener(new OnClickListener() {
                @Override
                public void onClick(View view) {
                    mConditionalSelectorLV.setItemChecked(-1, true);
                    mCurrConditionalSelector = null;
                }
            });
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public ConditionalSelectorByActionType(Context context, AttributeSet attrs, int defStyle) {
        super(context, attrs, defStyle);
    }


    public void setObject(Object obj) {
        mObject = obj;
        ObjectRef<Boolean> actionOnResult = new ObjectRef<>();
        IMcConditionalSelector condSelectors[] = new IMcConditionalSelector[0];

        try {
            if (mObject instanceof IMcOverlay) {
                condSelectors = ((IMcOverlay) mObject).GetOverlayManager().GetConditionalSelectors();
                mCurrConditionalSelector = ((IMcOverlay) mObject).GetConditionalSelector(IMcConditionalSelector.EActionType.EAT_VISIBILITY, actionOnResult);
            } else if (mObject instanceof IMcObject) {
                condSelectors = ((IMcObject) mObject).GetOverlayManager().GetConditionalSelectors();
                mCurrConditionalSelector = ((IMcObject) mObject).GetConditionalSelector(IMcConditionalSelector.EActionType.EAT_VISIBILITY, actionOnResult);
            }
            for (IMcConditionalSelector condSelector : condSelectors) {
                mConditionalSelectorList.put(condSelector.hashCode(), condSelector);
            }
            if (mConditionalSelectorList.size() > 0) {
                mConditionalSelectorAdapter = new ConditionalSelectorHashMapAdapter(getContext(), mConditionalSelectorList, Consts.ListType.SINGLE_CHECK);
                mConditionalSelectorLV.setAdapter(mConditionalSelectorAdapter);
                mConditionalSelectorLV.setChoiceMode(ListView.CHOICE_MODE_SINGLE);
                for (int i = 0; i < mConditionalSelectorAdapter.getCount(); i++) {
                    if (mConditionalSelectorAdapter.getItem(i).getValue().equals(mCurrConditionalSelector))
                        mConditionalSelectorLV.setItemChecked(i, true);
                }
            }

            Funcs.setListViewHeightBasedOnChildren(mConditionalSelectorLV);

            mActionOnResultCB.setChecked(actionOnResult.getValue());

        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(mContext, e, "GetConditionalSelector");
        } catch (Exception e) {
            e.printStackTrace();
        }


        mConditionalSelectorLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int index, long l) {
                Map.Entry<Object, Object> item = mConditionalSelectorAdapter.getItem(index);
                if (((CheckedTextView) view).isChecked())
                    mCurrConditionalSelector = (IMcConditionalSelector) item.getValue();
                else
                    mCurrConditionalSelector = null;
            }
        });
    }

    @Nullable
    @Override
    protected Parcelable onSaveInstanceState() {
        Bundle bundle = new Bundle();
        bundle.putParcelable("superState", super.onSaveInstanceState());
        bundle.putSerializable("mCurrConditionalSelector",new AMCTSerializableObject(mCurrConditionalSelector));
        return bundle;

    }

    @Override
    protected void onRestoreInstanceState(Parcelable state) {
        if (state instanceof Bundle) // implicit null check
        {
            Bundle bundle = (Bundle) state;
            AMCTSerializableObject mcObject = (AMCTSerializableObject)bundle.getSerializable("mCurrConditionalSelector");
            if(mcObject != null && mcObject.getMcObject() != null)
            mCurrConditionalSelector = (IMcConditionalSelector)mcObject.getMcObject();
            state = bundle.getParcelable("superState");
        }
        super.onRestoreInstanceState(state);

    }

    public void Save() {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    if (mObject instanceof IMcOverlay) {
                        ((IMcOverlay) mObject).SetConditionalSelector(IMcConditionalSelector.EActionType.EAT_VISIBILITY, mActionOnResultCB.isChecked(), mCurrConditionalSelector);
                    } else if (mObject instanceof IMcObject) {
                        ((IMcObject) mObject).SetConditionalSelector(IMcConditionalSelector.EActionType.EAT_VISIBILITY, mActionOnResultCB.isChecked(), mCurrConditionalSelector);
                    }
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(mContext, e, "SetConditionalSelector");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }
}
