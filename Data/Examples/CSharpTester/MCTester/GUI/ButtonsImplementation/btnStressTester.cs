using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using MCTester.Managers.MapWorld;
using System.IO;
using System.Xml.Serialization;
using MCTester.General_Forms;
using UnmanagedWrapper;
using MCTester.GUI.Map;
using System.Reflection;
using MapCore;

namespace MCTester.ButtonsImplementation
{
    public class btnStressTester
    {
        private OpenFileDialog OFD;
        private MCTStressTest cMCTStressTest;
        private Random m_Rand;
        private IDNMcMapViewport mMapViewport;
        private IDNMcMapCamera mActiveCamera;
        private StreamWriter STW;
        private PerformanceCounter performanceCounter;
        private System.Windows.Forms.Timer mTmr;
        private int mCurrTickNum;
        private int timeoutTicks;
        private List<string> lAactionsToExecute = new List<string>();
        private int mNumIterations = 1;
        private int mCurrIterationNum = 0;
        private int mTestCaseNum = 0;
        private int mActionNum = 0;
        private DateTime mStartRun;

        public btnStressTester()
        {
            OFD = new OpenFileDialog();
            m_Rand = new Random();
            mStartRun = new DateTime();

            performanceCounter = new PerformanceCounter();
            performanceCounter.CategoryName = "Process";
            performanceCounter.CounterName = "Private Bytes";
            performanceCounter.InstanceName = Process.GetCurrentProcess().ProcessName;

            mTmr = new System.Windows.Forms.Timer();
            mCurrTickNum = 0;  
        }

        public void ExecuteAction()
        {
            mMapViewport = MCTMapFormManager.MapForm.Viewport;
            mActiveCamera = MCTMapFormManager.MapForm.Viewport.ActiveCamera;

            OFD.RestoreDirectory = true;
            if (OFD.ShowDialog() == DialogResult.OK)
            {
                cMCTStressTest = null;
                try
                {
                    StreamReader SR = new StreamReader(OFD.FileName);
                    XmlSerializer Xser = new XmlSerializer(typeof(MCTStressTest));

                    cMCTStressTest = (MCTStressTest)Xser.Deserialize(SR);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("LoadXmlFile", McEx);
                }

                // open stream writer
                string path = OFD.FileName.Replace(".xml", "") + "_MemoryUsage" + "_" + DateTime.Today.Day.ToString() + "_" + DateTime.Today.Month.ToString() + "_" + DateTime.Today.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + ".txt";
                STW = new StreamWriter(path);
                STW.WriteLine("This log file display the MCTester process memory usage (of kind Private Byte) in KB");
                STW.WriteLine("Iteration Number:    " + (mCurrIterationNum + 1).ToString());

                mNumIterations = cMCTStressTest.GeneralDefinitions.TestIterationsNumber;
                timeoutTicks = cMCTStressTest.GeneralDefinitions.GapBetweenActions;

                mTmr.Interval = MCTMapForm.RenderTimer.Interval;
                mTmr.Tick += new EventHandler(mTmr_Tick);
                mStartRun = DateTime.Now;
                mTmr.Start();

            }
        }

        void mTmr_Tick(object sender, EventArgs e)
        {
            object[] paramsArr = new object[1];
            STW.WriteLine(performanceCounter.NextValue() / 1024);

            if (mCurrTickNum >= timeoutTicks * 16)
            {
                // every timeoutTicks perform one action and stop the timer at the end of the actions list.
                if (mCurrIterationNum < mNumIterations)
                {
                    if (mTestCaseNum < cMCTStressTest.TestCase.Count)
                    {
                        paramsArr[0] = mTestCaseNum;
                        if (mActionNum < cMCTStressTest.TestCase[mTestCaseNum].TestManager.Count)
                        {
                            // print action name
                            STW.WriteLine(cMCTStressTest.TestCase[mTestCaseNum].TestManager[mActionNum]);

                            MethodInfo MI = this.GetType().GetMethod(cMCTStressTest.TestCase[mTestCaseNum].TestManager[mActionNum]);
                            MI.Invoke(this, paramsArr);

                            // turn on all viewports render needed flags
                            Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();

                            mActionNum++;
                        }
                        else
                        {
                            mActionNum = 0;
                            mTestCaseNum++;
                        }
                    }
                    else
                    {
                        mTestCaseNum = 0;
                        mCurrIterationNum++;

                        if (mCurrIterationNum < mNumIterations)
                            STW.WriteLine("Iteration Number:    " + (mCurrIterationNum + 1).ToString());
                        else
                        {
                            mTmr.Tick -= new EventHandler(mTmr_Tick);
                            mTmr.Stop();
                            STW.Close();
                            DateTime endRun = DateTime.Now;
                            MessageBox.Show("Auto stress test scenario completed successfully.\n\n" +
                                                "Test started at:\t" + mStartRun.ToString() + "\n" +
                                                "Test ended at:\t" + endRun.ToString() + "\n" +
                                                "Total time:\t" + (endRun - mStartRun).ToString()
                                                , "scenario completed successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            mCurrIterationNum = 0;
                        }
                    }
                }

                mCurrTickNum = 0;
            }
            else
                mCurrTickNum++;
        }

        public void LoadObjects(int testCaseNumber)
        {
            UserDataFactory UDF = new UserDataFactory();
            IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;
            Dictionary<IDNMcObject, int> objectParams = new Dictionary<IDNMcObject, int>();

            foreach (cObjectsParams obj in cMCTStressTest.TestCase[testCaseNumber].LoadObjects.LoadedObjectsParams)
            {
                try
                {
                    IDNMcObject[] objArr = activeOverlay.LoadObjects(obj.ObjectPath, UDF);
                    objectParams.Add(objArr[0], obj.AmountObjects);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("LoadObjects", McEx);
                    return;
                }
            }

            foreach (IDNMcObject obj in objectParams.Keys)
            {
                IDNMcObjectScheme scheme = obj.GetScheme();
                DNSMcVector3D[] objOriginLocationPoints = obj.GetLocationPoints(0);
                int ObjNumPoints = objOriginLocationPoints.Length;
				DNSMcVector3D[] objLocationPointsAfterShifting;
				if (ObjNumPoints > 0)
				{
					objLocationPointsAfterShifting = new DNSMcVector3D[ObjNumPoints];
				}
				else
				{
					objLocationPointsAfterShifting = new DNSMcVector3D[1];
				}
                int iterations = objectParams[obj];

                for (int numObj = 0; numObj < iterations; numObj++)
                {
                    DNSMcVector3D distanceDelta = new DNSMcVector3D(); ;
                    double NewXPoint = m_Rand.Next((int)cMCTStressTest.TestCase[testCaseNumber].LoadObjects.BoxLoadingArea.ObjectLoadingArea.MinVertex.x, (int)cMCTStressTest.TestCase[testCaseNumber].LoadObjects.BoxLoadingArea.ObjectLoadingArea.MaxVertex.x);
                    double NewYPoint = m_Rand.Next((int)cMCTStressTest.TestCase[testCaseNumber].LoadObjects.BoxLoadingArea.ObjectLoadingArea.MinVertex.y, (int)cMCTStressTest.TestCase[testCaseNumber].LoadObjects.BoxLoadingArea.ObjectLoadingArea.MaxVertex.y);
					if (ObjNumPoints > 0)
					{
						distanceDelta.x = NewXPoint - objOriginLocationPoints[0].x;
						distanceDelta.y = NewYPoint - objOriginLocationPoints[0].y;

						for (int locationIdx = 0; locationIdx < ObjNumPoints; locationIdx++)
						{
							objLocationPointsAfterShifting[locationIdx].x = objOriginLocationPoints[locationIdx].x + distanceDelta.x;
							objLocationPointsAfterShifting[locationIdx].y = objOriginLocationPoints[locationIdx].y + distanceDelta.y;
							objLocationPointsAfterShifting[locationIdx].z = objOriginLocationPoints[locationIdx].z + distanceDelta.z;
						}
					}
					else
					{
						distanceDelta.x = NewXPoint;
						distanceDelta.y = NewYPoint;

						objLocationPointsAfterShifting[0].x = distanceDelta.x;
						objLocationPointsAfterShifting[0].y = distanceDelta.y;
						objLocationPointsAfterShifting[0].z = distanceDelta.z;
					}

                    try
                    {
                        IDNMcObject cloneObj = obj.Clone(activeOverlay, false, true);
                        cloneObj.SetLocationPoints(objLocationPointsAfterShifting, 0);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("DNMcObject.Clone", McEx);
                    }
                }

                // remove the original object
                obj.Remove();
            }
        }

        public void RemoveObjects(int testCaseNumber)
        {
            IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;
            IDNMcObject[] objects = activeOverlay.GetObjects();
            int amountToRemove = cMCTStressTest.TestCase[testCaseNumber].RemoveObjects.AmountToRemove;

            if (amountToRemove > objects.Length)
                amountToRemove = objects.Length;

            for (int i = 0; i < amountToRemove; i++)
            {
                objects[i].Remove();
                //objects[i].Dispose();
            }
        }

        public void StartObjcetsAnimation(int testCaseNumber)
        {
            IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;
            IDNMcObject[] objects = activeOverlay.GetObjects();


            List<cPathAnimationNode> pathAnimationNode = cMCTStressTest.TestCase[testCaseNumber].StartObjcetsAnimation.lPathAnimationNode;

            DNSPathAnimationNode[] animationNode = new DNSPathAnimationNode[pathAnimationNode.Count];
            for (int i = 0; i < pathAnimationNode.Count; i++)
            {
                animationNode[i].Position.x = pathAnimationNode[i].PointX;
                animationNode[i].Position.y = pathAnimationNode[i].PointY;
                animationNode[i].Position.z = pathAnimationNode[i].PointZ;
                animationNode[i].fTime = pathAnimationNode[i].Time;
                animationNode[i].ManualRotation.fYaw = pathAnimationNode[i].Yaw;
                animationNode[i].ManualRotation.fPitch = pathAnimationNode[i].Pitch;
                animationNode[i].ManualRotation.fRoll = pathAnimationNode[i].Roll;
                animationNode[i].ManualRotation.bRelativeToCurrOrientation = pathAnimationNode[i].RelativeToCurrentOrientation;
            }

            int numObj = cMCTStressTest.TestCase[testCaseNumber].StartObjcetsAnimation.NumObjectToAnimate;
            // in case that user set the number of object to '-1' or higher then the number of existing objects it work on all objects
            if (numObj == -1 || numObj > objects.Length)
                numObj = objects.Length;

            for (int z = 0; z < numObj; z++)
            {
                try
                {
                    objects[z].PlayPathAnimation(animationNode,
                                            cMCTStressTest.TestCase[testCaseNumber].StartObjcetsAnimation.PositionInterpulationMode,
                                            cMCTStressTest.TestCase[testCaseNumber].StartObjcetsAnimation.RotationInterpulationMode,
                                            cMCTStressTest.TestCase[testCaseNumber].StartObjcetsAnimation.StartingTimePoint,
                                            cMCTStressTest.TestCase[testCaseNumber].StartObjcetsAnimation.RotationAdditionalYaw,
                                            cMCTStressTest.TestCase[testCaseNumber].StartObjcetsAnimation.AutomaticRotation,
                                            cMCTStressTest.TestCase[testCaseNumber].StartObjcetsAnimation.Loop);

                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("PlayPathAnimation", McEx);
                }
            }
        }

        public void StopObjcetsAnimation(int testCaseNumber)
        {
            IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;
            IDNMcObject[] objects = activeOverlay.GetObjects();

            int numObj = cMCTStressTest.TestCase[testCaseNumber].StopObjcetsAnimation.NumObjectToStop;
            // in case that user set the number of object to '-1' or higher then the number of existing objects it work on all objects
            if (numObj == -1 || numObj > objects.Length)
                numObj = objects.Length;

            for (int i = 0; i < numObj; i++)
            {
                try
                {
                    objects[i].StopPathAnimation();
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("StopPathAnimation", McEx);
                }
            }
        }

        public void MapZoom(int testCaseNumber)
        {
            int iterations = cMCTStressTest.TestCase[testCaseNumber].MapZoom.ZoomIterations;
            if (mMapViewport.MapType == DNEMapType._EMT_2D)
            {
                for (int i = 0; i < iterations; i++)
                {
                    try
                    {
                        float currScale = mMapViewport.GetCameraScale();
                        mActiveCamera.SetCameraScale(currScale * cMCTStressTest.TestCase[testCaseNumber].MapZoom.ZoomFactor);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("SetCameraScale", McEx);
                    }
                }
            }
            else
            {
                for (int i = 0; i < iterations; i++)
                {
                    try
                    {
                        float currFOV = mMapViewport.GetCameraFieldOfView();
                        mActiveCamera.SetCameraFieldOfView(currFOV * cMCTStressTest.TestCase[testCaseNumber].MapZoom.ZoomFactor);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("SetCameraFieldOfView", McEx);
                    }
                }
            }
        }
    }
}
