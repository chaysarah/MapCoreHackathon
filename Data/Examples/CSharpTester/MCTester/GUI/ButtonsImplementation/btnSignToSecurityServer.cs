using MapCore;
using MapCore.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using UnmanagedWrapper;

namespace MCTester.ButtonsImplementation
{
    public partial class btnSignToSecurityServer : Form
    {
        static string UserName = "";
        static string ServerPath = "http://localhost:6868";
        private Timer m_UserTimer;
        private MCTPackages.MCTUsersData m_users;
        private DateTime m_ExpiredDateTime;

        public btnSignToSecurityServer()
        {
            InitializeComponent();
            txtServerPath.Text = ServerPath;
        }

        private void btnSignToSecurityServer_Load(object sender, EventArgs e)
        {
            GetSecurityServerUsers();
            lblCurrentUser.Text = UserName;

            try
            {
                string pstrToken = "";
                string pstrSessionID = "";
                DNMcMapLayer.GetNativeServerCredentials(ref pstrToken, ref pstrSessionID);
                txtCurrentToken.Text = pstrToken;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcMapLayer.GetNativeServerCredentials", McEx);
            }
        }

        private void GetSecurityServerUsers()
        {
            try
            {
                m_users = new MCTPackages.MCTUsersData();
                string sURL = ServerPath + "/users.json";

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(sURL);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                MCTPackages.MCTSignInResponse signInResponse = new MCTPackages.MCTSignInResponse();
                if (m_users.Load(httpResponse.GetResponseStream()))
                {
                    cbUsers.Items.Clear();
                    cbUsers.Items.AddRange(m_users.UsersData.Select(x => x.UserName).ToArray());
                    btnSignIn.Enabled = false;
                    cbUsers.SelectedText = "";
                }
            }
            catch (WebException ex)
            {
                ShowGetUsersMsg("Get users from security server failed, message: " + ex.Message);
            }
            catch (IOException ex)
            {
                ShowGetUsersMsg("Get users from security server failed, message: " + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                ShowGetUsersMsg("Get users from security server failed, message: " + ex.Message);
            }
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            if (cbUsers.SelectedItem != null)
            {
                txtCurrentToken.Text = "";
                lblCurrentUser.Text = "";
                ServerPath = txtServerPath.Text;
                try
                {
                    string sURL = ServerPath + "/signin";

                    var httpWebRequestSignIn = (HttpWebRequest)WebRequest.Create(sURL);
                    httpWebRequestSignIn.ContentType = "application/json";
                    httpWebRequestSignIn.Method = "POST";
                    Guid sessionId = Guid.NewGuid();
                    using (var streamWriter = new StreamWriter(httpWebRequestSignIn.GetRequestStream()))
                    {
                        string json = "{\"initSession\":true," +
                                      "\"userId\":\"" + m_users.UsersData.First(x => x.UserName == cbUsers.SelectedItem.ToString()).UserId + "\"," +
                                      "\"sessionId\":\"" + sessionId.ToString() + "\"}";
                        streamWriter.Write(json);
                    }

                    var httpResponseSignIn = (HttpWebResponse)httpWebRequestSignIn.GetResponse();

                    MCTPackages.MCTSignInResponse signInResponse = new MCTPackages.MCTSignInResponse();
                    if (signInResponse.Load(httpResponseSignIn.GetResponseStream()))
                    {
                        if (signInResponse.isAuthenticated.ToString().ToLower() == "true")
                        {
                            UserName = cbUsers.SelectedItem.ToString();

                            txtCurrentToken.Text = signInResponse.payload;
                            lblCurrentUser.Text = UserName;
                            btnSignIn.Enabled = false;
                            try
                            {
                                DNMcMapLayer.SetNativeServerCredentials(signInResponse.payload, sessionId.ToString());
                            }
                            catch (MapCoreException McEx)
                            {
                                Utilities.ShowErrorMessage("DNMcMapLayer.SetNativeServerCredentials", McEx);
                            }
                            ShowSignInMsg("Sign in succeeds");

                            Authenticate(signInResponse.payload, sessionId.ToString());

                        }
                        else
                        {
                            if (signInResponse.message.ToLower().Contains("expired token"))
                            {
                                ValidateToken();
                            }
                            else
                                ShowSignInMsg("Sign in fails, message: " + signInResponse.message);

                        }
                    }
                }
                catch (WebException ex)
                {
                    ShowSignInMsg("Sign in fails, message: " + ex.Message);
                }
                catch (IOException ex)
                {
                    ShowSignInMsg("Sign in fails, message: " + ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    ShowSignInMsg("Sign in fails, message: " + ex.Message);
                }
            }
        }

        private void MUserTimer_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.ToUniversalTime() > m_ExpiredDateTime)
            {
                ValidateToken();
            }
        }

        private void Authenticate(string token, string sessionId)
        {
            string sURL = ServerPath + "/authenticate";

            var httpWebRequestAuthenticate = (HttpWebRequest)WebRequest.Create(sURL);
            httpWebRequestAuthenticate.ContentType = "application/json";
            httpWebRequestAuthenticate.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequestAuthenticate.GetRequestStream()))
            {
                string json = "{\"token\":\"" + token + "\"," +
                              "\"sessionId\":\"" + sessionId + "\"}";
                streamWriter.Write(json);
            }

            var httpResponseAuthenticate = (HttpWebResponse)httpWebRequestAuthenticate.GetResponse();
            MCTPackages.MCTAuthenticateResponse authenticateResponse = new MCTPackages.MCTAuthenticateResponse();
            if (authenticateResponse.Load(httpResponseAuthenticate.GetResponseStream()))
            {
                if (authenticateResponse.isAuthenticated.ToLower() == "true")
                {
                    string strExpiredDateTime = authenticateResponse.payload.exp;
                    m_ExpiredDateTime = new DateTime();
                    if (GetServerTime(strExpiredDateTime, out m_ExpiredDateTime))
                    {
                        if (m_UserTimer != null)
                            m_UserTimer.Stop();
                        m_UserTimer = new Timer();
                        m_UserTimer.Tick += MUserTimer_Tick;
                        m_UserTimer.Interval = 1000;
                        m_UserTimer.Start();
                    }
                }
            }
        }

        private void ValidateToken()
        {
            try
            {
                string pstrToken = "";
                string pstrSessionID = "";
                DNMcMapLayer.GetNativeServerCredentials(ref pstrToken, ref pstrSessionID);

                string sURL = ServerPath + "/validateToken";
                WebRequest httpWebRequestValidateToken = WebRequest.Create(sURL);
                httpWebRequestValidateToken.ContentType = "application/json";
                httpWebRequestValidateToken.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequestValidateToken.GetRequestStream()))
                {
                    string json = "{\"token\":\"" + pstrToken + "\"," +
                                  "\"sessionId\":\"" + pstrSessionID + "\"}";
                    streamWriter.Write(json);
                }
                var httpResponseValidateToken = (HttpWebResponse)httpWebRequestValidateToken.GetResponse();

                MCTPackages.MCTSignInResponse validateResponse = new MCTPackages.MCTSignInResponse();
                if (validateResponse.Load(httpResponseValidateToken.GetResponseStream()))
                {
                    if (validateResponse.isAuthenticated.ToString().ToLower() == "true")
                    {
                        txtCurrentToken.Text = validateResponse.payload;
                        lblCurrentUser.Text = UserName;
                        btnSignIn.Enabled = false;
                        try
                        {
                            DNMcMapLayer.SetNativeServerCredentials(validateResponse.payload, pstrSessionID);
                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("DNMcMapLayer.SetNativeServerCredentials", McEx);
                        }
                        Authenticate(validateResponse.payload, pstrSessionID);
                    }
                    else
                        ShowSignInMsg("Sign in fails after validate, message: " + validateResponse.message);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcMapLayer.GetNativeServerCredentials", McEx);
            }
            catch (WebException ex)
            {
                ShowValidateMsg("Validate fails, message: " + ex.Message);
            }
            catch (IOException ex)
            {
                ShowValidateMsg("Validate fails, message: " + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                ShowValidateMsg("Validate fails, message: " + ex.Message);
            }
        }

        public bool GetServerTime(string serverFormatTime, out DateTime dateTime)
        {
            string[] formats = {"yyyyMMdd", "hhmmss tt"};
            string[] dtStrings = serverFormatTime.Split("T".ToCharArray());
            dateTime = new DateTime();

            if (dtStrings.Length != 2)
            {
                return false;
            }

            DateTime date = new DateTime();

            int hour = int.Parse(dtStrings[1].Substring(0, 2));
            int minute = int.Parse(dtStrings[1].Substring(2, 2));
            int second = int.Parse(dtStrings[1].Substring(4, 2));

            if (DateTime.TryParseExact(dtStrings[0], formats[0], null, System.Globalization.DateTimeStyles.AssumeUniversal, out date)) 
            {
                dateTime = new DateTime(date.Year, date.Month, date.Day, hour, minute, second);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ShowSignInMsg(string msg)
        {
            MessageBox.Show(msg, "Sign In Operation", MessageBoxButtons.OK);
        }

        private void ShowGetUsersMsg(string msg)
        {
            MessageBox.Show(msg, "Get Users From Security Server Operation", MessageBoxButtons.OK);
        }

        private void ShowValidateMsg(string msg)
        {
            MessageBox.Show(msg, "Validate Token Operation", MessageBoxButtons.OK);
        }

        private void btnGetUsers_Click(object sender, EventArgs e)
        {
            GetSecurityServerUsers();
        }

        private void cbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSignIn.Enabled = (cbUsers.SelectedItem != null  && cbUsers.SelectedItem.ToString() != UserName);
        }
    }
}
