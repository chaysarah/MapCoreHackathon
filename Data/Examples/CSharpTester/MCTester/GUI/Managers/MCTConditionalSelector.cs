using MapCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCTester.Managers
{
    public class MCTConditionalSelectorProperty
    {
        IDNMcConditionalSelector m_ConditionalSelector;
        bool m_bActionOnResult;
        public static delegateLoadDicSelectorConditional delegateLoad;
        public static delegateSaveDicSelectorConditional delegateSave;

        public delegate void delegateLoadDicSelectorConditional(DNEActionType key, out bool bActionOnResult, out IDNMcConditionalSelector value, out uint puPropertyID, DNSByteBool uObjectStateToServe);
        public delegate void delegateSaveDicSelectorConditional(DNEActionType key, bool bActionOnResult, IDNMcConditionalSelector value, uint puPropertyID, DNSByteBool uObjectStateToServe);
        

        public MCTConditionalSelectorProperty(IDNMcConditionalSelector conditionalSelector, bool bActionOnResult)
        {
            m_ConditionalSelector = conditionalSelector;
            m_bActionOnResult = bActionOnResult;
        }

        public IDNMcConditionalSelector ConditionalSelector
        {
            get { return m_ConditionalSelector; }
            set { m_ConditionalSelector = value; }
        }

        public bool ActionOnResult
        {
            get { return m_bActionOnResult; }
            set { m_bActionOnResult = value; }
        }


        public static void Load(DNEActionType key, out MCTConditionalSelectorProperty value, out uint puPropertyID, DNSByteBool uObjectStateToServe)
        {
            bool bActionOnResult;
            IDNMcConditionalSelector selector;
            delegateLoad(key, out bActionOnResult, out selector, out puPropertyID, uObjectStateToServe);
            value = new MCTConditionalSelectorProperty(selector,bActionOnResult );
            Manager_MCPropertyDescription.InsertNode((IDNMcObjectSchemeNode)delegateLoad.Target, puPropertyID, delegateLoad.Method.Name);
        }

        public static void Save(DNEActionType key, MCTConditionalSelectorProperty value, uint puPropertyID, DNSByteBool uObjectStateToServe)
        {
            delegateSave(key, value.ActionOnResult, value.ConditionalSelector, puPropertyID, uObjectStateToServe);
            Manager_MCPropertyDescription.InsertNode((IDNMcObjectSchemeNode)delegateSave.Target, puPropertyID, delegateSave.Method.Name);

        }
    }
}
