import { Button } from "primereact/button";
import { useEffect, useState } from "react";
import EditTextureCtrl from "../../../../shared/editTextureCtrl";

export default function EditTextureType(props: { value: any, onOk: (newValue: number) => void }) {

    let [value, setValue] = useState(props.value);

    return (<>
        <EditTextureCtrl value={props.value} onOk={(t: any) => { setValue(t) }} label='' texturePropertiesBase={null} saveTexturePropertiesCB={() => { }}></EditTextureCtrl>
        <Button label="OK" onClick={() => { props.onOk(value) }}></Button>
    </>
    );
}
