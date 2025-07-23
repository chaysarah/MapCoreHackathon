import { Button } from "primereact/button";
import { Dialog } from "primereact/dialog";
import { useEffect, useRef, useState } from "react";
import TextureDialog, { TextureDialogActionMode } from "../mapToolbarActions/symbolicItemsDialogs/texture/textureDialog";
import TexturePropertiesBase from "../../../propertiesBase/texturePropertiesBase";
import { InputText } from "primereact/inputtext";
import { ConfirmDialog } from "primereact/confirmdialog";
import { useSelector } from "react-redux";
import { AppState } from "../../../redux/combineReducer";

export default function EditTextureCtrl(props: { value: any, onOk: (newValue: any) => void, label: string, texturePropertiesBase: TexturePropertiesBase, saveTexturePropertiesCB: (textureProp: TexturePropertiesBase) => void }) {

    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    let [value, setValue] = useState(props.value);
    let [openDialog, setOpenDialog] = useState(false);
    let [isConfirmDialogVisible, setIsConfirmDialogVisible] = useState<boolean>(false);
    let [actionMode, setActionMode] = useState<TextureDialogActionMode>();
    let [textureFooter, setTextureFooter] = useState(<div></div>);

    const handleTextureClose = (texture: MapCore.IMcTexture) => {
        setOpenDialog(false);
        setValue(texture);
        props.onOk(texture);
    }
    const handleButtonClick = (localActionMode: TextureDialogActionMode) => {
        setActionMode(localActionMode);
        setOpenDialog(true);
    }
    const handleDeleteClick = () => {
        setValue(null);
        props.onOk(null);
    }
    const getConfirmDialogMessage = () => {
        return <div style={{ whiteSpace: 'pre-line' }}>
            {`The update will be performed in-place influencing other properties using the same texture and canceling use-existing option.\nRe-creating should be used to update this property only and preserve use-existing option.\nAre you sure you want to continue updating in-place?`}
        </div>
    }
    const getConfirmDialogFooter = () => {
        return <div>
            <Button label="No" onClick={e => { setIsConfirmDialogVisible(false) }} />
            <Button label="Yes" onClick={e => {
                setIsConfirmDialogVisible(false);
                handleButtonClick(TextureDialogActionMode.update);
            }} />
        </div>
    }

    return (<div style={{ display: 'flex', flexDirection: 'column' }}>
        <div style={{ display: "flex", flexDirection: "row", justifyContent: 'space-between' }}>
            <div>{props.label}</div>
            <span className="form__row-container">
                <Button label="Create" onClick={() => handleButtonClick(TextureDialogActionMode.create)} />
                <Button label="Re-Create" disabled={value == null} onClick={() => handleButtonClick(TextureDialogActionMode.reCreate)} />
                <Button label="Update" disabled={value == null} onClick={() => setIsConfirmDialogVisible(true)} />
                <Button label="Delete" disabled={value == null} onClick={handleDeleteClick} />
            </span>
        </div>
        {value !== null && <div style={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between', marginBottom: `${globalSizeFactor * 0.75}vh` }}>
            <label>Default Texture Type:</label>
            <InputText disabled value={value?.constructor.name} />
        </div>}

        <Dialog className="scroll-dialog-3" footer={textureFooter} onHide={() => { setOpenDialog(false) }} visible={openDialog} header="Texture">
            <TextureDialog
                footerHook={footer => {
                    setTextureFooter(footer())
                }}
                defaultTexture={TextureDialogActionMode.create == actionMode ? null : value}
                textureClose={handleTextureClose}
                isSetAsDefault={false}
                actionMode={actionMode}
                texturePropertiesBase={props.texturePropertiesBase}
                saveTexturePropertiesCB={props.saveTexturePropertiesCB}
            />
        </Dialog>

        <ConfirmDialog
            contentClassName='form__confirm-dialog-content'
            message={getConfirmDialogMessage()}
            header={'Update Texture'}
            footer={getConfirmDialogFooter()}
            visible={isConfirmDialogVisible}
            onHide={e => { setIsConfirmDialogVisible(false) }}
        />
    </div>
    );
}
