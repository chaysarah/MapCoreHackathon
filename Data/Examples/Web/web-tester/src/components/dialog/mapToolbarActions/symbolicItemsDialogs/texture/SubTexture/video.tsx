import { useEffect } from 'react';
import { InputText } from 'primereact/inputtext';
import { Checkbox } from 'primereact/checkbox';
import { InputNumber } from 'primereact/inputnumber';
import { RadioButton } from 'primereact/radiobutton';

import { Properties } from "../../../../dialog";
import TexturePropertiesBase from '../../../../../../propertiesBase/texturePropertiesBase';
import TextureService, { TextureTypeEnum } from '../../../../../../services/texture.service';
import { runCodeSafely } from '../../../../../../common/services/error-handling/errorHandler';

export class VideoProperties implements Properties {
    NameSource: string;
    TextureName: string;
    State: MapCore.IMcVideoTexture.EState;
    IsReadable: boolean;
    PlayInLoop: boolean;
    WithSound: boolean;
    FrameRate: number;

    static getDefault(props: any): VideoProperties {
        let { tabInfo } = props;

        return {
            NameSource: tabInfo.properties.videoProperties.NameSource || "",
            TextureName: tabInfo.properties.videoProperties.TextureName || "",
            State: tabInfo.properties.videoProperties.State || MapCore.IMcVideoTexture.EState.ES_RUNNING,
            IsReadable: tabInfo.properties.videoProperties.IsReadable || false,
            PlayInLoop: tabInfo.properties.videoProperties.PlayInLoop || false,
            WithSound: tabInfo.properties.videoProperties.WithSound || false,
            FrameRate: tabInfo.properties.videoProperties.FrameRate || 0,
        }
    }
}

export default function Video(props: {
    defaultTexture: MapCore.IMcVideoTexture,
    tabInfo: {
        properties: TexturePropertiesBase,
        setPropertiesCallback: (tab: string, key: string, value: any) => void,
    }
}) {

    useEffect(() => {
        runCodeSafely(() => {
            let defaultProperties = VideoProperties.getDefault(props)
            props.tabInfo.setPropertiesCallback("videoProperties", null, defaultProperties);
            TextureService.textureType = TextureTypeEnum.Video;
        }, 'video.useEffect')
    }, [])

    const saveData = (event: any) => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback("videoProperties", event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
        }, "video.saveData => onChange");
    }

    return (
        <div className='texture__general-container'>
            <b>HTML video</b>
            <br />
            <div className='form__flex-and-row-between'>
                <label>Name Source:</label>
                <InputText name="NameSource" value={props.tabInfo.properties.videoProperties.NameSource ?? ''} onChange={saveData} />
            </div>
            <div className='form__center-aligned-row texture__flex-and-row-start'>
                <div className='form__flex-and-row'>
                    <Checkbox name="IsReadable" onChange={saveData} checked={props.tabInfo.properties.videoProperties.IsReadable ?? false} />
                    <label className="ml-2 texture__checkbox-div">Is Readable</label>
                </div>
                <div className='form__flex-and-row'>
                    <Checkbox name="PlayInLoop" onChange={saveData} checked={props.tabInfo.properties.videoProperties.PlayInLoop ?? false} />
                    <label className="ml-2 texture__checkbox-div">Play In Loop</label>
                </div>
                <div className='form__flex-and-row'>
                    <Checkbox name="WithSound" onChange={saveData} checked={props.tabInfo.properties.videoProperties.WithSound ?? false} />
                    <label className="ml-2 texture__checkbox-div">With Sound</label>
                </div>
            </div>
            <div className='form__flex-and-row-between'>
                <label>Texture Name:</label>
                <InputText name="TextureName" value={props.tabInfo.properties.videoProperties.TextureName ?? ''} onChange={saveData} />
            </div>
            <div className='form__center-aligned-row'>
                <div>State:</div>
                <div className="form__flex-and-row">
                    <RadioButton inputId="runningState" name="State" value="running" onChange={() => { props.tabInfo.setPropertiesCallback("videoProperties", 'State', MapCore.IMcVideoTexture.EState.ES_RUNNING) }} checked={props.tabInfo.properties.videoProperties.State === MapCore.IMcVideoTexture.EState.ES_RUNNING} />
                    <label htmlFor="runningState" className="ml-2">Running</label>
                </div>
                <div className="form__flex-and-row">
                    <RadioButton inputId="stoppedState" name="State" value="stopped" onChange={() => { props.tabInfo.setPropertiesCallback("videoProperties", 'State', MapCore.IMcVideoTexture.EState.ES_STOPPED) }} checked={props.tabInfo.properties.videoProperties.State === MapCore.IMcVideoTexture.EState.ES_STOPPED} />
                    <label htmlFor="stoppedState" className="ml-2">Stopped</label>
                </div>
                <div className="form__flex-and-row">
                    <RadioButton inputId="pausedState" name="State" value="paused" onChange={() => { props.tabInfo.setPropertiesCallback("videoProperties", 'State', MapCore.IMcVideoTexture.EState.ES_PAUSED) }} checked={props.tabInfo.properties.videoProperties.State === MapCore.IMcVideoTexture.EState.ES_PAUSED} />
                    <label htmlFor="pausedState" className="ml-2">Paused</label>
                </div>
            </div>
            <div className='form__flex-and-row-between'>
                <label>frame Rate For Render Based Update:</label>
                <InputNumber name="FrameRate" value={props.tabInfo.properties.videoProperties.FrameRate ?? 0} onValueChange={saveData} />
            </div>
        </div >
    )
}


