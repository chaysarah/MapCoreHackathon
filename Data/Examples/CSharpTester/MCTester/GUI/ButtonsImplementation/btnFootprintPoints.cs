using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCTester.ButtonsImplementation
{
    public class btnFootprintPoints
    {
        bool m_isAsync;

        public btnFootprintPoints(bool isAsync)
        {
            m_isAsync = isAsync;
        }

        public void ExecuteAction()
        {
            Footprints Footprints = new Footprints();
            Footprints.DrawCameraFootprint(m_isAsync);
        }
    }
}
