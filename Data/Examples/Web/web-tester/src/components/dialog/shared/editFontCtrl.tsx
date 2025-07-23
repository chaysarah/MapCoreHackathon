import { Button } from "primereact/button";
import { Dialog } from "primereact/dialog";
import { useEffect, useState } from "react";
import Font, { FontDialogActionMode } from "../mapToolbarActions/symbolicItemsDialogs/font/font";
import { InputText } from "primereact/inputtext";
import { ConfirmDialog } from "primereact/confirmdialog";
import { runCodeSafely } from "../../../common/services/error-handling/errorHandler";
import { useSelector } from "react-redux";
import { AppState } from "../../../redux/combineReducer";

export default function EditFontCtrl(props: { defaultFont: MapCore.IMcFont, handleGetFont: (font: MapCore.IMcFont, isSetAsDefault: boolean) => void, label: string }) {

    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    let [value, setValue] = useState<MapCore.IMcFont>(props.defaultFont);
    let [openDialog, setOpenDialog] = useState(false);
    let [actionMode, setActionMode] = useState<FontDialogActionMode>();
    let [isConfirmDialogVisible, setIsConfirmDialogVisible] = useState<boolean>(false);

    const handleLocalGetFont = (font: MapCore.IMcFont, isSetAsDefault: boolean) => {
        runCodeSafely(() => {
            setOpenDialog(false);
            setValue(font);
            props.handleGetFont(font, isSetAsDefault);
        }, 'editFontCtrl.handleLocalGetFont')
    }
    const handleButtonClick = (localActionMode: FontDialogActionMode) => {
        runCodeSafely(() => {
            setActionMode(localActionMode);
            setOpenDialog(true);
        }, 'editFontCtrl.handleButtonClick')
    }
    const handleDeleteClick = () => {
        runCodeSafely(() => {
            setValue(null);
            props.handleGetFont(null, false);
        }, 'editFontCtrl.handleDeleteClick')
    }
    const getConfirmDialogFooter = () => {
        return <div>
            <Button label="No" onClick={e => { setIsConfirmDialogVisible(false) }} />
            <Button label="Yes" onClick={e => {
                setIsConfirmDialogVisible(false);
                handleButtonClick(FontDialogActionMode.update);
            }} />
        </div>
    }
    const getConfirmDialogMessage = () => {
        return <div style={{ whiteSpace: 'pre-line' }}>
            {`The update will be performed in-place influencing other properties using the same font and canceling use-existing option.\nRe-creating should be used to update this property only and preserve use-existing option.\nAre you sure you want to continue updating in-place?`}
        </div>
    }

    return (<div style={{ display: 'flex', flexDirection: 'column' }}>
        <div style={{ display: "flex", flexDirection: "row", justifyContent: 'space-between' }}>
            <div>{props.label}</div>
            <span>
                <Button label="Create" onClick={() => handleButtonClick(FontDialogActionMode.create)} />
                <Button label="Re-Create" disabled={value == null} onClick={() => handleButtonClick(FontDialogActionMode.reCreate)} />
                <Button label="Update" disabled={value == null} onClick={() => setIsConfirmDialogVisible(true)} />
                <Button label="Delete" disabled={value == null} onClick={handleDeleteClick} />
            </span>
        </div>
        {value !== null && <div style={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between', marginBottom: `${globalSizeFactor * 0.75}vh` }}>
            <label>Default Font Type:</label>
            <InputText disabled value={value?.constructor.name} />
        </div>}

        <Dialog className="scroll-dialog-10" onHide={() => { setOpenDialog(false) }} visible={openDialog} header="Texture">
            <Font
                defaultFont={FontDialogActionMode.create == actionMode ? null : value as MapCore.IMcFileFont}
                getFont={handleLocalGetFont}
                isSetAsDefaultCheckbox={false}
                actionMode={actionMode}
            />
        </Dialog>

        <ConfirmDialog
            contentClassName='form__confirm-dialog-content'
            message={getConfirmDialogMessage()}
            header={'Update Font'}
            footer={getConfirmDialogFooter()}
            visible={isConfirmDialogVisible}
            onHide={e => { setIsConfirmDialogVisible(false) }}
        />
    </div>
    );
}
