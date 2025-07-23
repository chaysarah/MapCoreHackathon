import { useEffect, useState } from "react"
import RawVectorParamsCtrl, { cloneRawVectorParams } from "./rawVectorParamsCtrl"
import { Button } from "primereact/button";
import _ from 'lodash';

export default function RawVectorTryExample() {
    let [rawVectorParams, setRawVectorParams] = useState<MapCore.IMcRawVectorMapLayer.SParams>(new MapCore.IMcRawVectorMapLayer.SParams('', null));

    useEffect(() => {
        let params = new MapCore.IMcRawVectorMapLayer.SParams('', null)
        params.fMinScale = 4;
        params.fMaxScale = 3;
        setRawVectorParams(params)
    }, [])

    const getParams = (localRawVectorParams: MapCore.IMcRawVectorMapLayer.SParams) => {
        setRawVectorParams(localRawVectorParams)
    }
    const changeMaxScale = () => {
        let updatedObj = cloneRawVectorParams(rawVectorParams);
        updatedObj.fMaxScale = 6
        setRawVectorParams(updatedObj);
    }

    return <div>
        <RawVectorParamsCtrl getRawVectorParams={getParams} defaultRawVectorParams={rawVectorParams} />
        <Button label='change max scale to 6' onClick={changeMaxScale} />
    </div>
}