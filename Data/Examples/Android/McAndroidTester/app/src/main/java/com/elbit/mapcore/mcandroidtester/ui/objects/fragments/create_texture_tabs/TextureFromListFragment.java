package com.elbit.mapcore.mcandroidtester.ui.objects.fragments.create_texture_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.managers.ObjectWorld.Manager_MCTextures;
import com.elbit.mapcore.mcandroidtester.ui.adapters.HashMapAdapter;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.private_properties.TexturePropertyDialogFragment;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.customviews.CreateTextureBottom;

import java.util.ArrayList;
import java.util.HashMap;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcTexture;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link TextureFromListFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link TextureFromListFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class TextureFromListFragment extends BaseTextureTabFragment {

    private OnFragmentInteractionListener mListener;
    private View mRootview;
    public ListView mExistingTexturesLV;
    private ArrayList<IMcTexture> mLstExistingTexturesValues;
    private CreateTextureBottom mCreateTextureBottom;

    private TexturePropertyDialogFragment mTexturePropertyDialogFragment;
    public void setmTexturePropertyDialogFragment(TexturePropertyDialogFragment TexturePropertyDialogFragment)
    {
        mTexturePropertyDialogFragment = TexturePropertyDialogFragment;
    }

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment TextureFromListFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static TextureFromListFragment newInstance() {
        TextureFromListFragment fragment = new TextureFromListFragment();
        return fragment;
    }

    public TextureFromListFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        mRootview = inflater.inflate(R.layout.fragment_texture_from_list, container, false);
        initViews();
        return mRootview;
    }

    private void initViews() {
        inflateViews();
        initExistingTextures();
        initTextureBottom();
    }

    private void initTextureBottom() {
        mCreateTextureBottom.setmCurFragment(this);
        mCreateTextureBottom.setmTexturePropertyDialogFragment(mTexturePropertyDialogFragment);
        mCreateTextureBottom.findViewById(R.id.texture_bottom_colors_ll).setVisibility(View.GONE);
        mCreateTextureBottom.findViewById(R.id.texture_bottom_cbs).setVisibility(View.GONE);
    }

    private void initExistingTextures() {
        mLstExistingTexturesValues = new ArrayList<>();
        HashMap<Object, Integer> textures = Manager_MCTextures.getInstance().getTextures();
        HashMapAdapter texturesAdapter = new HashMapAdapter(getContext(), textures, Consts.ListType.SINGLE_CHECK);
        mExistingTexturesLV.setAdapter(texturesAdapter);
        mExistingTexturesLV.setChoiceMode(ListView.CHOICE_MODE_SINGLE);
    }

    private void inflateViews() {
        mCreateTextureBottom=(CreateTextureBottom)mRootview.findViewById(R.id.texture_from_list_ctb);
        mExistingTexturesLV = (ListView) mRootview.findViewById(R.id.texture_from_list_existing_texture_list);
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
        }
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
