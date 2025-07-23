import React, { PureComponent } from "react";
import cn from './SelectButton.module.css';
import ApplicationContext from '../../context/applicationContext';

export default class Dropdown extends PureComponent {
  static contextType = ApplicationContext;
  constructor() {
    super();

    this.state = {
      displayMenu: false
    };

    this.showDropdownMenu = this.showDropdownMenu.bind(this);
    this.hideDropdownMenu = this.hideDropdownMenu.bind(this);
  }

  showDropdownMenu(event) {
    event.preventDefault();
    this.setState({ displayMenu: true }, () => {
      document.addEventListener("click", this.hideDropdownMenu);
    });
  }

  hideDropdownMenu() {
    this.setState({ displayMenu: false }, () => {
      document.removeEventListener("click", this.hideDropdownMenu);
    });
  }

  onItemClick = (item) => {    
    this.setState({ displayMenu: false }, () => {
      this.props.onChange(item);
    });    
  }


  getElements() {
      return (
          <ul className={cn.ListWrapper}>
              {
                this.props.options.map( item => 
                    <li key={item.code} className={cn.ListElement} onClick={()=> this.onItemClick(item)}>
                        <a >{item.value}</a>
                    </li>
                )
              }
          </ul>
      )
  }

  render() {
    const isActiveClass = this.state.displayMenu ? ` ${cn.DropDownActive}` : '';
    return (
      <div className={`${cn.Dropdown}${isActiveClass}`} onClick={this.showDropdownMenu}>
        <div className={cn.Button}>
        {this.context.dict.importLayer}
        </div>
        {this.state.displayMenu ? this.getElements(): null}
      </div>
    );
  }
}
