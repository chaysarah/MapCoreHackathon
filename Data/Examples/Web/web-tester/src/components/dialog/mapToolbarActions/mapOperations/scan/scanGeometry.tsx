import { ReactElement, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import './styles/scan.css';
import Footer from "../../../footerDialog";
import { RadioButton } from "primereact/radiobutton";
import { InputNumber } from "primereact/inputnumber";
import { AppState } from "../../../../../redux/combineReducer";
import { MapCoreData } from "mapcore-lib";
import { ViewportData } from "mapcore-lib";
import { closeDialog, setDialogType } from "../../../../../redux/MapCore/mapCoreAction";
import { ScanService } from "mapcore-lib";
import { DialogTypesEnum } from "../../../../../tools/enum/enums";
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";
import { Button } from "primereact/button";
import { Checkbox } from "primereact/checkbox";
import { Dialog } from "primereact/dialog";
import ScanSQParams from "./scanSQParams";
import { Dropdown } from "primereact/dropdown";
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import generalService from "../../../../../services/general.service";
import { Fieldset } from "primereact/fieldset";

export default function ScanGeometry(props: { FooterHook: (footer: () => ReactElement) => void }) {
    const [openScanSQParams, setopenScanSQParams] = useState(false)
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const activeCard = useSelector((state: AppState) => state.mapWindowReducer.activeCard);
    const [enumDetails] = useState({
        CoordSystem: getEnumDetailsList(MapCore.EMcPointCoordSystem)
    });
    const [formData, setFormData] = useState({
        ...generalService.ScanPropertiesBase,
        coordSys: getEnumValueDetails(generalService.ScanPropertiesBase.coordSys, enumDetails.CoordSystem)
    });
    const dispatch = useDispatch();

    const restoreDefualtValues = () => {
        runCodeSafely(() => {
            setFormData({
                scanType: 'PolyScan',
                SpecificPointX: 0, SpecificPointY: 0, SpecificPointZ: 0,
                CompletelyInside: false,
                Tolerance: 0,
                coordSys: getEnumValueDetails(MapCore.EMcPointCoordSystem.EPCS_SCREEN, enumDetails.CoordSystem),
                SpatialQueryParams: new MapCore.IMcSpatialQueries.SQueryParams(),
            })
        }, "ScanGeometry.restoreDefualtValues")
    }

    const saveData = (event: any) => {
        setFormData({ ...formData, [event.target.name]: event.target.type === "checkbox" ? event.target.checked : event.target.value })
    }

    useEffect(() => {
        const root = document.documentElement;
        let pixelWidth = window.innerHeight * 0.35 * globalSizeFactor;
        root.style.setProperty('--scan-geometry-dialog-width', `${pixelWidth}px`);
    }, [])
    useEffect(() => {
        generalService.ScanPropertiesBase = { ...formData, coordSys: formData.coordSys.theEnum }
        props.FooterHook(getScanResultFooter);
    }, [formData])

    const getScanResultFooter = () => {
        return (
            <div className='form__footer-padding' style={{ padding: `0vh ${globalSizeFactor * 1.5}vh ${globalSizeFactor * 2}vh ${globalSizeFactor * 1.5}vh` }}>
                <Button label="OK" onClick={() => { runCodeSafely(onOk, "ScanGeometry.onOk") }} />
            </div>
        );
    }
    MapCoreData.whenTargetFoundFilled = () => {
        dispatch(setDialogType(DialogTypesEnum.scanResult))
    }
    const onOk = () => {
        runCodeSafely(() => {
            const viewportData: ViewportData = MapCoreData.findViewport(activeCard)
            if (viewportData) {
                if (formData.scanType == 'Pointscan') {
                    ScanService.doPointScan(formData.coordSys.theEnum, viewportData, formData.CompletelyInside
                        , formData.Tolerance, generalService.ScanPropertiesBase.SpatialQueryParams)
                    dispatch(closeDialog(DialogTypesEnum.scanGeometry))
                }
                if (formData.scanType == 'SpecificPoint') {
                    ScanService.doSpecificPoint(formData.coordSys.theEnum, viewportData, formData.CompletelyInside,
                        formData.Tolerance,
                        new MapCore.SMcVector3D(formData.SpecificPointX, formData.SpecificPointY, formData.SpecificPointZ)
                        , generalService.ScanPropertiesBase.SpatialQueryParams)
                }
                if (formData.scanType == 'PolyScan') {
                    ScanService.doPolyScan(formData.coordSys.theEnum,
                        viewportData,
                        formData.CompletelyInside,
                        formData.Tolerance,
                        generalService.ScanPropertiesBase.SpatialQueryParams
                    )
                    dispatch(closeDialog(DialogTypesEnum.scanGeometry))
                }
                if (formData.scanType == 'RectScan') {
                    ScanService.doRectScan(formData.coordSys.theEnum, viewportData,
                        formData.CompletelyInside, formData.Tolerance, generalService.ScanPropertiesBase.SpatialQueryParams)
                    dispatch(closeDialog(DialogTypesEnum.scanGeometry))
                }
            }
        }, "ScanGeometry.onOk")
    }

    return (<div className="scan__scan-geometry-form">
        <div style={{ height: '90%', margin: '4%' }}>
            <Fieldset className="form__column-fieldset" legend="Scan Type">
                <div className="form__center-aligned-row">
                    <RadioButton name='scanType' inputId="ingredient3" value="Pointscan" onChange={saveData} checked={formData.scanType === 'Pointscan'} />
                    <label htmlFor="ingredient3" className="form__radio-button-label">PointScan</label>
                </div>
                <div className="form__center-aligned-row" style={{ display: 'flex' }}  >
                    <RadioButton name='scanType' inputId="ingredient3" value="SpecificPoint" onChange={saveData} checked={formData.scanType === 'SpecificPoint'} />
                    <div className="form__center-aligned-row">
                        <label htmlFor="SpecificPointX" className="scan__coordinate-label">X </label>
                        <InputNumber name='SpecificPointX' className="form__medium-width-input" value={formData.SpecificPointX} onValueChange={saveData} useGrouping={false} maxFractionDigits={2} mode="decimal" />
                    </div>
                    <div className="form__center-aligned-row">
                        <label htmlFor="SpecificPointY" className="scan__coordinate-label">Y </label>
                        <InputNumber name='SpecificPointY' className="form__medium-width-input" value={formData.SpecificPointY} onValueChange={saveData} useGrouping={false} maxFractionDigits={2} mode="decimal" />
                    </div>
                    <div className="form__center-aligned-row">
                        <label htmlFor="SpecificPointZ" className="scan__coordinate-label">Z </label>
                        <InputNumber name='SpecificPointZ' className="form__medium-width-input" value={formData.SpecificPointZ} onValueChange={saveData} useGrouping={false} maxFractionDigits={2} mode="decimal" />
                    </div>
                </div>
                <div className="form__center-aligned-row">
                    <RadioButton name='scanType' inputId="ingredient2" value="RectScan" onChange={saveData} checked={formData.scanType === 'RectScan'} />
                    <label htmlFor="ingredient2" className="form__radio-button-label">RectScan</label>
                </div>
                <div className="form__center-aligned-row">
                    <RadioButton name='scanType' inputId="ingredient1" value="PolyScan" onChange={saveData} checked={formData.scanType === 'PolyScan'} />
                    <label htmlFor="ingredient1" className="form__radio-button-label">PolyScan</label>
                </div>
            </Fieldset>
            <div className="scan__divide-segments"></div>
            <div className="form__column-container">
                <label >Coordinate System:</label>
                <Dropdown name="coordSys" value={formData.coordSys} onChange={saveData} options={enumDetails.CoordSystem} optionLabel="name" />
                <div><Button onClick={() => { setopenScanSQParams(true) }}>SQParams</Button></div>
                <div className="form__center-aligned-row">
                    <Checkbox name="CompletelyInside" onChange={saveData} checked={formData.CompletelyInside}></Checkbox>
                    <label>Return Completely Inside Only</label>
                </div>
                <div className='form__center-aligned-row'>
                    <label > PointScan Tolerance:</label>
                    <InputNumber name="Tolerance" className="form__medium-width-input" value={formData.Tolerance} onValueChange={(e) => saveData(e)} mode="decimal" />
                </div>
                <Button className="scan__wide-button" onClick={restoreDefualtValues}>Restore Defualt Values</Button>
            </div>
        </div>

        <Dialog className="scroll-dialog-sq-params" header="Scan SQParams" visible={openScanSQParams} onHide={() => { setopenScanSQParams(false) }}>
            <ScanSQParams sqParams={generalService.ScanPropertiesBase.SpatialQueryParams} setSQParamsCallback={(sqParams: MapCore.IMcSpatialQueries.SQueryParams) => {
                generalService.ScanPropertiesBase.SpatialQueryParams = sqParams;
            }} />
            <Footer onOk={() => { setopenScanSQParams(false) }} label="OK"></Footer>
        </Dialog>
    </div>

    )
}


