import React, { PureComponent } from "react";
import cn from './AddRenameGroupForm.module.css';
import Input from '../../Input/Input';

import ApplicationContext from '../../../context/applicationContext';

export default class AddRenameGroupForm extends PureComponent {
  static contextType = ApplicationContext;

  constructor(props) {
    super(props);
    this.inputRef = React.createRef()
    this.state = {
      inputName: this.props.oldGroupName ? this.props.oldGroupName : ""
    };
  }

  componentDidMount() {
    if (this.inputRef && this.inputRef.current) {
      this.inputRef.current.focus();
    }
  }

  handleInputChange = (e) => {
    const reg = /^[^,]*$/;
    if (reg.test(e.target.value)) {
      let prevVal = this.state.inputName;
      this.setState({ ...this.state.inputName, inputName: e.target.value }, () => {
        if (prevVal != this.state.inputName) {
          let indexChange = this.inputRef.current.value.length;
          let shortLength = prevVal.length > this.state.inputName.length ? this.state.inputName.length : prevVal.length;
          let flag = false;
          for (let i = 0; i < shortLength; i++) {
            if (prevVal[i] != this.state.inputName[i]) {
              if (prevVal.substring(i + 1) == this.state.inputName.substring(i)) {
                indexChange = i;
                flag = true;
                break;
              }
              else {
                indexChange = i + 1;
                flag = true;
                break;
              }
            }
          }
          if (!flag) {
            indexChange = this.inputRef.current.value.length;
          }
          this.inputRef.current.selectionStart = this.inputRef.current.selectionEnd = indexChange;
        }
      })
      this.props.updateNewName(e.target.value)
    }
  }

  getInput(name, label, options) {
    return (
      <div className={cn.Row}>
        <Input
          parentRef={this.inputRef}
          mandatory={options.mandatory}
          label={label}
          name={name}
          value={this.state.inputName}
          maxLength={options.maxLength || null}
          type={options.type || "text"}
          onChange={this.handleInputChange} />
      </div>
    );
  }


  render() {
    return (
      <div className={cn.Wrapper}>
        {this.getInput('groupName', this.context.dict.newGroupName, { maxLength: '100', mandatory: true })}
      </div>
    );
  }
}