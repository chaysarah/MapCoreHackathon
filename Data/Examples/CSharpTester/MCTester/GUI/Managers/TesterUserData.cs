using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.IO;

namespace MCTester
{
    [Serializable]
    public class TesterUserData: IDNMcUserData
    {
        private byte [] m_bUserDataBuffer;
        
        public byte [] UserDataBuffer
        {
            get { return m_bUserDataBuffer; }
            set { m_bUserDataBuffer = value; }
        }

        public TesterUserData(byte [] buffer)
        {
            m_bUserDataBuffer = buffer;
        }

       
        #region IDNMcUserData Members

        public IDNMcUserData Clone()
        {
            byte[] copyArr = new byte[m_bUserDataBuffer.Length];
            m_bUserDataBuffer.CopyTo(copyArr, 0);

            return new TesterUserData(copyArr);
        }

        public uint GetSaveBufferSize()
        {
            return (uint)m_bUserDataBuffer.Length;
        }

        public bool IsSavedBufferUTF8Bytes()
        {
            return true;
        }

        public byte[] SaveToBuffer()
        {
            return m_bUserDataBuffer;
        }

        #endregion


    }
}
