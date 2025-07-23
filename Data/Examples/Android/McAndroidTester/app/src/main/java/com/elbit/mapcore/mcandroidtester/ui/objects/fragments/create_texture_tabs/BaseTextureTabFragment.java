package com.elbit.mapcore.mcandroidtester.ui.objects.fragments.create_texture_tabs;

import android.content.Context;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.ITextureTab;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcTexture;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link BaseTextureTabFragment.OnTextureCreatedListener} interface
 * to handle interaction events.
 * Use the {@link BaseTextureTabFragment#newInstance} factory method to
 * create an instance of this fragment.
 *
 */
public class BaseTextureTabFragment extends Fragment implements ITextureTab{
    public IMcTexture mCurrentTexture=null;

    public OnTextureCreatedListener mTextureCreatedListener;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment BaseTextureTabFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static BaseTextureTabFragment newInstance() {
        BaseTextureTabFragment fragment = new BaseTextureTabFragment();
        return fragment;
    }
    public BaseTextureTabFragment() {
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
        return inflater.inflate(R.layout.fragment_base_texture_tab, container, false);
    }

    @Override
    public void onAttach(Context context) {
        super.onAttach(context);
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mTextureCreatedListener = null;
    }

    @Override
    public void setCurrentTexture(IMcTexture curTexture) {
        mCurrentTexture=curTexture;
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
    public interface OnTextureCreatedListener {
        // TODO: Update argument type and name
        public void onTextureCreated(IMcTexture mCurTexture, String tag);
    }
}
