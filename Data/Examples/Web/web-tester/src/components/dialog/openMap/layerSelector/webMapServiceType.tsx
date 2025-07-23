import { useEffect, useState } from "react";
import { InputNumber } from "primereact/inputnumber";
import { TabPanel, TabView } from "primereact/tabview";
import { Checkbox } from "primereact/checkbox";
import { InputText } from "primereact/inputtext";
import { Dropdown } from "primereact/dropdown";
import _ from "lodash";

import {  LayerSourceEnum, getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import { runCodeSafely } from "../../../../common/services/error-handling/errorHandler";

export default function WebMapServiceType(props: {
    setWebMapService: (serverParams: any) => void,
    setCoorSys: (coorSys: any) => void,
    serverParamsInit: any,
    serviceType: LayerSourceEnum,
    SWebMapServiceParams: MapCore.IMcMapLayer.SWebMapServiceParams,
    selectedLayer: any
}) {

    let [serverParams, setServerParams] = useState(props.serverParamsInit)

    useEffect(() => {
        setServerParams(props.serverParamsInit)
    }, [props.serverParamsInit])


    let [coorSys, setCoorSys] = useState(props.selectedLayer ? props.selectedLayer.aTileMatrixSets[0] : "")

    useEffect(() => {
        if (props.selectedLayer)
            setCoorSys(props.selectedLayer.aTileMatrixSets[0])
        else
            setCoorSys("")
    }, [props.selectedLayer])

    useEffect(() => {
        props.setCoorSys(coorSys?.strName ? coorSys.strName : coorSys)
    }, [coorSys])

    const getActiveTab = (): number => {
        switch (props.serviceType) {
            case LayerSourceEnum.CSW:
                return 0
            case LayerSourceEnum.MAPCORE:
                return null
            case LayerSourceEnum.WCS:
                return 1
            case LayerSourceEnum.WMS:
                return 2
            case LayerSourceEnum.WMTS:
            case LayerSourceEnum.CSW_WMTS:
                return 4
        }
    }
    let [activeTab, setActiveTab] = useState(getActiveTab())

    const saveData = (event: any) => {
        let value;
        value = event.target.type === "checkbox" ? event.target.checked : event.target.value
        runCodeSafely(() => {
            let class_name = event.originalEvent?.currentTarget?.className
            if (class_name?.includes("p-dropdown-item")) {
                value = event.target.value.theEnum;
            }
            let newParams = { ...serverParams, [event.target.name]: value }
            setServerParams(newParams)
            props.setWebMapService(newParams)
        }, "EditModePropertiesDialog/General.saveData => onChange")
    }
    const [enumDetails] = useState({
        ECoordinateAxesOrder: getEnumDetailsList(MapCore.IMcMapLayer.ECoordinateAxesOrder)
    });

    const CSWParams = () => {
        return <div>
            <div >
                <Checkbox onChange={e => { saveData(e) }}
                    checked={serverParams.OrthometricHeights} name="OrthometricHeights"></Checkbox>
                <label className="ml-2">Orthometric Heights</label>
            </div>

        </div>
    }

    const WCSParams = () => {
        return <div>
            <div >
                <label>WCS Version :</label>
                <InputText value={serverParams.strWCSVersion} name="strWCSVersion" onChange={(e) => { saveData(e) }} />
            </div>
            <div >
                <Checkbox onChange={e => { saveData(e) }}
                    checked={serverParams.bDontUseServerInterpolation} name="bDontUseServerInterpolation"></Checkbox>
                <label className="ml-2">Dont Use Server Interpolation</label>
            </div>
        </div>
    }
    const WMSParams = () => {
        return <div>
            <div >
                <label>Block Width:</label>
                <InputNumber value={serverParams.uBlockWidth} name="uBlockWidth" onValueChange={saveData} mode="decimal" />
            </div>
            <div >
                <label>Block Height:</label>
                <InputNumber value={serverParams.uBlockHeight} name="uBlockHeight" onValueChange={saveData} mode="decimal" />
            </div>
            <div >
                <label>Min Scale:</label>
                <InputNumber value={serverParams.fMinScale} name="fMinScale" onValueChange={saveData} mode="decimal" />
            </div>
            <div >
                <label>WMS Version :</label>
                <InputText value={serverParams.strWMSVersion} name="strWMSVersion" onChange={saveData} />
            </div>
            <div >
                <label>WMS Coordinate System (EPSG:XXXX)</label>
                {
                    props.selectedLayer ?
                        <Dropdown style={{ width: 'max(150px,9.5vw)' }} optionLabel='strName'
                            onChange={(event) => setCoorSys(event.target.value)}
                            value={coorSys}
                            options={props.selectedLayer.aTileMatrixSets}
                        />
                        : <InputText style={{ width: 'max(150px,9.5vw)' }} name=""
                            value={coorSys} onChange={(e) => { setCoorSys(e.target.value) }}
                        />}
            </div>
        </div>
    }
    const WMTSParams = () => {
        return <div>
            <div className='object-props__flex-and-row-between' >
                <label>Info Format:</label>
                <InputText value={serverParams?.strInfoFormat} onChange={saveData} name="strInfoFormat" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label>Tile Matrix Set</label>
                {props.selectedLayer ?
                    <Dropdown optionLabel='strName'
                        onChange={(event) => setCoorSys(event.target.value)}
                        value={coorSys}
                        options={props.selectedLayer?.aTileMatrixSets}
                    />
                    : <InputText name="" value={coorSys}
                        onChange={(e) => { setCoorSys(e.target.value) }}
                    />}
            </div>
            <div >
                <Checkbox checked={serverParams?.bUseServerTilingScheme} name="bUseServerTilingScheme" onChange={saveData}></Checkbox>
                <label className="ml-2">Use Server Tiling Scheme</label>
            </div>
            {/* <div >
                <Checkbox onChange={saveData} checked={true}></Checkbox>
                לעשות יותר מאוחר
                <label className="ml-2">Is Server Tiling Scheme Actually Used</label>
            </div> */}
            <div >
                <Checkbox checked={serverParams?.bExtendBeyondDateLine} name="bExtendBeyondDateLine" onChange={saveData} ></Checkbox>
                <label className="ml-2"> Extend Beyond Date Line</label>
            </div>
            <div className='object-props__flex-and-row-between'>
                <label style={{ whiteSpace: 'wrap' }}>Capabilities Bounding Box Axes Order: </label>
                <Dropdown name="eCapabilitiesBoundingBoxAxesOrder"
                    value={getEnumValueDetails(serverParams?.eCapabilitiesBoundingBoxAxesOrder, enumDetails.ECoordinateAxesOrder)}
                    onChange={saveData} optionLabel='name'
                    options={enumDetails.ECoordinateAxesOrder} />
            </div>
        </div>
    }

    return (<>
        <div>
            <TabView className="manual-layers__tab-view" activeIndex={activeTab} onTabChange={e => setActiveTab(e.index)}>
                {[LayerSourceEnum.CSW, LayerSourceEnum.WCS, LayerSourceEnum.WMS, , LayerSourceEnum.WMTS].map((item: LayerSourceEnum, key: number) => {
                    return <TabPanel header={item} key={key} disabled={true}>
                        {props.serviceType == item && eval(item + "Params()")
                        }
                    </TabPanel>
                })}
            </TabView>
        </div>
    </>
    );
}