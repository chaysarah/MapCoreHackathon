using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MCTester.Managers;

namespace MCTester.GUI.Trees
{
    public class TreeFormBaseClass
    {
        private string m_SrcDataArray;
        private Dictionary<Type,string> m_HandlerPanelType;
        private object m_tempObject;
        private Dictionary<Type,TreeFormBaseClass> m_children;
        private Type m_ClassHandles;

        public TreeFormBaseClass()
        {
			m_children = new Dictionary<Type, TreeFormBaseClass>();
			m_HandlerPanelType = new Dictionary<Type, string>();            
        }

        public TreeFormBaseClass(TreeFormBaseClass Src)
            : this()
        {
            if (Src!=null)
            {
                m_SrcDataArray = Src.SourceDataArray;
                m_HandlerPanelType = Src.HandlerPanelType;
                m_tempObject = Src.TemporaryObject;
                m_children = Src.Children;
                m_ClassHandles = Src.ClassHandles;
            }
            
        }

        public TreeFormBaseClass GetChildThatHandlesType(Type aType)
        {
            TreeFormBaseClass ret = null;

            Type directInterface = GeneralFuncs.GetDirectInterface(aType);
            if (directInterface != null && Children.ContainsKey(directInterface))
            {
                ret = Children[directInterface];
            }
            return ret;
        }

       /* public Type GetDirectInterface(Type aType)
        {
            Type ret = null;
            Type[] allInterfaces = aType.GetInterfaces();

            var minimalInterfaces = allInterfaces.Except(allInterfaces.SelectMany(t => t.GetInterfaces()));
            if (minimalInterfaces != null)
            {
                List<Type> minimalInterfacesLst = minimalInterfaces.ToList();
                if (minimalInterfacesLst.Count > 0 )
                    ret = minimalInterfacesLst[0];
            }
            return ret;
        }*/

        #region Public Properties
        public string SourceDataArray
        {
            get { return this.m_SrcDataArray; }
            set { this.m_SrcDataArray = value; }
        }

        public Dictionary<Type, string> HandlerPanelType
        {
            get { return this.m_HandlerPanelType; }
            set { m_HandlerPanelType = value; }
        }

        public object TemporaryObject
        {
            get { return m_tempObject; }
            set { m_tempObject = value; }
        }

        public Dictionary<Type, TreeFormBaseClass> Children
        {
            get { return this.m_children; }
            set { m_children = value; }
        }

        public Type ClassHandles
        {
            get { return m_ClassHandles; }
            set { m_ClassHandles = value; }
        }


        #endregion
    }
}
