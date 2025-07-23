import { useSelector } from "react-redux";
import { AppState } from "../../../../../redux/combineReducer";
import TreeNodeModel, { objectWorldNodeType } from '../../../../shared/models/tree-node.model'
import { ChangeEvent, useEffect, useState } from "react";
import { InputText } from "primereact/inputtext";
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";
import { Button } from "primereact/button";
import { InputNumber } from "primereact/inputnumber";
import objectWorldTreeService from "../../../../../services/objectWorldTree.service";

export default function ItemForm() {
    let selectedNodeInTree: TreeNodeModel = useSelector((state: AppState) => state.objectWorldTreeReducer.selectedNodeInTree);
    const [item] = useState<MapCore.IMcSymbolicItem>(selectedNodeInTree.nodeMcContent)

    const onClickApply = () => {
        runCodeSafely(() => {
            let scheme = item.GetScheme();
            scheme.SetObjectScreenArrangementItem(item)
        }, 'itemForm.onClickApply')
    }

    return (
        <div>
            <h4> Item</h4>
            <label>Set Offset: </label>
            <Button label="Apply" onClick={onClickApply}></Button>
        </div>
    )
}