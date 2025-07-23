package com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.layers_tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.elbit.mapcore.Interfaces.Map.IMcRasterMapLayer;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.interfaces.FragmentWithObject;
import com.elbit.mapcore.mcandroidtester.utils.AMCTSerializableObject;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link RasterMapLayerFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link RasterMapLayerFragment#newInstance} factory method to
 * create an instance of this fragment.
 *
 */
public class RasterMapLayerFragment extends Fragment implements FragmentWithObject{

    private OnFragmentInteractionListener mListener;
    private IMcRasterMapLayer mRasterMapLayer;
    private View mHistogramRET;
    private View mHistogramGET;
    private View mHistogramBET;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment NativeRasterMapLayerFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static RasterMapLayerFragment newInstance() {
        RasterMapLayerFragment fragment = new RasterMapLayerFragment();
        return fragment;
    }
    public RasterMapLayerFragment() {
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
        View view= inflater.inflate(R.layout.fragment_raster_map_layer, container, false);
        Funcs.SetObjectFromBundle(savedInstanceState, this );
        initBttn(view);
        initEditTxts(view);

        return view;
    }

    private void initEditTxts(View view) {
        mHistogramRET=view.findViewById(R.id.layer_histogram_R);
        mHistogramGET=view.findViewById(R.id.layer_histogram_G);
        mHistogramBET=view.findViewById(R.id.layer_histogram_B);

    }

    private void initBttn(View view) {
   /*     Button calculateBttn= (Button) view.findViewById(R.id.layer_histogram_calculate_bttn);
        calculateBttn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {


                long[][] histogram = null;
                try
                {
                    mRasterMapLayer.CalcHistogram(histogram);

                    if (histogram != null)
                    {
                        for (int i = 0; i < 256; i++)
                        {
                            txtCalcHistogramR.Text += histogram[0][i] + ",";
                            txtCalcHistogramG.Text += histogram[1][i] + ",";
                            txtCalcHistogramB.Text += histogram[2][i] + ",";
                        }
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("CalcHistogram", McEx);
                }
            }
            }
        });*/

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
           // throw new RuntimeException(context.toString()
            //        + " must implement OnFragmentInteractionListener");
        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    @Override
    public void setObject(Object obj) {
        mRasterMapLayer = (IMcRasterMapLayer) obj;
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putSerializable(AMCTSerializableObject.AMCT_SERIALIZABLE_OBJECT, new AMCTSerializableObject(mRasterMapLayer));
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
