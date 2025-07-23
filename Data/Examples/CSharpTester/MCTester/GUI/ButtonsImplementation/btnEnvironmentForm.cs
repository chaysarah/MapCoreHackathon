using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers.MapWorld;
using MCTester.Managers;

namespace MCTester.GUI.Forms
{
    public partial class btnEnvironmentForm : Form
    {
        IDNMcMapEnvironment m_CurrMapEnvironment;
        private ColorDialog m_ColorDialog;

        public btnEnvironmentForm()
        {
            if (MCTMapFormManager.MapForm.Viewport.MapType == DNEMapType._EMT_3D)
            {
                m_CurrMapEnvironment = MCTMapFormManager.MapForm.Environment;

                if (m_CurrMapEnvironment == null)
                {
                    DialogResult messResult = MessageBox.Show("Before opening this dialog you have to create an environment.\n do you want to create an environment now?",
                                                                "Create Environment",
                                                                MessageBoxButtons.OKCancel);

                    if (messResult == DialogResult.Cancel)
                        return;
                    
                    if (messResult == DialogResult.OK)
                    {
                        m_CurrMapEnvironment = DNMcMapEnvironment.Create(MCTMapFormManager.MapForm.Viewport);
                        MCTMapFormManager.MapForm.Environment = m_CurrMapEnvironment;
                    }
                }

                InitializeComponent();

                m_ColorDialog = new ColorDialog();

                cmbSkyType.Items.AddRange(Enum.GetNames(typeof(DNEMapEnvSkyType)));
                cmbSunType.Items.AddRange(Enum.GetNames(typeof(DNEMapEnvSunType)));
                cmbFogType.Items.AddRange(Enum.GetNames(typeof(DNEMapEnvFogType)));

                rdbCustomType.CheckedChanged += new EventHandler(rdbCustomType_CheckedChanged);
                rdbAllType.CheckedChanged += new EventHandler(rdbAllType_CheckedChanged);
                rdbNoneType.CheckedChanged += new EventHandler(rdbNoneType_CheckedChanged);

                GetCurrEnabledComponents();
                GetCurrVisibleComponents();
                LoadItem();

            }
            else
            {
                MessageBox.Show("Environment can be defined only on 3D map");
                return;            	
            }            
        }

        /// <summary>
        /// Get environment properties parameters
        /// </summary>
        private void LoadItem()
        {

            try
            {
                DNSMcFColor ambientColor = m_CurrMapEnvironment.GetDefaultAmbientLight();
                picAmbienteColor.BackColor = Color.FromArgb((int)(ambientColor.a * 255), (int)(ambientColor.r * 255), (int)(ambientColor.g * 255), (int)(ambientColor.b * 255));
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetDefaultAmbientLight", McEx);
            }

            try
            {
                chxEnableTimeAutoUpdate.Checked = m_CurrMapEnvironment.GetTimeAutoUpdate();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetTimeAutoUpdate", McEx);
            }

            try
            {
                ntxTimeAutoUpdateFactor.SetFloat(m_CurrMapEnvironment.GetTimeAutoUpdateFactor());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetTimeAutoUpdateFactor", McEx);
            }

            try
            {
                DNEMapEnvSkyType skyType;
                string pstrMaterial;
                DNSMcFColor skyColor;

                m_CurrMapEnvironment.GetSkyParams(out skyType, out pstrMaterial, out skyColor);
           
                cmbSkyType.Text = skyType.ToString();
                txtSkyPstrMaterial.Text = pstrMaterial;
                picSkyColor.BackColor = Color.FromArgb((int)(skyColor.a * 255), (int)(skyColor.r * 255), (int)(skyColor.g * 255), (int)(skyColor.b * 255));
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetSkyParams", McEx);
            }

            try
            {
                DNEMapEnvSunType sunType;
                
                m_CurrMapEnvironment.GetSunParams(out sunType);

                cmbSunType.Text = sunType.ToString();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetSunParams", McEx);
            }

            try
            {
                DNEMapEnvFogType fogType;
                DNSMcFColor fogColor;
                float expDensity;
                float linearStart;
                float linearEnd;

                m_CurrMapEnvironment.GetFogParams(out fogType, out fogColor, out expDensity, out linearStart, out linearEnd);

                cmbFogType.Text = fogType.ToString();
                picFogColor.BackColor = Color.FromArgb((int)(fogColor.a * 255), (int)(fogColor.r * 255), (int)(fogColor.g * 255), (int)(fogColor.b * 255));
                ntxFogExpDensity.SetFloat(expDensity);
                ntxFogLinearStart.SetFloat(linearStart);
                ntxFogLinearEnd.SetFloat(linearEnd);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetFogParams", McEx);
            }

            try
            {
                float cloudCover;
                DNSMcFVector2D cloudSpeed;

                m_CurrMapEnvironment.GetCloudsParams(out cloudCover, out cloudSpeed);

                ntxCloudCover.SetFloat(cloudCover);
                ctrl2DCloudsSpeed.SetVector2D(cloudSpeed);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetCloudsParams", McEx);
            }

            try
            {
                DateTime defaultAbsoluteTime = m_CurrMapEnvironment.GetAbsoluteTime();
                ntxAbsoluteTime.Value= defaultAbsoluteTime;
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetAbsoluteTime", McEx);
            }
        }

        /// <summary>
        /// Set environment properties parameters
        /// </summary>
        private void SaveItem()
        {
            try
            {
                DNEMapEnvSkyType skyType = (DNEMapEnvSkyType)Enum.Parse(typeof(DNEMapEnvSkyType), cmbSkyType.Text);

                DNSMcFColor skyColor = new DNSMcFColor((float)(picSkyColor.BackColor.R / 255f), (float)(picSkyColor.BackColor.G / 255f), (float)(picSkyColor.BackColor.B / 255f), (float)(picSkyColor.BackColor.A / 255f));
                m_CurrMapEnvironment.SetSkyParams(skyType, txtSkyPstrMaterial.Text, skyColor);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetSkyParams", McEx);
            }

            try
            {
                DNEMapEnvSunType sunType = (DNEMapEnvSunType)Enum.Parse(typeof(DNEMapEnvSunType), cmbSunType.Text);

                m_CurrMapEnvironment.SetSunParams(sunType);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetSunParams", McEx);
            }

            try
            {
                DNEMapEnvFogType fogType = (DNEMapEnvFogType)Enum.Parse(typeof(DNEMapEnvFogType), cmbFogType.Text);
                DNSMcFColor fogColor = new DNSMcFColor((float)(picFogColor.BackColor.R / 255f), (float)(picFogColor.BackColor.G / 255f), (float)(picFogColor.BackColor.B / 255f), (float)(picFogColor.BackColor.A / 255f));
                m_CurrMapEnvironment.SetFogParams(fogType, fogColor, ntxFogExpDensity.GetFloat(), ntxFogLinearStart.GetFloat(), ntxFogLinearEnd.GetFloat());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetFogParams", McEx);
            }

            try
            {
                m_CurrMapEnvironment.SetCloudsParams(ntxCloudCover.GetFloat(), ctrl2DCloudsSpeed.GetVector2D());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetCloudsParams", McEx);
            }

            try
            {
                DNSMcFColor ambientColor = new DNSMcFColor((float)(picAmbienteColor.BackColor.R / 255f), (float)(picAmbienteColor.BackColor.G / 255f), (float)(picAmbienteColor.BackColor.B / 255f), (float)(picAmbienteColor.BackColor.A / 255f));
                m_CurrMapEnvironment.SetDefaultAmbientLight(ambientColor);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetDefaultAmbientLight", McEx);
            }

            try
            {
                m_CurrMapEnvironment.SetTimeAutoUpdate(chxEnableTimeAutoUpdate.Checked);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetTimeAutoUpdate", McEx);
            }

            try
            {
                m_CurrMapEnvironment.SetTimeAutoUpdateFactor(ntxTimeAutoUpdateFactor.GetFloat());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetTimeAutoUpdateFactor", McEx);
            }

            try
            {

                DateTime timeResult = ntxAbsoluteTime.Value;
                //DateTime.TryParse(ntxAbsoluteTime.Text, out timeResult);
                m_CurrMapEnvironment.SetAbsoluteTime(ref timeResult);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetAbsoluteTime", McEx);
            }

            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }
        
        private void GetCurrVisibleComponents()
        {
            try
            {
                DNEMapEnvComponentType m_VisibleComp = m_CurrMapEnvironment.GetVisibleComponents();

                if ((m_VisibleComp & DNEMapEnvComponentType._ECT_SKY) == DNEMapEnvComponentType._ECT_SKY)
                {
                    chxHideShowSky.Checked = true;
                }
                if ((m_VisibleComp & DNEMapEnvComponentType._ECT_STARS) == DNEMapEnvComponentType._ECT_STARS)
                {
                    chxHideShowStars.Checked = true;
                }
                if ((m_VisibleComp & DNEMapEnvComponentType._ECT_SUN) == DNEMapEnvComponentType._ECT_SUN)
                {
                    chxHideShowSun.Checked = true;
                }
                if ((m_VisibleComp & DNEMapEnvComponentType._ECT_FOG) == DNEMapEnvComponentType._ECT_FOG)
                {
                    chxHideShowFog.Checked = true;
                }
                if ((m_VisibleComp & DNEMapEnvComponentType._ECT_CLOUDS) == DNEMapEnvComponentType._ECT_CLOUDS)
                {
                    chxHideShowClouds.Checked = true;
                }
                if ((m_VisibleComp & DNEMapEnvComponentType._ECT_RAIN) == DNEMapEnvComponentType._ECT_RAIN)
                {
                    chxHideShowRain.Checked = true;
                }
                if ((m_VisibleComp & DNEMapEnvComponentType._ECT_SNOW) == DNEMapEnvComponentType._ECT_SNOW)
                {
                    chxHideShowSnow.Checked = true;
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetVisibleComponents", McEx);
            }
        }

        private void GetCurrEnabledComponents()
        {
            try
            {
                DNEMapEnvComponentType m_EnabledComp = m_CurrMapEnvironment.GetEnabledComponents();

                if ((m_EnabledComp & DNEMapEnvComponentType._ECT_SKY) == DNEMapEnvComponentType._ECT_SKY)
                {
                    chxAddRemSky.Checked = true;
                }
                if ((m_EnabledComp & DNEMapEnvComponentType._ECT_STARS) == DNEMapEnvComponentType._ECT_STARS)
                {
                    chxAddRemStars.Checked = true;
                }
                if ((m_EnabledComp & DNEMapEnvComponentType._ECT_SUN) == DNEMapEnvComponentType._ECT_SUN)
                {
                    chxAddRemSun.Checked = true;
                }
                if ((m_EnabledComp & DNEMapEnvComponentType._ECT_FOG) == DNEMapEnvComponentType._ECT_FOG)
                {
                    chxAddRemFog.Checked = true;
                }
                if ((m_EnabledComp & DNEMapEnvComponentType._ECT_CLOUDS) == DNEMapEnvComponentType._ECT_CLOUDS)
                {
                    chxAddRemClouds.Checked = true;
                }
                if ((m_EnabledComp & DNEMapEnvComponentType._ECT_RAIN) == DNEMapEnvComponentType._ECT_RAIN)
                {
                    chxAddRemRain.Checked = true;
                }
                if ((m_EnabledComp & DNEMapEnvComponentType._ECT_SNOW) == DNEMapEnvComponentType._ECT_SNOW)
                {
                    chxAddRemSnow.Checked = true;
                }

            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetVisibleComponents", McEx);
            }
        }

        /// <summary>
        /// check which component type checkBox was selected
        /// </summary>
        private void ShowHideComponent()
        {
            if (chxHideShowSky.Checked == true)
            {
                try
                {
                    m_CurrMapEnvironment.ShowComponents(DNEMapEnvComponentType._ECT_SKY);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("ShowComponents - SKY", McEx);
                }
            }
            else
            {
                try
                {
                    m_CurrMapEnvironment.HideComponents(DNEMapEnvComponentType._ECT_SKY);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("HideComponents", McEx);
                }
            }

            if (chxHideShowStars.Checked == true)
            {
                try
                {
                    m_CurrMapEnvironment.ShowComponents(DNEMapEnvComponentType._ECT_STARS);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("ShowComponents - STARS", McEx);
                }
            }
            else
            {
                try
                {
                    m_CurrMapEnvironment.HideComponents(DNEMapEnvComponentType._ECT_STARS);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("HideComponents", McEx);
                }
            }

            if (chxHideShowSun.Checked == true)
            {
                try
                {
                    m_CurrMapEnvironment.ShowComponents(DNEMapEnvComponentType._ECT_SUN);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("ShowComponents - SUN", McEx);
                }
            }
            else
            {
                try
                {
                    m_CurrMapEnvironment.HideComponents(DNEMapEnvComponentType._ECT_SUN);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("HideComponents", McEx);
                }
            }

            if (chxHideShowFog.Checked == true)
            {
                try
                {
                    m_CurrMapEnvironment.ShowComponents(DNEMapEnvComponentType._ECT_FOG);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("ShowComponents - FOG", McEx);
                }
            }
            else
            {
                try
                {
                    m_CurrMapEnvironment.HideComponents(DNEMapEnvComponentType._ECT_FOG);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("HideComponents", McEx);
                }
            }

            if (chxHideShowClouds.Checked == true)
            {
                try
                {
                    m_CurrMapEnvironment.ShowComponents(DNEMapEnvComponentType._ECT_CLOUDS);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("ShowComponents - CLOUDS", McEx);
                }
            }
            else
            {
                try
                {
                    m_CurrMapEnvironment.HideComponents(DNEMapEnvComponentType._ECT_CLOUDS);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("HideComponents", McEx);
                }
            }

            if (chxHideShowRain.Checked == true)
            {
                try
                {
                    m_CurrMapEnvironment.ShowComponents(DNEMapEnvComponentType._ECT_RAIN);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("ShowComponents - RAIN", McEx);
                }
            }
            else
            {
                try
                {
                    m_CurrMapEnvironment.HideComponents(DNEMapEnvComponentType._ECT_RAIN);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("HideComponents", McEx);
                }
            }

            if (chxHideShowSnow.Checked == true)
            {
                try
                {
                    m_CurrMapEnvironment.ShowComponents(DNEMapEnvComponentType._ECT_SNOW);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("ShowComponents - SNOW", McEx);
                }
            }
            else
            {
                try
                {
                    m_CurrMapEnvironment.HideComponents(DNEMapEnvComponentType._ECT_SNOW);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("HideComponents", McEx);
                }
                
            }

            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }

        private void EnableDisableComponent()
        {
            try
            {
                if (chxAddRemSky.Checked == true)
                {
                    m_CurrMapEnvironment.EnableComponents(DNEMapEnvComponentType._ECT_SKY);
                }
                else
                {
                    m_CurrMapEnvironment.DisableComponents(DNEMapEnvComponentType._ECT_SKY);
                }

                if (chxAddRemStars.Checked == true)
                {
                    m_CurrMapEnvironment.EnableComponents(DNEMapEnvComponentType._ECT_STARS);
                }
                else
                {
                    m_CurrMapEnvironment.DisableComponents(DNEMapEnvComponentType._ECT_STARS);
                }

                if (chxAddRemSun.Checked == true)
                {
                    m_CurrMapEnvironment.EnableComponents(DNEMapEnvComponentType._ECT_SUN);
                }
                else
                {
                    m_CurrMapEnvironment.DisableComponents(DNEMapEnvComponentType._ECT_SUN);
                }

                if (chxAddRemFog.Checked == true)
                {
                    m_CurrMapEnvironment.EnableComponents(DNEMapEnvComponentType._ECT_FOG);
                }
                else
                {
                    m_CurrMapEnvironment.DisableComponents(DNEMapEnvComponentType._ECT_FOG);
                }

                if (chxAddRemClouds.Checked == true)
                {
                    m_CurrMapEnvironment.EnableComponents(DNEMapEnvComponentType._ECT_CLOUDS);
                }
                else
                {
                    m_CurrMapEnvironment.DisableComponents(DNEMapEnvComponentType._ECT_CLOUDS);
                }

                if (chxAddRemRain.Checked == true)
                {
                    m_CurrMapEnvironment.EnableComponents(DNEMapEnvComponentType._ECT_RAIN);
                }
                else
                {
                    m_CurrMapEnvironment.DisableComponents(DNEMapEnvComponentType._ECT_RAIN);
                }

                if (chxAddRemSnow.Checked == true)
                {
                    m_CurrMapEnvironment.EnableComponents(DNEMapEnvComponentType._ECT_SNOW);
                }
                else
                {
                    m_CurrMapEnvironment.DisableComponents(DNEMapEnvComponentType._ECT_SNOW);
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("EnableComponents & DisableComponents", McEx);
            }

            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
            
        }

        #region Events
        /// <summary>
        /// Disable and uncheck all check boxes when the 'None' radio button is chosen  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void rdbNoneType_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbNoneType.Checked == true)
            {
                chxAddRemSky.Checked = false;
                chxAddRemSky.Enabled = false;
                chxHideShowSky.Checked = false;
                chxHideShowSky.Enabled = false;

                chxAddRemStars.Checked = false;
                chxAddRemStars.Enabled = false;
                chxHideShowStars.Checked = false;
                chxHideShowStars.Enabled = false;

                chxAddRemSun.Checked = false;
                chxAddRemSun.Enabled = false;
                chxHideShowSun.Checked = false;
                chxHideShowSun.Enabled = false;

                chxAddRemFog.Checked = false;
                chxAddRemFog.Enabled = false;
                chxHideShowFog.Checked = false;
                chxHideShowFog.Enabled = false;

                chxAddRemClouds.Checked = false;
                chxAddRemClouds.Enabled = false;
                chxHideShowClouds.Checked = false;
                chxHideShowClouds.Enabled = false;

                chxAddRemRain.Checked = false;
                chxAddRemRain.Enabled = false;
                chxHideShowRain.Checked = false;
                chxHideShowRain.Enabled = false;

                chxAddRemSnow.Checked = false;
                chxAddRemSnow.Enabled = false;
                chxHideShowSnow.Checked = false;
                chxHideShowSnow.Enabled = false;
            }
        }
        
        /// <summary>
        /// Disable and check all check boxes when the 'All' radio button is chosen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void rdbAllType_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbAllType.Checked == true)
            {
                chxAddRemSky.Checked = true;
                chxAddRemSky.Enabled = false;
                chxHideShowSky.Checked = true;
                chxHideShowSky.Enabled = false;

                chxAddRemStars.Checked = true;
                chxAddRemStars.Enabled = false;
                chxHideShowStars.Checked = true;
                chxHideShowStars.Enabled = false;

                chxAddRemSun.Checked = true;
                chxAddRemSun.Enabled = false;
                chxHideShowSun.Checked = true;
                chxHideShowSun.Enabled = false;

                chxAddRemFog.Checked = true;
                chxAddRemFog.Enabled = false;
                chxHideShowFog.Checked = true;
                chxHideShowFog.Enabled = false;

                chxAddRemClouds.Checked = true;
                chxAddRemClouds.Enabled = false;
                chxHideShowClouds.Checked = true;
                chxHideShowClouds.Enabled = false;

                chxAddRemRain.Checked = true;
                chxAddRemRain.Enabled = false;
                chxHideShowRain.Checked = true;
                chxHideShowRain.Enabled = false;

                chxAddRemSnow.Checked = true;
                chxAddRemSnow.Enabled = false;
                chxHideShowSnow.Checked = true;
                chxHideShowSnow.Enabled = false;
            }

        }

        /// <summary>
        /// Enable all check boxes when the 'Custom' radio button is chosen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void rdbCustomType_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbCustomType.Checked == true)
            {
                chxAddRemSky.Enabled = true;
                chxHideShowSky.Enabled = true;

                chxAddRemStars.Enabled = true;
                chxHideShowStars.Enabled = true;

                chxAddRemSun.Enabled = true;
                chxHideShowSun.Enabled = true;

                chxAddRemFog.Enabled = true;
                chxHideShowFog.Enabled = true;

                chxAddRemClouds.Enabled = true;
                chxHideShowClouds.Enabled = true;

                chxAddRemRain.Enabled = true;
                chxHideShowRain.Enabled = true;

                chxAddRemSnow.Enabled = true;
                chxHideShowSnow.Enabled = true;
            }
        }

        private void btnIncreaseTime_Click(object sender, EventArgs e)
        {
            try
            {
                m_CurrMapEnvironment.IncrementTime(ntxIncrementSecTime.GetInt32());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("", McEx);
            }

        }

        private void BtnGeneralOk_Click(object sender, EventArgs e)
        {
            btnEnvironmentApply_Click(sender, e);
            this.Close();
        }

        private void btnEnvironmentApply_Click(object sender, EventArgs e)
        {
            SaveItem();
            EnableDisableComponent();
            ShowHideComponent();
        }

        private void btnAmbientColor_Click(object sender, EventArgs e)
        {
            if (m_ColorDialog.ShowDialog() == DialogResult.OK)
            {
                picAmbienteColor.BackColor = m_ColorDialog.Color;
            }
        }

        private void btnSkyColor_Click(object sender, EventArgs e)
        {
            if (m_ColorDialog.ShowDialog() == DialogResult.OK)
            {
                picSkyColor.BackColor = m_ColorDialog.Color;
            }
        }

        private void nudSkyColor_ValueChanged(object sender, EventArgs e)
        {
            picSkyColor.BackColor = Color.FromArgb((int)nudSkyColor.Value, picSkyColor.BackColor);
        }

        private void btnFogColor_Click(object sender, EventArgs e)
        {
            if (m_ColorDialog.ShowDialog() == DialogResult.OK)
            {
                picFogColor.BackColor = m_ColorDialog.Color;
            }
        }

        private void nudFogColor_ValueChanged(object sender, EventArgs e)
        {
            picFogColor.BackColor = Color.FromArgb((int)nudFogColor.Value, picFogColor.BackColor);
        }

        #endregion       
    }
}
