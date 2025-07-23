using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.Managers.ObjectWorld 
{
    public static class Manager_MCTextures
    {
        static Dictionary<IDNMcTexture, uint> dExistentTextures;

        static Manager_MCTextures()
        {
            dExistentTextures = new Dictionary<IDNMcTexture, uint>();
        }

        #region Static Methods
       
        public static void AddToDictionary(IDNMcTexture texture)
        {
            //Add texture to dictionary in case it doesn't exist already.
            if (!dTextures.ContainsKey(texture))
            {
                dTextures.Add(texture, (uint)texture.TextureType);
            }
        }

        public static void RemoveFromDictionary(IDNMcTexture texture)
        {
            //Remove texture from dictionary
            dTextures.Remove(texture);
        }
        
        public static Dictionary<IDNMcTexture, uint> dTextures
        {
            get { return dExistentTextures; }
        }

        #endregion
    }
}
