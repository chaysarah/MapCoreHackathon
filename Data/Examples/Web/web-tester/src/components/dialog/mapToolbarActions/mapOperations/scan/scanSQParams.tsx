import { useEffect, useRef, useState } from 'react';
import { MultiSelect } from 'primereact/multiselect';
import { Dropdown } from 'primereact/dropdown';
import { InputNumber } from 'primereact/inputnumber';
import { Checkbox } from 'primereact/checkbox';
import { Tree } from 'primereact/tree';
import { useSelector } from 'react-redux';
import { Button } from 'primereact/button';

import { getEnumDetailsList, getEnumValueDetails, ViewportData, MapCoreData } from 'mapcore-lib';
import './styles/scan.css';
import { AppState } from '../../../../../redux/combineReducer';
import { runCodeSafely } from '../../../../../common/services/error-handling/errorHandler';

export default function ScanSQParams(props: { sqParams: MapCore.IMcSpatialQueries.SQueryParams, setSQParamsCallback: (sqParams: MapCore.IMcSpatialQueries.SQueryParams) => void }) {
    const [nodes, setNodes] = useState([]);
    const [arrNodes, setarrNodes] = useState([]);
    const [selectedKeys, setSelectedKeys] = useState(null);
    const [listOverlay, setListOverlay] = useState(null);
    const activeCard = useSelector((state: AppState) => state.mapWindowReducer.activeCard);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor);
    const viewportData: ViewportData = MapCoreData.findViewport(activeCard);
    const srcParams = useRef<MapCore.IMcSpatialQueries.SQueryParams>(props.sqParams)

    useEffect(() => {
        runCodeSafely(() => {
            //  לאתחל את העץ בערכים שהגיעו ממאפ קור או שנבחרו בטופס הקודם.
            // getEnumValueDetails(generalService.ScanPropertiesBase.SpatialQueryParams.uItemTypeFlagsBitField)
            // setSelectedKeys()
            // let arr: any[] = [];
            // let node = [];
            // // 3  4  5  6
            // for (let index = 0; index < arr.length; index++) {
            //     node.push(arrNodes.filter(n => n.data == arr[index]))
            // }
            const root = document.documentElement;
            let pixelWidth = window.innerHeight * 0.6 * globalSizeFactor;
            root.style.setProperty('--sq-params-dialog-width', `${pixelWidth}px`);
        }, "ScanSQParams.useEffect")
    }, []);

    const [enumDetails] = useState({
        EIntersectionuTargetsBitMask: getEnumDetailsList(MapCore.IMcSpatialQueries.EIntersectionTargetType),
        ENodeKindFlags: getEnumDetailsList(MapCore.IMcObjectSchemeNode.ENodeKindFlags),
        EeNoDTMResult: getEnumDetailsList(MapCore.IMcSpatialQueries.ENoDTMResult),
        EQueryPrecision: getEnumDetailsList(MapCore.IMcSpatialQueries.EQueryPrecision)
    });

    const [formScanSQParams, setScanSQParams] = useState({
        ...srcParams.current,
        uTargetsBitMask: getEnumValueDetails(srcParams.current.uTargetsBitMask, enumDetails.EIntersectionuTargetsBitMask),
        uItemKindsBitField: getEnumValueDetails(srcParams.current.uItemKindsBitField, enumDetails.ENodeKindFlags),
        eNoDTMResult: getEnumValueDetails(srcParams.current.eNoDTMResult, enumDetails.EeNoDTMResult),
        eTerrainPrecision: getEnumValueDetails(srcParams.current.eTerrainPrecision, enumDetails.EQueryPrecision),
        overlay: { name: srcParams.current.pOverlayFilter?.GetID(), code: 0, pOverlayFilter: srcParams.current.pOverlayFilter }
    })

    useEffect(() => {
        runCodeSafely(() => {
            let overlays = viewportData ? viewportData.viewport.GetOverlayManager().GetOverlays() : [];
            setListOverlay(overlays.map((o, i) => { return { name: "overlay(" + o.GetID() + ")", code: o.GetID == formScanSQParams.overlay?.pOverlayFilter?.GetID ? 0 : i, overlay: o } }))
            createTree()
        }, "ScanSQParams.useEffect")
    }, []);

    const createTree = () => {
        let res: any = MapCore.IMcObjectScheme.SaveSchemeComponentInterface(MapCore.IMcObjectScheme.ESchemeComponentKind.ESCK_OBJECT_SCHEME_NODE, 0)
        res = JSON.parse(String.fromCharCode.apply(null, res))
        let arr: any = [];
        let arrN: any = [];
        res.objectSchemeNodes.map((e: any, index1: number) => {
            let mone1 = 0
            let o: any = {
                key: index1,
                label: e.groupName ? e.groupName : e.displayName,
                data: e.interfaceName ? eval("MapCore." + e.interfaceName + ".NODE_TYPE") : 0
            }
            let children_: any = [];
            if (e.elements) {
                e.elements.map((element: any, index2: number) => {
                    let mone2 = 0
                    let child: any = {
                        key: index1 + "-" + index2,
                        label: element.groupName ? element.groupName : element.displayName,
                        data: element.interfaceName ? eval("MapCore." + element.interfaceName + ".NODE_TYPE") : 0
                    }
                    mone1 += child.data;
                    let child_children: any = [];
                    if (element.elements) {
                        element.elements.forEach((element_: any, index3: number) => {
                            let child_child: any = {
                                key: index1 + "-" + index2 + "-" + index3,
                                label: element_.groupName ? element_.groupName : element_.displayName,
                                data: element_.interfaceName ? eval("MapCore." + element_.interfaceName + ".NODE_TYPE") : 0
                            }
                            mone1 += child_child.data;
                            mone2 += child_child.data;
                            arrN.push(child_child)
                            child_children.push(child_child)
                        })
                        child = { ...child, children: child_children }
                    }
                    arrN.push(child)
                    children_.push(child)
                })
                o = { ...o, children: children_ }
            }
            arrN.push(o)
            arr.push(o)
        });
        setNodes(arr)
        setarrNodes(arrN)
    }
    const saveData = (event: any) => {
        setScanSQParams({ ...formScanSQParams, [event.target.name]: event.target.type === "checkbox" ? event.target.checked : event.target.value })
    }

    useEffect(() => {
        runCodeSafely(() => {
            let selectedData = savetree();
            let bitFielduTargetsBitMask: number = 0;
            if (formScanSQParams.uTargetsBitMask?.length > 0) {
                for (let index: number = 0; index < formScanSQParams.uTargetsBitMask.length; index++) {
                    bitFielduTargetsBitMask |= formScanSQParams.uTargetsBitMask[index]!.code;
                }
            }
            let bitFielduItemKindsBitField: number = 0;
            if (formScanSQParams.uTargetsBitMask?.length > 0) {
                for (let index: number = 0; index < formScanSQParams.uItemKindsBitField.length; index++) {
                    bitFielduItemKindsBitField |= formScanSQParams.uItemKindsBitField[index]!.code;
                }
            }
            let SQParams = {
                ...formScanSQParams,
                uTargetsBitMask: bitFielduTargetsBitMask,
                uItemKindsBitField: bitFielduItemKindsBitField,
                eNoDTMResult: formScanSQParams.eNoDTMResult.theEnum,
                eTerrainPrecision: formScanSQParams.eTerrainPrecision.theEnum,
                uItemTypeFlagsBitField: selectedData,

                pOverlayFilter: formScanSQParams.overlay.pOverlayFilter
            }
            props.setSQParamsCallback(SQParams);
        }, "ScanSQParams.useEffect of formScanSQParams, selectedKeys,formScanSQParams.Overlay")

    }, [formScanSQParams, selectedKeys, formScanSQParams.overlay]);

    const savetree = () => {
        let sum = 0;
        let keys: any = []
        if (selectedKeys)
            keys = Object.keys(selectedKeys).filter(key => selectedKeys[key].partialChecked == false);
        for (let index = 0; index < keys.length; index++) {
            const element = arrNodes.find(y => y.key == keys[index]);
            if (element)
                sum += element.data;
        }
        return sum;
    }

    return (<div className='scan__sq-params-form'>
        <div className='form__column-container'>
            <div className='form__flex-and-row-between'>
                <label >Intersection Target Type:</label>
                <MultiSelect style={{ width: `${globalSizeFactor * 14}vh` }} maxSelectedLabels={1} name="uTargetsBitMask" value={formScanSQParams.uTargetsBitMask} onChange={saveData} options={enumDetails.EIntersectionuTargetsBitMask} optionLabel="name" />
            </div>
            <div className='form__flex-and-row-between'>
                <label >Items Type:</label>
                <Tree style={{ padding: '0' }} value={nodes} selectionMode="checkbox" selectionKeys={selectedKeys}
                    onSelectionChange={e => { setSelectedKeys(e.value) }} />
            </div>
            <div className='form__flex-and-row-between'>
                <label >Items Kind:</label>
                <MultiSelect style={{ width: `${globalSizeFactor * 14}vh` }} maxSelectedLabels={1} name="uItemKindsBitField" value={formScanSQParams.uItemKindsBitField} onChange={saveData} options={enumDetails.ENodeKindFlags} optionLabel="name" />
            </div>
            <div className='form__flex-and-row-between'>
                <label>Bounding Box Expansion Dist</label>
                <InputNumber name="fBoundingBoxExpansionDist" value={formScanSQParams.fBoundingBoxExpansionDist} onValueChange={(e) => saveData(e)} mode="decimal" />
            </div>
            <div className='form__flex-and-row-between'>
                <label>Great Circle Precision</label>
                <InputNumber name="fGreatCirclePrecision" value={formScanSQParams.fGreatCirclePrecision} onValueChange={(e) => saveData(e)} mode="decimal" />
            </div>
            <div className='form__flex-and-row'>
                <Checkbox inputId="Checkbox1" name="bUseMeshBoundingBoxOnly" onChange={saveData} checked={formScanSQParams.bUseMeshBoundingBoxOnly} />
                <label htmlFor="Checkbox1" className="ml-2 checkboxDiv">Use Mesh Bounding Box Only</label>
            </div>
            <div className='form__flex-and-row'>
                <Checkbox inputId="Checkbox2" name="UseFlatEarth" onChange={saveData} checked={formScanSQParams.bUseFlatEarth} />
                <label htmlFor="Checkbox2" className="ml-2 checkboxDiv">Use Flat Earth</label>
            </div>
            <div className='form__flex-and-row'>
                <Checkbox inputId="Checkbox3" name="bAddStaticObjectContours" onChange={saveData} checked={formScanSQParams.bAddStaticObjectContours} />
                <label htmlFor="Checkbox3" className="ml-2 checkboxDiv">Add Static Object Contours</label>
            </div>
            <div className='form__flex-and-row-between'>
                <label >No DTM Result:</label>
                <Dropdown name="eNoDTMResult" value={formScanSQParams.eNoDTMResult} onChange={saveData} options={enumDetails.EeNoDTMResult} optionLabel="name" />
            </div>
            <div className='form__flex-and-row-between'>
                <label >Terrain Precision:</label>
                <Dropdown name="eTerrainPrecision" value={formScanSQParams.eTerrainPrecision} onChange={saveData} options={enumDetails.EQueryPrecision} optionLabel="name" />
            </div>
            <div className='form__flex-and-row-between'>
                <label >Overlay:</label>
                <Dropdown name="overlay" value={formScanSQParams.overlay} onChange={saveData} options={listOverlay} optionLabel="name" />
                <Button style={{ marginLeft: '1%' }} onClick={() => { setScanSQParams({ ...formScanSQParams, overlay: null }) }}>Clear Selection</Button>
            </div>
            <br></br>
        </div></div>)
}