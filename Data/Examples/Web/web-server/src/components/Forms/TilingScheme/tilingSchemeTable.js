import React, { PureComponent } from "react";
import cn from './tilingScheme.module.css';
import ApplicationContext from '../../../context/applicationContext';
import Input from '../../Input/Input';
import { fieldsInfo, fieldsValidation, validationFunctions, validationMessages } from './tilingSchemeFields';
import { Dropdown } from '../../MultipleSelect';
import Select from '../../Select/Select'

export default class TilingSchemeTableForm extends PureComponent {

    static contextType = ApplicationContext;

    constructor() {
        super();
        this.state = {
            errors: {},
            epsg: null
        };
    }

    componentDidMount() {
        this.setState({ defaultTilingScheme: 'MAPCORE' });
        this.props.setParentHook(this.getImportFormData);
        this.edit()
    }

    edit = () => {
        const key = this.context.rowNum;
        this.setState({ key });
        const tableData = this.context.getTableData();
        if (key != null) {
            this.setState({ epsg: tableData[key].epsg })
            this.setState({ epsgTitle: tableData[key].epsgTitle })
            this.setState({ defaultTilingScheme: tableData[key].tilingScheme });
        }
    }

    getDefaultTilingSchemeDropDown(name, label) {
        const filesArr = [{ code: "MAPCORE", value: "MAPCORE" }, { code: "GLOBAL_LOGICAL", value: "GLOBAL_LOGICAL" }, { code: "GOOGLE_CRS84_QUAD", value: "GOOGLE_CRS84_QUAD" }, { code: "GOOGLE_MAPS_COMPATIBLE", value: "GOOGLE_MAPS_COMPATIBLE" }];
        const defaultCode = this.state[name];
        if (defaultCode == undefined) {
            return ""
        }

        const dropDownData = {
            options: [...filesArr],
            defaultCode,
            label,
            fieldNames: { code: 'code', value: 'value' },
            onChange: (e => this.handleDropDownChange(e)),
            error: this.state.errors[name],
            readOnly: this.state.readOnly,
        };

        return (
            <div className={cn.Row}>
                <Select {...dropDownData} />
            </div>
        )
    }

    handleDropDownChange = (e) => {
        this.setState({ defaultTilingScheme: e });
    }

    onTilingSchemeTableClicked = (e) => {
        e.preventDefault();
        if (this.props.onTilingSchemeTable) {
            this.props.onTilingSchemeTable();
        }
    }

    getEPSGDropDown(epsgType) {
        const label = this.context.dict.coordinateSystem;
        if (this.context.epsgCodes.length < 1) return null;

        const options = this.context.epsgCodes.map(item => {
            return {
                id: item.code,
                title: `${item.code}: ${item.desc}`,
                selected: item.code == this.state.epsg,
                key: epsgType
            }
        });

        return (
            <div className={cn.Row}>
                <div className={cn.DropDownWrapper}>
                    <Dropdown
                        searchable={["Search coordinate system...", "No matching values"]}
                        title={this.state.epsgTitle || "Select coordinate system"}
                        list={options}
                        onChange={this.handleEpsgChange}
                        label={label}
                        error={this.state.errors[epsgType]}
                        info={fieldsInfo[epsgType]}
                        isMandatoy={true}
                        type={epsgType}
                    />
                </div>
            </div>
        )
    }

    handleEpsgChange = (code, title) => {
        this.setState({ epsg: code, epsgTitle: title });
    }

    getImportFormData = () => {
        if (this.state.key != null) {
            this.context.setTableDataEdit({ epsg: this.state.epsg, epsgTitle: this.state.epsgTitle, tilingScheme: this.state.defaultTilingScheme });
        } else {
            let isValid = this.validateForm();
            if (isValid) {
                this.props.isValid(true);
                this.context.setIsRequireTableData(true);
                this.context.setTableData({ epsg: this.state.epsg, epsgTitle: this.state.epsgTitle, tilingScheme: this.state.defaultTilingScheme });
            } else {
                this.props.isValid(false);
                this.context.setIsRequireTableData(false);
            }
        }
    }

    validateForm() {
        this.setState({ firstTime: false })
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
                    {this.getDefaultTilingSchemeDropDown('defaultTilingScheme', this.context.dict.defaultTilingScheme)}
                    {this.getEPSGDropDown('epsg')}
                </div>
            </div>
        );
    }
}
