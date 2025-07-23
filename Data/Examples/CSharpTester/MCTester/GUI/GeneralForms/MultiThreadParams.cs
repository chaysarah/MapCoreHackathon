using MapCore;
using System.IO;
using UnmanagedWrapper;
using MCTester.Managers.MapWorld;
using System.Collections.Generic;
using System;
namespace MCTester.General_Forms
{
    public class MultiThreadParams
    {
        public enum FuncCalc
        {
            TerrainHeight,
            TerrainHeightsAlongLine,
            RayIntersectionPoint,
            RayDirection,
            FrameImageCalc,
            ImageToWorld
        }

        public struct ExceptionsThrows
        {
            public int ErrorNumber;
            public int IndexPoint;
            public FuncCalc Function;
        }

        public int m_NumTestingPoints;
        public DNSMcVector3D OriginPoint;
        public DNSMcVector3D[] TargetPoints;
        public double[] ResGetTerrainHeight;
        public bool[] ResGetTerrainHeightIsFoundHeight;
        public float[] ResGetTerrainHeightsAlongLine;
        public DNSMcVector3D?[] ResGetRayIntersectionPoint;
        public DNSMcVector3D[] ResRayDirection;
        public bool PrintToLog;
        public bool ExitRun = false;
        public IDNMcFrameImageCalc[] ResFrameImageCalc;
        public IDNMcFrameImageCalc[] ThreadsFrameImageCalc;
        public IDNMcFrameImageCalc originalImageCalc;
        public DNSCameraParams OriginalframeCameraParams;
        public DNSCameraParams[] frameCameraParams;
        public IDNMcDtmMapLayer DTMlayer;
        public IDNMcGridCoordinateSystem coorSys;
        public DNSMcVector2D imagePoint;
        public DNSMcVector3D[] ResImageToWorld;
        public List<ExceptionsThrows> ExceptionsThrowsInThreads;

        public StreamWriter m_STW;
        public DNSQueryParams QueryParams;

        public void SetResultArraySize()
        {
            TargetPoints = new DNSMcVector3D[m_NumTestingPoints];
            ResGetTerrainHeight = new double[m_NumTestingPoints];
            ResGetTerrainHeightIsFoundHeight = new bool[m_NumTestingPoints];
            ResGetTerrainHeightsAlongLine = new float[m_NumTestingPoints];
            ResGetRayIntersectionPoint = new DNSMcVector3D?[m_NumTestingPoints];
            ResRayDirection = new DNSMcVector3D[m_NumTestingPoints];
            ResFrameImageCalc = new IDNMcFrameImageCalc[m_NumTestingPoints];
            ThreadsFrameImageCalc = new IDNMcFrameImageCalc[m_NumTestingPoints];
            frameCameraParams = new DNSCameraParams[m_NumTestingPoints];
            ResImageToWorld = new DNSMcVector3D[m_NumTestingPoints];
            imagePoint = new DNSMcVector2D(250, 250);
            ExceptionsThrowsInThreads = new List<ExceptionsThrows>();
        }

        public void AddException(int errorCode, int indexPoint, FuncCalc function)
        {
            ExceptionsThrows exp = new ExceptionsThrows();
            exp.ErrorNumber = errorCode;
            exp.IndexPoint = indexPoint;
            exp.Function = function;
            ExceptionsThrowsInThreads.Add(exp);
        }

        public bool IsExistExceptionsThrows(int errorCode, int indexPoint, FuncCalc function)
        {
            return ExceptionsThrowsInThreads.Exists(x => 
                   ((x.ErrorNumber == errorCode) &&
                    (x.IndexPoint == indexPoint) &&
                    (x.Function == function)));
        }

        public void GenerateLog(string FuncName, string sourcePt ,string resultPt)
        {
            lock (m_STW)
            {
                m_STW.WriteLine(FuncName + ":\t" + "Single Thread Result:\t" + sourcePt + "\t\t Multi Thread Result:\t" + resultPt);              
            }
        }
        
    }
}
