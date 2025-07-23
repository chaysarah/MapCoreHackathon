import React, { Component } from 'react'
import cn from './MapPreviewActions.module.css';

export default class Action extends Component {
    render() {
        const visibleClass = this.props.visible ? cn.Visible : '';
        const activeClass = this.props.active === this.props.name ? cn.Active : '';
        const activeClickClass = this.props.active === this.props.name ? cn.ActiveClick : '';
        
        return (
            <span className={`${cn.ActionWrapper} ${visibleClass}  ${activeClickClass}`} onClick={() => this.props.onClick(this.props.name)}>
                <a className={`${cn.Action} ${cn[this.props.name]} ${activeClass}`}></a>
                <span className={cn.Desc}>{this.props.label}</span>
            </span>
        );
    }
}