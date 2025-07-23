package com.elbit.mapcore.mcandroidtester.utils.customviews;

import android.app.Activity;
import android.content.Context;
import android.os.Bundle;
import android.os.Parcelable;
import androidx.annotation.Nullable;
import android.text.InputType;
import android.text.TextUtils;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.Spinner;
import android.widget.Toast;

import androidx.fragment.app.Fragment;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.OnCreateCoordinateSystemListener;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_MCGridCoordinateSystem;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.GridCoordinateSysFragment;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

import com.elbit.mapcore.Classes.Calculations.GridCoordSystem.McGridMGRS;
import com.elbit.mapcore.Classes.Calculations.GridCoordSystem.McGridNZMG;
import com.elbit.mapcore.Classes.Calculations.GridCoordSystem.McGridNewIsrael;
import com.elbit.mapcore.Classes.Calculations.GridCoordSystem.McGridRSOSingapore;
import com.elbit.mapcore.Classes.Calculations.GridCoordSystem.McGridRT90;
import com.elbit.mapcore.Classes.Calculations.GridCoordSystem.McGridS42;
import com.elbit.mapcore.Classes.Calculations.GridCoordSystem.McGridTMUserDefined;
import com.elbit.mapcore.Classes.Calculations.GridCoordSystem.McGridUTM;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Calculations.GridCoordSystem.IMcGridCoordSystemGeocentric;
import com.elbit.mapcore.Interfaces.Calculations.GridCoordSystem.IMcGridCoordSystemGeographic;
import com.elbit.mapcore.Interfaces.Calculations.GridCoordSystem.IMcGridCoordSystemTraverseMercator;
import com.elbit.mapcore.Interfaces.Calculations.GridCoordSystem.IMcGridCoordinateSystem;

/**
 * Created by tc97803 on 05/01/2017.
 */
public class GridCoordinateSystemDetails extends LinearLayout {

    public static final String COORDINATE_SYSTEM = "COORDINATE_SYSTEM";
    public static final String DATUM = "DATUM";

    View mView;
    Fragment mFragment;
    public IMcGridCoordinateSystem mNewGridCoordinateSystem;
    private Spinner mCoordSysSpinner;
    private Spinner mDatumSpinner;
    private EditText mFET, mSET, mDxET, mDyET, mRxET, mRyET, mRzET, mAET, mDzET;
    private LinearLayout mRt90Layout;
    private LinearLayout mUserDefinedLayout;
    private EditText mCentralMeridianET;
    private EditText mFalseEastingET;
    private EditText mFalseNorthingET;
    private EditText mLatOfGridET;
    private EditText mScaleFactorET;
    private EditText mZoneET;
    private EditText mZoneWidthET;
    private SpinnerWithLabel mDatumSWL;
    private LinearLayout mXyzLayout;
    private int mCoordSysSpinnerValue = -1;
    private int mDatumSpinnerValue = -1;

    public GridCoordinateSystemDetails(Context context) {
        super(context);
        InflateLayout(context);
    }

    public GridCoordinateSystemDetails(Context context, AttributeSet attrs) {
        super(context, attrs);
        InflateLayout(context);
    }

    @Nullable
    @Override
    protected Parcelable onSaveInstanceState() {
        Bundle bundle = new Bundle();
        bundle.putParcelable("superState2", super.onSaveInstanceState());

        if(mFragment != null && mFragment.getClass() == GridCoordinateSysFragment.class
                && ((GridCoordinateSysFragment)mFragment).getRadioButtonOptions() == GridCoordinateSysFragment.RadioButtonOptions.CreateNew) {
            bundle.putSerializable(COORDINATE_SYSTEM, new AMCTSerializableObject( mCoordSysSpinner.getSelectedItem()));
            bundle.putSerializable(DATUM, new AMCTSerializableObject(mDatumSpinner.getSelectedItem()));
        }
        return bundle;
    }

    @Override
    protected void onRestoreInstanceState(Parcelable state) {
        if (state instanceof Bundle) // implicit null check
        {
            Bundle bundle = (Bundle) state;
            AMCTSerializableObject mcObject = (AMCTSerializableObject) bundle.getSerializable(COORDINATE_SYSTEM);
            if (mcObject != null && mcObject.getMcObject() != null)
                mCoordSysSpinnerValue = ((IMcGridCoordinateSystem.EGridCoordSystemType) mcObject.getMcObject()).getValue();

            AMCTSerializableObject mcObjectDatum = (AMCTSerializableObject) bundle.getSerializable(DATUM);
            if (mcObjectDatum != null && mcObjectDatum.getMcObject() != null)
                mDatumSpinnerValue = ((IMcGridCoordinateSystem.EDatumType) mcObjectDatum.getMcObject()).getValue();

            state = bundle.getParcelable("superState2");
        }
        super.onRestoreInstanceState(state);
    }

    private void InflateLayout(Context context) {
        LayoutInflater inflater = (LayoutInflater) context
                .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        mView = inflater.inflate(R.layout.cv_grid_coordinate_system_details, this);
        mZoneET = (EditText) mView.findViewById(R.id.zone);
        if(mCoordSysSpinnerValue == -1)
            mCoordSysSpinnerValue = IMcGridCoordinateSystem.EGridCoordSystemType.EGCS_GEOGRAPHIC.getValue();
        initComponents();
    }

    public void SetFragment(Fragment fragment)
    {
        mFragment = fragment;
    }

    private void setDatumParamsETs(IMcGridCoordinateSystem.SDatumParams datumParams) {
        mAET.setText(String.valueOf(datumParams.dA));
        mDxET.setText(String.valueOf(datumParams.dDX));
        mDyET.setText(String.valueOf(datumParams.dDY));
        mDzET.setText(String.valueOf(datumParams.dDZ));
        mFET.setText(String.valueOf(datumParams.dF));
        mRxET.setText(String.valueOf(datumParams.dRx));
        mRyET.setText(String.valueOf(datumParams.dRy));
        mRzET.setText(String.valueOf(datumParams.dRz));
        mSET.setText(String.valueOf(datumParams.dS));
    }

    public void initComponents() {
        initBttns();
        initSpinners();
        initStubViews();
    }

    private void initStubViews() {
        initXyzLayout();
        initUserDefinedLayout();
        initRt90Layout();
    }

    private void initRt90Layout() {
        mRt90Layout = (LinearLayout) mView.findViewById(R.id.coord_sys_rt90_vs);
    }

    private void initUserDefinedLayout() {
        mUserDefinedLayout = (LinearLayout) mView.findViewById(R.id.coord_sys_tm_user_defined_vs);
        mCentralMeridianET = (EditText) mUserDefinedLayout.findViewById(R.id.central_meridian);
        mFalseEastingET = (EditText) mUserDefinedLayout.findViewById(R.id.false_easting);
        mFalseNorthingET = (EditText) mUserDefinedLayout.findViewById(R.id.false_northing);
        mLatOfGridET = (EditText) mUserDefinedLayout.findViewById(R.id.latitude_of_grid_origin);
        mScaleFactorET = (EditText) mUserDefinedLayout.findViewById(R.id.scale_factor);
        mZoneWidthET = (EditText) mUserDefinedLayout.findViewById(R.id.zone_width);
    }

    private void initXyzLayout() {
        mXyzLayout = (LinearLayout) mView.findViewById(R.id.coord_sys_xyz_vs);
        mFET = (EditText) mXyzLayout.findViewById(R.id.F);
        mAET = (EditText) mXyzLayout.findViewById(R.id.A);
        mSET = (EditText) mXyzLayout.findViewById(R.id.S);
        mDxET = (EditText) mXyzLayout.findViewById(R.id.DX);
        mDyET = (EditText) mXyzLayout.findViewById(R.id.DY);
        mDzET = (EditText) mXyzLayout.findViewById(R.id.DZ);
        mRxET = (EditText) mXyzLayout.findViewById(R.id.Rx);
        mRzET = (EditText) mXyzLayout.findViewById(R.id.Rz);
        mRyET = (EditText) mXyzLayout.findViewById(R.id.Ry);
    }

    public void initSpinners() {
        setCoordSysSpinner();
        setDatumSpinner();
    }

    private void setDatumSpinner() {
        mDatumSWL = (SpinnerWithLabel) mView.findViewById(R.id.datum_swl);
        mDatumSpinner = (Spinner) mDatumSWL.findViewById(R.id.spinner_in_cv);
        mDatumSpinner.setAdapter(new ArrayAdapter<>(mView.getContext(), android.R.layout.simple_spinner_item, IMcGridCoordinateSystem.EDatumType.values()));
        if(mDatumSpinnerValue !=-1)
            mDatumSpinner.setSelection(mDatumSpinnerValue);
    }

    private void setCoordSysSpinner() {
        SpinnerWithLabel SWL = (SpinnerWithLabel) mView.findViewById(R.id.coordinate_system_swl);
        mCoordSysSpinner = (Spinner) SWL.findViewById(R.id.spinner_in_cv);
        mCoordSysSpinner.setAdapter(new ArrayAdapter<>(mView.getContext(), android.R.layout.simple_spinner_item, IMcGridCoordinateSystem.EGridCoordSystemType.values()));
        mCoordSysSpinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                //mCoordSysSpinnerValue = position;
                mDatumSpinnerValue = 0;
                IMcGridCoordinateSystem.EGridCoordSystemType gridCoordSys = IMcGridCoordinateSystem.EGridCoordSystemType.values()[position];
                ShowSelectedCoordinateSystemGroupBox(gridCoordSys);
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });

        mCoordSysSpinner.setSelection(mCoordSysSpinnerValue);
    }

    private void ShowSelectedCoordinateSystemGroupBox(IMcGridCoordinateSystem.EGridCoordSystemType coordSysType) {
        mRt90Layout.setVisibility(View.GONE);
        mUserDefinedLayout.setVisibility(View.GONE);
        mXyzLayout.setVisibility(View.GONE);
        mDatumSWL.setVisibility(View.GONE);
        mZoneET.setVisibility(View.GONE);

        switch (coordSysType) {
            case EGCS_GEOCENTRIC:
                mDatumSWL.setVisibility(View.VISIBLE);
                mXyzLayout.setVisibility(View.VISIBLE);
                break;
            case EGCS_GEOGRAPHIC:
                mDatumSWL.setVisibility(View.VISIBLE);
                mXyzLayout.setVisibility(View.VISIBLE);
                break;
            case EGCS_INDIA_LCC:
                break;
            case EGCS_KKJ:
                break;
            case EGCS_NEW_ISRAEL:
                mDatumSWL.setVisibility(View.VISIBLE);
                mZoneET.setVisibility(View.VISIBLE);
                break;
            case EGCS_RSO_SINGAPORE:
                mDatumSWL.setVisibility(View.VISIBLE);
                break;
            case EGCS_RT90:
                mRt90Layout.setVisibility(View.VISIBLE);
                mZoneET.setVisibility(View.VISIBLE);
                break;
            case EGCS_S42:
                mDatumSWL.setVisibility(View.VISIBLE);
                mZoneET.setVisibility(View.VISIBLE);
                mXyzLayout.setVisibility(View.VISIBLE);
                break;
            case EGCS_TM_USER_DEFINED:
                mDatumSWL.setVisibility(View.VISIBLE);
                mZoneET.setVisibility(View.VISIBLE);
                mXyzLayout.setVisibility(View.VISIBLE);
                mUserDefinedLayout.setVisibility(View.VISIBLE);
                break;
            case EGCS_UTM:
                mDatumSWL.setVisibility(View.VISIBLE);
                mZoneET.setVisibility(View.VISIBLE);
                mXyzLayout.setVisibility(View.VISIBLE);
                break;
        }
    }

    private void createNewGridCoordinateSys(final IMcGridCoordinateSystem.EGridCoordSystemType selectedCoordSys) {
        IMcGridCoordinateSystem.EDatumType datum = null;
        if (mDatumSpinner != null)
            datum = ((IMcGridCoordinateSystem.EDatumType) mDatumSpinner.getSelectedItem());
        final IMcGridCoordinateSystem.SDatumParams datumParams = getSelectedDatumParams();
        final IMcGridCoordinateSystem.EDatumType finalDatum = datum;
        setDefVal(mZoneET);
        final String zone = mZoneET.getText().toString();
        //final int numZone = Integer.valueOf(zone);
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {

                switch (selectedCoordSys) {
                    case EGCS_GEOGRAPHIC:
                        try {
                            if (datumParams != null)
                                mNewGridCoordinateSystem = IMcGridCoordSystemGeographic.Static.Create(finalDatum, datumParams);
                        } catch (MapCoreException McEx) {
                            AlertMessages.ShowMapCoreErrorMessage(mView.getContext(), McEx, "McGridCoordSystemGeographic.Creat");
                        }catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EGCS_GEOCENTRIC:
                        try {
                            mNewGridCoordinateSystem = IMcGridCoordSystemGeocentric.Static.Create(finalDatum, datumParams);
                        } catch (MapCoreException McEx) {
                            AlertMessages.ShowMapCoreErrorMessage(mView.getContext(), McEx, "McGridCoordSystemGeocentric.Create");
                            McEx.printStackTrace();
                        }
                        break;
                    case EGCS_TM_USER_DEFINED:
                        IMcGridCoordSystemTraverseMercator.STMGridParams gridParams = new IMcGridCoordSystemTraverseMercator.STMGridParams();
                        getSelectedGridParams(gridParams);
                        try {

                            mNewGridCoordinateSystem = McGridTMUserDefined.Static.Create(gridParams, Integer.valueOf(zone), finalDatum, datumParams);
                        } catch (MapCoreException McEx) {
                            AlertMessages.ShowMapCoreErrorMessage(mView.getContext(), McEx, "McGridCoordSystemGeographic.Creat");
                            McEx.printStackTrace();
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EGCS_UTM:
                        try {
                            setDefVal(mZoneET);
                            mNewGridCoordinateSystem = McGridUTM.Static.Create(Integer.valueOf(zone), finalDatum, datumParams);
                        } catch (MapCoreException McEx) {
                            AlertMessages.ShowMapCoreErrorMessage(mView.getContext(), McEx, "McGridUTM.Create");
                            McEx.printStackTrace();
                        }
                        break;
                    case EGCS_NEW_ISRAEL:
                        try {
                            mNewGridCoordinateSystem = McGridNewIsrael.Static.Create();
                        } catch (MapCoreException McEx) {
                            AlertMessages.ShowMapCoreErrorMessage(mView.getContext(), McEx, "McGridNewIsrael.Create");
                            McEx.printStackTrace();
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EGCS_RSO_SINGAPORE:
                        try {
                            mNewGridCoordinateSystem = McGridRSOSingapore.Static.Create();
                        } catch (MapCoreException McEx) {
                            AlertMessages.ShowMapCoreErrorMessage(mView.getContext(), McEx, "McGridRSOSingapore.Create");
                            McEx.printStackTrace();
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EGCS_NZMG:
                        try {
                            mNewGridCoordinateSystem = McGridNZMG.Static.Create();
                        } catch (MapCoreException McEx) {
                            AlertMessages.ShowMapCoreErrorMessage(mView.getContext(), McEx, "McGridNZMG.Create.Create");
                            McEx.printStackTrace();
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EGCS_S42:
                        try {
                            setDefVal(mZoneET);
                            mNewGridCoordinateSystem = McGridS42.Static.Create(Integer.valueOf(zone), finalDatum, datumParams);
                        } catch (MapCoreException McEx) {
                            AlertMessages.ShowMapCoreErrorMessage(mView.getContext(), McEx, "McGridS42.Create");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EGCS_RT90:
                        try {
                            mNewGridCoordinateSystem = McGridRT90.Static.Create();
                        } catch (MapCoreException McEx) {
                            AlertMessages.ShowMapCoreErrorMessage(mView.getContext(), McEx, "McGridRT90.Create");
                            McEx.printStackTrace();
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                    case EGCS_MGRS:
                        try {
                            mNewGridCoordinateSystem = McGridMGRS.Static.Create();
                        } catch (MapCoreException McEx) {
                            AlertMessages.ShowMapCoreErrorMessage(mView.getContext(), McEx, "McGridMGRS.Create");
                            McEx.printStackTrace();
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        break;
                }
                ((Activity) mView.getContext()).runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        finishCoordSysCreation();
                    }
                });
            }
        });
    }

    private void finishCoordSysCreation() {
        if (mNewGridCoordinateSystem != null) {
            Toast.makeText(mView.getContext(), "coordinate system was created successfully", Toast.LENGTH_LONG).show();
            Manager_MCGridCoordinateSystem.getInstance().AddNewGridCoordinateSystem(mNewGridCoordinateSystem);
            if(mFragment != null)
            {
                if(mFragment instanceof OnCreateCoordinateSystemListener)
                   ((OnCreateCoordinateSystemListener) mFragment).onCoordSysCreated(mNewGridCoordinateSystem);
            }
        }
    }

    private void getSelectedGridParams(IMcGridCoordSystemTraverseMercator.STMGridParams gridParams) {
        setGridParamsDefVals();
        gridParams.dCentralMeridian = Double.valueOf(mCentralMeridianET.getText().toString());
        gridParams.dFalseEasting = Double.valueOf(mFalseEastingET.getText().toString());
        gridParams.dFalseNorthing = Double.valueOf(mFalseNorthingET.getText().toString());
        gridParams.dLatitudeOfGridOrigin = Double.valueOf(mLatOfGridET.getText().toString());
        gridParams.dScaleFactor = Double.valueOf(mScaleFactorET.getText().toString());
        gridParams.dZoneWidth = Double.valueOf(mZoneWidthET.getText().toString());
    }

    private void setGridParamsDefVals() {
        setDefVal(mCentralMeridianET);
        setDefVal(mFalseEastingET);
        setDefVal(mFalseNorthingET);
        setDefVal(mLatOfGridET);
        setDefVal(mScaleFactorET);
        setDefVal(mZoneWidthET);
    }

    private void setDefVal(EditText editText) {
        if (TextUtils.isEmpty(editText.getText()))
            if (editText.getInputType() == (InputType.TYPE_NUMBER_FLAG_DECIMAL | InputType.TYPE_CLASS_NUMBER))
                editText.setText("0.0");
            else if (editText.getInputType() == InputType.TYPE_CLASS_NUMBER)
                editText.setText("0");
    }

    private IMcGridCoordinateSystem.SDatumParams getSelectedDatumParams() {
        setDatumParamsDefVals();
        return new IMcGridCoordinateSystem.SDatumParams(Double.valueOf(mAET.getText().toString()),
                Double.valueOf(mFET.getText().toString()),
                Double.valueOf(mDxET.getText().toString()),
                Double.valueOf(mDyET.getText().toString()),
                Double.valueOf(mDzET.getText().toString()),
                Double.valueOf(mRxET.getText().toString()),
                Double.valueOf(mRyET.getText().toString()),
                Double.valueOf(mRzET.getText().toString()),
                Double.valueOf(mSET.getText().toString()));

    }

    private void setDatumParamsDefVals() {
        setDefVal(mAET);
        setDefVal(mFET);
        setDefVal(mDxET);
        setDefVal(mDyET);
        setDefVal(mDzET);
        setDefVal(mRxET);
        setDefVal(mRyET);
        setDefVal(mRzET);
        setDefVal(mSET);
    }

    private void initBttns() {
        mView.findViewById(R.id.create_coord_sys_bttn).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                IMcGridCoordinateSystem.EGridCoordSystemType gridCoordSys = ((IMcGridCoordinateSystem.EGridCoordSystemType) mCoordSysSpinner.getSelectedItem());
                createNewGridCoordinateSys(gridCoordSys);
            }
        });
    }

    public void ShowCurrGridCoordSysParams(IMcGridCoordinateSystem m_ExistCoordSys) {
        mView.findViewById(R.id.create_coord_sys_bttn).setVisibility(View.GONE);
        IMcGridCoordinateSystem.EGridCoordSystemType type = m_ExistCoordSys.GetGridCoorSysType();
        IMcGridCoordinateSystem.SDatumParams datumParams = null;
        try {
            mCoordSysSpinner.setSelection(m_ExistCoordSys.GetGridCoorSysType().getValue());
            datumParams = m_ExistCoordSys.GetDatumParams();
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(mView.getContext(),e,"GetDatumParams");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
        IMcGridCoordinateSystem.EDatumType datumType = m_ExistCoordSys.GetDatum();
        switch (type) {
            case EGCS_GEOCENTRIC:
                mDatumSpinner.setSelection(datumType.getValue());
                setDatumParamsETs(datumParams);
                break;
            case EGCS_GEOGRAPHIC:
                mDatumSpinner.setSelection(datumType.getValue());
                setDatumParamsETs(datumParams);
                break;
            case EGCS_INDIA_LCC:
                break;
            case EGCS_KKJ:
                break;
            case EGCS_NEW_ISRAEL:
                mDatumSpinner.setSelection(datumType.getValue());
                mZoneET.setText(String.valueOf(m_ExistCoordSys.GetZone()));
                break;
            case EGCS_RSO_SINGAPORE:
                mDatumSpinner.setSelection(datumType.getValue());
                break;
            case EGCS_RT90:
                mDatumSpinner.setSelection(datumType.getValue());
                mZoneET.setText(String.valueOf(m_ExistCoordSys.GetZone()));
                break;
            case EGCS_S42:
                mDatumSpinner.setSelection(datumType.getValue());
                setDatumParamsETs(datumParams);
                break;
            case EGCS_TM_USER_DEFINED:
            case EGCS_UTM:
                mDatumSpinner.setSelection(datumType.getValue());
                mZoneET.setText(String.valueOf(m_ExistCoordSys.GetZone()));
                setDatumParamsETs(datumParams);
                break;
            case EGCS_MGRS:
                break;
            case EGCS_NZMG:
                break;
        }
    }

    public void SetEnabled(boolean isEnabled) {
        SetEnabled(this, isEnabled);
    }

    private void SetEnabled(ViewGroup view, boolean isEnabled) {
        for (int i = 0; i < view.getChildCount(); i++) {
            View v = view.getChildAt(i);
             if (v instanceof ViewGroup) {
                this.SetEnabled((ViewGroup) v, isEnabled);
            }
            v.setEnabled(isEnabled);
        }
    }
}
