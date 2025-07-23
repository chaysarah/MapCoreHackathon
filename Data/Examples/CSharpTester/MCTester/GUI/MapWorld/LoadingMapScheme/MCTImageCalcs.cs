using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.MapWorld.LoadingMapScheme
{
    [Serializable]
    public class MCTImageCalcs
    {
        private List<MCTImageCalc> m_ImageCalcs;

        public MCTImageCalcs()
        {
            m_ImageCalcs = new List<MCTImageCalc>();
        }

        public MCTImageCalc GetImageCalc(int ImageCalcID)
        {
            MCTImageCalc ret = null;
            foreach (MCTImageCalc ImCal in ImageCalc)
            {
                if (ImCal.ID == ImageCalcID)
                {
                    ret = ImCal;
                    break;
                }
            }

            return ret;
        }

        public int GetNextImageCalcID()
        {
            int max = 0;
            foreach (MCTImageCalc ImCal in ImageCalc)
            {
                if (ImCal.ID > max)
                {
                    max = ImCal.ID;
                }
            }

            return max + 1;
        }
        #region Public Properties
        public List<MCTImageCalc> ImageCalc
        {
            get { return m_ImageCalcs; }
            set { m_ImageCalcs = value; }
        }

        #endregion

    }
}
