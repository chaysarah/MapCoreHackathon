using System;
using System.Collections.Generic;
using System.Text;
using UnmanagedWrapper;
using MapCore;
using System.Collections;
using System.Windows.Forms;
using MCTester.GUI.Trees;
using MCTester.Managers.MapWorld;
using MCTester.Managers.VectorialWorld;

namespace MCTester.Managers.ObjectWorld
{
    public static class Manager_MCOverlayManager
    {
        static Dictionary<IDNMcOverlayManager, IDNMcOverlay> dOM_Overlay = new Dictionary<IDNMcOverlayManager, IDNMcOverlay>();
        //static IDNMcOverlayManager m_ActiveOverlayManager = null;
       
        static Manager_MCOverlayManager()
        {

        }


        public static IDNMcOverlayManager CreateOverlayManager(IDNMcGridCoordinateSystem gridCoordSysDef)
        {
            try
            {
                IDNMcOverlayManager activeOverlayManager = DNMcOverlayManager.Create(gridCoordSysDef);
                dOM_Overlay.Add(activeOverlayManager, null);

                return activeOverlayManager;	
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcOverlayManager.Create", McEx);
                return null;
            }
        }

        public static void UpdateOverlayManager(IDNMcOverlayManager overlayManager, IDNMcOverlay overlay)
        {

            if (dOM_Overlay.ContainsKey(overlayManager))
                dOM_Overlay[overlayManager] = overlay;
            else
                dOM_Overlay.Add(overlayManager, overlay);

        }

        public static IDNMcOverlay GetActiveOverlayOfOverlayManager(IDNMcOverlayManager overlayManager)
        {
            if (overlayManager != null && dOM_Overlay.ContainsKey(overlayManager))
                return dOM_Overlay[overlayManager];
            else
                return null;
        }

        public static IDNMcOverlay ActiveOverlay
        {
            get
            {
                if (ActiveOverlayManager != null)
                {
                    return dOM_Overlay[ActiveOverlayManager];
                }
                else
                    return null;
            }
           /* set
            {
                if (ActiveOverlayManager != null)
                    if (dOM_Overlay.ContainsKey(ActiveOverlayManager))
                        dOM_Overlay[ActiveOverlayManager] = value;
            }*/
        }

        public static void SetActiveOverlayToOverlayManager(IDNMcOverlay activeOverlay, IDNMcOverlayManager overlayManager)
        {
            if (dOM_Overlay.ContainsKey(overlayManager))
                dOM_Overlay[overlayManager] = activeOverlay;
            else
                dOM_Overlay.Add(overlayManager, activeOverlay);
        }

        public static IDNMcOverlayManager ActiveOverlayManager
        {
            get
            {
                if (MCTMapFormManager.MapForm != null && MCTMapFormManager.MapForm.Viewport != null)
                    return MCTMapFormManager.MapForm.Viewport.OverlayManager;
                else
                    return null;
            }
        }

        private static Form GetParentForm(Control ctr)
        {
            if (ctr.Parent is Form)
                return ctr.Parent as Form;
            else
                return GetParentForm(ctr.Parent);
        }

        public static Dictionary<IDNMcOverlayManager, IDNMcOverlay> dOverlayManager_Overlay
        {
            get { return dOM_Overlay; }
        }

        public static Dictionary<object, uint> AllParams
        {
            get 
            {
                Dictionary<object, uint> Ret = new Dictionary<object, uint>();
                uint i =0;
                foreach (IDNMcOverlayManager keyOM in dOM_Overlay.Keys)
                {
                    Ret.Add(keyOM, i);
                    i++;
                }
                return Ret; 
            }
        }

        public static Dictionary<object, uint > GetChildren(object Parent)
        {
			IDNMcOverlayManager Om = (IDNMcOverlayManager)Parent;
            Dictionary<object, uint > Ret = new Dictionary<object, uint >();

			if (Om == null)
                return Ret;

            IDNMcOverlay[] Overlays = Om.GetOverlays();

            uint i = 0;
            foreach (IDNMcOverlay overlay in Overlays)
            {
                Ret.Add(overlay, i++);
            }

			IDNMcCollection[] collections = Om.GetCollections();
			foreach(IDNMcCollection col in collections)
			{
                Ret.Add(col, i++);
			}

            IDNMcObjectScheme[] schemes = Om.GetObjectSchemes();
            foreach (IDNMcObjectScheme scheme in schemes)
            {
                if(!Manager_MCObjectScheme.IsTempObjectScheme(scheme))
                    Ret.Add(scheme, i++);
            }

            IDNMcConditionalSelector [] selectors = Om.GetConditionalSelectors();
            foreach (IDNMcConditionalSelector selector in selectors)
            {
                Ret.Add(selector, i++);
            }

            if (Manager_MCVectorial.lMapProductionParams.Count > 0)
            {
                IDNMcObject[] vectorObjects;
                for (int idx = 0; idx < Manager_MCVectorial.lMapProductionParams.Count; idx++)
                {
                    vectorObjects = Manager_MCVectorial.lMapProductionParams[idx].lObjectsToUpdate.ToArray();
                    foreach (IDNMcObject vectorObj in vectorObjects)
                    {
                        if (!Ret.ContainsKey(vectorObj))
                            Ret.Add(vectorObj, i++);
                    }
                
                }
            }

            return Ret;
        }

      
    }
}
