import React, { PureComponent } from "react";
import config from '../../../config';
import { SaveMapToPreview, SetActionDtmMapPreview, SetCameraData, SetErrorInPreview } from "../../../redux/MapContainer/MapContainerAction";
import { connect } from "react-redux";
import { MapCoreService } from "mapcore-lib";

class About extends PureComponent {

    constructor(props) {
        super(props);
        this.state = {
            detailsAbout: this.props.detailsAbout
        };
    }
    async getCapability() {
        await MapCoreService.getStrServiceProviderName((providerName) => {
            this.setState({ detailsAbout: providerName });
        }, config.urls.getCapabilities);
    }
    render() {
        this.getCapability();
        return (
            <div>
                <div>{this.state.detailsAbout}</div>
                <div>{"(MapCore viewer version: " + window.MapCore.IMcMapDevice.GetVersion() + ")"}</div>
            </div>
        )
    }
}
const mapStateToProps = (state) => {
    return {
        mapToPreview: state.MapContainerReducer.mapToPreview,
        activeMapPreview: state.MapContainerReducer.activeMapPreview,
        activeDtmMapPreview: state.MapContainerReducer.activeDtmMapPreview,
        prevCameraData: state.MapContainerReducer.prevCameraData,
        errorInPreview: state.MapContainerReducer.errorInPreview,
        openMapService: state.MapContainerReducer.openMapService,
    }
};
const mapDispatchToProps = (dispacth) => {
    return {
        setMapPreview: () => dispacth(SaveMapToPreview({})),
        SetActionDtmMapPreview: (activeMapPreview) => dispacth(SetActionDtmMapPreview(activeMapPreview)),
        SetCameraData: (prevCameraData) => dispacth(SetCameraData(prevCameraData)),
        SetErrorInPreview: (error) => dispacth(SetErrorInPreview(error)),

    }
};
export default connect(mapStateToProps, mapDispatchToProps, null, { forwardRef: true })(About);
