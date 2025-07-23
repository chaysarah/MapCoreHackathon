import React, { useContext, useEffect, useState } from 'react'
import cn from './RemoveForm.module.css';
import ApplicationContext from '../../../context/applicationContext';
import config from '../../../config';

export default function RemoveForm(props) {
    const context = useContext(ApplicationContext);
    let [extrusionBasedOnDtmsArr, setExtrusionBasedOnDtmsArr] = useState([]);

    // function getRemoveMessage() {
    //     switch (props.nodeLevel) {
    //         case config.nodesLevel.group:
    //             if (props.nodeName === config.LAYERS_BACKLOGS_TITLE) {
    //                 return context.dict.areYouSureRemove;
    //             } else {
    //                 // normal group remove
    //                 return `Are you sure you want to remove ${props.node.title} group (layers that exists only in this group will be moved to Layers Backlog)?\n  The layers will be moved to Backlog and will be hidden in client applications.`

    //             }
    //         case config.nodesLevel.layer:
    //             if (context.selectedLayers.length > 0) {
    //                 return `Are you sure you want to remove ${context.selectedLayers.length} selected layers?`
    //             } else if (props.node.parentFolder === config.LAYERS_BACKLOGS_TITLE) {
    //                 return `Are you sure you want to permanently delete ${props.node.title} layer?`
    //             } else {
    //                 if (props.node.Group.split(',').length > 1) {
    //                     return `Are you sure you want to remove ${props.node.title} layer?\n  The layers will be moved to Backlog and will be hidden in client applications.`
    //                 } else {
    //                     return `Are you sure you want to remove ${props.node.title} layer? \nThe layer will be moved to Backlog and will be hidden in client applications.\n  The layer will be moved to Backlog and will be hidden in client applications.`
    //                 }
    //             }
    //         default:
    //             break;
    //     }
    // }


    function isThereAnySilentImportInSelectedLayersToRemove() {
        let flag = false;
        if (props.selectedlayers) {
            props.selectedlayers.forEach((node) => {
                let lastIndexOf = node.lastIndexOf("#");
                let subName = node.slice(lastIndexOf + 1);
                let layerNameNode = props.backlogGroupNodes.find(node => node.LayerId == subName);
                if (layerNameNode.LayerImportType == "SILENTLY_IMPORTED")
                    flag = true;
            });
        }
        return flag;
    }
    const isExtrExistInLayersToRemove = (layer3DExtr) => {
        let layer = props.layersToRemove.find(layer => layer.LayerId === layer3DExtr.LayerId);
        if (layer?.LayerId == layer3DExtr.LayerId) {
            return true;
        }
        return false;
    }

    function getExtrusionsBasedOnSpesificArr(dtmLayerId) {//and put then in state arr
        let ExtrusionsBasedOnSpesificArr = [];
        let flatGroupsTree = props.groupsTree.map(group => group.childNodes).flat();
        flatGroupsTree.map(layer => {
            if (layer.LayerType == 'RAW_VECTOR_3D_EXTRUSION' && layer.RawLayerInfo?.Vector3DExt?.DtmLayerId == dtmLayerId && !isExtrExistInLayersToRemove(layer)) {
                ExtrusionsBasedOnSpesificArr.push({ ...layer, deleteExtrusionFromBacklog: true });
            }
        });
        return ExtrusionsBasedOnSpesificArr;
    }

    function isHasDtmToRemoveWithoutAllItsExtrusion() {
        let numOfDtmsWithExtrBasedOn = false;
        extrusionBasedOnDtmsArr = [];
        props.layersToRemove.map((layer) => {
            if (layer.LayerType?.includes('DTM')) {
                let tmpExtrArr = getExtrusionsBasedOnSpesificArr(layer.LayerId);
                if (tmpExtrArr.length !== 0) {
                    extrusionBasedOnDtmsArr = [...extrusionBasedOnDtmsArr, ...tmpExtrArr];
                    numOfDtmsWithExtrBasedOn = numOfDtmsWithExtrBasedOn ? numOfDtmsWithExtrBasedOn + 1 : 1;
                }
            }
        });
        props.setParentHook(extrusionBasedOnDtmsArr);
        return numOfDtmsWithExtrBasedOn;
    }

    function getRemoveCheckBoxMessageOfOneGroup() {
        return (
            <>
                {props.node.title == config.LAYERS_BACKLOGS_TITLE ?
                    <label>Are you sure you want to permanently delete all the layers from layers Backlog?</label> :
                    <label>Are you sure you want to remove the group?
                        The group layers will be moved to layers Backlog and will be hidden in client applications.
                    </label>
                }
                {props.node.title == config.LAYERS_BACKLOGS_TITLE && props.node.childNodes.find(layer => layer.LayerImportType == "SILENTLY_IMPORTED") ?
                    <div className={cn.Wrapper}>
                        <label style={{ color: 'red' }} >The data of some of the layers were silently imported without copying. Select the following checkbox only if you are sure you want to delete the original data of silently imported layers from the disk!</label>
                        <br></br>
                        <br></br>
                        <input type='checkbox' onChange={(e) => props.changeRemoveCheckBox(e.target.checked)}></input>
                        <label style={{ color: 'red' }}> Delete also the original data of silently imported layers from the disk</label>
                    </div> :
                    ""}
                {props.node.title == config.LAYERS_BACKLOGS_TITLE && isHasDtmToRemoveWithoutAllItsExtrusion() && !props.isDtmWithoutAllExtrAndSilentImportRemove ?
                    <label><br></br>
                        <br></br>{extrusionBasedOnDtmsArr.length} Vector 3D Extrusion layers depend on {isHasDtmToRemoveWithoutAllItsExtrusion()} DTM layers you specified for deletion. These Vector 3D Extrusion layers must therefore also be deleted.
                        <br></br> <br></br>Are you sure you want to delete all these layers?
                    </label> : ""
                }
            </>
        )
    }

    function getRemoveCheckBoxMessageOfLayersFromSameGroup() {
        return (
            <>
                {props.node.parentFolder == config.LAYERS_BACKLOGS_TITLE && context.selectedLayers.length > 0 ?
                    <label>Are you sure you want to permanently delete {context.selectedLayers.length} layers from layers Backlog?</label>
                    : ""}

                {props.node.parentFolder == config.LAYERS_BACKLOGS_TITLE && context.selectedLayers.length == 0 ?
                    <label> Are you sure you want to permanently delete {props.node.title} layer from layers Backlog?</label>
                    : ""}
                {(props.node.parentFolder == config.LAYERS_BACKLOGS_TITLE && ((props.node.LayerImportType == "SILENTLY_IMPORTED")
                    || isThereAnySilentImportInSelectedLayersToRemove()))?
                    <div className={cn.Wrapper}>
                        <label style={{ color: 'red' }}>The data of some were silently imported without copying. Select the following checkbox only if you are sure you want to delete the original data of the layer from the disk!</label>
                        <br></br>
                        <br></br>
                        <input type='checkbox' onChange={(e) => props.changeRemoveCheckBox(e.target.checked)}></input>
                        <label style={{ color: 'red' }}> Delete also the original data of the layer from the disk</label>
                    </div>
                    : ""}
                {props.node.parentFolder != config.LAYERS_BACKLOGS_TITLE && context.selectedLayers.length > 0 ?
                    <label>Are you sure you want to remove {context.selectedLayers.length} layers?
                        The layers will be moved to layers Backlog and will be hidden in client applications.
                    </label>
                    : ""}
                {props.node.parentFolder != config.LAYERS_BACKLOGS_TITLE && context.selectedLayers.length == 0 ?
                    <label>Are you sure you want to remove {props.node.title} layer?
                        The layer will be moved to layers Backlog and will be hidden in client applications.
                    </label>
                    : ""}
                {props.node.parentFolder == config.LAYERS_BACKLOGS_TITLE && isHasDtmToRemoveWithoutAllItsExtrusion() && !props.isDtmWithoutAllExtrAndSilentImportRemove ?
                    <label><br></br>
                        <br></br>{extrusionBasedOnDtmsArr.length} Vector 3D Extrusion layers depend on {isHasDtmToRemoveWithoutAllItsExtrusion()} DTM layers you specified for deletion. These Vector 3D Extrusion layers must therefore also be deleted.
                        <br></br><br></br>Are you sure you want to delete all these layers?
                    </label> : ""
                }

            </>
        )
    }


    function getRemoveCheckBoxMessage() {
        if (props.isSecondRemove) {
            props.setParentSecondRemove();
        }
        if (!props.node.length) {//for cases:one group, one layer,some layers from same group
            switch (props.nodeLevel) {
                case config.nodesLevel.group:
                    return getRemoveCheckBoxMessageOfOneGroup();
                case config.nodesLevel.layer:
                    return getRemoveCheckBoxMessageOfLayersFromSameGroup();
            }
        }
        else {//for case: some groups,group/s and layer/s, layers from some groups
            if (props.node[1].length == 0) {//some groups
                return <label>Are you sure you want to remove {props.node[0].length} groups?
                    The layers will be moved to layers Backlog and will be hidden in client applications.
                </label>
            }
            else if (props.node[0].length !== 0 && props.node[1].length !== 0) {//group/s and layer/s
                return <label>Are you sure you want to remove {props.node[0].length} groups and {props.node[1].length} layers?
                    The layers will be moved to layers Backlog and will be hidden in client applications.
                </label>
            }
            else {
                return <label>Are you sure you want to remove {props.node[1].length} layers?
                    The layers will be moved to layers Backlog and will be hidden in client applications.
                </label>
            }
        }
    }

    function getBasisDtmMessege() {
      let x=  isHasDtmToRemoveWithoutAllItsExtrusion();
        props.setParentSecondRemove();
        return (<label>
            <br></br>{extrusionBasedOnDtmsArr.length} Vector 3D Extrusion layers depend on {isHasDtmToRemoveWithoutAllItsExtrusion()} DTM layers you specified for deletion. These Vector 3D Extrusion layers must therefore also be deleted.
            <br></br> <br></br>Are you sure you want to delete all these layers?
        </label>);
    }

    return (
        <>
            <div className={cn.Wrapper}>
                <span>{props.isDtmWithoutAllExtrAndSilentImportRemove && !props.isSecondRemove ?
                    getBasisDtmMessege() :
                    getRemoveCheckBoxMessage()
                }</span>
            </div>

        </>
    )
}
