import React, { PureComponent } from "react";
import cn from './tilingScheme.module.css';
import ApplicationContext from '../../../context/applicationContext';
import Table from '../../Table/table';


export default class TilingSchemeForm extends PureComponent {

    static contextType = ApplicationContext;

    constructor() {
        super();
        this.state = {
            errors: {},
            // tableData: []
        };
    }

    componentDidMount() {
        this.props.setParentHook(this.getImportFormData);
        let tableData = this.context.getTableData();
        this.setState({ tableData })
        this.context.setReadOnlyAfterTilingScheme(false);
    }

    onAddRowButton = () => {
        if (this.onAddRowButton) {
            this.props.onTilingSchemeTable();
        }
    }

    updateDataState = () => {
        let tableData = this.context.getTableData();
        this.setState({ tableData })
    }

    getTable(name, label, options) {
        return (
            <div className={cn.Row}>
                <Table
                    addRowButton={this.onAddRowButton}
                    deleteRowButton={this.updateDataState}
                    error={this.state.errors[name]}
                    label={label}
                    name={name}
                    tableData={this.state.tableData}
                    type={options.type || "text"}
                    onFocus={options.onFocus || null}
                    onChange={this.handleInputChange} />
            </div>
        );
    }

    getImportFormData = () => {
        this.context.setAdvancedData({
            maxVerticesPerTile: this.state.maxVerticesPerTile,
            maxVisiblePointsPerTile: this.state.maxVisiblePointsPerTile,
            minSizeForObjVisibility: this.state.minSizeForObjVisibility,
            optimizationMinScale: this.state.optimizationMinScale
        })
        return this.state.data;
    }

    render() {
        this.updateDataState()
        return (
            <div className={cn.Wrapper}>
                <div className={cn.Split}>
                    {this.state.tableData && this.getTable('TilingSchemesByCRS', this.context.dict.tilingSchemesByCRS, { type: 'number' })}
                </div>
            </div>
        );
    }
}
