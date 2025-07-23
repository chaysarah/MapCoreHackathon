import { Column } from "primereact/column"
import { DataTable } from "primereact/datatable"
import { ReactElement, useEffect, useRef, useState } from "react"
import { AppState } from "../../../../../redux/combineReducer";
import { ObjectWorldService, ViewportService, MapCoreData, ViewportData, getEnumValueDetails, getEnumDetailsList } from 'mapcore-lib';
import { useDispatch, useSelector } from "react-redux";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";
import { setObjectWorldTree } from "../../../../../redux/ObjectWorldTree/objectWorldTreeActions";
import objectWorldTreeService from "../../../../../services/objectWorldTree.service";
import TreeNodeModel from "../../../../shared/models/tree-node.model"
import './styles/eventCallbackDialog.css';
import generalService from "../../../../../services/general.service";
import { Button } from "primereact/button";

export default function EventCallbackDialog(props: { footerHook: (footer: () => ReactElement) => void }) {
    const dispatch = useDispatch();
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const activeViewport: ViewportData = useSelector((state: AppState) => MapCoreData.findViewport(state.mapWindowReducer.activeCard));
    let [eventCallbackList, setEventCallbackList] = useState([]);
    let treeRedux = useSelector((state: AppState) => state.objectWorldTreeReducer.objectWorldTree);
    let eventCallbackListRef = useRef([]);
    let lastMessageRef = useRef(null);

    useEffect(() => {
        setEventCallbackDialogWidth();
        updateTreeRedux();
    }, [])
    useEffect(() => {
        let iMcEditModeCallback: any = new MapCoreData.iMcEditModeCallbacksClass({
            NewVertex: function (pObject: MapCore.IMcObject, pItem: MapCore.IMcObjectSchemeItem, WorldVertex: MapCore.SMcVector3D, ScreenVertex: MapCore.SMcVector3D, uVertexIndex: number, dAngle: number) {
                let treeNodeModel = objectWorldTreeService.convertMapcorObjectToTreeNodeModel(treeRedux, pItem)
                let messageObj = {
                    number: eventCallbackListRef.current.length + 1,
                    eventDescription: `EditMode NewVertex() Item Code: ${treeNodeModel?.label.substring(1, treeNodeModel?.label.indexOf(')'))} Vertex index ${uVertexIndex} World (x: ${Math.round(WorldVertex.x)} ,y: ${Math.round(WorldVertex.y)} ,z: ${Math.round(WorldVertex.z)} ) Screen (x: ${Math.round(ScreenVertex.x)} ,y: ${Math.round(ScreenVertex.y)} ) Angle: ${dAngle}`
                }
                eventCallbackListRef.current = [...eventCallbackListRef.current, messageObj];
                setEventCallbackList(eventCallbackListRef.current)
            },
            /** Optional */
            PointDeleted: function (pObject: MapCore.IMcObject, pItem: MapCore.IMcObjectSchemeItem, WorldVertex: MapCore.SMcVector3D, ScreenVertex: MapCore.SMcVector3D, uVertexIndex: number) {
                let treeNodeModel = objectWorldTreeService.convertMapcorObjectToTreeNodeModel(treeRedux, pItem)
                let obj = {
                    number: eventCallbackListRef.current.length + 1, eventDescription: `EditMode - PointDeleted() Item Code: ${treeNodeModel?.label.substring(1, treeNodeModel?.label.indexOf(')'))} Vertex Index: ${uVertexIndex} World (x: ${Math.round(WorldVertex.x)} , y: ${Math.round(WorldVertex.y)} , z: ${Math.round(WorldVertex.z)} ) Screen(x: ${Math.round(ScreenVertex.x)} y: ${Math.round(ScreenVertex.y)} )`
                }
                eventCallbackListRef.current = [...eventCallbackListRef.current, obj];
                setEventCallbackList(eventCallbackListRef.current)
            },
            /** Optional */
            PointNewPos: function (pObject: MapCore.IMcObject, pItem: MapCore.IMcObjectSchemeItem, WorldVertex: MapCore.SMcVector3D, ScreenVertex: MapCore.SMcVector3D, uVertexIndex: number, dAngle: number, bDownOnHeadPoint: boolean) {
                let treeNodeModel = objectWorldTreeService.convertMapcorObjectToTreeNodeModel(treeRedux, pItem)
                let obj = {
                    number: eventCallbackListRef.current.length + 1, eventDescription: `EditMode - PointNewPos() Item Code: ${treeNodeModel?.label.substring(1, treeNodeModel?.label.indexOf(')'))} Vertex Index: ${uVertexIndex} World(x: ${Math.round(WorldVertex.x)}, y: ${Math.round(WorldVertex.y)}, z: ${Math.round(WorldVertex.z)}) Screen(x: ${Math.round(ScreenVertex.x)},  y: ${Math.round(ScreenVertex.y)}) Angle: ${dAngle} DownOnHeadPoint: ${bDownOnHeadPoint}`
                }
                eventCallbackListRef.current = [...eventCallbackListRef.current, obj];
                setEventCallbackList(eventCallbackListRef.current)
            },
            /** Optional */
            ActiveIconChanged: function (pObject: MapCore.IMcObject, pItem: MapCore.IMcObjectSchemeItem, eIconPermission: MapCore.IMcEditMode.EPermission, uIconIndex: number) {
                let treeNodeModel = objectWorldTreeService.convertMapcorObjectToTreeNodeModel(treeRedux, pItem)
                let eIconPermissionList = getEnumDetailsList(MapCore.IMcEditMode.EPermission);
                let eIconPermissionName = getEnumValueDetails(eIconPermission, eIconPermissionList).name;
                let obj = {
                    number: eventCallbackListRef.current.length + 1, eventDescription: `EditMode - ActiveIconChanged() Item Code: ${treeNodeModel?.label.substring(1, treeNodeModel?.label.indexOf(')'))} Icon Permission: ${eIconPermissionName} Icon Index: ${uIconIndex}`
                }
                eventCallbackListRef.current = [...eventCallbackListRef.current, obj];
                setEventCallbackList(eventCallbackListRef.current)
            },
            /** Optional */
            InitItemResults: function (pObject: MapCore.IMcObject, pItem: MapCore.IMcObjectSchemeItem, nExitCode: number) {
                let treeNodeModel = objectWorldTreeService.convertMapcorObjectToTreeNodeModel(treeRedux, pItem)
                let obj = {
                    number: eventCallbackListRef.current.length + 1, eventDescription: `EditMode - InitItemResults() Item Code: ${treeNodeModel?.label.substring(1, treeNodeModel?.label.indexOf(')'))} exit code: ${nExitCode}`
                }
                eventCallbackListRef.current = [...eventCallbackListRef.current, obj];
                setEventCallbackList(eventCallbackListRef.current)
            },
            /** Optional */
            EditItemResults: function (pObject: MapCore.IMcObject, pItem: MapCore.IMcObjectSchemeItem, nExitCode: number) {
                let treeNodeModel = objectWorldTreeService.convertMapcorObjectToTreeNodeModel(treeRedux, pItem)
                let obj = {
                    number: eventCallbackListRef.current.length + 1, eventDescription: `EditMode - EditItemResults() Item Code: ${treeNodeModel?.label.substring(1, treeNodeModel?.label.indexOf(')'))} exit code: ${nExitCode}`
                }
                eventCallbackListRef.current = [...eventCallbackListRef.current, obj];
                setEventCallbackList(eventCallbackListRef.current)
            },
            /** Optional */
            DragMapResults: function (pViewport: MapCore.IMcMapViewport, NewCenter: MapCore.SMcVector3D) {
                let obj = {
                    number: eventCallbackListRef.current.length + 1, eventDescription: `EditMode - DragMapResults() Viewport: ${pViewport} New Center(x: ${Math.round(NewCenter.x)} y: ${Math.round(NewCenter.y)} z: ${Math.round(NewCenter.z)}`
                }
                eventCallbackListRef.current = [...eventCallbackListRef.current, obj];
                setEventCallbackList(eventCallbackListRef.current)
            },
            /** Optional */
            RotateMapResults: function (pViewport: MapCore.IMcMapViewport, fNewYaw: number, fNewPitch: number) {
                let obj = {
                    number: eventCallbackListRef.current.length + 1, eventDescription: `EditMode - RotateMapResults() Viewport: ${pViewport} New Yaw: ${fNewYaw} New Pitch: ${fNewPitch}`
                }
                eventCallbackListRef.current = [...eventCallbackListRef.current, obj];
                setEventCallbackList(eventCallbackListRef.current)
            },
            /** Optional */
            DynamicZoomResults: function (pViewport: MapCore.IMcMapViewport, fNewScale: number, NewCenter: MapCore.SMcVector3D) {
                let obj = {
                    number: eventCallbackListRef.current.length + 1, eventDescription: `EditMode - DynamicZoomResults() Viewport: ${pViewport} New Scale: ${fNewScale} New Center(x: ${Math.round(NewCenter.x)} y: ${Math.round(NewCenter.y)} z: ${Math.round(NewCenter.z)} )`
                }
                eventCallbackListRef.current = [...eventCallbackListRef.current, obj];
                setEventCallbackList(eventCallbackListRef.current)
            },
            /** Optional */
            DistanceDirectionMeasureResults: function (pViewport: MapCore.IMcMapViewport, WorldVertex1: MapCore.SMcVector3D, WorldVertex2: MapCore.SMcVector3D, dDistance: number, dAngle: number) {
                let obj = {
                    number: eventCallbackListRef.current.length + 1, eventDescription: `EditMode - DistanceDirectionMeasureResults() world vertex1(x: ${Math.round(WorldVertex1.x)} y: ${Math.round(WorldVertex1.y)} z: ${Math.round(WorldVertex1.z)}) world vertex2(x: ${Math.round(WorldVertex2.x)} y: ${Math.round(WorldVertex2.y)} z: ${Math.round(WorldVertex2.z)} ) distance: ${Math.round(dDistance)} angle: ${dAngle}`
                }
                eventCallbackListRef.current = [...eventCallbackListRef.current, obj];
                setEventCallbackList(eventCallbackListRef.current)
            },
            /** Optional */
            CalculateHeightResults: function (pViewport: MapCore.IMcMapViewport, dHeight: number, aCoords: MapCore.SMcVector3D[], nStatus: number) {
                let testerVp = MapCoreData.viewportsData.find(testerVp => testerVp.viewport == pViewport);
                let obj = {
                    number: eventCallbackListRef.current.length + 1, eventDescription: `EditMode - CalculateHeightResults()
                Map Code: ${generalService.getObjectName(testerVp, 'Viewport')}
                Returned Height: ${dHeight}\n`
                }
                let coordNum = 1;
                for (let currCord of aCoords) {
                    obj.eventDescription += "-------------------------------------------\n";
                    obj.eventDescription += "Coord Number " + coordNum + "\n";
                    obj.eventDescription += "X=" + currCord.x + ",Y=" + currCord.y + ",Z=" + currCord.z + "\n";
                }
                eventCallbackListRef.current = [...eventCallbackListRef.current, obj];
                setEventCallbackList(eventCallbackListRef.current)
            },
            /** Optional */
            CalculateVolumeResults: function (pViewport: MapCore.IMcMapViewport, dVolume: number, aCoords: MapCore.SMcVector3D[], nStatus: number) {
                let testerVp = MapCoreData.viewportsData.find(testerVp => testerVp.viewport == pViewport);
                let eventMessage = `EditMode - CalculateVolumeResults()\n Map Code: ${generalService.getObjectName(testerVp, 'Viewport')}\n Returned Volume: ${dVolume}\n`;
                let coordNum = 1;
                for (let currCord of aCoords) {
                    eventMessage += "-------------------------------------------\n";
                    eventMessage += "Coord Number " + coordNum + "\n";
                    eventMessage += "X=" + currCord.x + ",Y=" + currCord.y + ",Z=" + currCord.z + "\n";
                }
                let obj = { number: eventCallbackListRef.current.length + 1, eventDescription: 'CalculateVolumeResults' }
                eventCallbackListRef.current = [...eventCallbackListRef.current, obj];
                setEventCallbackList(eventCallbackListRef.current)
            }
        });

        runMapCoreSafely(() => { activeViewport.editMode.SetEventsCallback(iMcEditModeCallback); }, "eventCallbackDialog.useEffect", true)
    }, [treeRedux])
    useEffect(() => {
        lastMessageRef?.current?.scrollIntoView({
            behavior: 'smooth'
        })
    })

    function setEventCallbackDialogWidth() {
        const root = document.documentElement;
        let pixelWidth = window.innerHeight * 0.7 * globalSizeFactor;
        root.style.setProperty('--event-callback-dialog-width', `${pixelWidth}px`);
    }
    const updateTreeRedux = () => {
        runCodeSafely(() => {
            let buildedTree: TreeNodeModel = objectWorldTreeService.buildTree();
            dispatch(setObjectWorldTree(buildedTree));
        }, "objectWorldTreeDialog.useEffect");
    }
    useEffect(() => {
        props.footerHook(getFooter)
    }, [eventCallbackList])

    const getFooter = () => {
        return <div className='form__footer-padding' >
            <Button label='Clear' onClick={() => { eventCallbackListRef.current = []; setEventCallbackList([]) }} />
        </div>
    }

    return <div className="event-allback__table">
        <DataTable className="event-callback__multi-line" showGridlines size="small" value={eventCallbackList} tableStyle={{ minWidth: '47rem', maxWidth: '47rem' }}>
            <Column field="number" header='Number'></Column>
            <Column field="eventDescription" header='Event Description' body={(rowData) => { return <div ref={rowData.number == eventCallbackList.length ? lastMessageRef : null}> {rowData.eventDescription}</div> }} ></Column>
        </DataTable>
    </div>
}
