using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;
using MCTester.MapWorld;

namespace MCTester.MapWorld
{
    [Serializable]
    public class MCTMapSchemas
    {
        private List<MCTMapSchema> m_schemas;

        public MCTMapSchemas()
        {
            m_schemas = new List<MCTMapSchema>();
        }

        public List<MCTMapSchema> Schema
        {
            get { return m_schemas; }
            set 
            {
                m_schemas = value;
            }
        }

        public MCTMapSchema GetSchema(int SchemaID)
        {
            MCTMapSchema ret = null;
            foreach (MCTMapSchema scheme in m_schemas)
            {
                if (scheme.ID == SchemaID)
                {
                    ret = scheme;
                    break;
                }
            }

            return ret;
        }
        public int GetNextID()
        {
            int max = 0;
            foreach (MCTMapSchema s in m_schemas)
            {
                if (s.ID > max)
                {
                    max = s.ID;
                }
            }

            return max + 1;
        }

        public void RemoveScheme(int SchemeID)
        {
            MCTMapSchema mctScheme = Schema.Find(x => x.ID == SchemeID);
            if (mctScheme != null)
                Schema.Remove(mctScheme);
        }

    }
}
