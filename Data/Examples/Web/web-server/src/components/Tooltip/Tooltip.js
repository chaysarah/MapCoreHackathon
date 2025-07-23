import React from 'react';
import classNames from './Tooltip.module.css';

export default class Tooltip extends React.Component {    
        
    render() {
        
        return (
            <div className={classNames.Wrapper} style={{top: this.props.y, left: this.props.x}}>
                <span className={classNames.Title}>{this.props.title}:</span>
                <span className={classNames.Text}>{this.props.text}</span>                
            </div>
        );            
    }
}
