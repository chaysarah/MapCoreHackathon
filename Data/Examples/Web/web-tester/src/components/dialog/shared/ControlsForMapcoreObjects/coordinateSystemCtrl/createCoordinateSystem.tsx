import { useSelector } from "react-redux";
import { useEffect, useState } from "react";
import { Fieldset } from "primereact/fieldset";
import { InputNumber } from "primereact/inputnumber"
import { Button } from "primereact/button";
import { Dropdown } from "primereact/dropdown";
import { RadioButton } from "primereact/radiobutton";
import { InputText } from "primereact/inputtext";
import { Checkbox } from "primereact/checkbox";

import { getEnumDetailsList, getEnumValueDetails, MapCoreData, MapCoreService } from 'mapcore-lib';
import { AppState } from "../../../../../redux/combineReducer";
import { EGridCoordSystemType } from "../../../../../tools/enum/coordinateSystem";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";

export default function CreateCoordinateSystem(props: {
    getFooter: (thisFooter: any) => void,
    getNewCoorSys: (NewCoorSys: MapCore.IMcGridCoordinateSystem) => void, ToClose: () => void
}) {

    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    let [device, setDevice] = useState(MapCoreData.device)

    const [enumDetails] = useState({
        EDatumType: getEnumDetailsList(MapCore.IMcGridCoordinateSystem.EDatumType),
        EGridCoordSystemType: Object.values(EGridCoordSystemType).map(o => { return { name: o } }),
    });

    let [datumParams, setDatumParams] = useState(new MapCore.IMcGridCoordinateSystem.SDatumParams())
    let [userDefined, setUserDefined] = useState({
        centralMeridian: null,
        falseEasting: null,
        falseNorthing: null,
        latitudeOfGridOrigin: null,
        scaleFactor: null,
        zoneWidth: null,
    })
    let [datum, setDatum] = useState(MapCore.IMcGridCoordinateSystem.EDatumType.EDT_WGS84)
    let [gridCoordinateSystem, setGridCoordinateSystem] = useState({ name: EGridCoordSystemType.EGCS_GEOGRAPHIC })
    let [zone, setZone] = useState(0)

    let [datumVisible, setDatumVisible] = useState(true)
    let [zoneVisible, setZoneVisible] = useState(false)

    let [genericRadio, setGenericRadio] = useState("SRID");
    let [inputSRID, setInputSRID] = useState("")
    let [inputInitString, setInputInitString] = useState("")
    let [inputArgs, setInputArgs] = useState("")
    let [datumParamsDisabled, setDatumParamsDisabled] = useState(false)
    let [newGridCoordinateSystem, setNewGridCoordinateSystem] = useState(null)

    useEffect(() => {
        const root = document.documentElement;
        let pixelWidth = window.innerHeight * 0.6 * globalSizeFactor;
        root.style.setProperty('--coordinate-system-dialog-width', `${pixelWidth}px`);
    }, [])
    useEffect(() => {
        props.getFooter(() => {
            return <div className='form__footer-padding'>
                <Button onClick={() => {
                    runCodeSafely(() => {
                        if (!MapCoreData.device && (gridCoordinateSystem.name == EGridCoordSystemType.EGCS_GENERIC_GRID))
                            throw Error("You can't create a coordinateSystem without a device.");
                        else {
                            CreateCoordSys()
                        }
                    }, " CreateCoordinateSystem=> useEffect.CreateCoordSys")
                }}>OK</Button>
            </div>
        })
    }, [datumParams, gridCoordinateSystem, datum, userDefined, inputSRID, inputInitString, inputArgs, genericRadio, zone])

    useEffect(() => {
        if ((gridCoordinateSystem.name == EGridCoordSystemType.EGCS_GEOCENTRIC) ||
            (gridCoordinateSystem.name == EGridCoordSystemType.EGCS_GEOGRAPHIC) ||
            (gridCoordinateSystem.name == EGridCoordSystemType.EGCS_TM_USER_DEFINED) ||
            (gridCoordinateSystem.name == EGridCoordSystemType.EGCS_UTM))
            setDatumVisible(true)
        else
            setDatumVisible(false)

        if ((gridCoordinateSystem.name == EGridCoordSystemType.EGCS_S42) ||
            (gridCoordinateSystem.name == EGridCoordSystemType.EGCS_TM_USER_DEFINED) ||
            (gridCoordinateSystem.name == EGridCoordSystemType.EGCS_UTM))
            setZoneVisible(true)
        else
            setZoneVisible(false)

    }, [gridCoordinateSystem])

    const CreateCoordSys = () => {
        runCodeSafely(() => {
            let newGridCoordinateSystem: MapCore.IMcGridCoordinateSystem = null
            let datumParams_: MapCore.IMcGridCoordinateSystem.SDatumParams = new MapCore.IMcGridCoordinateSystem.SDatumParams(datumParams.dA, datumParams.dF, datumParams.dDX, datumParams.dDY, datumParams.dDZ, datumParams.dRx, datumParams.dRy, datumParams.dRz, datumParams.dS)
            switch (gridCoordinateSystem.name) {
                case EGridCoordSystemType.EGCS_GEOGRAPHIC:
                    newGridCoordinateSystem = MapCore.IMcGridCoordSystemGeographic.Create(datum, datumParamsDisabled ? datumParams_ : null);
                    break;
                case EGridCoordSystemType.EGCS_GEOCENTRIC:
                    newGridCoordinateSystem = MapCore.IMcGridCoordSystemGeocentric.Create(datum, datumParamsDisabled ? datumParams_ : null);
                    break;
                case EGridCoordSystemType.EGCS_TM_USER_DEFINED:
                    let gridParams = new MapCore.IMcGridCoordSystemTraverseMercator.STMGridParams(userDefined.falseNorthing, userDefined.falseEasting, userDefined.centralMeridian, userDefined.latitudeOfGridOrigin, userDefined.scaleFactor, userDefined.zoneWidth)
                    newGridCoordinateSystem = MapCore.IMcGridTMUserDefined.Create(gridParams, zone, datum, datumParams);
                    break;
                case EGridCoordSystemType.EGCS_UTM:
                    newGridCoordinateSystem = MapCore.IMcGridUTM.Create(zone, datum, datumParams);
                    break;
                case EGridCoordSystemType.EGCS_NEW_ISRAEL:
                    newGridCoordinateSystem = MapCore.IMcGridNewIsrael.Create();
                    break;
                case EGridCoordSystemType.EGCS_RSO_SINGAPORE:
                    newGridCoordinateSystem = MapCore.IMcGridRSOSingapore.Create();
                    break;
                case EGridCoordSystemType.EGCS_NZMG:
                    newGridCoordinateSystem = MapCore.IMcGridNZMG.Create();
                    break;
                case EGridCoordSystemType.EGCS_S42:
                    newGridCoordinateSystem = MapCore.IMcGridS42.Create(zone, datum, datumParams);
                    break;
                case EGridCoordSystemType.EGCS_RT90:
                    newGridCoordinateSystem = MapCore.IMcGridRT90.Create();
                    break;
                case EGridCoordSystemType.EGCS_MGRS:
                    newGridCoordinateSystem = MapCore.IMcGridMGRS.Create();
                    break;
                case EGridCoordSystemType.EGCS_BNG:
                    newGridCoordinateSystem = MapCore.IMcGridBNG.Create();
                    break;
                case EGridCoordSystemType.EGCS_GEOREF:
                    newGridCoordinateSystem = MapCore.IMcGridGEOREF.Create();
                    break;
                case EGridCoordSystemType.EGCS_GARS:
                    newGridCoordinateSystem = MapCore.IMcGridGARS.Create();
                    break;
                case EGridCoordSystemType.EGCS_IRISH:
                    newGridCoordinateSystem = MapCore.IMcGridIrish.Create();
                    break;
                case EGridCoordSystemType.EGCS_KKJ:
                    break;
                case EGridCoordSystemType.EGCS_INDIA_LCC:
                    break;
                case EGridCoordSystemType.EGCS_GENERIC_GRID:
                    if (genericRadio == "SRID") {
                        newGridCoordinateSystem = MapCore.IMcGridGeneric.Create(inputSRID);
                    }
                    else if (genericRadio == "InitString") {
                        newGridCoordinateSystem = MapCore.IMcGridGeneric.Create(inputInitString, false);
                    }
                    else {
                        let args: string[] = inputArgs.split(" ");
                        newGridCoordinateSystem = MapCore.IMcGridGeneric.Create(args);
                    }
                    break;
            }
            setNewGridCoordinateSystem(newGridCoordinateSystem);
            props.getNewCoorSys(newGridCoordinateSystem)
            MapCoreService.addCoordinateSystemToList(newGridCoordinateSystem)
            props.ToClose()
        }, "")
    }
    const saveData = (event: any) => {
        setDatumParams({ ...datumParams, [event.target.name]: event.target.type === "checkbox" ? event.target.checked : event.target.value });
    }
    const saveDataUserDefined = (event: any) => {
        setUserDefined({ ...userDefined, [event.target.name]: event.target.type === "checkbox" ? event.target.checked : event.target.value });
    }
    const getInit = () => {
        let initString: string
        runCodeSafely(() => {
            runMapCoreSafely(() => {
                if (device == null)
                    throw Error("You can't create a coordinateSystem without a device.");
                else {
                    initString = MapCore.IMcGridGeneric.GetFullInitializationString(inputSRID);
                }
            }, "CreateCoordinateSystem=> getInit.GetFullInitializationString", true)
            setInputInitString(initString);
        }, "CreateCoordinateSystem=> getInit")
    }

    return (<div style={{ height: `${globalSizeFactor * 55}vh`, width: `${globalSizeFactor * 1.5 * 50}vh` }}>
        <div>
            <div>
                <label> Coordinate System:</label>
                <Dropdown name="LocationCoordSys" value={gridCoordinateSystem} onChange={(e) => { setGridCoordinateSystem(e.value) }} options={enumDetails.EGridCoordSystemType} optionLabel="name" />
            </div>
            {datumVisible
                && <div>
                    <label>Datum:</label>
                    <Dropdown name="LocationCoordSys" value={getEnumValueDetails(datum, enumDetails.EDatumType)}
                        onChange={(e) => setDatum(e.target.value.theEnum)} options={enumDetails.EDatumType} optionLabel="name" />
                </div>}
            {zoneVisible && <div ><label >zone: </label>
                <InputNumber value={zone} onValueChange={(event) => { setZone(event.target.value) }}></InputNumber> </div>}
        </div>
        {datumVisible && <Fieldset legend={<><Checkbox onChange={(e) => { setDatumParamsDisabled(e.checked) }} checked={datumParamsDisabled}></Checkbox><label>Datum Params</label></>}>
            <div style={{ display: 'flex' }} className={`${!datumParamsDisabled && "form__disabled"}`}>
                <div style={{ marginRight: `${globalSizeFactor * 1.5 * 1}vh` }} >
                    <div style={{ marginBottom: `${globalSizeFactor * 0.5}vh`, justifyContent: 'space-between', display: "flex" }}>
                        <label>A</label>
                        <InputNumber value={datumParams.dA} name="dA" onValueChange={saveData}></InputNumber>
                    </div>
                    <div style={{ marginBottom: `${globalSizeFactor * 0.5}vh`, justifyContent: 'space-between', display: "flex" }}>
                        <label className="MarginRight">DX  </label>
                        <InputNumber value={datumParams.dDX} name="dDX" onValueChange={saveData} ></InputNumber>
                    </div>
                    <div style={{ marginBottom: `${globalSizeFactor * 0.5}vh`, justifyContent: 'space-between', display: "flex" }}>
                        <label>RX  </label>
                        <InputNumber value={datumParams.dRx} name="dRx" onValueChange={saveData}></InputNumber>
                    </div>
                </div>
                <div style={{ marginRight: `${globalSizeFactor * 1.5 * 1}vh` }}>
                    <div style={{ marginBottom: `${globalSizeFactor * 0.5}vh`, justifyContent: 'space-between', display: "flex" }}>
                        <label>F</label>
                        <InputNumber value={datumParams.dF} name="dF" onValueChange={saveData}></InputNumber>
                    </div>
                    <div style={{ marginBottom: `${globalSizeFactor * 0.5}vh`, justifyContent: 'space-between', display: "flex" }}>
                        <label className="MarginRight">DY  </label>
                        <InputNumber value={datumParams.dDY} name="dDY" onValueChange={saveData}></InputNumber>
                    </div>
                    <div style={{ marginBottom: `${globalSizeFactor * 0.5}vh`, justifyContent: 'space-between', display: "flex" }}>
                        <label>RY  </label>
                        <InputNumber value={datumParams.dRy} name="dRy" onValueChange={saveData}></InputNumber>
                    </div>
                </div>
                <div>
                    <div style={{ marginBottom: `${globalSizeFactor * 0.5}vh`, justifyContent: 'space-between', display: "flex" }}>
                        <label >S</label>
                        <InputNumber value={datumParams.dS} name="dS" onValueChange={saveData}></InputNumber>
                    </div>
                    <div style={{ marginBottom: `${globalSizeFactor * 0.5}vh`, justifyContent: 'space-between', display: "flex" }}>
                        <label className="MarginRight">DZ  </label>
                        <InputNumber value={datumParams.dDZ} name="dDZ" onValueChange={saveData}></InputNumber>
                    </div>
                    <div style={{ marginBottom: `${globalSizeFactor * 0.5}vh`, justifyContent: 'space-between', display: "flex" }}>
                        <label>RX  </label>
                        <InputNumber value={datumParams.dRx} name="dRx" onValueChange={saveData}></InputNumber>
                    </div>
                </div>
            </div>
        </Fieldset>}
        {gridCoordinateSystem.name == EGridCoordSystemType.EGCS_TM_USER_DEFINED && <Fieldset legend="User Defined">
            <div style={{ display: 'flex' }}  >
                <div style={{ marginRight: `${globalSizeFactor * 1.5 * 1}vh` }} >
                    <div style={{ marginBottom: `${globalSizeFactor * 0.5}vh`, justifyContent: 'space-between', display: "flex" }}>
                        <label>Central Meridian:</label>
                        <InputNumber value={userDefined.centralMeridian} name="centralMeridian" onValueChange={saveDataUserDefined}></InputNumber>
                    </div>
                    <div style={{ marginBottom: `${globalSizeFactor * 0.5}vh`, justifyContent: 'space-between', display: "flex" }}>
                        <label className="MarginRight">False Easting:  </label>
                        <InputNumber value={userDefined.falseEasting} name="falseEasting" onValueChange={saveDataUserDefined} ></InputNumber>
                    </div>
                    <div style={{ marginBottom: `${globalSizeFactor * 0.5}vh`, justifyContent: 'space-between', display: "flex" }}>
                        <label>False Northing: </label>
                        <InputNumber value={userDefined.falseNorthing} name="falseNorthing" onValueChange={saveDataUserDefined}></InputNumber>
                    </div>
                </div>
                <div style={{ marginRight: `${globalSizeFactor * 1.5 * 1}vh` }}>
                    <div style={{ marginBottom: `${globalSizeFactor * 0.5}vh`, justifyContent: 'space-between', display: "flex" }}>
                        <label>Latitude Of Grid Origin:</label>
                        <InputNumber value={userDefined.latitudeOfGridOrigin} name="latitudeOfGridOrigin" onValueChange={saveDataUserDefined}></InputNumber>
                    </div>
                    <div style={{ marginBottom: `${globalSizeFactor * 0.5}vh`, justifyContent: 'space-between', display: "flex" }}>
                        <label className="MarginRight">Scale Factor:  </label>
                        <InputNumber value={userDefined.scaleFactor} name="scaleFactor" onValueChange={saveDataUserDefined}></InputNumber>
                    </div>
                    <div style={{ marginBottom: `${globalSizeFactor * 0.5}vh`, justifyContent: 'space-between', display: "flex" }}>
                        <label>Zone Width:</label>
                        <InputNumber value={userDefined.zoneWidth} name="zoneWidth" onValueChange={saveDataUserDefined}></InputNumber>
                    </div>
                </div>
            </div>
        </Fieldset>}
        {gridCoordinateSystem.name == EGridCoordSystemType.EGCS_GENERIC_GRID &&
            <Fieldset legend="Generic Grid">
                <div>
                    <div style={{ display: "flex" }}>
                        <RadioButton name="SRID" checked={genericRadio == "SRID"} onChange={() => setGenericRadio("SRID")}></RadioButton>
                        <div className={genericRadio != 'SRID' ? "disabledDiv" : ""} >
                            <label>Use SRID:</label>
                            <InputText name="inputSRID" value={inputSRID} onChange={(e) => { setInputSRID(e.target.value) }}></InputText>
                            <Button onClick={getInit}>Get Init</Button>
                        </div>
                    </div>
                    <div style={{ display: "flex" }}>
                        <RadioButton name="InitString" checked={genericRadio == "InitString"} onChange={() => setGenericRadio("InitString")}></RadioButton>
                        <div className={genericRadio != 'InitString' ? "disabledDiv" : ""}>
                            <label>Use Init String:</label>
                            <InputText style={{ width: `${globalSizeFactor * 1.5 * 40}vh` }} name="inputInitString" value={inputInitString} onChange={(e) => { setInputInitString(e.target.value) }}></InputText></div>
                    </div>
                    <div style={{ display: "flex" }} >
                        {/* 4 8 10:10 */}
                        <RadioButton name="Args" checked={genericRadio == "Args"} onChange={() => setGenericRadio("Args")}></RadioButton>
                        <div className={genericRadio != 'Args' ? "disabledDiv" : ""}>
                            <label>Use Args (seperated by space):</label>
                            <InputText style={{ width: `${globalSizeFactor * 1.5 * 30}vh` }} name="inputArgs" value={inputArgs} onChange={(e) => { setInputArgs(e.target.value) }}></InputText>
                        </div>
                    </div>
                </div>
            </Fieldset>}
    </div>)
}