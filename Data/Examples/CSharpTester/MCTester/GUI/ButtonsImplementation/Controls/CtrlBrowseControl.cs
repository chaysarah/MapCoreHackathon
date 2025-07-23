using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MCTester;
using UnmanagedWrapper;

namespace MCTester.Controls
{
    public partial class CtrlBrowseControl : UserControl
    {
        OpenFileDialog OFD;
        SaveFileDialog SFD;
       
        FolderSelectDialog FSD;
        DialogResult DiagRes;
        private TextBox txtFileName;
        private Label label1;
        private Button btnBrowse;
        ListBox lstBox;
        private bool m_IsFullPath;
        private string m_prefix;
        /// <summary>
        /// Handle file name prefixes
        /// </summary>
        Dictionary<string, string> m_prefixByControl;
        private string m_lastFileName = "";

        public event EventHandler FileNameChanged;
        public event EventHandler FileNameSelected;
        private bool mSaveFile = false;
        
        public CtrlBrowseControl()
        {
            m_prefix = string.Empty;
            InitializeComponent();
            OFD = new OpenFileDialog();
            SFD = new SaveFileDialog();
            FSD = new FolderSelectDialog();
            FSD.Title = "Folder to select";
            FSD.InitialDirectory = @"c:\";

            DiagRes = new DialogResult();
            mFolderDialog = false;
            m_IsFullPath = true;
            m_prefixByControl = new Dictionary<string, string>();
            btnBrowse.Height = 22;
        }

        private bool mFolderDialog;
        public bool IsFolderDialog
        {
            get { return mFolderDialog; }
            set { mFolderDialog = value; }
        }

        
        public bool IsSaveFile
        {
            get { return mSaveFile; }
            set { mSaveFile = value; }
        }

        public string Filter
        {
            set { OFD.Filter = value; SFD.Filter = value; }
            get 
            { 
                if (mSaveFile)
                    return SFD.Filter;
                else
                    return OFD.Filter; 
            }
        }

        public ListBox ListBox
        {
            set 
            { 
                this.lstBox = value;

                if (value!=null)
                {
                    OFD.Multiselect = true;
                }
            }
        }

        public string Prefix
        {
            get { return m_prefix; }
            set { m_prefix = value; }
        }

        public bool MultiFilesSelect
        {
            get
            {
                return OFD.Multiselect;
            }
            set
            {
                OFD.Multiselect = value;
                txtFileName.Multiline = value;

                if (value == true)
                    txtFileName.ScrollBars = ScrollBars.Vertical;
                
            }
        }

        public string FileName
        {
            get 
            {
                return txtFileName.Text;
            }
            set 
            {
                if (value != null && value != string.Empty && m_prefix != string.Empty && value.StartsWith(m_prefix) == false)
                    value = string.Concat(m_prefix, value);
                txtFileName.Text = value; 
            }
        }

        public string[] FileNames
        {
            get
            {
                if (DiagRes == DialogResult.OK)
                {
                    return OFD.FileNames;
                }
                else
                    return null;
            }
        }

        public string LabelCaption
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        public Button BtnBrowse
        {
            get { return btnBrowse; }
        }

        public bool IsFullPath
        {
            get { return m_IsFullPath; }
            set { m_IsFullPath = value; }
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnBrowse
            // 
            this.btnBrowse.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnBrowse.Location = new System.Drawing.Point(232, 0);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(3, 0, 3, 8);
            this.btnBrowse.MaximumSize = new System.Drawing.Size(150, 22);
            this.btnBrowse.MinimumSize = new System.Drawing.Size(22, 20);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(25, 22);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click_1);
            // 
            // txtFileName
            // 
            this.txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileName.Location = new System.Drawing.Point(4, 1);
            this.txtFileName.Margin = new System.Windows.Forms.Padding(0);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(228, 20);
            this.txtFileName.TabIndex = 2;
            this.txtFileName.TextChanged += new System.EventHandler(this.txtFileName_TextChanged);
            // 
            // CtrlBrowseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.btnBrowse);
            this.MinimumSize = new System.Drawing.Size(120, 24);
            this.Name = "CtrlBrowseControl";
            this.Size = new System.Drawing.Size(257, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void SetReadOnly()
        {
            txtFileName.ReadOnly = true;
            btnBrowse.Visible = false;
        }

        public void btnBrowse_Click_1(object sender, EventArgs e)
        {
            if (IsFolderDialog)
            {
                if (FileName != String.Empty)
                    FSD.InitialDirectory = FileName;
                if (FSD.ShowDialog(IntPtr.Zero))
                {
                    m_lastFileName = FileName;

                    FileName = FSD.FileName;
                    if (this.lstBox != null)
                    {
                        lstBox.Items.Add(FileName);
                    }
                    if (FileNameChanged != null && m_lastFileName == FileName)
                    {
                        FileNameChanged(this, new EventArgs());
                    }
                }

                if (FileNameSelected != null)
                {
                    FileNameSelected(this, new EventArgs());
                }

            }
            else
            {
                if (!IsSaveFile)
                {
                    OFD.RestoreDirectory = true;
                    DiagRes = OFD.ShowDialog();
                }
                else
                {
                    SFD.RestoreDirectory = true;
                    DiagRes = SFD.ShowDialog();
                }

                if (DiagRes == DialogResult.OK)
                {
                    m_lastFileName = FileName;

                    if (!IsSaveFile)
                    {

                        FileName = OFD.FileName;

                        if (m_IsFullPath == false)
                        {
                            char[] delimeters = new char[1];
                            delimeters[0] = '\\';
                            string[] splitedString = FileName.Split(delimeters);
                            FileName = splitedString[splitedString.Length - 1];
                        }
                    }
                    else
                        FileName = SFD.FileName;

                    if (this.lstBox != null)
                    {
                        lstBox.Items.AddRange(FileNames);
                    }
                    if (FileNameSelected != null)
                    {
                        FileNameSelected(this, new EventArgs());
                    }
                    if (FileNameChanged != null && m_lastFileName == FileName)
                    {
                        FileNameChanged(this, new EventArgs());
                    }
                }
            }            
        }

        void txtFileName_TextChanged(object sender, EventArgs e)
        {
            // if want the prefix be in the text box all the time.
            //if (m_prefix != string.Empty && txtFileName.Text == string.Empty)
            //    txtFileName.Text = m_prefix;
            if (FileNameChanged != null)
            {
                FileNameChanged(this, new EventArgs());
            }
        }       

        public void SetPrefixFileName(string prefix)
        {
            m_prefix = prefix;
        }

        private string CheckPrefix(string currValue)
        {
            string returnValue = currValue;
            return currValue;
        }
    }
}
