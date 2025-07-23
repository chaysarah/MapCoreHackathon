import React, { PureComponent } from 'react';
import cn from './AdvancedForm.module.css';
import ApplicationContext from '../../../context/applicationContext';
import Input from '../../Input/Input';
import InputColor from '../../InputColor/InputColor';
import Select from '../../Select/Select';
import { fieldsInfo, fieldsValidation, validationFunctions, validationMessages } from './AdvancedFormFields';

export default class AdvancedForm extends PureComponent {

    static contextType = ApplicationContext;

    constructor() {
        super();
        this.state = {
            layerType: '',
            errors: {},
            isCheckedOptiMin: false,
            isCheckedMaxVisible: false,
            isCheckedMinSize: false,
        };
    }

    componentDidMount() {
        this.setDefaultFieldsStateByType();
        this.props.setParentHook(this.getImportFormData);
    }

    handleInputChange = (e) => {
        this.setState({ [e.target.name]: e.target.value })
        if (this.state.popupType == 'edit') {
            this.props.isChanged();
        }
    }

    handleInputsColortChange = (e, element) => {
        let value = e.target.value
        this.setState({ ...this.state, [e.target.name]: { ...this.state[e.target.name], [element]: value } })
        if (this.state.popupType == 'edit') {
            this.props.isChanged();
        }
    }

    handleCheckboxChange = (e) => {
        this.setState({ [e.target.name]: !this.state.isTransparentColor }, () => {
            if (this.state.isTransparentColor) {
                this.setState({ ...this.state, transparentColor: { ...this.state.transparentColor, a: 255 } })
            }
            else {
                this.setState({ ...this.state, transparentColor: { ...this.state.transparentColor, a: 0 } })
            }
        })
        if (this.state.popupType == 'edit') {
            this.props.isChanged();
        }
    }

    handleDropDownChange = (e, name) => {
        e = e == "True" ? true : false;
        this.setState({ [name]: e })
        if (this.state.popupType == 'edit') {
            this.props.isChanged();
        }
    }
    handleMinSizeCheckboxChange = () => {
        if (this.state.isCheckedOptiMin) {
            this.setState({ isCheckedMinSize: !this.state.isCheckedMinSize });
        }
    }
    handleMaxVisCheckboxChange = () => {
        if (this.state.isCheckedOptiMin) {
            this.setState({ isCheckedMaxVisible: !this.state.isCheckedMaxVisible });
        }
    }
    getInputsColor(name, label, options) {
        return (
            <div className={cn.Row}>
                <InputColor
                    error={this.state.errors[name]}
                    label={label}
                    name={name}
                    value={this.state[name]}
                    type={options.type || 'text'}
                    onFocus={options.onFocus || null}
                    onChange={this.handleInputsColortChange}
                    readOnly={options.readOnly}
                    handleCheckboxChange={this.handleCheckboxChange}
                    maxLength={3}
                    info={fieldsInfo[name]}
                    checked={this.state.isTransparentColor} />
            </div>
        );
    }

    getInput(name, label, options) {
        return (
            <div className={cn.Row}>
                <Input
                    noInput={options.noInput}
                    error={this.state.errors[name]}
                    label={label}
                    name={name}
                    value={this.state[name]}
                    type={options.type || 'text'}
                    onFocus={options.onFocus || null}
                    onChange={this.handleInputChange}
                    info={fieldsInfo[name]}
                    readOnly={options.readOnly}
                    checkboxLabel={options.checkboxLabel}
                    handleCheckboxChange={options.handleCheckboxChange}
                    checked={options.checked}
                    isPositive={options.isPositive} />
            </div>
        );
    }

    getTrueFalseDropDown(name, label) {
        const filesArr = [{ code: 'True', value: 'True' }, { code: 'False', value: 'False' },];
        const defaultCode = this.state[name] ? "True" : "False";
        if (defaultCode == undefined) {
            return ''
        }
        const dropDownData = {
            options: [...filesArr],
            defaultCode,
            label,
            fieldNames: { code: 'code', value: 'value' },
            isMandatoy: false,
            onChange: (e => this.handleDropDownChange(e, name)),
            error: this.state.errors[name],
            info: fieldsInfo[name],
            // readOnly: this.state.readOnly,
        };

        return (
            <div className={cn.Row}>
                <Select {...dropDownData} />
            </div>
        )
    }
    getOptimizationInput(name, label, options) {
        return <span>
            {this.getInput('OptimizationOfHidingObjects', 'Optimization of Hiding Objects', {
                noInput: true, checkboxLabel: true,
                checked: this.state.isCheckedOptiMin,
                handleCheckboxChange: (e) => { this.setState({ isCheckedOptiMin: e.target.checked }) }
            })}
            <div style={{ borderStyle: 'solid', borderRadius: 'var(--button-border-radius)', padding: '9px', borderWidth: 'thin', padding: '10px', margin: '10px' }}>
                {this.getInput(name, label, options)}
                {this.getOptimizationInputChildren()}
            </div>
        </span>
    }
    getOptimizationInputChildren() {
        return (<>
            {this.getInput('maxVisiblePointsPerTile', this.context.dict.maxVisiblePointsPerTile, { type: 'number', readOnly: !this.state.isCheckedMaxVisible || !this.state.isCheckedOptiMin, checkboxLabel: true, checked: this.state.isCheckedMaxVisible, handleCheckboxChange: this.handleMaxVisCheckboxChange })}
            {this.getInput('minSizeForObjVisibility', this.context.dict.minSizeForObjVisibility, { type: 'number', readOnly: !this.state.isCheckedMinSize || !this.state.isCheckedOptiMin, checkboxLabel: true, checked: this.state.isCheckedMinSize, handleCheckboxChange: this.handleMinSizeCheckboxChange })}
        </>)
    }
    getOptimizationOfHidingObjects() {
        return (
            <>
                <div style={{ margin: '8px' }}>
                    {this.getInput('maxVerticesPerTile', this.context.dict.maxVerticesPerTile, { type: 'number' })}
                </div>
                {this.getOptimizationInput('optimizationMinScale', this.context.dict.optimizationMinScale, { type: 'number', readOnly: !this.state.isCheckedOptiMin })}
            </>
        )
    }
    setDefaultFieldsStateByType() {
        const popupType = this.context.getAdvancedPopupType();
        const data = this.context.getAdvancedData();
        const type = this.context.getAdvancedDataType();
        this.setState({ data, type: type, popupType });
        const fields = {};
        if (type.includes('VECTOR') || type == "S-57") {
            let tmpData = this.props.filledFields;
            if (!tmpData) {
                fields.maxVerticesPerTile = data?.maxVerticesPerTile;
                fields.optimizationMinScale = data?.optimizationMinScale == window.MapCore.FLT_MAX ? '' : data?.optimizationMinScale;
                fields.isCheckedOptiMin = data?.optimizationMinScale == window.MapCore.FLT_MAX ? false : true;

                fields.maxVisiblePointsPerTile = data?.maxVisiblePointsPerTile == window.MapCore.UINT_MAX ? '' : data?.maxVisiblePointsPerTile;
                fields.isCheckedMaxVisible = data?.maxVisiblePointsPerTile == window.MapCore.UINT_MAX ? false : true;

                fields.minSizeForObjVisibility = data?.minSizeForObjVisibility === 0 ? '' : data?.minSizeForObjVisibility;
                fields.isCheckedMinSize = data?.minSizeForObjVisibility === 0 ? false : true;
            }
            else {
                this.setState({ ...tmpData })
            }
        } else if (type == 'RAW_RASTER') {
            fields.resolveOverlapConflicts = data?.resolveOverlapConflicts;
            fields.maxScale = (data?.maxScale && data?.maxScale != window.MapCore.FLT_MAX) ? data?.maxScale : '';
            fields.enhanceBorderOverlap = data?.enhanceBorderOverlap;
            fields.imageCoordinateSystem = data?.imageCoordinateSystem;
            fields.fillEmptyTilesByLowerResolutionTiles = data?.fillEmptyTilesByLowerResolutionTiles;
            fields.transparentColor = {};
            fields.transparentColor.a = data?.transparentColorA;
            fields.transparentColor.r = data?.transparentColorR;
            fields.transparentColor.g = data?.transparentColorG;
            fields.transparentColor.b = data?.transparentColorB;
            fields.transparentColor.precision = data?.byTransparentColorPrecision;
            fields.ignoreRasterPalette = data?.ignoreRasterPalette;
            fields.highestResolution = data?.highestResolution != 0 ? data?.highestResolution : '';
            fields.isTransparentColor = data?.transparentColorA ? true : false;
        }
        this.setState({ ...fields });
    }
    outOfRuleValidations(field) {
        switch (field) {
            case 'optimizationMinScale':
                return !this.state.isCheckedOptiMin
            case 'maxVisiblePointsPerTile':
                return !this.state.isCheckedMaxVisible
            case 'minSizeForObjVisibility':
                return !this.state.isCheckedMinSize
            default:
                break;
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
                    const isValid = this.outOfRuleValidations(field) ? true : validationFunctions[validation](valueToValidate);
                    if (!isValid) {
                        errors[field] = validationMessages[validation];
                    }
                })
            }
        });
        this.setState({ errors });
        return Object.keys(errors).length > 0 ? false : true;
    }
    getImportFormData = () => {
        let isValid = this.validateForm();
        if (isValid) {
            const fields = {};
            if (this.state.type.includes('VECTOR') || this.state.type == "S-57") {
                this.props.setFilledFields({
                    maxVerticesPerTile: this.state.maxVerticesPerTile,
                    optimizationMinScale: this.state.optimizationMinScale,
                    maxVisiblePointsPerTile: this.state.maxVisiblePointsPerTile,
                    minSizeForObjVisibility: this.state.minSizeForObjVisibility,
                    isCheckedOptiMin: this.state.isCheckedOptiMin,
                    isCheckedMaxVisible: this.state.isCheckedMaxVisible,
                    isCheckedMinSize: this.state.isCheckedMinSize,
                });
                fields.maxVerticesPerTile = this.state.maxVerticesPerTile;
                fields.optimizationMinScale = this.state.isCheckedOptiMin ? this.state.optimizationMinScale : window.MapCore.FLT_MAX;
                fields.maxVisiblePointsPerTile = this.state.isCheckedMaxVisible ? this.state.maxVisiblePointsPerTile : window.MapCore.UINT_MAX;
                fields.minSizeForObjVisibility = this.state.isCheckedMinSize ? this.state.minSizeForObjVisibility : 0;

            } else if (this.state.type == 'RAW_RASTER') {
                fields.resolveOverlapConflicts = this.state.resolveOverlapConflicts;
                fields.maxScale = this.state.maxScale;
                fields.enhanceBorderOverlap = this.state.enhanceBorderOverlap;
                fields.imageCoordinateSystem = this.state.imageCoordinateSystem;
                fields.fillEmptyTilesByLowerResolutionTiles = this.state.fillEmptyTilesByLowerResolutionTiles;
                fields.byTransparentColorPrecision = this.state.transparentColor.precision;
                delete this.state.transparentColor.precision;
                fields.transparentColorA = this.state.transparentColor.a;
                fields.transparentColorR = this.state.transparentColor.r;
                fields.transparentColorG = this.state.transparentColor.g;
                fields.transparentColorB = this.state.transparentColor.b;
                fields.ignoreRasterPalette = this.state.ignoreRasterPalette;
                fields.highestResolution = this.state.highestResolution;
                this.props.setFilledFields({ ...fields })
            }
            this.context.setAdvancedData(fields)
            this.context.setAdvancedDataType(this.state.type)
        }
        return this.state.data && isValid;
    }

    render() {
        return (
            (this.state.type?.includes('VECTOR') || this.state.type == "S-57") ?
                <div style={{ left: '27%', width: '308px' }}>
                    {this.getOptimizationOfHidingObjects()}
                </div>
                :
                <div className={cn.Wrapper}>
                    <div className={cn.Split}>
                        {this.state.type === 'RAW_RASTER' && this.getTrueFalseDropDown('enhanceBorderOverlap', 'Enhance Border Overlap')}
                        {this.state.type === 'RAW_RASTER' && this.getTrueFalseDropDown('resolveOverlapConflicts', 'Resolve Overlap Conflicts')}
                        {this.state.type === 'RAW_RASTER' && this.getTrueFalseDropDown('imageCoordinateSystem', 'Image Coordinate System')}
                        {this.state.type === 'RAW_RASTER' && this.getInputsColor('transparentColor', 'Transparent Color', { type: 'number', readOnly: !this.state.isTransparentColor })}
                    </div>
                    <div className={cn.Split}>
                        {this.state.type === 'RAW_RASTER' && this.getTrueFalseDropDown('ignoreRasterPalette', 'Ignore Raster Palette')}
                        {this.state.type === 'RAW_RASTER' && this.getInput('highestResolution', 'Highest Resolution', { type: 'number' })}
                        {this.state.type === 'RAW_RASTER' && this.getTrueFalseDropDown('fillEmptyTilesByLowerResolutionTiles', 'Fill Empty Tiles by Lower Resolution Tiles')}
                        {this.state.type === 'RAW_RASTER' && this.getInput('maxScale', 'Max Scale (m/pixel)', { type: 'number', isPositive: true })}
                    </div>
                </div>
        );
    }
}
