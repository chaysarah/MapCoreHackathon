import React, { useEffect, useState } from 'react';
import { enums } from 'mapcore-lib'
import { useSelector } from 'react-redux';
import { AppState } from '../../redux/combineReducer';
import { DialogTypesEnum } from '../../tools/enum/enums';

export default function Header(props: { dialogType: DialogTypesEnum }) {

    const renderDialogHeader = () => {
        switch (props.dialogType) {
            case DialogTypesEnum.chooseLayers:
                return (
                    <b>Open Map</b>
                );
            case DialogTypesEnum.objectProperties:
                return (
                    <b>Object Properties</b>
                );
            case DialogTypesEnum.createTexture:
                return (
                    <b>Texture</b>
                );
            case DialogTypesEnum.addJsonViewport:
                return (
                    <b>Adding a viewport using a json file</b>
                );
            case DialogTypesEnum.scanGeometry:
                return (
                    <b>Scan Geometry</b>
                );
            case DialogTypesEnum.scanResult:
                return (
                    <b>Scan Result</b>
                );
            case DialogTypesEnum.mapWorldTree:
                return (
                    <b>Map World</b>
                );
            case DialogTypesEnum.objectWorldTree:
                return (
                    <b>Object World</b>
                );
            case DialogTypesEnum.font:
                return (
                    <b>Font</b>
                );
            case DialogTypesEnum.editModeProperties:
                return (
                    <b>Edit Mode Properties</b>
                );
            case DialogTypesEnum.editObject:
                return (
                    <b>Items List</b>
                );
            case DialogTypesEnum.initObject:
                return (
                    <b>Items List</b>
                );
            case DialogTypesEnum.checkRenderNeeded:
                return (
                    <b>Check Render Needed</b>
                )
            case DialogTypesEnum.font:
                return (
                    <b>Font</b>
                );
            case DialogTypesEnum.eventCallback:
                return (
                    <b>Event Callback</b>
                );
            case DialogTypesEnum.addMapManuly:
                return (
                    <b>Open Map Manully</b>
                );
            case DialogTypesEnum.standAloneSpatialQueries:
                return (
                    <b>Stand Alone Spatial Queries</b>
                );
            case DialogTypesEnum.printMap:
                return (
                    <b>Print Map</b>
                );
            default:
                break;
        }
    }

    return (
        <div>
            {renderDialogHeader()}
        </div >
    )
}