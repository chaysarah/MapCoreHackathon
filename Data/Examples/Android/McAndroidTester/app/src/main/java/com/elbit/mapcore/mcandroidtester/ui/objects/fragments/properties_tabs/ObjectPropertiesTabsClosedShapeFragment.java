package com.elbit.mapcore.mcandroidtester.ui.objects.fragments.properties_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentTransaction;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CompoundButton;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.ui.objects.ObjectPropertiesBase;
import com.elbit.mapcore.mcandroidtester.ui.objects.fragments.create_texture_tabs.CreateTextureTabsFragment;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SelectColor;
import com.elbit.mapcore.mcandroidtester.utils.customviews.TwoDFVector;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link ObjectPropertiesTabsClosedShapeFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link ObjectPropertiesTabsClosedShapeFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class ObjectPropertiesTabsClosedShapeFragment extends Fragment {

    private OnFragmentInteractionListener mListener;
    private View mRootView;
    private Button mSaveBttn;
    private SelectColor mFillColorSC;
    private CheckBox mFillTextureNoneCb;
    private Button mClosedShapeTexturebttn;
    private TwoDFVector mFillTextureScale;


    /**
     * Use this factory method to create a new instance of
     *
     * @return A new instance of fragment ObjectPropertiesTabsClosedShapeFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static ObjectPropertiesTabsClosedShapeFragment newInstance() {
        ObjectPropertiesTabsClosedShapeFragment fragment = new ObjectPropertiesTabsClosedShapeFragment();
        return fragment;
    }

    public ObjectPropertiesTabsClosedShapeFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        mRootView = inflater.inflate(R.layout.fragment_object_properties_tabs_closed_shape, container, false);
        initviews();
        return mRootView;
    }

    private void initviews() {
        inflateViews();
        initSaveBttn();
        initFillTextureScale();
        initFillColor();
        initFillTexture();
    }


    private void initFillTextureScale() {
        mFillTextureScale=(TwoDFVector)mRootView.findViewById(R.id.closed_shape_fill_texture_scale);
        mFillTextureScale.setVector2D(ObjectPropertiesBase.mFillTextureScale);
    }

    private void initFillColor() {
        mFillColorSC.enableButtons(true);
        mFillColorSC.setmBColor(ObjectPropertiesBase.mFillColor);

    }

    private void initFillTexture() {
        if (ObjectPropertiesBase.mFillTexture == null)
            mFillTextureNoneCb.setChecked(true);
        else
            mFillTextureNoneCb.setChecked(false);
        mClosedShapeTexturebttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                CreateTextureTabsFragment createTextureTabsFragment;
                if (ObjectPropertiesBase.mFillTexture == null) {
                    createTextureTabsFragment = CreateTextureTabsFragment.newInstance();
                    createTextureTabsFragment.setCurrentTexture(null);
                } else {
                    createTextureTabsFragment = CreateTextureTabsFragment.newInstance();
                    createTextureTabsFragment.setCurrentTexture(ObjectPropertiesBase.mFillTexture);
                }

                FragmentTransaction transaction = getChildFragmentManager().beginTransaction().add(R.id.closed_shape_texture_container, createTextureTabsFragment, Consts.TextureFragmentsTags.TEXTURE_FROM_CLOSED_SHAPE);
                transaction.addToBackStack(Consts.TextureFragmentsTags.TEXTURE_FROM_CLOSED_SHAPE);
                transaction.commit();
                //ObjectPropertiesBase.mLineTexture=(McTexture)createTextureTabsFragment.getmCurrentTexture();
            }
        });

        mFillTextureNoneCb.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                if (mFillTextureNoneCb.isChecked()) {
                    mClosedShapeTexturebttn.setEnabled(false);
                    ObjectPropertiesBase.mFillTexture = null;
                } else
                    mClosedShapeTexturebttn.setEnabled(true);
            }
        });
    }


    private void initSaveBttn() {
        mSaveBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                saveFillTextureScale();
                saveFillColor();
                saveFillTexture();
            }
        });
    }

    private void saveFillTextureScale() {
        ObjectPropertiesBase.mFillTextureScale.x=mFillTextureScale.getmX();
        ObjectPropertiesBase.mFillTextureScale.y=mFillTextureScale.getmY();
    }

    private void saveFillColor() {
        ObjectPropertiesBase.mFillColor = mFillColorSC.getmBColor();
    }

    private void saveFillTexture() {

    }

    private void inflateViews() {
        mSaveBttn = (Button) mRootView.findViewById(R.id.object_properties_tabs_closed_shape_save_bttn);
        mFillColorSC = (SelectColor) mRootView.findViewById(R.id.closed_shape_fill_color_sc);
        mFillTextureNoneCb = (CheckBox) mRootView.findViewById(R.id.obj_properties_closed_shape_texture_none_rb);
        mClosedShapeTexturebttn = (Button) mRootView.findViewById(R.id.obj_properties_closed_shape_texture_bttn);
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
