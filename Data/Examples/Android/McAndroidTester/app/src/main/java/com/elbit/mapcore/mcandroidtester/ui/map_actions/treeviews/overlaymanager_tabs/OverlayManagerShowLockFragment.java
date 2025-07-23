package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlaymanager_tabs;


import android.app.Dialog;
import android.os.Bundle;
import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.DialogFragment;
import androidx.appcompat.app.AlertDialog;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AbsListView;
import android.widget.ListView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.IOverlayManagerShowLockFragment;
import com.elbit.mapcore.mcandroidtester.ui.adapters.HashMapAdapter;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.BaseApplication;
import com.elbit.mapcore.mcandroidtester.utils.Consts;

import java.util.HashMap;
import java.util.Hashtable;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcConditionalSelector;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectScheme;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

/**
 * Created by tc97803 on 21/11/2016.
 */
public class OverlayManagerShowLockFragment extends DialogFragment implements IOverlayManagerShowLockFragment {

    IMcOverlayManager mOverlayManager;
    IOverlayManagerShowLockFragment.EItemsType type;
    private ListView mSchemedLV;
    private HashMapAdapter mSchemesAdapter;
    private HashMap<Object, Boolean> mapObjectsLocked;
    Hashtable<Object,Boolean> cc;
    IMcObjectScheme[] arraySchemes;
    IMcConditionalSelector[] arrayCS;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        int eTypeValue = getArguments().getInt("type");
        type = IOverlayManagerShowLockFragment.EItemsType.values()[eTypeValue];
        mapObjectsLocked = new HashMap<>();
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        setTitle();
        // Inflate the layout for this fragment
        View inflaterView = inflater.inflate(R.layout.fragment_overlay_manager_show_lock, container, false);
        return inflaterView;
    }

    private void setTitle() {
    }

    @Override
    public void onHiddenChanged(boolean hidden) {
        super.onHiddenChanged(hidden);
        if(!hidden)
            setTitle();
    }
    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
    }

    @Override
    public void getObject(IMcOverlayManager overlayManager) {
        mOverlayManager = overlayManager;
    }

    @NonNull
    @Override
    public Dialog onCreateDialog(Bundle savedInstanceState) {
        View v = getActivity().getLayoutInflater().inflate(R.layout.fragment_overlay_manager_show_lock, null);
        mSchemedLV = (ListView) v.findViewById(R.id.om_show_lock_lv);
        v.findViewById(R.id.btn_lock_ok).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(type == EItemsType.ObjectScheme) {
                    if (arraySchemes != null && arraySchemes.length > 0) {
                        for (int i = 0; i < arraySchemes.length; i++) {
                            final int index = i;
                            final boolean isLock = mSchemedLV.isItemChecked(i);
                            Funcs.runMapCoreFunc(new Runnable() {
                                @Override
                                public void run() {
                                    try {
                                        mOverlayManager.SetObjectSchemeLock(arraySchemes[index], isLock);
                                    } catch (MapCoreException McEx) {
                                        McEx.printStackTrace();
                                        AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), McEx, "IMcOverlayManager.SetObjectSchemeLock");
                                    } catch (Exception ex) {
                                        ex.printStackTrace();
                                    }
                                }
                            });
                        }
                    }
                }
                else if(type == EItemsType.ConditionalSelector) {
                    if (arrayCS != null && arrayCS.length > 0) {
                        for (int i = 0; i < arrayCS.length; i++) {
                            final int index = i;
                            final boolean isLock = mSchemedLV.isItemChecked(i);
                            Funcs.runMapCoreFunc(new Runnable() {
                                @Override
                                public void run() {
                                    try {
                                        mOverlayManager.SetConditionalSelectorLock(arrayCS[index], isLock);
                                    } catch (MapCoreException McEx) {
                                        McEx.printStackTrace();
                                        AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), McEx, "IMcOverlayManager.SetConditionalSelectorLock");
                                    } catch (Exception ex) {
                                        ex.printStackTrace();
                                    }
                                }
                            });

                        }
                    }
                }
                dismiss();
            }
        });
        mSchemedLV.setChoiceMode(AbsListView.CHOICE_MODE_MULTIPLE);

            if(type == EItemsType.ObjectScheme) {
                try {
                    arraySchemes = mOverlayManager.GetObjectSchemes();
                    if (arraySchemes != null && arraySchemes.length > 0) {
                        initListView(arraySchemes);
                        for (int i = 0; i < arraySchemes.length; i++) {
                            boolean isLock = false;
                            isLock = mOverlayManager.IsObjectSchemeLocked(arraySchemes[i]);
                            if (isLock)
                                mSchemedLV.setItemChecked(i, isLock);
                        }
                    }
                } catch (MapCoreException McEx) {
                    McEx.printStackTrace();
                    AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), McEx, "IMcOverlayManager.GetObjectSchemes/IsObjectSchemeLocked");
                } catch (Exception ex) {
                    ex.printStackTrace();
                }
            }
            else if(type == EItemsType.ConditionalSelector) {
                try {
                    arrayCS = mOverlayManager.GetConditionalSelectors();
                    if (arrayCS != null && arrayCS.length > 0) {
                        initListView(arrayCS);
                        for (int i = 0; i < arrayCS.length; i++) {
                            boolean isLock = mOverlayManager.IsConditionalSelectorLocked(arrayCS[i]);
                            if (isLock)
                                mSchemedLV.setItemChecked(i, isLock);
                        }
                    }
                } catch (MapCoreException McEx) {
                    McEx.printStackTrace();
                    AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), McEx, "IMcOverlayManager.GetConditionalSelectors/IsConditionalSelectorLocked");
                } catch (Exception ex) {
                    ex.printStackTrace();
                }
            }
            mSchemedLV.deferNotifyDataSetChanged();

        AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());

        builder.setView(v);

        return builder.create();
    }

    private void initListView(Object[] objects)
    {
        HashMap<Object, Integer> hashMapSchemes = new HashMap<Object, Integer>();
        for (Object object : objects) {
            hashMapSchemes.put(object, object.hashCode());
        }
        mSchemesAdapter = new HashMapAdapter(null, hashMapSchemes, Consts.ListType.MULTIPLE_CHECK);
        mSchemedLV.setChoiceMode(ListView.CHOICE_MODE_MULTIPLE);
        mSchemedLV.setAdapter(mSchemesAdapter);

    }
}
