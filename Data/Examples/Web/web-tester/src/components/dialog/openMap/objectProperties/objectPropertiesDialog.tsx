import React, { ReactElement, useEffect, useState } from 'react';
import './styles/objectPropertiesDialog.css';
import { TabView, TabPanel } from 'primereact/tabview';
import General from './SubObjectProperties/general';
import Arc from './SubObjectProperties/arc';
import LineBased from './SubObjectProperties/lineBased';
import Ellipse from './SubObjectProperties/ellipse';
import Text from './SubObjectProperties/text';
import CloseShape from './SubObjectProperties/closeShape';
import Arrow from './SubObjectProperties/arrow';
import Picture from './SubObjectProperties/picture';
import LineExpansion from './SubObjectProperties/lineExpansion';
import Rectangle from './SubObjectProperties/rectangle';
import SightPresentation from './SubObjectProperties/sightPresentation';
import { useDispatch, useSelector } from 'react-redux';
import { runCodeSafely } from '../../../../common/services/error-handling/errorHandler';
import generalService from '../../../../services/general.service';
import ObjectPropertiesBase from '../../../../propertiesBase/objectPropertiesBase';
import { Checkbox } from 'primereact/checkbox';
import { Button } from 'primereact/button';
import { closeDialog } from '../../../../redux/MapCore/mapCoreAction';
import { DialogTypesEnum } from '../../../../tools/enum/enums';
import { AppState } from '../../../../redux/combineReducer';

export default function OpenObjectPropertiesDialog(props: { FooterHook: (footer: () => ReactElement) => void }) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [types, setType] = useState<string[]>(['General', 'Rectangle', 'LineBased', 'SightPresentation', 'CloseShape', 'Arc', 'Ellipse', 'Arrow', 'Picture', 'LineExpansion', 'Text'])
    const [localProperties, setLocalProperties] = useState<ObjectPropertiesBase>({ ...generalService.ObjectProperties })
    const dispatch = useDispatch();

    const setLocalPropertiesCallback = (key: string, value: any) => {
        runCodeSafely(() => {
            setLocalProperties(prevState => {
                const keys = key.split('.');
                if (keys.length === 1) {
                    return { ...prevState, [key]: value };
                } else {
                    const updatedState = { ...prevState };
                    let current = updatedState;

                    for (let i = 0; i < keys.length - 1; i++) {
                        current[keys[i]] = { ...current[keys[i]] };
                        current = current[keys[i]];
                    }

                    current[keys[keys.length - 1]] = value;
                    return updatedState;
                }
            });
        }, 'EditModePropertiesDialog => setLocalPropertiesCallback');
    };

    useEffect(() => {
        const root = document.documentElement;
        let pixelWidth = window.innerHeight * 0.5 * globalSizeFactor;
        root.style.setProperty('--object-properties-dialog-width', `${pixelWidth}px`);

    }, [])
    useEffect(() => {
        props.FooterHook(getFooter);
    }, [localProperties])

    let Comps: any = {
        'General': General,
        'LineBased': LineBased,
        'SightPresentation': SightPresentation,
        'CloseShape': CloseShape,
        'Rectangle': Rectangle,
        'Arc': Arc,
        'Ellipse': Ellipse,
        'Text': Text,
        'Arrow': Arrow,
        'Picture': Picture,
        'LineExpansion': LineExpansion,
    }
    const getFooter = () => {
        return (
            <div className='form__footer-padding' style={{ display: 'flex', flexDirection: 'row-reverse' }}>
                <Button label="OK" onClick={() => {
                    generalService.ObjectProperties = localProperties;
                    dispatch(closeDialog(DialogTypesEnum.objectProperties));
                }} />
            </div>
        );
    }
    return (
        <div style={{ height: `${globalSizeFactor * 50}vh` }}>
            <TabView scrollable style={{ height: '100%', borderBottom: `${globalSizeFactor * 0.15}vh solid #dee2e6`, marginBottom: '1%' }}>
                {types.map((item: string, key: number) => {
                    const Item = Comps[item];
                    return <TabPanel header={item} key={key} style={{ height: '100%' }}>
                        <Item dialogProperties={localProperties} setDialogPropertiesCallback={setLocalPropertiesCallback}></Item>
                    </TabPanel>
                })}
            </TabView>
            <br></br>
        </div>
    );
}
