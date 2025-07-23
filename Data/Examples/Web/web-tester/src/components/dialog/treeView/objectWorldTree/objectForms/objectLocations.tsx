// External libraries
import { useState, useEffect, useRef, ChangeEvent } from 'react';
import { useSelector } from 'react-redux';
import _ from 'lodash';

// UI/component libraries
import { Fieldset } from "primereact/fieldset";
import { InputNumber } from "primereact/inputnumber";
import { ListBox } from "primereact/listbox";
import { Button } from "primereact/button";
import { Checkbox } from "primereact/checkbox";
import { Dropdown } from "primereact/dropdown";

// Project-specific imports
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import { ObjectFormTabInfo } from "./objectForm";
import Vector3DFromMap from "../shared/Vector3DFromMap";
import Vector3DGrid from "../shared/vector3DGrid";
import { Properties } from "../../../dialog";
import TreeNodeModel from "../../../../shared/models/tree-node.model";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";
import { AppState } from "../../../../../redux/combineReducer";
import objectWorldTreeService from "../../../../../services/objectWorldTree.service";
import generalService from "../../../../../services/general.service";
import dialogStateService from "../../../../../services/dialogStateService";

export class ObjectLocationsPropertiesState implements Properties {
    offsetX: number | null;
    offsetY: number | null;
    nodeID: number | null;
    numberOfLocationPoints: number | null;
    locationPointsTable: MapCore.SMcVector3D[];

    static getDefault(props: any): ObjectLocationsPropertiesState {
        return {
            offsetX: 0,
            offsetY: 0,
            nodeID: null,
            numberOfLocationPoints: null,
            locationPointsTable: [...props.props.tabInfo.currentObject.nodeMcContent.GetLocationPoints(0)],
        }
    }
}

export class ObjectLocationsProperties extends ObjectLocationsPropertiesState {
    locatonPointOperationPoint: MapCore.SMcVector3D;
    allViewports: any[];
    selectedPointsIndexes: number[];
    locationIndexesArr: any[];
    viewportToSave: any;
    locationIndex: { index: number, label: string };
    cs: any;
    isRelativeToDTM: boolean;

    static getDefault(props: any): ObjectLocationsProperties {
        let EMcPointCoordSystem = getEnumDetailsList(MapCore.EMcPointCoordSystem);
        let mcCurrentObject = props.props.tabInfo.currentObject.nodeMcContent as MapCore.IMcObject;
        let mcObjectLocation: MapCore.IMcObjectLocation
        runMapCoreSafely(() => {
            mcObjectLocation = props.props.tabInfo.properties.objectLocationsProperties?.locationIndex?.index ?
                mcCurrentObject.GetScheme().GetObjectLocation(props.props.tabInfo.properties.objectLocationsProperties?.locationIndex?.index) :
                mcCurrentObject.GetScheme().GetObjectLocation();
        }, 'ObjectForms/ObjectLocations.useEffect => IMcObjectScheme.GetObjectLocation', true)
        let cs: MapCore.EMcPointCoordSystem = mcObjectLocation.GetCoordSystem();
        let finalCS = getEnumValueDetails(cs, EMcPointCoordSystem);

        let stateDefaults = super.getDefault(props);
        let defaults: ObjectLocationsProperties = {
            ...stateDefaults,
            locatonPointOperationPoint: new MapCore.SMcVector3D(0, 0, 0),
            allViewports: getViewports(props.props, props.treeRedux),
            selectedPointsIndexes: [],
            locationIndexesArr: getLocationIndexArr(props.props),
            viewportToSave: null,
            locationIndex: { index: 0, label: `${0}` },
            cs: finalCS,
            isRelativeToDTM: false,
        }

        return defaults
    }
};

const getLocationIndexArr = (props: any) => {
    let mcCurrentObject = props.tabInfo.currentObject.nodeMcContent as MapCore.IMcObject;
    let numLocations = mcCurrentObject.GetNumLocations();
    let arr: { index: number, label: string }[] = [];
    for (let index = 0; index < numLocations; index++) {
        arr = [...arr, { index: index, label: `${index}` }];
    }
    return arr;
}
const getViewports = (props: any, treeRedux: TreeNodeModel) => {
    let overlay = objectWorldTreeService.getParentByChildKey(treeRedux, props.tabInfo.currentObject.key) as TreeNodeModel;
    let mcVps = Array.from(objectWorldTreeService.getOMMCViewportsByOverlay(treeRedux, overlay))
    let vpList = mcVps.map(vp => { return { viewport: vp.viewport, label: generalService.getObjectName(vp, "Viewport") } });
    return vpList;
}

export default function ObjectLocations(props: { tabInfo: ObjectFormTabInfo }) {
    let treeRedux = useSelector((state: AppState) => state.objectWorldTreeReducer.objectWorldTree);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [isMountedUseEffect, setIsMountedUseEffect] = useState({
        currentObject: false,
        locationIndex: false,
    })
    const isMountedCurrentObject = useRef(false)
    const isMountedLocationIndex = useRef(false)

    const [enumDetails] = useState({
        EMcPointCoordSystem: getEnumDetailsList(MapCore.EMcPointCoordSystem),
    });

    useEffect(() => {
        // initialize first time the tab is loaded only
        if (!props.tabInfo.properties.objectLocationsProperties) {
            props.tabInfo.setInitialStatePropertiesCallback("objectLocationsProperties", null, ObjectLocationsPropertiesState.getDefault({ props, treeRedux }));
            props.tabInfo.setPropertiesCallback("objectLocationsProperties", null, ObjectLocationsProperties.getDefault({ props, treeRedux }));
        }
    }, [])

    const saveData = (event: any) => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback("objectLocationsProperties", event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
            if (event.target.name in ObjectLocationsPropertiesState.getDefault({ props, treeRedux })) {
                props.tabInfo.setCurrStatePropertiesCallback("objectLocationsProperties", event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value)
            }
        }, "ObjectForms/ObjectLocations.saveData => onChange")
    }
    useEffect(() => {
        if (isMountedUseEffect.currentObject) {
            props.tabInfo.setInitialStatePropertiesCallback("objectLocationsProperties", null, ObjectLocationsPropertiesState.getDefault({ props, treeRedux }));
            props.tabInfo.setPropertiesCallback("objectLocationsProperties", null, ObjectLocationsProperties.getDefault({ props, treeRedux }));
        }
        else {
            setIsMountedUseEffect({ ...isMountedUseEffect, currentObject: true })
        }
    }, [props.tabInfo.currentObject])

    useEffect(() => {
        runCodeSafely(() => {
            let mcCurrentObject = props.tabInfo.currentObject.nodeMcContent as MapCore.IMcObject;
            let mcObjectLocation: MapCore.IMcObjectLocation
            runMapCoreSafely(() => {
                mcObjectLocation = props.tabInfo.properties.objectLocationsProperties?.locationIndex?.index ?
                    mcCurrentObject.GetScheme().GetObjectLocation(props.tabInfo.properties.objectLocationsProperties?.locationIndex?.index) :
                    mcCurrentObject.GetScheme().GetObjectLocation();
            }, 'ObjectForms/ObjectLocations.useEffect => IMcObjectScheme.GetObjectLocation', true)
            if (mcObjectLocation) {
                if (isMountedUseEffect.locationIndex) {
                    let propId: { Value?: number } = {}
                    let bool = mcObjectLocation.GetRelativeToDTM(propId);
                    let finalCheckboxVal;
                    if (propId.Value == MapCore.UINT_MAX) {
                        finalCheckboxVal = bool;
                    }
                    else {
                        finalCheckboxVal = mcCurrentObject.GetBoolProperty(propId.Value);
                    }
                    let cs: MapCore.EMcPointCoordSystem = mcObjectLocation.GetCoordSystem();
                    let finalCS = getEnumValueDetails(cs, enumDetails.EMcPointCoordSystem);
                    props.tabInfo.setPropertiesCallback("objectLocationsProperties", "cs", finalCS);
                    props.tabInfo.setPropertiesCallback("objectLocationsProperties", "isRelativeToDTM", finalCheckboxVal);
                    props.tabInfo.setPropertiesCallback("objectLocationsProperties", "locationPointsTable", [...props.tabInfo.currentObject.nodeMcContent.GetLocationPoints(props.tabInfo.properties.objectLocationsProperties?.locationIndex.index)]);
                    props.tabInfo.setCurrStatePropertiesCallback("objectLocationsProperties", "locationPointsTable", [...props.tabInfo.currentObject.nodeMcContent.GetLocationPoints(props.tabInfo.properties.objectLocationsProperties?.locationIndex.index)]);

                }
                else {
                    setIsMountedUseEffect({ ...isMountedUseEffect, locationIndex: true })
                }
            }
        }, 'ObjectForms/ObjectLocations => useEffect.bjectLocationFormData.locationIndex')
    }, [props.tabInfo.properties.objectLocationsProperties?.locationIndex])

    const saveLocationPointOperationPoint = (point: MapCore.SMcVector3D, flagNull: boolean) => {
        props.tabInfo.setPropertiesCallback("objectLocationsProperties", "locatonPointOperationPoint", point);
    }
    const saveLocationPointsTable = (locationPointsList: any, validPoints: any, selectionArr?: any) => {
        if (!_.isEqual(props.tabInfo.properties.objectLocationsProperties?.locationPointsTable, locationPointsList) ||
            !_.isEqual(props.tabInfo.properties.objectLocationsProperties?.selectedPointsIndexes, selectionArr)) {

            let finalTable = locationPointsList;
            if (props.tabInfo.properties.objectLocationsProperties?.isRelativeToDTM) {
                finalTable = locationPointsList.map(point => ({ ...point, z: 0 }));
            }
            props.tabInfo.setPropertiesCallback("objectLocationsProperties", "locationPointsTable", finalTable);
            props.tabInfo.setPropertiesCallback("objectLocationsProperties", "selectedPointsIndexes", selectionArr);
        }
    }

    const isSelectedPointsConsecutive = () => {
        let arr = _.sortBy(props.tabInfo.properties.objectLocationsProperties?.selectedPointsIndexes);
        let bool = arr.every((item, i) => i === 0 || item === arr[i - 1] + 1);
        return bool && arr.length > 1;
    }
    //Handle Funcs
    const handleSetNumLocationPoints = () => {
        runCodeSafely(() => {
            let mcCurrentObject = props.tabInfo.currentObject.nodeMcContent as MapCore.IMcObject;
            runMapCoreSafely(() => {
                props.tabInfo.properties.objectLocationsProperties?.locationIndex ?
                    mcCurrentObject.SetNumLocationPoints(props.tabInfo.properties.objectLocationsProperties?.numberOfLocationPoints, props.tabInfo.properties.objectLocationsProperties?.locationIndex.index) :
                    mcCurrentObject.SetNumLocationPoints(props.tabInfo.properties.objectLocationsProperties?.numberOfLocationPoints);
            }, 'ObjectForms/ObjectLocations => IMcObject.SetNumLocationPoints', true)
            props.tabInfo.setPropertiesCallback("objectLocationsProperties", "locationPointsTable", [...props.tabInfo.currentObject.nodeMcContent.GetLocationPoints(props.tabInfo.properties.objectLocationsProperties?.locationIndex.index)]);
            props.tabInfo.setCurrStatePropertiesCallback("objectLocationsProperties", "locationPointsTable", [...props.tabInfo.currentObject.nodeMcContent.GetLocationPoints(props.tabInfo.properties.objectLocationsProperties?.locationIndex.index)]);
            dialogStateService.applyDialogState(["objectLocationsProperties.numberOfLocationPoints",
                "objectLocationsProperties.locationPointsTable"]);
        }, 'ObjectForms/ObjectLocations.handleSetNumLocationPoints => onClick')
    }
    const handleSetLocationPointsTable = () => {
        runCodeSafely(() => {
            runMapCoreSafely(() => {
                let mcCurrentObject = props.tabInfo.currentObject.nodeMcContent as MapCore.IMcObject;
                mcCurrentObject.SetLocationPoints(props.tabInfo.properties.objectLocationsProperties?.locationPointsTable);
                let x = mcCurrentObject.GetLocationPoints(props.tabInfo.properties.objectLocationsProperties?.locationIndex.index);
                console.log(x)
            }, 'ObjectForms/ObjectLocations => IMcObject.SetLocationPoints', true)

            dialogStateService.applyDialogState(["objectLocationsProperties.locationIndex",
                "objectLocationsProperties.locationPointsTable"]);
        }, 'ObjectForms/ObjectLocations.handleSetLocationPointsTable => onClick')
    }
    const handleGetLocationIndexByID = () => {
        runCodeSafely(() => {
            let mcCurrentObject = props.tabInfo.currentObject.nodeMcContent as MapCore.IMcObject;
            let locationIndex: number;
            runMapCoreSafely(() => {
                locationIndex = mcCurrentObject.GetLocationIndexByID(props.tabInfo.properties.objectLocationsProperties?.nodeID);
            }, 'ObjectForms/ObjectLocations.handleGetLocationIndexByID => IMcObject.GetLocationIndexByID', true)
            props.tabInfo.setPropertiesCallback("objectLocationsProperties", "locationIndex", { index: locationIndex, label: `${locationIndex}` });
            // dialogStateService.applyDialogState(["objectLocationsProperties.locationIndex",
            //     "objectLocationsProperties.nodeID"]);
        }, 'ObjectForms/ObjectLocations.handleGetLocationIndexByID => onClick')
    }
    const handleCsvFileSelected = (event: ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files[0];
        const reader = new FileReader();
        reader.onload = (event) => {
            const csvData = event.target.result as string;
            console.log(csvData)
            const data = csvData.split('\n').map((row: string) => row.split(','));
            let newLocationPointsList: { x: number, y: number, z: number }[] = [];
            data.forEach((arr) => {
                if (arr.length >= 2) {
                    let obj = { x: parseInt(arr[0]), y: parseInt(arr[1]), z: parseInt(arr[2]) } as MapCore.SMcVector3D
                    newLocationPointsList = [...newLocationPointsList, obj]
                }
            })
            newLocationPointsList = [...newLocationPointsList]
            props.tabInfo.setPropertiesCallback("objectLocationsProperties", "locationPointsTable", newLocationPointsList);
            props.tabInfo.setCurrStatePropertiesCallback("objectLocationsProperties", "locationPointsTable", newLocationPointsList);
            // apply ?
        };
        reader.readAsText(file);
    }
    const handleExportCsvFileClick = () => {
        let pointsToExport = props.tabInfo.properties.objectLocationsProperties?.locationPointsTable.filter(p => p.x && p.y)
        const csvData = 'Z,Y,X' + "\n" + pointsToExport.map((point) => `${point.z},${point.y},${point.x}`).join("\n");
        const downloadLink = document.createElement("a");
        downloadLink.href = "data:text/csv;charset=utf-8," + encodeURIComponent(csvData);
        downloadLink.download = "objectLocationPoints.csv";
        downloadLink.click();
    }
    const handleUpdateConsecutiveSelectedLocationPoints = () => {
        let mcCurrentObject = props.tabInfo.currentObject.nodeMcContent as MapCore.IMcObject;
        let selectedPoints: MapCore.SMcFVector3D[] = props.tabInfo.properties.objectLocationsProperties?.locationPointsTable.filter((point, ind) => {
            props.tabInfo.properties.objectLocationsProperties?.selectedPointsIndexes.includes(ind)
        });
        mcCurrentObject.UpdateLocationPoints(selectedPoints, props.tabInfo.properties.objectLocationsProperties?.selectedPointsIndexes[0], props.tabInfo.properties.objectLocationsProperties?.locationIndex.index);
    }
    const handleAddPointBeforeSelectedRow = () => {
        let mcCurrentObject = props.tabInfo.currentObject.nodeMcContent as MapCore.IMcObject;
        props.tabInfo.properties.objectLocationsProperties?.locationIndex ?
            mcCurrentObject.AddLocationPoint(props.tabInfo.properties.objectLocationsProperties?.selectedPointsIndexes[0],
                props.tabInfo.properties.objectLocationsProperties?.locatonPointOperationPoint,
                props.tabInfo.properties.objectLocationsProperties?.locationIndex.index) :
            mcCurrentObject.AddLocationPoint(props.tabInfo.properties.objectLocationsProperties?.selectedPointsIndexes[0],
                props.tabInfo.properties.objectLocationsProperties?.locatonPointOperationPoint);

        props.tabInfo.setPropertiesCallback("objectLocationsProperties", "locationPointsTable", [...props.tabInfo.currentObject.nodeMcContent.GetLocationPoints(props.tabInfo.properties.objectLocationsProperties?.locationIndex.index)]);
        props.tabInfo.setCurrStatePropertiesCallback("objectLocationsProperties", "locationPointsTable", [...props.tabInfo.currentObject.nodeMcContent.GetLocationPoints(props.tabInfo.properties.objectLocationsProperties?.locationIndex.index)]);
        dialogStateService.applyDialogState(["objectLocationsProperties.locationPointsTable"]);
    }
    const handleUpdateLocationPointByAnotherPoint = () => {
        let mcCurrentObject = props.tabInfo.currentObject.nodeMcContent as MapCore.IMcObject;
        props.tabInfo.properties.objectLocationsProperties?.locationIndex ?
            mcCurrentObject.UpdateLocationPoint(props.tabInfo.properties.objectLocationsProperties?.selectedPointsIndexes[0],
                props.tabInfo.properties.objectLocationsProperties?.locatonPointOperationPoint,
                props.tabInfo.properties.objectLocationsProperties?.locationIndex.index)
            : mcCurrentObject.UpdateLocationPoint(props.tabInfo.properties.objectLocationsProperties?.selectedPointsIndexes[0],
                props.tabInfo.properties.objectLocationsProperties?.locatonPointOperationPoint);
        props.tabInfo.setPropertiesCallback("objectLocationsProperties", "locationPointsTable", [...props.tabInfo.currentObject.nodeMcContent.GetLocationPoints(props.tabInfo.properties.objectLocationsProperties?.locationIndex.index)]);
        dialogStateService.applyDialogState(["objectLocationsProperties.locationPointsTable"]);
    }
    const handleMoveAllLocationsPointsByPoint = () => {
        let mcCurrentObject = props.tabInfo.currentObject.nodeMcContent as MapCore.IMcObject;
        mcCurrentObject.MoveAllLocationsPoints(props.tabInfo.properties.objectLocationsProperties?.locatonPointOperationPoint);
        props.tabInfo.setPropertiesCallback("objectLocationsProperties", "locationPointsTable", [...props.tabInfo.currentObject.nodeMcContent.GetLocationPoints(props.tabInfo.properties.objectLocationsProperties?.locationIndex.index)]);
        props.tabInfo.setCurrStatePropertiesCallback("objectLocationsProperties", "locationPointsTable", [...props.tabInfo.currentObject.nodeMcContent.GetLocationPoints(props.tabInfo.properties.objectLocationsProperties?.locationIndex.index)]);
        dialogStateService.applyDialogState(["objectLocationsProperties.locationPointsTable"]);
    }
    const handleClearLocationsClick = () => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback("objectLocationsProperties", "locationPointsTable", [{ x: null, y: null, z: null }]);
        }, "ObjectForms/ObjectLocations.handleClearLocationsClick => onChange")
    }
    const handleGetOffsetClick = (event: any) => {
        runCodeSafely(() => {
            let mcCurrentObject = props.tabInfo.currentObject.nodeMcContent as MapCore.IMcObject;
            let vector2D: MapCore.SMcFVector2D;
            if (event.value) {
                runMapCoreSafely(() => {
                    vector2D = mcCurrentObject.GetScreenArrangementOffset(event.value.viewport);
                }, 'ObjectForms/ObjectLocations.handleGetOffsetClick => IMcObject.GetScreenArrangementOffset', true)
                props.tabInfo.setPropertiesCallback("objectLocationsProperties", "offsetX", vector2D.x);
                props.tabInfo.setPropertiesCallback("objectLocationsProperties", "offsetY", vector2D.y);
                props.tabInfo.setPropertiesCallback("objectLocationsProperties", "viewportToSave", event.value);
            }
            else {
                props.tabInfo.setPropertiesCallback("objectLocationsProperties", "offsetX", 0);
                props.tabInfo.setPropertiesCallback("objectLocationsProperties", "offsetY", 0);
                props.tabInfo.setPropertiesCallback("objectLocationsProperties", "viewportToSave", event.value);
            }
        }, 'ObjectForms/ObjectLocations => handleGetOffsetClick')
    }
    const handleSetOffsetClick = () => {
        runCodeSafely(() => {
            let mcCurrentObject = props.tabInfo.currentObject.nodeMcContent as MapCore.IMcObject;
            let vector2D: MapCore.SMcFVector2D = { x: props.tabInfo.properties.objectLocationsProperties?.offsetX, y: props.tabInfo.properties.objectLocationsProperties?.offsetY }
            runMapCoreSafely(() => {
                if (props.tabInfo.properties.objectLocationsProperties?.viewportToSave) {
                    mcCurrentObject.SetScreenArrangementOffset(props.tabInfo.properties.objectLocationsProperties?.viewportToSave.viewport, vector2D);
                }
                else {
                    mcCurrentObject.SetScreenArrangementOffset(null, vector2D);
                }
            }, 'ObjectForms/ObjectLocations.handleGetOffsetClick => IMcObject.GetScreenArrangementOffset', true)
            dialogStateService.applyDialogState(["objectLocationsProperties.offsetX", "objectLocationsProperties.offsetY"]);
        }, 'ObjectForms/ObjectLocations => handleSetOffsetClick')
    }
    const handleRefreshPointsClick = () => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback("objectLocationsProperties", "locationPointsTable", [...props.tabInfo.currentObject.nodeMcContent.GetLocationPoints(props.tabInfo.properties.objectLocationsProperties?.locationIndex.index)]);
            props.tabInfo.setCurrStatePropertiesCallback("objectLocationsProperties", "locationPointsTable", [...props.tabInfo.currentObject.nodeMcContent.GetLocationPoints(props.tabInfo.properties.objectLocationsProperties?.locationIndex.index)]);
            dialogStateService.applyDialogState(["objectLocationsProperties.locationPointsTable"]);
        }, 'ObjectForms/ObjectLocations => handleRefreshPointsClick')
    }

    //DOM Functions
    const getScreenFieldset = () => {
        return <Fieldset className="form__row-fieldset" legend="Screen Arrangement Offset">
            <div style={{ display: 'flex', flexDirection: 'column', width: '100%' }}>
                <div style={{ display: 'flex', flexDirection: 'column' }}>
                    <div style={{ display: 'flex', flexDirection: 'column', padding: `${globalSizeFactor * 0.75}vh` }}>
                        <label>Offset :</label>
                        <div style={{ display: 'flex', flexDirection: 'row' }}>
                            <div>
                                <label htmlFor='offsetX'>X </label>
                                <InputNumber className="form__medium-width-input" id='offsetX' value={props.tabInfo.properties.objectLocationsProperties?.offsetX ?? null} name='offsetX' onValueChange={saveData} />
                            </div>
                            <div>
                                <label style={{ paddingLeft: `${globalSizeFactor * 1.5 * 0.5}vh` }} htmlFor='offsetY'>Y </label>
                                <InputNumber className="form__medium-width-input" id='offsetY' value={props.tabInfo.properties.objectLocationsProperties?.offsetY ?? null} name='offsetY' onValueChange={saveData} />
                            </div>
                        </div>
                    </div>
                    <ListBox listStyle={{ minHeight: `${globalSizeFactor * 15}vh`, maxHeight: `${globalSizeFactor * 15}vh` }} name='viewportToSave' value={props.tabInfo.properties.objectLocationsProperties?.viewportToSave} onChange={handleGetOffsetClick} optionLabel='label' options={props.tabInfo.properties.objectLocationsProperties?.allViewports} />
                </div>
                <div style={{ display: 'flex', padding: `${globalSizeFactor * 0.75}vh`, justifyContent: 'flex-end' }}>
                    <Button label='Set' onClick={handleSetOffsetClick} />
                </div>
            </div>
        </Fieldset>
    }
    const getLocationPointOperationFieldset = () => {
        return <Fieldset className="form__column-fieldset form__space-between" legend="Location Point Operation">
            <div style={{ display: 'flex', justifyContent: 'center' }}>
                <Vector3DFromMap pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={props.tabInfo.properties.objectLocationsProperties?.locatonPointOperationPoint} saveTheValue={saveLocationPointOperationPoint} lastPoint={true} />
            </div>
            <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
                <div>
                    <Button label='Add before selected row' onClick={handleAddPointBeforeSelectedRow} disabled={props.tabInfo.properties.objectLocationsProperties?.selectedPointsIndexes?.length === 0} />
                    <Button label='Update selected row like this' onClick={handleUpdateLocationPointByAnotherPoint} disabled={props.tabInfo.properties.objectLocationsProperties?.selectedPointsIndexes?.length === 0} />
                </div>
                <div> <Button label='Move all points by this' onClick={handleMoveAllLocationsPointsByPoint} /></div>
            </div>
        </Fieldset>
    }
    const getSetNumberOfLocationPointsFieldset = () => {
        return <Fieldset className="form__row-fieldset" legend="Set Number Of Location Points">
            <div style={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between', width: '100%' }}>
                <div style={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between', width: '90%' }}>
                    <label htmlFor='numberOfLocationPoints'>Number of Location Points :</label>
                    <InputNumber style={{ paddingLeft: `${globalSizeFactor * 1.5 * 1}vh` }} id='numberOfLocationPoints' value={props.tabInfo.properties.objectLocationsProperties?.numberOfLocationPoints ?? null} name='numberOfLocationPoints' onValueChange={saveData} />
                </div>
                <div><Button disabled={!props.tabInfo.properties.objectLocationsProperties?.numberOfLocationPoints} label='OK' onClick={handleSetNumLocationPoints} /></div>
            </div>
        </Fieldset>
    }
    const getLocationIndexByIDFieldset = () => {
        return <Fieldset className="form__row-fieldset" legend="Location Index by ID">
            <div style={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between', width: '100%' }}>
                <div style={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between', width: '90%' }}>
                    <label htmlFor='nodeID'>Node ID :</label>
                    <InputNumber style={{ paddingLeft: `${globalSizeFactor * 1.5 * 7.75}vh` }} id='nodeID' value={props.tabInfo.properties.objectLocationsProperties?.nodeID ?? null} name='nodeID' onValueChange={saveData} />
                </div>
                <div><Button disabled={!props.tabInfo.properties.objectLocationsProperties?.nodeID} label='OK' onClick={handleGetLocationIndexByID} /></div>
            </div>
        </Fieldset>
    }
    const getObjectLocationPointsFields = () => {
        return <div style={{ padding: `${globalSizeFactor * 1.5}vh` }}>
            <div style={{ padding: `${globalSizeFactor * 0.4}vh`, display: 'flex' }}>
                <label style={{ paddingRight: `${globalSizeFactor * 1.5 * 1.7}vh` }} htmlFor='locationIndex'>Location Index :</label>
                <Dropdown id='locationIndex' value={props.tabInfo.properties.objectLocationsProperties?.locationIndex ?? null} name='locationIndex' onChange={saveData} options={props.tabInfo.properties.objectLocationsProperties?.locationIndexesArr} optionLabel="label" />
            </div>
            <div style={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between', padding: `${globalSizeFactor * 0.4}vh` }}>
                <div>
                    <label style={{ color: 'grey', paddingRight: `${globalSizeFactor * 1.5 * 0.45}vh` }} htmlFor='cs'>Coordinate System :</label>
                    <Dropdown id='cs' value={props.tabInfo.properties.objectLocationsProperties?.cs ?? null} name='cs' onChange={saveData} disabled options={enumDetails.EMcPointCoordSystem} optionLabel="name" />
                </div>
                <div style={{ padding: `${globalSizeFactor * 0.15}vh` }}>
                    <Checkbox disabled inputId="isRelativeToDTM" checked={props.tabInfo.properties.objectLocationsProperties?.isRelativeToDTM} />
                    <label style={{ color: 'grey' }} htmlFor="isRelativeToDTM">Is Relative to DTM</label>
                </div>
            </div>
            <div style={{ paddingTop: `${globalSizeFactor * 1}vh` }}>
                <Vector3DGrid pointCoordSystem={props.tabInfo.properties.objectLocationsProperties?.cs?.theEnum} sendPointList={saveLocationPointsTable} initLocationPointsList={props.tabInfo.properties.objectLocationsProperties?.locationPointsTable} />
            </div>
            <div style={{ display: 'flex', flexDirection: 'row', justifyContent: 'center', paddingTop: `${globalSizeFactor * 0.75}vh`, paddingRight: `${globalSizeFactor * 1.5 * 2}vh` }}>
                <Button label='Set' onClick={handleSetLocationPointsTable} />
                {isSelectedPointsConsecutive() ?
                    <Button label='Update consecutive selected points' onClick={handleUpdateConsecutiveSelectedLocationPoints} /> :
                    <Button label='Update consecutive selected points' disabled />
                }
            </div>
        </div>
    }

    return <div style={{ display: 'flex', flexDirection: 'row' }}>
        <div style={{ width: '70%' }}>
            <Fieldset className="form__column-fieldset form__space-around" legend="Object Location Points">
                {getObjectLocationPointsFields()}
                {getLocationPointOperationFieldset()}
                {getSetNumberOfLocationPointsFieldset()}
                {getLocationIndexByIDFieldset()}
            </Fieldset>
        </div>
        <div style={{ width: '30%' }}>
            {getScreenFieldset()}
        </div>
    </div>
}
