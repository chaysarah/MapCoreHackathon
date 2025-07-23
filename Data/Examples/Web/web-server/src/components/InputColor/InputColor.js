import React, { Component } from 'react'
import cn from './InputColor.module.css';
import validationX from '../../assets/images/close-red.svg';
import Tooltip from '../Tooltip/Tooltip';
import arrowRight from '../../assets/images/arrow-right.svg'


export default class Input extends Component {
    state = {
        isShowInfoTooltip: false
    }

    showInfoTooltip = e => {
        this.setState({ isShowInfoTooltip: true, clientX: e.target.getBoundingClientRect().left, clientY: e.target.getBoundingClientRect().top });
    }

    onChange = (e, name) => {
        if (e.target.value > 255 || e.target.value == '') {
            e.target.value = Math.floor(e.target.value / 10);
        }
        else if (e.target.value < 0) {
            e.target.value = 0;
        }
        if (String(e.target.value).length > 1 && String(e.target.value)[0] == '0') {
            e.target.value = Number(String(e.target.value).substring(1));
        }
        this.props.onChange(e, name)
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
        const readOnlyBorder = this.props.readOnly ? ` ${cn.readOnlyBorder}` : '';
        return (
            <div className={cn.Row}>
                <div className={`${cn.titleWrapper}${readOnly}`}>
                    <label className={`${cn.container}`}>
                        <input type='checkbox' name='isTransparentColor' onClick={this.props.handleCheckboxChange} checked={this.props.checked ? 'checked' : ''}></input>
                        <span className={`${cn.checkmark}`}></span>
                    </label>
                    <span className={`${cn.MainLabel}${mandatoyClass}`}>{this.props.label}{this.renderInfo()}</span>
                </div>
                <div className={`${cn.InputWrapper}${readOnly}`}>
                    <div className={`${cn.InnerInputWrapper}${readOnlyBorder}`}>
                        <div>
                            <b className={`${cn.Label}${mandatoyClass}`}>R</b>
                            <input
                                ref={this.props.parentRef || null}
                                value={this.props.value.r}
                                name={this.props.name}
                                className={`${cn.Input}${errorClass}${readOnly}${readOnlyBorder}`}
                                maxLength={3}
                                type={this.props.type || 'text'}
                                onFocus={this.props.onFocus}
                                onChange={(e) => this.onChange(e, 'r')}
                                // onChange={(e) => this.props.onChange(e, 'r')}
                                readOnly={this.props.readOnly}>
                            </input>
                        </div>
                        <div>
                            <b className={`${cn.Label}${mandatoyClass}`}>G</b>
                            <input
                                ref={this.props.parentRef || null}
                                value={this.props.value.g}
                                name={this.props.name}
                                className={`${cn.Input}${errorClass}${readOnly}${readOnlyBorder}`}
                                maxLength={this.props.maxLength || null}
                                type={this.props.type || 'text'}
                                onFocus={this.props.onFocus}
                                onChange={(e) => this.onChange(e, 'g')}
                                readOnly={this.props.readOnly}>
                            </input>
                        </div>
                        <div>
                            <b className={`${cn.Label}${mandatoyClass}`}>B</b>
                            <input
                                ref={this.props.parentRef || null}
                                value={this.props.value.b}
                                name={this.props.name}
                                className={`${cn.Input}${errorClass}${readOnly}${readOnlyBorder}`}
                                maxLength={this.props.maxLength || null}
                                type={this.props.type || 'text'}
                                onFocus={this.props.onFocus}
                                onChange={(e) => this.onChange(e, 'b')}
                                readOnly={this.props.readOnly}>
                            </input>
                        </div>
                        <div>
                            <b className={`${cn.Label}${mandatoyClass}`}>Precision</b>
                            <input
                                ref={this.props.parentRef || null}
                                value={this.props.value.precision}
                                name={this.props.name}
                                className={`${cn.Input}${errorClass}${readOnly}${readOnlyBorder}`}
                                maxLength={this.props.maxLength || null}
                                type={this.props.type || 'text'}
                                onFocus={this.props.onFocus}
                                onChange={(e) => this.onChange(e, 'precision')}
                                readOnly={this.props.readOnly}>
                            </input>
                        </div>
                    </div>
                    {this.props.link ?
                        <a className={cn.arrowRight} onClick={() => this.props.onClickTS()}>
                            <img src={arrowRight} />
                        </a>
                        : null}

                    {this.props.dots ?
                        <a  className={cn.enableEditText} onClick={() => this.props.onClickSSL()}>
                            ...
                        </a>
                        : null}

                    <img className={`${cn.ValidationImg}${errorClass}`} src={validationX} alt="" />
                </div>

                <div className={`${cn.ValidationMessage}${errorClass}`}>{this.props.error || ''}</div>
            </div>
        )
    }
}