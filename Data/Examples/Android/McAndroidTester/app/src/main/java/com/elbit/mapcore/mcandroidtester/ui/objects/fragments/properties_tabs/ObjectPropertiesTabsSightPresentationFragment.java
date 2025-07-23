package com.elbit.mapcore.mcandroidtester.ui.objects.fragments.properties_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.ui.objects.ObjectPropertiesBase;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SelectColor;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SpinnerWithLabel;

import java.util.HashMap;

import com.elbit.mapcore.Interfaces.Calculations.IMcSpatialQueries;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcSightPresentationItemParams;
import com.elbit.mapcore.Structs.SMcBColor;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link ObjectPropertiesTabsSightPresentationFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link ObjectPropertiesTabsSightPresentationFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class ObjectPropertiesTabsSightPresentationFragment extends Fragment {
    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;

    private OnFragmentInteractionListener mListener;
    private View mRootView;
    private SpinnerWithLabel mSightPresentationType;
    private EditText mSightObserverHeight;
    private CheckBox mIsObserverHeightAbsolute;
    private EditText mObserverMinPitch;
    private EditText mObserverMaxPitch;
    private SpinnerWithLabel mSightQueryPrecision;
    private SpinnerWithLabel mSightColorSWL;
    private SelectColor mSightColorSC;
    private EditText mSightNumEllipseRays;
    private Button mSaveBttn;
    private EditText mSightObservedHeight;
    private CheckBox mIsObservedHeightAbsolute;
    private EditText mSightObservedHeightAbsolute;
    private EditText mSightObserverHeightAbsolute;
    private HashMap<IMcSpatialQueries.EPointVisibility,SMcBColor> mColorsByVisibility;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment ObjectPropertiesTabsSightPresentationFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static ObjectPropertiesTabsSightPresentationFragment newInstance() {
        ObjectPropertiesTabsSightPresentationFragment fragment = new ObjectPropertiesTabsSightPresentationFragment();
        return fragment;
    }

    public ObjectPropertiesTabsSightPresentationFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        mRootView = inflater.inflate(R.layout.fragment_object_properties_tabs_sight_presentation, container, false);
        initViews();
        return mRootView;
    }

    private void initViews() {
        inflateViews();
        initSaveBttn();
        initSightPresentationType();
        initSightObserverHeight();
        initSightObservedHeight();
        initIsObservedHeightAbsolute();
        initIsObserverHeightAbsolute();
        initObserverMinPitch();
        initObserverMaxPitch();
        initSightQueryPrecision();
        initSightColor();
        initSightNumEllipseRays();
    }

    private void initSightNumEllipseRays() {
        mSightNumEllipseRays.setText(String.valueOf(ObjectPropertiesBase.SightPresentation.numEllipseRays));
    }

    private void initSightColor() {
        mColorsByVisibility=ObjectPropertiesBase.SightPresentation.colorsByVisibility;
        // init default colors
        if(mColorsByVisibility.size() == 0)
        {
            mColorsByVisibility.put(IMcSpatialQueries.EPointVisibility.EPV_SEEN, new SMcBColor(0,255,0,255));
            mColorsByVisibility.put(IMcSpatialQueries.EPointVisibility.EPV_UNSEEN, new SMcBColor(255,0,0,255));
            mColorsByVisibility.put(IMcSpatialQueries.EPointVisibility.EPV_UNKNOWN, new SMcBColor(128,128,128,0));
            mColorsByVisibility.put(IMcSpatialQueries.EPointVisibility.EPV_OUT_OF_QUERY_AREA, new SMcBColor(128,128,128,0));
            mColorsByVisibility.put(IMcSpatialQueries.EPointVisibility.EPV_SEEN_STATIC_OBJECT, new SMcBColor(0,0,0,0));
            mColorsByVisibility.put(IMcSpatialQueries.EPointVisibility.EPV_ASYNC_CALCULATING, new SMcBColor(0,0,0,0));
        }
        mSightColorSWL.setAdapter(new ArrayAdapter<>(getContext(), android.R.layout.simple_spinner_item, IMcSpatialQueries.EPointVisibility.values()));
        mSightColorSWL.setSelection(ObjectPropertiesBase.SightPresentation.colorPointVisibility.ordinal());
        mSightColorSWL.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                SMcBColor colorByVisibility = mColorsByVisibility.get(mSightColorSWL.getAdapter().getItem(position));
                if (colorByVisibility != null)
                    mSightColorSC.setmBColor(colorByVisibility);
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) { }
        });
        mSightColorSC.enableButtons(true);
        mSightColorSC.setmBColor(mColorsByVisibility.get(IMcSpatialQueries.EPointVisibility.EPV_SEEN));
        mSightColorSC.setOnColorSelectedAction(new Runnable() {
            @Override
            public void run() {
                mColorsByVisibility.put((IMcSpatialQueries.EPointVisibility) mSightColorSWL.getSelectedItem(),mSightColorSC.getmBColor());
            }
        });
    }

    private void initSightQueryPrecision() {
        mSightQueryPrecision.setAdapter(new ArrayAdapter<>(getContext(), android.R.layout.simple_spinner_item, IMcSpatialQueries.EQueryPrecision.values()));
        mSightQueryPrecision.setSelection(ObjectPropertiesBase.SightPresentation.precision.ordinal());
    }

    private void initObserverMaxPitch() {
        mObserverMaxPitch.setText(String.valueOf(ObjectPropertiesBase.SightPresentation.maxPitch));

    }

    private void initObserverMinPitch() {
        mObserverMinPitch.setText(String.valueOf(String.valueOf(ObjectPropertiesBase.SightPresentation.minPitch)));
    }

    private void initIsObserverHeightAbsolute() {
        mIsObserverHeightAbsolute.setChecked(ObjectPropertiesBase.SightPresentation.isObserverHeightAbsolute);
    }

    private void initIsObservedHeightAbsolute() {
        mIsObservedHeightAbsolute.setChecked(ObjectPropertiesBase.SightPresentation.isObservedHeightAbsolute);
    }
    private void initSightObserverHeight() {
        float height=ObjectPropertiesBase.SightPresentation.observerHeight;
        mSightObserverHeight.setText(String.valueOf(height));
    }
    private void initSightObservedHeight() {
        float height=ObjectPropertiesBase.SightPresentation.observedHeight;
        mSightObservedHeight.setText(String.valueOf(height));
    }

    private void initSightPresentationType() {
        mSightPresentationType.setAdapter(new ArrayAdapter<>(getContext(), android.R.layout.simple_spinner_item, IMcSightPresentationItemParams.ESightPresentationType.values()));
        mSightPresentationType.setSelection(ObjectPropertiesBase.SightPresentation.type.ordinal());
    }

    private void initSaveBttn() {
        mSaveBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveSightPresentationParams();
            }
        });
    }


    private void saveSightPresentationParams() {
        ObjectPropertiesBase.SightPresentation.colorPointVisibility= (IMcSpatialQueries.EPointVisibility) mSightColorSWL.getSelectedItem();
        ObjectPropertiesBase.SightPresentation.color=mSightColorSC.getmBColor();
        ObjectPropertiesBase.SightPresentation.colorsByVisibility=mColorsByVisibility;
        ObjectPropertiesBase.SightPresentation.precision= (IMcSpatialQueries.EQueryPrecision) mSightQueryPrecision.getSelectedItem();
        ObjectPropertiesBase.SightPresentation.maxPitch=Integer.valueOf(String.valueOf(mObserverMaxPitch.getText()));
        ObjectPropertiesBase.SightPresentation.minPitch=Integer.valueOf(String.valueOf(mObserverMinPitch.getText()));
        ObjectPropertiesBase.SightPresentation.isObserverHeightAbsolute= mIsObserverHeightAbsolute.isChecked();
        ObjectPropertiesBase.SightPresentation.isObservedHeightAbsolute= mIsObservedHeightAbsolute.isChecked();
        ObjectPropertiesBase.SightPresentation.observerHeight=Float.valueOf(String.valueOf(mSightObserverHeight.getText()));
        ObjectPropertiesBase.SightPresentation.observedHeight=Float.valueOf(String.valueOf(mSightObservedHeight.getText()));
        ObjectPropertiesBase.SightPresentation.type= (IMcSightPresentationItemParams.ESightPresentationType) mSightPresentationType.getSelectedItem();
        ObjectPropertiesBase.SightPresentation.numEllipseRays=Integer.valueOf(String.valueOf(mSightNumEllipseRays.getText()));
    }

    private void inflateViews() {
        mSaveBttn = (Button) mRootView.findViewById(R.id.sight_presentation_save_bttn);
        mSightPresentationType = (SpinnerWithLabel) mRootView.findViewById(R.id.sight_presentation_type_swl);
        mSightObserverHeight = (EditText) mRootView.findViewById(R.id.sight_presentation_sight_observer_height);
        mSightObservedHeight = (EditText) mRootView.findViewById(R.id.sight_presentation_sight_observed_height);

        mIsObservedHeightAbsolute = (CheckBox) mRootView.findViewById(R.id.sight_observed_height_absolute_cb);

        mIsObserverHeightAbsolute = (CheckBox) mRootView.findViewById(R.id.sight_observer_height_absolute_cb);
        mObserverMinPitch = (EditText) mRootView.findViewById(R.id.sight_presentation_sight_observer_min_pitch);
        mObserverMaxPitch = (EditText) mRootView.findViewById(R.id.sight_presentation_sight_observer_max_pitch);
        mSightQueryPrecision = (SpinnerWithLabel) mRootView.findViewById(R.id.sight_presentation_sight_query_precision_swl);
        mSightColorSWL = (SpinnerWithLabel) mRootView.findViewById(R.id.sight_presentation_sight_color_swl);
        mSightColorSC = (SelectColor) mRootView.findViewById(R.id.sight_presentation_sight_color_sc);
        mSightNumEllipseRays = (EditText) mRootView.findViewById(R.id.sight_presentation_sight_num_ellipse_rays);
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
