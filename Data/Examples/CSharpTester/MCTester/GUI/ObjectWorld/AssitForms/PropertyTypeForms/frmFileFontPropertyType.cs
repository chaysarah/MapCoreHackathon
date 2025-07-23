using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.ObjectWorld.ObjectsUserControls.PropertyTypeForms
{
    public partial class frmFileFontPropertyType : frmBasePropertyType
    {
        private IDNMcFont m_McFont;
        
        public frmFileFontPropertyType()
            : base()
        {
            InitializeComponent();
        }

        public frmFileFontPropertyType(uint id):base (id)
        {
            base.ID = id;
            InitializeComponent();
        }
        public frmFileFontPropertyType(uint id, IDNMcFont value)
            : this(id)
        {
            McFont = value;
        }

        public IDNMcFont McFont
        {
            get 
            {
                string fileName = ctrlBrowseFileFont.FileName;
                if (fileName != "")
                {
                    DNSMcFileSource mfileSource = new DNSMcFileSource();
                    mfileSource.strFileName = fileName;
                    try
                    {
                        if (cbIsMemoryBuffer.Checked)
                        {
                            mfileSource.strFileName = "";
                            byte[] fileByte = File.ReadAllBytes(fileName);
                            mfileSource.aFileMemoryBuffer = fileByte;
                            mfileSource.bIsMemoryBuffer = true;
                        }

                        IDNMcFileFont varFileFont = DNMcFileFont.Create(mfileSource, ntbFontHeight.GetInt32());
                        
                        m_McFont = varFileFont;
                        return m_McFont;
                    }
                    catch (IOException )
                    {
                        MessageBox.Show("File Name is Wrong", "No File Choose", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("File Name is Missing", "No File Choose", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return null;
                
            }
            set {
                m_McFont = value;
                IDNMcFileFont mcFileFont = (IDNMcFileFont)m_McFont;
                DNSMcFileSource mcFileSource = new DNSMcFileSource();
                int height = 0;
                mcFileFont.GetFontFileAndHeight(out mcFileSource, out height);
                ctrlBrowseFileFont.FileName = mcFileSource.strFileName;
                cbIsMemoryBuffer.Checked = mcFileSource.bIsMemoryBuffer;
                ntbFontHeight.SetInt(height);
            }
        }
    }
}