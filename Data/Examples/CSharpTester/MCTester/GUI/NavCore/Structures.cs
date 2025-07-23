using System;
using System.Runtime.InteropServices;

namespace NavCore.Net
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SParams
    {
        public int m_MinHeight;
        public int m_MaxHeight;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SNavCoreParams
    {
        public byte m_NavMeshAlfa;
        public int m_MinHeight;
        public int m_MaxHeight;
        public float m_cellHeight;
        public float m_agentHeight;
        public float m_agentRadius;
        public float m_agentMaxClimb;
        public float m_agentMaxSlope;
        public float m_regionMinSize;
        public float m_regionMergeSize;
        public float m_edgeMaxLen;
        public float m_edgeMaxError;
        public int m_vertsPerPoly;
        public float m_detailSampleDist;
        public float m_detailSampleMaxError;
        public bool m_bDrawIn3D;
        public bool m_bDrawNavMeshDetail;
        public bool m_bDrawPolyMesh;
        public UInt32 m_MaxFindValidPathsIter;
        public UInt32 m_MaxPaths;
        public UInt32 m_MaxPathsDrawn; // m_MaxPathsDrawn <= m_MaxPaths !!!
        public bool m_bCalcStraightPath;
        public bool m_bDisplayPathCorridor;
        public float m_PathCorridorWidth;
        public float m_PathCostRatio;
        public UInt32 m_PathDistThreshSqr;
        public float m_SimilarityThresh;
        public bool m_bObstacleAvoidance;
        public bool m_bSeperation;
        public bool m_bOptimizeVisibility;
        public bool m_bOptimizeTopology;
        public bool m_bAnticipateTurns;
        public byte m_AvoidanceQuality;
        public float m_SeperationWeight;
        public bool m_bRunCrowdSimulation;
        public int m_SelectedAgentIdx;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct SNavCoreRTParams
    {
        // TraversabilityMap:
        public UInt32 traversabilityMapPixelByteCount; // 0
        public UInt32 traversabilityMapWidth; // 0
        public UInt32 traversabilityMapHeight; // 0
        public double traversabilityMapEdgeLength; // 0
        public double traversabilityThreshold; // 32.0

        // Short Path:
        public double shortPathdistanceBetweenPoints;  // 0.2
        public double shortPathmaximumDistanceBetweenPoints; // 0.3 
        public UInt32 shortPathmaximumNumOfPoints; // 200

        // Vehicle:
        public double vehicleSafetyCorridor; // 0.5
        public double vehicleHysteresisCorridor; //0.5

        // Policy:
        public bool useGlobalTraversabilityMap; // true
    };

    public struct SNavCorePointsArray
    {
        public IntPtr m_aPoints;
        public UInt32 m_uNumPoints;
    };

    // Structures:
    [Serializable()]
    [StructLayout(LayoutKind.Sequential)]
    public struct STnVector3D
    {
        public double x;
        public double y;
        public double z;

        public STnVector3D(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct STnBox
    {
        public STnVector3D MinVertex;
        public STnVector3D MaxVertex;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct STnRotation
    {
        public float fYaw;
        public float fPitch;
        public float fRoll;
        public bool bRelativeToCurrOrientation;

        public STnRotation(float yaw, float pitch, float roll)
        {
            fYaw = yaw;
            fPitch = pitch;
            fRoll = roll;
            bRelativeToCurrOrientation = false;
        }
    }
}
