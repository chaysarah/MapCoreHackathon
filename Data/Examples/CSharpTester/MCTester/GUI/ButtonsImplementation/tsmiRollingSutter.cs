using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using System.IO;
using System.Xml.Serialization;


namespace MCTester.ButtonsImplementation
{
    public partial class tsmiRollingSutterForm : Form
    {
        public tsmiRollingSutterForm()
        {
            try
            {
                string currentPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
                currentPath = System.IO.Path.GetDirectoryName(currentPath);
                string XML_File_Path = currentPath + @"\RollingShutterTest.xml";
                string Object_File_Path = currentPath + @"\RollingShutterTestObjects.m";

                if (System.IO.File.Exists(XML_File_Path))
                {
                    if (System.IO.File.Exists(Object_File_Path))
                    {
                        if (MCTester.Managers.MapWorld.MCTMapFormManager.MapForm != null && MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.Viewport != null)
                        {
                            IDNMcMapViewport currViewport = MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.Viewport;
                            uint width, height;
                            currViewport.GetViewportSize(out width, out height);


                            if (currViewport != null)
                            {
                                try
                                {
                                    UserDataFactory UDF = new UserDataFactory();

                                    MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay.LoadObjects(Object_File_Path, UDF);
                                }
                                catch (MapCoreException McEx)
                                {
                                    MapCore.Common.Utilities.ShowErrorMessage("LoadObjects", McEx);
                                    return;
                                }

                                // deserialize the rolling shutter XML
                                StreamReader SR = new StreamReader(XML_File_Path);
                                XmlSerializer Xser = new XmlSerializer(typeof(cRollingShutterData));

                                cRollingShutterData rollingShutterData = (cRollingShutterData)Xser.Deserialize(SR);

                                DNSRollingShutterData rollingShutter = new DNSRollingShutterData();
                                rollingShutter.aRollingShutterLocations = rollingShutterData.RollingShutterLocations.ToArray();

                                uint viewportHeight = height;
                                if (rollingShutterData.SurfaceHeight > 0)
                                {
                                    viewportHeight = rollingShutterData.SurfaceHeight;
                                }
                                // normalize the Row param
                                for (int i = 0; i < rollingShutter.aRollingShutterLocations.Length; i++)
                                {
                                    if (rollingShutter.aRollingShutterLocations[i].uRow > 0)
                                    {
                                        if (rollingShutter.aRollingShutterLocations[i].uRow <= 100)
                                        {
                                            rollingShutter.aRollingShutterLocations[i].uRow = (rollingShutter.aRollingShutterLocations[i].uRow * (viewportHeight - 1)) / 100;
                                        }
                                        else
                                            rollingShutter.aRollingShutterLocations[i].uRow = (viewportHeight - 1);
                                    }
                                }

                                rollingShutter.uNumPixelsPerStrip = rollingShutterData.NumPixelsPerStrip;

                                try
                                {
                                    if (width >= 1 && height >= 1)
                                    {
                                        Bitmap bmp;
                                        uint renderWidth = width;
                                        uint renderHeight = height;

                                        if (MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.IsWpfMapWindow == true && rollingShutterData.SurfaceWidth > 0 && rollingShutterData.SurfaceHeight > 0)
                                        {
                                            IntPtr pSurface = IntPtr.Zero;
                                            currViewport.ResizeRenderSurface(rollingShutterData.SurfaceWidth, rollingShutterData.SurfaceHeight, out pSurface);

                                            renderWidth = rollingShutterData.SurfaceWidth;
                                            renderHeight = rollingShutterData.SurfaceHeight;
                                        }


                                        uint stride = renderWidth * 4;
                                        int bufferSize = (int)stride * (int)renderHeight;
                                        IntPtr ptr = System.Runtime.InteropServices.Marshal.AllocHGlobal(bufferSize);

                                        DNSMcRect rect = new DNSMcRect(0, 0, (int)width, (int)height);
                                        currViewport.RenderRollingShutterRectToBuffer(rollingShutter, rect, renderWidth, renderHeight, DNEPixelFormat._EPF_A8R8G8B8, 0, ptr);
                                        bmp = new Bitmap((int)renderWidth, (int)renderHeight, (int)stride, System.Drawing.Imaging.PixelFormat.Format32bppPArgb, ptr);

                                        IDNMcImage mcImage = DNMcImage.Create(ptr, renderWidth, renderHeight, DNEPixelFormat._EPF_A8R8G8B8);

                                        SaveFileDialog SFD = new SaveFileDialog();
                                        SFD.RestoreDirectory = true;
                                        SFD.Filter = "PNG|*.png";

                                        if (SFD.ShowDialog() == DialogResult.OK)
                                        {
                                            mcImage.Save(SFD.FileName);
                                        }

                                        this.Width = (int)width + 20;
                                        this.Height = (int)height + 40;
                                        this.BackgroundImage = (Image)bmp;
                                        this.BackgroundImageLayout = ImageLayout.Zoom;

                                        this.Show();

                                        //Size mainFormSize = MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.Size;

                                        //mainFormSize.Height -= 1;
                                        MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.WindowState = FormWindowState.Normal;

                                        MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.RenderMap();

                                        MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.WindowState = FormWindowState.Maximized;
                                    }
                                }
                                catch (MapCoreException McEx)
                                {
                                    MapCore.Common.Utilities.ShowErrorMessage("RenderRollingShutterRectToBuffer", McEx);
                                }


                            }
                            else
                                MessageBox.Show("You have to load viewport first", "Viewport missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                            MessageBox.Show("Missing viewport", "RenderRollingShutterRectToBuffer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                        MessageBox.Show(Object_File_Path + " was not found", "Objects test file not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                    MessageBox.Show(XML_File_Path + " was not found", "XML not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("RenderRollingShutterRectToBuffer", McEx);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // tsmiRollingSutterForm
            // 
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(284, 264);
            this.Name = "tsmiRollingSutterForm";
            this.Text = "Rolling Shutter";
            this.ResumeLayout(false);

        }
    }


    [Serializable]
    public class cRollingShutterData
    {
        #region Private Members

        private List<DNSRollingShutterLocation> m_RollingShutterLocations = new List<DNSRollingShutterLocation>();
        private DNSRollingShutterLocation m_RollingShutterLocation;
        private uint m_NumPixelsPerStrip;
        private uint m_SurfaceWidth;
        private uint m_SurfaceHeight;

        #endregion

        public cRollingShutterData()
        {
        }

        #region Public Properties

        public List<DNSRollingShutterLocation> RollingShutterLocations
        {
            get { return m_RollingShutterLocations; }
            set { m_RollingShutterLocations = value; }
        }

        private DNSRollingShutterLocation RollingShutterLocation
        {
            get { return m_RollingShutterLocation; }
            set { m_RollingShutterLocation = value; }
        }

        private double PositionX
        {
            get { return m_RollingShutterLocation.Position.x; }
            set { m_RollingShutterLocation.Position.x = value; }
        }

        private double PositionY
        {
            get { return m_RollingShutterLocation.Position.y; }
            set { m_RollingShutterLocation.Position.y = value; }
        }

        private double PositionZ
        {
            get { return m_RollingShutterLocation.Position.z; }
            set { m_RollingShutterLocation.Position.z = value; }
        }

        private float Yaw
        {
            get { return m_RollingShutterLocation.fYaw; }
            set { m_RollingShutterLocation.fYaw = value; }
        }

        private float Pitch
        {
            get { return m_RollingShutterLocation.fPitch; }
            set { m_RollingShutterLocation.fPitch = value; }
        }

        private float Roll
        {
            get { return m_RollingShutterLocation.fRoll; }
            set { m_RollingShutterLocation.fRoll = value; }
        }

        private uint Row
        {
            get { return m_RollingShutterLocation.uRow; }
            set { m_RollingShutterLocation.uRow = value; }
        }

        public uint NumPixelsPerStrip
        {
            get { return m_NumPixelsPerStrip; }
            set { m_NumPixelsPerStrip = value; }
        }

        public uint SurfaceWidth
        {
            get { return m_SurfaceWidth; }
            set { m_SurfaceWidth = value; }
        }

        public uint SurfaceHeight
        {
            get { return m_SurfaceHeight; }
            set { m_SurfaceHeight = value; }
        }

        #endregion
    }




}
