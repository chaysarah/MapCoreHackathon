using MCTester.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCTester.Managers
{
    public class GeneralFuncs
    {
        public static Type GetDirectInterface(Type aType)
        {
            Type ret = null;
            Type[] allInterfaces = aType.GetInterfaces();

            var minimalInterfaces = allInterfaces.Except(allInterfaces.SelectMany(t => t.GetInterfaces()));
            if (minimalInterfaces != null)
            {
                List<Type> minimalInterfacesLst = minimalInterfaces.ToList();
                if (minimalInterfacesLst.Count > 0)
                    ret = minimalInterfacesLst[0];
            }
            return ret;
        }

        public static string GetDirectInterfaceName(Type aType)
        {
            string ret = "";
            Type[] allInterfaces = aType.GetInterfaces();

            var minimalInterfaces = allInterfaces.Except(allInterfaces.SelectMany(t => t.GetInterfaces()));
            if (minimalInterfaces != null)
            {
                List<Type> minimalInterfacesLst = minimalInterfaces.ToList();
                if (minimalInterfacesLst.Count > 0)
                {
                    Type type = minimalInterfacesLst[0];
                    if (type != null)
                        ret = type.Name;
                }
            }
            return ret;
        }

        public static void SetControlsReadonly(Control control)
        {
            if (control.Controls.Count == 0)
            {
                if (control is TextBox)
                    ((TextBox)control).ReadOnly = true;
                else if (!(control is Button))
                    control.Enabled = false;
            }
            else
            {
                if (control is DataGridView)
                {
                    ((DataGridView)control).ReadOnly = true;
                    ((DataGridView)control).AllowUserToAddRows = false;
                    ((DataGridView)control).AllowUserToDeleteRows = false;
                }
                else
                {
                    foreach (Control node in control.Controls)
                    {
                        if (node is CtrlSMcBox)
                        {
                            ((CtrlSMcBox)node).SetReadOnly();
                        }
                        else if (node is CtrlBrowseControl)
                        {
                            ((CtrlBrowseControl)node).SetReadOnly();
                        }
                        else if (node is CtrlGridCoordinateSystem)
                        {
                            ((CtrlGridCoordinateSystem)node).SetReadOnly();
                        }
                        else
                            SetControlsReadonly(node);
                    }
                }
            }
        }

        public static Form GetParentForm(Control ctr)
        {
            if (ctr.Parent is Form)
                return ctr.Parent as Form;
            else
                return GetParentForm(ctr.Parent);
        }

        public static void CloseParentForm(Control ctr)
        {
            Form ContainerForm = GetParentForm(ctr);
            if(ContainerForm != null)
                ContainerForm.Close();
        }

    }
}
