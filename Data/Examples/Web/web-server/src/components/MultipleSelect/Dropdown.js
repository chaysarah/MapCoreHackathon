import React, { Component } from 'react';
import './styles/global.css';
import validationX from '../../assets/images/close-red.svg';
import Tooltip from '../Tooltip/Tooltip';
import ReactTooltip from "react-tooltip";
import ApplicationContext from '../../context/applicationContext';
import cn from '../Input/Input.module.css';

class Dropdown extends Component {
  static contextType = ApplicationContext;

  constructor(props) {

    super(props);

    this.state = {
      listOpen: !!this.props.isOpenByDefault,
      keyword: '',
      focusElementIndex: -1,
      headerTitle: this.props.title || this.props.defaultTitle,
      filteredSortedList: this.props.list,
      isShowInfoTooltip: false
    };

    this.searchField = React.createRef();
    this.close = this.close.bind(this);
  }

  ELEMENTS_IN_PAGE = 4;

  ENTER_KEY_CODE = 13;
  DOWN_ARROW_KEY_CODE = 40;
  UP_ARROW_KEY_CODE = 38;
  ESC_ARROW_KEY_CODE = 27;
  HOME_KEY_CODE = 36;
  END_KEY_CODE = 35;
  PAGE_UP_KEY_CODE = 33;
  PAGE_DOWN_KEY_CODE = 34;

  componentDidMount() {
    this.props.type && this.selectedData();
    setTimeout(() => {
      if (this.state.listOpen) {
        window.addEventListener('click', this.close);
      } else {
        window.removeEventListener('click', this.close);
      }
    }, 0);
  }

  componentDidUpdate(oldProps, oldState) {
    if (JSON.stringify(oldProps.list) !== JSON.stringify(this.props.list)) {
      this.setState({
        filteredSortedList: this.props.list,
        headerTitle: this.props.title || this.props.defaultTitle
      })
    } else if (JSON.stringify(oldProps.emptyDTMsProps) !== JSON.stringify(this.props.emptyDTMsProps)) {
      this.selectItem({ title: this.props.title })
      this.props.emptyDTMsPropsFunc();
    }

    setTimeout(() => {
      if (this.state.listOpen) {
        window.addEventListener('click', this.close);
      } else {
        window.removeEventListener('click', this.close);
      }
    }, 0);

    if (!oldState.listOpen && this.state.listOpen) {
      const listItem = document.getElementById(`${this.state.headerTitle}-scrollListWrapper`);
      if (listItem && listItem.scrollIntoViewIfNeeded) {
        listItem.scrollIntoViewIfNeeded()
      };
    }
  }

  componentWillUnmount() {
    window.removeEventListener('click', this.close);
  }

  close() {
    this.setState({
      listOpen: false,
      focusElementIndex: -1,
      filteredSortedList: this.props.list
    });
    if (this.props.closeListCallBack) {
      this.props.closeListCallBack();
    }
  }

  selectItem({ title, id, stateKey }) {
    const { onChange } = this.props;
    this.setState({
      headerTitle: title || this.props.title || this.props.defaultTitle,
      listOpen: false,
    },
      onChange(id, title, stateKey));
  }

  toggleList() {
    this.setState((prevState) => ({
      listOpen: !prevState.listOpen,
      keyword: '',
    }), () => {
      if (this.state.listOpen && this.searchField.current) {
        this.searchField.current.focus();
        this.setState({
          keyword: '',
        });
      }
    });
  }

  filterAndSortList(e) {
    const keyword = e.target.value.toLowerCase();
    let filteredSortedList = this.props.list;

    if (keyword.length) {
      filteredSortedList =
        this.props.list.filter((item) => (
          item.title.toLowerCase().includes(keyword)
        ))/*.sort((a, b) => {
          if (a.title < b.title) { return -1; }
          if (a.title > b.title) { return 1; }
          return 0;
        });*/
    }

    this.setState({
      keyword,
      focusElementIndex: -1,
      filteredSortedList
    });
  }

  listItems() {
    const { searchable } = this.props;

    if (this.state.filteredSortedList.length) {
      return (<>
        {this.state.filteredSortedList.map((item, i) => {

          const hoverClass = i === this.state.focusElementIndex ? ` Hover` : '';
          return (
            <button
              id={i}
              type="button"
              className={`dd-list-item${item.selected ? ' selected' : ''}${hoverClass}`}
              title={item.title}
              key={item.id}
              onClick={() => this.selectItem(item)}
              readOnly={this.props.readOnly}
            >
              <p className='aaaaaaaaaaaaaa' data-tip={item.title}>{item.title}</p>
            </button>
          )
        })}  <ReactTooltip /></>
      );
    }

    return <div className="dd-list-item no-result">{searchable[1]}</div>;
  }

  handleKeyDown = e => {
    if (!this.state.listOpen) return;
    switch (e.keyCode) {
      case this.DOWN_ARROW_KEY_CODE:
        e.preventDefault();
        if (this.state.focusElementIndex < this.state.filteredSortedList.length - 1) {
          this.setState({ focusElementIndex: this.state.focusElementIndex + 1 })
        }
        break;
      case this.UP_ARROW_KEY_CODE:
        e.preventDefault();
        if (this.state.focusElementIndex > 0) {
          this.setState({ focusElementIndex: this.state.focusElementIndex - 1 })
        } else {
          this.setState({ focusElementIndex: this.state.filteredSortedList.length - 1 })
        }
        break;
      case this.HOME_KEY_CODE:
        e.preventDefault();
        this.setState({ focusElementIndex: 0 });
        break;
      case this.END_KEY_CODE:
        e.preventDefault();
        this.setState({ focusElementIndex: this.state.filteredSortedList.length - 1 });
        break;
      case this.PAGE_DOWN_KEY_CODE:
        e.preventDefault();
        if (this.state.focusElementIndex + this.ELEMENTS_IN_PAGE < this.state.filteredSortedList.length) {
          this.setState({ focusElementIndex: this.state.focusElementIndex + this.ELEMENTS_IN_PAGE });
        } else {
          this.setState({ focusElementIndex: this.state.filteredSortedList.length - 1 });
        }
        break;
      case this.PAGE_UP_KEY_CODE:
        e.preventDefault();
        if (this.state.focusElementIndex - this.ELEMENTS_IN_PAGE >= 0) {
          this.setState({ focusElementIndex: this.state.focusElementIndex - this.ELEMENTS_IN_PAGE });
        } else {
          this.setState({ focusElementIndex: 0 });
        }
        break;
      case this.ENTER_KEY_CODE:
        if (this.state.focusElementIndex === -1) return;
        this.selectItem(this.state.filteredSortedList[this.state.focusElementIndex]);
        this.setState({ focusElementIndex: -1, filteredSortedList: this.props.list })
        break;
      case this.ESC_ARROW_KEY_CODE:
        e.stopPropagation();
        e.nativeEvent.stopImmediatePropagation();
        this.setState({ focusElementIndex: -1, filteredSortedList: this.props.list });
        this.close();
        break;
      default:
        break;
    }
  }

  showInfoTooltip = e => {

    this.setState({
      isShowInfoTooltip: true,
      clientX: e.pageX,
      clientY: e.pageY
    });
  }

  hideTooltip = () => this.setState({ isShowInfoTooltip: false, clientX: null, clientY: null })

  renderInfo() {
    if (this.props.info) {
      return (
        <span className={'InfoImage'} onMouseEnter={this.showInfoTooltip} onMouseLeave={this.hideTooltip}>
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

  getTitle(headerTitle) {
    // const headerTitle = selectedEpsg || headerTitle1;
    return (
      <>
        <div className="dd-header-title" title={headerTitle}>{headerTitle}</div>
        <span className="DropDownRemove"
          onClick={(e) => {
            e.preventDefault(); e.stopPropagation();
            this.selectItem({});
          }}>
        </span>
        <span className="DropDownArrow" />
      </>
    )
  }

  selectedData() {
    const contextData = this.context.getData();
    const selected = contextData?.data[this.props.type];
    const selectedEpsg = this.props?.list?.filter(item => item.id == selected)
    const title = selectedEpsg[0]?.title;
    const id = selectedEpsg[0]?.id;
    const key = selectedEpsg[0]?.key;
    title && this.selectItem({ title, id, key });
  }

  render() {
    const { searchable } = this.props;
    const errorClass = this.props.error ? ` ShowError` : '';
    const readOnly = this.props.readOnly ? `readOnly` : '';
    const readOnlyCheckBox = this.props.readOnly ? `${cn.readOnly}` : '';
    const mandatoryClass = this.props.isMandatoy ? ` Mandatory` : ''
    const { listOpen, headerTitle } = this.state;
    const mandatoyClass = this.props.mandatory ? ` ${cn.Mandatory}` : '';


    return (
      <div className="dd-wrapper">
        {this.props.checkboxLabel ?
          <div style={{ display: 'flex', justifyContent: 'flex-start' }} className={`${cn.titleWrapper}${readOnlyCheckBox}`}>
            <label className={`${cn.container}`}>
              <input type='checkbox' onClick={this.props.handleCheckboxChange} checked={this.props.checked ? 'checked' : ''}></input>
              <span className={`${cn.checkmark}`}></span>
            </label>
            <span style={{ color: `${this.props.readOnly ? 'grey' : 'white'}` }} className={`${cn.MainLabel}${mandatoyClass}`}>{this.props.label}{this.renderInfo()}</span>
          </div>
          :
          <span className={`dd-label${mandatoryClass}`}>{this.props.label}{this.renderInfo()}</span>
        }
        <div className="dd-btn-wrapper">
          <button
            type="button"
            className={`dd-header${errorClass} ${readOnly}`}
            onClick={() => this.toggleList()}
          >
            {!this.props.isShowOnlySearchList ? this.getTitle(headerTitle) : null}
          </button>
          <img className={`ValidationImg${errorClass}`} src={validationX} />
          <div className={`ValidationMessage${errorClass}`}>{this.props.error || ''}</div>
        </div>
        {listOpen && (
          <div
            role="list"
            className={`dd-list ${searchable ? 'searchable' : ''}`}
            onClick={(e) => e.stopPropagation()}
          >
            {searchable
              && (
                <input
                  ref={this.searchField}
                  className="dd-list-search-bar"
                  placeholder={searchable[0]}
                  onKeyDown={this.handleKeyDown}
                  onChange={(e) => this.filterAndSortList(e)}
                />
              )}
            <div id={`${this.state.headerTitle}-scrollListWrapper`} className="dd-scroll-list">
              {this.listItems()}
            </div>
          </div>
        )}
      </div>
    );
  }
}

export default Dropdown;
