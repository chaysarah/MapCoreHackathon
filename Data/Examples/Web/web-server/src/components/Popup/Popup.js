import React, { PureComponent } from 'react';
import cn from './Popup.module.css';
import closeImg from '../../assets/images/close.svg';
import popupTypes from '../../config';


export default class Popup extends PureComponent {
    EscKey = 27;
    EnterKey = 13;
    componentDidMount() {
        document.addEventListener('keydown', this.handleKeyDown);
    }

    componentWillUnmount() {
        document.removeEventListener('keydown', this.handleKeyDown);
    }

    handleKeyDown = (e) => {
        if (e.keyCode === this.EnterKey && this.props.buttonOk.toUpperCase() == 'OK') {
            this.onOkClicked(e);
        }
        else if (e.keyCode === this.EscKey && this.props.onCancel) {
            this.onCancelClicked(e);
        }
    }

    onOkClicked = (e) => {
        e.preventDefault();
        if (this.props.onOk) {
            this.props.onOk();
        }
    }

    onCancelClicked = (e) => {
        e.preventDefault();
        if (this.props.onCancel) {
            this.props.onCancel();
        }
    }

    onAdvancedClicked = (e) => {
        e.preventDefault();
        if (this.props.onAdvanced) {
            this.props.onAdvanced();
        }
    }

    getXBtn() {
        return (
            this.props.hideXButton ? null :
                <a className={cn.Close} onClick={this.onCancelClicked}>
                    <img className={cn.closeBtn} src={closeImg} />
                </a>
        );
    }
    getFooter() {
        let okButton = null;
        let cancelButton = null;
        let linkAdvanced = null;
        if (this.props.buttonOk) {
            okButton = <button
                onClick={this.onOkClicked}
                className={((!this.props.disabledOk) && (this.props.header == "Rename Group" || this.props.header == popupTypes.MOVE_TO_GROUP ||
                    this.props.header == "Add Group" || this.props.header.includes('Edit Layer'))) || this.props.readOnly ? `${cn.FormButton} ${cn.Apply} ${cn.ApplyFormButtonDisabled} ` : `${cn.FormButton} ${cn.Apply}`}
            >{this.props.buttonOk}</button>;
        }
        if (this.props.buttonCancel) {
            cancelButton = <button onClick={this.onCancelClicked} className={`${cn.FormButton}`}>{this.props.buttonCancel}</button>;
        }
        if (this.props.linkAdvanced) {
            linkAdvanced = <a onClick={this.onAdvancedClicked} className={`${cn.FormLink}`}>{this.props.linkAdvanced}</a>;
        }
        if (!okButton && !cancelButton) return null;
        return (
            <div className={cn.PopupFooter}>
                <div style={{ lineHeight: "40px" }}>
                    {linkAdvanced}
                </div>
                <div>
                    {cancelButton}
                    {okButton}
                </div>
            </div>
        );
    }

    getHeader() {
        return (
            <div className={cn.PopupHeader}>
                <div className={cn.PopupHeaderWrapper}>
                    <h2 className={cn.h2}>{this.props.header}</h2>
                    {this.getXBtn()}
                </div>
            </div>
        );
    }

    getBody() {
        const noBodyOverflowClass = this.props.noBodyOverflow ? cn.NoBodyOverFlow : '';

        const body = React.Children.count(this.props.children) > 0 ?
            (<div style={{ padding: `${this.props.size === "small" ? '30px 30px' : '30px 55px'}` }} className={`${cn.PopupBody} ${noBodyOverflowClass}`}>
                {this.props.children}
            </div>) : null;
        return body;
    }

    render() {
        const size = this.props.size ? cn[this.props.size] : '';
        return (this.props.children) ?
            (
                <div className={cn.Overlay}>
                    <div className={`${cn.Popup} ${size}`}>
                        {this.getHeader()}
                        {this.getBody()}
                        {this.getFooter()}
                    </div>
                </div>
            ) : null;
    }
}
