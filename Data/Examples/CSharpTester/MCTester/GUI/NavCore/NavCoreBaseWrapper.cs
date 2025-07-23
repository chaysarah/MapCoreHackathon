using System;
using System.Runtime.InteropServices;

namespace NavCore.Net
{
    // NavCoreBaseWrapper:
    public class NavCoreBaseWrapper : IDisposable
    {
#if DEBUG
        private const string m_navCoreBaseDllPath = @"NavCoreD.dll";
#else
        private const string m_navCoreBaseDllPath = @"NavCore.dll";
#endif


        // Initializations:
        [DllImport(m_navCoreBaseDllPath, CallingConvention = CallingConvention.Cdecl)]
        private static extern ECode ITnNavCoreBase_ConstructParams(ref SParams pParams);

        [DllImport(m_navCoreBaseDllPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern ECode ITnNavCoreBase_Create(
            ref SParams oParams,
            [MarshalAs(UnmanagedType.LPStr)] string sNavCoreDir,
            ref IntPtr ppOutNavCoreBaseInstance);

        // Destroy:
        [DllImport(m_navCoreBaseDllPath, CallingConvention = CallingConvention.Cdecl)]
        private static extern ECode ITnNavCoreBase_Delete(IntPtr handle);

        [DllImport(m_navCoreBaseDllPath, CallingConvention = CallingConvention.Cdecl)]
        private static extern ECode ITnNavCoreBase_DeletePointsArray(ref SNavCorePointsArray OutPathPoints);

        // Engine:
        [DllImport(m_navCoreBaseDllPath, CallingConvention = CallingConvention.Cdecl)]
        private static extern ECode ITnNavCoreBase_Load(IntPtr handle, ref STnBox boundingBox, ref ELoadStatus eOutLoadStatus);

        [DllImport(m_navCoreBaseDllPath, CallingConvention = CallingConvention.Cdecl)]
        private static extern ECode ITnNavCoreBase_FindPath(
            IntPtr handle,
            STnVector3D[] aPathPoints,
            UInt32 nNumPathPoints,
            ref SNavCorePointsArray pOutPathPoints,
            ref EFindPathStatus eOutPathStatus);

        // Setters:
        [DllImport(m_navCoreBaseDllPath, CallingConvention = CallingConvention.Cdecl)]
        private static extern ECode ITnNavCoreBase_SetRoadsCost(IntPtr handle, float NewRoadsCost);

        [DllImport(m_navCoreBaseDllPath, CallingConvention = CallingConvention.Cdecl)]
        private static extern ECode ITnNavCoreBase_SetSoilCost(IntPtr handle, float NewSoilCost);

        [DllImport(m_navCoreBaseDllPath, CallingConvention = CallingConvention.Cdecl)]
        private static extern ECode ITnNavCoreBase_SetLimitedPassabilityAreasCost(IntPtr handle, float NewRoadsCost);

        [DllImport(m_navCoreBaseDllPath, CallingConvention = CallingConvention.Cdecl)]
        private static extern ECode ITnNavCoreBase_SetVeryLimitedPassabilityAreasCost(IntPtr handle, float NewSoilCost);

        // Getters:
        [DllImport(m_navCoreBaseDllPath, CallingConvention = CallingConvention.Cdecl)]
        private static extern ECode ITnNavCoreBase_GetCoordSysEpsgCode(IntPtr handle, ref UInt32 uOutEpsgCode);

        [DllImport(m_navCoreBaseDllPath, CallingConvention = CallingConvention.Cdecl)]
        private static extern ECode ITnNavCoreBase_GetBoundingBox(IntPtr handle, ref STnBox pBoundingBox);

        [DllImport(m_navCoreBaseDllPath, CallingConvention = CallingConvention.Cdecl)]
        private static extern ECode ITnNavCoreBase_GetMaxRoiAreaSupported(IntPtr handle, ref double dOutMaxRoiAreaSupported);

        [DllImport(m_navCoreBaseDllPath, CallingConvention = CallingConvention.Cdecl)]
        private static extern ECode ITnNavCoreBase_GetNavigationParamsCostRegion(IntPtr handle, ref float fOutMinCost, ref float fOutMaxCost);

        // ==============================================================================

        private IntPtr m_NavCore = IntPtr.Zero;

        private string m_lastError;

        public string LastError
        {
            get { return m_lastError; }
        }

        // Initializations:
        public ECode Init(ref SParams param)
        {
            m_lastError = "";
            try
            {
                ECode status = ITnNavCoreBase_ConstructParams(ref param);
                return status;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                m_lastError = ex.Message;
            }
            return ECode.FAILURE;
        }

        public ECode NavCoreCreate(
            ref SParams param,
            string NavCoreDir)
        {
            m_lastError = "";
            try
            {
                ECode status = ITnNavCoreBase_Create(ref param, NavCoreDir, ref m_NavCore);
                return status;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                m_lastError = ex.Message;
            }

            return ECode.FAILURE;
        }

        // Destroy:
        public void Dispose()
        {
            if (m_NavCore != IntPtr.Zero)
            {
                ITnNavCoreBase_Delete(m_NavCore);
                m_NavCore = IntPtr.Zero;
            }
        }

        public ECode DeletePointsArray(ref SNavCorePointsArray OutPathPoints)
        {
            return ITnNavCoreBase_DeletePointsArray(ref OutPathPoints);
        }

        // Engine:
        public ECode LoadNavMesh(STnBox boundingBox, ref ELoadStatus loadStatus)
        {
            m_lastError = "";
            try
            {
                if (m_NavCore == IntPtr.Zero)
                    throw new Exception("NavCore didn't initialized. Call NavCoreCreate first.");

                return ITnNavCoreBase_Load(m_NavCore, ref boundingBox, ref loadStatus);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                m_lastError = ex.Message;
            }
            loadStatus = ELoadStatus.ELS_NOT_LOADED;
            return ECode.FAILURE;
        }

        public ECode FindPath(STnVector3D[] aPathPoints, ref SNavCorePointsArray pOutPathPoints, ref EFindPathStatus findPathStatus)
        {
            m_lastError = "";
            try
            {
                if (m_NavCore == IntPtr.Zero)
                    throw new Exception("NavCore didn't initialized. Call NavCoreCreate first.");

                return ITnNavCoreBase_FindPath(m_NavCore, aPathPoints, (uint)aPathPoints.Length, ref pOutPathPoints, ref findPathStatus);
            }
            catch (Exception ex)
            {
                m_lastError = ex.Message;
                Console.WriteLine(ex.Message);
            }
            findPathStatus = EFindPathStatus.EFPS_UNKNOWN;
            return ECode.FAILURE;
        }

        // Setters:
        public ECode SetRoadsCost(float NewRoadsCost)
        {
            m_lastError = "";
            try
            {
                if (m_NavCore == IntPtr.Zero)
                    throw new Exception("NavCore didn't initialized. Call NavCoreCreate first.");

                return ITnNavCoreBase_SetRoadsCost(m_NavCore, NewRoadsCost);
            }
            catch (Exception ex)
            {
                m_lastError = ex.Message;
                Console.WriteLine(ex.Message);
            }
            return ECode.FAILURE;
        }

        public ECode SetSoilCost(float NewSoilCost)
        {
            m_lastError = "";
            try
            {
                if (m_NavCore == IntPtr.Zero)
                    throw new Exception("NavCore didn't initialized. Call NavCoreCreate first.");

                return ITnNavCoreBase_SetSoilCost(m_NavCore, NewSoilCost);
            }
            catch (Exception ex)
            {
                m_lastError = ex.Message;
                Console.WriteLine(ex.Message);
            }
            return ECode.FAILURE;
        }

        public ECode SetLimitedPassabilityAreasCost(float NewLimitedPassabilityAreasCost)
        {
            m_lastError = "";
            try
            {
                if (m_NavCore == IntPtr.Zero)
                    throw new Exception("NavCore didn't initialized. Call NavCoreCreate first.");

                return ITnNavCoreBase_SetLimitedPassabilityAreasCost(m_NavCore, NewLimitedPassabilityAreasCost);
            }
            catch (Exception ex)
            {
                m_lastError = ex.Message;
                Console.WriteLine(ex.Message);
            }
            return ECode.FAILURE;
        }

        public ECode SetVeryLimitedPassabilityAreasCost(float NewVeryLimitedPassabilityAreasCost)
        {
            m_lastError = "";
            try
            {
                if (m_NavCore == IntPtr.Zero)
                    throw new Exception("NavCore didn't initialized. Call NavCoreCreate first.");

                return ITnNavCoreBase_SetVeryLimitedPassabilityAreasCost(m_NavCore, NewVeryLimitedPassabilityAreasCost);
            }
            catch (Exception ex)
            {
                m_lastError = ex.Message;
                Console.WriteLine(ex.Message);
            }
            return ECode.FAILURE;
        }

        // Getters:
        public ECode GetCoordSysEpsgCode(ref UInt32 uOutEpsgCode)
        {
            m_lastError = "";
            try
            {
                if (m_NavCore == IntPtr.Zero)
                    throw new Exception("NavCore didn't initialized. Call NavCoreCreate first.");

                return ITnNavCoreBase_GetCoordSysEpsgCode(m_NavCore, ref uOutEpsgCode);
            }
            catch (Exception ex)
            {
                m_lastError = ex.Message;
                Console.WriteLine(ex.Message);
            }
            return ECode.FAILURE;
        }

        public ECode GetBoundingBox(ref STnBox pBoundingBox)
        {
            m_lastError = "";
            try
            {
                if (m_NavCore == IntPtr.Zero)
                    throw new Exception("NavCore didn't initialized. Call NavCoreCreate first.");

                return ITnNavCoreBase_GetBoundingBox(m_NavCore, ref pBoundingBox);
            }
            catch (Exception ex)
            {
                m_lastError = ex.Message;
                Console.WriteLine(ex.Message);
            }
            return ECode.FAILURE;
        }

        public ECode GetMaxRoiAreaSupported(ref double dOutMaxRoiAreaSupported)
        {
            m_lastError = "";
            try
            {
                if (m_NavCore == IntPtr.Zero)
                    throw new Exception("NavCore didn't initialized. Call NavCoreCreate first.");

                return ITnNavCoreBase_GetMaxRoiAreaSupported(m_NavCore, ref dOutMaxRoiAreaSupported);
            }
            catch (Exception ex)
            {
                m_lastError = ex.Message;
                Console.WriteLine(ex.Message);
            }
            return ECode.FAILURE;
        }

        public ECode GetNavigationParamsCostRegion(ref float fOutMinCost, ref float fOutMaxCost)
        {
            m_lastError = "";
            try
            {
                if (m_NavCore == IntPtr.Zero)
                    throw new Exception("NavCore didn't initialized. Call NavCoreCreate first.");

                return ITnNavCoreBase_GetNavigationParamsCostRegion(m_NavCore, ref fOutMinCost, ref fOutMaxCost);
            }
            catch (Exception ex)
            {
                m_lastError = ex.Message;
                Console.WriteLine(ex.Message);
            }
            return ECode.FAILURE;
        }
    }
}
