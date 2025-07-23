import React, { Component } from 'react';
import './styles/global.css';
import validationX from '../../assets/images/close-red.svg';
import Tooltip from '../Tooltip/Tooltip';
import ReactTooltip from "react-tooltip";
class DropdownMultiple extends Component {
  constructor(props) {
    super(props);
    const { title } = this.props;

    this.state = {
      listOpen: false,
      headerTitle: title,
      keyword: '',
      isShowInfoTooltip: false
    };

    this.searchField = React.createRef();
    this.close = this.close.bind(this);
  }
  componentDidMount() {
    this.props.type && this.selectedData();
  }

  componentDidUpdate() {
    const { listOpen } = this.state;
    setTimeout(() => {
      if (listOpen) {
        window.addEventListener('click', this.close);
      } else {
        window.removeEventListener('click', this.close);
      }
    }, 0);
  }

  componentWillUnmount() {
    window.removeEventListener('click', this.close);
  }

  static getDerivedStateFromProps(nextProps) {
    const count = nextProps.list.filter((item) => item.selected).length;

    if (count === 0) {
      return { headerTitle: nextProps.title };
    }
    if (count === 1) {
      const selectedItem = nextProps.list.find((item) => item.selected);
      return { headerTitle: selectedItem.title };
    }
    if (count > 1) {
      if (nextProps.titleHelperPlural) {
        return { headerTitle: `${count} ${nextProps.titleHelperPlural}` };
      }
      return { headerTitle: `${count} ${nextProps.titleHelper}s` };
    }

    return null;
  }

  close() {
    this.setState({
      listOpen: false,
    });
  }

  toggleList() {
    this.setState((prevState) => ({
      listOpen: !prevState.listOpen,
    }), () => {
      if (this.state.listOpen && this.searchField.current) {
        this.searchField.current.focus();
        this.setState({
          keyword: '',
        });
      }
    });
  }

  filterList(e) {
    this.setState({
      keyword: e.target.value.toLowerCase(),
    });
  }

  listItems() {
    const { list, toggleItem, searchable } = this.props;
    const { keyword } = this.state;
    let tempList = list;

    if (keyword.length) {
      tempList = list
        .filter((item) => (
          item.title.toLowerCase().slice(0, keyword.length).includes(keyword)
        )).sort((a, b) => {
          if (a.title < b.title) { return -1; }
          if (a.title > b.title) { return 1; }
          return 0;
        });
    }

    if (tempList.length) {
      return (<>
        {tempList.map((item, index) => (
          <div key={index}>
            <button
              type="button"
              className="dd-list-item"
              key={item.id}
              onClick={() => toggleItem(item.id, item.key)}
            >
              <p
                data-tip={item.title}
                style={{ fontWeight: item.selected ? "bold" : "normal" }}
              >
                {item.title}
              </p>
              {' '}
              {item.selected && <span className="CheckedIcon"></span>}
            </button>
          </div>
        ))}
        <ReactTooltip />
      </>
      );

    }

    return <div className="dd-list-item no-result">{searchable[1]}</div>;
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
    const errorClass = this.props.error ? ` ShowError` : ''
    const mandatoryClass = this.props.isMandatoy ? ` Mandatory` : ''
    const { searchable } = this.props;
    const { listOpen, headerTitle } = this.state;
    return (
      <div className="dd-wrapper">
        <span className={`dd-label${mandatoryClass}`}>{this.props.label}{this.renderInfo()}</span>

        <div className="dd-btn-wrapper">
          <button type="button" className={`dd-header${errorClass}`} onClick={() => this.toggleList()}>
            <div className="dd-header-title">{headerTitle}</div>
            <span className="DropDownArrow"></span>
            <img className={`ValidationImg${errorClass}`} src={validationX} />
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
                  onChange={(e) => this.filterList(e)}
                />
              )}
            <div className="dd-scroll-list">
              {this.listItems()}
            </div>
          </div>
        )}

      </div>
    );
  }
}

export default DropdownMultiple;
