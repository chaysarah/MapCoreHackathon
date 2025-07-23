import { useDispatch, useSelector } from 'react-redux';
import './styles/footer.css'
import { AppState } from '../../../redux/combineReducer';
import { MapCoreData } from 'mapcore-lib';
import { InputSwitch } from 'primereact/inputswitch';
import { useState } from 'react';
import { setIsCatchErrors } from '../../../redux/MapCore/mapCoreAction';
export default function Footer() {
    const dispatch = useDispatch();
    const cursorPos: any = useSelector((state: AppState) => state.mapWindowReducer.cursorPos);
    const ScaleBox: number = useSelector((state: AppState) => state.mapWindowReducer.scaleBox);
    const MapScaleBox: number = useSelector((state: AppState) => state.mapWindowReducer.mapScaleBox);
    const screenPos: MapCore.SMcVector3D = useSelector((state: AppState) => state.mapWindowReducer.screenPos);
    const currentViewport: number = useSelector((state: AppState) => state.mapWindowReducer.currentViewport);
    const isCatchErrors: boolean = useSelector((state: AppState) => state.mapCoreReducer.isCatchErrors);


    return (
        <div className='footer__footer'>
            <div >
                {cursorPos != null ?
                    <>
                        <label>   X:  </label>
                        <label className='footer__value-footer'>{cursorPos.Value.x.toFixed(2)} </label>
                        <label>   Y:   </label>
                        <label className='footer__value-footer'>{cursorPos.Value.y.toFixed(2)}</label>
                        <label>   Z:  </label>
                        <label className='footer__value-footer'>{cursorPos.Value.z.toFixed(2)}</label>
                        <label>   Screen:  </label>
                        <label className='footer__value-footer'> {screenPos.x}/{screenPos.y}</label>
                        <label>   Image:  </label>
                        <label className='footer__value-footer'></label>
                        <label>   ViewPort Id:  </label>
                        <label className='footer__value-footer'>{MapCoreData.findViewport(currentViewport)?.viewport?.GetViewportID()}</label>
                        <label>   Camera Scale:  </label>
                        <label className='footer__value-footer'>1:{ScaleBox} </label>
                        <label>   Map Scale:  </label>
                        {MapScaleBox && <label className='footer__value-footer'>1:{MapScaleBox.toFixed(2)} </label>}
                    </> : ""}

            </div>
            <div style={{display:'flex'}} className='rrr'>
                <label>Catch Errors</label>
                <InputSwitch checked={isCatchErrors} onChange={(e) => {
                     dispatch(setIsCatchErrors(e.value));
                  }} />
            </div>
        </div>
    );
}






