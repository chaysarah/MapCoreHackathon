using MapCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTester.Managers.MapWorld
{
    public static class Manager_MapLayerCallbacks
    {
        private static Dictionary<IDNMcMapLayer, MCTMapLayerReadCallback> dLayerReadCallbacks = new Dictionary<IDNMcMapLayer, MCTMapLayerReadCallback>();

        public static void AddLayerCallback(IDNMcMapLayer layer, MCTMapLayerReadCallback callback)
        {
            dLayerReadCallbacks.Add(layer, callback);
        }

        public static void ReplaceLayerCallback(IDNMcMapLayer layer, MCTMapLayerReadCallback newCallback)
        {
            if (dLayerReadCallbacks.ContainsKey(layer))
            {
                MCTMapLayerReadCallback oldCallback = dLayerReadCallbacks[layer];
                dLayerReadCallbacks[layer] = newCallback;
            }
        }
        public static IDNMcMapLayer GetLayerByCallback(MCTMapLayerReadCallback callback)
        {
            KeyValuePair<IDNMcMapLayer, MCTMapLayerReadCallback> item = dLayerReadCallbacks.First(x => x.Value == callback);
            return item.Key;
        }
    }
}
