import { Button } from 'primereact/button';
import { Checkbox } from 'primereact/checkbox';
import { InputNumber } from 'primereact/inputnumber';
import { useEffect, useState } from 'react';
import { ScanService, MapCoreData } from 'mapcore-lib';
import { runCodeSafely } from '../../../../../../common/services/error-handling/errorHandler';
import { getEnumDetailsList, getEnumValueDetails } from 'mapcore-lib';
import { Fieldset } from 'primereact/fieldset';


export default function EditObjectHeightProperties(props: { setParent: any }) {
    const [targetFound, setTargetFound] = useState<MapCore.IMcSpatialQueries.STargetFound[]>(MapCoreData.TargetFound);
    const [heightChange, setHeightChange] = useState<Boolean>(false);
    const [formObjectProperties, setFormObjectProperties] = useState({
        heightProperties: null,
        resetOnClosure: true
    });
    const [mVector3DExtrusionOldHeight, setmVector3DExtrusionOldHeight] = useState<number[]>([])

    useEffect(() => {
        runCodeSafely(() => {
            CreateVector3DExtrusionOldHeight();
        }, "EditObjectHeightProperties.useEffect")
    }, [])

    useEffect(() => {
        runCodeSafely(() => { props.setParent(RemoveHeight) }, "EditObjectHeightProperties.useEffect of formObjectProperties.resetOnClosure")
    }, [formObjectProperties.resetOnClosure])

    const CreateVector3DExtrusionOldHeight = () => {
        targetFound.forEach(target => {
            let staticMapLayer: MapCore.IMcVector3DExtrusionMapLayer = ScanService.ifVector3DExtrusion(target.pTerrainLayer)
            if (target.eTargetType == MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_STATIC_OBJECTS_LAYER &&
                staticMapLayer) {
                let height: number = staticMapLayer.GetObjectExtrusionHeight(target.uTargetID);
                setmVector3DExtrusionOldHeight([...mVector3DExtrusionOldHeight, height])
                mVector3DExtrusionOldHeight.push(height)
            }
        });
    }

    const saveData = (event: any) => {
        setFormObjectProperties({ ...formObjectProperties, [event.target.name]: event.target.type === "checkbox" ? event.target.checked : event.target.value })
    }

    const SetHeight = () => {
        runCodeSafely(() => { setHeightChange(ScanService.SetHeight(formObjectProperties.heightProperties)); }, "EditObjectHeightProperties.setHeight")
    }

    const RemoveHeight = () => {
        runCodeSafely(() => {
            if (heightChange == true)
                if (formObjectProperties.resetOnClosure == true) {
                    ScanService.RemoveHeight(mVector3DExtrusionOldHeight)
                }
        }, "EditObjectHeightProperties.RemoveHeight")
    }
    const RemoveAllHeight = () => {
        runCodeSafely(() => {
            ScanService.RemoveAllHeight()
        }, "EditObjectHeightProperties.RemoveAllHeight")
    }

    return (
        <div className='scan__edit-object-height-properties'>
            <Fieldset legend="Static Objects Height:">
                <div className='form__center-aligned-row'>
                    <label >Height:</label>
                    <InputNumber name='heightProperties' className='form__medium-width-input' mode="decimal"
                        value={formObjectProperties.heightProperties} onValueChange={(e) => saveData(e)} />
                </div>
                <div className='form__center-aligned-row'>
                    <div className='scan__button-segment'>
                        <div className='form__center-aligned-row'>
                            <Button onClick={SetHeight}>Set </Button>
                            <Button onClick={RemoveHeight}  >Reset </Button>
                        </div>
                        <Button onClick={RemoveAllHeight}  >Reset all height</Button>
                    </div>
                </div>
                <div className='form__center-aligned-row'>
                    <Checkbox onChange={saveData} name='resetOnClosure' checked={formObjectProperties.resetOnClosure}></Checkbox>
                    <label> Reset height on closure</label>
                </div>
            </Fieldset>
        </div>
    );
}









