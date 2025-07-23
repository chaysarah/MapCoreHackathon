package com.elbit.mapcore.mcandroidtester.ui.map_actions;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentTransaction;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.RadioGroup;
import android.widget.TextView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.MapsContainerActivity;
import com.elbit.mapcore.mcandroidtester.ui.objects.ScanBoxGeometry;
import com.elbit.mapcore.mcandroidtester.ui.objects.ScanPointGeometry;
import com.elbit.mapcore.mcandroidtester.ui.objects.ScanPolygonGeometry;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SpinnerWithLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.ThreeDVector;

import java.util.Observable;

import com.elbit.mapcore.Classes.Calculations.SMcScanPolygonGeometry;
import com.elbit.mapcore.Enums.EMcPointCoordSystem;
import com.elbit.mapcore.Interfaces.Calculations.IMcSpatialQueries;
import com.elbit.mapcore.Interfaces.General.IMcEditMode;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObject;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeItem;
import com.elbit.mapcore.Structs.SMcVector3D;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link ScanFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link ScanFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class ScanFragment extends Fragment implements FragmentWithObject, IMcEditMode.ICallback/*, Observer*/ {
    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;

    private OnFragmentInteractionListener mListener;
    private View mRootView;
    private RadioGroup mScanTypeRG;
    private Button mScanTypeSaveBttn;
    private SpinnerWithLabel mCoordSysSWL;
    private Button mSQParamsBttn;
    private IMcSpatialQueries.SQueryParams mQueryParams;
    private CheckBox mScanCompletelyInsideOnlyCB;
    private NumericEditTextLabel mToleranceNETL;
    private EMcPointCoordSystem mCoordSys;
    private SMcScanPolygonGeometry mScanPolygonGeometry;
    private ThreeDVector mManualPoint;
    private TextView mSQParamsTV;
    
    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment ScanFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static ScanFragment newInstance() {
        ScanFragment fragment = new ScanFragment();
        return fragment;
    }

    public ScanFragment() {
        // Required empty public constructor
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
        setRetainInstance(true);
        mRootView = inflater.inflate(R.layout.fragment_scan, container, false);
        Funcs.SetObjectFromBundle(savedInstanceState, this );
        setTitle();
        inflateViews();
        initViews();

        return mRootView;
    }

    private void initViews() {
        mManualPoint.setVector3D(new SMcVector3D());
        mToleranceNETL.setFloat(0);
        initSQParamsBttn();
        initCoordSysSWL();
        initSaveBttn();
    }

    private void initSQParamsBttn() {
        mSQParamsBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                goToSQParamsFragment();
                mSQParamsTV.setText("Defined");
            }
        });
    }

    private void goToSQParamsFragment() {
        QueryParamsFragment queryParamsFragment = QueryParamsFragment.newInstance();
        FragmentTransaction transaction = getFragmentManager().beginTransaction().hide(this).add(R.id.fragment_container, queryParamsFragment, queryParamsFragment.getClass().getSimpleName());
        transaction.addToBackStack(queryParamsFragment.getClass().getSimpleName());
        transaction.commit();
    }

    private void initCoordSysSWL() {
        mCoordSysSWL.setAdapter(new ArrayAdapter<>(getContext(), android.R.layout.simple_spinner_item, EMcPointCoordSystem.values()));
        mCoordSysSWL.setSelection(EMcPointCoordSystem.EPCS_SCREEN.ordinal());
    }

    private void initSaveBttn() {
        mScanTypeSaveBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                mCoordSys = (EMcPointCoordSystem) mCoordSysSWL.getSelectedItem();

                int scanType = mScanTypeRG.getCheckedRadioButtonId();
                if(scanType == R.id.scan_type_point_scan_rb) {
                    ScanPointGeometry pointScan = new ScanPointGeometry(ScanFragment.this, mCoordSys, mQueryParams, mScanCompletelyInsideOnlyCB.isChecked(), mToleranceNETL.getFloat(), new SMcVector3D());
                    pointScan.startPointScan();
                }
                else if (scanType == R.id.scan_type_manual_point_scan_rb) {
                    ScanPointGeometry manualPointScan = new ScanPointGeometry(ScanFragment.this, mCoordSys, mQueryParams, mScanCompletelyInsideOnlyCB.isChecked(), mToleranceNETL.getFloat(), mManualPoint.getVector3D());
                    manualPointScan.startManualPointScan();
                }
                else if (scanType == R.id.scan_type_poly_scan_rb) {
                    ScanPolygonGeometry polyScan = new ScanPolygonGeometry(ScanFragment.this, mCoordSys, mQueryParams, mScanCompletelyInsideOnlyCB.isChecked(), mToleranceNETL.getFloat());
                    polyScan.startPolyScan();
                }
                else if (scanType == R.id.scan_type_rect_scan_rb) {
                    ScanBoxGeometry rectScan = new ScanBoxGeometry(ScanFragment.this, mCoordSys, mQueryParams, mScanCompletelyInsideOnlyCB.isChecked(), mToleranceNETL.getFloat());
                    rectScan.startRectScan();
                }
            }
        });
    }
    @Override
    public void onHiddenChanged(boolean _hidden) {
        super.onHiddenChanged(_hidden);
    }

    private void inflateViews() {
        mSQParamsBttn = (Button) mRootView.findViewById(R.id.scan_sqparams_bttn);
        mCoordSysSWL = (SpinnerWithLabel) mRootView.findViewById(R.id.scan_coord_sys_swl);
        mScanTypeSaveBttn = (Button) mRootView.findViewById(R.id.scan_save_bttn);
        mScanTypeRG = (RadioGroup) mRootView.findViewById(R.id.scan_type_rg);
        mScanCompletelyInsideOnlyCB = (CheckBox) mRootView.findViewById(R.id.scan_completely_inside_only_cb);
        mToleranceNETL = (NumericEditTextLabel) mRootView.findViewById(R.id.scan_tolerance);
        mManualPoint = (ThreeDVector) mRootView.findViewById(R.id.scan_manual_point_scan_3d_vector);
        mSQParamsTV=(TextView)mRootView.findViewById(R.id.scan_sqparams_tv);
    }

    private void setTitle() {
        getActivity().setTitle("scan");
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
        }/* else {
            throw new RuntimeException(context.toString()
                    + " must implement OnFragmentInteractionListener");
        }*/
        if (context instanceof MapsContainerActivity) {
            ((MapsContainerActivity) context).mCurFragmentTag = ScanFragment.class.getSimpleName();
        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    @Override
    public void setObject(Object obj) {
        if (obj instanceof IMcSpatialQueries.SQueryParams)
            mQueryParams = (IMcSpatialQueries.SQueryParams) obj;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mQueryParams));
    }

    @Override
    public void ExitAction(int i) {

    }

    @Override
    public void NewVertex(IMcObject iMcObject, IMcObjectSchemeItem iMcObjectSchemeItem, SMcVector3D sMcVector3D, SMcVector3D sMcVector3D1, int i, double v) {

    }

    @Override
    public void PointDeleted(IMcObject iMcObject, IMcObjectSchemeItem iMcObjectSchemeItem, SMcVector3D sMcVector3D, SMcVector3D sMcVector3D1, int i) {

    }

    @Override
    public void PointNewPos(IMcObject iMcObject, IMcObjectSchemeItem iMcObjectSchemeItem, SMcVector3D sMcVector3D, SMcVector3D sMcVector3D1, int i, double v, boolean b) {

    }

    @Override
    public void ActiveIconChanged(IMcObject iMcObject, IMcObjectSchemeItem iMcObjectSchemeItem, IMcEditMode.EPermission ePermission, int i) {

    }

    @Override
    public void InitItemResults(IMcObject iMcObject, IMcObjectSchemeItem iMcObjectSchemeItem, int i) {

    }

    @Override
    public void EditItemResults(IMcObject iMcObject, IMcObjectSchemeItem iMcObjectSchemeItem, int i) {

    }

    @Override
    public void DragMapResults(IMcMapViewport iMcMapViewport, SMcVector3D sMcVector3D) {

    }

    @Override
    public void RotateMapResults(IMcMapViewport iMcMapViewport, float v, float v1) {

    }

    @Override
    public void DistanceDirectionMeasureResults(IMcMapViewport iMcMapViewport, SMcVector3D sMcVector3D, SMcVector3D sMcVector3D1, double v, double v1) {

    }

    @Override
    public void DynamicZoomResults(IMcMapViewport iMcMapViewport, float v, SMcVector3D sMcVector3D) {

    }

    @Override
    public void CalculateHeightResults(IMcMapViewport iMcMapViewport, double v, SMcVector3D[] sMcVector3Ds, int i) {

    }

    @Override
    public void CalculateVolumeResults(IMcMapViewport iMcMapViewport, double v, SMcVector3D[] sMcVector3Ds, int i) {

    }

    /**
     * This method is called if the specified {@code Observable} object's
     * {@code notifyObservers} method is called (because the {@code Observable}
     * object has been updated.
     *
     * @param observable the {@link Observable} object.
     * @param data       the data passed to {@link Observable#notifyObservers(Object)}.
     
    @Override
    public void update(Observable observable, Object data) {
        if (observable instanceof DrawPolygonSchemeForScanResult) {
            mObjSchemeItem = (((DrawPolygonSchemeForScanResult) data).getObjScheme());
            mObject = (((DrawPolygonSchemeForScanResult) data).getObj());
        }
    }
*/
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
