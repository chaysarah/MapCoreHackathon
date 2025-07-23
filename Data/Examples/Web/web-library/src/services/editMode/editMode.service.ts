import { ViewportData } from "../../model/viewportData";
import { PaintRectangle } from "./paintRectangle";

class ListItem {
    index: number; renderNeededResult: boolean; counter: number
    constructor(index_: number, renderNeededResult_: boolean, counter_: number) { this.index = index_; this.renderNeededResult = renderNeededResult_; this.counter = counter_ }
}

class EditModeService {
    m_Counter = 0;
    m_RenderState = false;
    listRenderNeeded: ListItem[] = []

    public AddRenderToList(renderResult: boolean) {
        let eventIndex = this.listRenderNeeded.length;
        if (this.m_RenderState != renderResult) {
            let newItem: ListItem = new ListItem(eventIndex, renderResult, 1);
            this.listRenderNeeded.push(newItem);
            this.m_Counter = 0;
        }
        else {
            if (this.listRenderNeeded.length == 0) {
                let newItem: ListItem = new ListItem(1, renderResult, 1);
                this.listRenderNeeded.push(newItem);
            }
            else
                this.listRenderNeeded[this.listRenderNeeded.length - 1].counter = (++this.m_Counter);
        }
        this.m_RenderState = renderResult;
        return this.listRenderNeeded
    }
    public clearList() {
        this.listRenderNeeded = [];
        return this.listRenderNeeded;
    }

    /* EditMode Usages */
    public doPaintRectangle(viewportData: ViewportData, coordSys: MapCore.EMcPointCoordSystem, onRectResultsCallback: (Coords: MapCore.SMcVector3D[]) => void) {
        let painter: PaintRectangle = new PaintRectangle(coordSys, onRectResultsCallback);
        painter.StartPaintRect(viewportData);
    }
}
export default new EditModeService()
