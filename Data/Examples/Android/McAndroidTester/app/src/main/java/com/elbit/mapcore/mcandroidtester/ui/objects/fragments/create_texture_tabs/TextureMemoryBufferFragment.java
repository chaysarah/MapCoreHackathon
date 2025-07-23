package com.elbit.mapcore.mcandroidtester.ui.objects.fragments.create_texture_tabs;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.CheckBox;
import android.widget.EditText;

import com.elbit.mapcore.General.ObjectRef;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.ITextureTab;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.private_properties.TexturePropertyDialogFragment;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.PixelFormatUtils;
import com.elbit.mapcore.mcandroidtester.utils.customviews.CreateTextureBottom;
import com.elbit.mapcore.mcandroidtester.utils.customviews.FileChooserEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SpinnerWithLabel;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcMemoryBufferTexture;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcTexture;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link TextureMemoryBufferFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link TextureMemoryBufferFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class TextureMemoryBufferFragment extends BaseTextureTabFragment implements ITextureTab {

    private OnFragmentInteractionListener mListener;
    public IMcMemoryBufferTexture mMemoryBufferTexture;
    private View mRootView;
    public SpinnerWithLabel mMemBufferTextureUsageSWL;
    public SpinnerWithLabel mBmpOriginalFormatSWL;
    public CheckBox mMipMapCb;
    public FileChooserEditTextLabel mMemoryBufferFileFC;
    private CreateTextureBottom mCreateTextureBottom;
    public Bitmap mBMP;
    private EditText mSrcPixelFormat;
    private EditText mMemBufferSrcWidth;
    private EditText mMemBufferSrcHeight;
    public EditText mMemBufferPixelFormat;
    public EditText mMemBufferWidth;
    public EditText mMemBufferHeight;

    private TexturePropertyDialogFragment mTexturePropertyDialogFragment;
    public void setmTexturePropertyDialogFragment(TexturePropertyDialogFragment TexturePropertyDialogFragment)
    {
        mTexturePropertyDialogFragment = TexturePropertyDialogFragment;
    }

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment TextureMemoryBufferFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static TextureMemoryBufferFragment newInstance() {
        TextureMemoryBufferFragment fragment = new TextureMemoryBufferFragment();
        return fragment;
    }

    public TextureMemoryBufferFragment() {
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
        mRootView = inflater.inflate(R.layout.fragment_texture_memory_buffer, container, false);
        initViews();
        return mRootView;
    }

    private void initViews() {
        inflateViews();
        initBmpFile();
        initBmpOriginalFormat();
        initSrc();
        initTextureUsage();
        initAutoMipMapCB();
        initTextureBottom();
    }

    private void initSrc() {
        ObjectRef<Integer> srcWidth = new ObjectRef<>();
        ObjectRef<Integer> srcHeight = new ObjectRef<>();
        ObjectRef<Integer> textureWidth = new ObjectRef<>();
        ObjectRef<Integer> textureHeight = new ObjectRef<>();

        if (mMemoryBufferTexture != null) {
            try {
                mSrcPixelFormat.setText((String.valueOf(((IMcMemoryBufferTexture) mCurrentTexture).GetSourcePixelFormat())));
                mMemoryBufferTexture.GetSourceSize(srcWidth, srcHeight);
                mMemBufferSrcWidth.setText(String.valueOf(srcWidth));
                mMemBufferSrcHeight.setText(String.valueOf(srcHeight));

                mMemoryBufferTexture.GetSize(textureWidth, textureHeight);
                mMemBufferWidth.setText(String.valueOf(textureWidth));
                mMemBufferHeight.setText(String.valueOf(textureHeight));
                mMemBufferPixelFormat.setText(String.valueOf(mMemoryBufferTexture.GetPixelFormat()));

            } catch (MapCoreException e) {
                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetSourcePixelFormat");
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    private void initBmpFile() {
        mMemoryBufferFileFC.getmFilePathEt().addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {
            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {

            }

            @Override
            public void afterTextChanged(Editable s) {
                //BitmapFactory.Options bmOptions = new BitmapFactory.Options();
                mBMP = BitmapFactory.decodeFile(s.toString()/*, bmOptions*/);
                if (mBMP != null) {
                    IMcTexture.EPixelFormat convertedResult = PixelFormatUtils.convertPixelFormatToEPixelFormat(mBMP.getConfig());
                    mBmpOriginalFormatSWL.setSelection(convertedResult.ordinal());

                    if (convertedResult != IMcTexture.EPixelFormat.EPF_UNKNOWN) {
                        if (mMemoryBufferTexture != null) {
                            ObjectRef<Integer> srcWidth = new ObjectRef<>();
                            ObjectRef<Integer> srcHeight = new ObjectRef<>();
                            try {
                                IMcTexture.EPixelFormat srcFormat = mMemoryBufferTexture.GetSourcePixelFormat();
                                mMemoryBufferTexture.GetSourceSize(srcWidth, srcHeight);
                                mSrcPixelFormat.setText(srcFormat.toString());
                                mMemBufferSrcWidth.setText(String.valueOf(srcWidth));
                                mMemBufferSrcHeight.setText(String.valueOf(srcHeight));

                            } catch (MapCoreException e) {
                                AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetSourceSize");
                            } catch (Exception e) {
                                e.printStackTrace();
                            }
                        }
                        mMemBufferPixelFormat.setText(mBmpOriginalFormatSWL.getSelectedItem().toString());
                        mMemBufferWidth.setText(String.valueOf(mBMP.getWidth()));
                        mMemBufferHeight.setText(String.valueOf(mBMP.getHeight()));

                    } else
                        AlertMessages.ShowErrorMessage(getContext(), "Format Converted", "Format Converted Failed");
                }
            }
        });
    }



    private void initTextureBottom() {
        mCreateTextureBottom.setmCurFragment(this);
        mCreateTextureBottom.setmTexturePropertyDialogFragment(mTexturePropertyDialogFragment);
        mCreateTextureBottom.findViewById(R.id.texture_bottom_colors_ll).setVisibility(View.GONE);
        mCreateTextureBottom.findViewById(R.id.texture_bottom_cbs).setVisibility(View.GONE);
        mCreateTextureBottom.findViewById(R.id.texture_bottom_direct_x_cb).setVisibility(View.VISIBLE);


    }

    private void initAutoMipMapCB() {
        if (mMemoryBufferTexture == null)
            mMipMapCb.setEnabled(true);
        else
            mMipMapCb.setEnabled(false);
    }

    private void initBmpOriginalFormat() {
        mBmpOriginalFormatSWL.setAdapter(new ArrayAdapter<>(getContext(), android.R.layout.simple_spinner_item, IMcTexture.EPixelFormat.values()));

    }

    private void initTextureUsage() {
        //TODO uncomment after will be added to api
        mMemBufferTextureUsageSWL.setAdapter(new ArrayAdapter<>(getContext(), android.R.layout.simple_spinner_item, IMcTexture.EUsage.values()));
        mMemBufferTextureUsageSWL.setSelection(IMcTexture.EUsage.EU_STATIC.ordinal());
        if (mMemoryBufferTexture == null)
            mMemBufferTextureUsageSWL.setEnabled(true);
        else
            mMemBufferTextureUsageSWL.setEnabled(false);
    }

    private void inflateViews() {
        mCreateTextureBottom = (CreateTextureBottom) mRootView.findViewById(R.id.texture_memory_buffer_ctb);
        mMemBufferTextureUsageSWL = (SpinnerWithLabel) mRootView.findViewById(R.id.texture_memory_buffer_texture_usage_swl);
        mBmpOriginalFormatSWL = (SpinnerWithLabel) mRootView.findViewById(R.id.texture_memory_buffer_bmp_original_format_swl);
        mMipMapCb = (CheckBox) mRootView.findViewById(R.id.texture_memory_buffer_auto_mipmap_cb);
        mMemoryBufferFileFC = (FileChooserEditTextLabel) mRootView.findViewById(R.id.texture_memory_buffer_bmp_file_chooser);
        mSrcPixelFormat = (EditText) mRootView.findViewById(R.id.texture_memory_buffer_src_pixel_format_et);
        mMemBufferSrcWidth = (EditText) mRootView.findViewById(R.id.texture_memory_buffer_src_width_et);
        mMemBufferSrcHeight = (EditText) mRootView.findViewById(R.id.texture_memory_buffer_src_height_et);
        mMemBufferPixelFormat = (EditText) mRootView.findViewById(R.id.texture_memory_buffer_pixel_format_et);
        mMemBufferWidth = (EditText) mRootView.findViewById(R.id.texture_memory_buffer_width_et);
        mMemBufferHeight = (EditText) mRootView.findViewById(R.id.texture_memory_buffer_height_et);
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
            mMemoryBufferTexture = (IMcMemoryBufferTexture) curTexture;
            mCurrentTexture = mMemoryBufferTexture;
        } catch (Exception e) {
            e.printStackTrace();
        }
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
