using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCTester.ObjectWorld.ObjectsUserControls;
using System.Windows.Forms;
using MCTester.Managers.MapWorld;
using UnmanagedWrapper;
using MCTester.Managers.ObjectWorld;

namespace MCTester.ButtonsImplementation
{
    public class btnEditObject
    {
        public btnEditObject()
        {
        }

        public void ExecuteAction()
        {
            if(Manager_MCOverlayManager.ActiveOverlayManager == null)
            {
                MessageBox.Show("There is no active overlay manager");
                return;
            }

            ObjectItemSelectedFrm EditObjectListForm = new ObjectItemSelectedFrm("Edit");

            //Set form buttons to fit this action
            EditObjectListForm.btnOKPlay.Text = "OK";
            EditObjectListForm.btnAnimationStop.Visible = false;
            EditObjectListForm.chxAnimatedAll.Visible = false;

            EditObjectListForm.ShowDialog();
        }
    }
}
