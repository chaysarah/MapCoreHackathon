import React, { useState } from 'react';
import { Button } from 'primereact/button';

interface footer {
    label: string,
    onOk: () => void;
}
export default function Footer(props: { onOk: () => void, onCancel?: () => void, label: string }) {
    const renderDialogFooter = () => {
        return (
            <div className='form__footer-dialog'>
                {props.onCancel && <Button label="Cancel" onClick={() => props.onCancel()} className="p-button-text" />}
                <Button label={props.label} onClick={() => props.onOk()} />
            </div>
        );
    }
    return (
        <div>
            {renderDialogFooter()}
        </div>
    )
}