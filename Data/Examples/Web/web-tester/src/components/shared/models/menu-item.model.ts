import { MenuItem } from "primereact/menuitem";

export default interface MenuItemModel {
    label: string;
    icon?: string;
    action?: () => void;
    menu?: MenuItem[];
}