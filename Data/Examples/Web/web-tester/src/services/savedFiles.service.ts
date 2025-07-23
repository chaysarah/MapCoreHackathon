import _ from 'lodash';

import { getEnumDetailsList, getEnumValueDetails } from "mapcore-lib";
import { TreeNodeModel } from "./tree.service";
import { runCodeSafely } from "../common/services/error-handling/errorHandler";
import store from "../redux/store";
import { objectWorldNodeType } from '../components/shared/models/tree-node.model';

export class SavedFileData {
    filePath: string;
    version: MapCore.IMcOverlayManager.ESavingVersionCompatibility | number;//number type is for informal version that not exist in MapCore.IMcOverlayManager.ESavingVersionCompatibility enum
    format: MapCore.IMcOverlayManager.EStorageFormat

    constructor(version: MapCore.IMcOverlayManager.ESavingVersionCompatibility | number, format: MapCore.IMcOverlayManager.EStorageFormat, filePath: string) {
        this.filePath = filePath;
        this.version = version;
        this.format = format;
    }
}

class SavedFilesService {

    private getEnumDetails() {
        const enumDetails = {
            EStorageFormat: getEnumDetailsList(MapCore.IMcOverlayManager.EStorageFormat),
            EVersion: getEnumDetailsList(MapCore.IMcOverlayManager.ESavingVersionCompatibility),
        };
        return enumDetails;
    }
    private getLatestVersion() {
        const versionsList = this.getEnumDetails().EVersion
        const latestVersion = getEnumValueDetails(MapCore.IMcOverlayManager.ESavingVersionCompatibility.ESVC_LATEST, versionsList);
        return latestVersion;
    }

    getObjectsOrSchemesVersion<T>(schemesOrObjects: T[], isObjects: boolean): { version: { name: string; code: number; theEnum: any; }, isAllSameVersion: boolean } {
        let finalVersion: { name: string; code: number; theEnum: any; } = null;
        let isAllSameCode: boolean = true;
        runCodeSafely(() => {
            const savedObjectOrSchemeDataMap: any = isObjects ? store.getState().objectWorldTreeReducer.savedObjectDataMap : store.getState().objectWorldTreeReducer.savedSchemeDataMap;
            const footerVersion = store.getState().objectWorldTreeReducer.footerVersion;
            if (footerVersion.code != -1) {//not Original Version
                finalVersion = footerVersion.theEnum == MapCore.IMcOverlayManager.ESavingVersionCompatibility.ESVC_LATEST ? this.getLatestVersion() : footerVersion;
            }
            else {
                const savedObjectsOrSchemes = schemesOrObjects.filter((schemeOrObject: T) => savedObjectOrSchemeDataMap.get(schemeOrObject))
                const savedObjectsOrSchemesVersions = savedObjectsOrSchemes.map((schemeOrObject: T) => typeof savedObjectOrSchemeDataMap.get(schemeOrObject).version == 'number' ? MapCore.IMcOverlayManager.ESavingVersionCompatibility.ESVC_LATEST : savedObjectOrSchemeDataMap.get(schemeOrObject).version);
                const savedVersionsEnumDetails = savedObjectsOrSchemesVersions.map((version: MapCore.IMcOverlayManager.ESavingVersionCompatibility) => getEnumValueDetails(version, this.getEnumDetails().EVersion));
                const isLatestExist = savedVersionsEnumDetails.some(versionEnum => versionEnum.theEnum == MapCore.IMcOverlayManager.ESavingVersionCompatibility.ESVC_LATEST);
                finalVersion = isLatestExist || savedObjectsOrSchemes.length == 0 ? this.getLatestVersion() : _.maxBy(savedVersionsEnumDetails, 'code');
                isAllSameCode = _.every(savedVersionsEnumDetails, v => v.code === savedVersionsEnumDetails[0]?.code);
            }
        }, 'SavedFilesService.getObjectsOrSchemesVersion')
        return { version: finalVersion, isAllSameVersion: isAllSameCode };
    }
    getSchemeOrObjectLabel(node: TreeNodeModel) {
        let finalLabel: string = node.label;
        runCodeSafely(() => {
            const savedSchemeOrObjectDataMap = node.nodeType == objectWorldNodeType.OBJECT_SCHEME ? store.getState().objectWorldTreeReducer.savedSchemeDataMap : store.getState().objectWorldTreeReducer.savedObjectDataMap;
            let isSchemeOrObjectExist = savedSchemeOrObjectDataMap.get(node.nodeMcContent);
            if (isSchemeOrObjectExist) {
                const enumDetails = this.getEnumDetails();
                const versionEnum = getEnumValueDetails(isSchemeOrObjectExist.version, enumDetails.EVersion)
                const formatEnum = getEnumValueDetails(isSchemeOrObjectExist.format, enumDetails.EStorageFormat)
                const labelDeyails = typeof isSchemeOrObjectExist.version == 'number' ? `(${isSchemeOrObjectExist.version})` : `(${versionEnum.name} (${versionEnum.code}), ${formatEnum.name})`;
                finalLabel = `${finalLabel} ${labelDeyails}`
            }
        }, 'SavedFilesService.getSchemeOrObjectLabel')
        return finalLabel;
    }
}
export default new SavedFilesService;