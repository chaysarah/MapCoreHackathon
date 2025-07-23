using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace MCTester.Managers.ObjectWorld
{
    public static class ObjectPropertiesBase
    {

        #region Data Memmber

        //Line Member
        //Polygon Member
        //Light Member

        //General Member
        static DNEItemSubTypeFlags m_ItemSubTypeFlags;
        static DNEMcPointCoordSystem m_LocationCoordSys;
        static bool m_LocationRelativeToDtm;
        static bool m_VerifyPrivatePropertiesId;
        static DNSMcFVector2D m_LineTextureHeightRange;
        static IDNMcImageCalc m_ImageCalc;
        static DNEDrawPriorityGroup m_DrawPriorityGroup;

        //Line Base Member
        static float m_LineWidth;
        static DNSMcBColor m_LineColor;
        static DNELineStyle m_LineStyle;
        static IDNMcTexture m_LineTexture;
        static byte m_LineBasedSmoothingLevels;
        static float m_LineBasedGreatCirclePrecision;
        static float m_LineOutlineWidth;
        static DNSMcBColor m_LineOutlineColor;
        static DNSMcBColor m_SidesFillColor;
        static DNEFillStyle m_SidesFillStyle;
        static IDNMcTexture m_SidesFillTexture;
        static float m_VerticalHeight;
        static DNEPointOrderReverseMode m_PointOrderReverseMode;

        //Closed Shape Member
        static DNEShapeType m_ShapeType;
        static DNSMcBColor m_FillColor;
        static IDNMcTexture m_FillTexture;
        static DNEFillStyle m_FillStyle;

        //Arrow Member
        static DNEMcPointCoordSystem m_ArrowCoordSys;
        static float m_ArrowHeadAngle;
        static float m_ArrowHeadSize;
        static float m_ArrowGapSize;

        //Arc Member
        static DNEMcPointCoordSystem m_ArcCoordSys;
        static DNEItemGeometryType m_ArcEllipseType;
        static float m_ArcStartAngle;
        static float m_ArcEndAngle;
        static DNEEllipseDefinition m_ArcEllipseDefinition;

        //Procedural GeometryCoordinate System Member
        static DNEMcPointCoordSystem m_ProceduralGeometryCoordinateSys;

        //Ellipse Member
        static DNEMcPointCoordSystem m_EllipseCoordSys;
        static DNEItemGeometryType m_EllipseType;
        static float m_EllipseStartAngle;
        static float m_EllipseEndAngle;
        static float m_EllipseInnerRadiusFactor;
        static DNEEllipseDefinition m_EllipseDefinition;

        //Rectangle Member
        static DNEMcPointCoordSystem m_RectangleCoordSys;
        static DNEItemGeometryType m_RectangleType;
        static DNERectangleDefinition m_RectangleDefinition;

        //Text Member
        static DNEMcPointCoordSystem m_TextCoordSys;
        static IDNMcFont m_TextFont;
        static DNENeverUpsideDownMode m_NeverUpsideDown;
        static DNSMcFVector2D m_TextScale;
        static uint m_TextMargin;
        static DNSMcBColor m_TextColor;
        static DNSMcBColor m_TextBackgroundColor;
        static bool m_TextIsUnicode;
        static DNSMcBColor m_TextOutlineColor;
        static uint m_TextMarginY;
        static DNEBackgroundShape m_BackgroundShape;

        //Picture Member
        static IDNMcTexture m_PictureTexture;
        static DNEMcPointCoordSystem m_PictureCoordSys;
        static bool m_PictureIsSizeFactor;
        static bool m_PictureIsUseTextureGeoReferencing;
        static float m_PicWidth;
        static float m_PicHeight;
        static DNEBoundingBoxPointFlags m_PicRectAlignmentBitField;
        static bool m_PictureNeverUpsideDown;

        //Line Expansion Member
        static DNEMcPointCoordSystem m_LineExpansionCoordinateSystem;
        static DNEItemGeometryType m_LineExpansionType;
        static float m_LineExpansionRadius;

        //Light
        static DNSMcFColor m_LightDiffuseColor;
        static DNSMcFColor m_LightSpecularColor;
        static DNSMcAttenuation m_LightAttenuation;
        static DNSMcFVector3D m_LightDirection;
        static float m_LightHalfOuterAngle;
        static float m_LightHalfInnerAngle;

        //Partical Effect
        static float m_ParticalEffectStartingTimePoint;
        static DNEParticleEffectState m_ParticalEffectState;
        static float m_ParticalEffectSartingDelay;

        //Projector
        static float m_ProjectorHalfFOVHorizAngle;
        static float m_ProjectorAspectRatio;
        static DNETargetTypesFlags m_ProjectorItemTargetTypeFlags;

        //Sound
        static bool m_SoundLoop;
        static float m_SoundStartingTimePoint;
        static DNESoundState m_SoundState;

        //Mesh
        static IDNMcMeshItem m_MeshFile;
        static bool m_MeshRotateToTerrain;
        static DNEBasePointAlignment m_MeshAlignment;
        static DNSMcBColor m_MeshTextureColor;
        static bool m_MeshParticipatesInTerrainHeight;
        static bool m_MeshUseExisting;
        static bool m_MeshIsStatic;
        static bool m_Meshcastshadows;
        static bool m_MeshDisplayItemsAttachedToTerrain;

        #endregion

        #region C-tor
        static ObjectPropertiesBase()
        {
            //General c-tor
            m_ItemSubTypeFlags = DNEItemSubTypeFlags._EISTF_SCREEN;
            m_LocationCoordSys = DNEMcPointCoordSystem._EPCS_WORLD;
            m_LocationRelativeToDtm = false;
            m_VerifyPrivatePropertiesId = false;
            m_LineTextureHeightRange = new DNSMcFVector2D(0, -1);
            m_ImageCalc = null;
            m_DrawPriorityGroup = DNEDrawPriorityGroup._EDPG_REGULAR;

            //Line Base c-tor
            m_LineWidth = 2;
            m_LineColor = new DNSMcBColor(255, 255, 0, 255);
            m_LineStyle = DNELineStyle._ELS_SOLID;
            m_LineTexture = null;
            m_FillStyle = DNEFillStyle._EFS_SOLID;
            m_LineBasedSmoothingLevels = 0;
            m_LineBasedGreatCirclePrecision = 0;
            m_LineOutlineColor = new DNSMcBColor(0, 0, 0, 0);
            m_SidesFillColor = DNSMcBColor.bcBlackOpaque;
            m_SidesFillStyle = DNEFillStyle._EFS_SOLID;
            m_SidesFillTexture = null;
            m_VerticalHeight = 1;
            m_ShapeType = DNEShapeType._EST_2D;
            m_PointOrderReverseMode = DNEPointOrderReverseMode._EPORM_NONE;

            //Closed Shape c-tor
            m_FillColor = new DNSMcBColor(0, 128, 255, 255);
            m_FillTexture = null;

            //Arrow c-tor
            m_ArrowCoordSys = DNEMcPointCoordSystem._EPCS_WORLD;
            m_ArrowHeadAngle = 45;
            m_ArrowHeadSize = 10;
            m_ArrowGapSize = 0;

            //Arc c-tor
            m_ArcCoordSys = DNEMcPointCoordSystem._EPCS_WORLD;
            m_ArcEllipseType = DNEItemGeometryType._EGT_GEOMETRIC_IN_OVERLAY_MANAGER;
            m_ArcStartAngle = 0;
            m_ArcEndAngle = 180;
            m_ArcEllipseDefinition = DNEEllipseDefinition._EED_ELLIPSE_CENTER_RADIUSES_ANGLES;

            //Ellipse c-tor
            m_EllipseType = DNEItemGeometryType._EGT_GEOMETRIC_IN_OVERLAY_MANAGER;
            m_EllipseCoordSys = DNEMcPointCoordSystem._EPCS_WORLD;
            m_EllipseStartAngle = 0;
            m_EllipseEndAngle = 360;
            m_EllipseInnerRadiusFactor = 0;
            m_EllipseDefinition = DNEEllipseDefinition._EED_ELLIPSE_CENTER_RADIUSES_ANGLES;

            //Rectangle c-tor
            m_RectangleCoordSys = DNEMcPointCoordSystem._EPCS_WORLD;
            m_RectangleDefinition = DNERectangleDefinition._ERD_RECTANGLE_DIAGONAL_POINTS;
            m_RectangleType = DNEItemGeometryType._EGT_GEOMETRIC_IN_OVERLAY_MANAGER;

            //Line Expansion cTor
            m_LineExpansionCoordinateSystem = DNEMcPointCoordSystem._EPCS_WORLD;
            m_LineExpansionType = DNEItemGeometryType._EGT_GEOGRAPHIC;
            m_LineExpansionRadius = 1;

            //Text c-tor
            m_TextCoordSys = DNEMcPointCoordSystem._EPCS_SCREEN;
            m_NeverUpsideDown = DNENeverUpsideDownMode._ENUDM_NONE;
            m_TextScale = new DNSMcFVector2D(1, 1);
            m_TextMargin = 0;
            m_TextMarginY = DNMcConstants._MC_EMPTY_ID;
            m_TextColor = new DNSMcBColor(255, 255, 0, 255);
            m_TextBackgroundColor = new DNSMcBColor(0, 128, 255, 255);
            m_TextIsUnicode = false;
            m_TextOutlineColor = new DNSMcBColor(0, 0, 0, 0);
            m_BackgroundShape = DNEBackgroundShape._EBS_RECTANGLE;

            //Picture c-tor
            m_PictureTexture = null;
            m_PictureCoordSys = DNEMcPointCoordSystem._EPCS_SCREEN;
            m_PictureIsSizeFactor = true;
            m_PictureIsUseTextureGeoReferencing = false;
            m_PicWidth = 1;
            m_PicHeight = 1;
            m_PicRectAlignmentBitField = DNEBoundingBoxPointFlags._EBBPF_CENTER;
            m_PictureNeverUpsideDown = false;

            //Mesh c-tor
            m_MeshFile = null;

            //Light c-tor
            m_LightDiffuseColor = DNSMcFColor.fcWhiteOpaque;
            m_LightSpecularColor = DNSMcFColor.fcBlackOpaque;
            m_LightDirection = new DNSMcFVector3D(0, 0, -1);
            m_LightAttenuation = new DNSMcAttenuation(1, 0, 0, float.MaxValue);
            m_LightHalfOuterAngle = 22.5f;
            m_LightHalfInnerAngle = 2.5f;

            //Partical Effect c-tor
            m_ParticalEffectStartingTimePoint = 0;
            m_ParticalEffectState = DNEParticleEffectState._ES_RUNNING;
            m_ParticalEffectSartingDelay = 0;

            //Projector c-tor
            m_ProjectorHalfFOVHorizAngle = 45;
            m_ProjectorAspectRatio = 1;
            m_ProjectorItemTargetTypeFlags = DNETargetTypesFlags._ETTF_DTM;

            //Sound c-tor
            m_SoundLoop = true;
            m_SoundStartingTimePoint = 0;
            m_SoundState = DNESoundState._ES_RUNNING;

            //Mesh c-tor
            m_MeshRotateToTerrain = false;
            m_MeshAlignment = DNEBasePointAlignment._EBPA_BOUNDING_BOX_CENTER_LOWERED;
            m_MeshTextureColor = new DNSMcBColor(255, 255, 255, 255);
            m_MeshParticipatesInTerrainHeight = false;
            m_MeshUseExisting = true;
            m_Meshcastshadows = true;
            m_MeshIsStatic = false;
            m_MeshDisplayItemsAttachedToTerrain = false;

            // ProceduralGeometry c-tor
            m_ProceduralGeometryCoordinateSys = DNEMcPointCoordSystem._EPCS_WORLD;
        }
        #endregion


        #region Public Properties

        //Line Properties
        //Polygon Properties
        //Light Properties

        //General Properties
        public static DNEItemSubTypeFlags ItemSubTypeFlags
        {
            get { return m_ItemSubTypeFlags; }
            set { m_ItemSubTypeFlags = value; }
        }

        public static DNEMcPointCoordSystem LocationCoordSys
        {
            get { return m_LocationCoordSys; }
            set { m_LocationCoordSys = value; }
        }

        public static bool LocationRelativeToDtm
        {
            get { return m_LocationRelativeToDtm; }
            set { m_LocationRelativeToDtm = value; }
        }

        public static DNSMcFVector2D LineTextureHeightRange
        {
            get { return m_LineTextureHeightRange; }
            set { m_LineTextureHeightRange = value; }
        }

        public static bool VerifyPrivatePropertiesId
        {
            get { return m_VerifyPrivatePropertiesId; }
            set { m_VerifyPrivatePropertiesId = value; }
        }

        public static IDNMcImageCalc ImageCalc
        {
            get { return m_ImageCalc; }
            set { m_ImageCalc = value; }
        }

        public static DNEDrawPriorityGroup DrawPriorityGroup
        {
            get { return m_DrawPriorityGroup; }
            set { m_DrawPriorityGroup = value; }
        }

        //Line Base Properties
        public static float LineWidth
        {
            get { return m_LineWidth; }
            set { m_LineWidth = value; }
        }

        public static DNELineStyle LineStyle
        {
            get { return m_LineStyle; }
            set { m_LineStyle = value; }
        }

        public static DNSMcBColor LineColor
        {
            get { return m_LineColor; }
            set { m_LineColor = value; }
        }

        public static IDNMcTexture LineTexture
        {
            get { return m_LineTexture; }
            set { m_LineTexture = value; }
        }

        public static byte LineBasedSmoothingLevels
        {
            get { return m_LineBasedSmoothingLevels; }
            set { m_LineBasedSmoothingLevels = value; }
        }

        public static float LineBasedGreatCirclePrecision
        {
            get { return m_LineBasedGreatCirclePrecision; }
            set { m_LineBasedGreatCirclePrecision = value; }
        }

        public static float LineOutlineWidth
        {
            get { return m_LineOutlineWidth; }
            set { m_LineOutlineWidth = value; }
        }

        public static DNSMcBColor LineOutlineColor
        {
            get { return m_LineOutlineColor; }
            set { m_LineOutlineColor = value; }
        }

        public static DNEPointOrderReverseMode PointOrderReverseMode
        {
            get { return m_PointOrderReverseMode; }
            set { m_PointOrderReverseMode = value; }
        }

        //Closed Shape Properties
        public static DNEShapeType ShapeType
        {
            get { return m_ShapeType; }
            set { m_ShapeType = value; }
        }

        public static DNSMcBColor FillColor
        {
            get { return m_FillColor; }
            set { m_FillColor = value; }
        }

        public static DNEFillStyle FillStyle
        {
            get { return m_FillStyle; }
            set { m_FillStyle = value; }
        }

        public static IDNMcTexture FillTexture
        {
            get { return m_FillTexture; }
            set { m_FillTexture = value; }
        }

        public static DNSMcBColor SidesFillColor
        {
            get { return m_SidesFillColor; }
            set { m_SidesFillColor = value; }
        }

        public static DNEFillStyle SidesFillStyle
        {
            get { return m_SidesFillStyle; }
            set { m_SidesFillStyle = value; }
        }

        public static IDNMcTexture SidesFillTexture
        {
            get { return m_SidesFillTexture; }
            set { m_SidesFillTexture = value; }
        }

        public static float VerticalHeight
        {
            get { return m_VerticalHeight; }
            set { m_VerticalHeight = value; }
        }


        //Arrow Properties
        public static DNEMcPointCoordSystem ArrowCoordSys
        {
            get { return m_ArrowCoordSys; }
            set { m_ArrowCoordSys = value; }
        }

        public static float ArrowHeadAngle
        {
            get { return m_ArrowHeadAngle; }
            set { m_ArrowHeadAngle = value; }
        }

        public static float ArrowHeadSize
        {
            get { return m_ArrowHeadSize; }
            set { m_ArrowHeadSize = value; }
        }

        public static float ArrowGapSize
        {
            get { return m_ArrowGapSize; }
            set { m_ArrowGapSize = value; }
        }

        //Arc Properties
        public static DNEMcPointCoordSystem ArcCoordSys
        {
            get { return m_ArcCoordSys; }
            set { m_ArcCoordSys = value; }
        }

        public static DNEItemGeometryType ArcEllipseType
        {
            get { return m_ArcEllipseType; }
            set { m_ArcEllipseType = value; }
        }

        public static float ArcStartAngle
        {
            get { return m_ArcStartAngle; }
            set { m_ArcStartAngle = value; }
        }

        public static float ArcEndAngle
        {
            get { return m_ArcEndAngle; }
            set { m_ArcEndAngle = value; }
        }

        public static DNEEllipseDefinition ArcEllipseDefinition
        {
            get { return m_ArcEllipseDefinition; }
            set { m_ArcEllipseDefinition = value; }
        }

        //ProceduralGeometryCoordinateSystem Properties
        public static DNEMcPointCoordSystem ProceduralGeometryCoordinateSys
        {
            get { return m_ProceduralGeometryCoordinateSys; }
            set { m_ProceduralGeometryCoordinateSys = value; }
        }

        //Ellipse Properties
        public static DNEMcPointCoordSystem EllipseCoordSys
        {
            get { return m_EllipseCoordSys; }
            set { m_EllipseCoordSys = value; }
        }

        public static DNEItemGeometryType EllipseType
        {
            get { return m_EllipseType; }
            set { m_EllipseType = value; }
        }

        public static float EllipseStartAngle
        {
            get { return m_EllipseStartAngle; }
            set { m_EllipseStartAngle = value; }
        }

        public static float EllipseEndAngle
        {
            get { return m_EllipseEndAngle; }
            set { m_EllipseEndAngle = value; }
        }

        public static float EllipseInnerRadiusFactor
        {
            get { return m_EllipseInnerRadiusFactor; }
            set { m_EllipseInnerRadiusFactor = value; }
        }

        public static DNEEllipseDefinition EllipseDefinition
        {
            get { return m_EllipseDefinition; }
            set { m_EllipseDefinition = value; }
        }

        //Rectangle Properties
        public static DNEMcPointCoordSystem RectangleCoordSys
        {
            get { return m_RectangleCoordSys; }
            set { m_RectangleCoordSys = value; }
        }

        public static DNEItemGeometryType RectangleType
        {
            get { return m_RectangleType; }
            set { m_RectangleType = value; }
        }

        public static DNERectangleDefinition RectangleDefinition
        {
            get { return m_RectangleDefinition; }
            set { m_RectangleDefinition = value; }
        }

        //Text Properties
        public static DNEMcPointCoordSystem TextCoordSys
        {
            get { return m_TextCoordSys; }
            set { m_TextCoordSys = value; }
        }

        public static IDNMcFont TextFont
        {
            get { return m_TextFont; }
            set { m_TextFont = value; }
        }

        public static DNENeverUpsideDownMode NeverUpsideDown
        {
            get { return m_NeverUpsideDown; }
            set { m_NeverUpsideDown = value; }
        }

        public static DNSMcFVector2D TextScale
        {
            get { return m_TextScale; }
            set { m_TextScale = value; }
        }

        public static uint TextMargin
        {
            get { return m_TextMargin; }
            set { m_TextMargin = value; }
        }

        public static DNSMcBColor TextColor
        {
            get { return m_TextColor; }
            set { m_TextColor = value; }
        }

        public static DNSMcBColor TextBackgroundColor
        {
            get { return m_TextBackgroundColor; }
            set { m_TextBackgroundColor = value; }
        }

        public static bool TextIsUnicode
        {
            get { return m_TextIsUnicode; }
            set { m_TextIsUnicode = value; }
        }

        public static DNSMcBColor TextOutlineColor
        {
            get { return m_TextOutlineColor; }
            set { m_TextOutlineColor = value; }
        }

        public static uint TextMarginY
        {
            get { return m_TextMarginY; }
            set { m_TextMarginY = value; }
        }

        public static DNEBackgroundShape BackgroundShape
        {
            get { return m_BackgroundShape; }
            set { m_BackgroundShape = value; }
        }

        //Picture

        public static IDNMcTexture PictureTexture
        {
            get { return m_PictureTexture; }
            set { m_PictureTexture = value; }
        }

        public static DNEMcPointCoordSystem PictureCoordSys
        {
            get { return m_PictureCoordSys; }
            set { m_PictureCoordSys = value; }
        }

        public static bool PictureIsSizeFactor
        {
            get { return m_PictureIsSizeFactor; }
            set { m_PictureIsSizeFactor = value; }
        }

        public static bool PictureIsUseTextureGeoReferencing
        {
            get { return m_PictureIsUseTextureGeoReferencing; }
            set { m_PictureIsUseTextureGeoReferencing = value; }
        }

        public static float PicWidth
        {
            get { return m_PicWidth; }
            set { m_PicWidth = value; }
        }

        public static float PicHeight
        {
            get { return m_PicHeight; }
            set { m_PicHeight = value; }
        }

        public static DNEBoundingBoxPointFlags PicRectAlignmentBitField
        {
            get { return m_PicRectAlignmentBitField; }
            set { m_PicRectAlignmentBitField = value; }
        }

        public static bool PictureNeverUpsideDown
        {
            get { return m_PictureNeverUpsideDown; }
            set { m_PictureNeverUpsideDown = value; }
        }

        //Line Expansion Properties
        public static DNEMcPointCoordSystem LineExpansionCoordinateSystem
        {
            get { return m_LineExpansionCoordinateSystem; }
            set { m_LineExpansionCoordinateSystem = value; }
        }

        public static DNEItemGeometryType LineExpansionType
        {
            get { return m_LineExpansionType; }
            set { m_LineExpansionType = value; }
        }

        public static float LineExpansionRadius
        {
            get { return m_LineExpansionRadius; }
            set { m_LineExpansionRadius = value; }
        }

        //Light Properties
        public static DNSMcFColor LightDiffuseColor
        {
            get { return m_LightDiffuseColor; }
            set { m_LightDiffuseColor = value; }
        }

        public static DNSMcFColor LightSpecularColor
        {
            get { return m_LightSpecularColor; }
            set { m_LightSpecularColor = value; }
        }

        public static DNSMcAttenuation LightAttenuation
        {
            get { return m_LightAttenuation; }
            set { m_LightAttenuation = value; }
        }

        public static DNSMcFVector3D LightDirection
        {
            get { return m_LightDirection; }
            set { m_LightDirection = value; }
        }

        public static float LightHalfOuterAngle
        {
            get { return m_LightHalfOuterAngle; }
            set { m_LightHalfOuterAngle = value; }
        }

        public static float LightHalfInnerAngle
        {
            get { return m_LightHalfInnerAngle; }
            set { m_LightHalfInnerAngle = value; }
        }

        //Partical Effect Properties
        public static float ParticalEffectStartingTimePoint
        {
            get { return m_ParticalEffectStartingTimePoint; }
            set { m_ParticalEffectStartingTimePoint = value; }
        }

        public static DNEParticleEffectState ParticalEffectState
        {
            get { return m_ParticalEffectState; }
            set { m_ParticalEffectState = value; }
        }

        public static float ParticalEffectSartingDelay
        {
            get { return m_ParticalEffectSartingDelay; }
            set { m_ParticalEffectSartingDelay = value; }
        }

        //Projector Properties
        public static float ProjectorHalfFOVHorizAngle
        {
            get { return m_ProjectorHalfFOVHorizAngle; }
            set { m_ProjectorHalfFOVHorizAngle = value; }
        }

        public static float ProjectorAspectRatio
        {
            get { return m_ProjectorAspectRatio; }
            set { m_ProjectorAspectRatio = value; }
        }

        public static DNETargetTypesFlags ProjectorItemTargetTypeFlags
        {
            get { return m_ProjectorItemTargetTypeFlags; }
            set { m_ProjectorItemTargetTypeFlags = value; }
        }

        //Sound Properties
        public static bool SoundLoop
        {
            get { return m_SoundLoop; }
            set { m_SoundLoop = value; }
        }

        public static float SoundStartingTimePoint
        {
            get { return m_SoundStartingTimePoint; }
            set { m_SoundStartingTimePoint = value; }
        }

        public static DNESoundState SoundState
        {
            get { return m_SoundState; }
            set { m_SoundState = value; }
        }

        //Mesh Properties

        public static IDNMcMeshItem MeshFile
        {
            get
            {
                try
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Filter = "all files *.*|*.*";
                    ofd.RestoreDirectory = true;
                    if (ofd.ShowDialog() != DialogResult.OK)
                    {
                        MessageBox.Show("aborted");
                        return null;
                    }

                    string filename = ofd.FileName;
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("DNMcXFileMesh.Create", McEx);
                }

                return m_MeshFile;
            }
            set { m_MeshFile = value; }
        }

        public static bool MeshRotateToTerrain
        {
            get { return m_MeshRotateToTerrain; }
            set { m_MeshRotateToTerrain = value; }
        }

        public static DNEBasePointAlignment MeshAlignment
        {
            get { return m_MeshAlignment; }
            set { m_MeshAlignment = value; }
        }

        public static DNSMcBColor MeshTextureColor
        {
            get { return m_MeshTextureColor; }
            set { m_MeshTextureColor = value; }
        }

        public static bool MeshParticipatesInTerrainHeight
        {
            get { return m_MeshParticipatesInTerrainHeight; }
            set { m_MeshParticipatesInTerrainHeight = value; }
        }
        public static bool MeshUseExisting
        {
            get { return m_MeshUseExisting; }
            set { m_MeshUseExisting = value; }
        }
        public static bool MeshIsStatic
        {
            get { return m_MeshIsStatic; }
            set { m_MeshIsStatic = value; }
        }
        public static bool Meshcastshadows
        {
            get { return m_Meshcastshadows; }
            set { m_Meshcastshadows = value; }
        }
        public static bool MeshDisplayItemsAttachedToTerrain
        {
            get { return m_MeshDisplayItemsAttachedToTerrain; }
            set { m_MeshDisplayItemsAttachedToTerrain = value; }
        }

        public static void CopyDataFromClipboard(DataGridView dataGridView, bool isAddRow = false )
        {
            char[] rowSplitter = { '\r', '\n' };
            char[] columnSplitter = { '\t' };

            //get the text from clipboard
            string stringInClipboard = Clipboard.GetText();
            //split it into lines
            string[] rowsInClipboard = stringInClipboard.Split(rowSplitter, StringSplitOptions.RemoveEmptyEntries);
            //get the row and column of selected cell in dgvLocationPoints
            if (dataGridView.SelectedCells.Count > 0)
            {
                int r = dataGridView.SelectedCells[0].RowIndex;
                int c = dataGridView.SelectedCells[0].ColumnIndex;

                //add rows into dgvLocationPoints to fit clipboard lines
                if (dataGridView.Rows.Count < (r + rowsInClipboard.Length))
                {
                    int numRows = r + rowsInClipboard.Length - dataGridView.Rows.Count;
                    if (isAddRow)
                        numRows++;
                    dataGridView.Rows.Add(numRows);
                }

                // loop through the lines, split them into cells and place the values in the corresponding cell.
                for (int iRow = 0; iRow < rowsInClipboard.Length; iRow++)
                {
                    //split row into cell values
                    string[] valuesInRow = rowsInClipboard[iRow].Trim().Split(columnSplitter);

                    //cycle through cell values
                    for (int iCol = 0; iCol < valuesInRow.Length; iCol++)
                    {
                        //assign cell value, only if it within columns of the dgvLocationPoints
                        if (dataGridView.ColumnCount - 1 >= c + iCol)
                        {
                            dataGridView.Rows[r + iRow].Cells[c + iCol].Value = valuesInRow[iCol];
                        }
                    }
                }
            }
        }

        public static void ImportDataFromFile(DataGridView dataGridView, int numColumns)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                StreamReader reader = new StreamReader(ofd.FileName);
                char[] delimeters = new char[1];
                delimeters[0] = ',';
                string[] readerData = reader.ReadLine().Split(delimeters);
                reader.Close();

                double result = 0;
                int currColumn = 0;
                int currRow = 0;
                // int numColumns = 3;
                //
                dataGridView.RowCount = (int)readerData.Length / numColumns + 1;

                for (int i = 0; i < readerData.Length; i++)
                {
                    bool IsParseSucc = double.TryParse(readerData[i], out result);
                    if (IsParseSucc != true)
                    {
                        MessageBox.Show("Import data from file failed.\nThe data '" + readerData[i].ToString() + "' located in cell: " + i.ToString() + " is invalid",
                                            "Import Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        dataGridView.Rows.Clear();
                        dataGridView.RowCount = 1;
                        return;
                    }
                    else
                    {
                        dataGridView[currColumn, currRow].Value = result;
                        currColumn++;

                        if (currColumn >= numColumns)
                        {
                            currColumn = 0;
                            currRow++;
                        }
                    }
                }
            }
        }

        public static void ExportDataToFile(DataGridView dataGridView, int numColumns)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV File (*.csv) | *.csv";
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StreamWriter stw = new StreamWriter(sfd.FileName);
                string amplifiers = "";

                for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < numColumns; j++)
                    {
                        if (dataGridView[j, i].Value != null)
                            amplifiers += dataGridView[j, i].Value.ToString() + ",";
                        else
                            amplifiers += ",";
                    }
                }

                string exportData = amplifiers.Remove(amplifiers.Length - 1);
                stw.Write(exportData);

                stw.Close();
            }
        }

    }

    #endregion
}

