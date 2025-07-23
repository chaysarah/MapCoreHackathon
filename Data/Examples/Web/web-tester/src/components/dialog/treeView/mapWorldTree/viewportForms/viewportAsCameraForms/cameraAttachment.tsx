import { useEffect, useState } from "react";
import { Fieldset } from "primereact/fieldset";
import { useDispatch, useSelector } from "react-redux";
import { Button } from "primereact/button";
import { Checkbox } from "primereact/checkbox";
import { InputNumber } from "primereact/inputnumber";
import { Tree } from "primereact/tree";

import { ViewportAsCameraFormTabInfo } from "./viewportAsCamera";
import Vector3DFromMap from "../../../objectWorldTree/shared/Vector3DFromMap";
import { Properties } from "../../../../dialog";
import ObjectTreeNode, { objectWorldNodeType } from "../../../../../shared/models/tree-node.model";
import { runCodeSafely, runMapCoreSafely } from "../../../../../../common/services/error-handling/errorHandler";
import objectWorldTreeService from "../../../../../../services/objectWorldTree.service";
import { AppState } from "../../../../../../redux/combineReducer";

export class CameraAttachmentPropertiesState implements Properties {
    attachPointIndexSrc: number;
    attachPointIndexLookAt: number;
    cameraAttchmentEnabled: boolean;
    offset: MapCore.SMcVector3D;
    isAttachOrientation: boolean;
    additionalYaw: number;
    additionalPitch: number;
    additionalRoll: number;
    pbLookAtAttachmentDefined: boolean;

    static getDefault(p: any): CameraAttachmentPropertiesState {
        let { currentViewport } = p;
        let mcCurrentViewport = currentViewport.nodeMcContent as MapCore.IMcMapViewport;
        let mcCameraAttchmentEnabled = false;

        let pAttachment: { Value?: MapCore.IMcMapCamera.SCameraAttachmentParams } = { Value: new MapCore.IMcMapCamera.SCameraAttachmentParams() }
        let pbAttachmentDefined: { Value?: boolean } = {}
        let pLookAtAttachment: { Value?: MapCore.IMcMapCamera.SCameraAttachmentTarget } = { Value: new MapCore.IMcMapCamera.SCameraAttachmentTarget() }
        let pbLookAtAttachmentDefined: { Value?: boolean } = {}
        runMapCoreSafely(() => {
            mcCameraAttchmentEnabled = mcCurrentViewport.GetCameraAttachmentEnabled();
        }, 'CameraAttachmentPropertiesState.getDefault => IMcMapCamera.GetCameraAttachmentEnabled', true)
        runMapCoreSafely(() => {
            mcCurrentViewport.GetCameraAttachment(pAttachment, pbAttachmentDefined, pLookAtAttachment, pbLookAtAttachmentDefined);
        }, 'CameraAttachmentPropertiesState.getDefault => IMcMapCamera.GetCameraAttachment', true)

        return {
            cameraAttchmentEnabled: mcCameraAttchmentEnabled,
            offset: pAttachment.Value.Offset,
            isAttachOrientation: pAttachment.Value.bAttachOrientation,
            additionalYaw: pAttachment.Value.fAdditionalYaw,
            additionalPitch: pAttachment.Value.fAdditionalPitch,
            additionalRoll: pAttachment.Value.fAdditionalRoll,
            attachPointIndexSrc: pAttachment.Value.uAttachPoint,
            attachPointIndexLookAt: pLookAtAttachment.Value.uAttachPoint,

            pbLookAtAttachmentDefined: pbLookAtAttachmentDefined.Value,//help parameter - not in DOM
        }
    }
}
export class CameraAttachmentProperties extends CameraAttachmentPropertiesState {
    isLookAt: boolean;
    treeSrc: ObjectTreeNode[];
    treeLookAt: ObjectTreeNode[];
    selectedNodeSrc: ObjectTreeNode;
    selectedNodeLookAt: ObjectTreeNode;

    static getDefault(p: any): CameraAttachmentProperties {
        let stateDefaults = super.getDefault(p);
        let { currentViewport } = p;

        let defaults: CameraAttachmentProperties = {
            ...stateDefaults,
            isLookAt: stateDefaults.pbLookAtAttachmentDefined,
            treeSrc: [],// objectsArr,// objectsWithSchemesTree,
            treeLookAt: [],// objectsArr// objectsWithSchemesTree,
            selectedNodeSrc: null,
            selectedNodeLookAt: null,
        }
        return defaults;
    }
};

export default function CameraAttachment(props: { tabInfo: ViewportAsCameraFormTabInfo }) {
    const dispatch = useDispatch();
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    let treeRedux = useSelector((state: AppState) => state.mapWorldTreeReducer.mapWorldTree);
    let objectTreeRedux = useSelector((state: AppState) => state.objectWorldTreeReducer.objectWorldTree);
    let selectedNodeInTree = useSelector((state: AppState) => state.mapWorldTreeReducer.selectedNodeInTree);
    let [cameraAttachmentLocalProperties, setCameraAttachmentLocalProperties] = useState(props.tabInfo.tabProperties);

    const [isMountedUseEffect, setIsMountedUseEffect] = useState({
        currentViewport: false,
    })
    //UseEffects
    useEffect(() => {
        runCodeSafely(() => {
            props.tabInfo.setApplyCallBack(applyAll);
            setCameraAttachmentLocalProperties(props.tabInfo.tabProperties)
        }, 'ViewportAsCamera/CameraAttachment.useEffect => props.tabInfo.tabProperties');
    }, [props.tabInfo.tabProperties])

    useEffect(() => {
        runCodeSafely(() => {
            let mcCurrentViewport = selectedNodeInTree.nodeMcContent as MapCore.IMcMapViewport;
            let mcOM = mcCurrentViewport.GetOverlayManager();
            let overlayManagerTreeNode = objectWorldTreeService.convertMapcorObjectToTreeNodeModel(objectTreeRedux, mcOM)
            let objectsArr = objectWorldTreeService.findNodesByType(overlayManagerTreeNode, objectWorldNodeType.OBJECT)
            let objectsWithSchemesTree: ObjectTreeNode[] = [];
            objectsArr.forEach((objectTreeNode) => {
                let mcScheme = objectTreeNode.nodeMcContent.GetScheme();
                let schemeTreeNode = objectWorldTreeService.convertMapcorObjectToTreeNodeModel(overlayManagerTreeNode, mcScheme)
                let items = objectWorldTreeService.findNodesByType(schemeTreeNode, objectWorldNodeType.ITEM);
                objectTreeNode.children = items;
                objectsWithSchemesTree = [...objectsWithSchemesTree, objectTreeNode];
            })

            props.tabInfo.setPropertiesCallback('treeSrc', objectsArr);
            props.tabInfo.setPropertiesCallback('treeLookAt', objectsArr);
        }, 'ViewportAsCamera/CameraAttachment.useEffect => objectTreeRedux');
    }, [objectTreeRedux])

    useEffect(() => {
        runCodeSafely(() => {
            if (!cameraAttachmentLocalProperties.isLookAt) {
                props.tabInfo.setPropertiesCallback('selectedNodeLookAt', null);
            }
        }, 'ViewportAsCamera/CameraAttachment.useEffect => cameraAttachmentLocalProperties.isLookAt');
    }, [cameraAttachmentLocalProperties.isLookAt])

    const saveData = (event: any) => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback(event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
            if (event.target.name in new CameraAttachmentPropertiesState) {
                props.tabInfo.setCurrStatePropertiesCallback(event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
            }
        }, "ViewportAsCamera/CameraAttachment.saveData => onChange")
    }

    const getCameraAttachmentObjectAndItem = (selectedNode: ObjectTreeNode, tree: ObjectTreeNode[]) => {
        let finalObject = null;
        let finalItem = null;
        runCodeSafely(() => {
            if (selectedNode) {
                if (selectedNode.nodeType == objectWorldNodeType.OBJECT) {
                    finalObject = selectedNode.nodeMcContent;
                }
                else {
                    finalItem = selectedNode.nodeMcContent;
                    finalObject = tree.find(obj => {
                        let foundedItem = obj.children.find(item => JSON.stringify(item) === JSON.stringify(selectedNode))
                        return foundedItem ? true : false;
                    })?.nodeMcContent;
                }
            }
        }, 'ViewportAsCamera/CameraAttachment.getCameraAttachmentObjectAndItem');

        return { object: finalObject, item: finalItem };
    }

    const isSelectedNodeSrcMeshItem = (selectedNode: ObjectTreeNode) => {
        let isMeshItem = false;
        runCodeSafely(() => {
            if (selectedNode.nodeType == objectWorldNodeType.ITEM) {
                let mcItem = selectedNode.nodeMcContent as MapCore.IMcObjectSchemeItem;
                isMeshItem = mcItem.GetNodeType() == MapCore.IMcMeshItem.NODE_TYPE;
            }
        }, 'ViewportAsCamera/CameraAttachment.isMeshItem');
        return isMeshItem;
    }

    const applyAll = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback([]);
            let mcCurrentViewport = selectedNodeInTree.nodeMcContent as MapCore.IMcMapViewport;

            let attachmentParams = new MapCore.IMcMapCamera.SCameraAttachmentParams();
            if (cameraAttachmentLocalProperties.selectedNodeSrc) {
                let objectAndItem = getCameraAttachmentObjectAndItem(cameraAttachmentLocalProperties.selectedNodeSrc, cameraAttachmentLocalProperties.treeSrc);
                attachmentParams.pObject = objectAndItem.object;
                attachmentParams.pItem = objectAndItem.item;
                attachmentParams.uAttachPoint = isSelectedNodeSrcMeshItem(cameraAttachmentLocalProperties.selectedNodeSrc) ? cameraAttachmentLocalProperties.attachPointIndexSrc : null;
                attachmentParams.Offset = cameraAttachmentLocalProperties.offset;
                attachmentParams.bAttachOrientation = cameraAttachmentLocalProperties.isAttachOrientation;
                attachmentParams.fAdditionalYaw = cameraAttachmentLocalProperties.additionalYaw;
                attachmentParams.fAdditionalPitch = cameraAttachmentLocalProperties.additionalPitch;
                attachmentParams.fAdditionalRoll = cameraAttachmentLocalProperties.additionalRoll;
                console.log(attachmentParams);
            }

            let cameraAttachmentTarget = new MapCore.IMcMapCamera.SCameraAttachmentTarget();
            if (cameraAttachmentLocalProperties.selectedNodeLookAt) {
                let objectAndItem = getCameraAttachmentObjectAndItem(cameraAttachmentLocalProperties.selectedNodeLookAt, cameraAttachmentLocalProperties.treeLookAt);
                cameraAttachmentTarget.pItem = objectAndItem.item;
                cameraAttachmentTarget.pObject = objectAndItem.object;
                cameraAttachmentTarget.uAttachPoint = isSelectedNodeSrcMeshItem(cameraAttachmentLocalProperties.selectedNodeLookAt) ? cameraAttachmentLocalProperties.attachPointIndexLookAt : null;
            }

            runMapCoreSafely(() => {
                mcCurrentViewport.SetCameraAttachment(attachmentParams, cameraAttachmentTarget);
            }, 'CameraAttachmentPropertiesState.getDefault => IMcMapCamera.SetCameraAttachment', true)
        }, 'ViewportAsCamera/CameraAttachment.applyAll');
    }
    //Handle Functions
    const saveOffsetPoint = (...args) => {
        runCodeSafely(() => {
            const [point, flagNull, pointType] = args;
            props.tabInfo.setPropertiesCallback(pointType, point);
        }, 'ViewportAsCamera/CameraAttachment.saveOffsetPoint');
    }
    const handleCameraAttchmentEnabledSetClick = () => {
        props.tabInfo.applyCurrStatePropertiesCallback(['cameraAttchmentEnabled']);
        let mcCurrentViewport = selectedNodeInTree.nodeMcContent as MapCore.IMcMapViewport;
        runCodeSafely(() => {
            runMapCoreSafely(() => {
                mcCurrentViewport.SetCameraAttachmentEnabled(cameraAttachmentLocalProperties.cameraAttchmentEnabled);
            }, 'CameraAttachmentPropertiesState.getDefault => IMcMapCamera.SetCameraAttachment', true)
        }, 'ViewportAsCamera/CameraAttachment.handleCameraAttchmentEnabledSetClick');
    }
    //DOM Functions
    const getSrcSide = () => {
        return <Fieldset style={{ width: '50%' }} className="form__column-fieldset">
            <Tree
                selectionMode='single'
                selectionKeys={cameraAttachmentLocalProperties.selectedNodeSrc?.key}
                onSelectionChange={(e) => {
                    let selectedNode = objectWorldTreeService.getNodeByKey(objectTreeRedux, e.value as string)
                    props.tabInfo.setPropertiesCallback('selectedNodeSrc', selectedNode)
                }}
                style={{ height: `${globalSizeFactor * 15}vh`, overflow: 'auto' }}
                value={cameraAttachmentLocalProperties.treeSrc}
            />
            <div className="form__flex-and-row-between form__items-center">
                <span className="vp-as-camera__r-padding-span">Attach Point Index:</span>
                <InputNumber className="form__medium-width-input" name="attachPointIndexSrc" value={cameraAttachmentLocalProperties.attachPointIndexSrc} onValueChange={saveData} />
            </div>
            <div className="form__flex-and-row form__items-center">
                <span> Offset:</span>
                <Vector3DFromMap flagNull={{ x: false, y: false, z: false }} pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={cameraAttachmentLocalProperties.offset} saveTheValue={(...args) => saveOffsetPoint(...args, 'offset')} lastPoint={true} />
            </div>
            <div className="form__flex-and-row form__items-center">
                <Checkbox name="isLookAt" onChange={saveData} checked={cameraAttachmentLocalProperties.isLookAt} />
                <span style={{ whiteSpace: 'pre' }} className="form__checkbox-div">Look At</span>
            </div>
            {!cameraAttachmentLocalProperties.isLookAt &&
                <Fieldset>
                    <div className="form__flex-and-row form__items-center">
                        <Checkbox name="isAttachOrientation" onChange={saveData} checked={cameraAttachmentLocalProperties.isAttachOrientation} />
                        <span className="form__checkbox-div">Attach Orientation (Additional angle relevant only when it's checked)</span>
                    </div>
                    <Fieldset>
                        <div className="form__flex-and-row-between form__items-center">
                            <span className="vp-as-camera__r-padding-span">Additional Yaw:</span>
                            <InputNumber className="form__medium-width-input" name="additionalYaw" value={cameraAttachmentLocalProperties.additionalYaw} onValueChange={saveData} />
                        </div>
                        <div className="form__flex-and-row-between form__items-center">
                            <span className="vp-as-camera__r-padding-span">Additional Pitch:</span>
                            <InputNumber className="form__medium-width-input" name="additionalPitch" value={cameraAttachmentLocalProperties.additionalPitch} onValueChange={saveData} />
                        </div>
                        <div className="form__flex-and-row-between form__items-center">
                            <span className="vp-as-camera__r-padding-span">Additional Roll:</span>
                            <InputNumber className="form__medium-width-input" name="additionalRoll" value={cameraAttachmentLocalProperties.additionalRoll} onValueChange={saveData} />
                        </div>
                    </Fieldset>
                </Fieldset>
            }
        </Fieldset>
    }

    return (
        <div>
            <div className="form__column-container">
                <div className="form__flex-and-row-between form__items-center">
                    <div className="form__flex-and-row form__items-center">
                        <Checkbox name="cameraAttchmentEnabled" onChange={saveData} checked={cameraAttachmentLocalProperties.cameraAttchmentEnabled} />
                        <span style={{ whiteSpace: 'pre' }} className="form__checkbox-div">Camera Attchment Enabled</span>
                    </div>
                    <Button label="Set" onClick={handleCameraAttchmentEnabledSetClick} />
                </div>
                <div className="form__flex-and-row-between">
                    {getSrcSide()}
                    <Fieldset className={`form__column-fieldset ${!cameraAttachmentLocalProperties.isLookAt && 'form__disabled-fieldset'}`} style={{ width: '50%' }}>
                        <Tree
                            selectionMode='single'
                            selectionKeys={cameraAttachmentLocalProperties.selectedNodeLookAt?.key}
                            onSelectionChange={(e) => {
                                let selectedNode = objectWorldTreeService.getNodeByKey(objectTreeRedux, e.value as string)
                                props.tabInfo.setPropertiesCallback('selectedNodeLookAt', selectedNode)
                            }}
                            style={{ height: `${globalSizeFactor * 15}vh`, overflow: 'auto' }}
                            value={cameraAttachmentLocalProperties.treeLookAt}
                        />
                        <div className='form__flex-and-row-between form__items-center'>
                            <span className="vp-as-camera__r-padding-span">Attach Point Index:</span>
                            <InputNumber className="form__medium-width-input" name="attachPointIndexLookAt" value={cameraAttachmentLocalProperties.attachPointIndexLookAt} onValueChange={saveData} />
                        </div>
                    </Fieldset>
                </div>
            </div>
        </div>
    )
}
