package com.elbit.mapcore.mcandroidtester.ui.map_actions;


import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.os.Message;
import androidx.annotation.Nullable;
import androidx.fragment.app.DialogFragment;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ExpandableListAdapter;
import android.widget.ExpandableListView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.interfaces.IEditModeFragmentCallback;
import com.elbit.mapcore.mcandroidtester.managers.ObjectWorld.Manager_MCOverlayManager;
import com.elbit.mapcore.mcandroidtester.ui.adapters.CustomExpandableListAdapter;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.List;

import com.elbit.mapcore.Classes.OverlayManager.McObjectSchemeNode;
import com.elbit.mapcore.General.CMcEnumBitField;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectScheme;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeNode;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlay;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;

/**
 * Created by tc97803 on 21/11/2016.
 */
public class EditModeFragment extends DialogFragment implements FragmentWithObject {
    private static final int GET_OBJECT_LIST_COMPLETED = 1;
    private static final int GET_OBJECT_LIST_FAILED = 2;
    ExpandableListView expandableListView;
    ExpandableListAdapter expandableListAdapter;
    List<IMcObject> objectsList = new ArrayList<>();
    HashMap<IMcObject, List<IMcObjectSchemeNode>> objectsSchemeNodeList = new HashMap<>();
    IMcObject selectedObject;
    IMcObjectSchemeItem selectedItem;
    IEditModeFragmentCallback.EType callbackType = IEditModeFragmentCallback.EType.Init_Mode;
    Handler mHandler;
    IMcObject m_selectedScanObject;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        int eTypeValue = getArguments().getInt("type");
        callbackType = IEditModeFragmentCallback.EType.values()[eTypeValue];

        Funcs.SetObjectFromBundle(savedInstanceState, this );
        initEditModeHandler();
    }

    private void initEditModeHandler() {
        mHandler = new Handler(Looper.getMainLooper()) {
            @Override
            public void handleMessage(Message msg) {
                switch (msg.what) {
                    case GET_OBJECT_LIST_COMPLETED:
                        doUiActionsAfterGetObjListFromRenderThread();
                        break;
                    case GET_OBJECT_LIST_FAILED:
                        showAlertMsg(msg.obj);
                        break;
                }
            }
        };
    }

    private void showAlertMsg(Object obj) {
        AlertMessages.ShowMapCoreErrorMessage(this.getContext(), (MapCoreException) obj, "GetObjectsList");
    }

    private void doUiActionsAfterGetObjListFromRenderThread() {
        expandableListView = (ExpandableListView) getView().findViewById(R.id.expandableListView);
        expandableListAdapter = new CustomExpandableListAdapter(this.getContext(), objectsList, objectsSchemeNodeList);
        expandableListView.setAdapter(expandableListAdapter);
        expandableListView.setOnChildClickListener(new ExpandableListView.OnChildClickListener() {
            @Override
            public boolean onChildClick(ExpandableListView expandableListView, View view, int groupPosition, int childPosition, long id) {
                selectedObject = objectsList.get(groupPosition);
                selectedItem = (IMcObjectSchemeItem) objectsSchemeNodeList.get(selectedObject).get(childPosition);

                dismiss();
                IEditModeFragmentCallback callback = (IEditModeFragmentCallback) getTargetFragment();
                callback.callBackEditMode(callbackType, selectedObject, selectedItem);

                return false;
            }
        });

        expandableListView.setOnTouchListener(new View.OnTouchListener() {
            // Setting on Touch Listener for handling the touch inside ScrollView
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                // Disallow the touch request for parent scroll on touch of child view
                v.getParent().requestDisallowInterceptTouchEvent(true);
                return false;
            }
        });
        if(m_selectedScanObject != null) {
            int indexScanObject = -1;
            for (int i = 0; i < objectsList.size(); i++) {
                if (objectsList.get(i) == m_selectedScanObject) {
                    indexScanObject = i;
                    break;
                }
            }
            if (indexScanObject >= 0) {
                expandableListView.expandGroup(indexScanObject);
            }
        }
        Button btnScan = (Button) getView().findViewById(R.id.btnEditModeScan);
        btnScan.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                dismiss();
                IEditModeFragmentCallback callback = (IEditModeFragmentCallback) getTargetFragment();
                IEditModeFragmentCallback.EType scanType = IEditModeFragmentCallback.EType.Scan_From_Init;
                if(callbackType == IEditModeFragmentCallback.EType.Edit_Mode)
                    scanType = IEditModeFragmentCallback.EType.Scan_From_Edit;
                callback.callBackEditMode(scanType, null, null);
            }
        });
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        View inflaterView = inflater.inflate(R.layout.fragment_edit_mode_list, container, false);
        return inflaterView;
    }

    private void GetObjectsListFromRenderThread() {
        final IMcOverlayManager mcActiveOM = Manager_MCOverlayManager.getInstance().getActiveOverlayManager();
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    IMcOverlay[] overlays = mcActiveOM.GetOverlays();
                    if (overlays != null) {
                        for (IMcOverlay mcOverlay : overlays) {
                            IMcObject[] objects = mcOverlay.GetObjects();
                            if (objects != null) {
                                for (IMcObject mcObject : objects) {

                                    IMcObjectScheme scheme = mcObject.GetScheme();
                                    IMcObjectSchemeNode[] nodes = scheme.GetNodes(new CMcEnumBitField<>(McObjectSchemeNode.ENodeKindFlags.ENKF_ANY_ITEM));
                                   if(nodes!= null) {
                                       objectsList.add(mcObject);
                                       List<IMcObjectSchemeNode> nodesList = new ArrayList<>(Arrays.asList(nodes));
                                       objectsSchemeNodeList.put(mcObject, nodesList);
                                   }
                                }
                            }
                        }
                    }
                    Message msg = mHandler.obtainMessage(GET_OBJECT_LIST_COMPLETED);
                    msg.sendToTarget();

                } catch (MapCoreException e) {
                    Message msg = mHandler.obtainMessage(GET_OBJECT_LIST_FAILED,e);
                    msg.sendToTarget();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }
        @Override
        public void onViewCreated (View view, @Nullable Bundle savedInstanceState){
            super.onViewCreated(view, savedInstanceState);
            //TODO move to glthread,seng message when complete then continue
            GetObjectsListFromRenderThread();
        }

    @Override
    public void setObject(Object obj) {
        m_selectedScanObject = (IMcObject) obj;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(m_selectedScanObject));
    }
}