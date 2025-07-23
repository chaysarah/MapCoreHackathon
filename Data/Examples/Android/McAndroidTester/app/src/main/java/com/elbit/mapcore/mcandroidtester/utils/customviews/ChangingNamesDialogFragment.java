package com.elbit.mapcore.mcandroidtester.utils.customviews;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.DialogFragment;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.managers.Manager_MCNames;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link ChangingNamesDialogFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link ChangingNamesDialogFragment#newInstance} factory method to
 * create an instance of this fragment.
 *
 */
public class ChangingNamesDialogFragment extends DialogFragment implements FragmentWithObject {

    private String mParam1;
    private String mParam2;
    private String mNewName;
    private String mFullName;
    private int mCurrId;
    private Object mCurrNode;

    private OnNameChangedListener mNameChangedListener;
    private OnFragmentInteractionListener mListener;
    private EditText mNameET;
    private View mRootView;
    private Button mSaveBttn;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment ChangingNamesDialogFragment.
     */
    public static ChangingNamesDialogFragment newInstance(String param1, String param2) {
        ChangingNamesDialogFragment fragment = new ChangingNamesDialogFragment();
        return fragment;
    }

    public ChangingNamesDialogFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        Fragment parentFragment = getParentFragment();
        if (parentFragment instanceof OnNameChangedListener)
            mNameChangedListener = (OnNameChangedListener) parentFragment;
        else
            throw new RuntimeException(getParentFragment().toString()
                    + " must implement OnNameChangedListener");
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        AMCTSerializableObject object = new AMCTSerializableObject(mCurrNode);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, object);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        mRootView=inflater.inflate(R.layout.fragment_changing_names_dialog, container, false);
        Funcs.SetObjectFromBundle(savedInstanceState, this );
        inflateViews();
        initViews();
        return mRootView;
    }

    private void initViews() {
        if(mCurrNode!=null) {
            mCurrId = mCurrNode.hashCode();
            mNameET.setText(Manager_MCNames.getInstance().getNameById(mCurrId));
            mSaveBttn.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    mNewName= String.valueOf(mNameET.getText());
                    Funcs.runMapCoreFunc(new Runnable() {
                        @Override
                        public void run() {
                            Manager_MCNames.getInstance().setName(mCurrNode,mNewName);
                            mFullName= Manager_MCNames.getInstance().getNameByObject(mCurrNode);
                            getActivity().runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    mNameChangedListener.onNameChanged();
                                    dismiss();
                                }
                            });

                        }
                    });

                }
            });
        }
    }

    private void inflateViews() {
        mNameET=(EditText)mRootView.findViewById(R.id.changing_names_et);
        mSaveBttn=(Button)mRootView.findViewById(R.id.changing_names_save_bttn);
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
    public void setObject(Object currNode) {
        mCurrNode=currNode;
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

    public interface OnNameChangedListener
    {
        void onNameChanged();
    }
}
