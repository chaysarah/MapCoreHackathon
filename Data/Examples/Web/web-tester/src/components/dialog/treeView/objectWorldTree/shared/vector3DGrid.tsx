import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Column } from "primereact/column";
import { DataTable } from "primereact/datatable";
import { InputNumber } from "primereact/inputnumber";
import { useEffect, useState, useRef, ChangeEvent } from "react";
import { useDispatch, useSelector } from "react-redux";
import { faTrash } from '@fortawesome/free-solid-svg-icons'
import { Button } from "primereact/button";
import { ContextMenu } from "primereact/contextmenu";
import _ from "lodash";

import { hideFormReasons } from "../../../../shared/models/tree-node.model";
import { AppState } from "../../../../../redux/combineReducer";
import { DialogTypesEnum } from "../../../../../tools/enum/enums";
import { setShowDialog } from "../../../../../redux/ObjectWorldTree/objectWorldTreeActions";

class Point {
    x: number;
    y: number;
    z: number
    constructor(x: number, y: number, z?: number) {
        this.x = x;
        this.y = y;
        if (z != undefined)
            this.z = z;
        else
            this.z = null
    }
}
class ShowPoint {
    index: number;
    point: Point;
    valid: boolean
    constructor(index: number, point: Point, valid: boolean) {
        this.index = index;
        this.point = point;
        this.valid = valid;
    }
}
export default function Vector3DGrid(props: {
    pointCoordSystem: MapCore.EMcPointCoordSystem, initLocationPointsList: Point[],
    sendPointList: (locationPointsList: Point[], valid: boolean, selectedPoint?: Point[]) => void,
    ctrlHeight?: number,
    disabled?: boolean,
    disabledPointFromMap?: boolean
    style?: { [key: string]: any },
}) {
    const dispatch = useDispatch();
    const cursorPos: any = useSelector((state: AppState) => state.mapWindowReducer.cursorPos);
    const dialogTypesArr: DialogTypesEnum[] = useSelector((state: AppState) => state.mapCoreReducer.dialogTypesArr);
    const screenPos: MapCore.SMcVector3D = useSelector((state: AppState) => state.mapWindowReducer.screenPos);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [buttonClick, setbuttonClick] = useState(false);

    const [locationPointsList, setlocationPointsList] = useState<ShowPoint[]>();//([{ index: -1, point: new Point(null, null, null), valid: true }])
    const [startLocationPointsList, setStartLocationPointsList] = useState<ShowPoint[]>()
    const [selectedPoint, setSelectedPoint] = useState<ShowPoint[]>([])

    const choosePoint = () => {
        let lastDialogType = dialogTypesArr[dialogTypesArr.length - 1];
        dispatch(setShowDialog({ hideFormReason: hideFormReasons.CHOOSE_POINT, dialogType: lastDialogType }));
        setbuttonClick(true)
    }
    useEffect(() => {

        let l = createShowPointFromPoint(props.initLocationPointsList ? props.initLocationPointsList : []);
        setlocationPointsList(l);
        setStartLocationPointsList(l)

    }, [])
    const createShowPointFromPoint = (pointList: Point[]) => {
        let l: ShowPoint[] = [];
        if (pointList) {
            l = pointList.map((p, i) => { return new ShowPoint(i + 1, new Point(p.x, p.y, p.z), p.x != null && p.y != null) })
        }
        l.push({ index: -1, point: new Point(null, null, null), valid: true });
        return l;
    }
    useEffect(() => {
        if (locationPointsList) {
            let v = locationPointsList.filter(p => p.valid == false);
            let p = locationPointsList.map((p) => { return p.point })
            let s = selectedPoint.map((p) => { return p.point })
            p.pop()
            let typedPropsPoints = props.initLocationPointsList.map(point => new Point(point.x, point.y, point.z))
            if (!_.isEqual(p, typedPropsPoints)) {
                props.sendPointList(p, v.length === 0, s)
            }
        }
    }, [locationPointsList])

    useEffect(() => {
        if (buttonClick == true) {
            setbuttonClick(false)
            let newPoint;
            if (props.pointCoordSystem === MapCore.EMcPointCoordSystem.EPCS_WORLD)
                newPoint = new ShowPoint(locationPointsList.length, new Point(cursorPos.Value.x, cursorPos.Value.y, cursorPos.Value.z), true)
            if (props.pointCoordSystem === MapCore.EMcPointCoordSystem.EPCS_SCREEN)
                newPoint = new ShowPoint(locationPointsList.length, new Point(screenPos.x, screenPos.y, screenPos.z), true)
            let l = [...locationPointsList]
            l.splice(l.length - 1, 0, newPoint);
            setlocationPointsList(l);
        }
    }, [cursorPos])

    useEffect(() => {
        let showPointsArr = createShowPointFromPoint(props.initLocationPointsList);
        setlocationPointsList(showPointsArr);
    }, [props.initLocationPointsList])

    const changeInput = (event: any, rowData: ShowPoint) => {
        if (rowData.index == -1)
            addNewRow(event)
        else {
            let l = [...locationPointsList];
            let pointEdit = locationPointsList.filter(p => p.index == rowData.index)[0].point;
            pointEdit = { ...pointEdit, [event.target.name]: event.target.value }
            l[rowData.index - 1].valid = pointEdit.x != null && pointEdit.y != null;
            l[rowData.index - 1].point = pointEdit;
            setlocationPointsList(l);
        }
    }
    const addNewRow = (event: any) => {
        let newPoint = new ShowPoint(locationPointsList.length, new Point(null, null, null), false);
        newPoint.point = { ...newPoint.point, [event.target.name]: event.target.value }
        let l = [...locationPointsList]
        l.splice(l.length - 1, 0, newPoint);
        setlocationPointsList(l);
    }
    const deleteItem = (rowData: ShowPoint) => {
        let l = [...locationPointsList].filter(p => p.index != rowData.index)
        setlocationPointsList(l)
        arrangeIndex(l)
    }
    const arrangeIndex = (list: ShowPoint[]) => {
        let l = list;
        l = l.map((p, i) => {
            if (p.index != -1) return { ...p, index: i + 1 }
            else return p
        })
        setlocationPointsList(l)
    }
    const handleExportCsvFileClick = () => {
        let l = [...locationPointsList];
        l.pop()
        const csvData = 'Z,Y,X' + "\n" + l.map((p) => `${p.point.z},${p.point.y},${p.point.x}`).join("\n");
        const downloadLink = document.createElement("a");
        downloadLink.href = "data:text/csv;charset=utf-8," + encodeURIComponent(csvData);
        downloadLink.download = "objectLocationPoints.csv";
        downloadLink.click();
    }
    const handleTableSelectionChange = (e) => {
        const showPointsArr = e.value as ShowPoint[];
        let l = showPointsArr.filter((p: ShowPoint) => p.index != -1);
        setSelectedPoint(l)
    }
    const handleCsvFileSelected = (event: ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files[0];
        const reader = new FileReader();
        reader.onload = (event) => {
            const csvData = event.target.result as string;
            console.log(csvData)
            const data = csvData.split('\n').map((row: string) => row.split(','));
            let newLocationPointsList: { x: number, y: number, z: number }[] = [];
            data.forEach((arr, i) => {
                if ((arr.length >= 2) && (i != 0)) {
                    let obj = { x: parseInt(arr[2]), y: parseInt(arr[1]), z: parseInt(arr[0]) } as MapCore.SMcVector3D
                    newLocationPointsList = [...newLocationPointsList, obj]
                }
            })
            let l = createShowPointFromPoint(newLocationPointsList)
            setlocationPointsList(l)
        };
        reader.readAsText(file);
    }
    let cm = useRef<ContextMenu>();
    const [pastePoint, setPastePoint] = useState<ShowPoint[]>()
    const contextMenuItems = [{ label: "Copy", command: () => { setPastePoint(selectedPoint) } },
    {
        label: "Paste", command: () => {
            let l = [...locationPointsList];
            pastePoint.forEach((newPoint) => { l.splice(l.length - 1, 0, { ...newPoint }) });
            setlocationPointsList(l);
            arrangeIndex(l)
        }
    }];

    return (<div onContextMenu={(e) => { cm.current?.show(e) }}>
        <DataTable style={{ overflow: 'auto', height: `${globalSizeFactor * (props.ctrlHeight || 13)}vh` }} size='small' value={locationPointsList} selectionMode='checkbox'
            selection={selectedPoint} onSelectionChange={handleTableSelectionChange} >
            <Column selectionMode="multiple"></Column>
            <Column field="index" header="No." body={(rowData) => { return (rowData.index != -1) ? <label >{rowData.index}</label> : null }}></Column>
            <Column field="x" header="X" body={(rowData) => {
                return <InputNumber disabled={props.disabled} name="x" className={!rowData.valid ? 'p-invalid form__narrow-input' : 'form__narrow-input'}
                    onValueChange={(e) => { changeInput(e, rowData) }} value={rowData.point.x} ></InputNumber>
            }}></Column>
            <Column field="y" header="Y" body={(rowData) => {
                return <InputNumber disabled={props.disabled} name="y" className={!rowData.valid ? 'p-invalid form__narrow-input' : 'form__narrow-input'}
                    onValueChange={(e) => { changeInput(e, rowData) }} value={rowData.point.y} ></InputNumber>
            }}></Column>
            <Column field="z" header="Z" body={(rowData) => {
                return <InputNumber disabled={props.disabled} name="z" className={!rowData.valid ? 'p-invalid form__narrow-input' : 'form__narrow-input'}
                    onValueChange={(e) => { changeInput(e, rowData) }} value={rowData.point.z} ></InputNumber>
            }}></Column>
            <Column field="t" body={(rowData) => {
                if (rowData.index != -1)
                    return <FontAwesomeIcon size="xl" style={{ color: '#6366F1', marginLeft: '3%' }} icon={faTrash} onClick={() => { deleteItem(rowData) }} />
                else
                    return null
            }}></Column>
        </DataTable>
        {!props.disabled && <div style={{ display: 'flex', flexDirection: 'row', justifyContent: 'center', paddingTop: `${globalSizeFactor * 0.75}vh`, paddingRight: `${globalSizeFactor * 1.5 * 2}vh` }}>
            <Button disabled={props.disabledPointFromMap} onClick={choosePoint}>...</Button>
            <Button label='Export...' onClick={handleExportCsvFileClick} />
            <input type="file" id="importCsv" accept='.csv' style={{ display: 'none' }} onChange={handleCsvFileSelected} />
            <Button label="Import..." onClick={() => document.getElementById('importCsv').click()} />
            <Button label="Clear" onClick={() => setlocationPointsList([{ index: -1, point: new Point(null, null, null), valid: true }])} />
            {startLocationPointsList?.length > 1 && <Button label="Refresh" onClick={() => {
                setlocationPointsList(startLocationPointsList);
            }} />}
        </div>}
        <ContextMenu model={contextMenuItems} ref={cm} ></ContextMenu>
    </div>
    )
}