package com.elbit.mapcore.mcandroidtester.model;

import java.io.Serializable;
import com.elbit.mapcore.Interfaces.General.IMcUserData;

/**
 * Created by tc97803 on 19/04/2017.
 */

public class AMCTUserData implements Serializable, IMcUserData {

    private byte[] m_bUserDataBuffer;

    public AMCTUserData(byte[] buffer)
    {
        m_bUserDataBuffer = buffer;
    }

    public byte[] getUserDataBuffer()
    {
        return m_bUserDataBuffer;
    }

    public void setUserDataBuffer(byte[] value)
    {
         m_bUserDataBuffer = value;
    }

    @Override
    public IMcUserData Clone()
    {
        byte[] copyArr = new byte[m_bUserDataBuffer.length];
        copyArr = m_bUserDataBuffer.clone();

        return new AMCTUserData(copyArr);
    }

    @Override
    public int GetSaveBufferSize()
    {
        return (int)m_bUserDataBuffer.length;
    }

    @Override
    public boolean IsSavedBufferUTF8Bytes() {
        return true;
    }

    @Override
    public byte[] SaveToBuffer()
    {
        return m_bUserDataBuffer;
    }

}
