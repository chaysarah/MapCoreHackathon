import React, { PureComponent } from "react";
import ApplicationContext from '../../context/applicationContext';
import cn from './table.module.css';
import editIcon from '../../assets/images/edit.svg';
import deleteIcon from '../../assets/images/delete.svg';
import closeIcon from '../../assets/images/close.svg';
import addIcon from '../../assets/images/add.svg';
import Tooltip from '../Tooltip/Tooltip';
import ReactTooltip from "react-tooltip";

export default class Table extends PureComponent {
    static contextType = ApplicationContext;
    state = {
        isShowInfoTooltip: false
    }

    showInfoTooltip = e => {
        this.setState({ isShowInfoTooltip: true, clientX: e.target.getBoundingClientRect().left, clientY: e.target.getBoundingClientRect().top });
    }

    hideTooltip = () => this.setState({ isShowInfoTooltip: false, clientX: null, clientY: null })

    renderInfo() {
        if (this.props.info) {
            return (
                <span className={'InfoImage'} onMouseEnter={this.showInfoTooltip} onMouseLeave={this.hideTooltip}>
                    {
                        this.state.isShowInfoTooltip ?
                            (<Tooltip
                                title={this.props.info.title}
                                text={this.props.info}
                                x={this.state.clientX}
                                y={this.state.clientY}
                            />) : null
                    }
                </span>
            )
        }
    }

    onAddRowClicked = (e) => {
        this.context.setRowNum(null)
        e.preventDefault();
        if (this.props.addRowButton) {
            this.props.addRowButton();
        }
    }

    onEditRowClicked = (key) => {
        this.context.setRowNum(key)
        if (this.props.addRowButton) {
            this.props.addRowButton();
        }
    }

    onDeleteRowClicked = (key) => {
        this.context.setDeleteRow(key, () => {
            this.props.deleteRowButton()
        })

    }

    onDeleteAllClicked = () => {
        this.context.deleteTableData(() => {
            this.props.deleteRowButton()
        })
    }

    render() {
        const { tableData } = this.props;

        return (
            <div className={cn.App}>
                <div>
                    <table>
                        <thead>
                            <tr style={{ height: "25px" }}>
                                <th style={{ border: "1px solid white" }}>Coordinate System</th>
                                <th>Tiling Scheme </th>
                                <th> </th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                tableData?.map((value, key) => {
                                    return (
                                        <tr key={key}>
                                            <td>{value.epsgTitle}</td>
                                            <td>{value.tilingScheme}</td>
                                            <td>
                                                {<a className={this.context.readOnlyTilingScheme ? cn.ReadOnly : ''} onClick={() => this.onEditRowClicked(key)}><img className={cn.Icon} src={editIcon} data-tip={"Edit current item"} /><ReactTooltip /></a>}
                                                {<a className={this.context.readOnlyTilingScheme ? cn.ReadOnly : ''} onClick={() => this.onDeleteRowClicked(key)}><img className={cn.Icon} src={closeIcon} data-tip={"Delete current item"} style={{ width: "12px" }} /><ReactTooltip /></a>}
                                            </td>
                                        </tr>
                                    )
                                })
                            }
                        </tbody>
                    </table>
                </div>
                <div className={cn.buttonDivs}>
                    <a className={this.context.readOnlyTilingScheme ? cn.ReadOnly : ''} onClick={this.onAddRowClicked}><img className={cn.Icon} src={addIcon} data-tip={"Add a new item"} /><ReactTooltip /></a>
                    <a className={this.context.readOnlyTilingScheme ? cn.ReadOnly : ''} onClick={this.onDeleteAllClicked}><img className={cn.Icon} src={deleteIcon} data-tip={"Delete all items"} /><ReactTooltip /></a>
                </div>
            </div>
        );
    }
}