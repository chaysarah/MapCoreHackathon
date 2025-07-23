using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers.MapWorld;
using System.Diagnostics;
using MCTester.GUI.Map;


namespace MCTester.GUI.Forms
{
    public partial class AutoLoadedObjForm : Form
    {
        Random randX;
        Random randY;

        Random randMovementX;
        Random randMovementY;

        int ChangeXPoint;
        int ChangeYPoint;
        IDNMcOverlay activeOverlay;
        List<IDNMcObject> clonedObjectsLst = new List<IDNMcObject>();
        Timer mMovementTimer;
        int counterMovement = 0;
        int counterAngle = 0;
        float[] diffX, diffY;
        IDNMcMapViewport currVP;
        Stopwatch watch; 

        public AutoLoadedObjForm()
        {
            InitializeComponent();

            mMovementTimer = new Timer();
            mMovementTimer.Interval = 1;
            mMovementTimer.Tick += new EventHandler(mMovementTimer_Tick);

            randX = new Random(new System.DateTime().Millisecond);
            randMovementX = new Random(new System.DateTime().Millisecond);
            randY = new Random(new System.DateTime().Millisecond);
            randMovementY = new Random(new System.DateTime().Millisecond);

            if(MCTMapFormManager.MapForm != null)
                currVP = MCTMapFormManager.MapForm.Viewport;
            rbVisibleArea.Enabled = rbVisibleArea.Checked = currVP != null;
            rbSelectRect.Checked = currVP == null;
        }

        private void dgvLoadedObj_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.RestoreDirectory = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    dgvLoadedObj[0, e.RowIndex].Tag = ofd.FileName;
                    dgvLoadedObj[0, e.RowIndex].Value = ofd.FileName;
                }
            }
        }
        
        private void AutoLodededObjOK_Click(object sender, EventArgs e)
        {
            clonedObjectsLst = new List<IDNMcObject>();

            UserDataFactory UDF = new UserDataFactory();
            activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;
            int offsetX = (int)ctrl2DVectorOffset.X;
            int offsetY = (int)ctrl2DVectorOffset.Y;

            int currOffsetX = offsetX, currOffsetY = 0, counterOffsetY = 1, counterOffsetX = 0;
            DNSMcVector3D[] diffPoints ;
            if (activeOverlay != null)
            {
                try
                {
                    for (int indexRow = 0; indexRow < dgvLoadedObj.Rows.Count; indexRow++)
                    {
                        if (dgvLoadedObj.Rows[indexRow].IsNewRow == false)
                        {
                            object objPath = dgvLoadedObj[0, indexRow].Value;
                            object objAmount = dgvLoadedObj[1, indexRow].Value;
                            if (objPath == null)
                            {
                                MessageBox.Show("Missing file path in row " + indexRow);
                                return;
                            }
                            if (objAmount == null)
                            {
                                MessageBox.Show("Missing amount in row " + indexRow);
                                return;
                            }
                            string strPath = objPath.ToString();

                            IDNMcObject[] loadedObj = activeOverlay.LoadObjects(strPath, UDF);
                            if (loadedObj != null)
                            {
                                IDNMcObject objectLoaded = loadedObj[0];
                                DNSMcVector3D[] objLocationPoints = objectLoaded.GetLocationPoints(0);
                                if (objLocationPoints != null)
                                {
                                    int NumVertexes = objLocationPoints.Length;
                                    diffX = new float[NumVertexes - 1];
                                    diffY = new float[NumVertexes - 1];
                                    diffPoints = new DNSMcVector3D[NumVertexes];
                                    diffPoints[0] = new DNSMcVector3D(0, 0, 0);

                                    if (NumVertexes > 1)
                                    {
                                        for (int z = 0; z < NumVertexes - 1; z++)
                                        {
                                            diffPoints[z + 1] = objLocationPoints[z + 1] - objLocationPoints[0];
                                        }
                                    }

                                    int objectAmount = int.Parse(objAmount.ToString());

                                    uint widthDimension, heightDimension;

                                    DNSMcVector3D minWorldPoint = new DNSMcVector3D();
                                    DNSMcVector3D maxWorldPoint = new DNSMcVector3D();
                                    bool isIntersection;

                                    if (rbVisibleArea.Checked)
                                    {
                                        currVP.GetViewportSize(out widthDimension, out heightDimension);
                                        DNSMcVector3D minScreenPoint = new DNSMcVector3D(0, 0, 0);
                                        DNSMcVector3D maxScreenPoint = new DNSMcVector3D(widthDimension, heightDimension, 0);

                                        currVP.ScreenToWorldOnTerrain(minScreenPoint, out minWorldPoint, out isIntersection);
                                        if (isIntersection == false)
                                            currVP.ScreenToWorldOnPlane(minScreenPoint, out minWorldPoint, out isIntersection);
                                        minWorldPoint = currVP.ViewportToOverlayManagerWorld(minWorldPoint, currVP.OverlayManager);

                                        currVP.ScreenToWorldOnTerrain(maxScreenPoint, out maxWorldPoint, out isIntersection);
                                        if (isIntersection == false)
                                            currVP.ScreenToWorldOnPlane(maxScreenPoint, out maxWorldPoint, out isIntersection);
                                        maxWorldPoint = currVP.ViewportToOverlayManagerWorld(maxWorldPoint, currVP.OverlayManager);

                                    }
                                    else  // if rbSelectRect.checked
                                    {
                                        DNSMcFVector3D rectPoint = ctrl3DFVectorCenterPoint.GetVector3D();
                                        int rectWidth = ntxRadiusX.GetInt32() / 2;
                                        int rectHeight = ntxRadiusY.GetInt32() / 2;

                                        minWorldPoint = new DNSMcVector3D(rectPoint.x - rectWidth,
                                                                          rectPoint.y + rectHeight, 0);

                                        maxWorldPoint = new DNSMcVector3D(rectPoint.x + rectWidth,
                                                                          rectPoint.y - rectHeight, 0);
                                    }

                                    for (int i = 0; i < objectAmount; i++)
                                    {
                                        if (rbRandom.Checked)
                                        {
                                            //randX.Next(ntxRadiusX.GetInt32());
                                            ChangeXPoint = randX.Next((int)minWorldPoint.x, (int)maxWorldPoint.x);
                                            ChangeYPoint = randY.Next((int)maxWorldPoint.y, (int)minWorldPoint.y);
                                        }
                                        else //if(rbFixedOffset.Checked)
                                        {
                                            int x = (int)minWorldPoint.x + (int)counterOffsetX * currOffsetX;
                                            int y = (int)minWorldPoint.y + (int)currOffsetY;
                                            counterOffsetX++;

                                            if (x > maxWorldPoint.x)
                                            {
                                                x = (int)minWorldPoint.x;
                                                currOffsetY = offsetY * counterOffsetY * (-1);
                                                y = (int)minWorldPoint.y + (int)currOffsetY;
                                                counterOffsetY++;
                                                counterOffsetX = 0;
                                            }

                                            if (y < maxWorldPoint.y)
                                            {
                                                x = (int)minWorldPoint.x;
                                                y = (int)minWorldPoint.y;
                                                counterOffsetY = 1; counterOffsetX = 0;
                                            }
                                            ChangeXPoint = x;
                                            ChangeYPoint = y;
                                        }
                                        DNSMcVector3D[] locationPoints = new DNSMcVector3D[NumVertexes];

                                        for (int z = 0; z < NumVertexes; z++)
                                        {
                                            locationPoints[z].x = ChangeXPoint + diffPoints[z].x;
                                            locationPoints[z].y = ChangeYPoint + diffPoints[z].y;
                                            locationPoints[z].z = 0;
                                        }

                                        Console.WriteLine(ChangeXPoint + ",   " + ChangeYPoint);

                                        if (loadedObj[0] != null)
                                        {
                                            IDNMcObject cloneObj = loadedObj[0].Clone(activeOverlay, chxIsClonedSchemes.Checked);
                                            cloneObj.SetLocationPoints(locationPoints, 0);
                                            clonedObjectsLst.Add(cloneObj);
                                        }
                                    }

                                    // remove original loaded objects
                                    foreach (IDNMcObject obj in loadedObj)
                                        obj.Remove();
                                }
                            }
                        }
                    }
                    EnabledIterationsControls();
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("LoadObjects", McEx);
                }
            }
            else
                MessageBox.Show("There is no active overlay", "Missing Overlay");
        }

        private void EnabledIterationsControls()
        {
            grpIterationPrm.Enabled = clonedObjectsLst.Count > 0 ? true : false;
        }

        private void rbSelectRadius_CheckedChanged(object sender, EventArgs e)
        {
            gbCenterPointAndRadius.Enabled = rbSelectRect.Checked;
        }

        private void rbFixedOffset_CheckedChanged(object sender, EventArgs e)
        {
            ctrl2DVectorOffset.Enabled = rbFixedOffset.Checked;
        }

        int movementX, movementY, noIterationNotCount, propertyId, totalIteration;
        float angle;
        
        private void btnStartIterations_Click(object sender, EventArgs e)
        {
            MCTMapForm.eRender = MCTMapForm.RenderType.Manual;
            movementX = (int)ctrl2DVectorMovement.X;
            movementY = (int)ctrl2DVectorMovement.Y;
            angle = ntxAngle.GetFloat();
            noIterationNotCount = ntxNoIterationNotCount.GetInt32();
            totalIteration = ntxTotalIteration.GetInt32();
            if (noIterationNotCount > totalIteration)
            {
                MessageBox.Show("No. Iteration Not Count Times needed to be less from Total Iteration.", "Error Input Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            propertyId = ntxPropertyId.GetInt32();

            mMovementTimer.Start();
            watch = Stopwatch.StartNew();
            watch.Reset();
        }

        private void mMovementTimer_Tick(object sender, EventArgs e)
        {
         
            if (totalIteration > 0)
                counterMovement++;
            if (counterMovement == noIterationNotCount)
                watch = Stopwatch.StartNew();
            try
            {
                foreach (IDNMcObject obj in clonedObjectsLst)
                {
                    DNSMcVector3D[] objLocationPoints = obj.GetLocationPoints(0);
                    int NumVertexes = objLocationPoints.Length;
                    DNSMcVector3D[] locationPoints = new DNSMcVector3D[NumVertexes];

                    if(chxRandom.Checked)
                    {
                        movementX = randMovementX.Next(Math.Abs((int)ctrl2DVectorMovement.X));
                        if (ctrl2DVectorMovement.X < 0)
                            movementX = movementX * -1;
                        movementY = randMovementY.Next(Math.Abs((int)ctrl2DVectorMovement.Y));
                        if (ctrl2DVectorMovement.Y < 0)
                            movementY = movementY * -1;
                    }
                    for (int z = 0; z < NumVertexes; z++)
                    {
                        locationPoints[z].x = objLocationPoints[z].x + movementX;
                        locationPoints[z].y = objLocationPoints[z].y - movementY;
                        locationPoints[z].z = objLocationPoints[z].z;
                    }

                    obj.SetLocationPoints(locationPoints, 0);
                    
                    try
                    {
                        if (angle > 0 && propertyId > 0)
                            obj.SetFloatProperty((uint)propertyId, angle + counterAngle);
                    }
                    catch (MapCoreException mc)
                    {
                        StopTimer();
                        MapCore.Common.Utilities.ShowErrorMessage("SetFloatProperty", mc);
                    }

                } 
                currVP.Render();

                counterAngle += (int)angle;
                if (counterAngle > 360)
                    counterAngle = 0;

                if (totalIteration > 0 && counterMovement == totalIteration)
                {
                    counterMovement = 0;
                    watch.Stop();
                    String str = "The " + (totalIteration - noIterationNotCount) + " Iterations took " + watch.ElapsedMilliseconds + " total ms time; \r\n";
                    tbOutput.AppendText(str);
                }
            }
            catch (MapCoreException mc)
            { 
                StopTimer();
                MapCore.Common.Utilities.ShowErrorMessage("Set/GetLocationPoints", mc);
               
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopTimer();
            counterMovement = 0;
        }

        private void AutoLoadedObjForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopTimer();
        }

        private void StopTimer()
        {
            if (mMovementTimer.Enabled)
            {
                mMovementTimer.Stop();
                watch.Stop();
            }
            MCTMapForm.eRender = MCTMapForm.RenderType.FlagBaseRender;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void ntxAngle_TextChanged(object sender, EventArgs e)
        {

        }

        private void AutoLoadedObjForm_Load(object sender, EventArgs e)
        {

        }

        private void btnDeleteObjects_Click(object sender, EventArgs e)
        {
            foreach (IDNMcObject obj in clonedObjectsLst)
                obj.Remove();
            clonedObjectsLst.Clear();
            EnabledIterationsControls();
        }

        private void rbMovementOffsetPoints_CheckedChanged(object sender, EventArgs e)
        {
            //ctrl2DVectorMovement.Enabled = rbMovementOffsetPoints.Checked;
        }

        private void rbMovementRandomPoints_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}