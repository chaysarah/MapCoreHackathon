import React, { PureComponent } from "react";
import cn from './ServerConfigurationForm.module.css';
import Select from '../../Select/Select';
import Input from '../../Input/Input';
import { fieldsValidation, validationFunctions, validationMessages } from './ServerConfigurationFormFields';
import ApplicationContext from '../../../context/applicationContext';
import editIcon from '../../../assets/images/edit.svg';

export default class ConfigurationForm extends PureComponent {
    static contextType = ApplicationContext;

    constructor() {
        super();
        this.state = {
            errors: {},
            readOnly: true,
            tilingScheme: [null],
        };
    }

    componentDidUpdate() {

    }

    componentWillUnmount() {
        this.context.setReadOnly(false);
        this.context.setReadOnlyAfterTilingScheme(true);
    }

    componentDidMount() {
        let configData = this.context.getConfigData();
        if (this.context.readOnlyAfterTilingScheme === false && this.context.readOnlyTilingScheme === false) {
            this.setState({ readOnly: false })
            this.context.setReadOnly(false);
        } else {
            this.context.setReadOnly(this.state.readOnly);
            this.context.setReadOnlyTilingScheme(this.state.readOnly);
        }
        this.props.setParentHook(this.getConfigFormData);
        const repositoryData = this.props.repositoryData;
        let tilingScheme = this.context.getTableData();
        // tilingScheme = tilingScheme == [null] ? tilingScheme : repositoryData.TilingSchemePolicy.TilingSchemesByCRS;
        const items = tilingScheme.length == 1 ? "item" : "items";
        const tilinSchemes = tilingScheme.length + " " + items;
        const fields = {
            SuffixUrl: configData?.SuffixUrl || repositoryData.SuffixUrl,
            MemoryCacheSize: configData?.MemoryCacheSize || repositoryData.MemoryCacheSize,
            DiskCacheSize: configData?.DiskCacheSize || repositoryData.DiskCacheSize,
            DiskCacheFolder: configData?.DiskCacheFolder || repositoryData.DiskCacheFolder,
            DefaultTilingScheme: configData?.DefaultTilingScheme || repositoryData.TilingSchemePolicy.DefaultTilingScheme,
            TilingSchemePolicy: tilinSchemes,
            IntermediateFilesFolder: configData?.IntermediateFilesFolder || repositoryData.IntermediateFilesFolder,
            GpuUsageInSpatialQueries: configData?.GpuUsageInSpatialQueries || repositoryData.GpuUsageInSpatialQueries.toString(),
            SSLUsage: configData?.SSLUsage || repositoryData.SSLUsage.toString(),
            MapScaleFactor: configData?.MapScaleFactor || repositoryData.DefaultMaxScaleFactor,
            DefaultTextIndexFilePath: configData?.DefaultTextIndexFilePath || repositoryData.DefaultTextIndexFilePath,
            ImportScansInterval: configData?.ImportScansInterval || repositoryData.ImportScansInterval,
            IsClientVerPre_7_11_4: configData?.IsClientVerPre_7_11_4 || repositoryData.IsClientVerPre_7_11_4.toString(),

            SuffixUrlChanged: configData?.SuffixUrlChanged || false,
            MapScaleFactorChanged: configData?.MapScaleFactorChanged || false,
            MemoryCacheSizeChanged: configData?.MemoryCacheSizeChanged || false,
            DiskCacheSizeChanged: configData?.DiskCacheSizeChanged || false,
            DiskCacheFolderChanged: configData?.DiskCacheFolderChanged || false,
            DefaultTilingSchemeChanged: configData?.DefaultTilingSchemeChanged || false,
            TilingSchemePolicyChanged: configData?.TilingSchemePolicyChanged || false,
            IntermediateFilesFolderChanged: configData?.IntermediateFilesFolderChanged || false,
            GpuUsageInSpatialQueriesChanged: configData?.GpuUsageInSpatialQueriesChanged || false,
            SSLUsageChanged: configData?.SSLUsageChanged || false,
            MapScaleFactorChanged: configData?.MapScaleFactorChanged || false,
            DefaultTextIndexFilePathChanged: configData?.DefaultTextIndexFilePathChanged || false,
            ImportScansIntervalChanged: configData?.ImportScansIntervalChanged || false,
            IsClientVerPre_7_11_4Changed: configData?.IsClientVerPre_7_11_4Changed || false,
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
                    onClickTS={this.onTilingSchemeClicked}
                    onClickSSL={this.onSSLInfoClicked}
                    error={this.state.errors[name]}
                    link={name == "TilingSchemePolicy"}
                    dots={name == "SSLData"}
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

    handleDropDownChange = (e, name) => {
        e = e == "True" ? true : e == "False" ? false : e;
        const obj = {};
        obj[name] = e;
        if (this.state[name] != e) {
            this.setState({ [name + "Changed"]: true });
        }
        this.setState(obj);

    }

    getTrueFalseDropDown(name, label) {
        const filesArr = [{ code: "True", value: "True" }, { code: "False", value: "False" },];
        const defaultCode = this.state[name] ? "True" : "False";
        if (defaultCode == undefined) {
            return ""
        }
        const dropDownData = {
            options: [...filesArr],
            defaultCode,
            label,
            fieldNames: { code: 'code', value: 'value' },
            isMandatoy: true,
            onChange: (e => this.handleDropDownChange(e, name)),
            error: this.state.errors[name],
            readOnly: this.state.readOnly,
        };

        return (
            <div className={cn.Row}>
                <Select {...dropDownData} />
            </div>
        )
    }

    handleTextureChanged = (selectedCode) => {
        if (selectedCode === 'selectFile') {
            this.setState({ textureFile: "" });
        } else {
            const file = [...this.props.fileList].find(file => file.name === selectedCode);
            this.setState({ textureFile: file.name, "textureFileChanged": true });
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
            onChange: (e => this.handleDropDownChange(e, name)),
            error: this.state.errors[name],
            readOnly: this.state.readOnly,
        };

        return (
            <div className={cn.Row}>
                <Select {...dropDownData} />
            </div>
        )
    }

    getPointTextureFile() {
        const filesArr = [...this.props.fileList].map(file => ({ code: file.name, value: file.name }));
        const dropDownData = {
            options: [{ code: 'selectFile', value: 'Select point image...' }, ...filesArr],
            onChange: this.handleTextureChanged,
            label: 'Default Point Image',
            fieldNames: { code: 'code', value: 'value' },
            isMandatoy: false,
            error: this.state.errors['textureFile']
        };

        return (
            <div className={cn.Row}>
                <Select {...dropDownData} />
            </div>
        )
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

    getFormatTilingScheme = () => {
        let TilingSchemes;
        const tilingSchemePolicy = this.context.getTilingScheme();
        let TilingSchemesByCRS = tilingSchemePolicy.map((item) => {
            return TilingSchemes = {
                "CoordinateSystem": {
                    "SRIDType": "epsg",
                    "Code": item.epsg
                },
                "TilingScheme": item.tilingScheme
            }
        })
        return TilingSchemesByCRS;
    }

    getConfigFormData = () => {
        let data = {};
        const isValid = this.validateForm();
        const tilingSchemePolicy = this.getFormatTilingScheme();
        this.setState({ tilingScheme: tilingSchemePolicy.tilingSchemePolicy })
        const SSLInfo = this.context.getSSLData();
        if (isValid) {
            // this.state.ImportScansIntervalChanged && this.buildObj("ImportScansInterval");
            this.state.ImportScansIntervalChanged && (data.ImportScansInterval = this.state["ImportScansInterval"]);
            (this.context.tilingSchemeChanged || this.state.DefaultTilingSchemeChanged) && (data.TilingSchemePolicy = { "DefaultTilingScheme": this.state["DefaultTilingScheme"], "TilingSchemesByCRS": tilingSchemePolicy });
            this.state.SuffixUrlChanged && (data.SuffixUrl = this.state["SuffixUrl"]);
            this.state.MemoryCacheSizeChanged && (data.MemoryCacheSize = this.state["MemoryCacheSize"]);
            this.state.DiskCacheSizeChanged && (data.DiskCacheSize = this.state["DiskCacheSize"]);
            this.state.DiskCacheFolderChanged && (data.DiskCacheFolder = this.state["DiskCacheFolder"]);
            this.state.DefaultTextIndexFilePathChanged && (data.DefaultTextIndexFilePath = this.state["DefaultTextIndexFilePath"]);
            this.state.GpuUsageInSpatialQueriesChanged && (data.GpuUsageInSpatialQueries = this.state["GpuUsageInSpatialQueries"]);
            this.state.SSLUsageChanged && (data.SSLUsage = this.state["SSLUsage"]);
            this.context.SSLChanged && (data.SSLInfo = SSLInfo);
            this.state.MapScaleFactorChanged && (data.DefaultMaxScaleFactor = this.state["MapScaleFactor"]);
            this.state.IsClientVerPre_7_11_4Changed && (data.IsClientVerPre_7_11_4 = this.state["IsClientVerPre_7_11_4"]);
        }
        return data;
    }

    enableEdit = () => {
        // this.props.onEditConfig()
        if (window.confirm("Changing the server configuration might cause it to be activated misproperly. Continue?") == true) {
            this.setState({ readOnly: false });
            this.context.setReadOnly(false);
            this.context.setReadOnlyTilingScheme(false);
        }
    }

    onSSLInfoClicked = () => {
        if (this.props.onSSL) {
            this.props.onSSL();
        }
        this.context.setConfigData(this.state)
    }

    onTilingSchemeClicked = () => {
        if (this.props.onTilingScheme) {
            this.props.onTilingScheme();
        }
        this.context.setConfigData(this.state)
    }

    render() {
        return (
            <div className={cn.Wrapper}>
                <div className={cn.Split}>
                    {this.getInput('SuffixUrl', this.context.dict.suffixUrl, { maxLength: '20' })}
                    {this.getInput('MemoryCacheSize', this.context.dict.memoryCacheSize, { type: 'number', maxLength: '20' })}
                    {this.getInput('DiskCacheSize', this.context.dict.diskCacheSize, { type: 'number', maxLength: '20' })}
                    {this.getInput("DefaultTextIndexFilePath", this.context.dict.DefaultTextIndexFilePath, {})}
                    {this.getInput("SecurityRecordFileName", this.context.dict.SecurityRecordFileName, {})}
                    {this.getInput('DiskCacheFolder', this.context.dict.diskCacheFolder, { maxLength: '20' })}
                    {this.getDefaultTilingSchemeDropDown('DefaultTilingScheme', "Default Tiling Scheme", {})}
                    {this.getInput('TilingSchemePolicy', "Default Tiling Scheme Exceptions by CRS:", { maxLength: '20', readOnly: true })}
                </div>
                <div className={cn.Split}>
                    {this.getInput('IntermediateFilesFolder', this.context.dict.intermediateFilesFolder, { maxLength: '20' })}
                    {this.getTrueFalseDropDown("GpuUsageInSpatialQueries", this.context.dict.gpuUsageInSpatialQueries)}
                    {this.getInput("MapScaleFactor", this.context.dict.mapScaleFactor, { type: 'number', maxLength: '20' })}
                    {this.getInput("ImportScansInterval", this.context.dict.ImportScansInterval, { type: 'number', maxLength: '20' })}
                    {this.getTrueFalseDropDown("IsClientVerPre_7_11_4", this.context.dict.IsClientVerPre_7_11_4)}
                    {/* {this.getTrueFalseDropDown("SSLUsage", this.context.dict.sSLUsage)} */}
                    {(!this.state.readOnly && this.state.SSLUsage == "true") ? this.getInput("SSLData", "SSL Data", { readOnly: true }) : null}

                    <div className={cn.EnableEdit}>
                        <img className={cn.Icon} src={editIcon} />
                        <a className={cn.EnableEditText} onClick={() => this.enableEdit()}>{this.context.dict.enableEdit}</a>
                    </div>
                </div>
            </div>
        );
    }
}
