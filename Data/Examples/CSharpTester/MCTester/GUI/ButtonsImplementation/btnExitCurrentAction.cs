using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapCore;
using MCTester.Managers.ObjectWorld;
using MCTester.Managers.MapWorld;
using UnmanagedWrapper;

namespace MCTester.ButtonsImplementation
{
    public class btnExitCurrentAction
    {
        public btnExitCurrentAction()
        {
        }

        public void ExecuteAction()
        {
            IDNMcEditMode m_EditMode = MCTMapFormManager.MapForm.EditMode;
            
            try
            {
                m_EditMode.ExitCurrentAction(EditModePropertiesBase.DiscardChanges);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("ExitCurrentAction", McEx);
            } 
        }
    }
}
