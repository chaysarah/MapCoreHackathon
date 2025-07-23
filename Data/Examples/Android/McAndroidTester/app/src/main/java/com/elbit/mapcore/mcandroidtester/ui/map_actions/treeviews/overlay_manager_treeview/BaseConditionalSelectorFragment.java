package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlay_manager_treeview;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SpinnerWithLabel;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcConditionalSelector;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link BaseConditionalSelectorFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link BaseConditionalSelectorFragment#newInstance} factory method to
 * create an instance of this fragment.
 *
 */
public class BaseConditionalSelectorFragment extends Fragment implements FragmentWithObject {

    private OnFragmentInteractionListener mListener;
    protected View mRootview;
    private Button mSaveBttn;
    private NumericEditTextLabel mConditionalSelectorIDNETL;
    private SpinnerWithLabel mConditionalSelectorTypeSWL;
    private IMcConditionalSelector mCurrentCondSelector;
    private IMcOverlayManager mOverlayManager;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment BaseConditionalSelectorFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static BaseConditionalSelectorFragment newInstance(String param1, String param2) {
        BaseConditionalSelectorFragment fragment = new BaseConditionalSelectorFragment();
        return fragment;
    }
    public BaseConditionalSelectorFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        Funcs.SetObjectFromBundle(savedInstanceState, this );
    }

    protected void initViews() {
        setTitle();
        mConditionalSelectorTypeSWL.setAdapter(new ArrayAdapter<>(getContext(), android.R.layout.simple_spinner_item, IMcConditionalSelector.EConditionalSelectorType.values()));
        try {
            mConditionalSelectorTypeSWL.setSelection(mCurrentCondSelector.GetConditionalSelectorType().ordinal());
        }catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "GetConditionalSelectorType");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
        try {
            mConditionalSelectorIDNETL.setUInt(mCurrentCondSelector.GetID());
        }catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "GetID");
            e.printStackTrace();
        }  catch (Exception e) {
            e.printStackTrace();
        }

        mSaveBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                save();
            }
        });
    }

    private void setTitle() {
        getActivity().setTitle(this.getClass().getSimpleName().replace("Fragment","").replaceAll("\\d+", "").replaceAll("(.)([A-Z])", "$1 $2"));
    }

    protected void save() {
        final int id = mConditionalSelectorIDNETL.getUInt();
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    mCurrentCondSelector.SetID(id);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "SetID");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }});
    }

    protected void inflateViews() {
        mSaveBttn=(Button)mRootview.findViewById(R.id.conditional_selector_save_changes_bttn);
        mConditionalSelectorIDNETL =(NumericEditTextLabel)mRootview.findViewById(R.id.conditional_selector_id);
        mConditionalSelectorTypeSWL =(SpinnerWithLabel)mRootview.findViewById(R.id.conditional_selector_type);
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
        } /*else {
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
    public void setObject(Object obj) {
        mCurrentCondSelector=((IMcConditionalSelector) obj);
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mCurrentCondSelector));
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
    public static String getSelectorFragNameFromSelectorClassName(String selectorClassName)
    {
        String fragmentName=selectorClassName.substring(2).concat("Fragment");
        String pkgName=BaseConditionalSelectorFragment.class.getPackage().getName();
        return pkgName.concat("."+fragmentName);
    }
}
