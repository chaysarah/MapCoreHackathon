using MapCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using UnmanagedWrapper;

namespace MCTester.ObjectWorld.OverlayManagerWorld
{
    public class MCTOverlayManagerAsyncOperationCallback : IDNOverlayManagerAsyncOperationCallback
    {
        ucOverlay m_ucOverlay = null;
        string m_fullPath;

        public MCTOverlayManagerAsyncOperationCallback(ucOverlay ucOverlay, string fullPath)
        {
            m_ucOverlay = ucOverlay;
            m_fullPath = fullPath;
        }

        public void OnSaveObjectsAsRawVectorToFileResult(string strFileName, DNEMcErrorCode eStatus, string[] aAdditionalFiles)
        {
            if (eStatus == DNEMcErrorCode.SUCCESS)
            {
                m_ucOverlay.ShowAdditionalFiles(aAdditionalFiles);
                MessageBox.Show("Action completed successfully", "Save Objects As Raw Vector To File Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                OnReadError(eStatus, "On Save Objects As Raw Vector To File Result", strFileName);
        }

        public void OnSaveObjectsAsRawVectorToBufferResult(string strFileName, DNEMcErrorCode eStatus, byte[] auFileMemoryBuffer, DNSMcFileInMemory[] aAdditionalFiles)
        {
            if (eStatus == DNEMcErrorCode.SUCCESS)
            {
                string strFullFilePath = string.Concat(@m_fullPath, strFileName);
                m_ucOverlay.SaveByteArrayToFile(strFullFilePath, auFileMemoryBuffer, aAdditionalFiles, m_fullPath);
                MessageBox.Show("Action completed successfully", "Save Objects As Raw Vector To Buffer Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                OnReadError(eStatus, "On Save Objects As Raw Vector To Buffer Result", strFileName);
        }

        public void OnReadError(DNEMcErrorCode eErrorCode, string title, string strFileName)
        {
            MessageBox.Show(strFileName + " " + IDNMcErrors.ErrorCodeToString(eErrorCode) , title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
