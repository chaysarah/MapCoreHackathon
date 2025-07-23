import { useEffect, useState } from "react";
import { Dropdown } from "primereact/dropdown";
import { InputText } from "primereact/inputtext";
import { Button } from "primereact/button";
import { Fieldset } from "primereact/fieldset";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { useSelector } from "react-redux";

import { getEnumDetailsList, getEnumValueDetails, MapCoreData } from 'mapcore-lib';
import UploadFilesCtrl, { UploadTypeEnum } from "../../../shared/uploadFilesCtrl";
import { AppState } from "../../../../../redux/combineReducer";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";

export default function DeviceOperations(props: { getStringPath: (stringPath: { index: number, stringPath: string }[]) => void, initStringPath: { index: number, stringPath: string }[] }) {
    const [existsDevice, setExistsDevice] = useState(MapCoreData.device ? true : false);
    const [resourceLocationType, setResourceLocationType] = useState(MapCore.IMcMapDevice.EResourceLocationType.ERLT_FOLDER);
    const [groupName, setGroupName] = useState('');
    const [stringPath, setStringPath] = useState<{ index: number, stringPath: string }[]>(props.initStringPath);
    const [count, setCount] = useState<number>(0);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    const [enumDetails] = useState({
        EResourceLocationType: getEnumDetailsList(MapCore.IMcMapDevice.EResourceLocationType),
    });

    useEffect(() => {
        props.getStringPath(stringPath)
    }, [stringPath])

    const loadStringPath = () => {
        runCodeSafely(() => {
            runMapCoreSafely(() => {
                MapCore.IMcMapDevice.LoadResourceGroup(groupName, stringPath.map(s => s.stringPath), resourceLocationType)
            }, 'loadStringPath=> MapCore.IMcMapDevice.LoadResourceGroup', true)
        }, "DeviceOperations=> loadStringPath")
    }
    const unLoadStringPath = () => {
        runCodeSafely(() => {
            runMapCoreSafely(() => {
                MapCore.IMcMapDevice.UnloadResourceGroup(groupName)
            }, 'unLoadStringPath=> MapCore.IMcMapDevice.UnloadResourceGroup', true)
        }, "DeviceOperations=> unLoadStringPath")
    }

    const switchResourceLocationType = (event) => {
        runCodeSafely(() => {
            if (stringPath.length > 0) {
                let result = window.confirm("This change delete table rows, Do you want to continue? ");
                if (result) {
                    setResourceLocationType(event.target.value.theEnum);
                    setStringPath([])
                }
            }
            else
                setResourceLocationType(event.target.value.theEnum);
        }, 'DeviceOperations=> switchResourceLocationType')
    }
    return (<div className={` ${!existsDevice && "form__disabled"}`}>
        <Fieldset legend="Load Resource Group " className="form__column-fieldset" >
            <div className='object-props__flex-and-row-between'>
                <label>Resource Location Type: </label>
                <Dropdown className="object-props__dropdown" name="resourceLocationType" value={getEnumValueDetails(resourceLocationType, enumDetails.EResourceLocationType)}
                    onChange={switchResourceLocationType} options={enumDetails.EResourceLocationType} optionLabel="name" />
            </div>
            <div className='object-props__flex-and-row-between'>
                <label >Group Name:</label>
                <InputText value={groupName} onChange={(e) => { setGroupName(e.target.value) }}></InputText>
            </div>
            <DataTable size="small" value={stringPath} editMode="cell" style={{ overflow: 'auto', height: `${globalSizeFactor * 10}vh` }}>
                <Column header={<Button onClick={() => { setStringPath([...stringPath, { index: count, stringPath: "" }]); setCount(count + 1) }}>Add Path</Button>}
                    body={(rowData, bodyOptions) => {
                        return <div><UploadFilesCtrl
                            isDirectoryUpload={(resourceLocationType.constructor.name).includes("FOLDER") ? true : false}
                            accept={(resourceLocationType.constructor.name).includes("FOLDER") ? "directory" : ".zip"}
                            uploadOptions={[UploadTypeEnum.upload]} existInputValue={rowData.stringPath}
                            getVirtualFSPath={(virtualFSPath, selectedOption) => {
                                rowData.stringPath = virtualFSPath;
                            }}  ></UploadFilesCtrl></div>
                    }}
                ></Column>
                <Column body={(rowData, f) => {
                    return <div><Button label="Delete"
                        onClick={() => {
                            setStringPath(stringPath.filter(u => u != rowData))
                        }}></Button></div>
                }}></Column>
            </DataTable>
            <div style={{ direction: 'rtl' }}><Button onClick={loadStringPath}>Load</Button></div>
        </Fieldset>
        <Fieldset legend="Unload Resource Group" style={{ justifyContent: 'space-between' }}>
            <div >   <label >Group Name:</label>
                <InputText value={groupName} onChange={(e) => { setGroupName(e.target.value) }}></InputText></div>
            <Button onClick={unLoadStringPath}>Unload</Button>
        </Fieldset>
    </div>
    )
}