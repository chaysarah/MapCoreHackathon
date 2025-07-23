using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers.ObjectWorld;
using MCTester.Managers;
using MCTester.ObjectWorld.ObjectsUserControls;
using MCTester.General_Forms;

namespace MCTester.GUI.Forms
{
    public partial class ObjectPropertiesForm : Form
    {
        private ColorDialog colorDialog;
       

        public ObjectPropertiesForm()
        {
            InitializeComponent();

            cmbLocationCoordSys.Items.AddRange(Enum.GetNames(typeof(DNEMcPointCoordSystem)));
            cmbDrawPriorityGroup.Items.AddRange(Enum.GetNames(typeof(DNEDrawPriorityGroup)));
            cmbArrowCoordinateSystem .Items.AddRange(Enum.GetNames(typeof(DNEMcPointCoordSystem)));
            cmbArcCoordinateSystem.Items.AddRange(Enum.GetNames(typeof(DNEMcPointCoordSystem)));
            cmbArcEllipseType.Items.AddRange(Enum.GetNames(typeof(DNEItemGeometryType)));
            cmbArcEllipseDefinition.Items.AddRange(Enum.GetNames(typeof(DNEEllipseDefinition)));
            cmbEllipseCoordinateSystem.Items.AddRange(Enum.GetNames(typeof(DNEMcPointCoordSystem)));
            cmbPictureCoordinateSystem.Items.AddRange(Enum.GetNames(typeof(DNEMcPointCoordSystem)));
            cmbTextCoordinateSystem.Items.AddRange(Enum.GetNames(typeof(DNEMcPointCoordSystem)));
            cmbEllipseType.Items.AddRange(Enum.GetNames(typeof(DNEItemGeometryType)));
            cmbEllipseDefinition.Items.AddRange(Enum.GetNames(typeof(DNEEllipseDefinition)));
            cmbRectangleCoordinateSystem.Items.AddRange(Enum.GetNames(typeof(DNEMcPointCoordSystem)));
            cmbRectangleType.Items.AddRange(Enum.GetNames(typeof(DNEItemGeometryType)));
            cmbRectangleDefinition.Items.AddRange(Enum.GetNames(typeof(DNERectangleDefinition)));
            cmbLineExpansionCoordSys.Items.AddRange(Enum.GetNames(typeof(DNEMcPointCoordSystem)));
            cmbLineExpansionType.Items.AddRange(Enum.GetNames(typeof(DNEItemGeometryType)));
            cmbLineStyle.Items.AddRange(Enum.GetNames(typeof(DNELineStyle)));
            cmbFillStyle.Items.AddRange(Enum.GetNames(typeof(DNEFillStyle)));
            cmbSidesFillStyle.Items.AddRange(Enum.GetNames(typeof(DNEFillStyle)));
            cmbClosedShapeType.Items.AddRange(Enum.GetNames(typeof(DNEShapeType)));
            cmbParticalEffectState.Items.AddRange(Enum.GetNames(typeof(DNEParticleEffectState)));
            cmbSoundState.Items.AddRange(Enum.GetNames(typeof(DNESoundState)));
            cmbMeshBasePointAlignment.Items.AddRange(Enum.GetNames(typeof(DNEBasePointAlignment)));
            cmbNeverUpsideDownMode.Items.AddRange(Enum.GetNames(typeof(DNENeverUpsideDownMode)));
            cmbBackgroundShape.Items.AddRange(Enum.GetNames(typeof(DNEBackgroundShape)));
            cmbProcedualGeomtryCoordinateSys.Items.AddRange(Enum.GetNames(typeof(DNEMcPointCoordSystem)));
            cmbPointOrderReverseMode.Items.AddRange(Enum.GetNames(typeof(DNEPointOrderReverseMode)));

            colorDialog = new ColorDialog();
            selectLineColor.EnabledButtons(true);
            selectFillColor.EnabledButtons(true);
            selectOutlineColor.EnabledButtons(true); 
            selectSidesFillColor.EnabledButtons(true); 
            selectTextBackgroundColor.EnabledButtons(true);
            selectTextColor.EnabledButtons(true);
            selectTextOutlineColor.EnabledButtons(true);
            selectMeshTextureColor.EnabledButtons(true);
            selectLightDiffuseColor.EnabledButtons(true);
            selectLightSpecularColor.EnabledButtons(true);

            LoadItem();
        }

        private void LoadItem()
        {
            //General Tab
            cmbLocationCoordSys.Text = ObjectPropertiesBase.LocationCoordSys.ToString();
            cmbDrawPriorityGroup.Text = ObjectPropertiesBase.DrawPriorityGroup.ToString();
            chxLocationRelativeToDTM.Checked = ObjectPropertiesBase.LocationRelativeToDtm;
            ntxMinLineTextureHeightRange.SetFloat(ObjectPropertiesBase.LineTextureHeightRange.x);
            ntxMaxLineTextureHeightRange.SetFloat(ObjectPropertiesBase.LineTextureHeightRange.y);
            chxVerifyPrivatePropertiesId.Checked = ObjectPropertiesBase.VerifyPrivatePropertiesId;
            
            DNEItemSubTypeFlags[] itemSubTypeFlags = ((DNEItemSubTypeFlags[])Enum.GetValues(typeof(DNEItemSubTypeFlags)));
            foreach (DNEItemSubTypeFlags subType in itemSubTypeFlags)
                clstItemSubTypeBitField.Items.Add(subType);

            DNEItemSubTypeFlags itemSubType = ObjectPropertiesBase.ItemSubTypeFlags;
            int index = 0;
            
            foreach (DNEItemSubTypeFlags flag in itemSubTypeFlags)
            {
                if ((itemSubType & flag) == flag)
                    clstItemSubTypeBitField.SetItemChecked(index, true);

                ++index;
            }

            ctrlImageCalc.ImageCalc = ObjectPropertiesBase.ImageCalc;
            
            //Line Based Tab
            ntxLineWidth.SetFloat(ObjectPropertiesBase.LineWidth);
            selectLineColor.BColor = ObjectPropertiesBase.LineColor;
            cmbLineStyle.Text = ObjectPropertiesBase.LineStyle.ToString();

            ntxLineBasedSmoothingLevels.SetByte(ObjectPropertiesBase.LineBasedSmoothingLevels);
            ntxLineBasedGreatCirclePrecision.SetFloat(ObjectPropertiesBase.LineBasedGreatCirclePrecision);
            ntxOutlineWidth.SetFloat(ObjectPropertiesBase.LineOutlineWidth);
            selectOutlineColor.BColor = ObjectPropertiesBase.LineOutlineColor; 
            cmbPointOrderReverseMode.Text = ObjectPropertiesBase.PointOrderReverseMode.ToString();
            ctrlLineTexture.SetTexture(ObjectPropertiesBase.LineTexture);

            //Closed Shape Tab
            cmbClosedShapeType.Text = ObjectPropertiesBase.ShapeType.ToString();
            selectFillColor.BColor = ObjectPropertiesBase.FillColor;
            cmbFillStyle.Text = ObjectPropertiesBase.FillStyle.ToString();

            selectSidesFillColor.BColor = ObjectPropertiesBase.SidesFillColor;
            cmbSidesFillStyle.Text = ObjectPropertiesBase.SidesFillStyle.ToString();

            ntxVerticalHeight.SetFloat(ObjectPropertiesBase.VerticalHeight);
            ctrlSidesFillTexture.SetTexture(ObjectPropertiesBase.SidesFillTexture);
            ctrlFillTexture.SetTexture(ObjectPropertiesBase.FillTexture);

            //Arrow Tab
            cmbArrowCoordinateSystem.Text = ObjectPropertiesBase.ArrowCoordSys.ToString();
            ntxArrowHeadAngle.SetFloat(ObjectPropertiesBase.ArrowHeadAngle);
            ntxArrowHeadSize.SetFloat(ObjectPropertiesBase.ArrowHeadSize);
            ntxArrowGapSize.SetFloat(ObjectPropertiesBase.ArrowGapSize);
            
            //Arc Tab
            cmbArcCoordinateSystem.Text = ObjectPropertiesBase.ArcCoordSys.ToString();
            cmbArcEllipseType.Text = ObjectPropertiesBase.ArcEllipseType.ToString();
            ntxArcStartAngle.SetFloat(ObjectPropertiesBase.ArcStartAngle);
            ntxArcEndAngle.SetFloat(ObjectPropertiesBase.ArcEndAngle);
            cmbArcEllipseDefinition.Text = ObjectPropertiesBase.ArcEllipseDefinition.ToString();

            //Ellipse Tab
            cmbEllipseCoordinateSystem.Text = ObjectPropertiesBase.EllipseCoordSys.ToString();
            cmbEllipseType.Text = ObjectPropertiesBase.EllipseType.ToString();
            ntxInnerRadiusFactor.SetFloat(ObjectPropertiesBase.EllipseInnerRadiusFactor);
            ntxEllipseStartAngle.SetFloat(ObjectPropertiesBase.EllipseStartAngle);
            ntxEllipseEndAngle.SetFloat(ObjectPropertiesBase.EllipseEndAngle);
            cmbEllipseDefinition.Text = ObjectPropertiesBase.EllipseDefinition.ToString();
            
            //Rectangle Tab
            cmbRectangleCoordinateSystem.Text = ObjectPropertiesBase.RectangleCoordSys.ToString();
            cmbRectangleType.Text = ObjectPropertiesBase.RectangleType.ToString();
            cmbRectangleDefinition.Text = ObjectPropertiesBase.RectangleDefinition.ToString();

            //Text Tab
            ctrlFontButtons1.SetFont(ObjectPropertiesBase.TextFont);
            cmbTextCoordinateSystem.Text = ObjectPropertiesBase.TextCoordSys.ToString();
            cmbNeverUpsideDownMode.Text = ObjectPropertiesBase.NeverUpsideDown.ToString();
            cmbBackgroundShape.Text = ObjectPropertiesBase.BackgroundShape.ToString();
            ntxTextMargin.SetUInt32(ObjectPropertiesBase.TextMargin);
            ntxTextMarginY.SetUInt32(ObjectPropertiesBase.TextMarginY);
            ctrl2DFVectorTextScale.SetVector2D(ObjectPropertiesBase.TextScale);

            selectTextColor.BColor = ObjectPropertiesBase.TextColor;
            selectTextBackgroundColor.BColor = ObjectPropertiesBase.TextBackgroundColor;
            selectTextOutlineColor.BColor = ObjectPropertiesBase.TextOutlineColor;
            
            chxTextIsUnicode.Checked = ObjectPropertiesBase.TextIsUnicode;

            //Picture
            cmbPictureCoordinateSystem.Text = ObjectPropertiesBase.PictureCoordSys.ToString();
            chkPictureIsSizeFactor.Checked = ObjectPropertiesBase.PictureIsSizeFactor;  
            chxPictureIsUseTextureGeoReferencing.Checked = ObjectPropertiesBase.PictureIsUseTextureGeoReferencing;
            ntxPicWidth.SetFloat(ObjectPropertiesBase.PicWidth);
            ntxPicHeight.SetFloat(ObjectPropertiesBase.PicHeight);
            chxPictureNeverUpsideDown.Checked = ObjectPropertiesBase.PictureNeverUpsideDown;
            ctrlDefaultTextureButtons.SetTexture(ObjectPropertiesBase.PictureTexture);
            
            //Line Expansion Tab
            cmbLineExpansionCoordSys.Text = ObjectPropertiesBase.LineExpansionCoordinateSystem.ToString();
            cmbLineExpansionType.Text = ObjectPropertiesBase.LineExpansionType.ToString();
            ntxLineExpansionRadius.SetFloat(ObjectPropertiesBase.LineExpansionRadius);

            //Light Tab
            selectLightDiffuseColor.FColor = ObjectPropertiesBase.LightDiffuseColor;
            selectLightSpecularColor.FColor = ObjectPropertiesBase.LightSpecularColor;
            ctrlLightAttenuation.Attenuation = ObjectPropertiesBase.LightAttenuation;
            ctrl3DFVectorLightDirection.SetVector3D(ObjectPropertiesBase.LightDirection);
            ntxLightHalfOuterAngle.SetFloat(ObjectPropertiesBase.LightHalfOuterAngle);
            ntxLightHalfInnerAngle.SetFloat(ObjectPropertiesBase.LightHalfInnerAngle);

            //Partical Effect Tab
            ntxParticalEffectStartingTimePoint.SetFloat(ObjectPropertiesBase.ParticalEffectStartingTimePoint);
            cmbParticalEffectState.Text = ObjectPropertiesBase.ParticalEffectState.ToString();
            ntxParticalEffectStartingDelay.SetFloat(ObjectPropertiesBase.ParticalEffectSartingDelay);

            //Projector Tab
            ntxProjectorHalfFOVHorizAngle.SetFloat(ObjectPropertiesBase.ProjectorHalfFOVHorizAngle);
            ntxAspectRatio.SetFloat(ObjectPropertiesBase.ProjectorAspectRatio);

            DNETargetTypesFlags[] itemDefaultTargetTypesFlags = ((DNETargetTypesFlags[])Enum.GetValues(typeof(DNETargetTypesFlags)));
            foreach (DNETargetTypesFlags subType in itemDefaultTargetTypesFlags)
            {
                if (subType != DNETargetTypesFlags._ETTF_NONE)
                clbDefaultTargetTypes.Items.Add(subType);
            }

            DNETargetTypesFlags itemTargetType = ObjectPropertiesBase.ProjectorItemTargetTypeFlags;
            int indexTargetType = 0;

            foreach (DNETargetTypesFlags flag in itemDefaultTargetTypesFlags)
            {
                if (flag != DNETargetTypesFlags._ETTF_NONE)
                {
                    if ((itemTargetType & flag) == flag)
                        clbDefaultTargetTypes.SetItemChecked(indexTargetType, true);

                    ++indexTargetType;
                }
            }


            //Sound Tab
            chxSoundLoop.Checked = ObjectPropertiesBase.SoundLoop;
            ntxSoundStartingTimePoint.SetFloat(ObjectPropertiesBase.SoundStartingTimePoint);
            cmbSoundState.Text = ObjectPropertiesBase.SoundState.ToString();

            //Mesh Tab
            chxMeshUseExisting.Checked = ObjectPropertiesBase.MeshUseExisting;
            chxMeshRotateToTerrain.Checked = ObjectPropertiesBase.MeshRotateToTerrain;
            selectMeshTextureColor.BColor = ObjectPropertiesBase.MeshTextureColor;
            cmbMeshBasePointAlignment.Text = ObjectPropertiesBase.MeshAlignment.ToString();
            chxMeshParticipatesInTerrainHeight.Checked = ObjectPropertiesBase.MeshParticipatesInTerrainHeight;
            chxMeshIsStatic.Checked = ObjectPropertiesBase.MeshIsStatic;
            chxDisplayItemsAttachedToTerrain.Checked = ObjectPropertiesBase.MeshDisplayItemsAttachedToTerrain;

            //Procedural geometry Tab
            cmbProcedualGeomtryCoordinateSys.Text = ObjectPropertiesBase.ProceduralGeometryCoordinateSys.ToString();
        }


        private void SaveItems()
        {
            //General Tab
            DNEItemSubTypeFlags bitField = 0;
            for (int i = 0; i < clstItemSubTypeBitField.CheckedItems.Count; i++)
            {
                DNEItemSubTypeFlags flag = (DNEItemSubTypeFlags)Enum.Parse(typeof(DNEItemSubTypeFlags), clstItemSubTypeBitField.CheckedItems[i].ToString());
                bitField |= flag;
            }
            
            ObjectPropertiesBase.ItemSubTypeFlags = bitField;
            
            ObjectPropertiesBase.LocationRelativeToDtm = chxLocationRelativeToDTM.Checked;
            ObjectPropertiesBase.LineTextureHeightRange = new DNSMcFVector2D(ntxMinLineTextureHeightRange.GetFloat(), ntxMaxLineTextureHeightRange.GetFloat());
            ntxMaxLineTextureHeightRange.GetDouble();
            ObjectPropertiesBase.VerifyPrivatePropertiesId = chxVerifyPrivatePropertiesId.Checked;

            DNEMcPointCoordSystem LocationCoordSys = (DNEMcPointCoordSystem)Enum.Parse(typeof(DNEMcPointCoordSystem), cmbLocationCoordSys.Text);
            ObjectPropertiesBase.LocationCoordSys = LocationCoordSys;
            ObjectPropertiesBase.DrawPriorityGroup = (DNEDrawPriorityGroup)Enum.Parse(typeof(DNEDrawPriorityGroup), cmbDrawPriorityGroup.Text);

            ObjectPropertiesBase.ImageCalc = ctrlImageCalc.ImageCalc;

            //Line Base Tab
            ObjectPropertiesBase.LineWidth = ntxLineWidth.GetFloat();
            ObjectPropertiesBase.LineColor = selectLineColor.BColor;
            
            DNELineStyle LineStyle = (DNELineStyle)Enum.Parse(typeof(DNELineStyle), cmbLineStyle.Text);
            ObjectPropertiesBase.LineStyle = LineStyle;
            ObjectPropertiesBase.LineBasedSmoothingLevels = ntxLineBasedSmoothingLevels.GetByte();
            ObjectPropertiesBase.LineBasedGreatCirclePrecision = ntxLineBasedGreatCirclePrecision.GetFloat();
            ObjectPropertiesBase.LineOutlineWidth = ntxOutlineWidth.GetFloat();
            ObjectPropertiesBase.LineOutlineColor = selectOutlineColor.BColor;
            ObjectPropertiesBase.PointOrderReverseMode = (DNEPointOrderReverseMode)Enum.Parse(typeof(DNEPointOrderReverseMode), cmbPointOrderReverseMode.Text);
            ObjectPropertiesBase.LineTexture = ctrlLineTexture.GetTexture();

            //Closed Shape Tab
            ObjectPropertiesBase.ShapeType = (DNEShapeType)Enum.Parse(typeof(DNEShapeType),cmbClosedShapeType.Text);
            ObjectPropertiesBase.FillColor = selectFillColor.BColor;
            DNEFillStyle FillStyle = (DNEFillStyle)Enum.Parse(typeof(DNEFillStyle), cmbFillStyle.Text);
            ObjectPropertiesBase.FillStyle = FillStyle;
            ObjectPropertiesBase.SidesFillColor = selectSidesFillColor.BColor;
    
            DNEFillStyle SidesFillStyle = (DNEFillStyle)Enum.Parse(typeof(DNEFillStyle), cmbSidesFillStyle.Text);
            ObjectPropertiesBase.SidesFillStyle = SidesFillStyle;
            ObjectPropertiesBase.VerticalHeight = ntxVerticalHeight.GetFloat();
            ObjectPropertiesBase.SidesFillTexture = ctrlSidesFillTexture.GetTexture();
            ObjectPropertiesBase.FillTexture = ctrlFillTexture.GetTexture();

            //Arrow Tab
            DNEMcPointCoordSystem arrowCoordSys = (DNEMcPointCoordSystem)Enum.Parse(typeof(DNEMcPointCoordSystem), cmbArrowCoordinateSystem.Text);
            ObjectPropertiesBase.ArrowCoordSys = arrowCoordSys;
            ObjectPropertiesBase.ArrowHeadAngle = ntxArrowHeadAngle.GetFloat();
            ObjectPropertiesBase.ArrowHeadSize = ntxArrowHeadSize.GetFloat();
            ObjectPropertiesBase.ArrowGapSize = ntxArrowGapSize.GetFloat();

            //Arc Tab
            DNEMcPointCoordSystem arcVertexCoordSys = (DNEMcPointCoordSystem)Enum.Parse(typeof(DNEMcPointCoordSystem), cmbArcCoordinateSystem.Text);
            ObjectPropertiesBase.ArcCoordSys = arcVertexCoordSys;
            ObjectPropertiesBase.ArcStartAngle = ntxArcStartAngle.GetFloat();
            ObjectPropertiesBase.ArcEndAngle = ntxArcEndAngle.GetFloat();
            ObjectPropertiesBase.ArcEllipseDefinition = (DNEEllipseDefinition)Enum.Parse(typeof(DNEEllipseDefinition), cmbArcEllipseDefinition.Text);

            DNEItemGeometryType arcEllipseType = (DNEItemGeometryType)Enum.Parse(typeof(DNEItemGeometryType), cmbArcEllipseType.Text);
            ObjectPropertiesBase.ArcEllipseType = arcEllipseType;

            //Ellipse Tab
            DNEMcPointCoordSystem ellipseVertexCoordSys = (DNEMcPointCoordSystem)Enum.Parse(typeof(DNEMcPointCoordSystem), cmbEllipseCoordinateSystem.Text);
            ObjectPropertiesBase.EllipseCoordSys = ellipseVertexCoordSys;
            ObjectPropertiesBase.EllipseInnerRadiusFactor = ntxInnerRadiusFactor.GetFloat();
            ObjectPropertiesBase.EllipseStartAngle = ntxEllipseStartAngle.GetFloat();
            ObjectPropertiesBase.EllipseEndAngle = ntxEllipseEndAngle.GetFloat();
            ObjectPropertiesBase.EllipseDefinition = (DNEEllipseDefinition)Enum.Parse(typeof(DNEEllipseDefinition), cmbEllipseDefinition.Text);

            DNEItemGeometryType ellipseType = (DNEItemGeometryType)Enum.Parse(typeof(DNEItemGeometryType), cmbEllipseType.Text);
            ObjectPropertiesBase.EllipseType = ellipseType;

            //Rectangle Tab
            DNEMcPointCoordSystem rectangleVertexCoordSys = (DNEMcPointCoordSystem)Enum.Parse(typeof(DNEMcPointCoordSystem), cmbRectangleCoordinateSystem.Text);
            ObjectPropertiesBase.RectangleCoordSys = rectangleVertexCoordSys;

            ObjectPropertiesBase.RectangleDefinition = (DNERectangleDefinition)Enum.Parse(typeof(DNERectangleDefinition), cmbRectangleDefinition.Text);

            DNEItemGeometryType rectType = (DNEItemGeometryType)Enum.Parse(typeof(DNEItemGeometryType), cmbRectangleType.Text);
            ObjectPropertiesBase.RectangleType = rectType;

            //Text Tab
            ObjectPropertiesBase.TextFont = ctrlFontButtons1.GetFont();

            DNEMcPointCoordSystem textCoordSys = (DNEMcPointCoordSystem)Enum.Parse(typeof(DNEMcPointCoordSystem), cmbTextCoordinateSystem.Text);
            ObjectPropertiesBase.TextCoordSys = textCoordSys;
            ObjectPropertiesBase.TextScale = ctrl2DFVectorTextScale.GetVector2D();
            ObjectPropertiesBase.NeverUpsideDown = (DNENeverUpsideDownMode)Enum.Parse(typeof(DNENeverUpsideDownMode), cmbNeverUpsideDownMode.Text);
            ObjectPropertiesBase.TextMargin = ntxTextMargin.GetUInt32();
            ObjectPropertiesBase.TextMarginY = ntxTextMarginY.GetUInt32();
            ObjectPropertiesBase.TextColor = selectTextColor.BColor;
            ObjectPropertiesBase.TextBackgroundColor = selectTextBackgroundColor.BColor;
            ObjectPropertiesBase.TextIsUnicode = chxTextIsUnicode.Checked;
            ObjectPropertiesBase.TextOutlineColor = selectTextOutlineColor.BColor;
            ObjectPropertiesBase.BackgroundShape = (DNEBackgroundShape)Enum.Parse(typeof(DNEBackgroundShape), cmbBackgroundShape.Text);

            //Picture
            DNEMcPointCoordSystem pictureCoordSys = (DNEMcPointCoordSystem)Enum.Parse(typeof(DNEMcPointCoordSystem), cmbPictureCoordinateSystem.Text);
            ObjectPropertiesBase.PictureCoordSys = pictureCoordSys;
            ObjectPropertiesBase.PictureIsSizeFactor = chkPictureIsSizeFactor.Checked;
            ObjectPropertiesBase.PictureIsUseTextureGeoReferencing = chxPictureIsUseTextureGeoReferencing.Checked;
            ObjectPropertiesBase.PicWidth = ntxPicWidth.GetFloat();
            ObjectPropertiesBase.PicHeight = ntxPicHeight.GetFloat();
            ObjectPropertiesBase.PictureNeverUpsideDown = chxPictureNeverUpsideDown.Checked;
            ObjectPropertiesBase.PictureTexture = ctrlDefaultTextureButtons.GetTexture();

            //Line Expansion Tab
            DNEMcPointCoordSystem lineExpansionCoordSys = (DNEMcPointCoordSystem)Enum.Parse(typeof(DNEMcPointCoordSystem), cmbLineExpansionCoordSys.Text);
            ObjectPropertiesBase.LineExpansionCoordinateSystem = lineExpansionCoordSys;
            ObjectPropertiesBase.LineExpansionRadius = ntxLineExpansionRadius.GetFloat();

            DNEItemGeometryType lineExpansionType = (DNEItemGeometryType)Enum.Parse(typeof(DNEItemGeometryType), cmbLineExpansionType.Text);
            ObjectPropertiesBase.LineExpansionType = lineExpansionType;
            
            //Light Tab
            ObjectPropertiesBase.LightDiffuseColor = selectLightDiffuseColor.FColor;
            ObjectPropertiesBase.LightSpecularColor = selectLightSpecularColor.FColor; 
            ObjectPropertiesBase.LightAttenuation = ctrlLightAttenuation.Attenuation;
            ObjectPropertiesBase.LightDirection = ctrl3DFVectorLightDirection.GetVector3D();
            ObjectPropertiesBase.LightHalfOuterAngle = ntxLightHalfOuterAngle.GetFloat();
            ObjectPropertiesBase.LightHalfInnerAngle = ntxLightHalfInnerAngle.GetFloat();

            //Partical Effect Tab
            ObjectPropertiesBase.ParticalEffectStartingTimePoint = ntxParticalEffectStartingTimePoint.GetFloat();
            ObjectPropertiesBase.ParticalEffectSartingDelay = ntxParticalEffectStartingDelay.GetFloat();

            DNEParticleEffectState ParticalEffectState = (DNEParticleEffectState)Enum.Parse(typeof(DNEParticleEffectState), cmbParticalEffectState.Text);
            ObjectPropertiesBase.ParticalEffectState = ParticalEffectState;

            //Projector Tab
            ObjectPropertiesBase.ProjectorHalfFOVHorizAngle = ntxProjectorHalfFOVHorizAngle.GetFloat();
            ObjectPropertiesBase.ProjectorAspectRatio = ntxAspectRatio.GetFloat();
            
            DNETargetTypesFlags bitTargetTypeField = 0;
            for (int i = 0; i < clbDefaultTargetTypes.CheckedItems.Count; i++)
            {
                DNETargetTypesFlags flag = (DNETargetTypesFlags)Enum.Parse(typeof(DNETargetTypesFlags), clbDefaultTargetTypes.CheckedItems[i].ToString());
                bitTargetTypeField |= flag;
            }

            ObjectPropertiesBase.ProjectorItemTargetTypeFlags = bitTargetTypeField;

            //Sound Tab
            ObjectPropertiesBase.SoundLoop = chxSoundLoop.Checked;
            ObjectPropertiesBase.SoundStartingTimePoint = ntxSoundStartingTimePoint.GetFloat();

            DNESoundState SoundState = (DNESoundState)Enum.Parse(typeof(DNESoundState),cmbSoundState.Text);
            ObjectPropertiesBase.SoundState = SoundState;

            //Mesh Tab
            ObjectPropertiesBase.MeshUseExisting = chxMeshUseExisting.Checked;
            ObjectPropertiesBase.MeshRotateToTerrain = chxMeshRotateToTerrain.Checked;
            ObjectPropertiesBase.MeshAlignment = (DNEBasePointAlignment)Enum.Parse(typeof(DNEBasePointAlignment), cmbMeshBasePointAlignment.Text);
            ObjectPropertiesBase.MeshTextureColor = selectMeshTextureColor.BColor;
            ObjectPropertiesBase.MeshParticipatesInTerrainHeight = chxMeshParticipatesInTerrainHeight.Checked;
            ObjectPropertiesBase.MeshDisplayItemsAttachedToTerrain = chxDisplayItemsAttachedToTerrain.Checked;
            ObjectPropertiesBase.MeshIsStatic = chxMeshIsStatic.Checked;

            //Procedural geometry Tab
            DNEMcPointCoordSystem proceduralGeometryCoordinateSys = (DNEMcPointCoordSystem)Enum.Parse(typeof(DNEMcPointCoordSystem), cmbProcedualGeomtryCoordinateSys.Text);
            ObjectPropertiesBase.ProceduralGeometryCoordinateSys = proceduralGeometryCoordinateSys;
        }

        #region Private Events

        private void btnObjectPropertiesOk_Click(object sender, EventArgs e)
        {
            SaveItems();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        #endregion        
      
    } 
}