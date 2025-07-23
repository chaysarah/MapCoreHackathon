import MenuItemModel from "./menu-item.model";

export default interface TabMenuItemModel {
    header: string;
    menuItems?: MenuItemModel[];
    action?: () => void;
}