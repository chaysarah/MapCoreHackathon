package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlay_manager_treeview;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.CheckBox;
import android.widget.LinearLayout;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcScaleConditionalSelector;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link ScaleConditionalSelectorFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link ScaleConditionalSelectorFragment#newInstance} factory method to
 * create an instance of this fragment.
 *
 */
public class ScaleConditionalSelectorFragment extends BaseConditionalSelectorFragment implements FragmentWithObject{

    private int MASK0 = 1;
    private int MASK1 = 2;
    private int MASK2 = 4;
    private int MASK3 = 8;
    private int MASK4 = 16;
    private int MASK5 = 32;
    private int MASK6 = 64;
    private int MASK7 = 128;
    private int MASK8 = 256;
    private int MASK9 = 512;
    private OnFragmentInteractionListener mListener;
    private IMcScaleConditionalSelector mCurrentCondSelector;
    private NumericEditTextLabel mMinScaleNET;
    private NumericEditTextLabel mMaxScaleNET;
    private CheckBox mCancelScale0;
    private CheckBox mCancelScale1;
    private CheckBox mCancelScale2;
    private CheckBox mCancelScale3;
    private CheckBox mCancelScale4;
    private CheckBox mCancelScale5;
    private CheckBox mCancelScale6;
    private CheckBox mCancelScale7;
    private CheckBox mCancelScale8;
    private CheckBox mCancelScale9;
    private CheckBox mCancelScaleResult0;
    private CheckBox mCancelScaleResult1;
    private CheckBox mCancelScaleResult2;
    private CheckBox mCancelScaleResult3;
    private CheckBox mCancelScaleResult4;
    private CheckBox mCancelScaleResult5;
    private CheckBox mCancelScaleResult6;
    private CheckBox mCancelScaleResult7;
    private CheckBox mCancelScaleResult8;
    private CheckBox mCancelScaleResult9;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment ScaleConditionalSelectorFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static ScaleConditionalSelectorFragment newInstance() {
        ScaleConditionalSelectorFragment fragment = new ScaleConditionalSelectorFragment();
        return fragment;
    }
    public ScaleConditionalSelectorFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        Funcs.SetObjectFromBundle(savedInstanceState, this );
        mRootview=inflater.inflate(R.layout.fragment_scale_conditional_selector, container, false);
        inflateViews();
        initViews();
        return mRootview;
    }

    // TODO: Rename method, update argument and hook method into UI event
    public void onButtonPressed(Uri uri) {
        if (mListener != null) {
            mListener.onFragmentInteraction(uri);
        }
    }

    @Override
    protected void inflateViews() {
        super.inflateViews();
        mMinScaleNET=(NumericEditTextLabel)mRootview.findViewById(R.id.scale_conditional_selector_min_scale_net);
        mMaxScaleNET=(NumericEditTextLabel)mRootview.findViewById(R.id.scale_conditional_selector_max_scale_net);
        LinearLayout cancelScaleLayout=(LinearLayout)mRootview.findViewById(R.id.scale_conditional_selector_cancel_scale_checkboxes);
        mCancelScale0=(CheckBox)cancelScaleLayout.findViewById(R.id.checkbox_0);
        mCancelScale1=(CheckBox)cancelScaleLayout.findViewById(R.id.checkbox_1);
        mCancelScale2=(CheckBox)cancelScaleLayout.findViewById(R.id.checkbox_2);
        mCancelScale3=(CheckBox)cancelScaleLayout.findViewById(R.id.checkbox_3);
        mCancelScale4=(CheckBox)cancelScaleLayout.findViewById(R.id.checkbox_4);
        mCancelScale5=(CheckBox)cancelScaleLayout.findViewById(R.id.checkbox_5);
        mCancelScale6=(CheckBox)cancelScaleLayout.findViewById(R.id.checkbox_6);
        mCancelScale7=(CheckBox)cancelScaleLayout.findViewById(R.id.checkbox_7);
        mCancelScale8=(CheckBox)cancelScaleLayout.findViewById(R.id.checkbox_8);
        mCancelScale9=(CheckBox)cancelScaleLayout.findViewById(R.id.checkbox_9);
        LinearLayout cancelScaleResultLayout=(LinearLayout)mRootview.findViewById(R.id.scale_conditional_selector_cancel_scale_result_checkboxes);
        mCancelScaleResult0=(CheckBox)cancelScaleResultLayout.findViewById(R.id.checkbox_0);
        mCancelScaleResult1=(CheckBox)cancelScaleResultLayout.findViewById(R.id.checkbox_1);
        mCancelScaleResult2=(CheckBox)cancelScaleResultLayout.findViewById(R.id.checkbox_2);
        mCancelScaleResult3=(CheckBox)cancelScaleResultLayout.findViewById(R.id.checkbox_3);
        mCancelScaleResult4=(CheckBox)cancelScaleResultLayout.findViewById(R.id.checkbox_4);
        mCancelScaleResult5=(CheckBox)cancelScaleResultLayout.findViewById(R.id.checkbox_5);
        mCancelScaleResult6=(CheckBox)cancelScaleResultLayout.findViewById(R.id.checkbox_6);
        mCancelScaleResult7=(CheckBox)cancelScaleResultLayout.findViewById(R.id.checkbox_7);
        mCancelScaleResult8=(CheckBox)cancelScaleResultLayout.findViewById(R.id.checkbox_8);
        mCancelScaleResult9=(CheckBox)cancelScaleResultLayout.findViewById(R.id.checkbox_9);

    }

    @Override
    protected void save() {
        super.save();
        saveScales();
        saveScaleMode();
        saveScaleModeResult();
    }

    private void saveScaleModeResult() {
        int scaleModeResult = 0;
        scaleModeResult += (mCancelScaleResult0.isChecked()) ? MASK0 : 0;
        scaleModeResult += (mCancelScaleResult1.isChecked()) ? MASK1 : 0;
        scaleModeResult += (mCancelScaleResult2.isChecked()) ? MASK2 : 0;
        scaleModeResult += (mCancelScaleResult3.isChecked()) ? MASK3 : 0;
        scaleModeResult += (mCancelScaleResult4.isChecked()) ? MASK4 : 0;
        scaleModeResult += (mCancelScaleResult5.isChecked()) ? MASK5 : 0;
        scaleModeResult += (mCancelScaleResult6.isChecked()) ? MASK6 : 0;
        scaleModeResult += (mCancelScaleResult7.isChecked()) ? MASK7 : 0;
        scaleModeResult += (mCancelScaleResult8.isChecked()) ? MASK8 : 0;
        scaleModeResult += (mCancelScaleResult9.isChecked()) ? MASK9 : 0;

        final int finalScaleModeResult = scaleModeResult;
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    mCurrentCondSelector.SetCancelScaleModeResult(finalScaleModeResult);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "SetCancelScaleMode");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void saveScaleMode() {
        int scaleMode = 0;
        scaleMode += (mCancelScale0.isChecked()) ? MASK0 : 0;
        scaleMode += (mCancelScale1.isChecked()) ? MASK1 : 0;
        scaleMode += (mCancelScale2.isChecked()) ? MASK2 : 0;
        scaleMode += (mCancelScale3.isChecked()) ? MASK3 : 0;
        scaleMode += (mCancelScale4.isChecked()) ? MASK4 : 0;
        scaleMode += (mCancelScale5.isChecked()) ? MASK5 : 0;
        scaleMode += (mCancelScale6.isChecked()) ? MASK6 : 0;
        scaleMode += (mCancelScale7.isChecked()) ? MASK7 : 0;
        scaleMode += (mCancelScale8.isChecked()) ? MASK8 : 0;
        scaleMode += (mCancelScale9.isChecked()) ? MASK9 : 0;

        final int finalScaleMode = 0;
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    mCurrentCondSelector.SetCancelScaleMode(finalScaleMode);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "SetCancelScaleMode");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void saveScales() {
        final float minScale = mMinScaleNET.getFloat();
        final float maxScale = mMaxScaleNET.getFloat();

        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    mCurrentCondSelector.SetMinScale(minScale);
                    mCurrentCondSelector.SetMaxScale(maxScale);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "SetMaxScale");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    @Override
    protected void initViews() {
        super.initViews();
        initScales();
        initScaleMode();
        initScaleModeResult();


    }

    private void initScaleModeResult() {
        int scaleModeResult= 0;
        try {
            scaleModeResult = mCurrentCondSelector.GetCancelScaleModeResult();
            mCancelScaleResult0.setChecked(((scaleModeResult & MASK0) > 0));
            mCancelScaleResult1.setChecked(((scaleModeResult & MASK1) > 0));
            mCancelScaleResult2.setChecked(((scaleModeResult & MASK2) > 0));
            mCancelScaleResult3.setChecked(((scaleModeResult & MASK3) > 0));
            mCancelScaleResult4.setChecked(((scaleModeResult & MASK4) > 0));
            mCancelScaleResult5.setChecked(((scaleModeResult & MASK5) > 0));
            mCancelScaleResult6.setChecked(((scaleModeResult & MASK6) > 0));
            mCancelScaleResult7.setChecked(((scaleModeResult & MASK7) > 0));
            mCancelScaleResult8.setChecked(((scaleModeResult & MASK8) > 0));
            mCancelScaleResult9.setChecked(((scaleModeResult & MASK9) > 0));
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "GetCancelScaleModeResult");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initScaleMode() {
        int scaleMode= 0;
        try {
            scaleMode = mCurrentCondSelector.GetCancelScaleMode();
            mCancelScale0.setChecked(((scaleMode & MASK0) > 0));
            mCancelScale1.setChecked(((scaleMode & MASK1) > 0));
            mCancelScale2.setChecked(((scaleMode & MASK2) > 0));
            mCancelScale3.setChecked(((scaleMode & MASK3) > 0));
            mCancelScale4.setChecked(((scaleMode & MASK4) > 0));
            mCancelScale5.setChecked(((scaleMode & MASK5) > 0));
            mCancelScale6.setChecked(((scaleMode & MASK6) > 0));
            mCancelScale7.setChecked(((scaleMode & MASK7) > 0));
            mCancelScale8.setChecked(((scaleMode & MASK8) > 0));
            mCancelScale9.setChecked(((scaleMode & MASK9) > 0));
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "GetCancelScaleMode");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initScales() {
        try {
            mMinScaleNET.setFloat(mCurrentCondSelector.GetMinScale());
            mMaxScaleNET.setFloat(mCurrentCondSelector.GetMaxScale());
        } catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "GetMin/MaxScale");
            e.printStackTrace();
        }catch (Exception e) {
            e.printStackTrace();
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
     * <p>
     * See the Android Training lesson <a href=
     * "http://developer.android.com/training/basics/fragments/communicating.html"
     * >Communicating with Other Fragments</a> for more information.
     */
    public interface OnFragmentInteractionListener {
        // TODO: Update argument type and name
        void onFragmentInteraction(Uri uri);
    }

    @Override
    public void setObject(Object obj) {
        mCurrentCondSelector=(IMcScaleConditionalSelector)obj;
        super.setObject(obj);
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mCurrentCondSelector));
    }
}
