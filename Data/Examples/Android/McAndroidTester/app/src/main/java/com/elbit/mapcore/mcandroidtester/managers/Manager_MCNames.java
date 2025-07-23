package com.elbit.mapcore.mcandroidtester.managers;

import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.BaseApplication;
import com.elbit.mapcore.mcandroidtester.utils.Consts;

import java.util.Hashtable;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcConditionalSelector;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectLocation;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectScheme;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcObjectSchemeNode;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

/**
 * Created by tc99382 on 24/01/2017.
 */
public class Manager_MCNames {
    private static Hashtable<Integer, String> names;
    private static Manager_MCNames instance;

    private Manager_MCNames() {
        names = new Hashtable<>();
    }

    public static Hashtable<Integer, String> getNames() {
        return names;
    }

    public static void setNames(Hashtable<Integer, String> names) {
        Manager_MCNames.names = names;
    }

    public String getNameById(Integer id) {
        if (names.containsKey(id))
            return names.get(id);
        return String.valueOf(id);
    }

    public String getNameById(Integer id, String type) {
        return "(" + getNameById(id) + ") " + type;
    }

    public String getNameByObject(Object obj) {
        StringBuilder objType = getType(obj);
        return getNameByObject(obj, objType.toString());
    }

    public String[] getNameByObjectArray(Object obj, String[] arrOutput) {
        StringBuilder objType = getType(obj);
        String id = getIdByObject(obj);
        arrOutput[0] = id;
        arrOutput[1] = id + " " + objType;
        return arrOutput;
    }


    private StringBuilder getType(Object obj) {
        String typeName = obj.getClass().getSimpleName();
        typeName = typeName.replace("Mc", "");
        StringBuilder modifiedString = new StringBuilder();
        for (Character s : typeName.toCharArray()) {
            if (Character.isUpperCase(s))
                modifiedString.append(' ');
            modifiedString.append(s);
        }
        return modifiedString;
    }

    public String getNameByObject(Object obj, String objType) {
        String fullName = "(" + getIdByObject(obj) + ")";
        return fullName + " " + objType;
    }

    public String getIdByObject(Object obj) {
        Integer id = obj.hashCode();
        String fullName;
        String mcName = Consts.EMPTY_STRING;
        if (obj instanceof IMcObjectScheme) {
            try {
                mcName = (((IMcObjectScheme) obj).GetName());
            } catch (MapCoreException e) {
                AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), e, "IMcObjectScheme.GetName()");
            } catch (Exception ex) {
                ex.printStackTrace();
            }
        } else if (obj instanceof IMcObjectLocation) {
            try {
                mcName = ((IMcObjectLocation) obj).GetName();
            } catch (MapCoreException mcEx) {
                AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), mcEx, "IMcObjectLocation.GetName()");
            } catch (Exception e) {
                e.printStackTrace();
            }
        } else if (obj instanceof IMcObjectSchemeNode) {
            try {
                mcName = ((IMcObjectSchemeNode) obj).GetName();
            } catch (MapCoreException mcEx) {
                AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), mcEx, "IMcObjectSchemeNode.GetName()");
            } catch (Exception e) {
                e.printStackTrace();
            }
        } else if (obj instanceof IMcConditionalSelector) {

            try {
                mcName = ((IMcConditionalSelector) obj).GetName();
            } catch (MapCoreException mcEx) {
                AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), mcEx, "IMcConditionalSelector.GetName()");
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
        if (mcName != Consts.EMPTY_STRING && mcName != null) {
            fullName = mcName;
        } else if (names.containsKey(id)) {
            fullName = names.get(id);
        } else
            fullName = String.valueOf(id);
        return fullName;

    }

    private String getDefaultName(Object obj) {
        StringBuilder objType = getType(obj);
        return getNameByObject(obj, objType.toString());
    }

    public void setName(Object node, String newName) {

        if (!setMcName(node, newName)) {
            Integer id = node.hashCode();
            names.put(id, newName);
        }
    }

    private boolean setMcName(Object node, String newName) {
         boolean isChanged = false;
        if (node instanceof IMcObjectScheme) {
            isChanged = true;
            try {

                ((IMcObjectScheme) node).SetName(newName);
            } catch (MapCoreException McEx) {
                AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), McEx, "IDNMcObjectScheme.SetName()");
            } catch (Exception e) {

            }
        }
        if (node instanceof IMcObjectSchemeNode) {
            isChanged = true;
            try {
                ((IMcObjectSchemeNode) node).SetName(newName);
            } catch (MapCoreException McEx) {
                AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), McEx, "IDNMcObjectSchemeNode.SetName()");
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
        if (node instanceof IMcConditionalSelector) {
            isChanged = true;
            try {
                ((IMcConditionalSelector) node).SetName(newName);
            } catch (MapCoreException McEx) {
                AlertMessages.ShowMapCoreErrorMessage(BaseApplication.getCurrActivityContext(), McEx, "IDNMcConditionalSelector.SetName()");
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
        return isChanged;
    }

    public boolean removeName(Object node) {
        Integer id = Integer.valueOf(node.hashCode());
        boolean isRemoved = false;
        if (setMcName(node, Consts.EMPTY_STRING))
            isRemoved = true;
        if (!isRemoved && names.containsKey(id)) {
            names.remove(id);
            isRemoved = true;
        }
        return isRemoved;
    }

    public static Manager_MCNames getInstance() {
        if (instance == null)
            instance = new Manager_MCNames();
        return instance;
    }


}
