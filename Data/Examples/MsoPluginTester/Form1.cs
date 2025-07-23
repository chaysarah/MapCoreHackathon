using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCMsoPluginTester
{
    public partial class Form1 : Form
    {
        DbConnectionParams m_dbConnProps;
        IDNMcOsmConnection m_dbConn = null;
        IDNMcOsmConnection m_dbRoutingConn = null;

        Stack<IDNMcOsmConnection> m_dbConnections = new Stack<IDNMcOsmConnection>();
        Stack<IDNMcOsmConnection> m_dbRoutingConnections = new Stack<IDNMcOsmConnection>();

        String m_currLanguage = "";

        //bool txt1Valid = true;
        //bool txt2Valid = true;

        bool btnQueryEnabled = false;
        bool bDoNotLoadState = false;

        public Form1()
        {
            InitializeComponent();
            m_dbConnProps = new DbConnectionParams();
            propertyGrid1.SelectedObject = m_dbConnProps;
            tabControl1.TabPages.Remove(tabPage3);
        }

        private void LoadState()
        {
            if (bDoNotLoadState)
            {
                bDoNotLoadState = false;
                return;
            }

            cmbState.Items.Clear();
            cmbState.Text = "";

            IDNMcOsmGeocoder geoCoder = DNMcOsmGeocoder.Create(m_dbConn);

            DNSMcOsmStateInfo[] stateInfo = geoCoder.EnumStates(m_currLanguage);
            foreach (DNSMcOsmStateInfo info in stateInfo)
            {
                cmbState.Items.Add(info.strName);
            }
        }

        private void LoadCity()
        {
            LoadState();

            cmbCity.Items.Clear();
            cmbCity.Text = "";

            cmbStreet.Items.Clear();
            cmbStreet.Text = "";

            IDNMcOsmGeocoder geoCoder = DNMcOsmGeocoder.Create(m_dbConn);

            DNSMcOsmCityInfo[] cityInfo = null;
            if (cmbState.Items.Count > 0 && cmbState.SelectedIndex >= 0)
            {
                cityInfo = geoCoder.EnumCities(cmbState.Items[cmbState.SelectedIndex].ToString(), m_currLanguage);
            }
            else
            {
                cityInfo = geoCoder.EnumCities(null, m_currLanguage);
            }
            foreach (DNSMcOsmCityInfo info in cityInfo)
            {
                if (m_dbConnProps.ExcludeVillages && info.strType == "village")
                {
                    continue;
                }
                if (cmbCity.Items.Contains(info.strName))
                {
                    continue;
                }
                cmbCity.Items.Add(info.strName);
            }


        }

        private void LoadTypes()
        {
            sourceList.Items.Clear();
            IDNMcOsmGeocoder geocoder = DNMcOsmGeocoder.Create(m_dbConn);

            string[] types = geocoder.EnumPlaceTypes();
            foreach (string aType in types)
            {
                listTypes.Items.Add(aType);
                sourceList.Items.Add(aType);
            }

        }

        private void OnConnectionChanged()
        {
            connCounter.Text = m_dbConnections.Count.ToString();
            btnDisconnect.Enabled = m_dbConnections.Count > 0;
            if (m_dbConnections.Count >= 1)
            {
                groupBox1.Enabled = true;
                richTextBox1.Text = "";
                if (m_dbConnProps.DataSource == DbConnectionParams.DataSourceEnum.Database)
                {
                    LoadCity();
                }
                LoadTypes();

                grpRvrsGeocoding.Enabled = true;
            }
            else
            {
                groupBox1.Enabled = false;
                listTypes.Items.Clear();
                grpRvrsGeocoding.Enabled = false;
            }

            if (m_dbConnections.Count >= 1)
            {
                btnRoute.Enabled = true;
            }
            else
            {
                btnRoute.Enabled = false;
            }
        }

        private void GetStreetAndHouse(out string street, out string houseNum)
        {
            street = cmbStreet.Text;
            houseNum = "";

            string[] args = cmbStreet.Text.Split(" ".ToCharArray());
            if (args.Length > 1)
            {
                int houseNo;
                bool bIsFirst = true;
                if (!int.TryParse(args[0], out houseNo))
                {
                    int.TryParse(args[args.Length - 1], out houseNo);
                    bIsFirst = false;
                }

                if (houseNo != 0)
                {
                    if (bIsFirst)
                    {
                        street = cmbStreet.Text.Substring(args[0].Length, cmbStreet.Text.Length - 2 - args[args.Length - 1].Length).Trim();
                        houseNum = args[0];
                    }
                    else
                    {
                        street = cmbStreet.Text.Substring(0, cmbStreet.Text.Length - args[args.Length - 1].Length).Trim();
                        houseNum = args[args.Length - 1];
                    }
                }
            }
        }

        private void ShowResults(IDNMcOsmGeocoder geocoder, DNSMcOsmPlaceInfo[] places, String[] geoms, long[] osmIds)
        {
            richTextBox1.Clear();
            string rbText = "";
            foreach (DNSMcOsmPlaceInfo place in places)
            {
                if (place.strType == "primary" ||
                        place.strType == "secondary" ||
                        place.strType == "tertiary" ||
                        place.strType == "residential")
                {
                    rbText += string.Format("Type: ({0}), {1} {2}", place.strType, place.strName, place.strCity);
                }
                else if (place.strType == "house_number")
                {
                    rbText += string.Format("Type: ({0}), {1} {2} {3}", place.strType, place.strHouseNum, place.strStreetName, place.strCity);
                }
                else
                {
                    rbText += string.Format("Type: ({0}), {1} - {2} {3} {4}", place.strType, place.strName, place.strHouseNum, place.strStreetName, place.strCity);
                }

                if (place.strState != string.Empty)
                {
                    rbText += string.Format(" State: {0}", place.strState);
                }

                if (place.strCountry != string.Empty)
                {
                    rbText += string.Format(" Country: {0}\n", place.strCountry);
                }


                int idx = 0;
                if (m_dbConnProps.DataSource == DbConnectionParams.DataSourceEnum.Database)
                {
                    bool bFound = false;
                    for (int i = 0; i < geoms.Length; i++)
                    {
                        if ((long)place.nOsmId == osmIds[i])
                        {
                            bFound = true;
                            idx = i;
                            break;
                        }
                    }

                    if (!bFound)
                    {
                        continue;
                    }
                }

                string coordType;
                DNSMcVector3D[] coords;

                if (m_dbConnProps.DataSource == DbConnectionParams.DataSourceEnum.Database)
                {
                    geocoder.GetGeometryCoord(geoms[idx], out coords, out coordType);

                    if (place.strStreetName != string.Empty)
                    {
                        rbText += "\t";
                    }
                    rbText += string.Format("\tLocation: [{0}]", coordType);
                    for (int i = 0; i < coords.Length; i++)
                    {
                        rbText += string.Format(" ({0:g9}", coords[i].x);
                        rbText += " , ";
                        rbText += string.Format("{0:g9})", coords[i].y);
                    }
                }
                else
                {
                    rbText += string.Format("\tLocation: {0}", place.strGeometry);
                }
                rbText += "\n";
            }
            richTextBox1.Text = rbText;
        }

        private void QueryByFields(IDNMcOsmGeocoder geocoder)
        {
            string street, house;
            GetStreetAndHouse(out street, out house);

            DNSMcOsmPlaceInfo[] result = geocoder.QueryForAddress(cmbCity.Text, street, house, null, m_currLanguage);
            if (result != null)
            {
                long[] osmIds = null;
                string[] geoms = null;
                if (m_dbConnProps.DataSource == DbConnectionParams.DataSourceEnum.Database)
                {
                    long[] nOsmIds = new long[result.Length];
                    for (int i = 0; i < result.Length; i++)
                    {
                        nOsmIds[i] = result[i].nOsmId;
                    }
                    geocoder.GetGeometries(nOsmIds, out geoms, out osmIds);
                }
                ShowResults(geocoder, result, geoms, osmIds);
            }
            else
            {
                MessageBox.Show("Unexpected error occurred", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void SearchName(IDNMcOsmGeocoder geocoder)
        {
            string address = tbAddress.Text;
            DNSMcOsmAddressSearchResult[] result = geocoder.SearchForAddress(address, m_currLanguage);
            richTextBox1.Text = "";
            string txt = "";


            Int64[] osmIds = new long[result.Length];
            for (int i = 0; i < osmIds.Length; i++)
            {
                osmIds[i] = result[i].nOsmId;
            }

            string [] geoms = new string[] { };
            long[] retIds = new long[] { };

            if (m_dbConnProps.DataSource == DbConnectionParams.DataSourceEnum.Database)
            {
                geocoder.GetGeometries(osmIds, out geoms, out retIds);
                if (retIds == null)
                {
                    txt += "INVALID QUERY!!!\n";
                    richTextBox1.Text = txt;
                    return;
                }
            }

            List<long> ids = new List<long>(retIds);

            foreach (DNSMcOsmAddressSearchResult item in result)
            {
                txt += string.Format("ID:\t\t\t{0}\n", item.nOsmId.ToString());
                txt += string.Format("Type:\t\t{0}\n", item.strType);
                txt += string.Format("Name:\t\t{0}\n", item.strPlaceName);
                if (item.strPlaceName != item.strStreet)
                {
                    txt += string.Format("Street:\t{0}\n", item.strStreet);
                }
                if (!string.IsNullOrEmpty(item.strHouseNumber))
                {
                    txt += string.Format("House No.:\t{0}\n", item.strHouseNumber);
                }
                txt += string.Format("Neighbourhood:\t{0}\n", item.strNeighbourhood);
                txt += string.Format("Suburb:\t\t{0}\n", item.strSuburb);
                txt += string.Format("City:\t\t{0}\n", item.strCity);
                txt += string.Format("Country:\t\t{0}\n", item.strCountry);
                txt += string.Format("State:\t\t{0}\n", item.strState);
                txt += string.Format("Postcode:\t{0}\n", item.strPostcode);
                txt += string.Format("Rank:\t\t{0}\n", item.nRank.ToString());
                txt += string.Format("Grade:\t\t{0}\n", item.nGrade.ToString());

                if (item.nRank >= 28)
                {
                    int idx = ids.IndexOf(item.nOsmId);
                    
                    DNSMcVector3D [] coords = null;
                    string coordType;
                    if (m_dbConnProps.DataSource == DbConnectionParams.DataSourceEnum.HTML)
                    {
                        geocoder.GetGeometryCoord(item.strGeometry, out coords, out coordType);
                        txt += string.Format("Pos:\t{0}", item.strGeometry);
                    }
                    else
                    {
                        geocoder.GetGeometryCoord(geoms[idx], out coords, out coordType);
                    }

                    if (coords != null && m_dbConnProps.DataSource == DbConnectionParams.DataSourceEnum.Database)
                    {
                        txt += string.Format("Pos:\t{0:g8} {1:g9}\n", coords[0].x, coords[0].y);
                    }
                }

                txt += "==============================================================================\n";

            }

            richTextBox1.Text = txt;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (m_dbConnProps.DataSource == DbConnectionParams.DataSourceEnum.Database)
            {
                m_dbConn = DNMcOsmConnection.Create(
                    m_dbConnProps.HostName,
                    m_dbConnProps.Port,
                    m_dbConnProps.Database,
                    m_dbConnProps.User,
                    m_dbConnProps.Password);
            }
            else
            {
                m_dbConn = DNMcOsmConnection.Create(m_dbConnProps.ConnectionString);
            }


            if (m_dbConn.IsDatabaseConnected())
            {
                m_dbConnections.Push(m_dbConn);
            }
            else
            {
                MessageBox.Show("Error connecting the data-source", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                m_dbConn.Dispose();
                if (m_dbConnections.Count > 0)
                {
                    m_dbConn = m_dbConnections.Peek();
                }
                else
                {
                    m_dbConn = null;
                }
            }

            m_dbRoutingConn = DNMcOsmConnection.Create(
                m_dbConnProps.HostName,
                m_dbConnProps.Port,
                m_dbConnProps.RoutingDatabase,
                m_dbConnProps.User,
                m_dbConnProps.Password);
            if (m_dbRoutingConn.IsDatabaseConnected())
            {
                m_dbRoutingConnections.Push(m_dbRoutingConn);
            }
            else
            { 
                MessageBox.Show("Error connecting routing database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                m_dbRoutingConn.Dispose();
                if (m_dbRoutingConnections.Count > 0)
                {
                    m_dbRoutingConn = m_dbRoutingConnections.Peek();
                }
                else
                {
                    m_dbRoutingConn = null;
                }
            }

            OnConnectionChanged();

        }

        private void rbNeutral_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNeutral.Checked)
            {
                m_currLanguage = "";
                LoadCity();
            }
        }

        private void rbHebrew_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHebrew.Checked)
            {
                m_currLanguage = "he";
                LoadCity();
            }
        }

        private void rbEnglish_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEnglish.Checked)
            {
                m_currLanguage = "en";
                LoadCity();
            }
        }
        private void cmbCity_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbCity.Text != string.Empty && m_dbConn.IsDatabaseConnected())
            {
                cmbStreet.Items.Clear();
                cmbStreet.Text = "";
                IDNMcOsmGeocoder geocoder = DNMcOsmGeocoder.Create(m_dbConn);
                DNSMcOsmStreetInfo[] streetInfo = null;
                if (cmbState.Items.Count > 0)
                {
                    streetInfo = geocoder.EnumStreets(cmbCity.Text, cmbState.Text, m_currLanguage);
                }
                else
                {
                    streetInfo = geocoder.EnumStreets(cmbCity.Text, null, m_currLanguage);
                }

                if (streetInfo != null)
                {
                    foreach (DNSMcOsmStreetInfo info in streetInfo)
                    {
                        cmbStreet.Items.Add(info.strStreetName);
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cmbCity_TextChanged(object sender, EventArgs e)
        {
            if (cmbCity.Text != string.Empty)
            {
                btnQuery.Enabled = true;
            }
            else
            {
                if (cmbStreet.Text != string.Empty)
                {
                    btnQuery.Enabled = true;
                }
                else
                {
                    btnQuery.Enabled = false;
                }
            }
        }

        private void cmbStreet_TextChanged(object sender, EventArgs e)
        {
            cmbCity_TextChanged(this, e);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (m_dbConn.IsDatabaseConnected())
            {
                richTextBox1.Text = "";

                IDNMcOsmGeocoder geocoder = DNMcOsmGeocoder.Create(m_dbConn);

                if (rbByAddress.Checked)
                {
                    QueryByFields(geocoder);
                }
                else
                {
                    SearchName(geocoder);
                }
            }
            else
            {
                MessageBox.Show("You are not connected into the database, check network", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                OnConnectionChanged();
            }
        }

        private void listTypes_MouseClick(object sender, MouseEventArgs e)
        {
            if (cmbCity.Text == string.Empty)
            {
                MessageBox.Show("Must provide a city", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (e.Button == System.Windows.Forms.MouseButtons.Right && m_dbConn.IsDatabaseConnected())
            {
                if (MessageBox.Show("Perform query", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    IDNMcOsmGeocoder geocoder = DNMcOsmGeocoder.Create(m_dbConn);

                    string[] layerTypes = new string[listTypes.SelectedItems.Count];
                    for (int i = 0; i < listTypes.SelectedItems.Count; i++)
                    {
                        layerTypes[i] = listTypes.SelectedItems[i].Text;
                    }

                    DNSMcOsmPlaceInfo[] places;
                    if (cmbState.Text == "")
                    {
                        places = geocoder.QueryForPlace(cmbCity.Text, layerTypes, null, m_currLanguage);
                    }
                    else
                    {
                        places = geocoder.QueryForPlace(cmbCity.Text, layerTypes, cmbState.Text, m_currLanguage);
                    }

                    long[] inOsmIds = new long[places.Length];
                    for (int i = 0; i < inOsmIds.Length; i++)
                    {
                        inOsmIds[i] = places[i].nOsmId;
                    }

                    string[] geoms = null;
                    long[] osmIds = null;

                    if (places != null)
                    {

                        if (m_dbConnProps.DataSource == DbConnectionParams.DataSourceEnum.Database)
                        {
                            geocoder.GetGeometries(inOsmIds, out geoms, out osmIds);
                        }

                        ShowResults(geocoder, places, geoms, osmIds);
                    }
                }
            }
        }

        private void grpRvrsGeocoding_EnabledChanged(object sender, EventArgs e)
        {
            if (grpRvrsGeocoding.Enabled)
            {
                IDNMcOsmRvrsGeocoder rvrsGeocoder = DNMcOsmRvrsGeocoder.Create(m_dbConn);
                int nStreetTolerance, nPlaceTolerance;
                rvrsGeocoder.GetTolerances(out nPlaceTolerance, out nStreetTolerance);
                if (nPlaceTolerance > 0)
                {
                    dPlaceTolerance.Value = (decimal)nPlaceTolerance;
                    dPlaceTolerance.Enabled = true;
                }
                else
                {
                    dPlaceTolerance.Value = 0;
                    dPlaceTolerance.Enabled = false;
                }

                if (nStreetTolerance > 0)
                {
                    dStreetTolerance.Value = (decimal)nStreetTolerance;
                    dStreetTolerance.Enabled = true;
                }
                else
                {
                    dStreetTolerance.Value = 0;
                    dStreetTolerance.Enabled = false;
                }
            }
            else
            {
                dStreetTolerance.Value = 0;
                dPlaceTolerance.Value = 0;
            }
        }

        private void btnGetTolerance_Click(object sender, EventArgs e)
        {
            grpRvrsGeocoding_EnabledChanged(this, new EventArgs());
        }

        private void coord_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            double result = 0;
            if (txt.Text != string.Empty)
            {
                if (!double.TryParse(txt.Text, out result))
                {
                    errorProvider1.SetError(txt, "Must provide a valid coordinate!");
                    e.Cancel = true;
                    return;
                }

            }
            errorProvider1.Clear();
        }

        private void coord_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;

            errorProvider1.Clear();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            if (txtX.Text != string.Empty &&
                txtY.Text != string.Empty)
            {
                DNSMcVector3D vect = new DNSMcVector3D(double.Parse(txtX.Text),
                                                        double.Parse(txtY.Text),
                                                        0.0);
                double[] distances;
                DNSMcOsmPlaceInfo[] places;
                IDNMcOsmRvrsGeocoder rvrsGeocoder = DNMcOsmRvrsGeocoder.Create(m_dbConn);
                try
                {
                    if (destList.Items.Count == 0)
                    {
                        rvrsGeocoder.ScanPoint(vect, out places, out distances, m_currLanguage);
                    }
                    else
                    {
                        string[] classification = new string[destList.Items.Count];
                        for (int i = 0; i < destList.Items.Count; i++)
                        {
                            classification[i] = destList.Items[i].ToString();
                        }
                        rvrsGeocoder.ScanPoint(vect, out places, out distances, m_currLanguage, classification);
                    }
                }
                catch (MapCoreException mce)
                {
                    string caption = "Mapcore Error " + mce.ErrorCode.ToString();
                    MessageBox.Show(mce.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }


                if (places == null || places.Length == 0)
                {
                    MessageBox.Show("No matching place was found", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    IDNMcOsmGeocoder geocoder = DNMcOsmGeocoder.Create(m_dbConn);

                    string[] geoms = null;
                    long[] nRetOsmIds = null;
                    if (m_dbConnProps.DataSource == DbConnectionParams.DataSourceEnum.Database)
                    {
                        long[] nOsmIds = new long[places.Length];
                        for (int i = 0; i < nOsmIds.Length; i++)
                        {
                            nOsmIds[i] = places[i].nOsmId;
                        }

                        geocoder.GetGeometries(nOsmIds, out geoms, out nRetOsmIds);
                    }

                    for (int i = 0; i < places.Length; i++)
                    {
                        string geomType = "";
                        DNSMcVector3D[] coords = null;

                        if (m_dbConnProps.DataSource == DbConnectionParams.DataSourceEnum.Database)
                        {
                            string geom = "";
                            for (int j = 0; j < nRetOsmIds.Length; j++)
                            {
                                if ((places[i].nOsmId) == (nRetOsmIds[j]))
                                {
                                    geom = geoms[j];
                                    break;
                                }
                            }

                            geocoder.GetGeometryCoord(geom, out coords, out geomType);
                        }
                        else
                        {
                            geocoder.GetGeometryCoord(places[i].strGeometry, out coords, out geomType);
                        }

                        ListViewItem lvi = listView1.Items.Add(places[i].strName);
                        lvi.SubItems.Add(places[i].strType);
                        if (geomType == "LINESTRING" ||
                            geomType == "POLYGON")
                        {
                            lvi.SubItems.Add("Multiple");
                            lvi.SubItems.Add("Multiple");
                        }
                        else
                        {
                            lvi.SubItems.Add(string.Format("{0}", coords[0].x));
                            lvi.SubItems.Add(string.Format("{0}", coords[0].y));
                        }
                        lvi.SubItems.Add(places[i].strStreetName);
                        lvi.SubItems.Add(places[i].strHouseNum);

                        if (m_dbConnProps.DataSource == DbConnectionParams.DataSourceEnum.Database)
                        {
                            lvi.SubItems.Add(distances[i].ToString());
                        }
                        else
                        {
                            lvi.SubItems.Add("N/A");
                        }

                        lvi.SubItems.Add(places[i].strCity.ToString());
                        lvi.SubItems.Add(places[i].strState.ToString());
                    }
                }
            }
        }

        private void btnSetTolerance_Click(object sender, EventArgs e)
        {
            IDNMcOsmRvrsGeocoder rvrsGeocoder = DNMcOsmRvrsGeocoder.Create(m_dbConn);
            rvrsGeocoder.SetTolerances((int)dPlaceTolerance.Value, (int)dStreetTolerance.Value);
        }

        private void btnPrioritize_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Add(tabPage3);
            tabControl1.SelectTab("tabPage3");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < destList.Items.Count; i++)
            {
                sourceList.Items.Add(destList.Items[i],false);
            }
            destList.Items.Clear();
            tabControl1.SelectTab("tabPage2");
            tabControl1.TabPages.Remove(tabPage3);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("tabPage2");
            tabControl1.TabPages.Remove(tabPage3);
        }

        private void sourceList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                if (!btnAdd.Enabled)
                {
                    btnAdd.Enabled = true;
                }
            }
            else
            {
                if (btnAdd.Enabled && sourceList.CheckedItems.Count <= 1)
                {
                    btnAdd.Enabled = false;
                }
            }
        }

        private void destList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                if (!btnRemove.Enabled)
                {
                    btnRemove.Enabled = true;
                }
            }
            else
            {
                if (btnRemove.Enabled && destList.CheckedItems.Count <= 1)
                {
                    btnRemove.Enabled = false;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            object[] checkedListBoxes = new object[sourceList.CheckedItems.Count];
            int i=0;
            foreach (object item in sourceList.CheckedItems)
            {
                checkedListBoxes[i] = item;
                i++;
            }
            destList.Items.AddRange(checkedListBoxes);

            for (int j = 0; j < checkedListBoxes.Length; j++)
            {
                sourceList.Items.Remove(checkedListBoxes[j]);
            }

            btnAdd.Enabled = false;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            object[] checkedListBoxes = new object[sourceList.CheckedItems.Count];
            int i = 0;
            foreach (object item in destList.CheckedItems)
            {
                checkedListBoxes[i] = item;
                i++;
            }
            sourceList.Items.AddRange(checkedListBoxes);

            for (int j = 0; j < checkedListBoxes.Length; j++)
            {
                destList.Items.Remove(checkedListBoxes[j]);
            }

            btnRemove.Enabled = false;

        }

        private void destList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (destList.SelectedItems.Count == 1)
            {
                if (destList.SelectedIndex > 0)
                {
                    btnUp.Enabled = true;
                }
                else
                {
                    btnUp.Enabled = false;
                }

                if (destList.SelectedIndex < destList.Items.Count - 1)
                {
                    btnDown.Enabled = true;
                }
                else
                {
                    btnDown.Enabled = false;
                }
            }
            else
            {
                btnDown.Enabled = false;
                btnUp.Enabled = false;
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            int i = destList.SelectedIndex;
            object item = destList.Items[i];
            i--;
            destList.Items.Remove(item);
            destList.Items.Insert(i, item);
            destList.SelectedIndex = i;
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            int i = destList.SelectedIndex;
            object item = destList.Items[i];
            i++;
            destList.Items.Remove(item);
            destList.Items.Insert(i, item);
            destList.SelectedIndex = i;
        }

        private void btnRoute_Click(object sender, EventArgs e)
        {
            listView2.Items.Clear();
            IDNMcOsmRouter router = DNMcOsmRouter.Create(m_dbRoutingConn);
            IDNMcOsmGeocoder geocoder = DNMcOsmGeocoder.Create(m_dbConn); // Used for geometric conversions

            DNSMcVector3D pointA = new DNSMcVector3D(double.Parse(tbX1.Text), double.Parse(tbY1.Text), 0.0);
            DNSMcVector3D pointB = new DNSMcVector3D(double.Parse(tbX2.Text), double.Parse(tbY2.Text), 0.0);

            DNSMcOsmRoutingDirection[] directions = router.RouteFromAtoB(pointA, pointB, cbOneWay.Checked);
            if (directions.Length > 0)
            {
                foreach (DNSMcOsmRoutingDirection direction in directions)
                {
                    string geomType;
                    DNSMcVector3D[] coords;

                    ListViewItem item = listView2.Items.Add(direction.nSequenceNo.ToString());
                    item.SubItems.Add(direction.nNodeId.ToString());
                    item.SubItems.Add(direction.strName);
                    item.SubItems.Add(direction.nHeading.ToString("000.#####"));
                    item.SubItems.Add(direction.nCost.ToString("####.##"));
                    geocoder.GetGeometryCoord(direction.strGeom,out coords, out geomType);
                    item.SubItems.Add("X: " + coords[0].x.ToString("########.0") + " , Y: " + coords[0].y.ToString("#######.0"));
                    if (geomType != "POINT")
                    {
                        int nLast = coords.Length - 1;
                        item.SubItems.Add("X: " + coords[nLast].x.ToString("########.0") + " , Y: " + coords[nLast].y.ToString("#######.0"));
                    }
                }
            }
            else
            {
                MessageBox.Show("No routing data was available", "Route failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void rbByAddress_CheckedChanged(object sender, EventArgs e)
        {
            lblState.Visible    = rbByAddress.Checked;
            lblCity.Visible     = rbByAddress.Checked;
            lblStreet.Visible   = rbByAddress.Checked;
            lblTypes.Visible    = rbByAddress.Checked;
            cmbCity.Visible     = rbByAddress.Checked;
            cmbStreet.Visible   = rbByAddress.Checked;
            listTypes.Visible   = rbByAddress.Checked;

            if (rbByAddress.Checked)
            {
                btnQuery.Enabled = btnQueryEnabled;
            }
        }

        private void rbFreeText_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFreeText.Checked)
            {
                btnQueryEnabled = btnQuery.Enabled;
                btnQuery.Enabled = true;
            }
            lblAddress.Visible = rbFreeText.Checked;
            tbAddress.Visible = rbFreeText.Checked;
        }

        private void cmbState_SelectedValueChanged(object sender, EventArgs e)
        {
            string selectedValue = null;
            if (cmbState.SelectedIndex >= 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                selectedValue = cmbState.Items[cmbState.SelectedIndex].ToString();
                bDoNotLoadState = true;
                LoadCity();
                LoadTypes();
                Cursor.Current = Cursors.Default;
            }
        }

        private void DisconnectFromDb(IDNMcOsmConnection conn, Stack<IDNMcOsmConnection> stack)
        {
            if (stack.Count > 0)
            {
                conn = stack.Pop();
                if (m_dbConn != null)
                {
                    conn.Dispose();
                }

                if (stack.Count > 0)
                {
                    conn = stack.Peek();
                }
                else
                {
                    conn = null;
                }
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            DisconnectFromDb(m_dbConn, m_dbConnections);
            DisconnectFromDb(m_dbRoutingConn, m_dbRoutingConnections);
            OnConnectionChanged();
        }

        private void searchResultMenu_Opening(object sender, CancelEventArgs e)
        {
            long itemId;
            getGeometryToolStripMenuItem.Enabled = false;
            copyToolStripMenuItem.Enabled = false;

            if (Clipboard.ContainsText() &&
                long.TryParse(Clipboard.GetText(), out itemId))
            {
                getGeometryToolStripMenuItem.Enabled = true;
            }

            if (richTextBox1.SelectedText != "")
            {
                if (long.TryParse(richTextBox1.SelectedText, out itemId))
                {
                    copyToolStripMenuItem.Enabled = true;
                }
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.SelectedText);
        }

        private void getGeometryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            long[] placeIds = new long[1];
            long[] retPlaceIds;
            string[] retGeoms;

            placeIds[0] = long.Parse(Clipboard.GetText());
            IDNMcOsmGeocoder geoCoder = DNMcOsmGeocoder.Create(m_dbConn);
            geoCoder.GetGeometries(placeIds, out retGeoms, out retPlaceIds);

            if (retGeoms.Length > 0)
            {
                DNSMcVector3D[] retCoords;
                string coordType;
                geoCoder.GetGeometryCoord(retGeoms[0], out retCoords, out coordType);

                ShowCoords showGeomDlg = new ShowCoords(coordType, retCoords);
                showGeomDlg.ShowDialog();
            }
            else
            {
                MessageBox.Show("Couldn't get geometry", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
