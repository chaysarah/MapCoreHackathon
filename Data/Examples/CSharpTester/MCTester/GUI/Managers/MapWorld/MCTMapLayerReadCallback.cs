using MapCore;
using MapCore.Common;
using MCTester.MapWorld;
using MCTester.MapWorld.MapUserControls;
using MCTester.ObjectWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using UnmanagedWrapper;

namespace MCTester.Managers.MapWorld
{
    public class MCTMapLayerReadCallback : IDNMapLayerReadCallback
    {
        private static bool mIsSaveReplacedOrRemovedLayer = false;
        private static bool mIsCheckNativeServerLayer = false;

        public static bool IsCheckNativeServerLayer
        {
            get { return mIsCheckNativeServerLayer; }
            set { mIsCheckNativeServerLayer = value; }
        }

        // private static int mCounterDelayLoadLayers = 0;
        private static bool mIsShowError = false;
        private static bool mIsShowFailedError = false;
        private static MCTMapLayerReadCallback mInstance;

        private static Dictionary<IDNMcMapLayer, IDNMcMapLayer> mReplacedLayers = new Dictionary<IDNMcMapLayer, IDNMcMapLayer>();
        private static List<IDNMcMapLayer> mRemovedLayers = new List<IDNMcMapLayer>();

        private static Dictionary<IntPtr, List<DNEMcErrorCode>> OnInitializedErrorDontShowErrorMsg = new Dictionary<IntPtr, List<DNEMcErrorCode>>();

        public static byte CallbackBuffer = 0;

        public MCTMapLayerReadCallback()
        { }

        public static bool IsSaveReplacedOrRemovedLayer
        {
            get { return mIsSaveReplacedOrRemovedLayer; }
            set { mIsSaveReplacedOrRemovedLayer = value; }
        }

        public static MCTMapLayerReadCallback getInstance()
        {
            if (mInstance == null)
                mInstance = new MCTMapLayerReadCallback();
            return mInstance;
        }

        public static void ResetReadCallbackCounter(bool isResetCounter = true)
        {
            mIsShowError = false;
        }

        public static void SetMapLayerReadCallback(DNSNonNativeParams nonNativeParams)
        {
            nonNativeParams.pReadCallback = getInstance();
        }

        public static void SetIsShowError(bool flag)
        {
            mIsShowError = flag;
        }

        public static bool GetIsShowError()
        {
            return mIsShowError;
        }

        public static void SetIsShowFailedError(bool flag)
        {
            mIsShowFailedError = flag;
        }

        public static bool GetIsShowFailedError()
        {
            return mIsShowFailedError;
        }

        #region IDNMapLayerReadCallback Members
        public void OnInitialized(IDNMcMapLayer pLayer, DNEMcErrorCode eStatus, string strAdditionalDataString)
        {
            if (eStatus != DNEMcErrorCode.SUCCESS && eStatus != DNEMcErrorCode.NATIVE_SERVER_LAYER_NOT_VALID)
            {
                mIsShowFailedError = true;

                if (pLayer != null)
                {
                    try
                    {
                        pLayer.RemoveLayerAsync();
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("RemoveLayerAsync", McEx);
                    }
                }
                OnReadError(pLayer, eStatus, strAdditionalDataString, "On Initialized Map Layer Failed");
            }
            else if (eStatus == DNEMcErrorCode.NATIVE_SERVER_LAYER_NOT_VALID)
                mIsCheckNativeServerLayer = true;
            else if (eStatus == DNEMcErrorCode.SUCCESS && pLayer != null && pLayer.LayerType.ToString().ToLower().Contains("native"))
            {
                Manager_MCGridCoordinateSystem.AddNewGridCoordinateSystem(pLayer.CoordinateSystem);
            }
        }

        public void OnReadError(IDNMcMapLayer pLayer, DNEMcErrorCode eErrorCode, string strAdditionalDataString)
        {
            OnReadError(pLayer, eErrorCode, strAdditionalDataString, "On Read Error Map Layer");
        }

        public void OnReadError(IDNMcMapLayer pLayer, DNEMcErrorCode eErrorCode, string strAdditionalDataString, string title)
        {
            bool isExistErrorToLayerInDic = false;
            bool isExistLayerInDic = false;

            if (pLayer != null)
            {
                isExistErrorToLayerInDic = OnInitializedErrorDontShowErrorMsg.Keys.ToList().Any(x => x == pLayer.GetBaseUnmanagedPtr() && OnInitializedErrorDontShowErrorMsg[x].Contains(eErrorCode)); 
                isExistLayerInDic = OnInitializedErrorDontShowErrorMsg.ContainsKey(pLayer.GetBaseUnmanagedPtr());
            }

            bool isNotShowAgain = false;

            if (pLayer == null)
            {
                MessageBox.Show(IDNMcErrors.ErrorCodeToString(eErrorCode) + " (" + strAdditionalDataString + ")", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!isExistErrorToLayerInDic)
                isNotShowAgain = ShowCustomMessageBox(IDNMcErrors.ErrorCodeToString(eErrorCode) + " (" + strAdditionalDataString + ")", title);


            if (pLayer != null && isNotShowAgain && !isExistErrorToLayerInDic )
            {
                if (isExistLayerInDic)
                {

                    OnInitializedErrorDontShowErrorMsg[pLayer.GetBaseUnmanagedPtr()].Add(eErrorCode);
                }
                else
                {
                    OnInitializedErrorDontShowErrorMsg.Add(pLayer.GetBaseUnmanagedPtr(), new List<DNEMcErrorCode>() { eErrorCode });
                }
            }
        }

        static bool ShowCustomMessageBox(string message, string title)
        {
            Form form = new Form();

            Label label = new Label() { Left = 20, Top = 20, AutoSize = true };

            StringBuilder result = new StringBuilder();
            int lineLength = 60;
            for (int i = 0; i < message.Length; i += lineLength)
            {
                if (i + lineLength < message.Length)
                    result.AppendLine(message.Substring(i, lineLength));
                else
                    result.Append(message.Substring(i)); // Append last part without newline
            }

            label.Text = result.ToString();  
            CheckBox checkBox = new CheckBox() { Text = "Don't show this again", Left = 20, Top = 80, AutoSize = true };
            Button okButton = new Button() { Text = "OK", Left = 100, Width = 80, Top = 110, DialogResult = DialogResult.OK };
            
            form.Text = title;
            form.ClientSize = new System.Drawing.Size(400, 150);
            form.Controls.Add(label);
            form.Controls.Add(checkBox);
            form.Controls.Add(okButton);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.AcceptButton = okButton;
           
            DialogResult dialogResult = form.ShowDialog();
            return checkBox.Checked;
        }

        public static void CheckIfNewLayerExistInDontShowErrorMsg(IntPtr newCreatedLayerPointer)
        {
            if(OnInitializedErrorDontShowErrorMsg.ContainsKey(newCreatedLayerPointer))
            {
                OnInitializedErrorDontShowErrorMsg.Remove(newCreatedLayerPointer);
            }
        }

        public void OnNativeServerLayerNotValid(IDNMcMapLayer pLayer, bool bLayerVersionUpdated)
        {
            string msgText = "Native Server Map Layer (" + pLayer.LayerType.ToString().Replace("_ELT_", "").Replace("_", " ") + ")  is not valid: ";
            string msgText2 = "Layer's version was updated, Do you want to replace the layer?";
            if (!bLayerVersionUpdated)
                msgText2 = "Layer's ID was not found, Do you want to remove the layer?";
            DialogResult dialogResult = MessageBox.Show(msgText + msgText2, "On Native Server Map Layer Not Valid", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    if (bLayerVersionUpdated)
                        pLayer.ReplaceNativeServerLayerAsync(getInstance());
                    else
                        pLayer.RemoveLayerAsync();
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("ReplaceNativeServerLayerAsync/RemoveLayerAsync", McEx);
                }
            }
            else
                mIsCheckNativeServerLayer = false;
        }

        public void OnRemoved(IDNMcMapLayer pLayer, DNEMcErrorCode eStatus, string strAdditionalDataString)
        {
            mIsShowFailedError = false;
            if (eStatus == DNEMcErrorCode.SUCCESS)
            {
                Manager_MCLayers.RemoveLayerFromTester(pLayer);
                AddRemovedLayers(pLayer);
                MessageBox.Show("Removed map layer successfully", "On Removed Map Layer", MessageBoxButtons.OK);
            }
            else
            {
                OnReadError(pLayer, eStatus, strAdditionalDataString, "On Removed Map Layer");
            }
            mIsCheckNativeServerLayer = false;
        }

        public void OnReplaced(IDNMcMapLayer pOldLayer, IDNMcMapLayer pNewLayer, DNEMcErrorCode eStatus, string strAdditionalDataString)
        {
            mIsShowFailedError = false;
            if (eStatus == DNEMcErrorCode.SUCCESS)
            {
                Manager_MCLayers.RemoveLayerFromTester(pOldLayer, pNewLayer);
                AddReplacedLayers(pOldLayer, pNewLayer);
                MessageBox.Show("Replaced map layer successfully", "On Replaced Map Layer", MessageBoxButtons.OK);
            }
            else
            {
                OnReadError(pOldLayer, eStatus, strAdditionalDataString, "On Replaced Map Layer");
            }
            mIsCheckNativeServerLayer = false;
        }

        private static void AddReplacedLayers(IDNMcMapLayer pOldLayer, IDNMcMapLayer pNewLayer)
        {
            if (mIsSaveReplacedOrRemovedLayer)
            {
                if (mReplacedLayers.ContainsKey(pOldLayer))
                    mReplacedLayers[pOldLayer] = pNewLayer;
                else
                    mReplacedLayers.Add(pOldLayer, pNewLayer);
            }
        }

        public static IDNMcMapLayer GetReplacedLayer(IDNMcMapLayer pOldLayer)
        {
            if (mReplacedLayers.ContainsKey(pOldLayer))
            {
                IDNMcMapLayer pNewLayer = mReplacedLayers[pOldLayer];
                mReplacedLayers.Remove(pOldLayer);
                return pNewLayer;
            }
            else
                return null;
        }

        public static bool IsExistReplacedLayer(IDNMcMapLayer pOldLayer)
        {
            return mReplacedLayers.ContainsKey(pOldLayer);
        }

        public static void RemoveAllReplacedLayer()
        {
            mReplacedLayers.Clear();
            mRemovedLayers.Clear();
            mIsSaveReplacedOrRemovedLayer = false;
        }

        public static void AddRemovedLayers(IDNMcMapLayer pLayer)
        {
            if (mIsSaveReplacedOrRemovedLayer)
                mRemovedLayers.Add(pLayer);
        }

        public static bool IsLayerRemoved(IDNMcMapLayer pLayer)
        {
            return mRemovedLayers.Contains(pLayer);
        }

        public static List<IDNMcMapLayer> CheckLayersIsRemovedOrReplaced(List<IDNMcMapLayer> layers)
        {
            List<IDNMcMapLayer> newLayers = new List<IDNMcMapLayer>();
            for (int i = 0; i < layers.Count; i++)
            {
                IDNMcMapLayer mapLayer = CheckLayerIsRemovedOrReplaced(layers[i]);
                if (mapLayer != null)
                    newLayers.Add(mapLayer);
            }
            return newLayers;
        }

        public static IDNMcMapLayer CheckLayerIsRemovedOrReplaced(IDNMcMapLayer layer)
        {
            IDNMcMapLayer checkedLayer = layer;

            if (IsLayerRemoved(layer))
                checkedLayer = null;
            else if (IsExistReplacedLayer(layer))
                checkedLayer = GetReplacedLayer(layer);

            return checkedLayer;
        }

        public void OnPostProcessSourceData(IDNMcMapLayer pLayer, bool bGrayscaleSource, DNSTilePostProcessData[] aVisibleTiles)
        {
            DebugPostProcessTiles(aVisibleTiles, aVisibleTiles == null ? 0 : (uint)aVisibleTiles.Length);
        }

        public void DebugPostProcessTiles(DNSTilePostProcessData[] aTiles, uint uNumTiles)
        {
            for (uint uIdx = 0; uIdx < uNumTiles; ++uIdx)
            {
                if (aTiles[uIdx].pSrcBuffer != IntPtr.Zero)
                {
                    if (aTiles[uIdx].uSrcBytesPerChannel == 0)
                    {
                        MessageBox.Show("uSrcBytesPerChannel cannot be zero.", "OnPostProcessSourceData", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    byte[] aRGBA = GetBitShifts(aTiles[uIdx].eTilePixelFormat);
                    aRGBA[0] /= 8;
                    aRGBA[1] /= 8;
                    aRGBA[2] /= 8;
                    aRGBA[3] /= 8;

                    byte[] aSrcRGBA = new byte[4]
                    {
                    (byte)(aRGBA[0] * aTiles[uIdx].uSrcBytesPerChannel),
                    (byte)(aRGBA[1] * aTiles[uIdx].uSrcBytesPerChannel),
                    (byte)(aRGBA[2] * aTiles[uIdx].uSrcBytesPerChannel),
                    (byte)(aRGBA[3] * aTiles[uIdx].uSrcBytesPerChannel)
                    };

                    uint nTileBytesPerPixel = DNMcTexture.GetPixelFormatByteCount(aTiles[uIdx].eTilePixelFormat);
                    uint nTileBytesPerPixelPerDataType = (uint)(nTileBytesPerPixel * aTiles[uIdx].uSrcBytesPerChannel);
                    uint uSrcLineSizeInBytes = (uint)(aTiles[uIdx].TileSizeInPixels.Width * nTileBytesPerPixelPerDataType);
                    uint uSrcLineIdx = 0;
                    uint nTileWidthInBytes = (uint)aTiles[uIdx].TileSizeInPixels.Width * nTileBytesPerPixel;

                    for (uint uLineIdx = 0; uLineIdx < aTiles[uIdx].TileSizeInPixels.Height; ++uLineIdx, uSrcLineIdx += uSrcLineSizeInBytes)
                    {
                        for (uint uPixelIdx = 0; uPixelIdx < uSrcLineSizeInBytes; uPixelIdx += nTileBytesPerPixelPerDataType)
                        {
                            int nIndex = (int)(uLineIdx * nTileWidthInBytes + (uPixelIdx / aTiles[uIdx].uSrcBytesPerChannel));

                            for (uint uBandIdx = 0; uBandIdx < nTileBytesPerPixel; ++uBandIdx)
                            {
                                double nChannelColor = 0;
                                int bufferIndex = (int)(uSrcLineIdx + uPixelIdx + aSrcRGBA[uBandIdx]);

                                IntPtr srcPtr = IntPtr.Add(aTiles[uIdx].pSrcBuffer, bufferIndex);

                                switch (aTiles[uIdx].uSrcBytesPerChannel)
                                {
                                    case 2:
                                        nChannelColor = Marshal.ReadInt16(srcPtr);
                                        break;
                                    case 4:
                                        nChannelColor = Marshal.ReadInt32(srcPtr);
                                        break;
                                    default:
                                        nChannelColor = 0;
                                        break;
                                }

                                IntPtr destPtr = IntPtr.Add(aTiles[uIdx].pTileBuffer, nIndex + aRGBA[uBandIdx]);
                                Marshal.WriteByte(destPtr, (byte)nChannelColor);
                            }
                        }
                    }
                }
            }
        }

        private byte[] GetBitShifts(DNEPixelFormat pixelFormat)
        {
            // Custom implementation for getting bit shifts
            switch (pixelFormat)
            {
                case DNEPixelFormat._EPF_R8G8B8:
                    return new byte[] { 0, 8, 16, 24 }; // R, G, B, no alpha
                case DNEPixelFormat._EPF_A8R8G8B8:
                    return new byte[] { 8, 16, 24, 0 }; // A, R, G, B
                case DNEPixelFormat._EPF_R8G8B8A8:
                    return new byte[] { 0, 8, 16, 24 }; // R, G, B, A
                default:
                    return new byte[] { 0, 8, 16, 24 }; // Default RGB ordering
            }
        }

        public uint GetSaveBufferSize()
        {
            return sizeof(byte);
        }

        public byte[] SaveToBuffer()
        {
            return new byte[1] { CallbackBuffer };
        }

        #endregion

    }
}
