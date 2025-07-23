using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.MapWorld
{
    [Serializable]
    public class MCTMapViewports
    {
        private List<MCTMapViewport> m_Viewports;

        public MCTMapViewports()
        {
            m_Viewports = new List<MCTMapViewport>();
        }

        public MCTMapViewport GetViewport(int ViewportID)
        {
            MCTMapViewport ret = null;
            foreach (MCTMapViewport r in Viewport)
            {
                if (r.ID == ViewportID)
                {
                    ret = r;
                    break;
                }
            }

            return ret;
        }

        public int GetNextId()
        {
            int max = 0;

            foreach (MCTMapViewport r in Viewport)
            {
                if (r.ID >max)
                {
                    max = r.ID;
                }
            }
            return max + 1;
        }

        public void RemoveViewport(int ViewportID)
        {
            MCTMapViewport mctViewport = Viewport.Find(x => x.ID == ViewportID);
            if (mctViewport != null)
                Viewport.Remove(mctViewport);
        }

        #region Public Properties

        public List<MCTMapViewport> Viewport
        {
            get { return m_Viewports; }
            set { m_Viewports = value; }
        }

        
        #endregion
    }
}
