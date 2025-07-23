import React, { Component } from 'react';
import cn from './ContextMenu.module.css';
import arrowRight from '../../assets/images/arrow-right.svg';
import ApplicationContext from '../../context/applicationContext';
import config, { popupTypes, popupSize, layerTypesStrings, mapActions } from "../../config";

export default class ContextMenu extends Component {
    static contextType = ApplicationContext;
    constructor(props) {
        super(props);
        this.CONTEXT_MENU_WIDTH = 200;
        this.SUB_MENU_WIDTH = 150;
        this.contextMenuRef = React.createRef();

        this.state = {
            top: 0,
            left: 0,
            menuItems: [],
            node: {},
            nodeLevel: '',
            subMenu: null
        };
    }

    updatePosition = (left, top, menuItems, nodeLevel, node) => {
        document.removeEventListener('click', this.hideContextMenu);
        const height = this.contextMenuRef && this.contextMenuRef.current && this.contextMenuRef.current.clientHeight || menuItems.length * 42;
        this.setState(
            {
                top: top + height > document.body.offsetHeight ? document.body.offsetHeight - (height + 10) : top,
                left: left > document.body.offsetWidth - this.CONTEXT_MENU_WIDTH ? left - this.CONTEXT_MENU_WIDTH : left,
                menuItems,
                node,
                nodeLevel
            },
            () => {
                document.addEventListener('click', this.hideContextMenu);
            }
        );
    }

    hideContextMenu = () => {
        this.setState(
            { menuItems: [], subMenu: {}, node: {}, nodeLevel: '' },
            () => {
                document.removeEventListener('click', this.hideContextMenu);
            }
        );
    }
    handleMouseOver(e, menuItem) {
        if (menuItem.subMenuItems) {
            this.toggleSubMenu(
                e,
                menuItem.subMenuItems,
                menuItem
            )
        }
        else {
            this.toggleSubMenu()
        }
    }

    handleMouseOut(e, menuItem) {
        if (!menuItem.subMenuItems) {
            this.toggleSubMenu()
        }
    }

    toggleSubMenu(e, items, parent) {
        let subMenu = null;
        if (e && items && parent) {
            const clickedElement = e.currentTarget.getBoundingClientRect();
            let top = clickedElement.y - (Math.floor((items.length / 2)) * clickedElement.height);
            if (top + (clickedElement.height * items.length) > window.innerHeight - 40) {
                // if sub menu is cutted by the page buttom
                top = window.innerHeight - (clickedElement.height * items.length) - 40
            }
            subMenu = { items, parent, top };
        }

        this.setState({ subMenu });
    }
    getLyersOfRepositoryWithoutBacklog() {
        let filteredLayersWithoutBacklog = this.props.groupsTree.filter(g => g.title != config.LAYERS_BACKLOGS_TITLE);
        return filteredLayersWithoutBacklog.map(g => g.childNodes).flat();
    }

    getContextMenu() {
        let contextMenu = this.state.menuItems.map(menuItem => (
            <li
                key={menuItem.name}
                // menuItem.name === "Export Layer" || was in line 97
                className={
                    ((this.state.node?.title === config.LAYERS_BACKLOGS_TITLE)
                        && (menuItem.name === 'Add Layer' || menuItem.name === 'Export Group' || (menuItem.name === 'Clear Backlog' && this.props.groupsTree[0].childNodes.length == 0))
                        || (this.state.node?.parentFolder === config.LAYERS_BACKLOGS_TITLE
                            && (menuItem.name === "Edit Layer" || menuItem.name === "Preview" || menuItem.name === "Build Search Indexes" || menuItem.name === "Export Layer" || menuItem.name === "Export Layers")
                            || (this.props.groupsTree.length == 1 && menuItem.name === 'Select Group')
                            || (menuItem.name === 'Move To Groups' && this.props.groupsTree.length < 2)
                        )
                        || (this.props.selctedGroupsLayersNodesArr[0].length > 1 && (menuItem.name === "Rename Group" || menuItem.name === "Add Layer"))
                    ) ? cn.ListElementDisabled : cn.ListElement}

                onClick={
                    menuItem.subMenuItems
                        ? undefined
                        : e => {
                            e.preventDefault();
                            this.props.onContextMenuItemClick({
                                type: menuItem.type,
                                name: menuItem.name,
                                nodeLevel: this.state.nodeLevel,
                                node: this.state.node
                            }
                            );
                            this.config(menuItem);
                        }
                }
                onMouseOver={e => this.handleMouseOver(e, menuItem)}
                onMouseOut={e => this.handleMouseOut(e, menuItem)}
            >
                {menuItem.iconType ? (
                    <img
                        className={cn.ContextMenuIcon}
                        src={menuItem.iconType}
                        alt=''
                    />
                ) : null}
                <a >{menuItem.name}</a>
                {menuItem.subMenuItems ? (
                    <img
                        className={cn.arrowRightIcon}
                        src={arrowRight}
                        alt=''
                    />
                ) : null}
            </li>
        ));

        return (
            <div
                className={`${cn.ContextMenu}`}
                style={{
                    width: this.CONTEXT_MENU_WIDTH,
                    top: this.state.top,
                    left: this.state.left
                }}>
                <ul ref={this.contextMenuRef} className={`${cn.ListWrapper}`}>{contextMenu}</ul>
            </div>
        );
    }
    config = (menuItem) => {
        if (menuItem.name == 'Configuration') {
            this.context.setReadOnly(true);
        }
    }

    getSubMenu() {
        let subMenu = this.state.subMenu.items.map(menuItem => {
            const data = {
                type: this.state.subMenu.parent.type,
                name: this.state.subMenu.parent.name,
                nodeLevel: this.state.nodeLevel,
                node: this.state.node,
                layerType: menuItem.name.includes("Raw ") ? menuItem.name.split("Raw ")[1] : menuItem.name
            };
            let liStyle = menuItem.name == "Native Layer" ? cn.nativeLayer : "";
            return (
                <li
                    key={menuItem.name}
                    className={`${cn.ListElement} ${liStyle}`}
                    onClick={e => {
                        e.preventDefault();
                        this.props.onContextMenuItemClick(data);
                    }}>

                    {menuItem.iconType ? (
                        <img
                            className={cn.ContextMenuIcon}
                            src={menuItem.iconType}
                            alt=''
                        />
                    ) : null}
                    <a>{menuItem.name}</a>
                </li>
            );
        });

        return (
            <div
                className={`${cn.ContextMenu}`}
                style={{
                    width: this.SUB_MENU_WIDTH,
                    top: this.state.subMenu.top,
                    left: this.state.left + this.CONTEXT_MENU_WIDTH
                }}>
                <ul className={`${cn.ListWrapper}`}>{subMenu}</ul>
            </div>
        );
    }

    render() {
        const { menuItems, subMenu } = this.state;
        const contextMenu = menuItems && menuItems.length > 0 ? this.getContextMenu() : '';
        const secondaryMenu = subMenu && subMenu.items && subMenu.items.length > 0 ? this.getSubMenu() : '';

        return (
            <>
                {contextMenu}
                {secondaryMenu}
            </>
        );
    }
}
