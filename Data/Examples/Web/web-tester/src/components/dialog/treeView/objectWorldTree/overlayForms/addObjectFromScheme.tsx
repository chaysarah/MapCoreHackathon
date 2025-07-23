import _ from 'lodash';
import { Button } from "primereact/button";
import { Checkbox } from "primereact/checkbox";
import { Dropdown } from "primereact/dropdown";
import { ListBox } from "primereact/listbox";
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";
import objectWorldTreeService from "../../../../../services/objectWorldTree.service";
import { AppState } from "../../../../../redux/combineReducer";
import TreeNodeModel, { objectWorldNodeType } from '../../../../shared/models/tree-node.model'
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import { runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";
import { setObjectWorldTree, setTypeObjectWorldDialogSecond } from "../../../../../redux/ObjectWorldTree/objectWorldTreeActions";
import Vector3DGrid from "../shared/vector3DGrid";
import './styles/addObjectFromScheme.css';

export default function AddObjectFromScheme() {
    const selectedNodeInTree: TreeNodeModel = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);
    const treeRedux: TreeNodeModel = useSelector((state: AppState) => state.objectWorldTreeReducer.objectWorldTree);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor);
    let [overlayManager, setOverlayManager] = useState(objectWorldTreeService.getParentByChildKey(treeRedux, selectedNodeInTree.key) as TreeNodeModel)
    let [allSchemesOfOverlay, setAllSchemesOfOverlay] = useState(overlayManager.children.filter(c => c.nodeType == objectWorldNodeType.OBJECT_SCHEME))
    const [locationPoint, setLocationPoint] = useState([])
    const [validPoints, setValidPoints] = useState(true)
    const dispatch = useDispatch();

    const [enumDetails] = useState({
        pointCoordSystem: getEnumDetailsList(MapCore.EMcPointCoordSystem)
    });
    const [formData, setFormData] = useState<{ locationRelativeToDTM: boolean, pointCoordSystemSelect: any }>({
        locationRelativeToDTM: false,
        pointCoordSystemSelect: getEnumValueDetails(MapCore.EMcPointCoordSystem.EPCS_WORLD, enumDetails.pointCoordSystem),
    });

    const [checkScheme, setCheckScheme] = useState<TreeNodeModel>(null)

    useEffect(() => {
        const root = document.documentElement;
        let pixelWidth = window.innerHeight * 0.3 * globalSizeFactor;
        root.style.setProperty('--add-obj-from-scheme-dialog-width', `${pixelWidth}px`);
    }, [])
    const saveData = (event: any) => {
        runCodeSafely(() => {
            setFormData({ ...formData, [event.target.name]: event.target.type === "checkbox" ? event.target.checked : event.target.value })
        }, "general.saveData => onChange")
    }
    const saveCheckScheme = (event: any) => {
        runCodeSafely(() => {
            let Scheme: TreeNodeModel = event.target.value;
            setCheckScheme(Scheme)
            if (Scheme?.nodeMcContent != null) {
                let coordSys = Scheme.nodeMcContent.GetObjectLocation(0).GetCoordSystem()
                let coordSysToDropdown = getEnumValueDetails(coordSys, enumDetails.pointCoordSystem)
                let mcObjectLocation = Scheme.nodeMcContent.GetObjectLocation(0);
                let propId: { Value?: number } = {}
                let isRelativeToDtmScheme = mcObjectLocation.GetRelativeToDTM(propId);
                setFormData({ ...formData, pointCoordSystemSelect: coordSysToDropdown, locationRelativeToDTM: isRelativeToDtmScheme })
            }
        }, "AddObjectFromScheme.saveCheckScheme => onChange")
    }
    const saveLocationPointsTable = (locationPointsList: any, validPoints: any, selectionArr?: any) => {
        let finalTable = locationPointsList;
        if (formData.locationRelativeToDTM) {
            finalTable = locationPointsList.map(point => ({ ...point, z: 0 }));
        }
        setLocationPoint(finalTable);
        setValidPoints(validPoints);
    }
    function createObject(): void {
        runCodeSafely(() => {
            let mcObject;
            let scheme = checkScheme ? checkScheme.nodeMcContent : null
            if (scheme == null)
                throw new Error("No scheme selected");
            if (!validPoints)
                throw new Error("There is an invalid point");
            runMapCoreSafely(() => {
                mcObject = MapCore.IMcObject.Create(selectedNodeInTree.nodeMcContent,
                    scheme,
                    locationPoint);
            }, "AddObjectFromScheme.createObject=>MapCore.IMcObject.Create", true)
            let buildedTree: TreeNodeModel = objectWorldTreeService.buildTree();
            dispatch(setObjectWorldTree(buildedTree));
            dispatch(setTypeObjectWorldDialogSecond(undefined))
        }, "AddObjectFromScheme.createObject")
    }

    return (
        <>
            <div style={{ display: 'flex', flexDirection: 'row' }}>
                <div>
                    <h4 className="noMargin">Schemes</h4>
                    <ListBox name="checkScheme" value={checkScheme} multiple={false}
                        onChange={saveCheckScheme} options={allSchemesOfOverlay} optionLabel="label"></ListBox>
                </div>
                <div style={{ marginLeft: '2%' }}>
                    <div className='form__lower-margin disabledDiv'>
                        <Checkbox inputId="ingredient1" name="LocationRelativeToDTM" onChange={saveData} checked={formData.locationRelativeToDTM} />
                        <label htmlFor="ingredient1">Location Relative To DTM</label><br></br>
                        <div style={{ whiteSpace: 'nowrap' }}><label>Location Coordinate System: </label>
                            <Dropdown name="pointCoordSystemSelect" value={formData.pointCoordSystemSelect} onChange={saveData} options={enumDetails.pointCoordSystem} optionLabel="name" />
                        </div>
                    </div>
                    <Vector3DGrid initLocationPointsList={locationPoint} pointCoordSystem={formData.pointCoordSystemSelect.theEnum} sendPointList={saveLocationPointsTable} />
                </div>
            </div>
            <Button label="OK" onClick={createObject}></Button>
        </>
    )
}

