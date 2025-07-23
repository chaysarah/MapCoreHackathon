package com.elbit.mapcore.mcandroidtester.utils.customviews;

import android.app.Activity;
import android.content.Context;
import android.util.AttributeSet;
import android.view.ContextThemeWrapper;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.LinearLayout;

import com.elbit.mapcore.Classes.Map.McMapLayer;
import com.elbit.mapcore.Enums.EMcPointCoordSystem;
import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.Map.IMcMapLayer;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

public class TilingScheme extends LinearLayout {

    private Context mContext;
    private SpinnerWithLabel mTilingSchemeTypeSWL;
    private TwoDVector mTilingOrigin2DVector;
    private NumericEditTextLabel mLargestTileSizeInMapUnitsET;
    private NumericEditTextLabel mNumLargestTilesXET;
    private NumericEditTextLabel mNumLargestTilesYET;
    private NumericEditTextLabel mTileSizeInPixelsET;
    private NumericEditTextLabel mRasterTileMarginInPixelsET;

    public TilingScheme(Context context, AttributeSet attrs) {
        super(context, attrs);
        LayoutInflater inflater = (LayoutInflater) context
                .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        inflater.inflate(R.layout.cv_tiling_scheme, this);
        mContext = context;

        if (!isInEditMode()) {
            initViews();
        }
    }

    private void initViews() {
        initTilingSchemeType();
        mTilingOrigin2DVector = (TwoDVector) findViewById(R.id.tiling_scheme_tiling_origin_2dvector);
        mLargestTileSizeInMapUnitsET = (NumericEditTextLabel) findViewById(R.id.tiling_scheme_largest_tile_size_in_map_units);
        mNumLargestTilesXET = (NumericEditTextLabel) findViewById(R.id.tiling_scheme_largest_tiles_x);
        mNumLargestTilesYET = (NumericEditTextLabel) findViewById(R.id.tiling_scheme_largest_tiles_y);
        mTileSizeInPixelsET = (NumericEditTextLabel) findViewById(R.id.tiling_scheme_tile_size_in_pixels);
        mRasterTileMarginInPixelsET = (NumericEditTextLabel) findViewById(R.id.tiling_scheme_raster_tile_margin_in_pixels);
    }

    private void initTilingSchemeType() {
        mTilingSchemeTypeSWL = (SpinnerWithLabel) findViewById(R.id.tiling_scheme_type_swl);
        mTilingSchemeTypeSWL.setAdapter(new ArrayAdapter<>(getContext(), android.R.layout.simple_spinner_item, IMcMapLayer.ETilingSchemeType.values()));
        mTilingSchemeTypeSWL.setSelection(IMcMapLayer.ETilingSchemeType.ETST_MAPCORE.getValue());
        mTilingSchemeTypeSWL.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                IMcMapLayer.ETilingSchemeType tilingSchemeType = ( IMcMapLayer.ETilingSchemeType) mTilingSchemeTypeSWL.getSelectedItem();
                SetTilingSchemeByType(tilingSchemeType);
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });
    }

    public void SetTilingSchemeByType(final IMcMapLayer.ETilingSchemeType tilingSchemeType) {
        Funcs.runMapCoreFunc(new Runnable() {
            @Override
            public void run() {
                try {
                    final IMcMapLayer.STilingScheme tilingScheme = McMapLayer.Static.GetStandardTilingScheme(tilingSchemeType);
                    Activity activity = null;

                    if (mContext instanceof Activity) {
                        activity = ((Activity) mContext);
                    } else if (mContext instanceof ContextThemeWrapper) {
                        Context context = ((ContextThemeWrapper) mContext).getBaseContext();
                        activity = (Activity) context;
                    }
                    if (activity != null) {
                        activity.runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                if (tilingScheme != null) {
                                    mTilingOrigin2DVector.setVector2D(tilingScheme.TilingOrigin);
                                    mLargestTileSizeInMapUnitsET.setDouble(tilingScheme.dLargestTileSizeInMapUnits);
                                    mTileSizeInPixelsET.setUInt(tilingScheme.uTileSizeInPixels);
                                    mRasterTileMarginInPixelsET.setUInt(tilingScheme.uRasterTileMarginInPixels);
                                    mNumLargestTilesXET.setUInt(tilingScheme.uNumLargestTilesX);
                                    mNumLargestTilesYET.setUInt(tilingScheme.uNumLargestTilesY);
                                }
                            }
                        });
                    }
                } catch (MapCoreException e) {
                    AlertMessages.ShowMapCoreErrorMessage(getContext(), e, "GetStandardTilingScheme");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });

    }

    public void SetETilingSchemeType(IMcMapLayer.ETilingSchemeType tilingSchemeType)
    {
        mTilingSchemeTypeSWL.setSelection(tilingSchemeType.getValue());
    }

    public IMcMapLayer.ETilingSchemeType GetETilingSchemeType()
    {
        return (IMcMapLayer.ETilingSchemeType) mTilingSchemeTypeSWL.getSelectedItem();
    }

    public IMcMapLayer.STilingScheme getTilingScheme() {
        IMcMapLayer.STilingScheme mTilingScheme = new IMcMapLayer.STilingScheme();
        mTilingScheme.TilingOrigin = mTilingOrigin2DVector.getVector2D();
        mTilingScheme.dLargestTileSizeInMapUnits = mLargestTileSizeInMapUnitsET.getDouble();
        mTilingScheme.uNumLargestTilesX = mNumLargestTilesXET.getUInt();
        mTilingScheme.uNumLargestTilesY = mNumLargestTilesYET.getUInt();
        mTilingScheme.uTileSizeInPixels = mTileSizeInPixelsET.getUInt();
        mTilingScheme.uRasterTileMarginInPixels = mRasterTileMarginInPixelsET.getUInt();
        return mTilingScheme;
    }

  /*  public void SetETilingSchemeTypeByValue(DNSTilingScheme tilingScheme)
    {
        if (tilingScheme != null)
        {
            DNSTilingScheme tilingScheme1 = DNMcMapLayer.GetStandardTilingScheme(DNETilingSchemeType._ETST_MAPCORE);
            DNSTilingScheme tilingScheme2 = DNMcMapLayer.GetStandardTilingScheme(DNETilingSchemeType._ETST_GLOBAL_LOGICAL);
            DNSTilingScheme tilingScheme3 = DNMcMapLayer.GetStandardTilingScheme(DNETilingSchemeType._ETST_GOOGLE_CRS84_QUAD);
            DNSTilingScheme tilingScheme4 = DNMcMapLayer.GetStandardTilingScheme(DNETilingSchemeType._ETST_GOOGLE_MAPS_COMPATIBLE);

            if (CompareTilingScheme(tilingScheme, tilingScheme1))
                cmbETilingSchemeType.Text = DNETilingSchemeType._ETST_MAPCORE.ToString();
            else if (CompareTilingScheme(tilingScheme, tilingScheme2))
                cmbETilingSchemeType.Text = DNETilingSchemeType._ETST_GLOBAL_LOGICAL.ToString();
            else if (CompareTilingScheme(tilingScheme, tilingScheme3))
                cmbETilingSchemeType.Text = DNETilingSchemeType._ETST_GOOGLE_CRS84_QUAD.ToString();
            else if (CompareTilingScheme(tilingScheme, tilingScheme4))
                cmbETilingSchemeType.Text = DNETilingSchemeType._ETST_GOOGLE_MAPS_COMPATIBLE.ToString();
            else
                cmbETilingSchemeType.Text = "";
        }
    }

    private bool CompareTilingScheme(DNSTilingScheme tilingScheme1, DNSTilingScheme tilingScheme2)
    {
        return (tilingScheme1.dLargestTileSizeInMapUnits == tilingScheme2.dLargestTileSizeInMapUnits &&
                tilingScheme1.TilingOrigin == tilingScheme2.TilingOrigin &&
                tilingScheme1.uRasterTileMarginInPixels == tilingScheme2.uRasterTileMarginInPixels &&
                tilingScheme1.uTileSizeInPixels == tilingScheme2.uTileSizeInPixels);
    }

    public void SetTilingScheme(DNSTilingScheme tilingScheme)
    {
        if (tilingScheme != null)
        {
            ctrl2DVectorTilingOrigin.Vector2D = tilingScheme.TilingOrigin;
            ntxLargestTileSizeInMapUnits.SetDouble(tilingScheme.dLargestTileSizeInMapUnits);
            ntxTileSizeInPixels.SetUInt32(tilingScheme.uTileSizeInPixels);
            ntxRasterTileMarginInPixels.SetUInt32(tilingScheme.uRasterTileMarginInPixels);
            ntxNumLargestTilesX.SetUInt32(tilingScheme.uNumLargestTilesX);
            ntxNumLargestTilesY.SetUInt32(tilingScheme.uNumLargestTilesY);
        }
    }

    public void SetStandardTilingScheme()
    {
        if (cmbETilingSchemeType.Text != "")
        {
            DNETilingSchemeType type = GetETilingSchemeType();
            SetTilingScheme(DNMcMapLayer.GetStandardTilingScheme(type));
        }
    }
*/

}