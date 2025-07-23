import React from 'react';
import { TabView, TabPanel } from 'primereact/tabview';

import './styles/TabView.css';
import ButtonToolbar from '../buttonToolbar/button'
import TabMenuModel from '../models/tab-menu.model';
import TabMenuItemModel from '../models/tab-menu-item.model';
import MenuItemModel from '../models/menu-item.model';


export default function TabMenu(props: { toolbar: TabMenuModel, flagMenu: Boolean, closeNav: any }) {
    const { toolbar } = props;
    const [open, setOpen] = React.useState(!props.flagMenu);
    const [activeIndex, setActiveIndex] = React.useState(0);

    const handleChange = () => {
        setOpen(true);
    };

    return (
        <div className="tabview-demo">
            <div>
                <TabView onTabChange={(e) => { setActiveIndex(e.index); toolbar.items[e.index].action && toolbar.items[e.index].action() }} activeIndex={activeIndex} onClick={() => { handleChange() }} scrollable>
                    {toolbar.items.map((item: TabMenuItemModel, index: number) => {
                        return <TabPanel className='tab' key={index} header={item.header} style={{ zIndex: '100', backgroundColor: 'white', display: 'flex' }} >
                            {open ? item.menuItems?.map((menuItem: MenuItemModel, indexBtn: number) => {
                                return <ButtonToolbar key={indexBtn} menuItem={menuItem} closeNav={props.closeNav}></ButtonToolbar>
                            }) : ""}
                        </TabPanel>
                    })}
                </TabView>
            </div>
        </div>
    )
}

