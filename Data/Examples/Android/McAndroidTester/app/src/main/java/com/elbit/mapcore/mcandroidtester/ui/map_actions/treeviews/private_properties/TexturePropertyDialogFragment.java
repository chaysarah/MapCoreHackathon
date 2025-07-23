package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.private_properties;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.DialogFragment;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentTransaction;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.ui.adapters.PrivatePropertiesListAdapter;
import com.elbit.mapcore.mcandroidtester.ui.objects.fragments.create_texture_tabs.CreateTextureTabsFragment;
import com.elbit.mapcore.mcandroidtester.utils.Consts;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcTexture;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link TexturePropertyDialogFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link TexturePropertyDialogFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class TexturePropertyDialogFragment extends DialogFragment {

    private OnFragmentInteractionListener mListener;
    private View mRootView;
    private int mPropertyId;
    private IMcTexture mCurTexture;
    private TextView mPropertyIdTV;
    private Button mCreateTextureBttn;
    private Button mEditTextureBttn;
    private Button mDeleteTextureBttn;
    //rivate IMcTexture mCreatedTexture;
    private Button mOkBttn;
    private PrivatePropertiesListAdapter.PropertiesHolder mCurTextureHolder;
    boolean isTextureCreated = false;

    public static TexturePropertyDialogFragment newInstance() {
        TexturePropertyDialogFragment fragment = new TexturePropertyDialogFragment();
        return fragment;
    }

    public TexturePropertyDialogFragment() {
        // Required empty public constructor
    }

    public void setCurTexture(int propertyId, IMcTexture texture) {
        mPropertyId = propertyId;
        mCurTexture = texture;
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
        mRootView = inflater.inflate(R.layout.fragment_texture_property_dialog, container, false);
        inflateViews();
        initViews();
        return mRootView;
    }

    private void inflateViews() {
        mPropertyIdTV = (TextView) mRootView.findViewById(R.id.texture_property_id);
        mCreateTextureBttn = (Button) mRootView.findViewById(R.id.texture_property_create);
        mEditTextureBttn = (Button) mRootView.findViewById(R.id.texture_property_edit);
        mDeleteTextureBttn = (Button) mRootView.findViewById(R.id.texture_property_delete);
        mOkBttn = (Button) mRootView.findViewById(R.id.texture_property_dialog_ok_bttn);

    }

    private void initViews() {
        setBttnsEnable();
        mOkBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                returnToUpdatePropertiesList();
                TexturePropertyDialogFragment.this.dismiss();

            }
        });
        mPropertyIdTV.setText(String.valueOf(mPropertyId));
        final TexturePropertyDialogFragment texturePropertyDialogFragment = this;
        mCreateTextureBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                CreateTextureTabsFragment createTextureTabsFragment = CreateTextureTabsFragment.newInstance();
                createTextureTabsFragment.setCurrentTexture(mCurTexture);
                createTextureTabsFragment.setmTexturePropertyDialogFragment(texturePropertyDialogFragment);

                FragmentTransaction transaction = getChildFragmentManager().beginTransaction().add(R.id.texture_fragment_container, createTextureTabsFragment, Consts.TextureFragmentsTags.TEXTURE_FROM_PROPERTIES_LIST);
                transaction.addToBackStack(Consts.TextureFragmentsTags.TEXTURE_FROM_PROPERTIES_LIST);
                transaction.commit();
            }
        });
        mEditTextureBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                CreateTextureTabsFragment createTextureTabsFragment;
                if (mCurTexture != null) {
                    createTextureTabsFragment = CreateTextureTabsFragment.newInstance();
                    createTextureTabsFragment.setCurrentTexture( mCurTexture);
                    FragmentTransaction transaction = getChildFragmentManager().beginTransaction().add(R.id.texture_fragment_container, createTextureTabsFragment, Consts.TextureFragmentsTags.TEXTURE_FROM_PROPERTIES_LIST);
                    transaction.addToBackStack(Consts.TextureFragmentsTags.TEXTURE_FROM_PROPERTIES_LIST);
                    transaction.commit();
                }
            }
        });
        mDeleteTextureBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                mCurTexture=null;
            }
        });
    }

    private void returnToUpdatePropertiesList() {
        PropertiesIdListFragment propertiesIdListFragment = (PropertiesIdListFragment) getFragmentManager().findFragmentByTag(PropertiesIdListFragment.class.getSimpleName());
        if (propertiesIdListFragment != null) {
            propertiesIdListFragment.updateTextureProperty(mPropertyId, mCurTexture);
        }
    }

    private void setBttnsEnable() {
        isTextureCreated = (!(mCurTexture == null));
        mCreateTextureBttn.setEnabled(!isTextureCreated);
        mEditTextureBttn.setEnabled(isTextureCreated);
        mDeleteTextureBttn.setEnabled(isTextureCreated);
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

    public void onTextureActionsCompleted(IMcTexture createdTexture) {
        mCurTexture = createdTexture;
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
