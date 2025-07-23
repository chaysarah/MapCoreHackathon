import React, { PureComponent } from 'react'
import cn from './RunningProcesses.module.css';
import Progress from '../Progress/Progress';
import RunningProcessesAdmin from '../RunningProcessesAdmin/RunningProcessesAdmin';
import ApplicationContext from '../../context/applicationContext';
export default class RunningProcesses extends PureComponent {
    static contextType = ApplicationContext;
    DELIMITER = '%';
    processErrorMessage = '';

    getPecent(layerFiles) {
        let finished = 0;
        for (let i = 0; i < layerFiles.length; i++) {
            const p = layerFiles[i].percent;
            if (p === 'error') {
                this.processErrorMessage = layerFiles[i].errorMessage;
                return `error`;
            } else {
                finished += p;
            }
        }
        return Math.floor((finished / (layerFiles.length * 100)) * 100);
    }

    getProcPercent(procData) {
        if (procData[0].hasOwnProperty('percent')) {
            return procData[0].percent;
        }
        else {
            return -1;
        }
    }

    getProcesses() {
        let completed = [], failed = [], inProgress = [], asyncProcess = [];

        const processes = [];
        const processesObj = {};  // key: layerTitle, value: filesArray
        const asyncProcObj = {};  // key: layerTitle, value: last converted message data
        for (let i = 0; i < this.props.uploadFiles.length; i++) {
            const layerTitle = this.props.uploadFiles[i].layersParams.layerId ?
                this.props.uploadFiles[i].layersParams.layerId + this.DELIMITER + this.props.uploadFiles[i].keyForUploadedLayer :
                this.props.uploadFiles[i].layersParams.McPackage.split(".mcpkg")[0] + this.DELIMITER + this.props.uploadFiles[i].keyForUploadedLayer;
            if (layerTitle in processesObj) {
                processesObj[layerTitle].push(this.props.uploadFiles[i])
            } else {
                processesObj[layerTitle] = [this.props.uploadFiles[i]]
            }
        }

        let servProcess = {};
        let servPerctent = -1;

        for (let i = 0; i < this.props.convertedFiles.length; i++) {
            if (this.props.convertedFiles[i].message.hasOwnProperty('vector_processing')) {
                asyncProcObj[this.props.convertedFiles[i].message.vector_processing.title] = [this.props.convertedFiles[i]];
                servPerctent = this.getProcPercent(asyncProcObj[this.props.convertedFiles[i].message.vector_processing.title]);
            }
        }

        for (const layerTitle in processesObj) {
            let title = layerTitle.substring(0, layerTitle.indexOf(this.DELIMITER));
            this.processErrorMessage = '';
            const process = {
                percent: this.getPecent(processesObj[layerTitle]),
                layer: title,
                errorMessage: this.processErrorMessage
            };

            let servProcess = {}
            if (servPerctent >= 0) {
                if (!!title && !!asyncProcObj[title] && asyncProcObj[title].length > 0 &&
                    !!asyncProcObj[title][0].message && !!asyncProcObj[title][0].message.vector_processing.state && processesObj[layerTitle][processesObj[layerTitle].length - 1].percent < 100) {
                    servProcess = {
                        percent: asyncProcObj[title][0].percent,
                        layer: title,
                        errorMessage: !!title ? asyncProcObj[title][0].message.vector_processing.state : ''
                    }
                }
                else {
                    if (!!asyncProcObj[title]) {
                        if (asyncProcObj[title].length > 0) {
                            console.log("Message exists: ", !!asyncProcObj[title][0].message);
                        }
                    }
                    else {
                        console.log("asyncProcObj is undefined");
                    }
                }
            }
            else {
                servProcess = {
                    percent: -1,
                    layer: title,
                    errorMessage: ''
                }
            }

            if (servProcess.percent >= 0 && servProcess.percent < 100) {
                asyncProcess.push(layerTitle)
                processes.unshift(<Progress process={servProcess} Async={true} key={layerTitle} />)
            }
            else {
                if (process.percent === 100) {
                    completed.push(layerTitle);
                } else if (process.percent === 'error') {
                    failed.push(layerTitle);
                } else {
                    inProgress.push(layerTitle);
                }
                processes.unshift(<Progress process={process} Async={false} key={layerTitle} />)
            }
        }

        const statuses = { completed, failed, inProgress, asyncProcess };
        return { statuses, processes };
    }

    render() {
        const { statuses, processes } = this.getProcesses();
        return (
            <div className={cn.Wrapper}>
                <h2 className={cn.Header}>{this.context.dict.runningProcesses}</h2>
                <RunningProcessesAdmin
                    statuses={statuses}
                    onClearClick={this.props.onClearClick} />
                <div className={cn.ProcessList}>
                    {processes}
                </div>
            </div>
        )
    }
}
