import React, { useRef } from 'react';
import { Button } from 'primereact/button';
import { Menu } from 'primereact/menu';

import './styles/btn.css';
import MenuItemModel from '../models/menu-item.model'

export default function ButtonDesign(props: { menuItem: MenuItemModel, closeNav: any }) {
  const { menuItem } = props;
  const menu = useRef<Menu>(null);

  const onClick = (e: React.MouseEvent) => {
    // runCodeSafely(() => {
    props.closeNav()
    if (menuItem.menu && menuItem.menu.length > 0) {
      menu.current.toggle(e);
    }
    if (menuItem.action) {
      menuItem.action()
    }
    // }, menuItem.action.name)
  }

  const showLabel = () => {
    if (menuItem.label == "Edit mode navigation" || menuItem.label == "Check render needed" ||
      menuItem.label == "Event callback" || menuItem.label == "Object world" || menuItem.label == "Map world" || menuItem.label == "2D/3D")
      return true
    else
      return false
  }

  return (<span style={{ display: 'flex' }}>
    <Button className="p-button-secondary p-button-text btn__button-toolbar" onClick={onClick}  >
      {menuItem.icon?.startsWith("http:") ? <img className='btn__img-icon' title={menuItem.label} src={menuItem.icon}></img>
        : null}

      {
        showLabel() ? <label >{menuItem.label}</label> : null
      }
    </Button>
    {menuItem.menu && menuItem.menu.length > 0 && <Menu model={menuItem.menu} popup ref={menu} />}
  </span>
  );
}