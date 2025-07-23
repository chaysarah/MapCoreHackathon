import React, { PureComponent } from 'react';
import classNames from './Select.module.css';
import Tooltip from '../Tooltip/Tooltip';
import validationX from '../../assets/images/close-red.svg'
import '../MultipleSelect/styles/global.css';
import ApplicationContext from '../../context/applicationContext';

export default class Select extends PureComponent {
  static contextType = ApplicationContext;
  isFocused = false;
  KEY_UP = 38;
  KEY_DOWN = 40;
  ENTER = 13;
  ESC = 27;

  listRef = React.createRef();
  state = {
    selectedCode: null,
    focusElementIndex: -1,
    isShowInfoTooltip: false
  };

  componentDidMount() {
    let initialCode = '';
    const contextData = this.context.getData();
    const type = this.props.type;
    if (contextData?.data[type]) {
      initialCode = contextData?.data[type];
    } else if (this.props.defaultCode != undefined) {
      initialCode = this.props.defaultCode.toString();
    } else {
      initialCode = this.props.options[0][this.props.fieldNames.code];
    }
    this.setState({
      selectedCode: initialCode
    }, () => this.props.onChange(initialCode));
  }

  componentDidUpdate(prevProps, prevState) {
    let initialCode = '';
    if (this.state.selectedCode !== prevState.selectedCode) {
      if ((this.props.options !== prevProps.options) &&
        (JSON.stringify(this.props.options) !== JSON.stringify(prevProps.options))) {
        if (this.state.selectedCode || this.state.selectedCode === '') {
          initialCode = this.state.selectedCode;
        } else if (this.props.defaultCode != undefined) {
          initialCode = this.props.defaultCode.toString();
        } else {
          initialCode = this.props.options[0][this.props.fieldNames.code];
        }
        if (this.state.selectedCode !== prevState.selectedCode)
          this.setState({
            selectedCode: initialCode
          }, () => this.props.onChange(initialCode));
      }
    }
  }

  onDataChange = (item) => {
    let selected = item[this.props.fieldNames.code];
    this.setState({
      selectedCode: selected,
      displayMenu: false
    });
    this.props.onChange(selected);
  }

  showInfoTooltip = e => {
    this.setState({
      isShowInfoTooltip: true,
      clientX: e.pageX,
      clientY: e.pageY
      // clientX: this.infoImageRef.current.getBoundingClientRect().x, 
      // clientY: this.infoImageRef.current.getBoundingClientRect().y
    });
  }

  hideTooltip = () => this.setState({ isShowInfoTooltip: false, clientX: null, clientY: null })

  renderInfo() {
    if (this.props.info) {
      return (
        <span className={classNames.InfoImage} onMouseEnter={this.showInfoTooltip} onMouseLeave={this.hideTooltip}>
          {
            this.state.isShowInfoTooltip ?
              (<Tooltip
                title={this.props.label}
                text={this.props.info}
                x={this.state.clientX}
                y={this.state.clientY}
              />) : null
          }
        </span>
      )
    }
  }
  getLabel() {
    if (this.props.label) {
      const mandatoryClass = this.props.isMandatoy ? ` ${classNames.Mandatory}` : ''
      return <span className={`${classNames.Label}${mandatoryClass}`} >{this.props.label}{this.renderInfo()}</span>
    }
    return null;
  }

  getElements() {
    // let maxWidthText;
    return (
      <ul className={classNames.ListWrapper} ref={this.listRef}>
        {
          this.props.options.map((item, i) => {
            // let widthText = useMeasureText(item.value)

            const hoverClass = i === this.state.focusElementIndex ? ` ${classNames.Hover}` : '';


            return (

              <li key={item.code} className={`${classNames.ListElement}${hoverClass}`} onClick={() => this.onDataChange(item)}>
                <a title={item.value}>{item.value}
                </a>
              </li>

            )
          })
        }

      </ul>
    )
  }

  handleKeyDown = e => {
    if (!this.state.displayMenu) return;
    switch (e.keyCode) {
      case this.KEY_DOWN:
        e.preventDefault();
        if (this.state.focusElementIndex < this.props.options.length - 1) {
          this.setState({ focusElementIndex: this.state.focusElementIndex + 1 })
        }
        break;
      case this.KEY_UP:
        e.preventDefault();
        if (this.state.focusElementIndex > 0) {
          this.setState({ focusElementIndex: this.state.focusElementIndex - 1 })
        }
        break;
      case this.ENTER:
        this.onDataChange(this.props.options[this.state.focusElementIndex])
        this.setState({ focusElementIndex: - 1 })
        break;
      case this.ESC:
        e.stopPropagation();
        e.nativeEvent.stopImmediatePropagation();
        this.setState({ displayMenu: false, focusElementIndex: -1 });
        break;
      default:
        break;
    }
  }

  onClick = () => {
    if (this.isFocused && this.state.displayMenu) {
      this.isFocused = false;
    } else {
      this.setState({ displayMenu: !this.state.displayMenu, focusElementIndex: -1 }, () => {
        if (this.state.displayMenu && this.listRef.current.scrollIntoViewIfNeeded) this.listRef.current.scrollIntoViewIfNeeded();
      });
    }
  }

  onBlur = () => {
    this.isFocused = false;
    setTimeout(() => {
      if (this.state.displayMenu) {
        this.setState({ displayMenu: false, focusElementIndex: -1 });
      }
    }, 300)
  }

  onFocus = () => {
    this.isFocused = true;
    if (!this.state.displayMenu) {
      this.setState({ displayMenu: true }, () => {
        if (this.listRef.current.scrollIntoViewIfNeeded) {
          this.listRef.current.scrollIntoViewIfNeeded();
        }
      });
    }
  }
  handleInputFocus = () => {
    if (this.state.selectedCode === 'selectFile') {
      this.setState({
        selectedCode: '',
        displayMenu: false
      });
      this.props.onChange('');
    }
  }
  handleInputBlur = () => {
    if (!this.state.selectedCode) {
      let selectF = this.props.options[0][this.props.fieldNames.code] || 'Select file...';
      this.setState({
        selectedCode: selectF,
        displayMenu: false
      });
      this.props.onChange(selectF);
    }
  }
  render() {

    const errorClass = this.props.error ? ` ${classNames.ShowError}` : ''
    const isActiveClass = this.state.displayMenu ? ` ${classNames.DropDownActive}` : '';
    const readOnly = this.props.readOnly ? ` ${classNames.readOnly}` : '';
    const isSmall = this.props.isSmall ? ` ${classNames.Small}` : '';

    let selectedItem = this.props.options.find(item => item[this.props.fieldNames.code] === this.state.selectedCode);
    if (!selectedItem && this.props.editMode) {
      selectedItem = this.state.selectedCode;
    }
    let selectedValue = '';
    if (selectedItem) {
      selectedValue = selectedItem[this.props.fieldNames.value] ? selectedItem[this.props.fieldNames.value] : selectedItem;
    }
    // let editModeDropDownBefore = this.props.editMode ? '' : 'DropDownBefore';
    return (
      <div className={classNames.Wrapper}>
        {this.getLabel()}
        <div className={`${classNames.DropDownWrapper}${isSmall}`}>
          <div className={`${classNames.Dropdown}${isActiveClass}${isSmall}${errorClass}${readOnly}`}
            style={this.props.styles}
            tabIndex='0'
            onBlur={this.props.editMode ? undefined : this.onBlur}
            onFocus={this.props.readOnly || this.props.editMode ? undefined : this.onFocus}
            onClick={this.props.readOnly ? undefined : this.onClick}
            onKeyDown={this.handleKeyDown} >
            {this.props.editMode ?
              <input style={{ backgroundColor: 'inherit', border: 'none', outline: 'none', width: '88%', height: '100%', color: 'white', fontSize: '1.03em', fontWeight: '1.5em' }}
                type='text'
                value={selectedValue}
                onBlur={this.handleInputBlur}
                onFocus={this.handleInputFocus}
                onChange={(e) => { this.onDataChange({ code: e.target.value }); this.props.onChange(e.target.value); }}
                onClick={(e) => { e.stopPropagation() }}
              /> :
              <div className={classNames.SelectedValue}>
                {selectedValue}
              </div>
            }
            <img className={`${classNames.ValidationImg}${errorClass}`} src={validationX} />
            {this.state.displayMenu ? this.getElements() : null}
          </div>
          <div className={`${classNames.ValidationMessage}${errorClass}`}>{this.props.error || ''}</div>
        </div>
      </div>
    );
  }
}
