import { useEffect } from "react";
import { Properties } from "../../../../dialog";
import { runCodeSafely } from "../../../../../../common/services/error-handling/errorHandler";
import { TabInfo, TabType } from "../../../../shared/tabCtrls/tabModels";
import NestedTabsCtrl from "../../../../shared/tabCtrls/nestedTabsCtrl";
import LineOfSight, { LineOfSightProperties } from "./lineOfSight";
import { AreaOfSightProperties } from "./areaOfSight/structs";
import AreaOfSight from "./areaOfSight/areaOfSight";

export class SightProperties implements Properties {
    LineOfSightProperties: LineOfSightProperties;
    AreaOfSightProperties: AreaOfSightProperties;

    static getDefault(p: any): SightProperties {

        let defaults: SightProperties = {
            LineOfSightProperties: LineOfSightProperties.getDefault(p),
            AreaOfSightProperties: AreaOfSightProperties.getDefault(p),
        }

        return defaults;
    }
};

export default function Sight(props: { tabInfo: TabInfo }) {
    const tabTypes: TabType[] = [
        { index: 0, header: 'Line Of Sight', propertiesClass: LineOfSightProperties, component: LineOfSight },
        { index: 1, header: 'Area Of Sight', propertiesClass: AreaOfSightProperties, component: AreaOfSight },
    ]

    return (
        <NestedTabsCtrl
            tabTypes={tabTypes}
            nestedTabName='sight'
            lastTabInfo={props.tabInfo}
        />
    )
}
