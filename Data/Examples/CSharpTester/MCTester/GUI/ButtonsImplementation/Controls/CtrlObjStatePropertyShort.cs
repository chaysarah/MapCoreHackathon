using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;

namespace MCTester.Controls
{
    public partial class CtrlObjStatePropertyShort : CtrlObjStatePropertyDataHandlerShort
    {
        public CtrlObjStatePropertyShort()
        {
            InitializeComponent();
            m_InitValue = 0;
        }

        public short RegShortVal
        {
            get { return ntbRegProperty.GetShort(); }
            set { ntbRegProperty.SetShort(value); }
        }

        public short SelShortVal
        {
            get { return ntbSelProperty.GetShort(); }
            set { ntbSelProperty.SetShort(value); }
        }


        public override void ObjectStateAdd(byte objectState, bool bInitCtrl)
        {
            OnObjectStateAddData(objectState);
            if (bInitCtrl)
                SelShortVal = m_InitValue;
        }

        public override void ObjectStateChanged(byte objectState, byte previousState)
        {
            OnObjectStateChangedData(previousState, SelShortVal);

            if (IsExistObjectState(objectState))
            {
                SelShortVal = GetSelValByObjectState(objectState);
            }
        }

        //Special implement for IMcSymolicItem.SetAttachPointPositionValue 
        public void Save(delegateSaveWithParentParam func, uint numTypeParentIndex)
        {
            base.Save(func,numTypeParentIndex, RegShortVal);
        }

        public void Save(delegateSave func)
        {
            base.Save(func, RegShortVal);
        }

        public new void Load(delegateLoad func)
        {
            SelShortVal = m_InitValue;
            RegShortVal = base.Load(func);
        }

    }

    public class CtrlObjStatePropertyDataHandlerShort : CtrlObjStatePropertyDataHandler<short>
    {
        
    }
}
