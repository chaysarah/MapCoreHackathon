import _ from 'lodash';

import { MapCoreData, MapCoreService, OpenMapService, ScanPropertiesBase, ViewportService } from 'mapcore-lib';
import store from '../redux/store';
import { resizeViewport, setViewportPosition } from '../redux/mapWindow/mapWindowAction';
import EditModePropertiesBase from '../propertiesBase/editModePropertiesBase';
import MapCorePropertiesBase from '../propertiesBase/mapCorePropertiesBase';
import { runAsyncCodeSafely, runCodeSafely } from '../common/services/error-handling/errorHandler';
import ObjectWorldTreePropertiesBase from '../propertiesBase/ObjectWorldTreePropertiesBase';
import ObjectPropertiesBase from '../propertiesBase/objectPropertiesBase';
import TexturePropertiesBase from '../propertiesBase/texturePropertiesBase';
import LayerPropertiesBase from '../propertiesBase/layerPropertiesBase';

export const getState = () => store.getState()


class generalService {
  mapCorePropertiesBase: MapCorePropertiesBase;
  layerPropertiesBase: LayerPropertiesBase;
  ObjectProperties: ObjectPropertiesBase;
  TextureProperties: TexturePropertiesBase;
  EditModePropertiesBase!: EditModePropertiesBase;
  ScanPropertiesBase: ScanPropertiesBase;
  ErrorDisplayAlert: boolean = true;
  ObjectWorldTreeProperties: ObjectWorldTreePropertiesBase;
  OpenMapService: OpenMapService;

  calcSizeAndPositionCanvases = () => {
    runCodeSafely(() => {
      if (MapCoreData.viewportsData.length == 0) {
        return
      }
      let CanvasesInRow: number, CanvasesInColumn: number;
      CanvasesInRow = Math.ceil(Math.sqrt(MapCoreData.viewportsData.length));
      CanvasesInColumn = Math.ceil(MapCoreData.viewportsData.length / CanvasesInRow);
      const width: number = (window.innerWidth - 10) / CanvasesInRow - 10;
      const height: number = ((window.innerHeight - 115) / CanvasesInColumn - 15);
      let x: number = 4; let y: number = 4; let k: number = 0;
      store.dispatch(resizeViewport({ width: width, height: height - 40 }))
      MapCoreService.resizeAllViewport()
      for (let i: number = 0; i < CanvasesInColumn; i++) {
        x = 0;
        for (let j: number = 0; j < CanvasesInRow; j++) {
          store.dispatch(setViewportPosition(MapCoreData.viewportsData[k].id, { x, y }));
          x = x + width + 3;
          k++;
          if (k == MapCoreData.viewportsData.length) {
            return;
          }
        }
        y = y + height + 3
      }
    }, "generalService.calcSizeAndPositioinCanvases")
  }

  initBaseProperties() {
    this.ObjectProperties = new ObjectPropertiesBase();
    this.TextureProperties = new TexturePropertiesBase();
    this.EditModePropertiesBase = new EditModePropertiesBase();
    this.mapCorePropertiesBase = new MapCorePropertiesBase();
    this.layerPropertiesBase = new LayerPropertiesBase();
    this.ScanPropertiesBase = new ScanPropertiesBase();
    this.ObjectWorldTreeProperties = new ObjectWorldTreePropertiesBase();
  }

  createAnddownloadFile(fileName: string, content: any = '', fileType: string = '.txt') {
    if (fileName.charAt(fileName.length - 1) == '/') {
      fileName = fileName.substr(0, fileName.length - 1);
    }
    fileName = `${fileName}${fileType}`
    const blob = new Blob([content], { type: 'text/plain' });
    const url = URL.createObjectURL(blob);
    let HTMLElementFile: HTMLElement = document.createElement('a');
    (HTMLElementFile as HTMLAnchorElement).href = url;
    (HTMLElementFile as HTMLAnchorElement).download = fileName;
    document.body.appendChild(HTMLElementFile);
    HTMLElementFile.click();
    document.body.removeChild(HTMLElementFile);
    URL.revokeObjectURL(url);
    setTimeout(() => {
      window.close();
    }, 2000)
  }
  getFileSourceByUrl = async (url: string) => {
    let fileSource: MapCore.SMcFileSource;
    await runAsyncCodeSafely(async () => {
      const response = await fetch(url);
      const arrayBuffer = await response.arrayBuffer();
      let uint8Array = new Uint8Array(arrayBuffer);
      fileSource = new MapCore.SMcFileSource(uint8Array, true)
    }, 'SpatialQueriesForm/Heights.getFileSourceByUrl')
    return fileSource;
  }

  //#region Names Functions
  getObjectName(obj: any, type: string) {
    let name: string = `(${obj.id}) ${type}`
    //nodeType.OBJECT
    switch (type) {
      case "Viewport":
        name = "(" + obj.id + ") Map Viewport " + (ViewportService.viewport3D(obj.id) ? "[3D]" : "[2D]")
        break;
      case 'IMcTraversabilityMapLayer':
        name = `(${obj.GetID()}) IMcTraversabilityMapLayer`;
        break;
      default:
        break;
    }
    return name;
  }
  //#endregion
}


export default new generalService();
