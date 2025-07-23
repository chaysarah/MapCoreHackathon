package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlay_manager_treeview;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.NumericEditTextLabel;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectStateConditionalSelector;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link ObjectStateConditionalSelectorFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link ObjectStateConditionalSelectorFragment#newInstance} factory method to
 * create an instance of this fragment.
 *
 */
public class ObjectStateConditionalSelectorFragment extends BaseConditionalSelectorFragment implements FragmentWithObject {

    private OnFragmentInteractionListener mListener;
    private IMcObjectStateConditionalSelector mCurrentSelector;
    private NumericEditTextLabel mObjectStateNET;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     * @return A new instance of fragment ObjectStateConditionalSelectorFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static ObjectStateConditionalSelectorFragment newInstance(String param1, String param2) {
        ObjectStateConditionalSelectorFragment fragment = new ObjectStateConditionalSelectorFragment();
        return fragment;
    }
    public ObjectStateConditionalSelectorFragment() {
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
        mRootview=inflater.inflate(R.layout.fragment_object_state_conditional_selector, container, false);
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
        mObjectStateNET=(NumericEditTextLabel)mRootview.findViewById(R.id.object_state_conditional_selector_object_state);

    }

    @Override
    protected void initViews() {
        super.initViews();
        try {
            mObjectStateNET.setByte(mCurrentSelector.GetObjectState());
        }catch (MapCoreException e) {
            AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "GetObjectState");
            e.printStackTrace();
        } catch (Exception e) {
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
    protected void save() {
        final byte state = mObjectStateNET.getByte();
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    mCurrentSelector.SetObjectState(state);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getActivity(), e, "SetObjectState");
                    e.printStackTrace();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
        super.save();
    }

    @Override
    public void setObject(Object obj) {
        mCurrentSelector=(IMcObjectStateConditionalSelector)obj;
        super.setObject(obj);
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mCurrentSelector));
    }
}
