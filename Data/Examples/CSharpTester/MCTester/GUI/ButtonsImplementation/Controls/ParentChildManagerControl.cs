using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MCTester.ObjectWorld;
using System.Reflection;
using MapCore;
using UnmanagedWrapper;
using System.Collections;

namespace MCTester.Controls
{
    public delegate void OnRefreshItemEventArgs(object ItemToRefresh);
    public delegate void OnItemChangedEventArgs(object SelectedItem);
	public partial class ParentChildManagerControl : UserControl, IUserControlItem
	{
		private object m_Obj;
		private string m_countFunctionName;
		private string m_GetAllFunctionName;

        private bool m_IsCountMethod;

        private string m_removeFunctionName;
        private string m_AddFunctionName;
        private bool m_bAssignShortValue;

        public static event OnRefreshItemEventArgs OnRefreshItem;
        public event OnItemChangedEventArgs OnItemChanged;

        private bool m_bUseCountingMethod;
        private object [] m_ObjectList;
        private ListBox m_itemList;

		public ParentChildManagerControl()
		{
			InitializeComponent();
            _IsCountMethod = true;
            _AssignShortValue = false;
            lstItems.SelectedIndexChanged += new EventHandler(lstItems_SelectedIndexChanged);

            m_bUseCountingMethod = false;

            this.VisibleChanged += new EventHandler(ParentChildManagerControl_VisibleChanged);
            ParentChildManagerControl.OnRefreshItem += new OnRefreshItemEventArgs(ParentChildManagerControl_OnRefreshItem);
                        
            ListBox.ObjectCollection listCollection = new ListBox.ObjectCollection(lstItems);
            m_ObjectList = new object[lstItems.Items.Count];
            listCollection.CopyTo(m_ObjectList, 0);

            m_itemList = lstItems;
        }

        void ParentChildManagerControl_OnRefreshItem(object ItemToRefresh)
        {
            if (m_Obj!=null)
                ((IUserControlItem)this).LoadItem(m_Obj);
        }

        void ParentChildManagerControl_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (this._AddFunctionName!=null && this._AddFunctionName.Length > 0)
                    btnAdd.Visible = true;
                else
                    btnAdd.Visible = false;
            }
        }

        void lstItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnItemChanged != null)
                OnItemChanged(lstItems.SelectedItem);
        }

        public object _Obj
        {
            get { return m_Obj; }
        }

        public object _SelectedObject
        {
            get { return lstItems.SelectedItem; }
        }

		public string _CountFunctionName
		{
			get { return m_countFunctionName; }
			set { m_countFunctionName = value; }
		}
		public string _GetAllFunctionName
		{
			get { return m_GetAllFunctionName; }
			set { m_GetAllFunctionName = value; }
		}

		public string _GroupTitle
		{
			get { return grpElements.Text; }
			set { grpElements.Text = value; }
		}

        public bool _IsCountMethod
        {
            get { return m_IsCountMethod; }
            set { m_IsCountMethod = value; }
        }

        public string _RemoveFunctionName
        {
            get { return m_removeFunctionName; }
            set { m_removeFunctionName= value; }
        }

        public string _AddFunctionName
        {
            get { return m_AddFunctionName; }
            set { m_AddFunctionName = value; }
        }

        public bool _AssignShortValue
        {
            get { return m_bAssignShortValue; }
            set { m_bAssignShortValue = value; }
        }

        public bool _UseCountingMethod
        {
            get { return m_bUseCountingMethod; }
            set { m_bUseCountingMethod = value; }
        }

        public object[] ObjectList
        {
            get { return m_ObjectList; }
            set { m_ObjectList = value; }
        }

        public ListBox itemList
        {
            get { return m_itemList; }
            set { m_itemList = value; }
        }

        #region IUserControlItem Members

        public void LoadItem(object aItem)
        {
            lstItems.Items.Clear();
			this.m_Obj = aItem;
			
			Type ObjType =m_Obj.GetType();

            uint uCountResult = 0;
            object[] oCountResult = null;

            if (_UseCountingMethod)
            {
                oCountResult = new object[1];
                if (_IsCountMethod)
                {
                    MethodInfo Mi = ObjType.GetMethod(_CountFunctionName);
                    if (Mi == null)
                    {
                        MessageBox.Show("Problem finding method name " + _CountFunctionName);
                        return;
                    }
                    oCountResult[0] = Mi.Invoke(aItem, null);
                    uCountResult = (uint)oCountResult[0];
                }
                else
                {
                    PropertyInfo Pi = ObjType.GetProperty(_CountFunctionName);
                    if (Pi == null)
                    {
                        MessageBox.Show("Problem finding property name " + _CountFunctionName);
                        return;
                    }

                    oCountResult[0] = Pi.GetValue(aItem, null);
                    uCountResult = (uint)oCountResult[0];
                }
            }
            

			MethodInfo GetMI = ObjType.GetMethod(_GetAllFunctionName);
			if (GetMI == null)
			{
				MessageBox.Show("Problem finding method name " + _CountFunctionName);
				return;
			}


			object oItemArray = GetMI.Invoke(aItem, oCountResult);
			List<object> lItemArray = new List<object>();
			lItemArray.AddRange((object[])oItemArray);


			foreach(object obj in lItemArray)
			{
				lstItems.Items.Add(obj);
			}
		}

		#endregion

        private void btnRemove_Click(object sender, EventArgs e)
        {
            Type ObjType = m_Obj.GetType();

            MethodInfo Mi = ObjType.GetMethod(_RemoveFunctionName);
            if (Mi == null)
            {
                MessageBox.Show("Problem finding method name " + _RemoveFunctionName);
                return;
            }

            object[] Params = new object[1];
            Params[0] = lstItems.SelectedItem;

            try
            {
                object Ret = Mi.Invoke(m_Obj, Params);
            }
            catch (MapCoreException McEx)
            {
            	MapCore.Common.Utilities.ShowErrorMessage("", McEx);
            }

            ((IUserControlItem)this).LoadItem(m_Obj);

            if (OnRefreshItem != null)
                OnRefreshItem(null);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Type ObjType = m_Obj.GetType();

            MethodInfo Mi = ObjType.GetMethod(_AddFunctionName);
            if (Mi == null)
            {
                MessageBox.Show("Problem finding method name " + _AddFunctionName);
                return;
            }

            if (_AssignShortValue)
            {
                object[] Params = new object[1];
                Params[0] = (short)0;
                object Ret = Mi.Invoke(m_Obj, Params);
            }
            else
            {
                object Ret = Mi.Invoke(m_Obj, null);
            }


            ((IUserControlItem)this).LoadItem(m_Obj);
            if (OnRefreshItem != null)
                OnRefreshItem(null);
        }

        public static void RefreshDisplay()
        {
            if (OnRefreshItem != null)
                OnRefreshItem(null);
        }

	}
}
