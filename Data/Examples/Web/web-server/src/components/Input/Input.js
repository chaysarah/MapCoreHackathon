import React, { Component } from 'react'
import cn from './Input.module.css';
import validationX from '../../assets/images/close-red.svg';
import Tooltip from '../Tooltip/Tooltip';
import arrowRight from '../../assets/images/arrow-right.svg'
import { parse } from '@fortawesome/fontawesome-svg-core';


export default class Input extends Component {
    state = {
        isShowInfoTooltip: false
    }

    showInfoTooltip = e => {
        this.setState({ isShowInfoTooltip: true, clientX: e.target.getBoundingClientRect().left, clientY: e.target.getBoundingClientRect().top });
    }


    // componentDidUpdate(prevProps, prevState) {
    //     if (prevProps.value !== this.props.value) {
    //         const trimmedNumber = this.props.value.replace(/^0+(?=\d)/, '');
    //         this.setState({ number: trimmedNumber });
    //     }
    // }


    onChange = event => {
        const input = event.target.value;
        let updatedNumber = '';

        // Remove leading zero if it's the first digit
        if (input.length === 2 && input[0] === '0') {
            updatedNumber = input[1];
        } else {
            updatedNumber = input;
        }

        const updatedEvent = {
            ...event,
            target: {
                ...event.target,
                value: updatedNumber,
                name: event.target.name
            },
        };
        setTimeout(() => {
            this.props.onChange(updatedEvent);
        }, 0);
    }

    hideTooltip = () => this.setState({ isShowInfoTooltip: false, clientX: null, clientY: null })

    renderInfo() {
        if (this.props.info) {
            return (
                <span className={cn.InfoImage} onMouseEnter={this.showInfoTooltip} onMouseLeave={this.hideTooltip}>
                    {
                        this.state.isShowInfoTooltip ?
                            (<Tooltip
                                title={this.props.label}
                                text={this.props.info}
                                x={this.state.clientX}
                                y={this.state.clientY}
                            />) : null
                    }
                </span>
            )
        }
    }

    render() {
        const mandatoyClass = this.props.mandatory ? ` ${cn.Mandatory}` : '';
        const errorClass = this.props.error ? ` ${cn.ShowError}` : '';
        const readOnly = this.props.readOnly ? ` ${cn.readOnly}` : '';
        return (
            <div className={cn.Row}>
                {this.props.checkboxLabel ?
                    <div className={`${cn.titleWrapper}${readOnly}`}>
                        <label className={`${cn.container}`}>
                            <input type='checkbox' onClick={this.props.handleCheckboxChange} checked={this.props.checked ? 'checked' : ''}></input>
                            <span className={`${cn.checkmark}`}></span>
                        </label>
                        <span className={`${cn.MainLabel}${mandatoyClass}`}>{this.props.label}{this.renderInfo()}</span>
                    </div>
                    :
                    <span className={`${cn.Label}${mandatoyClass}${readOnly}`}>{this.props.label}{this.renderInfo()}</span>
                }
                {this.props.noInput ? '' : <div className={cn.InputWrapper}>
                    <div className={cn.InnerInputWrapper}>
                        <input
                            ref={this.props.parentRef || null}
                            value={this.props.value}
                            name={this.props.name}
                            className={`${cn.Input}${errorClass}${readOnly}`}
                            maxLength={this.props.maxLength || null}
                            type={this.props.type || 'text'}
                            onFocus={this.props.onFocus}
                            onChange={this.onChange}
                            readOnly={this.props.readOnly}>
                        </input>

                        {this.props.link ?
                            <a className={cn.arrowRight} onClick={() => this.props.onClickTS()}>
                                <img src={arrowRight} />
                            </a>
                            : null}

                        {this.props.dots ?
                            <a className={cn.enableEditText} onClick={() => this.props.onClickSSL()}>
                                ...
                            </a>
                            : null}

                        <img className={`${cn.ValidationImg}${errorClass}`} src={validationX} alt="" />
                    </div>

                    <div className={`${cn.ValidationMessage}${errorClass}`}>{this.props.error || ''}</div>
                </div>}
            </div>
        )
    }
}