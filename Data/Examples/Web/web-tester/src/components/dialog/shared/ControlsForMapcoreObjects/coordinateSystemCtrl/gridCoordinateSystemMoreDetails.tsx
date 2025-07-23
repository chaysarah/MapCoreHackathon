import { useSelector } from "react-redux";
import { useEffect, useState } from "react";
import { Fieldset } from "primereact/fieldset";
import { Dialog } from "primereact/dialog";
import { InputNumber } from "primereact/inputnumber";
import { Button } from "primereact/button";
import { Checkbox } from "primereact/checkbox";
import { InputText } from "primereact/inputtext";
import { ListBox } from "primereact/listbox";

import { MapCoreData, MapCoreService, TypeToStringService } from 'mapcore-lib';
import Vector3DFromMap from "../../../treeView/objectWorldTree/shared/Vector3DFromMap";
import { TreeNodeModel } from "../../../../../services/tree.service";
import { AppState } from "../../../../../redux/combineReducer";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";
import generalService from "../../../../../services/general.service";

export default function GridCoordinateSystemMoreDetails(props: { gridCoordinateSystems: MapCore.IMcGridCoordinateSystem }) {
    let selectedNodeInTree: TreeNodeModel = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [gridCoordinateSystems, setGridCoordinateSystems] = useState<MapCore.IMcGridCoordinateSystem>(props.gridCoordinateSystems)
    let [OgcCrsCode, setOgcCrsCode] = useState<string>("__")
    let [coordinateSystemArr, setCoordinateSystemArr] = useState<any>(MapCoreData.coordinateSystemArr.map(cs => { return { coordinateSystem: cs.pCoordinateSystem, name: `${TypeToStringService.convertNumberToGridString(cs.pCoordinateSystem.GetGridCoorSysType())} (${cs.numInstancePointers} pointers)` } }))
    let [isGeneric, setGeneric] = useState(gridCoordinateSystems.GetGridCoorSysType() == MapCore.IMcGridGeneric.GRID_COOR_SYS_TYPE)
    let [legalPoint, setLegalPoint] = useState(new MapCore.SMcVector3D(0, 0, 0))

    let [FormData, setFormData] = useState({
        isOgcCrsCode: false,
        isUtm: gridCoordinateSystems.IsUtm(),
        isGeographical: gridCoordinateSystems.IsGeographic(),
        isMultyZone: gridCoordinateSystems.IsMultyZoneGrid(),
        geographicLegal: gridCoordinateSystems.GetLegalValuesForGeographicCoordinates(),
        gridCoordinatesLegal: gridCoordinateSystems.GetLegalValuesForGridCoordinates(),
        isLocationLegal: false,
        IsGeographicLocation: false,
        DefaultZone: null,
        datumParams: gridCoordinateSystems.GetDatumParams(),
        addToCSList: false,
        selectedCorSys: [],
        isEqual: false
    })

    useEffect(() => {
        runCodeSafely(() => {
            const root = document.documentElement;
            let pixelWidth = window.innerHeight * 1 * globalSizeFactor;
            root.style.setProperty('--more-details-dialog-width', `${pixelWidth}px`);

            let OgcCrsCode_: { Value?: any } = {};
            let isOgcCrsCode
            runMapCoreSafely(() => {
                isOgcCrsCode = gridCoordinateSystems.GetOgcCrsCode(OgcCrsCode_)
            }, "MoreDetails.useEffect=> GetOgcCrsCode", true)
            setFormData({ ...FormData, isOgcCrsCode: isOgcCrsCode })
            if (OgcCrsCode_) {
                let o = OgcCrsCode_.Value
                setOgcCrsCode(o)
            }
        }, "MoreDetails.useEffect")
    }, [])

    const splitEnumStr = (str: string) => {
        let subString;
        runCodeSafely(() => {
            let lastUnderscoreIndex = str.lastIndexOf("_");
            if (lastUnderscoreIndex !== -1) {
                let secondLastUnderscoreIndex = str.lastIndexOf("_", lastUnderscoreIndex - 1);
                if (secondLastUnderscoreIndex !== -1) {
                    subString = str.substring(secondLastUnderscoreIndex + 1);

                }
            }
        }, "MoreDetails.splitEnumStr")
        return subString;
    }

    const saveFormData = (event: any) => {
        setFormData({ ...FormData, [event.target.name]: event.target.type === "checkbox" ? event.target.checked : event.target.value });
    }
    const showIsEqual = (event: any) => {
        runCodeSafely(() => {
            let corSys = event.target.value.coordinateSystem;
            let isEqual
            runMapCoreSafely(() => {
                isEqual = gridCoordinateSystems.IsEqual(corSys)
            }, "MoreDetails.showIsEqual=> IsEqual", true)
            setFormData({ ...FormData, isEqual: isEqual });
        }, "MoreDetails.showIsEqual")
    }
    const setGridCoordinates = () => {
        runCodeSafely(() => {
            runMapCoreSafely(() => {
                gridCoordinateSystems.SetLegalValuesForGridCoordinates(FormData.gridCoordinatesLegal)
            }, "MoreDetails.setGridCoordinates=> SetLegalValuesForGridCoordinates", true)
        }, "MoreDetails.setGridCoordinates")
    }
    const setGeographicLegal = () => {
        runCodeSafely(() => {
            runMapCoreSafely(() => {
                gridCoordinateSystems.SetLegalValuesForGeographicCoordinates(FormData.geographicLegal)
            }, "MoreDetails.setGeographicLegal=> SetLegalValuesForGeographicCoordinates", true)
        }, "MoreDetails.setGeographicLegal")

    }
    const saveFormDataLegal = (event: MapCore.SMcVector3D, vertex: string, legal: string) => {
        // let num = event.target.value;
        // let Vertex = { ...FormData[legal][vertex], [event.target.name]: num }
        let Legal = { ...FormData[legal], [vertex]: event }
        setFormData({ ...FormData, [legal]: Legal })
    }
    const isLocationLegal = () => {
        runCodeSafely(() => {
            runMapCoreSafely(() => {
                let IsLocationLegal = gridCoordinateSystems.IsLocationLegal(new MapCore.SMcVector3D(legalPoint.x, legalPoint.y, legalPoint.z))
                setFormData({ ...FormData, isLocationLegal: IsLocationLegal })
            }, "MoreDetails.isLocationLegal=> isLocationLegal", true)
        }, "MoreDetails.isLocationLegal")
    }
    const IsGeographicLocation = () => {
        runCodeSafely(() => {
            runMapCoreSafely(() => {
                let IsGeographicLocationLegal = gridCoordinateSystems.IsGeographicLocationLegal(new MapCore.SMcVector3D(legalPoint.x, legalPoint.y, legalPoint.z))
                setFormData({ ...FormData, IsGeographicLocation: IsGeographicLocationLegal })
            }, "MoreDetails.IsGeographicLocation=> IsGeographicLocationLegal", true)
        }, "MoreDetails.IsGeographicLocation")
    }
    const GetDefaultZone = () => {
        runCodeSafely(() => {
            runMapCoreSafely(() => {
                let zone = gridCoordinateSystems.GetDefaultZoneFromGeographicLocation(new MapCore.SMcVector3D(legalPoint.x, legalPoint.y, legalPoint.z))
                setFormData({ ...FormData, DefaultZone: zone })
            }, "MoreDetails.GetDefaultZone=> GetDefaultZoneFromGeographicLocation", true)
        }, "MoreDetails.GetDefaultZone")
    }
    let [newMoreDetailsCS, setNewMoreDetailsCS] = useState(false)
    let [newGridCoordinateSystems, setNewGridCoordinateSystems] = useState(null)

    const asGeneric = () => {
        runCodeSafely(() => {
            let gridGeneric: MapCore.IMcGridGeneric
            runMapCoreSafely(() => { gridGeneric = gridCoordinateSystems.CloneAsGeneric(); }, "MoreDetails.asGeneric=> CloneAsGeneric", true)
            if (gridGeneric != null) {
                setNewGridCoordinateSystems(gridGeneric)
                setNewMoreDetailsCS(true)
                if (FormData.addToCSList) {
                    MapCoreService.addCoordinateSystemToList(gridGeneric)
                }
            }
            else
                throw new Error("This grid coordinate system could not be cloned as generic")
        }, "MoreDetails.asGeneric")
    }
    const asNonGeneric = () => {
        runCodeSafely(() => {
            let nonGridGeneric: MapCore.IMcGridCoordinateSystem = null;
            if (isGeneric) {
                runMapCoreSafely(() => { nonGridGeneric = (gridCoordinateSystems as MapCore.IMcGridGeneric).CloneAsNonGeneric(); }, "MoreDetails.asNonGeneric=> CloneAsNonGeneric", true)
                if (nonGridGeneric != null && nonGridGeneric != undefined) {
                    setNewGridCoordinateSystems(nonGridGeneric)
                    setNewMoreDetailsCS(true)
                    if (FormData.addToCSList) {
                        MapCoreService.addCoordinateSystemToList(nonGridGeneric)
                    }
                }
                else {
                    throw new Error("This grid coordinate system could not be cloned as non generic")
                }
            }
        }, "MoreDetails.asNonGeneric")
    }

    return (<>
        <div style={{ display: 'flex', justifyContent: 'space-between' }}>
            <div >
                <div ><label >Type:  {TypeToStringService.convertNumberToGridString(gridCoordinateSystems.GetGridCoorSysType())}</label> </div>
                <div ><label >datum:  {splitEnumStr(gridCoordinateSystems.GetDatum().constructor.name)}</label>  </div>
                <div ><label >zone:  {gridCoordinateSystems.GetZone().toString()}</label>  </div>

                <Fieldset legend="Datum Params">
                    <div style={{ display: 'flex' }} className="disabledDiv" >
                        <div style={{ marginRight: `${globalSizeFactor * 1.5 * 1}vh` }} >
                            <div style={{ marginBottom: `${globalSizeFactor * 0.5}vh`, justifyContent: 'space-between', display: "flex" }}>
                                <label>A</label>
                                <InputNumber value={FormData.datumParams.dA}></InputNumber>
                            </div>
                            <div style={{ marginBottom: `${globalSizeFactor * 0.5}vh`, justifyContent: 'space-between', display: "flex" }}>
                                <label className="MarginRight">DX  </label>
                                <InputNumber value={FormData.datumParams.dDX}></InputNumber>
                            </div>
                            <div style={{ marginBottom: `${globalSizeFactor * 0.5}vh`, justifyContent: 'space-between', display: "flex" }}>
                                <label>RX  </label>
                                <InputNumber value={FormData.datumParams.dRx}></InputNumber>
                            </div>
                        </div>
                        <div style={{ marginRight: `${globalSizeFactor * 1.5 * 1}vh` }}>
                            <div style={{ marginBottom: `${globalSizeFactor * 0.5}vh`, justifyContent: 'space-between', display: "flex" }}>
                                <label>F</label>
                                <InputNumber value={FormData.datumParams.dF}></InputNumber>
                            </div>
                            <div style={{ marginBottom: `${globalSizeFactor * 0.5}vh`, justifyContent: 'space-between', display: "flex" }}>
                                <label className="overlay-manager__margin-right">DY  </label>
                                <InputNumber value={FormData.datumParams.dDY}></InputNumber>
                            </div>
                            <div style={{ marginBottom: `${globalSizeFactor * 0.5}vh`, justifyContent: 'space-between', display: "flex" }}>
                                <label>RY  </label>
                                <InputNumber value={FormData.datumParams.dRy}></InputNumber>
                            </div>
                        </div>
                        <div>
                            <div style={{ marginBottom: `${globalSizeFactor * 0.5}vh`, justifyContent: 'space-between', display: "flex" }}>
                                <label >S</label>
                                <InputNumber value={FormData.datumParams.dS}></InputNumber>
                            </div>
                            <div style={{ marginBottom: `${globalSizeFactor * 0.5}vh`, justifyContent: 'space-between', display: "flex" }}>
                                <label className="overlay-manager__margin-right">DZ  </label>
                                <InputNumber value={FormData.datumParams.dDZ}></InputNumber>
                            </div>
                            <div style={{ marginBottom: `${globalSizeFactor * 0.5}vh`, justifyContent: 'space-between', display: "flex" }}>
                                <label>RX  </label>
                                <InputNumber value={FormData.datumParams.dRx}></InputNumber>
                            </div>
                        </div>
                    </div>
                </Fieldset>
            </div>
            <Fieldset legend="Clone" className="form__column-fieldset" >
                <div style={{ display: "flex" }} >
                    <Checkbox name="addToCSList" className="overlay-manager__margin-right" checked={FormData.addToCSList} onChange={saveFormData} ></Checkbox>
                    <label>Add To Grid Coordinate System List</label>
                </div>
                {!isGeneric ? <Button label="As Generic" onClick={asGeneric}></Button>
                    : <Button style={{ marginTop: "2%" }} label="As Non Generic" onClick={asNonGeneric}></Button>}
            </Fieldset>
        </div>
        <Fieldset className="form__column-fieldset form__align-items-end" legend="Grid Coordinate System" >
            <div style={{ display: "flex", width: '100%' }} className="disabledDiv">
                <Checkbox className="overlay-manager__margin-right" checked={FormData.isMultyZone} ></Checkbox>
                <label style={{ marginRight: '3%' }}>Is multy zone grid</label>
                <Checkbox className="overlay-manager__margin-right" checked={FormData.isGeographical} ></Checkbox>
                <label style={{ marginRight: '3%' }}>Is geographical</label>
                <Checkbox className="overlay-manager__margin-right" checked={FormData.isUtm} ></Checkbox>
                <label style={{ marginRight: '3%' }}>Is utm</label>
            </div>
            <div style={{ display: "flex" }}>
                <div>
                    <Fieldset className="form__row-fieldset" legend="Ogc Crs Code" >
                        <Checkbox className="overlay-manager__margin-right" checked={FormData.isOgcCrsCode}></Checkbox>
                        <label style={{ marginRight: "3%" }}>Found: </label>
                        <InputText className="disabledDiv" style={{ width: "80%" }} value={OgcCrsCode} ></InputText>
                    </Fieldset>
                    <Fieldset className="form__column-fieldset" legend="Legal Values For Geographic Coordinates">
                        <div style={{ display: 'flex', flexDirection: 'column' }}>
                            <Vector3DFromMap pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={FormData.geographicLegal.MinVertex} name="Min: " flagNull={{ x: false, y: false, z: false }} saveTheValue={(e: MapCore.SMcVector3D) => { saveFormDataLegal(e, "MinVertex", "geographicLegal") }}></Vector3DFromMap>
                            <Vector3DFromMap pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={FormData.geographicLegal.MaxVertex} name="Max: " flagNull={{ x: false, y: false, z: false }} saveTheValue={(e: MapCore.SMcVector3D) => { saveFormDataLegal(e, "MaxVertex", "geographicLegal") }}></Vector3DFromMap>
                            <Button label="Set" onClick={setGeographicLegal} style={{ marginLeft: '2%' }}></Button>
                        </div>
                    </Fieldset>
                    <Fieldset legend="Legal Values For Grid Coordinates">
                        <div style={{ display: 'flex', flexDirection: 'column' }}>
                            <Vector3DFromMap pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={FormData.gridCoordinatesLegal.MinVertex} name="Min: " flagNull={{ x: false, y: false, z: false }} saveTheValue={(e: MapCore.SMcVector3D) => { saveFormDataLegal(e, "MinVertex", "gridCoordinatesLegal") }}></Vector3DFromMap>
                            <Vector3DFromMap pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={FormData.gridCoordinatesLegal.MaxVertex} name="Max: " flagNull={{ x: false, y: false, z: false }} saveTheValue={(e: MapCore.SMcVector3D) => { saveFormDataLegal(e, "MaxVertex", "gridCoordinatesLegal") }}></Vector3DFromMap>
                            <Button label="Set" onClick={setGeographicLegal} style={{ marginLeft: '2%' }}></Button>
                        </div>
                    </Fieldset>
                </div>
                <div >
                    <Fieldset className="form__row-fieldset" legend="Is Equal">
                        <ListBox style={{ margin: `${globalSizeFactor * 1.5}vh`, width: '70%' }} options={coordinateSystemArr} optionLabel="name" value={FormData.selectedCorSys} onChange={showIsEqual} ></ListBox>
                        <div style={{ display: 'flex', flexDirection: 'row' }}>
                            <Checkbox checked={FormData.isEqual} className="overlay-manager__margin-right disabledDiv"></Checkbox>
                            <div>Is equal</div>
                        </div>
                    </Fieldset>
                    <Fieldset style={{ marginTop: `${globalSizeFactor * 1.5}vh` }}>
                        <div>
                            <Vector3DFromMap pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} name="Location" initValue={legalPoint} flagNull={{ x: false, y: false, z: false }} saveTheValue={(e: MapCore.SMcVector3D) => { setLegalPoint(e) }}></Vector3DFromMap>
                            <div>
                                <div style={{ marginTop: '2%', display: 'flex', justifyContent: 'space-between' }}><Button label="Is Location Legal" onClick={isLocationLegal}></Button>
                                    <div > <Checkbox className="disabledDiv overlay-manager__margin-right" name="isLocationLegal" checked={FormData.isLocationLegal}></Checkbox> <label>Is Legal</label></div></div>
                                <div style={{ marginTop: '2%', display: 'flex', justifyContent: 'space-between' }}><Button label="Is Geographic Location Legal" onClick={IsGeographicLocation}></Button>
                                    <div><Checkbox className="disabledDiv overlay-manager__margin-right" name="IsGeographicLocation" checked={FormData.IsGeographicLocation}></Checkbox> <label>Is Legal</label></div></div>
                                <div style={{ marginTop: '2%', display: 'flex', justifyContent: 'space-between' }}>
                                    <Button label="Get Default Zone From Geographic Location" onClick={GetDefaultZone}></Button>
                                    <div style={{ marginLeft: '1%', whiteSpace: 'nowrap' }} >
                                        <label>Zone:</label>
                                        <InputNumber className="disabledDiv" name="DefaultZone" value={FormData.DefaultZone}></InputNumber>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </Fieldset>
                </div>
            </div>
        </Fieldset>
        <Dialog
            className="overlay-manager__scroll-dialog-more-details"
            onHide={() => { setNewMoreDetailsCS(false) }}
            visible={newMoreDetailsCS}
            header="Grid Coordinate System">
            <GridCoordinateSystemMoreDetails gridCoordinateSystems={newGridCoordinateSystems}></GridCoordinateSystemMoreDetails>
        </Dialog>
    </>)
}
