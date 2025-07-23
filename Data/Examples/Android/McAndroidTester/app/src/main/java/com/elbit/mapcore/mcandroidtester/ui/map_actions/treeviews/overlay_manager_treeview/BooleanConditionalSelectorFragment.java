package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlay_manager_treeview;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AbsListView;
import android.widget.ArrayAdapter;
import android.widget.ListView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.ui.adapters.CondSelectorsAdapter;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SpinnerWithLabel;

import java.util.Arrays;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcBooleanConditionalSelector;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcConditionalSelector;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link BooleanConditionalSelectorFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link BooleanConditionalSelectorFragment#newInstance} factory method to
 * create an instance of this fragment.
 *
 */
public class BooleanConditionalSelectorFragment extends BaseConditionalSelectorFragment {

    private OnFragmentInteractionListener mListener;
    private IMcBooleanConditionalSelector mCurrentCondSelector;
    private IMcOverlayManager mOverlayManager;
    private SpinnerWithLabel mOperationSWL;
    private ListView mCondSelectorsLV;
    private IMcConditionalSelector[] mExistingSelectors;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     * @return A new instance of fragment BooleanConditionalSelectorFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static BooleanConditionalSelectorFragment newInstance(String param1, String param2) {
        BooleanConditionalSelectorFragment fragment = new BooleanConditionalSelectorFragment();
        return fragment;
    }
    public BooleanConditionalSelectorFragment() {
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
        mRootview = inflater.inflate(R.layout.fragment_boolean_conditional_selector, container, false);
        inflateViews();
        initViews();
        return mRootview;
    }

    @Override
    protected void inflateViews() {
        super.inflateViews();
        mOperationSWL = (SpinnerWithLabel) mRootview.findViewById(R.id.boolean_conditional_selector_operation_swl);
        mCondSelectorsLV = (ListView) mRootview.findViewById(R.id.boolean_conditional_selector_cond_selectors_lv);
    }

    @Override
    protected void initViews() {
        super.initViews();
        initOperationSWL();
        initSelectorsLV();
    }

    private void initSelectorsLV() {
        fillSelectorsList();
        markActiveSelectors();
        markOperation();
    }

    private void markOperation() {
        try {
            mOperationSWL.setSelection(mCurrentCondSelector.GetBooleanOperation().getValue());
        }catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "GetBooleanOperation");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void markActiveSelectors() {
        try {
            IMcConditionalSelector[] bondSelectors = mCurrentCondSelector.GetListOfSelectors();
            if(bondSelectors.length>0)
            {
                for(int i=0;i<mExistingSelectors.length;i++)
                {
                    for(int z=0;z<bondSelectors.length;z++)
                    {
                        if(mExistingSelectors[i]==bondSelectors[z])
                        {
                            mCondSelectorsLV.setItemChecked(i,true);
                        }
                    }
                }
            }

        }catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "GetListOfSelectors");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void fillSelectorsList() {
        try {
            mExistingSelectors = mOverlayManager.GetConditionalSelectors();
            mCondSelectorsLV.setAdapter(new CondSelectorsAdapter(getContext(),R.layout.checkable_list_item, Arrays.asList(mExistingSelectors)));
            mCondSelectorsLV.setChoiceMode(AbsListView.CHOICE_MODE_MULTIPLE);
        }catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "GetConditionalSelectors");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void initOperationSWL() {
        mOperationSWL.setAdapter(new ArrayAdapter<>(getContext(), android.R.layout.simple_spinner_item, IMcBooleanConditionalSelector.EBooleanOp.values()));
        mOperationSWL.setSelection(IMcBooleanConditionalSelector.EBooleanOp.EB_OR.getValue());
    }

    @Override
    protected void save() {
        super.save();
        IMcConditionalSelector[] checkedSelector=new IMcConditionalSelector[mCondSelectorsLV.getCheckedItemCount()];
        int idx=0;
        for(int i=0;i<mExistingSelectors.length;i++)
        {
            if(mCondSelectorsLV.isItemChecked(i))
            {
                checkedSelector[idx]=mExistingSelectors[i];
                idx++;
            }
        }
        try {
            mCurrentCondSelector.SetListOfSelectors(checkedSelector);
        }catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "SetListOfSelectors");
            e.printStackTrace();
        }    catch (Exception e) {
            e.printStackTrace();
        }
        final IMcBooleanConditionalSelector.EBooleanOp var1 = (IMcBooleanConditionalSelector.EBooleanOp) mOperationSWL.getAdapter().getItem(mOperationSWL.getSelectedItemPosition());
        try {
            mCurrentCondSelector.SetBooleanOperation(var1);
        }catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "SetBooleanOperation");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
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
        mCurrentCondSelector=(IMcBooleanConditionalSelector)obj;
        try {
            mOverlayManager=mCurrentCondSelector.GetOverlayManager();
        }catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "GetOverlayManager");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
        super.setObject(obj);
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mCurrentCondSelector));
    }

}
