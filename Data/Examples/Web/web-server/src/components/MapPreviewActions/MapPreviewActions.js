import React, { Component } from 'react';
import cn from './MapPreviewActions.module.css';
import ApplicationContext from '../../context/applicationContext';
import close from "../../assets/images/close.svg";
import { mapActions } from "../../config";
import Action from './Action';
import { connect } from "react-redux";
import { SetActionMapPreview } from '../../redux/MapContainer/MapContainerAction';

class MapPreviewActions extends Component {
    static contextType = ApplicationContext;

    render() {
        let actions = this.context.mapToPreview.mapActions;

        return (
            <div className={cn.PreviewActions}>
                <div className={cn.ActionsWrapper}>
                    <span className={cn.ActionsRectangle}>
                        <Action
                            label={'2D'}
                            onClick={this.props.onActionClick}
                            name={mapActions.MAP}
                            active={this.props.activeMapPreview}
                            visible={
                                this.context.mapToPreview.mapActions[mapActions.MAP]
                            }
                        />
                        <Action
                            onClick={this.props.onActionClick}
                            name={mapActions.THREE_D}
                            label={'3D'}
                            active={this.props.activeMapPreview}
                            visible={
                                this.context.mapToPreview.mapActions[mapActions.THREE_D]
                            }
                        />
                    </span>
                    <div className={cn.ActionDtm} >
                        <Action
                            onClick={this.props.onActionClick}
                            name={mapActions.SHOW_DTM_MAP}
                            label={'DTM'}
                            active={this.props.activeDtmMapPreview}
                            visible={
                                this.context.mapToPreview.mapActions[mapActions.SHOW_DTM_MAP]
                            }
                        />
                    </div>
                    {/* <Action
                        onClick={this.props.onActionClick}
                        name={mapActions.DESCRIPTION}
                        label={mapActions.DESCRIPTION}
                        active={this.props.activeMapPreview}
                        visible={
                            this.context.mapToPreview.mapActions[mapActions.DESCRIPTION]
                        }
                    /> */}
                    {/*<Action
                        onClick={this.props.onActionClick}
                        name={mapActions.DATA}
                        label={mapActions.DATA}
                        active={this.props.activeMapPreview}
                        enable={
                            this.context.mapToPreview.mapActions[mapActions.DATA]
                        }
                    />*/}
                </div>
                <div className={cn.CloseWrapper}>
                    <a
                        onClick={this.props.onCloseClick}
                        className={cn.CloseBtn}>
                        <img className={cn.CloseIcon} src={close} alt='close' />
                    </a>
                </div>
            </div>
        );
    }
}
const mapDispatchToProps = (dispacth) => {
    return {
        SetActionMapPreview: (activeMapPreview) => dispacth(SetActionMapPreview(activeMapPreview)),
    }
};
const mapStateToProps = (state) => {
    return {
        activeMapPreview: state.MapContainerReducer.activeMapPreview,
        activeDtmMapPreview: state.MapContainerReducer.activeDtmMapPreview,
    }
};
export default connect(mapStateToProps, mapDispatchToProps)(MapPreviewActions);