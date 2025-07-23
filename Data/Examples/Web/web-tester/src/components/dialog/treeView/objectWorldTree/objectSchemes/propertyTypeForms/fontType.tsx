import Font, { FontDialogActionMode } from "../../../../mapToolbarActions/symbolicItemsDialogs/font/font";
import { Button } from "primereact/button";
import { Dialog } from "primereact/dialog";
import { useEffect, useState } from "react";
import EditFontCtrl from "../../../../shared/editFontCtrl";

export default function FontType(props: { value: any, onOk: (newValue: number) => void }) {
    let [value, setValue] = useState(props.value);

    return (<>
        <EditFontCtrl
            defaultFont={value}
            handleGetFont={(font: MapCore.IMcFont, isSetAsDefault: boolean) => { setValue(font) }}
            label=''
        />
        <Button label="OK" onClick={() => { props.onOk(value) }}></Button>
    </>
    );
}
