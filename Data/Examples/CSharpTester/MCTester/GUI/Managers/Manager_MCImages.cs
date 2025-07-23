using MapCore;
using MapCore.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MCTester.Managers
{
    public static class Manager_MCImages
    {
        public static List<IDNMcImage> GetImages()
        {
            List<IDNMcImage> images = new List<IDNMcImage>();
            foreach (IDNMcFont font in Managers.ObjectWorld.Manager_MCFont.dFont.Keys)
            {
                try
                {
                    bool bUseSpecialCharsColors;
                    DNSSpecialChar[] mcSpecialChars;
                    font.GetSpecialChars(out mcSpecialChars, out bUseSpecialCharsColors);
                    if (mcSpecialChars != null && mcSpecialChars.Length > 0)
                    {
                        for (int i = 0; i < mcSpecialChars.Length; i++)
                        {
                            if (mcSpecialChars[i].pImage != null && !images.Contains(mcSpecialChars[i].pImage))
                            {
                                images.Add(mcSpecialChars[i].pImage);
                            }
                        }
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetSpecialChars", McEx);
                }
            }
            return images;
        }
    }
}
