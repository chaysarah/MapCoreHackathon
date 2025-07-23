package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.private_properties;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.annotation.Nullable;
import androidx.fragment.app.DialogFragment;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentTransaction;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.ui.objects.fragments.create_text_tabs.CreateTextTabsFragment;
import com.elbit.mapcore.mcandroidtester.utils.Consts;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcFont;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link FontPropertyDialogFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link FontPropertyDialogFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class FontPropertyDialogFragment extends DialogFragment {

    private OnFragmentInteractionListener mListener;
    private View mRootView;
    private int mPropertyId;
    private IMcFont mCurFont;
    private TextView mPropertyIdTV;
    private Button mCreateFontBttn;
    private Button mDeleteFontBttn;
    //private IMcFont mCreatedFont;
    private Button mOkBttn;

    public static FontPropertyDialogFragment newInstance() {
        FontPropertyDialogFragment fragment = new FontPropertyDialogFragment();
        return fragment;
    }

    public FontPropertyDialogFragment() {
        // Required empty public constructor
    }

    public void setCurFont(int propertyId, IMcFont font) {
        mPropertyId = propertyId;
        mCurFont = font;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if (getArguments() != null) {

        }
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_font_property_dialog, container);
    }

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        inflateViews(view);
        initViews();
        view.findViewById(R.id.font_property_id);
    }

    private void inflateViews(View view) {
        mPropertyIdTV = (TextView) view.findViewById(R.id.font_property_id);
        mCreateFontBttn = (Button) view.findViewById(R.id.font_property_create_font);
        mDeleteFontBttn = (Button) view.findViewById(R.id.font_property_delete_font);

        mOkBttn = (Button) view.findViewById(R.id.font_property_dialog_ok_bttn);
    }

    private void initViews() {
        mOkBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                returnToUpdatePropertiesList();
                FontPropertyDialogFragment.this.dismiss();
            }
        });
        mPropertyIdTV.setText(String.valueOf(mPropertyId));
        mCreateFontBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                CreateTextTabsFragment createTextTabsFragment = CreateTextTabsFragment.newInstance();
                createTextTabsFragment.setCurrentFont(mCurFont);

                FragmentTransaction transaction = getChildFragmentManager().beginTransaction().add(R.id.font_fragment_container, createTextTabsFragment, Consts.TextFragmentsTags.TEXT_FROM_PROPERTIES_LIST);
                transaction.addToBackStack(Consts.TextFragmentsTags.TEXT_FROM_PROPERTIES_LIST);
                transaction.commit();
            }
        });

        mDeleteFontBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                mCurFont = null;
                //mCreatedFont = null;
            }
        });
    }

    private void returnToUpdatePropertiesList() {
        PropertiesIdListFragment propertiesIdListFragment = (PropertiesIdListFragment) getFragmentManager().findFragmentByTag(PropertiesIdListFragment.class.getSimpleName());
        if (propertiesIdListFragment != null) {
            propertiesIdListFragment.updateFontProperty(mPropertyId, mCurFont);
        }

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

    public void onFontActionsCompleted(IMcFont createdFont) {
        mCurFont = createdFont;
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
