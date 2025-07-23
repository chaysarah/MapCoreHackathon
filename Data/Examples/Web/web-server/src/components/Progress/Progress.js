import React, { PureComponent } from 'react'
import cn from './Progress.module.css';

export default class Progress extends PureComponent {

    getTransformClass(percent) {
        if (percent === 0) {
            return ` ${cn.Zero}`
        } else if (percent > 0 && percent < 100) {
           return ` ${cn.Half}`;
        } else if (percent === 'error') {
            return ` ${cn.Error}`;
        } else {
            return '';
        } 
    }

    render() {
        const {percent, layer, errorMessage} = this.props.process;
        const percentValue = percent !== 'error' ? `${percent}%` : errorMessage;
        const errorClass = percent === 'error' ? ` ${cn.Error}` : '';
        const noTransformClass = this.getTransformClass(percent);
        const success = percent === 100 ? ` ${cn.Success}` : '';      
        const asyncBarColor = percent !== 100 && percent !== 'error' ? `${cn.AsyncBarColor} ` : '';
        const isAsync = this.props.Async;  

        return (
            <div className={cn.Wrapper}>
                <div className={cn.Title} title={layer}>{layer}</div>
                <div className={cn.ProgressWrapper}>
                    <span className={`${cn.PercentValue}${errorClass}`} style={{paddingLeft: percent !== 'error' ? `${percent}%` : null}}>
                        <span className={`${cn.PercentInitValue}${noTransformClass}`} title={percentValue}>{success ? 'Completed' : percentValue}</span>
                        <span className={`${cn.ErrorIcon}${errorClass}`}></span>
                    </span>
                    {isAsync
                        ? <div className={`${cn.AsyncProgress}${errorClass}`}> 
                            <div className={`${cn.AsyncBar}${errorClass}${success}`} style={{width: percent === 'error' ? '100%' : `${percent}%`}}/>
                        </div>
                        :  <div className={`${cn.Progress}${errorClass}`}> 
                            <div className={`${cn.Bar}${errorClass}${success}`} style={{width: percent === 'error' ? '100%' : `${percent}%`}}/>
                        </div>
                    }
                </div>
            </div>
        )
    }
}
