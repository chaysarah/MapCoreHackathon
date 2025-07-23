import { useEffect, useRef, useState } from "react";
import { TabPanel, TabView } from "primereact/tabview";
import { Button } from "primereact/button";
import { useDispatch, useSelector } from "react-redux";
import _ from "lodash";
import { runCodeSafely } from "../../../../../../common/services/error-handling/errorHandler";
import Params, { ParamsProperties } from "./params";
import General, { GeneralProperties } from "./general";
import Heights, { HeightsProperties } from "./heights";
import Sight, { SightProperties } from "./sight";
import RayIntersection, { RayIntersectionProperties } from "./rayIntersection";
import RasterAndTraversability, { RasterAndTraversabilityProperties } from "./rasterAndTraversability";
import './styles/spatialQueries.css';
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { Checkbox } from "primereact/checkbox";
import { Fieldset } from "primereact/fieldset";
import { AppState } from "../../../../../../redux/combineReducer";
import { TabType } from "../../../../shared/tabCtrls/tabModels";
import TabsParentCtrl from "../../../../shared/tabCtrls/tabsParentCtrl";
import { TreeNodeModel } from "../../../../../../services/tree.service";
import { setSpatialQueriesResultsObjects } from "../../../../../../redux/MapWorldTree/mapWorldTreeActions";

const tabTypes: TabType[] = [
    { index: 0, header: 'Params', propertiesClass: ParamsProperties, component: Params },
    { index: 1, header: 'General', propertiesClass: GeneralProperties, component: General },
    { index: 2, header: 'Heights', propertiesClass: HeightsProperties, component: Heights },
    { index: 3, header: 'Ray Intersection', propertiesClass: RayIntersectionProperties, component: RayIntersection },
    { index: 4, header: 'Sight', propertiesClass: SightProperties, component: Sight },
    { index: 5, header: 'Raster and Traversability', propertiesClass: RasterAndTraversabilityProperties, component: RasterAndTraversability },
]

export default function SpatialQueriesForm(props: { viewport: MapCore.IMcSpatialQueries }) {
    const dispatch = useDispatch();
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    let spatialQueriesResultsObjects = useSelector((state: AppState) => state.mapWorldTreeReducer.spatialQueriesResultsObjects);
    let mapWorldTree = useSelector((state: AppState) => state.mapWorldTreeReducer.mapWorldTree);
    let spatialQueriesResultsObjectsRef = useRef(spatialQueriesResultsObjects);

    useEffect(() => {
        const root = document.documentElement;
        let pixelWidth = window.innerHeight * 0.85 * globalSizeFactor;
        root.style.setProperty('--spatial-queries-dialog-width', `${pixelWidth}px`);
        return () => {
            let objects = spatialQueriesResultsObjectsRef.current.objects;
            spatialQueriesResultsObjectsRef.current.removeObjectsCB(objects);
            dispatch(setSpatialQueriesResultsObjects({ queryName: null, objects: [], removeObjectsCB: (objects: MapCore.IMcObject[]) => { } }))
        }
    }, [])
    useEffect(() => {
        spatialQueriesResultsObjectsRef.current = spatialQueriesResultsObjects;
    }, [spatialQueriesResultsObjects])

    return (
        <div className='form__column-container'>
            <TabsParentCtrl
                parentName='SpatialQueriesForm'
                tabTypes={tabTypes}
                getDefaultFuncProps={{ mcCurrentSpatialQueries: props.viewport, mapWorldTree }}
                tabViewHeight={60}
            />
        </div>
    );
}
