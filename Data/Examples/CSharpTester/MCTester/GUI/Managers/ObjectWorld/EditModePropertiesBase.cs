using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using UnmanagedWrapper;
using System.Windows.Forms;
using System.Drawing;

namespace MCTester.Managers.ObjectWorld
{
    public static class EditModePropertiesBase
    {
        #region Private Properties
        //static bool m_IsEditModeActive;

        static bool m_ChangeAutomaticallyAccordingObjectScheme;
        static bool m_EnableAddingNewPointsForMultiPointItem;
        static bool m_EnableDistanceDirectionMeasureForMultiPointItem;
        static bool m_DrawLine;
        static bool m_OneOperationOnly;
        static bool m_WaitForMouseClick;
        static DNESetVisibleArea3DOperation m_VisibleArea3DOperation;

        static bool m_ShowResult;
        static bool m_P2PWaitForClick;
        static bool m_DiscardChanges;

        static float m_DynamicZoomMinScale;
        static bool m_DynamicZoomWaitForClick;
        static IDNMcRectangleItem m_DynamicZoomRectangle;
        static DNESetVisibleArea3DOperation m_DynamicZoomOperation;
        static DNEMouseMoveUsage m_MouseMoveUsage;

        #endregion

        #region Public C-tor
        static EditModePropertiesBase()
        {
            //m_IsEditModeActive = false;
            m_ChangeAutomaticallyAccordingObjectScheme = false;
            m_EnableAddingNewPointsForMultiPointItem = false;
            m_DrawLine = true;
            m_OneOperationOnly = true;
            m_WaitForMouseClick = true;
            m_VisibleArea3DOperation = DNESetVisibleArea3DOperation._ESVAO_ROTATE_AND_MOVE;


            m_ShowResult = true;
            m_P2PWaitForClick = false;
            m_DiscardChanges = false;

            m_DynamicZoomMinScale = 0.001f; /* ~ (50.0f / 4000) */
            m_DynamicZoomWaitForClick = false;
            m_DynamicZoomRectangle = null;
            m_DynamicZoomOperation = DNESetVisibleArea3DOperation._ESVAO_ROTATE_AND_MOVE;
            
        }
        #endregion

        #region Public Properties
        //public static bool IsEditModeActive
        //{
        //    get { return m_IsEditModeActive; }
        //    set { m_IsEditModeActive = value; }
        //}

        public static bool ChangeAutomaticallyAccordingObjectScheme
        {
            get { return m_ChangeAutomaticallyAccordingObjectScheme; }
            set { m_ChangeAutomaticallyAccordingObjectScheme = value; }
        }

        public static bool EnableAddingNewPointsForMultiPointItem
        {
            get { return m_EnableAddingNewPointsForMultiPointItem; }
            set { m_EnableAddingNewPointsForMultiPointItem = value; }
        }

        
        public static bool EnableDistanceDirectionMeasureForMultiPointItem
        {
            get { return m_EnableDistanceDirectionMeasureForMultiPointItem; }
            set { m_EnableDistanceDirectionMeasureForMultiPointItem = value; }
        }

        public static bool DrawLine
        {
            get { return m_DrawLine; }
            set { m_DrawLine = value ; }
        }

        public static bool OneOperationOnly
        {
            get { return m_OneOperationOnly; }
            set { m_OneOperationOnly = value ; }
        }

        public static bool WaitForMouseClick
        {
            get { return m_WaitForMouseClick; }
            set { m_WaitForMouseClick = value ; }
        }

        public static DNESetVisibleArea3DOperation VisibleArea3DOperation
        {
            get { return m_VisibleArea3DOperation; }
            set { m_VisibleArea3DOperation = value; }
        }

        public static bool ShowResult
        {
            get { return m_ShowResult; }
            set { m_ShowResult = value ; }
        }

        public static bool P2PWaitForClick
        {
            get { return m_P2PWaitForClick; }
            set { m_P2PWaitForClick = value ; }
        }

        public static bool DiscardChanges
        {
            get { return m_DiscardChanges; }
            set { m_DiscardChanges = value; }
        }

        public static float DynamicZoomMinScale
        {
            get { return m_DynamicZoomMinScale; }
            set { m_DynamicZoomMinScale = value; }
        }

        public static bool DynamicZoomWaitForClick
        {
            get { return m_DynamicZoomWaitForClick; }
            set { m_DynamicZoomWaitForClick = value; }
        }

        public static IDNMcRectangleItem DynamicZoomRectangle
        {
            get { return m_DynamicZoomRectangle; }
            set { m_DynamicZoomRectangle = value; }
        }

        public static DNESetVisibleArea3DOperation DynamicZoomOperation
        {
            get { return m_DynamicZoomOperation; }
            set { m_DynamicZoomOperation = value; }
        }

        public static DNEMouseMoveUsage MouseMoveUsage
        {
            get { return m_MouseMoveUsage; }
            set { m_MouseMoveUsage = value; }
        }

        #endregion

    }
}
