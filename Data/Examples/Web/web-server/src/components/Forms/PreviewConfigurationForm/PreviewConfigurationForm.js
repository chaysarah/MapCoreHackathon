import React, { PureComponent } from "react";
import cn from './PreviewConfigurationForm.module.css';
import Select from '../../Select/Select';
import Input from '../../Input/Input';
import { fieldsValidation, validationFunctions, validationMessages } from './PreviewConfigurationFormFields';
import ApplicationContext from '../../../context/applicationContext';
import editIcon from '../../../assets/images/edit.svg';

export default class PreviewConfigurationForm extends PureComponent {
    static contextType = ApplicationContext;

    constructor() {
        super();
        this.state = {
            errors: {},
        };
    }

    componentDidMount() {
        let mapScaleFactor = JSON.parse(localStorage.getItem("mapScaleFactor"));

        this.props.setParentHook(this.getConfigFormData);
        const fields = {
            MapScaleFactor: mapScaleFactor,
        }
        this.setState({ ...fields })
    }

    handleInputChange = (e) => {
        const inputValue = isNaN(e.target.value) ? e.target.value : parseFloat(e.target.value);
        this.setState({
            [e.target.name]: inputValue
            , [e.target.name + "Changed"]: true
        });
    }

    getInput(name, label, options) {
        return (
            <div className={cn.Row}>
                <Input
                    error={this.state.errors[name]}
                    mandatory={options.mandatory}
                    label={label}
                    name={name}
                    maxLength={options.maxLength || null}
                    value={this.state[name]}
                    type={options.type || "text"}
                    onFocus={options.onFocus || null}
                    onChange={this.handleInputChange}
                    readOnly={options.readOnly ?? this.state.readOnly} />
            </div>
        );
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
        this.setState({ errors });
        return Object.keys(errors).length > 0 ? false : true;
    }
    getConfigFormData = () => {
        let data = {};
        const isValid = this.validateForm();
        if (isValid) {
            data.MapScaleFactor = this.state.MapScaleFactor;
            localStorage.setItem('mapScaleFactor', this.state.MapScaleFactor);
        }
        return data;
    }

    render() {
        return (
            <div >
                {this.getInput("MapScaleFactor", this.context.dict.previewScaleFactor, { type: 'number', maxLength: '20' })}
            </div>
        );
    }
}
