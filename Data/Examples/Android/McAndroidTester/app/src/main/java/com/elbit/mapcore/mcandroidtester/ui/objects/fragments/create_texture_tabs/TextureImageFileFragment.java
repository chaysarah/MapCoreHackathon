package com.elbit.mapcore.mcandroidtester.ui.objects.fragments.create_texture_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.ITextureTab;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.private_properties.TexturePropertyDialogFragment;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.customviews.CreateTextureBottom;
import com.elbit.mapcore.mcandroidtester.utils.customviews.FileChooserEditTextLabel;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcImageFileTexture;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcTexture;
import com.elbit.mapcore.Structs.SMcFileSource;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link TextureImageFileFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link TextureImageFileFragment#newInstance} factory method to
 * create an instance of this fragment.
 *
 */
public class TextureImageFileFragment extends BaseTextureTabFragment implements ITextureTab{

    private OnFragmentInteractionListener mListener;
    public IMcImageFileTexture mImageTexture;
    private View mRootView;
    public FileChooserEditTextLabel mImageFileFC;
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
     * @return A new instance of fragment TextureImageFileFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static TextureImageFileFragment newInstance() {
        TextureImageFileFragment fragment = new TextureImageFileFragment();
        return fragment;
    }
    public TextureImageFileFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        mRootView=inflater.inflate(R.layout.fragment_texture_image_file, container, false);;
        // Inflate the layout for this fragment
        initViews();
        return mRootView;
    }

    private void initViews() {
        inflateViews();
        if (mCurrentTexture != null)
            initImageFile();
        initTextureBottom();
    }

    private void initImageFile() {
        try {
            SMcFileSource sImageSource = ((IMcImageFileTexture) mCurrentTexture).GetImageFile();
            mImageFileFC.setDirPath(sImageSource.strFileName);
        } catch (MapCoreException McEx) {
            AlertMessages.ShowMapCoreErrorMessage(getContext(), McEx, "GetImageFile");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void inflateViews() {
        mImageFileFC=(FileChooserEditTextLabel)mRootView.findViewById(R.id.texture_image_file_file_chooser);
        mCreateTextureBottom=(CreateTextureBottom)mRootView.findViewById(R.id.texture_image_file_ctb);
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

    @Override
    public void setCurrentTexture(IMcTexture curTexture) {
        super.setCurrentTexture(curTexture);
        try {
            mImageTexture = (IMcImageFileTexture) curTexture;
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initTextureBottom() {
        mCreateTextureBottom.setmCurFragment(this);
        mCreateTextureBottom.setmTexturePropertyDialogFragment(mTexturePropertyDialogFragment);
    }

    /**
     * This interface must be implemented by activities that contain this
     * fragment to allow an interaction in this fragment to be communicated
     * to the activity and potentially other fragments contained in that
     * activity.
     * <p>
     * See the Android Training lesson <a href=
     * "http://developer.android.com/training/basics/fragments/communicating.html"
     * >Communicating with Other Fragments</a> for more information.
     */
    public interface OnFragmentInteractionListener {
        // TODO: Update argument type and name
        void onFragmentInteraction(Uri uri);
    }
}
