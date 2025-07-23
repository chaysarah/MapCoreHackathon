import { Button } from 'primereact/button';
import { Checkbox } from 'primereact/checkbox';
import { InputNumber } from 'primereact/inputnumber';
import { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { ObjectWorldService } from 'mapcore-lib';
import { ScanService, MapCoreData } from 'mapcore-lib';
import { AppState } from '../../../../../../redux/combineReducer';
import { runCodeSafely } from '../../../../../../common/services/error-handling/errorHandler';
import { ViewportData } from "mapcore-lib";
import generalService from '../../../../../../services/general.service';
import { setallPolygonContoursArr } from '../../../../../../redux/MapCore/mapCoreAction';
import { Fieldset } from 'primereact/fieldset';
import ColorPickerCtrl from '../../../../../shared/colorPicker';

export default function EditColorProperties(props: { setParent: any }) {
    const [targetFound, setTargetFound] = useState<MapCore.IMcSpatialQueries.STargetFound[]>(MapCoreData.TargetFound);
    const [AddContours, setAddContours] = useState(generalService.ScanPropertiesBase.SpatialQueryParams.bAddStaticObjectContours);
    const [mVector3DExtrusionOldColors, setmVector3DExtrusionOldColors] = useState<MapCore.IMcStaticObjectsMapLayer.SObjectColor[]>([])
    const [PolygonContoursArr, setPolygonContoursArr] = useState([])
    let allPolygonContoursArr = useSelector((state: AppState) => state.mapCoreReducer.allPolygonContoursArr);
    const activeCard = useSelector((state: AppState) => state.mapWindowReducer.activeCard);
    const viewportData: ViewportData = MapCoreData.findViewport(activeCard);
    const activeOverlay = ObjectWorldService.findActiveOverlayByMcViewport(viewportData.viewport);
    const dispatch = useDispatch();
    const [Theinterval, setTheInterval] = useState<NodeJS.Timer>(null);
    let flagColor: boolean = false
    const [formObjectProperties, setFormObjectProperties] = useState({
        colorProperties: new MapCore.SMcBColor(225, 0, 0, 0),
        colorPropertiesAlpha: 255,
        colorContours: new MapCore.SMcBColor(0, 0, 0, 0),
        colorContoursAlpha: 255,
        resetOnClosure: true,
    });

    useEffect(() => {
        runCodeSafely(() => {
            props.setParent(() => { RemoveColor() });
        }, "EditColorProperties.useEffect of formObjectProperties.resetOnClosure")
    }, [formObjectProperties.resetOnClosure, PolygonContoursArr, allPolygonContoursArr])

    useEffect(() => {
        runCodeSafely(() => {
            CreateVector3DExtrusionOldColors()
            SetColor()
        }, "EditColorProperties.useEffect")
        return () => {
            runCodeSafely(() => {
                stopBlink()
            }, "EditColorProperties.useEffect")
        }
    }, [])

    const CreateVector3DExtrusionOldColors = () => {
        let mVector: any = []
        targetFound.forEach(target => {
            let staticMapLayer = ScanService.ifVector3DExtrusion(target.pTerrainLayer)|| ScanService.if3DModel(target.pTerrainLayer)
            if (target.eTargetType == MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_STATIC_OBJECTS_LAYER &&
                staticMapLayer) {
                let objColor: MapCore.IMcStaticObjectsMapLayer.SObjectColor = new MapCore.IMcStaticObjectsMapLayer.SObjectColor();
                objColor.uObjectID = target.uTargetID
                objColor.Color = staticMapLayer.GetObjectColor(target.uTargetID)
                setmVector3DExtrusionOldColors([...mVector3DExtrusionOldColors, objColor])
                mVector3DExtrusionOldColors.push(objColor)
            }
        });
    }

    const saveData = (event: any) => {
        setFormObjectProperties({ ...formObjectProperties, [event.target.name]: event.target.type === "checkbox" ? event.target.checked : event.target.value })
    }

    const SetColor = () => {
        flagColor = true;
        runCodeSafely(() => {
            let mMcColor = new MapCore.SMcBColor(formObjectProperties.colorProperties.r, formObjectProperties.colorProperties.g, formObjectProperties.colorProperties.b, formObjectProperties.colorPropertiesAlpha);
            ScanService.SetColor(mMcColor)
        }, "EditColorProperties.SetColor")
    }
    const RemoveColor = () => {
        runCodeSafely(() => {
            flagColor = false;
            stopBlink();
            if (formObjectProperties.resetOnClosure == true) {
                RemoveContoursColor()
                ScanService.RemoveColor(mVector3DExtrusionOldColors)
            }
        }, "EditColorProperties.RemoveColor")
    }

    const SetContoursColor = () => {
        let arr = [...PolygonContoursArr]
        let allarr = [...allPolygonContoursArr]
        runCodeSafely(() => {
            if (AddContours) {
                let ColorsContours: MapCore.SMcBColor = new MapCore.SMcBColor(formObjectProperties.colorContours.r,
                    formObjectProperties.colorContours.g,
                    formObjectProperties.colorContours.b,
                    formObjectProperties.colorContoursAlpha)
                targetFound.forEach(target => {
                    target.aStaticObjectContours.forEach((c: MapCore.IMcSpatialQueries.SStaticObjectContour) => {
                        let obj = ScanService.SetContoursColor(ColorsContours, c, activeOverlay)
                        arr.push(obj);
                        allarr.push(obj)
                    })
                })
            }
        }, "EditColorProperties.SetContoursColor")
        setPolygonContoursArr(arr)
        dispatch(setallPolygonContoursArr(allarr))
    }

    const RemoveContoursColor = () => {
        let arr = [...allPolygonContoursArr];
        arr = arr.filter(i => !PolygonContoursArr.includes(i))
        if (PolygonContoursArr.length > 0)
            PolygonContoursArr.forEach(
                o => {
                    o?.Remove();
                }
            )
        dispatch(setallPolygonContoursArr(arr))
        setPolygonContoursArr([])
    }

    const RemoveAllColor = () => {
        runCodeSafely(() => {
            stopBlink();
            allPolygonContoursArr.forEach(
                (o) => {
                    o.Remove()
                })
            dispatch(setallPolygonContoursArr([]))
            setPolygonContoursArr([])
            ScanService.RemoveAllColor()
        }, "EditColorProperties.RemoveAllColor")
    }

    const Blink = () => {
        runCodeSafely(() => {
            let i;
            stopBlink()
            if (Theinterval == null) {
                i = setInterval(() => {
                    if (flagColor)
                        RemoveColor()
                    else {
                        SetContoursColor();
                        SetColor()
                    }
                }, 1000)
                setTheInterval(i)
            }
        }, "EditColorProperties.Blink")
    }
    const stopBlink = () => {
        if (Theinterval) {
            clearInterval(Theinterval);
            setTheInterval(null)
        }
    }

    return (
        <div className='scan__edit-color-properties'>
            <Button className='scan__icon-button' onClick={() => { SetColor(); SetContoursColor() }}>
                <img src="http:mctester icons/tsbSetColor.Image.png"
                    alt="custom icon"
                    className='scan__icon-img' />
                Set Color
            </Button>
            <Button className='scan__icon-button' onClick={RemoveColor}>
                <img src="http:mctester icons/tsbRemoveColor.Image.png"
                    alt="custom icon"
                    className='scan__icon-img' />
                Reset
            </Button>
            <Button className='scan__icon-button' onClick={Blink}>
                <img src="http:mctester icons/tsbBlink.Image.png"
                    alt="custom icon"
                    className='scan__icon-img' />
                Blink
            </Button>
            <Button className='scan__icon-button' onClick={RemoveAllColor}>
                <img src="http:mctester icons/tsbRemoveAll.Image.png"
                    alt="custom icon"
                    className='scan__icon-img' />
                Reset All Colors
            </Button>
            <Fieldset className='scan__color-picker' legend="Color Properties:">
                <div className='form__center-aligned-row'>
                    <ColorPickerCtrl name='colorProperties' value={formObjectProperties.colorProperties} onChange={saveData} />
                    <label >Alpha:</label>
                    <InputNumber name='colorPropertiesAlpha' className='form__slim-input' mode="decimal"
                        value={formObjectProperties.colorPropertiesAlpha} onValueChange={(e) => saveData(e)} />
                </div>
            </Fieldset>
            <Fieldset className={"scan__color-picker" + (AddContours == true ? "" : "--disabled")} legend="Static Objects Contours Properties:">
                <div className='form__center-aligned-row'>
                    <ColorPickerCtrl name='colorContours' value={formObjectProperties.colorContours} onChange={saveData} />
                    <label >Alpha:</label>
                    <InputNumber name='colorContoursAlpha' className='form__slim-input' mode="decimal"
                        value={formObjectProperties.colorContoursAlpha} onValueChange={(e) => saveData(e)} />
                </div>
            </Fieldset>
            <div className='form__center-aligned-row'>
                <Checkbox onChange={saveData} name='resetOnClosure' checked={formObjectProperties.resetOnClosure}></Checkbox>
                <label> Reset color on closure</label>
            </div>
        </div>
    );
}