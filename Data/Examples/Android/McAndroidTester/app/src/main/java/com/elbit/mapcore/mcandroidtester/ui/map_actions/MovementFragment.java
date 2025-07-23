package com.elbit.mapcore.mcandroidtester.ui.map_actions;

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
import android.widget.ImageButton;

import com.elbit.mapcore.General.ObjectRef;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_AMCTMapForm;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Map.IMcMapCamera;
import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;
import com.elbit.mapcore.Structs.SMcVector3D;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link MovementFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link MovementFragment#newInstance} factory method to
 * create an instance of this fragment.
 *
 */
public class MovementFragment extends DialogFragment {

    private View mRootView;
    private Button mLoweringCameraBtn;
    private Button mRaisingCameraBtn;
    private ImageButton mRotateRightBtn;
    private ImageButton mRotateLeftBtn;
    private ImageButton mRotateAlignBtn;
    private ImageButton mMoveLeftBtn;
    private ImageButton mMoveRightBtn;
    private ImageButton mForwardBtn;
    private ImageButton mBackwardBtn;
    private int mMoveFactor3D = 40;
    private float mCurrAngle;
    private OnFragmentInteractionListener mListener;

    private EditText mMoveFactorET;
    private EditText mRotateFactorET;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @return A new instance of fragment MovementFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static MovementFragment newInstance() {
        MovementFragment fragment = new MovementFragment();
        return fragment;
    }
    public MovementFragment() {
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
        mRootView = inflater.inflate(R.layout.fragment_movement, container, false);
        mLoweringCameraBtn = (Button) mRootView.findViewById(R.id.movement_lowering_camera_btn);
        mRaisingCameraBtn= (Button) mRootView.findViewById(R.id.movement_raising_camera_btn);
        mRotateRightBtn = (ImageButton) mRootView.findViewById(R.id.movement_rotate_right_btn);
        mRotateLeftBtn = (ImageButton) mRootView.findViewById(R.id.movement_rotate_left_btn);
        mRotateAlignBtn = (ImageButton) mRootView.findViewById(R.id.movement_rotate_align_btn);
        mMoveLeftBtn = (ImageButton) mRootView.findViewById(R.id.movement_move_left_btn);
        mMoveRightBtn = (ImageButton) mRootView.findViewById(R.id.movement_move_right_btn);
        mForwardBtn = (ImageButton) mRootView.findViewById(R.id.movement_forward_camera_btn);
        mBackwardBtn = (ImageButton) mRootView.findViewById(R.id.movement_backward_camera_btn);
        mRotateFactorET = (EditText) mRootView.findViewById(R.id.movement_rotate_factor_et);
        mMoveFactorET = (EditText) mRootView.findViewById(R.id.movement_move_factor_et);
        mRotateFactorET.setText("2");
        mMoveFactorET.setText("5");
        try {
            if (Manager_AMCTMapForm.getInstance().getCurViewport().GetMapType() == IMcMapCamera.EMapType.EMT_2D)
            {
                mLoweringCameraBtn.setEnabled(false);
                mRaisingCameraBtn.setEnabled(false);
            }
        }
        catch (MapCoreException e){
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetMapType");
        }
        catch (Exception ex){
            ex.printStackTrace();
        }

        mLoweringCameraBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                ExecuteAction(R.id.movement_lowering_camera_btn);
            }
        });
        mRaisingCameraBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                ExecuteAction(R.id.movement_raising_camera_btn);
            }
        });
        mRotateRightBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                ExecuteAction(R.id.movement_rotate_right_btn);
            }
        });
        mRotateLeftBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                ExecuteAction(R.id.movement_rotate_left_btn);
            }
        });
        mRotateAlignBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                ExecuteAction(R.id.movement_rotate_align_btn);
            }
        });
        mMoveLeftBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                ExecuteAction(R.id.movement_move_left_btn);
            }
        });
        mMoveRightBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                ExecuteAction(R.id.movement_move_right_btn);
            }
        });
        mForwardBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                ExecuteAction(R.id.movement_forward_camera_btn);
            }
        });
        mBackwardBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                ExecuteAction(R.id.movement_backward_camera_btn);
            }
        });

        return mRootView;
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

    public void RotateMap(final float RotateDirection) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    mCurrAngle += RotateDirection;
                    IMcMapViewport currViewport = Manager_AMCTMapForm.getInstance().getCurViewport();
                    currViewport.GetActiveCamera().SetCameraOrientation(mCurrAngle, false);
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "SetCameraOrientation");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    //Scrool camera forward\backward and to the sides
    public void MoveCamera(int deltaX, int deltaY, int deltaZ) {
        int deltaX3D = deltaX * mMoveFactor3D;
        int deltaY3D = deltaY * mMoveFactor3D;
        int deltaZ3D = deltaZ * mMoveFactor3D;
        final SMcVector3D VectorDelta = new SMcVector3D(deltaX3D, deltaY3D, deltaZ3D);
        final IMcMapViewport currViewport = Manager_AMCTMapForm.getInstance().getCurViewport();

        if (currViewport == null) {
            return;
        }
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    if (currViewport.GetMapType() == IMcMapCamera.EMapType.EMT_2D) {
                        currViewport.MoveCameraRelativeToOrientation(VectorDelta, false);
                    } else if (currViewport.GetMapType() == IMcMapCamera.EMapType.EMT_3D) {
                        currViewport.MoveCameraRelativeToOrientation(VectorDelta);
                    }
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "MoveCameraRelativeToOrientation");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private float getRotateMapDelta()
    {
        String value = mRotateFactorET.getText().toString();
        if(value != null && value != "")
            return Float.parseFloat(value);
        return 0f;
    }

    public int getMoveMapDelta()
    {
        String value = mMoveFactorET.getText().toString();
        if(value != null && value != "")
            return Integer.parseInt(value);
        return 0;
    }

    private void ExecuteAction(int nameBtn) {
        ObjectRef<Float> currYaw = new ObjectRef<>();
        try {
            Manager_AMCTMapForm.getInstance().getCurViewport().GetActiveCamera().GetCameraOrientation(currYaw);
        }
        catch (MapCoreException e){
            AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetCameraOrientation");}
        catch (Exception e) {
            e.printStackTrace();
        }
        mCurrAngle = currYaw.getValue();
        if(nameBtn == R.id.movement_backward_camera_btn) {
            MoveCamera(0, -getMoveMapDelta(), 0);
        }
        else if (nameBtn == R.id.movement_forward_camera_btn) {
            MoveCamera(0, getMoveMapDelta(), 0);
        }
        else if (nameBtn == R.id.movement_lowering_camera_btn) {
                MoveCamera(0, 0, -getMoveMapDelta());
        }
        else if (nameBtn == R.id.movement_move_left_btn) {
            MoveCamera(-getMoveMapDelta(), 0, 0);
        }
        else if (nameBtn == R.id.movement_move_right_btn) {
            MoveCamera(getMoveMapDelta(), 0, 0);
        }
        else if (nameBtn ==  R.id.movement_raising_camera_btn) {
            MoveCamera(0, 0, getMoveMapDelta());
        }
        else if (nameBtn == R.id.movement_rotate_align_btn) {
            RotateMap(-mCurrAngle);
        }
        else if (nameBtn == R.id.movement_rotate_left_btn) {
            RotateMap(-getRotateMapDelta());
        }
        else if (nameBtn == R.id.movement_rotate_right_btn) {
            RotateMap(getRotateMapDelta());
        }
    }
}
