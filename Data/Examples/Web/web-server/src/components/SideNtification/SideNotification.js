import React, { PureComponent } from 'react';
import classNames from './SideNotification.module.css';
import ApplicationContext from '../../context/applicationContext';
// import infoIcon from '../../assets/images/infoFullYellow.svg';

export default class SideNotification extends PureComponent {
    static contextType = ApplicationContext;

    // renderIcon() {

    //     return <img className={classNames.Icon} src={infoIcon}></img>
    // }
    renderText(text) {
        return <div className={classNames.Text}>{text}</div>
    }

    Notification(text) {
        return (
            <div className={classNames.SingleWrapper}>
                {/* {this.renderIcon()} */}
                {this.renderText(text)}
                <span className={classNames.RemoveIcon}
                    onClick={() => this.context.removeSideNotification(text)}>
                </span>
            </div >
        );
    }

    render() {
        const Notifications = this.context.sideNotifications.map(notification => notification.text && this.Notification(notification.text));
        if (!this.context.sideNotifications || this.context.sideNotifications.length == 0) {
            return "";
        }

        return (
            <div className={classNames.Wrapper}>
                <span className={classNames.RemoveAllIcon} onClick={() => this.context.removeAllSideNotifications()}>
                    Clear All
                </span>
                {Notifications}
            </div>
        );
    }
}
