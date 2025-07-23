using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using MapCore.Common;
using UnmanagedWrapper;

namespace MCTester.Managers.ObjectWorld
{
    public static class Manager_MCFont
    {
        static Dictionary<IDNMcFont, uint> dExistingFonts;

        static Manager_MCFont()
        {
            dExistingFonts = new Dictionary<IDNMcFont, uint>();
        }

        #region Static Methods
        public static void AddToDictionary(IDNMcFont font)
        {
            if (font != null)
            {
                //Add font to dictionary in case the it doesn't exist already.
                if (!dFont.ContainsKey(font))
                {
                    dFont.Add(font, (uint)font.FontType);
                }
            }
        }

        public static void RemoveFromDictionary(IDNMcFont font)
        {
            dFont.Remove(font);
        }

        public static Dictionary<IDNMcFont, uint> dFont
        {
            get { return dExistingFonts; }
        }

        #endregion
    }
}
