import React, { Component } from 'react'
import cn from './RunningProcessesAdmin.module.css';
import ApplicationContext from '../../context/applicationContext';

export default class RunningProcessesAdmin extends Component {
    static contextType = ApplicationContext;
    render() {
        const {completed, failed, inProgress, asyncProcess} = this.props.statuses;

        const completedHiddenClass = completed.length > 0 ? '' : ` ${cn.Hidden}`;
        const failedHiddenClass = failed.length > 0 ? '' : ` ${cn.Hidden}`;
        const asyncHiddenClass = (this.context.dict.asyncProcess <= 10 || this.context.dict.asyncProcess >= 100) ? '' : ` ${cn.Hidden}`;

        return (
            <div className={cn.Wrapper}>
                <div className={`${cn.Process} ${cn.Completed}`}>
                    <span className={cn.Number}>{completed.length}/{completed.length + failed.length + inProgress.length + asyncProcess.length}</span>
                    <div className={cn.StatusWrapper}>
                        <span className={cn.Status}>{this.context.dict.completed}</span>
                        <button className={`${cn.ClearBtn}${completedHiddenClass}`} onClick={() => this.props.onClearClick('completed',completed)}>Clear</button>
                    </div>
                </div>
                <div className={`${cn.Process} ${cn.Failed}`}>
                    <span className={cn.Number}>{failed.length}</span>
                    <div className={cn.StatusWrapper}>
                        <span className={cn.Status}>{this.context.dict.failed}</span>
                        <button className={`${cn.ClearBtn}${failedHiddenClass}`} onClick={() => this.props.onClearClick('failed', failed)}>Clear</button>
                    </div>
                </div>
                <div className={`${cn.Process} ${cn.InProcess}`}>
                    <span className={cn.Number}>{inProgress.length}</span>
                    <div className={cn.StatusWrapper}>
                        <span className={cn.Status}>{this.context.dict.inProgress}</span>                        
                    </div>
                </div>
                <div className={`${cn.Process} ${cn.AsyncProcess}${asyncHiddenClass}`}>
                    <span className={cn.Number}>{asyncProcess.length}</span>
                    <div className={cn.StatusWrapper}>
                        <span className={cn.Status}>{this.context.dict.asyncProcess}</span>
                    </div>
                </div>
            </div>
        )
    }
}
