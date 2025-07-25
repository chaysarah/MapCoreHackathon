import React, { PureComponent}  from 'react'
import classNames from './Clock.module.css';
import ApplicationContext from '../../context/applicationContext';

export default class Clock extends PureComponent {
    static contextType = ApplicationContext;
    

    state = {
        date: new Date()
    }

    componentDidMount() {
        setInterval(
          () =>
            this.setState({
              date: new Date()
            }),
          1000
        );    
    }
    
    getFormatedData() {
        const {date} = this.state;
        const year = date.getFullYear();
        const month = date.toLocaleString('en', { month: 'short' });        
        let day = date.getDate();
        day = day < 10 ? `0${day}` : day;
        return `${month}. ${day} ${year},`;
    }

    render() {
        return (
            <span className={classNames.DateTime}>
                {this.getFormatedData()}{`  `}
                <span className={classNames.Hour}>{this.state.date.toLocaleTimeString(navigator.language,{hour12: false})}</span>
            </span>
        )
    }
}
