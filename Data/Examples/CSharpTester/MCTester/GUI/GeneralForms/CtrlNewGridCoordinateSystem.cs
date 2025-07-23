using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MapCore.Common;

namespace MCTester.General_Forms
{

    public partial class CtrlNewGridCoordinateSystem : UserControl
    {
        private IDNMcGridCoordinateSystem m_ExistCoordSys;
        private IDNMcGridCoordinateSystem m_GridCoordinateSystem = null;

        public CtrlNewGridCoordinateSystem()
        {
            InitializeComponent();

            cmbGridCoordSysType.Items.AddRange(Enum.GetNames(typeof(DNEGridCoordSystemType)));
            cmbGridCoordSysType.Text = DNEGridCoordSystemType._EGCS_GEOGRAPHIC.ToString();

            cmbDatum.Items.AddRange(Enum.GetNames(typeof(DNEDatumType)));
            cmbDatum.Text = DNEDatumType._EDT_WGS84.ToString();

        }

        public int GetOKBtnLocation()
        {
            return gbUserDefined.Location.Y + gbUserDefined.Size.Height + 50;
        }

        private void cmbGridCoordSysType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DNEGridCoordSystemType gridCoordSys = (DNEGridCoordSystemType)Enum.Parse(typeof(DNEGridCoordSystemType), cmbGridCoordSysType.Text);

            cgbDatumParams.Visible = false;
            gbUserDefined.Visible = false;
            gbGeneric.Visible = false;

            lblDatum.Visible = cmbDatum.Visible = false;
            ntxZone.Visible = lblZone.Visible = false;

            switch (gridCoordSys)
            {
                case DNEGridCoordSystemType._EGCS_GEOCENTRIC:
                    lblDatum.Visible = cmbDatum.Visible = true;
                    cgbDatumParams.Visible = true;
                    break;
                case DNEGridCoordSystemType._EGCS_GEOGRAPHIC:
                    lblDatum.Visible = cmbDatum.Visible = true;
                    cgbDatumParams.Visible = true;
                    break;
                case DNEGridCoordSystemType._EGCS_INDIA_LCC:
                    break;
                case DNEGridCoordSystemType._EGCS_KKJ:
                    break;
                case DNEGridCoordSystemType._EGCS_S42:
                    lblDatum.Visible = cmbDatum.Visible = true;
                    ntxZone.Visible = lblZone.Visible = true;
                    cgbDatumParams.Visible = true;
                    break;
                case DNEGridCoordSystemType._EGCS_TM_USER_DEFINED:
                    lblDatum.Visible = cmbDatum.Visible = true;
                    ntxZone.Visible = lblZone.Visible = true;
                    gbUserDefined.Visible = true;
                    cgbDatumParams.Visible = true;
                    break;
                case DNEGridCoordSystemType._EGCS_UTM:
                    ntxZone.Visible = lblZone.Visible = true;
                    lblDatum.Visible = cmbDatum.Visible = true;
                    cgbDatumParams.Visible = true;
                    break;
                case DNEGridCoordSystemType._EGCS_GENERIC_GRID:
                    gbGeneric.Visible = true;
                    break;
            }

            if (gridCoordSys == DNEGridCoordSystemType._EGCS_UTM)
                ntxZone.Text = "36";
            else
                ntxZone.Text = "";
        }

        public void ShowCurrGridCoordSysParams(IDNMcGridCoordinateSystem ExistCoordSys, bool isEnable = false)
        {
            try
            {
                m_ExistCoordSys = ExistCoordSys;
                DNEGridCoordSystemType type = ExistCoordSys.GetGridCoorSysType();
                cmbGridCoordSysType.Text = type.ToString();

                DNSDatumParams datumParams = ExistCoordSys.GetDatumParams();

                DNEDatumType datumType = ExistCoordSys.GetDatum();
                int zone = ExistCoordSys.GetZone();

                switch (type)
                {
                    case DNEGridCoordSystemType._EGCS_GEOCENTRIC:
                        cmbDatum.Text = datumType.ToString();
                        break;
                    case DNEGridCoordSystemType._EGCS_GEOGRAPHIC:
                        SetDatumType(datumType);
                        SetDatumParams(datumParams);
                        break;
                    case DNEGridCoordSystemType._EGCS_INDIA_LCC:
                        break;
                    case DNEGridCoordSystemType._EGCS_KKJ:
                        break;
                    case DNEGridCoordSystemType._EGCS_S42:
                        SetDatumType(datumType);
                        SetZone(ExistCoordSys.GetZone());
                        SetDatumParams(datumParams);

                        break;
                    case DNEGridCoordSystemType._EGCS_TM_USER_DEFINED:
                        SetDatumType(datumType);
                        SetZone(ExistCoordSys.GetZone());
                        SetDatumParams(datumParams);

                        DNSTMGridParams tmUserDefined = ((IDNMcGridTMUserDefined)ExistCoordSys).GetTMParams();
                        ntxCentralMeridian.SetDouble(tmUserDefined.dCentralMeridian);
                        ntxFalseEasting.SetDouble(tmUserDefined.dFalseEasting);
                        ntxFalseNorthing.SetDouble(tmUserDefined.dFalseNorthing);
                        ntxLatitudeOfGridOrigin.SetDouble(tmUserDefined.dLatitudeOfGridOrigin);
                        ntxScaleFactor.SetDouble(tmUserDefined.dScaleFactor);
                        break;
                    case DNEGridCoordSystemType._EGCS_UTM:
                        SetDatumType(datumType);
                        SetZone(ExistCoordSys.GetZone());
                        SetDatumParams(datumParams);

                        break;
                    case DNEGridCoordSystemType._EGCS_MGRS:
                        break;
                    case DNEGridCoordSystemType._EGCS_NZMG:
                        break;
                    case DNEGridCoordSystemType._EGCS_GENERIC_GRID:
                        string[] pastrCreateParams;
                        bool isSRID;
                        (ExistCoordSys as IDNMcGridGeneric).GetCreateParams(out pastrCreateParams, out isSRID);
                        SetGridGenericParams(pastrCreateParams, isSRID);
                        break;
                }

                cmbGridCoordSysType.Enabled = isEnable;
                cmbDatum.Enabled = isEnable;
                ntxZone.Enabled = isEnable;
                cgbDatumParams.Enabled = isEnable;
                gbUserDefined.Enabled = isEnable;
                gbGeneric.Enabled = isEnable;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("ShowCurrGridCoordSysParams", McEx);
            }
        }

        private void rbGenericBySrid_CheckedChanged(object sender, EventArgs e)
        {
            txGenericSrid.Enabled = rbGenericBySrid.Checked;
        }

        private void rbGenericByInitString_CheckedChanged(object sender, EventArgs e)
        {
            txGenericInitString.Enabled = rbGenericByInitString.Checked;
        }

        private void rbGenericByArgs_CheckedChanged(object sender, EventArgs e)
        {
            txGenericArgs.Enabled = rbGenericByArgs.Checked;
        }

        private void btnGetInitString_Click(object sender, EventArgs e)
        {
            if (txGenericSrid.Text != string.Empty)
            {
                try
                {
                    string initString = DNMcGridGeneric.GetFullInitializationString(txGenericSrid.Text);
                    txGenericInitString.Text = initString;
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("DNMcGridGeneric.GetFullInitializationString", McEx);
                }
            }
            else
            {
                MessageBox.Show("Srid code is missing", "No data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        public DNEGridCoordSystemType GetGridCoordSystemType()
        {
            return (DNEGridCoordSystemType)Enum.Parse(typeof(DNEGridCoordSystemType), cmbGridCoordSysType.Text);
        }


        public void SetGridCoordSystemType(DNEGridCoordSystemType gridGridCoordSystemType)
        {
            cmbGridCoordSysType.Text = gridGridCoordSystemType.ToString();
        }

        public DNEDatumType GetDatumType()
        {
            return (DNEDatumType)Enum.Parse(typeof(DNEDatumType), cmbDatum.Text);
        }

        public void SetDatumType(DNEDatumType datumType)
        {
            cmbDatum.Text = datumType.ToString();
        }


        public int GetZone()
        {
            return ntxZone.GetInt32();
        }

        public void SetZone(int zone)
        {
            ntxZone.SetInt(zone);
        }

        public DNSDatumParams GetDatumParams()
        {
            if(cgbDatumParams.Checked)
                return new DNSDatumParams(ntx_A.GetDouble(),
                                        ntx_F.GetDouble(),
                                        ntx_DX.GetDouble(),
                                        ntx_DY.GetDouble(),
                                        ntx_DZ.GetDouble(),
                                        ntx_Rx.GetDouble(),
                                        ntx_Ry.GetDouble(),
                                        ntx_Rz.GetDouble(),
                                        ntx_S.GetDouble());
            else
                return null;
        }

        public void SetDatumParams(DNSDatumParams datumParams)
        {
            if (datumParams != null)
            {
                ntx_A.SetDouble(datumParams.dA);
                ntx_DX.SetDouble(datumParams.dDX);
                ntx_DY.SetDouble(datumParams.dDY);
                ntx_DZ.SetDouble(datumParams.dDZ);
                ntx_F.SetDouble(datumParams.dF);
                ntx_Rx.SetDouble(datumParams.dRx);
                ntx_Ry.SetDouble(datumParams.dRy);
                ntx_Rz.SetDouble(datumParams.dRz);
                ntx_S.SetDouble(datumParams.dS);
            }
            else
            {
                cgbDatumParams.Checked = false;
            }
        }

        public DNSTMGridParams GetTMGridParams()
        {
            DNSTMGridParams gridParams = new DNSTMGridParams();
            gridParams.dCentralMeridian = ntxCentralMeridian.GetDouble();
            gridParams.dFalseEasting = ntxFalseEasting.GetDouble();
            gridParams.dFalseNorthing = ntxFalseNorthing.GetDouble();
            gridParams.dLatitudeOfGridOrigin = ntxLatitudeOfGridOrigin.GetDouble();
            gridParams.dScaleFactor = ntxScaleFactor.GetDouble();
            gridParams.dZoneWidth = ntxZoneWidth.GetDouble();
            return gridParams;
        }

        public void SetTMGridParams(DNSTMGridParams gridParams)
        {
            ntxCentralMeridian.SetDouble(gridParams.dCentralMeridian);
            ntxFalseEasting.SetDouble(gridParams.dFalseEasting);
            ntxFalseNorthing.SetDouble(gridParams.dFalseNorthing);
            ntxLatitudeOfGridOrigin.SetDouble(gridParams.dLatitudeOfGridOrigin);
            ntxScaleFactor.SetDouble(gridParams.dScaleFactor);
            ntxZoneWidth.SetDouble(gridParams.dZoneWidth);
        }

        public void GetGridGenericParams(out string[] argParams, out bool isSrid)
        {
            argParams = null;
            isSrid = false;

            if (GetGridCoordSystemType() == DNEGridCoordSystemType._EGCS_GENERIC_GRID)
            {
                if (rbGenericBySrid.Checked)
                {
                    isSrid = true;
                    argParams = new string[1] { txGenericSrid.Text };
                }
                else if (rbGenericByInitString.Checked)
                {
                    argParams = new string[1] { txGenericInitString.Text };
                }
                else
                {
                    argParams = txGenericArgs.Text.Split(" ".ToCharArray());
                }
            }
        }

        public void SetGridGenericParams(string[] pastrCreateParams, bool isSRID)
        {
            txGenericSrid.Text = txGenericInitString.Text = txGenericArgs.Text = "";

            if (pastrCreateParams != null && pastrCreateParams.Length > 0)
            {
                if (isSRID)
                {
                    rbGenericBySrid.Checked = true;
                    txGenericSrid.Text = pastrCreateParams[0];
                }
                else
                {
                    if (pastrCreateParams.Length == 1)
                    {
                        rbGenericByInitString.Checked = true;
                        txGenericInitString.Text = pastrCreateParams[0];
                    }
                    else
                    {
                        rbGenericByArgs.Checked = true;
                        string args = "";
                        foreach (string str in pastrCreateParams)
                        {
                            args += str + " ";
                        }
                        if (args != "")
                            args = args.Substring(0, args.Length - 1);

                        txGenericArgs.Text = args;
                    }
                }
            }
        }

        public IDNMcGridCoordinateSystem CreateCoordinateSystem()
        {
            DNEDatumType datum;
            DNSDatumParams datumParams = null;
            DNEGridCoordSystemType gridCoordSys = (DNEGridCoordSystemType)Enum.Parse(typeof(DNEGridCoordSystemType), cmbGridCoordSysType.Text);

            switch (gridCoordSys)
            {
                case DNEGridCoordSystemType._EGCS_GEOGRAPHIC:

                    datum = GetDatumType();
                    datumParams = GetDatumParams();
                    try
                    {
                        m_GridCoordinateSystem = DNMcGridCoordSystemGeographic.Create(datum, datumParams);
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("DNMcGridCoordSystemGeographic.Create", McEx);
                    }

                    break;
                case DNEGridCoordSystemType._EGCS_GEOCENTRIC:

                    datum = GetDatumType();
                    datumParams = GetDatumParams();

                    try
                    {
                        m_GridCoordinateSystem = DNMcGridCoordSystemGeocentric.Create(datum, datumParams);
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("DNMcGridCoordSystemGeocentric.Create", McEx);
                    }
                    break;
                case DNEGridCoordSystemType._EGCS_TM_USER_DEFINED:

                    datum = GetDatumType();
                    datumParams = GetDatumParams();

                    DNSTMGridParams gridParams = GetTMGridParams();

                    try
                    {
                        m_GridCoordinateSystem = DNMcGridTMUserDefined.Create(gridParams, GetZone(), datum, datumParams);
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("DNMcGridTMUserDefined.Create", McEx);
                    }
                    break;
                case DNEGridCoordSystemType._EGCS_UTM:

                    datum = GetDatumType();
                    datumParams = GetDatumParams();
                    try
                    {
                        m_GridCoordinateSystem = DNMcGridUTM.Create(GetZone(), datum, datumParams);
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("DNMcGridUTM.Create", McEx);
                    }
                    break;
                case DNEGridCoordSystemType._EGCS_NEW_ISRAEL:
                    try
                    {
                        m_GridCoordinateSystem = DNMcGridNewIsrael.Create();
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("DNMcGridNewIsrael.Create", McEx);
                    }
                    break;
                case DNEGridCoordSystemType._EGCS_RSO_SINGAPORE:
                    try
                    {
                        m_GridCoordinateSystem = DNMcGridRSOSingapore.Create();
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("DNMcGridRSOSingapore.Create", McEx);
                    }
                    break;
                case DNEGridCoordSystemType._EGCS_NZMG:
                    try
                    {
                        m_GridCoordinateSystem = DNMcGridNZMG.Create();
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("DNMcGridNZMG.Create", McEx);
                    }
                    break;
                case DNEGridCoordSystemType._EGCS_S42:
                    datum = GetDatumType();
                    datumParams = GetDatumParams();

                    try
                    {
                        m_GridCoordinateSystem = DNMcGridS42.Create(GetZone(), datum, datumParams);
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("DNMcGridS42.Create", McEx);
                    }
                    break;
                case DNEGridCoordSystemType._EGCS_RT90:
                    try
                    {
                        m_GridCoordinateSystem = DNMcGridRT90.Create();
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("DNMcGridRT90.Create", McEx);
                    }
                    break;
                case DNEGridCoordSystemType._EGCS_MGRS:
                    try
                    {
                        m_GridCoordinateSystem = DNMcGridMGRS.Create();
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("DNMcGridMGRS.Create", McEx);
                    }
                    break;
                case DNEGridCoordSystemType._EGCS_BNG:
                    try
                    {
                        m_GridCoordinateSystem = DNMcGridBNG.Create();
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("IDNMcGridBNG.Create", McEx);
                    }
                    break;
                case DNEGridCoordSystemType._EGCS_GEOREF:
                    try
                    {
                        m_GridCoordinateSystem = DNMcGridGEOREF.Create();
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("DNMcGridGEOREF.Create", McEx);
                    }
                    break;
                case DNEGridCoordSystemType._EGCS_GARS:
                    try
                    {
                        m_GridCoordinateSystem = DNMcGridGARS.Create();
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("IDNMcGridBNG.Create", McEx);
                    }
                    break;
                case DNEGridCoordSystemType._EGCS_IRISH:
                    try
                    {
                        m_GridCoordinateSystem = DNMcGridIrish.Create();
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("IDNMcGridBNG.Create", McEx);
                    }
                    break;
                case DNEGridCoordSystemType._EGCS_KKJ:
                    break;
                case DNEGridCoordSystemType._EGCS_INDIA_LCC:
                    break;
                case DNEGridCoordSystemType._EGCS_GENERIC_GRID:
                    try
                    {
                        if (rbGenericBySrid.Checked)
                        {
                            m_GridCoordinateSystem = DNMcGridGeneric.Create(txGenericSrid.Text);
                        }
                        else if (rbGenericByInitString.Checked)
                        {
                            m_GridCoordinateSystem = DNMcGridGeneric.Create(txGenericInitString.Text, false);
                        }
                        else
                        {
                            string[] args = txGenericArgs.Text.Split(" ".ToCharArray());
                            m_GridCoordinateSystem = DNMcGridGeneric.Create(args);
                        }
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("IDNMcGridGeneric.Create", McEx);
                    }
                    break;
            }

            return m_GridCoordinateSystem;
        }

       
    }
}
