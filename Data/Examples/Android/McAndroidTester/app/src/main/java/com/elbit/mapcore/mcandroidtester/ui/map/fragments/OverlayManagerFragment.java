package com.elbit.mapcore.mcandroidtester.ui.map.fragments;

import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import androidx.annotation.Nullable;
import com.google.android.material.tabs.TabLayout;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ListView;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.managers.ObjectWorld.Manager_MCOverlayManager;
import com.elbit.mapcore.mcandroidtester.model.AMCTViewPort;
import com.elbit.mapcore.mcandroidtester.ui.map.activities.OverlayManagerActivity;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.ui.adapters.HashMapAdapter;

import java.util.Map;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcOverlayManager;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link OverlayManagerFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link OverlayManagerFragment#newInstance} factory method to
 * create an instance of this fragment.
 *
 */
public class OverlayManagerFragment extends Fragment/* implements OnCreateCoordinateSystemListener*/{
    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;

    private OnFragmentInteractionListener mListener;
    private ListView mOverlayManagerLV;
    private HashMapAdapter mOverlayManagerAdapter;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment OverlayManagerFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static OverlayManagerFragment newInstance() {
        OverlayManagerFragment fragment = new OverlayManagerFragment();
        return fragment;
    }
    public OverlayManagerFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View inflaterView=inflater.inflate(R.layout.fragment_overlay_manager, container, false);
        initFinishBttn(inflaterView);
        initOverlayManagerBttn(inflaterView);
        initOverlayManagerLV(inflaterView);
        // Inflate the layout for this fragment
        return inflaterView;
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
        } else {
            throw new RuntimeException(context.toString()
                    + " must implement OnFragmentInteractionListener");
        }
    }

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
    }

    private void initFinishBttn(View inflaterView) {
        inflaterView.findViewById(R.id.finish_overlay_manager_bttn).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                ((TabLayout)getActivity().findViewById(R.id.tabs)).getTabAt(Consts.SETTINGS_TAB_INDEX).select();
            }
        });
    }

    private void initOverlayManagerBttn(View inflaterView) {
        inflaterView.findViewById(R.id.create_overlay_manager_bttn).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent=new Intent(OverlayManagerFragment.this.getActivity(),OverlayManagerActivity.class);
                startActivity(intent);
            }
        });
    }

    private void initOverlayManagerLV(View inflaterView) {
        mOverlayManagerLV=(ListView)(inflaterView.findViewById(R.id.overlay_manager_lv));
        mOverlayManagerLV.setChoiceMode(ListView.CHOICE_MODE_SINGLE);
        mOverlayManagerAdapter=new HashMapAdapter(getActivity(), Manager_MCOverlayManager.getInstance().getAllParams(),Consts.ListType.SINGLE_CHECK);
        mOverlayManagerLV.setAdapter(null);
        mOverlayManagerLV.setAdapter(mOverlayManagerAdapter);
        mOverlayManagerLV.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                Map.Entry<Object, Integer> item = (Map.Entry<Object, Integer>) mOverlayManagerLV.getAdapter().getItem(position);
                AMCTViewPort.getViewportInCreation().setOverlayManager((IMcOverlayManager) item.getKey());
                     }
        });
        selectCurOverlayManager();
    }

    private void selectCurOverlayManager() {
            if(AMCTViewPort.getViewportInCreation().getmOverlayManager()!=null)
            {int i;
                for(i=0;i<mOverlayManagerAdapter.getCount();i++)
                {
                    if(mOverlayManagerAdapter.getItem(i).getKey().equals(AMCTViewPort.getViewportInCreation().getmOverlayManager()))
                        mOverlayManagerLV.setItemChecked(i,true);



                }

            }
    }


    @Override
    public void setUserVisibleHint(boolean visible)
    {
        super.setUserVisibleHint(visible);
        if (visible && isResumed())
        {
            //Only manually call onResume if fragment is already visible
            //Otherwise allow natural fragment lifecycle to call onResume
            onResume();
        }
    }
    @Override
    public void onResume() {
        super.onResume();
        if (!getUserVisibleHint())
        {
            return;
        }
        if (mOverlayManagerAdapter != null && mOverlayManagerLV != null) {
            initOverlayManagerLV(getView());
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
