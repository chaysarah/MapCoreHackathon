import { Fieldset } from "primereact/fieldset";
import { Checkbox } from "primereact/checkbox";
import { Button } from "primereact/button";
import { InputText } from "primereact/inputtext";
import { useEffect, useState } from "react";
import { useSelector } from "react-redux";

import { OverlayManagerFormTabInfo } from "./overlayManagerForm";
import Vector3DFromMap from "../shared/Vector3DFromMap";
import Vector2DFromMap from "../shared/Vector2DFromMap";
import TreeNodeModel from '../../../../shared/models/tree-node.model'
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";
import { AppState } from "../../../../../redux/combineReducer";

export class ConvertPointProperties {
    Vector3D: MapCore.SMcVector3D;
    Vector2D: MapCore.SMcVector2D;
    requestSeparateIntersection: boolean;

    static getDefault(p: any): ConvertPointProperties {
        return {
            Vector3D: new MapCore.SMcVector3D(0, 0, 0),
            Vector2D: new MapCore.SMcVector2D(0, 0),
            requestSeparateIntersection: false,
        }
    }
}

export default function ConvertPoint(props: { tabInfo: OverlayManagerFormTabInfo }) {
    let selectedNodeInTree: TreeNodeModel = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    const [isMountedUseEffect, setIsMountedUseEffect] = useState({
        selectedNodeInTree: false,
    })

    useEffect(() => {
        runCodeSafely(() => {
            if (!props.tabInfo.properties.convertPointProperties) {
                let defaultProperties = ConvertPointProperties.getDefault({})
                props.tabInfo.setInitialStatePropertiesCallback("convertPointProperties", null, { ...defaultProperties });
                props.tabInfo.setPropertiesCallback("convertPointProperties", null, defaultProperties);
            }

        }, "ConvertPoint.useEffect")
    }, [])
    useEffect(() => {
        if (isMountedUseEffect.selectedNodeInTree) {
            let defaultProperties = ConvertPointProperties.getDefault({})
            props.tabInfo.setInitialStatePropertiesCallback("convertPointProperties", null, { ...defaultProperties });
            props.tabInfo.setPropertiesCallback("convertPointProperties", null, defaultProperties);
        }
        else {
            setIsMountedUseEffect({ ...isMountedUseEffect, selectedNodeInTree: true })
        }
    }, [selectedNodeInTree])

    return (<>
        <div style={{ display: "flex", justifyContent: 'space-between' }}>
            <div>
                <h4>Overlay manager point</h4>
                <Vector3DFromMap pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={props.tabInfo.properties.convertPointProperties?.Vector3D} lastPoint={true} saveTheValue={(x) => {
                    props.tabInfo.setPropertiesCallback("convertPointProperties", "Vector3D", x);
                }} />
                <h4>Image calc point</h4>
                <Vector2DFromMap initValue={props.tabInfo.properties.convertPointProperties?.Vector2D} lastPoint={true} saveTheValue={(x) => {
                    props.tabInfo.setPropertiesCallback("convertPointProperties", "Vector2D", x);
                }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} />
                <div style={{ paddingTop: `${globalSizeFactor * 2}vh` }} className="form__flex-and-row">
                    <Checkbox checked={props.tabInfo.properties.convertPointProperties?.requestSeparateIntersection} inputId="requestSeparateIntersection" name="requestSeparateIntersection" onChange={(e) =>
                        props.tabInfo.setPropertiesCallback("convertPointProperties", "requestSeparateIntersection", e.checked)} />
                    <label htmlFor="requestSeparateIntersection">Request separate intersection status</label>
                </div>
            </div>
            <Fieldset className="form__column-fieldset" style={{ marginLeft: '2%' }} legend="Image Calc">
                <h4>Select from list</h4>
                <div style={{ width: `${globalSizeFactor * 1.5 * 15}vh` }} className='layout__display-selected-layers' >
                    {/* {selectedLayers?.map((layer: string, key: number) => (
                                <div key={key}>{layer}</div>
                            ))} */}
                </div>
                <div>
                    <Button className="overlay-manager__margin-right">Delete</Button>
                    <Button className="overlay-manager__margin-right">Refresh</Button>
                    <Button className="overlay-manager__margin-right">Create New</Button>
                </div>
            </Fieldset>
        </div>
        <div style={{ marginTop: `${globalSizeFactor * 6}vh`, width: '100%', display: 'flex', flexDirection: 'column', alignItems: 'center', }}>
            <div style={{ display: 'flex', }}>
                <div style={{ padding: `${globalSizeFactor * 0.5}vh` }}> <Button>Convert world to image</Button></div>
                <div style={{ padding: `${globalSizeFactor * 0.5}vh` }}><Button>Convert image to world</Button></div>
            </div>
            <div style={{ padding: `${globalSizeFactor * 1}vh` }}>
                <label style={{ paddingRight: `${globalSizeFactor * 1}vh` }}>Intersection Status:</label>
                <InputText className="disabledDiv" />
            </div>
        </div>
    </>
    );
}
