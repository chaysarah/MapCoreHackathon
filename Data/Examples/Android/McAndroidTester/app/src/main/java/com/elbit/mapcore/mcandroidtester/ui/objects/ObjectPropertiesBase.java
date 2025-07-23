package com.elbit.mapcore.mcandroidtester.ui.objects;


import android.util.SparseBooleanArray;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.List;
import java.util.Observable;

import com.elbit.mapcore.Classes.OverlayManager.McTexture;
import com.elbit.mapcore.Enums.EAxisXAlignment;
import com.elbit.mapcore.Enums.EMcPointCoordSystem;
import com.elbit.mapcore.General.CMcEnumBitField;
import com.elbit.mapcore.Interfaces.Calculations.IMcSpatialQueries;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcClosedShapeItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcConditionalSelector;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcFont;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcLineBasedItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcLogFont;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcMesh;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcPictureItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcProperty;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcRectangleItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcSightPresentationItemParams;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcSymbolicItem;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcTextItem;
import com.elbit.mapcore.Structs.SMcBColor;
import com.elbit.mapcore.Structs.SMcFVector2D;
import com.elbit.mapcore.Structs.SMcFVector3D;
import com.elbit.mapcore.Structs.SMcSubItemData;
import com.elbit.mapcore.Structs.SMcVariantString;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.General.Constants;

/**
 * Created by tc99382 on 12/01/2017.
 */
public class ObjectPropertiesBase {
    ObjectPropertiesBase()
    {
    }
    public static boolean Grid_IsUsingBasicItemPropertiesOnly = true;

    public static ArrayList<SMcSubItemData> mSubItemsData = new ArrayList<>();
    public static float mMoveIfBlockedMaxChange = 0;
    public static float mMoveIfBlockedHeightAboveObstacle = 0;
    public static short mBlockedTransparency = 0;
    public static IMcConditionalSelector.EActionOptions mTransformOption = IMcConditionalSelector.EActionOptions.EAO_USE_SELECTOR;
    public static IMcConditionalSelector.EActionOptions mVisibilityOption = IMcConditionalSelector.EActionOptions.EAO_USE_SELECTOR;
    private static SparseBooleanArray mItemSubTypeFlags = new SparseBooleanArray(IMcObjectSchemeItem.EItemSubTypeFlags.values().length);
    public static SparseBooleanArray getItemSubTypeFlags()
    {
        if(mItemSubTypeFlags.size() == 0) {
            mItemSubTypeFlags = new SparseBooleanArray(IMcObjectSchemeItem.EItemSubTypeFlags.values().length);
            mItemSubTypeFlags.put(IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_WORLD.ordinal(), false);
            mItemSubTypeFlags.put(IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN.ordinal(), true);
            mItemSubTypeFlags.put(IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_ATTACHED_TO_TERRAIN.ordinal(), false);
            mItemSubTypeFlags.put(IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_ACCURATE_3D_SCREEN_WIDTH.ordinal(), false);
        }
        return mItemSubTypeFlags;
    }
    public static void setItemSubTypeFlags(SparseBooleanArray itemSubTypeFlags)
    {
        mItemSubTypeFlags = itemSubTypeFlags;
    }

    public static EMcPointCoordSystem mLocationCoordSys = EMcPointCoordSystem.EPCS_WORLD;
    public static boolean mLocationRelativeToDtm = false;
    public static int mLocationMaxPoints = 0;
    public static SMcFVector2D mLineTextureHeightRange = new SMcFVector2D(0, -1);

    //Symbolic Item - General
    public static IMcSymbolicItem.EDrawPriorityGroup mDrawPriorityGroup = IMcSymbolicItem.EDrawPriorityGroup.EDPG_REGULAR;
    public static byte mConplanar3dPriority = 0;
    public static byte mDrawPriority = 0;
    public static short mTransparency = 255;
    public static float mMoveIfBlockedMax = 255;
    public static float mMoveIfBlockedHeight = 255;

    // Symbolic Item - Attach point
    public static IMcSymbolicItem.EAttachPointType mAttachPointType = IMcSymbolicItem.EAttachPointType.EAPT_ALL_POINTS;
    public static int mAttachPointIndex = 0;
    public static int mAttachPointNumPoints = 1;
    public static CMcEnumBitField<IMcSymbolicItem.EBoundingBoxPointFlags> mBoundingBoxType = new CMcEnumBitField<>(IMcSymbolicItem.EBoundingBoxPointFlags.EBBPF_CENTER);
    public static float mAttachPointPositionValue = 1;

    // Symbolic Item - transform param
    public static int mVectorTransformSegment = 0;

    // Symbolic Item - offset
    public static IMcSymbolicItem.EOffsetOrientation mOrientation = IMcSymbolicItem.EOffsetOrientation.EOO_RELATIVE_TO_PARENT_ROTATION;
    public static IMcSymbolicItem.EVectorOffsetCalc mVectorOffsetCalc = IMcSymbolicItem.EVectorOffsetCalc.EVOC_PARALLEL_DISTANCE;
    public static float mVectorOffsetValue = 0;
    public static SMcFVector3D mOffset = new SMcFVector3D();;
    public static IMcProperty.SArrayProperty<Integer> mPointsIndicesAndDuplications = null;
    public static IMcProperty.SArrayProperty<SMcFVector3D> mPointsOffset = null;

    // Symbolic Item - Rotation
    public static float mRotationYaw = 0;
    public static float mRotationPitch = 0;
    public static float mRotationRoll = 0;
    public static boolean mVectorRotation = false;

    //Line Base Member
    public static float mLineTextureScale = 1;
    public static float mLineWidth = 2;
    public static SMcBColor mLineColor = new SMcBColor(255, 255, 0, 255);
    public static IMcLineBasedItem.ELineStyle mLineStyle = IMcLineBasedItem.ELineStyle.ELS_SOLID;
    public static McTexture mLineTexture = null;
    public static short mLineBasedSmoothingLevels = 0;
    public static float mLineBasedGreatCirclePrecision = 0;
    public static float mLineOutlineWidth;
    public static SMcBColor mLineOutlineColor = new SMcBColor(0, 0, 0, 0);
    public static IMcLineBasedItem.EShapeType mShapeType = IMcLineBasedItem.EShapeType.EST_2D;
    public static IMcLineBasedItem.EPointOrderReverseMode mLineTextureFlipMode = IMcLineBasedItem.EPointOrderReverseMode.EPORM_NONE;

    //Closed Shape
    public static SMcFVector2D mSidesFillTextureScale = new SMcFVector2D(1,1);
    public static SMcFVector2D mFillTextureScale = new SMcFVector2D(1,1);
    public static SMcBColor mFillColor = new SMcBColor(0, 128, 255, 255);
    public static IMcClosedShapeItem.EFillStyle mFillStyle = IMcClosedShapeItem.EFillStyle.EFS_SOLID;
    public static McTexture mFillTexture = null;
    public static SMcBColor mSidesFillColor = new SMcBColor(192, 192, 192, 255);
    public static IMcClosedShapeItem.EFillStyle mSidesFillStyle = IMcClosedShapeItem.EFillStyle.EFS_SOLID;
    public static McTexture mSidesFillTexture = null;
    public static float mVerticalHeight = 50;

    //Ellipse Tab
    public static IMcObjectSchemeItem.EGeometryType mEllipseType = IMcObjectSchemeItem.EGeometryType.EGT_GEOMETRIC_IN_OVERLAY_MANAGER;
    public static EMcPointCoordSystem mEllipseCoordSys = EMcPointCoordSystem.EPCS_WORLD;
    public static float mEllipseInnerRadiusFactor = 0;
    public static float mEllipseStartAngle = 0;
    public static float mEllipseEndAngle = 360;
    public static float mEllipseRadiusX = 0;
    public static float mEllipseRadiusY= 0;

    public static IMcObjectSchemeItem.EEllipseDefinition mEllipseDefinition = IMcObjectSchemeItem.EEllipseDefinition.EED_ELLIPSE_CENTER_RADIUSES_ANGLES;

    //Arc Member
    public static EMcPointCoordSystem mArcCoordSys = EMcPointCoordSystem.EPCS_WORLD;
    public static IMcObjectSchemeItem.EGeometryType mArcEllipseType = IMcObjectSchemeItem.EGeometryType.EGT_GEOMETRIC_IN_OVERLAY_MANAGER;
    public static float mArcStartAngle = 0;
    public static float mArcEndAngle = 180;
    public static float mArcRadiusX = 0;
    public static float mArcRadiusY = 0;

    public static IMcObjectSchemeItem.EEllipseDefinition mArcEllipseDefinition = IMcObjectSchemeItem.EEllipseDefinition.EED_ELLIPSE_CENTER_RADIUSES_ANGLES;

    //rectangle
    public static IMcRectangleItem.ERectangleDefinition mRectangleDefinition = IMcRectangleItem.ERectangleDefinition.ERD_RECTANGLE_DIAGONAL_POINTS;
    public static EMcPointCoordSystem mRectangleCoordSys = EMcPointCoordSystem.EPCS_WORLD;
    public static IMcObjectSchemeItem.EGeometryType mRectangleType = IMcObjectSchemeItem.EGeometryType.EGT_GEOMETRIC_IN_OVERLAY_MANAGER;
    public static float mRectRadiusX=0;
    public static float mRectRadiusY=0;

    //Text Member
    public static class Text extends Observable{
        public boolean mTextRtlReadingOrder = false;
        public EAxisXAlignment mTextAlignment = EAxisXAlignment.EXA_CENTER;
        public IMcFont mTextFont = null;
        public String mTextString = "";
        public SMcFVector2D mTextScale = new SMcFVector2D(1, 1);
        public int mTextMargin = 0;
        public int mTextMarginY = Constants.UINT_MAX;
        public SMcBColor mTextColor = new SMcBColor(255, 255, 0, 255);
        public SMcBColor mTextBackgroundColor = new SMcBColor(0, 128, 255, 255);
        public boolean mTextIsUnicode = false;
        public SMcBColor mTextOutlineColor = new SMcBColor(0, 0, 0, 0);
        public IMcTextItem.ENeverUpsideDownMode mNeverUpsideDown = IMcTextItem.ENeverUpsideDownMode.ENUDM_NONE;
        public IMcTextItem.EBackgroundShape mBackgroundShape = IMcTextItem.EBackgroundShape.EBS_RECTANGLE;
        public IMcSymbolicItem.EBoundingBoxPointFlags mTextRectAlignment = IMcSymbolicItem.EBoundingBoxPointFlags.EBBPF_CENTER;

        public IMcLogFont.SLogFontToTtfFile[] mTextFontsMap;
        public IMcLogFont.SLogFontToTtfFile mTextCurMapping = null;
        public PreviousFragmentText mPreviousFragmentText = PreviousFragmentText.ObjectProperties;
        public int mTextCurMappingPos = -1;
        public boolean mIsWaitForCreate = false;

        public void updateTextCompleted()
        {
            setChanged();
            notifyObservers();
        }
        private static Text instance;
        public static Text getInstance()
        {
            if(instance==null)
                instance=new Text();
            return instance;
        }

        public enum PreviousFragmentText
        {
            PropertiesList,
            CreateNewText,
            ObjectProperties;
        }
    }

    //Arrow Member
    public static EMcPointCoordSystem mArrowCoordSys = EMcPointCoordSystem.EPCS_WORLD;
    public static float mArrowHeadAngle = 45;
    public static float mArrowHeadSize = 10;
    public static float mArrowGapSize = 0;

    //Picture Member
    public static boolean mPictureIsSizeFactor = true;
    public static boolean mPictureNeverUpsideDown = false;
    public static boolean mPictureIsUseTextureGeoReferencing = false;
    public static float mPicWidth = 1;
    public static float mPicHeight = 1;
    public static McTexture mPicTexture=null;
    public static SMcBColor mPicTextureColor=new SMcBColor(255,255,255,255);
    public static IMcSymbolicItem.EBoundingBoxPointFlags mPicRectAlignment = IMcSymbolicItem.EBoundingBoxPointFlags.EBBPF_CENTER;

    //Line Expansion cTor
    public static EMcPointCoordSystem mLineExpansionCoordinateSystem = EMcPointCoordSystem.EPCS_WORLD;
    public static float mLineExpansionRadius = 1;
    public static IMcObjectSchemeItem.EGeometryType mLineExpansionType = IMcObjectSchemeItem.EGeometryType.EGT_GEOGRAPHIC;


    public static class SightPresentation {
        public static IMcSpatialQueries.EPointVisibility colorPointVisibility = IMcSpatialQueries.EPointVisibility.EPV_SEEN;
        public static HashMap<IMcSpatialQueries.EPointVisibility, SMcBColor> colorsByVisibility = new HashMap<>();

        public static SMcBColor color = new SMcBColor();
        public static IMcSightPresentationItemParams.ESightPresentationType type = IMcSightPresentationItemParams.ESightPresentationType.ESPT_NONE;
        public static boolean isObserverHeightAbsolute = false;
        public static boolean isObservedHeightAbsolute = false;
        public static IMcSpatialQueries.EQueryPrecision precision = IMcSpatialQueries.EQueryPrecision.EQP_HIGHEST;
        public static int numEllipseRays = 64;
        public static float observedHeight = 1.7F;
        public static float observerHeight = 1.7F;
        public static int minPitch = -90;
        public static int maxPitch = 90;
        public static float sightTextureResolution = 20;

    }

    public static IMcMesh mMesh=null;

}
