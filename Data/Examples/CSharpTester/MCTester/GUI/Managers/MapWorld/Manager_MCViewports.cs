using System;
using System.Collections.Generic;
using System.Text;
using UnmanagedWrapper;
using MapCore;
using MCTester.MapWorld;
using System.Drawing.Imaging;
using System.Drawing;

using MapCore.Common;
using System.Windows.Forms;
using MCTester.MapWorld.Assist_Forms;
using System.IO;
using MCTester.Automation;
using MCTester.MapWorld.MapUserControls;

namespace MCTester.Managers.MapWorld
{
    public static class Manager_MCViewports
    {
        static Dictionary<object, uint > dViewports;
        static Dictionary<IDNMcMapViewport, bool> m_dViewportRenderNeededFlag;
        static int indexSchemeAOS = 0;
        static uint counter = 1;

        public static int IndexSchemeAOS
        {
            get { return Manager_MCViewports.indexSchemeAOS; }
            set { Manager_MCViewports.indexSchemeAOS = value; }
        }

        private static uint GetViewportCounter()
        {
            return counter++;
        }

        static Manager_MCViewports()
        {
            dViewports = new Dictionary<object, uint >();
            dViewportRenderNeededFlag = new Dictionary<IDNMcMapViewport, bool>();
        }

        public static string GetFullNameOfViewport(IDNMcMapViewport viewport, bool isAddOMData = true)
        {
            string fullName = "";
            if (viewport != null)
            {
                StringBuilder strType = Manager_MCNames.GetType(viewport);
                string text = Manager_MCNames.GetNameByObject(viewport, strType.ToString());
                if (isAddOMData && viewport.OverlayManager != null)
                {
                    text += ", " + Manager_MCNames.GetNameByObject(viewport.OverlayManager);
                }

                fullName = text;
            }
            return fullName;
        }

        public static void AddViewport(IDNMcMapViewport viewport)
        {
            uint id = GetViewportCounter();
            dViewports.Add(viewport, id);
            if(!dViewportRenderNeededFlag.ContainsKey(viewport))
                dViewportRenderNeededFlag.Add(viewport, false);

            Manager_MCNames.SetName(viewport, id.ToString());

            MainForm.RebuildMapWorldTree();
        }

        public static void TurnOnRelevantViewportsRenderingFlags()
        {
            // turn on all flags
            IDNMcMapViewport[] vpArr = new IDNMcMapViewport[dViewportRenderNeededFlag.Keys.Count];
            dViewportRenderNeededFlag.Keys.CopyTo(vpArr, 0);

            foreach (IDNMcMapViewport vp in vpArr)
                dViewportRenderNeededFlag[vp] = true;
        }

        public static void TurnOnRelevantViewportsRenderingFlags(IDNMcOverlayManager overlayManager)
        {
            if (overlayManager != null)
            {
                // turn on only relevant viewport flag
                uint[] VpIds = overlayManager.GetViewportsIDs();

                foreach (uint ID in VpIds)
                {
                    foreach (IDNMcMapViewport VP in dViewports.Keys)
                    {
                        if (VP.ViewportID == ID)
                        {
                            dViewportRenderNeededFlag[VP] = true;
                        }
                    }
                }
            }
            else
                TurnOnRelevantViewportsRenderingFlags();
        }

        public static void TurnOnRelevantViewportsRenderingFlags(IDNMcMapViewport viewport)
        {
            // turn on only relevant viewport flag
            if (viewport != null)
                dViewportRenderNeededFlag[viewport] = true;
            else
                TurnOnRelevantViewportsRenderingFlags();
        }
    
        public static Dictionary<object, uint > AllParams
        {
            get  
            {
                Dictionary<object, uint > Ret = new Dictionary<object, uint >();

                foreach (object keyViewport in dViewports.Keys)
                {
                    Ret.Add(keyViewport, dViewports[keyViewport]);
                }

                return Ret; 
            }
        }

        public static  Dictionary<object, uint > GetChildren(object Parent)
        {
            Dictionary<object, uint> Ret = new Dictionary<object, uint>();
            try
            {
                IDNMcMapViewport Viewport = (IDNMcMapViewport)Parent;

                if (Viewport == null)
                    return Ret;

                IDNMcMapTerrain[] TerrainsInViewport = Viewport.GetTerrains();

                uint i = 0;
                foreach (IDNMcMapTerrain currTerrain in TerrainsInViewport)
                {
                    Ret.Add(currTerrain, i++);
                }

                IDNMcMapCamera[] CameraInViewport = Viewport.GetCameras();
                foreach (IDNMcMapCamera currCamera in CameraInViewport)
                {
                    Ret.Add(currCamera, i++);
                }

                IDNMcMapGrid GridInViewport = Viewport.Grid;
                if (GridInViewport != null)
                    Ret.Add(GridInViewport, i++);

                IDNMcMapHeightLines MapHeightLinesInViewport = Viewport.GetHeightLines();
                if (MapHeightLinesInViewport != null)
                    Ret.Add(MapHeightLinesInViewport, i++);

                IDNMcDtmMapLayer[] DTMInViewport = Viewport.GetQuerySecondaryDtmLayers();
                foreach (IDNMcDtmMapLayer dtmMapLayer in DTMInViewport)
                {
                    Ret.Add(dtmMapLayer, i++);
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Viewport.GetChildren()", McEx);
            }
            return Ret;
        }      

        public static void RemoveOnlyViewport(IDNMcMapViewport viewport)
        {
            dViewports.Remove(viewport);
            dViewportRenderNeededFlag.Remove(viewport);
        }

        public static Dictionary<IDNMcMapViewport, bool> dViewportRenderNeededFlag
        {
            get { return m_dViewportRenderNeededFlag; }
            set { m_dViewportRenderNeededFlag = value; }
        }

        public static bool IsExistViewportInManager(IDNMcMapViewport viewport)
        {
            uint value = 0;
            bool result = dViewports.TryGetValue(viewport,out value);
            return result;
        }


        public static void RenderScreenRectToBuffer(IDNMcMapViewport viewport,
            DNEPixelFormat viewportPixelFormat,
            uint numBufferRawPitch,
            uint widthDimension,
            uint heightDimension,
            Point pointTopLeft,
            Point pointBottomRight,
            string FolderPath = "",
            string fileName = "",
            bool isClosePic = false,
            bool isShowMsg = true, 
            StreamWriter streamWriter = null)
        {
            PixelFormat pixFormat = PixelFormat.Format16bppRgb565;
            uint pixelFormatByteCount = 0;

            DNSMcRect rect = new DNSMcRect(pointTopLeft, pointBottomRight);

            try
            {
                viewport.GetRenderToBufferNativePixelFormat(out viewportPixelFormat);
                pixelFormatByteCount = DNMcTexture.GetPixelFormatByteCount(viewportPixelFormat);

                switch (viewportPixelFormat)
                {
                    case DNEPixelFormat._EPF_L16:
                        pixFormat = PixelFormat.Format16bppGrayScale;
                        break;
                    case DNEPixelFormat._EPF_R5G6B5:
                        pixFormat = PixelFormat.Format16bppRgb565;
                        break;
                    case DNEPixelFormat._EPF_R8G8B8:
                        pixFormat = PixelFormat.Format24bppRgb;
                        break;
                    case DNEPixelFormat._EPF_A8R8G8B8:
                        pixFormat = PixelFormat.Format32bppArgb;
                        break;
                    case DNEPixelFormat._EPF_SHORT_RGB:
                        pixFormat = PixelFormat.Format48bppRgb;
                        break;
                    case DNEPixelFormat._EPF_SHORT_RGBA:
                        pixFormat = PixelFormat.Format64bppArgb;
                        break;
                    case DNEPixelFormat._EPF_X8R8G8B8:
                        pixFormat = PixelFormat.Format32bppRgb;
                        break;
                    case DNEPixelFormat._EPF_B8G8R8:
                        pixFormat = PixelFormat.Gdi;
                        break;
                    case DNEPixelFormat._EPF_L8:
                        pixFormat = PixelFormat.Indexed;
                        break;
                    case DNEPixelFormat._EPF_A8:
                        pixFormat = PixelFormat.Alpha;
                        break;
                    case DNEPixelFormat._EPF_A1R5G5B5:
                        pixFormat = PixelFormat.Format16bppArgb1555;
                        break;
                    default:
                        Manager_MCAutomation.HandleTesterExecption("Pixel Format", "Pixel format not supported");
                        return;
                }
            }
            catch (MapCoreException McEx)
            {
                Manager_MCAutomation.HandleMapCoreExecption("GetRenderToBufferNativePixelFormat", McEx);
            }

            uint stride = 0;
            if ((int)numBufferRawPitch == 0)
                stride = widthDimension * pixelFormatByteCount;
            else
                stride = numBufferRawPitch * pixelFormatByteCount;

            int bufferSize = (int)stride * (int)heightDimension;


            IntPtr ptr = System.Runtime.InteropServices.Marshal.AllocHGlobal(bufferSize);

            try
            {
                viewport.RenderScreenRectToBuffer(rect,
                widthDimension,
                heightDimension,
                viewportPixelFormat,
                numBufferRawPitch,
                ptr);

                try
                {
                    Bitmap bmp = new Bitmap((int)widthDimension, (int)heightDimension, (int)stride, pixFormat, ptr);

                    frmRenderScreenRectToBufferResult RenderScreenRectToBufferResultForm = new frmRenderScreenRectToBufferResult(bmp, (int)widthDimension, (int)heightDimension, viewportPixelFormat, ptr, FolderPath, fileName, isClosePic);
                    RenderScreenRectToBufferResultForm.ShowDialog();

                    bmp.Dispose();

                }
                catch (System.Exception ex)
                {
                    Manager_MCAutomation.HandleTesterExecption("Create new bitmap failed", "Create new bitmap failed by windows in the desired format");
                    Console.WriteLine(ex.Message);
                }

                System.Runtime.InteropServices.Marshal.FreeHGlobal(ptr);
            }
            catch (MapCoreException McEx)
            {
                Manager_MCAutomation.HandleMapCoreExecption("RenderScreenRectToBuffer", McEx);

            }
        }
    }
}
