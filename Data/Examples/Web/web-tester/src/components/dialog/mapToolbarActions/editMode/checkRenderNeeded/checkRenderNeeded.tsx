import { Column } from 'primereact/column';
import { DataTable } from 'primereact/datatable';
import React, { ReactElement, useEffect, useRef, useState } from 'react';
import { EditModeService } from 'mapcore-lib';
import { useSelector } from 'react-redux';
import { AppState } from '../../../../../redux/combineReducer';
import './styles/checkRenderNeeded.css';
import { Button } from 'primereact/button';
import editModeReducer from '../../../../../redux/EditMode/editModeReducer';

export default function CheckRenderNeeded(props: { footerHook: (footer: () => ReactElement) => void }) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [list, setList] = useState(EditModeService.listRenderNeeded)
    let lastMessageRef = useRef(null);

    useEffect(() => {
        let interval = setInterval(() => {
            setList([...EditModeService.listRenderNeeded]);
        }, 300)
        return () => {
            clearInterval(interval);
            EditModeService.clearList()
        }
    }, [])
    useEffect(() => {
        lastMessageRef?.current?.scrollIntoView({
            behavior: 'smooth'
        })
    })
    useEffect(() => {
        setCheckRenderNeededDialogWidth();
        props.footerHook(getFooter)
        EditModeService.clearList()
    }, [])

    function setCheckRenderNeededDialogWidth() {
        const root = document.documentElement;
        let pixelWidth = window.innerHeight * 0.35 * globalSizeFactor;
        root.style.setProperty('--check-render-needed-dialog-width', `${pixelWidth}px`);
    }
    const getFooter = () => {
        return <div className='form__footer-padding' >
            <Button label='Clear' onClick={() => { EditModeService.clearList() }}></Button>
        </div>
    }

    return (<div className="check-render-needed__table" >
        <DataTable style={{ overflowY: 'auto', height: `${globalSizeFactor * 18}vh` }} value={list} className="check-render-needed__multi-line" showGridlines size="small" tableStyle={{ minWidth: '22rem', maxWidth: '22rem' }}>
            <Column header="Line Number" field="index"></Column>
            <Column header="Render Needed Result" field="renderNeededResult"></Column>
            <Column header="Counter" field="counter" body={(rowData) => { return <div ref={rowData.index == list.length - 1 ? lastMessageRef : null}> {rowData.counter}</div> }} ></Column>
        </DataTable>
    </div>
    );
}
