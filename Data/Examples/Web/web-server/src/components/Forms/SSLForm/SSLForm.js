import React, { PureComponent } from "react";
import cn from './SSLForm.module.css';
import ApplicationContext from '../../../context/applicationContext';
import Input from '../../Input/Input';
import { fieldsInfo, fieldsValidation, validationFunctions, validationMessages } from './SSLFormField';

export default class SSLForm extends PureComponent {

    static contextType = ApplicationContext;

    constructor() {
        super();
        this.state = {
            layerType: '',
            errors: {},
        };
    }

    componentDidMount() {
        this.setDefaultFieldsStateByType();
        this.props.setParentHook(this.getImportFormData);
    }

    handleInputChange = (e) => {
        this.setState({ [e.target.name]: e.target.value })
    }

    getInput(name, label, options) {
        return (
            <div className={cn.Row}>
                <Input
                    mandatory={options.mandatory}
                    error={this.state.errors[name]}
                    label={label}
                    name={name}
                    value={this.state[name]}
                    type={options.type || "text"}
                    onFocus={options.onFocus || null}
                    onChange={this.handleInputChange} />
            </div>
        );
    }

    setDefaultFieldsStateByType() {
        let data = this.context.getSSLData();
        this.setState({ data })
        const fields = {
            certitficateFileName: data?.certitficateFileName,
            privateKeyFileName: data?.privateKeyFileName,
            dpKeyExchangeFileName: data?.dpKeyExchangeFileName,
        }
        this.setState({ ...fields });
    }

    getImportFormData = () => {
        let isValid = this.validateForm();
        if (isValid) {
            this.props.isValid(true);
            this.context.setSSLData({
                certitficateFileName: this.state.certitficateFileName,
                privateKeyFileName: this.state.privateKeyFileName,
                dpKeyExchangeFileName: this.state.dpKeyExchangeFileName,
            })
            return this.state.data;
        } else {
            this.props.isValid(false);
        }
    }

    validateForm() {
        const errors = {};
        Object.keys(fieldsValidation).forEach(field => {
            // Check only for fields that are inside the state (we put those fiels in) setDefaultFieldsStateByType
            if (this.state.hasOwnProperty(field)) {
                const valueToValidate = this.state[field];
                const validationsToRun = fieldsValidation[field];
                validationsToRun.forEach(validation => {
                    const isValid = validationFunctions[validation](valueToValidate);
                    if (!isValid) {
                        errors[field] = validationMessages[validation];
                    }
                })
            }
        });
        console.log(errors);
        this.setState({ errors });
        return Object.keys(errors).length > 0 ? false : true;
    }

    render() {
        return (
            <div className={cn.Wrapper}>
                <div className={cn.Split}>
                    {this.getInput('certitficateFileName', this.context.dict.certitficateFileName, { mandatory: true })}
                    {this.getInput('privateKeyFileName', this.context.dict.privateKeyFileName, { mandatory: true })}
                    {this.getInput('dpKeyExchangeFileName', this.context.dict.dpKeyExchangeFileName, { mandatory: true })}
                </div>
            </div>
        );
    }
}
