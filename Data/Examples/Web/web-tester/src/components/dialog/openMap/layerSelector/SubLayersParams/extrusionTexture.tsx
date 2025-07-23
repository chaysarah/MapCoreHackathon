import { ChangeEvent, useEffect, useState } from "react";
import { Button } from "primereact/button";
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import { ListBox } from "primereact/listbox";
import Vector2DFromMap from "../../../treeView/objectWorldTree/shared/Vector2DFromMap";
import { InputText } from "primereact/inputtext";
import { Checkbox } from "primereact/checkbox";
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";
import objectWorldTreeService from "../../../../../services/objectWorldTree.service";

export default function ExtrusionTexture(props: { getExtrusionTexture: (ExtrusionTexture: MapCore.IMcRawVector3DExtrusionMapLayer.SExtrusionTexture) => void ,initExtrusion:MapCore.IMcRawVector3DExtrusionMapLayer.SExtrusionTexture}) {
    const [extrusionTexture, setExtrusionTexture] = useState<MapCore.IMcRawVector3DExtrusionMapLayer.SExtrusionTexture>(new MapCore.IMcRawVector3DExtrusionMapLayer.SExtrusionTexture())
    const [enumDetails] = useState({
        TexturePlacementFlags: getEnumDetailsList(MapCore.IMcRawVector3DExtrusionMapLayer.ETexturePlacementFlags),
    });
    useEffect(() => { if(props.initExtrusion)setExtrusionTexture(props.initExtrusion)}, [])
    useEffect(() => { props.getExtrusionTexture(extrusionTexture) }, [extrusionTexture])

    const isFlagSet = (flags: number, flagToCheck: number) => {
        return (flags & flagToCheck) != 0;
    }
    const addFlag = (e, code, placement: string) => {
        if (code == 0) {
            setExtrusionTexture({ ...extrusionTexture, [placement]: 0 });
            return;
        }
        let bitField = extrusionTexture[placement];
        if (e.checked == true)
            bitField |= code;
        else
            bitField &= ~code;

        setExtrusionTexture({ ...extrusionTexture, [placement]: bitField })
    }

    const handleDirectorySelected = (event: ChangeEvent<HTMLInputElement>) => {
        runCodeSafely(() => {
            const selectedFiles = event.target.files;
            let selectedFilesArr = Array.from(selectedFiles);
            let filePaths: string[] = selectedFilesArr.map((f: any) => f.webkitRelativePath.substring(f.webkitRelativePath.indexOf('/') + 1));
            let directories = objectWorldTreeService.getDirectoriesFromFilesPaths(filePaths);
            let DirRoot = selectedFilesArr[0].webkitRelativePath.split('/')[0];
            directories.map(dir => {
                let name = DirRoot + "/" + dir;
                return name
            });
            directories.unshift(DirRoot)
            directories.forEach(dir => {
                MapCore.IMcMapDevice.CreateFileSystemDirectory(dir);
            });
            for (let i = 0; i < selectedFiles.length; i++) {
                const reader = new FileReader();
                reader.onload = (event) => {
                    const arrayBuffer = event.target.result;
                    const uint8Array = new Uint8Array(arrayBuffer as ArrayBuffer);
                    let path = selectedFiles[i].webkitRelativePath;
                    MapCore.IMcMapDevice.CreateFileSystemFile(path, uint8Array);
                };
                reader.readAsArrayBuffer(selectedFiles[i]);
            }
           setExtrusionTexture({...extrusionTexture,strTexturePath:directories[0]})
        }, "overlayForm.handleFileUpload => onChange")
    }
    return (<div >
        <div style={{ display: 'flex' }}>
            <label style={{ whiteSpace: 'nowrap' }}>Data Source</label>
            <InputText style={{ width: '80%' }} value={extrusionTexture.strTexturePath}/>
            <input
                type="file"
                // @ts-ignore
                webkitdirectory=""
                // @ts-ignore
                directory=""
                id="directoryNameDots"
                accept='directory'
                style={{ display: 'none' }}
                onChange={(e) => { handleDirectorySelected(e)}} multiple
            />
            <Button style={{ margin: '0.3vh', height: '2.2vh' }} label="..." onClick={() => {
                document.getElementById('directoryNameDots').click();
            }} />
        </div>
        <Vector2DFromMap initValue={new MapCore.SMcFVector2D(0, 0)} name="Texture Scale"
            pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} saveTheValue={(scale) => { setExtrusionTexture({ ...extrusionTexture, TextureScale: scale }) }}></Vector2DFromMap>
        <div style={{ display: 'flex' }}>
            <div style={{ marginRight: '5px', marginLeft: '5px' }}>
                <label>X Placement</label>
                <ListBox optionLabel="name" options={enumDetails.TexturePlacementFlags}
                    itemTemplate={(item) => {
                        return <div><Checkbox onChange={(e) => { addFlag(e, item.code, "uXPlacementBitField") }} checked={isFlagSet(extrusionTexture.uXPlacementBitField, item.code)} style={{ marginRight: '2px' }}></Checkbox>
                            <label className="ml-2">{item.name}</label> </div>
                    }}> </ListBox>
            </div>
            <div>
                <label>Y Placement</label>
                <ListBox optionLabel="name" options={enumDetails.TexturePlacementFlags}
                    itemTemplate={(item) => {
                        return <div><Checkbox onChange={(e) => addFlag(e, item.code, "uYPlacementBitField")} checked={isFlagSet(extrusionTexture.uYPlacementBitField, item.code)} style={{ marginRight: '2px' }}></Checkbox>
                            <label className="ml-2">{item.name}</label> </div>
                    }} >  </ListBox>
            </div>
        </div>
    </div>
    )
}