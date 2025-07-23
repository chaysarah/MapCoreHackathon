using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers.ObjectWorld;
using MCTester.Managers.MapWorld;
using MCTester.General_Forms;
using System.Drawing;
using System.Windows.Forms;

namespace MCTester.ButtonsImplementation
{
    public class btnEditModeNavigation
    {
        private IDNMcEditMode m_EditMode;
        private IDNMcLineItem m_NavigationDefaultLine;
        
        public btnEditModeNavigation()
        {
            m_NavigationDefaultLine = DNMcLineItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                            DNELineStyle._ELS_SOLID,
                                                            DNSMcBColor.bcBlackOpaque,
                                                            5f);
        }

        public void ExecuteAction()
        {
            try
            {
                if (Program.AppMainForm.EditModeNavigationButtonState == true)
                {
                    if (EditModePropertiesBase.WaitForMouseClick == true)
                    {
                        try
                        {
                            EditMode.StartNavigateMap(EditModePropertiesBase.DrawLine,
                                                        EditModePropertiesBase.OneOperationOnly);
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("StartNavigateMap", McEx);
                        }
                    }
                    else
                    {
                        MCTMapFormManager.MapForm.MapObjectCtrl.MouseDownEvent += new MouseDownEventArgs(MapObjectCtrl_MouseDownEvent);                        
                    }

                    MCTMapFormManager.MapForm.Leave += new EventHandler(MapForm_Leave);
                }
                else
                {
                    try
                    {
                        if (EditMode.IsEditingActive == true)
                        {
                            EditMode.ExitCurrentAction(false);
                            Program.AppMainForm.EditModeNavigationButtonState = false;
                            MCTMapFormManager.MapForm.MapObjectCtrl.MouseDownEvent -= new MouseDownEventArgs(MapObjectCtrl_MouseDownEvent);
                        }
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("ExitCurrentAction", McEx);
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("StartNavigateMap", McEx);
            }
        }

        void MapForm_Leave(object sender, EventArgs e)
        {
            Program.AppMainForm.EditModeNavigationButtonState = false;
            if (MCTMapFormManager.MapForm != null)
            {
                MCTMapFormManager.MapForm.Leave -= new EventHandler(MapForm_Leave);
            }
            try
            {
                if (EditMode != null)
                {
                    if (EditMode.IsEditingActive == true)
                        EditMode.ExitCurrentAction(false);
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("ExitCurrentAction", McEx);
            }

        }

        void MapObjectCtrl_MouseDownEvent(object sender, Point mouseLocation, MouseButtons mouseClickedButton, int mouseWheelDelta)
        {
            try
            {
                DNSMcPoint devicePoint = new DNSMcPoint(mouseLocation.X, mouseLocation.Y);
                EditMode.StartNavigateMap(EditModePropertiesBase.DrawLine,
                                            EditModePropertiesBase.OneOperationOnly,
                                            EditModePropertiesBase.WaitForMouseClick,
                                            devicePoint,
                                            m_NavigationDefaultLine);
                
                MCTMapFormManager.MapForm.MapObjectCtrl.MouseDownEvent -= new MouseDownEventArgs(MapObjectCtrl_MouseDownEvent);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("StartNavigateMap", McEx);
            }
        }

        public IDNMcEditMode EditMode
        {
            get
            {
                m_EditMode = MCTMapFormManager.MapForm != null ? MCTMapFormManager.MapForm.EditMode : null;
                return m_EditMode;
            }
        }
    }
}
