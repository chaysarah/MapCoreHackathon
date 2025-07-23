
import { Fieldset } from "primereact/fieldset";
import { ListBox } from "primereact/listbox";
import { useSelector } from "react-redux";
import { Button } from "primereact/button";
import { Dialog } from "primereact/dialog";
import { ConfirmDialog } from "primereact/confirmdialog";

import { TypeToStringService } from "mapcore-lib";
import MapLayer, { MapLayerProperties, MapLayerPropertiesState } from "../../mapLayerForms/mapLayer";
import { Properties } from "../../../../dialog";
import ScanSQParams from "../../../../mapToolbarActions/mapOperations/scan/scanSQParams";
import { TabInfo, TabType } from "../../../../shared/tabCtrls/tabModels";
import TabsParentCtrl from "../../../../shared/tabCtrls/tabsParentCtrl";
import { AppState } from "../../../../../../redux/combineReducer";
import { runCodeSafely, runMapCoreSafely } from "../../../../../../common/services/error-handling/errorHandler";


export class ParamsProperties implements Properties {
    mcCurrentSpatialQueries: MapCore.IMcSpatialQueries;
    dialogVisible: boolean;
    confirmDialogMessage: string;
    confirmDialogVisible: boolean;
    sqParams: MapCore.IMcSpatialQueries.SQueryParams;
    querySecondaryDtms: { label: string, nodeMcContent: MapCore.IMcDtmMapLayer }[];
    selectedSecondaryDtm: { label: string, nodeMcContent: MapCore.IMcDtmMapLayer };

    static getDefault(p: any): ParamsProperties {
        let { mcCurrentSpatialQueries } = p;

        let querySecondaryDtms: MapCore.IMcDtmMapLayer[] = [];
        runMapCoreSafely(() => {
            querySecondaryDtms = mcCurrentSpatialQueries.GetQuerySecondaryDtmLayers();
        }, 'SpatialQueriesForm/Params.getDefault => IMcSpatialQueries.GetQuerySecondaryDtmLayers', true);
        let querySecondaryDtmsNodes: { label: string, nodeMcContent: MapCore.IMcDtmMapLayer }[] = querySecondaryDtms.map((dtmLayer, i) => { return { label: `${i + 1}) ${TypeToStringService.getLayerTypeByTypeNumber(dtmLayer.GetLayerType())}`, nodeMcContent: dtmLayer } })

        let defaults: ParamsProperties = {
            mcCurrentSpatialQueries: mcCurrentSpatialQueries,
            dialogVisible: false,
            confirmDialogMessage: '',
            confirmDialogVisible: false,
            sqParams: new MapCore.IMcSpatialQueries.SQueryParams(),
            querySecondaryDtms: querySecondaryDtmsNodes,
            selectedSecondaryDtm: null,
        }

        return defaults;
    }
};

export default function Params(props: { tabInfo: TabInfo }) {
    let { saveData, setApplyCallBack, setPropertiesCallback, tabProperties } = props.tabInfo;
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const cursorPos = useSelector((state: AppState) => state.mapWindowReducer.cursorPos);
    let mapWorldTree = useSelector((state: AppState) => state.mapWorldTreeReducer.mapWorldTree);
    const handleApplyFunc = useSelector((state: AppState) => state.mapWorldTreeReducer.handleApplyFunc);

    const tabTypes: TabType[] = [
        { index: 0, header: 'MapLayer', statePropertiesClass: MapLayerPropertiesState, propertiesClass: MapLayerProperties, component: MapLayer },
    ]

    const setSqParams = (sqParams: MapCore.IMcSpatialQueries.SQueryParams) => {
        setPropertiesCallback("sqParams", sqParams)
    }

    const handleShowMapLayerFormClick = () => {
        runCodeSafely(() => {
            if (!tabProperties.selectedSecondaryDtm) {
                setPropertiesCallback({ confirmDialogVisible: true, confirmDialogMessage: 'Missing Selected Dtm Map Layer' })
            }
            else {
                setPropertiesCallback('dialogVisible', true)
            }
        }, 'SpatialQueries/Params.handleShowMapLayerFormClick')
    }

    return (
        <div className="form__row-container">
            <Fieldset style={{ width: '50%' }} legend='SQuery Params'>
                <ScanSQParams sqParams={tabProperties.sqParams} setSQParamsCallback={setSqParams} />
            </Fieldset>
            <Fieldset className="form__align-items-end" style={{ width: '50%' }} legend='Query Secondary Dtm Layers'>
                <ListBox emptyMessage={() => { return <div></div> }} listStyle={{ minHeight: `${globalSizeFactor * 20}vh`, maxHeight: `${globalSizeFactor * 20}vh`, width: `${globalSizeFactor * 30}vh` }}
                    name='selectedSecondaryDtm' value={tabProperties.selectedSecondaryDtm} onChange={saveData} optionLabel="label" options={tabProperties.querySecondaryDtms}
                />
                <Button label='Show Map Layer Form' onClick={handleShowMapLayerFormClick} />
            </Fieldset>

            <Dialog
                footer={<Button label='Apply' onClick={() => {
                    handleApplyFunc()
                }} />}
                modal={false}
                visible={tabProperties.dialogVisible}
                onHide={() => setPropertiesCallback('dialogVisible', false)}
            >
                <TabsParentCtrl
                    parentName='MapLayerForm'
                    tabTypes={tabTypes}
                    getDefaultFuncProps={{ currentLayer: tabProperties.selectedSecondaryDtm, cursorPos, mapWorldTree, isSpatialQueriesForm: true }}
                    selectedNode={tabProperties.selectedSecondaryDtm}
                />
            </Dialog>

            <ConfirmDialog
                contentClassName='form__confirm-dialog-content'
                message={tabProperties.confirmDialogMessage}
                header=''
                footer={<div></div>}
                visible={tabProperties.confirmDialogVisible}
                onHide={e => { setPropertiesCallback('confirmDialogVisible', false) }}
            />
        </div>
    )
}
