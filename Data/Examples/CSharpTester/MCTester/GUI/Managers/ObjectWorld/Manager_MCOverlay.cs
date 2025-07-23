using System;
using System.Collections.Generic;
using System.Text;
using UnmanagedWrapper;
using MapCore;
using static MCTester.MapWorld.MapUserControls.ucVectorLayer;

namespace MCTester.Managers.ObjectWorld
{
    public static class Manager_MCOverlay
    {
        static Dictionary<object, uint > dOverlays;

        static Dictionary<int,IDNMcOverlay> dVectoryItemOverlaysPerLayer;

        public static void AddOverlayToVectoryItemOverlays(int layerId, IDNMcOverlay overlay)
        {
            if (dVectoryItemOverlaysPerLayer == null)
                dVectoryItemOverlaysPerLayer = new Dictionary<int, IDNMcOverlay>();

            dVectoryItemOverlaysPerLayer.Add(layerId, overlay);

        }

        public static IDNMcOverlay GetOverlayOfLayer(int layerId)
        {
            
                if (dVectoryItemOverlaysPerLayer != null && dVectoryItemOverlaysPerLayer.ContainsKey(layerId))
                    return dVectoryItemOverlaysPerLayer[layerId];
           

            return null;
        }

        public static bool IsExistOverlayVectorItem()
        {
            return dVectoryItemOverlaysPerLayer != null && dVectoryItemOverlaysPerLayer.Count > 0;
        }

        public static void RemoveAllOverlaysFromVectorItemsOverlay()
        {
            if (dVectoryItemOverlaysPerLayer != null)
                foreach (int layerId in dVectoryItemOverlaysPerLayer.Keys)
                    dVectoryItemOverlaysPerLayer[layerId].Remove();
            dVectoryItemOverlaysPerLayer = null;
        }

        public static void RemoveOverlaysFromVectoryItemOverlays(int layerId)
        {
            if (dVectoryItemOverlaysPerLayer != null && dVectoryItemOverlaysPerLayer.ContainsKey(layerId))
            {
                dVectoryItemOverlaysPerLayer[layerId].Remove();
                dVectoryItemOverlaysPerLayer.Remove(layerId);
            }
        }


        static Manager_MCOverlay()
        {
            dOverlays = new Dictionary<object, uint >();
        }

        public static Dictionary<object, uint> AllParams
        {
            get { return dOverlays; }            
        }

        public static Dictionary<object, uint > GetChildren(object Parent)
        {
			Dictionary<object, uint > Ret = new Dictionary<object, uint >();
			if (Parent is IDNMcOverlay)
			{
				IDNMcOverlay overlay = (IDNMcOverlay)Parent;

				IDNMcObject[] objectsInOverlay = overlay.GetObjects();

				uint i = 0;
				foreach (IDNMcObject currObj in objectsInOverlay)
				{
					Ret.Add(currObj,i++);
				}

			}
			return Ret;
        }

        public static void RemoveOverlay(IDNMcOverlay overlayToRemove)
        {
            if (overlayToRemove != null)
            {
                overlayToRemove.Remove();
                overlayToRemove.Dispose();
            }

        }
    }
}
