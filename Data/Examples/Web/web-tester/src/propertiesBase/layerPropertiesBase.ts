import { MapCoreData } from 'mapcore-lib';
import mapWorldTreeService from '../services/mapWorldTreeService';
import store from '../redux/store';
import MapTreeNode from '../components/shared/models/map-tree-node.model';
import { setMapWorldTree, setSelectedNodeInTree } from '../redux/MapWorldTree/mapWorldTreeActions';
import { runCodeSafely, runMapCoreSafely } from '../common/services/error-handling/errorHandler';

class LayerPropertiesBase {
    layerIReadCallback: MapCore.IMcMapLayer.IReadCallback;

    updateTreeRedux() {
        let buildedMapTree: MapTreeNode = mapWorldTreeService.buildTree();
        store.dispatch(setMapWorldTree(buildedMapTree));
    }

    constructor() {
        this.layerIReadCallback = new MapCoreData.layersCallback((pLayer: MapCore.IMcMapLayer, eStatus: MapCore.IMcErrors.ECode, strAdditionalDataString: string) => {
            runCodeSafely(() => {
                let isStandAloneExist = MapCoreData.standAloneLayers.find(p => p == pLayer);
                if (isStandAloneExist) {
                    MapCoreData.standAloneLayers = MapCoreData.standAloneLayers.filter(p => p != pLayer);
                    runMapCoreSafely(() => { isStandAloneExist.Release() }, "layerPropertiesBase.constructor => IMcBase.Release", true)
                }
                this.updateTreeRedux()
            }, 'layerPropertiesBase.constructor')
        }, (pOldLayer: MapCore.IMcMapLayer, pNewLayer: MapCore.IMcMapLayer, eStatus: MapCore.IMcErrors.ECode, strAdditionalDataString: string) => {
            runCodeSafely(() => {
                let isStandAloneExist = MapCoreData.standAloneLayers.find(p => p == pOldLayer);
                if (isStandAloneExist) {
                    MapCoreData.standAloneLayers = MapCoreData.standAloneLayers.filter(p => p != pOldLayer);
                    runMapCoreSafely(() => { isStandAloneExist.Release() }, "layerPropertiesBase.constructor => IMcBase.Release", true)
                    runMapCoreSafely(() => { pNewLayer.AddRef() }, "layerPropertiesBase.constructor => IMcBase.Release", true)
                    MapCoreData.standAloneLayers.push(pNewLayer);
                }
                this.updateTreeRedux()
            }, 'layerPropertiesBase.constructor')
        });
    }
}
export default LayerPropertiesBase 