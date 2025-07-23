package com.elbit.mapcore.mcandroidtester.utils;

import java.io.Serializable;

public class AMCTSerializableObject implements Serializable {

    public static final String AMCT_SERIALIZABLE_OBJECT = "AMCT_Serializable_Object";

    Object mcObject;

    public Object getMcObject() {
        return mcObject;
    }

    public void setMcObject(Object mcObject) {
        this.mcObject = mcObject;
    }

    public AMCTSerializableObject(){}
    public AMCTSerializableObject(Object _mcObject){
        mcObject = _mcObject;
    }
}
