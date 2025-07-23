using System;
using System.Collections.Generic;
using System.Text;
using UnmanagedWrapper;
using MapCore;

namespace MCTester.Managers.ObjectWorld
{
    public static class Manager_MCMesh
    {
        private static Dictionary<IDNMcMesh, uint> m_Mesh;
        
        static Manager_MCMesh()
        {
            m_Mesh = new Dictionary<IDNMcMesh, uint>();
        }

        public static void AddToDictionary(IDNMcMesh mesh)
        {
            //Add Mesh to dictionary in case the it doesn't exist already.
            if (!m_Mesh.ContainsKey(mesh))
            {
                m_Mesh.Add(mesh, (uint)mesh.MeshType);
            }
        }

        public static void RemoveFromDictionary(IDNMcMesh mesh)
        {
            m_Mesh.Remove(mesh);
        }

        public static Dictionary<IDNMcMesh, uint> dMesh
        {
            get
            {
                return m_Mesh;
            }
        }

    }
}
